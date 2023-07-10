using HaberApp.Core.DTOs;
using System.Net;

namespace HaberApp.Core.Utils
{
    public class ResponseResult<T> where T : BaseDto
    {
        public T? Entity { get; set; } = null;
        public List<T>? Entities { get; set; } = null;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool Success { get; set; } = true;
        public List<string> ErrorList { get; set; } = new List<string>();
    }
}
