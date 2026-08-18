using System;
using System.Collections.Generic;
using System.Xml;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x02000467 RID: 1127
internal class spr\u2104 : spr\u1A65
{
	// Token: 0x060044A9 RID: 17577 RVA: 0x0028E940 File Offset: 0x0028D940
	protected override int ᜀ()
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
		return 202;
	}

	// Token: 0x060044AA RID: 17578 RVA: 0x0028E980 File Offset: 0x0028D980
	protected override string ᜁ()
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
		return RecordTableEnumerator.b("礶嘸伺堼", a_);
	}

	// Token: 0x060044AB RID: 17579 RVA: 0x0028E9D4 File Offset: 0x0028D9D4
	protected override void ᜀ(List<string> A_0, XlsShape A_1)
	{
		int a_ = 11;
		for (;;)
		{
			IL_1D:
			base.ᜀ(A_0, A_1);
			XlsComment xlsComment = A_1 as XlsComment;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0.Add(RecordTableEnumerator.b("㝀⩂㙄⹆⭈≊⅌♎═⩒潔㽖じ㽚㥜㩞འ", a_));
					num = 1;
					continue;
				case 1:
					goto IL_6C;
				case 2:
					if (!xlsComment.IsVisible)
					{
						num = 0;
						continue;
					}
					return;
				}
				goto IL_1D;
			}
			IL_6C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_82;
			}
		}
		IL_82:
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x060044AC RID: 17580 RVA: 0x0028EA74 File Offset: 0x0028DA74
	protected override void ᜀ(XmlWriter A_0, XlsShape A_1)
	{
		int a_ = 19;
		int num = 3;
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
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					goto IL_A1;
				case 1:
					goto IL_83;
				case 2:
					goto IL_5A;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
		}
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_83:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊ⱌ㽎㑐", a_));
		IL_A1:
		base.ᜀ(A_0, A_1);
		XlsComment xlsComment = A_1 as XlsComment;
		A_0.WriteElementString(RecordTableEnumerator.b("ᭈ⑊㩌", a_), RecordTableEnumerator.b("㱈㥊⍌畎≐げ㵔㉖㑘㩚⹜牞ౠ੢٤ᕦ٨ᡪɬ८հ干ᙴᡶᑸ䅺ቼ᥾뎈ﾒ", a_), (xlsComment.Row - 1).ToString());
		A_0.WriteElementString(RecordTableEnumerator.b("ੈ⑊⅌㩎㱐㵒", a_), RecordTableEnumerator.b("㱈㥊⍌畎≐げ㵔㉖㑘㩚⹜牞ౠ੢٤ᕦ٨ᡪɬ८հ干ᙴᡶᑸ䅺ቼ᥾뎈ﾒ", a_), (xlsComment.Column - 1).ToString());
	}

	// Token: 0x060044AD RID: 17581 RVA: 0x0028EB98 File Offset: 0x0028DB98
	protected override void ᜁ(XmlWriter A_0, XlsShape A_1)
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
		base.ᜃ(A_0, A_1);
	}
}
