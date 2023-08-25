namespace Installer;

public class License
{
    public LicenseLang jaJP { get; set; } = null!;
    public LicenseLang enUS { get; set; } = null!;
}

public class LicenseLang
{
    public string ApplicationName { get; set; } = "";
    public string Document{get; set; } = "";
}
