
namespace MEAlumniAssociationDUET.Common.Values
{
    public static class Permissions
    {
        public const string SuperAdmin = "Permissions.SuperAdmin";

        public static class Users
        {
            public const string ListView = "Permissions.USR.LV";
            // public const string DetailsView = "Permissions.USR.DV";
            public const string AddEdit = "Permissions.USR.C";
            // public const string Edit = "Permissions.USR.E";
            public const string Delete = "Permissions.USR.D";
            public const string ResetPassword = "Permissions.USR.RP";
            public const string ActiveInactive = "Permissions.USR.AI";
        }

        public static class UserRoles
        {
            public const string ListView = "Permissions.UR.LV";
            // public const string DetailsView = "Permissions.UR.DV";
            public const string AddEdit = "Permissions.UR.C";
            // public const string Edit = "Permissions.UR.E";
            public const string Delete = "Permissions.UR.D";
            public const string ActiveInactive = "Permissions.UR.AI";
        }
        public static class Settings
        {
            public const string GeneralConfiguration = "Permissions.Conf.General";
        }
        public static class Registration
        {
            public const string ListView = "Permissions.Reg.LV";
            // public const string DetailsView = "Permissions.Reg.DV";
            public const string AddEdit = "Permissions.Reg.C";
            // public const string Edit = "Permissions.Reg.E";
            public const string Delete = "Permissions.Reg.D";
        }

    }
}