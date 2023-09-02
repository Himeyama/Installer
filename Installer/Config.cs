namespace Installer;

public class Config
{
    public LicenseLang jaJP { get; set; } = null!;
    public LicenseLang enUS { get; set; } = null!;
    public string source { get; set; } = "";
    public string applicationName { get; set; } = "";
    public string description { get; set; } = "";
}

public class LicenseLang
{
    public string ApplicationName { get; set; } = "";
    public string Document { get; set; } = "";
}
