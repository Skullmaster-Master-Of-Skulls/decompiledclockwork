using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.CompoundFile.Doc;

// Token: 0x020003FD RID: 1021
[DefaultMember("Item")]
internal class sprᡜ : List<sprហ>
{
	// Token: 0x060038FB RID: 14587 RVA: 0x003536FC File Offset: 0x003526FC
	internal sprហ ᜀ(int A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.ᜁ(num).ᜄ() == A_0)
					{
						goto IL_6B;
					}
					if (true)
					{
					}
					num++;
					num2 = 1;
					continue;
				case 1:
					goto IL_75;
				case 2:
					if (num >= base.Count)
					{
						num2 = 3;
						continue;
					}
					num2 = 0;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6B;
					default:
						goto IL_A7;
					}
					break;
				case 4:
					goto IL_73;
				case 5:
					goto IL_75;
				}
				break;
				IL_6B:
				num2 = 4;
				continue;
				IL_75:
				num2 = 2;
			}
		}
		IL_73:
		return this.ᜁ(num);
		IL_A7:
		if (false)
		{
		}
		return null;
	}

	// Token: 0x060038FC RID: 14588 RVA: 0x003537BC File Offset: 0x003527BC
	internal sprហ ᜂ(int A_0)
	{
		int a_ = 16;
		sprហ sprហ = this.ᜀ(A_0);
		if (sprហ == null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				throw new ArgumentException(ClipboardData.b("㩵ᅷॹࡻ幽ꢇﶉ揄늑ﺕﶗ몙얟송춣삥솧쾩좫躭\ud9af횱钳햵ힷ쾹킻\udabd곁ꯃ닅꣉꧋뛏뷑ꇓ룕볗", a_));
			}
		}
		return sprហ;
	}

	// Token: 0x060038FD RID: 14589 RVA: 0x00353824 File Offset: 0x00352824
	internal sprហ ᜁ(int A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return base[A_0];
	}

	// Token: 0x060038FE RID: 14590 RVA: 0x00353868 File Offset: 0x00352868
	internal void ᜀ(int A_0, sprហ A_1)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		base[A_0] = A_1;
	}
}
