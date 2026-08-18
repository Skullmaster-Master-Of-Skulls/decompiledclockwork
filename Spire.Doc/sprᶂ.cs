using System;
using Spire.Doc.Fields.Shape;

// Token: 0x020002CE RID: 718
internal class sprᶂ
{
	// Token: 0x060026D3 RID: 9939 RVA: 0x002644CC File Offset: 0x002634CC
	internal sprᶂ() : this(HandlePositionType.Unknown, 0)
	{
	}

	// Token: 0x060026D4 RID: 9940 RVA: 0x002644E4 File Offset: 0x002634E4
	internal sprᶂ(HandlePositionType A_0, int A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x060026D5 RID: 9941 RVA: 0x00264508 File Offset: 0x00263508
	internal sprᶂ(int A_0)
	{
		this.ᜁ = 0;
		switch (A_0)
		{
		case 0:
			this.ᜀ = HandlePositionType.LeftTop;
			return;
		case 1:
			this.ᜀ = HandlePositionType.RightBottom;
			return;
		case 2:
			this.ᜀ = HandlePositionType.Center;
			return;
		default:
			if (A_0 >= 3 && A_0 <= 132)
			{
				this.ᜀ = HandlePositionType.Formula;
				this.ᜁ = A_0 - 3;
				return;
			}
			this.ᜀ = HandlePositionType.Adjust;
			this.ᜁ = A_0 - 256;
			return;
		}
	}

	// Token: 0x060026D6 RID: 9942 RVA: 0x00264588 File Offset: 0x00263588
	internal int ᜁ()
	{
		for (;;)
		{
			HandlePositionType handlePositionType = this.ᜀ;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					switch (handlePositionType)
					{
					case HandlePositionType.LeftTop:
						return 0;
					case HandlePositionType.RightBottom:
						return 1;
					case HandlePositionType.Center:
						return 2;
					case HandlePositionType.Formula:
						goto IL_77;
					case HandlePositionType.Adjust:
						goto IL_8F;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_82;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					break;
				case 1:
					goto IL_8D;
				case 2:
					goto IL_82;
				}
				break;
				IL_82:
				num = 1;
			}
		}
		IL_77:
		return 3 + this.ᜁ;
		IL_8D:
		return this.ᜁ;
		IL_8F:
		return 256 + this.ᜁ;
	}

	// Token: 0x060026D7 RID: 9943 RVA: 0x0026463C File Offset: 0x0026363C
	internal HandlePositionType ᜂ()
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
		return this.ᜀ;
	}

	// Token: 0x060026D8 RID: 9944 RVA: 0x00264680 File Offset: 0x00263680
	internal int ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x0400228D RID: 8845
	private readonly HandlePositionType ᜀ;

	// Token: 0x0400228E RID: 8846
	private readonly int ᜁ;
}
