using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020005CD RID: 1485
	public class PushbuttonField : BaseField
	{
		// Token: 0x06003312 RID: 13074 RVA: 0x0013D541 File Offset: 0x0013C541
		public PushbuttonField(PdfWriter writer, Rectangle box, string fieldName) : base(writer, box, fieldName)
		{
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x0013D598 File Offset: 0x0013C598
		// (set) Token: 0x06003313 RID: 13075 RVA: 0x0013D577 File Offset: 0x0013C577
		public int Layout
		{
			get
			{
				return this.layout;
			}
			set
			{
				if (value < 1 || value > 7)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("layout.out.of.bounds"));
				}
				this.layout = value;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x0013D5A0 File Offset: 0x0013C5A0
		// (set) Token: 0x06003316 RID: 13078 RVA: 0x0013D5A8 File Offset: 0x0013C5A8
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
				this.template = null;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x0013D5C8 File Offset: 0x0013C5C8
		// (set) Token: 0x06003317 RID: 13079 RVA: 0x0013D5B8 File Offset: 0x0013C5B8
		public PdfTemplate Template
		{
			get
			{
				return this.template;
			}
			set
			{
				this.template = value;
				this.image = null;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x0013D5E9 File Offset: 0x0013C5E9
		// (set) Token: 0x06003319 RID: 13081 RVA: 0x0013D5D0 File Offset: 0x0013C5D0
		public int ScaleIcon
		{
			get
			{
				return this.scaleIcon;
			}
			set
			{
				if (value < 1 || value > 4)
				{
					this.scaleIcon = 1;
					return;
				}
				this.scaleIcon = value;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x0013D5F1 File Offset: 0x0013C5F1
		// (set) Token: 0x0600331C RID: 13084 RVA: 0x0013D5F9 File Offset: 0x0013C5F9
		public bool ProportionalIcon
		{
			get
			{
				return this.proportionalIcon;
			}
			set
			{
				this.proportionalIcon = value;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x0013D602 File Offset: 0x0013C602
		// (set) Token: 0x0600331E RID: 13086 RVA: 0x0013D60A File Offset: 0x0013C60A
		public float IconVerticalAdjustment
		{
			get
			{
				return this.iconVerticalAdjustment;
			}
			set
			{
				this.iconVerticalAdjustment = value;
				if (this.iconVerticalAdjustment < 0f)
				{
					this.iconVerticalAdjustment = 0f;
					return;
				}
				if (this.iconVerticalAdjustment > 1f)
				{
					this.iconVerticalAdjustment = 1f;
				}
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x0013D644 File Offset: 0x0013C644
		// (set) Token: 0x06003320 RID: 13088 RVA: 0x0013D64C File Offset: 0x0013C64C
		public float IconHorizontalAdjustment
		{
			get
			{
				return this.iconHorizontalAdjustment;
			}
			set
			{
				this.iconHorizontalAdjustment = value;
				if (this.iconHorizontalAdjustment < 0f)
				{
					this.iconHorizontalAdjustment = 0f;
					return;
				}
				if (this.iconHorizontalAdjustment > 1f)
				{
					this.iconHorizontalAdjustment = 1f;
				}
			}
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x0013D688 File Offset: 0x0013C688
		private float CalculateFontSize(float w, float h)
		{
			BaseFont realFont = base.RealFont;
			float num = this.fontSize;
			if (num == 0f)
			{
				float widthPoint = realFont.GetWidthPoint(this.text, 1f);
				if (widthPoint == 0f)
				{
					num = 12f;
				}
				else
				{
					num = w / widthPoint;
				}
				float val = h / (1f - realFont.GetFontDescriptor(3, 1f));
				num = Math.Min(num, val);
				if (num < 4f)
				{
					num = 4f;
				}
			}
			return num;
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x0013D700 File Offset: 0x0013C700
		public PdfAppearance GetAppearance()
		{
			PdfAppearance borderAppearance = base.GetBorderAppearance();
			Rectangle rectangle = new Rectangle(borderAppearance.BoundingBox);
			if ((this.text == null || this.text.Length == 0) && (this.layout == 1 || (this.image == null && this.template == null && this.iconReference == null)))
			{
				return borderAppearance;
			}
			if (this.layout == 2 && this.image == null && this.template == null && this.iconReference == null)
			{
				return borderAppearance;
			}
			BaseFont realFont = base.RealFont;
			bool flag = this.borderStyle == 2 || this.borderStyle == 3;
			float num = rectangle.Height - this.borderWidth * 2f;
			float num2 = this.borderWidth;
			if (flag)
			{
				num -= this.borderWidth * 2f;
				num2 *= 2f;
			}
			float num3 = flag ? (2f * this.borderWidth) : this.borderWidth;
			num3 = Math.Max(num3, 1f);
			float num4 = Math.Min(num2, num3);
			this.tp = null;
			float num5 = float.NaN;
			float num6 = 0f;
			float num7 = this.fontSize;
			float num8 = rectangle.Width - 2f * num4 - 2f;
			float num9 = rectangle.Height - 2f * num4;
			float num10 = this.iconFitToBounds ? 0f : (num4 + 1f);
			int num11 = this.layout;
			if (this.image == null && this.template == null && this.iconReference == null)
			{
				num11 = 1;
			}
			Rectangle rectangle2 = null;
			for (;;)
			{
				switch (num11)
				{
				case 1:
				case 7:
					goto IL_1AA;
				case 2:
					goto IL_213;
				case 3:
					if (this.text == null || this.text.Length == 0 || num8 <= 0f || num9 <= 0f)
					{
						num11 = 2;
						continue;
					}
					goto IL_27F;
				case 4:
					if (this.text == null || this.text.Length == 0 || num8 <= 0f || num9 <= 0f)
					{
						num11 = 2;
						continue;
					}
					goto IL_334;
				case 5:
				{
					if (this.text == null || this.text.Length == 0 || num8 <= 0f || num9 <= 0f)
					{
						num11 = 2;
						continue;
					}
					float num12 = rectangle.Width * 0.35f - num4;
					if (num12 > 0f)
					{
						num7 = this.CalculateFontSize(num8, num12);
					}
					else
					{
						num7 = 4f;
					}
					if (realFont.GetWidthPoint(this.text, num7) >= num8)
					{
						num11 = 1;
						num7 = this.fontSize;
						continue;
					}
					goto IL_525;
				}
				case 6:
				{
					if (this.text == null || this.text.Length == 0 || num8 <= 0f || num9 <= 0f)
					{
						num11 = 2;
						continue;
					}
					float num12 = rectangle.Width * 0.35f - num4;
					if (num12 > 0f)
					{
						num7 = this.CalculateFontSize(num8, num12);
					}
					else
					{
						num7 = 4f;
					}
					if (realFont.GetWidthPoint(this.text, num7) >= num8)
					{
						num11 = 1;
						num7 = this.fontSize;
						continue;
					}
					goto IL_44B;
				}
				}
				break;
			}
			goto IL_587;
			IL_1AA:
			if (this.text != null && this.text.Length > 0 && num8 > 0f && num9 > 0f)
			{
				num7 = this.CalculateFontSize(num8, num9);
				num5 = (rectangle.Width - realFont.GetWidthPoint(this.text, num7)) / 2f;
				num6 = (rectangle.Height - realFont.GetFontDescriptor(1, num7)) / 2f;
			}
			IL_213:
			if (num11 == 7 || num11 == 2)
			{
				rectangle2 = new Rectangle(rectangle.Left + num10, rectangle.Bottom + num10, rectangle.Right - num10, rectangle.Top - num10);
				goto IL_587;
			}
			goto IL_587;
			IL_27F:
			float num13 = rectangle.Height * 0.35f - num4;
			if (num13 > 0f)
			{
				num7 = this.CalculateFontSize(num8, num13);
			}
			else
			{
				num7 = 4f;
			}
			num5 = (rectangle.Width - realFont.GetWidthPoint(this.text, num7)) / 2f;
			num6 = num4 - realFont.GetFontDescriptor(3, num7);
			rectangle2 = new Rectangle(rectangle.Left + num10, num6 + num7, rectangle.Right - num10, rectangle.Top - num10);
			goto IL_587;
			IL_334:
			num13 = rectangle.Height * 0.35f - num4;
			if (num13 > 0f)
			{
				num7 = this.CalculateFontSize(num8, num13);
			}
			else
			{
				num7 = 4f;
			}
			num5 = (rectangle.Width - realFont.GetWidthPoint(this.text, num7)) / 2f;
			num6 = rectangle.Height - num4 - num7;
			if (num6 < num4)
			{
				num6 = num4;
			}
			rectangle2 = new Rectangle(rectangle.Left + num10, rectangle.Bottom + num10, rectangle.Right - num10, num6 + realFont.GetFontDescriptor(3, num7));
			goto IL_587;
			IL_44B:
			num5 = num4 + 1f;
			num6 = (rectangle.Height - realFont.GetFontDescriptor(1, num7)) / 2f;
			rectangle2 = new Rectangle(num5 + realFont.GetWidthPoint(this.text, num7), rectangle.Bottom + num10, rectangle.Right - num10, rectangle.Top - num10);
			goto IL_587;
			IL_525:
			num5 = rectangle.Width - realFont.GetWidthPoint(this.text, num7) - num4 - 1f;
			num6 = (rectangle.Height - realFont.GetFontDescriptor(1, num7)) / 2f;
			rectangle2 = new Rectangle(rectangle.Left + num10, rectangle.Bottom + num10, num5 - 1f, rectangle.Top - num10);
			IL_587:
			if (num6 < rectangle.Bottom + num4)
			{
				num6 = rectangle.Bottom + num4;
			}
			if (rectangle2 != null && (rectangle2.Width <= 0f || rectangle2.Height <= 0f))
			{
				rectangle2 = null;
			}
			bool flag2 = false;
			float num14 = 0f;
			float num15 = 0f;
			PdfArray pdfArray = null;
			if (rectangle2 != null)
			{
				if (this.image != null)
				{
					this.tp = new PdfTemplate(this.writer);
					this.tp.BoundingBox = new Rectangle(this.image);
					this.writer.AddDirectTemplateSimple(this.tp, PdfName.FRM);
					this.tp.AddImage(this.image, this.image.Width, 0f, 0f, this.image.Height, 0f, 0f);
					flag2 = true;
					num14 = this.tp.BoundingBox.Width;
					num15 = this.tp.BoundingBox.Height;
				}
				else if (this.template != null)
				{
					this.tp = new PdfTemplate(this.writer);
					this.tp.BoundingBox = new Rectangle(this.template.Width, this.template.Height);
					this.writer.AddDirectTemplateSimple(this.tp, PdfName.FRM);
					this.tp.AddTemplate(this.template, this.template.BoundingBox.Left, this.template.BoundingBox.Bottom);
					flag2 = true;
					num14 = this.tp.BoundingBox.Width;
					num15 = this.tp.BoundingBox.Height;
				}
				else if (this.iconReference != null)
				{
					PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(this.iconReference);
					if (pdfDictionary != null)
					{
						Rectangle normalizedRectangle = PdfReader.GetNormalizedRectangle(pdfDictionary.GetAsArray(PdfName.BBOX));
						pdfArray = pdfDictionary.GetAsArray(PdfName.MATRIX);
						flag2 = true;
						num14 = normalizedRectangle.Width;
						num15 = normalizedRectangle.Height;
					}
				}
			}
			if (flag2)
			{
				float num16 = rectangle2.Width / num14;
				float num17 = rectangle2.Height / num15;
				if (this.proportionalIcon)
				{
					switch (this.scaleIcon)
					{
					case 2:
						num16 = 1f;
						break;
					case 3:
						num16 = Math.Min(num16, num17);
						num16 = Math.Min(num16, 1f);
						break;
					case 4:
						num16 = Math.Min(num16, num17);
						num16 = Math.Max(num16, 1f);
						break;
					default:
						num16 = Math.Min(num16, num17);
						break;
					}
					num17 = num16;
				}
				else
				{
					switch (this.scaleIcon)
					{
					case 2:
						num17 = (num16 = 1f);
						break;
					case 3:
						num16 = Math.Min(num16, 1f);
						num17 = Math.Min(num17, 1f);
						break;
					case 4:
						num16 = Math.Max(num16, 1f);
						num17 = Math.Max(num17, 1f);
						break;
					}
				}
				float num18 = rectangle2.Left + (rectangle2.Width - num14 * num16) * this.iconHorizontalAdjustment;
				float num19 = rectangle2.Bottom + (rectangle2.Height - num15 * num17) * this.iconVerticalAdjustment;
				borderAppearance.SaveState();
				borderAppearance.Rectangle(rectangle2.Left, rectangle2.Bottom, rectangle2.Width, rectangle2.Height);
				borderAppearance.Clip();
				borderAppearance.NewPath();
				if (this.tp != null)
				{
					borderAppearance.AddTemplate(this.tp, num16, 0f, 0f, num17, num18, num19);
				}
				else
				{
					float num20 = 0f;
					float num21 = 0f;
					if (pdfArray != null && pdfArray.Size == 6)
					{
						PdfNumber asNumber = pdfArray.GetAsNumber(4);
						if (asNumber != null)
						{
							num20 = asNumber.FloatValue;
						}
						asNumber = pdfArray.GetAsNumber(5);
						if (asNumber != null)
						{
							num21 = asNumber.FloatValue;
						}
					}
					borderAppearance.AddTemplateReference(this.iconReference, PdfName.FRM, num16, 0f, 0f, num17, num18 - num20 * num16, num19 - num21 * num17);
				}
				borderAppearance.RestoreState();
			}
			if (!float.IsNaN(num5))
			{
				borderAppearance.SaveState();
				borderAppearance.Rectangle(num4, num4, rectangle.Width - 2f * num4, rectangle.Height - 2f * num4);
				borderAppearance.Clip();
				borderAppearance.NewPath();
				if (this.textColor == null)
				{
					borderAppearance.ResetGrayFill();
				}
				else
				{
					borderAppearance.SetColorFill(this.textColor);
				}
				borderAppearance.BeginText();
				borderAppearance.SetFontAndSize(realFont, num7);
				borderAppearance.SetTextMatrix(num5, num6);
				borderAppearance.ShowText(this.text);
				borderAppearance.EndText();
				borderAppearance.RestoreState();
			}
			return borderAppearance;
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003323 RID: 13091 RVA: 0x0013E148 File Offset: 0x0013D148
		public PdfFormField Field
		{
			get
			{
				PdfFormField pdfFormField = PdfFormField.CreatePushButton(this.writer);
				pdfFormField.SetWidget(this.box, PdfAnnotation.HIGHLIGHT_INVERT);
				if (this.fieldName != null)
				{
					pdfFormField.FieldName = this.fieldName;
					if ((this.options & 1) != 0)
					{
						pdfFormField.SetFieldFlags(1);
					}
					if ((this.options & 2) != 0)
					{
						pdfFormField.SetFieldFlags(2);
					}
				}
				if (this.text != null)
				{
					pdfFormField.MKNormalCaption = this.text;
				}
				if (this.rotation != 0)
				{
					pdfFormField.MKRotation = this.rotation;
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
				if (this.tp != null)
				{
					pdfFormField.MKNormalIcon = this.tp;
				}
				pdfFormField.MKTextPosition = this.layout - 1;
				PdfName scale = PdfName.A;
				if (this.scaleIcon == 3)
				{
					scale = PdfName.B;
				}
				else if (this.scaleIcon == 4)
				{
					scale = PdfName.S;
				}
				else if (this.scaleIcon == 2)
				{
					scale = PdfName.N;
				}
				pdfFormField.SetMKIconFit(scale, this.proportionalIcon ? PdfName.P : PdfName.A, this.iconHorizontalAdjustment, this.iconVerticalAdjustment, this.iconFitToBounds);
				return pdfFormField;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x0013E334 File Offset: 0x0013D334
		// (set) Token: 0x06003325 RID: 13093 RVA: 0x0013E33C File Offset: 0x0013D33C
		public bool IconFitToBounds
		{
			get
			{
				return this.iconFitToBounds;
			}
			set
			{
				this.iconFitToBounds = value;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x0013E345 File Offset: 0x0013D345
		// (set) Token: 0x06003327 RID: 13095 RVA: 0x0013E34D File Offset: 0x0013D34D
		public PRIndirectReference IconReference
		{
			get
			{
				return this.iconReference;
			}
			set
			{
				this.iconReference = value;
			}
		}

		// Token: 0x040022AE RID: 8878
		public const int LAYOUT_LABEL_ONLY = 1;

		// Token: 0x040022AF RID: 8879
		public const int LAYOUT_ICON_ONLY = 2;

		// Token: 0x040022B0 RID: 8880
		public const int LAYOUT_ICON_TOP_LABEL_BOTTOM = 3;

		// Token: 0x040022B1 RID: 8881
		public const int LAYOUT_LABEL_TOP_ICON_BOTTOM = 4;

		// Token: 0x040022B2 RID: 8882
		public const int LAYOUT_ICON_LEFT_LABEL_RIGHT = 5;

		// Token: 0x040022B3 RID: 8883
		public const int LAYOUT_LABEL_LEFT_ICON_RIGHT = 6;

		// Token: 0x040022B4 RID: 8884
		public const int LAYOUT_LABEL_OVER_ICON = 7;

		// Token: 0x040022B5 RID: 8885
		public const int SCALE_ICON_ALWAYS = 1;

		// Token: 0x040022B6 RID: 8886
		public const int SCALE_ICON_NEVER = 2;

		// Token: 0x040022B7 RID: 8887
		public const int SCALE_ICON_IS_TOO_BIG = 3;

		// Token: 0x040022B8 RID: 8888
		public const int SCALE_ICON_IS_TOO_SMALL = 4;

		// Token: 0x040022B9 RID: 8889
		private int layout = 1;

		// Token: 0x040022BA RID: 8890
		private Image image;

		// Token: 0x040022BB RID: 8891
		private PdfTemplate template;

		// Token: 0x040022BC RID: 8892
		private int scaleIcon = 1;

		// Token: 0x040022BD RID: 8893
		private bool proportionalIcon = true;

		// Token: 0x040022BE RID: 8894
		private float iconVerticalAdjustment = 0.5f;

		// Token: 0x040022BF RID: 8895
		private float iconHorizontalAdjustment = 0.5f;

		// Token: 0x040022C0 RID: 8896
		private bool iconFitToBounds;

		// Token: 0x040022C1 RID: 8897
		private PdfTemplate tp;

		// Token: 0x040022C2 RID: 8898
		private PRIndirectReference iconReference;
	}
}
