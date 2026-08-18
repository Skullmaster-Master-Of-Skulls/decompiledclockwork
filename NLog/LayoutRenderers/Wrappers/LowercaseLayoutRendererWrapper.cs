using System;
using System.ComponentModel;
using System.Globalization;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FF RID: 255
	[LayoutRenderer("lowercase")]
	[AmbientProperty("Lowercase")]
	[ThreadAgnostic]
	public sealed class LowercaseLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x0000FFA3 File Offset: 0x0000E1A3
		public LowercaseLayoutRendererWrapper()
		{
			this.Culture = CultureInfo.InvariantCulture;
			this.Lowercase = true;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0000FFBD File Offset: 0x0000E1BD
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0000FFC5 File Offset: 0x0000E1C5
		[DefaultValue(true)]
		public bool Lowercase { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0000FFCE File Offset: 0x0000E1CE
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public CultureInfo Culture { get; set; }

		// Token: 0x0600072D RID: 1837 RVA: 0x0000FFDF File Offset: 0x0000E1DF
		protected override string Transform(string text)
		{
			if (!this.Lowercase)
			{
				return text;
			}
			return text.ToLower(this.Culture);
		}
	}
}
