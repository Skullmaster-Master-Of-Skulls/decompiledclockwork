using System;
using System.ComponentModel;
using System.Globalization;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000108 RID: 264
	[ThreadAgnostic]
	[LayoutRenderer("uppercase")]
	[AmbientProperty("Uppercase")]
	public sealed class UppercaseLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0001047C File Offset: 0x0000E67C
		public UppercaseLayoutRendererWrapper()
		{
			this.Culture = CultureInfo.InvariantCulture;
			this.Uppercase = true;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00010496 File Offset: 0x0000E696
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0001049E File Offset: 0x0000E69E
		[DefaultValue(true)]
		public bool Uppercase { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x000104A7 File Offset: 0x0000E6A7
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x000104AF File Offset: 0x0000E6AF
		public CultureInfo Culture { get; set; }

		// Token: 0x06000761 RID: 1889 RVA: 0x000104B8 File Offset: 0x0000E6B8
		protected override string Transform(string text)
		{
			if (!this.Uppercase)
			{
				return text;
			}
			return text.ToUpper(this.Culture);
		}
	}
}
