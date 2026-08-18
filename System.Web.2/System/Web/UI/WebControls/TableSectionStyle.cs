using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004EF RID: 1263
	public class TableSectionStyle : Style
	{
		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06003EF1 RID: 16113 RVA: 0x000CA540 File Offset: 0x000C8740
		// (set) Token: 0x06003EF2 RID: 16114 RVA: 0x000CA569 File Offset: 0x000C8769
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("TableSectionStyle_Visible")]
		[NotifyParentProperty(true)]
		public bool Visible
		{
			get
			{
				object obj = base.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}
	}
}
