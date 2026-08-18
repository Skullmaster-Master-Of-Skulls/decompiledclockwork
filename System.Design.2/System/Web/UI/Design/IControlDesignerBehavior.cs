using System;

namespace System.Web.UI.Design
{
	// Token: 0x0200005A RID: 90
	[Obsolete("The recommended alternative is System.Web.UI.Design.IControlDesignerTag and System.Web.UI.Design.IControlDesignerView. http://go.microsoft.com/fwlink/?linkid=14202")]
	public interface IControlDesignerBehavior
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002D4 RID: 724
		object DesignTimeElementView { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002D5 RID: 725
		// (set) Token: 0x060002D6 RID: 726
		string DesignTimeHtml { get; set; }

		// Token: 0x060002D7 RID: 727
		void OnTemplateModeChanged();
	}
}
