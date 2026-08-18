using System;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000384 RID: 900
internal class sprΊ : IListObjectColumn
{
	// Token: 0x060036AB RID: 13995 RVA: 0x001EE2F4 File Offset: 0x001ED2F4
	public string ᜅ()
	{
		int a_ = 11;
		string text;
		for (;;)
		{
			IXLSRange ixlsrange = this.ᜀ();
			text = ixlsrange.Text;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (text != null)
					{
						num = 3;
						continue;
					}
					goto IL_5A;
				case 1:
					return text;
				case 2:
					goto IL_5A;
				case 3:
					goto IL_4E;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4E;
					default:
						if (false)
						{
						}
						if (text.Length == 0)
						{
							num = 2;
							continue;
						}
						return text;
					}
					break;
				}
				break;
				IL_4E:
				num = 4;
				continue;
				IL_5A:
				text = string.Format(RecordTableEnumerator.b("ɀⱂ⥄㉆⑈╊㙌罎ⱐ", a_), this.ᜂ);
				num = 1;
			}
		}
		return text;
	}

	// Token: 0x060036AC RID: 13996 RVA: 0x001EE3C0 File Offset: 0x001ED3C0
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
		this.ᜀ().Text = A_0;
	}

	// Token: 0x060036AD RID: 13997 RVA: 0x001EE408 File Offset: 0x001ED408
	public int ᜄ()
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
		return this.ᜂ;
	}

	// Token: 0x060036AE RID: 13998 RVA: 0x001EE44C File Offset: 0x001ED44C
	public ExcelTotalsCalculation ᜆ()
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
		return this.ᜃ;
	}

	// Token: 0x060036AF RID: 13999 RVA: 0x001EE490 File Offset: 0x001ED490
	public void ᜀ(ExcelTotalsCalculation A_0)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			IXLSRange ixlsrange;
			XlsWorkbook xlsWorkbook;
			string argumentsSeparator;
			for (;;)
			{
				this.ᜃ = A_0;
				ixlsrange = this.ᜂ();
				xlsWorkbook = (ixlsrange.Worksheet.Workbook as XlsWorkbook);
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
							goto IL_70;
						default:
							goto IL_AE;
						}
						break;
					case 1:
						goto IL_89;
					case 2:
						if (!xlsWorkbook.Loading)
						{
							num = 4;
							continue;
						}
						return;
					case 3:
						if (A_0 != ExcelTotalsCalculation.None)
						{
							num = 0;
							continue;
						}
						ixlsrange.Value = string.Empty;
						num = 1;
						continue;
					case 4:
						goto IL_70;
					}
					break;
					IL_70:
					argumentsSeparator = xlsWorkbook.ArgumentsSeparator;
					num = 3;
				}
			}
			IL_89:
			return;
			IL_AE:
			if (false)
			{
			}
			if (true)
			{
			}
			bool throwOnUnknownNames = xlsWorkbook.ThrowOnUnknownNames;
			xlsWorkbook.ThrowOnUnknownNames = false;
			ixlsrange.Formula = string.Format(RecordTableEnumerator.b("祃ᕅᵇࡉᡋōяፑᡓ繕⍗橙⅛╝兟ὡ㽣ᵥ婧ᝩㅫ䝭", a_), (int)A_0, argumentsSeparator, this.ᜁ);
			xlsWorkbook.ThrowOnUnknownNames = throwOnUnknownNames;
			return;
		}
		}
	}

	// Token: 0x060036B0 RID: 14000 RVA: 0x001EE5C0 File Offset: 0x001ED5C0
	public string ᜇ()
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
		return this.ᜄ;
	}

	// Token: 0x060036B1 RID: 14001 RVA: 0x001EE604 File Offset: 0x001ED604
	public void ᜀ(string A_0)
	{
		for (;;)
		{
			IL_42:
			this.ᜄ = A_0;
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
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_68;
					case 2:
						if (!this.ᜁ().Loading)
						{
							num = 1;
							continue;
						}
						return;
					}
					goto IL_42;
				}
				IL_68:
				this.ᜂ().Value = A_0;
				num = 0;
			}
		}
	}

	// Token: 0x060036B2 RID: 14002 RVA: 0x001EE690 File Offset: 0x001ED690
	private IXLSRange ᜂ()
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
		IXLSRange ixlsrange = this.ᜅ.ᜐ();
		return ixlsrange.Worksheet[ixlsrange.LastRow, ixlsrange.Column + this.ᜂ - 1];
	}

	// Token: 0x060036B3 RID: 14003 RVA: 0x001EE6F8 File Offset: 0x001ED6F8
	public int ᜈ()
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

	// Token: 0x060036B4 RID: 14004 RVA: 0x001EE73C File Offset: 0x001ED73C
	public void ᜀ(int A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x060036B5 RID: 14005 RVA: 0x001EE780 File Offset: 0x001ED780
	public string ᜃ()
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
		return this.ᜇ;
	}

	// Token: 0x060036B6 RID: 14006 RVA: 0x001EE7C4 File Offset: 0x001ED7C4
	public void ᜂ(string A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x060036B7 RID: 14007 RVA: 0x001EE808 File Offset: 0x001ED808
	private XlsWorkbook ᜁ()
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
		return this.ᜅ.ᜈ().Workbook as XlsWorkbook;
	}

	// Token: 0x060036B8 RID: 14008 RVA: 0x001EE858 File Offset: 0x001ED858
	private IXLSRange ᜀ()
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
		int row = this.ᜅ.ᜐ().Row;
		int column = this.ᜅ.ᜐ().Column + this.ᜂ - 1;
		return this.ᜅ.ᜈ().Range[row, column];
	}

	// Token: 0x060036B9 RID: 14009 RVA: 0x001EE8D8 File Offset: 0x001ED8D8
	public sprΊ(string A_0, int A_1, spr\u1C4A A_2, int A_3)
	{
		int a_ = 5;
		base..ctor();
		if (A_2 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("䬺尼䴾⑀ⵂㅄፆ⡈⥊⅌⩎", a_));
		}
		this.ᜁ = A_0;
		this.ᜂ = A_1;
		this.ᜅ = A_2;
		this.ᜆ = A_3;
	}

	// Token: 0x060036BA RID: 14010 RVA: 0x001EE92C File Offset: 0x001ED92C
	public sprΊ ᜀ(spr\u1C4A A_0)
	{
		int a_ = 5;
		if (A_0 == null)
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
			throw new ArgumentNullException(RecordTableEnumerator.b("䬺尼䴾⑀ⵂㅄፆ⡈⥊⅌⩎", a_));
		}
		sprΊ sprΊ = (sprΊ)base.MemberwiseClone();
		sprΊ.ᜅ = A_0;
		return sprΊ;
	}

	// Token: 0x04001848 RID: 6216
	private const string ᜀ = "=SUBTOTAL({0}{1}[{2}])";

	// Token: 0x04001849 RID: 6217
	private string ᜁ;

	// Token: 0x0400184A RID: 6218
	private int ᜂ;

	// Token: 0x0400184B RID: 6219
	private ExcelTotalsCalculation ᜃ;

	// Token: 0x0400184C RID: 6220
	private string ᜄ;

	// Token: 0x0400184D RID: 6221
	private spr\u1C4A ᜅ;

	// Token: 0x0400184E RID: 6222
	private int ᜆ;

	// Token: 0x0400184F RID: 6223
	private string ᜇ;
}
