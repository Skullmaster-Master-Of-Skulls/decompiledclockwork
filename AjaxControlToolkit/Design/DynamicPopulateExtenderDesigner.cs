using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000082 RID: 130
	public class DynamicPopulateExtenderDesigner : ExtenderControlBaseDesigner<DynamicPopulateExtender>
	{
		// Token: 0x02000083 RID: 131
		// (Invoke) Token: 0x0600047C RID: 1148
		[PageMethodSignature("Dynamic Populate", "ServicePath", "ServiceMethod")]
		private delegate string GetDynamicContent(string contextKey);
	}
}
