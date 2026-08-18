using System;
using System.Data;
using System.Windows.Forms;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ReportFunctions.ReportFunctionParameterControls
{
	// Token: 0x02000036 RID: 54
	public class ReportFunctionParameterControlsFactory
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0003E408 File Offset: 0x0003D408
		public static Control GetControl(int functionCode, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			return ReportFunctionParameterControlsFactory.GetControl(functionCode, da, tripleDES, new DataTable());
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0003E428 File Offset: 0x0003D428
		private static string GetFirstSqlQueryParameters(DataTable searchFunctions)
		{
			foreach (object obj in searchFunctions.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					int num = (dataRow[2] == DBNull.Value) ? 0 : ((int)dataRow[2]);
					if (num == 0 || num == 1 || num == 45 || num == 44 || num == 33 || num == 48)
					{
						return (dataRow[3] != DBNull.Value) ? ((string)dataRow[3]) : "";
					}
				}
			}
			return "";
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0003E520 File Offset: 0x0003D520
		public static Control GetControl(int functionCode, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataTable searchFunctions)
		{
			if (functionCode <= 33)
			{
				switch (functionCode)
				{
				case -1:
					return null;
				case 0:
				case 1:
					goto IL_F0;
				case 2:
				case 3:
					goto IL_1E4;
				case 4:
				case 5:
					break;
				default:
					switch (functionCode)
					{
					case 11:
					case 12:
						break;
					default:
						if (functionCode != 33)
						{
							goto IL_1E4;
						}
						goto IL_F0;
					}
					break;
				}
				ReportFunctionParameterControl_CommaSeparatedWithColumnChooser reportFunctionParameterControl_CommaSeparatedWithColumnChooser = new ReportFunctionParameterControl_CommaSeparatedWithColumnChooser();
				string firstSqlQueryParameters = ReportFunctionParameterControlsFactory.GetFirstSqlQueryParameters(searchFunctions);
				if (!string.IsNullOrEmpty(firstSqlQueryParameters))
				{
					reportFunctionParameterControl_CommaSeparatedWithColumnChooser.SetColumnsToChooseFrom(firstSqlQueryParameters);
				}
				return reportFunctionParameterControl_CommaSeparatedWithColumnChooser;
			}
			if (functionCode <= 58)
			{
				switch (functionCode)
				{
				case 44:
				case 45:
					break;
				case 46:
					goto IL_1E4;
				case 47:
				{
					ReportFunctionParameterControl_DataSyncDataMap reportFunctionParameterControl_DataSyncDataMap = new ReportFunctionParameterControl_DataSyncDataMap();
					string firstSqlQueryParameters2 = ReportFunctionParameterControlsFactory.GetFirstSqlQueryParameters(searchFunctions);
					reportFunctionParameterControl_DataSyncDataMap.Initialize(da);
					if (!string.IsNullOrEmpty(firstSqlQueryParameters2))
					{
						reportFunctionParameterControl_DataSyncDataMap.SetColumnsToChooseFrom(firstSqlQueryParameters2);
					}
					return reportFunctionParameterControl_DataSyncDataMap;
				}
				case 48:
					return new ReportFunctionParameterControl_PlainText();
				default:
				{
					if (functionCode != 58)
					{
						goto IL_1E4;
					}
					ReportFunctionParameterControl_ControlIdChooser reportFunctionParameterControl_ControlIdChooser = new ReportFunctionParameterControl_ControlIdChooser();
					reportFunctionParameterControl_ControlIdChooser.Initialize(da, tripleDES);
					return reportFunctionParameterControl_ControlIdChooser;
				}
				}
			}
			else
			{
				switch (functionCode)
				{
				case 117:
					return new ReportFunctionParameterControl_PlainText();
				case 118:
				case 120:
					goto IL_1E4;
				case 119:
				{
					ReportFunctionParameterControl_importFileDb reportFunctionParameterControl_importFileDb = new ReportFunctionParameterControl_importFileDb();
					reportFunctionParameterControl_importFileDb.Initialize("Csv files|*.csv|All files|*.*", false);
					return reportFunctionParameterControl_importFileDb;
				}
				case 121:
				{
					ReportFunctionParameterControl_importFileDb reportFunctionParameterControl_importFileDb2 = new ReportFunctionParameterControl_importFileDb();
					reportFunctionParameterControl_importFileDb2.Initialize("Text files|*.txt|All files|*.*", true);
					return reportFunctionParameterControl_importFileDb2;
				}
				case 122:
				{
					ReportFunctionParameterControl_ParameterCollect reportFunctionParameterControl_ParameterCollect = new ReportFunctionParameterControl_ParameterCollect();
					reportFunctionParameterControl_ParameterCollect.Initialize(da, tripleDES);
					return reportFunctionParameterControl_ParameterCollect;
				}
				default:
					switch (functionCode)
					{
					case 135:
					{
						ReportFunctionParameterControl_BatchEmail3 reportFunctionParameterControl_BatchEmail = new ReportFunctionParameterControl_BatchEmail3();
						reportFunctionParameterControl_BatchEmail.Initialize(da, tripleDES);
						return reportFunctionParameterControl_BatchEmail;
					}
					case 136:
						return new ReportFunctionParameterControl_PlainText();
					default:
						goto IL_1E4;
					}
					break;
				}
			}
			IL_F0:
			return new ReportFunctionParameterControl_PlainText();
			IL_1E4:
			return new ReportFunctionParameterControl_PlainText();
		}
	}
}
