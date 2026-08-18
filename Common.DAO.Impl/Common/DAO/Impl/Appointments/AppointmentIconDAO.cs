using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DataStructure.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000127 RID: 295
	public class AppointmentIconDAO : IAppointmentIconDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00056941 File Offset: 0x00054B41
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00056949 File Offset: 0x00054B49
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000871 RID: 2161 RVA: 0x00056952 File Offset: 0x00054B52
		public AppointmentIconDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00056983 File Offset: 0x00054B83
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0005698B File Offset: 0x00054B8B
		public OperationContext OpContext { get; set; }

		// Token: 0x06000874 RID: 2164 RVA: 0x00056994 File Offset: 0x00054B94
		internal static AppointmentIcon GetAppointmentIconFromRecord(IDataReader record)
		{
			bool flag = record["appiconid"] == DBNull.Value;
			AppointmentIcon result;
			if (flag)
			{
				result = null;
			}
			else
			{
				AppointmentIcon appointmentIcon = new AppointmentIcon
				{
					AppointmentIconId = (int)record["appiconid"],
					Icon = IconInfoDAO.GetIconInfoFromRecord(record),
					Screen = DynamicFormsDAO.GetDynamicFormBaseFromRecord<DynamicFormBase>(record)
				};
				bool flag2 = appointmentIcon.Icon == null || appointmentIcon.Icon.IconNum < 0;
				if (flag2)
				{
					int num = (record["iconnum"] == DBNull.Value) ? 0 : ((int)record["iconnum"]);
					bool flag3 = num > 0;
					if (flag3)
					{
						bool flag4 = appointmentIcon.Icon == null;
						if (flag4)
						{
							appointmentIcon.Icon = new IconInfo
							{
								IconNum = num
							};
						}
						else
						{
							appointmentIcon.Icon.IconNum = num;
						}
					}
				}
				result = appointmentIcon;
			}
			return result;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00056A84 File Offset: 0x00054C84
		public IList<AppointmentIcon> LoadAppointmentIconsByAppointment(int AppointmentId)
		{
			IList<AppointmentIcon> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ai.appiconid,ai.appointmentid,ai.iconnum,ai.screennum,\r\n            s.typecode,s.[description],s.shorttext,s.isactive,s.showasbutton,\r\n            ii.icontext,ii.iconletteridentifier,ii.appointmenticoninfoid\r\nFROM        appointmenticons ai LEFT JOIN appointmenticoninfo ii ON ii.iconindex=ai.iconnum\r\n            LEFT JOIN screens s ON s.screennum=ai.screennum\r\nWHERE       ai.appointmentid=@appid\r\nORDER BY ai.iconnum", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			}))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppointmentIcon> list = new List<AppointmentIcon>();
					while (dataReader.Read())
					{
						AppointmentIcon appointmentIconFromRecord = AppointmentIconDAO.GetAppointmentIconFromRecord(dataReader);
						bool flag2 = appointmentIconFromRecord != null;
						if (flag2)
						{
							list.Add(appointmentIconFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00056B24 File Offset: 0x00054D24
		public IDictionary<int, IList<AppointmentIcon>> LoadAppointmentIconsByAppointments(int[] appointmentIds)
		{
			int[] array = appointmentIds.Distinct<int>().ToArray<int>();
			IList<Chunk> list = array.BreakdownItemsIntoChunks(2500);
			Dictionary<int, IList<AppointmentIcon>> dictionary = new Dictionary<int, IList<AppointmentIcon>>();
			foreach (Chunk chunk in list)
			{
				this.LoadAppointmentIconInfoToDictionary(dictionary, this.DatabaseManager, array, chunk.Start, chunk.End);
			}
			return dictionary;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00056BAC File Offset: 0x00054DAC
		private void LoadAppointmentIconInfoToDictionary(IDictionary<int, IList<AppointmentIcon>> appIcons, DatabaseLayer db, int[] appIds, int startInd, int endInd)
		{
			int[] rangeByIndices = appIds.GetRangeByIndices(startInd, endInd);
			bool flag = rangeByIndices == null || rangeByIndices.Length < 1;
			if (!flag)
			{
				DbParameter[] array = new DbParameter[1];
				array[0] = db.GetParameter("@appids", DbType.String, string.Join(",", from g in rangeByIndices
				select g.ToString()));
				DbParameter[] parameters = array;
				using (IDataReader dataReader = db.ExecuteQueryReader("SELECT    ai.appiconid,ai.appointmentid,ai.iconnum,ai.screennum,\r\n            s.typecode,s.[description],s.shorttext,s.isactive,s.showasbutton,\r\n            ii.icontext,ii.iconletteridentifier,ii.appointmenticoninfoid\r\nFROM        appointmenticons ai LEFT JOIN appointmenticoninfo ii ON ii.iconindex=ai.iconnum\r\n            LEFT JOIN screens s ON s.screennum=ai.screennum\r\nWHERE       ai.appointmentid IN (SELECT orderid AS appointmentid FROM splitorderids(@appids,','))\r\nORDER BY ai.appointmentid,ai.iconnum", parameters))
				{
					bool flag2 = dataReader == null;
					if (!flag2)
					{
						int num = 0;
						List<AppointmentIcon> list = new List<AppointmentIcon>();
						while (dataReader.Read())
						{
							int num2 = (dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]);
							bool flag3 = num2 < 1;
							if (!flag3)
							{
								AppointmentIcon appointmentIconFromRecord = AppointmentIconDAO.GetAppointmentIconFromRecord(dataReader);
								bool flag4 = appointmentIconFromRecord == null;
								if (!flag4)
								{
									bool flag5 = num2 != num;
									if (flag5)
									{
										bool flag6 = list.Count > 0;
										if (flag6)
										{
											appIcons.Add(num2, list);
										}
										num = num2;
										list = new List<AppointmentIcon>();
									}
									list.Add(appointmentIconFromRecord);
								}
							}
						}
						bool flag7 = num > 0 && list.Count > 0 && !appIcons.ContainsKey(num);
						if (flag7)
						{
							appIcons.Add(num, list);
						}
					}
				}
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00056D34 File Offset: 0x00054F34
		public AppointmentIcon LoadAppointmentIcon(int AppointmentId, int IconNum)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
				this.DatabaseManager.GetParameter("@iconnum", DbType.Int32, IconNum)
			};
			AppointmentIcon result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ai.appiconid,ai.appointmentid,ai.iconnum,ai.screennum,\r\n            s.typecode,s.[description],s.shorttext,s.isactive,s.showasbutton,\r\n            ii.icontext,ii.iconletteridentifier,ii.appointmenticoninfoid\r\nFROM        appointmenticons ai LEFT JOIN appointmenticoninfo ii ON ii.iconindex=ai.iconnum\r\n            LEFT JOIN screens s ON s.screennum=ai.screennum\r\nWHERE       ai.appointmentid=@appid AND ai.iconnum=@iconnum", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentIconDAO.GetAppointmentIconFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00056DD0 File Offset: 0x00054FD0
		public AppointmentIcon LoadAppointmentIconByIconNum(int IconNum)
		{
			bool flag = IconNum < 1;
			AppointmentIcon result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@iconnum", DbType.Int32, IconNum)
				};
				AppointmentIcon appointmentIcon = null;
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ai.appiconid,ai.appointmentid,ai.iconnum,ai.screennum,\r\n            s.typecode,s.[description],s.shorttext,s.isactive,s.showasbutton,\r\n            ii.icontext,ii.iconletteridentifier,ii.appointmenticoninfoid\r\nFROM        appointmenticons ai LEFT JOIN appointmenticoninfo ii ON ii.iconindex=ai.iconnum\r\n            LEFT JOIN screens s ON s.screennum=ai.screennum\r\nWHERE       ai.iconnum=@iconnum", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						appointmentIcon = AppointmentIconDAO.GetAppointmentIconFromRecord(dataReader);
					}
				}
				result = appointmentIcon;
			}
			return result;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00056E64 File Offset: 0x00055064
		public AppointmentIcon LoadAppointmentIcon(int AppointmentIconId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@AppointmentIconId", DbType.Int32, AppointmentIconId)
			};
			AppointmentIcon result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ai.appiconid,ai.appointmentid,ai.iconnum,ai.screennum,\r\n            s.typecode,s.[description],s.shorttext,s.isactive,s.showasbutton,\r\n            ii.icontext,ii.iconletteridentifier,ii.appointmenticoninfoid\r\nFROM        appointmenticons ai LEFT JOIN appointmenticoninfo ii ON ii.iconindex=ai.iconnum\r\n            LEFT JOIN screens s ON s.screennum=ai.screennum\r\nWHERE       ai.appiconid=@AppointmentIconId", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentIconDAO.GetAppointmentIconFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00056EE4 File Offset: 0x000550E4
		public void DeleteAppointmentIconsNotInList(int AppointmentId, IList<int> IconNums, DbTransaction transaction = null)
		{
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId);
			int num = 1;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@iconnums";
			DbType pType = DbType.String;
			object value;
			if (IconNums != null)
			{
				value = string.Join(",", IconNums.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmenticons WHERE appointmentid=@appid \r\n        AND NOT iconnum IN (SELECT orderid AS iconnum FROM splitorderids(@iconnums,','))", parameters);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00056F80 File Offset: 0x00055180
		public int InsertOrUpdateAppointmentIcon(int AppointmentId, AppointmentIcon icon, DbTransaction transaction = null)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@appiconid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@iconnum", DbType.Int32, icon.IconNum),
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
				this.DatabaseManager.GetParameter("@screennum", DbType.Int32, (icon.Screen == null) ? DBNull.Value : icon.Screen.ScreenNum)
			};
			this.DatabaseManager.ExecuteNonQuery("IF EXISTS(SELECT appiconid FROM appointmenticons WHERE appointmentid=@appid AND iconnum=@iconnum)\r\nBEGIN\r\n    UPDATE appointmenticons SET screennum=@screennum WHERE appointmentid=@appid AND iconnum=@iconnum\r\n    SET @appiconid=(SELECT TOP 1 appiconid FROM appointmenticons WHERE appointmentid=@appid AND iconnum=@iconnum)\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO appointmenticons (appointmentid,screennum,iconnum) VALUES (@appid,@screennum,@iconnum)\r\n    SET @appiconid=SCOPE_IDENTITY()\r\nEND", array);
			int num = (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
			icon.AppointmentIconId = num;
			return num;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0005705C File Offset: 0x0005525C
		public void DeleteAppointmentIcon(int AppointmentId, int IconNum, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
				this.DatabaseManager.GetParameter("@iconnum", DbType.Int32, IconNum)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmenticons WHERE appointmentid=@appid AND iconnum=@iconnum", parameters);
		}
	}
}
