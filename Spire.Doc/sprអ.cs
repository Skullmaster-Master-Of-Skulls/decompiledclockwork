using System;
using System.Globalization;
using System.Threading;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

// Token: 0x02000372 RID: 882
internal class sprអ
{
	// Token: 0x0600316F RID: 12655 RVA: 0x002DC060 File Offset: 0x002DB060
	public static void ᜀ(FormField A_0, spr\u258D A_1)
	{
		switch (0)
		{
		default:
		{
			DropDownFormField dropDownFormField;
			for (;;)
			{
				A_0.Name = A_1.ᜂ();
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_2B2;
					case 1:
					{
						int num2;
						if (num2 >= A_1.ᜌ().Count)
						{
							num = 6;
							continue;
						}
						dropDownFormField.DropDownItems.Add(A_1.ᜌ()[num2]);
						num2++;
						num = 13;
						continue;
					}
					case 2:
						if (A_1.ᜉ() == FieldType.FieldFormCheckBox)
						{
							num = 0;
							continue;
						}
						num = 16;
						continue;
					case 3:
						goto IL_1C1;
					case 4:
					{
						TextFormField textFormField;
						if (textFormField.TextFieldType == TextFormFieldType.RegularText)
						{
							num = 15;
							continue;
						}
						return;
					}
					case 5:
						A_0.Help = A_1.ᜄ();
						A_0.MacroOnEnd = A_1.ᜈ();
						A_0.MacroOnStart = A_1.ᜅ();
						A_0.StatusBarHelp = A_1.ᜏ();
						A_0.InnerValue = A_1.\u1713();
						A_0.Params = (int)A_1.\u1716();
						num = 3;
						continue;
					case 6:
						num = 7;
						continue;
					case 7:
						if (A_1.ᜌ().Count > 0)
						{
							num = 12;
							continue;
						}
						return;
					case 8:
					{
						TextFormField textFormField = A_0 as TextFormField;
						textFormField.MaximumLength = A_1.\u170D();
						textFormField.StringFormat = A_1.ᜎ();
						textFormField.TextFieldType = A_1.ᜊ();
						textFormField.DefaultText = A_1.ᜆ();
						num = 4;
						continue;
					}
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1C1;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_2B7;
						}
						break;
					case 10:
						if (A_1.ᜂ() != null)
						{
							num = 5;
							continue;
						}
						return;
					case 11:
					{
						dropDownFormField = (A_0 as DropDownFormField);
						dropDownFormField.DefaultDropDownValue = A_1.ᜐ();
						dropDownFormField.DropDownSelectedIndex = A_1.ᜁ();
						int num2 = 0;
						num = 9;
						continue;
					}
					case 12:
						goto IL_254;
					case 13:
						goto IL_2B7;
					case 14:
						goto IL_226;
					case 15:
					{
						TextFormField textFormField;
						textFormField.TextFormat = sprអ.ᜁ(A_1.ᜎ());
						num = 14;
						continue;
					}
					case 16:
						if (A_1.ᜉ() == FieldType.FieldFormTextInput)
						{
							num = 8;
							continue;
						}
						return;
					}
					break;
					IL_1C1:
					if (A_1.ᜉ() == FieldType.FieldFormDropDown)
					{
						num = 11;
						continue;
					}
					num = 2;
					continue;
					IL_2B7:
					num = 1;
				}
			}
			IL_226:
			return;
			IL_254:
			dropDownFormField.DropDownValue = A_1.\u1712();
			return;
			IL_2B2:
			CheckBoxFormField checkBoxFormField = A_0 as CheckBoxFormField;
			checkBoxFormField.CheckBoxSize = A_1.ᜋ() / 2;
			checkBoxFormField.DefaultCheckBoxValue = A_1.ᜑ();
			return;
		}
		}
	}

	// Token: 0x06003170 RID: 12656 RVA: 0x002DC350 File Offset: 0x002DB350
	public static void ᜀ(spr\u258D A_0, FormField A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			DropDownFormField dropDownFormField;
			TextFormField textFormField;
			for (;;)
			{
				A_0.ᜆ(A_1.Help);
				A_0.ᜃ(A_1.MacroOnEnd);
				A_0.ᜄ(A_1.MacroOnStart);
				A_0.ᜀ((short)A_1.Params);
				A_0.ᜁ(A_1.Name);
				A_0.ᜇ(A_1.StatusBarHelp);
				A_0.ᜀ(A_1.InnerValue);
				int num = 16;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_18F;
						default:
							goto IL_2E5;
						}
						break;
					case 1:
						goto IL_117;
					case 2:
						if (dropDownFormField.DropDownItems.Count > 0)
						{
							num = 12;
							continue;
						}
						return;
					case 3:
						A_0.ᜂ(dropDownFormField.DefaultDropDownValue);
						A_0.ᜃ(dropDownFormField.DropDownSelectedIndex);
						num = 2;
						continue;
					case 4:
						dropDownFormField = (A_1 as DropDownFormField);
						num2 = 0;
						num = 1;
						continue;
					case 5:
						if (dropDownFormField.DropDownItems.Count <= dropDownFormField.DropDownSelectedIndex)
						{
							num = 15;
							continue;
						}
						goto IL_10A;
					case 6:
						if (num2 >= dropDownFormField.DropDownItems.Count)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_18F;
					case 7:
						if (A_1.FormFieldType == FormFieldType.CheckBox)
						{
							num = 8;
							continue;
						}
						num = 14;
						continue;
					case 8:
						goto IL_314;
					case 9:
						goto IL_206;
					case 10:
						textFormField = (A_1 as TextFormField);
						A_0.ᜁ(textFormField.MaximumLength);
						A_0.ᜀ(textFormField.TextFieldType);
						num = 11;
						continue;
					case 11:
						if (textFormField.TextFieldType == TextFormFieldType.RegularText)
						{
							num = 0;
							continue;
						}
						A_0.ᜅ(textFormField.StringFormat);
						A_0.ᜀ(textFormField.DefaultText);
						num = 9;
						continue;
					case 12:
						num = 5;
						continue;
					case 13:
						goto IL_117;
					case 14:
						if (A_1.FormFieldType == FormFieldType.TextInput)
						{
							num = 10;
							continue;
						}
						return;
					case 15:
						goto IL_361;
					case 16:
						if (A_1.FormFieldType == FormFieldType.DropDown)
						{
							num = 4;
							continue;
						}
						num = 7;
						continue;
					}
					break;
					IL_117:
					num = 6;
					continue;
					IL_18F:
					A_0.ᜌ().Add(dropDownFormField.DropDownItems[num2].Text);
					num2++;
					num = 13;
				}
			}
			IL_10A:
			A_0.ᜂ(dropDownFormField.DropDownValue);
			return;
			IL_206:
			return;
			IL_2E5:
			if (false)
			{
			}
			A_0.ᜅ(sprអ.ᜀ(textFormField));
			A_0.ᜀ(sprអ.ᜀ(textFormField.TextFormat, textFormField.DefaultText));
			textFormField.TextRange.Text = sprអ.ᜀ(textFormField.TextFormat, textFormField.TextRange.Text);
			return;
			IL_314:
			CheckBoxFormField checkBoxFormField = A_1 as CheckBoxFormField;
			A_0.ᜄ(checkBoxFormField.CheckBoxSize * 2);
			A_0.ᜂ(checkBoxFormField.DefaultCheckBoxValue);
			return;
			IL_361:
			throw new ArgumentException(ClipboardData.b("㕰Ųᩴݶ㵸ᑺ੼ᅾ좀ꦈﲊﮎ戀뎒ﲔ練ﶘﺚ뾞", a_) + dropDownFormField.DropDownSelectedIndex + ClipboardData.b("兰ᝲᩴቶ੸ᕺ婼୾ꆀﶄ愈ﾊ", a_));
		}
		}
	}

	// Token: 0x06003171 RID: 12657 RVA: 0x002DC6C4 File Offset: 0x002DB6C4
	private static TextFormat ᜁ(string A_0)
	{
		int a_ = 19;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == ClipboardData.b("㕸㑺⩼㩾펀삂쒄풆첈", a_)))
				{
					num = 2;
					continue;
				}
				return TextFormat.Lowercase;
			case 1:
				num = 0;
				continue;
			case 2:
				goto IL_124;
			case 4:
				goto IL_61;
			case 5:
				num = 10;
				continue;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_124;
				default:
					if (false)
					{
					}
					if (!(A_0 == ClipboardData.b("⵸㉺⥼㍾쒀ꎂ욄욆\uda88캊", a_)))
					{
						num = 8;
						continue;
					}
					return TextFormat.Titlecase;
				}
				break;
			case 7:
				if (!(A_0 == ClipboardData.b("㽸㉺⽼Ȿ햀ꎂ욄욆\ud988슊\ud98c캎\udd90", a_)))
				{
					num = 9;
					continue;
				}
				return TextFormat.FirstCapital;
			case 8:
				if (true)
				{
				}
				num = 4;
				continue;
			case 9:
				num = 6;
				continue;
			case 10:
				if (!(A_0 == ClipboardData.b("ⱸ⭺⵼㩾펀삂쒄풆첈", a_)))
				{
					num = 1;
					continue;
				}
				return TextFormat.Uppercase;
			}
			if (A_0 != null)
			{
				num = 5;
				continue;
			}
			return TextFormat.None;
			IL_124:
			num = 7;
		}
		return TextFormat.Lowercase;
		IL_61:
		return TextFormat.None;
	}

	// Token: 0x06003172 RID: 12658 RVA: 0x002DC82C File Offset: 0x002DB82C
	private static string ᜀ(TextFormField A_0)
	{
		int a_ = 2;
		for (;;)
		{
			TextFormat textFormat = A_0.TextFormat;
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch (textFormat)
						{
						case TextFormat.Uppercase:
							goto IL_B6;
						case TextFormat.Lowercase:
							goto IL_83;
						case TextFormat.FirstCapital:
							goto IL_74;
						case TextFormat.Titlecase:
							goto IL_9F;
						default:
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_9D;
					case 2:
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
		IL_74:
		return ClipboardData.b("⹧⍩㹫㵭⑯剱㝳㝵⡷㍹⡻㽽챿", a_);
		IL_83:
		return ClipboardData.b("⑧╩㭫⭭≯ㅱ㕳╵㵷", a_);
		IL_9D:
		return string.Empty;
		IL_9F:
		if (true)
		{
		}
		return ClipboardData.b("㱧⍩㡫≭㕯剱㝳㝵⭷㽹", a_);
		IL_B6:
		return ClipboardData.b("㵧㩩㱫⭭≯ㅱ㕳╵㵷", a_);
	}

	// Token: 0x06003173 RID: 12659 RVA: 0x002DC904 File Offset: 0x002DB904
	private static NumberFormat ᜀ(string A_0)
	{
		int a_ = 0;
		int num = 12;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 13;
				continue;
			case 1:
				if (!(A_0 == ClipboardData.b("䕥坧䥩屫䉭䁯䉱", a_)))
				{
					num = 14;
					continue;
				}
				return NumberFormat.FloatingPointWithSpace;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_ED;
				default:
					goto IL_158;
				}
				break;
			case 3:
				if (!(A_0 == ClipboardData.b("噥䑧婩屫", a_)))
				{
					num = 0;
					continue;
				}
				return NumberFormat.FloatingPoint;
			case 4:
				num = 16;
				continue;
			case 5:
				num = 9;
				continue;
			case 6:
				num = 1;
				continue;
			case 7:
				num = 3;
				continue;
			case 8:
				goto IL_113;
			case 9:
				goto IL_ED;
			case 10:
				num = 11;
				continue;
			case 11:
				if (!(A_0 == ClipboardData.b("噥", a_)))
				{
					num = 7;
					continue;
				}
				return NumberFormat.WholeNumber;
			case 13:
				if (!(A_0 == ClipboardData.b("噥䵧", a_)))
				{
					num = 5;
					continue;
				}
				return NumberFormat.WholeNumberPercent;
			case 14:
				num = 8;
				continue;
			case 15:
				if (A_0.StartsWith(ClipboardData.b("䕥坧䥩屫䉭䁯䉱味", a_)))
				{
					num = 2;
					continue;
				}
				return NumberFormat.None;
			case 16:
				if (!(A_0 == ClipboardData.b("䕥坧䥩屫", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_163;
			}
			if (A_0 != null)
			{
				num = 10;
				continue;
			}
			goto IL_113;
			IL_ED:
			if (!(A_0 == ClipboardData.b("噥䑧婩屫䭭", a_)))
			{
				num = 4;
				continue;
			}
			break;
			IL_113:
			num = 15;
		}
		return NumberFormat.FloatingPointPercent;
		IL_158:
		if (false)
		{
		}
		return NumberFormat.CurrencyFormat;
		IL_163:
		if (true)
		{
		}
		return NumberFormat.WholeNumberWithSpace;
	}

	// Token: 0x06003174 RID: 12660 RVA: 0x002DCB20 File Offset: 0x002DBB20
	private static string ᜁ(NumberFormat A_0)
	{
		int a_ = 16;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case NumberFormat.WholeNumber:
						goto IL_A1;
					case NumberFormat.FloatingPoint:
						goto IL_E1;
					case NumberFormat.WholeNumberPercent:
						goto IL_D2;
					case NumberFormat.FloatingPointPercent:
						goto IL_92;
					case NumberFormat.WholeNumberWithSpace:
						goto IL_83;
					case NumberFormat.FloatingPointWithSpace:
						goto IL_C3;
					case NumberFormat.CurrencyFormat:
						goto IL_B0;
					default:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					break;
				case 1:
					goto IL_C1;
				case 2:
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_83:
		return ClipboardData.b("啵䝷她䱻", a_);
		IL_92:
		return ClipboardData.b("䙵呷䩹䱻孽", a_);
		IL_A1:
		return ClipboardData.b("䙵", a_);
		IL_B0:
		return sprអ.ᜁ;
		IL_C1:
		return string.Empty;
		IL_C3:
		return ClipboardData.b("啵䝷她䱻剽끿늁", a_);
		IL_D2:
		return ClipboardData.b("䙵嵷", a_);
		IL_E1:
		return ClipboardData.b("䙵呷䩹䱻", a_);
	}

	// Token: 0x06003175 RID: 12661 RVA: 0x002DCC24 File Offset: 0x002DBC24
	private static string ᜀ(NumberFormat A_0)
	{
		int a_ = 16;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_55:
				switch (A_0)
				{
				case NumberFormat.WholeNumber:
				case NumberFormat.WholeNumberWithSpace:
					goto IL_D1;
				case NumberFormat.FloatingPoint:
				case NumberFormat.FloatingPointWithSpace:
					goto IL_92;
				case NumberFormat.WholeNumberPercent:
					goto IL_E0;
				case NumberFormat.FloatingPointPercent:
					goto IL_83;
				case NumberFormat.CurrencyFormat:
					goto IL_AE;
				default:
					num = 0;
					break;
				}
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					goto IL_55;
				case 2:
					goto IL_AC;
				}
				break;
			}
		}
		IL_83:
		return ClipboardData.b("䙵呷䩹䱻孽", a_);
		IL_92:
		return ClipboardData.b("䙵呷䩹䱻", a_);
		IL_AC:
		return string.Empty;
		IL_AE:
		return ClipboardData.b("䙵呷䩹䱻幽", a_) + sprអ.ᜀ.NumberFormat.CurrencySymbol;
		IL_D1:
		return ClipboardData.b("䙵", a_);
		IL_E0:
		return ClipboardData.b("䙵嵷", a_);
	}

	// Token: 0x06003176 RID: 12662 RVA: 0x002DCD28 File Offset: 0x002DBD28
	internal static string ᜀ(TextFormat A_0, string A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					if (A_0 == TextFormat.FirstCapital)
					{
						num = 20;
						continue;
					}
					num = 7;
					continue;
				case 1:
				{
					int num3;
					if (num2 < num3 - 1)
					{
						num = 10;
						continue;
					}
					goto IL_112;
				}
				case 2:
					if (A_0 == TextFormat.Uppercase)
					{
						num = 11;
						continue;
					}
					num = 15;
					continue;
				case 3:
				{
					int num4;
					string[] array;
					if (num4 >= array.Length)
					{
						num = 6;
						continue;
					}
					string text = array[num4];
					text[0].ToString().ToUpper() + text.Remove(0, 1);
					array[num4] = text;
					num4++;
					num = 17;
					continue;
				}
				case 4:
				{
					string[] array = A_1.Split(new char[]
					{
						' '
					});
					int num4 = 0;
					num = 8;
					continue;
				}
				case 5:
					goto IL_1C2;
				case 6:
				{
					A_1 = string.Empty;
					string[] array;
					int num3 = array.Length;
					num2 = 0;
					num = 5;
					continue;
				}
				case 7:
					if (A_0 == TextFormat.Titlecase)
					{
						num = 4;
						continue;
					}
					return A_1;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_25D;
					default:
						if (false)
						{
						}
						goto IL_29F;
					}
					break;
				case 9:
					return A_1;
				case 10:
					A_1 += ClipboardData.b("剱", a_);
					num = 12;
					continue;
				case 11:
					goto IL_258;
				case 12:
					goto IL_112;
				case 13:
					num = 2;
					continue;
				case 15:
					if (A_0 == TextFormat.Lowercase)
					{
						num = 16;
						continue;
					}
					goto IL_25D;
				case 16:
					goto IL_1BD;
				case 17:
					goto IL_29F;
				case 18:
					goto IL_1C2;
				case 19:
				{
					int num3;
					if (num2 >= num3)
					{
						if (true)
						{
						}
						num = 9;
						continue;
					}
					string[] array;
					A_1 += array[num2];
					num = 1;
					continue;
				}
				case 20:
					goto IL_279;
				}
				if (A_1 != string.Empty)
				{
					num = 13;
					continue;
				}
				return A_1;
				IL_112:
				num2++;
				num = 18;
				continue;
				IL_1C2:
				num = 19;
				continue;
				IL_25D:
				num = 0;
				continue;
				IL_29F:
				num = 3;
			}
			IL_1BD:
			return A_1.ToLower();
			IL_258:
			return A_1.ToUpper();
			IL_279:
			return A_1[0].ToString().ToUpper() + A_1.Remove(0, 1);
		}
		}
	}

	// Token: 0x06003177 RID: 12663 RVA: 0x002DCFFC File Offset: 0x002DBFFC
	private static string ᜀ(string A_0, NumberFormat A_1, string A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 10;
			double num2;
			string text;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_273;
				case 1:
					goto IL_86;
				case 2:
					goto IL_237;
				case 3:
					num2 *= 100.0;
					num2 = Math.Floor(num2);
					num2 /= 100.0;
					num = 12;
					continue;
				case 4:
					if (A_1 == NumberFormat.WholeNumberWithSpace)
					{
						num = 14;
						continue;
					}
					goto IL_86;
				case 5:
					if (A_0 != string.Empty)
					{
						num = 13;
						continue;
					}
					goto IL_187;
				case 6:
					try
					{
						num2 = Convert.ToDouble(A_2);
						goto IL_120;
					}
					catch
					{
						return sprអ.ᜀ(A_1);
					}
					goto IL_F8;
					IL_120:
					num = 11;
					continue;
				case 7:
					if (true)
					{
					}
					text = text.Substring(1, text.Length - 1);
					num = 2;
					continue;
				case 8:
					goto IL_81;
				case 9:
					if (num2 >= 1000.0)
					{
						return text;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return A_2;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 11:
					switch (A_1)
					{
					case NumberFormat.WholeNumber:
					case NumberFormat.WholeNumberPercent:
						num = 17;
						continue;
					case NumberFormat.FloatingPoint:
					case NumberFormat.FloatingPointPercent:
						goto IL_D8;
					case NumberFormat.WholeNumberWithSpace:
					case NumberFormat.FloatingPointWithSpace:
					case NumberFormat.CurrencyFormat:
						num = 4;
						continue;
					default:
						num = 16;
						continue;
					}
					break;
				case 12:
					goto IL_216;
				case 13:
					goto IL_185;
				case 14:
					goto IL_F8;
				case 15:
					num = 5;
					continue;
				case 16:
					num = 15;
					continue;
				case 17:
					if (A_1 == NumberFormat.WholeNumberPercent)
					{
						num = 3;
						continue;
					}
					num2 = Math.Floor(num2);
					num = 0;
					continue;
				}
				if (A_1 == NumberFormat.None)
				{
					num = 8;
					continue;
				}
				A_2 = A_2.Replace('.', ',');
				num2 = 0.0;
				text = string.Empty;
				num = 6;
				continue;
				IL_86:
				text = sprអ.ᜀ(A_0, num2);
				num = 9;
				continue;
				IL_F8:
				num2 = Math.Floor(num2);
				num = 1;
			}
			IL_81:
			return A_2;
			IL_D8:
			return sprអ.ᜀ(A_0, num2);
			IL_118:
			return sprអ.ᜀ(A_0, num2);
			IL_185:
			return sprអ.ᜀ(A_0, num2);
			IL_187:
			return string.Empty;
			IL_216:
			goto IL_118;
			IL_237:
			return text;
			IL_273:
			goto IL_118;
		}
		}
	}

	// Token: 0x06003178 RID: 12664 RVA: 0x002DD294 File Offset: 0x002DC294
	private static string ᜀ(string A_0, double A_1)
	{
		double num2;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_76:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num2 = 0.0;
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0[A_0.Length - 1] == '%')
					{
						goto IL_76;
					}
					num2 = Math.Round(A_1, 2);
					num = 7;
					continue;
				case 1:
					if (num2 > A_1)
					{
						num = 5;
						continue;
					}
					goto IL_A7;
				case 2:
					A_1 *= 100.0;
					num2 = Math.Round(A_1, 2);
					num = 1;
					continue;
				case 3:
					goto IL_A7;
				case 4:
					goto IL_BE;
				case 5:
					num2 -= 0.01;
					num = 3;
					continue;
				case 6:
					num2 -= 0.01;
					if (true)
					{
					}
					num = 8;
					continue;
				case 7:
					if (num2 > A_1)
					{
						num = 6;
						continue;
					}
					goto IL_12E;
				case 8:
					goto IL_110;
				}
				break;
				IL_A7:
				num2 /= 100.0;
				num = 4;
			}
		}
		IL_BE:
		IL_110:
		IL_12E:
		return num2.ToString(A_0);
	}

	// Token: 0x0600317A RID: 12666 RVA: 0x002DD3F0 File Offset: 0x002DC3F0
	// Note: this type is marked as 'beforefieldinit'.
	static sprអ()
	{
		int a_ = 8;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		sprអ.ᜀ = Thread.CurrentThread.CurrentCulture;
		sprអ.ᜁ = string.Concat(new string[]
		{
			ClipboardData.b("䵭佯共䑳塵䡷䩹屻", a_),
			sprអ.ᜀ.NumberFormat.CurrencySymbol,
			ClipboardData.b("啭塯共䭳啵䡷呹䱻乽ꁿ", a_),
			sprអ.ᜀ.NumberFormat.CurrencySymbol,
			ClipboardData.b("䝭", a_)
		});
	}

	// Token: 0x0400270A RID: 9994
	private static CultureInfo ᜀ;

	// Token: 0x0400270B RID: 9995
	private static string ᜁ;
}
