using System;
using System.Data;
using System.Data.Common;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000137 RID: 311
	public class AppointmentCancelInfoDAO : IAppointmentCancelInfoDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0005CC34 File Offset: 0x0005AE34
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x0005CC3C File Offset: 0x0005AE3C
		private DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0005CC45 File Offset: 0x0005AE45
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x0005CC4D File Offset: 0x0005AE4D
		public OperationContext OpContext { get; set; }

		// Token: 0x060008FE RID: 2302 RVA: 0x0005CC56 File Offset: 0x0005AE56
		public AppointmentCancelInfoDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0005CC88 File Offset: 0x0005AE88
		internal static AppCancelInfo GetCancelInfoFromRecord(IDataReader record, OperationContext opContext)
		{
			bool flag = record == null || record["cancelleddate"] == DBNull.Value;
			AppCancelInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new AppCancelInfo
				{
					CancelReason = AppointmentCancelReasonDAO.GetCancelReasonFromRecord(record),
					CancelledBy = PeopleDAO.GetPersonFromReader("", record, opContext, null),
					CancelledDate = (DateTime)record["cancelleddate"],
					CancelReasonText = record["cancelreasontext"].ToString()
				};
			}
			return result;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0005CD10 File Offset: 0x0005AF10
		public AppCancelInfo LoadCancelInfoByAppointmentId(int AppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			AppCancelInfo result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    acr.appointmentid,acr.cancelreasonid,acr.cancelreasontext,\r\n            acr.cancelledbypersonid AS personid,p.firstname,p.lastname,p.student_no,\r\n            acr.cancelleddate,cr.cancelreasongroupname,cr.cancelreasontitle,cr.cancelreasondescription,\r\n            cr.colour AS cancelreasoncolour,cr.ordernum AS cancelreasonordernum,cr.isactive AS cancelreasonisactive\r\nFROM        appointmentcancelledreason acr LEFT JOIN cancelreason cr ON cr.cancelreasonid=acr.cancelreasonid\r\n            LEFT JOIN people p ON p.personid=acr.cancelledbypersonid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentCancelInfoDAO.GetCancelInfoFromRecord(dataReader, this.OpContext);
				}
			}
			return result;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0005CD98 File Offset: 0x0005AF98
		public void DeleteCancelInfo(int AppointmentId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmentcancelledreason WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0005CDDC File Offset: 0x0005AFDC
		public void InsertOrUpdateAppointmentCancelInfo(int appId, AppCancelInfo appCancelInfo, DbTransaction transaction = null)
		{
			bool flag = appId < 1;
			if (flag)
			{
				CWLogger.Logger.Warn("AppointmentCancelInfoDAO:InsertOrUpdateAppointmentCancelInfo:AppId invalid={0}", appId.ToString());
			}
			else
			{
				bool flag2 = appCancelInfo == null || ((appCancelInfo.CancelReason == null || appCancelInfo.CancelReason.CancelReasonId < 1) && string.IsNullOrEmpty(appCancelInfo.CancelReasonText));
				if (flag2)
				{
					this.DeleteCancelInfo(appId, transaction);
				}
				else
				{
					DbParameter[] parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@appid", DbType.Int32, appId),
						this.DatabaseManager.GetParameter("@cancelreasonid", DbType.Int32, (appCancelInfo.CancelReason == null || appCancelInfo.CancelReason.CancelReasonId < 1) ? DBNull.Value : appCancelInfo.CancelReason.CancelReasonId),
						this.DatabaseManager.GetParameter("@canceltext", DbType.String, appCancelInfo.CancelReasonText ?? ""),
						this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI)
					};
					this.DatabaseManager.ExecuteNonQuery("IF EXISTS(SELECT appointmentid FROM appointmentcancelledreason WHERE appointmentid=@appid)\r\nBEGIN\r\n    UPDATE appointmentcancelledreason SET cancelreasonid=@cancelreasonid,cancelreasontext=@canceltext \r\n        WHERE appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO appointmentcancelledreason (appointmentid,cancelreasonid,cancelreasontext,cancelledbypersonid,cancelleddate)\r\n        VALUES (@appid,@cancelreasonid,@canceltext,@whoami,getdate())\r\nEND", parameters);
				}
			}
		}
	}
}
