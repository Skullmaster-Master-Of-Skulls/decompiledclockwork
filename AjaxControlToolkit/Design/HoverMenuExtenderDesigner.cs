using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x020000A6 RID: 166
	public class HoverMenuExtenderDesigner : ExtenderControlBaseDesigner<HoverMenuExtender>
	{
		// Token: 0x020000A7 RID: 167
		// (Invoke) Token: 0x06000506 RID: 1286
		[PageMethodSignature("Dynamic Populate", "DynamicServicePath", "DynamicServiceMethod")]
		private delegate string GetDynamicContent(string contextKey);
	}
}
