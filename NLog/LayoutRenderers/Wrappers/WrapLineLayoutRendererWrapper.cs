using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x0200010C RID: 268
	[AmbientProperty("WrapLine")]
	[ThreadAgnostic]
	[LayoutRenderer("wrapline")]
	public sealed class WrapLineLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x000105C2 File Offset: 0x0000E7C2
		public WrapLineLayoutRendererWrapper()
		{
			this.WrapLine = 80;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x000105D2 File Offset: 0x0000E7D2
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x000105DA File Offset: 0x0000E7DA
		[DefaultValue(80)]
		public int WrapLine { get; set; }

		// Token: 0x06000775 RID: 1909 RVA: 0x000105E4 File Offset: 0x0000E7E4
		protected override string Transform(string text)
		{
			if (this.WrapLine <= 0)
			{
				return text;
			}
			int num = this.WrapLine;
			if (text.Length <= num)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length + text.Length / num * Environment.NewLine.Length);
			for (int i = 0; i < text.Length; i += num)
			{
				if (num + i > text.Length)
				{
					num = text.Length - i;
				}
				stringBuilder.Append(text.Substring(i, num));
				if (num + i < text.Length)
				{
					stringBuilder.Append(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
