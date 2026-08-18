using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x020001AC RID: 428
[CLSCompliant(false)]
internal class spr\u2618 : sprᤒ
{
	// Token: 0x060010D3 RID: 4307 RVA: 0x000FCF70 File Offset: 0x000FBF70
	internal override int ᜇ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_6D;
				}
				break;
			case 2:
				num = 0;
				continue;
			case 3:
				goto IL_42;
			}
			if (true)
			{
			}
			if (!this.ᜀ())
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
		}
		IL_42:
		int num2 = 2;
		goto IL_76;
		IL_6D:
		if (false)
		{
		}
		num2 = 1;
		IL_76:
		return num2 + 2 + this.ᜁ.ᜇ();
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x000FD004 File Offset: 0x000FC004
	protected new bool ᜀ()
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
		return this.ᜁ.ᜇ() % 2 == 0;
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x000FD050 File Offset: 0x000FC050
	internal spr\u2618()
	{
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x000FD064 File Offset: 0x000FC064
	internal spr\u2618(sprᤒ A_0)
	{
		this.ᜀ = A_0.ᜄ();
		this.ᜁ = A_0.ᜁ();
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x000FD090 File Offset: 0x000FC090
	internal spr\u2618(byte[] A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x000FD0A8 File Offset: 0x000FC0A8
	internal new int ᜁ(byte[] A_0, int A_1)
	{
		int a_ = 18;
		int num = 0;
		int num3;
		for (;;)
		{
			int num2;
			byte b;
			switch (num)
			{
			case 1:
				if (A_1 >= num2)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				b = A_0[A_1];
				A_1++;
				num = 7;
				continue;
			case 2:
				if (A_1 + num3 > num2)
				{
					num = 9;
					continue;
				}
				goto IL_192;
			case 3:
				goto IL_166;
			case 4:
				if (A_1 >= 0)
				{
					num = 5;
					continue;
				}
				goto IL_130;
			case 5:
				num = 1;
				continue;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_50;
				default:
					goto IL_EE;
				}
				break;
			case 7:
				if (b == 0)
				{
					num = 12;
					continue;
				}
				goto IL_A9;
			case 8:
				goto IL_81;
			case 9:
				goto IL_CC;
			case 10:
				if (A_1 >= num2)
				{
					num = 8;
					continue;
				}
				b = A_0[A_1++];
				num = 11;
				continue;
			case 11:
				goto IL_A9;
			case 12:
				num = 10;
				continue;
			}
			goto IL_4D;
			IL_50:
			num = 6;
			continue;
			IL_4D:
			if (A_0 == null)
			{
				goto IL_50;
			}
			num2 = A_0.Length;
			num = 4;
			continue;
			IL_A9:
			num3 = (int)(b * 2);
			num = 2;
		}
		IL_81:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᅷ㕹᩻᡽", a_));
		IL_CC:
		throw new ArgumentOutOfRangeException(ClipboardData.b("㱷᭹ࡻώꁿ겋늑秊몙쾟킡킣", a_));
		IL_EE:
		if (false)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("᥷ࡹ๻㩽", a_));
		IL_130:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᅷ㕹᩻᡽", a_));
		IL_166:
		goto IL_130;
		IL_192:
		this.ᜀ = BitConverter.ToUInt16(A_0, A_1);
		A_1 += 2;
		num3 -= 2;
		this.ᜁ.ᜀ(A_0, A_1, num3);
		return A_1;
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x000FD26C File Offset: 0x000FC26C
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int num;
		int num3;
		for (;;)
		{
			num = A_1;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_46;
				case 1:
					if (this.ᜀ())
					{
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					goto IL_98;
				case 2:
					num3 = this.ᜇ() - (this.ᜀ() ? 2 : 1);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_93;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 3:
					goto IL_93;
				}
				break;
				IL_93:
				A_0[A_1++] = 0;
				num2 = 0;
			}
		}
		IL_46:
		IL_98:
		byte b = (byte)(num3 / 2);
		A_0[A_1++] = b;
		BitConverter.GetBytes(this.ᜀ).CopyTo(A_0, A_1);
		A_1 += 2;
		A_1 += this.ᜁ.ᜀ(A_0, A_1);
		return A_1 - num;
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x000FD34C File Offset: 0x000FC34C
	internal new void ᜀ(BinaryWriter A_0, Stream A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_44;
				default:
					if (false)
					{
					}
					goto IL_AE;
				}
				break;
			case 1:
				A_0.Write(0);
				num = 0;
				continue;
			case 3:
				return;
			case 4:
				if (this.ᜁ != null)
				{
					num = 6;
					continue;
				}
				return;
			case 5:
				if (this.ᜀ())
				{
					num = 1;
					continue;
				}
				goto IL_AE;
			case 6:
				if (true)
				{
				}
				this.ᜁ.ᜀ(A_0, A_1);
				num = 3;
				continue;
			}
			goto IL_2C;
			IL_44:
			num = 5;
			continue;
			IL_2C:
			int num2 = this.ᜇ() - (this.ᜀ() ? 2 : 1);
			goto IL_44;
			IL_AE:
			byte value = (byte)(num2 / 2);
			A_0.Write(value);
			A_0.Write(this.ᜀ);
			num = 4;
		}
	}
}
