using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x020001A3 RID: 419
	public class SlideShowExtenderDesigner : ExtenderControlBaseDesigner<SlideShowExtender>
	{
		// Token: 0x020001A4 RID: 420
		// (Invoke) Token: 0x06000C1E RID: 3102
		[PageMethodSignature("SlideShow", "SlideShowServicePath", "SlideShowServiceMethod", "UseContextKey")]
		private delegate Slide[] GetSlides(string contextKey);
	}
}
