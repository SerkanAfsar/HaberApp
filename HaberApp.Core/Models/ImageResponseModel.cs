namespace HaberApp.Core.Models
{
    public class ImageResponseModel
    {
        public class Error
        {
            public int code { get; set; }
            public string message { get; set; }
        }
        public class MessageObject
        {
            public int code { get; set; }
            public string message { get; set; }
        }
        public class ResponseResult
        {
            public string id { get; set; }
            public string filename { get; set; }
            public DateTime uploaded { get; set; }
            public List<string> variants { get; set; }
        }
        public bool success { get; set; }
        public List<Error> errors { get; set; } = new List<Error>();
        public List<MessageObject> messages { get; set; } = new List<MessageObject>();
        public ResponseResult result { get; set; }
    }
}
