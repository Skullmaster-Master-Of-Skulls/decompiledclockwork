using System;
using System.ComponentModel;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x02000190 RID: 400
	[ControlBuilder(typeof(ViewTypeControlBuilder))]
	[NonVisualControl]
	public class ViewType : Control
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0001E2F8 File Offset: 0x0001C4F8
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x0001E309 File Offset: 0x0001C509
		[DefaultValue("")]
		public string TypeName
		{
			get
			{
				return this._typeName ?? string.Empty;
			}
			set
			{
				this._typeName = value;
			}
		}

		// Token: 0x04000307 RID: 775
		private string _typeName;
	}
}
