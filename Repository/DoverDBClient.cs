using Api.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class DoverDBClient
    {
        public List<PartPrice> GetPriceOutput(PartList model,string connString)
        {
            SqlParameter[] param = {
                new SqlParameter("@invoice_number",model.invoiceNumber),
                new SqlParameter("@part",model.materialNumber),
                new SqlParameter("@SalesForceID",model.dealerNumber),
                new SqlParameter("@BUName",model.buName),

            };
            return SqlHelper.ExtecuteProcedureReturnData<List<PartPrice>>(connString,
                "PartsCentral_Pricing", r => r.TranslateAsOutputList(),param);
        }
        public List<tbl_product> GetPriceOutlist(tbl_product model, string connString)
        {
            SqlParameter[] param = {
             new SqlParameter("@product_ID",680)
            };
           
            return SqlHelper.ExtecuteProcedureReturnData<List<tbl_product>>(connString,
                "GetProductByID", r => r.TranslateAsOutputTest(),param);
        }

    }
}
