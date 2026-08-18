using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.util;
using System.util.collections;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.collection;
using iTextSharp.text.pdf.events;
using iTextSharp.text.pdf.interfaces;
using iTextSharp.text.pdf.intern;
using iTextSharp.text.xml.xmp;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000DA RID: 218
	public class PdfWriter : DocWriter, IPdfViewerPreferences, IPdfEncryptionSettings, IPdfVersion, IPdfDocumentActions, IPdfPageActions, IPdfXConformance, IPdfRunDirection, IPdfAnnotations
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x000266F0 File Offset: 0x000256F0
		protected PdfWriter()
		{
			this.root = new PdfPages(this);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00026834 File Offset: 0x00025834
		protected PdfWriter(PdfDocument document, Stream os) : base(document, os)
		{
			this.root = new PdfPages(this);
			this.pdf = document;
			this.directContent = new PdfContentByte(this);
			this.directContentUnder = new PdfContentByte(this);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00026998 File Offset: 0x00025998
		public static PdfWriter GetInstance(Document document, Stream os)
		{
			PdfDocument pdfDocument = new PdfDocument();
			document.AddDocListener(pdfDocument);
			PdfWriter pdfWriter = new PdfWriter(pdfDocument, os);
			pdfDocument.AddWriter(pdfWriter);
			return pdfWriter;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000269C4 File Offset: 0x000259C4
		public static PdfWriter GetInstance(Document document, Stream os, IDocListener listener)
		{
			PdfDocument pdfDocument = new PdfDocument();
			pdfDocument.AddDocListener(listener);
			document.AddDocListener(pdfDocument);
			PdfWriter pdfWriter = new PdfWriter(pdfDocument, os);
			pdfDocument.AddWriter(pdfWriter);
			return pdfWriter;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x000269F5 File Offset: 0x000259F5
		internal PdfDocument PdfDocument
		{
			get
			{
				return this.pdf;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x000269FD File Offset: 0x000259FD
		public PdfDictionary Info
		{
			get
			{
				return ((PdfDocument)this.document).Info;
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00026A0F File Offset: 0x00025A0F
		public float GetVerticalPosition(bool ensureNewLine)
		{
			return this.pdf.GetVerticalPosition(ensureNewLine);
		}

		// Token: 0x1700018B RID: 395
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x00026A1D File Offset: 0x00025A1D
		public float InitialLeading
		{
			set
			{
				if (this.open)
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("you.can.t.set.the.initial.leading.if.the.document.is.already.open"));
				}
				this.pdf.Leading = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00026A43 File Offset: 0x00025A43
		public virtual PdfContentByte DirectContent
		{
			get
			{
				if (!this.open)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("the.document.is.not.open"));
				}
				return this.directContent;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00026A63 File Offset: 0x00025A63
		public virtual PdfContentByte DirectContentUnder
		{
			get
			{
				if (!this.open)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("the.document.is.not.open"));
				}
				return this.directContentUnder;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00026A83 File Offset: 0x00025A83
		internal void ResetContent()
		{
			this.directContent.Reset();
			this.directContentUnder.Reset();
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00026A9C File Offset: 0x00025A9C
		internal void AddLocalDestinations(SortedDictionary<string, PdfDocument.Destination> desto)
		{
			foreach (string text in desto.Keys)
			{
				PdfDocument.Destination destination = desto[text];
				PdfDestination destination2 = destination.destination;
				if (destination.reference == null)
				{
					destination.reference = this.PdfIndirectReference;
				}
				if (destination2 == null)
				{
					this.AddToBody(new PdfString("invalid_" + text), destination.reference);
				}
				else
				{
					this.AddToBody(destination2, destination.reference);
				}
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00026B3C File Offset: 0x00025B3C
		public PdfIndirectObject AddToBody(PdfObject objecta)
		{
			return this.body.Add(objecta);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00026B58 File Offset: 0x00025B58
		public PdfIndirectObject AddToBody(PdfObject objecta, bool inObjStm)
		{
			return this.body.Add(objecta, inObjStm);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00026B74 File Offset: 0x00025B74
		public PdfIndirectObject AddToBody(PdfObject objecta, PdfIndirectReference refa)
		{
			return this.body.Add(objecta, refa);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00026B90 File Offset: 0x00025B90
		public PdfIndirectObject AddToBody(PdfObject objecta, PdfIndirectReference refa, bool inObjStm)
		{
			return this.body.Add(objecta, refa, inObjStm);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00026BB0 File Offset: 0x00025BB0
		public PdfIndirectObject AddToBody(PdfObject objecta, int refNumber)
		{
			return this.body.Add(objecta, refNumber);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00026BCC File Offset: 0x00025BCC
		public PdfIndirectObject AddToBody(PdfObject objecta, int refNumber, bool inObjStm)
		{
			return this.body.Add(objecta, refNumber, inObjStm);
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00026BE9 File Offset: 0x00025BE9
		public PdfIndirectReference PdfIndirectReference
		{
			get
			{
				return this.body.PdfIndirectReference;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00026BF6 File Offset: 0x00025BF6
		internal int IndirectReferenceNumber
		{
			get
			{
				return this.body.IndirectReferenceNumber;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00026C03 File Offset: 0x00025C03
		internal OutputStreamCounter Os
		{
			get
			{
				return this.os;
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00026C0C File Offset: 0x00025C0C
		protected virtual PdfDictionary GetCatalog(PdfIndirectReference rootObj)
		{
			PdfDictionary catalog = this.pdf.GetCatalog(rootObj);
			if (this.tagged)
			{
				this.StructureTreeRoot.BuildTree();
				catalog.Put(PdfName.STRUCTTREEROOT, this.structureTreeRoot.Reference);
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.MARKED, PdfBoolean.PDFTRUE);
				if (this.userProperties)
				{
					pdfDictionary.Put(PdfName.USERPROPERTIES, PdfBoolean.PDFTRUE);
				}
				catalog.Put(PdfName.MARKINFO, pdfDictionary);
			}
			if (this.documentOCG.Count != 0)
			{
				this.FillOCProperties(false);
				catalog.Put(PdfName.OCPROPERTIES, this.vOCProperties);
			}
			return catalog;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00026CAF File Offset: 0x00025CAF
		public PdfDictionary ExtraCatalog
		{
			get
			{
				if (this.extraCatalog == null)
				{
					this.extraCatalog = new PdfDictionary();
				}
				return this.extraCatalog;
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00026CCA File Offset: 0x00025CCA
		public void SetLinearPageMode()
		{
			this.root.SetLinearMode(null);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00026CD8 File Offset: 0x00025CD8
		public int ReorderPages(int[] order)
		{
			return this.root.ReorderPages(order);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00026CE8 File Offset: 0x00025CE8
		public virtual PdfIndirectReference GetPageReference(int page)
		{
			page--;
			if (page < 0)
			{
				throw new ArgumentOutOfRangeException(MessageLocalization.GetComposedMessage("the.page.number.must.be.gt.eq.1"));
			}
			PdfIndirectReference pdfIndirectReference;
			if (page < this.pageReferences.Count)
			{
				pdfIndirectReference = this.pageReferences[page];
				if (pdfIndirectReference == null)
				{
					pdfIndirectReference = this.body.PdfIndirectReference;
					this.pageReferences[page] = pdfIndirectReference;
				}
			}
			else
			{
				int num = page - this.pageReferences.Count;
				for (int i = 0; i < num; i++)
				{
					this.pageReferences.Add(null);
				}
				pdfIndirectReference = this.body.PdfIndirectReference;
				this.pageReferences.Add(pdfIndirectReference);
			}
			return pdfIndirectReference;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00026D86 File Offset: 0x00025D86
		public int PageNumber
		{
			get
			{
				return this.pdf.PageNumber;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00026D93 File Offset: 0x00025D93
		internal virtual PdfIndirectReference CurrentPage
		{
			get
			{
				return this.GetPageReference(this.currentPageNumber);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00026DA1 File Offset: 0x00025DA1
		public virtual int CurrentPageNumber
		{
			get
			{
				return this.currentPageNumber;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00026DA9 File Offset: 0x00025DA9
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x00026DB1 File Offset: 0x00025DB1
		public PdfName Tabs
		{
			get
			{
				return this.tabs;
			}
			set
			{
				this.tabs = value;
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00026DBC File Offset: 0x00025DBC
		internal virtual PdfIndirectReference Add(PdfPage page, PdfContents contents)
		{
			if (!this.open)
			{
				throw new PdfException(MessageLocalization.GetComposedMessage("the.document.is.not.open"));
			}
			PdfIndirectObject pdfIndirectObject = this.AddToBody(contents);
			page.Add(pdfIndirectObject.IndirectReference);
			if (this.group != null)
			{
				page.Put(PdfName.GROUP, this.group);
				this.group = null;
			}
			else if (this.rgbTransparencyBlending)
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.TYPE, PdfName.GROUP);
				pdfDictionary.Put(PdfName.S, PdfName.TRANSPARENCY);
				pdfDictionary.Put(PdfName.CS, PdfName.DEVICERGB);
				page.Put(PdfName.GROUP, pdfDictionary);
			}
			this.root.AddPage(page);
			this.currentPageNumber++;
			return null;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00026E7C File Offset: 0x00025E7C
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x00026E84 File Offset: 0x00025E84
		public IPdfPageEvent PageEvent
		{
			get
			{
				return this.pageEvent;
			}
			set
			{
				if (value == null)
				{
					this.pageEvent = null;
					return;
				}
				if (this.pageEvent == null)
				{
					this.pageEvent = value;
					return;
				}
				if (this.pageEvent is PdfPageEventForwarder)
				{
					((PdfPageEventForwarder)this.pageEvent).AddPageEvent(value);
					return;
				}
				PdfPageEventForwarder pdfPageEventForwarder = new PdfPageEventForwarder();
				pdfPageEventForwarder.AddPageEvent(this.pageEvent);
				pdfPageEventForwarder.AddPageEvent(value);
				this.pageEvent = pdfPageEventForwarder;
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00026F34 File Offset: 0x00025F34
		public override void Open()
		{
			base.Open();
			this.pdf_version.WriteHeader(this.os);
			this.body = new PdfWriter.PdfBody(this);
			if (this.pdfxConformance.IsPdfX32002())
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.GAMMA, new PdfArray(new float[]
				{
					2.2f,
					2.2f,
					2.2f
				}));
				pdfDictionary.Put(PdfName.MATRIX, new PdfArray(new float[]
				{
					0.4124f,
					0.2126f,
					0.0193f,
					0.3576f,
					0.7152f,
					0.1192f,
					0.1805f,
					0.0722f,
					0.9505f
				}));
				pdfDictionary.Put(PdfName.WHITEPOINT, new PdfArray(new float[]
				{
					0.9505f,
					1f,
					1.089f
				}));
				PdfArray pdfArray = new PdfArray(PdfName.CALRGB);
				pdfArray.Add(pdfDictionary);
				this.SetDefaultColorspace(PdfName.DEFAULTRGB, this.AddToBody(pdfArray).IndirectReference);
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00027008 File Offset: 0x00026008
		public override void Close()
		{
			if (this.open)
			{
				if (this.currentPageNumber - 1 != this.pageReferences.Count)
				{
					throw new Exception(string.Concat(new object[]
					{
						"The page ",
						this.pageReferences.Count,
						" was requested but the document has only ",
						this.currentPageNumber - 1,
						" pages."
					}));
				}
				this.pdf.Close();
				this.AddSharedObjectsToBody();
				foreach (IPdfOCG pdfOCG in this.documentOCG.Keys)
				{
					this.AddToBody(pdfOCG.PdfObject, pdfOCG.Ref);
				}
				PdfIndirectReference rootObj = this.root.WritePageTree();
				PdfDictionary catalog = this.GetCatalog(rootObj);
				if (this.xmpMetadata != null)
				{
					PdfStream pdfStream = new PdfStream(this.xmpMetadata);
					pdfStream.Put(PdfName.TYPE, PdfName.METADATA);
					pdfStream.Put(PdfName.SUBTYPE, PdfName.XML);
					if (this.crypto != null && !this.crypto.IsMetadataEncrypted())
					{
						PdfArray pdfArray = new PdfArray();
						pdfArray.Add(PdfName.CRYPT);
						pdfStream.Put(PdfName.FILTER, pdfArray);
					}
					catalog.Put(PdfName.METADATA, this.body.Add(pdfStream).IndirectReference);
				}
				if (this.IsPdfX())
				{
					this.pdfxConformance.CompleteInfoDictionary(this.Info);
					this.pdfxConformance.CompleteExtraCatalog(this.ExtraCatalog);
				}
				if (this.extraCatalog != null)
				{
					catalog.MergeDifferent(this.extraCatalog);
				}
				this.WriteOutlines(catalog, false);
				PdfIndirectObject pdfIndirectObject = this.AddToBody(catalog, false);
				PdfIndirectObject pdfIndirectObject2 = this.AddToBody(this.Info, false);
				PdfIndirectReference encryption = null;
				this.body.FlushObjStm();
				PdfObject fileID;
				if (this.crypto != null)
				{
					PdfIndirectObject pdfIndirectObject3 = this.AddToBody(this.crypto.GetEncryptionDictionary(), false);
					encryption = pdfIndirectObject3.IndirectReference;
					fileID = this.crypto.FileID;
				}
				else
				{
					fileID = PdfEncryption.CreateInfoId(PdfEncryption.CreateDocumentId());
				}
				this.body.WriteCrossReferenceTable(this.os, pdfIndirectObject.IndirectReference, pdfIndirectObject2.IndirectReference, encryption, fileID, this.prevxref);
				if (this.fullCompression)
				{
					byte[] isobytes = DocWriter.GetISOBytes("startxref\n");
					this.os.Write(isobytes, 0, isobytes.Length);
					isobytes = DocWriter.GetISOBytes(this.body.Offset.ToString());
					this.os.Write(isobytes, 0, isobytes.Length);
					isobytes = DocWriter.GetISOBytes("\n%%EOF\n");
					this.os.Write(isobytes, 0, isobytes.Length);
				}
				else
				{
					PdfWriter.PdfTrailer pdfTrailer = new PdfWriter.PdfTrailer(this.body.Size, this.body.Offset, pdfIndirectObject.IndirectReference, pdfIndirectObject2.IndirectReference, encryption, fileID, this.prevxref);
					pdfTrailer.ToPdf(this, this.os);
				}
				base.Close();
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00027324 File Offset: 0x00026324
		protected void AddSharedObjectsToBody()
		{
			foreach (FontDetails fontDetails in this.documentFonts.Values)
			{
				fontDetails.WriteFont(this);
			}
			foreach (object[] array in this.formXObjects.Values)
			{
				PdfTemplate pdfTemplate = (PdfTemplate)array[1];
				if ((pdfTemplate == null || !(pdfTemplate.IndirectReference is PRIndirectReference)) && pdfTemplate != null && pdfTemplate.Type == 1)
				{
					this.AddToBody(pdfTemplate.GetFormXObject(this.compressionLevel), pdfTemplate.IndirectReference);
				}
			}
			foreach (PdfReaderInstance pdfReaderInstance in this.readerInstances.Values)
			{
				this.currentPdfReaderInstance = pdfReaderInstance;
				this.currentPdfReaderInstance.WriteAllPages();
			}
			this.currentPdfReaderInstance = null;
			foreach (ColorDetails colorDetails in this.documentColors.Values)
			{
				this.AddToBody(colorDetails.GetSpotColor(this), colorDetails.IndirectReference);
			}
			foreach (PdfPatternPainter pdfPatternPainter in this.documentPatterns.Keys)
			{
				this.AddToBody(pdfPatternPainter.GetPattern(this.compressionLevel), pdfPatternPainter.IndirectReference);
			}
			foreach (PdfShadingPattern pdfShadingPattern in this.documentShadingPatterns.Keys)
			{
				pdfShadingPattern.AddToBody();
			}
			foreach (PdfShading pdfShading in this.documentShadings.Keys)
			{
				pdfShading.AddToBody();
			}
			foreach (KeyValuePair<PdfDictionary, PdfObject[]> keyValuePair in this.documentExtGState)
			{
				PdfDictionary key = keyValuePair.Key;
				PdfObject[] value = keyValuePair.Value;
				this.AddToBody(key, (PdfIndirectReference)value[1]);
			}
			foreach (KeyValuePair<object, PdfObject[]> keyValuePair2 in this.documentProperties)
			{
				object key2 = keyValuePair2.Key;
				PdfObject[] value2 = keyValuePair2.Value;
				if (key2 is PdfLayerMembership)
				{
					PdfLayerMembership pdfLayerMembership = (PdfLayerMembership)key2;
					this.AddToBody(pdfLayerMembership.PdfObject, pdfLayerMembership.Ref);
				}
				else if (key2 is PdfDictionary && !(key2 is PdfLayer))
				{
					this.AddToBody((PdfDictionary)key2, (PdfIndirectReference)value2[1]);
				}
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x000276A0 File Offset: 0x000266A0
		public PdfOutline RootOutline
		{
			get
			{
				return this.directContent.RootOutline;
			}
		}

		// Token: 0x17000198 RID: 408
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x000276AD File Offset: 0x000266AD
		public IList<Dictionary<string, object>> Outlines
		{
			set
			{
				this.newBookmarks = value;
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000276B8 File Offset: 0x000266B8
		protected internal void WriteOutlines(PdfDictionary catalog, bool namedAsNames)
		{
			if (this.newBookmarks == null || this.newBookmarks.Count == 0)
			{
				return;
			}
			PdfDictionary pdfDictionary = new PdfDictionary();
			PdfIndirectReference pdfIndirectReference = this.PdfIndirectReference;
			object[] array = SimpleBookmark.IterateOutlines(this, pdfIndirectReference, this.newBookmarks, namedAsNames);
			pdfDictionary.Put(PdfName.FIRST, (PdfIndirectReference)array[0]);
			pdfDictionary.Put(PdfName.LAST, (PdfIndirectReference)array[1]);
			pdfDictionary.Put(PdfName.COUNT, new PdfNumber((int)array[2]));
			this.AddToBody(pdfDictionary, pdfIndirectReference);
			catalog.Put(PdfName.OUTLINES, pdfIndirectReference);
		}

		// Token: 0x17000199 RID: 409
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x0002774A File Offset: 0x0002674A
		public virtual char PdfVersion
		{
			set
			{
				this.pdf_version.PdfVersion = value;
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00027758 File Offset: 0x00026758
		public void SetAtLeastPdfVersion(char version)
		{
			this.pdf_version.SetAtLeastPdfVersion(version);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00027766 File Offset: 0x00026766
		public void SetPdfVersion(PdfName version)
		{
			this.pdf_version.SetPdfVersion(version);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00027774 File Offset: 0x00026774
		public void AddDeveloperExtension(PdfDeveloperExtension de)
		{
			this.pdf_version.AddDeveloperExtension(de);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00027782 File Offset: 0x00026782
		internal PdfVersionImp GetPdfVersion()
		{
			return this.pdf_version;
		}

		// Token: 0x1700019A RID: 410
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0002778A File Offset: 0x0002678A
		public virtual int ViewerPreferences
		{
			set
			{
				this.pdf.ViewerPreferences = value;
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00027798 File Offset: 0x00026798
		public virtual void AddViewerPreference(PdfName key, PdfObject value)
		{
			this.pdf.AddViewerPreference(key, value);
		}

		// Token: 0x1700019B RID: 411
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x000277A7 File Offset: 0x000267A7
		public virtual PdfPageLabels PageLabels
		{
			set
			{
				this.pdf.PageLabels = value;
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000277B8 File Offset: 0x000267B8
		public void AddNamedDestinations(IDictionary<string, string> map, int page_offset)
		{
			foreach (KeyValuePair<string, string> keyValuePair in map)
			{
				string value = keyValuePair.Value;
				int num = int.Parse(value.Substring(0, value.IndexOf(" ")));
				PdfDestination dest = new PdfDestination(value.Substring(value.IndexOf(" ") + 1));
				this.AddNamedDestination(keyValuePair.Key, num + page_offset, dest);
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00027848 File Offset: 0x00026848
		public void AddNamedDestination(string name, int page, PdfDestination dest)
		{
			dest.AddPage(this.GetPageReference(page));
			this.pdf.LocalDestination(name, dest);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00027866 File Offset: 0x00026866
		public virtual void AddJavaScript(PdfAction js)
		{
			this.pdf.AddJavaScript(js);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00027874 File Offset: 0x00026874
		public virtual void AddJavaScript(string code, bool unicode)
		{
			this.AddJavaScript(PdfAction.JavaScript(code, this, unicode));
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00027884 File Offset: 0x00026884
		public virtual void AddJavaScript(string code)
		{
			this.AddJavaScript(code, false);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0002788E File Offset: 0x0002688E
		public void AddJavaScript(string name, PdfAction js)
		{
			this.pdf.AddJavaScript(name, js);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0002789D File Offset: 0x0002689D
		public void AddJavaScript(string name, string code, bool unicode)
		{
			this.AddJavaScript(name, PdfAction.JavaScript(code, this, unicode));
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000278AE File Offset: 0x000268AE
		public void AddJavaScript(string name, string code)
		{
			this.AddJavaScript(name, code, false);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000278B9 File Offset: 0x000268B9
		public virtual void AddFileAttachment(string description, byte[] fileStore, string file, string fileDisplay)
		{
			this.AddFileAttachment(description, PdfFileSpecification.FileEmbedded(this, file, fileDisplay, fileStore));
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000278CC File Offset: 0x000268CC
		public virtual void AddFileAttachment(string description, PdfFileSpecification fs)
		{
			this.pdf.AddFileAttachment(description, fs);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000278DB File Offset: 0x000268DB
		public void AddFileAttachment(PdfFileSpecification fs)
		{
			this.pdf.AddFileAttachment(null, fs);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000278EA File Offset: 0x000268EA
		public virtual void SetOpenAction(string name)
		{
			this.pdf.SetOpenAction(name);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000278F8 File Offset: 0x000268F8
		public virtual void SetOpenAction(PdfAction action)
		{
			this.pdf.SetOpenAction(action);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00027908 File Offset: 0x00026908
		public virtual void SetAdditionalAction(PdfName actionType, PdfAction action)
		{
			if (!actionType.Equals(PdfWriter.DOCUMENT_CLOSE) && !actionType.Equals(PdfWriter.WILL_SAVE) && !actionType.Equals(PdfWriter.DID_SAVE) && !actionType.Equals(PdfWriter.WILL_PRINT) && !actionType.Equals(PdfWriter.DID_PRINT))
			{
				throw new PdfException(MessageLocalization.GetComposedMessage("invalid.additional.action.type.1", actionType.ToString()));
			}
			this.pdf.AddAdditionalAction(actionType, action);
		}

		// Token: 0x1700019C RID: 412
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x00027979 File Offset: 0x00026979
		public PdfCollection Collection
		{
			set
			{
				this.SetAtLeastPdfVersion('7');
				this.pdf.Collection = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0002798F File Offset: 0x0002698F
		public PdfAcroForm AcroForm
		{
			get
			{
				return this.pdf.AcroForm;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0002799C File Offset: 0x0002699C
		public virtual void AddAnnotation(PdfAnnotation annot)
		{
			this.pdf.AddAnnotation(annot);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000279AA File Offset: 0x000269AA
		internal virtual void AddAnnotation(PdfAnnotation annot, int page)
		{
			this.AddAnnotation(annot);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000279B3 File Offset: 0x000269B3
		public virtual void AddCalculationOrder(PdfFormField annot)
		{
			this.pdf.AddCalculationOrder(annot);
		}

		// Token: 0x1700019E RID: 414
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x000279C1 File Offset: 0x000269C1
		public virtual int SigFlags
		{
			set
			{
				this.pdf.SigFlags = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x000279D8 File Offset: 0x000269D8
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x000279CF File Offset: 0x000269CF
		public byte[] XmpMetadata
		{
			get
			{
				return this.xmpMetadata;
			}
			set
			{
				this.xmpMetadata = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x000279E0 File Offset: 0x000269E0
		public byte[] PageXmpMetadata
		{
			set
			{
				this.pdf.XmpMetadata = value;
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000279EE File Offset: 0x000269EE
		public void CreateXmpMetadata()
		{
			this.XmpMetadata = this.CreateXmpMetadataBytes();
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000279FC File Offset: 0x000269FC
		private byte[] CreateXmpMetadataBytes()
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				XmpWriter xmpWriter = new XmpWriter(memoryStream, this.pdf.Info, this.pdfxConformance.PDFXConformance);
				xmpWriter.Close();
			}
			catch (IOException)
			{
			}
			return memoryStream.ToArray();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00027ACA File Offset: 0x00026ACA
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00027A50 File Offset: 0x00026A50
		public int PDFXConformance
		{
			get
			{
				return this.pdfxConformance.PDFXConformance;
			}
			set
			{
				if (this.pdfxConformance.PDFXConformance == value)
				{
					return;
				}
				if (this.pdf.IsOpen())
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("pdfx.conformance.can.only.be.set.before.opening.the.document"));
				}
				if (this.crypto != null)
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("a.pdfx.conforming.document.cannot.be.encrypted"));
				}
				if (value == 3 || value == 4)
				{
					this.PdfVersion = '4';
				}
				else if (value != 0)
				{
					this.PdfVersion = '3';
				}
				this.pdfxConformance.PDFXConformance = value;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00027AD7 File Offset: 0x00026AD7
		public bool IsPdfX()
		{
			return this.pdfxConformance.IsPdfX();
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00027AE4 File Offset: 0x00026AE4
		public void SetOutputIntents(string outputConditionIdentifier, string outputCondition, string registryName, string info, ICC_Profile colorProfile)
		{
			PdfDictionary pdfDictionary = this.ExtraCatalog;
			pdfDictionary = new PdfDictionary(PdfName.OUTPUTINTENT);
			if (outputCondition != null)
			{
				pdfDictionary.Put(PdfName.OUTPUTCONDITION, new PdfString(outputCondition, "UnicodeBig"));
			}
			if (outputConditionIdentifier != null)
			{
				pdfDictionary.Put(PdfName.OUTPUTCONDITIONIDENTIFIER, new PdfString(outputConditionIdentifier, "UnicodeBig"));
			}
			if (registryName != null)
			{
				pdfDictionary.Put(PdfName.REGISTRYNAME, new PdfString(registryName, "UnicodeBig"));
			}
			if (info != null)
			{
				pdfDictionary.Put(PdfName.INFO, new PdfString(info, "UnicodeBig"));
			}
			if (colorProfile != null)
			{
				PdfStream objecta = new PdfICCBased(colorProfile, this.compressionLevel);
				pdfDictionary.Put(PdfName.DESTOUTPUTPROFILE, this.AddToBody(objecta).IndirectReference);
			}
			PdfName value;
			if (this.pdfxConformance.IsPdfA1() || "PDFA/1".Equals(outputCondition))
			{
				value = PdfName.GTS_PDFA1;
			}
			else
			{
				value = PdfName.GTS_PDFX;
			}
			pdfDictionary.Put(PdfName.S, value);
			this.extraCatalog.Put(PdfName.OUTPUTINTENTS, new PdfArray(pdfDictionary));
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00027BDC File Offset: 0x00026BDC
		public void SetOutputIntents(string outputConditionIdentifier, string outputCondition, string registryName, string info, byte[] destOutputProfile)
		{
			ICC_Profile colorProfile = (destOutputProfile == null) ? null : ICC_Profile.GetInstance(destOutputProfile);
			this.SetOutputIntents(outputConditionIdentifier, outputCondition, registryName, info, colorProfile);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00027C04 File Offset: 0x00026C04
		public bool SetOutputIntents(PdfReader reader, bool checkExistence)
		{
			PdfDictionary catalog = reader.Catalog;
			PdfArray asArray = catalog.GetAsArray(PdfName.OUTPUTINTENTS);
			if (asArray == null)
			{
				return false;
			}
			List<PdfObject> arrayList = asArray.ArrayList;
			if (asArray.Size == 0)
			{
				return false;
			}
			PdfDictionary asDict = asArray.GetAsDict(0);
			PdfObject pdfObject = PdfReader.GetPdfObject(asDict.Get(PdfName.S));
			if (pdfObject == null || !PdfName.GTS_PDFX.Equals(pdfObject))
			{
				return false;
			}
			if (checkExistence)
			{
				return true;
			}
			PRStream prstream = (PRStream)PdfReader.GetPdfObject(asDict.Get(PdfName.DESTOUTPUTPROFILE));
			byte[] destOutputProfile = null;
			if (prstream != null)
			{
				destOutputProfile = PdfReader.GetStreamBytes(prstream);
			}
			this.SetOutputIntents(PdfWriter.GetNameString(asDict, PdfName.OUTPUTCONDITIONIDENTIFIER), PdfWriter.GetNameString(asDict, PdfName.OUTPUTCONDITION), PdfWriter.GetNameString(asDict, PdfName.REGISTRYNAME), PdfWriter.GetNameString(asDict, PdfName.INFO), destOutputProfile);
			return true;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00027CC8 File Offset: 0x00026CC8
		private static string GetNameString(PdfDictionary dic, PdfName key)
		{
			PdfObject pdfObject = PdfReader.GetPdfObject(dic.Get(key));
			if (pdfObject == null || !pdfObject.IsString())
			{
				return null;
			}
			return ((PdfString)pdfObject).ToUnicodeString();
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00027CFA File Offset: 0x00026CFA
		internal PdfEncryption Encryption
		{
			get
			{
				return this.crypto;
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00027D04 File Offset: 0x00026D04
		public void SetEncryption(byte[] userPassword, byte[] ownerPassword, int permissions, int encryptionType)
		{
			if (this.pdf.IsOpen())
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("encryption.can.only.be.added.before.opening.the.document"));
			}
			this.crypto = new PdfEncryption();
			this.crypto.SetCryptoMode(encryptionType, 0);
			this.crypto.SetupAllKeys(userPassword, ownerPassword, permissions);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00027D58 File Offset: 0x00026D58
		public void SetEncryption(X509Certificate[] certs, int[] permissions, int encryptionType)
		{
			if (this.pdf.IsOpen())
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("encryption.can.only.be.added.before.opening.the.document"));
			}
			this.crypto = new PdfEncryption();
			if (certs != null)
			{
				for (int i = 0; i < certs.Length; i++)
				{
					this.crypto.AddRecipient(certs[i], permissions[i]);
				}
			}
			this.crypto.SetCryptoMode(encryptionType, 0);
			this.crypto.GetEncryptionDictionary();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00027DC8 File Offset: 0x00026DC8
		public void SetEncryption(byte[] userPassword, byte[] ownerPassword, int permissions, bool strength128Bits)
		{
			this.SetEncryption(userPassword, ownerPassword, permissions, strength128Bits ? 1 : 0);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00027DDB File Offset: 0x00026DDB
		public void SetEncryption(bool strength, string userPassword, string ownerPassword, int permissions)
		{
			this.SetEncryption(DocWriter.GetISOBytes(userPassword), DocWriter.GetISOBytes(ownerPassword), permissions, strength);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00027DF2 File Offset: 0x00026DF2
		public void SetEncryption(int encryptionType, string userPassword, string ownerPassword, int permissions)
		{
			this.SetEncryption(DocWriter.GetISOBytes(userPassword), DocWriter.GetISOBytes(ownerPassword), permissions, encryptionType);
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00027E09 File Offset: 0x00026E09
		public bool FullCompression
		{
			get
			{
				return this.fullCompression;
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00027E11 File Offset: 0x00026E11
		public void SetFullCompression()
		{
			this.fullCompression = true;
			this.SetAtLeastPdfVersion('5');
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00027E3C File Offset: 0x00026E3C
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x00027E22 File Offset: 0x00026E22
		public int CompressionLevel
		{
			get
			{
				return this.compressionLevel;
			}
			set
			{
				if (value < 0 || value > 9)
				{
					this.compressionLevel = -1;
					return;
				}
				this.compressionLevel = value;
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00027E44 File Offset: 0x00026E44
		internal FontDetails AddSimple(BaseFont bf)
		{
			if (bf.FontType == 4)
			{
				return new FontDetails(new PdfName("F" + this.fontNumber++), ((DocumentFont)bf).IndirectReference, bf);
			}
			FontDetails fontDetails;
			if (!this.documentFonts.TryGetValue(bf, out fontDetails))
			{
				PdfXConformanceImp.CheckPDFXConformance(this, 4, bf);
				fontDetails = new FontDetails(new PdfName("F" + this.fontNumber++), this.body.PdfIndirectReference, bf);
				this.documentFonts[bf] = fontDetails;
			}
			return fontDetails;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00027EF0 File Offset: 0x00026EF0
		internal void EliminateFontSubset(PdfDictionary fonts)
		{
			foreach (FontDetails fontDetails in this.documentFonts.Values)
			{
				if (fonts.Get(fontDetails.FontName) != null)
				{
					fontDetails.Subset = false;
				}
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00027F58 File Offset: 0x00026F58
		internal PdfName AddDirectTemplateSimple(PdfTemplate template, PdfName forcedName)
		{
			PdfIndirectReference indirectReference = template.IndirectReference;
			object[] array;
			this.formXObjects.TryGetValue(indirectReference, out array);
			PdfName pdfName;
			if (array == null)
			{
				if (forcedName == null)
				{
					pdfName = new PdfName("Xf" + this.formXObjectsCounter);
					this.formXObjectsCounter++;
				}
				else
				{
					pdfName = forcedName;
				}
				if (template.Type == 2)
				{
					PdfImportedPage pdfImportedPage = (PdfImportedPage)template;
					PdfReader reader = pdfImportedPage.PdfReaderInstance.Reader;
					if (!this.readerInstances.ContainsKey(reader))
					{
						this.readerInstances[reader] = pdfImportedPage.PdfReaderInstance;
					}
					template = null;
				}
				this.formXObjects[indirectReference] = new object[]
				{
					pdfName,
					template
				};
			}
			else
			{
				pdfName = (PdfName)array[0];
			}
			return pdfName;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00028024 File Offset: 0x00027024
		public void ReleaseTemplate(PdfTemplate tp)
		{
			PdfIndirectReference indirectReference = tp.IndirectReference;
			object[] array;
			this.formXObjects.TryGetValue(indirectReference, out array);
			if (array == null || array[1] == null)
			{
				return;
			}
			PdfTemplate pdfTemplate = (PdfTemplate)array[1];
			if (pdfTemplate.IndirectReference is PRIndirectReference)
			{
				return;
			}
			if (pdfTemplate.Type == 1)
			{
				this.AddToBody(pdfTemplate.GetFormXObject(this.compressionLevel), pdfTemplate.IndirectReference);
				array[1] = null;
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002808D File Offset: 0x0002708D
		public virtual PdfImportedPage GetImportedPage(PdfReader reader, int pageNumber)
		{
			return this.GetPdfReaderInstance(reader).GetImportedPage(pageNumber);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002809C File Offset: 0x0002709C
		protected virtual PdfReaderInstance GetPdfReaderInstance(PdfReader reader)
		{
			PdfReaderInstance pdfReaderInstance;
			this.readerInstances.TryGetValue(reader, out pdfReaderInstance);
			if (pdfReaderInstance == null)
			{
				pdfReaderInstance = reader.GetPdfReaderInstance(this);
				this.readerInstances[reader] = pdfReaderInstance;
			}
			return pdfReaderInstance;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000280D1 File Offset: 0x000270D1
		public virtual void FreeReader(PdfReader reader)
		{
			this.readerInstances.TryGetValue(reader, out this.currentPdfReaderInstance);
			if (this.currentPdfReaderInstance == null)
			{
				return;
			}
			this.currentPdfReaderInstance.WriteAllPages();
			this.currentPdfReaderInstance = null;
			this.readerInstances.Remove(reader);
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0002810E File Offset: 0x0002710E
		public int CurrentDocumentSize
		{
			get
			{
				return this.body.Offset + this.body.Size * 20 + 72;
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0002812D File Offset: 0x0002712D
		protected internal virtual int GetNewObjectNumber(PdfReader reader, int number, int generation)
		{
			return this.currentPdfReaderInstance.GetNewObjectNumber(number, generation);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002813C File Offset: 0x0002713C
		internal virtual RandomAccessFileOrArray GetReaderFile(PdfReader reader)
		{
			return this.currentPdfReaderInstance.ReaderFile;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002814C File Offset: 0x0002714C
		internal PdfName GetColorspaceName()
		{
			return new PdfName("CS" + this.colorNumber++);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00028180 File Offset: 0x00027180
		internal ColorDetails AddSimple(PdfSpotColor spc)
		{
			ColorDetails colorDetails;
			this.documentColors.TryGetValue(spc, out colorDetails);
			if (colorDetails == null)
			{
				colorDetails = new ColorDetails(this.GetColorspaceName(), this.body.PdfIndirectReference, spc);
				this.documentColors[spc] = colorDetails;
			}
			return colorDetails;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000281C8 File Offset: 0x000271C8
		internal PdfName AddSimplePattern(PdfPatternPainter painter)
		{
			PdfName pdfName;
			this.documentPatterns.TryGetValue(painter, out pdfName);
			if (pdfName == null)
			{
				pdfName = new PdfName("P" + this.patternNumber);
				this.patternNumber++;
				this.documentPatterns[painter] = pdfName;
			}
			return pdfName;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00028220 File Offset: 0x00027220
		internal void AddSimpleShadingPattern(PdfShadingPattern shading)
		{
			if (!this.documentShadingPatterns.ContainsKey(shading))
			{
				shading.Name = this.patternNumber;
				this.patternNumber++;
				this.documentShadingPatterns[shading] = null;
				this.AddSimpleShading(shading.Shading);
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002826E File Offset: 0x0002726E
		internal void AddSimpleShading(PdfShading shading)
		{
			if (!this.documentShadings.ContainsKey(shading))
			{
				this.documentShadings[shading] = null;
				shading.Name = this.documentShadings.Count;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002829C File Offset: 0x0002729C
		internal PdfObject[] AddSimpleExtGState(PdfDictionary gstate)
		{
			if (!this.documentExtGState.ContainsKey(gstate))
			{
				PdfXConformanceImp.CheckPDFXConformance(this, 6, gstate);
				this.documentExtGState[gstate] = new PdfObject[]
				{
					new PdfName("GS" + (this.documentExtGState.Count + 1)),
					this.PdfIndirectReference
				};
			}
			return this.documentExtGState[gstate];
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0002830C File Offset: 0x0002730C
		internal PdfObject[] AddSimpleProperty(object prop, PdfIndirectReference refi)
		{
			if (!this.documentProperties.ContainsKey(prop))
			{
				if (prop is IPdfOCG)
				{
					PdfXConformanceImp.CheckPDFXConformance(this, 7, null);
				}
				this.documentProperties[prop] = new PdfObject[]
				{
					new PdfName("Pr" + (this.documentProperties.Count + 1)),
					refi
				};
			}
			return this.documentProperties[prop];
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0002837F File Offset: 0x0002737F
		internal bool PropertyExists(object prop)
		{
			return this.documentProperties.ContainsKey(prop);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0002838D File Offset: 0x0002738D
		public void SetTagged()
		{
			if (this.open)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("tagging.must.be.set.before.opening.the.document"));
			}
			this.tagged = true;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000283AE File Offset: 0x000273AE
		public bool IsTagged()
		{
			return this.tagged;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x000283B6 File Offset: 0x000273B6
		public PdfStructureTreeRoot StructureTreeRoot
		{
			get
			{
				if (this.tagged && this.structureTreeRoot == null)
				{
					this.structureTreeRoot = new PdfStructureTreeRoot(this);
				}
				return this.structureTreeRoot;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x000283DA File Offset: 0x000273DA
		public PdfOCProperties OCProperties
		{
			get
			{
				this.FillOCProperties(true);
				return this.vOCProperties;
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000283EC File Offset: 0x000273EC
		public void AddOCGRadioGroup(List<PdfLayer> group)
		{
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < group.Count; i++)
			{
				PdfLayer pdfLayer = group[i];
				if (pdfLayer.Title == null)
				{
					pdfArray.Add(pdfLayer.Ref);
				}
			}
			if (pdfArray.Size == 0)
			{
				return;
			}
			this.OCGRadioGroup.Add(pdfArray);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00028443 File Offset: 0x00027443
		public void LockLayer(PdfLayer layer)
		{
			this.OCGLocked.Add(layer.Ref);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00028458 File Offset: 0x00027458
		private static void GetOCGOrder(PdfArray order, PdfLayer layer)
		{
			if (!layer.OnPanel)
			{
				return;
			}
			if (layer.Title == null)
			{
				order.Add(layer.Ref);
			}
			List<PdfLayer> children = layer.Children;
			if (children == null)
			{
				return;
			}
			PdfArray pdfArray = new PdfArray();
			if (layer.Title != null)
			{
				pdfArray.Add(new PdfString(layer.Title, "UnicodeBig"));
			}
			for (int i = 0; i < children.Count; i++)
			{
				PdfWriter.GetOCGOrder(pdfArray, children[i]);
			}
			if (pdfArray.Size > 0)
			{
				order.Add(pdfArray);
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000284E4 File Offset: 0x000274E4
		private void AddASEvent(PdfName eventa, PdfName category)
		{
			PdfArray pdfArray = new PdfArray();
			foreach (IPdfOCG pdfOCG in this.documentOCG.Keys)
			{
				PdfLayer pdfLayer = (PdfLayer)pdfOCG;
				PdfDictionary pdfDictionary = (PdfDictionary)pdfLayer.Get(PdfName.USAGE);
				if (pdfDictionary != null && pdfDictionary.Get(category) != null)
				{
					pdfArray.Add(pdfLayer.Ref);
				}
			}
			if (pdfArray.Size == 0)
			{
				return;
			}
			PdfDictionary pdfDictionary2 = (PdfDictionary)this.vOCProperties.Get(PdfName.D);
			PdfArray pdfArray2 = (PdfArray)pdfDictionary2.Get(PdfName.AS);
			if (pdfArray2 == null)
			{
				pdfArray2 = new PdfArray();
				pdfDictionary2.Put(PdfName.AS, pdfArray2);
			}
			PdfDictionary pdfDictionary3 = new PdfDictionary();
			pdfDictionary3.Put(PdfName.EVENT, eventa);
			pdfDictionary3.Put(PdfName.CATEGORY, new PdfArray(category));
			pdfDictionary3.Put(PdfName.OCGS, pdfArray);
			pdfArray2.Add(pdfDictionary3);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000285F4 File Offset: 0x000275F4
		protected void FillOCProperties(bool erase)
		{
			if (this.vOCProperties == null)
			{
				this.vOCProperties = new PdfOCProperties();
			}
			if (erase)
			{
				this.vOCProperties.Remove(PdfName.OCGS);
				this.vOCProperties.Remove(PdfName.D);
			}
			if (this.vOCProperties.Get(PdfName.OCGS) == null)
			{
				PdfArray pdfArray = new PdfArray();
				foreach (IPdfOCG pdfOCG in this.documentOCG.Keys)
				{
					PdfLayer pdfLayer = (PdfLayer)pdfOCG;
					pdfArray.Add(pdfLayer.Ref);
				}
				this.vOCProperties.Put(PdfName.OCGS, pdfArray);
			}
			if (this.vOCProperties.Get(PdfName.D) != null)
			{
				return;
			}
			List<IPdfOCG> list = new List<IPdfOCG>(this.documentOCGorder);
			ListIterator<IPdfOCG> listIterator = new ListIterator<IPdfOCG>(list);
			while (listIterator.HasNext())
			{
				PdfLayer pdfLayer2 = (PdfLayer)listIterator.Next();
				if (pdfLayer2.Parent != null)
				{
					listIterator.Remove();
				}
			}
			PdfArray pdfArray2 = new PdfArray();
			foreach (IPdfOCG pdfOCG2 in list)
			{
				PdfLayer layer = (PdfLayer)pdfOCG2;
				PdfWriter.GetOCGOrder(pdfArray2, layer);
			}
			PdfDictionary pdfDictionary = new PdfDictionary();
			this.vOCProperties.Put(PdfName.D, pdfDictionary);
			pdfDictionary.Put(PdfName.ORDER, pdfArray2);
			PdfArray pdfArray3 = new PdfArray();
			foreach (IPdfOCG pdfOCG3 in this.documentOCG.Keys)
			{
				PdfLayer pdfLayer3 = (PdfLayer)pdfOCG3;
				if (!pdfLayer3.On)
				{
					pdfArray3.Add(pdfLayer3.Ref);
				}
			}
			if (pdfArray3.Size > 0)
			{
				pdfDictionary.Put(PdfName.OFF, pdfArray3);
			}
			if (this.OCGRadioGroup.Size > 0)
			{
				pdfDictionary.Put(PdfName.RBGROUPS, this.OCGRadioGroup);
			}
			if (this.OCGLocked.Size > 0)
			{
				pdfDictionary.Put(PdfName.LOCKED, this.OCGLocked);
			}
			this.AddASEvent(PdfName.VIEW, PdfName.ZOOM);
			this.AddASEvent(PdfName.VIEW, PdfName.VIEW);
			this.AddASEvent(PdfName.PRINT, PdfName.PRINT);
			this.AddASEvent(PdfName.EXPORT, PdfName.EXPORT);
			pdfDictionary.Put(PdfName.LISTMODE, PdfName.VISIBLEPAGES);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00028888 File Offset: 0x00027888
		internal void RegisterLayer(IPdfOCG layer)
		{
			PdfXConformanceImp.CheckPDFXConformance(this, 7, null);
			if (!(layer is PdfLayer))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("only.pdflayer.is.accepted"));
			}
			PdfLayer pdfLayer = (PdfLayer)layer;
			if (pdfLayer.Title != null)
			{
				this.documentOCGorder.Add(layer);
				return;
			}
			if (!this.documentOCG.ContainsKey(layer))
			{
				this.documentOCG[layer] = null;
				this.documentOCGorder.Add(layer);
				return;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x000288F9 File Offset: 0x000278F9
		public Rectangle PageSize
		{
			get
			{
				return this.pdf.PageSize;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00028906 File Offset: 0x00027906
		public virtual Rectangle CropBoxSize
		{
			set
			{
				this.pdf.CropBoxSize = value;
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00028914 File Offset: 0x00027914
		public void SetBoxSize(string boxName, Rectangle size)
		{
			this.pdf.SetBoxSize(boxName, size);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00028923 File Offset: 0x00027923
		public Rectangle GetBoxSize(string boxName)
		{
			return this.pdf.GetBoxSize(boxName);
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00028943 File Offset: 0x00027943
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x00028931 File Offset: 0x00027931
		public bool PageEmpty
		{
			get
			{
				return this.pdf.PageEmpty;
			}
			set
			{
				if (value)
				{
					return;
				}
				this.pdf.PageEmpty = value;
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00028950 File Offset: 0x00027950
		public virtual void SetPageAction(PdfName actionType, PdfAction action)
		{
			if (!actionType.Equals(PdfWriter.PAGE_OPEN) && !actionType.Equals(PdfWriter.PAGE_CLOSE))
			{
				throw new PdfException(MessageLocalization.GetComposedMessage("invalid.page.additional.action.type.1", actionType.ToString()));
			}
			this.pdf.SetPageAction(actionType, action);
		}

		// Token: 0x170001AB RID: 427
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0002898F File Offset: 0x0002798F
		public virtual int Duration
		{
			set
			{
				this.pdf.Duration = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x0002899D File Offset: 0x0002799D
		public virtual PdfTransition Transition
		{
			set
			{
				this.pdf.Transition = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x000289AB File Offset: 0x000279AB
		public virtual Image Thumbnail
		{
			set
			{
				this.pdf.Thumbnail = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x000289B9 File Offset: 0x000279B9
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x000289C1 File Offset: 0x000279C1
		public PdfDictionary Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x000289E7 File Offset: 0x000279E7
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x000289CA File Offset: 0x000279CA
		public virtual float SpaceCharRatio
		{
			get
			{
				return this.spaceCharRatio;
			}
			set
			{
				if (value < 0.001f)
				{
					this.spaceCharRatio = 0.001f;
					return;
				}
				this.spaceCharRatio = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00028A16 File Offset: 0x00027A16
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x000289EF File Offset: 0x000279EF
		public virtual int RunDirection
		{
			get
			{
				return this.runDirection;
			}
			set
			{
				if (value < 1 || value > 3)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("invalid.run.direction.1", value));
				}
				this.runDirection = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00028A1E File Offset: 0x00027A1E
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x00028A26 File Offset: 0x00027A26
		public float Userunit
		{
			get
			{
				return this.userunit;
			}
			set
			{
				if (value < 1f || value > 75000f)
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("userunit.should.be.a.value.between.1.and.75000"));
				}
				this.userunit = value;
				this.SetAtLeastPdfVersion('6');
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00028A57 File Offset: 0x00027A57
		public PdfDictionary DefaultColorspace
		{
			get
			{
				return this.defaultColorspace;
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00028A5F File Offset: 0x00027A5F
		public void SetDefaultColorspace(PdfName key, PdfObject cs)
		{
			if (cs == null || cs.IsNull())
			{
				this.defaultColorspace.Remove(key);
			}
			this.defaultColorspace.Put(key, cs);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00028A88 File Offset: 0x00027A88
		internal ColorDetails AddSimplePatternColorspace(BaseColor color)
		{
			int type = ExtendedColor.GetType(color);
			if (type == 4 || type == 5)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("an.uncolored.tile.pattern.can.not.have.another.pattern.or.shading.as.color"));
			}
			switch (type)
			{
			case 0:
				if (this.patternColorspaceRGB == null)
				{
					this.patternColorspaceRGB = new ColorDetails(this.GetColorspaceName(), this.body.PdfIndirectReference, null);
					PdfArray pdfArray = new PdfArray(PdfName.PATTERN);
					pdfArray.Add(PdfName.DEVICERGB);
					this.AddToBody(pdfArray, this.patternColorspaceRGB.IndirectReference);
				}
				return this.patternColorspaceRGB;
			case 1:
				if (this.patternColorspaceGRAY == null)
				{
					this.patternColorspaceGRAY = new ColorDetails(this.GetColorspaceName(), this.body.PdfIndirectReference, null);
					PdfArray pdfArray2 = new PdfArray(PdfName.PATTERN);
					pdfArray2.Add(PdfName.DEVICEGRAY);
					this.AddToBody(pdfArray2, this.patternColorspaceGRAY.IndirectReference);
				}
				return this.patternColorspaceGRAY;
			case 2:
				if (this.patternColorspaceCMYK == null)
				{
					this.patternColorspaceCMYK = new ColorDetails(this.GetColorspaceName(), this.body.PdfIndirectReference, null);
					PdfArray pdfArray3 = new PdfArray(PdfName.PATTERN);
					pdfArray3.Add(PdfName.DEVICECMYK);
					this.AddToBody(pdfArray3, this.patternColorspaceCMYK.IndirectReference);
				}
				return this.patternColorspaceCMYK;
			case 3:
			{
				ColorDetails colorDetails = this.AddSimple(((SpotColor)color).PdfSpotColor);
				ColorDetails colorDetails2;
				this.documentSpotPatterns.TryGetValue(colorDetails, out colorDetails2);
				if (colorDetails2 == null)
				{
					colorDetails2 = new ColorDetails(this.GetColorspaceName(), this.body.PdfIndirectReference, null);
					PdfArray pdfArray4 = new PdfArray(PdfName.PATTERN);
					pdfArray4.Add(colorDetails.IndirectReference);
					this.AddToBody(pdfArray4, colorDetails2.IndirectReference);
					this.documentSpotPatterns[colorDetails] = colorDetails2;
				}
				return colorDetails2;
			}
			default:
				throw new Exception(MessageLocalization.GetComposedMessage("invalid.color.type"));
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00028C6F File Offset: 0x00027C6F
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00028C61 File Offset: 0x00027C61
		public bool StrictImageSequence
		{
			get
			{
				return this.pdf.StrictImageSequence;
			}
			set
			{
				this.pdf.StrictImageSequence = value;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00028C7C File Offset: 0x00027C7C
		public void ClearTextWrap()
		{
			this.pdf.ClearTextWrap();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00028C89 File Offset: 0x00027C89
		public PdfName AddDirectImageSimple(Image image)
		{
			return this.AddDirectImageSimple(image, null);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00028C94 File Offset: 0x00027C94
		public PdfName AddDirectImageSimple(Image image, PdfIndirectReference fixedRef)
		{
			PdfName pdfName;
			if (this.images.ContainsKey(image.MySerialId))
			{
				pdfName = this.images[image.MySerialId];
			}
			else
			{
				if (image.IsImgTemplate())
				{
					pdfName = new PdfName("img" + this.images.Count);
					if (image is ImgWMF)
					{
						ImgWMF imgWMF = (ImgWMF)image;
						imgWMF.ReadWMF(PdfTemplate.CreateTemplate(this, 0f, 0f));
					}
				}
				else
				{
					PdfIndirectReference directReference = image.DirectReference;
					if (directReference != null)
					{
						PdfName pdfName2 = new PdfName("img" + this.images.Count);
						this.images[image.MySerialId] = pdfName2;
						this.imageDictionary.Put(pdfName2, directReference);
						return pdfName2;
					}
					Image imageMask = image.ImageMask;
					PdfIndirectReference maskRef = null;
					if (imageMask != null)
					{
						PdfName name = this.images[imageMask.MySerialId];
						maskRef = this.GetImageReference(name);
					}
					PdfImage pdfImage = new PdfImage(image, "img" + this.images.Count, maskRef);
					if (image is ImgJBIG2)
					{
						byte[] globalBytes = ((ImgJBIG2)image).GlobalBytes;
						if (globalBytes != null)
						{
							PdfDictionary pdfDictionary = new PdfDictionary();
							pdfDictionary.Put(PdfName.JBIG2GLOBALS, this.GetReferenceJBIG2Globals(globalBytes));
							pdfImage.Put(PdfName.DECODEPARMS, pdfDictionary);
						}
					}
					if (image.HasICCProfile())
					{
						PdfICCBased icc = new PdfICCBased(image.TagICC, image.CompressionLevel);
						PdfIndirectReference obj = this.Add(icc);
						PdfArray pdfArray = new PdfArray();
						pdfArray.Add(PdfName.ICCBASED);
						pdfArray.Add(obj);
						PdfArray asArray = pdfImage.GetAsArray(PdfName.COLORSPACE);
						if (asArray != null)
						{
							if (asArray.Size > 1 && PdfName.INDEXED.Equals(asArray[0]))
							{
								asArray[1] = pdfArray;
							}
							else
							{
								pdfImage.Put(PdfName.COLORSPACE, pdfArray);
							}
						}
						else
						{
							pdfImage.Put(PdfName.COLORSPACE, pdfArray);
						}
					}
					this.Add(pdfImage, fixedRef);
					pdfName = pdfImage.Name;
				}
				this.images[image.MySerialId] = pdfName;
			}
			return pdfName;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00028EC4 File Offset: 0x00027EC4
		internal virtual PdfIndirectReference Add(PdfImage pdfImage, PdfIndirectReference fixedRef)
		{
			if (!this.imageDictionary.Contains(pdfImage.Name))
			{
				PdfXConformanceImp.CheckPDFXConformance(this, 5, pdfImage);
				if (fixedRef is PRIndirectReference)
				{
					PRIndirectReference prindirectReference = (PRIndirectReference)fixedRef;
					fixedRef = new PdfIndirectReference(0, this.GetNewObjectNumber(prindirectReference.Reader, prindirectReference.Number, prindirectReference.Generation));
				}
				if (fixedRef == null)
				{
					fixedRef = this.AddToBody(pdfImage).IndirectReference;
				}
				else
				{
					this.AddToBody(pdfImage, fixedRef);
				}
				this.imageDictionary.Put(pdfImage.Name, fixedRef);
				return fixedRef;
			}
			return (PdfIndirectReference)this.imageDictionary.Get(pdfImage.Name);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00028F61 File Offset: 0x00027F61
		internal virtual PdfIndirectReference GetImageReference(PdfName name)
		{
			return (PdfIndirectReference)this.imageDictionary.Get(name);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00028F74 File Offset: 0x00027F74
		protected virtual PdfIndirectReference Add(PdfICCBased icc)
		{
			PdfIndirectObject pdfIndirectObject = this.AddToBody(icc);
			return pdfIndirectObject.IndirectReference;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00028F90 File Offset: 0x00027F90
		protected internal PdfIndirectReference GetReferenceJBIG2Globals(byte[] content)
		{
			if (content == null)
			{
				return null;
			}
			foreach (PdfStream pdfStream in this.JBIG2Globals.Keys)
			{
				if (Arrays.AreEqual(content, pdfStream.GetBytes()))
				{
					return this.JBIG2Globals[pdfStream];
				}
			}
			PdfStream pdfStream2 = new PdfStream(content);
			PdfIndirectObject pdfIndirectObject;
			try
			{
				pdfIndirectObject = this.AddToBody(pdfStream2);
			}
			catch (IOException)
			{
				return null;
			}
			this.JBIG2Globals[pdfStream2] = pdfIndirectObject.IndirectReference;
			return pdfIndirectObject.IndirectReference;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00029049 File Offset: 0x00028049
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00029040 File Offset: 0x00028040
		public bool UserProperties
		{
			get
			{
				return this.userProperties;
			}
			set
			{
				this.userProperties = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00029051 File Offset: 0x00028051
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00029059 File Offset: 0x00028059
		public bool RgbTransparencyBlending
		{
			get
			{
				return this.rgbTransparencyBlending;
			}
			set
			{
				this.rgbTransparencyBlending = value;
			}
		}

		// Token: 0x0400062A RID: 1578
		public const int GENERATION_MAX = 65535;

		// Token: 0x0400062B RID: 1579
		public const char VERSION_1_2 = '2';

		// Token: 0x0400062C RID: 1580
		public const char VERSION_1_3 = '3';

		// Token: 0x0400062D RID: 1581
		public const char VERSION_1_4 = '4';

		// Token: 0x0400062E RID: 1582
		public const char VERSION_1_5 = '5';

		// Token: 0x0400062F RID: 1583
		public const char VERSION_1_6 = '6';

		// Token: 0x04000630 RID: 1584
		public const char VERSION_1_7 = '7';

		// Token: 0x04000631 RID: 1585
		public const int PageLayoutSinglePage = 1;

		// Token: 0x04000632 RID: 1586
		public const int PageLayoutOneColumn = 2;

		// Token: 0x04000633 RID: 1587
		public const int PageLayoutTwoColumnLeft = 4;

		// Token: 0x04000634 RID: 1588
		public const int PageLayoutTwoColumnRight = 8;

		// Token: 0x04000635 RID: 1589
		public const int PageLayoutTwoPageLeft = 16;

		// Token: 0x04000636 RID: 1590
		public const int PageLayoutTwoPageRight = 32;

		// Token: 0x04000637 RID: 1591
		public const int PageModeUseNone = 64;

		// Token: 0x04000638 RID: 1592
		public const int PageModeUseOutlines = 128;

		// Token: 0x04000639 RID: 1593
		public const int PageModeUseThumbs = 256;

		// Token: 0x0400063A RID: 1594
		public const int PageModeFullScreen = 512;

		// Token: 0x0400063B RID: 1595
		public const int PageModeUseOC = 1024;

		// Token: 0x0400063C RID: 1596
		public const int PageModeUseAttachments = 2048;

		// Token: 0x0400063D RID: 1597
		public const int HideToolbar = 4096;

		// Token: 0x0400063E RID: 1598
		public const int HideMenubar = 8192;

		// Token: 0x0400063F RID: 1599
		public const int HideWindowUI = 16384;

		// Token: 0x04000640 RID: 1600
		public const int FitWindow = 32768;

		// Token: 0x04000641 RID: 1601
		public const int CenterWindow = 65536;

		// Token: 0x04000642 RID: 1602
		public const int DisplayDocTitle = 131072;

		// Token: 0x04000643 RID: 1603
		public const int NonFullScreenPageModeUseNone = 262144;

		// Token: 0x04000644 RID: 1604
		public const int NonFullScreenPageModeUseOutlines = 524288;

		// Token: 0x04000645 RID: 1605
		public const int NonFullScreenPageModeUseThumbs = 1048576;

		// Token: 0x04000646 RID: 1606
		public const int NonFullScreenPageModeUseOC = 2097152;

		// Token: 0x04000647 RID: 1607
		public const int DirectionL2R = 4194304;

		// Token: 0x04000648 RID: 1608
		public const int DirectionR2L = 8388608;

		// Token: 0x04000649 RID: 1609
		public const int PrintScalingNone = 16777216;

		// Token: 0x0400064A RID: 1610
		public const int SIGNATURE_EXISTS = 1;

		// Token: 0x0400064B RID: 1611
		public const int SIGNATURE_APPEND_ONLY = 2;

		// Token: 0x0400064C RID: 1612
		public const int PDFXNONE = 0;

		// Token: 0x0400064D RID: 1613
		public const int PDFX1A2001 = 1;

		// Token: 0x0400064E RID: 1614
		public const int PDFX32002 = 2;

		// Token: 0x0400064F RID: 1615
		public const int PDFA1A = 3;

		// Token: 0x04000650 RID: 1616
		public const int PDFA1B = 4;

		// Token: 0x04000651 RID: 1617
		public const int STANDARD_ENCRYPTION_40 = 0;

		// Token: 0x04000652 RID: 1618
		public const int STANDARD_ENCRYPTION_128 = 1;

		// Token: 0x04000653 RID: 1619
		public const int ENCRYPTION_AES_128 = 2;

		// Token: 0x04000654 RID: 1620
		internal const int ENCRYPTION_MASK = 7;

		// Token: 0x04000655 RID: 1621
		public const int DO_NOT_ENCRYPT_METADATA = 8;

		// Token: 0x04000656 RID: 1622
		public const int EMBEDDED_FILES_ONLY = 24;

		// Token: 0x04000657 RID: 1623
		public const int ALLOW_PRINTING = 2052;

		// Token: 0x04000658 RID: 1624
		public const int ALLOW_MODIFY_CONTENTS = 8;

		// Token: 0x04000659 RID: 1625
		public const int ALLOW_COPY = 16;

		// Token: 0x0400065A RID: 1626
		public const int ALLOW_MODIFY_ANNOTATIONS = 32;

		// Token: 0x0400065B RID: 1627
		public const int ALLOW_FILL_IN = 256;

		// Token: 0x0400065C RID: 1628
		public const int ALLOW_SCREENREADERS = 512;

		// Token: 0x0400065D RID: 1629
		public const int ALLOW_ASSEMBLY = 1024;

		// Token: 0x0400065E RID: 1630
		public const int ALLOW_DEGRADED_PRINTING = 4;

		// Token: 0x0400065F RID: 1631
		public const int AllowPrinting = 2052;

		// Token: 0x04000660 RID: 1632
		public const int AllowModifyContents = 8;

		// Token: 0x04000661 RID: 1633
		public const int AllowCopy = 16;

		// Token: 0x04000662 RID: 1634
		public const int AllowModifyAnnotations = 32;

		// Token: 0x04000663 RID: 1635
		public const int AllowFillIn = 256;

		// Token: 0x04000664 RID: 1636
		public const int AllowScreenReaders = 512;

		// Token: 0x04000665 RID: 1637
		public const int AllowAssembly = 1024;

		// Token: 0x04000666 RID: 1638
		public const int AllowDegradedPrinting = 4;

		// Token: 0x04000667 RID: 1639
		public const bool STRENGTH40BITS = false;

		// Token: 0x04000668 RID: 1640
		public const bool STRENGTH128BITS = true;

		// Token: 0x04000669 RID: 1641
		public const float SPACE_CHAR_RATIO_DEFAULT = 2.5f;

		// Token: 0x0400066A RID: 1642
		public const float NO_SPACE_CHAR_RATIO = 10000000f;

		// Token: 0x0400066B RID: 1643
		public const int RUN_DIRECTION_DEFAULT = 0;

		// Token: 0x0400066C RID: 1644
		public const int RUN_DIRECTION_NO_BIDI = 1;

		// Token: 0x0400066D RID: 1645
		public const int RUN_DIRECTION_LTR = 2;

		// Token: 0x0400066E RID: 1646
		public const int RUN_DIRECTION_RTL = 3;

		// Token: 0x0400066F RID: 1647
		protected internal PdfDocument pdf;

		// Token: 0x04000670 RID: 1648
		protected PdfContentByte directContent;

		// Token: 0x04000671 RID: 1649
		protected PdfContentByte directContentUnder;

		// Token: 0x04000672 RID: 1650
		protected internal PdfWriter.PdfBody body;

		// Token: 0x04000673 RID: 1651
		protected internal PdfDictionary extraCatalog;

		// Token: 0x04000674 RID: 1652
		protected PdfPages root;

		// Token: 0x04000675 RID: 1653
		protected List<PdfIndirectReference> pageReferences = new List<PdfIndirectReference>();

		// Token: 0x04000676 RID: 1654
		protected int currentPageNumber = 1;

		// Token: 0x04000677 RID: 1655
		protected PdfName tabs;

		// Token: 0x04000678 RID: 1656
		private IPdfPageEvent pageEvent;

		// Token: 0x04000679 RID: 1657
		protected int prevxref;

		// Token: 0x0400067A RID: 1658
		protected IList<Dictionary<string, object>> newBookmarks;

		// Token: 0x0400067B RID: 1659
		public static readonly PdfName PDF_VERSION_1_2 = new PdfName("1.2");

		// Token: 0x0400067C RID: 1660
		public static readonly PdfName PDF_VERSION_1_3 = new PdfName("1.3");

		// Token: 0x0400067D RID: 1661
		public static readonly PdfName PDF_VERSION_1_4 = new PdfName("1.4");

		// Token: 0x0400067E RID: 1662
		public static readonly PdfName PDF_VERSION_1_5 = new PdfName("1.5");

		// Token: 0x0400067F RID: 1663
		public static readonly PdfName PDF_VERSION_1_6 = new PdfName("1.6");

		// Token: 0x04000680 RID: 1664
		public static readonly PdfName PDF_VERSION_1_7 = new PdfName("1.7");

		// Token: 0x04000681 RID: 1665
		protected PdfVersionImp pdf_version = new PdfVersionImp();

		// Token: 0x04000682 RID: 1666
		public static PdfName DOCUMENT_CLOSE = PdfName.WC;

		// Token: 0x04000683 RID: 1667
		public static PdfName WILL_SAVE = PdfName.WS;

		// Token: 0x04000684 RID: 1668
		public static PdfName DID_SAVE = PdfName.DS;

		// Token: 0x04000685 RID: 1669
		public static PdfName WILL_PRINT = PdfName.WP;

		// Token: 0x04000686 RID: 1670
		public static PdfName DID_PRINT = PdfName.DP;

		// Token: 0x04000687 RID: 1671
		protected byte[] xmpMetadata;

		// Token: 0x04000688 RID: 1672
		private PdfXConformanceImp pdfxConformance = new PdfXConformanceImp();

		// Token: 0x04000689 RID: 1673
		protected PdfEncryption crypto;

		// Token: 0x0400068A RID: 1674
		protected bool fullCompression;

		// Token: 0x0400068B RID: 1675
		protected internal int compressionLevel = -1;

		// Token: 0x0400068C RID: 1676
		protected Dictionary<BaseFont, FontDetails> documentFonts = new Dictionary<BaseFont, FontDetails>();

		// Token: 0x0400068D RID: 1677
		protected int fontNumber = 1;

		// Token: 0x0400068E RID: 1678
		protected Dictionary<PdfIndirectReference, object[]> formXObjects = new Dictionary<PdfIndirectReference, object[]>();

		// Token: 0x0400068F RID: 1679
		protected int formXObjectsCounter = 1;

		// Token: 0x04000690 RID: 1680
		protected Dictionary<PdfReader, PdfReaderInstance> readerInstances = new Dictionary<PdfReader, PdfReaderInstance>();

		// Token: 0x04000691 RID: 1681
		protected PdfReaderInstance currentPdfReaderInstance;

		// Token: 0x04000692 RID: 1682
		protected Dictionary<PdfSpotColor, ColorDetails> documentColors = new Dictionary<PdfSpotColor, ColorDetails>();

		// Token: 0x04000693 RID: 1683
		protected int colorNumber = 1;

		// Token: 0x04000694 RID: 1684
		protected Dictionary<PdfPatternPainter, PdfName> documentPatterns = new Dictionary<PdfPatternPainter, PdfName>();

		// Token: 0x04000695 RID: 1685
		protected int patternNumber = 1;

		// Token: 0x04000696 RID: 1686
		protected Dictionary<PdfShadingPattern, object> documentShadingPatterns = new Dictionary<PdfShadingPattern, object>();

		// Token: 0x04000697 RID: 1687
		protected Dictionary<PdfShading, object> documentShadings = new Dictionary<PdfShading, object>();

		// Token: 0x04000698 RID: 1688
		protected Dictionary<PdfDictionary, PdfObject[]> documentExtGState = new Dictionary<PdfDictionary, PdfObject[]>();

		// Token: 0x04000699 RID: 1689
		protected Dictionary<object, PdfObject[]> documentProperties = new Dictionary<object, PdfObject[]>();

		// Token: 0x0400069A RID: 1690
		protected bool tagged;

		// Token: 0x0400069B RID: 1691
		protected PdfStructureTreeRoot structureTreeRoot;

		// Token: 0x0400069C RID: 1692
		protected Dictionary<IPdfOCG, object> documentOCG = new Dictionary<IPdfOCG, object>();

		// Token: 0x0400069D RID: 1693
		protected List<IPdfOCG> documentOCGorder = new List<IPdfOCG>();

		// Token: 0x0400069E RID: 1694
		protected PdfOCProperties vOCProperties;

		// Token: 0x0400069F RID: 1695
		protected PdfArray OCGRadioGroup = new PdfArray();

		// Token: 0x040006A0 RID: 1696
		protected PdfArray OCGLocked = new PdfArray();

		// Token: 0x040006A1 RID: 1697
		public static readonly PdfName PAGE_OPEN = PdfName.O;

		// Token: 0x040006A2 RID: 1698
		public static readonly PdfName PAGE_CLOSE = PdfName.C;

		// Token: 0x040006A3 RID: 1699
		protected PdfDictionary group;

		// Token: 0x040006A4 RID: 1700
		private float spaceCharRatio = 2.5f;

		// Token: 0x040006A5 RID: 1701
		protected int runDirection = 1;

		// Token: 0x040006A6 RID: 1702
		protected float userunit;

		// Token: 0x040006A7 RID: 1703
		protected PdfDictionary defaultColorspace = new PdfDictionary();

		// Token: 0x040006A8 RID: 1704
		protected Dictionary<ColorDetails, ColorDetails> documentSpotPatterns = new Dictionary<ColorDetails, ColorDetails>();

		// Token: 0x040006A9 RID: 1705
		protected ColorDetails patternColorspaceRGB;

		// Token: 0x040006AA RID: 1706
		protected ColorDetails patternColorspaceGRAY;

		// Token: 0x040006AB RID: 1707
		protected ColorDetails patternColorspaceCMYK;

		// Token: 0x040006AC RID: 1708
		protected PdfDictionary imageDictionary = new PdfDictionary();

		// Token: 0x040006AD RID: 1709
		private Dictionary<long, PdfName> images = new Dictionary<long, PdfName>();

		// Token: 0x040006AE RID: 1710
		protected Dictionary<PdfStream, PdfIndirectReference> JBIG2Globals = new Dictionary<PdfStream, PdfIndirectReference>();

		// Token: 0x040006AF RID: 1711
		private bool userProperties;

		// Token: 0x040006B0 RID: 1712
		private bool rgbTransparencyBlending;

		// Token: 0x020000DB RID: 219
		public class PdfBody
		{
			// Token: 0x06000809 RID: 2057 RVA: 0x00029114 File Offset: 0x00028114
			internal PdfBody(PdfWriter writer)
			{
				this.xrefs = new OrderedTree();
				this.xrefs[new PdfWriter.PdfBody.PdfCrossReference(0, 0, 65535)] = null;
				this.position = writer.Os.Counter;
				this.refnum = 1;
				this.writer = writer;
			}

			// Token: 0x170001B6 RID: 438
			// (set) Token: 0x0600080A RID: 2058 RVA: 0x00029169 File Offset: 0x00028169
			internal int Refnum
			{
				set
				{
					this.refnum = value;
				}
			}

			// Token: 0x0600080B RID: 2059 RVA: 0x00029174 File Offset: 0x00028174
			private PdfWriter.PdfBody.PdfCrossReference AddToObjStm(PdfObject obj, int nObj)
			{
				if (this.numObj >= 200)
				{
					this.FlushObjStm();
				}
				if (this.index == null)
				{
					this.index = new ByteBuffer();
					this.streamObjects = new ByteBuffer();
					this.currentObjNum = this.IndirectReferenceNumber;
					this.numObj = 0;
				}
				int size = this.streamObjects.Size;
				int generation = this.numObj++;
				PdfEncryption crypto = this.writer.crypto;
				this.writer.crypto = null;
				obj.ToPdf(this.writer, this.streamObjects);
				this.writer.crypto = crypto;
				this.streamObjects.Append(' ');
				this.index.Append(nObj).Append(' ').Append(size).Append(' ');
				return new PdfWriter.PdfBody.PdfCrossReference(2, nObj, this.currentObjNum, generation);
			}

			// Token: 0x0600080C RID: 2060 RVA: 0x00029258 File Offset: 0x00028258
			internal void FlushObjStm()
			{
				if (this.numObj == 0)
				{
					return;
				}
				int size = this.index.Size;
				this.index.Append(this.streamObjects);
				PdfStream pdfStream = new PdfStream(this.index.ToByteArray());
				pdfStream.FlateCompress(this.writer.CompressionLevel);
				pdfStream.Put(PdfName.TYPE, PdfName.OBJSTM);
				pdfStream.Put(PdfName.N, new PdfNumber(this.numObj));
				pdfStream.Put(PdfName.FIRST, new PdfNumber(size));
				this.Add(pdfStream, this.currentObjNum);
				this.index = null;
				this.streamObjects = null;
				this.numObj = 0;
			}

			// Token: 0x0600080D RID: 2061 RVA: 0x00029308 File Offset: 0x00028308
			internal PdfIndirectObject Add(PdfObject objecta)
			{
				return this.Add(objecta, this.IndirectReferenceNumber);
			}

			// Token: 0x0600080E RID: 2062 RVA: 0x00029317 File Offset: 0x00028317
			internal PdfIndirectObject Add(PdfObject objecta, bool inObjStm)
			{
				return this.Add(objecta, this.IndirectReferenceNumber, inObjStm);
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x0600080F RID: 2063 RVA: 0x00029327 File Offset: 0x00028327
			internal PdfIndirectReference PdfIndirectReference
			{
				get
				{
					return new PdfIndirectReference(0, this.IndirectReferenceNumber);
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x06000810 RID: 2064 RVA: 0x00029338 File Offset: 0x00028338
			internal int IndirectReferenceNumber
			{
				get
				{
					int result = this.refnum++;
					this.xrefs[new PdfWriter.PdfBody.PdfCrossReference(result, 0, 65535)] = null;
					return result;
				}
			}

			// Token: 0x06000811 RID: 2065 RVA: 0x00029370 File Offset: 0x00028370
			internal PdfIndirectObject Add(PdfObject objecta, PdfIndirectReference refa)
			{
				return this.Add(objecta, refa.Number);
			}

			// Token: 0x06000812 RID: 2066 RVA: 0x0002937F File Offset: 0x0002837F
			internal PdfIndirectObject Add(PdfObject objecta, PdfIndirectReference refa, bool inObjStm)
			{
				return this.Add(objecta, refa.Number, inObjStm);
			}

			// Token: 0x06000813 RID: 2067 RVA: 0x0002938F File Offset: 0x0002838F
			internal PdfIndirectObject Add(PdfObject objecta, int refNumber)
			{
				return this.Add(objecta, refNumber, true);
			}

			// Token: 0x06000814 RID: 2068 RVA: 0x0002939C File Offset: 0x0002839C
			internal PdfIndirectObject Add(PdfObject objecta, int refNumber, bool inObjStm)
			{
				if (inObjStm && objecta.CanBeInObjStm() && this.writer.FullCompression)
				{
					PdfWriter.PdfBody.PdfCrossReference key = this.AddToObjStm(objecta, refNumber);
					PdfIndirectObject result = new PdfIndirectObject(refNumber, objecta, this.writer);
					this.xrefs.Remove(key);
					this.xrefs[key] = null;
					return result;
				}
				PdfIndirectObject pdfIndirectObject = new PdfIndirectObject(refNumber, objecta, this.writer);
				PdfWriter.PdfBody.PdfCrossReference key2 = new PdfWriter.PdfBody.PdfCrossReference(refNumber, this.position);
				this.xrefs.Remove(key2);
				this.xrefs[key2] = null;
				pdfIndirectObject.WriteTo(this.writer.Os);
				this.position = this.writer.Os.Counter;
				return pdfIndirectObject;
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06000815 RID: 2069 RVA: 0x0002944F File Offset: 0x0002844F
			internal int Offset
			{
				get
				{
					return this.position;
				}
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000816 RID: 2070 RVA: 0x00029457 File Offset: 0x00028457
			internal int Size
			{
				get
				{
					return Math.Max(((PdfWriter.PdfBody.PdfCrossReference)this.xrefs.GetMaxKey()).Refnum + 1, this.refnum);
				}
			}

			// Token: 0x06000817 RID: 2071 RVA: 0x0002947C File Offset: 0x0002847C
			internal void WriteCrossReferenceTable(Stream os, PdfIndirectReference root, PdfIndirectReference info, PdfIndirectReference encryption, PdfObject fileID, int prevxref)
			{
				int number = 0;
				if (this.writer.FullCompression)
				{
					this.FlushObjStm();
					number = this.IndirectReferenceNumber;
					this.xrefs[new PdfWriter.PdfBody.PdfCrossReference(number, this.position)] = null;
				}
				int num = ((PdfWriter.PdfBody.PdfCrossReference)this.xrefs.GetMinKey()).Refnum;
				int num2 = 0;
				List<int> list = new List<int>();
				foreach (object obj in this.xrefs.Keys)
				{
					PdfWriter.PdfBody.PdfCrossReference pdfCrossReference = (PdfWriter.PdfBody.PdfCrossReference)obj;
					if (num + num2 == pdfCrossReference.Refnum)
					{
						num2++;
					}
					else
					{
						list.Add(num);
						list.Add(num2);
						num = pdfCrossReference.Refnum;
						num2 = 1;
					}
				}
				list.Add(num);
				list.Add(num2);
				if (this.writer.FullCompression)
				{
					int num3 = 4;
					uint num4 = 4278190080U;
					while (num3 > 1 && ((ulong)num4 & (ulong)((long)this.position)) == 0UL)
					{
						num4 >>= 8;
						num3--;
					}
					ByteBuffer byteBuffer = new ByteBuffer();
					foreach (object obj2 in this.xrefs.Keys)
					{
						PdfWriter.PdfBody.PdfCrossReference pdfCrossReference2 = (PdfWriter.PdfBody.PdfCrossReference)obj2;
						pdfCrossReference2.ToPdf(num3, byteBuffer);
					}
					PdfStream pdfStream = new PdfStream(byteBuffer.ToByteArray());
					byteBuffer = null;
					pdfStream.FlateCompress(this.writer.CompressionLevel);
					pdfStream.Put(PdfName.SIZE, new PdfNumber(this.Size));
					pdfStream.Put(PdfName.ROOT, root);
					if (info != null)
					{
						pdfStream.Put(PdfName.INFO, info);
					}
					if (encryption != null)
					{
						pdfStream.Put(PdfName.ENCRYPT, encryption);
					}
					if (fileID != null)
					{
						pdfStream.Put(PdfName.ID, fileID);
					}
					pdfStream.Put(PdfName.W, new PdfArray(new int[]
					{
						1,
						num3,
						2
					}));
					pdfStream.Put(PdfName.TYPE, PdfName.XREF);
					PdfArray pdfArray = new PdfArray();
					for (int i = 0; i < list.Count; i++)
					{
						pdfArray.Add(new PdfNumber(list[i]));
					}
					pdfStream.Put(PdfName.INDEX, pdfArray);
					if (prevxref > 0)
					{
						pdfStream.Put(PdfName.PREV, new PdfNumber(prevxref));
					}
					PdfEncryption crypto = this.writer.crypto;
					this.writer.crypto = null;
					PdfIndirectObject pdfIndirectObject = new PdfIndirectObject(number, pdfStream, this.writer);
					pdfIndirectObject.WriteTo(this.writer.Os);
					this.writer.crypto = crypto;
					return;
				}
				byte[] isobytes = DocWriter.GetISOBytes("xref\n");
				os.Write(isobytes, 0, isobytes.Length);
				IEnumerator keys = this.xrefs.Keys;
				keys.MoveNext();
				for (int j = 0; j < list.Count; j += 2)
				{
					num = list[j];
					num2 = list[j + 1];
					isobytes = DocWriter.GetISOBytes(num.ToString());
					os.Write(isobytes, 0, isobytes.Length);
					os.WriteByte(32);
					isobytes = DocWriter.GetISOBytes(num2.ToString());
					os.Write(isobytes, 0, isobytes.Length);
					os.WriteByte(10);
					while (num2-- > 0)
					{
						((PdfWriter.PdfBody.PdfCrossReference)keys.Current).ToPdf(os);
						keys.MoveNext();
					}
				}
			}

			// Token: 0x040006B1 RID: 1713
			private const int OBJSINSTREAM = 200;

			// Token: 0x040006B2 RID: 1714
			private OrderedTree xrefs;

			// Token: 0x040006B3 RID: 1715
			private int refnum;

			// Token: 0x040006B4 RID: 1716
			private int position;

			// Token: 0x040006B5 RID: 1717
			private PdfWriter writer;

			// Token: 0x040006B6 RID: 1718
			private ByteBuffer index;

			// Token: 0x040006B7 RID: 1719
			private ByteBuffer streamObjects;

			// Token: 0x040006B8 RID: 1720
			private int currentObjNum;

			// Token: 0x040006B9 RID: 1721
			private int numObj;

			// Token: 0x020000DC RID: 220
			internal class PdfCrossReference : IComparable
			{
				// Token: 0x06000818 RID: 2072 RVA: 0x00029820 File Offset: 0x00028820
				internal PdfCrossReference(int refnum, int offset, int generation)
				{
					this.type = 0;
					this.offset = offset;
					this.refnum = refnum;
					this.generation = generation;
				}

				// Token: 0x06000819 RID: 2073 RVA: 0x00029844 File Offset: 0x00028844
				internal PdfCrossReference(int refnum, int offset)
				{
					this.type = 1;
					this.offset = offset;
					this.refnum = refnum;
					this.generation = 0;
				}

				// Token: 0x0600081A RID: 2074 RVA: 0x00029868 File Offset: 0x00028868
				internal PdfCrossReference(int type, int refnum, int offset, int generation)
				{
					this.type = type;
					this.offset = offset;
					this.refnum = refnum;
					this.generation = generation;
				}

				// Token: 0x170001BB RID: 443
				// (get) Token: 0x0600081B RID: 2075 RVA: 0x0002988D File Offset: 0x0002888D
				internal int Refnum
				{
					get
					{
						return this.refnum;
					}
				}

				// Token: 0x0600081C RID: 2076 RVA: 0x00029898 File Offset: 0x00028898
				public void ToPdf(Stream os)
				{
					string str = this.offset.ToString().PadLeft(10, '0');
					string str2 = this.generation.ToString().PadLeft(5, '0');
					ByteBuffer byteBuffer = new ByteBuffer(40);
					if (this.generation == 65535)
					{
						byteBuffer.Append(str).Append(' ').Append(str2).Append(" f \n");
					}
					else
					{
						byteBuffer.Append(str).Append(' ').Append(str2).Append(" n \n");
					}
					os.Write(byteBuffer.Buffer, 0, byteBuffer.Size);
				}

				// Token: 0x0600081D RID: 2077 RVA: 0x00029938 File Offset: 0x00028938
				public void ToPdf(int midSize, Stream os)
				{
					os.WriteByte((byte)this.type);
					while (--midSize >= 0)
					{
						os.WriteByte((byte)(this.offset >> 8 * midSize & 255));
					}
					os.WriteByte((byte)(this.generation >> 8 & 255));
					os.WriteByte((byte)(this.generation & 255));
				}

				// Token: 0x0600081E RID: 2078 RVA: 0x000299A0 File Offset: 0x000289A0
				public int CompareTo(object o)
				{
					PdfWriter.PdfBody.PdfCrossReference pdfCrossReference = (PdfWriter.PdfBody.PdfCrossReference)o;
					if (this.refnum < pdfCrossReference.refnum)
					{
						return -1;
					}
					if (this.refnum != pdfCrossReference.refnum)
					{
						return 1;
					}
					return 0;
				}

				// Token: 0x0600081F RID: 2079 RVA: 0x000299D8 File Offset: 0x000289D8
				public override bool Equals(object obj)
				{
					if (obj is PdfWriter.PdfBody.PdfCrossReference)
					{
						PdfWriter.PdfBody.PdfCrossReference pdfCrossReference = (PdfWriter.PdfBody.PdfCrossReference)obj;
						return this.refnum == pdfCrossReference.refnum;
					}
					return false;
				}

				// Token: 0x06000820 RID: 2080 RVA: 0x00029A04 File Offset: 0x00028A04
				public override int GetHashCode()
				{
					return this.refnum;
				}

				// Token: 0x040006BA RID: 1722
				private int type;

				// Token: 0x040006BB RID: 1723
				private int offset;

				// Token: 0x040006BC RID: 1724
				private int refnum;

				// Token: 0x040006BD RID: 1725
				private int generation;
			}
		}

		// Token: 0x020000DD RID: 221
		internal class PdfTrailer : PdfDictionary
		{
			// Token: 0x06000821 RID: 2081 RVA: 0x00029A0C File Offset: 0x00028A0C
			internal PdfTrailer(int size, int offset, PdfIndirectReference root, PdfIndirectReference info, PdfIndirectReference encryption, PdfObject fileID, int prevxref)
			{
				this.offset = offset;
				base.Put(PdfName.SIZE, new PdfNumber(size));
				base.Put(PdfName.ROOT, root);
				if (info != null)
				{
					base.Put(PdfName.INFO, info);
				}
				if (encryption != null)
				{
					base.Put(PdfName.ENCRYPT, encryption);
				}
				if (fileID != null)
				{
					base.Put(PdfName.ID, fileID);
				}
				if (prevxref > 0)
				{
					base.Put(PdfName.PREV, new PdfNumber(prevxref));
				}
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x00029A90 File Offset: 0x00028A90
			public override void ToPdf(PdfWriter writer, Stream os)
			{
				byte[] isobytes = DocWriter.GetISOBytes("trailer\n");
				os.Write(isobytes, 0, isobytes.Length);
				base.ToPdf(null, os);
				isobytes = DocWriter.GetISOBytes("\nstartxref\n");
				os.Write(isobytes, 0, isobytes.Length);
				isobytes = DocWriter.GetISOBytes(this.offset.ToString());
				os.Write(isobytes, 0, isobytes.Length);
				isobytes = DocWriter.GetISOBytes("\n%%EOF\n");
				os.Write(isobytes, 0, isobytes.Length);
			}

			// Token: 0x040006BE RID: 1726
			internal int offset;
		}
	}
}
