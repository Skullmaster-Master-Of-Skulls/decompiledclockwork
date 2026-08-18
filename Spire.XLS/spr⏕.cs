using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000548 RID: 1352
internal class spr\u23D5
{
	// Token: 0x06005221 RID: 21025 RVA: 0x00332534 File Offset: 0x00331534
	public spr\u23D5()
	{
		this.ᜁ = new List<int>();
	}

	// Token: 0x06005222 RID: 21026 RVA: 0x00332560 File Offset: 0x00331560
	public spr\u23D5(Stream A_0, spr\u19E8 A_1)
	{
		int num = A_1.ᜎ();
		int num2 = A_1.ᜁ();
		ushort a_ = A_1.\u170D();
		int capacity = 109 + num * (num2 - 4) / 4;
		this.ᜁ = new List<int>(capacity);
		this.ᜁ.AddRange(A_1.ᜂ());
		if (num > 0)
		{
			int i = A_1.ᜋ();
			A_1.\u170D();
			byte[] array = new byte[num2];
			int[] array2 = new int[num2 / 4 - 1];
			while (i >= 0)
			{
				long position = spr\u2604.ᜀ(i, a_);
				this.ᜂ.Add(i);
				A_0.Position = position;
				A_0.Read(array, 0, num2);
				Buffer.BlockCopy(array, 0, array2, 0, num2 - 4);
				this.ᜁ.AddRange(array2);
				i = BitConverter.ToInt32(array, num2 - 4);
			}
		}
	}

	// Token: 0x06005223 RID: 21027 RVA: 0x00332644 File Offset: 0x00331644
	public List<int> ᜀ()
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

	// Token: 0x06005224 RID: 21028 RVA: 0x00332688 File Offset: 0x00331688
	internal void ᜀ(Stream A_0, spr\u19E8 A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 27;
			for (;;)
			{
				int num2;
				int[] array;
				int num3;
				int num4;
				int num5;
				int count;
				long position;
				int num6;
				int count2;
				int num7;
				int num8;
				switch (num)
				{
				case 0:
					goto IL_125;
				case 1:
					num = 21;
					continue;
				case 2:
					num = 0;
					continue;
				case 3:
					A_1.ᜀ(this.ᜂ[0]);
					A_1.ᜃ(this.ᜂ.Count);
					num = 23;
					continue;
				case 4:
					goto IL_331;
				case 5:
					if (num2 >= 109)
					{
						num = 28;
						continue;
					}
					array[num2] = -1;
					num2++;
					num = 25;
					continue;
				case 6:
					goto IL_2D3;
				case 7:
					if (num3 >= num4)
					{
						num = 30;
						continue;
					}
					num = 11;
					continue;
				case 8:
					goto IL_331;
				case 9:
				{
					if (num5 >= count)
					{
						num = 10;
						continue;
					}
					int a_2 = this.ᜂ[num5];
					position = A_1.ᜅ(a_2);
					num3 = 0;
					num6 = 0;
					num = 12;
					continue;
				}
				case 10:
					return;
				case 11:
					if (num2 >= count2)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					num = 22;
					continue;
				case 12:
					goto IL_355;
				case 13:
					num7 = -2;
					goto IL_24D;
				case 14:
					goto IL_BA;
				case 15:
					num = 20;
					continue;
				case 16:
					num = 24;
					continue;
				case 17:
					goto IL_2D3;
				case 18:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_376;
					default:
						if (false)
						{
						}
						goto IL_355;
					}
					break;
				case 19:
					if (this.ᜂ.Count > 0)
					{
						num = 3;
						continue;
					}
					goto IL_210;
				case 20:
					if (num2 >= 109)
					{
						num = 2;
						continue;
					}
					array[num2] = this.ᜁ[num2];
					num2++;
					num = 8;
					continue;
				case 21:
					num8 = -1;
					goto IL_3B7;
				case 22:
					num8 = this.ᜁ[num2];
					goto IL_3B7;
				case 23:
					goto IL_210;
				case 24:
					num7 = this.ᜂ[num5 + 1];
					goto IL_24D;
				case 25:
					goto IL_125;
				case 26:
					if (num2 < count2)
					{
						num = 15;
						continue;
					}
					goto IL_125;
				case 28:
					num = 19;
					continue;
				case 29:
					if (num5 != count - 1)
					{
						num = 16;
						continue;
					}
					num = 13;
					continue;
				case 30:
					goto IL_376;
				}
				if (A_0 == null)
				{
					num = 14;
					continue;
				}
				count2 = this.ᜁ.Count;
				array = A_1.ᜂ();
				num2 = 0;
				num = 4;
				continue;
				IL_125:
				num = 5;
				continue;
				IL_210:
				byte[] array2 = new byte[A_1.ᜁ()];
				int num9 = A_1.ᜁ();
				num4 = num9 / 4 - 1;
				num5 = 0;
				count = this.ᜂ.Count;
				num = 17;
				continue;
				IL_24D:
				int value = num7;
				byte[] bytes = BitConverter.GetBytes(value);
				Buffer.BlockCopy(bytes, 0, array2, num9 - 4, 4);
				A_0.Position = position;
				A_0.Write(array2, 0, num9);
				num5++;
				num = 6;
				continue;
				IL_2D3:
				num = 9;
				continue;
				IL_331:
				num = 26;
				continue;
				IL_355:
				num = 7;
				continue;
				IL_376:
				num = 29;
				continue;
				IL_3B7:
				int value2 = num8;
				byte[] bytes2 = BitConverter.GetBytes(value2);
				Buffer.BlockCopy(bytes2, 0, array2, num6, 4);
				num3++;
				num2++;
				num6 += 4;
				num = 18;
			}
			IL_BA:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺似娾⁀⹂", a_));
		}
		}
	}

	// Token: 0x06005225 RID: 21029 RVA: 0x00332AA0 File Offset: 0x00331AA0
	internal void ᜃ(int A_0, sprប A_1)
	{
		for (;;)
		{
			int num = A_0 - 109;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						int a_ = (int)Math.Ceiling((double)(num * 4) / (double)(A_1.ᜁ() - 4));
						this.ᜂ(a_, A_1);
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					}
					break;
				case 2:
					if (num > 0)
					{
						num2 = 1;
						continue;
					}
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06005226 RID: 21030 RVA: 0x00332B34 File Offset: 0x00331B34
	private void ᜂ(int A_0, sprប A_1)
	{
		int count;
		for (;;)
		{
			count = this.ᜂ.Count;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_38;
				case 1:
					if (count == A_0)
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
				case 2:
					goto IL_59;
				case 3:
					if (count > A_0)
					{
						num = 2;
						continue;
					}
					goto IL_8A;
				}
				break;
			}
		}
		IL_38:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_8A:
			this.ᜀ(A_0 - count, A_1);
			return;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			return;
		}
		IL_59:
		this.ᜁ(count - A_0, A_1);
	}

	// Token: 0x06005227 RID: 21031 RVA: 0x00332BD8 File Offset: 0x00331BD8
	private void ᜁ(int A_0, sprប A_1)
	{
		int a_ = 5;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_98;
			case 1:
			{
				int num2;
				if (num2 >= A_0)
				{
					num = 3;
					continue;
				}
				int num3;
				int a_2 = this.ᜂ[num3];
				A_1.ᜄ(a_2);
				num2++;
				num3--;
				num = 2;
				continue;
			}
			case 2:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_69;
				default:
					if (false)
					{
					}
					goto IL_98;
				}
				break;
			case 3:
				goto IL_BC;
			case 4:
				if (A_0 == 0)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
			case 5:
				goto IL_141;
			case 6:
				return;
			case 7:
			{
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				int num2 = 0;
				int num3 = this.ᜂ.Count - 1;
				num = 0;
				continue;
			}
			case 8:
				goto IL_4D;
			}
			if (A_0 < 0)
			{
				num = 8;
				continue;
			}
			num = 4;
			continue;
			IL_98:
			num = 1;
		}
		IL_4D:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠺堼尾㕀ⱂ㝄ц♈㹊⍌㭎", a_));
		IL_69:
		throw new ArgumentNullException(RecordTableEnumerator.b("崺尼䬾", a_));
		IL_BC:
		this.ᜂ.RemoveRange(this.ᜂ.Count - A_0, A_0);
		throw new NotImplementedException();
		IL_141:
		goto IL_69;
	}

	// Token: 0x06005228 RID: 21032 RVA: 0x00332D4C File Offset: 0x00331D4C
	private void ᜀ(int A_0, sprប A_1)
	{
		int a_ = 4;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_C3;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_91;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				goto IL_F8;
			case 3:
				goto IL_C3;
			case 4:
				return;
			case 5:
			{
				int num2;
				if (num2 >= A_0)
				{
					num = 4;
					continue;
				}
				int item = A_1.ᜁ(-4);
				this.ᜂ.Add(item);
				num2++;
				num = 3;
				continue;
			}
			case 6:
			{
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				int num2 = 0;
				num = 0;
				continue;
			}
			case 7:
				goto IL_6C;
			}
			if (A_0 < 0)
			{
				if (true)
				{
				}
				num = 7;
				continue;
			}
			num = 6;
			continue;
			IL_C3:
			num = 5;
		}
		IL_6C:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤹夻崽㐿ⵁ㙃Յ❇㽉≋㩍", a_));
		IL_91:
		throw new ArgumentNullException(RecordTableEnumerator.b("尹崻䨽", a_));
		IL_F8:
		goto IL_91;
	}

	// Token: 0x040024A0 RID: 9376
	public const int ᜀ = 109;

	// Token: 0x040024A1 RID: 9377
	private List<int> ᜁ;

	// Token: 0x040024A2 RID: 9378
	private List<int> ᜂ = new List<int>();
}
