using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000128 RID: 296
	public class AppointmentLogDAO : IAppointmentLogDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x000570B9 File Offset: 0x000552B9
		public AppointmentLogDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000570CB File Offset: 0x000552CB
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x000570D3 File Offset: 0x000552D3
		public OperationContext OpContext { get; set; }

		// Token: 0x06000881 RID: 2177 RVA: 0x000570DC File Offset: 0x000552DC
		public void LogAppModifications(int AppointmentId, eHowModifiedCode howModifiedCode, eAppointmentModifiedItemType modifiedItems)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
				databaseLayer.GetParameter("@howmodifiedcode", DbType.Int32, (int)howModifiedCode),
				databaseLayer.GetParameter("@whomodified", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@changed_room", DbType.Boolean, (modifiedItems & eAppointmentModifiedItemType.Room) > eAppointmentModifiedItemType.None),
				databaseLayer.GetParameter("@changed_memo", DbType.Boolean, (modifiedItems & eAppointmentModifiedItemType.Memo) > eAppointmentModifiedItemType.None),
				databaseLayer.GetParameter("@changed_attendees", DbType.Boolean, (modifiedItems & eAppointmentModifiedItemType.Attendees) > eAppointmentModifiedItemType.None),
				databaseLayer.GetParameter("@changed_cancelled", DbType.Boolean, (modifiedItems & eAppointmentModifiedItemType.Cancelled) > eAppointmentModifiedItemType.None),
				databaseLayer.GetParameter("@changed_icons", DbType.Boolean, (modifiedItems & eAppointmentModifiedItemType.Icons) > eAppointmentModifiedItemType.None)
			};
			databaseLayer.ExecuteNonQuery("IF EXISTS(SELECT appointmentid FROM appointmentsmodifieddates WHERE appointmentid=@appid AND howmodifiedcode=@howmodifiedcode AND personid=@whomodified AND DATEDIFF(s,datemodified,getdate())<=2)\r\nBEGIN\r\n    UPDATE appointmentsmodifieddates SET \r\n    datemodified=getdate(),changed_cancelled=changed_cancelled | @changed_cancelled,changed_room = changed_room | @changed_room,\r\n    changed_memo=changed_memo | @changed_memo,changed_attendees=changed_attendees | @changed_attendees,changed_icons = changed_icons | @changed_icons\r\n    WHERE appointmentid=@appid AND howmodifiedcode=@howmodifiedcode AND personid=@whomodified AND DATEDIFF(s,datemodified,getdate())<=2\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO appointmentsmodifieddates (appointmentid,datemodified,personid,howmodifiedcode,changed_cancelled,changed_room,changed_memo,changed_attendees,changed_icons)\r\n    VALUES (@appid,getdate(),@whomodified,@howmodifiedcode,@changed_cancelled,@changed_room,@changed_memo,@changed_attendees,@changed_icons)\r\nEND", parameters);
			bool flag = howModifiedCode == eHowModifiedCode.Delete;
			if (flag)
			{
				parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
					databaseLayer.GetParameter("@whodeleted", DbType.Int32, this.OpContext.WhoAmI)
				};
				databaseLayer.ExecuteNonQuery("INSERT INTO appointmentsdeleteddates (appointmentid,datedeleted,personid) \r\nVALUES (@appid,getdate(),@whodeleted)", parameters);
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00057248 File Offset: 0x00055448
		public void LogAppModificationsPreChangeCommitted(int AppointmentId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appointmentid", DbType.Int32, AppointmentId),
				databaseLayer.GetParameter("@whomodifiedpid", DbType.Int32, (this.OpContext == null) ? 0 : this.OpContext.WhoAmI)
			};
			databaseLayer.ExecuteNonQuery("DECLARE @pidlist varchar(max)\r\nSELECT @pidlist=COALESCE(@pidlist + ',', '' ) + CAST(personid AS varchar(256))\r\nFROM attendees WHERE appointmentid=@appointmentid\r\n\r\nSET @pidlist= 'pids=' + @pidlist + ';who=' + CAST(@whomodifiedpid AS varchar(256));\r\n\r\nINSERT INTO archive_appointments (auditaction,appointmentid,apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode,[subject],location,examid,caseid,totalbreakminutes,sittingid,extrainfo)\r\n    SELECT \t'PRE',a.appointmentid,a.apptypeid,a.startdate,a.enddate,a.cancelled,a.dateadded,a.personid,a.ishidden,a.islocked,a.extraattendeescount,a.appcode,a.groupcode,\r\n\t\t    a.[subject],a.location,a.examid,a.caseid,a.totalbreakminutes,a.sittingid,COALESCE(@pidlist,'-') AS extrainfo\r\n    FROM    appointments a\r\n    WHERE a.appointmentid=@appointmentid", parameters);
		}
	}
}
