using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200040F RID: 1039
internal class sprប
{
	// Token: 0x06003E74 RID: 15988 RVA: 0x0022A1D4 File Offset: 0x002291D4
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

	// Token: 0x06003E75 RID: 15989 RVA: 0x0022A21C File Offset: 0x0022921C
	public sprប(Stream A_0, ushort A_1, int A_2)
	{
		this.ᜀ = new List<int>();
		this.ᜁ = new List<int>();
		base..ctor();
		this.ᜃ = A_0;
		this.ᜂ = A_1;
		this.ᜄ = A_2;
	}

	// Token: 0x06003E76 RID: 15990 RVA: 0x0022A25C File Offset: 0x0022925C
	public sprប(Stream A_0, ushort A_1, Stream A_2, int A_3)
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

	// Token: 0x06003E77 RID: 15991 RVA: 0x0022A2D0 File Offset: 0x002292D0
	public sprប(spr\u2604 A_0, Stream A_1, spr\u23D5 A_2, spr\u19E8 A_3)
	{
		int a_ = 13;
		this.ᜀ = new List<int>();
		this.ᜁ = new List<int>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("╂ⱄ⭆ⱈ", a_));
		}
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("あㅄ㕆ⱈ⩊⁌", a_));
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

	// Token: 0x06003E78 RID: 15992 RVA: 0x0022A3CC File Offset: 0x002293CC
	public byte[] ᜀ(Stream A_0, int A_1, spr\u2604 A_2)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				byte[] array;
				List<int> list;
				int num3;
				int num4;
				int num5;
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 >= 0)
					{
						num = 8;
						continue;
					}
					goto IL_228;
				}
				case 1:
					return array;
				case 2:
					goto IL_CD;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E6;
					default:
						if (false)
						{
						}
						goto IL_185;
					}
					break;
				case 5:
					if (A_2 == null)
					{
						num = 9;
						continue;
					}
					num = 14;
					continue;
				case 6:
				{
					if (true)
					{
					}
					int count = list.Count;
					array = new byte[count << (int)this.ᜂ];
					num3 = 1 << (int)this.ᜂ;
					num4 = 0;
					num5 = 0;
					num = 3;
					continue;
				}
				case 7:
					goto IL_1E4;
				case 8:
					num = 15;
					continue;
				case 9:
					goto IL_15E;
				case 10:
					goto IL_185;
				case 11:
					goto IL_80;
				case 12:
				{
					int count;
					if (num4 >= count)
					{
						num = 1;
						continue;
					}
					goto IL_1E6;
				}
				case 13:
				{
					int num2;
					if (num2 == -2)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
				}
				case 14:
				{
					if (A_1 < 0)
					{
						num = 7;
						continue;
					}
					list = new List<int>();
					A_2.ᜅ();
					int num2 = A_1;
					num = 16;
					continue;
				}
				case 15:
				{
					int num2;
					if (num2 >= this.ᜀ.Count)
					{
						num = 2;
						continue;
					}
					list.Add(num2);
					num2 = this.ᜀ[num2];
					num = 17;
					continue;
				}
				case 16:
					goto IL_163;
				case 17:
					goto IL_163;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 5;
				continue;
				IL_163:
				num = 13;
				continue;
				IL_185:
				num = 12;
				continue;
				IL_1E6:
				long position = this.ᜆ(list[num4]);
				A_0.Position = position;
				A_0.Read(array, num5, num3);
				num4++;
				num5 += num3;
				num = 10;
			}
			IL_80:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋⭍ㅏ㽑", a_));
			IL_CD:
			goto IL_228;
			IL_15E:
			throw new ArgumentNullException(RecordTableEnumerator.b("⹇⍉⁋⭍", a_));
			IL_1E4:
			return null;
			IL_228:
			throw new ApplicationException();
		}
		}
	}

	// Token: 0x06003E79 RID: 15993 RVA: 0x0022A648 File Offset: 0x00229648
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

	// Token: 0x06003E7A RID: 15994 RVA: 0x0022A690 File Offset: 0x00229690
	internal void ᜅ(int A_0)
	{
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			int num = this.ᜀ[A_0];
			this.ᜀ[A_0] = -2;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_63;
				case 1:
					goto IL_63;
				case 2:
					if (num == -2)
					{
						num2 = 3;
						continue;
					}
					A_0 = num;
					num = this.ᜀ[A_0];
					this.ᜀ[A_0] = -1;
					this.ᜁ.Add(A_0);
					if (true)
					{
					}
					num2 = 0;
					continue;
				case 3:
					return;
				}
				break;
				IL_63:
				num2 = 2;
			}
		}
	}

	// Token: 0x06003E7B RID: 15995 RVA: 0x0022A758 File Offset: 0x00229758
	internal int ᜀ(int A_0, int A_1)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_36;
		}
		if (false)
		{
		}
		if (A_1 <= 0)
		{
			if (true)
			{
			}
			return A_0;
		}
		IL_36:
		int count = this.ᜁ.Count;
		int num = Math.Min(A_1, count);
		int a_ = A_1 - num;
		int result = this.ᜀ(ref A_0, num);
		result = this.ᜁ(ref A_0, a_);
		this.ᜀ[A_0] = -2;
		return result;
	}

	// Token: 0x06003E7C RID: 15996 RVA: 0x0022A7D8 File Offset: 0x002297D8
	internal void ᜄ(int A_0)
	{
		int a_ = 8;
		for (;;)
		{
			int count = this.ᜀ.Count;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 != count - 1)
					{
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3D;
					default:
						goto IL_D2;
					}
					break;
				case 1:
					goto IL_3D;
				case 2:
					goto IL_69;
				case 3:
					if (A_0 >= count)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				case 4:
					if (true)
					{
					}
					num = 3;
					continue;
				case 5:
					goto IL_A0;
				}
				break;
				IL_3D:
				if (A_0 < 0)
				{
					goto IL_6B;
				}
				num = 4;
			}
		}
		IL_69:
		this.ᜀ[A_0] = -1;
		this.ᜁ.Add(A_0);
		return;
		IL_6B:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䴽┿⅁ぃ⥅㩇", a_));
		IL_A0:
		goto IL_6B;
		IL_D2:
		if (false)
		{
		}
		this.ᜀ.RemoveAt(A_0);
		this.ᜀ();
	}

	// Token: 0x06003E7D RID: 15997 RVA: 0x0022A8D0 File Offset: 0x002298D0
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

	// Token: 0x06003E7E RID: 15998 RVA: 0x0022A934 File Offset: 0x00229934
	private int ᜁ(ref int A_0, int A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_95:
			this.ᜀ.Add(A_0 + 1);
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_56;
		}
		int num2;
		int num4;
		for (;;)
		{
			IL_28:
			int num3;
			switch (num)
			{
			case 0:
				this.ᜀ[num2] = A_0;
				num = 6;
				continue;
			case 1:
				if (num2 >= 0)
				{
					num = 0;
					continue;
				}
				goto IL_7E;
			case 2:
				if (num3 >= A_1)
				{
					num = 7;
					continue;
				}
				goto IL_95;
			case 3:
				goto IL_D8;
			case 4:
				if (num4 < 0)
				{
					num = 5;
					continue;
				}
				goto IL_F4;
			case 5:
				num4 = A_0;
				if (true)
				{
				}
				num = 9;
				continue;
			case 6:
				goto IL_7E;
			case 7:
				goto IL_F2;
			case 8:
				goto IL_D8;
			case 9:
				goto IL_F4;
			}
			goto IL_56;
			IL_7E:
			num2 = A_0;
			num3++;
			A_0++;
			num = 3;
			continue;
			IL_D8:
			num = 2;
			continue;
			IL_F4:
			num3 = 0;
			num = 8;
		}
		IL_F2:
		A_0--;
		this.ᜀ[A_0] = -2;
		return num4;
		IL_56:
		num4 = A_0;
		num2 = A_0;
		A_0 = this.ᜀ(A_1);
		num = 4;
		goto IL_28;
	}

	// Token: 0x06003E7F RID: 15999 RVA: 0x0022AA78 File Offset: 0x00229A78
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

	// Token: 0x06003E80 RID: 16000 RVA: 0x0022AAEC File Offset: 0x00229AEC
	private int ᜀ(ref int A_0, int A_1)
	{
		int result;
		for (;;)
		{
			for (;;)
			{
				result = A_0;
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_37;
					case 1:
						if (num >= A_1)
						{
							if (true)
							{
							}
							num2 = 6;
							continue;
						}
						num3 = this.ᜁ[num];
						num2 = 7;
						continue;
					case 2:
						goto IL_94;
					case 3:
						goto IL_94;
					case 4:
						goto IL_37;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜀ[A_0] = num3;
							num2 = 4;
							continue;
						}
						break;
					case 6:
						goto IL_C0;
					case 7:
						if (A_0 >= 0)
						{
							num2 = 5;
							continue;
						}
						result = num3;
						num2 = 0;
						continue;
					}
					break;
					IL_37:
					A_0 = num3;
					num++;
					num2 = 2;
					continue;
					IL_94:
					num2 = 1;
				}
			}
		}
		IL_C0:
		this.ᜁ.RemoveRange(0, A_1);
		return result;
	}

	// Token: 0x06003E81 RID: 16001 RVA: 0x0022ABE8 File Offset: 0x00229BE8
	public void ᜀ(Stream A_0, spr\u23D5 A_1, spr\u19E8 A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_0E:
				if (true)
				{
				}
				for (;;)
				{
					int count = this.ᜀ.Count;
					int num = A_2.ᜁ();
					ushort a_ = A_2.\u170D();
					int num2 = this.ᜁ() / 4;
					int num3 = num2 - 1;
					double num4 = (double)num3 * (double)count - 109.0;
					double num5 = (double)num3 * (double)num3 - 1.0;
					int num6 = (int)Math.Ceiling(num4 / num5);
					A_2.ᜁ(num6);
					byte[] array = new byte[num];
					A_1.ᜃ(num6, this);
					this.ᜀ(num6, A_1);
					List<int> list = A_1.ᜀ();
					int num7 = 0;
					int a_2 = 0;
					int num8 = 0;
					for (;;)
					{
						switch (num8)
						{
						case 0:
							goto IL_C7;
						case 1:
							goto IL_C7;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_0E;
							default:
							{
								if (false)
								{
								}
								if (num7 >= num6)
								{
									num8 = 3;
									continue;
								}
								a_2 = this.ᜀ(a_2, array);
								int a_3 = list[num7];
								long offset = spr\u2604.ᜀ(a_3, a_);
								A_0.Seek(offset, SeekOrigin.Begin);
								A_0.Write(array, 0, num);
								num7++;
								num8 = 1;
								continue;
							}
							}
							break;
						case 3:
							return;
						}
						break;
						IL_C7:
						num8 = 2;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06003E82 RID: 16002 RVA: 0x0022AD50 File Offset: 0x00229D50
	private void ᜀ(int A_0, spr\u23D5 A_1)
	{
		int a_ = 15;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					int count;
					List<int> list;
					switch (num)
					{
					case 0:
					{
						int num2 = count;
						num = 5;
						continue;
					}
					case 1:
						return;
					case 2:
						goto IL_A2;
					case 3:
						if (count < A_0)
						{
							num = 0;
							continue;
						}
						return;
					case 5:
						goto IL_A2;
					case 6:
						goto IL_58;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
						{
							if (false)
							{
							}
							int num2;
							if (num2 >= A_0)
							{
								num = 1;
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
						}
						break;
					}
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					list = A_1.ᜀ();
					count = list.Count;
					num = 3;
					continue;
					IL_A2:
					num = 7;
				}
				break;
			}
			}
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("⅄⹆⽈", a_));
	}

	// Token: 0x06003E83 RID: 16003 RVA: 0x0022AE74 File Offset: 0x00229E74
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
				int num3 = 7;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						if (num2 >= num)
						{
							num3 = 3;
							continue;
						}
						byte[] bytes;
						Buffer.BlockCopy(bytes, 0, A_1, num2, 4);
						num2 += 4;
						num3 = 6;
						continue;
					}
					case 1:
						goto IL_9C;
					case 2:
						if (num2 < num)
						{
							num3 = 8;
							continue;
						}
						goto IL_76;
					case 3:
						goto IL_150;
					case 4:
						goto IL_BA;
					case 5:
						goto IL_76;
					case 6:
						IL_123:
						goto IL_BA;
					case 7:
						goto IL_9C;
					case 8:
						num3 = 11;
						continue;
					case 9:
						if (num2 < num)
						{
							num3 = 10;
							continue;
						}
						goto IL_150;
					case 10:
					{
						byte[] bytes = BitConverter.GetBytes(-1);
						num3 = 4;
						continue;
					}
					case 11:
						if (A_0 >= count)
						{
							num3 = 5;
							continue;
						}
						Buffer.BlockCopy(BitConverter.GetBytes(this.ᜀ[A_0]), 0, A_1, num2, 4);
						num2 += 4;
						A_0++;
						num3 = 1;
						continue;
					}
					break;
					IL_76:
					if (true)
					{
					}
					num3 = 9;
					continue;
					IL_9C:
					num3 = 2;
					continue;
					IL_BA:
					num3 = 0;
					continue;
					IL_150:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_123;
					default:
						goto IL_166;
					}
				}
			}
			IL_166:
			if (false)
			{
			}
			return A_0;
		}
	}

	// Token: 0x06003E84 RID: 16004 RVA: 0x0022AFF0 File Offset: 0x00229FF0
	internal int ᜁ(int A_0)
	{
		int num2;
		for (;;)
		{
			int count = this.ᜁ.Count;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return num2;
				case 1:
					return num2;
				case 2:
				{
					int index = count - 1;
					num2 = this.ᜁ[index];
					this.ᜁ.RemoveAt(index);
					this.ᜀ[num2] = A_0;
					num = 0;
					continue;
				}
				case 3:
					if (true)
					{
					}
					if (count > 0)
					{
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
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
		return num2;
	}

	// Token: 0x06003E85 RID: 16005 RVA: 0x0022B0C0 File Offset: 0x0022A0C0
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

	// Token: 0x06003E86 RID: 16006 RVA: 0x0022B134 File Offset: 0x0022A134
	internal void ᜀ(MemoryStream A_0, int A_1)
	{
		int a_ = 10;
		for (;;)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_115;
					case 1:
						goto IL_D1;
					case 2:
						goto IL_60;
					case 4:
						goto IL_B4;
					case 5:
						goto IL_B4;
					case 6:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 1;
							continue;
						}
						int num4;
						int a_2 = num2 * num4;
						byte[] array;
						this.ᜀ(a_2, array);
						A_0.Write(array, 0, A_1);
						num2++;
						num = 5;
						continue;
					}
					case 7:
					{
						if (A_1 <= 0)
						{
							num = 0;
							continue;
						}
						int count = this.ᜀ.Count;
						int num3 = (int)Math.Ceiling((double)(count * 4) / (double)A_1);
						byte[] array = new byte[A_1];
						int num4 = A_1 / 4;
						int num2 = 0;
						num = 4;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 2;
						continue;
					}
					num = 7;
					continue;
					IL_B4:
					num = 6;
				}
				IL_D1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_E7;
				}
				break;
			}
			}
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("㌿㙁㙃⍅⥇❉", a_));
		IL_E7:
		if (false)
		{
		}
		return;
		IL_115:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㌿❁❃㉅❇㡉Ὃ❍⩏㝑", a_));
	}

	// Token: 0x06003E87 RID: 16007 RVA: 0x0022B294 File Offset: 0x0022A294
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
		return spr\u2604.ᜀ(A_0, this.ᜂ, this.ᜄ);
	}

	// Token: 0x06003E88 RID: 16008 RVA: 0x0022B2E4 File Offset: 0x0022A2E4
	internal int ᜃ(int A_0)
	{
		int num;
		for (;;)
		{
			IL_34:
			if (true)
			{
			}
			num = 1;
			int num2 = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return num;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						return num;
					case 1:
						goto IL_52;
					case 2:
						if ((A_0 = this.ᜂ(A_0)) < 0)
						{
							num2 = 0;
							continue;
						}
						num++;
						num2 = 3;
						continue;
					case 3:
						goto IL_52;
					}
					goto IL_34;
					IL_52:
					num2 = 2;
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x04001AC0 RID: 6848
	private List<int> ᜀ;

	// Token: 0x04001AC1 RID: 6849
	private List<int> ᜁ;

	// Token: 0x04001AC2 RID: 6850
	private ushort ᜂ;

	// Token: 0x04001AC3 RID: 6851
	private Stream ᜃ;

	// Token: 0x04001AC4 RID: 6852
	private int ᜄ;
}
