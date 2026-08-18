using System;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x0200029A RID: 666
internal class spr\u229F : spr\u2316
{
	// Token: 0x0600273D RID: 10045 RVA: 0x00166B7C File Offset: 0x00165B7C
	public override XlsShape ᜀ(XmlReader A_0, ShapeCollectionBase A_1)
	{
		int a_ = 0;
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
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_8B;
				case 1:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					goto IL_A1;
				case 2:
					goto IL_62;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				break;
			}
			num = 1;
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷嬹䰻嬽㌿", a_));
		IL_A1:
		A_0.Skip();
		return A_1.AppImplementation.ᜋ(A_1 as spr\u22F9);
	}

	// Token: 0x0600273E RID: 10046 RVA: 0x00166C44 File Offset: 0x00165C44
	protected override void ᜀ(XmlReader A_0, TextBoxShapeBase A_1)
	{
		int a_ = 4;
		int num = 0;
		RadioButton radioButton;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 16;
				continue;
			case 2:
				goto IL_EF;
			case 3:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("簹唻䰽㌿㙁ك㍅㱇㹉⍋⁍", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_C6;
			}
			case 4:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("礹吻嬽⌿⥁⅃≅", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_6D;
			}
			case 5:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 14;
					continue;
				}
				goto IL_FE;
			}
			case 6:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 9;
				continue;
			case 7:
				num = 10;
				continue;
			case 8:
				goto IL_FC;
			case 9:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 15;
					continue;
				}
				goto IL_24C;
			case 10:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("琹医樽⠿ぁ⅃⍅ే", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_176;
			}
			case 11:
				num = 3;
				continue;
			case 12:
				goto IL_68;
			case 13:
				num = 8;
				continue;
			case 14:
				num = 4;
				continue;
			case 15:
				radioButton = (A_1 as RadioButton);
				radioButton.Display3DShading = true;
				num = 5;
				continue;
			case 16:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("簹儻刽ℿแⵃ⡅⍇", a_))
				{
					goto IL_17E;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_60;
				default:
					if (false)
					{
					}
					num = 11;
					continue;
				}
				break;
			}
			}
			goto IL_5D;
			IL_60:
			num = 12;
			continue;
			IL_5D:
			if (A_0 == null)
			{
				goto IL_60;
			}
			num = 6;
		}
		IL_68:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_6D:
		radioButton.CheckState = (CheckState)A_0.ReadElementContentAsInt();
		return;
		IL_C6:
		radioButton.IsFirstButton = true;
		return;
		IL_EF:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻弽〿❁㝃", a_));
		IL_FC:
		IL_FE:
		A_0.Skip();
		return;
		IL_176:
		radioButton.Display3DShading = false;
		return;
		IL_17E:
		IWorksheet worksheet = radioButton.Worksheet as IWorksheet;
		radioButton.LinkedCell = worksheet[A_0.ReadElementContentAsString()];
		return;
		IL_24C:
		A_0.Skip();
	}

	// Token: 0x0600273F RID: 10047 RVA: 0x00166EA4 File Offset: 0x00165EA4
	protected override void ᜀ(TextBoxShapeBase A_0)
	{
		int a_ = 16;
		while (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⭉㱋⭍⍏", a_));
			}
		}
		base.ᜀ(A_0);
		XlsWorksheet xlsWorksheet = (XlsWorksheet)A_0.Worksheet;
		xlsWorksheet.TypedOptionButtons.AddRadioButton(A_0 as IRadioButton);
	}
}
