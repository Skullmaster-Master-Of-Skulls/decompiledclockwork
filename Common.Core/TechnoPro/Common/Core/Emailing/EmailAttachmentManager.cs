using System;
using TechnoPro.Common.DAO.Email;
using TechnoPro.Common.DAO.Impl.Email;
using TechnoPro.Common.ICore.Emailing;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Emailing
{
	// Token: 0x020000F5 RID: 245
	public class EmailAttachmentManager : IEmailAttachmentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0003CAEC File Offset: 0x0003ACEC
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x0003CAF4 File Offset: 0x0003ACF4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000993 RID: 2451 RVA: 0x0003CAFD File Offset: 0x0003ACFD
		public EmailAttachmentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0003CB10 File Offset: 0x0003AD10
		private IEmailAttachmentDAO dao
		{
			get
			{
				IEmailAttachmentDAO result;
				if ((result = this._dao) == null)
				{
					result = (this._dao = new EmailAttachmentDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0003CB3C File Offset: 0x0003AD3C
		public TPMailAttachment LoadAttachment(int FileId)
		{
			return this.dao.LoadAttachment(FileId);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0003CB5A File Offset: 0x0003AD5A
		public void DeleteAttachment(int FileId)
		{
			this.dao.DeleteAttachment(FileId);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0003CB6C File Offset: 0x0003AD6C
		public int CreateAttachment(TPMailAttachment attachment)
		{
			return this.dao.CreateAttachment(attachment);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00003940 File Offset: 0x00001B40
		public void UpdateAttachment(TPMailAttachment attachment)
		{
		}

		// Token: 0x040001AE RID: 430
		private IEmailAttachmentDAO _dao;
	}
}
