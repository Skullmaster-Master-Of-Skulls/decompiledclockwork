using System;
using System.Collections.Generic;
using System.Xml;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200023A RID: 570
internal class spr\u214F : spr\u2175
{
	// Token: 0x06002294 RID: 8852 RVA: 0x001357AC File Offset: 0x001347AC
	static spr\u214F()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u214F.ᜀ = new Dictionary<Type, spr\u2175>();
		spr\u214F.ᜀ.Add(typeof(sprថ), new spr\u1979());
		spr\u214F.ᜀ.Add(typeof(XlsComboBoxShape), new spr\u1715());
	}

	// Token: 0x06002295 RID: 8853 RVA: 0x00135824 File Offset: 0x00134824
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int num = 1;
		spr\u2175 spr_u;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_77;
			case 2:
				goto IL_68;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_68:
				spr_u = new sprᥑ(null);
				num = 0;
				continue;
			}
			if (false)
			{
			}
			if (spr\u214F.ᜀ.TryGetValue(A_1.GetType(), out spr_u))
			{
				break;
			}
			num = 2;
		}
		IL_77:
		spr_u.ᜀ(A_0, A_1, A_2, A_3);
	}

	// Token: 0x06002296 RID: 8854 RVA: 0x001358B8 File Offset: 0x001348B8
	public override void ᜀ(XmlWriter A_0, Type A_1)
	{
		int num = 1;
		spr\u2175 spr_u;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5B;
			case 2:
				goto IL_72;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5B:
				spr_u = new sprᥑ(null);
				if (true)
				{
				}
				num = 2;
				break;
			default:
				if (false)
				{
				}
				if (spr\u214F.ᜀ.TryGetValue(A_1, out spr_u))
				{
					goto IL_74;
				}
				num = 0;
				break;
			}
		}
		IL_72:
		IL_74:
		spr_u.ᜀ(A_0, A_1);
	}

	// Token: 0x04001205 RID: 4613
	private new static Dictionary<Type, spr\u2175> ᜀ;
}
