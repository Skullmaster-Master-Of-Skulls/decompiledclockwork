using System;
using System.Text;
using NLog.Config;
using NLog.Layouts;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FA RID: 250
	public abstract class WrapperLayoutRendererBase : LayoutRenderer
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0000FCAE File Offset: 0x0000DEAE
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0000FCB6 File Offset: 0x0000DEB6
		[DefaultParameter]
		public Layout Inner { get; set; }

		// Token: 0x06000710 RID: 1808 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text = this.RenderInner(logEvent);
			builder.Append(this.Transform(text));
		}

		// Token: 0x06000711 RID: 1809
		protected abstract string Transform(string text);

		// Token: 0x06000712 RID: 1810 RVA: 0x0000FCE3 File Offset: 0x0000DEE3
		protected virtual string RenderInner(LogEventInfo logEvent)
		{
			return this.Inner.Render(logEvent);
		}
	}
}
