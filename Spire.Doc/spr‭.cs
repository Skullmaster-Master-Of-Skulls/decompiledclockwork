using System;
using Spire.Doc.Core;

// Token: 0x020002A9 RID: 681
[CLSCompliant(false)]
internal class spr\u202D : spr\u235C
{
	// Token: 0x0600249A RID: 9370 RVA: 0x0024F724 File Offset: 0x0024E724
	internal spr\u202D(sprច A_0) : base(A_0)
	{
		this.\u1712 = WordSubdocument.HeaderTextBox;
	}

	// Token: 0x0600249B RID: 9371 RVA: 0x0024F740 File Offset: 0x0024E740
	internal new void ᜁ(int A_0)
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
		base.ᜀ(WordChunkType.ParagraphEnd);
		this.ᜀ(false, A_0);
	}

	// Token: 0x0600249C RID: 9372 RVA: 0x0024F78C File Offset: 0x0024E78C
	protected override void ᜀ(int A_0)
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
		sprᾱ sprᾱ = this.ᜂ.ᜀ();
		sprᾱ.\u1717(sprᾱ.\u17D1() + A_0);
	}

	// Token: 0x0600249D RID: 9373 RVA: 0x0024F7E0 File Offset: 0x0024E7E0
	protected override void ᜀ(bool A_0, int A_1)
	{
		spr\u181A spr_u181A;
		spr\u208C spr_u208C;
		for (;;)
		{
			spr_u181A = new spr\u181A();
			spr_u208C = new spr\u208C();
			spr_u181A.ᜀ(1);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7C;
				case 1:
					if (true)
					{
					}
					spr_u208C.ᜁ((short)this.ᜂ);
					spr_u208C.ᜀ(16);
					spr_u181A.ᜁ(A_1);
					spr_u181A.ᜀ(uint.MaxValue);
					num = 3;
					continue;
				case 2:
					if (A_0)
					{
						spr_u208C.ᜁ(-1);
						spr_u208C.ᜀ(0);
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 3:
					goto IL_B4;
				}
				break;
			}
		}
		IL_7C:
		IL_B4:
		this.ᜂ.ᜃ().ᜐ().ᜀ(WordSubdocument.HeaderFooter, spr_u181A, spr_u208C, this.ᜁ);
		this.ᜁ = this.ᜂ.ᜀ().\u17D1();
		this.ᜂ++;
	}
}
