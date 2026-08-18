using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000053 RID: 83
	public class PdfAnnotation : PdfDictionary
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000B7A2 File Offset: 0x0000A7A2
		public PdfAnnotation(PdfWriter writer, Rectangle rect)
		{
			this.writer = writer;
			if (rect != null)
			{
				base.Put(PdfName.RECT, new PdfRectangle(rect));
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B7D4 File Offset: 0x0000A7D4
		public PdfAnnotation(PdfWriter writer, float llx, float lly, float urx, float ury, PdfString title, PdfString content)
		{
			this.writer = writer;
			base.Put(PdfName.SUBTYPE, PdfName.TEXT);
			base.Put(PdfName.T, title);
			base.Put(PdfName.RECT, new PdfRectangle(llx, lly, urx, ury));
			base.Put(PdfName.CONTENTS, content);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B83C File Offset: 0x0000A83C
		public PdfAnnotation(PdfWriter writer, float llx, float lly, float urx, float ury, PdfAction action)
		{
			this.writer = writer;
			base.Put(PdfName.SUBTYPE, PdfName.LINK);
			base.Put(PdfName.RECT, new PdfRectangle(llx, lly, urx, ury));
			base.Put(PdfName.A, action);
			base.Put(PdfName.BORDER, new PdfBorderArray(0f, 0f, 0f));
			base.Put(PdfName.C, new PdfColor(0, 0, 255));
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B8D0 File Offset: 0x0000A8D0
		public static PdfAnnotation CreateScreen(PdfWriter writer, Rectangle rect, string clipTitle, PdfFileSpecification fs, string mimeType, bool playOnDisplay)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.SCREEN);
			pdfAnnotation.Put(PdfName.F, new PdfNumber(4));
			pdfAnnotation.Put(PdfName.TYPE, PdfName.ANNOT);
			pdfAnnotation.SetPage();
			PdfIndirectReference indirectReference = pdfAnnotation.IndirectReference;
			PdfAction objecta = PdfAction.Rendition(clipTitle, fs, mimeType, indirectReference);
			PdfIndirectReference indirectReference2 = writer.AddToBody(objecta).IndirectReference;
			if (playOnDisplay)
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(new PdfName("PV"), indirectReference2);
				pdfAnnotation.Put(PdfName.AA, pdfDictionary);
			}
			pdfAnnotation.Put(PdfName.A, indirectReference2);
			return pdfAnnotation;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000B972 File Offset: 0x0000A972
		public PdfIndirectReference IndirectReference
		{
			get
			{
				if (this.reference == null)
				{
					this.reference = this.writer.PdfIndirectReference;
				}
				return this.reference;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B994 File Offset: 0x0000A994
		public static PdfAnnotation CreateText(PdfWriter writer, Rectangle rect, string title, string contents, bool open, string icon)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.TEXT);
			if (title != null)
			{
				pdfAnnotation.Put(PdfName.T, new PdfString(title, "UnicodeBig"));
			}
			if (contents != null)
			{
				pdfAnnotation.Put(PdfName.CONTENTS, new PdfString(contents, "UnicodeBig"));
			}
			if (open)
			{
				pdfAnnotation.Put(PdfName.OPEN, PdfBoolean.PDFTRUE);
			}
			if (icon != null)
			{
				pdfAnnotation.Put(PdfName.NAME, new PdfName(icon));
			}
			return pdfAnnotation;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000BA18 File Offset: 0x0000AA18
		protected static PdfAnnotation CreateLink(PdfWriter writer, Rectangle rect, PdfName highlight)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.LINK);
			if (!highlight.Equals(PdfAnnotation.HIGHLIGHT_INVERT))
			{
				pdfAnnotation.Put(PdfName.H, highlight);
			}
			return pdfAnnotation;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000BA58 File Offset: 0x0000AA58
		public static PdfAnnotation CreateLink(PdfWriter writer, Rectangle rect, PdfName highlight, PdfAction action)
		{
			PdfAnnotation pdfAnnotation = PdfAnnotation.CreateLink(writer, rect, highlight);
			pdfAnnotation.PutEx(PdfName.A, action);
			return pdfAnnotation;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000BA7C File Offset: 0x0000AA7C
		public static PdfAnnotation CreateLink(PdfWriter writer, Rectangle rect, PdfName highlight, string namedDestination)
		{
			PdfAnnotation pdfAnnotation = PdfAnnotation.CreateLink(writer, rect, highlight);
			pdfAnnotation.Put(PdfName.DEST, new PdfString(namedDestination));
			return pdfAnnotation;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000BAA4 File Offset: 0x0000AAA4
		public static PdfAnnotation CreateLink(PdfWriter writer, Rectangle rect, PdfName highlight, int page, PdfDestination dest)
		{
			PdfAnnotation pdfAnnotation = PdfAnnotation.CreateLink(writer, rect, highlight);
			PdfIndirectReference pageReference = writer.GetPageReference(page);
			dest.AddPage(pageReference);
			pdfAnnotation.Put(PdfName.DEST, dest);
			return pdfAnnotation;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000BADC File Offset: 0x0000AADC
		public static PdfAnnotation CreateFreeText(PdfWriter writer, Rectangle rect, string contents, PdfContentByte defaultAppearance)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.FREETEXT);
			pdfAnnotation.Put(PdfName.CONTENTS, new PdfString(contents, "UnicodeBig"));
			pdfAnnotation.DefaultAppearanceString = defaultAppearance;
			return pdfAnnotation;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000BB20 File Offset: 0x0000AB20
		public static PdfAnnotation CreateLine(PdfWriter writer, Rectangle rect, string contents, float x1, float y1, float x2, float y2)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.LINE);
			pdfAnnotation.Put(PdfName.CONTENTS, new PdfString(contents, "UnicodeBig"));
			PdfArray pdfArray = new PdfArray(new PdfNumber(x1));
			pdfArray.Add(new PdfNumber(y1));
			pdfArray.Add(new PdfNumber(x2));
			pdfArray.Add(new PdfNumber(y2));
			pdfAnnotation.Put(PdfName.L, pdfArray);
			return pdfAnnotation;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000BBA0 File Offset: 0x0000ABA0
		public static PdfAnnotation CreateSquareCircle(PdfWriter writer, Rectangle rect, string contents, bool square)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			if (square)
			{
				pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.SQUARE);
			}
			else
			{
				pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.CIRCLE);
			}
			pdfAnnotation.Put(PdfName.CONTENTS, new PdfString(contents, "UnicodeBig"));
			return pdfAnnotation;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000BBF4 File Offset: 0x0000ABF4
		public static PdfAnnotation CreateMarkup(PdfWriter writer, Rectangle rect, string contents, int type, float[] quadPoints)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			PdfName value = PdfName.HIGHLIGHT;
			switch (type)
			{
			case 1:
				value = PdfName.UNDERLINE;
				break;
			case 2:
				value = PdfName.STRIKEOUT;
				break;
			case 3:
				value = PdfName.SQUIGGLY;
				break;
			}
			pdfAnnotation.Put(PdfName.SUBTYPE, value);
			pdfAnnotation.Put(PdfName.CONTENTS, new PdfString(contents, "UnicodeBig"));
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < quadPoints.Length; i++)
			{
				pdfArray.Add(new PdfNumber(quadPoints[i]));
			}
			pdfAnnotation.Put(PdfName.QUADPOINTS, pdfArray);
			return pdfAnnotation;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000BC94 File Offset: 0x0000AC94
		public static PdfAnnotation CreateStamp(PdfWriter writer, Rectangle rect, string contents, string name)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.STAMP);
			pdfAnnotation.Put(PdfName.CONTENTS, new PdfString(contents, "UnicodeBig"));
			pdfAnnotation.Put(PdfName.NAME, new PdfName(name));
			return pdfAnnotation;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000BCE4 File Offset: 0x0000ACE4
		public static PdfAnnotation CreateInk(PdfWriter writer, Rectangle rect, string contents, float[][] inkList)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.INK);
			pdfAnnotation.Put(PdfName.CONTENTS, new PdfString(contents, "UnicodeBig"));
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < inkList.Length; i++)
			{
				PdfArray pdfArray2 = new PdfArray();
				float[] array = inkList[i];
				for (int j = 0; j < array.Length; j++)
				{
					pdfArray2.Add(new PdfNumber(array[j]));
				}
				pdfArray.Add(pdfArray2);
			}
			pdfAnnotation.Put(PdfName.INKLIST, pdfArray);
			return pdfAnnotation;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000BD77 File Offset: 0x0000AD77
		public static PdfAnnotation CreateFileAttachment(PdfWriter writer, Rectangle rect, string contents, byte[] fileStore, string file, string fileDisplay)
		{
			return PdfAnnotation.CreateFileAttachment(writer, rect, contents, PdfFileSpecification.FileEmbedded(writer, file, fileDisplay, fileStore));
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000BD8C File Offset: 0x0000AD8C
		public static PdfAnnotation CreateFileAttachment(PdfWriter writer, Rectangle rect, string contents, PdfFileSpecification fs)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.FILEATTACHMENT);
			if (contents != null)
			{
				pdfAnnotation.Put(PdfName.CONTENTS, new PdfString(contents, "UnicodeBig"));
			}
			pdfAnnotation.Put(PdfName.FS, fs.Reference);
			return pdfAnnotation;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000BDDC File Offset: 0x0000ADDC
		public static PdfAnnotation CreatePopup(PdfWriter writer, Rectangle rect, string contents, bool open)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.POPUP);
			if (contents != null)
			{
				pdfAnnotation.Put(PdfName.CONTENTS, new PdfString(contents, "UnicodeBig"));
			}
			if (open)
			{
				pdfAnnotation.Put(PdfName.OPEN, PdfBoolean.PDFTRUE);
			}
			return pdfAnnotation;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000BE30 File Offset: 0x0000AE30
		public static PdfAnnotation CreatePolygonPolyline(PdfWriter writer, Rectangle rect, string contents, bool polygon, PdfArray vertices)
		{
			PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, rect);
			if (polygon)
			{
				pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.POLYGON);
			}
			else
			{
				pdfAnnotation.Put(PdfName.SUBTYPE, PdfName.POLYLINE);
			}
			pdfAnnotation.Put(PdfName.CONTENTS, new PdfString(contents, "UnicodeBig"));
			pdfAnnotation.Put(PdfName.VERTICES, new PdfArray(vertices));
			return pdfAnnotation;
		}

		// Token: 0x1700005B RID: 91
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000BE94 File Offset: 0x0000AE94
		public PdfContentByte DefaultAppearanceString
		{
			set
			{
				byte[] array = value.InternalBuffer.ToByteArray();
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					if (array[i] == 10)
					{
						array[i] = 32;
					}
				}
				base.Put(PdfName.DA, new PdfString(array));
			}
		}

		// Token: 0x1700005C RID: 92
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000BEDA File Offset: 0x0000AEDA
		public int Flags
		{
			set
			{
				if (value == 0)
				{
					base.Remove(PdfName.F);
					return;
				}
				base.Put(PdfName.F, new PdfNumber(value));
			}
		}

		// Token: 0x1700005D RID: 93
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000BEFC File Offset: 0x0000AEFC
		public PdfBorderArray Border
		{
			set
			{
				base.Put(PdfName.BORDER, value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000BF0A File Offset: 0x0000AF0A
		public PdfBorderDictionary BorderStyle
		{
			set
			{
				base.Put(PdfName.BS, value);
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000BF18 File Offset: 0x0000AF18
		public void SetHighlighting(PdfName highlight)
		{
			if (highlight.Equals(PdfAnnotation.HIGHLIGHT_INVERT))
			{
				base.Remove(PdfName.H);
				return;
			}
			base.Put(PdfName.H, highlight);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000BF40 File Offset: 0x0000AF40
		public void SetAppearance(PdfName ap, PdfTemplate template)
		{
			PdfDictionary pdfDictionary = (PdfDictionary)base.Get(PdfName.AP);
			if (pdfDictionary == null)
			{
				pdfDictionary = new PdfDictionary();
			}
			pdfDictionary.Put(ap, template.IndirectReference);
			base.Put(PdfName.AP, pdfDictionary);
			if (!this.form)
			{
				return;
			}
			if (this.templates == null)
			{
				this.templates = new Dictionary<PdfTemplate, object>();
			}
			this.templates[template] = null;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000BFAC File Offset: 0x0000AFAC
		public void SetAppearance(PdfName ap, string state, PdfTemplate template)
		{
			PdfDictionary pdfDictionary = (PdfDictionary)base.Get(PdfName.AP);
			if (pdfDictionary == null)
			{
				pdfDictionary = new PdfDictionary();
			}
			PdfObject pdfObject = pdfDictionary.Get(ap);
			PdfDictionary pdfDictionary2;
			if (pdfObject != null && pdfObject.IsDictionary())
			{
				pdfDictionary2 = (PdfDictionary)pdfObject;
			}
			else
			{
				pdfDictionary2 = new PdfDictionary();
			}
			pdfDictionary2.Put(new PdfName(state), template.IndirectReference);
			pdfDictionary.Put(ap, pdfDictionary2);
			base.Put(PdfName.AP, pdfDictionary);
			if (!this.form)
			{
				return;
			}
			if (this.templates == null)
			{
				this.templates = new Dictionary<PdfTemplate, object>();
			}
			this.templates[template] = null;
		}

		// Token: 0x1700005F RID: 95
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000C044 File Offset: 0x0000B044
		public string AppearanceState
		{
			set
			{
				if (value == null)
				{
					base.Remove(PdfName.AS);
					return;
				}
				base.Put(PdfName.AS, new PdfName(value));
			}
		}

		// Token: 0x17000060 RID: 96
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000C066 File Offset: 0x0000B066
		public BaseColor Color
		{
			set
			{
				base.Put(PdfName.C, new PdfColor(value));
			}
		}

		// Token: 0x17000061 RID: 97
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000C079 File Offset: 0x0000B079
		public string Title
		{
			set
			{
				if (value == null)
				{
					base.Remove(PdfName.T);
					return;
				}
				base.Put(PdfName.T, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x17000062 RID: 98
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000C0A0 File Offset: 0x0000B0A0
		public PdfAnnotation Popup
		{
			set
			{
				base.Put(PdfName.POPUP, value.IndirectReference);
				value.Put(PdfName.PARENT, this.IndirectReference);
			}
		}

		// Token: 0x17000063 RID: 99
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000C0C4 File Offset: 0x0000B0C4
		public PdfAction Action
		{
			set
			{
				base.Put(PdfName.A, value);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C0D4 File Offset: 0x0000B0D4
		public void SetAdditionalActions(PdfName key, PdfAction action)
		{
			PdfObject pdfObject = base.Get(PdfName.AA);
			PdfDictionary pdfDictionary;
			if (pdfObject != null && pdfObject.IsDictionary())
			{
				pdfDictionary = (PdfDictionary)pdfObject;
			}
			else
			{
				pdfDictionary = new PdfDictionary();
			}
			pdfDictionary.Put(key, action);
			base.Put(PdfName.AA, pdfDictionary);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C11B File Offset: 0x0000B11B
		internal virtual bool IsUsed()
		{
			return this.used;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C123 File Offset: 0x0000B123
		public virtual void SetUsed()
		{
			this.used = true;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000C12C File Offset: 0x0000B12C
		public Dictionary<PdfTemplate, object> Templates
		{
			get
			{
				return this.templates;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C134 File Offset: 0x0000B134
		public bool IsForm()
		{
			return this.form;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C13C File Offset: 0x0000B13C
		public bool IsAnnotation()
		{
			return this.annotation;
		}

		// Token: 0x17000065 RID: 101
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000C144 File Offset: 0x0000B144
		public int Page
		{
			set
			{
				base.Put(PdfName.P, this.writer.GetPageReference(value));
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C15D File Offset: 0x0000B15D
		public void SetPage()
		{
			base.Put(PdfName.P, this.writer.CurrentPage);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000C175 File Offset: 0x0000B175
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000C17D File Offset: 0x0000B17D
		public int PlaceInPage
		{
			get
			{
				return this.placeInPage;
			}
			set
			{
				this.placeInPage = value;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C188 File Offset: 0x0000B188
		public static PdfAnnotation ShallowDuplicate(PdfAnnotation annot)
		{
			PdfAnnotation pdfAnnotation;
			if (annot.IsForm())
			{
				pdfAnnotation = new PdfFormField(annot.writer);
				PdfFormField pdfFormField = (PdfFormField)pdfAnnotation;
				PdfFormField pdfFormField2 = (PdfFormField)annot;
				pdfFormField.parent = pdfFormField2.parent;
				pdfFormField.kids = pdfFormField2.kids;
			}
			else
			{
				pdfAnnotation = new PdfAnnotation(annot.writer, null);
			}
			pdfAnnotation.Merge(annot);
			pdfAnnotation.form = annot.form;
			pdfAnnotation.annotation = annot.annotation;
			pdfAnnotation.templates = annot.templates;
			return pdfAnnotation;
		}

		// Token: 0x17000067 RID: 103
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000C20A File Offset: 0x0000B20A
		public int Rotate
		{
			set
			{
				base.Put(PdfName.ROTATE, new PdfNumber(value));
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000C220 File Offset: 0x0000B220
		internal PdfDictionary MK
		{
			get
			{
				PdfDictionary pdfDictionary = (PdfDictionary)base.Get(PdfName.MK);
				if (pdfDictionary == null)
				{
					pdfDictionary = new PdfDictionary();
					base.Put(PdfName.MK, pdfDictionary);
				}
				return pdfDictionary;
			}
		}

		// Token: 0x17000069 RID: 105
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000C254 File Offset: 0x0000B254
		public int MKRotation
		{
			set
			{
				this.MK.Put(PdfName.R, new PdfNumber(value));
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C26C File Offset: 0x0000B26C
		public static PdfArray GetMKColor(BaseColor color)
		{
			PdfArray pdfArray = new PdfArray();
			switch (ExtendedColor.GetType(color))
			{
			case 1:
				pdfArray.Add(new PdfNumber(((GrayColor)color).Gray));
				break;
			case 2:
			{
				CMYKColor cmykcolor = (CMYKColor)color;
				pdfArray.Add(new PdfNumber(cmykcolor.Cyan));
				pdfArray.Add(new PdfNumber(cmykcolor.Magenta));
				pdfArray.Add(new PdfNumber(cmykcolor.Yellow));
				pdfArray.Add(new PdfNumber(cmykcolor.Black));
				break;
			}
			case 3:
			case 4:
			case 5:
				throw new Exception(MessageLocalization.GetComposedMessage("separations.patterns.and.shadings.are.not.allowed.in.mk.dictionary"));
			default:
				pdfArray.Add(new PdfNumber((float)color.R / 255f));
				pdfArray.Add(new PdfNumber((float)color.G / 255f));
				pdfArray.Add(new PdfNumber((float)color.B / 255f));
				break;
			}
			return pdfArray;
		}

		// Token: 0x1700006A RID: 106
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000C36F File Offset: 0x0000B36F
		public BaseColor MKBorderColor
		{
			set
			{
				if (value == null)
				{
					this.MK.Remove(PdfName.BC);
					return;
				}
				this.MK.Put(PdfName.BC, PdfAnnotation.GetMKColor(value));
			}
		}

		// Token: 0x1700006B RID: 107
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000C39B File Offset: 0x0000B39B
		public BaseColor MKBackgroundColor
		{
			set
			{
				if (value == null)
				{
					this.MK.Remove(PdfName.BG);
					return;
				}
				this.MK.Put(PdfName.BG, PdfAnnotation.GetMKColor(value));
			}
		}

		// Token: 0x1700006C RID: 108
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000C3C7 File Offset: 0x0000B3C7
		public string MKNormalCaption
		{
			set
			{
				this.MK.Put(PdfName.CA, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x1700006D RID: 109
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000C3E4 File Offset: 0x0000B3E4
		public string MKRolloverCaption
		{
			set
			{
				this.MK.Put(PdfName.RC, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x1700006E RID: 110
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000C401 File Offset: 0x0000B401
		public string MKAlternateCaption
		{
			set
			{
				this.MK.Put(PdfName.AC, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x1700006F RID: 111
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000C41E File Offset: 0x0000B41E
		public PdfTemplate MKNormalIcon
		{
			set
			{
				this.MK.Put(PdfName.I, value.IndirectReference);
			}
		}

		// Token: 0x17000070 RID: 112
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000C436 File Offset: 0x0000B436
		public PdfTemplate MKRolloverIcon
		{
			set
			{
				this.MK.Put(PdfName.RI, value.IndirectReference);
			}
		}

		// Token: 0x17000071 RID: 113
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000C44E File Offset: 0x0000B44E
		public PdfTemplate MKAlternateIcon
		{
			set
			{
				this.MK.Put(PdfName.IX, value.IndirectReference);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C468 File Offset: 0x0000B468
		public void SetMKIconFit(PdfName scale, PdfName scalingType, float leftoverLeft, float leftoverBottom, bool fitInBounds)
		{
			PdfDictionary pdfDictionary = new PdfDictionary();
			if (!scale.Equals(PdfName.A))
			{
				pdfDictionary.Put(PdfName.SW, scale);
			}
			if (!scalingType.Equals(PdfName.P))
			{
				pdfDictionary.Put(PdfName.S, scalingType);
			}
			if (leftoverLeft != 0.5f || leftoverBottom != 0.5f)
			{
				PdfArray pdfArray = new PdfArray(new PdfNumber(leftoverLeft));
				pdfArray.Add(new PdfNumber(leftoverBottom));
				pdfDictionary.Put(PdfName.A, pdfArray);
			}
			if (fitInBounds)
			{
				pdfDictionary.Put(PdfName.FB, PdfBoolean.PDFTRUE);
			}
			this.MK.Put(PdfName.IF, pdfDictionary);
		}

		// Token: 0x17000072 RID: 114
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000C509 File Offset: 0x0000B509
		public int MKTextPosition
		{
			set
			{
				this.MK.Put(PdfName.TP, new PdfNumber(value));
			}
		}

		// Token: 0x17000073 RID: 115
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000C521 File Offset: 0x0000B521
		public IPdfOCG Layer
		{
			set
			{
				base.Put(PdfName.OC, value.Ref);
			}
		}

		// Token: 0x17000074 RID: 116
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000C534 File Offset: 0x0000B534
		public string Name
		{
			set
			{
				base.Put(PdfName.NM, new PdfString(value));
			}
		}

		// Token: 0x04000110 RID: 272
		public const int FLAGS_INVISIBLE = 1;

		// Token: 0x04000111 RID: 273
		public const int FLAGS_HIDDEN = 2;

		// Token: 0x04000112 RID: 274
		public const int FLAGS_PRINT = 4;

		// Token: 0x04000113 RID: 275
		public const int FLAGS_NOZOOM = 8;

		// Token: 0x04000114 RID: 276
		public const int FLAGS_NOROTATE = 16;

		// Token: 0x04000115 RID: 277
		public const int FLAGS_NOVIEW = 32;

		// Token: 0x04000116 RID: 278
		public const int FLAGS_READONLY = 64;

		// Token: 0x04000117 RID: 279
		public const int FLAGS_LOCKED = 128;

		// Token: 0x04000118 RID: 280
		public const int FLAGS_TOGGLENOVIEW = 256;

		// Token: 0x04000119 RID: 281
		public const int MARKUP_HIGHLIGHT = 0;

		// Token: 0x0400011A RID: 282
		public const int MARKUP_UNDERLINE = 1;

		// Token: 0x0400011B RID: 283
		public const int MARKUP_STRIKEOUT = 2;

		// Token: 0x0400011C RID: 284
		public const int MARKUP_SQUIGGLY = 3;

		// Token: 0x0400011D RID: 285
		public static readonly PdfName HIGHLIGHT_NONE = PdfName.N;

		// Token: 0x0400011E RID: 286
		public static readonly PdfName HIGHLIGHT_INVERT = PdfName.I;

		// Token: 0x0400011F RID: 287
		public static readonly PdfName HIGHLIGHT_OUTLINE = PdfName.O;

		// Token: 0x04000120 RID: 288
		public static readonly PdfName HIGHLIGHT_PUSH = PdfName.P;

		// Token: 0x04000121 RID: 289
		public static readonly PdfName HIGHLIGHT_TOGGLE = PdfName.T;

		// Token: 0x04000122 RID: 290
		public static readonly PdfName APPEARANCE_NORMAL = PdfName.N;

		// Token: 0x04000123 RID: 291
		public static readonly PdfName APPEARANCE_ROLLOVER = PdfName.R;

		// Token: 0x04000124 RID: 292
		public static readonly PdfName APPEARANCE_DOWN = PdfName.D;

		// Token: 0x04000125 RID: 293
		public static readonly PdfName AA_ENTER = PdfName.E;

		// Token: 0x04000126 RID: 294
		public static readonly PdfName AA_EXIT = PdfName.X;

		// Token: 0x04000127 RID: 295
		public static readonly PdfName AA_DOWN = PdfName.D;

		// Token: 0x04000128 RID: 296
		public static readonly PdfName AA_UP = PdfName.U;

		// Token: 0x04000129 RID: 297
		public static readonly PdfName AA_FOCUS = PdfName.FO;

		// Token: 0x0400012A RID: 298
		public static readonly PdfName AA_BLUR = PdfName.BL;

		// Token: 0x0400012B RID: 299
		public static readonly PdfName AA_JS_KEY = PdfName.K;

		// Token: 0x0400012C RID: 300
		public static readonly PdfName AA_JS_FORMAT = PdfName.F;

		// Token: 0x0400012D RID: 301
		public static readonly PdfName AA_JS_CHANGE = PdfName.V;

		// Token: 0x0400012E RID: 302
		public static readonly PdfName AA_JS_OTHER_CHANGE = PdfName.C;

		// Token: 0x0400012F RID: 303
		protected internal PdfWriter writer;

		// Token: 0x04000130 RID: 304
		protected internal PdfIndirectReference reference;

		// Token: 0x04000131 RID: 305
		protected internal Dictionary<PdfTemplate, object> templates;

		// Token: 0x04000132 RID: 306
		protected internal bool form;

		// Token: 0x04000133 RID: 307
		protected internal bool annotation = true;

		// Token: 0x04000134 RID: 308
		protected internal bool used;

		// Token: 0x04000135 RID: 309
		private int placeInPage = -1;

		// Token: 0x02000054 RID: 84
		public class PdfImportedLink
		{
			// Token: 0x06000290 RID: 656 RVA: 0x0000C60C File Offset: 0x0000B60C
			internal PdfImportedLink(PdfDictionary annotation)
			{
				this.parameters = new Dictionary<PdfName, PdfObject>(annotation.hashMap);
				try
				{
					if (this.parameters.ContainsKey(PdfName.DEST))
					{
						this.destination = (PdfArray)this.parameters[PdfName.DEST];
					}
					this.parameters.Remove(PdfName.DEST);
				}
				catch (Exception)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("you.have.to.consolidate.the.named.destinations.of.your.reader"));
				}
				if (this.destination != null)
				{
					this.destination = new PdfArray(this.destination);
				}
				PdfArray pdfArray = (PdfArray)this.parameters[PdfName.RECT];
				this.parameters.Remove(PdfName.RECT);
				this.llx = pdfArray.GetAsNumber(0).FloatValue;
				this.lly = pdfArray.GetAsNumber(1).FloatValue;
				this.urx = pdfArray.GetAsNumber(2).FloatValue;
				this.ury = pdfArray.GetAsNumber(3).FloatValue;
			}

			// Token: 0x06000291 RID: 657 RVA: 0x0000C71C File Offset: 0x0000B71C
			public bool IsInternal()
			{
				return this.destination != null;
			}

			// Token: 0x06000292 RID: 658 RVA: 0x0000C72C File Offset: 0x0000B72C
			public int GetDestinationPage()
			{
				if (!this.IsInternal())
				{
					return 0;
				}
				PdfIndirectReference asIndirectObject = this.destination.GetAsIndirectObject(0);
				PRIndirectReference prindirectReference = (PRIndirectReference)asIndirectObject;
				PdfReader reader = prindirectReference.Reader;
				for (int i = 1; i <= reader.NumberOfPages; i++)
				{
					PRIndirectReference pageOrigRef = reader.GetPageOrigRef(i);
					if (pageOrigRef.Generation == prindirectReference.Generation && pageOrigRef.Number == prindirectReference.Number)
					{
						return i;
					}
				}
				throw new ArgumentException(MessageLocalization.GetComposedMessage("page.not.found"));
			}

			// Token: 0x06000293 RID: 659 RVA: 0x0000C7A7 File Offset: 0x0000B7A7
			public void SetDestinationPage(int newPage)
			{
				if (!this.IsInternal())
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("cannot.change.destination.of.external.link"));
				}
				this.newPage = newPage;
			}

			// Token: 0x06000294 RID: 660 RVA: 0x0000C7C8 File Offset: 0x0000B7C8
			public void TransformDestination(float a, float b, float c, float d, float e, float f)
			{
				if (!this.IsInternal())
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("cannot.change.destination.of.external.link"));
				}
				if (this.destination.GetAsName(1).Equals(PdfName.XYZ))
				{
					float floatValue = this.destination.GetAsNumber(2).FloatValue;
					float floatValue2 = this.destination.GetAsNumber(3).FloatValue;
					float value = floatValue * a + floatValue2 * c + e;
					float value2 = floatValue * b + floatValue2 * d + f;
					this.destination.ArrayList[2] = new PdfNumber(value);
					this.destination.ArrayList[3] = new PdfNumber(value2);
				}
			}

			// Token: 0x06000295 RID: 661 RVA: 0x0000C870 File Offset: 0x0000B870
			public void TransformRect(float a, float b, float c, float d, float e, float f)
			{
				float num = this.llx * a + this.lly * c + e;
				float num2 = this.llx * b + this.lly * d + f;
				this.llx = num;
				this.lly = num2;
				num = this.urx * a + this.ury * c + e;
				num2 = this.urx * b + this.ury * d + f;
				this.urx = num;
				this.ury = num2;
			}

			// Token: 0x06000296 RID: 662 RVA: 0x0000C8F0 File Offset: 0x0000B8F0
			public PdfAnnotation CreateAnnotation(PdfWriter writer)
			{
				PdfAnnotation pdfAnnotation = new PdfAnnotation(writer, new Rectangle(this.llx, this.lly, this.urx, this.ury));
				if (this.newPage != 0)
				{
					PdfIndirectReference pageReference = writer.GetPageReference(this.newPage);
					this.destination[0] = pageReference;
				}
				if (this.destination != null)
				{
					pdfAnnotation.Put(PdfName.DEST, this.destination);
				}
				foreach (PdfName key in this.parameters.Keys)
				{
					pdfAnnotation.hashMap[key] = this.parameters[key];
				}
				return pdfAnnotation;
			}

			// Token: 0x06000297 RID: 663 RVA: 0x0000C9BC File Offset: 0x0000B9BC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("Imported link: location [");
				stringBuilder.Append(this.llx);
				stringBuilder.Append(' ');
				stringBuilder.Append(this.lly);
				stringBuilder.Append(' ');
				stringBuilder.Append(this.urx);
				stringBuilder.Append(' ');
				stringBuilder.Append(this.ury);
				stringBuilder.Append("] destination ");
				stringBuilder.Append(this.destination);
				stringBuilder.Append(" parameters ");
				stringBuilder.Append(this.parameters);
				return stringBuilder.ToString();
			}

			// Token: 0x04000136 RID: 310
			private float llx;

			// Token: 0x04000137 RID: 311
			private float lly;

			// Token: 0x04000138 RID: 312
			private float urx;

			// Token: 0x04000139 RID: 313
			private float ury;

			// Token: 0x0400013A RID: 314
			private Dictionary<PdfName, PdfObject> parameters;

			// Token: 0x0400013B RID: 315
			private PdfArray destination;

			// Token: 0x0400013C RID: 316
			private int newPage;
		}
	}
}
