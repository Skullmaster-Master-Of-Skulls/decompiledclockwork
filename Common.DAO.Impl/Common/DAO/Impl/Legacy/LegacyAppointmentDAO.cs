using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.Appointment;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.Legacy
{
	// Token: 0x020000A6 RID: 166
	public class LegacyAppointmentDAO : ILegacyAppointmentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x00029D3E File Offset: 0x00027F3E
		public LegacyAppointmentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00029D50 File Offset: 0x00027F50
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00029D58 File Offset: 0x00027F58
		public OperationContext OpContext { get; set; }

		// Token: 0x0600048F RID: 1167 RVA: 0x00029D64 File Offset: 0x00027F64
		private static AppointmentModifiedHistoryItem GetAppModifiedHistoryItemFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			string[] source = new string[]
			{
				"changed_description",
				"changed_room",
				"changed_memo",
				"changed_attendees",
				"changed_cancelled",
				"changed_noshow",
				"changed_course",
				"changed_other1",
				"changed_other2",
				"changed_icons",
				"changed_datetime"
			};
			string actionDetails = string.Join(", ", (from g in source
			where record[g] != DBNull.Value && Convert.ToBoolean(record[g])
			select g).ToArray<string>());
			string text = (record["firstname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["firstname"]);
			string text2 = (record["lastname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["lastname"]);
			AppointmentModifiedHistoryItem appointmentModifiedHistoryItem = new AppointmentModifiedHistoryItem();
			appointmentModifiedHistoryItem.Action = record["action"].ToString().Trim();
			appointmentModifiedHistoryItem.ActionDate = ((record["action_date"] is DBNull) ? DateTime.MinValue : ((DateTime)record["action_date"]));
			appointmentModifiedHistoryItem.ActionDetails = actionDetails;
			AppointmentModifiedHistoryItem appointmentModifiedHistoryItem2 = appointmentModifiedHistoryItem;
			PersonBase actionBy;
			if (text.Length <= 0 && text2.Length <= 0)
			{
				actionBy = null;
			}
			else
			{
				PersonBase personBase = new PersonBase();
				personBase.FirstName = text;
				actionBy = personBase;
				personBase.LastName = text2;
			}
			appointmentModifiedHistoryItem2.ActionBy = actionBy;
			return appointmentModifiedHistoryItem;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00029F24 File Offset: 0x00028124
		public IList<AppointmentModifiedHistoryItem> LoadAsAppointmentModifiedHistory(int AppointmentId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			IList<AppointmentModifiedHistoryItem> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT 'Created' AS action,app.dateadded AS action_date,'' AS changed_description,'' AS changed_room,'' AS changed_memo,'' AS changed_attendees,'' AS changed_cancelled,'' AS changed_noshow,'' AS changed_course,'' AS changed_other1,'' AS changed_other2,'' AS changed_icons,'' AS changed_datetime,p.firstname,p.lastname FROM appointments app LEFT JOIN people p ON p.personid=app.personid WHERE app.appointmentid=@appid UNION SELECT x.action,m.datemodified AS action_date,m.changed_description,m.changed_room,m.changed_memo,m.changed_attendees,m.changed_cancelled,m.changed_noshow,m.changed_course,m.changed_other1,m.changed_other2,m.changed_icons,m.changed_datetime,p.firstname,p.lastname FROM appointmentsmodifieddates m LEFT JOIN (SELECT 1 AS howmodifiedcode,'Modified' AS action UNION SELECT 2 AS howmodifiedcode,'Deleted' AS action) x ON x.howmodifiedcode=m.howmodifiedcode LEFT JOIN people p ON p.personid=m.personid WHERE m.appointmentid=@appid ORDER BY action_date", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					List<AppointmentModifiedHistoryItem> list = new List<AppointmentModifiedHistoryItem>();
					while (dataReader.Read())
					{
						AppointmentModifiedHistoryItem appModifiedHistoryItemFromRecord = LegacyAppointmentDAO.GetAppModifiedHistoryItemFromRecord(dataReader, batchDecryptor);
						bool flag2 = appModifiedHistoryItemFromRecord != null;
						if (flag2)
						{
							list.Add(appModifiedHistoryItemFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}
	}
}
