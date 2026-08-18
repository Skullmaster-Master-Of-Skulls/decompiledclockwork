using System;
using System.Collections.Generic;

namespace iTextSharp.text.xml.simpleparser
{
	// Token: 0x02000586 RID: 1414
	public class EntitiesToSymbol
	{
		// Token: 0x06003016 RID: 12310 RVA: 0x001280DC File Offset: 0x001270DC
		static EntitiesToSymbol()
		{
			EntitiesToSymbol.map["169"] = 'ã';
			EntitiesToSymbol.map["172"] = 'Ø';
			EntitiesToSymbol.map["174"] = 'Ò';
			EntitiesToSymbol.map["177"] = '±';
			EntitiesToSymbol.map["215"] = '´';
			EntitiesToSymbol.map["247"] = '¸';
			EntitiesToSymbol.map["8230"] = '¼';
			EntitiesToSymbol.map["8242"] = '¢';
			EntitiesToSymbol.map["8243"] = '²';
			EntitiesToSymbol.map["8260"] = '¤';
			EntitiesToSymbol.map["8364"] = 'ð';
			EntitiesToSymbol.map["8465"] = 'Á';
			EntitiesToSymbol.map["8472"] = 'Ã';
			EntitiesToSymbol.map["8476"] = 'Â';
			EntitiesToSymbol.map["8482"] = 'Ô';
			EntitiesToSymbol.map["8501"] = 'À';
			EntitiesToSymbol.map["8592"] = '¬';
			EntitiesToSymbol.map["8593"] = '­';
			EntitiesToSymbol.map["8594"] = '®';
			EntitiesToSymbol.map["8595"] = '¯';
			EntitiesToSymbol.map["8596"] = '«';
			EntitiesToSymbol.map["8629"] = '¿';
			EntitiesToSymbol.map["8656"] = 'Ü';
			EntitiesToSymbol.map["8657"] = 'Ý';
			EntitiesToSymbol.map["8658"] = 'Þ';
			EntitiesToSymbol.map["8659"] = 'ß';
			EntitiesToSymbol.map["8660"] = 'Û';
			EntitiesToSymbol.map["8704"] = '"';
			EntitiesToSymbol.map["8706"] = '¶';
			EntitiesToSymbol.map["8707"] = '$';
			EntitiesToSymbol.map["8709"] = 'Æ';
			EntitiesToSymbol.map["8711"] = 'Ñ';
			EntitiesToSymbol.map["8712"] = 'Î';
			EntitiesToSymbol.map["8713"] = 'Ï';
			EntitiesToSymbol.map["8717"] = '\'';
			EntitiesToSymbol.map["8719"] = 'Õ';
			EntitiesToSymbol.map["8721"] = 'å';
			EntitiesToSymbol.map["8722"] = '-';
			EntitiesToSymbol.map["8727"] = '*';
			EntitiesToSymbol.map["8729"] = '·';
			EntitiesToSymbol.map["8730"] = 'Ö';
			EntitiesToSymbol.map["8733"] = 'µ';
			EntitiesToSymbol.map["8734"] = '¥';
			EntitiesToSymbol.map["8736"] = 'Ð';
			EntitiesToSymbol.map["8743"] = 'Ù';
			EntitiesToSymbol.map["8744"] = 'Ú';
			EntitiesToSymbol.map["8745"] = 'Ç';
			EntitiesToSymbol.map["8746"] = 'È';
			EntitiesToSymbol.map["8747"] = 'ò';
			EntitiesToSymbol.map["8756"] = '\\';
			EntitiesToSymbol.map["8764"] = '~';
			EntitiesToSymbol.map["8773"] = '@';
			EntitiesToSymbol.map["8776"] = '»';
			EntitiesToSymbol.map["8800"] = '¹';
			EntitiesToSymbol.map["8801"] = 'º';
			EntitiesToSymbol.map["8804"] = '£';
			EntitiesToSymbol.map["8805"] = '³';
			EntitiesToSymbol.map["8834"] = 'Ì';
			EntitiesToSymbol.map["8835"] = 'É';
			EntitiesToSymbol.map["8836"] = 'Ë';
			EntitiesToSymbol.map["8838"] = 'Í';
			EntitiesToSymbol.map["8839"] = 'Ê';
			EntitiesToSymbol.map["8853"] = 'Å';
			EntitiesToSymbol.map["8855"] = 'Ä';
			EntitiesToSymbol.map["8869"] = '^';
			EntitiesToSymbol.map["8901"] = '×';
			EntitiesToSymbol.map["8992"] = 'ó';
			EntitiesToSymbol.map["8993"] = 'õ';
			EntitiesToSymbol.map["9001"] = 'á';
			EntitiesToSymbol.map["9002"] = 'ñ';
			EntitiesToSymbol.map["913"] = 'A';
			EntitiesToSymbol.map["914"] = 'B';
			EntitiesToSymbol.map["915"] = 'G';
			EntitiesToSymbol.map["916"] = 'D';
			EntitiesToSymbol.map["917"] = 'E';
			EntitiesToSymbol.map["918"] = 'Z';
			EntitiesToSymbol.map["919"] = 'H';
			EntitiesToSymbol.map["920"] = 'Q';
			EntitiesToSymbol.map["921"] = 'I';
			EntitiesToSymbol.map["922"] = 'K';
			EntitiesToSymbol.map["923"] = 'L';
			EntitiesToSymbol.map["924"] = 'M';
			EntitiesToSymbol.map["925"] = 'N';
			EntitiesToSymbol.map["926"] = 'X';
			EntitiesToSymbol.map["927"] = 'O';
			EntitiesToSymbol.map["928"] = 'P';
			EntitiesToSymbol.map["929"] = 'R';
			EntitiesToSymbol.map["931"] = 'S';
			EntitiesToSymbol.map["932"] = 'T';
			EntitiesToSymbol.map["933"] = 'U';
			EntitiesToSymbol.map["934"] = 'F';
			EntitiesToSymbol.map["935"] = 'C';
			EntitiesToSymbol.map["936"] = 'Y';
			EntitiesToSymbol.map["937"] = 'W';
			EntitiesToSymbol.map["945"] = 'a';
			EntitiesToSymbol.map["946"] = 'b';
			EntitiesToSymbol.map["947"] = 'g';
			EntitiesToSymbol.map["948"] = 'd';
			EntitiesToSymbol.map["949"] = 'e';
			EntitiesToSymbol.map["950"] = 'z';
			EntitiesToSymbol.map["951"] = 'h';
			EntitiesToSymbol.map["952"] = 'q';
			EntitiesToSymbol.map["953"] = 'i';
			EntitiesToSymbol.map["954"] = 'k';
			EntitiesToSymbol.map["955"] = 'l';
			EntitiesToSymbol.map["956"] = 'm';
			EntitiesToSymbol.map["957"] = 'n';
			EntitiesToSymbol.map["958"] = 'x';
			EntitiesToSymbol.map["959"] = 'o';
			EntitiesToSymbol.map["960"] = 'p';
			EntitiesToSymbol.map["961"] = 'r';
			EntitiesToSymbol.map["962"] = 'V';
			EntitiesToSymbol.map["963"] = 's';
			EntitiesToSymbol.map["964"] = 't';
			EntitiesToSymbol.map["965"] = 'u';
			EntitiesToSymbol.map["966"] = 'f';
			EntitiesToSymbol.map["967"] = 'c';
			EntitiesToSymbol.map["9674"] = 'à';
			EntitiesToSymbol.map["968"] = 'y';
			EntitiesToSymbol.map["969"] = 'w';
			EntitiesToSymbol.map["977"] = 'J';
			EntitiesToSymbol.map["978"] = '¡';
			EntitiesToSymbol.map["981"] = 'j';
			EntitiesToSymbol.map["982"] = 'v';
			EntitiesToSymbol.map["9824"] = 'ª';
			EntitiesToSymbol.map["9827"] = '§';
			EntitiesToSymbol.map["9829"] = '©';
			EntitiesToSymbol.map["9830"] = '¨';
			EntitiesToSymbol.map["Alpha"] = 'A';
			EntitiesToSymbol.map["Beta"] = 'B';
			EntitiesToSymbol.map["Chi"] = 'C';
			EntitiesToSymbol.map["Delta"] = 'D';
			EntitiesToSymbol.map["Epsilon"] = 'E';
			EntitiesToSymbol.map["Eta"] = 'H';
			EntitiesToSymbol.map["Gamma"] = 'G';
			EntitiesToSymbol.map["Iota"] = 'I';
			EntitiesToSymbol.map["Kappa"] = 'K';
			EntitiesToSymbol.map["Lambda"] = 'L';
			EntitiesToSymbol.map["Mu"] = 'M';
			EntitiesToSymbol.map["Nu"] = 'N';
			EntitiesToSymbol.map["Omega"] = 'W';
			EntitiesToSymbol.map["Omicron"] = 'O';
			EntitiesToSymbol.map["Phi"] = 'F';
			EntitiesToSymbol.map["Pi"] = 'P';
			EntitiesToSymbol.map["Prime"] = '²';
			EntitiesToSymbol.map["Psi"] = 'Y';
			EntitiesToSymbol.map["Rho"] = 'R';
			EntitiesToSymbol.map["Sigma"] = 'S';
			EntitiesToSymbol.map["Tau"] = 'T';
			EntitiesToSymbol.map["Theta"] = 'Q';
			EntitiesToSymbol.map["Upsilon"] = 'U';
			EntitiesToSymbol.map["Xi"] = 'X';
			EntitiesToSymbol.map["Zeta"] = 'Z';
			EntitiesToSymbol.map["alefsym"] = 'À';
			EntitiesToSymbol.map["alpha"] = 'a';
			EntitiesToSymbol.map["and"] = 'Ù';
			EntitiesToSymbol.map["ang"] = 'Ð';
			EntitiesToSymbol.map["asymp"] = '»';
			EntitiesToSymbol.map["beta"] = 'b';
			EntitiesToSymbol.map["cap"] = 'Ç';
			EntitiesToSymbol.map["chi"] = 'c';
			EntitiesToSymbol.map["clubs"] = '§';
			EntitiesToSymbol.map["cong"] = '@';
			EntitiesToSymbol.map["copy"] = 'Ó';
			EntitiesToSymbol.map["crarr"] = '¿';
			EntitiesToSymbol.map["cup"] = 'È';
			EntitiesToSymbol.map["dArr"] = 'ß';
			EntitiesToSymbol.map["darr"] = '¯';
			EntitiesToSymbol.map["delta"] = 'd';
			EntitiesToSymbol.map["diams"] = '¨';
			EntitiesToSymbol.map["divide"] = '¸';
			EntitiesToSymbol.map["empty"] = 'Æ';
			EntitiesToSymbol.map["epsilon"] = 'e';
			EntitiesToSymbol.map["equiv"] = 'º';
			EntitiesToSymbol.map["eta"] = 'h';
			EntitiesToSymbol.map["euro"] = 'ð';
			EntitiesToSymbol.map["exist"] = '$';
			EntitiesToSymbol.map["forall"] = '"';
			EntitiesToSymbol.map["frasl"] = '¤';
			EntitiesToSymbol.map["gamma"] = 'g';
			EntitiesToSymbol.map["ge"] = '³';
			EntitiesToSymbol.map["hArr"] = 'Û';
			EntitiesToSymbol.map["harr"] = '«';
			EntitiesToSymbol.map["hearts"] = '©';
			EntitiesToSymbol.map["hellip"] = '¼';
			EntitiesToSymbol.map["horizontal arrow extender"] = '¾';
			EntitiesToSymbol.map["image"] = 'Á';
			EntitiesToSymbol.map["infin"] = '¥';
			EntitiesToSymbol.map["int"] = 'ò';
			EntitiesToSymbol.map["iota"] = 'i';
			EntitiesToSymbol.map["isin"] = 'Î';
			EntitiesToSymbol.map["kappa"] = 'k';
			EntitiesToSymbol.map["lArr"] = 'Ü';
			EntitiesToSymbol.map["lambda"] = 'l';
			EntitiesToSymbol.map["lang"] = 'á';
			EntitiesToSymbol.map["large brace extender"] = 'ï';
			EntitiesToSymbol.map["large integral extender"] = 'ô';
			EntitiesToSymbol.map["large left brace (bottom)"] = 'î';
			EntitiesToSymbol.map["large left brace (middle)"] = 'í';
			EntitiesToSymbol.map["large left brace (top)"] = 'ì';
			EntitiesToSymbol.map["large left bracket (bottom)"] = 'ë';
			EntitiesToSymbol.map["large left bracket (extender)"] = 'ê';
			EntitiesToSymbol.map["large left bracket (top)"] = 'é';
			EntitiesToSymbol.map["large left parenthesis (bottom)"] = 'è';
			EntitiesToSymbol.map["large left parenthesis (extender)"] = 'ç';
			EntitiesToSymbol.map["large left parenthesis (top)"] = 'æ';
			EntitiesToSymbol.map["large right brace (bottom)"] = 'þ';
			EntitiesToSymbol.map["large right brace (middle)"] = 'ý';
			EntitiesToSymbol.map["large right brace (top)"] = 'ü';
			EntitiesToSymbol.map["large right bracket (bottom)"] = 'û';
			EntitiesToSymbol.map["large right bracket (extender)"] = 'ú';
			EntitiesToSymbol.map["large right bracket (top)"] = 'ù';
			EntitiesToSymbol.map["large right parenthesis (bottom)"] = 'ø';
			EntitiesToSymbol.map["large right parenthesis (extender)"] = '÷';
			EntitiesToSymbol.map["large right parenthesis (top)"] = 'ö';
			EntitiesToSymbol.map["larr"] = '¬';
			EntitiesToSymbol.map["le"] = '£';
			EntitiesToSymbol.map["lowast"] = '*';
			EntitiesToSymbol.map["loz"] = 'à';
			EntitiesToSymbol.map["minus"] = '-';
			EntitiesToSymbol.map["mu"] = 'm';
			EntitiesToSymbol.map["nabla"] = 'Ñ';
			EntitiesToSymbol.map["ne"] = '¹';
			EntitiesToSymbol.map["not"] = 'Ø';
			EntitiesToSymbol.map["notin"] = 'Ï';
			EntitiesToSymbol.map["nsub"] = 'Ë';
			EntitiesToSymbol.map["nu"] = 'n';
			EntitiesToSymbol.map["omega"] = 'w';
			EntitiesToSymbol.map["omicron"] = 'o';
			EntitiesToSymbol.map["oplus"] = 'Å';
			EntitiesToSymbol.map["or"] = 'Ú';
			EntitiesToSymbol.map["otimes"] = 'Ä';
			EntitiesToSymbol.map["part"] = '¶';
			EntitiesToSymbol.map["perp"] = '^';
			EntitiesToSymbol.map["phi"] = 'f';
			EntitiesToSymbol.map["pi"] = 'p';
			EntitiesToSymbol.map["piv"] = 'v';
			EntitiesToSymbol.map["plusmn"] = '±';
			EntitiesToSymbol.map["prime"] = '¢';
			EntitiesToSymbol.map["prod"] = 'Õ';
			EntitiesToSymbol.map["prop"] = 'µ';
			EntitiesToSymbol.map["psi"] = 'y';
			EntitiesToSymbol.map["rArr"] = 'Þ';
			EntitiesToSymbol.map["radic"] = 'Ö';
			EntitiesToSymbol.map["radical extender"] = '`';
			EntitiesToSymbol.map["rang"] = 'ñ';
			EntitiesToSymbol.map["rarr"] = '®';
			EntitiesToSymbol.map["real"] = 'Â';
			EntitiesToSymbol.map["reg"] = 'Ò';
			EntitiesToSymbol.map["rho"] = 'r';
			EntitiesToSymbol.map["sdot"] = '×';
			EntitiesToSymbol.map["sigma"] = 's';
			EntitiesToSymbol.map["sigmaf"] = 'V';
			EntitiesToSymbol.map["sim"] = '~';
			EntitiesToSymbol.map["spades"] = 'ª';
			EntitiesToSymbol.map["sub"] = 'Ì';
			EntitiesToSymbol.map["sube"] = 'Í';
			EntitiesToSymbol.map["sum"] = 'å';
			EntitiesToSymbol.map["sup"] = 'É';
			EntitiesToSymbol.map["supe"] = 'Ê';
			EntitiesToSymbol.map["tau"] = 't';
			EntitiesToSymbol.map["there4"] = '\\';
			EntitiesToSymbol.map["theta"] = 'q';
			EntitiesToSymbol.map["thetasym"] = 'J';
			EntitiesToSymbol.map["times"] = '´';
			EntitiesToSymbol.map["trade"] = 'Ô';
			EntitiesToSymbol.map["uArr"] = 'Ý';
			EntitiesToSymbol.map["uarr"] = '­';
			EntitiesToSymbol.map["upsih"] = '¡';
			EntitiesToSymbol.map["upsilon"] = 'u';
			EntitiesToSymbol.map["vertical arrow extender"] = '½';
			EntitiesToSymbol.map["weierp"] = 'Ã';
			EntitiesToSymbol.map["xi"] = 'x';
			EntitiesToSymbol.map["zeta"] = 'z';
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x001294F0 File Offset: 0x001284F0
		public static Chunk Get(string e, Font font)
		{
			char correspondingSymbol = EntitiesToSymbol.GetCorrespondingSymbol(e);
			if (correspondingSymbol == '\0')
			{
				try
				{
					return new Chunk("" + (char)int.Parse(e), font);
				}
				catch (Exception)
				{
					return new Chunk(e, font);
				}
			}
			Font font2 = new Font(Font.FontFamily.SYMBOL, font.Size, font.Style, font.Color);
			return new Chunk(correspondingSymbol.ToString(), font2);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x0012956C File Offset: 0x0012856C
		public static char GetCorrespondingSymbol(string name)
		{
			if (EntitiesToSymbol.map.ContainsKey(name))
			{
				return EntitiesToSymbol.map[name];
			}
			return '\0';
		}

		// Token: 0x0400210A RID: 8458
		public static readonly Dictionary<string, char> map = new Dictionary<string, char>();
	}
}
