using System;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x0200034D RID: 845
internal class spr\u1DCB : spr\u2316
{
	// Token: 0x06003368 RID: 13160 RVA: 0x001DB748 File Offset: 0x001DA748
	public override XlsShape ᜀ(XmlReader A_0, ShapeCollectionBase A_1)
	{
		int a_ = 14;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				if (A_1 == null)
				{
					goto IL_83;
				}
				goto IL_A1;
			case 3:
				goto IL_58;
			}
			if (A_0 != null)
			{
				num = 1;
				continue;
			}
			if (true)
			{
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
				num = 3;
				continue;
			}
			IL_83:
			num = 0;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅⥇㩉⥋㵍", a_));
		IL_A1:
		A_0.Skip();
		sprថ sprថ = A_1.AppImplementation.ᜉ(A_1 as spr\u1D9B);
		sprថ.ᜀ(true);
		return sprថ;
	}

	// Token: 0x06003369 RID: 13161 RVA: 0x001DB818 File Offset: 0x001DA818
	protected override void ᜀ(XmlReader A_0, TextBoxShapeBase A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 10;
			sprថ sprថ;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("琶儸帺帼吾⑀❂", a_))
					{
						goto IL_16D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_168;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				}
				case 1:
					goto IL_CC;
				case 2:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 4;
						continue;
					}
					goto IL_17A;
				}
				case 3:
					sprថ = (A_1 as sprថ);
					num = 2;
					continue;
				case 4:
					goto IL_168;
				case 5:
					num = 1;
					continue;
				case 6:
					num = 7;
					continue;
				case 7:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("然吸场尼猾⡀ⵂ⹄", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_6F;
				}
				case 8:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("礶嘸漺唼䴾⑀♂ń", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_129;
				}
				case 9:
					num = 8;
					continue;
				}
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				goto IL_1E9;
				IL_168:
				if (true)
				{
				}
				num = 0;
			}
			IL_6F:
			IWorksheet a_2 = sprថ.Worksheet as IWorksheet;
			XlsWorkbook parentWorkbook = sprថ.ParentWorkbook;
			string a_3 = A_0.ReadElementContentAsString();
			FormulaUtil formulaUtil = parentWorkbook.DataHolder.\u1718().ᜀ();
			Ptg[] array = formulaUtil.ᜃ(a_3);
			sprỜ sprỜ = array[0] as sprỜ;
			sprថ.ᜀ(sprỜ.ᜀ(parentWorkbook, a_2));
			return;
			IL_CC:
			goto IL_17A;
			IL_129:
			sprថ.ᜀ(false);
			return;
			IL_16D:
			sprថ.ᜁ((CheckState)A_0.ReadElementContentAsInt());
			return;
			IL_17A:
			A_0.Skip();
			return;
			IL_1E9:
			A_0.Skip();
			return;
		}
		}
	}

	// Token: 0x0600336A RID: 13162 RVA: 0x001DBA14 File Offset: 0x001DAA14
	protected override void ᜀ(TextBoxShapeBase A_0)
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
		base.ᜀ(A_0);
		XlsWorksheet xlsWorksheet = (XlsWorksheet)A_0.Worksheet;
		xlsWorksheet.TypedCheckBoxes.AddCheckBox(A_0 as ICheckBoxShape);
	}
}
