using System;
using System.Collections.Generic;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002A6 RID: 678
internal class spr\u223A : IComparable, ICloneable
{
	// Token: 0x060028E6 RID: 10470 RVA: 0x00172664 File Offset: 0x00171664
	public spr\u223A()
	{
	}

	// Token: 0x060028E7 RID: 10471 RVA: 0x00172690 File Offset: 0x00171690
	public spr\u223A(int A_0) : this()
	{
		this.ᜇ = A_0;
	}

	// Token: 0x060028E8 RID: 10472 RVA: 0x001726AC File Offset: 0x001716AC
	public static string ᜀ(spr\u223A A_0)
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
		return A_0.ᜏ();
	}

	// Token: 0x060028E9 RID: 10473 RVA: 0x001726F0 File Offset: 0x001716F0
	public static spr\u223A ᜀ(string A_0)
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
		spr\u223A spr_u223A = new spr\u223A();
		spr_u223A.ᜁ(A_0);
		return spr_u223A;
	}

	// Token: 0x060028EA RID: 10474 RVA: 0x0017273C File Offset: 0x0017173C
	internal string ᜂ()
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
		return this.ᜋ;
	}

	// Token: 0x060028EB RID: 10475 RVA: 0x00172780 File Offset: 0x00171780
	internal void ᜂ(string A_0)
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
		this.ᜋ = A_0;
	}

	// Token: 0x060028EC RID: 10476 RVA: 0x001727C4 File Offset: 0x001717C4
	public string ᜏ()
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
		return this.ᜆ;
	}

	// Token: 0x060028ED RID: 10477 RVA: 0x00172808 File Offset: 0x00171808
	public void ᜁ(string A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x060028EE RID: 10478 RVA: 0x0017284C File Offset: 0x0017184C
	public SortedList<int, int> ᜇ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_50:
			this.ᜅ = new SortedList<int, int>();
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_50;
			case 1:
				if (true)
				{
				}
				break;
			case 2:
				goto IL_6F;
			}
			if (this.ᜅ != null)
			{
				break;
			}
			num = 0;
		}
		IL_6F:
		return this.ᜅ;
	}

	// Token: 0x060028EF RID: 10479 RVA: 0x001728D0 File Offset: 0x001718D0
	internal SortedList<int, int> ᜊ()
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
		return this.ᜅ;
	}

	// Token: 0x060028F0 RID: 10480 RVA: 0x00172914 File Offset: 0x00171914
	public int ᜄ()
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
		return this.ᜇ;
	}

	// Token: 0x060028F1 RID: 10481 RVA: 0x00172958 File Offset: 0x00171958
	public void ᜁ(int A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x060028F2 RID: 10482 RVA: 0x0017299C File Offset: 0x0017199C
	public int ᜆ()
	{
		if (true)
		{
		}
		if (this.ᜅ == null)
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
				return 0;
			}
		}
		return this.ᜅ.Count;
	}

	// Token: 0x060028F3 RID: 10483 RVA: 0x001729F0 File Offset: 0x001719F0
	public void ᜀ(int A_0, int A_1, int A_2)
	{
		int a_ = 17;
		for (;;)
		{
			this.ᜉ = true;
			this.ᜁ();
			int num = 20;
			for (;;)
			{
				int num3;
				int num4;
				int num5;
				int value;
				switch (num)
				{
				case 0:
					if (A_0 >= this.ᜆ.Length)
					{
						num = 10;
						continue;
					}
					num = 8;
					continue;
				case 1:
				{
					int num2 = this.ᜅ[num3];
					num = 13;
					continue;
				}
				case 2:
					num = 18;
					continue;
				case 3:
					num4 = this.ᜇ;
					goto IL_F1;
				case 4:
					if (A_0 > A_1)
					{
						num = 7;
						continue;
					}
					goto IL_294;
				case 5:
					num = 0;
					continue;
				case 6:
					num = 3;
					continue;
				case 7:
					goto IL_28F;
				case 8:
					if (A_1 >= 0)
					{
						num = 21;
						continue;
					}
					goto IL_235;
				case 9:
					if (A_1 < this.ᜆ.Length - 1)
					{
						num = 2;
						continue;
					}
					return;
				case 10:
					goto IL_270;
				case 11:
					goto IL_1A9;
				case 12:
					num4 = this.ᜅ[num5];
					goto IL_F1;
				case 13:
					goto IL_16D;
				case 14:
					if (num5 < 0)
					{
						num = 6;
						continue;
					}
					num = 12;
					continue;
				case 15:
					goto IL_1E9;
				case 16:
					if (num3 >= 0)
					{
						num = 1;
						continue;
					}
					goto IL_16D;
				case 17:
					this.ᜅ[A_1 + 1] = value;
					num = 11;
					continue;
				case 18:
					if (!this.ᜅ.ContainsKey(A_1 + 1))
					{
						num = 17;
						continue;
					}
					return;
				case 19:
					if (A_1 >= this.ᜆ.Length)
					{
						num = 15;
						continue;
					}
					num = 4;
					continue;
				case 20:
					if (A_0 >= 0)
					{
						num = 5;
						continue;
					}
					goto IL_159;
				case 21:
					num = 19;
					continue;
				}
				break;
				IL_F1:
				value = num4;
				this.ᜃ(num3, num5);
				this.ᜅ[A_0] = A_2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_294:
					num3 = this.ᜀ(A_0);
					num5 = this.ᜀ(A_1);
					num = 16;
					continue;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 9;
					continue;
				}
				IL_16D:
				num = 14;
			}
		}
		IL_159:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆ᩈ㽊ⱌ㵎═͒㩔⑖", a_));
		IL_1A9:
		return;
		IL_1E9:
		IL_235:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆ై╊⥌὎㹐⁒", a_));
		IL_270:
		goto IL_159;
		IL_28F:
		throw new ArgumentException(RecordTableEnumerator.b("⹆ᩈ㽊ⱌ㵎═͒㩔⑖祘㡚㱜ㅞའౢᅤ䝦୨๪䵬ͮၰŲቴቶ୸孺ॼ᝾ꖄ첈\udf8eﺐ뮔", a_));
	}

	// Token: 0x060028F4 RID: 10484 RVA: 0x00172CC4 File Offset: 0x00171CC4
	public int ᜆ(int A_0)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int result;
				return result;
			}
			case 1:
			{
				int num2;
				if (num2 >= 0)
				{
					num = 2;
					continue;
				}
				int result;
				return result;
			}
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					int result;
					return result;
				}
				default:
				{
					if (false)
					{
					}
					int num2;
					int result = this.ᜅ[num2];
					num = 0;
					continue;
				}
				}
				break;
			case 3:
				goto IL_46;
			}
			if (this.ᜅ == null)
			{
				if (true)
				{
				}
				num = 3;
			}
			else
			{
				int result = this.ᜇ;
				int num2 = this.ᜀ(A_0);
				num = 1;
			}
		}
		IL_46:
		return this.ᜇ;
	}

	// Token: 0x060028F5 RID: 10485 RVA: 0x00172D7C File Offset: 0x00171D7C
	public int ᜃ(int A_0)
	{
		int a_ = 2;
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
			if (this.ᜅ == null)
			{
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷猹刻娽┿㩁", a_));
			}
			break;
		}
		spr\u223A.ᜀ(this.ᜅ.Count, A_0);
		return this.ᜅ.Values[A_0];
	}

	// Token: 0x060028F6 RID: 10486 RVA: 0x00172E00 File Offset: 0x00171E00
	public int ᜄ(int A_0)
	{
		int a_ = 12;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 2:
				goto IL_8B;
			case 3:
				goto IL_9A;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8B:
				if (A_0 >= 0)
				{
					goto IL_9C;
				}
				num = 3;
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				if (this.ᜅ.Count <= A_0)
				{
					goto IL_6F;
				}
				num = 0;
				break;
			}
		}
		IL_6F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁ൃ⡅ⱇ⽉㑋", a_));
		IL_9A:
		goto IL_6F;
		IL_9C:
		return this.ᜅ.Keys[A_0];
	}

	// Token: 0x060028F7 RID: 10487 RVA: 0x00172EBC File Offset: 0x00171EBC
	public void ᜂ(int A_0, int A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜁ();
		int key = this.ᜅ.Keys[A_0];
		this.ᜅ[key] = A_1;
	}

	// Token: 0x060028F8 RID: 10488 RVA: 0x00172F1C File Offset: 0x00171F1C
	public void ᜉ()
	{
		for (;;)
		{
			IL_00:
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
						goto IL_00;
					default:
						if (false)
						{
						}
						this.ᜅ.Clear();
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					return;
				}
				if (this.ᜅ == null)
				{
					return;
				}
				num = 0;
			}
		}
	}

	// Token: 0x060028F9 RID: 10489 RVA: 0x00172F9C File Offset: 0x00171F9C
	public int ᜁ(object A_0)
	{
		spr\u223A spr_u223A;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			for (;;)
			{
				int num = 0;
				spr_u223A = (A_0 as spr\u223A);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num = string.CompareOrdinal(spr_u223A.ᜆ, this.ᜆ);
						num2 = 7;
						continue;
					case 1:
						if (this.ᜆ() == 0)
						{
							num2 = 6;
							continue;
						}
						goto IL_C0;
					case 2:
						if (spr_u223A != null)
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						return num;
					case 3:
						return 0;
					case 4:
						num2 = 1;
						continue;
					case 5:
						if (spr_u223A.ᜆ() == 0)
						{
							num2 = 3;
							continue;
						}
						goto IL_C0;
					case 6:
						num2 = 5;
						continue;
					case 7:
						if (num == 0)
						{
							num2 = 4;
							continue;
						}
						return num;
					}
					break;
				}
			}
			return 0;
		}
		IL_C0:
		this.ᜈ();
		spr_u223A.ᜈ();
		return spr\u223A.ᜀ(this.ᜅ, spr_u223A.ᜅ);
	}

	// Token: 0x060028FA RID: 10490 RVA: 0x001730AC File Offset: 0x001720AC
	public static int ᜀ(SortedList<int, int> A_0, SortedList<int, int> A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_145;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return 1;
					default:
					{
						if (false)
						{
						}
						if (num2 != 0)
						{
							num = 5;
							continue;
						}
						int num3;
						num3++;
						num = 4;
						continue;
					}
					}
					break;
				case 2:
					num = 13;
					continue;
				case 3:
					return -1;
				case 4:
					goto IL_145;
				case 5:
					return num2;
				case 7:
					return 0;
				case 8:
				{
					if (num2 != 0)
					{
						num = 12;
						continue;
					}
					int num3;
					IList<int> values;
					IList<int> values2;
					num2 = values[num3] - values2[num3];
					num = 1;
					continue;
				}
				case 9:
				{
					if (A_1 == null)
					{
						num = 14;
						continue;
					}
					int num4 = Math.Min(A_0.Count, A_1.Count);
					IList<int> keys = A_0.Keys;
					IList<int> keys2 = A_1.Keys;
					IList<int> values = A_0.Values;
					IList<int> values2 = A_1.Values;
					int num3 = 0;
					num = 0;
					continue;
				}
				case 10:
				{
					int num3;
					int num4;
					if (num3 >= num4)
					{
						num = 11;
						continue;
					}
					IList<int> keys;
					IList<int> keys2;
					num2 = keys[num3] - keys2[num3];
					num = 8;
					continue;
				}
				case 11:
					goto IL_162;
				case 12:
					return num2;
				case 13:
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					goto IL_102;
				case 14:
					return 1;
				case 15:
					if (A_0 == null)
					{
						num = 3;
						continue;
					}
					num = 9;
					continue;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				IL_102:
				num = 15;
				continue;
				IL_145:
				num = 10;
			}
			return num2;
			IL_162:
			if (true)
			{
			}
			return A_0.Count - A_1.Count;
		}
		}
	}

	// Token: 0x060028FB RID: 10491 RVA: 0x001732B0 File Offset: 0x001722B0
	private void ᜁ()
	{
		for (;;)
		{
			IL_00:
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						this.ᜅ = new SortedList<int, int>();
						num = 0;
						continue;
					}
					break;
				}
				if (this.ᜅ != null)
				{
					return;
				}
				num = 1;
			}
		}
	}

	// Token: 0x060028FC RID: 10492 RVA: 0x00173330 File Offset: 0x00172330
	private int ᜀ(int A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = this.ᜆ() - 1;
				int num3 = 6;
				for (;;)
				{
					int num5;
					IList<int> keys;
					int num6;
					switch (num3)
					{
					case 0:
					{
						int num4;
						return num4;
					}
					case 1:
						return num5;
					case 2:
						goto IL_BE;
					case 3:
						if (num5 == A_0)
						{
							num3 = 4;
							continue;
						}
						num3 = 8;
						continue;
					case 4:
						return num5;
					case 5:
						goto IL_BE;
					case 6:
						if (num2 >= 0)
						{
							keys = this.ᜅ.Keys;
							num3 = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9B;
						default:
							if (false)
							{
							}
							num3 = 7;
							continue;
						}
						break;
					case 7:
						return -1;
					case 8:
						if (num5 < A_0)
						{
							num3 = 9;
							continue;
						}
						num2 = Math.Max(num, num6);
						num3 = 2;
						continue;
					case 9:
						if (true)
						{
						}
						num = Math.Min(num2, num6);
						num3 = 12;
						continue;
					case 10:
						if (num >= num2 - 1)
						{
							num3 = 13;
							continue;
						}
						goto IL_9B;
					case 11:
					{
						int num4;
						if (num4 <= A_0)
						{
							num3 = 0;
							continue;
						}
						num3 = 14;
						continue;
					}
					case 12:
						goto IL_BE;
					case 13:
					{
						int num4 = keys[num2];
						num3 = 11;
						continue;
					}
					case 14:
						if (num5 <= A_0)
						{
							num3 = 1;
							continue;
						}
						return -1;
					}
					break;
					IL_9B:
					num3 = 3;
					continue;
					IL_BE:
					num6 = (num + num2) / 2;
					num5 = keys[num6];
					num3 = 10;
				}
			}
			return -1;
		}
	}

	// Token: 0x060028FD RID: 10493 RVA: 0x00173500 File Offset: 0x00172500
	internal void ᜃ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int count = this.ᜅ.Count;
				int num = 7;
				for (;;)
				{
					int num2;
					IList<int> keys;
					int num3;
					int num4;
					int num5;
					int num6;
					int num7;
					switch (num)
					{
					case 0:
						goto IL_16C;
					case 1:
						goto IL_16C;
					case 2:
						goto IL_1BA;
					case 3:
						if (A_1 != -1)
						{
							num = 13;
							continue;
						}
						num = 16;
						continue;
					case 4:
						num = 12;
						continue;
					case 5:
					{
						int[] array;
						num2 = Array.BinarySearch<int>(array, A_1);
						goto IL_13F;
					}
					case 6:
						goto IL_9E;
					case 7:
						if (count != 0)
						{
							keys = this.ᜅ.Keys;
							int[] array = new int[count];
							keys.CopyTo(array, 0);
							num = 10;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A3;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 8:
						return;
					case 9:
						if (num3 == A_0)
						{
							num = 15;
							continue;
						}
						goto IL_1BA;
					case 10:
						if (A_0 != -1)
						{
							num = 4;
							continue;
						}
						num = 14;
						continue;
					case 11:
						if (num4 > num5)
						{
							num = 8;
							continue;
						}
						this.ᜅ.RemoveAt(num6);
						num4++;
						num = 1;
						continue;
					case 12:
					{
						int[] array;
						num7 = Array.BinarySearch<int>(array, A_0);
						goto IL_18F;
					}
					case 13:
						num = 5;
						continue;
					case 14:
						num7 = 0;
						goto IL_18F;
					case 15:
						goto IL_A3;
					case 16:
					{
						int[] array;
						num2 = array.Length - 1;
						goto IL_13F;
					}
					}
					break;
					IL_A3:
					num6++;
					num = 2;
					continue;
					IL_13F:
					num5 = num2;
					num3 = keys[num6];
					num = 9;
					continue;
					IL_16C:
					num = 11;
					continue;
					IL_18F:
					num6 = num7;
					num = 3;
					continue;
					IL_1BA:
					num4 = num6;
					num = 0;
				}
			}
			IL_9E:
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x060028FE RID: 10494 RVA: 0x0017370C File Offset: 0x0017270C
	public void ᜈ()
	{
		int num = 6;
		for (;;)
		{
			if (true)
			{
			}
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				goto IL_5D;
			case 1:
				goto IL_A0;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5D;
				default:
					goto IL_D4;
				}
				break;
			case 3:
				goto IL_DC;
			case 4:
				goto IL_DC;
			case 5:
			{
				if (!this.ᜉ)
				{
					num = 2;
					continue;
				}
				num2 = this.ᜅ.Count;
				IList<int> values = this.ᜅ.Values;
				num3 = 0;
				num = 9;
				continue;
			}
			case 7:
				if (num3 >= num2 - 1)
				{
					num = 11;
					continue;
				}
				num = 10;
				continue;
			case 8:
				this.ᜉ = false;
				num = 1;
				continue;
			case 9:
				goto IL_DC;
			case 10:
			{
				IList<int> values;
				if (values[num3] == values[num3 + 1])
				{
					num = 0;
					continue;
				}
				num3++;
				num = 3;
				continue;
			}
			case 11:
				goto IL_F8;
			}
			if (this.ᜅ == null)
			{
				num = 8;
				continue;
			}
			goto IL_A0;
			IL_5D:
			this.ᜅ.RemoveAt(num3 + 1);
			num2--;
			num = 4;
			continue;
			IL_A0:
			num = 5;
			continue;
			IL_DC:
			num = 7;
		}
		IL_D4:
		if (false)
		{
		}
		return;
		IL_F8:
		this.ᜉ = false;
	}

	// Token: 0x060028FF RID: 10495 RVA: 0x00173874 File Offset: 0x00172874
	public static void ᜀ(int A_0, int A_1)
	{
		int a_ = 14;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8D;
			case 1:
				goto IL_79;
			case 2:
				num = 1;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_79:
				if (true)
				{
				}
				if (A_1 <= A_0)
				{
					return;
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				if (A_1 < 0)
				{
					goto IL_5D;
				}
				num = 2;
				break;
			}
		}
		IL_5D:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⵃॅ⹇ⱉ㽋⭍⑏", a_));
		IL_8D:
		goto IL_5D;
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x00173910 File Offset: 0x00172910
	public void ᜁ(spr\u223A A_0)
	{
		int a_ = 12;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_176;
					case 1:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 5;
							continue;
						}
						IList<int> keys;
						IList<int> values;
						A_0.ᜅ.Add(keys[num2], values[num2]);
						num2++;
						num = 3;
						continue;
					}
					case 2:
						goto IL_F1;
					case 3:
						goto IL_F1;
					case 4:
						if (this.ᜅ != null)
						{
							num = 8;
							continue;
						}
						goto IL_BD;
					case 5:
						return;
					case 6:
						goto IL_79;
					case 8:
						num = 9;
						continue;
					case 9:
					{
						if (this.ᜅ.Count == 0)
						{
							num = 0;
							continue;
						}
						A_0.ᜅ = new SortedList<int, int>();
						IList<int> keys = this.ᜅ.Keys;
						IList<int> values = this.ᜅ.Values;
						int num2 = 0;
						int count = this.ᜅ.Count;
						num = 2;
						continue;
					}
					}
					if (A_0 != null)
					{
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					IL_F1:
					num = 1;
				}
				break;
			}
			}
		}
		IL_79:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙁㍃⽅♇", a_));
		IL_BD:
		A_0.ᜅ = null;
		return;
		IL_176:
		if (true)
		{
		}
		goto IL_BD;
	}

	// Token: 0x06002901 RID: 10497 RVA: 0x00173AA0 File Offset: 0x00172AA0
	public virtual string ᜎ()
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				stringBuilder = new StringBuilder(this.ᜆ + Environment.NewLine);
				stringBuilder.Append(RecordTableEnumerator.b("あㅄ㕆ቈ歊経潎౐獒答祖睘筚灜罞", a_) + this.ᜇ);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9F;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15B;
						default:
						{
							if (false)
							{
							}
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 4;
								continue;
							}
							IList<int> keys;
							int num3 = keys[num2];
							stringBuilder.AppendFormat(RecordTableEnumerator.b("䥂㙄㍆㭈၊浌㑎慐⹒畔੖祘畚獜煞䅠乢䕤ᱦ塨ᙪ", a_), num3, this.ᜅ[num3]);
							num2++;
							num = 5;
							continue;
						}
						}
						break;
					case 2:
					{
						IList<int> keys = this.ᜅ.Keys;
						int num2 = 0;
						int count = this.ᜅ.Count;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					case 3:
						if (this.ᜅ != null)
						{
							num = 2;
							continue;
						}
						goto IL_15B;
					case 4:
						goto IL_D7;
					case 5:
						goto IL_9F;
					}
					break;
					IL_9F:
					num = 1;
				}
			}
			IL_D7:
			IL_15B:
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x06002902 RID: 10498 RVA: 0x00173C10 File Offset: 0x00172C10
	public virtual bool ᜀ(object A_0)
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
			if (!(A_0 is spr\u223A))
			{
				return false;
			}
			break;
		}
		return this.ᜁ(A_0) == 0;
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x00173C64 File Offset: 0x00172C64
	public virtual int ᜅ()
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
		return this.ᜆ.GetHashCode();
	}

	// Token: 0x06002904 RID: 10500 RVA: 0x00173CAC File Offset: 0x00172CAC
	public virtual int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 6;
			int num2;
			for (;;)
			{
				int a_2;
				ushort a_3;
				bool a_4;
				switch (num)
				{
				case 0:
				{
					if (A_1 > A_0.Length)
					{
						num = 9;
						continue;
					}
					num2 = A_1;
					spr\u223A.ᜀ(a_2, num2 + 2);
					a_3 = BitConverter.ToUInt16(A_0, num2);
					num2 += 2;
					spr\u223A.ᜀ(a_2, num2 + 1);
					this.ᜈ = (spr\u223A.StringType)A_0[num2];
					num2++;
					a_4 = ((byte)(this.ᜈ & spr\u223A.StringType.Unicode) != 0);
					bool flag = (byte)(this.ᜈ & spr\u223A.StringType.FarEast) != 0;
					bool flag2 = (byte)(this.ᜈ & spr\u223A.StringType.RichText) != 0;
					int num3 = 0;
					int num4 = 0;
					num = 3;
					continue;
				}
				case 1:
					num = 0;
					continue;
				case 2:
					goto IL_106;
				case 3:
				{
					bool flag2;
					if (flag2)
					{
						num = 16;
						continue;
					}
					goto IL_BF;
				}
				case 4:
					goto IL_1B0;
				case 5:
					goto IL_80;
				case 7:
				{
					int num3;
					if (num3 > 0)
					{
						num = 13;
						continue;
					}
					goto IL_106;
				}
				case 8:
					goto IL_BF;
				case 9:
					goto IL_101;
				case 10:
				{
					spr\u223A.ᜀ(a_2, num2 + 4);
					int num4 = BitConverter.ToInt32(A_0, num2);
					num2 += 4;
					num = 12;
					continue;
				}
				case 11:
				{
					int num4;
					if (num4 > 0)
					{
						num = 15;
						continue;
					}
					goto IL_2AD;
				}
				case 12:
					goto IL_85;
				case 13:
				{
					int num3;
					this.ᜁ(A_0, num2, num3);
					num = 2;
					continue;
				}
				case 14:
					if (A_1 >= 0)
					{
						num = 1;
						continue;
					}
					goto IL_299;
				case 15:
				{
					int num4;
					this.ᜀ(A_0, num2, num4);
					num = 4;
					continue;
				}
				case 16:
				{
					if (true)
					{
					}
					spr\u223A.ᜀ(a_2, num2 + 2);
					int num3 = (int)BitConverter.ToUInt16(A_0, num2);
					num2 += 2;
					num = 8;
					continue;
				}
				case 17:
				{
					bool flag;
					if (flag)
					{
						num = 10;
						continue;
					}
					goto IL_85;
				}
				}
				goto IL_71;
				IL_77:
				num = 5;
				continue;
				IL_71:
				if (A_0 == null)
				{
					goto IL_77;
				}
				a_2 = A_0.Length;
				spr\u223A.ᜀ(a_2, A_1);
				num = 14;
				continue;
				IL_85:
				this.ᜁ(this.ᜀ(A_0, a_3, a_4, ref num2));
				num = 7;
				continue;
				IL_BF:
				num = 17;
				continue;
				IL_106:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_77;
				default:
					if (false)
					{
					}
					num = 11;
					break;
				}
			}
			IL_80:
			throw new ArgumentNullException(RecordTableEnumerator.b("␿⍁ぃ❅", a_));
			IL_101:
			goto IL_299;
			IL_1B0:
			goto IL_2AD;
			IL_299:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ു≃⁅㭇⽉㡋", a_));
			IL_2AD:
			return num2 - A_1;
		}
		}
	}

	// Token: 0x06002905 RID: 10501 RVA: 0x00173F6C File Offset: 0x00172F6C
	public int ᜋ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		int byteCount = Encoding.Unicode.GetByteCount(this.ᜆ);
		return byteCount + 3;
	}

	// Token: 0x06002906 RID: 10502 RVA: 0x00173FBC File Offset: 0x00172FBC
	public int ᜌ()
	{
		int num;
		for (;;)
		{
			IL_00:
			if (true)
			{
			}
			for (;;)
			{
				this.ᜈ();
				num = this.ᜆ();
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num > 0)
						{
							num2 = 1;
							continue;
						}
						goto IL_7E;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							this.ᜈ |= spr\u223A.StringType.RichText;
							num2 = 2;
							continue;
						}
						break;
					case 2:
						goto IL_7C;
					}
					break;
				}
			}
		}
		IL_7C:
		IL_7E:
		return num * 4;
	}

	// Token: 0x06002907 RID: 10503 RVA: 0x0017404C File Offset: 0x0017304C
	public byte[] ᜃ()
	{
		byte[] array;
		for (;;)
		{
			array = this.ᜀ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (array != null)
					{
						num = 3;
						continue;
					}
					return array;
				case 1:
					this.ᜈ |= spr\u223A.StringType.RichText;
					num = 2;
					continue;
				case 2:
					return array;
				case 3:
					num = 4;
					continue;
				case 4:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return array;
					default:
						if (false)
						{
						}
						if (array.Length > 0)
						{
							num = 1;
							continue;
						}
						return array;
					}
					break;
				}
				break;
			}
		}
		return array;
	}

	// Token: 0x06002908 RID: 10504 RVA: 0x001740F4 File Offset: 0x001730F4
	public int ᜁ(byte[] A_0, int A_1, bool A_2)
	{
		int num;
		for (;;)
		{
			IL_38:
			num = this.ᜀ(A_0, A_1, A_2);
			int num2 = 2;
			for (;;)
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
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						return num;
					case 1:
						goto IL_58;
					case 2:
						if (num > 0)
						{
							num2 = 1;
							continue;
						}
						return num;
					}
					goto IL_38;
				}
				IL_58:
				this.ᜈ |= spr\u223A.StringType.RichText;
				num2 = 0;
			}
		}
		return num;
	}

	// Token: 0x06002909 RID: 10505 RVA: 0x00174180 File Offset: 0x00173180
	public spr\u223A.StringType ᜑ()
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
		return this.ᜈ;
	}

	// Token: 0x0600290A RID: 10506 RVA: 0x001741C4 File Offset: 0x001731C4
	private string ᜀ(byte[] A_0, ushort A_1, bool A_2, ref int A_3)
	{
		int a_ = 2;
		int num = 3;
		int num2;
		string @string;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_67;
			case 1:
				if (A_2)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				num2 = (int)A_1;
				spr\u223A.ᜀ(A_0.Length, A_3 + num2);
				@string = BiffRecordRaw.LatinEncoding.GetString(A_0, A_3, num2);
				num = 2;
				continue;
			case 2:
				goto IL_ED;
			case 4:
				goto IL_3C;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_EF;
				default:
					if (false)
					{
					}
					num2 = (int)(A_1 * 2);
					spr\u223A.ᜀ(A_0.Length, A_3 + num2);
					@string = Encoding.Unicode.GetString(A_0, A_3, num2);
					num = 0;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 4;
			}
			else
			{
				num = 1;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("尷嬹䠻弽", a_));
		IL_67:
		IL_ED:
		IL_EF:
		A_3 += num2;
		return @string;
	}

	// Token: 0x0600290B RID: 10507 RVA: 0x001742C8 File Offset: 0x001732C8
	private void ᜁ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BF;
				case 1:
					goto IL_CB;
				case 2:
					goto IL_6F;
				case 3:
					if (A_2 > 0)
					{
						num = 7;
						continue;
					}
					goto IL_71;
				case 5:
					return;
				case 6:
					if (A_1 >= A_0.Length)
					{
						num = 9;
						continue;
					}
					goto IL_71;
				case 7:
					goto IL_171;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CB;
					default:
						if (false)
						{
						}
						goto IL_BF;
					}
					break;
				case 9:
					num = 3;
					continue;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				num = 6;
				continue;
				IL_71:
				this.ᜁ();
				int a_2 = A_0.Length;
				int num2 = 0;
				num = 0;
				continue;
				IL_BF:
				num = 1;
				continue;
				IL_CB:
				if (num2 >= A_2)
				{
					num = 5;
				}
				else
				{
					spr\u223A.ᜀ(a_2, A_1 + 4);
					int key = (int)BitConverter.ToUInt16(A_0, A_1);
					int value = (int)BitConverter.ToUInt16(A_0, A_1 + 2);
					this.ᜅ[key] = value;
					num2++;
					A_1 += 4;
					num = 8;
				}
			}
			IL_6F:
			throw new ArgumentNullException(RecordTableEnumerator.b("╀≂ㅄ♆", a_));
			IL_171:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀ూ⍄ⅆ㩈⹊㥌", a_));
		}
		}
	}

	// Token: 0x0600290C RID: 10508 RVA: 0x0017444C File Offset: 0x0017344C
	internal void ᜀ(byte[] A_0)
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
					break;
				case 1:
					goto IL_5E;
				default:
					goto IL_5E;
				}
				IL_69:
				num = 1;
				continue;
				IL_5E:
				if (false)
				{
				}
				if (A_0.Length == 0)
				{
					goto IL_69;
				}
				goto IL_73;
			case 1:
				goto IL_71;
			case 3:
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				break;
			}
			num = 3;
		}
		IL_2D:
		if (true)
		{
		}
		return;
		IL_71:
		goto IL_2D;
		IL_73:
		this.ᜁ(A_0, 0, A_0.Length / 4);
	}

	// Token: 0x0600290D RID: 10509 RVA: 0x001744DC File Offset: 0x001734DC
	private void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x0600290E RID: 10510 RVA: 0x00174518 File Offset: 0x00173518
	private byte[] ᜀ()
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
			this.ᜈ();
			int num = this.ᜆ();
			if (num != 0)
			{
				byte[] array = new byte[num * 4];
				this.ᜀ(array, 0, true);
				return array;
			}
			break;
		}
		}
		return null;
	}

	// Token: 0x0600290F RID: 10511 RVA: 0x0017457C File Offset: 0x0017357C
	private int ᜀ(byte[] A_0, int A_1, bool A_2)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				int num4;
				switch (num)
				{
				case 0:
				{
					int num2;
					if (A_1 + num2 > A_0.Length)
					{
						num = 14;
						continue;
					}
					IList<int> keys = this.ᜅ.Keys;
					IList<int> values = this.ᜅ.Values;
					int num3 = 0;
					num = 11;
					continue;
				}
				case 1:
					this.ᜈ();
					num = 6;
					continue;
				case 2:
					if (num4 == 0)
					{
						num = 12;
						continue;
					}
					if (true)
					{
					}
					num = 13;
					continue;
				case 3:
				{
					int num3;
					if (num3 >= num4)
					{
						num = 7;
						continue;
					}
					IList<int> keys;
					byte[] bytes = BitConverter.GetBytes((ushort)keys[num3]);
					A_0[A_1] = bytes[0];
					A_0[A_1 + 1] = bytes[1];
					IList<int> values;
					int num5 = values[num3];
					bytes = BitConverter.GetBytes((ushort)num5);
					A_0[A_1 + 2] = bytes[0];
					A_0[A_1 + 3] = bytes[1];
					num3++;
					A_1 += 4;
					num = 4;
					continue;
				}
				case 4:
					goto IL_171;
				case 6:
					goto IL_AC;
				case 7:
				{
					int num2;
					return num2;
				}
				case 8:
					if (A_1 >= 0)
					{
						num = 9;
						continue;
					}
					goto IL_76;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F5;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 10:
					goto IL_220;
				case 11:
					goto IL_171;
				case 12:
					return 0;
				case 13:
				{
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					int num2 = num4 * 4;
					num = 8;
					continue;
				}
				case 14:
					goto IL_1F5;
				}
				if (A_2)
				{
					num = 1;
					continue;
				}
				IL_AC:
				num4 = this.ᜆ();
				num = 2;
				continue;
				IL_171:
				num = 3;
			}
			IL_76:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ു≃⁅㭇⽉㡋", a_));
			IL_1F5:
			goto IL_76;
			IL_220:
			throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ㙃Ʌⵇ㥉㡋❍㹏㍑⁓㽕㝗㑙", a_));
		}
		}
	}

	// Token: 0x06002910 RID: 10512 RVA: 0x001747B0 File Offset: 0x001737B0
	public object ᜐ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.\u170D();
	}

	// Token: 0x06002911 RID: 10513 RVA: 0x001747F4 File Offset: 0x001737F4
	public spr\u223A \u170D()
	{
		spr\u223A spr_u223A;
		for (;;)
		{
			IL_30:
			spr_u223A = (base.MemberwiseClone() as spr\u223A);
			int num = 1;
			for (;;)
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
					switch (num)
					{
					case 0:
						goto IL_5E;
					case 1:
						if (true)
						{
						}
						if (this.ᜅ != null)
						{
							num = 0;
							continue;
						}
						return spr_u223A;
					case 2:
						return spr_u223A;
					}
					goto IL_30;
				}
				IL_5E:
				this.ᜁ(spr_u223A);
				num = 2;
			}
		}
		return spr_u223A;
	}

	// Token: 0x06002912 RID: 10514 RVA: 0x0017487C File Offset: 0x0017387C
	public spr\u223A ᜁ(Dictionary<int, int> A_0)
	{
		spr\u223A spr_u223A;
		for (;;)
		{
			IL_30:
			spr_u223A = this.\u170D();
			int num = 2;
			for (;;)
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
					switch (num)
					{
					case 0:
						goto IL_54;
					case 1:
						return spr_u223A;
					case 2:
						if (true)
						{
						}
						if (A_0 != null)
						{
							num = 0;
							continue;
						}
						return spr_u223A;
					}
					goto IL_30;
				}
				IL_54:
				spr_u223A.ᜀ(A_0);
				num = 1;
			}
		}
		return spr_u223A;
	}

	// Token: 0x06002913 RID: 10515 RVA: 0x001748FC File Offset: 0x001738FC
	private void ᜀ(Dictionary<int, int> A_0)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				int num4;
				switch (num)
				{
				case 0:
				{
					IList<int> values = this.ᜅ.Values;
					IList<int> keys = this.ᜅ.Keys;
					int num2 = 0;
					int num3 = this.ᜆ();
					num = 1;
					continue;
				}
				case 1:
					goto IL_FD;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_DB;
					}
					break;
				case 3:
					goto IL_FD;
				case 4:
					if (num4 > 0)
					{
						num = 0;
						continue;
					}
					return;
				case 5:
					return;
				case 7:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 5;
						continue;
					}
					IList<int> keys;
					int key = keys[num2];
					IList<int> values;
					int num5 = values[num2];
					num5 = XlsFont.ᜀ(num5, A_0, ExcelParseOptions.Default);
					this.ᜅ[key] = num5;
					num2++;
					num = 3;
					continue;
				}
				}
				IL_49:
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num4 = this.ᜆ();
				num = 4;
				continue;
				goto IL_49;
				IL_FD:
				num = 7;
			}
			IL_DB:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽ؿⵁ⩃㉅Ň⑉⡋⭍⡏㝑❓", a_));
		}
		}
	}

	// Token: 0x06002914 RID: 10516 RVA: 0x00174A5C File Offset: 0x00173A5C
	internal void ᜁ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			SortedList<int, int> sortedList = new SortedList<int, int>();
			IEnumerator<KeyValuePair<int, int>> enumerator = this.ᜅ.GetEnumerator();
			try
			{
				int num = 1;
				for (;;)
				{
					int num2;
					KeyValuePair<int, int> keyValuePair;
					switch (num)
					{
					case 0:
						goto IL_D6;
					case 3:
						if (num2 == A_0)
						{
							if (true)
							{
							}
							num = 7;
							continue;
						}
						goto IL_AE;
					case 4:
						num = 0;
						continue;
					case 5:
						goto IL_AE;
					case 6:
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						keyValuePair = enumerator.Current;
						num2 = keyValuePair.Value;
						num = 3;
						continue;
					case 7:
						num2 = A_1;
						num = 5;
						continue;
					}
					IL_85:
					num = 6;
					continue;
					goto IL_85;
					IL_AE:
					sortedList.Add(keyValuePair.Key, num2);
					num = 2;
				}
				IL_D6:;
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						enumerator.Dispose();
						num = 1;
						continue;
					case 1:
						goto IL_12F;
					case 2:
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
						break;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 0;
				}
				IL_12F:;
			}
			this.ᜅ = sortedList;
			return;
		}
		}
	}

	// Token: 0x06002915 RID: 10517 RVA: 0x00174BBC File Offset: 0x00173BBC
	internal void ᜅ(int A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = this.ᜀ(A_0);
				int num2 = 7;
				for (;;)
				{
					int value;
					IEnumerator<KeyValuePair<int, int>> enumerator;
					SortedList<int, int> sortedList;
					switch (num2)
					{
					case 0:
					{
						value = this.ᜅ[num];
						int num3 = this.ᜅ.IndexOfKey(num);
						int num4 = num3;
						num2 = 6;
						continue;
					}
					case 1:
						goto IL_137;
					case 2:
						goto IL_1A3;
					case 3:
						goto IL_1D9;
					case 4:
						try
						{
							num2 = 1;
							for (;;)
							{
								switch (num2)
								{
								case 2:
									num2 = 3;
									continue;
								case 3:
									goto IL_D6;
								case 4:
								{
									if (!enumerator.MoveNext())
									{
										num2 = 2;
										continue;
									}
									KeyValuePair<int, int> keyValuePair = enumerator.Current;
									sortedList[keyValuePair.Key - A_0] = keyValuePair.Value;
									num2 = 0;
									continue;
								}
								}
								IL_85:
								num2 = 4;
								continue;
								goto IL_85;
							}
							IL_D6:
							goto IL_18F;
						}
						finally
						{
							num2 = 1;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									enumerator.Dispose();
									num2 = 2;
									continue;
								case 1:
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
									break;
								case 2:
									goto IL_134;
								}
								if (enumerator == null)
								{
									break;
								}
								num2 = 0;
							}
							IL_134:;
						}
						goto IL_137;
						IL_18F:
						this.ᜅ = sortedList;
						num2 = 2;
						continue;
					case 5:
					{
						int num4;
						if (num4 < 0)
						{
							num2 = 1;
							continue;
						}
						this.ᜅ.RemoveAt(num4);
						num4--;
						num2 = 3;
						continue;
					}
					case 6:
						goto IL_1D9;
					case 7:
						if (num >= 0)
						{
							num2 = 0;
							continue;
						}
						goto IL_1FA;
					}
					break;
					IL_137:
					if (true)
					{
					}
					this.ᜅ[A_0] = value;
					sortedList = new SortedList<int, int>();
					enumerator = this.ᜅ.GetEnumerator();
					num2 = 4;
					continue;
					IL_1D9:
					num2 = 5;
				}
			}
			IL_1A3:
			IL_1FA:
			this.ᜁ(this.ᜏ().Substring(A_0));
			return;
		}
	}

	// Token: 0x06002916 RID: 10518 RVA: 0x00174DE8 File Offset: 0x00173DE8
	internal void ᜂ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = this.ᜏ().Length - A_0;
				int a_ = num - 1;
				int num2 = this.ᜀ(a_);
				if (true)
				{
				}
				int num3 = 7;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_82;
					case 1:
					{
						int num4;
						int num5;
						if (num4 < num5)
						{
							num3 = 6;
							continue;
						}
						this.ᜅ.RemoveAt(num4);
						num4--;
						num3 = 0;
						continue;
					}
					case 2:
						goto IL_112;
					case 3:
						goto IL_7D;
					case 4:
						goto IL_82;
					case 5:
					{
						int num6 = this.ᜅ[num2];
						int num5 = this.ᜅ.IndexOfKey(num2) + 1;
						int num4 = this.ᜅ.Count - 1;
						num3 = 4;
						continue;
					}
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num3 = 3;
							continue;
						}
						break;
					case 7:
						if (num2 >= 0)
						{
							num3 = 5;
							continue;
						}
						this.ᜉ();
						num3 = 2;
						continue;
					}
					break;
					IL_82:
					num3 = 1;
				}
			}
			IL_7D:
			IL_112:
			this.ᜁ(this.ᜏ().Substring(0, num));
			return;
		}
		}
	}

	// Token: 0x0400138A RID: 5002
	private const byte ᜀ = 1;

	// Token: 0x0400138B RID: 5003
	private const byte ᜁ = 8;

	// Token: 0x0400138C RID: 5004
	internal const int ᜂ = 4;

	// Token: 0x0400138D RID: 5005
	private const byte ᜃ = 1;

	// Token: 0x0400138E RID: 5006
	private const byte ᜄ = 9;

	// Token: 0x0400138F RID: 5007
	private SortedList<int, int> ᜅ;

	// Token: 0x04001390 RID: 5008
	private string ᜆ = string.Empty;

	// Token: 0x04001391 RID: 5009
	private int ᜇ;

	// Token: 0x04001392 RID: 5010
	private spr\u223A.StringType ᜈ = spr\u223A.StringType.Unicode;

	// Token: 0x04001393 RID: 5011
	private bool ᜉ = true;

	// Token: 0x04001394 RID: 5012
	public int ᜊ;

	// Token: 0x04001395 RID: 5013
	private string ᜋ;

	// Token: 0x020002A7 RID: 679
	[Flags]
	public enum StringType : byte
	{
		// Token: 0x04001397 RID: 5015
		NonUnicode = 0,
		// Token: 0x04001398 RID: 5016
		Unicode = 1,
		// Token: 0x04001399 RID: 5017
		FarEast = 4,
		// Token: 0x0400139A RID: 5018
		RichText = 8
	}
}
