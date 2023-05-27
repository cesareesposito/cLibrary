using OfficeOpenXml;
using System.ComponentModel;

namespace cLibrary.helper
{
    public class cExcel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        /// EXEMPLE 
        /// Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
        /// dic["sheet_name"] = list;
        public byte[] MakeExcel(Dictionary<string, dynamic> dic)
        {
            using (ExcelPackage packge = new ExcelPackage())
            {
                var offsetRow = 1;
                foreach (var ws in dic)
                {
                    if (ws.Value.Count == 0)
                        continue;

                    var worksheet = packge.Workbook.Worksheets.Add(ws.Key);
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(ws.Value[0]);

                    // Header
                    var index = 1;
                    foreach (PropertyDescriptor prop in properties)
                    {
                        worksheet.Cells[offsetRow, index].Value = prop.Name;
                        worksheet.Cells[offsetRow, index].Style.Font.Bold = true;
                        index++;
                    }

                    // Dati
                    for (int i = 0; i < ws.Value.Count; i++)
                    {
                        var offsetColumns = 0;
                        for (int j = 0; j < properties.Count; j++)
                        {
                            var val = properties[j].GetValue(ws.Value[i]);
                            if (val is DateTime)
                                val = ((DateTime)val).ToString();
                            else if (val is decimal)
                                val = ((decimal)val).ToString("c");

                            worksheet.Cells[i + offsetRow + 1, offsetColumns + 1].Value = val;
                            offsetColumns++;
                        }
                    }
                    worksheet.Cells.AutoFitColumns();
                }
                return packge.GetAsByteArray();
            }
        }
    }
}