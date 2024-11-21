

namespace IMSClassLibrary.Repos
{


    public class ResultObject<T>
    {
        public bool IsSuccessful { get; }
        public T Data { get; }
        public string Message { get; }

        private ResultObject(bool isSuccessful, T data, string Message)
        {
            IsSuccessful = isSuccessful;
            Data = data;
            this.Message = Message;
        }

        public static ResultObject<T> Success(string message, T data = default)
        {
            return new ResultObject<T>(true, data, message);
        }

        public static ResultObject<T> Failure(string errorMessage, T data= default)
        {
            return new ResultObject<T>(false, data, errorMessage);
        }
    }



}

