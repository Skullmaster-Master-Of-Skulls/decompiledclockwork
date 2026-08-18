using System;
using System.Collections.Generic;
using System.Data;
using Spire.License;
using Spire.Xls;

namespace ImportExportClassLibrary.MSOffice
{
	// Token: 0x0200001D RID: 29
	public class ExcelUtility
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00004A20 File Offset: 0x00003A20
		public static DataTable ImportFromExcelXml(string filename, int worksheetIndex = 0)
		{
			Spire.License.LicenseProvider.SetLicenseKey("Nt7LAQBrbp9srInAFY1rTBrNwnmkYJkGXApnOKEuC/9MCHqIMm712u/jfvvnbpVIQiXmzdElmPz/sUFt/zJRG/cOb9fMnza0pvdIOGRFzQC1O+XHsUVxMF2mfKxawADpojVNdvk/OycnX+c7iLqgLfMRGsMQ7FYpqyFtKfxZXwqcFXOMnXGQSVf+bkXv/mfn4o3C7YhCx0IvOoBhgmVWItGwjGfeem0T39gv60LHF2Jw6Eo6Y3LDLASobZXkt0MxOfc330IkYtpeLEovbZWyf/9PUk7iLG6+ursM6zjMEjX6EIKKehcS/yMmPY76pwjcX43eOzWcncDGu6m7tSHb1ZRnewehTDOowiW0dJWsvwjZTn6JYP6JbeyBKGa3WdZ4BUOuzW8GtIgF2FMi7CfNu+Xq51yjzhNYwxAEuLSmLDfgnWmqEzWNWrLGUlMTqQMu07s9RCMp+o9nZNr9U3En2FlMKXZ92Tt9zSZcfi+n8u/Zk2QSfeBN6PEmYOuFO5b+ULp3ymNRpAYuYXWdG6XLka+xJiEdWcjc3tGsi0zhmKohKrL4WFB9uWs7jaHoyqn6IAXjJP+BhMXRaSDn3W8CISe/rsBzq5HS/OMb9MtV7CnUcorvlAJ50Rv6aSgDpfpNZB1z7WKeGNtxedg9Fp+HOJ+xrRLecdUTVAVlqqn5JPM2O8nd84k7TUUqs9t0ZjKyEG2UarSjtcjARWXVAv7kLx/MFaHgHFrLGmec0hDKwwIv2ejCiHQBqrj37J8HYvvc3ShTfkcBSnytUzBuzMVBD8G1Fx0rAwll3gEhRp+bPDXrB/7m/hm+x2ZjriUkbKCmijNJGMRh2KoM/2lwzvDIsZHQheZtYBruz803UtTBjxRlJvwqVAfCX09K/MFTqM7pwv9G9eJQAwpYMKk4vZD87Gvvcr01Fs9A0qk541MCC/D+uDyFv4XwxHNu68R110x1sYOcyYtZzRIOo6Ag+84DQOSDTm9j5pc29IlBAXV5P41z8OmyS2ZTrLe/jGN5ZGc3KgTqbqz9YP6z+KYe2rtmQpCRSs1eDcV2aCntcJTLEMjil/aA7A/FN4m6bQ3EBuV9NqcH3MmNQ/2Js3jq+I0GpzuKX7iGdi6ti0nD16ULLgVTY0BvNYf9+s9k/o1LMGuloZPPf3poxhgNSMUBdyEg+kjh8i+AMa42a6CRv0cpmrE4UM7Kc8n34BzCHNARd3qZLnWt8MlXV+dfSkaAVjdx1TwHvQLGmG9do372RIWHtLTra9rc22hJjvwnsI9LZz88SFtSQlrZfqNxn6Z1h+NvHBmIXDqAoBSvt6TorDgMBL64EYCbxo9fdyEj/+i2TMv8aq4UJUK6p29hWaD/Rmu/v6BT/EDhMzFJdFOHbFu4AxOhNX5jOjhCH1El8Es0JRpc4HMWfT7zDV/WOU3eH3RO5j6Iu2fD2Q3EpGJfINvVKZ8uAnI2z44tQIUgJmHrthd4cecyHBzS2IU=");
			Workbook workbook = new Workbook();
			workbook.LoadFromXml(filename);
			Worksheet worksheet = workbook.Worksheets[worksheetIndex];
			return worksheet.ExportDataTable();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004A58 File Offset: 0x00003A58
		public static DataTable ImportFromExcel(string filename, int worksheetIndex = 0)
		{
			Spire.License.LicenseProvider.SetLicenseKey("Nt7LAQBrbp9srInAFY1rTBrNwnmkYJkGXApnOKEuC/9MCHqIMm712u/jfvvnbpVIQiXmzdElmPz/sUFt/zJRG/cOb9fMnza0pvdIOGRFzQC1O+XHsUVxMF2mfKxawADpojVNdvk/OycnX+c7iLqgLfMRGsMQ7FYpqyFtKfxZXwqcFXOMnXGQSVf+bkXv/mfn4o3C7YhCx0IvOoBhgmVWItGwjGfeem0T39gv60LHF2Jw6Eo6Y3LDLASobZXkt0MxOfc330IkYtpeLEovbZWyf/9PUk7iLG6+ursM6zjMEjX6EIKKehcS/yMmPY76pwjcX43eOzWcncDGu6m7tSHb1ZRnewehTDOowiW0dJWsvwjZTn6JYP6JbeyBKGa3WdZ4BUOuzW8GtIgF2FMi7CfNu+Xq51yjzhNYwxAEuLSmLDfgnWmqEzWNWrLGUlMTqQMu07s9RCMp+o9nZNr9U3En2FlMKXZ92Tt9zSZcfi+n8u/Zk2QSfeBN6PEmYOuFO5b+ULp3ymNRpAYuYXWdG6XLka+xJiEdWcjc3tGsi0zhmKohKrL4WFB9uWs7jaHoyqn6IAXjJP+BhMXRaSDn3W8CISe/rsBzq5HS/OMb9MtV7CnUcorvlAJ50Rv6aSgDpfpNZB1z7WKeGNtxedg9Fp+HOJ+xrRLecdUTVAVlqqn5JPM2O8nd84k7TUUqs9t0ZjKyEG2UarSjtcjARWXVAv7kLx/MFaHgHFrLGmec0hDKwwIv2ejCiHQBqrj37J8HYvvc3ShTfkcBSnytUzBuzMVBD8G1Fx0rAwll3gEhRp+bPDXrB/7m/hm+x2ZjriUkbKCmijNJGMRh2KoM/2lwzvDIsZHQheZtYBruz803UtTBjxRlJvwqVAfCX09K/MFTqM7pwv9G9eJQAwpYMKk4vZD87Gvvcr01Fs9A0qk541MCC/D+uDyFv4XwxHNu68R110x1sYOcyYtZzRIOo6Ag+84DQOSDTm9j5pc29IlBAXV5P41z8OmyS2ZTrLe/jGN5ZGc3KgTqbqz9YP6z+KYe2rtmQpCRSs1eDcV2aCntcJTLEMjil/aA7A/FN4m6bQ3EBuV9NqcH3MmNQ/2Js3jq+I0GpzuKX7iGdi6ti0nD16ULLgVTY0BvNYf9+s9k/o1LMGuloZPPf3poxhgNSMUBdyEg+kjh8i+AMa42a6CRv0cpmrE4UM7Kc8n34BzCHNARd3qZLnWt8MlXV+dfSkaAVjdx1TwHvQLGmG9do372RIWHtLTra9rc22hJjvwnsI9LZz88SFtSQlrZfqNxn6Z1h+NvHBmIXDqAoBSvt6TorDgMBL64EYCbxo9fdyEj/+i2TMv8aq4UJUK6p29hWaD/Rmu/v6BT/EDhMzFJdFOHbFu4AxOhNX5jOjhCH1El8Es0JRpc4HMWfT7zDV/WOU3eH3RO5j6Iu2fD2Q3EpGJfINvVKZ8uAnI2z44tQIUgJmHrthd4cecyHBzS2IU=");
			Workbook workbook = new Workbook();
			workbook.LoadFromFile(filename);
			Worksheet worksheet = workbook.Worksheets[worksheetIndex];
			return worksheet.ExportDataTable();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004A90 File Offset: 0x00003A90
		public static DataTable ImportFromExcel2010(string filename, int worksheetIndex = 0)
		{
			Spire.License.LicenseProvider.SetLicenseKey("Nt7LAQBrbp9srInAFY1rTBrNwnmkYJkGXApnOKEuC/9MCHqIMm712u/jfvvnbpVIQiXmzdElmPz/sUFt/zJRG/cOb9fMnza0pvdIOGRFzQC1O+XHsUVxMF2mfKxawADpojVNdvk/OycnX+c7iLqgLfMRGsMQ7FYpqyFtKfxZXwqcFXOMnXGQSVf+bkXv/mfn4o3C7YhCx0IvOoBhgmVWItGwjGfeem0T39gv60LHF2Jw6Eo6Y3LDLASobZXkt0MxOfc330IkYtpeLEovbZWyf/9PUk7iLG6+ursM6zjMEjX6EIKKehcS/yMmPY76pwjcX43eOzWcncDGu6m7tSHb1ZRnewehTDOowiW0dJWsvwjZTn6JYP6JbeyBKGa3WdZ4BUOuzW8GtIgF2FMi7CfNu+Xq51yjzhNYwxAEuLSmLDfgnWmqEzWNWrLGUlMTqQMu07s9RCMp+o9nZNr9U3En2FlMKXZ92Tt9zSZcfi+n8u/Zk2QSfeBN6PEmYOuFO5b+ULp3ymNRpAYuYXWdG6XLka+xJiEdWcjc3tGsi0zhmKohKrL4WFB9uWs7jaHoyqn6IAXjJP+BhMXRaSDn3W8CISe/rsBzq5HS/OMb9MtV7CnUcorvlAJ50Rv6aSgDpfpNZB1z7WKeGNtxedg9Fp+HOJ+xrRLecdUTVAVlqqn5JPM2O8nd84k7TUUqs9t0ZjKyEG2UarSjtcjARWXVAv7kLx/MFaHgHFrLGmec0hDKwwIv2ejCiHQBqrj37J8HYvvc3ShTfkcBSnytUzBuzMVBD8G1Fx0rAwll3gEhRp+bPDXrB/7m/hm+x2ZjriUkbKCmijNJGMRh2KoM/2lwzvDIsZHQheZtYBruz803UtTBjxRlJvwqVAfCX09K/MFTqM7pwv9G9eJQAwpYMKk4vZD87Gvvcr01Fs9A0qk541MCC/D+uDyFv4XwxHNu68R110x1sYOcyYtZzRIOo6Ag+84DQOSDTm9j5pc29IlBAXV5P41z8OmyS2ZTrLe/jGN5ZGc3KgTqbqz9YP6z+KYe2rtmQpCRSs1eDcV2aCntcJTLEMjil/aA7A/FN4m6bQ3EBuV9NqcH3MmNQ/2Js3jq+I0GpzuKX7iGdi6ti0nD16ULLgVTY0BvNYf9+s9k/o1LMGuloZPPf3poxhgNSMUBdyEg+kjh8i+AMa42a6CRv0cpmrE4UM7Kc8n34BzCHNARd3qZLnWt8MlXV+dfSkaAVjdx1TwHvQLGmG9do372RIWHtLTra9rc22hJjvwnsI9LZz88SFtSQlrZfqNxn6Z1h+NvHBmIXDqAoBSvt6TorDgMBL64EYCbxo9fdyEj/+i2TMv8aq4UJUK6p29hWaD/Rmu/v6BT/EDhMzFJdFOHbFu4AxOhNX5jOjhCH1El8Es0JRpc4HMWfT7zDV/WOU3eH3RO5j6Iu2fD2Q3EpGJfINvVKZ8uAnI2z44tQIUgJmHrthd4cecyHBzS2IU=");
			Workbook workbook = new Workbook();
			workbook.LoadFromFile(filename, ExcelVersion.Version2010);
			Worksheet worksheet = workbook.Worksheets[worksheetIndex];
			return worksheet.ExportDataTable();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004AC8 File Offset: 0x00003AC8
		public static bool ExportToExcel<T>(List<T> objects, string filename)
		{
			Spire.License.LicenseProvider.SetLicenseKey("Nt7LAQBrbp9srInAFY1rTBrNwnmkYJkGXApnOKEuC/9MCHqIMm712u/jfvvnbpVIQiXmzdElmPz/sUFt/zJRG/cOb9fMnza0pvdIOGRFzQC1O+XHsUVxMF2mfKxawADpojVNdvk/OycnX+c7iLqgLfMRGsMQ7FYpqyFtKfxZXwqcFXOMnXGQSVf+bkXv/mfn4o3C7YhCx0IvOoBhgmVWItGwjGfeem0T39gv60LHF2Jw6Eo6Y3LDLASobZXkt0MxOfc330IkYtpeLEovbZWyf/9PUk7iLG6+ursM6zjMEjX6EIKKehcS/yMmPY76pwjcX43eOzWcncDGu6m7tSHb1ZRnewehTDOowiW0dJWsvwjZTn6JYP6JbeyBKGa3WdZ4BUOuzW8GtIgF2FMi7CfNu+Xq51yjzhNYwxAEuLSmLDfgnWmqEzWNWrLGUlMTqQMu07s9RCMp+o9nZNr9U3En2FlMKXZ92Tt9zSZcfi+n8u/Zk2QSfeBN6PEmYOuFO5b+ULp3ymNRpAYuYXWdG6XLka+xJiEdWcjc3tGsi0zhmKohKrL4WFB9uWs7jaHoyqn6IAXjJP+BhMXRaSDn3W8CISe/rsBzq5HS/OMb9MtV7CnUcorvlAJ50Rv6aSgDpfpNZB1z7WKeGNtxedg9Fp+HOJ+xrRLecdUTVAVlqqn5JPM2O8nd84k7TUUqs9t0ZjKyEG2UarSjtcjARWXVAv7kLx/MFaHgHFrLGmec0hDKwwIv2ejCiHQBqrj37J8HYvvc3ShTfkcBSnytUzBuzMVBD8G1Fx0rAwll3gEhRp+bPDXrB/7m/hm+x2ZjriUkbKCmijNJGMRh2KoM/2lwzvDIsZHQheZtYBruz803UtTBjxRlJvwqVAfCX09K/MFTqM7pwv9G9eJQAwpYMKk4vZD87Gvvcr01Fs9A0qk541MCC/D+uDyFv4XwxHNu68R110x1sYOcyYtZzRIOo6Ag+84DQOSDTm9j5pc29IlBAXV5P41z8OmyS2ZTrLe/jGN5ZGc3KgTqbqz9YP6z+KYe2rtmQpCRSs1eDcV2aCntcJTLEMjil/aA7A/FN4m6bQ3EBuV9NqcH3MmNQ/2Js3jq+I0GpzuKX7iGdi6ti0nD16ULLgVTY0BvNYf9+s9k/o1LMGuloZPPf3poxhgNSMUBdyEg+kjh8i+AMa42a6CRv0cpmrE4UM7Kc8n34BzCHNARd3qZLnWt8MlXV+dfSkaAVjdx1TwHvQLGmG9do372RIWHtLTra9rc22hJjvwnsI9LZz88SFtSQlrZfqNxn6Z1h+NvHBmIXDqAoBSvt6TorDgMBL64EYCbxo9fdyEj/+i2TMv8aq4UJUK6p29hWaD/Rmu/v6BT/EDhMzFJdFOHbFu4AxOhNX5jOjhCH1El8Es0JRpc4HMWfT7zDV/WOU3eH3RO5j6Iu2fD2Q3EpGJfINvVKZ8uAnI2z44tQIUgJmHrthd4cecyHBzS2IU=");
			Workbook workbook = new Workbook();
			Worksheet worksheet = workbook.Worksheets[0];
			worksheet.InsertArray<T>(objects.ToArray(), 0, 0, false);
			CellStyle cellStyle = workbook.Styles.Add("oddStyle");
			cellStyle.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;
			cellStyle.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
			cellStyle.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
			cellStyle.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;
			cellStyle.KnownColor = ExcelColors.LightGreen1;
			CellStyle cellStyle2 = workbook.Styles.Add("evenStyle");
			cellStyle2.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;
			cellStyle2.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
			cellStyle2.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
			cellStyle2.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;
			cellStyle2.KnownColor = ExcelColors.LightTurquoise;
			foreach (CellRange cellRange in worksheet.AllocatedRange.Rows)
			{
				if (cellRange.Row % 2 == 0)
				{
					cellRange.CellStyleName = cellStyle2.Name;
				}
				else
				{
					cellRange.CellStyleName = cellStyle.Name;
				}
			}
			CellStyle style = worksheet.Rows[0].Style;
			style.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;
			style.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
			style.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
			style.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;
			style.VerticalAlignment = VerticalAlignType.Center;
			style.KnownColor = ExcelColors.Green;
			style.Font.KnownColor = ExcelColors.White;
			style.Font.IsBold = true;
			workbook.SaveToFile(filename);
			return true;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004CA8 File Offset: 0x00003CA8
		public static bool ExportToExcel(DataView dv, string filename)
		{
			Spire.License.LicenseProvider.SetLicenseKey("Nt7LAQBrbp9srInAFY1rTBrNwnmkYJkGXApnOKEuC/9MCHqIMm712u/jfvvnbpVIQiXmzdElmPz/sUFt/zJRG/cOb9fMnza0pvdIOGRFzQC1O+XHsUVxMF2mfKxawADpojVNdvk/OycnX+c7iLqgLfMRGsMQ7FYpqyFtKfxZXwqcFXOMnXGQSVf+bkXv/mfn4o3C7YhCx0IvOoBhgmVWItGwjGfeem0T39gv60LHF2Jw6Eo6Y3LDLASobZXkt0MxOfc330IkYtpeLEovbZWyf/9PUk7iLG6+ursM6zjMEjX6EIKKehcS/yMmPY76pwjcX43eOzWcncDGu6m7tSHb1ZRnewehTDOowiW0dJWsvwjZTn6JYP6JbeyBKGa3WdZ4BUOuzW8GtIgF2FMi7CfNu+Xq51yjzhNYwxAEuLSmLDfgnWmqEzWNWrLGUlMTqQMu07s9RCMp+o9nZNr9U3En2FlMKXZ92Tt9zSZcfi+n8u/Zk2QSfeBN6PEmYOuFO5b+ULp3ymNRpAYuYXWdG6XLka+xJiEdWcjc3tGsi0zhmKohKrL4WFB9uWs7jaHoyqn6IAXjJP+BhMXRaSDn3W8CISe/rsBzq5HS/OMb9MtV7CnUcorvlAJ50Rv6aSgDpfpNZB1z7WKeGNtxedg9Fp+HOJ+xrRLecdUTVAVlqqn5JPM2O8nd84k7TUUqs9t0ZjKyEG2UarSjtcjARWXVAv7kLx/MFaHgHFrLGmec0hDKwwIv2ejCiHQBqrj37J8HYvvc3ShTfkcBSnytUzBuzMVBD8G1Fx0rAwll3gEhRp+bPDXrB/7m/hm+x2ZjriUkbKCmijNJGMRh2KoM/2lwzvDIsZHQheZtYBruz803UtTBjxRlJvwqVAfCX09K/MFTqM7pwv9G9eJQAwpYMKk4vZD87Gvvcr01Fs9A0qk541MCC/D+uDyFv4XwxHNu68R110x1sYOcyYtZzRIOo6Ag+84DQOSDTm9j5pc29IlBAXV5P41z8OmyS2ZTrLe/jGN5ZGc3KgTqbqz9YP6z+KYe2rtmQpCRSs1eDcV2aCntcJTLEMjil/aA7A/FN4m6bQ3EBuV9NqcH3MmNQ/2Js3jq+I0GpzuKX7iGdi6ti0nD16ULLgVTY0BvNYf9+s9k/o1LMGuloZPPf3poxhgNSMUBdyEg+kjh8i+AMa42a6CRv0cpmrE4UM7Kc8n34BzCHNARd3qZLnWt8MlXV+dfSkaAVjdx1TwHvQLGmG9do372RIWHtLTra9rc22hJjvwnsI9LZz88SFtSQlrZfqNxn6Z1h+NvHBmIXDqAoBSvt6TorDgMBL64EYCbxo9fdyEj/+i2TMv8aq4UJUK6p29hWaD/Rmu/v6BT/EDhMzFJdFOHbFu4AxOhNX5jOjhCH1El8Es0JRpc4HMWfT7zDV/WOU3eH3RO5j6Iu2fD2Q3EpGJfINvVKZ8uAnI2z44tQIUgJmHrthd4cecyHBzS2IU=");
			Workbook workbook = new Workbook();
			Worksheet worksheet = workbook.Worksheets[0];
			worksheet.InsertDataView(dv, true, 0, 0);
			CellStyle cellStyle = workbook.Styles.Add("oddStyle");
			cellStyle.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;
			cellStyle.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
			cellStyle.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
			cellStyle.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;
			cellStyle.KnownColor = ExcelColors.LightGreen1;
			CellStyle cellStyle2 = workbook.Styles.Add("evenStyle");
			cellStyle2.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;
			cellStyle2.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
			cellStyle2.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
			cellStyle2.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;
			cellStyle2.KnownColor = ExcelColors.LightTurquoise;
			foreach (CellRange cellRange in worksheet.AllocatedRange.Rows)
			{
				if (cellRange.Row % 2 == 0)
				{
					cellRange.CellStyleName = cellStyle2.Name;
				}
				else
				{
					cellRange.CellStyleName = cellStyle.Name;
				}
			}
			CellStyle style = worksheet.Rows[0].Style;
			style.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;
			style.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
			style.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
			style.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;
			style.VerticalAlignment = VerticalAlignType.Center;
			style.KnownColor = ExcelColors.Green;
			style.Font.KnownColor = ExcelColors.White;
			style.Font.IsBold = true;
			workbook.SaveToFile(filename);
			return true;
		}
	}
}
