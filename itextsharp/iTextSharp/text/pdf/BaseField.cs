using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020003B9 RID: 953
	public abstract class BaseField
	{
		// Token: 0x060020F9 RID: 8441 RVA: 0x000C6CC0 File Offset: 0x000C5CC0
		static BaseField()
		{
			foreach (KeyValuePair<PdfName, int> keyValuePair in PdfCopyFieldsImp.fieldKeys)
			{
				BaseField.fieldKeys[keyValuePair.Key] = keyValuePair.Value;
			}
			BaseField.fieldKeys[PdfName.T] = 1;
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000C6D40 File Offset: 0x000C5D40
		public BaseField(PdfWriter writer, Rectangle box, string fieldName)
		{
			this.writer = writer;
			this.Box = box;
			this.fieldName = fieldName;
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x000C6D68 File Offset: 0x000C5D68
		protected BaseFont RealFont
		{
			get
			{
				if (this.font == null)
				{
					return BaseFont.CreateFont("Helvetica", "Cp1252", false);
				}
				return this.font;
			}
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000C6D8C File Offset: 0x000C5D8C
		protected PdfAppearance GetBorderAppearance()
		{
			PdfAppearance pdfAppearance = PdfAppearance.CreateAppearance(this.writer, this.box.Width, this.box.Height);
			int num = this.rotation;
			if (num != 90)
			{
				if (num != 180)
				{
					if (num == 270)
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
			pdfAppearance.SaveState();
			if (this.backgroundColor != null)
			{
				pdfAppearance.SetColorFill(this.backgroundColor);
				pdfAppearance.Rectangle(0f, 0f, this.box.Width, this.box.Height);
				pdfAppearance.Fill();
			}
			if (this.borderStyle == 4)
			{
				if (this.borderWidth != 0f && this.borderColor != null)
				{
					pdfAppearance.SetColorStroke(this.borderColor);
					pdfAppearance.SetLineWidth(this.borderWidth);
					pdfAppearance.MoveTo(0f, this.borderWidth / 2f);
					pdfAppearance.LineTo(this.box.Width, this.borderWidth / 2f);
					pdfAppearance.Stroke();
				}
			}
			else if (this.borderStyle == 2)
			{
				if (this.borderWidth != 0f && this.borderColor != null)
				{
					pdfAppearance.SetColorStroke(this.borderColor);
					pdfAppearance.SetLineWidth(this.borderWidth);
					pdfAppearance.Rectangle(this.borderWidth / 2f, this.borderWidth / 2f, this.box.Width - this.borderWidth, this.box.Height - this.borderWidth);
					pdfAppearance.Stroke();
				}
				BaseColor white = this.backgroundColor;
				if (white == null)
				{
					white = BaseColor.WHITE;
				}
				pdfAppearance.SetGrayFill(1f);
				this.DrawTopFrame(pdfAppearance);
				pdfAppearance.SetColorFill(white.Darker());
				this.DrawBottomFrame(pdfAppearance);
			}
			else if (this.borderStyle == 3)
			{
				if (this.borderWidth != 0f && this.borderColor != null)
				{
					pdfAppearance.SetColorStroke(this.borderColor);
					pdfAppearance.SetLineWidth(this.borderWidth);
					pdfAppearance.Rectangle(this.borderWidth / 2f, this.borderWidth / 2f, this.box.Width - this.borderWidth, this.box.Height - this.borderWidth);
					pdfAppearance.Stroke();
				}
				pdfAppearance.SetGrayFill(0.5f);
				this.DrawTopFrame(pdfAppearance);
				pdfAppearance.SetGrayFill(0.75f);
				this.DrawBottomFrame(pdfAppearance);
			}
			else if (this.borderWidth != 0f && this.borderColor != null)
			{
				if (this.borderStyle == 1)
				{
					pdfAppearance.SetLineDash(3f, 0f);
				}
				pdfAppearance.SetColorStroke(this.borderColor);
				pdfAppearance.SetLineWidth(this.borderWidth);
				pdfAppearance.Rectangle(this.borderWidth / 2f, this.borderWidth / 2f, this.box.Width - this.borderWidth, this.box.Height - this.borderWidth);
				pdfAppearance.Stroke();
				if ((this.options & 16777216) != 0 && this.maxCharacterLength > 1)
				{
					float num2 = this.box.Width / (float)this.maxCharacterLength;
					float y = this.borderWidth / 2f;
					float y2 = this.box.Height - this.borderWidth / 2f;
					for (int i = 1; i < this.maxCharacterLength; i++)
					{
						float x = num2 * (float)i;
						pdfAppearance.MoveTo(x, y);
						pdfAppearance.LineTo(x, y2);
					}
					pdfAppearance.Stroke();
				}
			}
			pdfAppearance.RestoreState();
			return pdfAppearance;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000C71B0 File Offset: 0x000C61B0
		protected static List<string> GetHardBreaks(string text)
		{
			List<string> list = new List<string>();
			char[] array = text.ToCharArray();
			int num = array.Length;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				char c = array[i];
				if (c == '\r')
				{
					if (i + 1 < num && array[i + 1] == '\n')
					{
						i++;
					}
					list.Add(stringBuilder.ToString());
					stringBuilder = new StringBuilder();
				}
				else if (c == '\n')
				{
					list.Add(stringBuilder.ToString());
					stringBuilder = new StringBuilder();
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			list.Add(stringBuilder.ToString());
			return list;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000C724C File Offset: 0x000C624C
		protected static void TrimRight(StringBuilder buf)
		{
			int num = buf.Length;
			while (num != 0)
			{
				if (buf[--num] != ' ')
				{
					return;
				}
				buf.Length = num;
			}
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000C727C File Offset: 0x000C627C
		protected static List<string> BreakLines(List<string> breaks, BaseFont font, float fontSize, float width)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < breaks.Count; i++)
			{
				stringBuilder.Length = 0;
				float num = 0f;
				char[] array = breaks[i].ToCharArray();
				int num2 = array.Length;
				int num3 = 0;
				int num4 = -1;
				int num5 = 0;
				for (int j = 0; j < num2; j++)
				{
					char c = array[j];
					switch (num3)
					{
					case 0:
						num += font.GetWidthPoint((int)c, fontSize);
						stringBuilder.Append(c);
						if (num > width)
						{
							num = 0f;
							if (stringBuilder.Length > 1)
							{
								j--;
								stringBuilder.Length--;
							}
							list.Add(stringBuilder.ToString());
							stringBuilder.Length = 0;
							num5 = j;
							if (c == ' ')
							{
								num3 = 2;
							}
							else
							{
								num3 = 1;
							}
						}
						else if (c != ' ')
						{
							num3 = 1;
						}
						break;
					case 1:
						num += font.GetWidthPoint((int)c, fontSize);
						stringBuilder.Append(c);
						if (c == ' ')
						{
							num4 = j;
						}
						if (num > width)
						{
							num = 0f;
							if (num4 >= 0)
							{
								j = num4;
								stringBuilder.Length = num4 - num5;
								BaseField.TrimRight(stringBuilder);
								list.Add(stringBuilder.ToString());
								stringBuilder.Length = 0;
								num5 = j;
								num4 = -1;
								num3 = 2;
							}
							else
							{
								if (stringBuilder.Length > 1)
								{
									j--;
									stringBuilder.Length--;
								}
								list.Add(stringBuilder.ToString());
								stringBuilder.Length = 0;
								num5 = j;
								if (c == ' ')
								{
									num3 = 2;
								}
							}
						}
						break;
					case 2:
						if (c != ' ')
						{
							num = 0f;
							j--;
							num3 = 1;
						}
						break;
					}
				}
				BaseField.TrimRight(stringBuilder);
				list.Add(stringBuilder.ToString());
			}
			return list;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000C745C File Offset: 0x000C645C
		private void DrawTopFrame(PdfAppearance app)
		{
			app.MoveTo(this.borderWidth, this.borderWidth);
			app.LineTo(this.borderWidth, this.box.Height - this.borderWidth);
			app.LineTo(this.box.Width - this.borderWidth, this.box.Height - this.borderWidth);
			app.LineTo(this.box.Width - 2f * this.borderWidth, this.box.Height - 2f * this.borderWidth);
			app.LineTo(2f * this.borderWidth, this.box.Height - 2f * this.borderWidth);
			app.LineTo(2f * this.borderWidth, 2f * this.borderWidth);
			app.LineTo(this.borderWidth, this.borderWidth);
			app.Fill();
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000C755C File Offset: 0x000C655C
		private void DrawBottomFrame(PdfAppearance app)
		{
			app.MoveTo(this.borderWidth, this.borderWidth);
			app.LineTo(this.box.Width - this.borderWidth, this.borderWidth);
			app.LineTo(this.box.Width - this.borderWidth, this.box.Height - this.borderWidth);
			app.LineTo(this.box.Width - 2f * this.borderWidth, this.box.Height - 2f * this.borderWidth);
			app.LineTo(this.box.Width - 2f * this.borderWidth, 2f * this.borderWidth);
			app.LineTo(2f * this.borderWidth, 2f * this.borderWidth);
			app.LineTo(this.borderWidth, this.borderWidth);
			app.Fill();
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x000C7662 File Offset: 0x000C6662
		// (set) Token: 0x06002102 RID: 8450 RVA: 0x000C7659 File Offset: 0x000C6659
		public float BorderWidth
		{
			get
			{
				return this.borderWidth;
			}
			set
			{
				this.borderWidth = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x000C7673 File Offset: 0x000C6673
		// (set) Token: 0x06002104 RID: 8452 RVA: 0x000C766A File Offset: 0x000C666A
		public int BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				this.borderStyle = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x000C7684 File Offset: 0x000C6684
		// (set) Token: 0x06002106 RID: 8454 RVA: 0x000C767B File Offset: 0x000C667B
		public BaseColor BorderColor
		{
			get
			{
				return this.borderColor;
			}
			set
			{
				this.borderColor = value;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x000C7695 File Offset: 0x000C6695
		// (set) Token: 0x06002108 RID: 8456 RVA: 0x000C768C File Offset: 0x000C668C
		public BaseColor BackgroundColor
		{
			get
			{
				return this.backgroundColor;
			}
			set
			{
				this.backgroundColor = value;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x000C76A6 File Offset: 0x000C66A6
		// (set) Token: 0x0600210A RID: 8458 RVA: 0x000C769D File Offset: 0x000C669D
		public BaseColor TextColor
		{
			get
			{
				return this.textColor;
			}
			set
			{
				this.textColor = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x000C76B7 File Offset: 0x000C66B7
		// (set) Token: 0x0600210C RID: 8460 RVA: 0x000C76AE File Offset: 0x000C66AE
		public BaseFont Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x000C76C8 File Offset: 0x000C66C8
		// (set) Token: 0x0600210E RID: 8462 RVA: 0x000C76BF File Offset: 0x000C66BF
		public float FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				this.fontSize = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x000C76D9 File Offset: 0x000C66D9
		// (set) Token: 0x06002110 RID: 8464 RVA: 0x000C76D0 File Offset: 0x000C66D0
		public int Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				this.alignment = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x000C76EA File Offset: 0x000C66EA
		// (set) Token: 0x06002112 RID: 8466 RVA: 0x000C76E1 File Offset: 0x000C66E1
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000C7716 File Offset: 0x000C6716
		// (set) Token: 0x06002114 RID: 8468 RVA: 0x000C76F2 File Offset: 0x000C66F2
		public Rectangle Box
		{
			get
			{
				return this.box;
			}
			set
			{
				if (value == null)
				{
					this.box = null;
					return;
				}
				this.box = new Rectangle(value);
				this.box.Normalize();
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x000C775E File Offset: 0x000C675E
		// (set) Token: 0x06002116 RID: 8470 RVA: 0x000C771E File Offset: 0x000C671E
		public int Rotation
		{
			get
			{
				return this.rotation;
			}
			set
			{
				if (value % 90 != 0)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("rotation.must.be.a.multiple.of.90"));
				}
				this.rotation = value % 360;
				if (this.rotation < 0)
				{
					this.rotation += 360;
				}
			}
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000C7766 File Offset: 0x000C6766
		public void SetRotationFromPage(Rectangle page)
		{
			this.Rotation = page.Rotation;
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x000C777D File Offset: 0x000C677D
		// (set) Token: 0x06002119 RID: 8473 RVA: 0x000C7774 File Offset: 0x000C6774
		public int Visibility
		{
			get
			{
				return this.visibility;
			}
			set
			{
				this.visibility = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x000C778E File Offset: 0x000C678E
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x000C7785 File Offset: 0x000C6785
		public string FieldName
		{
			get
			{
				return this.fieldName;
			}
			set
			{
				this.fieldName = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x000C779F File Offset: 0x000C679F
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x000C7796 File Offset: 0x000C6796
		public int Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x000C77B0 File Offset: 0x000C67B0
		// (set) Token: 0x0600211F RID: 8479 RVA: 0x000C77A7 File Offset: 0x000C67A7
		public int MaxCharacterLength
		{
			get
			{
				return this.maxCharacterLength;
			}
			set
			{
				this.maxCharacterLength = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x000C77B8 File Offset: 0x000C67B8
		// (set) Token: 0x06002122 RID: 8482 RVA: 0x000C77C0 File Offset: 0x000C67C0
		public PdfWriter Writer
		{
			get
			{
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000C77CC File Offset: 0x000C67CC
		public static void MoveFields(PdfDictionary from, PdfDictionary to)
		{
			PdfName[] array = new PdfName[from.Size];
			from.Keys.CopyTo(array, 0);
			foreach (PdfName key in array)
			{
				if (BaseField.fieldKeys.ContainsKey(key))
				{
					if (to != null)
					{
						to.Put(key, from.Get(key));
					}
					from.Remove(key);
				}
			}
		}

		// Token: 0x040016B9 RID: 5817
		public const float BORDER_WIDTH_THIN = 1f;

		// Token: 0x040016BA RID: 5818
		public const float BORDER_WIDTH_MEDIUM = 2f;

		// Token: 0x040016BB RID: 5819
		public const float BORDER_WIDTH_THICK = 3f;

		// Token: 0x040016BC RID: 5820
		public const int VISIBLE = 0;

		// Token: 0x040016BD RID: 5821
		public const int HIDDEN = 1;

		// Token: 0x040016BE RID: 5822
		public const int VISIBLE_BUT_DOES_NOT_PRINT = 2;

		// Token: 0x040016BF RID: 5823
		public const int HIDDEN_BUT_PRINTABLE = 3;

		// Token: 0x040016C0 RID: 5824
		public const int READ_ONLY = 1;

		// Token: 0x040016C1 RID: 5825
		public const int REQUIRED = 2;

		// Token: 0x040016C2 RID: 5826
		public const int MULTILINE = 4096;

		// Token: 0x040016C3 RID: 5827
		public const int DO_NOT_SCROLL = 8388608;

		// Token: 0x040016C4 RID: 5828
		public const int PASSWORD = 8192;

		// Token: 0x040016C5 RID: 5829
		public const int FILE_SELECTION = 1048576;

		// Token: 0x040016C6 RID: 5830
		public const int DO_NOT_SPELL_CHECK = 4194304;

		// Token: 0x040016C7 RID: 5831
		public const int EDIT = 262144;

		// Token: 0x040016C8 RID: 5832
		public const int MULTISELECT = 2097152;

		// Token: 0x040016C9 RID: 5833
		public const int COMB = 16777216;

		// Token: 0x040016CA RID: 5834
		protected float borderWidth = 1f;

		// Token: 0x040016CB RID: 5835
		protected int borderStyle;

		// Token: 0x040016CC RID: 5836
		protected BaseColor borderColor;

		// Token: 0x040016CD RID: 5837
		protected BaseColor backgroundColor;

		// Token: 0x040016CE RID: 5838
		protected BaseColor textColor;

		// Token: 0x040016CF RID: 5839
		protected BaseFont font;

		// Token: 0x040016D0 RID: 5840
		protected float fontSize;

		// Token: 0x040016D1 RID: 5841
		protected int alignment;

		// Token: 0x040016D2 RID: 5842
		protected PdfWriter writer;

		// Token: 0x040016D3 RID: 5843
		protected string text;

		// Token: 0x040016D4 RID: 5844
		protected Rectangle box;

		// Token: 0x040016D5 RID: 5845
		protected int rotation;

		// Token: 0x040016D6 RID: 5846
		protected int visibility;

		// Token: 0x040016D7 RID: 5847
		protected string fieldName;

		// Token: 0x040016D8 RID: 5848
		protected int options;

		// Token: 0x040016D9 RID: 5849
		protected int maxCharacterLength;

		// Token: 0x040016DA RID: 5850
		private static Dictionary<PdfName, int> fieldKeys = new Dictionary<PdfName, int>();
	}
}
