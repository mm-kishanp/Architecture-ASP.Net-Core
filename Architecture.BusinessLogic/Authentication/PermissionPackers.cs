using System;
using System.Collections.Generic;
using System.Linq;

namespace Architecture.BusinessLogic.Authentication
{
    public static class PermissionPackers
    {
        public const char PackType = '+';

        public static string PackPermissionsIntoString(this IEnumerable<string> permissions)
        {
            return permissions.Aggregate((s, permission) => s + PackType + permission);
        }

        public static IEnumerable<string> UnpackPermissionValuesFromString(this string packedPermissions)
        {
            if (packedPermissions == null)
            {
                throw new ArgumentNullException(nameof(packedPermissions));
            }

            return packedPermissions.Split(PackType);
        }

        public static IEnumerable<string> UnpackPermissionsFromString(this string packedPermissions)
        {
            return packedPermissions.UnpackPermissionValuesFromString().Select(x => x);
        }

        //public static Permissions? FindPermissionViaName(this string permissionName)
        //{
        //    return Enum.TryParse(permissionName, out Permissions permission)
        //        ? (Permissions?)permission
        //        : null;
        //}
    }
}
