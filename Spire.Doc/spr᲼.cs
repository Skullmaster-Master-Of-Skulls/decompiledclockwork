using System;
using Spire.CompoundFile.Doc;

// Token: 0x0200023C RID: 572
[CLSCompliant(false)]
internal class spr\u1CBC : spr\u23F8
{
	// Token: 0x06001B4C RID: 6988 RVA: 0x001C6CA8 File Offset: 0x001C5CA8
	internal spr\u1CBC()
	{
	}

	// Token: 0x06001B4D RID: 6989 RVA: 0x001C6CBC File Offset: 0x001C5CBC
	internal spr\u1CBC(byte[] A_0) : base(A_0)
	{
	}

	// Token: 0x06001B4E RID: 6990 RVA: 0x001C6CD0 File Offset: 0x001C5CD0
	internal override void ᜂ(byte[] A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			int num3;
			int num4;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A8:
				if (num >= num2)
				{
					num3 = 0;
				}
				else
				{
					this.ᜁ[num] = new sprᨮ();
					this.ᜁ[num].ᜁ(A_0, num4);
					num++;
					num4 += 8;
					num3 = 1;
				}
				break;
			default:
				if (false)
				{
				}
				goto IL_55;
			}
			for (;;)
			{
				IL_36:
				if (true)
				{
				}
				switch (num3)
				{
				case 0:
					return;
				case 1:
					goto IL_9F;
				case 2:
					goto IL_A8;
				case 3:
					goto IL_9F;
				}
				goto IL_55;
				IL_9F:
				num3 = 2;
			}
			return;
			IL_55:
			int num5 = A_0.Length;
			num2 = (num5 - 4) / 12;
			this.ᜀ = new uint[num2 + 1];
			this.ᜁ = new sprᨮ[num2];
			int num6 = (num2 + 1) * 4;
			Buffer.BlockCopy(A_0, 0, this.ᜀ, 0, num6);
			num4 = num6;
			num = 0;
			num3 = 3;
			goto IL_36;
		}
		}
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x001C6DCC File Offset: 0x001C5DCC
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 3;
			int num6;
			for (;;)
			{
				int num5;
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 1:
					goto IL_1D3;
				case 2:
					goto IL_19A;
				case 4:
					goto IL_68;
				case 5:
				{
					int num2 = this.ᜀ.Length * 4;
					Buffer.BlockCopy(this.ᜀ, 0, A_0, A_1, num2);
					A_1 += num2;
					int num3 = 0;
					int num4 = this.ᜂ();
					num = 11;
					continue;
				}
				case 6:
					if (A_1 + num5 > A_0.Length)
					{
						num = 1;
						continue;
					}
					num6 = A_1;
					A_0[A_1++] = 2;
					BitConverter.GetBytes(num5 - 1 - 4).CopyTo(A_0, A_1);
					A_1 += 4;
					if (true)
					{
					}
					num = 9;
					continue;
				case 7:
				{
					int num3;
					int num4;
					if (num3 < num4)
					{
						A_1 += this.ᜁ[num3].ᜀ(A_0, A_1);
						num3++;
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_19A;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				}
				case 8:
					if (A_1 >= 0)
					{
						num = 0;
						continue;
					}
					goto IL_19C;
				case 9:
					if (this.ᜂ() > 0)
					{
						num = 5;
						continue;
					}
					goto IL_1D5;
				case 10:
					goto IL_161;
				case 11:
					goto IL_128;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num5 = this.ᜇ();
				num = 8;
				continue;
				IL_128:
				num = 7;
				continue;
				IL_19A:
				goto IL_128;
			}
			IL_68:
			throw new ArgumentNullException(ClipboardData.b("ᕳѵ੷㹹ᵻ੽", a_));
			IL_161:
			goto IL_1D5;
			IL_19C:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ᵳ㥵ṷᱹཻ᭽", a_));
			IL_1D3:
			goto IL_19C;
			IL_1D5:
			return A_1 - num6;
		}
		}
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x001C6FB4 File Offset: 0x001C5FB4
	internal uint[] ᜁ()
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
		return this.ᜀ;
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x001C6FF8 File Offset: 0x001C5FF8
	internal sprᨮ[] ᜀ()
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

	// Token: 0x06001B52 RID: 6994 RVA: 0x001C703C File Offset: 0x001C603C
	internal override int ᜇ()
	{
		int num;
		for (;;)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					num = this.ᜀ.Length * 4 + 1 + 4;
					int num2 = 0;
					int num3 = this.ᜁ.Length;
					int num4 = 1;
					for (;;)
					{
						switch (num4)
						{
						case 0:
							return num;
						case 1:
							goto IL_58;
						case 2:
							if (num2 >= num3)
							{
								if (true)
								{
								}
								num4 = 0;
								continue;
							}
							num += this.ᜁ[num2].ᜇ();
							num2++;
							num4 = 3;
							continue;
						case 3:
							goto IL_58;
						}
						break;
						IL_58:
						num4 = 2;
					}
					break;
				}
				}
			}
		}
		return num;
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x001C70EC File Offset: 0x001C60EC
	internal int ᜂ()
	{
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
			if (this.ᜁ == null)
			{
				return 0;
			}
			break;
		}
		return this.ᜁ.Length;
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x001C713C File Offset: 0x001C613C
	internal void ᜀ(int A_0)
	{
		int a_ = 7;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B4;
			case 1:
			{
				int num2;
				if (num2 >= A_0)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				this.ᜁ[num2] = new sprᨮ();
				num2++;
				num = 5;
				continue;
			}
			case 2:
			{
				this.ᜁ = new sprᨮ[A_0];
				this.ᜀ = new uint[A_0 + 1];
				int num2 = 0;
				num = 0;
				continue;
			}
			case 4:
				return;
			case 5:
				goto IL_B4;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_52;
				default:
					if (false)
					{
					}
					if (A_0 != this.ᜂ())
					{
						num = 2;
						continue;
					}
					return;
				}
				break;
			case 7:
				goto IL_52;
			}
			if (A_0 < 0)
			{
				num = 7;
				continue;
			}
			num = 6;
			continue;
			IL_B4:
			num = 1;
		}
		IL_52:
		throw new ArgumentOutOfRangeException(ClipboardData.b("⡬ŮհŲᱴቶ੸㡺ቼ੾", a_));
	}

	// Token: 0x04001EB7 RID: 7863
	private new uint[] ᜀ;

	// Token: 0x04001EB8 RID: 7864
	private new sprᨮ[] ᜁ;
}
