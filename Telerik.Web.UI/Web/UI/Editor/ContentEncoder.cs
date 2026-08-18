using System;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02001061 RID: 4193
	public class ContentEncoder
	{
		// Token: 0x0600A92C RID: 43308 RVA: 0x0024BD80 File Offset: 0x00249F80
		public static string Encode(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				for (int i = 0; i < ContentEncoder.characters.Length; i++)
				{
					text = text.Replace(ContentEncoder.characters[i].ToString(), ContentEncoder.percentFormattedStrings[i]);
				}
			}
			return text;
		}

		// Token: 0x0600A92D RID: 43309 RVA: 0x0024BDC8 File Offset: 0x00249FC8
		public static string Decode(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				for (int i = ContentEncoder.characters.Length - 1; i >= 0; i--)
				{
					text = text.Replace(ContentEncoder.percentFormattedStrings[i], ContentEncoder.characters[i].ToString());
				}
			}
			return text;
		}

		// Token: 0x04002DB3 RID: 11699
		private static readonly char[] characters = new char[]
		{
			'%',
			'<',
			'>',
			'!',
			'"',
			'#',
			'$',
			'&',
			'\'',
			'(',
			')',
			',',
			':',
			';',
			'=',
			'?',
			'[',
			'\\',
			']',
			'^',
			'`',
			'{',
			'|',
			'}',
			'~',
			'+'
		};

		// Token: 0x04002DB4 RID: 11700
		private static readonly string[] percentFormattedStrings = new string[]
		{
			"%25",
			"%3c",
			"%3e",
			"%21",
			"%22",
			"%23",
			"%24",
			"%26",
			"%27",
			"%28",
			"%29",
			"%2c",
			"%3a",
			"%3b",
			"%3d",
			"%3f",
			"%5b",
			"%5c",
			"%5d",
			"%5e",
			"%60",
			"%7b",
			"%7c",
			"%7d",
			"%7e",
			"%2b"
		};
	}
}
