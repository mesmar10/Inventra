namespace Inventra.Models.DomainModels
{
    public enum TransferStatus
    {
        Pending=0,   // تم إنشاء الطلب وفي انتظار التجهيز
        Shipped=1,   // الشحنة خرجت وفي الطريق للفرع الآخر
        Delivered=2, // وصلت وتم جردها وتأكيد استلامها
        Cancelled=3
    }
}
