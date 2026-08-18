using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000123 RID: 291
	public class UserSettingGroupDataAttribute : Attribute
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x0000FB3D File Offset: 0x0000DD3D
		public UserSettingGroupDataAttribute(string name)
		{
			this.name = name;
			this.isActive = true;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000FB55 File Offset: 0x0000DD55
		public UserSettingGroupDataAttribute(string name, bool isActive)
		{
			this.name = name;
			this.isActive = isActive;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0000FB70 File Offset: 0x0000DD70
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0000FB88 File Offset: 0x0000DD88
		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
			set
			{
				this.isActive = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0000FB94 File Offset: 0x0000DD94
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400035C RID: 860
		protected string name;

		// Token: 0x0400035D RID: 861
		protected string description;

		// Token: 0x0400035E RID: 862
		protected bool isActive;
	}
}
