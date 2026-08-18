using System;
using System.Configuration;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000402 RID: 1026
	internal partial class ToolStripSettings : ApplicationSettingsBase
	{
		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060046CE RID: 18126 RVA: 0x00128F5B File Offset: 0x0012715B
		// (set) Token: 0x060046CF RID: 18127 RVA: 0x00128F6D File Offset: 0x0012716D
		[UserScopedSetting]
		[DefaultSettingValue("true")]
		public bool IsDefault
		{
			get
			{
				return (bool)this["IsDefault"];
			}
			set
			{
				this["IsDefault"] = value;
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x060046D4 RID: 18132 RVA: 0x00128FC0 File Offset: 0x001271C0
		// (set) Token: 0x060046D5 RID: 18133 RVA: 0x00128FD2 File Offset: 0x001271D2
		[UserScopedSetting]
		[DefaultSettingValue("0,0")]
		public Point Location
		{
			get
			{
				return (Point)this["Location"];
			}
			set
			{
				this["Location"] = value;
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x060046D6 RID: 18134 RVA: 0x00128FE5 File Offset: 0x001271E5
		// (set) Token: 0x060046D7 RID: 18135 RVA: 0x00128FF7 File Offset: 0x001271F7
		[UserScopedSetting]
		[DefaultSettingValue("0,0")]
		public Size Size
		{
			get
			{
				return (Size)this["Size"];
			}
			set
			{
				this["Size"] = value;
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x060046DA RID: 18138 RVA: 0x0012902A File Offset: 0x0012722A
		// (set) Token: 0x060046DB RID: 18139 RVA: 0x0012903C File Offset: 0x0012723C
		[UserScopedSetting]
		[DefaultSettingValue("true")]
		public bool Visible
		{
			get
			{
				return (bool)this["Visible"];
			}
			set
			{
				this["Visible"] = value;
			}
		}
	}
}
