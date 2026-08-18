using System;
using NLog.Conditions;
using NLog.Config;
using NLog.Layouts;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x0200010B RID: 267
	[ThreadAgnostic]
	[LayoutRenderer("when")]
	[AmbientProperty("When")]
	public sealed class WhenLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00010547 File Offset: 0x0000E747
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0001054F File Offset: 0x0000E74F
		[RequiredParameter]
		public ConditionExpression When { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00010558 File Offset: 0x0000E758
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x00010560 File Offset: 0x0000E760
		public Layout Else { get; set; }

		// Token: 0x0600076F RID: 1903 RVA: 0x00010569 File Offset: 0x0000E769
		protected override string Transform(string text)
		{
			return text;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001056C File Offset: 0x0000E76C
		protected override string RenderInner(LogEventInfo logEvent)
		{
			if (this.When == null || true.Equals(this.When.Evaluate(logEvent)))
			{
				return base.RenderInner(logEvent);
			}
			if (this.Else != null)
			{
				return this.Else.Render(logEvent);
			}
			return string.Empty;
		}
	}
}
