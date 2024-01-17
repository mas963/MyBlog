﻿namespace MyBlog.Web.Settings;

public class MongoDbSettings
{
    public string ConnectionString;
    public string Database;

    //Configuration için kullanılacak
    #region Const Values

    public const string ConnectionStringValue = nameof(ConnectionString);
    public const string DatabaseValue = nameof(Database);

    #endregion
}
