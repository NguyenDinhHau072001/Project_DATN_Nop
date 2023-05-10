using ProjectDATN.Data.Entities;
using ProjectDATN.Web.Models.Payment;

namespace ProjectDATN.Web.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(Order model, HttpContext context);
        Order PaymentExecute(IQueryCollection collections);
    }
}
