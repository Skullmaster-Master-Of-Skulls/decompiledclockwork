using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Email;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.DAO.Impl.Email
{
	// Token: 0x020000D3 RID: 211
	public class EmailAttachmentDAO : IEmailAttachmentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000366B3 File Offset: 0x000348B3
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x000366BB File Offset: 0x000348BB
		public OperationContext OpContext { get; set; }

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public EmailAttachmentDAO()
		{
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000366C4 File Offset: 0x000348C4
		public EmailAttachmentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000366D8 File Offset: 0x000348D8
		private TPMailAttachment GetMailAttachmentFromRecord(IDataReader record)
		{
			return new TPMailAttachment
			{
				FileAttachmentId = Convert.ToInt32(record["fileid"]),
				FileBytes = (byte[])record["filebytes"],
				FileNameForDisplay = record["filename"].ToString()
			};
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00036734 File Offset: 0x00034934
		public TPMailAttachment LoadAttachment(int FileId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@fileid", DbType.Int32, FileId)
			};
			TPMailAttachment result;
			using (IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("SELECT fileid,filename,filebytes FROM emailtemplatefiles WHERE fileid=@fileid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetMailAttachmentFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000367B4 File Offset: 0x000349B4
		public void DeleteAttachment(int FileId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@fileid", DbType.Int32, FileId)
			};
			clockWorkFiles.ExecuteNonQuery("DELETE FROM emailtemplatefiles WHERE fileid=@fileid", parameters);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000367F4 File Offset: 0x000349F4
		public int CreateAttachment(TPMailAttachment attachment)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] array = new DbParameter[]
			{
				clockWorkFiles.GetOutputParameter("@fileid", DbType.Int32, 0),
				clockWorkFiles.GetParameter("@fn", DbType.String, attachment.FileNameForDisplay),
				clockWorkFiles.GetParameter("@bytes", DbType.Binary, attachment.FileBytes ?? new byte[0])
			};
			clockWorkFiles.ExecuteNonQuery("INSERT INTO emailtemplatefiles (filename,filebytes) VALUES (@fn,@bytes)\r\n SET @fileid= SCOPE_IDENTITY()", array);
			return (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
		}
	}
}
