using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Communications;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Communications;

namespace TechnoPro.Common.DAO.Impl.Communications
{
	// Token: 0x0200010B RID: 267
	public class CommunicationsSentDAO : ICommunicationsSentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x0004E43E File Offset: 0x0004C63E
		public CommunicationsSentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0004E450 File Offset: 0x0004C650
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0004E458 File Offset: 0x0004C658
		public OperationContext OpContext { get; set; }

		// Token: 0x060007AB RID: 1963 RVA: 0x0004E464 File Offset: 0x0004C664
		private Communication GetCommunicationFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null || record["CommunicationId"] is DBNull;
			Communication result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["SendAttemptedMethods"] is DBNull) ? 0 : ((int)record["SendAttemptedMethods"]);
				result = new Communication
				{
					CommunicationId = (int)record["CommunicationId"],
					PersonId = ((record["PersonId"] is DBNull) ? 0 : ((int)record["PersonId"])),
					DateSendAttempted = (DateTime)record["DateSendAttempted"],
					SentSuccessfully = (!(record["SentSuccessfully"] is DBNull) && (bool)record["SentSuccessfully"]),
					ErrorMessage = ((record["ErrorMessage"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["ErrorMessage"])),
					WhoSent = PeopleDAO.GetPersonFromReader("WhoSent", record, this.OpContext, batchDecryptor),
					SendAttemptedMethods = (eCommunicationSendMethod)((num > 0 && Enum.IsDefined(typeof(eCommunicationSendMethod), num)) ? num : 0),
					Subject = ((record["Subject"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["Subject"])),
					Body = ((record["Body"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["Body"]))
				};
			}
			return result;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0004E62C File Offset: 0x0004C82C
		[DebuggerStepThrough]
		public Task<IList<Communication>> LoadCommunicationsForUserAsync(int personId)
		{
			CommunicationsSentDAO.<LoadCommunicationsForUserAsync>d__6 <LoadCommunicationsForUserAsync>d__ = new CommunicationsSentDAO.<LoadCommunicationsForUserAsync>d__6();
			<LoadCommunicationsForUserAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<Communication>>.Create();
			<LoadCommunicationsForUserAsync>d__.<>4__this = this;
			<LoadCommunicationsForUserAsync>d__.personId = personId;
			<LoadCommunicationsForUserAsync>d__.<>1__state = -1;
			<LoadCommunicationsForUserAsync>d__.<>t__builder.Start<CommunicationsSentDAO.<LoadCommunicationsForUserAsync>d__6>(ref <LoadCommunicationsForUserAsync>d__);
			return <LoadCommunicationsForUserAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0004E678 File Offset: 0x0004C878
		public IList<Communication> LoadCommunicationsForUser(int personId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, personId)
			};
			IList<Communication> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tc.CommunicationId,c.PersonId,c.DateSendAttempted,c.SentSuccessfully,c.ErrorMessage,c.WhoSentPersonId,c.SendAttemptedMethods,c.[Subject],c.Body,\r\n        p.lastName AS WhoSentLastName,p.firstName AS WhoSentFirstName,p.middleName AS WhoSentMiddleName,p.student_no AS WhoSentStudent_no\r\nFROM    Communications c LEFT JOIN People p ON p.PersonId=c.WhoSentPersonId\r\nWHERE   c.PersonId= @pid\r\nORDER BY c.DateSendAttempted", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Communication> list = new List<Communication>();
					while (dataReader.Read())
					{
						Communication communicationFromRecord = this.GetCommunicationFromRecord(dataReader, databaseLayer.Encryption.GetBatchDecryptor());
						bool flag2 = communicationFromRecord != null;
						if (flag2)
						{
							list.Add(communicationFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0004E734 File Offset: 0x0004C934
		public int AddCommicationSendAttempt(CommunicationBase sendAttempt)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			IEncryption encryption = databaseLayer.Encryption;
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@id", DbType.Int32, 0),
				databaseLayer.GetParameter("@pid", DbType.Int32, sendAttempt.PersonId),
				databaseLayer.GetParameter("@sentsuccessfully", DbType.Boolean, sendAttempt.SentSuccessfully),
				databaseLayer.GetParameter("@errormessage", DbType.Binary, string.IsNullOrEmpty(sendAttempt.ErrorMessage) ? DBNull.Value : encryption.Encrypt(sendAttempt.ErrorMessage)),
				databaseLayer.GetParameter("@whosentpersonid", DbType.Int32, (sendAttempt.WhoSentPersonId > 0) ? sendAttempt.WhoSentPersonId : DBNull.Value),
				databaseLayer.GetParameter("@methods", DbType.Int32, (int)sendAttempt.SendAttemptedMethods),
				databaseLayer.GetParameter("@subject", DbType.Binary, string.IsNullOrEmpty(sendAttempt.Subject) ? DBNull.Value : encryption.Encrypt(sendAttempt.Subject)),
				databaseLayer.GetParameter("@body", DbType.Binary, string.IsNullOrEmpty(sendAttempt.Body) ? DBNull.Value : encryption.Encrypt(sendAttempt.Body))
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO Communications (PersonId,DateSendAttempted,SentSuccessfully,ErrorMessage,WhoSentPersonId,SendAttemptedMethods,Subject,Body)\r\nVALUES (@pid,getdate(),@sentsuccessfully,@errormessage,@whosentpersonid,@methods,@subject,@body)\r\n\r\nSET @id=(SELECT CAST(SCOPE_IDENTITY() AS int) AS id)", array);
			object value = array[0].Value;
			return (value as int?).GetValueOrDefault();
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0004E8AC File Offset: 0x0004CAAC
		[DebuggerStepThrough]
		public Task<int> AddCommicationSendAttemptAsync(CommunicationBase sendAttempt)
		{
			CommunicationsSentDAO.<AddCommicationSendAttemptAsync>d__9 <AddCommicationSendAttemptAsync>d__ = new CommunicationsSentDAO.<AddCommicationSendAttemptAsync>d__9();
			<AddCommicationSendAttemptAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<AddCommicationSendAttemptAsync>d__.<>4__this = this;
			<AddCommicationSendAttemptAsync>d__.sendAttempt = sendAttempt;
			<AddCommicationSendAttemptAsync>d__.<>1__state = -1;
			<AddCommicationSendAttemptAsync>d__.<>t__builder.Start<CommunicationsSentDAO.<AddCommicationSendAttemptAsync>d__9>(ref <AddCommicationSendAttemptAsync>d__);
			return <AddCommicationSendAttemptAsync>d__.<>t__builder.Task;
		}
	}
}
