using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FE RID: 254
	[AmbientProperty("JsonEncode")]
	[ThreadAgnostic]
	[LayoutRenderer("json-encode")]
	public sealed class JsonEncodeLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000722 RID: 1826 RVA: 0x0000FE27 File Offset: 0x0000E027
		public JsonEncodeLayoutRendererWrapper()
		{
			this.JsonEncode = true;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0000FE36 File Offset: 0x0000E036
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0000FE3E File Offset: 0x0000E03E
		[DefaultValue(true)]
		public bool JsonEncode { get; set; }

		// Token: 0x06000725 RID: 1829 RVA: 0x0000FE47 File Offset: 0x0000E047
		protected override string Transform(string text)
		{
			if (!this.JsonEncode)
			{
				return text;
			}
			return JsonEncodeLayoutRendererWrapper.DoJsonEscape(text);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000FE5C File Offset: 0x0000E05C
		private static string DoJsonEscape(string text)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			int i = 0;
			while (i < text.Length)
			{
				char c = text[i];
				if (c <= '"')
				{
					switch (c)
					{
					case '\b':
						stringBuilder.Append("\\b");
						break;
					case '\t':
						stringBuilder.Append("\\t");
						break;
					case '\n':
						stringBuilder.Append("\\n");
						break;
					case '\v':
						goto IL_CF;
					case '\f':
						stringBuilder.Append("\\f");
						break;
					case '\r':
						stringBuilder.Append("\\r");
						break;
					default:
						if (c != '"')
						{
							goto IL_CF;
						}
						stringBuilder.Append("\\\"");
						break;
					}
				}
				else if (c != '/')
				{
					if (c != '\\')
					{
						goto IL_CF;
					}
					stringBuilder.Append("\\\\");
				}
				else
				{
					stringBuilder.Append("\\/");
				}
				IL_116:
				i++;
				continue;
				IL_CF:
				if (JsonEncodeLayoutRendererWrapper.NeedsEscaping(text[i]))
				{
					stringBuilder.Append("\\u");
					stringBuilder.Append(Convert.ToString((int)text[i], 16).PadLeft(4, '0'));
					goto IL_116;
				}
				stringBuilder.Append(text[i]);
				goto IL_116;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000FF95 File Offset: 0x0000E195
		private static bool NeedsEscaping(char ch)
		{
			return ch < ' ' || ch > '\u007f';
		}
	}
}
