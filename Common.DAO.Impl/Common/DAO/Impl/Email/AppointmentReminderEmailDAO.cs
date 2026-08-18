using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Email;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.DAO.Impl.Email
{
	// Token: 0x020000D2 RID: 210
	public class AppointmentReminderEmailDAO : IAppointmentReminderEmailDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x00036528 File Offset: 0x00034728
		public AppointmentReminderEmailDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00036559 File Offset: 0x00034759
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x00036561 File Offset: 0x00034761
		public OperationContext OpContext { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0003656A File Offset: 0x0003476A
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x00036572 File Offset: 0x00034772
		private DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x060005C0 RID: 1472 RVA: 0x0003657C File Offset: 0x0003477C
		public void LogEmailSent(int StudentPersonId, string BatchEmailTitle, TPMailMessage Email, string Note, int TemplateId = 0)
		{
			bool flag = Email != null;
			string value;
			if (flag)
			{
				value = string.Concat(new string[]
				{
					Email.To.GetEmailList(),
					":",
					Email.Cc.GetEmailList(),
					":",
					Email.Bcc.GetEmailList()
				});
			}
			else
			{
				value = "Email null";
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, StudentPersonId),
				this.DatabaseManager.GetParameter("@templateid", DbType.Int32, TemplateId),
				this.DatabaseManager.GetParameter("@title", DbType.String, BatchEmailTitle ?? ""),
				this.DatabaseManager.GetParameter("@subject", DbType.String, (Email == null) ? "" : (Email.Subject ?? "")),
				this.DatabaseManager.GetParameter("@etoccbcc", DbType.String, value),
				this.DatabaseManager.GetParameter("@note", DbType.String, Note ?? "")
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO emailhistory (personid,templateid,datesent,sentby,etoccbcc,ebody,attachments,enote,successful,infopcid,lucourseid,emailtypecode)\r\nVALUES (@pid,@templateid,getdate(),NULL,@etoccbcc,@subject,'',@note,1,NULL,NULL,@title)", parameters);
		}
	}
}
