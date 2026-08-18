using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.TemplateMarkers;

// Token: 0x020003B5 RID: 949
[sprᦱ]
internal class spr\u25C1 : spr\u22EA
{
	// Token: 0x06003A27 RID: 14887 RVA: 0x00209808 File Offset: 0x00208808
	protected override spr\u22EA ᜀ(Match A_0)
	{
		int a_ = 7;
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
				throw new ArgumentNullException(RecordTableEnumerator.b("值", a_));
			}
		}
		this.ᜄ = (A_0.Groups[1].Value.Length > 0);
		return (spr\u22EA)this.ᜅ();
	}

	// Token: 0x06003A28 RID: 14888 RVA: 0x00209890 File Offset: 0x00208890
	public override void ᜀ(IWorksheet A_0, Point A_1, ref int A_2, ref int A_3, IList<long> A_4, spr\u2064 A_5)
	{
		int num = 5;
		InsertOptionsType a_;
		for (;;)
		{
			InsertOptionsType insertOptionsType;
			switch (num)
			{
			case 0:
				goto IL_B8;
			case 1:
				if (A_5.ᜁ() != DataMarkerDirection.Horizontal)
				{
					goto IL_C8;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4F;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 2:
				if (true)
				{
				}
				goto IL_4F;
			case 3:
				insertOptionsType = InsertOptionsType.FormatAsBefore;
				goto IL_7E;
			case 4:
				insertOptionsType = InsertOptionsType.FormatDefault;
				goto IL_7E;
			}
			if (!this.ᜄ)
			{
				num = 2;
				continue;
			}
			num = 3;
			continue;
			IL_4F:
			num = 4;
			continue;
			IL_7E:
			a_ = insertOptionsType;
			num = 1;
		}
		IL_B8:
		int num2 = A_3 + 1;
		((XlsWorksheet)A_0).ᜃ(num2, 1, a_);
		spr\u22EA.ᜀ(A_4, A_5.ᜃ(), num2);
		return;
		IL_C8:
		int num3 = A_2 + 1;
		((XlsWorksheet)A_0).ᜄ(num3, 1, a_);
		spr\u22EA.ᜁ(A_4, A_5.ᜃ(), num3);
	}

	// Token: 0x06003A29 RID: 14889 RVA: 0x00209988 File Offset: 0x00208988
	public override int ᜀ()
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
		return 1;
	}

	// Token: 0x06003A2A RID: 14890 RVA: 0x002099C4 File Offset: 0x002089C4
	public override bool ᜁ()
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
		return true;
	}

	// Token: 0x06003A2B RID: 14891 RVA: 0x00209A00 File Offset: 0x00208A00
	protected override Regex ᜄ()
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
		return spr\u25C1.ᜃ;
	}

	// Token: 0x06003A2C RID: 14892 RVA: 0x00209A40 File Offset: 0x00208A40
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u25C1()
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u25C1.ᜃ = new Regex(RecordTableEnumerator.b("尼嬾╀歂罄㑆㵈㉊⅌⩎≐穒橔", a_), RegexOptions.Compiled);
	}

	// Token: 0x0400195F RID: 6495
	private new const int ᜀ = 1;

	// Token: 0x04001960 RID: 6496
	private new const string ᜁ = "add";

	// Token: 0x04001961 RID: 6497
	private const int ᜂ = 1;

	// Token: 0x04001962 RID: 6498
	private static readonly Regex ᜃ;

	// Token: 0x04001963 RID: 6499
	private bool ᜄ;
}
