using System;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.DynamicForms.FormApproval;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.DAO.Impl.DynamicForms.FormApproval
{
	// Token: 0x020000EF RID: 239
	public class FormApprovalTraineeDAO : IFormApprovalTraineeDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public FormApprovalTraineeDAO()
		{
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00047EA8 File Offset: 0x000460A8
		public FormApprovalTraineeDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00047EBA File Offset: 0x000460BA
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00047EC2 File Offset: 0x000460C2
		public OperationContext OpContext { get; set; }

		// Token: 0x060006DE RID: 1758 RVA: 0x00047ECC File Offset: 0x000460CC
		public Guid CreateFormApproval(int screenNum, int studentPersonId, int appId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@formApprovalId", DbType.Guid, 0),
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@appid", DbType.Int32, appId),
				databaseLayer.GetParameter("@whoamipid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@currentstate", DbType.Int32, 0)
			};
			databaseLayer.ExecuteNonQuery("DECLARE @existingId uniqueidentifier = (SELECT TOP 1 FormApprovalId FROM FormApproval WHERE screennum=@screennum AND personid=@pid AND appointmentid=@appid)\r\nIF NOT @existingId IS NULL \r\nBEGIN\r\n    SET @formApprovalId = @existingId\r\nEND\r\nELSE\r\nBEGIN\r\n    SET @formApprovalId = newid()\r\n    INSERT INTO FormApproval (FormApprovalId,screennum,personid,appointmentid,DateCreated,WhoUploaded,CurrentStateId)\r\n    VALUES (@formApprovalId,@screennum,@pid,@appid,getdate(),@whoamipid,@currentstate)\r\nEND", array);
			object value = array[0].Value;
			return (value == null || value is DBNull) ? Guid.Empty : ((Guid)value);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00047FB8 File Offset: 0x000461B8
		public Guid CreateOrUpdateFormApprovalTraineeSignature(Guid formApprovalId, FormApprovalSignature traineeSignature)
		{
			string text = (traineeSignature != null) ? traineeSignature.SignatureText : null;
			byte[] array = (traineeSignature != null) ? traineeSignature.SignatureImage : null;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array2 = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@formApprovalSignatureId", DbType.Guid, 0),
				databaseLayer.GetParameter("@formApprovalId", DbType.Guid, formApprovalId),
				databaseLayer.GetParameter("@signatureText", DbType.Binary, databaseLayer.Encryption.Encrypt(text ?? "")),
				databaseLayer.GetParameter("@signatureImage", DbType.Binary, array ?? DBNull.Value),
				databaseLayer.GetParameter("@whoamipid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@currentstate", DbType.Int32, 2)
			};
			databaseLayer.ExecuteNonQuery("DECLARE @existingFormApprovalSignatureId uniqueidentifier = (SELECT TOP 1 SubmittedFormApprovalSignatureId FROM FormApproval WHERE FormApprovalId=@formApprovalId)\r\nIF NOT @existingFormApprovalSignatureId IS NULL \r\nBEGIN\r\n    DELETE FROM FormApprovalSignature WHERE FormApprovalSignatureId=@existingFormApprovalSignatureId\r\nEND\r\n\r\nSET @formApprovalSignatureId = newid()\r\nINSERT INTO FormApprovalSignature (FormApprovalSignatureId,personid,signatureText,signatureImage,DateCreated) VALUES (@formApprovalSignatureId,@whoamipid,@signatureText,@signatureImage,getdate())\r\n\r\nUPDATE FormApproval SET SubmittedFormApprovalSignatureId=@formApprovalSignatureId,CurrentStateId=@currentstate WHERE formApprovalId=@formApprovalId", array2);
			object value = array2[0].Value;
			return (value == null || value is DBNull) ? Guid.Empty : ((Guid)value);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000480D4 File Offset: 0x000462D4
		public FormApprovalSignature LoadTraineeSignature(Guid formApprovalId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@formApprovalId", DbType.Guid, formApprovalId)
			};
			FormApprovalSignature result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    fa.FormApprovalId,s.FormApprovalSignatureId,s.signatureText,s.signatureImage,s.DateCreated,\r\n    s.personid,p.lastname,p.firstname,p.middlename,p.student_no\r\nFROM    FormApproval fa LEFT JOIN FormApprovalSignature s ON s.FormApprovalSignatureId=fa.SubmittedFormApprovalSignatureId\r\n        LEFT JOIN people p ON p.personid=s.personid\r\nWHERE   fa.FormApprovalId=@formApprovalId", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					result = FormApprovalTraineeDAO.GetSignatureFromRecord(dataReader, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00048174 File Offset: 0x00046374
		public static FormApprovalSignature GetSignatureFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null;
			FormApprovalSignature result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Guid guid = (record["FormApprovalSignatureId"] is DBNull) ? Guid.Empty : ((Guid)record["FormApprovalSignatureId"]);
				bool flag2 = guid == Guid.Empty;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new FormApprovalSignature
					{
						FormApprovalSignatureId = guid,
						DateSigned = (DateTime)record["DateCreated"],
						WhoSigned = PeopleDAO.GetBasicPersonFromRecord("", record, batchDecryptor),
						SignatureText = ((record["SignatureText"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["SignatureText"])),
						SignatureImage = ((record["SignatureImage"] is DBNull) ? null : ((byte[])record["SignatureImage"]))
					};
				}
			}
			return result;
		}
	}
}
