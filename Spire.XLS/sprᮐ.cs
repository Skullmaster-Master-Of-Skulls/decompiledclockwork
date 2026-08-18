using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000381 RID: 897
[DefaultMember("Item")]
internal class sprᮐ
{
	// Token: 0x0600368F RID: 13967 RVA: 0x001ED728 File Offset: 0x001EC728
	public Rectangle ᜀ(int A_0)
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
		return this.ᜀ[A_0];
	}

	// Token: 0x06003690 RID: 13968 RVA: 0x001ED770 File Offset: 0x001EC770
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
		return this.ᜀ.Count;
	}

	// Token: 0x06003691 RID: 13969 RVA: 0x001ED7B8 File Offset: 0x001EC7B8
	public void ᜁ(int A_0, int A_1)
	{
		int num;
		Rectangle value;
		for (;;)
		{
			num = this.ᜁ();
			bool flag = true;
			value = Rectangle.Empty;
			int num2 = 23;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (value.Top - 1 == A_0)
					{
						num2 = 14;
						continue;
					}
					num2 = 17;
					continue;
				case 1:
					goto IL_172;
				case 2:
					goto IL_1EA;
				case 3:
					if (flag)
					{
						num2 = 19;
						continue;
					}
					goto IL_2C7;
				case 4:
					if (value.Right + 1 == A_1)
					{
						num2 = 6;
						continue;
					}
					flag = true;
					num2 = 2;
					continue;
				case 5:
					goto IL_1EA;
				case 6:
					value.Width++;
					num2 = 15;
					continue;
				case 7:
					goto IL_1EA;
				case 8:
					flag = false;
					num2 = 13;
					continue;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_172;
					default:
						if (false)
						{
						}
						if (value.Left == A_1)
						{
							num2 = 20;
							continue;
						}
						goto IL_2A0;
					}
					break;
				case 10:
					num2 = 9;
					continue;
				case 11:
					value.Height++;
					num2 = 7;
					continue;
				case 12:
					goto IL_1EA;
				case 13:
					if (value.Left - 1 == A_1)
					{
						num2 = 1;
						continue;
					}
					num2 = 4;
					continue;
				case 14:
					value.Height++;
					value.Y--;
					num2 = 5;
					continue;
				case 15:
					goto IL_1EA;
				case 16:
					if (value.Height == 0)
					{
						num2 = 8;
						continue;
					}
					goto IL_1EA;
				case 17:
					if (value.Bottom + 1 == A_0)
					{
						num2 = 11;
						continue;
					}
					flag = true;
					num2 = 12;
					continue;
				case 18:
					goto IL_1EA;
				case 19:
					goto IL_206;
				case 20:
					flag = false;
					num2 = 0;
					continue;
				case 21:
					if (value.Width == 0)
					{
						num2 = 10;
						continue;
					}
					goto IL_2A0;
				case 22:
					value = this.ᜀ(num - 1);
					if (true)
					{
					}
					num2 = 21;
					continue;
				case 23:
					if (num > 0)
					{
						num2 = 22;
						continue;
					}
					goto IL_1EA;
				}
				break;
				IL_172:
				value.Width++;
				value.X--;
				num2 = 18;
				continue;
				IL_1EA:
				num2 = 3;
				continue;
				IL_2A0:
				num2 = 16;
			}
		}
		IL_206:
		this.ᜀ.Add(sprᮐ.ᜀ(A_0, A_1));
		return;
		IL_2C7:
		this.ᜀ[num - 1] = value;
	}

	// Token: 0x06003692 RID: 13970 RVA: 0x001EDA9C File Offset: 0x001ECA9C
	public void ᜀ()
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
		this.ᜀ.Clear();
	}

	// Token: 0x06003693 RID: 13971 RVA: 0x001EDAE4 File Offset: 0x001ECAE4
	public IXLSRange ᜀ(IWorksheet A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 5;
			IXLSRanges ixlsranges;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_EF;
				case 1:
					goto IL_1AA;
				case 2:
					goto IL_FE;
				case 3:
					goto IL_11A;
				case 4:
					goto IL_5D;
				case 6:
				{
					int count;
					if (count == 0)
					{
						goto IL_E3;
					}
					num = 8;
					continue;
				}
				case 7:
					goto IL_FE;
				case 8:
				{
					int count;
					if (count == 1)
					{
						num = 1;
						continue;
					}
					ixlsranges = ((XlsWorksheet)A_0).ᜮ();
					int num2 = 0;
					num = 7;
					continue;
				}
				case 9:
				{
					int count;
					int num2;
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					Rectangle rectangle = this.ᜀ(num2);
					((XlsRangesCollection)ixlsranges).Add(A_0.AllocatedRange[rectangle.Top, rectangle.Left, rectangle.Bottom, rectangle.Right]);
					num2++;
					num = 2;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
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
					int count = this.ᜀ.Count;
					num = 6;
					continue;
				}
				}
				IL_E3:
				num = 0;
				continue;
				IL_FE:
				num = 9;
			}
			IL_5D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉁╃㑅ⵇ⑉㡋᥍㽏⁑㽓╕し㽙㥛⩝", a_));
			IL_EF:
			return null;
			IL_11A:
			if (true)
			{
			}
			return ixlsranges;
			IL_1AA:
			Rectangle rectangle2 = this.ᜀ(0);
			return A_0[rectangle2.Top, rectangle2.Left, rectangle2.Bottom, rectangle2.Right];
		}
		}
	}

	// Token: 0x06003694 RID: 13972 RVA: 0x001EDCA4 File Offset: 0x001ECCA4
	public static Rectangle ᜀ(int A_0, int A_1)
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
		return Rectangle.FromLTRB(A_1, A_0, A_1, A_0);
	}

	// Token: 0x04001842 RID: 6210
	private List<Rectangle> ᜀ = new List<Rectangle>();
}
