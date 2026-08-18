using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200020B RID: 523
[CLSCompliant(false)]
internal class spr\u1804 : spr\u23F8
{
	// Token: 0x06001892 RID: 6290 RVA: 0x001775A8 File Offset: 0x001765A8
	internal uint[] ᜄ()
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

	// Token: 0x06001893 RID: 6291 RVA: 0x001775EC File Offset: 0x001765EC
	internal spr\u17CB[] ᜂ()
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

	// Token: 0x06001894 RID: 6292 RVA: 0x00177630 File Offset: 0x00176630
	internal spr\u2618[] ᜅ()
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
		return this.ᜃ;
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x00177674 File Offset: 0x00176674
	internal int ᜀ()
	{
		if (this.ᜃ == null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_20;
				}
			}
			IL_20:
			if (false)
			{
			}
			if (true)
			{
			}
			return 0;
		}
		return this.ᜃ.Length;
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x001776C4 File Offset: 0x001766C4
	internal void ᜀ(int A_0)
	{
		int a_ = 0;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜀ() == A_0)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AB;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 1:
				goto IL_39;
			case 3:
				goto IL_AB;
			case 4:
				return;
			}
			if (A_0 < 0)
			{
				num = 1;
				continue;
			}
			num = 0;
			continue;
			IL_AB:
			this.ᜃ = new spr\u2618[A_0];
			this.ᜁ = new uint[A_0 + 1];
			this.ᜂ = new spr\u17CB[A_0];
			num = 4;
		}
		IL_39:
		throw new ArgumentOutOfRangeException(ClipboardData.b("㑥ᵧѩὫ⵭Ὧݱᩳɵ", a_));
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x0017779C File Offset: 0x0017679C
	internal override int ᜇ()
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
		return 512;
	}

	// Token: 0x06001898 RID: 6296 RVA: 0x001777DC File Offset: 0x001767DC
	internal spr\u1804()
	{
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x001777F0 File Offset: 0x001767F0
	internal spr\u1804(spr\u193A A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x0017780C File Offset: 0x0017680C
	private void ᜀ(spr\u193A A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte[] array = A_0.ᜂ();
				this.ᜁ = new uint[(int)(A_0.ᜁ() + 1)];
				int num = (int)((A_0.ᜁ() + 1) * 4);
				Buffer.BlockCopy(array, 0, this.ᜁ, 0, num);
				this.ᜃ = new spr\u2618[(int)A_0.ᜁ()];
				this.ᜂ = new spr\u17CB[(int)A_0.ᜁ()];
				int num2 = num;
				int num3 = 0;
				int num4 = 5;
				for (;;)
				{
					switch (num4)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_181;
						default:
						{
							if (false)
							{
							}
							int num5;
							if (num5 >= (int)A_0.ᜁ())
							{
								num4 = 6;
								continue;
							}
							num2 = (int)(this.ᜂ[num5].ᜂ() * 2);
							this.ᜃ[num5] = new spr\u2618();
							this.ᜃ[num5].ᜁ(array, num2);
							num5++;
							if (true)
							{
							}
							num4 = 3;
							continue;
						}
						}
						break;
					case 1:
						goto IL_181;
					case 2:
						goto IL_132;
					case 3:
						goto IL_132;
					case 4:
					{
						int num5 = 0;
						num4 = 2;
						continue;
					}
					case 5:
						goto IL_175;
					case 6:
						return;
					case 7:
						goto IL_175;
					}
					break;
					IL_181:
					if (num3 >= (int)A_0.ᜁ())
					{
						num4 = 4;
						continue;
					}
					this.ᜂ[num3] = new spr\u17CB();
					this.ᜂ[num3].ᜁ(array, num2);
					num2 += 13;
					num3++;
					num4 = 7;
					continue;
					IL_132:
					num4 = 0;
					continue;
					IL_175:
					num4 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x0600189B RID: 6299 RVA: 0x001779C4 File Offset: 0x001769C4
	internal spr\u193A ᜁ()
	{
		switch (0)
		{
		default:
		{
			spr\u193A spr_u193A;
			for (;;)
			{
				spr_u193A = new spr\u193A();
				int num = this.ᜀ();
				int num2 = this.ᜁ.Length * 4;
				spr_u193A.ᜀ((byte)num);
				Buffer.BlockCopy(this.ᜁ, 0, spr_u193A.ᜂ(), 0, num2);
				int num3 = num2;
				byte b = byte.MaxValue;
				int num4 = 0;
				int num5 = 7;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						goto IL_BB;
					case 1:
						return spr_u193A;
					case 2:
						this.ᜂ[num4] = new spr\u17CB();
						num5 = 0;
						continue;
					case 3:
						if (this.ᜂ[num4] == null)
						{
							num5 = 2;
							continue;
						}
						goto IL_16D;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BB;
						default:
							if (false)
							{
							}
							b -= (byte)(this.ᜃ[num4].ᜇ() / 2);
							num5 = 3;
							continue;
						}
						break;
					case 5:
						if (num4 >= num)
						{
							num5 = 1;
							continue;
						}
						num5 = 6;
						continue;
					case 6:
						if (this.ᜃ[num4] != null)
						{
							num5 = 4;
							continue;
						}
						goto IL_8D;
					case 7:
						goto IL_101;
					case 8:
						goto IL_101;
					case 9:
						goto IL_8D;
					}
					break;
					IL_8D:
					num4++;
					num5 = 8;
					continue;
					IL_101:
					if (true)
					{
					}
					num5 = 5;
					continue;
					IL_16D:
					spr\u17CB spr_u17CB = this.ᜂ[num4];
					spr_u17CB.ᜀ(b);
					spr_u17CB.ᜀ(spr_u193A.ᜂ(), num3);
					num3 += 13;
					this.ᜃ[num4].ᜀ(spr_u193A.ᜂ(), (int)(spr_u17CB.ᜂ() * 2));
					num5 = 9;
					continue;
					IL_BB:
					goto IL_16D;
				}
			}
			return spr_u193A;
		}
		}
	}

	// Token: 0x0600189C RID: 6300 RVA: 0x00177BA0 File Offset: 0x00176BA0
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 13;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_4E;
			case 1:
				if (A_1 >= 0)
				{
					num = 4;
					continue;
				}
				goto IL_B7;
			case 2:
				goto IL_88;
			case 3:
				if (A_1 + 512 > A_0.Length)
				{
					num = 2;
					continue;
				}
				goto IL_CB;
			case 4:
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 0;
			}
			else
			{
				num = 1;
			}
		}
		IL_4E:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_88:
			break;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("ቲݴն㵸᩺ॼṾ", a_));
		}
		IL_B7:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᩲ㩴ᅶὸࡺ᡼୾", a_));
		IL_CB:
		spr\u193A spr_u193A = this.ᜁ();
		spr_u193A.ᜀ(A_0, A_1);
		return 512;
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x00177C90 File Offset: 0x00176C90
	internal void ᜀ(BinaryWriter A_0, Stream A_1)
	{
		switch (0)
		{
		default:
		{
			long position;
			int num;
			for (;;)
			{
				position = A_1.Position;
				num = this.ᜀ();
				int num2 = this.ᜁ.Length * 4;
				int num3 = 0;
				int num4 = this.ᜁ.Length;
				int num5 = 9;
				for (;;)
				{
					int num6;
					byte b;
					int num7;
					switch (num5)
					{
					case 0:
						goto IL_8C;
					case 1:
						if (num3 >= num4)
						{
							num5 = 11;
							continue;
						}
						A_0.Write(this.ᜁ[num3]);
						num3++;
						num5 = 3;
						continue;
					case 2:
						if (num6 >= num)
						{
							num5 = 13;
							continue;
						}
						num5 = 7;
						continue;
					case 3:
						goto IL_129;
					case 4:
						this.ᜂ[num6] = new spr\u17CB();
						goto IL_18D;
					case 5:
						b -= (byte)(this.ᜃ[num6].ᜇ() / 2);
						num5 = 8;
						continue;
					case 6:
						goto IL_19E;
					case 7:
						if (this.ᜃ[num6] != null)
						{
							num5 = 5;
							continue;
						}
						goto IL_14B;
					case 8:
						if (this.ᜂ[num6] == null)
						{
							num5 = 4;
							continue;
						}
						goto IL_8C;
					case 9:
						goto IL_129;
					case 10:
						goto IL_14B;
					case 11:
						num7 = num2;
						b = byte.MaxValue;
						num6 = 0;
						num5 = 6;
						continue;
					case 12:
						goto IL_19E;
					case 13:
						goto IL_1DA;
					}
					break;
					IL_8C:
					spr\u17CB spr_u17CB = this.ᜂ[num6];
					spr_u17CB.ᜀ(b);
					A_1.Position = position + (long)num7;
					spr_u17CB.ᜀ(A_0);
					num7 += 13;
					A_1.Position = position + (long)(spr_u17CB.ᜂ() * 2);
					this.ᜃ[num6].ᜀ(A_0, A_1);
					num5 = 10;
					continue;
					IL_129:
					num5 = 1;
					continue;
					IL_14B:
					num6++;
					num5 = 12;
					continue;
					IL_18D:
					num5 = 0;
					continue;
					IL_19E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_18D;
					default:
						if (false)
						{
						}
						num5 = 2;
						break;
					}
				}
			}
			IL_1DA:
			if (true)
			{
			}
			A_1.Position = position + 511L;
			A_1.WriteByte((byte)num);
			return;
		}
		}
	}

	// Token: 0x04001CBC RID: 7356
	private new const int ᜀ = 4;

	// Token: 0x04001CBD RID: 7357
	private new uint[] ᜁ;

	// Token: 0x04001CBE RID: 7358
	private new spr\u17CB[] ᜂ;

	// Token: 0x04001CBF RID: 7359
	private new spr\u2618[] ᜃ;
}
