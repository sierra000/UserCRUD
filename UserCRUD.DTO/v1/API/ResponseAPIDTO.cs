using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace UserCRUD.DTO.v1.API
{
    public class ResponseAPIDTO<T>
    {
        public ResponseAPIDTO()
        {
            Errors = new List<ErrorAPIDTO>();
        }

        public T Response { get; set; }
        public long Total { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Succeed { get; set; }
        public IEnumerable<ErrorAPIDTO> Errors { get; set; }
    }
}
