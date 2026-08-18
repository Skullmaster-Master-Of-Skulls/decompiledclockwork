using System;

namespace iTextSharp.text
{
	// Token: 0x0200026B RID: 619
	public class SpecialSymbol
	{
		// Token: 0x06001740 RID: 5952 RVA: 0x00085C6C File Offset: 0x00084C6C
		public static int Index(string str)
		{
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				if (SpecialSymbol.GetCorrespondingSymbol(str[i]) != ' ')
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00085CA0 File Offset: 0x00084CA0
		public static Chunk Get(char c, Font font)
		{
			char correspondingSymbol = SpecialSymbol.GetCorrespondingSymbol(c);
			if (correspondingSymbol == ' ')
			{
				return new Chunk(c.ToString(), font);
			}
			Font font2 = new Font(Font.FontFamily.SYMBOL, font.Size, font.Style, font.Color);
			return new Chunk(correspondingSymbol.ToString(), font2);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00085CF0 File Offset: 0x00084CF0
		public static char GetCorrespondingSymbol(char c)
		{
			switch (c)
			{
			case 'Α':
				return 'A';
			case 'Β':
				return 'B';
			case 'Γ':
				return 'G';
			case 'Δ':
				return 'D';
			case 'Ε':
				return 'E';
			case 'Ζ':
				return 'Z';
			case 'Η':
				return 'H';
			case 'Θ':
				return 'Q';
			case 'Ι':
				return 'I';
			case 'Κ':
				return 'K';
			case 'Λ':
				return 'L';
			case 'Μ':
				return 'M';
			case 'Ν':
				return 'N';
			case 'Ξ':
				return 'X';
			case 'Ο':
				return 'O';
			case 'Π':
				return 'P';
			case 'Ρ':
				return 'R';
			case 'Σ':
				return 'S';
			case 'Τ':
				return 'T';
			case 'Υ':
				return 'U';
			case 'Φ':
				return 'F';
			case 'Χ':
				return 'C';
			case 'Ψ':
				return 'Y';
			case 'Ω':
				return 'W';
			case 'α':
				return 'a';
			case 'β':
				return 'b';
			case 'γ':
				return 'g';
			case 'δ':
				return 'd';
			case 'ε':
				return 'e';
			case 'ζ':
				return 'z';
			case 'η':
				return 'h';
			case 'θ':
				return 'q';
			case 'ι':
				return 'i';
			case 'κ':
				return 'k';
			case 'λ':
				return 'l';
			case 'μ':
				return 'm';
			case 'ν':
				return 'n';
			case 'ξ':
				return 'x';
			case 'ο':
				return 'o';
			case 'π':
				return 'p';
			case 'ρ':
				return 'r';
			case 'ς':
				return 'V';
			case 'σ':
				return 's';
			case 'τ':
				return 't';
			case 'υ':
				return 'u';
			case 'φ':
				return 'f';
			case 'χ':
				return 'c';
			case 'ψ':
				return 'y';
			case 'ω':
				return 'w';
			}
			return ' ';
		}
	}
}
