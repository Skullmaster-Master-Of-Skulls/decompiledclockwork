using System;
using System.Collections.Generic;
using System.Xml;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x020002D2 RID: 722
internal class spr\u1BEC : spr\u2316
{
	// Token: 0x06002C44 RID: 11332 RVA: 0x0018BC3C File Offset: 0x0018AC3C
	public override XlsShape ᜀ(XmlReader A_0, ShapeCollectionBase A_1)
	{
		int a_ = 17;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_3C;
			case 2:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_A1;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_54;
				}
				break;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				num = 2;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_54:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㝆⡈㥊⡌ⅎ═", a_));
		IL_A1:
		A_0.Skip();
		return A_1.AppImplementation.ᜆ(A_1);
	}

	// Token: 0x06002C45 RID: 11333 RVA: 0x0018BD00 File Offset: 0x0018AD00
	protected override void ᜀ(XmlReader A_0, TextBoxShapeBase A_1)
	{
		int a_ = 2;
		int num = 1;
		XlsComment xlsComment;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6A;
			case 2:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					goto IL_E8;
				}
				goto IL_A9;
			}
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E8;
				default:
					if (false)
					{
					}
					xlsComment = (A_1 as XlsComment);
					num = 2;
					continue;
				}
				break;
			case 4:
				num = 8;
				continue;
			case 5:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("樷唹䬻", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_53;
			}
			case 6:
				num = 5;
				continue;
			case 7:
				num = 0;
				continue;
			case 8:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("笷唹倻䬽ⴿⱁ", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_F8;
			}
			}
			if (A_0.NodeType == XmlNodeType.Element)
			{
				num = 3;
				continue;
			}
			goto IL_138;
			IL_E8:
			num = 6;
		}
		IL_53:
		xlsComment.Row = A_0.ReadElementContentAsInt() + 1;
		return;
		IL_6A:
		if (true)
		{
		}
		IL_A9:
		A_0.Skip();
		return;
		IL_F8:
		xlsComment.Column = A_0.ReadElementContentAsInt() + 1;
		return;
		IL_138:
		A_0.Skip();
	}

	// Token: 0x06002C46 RID: 11334 RVA: 0x0018BE4C File Offset: 0x0018AE4C
	protected override void ᜀ(TextBoxShapeBase A_0, Dictionary<string, string> A_1)
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
		XlsComment a_ = A_0 as XlsComment;
		this.ᜀ(a_, A_1);
	}

	// Token: 0x06002C47 RID: 11335 RVA: 0x0018BE98 File Offset: 0x0018AE98
	private new void ᜀ(XlsComment A_0, Dictionary<string, string> A_1)
	{
		int a_ = 9;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_51:
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			break;
		default:
			if (false)
			{
			}
			num = 4;
			break;
		}
		bool isVisible;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				isVisible = true;
				num = 2;
				continue;
			case 1:
				goto IL_5F;
			case 2:
			{
				string a;
				if (A_1.TryGetValue(RecordTableEnumerator.b("䤾⡀あⱄ╆⁈❊⑌㭎⡐", a_), out a))
				{
					num = 5;
					continue;
				}
				goto IL_10A;
			}
			case 3:
				goto IL_108;
			case 5:
			{
				string a;
				isVisible = (a != RecordTableEnumerator.b("圾⡀❂⅄≆❈", a_));
				num = 6;
				continue;
			}
			case 6:
				goto IL_E2;
			}
			break;
		}
		goto IL_51;
		IL_5F:
		throw new ArgumentNullException(RecordTableEnumerator.b("尾⹀⹂⡄≆❈㽊", a_));
		IL_E2:
		goto IL_10A;
		IL_108:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬾⡀⁂ㅄᝆ㭈⑊㵌⩎⍐❒㱔㉖⩘", a_));
		IL_10A:
		A_0.IsVisible = isVisible;
	}

	// Token: 0x06002C48 RID: 11336 RVA: 0x0018BFB8 File Offset: 0x0018AFB8
	protected override void ᜀ(TextBoxShapeBase A_0)
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
		base.ᜀ(A_0);
		XlsWorksheet xlsWorksheet = (XlsWorksheet)A_0.Worksheet;
		xlsWorksheet.InnerComments.ᜁ(A_0 as ICommentShape);
	}
}
