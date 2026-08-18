using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000107 RID: 263
	[AmbientProperty("TrimWhiteSpace")]
	[ThreadAgnostic]
	[LayoutRenderer("trim-whitespace")]
	public sealed class TrimWhiteSpaceLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x0001044A File Offset: 0x0000E64A
		public TrimWhiteSpaceLayoutRendererWrapper()
		{
			this.TrimWhiteSpace = true;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00010459 File Offset: 0x0000E659
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x00010461 File Offset: 0x0000E661
		[DefaultValue(true)]
		public bool TrimWhiteSpace { get; set; }

		// Token: 0x0600075B RID: 1883 RVA: 0x0001046A File Offset: 0x0000E66A
		protected override string Transform(string text)
		{
			if (!this.TrimWhiteSpace)
			{
				return text;
			}
			return text.Trim();
		}
	}
}
