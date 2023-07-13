using System.Globalization;

namespace HaberApp.ServiceLayer.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException(string message, params object[] values) : base(string.Format(CultureInfo.CurrentCulture, message, values))
        {

        }
    }
}
