namespace HaberApp.ServiceLayer.Exceptions
{
    public class CustomAppException : Exception
    {
        public List<string> errorList { get; set; }
        public CustomAppException()
        {

        }
        public CustomAppException(string message) : base(message)
        {

        }
        public CustomAppException(List<string> messageList)
        {
            this.errorList = messageList;
        }
    }
}
