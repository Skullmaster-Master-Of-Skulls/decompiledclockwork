using System;
using System.Collections.Generic;
using System.Text;
using NLog.Config;
using NLog.Layouts;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F8 RID: 248
	[LayoutRenderer("var")]
	public class VariableLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		[DefaultParameter]
		[RequiredParameter]
		public string Name { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0000FB05 File Offset: 0x0000DD05
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0000FB0D File Offset: 0x0000DD0D
		public string Default { get; set; }

		// Token: 0x06000704 RID: 1796 RVA: 0x0000FB18 File Offset: 0x0000DD18
		protected override void InitializeLayoutRenderer()
		{
			SimpleLayout simpleLayout;
			if (this.TryGetLayout(out simpleLayout) && simpleLayout != null)
			{
				simpleLayout.Initialize(base.LoggingConfiguration);
			}
			base.InitializeLayoutRenderer();
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000FB44 File Offset: 0x0000DD44
		private bool TryGetLayout(out SimpleLayout layout)
		{
			if (this.Name != null)
			{
				LoggingConfiguration loggingConfiguration = base.LoggingConfiguration;
				IDictionary<string, SimpleLayout> dictionary = (loggingConfiguration != null) ? loggingConfiguration.Variables : null;
				if (dictionary != null && dictionary.TryGetValue(this.Name, out layout))
				{
					return true;
				}
			}
			layout = null;
			return false;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000FB88 File Offset: 0x0000DD88
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this.Name != null)
			{
				SimpleLayout simpleLayout;
				if (this.TryGetLayout(out simpleLayout))
				{
					if (simpleLayout != null)
					{
						builder.Append(simpleLayout.Render(logEvent));
						return;
					}
				}
				else if (this.Default != null)
				{
					builder.Append(this.Default);
				}
			}
		}
	}
}
