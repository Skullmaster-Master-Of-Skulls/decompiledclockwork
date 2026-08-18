using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x0200012B RID: 299
	public class AppointmentTypeDAO : IAppointmentTypeDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00057EC6 File Offset: 0x000560C6
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x00057ECE File Offset: 0x000560CE
		public OperationContext OpContext { get; set; }

		// Token: 0x0600089A RID: 2202 RVA: 0x00057ED7 File Offset: 0x000560D7
		public AppointmentTypeDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00057F08 File Offset: 0x00056108
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x00057F10 File Offset: 0x00056110
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x0600089D RID: 2205 RVA: 0x00057F1C File Offset: 0x0005611C
		private static bool ReaderContainsColumn(IDataReader reader, string colName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				bool flag = reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00057F5C File Offset: 0x0005615C
		private AppTypeWithExtendedInfo GetAppTypeWithExtendedInfoFromRecord(IDataReader record)
		{
			AppTypeWithExtendedInfo appTypeFromReader = AppointmentTypeDAO.GetAppTypeFromReader2<AppTypeWithExtendedInfo>("", record);
			bool flag = appTypeFromReader == null;
			AppTypeWithExtendedInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				appTypeFromReader.IsBackground = (!(record["isbackground"] is DBNull) && Convert.ToBoolean(record["isbackground"]));
				appTypeFromReader.DefaultOverrideColourArgb = ((record["defaultOverrideColour"] is DBNull) ? 0 : ((int)record["defaultOverrideColour"]));
				appTypeFromReader.DefaultIconIndex = ((record["defaulticon"] is DBNull) ? 0 : ((int)record["defaulticon"]));
				appTypeFromReader.IconIndex = ((record["iconindex"] is DBNull) ? 0 : ((int)record["iconindex"]));
				appTypeFromReader.ShowInHighlights = (!(record["ShowInHighlights"] is DBNull) && Convert.ToBoolean(record["ShowInHighlights"]));
				appTypeFromReader.PerJustAppScreenNum = ((record["perJustAppScreenNum"] is DBNull) ? 0 : ((int)record["perJustAppScreenNum"]));
				appTypeFromReader.PerAppScreenNumsForTabs = AppointmentTypeDAO.ParseIntList((record["perAppScreenNumsForTabs"] is DBNull) ? "" : record["perAppScreenNumsForTabs"].ToString());
				appTypeFromReader.ClientGroupIds = AppointmentTypeDAO.ParseIntList((record["clientGroupIds"] is DBNull) ? "" : record["clientGroupIds"].ToString());
				appTypeFromReader.RequiresRoom = (record["requiresroom"] != DBNull.Value && Convert.ToBoolean(record["requiresroom"]));
				result = appTypeFromReader;
			}
			return result;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00058128 File Offset: 0x00056328
		private static IList<int> ParseIntList(string intList)
		{
			int num;
			return (from g in (intList ?? "").Trim().Split(new char[]
			{
				','
			})
			select g.Trim() into h
			where h.Length > 0
			select h into m
			select int.TryParse(m, out num) ? num : 0 into n
			where n > 0
			select n).Distinct<int>().ToList<int>();
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000581E8 File Offset: 0x000563E8
		internal static AppType GetAppTypeFromReader(string colPrefix, IDataReader record)
		{
			return AppointmentTypeDAO.GetAppTypeFromReader2<AppType>(colPrefix, record);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00058204 File Offset: 0x00056404
		internal static T GetAppTypeFromReader2<T>(string colPrefix, IDataReader record) where T : AppType
		{
			int num = (record[colPrefix + "apptypeid"] == DBNull.Value) ? 0 : ((int)record[colPrefix + "apptypeid"]);
			bool flag = num < 1;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				bool flag2 = AppointmentTypeDAO.ReaderContainsColumn(record, "iscourse");
				bool isTestOrExam;
				if (flag2)
				{
					object obj = record["iscourse"];
					isTestOrExam = (obj != DBNull.Value && Convert.ToBoolean(obj));
				}
				else
				{
					isTestOrExam = false;
				}
				bool flag3 = AppointmentTypeDAO.ReaderContainsColumn(record, "isworkshop");
				bool isWorkshop;
				if (flag3)
				{
					object obj2 = record["isworkshop"];
					isWorkshop = (obj2 != DBNull.Value && Convert.ToBoolean(obj2));
				}
				else
				{
					isWorkshop = false;
				}
				string text = colPrefix + "defaultcolour";
				T t = Activator.CreateInstance<T>();
				t.AppTypeId = num;
				t.Description = ((record[colPrefix + "apptypedescription"] == DBNull.Value) ? "" : ((string)record[colPrefix + "apptypedescription"]));
				t.Group = AppointmentTypeDAO.GetAppTypeGroupFromReader(colPrefix, record);
				t.DefaultColourArgb = (AppointmentTypeDAO.ReaderContainsColumn(record, text) ? ((record[text] == DBNull.Value) ? 0 : ((int)record[text])) : 0);
				t.IsTestOrExam = isTestOrExam;
				t.IsWorkshop = isWorkshop;
				bool flag4 = AppointmentTypeDAO.ReaderContainsColumn(record, "apptypeisactive");
				if (flag4)
				{
					t.IsActive = new bool?(!(record["apptypeisactive"] is DBNull) && Convert.ToBoolean(record["apptypeisactive"]));
				}
				result = t;
			}
			return result;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x000583E8 File Offset: 0x000565E8
		private static AppTypeGroup GetAppTypeGroupFromReader(string colPrefix, IDataReader reader)
		{
			string name = colPrefix + "appointmenttypegroupid";
			string name2 = colPrefix + "apptypegrouptitle";
			string text = colPrefix + "gidstr";
			int num = (reader[name] is DBNull) ? 0 : ((int)reader[name]);
			bool flag = num < 1;
			AppTypeGroup result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string description = (reader[name2] is DBNull) ? "" : ((string)reader[name2]);
				string text2 = AppointmentTypeDAO.ReaderContainsColumn(reader, text) ? reader[text].ToString() : "";
				int clientGroupId;
				bool flag2 = text2.Length < 1 || !int.TryParse(text2, out clientGroupId);
				if (flag2)
				{
					clientGroupId = 0;
				}
				result = new AppTypeGroup
				{
					AppointmentTypeGroupId = num,
					Description = description,
					ClientGroupId = clientGroupId
				};
			}
			return result;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x000584D4 File Offset: 0x000566D4
		public IList<AppType> LoadOrphanAppTypes()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<AppType> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,-1 AS appointmenttypegroupid,CAST(NULL AS varchar(max)) AS apptypegrouptitle,CAST(NULL AS varchar(max)) AS gidstr,at.isactive AS apptypeisactive\r\nFROM    appointmenttypes at \r\nWHERE   at.appointmentTypeGroupID IS NULL OR at.appointmentTypeGroupID<1\r\nORDER BY at.description"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppType> list = new List<AppType>();
					while (dataReader.Read())
					{
						list.Add(AppointmentTypeDAO.GetAppTypeFromReader("", dataReader));
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00058560 File Offset: 0x00056760
		public IList<int> GetAppointmentTypeAssociatedPerAppScreenNums(int AppTypeId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, AppTypeId)
			};
			IList<int> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT perappscreennumsfortabs FROM appointmenttypes WHERE apptypeid=@apptypeid", parameters))
			{
				List<int> list = new List<int>();
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					string text = dataReader["perappscreennumsfortabs"].ToString();
					bool flag2 = text.Length > 0;
					if (flag2)
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						foreach (string s in array)
						{
							int item;
							bool flag3 = int.TryParse(s, out item) && !list.Contains(item);
							if (flag3)
							{
								list.Add(item);
							}
						}
					}
					result = list;
				}
				else
				{
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00058668 File Offset: 0x00056868
		public List<AppType> LoadAllAppTypes()
		{
			List<AppType> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,at.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr\r\nFROM    appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE at.isactive=1\r\nORDER BY atg.title,at.description"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppType> list = new List<AppType>();
					while (dataReader.Read())
					{
						AppType appTypeFromReader = AppointmentTypeDAO.GetAppTypeFromReader("", dataReader);
						bool flag2 = appTypeFromReader != null;
						if (flag2)
						{
							list.Add(appTypeFromReader);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x000586EC File Offset: 0x000568EC
		[DebuggerStepThrough]
		public Task<List<AppType>> LoadAllAppTypesAsync()
		{
			AppointmentTypeDAO.<LoadAllAppTypesAsync>d__18 <LoadAllAppTypesAsync>d__ = new AppointmentTypeDAO.<LoadAllAppTypesAsync>d__18();
			<LoadAllAppTypesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<AppType>>.Create();
			<LoadAllAppTypesAsync>d__.<>4__this = this;
			<LoadAllAppTypesAsync>d__.<>1__state = -1;
			<LoadAllAppTypesAsync>d__.<>t__builder.Start<AppointmentTypeDAO.<LoadAllAppTypesAsync>d__18>(ref <LoadAllAppTypesAsync>d__);
			return <LoadAllAppTypesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00058730 File Offset: 0x00056930
		public IList<AppTypeGroupWithAppTypes> LoadAllAppTypeGroups(bool includeInactive = false)
		{
			return this.LoadAllAppTypeGroups(includeInactive ? "SELECT at.apptypeid,at.[description] AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,atg.appointmenttypegroupid,atg.title AS apptypegrouptitle,at.isactive AS apptypeisactive,atg.[description] AS gidstr\r\nFROM    appointmenttypegroups atg LEFT JOIN appointmenttypes at ON at.appointmenttypegroupid=atg.appointmenttypegroupid\r\nORDER BY atg.title,at.description" : "SELECT at.apptypeid,at.[description] AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,atg.appointmenttypegroupid,atg.title AS apptypegrouptitle,at.isactive AS apptypeisactive,atg.[description] AS gidstr\r\nFROM    appointmenttypegroups atg LEFT JOIN appointmenttypes at ON at.appointmenttypegroupid=atg.appointmenttypegroupid\r\nWHERE at.isactive IS NULL OR at.isactive=1\r\nORDER BY atg.title,at.description");
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00058758 File Offset: 0x00056958
		private IList<AppTypeGroupWithAppTypes> LoadAllAppTypeGroups(string query)
		{
			IList<AppTypeGroupWithAppTypes> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(query))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppTypeGroupWithAppTypes> list = new List<AppTypeGroupWithAppTypes>();
					AppTypeGroupWithAppTypes appTypeGroupWithAppTypes = null;
					while (dataReader.Read())
					{
						int num = (int)dataReader["appointmenttypegroupid"];
						bool flag2 = appTypeGroupWithAppTypes == null || appTypeGroupWithAppTypes.Group.AppointmentTypeGroupId != num;
						if (flag2)
						{
							AppTypeGroup appTypeGroupFromReader = AppointmentTypeDAO.GetAppTypeGroupFromReader("", dataReader);
							appTypeGroupWithAppTypes = new AppTypeGroupWithAppTypes
							{
								Group = appTypeGroupFromReader,
								SubAppTypes = new List<AppType>()
							};
							list.Add(appTypeGroupWithAppTypes);
						}
						AppType appTypeFromReader = AppointmentTypeDAO.GetAppTypeFromReader("", dataReader);
						bool flag3 = appTypeFromReader != null;
						if (flag3)
						{
							appTypeGroupWithAppTypes.SubAppTypes.Add(appTypeFromReader);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0005884C File Offset: 0x00056A4C
		public AppTypeGroupWithAppTypes LoadAppTypeGroupWithAppTypesById(int AppointmentTypeGroupId, bool IncludeInactiveAppTypes = false)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appointmenttypegroupid", DbType.Int32, AppointmentTypeGroupId),
				this.DatabaseManager.GetParameter("@includeinactiveapptypes", DbType.Boolean, IncludeInactiveAppTypes)
			};
			AppTypeGroupWithAppTypes result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT at.apptypeid,at.[description] AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,atg.appointmenttypegroupid,atg.title AS apptypegrouptitle,at.isactive AS apptypeisactive,atg.[description] AS gidstr\r\nFROM    appointmenttypegroups atg LEFT JOIN appointmenttypes at ON at.appointmenttypegroupid=atg.appointmenttypegroupid\r\nWHERE   (@includeinactiveapptypes=1 OR (at.isactive IS NULL OR at.isactive=1))\r\n        AND atg.appointmenttypegroupid=@appointmenttypegroupid\r\nORDER BY atg.title,at.description", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppTypeGroupWithAppTypes> list = new List<AppTypeGroupWithAppTypes>();
					AppTypeGroupWithAppTypes appTypeGroupWithAppTypes = null;
					while (dataReader.Read())
					{
						int num = (int)dataReader["appointmenttypegroupid"];
						bool flag2 = appTypeGroupWithAppTypes == null || appTypeGroupWithAppTypes.Group.AppointmentTypeGroupId != num;
						if (flag2)
						{
							AppTypeGroup appTypeGroupFromReader = AppointmentTypeDAO.GetAppTypeGroupFromReader("", dataReader);
							appTypeGroupWithAppTypes = new AppTypeGroupWithAppTypes
							{
								Group = appTypeGroupFromReader,
								SubAppTypes = new List<AppType>()
							};
							list.Add(appTypeGroupWithAppTypes);
						}
						AppType appTypeFromReader = AppointmentTypeDAO.GetAppTypeFromReader("", dataReader);
						bool flag3 = appTypeFromReader != null;
						if (flag3)
						{
							appTypeGroupWithAppTypes.SubAppTypes.Add(appTypeFromReader);
						}
					}
					result = ((list.Count > 0) ? list[0] : null);
				}
			}
			return result;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00058994 File Offset: 0x00056B94
		public AppTypeGroup LoadAppTypeGroupById(int AppointmentTypeGroupId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appointmenttypegroupid", DbType.Int32, AppointmentTypeGroupId)
			};
			AppTypeGroup result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT atg.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr\r\nFROM    appointmenttypegroups atg\r\nWHERE atg.appointmenttypegroupid=@appointmenttypegroupid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentTypeDAO.GetAppTypeGroupFromReader("", dataReader);
				}
			}
			return result;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00058A18 File Offset: 0x00056C18
		public void DeleteAppTypeGroup(int AppointmentTypeGroupId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appointmenttypegroupid", DbType.Int32, AppointmentTypeGroupId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmenttypegroups WHERE NOT appointmenttypegroupid IN (SELECT appointmenttypegroupid FROM appointmenttypes) AND appointmenttypegroupid=@appointmenttypegroupid", parameters);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00058A5C File Offset: 0x00056C5C
		public void DeleteAppType(int AppTypeId, int AppTypeIdToReplaceWith)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@apptypeidtoreplace", DbType.Int32, AppTypeId),
				this.DatabaseManager.GetParameter("@apptypeidtokeep", DbType.Int32, AppTypeIdToReplaceWith)
			};
			this.DatabaseManager.ExecuteNonQuery("IF @apptypeidtoreplace > 0 \r\n    UPDATE appointments SET apptypeid=@apptypeidtokeep WHERE apptypeid=@apptypeidtoreplace\r\nDELETE FROM appointmenttypes WHERE apptypeid=@apptypeidtoreplace AND NOT apptypeid IN (SELECT apptypeid FROM appointments)", parameters);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00058ABC File Offset: 0x00056CBC
		public void DisableAppType(int AppTypeId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, AppTypeId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointmenttypes SET isactive=0 WHERE apptypeid=@apptypeid", parameters);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00058B00 File Offset: 0x00056D00
		public int CreateAppTypeGroup(AppTypeGroup AppTypeGroup)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@appointmenttypegroupid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@title", DbType.String, AppTypeGroup.Description ?? "")
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO appointmenttypegroups (title,description) VALUES (@title,''); SET @appointmenttypegroupid=SCOPE_IDENTITY();", array);
			return AppTypeGroup.AppointmentTypeGroupId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00058B90 File Offset: 0x00056D90
		public void UpdateAppTypeGroup(AppTypeGroup AppTypeGroup)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appointmenttypegroupid", DbType.Int32, AppTypeGroup.AppointmentTypeGroupId),
				this.DatabaseManager.GetParameter("@title", DbType.String, AppTypeGroup.Description ?? ""),
				this.DatabaseManager.GetParameter("@gidstr", DbType.String, (AppTypeGroup.ClientGroupId > 0) ? AppTypeGroup.ClientGroupId.ToString() : "")
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointmenttypegroups SET title=@title,description=@gidstr WHERE appointmenttypegroupid=@appointmenttypegroupid", parameters);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00058C30 File Offset: 0x00056E30
		public AppType LoadAppTypeById(int AppTypeId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, AppTypeId)
			};
			AppType result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,at.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr\r\nFROM    appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE at.isactive=1 AND at.apptypeid=@apptypeid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentTypeDAO.GetAppTypeFromReader("", dataReader);
				}
			}
			return result;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00058CB4 File Offset: 0x00056EB4
		public AppType LoadAppTypeByAppointmentId(int appointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId)
			};
			AppType result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,at.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr\r\nFROM    appointments a LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE a.appointmentid=@appid AND at.isactive=1", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentTypeDAO.GetAppTypeFromReader("", dataReader);
				}
			}
			return result;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00058D38 File Offset: 0x00056F38
		public void UpdateAppType(AppType AppType)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, AppType.AppTypeId),
				this.DatabaseManager.GetParameter("@isworkshop", DbType.Boolean, AppType.IsWorkshop),
				this.DatabaseManager.GetParameter("@iscourse", DbType.Boolean, AppType.IsTestOrExam),
				this.DatabaseManager.GetParameter("@description", DbType.String, AppType.Description ?? ""),
				this.DatabaseManager.GetParameter("@defaultcolour", DbType.Int32, AppType.DefaultColourArgb),
				this.DatabaseManager.GetParameter("@appointmenttypegroupid", DbType.Int32, (AppType.Group != null && AppType.Group.AppointmentTypeGroupId > 0) ? AppType.Group.AppointmentTypeGroupId : -1)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointmenttypes SET \r\n    description=@description,defaultcolour=@defaultcolour,isworkshop=@isworkshop,\r\n    iscourse=@iscourse,appointmenttypegroupid=@appointmenttypegroupid\r\nWHERE apptypeid=@apptypeid", parameters);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00058E40 File Offset: 0x00057040
		public int CreateAppType(AppType AppType)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@apptypeid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@isworkshop", DbType.Boolean, AppType.IsWorkshop),
				this.DatabaseManager.GetParameter("@iscourse", DbType.Boolean, AppType.IsTestOrExam),
				this.DatabaseManager.GetParameter("@description", DbType.String, AppType.Description ?? ""),
				this.DatabaseManager.GetParameter("@defaultcolour", DbType.Int32, AppType.DefaultColourArgb),
				this.DatabaseManager.GetParameter("@appointmenttypegroupid", DbType.Int32, (AppType.Group != null && AppType.Group.AppointmentTypeGroupId > 0) ? AppType.Group.AppointmentTypeGroupId : -1)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO appointmenttypes \r\n    (description,defaultcolour,isworkshop,iscourse,appointmenttypegroupid,defaulticon)\r\nVALUES (@description,@defaultcolour,@isworkshop,@iscourse,@appointmenttypegroupid,NULL);\r\nSET @apptypeid=SCOPE_IDENTITY()", array);
			return AppType.AppTypeId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00058F6C File Offset: 0x0005716C
		public int CreateAppTypeWithExtendedInfo(AppTypeWithExtendedInfo AppType)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[16];
			array[0] = databaseLayer.GetOutputParameter("@apptypeid", DbType.Int32, 0);
			array[1] = databaseLayer.GetParameter("@isworkshop", DbType.Boolean, AppType.IsWorkshop);
			array[2] = databaseLayer.GetParameter("@iscourse", DbType.Boolean, AppType.IsTestOrExam);
			array[3] = databaseLayer.GetParameter("@description", DbType.String, AppType.Description ?? "");
			array[4] = databaseLayer.GetParameter("@defaultcolour", DbType.Int32, AppType.DefaultColourArgb);
			array[5] = databaseLayer.GetParameter("@appointmenttypegroupid", DbType.Int32, (AppType.Group != null && AppType.Group.AppointmentTypeGroupId > 0) ? AppType.Group.AppointmentTypeGroupId : -1);
			array[6] = databaseLayer.GetParameter("@isbackground", DbType.Boolean, AppType.IsBackground);
			array[7] = databaseLayer.GetParameter("@defaultoverridecolour", DbType.Int32, AppType.DefaultOverrideColourArgb);
			array[8] = databaseLayer.GetParameter("@defaulticon", DbType.Int32, (AppType.DefaultIconIndex >= 0) ? AppType.DefaultIconIndex : DBNull.Value);
			array[9] = databaseLayer.GetParameter("@showinhighlights", DbType.Boolean, AppType.ShowInHighlights);
			int num = 10;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@perAppScreenNumsForTabs";
			DbType pType = DbType.String;
			object value;
			if (AppType.PerAppScreenNumsForTabs != null)
			{
				value = string.Join(",", (from g in AppType.PerAppScreenNumsForTabs
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			array[11] = databaseLayer.GetParameter("@perjustappscreennum", DbType.Int32, AppType.PerJustAppScreenNum);
			array[12] = databaseLayer.GetParameter("@iconindex", DbType.Int32, AppType.IconIndex);
			int num2 = 13;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@longdescription";
			DbType pType2 = DbType.String;
			object value2;
			if (AppType.ClientGroupIds != null)
			{
				value2 = string.Join(",", (from g in AppType.ClientGroupIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value2 = "";
			}
			array[num2] = databaseLayer3.GetParameter(pName2, pType2, value2);
			array[14] = databaseLayer.GetParameter("@isactive", DbType.Boolean, AppType.IsActive);
			array[15] = databaseLayer.GetParameter("@requiresroom", DbType.Boolean, AppType.RequiresRoom);
			DbParameter[] array2 = array;
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO appointmenttypes \r\n    ([description],defaultcolour,isworkshop,iscourse,appointmenttypegroupid,isbackground,defaultoverridecolour,defaulticon,\r\n\tshowinhighlights,perappscreennumsfortabs,perjustappscreennum,iconindex,longdescription,isactive,requiresroom)\r\nVALUES (@description,@defaultcolour,@isworkshop,@iscourse,@appointmenttypegroupid,@isbackground,@defaultoverridecolour,@defaulticon,\r\n\t@showinhighlights,@perappscreennumsfortabs,@perjustappscreennum,@iconindex,@longdescription,@isactive,@requiresroom);\r\nSET @apptypeid=SCOPE_IDENTITY()", array2);
			return AppType.AppTypeId = ((array2[0].Value is DBNull) ? 0 : ((int)array2[0].Value));
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00059230 File Offset: 0x00057430
		public void UpdateAppTypeWithExtendedInfo(AppTypeWithExtendedInfo AppType)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[16];
			array[0] = databaseLayer.GetParameter("@apptypeid", DbType.Int32, AppType.AppTypeId);
			array[1] = databaseLayer.GetParameter("@isworkshop", DbType.Boolean, AppType.IsWorkshop);
			array[2] = databaseLayer.GetParameter("@iscourse", DbType.Boolean, AppType.IsTestOrExam);
			array[3] = databaseLayer.GetParameter("@description", DbType.String, AppType.Description ?? "");
			array[4] = databaseLayer.GetParameter("@defaultcolour", DbType.Int32, AppType.DefaultColourArgb);
			array[5] = databaseLayer.GetParameter("@appointmenttypegroupid", DbType.Int32, (AppType.Group != null && AppType.Group.AppointmentTypeGroupId > 0) ? AppType.Group.AppointmentTypeGroupId : -1);
			array[6] = databaseLayer.GetParameter("@isbackground", DbType.Boolean, AppType.IsBackground);
			array[7] = databaseLayer.GetParameter("@defaultoverridecolour", DbType.Int32, AppType.DefaultOverrideColourArgb);
			array[8] = databaseLayer.GetParameter("@defaulticon", DbType.Int32, (AppType.DefaultIconIndex >= 0) ? AppType.DefaultIconIndex : DBNull.Value);
			array[9] = databaseLayer.GetParameter("@showinhighlights", DbType.Boolean, AppType.ShowInHighlights);
			int num = 10;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@perAppScreenNumsForTabs";
			DbType pType = DbType.String;
			object value;
			if (AppType.PerAppScreenNumsForTabs != null)
			{
				value = string.Join(",", (from g in AppType.PerAppScreenNumsForTabs
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			array[11] = databaseLayer.GetParameter("@perjustappscreennum", DbType.Int32, AppType.PerJustAppScreenNum);
			array[12] = databaseLayer.GetParameter("@iconindex", DbType.Int32, AppType.IconIndex);
			int num2 = 13;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@longdescription";
			DbType pType2 = DbType.String;
			object value2;
			if (AppType.ClientGroupIds != null)
			{
				value2 = string.Join(",", (from g in AppType.ClientGroupIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value2 = "";
			}
			array[num2] = databaseLayer3.GetParameter(pName2, pType2, value2);
			array[14] = databaseLayer.GetParameter("@isactive", DbType.Boolean, AppType.IsActive);
			array[15] = databaseLayer.GetParameter("@requiresroom", DbType.Boolean, AppType.RequiresRoom);
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("UPDATE appointmenttypes SET \r\n    [description]=@description,defaultcolour=@defaultcolour,isworkshop=@isworkshop,\r\n    iscourse=@iscourse,appointmenttypegroupid=@appointmenttypegroupid,\r\n    isbackground=@isbackground,defaultoverridecolour=@defaultoverridecolour,defaulticon=@defaulticon,\r\n    showinhighlights=@showinhighlights,perappscreennumsfortabs=@perappscreennumsfortabs,\r\n    perjustappscreennum=@perjustappscreennum,iconindex=@iconindex,longdescription=@longdescription,\r\n    isactive=@isactive,requiresroom=@requiresroom\r\nWHERE apptypeid=@apptypeid", parameters);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000594CC File Offset: 0x000576CC
		public List<AppType> LoadAllInactiveAppTypes()
		{
			List<AppType> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,at.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr\r\nFROM    appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE at.isactive=0\r\nORDER BY atg.title,at.description"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppType> list = new List<AppType>();
					while (dataReader.Read())
					{
						AppType appTypeFromReader = AppointmentTypeDAO.GetAppTypeFromReader("", dataReader);
						bool flag2 = appTypeFromReader != null;
						if (flag2)
						{
							list.Add(appTypeFromReader);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00059550 File Offset: 0x00057750
		[DebuggerStepThrough]
		public Task<List<AppType>> LoadAllInactiveAppTypesAsync()
		{
			AppointmentTypeDAO.<LoadAllInactiveAppTypesAsync>d__35 <LoadAllInactiveAppTypesAsync>d__ = new AppointmentTypeDAO.<LoadAllInactiveAppTypesAsync>d__35();
			<LoadAllInactiveAppTypesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<AppType>>.Create();
			<LoadAllInactiveAppTypesAsync>d__.<>4__this = this;
			<LoadAllInactiveAppTypesAsync>d__.<>1__state = -1;
			<LoadAllInactiveAppTypesAsync>d__.<>t__builder.Start<AppointmentTypeDAO.<LoadAllInactiveAppTypesAsync>d__35>(ref <LoadAllInactiveAppTypesAsync>d__);
			return <LoadAllInactiveAppTypesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00059594 File Offset: 0x00057794
		public AppTypeWithExtendedInfo LoadAppTypeWithExtendedInfoIdById(int appTypeId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@apptypeid", DbType.Int32, appTypeId)
			};
			AppTypeWithExtendedInfo result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tat.apptypeid,at.[description] AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse,\r\n\t\tat.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr,\r\n\t\tat.isbackground,at.defaultOverrideColour,at.defaultIcon,at.ShowInHighlights,\r\n\t\tat.perAppScreenNumsForTabs,at.perJustAppScreenNum,at.iconindex,at.longdescription AS clientGroupIds,at.isactive AS apptypeisactive,\r\n        at.requiresroom\r\nFROM    appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE\tat.apptypeid=@apptypeid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetAppTypeWithExtendedInfoFromRecord(dataReader);
				}
			}
			return result;
		}
	}
}
