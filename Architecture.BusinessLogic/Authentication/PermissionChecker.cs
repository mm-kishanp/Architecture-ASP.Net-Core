using System;
using System.Linq;

namespace Architecture.BusinessLogic.Authentication
{
    public static class PermissionChecker
    {
        public static bool ThisPermissionIsAllowed(this string packedPermissions, string permissionName)
        {
            var usersPermissions = packedPermissions.UnpackPermissionsFromString().ToArray();
            return usersPermissions.Contains(permissionName);
        }
    }
}
