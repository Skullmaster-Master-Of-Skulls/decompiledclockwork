using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Email;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Emailing;

namespace TechnoPro.Common.DAO.Impl.Email
{
	// Token: 0x020000D4 RID: 212
	public class EmailHistoryLoggerDAO : IEmailHistoryLoggerDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x00036885 File Offset: 0x00034A85
		public EmailHistoryLoggerDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00036897 File Offset: 0x00034A97
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x0003689F File Offset: 0x00034A9F
		public OperationContext OpContext { get; set; }

		// Token: 0x060005CC RID: 1484 RVA: 0x000368A8 File Offset: 0x00034AA8
		public void LogItem(EmailHistoryLoggerItem item)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string query = "INSERT INTO emailhistory (personid,templateid,datesent,sentby,ebody,enote,successful,infopcid,emailtypecode,lucourseid) VALUES (@personid,@templateid,getdate(),@sentby,@ebody,@enote,@successful,@infopcid,@emailtypecode,@lucourseid)";
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@personid", DbType.Int32, (item.PersonId > 0) ? item.PersonId : DBNull.Value),
				databaseLayer.GetParameter("@templateid", DbType.Int32, (item.TemplateId > 0) ? item.TemplateId : DBNull.Value),
				databaseLayer.GetParameter("@sentby", DbType.Int32, (item.SentByPersonId > 0) ? item.SentByPersonId : DBNull.Value),
				databaseLayer.GetParameter("@infopcid", DbType.Int32, item.InfoPcId),
				databaseLayer.GetParameter("@lucourseid", DbType.Int32, item.LuCourseId),
				databaseLayer.GetParameter("@enote", DbType.String, item.Note ?? ""),
				databaseLayer.GetParameter("@ebody", DbType.String, Convert.ToBase64String(databaseLayer.Encryption.Encrypt(item.EmailMessage ?? ""))),
				databaseLayer.GetParameter("@successful", DbType.Boolean, item.WasSuccessfullySent),
				databaseLayer.GetParameter("@emailtypecode", DbType.String, item.HistoryCode ?? "")
			};
			databaseLayer.ExecuteNonQuery(query, parameters);
		}
	}
}
