namespace Mews.Fiscalizations.Hungary.Models
{
    public class ResponseResult<TResult, TCode>
        where TResult : class
        where TCode : struct
    {
        public ResponseResult(
            TResult successResult = null,
            ErrorResult<ResultErrorCode> generalErrorMessage = null,
            ErrorResult<TCode> operationErrorResult = null,
            string xmlRequestBody = null,
            string xmlResponseBody = null
        )
        {
            SuccessResult = successResult;
            GeneralErrorResult = generalErrorMessage;
            OperationalErrorResult = operationErrorResult;
            XmlRequestBody = xmlRequestBody;
            XmlResponseBody = xmlResponseBody;
        }

        public TResult SuccessResult { get; }
        public string XmlRequestBody { get; set; }
        public string XmlResponseBody { get; set; }

        public ErrorResult<ResultErrorCode> GeneralErrorResult { get; }

        public ErrorResult<TCode> OperationalErrorResult { get; }
    }
}
