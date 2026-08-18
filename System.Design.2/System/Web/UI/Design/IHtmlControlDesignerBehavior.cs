using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000055 RID: 85
	[Obsolete("The recommended alternative is System.Web.UI.Design.IControlDesignerTag and System.Web.UI.Design.IControlDesignerView. http://go.microsoft.com/fwlink/?linkid=14202")]
	public interface IHtmlControlDesignerBehavior
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002B1 RID: 689
		// (set) Token: 0x060002B2 RID: 690
		HtmlControlDesigner Designer { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002B3 RID: 691
		object DesignTimeElement { get; }

		// Token: 0x060002B4 RID: 692
		object GetAttribute(string attribute, bool ignoreCase);

		// Token: 0x060002B5 RID: 693
		void RemoveAttribute(string attribute, bool ignoreCase);

		// Token: 0x060002B6 RID: 694
		void SetAttribute(string attribute, object value, bool ignoreCase);

		// Token: 0x060002B7 RID: 695
		object GetStyleAttribute(string attribute, bool designTimeOnly, bool ignoreCase);

		// Token: 0x060002B8 RID: 696
		void RemoveStyleAttribute(string attribute, bool designTimeOnly, bool ignoreCase);

		// Token: 0x060002B9 RID: 697
		void SetStyleAttribute(string attribute, bool designTimeOnly, object value, bool ignoreCase);
	}
}
