using System;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003B6 RID: 950
internal class spr\u1C36 : spr\u2374
{
	// Token: 0x06003A2D RID: 14893 RVA: 0x00209AA0 File Offset: 0x00208AA0
	public spr\u1C36(object[][] A_0, Type[] A_1, OrderBy[] A_2, Color[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x06003A2E RID: 14894 RVA: 0x00209AB8 File Offset: 0x00208AB8
	public new object[][] ᜁ(object[][] A_0, int A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_9E;
			case 2:
				if (this.ᜂ[A_1 - 1] == typeof(string))
				{
					num = 7;
					continue;
				}
				num = 9;
				continue;
			case 3:
				goto IL_1A8;
			case 4:
				if (this.ᜃ[A_1 - 1] == OrderBy.Ascending)
				{
					num = 15;
					continue;
				}
				goto IL_A0;
			case 5:
				if (this.ᜂ[A_1 - 1] == typeof(double))
				{
					num = 8;
					continue;
				}
				num = 2;
				continue;
			case 6:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1D9;
				default:
					if (false)
					{
					}
					num = 14;
					continue;
				}
				break;
			case 7:
				num = 13;
				continue;
			case 8:
				num = 12;
				continue;
			case 9:
				if (this.ᜂ[A_1 - 1] == typeof(DateTime))
				{
					num = 6;
					continue;
				}
				goto IL_21B;
			case 10:
				num = 4;
				continue;
			case 11:
				goto IL_EF;
			case 12:
				if (this.ᜃ[A_1 - 1] == OrderBy.Ascending)
				{
					num = 3;
					continue;
				}
				goto IL_10C;
			case 13:
				if (this.ᜃ[A_1 - 1] == OrderBy.Ascending)
				{
					num = 0;
					continue;
				}
				goto IL_76;
			case 14:
				if (this.ᜃ[A_1 - 1] == OrderBy.Ascending)
				{
					num = 11;
					continue;
				}
				goto IL_103;
			case 15:
				goto IL_1D9;
			}
			if (this.ᜂ[A_1 - 1] == typeof(int))
			{
				num = 10;
			}
			else
			{
				num = 5;
			}
		}
		IL_76:
		return this.ᜅ(A_0, A_1);
		IL_9E:
		return this.ᜃ(A_0, A_1);
		IL_A0:
		return this.ᜀ(A_0, A_1);
		IL_EF:
		return this.ᜄ(A_0, A_1);
		IL_103:
		return this.ᜆ(A_0, A_1);
		IL_10C:
		return this.ᜇ(A_0, A_1);
		IL_1A8:
		return this.ᜂ(A_0, A_1);
		IL_1D9:
		return this.ᜈ(A_0, A_1);
		IL_21B:
		return new object[0][];
	}

	// Token: 0x06003A2F RID: 14895 RVA: 0x00209CE8 File Offset: 0x00208CE8
	public new object[][] ᜀ(object[][] A_0, int A_1, int A_2)
	{
		object[][] array;
		for (;;)
		{
			array = new object[A_2 - A_1][];
			int num = 0;
			int num2 = A_1;
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_37;
				case 1:
					if (num2 < A_2)
					{
						array[num] = new object[A_0[0].Length];
						array[num++] = A_0[num2];
						num2++;
						num3 = 0;
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
						num3 = 2;
						continue;
					}
					break;
				case 2:
					return array;
				case 3:
					if (true)
					{
					}
					goto IL_37;
				}
				break;
				IL_37:
				num3 = 1;
			}
		}
		return array;
	}

	// Token: 0x06003A30 RID: 14896 RVA: 0x00209D94 File Offset: 0x00208D94
	public new void ᜀ(object[][] A_0, object[][] A_1, int A_2)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_24;
				case 1:
					goto IL_24;
				case 2:
					return;
				case 3:
					if (num < A_1.Length)
					{
						if (true)
						{
						}
						A_0[A_2++] = A_1[num];
						num++;
						num2 = 0;
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
						num2 = 2;
						continue;
					}
					break;
				}
				break;
				IL_24:
				num2 = 3;
			}
		}
	}

	// Token: 0x06003A31 RID: 14897 RVA: 0x00209E24 File Offset: 0x00208E24
	public object[][] ᜈ(object[][] A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 12;
			for (;;)
			{
				int num2;
				object[][] array;
				object[][] array2;
				object[][] array3;
				int a_;
				int num3;
				int num6;
				switch (num)
				{
				case 0:
					goto IL_1E9;
				case 1:
					goto IL_276;
				case 2:
					return A_0;
				case 3:
					if (A_1 + 1 <= this.ᜁ)
					{
						num = 10;
						continue;
					}
					goto IL_11A;
				case 4:
					if (num2 >= array.Length + array2.Length)
					{
						num = 16;
						continue;
					}
					num = 7;
					continue;
				case 5:
					array3[a_++] = array2[num3];
					num3++;
					num = 17;
					continue;
				case 6:
				{
					int num4;
					int num5;
					if (num4 < num5)
					{
						num = 20;
						continue;
					}
					num = 3;
					continue;
				}
				case 7:
					if (num6 == array.Length)
					{
						num = 5;
						continue;
					}
					num = 14;
					continue;
				case 8:
					array3[a_++] = array[num6];
					num6++;
					num = 1;
					continue;
				case 9:
					goto IL_1E9;
				case 10:
					if (true)
					{
					}
					num = 15;
					continue;
				case 11:
					goto IL_276;
				case 13:
					goto IL_276;
				case 14:
				{
					if (num3 == array2.Length)
					{
						num = 8;
						continue;
					}
					int num4 = (int)array[num6][A_1];
					int num5 = (int)array2[num3][A_1];
					num = 6;
					continue;
				}
				case 15:
				{
					int num4;
					int num5;
					if (num4 == num5)
					{
						num = 18;
						continue;
					}
					goto IL_11A;
				}
				case 16:
					return array3;
				case 17:
					goto IL_276;
				case 18:
				{
					object[][] a_2 = this.ᜁ(new object[][]
					{
						array[num6],
						array2[num3]
					}, ++A_1);
					this.ᜀ(array3, a_2, a_);
					num6++;
					num3++;
					num2++;
					num = 19;
					continue;
				}
				case 19:
					goto IL_1E4;
				case 20:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E4;
					default:
						if (false)
						{
						}
						array3[a_++] = array[num6];
						num6++;
						num = 13;
						continue;
					}
					break;
				}
				if (A_0.Length == 1)
				{
					num = 2;
					continue;
				}
				array3 = new object[A_0.Length][];
				int num7 = A_0.Length / 2;
				a_ = 0;
				array = this.ᜀ(A_0, 0, num7);
				array2 = this.ᜀ(A_0, num7, A_0.Length);
				array = this.ᜈ(array, A_1);
				array2 = this.ᜈ(array2, A_1);
				num6 = 0;
				num3 = 0;
				num2 = 0;
				num = 9;
				continue;
				IL_11A:
				array3[a_++] = array2[num3];
				num3++;
				num = 11;
				continue;
				IL_1E9:
				num = 4;
				continue;
				IL_276:
				num2++;
				num = 0;
				continue;
				IL_1E4:
				goto IL_276;
			}
			return A_0;
		}
		}
	}

	// Token: 0x06003A32 RID: 14898 RVA: 0x0020A13C File Offset: 0x0020913C
	public new object[][] ᜃ(object[][] A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				object[][] array;
				int num2;
				object[][] array2;
				int num3;
				int num4;
				object[][] array3;
				int num5;
				switch (num)
				{
				case 0:
					return array;
				case 1:
				{
					string text;
					string text2;
					if (text.CompareTo(text2) < 0)
					{
						num = 5;
						continue;
					}
					num = 10;
					continue;
				}
				case 2:
					goto IL_28D;
				case 3:
					return A_0;
				case 4:
					goto IL_1ED;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1ED;
					default:
						if (false)
						{
						}
						array[num2++] = array2[num3];
						num3++;
						num = 2;
						continue;
					}
					break;
				case 6:
					goto IL_28D;
				case 7:
				{
					string text;
					string text2;
					if (text == text2)
					{
						num = 18;
						continue;
					}
					goto IL_11F;
				}
				case 8:
					if (num4 >= array2.Length + array3.Length)
					{
						num = 0;
						continue;
					}
					num = 15;
					continue;
				case 9:
					goto IL_28D;
				case 10:
					if (A_1 + 1 <= this.ᜁ)
					{
						num = 14;
						continue;
					}
					goto IL_11F;
				case 11:
					if (true)
					{
					}
					array[num2++] = array3[num5];
					num5++;
					num = 9;
					continue;
				case 12:
					goto IL_1F2;
				case 14:
					num = 7;
					continue;
				case 15:
					if (num3 == array2.Length)
					{
						num = 11;
						continue;
					}
					num = 19;
					continue;
				case 16:
					array[num2++] = array2[num3];
					num3++;
					num = 6;
					continue;
				case 17:
					goto IL_28D;
				case 18:
				{
					object[][] a_ = this.ᜁ(new object[][]
					{
						array2[num3],
						array3[num5]
					}, ++A_1);
					this.ᜀ(array, a_, num2);
					num2 += 2;
					num3++;
					num5++;
					num4++;
					num = 4;
					continue;
				}
				case 19:
				{
					if (num5 == array3.Length)
					{
						num = 16;
						continue;
					}
					string text = (string)array2[num3][A_1];
					string text2 = (string)array3[num5][A_1];
					num = 1;
					continue;
				}
				case 20:
					goto IL_1F2;
				}
				if (A_0.Length == 1)
				{
					num = 3;
					continue;
				}
				array = new object[A_0.Length][];
				num2 = 0;
				int num6 = A_0.Length / 2;
				array2 = this.ᜀ(A_0, 0, num6);
				array3 = this.ᜀ(A_0, num6, A_0.Length);
				array2 = this.ᜃ(array2, A_1);
				array3 = this.ᜃ(array3, A_1);
				num3 = 0;
				num5 = 0;
				num4 = 0;
				num = 12;
				continue;
				IL_11F:
				array[num2++] = array3[num5];
				num5++;
				num = 17;
				continue;
				IL_1F2:
				num = 8;
				continue;
				IL_28D:
				num4++;
				num = 20;
				continue;
				IL_1ED:
				goto IL_28D;
			}
			return A_0;
		}
		}
	}

	// Token: 0x06003A33 RID: 14899 RVA: 0x0020A464 File Offset: 0x00209464
	public new object[][] ᜂ(object[][] A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int num2;
				object[][] array;
				object[][] array2;
				int num3;
				object[][] array3;
				int num4;
				int num5;
				switch (num)
				{
				case 1:
					if (true)
					{
					}
					if (num2 == array.Length)
					{
						num = 11;
						continue;
					}
					num = 13;
					continue;
				case 2:
					goto IL_1F0;
				case 3:
					goto IL_282;
				case 4:
					goto IL_282;
				case 5:
				{
					object[][] a_ = this.ᜁ(new object[][]
					{
						array[num2],
						array2[num3]
					}, A_1 + 1);
					this.ᜀ(array3, a_, num4);
					num4 += 2;
					num2++;
					num3++;
					num5++;
					num = 2;
					continue;
				}
				case 6:
				{
					double num6;
					double num7;
					if (num6 == num7)
					{
						num = 5;
						continue;
					}
					goto IL_125;
				}
				case 7:
				{
					double num6;
					double num7;
					if (num6 < num7)
					{
						num = 9;
						continue;
					}
					num = 20;
					continue;
				}
				case 8:
					return array3;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F0;
					default:
						if (false)
						{
						}
						array3[num4++] = array[num2];
						num2++;
						num = 10;
						continue;
					}
					break;
				case 10:
					goto IL_282;
				case 11:
					array3[num4++] = array2[num3];
					num3++;
					num = 4;
					continue;
				case 12:
					if (num5 >= array.Length + array2.Length)
					{
						num = 8;
						continue;
					}
					num = 1;
					continue;
				case 13:
				{
					if (num3 == array2.Length)
					{
						num = 16;
						continue;
					}
					double num6 = (double)array[num2][A_1];
					double num7 = (double)array2[num3][A_1];
					num = 7;
					continue;
				}
				case 14:
					goto IL_282;
				case 15:
					return A_0;
				case 16:
					array3[num4++] = array[num2];
					num2++;
					num = 3;
					continue;
				case 17:
					goto IL_1F5;
				case 18:
					goto IL_1F5;
				case 19:
					num = 6;
					continue;
				case 20:
					if (A_1 + 1 <= this.ᜁ)
					{
						num = 19;
						continue;
					}
					goto IL_125;
				}
				if (A_0.Length == 1)
				{
					num = 15;
					continue;
				}
				array3 = new object[A_0.Length][];
				num4 = 0;
				int num8 = A_0.Length / 2;
				array = this.ᜀ(A_0, 0, num8);
				array2 = this.ᜀ(A_0, num8, A_0.Length);
				array = this.ᜂ(array, A_1);
				array2 = this.ᜂ(array2, A_1);
				num2 = 0;
				num3 = 0;
				num5 = 0;
				num = 18;
				continue;
				IL_125:
				array3[num4++] = array2[num3];
				num3++;
				num = 14;
				continue;
				IL_1F5:
				num = 12;
				continue;
				IL_282:
				num5++;
				num = 17;
				continue;
				IL_1F0:
				goto IL_282;
			}
			return A_0;
		}
		}
	}

	// Token: 0x06003A34 RID: 14900 RVA: 0x0020A780 File Offset: 0x00209780
	public new object[][] ᜄ(object[][] A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 19;
			for (;;)
			{
				int num2;
				object[][] array;
				object[][] array2;
				int num3;
				object[][] array3;
				int num4;
				int num5;
				DateTime dateTime;
				DateTime dateTime2;
				switch (num)
				{
				case 0:
					if (num2 == array.Length)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
				case 1:
					array2[num3++] = array3[num4];
					num4++;
					num = 8;
					continue;
				case 2:
					if (array3[num4] != null)
					{
						num = 3;
						continue;
					}
					goto IL_27B;
				case 3:
					num = 11;
					continue;
				case 4:
					if (num4 == array3.Length)
					{
						num = 22;
						continue;
					}
					num = 0;
					continue;
				case 5:
					if (A_1 + 1 <= this.ᜁ)
					{
						num = 15;
						continue;
					}
					goto IL_15D;
				case 6:
					if (num5 >= array3.Length + array.Length)
					{
						num = 21;
						continue;
					}
					num = 4;
					continue;
				case 7:
					goto IL_27B;
				case 8:
					goto IL_27B;
				case 9:
					if (dateTime < dateTime2)
					{
						num = 17;
						continue;
					}
					num = 5;
					continue;
				case 10:
					goto IL_27B;
				case 11:
					if (array[num2] != null)
					{
						num = 13;
						continue;
					}
					goto IL_27B;
				case 12:
					goto IL_201;
				case 13:
					dateTime = (DateTime)array3[num4][A_1];
					dateTime2 = (DateTime)array[num2][A_1];
					num = 9;
					continue;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C3;
					default:
						goto IL_AC;
					}
					break;
				case 15:
					num = 20;
					continue;
				case 16:
					goto IL_27B;
				case 17:
					if (true)
					{
					}
					array2[num3++] = array3[num4];
					num4++;
					num = 16;
					continue;
				case 18:
				{
					object[][] a_ = this.ᜁ(new object[][]
					{
						array3[num4],
						array[num2]
					}, A_1 + 1);
					this.ᜀ(array2, a_, num3);
					num3++;
					num4++;
					num2++;
					num5++;
					num = 24;
					continue;
				}
				case 20:
					goto IL_C3;
				case 21:
					return array2;
				case 22:
					array2[num3++] = array[num2];
					num2++;
					num = 10;
					continue;
				case 23:
					goto IL_201;
				case 24:
					goto IL_27B;
				}
				if (A_0.Length == 1)
				{
					num = 14;
					continue;
				}
				array2 = new object[A_0.Length][];
				int num6 = A_0.Length / 2;
				num3 = 0;
				array3 = this.ᜀ(A_0, 0, num6);
				array = this.ᜀ(A_0, num6, A_0.Length);
				array3 = this.ᜄ(array3, A_1);
				array = this.ᜄ(array, A_1);
				num4 = 0;
				num2 = 0;
				num5 = 0;
				num = 12;
				continue;
				IL_C3:
				if (dateTime == dateTime2)
				{
					num = 18;
					continue;
				}
				IL_15D:
				array2[num3++] = array[num2];
				num2++;
				num = 7;
				continue;
				IL_201:
				num = 6;
				continue;
				IL_27B:
				num5++;
				num = 23;
			}
			IL_AC:
			if (false)
			{
			}
			return A_0;
		}
		}
	}

	// Token: 0x06003A35 RID: 14901 RVA: 0x0020AB04 File Offset: 0x00209B04
	public new object[][] ᜀ(object[][] A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				object[][] array;
				int num2;
				object[][] array2;
				int num3;
				object[][] array3;
				int num4;
				int num5;
				switch (num)
				{
				case 0:
					array[num2++] = array2[num3];
					num3++;
					num = 4;
					continue;
				case 1:
					goto IL_1EA;
				case 2:
					goto IL_27F;
				case 3:
					return A_0;
				case 4:
					goto IL_27F;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_257;
					default:
					{
						if (false)
						{
						}
						object[][] a_ = this.ᜁ(new object[][]
						{
							array3[num4],
							array2[num3]
						}, A_1 + 1);
						this.ᜀ(array, a_, num2);
						num2 += 2;
						num4++;
						num3++;
						num5++;
						num = 2;
						continue;
					}
					}
					break;
				case 6:
				{
					if (num3 == array2.Length)
					{
						num = 11;
						continue;
					}
					int num6 = (int)array3[num4][A_1];
					int num7 = (int)array2[num3][A_1];
					if (true)
					{
					}
					num = 16;
					continue;
				}
				case 8:
					goto IL_1EA;
				case 9:
					goto IL_27F;
				case 10:
					return array;
				case 11:
					array[num2++] = array3[num4];
					num4++;
					num = 9;
					continue;
				case 12:
					goto IL_257;
				case 13:
					if (A_1 + 1 <= this.ᜁ)
					{
						num = 14;
						continue;
					}
					goto IL_110;
				case 14:
					num = 19;
					continue;
				case 15:
					if (num5 >= array3.Length + array2.Length)
					{
						num = 10;
						continue;
					}
					num = 18;
					continue;
				case 16:
				{
					int num6;
					int num7;
					if (num6 > num7)
					{
						num = 12;
						continue;
					}
					num = 13;
					continue;
				}
				case 17:
					goto IL_27F;
				case 18:
					if (num4 == array3.Length)
					{
						num = 0;
						continue;
					}
					num = 6;
					continue;
				case 19:
				{
					int num6;
					int num7;
					if (num6 == num7)
					{
						num = 5;
						continue;
					}
					goto IL_110;
				}
				case 20:
					goto IL_27F;
				}
				if (A_0.Length == 1)
				{
					num = 3;
					continue;
				}
				array = new object[A_0.Length][];
				int num8 = A_0.Length / 2;
				num2 = 0;
				array3 = this.ᜀ(A_0, 0, num8);
				array2 = this.ᜀ(A_0, num8, A_0.Length);
				array3 = this.ᜀ(array3, A_1);
				array2 = this.ᜀ(array2, A_1);
				num4 = 0;
				num3 = 0;
				num5 = 0;
				num = 8;
				continue;
				IL_110:
				array[num2++] = array2[num3];
				num3++;
				num = 17;
				continue;
				IL_1EA:
				num = 15;
				continue;
				IL_257:
				array[num2++] = array3[num4];
				num4++;
				num = 20;
				continue;
				IL_27F:
				num5++;
				num = 1;
			}
			return A_0;
		}
		}
	}

	// Token: 0x06003A36 RID: 14902 RVA: 0x0020AE1C File Offset: 0x00209E1C
	public new object[][] ᜅ(object[][] A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				object[][] array;
				int num2;
				object[][] array2;
				int num3;
				object[][] array3;
				int num4;
				int num5;
				switch (num)
				{
				case 0:
					goto IL_290;
				case 1:
					array[num2++] = array2[num3];
					num3++;
					num = 0;
					continue;
				case 2:
					return A_0;
				case 3:
					goto IL_290;
				case 4:
					array[num2++] = array3[num4];
					num4++;
					num = 17;
					continue;
				case 6:
				{
					if (num3 == array2.Length)
					{
						num = 4;
						continue;
					}
					string text = (string)array3[num4][A_1];
					string text2 = (string)array2[num3][A_1];
					num = 18;
					continue;
				}
				case 7:
					return array;
				case 8:
					goto IL_1FD;
				case 9:
					if (A_1 + 1 <= this.ᜁ)
					{
						num = 19;
						continue;
					}
					goto IL_12A;
				case 10:
					if (num4 == array3.Length)
					{
						num = 1;
						continue;
					}
					num = 6;
					continue;
				case 11:
					goto IL_268;
				case 12:
				{
					string text;
					string text2;
					if (text == text2)
					{
						num = 20;
						continue;
					}
					goto IL_12A;
				}
				case 13:
					goto IL_290;
				case 14:
					goto IL_290;
				case 15:
					if (num5 >= array3.Length + array2.Length)
					{
						num = 7;
						continue;
					}
					num = 10;
					continue;
				case 16:
					goto IL_1FD;
				case 17:
					goto IL_290;
				case 18:
				{
					string text;
					string text2;
					if (text.CompareTo(text2) > 0)
					{
						num = 11;
						continue;
					}
					num = 9;
					continue;
				}
				case 19:
					num = 12;
					continue;
				case 20:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_268;
					default:
					{
						if (false)
						{
						}
						object[][] a_ = this.ᜁ(new object[][]
						{
							array3[num4],
							array2[num3]
						}, ++A_1);
						this.ᜀ(array, a_, num2);
						num2 += 2;
						num4++;
						num3++;
						num5++;
						num = 3;
						continue;
					}
					}
					break;
				}
				if (A_0.Length == 1)
				{
					num = 2;
					continue;
				}
				array = new object[A_0.Length][];
				int num6 = A_0.Length / 2;
				num2 = 0;
				array3 = this.ᜀ(A_0, 0, num6);
				array2 = this.ᜀ(A_0, num6, A_0.Length);
				array3 = this.ᜅ(array3, A_1);
				array2 = this.ᜅ(array2, A_1);
				num4 = 0;
				num3 = 0;
				num5 = 0;
				num = 16;
				continue;
				IL_268:
				if (true)
				{
				}
				array[num2++] = array3[num4];
				num4++;
				num = 14;
				continue;
				IL_12A:
				array[num2++] = array2[num3];
				num3++;
				num = 13;
				continue;
				IL_1FD:
				num = 15;
				continue;
				IL_290:
				num5++;
				num = 8;
			}
			return A_0;
		}
		}
	}

	// Token: 0x06003A37 RID: 14903 RVA: 0x0020B148 File Offset: 0x0020A148
	public object[][] ᜇ(object[][] A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 18;
			for (;;)
			{
				int num2;
				object[][] array;
				int num3;
				object[][] array2;
				int num6;
				object[][] array3;
				int num7;
				switch (num)
				{
				case 0:
					goto IL_1EA;
				case 1:
					if (num2 == array.Length)
					{
						num = 9;
						continue;
					}
					num = 2;
					continue;
				case 2:
				{
					if (num3 == array2.Length)
					{
						num = 8;
						continue;
					}
					double num4 = (double)array[num2][A_1];
					double num5 = (double)array2[num3][A_1];
					num = 6;
					continue;
				}
				case 3:
					goto IL_277;
				case 4:
					goto IL_277;
				case 5:
					if (num6 >= array.Length + array2.Length)
					{
						num = 14;
						continue;
					}
					num = 1;
					continue;
				case 6:
				{
					double num4;
					double num5;
					if (num4 > num5)
					{
						num = 10;
						continue;
					}
					num = 15;
					continue;
				}
				case 7:
					goto IL_277;
				case 8:
					array3[num7++] = array[num2];
					num2++;
					num = 11;
					continue;
				case 9:
					array3[num7++] = array2[num3];
					num3++;
					num = 4;
					continue;
				case 10:
					goto IL_24F;
				case 11:
					goto IL_277;
				case 12:
					goto IL_1EA;
				case 13:
					return A_0;
				case 14:
					return array3;
				case 15:
					if (true)
					{
					}
					if (A_1 + 1 <= this.ᜁ)
					{
						num = 16;
						continue;
					}
					goto IL_110;
				case 16:
					num = 17;
					continue;
				case 17:
				{
					double num4;
					double num5;
					if (num4 == num5)
					{
						num = 19;
						continue;
					}
					goto IL_110;
				}
				case 19:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24F;
					default:
					{
						if (false)
						{
						}
						object[][] a_ = this.ᜁ(new object[][]
						{
							array[num2],
							array2[num3]
						}, A_1 + 1);
						this.ᜀ(array3, a_, num7);
						num7 += 2;
						num2++;
						num3++;
						num6++;
						num = 7;
						continue;
					}
					}
					break;
				case 20:
					goto IL_277;
				}
				if (A_0.Length == 1)
				{
					num = 13;
					continue;
				}
				array3 = new object[A_0.Length][];
				int num8 = A_0.Length / 2;
				num7 = 0;
				array = this.ᜀ(A_0, 0, num8);
				array2 = this.ᜀ(A_0, num8, A_0.Length);
				array = this.ᜇ(array, A_1);
				array2 = this.ᜇ(array2, A_1);
				num2 = 0;
				num3 = 0;
				num6 = 0;
				num = 0;
				continue;
				IL_110:
				array3[num7++] = array2[num3];
				num3++;
				num = 3;
				continue;
				IL_1EA:
				num = 5;
				continue;
				IL_24F:
				array3[num7++] = array[num2];
				num2++;
				num = 20;
				continue;
				IL_277:
				num6++;
				num = 12;
			}
			return A_0;
		}
		}
	}

	// Token: 0x06003A38 RID: 14904 RVA: 0x0020B460 File Offset: 0x0020A460
	public new object[][] ᜆ(object[][] A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 22;
			object[][] array;
			for (;;)
			{
				int a_;
				object[][] array2;
				int num2;
				object[][] array3;
				int num3;
				int num4;
				switch (num)
				{
				case 0:
					goto IL_255;
				case 1:
					array[a_++] = array2[num2];
					num2++;
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21D;
					default:
						goto IL_36A;
					}
					break;
				case 3:
					array[a_++] = array3[num3];
					num3++;
					num = 17;
					continue;
				case 4:
					if (num3 == array3.Length)
					{
						num = 1;
						continue;
					}
					num = 9;
					continue;
				case 5:
					array[a_++] = array3[num3];
					num3++;
					num = 18;
					continue;
				case 6:
					goto IL_255;
				case 7:
					if (array3[num3] != null)
					{
						num = 23;
						continue;
					}
					goto IL_255;
				case 8:
					num = 11;
					continue;
				case 9:
					if (num2 == array2.Length)
					{
						num = 3;
						continue;
					}
					num = 7;
					continue;
				case 10:
					goto IL_1DB;
				case 11:
				{
					DateTime dateTime;
					DateTime dateTime2;
					if (dateTime == dateTime2)
					{
						num = 14;
						continue;
					}
					goto IL_137;
				}
				case 12:
					if (num4 >= array3.Length + array2.Length)
					{
						num = 2;
						continue;
					}
					num = 4;
					continue;
				case 13:
					goto IL_1DB;
				case 14:
				{
					object[][] a_2 = this.ᜁ(new object[][]
					{
						array3[num3],
						array2[num2]
					}, A_1 + 1);
					this.ᜀ(array, a_2, a_);
					num3++;
					num2++;
					num4++;
					num = 6;
					continue;
				}
				case 15:
					goto IL_255;
				case 16:
					return A_0;
				case 17:
					goto IL_255;
				case 18:
					goto IL_255;
				case 19:
					if (A_1 + 1 <= this.ᜁ)
					{
						num = 8;
						continue;
					}
					goto IL_137;
				case 20:
				{
					DateTime dateTime = (DateTime)array3[num3][A_1];
					DateTime dateTime2 = (DateTime)array2[num2][A_1];
					num = 24;
					continue;
				}
				case 21:
					if (array2[num2] != null)
					{
						goto IL_21D;
					}
					goto IL_255;
				case 23:
					num = 21;
					continue;
				case 24:
				{
					DateTime dateTime;
					DateTime dateTime2;
					if (dateTime > dateTime2)
					{
						num = 5;
						continue;
					}
					num = 19;
					continue;
				}
				}
				if (true)
				{
				}
				if (A_0.Length == 1)
				{
					num = 16;
					continue;
				}
				array = new object[A_0.Length][];
				int num5 = A_0.Length / 2;
				a_ = 0;
				array3 = this.ᜀ(A_0, 0, num5);
				array2 = this.ᜀ(A_0, num5, A_0.Length);
				array3 = this.ᜆ(array3, A_1);
				array2 = this.ᜆ(array2, A_1);
				num3 = 0;
				num2 = 0;
				num4 = 0;
				num = 10;
				continue;
				IL_137:
				array[a_++] = array2[num2];
				num2++;
				num = 15;
				continue;
				IL_1DB:
				num = 12;
				continue;
				IL_21D:
				num = 20;
				continue;
				IL_255:
				num4++;
				num = 13;
			}
			return A_0;
			IL_36A:
			if (false)
			{
			}
			return array;
		}
		}
	}

	// Token: 0x06003A39 RID: 14905 RVA: 0x0020B7E0 File Offset: 0x0020A7E0
	public override void ᜄ(int A_0, int A_1, int A_2)
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
		this.ᜀ = this.ᜁ(this.ᜀ, A_2);
	}

	// Token: 0x06003A3A RID: 14906 RVA: 0x0020B830 File Offset: 0x0020A830
	public new void ᜉ(int A_0, int A_1, int A_2)
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
		this.ᜀ = this.ᜈ(this.ᜀ, A_2);
	}

	// Token: 0x06003A3B RID: 14907 RVA: 0x0020B880 File Offset: 0x0020A880
	public new void ᜂ(int A_0, int A_1, int A_2)
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
		this.ᜀ = this.ᜂ(this.ᜀ, A_2);
	}

	// Token: 0x06003A3C RID: 14908 RVA: 0x0020B8D0 File Offset: 0x0020A8D0
	public new void ᜅ(int A_0, int A_1, int A_2)
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
		this.ᜀ = this.ᜄ(this.ᜀ, A_2);
	}

	// Token: 0x06003A3D RID: 14909 RVA: 0x0020B920 File Offset: 0x0020A920
	public new void ᜃ(int A_0, int A_1, int A_2)
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
		this.ᜀ = this.ᜃ(this.ᜀ, A_2);
	}

	// Token: 0x06003A3E RID: 14910 RVA: 0x0020B970 File Offset: 0x0020A970
	public new void ᜁ(int A_0, int A_1, int A_2)
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
		this.ᜀ = this.ᜁ(this.ᜀ, A_2);
	}

	// Token: 0x06003A3F RID: 14911 RVA: 0x0020B9C0 File Offset: 0x0020A9C0
	public new void ᜀ(int A_0, int A_1, int A_2)
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
		this.ᜀ = this.ᜀ(this.ᜀ, A_2);
	}

	// Token: 0x06003A40 RID: 14912 RVA: 0x0020BA10 File Offset: 0x0020AA10
	public new void ᜆ(int A_0, int A_1, int A_2)
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
		this.ᜀ = this.ᜇ(this.ᜀ, A_2);
	}

	// Token: 0x06003A41 RID: 14913 RVA: 0x0020BA60 File Offset: 0x0020AA60
	public new void ᜇ(int A_0, int A_1, int A_2)
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
		this.ᜀ = this.ᜆ(this.ᜀ, A_2);
	}

	// Token: 0x06003A42 RID: 14914 RVA: 0x0020BAB0 File Offset: 0x0020AAB0
	public new void ᜈ(int A_0, int A_1, int A_2)
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
		this.ᜀ = this.ᜅ(this.ᜀ, A_2);
	}

	// Token: 0x06003A43 RID: 14915 RVA: 0x0020BB00 File Offset: 0x0020AB00
	public new IXLSRange ᜀ()
	{
		int a_ = 14;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("ᙃ❅♇ⵉ⥋", a_));
	}

	// Token: 0x06003A44 RID: 14916 RVA: 0x0020BB58 File Offset: 0x0020AB58
	public new void ᜀ(IXLSRange A_0)
	{
		int a_ = 7;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("漼帾⽀⑂⁄", a_));
	}
}
