using Macro;
using System.Collections.Generic;

namespace Atsam.Data
{
    public class Invoice
    {
        public int PK_InvoiceID { get; set; }

        public int FK_PartnerCode { get; set; }

        public InvoiceState FK_InvoiceStateCode { get; set; }

        public InvoiceType FK_InvoiceTypeCode { get; set; }

        public string InvoiceCode { get; set; }

        public string DeliverDate { get; set; }

        public int? InvoiceNumber { get; set; }

        public decimal VATValue { get; set; }

        public short Discount { get; set; }

        public string SolarDate { get; set; }

        public string SolarTime { get; set; }

        public int? FK_UserID { get; set; }

        public List<InvoiceLine> Lines { get; set; }

    }
    public class InvoiceLine
    {
        public int PK_InvoiceLineID { get; set; }

        public int FK_InvoiceID { get; set; }

        public int FK_ProductCode { get; set; }

        public short FK_DyeCode { get; set; }

        public long Price { get; set; }

    }
}
