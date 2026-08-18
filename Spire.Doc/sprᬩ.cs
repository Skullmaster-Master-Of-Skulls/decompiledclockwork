using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200035B RID: 859
internal class sprᬩ
{
	// Token: 0x06002E14 RID: 11796 RVA: 0x002BE8B8 File Offset: 0x002BD8B8
	public int ᜁ()
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
		return 1 << (int)this.ᜂ;
	}

	// Token: 0x06002E15 RID: 11797 RVA: 0x002BE900 File Offset: 0x002BD900
	public sprᬩ(Stream A_0, ushort A_1, int A_2)
	{
		this.ᜀ = new List<int>();
		this.ᜁ = new List<int>();
		base..ctor();
		this.ᜃ = A_0;
		this.ᜂ = A_1;
		this.ᜄ = A_2;
	}

	// Token: 0x06002E16 RID: 11798 RVA: 0x002BE940 File Offset: 0x002BD940
	public sprᬩ(Stream A_0, ushort A_1, Stream A_2, int A_3)
	{
		this.ᜀ = new List<int>();
		this.ᜁ = new List<int>();
		base..ctor();
		this.ᜂ = A_1;
		this.ᜄ = A_3;
		this.ᜃ = A_0;
		A_2.Position = 0L;
		byte[] array = new byte[4];
		while (A_2.Read(array, 0, 4) > 0)
		{
			this.ᜀ.Add(BitConverter.ToInt32(array, 0));
		}
	}

	// Token: 0x06002E17 RID: 11799 RVA: 0x002BE9B4 File Offset: 0x002BD9B4
	public sprᬩ(spr\u20BF A_0, Stream A_1, spr\u1CC8 A_2, spr\u250C A_3)
	{
		int a_ = 6;
		this.ᜀ = new List<int>();
		this.ᜁ = new List<int>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("੫ݭᱯ᝱", a_));
		}
		if (A_1 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("Ὣᩭɯ᝱ᕳ᭵", a_));
		}
		this.ᜃ = A_0.ᜉ();
		List<int> list = A_2.ᜀ();
		int num = A_3.ᜁ();
		this.ᜂ = A_3.\u170D();
		byte[] array = new byte[num];
		int[] array2 = new int[num >> 2];
		this.ᜄ = 512;
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			int num2 = list[i];
			if (num2 >= 0)
			{
				A_0.ᜀ(array, 0, num2, A_3);
				Buffer.BlockCopy(array, 0, array2, 0, num);
				this.ᜀ.AddRange(array2);
			}
			i++;
		}
	}

	// Token: 0x06002E18 RID: 11800 RVA: 0x002BEAB0 File Offset: 0x002BDAB0
	public byte[] ᜀ(Stream A_0, int A_1, spr\u20BF A_2)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				List<int> list;
				byte[] array;
				int num3;
				int num4;
				int num5;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 >= 0)
						{
							num = 2;
							continue;
						}
						goto IL_22F;
					}
					case 1:
					{
						int num2;
						if (num2 == -2)
						{
							num = 7;
							continue;
						}
						num = 0;
						continue;
					}
					case 2:
						num = 5;
						continue;
					case 3:
						goto IL_FE;
					case 4:
						goto IL_16D;
					case 5:
					{
						int num2;
						if (num2 >= this.ᜀ.Count)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						list.Add(num2);
						num2 = this.ᜀ[num2];
						num = 17;
						continue;
					}
					case 6:
						goto IL_18C;
					case 7:
					{
						int count = list.Count;
						array = new byte[count << (int)this.ᜂ];
						num3 = 1 << (int)this.ᜂ;
						num4 = 0;
						num5 = 0;
						num = 6;
						continue;
					}
					case 8:
					{
						int count;
						if (num4 >= count)
						{
							num = 16;
							continue;
						}
						goto IL_1ED;
					}
					case 10:
					{
						if (A_1 < 0)
						{
							num = 15;
							continue;
						}
						list = new List<int>();
						A_2.ᜅ();
						int num2 = A_1;
						num = 4;
						continue;
					}
					case 11:
						goto IL_16B;
					case 12:
						goto IL_9C;
					case 13:
						if (A_2 == null)
						{
							num = 11;
							continue;
						}
						num = 10;
						continue;
					case 14:
						goto IL_18C;
					case 15:
						goto IL_1EB;
					case 16:
						return array;
					case 17:
						goto IL_16D;
					}
					if (A_0 == null)
					{
						num = 12;
						continue;
					}
					num = 13;
					continue;
					IL_16D:
					num = 1;
					continue;
					IL_18C:
					num = 8;
					continue;
				}
				IL_1ED:
				long position = this.ᜆ(list[num4]);
				A_0.Position = position;
				A_0.Read(array, num5, num3);
				num4++;
				num5 += num3;
				num = 14;
			}
			IL_9C:
			throw new ArgumentNullException(ClipboardData.b("ᩨὪὬ੮ၰṲ", a_));
			IL_FE:
			goto IL_22F;
			IL_16B:
			throw new ArgumentNullException(ClipboardData.b("ཨɪŬ੮", a_));
			IL_1EB:
			return null;
			IL_22F:
			throw new ApplicationException();
		}
		}
	}

	// Token: 0x06002E19 RID: 11801 RVA: 0x002BED2C File Offset: 0x002BDD2C
	internal int ᜂ(int A_0)
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
		return this.ᜀ[A_0];
	}

	// Token: 0x06002E1A RID: 11802 RVA: 0x002BED74 File Offset: 0x002BDD74
	internal void ᜅ(int A_0)
	{
		for (;;)
		{
			int num = this.ᜀ[A_0];
			this.ᜀ[A_0] = -2;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_B6;
				case 1:
					if (num == -2)
					{
						num2 = 3;
						continue;
					}
					A_0 = num;
					num = this.ᜀ[A_0];
					this.ᜀ[A_0] = -1;
					this.ᜁ.Add(A_0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B6;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 2:
					goto IL_3D;
				case 3:
					return;
				}
				break;
				IL_3D:
				num2 = 1;
				continue;
				IL_B6:
				goto IL_3D;
			}
		}
	}

	// Token: 0x06002E1B RID: 11803 RVA: 0x002BEE3C File Offset: 0x002BDE3C
	internal int ᜀ(int A_0, int A_1)
	{
		if (A_1 > 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int count = this.ᜁ.Count;
				int num = Math.Min(A_1, count);
				int a_ = A_1 - num;
				int result = this.ᜀ(ref A_0, num);
				result = this.ᜁ(ref A_0, a_);
				this.ᜀ[A_0] = -2;
				return result;
			}
			}
		}
		return A_0;
	}

	// Token: 0x06002E1C RID: 11804 RVA: 0x002BEEBC File Offset: 0x002BDEBC
	internal void ᜄ(int A_0)
	{
		int a_ = 12;
		for (;;)
		{
			int count = this.ᜀ.Count;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_73;
				case 1:
					if (A_0 != count - 1)
					{
						num = 0;
						continue;
					}
					goto IL_DB;
				case 2:
					if (A_0 >= 0)
					{
						goto IL_41;
					}
					goto IL_75;
				case 3:
					goto IL_BF;
				case 4:
					if (A_0 >= count)
					{
						num = 3;
						continue;
					}
					num = 1;
					continue;
				case 5:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_41;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				break;
				IL_41:
				num = 5;
			}
		}
		IL_73:
		this.ᜀ[A_0] = -1;
		this.ᜁ.Add(A_0);
		return;
		IL_75:
		throw new ArgumentOutOfRangeException(ClipboardData.b("űᅳᕵ౷ᕹ๻", a_));
		IL_BF:
		goto IL_75;
		IL_DB:
		this.ᜀ.RemoveAt(A_0);
		this.ᜀ();
	}

	// Token: 0x06002E1D RID: 11805 RVA: 0x002BEFB8 File Offset: 0x002BDFB8
	private void ᜀ()
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
		long length = Math.Max(0L, this.ᜃ.Length - (long)this.ᜁ());
		this.ᜃ.SetLength(length);
	}

	// Token: 0x06002E1E RID: 11806 RVA: 0x002BF01C File Offset: 0x002BE01C
	private int ᜁ(ref int A_0, int A_1)
	{
		int num2;
		for (;;)
		{
			IL_30:
			int num;
			int num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8B:
				num = 9;
				break;
			default:
				if (false)
				{
				}
				num2 = A_0;
				num3 = A_0;
				A_0 = this.ᜀ(A_1);
				num = 1;
				break;
			}
			int num4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_11F;
				case 1:
					if (num2 < 0)
					{
						num = 4;
						continue;
					}
					goto IL_F7;
				case 2:
					goto IL_F5;
				case 3:
					if (num3 >= 0)
					{
						num = 5;
						continue;
					}
					goto IL_74;
				case 4:
					num2 = A_0;
					if (true)
					{
					}
					num = 8;
					continue;
				case 5:
					this.ᜀ[num3] = A_0;
					num = 0;
					continue;
				case 6:
					goto IL_DB;
				case 7:
					if (num4 >= A_1)
					{
						num = 2;
						continue;
					}
					this.ᜀ.Add(A_0 + 1);
					num = 3;
					continue;
				case 8:
					goto IL_F7;
				case 9:
					goto IL_DB;
				}
				goto IL_30;
				IL_DB:
				num = 7;
				continue;
				IL_F7:
				num4 = 0;
				num = 6;
			}
			IL_74:
			num3 = A_0;
			num4++;
			A_0++;
			goto IL_8B;
			IL_11F:
			goto IL_74;
		}
		IL_F5:
		A_0--;
		this.ᜀ[A_0] = -2;
		return num2;
	}

	// Token: 0x06002E1F RID: 11807 RVA: 0x002BF164 File Offset: 0x002BE164
	private int ᜀ(int A_0)
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
		long length = this.ᜃ.Length;
		this.ᜃ.SetLength(length + (long)((long)A_0 << (int)this.ᜂ));
		return (int)(length - (long)this.ᜄ >> (int)this.ᜂ);
	}

	// Token: 0x06002E20 RID: 11808 RVA: 0x002BF1D8 File Offset: 0x002BE1D8
	private int ᜀ(ref int A_0, int A_1)
	{
		int result;
		for (;;)
		{
			result = A_0;
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				int num3;
				switch (num2)
				{
				case 0:
					goto IL_A4;
				case 1:
					goto IL_A4;
				case 2:
					if (num >= A_1)
					{
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					num3 = this.ᜁ[num];
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E6;
					default:
						if (false)
						{
						}
						num2 = 6;
						continue;
					}
					break;
				case 3:
					goto IL_C6;
				case 4:
					goto IL_41;
				case 5:
					this.ᜀ[A_0] = num3;
					num2 = 4;
					continue;
				case 6:
					if (A_0 >= 0)
					{
						num2 = 5;
						continue;
					}
					result = num3;
					num2 = 7;
					continue;
				case 7:
					goto IL_41;
				}
				break;
				IL_41:
				A_0 = num3;
				num++;
				num2 = 0;
				continue;
				IL_A4:
				num2 = 2;
			}
		}
		IL_C6:
		IL_E6:
		this.ᜁ.RemoveRange(0, A_1);
		return result;
	}

	// Token: 0x06002E21 RID: 11809 RVA: 0x002BF2DC File Offset: 0x002BE2DC
	public void ᜀ(Stream A_0, spr\u1CC8 A_1, spr\u250C A_2)
	{
		int num = 0;
		switch (num)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				IL_2F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_4F;
				default:
					goto IL_4F;
				}
				int num2;
				int num3;
				int a_;
				byte[] array;
				List<int> list;
				ushort a_3;
				int num4;
				for (;;)
				{
					IL_18:
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						if (num2 >= num3)
						{
							num = 0;
							continue;
						}
						a_ = this.ᜀ(a_, array);
						int a_2 = list[num2];
						long offset = spr\u20BF.ᜀ(a_2, a_3);
						A_0.Seek(offset, SeekOrigin.Begin);
						A_0.Write(array, 0, num4);
						num2++;
						num = 3;
						continue;
					}
					case 2:
						goto IL_ED;
					case 3:
						goto IL_ED;
					}
					goto IL_2F;
					IL_ED:
					num = 1;
				}
				IL_4F:
				if (false)
				{
				}
				int count = this.ᜀ.Count;
				num4 = A_2.ᜁ();
				a_3 = A_2.\u170D();
				int num5 = this.ᜁ() / 4;
				int num6 = num5 - 1;
				double num7 = (double)num6 * (double)count - 109.0;
				double num8 = (double)num6 * (double)num6 - 1.0;
				num3 = (int)Math.Ceiling(num7 / num8);
				A_2.ᜁ(num3);
				array = new byte[num4];
				A_1.ᜃ(num3, this);
				this.ᜀ(num3, A_1);
				list = A_1.ᜀ();
				num2 = 0;
				a_ = 0;
				num = 2;
				goto IL_18;
			}
			return;
		}
	}

	// Token: 0x06002E22 RID: 11810 RVA: 0x002BF440 File Offset: 0x002BE440
	private void ᜀ(int A_0, spr\u1CC8 A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int count;
				List<int> list;
				switch (num)
				{
				case 1:
					if (count < A_0)
					{
						num = 5;
						continue;
					}
					return;
				case 2:
					goto IL_C8;
				case 3:
				{
					int num2;
					if (num2 >= A_0)
					{
						num = 6;
						continue;
					}
					int item = this.ᜁ(-3);
					list.Add(item);
					num2++;
					if (true)
					{
					}
					num = 2;
					continue;
				}
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						goto IL_C8;
					}
					break;
				case 5:
				{
					int num2 = count;
					num = 4;
					continue;
				}
				case 6:
					return;
				case 7:
					goto IL_62;
				}
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				list = A_1.ᜀ();
				count = list.Count;
				num = 1;
				continue;
				IL_C8:
				num = 3;
			}
			IL_62:
			throw new ArgumentNullException(ClipboardData.b("੭᥯ᑱ", a_));
		}
		}
	}

	// Token: 0x06002E23 RID: 11811 RVA: 0x002BF564 File Offset: 0x002BE564
	private int ᜀ(int A_0, byte[] A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int count = this.ᜀ.Count;
				int num = A_1.Length;
				int num2 = 0;
				int num3 = 9;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						num3 = 8;
						continue;
					case 1:
						goto IL_E3;
					case 2:
						goto IL_C5;
					case 3:
					{
						byte[] bytes = BitConverter.GetBytes(-1);
						num3 = 4;
						continue;
					}
					case 4:
						goto IL_E3;
					case 5:
						goto IL_76;
					case 6:
					{
						if (num2 >= num)
						{
							num3 = 10;
							continue;
						}
						byte[] bytes;
						Buffer.BlockCopy(bytes, 0, A_1, num2, 4);
						num2 += 4;
						num3 = 1;
						continue;
					}
					case 7:
						if (num2 < num)
						{
							num3 = 0;
							continue;
						}
						goto IL_76;
					case 8:
						if (A_0 >= count)
						{
							num3 = 5;
							continue;
						}
						Buffer.BlockCopy(BitConverter.GetBytes(this.ᜀ[A_0]), 0, A_1, num2, 4);
						num2 += 4;
						A_0++;
						num3 = 2;
						continue;
					case 9:
						goto IL_C5;
					case 10:
						return A_0;
					case 11:
						if (num2 < num)
						{
							num3 = 3;
							continue;
						}
						return A_0;
					}
					break;
					IL_76:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return A_0;
					default:
						if (false)
						{
						}
						num3 = 11;
						continue;
					}
					IL_C5:
					num3 = 7;
					continue;
					IL_E3:
					num3 = 6;
				}
			}
			return A_0;
		}
	}

	// Token: 0x06002E24 RID: 11812 RVA: 0x002BF6E4 File Offset: 0x002BE6E4
	internal int ᜁ(int A_0)
	{
		int num2;
		for (;;)
		{
			int count = this.ᜁ.Count;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int index = count - 1;
						num2 = this.ᜁ[index];
						this.ᜁ.RemoveAt(index);
						this.ᜀ[num2] = A_0;
						num = 2;
						continue;
					}
					case 1:
						return num2;
					case 2:
						return num2;
					case 3:
						if (count > 0)
						{
							num = 0;
							continue;
						}
						num2 = this.ᜂ();
						this.ᜀ.Add(A_0);
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
		return num2;
	}

	// Token: 0x06002E25 RID: 11813 RVA: 0x002BF7B4 File Offset: 0x002BE7B4
	internal int ᜂ()
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
		long length = this.ᜃ.Length;
		int num = this.ᜁ();
		this.ᜃ.SetLength(length + (long)num);
		return (int)(length - (long)this.ᜄ >> (int)this.ᜂ);
	}

	// Token: 0x06002E26 RID: 11814 RVA: 0x002BF828 File Offset: 0x002BE828
	internal void ᜀ(MemoryStream A_0, int A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_F7;
				case 1:
				{
					if (A_1 <= 0)
					{
						num = 6;
						continue;
					}
					int count = this.ᜀ.Count;
					int num2 = (int)Math.Ceiling((double)(count * 4) / (double)A_1);
					byte[] array = new byte[A_1];
					int num3 = A_1 / 4;
					int num4 = 0;
					num = 4;
					continue;
				}
				case 3:
					goto IL_6A;
				case 4:
					goto IL_BE;
				case 5:
				{
					int num2;
					int num4;
					if (num4 >= num2)
					{
						num = 0;
						continue;
					}
					int num3;
					int a_2 = num4 * num3;
					byte[] array;
					this.ᜀ(a_2, array);
					A_0.Write(array, 0, A_1);
					num4++;
					num = 7;
					continue;
				}
				case 6:
					goto IL_115;
				case 7:
					goto IL_BE;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
				IL_BE:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 5;
					break;
				}
			}
			IL_6A:
			throw new ArgumentNullException(ClipboardData.b("᭧ṩṫ୭ᅯά", a_));
			IL_F7:
			return;
			IL_115:
			throw new ArgumentOutOfRangeException(ClipboardData.b("᭧ཀྵཫᩭὯq❳ήɷό", a_));
		}
		}
	}

	// Token: 0x06002E27 RID: 11815 RVA: 0x002BF988 File Offset: 0x002BE988
	internal long ᜆ(int A_0)
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
		return spr\u20BF.ᜀ(A_0, this.ᜂ, this.ᜄ);
	}

	// Token: 0x06002E28 RID: 11816 RVA: 0x002BF9D8 File Offset: 0x002BE9D8
	internal int ᜃ(int A_0)
	{
		int num;
		for (;;)
		{
			num = 1;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_52;
				case 1:
					if ((A_0 = this.ᜂ(A_0)) < 0)
					{
						num2 = 2;
						continue;
					}
					num++;
					num2 = 0;
					continue;
				case 2:
					return num;
				case 3:
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
						goto IL_52;
					}
					break;
				}
				break;
				IL_52:
				num2 = 1;
			}
		}
		return num;
	}

	// Token: 0x040026A8 RID: 9896
	private List<int> ᜀ;

	// Token: 0x040026A9 RID: 9897
	private List<int> ᜁ;

	// Token: 0x040026AA RID: 9898
	private ushort ᜂ;

	// Token: 0x040026AB RID: 9899
	private Stream ᜃ;

	// Token: 0x040026AC RID: 9900
	private int ᜄ;
}
