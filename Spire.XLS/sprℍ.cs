using System;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003B7 RID: 951
internal class sprℍ : spr\u2374
{
	// Token: 0x06003A45 RID: 14917 RVA: 0x0020BBB0 File Offset: 0x0020ABB0
	public sprℍ(object[][] A_0, Type[] A_1, OrderBy[] A_2, Color[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x06003A46 RID: 14918 RVA: 0x0020BBC8 File Offset: 0x0020ABC8
	public new void ᜉ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0 + 1;
				int num2 = 8;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						this.ᜁ(num3 - 1, num3, A_2 + 1);
						num2 = 6;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F5;
						default:
							if (false)
							{
							}
							num2 = 7;
							continue;
						}
						break;
					case 2:
						goto IL_F5;
					case 3:
						if (A_2 + 1 <= this.ᜁ)
						{
							num2 = 1;
							continue;
						}
						goto IL_8E;
					case 4:
						return;
					case 5:
					{
						if (num > A_1)
						{
							num2 = 4;
							continue;
						}
						base.ᜁ(num);
						int num4 = (int)this.ᜀ[num][A_2];
						num3 = num;
						num2 = 16;
						continue;
					}
					case 6:
						goto IL_1DD;
					case 7:
					{
						int num4;
						if (num4 == (int)this.ᜀ[num3 - 1][A_2])
						{
							num2 = 17;
							continue;
						}
						goto IL_8E;
					}
					case 8:
						goto IL_163;
					case 9:
						goto IL_73;
					case 10:
						if (A_2 + 1 < this.ᜁ)
						{
							num2 = 0;
							continue;
						}
						goto IL_8E;
					case 11:
						goto IL_163;
					case 12:
						num2 = 14;
						continue;
					case 13:
						if (num3 > A_0)
						{
							num2 = 12;
							continue;
						}
						goto IL_F5;
					case 14:
					{
						int num4;
						if ((int)this.ᜀ[num3 - 1][A_2] < num4)
						{
							num2 = 2;
							continue;
						}
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					case 15:
						goto IL_1DD;
					case 16:
						goto IL_73;
					case 17:
						num2 = 10;
						continue;
					}
					break;
					IL_73:
					num2 = 13;
					continue;
					IL_8E:
					base.ᜃ(num3 - 1, num3);
					num2 = 15;
					continue;
					IL_F5:
					num++;
					num2 = 11;
					continue;
					IL_163:
					num2 = 5;
					continue;
					IL_1DD:
					num3--;
					num2 = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06003A47 RID: 14919 RVA: 0x0020BDF4 File Offset: 0x0020ADF4
	public new void ᜂ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0 + 1;
				int num2 = 10;
				for (;;)
				{
					double num3;
					int num4;
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_96;
					case 2:
						goto IL_15F;
					case 3:
						if (num > A_1)
						{
							num2 = 0;
							continue;
						}
						base.ᜁ(num);
						num3 = (double)this.ᜀ[num][A_2];
						num4 = num;
						num2 = 5;
						continue;
					case 4:
						num2 = 6;
						continue;
					case 5:
						goto IL_13E;
					case 6:
						goto IL_1A7;
					case 7:
						goto IL_104;
					case 8:
						if ((double)this.ᜀ[num4 - 1][A_2] < num3)
						{
							num2 = 7;
							continue;
						}
						if (true)
						{
						}
						num2 = 15;
						continue;
					case 9:
						num2 = 8;
						continue;
					case 10:
						goto IL_15F;
					case 11:
						if (num4 > A_0)
						{
							num2 = 9;
							continue;
						}
						goto IL_104;
					case 12:
						goto IL_96;
					case 13:
						goto IL_13E;
					case 14:
						this.ᜁ(num4 - 1, num4, A_2 + 1);
						num2 = 12;
						continue;
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1A7;
						}
						if (false)
						{
						}
						if (A_2 + 1 <= this.ᜁ)
						{
							num2 = 4;
							continue;
						}
						goto IL_1C9;
					}
					break;
					IL_96:
					num4--;
					num2 = 13;
					continue;
					IL_104:
					num++;
					num2 = 2;
					continue;
					IL_13E:
					num2 = 11;
					continue;
					IL_15F:
					num2 = 3;
					continue;
					IL_1A7:
					if (num3 == (double)this.ᜀ[num4 - 1][A_2])
					{
						num2 = 14;
						continue;
					}
					IL_1C9:
					base.ᜃ(num4 - 1, num4);
					num2 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x06003A48 RID: 14920 RVA: 0x0020BFE8 File Offset: 0x0020AFE8
	public new void ᜅ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0 + 1;
				int num2 = 7;
				for (;;)
				{
					int num3;
					DateTime dateTime;
					switch (num2)
					{
					case 0:
						if (!((DateTime)this.ᜀ[num3 - 1][A_2] >= dateTime))
						{
							num2 = 2;
							continue;
						}
						num2 = 3;
						continue;
					case 1:
						goto IL_13B;
					case 2:
						goto IL_101;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1AF;
						default:
							if (false)
							{
							}
							if (A_2 + 1 <= this.ᜁ)
							{
								num2 = 12;
								continue;
							}
							goto IL_1D6;
						}
						break;
					case 4:
						goto IL_1AF;
					case 5:
						goto IL_9B;
					case 6:
						if (true)
						{
						}
						goto IL_9B;
					case 7:
						goto IL_15C;
					case 8:
						this.ᜁ(num3 - 1, num3, A_2 + 1);
						num2 = 6;
						continue;
					case 9:
						if (num3 > A_0)
						{
							num2 = 14;
							continue;
						}
						goto IL_101;
					case 10:
						if (num > A_1)
						{
							num2 = 11;
							continue;
						}
						base.ᜁ(num);
						dateTime = (DateTime)this.ᜀ[num][A_2];
						num3 = num;
						num2 = 1;
						continue;
					case 11:
						return;
					case 12:
						num2 = 4;
						continue;
					case 13:
						goto IL_13B;
					case 14:
						num2 = 0;
						continue;
					case 15:
						goto IL_15C;
					}
					break;
					IL_9B:
					num3--;
					num2 = 13;
					continue;
					IL_101:
					num++;
					num2 = 15;
					continue;
					IL_13B:
					num2 = 9;
					continue;
					IL_15C:
					num2 = 10;
					continue;
					IL_1AF:
					if (dateTime == (DateTime)this.ᜀ[num3 - 1][A_2])
					{
						num2 = 8;
						continue;
					}
					IL_1D6:
					base.ᜃ(num3 - 1, num3);
					num2 = 5;
				}
			}
			return;
		}
	}

	// Token: 0x06003A49 RID: 14921 RVA: 0x0020C1E8 File Offset: 0x0020B1E8
	public new void ᜃ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0 + 1;
				int num2 = 0;
				for (;;)
				{
					int num3;
					string text;
					switch (num2)
					{
					case 0:
						goto IL_15D;
					case 1:
						goto IL_102;
					case 2:
						if (num3 > A_0)
						{
							num2 = 4;
							continue;
						}
						goto IL_102;
					case 3:
						goto IL_13C;
					case 4:
						num2 = 8;
						continue;
					case 5:
						if (num > A_1)
						{
							num2 = 13;
							continue;
						}
						base.ᜁ(num);
						text = (string)this.ᜀ[num][A_2];
						num3 = num;
						num2 = 3;
						continue;
					case 6:
						goto IL_13C;
					case 7:
						if (true)
						{
						}
						num2 = 11;
						continue;
					case 8:
						if (((string)this.ᜀ[num3 - 1][A_2]).CompareTo(text) < 0)
						{
							num2 = 1;
							continue;
						}
						num2 = 14;
						continue;
					case 9:
						goto IL_9C;
					case 10:
						this.ᜁ(num3 - 1, num3, A_2 + 1);
						num2 = 15;
						continue;
					case 11:
						goto IL_1B0;
					case 12:
						goto IL_15D;
					case 13:
						return;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B0;
						default:
							if (false)
							{
							}
							if (A_2 + 1 <= this.ᜁ)
							{
								num2 = 7;
								continue;
							}
							goto IL_1D7;
						}
						break;
					case 15:
						goto IL_9C;
					}
					break;
					IL_9C:
					num3--;
					num2 = 6;
					continue;
					IL_102:
					num++;
					num2 = 12;
					continue;
					IL_13C:
					num2 = 2;
					continue;
					IL_15D:
					num2 = 5;
					continue;
					IL_1B0:
					if (text == (string)this.ᜀ[num3 - 1][A_2])
					{
						num2 = 10;
						continue;
					}
					IL_1D7:
					base.ᜃ(num3 - 1, num3);
					num2 = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06003A4A RID: 14922 RVA: 0x0020C3E8 File Offset: 0x0020B3E8
	public new void ᜁ(int A_0, int A_1, int A_2)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7A;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (this.ᜂ[A_2 - 1] == typeof(string))
					{
						num = 8;
						continue;
					}
					num = 7;
					continue;
				}
				break;
			case 2:
				if (this.ᜃ[A_2 - 1] == OrderBy.Ascending)
				{
					num = 5;
					continue;
				}
				this.ᜇ(A_0, A_1, A_2);
				num = 12;
				continue;
			case 3:
				goto IL_F9;
			case 4:
				if (this.ᜃ[A_2 - 1] == OrderBy.Ascending)
				{
					num = 13;
					continue;
				}
				goto IL_136;
			case 5:
				goto IL_1E0;
			case 6:
				num = 14;
				continue;
			case 7:
				goto IL_7A;
			case 8:
				num = 4;
				continue;
			case 9:
				if (this.ᜂ[A_2 - 1] == typeof(double))
				{
					num = 6;
					continue;
				}
				num = 1;
				continue;
			case 10:
				goto IL_6D;
			case 11:
				num = 2;
				continue;
			case 12:
				goto IL_154;
			case 13:
				goto IL_CF;
			case 14:
				if (this.ᜃ[A_2 - 1] == OrderBy.Ascending)
				{
					num = 3;
					continue;
				}
				goto IL_1F9;
			}
			if (this.ᜂ[A_2 - 1] == typeof(int))
			{
				num = 10;
				continue;
			}
			num = 9;
			continue;
			IL_7A:
			if (this.ᜂ[A_2 - 1] != typeof(DateTime))
			{
				return;
			}
			num = 11;
		}
		IL_6D:
		this.ᜉ(A_0, A_1, A_2);
		return;
		IL_CF:
		this.ᜃ(A_0, A_1, A_2);
		return;
		IL_F9:
		this.ᜂ(A_0, A_1, A_2);
		return;
		IL_136:
		this.ᜈ(A_0, A_1, A_2);
		return;
		IL_154:
		return;
		IL_1E0:
		this.ᜅ(A_0, A_1, A_2);
		return;
		IL_1F9:
		this.ᜆ(A_0, A_1, A_2);
	}

	// Token: 0x06003A4B RID: 14923 RVA: 0x0020C5F8 File Offset: 0x0020B5F8
	public new void ᜀ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0 + 1;
				int num2 = 10;
				for (;;)
				{
					int num4;
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						if (A_2 + 1 <= this.ᜁ)
						{
							num2 = 8;
							continue;
						}
						goto IL_1C9;
					case 1:
						goto IL_96;
					case 2:
						num2 = 13;
						continue;
					case 3:
					{
						int num3;
						if (num3 == (int)this.ᜀ[num4 - 1][A_2])
						{
							num2 = 11;
							continue;
						}
						goto IL_1C9;
					}
					case 4:
						goto IL_15F;
					case 5:
						return;
					case 6:
						goto IL_96;
					case 7:
						goto IL_DE;
					case 8:
						num2 = 3;
						continue;
					case 9:
						goto IL_13E;
					case 10:
						goto IL_15F;
					case 11:
						this.ᜁ(num4 - 1, num4, A_2 + 1);
						num2 = 6;
						continue;
					case 12:
						if (num4 > A_0)
						{
							num2 = 2;
							continue;
						}
						goto IL_DE;
					case 13:
					{
						int num3;
						if ((int)this.ᜀ[num4 - 1][A_2] > num3)
						{
							num2 = 7;
							continue;
						}
						num2 = 0;
						continue;
					}
					case 14:
					{
						if (num > A_1)
						{
							num2 = 5;
							continue;
						}
						base.ᜁ(num);
						int num3 = (int)this.ᜀ[num][A_2];
						num4 = num;
						num2 = 15;
						continue;
					}
					case 15:
						goto IL_13E;
					}
					break;
					IL_96:
					num4--;
					num2 = 9;
					continue;
					IL_DE:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_13E:
						num2 = 12;
						continue;
					default:
						if (false)
						{
						}
						num2 = 4;
						continue;
					}
					IL_15F:
					num2 = 14;
					continue;
					IL_1C9:
					base.ᜃ(num4 - 1, num4);
					num2 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x06003A4C RID: 14924 RVA: 0x0020C7EC File Offset: 0x0020B7EC
	public new void ᜆ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0 + 1;
				int num2 = 4;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_DE;
					case 1:
						goto IL_15F;
					case 2:
						return;
					case 3:
						goto IL_96;
					case 4:
						goto IL_15F;
					case 5:
						num2 = 9;
						continue;
					case 6:
						if (A_2 + 1 <= this.ᜁ)
						{
							num2 = 5;
							continue;
						}
						goto IL_1C9;
					case 7:
						this.ᜁ(num3 - 1, num3, A_2 + 1);
						num2 = 3;
						continue;
					case 8:
						goto IL_96;
					case 9:
					{
						double num4;
						if (num4 == (double)this.ᜀ[num3 - 1][A_2])
						{
							num2 = 7;
							continue;
						}
						goto IL_1C9;
					}
					case 10:
					{
						if (num > A_1)
						{
							num2 = 2;
							continue;
						}
						base.ᜁ(num);
						double num4 = (double)this.ᜀ[num][A_2];
						num3 = num;
						num2 = 12;
						continue;
					}
					case 11:
						num2 = 13;
						continue;
					case 12:
						goto IL_13E;
					case 13:
					{
						double num4;
						if ((double)this.ᜀ[num3 - 1][A_2] > num4)
						{
							num2 = 0;
							continue;
						}
						num2 = 6;
						continue;
					}
					case 14:
						if (num3 > A_0)
						{
							num2 = 11;
							continue;
						}
						goto IL_DE;
					case 15:
						if (true)
						{
						}
						goto IL_13E;
					}
					break;
					IL_96:
					num3--;
					num2 = 15;
					continue;
					IL_DE:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_13E:
						num2 = 14;
						continue;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					IL_15F:
					num2 = 10;
					continue;
					IL_1C9:
					base.ᜃ(num3 - 1, num3);
					num2 = 8;
				}
			}
			return;
		}
	}

	// Token: 0x06003A4D RID: 14925 RVA: 0x0020C9E0 File Offset: 0x0020B9E0
	public new void ᜇ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0 + 1;
				int num2 = 8;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						this.ᜁ(num3 - 1, num3, A_2 + 1);
						num2 = 9;
						continue;
					case 1:
						return;
					case 2:
						goto IL_9B;
					case 3:
						num2 = 15;
						continue;
					case 4:
						goto IL_13B;
					case 5:
						goto IL_DB;
					case 6:
						num2 = 12;
						continue;
					case 7:
						if (true)
						{
						}
						if (num3 > A_0)
						{
							num2 = 3;
							continue;
						}
						goto IL_DB;
					case 8:
						goto IL_164;
					case 9:
						goto IL_9B;
					case 10:
						if (A_2 + 1 <= this.ᜁ)
						{
							num2 = 6;
							continue;
						}
						goto IL_1D6;
					case 11:
					{
						if (num > A_1)
						{
							num2 = 1;
							continue;
						}
						base.ᜁ(num);
						DateTime dateTime = (DateTime)this.ᜀ[num][A_2];
						num3 = num;
						num2 = 13;
						continue;
					}
					case 12:
					{
						DateTime dateTime;
						if (dateTime == (DateTime)this.ᜀ[num3 - 1][A_2])
						{
							num2 = 0;
							continue;
						}
						goto IL_1D6;
					}
					case 13:
						goto IL_13B;
					case 14:
						goto IL_164;
					case 15:
					{
						DateTime dateTime;
						if (!((DateTime)this.ᜀ[num3 - 1][A_2] <= dateTime))
						{
							num2 = 5;
							continue;
						}
						num2 = 10;
						continue;
					}
					}
					break;
					IL_9B:
					num3--;
					num2 = 4;
					continue;
					IL_DB:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_13B:
						num2 = 7;
						continue;
					default:
						if (false)
						{
						}
						num2 = 14;
						continue;
					}
					IL_164:
					num2 = 11;
					continue;
					IL_1D6:
					base.ᜃ(num3 - 1, num3);
					num2 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06003A4E RID: 14926 RVA: 0x0020CBE0 File Offset: 0x0020BBE0
	public new void ᜈ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0 + 1;
				int num2 = 10;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_9C;
					case 1:
						goto IL_144;
					case 2:
					{
						string text;
						if (text == (string)this.ᜀ[num3 - 1][A_2])
						{
							num2 = 15;
							continue;
						}
						goto IL_1D7;
					}
					case 3:
					{
						string text;
						if (((string)this.ᜀ[num3 - 1][A_2]).CompareTo(text) > 0)
						{
							num2 = 8;
							continue;
						}
						num2 = 11;
						continue;
					}
					case 4:
						num2 = 3;
						continue;
					case 5:
						return;
					case 6:
						goto IL_9C;
					case 7:
						goto IL_144;
					case 8:
						goto IL_E4;
					case 9:
					{
						if (num > A_1)
						{
							num2 = 5;
							continue;
						}
						base.ᜁ(num);
						string text = (string)this.ᜀ[num][A_2];
						num3 = num;
						num2 = 7;
						continue;
					}
					case 10:
						goto IL_165;
					case 11:
						if (A_2 + 1 <= this.ᜁ)
						{
							num2 = 12;
							continue;
						}
						goto IL_1D7;
					case 12:
						num2 = 2;
						continue;
					case 13:
						goto IL_165;
					case 14:
						if (num3 > A_0)
						{
							num2 = 4;
							continue;
						}
						goto IL_E4;
					case 15:
						this.ᜁ(num3 - 1, num3, A_2 + 1);
						num2 = 6;
						continue;
					}
					break;
					IL_9C:
					num3--;
					if (true)
					{
					}
					num2 = 1;
					continue;
					IL_E4:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_144:
						num2 = 14;
						continue;
					default:
						if (false)
						{
						}
						num2 = 13;
						continue;
					}
					IL_165:
					num2 = 9;
					continue;
					IL_1D7:
					base.ᜃ(num3 - 1, num3);
					num2 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06003A4F RID: 14927 RVA: 0x0020CDE0 File Offset: 0x0020BDE0
	public override void ᜄ(int A_0, int A_1, int A_2)
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
		this.ᜁ(A_0, A_1, A_2);
	}

	// Token: 0x06003A50 RID: 14928 RVA: 0x0020CE24 File Offset: 0x0020BE24
	public new IXLSRange ᜀ()
	{
		int a_ = 6;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("渻弽⸿╁⅃", a_));
	}

	// Token: 0x06003A51 RID: 14929 RVA: 0x0020CE7C File Offset: 0x0020BE7C
	public new void ᜀ(IXLSRange A_0)
	{
		int a_ = 1;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("收堸唺娼娾", a_));
	}
}
