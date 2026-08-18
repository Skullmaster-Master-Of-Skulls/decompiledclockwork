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

// Token: 0x02000335 RID: 821
[spr\u2400(FormulaToken.tRef3d3)]
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tRef3d1)]
[spr\u2400(FormulaToken.tRef3d2)]
internal class sprᣋ : sprᦊ, sprỜ, spr\u2086
{
	// Token: 0x0600324F RID: 12879 RVA: 0x001D03FC File Offset: 0x001CF3FC
	public sprᣋ()
	{
	}

	// Token: 0x06003250 RID: 12880 RVA: 0x001D0410 File Offset: 0x001CF410
	public sprᣋ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003251 RID: 12881 RVA: 0x001D0428 File Offset: 0x001CF428
	public sprᣋ(string A_0, IWorkbook A_1)
	{
		int a_ = 2;
		base..ctor();
		Match match = FormulaUtil.Cell3DRegex.Match(A_0);
		if (!match.Success)
		{
			throw new ArgumentException(RecordTableEnumerator.b("䬷丹主砽⼿ぁ⥃㍅⑇⭉", a_));
		}
		string value = match.Groups[RecordTableEnumerator.b("欷刹夻嬽㐿ు╃⭅ⵇ", a_)].Value;
		string value2 = match.Groups[RecordTableEnumerator.b("樷唹䬻༽", a_)].Value;
		string value3 = match.Groups[RecordTableEnumerator.b("笷唹倻䬽ⴿⱁ畃", a_)].Value;
		base.ᜀ(value3, value2);
		this.ᜀ(value, A_1);
	}

	// Token: 0x06003252 RID: 12882 RVA: 0x001D04DC File Offset: 0x001CF4DC
	public sprᣋ(int A_0, int A_1, int A_2, string A_3, string A_4, bool A_5) : base(A_0, A_1, A_3, A_4, A_5)
	{
		this.ᜀ = (ushort)A_2;
	}

	// Token: 0x06003253 RID: 12883 RVA: 0x001D0500 File Offset: 0x001CF500
	public sprᣋ(int A_0, int A_1, int A_2, byte A_3) : base(A_1, A_2, A_3)
	{
		this.ᜀ = (ushort)A_0;
	}

	// Token: 0x06003254 RID: 12884 RVA: 0x001D0520 File Offset: 0x001CF520
	public sprᣋ(sprᣋ A_0) : base(A_0)
	{
		this.ᜀ = A_0.ᜀ;
	}

	// Token: 0x06003255 RID: 12885 RVA: 0x001D0540 File Offset: 0x001CF540
	public new ushort ᜁ()
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
		return this.ᜀ;
	}

	// Token: 0x06003256 RID: 12886 RVA: 0x001D0584 File Offset: 0x001CF584
	public void ᜂ(ushort A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06003257 RID: 12887 RVA: 0x001D05C8 File Offset: 0x001CF5C8
	public override int ᜁ(ExcelVersion A_0)
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
		return base.ᜁ(A_0) + 2;
	}

	// Token: 0x06003258 RID: 12888 RVA: 0x001D060C File Offset: 0x001CF60C
	public override string ᜀ()
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return string.Concat(new string[]
		{
			RecordTableEnumerator.b("挷根夻堽िⱁ⁃⍅ぇ睉", a_),
			this.ᜀ.ToString(),
			RecordTableEnumerator.b("ᠷ", a_),
			base.ᜀ(),
			RecordTableEnumerator.b("攷", a_)
		});
	}

	// Token: 0x06003259 RID: 12889 RVA: 0x001D06A8 File Offset: 0x001CF6A8
	public override byte[] ᜀ(ExcelVersion A_0)
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
		byte[] array = base.ᜀ(A_0);
		int num = array.Length;
		Buffer.BlockCopy(array, 1, array, 3, num - 3);
		BitConverter.GetBytes(this.ᜀ).CopyTo(array, 1);
		return array;
	}

	// Token: 0x0600325A RID: 12890 RVA: 0x001D0710 File Offset: 0x001CF710
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 14;
		string text;
		for (;;)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					goto IL_3C;
				case 2:
					goto IL_4E;
				case 3:
					if (text == null)
					{
						num = 0;
						continue;
					}
					num = 5;
					continue;
				case 5:
					goto IL_B7;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					text = sprᣋ.ᜀ(A_0.ParentWorkbook, (int)this.ᜀ);
					num = 3;
				}
			}
			IL_3C:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_9F;
			}
		}
		IL_4E:
		string text2 = string.Empty;
		goto IL_DB;
		IL_9F:
		if (false)
		{
		}
		return this.ToString();
		IL_B7:
		text2 = RecordTableEnumerator.b("捃", a_) + text + RecordTableEnumerator.b("捃杅", a_);
		IL_DB:
		text = text2;
		return text + base.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5);
	}

	// Token: 0x0600325B RID: 12891 RVA: 0x001D0810 File Offset: 0x001CF810
	public new string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3)
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
		return base.ToString(A_0, A_1, A_2, A_3);
	}

	// Token: 0x0600325C RID: 12892 RVA: 0x001D0858 File Offset: 0x001CF858
	public override Ptg ᜀ(int A_0, int A_1, int A_2, int A_3, Rectangle A_4, int A_5, Rectangle A_6, out bool A_7, XlsWorkbook A_8)
	{
		sprᣋ sprᣋ;
		for (;;)
		{
			A_7 = false;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_7)
					{
						num = 4;
						continue;
					}
					return sprᣋ;
				case 1:
					goto IL_83;
				case 2:
					if (this.ᜀ == (ushort)A_3)
					{
						num = 1;
						continue;
					}
					goto IL_BC;
				case 3:
					goto IL_81;
				case 4:
					sprᣋ.ᜀ = (ushort)A_5;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_83;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				break;
				IL_83:
				sprᣋ = (sprᣋ)base.ᜀ(A_5, A_1, A_2, A_5, A_4, A_5, A_6, out A_7, A_8);
				num = 0;
			}
		}
		return sprᣋ;
		IL_81:
		return sprᣋ;
		IL_BC:
		return (Ptg)base.Clone();
	}

	// Token: 0x0600325D RID: 12893 RVA: 0x001D092C File Offset: 0x001CF92C
	public override int ᜄ()
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
		return sprᣋ.ᜀ(this.TokenCode);
	}

	// Token: 0x0600325E RID: 12894 RVA: 0x001D0974 File Offset: 0x001CF974
	public override FormulaToken ᜂ()
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
		return spr\u1B37.ᜀ(a_);
	}

	// Token: 0x0600325F RID: 12895 RVA: 0x001D09BC File Offset: 0x001CF9BC
	public new static string ᜀ(IWorkbook A_0, int A_1)
	{
		int a_ = 14;
		int num = 1;
		string sheetNameByReference;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_87;
			case 1:
				if (true)
				{
				}
				break;
			case 2:
				if (sheetNameByReference != null)
				{
					goto IL_8B;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3E;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 3:
				goto IL_3C;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				sheetNameByReference = ((XlsWorkbook)A_0).GetSheetNameByReference(A_1, false);
				num = 2;
			}
		}
		IL_3C:
		return null;
		IL_3E:
		return null;
		IL_87:
		goto IL_3E;
		IL_8B:
		return sheetNameByReference.Replace(RecordTableEnumerator.b("捃", a_), RecordTableEnumerator.b("捃慅", a_));
	}

	// Token: 0x06003260 RID: 12896 RVA: 0x001D0A78 File Offset: 0x001CFA78
	protected new void ᜀ(string A_0, IWorkbook A_1)
	{
		for (;;)
		{
			XlsWorkbook xlsWorkbook = (XlsWorkbook)A_1;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 3;
					continue;
				case 1:
					goto IL_48;
				case 2:
					goto IL_5F;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
						if (false)
						{
						}
						if (A_0[A_0.Length - 1] == '\'')
						{
							num = 2;
							continue;
						}
						goto IL_48;
					}
					break;
				case 4:
					if (A_0[0] == '\'')
					{
						num = 0;
						continue;
					}
					goto IL_48;
				}
				break;
				IL_70:
				num = 1;
				continue;
				IL_5F:
				A_0 = A_0.Substring(1, A_0.Length - 2);
				goto IL_70;
				try
				{
					IL_48:
					this.ᜀ = (ushort)xlsWorkbook.AddSheetReference(A_0);
					return;
				}
				catch (ArgumentException)
				{
					throw new spr\u2313();
				}
				goto IL_5F;
			}
		}
	}

	// Token: 0x06003261 RID: 12897 RVA: 0x001D0B5C File Offset: 0x001CFB5C
	public new static FormulaToken ᜀ(int A_0)
	{
		int a_ = 8;
		for (;;)
		{
			IL_1D:
			for (;;)
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						switch (A_0)
						{
						case 1:
							return FormulaToken.tRef3d1;
						case 2:
							goto IL_4D;
						case 3:
							return FormulaToken.tRef3d3;
						default:
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_81;
					}
					goto IL_1D;
				}
				IL_4D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				goto Block_2;
			}
		}
		Block_2:
		if (false)
		{
		}
		return FormulaToken.tRef3d2;
		IL_81:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圽⸿♁⅃㹅", a_));
	}

	// Token: 0x06003262 RID: 12898 RVA: 0x001D0C04 File Offset: 0x001CFC04
	public new static int ᜀ(FormulaToken A_0)
	{
		int a_ = 10;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_62;
				case 3:
					if (A_0 != FormulaToken.tRef3d1)
					{
						num = 0;
						continue;
					}
					return 1;
				case 4:
					if (A_0 != FormulaToken.tRef3d3)
					{
						num = 1;
						continue;
					}
					return 3;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return 3;
					default:
						if (false)
						{
						}
						if (A_0 != FormulaToken.tRef3d2)
						{
							num = 6;
							continue;
						}
						goto IL_50;
					}
					break;
				case 6:
					num = 4;
					continue;
				}
				break;
			}
		}
		IL_50:
		if (true)
		{
		}
		return 2;
		IL_62:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ⱁ⁃⍅ぇ", a_));
	}

	// Token: 0x06003263 RID: 12899 RVA: 0x001D0CE0 File Offset: 0x001CFCE0
	public new IXLSRange ᜀ(IWorkbook A_0, IWorksheet A_1)
	{
		int a_ = 12;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				XlsWorkbook xlsWorkbook;
				if (xlsWorkbook.IsExternalReference((int)this.ᜀ))
				{
					XlsExternWorksheet a_2 = xlsWorkbook.ᜄ((int)this.ᜀ);
					IXLSRange result = new spr\u20A6(a_2, this.ᜇ() + 1, this.ᜆ() + 1);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
				}
				num = 3;
				continue;
			}
			case 1:
			{
				IXLSRange result;
				return result;
			}
			case 2:
			{
				IXLSRange result = A_1[this.ᜇ() + 1, this.ᜆ() + 1];
				num = 1;
				continue;
			}
			case 3:
			{
				XlsWorkbook xlsWorkbook;
				A_1 = xlsWorkbook.GetSheetByReference((int)this.ᜀ, false);
				num = 4;
				continue;
			}
			case 4:
			{
				if (A_1 != null)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				IXLSRange result;
				return result;
			}
			case 6:
				goto IL_47;
			case 7:
			{
				IXLSRange result;
				return result;
			}
			}
			if (A_0 == null)
			{
				num = 6;
			}
			else
			{
				XlsWorkbook xlsWorkbook = (XlsWorkbook)A_0;
				IXLSRange result = null;
				num = 0;
			}
		}
		IL_47:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁁⭃⥅⍇", a_));
	}

	// Token: 0x06003264 RID: 12900 RVA: 0x001D0E2C File Offset: 0x001CFE2C
	public override void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
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
		this.ᜀ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		base.ᜀ(A_0, ref A_1, A_2);
	}

	// Token: 0x04001600 RID: 5632
	private new ushort ᜀ;
}
