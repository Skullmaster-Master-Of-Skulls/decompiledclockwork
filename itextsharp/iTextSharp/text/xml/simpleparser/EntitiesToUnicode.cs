using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace iTextSharp.text.xml.simpleparser
{
	// Token: 0x020004DB RID: 1243
	public class EntitiesToUnicode
	{
		// Token: 0x06002A48 RID: 10824 RVA: 0x00100994 File Offset: 0x000FF994
		static EntitiesToUnicode()
		{
			EntitiesToUnicode.map["nbsp"] = '\u00a0';
			EntitiesToUnicode.map["iexcl"] = '¡';
			EntitiesToUnicode.map["cent"] = '¢';
			EntitiesToUnicode.map["pound"] = '£';
			EntitiesToUnicode.map["curren"] = '¤';
			EntitiesToUnicode.map["yen"] = '¥';
			EntitiesToUnicode.map["brvbar"] = '¦';
			EntitiesToUnicode.map["sect"] = '§';
			EntitiesToUnicode.map["uml"] = '¨';
			EntitiesToUnicode.map["copy"] = '©';
			EntitiesToUnicode.map["ordf"] = 'ª';
			EntitiesToUnicode.map["laquo"] = '«';
			EntitiesToUnicode.map["not"] = '¬';
			EntitiesToUnicode.map["shy"] = '­';
			EntitiesToUnicode.map["reg"] = '®';
			EntitiesToUnicode.map["macr"] = '¯';
			EntitiesToUnicode.map["deg"] = '°';
			EntitiesToUnicode.map["plusmn"] = '±';
			EntitiesToUnicode.map["sup2"] = '²';
			EntitiesToUnicode.map["sup3"] = '³';
			EntitiesToUnicode.map["acute"] = '´';
			EntitiesToUnicode.map["micro"] = 'µ';
			EntitiesToUnicode.map["para"] = '¶';
			EntitiesToUnicode.map["middot"] = '·';
			EntitiesToUnicode.map["cedil"] = '¸';
			EntitiesToUnicode.map["sup1"] = '¹';
			EntitiesToUnicode.map["ordm"] = 'º';
			EntitiesToUnicode.map["raquo"] = '»';
			EntitiesToUnicode.map["frac14"] = '¼';
			EntitiesToUnicode.map["frac12"] = '½';
			EntitiesToUnicode.map["frac34"] = '¾';
			EntitiesToUnicode.map["iquest"] = '¿';
			EntitiesToUnicode.map["Agrave"] = 'À';
			EntitiesToUnicode.map["Aacute"] = 'Á';
			EntitiesToUnicode.map["Acirc"] = 'Â';
			EntitiesToUnicode.map["Atilde"] = 'Ã';
			EntitiesToUnicode.map["Auml"] = 'Ä';
			EntitiesToUnicode.map["Aring"] = 'Å';
			EntitiesToUnicode.map["AElig"] = 'Æ';
			EntitiesToUnicode.map["Ccedil"] = 'Ç';
			EntitiesToUnicode.map["Egrave"] = 'È';
			EntitiesToUnicode.map["Eacute"] = 'É';
			EntitiesToUnicode.map["Ecirc"] = 'Ê';
			EntitiesToUnicode.map["Euml"] = 'Ë';
			EntitiesToUnicode.map["Igrave"] = 'Ì';
			EntitiesToUnicode.map["Iacute"] = 'Í';
			EntitiesToUnicode.map["Icirc"] = 'Î';
			EntitiesToUnicode.map["Iuml"] = 'Ï';
			EntitiesToUnicode.map["ETH"] = 'Ð';
			EntitiesToUnicode.map["Ntilde"] = 'Ñ';
			EntitiesToUnicode.map["Ograve"] = 'Ò';
			EntitiesToUnicode.map["Oacute"] = 'Ó';
			EntitiesToUnicode.map["Ocirc"] = 'Ô';
			EntitiesToUnicode.map["Otilde"] = 'Õ';
			EntitiesToUnicode.map["Ouml"] = 'Ö';
			EntitiesToUnicode.map["times"] = '×';
			EntitiesToUnicode.map["Oslash"] = 'Ø';
			EntitiesToUnicode.map["Ugrave"] = 'Ù';
			EntitiesToUnicode.map["Uacute"] = 'Ú';
			EntitiesToUnicode.map["Ucirc"] = 'Û';
			EntitiesToUnicode.map["Uuml"] = 'Ü';
			EntitiesToUnicode.map["Yacute"] = 'Ý';
			EntitiesToUnicode.map["THORN"] = 'Þ';
			EntitiesToUnicode.map["szlig"] = 'ß';
			EntitiesToUnicode.map["agrave"] = 'à';
			EntitiesToUnicode.map["aacute"] = 'á';
			EntitiesToUnicode.map["acirc"] = 'â';
			EntitiesToUnicode.map["atilde"] = 'ã';
			EntitiesToUnicode.map["auml"] = 'ä';
			EntitiesToUnicode.map["aring"] = 'å';
			EntitiesToUnicode.map["aelig"] = 'æ';
			EntitiesToUnicode.map["ccedil"] = 'ç';
			EntitiesToUnicode.map["egrave"] = 'è';
			EntitiesToUnicode.map["eacute"] = 'é';
			EntitiesToUnicode.map["ecirc"] = 'ê';
			EntitiesToUnicode.map["euml"] = 'ë';
			EntitiesToUnicode.map["igrave"] = 'ì';
			EntitiesToUnicode.map["iacute"] = 'í';
			EntitiesToUnicode.map["icirc"] = 'î';
			EntitiesToUnicode.map["iuml"] = 'ï';
			EntitiesToUnicode.map["eth"] = 'ð';
			EntitiesToUnicode.map["ntilde"] = 'ñ';
			EntitiesToUnicode.map["ograve"] = 'ò';
			EntitiesToUnicode.map["oacute"] = 'ó';
			EntitiesToUnicode.map["ocirc"] = 'ô';
			EntitiesToUnicode.map["otilde"] = 'õ';
			EntitiesToUnicode.map["ouml"] = 'ö';
			EntitiesToUnicode.map["divide"] = '÷';
			EntitiesToUnicode.map["oslash"] = 'ø';
			EntitiesToUnicode.map["ugrave"] = 'ù';
			EntitiesToUnicode.map["uacute"] = 'ú';
			EntitiesToUnicode.map["ucirc"] = 'û';
			EntitiesToUnicode.map["uuml"] = 'ü';
			EntitiesToUnicode.map["yacute"] = 'ý';
			EntitiesToUnicode.map["thorn"] = 'þ';
			EntitiesToUnicode.map["yuml"] = 'ÿ';
			EntitiesToUnicode.map["fnof"] = 'ƒ';
			EntitiesToUnicode.map["Alpha"] = 'Α';
			EntitiesToUnicode.map["Beta"] = 'Β';
			EntitiesToUnicode.map["Gamma"] = 'Γ';
			EntitiesToUnicode.map["Delta"] = 'Δ';
			EntitiesToUnicode.map["Epsilon"] = 'Ε';
			EntitiesToUnicode.map["Zeta"] = 'Ζ';
			EntitiesToUnicode.map["Eta"] = 'Η';
			EntitiesToUnicode.map["Theta"] = 'Θ';
			EntitiesToUnicode.map["Iota"] = 'Ι';
			EntitiesToUnicode.map["Kappa"] = 'Κ';
			EntitiesToUnicode.map["Lambda"] = 'Λ';
			EntitiesToUnicode.map["Mu"] = 'Μ';
			EntitiesToUnicode.map["Nu"] = 'Ν';
			EntitiesToUnicode.map["Xi"] = 'Ξ';
			EntitiesToUnicode.map["Omicron"] = 'Ο';
			EntitiesToUnicode.map["Pi"] = 'Π';
			EntitiesToUnicode.map["Rho"] = 'Ρ';
			EntitiesToUnicode.map["Sigma"] = 'Σ';
			EntitiesToUnicode.map["Tau"] = 'Τ';
			EntitiesToUnicode.map["Upsilon"] = 'Υ';
			EntitiesToUnicode.map["Phi"] = 'Φ';
			EntitiesToUnicode.map["Chi"] = 'Χ';
			EntitiesToUnicode.map["Psi"] = 'Ψ';
			EntitiesToUnicode.map["Omega"] = 'Ω';
			EntitiesToUnicode.map["alpha"] = 'α';
			EntitiesToUnicode.map["beta"] = 'β';
			EntitiesToUnicode.map["gamma"] = 'γ';
			EntitiesToUnicode.map["delta"] = 'δ';
			EntitiesToUnicode.map["epsilon"] = 'ε';
			EntitiesToUnicode.map["zeta"] = 'ζ';
			EntitiesToUnicode.map["eta"] = 'η';
			EntitiesToUnicode.map["theta"] = 'θ';
			EntitiesToUnicode.map["iota"] = 'ι';
			EntitiesToUnicode.map["kappa"] = 'κ';
			EntitiesToUnicode.map["lambda"] = 'λ';
			EntitiesToUnicode.map["mu"] = 'μ';
			EntitiesToUnicode.map["nu"] = 'ν';
			EntitiesToUnicode.map["xi"] = 'ξ';
			EntitiesToUnicode.map["omicron"] = 'ο';
			EntitiesToUnicode.map["pi"] = 'π';
			EntitiesToUnicode.map["rho"] = 'ρ';
			EntitiesToUnicode.map["sigmaf"] = 'ς';
			EntitiesToUnicode.map["sigma"] = 'σ';
			EntitiesToUnicode.map["tau"] = 'τ';
			EntitiesToUnicode.map["upsilon"] = 'υ';
			EntitiesToUnicode.map["phi"] = 'φ';
			EntitiesToUnicode.map["chi"] = 'χ';
			EntitiesToUnicode.map["psi"] = 'ψ';
			EntitiesToUnicode.map["omega"] = 'ω';
			EntitiesToUnicode.map["thetasym"] = 'ϑ';
			EntitiesToUnicode.map["upsih"] = 'ϒ';
			EntitiesToUnicode.map["piv"] = 'ϖ';
			EntitiesToUnicode.map["bull"] = '•';
			EntitiesToUnicode.map["hellip"] = '…';
			EntitiesToUnicode.map["prime"] = '′';
			EntitiesToUnicode.map["Prime"] = '″';
			EntitiesToUnicode.map["oline"] = '‾';
			EntitiesToUnicode.map["frasl"] = '⁄';
			EntitiesToUnicode.map["weierp"] = '℘';
			EntitiesToUnicode.map["image"] = 'ℑ';
			EntitiesToUnicode.map["real"] = 'ℜ';
			EntitiesToUnicode.map["trade"] = '™';
			EntitiesToUnicode.map["alefsym"] = 'ℵ';
			EntitiesToUnicode.map["larr"] = '←';
			EntitiesToUnicode.map["uarr"] = '↑';
			EntitiesToUnicode.map["rarr"] = '→';
			EntitiesToUnicode.map["darr"] = '↓';
			EntitiesToUnicode.map["harr"] = '↔';
			EntitiesToUnicode.map["crarr"] = '↵';
			EntitiesToUnicode.map["lArr"] = '⇐';
			EntitiesToUnicode.map["uArr"] = '⇑';
			EntitiesToUnicode.map["rArr"] = '⇒';
			EntitiesToUnicode.map["dArr"] = '⇓';
			EntitiesToUnicode.map["hArr"] = '⇔';
			EntitiesToUnicode.map["forall"] = '∀';
			EntitiesToUnicode.map["part"] = '∂';
			EntitiesToUnicode.map["exist"] = '∃';
			EntitiesToUnicode.map["empty"] = '∅';
			EntitiesToUnicode.map["nabla"] = '∇';
			EntitiesToUnicode.map["isin"] = '∈';
			EntitiesToUnicode.map["notin"] = '∉';
			EntitiesToUnicode.map["ni"] = '∋';
			EntitiesToUnicode.map["prod"] = '∏';
			EntitiesToUnicode.map["sum"] = '∑';
			EntitiesToUnicode.map["minus"] = '−';
			EntitiesToUnicode.map["lowast"] = '∗';
			EntitiesToUnicode.map["radic"] = '√';
			EntitiesToUnicode.map["prop"] = '∝';
			EntitiesToUnicode.map["infin"] = '∞';
			EntitiesToUnicode.map["ang"] = '∠';
			EntitiesToUnicode.map["and"] = '∧';
			EntitiesToUnicode.map["or"] = '∨';
			EntitiesToUnicode.map["cap"] = '∩';
			EntitiesToUnicode.map["cup"] = '∪';
			EntitiesToUnicode.map["int"] = '∫';
			EntitiesToUnicode.map["there4"] = '∴';
			EntitiesToUnicode.map["sim"] = '∼';
			EntitiesToUnicode.map["cong"] = '≅';
			EntitiesToUnicode.map["asymp"] = '≈';
			EntitiesToUnicode.map["ne"] = '≠';
			EntitiesToUnicode.map["equiv"] = '≡';
			EntitiesToUnicode.map["le"] = '≤';
			EntitiesToUnicode.map["ge"] = '≥';
			EntitiesToUnicode.map["sub"] = '⊂';
			EntitiesToUnicode.map["sup"] = '⊃';
			EntitiesToUnicode.map["nsub"] = '⊄';
			EntitiesToUnicode.map["sube"] = '⊆';
			EntitiesToUnicode.map["supe"] = '⊇';
			EntitiesToUnicode.map["oplus"] = '⊕';
			EntitiesToUnicode.map["otimes"] = '⊗';
			EntitiesToUnicode.map["perp"] = '⊥';
			EntitiesToUnicode.map["sdot"] = '⋅';
			EntitiesToUnicode.map["lceil"] = '⌈';
			EntitiesToUnicode.map["rceil"] = '⌉';
			EntitiesToUnicode.map["lfloor"] = '⌊';
			EntitiesToUnicode.map["rfloor"] = '⌋';
			EntitiesToUnicode.map["lang"] = '〈';
			EntitiesToUnicode.map["rang"] = '〉';
			EntitiesToUnicode.map["loz"] = '◊';
			EntitiesToUnicode.map["spades"] = '♠';
			EntitiesToUnicode.map["clubs"] = '♣';
			EntitiesToUnicode.map["hearts"] = '♥';
			EntitiesToUnicode.map["diams"] = '♦';
			EntitiesToUnicode.map["quot"] = '"';
			EntitiesToUnicode.map["amp"] = '&';
			EntitiesToUnicode.map["apos"] = '\'';
			EntitiesToUnicode.map["lt"] = '<';
			EntitiesToUnicode.map["gt"] = '>';
			EntitiesToUnicode.map["OElig"] = 'Œ';
			EntitiesToUnicode.map["oelig"] = 'œ';
			EntitiesToUnicode.map["Scaron"] = 'Š';
			EntitiesToUnicode.map["scaron"] = 'š';
			EntitiesToUnicode.map["Yuml"] = 'Ÿ';
			EntitiesToUnicode.map["circ"] = 'ˆ';
			EntitiesToUnicode.map["tilde"] = '˜';
			EntitiesToUnicode.map["ensp"] = '\u2002';
			EntitiesToUnicode.map["emsp"] = '\u2003';
			EntitiesToUnicode.map["thinsp"] = '\u2009';
			EntitiesToUnicode.map["zwnj"] = '‌';
			EntitiesToUnicode.map["zwj"] = '‍';
			EntitiesToUnicode.map["lrm"] = '‎';
			EntitiesToUnicode.map["rlm"] = '‏';
			EntitiesToUnicode.map["ndash"] = '–';
			EntitiesToUnicode.map["mdash"] = '—';
			EntitiesToUnicode.map["lsquo"] = '‘';
			EntitiesToUnicode.map["rsquo"] = '’';
			EntitiesToUnicode.map["sbquo"] = '‚';
			EntitiesToUnicode.map["ldquo"] = '“';
			EntitiesToUnicode.map["rdquo"] = '”';
			EntitiesToUnicode.map["bdquo"] = '„';
			EntitiesToUnicode.map["dagger"] = '†';
			EntitiesToUnicode.map["Dagger"] = '‡';
			EntitiesToUnicode.map["permil"] = '‰';
			EntitiesToUnicode.map["lsaquo"] = '‹';
			EntitiesToUnicode.map["rsaquo"] = '›';
			EntitiesToUnicode.map["euro"] = '€';
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00101D60 File Offset: 0x00100D60
		public static char DecodeEntity(string name)
		{
			if (name.StartsWith("#x"))
			{
				try
				{
					return (char)int.Parse(name.Substring(2), NumberStyles.AllowHexSpecifier);
				}
				catch
				{
					return '\0';
				}
			}
			if (name.StartsWith("#"))
			{
				try
				{
					return (char)int.Parse(name.Substring(1));
				}
				catch
				{
					return '\0';
				}
			}
			if (EntitiesToUnicode.map.ContainsKey(name))
			{
				return EntitiesToUnicode.map[name];
			}
			return '\0';
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x00101DF0 File Offset: 0x00100DF0
		public static string DecodeString(string s)
		{
			int num = s.IndexOf('&');
			if (num == -1)
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder(s.Substring(0, num));
			int num2;
			for (;;)
			{
				num2 = s.IndexOf(';', num);
				if (num2 == -1)
				{
					break;
				}
				int num3 = s.IndexOf('&', num + 1);
				while (num3 != -1 && num3 < num2)
				{
					stringBuilder.Append(s.Substring(num, num3 - num));
					num = num3;
					num3 = s.IndexOf('&', num + 1);
				}
				char c = EntitiesToUnicode.DecodeEntity(s.Substring(num + 1, num2 - (num + 1)));
				if (s.Length < num2 + 1)
				{
					goto Block_4;
				}
				if (c == '\0')
				{
					stringBuilder.Append(s.Substring(num, num2 + 1 - num));
				}
				else
				{
					stringBuilder.Append(c);
				}
				num = s.IndexOf('&', num2);
				if (num == -1)
				{
					goto Block_6;
				}
				stringBuilder.Append(s.Substring(num2 + 1, num - (num2 + 1)));
			}
			stringBuilder.Append(s.Substring(num));
			return stringBuilder.ToString();
			Block_4:
			return stringBuilder.ToString();
			Block_6:
			stringBuilder.Append(s.Substring(num2 + 1));
			return stringBuilder.ToString();
		}

		// Token: 0x04001D78 RID: 7544
		public static readonly Dictionary<string, char> map = new Dictionary<string, char>();
	}
}
