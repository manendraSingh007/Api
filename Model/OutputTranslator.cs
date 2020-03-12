using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Model
{
    public class OutputTranslator
    {
    }
    public static class UserTranslator
    {
        public static PartPrice TranslateAsPrice(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new PartPrice();
            //if (reader.IsColumnExists("invoiceNumber"))
            //    item.invoiceNumber = SqlHelper.GetNullableString(reader, "invoiceNumber");

            //if (reader.IsColumnExists("SalesForceID"))
            //    item.SalesForceID = SqlHelper.GetNullableString(reader, "SalesForceID");

            if (reader.IsColumnExists("bplitm"))
                item.materialNumber = SqlHelper.GetNullableString(reader, "bplitm");

            if (reader.IsColumnExists("price"))
                item.unitPrice = SqlHelper.GetNullableString(reader, "price");

            return item;
        }

        public static List<PartPrice> TranslateAsOutputList(this SqlDataReader reader)
        {
            var list = new List<PartPrice>();
            while (reader.Read())
            {
                list.Add(TranslateAsPrice(reader, true));
            }
            return list;
        }
        public static tbl_product TranslateAstest(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new tbl_product();
            if (reader.IsColumnExists("ProductID"))
                item.ProductID = SqlHelper.GetNullableInt32(reader, "ProductID");

            if (reader.IsColumnExists("Name"))
                item.Name = SqlHelper.GetNullableString(reader, "Name");

            if (reader.IsColumnExists("ProductNumber"))
                item.ProductNumber = SqlHelper.GetNullableString(reader, "ProductNumber");

            if (reader.IsColumnExists("Price"))
                item.Price = SqlHelper.GetNullableString(reader, "Price");
            if (reader.IsColumnExists("Quantity"))
                item.Price = SqlHelper.GetNullableString(reader, "Quantity");
            if (reader.IsColumnExists("Color"))
                item.Price = SqlHelper.GetNullableString(reader, "Color");

            return item;
        }
        public static List<tbl_product> TranslateAsOutputTest(this SqlDataReader reader)
        {
            var list = new List<tbl_product>();
            while (reader.Read())
            {
                list.Add(TranslateAstest(reader, true));
            }
            return list;
        }
    }
}
