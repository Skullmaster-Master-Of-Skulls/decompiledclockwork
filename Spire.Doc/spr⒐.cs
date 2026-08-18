using System;
using System.Runtime.InteropServices;
using Spire.CompoundFile.Doc;

// Token: 0x020003C2 RID: 962
[StructLayout(LayoutKind.Sequential)]
internal class spr\u2490
{
	// Token: 0x0600363B RID: 13883 RVA: 0x0032E408 File Offset: 0x0032D408
	internal int ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x0600363C RID: 13884 RVA: 0x0032E44C File Offset: 0x0032D44C
	internal void ᜀ(int A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x0600363D RID: 13885 RVA: 0x0032E490 File Offset: 0x0032D490
	internal int ᜁ(byte[] A_0, int A_1)
	{
		int a_ = 4;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 1;
				continue;
			case 1:
				if (A_1 > A_0.Length)
				{
					num = 2;
					continue;
				}
				goto IL_D3;
			case 2:
				goto IL_57;
			case 4:
				goto IL_3C;
			case 5:
				if (A_1 < 0)
				{
					goto IL_A7;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D3;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 4;
			}
			else
			{
				num = 5;
			}
		}
		IL_3C:
		throw new ArgumentNullException(ClipboardData.b("୩ṫᱭ㑯፱s᝵", a_));
		IL_57:
		IL_A7:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ͩ⍫࡭ᙯűᅳɵ", a_), ClipboardData.b("㱩൫ɭկ᝱味ᕵ᥷ᑹ屻ၽꒃꪉ뒓ꚕ뢗ﮙ瞧肟얡횣쎥즧\udea9즫\udcad邯펱욳쒵ﲷ\udbb9좻\udfbd軁ꇃꣅ꿇뻉꓋", a_));
		IL_D3:
		this.ᜁ = BitConverter.ToInt32(A_0, A_1);
		return A_1 + 4;
	}

	// Token: 0x0600363E RID: 13886 RVA: 0x0032E580 File Offset: 0x0032D580
	internal void ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 5;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 + 4 <= A_0.Length)
				{
					goto IL_10A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_97;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 1:
				if (true)
				{
				}
				if (A_1 >= 0)
				{
					num = 5;
					continue;
				}
				goto IL_F6;
			case 2:
				goto IL_97;
			case 3:
				goto IL_47;
			case 5:
				num = 2;
				continue;
			case 6:
				goto IL_8A;
			case 7:
				goto IL_A8;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 1;
			continue;
			IL_97:
			if (A_1 > A_0.Length)
			{
				num = 7;
			}
			else
			{
				num = 0;
			}
		}
		IL_47:
		throw new ArgumentNullException(ClipboardData.b("੪Ὤᵮ㕰ቲŴᙶ", a_));
		IL_8A:
		throw new ArgumentOutOfRangeException(ClipboardData.b("੪Ὤᵮ㕰ቲŴᙶ坸㝺᡼ᅾ", a_));
		IL_A8:
		IL_F6:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ɪ≬८ᝰrၴͶ", a_));
		IL_10A:
		byte[] bytes = BitConverter.GetBytes(this.ᜁ);
		bytes.CopyTo(A_0, A_1);
	}

	// Token: 0x04002990 RID: 10640
	public const int ᜀ = 4;

	// Token: 0x04002991 RID: 10641
	private int ᜁ;
}
