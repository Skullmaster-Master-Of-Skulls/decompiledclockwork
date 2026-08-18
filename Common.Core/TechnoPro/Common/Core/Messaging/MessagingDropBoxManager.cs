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
	// Token: 0x020000B2 RID: 178
	public class MessagingDropBoxManager : IMessagingDropBoxManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0002678D File Offset: 0x0002498D
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x00026795 File Offset: 0x00024995
		public IIMDropBoxDAO IM_DropBox { get; set; }

		// Token: 0x060006AC RID: 1708 RVA: 0x0002679E File Offset: 0x0002499E
		public MessagingDropBoxManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.IM_DropBox = new DropBox_IM_DAO(this.OpContext);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000267C4 File Offset: 0x000249C4
		public void Save(DropBox_IM im)
		{
			this.IM_DropBox.Save(im);
			bool settingValue = SettingManager.CurrentInstance.GetSettingValue<bool>(Setting.CLOCKWORKSERVER_DROPBOX_NOTIFICATION_EMAIL_ACTIVE);
			bool flag = !settingValue;
			if (flag)
			{
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000267FC File Offset: 0x000249FC
		public IList<DropBox_IM> GetAllIMs(string username)
		{
			return this.IM_DropBox.GetAllIMs(username);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0002681C File Offset: 0x00024A1C
		public int CountIMs(string username)
		{
			return this.IM_DropBox.CountIMs(username);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0002683C File Offset: 0x00024A3C
		public DropBox_IM GetIM(int imID)
		{
			return this.IM_DropBox.GetIM(imID);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0002685A File Offset: 0x00024A5A
		public void DeleteIM(int imID)
		{
			this.IM_DropBox.Delete(imID);
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0002686A File Offset: 0x00024A6A
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00026872 File Offset: 0x00024A72
		public OperationContext OpContext { get; set; }
	}
}
