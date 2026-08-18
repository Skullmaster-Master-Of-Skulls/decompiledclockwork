using System;
using Spire.Doc.Core;

// Token: 0x020002A8 RID: 680
[CLSCompliant(false)]
internal class spr\u235C : sprᥕ
{
	// Token: 0x06002494 RID: 9364 RVA: 0x0024F47C File Offset: 0x0024E47C
	internal spr\u235C(sprច A_0) : base(A_0)
	{
		this.\u1712 = WordSubdocument.TextBox;
	}

	// Token: 0x06002495 RID: 9365 RVA: 0x0024F498 File Offset: 0x0024E498
	public virtual void ᜀ()
	{
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_48:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_52;
				case 1:
					if (num2 >= 2)
					{
						num = 2;
						continue;
					}
					base.ᜁ('\r');
					num2++;
					num = 3;
					continue;
				case 2:
					goto IL_66;
				case 3:
					goto IL_52;
				}
				goto IL_3E;
				IL_52:
				num = 1;
			}
			IL_66:
			this.ᜀ(true, 0);
			return;
		}
		default:
			if (false)
			{
			}
			break;
		}
		IL_3E:
		if (true)
		{
		}
		num2 = 0;
		goto IL_48;
	}

	// Token: 0x06002496 RID: 9366 RVA: 0x0024F52C File Offset: 0x0024E52C
	public override void ᜃ()
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
		base.ᜃ();
		this.ᜀ(false, 0);
	}

	// Token: 0x06002497 RID: 9367 RVA: 0x0024F578 File Offset: 0x0024E578
	internal new void ᜂ(int A_0)
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
		base.ᜀ(WordChunkType.ParagraphEnd);
		this.ᜀ(false, A_0);
	}

	// Token: 0x06002498 RID: 9368 RVA: 0x0024F5C4 File Offset: 0x0024E5C4
	protected override void ᜀ(int A_0)
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
		sprᾱ sprᾱ = this.ᜂ.ᜀ();
		sprᾱ.\u1712(sprᾱ.\u1752() + A_0);
	}

	// Token: 0x06002499 RID: 9369 RVA: 0x0024F618 File Offset: 0x0024E618
	protected virtual void ᜀ(bool A_0, int A_1)
	{
		spr\u181A spr_u181A;
		spr\u208C spr_u208C;
		for (;;)
		{
			for (;;)
			{
				spr_u181A = new spr\u181A();
				spr_u208C = new spr\u208C();
				spr_u181A.ᜀ(1);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						spr_u208C.ᜁ((short)this.ᜂ);
						spr_u208C.ᜀ(16);
						spr_u181A.ᜁ(A_1);
						spr_u181A.ᜀ(uint.MaxValue);
						if (true)
						{
						}
						num = 2;
						continue;
					case 1:
						goto IL_67;
					case 2:
						goto IL_9F;
					case 3:
						if (!A_0)
						{
							num = 0;
							continue;
						}
						spr_u208C.ᜁ(-1);
						spr_u208C.ᜀ(0);
						spr_u181A.ᜁ(0);
						num = 1;
						continue;
					}
					break;
				}
			}
			IL_A1:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_B7;
			}
			IL_67:
			IL_9F:
			goto IL_A1;
		}
		IL_B7:
		if (false)
		{
		}
		this.ᜂ.ᜃ().ᜐ().ᜀ(WordSubdocument.Main, spr_u181A, spr_u208C, this.ᜁ);
		this.ᜁ = this.ᜂ.ᜀ().\u1752();
		this.ᜂ++;
	}

	// Token: 0x040021B5 RID: 8629
	protected new const uint ᜀ = 4294967295U;

	// Token: 0x040021B6 RID: 8630
	protected new int ᜁ;

	// Token: 0x040021B7 RID: 8631
	protected new int ᜂ;

	// Token: 0x040021B8 RID: 8632
	protected new long ᜃ;
}
