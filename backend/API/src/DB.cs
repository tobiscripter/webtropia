using System;
using MySqlConnector;

public class DB : IDisposable
{
    public MySqlConnection Connection { get; }

    public DB()
    {
        Connection = new MySqlConnection(Settings.dbconn);
    }

    public static async Task<MySqlConnection> getOpenConnection()
    {
        var c = new MySqlConnection(Settings.dbconn);
        
        await c.OpenAsync();
        return c;
    }

    public static async Task<MySqlCommand> getCommand()
    {
        return (await getOpenConnection()).CreateCommand();
    }

    public static async Task<MySqlCommand> getCommand(string sql)
    {
        var cmd = await getCommand();
        cmd.CommandText = sql;
        return cmd;
    }



    public static async Task<MySqlConnector.MySqlDataReader> readSQL(string sql)
    {
        return await readSQL(await getCommand(sql));
    }

    public static async Task<MySqlConnector.MySqlDataReader> readSQL(MySqlCommand cmd)
    {
        return await cmd.ExecuteReaderAsync();
    }

    public static async Task<int> execSQL(string sql)
    {
        return await execSQL(await getCommand(sql));
    }
    public static async Task<int> execSQL(MySqlCommand cmd)
    {
        return await cmd.ExecuteNonQueryAsync();
    }

    public static async Task<string> readJSONSQL(string sql)
    {
        return getJSON(await readSQL(sql));
    }

    public static async Task<string> readJSONSQL(MySqlCommand cmd)
    {
        return getJSON(await readSQL(cmd));
    }

    

    public void Dispose() => Connection.Dispose();

    private static string getJSON(MySqlConnector.MySqlDataReader r)
    {
        //return JsonConvert.SerializeObject(r.GetSchemaTable(), Formatting.Indented);
        var cols = r.GetColumnSchema();
        List<string[]> rows = new List<string[]>();

        while(r.Read())
        {
            string[] row = new string[cols.Count];

            for(int i = 0; i < cols.Count; i++)
                row[i] = r[i].ToString() ?? "";
            
            rows.Add(row);
        }

        if(rows.Count == 0) return "";
        if(rows.Count == 1)
        {
            if(cols.Count == 1) return printValue(rows[0][0], cols[0].DataTypeName);

            string res = "{";

            for(int i = 0; i < cols.Count; i++)
                res += "\"" + cols[i].ColumnName + "\":" + printValue(rows[0][i], cols[i].DataTypeName) + ",";

            return res.Substring(0, res.Length-1) + "}";
        }

        {
            string res = "[";

            for(int j = 0; j < rows.Count; j++)
            {
                if(cols.Count == 1){ res += printValue(rows[j][0], cols[0].DataTypeName) + ","; continue;} 
                res += "{";

                for(int i = 0; i < cols.Count; i++)
                    res += "\"" + cols[i].ColumnName + "\":" + printValue(rows[j][i], cols[i].DataTypeName) + ",";

                res = res.Substring(0, res.Length-1) + "},";
            }

            return res.Substring(0, res.Length-1) + "]";
        }
    }
    private static string printValue(string val, string? t)
    {
        if(t == "VARCHAR") return "\"" + val + "\"";
        return val;
    }
}
