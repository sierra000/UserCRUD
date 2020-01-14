using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UserCRUD.DTO.v1.API.Enums
{
    public enum IdentityErrorType
    {
        [Description("User not found")]
        UserNotFound = 0,
        [Description("Error adding claims")]
        AddClaimError,
        [Description("Error deleting claims")]
        DeleteClaimError,
        [Description("Error adding user")]
        AddUserError,
        [Description("Error updating user")]
        UpdateUserError,
        [Description("Error deleting user's password")]
        DeleteUserPasswordError,
        [Description("Error adding user's password")]
        AddUserPasswordError
    }
}
