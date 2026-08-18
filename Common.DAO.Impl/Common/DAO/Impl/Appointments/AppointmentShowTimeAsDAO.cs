using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x0200012A RID: 298
	public class AppointmentShowTimeAsDAO : IAppointmentShowTimeAsDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x000578E8 File Offset: 0x00055AE8
		public AppointmentShowTimeAsDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x00057918 File Offset: 0x00055B18
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x00057920 File Offset: 0x00055B20
		public OperationContext OpContext { get; set; }

		// Token: 0x06000890 RID: 2192 RVA: 0x0005792C File Offset: 0x00055B2C
		internal static AppShowTimeAsType GetShowTimeAsFromRecord(IDataRecord record)
		{
			bool flag = record == null;
			AppShowTimeAsType result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<string> list = new List<string>();
				for (int i = 0; i < record.FieldCount; i++)
				{
					list.Add(record.GetName(i).ToLower());
				}
				bool flag2 = list.Contains("appointmentshowtimeasid");
				if (flag2)
				{
					bool flag3 = record["appointmentshowtimeasid"] is DBNull;
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = new AppShowTimeAsType
						{
							AppointmentShowTimeAsId = (int)record["appointmentshowtimeasid"],
							AppCode = (int)record["extraiconid"],
							Title = record["showtimeastitle"].ToString(),
							ColourArgB = ((record["showtimeascolour"] is DBNull) ? null : new int?((int)record["showtimeascolour"]))
						};
					}
				}
				else
				{
					bool flag4 = list.Contains("appcode");
					if (flag4)
					{
						bool flag5 = record["appcode"] is DBNull;
						if (flag5)
						{
							result = null;
						}
						else
						{
							result = new AppShowTimeAsType
							{
								AppCode = (int)record["appcode"],
								Title = (list.Contains("showtimeastitle") ? record["showtimeastitle"].ToString() : ""),
								ColourArgB = (list.Contains("showtimeascolour") ? ((record["showtimeascolour"] is DBNull) ? null : new int?((int)record["showtimeascolour"])) : null)
							};
						}
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00057B20 File Offset: 0x00055D20
		public IList<AppShowTimeAsType> LoadAllShowTimeAsTypes()
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT appointmentshowtimeasid,extraiconid,showtimeastitle,showtimeascolour FROM appointmentshowtimeas ORDER BY showtimeastitle"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<AppShowTimeAsType> list = new List<AppShowTimeAsType>();
					while (dataReader.Read())
					{
						AppShowTimeAsType showTimeAsFromRecord = AppointmentShowTimeAsDAO.GetShowTimeAsFromRecord(dataReader);
						bool flag2 = showTimeAsFromRecord != null;
						if (flag2)
						{
							list.Add(showTimeAsFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00057BA4 File Offset: 0x00055DA4
		public AppShowTimeAsType LoadShowTimeAsTypeByAppCode(int AppCode)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appcode", DbType.Int32, AppCode)
			};
			AppShowTimeAsType result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT appointmentshowtimeasid,extraiconid,showtimeastitle,showtimeascolour FROM appointmentshowtimeas WHERE extraiconid=@appcode", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentShowTimeAsDAO.GetShowTimeAsFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00057C24 File Offset: 0x00055E24
		public void DeleteShowTimeAsTypeByAppCode(int AppCode)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appcode", DbType.Int32, AppCode)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmentshowtimeas WHERE extraiconid=@appcode", parameters);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00057C68 File Offset: 0x00055E68
		public void UpdateShowTimeAsType(AppShowTimeAsType ShowTimeAsType)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appcode", DbType.Int32, ShowTimeAsType.AppCode),
				this.DatabaseManager.GetParameter("@title", DbType.String, ShowTimeAsType.Title ?? ""),
				this.DatabaseManager.GetParameter("@colour", DbType.Int32, (ShowTimeAsType.ColourArgB != null) ? ShowTimeAsType.ColourArgB.Value : DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointmentshowtimeas SET showtimeastitle=@title,showtimeascolour=@colour WHERE extraiconid=@appcode", parameters);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00057D14 File Offset: 0x00055F14
		public int CreateShowTimeAsType(AppShowTimeAsType ShowTimeAsType)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@id", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@appcode", DbType.Int32, ShowTimeAsType.AppCode),
				this.DatabaseManager.GetParameter("@title", DbType.String, ShowTimeAsType.Title ?? ""),
				this.DatabaseManager.GetParameter("@colour", DbType.Int32, (ShowTimeAsType.ColourArgB != null) ? ShowTimeAsType.ColourArgB.Value : DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO appointmentshowtimeas (showtimeastitle,showtimeascolour,extraiconid)\r\nVALUES (@title,@colour,@appcode)\r\nSET @id=SCOPE_IDENTITY()", array);
			int num = (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
			ShowTimeAsType.AppointmentShowTimeAsId = num;
			return num;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00057E04 File Offset: 0x00056004
		public AppShowTimeAsType LoadShowTimeAsTypeById(int AppointmentShowTimeAsId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, AppointmentShowTimeAsId)
			};
			AppShowTimeAsType result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT appointmentshowtimeasid,extraiconid,showtimeastitle,showtimeascolour FROM appointmentshowtimeas WHERE appointmentshowtimeasid=@id", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentShowTimeAsDAO.GetShowTimeAsFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00057E84 File Offset: 0x00056084
		public void DeleteShowTimeAsTypeById(int AppointmentShowTimeAsId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, AppointmentShowTimeAsId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmentshowtimeas WHERE appointmentshowtimeasid=@id", parameters);
		}

		// Token: 0x040004EE RID: 1262
		private DatabaseLayer DatabaseManager;
	}
}
