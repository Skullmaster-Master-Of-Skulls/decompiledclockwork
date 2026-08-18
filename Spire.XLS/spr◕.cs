using System;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004F2 RID: 1266
internal class spr\u25D5 : spr\u2374
{
	// Token: 0x06004D65 RID: 19813 RVA: 0x002F2DEC File Offset: 0x002F1DEC
	public spr\u25D5(object[][] A_0, Type[] A_1, OrderBy[] A_2, Color[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x06004D66 RID: 19814 RVA: 0x002F2E04 File Offset: 0x002F1E04
	public new void ᜉ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 11;
			int num2;
			int num6;
			for (;;)
			{
				int num3;
				int num5;
				int num7;
				switch (num)
				{
				case 0:
					goto IL_237;
				case 1:
					if ((int)this.ᜀ[num2][A_2] == num3)
					{
						num = 25;
						continue;
					}
					goto IL_C0;
				case 2:
					return;
				case 3:
				{
					int num4;
					if (num4 <= num5)
					{
						num = 9;
						continue;
					}
					base.ᜃ(num4, num2);
					num4--;
					num2++;
					num = 7;
					continue;
				}
				case 4:
					goto IL_212;
				case 5:
					if (num3 == (int)this.ᜀ[num6][A_2])
					{
						num = 21;
						continue;
					}
					goto IL_290;
				case 6:
				{
					int num4 = A_1 - 1;
					num = 0;
					continue;
				}
				case 7:
					goto IL_237;
				case 8:
				{
					int num4;
					if (num4 >= num7)
					{
						num = 6;
						continue;
					}
					base.ᜃ(num4, num6);
					num4++;
					num6--;
					num = 22;
					continue;
				}
				case 9:
					goto IL_257;
				case 10:
					base.ᜃ(num2, num6);
					num = 1;
					continue;
				case 12:
					goto IL_25C;
				case 13:
					goto IL_25C;
				case 14:
					num = 13;
					continue;
				case 15:
					goto IL_C0;
				case 16:
					goto IL_290;
				case 17:
					if ((int)this.ᜀ[++num2][A_2] >= num3)
					{
						num = 14;
						continue;
					}
					goto IL_290;
				case 18:
					if (num3 > (int)this.ᜀ[--num6][A_2])
					{
						num = 20;
						continue;
					}
					num = 24;
					continue;
				case 19:
					goto IL_290;
				case 20:
					goto IL_2C3;
				case 21:
					num5--;
					base.ᜃ(num5, num6);
					num = 19;
					continue;
				case 22:
					goto IL_212;
				case 23:
				{
					if (num2 < num6)
					{
						num = 10;
						continue;
					}
					base.ᜃ(num2, A_1);
					num6 = num2 - 1;
					num2++;
					int num4 = A_0;
					num = 4;
					continue;
				}
				case 24:
					if (num6 == A_0)
					{
						goto IL_2C3;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_327;
					default:
						if (false)
						{
						}
						num = 12;
						continue;
					}
					break;
				case 25:
					num7++;
					base.ᜃ(num7, num2);
					num = 15;
					continue;
				}
				if (A_1 <= A_0)
				{
					num = 2;
					continue;
				}
				if (true)
				{
				}
				num3 = (int)this.ᜀ[A_1][A_2];
				num2 = A_0 - 1;
				num6 = A_1;
				num7 = A_0 - 1;
				num5 = A_1;
				num = 16;
				continue;
				IL_C0:
				num = 5;
				continue;
				IL_212:
				num = 8;
				continue;
				IL_237:
				num = 3;
				continue;
				IL_25C:
				num = 18;
				continue;
				IL_290:
				num = 17;
				continue;
				IL_2C3:
				num = 23;
			}
			return;
			IL_257:
			IL_327:
			this.ᜉ(A_0, num6, A_2);
			this.ᜉ(num2, A_1, A_2);
			return;
		}
		}
	}

	// Token: 0x06004D67 RID: 19815 RVA: 0x002F314C File Offset: 0x002F214C
	public new void ᜂ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			int num3;
			int num6;
			for (;;)
			{
				int num2;
				int num5;
				double num7;
				switch (num)
				{
				case 0:
					goto IL_25C;
				case 1:
					goto IL_212;
				case 3:
					goto IL_25C;
				case 4:
					goto IL_237;
				case 5:
					num2--;
					base.ᜃ(num2, num3);
					num = 15;
					continue;
				case 6:
					return;
				case 7:
					goto IL_257;
				case 8:
				{
					int num4;
					if (num4 >= num5)
					{
						num = 23;
						continue;
					}
					base.ᜃ(num4, num3);
					num4++;
					num3--;
					num = 18;
					continue;
				}
				case 9:
					if ((double)this.ᜀ[++num6][A_2] >= num7)
					{
						num = 12;
						continue;
					}
					goto IL_290;
				case 10:
					base.ᜃ(num6, num3);
					num = 13;
					continue;
				case 11:
					if (true)
					{
					}
					goto IL_290;
				case 12:
					num = 0;
					continue;
				case 13:
					if ((double)this.ᜀ[num6][A_2] == num7)
					{
						num = 19;
						continue;
					}
					goto IL_C0;
				case 14:
					if (num7 > (double)this.ᜀ[--num3][A_2])
					{
						num = 21;
						continue;
					}
					num = 24;
					continue;
				case 15:
					goto IL_290;
				case 16:
				{
					int num4;
					if (num4 <= num2)
					{
						num = 7;
						continue;
					}
					base.ᜃ(num4, num6);
					num4--;
					num6++;
					num = 25;
					continue;
				}
				case 17:
				{
					if (num6 < num3)
					{
						num = 10;
						continue;
					}
					base.ᜃ(num6, A_1);
					num3 = num6 - 1;
					num6++;
					int num4 = A_0;
					num = 1;
					continue;
				}
				case 18:
					goto IL_212;
				case 19:
					num5++;
					base.ᜃ(num5, num6);
					num = 22;
					continue;
				case 20:
					if (num7 == (double)this.ᜀ[num3][A_2])
					{
						num = 5;
						continue;
					}
					goto IL_290;
				case 21:
					goto IL_2C3;
				case 22:
					goto IL_C0;
				case 23:
				{
					int num4 = A_1 - 1;
					num = 4;
					continue;
				}
				case 24:
					if (num3 == A_0)
					{
						goto IL_2C3;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_327;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 25:
					goto IL_237;
				}
				if (A_1 <= A_0)
				{
					num = 6;
					continue;
				}
				num7 = (double)this.ᜀ[A_1][A_2];
				num6 = A_0 - 1;
				num3 = A_1;
				num5 = A_0 - 1;
				num2 = A_1;
				num = 11;
				continue;
				IL_C0:
				num = 20;
				continue;
				IL_212:
				num = 8;
				continue;
				IL_237:
				num = 16;
				continue;
				IL_25C:
				num = 14;
				continue;
				IL_290:
				num = 9;
				continue;
				IL_2C3:
				num = 17;
			}
			return;
			IL_257:
			IL_327:
			this.ᜂ(A_0, num3, A_2);
			this.ᜂ(num6, A_1, A_2);
			return;
		}
		}
	}

	// Token: 0x06004D68 RID: 19816 RVA: 0x002F3494 File Offset: 0x002F2494
	public new void ᜅ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 20;
			int num2;
			int num3;
			for (;;)
			{
				int num5;
				DateTime dateTime;
				int num6;
				switch (num)
				{
				case 0:
				{
					if (num2 < num3)
					{
						num = 11;
						continue;
					}
					base.ᜃ(num2, A_1);
					num3 = num2 - 1;
					num2++;
					int num4 = A_0;
					num = 22;
					continue;
				}
				case 1:
					goto IL_214;
				case 2:
					if (num3 == A_0)
					{
						goto IL_2D7;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_33B;
					default:
						if (false)
						{
						}
						num = 25;
						continue;
					}
					break;
				case 3:
					num5++;
					base.ᜃ(num5, num2);
					num = 14;
					continue;
				case 4:
					if (dateTime == (DateTime)this.ᜀ[num3][A_2])
					{
						num = 23;
						continue;
					}
					goto IL_297;
				case 5:
					goto IL_239;
				case 6:
					goto IL_259;
				case 7:
					goto IL_239;
				case 8:
					if (!((DateTime)this.ᜀ[++num2][A_2] < dateTime))
					{
						if (true)
						{
						}
						num = 18;
						continue;
					}
					goto IL_297;
				case 9:
				{
					int num4 = A_1 - 1;
					num = 7;
					continue;
				}
				case 10:
					if ((DateTime)this.ᜀ[num2][A_2] == dateTime)
					{
						num = 3;
						continue;
					}
					goto IL_C0;
				case 11:
					base.ᜃ(num2, num3);
					num = 10;
					continue;
				case 12:
					goto IL_297;
				case 13:
					if (!(dateTime <= (DateTime)this.ᜀ[--num3][A_2]))
					{
						num = 24;
						continue;
					}
					num = 2;
					continue;
				case 14:
					goto IL_C0;
				case 15:
					goto IL_25E;
				case 16:
				{
					int num4;
					if (num4 <= num6)
					{
						num = 6;
						continue;
					}
					base.ᜃ(num4, num2);
					num4--;
					num2++;
					num = 5;
					continue;
				}
				case 17:
				{
					int num4;
					if (num4 >= num5)
					{
						num = 9;
						continue;
					}
					base.ᜃ(num4, num3);
					num4++;
					num3--;
					num = 1;
					continue;
				}
				case 18:
					num = 15;
					continue;
				case 19:
					return;
				case 21:
					goto IL_297;
				case 22:
					goto IL_214;
				case 23:
					num6--;
					base.ᜃ(num6, num3);
					num = 21;
					continue;
				case 24:
					goto IL_2D7;
				case 25:
					goto IL_25E;
				}
				if (A_1 <= A_0)
				{
					num = 19;
					continue;
				}
				dateTime = (DateTime)this.ᜀ[A_1][A_2];
				num2 = A_0 - 1;
				num3 = A_1;
				num5 = A_0 - 1;
				num6 = A_1;
				num = 12;
				continue;
				IL_C0:
				num = 4;
				continue;
				IL_214:
				num = 17;
				continue;
				IL_239:
				num = 16;
				continue;
				IL_25E:
				num = 13;
				continue;
				IL_297:
				num = 8;
				continue;
				IL_2D7:
				num = 0;
			}
			return;
			IL_259:
			IL_33B:
			this.ᜅ(A_0, num3, A_2);
			this.ᜅ(num2, A_1, A_2);
			return;
		}
		}
	}

	// Token: 0x06004D69 RID: 19817 RVA: 0x002F37F0 File Offset: 0x002F27F0
	public new void ᜃ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			int num4;
			int num5;
			for (;;)
			{
				int num3;
				string text;
				int num6;
				switch (num)
				{
				case 0:
					goto IL_25E;
				case 1:
				{
					int num2 = A_1 - 1;
					num = 17;
					continue;
				}
				case 2:
					return;
				case 3:
					num3--;
					base.ᜃ(num3, num4);
					num = 20;
					continue;
				case 5:
					if (text.CompareTo((string)this.ᜀ[--num4][A_2]) > 0)
					{
						num = 15;
						continue;
					}
					num = 13;
					continue;
				case 6:
				{
					int num2;
					if (num2 <= num3)
					{
						num = 21;
						continue;
					}
					base.ᜃ(num2, num5);
					num2--;
					num5++;
					num = 23;
					continue;
				}
				case 7:
					goto IL_25E;
				case 8:
				{
					int num2;
					if (num2 >= num6)
					{
						num = 1;
						continue;
					}
					base.ᜃ(num2, num4);
					num2++;
					num4--;
					num = 12;
					continue;
				}
				case 9:
					goto IL_C0;
				case 10:
				{
					if (num5 < num4)
					{
						num = 24;
						continue;
					}
					base.ᜃ(num5, A_1);
					num4 = num5 - 1;
					num5++;
					int num2 = A_0;
					num = 25;
					continue;
				}
				case 11:
					num = 7;
					continue;
				case 12:
					goto IL_214;
				case 13:
					if (num4 == A_0)
					{
						goto IL_2D1;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_33D;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 14:
					if ((string)this.ᜀ[num5][A_2] == text)
					{
						num = 19;
						continue;
					}
					goto IL_C0;
				case 15:
					goto IL_2D1;
				case 16:
					if (text == (string)this.ᜀ[num4][A_2])
					{
						num = 3;
						continue;
					}
					goto IL_298;
				case 17:
					goto IL_239;
				case 18:
					goto IL_298;
				case 19:
					num6++;
					base.ᜃ(num6, num5);
					num = 9;
					continue;
				case 20:
					goto IL_298;
				case 21:
					goto IL_259;
				case 22:
					if (((string)this.ᜀ[++num5][A_2]).CompareTo(text) >= 0)
					{
						num = 11;
						continue;
					}
					goto IL_298;
				case 23:
					goto IL_239;
				case 24:
					base.ᜃ(num5, num4);
					num = 14;
					continue;
				case 25:
					goto IL_214;
				}
				if (A_1 <= A_0)
				{
					num = 2;
					continue;
				}
				text = (string)this.ᜀ[A_1][A_2];
				num5 = A_0 - 1;
				num4 = A_1;
				num6 = A_0 - 1;
				num3 = A_1;
				num = 18;
				continue;
				IL_C0:
				num = 16;
				continue;
				IL_214:
				num = 8;
				continue;
				IL_239:
				num = 6;
				continue;
				IL_25E:
				num = 5;
				continue;
				IL_298:
				num = 22;
				continue;
				IL_2D1:
				if (true)
				{
				}
				num = 10;
			}
			return;
			IL_259:
			IL_33D:
			this.ᜃ(A_0, num4, A_2);
			this.ᜃ(num5, A_1, A_2);
			return;
		}
		}
	}

	// Token: 0x06004D6A RID: 19818 RVA: 0x002F3B4C File Offset: 0x002F2B4C
	public new void ᜀ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 14;
			int num3;
			int num6;
			for (;;)
			{
				int num2;
				int num4;
				int num5;
				switch (num)
				{
				case 0:
					if (num2 < (int)this.ᜀ[--num3][A_2])
					{
						num = 24;
						continue;
					}
					num = 18;
					continue;
				case 1:
					num4--;
					base.ᜃ(num4, num3);
					num = 19;
					continue;
				case 2:
					return;
				case 3:
					goto IL_257;
				case 4:
					num5++;
					base.ᜃ(num5, num6);
					num = 9;
					continue;
				case 5:
				{
					if (num6 > num3)
					{
						num = 12;
						continue;
					}
					base.ᜃ(num6, A_1);
					num3 = num6 - 1;
					num6++;
					int num7 = A_0;
					num = 17;
					continue;
				}
				case 6:
					if ((int)this.ᜀ[++num6][A_2] <= num2)
					{
						num = 8;
						continue;
					}
					goto IL_290;
				case 7:
				{
					int num7;
					if (num7 >= num5)
					{
						num = 13;
						continue;
					}
					base.ᜃ(num7, num3);
					num7++;
					num3--;
					num = 11;
					continue;
				}
				case 8:
					num = 20;
					continue;
				case 9:
					goto IL_C0;
				case 10:
					if (num2 == (int)this.ᜀ[num3][A_2])
					{
						num = 1;
						continue;
					}
					goto IL_290;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1B9;
					default:
						if (false)
						{
						}
						goto IL_20A;
					}
					break;
				case 12:
					base.ᜃ(num6, num3);
					num = 21;
					continue;
				case 13:
				{
					int num7 = A_1 - 1;
					num = 22;
					continue;
				}
				case 15:
					goto IL_25C;
				case 16:
					goto IL_290;
				case 17:
					goto IL_20A;
				case 18:
					if (num3 != A_0)
					{
						num = 15;
						continue;
					}
					goto IL_2C3;
				case 19:
					goto IL_290;
				case 20:
					goto IL_25C;
				case 21:
					goto IL_1B9;
				case 22:
					goto IL_22F;
				case 23:
				{
					int num7;
					if (num7 <= num4)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					base.ᜃ(num7, num6);
					num7--;
					num6++;
					num = 25;
					continue;
				}
				case 24:
					goto IL_2C3;
				case 25:
					goto IL_22F;
				}
				if (A_1 <= A_0)
				{
					num = 2;
					continue;
				}
				num2 = (int)this.ᜀ[A_1][A_2];
				num6 = A_0 - 1;
				num3 = A_1;
				num5 = A_0 - 1;
				num4 = A_1;
				num = 16;
				continue;
				IL_C0:
				num = 10;
				continue;
				IL_1B9:
				if ((int)this.ᜀ[num6][A_2] == num2)
				{
					num = 4;
					continue;
				}
				goto IL_C0;
				IL_20A:
				num = 7;
				continue;
				IL_22F:
				num = 23;
				continue;
				IL_25C:
				num = 0;
				continue;
				IL_290:
				num = 6;
				continue;
				IL_2C3:
				num = 5;
			}
			return;
			IL_257:
			this.ᜀ(A_0, num3, A_2);
			this.ᜀ(num6, A_1, A_2);
			return;
		}
		}
	}

	// Token: 0x06004D6B RID: 19819 RVA: 0x002F3E94 File Offset: 0x002F2E94
	public new void ᜆ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 21;
			int num3;
			int num5;
			for (;;)
			{
				int num2;
				double num6;
				int num7;
				switch (num)
				{
				case 0:
					num2--;
					base.ᜃ(num2, num3);
					num = 5;
					continue;
				case 1:
					if (true)
					{
					}
					goto IL_290;
				case 2:
				{
					int num4 = A_1 - 1;
					num = 8;
					continue;
				}
				case 3:
					if ((double)this.ᜀ[++num5][A_2] <= num6)
					{
						num = 12;
						continue;
					}
					goto IL_290;
				case 4:
					goto IL_237;
				case 5:
					goto IL_290;
				case 6:
					if (num6 < (double)this.ᜀ[--num3][A_2])
					{
						num = 19;
						continue;
					}
					num = 24;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C1;
					default:
						if (false)
						{
						}
						goto IL_212;
					}
					break;
				case 8:
					goto IL_237;
				case 9:
					goto IL_25C;
				case 10:
					num7++;
					base.ᜃ(num7, num5);
					num = 11;
					continue;
				case 11:
					goto IL_C0;
				case 12:
					num = 9;
					continue;
				case 13:
					return;
				case 14:
				{
					int num4;
					if (num4 >= num7)
					{
						num = 2;
						continue;
					}
					base.ᜃ(num4, num3);
					num4++;
					num3--;
					num = 7;
					continue;
				}
				case 15:
					goto IL_1C1;
				case 16:
				{
					if (num5 < num3)
					{
						num = 18;
						continue;
					}
					base.ᜃ(num5, A_1);
					num3 = num5 - 1;
					num5++;
					int num4 = A_0;
					num = 25;
					continue;
				}
				case 17:
				{
					int num4;
					if (num4 <= num2)
					{
						num = 20;
						continue;
					}
					base.ᜃ(num4, num5);
					num4--;
					num5++;
					num = 4;
					continue;
				}
				case 18:
					base.ᜃ(num5, num3);
					num = 15;
					continue;
				case 19:
					goto IL_2C3;
				case 20:
					goto IL_257;
				case 22:
					if (num6 == (double)this.ᜀ[num3][A_2])
					{
						num = 0;
						continue;
					}
					goto IL_290;
				case 23:
					goto IL_25C;
				case 24:
					if (num3 != A_0)
					{
						num = 23;
						continue;
					}
					goto IL_2C3;
				case 25:
					goto IL_212;
				}
				if (A_1 <= A_0)
				{
					num = 13;
					continue;
				}
				num6 = (double)this.ᜀ[A_1][A_2];
				num5 = A_0 - 1;
				num3 = A_1;
				num7 = A_0 - 1;
				num2 = A_1;
				num = 1;
				continue;
				IL_C0:
				num = 22;
				continue;
				IL_1C1:
				if ((double)this.ᜀ[num5][A_2] == num6)
				{
					num = 10;
					continue;
				}
				goto IL_C0;
				IL_212:
				num = 14;
				continue;
				IL_237:
				num = 17;
				continue;
				IL_25C:
				num = 6;
				continue;
				IL_290:
				num = 3;
				continue;
				IL_2C3:
				num = 16;
			}
			return;
			IL_257:
			this.ᜆ(A_0, num3, A_2);
			this.ᜆ(num5, A_1, A_2);
			return;
		}
		}
	}

	// Token: 0x06004D6C RID: 19820 RVA: 0x002F41DC File Offset: 0x002F31DC
	public new void ᜇ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 20;
			int num2;
			int num3;
			for (;;)
			{
				DateTime dateTime;
				int num5;
				int num6;
				switch (num)
				{
				case 0:
					goto IL_29F;
				case 1:
					if (!(dateTime >= (DateTime)this.ᜀ[--num2][A_2]))
					{
						num = 19;
						continue;
					}
					num = 6;
					continue;
				case 2:
					if (!((DateTime)this.ᜀ[++num3][A_2] > dateTime))
					{
						num = 8;
						continue;
					}
					goto IL_29F;
				case 3:
				{
					int num4;
					if (num4 <= num5)
					{
						num = 22;
						continue;
					}
					base.ᜃ(num4, num3);
					num4--;
					num3++;
					num = 17;
					continue;
				}
				case 4:
					goto IL_C0;
				case 5:
					num6++;
					base.ᜃ(num6, num3);
					num = 4;
					continue;
				case 6:
					if (num2 != A_0)
					{
						num = 23;
						continue;
					}
					goto IL_2D7;
				case 7:
				{
					int num4 = A_1 - 1;
					num = 10;
					continue;
				}
				case 8:
					num = 25;
					continue;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C6;
					default:
						if (false)
						{
						}
						goto IL_21C;
					}
					break;
				case 10:
					goto IL_241;
				case 11:
					goto IL_21C;
				case 12:
				{
					int num4;
					if (num4 >= num6)
					{
						num = 7;
						continue;
					}
					base.ᜃ(num4, num2);
					num4++;
					num2--;
					num = 9;
					continue;
				}
				case 13:
					goto IL_1C6;
				case 14:
					if (dateTime == (DateTime)this.ᜀ[num2][A_2])
					{
						num = 15;
						continue;
					}
					goto IL_29F;
				case 15:
					num5--;
					base.ᜃ(num5, num2);
					num = 16;
					continue;
				case 16:
					goto IL_29F;
				case 17:
					goto IL_241;
				case 18:
					base.ᜃ(num3, num2);
					num = 13;
					continue;
				case 19:
					goto IL_2D7;
				case 21:
					return;
				case 22:
					goto IL_261;
				case 23:
					goto IL_266;
				case 24:
				{
					if (num3 < num2)
					{
						num = 18;
						continue;
					}
					base.ᜃ(num3, A_1);
					num2 = num3 - 1;
					num3++;
					int num4 = A_0;
					num = 11;
					continue;
				}
				case 25:
					goto IL_266;
				}
				if (A_1 <= A_0)
				{
					num = 21;
					continue;
				}
				if (true)
				{
				}
				dateTime = (DateTime)this.ᜀ[A_1][A_2];
				num3 = A_0 - 1;
				num2 = A_1;
				num6 = A_0 - 1;
				num5 = A_1;
				num = 0;
				continue;
				IL_C0:
				num = 14;
				continue;
				IL_1C6:
				if ((DateTime)this.ᜀ[num3][A_2] == dateTime)
				{
					num = 5;
					continue;
				}
				goto IL_C0;
				IL_21C:
				num = 12;
				continue;
				IL_241:
				num = 3;
				continue;
				IL_266:
				num = 1;
				continue;
				IL_29F:
				num = 2;
				continue;
				IL_2D7:
				num = 24;
			}
			return;
			IL_261:
			this.ᜇ(A_0, num2, A_2);
			this.ᜇ(num3, A_1, A_2);
			return;
		}
		}
	}

	// Token: 0x06004D6D RID: 19821 RVA: 0x002F4538 File Offset: 0x002F3538
	public new void ᜈ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 19;
			int num2;
			int num3;
			for (;;)
			{
				int num5;
				string text;
				int num6;
				switch (num)
				{
				case 0:
					if (num2 != A_0)
					{
						num = 1;
						continue;
					}
					goto IL_2D9;
				case 1:
					goto IL_266;
				case 2:
					goto IL_1BE;
				case 3:
					goto IL_241;
				case 4:
				{
					if (num3 < num2)
					{
						num = 11;
						continue;
					}
					base.ᜃ(num3, A_1);
					num2 = num3 - 1;
					num3++;
					int num4 = A_0;
					num = 8;
					continue;
				}
				case 5:
				{
					int num4;
					if (num4 <= num5)
					{
						num = 25;
						continue;
					}
					base.ᜃ(num4, num3);
					num4--;
					num3++;
					num = 3;
					continue;
				}
				case 6:
					goto IL_2D9;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1BE;
					default:
						if (false)
						{
						}
						goto IL_214;
					}
					break;
				case 8:
					goto IL_214;
				case 9:
					num5--;
					base.ᜃ(num5, num2);
					num = 20;
					continue;
				case 10:
					if (text.CompareTo((string)this.ᜀ[--num2][A_2]) < 0)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
				case 11:
					base.ᜃ(num3, num2);
					num = 2;
					continue;
				case 12:
					goto IL_C0;
				case 13:
					num = 21;
					continue;
				case 14:
				{
					if (true)
					{
					}
					int num4;
					if (num4 >= num6)
					{
						num = 22;
						continue;
					}
					base.ᜃ(num4, num2);
					num4++;
					num2--;
					num = 7;
					continue;
				}
				case 15:
					num6++;
					base.ᜃ(num6, num3);
					num = 12;
					continue;
				case 16:
					if (text == (string)this.ᜀ[num2][A_2])
					{
						num = 9;
						continue;
					}
					goto IL_2A0;
				case 17:
					goto IL_2A0;
				case 18:
					if (((string)this.ᜀ[++num3][A_2]).CompareTo(text) <= 0)
					{
						num = 13;
						continue;
					}
					goto IL_2A0;
				case 20:
					goto IL_2A0;
				case 21:
					goto IL_266;
				case 22:
				{
					int num4 = A_1 - 1;
					num = 23;
					continue;
				}
				case 23:
					goto IL_241;
				case 24:
					return;
				case 25:
					goto IL_261;
				}
				if (A_1 <= A_0)
				{
					num = 24;
					continue;
				}
				text = (string)this.ᜀ[A_1][A_2];
				num3 = A_0 - 1;
				num2 = A_1;
				num6 = A_0 - 1;
				num5 = A_1;
				num = 17;
				continue;
				IL_C0:
				num = 16;
				continue;
				IL_1BE:
				if ((string)this.ᜀ[num3][A_2] == text)
				{
					num = 15;
					continue;
				}
				goto IL_C0;
				IL_214:
				num = 14;
				continue;
				IL_241:
				num = 5;
				continue;
				IL_266:
				num = 10;
				continue;
				IL_2A0:
				num = 18;
				continue;
				IL_2D9:
				num = 4;
			}
			return;
			IL_261:
			this.ᜈ(A_0, num2, A_2);
			this.ᜈ(num3, A_1, A_2);
			return;
		}
		}
	}

	// Token: 0x06004D6E RID: 19822 RVA: 0x002F4894 File Offset: 0x002F3894
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
		this.ᜁ(A_0, A_1, A_2);
	}

	// Token: 0x06004D6F RID: 19823 RVA: 0x002F48D8 File Offset: 0x002F38D8
	public new void ᜁ(int A_0, int A_1, int A_2)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_209;
			case 1:
				goto IL_134;
			case 2:
				if (this.ᜃ[0] != OrderBy.Ascending)
				{
					goto IL_139;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_139;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 3:
				goto IL_236;
			case 5:
			{
				DateTime now = DateTime.Now;
				num = 14;
				continue;
			}
			case 6:
				if (this.ᜂ[0] == typeof(DateTime))
				{
					num = 13;
					continue;
				}
				return;
			case 7:
				num = 2;
				continue;
			case 8:
				if (true)
				{
				}
				num = 15;
				continue;
			case 9:
				if (this.ᜂ[0] == typeof(string))
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
			case 10:
				if (this.ᜂ[0] == typeof(double))
				{
					num = 7;
					continue;
				}
				num = 9;
				continue;
			case 11:
				goto IL_EC;
			case 12:
				goto IL_B1;
			case 13:
			{
				DateTime now2 = DateTime.Now;
				num = 16;
				continue;
			}
			case 14:
				if (this.ᜃ[0] == OrderBy.Ascending)
				{
					num = 12;
					continue;
				}
				goto IL_78;
			case 15:
				if (this.ᜃ[0] == OrderBy.Ascending)
				{
					num = 3;
					continue;
				}
				goto IL_B3;
			case 16:
				if (this.ᜃ[0] == OrderBy.Ascending)
				{
					num = 11;
					continue;
				}
				this.ᜇ(0, this.ᜀ.Length - 1, A_2);
				num = 1;
				continue;
			}
			if (this.ᜂ[0] == typeof(int))
			{
				num = 8;
			}
			else
			{
				num = 10;
			}
		}
		IL_78:
		this.ᜈ(0, this.ᜀ.Length - 1, A_2);
		return;
		IL_B1:
		this.ᜃ(0, this.ᜀ.Length - 1, A_2);
		return;
		IL_B3:
		this.ᜀ(0, this.ᜀ.Length - 1, A_2);
		return;
		IL_EC:
		this.ᜅ(0, this.ᜀ.Length - 1, A_2);
		return;
		IL_134:
		return;
		IL_139:
		this.ᜆ(0, this.ᜀ.Length - 1, A_2);
		return;
		IL_209:
		this.ᜂ(0, this.ᜀ.Length - 1, A_2);
		return;
		IL_236:
		this.ᜉ(0, this.ᜀ.Length - 1, A_2);
	}

	// Token: 0x06004D70 RID: 19824 RVA: 0x002F4B68 File Offset: 0x002F3B68
	public new IXLSRange ᜀ()
	{
		int a_ = 19;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("ᭈ⩊⍌⡎㑐", a_));
	}

	// Token: 0x06004D71 RID: 19825 RVA: 0x002F4BC0 File Offset: 0x002F3BC0
	public new void ᜀ(IXLSRange A_0)
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("樷嬹刻夽┿", a_));
	}
}
