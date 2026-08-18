using System;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005D3 RID: 1491
	public class WebSettingWithAttribute
	{
		// Token: 0x06002FE7 RID: 12263 RVA: 0x0000D55A File Offset: 0x0000B75A
		public WebSettingWithAttribute()
		{
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x0003B311 File Offset: 0x00039511
		public WebSettingWithAttribute(Setting setting)
		{
			this.Setting = setting;
			this.SettingReferenceAttribute = WebSettingGroupWithEnums.GetAttribute<ReferenceSettingAttribute>(setting);
			this.SettingDataAttribute = WebSettingGroupWithEnums.GetAttribute<SettingDataAttribute>(setting);
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x0003B348 File Offset: 0x00039548
		public bool IsValid
		{
			get
			{
				return this.SettingReferenceAttribute != null || this.SettingDataAttribute != null;
			}
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x0003B370 File Offset: 0x00039570
		public bool IsHidden
		{
			get
			{
				bool flag = this.SettingReferenceAttribute != null;
				bool result;
				if (flag)
				{
					result = this.SettingReferenceAttribute.IsHidden;
				}
				else
				{
					bool flag2 = this.SettingDataAttribute != null;
					result = (!flag2 || this.SettingDataAttribute.IsHidden);
				}
				return result;
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x0003B3BC File Offset: 0x000395BC
		public object DefaultValue
		{
			get
			{
				bool flag = this.SettingReferenceAttribute != null;
				object result;
				if (flag)
				{
					result = this.SettingReferenceAttribute.DefaultValue;
				}
				else
				{
					bool flag2 = this.SettingDataAttribute != null;
					if (flag2)
					{
						result = this.SettingDataAttribute.DefaultValue;
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x0003B408 File Offset: 0x00039608
		public string Name
		{
			get
			{
				bool flag = this.SettingReferenceAttribute != null;
				string result;
				if (flag)
				{
					result = (this.SettingReferenceAttribute.Name ?? "");
				}
				else
				{
					bool flag2 = this.SettingDataAttribute != null;
					if (flag2)
					{
						result = (this.SettingDataAttribute.Name ?? "");
					}
					else
					{
						result = "";
					}
				}
				return result;
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x0003B468 File Offset: 0x00039668
		public string Description
		{
			get
			{
				bool flag = this.SettingReferenceAttribute != null;
				string result;
				if (flag)
				{
					result = (this.SettingReferenceAttribute.Description ?? "");
				}
				else
				{
					bool flag2 = this.SettingDataAttribute != null;
					if (flag2)
					{
						result = (this.SettingDataAttribute.Description ?? "");
					}
					else
					{
						result = "";
					}
				}
				return result;
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x0003B4C8 File Offset: 0x000396C8
		public string SubGroup
		{
			get
			{
				bool flag = this.SettingReferenceAttribute != null;
				string result;
				if (flag)
				{
					result = (this.SettingReferenceAttribute.SubGroup ?? "");
				}
				else
				{
					bool flag2 = this.SettingDataAttribute != null;
					if (flag2)
					{
						result = (this.SettingDataAttribute.SubGroup ?? "");
					}
					else
					{
						result = "";
					}
				}
				return result;
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x0003B528 File Offset: 0x00039728
		public Group Group
		{
			get
			{
				bool flag = this.SettingReferenceAttribute != null;
				Group result;
				if (flag)
				{
					result = this.SettingReferenceAttribute.Group;
				}
				else
				{
					bool flag2 = this.SettingDataAttribute != null;
					if (flag2)
					{
						result = this.SettingDataAttribute.Group;
					}
					else
					{
						result = Group.UNKNOWN;
					}
				}
				return result;
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06002FF0 RID: 12272 RVA: 0x0003B571 File Offset: 0x00039771
		// (set) Token: 0x06002FF1 RID: 12273 RVA: 0x0003B579 File Offset: 0x00039779
		public Setting Setting { get; set; }

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06002FF2 RID: 12274 RVA: 0x0003B582 File Offset: 0x00039782
		// (set) Token: 0x06002FF3 RID: 12275 RVA: 0x0003B58A File Offset: 0x0003978A
		public ReferenceSettingAttribute SettingReferenceAttribute { get; set; }

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x0003B593 File Offset: 0x00039793
		// (set) Token: 0x06002FF5 RID: 12277 RVA: 0x0003B59B File Offset: 0x0003979B
		public SettingDataAttribute SettingDataAttribute { get; set; }
	}
}
