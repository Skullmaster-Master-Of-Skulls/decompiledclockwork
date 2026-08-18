using System;
using Spire.CompoundFile.Doc;

// Token: 0x0200017B RID: 379
internal class sprᠴ
{
	// Token: 0x06000D44 RID: 3396 RVA: 0x000DC0F4 File Offset: 0x000DB0F4
	internal void \u1717(int A_0)
	{
		int a_ = 5;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜁ = new int[A_0];
				num = 2;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_57;
				}
				break;
			case 2:
				return;
			case 4:
				if (A_0 != this.ᜁ.Length)
				{
					num = 0;
					continue;
				}
				return;
			}
			if (A_0 < 0)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				num = 4;
			}
		}
		IL_57:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ɪ⍬੮ٰ⁲ᱴ൶ᱸ孺ṼṾꒂꞆ권랖ﲜ膠\ud9a2삤햦욨薪", a_));
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x000DC1B4 File Offset: 0x000DB1B4
	internal virtual void ᜀ(byte[] A_0)
	{
		int a_ = 0;
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
			if (A_0 != null)
			{
				this.\u1717(A_0.Length / 4);
				Buffer.BlockCopy(A_0, 0, this.ᜁ, 0, A_0.Length);
				return;
			}
			break;
		}
		throw new ArgumentNullException(ClipboardData.b("ݥᩧᡩ⹫᭭ᙯᑱᅳѵ", a_));
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x000DC230 File Offset: 0x000DB230
	internal virtual int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 19;
		int num = 1;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 >= 0)
				{
					num = 4;
					continue;
				}
				goto IL_51;
			case 2:
				goto IL_FE;
			case 3:
				if (num2 == 0)
				{
					num = 2;
					continue;
				}
				goto IL_12C;
			case 4:
				num = 5;
				continue;
			case 5:
				if (A_1 <= A_0.Length)
				{
					num = 7;
					continue;
				}
				goto IL_51;
			case 6:
				goto IL_A6;
			case 7:
				num = 8;
				continue;
			case 8:
				if (A_1 + num2 <= A_0.Length)
				{
					num = 3;
					continue;
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
					num = 6;
					continue;
				}
				break;
			case 9:
				goto IL_4C;
			}
			if (A_0 == null)
			{
				num = 9;
			}
			else
			{
				num2 = this.ᜁ.Length * 4;
				num = 0;
			}
		}
		IL_4C:
		throw new ArgumentNullException(ClipboardData.b("ᡸॺོ㭾", a_));
		IL_51:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ၸ㑺᭼᥾", a_));
		IL_A6:
		goto IL_51;
		IL_FE:
		if (true)
		{
		}
		return 0;
		IL_12C:
		Buffer.BlockCopy(this.ᜁ, 0, A_0, A_1, num2);
		return num2;
	}

	// Token: 0x04001482 RID: 5250
	private const int ᜀ = 4;

	// Token: 0x04001483 RID: 5251
	protected int[] ᜁ = new int[0];
}
