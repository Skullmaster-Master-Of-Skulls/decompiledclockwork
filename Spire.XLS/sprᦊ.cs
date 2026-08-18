using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000331 RID: 817
[spr\u2400(FormulaToken.tRef2)]
[spr\u2400(FormulaToken.tRef1)]
[spr\u2400(FormulaToken.tRef3)]
internal class sprᦊ : Ptg, spr\u2590, spr\u1CD5
{
	// Token: 0x06003212 RID: 12818 RVA: 0x001CDE24 File Offset: 0x001CCE24
	public sprᦊ()
	{
	}

	// Token: 0x06003213 RID: 12819 RVA: 0x001CDE38 File Offset: 0x001CCE38
	public sprᦊ(string A_0)
	{
		int a_ = 10;
		base..ctor();
		Match match = FormulaUtil.CellRegex.Match(A_0);
		string value = match.Groups[RecordTableEnumerator.b("̿ⵁ⡃㍅╇⑉絋", a_)].Value;
		string value2 = match.Groups[RecordTableEnumerator.b("ሿⵁ㍃睅", a_)].Value;
		this.ᜀ(value, value2);
		this.TokenCode = FormulaToken.tRef2;
	}

	// Token: 0x06003214 RID: 12820 RVA: 0x001CDEB0 File Offset: 0x001CCEB0
	public sprᦊ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003215 RID: 12821 RVA: 0x001CDEC8 File Offset: 0x001CCEC8
	public sprᦊ(int A_0, int A_1, byte A_2)
	{
		this.ᜈ = A_0;
		this.ᜉ = A_1;
		this.ᜊ = A_2;
	}

	// Token: 0x06003216 RID: 12822 RVA: 0x001CDEF0 File Offset: 0x001CCEF0
	public sprᦊ(int A_0, int A_1, string A_2, string A_3, bool A_4)
	{
		this.ᜀ(A_0, A_1, A_2, A_3, A_4);
	}

	// Token: 0x06003217 RID: 12823 RVA: 0x001CDF10 File Offset: 0x001CCF10
	public sprᦊ(sprᦊ A_0)
	{
		this.ᜈ = A_0.ᜇ();
		this.ᜉ = A_0.ᜉ;
		this.ᜊ = A_0.ᜊ;
	}

	// Token: 0x06003218 RID: 12824 RVA: 0x001CDF48 File Offset: 0x001CCF48
	public virtual int ᜇ()
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

	// Token: 0x06003219 RID: 12825 RVA: 0x001CDF8C File Offset: 0x001CCF8C
	public virtual void ᜂ(int A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x0600321A RID: 12826 RVA: 0x001CDFD0 File Offset: 0x001CCFD0
	public virtual bool ᜃ()
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
		return sprᦊ.ᜀ(this.ᜊ, 128);
	}

	// Token: 0x0600321B RID: 12827 RVA: 0x001CE01C File Offset: 0x001CD01C
	public virtual void ᜀ(bool A_0)
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
		this.ᜊ = sprᦊ.ᜀ(this.ᜊ, 128, A_0);
	}

	// Token: 0x0600321C RID: 12828 RVA: 0x001CE070 File Offset: 0x001CD070
	public virtual bool ᜅ()
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
		return sprᦊ.ᜀ(this.ᜊ, 64);
	}

	// Token: 0x0600321D RID: 12829 RVA: 0x001CE0B8 File Offset: 0x001CD0B8
	public virtual void ᜁ(bool A_0)
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
		this.ᜊ = sprᦊ.ᜀ(this.ᜊ, 64, A_0);
	}

	// Token: 0x0600321E RID: 12830 RVA: 0x001CE108 File Offset: 0x001CD108
	public virtual int ᜆ()
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
		return this.ᜉ;
	}

	// Token: 0x0600321F RID: 12831 RVA: 0x001CE14C File Offset: 0x001CD14C
	public virtual void ᜃ(int A_0)
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
		this.ᜉ = A_0;
	}

	// Token: 0x06003220 RID: 12832 RVA: 0x001CE190 File Offset: 0x001CD190
	protected byte ᜊ()
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
		return this.ᜊ;
	}

	// Token: 0x06003221 RID: 12833 RVA: 0x001CE1D4 File Offset: 0x001CD1D4
	protected void ᜀ(byte A_0)
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
		this.ᜊ = A_0;
	}

	// Token: 0x06003222 RID: 12834 RVA: 0x001CE218 File Offset: 0x001CD218
	public virtual int ᜁ(ExcelVersion A_0)
	{
		int a_ = 6;
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_7E;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7E;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					switch (A_0)
					{
					case ExcelVersion.Version97to2003:
						return 5;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						return 10;
					default:
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
		return 10;
		IL_7E:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䨻嬽㈿ㅁⵃ⥅♇", a_));
	}

	// Token: 0x06003223 RID: 12835 RVA: 0x001CE2B8 File Offset: 0x001CD2B8
	public virtual string ᜀ()
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
		return sprᦊ.ᜀ(0, 0, this.ᜈ, this.ᜉ, this.ᜃ(), this.ᜅ(), false);
	}

	// Token: 0x06003224 RID: 12836 RVA: 0x001CE314 File Offset: 0x001CD314
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
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
		return sprᦊ.ᜀ(A_1, A_2, this.ᜇ(), this.ᜆ(), this.ᜃ(), this.ᜅ(), A_3);
	}

	// Token: 0x06003225 RID: 12837 RVA: 0x001CE370 File Offset: 0x001CD370
	public virtual byte[] ᜀ(ExcelVersion A_0)
	{
		int a_ = 4;
		byte[] array;
		int num;
		for (;;)
		{
			array = base.ToByteArray(A_0);
			num = 1;
			int num2 = 4;
			for (;;)
			{
				IL_0B:
				switch (num2)
				{
				case 0:
					goto IL_1A0;
				case 1:
					if (A_0 == ExcelVersion.Version2010)
					{
						num2 = 8;
						continue;
					}
					goto IL_D2;
				case 2:
					goto IL_170;
				case 3:
					num2 = 6;
					continue;
				case 4:
					if (A_0 == ExcelVersion.Version97to2003)
					{
						num2 = 5;
						continue;
					}
					if (true)
					{
					}
					num2 = 12;
					continue;
				case 5:
					num2 = 10;
					continue;
				case 6:
					if (this.ᜉ > 255)
					{
						num2 = 9;
						continue;
					}
					goto IL_170;
				case 7:
					goto IL_16E;
				case 8:
					goto IL_12D;
				case 9:
					goto IL_1A2;
				case 10:
					while (this.ᜈ <= 65535)
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
							num2 = 3;
							goto IL_0B;
						}
					}
					goto IL_1A2;
				case 11:
					num2 = 1;
					continue;
				case 12:
					if (A_0 != ExcelVersion.Version2007)
					{
						num2 = 11;
						continue;
					}
					goto IL_12D;
				}
				break;
				IL_12D:
				BitConverter.GetBytes(this.ᜈ).CopyTo(array, num);
				num += 4;
				BitConverter.GetBytes(this.ᜉ).CopyTo(array, num);
				num += 4;
				num2 = 7;
				continue;
				IL_170:
				BitConverter.GetBytes((ushort)this.ᜈ).CopyTo(array, num);
				num += 2;
				array[num] = (byte)this.ᜉ;
				num++;
				num2 = 0;
				continue;
				IL_1A2:
				FormulaToken formulaToken = this.ᜂ();
				array[0] = (byte)formulaToken;
				num2 = 2;
			}
		}
		IL_D2:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䰹夻䰽㌿⭁⭃⡅", a_));
		IL_16E:
		IL_1A0:
		array[num] = this.ᜊ;
		return array;
	}

	// Token: 0x06003226 RID: 12838 RVA: 0x001CE544 File Offset: 0x001CD544
	protected void ᜀ(int A_0, int A_1, string A_2, string A_3, bool A_4)
	{
		if (A_4)
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
			this.ᜀ(A_0, A_1, A_3, A_2);
			return;
		}
		this.ᜀ(A_3, A_2);
	}

	// Token: 0x06003227 RID: 12839 RVA: 0x001CE59C File Offset: 0x001CD59C
	protected void ᜀ(string A_0, string A_1)
	{
		int a_ = 13;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B8;
			case 1:
				goto IL_100;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
			case 4:
				goto IL_56;
			case 5:
				goto IL_9B;
			case 6:
				if (A_0.Length == 0)
				{
					num = 1;
					continue;
				}
				goto IL_116;
			case 7:
				if (A_1.Length == 0)
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_102;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			}
			if (A_1 == null)
			{
				num = 4;
			}
			else
			{
				num = 7;
			}
		}
		IL_56:
		throw new ArgumentNullException(RecordTableEnumerator.b("あㅄ㕆ᭈ⑊㩌", a_));
		IL_9B:
		throw new ArgumentNullException(RecordTableEnumerator.b("あㅄ㕆ੈ⑊⅌㩎㱐㵒", a_));
		IL_B8:
		goto IL_102;
		IL_100:
		throw new ArgumentException(RecordTableEnumerator.b("あㅄ㕆ੈ⑊⅌㩎㱐㵒畔穖祘⡚⥜ⵞࡠൢɤ䝦੨੪ͬŮṰݲ啴ᕶᱸ孺᡼ቾﲄ", a_));
		IL_102:
		throw new ArgumentException(RecordTableEnumerator.b("あㅄ㕆ᭈ⑊㩌潎籐獒♔⍖⭘㉚㍜㡞䅠bѤ०ݨѪᥬ佮፰ᙲ啴ቶᑸ୺ॼپ", a_));
		IL_116:
		bool a_2;
		this.ᜉ = sprᦊ.ᜁ(0, A_0, false, out a_2);
		this.ᜁ(a_2);
		this.ᜈ = sprᦊ.ᜀ(0, A_1, false, out a_2);
		this.ᜀ(a_2);
	}

	// Token: 0x06003228 RID: 12840 RVA: 0x001CE6F0 File Offset: 0x001CD6F0
	protected void ᜀ(int A_0, int A_1, string A_2, string A_3)
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
		bool a_;
		this.ᜉ = sprᦊ.ᜀ(A_1, A_2, out a_);
		this.ᜁ(a_);
		this.ᜈ = sprᦊ.ᜀ(A_0, A_3, out a_);
		this.ᜀ(a_);
	}

	// Token: 0x06003229 RID: 12841 RVA: 0x001CE758 File Offset: 0x001CD758
	public static int ᜀ(int A_0, string A_1, out bool A_2)
	{
		int num3;
		for (;;)
		{
			A_2 = false;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 < 2)
					{
						num = 3;
						continue;
					}
					A_1 = A_1.Substring(1);
					num2--;
					num = 5;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7F;
					default:
					{
						if (false)
						{
						}
						int num2;
						num2 -= 2;
						A_1 = A_1.Substring(1, num2);
						A_2 = true;
						num = 6;
						continue;
					}
					}
					break;
				case 2:
					return -1;
				case 3:
					goto IL_DB;
				case 4:
					goto IL_104;
				case 5:
					if (A_1[0] == '[')
					{
						num = 7;
						continue;
					}
					goto IL_E3;
				case 6:
					goto IL_E3;
				case 7:
					num = 10;
					continue;
				case 8:
					if (!A_2)
					{
						num = 4;
						continue;
					}
					goto IL_13E;
				case 9:
				{
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					int num2 = A_1.Length;
					num = 0;
					continue;
				}
				case 10:
				{
					int num2;
					if (A_1[num2 - 1] == ']')
					{
						goto IL_7F;
					}
					goto IL_E3;
				}
				}
				break;
				IL_7F:
				if (true)
				{
				}
				num = 1;
				continue;
				IL_E3:
				num3 = int.Parse(A_1);
				num = 8;
			}
		}
		return -1;
		IL_DB:
		A_2 = true;
		return A_0;
		IL_104:
		return num3 - 1;
		IL_13E:
		return A_0 + num3;
	}

	// Token: 0x0600322A RID: 12842 RVA: 0x001CE8A8 File Offset: 0x001CD8A8
	public virtual Ptg ᜀ(int A_0, int A_1, XlsWorkbook A_2)
	{
		switch (0)
		{
		default:
		{
			sprᦊ sprᦊ;
			int num2;
			int num5;
			for (;;)
			{
				sprᦊ = (sprᦊ)base.Offset(A_0, A_1, A_2);
				int num = 2;
				for (;;)
				{
					int num3;
					int num4;
					switch (num)
					{
					case 0:
						num = 5;
						continue;
					case 1:
						if (num2 <= A_2.MaxRowCount - 1)
						{
							num = 15;
							continue;
						}
						goto IL_82;
					case 2:
						if (!this.ᜃ())
						{
							num = 0;
							continue;
						}
						num = 3;
						continue;
					case 3:
						num3 = this.ᜇ() + A_0;
						goto IL_C7;
					case 4:
						num4 = this.ᜆ();
						goto IL_103;
					case 5:
						num3 = this.ᜇ();
						goto IL_C7;
					case 6:
						num = 4;
						continue;
					case 7:
						if (num2 >= 0)
						{
							num = 10;
							continue;
						}
						goto IL_82;
					case 8:
						if (num5 > A_2.MaxColumnCount - 1)
						{
							num = 12;
							continue;
						}
						goto IL_1D9;
					case 9:
						if (num5 < 0)
						{
							goto IL_82;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D4;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					case 10:
						num = 1;
						continue;
					case 11:
						goto IL_D4;
					case 12:
						goto IL_14E;
					case 13:
						num4 = this.ᜆ() + A_1;
						goto IL_103;
					case 14:
						num = 8;
						continue;
					case 15:
						num = 9;
						continue;
					}
					break;
					IL_D4:
					if (!this.ᜅ())
					{
						num = 6;
						continue;
					}
					num = 13;
					continue;
					IL_C7:
					num2 = num3;
					num = 11;
					continue;
					IL_103:
					num5 = num4;
					num = 7;
				}
			}
			IL_82:
			FormulaToken a_ = this.ᜂ();
			return FormulaUtil.ᜀ(a_, this.ToString(A_2.FormulaUtil), A_2);
			IL_14E:
			goto IL_82;
			IL_1D9:
			if (true)
			{
			}
			sprᦊ.ᜂ(num2);
			sprᦊ.ᜃ(num5);
			return sprᦊ;
		}
		}
	}

	// Token: 0x0600322B RID: 12843 RVA: 0x001CEAA8 File Offset: 0x001CDAA8
	public virtual Ptg ᜀ(int A_0, int A_1, int A_2, int A_3, Rectangle A_4, int A_5, Rectangle A_6, out bool A_7, XlsWorkbook A_8)
	{
		switch (0)
		{
		default:
		{
			sprᦊ sprᦊ;
			int num;
			int num2;
			int num3;
			int num4;
			for (;;)
			{
				A_7 = false;
				sprᦊ = (sprᦊ)base.Offset(A_0, A_1, A_2, A_3, A_4, A_5, A_6, out A_7, A_8);
				num = A_6.Top - A_4.Top;
				num2 = A_6.Left - A_4.Left;
				num3 = this.ᜇ();
				num4 = this.ᜆ();
				if (true)
				{
				}
				int num5 = 5;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						num5 = 2;
						continue;
					case 1:
						goto IL_156;
					case 2:
						if (!Ptg.RectangleContains(A_6, A_1, A_2))
						{
							return sprᦊ;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FF;
						default:
							if (false)
							{
							}
							num5 = 7;
							continue;
						}
						break;
					case 3:
						goto IL_1D1;
					case 4:
						if (A_3 != A_5)
						{
							num5 = 0;
							continue;
						}
						return sprᦊ;
					case 5:
						if (A_0 == A_3)
						{
							num5 = 10;
							continue;
						}
						num5 = 11;
						continue;
					case 6:
						goto IL_FF;
					case 7:
						goto IL_1A1;
					case 8:
						if (Ptg.RectangleContains(A_4, num3, num4))
						{
							num5 = 1;
							continue;
						}
						num5 = 9;
						continue;
					case 9:
						if (Ptg.RectangleContains(A_6, num3, num4))
						{
							num5 = 3;
							continue;
						}
						return sprᦊ;
					case 10:
						num5 = 8;
						continue;
					case 11:
						if (A_0 == A_5)
						{
							num5 = 6;
							continue;
						}
						return sprᦊ;
					}
					break;
					IL_FF:
					num5 = 4;
				}
			}
			IL_156:
			num3 += num;
			num4 += num2;
			return sprᦊ.ᜀ(A_0, A_5, num3, num4, ref A_7, A_8);
			IL_1A1:
			A_7 = true;
			return this.ᜀ(sprᦊ, A_3, A_4, A_5, num, num2, A_8);
			IL_1D1:
			return this.ᜉ();
		}
		}
	}

	// Token: 0x0600322C RID: 12844 RVA: 0x001CEC8C File Offset: 0x001CDC8C
	protected virtual Ptg ᜀ(sprᦊ A_0, int A_1, Rectangle A_2, int A_3, int A_4, int A_5, XlsWorkbook A_6)
	{
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			int num3;
			for (;;)
			{
				num = A_1;
				bool flag = this.ᜀ(A_2);
				num2 = A_0.ᜇ();
				num3 = A_0.ᜆ();
				int num4 = 0;
				for (;;)
				{
					switch (num4)
					{
					case 0:
						if (!flag)
						{
							goto IL_9B;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_99;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num4 = 1;
							continue;
						}
						break;
					case 1:
						num = A_3;
						num2 += A_4;
						num3 += A_5;
						num4 = 2;
						continue;
					case 2:
						goto IL_99;
					}
					break;
				}
			}
			IL_99:
			IL_9B:
			FormulaToken a_ = sprᣋ.ᜀ(this.ᜄ());
			return A_0 = (sprᦊ)FormulaUtil.ᜀ(a_, new object[]
			{
				num,
				num2,
				num3,
				this.ᜊ
			});
		}
		}
	}

	// Token: 0x0600322D RID: 12845 RVA: 0x001CED88 File Offset: 0x001CDD88
	private bool ᜀ(Rectangle A_0)
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
		int iRow = this.ᜇ();
		int iColumn = this.ᜆ();
		return Ptg.RectangleContains(A_0, iRow, iColumn);
	}

	// Token: 0x0600322E RID: 12846 RVA: 0x001CEDDC File Offset: 0x001CDDDC
	public virtual int ᜄ()
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
		return sprᦊ.ᜀ(this.TokenCode);
	}

	// Token: 0x0600322F RID: 12847 RVA: 0x001CEE24 File Offset: 0x001CDE24
	public virtual FormulaToken ᜂ()
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
		int a_ = this.ᜄ();
		return spr\u23C7.ᜀ(a_);
	}

	// Token: 0x06003230 RID: 12848 RVA: 0x001CEE6C File Offset: 0x001CDE6C
	private Ptg ᜀ(int A_0, int A_1, int A_2, int A_3, ref bool A_4, XlsWorkbook A_5)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_105;
				default:
					if (false)
					{
					}
					if (A_3 > A_5.MaxColumnCount - 1)
					{
						num = 5;
						continue;
					}
					num = 8;
					continue;
				}
				break;
			case 1:
				num = 0;
				continue;
			case 2:
				return this;
			case 3:
				this.ᜂ(A_2);
				this.ᜃ(A_3);
				A_4 = true;
				num = 2;
				continue;
			case 5:
				goto IL_176;
			case 6:
				num = 10;
				continue;
			case 7:
				if (A_3 >= 0)
				{
					num = 1;
					continue;
				}
				goto IL_66;
			case 8:
				if (A_0 == A_1)
				{
					num = 3;
					continue;
				}
				goto IL_97;
			case 9:
				goto IL_105;
			case 10:
				if (A_2 <= A_5.MaxRowCount - 1)
				{
					num = 9;
					continue;
				}
				goto IL_66;
			}
			if (A_2 >= 0)
			{
				num = 6;
				continue;
			}
			break;
			IL_105:
			num = 7;
		}
		IL_66:
		FormulaToken a_ = this.ᜂ();
		return FormulaUtil.ᜀ(a_, this.ToString());
		IL_97:
		A_4 = true;
		FormulaToken a_2 = sprᣋ.ᜀ(this.ᜄ());
		return FormulaUtil.ᜀ(a_2, new object[]
		{
			A_1,
			A_2,
			A_3,
			this.ᜊ
		});
		IL_176:
		if (true)
		{
		}
		goto IL_66;
	}

	// Token: 0x06003231 RID: 12849 RVA: 0x001CF000 File Offset: 0x001CE000
	public virtual Ptg ᜀ(IWorkbook A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			sprᦈ sprᦈ;
			for (;;)
			{
				int a_;
				int a_2;
				int num2;
				int num3;
				switch (num)
				{
				case 1:
					num = 8;
					continue;
				case 2:
				{
					if (A_0.Version == ExcelVersion.Version97to2003)
					{
						num = 10;
						continue;
					}
					sprᦊ sprᦊ = sprᦈ;
					sprᦊ.ᜂ(a_);
					sprᦊ.ᜃ(a_2);
					num = 5;
					continue;
				}
				case 3:
					goto IL_D3;
				case 4:
					num2 = this.ᜇ() - A_1;
					goto IL_143;
				case 5:
					goto IL_B4;
				case 6:
					if (!this.ᜃ())
					{
						num = 7;
						continue;
					}
					num = 4;
					continue;
				case 7:
					num = 9;
					continue;
				case 8:
					num3 = this.ᜆ();
					goto IL_100;
				case 9:
					num2 = this.ᜇ();
					goto IL_143;
				case 10:
					goto IL_B9;
				case 11:
					num3 = this.ᜆ() - A_2;
					goto IL_100;
				}
				if (!this.ᜅ())
				{
					num = 1;
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
					num = 11;
					continue;
				}
				IL_B9:
				sprᦈ.ᜂ(a_);
				sprᦈ.ᜃ(a_2);
				num = 3;
				continue;
				IL_100:
				a_2 = num3;
				num = 6;
				continue;
				IL_143:
				a_ = num2;
				FormulaToken a_3 = sprᦈ.ᜀ(this.ᜄ());
				sprᦈ = (sprᦈ)FormulaUtil.ᜁ(a_3);
				if (true)
				{
				}
				num = 2;
			}
			IL_B4:
			IL_D3:
			sprᦈ.ᜀ(this.ᜊ());
			return sprᦈ;
		}
		}
	}

	// Token: 0x06003232 RID: 12850 RVA: 0x001CF1A8 File Offset: 0x001CE1A8
	public static bool ᜀ(byte A_0, byte A_1)
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
		return (A_0 & A_1) != 0;
	}

	// Token: 0x06003233 RID: 12851 RVA: 0x001CF1EC File Offset: 0x001CE1EC
	public static byte ᜀ(byte A_0, byte A_1, bool A_2)
	{
		for (;;)
		{
			A_0 &= ~A_1;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return A_0;
				case 1:
					if (A_2)
					{
						num = 2;
						continue;
					}
					return A_0;
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
						if (true)
						{
						}
						A_0 += A_1;
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		return A_0;
	}

	// Token: 0x06003234 RID: 12852 RVA: 0x001CF26C File Offset: 0x001CE26C
	[CLSCompliant(false)]
	public static string ᜀ(int A_0, int A_1, int A_2, int A_3, bool A_4, bool A_5, bool A_6)
	{
		if (!A_6)
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
				return sprᦊ.ᜀ(A_3, A_2, A_5, A_4);
			}
		}
		return sprᦊ.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5);
	}

	// Token: 0x06003235 RID: 12853 RVA: 0x001CF2C8 File Offset: 0x001CE2C8
	private static string ᜀ(int A_0, int A_1, bool A_2, bool A_3)
	{
		int a_ = 15;
		string text;
		string str;
		for (;;)
		{
			if (true)
			{
			}
			text = sprṔ.ᜀ(A_0 + 1);
			str = (A_1 + 1).ToString();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_97;
				case 1:
					if (!A_2)
					{
						goto IL_A7;
					}
					goto IL_D6;
				case 2:
					str = RecordTableEnumerator.b("慄", a_) + str;
					num = 5;
					continue;
				case 3:
					text = RecordTableEnumerator.b("慄", a_) + text;
					num = 0;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A7;
					default:
						if (false)
						{
						}
						if (!A_3)
						{
							num = 2;
							continue;
						}
						goto IL_99;
					}
					break;
				case 5:
					goto IL_99;
				}
				break;
				IL_99:
				num = 1;
				continue;
				IL_A7:
				num = 3;
			}
		}
		IL_97:
		IL_D6:
		return text + str;
	}

	// Token: 0x06003236 RID: 12854 RVA: 0x001CF3BC File Offset: 0x001CE3BC
	public static string ᜀ(int A_0, int A_1)
	{
		int a_ = 0;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("搵", a_) + ((A_1 == 0) ? "" : (RecordTableEnumerator.b("洵", a_) + A_1 + RecordTableEnumerator.b("欵", a_))) + RecordTableEnumerator.b("电", a_) + ((A_0 == 0) ? "" : (RecordTableEnumerator.b("洵", a_) + A_0 + RecordTableEnumerator.b("欵", a_)));
	}

	// Token: 0x06003237 RID: 12855 RVA: 0x001CF48C File Offset: 0x001CE48C
	public static int ᜁ(int A_0, string A_1, bool A_2, out bool A_3)
	{
		if (!A_2)
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
				return sprᦊ.ᜁ(A_1, out A_3);
			}
		}
		return sprᦊ.ᜀ(A_0, A_1, out A_3);
	}

	// Token: 0x06003238 RID: 12856 RVA: 0x001CF4DC File Offset: 0x001CE4DC
	public static int ᜁ(string A_0, out bool A_1)
	{
		int a_ = 19;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_FE;
			case 2:
				goto IL_43;
			case 3:
				goto IL_DB;
			case 4:
				if (A_0.Length == 0)
				{
					num = 1;
					continue;
				}
				A_1 = (A_0[0] != '$');
				num = 5;
				continue;
			case 5:
				if (A_1)
				{
					goto IL_100;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B3;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 6:
				A_0 = A_0.Substring(1);
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 4;
			}
		}
		IL_43:
		if (true)
		{
		}
		IL_B3:
		throw new ArgumentNullException(RecordTableEnumerator.b("⩈⑊⅌㩎㱐㵒᭔㙖㑘㹚", a_));
		IL_DB:
		goto IL_100;
		IL_FE:
		throw new ArgumentException(RecordTableEnumerator.b("⩈⑊⅌㩎㱐㵒᭔㙖㑘㹚絜牞䅠ၢᅤᕦhժ੬佮ተቲ᭴᥶ᙸེ嵼ᵾꎂ麗ﾊꆎ", a_));
		IL_100:
		return sprṔ.ᜀ(A_0) - 1;
	}

	// Token: 0x06003239 RID: 12857 RVA: 0x001CF5F4 File Offset: 0x001CE5F4
	public static int ᜀ(int A_0, string A_1, bool A_2, out bool A_3)
	{
		if (!A_2)
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
				return sprᦊ.ᜀ(A_1, out A_3);
			}
		}
		return sprᦊ.ᜀ(A_0, A_1, out A_3);
	}

	// Token: 0x0600323A RID: 12858 RVA: 0x001CF644 File Offset: 0x001CE644
	public static int ᜀ(string A_0, out bool A_1)
	{
		int a_ = 2;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_DB;
			case 2:
				if (A_0.Length == 0)
				{
					num = 4;
					continue;
				}
				A_1 = (A_0[0] != '$');
				num = 5;
				continue;
			case 3:
				goto IL_43;
			case 4:
				goto IL_FE;
			case 5:
				if (A_1)
				{
					goto IL_100;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B3;
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
				break;
			case 6:
				A_0 = A_0.Substring(1);
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 2;
			}
		}
		IL_43:
		IL_B3:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬷丹主氽⼿㕁੃❅╇⽉", a_));
		IL_DB:
		goto IL_100;
		IL_FE:
		throw new ArgumentException(RecordTableEnumerator.b("䬷丹主氽⼿㕁੃❅╇⽉汋捍灏⅑⁓⑕ㅗ㑙㭛繝͟͡੣ࡥݧṩ䱫౭ᕯ剱ᅳ᭵ࡷ๹ջ偽", a_));
		IL_100:
		return int.Parse(A_0) - 1;
	}

	// Token: 0x0600323B RID: 12859 RVA: 0x001CF75C File Offset: 0x001CE75C
	public static FormulaToken ᜀ(int A_0)
	{
		int a_ = 7;
		for (;;)
		{
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case 1:
						return FormulaToken.tRef1;
					case 2:
						return FormulaToken.tRef2;
					case 3:
						return FormulaToken.tRef3;
					default:
						num = 1;
						continue;
					}
					break;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return FormulaToken.tRef1;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_81;
				}
				break;
			}
		}
		return FormulaToken.tRef2;
		IL_81:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吼儾╀♂㵄", a_));
	}

	// Token: 0x0600323C RID: 12860 RVA: 0x001CF804 File Offset: 0x001CE804
	public static int ᜀ(FormulaToken A_0)
	{
		int a_ = 14;
		for (;;)
		{
			int num = 15;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 13;
					continue;
				case 1:
					goto IL_75;
				case 2:
					num = 12;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A2;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (A_0 != FormulaToken.tRef1)
						{
							num = 8;
							continue;
						}
						return 1;
					}
					break;
				case 4:
					goto IL_170;
				case 5:
					num = 1;
					continue;
				case 6:
					if (A_0 != FormulaToken.tRefErr2)
					{
						num = 2;
						continue;
					}
					return 2;
				case 7:
					if (A_0 != FormulaToken.tRef2)
					{
						num = 14;
						continue;
					}
					return 2;
				case 8:
					num = 10;
					continue;
				case 9:
					num = 7;
					continue;
				case 10:
					goto IL_A2;
				case 11:
					num = 3;
					continue;
				case 12:
					if (A_0 != FormulaToken.tRef3)
					{
						num = 0;
						continue;
					}
					return 3;
				case 13:
					if (A_0 != FormulaToken.tRefErr3)
					{
						num = 5;
						continue;
					}
					return 3;
				case 14:
					num = 4;
					continue;
				case 15:
					if (A_0 <= FormulaToken.tRef2)
					{
						num = 11;
						continue;
					}
					num = 6;
					continue;
				}
				break;
				IL_A2:
				if (A_0 == FormulaToken.tRefErr1)
				{
					return 1;
				}
				num = 9;
			}
		}
		IL_75:
		IL_170:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⵃ⡅ⱇ⽉㑋", a_));
	}

	// Token: 0x0600323D RID: 12861 RVA: 0x001CF998 File Offset: 0x001CE998
	public static string ᜀ(int A_0, int A_1, int A_2, int A_3, bool A_4, bool A_5)
	{
		int a_ = 17;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return sprᦊ.ᜀ(A_0, RecordTableEnumerator.b("ᕆ", a_), A_2, A_4) + sprᦊ.ᜀ(A_1, RecordTableEnumerator.b("ц", a_), A_3, A_5);
	}

	// Token: 0x0600323E RID: 12862 RVA: 0x001CFA10 File Offset: 0x001CEA10
	public static string ᜀ(int A_0, string A_1, int A_2, bool A_3)
	{
		int a_ = 18;
		int num = 3;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_3)
				{
					num = 8;
					continue;
				}
				return text;
			case 1:
				if (A_2 == 0)
				{
					num = 12;
					continue;
				}
				goto IL_69;
			case 2:
				if (true)
				{
				}
				text += '[';
				num = 7;
				continue;
			case 4:
				goto IL_58;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_F9;
				default:
					if (false)
					{
					}
					A_2 -= A_0;
					num = 1;
					continue;
				}
				break;
			case 6:
				goto IL_114;
			case 7:
				goto IL_F9;
			case 8:
				text += ']';
				num = 6;
				continue;
			case 9:
				if (A_3)
				{
					num = 2;
					continue;
				}
				A_2++;
				num = 10;
				continue;
			case 10:
				goto IL_A0;
			case 11:
				if (A_3)
				{
					num = 5;
					continue;
				}
				goto IL_69;
			case 12:
				return A_1;
			}
			if (A_1 == null)
			{
				num = 4;
				continue;
			}
			num = 11;
			continue;
			IL_69:
			text = A_1;
			num = 9;
			continue;
			IL_A0:
			text += A_2.ToString();
			num = 0;
			continue;
			IL_F9:
			goto IL_A0;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋ᵍ⑏㍑♓≕", a_));
		IL_114:
		return text;
	}

	// Token: 0x0600323F RID: 12863 RVA: 0x001CFB80 File Offset: 0x001CEB80
	public IXLSRange ᜀ(IWorkbook A_0, IWorksheet A_1)
	{
		int a_ = 4;
		if (A_1 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻嬽┿㙁", a_));
			}
		}
		if (true)
		{
		}
		return A_1.Range[this.ᜈ + 1, this.ᜉ + 1];
	}

	// Token: 0x06003240 RID: 12864 RVA: 0x001CFBF8 File Offset: 0x001CEBF8
	public Rectangle ᜈ()
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
		return Rectangle.FromLTRB(this.ᜆ(), this.ᜇ(), this.ᜆ(), this.ᜇ());
	}

	// Token: 0x06003241 RID: 12865 RVA: 0x001CFC50 File Offset: 0x001CEC50
	public Ptg ᜁ(Rectangle A_0)
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
		sprᦊ sprᦊ = (sprᦊ)base.Clone();
		sprᦊ.ᜃ(A_0.Left);
		sprᦊ.ᜂ(A_0.Top);
		return sprᦊ;
	}

	// Token: 0x06003242 RID: 12866 RVA: 0x001CFCB4 File Offset: 0x001CECB4
	public Ptg ᜁ(int A_0)
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
		int a_ = sprᦊ.ᜀ(this.TokenCode);
		FormulaToken tokenCode = sprᣋ.ᜀ(a_);
		return new sprᣋ(A_0, this.ᜇ(), this.ᜆ(), this.ᜊ())
		{
			TokenCode = tokenCode
		};
	}

	// Token: 0x06003243 RID: 12867 RVA: 0x001CFD24 File Offset: 0x001CED24
	public virtual Ptg ᜉ()
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
		FormulaToken a_ = this.ᜂ();
		return FormulaUtil.ᜀ(a_, new object[]
		{
			this
		});
	}

	// Token: 0x06003244 RID: 12868 RVA: 0x001CFD78 File Offset: 0x001CED78
	public virtual void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7A;
			case 1:
				if (A_2 == ExcelVersion.Version2010)
				{
					num = 0;
					continue;
				}
				goto IL_112;
			case 2:
				goto IL_84;
			case 3:
				if (true)
				{
				}
				num = 1;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_84;
				default:
					goto IL_49;
				}
				break;
			}
			if (A_2 == ExcelVersion.Version97to2003)
			{
				num = 5;
				continue;
			}
			num = 2;
			continue;
			IL_84:
			if (A_2 == ExcelVersion.Version2007)
			{
				goto IL_D4;
			}
			num = 3;
		}
		IL_49:
		if (false)
		{
		}
		this.ᜈ = (int)A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜉ = (int)A_0.ReadByte(A_1++);
		this.ᜊ = A_0.ReadByte(A_1++);
		return;
		IL_7A:
		IL_D4:
		this.ᜈ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜉ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜊ = A_0.ReadByte(A_1++);
		return;
		IL_112:
		throw new NotImplementedException();
	}

	// Token: 0x040015F5 RID: 5621
	public const byte ᜀ = 128;

	// Token: 0x040015F6 RID: 5622
	public const byte ᜁ = 64;

	// Token: 0x040015F7 RID: 5623
	private const char ᜂ = '[';

	// Token: 0x040015F8 RID: 5624
	private const char ᜃ = ']';

	// Token: 0x040015F9 RID: 5625
	public const string ᜄ = "R";

	// Token: 0x040015FA RID: 5626
	public const string ᜅ = "C";

	// Token: 0x040015FB RID: 5627
	public const char ᜆ = '[';

	// Token: 0x040015FC RID: 5628
	public const char ᜇ = ']';

	// Token: 0x040015FD RID: 5629
	private int ᜈ;

	// Token: 0x040015FE RID: 5630
	private int ᜉ;

	// Token: 0x040015FF RID: 5631
	private byte ᜊ;
}
