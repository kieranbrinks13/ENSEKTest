namespace ENSEK_Technical_Test.Models
{
    public class BaseViewModel
    {
        public string MessageClass { get; set; } = "alert-info";
        public string Message { get; set; }
        public string MessageHtml { get 
            {
                return $"<div class='alert {MessageClass}'>{Message}</div>";
            } 
        }
    }
}
