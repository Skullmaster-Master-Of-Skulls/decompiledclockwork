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
	// Token: 0x02000125 RID: 293
	public class AppointmentCancelReasonDAO : IAppointmentCancelReasonDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00055EF4 File Offset: 0x000540F4
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x00055EFC File Offset: 0x000540FC
		private DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x0600085E RID: 2142 RVA: 0x00055F05 File Offset: 0x00054105
		public AppointmentCancelReasonDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00055F36 File Offset: 0x00054136
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x00055F3E File Offset: 0x0005413E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000861 RID: 2145 RVA: 0x00055F48 File Offset: 0x00054148
		internal static AppCancelReason GetCancelReasonFromRecord(IDataRecord record)
		{
			bool flag = record == null || record["cancelreasonid"] == DBNull.Value;
			AppCancelReason result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new AppCancelReason
				{
					CancelReasonId = (int)record["cancelreasonid"],
					CancelReasonGroup = AppointmentCancelReasonDAO.GetCancelReasonGroupFromRecord(record),
					CancelReasonTitle = record["cancelreasontitle"].ToString(),
					Colour = new int?((int)record["cancelreasoncolour"]),
					OrderNum = (int)record["cancelreasonordernum"],
					IsActive = (bool)record["cancelreasonisactive"]
				};
			}
			return result;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00056008 File Offset: 0x00054208
		private static AppCancelReasonGroup GetCancelReasonGroupFromRecord(IDataRecord record)
		{
			bool flag = record == null;
			AppCancelReasonGroup result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new AppCancelReasonGroup
				{
					CancelReasonGroupName = record["cancelreasongroupname"].ToString()
				};
			}
			return result;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00056044 File Offset: 0x00054244
		public IList<AppCancelReason> LoadAllCancelReasons()
		{
			IList<AppCancelReason> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    c.cancelreasonid,c.cancelreasongroupname,c.cancelreasontitle,c.cancelreasondescription,\r\n            c.colour AS cancelreasoncolour,c.ordernum AS cancelreasonordernum,c.isactive AS cancelreasonisactive\r\nFROM        cancelreason c\r\nORDER BY    c.cancelreasontitle"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppCancelReason> list = new List<AppCancelReason>();
					while (dataReader.Read())
					{
						AppCancelReason cancelReasonFromRecord = AppointmentCancelReasonDAO.GetCancelReasonFromRecord(dataReader);
						bool flag2 = cancelReasonFromRecord != null;
						if (flag2)
						{
							list.Add(cancelReasonFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000560C4 File Offset: 0x000542C4
		public AppCancelReason LoadCancelReasonById(int CancelReasonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@cancelreasonid", DbType.Int32, CancelReasonId)
			};
			AppCancelReason result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    c.cancelreasonid,c.cancelreasongroupname,c.cancelreasontitle,c.cancelreasondescription,\r\n            c.colour AS cancelreasoncolour,c.ordernum AS cancelreasonordernum,c.isactive AS cancelreasonisactive\r\nFROM        cancelreason c\r\nWHERE       c.cancelreasonid=@cancelreasonid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentCancelReasonDAO.GetCancelReasonFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00056144 File Offset: 0x00054344
		public void DeleteCancelReason(int CancelReasonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@cancelreasonid", DbType.Int32, CancelReasonId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM cancelreason WHERE cancelreasonid=@cancelreasonid", parameters);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00056188 File Offset: 0x00054388
		public void UpdateCancelReason(AppCancelReason CancelReason)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@cancelreasonid", DbType.Int32, CancelReason.CancelReasonId),
				this.DatabaseManager.GetParameter("@cancelreasongroup", DbType.String, (CancelReason.CancelReasonGroup == null) ? "" : (CancelReason.CancelReasonGroup.CancelReasonGroupName ?? "")),
				this.DatabaseManager.GetParameter("@cancelreasontitle", DbType.String, CancelReason.CancelReasonTitle ?? ""),
				this.DatabaseManager.GetParameter("@colour", DbType.Int32, (CancelReason.Colour != null) ? CancelReason.Colour.Value : 0),
				this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, CancelReason.OrderNum),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, CancelReason.IsActive)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE cancelreason SET cancelreasongroupname=@cancelreasongroup,cancelreasontitle=@cancelreasontitle,\r\n        colour=@colour,ordernum=@ordernum,isactive=@isactive\r\nWHERE cancelreasonid=@cancelreasonid", parameters);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000562A8 File Offset: 0x000544A8
		public int CreateCancelReason(AppCancelReason CancelReason)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@cancelreasonid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@cancelreasongroup", DbType.String, (CancelReason.CancelReasonGroup == null) ? "" : (CancelReason.CancelReasonGroup.CancelReasonGroupName ?? "")),
				this.DatabaseManager.GetParameter("@cancelreasontitle", DbType.String, CancelReason.CancelReasonTitle ?? ""),
				this.DatabaseManager.GetParameter("@colour", DbType.Int32, (CancelReason.Colour != null) ? CancelReason.Colour.Value : 0),
				this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, CancelReason.OrderNum),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, CancelReason.IsActive)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO cancelreason (cancelreasongroupname,cancelreasontitle,cancelreasondescription,colour,ordernum,isactive)\r\nVALUES (@cancelreasongroup,@cancelreasontitle,'',@colour,@ordernum,@isactive);\r\nSET @cancelreasonid=SCOPE_IDENTITY()", array);
			int num = (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
			CancelReason.CancelReasonId = num;
			return num;
		}
	}
}
