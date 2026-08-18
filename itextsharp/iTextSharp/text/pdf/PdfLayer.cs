using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200045C RID: 1116
	public class PdfLayer : PdfDictionary, IPdfOCG
	{
		// Token: 0x060025B1 RID: 9649 RVA: 0x000E4005 File Offset: 0x000E3005
		internal PdfLayer(string title)
		{
			this.title = title;
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000E4024 File Offset: 0x000E3024
		public static PdfLayer CreateTitle(string title, PdfWriter writer)
		{
			if (title == null)
			{
				throw new ArgumentNullException(MessageLocalization.GetComposedMessage("title.cannot.be.null"));
			}
			PdfLayer pdfLayer = new PdfLayer(title);
			writer.RegisterLayer(pdfLayer);
			return pdfLayer;
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000E4054 File Offset: 0x000E3054
		public PdfLayer(string name, PdfWriter writer) : base(PdfName.OCG)
		{
			this.Name = name;
			if (writer is PdfStamperImp)
			{
				this.refi = writer.AddToBody(this).IndirectReference;
			}
			else
			{
				this.refi = writer.PdfIndirectReference;
			}
			writer.RegisterLayer(this);
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x000E40B0 File Offset: 0x000E30B0
		internal string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000E40B8 File Offset: 0x000E30B8
		public void AddChild(PdfLayer child)
		{
			if (child.parent != null)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.layer.1.already.has.a.parent", ((PdfString)child.Get(PdfName.NAME)).ToUnicodeString()));
			}
			child.parent = this;
			if (this.children == null)
			{
				this.children = new List<PdfLayer>();
			}
			this.children.Add(child);
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000E4118 File Offset: 0x000E3118
		public PdfLayer Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x000E4120 File Offset: 0x000E3120
		public List<PdfLayer> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x000E4128 File Offset: 0x000E3128
		// (set) Token: 0x060025B9 RID: 9657 RVA: 0x000E4130 File Offset: 0x000E3130
		public PdfIndirectReference Ref
		{
			get
			{
				return this.refi;
			}
			set
			{
				this.refi = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (set) Token: 0x060025BA RID: 9658 RVA: 0x000E4139 File Offset: 0x000E3139
		public string Name
		{
			set
			{
				base.Put(PdfName.NAME, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x000E4151 File Offset: 0x000E3151
		public PdfObject PdfObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000E4154 File Offset: 0x000E3154
		// (set) Token: 0x060025BD RID: 9661 RVA: 0x000E415C File Offset: 0x000E315C
		public bool On
		{
			get
			{
				return this.on;
			}
			set
			{
				this.on = value;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x000E4168 File Offset: 0x000E3168
		private PdfDictionary Usage
		{
			get
			{
				PdfDictionary pdfDictionary = (PdfDictionary)base.Get(PdfName.USAGE);
				if (pdfDictionary == null)
				{
					pdfDictionary = new PdfDictionary();
					base.Put(PdfName.USAGE, pdfDictionary);
				}
				return pdfDictionary;
			}
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000E419C File Offset: 0x000E319C
		public void SetCreatorInfo(string creator, string subtype)
		{
			PdfDictionary usage = this.Usage;
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.CREATOR, new PdfString(creator, "UnicodeBig"));
			pdfDictionary.Put(PdfName.SUBTYPE, new PdfName(subtype));
			usage.Put(PdfName.CREATORINFO, pdfDictionary);
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000E41EC File Offset: 0x000E31EC
		public void SetLanguage(string lang, bool preferred)
		{
			PdfDictionary usage = this.Usage;
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.LANG, new PdfString(lang, "UnicodeBig"));
			if (preferred)
			{
				pdfDictionary.Put(PdfName.PREFERRED, PdfName.ON);
			}
			usage.Put(PdfName.LANGUAGE, pdfDictionary);
		}

		// Token: 0x1700067A RID: 1658
		// (set) Token: 0x060025C1 RID: 9665 RVA: 0x000E423C File Offset: 0x000E323C
		public bool Export
		{
			set
			{
				PdfDictionary usage = this.Usage;
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.EXPORTSTATE, value ? PdfName.ON : PdfName.OFF);
				usage.Put(PdfName.EXPORT, pdfDictionary);
			}
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000E427C File Offset: 0x000E327C
		public void SetZoom(float min, float max)
		{
			if (min <= 0f && max < 0f)
			{
				return;
			}
			PdfDictionary usage = this.Usage;
			PdfDictionary pdfDictionary = new PdfDictionary();
			if (min > 0f)
			{
				pdfDictionary.Put(PdfName.MIN_LOWER_CASE, new PdfNumber(min));
			}
			if (max >= 0f)
			{
				pdfDictionary.Put(PdfName.MAX_LOWER_CASE, new PdfNumber(max));
			}
			usage.Put(PdfName.ZOOM, pdfDictionary);
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000E42E8 File Offset: 0x000E32E8
		public void SetPrint(string subtype, bool printstate)
		{
			PdfDictionary usage = this.Usage;
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.SUBTYPE, new PdfName(subtype));
			pdfDictionary.Put(PdfName.PRINTSTATE, printstate ? PdfName.ON : PdfName.OFF);
			usage.Put(PdfName.PRINT, pdfDictionary);
		}

		// Token: 0x1700067B RID: 1659
		// (set) Token: 0x060025C4 RID: 9668 RVA: 0x000E433C File Offset: 0x000E333C
		public bool View
		{
			set
			{
				PdfDictionary usage = this.Usage;
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.VIEWSTATE, value ? PdfName.ON : PdfName.OFF);
				usage.Put(PdfName.VIEW, pdfDictionary);
			}
		}

		// Token: 0x1700067C RID: 1660
		// (set) Token: 0x060025C5 RID: 9669 RVA: 0x000E437C File Offset: 0x000E337C
		public string PageElement
		{
			set
			{
				PdfDictionary usage = this.Usage;
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.SUBTYPE, new PdfName(value));
				usage.Put(PdfName.PAGEELEMENT, pdfDictionary);
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000E43B4 File Offset: 0x000E33B4
		public void SetUser(string type, string[] names)
		{
			PdfDictionary usage = this.Usage;
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.TYPE, new PdfName(type));
			PdfArray pdfArray = new PdfArray();
			foreach (string value in names)
			{
				pdfArray.Add(new PdfString(value, "UnicodeBig"));
			}
			usage.Put(PdfName.NAME, pdfArray);
			usage.Put(PdfName.USER, pdfDictionary);
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000E442B File Offset: 0x000E342B
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x000E4433 File Offset: 0x000E3433
		public bool OnPanel
		{
			get
			{
				return this.onPanel;
			}
			set
			{
				this.onPanel = value;
			}
		}

		// Token: 0x04001A39 RID: 6713
		protected PdfIndirectReference refi;

		// Token: 0x04001A3A RID: 6714
		protected List<PdfLayer> children;

		// Token: 0x04001A3B RID: 6715
		protected PdfLayer parent;

		// Token: 0x04001A3C RID: 6716
		protected string title;

		// Token: 0x04001A3D RID: 6717
		private bool on = true;

		// Token: 0x04001A3E RID: 6718
		private bool onPanel = true;
	}
}
