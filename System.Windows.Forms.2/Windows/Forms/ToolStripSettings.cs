using System;
using System.Configuration;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000402 RID: 1026
	internal partial class ToolStripSettings : ApplicationSettingsBase
	{
		// Token: 0x060046CD RID: 18125 RVA: 0x00128F52 File Offset: 0x00127152
		internal ToolStripSettings(string settingsKey) : base(settingsKey)
		{
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x060046D0 RID: 18128 RVA: 0x00128F80 File Offset: 0x00127180
		// (set) Token: 0x060046D1 RID: 18129 RVA: 0x00128F92 File Offset: 0x00127192
		[UserScopedSetting]
		public string ItemOrder
		{
			get
			{
				return this["ItemOrder"] as string;
			}
			set
			{
				this["ItemOrder"] = value;
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x00128FA0 File Offset: 0x001271A0
		// (set) Token: 0x060046D3 RID: 18131 RVA: 0x00128FB2 File Offset: 0x001271B2
		[UserScopedSetting]
		public string Name
		{
			get
			{
				return this["Name"] as string;
			}
			set
			{
				this["Name"] = value;
			}
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x060046D8 RID: 18136 RVA: 0x0012900A File Offset: 0x0012720A
		// (set) Token: 0x060046D9 RID: 18137 RVA: 0x0012901C File Offset: 0x0012721C
		[UserScopedSetting]
		public string ToolStripPanelName
		{
			get
			{
				return this["ToolStripPanelName"] as string;
			}
			set
			{
				this["ToolStripPanelName"] = value;
			}
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x0012904F File Offset: 0x0012724F
		public override void Save()
		{
			this.IsDefault = false;
			base.Save();
		}
	}
}
