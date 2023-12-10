namespace TableBooking.Models
{
    /// <summary>
    /// Model representing the response of an API operation.
    /// </summary>
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }

        public ResponseModel(bool isError, string errorMessage, object data = null)
        {
            IsSuccess = isError;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public ResponseModel()
        {
        }
    }
}
