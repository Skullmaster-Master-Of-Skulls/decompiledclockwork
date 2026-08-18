using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000636 RID: 1590
	public class RadioCheckField : BaseField
	{
		// Token: 0x060035C6 RID: 13766 RVA: 0x0014D858 File Offset: 0x0014C858
		public RadioCheckField(PdfWriter writer, Rectangle box, string fieldName, string onValue) : base(writer, box, fieldName)
		{
			this.OnValue = onValue;
			this.CheckType = 2;
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060035C7 RID: 13767 RVA: 0x0014D872 File Offset: 0x0014C872
		// (set) Token: 0x060035C8 RID: 13768 RVA: 0x0014D87C File Offset: 0x0014C87C
		public int CheckType
		{
			get
			{
				return this.checkType;
			}
			set
			{
				this.checkType = value;
				if (this.checkType < 1 || this.checkType > 6)
				{
					this.checkType = 2;
				}
				base.Text = RadioCheckField.typeChars[this.checkType - 1];
				base.Font = BaseFont.CreateFont("ZapfDingbats", "Cp1252", false);
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x0014D8D3 File Offset: 0x0014C8D3
		// (set) Token: 0x060035CA RID: 13770 RVA: 0x0014D8DB File Offset: 0x0014C8DB
		public string OnValue
		{
			get
			{
				return this.onValue;
			}
			set
			{
				this.onValue = value;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060035CB RID: 13771 RVA: 0x0014D8E4 File Offset: 0x0014C8E4
		// (set) Token: 0x060035CC RID: 13772 RVA: 0x0014D8EC File Offset: 0x0014C8EC
		public bool Checked
		{
			get
			{
				return this.vchecked;
			}
			set
			{
				this.vchecked = value;
			}
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x0014D8F8 File Offset: 0x0014C8F8
		public PdfAppearance GetAppearance(bool isRadio, bool on)
		{
			if (isRadio && this.checkType == 2)
			{
				return this.GetAppearanceRadioCircle(on);
			}
			PdfAppearance borderAppearance = base.GetBorderAppearance();
			if (!on)
			{
				return borderAppearance;
			}
			BaseFont realFont = base.RealFont;
			bool flag = this.borderStyle == 2 || this.borderStyle == 3;
			float num = this.box.Height - this.borderWidth * 2f;
			float num2 = this.borderWidth;
			if (flag)
			{
				num -= this.borderWidth * 2f;
				num2 *= 2f;
			}
			float num3 = flag ? (2f * this.borderWidth) : this.borderWidth;
			num3 = Math.Max(num3, 1f);
			float num4 = Math.Min(num2, num3);
			float num5 = this.box.Width - 2f * num4;
			float h = this.box.Height - 2f * num4;
			float num6 = this.fontSize;
			if (num6 == 0f)
			{
				float widthPoint = realFont.GetWidthPoint(this.text, 1f);
				if (widthPoint == 0f)
				{
					num6 = 12f;
				}
				else
				{
					num6 = num5 / widthPoint;
				}
				float val = num / realFont.GetFontDescriptor(1, 1f);
				num6 = Math.Min(num6, val);
			}
			borderAppearance.SaveState();
			borderAppearance.Rectangle(num4, num4, num5, h);
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
			borderAppearance.SetFontAndSize(realFont, num6);
			borderAppearance.SetTextMatrix((this.box.Width - realFont.GetWidthPoint(this.text, num6)) / 2f, (this.box.Height - realFont.GetAscentPoint(this.text, num6)) / 2f);
			borderAppearance.ShowText(this.text);
			borderAppearance.EndText();
			borderAppearance.RestoreState();
			return borderAppearance;
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x0014DAE0 File Offset: 0x0014CAE0
		public PdfAppearance GetAppearanceRadioCircle(bool on)
		{
			PdfAppearance pdfAppearance = PdfAppearance.CreateAppearance(this.writer, this.box.Width, this.box.Height);
			int rotation = this.rotation;
			if (rotation != 90)
			{
				if (rotation != 180)
				{
					if (rotation == 270)
					{
						pdfAppearance.SetMatrix(0f, -1f, 1f, 0f, 0f, this.box.Width);
					}
				}
				else
				{
					pdfAppearance.SetMatrix(-1f, 0f, 0f, -1f, this.box.Width, this.box.Height);
				}
			}
			else
			{
				pdfAppearance.SetMatrix(0f, 1f, -1f, 0f, this.box.Height, 0f);
			}
			Rectangle rectangle = new Rectangle(pdfAppearance.BoundingBox);
			float x = rectangle.Width / 2f;
			float y = rectangle.Height / 2f;
			float num = (Math.Min(rectangle.Width, rectangle.Height) - this.borderWidth) / 2f;
			if (num <= 0f)
			{
				return pdfAppearance;
			}
			if (this.backgroundColor != null)
			{
				pdfAppearance.SetColorFill(this.backgroundColor);
				pdfAppearance.Circle(x, y, num + this.borderWidth / 2f);
				pdfAppearance.Fill();
			}
			if (this.borderWidth > 0f && this.borderColor != null)
			{
				pdfAppearance.SetLineWidth(this.borderWidth);
				pdfAppearance.SetColorStroke(this.borderColor);
				pdfAppearance.Circle(x, y, num);
				pdfAppearance.Stroke();
			}
			if (on)
			{
				if (this.textColor == null)
				{
					pdfAppearance.ResetGrayFill();
				}
				else
				{
					pdfAppearance.SetColorFill(this.textColor);
				}
				pdfAppearance.Circle(x, y, num / 2f);
				pdfAppearance.Fill();
			}
			return pdfAppearance;
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x0014DCB4 File Offset: 0x0014CCB4
		public PdfFormField GetRadioGroup(bool noToggleToOff, bool radiosInUnison)
		{
			PdfFormField pdfFormField = PdfFormField.CreateRadioButton(this.writer, noToggleToOff);
			if (radiosInUnison)
			{
				pdfFormField.SetFieldFlags(33554432);
			}
			pdfFormField.FieldName = this.fieldName;
			if ((this.options & 1) != 0)
			{
				pdfFormField.SetFieldFlags(1);
			}
			if ((this.options & 2) != 0)
			{
				pdfFormField.SetFieldFlags(2);
			}
			pdfFormField.ValueAsName = (this.vchecked ? this.onValue : "Off");
			return pdfFormField;
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x0014DD29 File Offset: 0x0014CD29
		public PdfFormField RadioField
		{
			get
			{
				return this.GetField(true);
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060035D1 RID: 13777 RVA: 0x0014DD32 File Offset: 0x0014CD32
		public PdfFormField CheckField
		{
			get
			{
				return this.GetField(false);
			}
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x0014DD3C File Offset: 0x0014CD3C
		protected PdfFormField GetField(bool isRadio)
		{
			PdfFormField pdfFormField;
			if (isRadio)
			{
				pdfFormField = PdfFormField.CreateEmpty(this.writer);
			}
			else
			{
				pdfFormField = PdfFormField.CreateCheckBox(this.writer);
			}
			pdfFormField.SetWidget(this.box, PdfAnnotation.HIGHLIGHT_INVERT);
			if (!isRadio)
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
				pdfFormField.ValueAsName = (this.vchecked ? this.onValue : "Off");
				this.CheckType = 1;
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
			PdfAppearance appearance = this.GetAppearance(isRadio, true);
			PdfAppearance appearance2 = this.GetAppearance(isRadio, false);
			pdfFormField.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, this.onValue, appearance);
			pdfFormField.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, "Off", appearance2);
			pdfFormField.AppearanceState = (this.vchecked ? this.onValue : "Off");
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

		// Token: 0x04002430 RID: 9264
		public const int TYPE_CHECK = 1;

		// Token: 0x04002431 RID: 9265
		public const int TYPE_CIRCLE = 2;

		// Token: 0x04002432 RID: 9266
		public const int TYPE_CROSS = 3;

		// Token: 0x04002433 RID: 9267
		public const int TYPE_DIAMOND = 4;

		// Token: 0x04002434 RID: 9268
		public const int TYPE_SQUARE = 5;

		// Token: 0x04002435 RID: 9269
		public const int TYPE_STAR = 6;

		// Token: 0x04002436 RID: 9270
		private static string[] typeChars = new string[]
		{
			"4",
			"l",
			"8",
			"u",
			"n",
			"H"
		};

		// Token: 0x04002437 RID: 9271
		private int checkType;

		// Token: 0x04002438 RID: 9272
		private string onValue;

		// Token: 0x04002439 RID: 9273
		private bool vchecked;
	}
}
