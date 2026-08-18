using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200007C RID: 124
	public class DropDownExtenderDesigner : ExtenderControlBaseDesigner<DropDownExtender>
	{
		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x06000440 RID: 1088
		[PageMethodSignature("Dynamic Populate", "DynamicServicePath", "DynamicServiceMethod")]
		private delegate string GetDynamicContent(string contextKey);
	}
}
