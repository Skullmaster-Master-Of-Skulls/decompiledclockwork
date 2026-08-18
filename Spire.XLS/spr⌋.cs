using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200043D RID: 1085
internal class spr\u230B : ShapeParser
{
	// Token: 0x06004146 RID: 16710 RVA: 0x0024868C File Offset: 0x0024768C
	public virtual XlsShape ᜀ(XmlReader A_0, ShapeCollectionBase A_1)
	{
		int a_ = 5;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_8B;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_52;
				}
				break;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		IL_52:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼帾ㅀ♂㙄", a_));
		IL_A1:
		A_0.Skip();
		return A_1.AppImplementation.ᜊ(A_1);
	}

	// Token: 0x06004147 RID: 16711 RVA: 0x00248750 File Offset: 0x00247750
	public virtual bool ᜀ(XmlReader A_0, XlsShape A_1, RelationsCollection A_2, string A_3)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 23;
			for (;;)
			{
				bool flag;
				XlsComboBoxShape xlsComboBoxShape;
				switch (num)
				{
				case 0:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 3;
						continue;
					}
					A_0.Skip();
					num = 17;
					continue;
				case 1:
					return flag;
				case 2:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 16;
						continue;
					}
					goto IL_1C3;
				case 3:
					num = 6;
					continue;
				case 4:
					goto IL_1C3;
				case 5:
					goto IL_234;
				case 6:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 18;
						continue;
					}
					goto IL_A5;
				}
				case 7:
					if (!flag)
					{
						num = 4;
						continue;
					}
					num = 0;
					continue;
				case 8:
					goto IL_A0;
				case 9:
					goto IL_1AA;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E8;
					default:
						if (false)
						{
						}
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐶䴸䈺儼娾", a_)))
						{
							num = 22;
							continue;
						}
						goto IL_276;
					}
					break;
				case 11:
					if (flag)
					{
						num = 15;
						continue;
					}
					return flag;
				case 12:
					goto IL_234;
				case 13:
					goto IL_234;
				case 14:
					A_0.Read();
					flag = true;
					num = 13;
					continue;
				case 15:
					goto IL_1E8;
				case 16:
					num = 7;
					continue;
				case 17:
					goto IL_234;
				case 18:
					num = 24;
					continue;
				case 19:
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					xlsComboBoxShape = (XlsComboBoxShape)A_1.Clone(A_1.Parent, null, null, false);
					spr\u2316.ᜀ(A_0, xlsComboBoxShape);
					num = 10;
					continue;
				case 20:
					flag = this.ᜀ(A_0, xlsComboBoxShape);
					num = 12;
					continue;
				case 21:
					goto IL_276;
				case 22:
				{
					if (true)
					{
					}
					string value = A_0.Value;
					this.ᜀ(value, xlsComboBoxShape);
					A_0.MoveToElement();
					num = 21;
					continue;
				}
				case 24:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("琶唸刺堼儾㕀݂⑄㍆⡈", a_))
					{
						num = 20;
						continue;
					}
					goto IL_A5;
				}
				case 25:
					if (!A_0.IsEmptyElement)
					{
						num = 14;
						continue;
					}
					goto IL_1C3;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 19;
				continue;
				IL_A5:
				A_0.Skip();
				num = 5;
				continue;
				IL_1C3:
				A_0.Read();
				num = 11;
				continue;
				IL_1E8:
				this.ᜀ(xlsComboBoxShape);
				num = 1;
				continue;
				IL_234:
				num = 2;
				continue;
				IL_276:
				flag = false;
				num = 25;
			}
			IL_A0:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
			IL_1AA:
			throw new ArgumentNullException(RecordTableEnumerator.b("匶尸崺尼䨾ⵀ㝂ᙄ⽆⡈㭊⡌", a_));
		}
		}
	}

	// Token: 0x06004148 RID: 16712 RVA: 0x00248A84 File Offset: 0x00247A84
	private void ᜀ(string A_0, XlsShape A_1)
	{
		int a_ = 3;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		Dictionary<string, string> a_2 = base.SplitStyle(A_0);
		A_1.Left = (int)this.ᜀ(a_2, RecordTableEnumerator.b("吸娺似堾⡀ⵂ桄⭆ⱈⵊ㥌", a_));
		A_1.Top = (int)this.ᜀ(a_2, RecordTableEnumerator.b("吸娺似堾⡀ⵂ桄㍆♈㭊", a_));
		A_1.Width = (int)this.ᜀ(a_2, RecordTableEnumerator.b("丸刺夼䬾⥀", a_));
		A_1.Height = (int)this.ᜀ(a_2, RecordTableEnumerator.b("儸帺吼堾⥀㝂", a_));
	}

	// Token: 0x06004149 RID: 16713 RVA: 0x00248B40 File Offset: 0x00247B40
	private double ᜀ(Dictionary<string, string> A_0, string A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			double num;
			for (;;)
			{
				num = 0.0;
				int num2 = 4;
				for (;;)
				{
					MeasureUnits a_2;
					string text;
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7F;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							a_2 = MeasureUnits.Millimeter;
							num2 = 12;
							continue;
						}
						break;
					case 1:
						if (text.Length >= 2)
						{
							num2 = 3;
							continue;
						}
						goto IL_134;
					case 2:
						text = text.Substring(0, text.Length - 2);
						num2 = 6;
						continue;
					case 3:
					{
						string text2 = text.Substring(text.Length - 2);
						num2 = 11;
						continue;
					}
					case 4:
						if (A_0.TryGetValue(A_1, out text))
						{
							goto IL_7F;
						}
						return num;
					case 5:
						return num;
					case 6:
						goto IL_DD;
					case 7:
						num2 = 10;
						continue;
					case 8:
					{
						string text2 = null;
						a_2 = MeasureUnits.Pixel;
						num2 = 1;
						continue;
					}
					case 9:
					{
						string text2;
						string a;
						if ((a = text2) != null)
						{
							num2 = 7;
							continue;
						}
						goto IL_134;
					}
					case 10:
					{
						string a;
						if (a == RecordTableEnumerator.b("唷圹", a_))
						{
							num2 = 0;
							continue;
						}
						goto IL_134;
					}
					case 11:
					{
						string text2;
						if (!char.IsNumber(text2[1]))
						{
							num2 = 2;
							continue;
						}
						goto IL_DD;
					}
					case 12:
						goto IL_134;
					}
					break;
					IL_7F:
					num2 = 8;
					continue;
					IL_DD:
					num2 = 9;
					continue;
					IL_134:
					num = double.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
					num = spr\u17FF.ᜁ(num, a_2);
					num2 = 5;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x0600414A RID: 16714 RVA: 0x00248D0C File Offset: 0x00247D0C
	private bool ᜀ(XmlReader A_0, XlsComboBoxShape A_1)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string localName;
					int num2;
					if (spr\u22D2.\u1759.TryGetValue(localName, out num2))
					{
						num = 11;
						continue;
					}
					goto IL_25B;
				}
				case 2:
				{
					A_0.Read();
					XlsWorkbook parentWorkbook = A_1.ParentWorkbook;
					IWorksheet worksheet = A_1.Worksheet as IWorksheet;
					num = 28;
					continue;
				}
				case 3:
					goto IL_356;
				case 4:
					spr\u22D2.\u1759 = new Dictionary<string, int>(6)
					{
						{
							RecordTableEnumerator.b("཈♊⅌⹎ᵐ㩒㭔㱖", a_),
							0
						},
						{
							RecordTableEnumerator.b("཈♊⅌⹎͐㉒㭔ざ㱘", a_),
							1
						},
						{
							RecordTableEnumerator.b("ᩈ⹊⅌", a_),
							2
						},
						{
							RecordTableEnumerator.b("ࡈ╊⹌❎㹐⅒", a_),
							3
						},
						{
							RecordTableEnumerator.b("ൈ㥊≌㽎ᵐ㩒㭔㉖⩘", a_),
							4
						},
						{
							RecordTableEnumerator.b("݈⑊᥌❎⍐㙒ごፖ歘", a_),
							5
						}
					};
					num = 36;
					continue;
				case 5:
					return false;
				case 6:
					num = 15;
					continue;
				case 7:
					goto IL_25B;
				case 8:
					num = 40;
					continue;
				case 9:
					goto IL_208;
				case 10:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("و⥊❌⩎㉐❒Ŕ⹖⥘㹚", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_51B;
				case 11:
					num = 34;
					continue;
				case 12:
					if (!(A_0.LocalName != RecordTableEnumerator.b("ੈ❊⑌⩎㽐❒ᅔ㙖ⵘ㩚", a_)))
					{
						num = 10;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_50A;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 13:
					goto IL_32C;
				case 14:
					goto IL_32C;
				case 15:
					if (A_0.Value != RecordTableEnumerator.b("ൈ㥊≌㽎", a_))
					{
						num = 5;
						continue;
					}
					goto IL_51B;
				case 16:
				{
					XlsWorkbook parentWorkbook;
					FormulaUtil formulaUtil = parentWorkbook.DataHolder.\u1718().ᜀ();
					string text;
					Ptg[] array = formulaUtil.ᜃ(text);
					sprỜ sprỜ = array[0] as sprỜ;
					IWorksheet worksheet;
					A_1.ListFillRange = sprỜ.ᜀ(parentWorkbook, worksheet);
					num = 20;
					continue;
				}
				case 17:
					if (A_1 == null)
					{
						num = 21;
						continue;
					}
					num = 12;
					continue;
				case 18:
					goto IL_EE;
				case 19:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 35;
						continue;
					}
					num = 31;
					continue;
				case 20:
					goto IL_32C;
				case 21:
					goto IL_5F9;
				case 22:
				{
					XlsWorkbook parentWorkbook;
					IWorksheet worksheet = parentWorkbook.Worksheets[0];
					num = 3;
					continue;
				}
				case 23:
					base.ParseAnchor(A_0, A_1);
					num = 37;
					continue;
				case 24:
				{
					string text;
					if (text != RecordTableEnumerator.b("橈᥊ࡌॎ灐", a_))
					{
						num = 16;
						continue;
					}
					goto IL_32C;
				}
				case 25:
					goto IL_32C;
				case 26:
					goto IL_32C;
				case 27:
					goto IL_32C;
				case 28:
				{
					IWorksheet worksheet;
					if (worksheet == null)
					{
						num = 32;
						continue;
					}
					goto IL_356;
				}
				case 29:
					if (A_1.Worksheet is XlsWorksheet)
					{
						goto IL_50A;
					}
					goto IL_25B;
				case 30:
					num = 7;
					continue;
				case 31:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 33;
						continue;
					}
					A_0.Read();
					num = 43;
					continue;
				case 32:
				{
					XlsWorkbook parentWorkbook;
					IWorksheets worksheets = parentWorkbook.Worksheets;
					if (true)
					{
					}
					num = 38;
					continue;
				}
				case 33:
					num = 39;
					continue;
				case 34:
				{
					int num2;
					switch (num2)
					{
					case 0:
					{
						string text = A_0.ReadElementContentAsString();
						XlsWorkbook parentWorkbook;
						FormulaUtil formulaUtil = parentWorkbook.DataHolder.\u1718().ᜀ();
						Ptg[] array2 = formulaUtil.ᜃ(text);
						sprỜ sprỜ2 = array2[0] as sprỜ;
						IWorksheet worksheet;
						A_1.LinkedCell = sprỜ2.ᜀ(parentWorkbook, worksheet);
						num = 14;
						continue;
					}
					case 1:
					{
						string text = A_0.ReadElementContentAsString();
						num = 24;
						continue;
					}
					case 2:
						A_1.SelectedIndex = A_0.ReadElementContentAsInt();
						num = 13;
						continue;
					case 3:
						num = 29;
						continue;
					case 4:
						A_1.DropDownLines = A_0.ReadElementContentAsInt();
						num = 26;
						continue;
					case 5:
						A_1.Display3DShading = false;
						A_0.Read();
						num = 27;
						continue;
					default:
						num = 30;
						continue;
					}
					break;
				}
				case 35:
					goto IL_351;
				case 36:
					goto IL_130;
				case 37:
					goto IL_32C;
				case 38:
				{
					IWorksheets worksheets;
					if (worksheets.Count > 0)
					{
						num = 22;
						continue;
					}
					goto IL_356;
				}
				case 39:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 8;
						continue;
					}
					goto IL_25B;
				}
				case 40:
					if (spr\u22D2.\u1759 == null)
					{
						num = 4;
						continue;
					}
					goto IL_130;
				case 41:
					if (!A_0.IsEmptyElement)
					{
						num = 2;
						continue;
					}
					goto IL_652;
				case 42:
					goto IL_32C;
				case 43:
					goto IL_32C;
				}
				if (A_0 == null)
				{
					num = 18;
					continue;
				}
				num = 17;
				continue;
				IL_130:
				num = 0;
				continue;
				IL_25B:
				A_0.Skip();
				num = 25;
				continue;
				IL_32C:
				num = 19;
				continue;
				IL_356:
				A_1.Display3DShading = true;
				num = 42;
				continue;
				IL_50A:
				num = 23;
				continue;
				IL_51B:
				num = 41;
			}
			IL_EE:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
			IL_208:
			throw new XmlException(RecordTableEnumerator.b("᱈╊⡌㝎⅐㙒㙔⍖㱘㽚絜❞ౠར䕤፦٨j࡬Ů", a_));
			IL_351:
			goto IL_652;
			IL_5F9:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩈⑊⁌ⵎ㹐ᅒ㩔⽖", a_));
			IL_652:
			A_0.Read();
			return true;
		}
		}
	}

	// Token: 0x0600414B RID: 16715 RVA: 0x00249374 File Offset: 0x00248374
	protected virtual void ᜀ(XlsComboBoxShape A_0)
	{
		int a_ = 16;
		if (A_0 != null)
		{
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
				XlsWorksheetBase worksheet = A_0.Worksheet;
				worksheet.InnerShapes.AddShape(A_0);
				worksheet.TypedComboBoxes.AddComboBox(A_0);
				return;
			}
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("╅❇❉⹋⅍቏㵑ⱓ", a_));
	}

	// Token: 0x04001D0A RID: 7434
	private const string ᜀ = "#REF!";
}
