using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000091 RID: 145
	public class ActiveStudentsWithAccommodations : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0000672B File Offset: 0x0000492B
		public ActiveStudentsWithAccommodations()
		{
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001DCAD File Offset: 0x0001BEAD
		public ActiveStudentsWithAccommodations(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001DCBF File Offset: 0x0001BEBF
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0001DCC7 File Offset: 0x0001BEC7
		public OperationContext OpContext { get; set; }

		// Token: 0x06000533 RID: 1331 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		public static void ExtractStartDateAndEndDateFromParameters(ReportFunction Function, out DateTime? sd, out DateTime? ed)
		{
			bool flag = Function == null || Function.FunctionParameters == null;
			if (flag)
			{
				sd = null;
				ed = null;
			}
			else
			{
				ReportParameter reportParameter = Function.FunctionParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals("startdate", StringComparison.OrdinalIgnoreCase));
				ReportParameter reportParameter2 = Function.FunctionParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals("enddate", StringComparison.OrdinalIgnoreCase));
				bool flag2 = reportParameter == null || reportParameter2 == null;
				if (flag2)
				{
					sd = null;
					ed = null;
				}
				else
				{
					sd = ActiveStudentsWithAccommodations.ConvertToDateTime(reportParameter.Value);
					ed = ActiveStudentsWithAccommodations.ConvertToDateTime(reportParameter2.Value);
				}
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001DD9C File Offset: 0x0001BF9C
		public static DateTime? ConvertToDateTime(object o)
		{
			bool flag = o == null;
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = o is DateTime;
				if (flag2)
				{
					result = new DateTime?((DateTime)o);
				}
				else
				{
					string s = o.ToString();
					DateTime value;
					bool flag3 = !DateTime.TryParse(s, out value);
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = new DateTime?(value);
					}
				}
			}
			return result;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001DE10 File Offset: 0x0001C010
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function)
		{
			DateTime? dateTime;
			DateTime? dateTime2;
			ActiveStudentsWithAccommodations.ExtractStartDateAndEndDateFromParameters(Function, out dateTime, out dateTime2);
			bool flag = dateTime == null || dateTime2 == null;
			if (flag)
			{
				throw new Exception("Invalid or missing startdate/enddate");
			}
			Result.ReportParametersOut.Add(new ReportParameter
			{
				Name = "startdate",
				Value = dateTime.Value
			});
			Result.ReportParametersOut.Add(new ReportParameter
			{
				Name = "enddate",
				Value = dateTime2.Value
			});
			OperationContext operationContext;
			if ((operationContext = this.OpContext) == null)
			{
				(operationContext = new OperationContext()).WhoAmI = 1;
			}
			OperationContext opContext = operationContext;
			IAccommodationsManager accommodationsManager = new AccommodationsManager(opContext);
			IList<DynamicDataSetWithStudentName> list = accommodationsManager.LoadActiveStudentsWithTemplateAccommodations(dateTime.Value, dateTime2.Value);
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(opContext);
			List<DynamicField> list2 = dynamicFieldManager.LoadFields(new DynamicForm
			{
				ScreenNum = 4
			});
			DataTable dataTable = new DataTable("t");
			dataTable.Columns.Add("personid", typeof(int));
			dataTable.Columns.Add("LastName");
			dataTable.Columns.Add("FirstName");
			dataTable.Columns.Add("MiddleName");
			dataTable.Columns.Add("StudentNumber");
			Type typeFromHandle = typeof(bool);
			Type typeFromHandle2 = typeof(DateTime);
			Type typeFromHandle3 = typeof(int);
			Type typeFromHandle4 = typeof(string);
			List<int> cidsUsed = new List<int>();
			foreach (DynamicField dynamicField in list2)
			{
				DynamicControlAttribute attribute = dynamicField.ControlCode.GetAttribute();
				Type type = (attribute == null) ? typeFromHandle4 : attribute.PresentationDataType;
				bool flag2 = type != typeFromHandle && type != typeFromHandle2 && type != typeFromHandle4 && type != typeFromHandle3;
				if (flag2)
				{
					type = typeFromHandle4;
				}
				string text = dynamicField.GetCaptionForDisplay().Replace(".", "_").Replace(",", "_");
				bool flag3 = !dataTable.Columns.Contains(text);
				if (flag3)
				{
					dataTable.Columns.Add(text, type);
					cidsUsed.Add(dynamicField.ControlId);
				}
			}
			Func<DynamicData, bool> <>9__0;
			foreach (DynamicDataSetWithStudentName dynamicDataSetWithStudentName in list)
			{
				IEnumerable<DynamicData> data = dynamicDataSetWithStudentName.Data;
				Func<DynamicData, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((DynamicData g) => !cidsUsed.Contains(g.Field.ControlId)));
				}
				IEnumerable<DynamicData> enumerable = data.Where(predicate);
				foreach (DynamicData dynamicData in enumerable)
				{
					DynamicControlAttribute attribute2 = dynamicData.Field.ControlCode.GetAttribute();
					Type type2 = (attribute2 == null) ? typeFromHandle4 : attribute2.PresentationDataType;
					bool flag4 = type2 != typeFromHandle && type2 != typeFromHandle2 && type2 != typeFromHandle4 && type2 != typeFromHandle3;
					if (flag4)
					{
						type2 = typeFromHandle4;
					}
					string text2 = dynamicData.Field.GetCaptionForDisplay().Replace(".", "_").Replace(",", "_");
					bool flag5 = !dataTable.Columns.Contains(text2);
					if (flag5)
					{
						dataTable.Columns.Add(text2, type2);
						cidsUsed.Add(dynamicData.Field.ControlId);
					}
				}
			}
			foreach (DynamicDataSetWithStudentName dynamicDataSetWithStudentName2 in list)
			{
				DataRow dataRow = dataTable.NewRow();
				PersonBase student = dynamicDataSetWithStudentName2.Student;
				dataRow["personid"] = student.PersonId;
				dataRow["firstname"] = (student.FirstName ?? "");
				dataRow["middlename"] = (student.MiddleName ?? "");
				dataRow["lastname"] = (student.LastName ?? "");
				dataRow["StudentNumber"] = (student.Student_no ?? "");
				foreach (DynamicData dynamicData2 in dynamicDataSetWithStudentName2.Data)
				{
					DynamicControlAttribute attribute3 = dynamicData2.Field.ControlCode.GetAttribute();
					Type left = (attribute3 == null) ? typeFromHandle4 : attribute3.PresentationDataType;
					bool flag6 = left != typeFromHandle && left != typeFromHandle2 && left != typeFromHandle4 && left != typeFromHandle3;
					if (flag6)
					{
						left = typeFromHandle4;
					}
					string columnName = dynamicData2.Field.GetCaptionForDisplay().Replace(".", "_").Replace(",", "_");
					bool flag7 = dynamicData2.Value == null;
					object obj;
					if (flag7)
					{
						obj = null;
					}
					else
					{
						bool flag8 = left == typeFromHandle;
						if (flag8)
						{
							bool flag9 = dynamicData2.Value is bool;
							if (flag9)
							{
								obj = (bool)dynamicData2.Value;
							}
							else
							{
								bool flag10 = dynamicData2.Value is int;
								if (flag10)
								{
									obj = ((int)dynamicData2.Value == 1);
								}
								else
								{
									string value = dynamicData2.Value.ToString();
									bool flag12;
									bool flag11 = !bool.TryParse(value, out flag12);
									if (flag11)
									{
										obj = null;
									}
									else
									{
										obj = flag12;
									}
								}
							}
						}
						else
						{
							bool flag13 = left == typeFromHandle2;
							if (flag13)
							{
								bool flag14 = dynamicData2.Value is DateTime;
								if (flag14)
								{
									obj = (DateTime)dynamicData2.Value;
								}
								else
								{
									DateTime dateTime3;
									bool flag15 = !DateTime.TryParse(dynamicData2.Value.ToString(), out dateTime3);
									if (flag15)
									{
										obj = null;
									}
									else
									{
										obj = dateTime3;
									}
								}
							}
							else
							{
								bool flag16 = left == typeFromHandle3;
								if (flag16)
								{
									bool flag17 = dynamicData2.Value is int;
									if (flag17)
									{
										obj = (int)dynamicData2.Value;
									}
									else
									{
										string s = dynamicData2.Value.ToString();
										int num;
										bool flag18 = !int.TryParse(s, out num);
										if (flag18)
										{
											obj = 0;
										}
										obj = num;
									}
								}
								else
								{
									bool flag19 = dynamicData2.Value == null;
									if (flag19)
									{
										obj = "";
									}
									else
									{
										obj = dynamicData2.Value.ToString();
									}
								}
							}
						}
					}
					bool flag20 = obj != null;
					if (flag20)
					{
						dataRow[columnName] = obj;
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			Result.Data.Table = dataTable;
		}
	}
}
