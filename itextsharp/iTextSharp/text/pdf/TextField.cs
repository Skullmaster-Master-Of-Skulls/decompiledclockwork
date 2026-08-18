using System;
using System.Collections.Generic;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x020004DC RID: 1244
	public class TextField : BaseField
	{
		// Token: 0x06002A4C RID: 10828 RVA: 0x00101F01 File Offset: 0x00100F01
		public TextField(PdfWriter writer, Rectangle box, string fieldName) : base(writer, box, fieldName)
		{
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x00101F18 File Offset: 0x00100F18
		private static bool CheckRTL(string text)
		{
			if (text == null || text.Length == 0)
			{
				return false;
			}
			foreach (int num in text.ToCharArray())
			{
				if (num >= 1424 && num < 1920)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x00101F60 File Offset: 0x00100F60
		private static void ChangeFontSize(Phrase p, float size)
		{
			foreach (IElement element in p)
			{
				Chunk chunk = (Chunk)element;
				chunk.Font.Size = size;
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x00101FB8 File Offset: 0x00100FB8
		private Phrase ComposePhrase(string text, BaseFont ufont, BaseColor color, float fontSize)
		{
			Phrase result;
			if (this.extensionFont == null && (this.substitutionFonts == null || this.substitutionFonts.Count == 0))
			{
				result = new Phrase(new Chunk(text, new Font(ufont, fontSize, 0, color)));
			}
			else
			{
				FontSelector fontSelector = new FontSelector();
				fontSelector.AddFont(new Font(ufont, fontSize, 0, color));
				if (this.extensionFont != null)
				{
					fontSelector.AddFont(new Font(this.extensionFont, fontSize, 0, color));
				}
				if (this.substitutionFonts != null)
				{
					foreach (BaseFont bf in this.substitutionFonts)
					{
						fontSelector.AddFont(new Font(bf, fontSize, 0, color));
					}
				}
				result = fontSelector.Process(text);
			}
			return result;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x00102094 File Offset: 0x00101094
		public static string RemoveCRLF(string text)
		{
			if (text.IndexOf('\n') >= 0 || text.IndexOf('\r') >= 0)
			{
				char[] array = text.ToCharArray();
				StringBuilder stringBuilder = new StringBuilder(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					char c = array[i];
					if (c == '\n')
					{
						stringBuilder.Append(' ');
					}
					else if (c == '\r')
					{
						stringBuilder.Append(' ');
						if (i < array.Length - 1 && array[i + 1] == '\n')
						{
							i++;
						}
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x0010211E File Offset: 0x0010111E
		public static string ObfuscatePassword(string text)
		{
			return new string('*', text.Length);
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x00102130 File Offset: 0x00101130
		public PdfAppearance GetAppearance()
		{
			PdfAppearance borderAppearance = base.GetBorderAppearance();
			borderAppearance.BeginVariableText();
			if (this.text == null || this.text.Length == 0)
			{
				borderAppearance.EndVariableText();
				return borderAppearance;
			}
			bool flag = this.borderStyle == 2 || this.borderStyle == 3;
			float num = this.box.Height - this.borderWidth * 2f - this.extraMarginTop;
			float num2 = this.borderWidth;
			if (flag)
			{
				num -= this.borderWidth * 2f;
				num2 *= 2f;
			}
			float num3 = Math.Max(num2, 1f);
			float num4 = Math.Min(num2, num3);
			borderAppearance.SaveState();
			borderAppearance.Rectangle(num4, num4, this.box.Width - 2f * num4, this.box.Height - 2f * num4);
			borderAppearance.Clip();
			borderAppearance.NewPath();
			string text;
			if ((this.options & 8192) != 0)
			{
				text = TextField.ObfuscatePassword(this.text);
			}
			else if ((this.options & 4096) == 0)
			{
				text = TextField.RemoveCRLF(this.text);
			}
			else
			{
				text = this.text;
			}
			BaseFont realFont = base.RealFont;
			BaseColor color = (this.textColor == null) ? GrayColor.GRAYBLACK : this.textColor;
			int runDirection = TextField.CheckRTL(text) ? 2 : 1;
			float num5 = this.fontSize;
			Phrase phrase = this.ComposePhrase(text, realFont, color, num5);
			if ((this.options & 4096) != 0)
			{
				float urx = this.box.Width - 4f * num3 - this.extraMarginLeft;
				float num6 = realFont.GetFontDescriptor(8, 1f) - realFont.GetFontDescriptor(6, 1f);
				ColumnText columnText = new ColumnText(null);
				if (num5 == 0f)
				{
					num5 = num / num6;
					if (num5 > 4f)
					{
						if (num5 > 12f)
						{
							num5 = 12f;
						}
						float num7 = Math.Max((num5 - 4f) / 10f, 0.2f);
						columnText.SetSimpleColumn(0f, -num, urx, 0f);
						columnText.Alignment = this.alignment;
						columnText.RunDirection = runDirection;
						while (num5 > 4f)
						{
							columnText.YLine = 0f;
							TextField.ChangeFontSize(phrase, num5);
							columnText.SetText(phrase);
							columnText.Leading = num6 * num5;
							int num8 = columnText.Go(true);
							if ((num8 & 2) == 0)
							{
								break;
							}
							num5 -= num7;
						}
					}
					if (num5 < 4f)
					{
						num5 = 4f;
					}
				}
				TextField.ChangeFontSize(phrase, num5);
				columnText.Canvas = borderAppearance;
				float num9 = num5 * num6;
				float num10 = num3 + num - realFont.GetFontDescriptor(8, num5);
				columnText.SetSimpleColumn(this.extraMarginLeft + 2f * num3, -20000f, this.box.Width - 2f * num3, num10 + num9);
				columnText.Leading = num9;
				columnText.Alignment = this.alignment;
				columnText.RunDirection = runDirection;
				columnText.SetText(phrase);
				columnText.Go();
			}
			else
			{
				if (num5 == 0f)
				{
					float num11 = num / (realFont.GetFontDescriptor(7, 1f) - realFont.GetFontDescriptor(6, 1f));
					TextField.ChangeFontSize(phrase, 1f);
					float width = ColumnText.GetWidth(phrase, runDirection, 0);
					if (width == 0f)
					{
						num5 = num11;
					}
					else
					{
						num5 = Math.Min(num11, (this.box.Width - this.extraMarginLeft - 4f * num3) / width);
					}
					if (num5 < 4f)
					{
						num5 = 4f;
					}
				}
				TextField.ChangeFontSize(phrase, num5);
				float num12 = num4 + (this.box.Height - 2f * num4 - realFont.GetFontDescriptor(1, num5)) / 2f;
				if (num12 < num4)
				{
					num12 = num4;
				}
				if (num12 - num4 < -realFont.GetFontDescriptor(3, num5))
				{
					float val = -realFont.GetFontDescriptor(3, num5) + num4;
					float val2 = this.box.Height - num4 - realFont.GetFontDescriptor(1, num5);
					num12 = Math.Min(val, Math.Max(num12, val2));
				}
				if ((this.options & 16777216) != 0 && this.maxCharacterLength > 0)
				{
					int num13 = Math.Min(this.maxCharacterLength, text.Length);
					int num14 = 0;
					if (this.alignment == 2)
					{
						num14 = this.maxCharacterLength - num13;
					}
					else if (this.alignment == 1)
					{
						num14 = (this.maxCharacterLength - num13) / 2;
					}
					float num15 = (this.box.Width - this.extraMarginLeft) / (float)this.maxCharacterLength;
					float num16 = num15 / 2f + (float)num14 * num15;
					if (this.textColor == null)
					{
						borderAppearance.SetGrayFill(0f);
					}
					else
					{
						borderAppearance.SetColorFill(this.textColor);
					}
					borderAppearance.BeginText();
					foreach (IElement element in phrase)
					{
						Chunk chunk = (Chunk)element;
						BaseFont baseFont = chunk.Font.BaseFont;
						borderAppearance.SetFontAndSize(baseFont, num5);
						StringBuilder stringBuilder = chunk.Append("");
						for (int i = 0; i < stringBuilder.Length; i++)
						{
							string text2 = stringBuilder.ToString(i, 1);
							float widthPoint = baseFont.GetWidthPoint(text2, num5);
							borderAppearance.SetTextMatrix(this.extraMarginLeft + num16 - widthPoint / 2f, num12 - this.extraMarginTop);
							borderAppearance.ShowText(text2);
							num16 += num15;
						}
					}
					borderAppearance.EndText();
				}
				else
				{
					float x;
					switch (this.alignment)
					{
					case 1:
						x = this.extraMarginLeft + this.box.Width / 2f;
						break;
					case 2:
						x = this.extraMarginLeft + this.box.Width - 2f * num3;
						break;
					default:
						x = this.extraMarginLeft + 2f * num3;
						break;
					}
					ColumnText.ShowTextAligned(borderAppearance, this.alignment, phrase, x, num12 - this.extraMarginTop, 0f, runDirection, 0);
				}
			}
			borderAppearance.RestoreState();
			borderAppearance.EndVariableText();
			return borderAppearance;
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x00102788 File Offset: 0x00101788
		internal PdfAppearance GetListAppearance()
		{
			PdfAppearance borderAppearance = base.GetBorderAppearance();
			if (this.choices == null || this.choices.Length == 0)
			{
				return borderAppearance;
			}
			borderAppearance.BeginVariableText();
			int topChoice = this.GetTopChoice();
			BaseFont realFont = base.RealFont;
			float num = this.fontSize;
			if (num == 0f)
			{
				num = 12f;
			}
			bool flag = this.borderStyle == 2 || this.borderStyle == 3;
			float num2 = this.box.Height - this.borderWidth * 2f;
			float num3 = this.borderWidth;
			if (flag)
			{
				num2 -= this.borderWidth * 2f;
				num3 *= 2f;
			}
			float num4 = realFont.GetFontDescriptor(8, num) - realFont.GetFontDescriptor(6, num);
			int num5 = (int)(num2 / num4) + 1;
			int num6 = topChoice;
			int num7 = num6 + num5;
			if (num7 > this.choices.Length)
			{
				num7 = this.choices.Length;
			}
			this.topFirst = num6;
			borderAppearance.SaveState();
			borderAppearance.Rectangle(num3, num3, this.box.Width - 2f * num3, this.box.Height - 2f * num3);
			borderAppearance.Clip();
			borderAppearance.NewPath();
			BaseColor baseColor = (this.textColor == null) ? GrayColor.GRAYBLACK : this.textColor;
			borderAppearance.SetColorFill(new BaseColor(10, 36, 106));
			for (int i = 0; i < this.choiceSelections.Count; i++)
			{
				int num8 = this.choiceSelections[i];
				if (num8 >= num6 && num8 <= num7)
				{
					borderAppearance.Rectangle(num3, num3 + num2 - (float)(num8 - num6 + 1) * num4, this.box.Width - 2f * num3, num4);
					borderAppearance.Fill();
				}
			}
			float x = num3 * 2f;
			float num9 = num3 + num2 - realFont.GetFontDescriptor(8, num);
			int j = num6;
			while (j < num7)
			{
				string text = this.choices[j];
				int runDirection = TextField.CheckRTL(text) ? 2 : 1;
				text = TextField.RemoveCRLF(text);
				BaseColor color = this.choiceSelections.Contains(j) ? GrayColor.GRAYWHITE : baseColor;
				Phrase phrase = this.ComposePhrase(text, realFont, color, num);
				ColumnText.ShowTextAligned(borderAppearance, 0, phrase, x, num9, 0f, runDirection, 0);
				j++;
				num9 -= num4;
			}
			borderAppearance.RestoreState();
			borderAppearance.EndVariableText();
			return borderAppearance;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x001029F4 File Offset: 0x001019F4
		public PdfFormField GetTextField()
		{
			if (this.maxCharacterLength <= 0)
			{
				this.options &= -16777217;
			}
			if ((this.options & 16777216) != 0)
			{
				this.options &= -4097;
			}
			PdfFormField pdfFormField = PdfFormField.CreateTextField(this.writer, false, false, this.maxCharacterLength);
			pdfFormField.SetWidget(this.box, PdfAnnotation.HIGHLIGHT_INVERT);
			switch (this.alignment)
			{
			case 1:
				pdfFormField.Quadding = 1;
				break;
			case 2:
				pdfFormField.Quadding = 2;
				break;
			}
			if (this.rotation != 0)
			{
				pdfFormField.MKRotation = this.rotation;
			}
			if (this.fieldName != null)
			{
				pdfFormField.FieldName = this.fieldName;
				if (!"".Equals(this.text))
				{
					pdfFormField.ValueAsString = this.text;
				}
				if (this.defaultText != null)
				{
					pdfFormField.DefaultValueAsString = this.defaultText;
				}
				if ((this.options & 1) != 0)
				{
					pdfFormField.SetFieldFlags(1);
				}
				if ((this.options & 2) != 0)
				{
					pdfFormField.SetFieldFlags(2);
				}
				if ((this.options & 4096) != 0)
				{
					pdfFormField.SetFieldFlags(4096);
				}
				if ((this.options & 8388608) != 0)
				{
					pdfFormField.SetFieldFlags(8388608);
				}
				if ((this.options & 8192) != 0)
				{
					pdfFormField.SetFieldFlags(8192);
				}
				if ((this.options & 1048576) != 0)
				{
					pdfFormField.SetFieldFlags(1048576);
				}
				if ((this.options & 4194304) != 0)
				{
					pdfFormField.SetFieldFlags(4194304);
				}
				if ((this.options & 16777216) != 0)
				{
					pdfFormField.SetFieldFlags(16777216);
				}
			}
			pdfFormField.BorderStyle = new PdfBorderDictionary(this.borderWidth, this.borderStyle, new PdfDashPattern(3f));
			PdfAppearance appearance = this.GetAppearance();
			pdfFormField.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, appearance);
			PdfAppearance pdfAppearance = (PdfAppearance)appearance.Duplicate;
			pdfAppearance.SetFontAndSize(base.RealFont, this.fontSize);
			if (this.textColor == null)
			{
				pdfAppearance.SetGrayFill(0f);
			}
			else
			{
				pdfAppearance.SetColorFill(this.textColor);
			}
			pdfFormField.DefaultAppearanceString = pdfAppearance;
			if (this.borderColor != null)
			{
				pdfFormField.MKBorderColor = this.borderColor;
			}
			if (this.backgroundColor != null)
			{
				pdfFormField.MKBackgroundColor = this.backgroundColor;
			}
			switch (this.visibility)
			{
			case 1:
				pdfFormField.Flags = 6;
				break;
			case 2:
				break;
			case 3:
				pdfFormField.Flags = 36;
				break;
			default:
				pdfFormField.Flags = 4;
				break;
			}
			return pdfFormField;
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x00102C83 File Offset: 0x00101C83
		public PdfFormField GetComboField()
		{
			return this.GetChoiceField(false);
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00102C8C File Offset: 0x00101C8C
		public PdfFormField GetListField()
		{
			return this.GetChoiceField(true);
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x00102C98 File Offset: 0x00101C98
		private int GetTopChoice()
		{
			if (this.choiceSelections == null || this.choiceSelections.Count == 0)
			{
				return 0;
			}
			int num = this.choiceSelections[0];
			int num2 = 0;
			if (this.choices != null)
			{
				num2 = num;
				num2 = Math.Min(num2, this.choices.Length);
				num2 = Math.Max(0, num2);
			}
			return num2;
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x00102CF0 File Offset: 0x00101CF0
		protected PdfFormField GetChoiceField(bool isList)
		{
			this.options &= -16781313;
			string[] array = this.choices;
			if (array == null)
			{
				array = new string[0];
			}
			int topChoice = this.GetTopChoice();
			if (this.text == null)
			{
				this.text = "";
			}
			if (topChoice >= 0)
			{
				this.text = array[topChoice];
			}
			string[,] array2 = null;
			PdfFormField pdfFormField;
			if (this.choiceExports == null)
			{
				if (isList)
				{
					pdfFormField = PdfFormField.CreateList(this.writer, array, topChoice);
				}
				else
				{
					pdfFormField = PdfFormField.CreateCombo(this.writer, (this.options & 262144) != 0, array, topChoice);
				}
			}
			else
			{
				array2 = new string[array.Length, 2];
				for (int i = 0; i < array2.GetLength(0); i++)
				{
					array2[i, 0] = (array2[i, 1] = array[i]);
				}
				int num = Math.Min(array.Length, this.choiceExports.Length);
				for (int j = 0; j < num; j++)
				{
					if (this.choiceExports[j] != null)
					{
						array2[j, 0] = this.choiceExports[j];
					}
				}
				if (isList)
				{
					pdfFormField = PdfFormField.CreateList(this.writer, array2, topChoice);
				}
				else
				{
					pdfFormField = PdfFormField.CreateCombo(this.writer, (this.options & 262144) != 0, array2, topChoice);
				}
			}
			pdfFormField.SetWidget(this.box, PdfAnnotation.HIGHLIGHT_INVERT);
			if (this.rotation != 0)
			{
				pdfFormField.MKRotation = this.rotation;
			}
			if (this.fieldName != null)
			{
				pdfFormField.FieldName = this.fieldName;
				if (array.Length > 0)
				{
					if (array2 != null)
					{
						if (this.choiceSelections.Count < 2)
						{
							pdfFormField.ValueAsString = array2[topChoice, 0];
							pdfFormField.DefaultValueAsString = array2[topChoice, 0];
						}
						else
						{
							this.WriteMultipleValues(pdfFormField, array2);
						}
					}
					else if (this.choiceSelections.Count < 2)
					{
						pdfFormField.ValueAsString = this.text;
						pdfFormField.DefaultValueAsString = this.text;
					}
					else
					{
						this.WriteMultipleValues(pdfFormField, null);
					}
				}
				if ((this.options & 1) != 0)
				{
					pdfFormField.SetFieldFlags(1);
				}
				if ((this.options & 2) != 0)
				{
					pdfFormField.SetFieldFlags(2);
				}
				if ((this.options & 4194304) != 0)
				{
					pdfFormField.SetFieldFlags(4194304);
				}
				if ((this.options & 2097152) != 0)
				{
					pdfFormField.SetFieldFlags(2097152);
				}
			}
			pdfFormField.BorderStyle = new PdfBorderDictionary(this.borderWidth, this.borderStyle, new PdfDashPattern(3f));
			PdfAppearance pdfAppearance;
			if (isList)
			{
				pdfAppearance = this.GetListAppearance();
				if (this.topFirst > 0)
				{
					pdfFormField.Put(PdfName.TI, new PdfNumber(this.topFirst));
				}
			}
			else
			{
				pdfAppearance = this.GetAppearance();
			}
			pdfFormField.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, pdfAppearance);
			PdfAppearance pdfAppearance2 = (PdfAppearance)pdfAppearance.Duplicate;
			pdfAppearance2.SetFontAndSize(base.RealFont, this.fontSize);
			if (this.textColor == null)
			{
				pdfAppearance2.SetGrayFill(0f);
			}
			else
			{
				pdfAppearance2.SetColorFill(this.textColor);
			}
			pdfFormField.DefaultAppearanceString = pdfAppearance2;
			if (this.borderColor != null)
			{
				pdfFormField.MKBorderColor = this.borderColor;
			}
			if (this.backgroundColor != null)
			{
				pdfFormField.MKBackgroundColor = this.backgroundColor;
			}
			switch (this.visibility)
			{
			case 1:
				pdfFormField.Flags = 6;
				break;
			case 2:
				break;
			case 3:
				pdfFormField.Flags = 36;
				break;
			default:
				pdfFormField.Flags = 4;
				break;
			}
			return pdfFormField;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x00103050 File Offset: 0x00102050
		private void WriteMultipleValues(PdfFormField field, string[,] mix)
		{
			PdfArray pdfArray = new PdfArray();
			PdfArray pdfArray2 = new PdfArray();
			for (int i = 0; i < this.choiceSelections.Count; i++)
			{
				int num = this.choiceSelections[i];
				pdfArray.Add(new PdfNumber(num));
				if (mix != null)
				{
					pdfArray2.Add(new PdfString(mix[num, 0]));
				}
				else if (this.choices != null)
				{
					pdfArray2.Add(new PdfString(this.choices[num]));
				}
			}
			field.Put(PdfName.V, pdfArray2);
			field.Put(PdfName.I, pdfArray);
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x001030E6 File Offset: 0x001020E6
		// (set) Token: 0x06002A5B RID: 10843 RVA: 0x001030EE File Offset: 0x001020EE
		public string DefaultText
		{
			get
			{
				return this.defaultText;
			}
			set
			{
				this.defaultText = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x001030F7 File Offset: 0x001020F7
		// (set) Token: 0x06002A5D RID: 10845 RVA: 0x001030FF File Offset: 0x001020FF
		public string[] Choices
		{
			get
			{
				return this.choices;
			}
			set
			{
				this.choices = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x00103108 File Offset: 0x00102108
		// (set) Token: 0x06002A5F RID: 10847 RVA: 0x00103110 File Offset: 0x00102110
		public string[] ChoiceExports
		{
			get
			{
				return this.choiceExports;
			}
			set
			{
				this.choiceExports = value;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x00103119 File Offset: 0x00102119
		// (set) Token: 0x06002A61 RID: 10849 RVA: 0x00103121 File Offset: 0x00102121
		public int ChoiceSelection
		{
			get
			{
				return this.GetTopChoice();
			}
			set
			{
				this.choiceSelections = new List<int>();
				this.choiceSelections.Add(value);
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x0010313A File Offset: 0x0010213A
		// (set) Token: 0x06002A63 RID: 10851 RVA: 0x00103144 File Offset: 0x00102144
		public List<int> ChoiceSelections
		{
			get
			{
				return this.choiceSelections;
			}
			set
			{
				if (value != null)
				{
					this.choiceSelections = new List<int>(value);
					if (this.choiceSelections.Count > 1 && (this.options & 2097152) == 0)
					{
						while (this.choiceSelections.Count > 1)
						{
							this.choiceSelections.RemoveAt(1);
						}
						return;
					}
				}
				else
				{
					this.choiceSelections.Clear();
				}
			}
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x001031A4 File Offset: 0x001021A4
		public void AddChoiceSelection(int selection)
		{
			if ((this.options & 2097152) != 0)
			{
				this.choiceSelections.Add(selection);
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x001031C0 File Offset: 0x001021C0
		internal int TopFirst
		{
			get
			{
				return this.topFirst;
			}
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x001031C8 File Offset: 0x001021C8
		public void SetExtraMargin(float extraMarginLeft, float extraMarginTop)
		{
			this.extraMarginLeft = extraMarginLeft;
			this.extraMarginTop = extraMarginTop;
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x001031E1 File Offset: 0x001021E1
		// (set) Token: 0x06002A67 RID: 10855 RVA: 0x001031D8 File Offset: 0x001021D8
		public List<BaseFont> SubstitutionFonts
		{
			get
			{
				return this.substitutionFonts;
			}
			set
			{
				this.substitutionFonts = value;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x001031F2 File Offset: 0x001021F2
		// (set) Token: 0x06002A69 RID: 10857 RVA: 0x001031E9 File Offset: 0x001021E9
		public BaseFont ExtensionFont
		{
			get
			{
				return this.extensionFont;
			}
			set
			{
				this.extensionFont = value;
			}
		}

		// Token: 0x04001D79 RID: 7545
		private string defaultText;

		// Token: 0x04001D7A RID: 7546
		private string[] choices;

		// Token: 0x04001D7B RID: 7547
		private string[] choiceExports;

		// Token: 0x04001D7C RID: 7548
		private List<int> choiceSelections = new List<int>();

		// Token: 0x04001D7D RID: 7549
		private int topFirst;

		// Token: 0x04001D7E RID: 7550
		private float extraMarginLeft;

		// Token: 0x04001D7F RID: 7551
		private float extraMarginTop;

		// Token: 0x04001D80 RID: 7552
		private List<BaseFont> substitutionFonts;

		// Token: 0x04001D81 RID: 7553
		private BaseFont extensionFont;
	}
}
