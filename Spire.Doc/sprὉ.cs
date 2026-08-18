using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x02000284 RID: 644
[CLSCompliant(false)]
internal class sprὉ : spr\u23F8
{
	// Token: 0x06002234 RID: 8756 RVA: 0x00235C40 File Offset: 0x00234C40
	internal sprὉ()
	{
	}

	// Token: 0x06002235 RID: 8757 RVA: 0x00235C54 File Offset: 0x00234C54
	internal sprὉ(byte[] A_0) : base(A_0)
	{
	}

	// Token: 0x06002236 RID: 8758 RVA: 0x00235C68 File Offset: 0x00234C68
	internal sprὉ(byte[] A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06002237 RID: 8759 RVA: 0x00235C80 File Offset: 0x00234C80
	internal sprὉ(byte[] A_0, int A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06002238 RID: 8760 RVA: 0x00235C98 File Offset: 0x00234C98
	internal sprὉ(Stream A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06002239 RID: 8761 RVA: 0x00235CB0 File Offset: 0x00234CB0
	internal override void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 19;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_70;
			case 1:
				goto IL_126;
			case 2:
				if (A_1 + A_2 > A_0.Length)
				{
					num = 1;
					continue;
				}
				num = 6;
				continue;
			case 3:
				if (A_1 >= 0)
				{
					num = 7;
					continue;
				}
				goto IL_13A;
			case 4:
				goto IL_E8;
			case 5:
				num = 2;
				continue;
			case 6:
				if (A_2 == 0)
				{
					num = 4;
					continue;
				}
				goto IL_17F;
			case 7:
				num = 8;
				continue;
			case 8:
				if (A_1 > A_0.Length - 1)
				{
					num = 9;
					continue;
				}
				num = 11;
				continue;
			case 9:
				goto IL_17D;
			case 11:
				if (A_2 >= 0)
				{
					num = 5;
					continue;
				}
				goto IL_72;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_70;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 3;
				}
				break;
			}
		}
		IL_70:
		throw new ArgumentNullException(ClipboardData.b("ᡸॺོ㭾", a_));
		IL_72:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ၸ㑺᭼᥾", a_));
		IL_E8:
		this.ᜁ = null;
		this.ᜀ = null;
		return;
		IL_126:
		goto IL_72;
		IL_13A:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ၸ㑺᭼᥾", a_), ClipboardData.b("⽸᩺ᅼ੾ꎂꮊ뎒릘튠莢톤쾦좨얪趬龮醰튲\udbb4펶馸\udcba쾼\udabeꃀ럂ꃄ뗆뿊ꗌ껎뿐듔ꗖꯘ鿚볜ꯞ胠췢ꧤ苦蟨質駬蟮퇰\udef2헴웶", a_));
		IL_17D:
		goto IL_13A;
		IL_17F:
		int num2 = (A_2 - 4) / 6;
		this.ᜀ = new int[num2 + 1];
		this.ᜁ = new ushort[num2];
		int num3 = (num2 + 1) * 4;
		Buffer.BlockCopy(A_0, A_1, this.ᜀ, 0, num3);
		A_1 += num3;
		Buffer.BlockCopy(A_0, A_1, this.ᜁ, 0, num2 * 2);
	}

	// Token: 0x0600223A RID: 8762 RVA: 0x00235E88 File Offset: 0x00234E88
	internal int[] ᜀ()
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

	// Token: 0x0600223B RID: 8763 RVA: 0x00235ECC File Offset: 0x00234ECC
	internal ushort[] ᜁ()
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

	// Token: 0x0600223C RID: 8764 RVA: 0x00235F10 File Offset: 0x00234F10
	internal override int ᜇ()
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
		return this.ᜁ.Length * 2 + this.ᜀ.Length * 4;
	}

	// Token: 0x040020DE RID: 8414
	private new int[] ᜀ;

	// Token: 0x040020DF RID: 8415
	private new ushort[] ᜁ;
}
