using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;

// Token: 0x0200033D RID: 829
internal class spr᱈
{
	// Token: 0x06002C58 RID: 11352 RVA: 0x002AD4CC File Offset: 0x002AC4CC
	public static Color ᜀ(string A_0)
	{
		Match match;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			A_0 = A_0.Trim();
			match = spr᱈.ᜀ.Match(A_0);
			if (!match.Success)
			{
				return ColorTranslator.FromHtml(A_0);
			}
			break;
		}
		return Color.FromArgb(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
	}

	// Token: 0x06002C5A RID: 11354 RVA: 0x002AD588 File Offset: 0x002AC588
	// Note: this type is marked as 'beforefieldinit'.
	static spr᱈()
	{
		int a_ = 6;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr᱈.ᜀ = new Regex(ClipboardData.b("㉫㕭ɯⁱ⥳⵵ί㵹ⅻ╽삁\ud983\uda85ﮇꂉ킋ꚍ뢏캑붕놗욙뒝貟ﺡ힣貥肧좫薭馯잳鲵钷쾻钽黁ꃃ雉뿋賏﯑", a_));
	}

	// Token: 0x04002625 RID: 9765
	private static Regex ᜀ;
}
