using System.ComponentModel;

namespace Domain.Enums;

public enum EmailFilePath
{
    [Description("FileSystem/EmailTemplates/BaseTemplate/EmailHeader.html")]
    EmailHeader,
    
    [Description("FileSystem/EmailTemplates/SendMailBooking.html")]
    SendMailBooking,
    
    [Description("FileSystem/EmailTemplates/SendMailRemind.html")]
    SendMailRemind,
    [Description("FileSystem/EmailTemplates/SendMailPay.html")]
    SendMailPay,
}