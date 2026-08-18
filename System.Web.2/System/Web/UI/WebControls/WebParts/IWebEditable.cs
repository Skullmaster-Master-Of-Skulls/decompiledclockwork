using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000546 RID: 1350
	public interface IWebEditable
	{
		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x060044D1 RID: 17617
		object WebBrowsableObject { get; }

		// Token: 0x060044D2 RID: 17618
		EditorPartCollection CreateEditorParts();
	}
}
