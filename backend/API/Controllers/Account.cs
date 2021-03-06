using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace test.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    public static async Task<int> GetID(string email, string password)
    {
        var cmd = await DB.getCommand("SELECT ID, PASSWORD, SALT FROM USER WHERE EMAIL = @email;");
        cmd.Parameters.AddWithValue("@email", email);

        var r = await DB.readSQL(cmd);
        await r.ReadAsync();
        var pass = r.GetString("PASSWORD");
        var salt = r.GetString("SALT");
        var id = r.GetInt32("ID");

        if(Encryption.HashPassword(password, salt) == pass) return id;
        return 0;
    }



    [HttpGet("")]
    public async Task<IActionResult> Login([FromHeader]string email, [FromHeader]string password)
    {
        var cmd = await DB.getCommand("SELECT ID, PASSWORD, SALT, PRIVATE_KEY, PUBLIC_KEY, USERNAME FROM USER WHERE EMAIL = @email;");
        cmd.Parameters.AddWithValue("@email", email);

        var r = await DB.readSQL(cmd);
        await r.ReadAsync();
        var pass = r.GetString("PASSWORD");
        var salt = r.GetString("SALT");
        var id = r.GetInt32("ID");
        var privateKey = r.GetString("PRIVATE_KEY");
        var publicKey = r.GetString("PUBLIC_KEY");
        var username = r.GetString("USERNAME");

        if(Encryption.HashPassword(password, salt) != pass) return Unauthorized();

        return Ok(new {privateKey= privateKey, publicKey = publicKey, id = id, username=username});

    }

    [HttpPost("")]
    public async Task<IActionResult> Register([FromForm]string email, [FromForm]string password, [FromForm]string username)
    {
        var rsa = Encryption.generateRSA();
        string salt = Encryption.generateSalt();
        string hashedPassword = Encryption.HashPassword(password, salt);
        string encryptedPrivateKey = Encryption.SymmetricEncrypt(password, rsa.private_key);

        var cmd = await DB.getCommand("INSERT INTO USER (TYPE, EMAIL, USERNAME, PASSWORD, SALT, PUBLIC_KEY, PRIVATE_KEY) VALUES(\"USER\", @email, @username, @password, @salt, @public_key, @private_key)");
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", hashedPassword);
        cmd.Parameters.AddWithValue("@salt", salt);
        cmd.Parameters.AddWithValue("@public_key", rsa.public_key);
        cmd.Parameters.AddWithValue("@private_key", encryptedPrivateKey);


        int id = await GetID(email, password);

        await DB.execSQL(cmd);
        return Ok(new {privateKey= rsa.private_key, publicKey = rsa.public_key, id = id});
    }


}
