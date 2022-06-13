namespace MultiplexKino.Services
{
    public interface IEmailLogSender
    {
        Task SendEmailLog(HttpContext context, Exception ex);
    }

    public class EmailLogSender : IEmailLogSender
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public EmailLogSender(IConfiguration configuration, IEmailSender emailSender)
        {
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public Task SendEmailLog(HttpContext context, Exception ex)
        {
            try
            {
                var adminEmail = _configuration["AdministratorEmail"];

                var newline = "<br/>";
                var errorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                var errormsg = ex.GetType().Name.ToString();
                var extype = ex.GetType().ToString();
                var exurl = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request);
                var errorLocation = ex.Message.ToString();

                var subject = "Exception occurred in Application " + exurl;
                var emailHead = "<b>Dear Team,</b>" + newline + "An exception occurred in a Application Url: " + exurl +
                                 newline + newline + "With following details:" + newline;
                var emailSing = "Thanks and Regards" + newline + "<b>Application Admin</b>";
                string errorMessage = emailHead + "<b>Log Written Date:</b>" + " " + DateTime.Now.ToString() +
                                      newline + "<b>Error Line No :</b>" + " " + errorlineNo + "\t\n" + " " +
                                      newline + "<b>Error Message:</b>" + " " + errormsg +
                                      newline + "<b>Exception Type:</b>" + " " + extype +
                                      newline + "<b>Error Details :</b>" + " " + errorLocation +
                                      newline + "<b>Error Page Url:</b>" + " " + exurl +
                                      newline + newline + emailSing;

                _emailSender.SendEmail(adminEmail, subject, errorMessage);
            }
            catch
            {

            }

            return Task.FromResult(0);
        }
    }
}
