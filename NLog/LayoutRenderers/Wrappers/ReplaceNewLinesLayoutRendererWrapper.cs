using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000105 RID: 261
	[ThreadAgnostic]
	[LayoutRenderer("replace-newlines")]
	[AmbientProperty("ReplaceNewLines")]
	public sealed class ReplaceNewLinesLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x0001035F File Offset: 0x0000E55F
		public ReplaceNewLinesLayoutRendererWrapper()
		{
			this.Replacement = " ";
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00010372 File Offset: 0x0000E572
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001037A File Offset: 0x0000E57A
		[DefaultValue(" ")]
		public string Replacement { get; set; }

		// Token: 0x06000751 RID: 1873 RVA: 0x00010383 File Offset: 0x0000E583
		protected override string Transform(string text)
		{
			return text.Replace(Environment.NewLine, this.Replacement);
		}
	}
}
