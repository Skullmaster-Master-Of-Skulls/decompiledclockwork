using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Spire.DataExport.Common;
using Spire.DataExport.XLS;
using Spire.License;
using Spire.Xls;
using Spire.Xls.Core;

namespace TechnoPro.Common.DataFileIO.cs.Excel
{
	// Token: 0x02000007 RID: 7
	public static class ExcelUtility
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00003568 File Offset: 0x00001768
		static ExcelUtility()
		{
			Spire.License.LicenseProvider.SetLicenseKey("Nt7LAQBrbp9srInAFY1rTBrNwnmkYJkGXApnOKEuC/9MCHqIMm712u/jfvvnbpVIQiXmzdElmPz/sUFt/zJRG/cOb9fMnza0pvdIOGRFzQC1O+XHsUVxMF2mfKxawADpojVNdvk/OycnX+c7iLqgLfMRGsMQ7FYpqyFtKfxZXwqcFXOMnXGQSVf+bkXv/mfn4o3C7YhCx0IvOoBhgmVWItGwjGfeem0T39gv60LHF2Jw6Eo6Y3LDLASobZXkt0MxOfc330IkYtpeLEovbZWyf/9PUk7iLG6+ursM6zjMEjX6EIKKehcS/yMmPY76pwjcX43eOzWcncDGu6m7tSHb1ZRnewehTDOowiW0dJWsvwjZTn6JYP6JbeyBKGa3WdZ4BUOuzW8GtIgF2FMi7CfNu+Xq51yjzhNYwxAEuLSmLDfgnWmqEzWNWrLGUlMTqQMu07s9RCMp+o9nZNr9U3En2FlMKXZ92Tt9zSZcfi+n8u/Zk2QSfeBN6PEmYOuFO5b+ULp3ymNRpAYuYXWdG6XLka+xJiEdWcjc3tGsi0zhmKohKrL4WFB9uWs7jaHoyqn6IAXjJP+BhMXRaSDn3W8CISe/rsBzq5HS/OMb9MtV7CnUcorvlAJ50Rv6aSgDpfpNZB1z7WKeGNtxedg9Fp+HOJ+xrRLecdUTVAVlqqn5JPM2O8nd84k7TUUqs9t0ZjKyEG2UarSjtcjARWXVAv7kLx/MFaHgHFrLGmec0hDKwwIv2ejCiHQBqrj37J8HYvvc3ShTfkcBSnytUzBuzMVBD8G1Fx0rAwll3gEhRp+bPDXrB/7m/hm+x2ZjriUkbKCmijNJGMRh2KoM/2lwzvDIsZHQheZtYBruz803UtTBjxRlJvwqVAfCX09K/MFTqM7pwv9G9eJQAwpYMKk4vZD87Gvvcr01Fs9A0qk541MCC/D+uDyFv4XwxHNu68R110x1sYOcyYtZzRIOo6Ag+84DQOSDTm9j5pc29IlBAXV5P41z8OmyS2ZTrLe/jGN5ZGc3KgTqbqz9YP6z+KYe2rtmQpCRSs1eDcV2aCntcJTLEMjil/aA7A/FN4m6bQ3EBuV9NqcH3MmNQ/2Js3jq+I0GpzuKX7iGdi6ti0nD16ULLgVTY0BvNYf9+s9k/o1LMGuloZPPf3poxhgNSMUBdyEg+kjh8i+AMa42a6CRv0cpmrE4UM7Kc8n34BzCHNARd3qZLnWt8MlXV+dfSkaAVjdx1TwHvQLGmG9do372RIWHtLTra9rc22hJjvwnsI9LZz88SFtSQlrZfqNxn6Z1h+NvHBmIXDqAoBSvt6TorDgMBL64EYCbxo9fdyEj/+i2TMv8aq4UJUK6p29hWaD/Rmu/v6BT/EDhMzFJdFOHbFu4AxOhNX5jOjhCH1El8Es0JRpc4HMWfT7zDV/WOU3eH3RO5j6Iu2fD2Q3EpGJfINvVKZ8uAnI2z44tQIUgJmHrthd4cecyHBzS2IU=");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public static IList<string> GetWorksheetNames(string fileName)
		{
			Workbook workbook = new Workbook();
			workbook.LoadFromFile(fileName);
			return (from g in workbook.Worksheets
			select g.Name).ToList<string>();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003E10 File Offset: 0x00002010
		public static DataTable LoadExcelFromFile(string fileName, string worksheetName)
		{
			Workbook workbook = new Workbook();
			workbook.LoadFromFile(fileName);
			Worksheet worksheet = string.IsNullOrEmpty(worksheetName) ? workbook.Worksheets[0] : workbook.Worksheets[worksheetName];
			return ExcelUtility.TableFromWorksheet(worksheet);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003E5C File Offset: 0x0000205C
		public static DataTable LoadExcelFromFile(string fileName, int worksheetIndex)
		{
			Workbook workbook = new Workbook();
			workbook.LoadFromFile(fileName);
			Worksheet worksheet = (worksheetIndex < 1) ? workbook.Worksheets[0] : workbook.Worksheets[worksheetIndex];
			return ExcelUtility.TableFromWorksheet(worksheet);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003EA4 File Offset: 0x000020A4
		private static DataTable TableFromWorksheet(Worksheet worksheet)
		{
			return worksheet.ExportDataTable();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003EBC File Offset: 0x000020BC
		public static void ExportDataTableToExcel(string fileName, DataTable t, FileActionAfterExport fileActionAfterExport)
		{
			CellExport cellExport = new CellExport();
			WorkSheet item = new WorkSheet
			{
				DataSource = ExportSource.DataTable,
				DataTable = t,
				StartDataCol = 0
			};
			cellExport.Sheets.Add(item);
			if (fileActionAfterExport != FileActionAfterExport.OpenView)
			{
				if (fileActionAfterExport != FileActionAfterExport.Print)
				{
					cellExport.ActionAfterExport = ActionType.None;
				}
				else
				{
					cellExport.ActionAfterExport = ActionType.Print;
				}
			}
			else
			{
				cellExport.ActionAfterExport = ActionType.OpenView;
			}
			cellExport.SaveToFile(fileName);
		}
	}
}
