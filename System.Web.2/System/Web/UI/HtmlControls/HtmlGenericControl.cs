using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000347 RID: 839
	[ConstructorNeedsTag(true)]
	public class HtmlGenericControl : HtmlContainerControl
	{
		// Token: 0x060026A6 RID: 9894 RVA: 0x0007EA38 File Offset: 0x0007CC38
		public HtmlGenericControl() : this("span")
		{
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x0007EA45 File Offset: 0x0007CC45
		public HtmlGenericControl(string tag)
		{
			if (tag == null)
			{
				tag = string.Empty;
			}
			this._tagName = tag;
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x0007E8BC File Offset: 0x0007CABC
		// (set) Token: 0x060026A9 RID: 9897 RVA: 0x0007EA5E File Offset: 0x0007CC5E
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string TagName
		{
			get
			{
				return this._tagName;
			}
			set
			{
				this._tagName = value;
			}
		}
	}
}
