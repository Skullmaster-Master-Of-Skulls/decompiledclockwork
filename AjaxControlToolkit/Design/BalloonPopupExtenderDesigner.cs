using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000052 RID: 82
	public class BalloonPopupExtenderDesigner : ExtenderControlBaseDesigner<BalloonPopupExtender>
	{
		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x060002E3 RID: 739
		[PageMethodSignature("Dynamic Populate", "DynamicServicePath", "DynamicServiceMethod")]
		private delegate string GetDynamicContent(string contextKey);
	}
}
