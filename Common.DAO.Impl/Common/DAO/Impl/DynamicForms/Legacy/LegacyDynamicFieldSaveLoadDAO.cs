using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.DynamicForms.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;

namespace TechnoPro.Common.DAO.Impl.DynamicForms.Legacy
{
	// Token: 0x020000EC RID: 236
	public class LegacyDynamicFieldSaveLoadDAO : ILegacyDynamicFieldSaveLoadDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public LegacyDynamicFieldSaveLoadDAO()
		{
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00046598 File Offset: 0x00044798
		public LegacyDynamicFieldSaveLoadDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x000465AA File Offset: 0x000447AA
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x000465B2 File Offset: 0x000447B2
		public OperationContext OpContext { get; set; }

		// Token: 0x060006BB RID: 1723 RVA: 0x000465BB File Offset: 0x000447BB
		public void LogDataChange(bool deleteOldLogData, int screenNum, int studentPid)
		{
			this.LogDataChange(deleteOldLogData, screenNum, studentPid, this.OpContext.WhoAmI);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x000465D4 File Offset: 0x000447D4
		public IList<LegacySaveDataResult> SaveLegacyDataPerStudent(IList<LegacyDynamicDataRowSaveData> todoList, string tableName, bool tablesStoreScreenNum, bool tablesHaveArchiveSets)
		{
			string text = tablesHaveArchiveSets ? "" : "--";
			string sql;
			if (tablesStoreScreenNum)
			{
				sql = string.Concat(new string[]
				{
					"IF EXISTS(SELECT dataid FROM ",
					tableName,
					" WHERE personid=@personid AND controlid=@controlid)\r\nBEGIN\r\n    ",
					text,
					"INSERT INTO ",
					tableName,
					"archive (dateentered,whoentered,wasdeleted,personid,controlid,oldcontrolvalue) SELECT getdate(),@whoami,1,personid,controlid,controlvalue FROM ",
					tableName,
					" WHERE personid=@personid AND controlid=@controlid\r\n    UPDATE ",
					tableName,
					" SET controlvalue=@controlvalue WHERE personid=@personid AND controlid=@controlid\r\nEND\r\nELSE\r\nBEGIN\r\n    ",
					text,
					"INSERT INTO ",
					tableName,
					"archive (dateentered,whoentered,wasdeleted,personid,controlid,oldcontrolvalue) VALUES (getdate(),@whoami,0,@personid,@controlid,NULL)\r\n    INSERT INTO ",
					tableName,
					" (screennum,personid,controlid,controlvalue) VALUES (@screennum,@personid,@controlid,@controlvalue)\r\nEND"
				});
			}
			else
			{
				sql = string.Concat(new string[]
				{
					"IF EXISTS(SELECT dataid FROM ",
					tableName,
					" WHERE personid=@personid AND controlid=@controlid)\r\nBEGIN\r\n    ",
					text,
					"INSERT INTO ",
					tableName,
					"archive (dateentered,whoentered,wasdeleted,personid,controlid,oldcontrolvalue) SELECT getdate(),@whoami,1,personid,controlid,controlvalue FROM ",
					tableName,
					" WHERE personid=@personid AND controlid=@controlid\r\n    UPDATE ",
					tableName,
					" SET controlvalue=@controlvalue WHERE personid=@personid AND controlid=@controlid\r\nEND\r\nELSE\r\nBEGIN\r\n    ",
					text,
					"INSERT INTO ",
					tableName,
					"archive (dateentered,whoentered,wasdeleted,personid,controlid,oldcontrolvalue) VALUES (getdate(),@whoami,0,@personid,@controlid,NULL)\r\n    INSERT INTO ",
					tableName,
					" (personid,controlid,controlvalue) VALUES (@personid,@controlid,@controlvalue)\r\nEND"
				});
			}
			string sql2 = string.Concat(new string[]
			{
				text,
				"INSERT INTO ",
				tableName,
				"archive (dateentered,whoentered,wasdeleted,personid,controlid,oldcontrolvalue) SELECT getdate(),@whoami,1,personid,controlid,controlvalue FROM ",
				tableName,
				" WHERE personid=@personid AND controlid=@controlid\r\nDELETE FROM ",
				tableName,
				" WHERE personid=@personid AND controlid=@controlid"
			});
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			List<LegacySaveDataResult> list = new List<LegacySaveDataResult>();
			foreach (LegacyDynamicDataRowSaveData legacyDynamicDataRowSaveData in todoList)
			{
				switch (legacyDynamicDataRowSaveData.RowState)
				{
				case eLegacyDynamicDataRowState.Added:
				{
					List<DbParameter> list2 = new List<DbParameter>();
					if (tablesStoreScreenNum)
					{
						list2.Add(databaseLayer.GetParameter("@screennum", DbType.Int32, legacyDynamicDataRowSaveData.ScreenNum));
					}
					list2.Add(databaseLayer.GetParameter("@personid", DbType.Int32, legacyDynamicDataRowSaveData.PersonId));
					list2.Add(databaseLayer.GetParameter("@controlid", DbType.Int32, legacyDynamicDataRowSaveData.ControlId));
					DbParameter controlValueParameter = this.GetControlValueParameter(databaseLayer, legacyDynamicDataRowSaveData);
					bool flag = controlValueParameter != null;
					if (flag)
					{
						list2.Add(controlValueParameter);
					}
					list2.Add(databaseLayer.GetParameter("@whoami", DbType.Int32, legacyDynamicDataRowSaveData.WhoAmI));
					list.Add(this.ExecuteNonQuery(databaseLayer, sql, list2.ToArray(), legacyDynamicDataRowSaveData.PersonId, legacyDynamicDataRowSaveData.ControlId));
					break;
				}
				case eLegacyDynamicDataRowState.Deleted:
				{
					DbParameter[] parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@screennum", DbType.Int32, legacyDynamicDataRowSaveData.ScreenNum),
						databaseLayer.GetParameter("@personid", DbType.Int32, legacyDynamicDataRowSaveData.PersonId),
						databaseLayer.GetParameter("@controlid", DbType.Int32, legacyDynamicDataRowSaveData.ControlId),
						databaseLayer.GetParameter("@whoami", DbType.Int32, legacyDynamicDataRowSaveData.WhoAmI)
					};
					list.Add(this.ExecuteNonQuery(databaseLayer, sql2, parameters, legacyDynamicDataRowSaveData.PersonId, legacyDynamicDataRowSaveData.ControlId));
					break;
				}
				case eLegacyDynamicDataRowState.Modified:
				{
					List<DbParameter> list3 = new List<DbParameter>();
					DbParameter controlValueParameter2 = this.GetControlValueParameter(databaseLayer, legacyDynamicDataRowSaveData);
					bool flag2 = controlValueParameter2 != null;
					if (flag2)
					{
						list3.Add(controlValueParameter2);
					}
					list3.Add(databaseLayer.GetParameter("@personid", DbType.Int32, legacyDynamicDataRowSaveData.PersonId));
					list3.Add(databaseLayer.GetParameter("@controlid", DbType.Int32, legacyDynamicDataRowSaveData.ControlId));
					list3.Add(databaseLayer.GetParameter("@whoami", DbType.Int32, legacyDynamicDataRowSaveData.WhoAmI));
					if (tablesStoreScreenNum)
					{
						list3.Add(databaseLayer.GetParameter("@screennum", DbType.Int32, legacyDynamicDataRowSaveData.ScreenNum));
					}
					list.Add(this.ExecuteNonQuery(databaseLayer, sql, list3.ToArray(), legacyDynamicDataRowSaveData.PersonId, legacyDynamicDataRowSaveData.ControlId));
					break;
				}
				}
			}
			return list;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00046A24 File Offset: 0x00044C24
		public void UpdateStudentFileUploadStatusMarkers(int cid, IDictionary<int, bool> pidsWithHasAtLeastOneFileOpen)
		{
			LegacyDynamicFieldSaveLoadDAO.StudentFileUploadMarkerJob studentFileUploadMarkerJob = this.SetupStudentFileUploadMarkerJob(pidsWithHasAtLeastOneFileOpen);
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, cid),
				databaseLayer.GetParameter("@pidsWithOpen", DbType.String, string.Join<int>(",", studentFileUploadMarkerJob.PidsWithOpen)),
				databaseLayer.GetParameter("@pidsWithNoOpen", DbType.String, string.Join<int>(",", studentFileUploadMarkerJob.PidsWithClosed))
			};
			databaseLayer.ExecuteNonQuery(studentFileUploadMarkerJob.Sql, parameters);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00046AB8 File Offset: 0x00044CB8
		private LegacyDynamicFieldSaveLoadDAO.StudentFileUploadMarkerJob SetupStudentFileUploadMarkerJob(IDictionary<int, bool> pidsWithHasAtLeastOneFileOpen)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			foreach (KeyValuePair<int, bool> keyValuePair in pidsWithHasAtLeastOneFileOpen)
			{
				bool value = keyValuePair.Value;
				if (value)
				{
					list.Add(keyValuePair.Key);
				}
				else
				{
					list2.Add(keyValuePair.Key);
				}
			}
			return new LegacyDynamicFieldSaveLoadDAO.StudentFileUploadMarkerJob
			{
				PidsWithClosed = list2,
				PidsWithOpen = list,
				Sql = "SELECT orderid AS personid INTO #tOpen FROM splitorderids(@pidsWithOpen,',')\r\nSELECT orderid AS personid INTO #tClosed FROM splitorderids(@pidsWithNoOpen,',')\r\n\r\nUPDATE OtherInfoPsFileUpload SET IsAtLeastOneFileStatusOpen=0,LastUpdated=getdate() WHERE FK_personid IN (SELECT personid AS FK_personid FROM #tClosed)\r\nUPDATE OtherInfoPsFileUpload SET IsAtLeastOneFileStatusOpen=1,LastUpdated=getdate() WHERE FK_personid IN (SELECT personid AS FK_personid FROM #tOpen)\r\n\r\nINSERT INTO OtherInfoPsFileUpload (FK_PersonId,IsAtLeastOneFileStatusOpen,LastUpdated)\r\n    SELECT #tClosed.personid,0,getdate() FROM #tClosed WHERE NOT #tClosed.personid IN (SELECT FK_personid AS personid FROM OtherInfoPsFileUpload)\r\n\r\nINSERT INTO OtherInfoPsFileUpload (FK_PersonId,IsAtLeastOneFileStatusOpen,LastUpdated)\r\n    SELECT #tOpen.personid,1,getdate() FROM #tOpen WHERE NOT #tOpen.personid IN (SELECT FK_personid AS personid FROM OtherInfoPsFileUpload)\r\n\r\nDROP TABLE #tOpen\r\nDROP TABLE #tClosed"
			};
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00046B5C File Offset: 0x00044D5C
		[DebuggerStepThrough]
		public Task UpdateStudentFileUploadStatusMarkersAsync(int cid, IDictionary<int, bool> pidsWithHasAtLeastOneFileOpen)
		{
			LegacyDynamicFieldSaveLoadDAO.<UpdateStudentFileUploadStatusMarkersAsync>d__11 <UpdateStudentFileUploadStatusMarkersAsync>d__ = new LegacyDynamicFieldSaveLoadDAO.<UpdateStudentFileUploadStatusMarkersAsync>d__11();
			<UpdateStudentFileUploadStatusMarkersAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateStudentFileUploadStatusMarkersAsync>d__.<>4__this = this;
			<UpdateStudentFileUploadStatusMarkersAsync>d__.cid = cid;
			<UpdateStudentFileUploadStatusMarkersAsync>d__.pidsWithHasAtLeastOneFileOpen = pidsWithHasAtLeastOneFileOpen;
			<UpdateStudentFileUploadStatusMarkersAsync>d__.<>1__state = -1;
			<UpdateStudentFileUploadStatusMarkersAsync>d__.<>t__builder.Start<LegacyDynamicFieldSaveLoadDAO.<UpdateStudentFileUploadStatusMarkersAsync>d__11>(ref <UpdateStudentFileUploadStatusMarkersAsync>d__);
			return <UpdateStudentFileUploadStatusMarkersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00046BB0 File Offset: 0x00044DB0
		public DynamicDataContext LoadDataContext(eDynamicFormType formType, int dataId, int controlId)
		{
			DynamicFormTypeAttribute attribute = formType.GetAttribute<DynamicFormTypeAttribute>();
			string text = "imageinfo" + attribute.TablePostFix;
			string databaseColumnName = attribute.PrimaryContextId.GetAttribute<DynamicDataContextColumnNameAttribute>().DatabaseColumnName;
			string databaseColumnName2 = attribute.SecondaryContextId.GetAttribute<DynamicDataContextColumnNameAttribute>().DatabaseColumnName;
			string[] value = (from g in new string[]
			{
				"dataid",
				"controlid",
				databaseColumnName,
				databaseColumnName2
			}
			where !string.IsNullOrEmpty(g)
			select g).ToArray<string>();
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			string query = string.Concat(new string[]
			{
				"SELECT ",
				string.Join(",", value),
				" FROM ",
				text,
				" WHERE dataid=@dataid AND controlid=@cid"
			});
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@dataid", DbType.Int32, dataId),
				databaseLayer.GetParameter("@cid", DbType.Int32, controlId)
			};
			DynamicDataContext result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(query, parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = new DynamicDataContext
					{
						PrimaryId = (string.IsNullOrEmpty(databaseColumnName) ? 0 : ((dataReader[databaseColumnName] is DBNull) ? 0 : ((int)dataReader[databaseColumnName]))),
						SecondaryId = (string.IsNullOrEmpty(databaseColumnName2) ? 0 : ((dataReader[databaseColumnName2] is DBNull) ? 0 : ((int)dataReader[databaseColumnName2])))
					};
				}
			}
			return result;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00046D8C File Offset: 0x00044F8C
		private DbParameter GetControlValueParameter(DatabaseLayer db, LegacyDynamicDataRowSaveData item)
		{
			DbParameter result;
			switch (item.ControlValueType)
			{
			case eLegacyDynamicDataType.Int:
				result = db.GetParameter("@controlvalue", DbType.Int32, (item.ControlValue == null || item.ControlValue is DBNull) ? DBNull.Value : ((int)item.ControlValue));
				break;
			case eLegacyDynamicDataType.Binary:
				result = db.GetParameter("@controlvalue", DbType.Binary, (item.ControlValue == null || item.ControlValue is DBNull) ? DBNull.Value : ((byte[])item.ControlValue));
				break;
			case eLegacyDynamicDataType.DateTime:
				result = db.GetParameter("@controlvalue", DbType.DateTime, (item.ControlValue == null || item.ControlValue is DBNull) ? DBNull.Value : ((DateTime)item.ControlValue));
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00046E70 File Offset: 0x00045070
		private LegacySaveDataResult ExecuteNonQuery(DatabaseLayer db, string sql, DbParameter[] parameters, int pid, int cid)
		{
			LegacySaveDataResult result;
			try
			{
				db.ExecuteNonQuery(sql, parameters);
				result = new LegacySaveDataResult
				{
					PersonId = pid,
					ControlId = cid
				};
			}
			catch (Exception exception)
			{
				result = new LegacySaveDataResult
				{
					PersonId = pid,
					ControlId = cid,
					Exception = exception
				};
			}
			return result;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00046ED8 File Offset: 0x000450D8
		private void LogDataChange(bool deleteOldLogData, int screenNum, int studentPid, int whoModifiedPid)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
				databaseLayer.GetParameter("@personid", DbType.Int32, studentPid),
				databaseLayer.GetParameter("@datemodified", DbType.DateTime, DateTime.Now),
				databaseLayer.GetParameter("@whomodified", DbType.Int32, whoModifiedPid)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO screendata (screennum,personid,datemodified,whomodified) VALUES (@screennum,@personid,@datemodified,@whomodified)", parameters);
			object[] yearStartEnd = this.GetYearStartEnd(databaseLayer);
			DateTime dateTime = new DateTime(((yearStartEnd != null) ? ((DateTime)yearStartEnd[0]) : DateTime.Now.Date).Year, 1, 1);
			string text = "DELETE FROM @archive WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum) AND dateentered=@dateentered".Replace("@archive", "archive_otherinfops");
			parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPid),
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
				databaseLayer.GetParameter("@dateentered", DbType.DateTime, dateTime)
			};
			string text2;
			this.ExecuteQuery(databaseLayer, text, parameters, out text2);
			text = "DELETE FROM @archive WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum) AND dateentered=@dateentered".Replace("@archive", "archive_maininfops");
			parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPid),
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
				databaseLayer.GetParameter("@dateentered", DbType.DateTime, dateTime)
			};
			this.ExecuteQuery(databaseLayer, text, parameters, out text2);
			text = "DELETE FROM @archive WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum) AND dateentered=@dateentered".Replace("@archive", "archive_datetimeinfops");
			parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPid),
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
				databaseLayer.GetParameter("@dateentered", DbType.DateTime, dateTime)
			};
			this.ExecuteQuery(databaseLayer, text, parameters, out text2);
			text = "INSERT INTO @archive (personid,controlid,controlvalue,dateentered,whoentered) SELECT personid,controlid,controlvalue,@dateentered,@whoentered FROM @archive2 WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)";
			string sql = text.Replace("@archive2", "otherinfops").Replace("@archive", "archive_otherinfops");
			parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPid),
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
				databaseLayer.GetParameter("@dateentered", DbType.DateTime, dateTime),
				databaseLayer.GetParameter("@whoentered", DbType.Int32, whoModifiedPid)
			};
			this.ExecuteQuery(databaseLayer, sql, parameters, out text2);
			string sql2 = text.Replace("@archive2", "maininfops").Replace("@archive", "archive_maininfops");
			parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPid),
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
				databaseLayer.GetParameter("@dateentered", DbType.DateTime, dateTime),
				databaseLayer.GetParameter("@whoentered", DbType.Int32, whoModifiedPid)
			};
			this.ExecuteQuery(databaseLayer, sql2, parameters, out text2);
			string sql3 = text.Replace("@archive2", "datetimeinfops").Replace("@archive", "archive_datetimeinfops");
			parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPid),
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
				databaseLayer.GetParameter("@dateentered", DbType.DateTime, dateTime),
				databaseLayer.GetParameter("@whoentered", DbType.Int32, whoModifiedPid)
			};
			this.ExecuteQuery(databaseLayer, sql3, parameters, out text2);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00047294 File Offset: 0x00045494
		private void ExecuteQuery(DatabaseLayer db, string sql, DbParameter[] parameters, out string ex)
		{
			try
			{
				db.ExecuteNonQuery(sql, parameters);
				ex = null;
			}
			catch (Exception ex2)
			{
				ex = ex2.ToString();
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000472D4 File Offset: 0x000454D4
		private object[] GetYearStartEnd(DatabaseLayer db)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				db.GetParameter("@schoolyearcode", DbType.Int32, 0)
			};
			DataTable dataTable = db.ExecuteQuery("SELECT startmonth,startday,endmonth,endday,numyearsbetween FROM dateranges WHERE usecode=@schoolyearcode", parameters);
			bool flag = dataTable.Rows.Count <= 0;
			object[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DataRow dataRow = dataTable.Rows[0];
				int month = (int)dataRow[0];
				int day = (int)dataRow[1];
				int num = (int)dataRow[2];
				int day2 = (int)dataRow[3];
				int num2 = (int)dataRow[4];
				DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
				DateTime dateTime = new DateTime(DateTime.Now.Year, month, num);
				DateTime dateTime2 = (t < dateTime) ? new DateTime(DateTime.Now.Year - 1, month, day) : dateTime;
				DateTime dateTime3 = new DateTime(dateTime2.Year + num2, num, day2);
				result = new object[]
				{
					dateTime2,
					dateTime3
				};
			}
			return result;
		}

		// Token: 0x02000251 RID: 593
		internal class StudentFileUploadMarkerJob
		{
			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0008928C File Offset: 0x0008748C
			// (set) Token: 0x06000E2F RID: 3631 RVA: 0x00089294 File Offset: 0x00087494
			public string Sql { get; set; }

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0008929D File Offset: 0x0008749D
			// (set) Token: 0x06000E31 RID: 3633 RVA: 0x000892A5 File Offset: 0x000874A5
			public IList<int> PidsWithOpen { get; set; }

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000E32 RID: 3634 RVA: 0x000892AE File Offset: 0x000874AE
			// (set) Token: 0x06000E33 RID: 3635 RVA: 0x000892B6 File Offset: 0x000874B6
			public IList<int> PidsWithClosed { get; set; }
		}
	}
}
