using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200019A RID: 410
internal class spr\u1CC8
{
	// Token: 0x06000FBA RID: 4026 RVA: 0x000F50C8 File Offset: 0x000F40C8
	public spr\u1CC8()
	{
		this.ᜁ = new List<int>();
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x000F50F4 File Offset: 0x000F40F4
	public spr\u1CC8(Stream A_0, spr\u250C A_1)
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
				long position = spr\u20BF.ᜀ(i, a_);
				this.ᜂ.Add(i);
				A_0.Position = position;
				A_0.Read(array, 0, num2);
				Buffer.BlockCopy(array, 0, array2, 0, num2 - 4);
				this.ᜁ.AddRange(array2);
				i = BitConverter.ToInt32(array, num2 - 4);
			}
		}
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x000F51D8 File Offset: 0x000F41D8
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

	// Token: 0x06000FBD RID: 4029 RVA: 0x000F521C File Offset: 0x000F421C
	internal void ᜀ(Stream A_0, spr\u250C A_1)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				IL_25:
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				int num7;
				int count2;
				long position;
				int num8;
				switch (num)
				{
				case 0:
					goto IL_34D;
				case 1:
					num2 = -2;
					goto IL_257;
				case 2:
					num = 4;
					continue;
				case 3:
					num = 15;
					continue;
				case 4:
				{
					if (num3 >= 109)
					{
						num = 9;
						continue;
					}
					int[] array;
					array[num3] = this.ᜁ[num3];
					num3++;
					num = 0;
					continue;
				}
				case 5:
				{
					int count;
					if (num3 < count)
					{
						num = 2;
						continue;
					}
					goto IL_125;
				}
				case 6:
					num4 = this.ᜁ[num3];
					goto IL_3D3;
				case 8:
					if (num5 >= num6)
					{
						num = 26;
						continue;
					}
					num = 13;
					continue;
				case 9:
					num = 25;
					continue;
				case 10:
				{
					if (num7 >= count2)
					{
						num = 24;
						continue;
					}
					int a_2 = this.ᜂ[num7];
					position = A_1.ᜅ(a_2);
					num5 = 0;
					num8 = 0;
					num = 14;
					continue;
				}
				case 11:
					goto IL_34D;
				case 12:
					goto IL_371;
				case 13:
				{
					int count;
					if (num3 >= count)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					num = 6;
					continue;
				}
				case 14:
					goto IL_371;
				case 15:
					num4 = -1;
					goto IL_3D3;
				case 16:
					num = 29;
					continue;
				case 17:
					goto IL_21A;
				case 18:
				{
					if (num3 >= 109)
					{
						num = 16;
						continue;
					}
					int[] array;
					array[num3] = -1;
					num3++;
					num = 22;
					continue;
				}
				case 19:
					goto IL_BA;
				case 20:
					goto IL_2F9;
				case 21:
					goto IL_2F9;
				case 22:
					goto IL_125;
				case 23:
					A_1.ᜀ(this.ᜂ[0]);
					A_1.ᜃ(this.ᜂ.Count);
					num = 17;
					continue;
				case 24:
					return;
				case 25:
					goto IL_125;
				case 26:
					num = 30;
					continue;
				case 27:
					num = 28;
					continue;
				case 28:
					num2 = this.ᜂ[num7 + 1];
					goto IL_257;
				case 29:
					if (this.ᜂ.Count > 0)
					{
						num = 23;
						continue;
					}
					goto IL_21A;
				case 30:
					if (num7 != count2 - 1)
					{
						num = 27;
						continue;
					}
					num = 1;
					continue;
				}
				while (A_0 != null)
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
						int count = this.ᜁ.Count;
						int[] array = A_1.ᜂ();
						num3 = 0;
						num = 11;
						goto IL_25;
					}
					}
				}
				num = 19;
				continue;
				IL_125:
				num = 18;
				continue;
				IL_21A:
				byte[] array2 = new byte[A_1.ᜁ()];
				int num9 = A_1.ᜁ();
				num6 = num9 / 4 - 1;
				num7 = 0;
				count2 = this.ᜂ.Count;
				num = 21;
				continue;
				IL_257:
				int value = num2;
				byte[] bytes = BitConverter.GetBytes(value);
				Buffer.BlockCopy(bytes, 0, array2, num9 - 4, 4);
				A_0.Position = position;
				A_0.Write(array2, 0, num9);
				num7++;
				num = 20;
				continue;
				IL_2F9:
				num = 10;
				continue;
				IL_34D:
				num = 5;
				continue;
				IL_371:
				num = 8;
				continue;
				IL_3D3:
				int value2 = num4;
				byte[] bytes2 = BitConverter.GetBytes(value2);
				Buffer.BlockCopy(bytes2, 0, array2, num8, 4);
				num5++;
				num3++;
				num8 += 4;
				num = 12;
			}
			IL_BA:
			throw new ArgumentNullException(ClipboardData.b("੸ེོ᩾", a_));
		}
		}
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x000F5634 File Offset: 0x000F4634
	internal void ᜃ(int A_0, sprᬩ A_1)
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
					goto IL_5E;
				case 1:
				{
					int a_ = (int)Math.Ceiling((double)(num * 4) / (double)(A_1.ᜁ() - 4));
					this.ᜂ(a_, A_1);
					num2 = 0;
					continue;
				}
				case 2:
					if (num > 0)
					{
						num2 = 1;
						continue;
					}
					goto IL_60;
				}
				break;
			}
		}
		IL_5E:
		IL_60:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_5E;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x000F56C8 File Offset: 0x000F46C8
	private void ᜂ(int A_0, sprᬩ A_1)
	{
		int count;
		for (;;)
		{
			for (;;)
			{
				count = this.ᜂ.Count;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (count > A_0)
						{
							num = 2;
							continue;
						}
						goto IL_8A;
					case 1:
						if (count != A_0)
						{
							num = 0;
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
							num = 3;
							continue;
						}
						break;
					case 2:
						goto IL_7F;
					case 3:
						return;
					}
					break;
				}
			}
		}
		return;
		IL_7F:
		if (true)
		{
		}
		this.ᜁ(count - A_0, A_1);
		return;
		IL_8A:
		this.ᜀ(A_0 - count, A_1);
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x000F576C File Offset: 0x000F476C
	private void ᜁ(int A_0, sprᬩ A_1)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				goto IL_A8;
			case 1:
				goto IL_DE;
			case 3:
				if (A_1 == null)
				{
					num = 9;
					continue;
				}
				goto IL_5C;
			case 4:
				return;
			case 5:
				goto IL_A8;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5C;
				default:
				{
					if (false)
					{
					}
					if (num2 >= A_0)
					{
						num = 1;
						continue;
					}
					int a_2 = this.ᜂ[num3];
					A_1.ᜄ(a_2);
					num2++;
					num3--;
					num = 0;
					continue;
				}
				}
				break;
			case 7:
				goto IL_4D;
			case 8:
				if (A_0 == 0)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			case 9:
				goto IL_141;
			}
			if (A_0 < 0)
			{
				num = 7;
				continue;
			}
			num = 8;
			continue;
			IL_5C:
			num2 = 0;
			num3 = this.ᜂ.Count - 1;
			num = 5;
			continue;
			IL_A8:
			num = 6;
		}
		IL_4D:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᥩ५൭ѯᵱٳ㕵᝷ཹቻ੽", a_));
		IL_DE:
		this.ᜂ.RemoveRange(this.ᜂ.Count - A_0, A_0);
		throw new NotImplementedException();
		IL_141:
		throw new ArgumentNullException(ClipboardData.b("౩൫ᩭ", a_));
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x000F58E0 File Offset: 0x000F48E0
	private void ᜀ(int A_0, sprᬩ A_1)
	{
		int a_ = 11;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B9;
			case 1:
			{
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				int num2 = 0;
				num = 0;
				continue;
			}
			case 2:
				return;
			case 3:
				goto IL_B9;
			case 4:
				goto IL_F8;
			case 6:
				goto IL_6C;
			case 7:
			{
				int num2;
				if (num2 >= A_0)
				{
					num = 2;
					continue;
				}
				int item = A_1.ᜁ(-4);
				this.ᜂ.Add(item);
				num2++;
				num = 3;
				continue;
			}
			}
			if (A_0 >= 0)
			{
				num = 1;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_91;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 6;
				continue;
			}
			IL_B9:
			num = 7;
		}
		IL_6C:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ɰᙲᙴͶᙸॺ㹼ၾ", a_));
		IL_91:
		throw new ArgumentNullException(ClipboardData.b("ᝰቲŴ", a_));
		IL_F8:
		goto IL_91;
	}

	// Token: 0x0400178A RID: 6026
	public const int ᜀ = 109;

	// Token: 0x0400178B RID: 6027
	private List<int> ᜁ;

	// Token: 0x0400178C RID: 6028
	private List<int> ᜂ = new List<int>();
}
