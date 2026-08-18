using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Spire.License;
using Spire.Xls;

namespace TechnoPro.Common.DataFileIO.cs.Excel
{
	// Token: 0x02000006 RID: 6
	public static class ExcelMergeUtility
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00003568 File Offset: 0x00001768
		static ExcelMergeUtility()
		{
			Spire.License.LicenseProvider.SetLicenseKey("Nt7LAQBrbp9srInAFY1rTBrNwnmkYJkGXApnOKEuC/9MCHqIMm712u/jfvvnbpVIQiXmzdElmPz/sUFt/zJRG/cOb9fMnza0pvdIOGRFzQC1O+XHsUVxMF2mfKxawADpojVNdvk/OycnX+c7iLqgLfMRGsMQ7FYpqyFtKfxZXwqcFXOMnXGQSVf+bkXv/mfn4o3C7YhCx0IvOoBhgmVWItGwjGfeem0T39gv60LHF2Jw6Eo6Y3LDLASobZXkt0MxOfc330IkYtpeLEovbZWyf/9PUk7iLG6+ursM6zjMEjX6EIKKehcS/yMmPY76pwjcX43eOzWcncDGu6m7tSHb1ZRnewehTDOowiW0dJWsvwjZTn6JYP6JbeyBKGa3WdZ4BUOuzW8GtIgF2FMi7CfNu+Xq51yjzhNYwxAEuLSmLDfgnWmqEzWNWrLGUlMTqQMu07s9RCMp+o9nZNr9U3En2FlMKXZ92Tt9zSZcfi+n8u/Zk2QSfeBN6PEmYOuFO5b+ULp3ymNRpAYuYXWdG6XLka+xJiEdWcjc3tGsi0zhmKohKrL4WFB9uWs7jaHoyqn6IAXjJP+BhMXRaSDn3W8CISe/rsBzq5HS/OMb9MtV7CnUcorvlAJ50Rv6aSgDpfpNZB1z7WKeGNtxedg9Fp+HOJ+xrRLecdUTVAVlqqn5JPM2O8nd84k7TUUqs9t0ZjKyEG2UarSjtcjARWXVAv7kLx/MFaHgHFrLGmec0hDKwwIv2ejCiHQBqrj37J8HYvvc3ShTfkcBSnytUzBuzMVBD8G1Fx0rAwll3gEhRp+bPDXrB/7m/hm+x2ZjriUkbKCmijNJGMRh2KoM/2lwzvDIsZHQheZtYBruz803UtTBjxRlJvwqVAfCX09K/MFTqM7pwv9G9eJQAwpYMKk4vZD87Gvvcr01Fs9A0qk541MCC/D+uDyFv4XwxHNu68R110x1sYOcyYtZzRIOo6Ag+84DQOSDTm9j5pc29IlBAXV5P41z8OmyS2ZTrLe/jGN5ZGc3KgTqbqz9YP6z+KYe2rtmQpCRSs1eDcV2aCntcJTLEMjil/aA7A/FN4m6bQ3EBuV9NqcH3MmNQ/2Js3jq+I0GpzuKX7iGdi6ti0nD16ULLgVTY0BvNYf9+s9k/o1LMGuloZPPf3poxhgNSMUBdyEg+kjh8i+AMa42a6CRv0cpmrE4UM7Kc8n34BzCHNARd3qZLnWt8MlXV+dfSkaAVjdx1TwHvQLGmG9do372RIWHtLTra9rc22hJjvwnsI9LZz88SFtSQlrZfqNxn6Z1h+NvHBmIXDqAoBSvt6TorDgMBL64EYCbxo9fdyEj/+i2TMv8aq4UJUK6p29hWaD/Rmu/v6BT/EDhMzFJdFOHbFu4AxOhNX5jOjhCH1El8Es0JRpc4HMWfT7zDV/WOU3eH3RO5j6Iu2fD2Q3EpGJfINvVKZ8uAnI2z44tQIUgJmHrthd4cecyHBzS2IU=");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00003578 File Offset: 0x00001778
		public static IList<string> ExtractMergeCodesWithoutTags(byte[] excelTemplateFile, out int mergingRowIndex, out IList<string> standAloneMergeCodesNoTags)
		{
			Workbook workbook = ExcelMergeUtility.LoadWorkbookFromByteArray(excelTemplateFile);
			Worksheet sheet = workbook.Worksheets[0];
			return ExcelMergeUtility.ExtractMergeCodesWithoutTags(sheet, out mergingRowIndex, out standAloneMergeCodesNoTags);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000035A8 File Offset: 0x000017A8
		private static IList<string> ExtractMergeCodesWithoutTags(Worksheet sheet, out int mergingRowIndex, out IList<string> standAloneMergeCodesNoTags)
		{
			int lastRow = sheet.LastRow;
			int lastColumn = sheet.LastColumn;
			int num = 0;
			Dictionary<int, IList<string>> dictionary = new Dictionary<int, IList<string>>();
			for (int i = 1; i <= lastRow; i++)
			{
				bool flag = false;
				List<string> list = new List<string>();
				int j = 1;
				while (j <= lastColumn)
				{
					try
					{
						CellRange cellRange = sheet.Range[i, j];
						string text = cellRange.Value ?? "";
						bool flag2 = !text.Contains("#<") || !text.Contains(">#");
						if (!flag2)
						{
							IList<string> list2 = ExcelMergeUtility.ExtractMailMergeCodesFromStringWithoutTags(text);
							foreach (string item in list2)
							{
								bool flag3 = !list.Contains(item);
								if (flag3)
								{
									list.Add(item);
								}
							}
							flag = true;
						}
					}
					catch
					{
					}
					IL_DD:
					j++;
					continue;
					goto IL_DD;
				}
				bool flag4 = list.Count > 0;
				if (flag4)
				{
					dictionary.Add(i, list);
				}
				bool flag5 = !flag;
				if (!flag5)
				{
					num = i;
				}
			}
			IList<string> list4;
			if (num <= 0 || !dictionary.ContainsKey(num))
			{
				IList<string> list3 = new List<string>();
				list4 = list3;
			}
			else
			{
				list4 = dictionary[num];
			}
			IList<string> result = list4;
			List<string> list5 = new List<string>();
			foreach (KeyValuePair<int, IList<string>> keyValuePair in dictionary)
			{
				bool flag6 = keyValuePair.Key == num;
				if (!flag6)
				{
					IList<string> value = keyValuePair.Value;
					foreach (string item2 in value)
					{
						bool flag7 = !list5.Contains(item2);
						if (flag7)
						{
							list5.Add(item2);
						}
					}
				}
			}
			try
			{
				sheet.Workbook.Dispose();
			}
			catch
			{
			}
			standAloneMergeCodesNoTags = list5;
			mergingRowIndex = num;
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003810 File Offset: 0x00001A10
		private static IList<string> ExtractMailMergeCodesFromStringWithoutTags(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			IList<string> result;
			if (flag)
			{
				result = new List<string>();
			}
			else
			{
				Regex regex = new Regex("#<[^#>]*>#");
				MatchCollection matchCollection = regex.Matches(s);
				List<string> list = new List<string>();
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					string item = match.Value.Substring(2, match.Value.Length - 4).Trim().ToLower();
					bool flag2 = !list.Contains(item);
					if (flag2)
					{
						list.Add(item);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000038E4 File Offset: 0x00001AE4
		private static void CopyRange(Worksheet sheet, CellRange sourceRange, CellRange destRange, bool copyStyle = true)
		{
			sheet.Copy(sourceRange, destRange, copyStyle);
			destRange.ClearConditionalFormats();
			int count = sourceRange.ConditionalFormats.Count;
			for (int i = 0; i < count; i++)
			{
				ConditionalFormatWrapper conditionalFormatWrapper = sourceRange.ConditionalFormats[i];
				ConditionalFormatWrapper conditionalFormatWrapper2 = destRange.ConditionalFormats.AddCondition();
				conditionalFormatWrapper2.FormatType = conditionalFormatWrapper.FormatType;
				conditionalFormatWrapper2.FirstFormula = conditionalFormatWrapper.FirstFormula;
				conditionalFormatWrapper2.BackColor = conditionalFormatWrapper.BackColor;
				conditionalFormatWrapper2.Color = conditionalFormatWrapper.Color;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003970 File Offset: 0x00001B70
		public static byte[] DoMailMerge(byte[] template, int mergingRowIndex, IDictionary<string, string> standAloneMergeCodesNoTagsWithValues, List<Dictionary<string, string>> mergingRowMergeCodesNoTagsWithValues)
		{
			Workbook workbook = ExcelMergeUtility.LoadWorkbookFromByteArray(template);
			Worksheet worksheet = workbook.Worksheets[0];
			foreach (KeyValuePair<string, string> keyValuePair in standAloneMergeCodesNoTagsWithValues)
			{
				try
				{
					worksheet.Replace("#<" + keyValuePair.Key + ">#", keyValuePair.Value ?? "");
				}
				catch
				{
				}
			}
			bool flag = mergingRowIndex < 1;
			byte[] result;
			if (flag)
			{
				result = ExcelMergeUtility.SaveToBytes(workbook);
			}
			else
			{
				CellRange cellRange = worksheet.Range[mergingRowIndex, 1, mergingRowIndex, worksheet.LastColumn];
				bool flag2 = mergingRowMergeCodesNoTagsWithValues.Count < 1;
				if (flag2)
				{
					worksheet.DeleteRow(cellRange.Row);
					result = ExcelMergeUtility.SaveToBytes(workbook);
				}
				else
				{
					bool flag3 = mergingRowMergeCodesNoTagsWithValues.Count > 1;
					CellRange cellRange2;
					if (flag3)
					{
						int row = mergingRowIndex + 1;
						int column = 1;
						int lastRow = mergingRowIndex + mergingRowMergeCodesNoTagsWithValues.Count - 1;
						int lastColumn = worksheet.LastColumn;
						cellRange2 = worksheet.Range[row, column, lastRow, lastColumn];
						ExcelMergeUtility.CopyRange(worksheet, cellRange, cellRange2, true);
					}
					else
					{
						cellRange2 = worksheet.Range[mergingRowIndex, 1, mergingRowIndex, worksheet.LastColumn];
					}
					int row2 = cellRange.Row;
					int column2 = cellRange.Column;
					int lastRow2 = cellRange2.LastRow;
					int lastColumn2 = cellRange2.LastColumn;
					int num = 0;
					for (int i = row2; i <= lastRow2; i++)
					{
						Dictionary<string, string> mergedRowValues = mergingRowMergeCodesNoTagsWithValues[num];
						for (int j = column2; j <= lastColumn2; j++)
						{
							int k = 0;
							while (k < mergingRowMergeCodesNoTagsWithValues.Count)
							{
								try
								{
									CellRange cellRange3 = worksheet.Range[i, j];
									string value = cellRange3.Value;
									bool flag4 = !value.Contains("#<") || !value.Contains(">#");
									if (!flag4)
									{
										IList<string> list = ExcelMergeUtility.ExtractMailMergeCodesFromStringWithoutTags(value);
										foreach (string mergeCode in list)
										{
											ExcelMergeUtility.MergeValue(cellRange3, mergeCode, mergedRowValues);
										}
									}
								}
								catch
								{
								}
								IL_209:
								k++;
								continue;
								goto IL_209;
							}
						}
						num++;
						bool flag5 = num >= mergingRowMergeCodesNoTagsWithValues.Count;
						if (flag5)
						{
							break;
						}
					}
					result = ExcelMergeUtility.SaveToBytes(workbook);
				}
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003C2C File Offset: 0x00001E2C
		private static void MergeValue(CellRange range, string mergeCode, IDictionary<string, string> mergedRowValues)
		{
			string text = mergeCode.ToLower().Trim();
			string newValue = mergedRowValues.ContainsKey(text) ? (mergedRowValues[text] ?? "") : string.Empty;
			try
			{
				range.Value = range.Value.Replace("#<" + text + ">#", newValue);
			}
			catch
			{
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003CA4 File Offset: 0x00001EA4
		private static byte[] SaveToBytes(Workbook workbook)
		{
			byte[] result = new byte[0];
			using (MemoryStream memoryStream = new MemoryStream(0))
			{
				workbook.SaveToStream(memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public static Workbook LoadWorkbookFromByteArray(byte[] bytes)
		{
			try
			{
				return ExcelMergeUtility.LoadWorkbookFromByteArray(bytes, new ExcelVersion?(ExcelVersion.Version2010));
			}
			catch
			{
			}
			try
			{
				return ExcelMergeUtility.LoadWorkbookFromByteArray(bytes, new ExcelVersion?(ExcelVersion.Version97to2003));
			}
			catch
			{
			}
			return ExcelMergeUtility.LoadWorkbookFromByteArray(bytes, null);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003D5C File Offset: 0x00001F5C
		private static Workbook LoadWorkbookFromByteArray(byte[] bytes, ExcelVersion? excelVersion)
		{
			Workbook workbook = new Workbook();
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				bool flag = excelVersion != null;
				if (flag)
				{
					workbook.LoadFromStream(memoryStream, excelVersion.Value);
				}
				else
				{
					workbook.LoadFromStream(memoryStream);
				}
			}
			return workbook;
		}
	}
}
