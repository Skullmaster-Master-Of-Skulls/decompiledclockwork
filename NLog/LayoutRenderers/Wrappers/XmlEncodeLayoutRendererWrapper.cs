using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x0200010D RID: 269
	[ThreadAgnostic]
	[LayoutRenderer("xml-encode")]
	[AmbientProperty("XmlEncode")]
	public sealed class XmlEncodeLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x0001067F File Offset: 0x0000E87F
		public XmlEncodeLayoutRendererWrapper()
		{
			this.XmlEncode = true;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001068E File Offset: 0x0000E88E
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x00010696 File Offset: 0x0000E896
		[DefaultValue(true)]
		public bool XmlEncode { get; set; }

		// Token: 0x06000779 RID: 1913 RVA: 0x0001069F File Offset: 0x0000E89F
		protected override string Transform(string text)
		{
			if (!this.XmlEncode)
			{
				return text;
			}
			return XmlEncodeLayoutRendererWrapper.DoXmlEscape(text);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000106B4 File Offset: 0x0000E8B4
		private static string DoXmlEscape(string text)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (c != '"')
				{
					switch (c)
					{
					case '&':
						stringBuilder.Append("&amp;");
						break;
					case '\'':
						stringBuilder.Append("&apos;");
						break;
					default:
						switch (c)
						{
						case '<':
							stringBuilder.Append("&lt;");
							goto IL_9C;
						case '>':
							stringBuilder.Append("&gt;");
							goto IL_9C;
						}
						stringBuilder.Append(text[i]);
						break;
					}
				}
				else
				{
					stringBuilder.Append("&quot;");
				}
				IL_9C:;
			}
			return stringBuilder.ToString();
		}
	}
}
