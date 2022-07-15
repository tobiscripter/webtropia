using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace test.Controllers;

[ApiController]
[Route("chat")]
public class ChatController : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> Get([FromHeader]string email, [FromHeader]string password)
    {
        int id = await AccountController.GetID(email, password);
        if(id == 0) return Unauthorized();

        var cmd = await DB.getCommand("SELECT * FROM SELK_APP.CHAT_USER cu INNER JOIN CHAT c ON c.ID = cu.CHAT_ID WHERE USER_ID = @id");
        cmd.Parameters.AddWithValue("@id", id);
        return Ok(await DB.readJSONSQL(cmd));
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(string name, string type, string description, string tags, [FromHeader]string email, [FromHeader]string password)
    {
        int id = await AccountController.GetID(email, password);
        if(id == 0) return Unauthorized();

        var cmd = await DB.getCommand("INSERT INTO CHAT (TYPE, NAME, DESCRIPTION, TAGS) VALUES (@type, @name, @desc, @tags)");
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@desc", description);
        cmd.Parameters.AddWithValue("@tags", tags);
        cmd.Parameters.AddWithValue("@type", type);
        if((await DB.execSQL(cmd)) != 1) return Problem();


        var cmd2 = await DB.getCommand("INSERT INTO CHAT_USER (USER_ID, CHAT_ID, ROLE) VALUES(@user, @chat, \"ADMIN\")");
        cmd2.Parameters.AddWithValue("@user", id);
        cmd2.Parameters.AddWithValue("@chat", cmd.LastInsertedId);

        return Ok(await DB.execSQL(cmd2));
    }




}
