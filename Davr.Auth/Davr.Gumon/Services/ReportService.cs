using System.Collections.Generic;
using System.Linq;
using Davr.Gumon.Entities;
using IronXL;

namespace Davr.Gumon.Services
{
    public interface IReportService
    {
        WorkBook GenerateTransactionQtyReport(List<TransactionSuspicious> transactions, List<Branch> branches,
            List<OperationCriteria> criterias);

        WorkBook GenerateTransactionListReport(List<TransactionSuspicious> transactions);
    }

    public class ReportService
    {
        public WorkBook GenerateTransactionListReport(List<TransactionSuspicious> transactions)
        {
            WorkBook workbook = WorkBook.Load("ReportCount.xlsx");
            WorkSheet sheet = workbook.DefaultWorkSheet;




            return workbook;
        }

        public WorkBook GenerateTransactionQtyReport(List<TransactionSuspicious> transactions, List<Branch> branches, List<OperationCriteria> criterias)
        {
            WorkBook workbook = WorkBook.Load("ReportCount.xlsx");
            WorkSheet sheet = workbook.DefaultWorkSheet;

            int dataRow = 2;

            //loop branches list
            foreach (var branch in branches.OrderBy(x=> x.Code))
            {
                //Borders
                AddBorders(dataRow, ref sheet);

                //Add branch code
                sheet.SetCellValue(dataRow, 0, $"{branch.Code} {branch.Name}");

                var dataColumn = 1;

                //loop Operation Criteries List OrderBy(x=> x.Code);
                foreach(var criteria in criterias.OrderBy(x=> x.Code))
                {
                    if (dataColumn != 1 && dataColumn != 18 && dataColumn != 21)
                    {
                        //get Transaction qty Where(x=> x.BranchId == brnach.Id && x.OperationCriteriaId == operCriteria.Id).Count();
                        var qty = transactions.Count(x => x.BranchId == branch.Id && x.OperationCriteriaId == criteria.Id);
                        sheet.SetCellValue(dataRow, dataColumn, qty);
                    }
                    dataColumn++;
                }
                dataRow++;
            }

            workbook.EvaluateAll();

            return workbook;
        }

        public static void AddBorders(int row, ref WorkSheet sheet)
        {
            if (sheet == null) return;

            var start = $"A{row + 1}";
            var end = $"AB{row + 1}";

            sheet[$"{start}:{end}"].Style.LeftBorder.Type = IronXL.Styles.BorderType.Thin;
            sheet[$"{start}:{end}"].Style.TopBorder.Type = IronXL.Styles.BorderType.Thin;
            sheet[$"{start}:{end}"].Style.RightBorder.Type = IronXL.Styles.BorderType.Thin;
            sheet[$"{start}:{end}"].Style.BottomBorder.Type = IronXL.Styles.BorderType.Thin;
        }
    }
}
