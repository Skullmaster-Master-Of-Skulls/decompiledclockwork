using System;
using System.IO;

// Token: 0x02000279 RID: 633
internal class spr\u1B63 : spr\u2281
{
	// Token: 0x060021D5 RID: 8661 RVA: 0x00232BEC File Offset: 0x00231BEC
	internal spr\u1B63(sprℛ A_0)
	{
		this.ᜁ = A_0;
	}

	// Token: 0x060021D6 RID: 8662 RVA: 0x00232C08 File Offset: 0x00231C08
	public Stream ᜀ(sprᣔ A_0)
	{
		string text;
		spr\u1B02 spr_u1B;
		for (;;)
		{
			text = A_0.ᜁ().Substring(1);
			spr_u1B = this.ᜁ.ᜃ();
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_43;
				case 1:
					goto IL_4B;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4B;
					default:
						goto IL_74;
					}
					break;
				case 3:
					if (true)
					{
					}
					goto IL_43;
				}
				break;
				IL_43:
				num = 1;
				continue;
				IL_4B:
				if (text.IndexOf('/') <= 0)
				{
					num = 2;
				}
				else
				{
					int num2 = text.IndexOf('/');
					spr_u1B = spr_u1B.ᜀ(text.Substring(0, num2));
					text = text.Substring(num2 + 1);
					num = 0;
				}
			}
		}
		IL_74:
		if (false)
		{
		}
		return spr_u1B.ᜂ(text);
	}

	// Token: 0x040020BC RID: 8380
	private const char ᜀ = '/';

	// Token: 0x040020BD RID: 8381
	private readonly sprℛ ᜁ;
}
