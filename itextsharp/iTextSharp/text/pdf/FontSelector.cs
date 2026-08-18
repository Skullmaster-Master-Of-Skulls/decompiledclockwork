using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200005B RID: 91
	public class FontSelector
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000CCC8 File Offset: 0x0000BCC8
		public void AddFont(Font font)
		{
			if (font.BaseFont != null)
			{
				this.fonts.Add(font);
				return;
			}
			BaseFont calculatedBaseFont = font.GetCalculatedBaseFont(true);
			Font item = new Font(calculatedBaseFont, font.Size, font.CalculatedStyle, font.Color);
			this.fonts.Add(item);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000CD18 File Offset: 0x0000BD18
		public Phrase Process(string text)
		{
			int count = this.fonts.Count;
			if (count == 0)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("no.font.is.defined"));
			}
			char[] array = text.ToCharArray();
			int num = array.Length;
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = -1;
			Phrase phrase = new Phrase();
			for (int i = 0; i < num; i++)
			{
				char c = array[i];
				if (c == '\n' || c == '\r')
				{
					stringBuilder.Append(c);
				}
				else if (Utilities.IsSurrogatePair(array, i))
				{
					int c2 = Utilities.ConvertToUtf32(array, i);
					for (int j = 0; j < count; j++)
					{
						Font font = this.fonts[j];
						if (font.BaseFont.CharExists(c2))
						{
							if (num2 != j)
							{
								if (stringBuilder.Length > 0 && num2 != -1)
								{
									Chunk element = new Chunk(stringBuilder.ToString(), this.fonts[num2]);
									phrase.Add(element);
									stringBuilder.Length = 0;
								}
								num2 = j;
							}
							stringBuilder.Append(c);
							stringBuilder.Append(array[++i]);
							break;
						}
					}
				}
				else
				{
					for (int k = 0; k < count; k++)
					{
						Font font = this.fonts[k];
						if (font.BaseFont.CharExists((int)c))
						{
							if (num2 != k)
							{
								if (stringBuilder.Length > 0 && num2 != -1)
								{
									Chunk element2 = new Chunk(stringBuilder.ToString(), this.fonts[num2]);
									phrase.Add(element2);
									stringBuilder.Length = 0;
								}
								num2 = k;
							}
							stringBuilder.Append(c);
							break;
						}
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				Chunk element3 = new Chunk(stringBuilder.ToString(), this.fonts[(num2 == -1) ? 0 : num2]);
				phrase.Add(element3);
			}
			return phrase;
		}

		// Token: 0x04000145 RID: 325
		protected List<Font> fonts = new List<Font>();
	}
}
