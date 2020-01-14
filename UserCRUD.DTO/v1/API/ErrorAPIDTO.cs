using System;
using System.Collections.Generic;
using System.Text;
using UserCRUD.DTO.v1.API.Enums;

namespace UserCRUD.DTO.v1.API
{
    public class ErrorAPIDTO
    {
        public ErrorAPIType ErrorAPIType { get; set; }
        public string Message { get; set; }
    }
}
