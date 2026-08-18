using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200015C RID: 348
	public class PopupControlExtenderDesigner : ExtenderControlBaseDesigner<PopupControlExtender>
	{
		// Token: 0x0200015D RID: 349
		// (Invoke) Token: 0x06000933 RID: 2355
		[PageMethodSignature("Dynamic Populate", "DynamicServicePath", "DynamicServiceMethod")]
		private delegate string GetDynamicContent(string contextKey);
	}
}
