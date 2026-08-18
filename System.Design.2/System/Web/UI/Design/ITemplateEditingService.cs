using System;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000059 RID: 89
	[Obsolete("Use of this type is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
	public interface ITemplateEditingService
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002D0 RID: 720
		bool SupportsNestedTemplateEditing { get; }

		// Token: 0x060002D1 RID: 721
		ITemplateEditingFrame CreateFrame(TemplatedControlDesigner designer, string frameName, string[] templateNames);

		// Token: 0x060002D2 RID: 722
		ITemplateEditingFrame CreateFrame(TemplatedControlDesigner designer, string frameName, string[] templateNames, Style controlStyle, Style[] templateStyles);

		// Token: 0x060002D3 RID: 723
		string GetContainingTemplateName(Control control);
	}
}
