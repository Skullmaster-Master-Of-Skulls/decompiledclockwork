using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Spire.Xls;
using TechnoPro.Common.DAO.MailMerging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.DAO.SpireDoc.Impl
{
	// Token: 0x02000003 RID: 3
	public class MailMergingListDAO : IMailMergingListDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002086 File Offset: 0x00000286
		public MailMergingListDAO()
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003FCC File Offset: 0x000021CC
		public MailMergingListDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00003FDE File Offset: 0x000021DE
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00003FE6 File Offset: 0x000021E6
		public OperationContext OpContext { get; set; }

		// Token: 0x06000015 RID: 21 RVA: 0x00003FF0 File Offset: 0x000021F0
		public IList<MailMergeCodeForExcel> ExtractCodesFromExcelTemplate(BinaryFile ExcelFile)
		{
			IList<MailMergeCodeForExcel> result;
			using (Stream stream = new MemoryStream(ExcelFile.ByteArray))
			{
				Workbook workbook = new Workbook();
				workbook.LoadFromStream(stream);
				bool flag = workbook.Worksheets.Count < 1;
				if (flag)
				{
					throw new InvalidParameterException("Common.DAO.SpireDoc.Impl.MailMergingListDAO.ExtractCodesFromExcelTemplate:Invalid worksheet count (no worksheets available).");
				}
				Worksheet worksheet = workbook.Worksheets[0];
				CellRange[] rows = worksheet.Rows;
				int num = rows.Count<CellRange>();
				bool flag2 = num > 100;
				if (flag2)
				{
					num = 100;
				}
				bool flag3 = num < 1;
				if (flag3)
				{
					result = new List<MailMergeCodeForExcel>();
				}
				else
				{
					IList<MailMergeCodeForExcel> list = this.ExtractLooseCodesFromFirstLine(worksheet, rows[0], 0);
					bool flag4 = list.Count < 1;
					if (flag4)
					{
						result = this.ExtractCodesFromFirstLineWithACode(worksheet, rows, 0, num);
					}
					else
					{
						IList<MailMergeCodeForExcel> second = this.ExtractCodesFromFirstLineWithACode(worksheet, rows, 1, num);
						result = list.Concat(second).ToList<MailMergeCodeForExcel>();
					}
				}
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000040E0 File Offset: 0x000022E0
		private IList<MailMergeCodeForExcel> ExtractLooseCodesFromFirstLine(Worksheet worksheet, CellRange firstRow, int firstRowIndex = 0)
		{
			List<MailMergeCodeForExcel> list = new List<MailMergeCodeForExcel>();
			CellRange[] cells = firstRow.Cells;
			int num = cells.Count<CellRange>();
			bool flag = num > 100;
			if (flag)
			{
				num = 100;
			}
			for (int i = 0; i < num; i++)
			{
				CellRange cellRange = cells[i];
				string text = cellRange.Value ?? "";
				bool flag2 = !text.StartsWith("#<") || !text.EndsWith(">#") || text.Length <= 4;
				if (!flag2)
				{
					string text2 = text.Substring(2, text.Length - 4);
					list.Add(new MailMergeCodeForExcel
					{
						Name = text2,
						OriginalCode = text2,
						RowIndex = firstRowIndex,
						IsLooseMailMergeCode = true
					});
				}
			}
			return list;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000041C0 File Offset: 0x000023C0
		private IList<MailMergeCodeForExcel> ExtractCodesFromFirstLineWithACode(Worksheet worksheet, CellRange[] rows, int start, int end)
		{
			List<MailMergeCodeForExcel> list = new List<MailMergeCodeForExcel>();
			for (int i = start; i < end; i++)
			{
				CellRange[] cells = rows[i].Cells;
				int num = cells.Count<CellRange>();
				bool flag = num > 100;
				if (flag)
				{
					num = 100;
				}
				for (int j = 0; j < num; j++)
				{
					CellRange cellRange = cells[j];
					string text = cellRange.Value ?? "";
					bool flag2 = !text.StartsWith("#<") || !text.EndsWith(">#") || text.Length <= 4;
					if (!flag2)
					{
						string text2 = text.Substring(2, text.Length - 4);
						list.Add(new MailMergeCodeForExcel
						{
							Name = text2,
							OriginalCode = text2,
							RowIndex = i,
							IsLooseMailMergeCode = false
						});
					}
				}
				bool flag3 = list.Count > 0;
				if (flag3)
				{
					break;
				}
			}
			return list;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000042D0 File Offset: 0x000024D0
		public BinaryFile MailMergeExcel(BinaryFile ExcelTemplate, IList<MailMergeContextWithCustomDictionary> ContextsWithDictionaries)
		{
			throw new NotImplementedException();
		}
	}
}
