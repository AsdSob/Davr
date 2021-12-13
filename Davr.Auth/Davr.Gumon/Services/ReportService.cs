using System.Collections.Generic;
using System.Linq;
using Davr.Gumon.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Davr.Gumon.Services
{
    public interface IReportService
    {
        ExcelPackage GenerateTransactionQtyReport(List<TransactionSuspicious> transactions, List<Branch> branches,
            List<OperationCriteria> criterias);

        ExcelPackage GenerateTransactionListReport(List<TransactionSuspicious> transactions);
    }

    public class ReportService
    {
        public ExcelPackage GenerateTransactionListReport(List<TransactionSuspicious> transactions)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelPack = new ExcelPackage("ReportList.xlsx");
            var sheet = excelPack.Workbook.Worksheets[0];

            var row = 2;

            foreach (var transaction in transactions)
            {
                AddBorders(row, 1, row, 12, ref sheet);

                sheet.Cells[row, 1].Value = transaction.Branch.Code; //MFO =>******
                sheet.Cells[row, 2].Value = transaction.Id; //Number == Id
                sheet.Cells[row, 3].Value = transaction.EntryDate; //DateTime created
                sheet.Cells[row, 4].Value = transaction.ClientName; //Client name
                sheet.Cells[row, 5].Value = transaction.TransactionAmount; //Amount
                sheet.Cells[row, 6].Value = transaction.Currency.Code; //currency Code =>*****
                sheet.Cells[row, 7].Value = transaction.TransactionDate; //DateTime operation
                sheet.Cells[row, 8].Value = transaction.CounterpartyName; //Contractor name
                sheet.Cells[row, 9].Value = transaction.OperationCriteria.Code; //Operation Criteria =>*****
                sheet.Cells[row, 10].Value = transaction.Comment; //Comment
                sheet.Cells[row, 11].Value = transaction.TransactionDetails; //Operation Details
                sheet.Cells[row, 12].Value = transaction.TransactionStatus.Name; //Status =>****
                
                row += 1;
            }

            sheet.Columns[1].Style.Numberformat.Format = "0";
            sheet.Columns[2].Style.Numberformat.Format = "00000";
            sheet.Columns[3].Style.Numberformat.Format = "dd-mm-yyyy";
            sheet.Columns[4].Style.Numberformat.Format = "@";
            sheet.Columns[4].Style.WrapText = true;
            sheet.Columns[5].Style.Numberformat.Format = "0.00";
            sheet.Columns[6].Style.Numberformat.Format = "@";
            sheet.Columns[7].Style.Numberformat.Format = "dd-mm-yyyy";
            sheet.Columns[8].Style.Numberformat.Format = "@";
            sheet.Columns[9].Style.Numberformat.Format = "@";
            sheet.Columns[10].Style.Numberformat.Format = "@";
            sheet.Columns[11].Style.Numberformat.Format = "@";
            sheet.Columns[12].Style.Numberformat.Format = "@";

            return excelPack;
        }

        public ExcelPackage GenerateTransactionQtyReport(List<TransactionSuspicious> transactions, List<Branch> branches, List<OperationCriteria> criterias)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelPack = new ExcelPackage("ReportCount.xlsx");
            var sheet = excelPack.Workbook.Worksheets[0];

            int dataRow = 3;

            sheet.Cells[1, 2, 2, 28].Style.TextRotation = 90;
            sheet.Cells[1, 3].Style.TextRotation = 0;
            sheet.Cells[1, 20].Style.TextRotation = 0;
            sheet.Cells[1, 23].Style.TextRotation = 0;

            //loop branches list
            foreach (var branch in branches.OrderBy(x=> x.Code))
            {
                //Borders
                AddBorders(dataRow, 1, dataRow, 28, ref sheet);

                //Add branch code
                sheet.Cells[dataRow, 1].Value = $"{branch.Code} {branch.Name}";

                var dataColumn = 2;

                //loop Operation Criteries List OrderBy(x=> x.Code);
                foreach (var criteria in criterias.OrderBy(x=> x.Code))
                {
                    if (dataColumn != 2 && dataColumn != 19 && dataColumn != 22)
                    {
                        var qty = transactions.Count(x => x.BranchId == branch.Id && x.OperationCriteriaId == criteria.Id);
                        sheet.Cells[dataRow, dataColumn].Value = qty;
                    }
                    else
                    {
                        sheet.Cells[dataRow, dataColumn].Formula =
                            $"SUM({sheet.Cells[dataRow, dataColumn + 1].Address} : {sheet.Cells[dataRow, dataColumn + 2].Address})";
                    }
                    dataColumn++;
                }
                dataRow++;
            }
            return excelPack;
        }

        public void AddBorders(int startRow, int startCol, int endRow, int endCol, ref ExcelWorksheet sheet)
        {
            sheet.Cells[startRow, startCol, endRow, endCol].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[startRow, startCol, endRow, endCol].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sheet.Cells[startRow, startCol, endRow, endCol].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[startRow, startCol, endRow, endCol].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }
    }
}
