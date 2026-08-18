using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iTextSharp.text.error_messages;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000495 RID: 1173
	public class PdfSignatureAppearance
	{
		// Token: 0x060027AE RID: 10158 RVA: 0x000EE8EE File Offset: 0x000ED8EE
		internal PdfSignatureAppearance(PdfStamperImp writer)
		{
			this.writer = writer;
			this.signDate = DateTime.Now;
			this.fieldName = this.GetNewSigName();
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x000EE92E File Offset: 0x000ED92E
		// (set) Token: 0x060027B0 RID: 10160 RVA: 0x000EE936 File Offset: 0x000ED936
		public PdfSignatureAppearance.SignatureRender Render
		{
			get
			{
				return this.render;
			}
			set
			{
				this.render = value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x000EE93F File Offset: 0x000ED93F
		// (set) Token: 0x060027B2 RID: 10162 RVA: 0x000EE947 File Offset: 0x000ED947
		public Image SignatureGraphic
		{
			get
			{
				return this.signatureGraphic;
			}
			set
			{
				this.signatureGraphic = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000EE950 File Offset: 0x000ED950
		// (set) Token: 0x060027B4 RID: 10164 RVA: 0x000EE958 File Offset: 0x000ED958
		public string Layer2Text
		{
			get
			{
				return this.layer2Text;
			}
			set
			{
				this.layer2Text = value;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000EE961 File Offset: 0x000ED961
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x000EE969 File Offset: 0x000ED969
		public string Layer4Text
		{
			get
			{
				return this.layer4Text;
			}
			set
			{
				this.layer4Text = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000EE972 File Offset: 0x000ED972
		public Rectangle Rect
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000EE97A File Offset: 0x000ED97A
		public bool IsInvisible()
		{
			return this.rect == null || this.rect.Width == 0f || this.rect.Height == 0f;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000EE9AA File Offset: 0x000ED9AA
		public void SetCrypto(ICipherParameters privKey, X509Certificate[] certChain, object[] crlList, PdfName filter)
		{
			this.privKey = privKey;
			this.certChain = certChain;
			this.crlList = crlList;
			this.filter = filter;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000EE9CC File Offset: 0x000ED9CC
		public void SetVisibleSignature(Rectangle pageRect, int page, string fieldName)
		{
			if (fieldName != null)
			{
				if (fieldName.IndexOf('.') >= 0)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("field.names.cannot.contain.a.dot"));
				}
				AcroFields acroFields = this.writer.AcroFields;
				AcroFields.Item fieldItem = acroFields.GetFieldItem(fieldName);
				if (fieldItem != null)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("the.field.1.already.exists", fieldName));
				}
				this.fieldName = fieldName;
			}
			if (page < 1 || page > this.writer.reader.NumberOfPages)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.page.number.1", page));
			}
			this.pageRect = new Rectangle(pageRect);
			this.pageRect.Normalize();
			this.rect = new Rectangle(this.pageRect.Width, this.pageRect.Height);
			this.page = page;
			this.newField = true;
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000EEA9C File Offset: 0x000EDA9C
		public void SetVisibleSignature(string fieldName)
		{
			AcroFields acroFields = this.writer.AcroFields;
			AcroFields.Item fieldItem = acroFields.GetFieldItem(fieldName);
			if (fieldItem == null)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.field.1.does.not.exist", fieldName));
			}
			PdfDictionary merged = fieldItem.GetMerged(0);
			if (!PdfName.SIG.Equals(PdfReader.GetPdfObject(merged.Get(PdfName.FT))))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.field.1.is.not.a.signature.field", fieldName));
			}
			this.fieldName = fieldName;
			PdfArray asArray = merged.GetAsArray(PdfName.RECT);
			float floatValue = asArray.GetAsNumber(0).FloatValue;
			float floatValue2 = asArray.GetAsNumber(1).FloatValue;
			float floatValue3 = asArray.GetAsNumber(2).FloatValue;
			float floatValue4 = asArray.GetAsNumber(3).FloatValue;
			this.pageRect = new Rectangle(floatValue, floatValue2, floatValue3, floatValue4);
			this.pageRect.Normalize();
			this.page = fieldItem.GetPage(0);
			int pageRotation = this.writer.reader.GetPageRotation(this.page);
			Rectangle pageSizeWithRotation = this.writer.reader.GetPageSizeWithRotation(this.page);
			int num = pageRotation;
			if (num != 90)
			{
				if (num != 180)
				{
					if (num == 270)
					{
						this.pageRect = new Rectangle(pageSizeWithRotation.Right - this.pageRect.Bottom, this.pageRect.Left, pageSizeWithRotation.Right - this.pageRect.Top, this.pageRect.Right);
					}
				}
				else
				{
					this.pageRect = new Rectangle(pageSizeWithRotation.Right - this.pageRect.Left, pageSizeWithRotation.Top - this.pageRect.Bottom, pageSizeWithRotation.Right - this.pageRect.Right, pageSizeWithRotation.Top - this.pageRect.Top);
				}
			}
			else
			{
				this.pageRect = new Rectangle(this.pageRect.Bottom, pageSizeWithRotation.Top - this.pageRect.Left, this.pageRect.Top, pageSizeWithRotation.Top - this.pageRect.Right);
			}
			if (pageRotation != 0)
			{
				this.pageRect.Normalize();
			}
			this.rect = new Rectangle(this.pageRect.Width, this.pageRect.Height);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000EECEC File Offset: 0x000EDCEC
		public PdfTemplate GetLayer(int layer)
		{
			if (layer < 0 || layer >= this.app.Length)
			{
				return null;
			}
			PdfTemplate pdfTemplate = this.app[layer];
			if (pdfTemplate == null)
			{
				pdfTemplate = (this.app[layer] = new PdfTemplate(this.writer));
				pdfTemplate.BoundingBox = this.rect;
				this.writer.AddDirectTemplateSimple(pdfTemplate, new PdfName("n" + layer));
			}
			return pdfTemplate;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000EED5C File Offset: 0x000EDD5C
		public PdfTemplate GetTopLayer()
		{
			if (this.frm == null)
			{
				this.frm = new PdfTemplate(this.writer);
				this.frm.BoundingBox = this.rect;
				this.writer.AddDirectTemplateSimple(this.frm, new PdfName("FRM"));
			}
			return this.frm;
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000EEDB8 File Offset: 0x000EDDB8
		public PdfTemplate GetAppearance()
		{
			if (this.IsInvisible())
			{
				PdfTemplate pdfTemplate = new PdfTemplate(this.writer);
				pdfTemplate.BoundingBox = new Rectangle(0f, 0f);
				this.writer.AddDirectTemplateSimple(pdfTemplate, null);
				return pdfTemplate;
			}
			if (this.app[0] == null)
			{
				PdfTemplate pdfTemplate2 = this.app[0] = new PdfTemplate(this.writer);
				pdfTemplate2.BoundingBox = new Rectangle(100f, 100f);
				this.writer.AddDirectTemplateSimple(pdfTemplate2, new PdfName("n0"));
				pdfTemplate2.SetLiteral("% DSBlank\n");
			}
			if (this.app[1] == null && !this.acro6Layers)
			{
				PdfTemplate pdfTemplate3 = this.app[1] = new PdfTemplate(this.writer);
				pdfTemplate3.BoundingBox = new Rectangle(100f, 100f);
				this.writer.AddDirectTemplateSimple(pdfTemplate3, new PdfName("n1"));
				pdfTemplate3.SetLiteral("% DSUnknown\nq\n1 G\n1 g\n0.1 0 0 0.1 9 0 cm\n0 J 0 j 4 M []0 d\n1 i \n0 g\n313 292 m\n313 404 325 453 432 529 c\n478 561 504 597 504 645 c\n504 736 440 760 391 760 c\n286 760 271 681 265 626 c\n265 625 l\n100 625 l\n100 828 253 898 381 898 c\n451 898 679 878 679 650 c\n679 555 628 499 538 435 c\n488 399 467 376 467 292 c\n313 292 l\nh\n308 214 170 -164 re\nf\n0.44 G\n1.2 w\n1 1 0.4 rg\n287 318 m\n287 430 299 479 406 555 c\n451 587 478 623 478 671 c\n478 762 414 786 365 786 c\n260 786 245 707 239 652 c\n239 651 l\n74 651 l\n74 854 227 924 355 924 c\n425 924 653 904 653 676 c\n653 581 602 525 512 461 c\n462 425 441 402 441 318 c\n287 318 l\nh\n282 240 170 -164 re\nB\nQ\n");
			}
			if (this.app[2] == null)
			{
				string text;
				if (this.layer2Text == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("Digitally signed by ").Append(PdfPKCS7.GetSubjectFields(this.certChain[0]).GetField("CN")).Append('\n');
					stringBuilder.Append("Date: ").Append(this.signDate.ToString("yyyy.MM.dd HH:mm:ss zzz"));
					if (this.reason != null)
					{
						stringBuilder.Append('\n').Append("Reason: ").Append(this.reason);
					}
					if (this.location != null)
					{
						stringBuilder.Append('\n').Append("Location: ").Append(this.location);
					}
					text = stringBuilder.ToString();
				}
				else
				{
					text = this.layer2Text;
				}
				PdfTemplate pdfTemplate4 = this.app[2] = new PdfTemplate(this.writer);
				pdfTemplate4.BoundingBox = this.rect;
				this.writer.AddDirectTemplateSimple(pdfTemplate4, new PdfName("n2"));
				if (this.image != null)
				{
					if (this.imageScale == 0f)
					{
						pdfTemplate4.AddImage(this.image, this.rect.Width, 0f, 0f, this.rect.Height, 0f, 0f);
					}
					else
					{
						float num = this.imageScale;
						if (this.imageScale < 0f)
						{
							num = Math.Min(this.rect.Width / this.image.Width, this.rect.Height / this.image.Height);
						}
						float num2 = this.image.Width * num;
						float num3 = this.image.Height * num;
						float e = (this.rect.Width - num2) / 2f;
						float f = (this.rect.Height - num3) / 2f;
						pdfTemplate4.AddImage(this.image, num2, 0f, 0f, num3, e, f);
					}
				}
				Font font;
				if (this.layer2Font == null)
				{
					font = new Font();
				}
				else
				{
					font = new Font(this.layer2Font);
				}
				float num4 = font.Size;
				Rectangle rectangle = null;
				Rectangle rectangle2 = null;
				if (this.Render == PdfSignatureAppearance.SignatureRender.NameAndDescription || (this.Render == PdfSignatureAppearance.SignatureRender.GraphicAndDescription && this.SignatureGraphic != null))
				{
					rectangle2 = new Rectangle(2f, 2f, this.rect.Width / 2f - 2f, this.rect.Height - 2f);
					rectangle = new Rectangle(this.rect.Width / 2f + 1f, 2f, this.rect.Width - 1f, this.rect.Height - 2f);
					if (this.rect.Height > this.rect.Width)
					{
						rectangle2 = new Rectangle(2f, this.rect.Height / 2f, this.rect.Width - 2f, this.rect.Height);
						rectangle = new Rectangle(2f, 2f, this.rect.Width - 2f, this.rect.Height / 2f - 2f);
					}
				}
				else if (this.render == PdfSignatureAppearance.SignatureRender.Graphic)
				{
					if (this.signatureGraphic == null)
					{
						throw new InvalidOperationException(MessageLocalization.GetComposedMessage("a.signature.image.should.be.present.when.rendering.mode.is.graphic.only"));
					}
					rectangle2 = new Rectangle(2f, 2f, this.rect.Width - 2f, this.rect.Height - 2f);
				}
				else
				{
					rectangle = new Rectangle(2f, 2f, this.rect.Width - 2f, this.rect.Height * 0.7f - 2f);
				}
				if (this.Render == PdfSignatureAppearance.SignatureRender.NameAndDescription)
				{
					string field = PdfPKCS7.GetSubjectFields(this.certChain[0]).GetField("CN");
					Rectangle rectangle3 = new Rectangle(rectangle2.Width - 2f, rectangle2.Height - 2f);
					float leading = PdfSignatureAppearance.FitText(font, field, rectangle3, -1f, this.runDirection);
					ColumnText columnText = new ColumnText(pdfTemplate4);
					columnText.RunDirection = this.runDirection;
					columnText.SetSimpleColumn(new Phrase(field, font), rectangle2.Left, rectangle2.Bottom, rectangle2.Right, rectangle2.Top, leading, 0);
					columnText.Go();
				}
				else if (this.Render == PdfSignatureAppearance.SignatureRender.GraphicAndDescription)
				{
					ColumnText columnText2 = new ColumnText(pdfTemplate4);
					columnText2.RunDirection = this.runDirection;
					columnText2.SetSimpleColumn(rectangle2.Left, rectangle2.Bottom, rectangle2.Right, rectangle2.Top, 0f, 2);
					Image instance = Image.GetInstance(this.SignatureGraphic);
					instance.ScaleToFit(rectangle2.Width, rectangle2.Height);
					Paragraph paragraph = new Paragraph();
					float num5 = 0f;
					float num6 = -instance.ScaledHeight + 15f;
					num5 += (rectangle2.Width - instance.ScaledWidth) / 2f;
					num6 -= (rectangle2.Height - instance.ScaledHeight) / 2f;
					paragraph.Add(new Chunk(instance, num5 + (rectangle2.Width - instance.ScaledWidth) / 2f, num6, false));
					columnText2.AddElement(paragraph);
					columnText2.Go();
				}
				else if (this.render == PdfSignatureAppearance.SignatureRender.Graphic)
				{
					ColumnText columnText3 = new ColumnText(pdfTemplate4);
					columnText3.RunDirection = this.runDirection;
					columnText3.SetSimpleColumn(rectangle2.Left, rectangle2.Bottom, rectangle2.Right, rectangle2.Top, 0f, 2);
					Image instance2 = Image.GetInstance(this.signatureGraphic);
					instance2.ScaleToFit(rectangle2.Width, rectangle2.Height);
					Paragraph paragraph2 = new Paragraph();
					float num7 = 0f;
					float num8 = -instance2.ScaledHeight + 15f;
					num7 += (rectangle2.Width - instance2.ScaledWidth) / 2f;
					num8 -= (rectangle2.Height - instance2.ScaledHeight) / 2f;
					paragraph2.Add(new Chunk(instance2, num7, num8, false));
					columnText3.AddElement(paragraph2);
					columnText3.Go();
				}
				if (this.render != PdfSignatureAppearance.SignatureRender.Graphic)
				{
					if (num4 <= 0f)
					{
						Rectangle rectangle4 = new Rectangle(rectangle.Width, rectangle.Height);
						num4 = PdfSignatureAppearance.FitText(font, text, rectangle4, 12f, this.runDirection);
					}
					ColumnText columnText4 = new ColumnText(pdfTemplate4);
					columnText4.RunDirection = this.runDirection;
					columnText4.SetSimpleColumn(new Phrase(text, font), rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Top, num4, 0);
					columnText4.Go();
				}
			}
			if (this.app[3] == null && !this.acro6Layers)
			{
				PdfTemplate pdfTemplate5 = this.app[3] = new PdfTemplate(this.writer);
				pdfTemplate5.BoundingBox = new Rectangle(100f, 100f);
				this.writer.AddDirectTemplateSimple(pdfTemplate5, new PdfName("n3"));
				pdfTemplate5.SetLiteral("% DSBlank\n");
			}
			if (this.app[4] == null && !this.acro6Layers)
			{
				PdfTemplate pdfTemplate6 = this.app[4] = new PdfTemplate(this.writer);
				pdfTemplate6.BoundingBox = new Rectangle(0f, this.rect.Height * 0.7f, this.rect.Right, this.rect.Top);
				this.writer.AddDirectTemplateSimple(pdfTemplate6, new PdfName("n4"));
				Font font2;
				if (this.layer2Font == null)
				{
					font2 = new Font();
				}
				else
				{
					font2 = new Font(this.layer2Font);
				}
				float leading2 = font2.Size;
				string text2 = "Signature Not Verified";
				if (this.layer4Text != null)
				{
					text2 = this.layer4Text;
				}
				Rectangle rectangle5 = new Rectangle(this.rect.Width - 4f, this.rect.Height * 0.3f - 4f);
				leading2 = PdfSignatureAppearance.FitText(font2, text2, rectangle5, 15f, this.runDirection);
				ColumnText columnText5 = new ColumnText(pdfTemplate6);
				columnText5.RunDirection = this.runDirection;
				columnText5.SetSimpleColumn(new Phrase(text2, font2), 2f, 0f, this.rect.Width - 2f, this.rect.Height - 2f, leading2, 0);
				columnText5.Go();
			}
			int pageRotation = this.writer.reader.GetPageRotation(this.page);
			Rectangle rectangle6 = new Rectangle(this.rect);
			for (int i = pageRotation; i > 0; i -= 90)
			{
				rectangle6 = rectangle6.Rotate();
			}
			if (this.frm == null)
			{
				this.frm = new PdfTemplate(this.writer);
				this.frm.BoundingBox = rectangle6;
				this.writer.AddDirectTemplateSimple(this.frm, new PdfName("FRM"));
				float num9 = Math.Min(this.rect.Width, this.rect.Height) * 0.9f;
				float e2 = (this.rect.Width - num9) / 2f;
				float f2 = (this.rect.Height - num9) / 2f;
				num9 /= 100f;
				if (pageRotation == 90)
				{
					this.frm.ConcatCTM(0f, 1f, -1f, 0f, this.rect.Height, 0f);
				}
				else if (pageRotation == 180)
				{
					this.frm.ConcatCTM(-1f, 0f, 0f, -1f, this.rect.Width, this.rect.Height);
				}
				else if (pageRotation == 270)
				{
					this.frm.ConcatCTM(0f, -1f, 1f, 0f, 0f, this.rect.Width);
				}
				this.frm.AddTemplate(this.app[0], 0f, 0f);
				if (!this.acro6Layers)
				{
					this.frm.AddTemplate(this.app[1], num9, 0f, 0f, num9, e2, f2);
				}
				this.frm.AddTemplate(this.app[2], 0f, 0f);
				if (!this.acro6Layers)
				{
					this.frm.AddTemplate(this.app[3], num9, 0f, 0f, num9, e2, f2);
					this.frm.AddTemplate(this.app[4], 0f, 0f);
				}
			}
			PdfTemplate pdfTemplate7 = new PdfTemplate(this.writer);
			pdfTemplate7.BoundingBox = rectangle6;
			this.writer.AddDirectTemplateSimple(pdfTemplate7, null);
			pdfTemplate7.AddTemplate(this.frm, 0f, 0f);
			return pdfTemplate7;
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000EFA24 File Offset: 0x000EEA24
		public static float FitText(Font font, string text, Rectangle rect, float maxFontSize, int runDirection)
		{
			if (maxFontSize <= 0f)
			{
				int num = 0;
				int num2 = 0;
				char[] array = text.ToCharArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == '\n')
					{
						num2++;
					}
					else if (array[i] == '\r')
					{
						num++;
					}
				}
				int num3 = Math.Max(num, num2) + 1;
				maxFontSize = Math.Abs(rect.Height) / (float)num3 - 0.001f;
			}
			font.Size = maxFontSize;
			Phrase phrase = new Phrase(text, font);
			ColumnText columnText = new ColumnText(null);
			columnText.SetSimpleColumn(phrase, rect.Left, rect.Bottom, rect.Right, rect.Top, maxFontSize, 0);
			columnText.RunDirection = runDirection;
			int num4 = columnText.Go(true);
			if ((num4 & 1) != 0)
			{
				return maxFontSize;
			}
			float num5 = 0.1f;
			float num6 = 0f;
			float num7 = maxFontSize;
			float num8 = maxFontSize;
			for (int j = 0; j < 50; j++)
			{
				num8 = (num6 + num7) / 2f;
				columnText = new ColumnText(null);
				font.Size = num8;
				columnText.SetSimpleColumn(new Phrase(text, font), rect.Left, rect.Bottom, rect.Right, rect.Top, num8, 0);
				columnText.RunDirection = runDirection;
				num4 = columnText.Go(true);
				if ((num4 & 1) != 0)
				{
					if (num7 - num6 < num8 * num5)
					{
						return num8;
					}
					num6 = num8;
				}
				else
				{
					num7 = num8;
				}
			}
			return num8;
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000EFB83 File Offset: 0x000EEB83
		public void SetExternalDigest(byte[] digest, byte[] RSAdata, string digestEncryptionAlgorithm)
		{
			this.externalDigest = digest;
			this.externalRSAdata = RSAdata;
			this.digestEncryptionAlgorithm = digestEncryptionAlgorithm;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000EFB9A File Offset: 0x000EEB9A
		// (set) Token: 0x060027C2 RID: 10178 RVA: 0x000EFBA2 File Offset: 0x000EEBA2
		public string Reason
		{
			get
			{
				return this.reason;
			}
			set
			{
				this.reason = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000EFBAB File Offset: 0x000EEBAB
		// (set) Token: 0x060027C4 RID: 10180 RVA: 0x000EFBB3 File Offset: 0x000EEBB3
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000EFBBC File Offset: 0x000EEBBC
		public ICipherParameters PrivKey
		{
			get
			{
				return this.privKey;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000EFBC4 File Offset: 0x000EEBC4
		public X509Certificate[] CertChain
		{
			get
			{
				return this.certChain;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000EFBCC File Offset: 0x000EEBCC
		public object[] CrlList
		{
			get
			{
				return this.crlList;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x000EFBD4 File Offset: 0x000EEBD4
		public PdfName Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000EFBDC File Offset: 0x000EEBDC
		public bool IsNewField()
		{
			return this.newField;
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x000EFBE4 File Offset: 0x000EEBE4
		public int Page
		{
			get
			{
				return this.page;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x000EFBEC File Offset: 0x000EEBEC
		public string FieldName
		{
			get
			{
				return this.fieldName;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x000EFBF4 File Offset: 0x000EEBF4
		public Rectangle PageRect
		{
			get
			{
				return this.pageRect;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x000EFBFC File Offset: 0x000EEBFC
		// (set) Token: 0x060027CE RID: 10190 RVA: 0x000EFC04 File Offset: 0x000EEC04
		public DateTime SignDate
		{
			get
			{
				return this.signDate;
			}
			set
			{
				this.signDate = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x000EFC0D File Offset: 0x000EEC0D
		// (set) Token: 0x060027D0 RID: 10192 RVA: 0x000EFC15 File Offset: 0x000EEC15
		internal ByteBuffer Sigout
		{
			get
			{
				return this.sigout;
			}
			set
			{
				this.sigout = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000EFC1E File Offset: 0x000EEC1E
		// (set) Token: 0x060027D2 RID: 10194 RVA: 0x000EFC26 File Offset: 0x000EEC26
		internal Stream Originalout
		{
			get
			{
				return this.originalout;
			}
			set
			{
				this.originalout = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x000EFC2F File Offset: 0x000EEC2F
		public string TempFile
		{
			get
			{
				return this.tempFile;
			}
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000EFC37 File Offset: 0x000EEC37
		internal void SetTempFile(string tempFile)
		{
			this.tempFile = tempFile;
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000EFC40 File Offset: 0x000EEC40
		public string GetNewSigName()
		{
			AcroFields acroFields = this.writer.AcroFields;
			string text = "Signature";
			int num = 0;
			bool flag = false;
			while (!flag)
			{
				num++;
				string text2 = text + num;
				if (acroFields.GetFieldItem(text2) == null)
				{
					text2 += ".";
					flag = true;
					foreach (string text3 in acroFields.Fields.Keys)
					{
						if (text3.StartsWith(text2))
						{
							flag = false;
							break;
						}
					}
				}
			}
			text += num;
			return text;
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000EFCF8 File Offset: 0x000EECF8
		public void PreClose()
		{
			this.PreClose(null);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000EFD04 File Offset: 0x000EED04
		public void PreClose(Dictionary<PdfName, int> exclusionSizes)
		{
			if (this.preClosed)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("document.already.pre.closed"));
			}
			this.preClosed = true;
			AcroFields acroFields = this.writer.AcroFields;
			string name = this.FieldName;
			bool flag = !this.IsInvisible() && !this.IsNewField();
			PdfIndirectReference pdfIndirectReference = this.writer.PdfIndirectReference;
			this.writer.SigFlags = 3;
			if (flag)
			{
				PdfDictionary widget = acroFields.GetFieldItem(name).GetWidget(0);
				this.writer.MarkUsed(widget);
				widget.Put(PdfName.P, this.writer.GetPageReference(this.Page));
				widget.Put(PdfName.V, pdfIndirectReference);
				PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(widget.Get(PdfName.F));
				int num = 0;
				if (pdfObjectRelease != null && pdfObjectRelease.IsNumber())
				{
					num = ((PdfNumber)pdfObjectRelease).IntValue;
				}
				num |= 128;
				widget.Put(PdfName.F, new PdfNumber(num));
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.N, this.GetAppearance().IndirectReference);
				widget.Put(PdfName.AP, pdfDictionary);
			}
			else
			{
				PdfFormField pdfFormField = PdfFormField.CreateSignature(this.writer);
				pdfFormField.FieldName = name;
				pdfFormField.Put(PdfName.V, pdfIndirectReference);
				pdfFormField.Flags = 132;
				int num2 = this.Page;
				if (!this.IsInvisible())
				{
					pdfFormField.SetWidget(this.PageRect, null);
				}
				else
				{
					pdfFormField.SetWidget(new Rectangle(0f, 0f), null);
				}
				pdfFormField.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, this.GetAppearance());
				pdfFormField.Page = num2;
				this.writer.AddAnnotation(pdfFormField, num2);
			}
			this.exclusionLocations = new Dictionary<PdfName, PdfLiteral>();
			if (this.cryptoDictionary == null)
			{
				if (PdfName.ADOBE_PPKLITE.Equals(this.Filter))
				{
					this.sigStandard = new PdfSigGenericPKCS.PPKLite();
				}
				else if (PdfName.ADOBE_PPKMS.Equals(this.Filter))
				{
					this.sigStandard = new PdfSigGenericPKCS.PPKMS();
				}
				else
				{
					if (!PdfName.VERISIGN_PPKVS.Equals(this.Filter))
					{
						throw new ArgumentException(MessageLocalization.GetComposedMessage("unknown.filter.1", this.Filter));
					}
					this.sigStandard = new PdfSigGenericPKCS.VeriSign();
				}
				this.sigStandard.SetExternalDigest(this.externalDigest, this.externalRSAdata, this.digestEncryptionAlgorithm);
				if (this.Reason != null)
				{
					this.sigStandard.Reason = this.Reason;
				}
				if (this.Location != null)
				{
					this.sigStandard.Location = this.Location;
				}
				if (this.Contact != null)
				{
					this.sigStandard.Contact = this.Contact;
				}
				this.sigStandard.Put(PdfName.M, new PdfDate(this.SignDate));
				this.sigStandard.SetSignInfo(this.PrivKey, this.CertChain, this.CrlList);
				PdfString pdfString = (PdfString)this.sigStandard.Get(PdfName.CONTENTS);
				PdfLiteral value = new PdfLiteral((pdfString.ToString().Length + (PdfName.ADOBE_PPKLITE.Equals(this.Filter) ? 0 : 64)) * 2 + 2);
				this.exclusionLocations[PdfName.CONTENTS] = value;
				this.sigStandard.Put(PdfName.CONTENTS, value);
				value = new PdfLiteral(80);
				this.exclusionLocations[PdfName.BYTERANGE] = value;
				this.sigStandard.Put(PdfName.BYTERANGE, value);
				if (this.certificationLevel > 0)
				{
					this.AddDocMDP(this.sigStandard);
				}
				if (this.signatureEvent != null)
				{
					this.signatureEvent.GetSignatureDictionary(this.sigStandard);
				}
				this.writer.AddToBody(this.sigStandard, pdfIndirectReference, false);
			}
			else
			{
				PdfLiteral value2 = new PdfLiteral(80);
				this.exclusionLocations[PdfName.BYTERANGE] = value2;
				this.cryptoDictionary.Put(PdfName.BYTERANGE, value2);
				foreach (KeyValuePair<PdfName, int> keyValuePair in exclusionSizes)
				{
					PdfName key = keyValuePair.Key;
					int value3 = keyValuePair.Value;
					value2 = new PdfLiteral(value3);
					this.exclusionLocations[key] = value2;
					this.cryptoDictionary.Put(key, value2);
				}
				if (this.certificationLevel > 0)
				{
					this.AddDocMDP(this.cryptoDictionary);
				}
				if (this.signatureEvent != null)
				{
					this.signatureEvent.GetSignatureDictionary(this.cryptoDictionary);
				}
				this.writer.AddToBody(this.cryptoDictionary, pdfIndirectReference, false);
			}
			if (this.certificationLevel > 0)
			{
				PdfDictionary pdfDictionary2 = new PdfDictionary();
				pdfDictionary2.Put(new PdfName("DocMDP"), pdfIndirectReference);
				this.writer.reader.Catalog.Put(new PdfName("Perms"), pdfDictionary2);
			}
			this.writer.Close(this.stamper.MoreInfo);
			this.range = new int[this.exclusionLocations.Count * 2];
			int position = this.exclusionLocations[PdfName.BYTERANGE].Position;
			this.exclusionLocations.Remove(PdfName.BYTERANGE);
			int num3 = 1;
			foreach (PdfLiteral pdfLiteral in this.exclusionLocations.Values)
			{
				int position2 = pdfLiteral.Position;
				this.range[num3++] = position2;
				this.range[num3++] = pdfLiteral.PosLength + position2;
			}
			Array.Sort<int>(this.range, 1, this.range.Length - 2);
			for (int i = 3; i < this.range.Length - 2; i += 2)
			{
				this.range[i] -= this.range[i - 1];
			}
			if (this.tempFile == null)
			{
				this.bout = this.sigout.Buffer;
				this.boutLen = this.sigout.Size;
				this.range[this.range.Length - 1] = this.boutLen - this.range[this.range.Length - 2];
				ByteBuffer byteBuffer = new ByteBuffer();
				byteBuffer.Append('[');
				for (int j = 0; j < this.range.Length; j++)
				{
					byteBuffer.Append(this.range[j]).Append(' ');
				}
				byteBuffer.Append(']');
				Array.Copy(byteBuffer.Buffer, 0, this.bout, position, byteBuffer.Size);
				return;
			}
			try
			{
				this.raf = new FileStream(this.tempFile, FileMode.Open, FileAccess.ReadWrite);
				int num4 = (int)this.raf.Length;
				this.range[this.range.Length - 1] = num4 - this.range[this.range.Length - 2];
				ByteBuffer byteBuffer2 = new ByteBuffer();
				byteBuffer2.Append('[');
				for (int k = 0; k < this.range.Length; k++)
				{
					byteBuffer2.Append(this.range[k]).Append(' ');
				}
				byteBuffer2.Append(']');
				this.raf.Seek((long)position, SeekOrigin.Begin);
				this.raf.Write(byteBuffer2.Buffer, 0, byteBuffer2.Size);
			}
			catch (IOException ex)
			{
				try
				{
					this.raf.Close();
				}
				catch
				{
				}
				try
				{
					File.Delete(this.tempFile);
				}
				catch
				{
				}
				throw ex;
			}
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000F04DC File Offset: 0x000EF4DC
		public void Close(PdfDictionary update)
		{
			try
			{
				if (!this.preClosed)
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("preclose.must.be.called.first"));
				}
				ByteBuffer byteBuffer = new ByteBuffer();
				foreach (PdfName pdfName in update.Keys)
				{
					PdfObject pdfObject = update.Get(pdfName);
					PdfLiteral pdfLiteral = this.exclusionLocations[pdfName];
					if (pdfLiteral == null)
					{
						throw new ArgumentException(MessageLocalization.GetComposedMessage("the.key.1.didn.t.reserve.space.in.preclose", pdfName.ToString()));
					}
					byteBuffer.Reset();
					pdfObject.ToPdf(null, byteBuffer);
					if (byteBuffer.Size > pdfLiteral.PosLength)
					{
						throw new ArgumentException(MessageLocalization.GetComposedMessage("the.key.1.is.too.big.is.2.reserved.3", pdfName.ToString(), byteBuffer.Size, pdfLiteral.PosLength));
					}
					if (this.tempFile == null)
					{
						Array.Copy(byteBuffer.Buffer, 0, this.bout, pdfLiteral.Position, byteBuffer.Size);
					}
					else
					{
						this.raf.Seek((long)pdfLiteral.Position, SeekOrigin.Begin);
						this.raf.Write(byteBuffer.Buffer, 0, byteBuffer.Size);
					}
				}
				if (update.Size != this.exclusionLocations.Count)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("the.update.dictionary.has.less.keys.than.required"));
				}
				if (this.tempFile == null)
				{
					this.originalout.Write(this.bout, 0, this.boutLen);
				}
				else if (this.originalout != null)
				{
					this.raf.Seek(0L, SeekOrigin.Begin);
					int i = (int)this.raf.Length;
					byte[] array = new byte[8192];
					while (i > 0)
					{
						int num = this.raf.Read(array, 0, Math.Min(array.Length, i));
						if (num < 0)
						{
							throw new EndOfStreamException(MessageLocalization.GetComposedMessage("unexpected.eof"));
						}
						this.originalout.Write(array, 0, num);
						i -= num;
					}
				}
			}
			finally
			{
				if (this.tempFile != null)
				{
					try
					{
						this.raf.Close();
					}
					catch
					{
					}
					if (this.originalout != null)
					{
						try
						{
							File.Delete(this.tempFile);
						}
						catch
						{
						}
					}
				}
				if (this.originalout != null)
				{
					try
					{
						this.originalout.Close();
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000F0794 File Offset: 0x000EF794
		private void AddDocMDP(PdfDictionary crypto)
		{
			PdfDictionary pdfDictionary = new PdfDictionary();
			PdfDictionary pdfDictionary2 = new PdfDictionary();
			pdfDictionary2.Put(PdfName.P, new PdfNumber(this.certificationLevel));
			pdfDictionary2.Put(PdfName.V, new PdfName("1.2"));
			pdfDictionary2.Put(PdfName.TYPE, PdfName.TRANSFORMPARAMS);
			pdfDictionary.Put(PdfName.TRANSFORMMETHOD, PdfName.DOCMDP);
			pdfDictionary.Put(PdfName.TYPE, PdfName.SIGREF);
			pdfDictionary.Put(PdfName.TRANSFORMPARAMS, pdfDictionary2);
			pdfDictionary.Put(new PdfName("DigestValue"), new PdfString("aa"));
			PdfArray pdfArray = new PdfArray();
			pdfArray.Add(new PdfNumber(0));
			pdfArray.Add(new PdfNumber(0));
			pdfDictionary.Put(new PdfName("DigestLocation"), pdfArray);
			pdfDictionary.Put(new PdfName("DigestMethod"), new PdfName("MD5"));
			pdfDictionary.Put(PdfName.DATA, this.writer.reader.Trailer.Get(PdfName.ROOT));
			PdfArray pdfArray2 = new PdfArray();
			pdfArray2.Add(pdfDictionary);
			crypto.Put(PdfName.REFERENCE, pdfArray2);
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x000F08B8 File Offset: 0x000EF8B8
		public Stream RangeStream
		{
			get
			{
				return new PdfSignatureAppearance.FRangeStream(this.raf, this.bout, this.range);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060027DB RID: 10203 RVA: 0x000F08D1 File Offset: 0x000EF8D1
		// (set) Token: 0x060027DC RID: 10204 RVA: 0x000F08D9 File Offset: 0x000EF8D9
		public PdfDictionary CryptoDictionary
		{
			get
			{
				return this.cryptoDictionary;
			}
			set
			{
				this.cryptoDictionary = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060027DD RID: 10205 RVA: 0x000F08E2 File Offset: 0x000EF8E2
		public PdfStamper Stamper
		{
			get
			{
				return this.stamper;
			}
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000F08EA File Offset: 0x000EF8EA
		internal void SetStamper(PdfStamper stamper)
		{
			this.stamper = stamper;
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000F08F3 File Offset: 0x000EF8F3
		public bool IsPreClosed()
		{
			return this.preClosed;
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x000F08FB File Offset: 0x000EF8FB
		public PdfSigGenericPKCS SigStandard
		{
			get
			{
				return this.sigStandard;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x000F0903 File Offset: 0x000EF903
		// (set) Token: 0x060027E2 RID: 10210 RVA: 0x000F090B File Offset: 0x000EF90B
		public string Contact
		{
			get
			{
				return this.contact;
			}
			set
			{
				this.contact = value;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060027E3 RID: 10211 RVA: 0x000F0914 File Offset: 0x000EF914
		// (set) Token: 0x060027E4 RID: 10212 RVA: 0x000F091C File Offset: 0x000EF91C
		public Font Layer2Font
		{
			get
			{
				return this.layer2Font;
			}
			set
			{
				this.layer2Font = value;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x000F0925 File Offset: 0x000EF925
		// (set) Token: 0x060027E6 RID: 10214 RVA: 0x000F092D File Offset: 0x000EF92D
		public bool Acro6Layers
		{
			get
			{
				return this.acro6Layers;
			}
			set
			{
				this.acro6Layers = value;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x000F0962 File Offset: 0x000EF962
		// (set) Token: 0x060027E7 RID: 10215 RVA: 0x000F0936 File Offset: 0x000EF936
		public int RunDirection
		{
			get
			{
				return this.runDirection;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.run.direction.1", this.runDirection));
				}
				this.runDirection = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x000F096A File Offset: 0x000EF96A
		// (set) Token: 0x060027EA RID: 10218 RVA: 0x000F0972 File Offset: 0x000EF972
		public PdfSignatureAppearance.ISignatureEvent SignatureEvent
		{
			get
			{
				return this.signatureEvent;
			}
			set
			{
				this.signatureEvent = value;
			}
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000F097B File Offset: 0x000EF97B
		public Image GetImage()
		{
			return this.image;
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x000F0983 File Offset: 0x000EF983
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x000F098B File Offset: 0x000EF98B
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x000F0994 File Offset: 0x000EF994
		// (set) Token: 0x060027EF RID: 10223 RVA: 0x000F099C File Offset: 0x000EF99C
		public float ImageScale
		{
			get
			{
				return this.imageScale;
			}
			set
			{
				this.imageScale = value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x000F09A5 File Offset: 0x000EF9A5
		// (set) Token: 0x060027F1 RID: 10225 RVA: 0x000F09AD File Offset: 0x000EF9AD
		public int CertificationLevel
		{
			get
			{
				return this.certificationLevel;
			}
			set
			{
				this.certificationLevel = value;
			}
		}

		// Token: 0x04001B41 RID: 6977
		public const int NOT_CERTIFIED = 0;

		// Token: 0x04001B42 RID: 6978
		public const int CERTIFIED_NO_CHANGES_ALLOWED = 1;

		// Token: 0x04001B43 RID: 6979
		public const int CERTIFIED_FORM_FILLING = 2;

		// Token: 0x04001B44 RID: 6980
		public const int CERTIFIED_FORM_FILLING_AND_ANNOTATIONS = 3;

		// Token: 0x04001B45 RID: 6981
		private const float TOP_SECTION = 0.3f;

		// Token: 0x04001B46 RID: 6982
		private const float MARGIN = 2f;

		// Token: 0x04001B47 RID: 6983
		public const string questionMark = "% DSUnknown\nq\n1 G\n1 g\n0.1 0 0 0.1 9 0 cm\n0 J 0 j 4 M []0 d\n1 i \n0 g\n313 292 m\n313 404 325 453 432 529 c\n478 561 504 597 504 645 c\n504 736 440 760 391 760 c\n286 760 271 681 265 626 c\n265 625 l\n100 625 l\n100 828 253 898 381 898 c\n451 898 679 878 679 650 c\n679 555 628 499 538 435 c\n488 399 467 376 467 292 c\n313 292 l\nh\n308 214 170 -164 re\nf\n0.44 G\n1.2 w\n1 1 0.4 rg\n287 318 m\n287 430 299 479 406 555 c\n451 587 478 623 478 671 c\n478 762 414 786 365 786 c\n260 786 245 707 239 652 c\n239 651 l\n74 651 l\n74 854 227 924 355 924 c\n425 924 653 904 653 676 c\n653 581 602 525 512 461 c\n462 425 441 402 441 318 c\n287 318 l\nh\n282 240 170 -164 re\nB\nQ\n";

		// Token: 0x04001B48 RID: 6984
		public static PdfName SELF_SIGNED = PdfName.ADOBE_PPKLITE;

		// Token: 0x04001B49 RID: 6985
		public static PdfName VERISIGN_SIGNED = PdfName.VERISIGN_PPKVS;

		// Token: 0x04001B4A RID: 6986
		public static PdfName WINCER_SIGNED = PdfName.ADOBE_PPKMS;

		// Token: 0x04001B4B RID: 6987
		private Rectangle rect;

		// Token: 0x04001B4C RID: 6988
		private Rectangle pageRect;

		// Token: 0x04001B4D RID: 6989
		private PdfTemplate[] app = new PdfTemplate[5];

		// Token: 0x04001B4E RID: 6990
		private PdfTemplate frm;

		// Token: 0x04001B4F RID: 6991
		private PdfStamperImp writer;

		// Token: 0x04001B50 RID: 6992
		private string layer2Text;

		// Token: 0x04001B51 RID: 6993
		private string reason;

		// Token: 0x04001B52 RID: 6994
		private string location;

		// Token: 0x04001B53 RID: 6995
		private DateTime signDate;

		// Token: 0x04001B54 RID: 6996
		private int page = 1;

		// Token: 0x04001B55 RID: 6997
		private string fieldName;

		// Token: 0x04001B56 RID: 6998
		private ICipherParameters privKey;

		// Token: 0x04001B57 RID: 6999
		private X509Certificate[] certChain;

		// Token: 0x04001B58 RID: 7000
		private object[] crlList;

		// Token: 0x04001B59 RID: 7001
		private PdfName filter;

		// Token: 0x04001B5A RID: 7002
		private bool newField;

		// Token: 0x04001B5B RID: 7003
		private ByteBuffer sigout;

		// Token: 0x04001B5C RID: 7004
		private Stream originalout;

		// Token: 0x04001B5D RID: 7005
		private string tempFile;

		// Token: 0x04001B5E RID: 7006
		private PdfDictionary cryptoDictionary;

		// Token: 0x04001B5F RID: 7007
		private PdfStamper stamper;

		// Token: 0x04001B60 RID: 7008
		private bool preClosed;

		// Token: 0x04001B61 RID: 7009
		private PdfSigGenericPKCS sigStandard;

		// Token: 0x04001B62 RID: 7010
		private int[] range;

		// Token: 0x04001B63 RID: 7011
		private FileStream raf;

		// Token: 0x04001B64 RID: 7012
		private byte[] bout;

		// Token: 0x04001B65 RID: 7013
		private int boutLen;

		// Token: 0x04001B66 RID: 7014
		private byte[] externalDigest;

		// Token: 0x04001B67 RID: 7015
		private byte[] externalRSAdata;

		// Token: 0x04001B68 RID: 7016
		private string digestEncryptionAlgorithm;

		// Token: 0x04001B69 RID: 7017
		private Dictionary<PdfName, PdfLiteral> exclusionLocations;

		// Token: 0x04001B6A RID: 7018
		private PdfSignatureAppearance.SignatureRender render;

		// Token: 0x04001B6B RID: 7019
		private Image signatureGraphic;

		// Token: 0x04001B6C RID: 7020
		private string contact;

		// Token: 0x04001B6D RID: 7021
		private Font layer2Font;

		// Token: 0x04001B6E RID: 7022
		private string layer4Text;

		// Token: 0x04001B6F RID: 7023
		private bool acro6Layers;

		// Token: 0x04001B70 RID: 7024
		private int runDirection = 1;

		// Token: 0x04001B71 RID: 7025
		private PdfSignatureAppearance.ISignatureEvent signatureEvent;

		// Token: 0x04001B72 RID: 7026
		private Image image;

		// Token: 0x04001B73 RID: 7027
		private float imageScale;

		// Token: 0x04001B74 RID: 7028
		private int certificationLevel;

		// Token: 0x02000496 RID: 1174
		public enum SignatureRender
		{
			// Token: 0x04001B76 RID: 7030
			Description,
			// Token: 0x04001B77 RID: 7031
			NameAndDescription,
			// Token: 0x04001B78 RID: 7032
			GraphicAndDescription,
			// Token: 0x04001B79 RID: 7033
			Graphic
		}

		// Token: 0x02000497 RID: 1175
		public class FRangeStream : Stream
		{
			// Token: 0x060027F3 RID: 10227 RVA: 0x000F09D6 File Offset: 0x000EF9D6
			internal FRangeStream(FileStream raf, byte[] bout, int[] range)
			{
				this.raf = raf;
				this.bout = bout;
				this.range = range;
			}

			// Token: 0x060027F4 RID: 10228 RVA: 0x000F0A00 File Offset: 0x000EFA00
			public override int ReadByte()
			{
				int num = this.Read(this.b, 0, 1);
				if (num != 1)
				{
					return -1;
				}
				return (int)(this.b[0] & byte.MaxValue);
			}

			// Token: 0x060027F5 RID: 10229 RVA: 0x000F0A30 File Offset: 0x000EFA30
			public override int Read(byte[] b, int off, int len)
			{
				if (b == null)
				{
					throw new ArgumentNullException();
				}
				if (off < 0 || off > b.Length || len < 0 || off + len > b.Length || off + len < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (len == 0)
				{
					return 0;
				}
				if (this.rangePosition >= this.range[this.range.Length - 2] + this.range[this.range.Length - 1])
				{
					return -1;
				}
				for (int i = 0; i < this.range.Length; i += 2)
				{
					int num = this.range[i];
					int num2 = num + this.range[i + 1];
					if (this.rangePosition < num)
					{
						this.rangePosition = num;
					}
					if (this.rangePosition >= num && this.rangePosition < num2)
					{
						int num3 = Math.Min(len, num2 - this.rangePosition);
						if (this.raf == null)
						{
							Array.Copy(this.bout, this.rangePosition, b, off, num3);
						}
						else
						{
							this.raf.Seek((long)this.rangePosition, SeekOrigin.Begin);
							this.ReadFully(b, off, num3);
						}
						this.rangePosition += num3;
						return num3;
					}
				}
				return -1;
			}

			// Token: 0x060027F6 RID: 10230 RVA: 0x000F0B48 File Offset: 0x000EFB48
			private void ReadFully(byte[] b, int offset, int count)
			{
				while (count > 0)
				{
					int num = this.raf.Read(b, offset, count);
					if (num <= 0)
					{
						throw new IOException(MessageLocalization.GetComposedMessage("insufficient.data"));
					}
					count -= num;
					offset += num;
				}
			}

			// Token: 0x170006F9 RID: 1785
			// (get) Token: 0x060027F7 RID: 10231 RVA: 0x000F0B88 File Offset: 0x000EFB88
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170006FA RID: 1786
			// (get) Token: 0x060027F8 RID: 10232 RVA: 0x000F0B8B File Offset: 0x000EFB8B
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170006FB RID: 1787
			// (get) Token: 0x060027F9 RID: 10233 RVA: 0x000F0B8E File Offset: 0x000EFB8E
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170006FC RID: 1788
			// (get) Token: 0x060027FA RID: 10234 RVA: 0x000F0B91 File Offset: 0x000EFB91
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x170006FD RID: 1789
			// (get) Token: 0x060027FB RID: 10235 RVA: 0x000F0B95 File Offset: 0x000EFB95
			// (set) Token: 0x060027FC RID: 10236 RVA: 0x000F0B99 File Offset: 0x000EFB99
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x060027FD RID: 10237 RVA: 0x000F0B9B File Offset: 0x000EFB9B
			public override void Flush()
			{
			}

			// Token: 0x060027FE RID: 10238 RVA: 0x000F0B9D File Offset: 0x000EFB9D
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x060027FF RID: 10239 RVA: 0x000F0BA1 File Offset: 0x000EFBA1
			public override void SetLength(long value)
			{
			}

			// Token: 0x06002800 RID: 10240 RVA: 0x000F0BA3 File Offset: 0x000EFBA3
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x06002801 RID: 10241 RVA: 0x000F0BA5 File Offset: 0x000EFBA5
			public override void WriteByte(byte value)
			{
			}

			// Token: 0x04001B7A RID: 7034
			private byte[] b = new byte[1];

			// Token: 0x04001B7B RID: 7035
			private FileStream raf;

			// Token: 0x04001B7C RID: 7036
			private byte[] bout;

			// Token: 0x04001B7D RID: 7037
			private int[] range;

			// Token: 0x04001B7E RID: 7038
			private int rangePosition;
		}

		// Token: 0x02000498 RID: 1176
		public interface ISignatureEvent
		{
			// Token: 0x06002802 RID: 10242
			void GetSignatureDictionary(PdfDictionary sig);
		}
	}
}
