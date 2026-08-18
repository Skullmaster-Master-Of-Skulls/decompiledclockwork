using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Impl.Messaging;
using TechnoPro.Common.DAO.Messaging;
using TechnoPro.Common.ICore.Messaging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DropBox;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.Messaging
{
	// Token: 0x020000B0 RID: 176
	public class AttachmentDropBoxManager : IAttachmentDropBoxManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x000264C8 File Offset: 0x000246C8
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x000264D0 File Offset: 0x000246D0
		public IAttachmentDropBoxDAO ATT_DropBox { get; set; }

		// Token: 0x0600069E RID: 1694 RVA: 0x000264D9 File Offset: 0x000246D9
		public AttachmentDropBoxManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.ATT_DropBox = new DropBox_Att_DAO(this.OpContext);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00026500 File Offset: 0x00024700
		public void Save(DropBox_Attachment att)
		{
			this.ATT_DropBox.Save(att);
			bool settingValue = SettingManager.CurrentInstance.GetSettingValue<bool>(Setting.CLOCKWORKSERVER_DROPBOX_NOTIFICATION_EMAIL_ACTIVE);
			bool flag = !settingValue;
			if (flag)
			{
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00026538 File Offset: 0x00024738
		public IList<DropBox_AttachmentInfo> GetAllAttachmentsInfo(string username)
		{
			return this.ATT_DropBox.GetAllAttachmentsInfo(username);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00026558 File Offset: 0x00024758
		public int CountAttachments(string username)
		{
			return this.ATT_DropBox.CountAttachments(username);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00026578 File Offset: 0x00024778
		public DropBox_Attachment GetAttachment(int attID)
		{
			return this.ATT_DropBox.GetAttachment(attID);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00026596 File Offset: 0x00024796
		public void DeleteAttachment(int attID)
		{
			this.ATT_DropBox.Delete(attID);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000265A8 File Offset: 0x000247A8
		public DropBox_Attachment GetAttachment(string filename, string extension)
		{
			return this.ATT_DropBox.GetAttachment(filename, extension);
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x000265C7 File Offset: 0x000247C7
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x000265CF File Offset: 0x000247CF
		public OperationContext OpContext { get; set; }
	}
}
