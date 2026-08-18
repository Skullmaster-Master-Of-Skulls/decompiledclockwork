using System;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.DynamicForms.FormApproval;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.DAO.Impl.DynamicForms.FormApproval
{
	// Token: 0x020000EE RID: 238
	public class FormApprovalSupervisorDAO : IFormApprovalSupervisorDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public FormApprovalSupervisorDAO()
		{
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00047C5F File Offset: 0x00045E5F
		public FormApprovalSupervisorDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00047C71 File Offset: 0x00045E71
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x00047C79 File Offset: 0x00045E79
		public OperationContext OpContext { get; set; }

		// Token: 0x060006D7 RID: 1751 RVA: 0x00047C84 File Offset: 0x00045E84
		public Guid CreateOrUpdateFormApprovalSupervisorSignature(Guid formApprovalId, FormApprovalSignature supervisorSignature)
		{
			string text = (supervisorSignature != null) ? supervisorSignature.SignatureText : null;
			byte[] array = (supervisorSignature != null) ? supervisorSignature.SignatureImage : null;
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
				databaseLayer.GetParameter("@currentstate", DbType.Int32, 4)
			};
			databaseLayer.ExecuteNonQuery("DECLARE @existingFormApprovalSignatureId uniqueidentifier = (SELECT TOP 1 ApprovedFormApprovalSignatureId FROM FormApproval WHERE FormApprovalId=@formApprovalId)\r\nIF NOT @existingFormApprovalSignatureId IS NULL \r\nBEGIN\r\n    DELETE FROM FormApprovalSignature WHERE FormApprovalSignatureId=@existingFormApprovalSignatureId\r\nEND\r\n\r\nSET @formApprovalSignatureId = newid()\r\nINSERT INTO FormApprovalSignature (FormApprovalSignatureId,personid,signatureText,signatureImage,DateCreated) VALUES (@formApprovalSignatureId,@whoamipid,@signatureText,@signatureImage,getdate())\r\n\r\nUPDATE FormApproval SET ApprovedFormApprovalSignatureId=@formApprovalSignatureId,CurrentStateId=@currentstate WHERE formApprovalId=@formApprovalId", array2);
			object value = array2[0].Value;
			return (value == null || value is DBNull) ? Guid.Empty : ((Guid)value);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00047DA0 File Offset: 0x00045FA0
		public void RemoveFormApprovalSupervisorSignature(Guid formApprovalId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@formApprovalId", DbType.Guid, formApprovalId),
				databaseLayer.GetParameter("@currentstate", DbType.Int32, 2)
			};
			databaseLayer.ExecuteNonQuery("DECLARE @existingFormApprovalSignatureId uniqueidentifier = (SELECT TOP 1 ApprovedFormApprovalSignatureId FROM FormApproval WHERE FormApprovalId=@formApprovalId)\r\nIF NOT @existingFormApprovalSignatureId IS NULL \r\nBEGIN\r\n    DELETE FROM FormApprovalSignature WHERE FormApprovalSignatureId=@existingFormApprovalSignatureId\r\nEND\r\n\r\nUPDATE FormApproval SET ApprovedFormApprovalSignatureId=NULL,CurrentStateId=@currentstate WHERE formApprovalId=@formApprovalId", parameters);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00047E08 File Offset: 0x00046008
		public FormApprovalSignature LoadSupervisorSignature(Guid formApprovalId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@formApprovalId", DbType.Guid, formApprovalId)
			};
			FormApprovalSignature result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    fa.FormApprovalId,s.FormApprovalSignatureId,s.signatureText,s.signatureImage,s.DateCreated,\r\n    s.personid,p.lastname,p.firstname,p.middlename,p.student_no\r\nFROM    FormApproval fa LEFT JOIN FormApprovalSignature s ON s.FormApprovalSignatureId=fa.ApprovedFormApprovalSignatureId\r\n        LEFT JOIN people p ON p.personid=s.personid\r\nWHERE   fa.FormApprovalId=@formApprovalId", parameters))
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
	}
}
