using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UserCRUD.DTO.v1.API.Enums
{
    public enum ErrorAPIType
    {
        [Description("Unexpected error")]
        UnexpectedError = 0,

        #region Identity

        [Description("User not found")]
        UserNotFound = 1000,
        [Description("Error adding claims")]
        AddClaimError,
        [Description("Error deleting claims")]
        DeleteClaimError,
        [Description("Error creating user")]
        CreateUserError,
        [Description("Error updating user")]
        UpdateUserError,
        [Description("Error deleting user's password")]
        DeleteUserPasswordError,
        [Description("Error adding user's password")]
        AddUserPasswordError,
        [Description("Error login")]
        LoginError,

        #endregion

        #region IEntity

        [Description("Entity not found")]
        EntityNotFound = 2000,
        [Description("Error creating entity")]
        CreateEntityError = 2001,
        [Description("Error updating entity")]
        UpdateEntityError = 2001,
        [Description("Error deleting entity")]
        DeleteEntityError = 2001,

        #endregion
    }
}
