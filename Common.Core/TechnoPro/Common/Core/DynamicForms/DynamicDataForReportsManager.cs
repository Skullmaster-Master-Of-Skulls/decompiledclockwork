using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Encryption;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.Encryption;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DataStructure.Adapters;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports.StudentReportInfo;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.DynamicForms
{
	// Token: 0x020000FA RID: 250
	public class DynamicDataForReportsManager : IDynamicDataForReportsManager
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0003E17E File Offset: 0x0003C37E
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x0003E186 File Offset: 0x0003C386
		public OperationContext OpContext { get; set; }

		// Token: 0x060009C6 RID: 2502 RVA: 0x0003E18F File Offset: 0x0003C38F
		public DynamicDataForReportsManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0003E1A4 File Offset: 0x0003C3A4
		private IList<StudentInfoItemBase> AddStudentReportInfoEmail(int[] pids, int cid = 0)
		{
			IStudentCommonInfoManager studentCommonInfoManager = new StudentCommonInfoManager(this.OpContext);
			IList<StudentWithCommonInfo> source = studentCommonInfoManager.LoadStudentsWithCommonInfo(pids.ToList<int>());
			return (from m in source.Where(delegate(StudentWithCommonInfo g)
			{
				StudentCommonInfo commonInfo = g.CommonInfo;
				return (((commonInfo != null) ? commonInfo.Email : null) ?? "").Trim().Length > 0;
			})
			select new StudentInfoEmailItem
			{
				PersonId = m.Student.PersonId,
				Email = m.CommonInfo.Email.Trim()
			}).ToList<StudentInfoItemBase>();
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0003E220 File Offset: 0x0003C420
		[DebuggerStepThrough]
		private Task<IList<StudentInfoItemBase>> AddStudentReportInfoEmailAsync(int[] pids, int cid = 0)
		{
			DynamicDataForReportsManager.<AddStudentReportInfoEmailAsync>d__8 <AddStudentReportInfoEmailAsync>d__ = new DynamicDataForReportsManager.<AddStudentReportInfoEmailAsync>d__8();
			<AddStudentReportInfoEmailAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentInfoItemBase>>.Create();
			<AddStudentReportInfoEmailAsync>d__.<>4__this = this;
			<AddStudentReportInfoEmailAsync>d__.pids = pids;
			<AddStudentReportInfoEmailAsync>d__.cid = cid;
			<AddStudentReportInfoEmailAsync>d__.<>1__state = -1;
			<AddStudentReportInfoEmailAsync>d__.<>t__builder.Start<DynamicDataForReportsManager.<AddStudentReportInfoEmailAsync>d__8>(ref <AddStudentReportInfoEmailAsync>d__);
			return <AddStudentReportInfoEmailAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0003E274 File Offset: 0x0003C474
		private IList<StudentInfoItemBase> AddStudentReportInfoAssignedAdvisor(int[] pids, int cid = 0)
		{
			IStudentCommonInfoManager studentCommonInfoManager = new StudentCommonInfoManager(this.OpContext);
			IList<StudentWithCommonInfo> source = studentCommonInfoManager.LoadStudentsWithCommonInfo(pids.ToList<int>());
			return source.Where(delegate(StudentWithCommonInfo g)
			{
				StudentCommonInfo commonInfo = g.CommonInfo;
				return ((commonInfo != null) ? commonInfo.AssignedCounsellor : null) != null;
			}).Select(delegate(StudentWithCommonInfo m)
			{
				PersonBase student = m.Student;
				return new StudentInfoAssignedAdvisorItem((student != null) ? student.PersonId : 0, m.CommonInfo);
			}).ToList<StudentInfoItemBase>();
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0003E2F0 File Offset: 0x0003C4F0
		[DebuggerStepThrough]
		private Task<IList<StudentInfoItemBase>> AddStudentReportInfoAssignedAdvisorAsync(int[] pids, int cid = 0)
		{
			DynamicDataForReportsManager.<AddStudentReportInfoAssignedAdvisorAsync>d__10 <AddStudentReportInfoAssignedAdvisorAsync>d__ = new DynamicDataForReportsManager.<AddStudentReportInfoAssignedAdvisorAsync>d__10();
			<AddStudentReportInfoAssignedAdvisorAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentInfoItemBase>>.Create();
			<AddStudentReportInfoAssignedAdvisorAsync>d__.<>4__this = this;
			<AddStudentReportInfoAssignedAdvisorAsync>d__.pids = pids;
			<AddStudentReportInfoAssignedAdvisorAsync>d__.cid = cid;
			<AddStudentReportInfoAssignedAdvisorAsync>d__.<>1__state = -1;
			<AddStudentReportInfoAssignedAdvisorAsync>d__.<>t__builder.Start<DynamicDataForReportsManager.<AddStudentReportInfoAssignedAdvisorAsync>d__10>(ref <AddStudentReportInfoAssignedAdvisorAsync>d__);
			return <AddStudentReportInfoAssignedAdvisorAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0003E344 File Offset: 0x0003C544
		private IList<StudentInfoItemBase> AddStudentReportInfoAccExpiry(int[] pids, int cid = 0)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
			IDictionary<int, DateTime?> source = accommodationsManager.LoadAccommodationExpiryDatesForStudents(pids);
			return source.Where(delegate(KeyValuePair<int, DateTime?> kvp)
			{
				KeyValuePair<int, DateTime?> keyValuePair = kvp;
				return keyValuePair.Value != null;
			}).Select(delegate(KeyValuePair<int, DateTime?> kvp)
			{
				StudentInfoAccExpiryItem studentInfoAccExpiryItem = new StudentInfoAccExpiryItem();
				KeyValuePair<int, DateTime?> keyValuePair = kvp;
				studentInfoAccExpiryItem.PersonId = keyValuePair.Key;
				keyValuePair = kvp;
				studentInfoAccExpiryItem.AccExpiry = keyValuePair.Value.Value;
				return studentInfoAccExpiryItem;
			}).Cast<StudentInfoItemBase>().ToList<StudentInfoItemBase>();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0003E3C0 File Offset: 0x0003C5C0
		[DebuggerStepThrough]
		private Task<IList<StudentInfoItemBase>> AddStudentReportInfoAccExpiryAsync(int[] pids, int cid = 0)
		{
			DynamicDataForReportsManager.<AddStudentReportInfoAccExpiryAsync>d__12 <AddStudentReportInfoAccExpiryAsync>d__ = new DynamicDataForReportsManager.<AddStudentReportInfoAccExpiryAsync>d__12();
			<AddStudentReportInfoAccExpiryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentInfoItemBase>>.Create();
			<AddStudentReportInfoAccExpiryAsync>d__.<>4__this = this;
			<AddStudentReportInfoAccExpiryAsync>d__.pids = pids;
			<AddStudentReportInfoAccExpiryAsync>d__.cid = cid;
			<AddStudentReportInfoAccExpiryAsync>d__.<>1__state = -1;
			<AddStudentReportInfoAccExpiryAsync>d__.<>t__builder.Start<DynamicDataForReportsManager.<AddStudentReportInfoAccExpiryAsync>d__12>(ref <AddStudentReportInfoAccExpiryAsync>d__);
			return <AddStudentReportInfoAccExpiryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0003E414 File Offset: 0x0003C614
		private IList<StudentInfoItemBase> AddStudentReportInfoAge(int[] pids, int cid = 0)
		{
			IDynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
			bool flag = cid < 1;
			IList<StudentInfoItemBase> result;
			if (flag)
			{
				CWLogger.Logger.Warn("DynamicDataForReportsManager:LoadStudentReportInfo:MissingControlIdForAge");
				result = new List<StudentInfoItemBase>();
			}
			else
			{
				IDictionary<int, DateTime?> source = dynamicDataDAO.LoadDateTimeDynamicPerStudentDataForStudents(pids, cid);
				result = (from g in source
				where g.Value != null
				select g into h
				select new StudentInfoAgeItem
				{
					PersonId = h.Key,
					DateOfBirth = new DateTime?(h.Value.Value),
					Age = DynamicDataForReportsManager.GetAge(h.Value.Value)
				}).ToList<StudentInfoItemBase>();
			}
			return result;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0003E4AC File Offset: 0x0003C6AC
		[DebuggerStepThrough]
		private Task<IList<StudentInfoItemBase>> AddStudentReportInfoAgeAsync(int[] pids, int cid = 0)
		{
			DynamicDataForReportsManager.<AddStudentReportInfoAgeAsync>d__14 <AddStudentReportInfoAgeAsync>d__ = new DynamicDataForReportsManager.<AddStudentReportInfoAgeAsync>d__14();
			<AddStudentReportInfoAgeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentInfoItemBase>>.Create();
			<AddStudentReportInfoAgeAsync>d__.<>4__this = this;
			<AddStudentReportInfoAgeAsync>d__.pids = pids;
			<AddStudentReportInfoAgeAsync>d__.cid = cid;
			<AddStudentReportInfoAgeAsync>d__.<>1__state = -1;
			<AddStudentReportInfoAgeAsync>d__.<>t__builder.Start<DynamicDataForReportsManager.<AddStudentReportInfoAgeAsync>d__14>(ref <AddStudentReportInfoAgeAsync>d__);
			return <AddStudentReportInfoAgeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0003E500 File Offset: 0x0003C700
		private IDictionary<eDynamicFormType, IList<int>> GetDistinctFormTypesWithControlIds(IList<int> controlIds)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			IList<DynamicForm> forms;
			IDictionary<int, IList<int>> source = dynamicFormManager.FindScreensControlIdsExistOn(controlIds, out forms);
			foreach (DynamicForm dynamicForm in forms)
			{
				bool flag = dynamicForm.ScreenNum == 4;
				if (flag)
				{
					dynamicForm.FormType = eDynamicFormType.Accommodation;
				}
			}
			IEnumerable<DynamicDataForReportsManager.ControlIdWithFormsItExistsOn> enumerable = from g in source
			select new DynamicDataForReportsManager.ControlIdWithFormsItExistsOn(g.Key, g.Value, forms);
			Dictionary<eDynamicFormType, IList<int>> dictionary = new Dictionary<eDynamicFormType, IList<int>>();
			foreach (DynamicDataForReportsManager.ControlIdWithFormsItExistsOn controlIdWithFormsItExistsOn in enumerable)
			{
				DynamicDataForReportsManager.eFormAControlExistsOnStatus formsStatus = controlIdWithFormsItExistsOn.GetFormsStatus();
				bool flag2 = formsStatus == DynamicDataForReportsManager.eFormAControlExistsOnStatus.HasNoForms;
				if (flag2)
				{
					CWLogger.Logger.Warn("DynamicDataForReportsManager:CrossReferenceData:ControlId provided has no forms - it will be ignored:cid={0}", controlIdWithFormsItExistsOn.ControlId.ToString());
				}
				else
				{
					foreach (DynamicForm dynamicForm2 in controlIdWithFormsItExistsOn.Forms)
					{
						eDynamicFormType formType = dynamicForm2.FormType;
						bool flag3 = !dictionary.ContainsKey(formType);
						if (flag3)
						{
							dictionary.Add(formType, new List<int>());
						}
						IList<int> list = dictionary[formType];
						int controlId = controlIdWithFormsItExistsOn.ControlId;
						bool flag4 = !list.Contains(controlId);
						if (flag4)
						{
							list.Add(controlId);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0003E6C0 File Offset: 0x0003C8C0
		private static int GetAge(DateTime birthday)
		{
			DateTime today = DateTime.Today;
			int num = today.Year - birthday.Year;
			bool flag = today < birthday.AddYears(num);
			if (flag)
			{
				num--;
			}
			return (num > 0) ? num : 0;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0003E708 File Offset: 0x0003C908
		private static string GetUniqueColumnName(DataTable t, string potentialColumnName)
		{
			int num = 1;
			string text = potentialColumnName;
			while (t.Columns.Contains(text) && num < 1000000)
			{
				text = potentialColumnName + "_" + num++.ToString();
			}
			return text;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0003E75C File Offset: 0x0003C95C
		private static string GetUniqueColumnName(DataColumnCollection cols, string proposedColName)
		{
			bool flag = !cols.Contains(proposedColName);
			if (!flag)
			{
				int i = 2;
				while (i < 100000)
				{
					string text = proposedColName + i++.ToString();
					bool flag2 = !cols.Contains(text);
					if (flag2)
					{
						return text;
					}
				}
				throw new Exception(string.Concat(new string[]
				{
					"Can't find unique column name for ",
					proposedColName,
					" (i=",
					i.ToString(),
					")"
				}));
			}
			return proposedColName;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0003E7F4 File Offset: 0x0003C9F4
		private static DataTable MergeData(DataTable primaryTable, DataTable lookedUpData, params string[] rowIdentifierColumnNames)
		{
			bool flag = primaryTable != null && primaryTable.TableName == "";
			if (flag)
			{
				primaryTable.TableName = "t1";
			}
			List<string> list = (from DataColumn dc in lookedUpData.Columns
			where rowIdentifierColumnNames.All((string g) => !g.Equals(dc.ColumnName, StringComparison.OrdinalIgnoreCase))
			select dc.ColumnName).ToList<string>();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in list)
			{
				string uniqueColumnName = DynamicDataForReportsManager.GetUniqueColumnName(primaryTable.Columns, text);
				primaryTable.Columns.Add(uniqueColumnName, lookedUpData.Columns[text].DataType);
				dictionary.Add(text, uniqueColumnName);
			}
			DataView dataView = new DataView();
			dataView.Table = primaryTable;
			dataView.Sort = string.Join(",", (from g in rowIdentifierColumnNames
			select g.ToString()).ToArray<string>());
			DataView dataView2 = dataView;
			int j;
			for (int i = 0; i < dataView2.Count; i = j)
			{
				DataRow row = dataView2[i].Row;
				int[] ids0 = DynamicDataForReportsManager.GetIdsFromRow(row, rowIdentifierColumnNames);
				for (j = i + 1; j < dataView2.Count; j++)
				{
					DataRow row2 = dataView2[j].Row;
					int[] ids = DynamicDataForReportsManager.GetIdsFromRow(row2, rowIdentifierColumnNames);
					bool flag2 = ids0.Length != ids.Length || ids0.Where((int t, int k) => t != ids[k]).Any<int>();
					if (flag2)
					{
						break;
					}
				}
				string filterExpression = string.Join(" AND ", rowIdentifierColumnNames.Select((string t, int k) => t.ToString() + "=" + ids0[k].ToString()).ToArray<string>());
				DataRow dataRow = lookedUpData.Select(filterExpression).FirstOrDefault<DataRow>();
				bool flag3 = dataRow != null;
				if (flag3)
				{
					for (int l = i; l < j; l++)
					{
						foreach (KeyValuePair<string, string> keyValuePair in dictionary)
						{
							dataView2[l].Row[keyValuePair.Value] = dataRow[keyValuePair.Key];
						}
					}
				}
			}
			return primaryTable;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0003EAE8 File Offset: 0x0003CCE8
		private static int[] GetIdsFromRow(DataRow dr, params string[] colNames)
		{
			return (from g in colNames
			select (dr[g] is DBNull) ? 0 : ((int)dr[g])).ToArray<int>();
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0003EB20 File Offset: 0x0003CD20
		private static IList<int> GetIdsFromRows(string colName, DataTable t, int start, int end)
		{
			List<int> list = new List<int>();
			for (int i = start; i <= end; i++)
			{
				DataRow dataRow = t.Rows[i];
				list.Add((dataRow[colName] is DBNull) ? 0 : ((int)dataRow[colName]));
			}
			return list;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0003EB84 File Offset: 0x0003CD84
		private static IList<DynamicDataContext> GetDataContextsFromRows(DataTable t, int start, int end, string primaryColName, string secondaryColName)
		{
			List<DynamicDataContext> list = new List<DynamicDataContext>();
			for (int i = start; i <= end; i++)
			{
				DataRow dataRow = t.Rows[i];
				list.Add(new DynamicDataContext
				{
					PrimaryId = ((dataRow[primaryColName] is DBNull) ? 0 : ((int)dataRow[primaryColName])),
					SecondaryId = (string.IsNullOrEmpty(secondaryColName) ? 0 : ((dataRow[secondaryColName] is DBNull) ? 0 : ((int)dataRow[secondaryColName])))
				});
			}
			return list;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0003EC2C File Offset: 0x0003CE2C
		private bool DoesTableWithContextContainAllColumns(DataTable TableWithContext, bool tryToUseStudent_noToProvidePersonId, params string[] requiredColumns)
		{
			foreach (string text in requiredColumns)
			{
				bool flag = TableWithContext.Columns.Contains(text);
				if (!flag)
				{
					bool flag2 = !tryToUseStudent_noToProvidePersonId || !text.Equals("personid", StringComparison.OrdinalIgnoreCase);
					if (flag2)
					{
						return false;
					}
					this.LookupPersonIdsAndAddToTable(ref TableWithContext, "student_no");
				}
			}
			return true;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0003EC98 File Offset: 0x0003CE98
		private void LookupPersonIdsAndAddToTable(ref DataTable t, string studentNumberColName)
		{
			bool flag = t.Columns[studentNumberColName].DataType == typeof(byte[]);
			if (flag)
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				IEncryptionDAO encryptionDAO = new EncryptionDAO(DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption);
				encryptionDAO.DecryptColumns(t, new string[]
				{
					studentNumberColName
				});
			}
			List<string> studentNumbers = (from DataRow dr in t.Rows
			where dr.RowState != DataRowState.Deleted && dr[studentNumberColName] != DBNull.Value && dr[studentNumberColName].ToString().Trim().Length > 0
			select dr[studentNumberColName].ToString()).Distinct<string>().ToList<string>();
			IList<Chunk> source = studentNumbers.BreakdownItemsIntoChunks(100000);
			IPeopleManager pm = new PeopleManager(this.OpContext);
			List<IDictionary<string, int>> list = (from chunk in source
			select pm.LoadPersonIdsByStudentNumbers2(studentNumbers.GetRange(chunk.Start, chunk.End - chunk.Start + 1))).ToList<IDictionary<string, int>>();
			t.Columns.Add("personid", typeof(int));
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow["student_no"].ToString();
				bool flag2 = text.Length <= 0;
				if (!flag2)
				{
					foreach (IDictionary<string, int> dictionary in list)
					{
						bool flag3 = !dictionary.ContainsKey(text);
						if (!flag3)
						{
							dataRow["personid"] = dictionary[text];
							break;
						}
					}
				}
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0003EE9C File Offset: 0x0003D09C
		public DataTable CrossReferenceAccommodationDataTemplateOrCourseSpecific(DataTable TableWithContext, IList<int> ControlIds)
		{
			bool flag = !this.DoesTableWithContextContainAllColumns(TableWithContext = (TableWithContext ?? new DataTable("TableWithContext")), true, new string[]
			{
				"personid",
				"lucourseid"
			});
			DataTable result;
			if (flag)
			{
				result = TableWithContext;
			}
			else
			{
				bool flag2 = !TableWithContext.Columns.Contains("appointmentid");
				if (flag2)
				{
					TableWithContext.Columns.Add("appointmentid", typeof(int));
				}
				IDynamicDataForReportsDAO dynamicDataForReportsDao = new DynamicDataForReportsDAO(this.OpContext);
				IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns;
				DataTable lookedUpData = this.LoadDynamicDataForMultipleStudentsAsDataTable(TableWithContext.Rows.Count, (int start, int end) => dynamicDataForReportsDao.LoadAccommodationDataForMultipleStudentsAsDataTable(DynamicDataForReportsManager.GetDataContextsFromRows(TableWithContext, start, end, "personid", "lucourseid"), ControlIds, out specialDataColumns));
				result = DynamicDataForReportsManager.MergeData(TableWithContext, lookedUpData, new string[]
				{
					"personid"
				});
			}
			return result;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0003EFA0 File Offset: 0x0003D1A0
		public DataTable CrossReferenceAccommodationDataTemplateOnly(DataTable TableWithContext, IList<int> ControlIds)
		{
			bool flag = !this.DoesTableWithContextContainAllColumns(TableWithContext = (TableWithContext ?? new DataTable("TableWithContext")), true, new string[]
			{
				"personid"
			});
			DataTable result;
			if (flag)
			{
				result = TableWithContext;
			}
			else
			{
				string text = null;
				bool flag2 = !TableWithContext.Columns.Contains("appointmentid");
				if (flag2)
				{
					TableWithContext.Columns.Add("appointmentid", typeof(int));
				}
				else
				{
					bool flag3 = (from DataRow dr in TableWithContext.Rows
					select (dr["appointmentid"] is DBNull) ? 0 : ((int)dr["appointmentid"])).Any((int g) => g > 0);
					bool flag4 = flag3;
					if (flag4)
					{
						text = DynamicDataForReportsManager.GetUniqueColumnName(TableWithContext.Columns, "appointmentid_bak");
						TableWithContext.Columns["appointmentid"].ColumnName = text;
						TableWithContext.Columns.Add("appointmentid");
					}
				}
				IDynamicDataForReportsDAO dynamicDataForReportsDao = new DynamicDataForReportsDAO(this.OpContext);
				IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns;
				DataTable lookedUpData = this.LoadDynamicDataForMultipleStudentsAsDataTable(TableWithContext.Rows.Count, (int start, int end) => dynamicDataForReportsDao.LoadAccommodationDataForMultipleStudentsAsDataTable(DynamicDataForReportsManager.GetDataContextsFromRows(TableWithContext, start, end, "personid", "appointmentid"), ControlIds, out specialDataColumns));
				DataTable dataTable = DynamicDataForReportsManager.MergeData(TableWithContext, lookedUpData, new string[]
				{
					"personid"
				});
				bool flag5 = string.IsNullOrEmpty(text);
				if (flag5)
				{
					result = dataTable;
				}
				else
				{
					dataTable.Columns.Remove("appointmentid");
					dataTable.Columns[text].ColumnName = "appointmentid";
					result = dataTable;
				}
			}
			return result;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0003F190 File Offset: 0x0003D390
		public DataTable CrossReferencePerStudentData(DataTable TableWithContext, IList<int> ControlIds)
		{
			bool flag = !this.DoesTableWithContextContainAllColumns(TableWithContext = (TableWithContext ?? new DataTable("TableWithContext")), true, new string[]
			{
				"personid"
			});
			DataTable result;
			if (flag)
			{
				result = TableWithContext;
			}
			else
			{
				IDynamicDataForReportsDAO dynamicDataForReportsDao = new DynamicDataForReportsDAO(this.OpContext);
				IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns;
				DataTable lookedUpData = this.LoadDynamicDataForMultipleStudentsAsDataTable(TableWithContext.Rows.Count, (int start, int end) => dynamicDataForReportsDao.LoadPerStudentDataForMultipleStudentsAsDataTable(DynamicDataForReportsManager.GetIdsFromRows("personid", TableWithContext, start, end), ControlIds, out specialDataColumns));
				result = DynamicDataForReportsManager.MergeData(TableWithContext, lookedUpData, new string[]
				{
					"personid"
				});
			}
			return result;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0003F24C File Offset: 0x0003D44C
		public DataTable CrossReferencePerDateData(DataTable TableWithContext, IList<int> ControlIds)
		{
			bool flag = !this.DoesTableWithContextContainAllColumns(TableWithContext = (TableWithContext ?? new DataTable("TableWithContext")), true, new string[]
			{
				"personid"
			});
			DataTable result;
			if (flag)
			{
				result = TableWithContext;
			}
			else
			{
				IDynamicDataForReportsDAO dynamicDataForReportsDao = new DynamicDataForReportsDAO(this.OpContext);
				IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns;
				DataTable lookedUpData = this.LoadDynamicDataForMultipleStudentsAsDataTable(TableWithContext.Rows.Count, (int start, int end) => dynamicDataForReportsDao.LoadPerDateDataForMultipleStudentsAsDataTable(DynamicDataForReportsManager.GetDataContextsFromRows(TableWithContext, start, end, "personid", "appointmentid"), ControlIds, out specialDataColumns));
				result = DynamicDataForReportsManager.MergeData(TableWithContext, lookedUpData, new string[]
				{
					"personid",
					"appointmentid"
				});
			}
			return result;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0003F310 File Offset: 0x0003D510
		public DataTable CrossReferencePerAppointmentData(DataTable TableWithContext, IList<int> ControlIds)
		{
			bool flag = !this.DoesTableWithContextContainAllColumns(TableWithContext = (TableWithContext ?? new DataTable("TableWithContext")), true, new string[]
			{
				"personid",
				"appointmentid"
			});
			DataTable result;
			if (flag)
			{
				result = TableWithContext;
			}
			else
			{
				IDynamicDataForReportsDAO dynamicDataForReportsDao = new DynamicDataForReportsDAO(this.OpContext);
				IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns;
				DataTable lookedUpData = this.LoadDynamicDataForMultipleStudentsAsDataTable(TableWithContext.Rows.Count, (int start, int end) => dynamicDataForReportsDao.LoadPerAppointmentDataForMultipleStudentsAsDataTable(DynamicDataForReportsManager.GetDataContextsFromRows(TableWithContext, start, end, "personid", "appointmentid"), ControlIds, out specialDataColumns));
				result = DynamicDataForReportsManager.MergeData(TableWithContext, lookedUpData, new string[]
				{
					"personid",
					"appointmentid"
				});
			}
			return result;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0003F3DC File Offset: 0x0003D5DC
		public DataTable LoadDynamicDataForMultipleStudentsAsDataTable(int rowCount, Func<int, int, DataTable> LoadDynamicData)
		{
			IList<Chunk> list = rowCount.BreakdownItemsIntoChunks(100000);
			DataTable dataTable = null;
			foreach (Chunk chunk in list)
			{
				DataTable dataTable2 = LoadDynamicData(chunk.Start, chunk.End);
				bool flag = dataTable == null;
				if (flag)
				{
					dataTable = dataTable2;
				}
				else
				{
					foreach (object obj in dataTable2.Rows)
					{
						DataRow row = (DataRow)obj;
						dataTable.ImportRow(row);
					}
				}
			}
			return dataTable ?? new DataTable("t");
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0003F4C0 File Offset: 0x0003D6C0
		public DataTable CrossReferenceDataIntoSingleTable(DataTable TableWithContext, IList<int> ControlIds)
		{
			IDictionary<eDynamicFormType, IList<int>> distinctFormTypesWithControlIds = this.GetDistinctFormTypesWithControlIds(ControlIds);
			foreach (KeyValuePair<eDynamicFormType, IList<int>> keyValuePair in distinctFormTypesWithControlIds)
			{
				switch (keyValuePair.Key)
				{
				case eDynamicFormType.PerStudent:
					this.CrossReferencePerStudentData(TableWithContext, keyValuePair.Value);
					break;
				case eDynamicFormType.PerAppointment:
					this.CrossReferencePerAppointmentData(TableWithContext, keyValuePair.Value);
					break;
				case eDynamicFormType.Anonymous:
					goto IL_BB;
				case eDynamicFormType.Accommodation:
				{
					bool flag = !TableWithContext.Columns.Contains("lucourseid") && !TableWithContext.Columns.Contains("coursesid");
					if (flag)
					{
						this.CrossReferenceAccommodationDataTemplateOnly(TableWithContext, keyValuePair.Value);
					}
					else
					{
						this.CrossReferenceAccommodationDataTemplateOrCourseSpecific(TableWithContext, keyValuePair.Value);
					}
					break;
				}
				default:
					goto IL_BB;
				}
				continue;
				IL_BB:
				CWLogger.Logger.Warn("DynamicDataForReportsManager:CrossReferenceDataIntoSingleTable:controlid on Un-supported form passed (will be skipped):cids={0}:formtype={1}", string.Join(",", (from g in keyValuePair.Value
				select g.ToString()).ToArray<string>()), keyValuePair.Key.ToString());
			}
			return TableWithContext;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0003F628 File Offset: 0x0003D828
		public DataTable ExpandListViewOrFileList(DataTable table, IList<DynamicDataColumn> cols)
		{
			DataTable dataTable = table;
			bool flag = dataTable == null || dataTable.Columns.Count <= 0 || dataTable.Rows.Count <= 0;
			DataTable result;
			if (flag)
			{
				result = dataTable;
			}
			else
			{
				IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				using (IEnumerator<DynamicDataColumn> enumerator = cols.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DynamicDataColumn col = enumerator.Current;
						IDictionary<string, Type> dictionary = (col.ControlId > 0) ? dynamicFieldManager.LoadListViewOrFileListColumns(col.ControlId) : null;
						bool flag2 = dictionary == null;
						if (flag2)
						{
							string text = (from DataRow dr in dataTable.Rows
							where dr[col.ColumnName] != DBNull.Value && dr[col.ColumnName].ToString().Trim().Length > 0
							select dr[col.ColumnName].ToString().Trim()).FirstOrDefault<string>();
							bool flag3 = text != null;
							if (flag3)
							{
								DataTable dataTable2 = text.ConvertListViewDataToDataTable(dictionary);
								dictionary = (from DataColumn dc in dataTable2.Columns
								select dc).ToDictionary((DataColumn g) => g.ColumnName, (DataColumn g) => g.DataType);
							}
						}
						bool flag4 = dictionary == null;
						if (!flag4)
						{
							Dictionary<string, Type> dictionary2 = new Dictionary<string, Type>();
							foreach (KeyValuePair<string, Type> keyValuePair in dictionary)
							{
								string uniqueColumnName = DynamicDataForReportsManager.GetUniqueColumnName(dataTable, keyValuePair.Key);
								dataTable.Columns.Add(uniqueColumnName, keyValuePair.Value);
								dictionary2.Add(uniqueColumnName, keyValuePair.Value);
							}
							dictionary = dictionary2;
							DataTable dataTable3 = dataTable.Clone();
							foreach (object obj in dataTable.Rows)
							{
								DataRow dataRow = (DataRow)obj;
								string text2 = (dataRow[col.ColumnName] is DBNull) ? "" : dataRow[col.ColumnName].ToString();
								bool flag5 = text2.Length > 0;
								if (flag5)
								{
									DataTable dataTable4 = text2.ConvertListViewDataToDataTable(dictionary);
									bool flag6 = dataTable4.Rows.Count > 0;
									if (flag6)
									{
										foreach (object obj2 in dataTable4.Rows)
										{
											DataRow dataRow2 = (DataRow)obj2;
											foreach (object obj3 in dataTable4.Columns)
											{
												DataColumn dataColumn = (DataColumn)obj3;
												dataRow[dataColumn.ColumnName] = dataRow2[dataColumn.ColumnName];
											}
											dataTable3.ImportRow(dataRow);
										}
									}
									else
									{
										dataTable3.ImportRow(dataRow);
									}
								}
								else
								{
									dataTable3.ImportRow(dataRow);
								}
							}
							dataTable = dataTable3;
							dataTable.Columns.Remove(col.ColumnName);
						}
					}
				}
				result = dataTable;
			}
			return result;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0003FA50 File Offset: 0x0003DC50
		[DebuggerStepThrough]
		public Task<IList<StudentInfoItemBase>[]> LoadStudentReportInfoAsync(int[] pids, eDynamicStudentReportInfoType[] typesToLoad, IDictionary<eDynamicStudentReportInfoType, int> ControlIds)
		{
			DynamicDataForReportsManager.<LoadStudentReportInfoAsync>d__33 <LoadStudentReportInfoAsync>d__ = new DynamicDataForReportsManager.<LoadStudentReportInfoAsync>d__33();
			<LoadStudentReportInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentInfoItemBase>[]>.Create();
			<LoadStudentReportInfoAsync>d__.<>4__this = this;
			<LoadStudentReportInfoAsync>d__.pids = pids;
			<LoadStudentReportInfoAsync>d__.typesToLoad = typesToLoad;
			<LoadStudentReportInfoAsync>d__.ControlIds = ControlIds;
			<LoadStudentReportInfoAsync>d__.<>1__state = -1;
			<LoadStudentReportInfoAsync>d__.<>t__builder.Start<DynamicDataForReportsManager.<LoadStudentReportInfoAsync>d__33>(ref <LoadStudentReportInfoAsync>d__);
			return <LoadStudentReportInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0003FAAC File Offset: 0x0003DCAC
		public IList<StudentInfoItemBase>[] LoadStudentReportInfo(int[] pids, eDynamicStudentReportInfoType[] typesToLoad, IDictionary<eDynamicStudentReportInfoType, int> ControlIds)
		{
			bool flag = pids == null || pids.Length < 1 || typesToLoad == null || typesToLoad.Length < 1;
			IList<StudentInfoItemBase>[] result;
			if (flag)
			{
				result = new IList<StudentInfoItemBase>[0];
			}
			else
			{
				int dobCid = (ControlIds == null || !ControlIds.ContainsKey(eDynamicStudentReportInfoType.Age)) ? 0 : ControlIds[eDynamicStudentReportInfoType.Age];
				ConcurrentBag<IList<StudentInfoItemBase>> items = new ConcurrentBag<IList<StudentInfoItemBase>>();
				Parallel.ForEach<eDynamicStudentReportInfoType>(typesToLoad, delegate(eDynamicStudentReportInfoType typeToLoad)
				{
					IList<StudentInfoItemBase> list;
					switch (typeToLoad)
					{
					case eDynamicStudentReportInfoType.Email:
						list = this.AddStudentReportInfoEmail(pids, 0);
						break;
					case eDynamicStudentReportInfoType.AssignedAdvisor:
						list = this.AddStudentReportInfoAssignedAdvisor(pids, 0);
						break;
					case eDynamicStudentReportInfoType.Age:
						list = this.AddStudentReportInfoAge(pids, dobCid);
						break;
					case eDynamicStudentReportInfoType.AccommodationsExpiry:
						list = this.AddStudentReportInfoAccExpiry(pids, 0);
						break;
					default:
						list = null;
						break;
					}
					bool flag2 = list != null;
					if (flag2)
					{
						items.Add(list);
					}
				});
				result = items.ToArray();
			}
			return result;
		}

		// Token: 0x020002D3 RID: 723
		internal class ControlIdWithFormsItExistsOn
		{
			// Token: 0x06001570 RID: 5488 RVA: 0x00085098 File Offset: 0x00083298
			public ControlIdWithFormsItExistsOn(int cid, IList<int> screenNums, IList<DynamicForm> forms)
			{
				this.ControlId = cid;
				Dictionary<int, DynamicForm> source = (screenNums ?? new List<int>()).ToDictionary((int g) => g, (int g) => forms.FirstOrDefault((DynamicForm h) => h.ScreenNum == g));
				this.ScreenNumsWithMissingForms = (from g in source
				where g.Value == null
				select g into h
				select h.Key).ToList<int>();
				this.Forms = (from g in source
				where g.Value != null
				select g into h
				select h.Value).ToList<DynamicForm>();
			}

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06001571 RID: 5489 RVA: 0x000851A7 File Offset: 0x000833A7
			// (set) Token: 0x06001572 RID: 5490 RVA: 0x000851AF File Offset: 0x000833AF
			public int ControlId { get; set; }

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06001573 RID: 5491 RVA: 0x000851B8 File Offset: 0x000833B8
			// (set) Token: 0x06001574 RID: 5492 RVA: 0x000851C0 File Offset: 0x000833C0
			public IList<DynamicForm> Forms { get; set; }

			// Token: 0x17000289 RID: 649
			// (get) Token: 0x06001575 RID: 5493 RVA: 0x000851C9 File Offset: 0x000833C9
			// (set) Token: 0x06001576 RID: 5494 RVA: 0x000851D1 File Offset: 0x000833D1
			public IList<int> ScreenNumsWithMissingForms { get; set; }

			// Token: 0x06001577 RID: 5495 RVA: 0x000851DC File Offset: 0x000833DC
			public DynamicDataForReportsManager.eFormAControlExistsOnStatus GetFormsStatus()
			{
				bool flag = this.Forms.Count < 1;
				DynamicDataForReportsManager.eFormAControlExistsOnStatus result;
				if (flag)
				{
					result = DynamicDataForReportsManager.eFormAControlExistsOnStatus.HasNoForms;
				}
				else
				{
					IEnumerable<eDynamicFormType> source = (from g in this.Forms
					select g.FormType).Distinct<eDynamicFormType>();
					result = ((source.Count<eDynamicFormType>() > 1) ? DynamicDataForReportsManager.eFormAControlExistsOnStatus.HasMultipleFormTypes : DynamicDataForReportsManager.eFormAControlExistsOnStatus.HasSingleFormType);
				}
				return result;
			}
		}

		// Token: 0x020002D4 RID: 724
		internal enum eFormAControlExistsOnStatus
		{
			// Token: 0x040008EB RID: 2283
			Unknown,
			// Token: 0x040008EC RID: 2284
			HasSingleFormType,
			// Token: 0x040008ED RID: 2285
			HasMultipleFormTypes,
			// Token: 0x040008EE RID: 2286
			HasNoForms
		}
	}
}
