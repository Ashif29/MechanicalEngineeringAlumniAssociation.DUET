namespace MEAlumniAssociationDUET.Web.Core
{
    public class AppSettings
    {
        public string TokenSecretKey { get; set; }
        public int TokenExpiresHours { get; set; }
        public string UserNewPassword { get; set; }
        public string UserResetPassword { get; set; }
        public bool IsMailServiceActive { get; set; }
        public string SubDomain { get; set; }
    }
}
