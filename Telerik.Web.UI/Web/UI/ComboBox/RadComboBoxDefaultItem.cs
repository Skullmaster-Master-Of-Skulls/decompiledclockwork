using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.ComboBox
{
	// Token: 0x02000A12 RID: 2578
	[EditorBrowsable(EditorBrowsableState.Never)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public class RadComboBoxDefaultItem : RadComboBoxItem
	{
		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x060061C4 RID: 25028 RVA: 0x00170A8B File Offset: 0x0016EC8B
		// (set) Token: 0x060061C5 RID: 25029 RVA: 0x00170A93 File Offset: 0x0016EC93
		[DefaultValue("")]
		[Description("The display text of the default item.")]
		[Localizable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x060061C6 RID: 25030 RVA: 0x00170A9C File Offset: 0x0016EC9C
		// (set) Token: 0x060061C7 RID: 25031 RVA: 0x00170AA4 File Offset: 0x0016ECA4
		[Category("Misc")]
		[Description("The value of the default item")]
		[Localizable(false)]
		[DefaultValue("")]
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x060061C8 RID: 25032 RVA: 0x00170AAD File Offset: 0x0016ECAD
		// (set) Token: 0x060061C9 RID: 25033 RVA: 0x00170AB5 File Offset: 0x0016ECB5
		[Localizable(false)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
			}
		}

		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x060061CA RID: 25034 RVA: 0x00170ABE File Offset: 0x0016ECBE
		// (set) Token: 0x060061CB RID: 25035 RVA: 0x00170AC6 File Offset: 0x0016ECC6
		[Localizable(false)]
		public override string ToolTip
		{
			get
			{
				return base.ToolTip;
			}
			set
			{
				base.ToolTip = value;
			}
		}
	}
}
