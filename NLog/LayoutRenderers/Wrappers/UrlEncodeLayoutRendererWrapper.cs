using System;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000109 RID: 265
	[ThreadAgnostic]
	[LayoutRenderer("url-encode")]
	public sealed class UrlEncodeLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x000104D0 File Offset: 0x0000E6D0
		public UrlEncodeLayoutRendererWrapper()
		{
			this.SpaceAsPlus = true;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x000104DF File Offset: 0x0000E6DF
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x000104E7 File Offset: 0x0000E6E7
		public bool SpaceAsPlus { get; set; }

		// Token: 0x06000765 RID: 1893 RVA: 0x000104F0 File Offset: 0x0000E6F0
		protected override string Transform(string text)
		{
			return UrlHelper.UrlEncode(text, this.SpaceAsPlus);
		}
	}
}
