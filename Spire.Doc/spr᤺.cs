using System;
using System.IO;
using System.Runtime.InteropServices;
using Spire.CompoundFile.Doc;

// Token: 0x020003FC RID: 1020
[StructLayout(LayoutKind.Sequential)]
internal class spr\u193A : spr\u2562
{
	// Token: 0x060038F1 RID: 14577 RVA: 0x00353354 File Offset: 0x00352354
	internal spr\u193A()
	{
	}

	// Token: 0x060038F2 RID: 14578 RVA: 0x00353378 File Offset: 0x00352378
	internal spr\u193A(Stream A_0)
	{
		A_0.Read(this.ᜁ, 0, 511);
		this.ᜂ = (byte)A_0.ReadByte();
	}

	// Token: 0x060038F3 RID: 14579 RVA: 0x003533BC File Offset: 0x003523BC
	internal byte[] ᜂ()
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

	// Token: 0x060038F4 RID: 14580 RVA: 0x00353400 File Offset: 0x00352400
	internal byte ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x060038F5 RID: 14581 RVA: 0x00353444 File Offset: 0x00352444
	internal void ᜀ(byte A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_4B:
			num = 1;
			break;
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		for (;;)
		{
			IL_30:
			switch (num)
			{
			case 1:
				this.ᜂ = A_0;
				if (true)
				{
				}
				num = 2;
				continue;
			case 2:
				return;
			}
			break;
		}
		if (this.ᜂ != A_0)
		{
			goto IL_4B;
		}
		return;
		IL_20:
		if (false)
		{
		}
		num = 0;
		goto IL_30;
	}

	// Token: 0x060038F6 RID: 14582 RVA: 0x003534C0 File Offset: 0x003524C0
	internal override int ᜀ()
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
		return 512;
	}

	// Token: 0x060038F7 RID: 14583 RVA: 0x00353500 File Offset: 0x00352500
	internal override void ᜁ(byte[] A_0, int A_1)
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
		this.ᜁ = spr\u2562.ᜀ(A_0, this.ᜁ.Length, ref A_1);
		this.ᜂ = A_0[A_1];
		A_1++;
	}

	// Token: 0x060038F8 RID: 14584 RVA: 0x00353560 File Offset: 0x00352560
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 10;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 >= 0)
				{
					num = 5;
					continue;
				}
				goto IL_B4;
			case 1:
				if (A_1 + 512 > A_0.Length)
				{
					num = 4;
					continue;
				}
				goto IL_C8;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_94;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 3:
				goto IL_60;
			case 4:
				goto IL_7E;
			case 5:
				goto IL_94;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 0;
			continue;
			IL_94:
			num = 1;
		}
		IL_60:
		throw new ArgumentNullException(ClipboardData.b("ᅯqٳ㉵᥷๹ᵻ", a_));
		IL_7E:
		IL_B4:
		throw new ArgumentOutOfRangeException(ClipboardData.b("᥯㵱ታၵ୷όࡻ", a_));
		IL_C8:
		this.ᜁ.CopyTo(A_0, A_1);
		A_1 += this.ᜁ.Length;
		A_0[A_1] = this.ᜂ;
		return ++A_1;
	}

	// Token: 0x060038F9 RID: 14585 RVA: 0x00353660 File Offset: 0x00352660
	internal int ᜀ(Stream A_0)
	{
		int a_ = 9;
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				A_0.Write(this.ᜁ, 0, this.ᜁ.Length);
				A_0.WriteByte(this.ᜂ);
				return (int)A_0.Position;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ᱮհŲၴᙶᑸ", a_));
	}

	// Token: 0x04002A99 RID: 10905
	internal new const int ᜀ = 512;

	// Token: 0x04002A9A RID: 10906
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 511)]
	private new byte[] ᜁ = new byte[511];

	// Token: 0x04002A9B RID: 10907
	private new byte ᜂ;
}
