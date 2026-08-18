using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004D6 RID: 1238
internal class spr\u24F3
{
	// Token: 0x06004BFC RID: 19452 RVA: 0x002E8D30 File Offset: 0x002E7D30
	public spr\u24F3(BinaryWriter A_0)
	{
		int a_ = 15;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
		}
		this.ᜀ = A_0;
	}

	// Token: 0x06004BFD RID: 19453 RVA: 0x002E8D6C File Offset: 0x002E7D6C
	public int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 12;
		if (A_0 == null)
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
				break;
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("♁╃㉅⥇", a_));
		}
		return this.ᜀ(A_0, A_1, A_0.Length - A_1, 8224);
	}

	// Token: 0x06004BFE RID: 19454 RVA: 0x002E8DDC File Offset: 0x002E7DDC
	public int ᜀ(byte[] A_0, int A_1, int A_2)
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
		return this.ᜀ(A_0, A_1, A_2, 8224);
	}

	// Token: 0x06004BFF RID: 19455 RVA: 0x002E8E28 File Offset: 0x002E7E28
	public int ᜀ(byte[] A_0, int A_1, int A_2, int A_3)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 12;
			int num4;
			for (;;)
			{
				int num3;
				int num6;
				switch (num)
				{
				case 0:
					num = 10;
					continue;
				case 1:
					goto IL_2DB;
				case 2:
					num = 3;
					continue;
				case 3:
				{
					int num2;
					if (num2 > num3)
					{
						num = 4;
						continue;
					}
					return num4;
				}
				case 4:
				{
					int num2;
					int num5 = num2 - num3;
					this.ᜀ.Write(60);
					this.ᜀ.Write((ushort)num5);
					this.ᜀ.Write(A_0, num3, num5);
					num4 += 4 + num5;
					num = 14;
					continue;
				}
				case 5:
					num = 18;
					continue;
				case 6:
				{
					int num2;
					if (num3 >= num2)
					{
						num = 2;
						continue;
					}
					num = 24;
					continue;
				}
				case 7:
					num = 20;
					continue;
				case 8:
					num = 11;
					continue;
				case 9:
					goto IL_A6;
				case 10:
					if (A_1 < 0)
					{
						num = 13;
						continue;
					}
					num = 21;
					continue;
				case 11:
				{
					if (A_3 > 8224)
					{
						num = 22;
						continue;
					}
					num4 = 0;
					int num2 = A_1 + A_2;
					num3 = A_1;
					num = 16;
					continue;
				}
				case 13:
					goto IL_2B2;
				case 14:
					goto IL_266;
				case 15:
					if (A_3 >= 0)
					{
						num = 8;
						continue;
					}
					goto IL_BE;
				case 16:
					goto IL_E6;
				case 17:
					if (A_0.Length < A_1)
					{
						goto IL_D2;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A6;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 18:
					num6 = A_3;
					goto IL_1DF;
				case 19:
					goto IL_E6;
				case 20:
					if (A_2 < 0)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					num = 15;
					continue;
				case 21:
					if (A_0.Length >= A_1 + A_2)
					{
						num = 7;
						continue;
					}
					goto IL_26B;
				case 22:
					goto IL_1B2;
				case 23:
				{
					int num2;
					num6 = num2 - num3;
					goto IL_1DF;
				}
				case 24:
				{
					int num2;
					if (num2 - num3 >= A_3)
					{
						num = 5;
						continue;
					}
					num = 23;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 17;
				continue;
				IL_E6:
				num = 6;
				continue;
				IL_1DF:
				int num7 = num6;
				this.ᜀ.Write(60);
				this.ᜀ.Write((ushort)num7);
				this.ᜀ.Write(A_0, num3, num7);
				num4 += num7 + 4;
				num3 += A_3;
				num = 19;
			}
			IL_A6:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍆⡈㽊ⱌ", a_));
			IL_BE:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩆⡈㍊Ṍ♎⭐㙒", a_));
			IL_D2:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㑆㵈⩊㽌㭎", a_));
			IL_1B2:
			goto IL_BE;
			IL_266:
			return num4;
			IL_26B:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭆ⱈ╊⩌㭎㥐", a_));
			IL_2B2:
			goto IL_D2;
			IL_2DB:
			goto IL_26B;
		}
		}
	}

	// Token: 0x06004C00 RID: 19456 RVA: 0x002E9168 File Offset: 0x002E8168
	public int ᜀ(byte[] A_0, int A_1, spr\u251F A_2, int A_3)
	{
		int a_ = 11;
		if (A_0 == null)
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
				break;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("╀≂ㅄ♆", a_));
		}
		return this.ᜀ(A_0, A_1, A_0.Length - A_1, 8224, A_2, A_3);
	}

	// Token: 0x06004C01 RID: 19457 RVA: 0x002E91DC File Offset: 0x002E81DC
	public int ᜀ(byte[] A_0, int A_1, int A_2, spr\u251F A_3, int A_4)
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
		return this.ᜀ(A_0, A_1, A_2, 8224, A_3, A_4);
	}

	// Token: 0x06004C02 RID: 19458 RVA: 0x002E922C File Offset: 0x002E822C
	public int ᜀ(byte[] A_0, int A_1, int A_2, int A_3, spr\u251F A_4, int A_5)
	{
		int a_ = 14;
		int num5;
		bool autoGrowData;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 12;
				for (;;)
				{
					int num3;
					int num6;
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 - num3 >= A_3)
						{
							num = 27;
							continue;
						}
						num = 17;
						continue;
					}
					case 1:
					{
						int num2;
						int num4 = num2 - num3;
						A_4.ᜀ(A_5, 60);
						A_4.ᜀ(A_5 + 2, (ushort)num4);
						A_4.ᜀ(A_5 + 4, A_0, num3, num4);
						num5 += num4 + 4;
						A_5 += num4 + 4;
						num = 9;
						continue;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
							if (false)
							{
							}
							num = 10;
							continue;
						}
						break;
					case 3:
						goto IL_134;
					case 4:
						if (A_1 < 0)
						{
							num = 13;
							continue;
						}
						num = 20;
						continue;
					case 5:
						if (A_3 >= 0)
						{
							num = 2;
							continue;
						}
						goto IL_139;
					case 6:
						num = 18;
						continue;
					case 7:
						num = 4;
						continue;
					case 8:
					{
						if (A_5 < 0)
						{
							num = 21;
							continue;
						}
						num5 = 0;
						int num2 = A_1 + A_2;
						num3 = A_1;
						autoGrowData = A_4.AutoGrowData;
						A_4.AutoGrowData = true;
						num = 11;
						continue;
					}
					case 9:
						goto IL_38F;
					case 10:
						if (A_3 > 8224)
						{
							num = 25;
							continue;
						}
						num = 14;
						continue;
					case 11:
						goto IL_311;
					case 13:
						goto IL_D7;
					case 14:
						if (A_4 == null)
						{
							num = 26;
							continue;
						}
						num = 8;
						continue;
					case 15:
						if (A_0.Length >= A_1)
						{
							num = 7;
							continue;
						}
						goto IL_28F;
					case 16:
						num6 = A_3;
						goto IL_21C;
					case 17:
					{
						int num2;
						num6 = num2 - num3;
						goto IL_21C;
					}
					case 18:
					{
						int num2;
						if (num2 > num3)
						{
							num = 1;
							continue;
						}
						goto IL_3C3;
					}
					case 19:
						num = 23;
						continue;
					case 20:
						if (A_0.Length >= A_1 + A_2)
						{
							num = 19;
							continue;
						}
						goto IL_332;
					case 21:
						goto IL_217;
					case 22:
					{
						int num2;
						if (num3 >= num2)
						{
							num = 6;
							continue;
						}
						if (true)
						{
						}
						num = 0;
						continue;
					}
					case 23:
						if (A_2 < 0)
						{
							num = 3;
							continue;
						}
						num = 5;
						continue;
					case 24:
						goto IL_311;
					case 25:
						goto IL_1F5;
					case 26:
						goto IL_2EA;
					case 27:
						num = 16;
						continue;
					case 28:
						goto IL_B9;
					}
					if (A_0 == null)
					{
						num = 28;
						continue;
					}
					num = 15;
					continue;
					IL_21C:
					int num7 = num6;
					A_4.ᜀ(A_5, 60);
					A_4.ᜀ(A_5 + 2, (ushort)num7);
					A_4.ᜀ(A_5 + 4, A_0, num3, num7);
					num5 += num7 + 4;
					A_5 += num7 + 4;
					num3 += A_3;
					num = 24;
					continue;
					IL_311:
					num = 22;
				}
				break;
			}
			}
		}
		IL_B9:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁃❅㱇⭉", a_));
		IL_D7:
		goto IL_28F;
		IL_134:
		goto IL_332;
		IL_139:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⥃❅ぇ᥉╋㑍㕏", a_));
		IL_1F5:
		goto IL_139;
		IL_217:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭃⁅⹇㥉⥋㩍", a_));
		IL_28F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㝃㉅⥇㡉㡋", a_));
		IL_2EA:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁃⍅㭇㹉╋⁍ㅏ♑㵓㥕㙗", a_));
		IL_332:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡃⍅♇ⵉ㡋♍", a_));
		IL_38F:
		IL_3C3:
		A_4.AutoGrowData = autoGrowData;
		return num5;
	}

	// Token: 0x04002293 RID: 8851
	private BinaryWriter ᜀ;
}
