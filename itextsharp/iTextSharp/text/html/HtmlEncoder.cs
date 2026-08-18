using System;
using System.Text;

namespace iTextSharp.text.html
{
	// Token: 0x02000334 RID: 820
	public sealed class HtmlEncoder
	{
		// Token: 0x06001D9A RID: 7578 RVA: 0x000B1C54 File Offset: 0x000B0C54
		static HtmlEncoder()
		{
			for (int i = 0; i < 10; i++)
			{
				HtmlEncoder.htmlCode[i] = "&#00" + i + ";";
			}
			for (int j = 10; j < 32; j++)
			{
				HtmlEncoder.htmlCode[j] = "&#0" + j + ";";
			}
			for (int k = 32; k < 128; k++)
			{
				HtmlEncoder.htmlCode[k] = ((char)k).ToString();
			}
			HtmlEncoder.htmlCode[9] = "\t";
			HtmlEncoder.htmlCode[10] = "<br />\n";
			HtmlEncoder.htmlCode[34] = "&quot;";
			HtmlEncoder.htmlCode[38] = "&amp;";
			HtmlEncoder.htmlCode[60] = "&lt;";
			HtmlEncoder.htmlCode[62] = "&gt;";
			for (int l = 128; l < 256; l++)
			{
				HtmlEncoder.htmlCode[l] = "&#" + l + ";";
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000B1D64 File Offset: 0x000B0D64
		private HtmlEncoder()
		{
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000B1D6C File Offset: 0x000B0D6C
		public static string Encode(string str)
		{
			int length = str.Length;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				if (c < 'Ā')
				{
					stringBuilder.Append(HtmlEncoder.htmlCode[(int)c]);
				}
				else
				{
					stringBuilder.Append("&#").Append((int)c).Append(';');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000B1DD4 File Offset: 0x000B0DD4
		public static string Encode(BaseColor color)
		{
			StringBuilder stringBuilder = new StringBuilder("#");
			if (color.R < 16)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(Convert.ToString(color.R, 16));
			if (color.G < 16)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(Convert.ToString(color.G, 16));
			if (color.B < 16)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(Convert.ToString(color.B, 16));
			return stringBuilder.ToString();
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000B1E68 File Offset: 0x000B0E68
		public static string GetAlignment(int alignment)
		{
			switch (alignment)
			{
			case 0:
				return "Left";
			case 1:
				return "Center";
			case 2:
				return "Right";
			case 3:
			case 8:
				return "Justify";
			case 4:
				return "Top";
			case 5:
				return "Middle";
			case 6:
				return "Bottom";
			case 7:
				return "Baseline";
			default:
				return "";
			}
		}

		// Token: 0x04001453 RID: 5203
		private static string[] htmlCode = new string[256];
	}
}
