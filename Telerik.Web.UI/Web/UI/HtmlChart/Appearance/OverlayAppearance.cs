using System;
using System.ComponentModel;
using Telerik.Web.UI.HtmlChart.Enums;

namespace Telerik.Web.UI.HtmlChart.Appearance
{
	// Token: 0x020003A8 RID: 936
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class OverlayAppearance : StateManager, IDefaultCheck
	{
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x000752B1 File Offset: 0x000734B1
		// (set) Token: 0x060022FE RID: 8958 RVA: 0x000752D2 File Offset: 0x000734D2
		[DefaultValue(Gradients.Glass)]
		public Gradients Gradient
		{
			get
			{
				return (Gradients)(base.ViewState["Gradient"] ?? Gradients.Glass);
			}
			set
			{
				base.ViewState["Gradient"] = value;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x000752EA File Offset: 0x000734EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public bool IsDefault
		{
			get
			{
				return this.Gradient == Gradients.Glass;
			}
		}
	}
}
