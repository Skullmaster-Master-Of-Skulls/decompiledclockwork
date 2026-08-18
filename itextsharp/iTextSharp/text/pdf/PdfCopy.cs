using System;
using System.Collections.Generic;
using System.IO;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000278 RID: 632
	public class PdfCopy : PdfWriter
	{
		// Token: 0x06001806 RID: 6150 RVA: 0x0008AFD8 File Offset: 0x00089FD8
		public PdfCopy(Document document, Stream os)
		{
			int[] array = new int[1];
			this.namePtr = array;
			this.rotateContents = true;
			base..ctor(new PdfDocument(), os);
			document.AddDocListener(this.pdf);
			this.pdf.AddWriter(this);
			this.indirectMap = new Dictionary<PdfReader, Dictionary<PdfCopy.RefKey, PdfCopy.IndirectReferences>>();
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0008B039 File Offset: 0x0008A039
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x0008B030 File Offset: 0x0008A030
		public bool RotateContents
		{
			get
			{
				return this.rotateContents;
			}
			set
			{
				this.rotateContents = value;
			}
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0008B044 File Offset: 0x0008A044
		public override PdfImportedPage GetImportedPage(PdfReader reader, int pageNumber)
		{
			if (this.currentPdfReaderInstance != null)
			{
				if (this.currentPdfReaderInstance.Reader != reader)
				{
					try
					{
						this.currentPdfReaderInstance.Reader.Close();
						this.currentPdfReaderInstance.ReaderFile.Close();
					}
					catch (IOException)
					{
					}
					this.currentPdfReaderInstance = base.GetPdfReaderInstance(reader);
				}
			}
			else
			{
				this.currentPdfReaderInstance = base.GetPdfReaderInstance(reader);
			}
			return this.currentPdfReaderInstance.GetImportedPage(pageNumber);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0008B0C4 File Offset: 0x0008A0C4
		protected virtual PdfIndirectReference CopyIndirect(PRIndirectReference inp)
		{
			PdfCopy.RefKey key = new PdfCopy.RefKey(inp);
			PdfCopy.IndirectReferences indirectReferences;
			this.indirects.TryGetValue(key, out indirectReferences);
			PdfIndirectReference pdfIndirectReference;
			if (indirectReferences != null)
			{
				pdfIndirectReference = indirectReferences.Ref;
				if (indirectReferences.Copied)
				{
					return pdfIndirectReference;
				}
			}
			else
			{
				pdfIndirectReference = this.body.PdfIndirectReference;
				indirectReferences = new PdfCopy.IndirectReferences(pdfIndirectReference);
				this.indirects[key] = indirectReferences;
			}
			PdfObject pdfObject = PdfReader.GetPdfObjectRelease(inp);
			if (pdfObject != null && pdfObject.IsDictionary())
			{
				PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(((PdfDictionary)pdfObject).Get(PdfName.TYPE));
				if (pdfObjectRelease != null && PdfName.PAGE.Equals(pdfObjectRelease))
				{
					return pdfIndirectReference;
				}
			}
			indirectReferences.SetCopied();
			pdfObject = this.CopyObject(pdfObject);
			base.AddToBody(pdfObject, pdfIndirectReference);
			return pdfIndirectReference;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0008B170 File Offset: 0x0008A170
		protected PdfDictionary CopyDictionary(PdfDictionary inp)
		{
			PdfDictionary pdfDictionary = new PdfDictionary();
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(inp.Get(PdfName.TYPE));
			foreach (PdfName pdfName in inp.Keys)
			{
				PdfObject inp2 = inp.Get(pdfName);
				if (pdfObjectRelease != null && PdfName.PAGE.Equals(pdfObjectRelease))
				{
					if (!pdfName.Equals(PdfName.B) && !pdfName.Equals(PdfName.PARENT))
					{
						pdfDictionary.Put(pdfName, this.CopyObject(inp2));
					}
				}
				else
				{
					pdfDictionary.Put(pdfName, this.CopyObject(inp2));
				}
			}
			return pdfDictionary;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0008B228 File Offset: 0x0008A228
		protected PdfStream CopyStream(PRStream inp)
		{
			PRStream prstream = new PRStream(inp, null);
			foreach (PdfName key in inp.Keys)
			{
				PdfObject inp2 = inp.Get(key);
				prstream.Put(key, this.CopyObject(inp2));
			}
			return prstream;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0008B294 File Offset: 0x0008A294
		protected PdfArray CopyArray(PdfArray inp)
		{
			PdfArray pdfArray = new PdfArray();
			foreach (PdfObject inp2 in inp.ArrayList)
			{
				pdfArray.Add(this.CopyObject(inp2));
			}
			return pdfArray;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0008B2F8 File Offset: 0x0008A2F8
		protected PdfObject CopyObject(PdfObject inp)
		{
			if (inp == null)
			{
				return PdfNull.PDFNULL;
			}
			switch (inp.Type)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 8:
				return inp;
			case 5:
				return this.CopyArray((PdfArray)inp);
			case 6:
				return this.CopyDictionary((PdfDictionary)inp);
			case 7:
				return this.CopyStream((PRStream)inp);
			case 10:
				return this.CopyIndirect((PRIndirectReference)inp);
			}
			if (inp.Type >= 0)
			{
				return null;
			}
			string text = ((PdfLiteral)inp).ToString();
			if (text.Equals("true") || text.Equals("false"))
			{
				return new PdfBoolean(text);
			}
			return new PdfLiteral(text);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0008B3C0 File Offset: 0x0008A3C0
		protected int SetFromIPage(PdfImportedPage iPage)
		{
			int pageNumber = iPage.PageNumber;
			PdfReaderInstance pdfReaderInstance = this.currentPdfReaderInstance = iPage.PdfReaderInstance;
			this.reader = pdfReaderInstance.Reader;
			this.SetFromReader(this.reader);
			return pageNumber;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0008B400 File Offset: 0x0008A400
		protected void SetFromReader(PdfReader reader)
		{
			this.reader = reader;
			this.indirectMap.TryGetValue(reader, out this.indirects);
			if (this.indirects == null)
			{
				this.indirects = new Dictionary<PdfCopy.RefKey, PdfCopy.IndirectReferences>();
				this.indirectMap[reader] = this.indirects;
				PdfDictionary catalog = reader.Catalog;
				PdfObject pdfObject = catalog.Get(PdfName.ACROFORM);
				if (pdfObject == null || pdfObject.Type != 10)
				{
					return;
				}
				PRIndirectReference refi = (PRIndirectReference)pdfObject;
				if (this.acroForm == null)
				{
					this.acroForm = this.body.PdfIndirectReference;
				}
				this.indirects[new PdfCopy.RefKey(refi)] = new PdfCopy.IndirectReferences(this.acroForm);
			}
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0008B4AC File Offset: 0x0008A4AC
		public void AddPage(PdfImportedPage iPage)
		{
			int pageNum = this.SetFromIPage(iPage);
			PdfDictionary pageN = this.reader.GetPageN(pageNum);
			PRIndirectReference pageOrigRef = this.reader.GetPageOrigRef(pageNum);
			this.reader.ReleasePage(pageNum);
			PdfCopy.RefKey key = new PdfCopy.RefKey(pageOrigRef);
			PdfCopy.IndirectReferences indirectReferences;
			this.indirects.TryGetValue(key, out indirectReferences);
			if (indirectReferences != null && !indirectReferences.Copied)
			{
				this.pageReferences.Add(indirectReferences.Ref);
				indirectReferences.SetCopied();
			}
			PdfIndirectReference currentPage = this.CurrentPage;
			if (indirectReferences == null)
			{
				indirectReferences = new PdfCopy.IndirectReferences(currentPage);
				this.indirects[key] = indirectReferences;
			}
			indirectReferences.SetCopied();
			PdfDictionary page = this.CopyDictionary(pageN);
			this.root.AddPage(page);
			this.currentPageNumber++;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0008B574 File Offset: 0x0008A574
		public void AddPage(Rectangle rect, int rotation)
		{
			PdfRectangle mediaBox = new PdfRectangle(rect, rotation);
			PageResources pageResources = new PageResources();
			PdfPage pdfPage = new PdfPage(mediaBox, new Dictionary<string, PdfRectangle>(), pageResources.Resources, 0);
			pdfPage.Put(PdfName.TABS, base.Tabs);
			this.root.AddPage(pdfPage);
			this.currentPageNumber++;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0008B5D0 File Offset: 0x0008A5D0
		public void CopyAcroForm(PdfReader reader)
		{
			this.SetFromReader(reader);
			PdfDictionary catalog = reader.Catalog;
			PRIndirectReference prindirectReference = null;
			PdfObject pdfObject = catalog.Get(PdfName.ACROFORM);
			if (pdfObject != null && pdfObject.Type == 10)
			{
				prindirectReference = (PRIndirectReference)pdfObject;
			}
			if (prindirectReference == null)
			{
				return;
			}
			PdfCopy.RefKey key = new PdfCopy.RefKey(prindirectReference);
			PdfCopy.IndirectReferences indirectReferences;
			this.indirects.TryGetValue(key, out indirectReferences);
			PdfIndirectReference pdfIndirectReference;
			if (indirectReferences != null)
			{
				pdfIndirectReference = (this.acroForm = indirectReferences.Ref);
			}
			else
			{
				pdfIndirectReference = (this.acroForm = this.body.PdfIndirectReference);
				indirectReferences = new PdfCopy.IndirectReferences(pdfIndirectReference);
				this.indirects[key] = indirectReferences;
			}
			if (!indirectReferences.Copied)
			{
				indirectReferences.SetCopied();
				PdfDictionary objecta = this.CopyDictionary((PdfDictionary)PdfReader.GetPdfObject(prindirectReference));
				base.AddToBody(objecta, pdfIndirectReference);
			}
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0008B698 File Offset: 0x0008A698
		protected override PdfDictionary GetCatalog(PdfIndirectReference rootObj)
		{
			PdfDictionary catalog = this.pdf.GetCatalog(rootObj);
			if (this.fieldArray == null)
			{
				if (this.acroForm != null)
				{
					catalog.Put(PdfName.ACROFORM, this.acroForm);
				}
			}
			else
			{
				this.AddFieldResources(catalog);
			}
			return catalog;
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0008B6E0 File Offset: 0x0008A6E0
		private void AddFieldResources(PdfDictionary catalog)
		{
			if (this.fieldArray == null)
			{
				return;
			}
			PdfDictionary pdfDictionary = new PdfDictionary();
			catalog.Put(PdfName.ACROFORM, pdfDictionary);
			pdfDictionary.Put(PdfName.FIELDS, this.fieldArray);
			pdfDictionary.Put(PdfName.DA, new PdfString("/Helv 0 Tf 0 g "));
			if (this.fieldTemplates.Count == 0)
			{
				return;
			}
			PdfDictionary pdfDictionary2 = new PdfDictionary();
			pdfDictionary.Put(PdfName.DR, pdfDictionary2);
			foreach (PdfTemplate pdfTemplate in this.fieldTemplates.Keys)
			{
				PdfFormField.MergeResources(pdfDictionary2, (PdfDictionary)pdfTemplate.Resources);
			}
			PdfDictionary pdfDictionary3 = pdfDictionary2.GetAsDict(PdfName.FONT);
			if (pdfDictionary3 == null)
			{
				pdfDictionary3 = new PdfDictionary();
				pdfDictionary2.Put(PdfName.FONT, pdfDictionary3);
			}
			if (!pdfDictionary3.Contains(PdfName.HELV))
			{
				PdfDictionary pdfDictionary4 = new PdfDictionary(PdfName.FONT);
				pdfDictionary4.Put(PdfName.BASEFONT, PdfName.HELVETICA);
				pdfDictionary4.Put(PdfName.ENCODING, PdfName.WIN_ANSI_ENCODING);
				pdfDictionary4.Put(PdfName.NAME, PdfName.HELV);
				pdfDictionary4.Put(PdfName.SUBTYPE, PdfName.TYPE1);
				pdfDictionary3.Put(PdfName.HELV, base.AddToBody(pdfDictionary4).IndirectReference);
			}
			if (!pdfDictionary3.Contains(PdfName.ZADB))
			{
				PdfDictionary pdfDictionary5 = new PdfDictionary(PdfName.FONT);
				pdfDictionary5.Put(PdfName.BASEFONT, PdfName.ZAPFDINGBATS);
				pdfDictionary5.Put(PdfName.NAME, PdfName.ZADB);
				pdfDictionary5.Put(PdfName.SUBTYPE, PdfName.TYPE1);
				pdfDictionary3.Put(PdfName.ZADB, base.AddToBody(pdfDictionary5).IndirectReference);
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0008B8A0 File Offset: 0x0008A8A0
		public override void Close()
		{
			if (this.open)
			{
				PdfReaderInstance currentPdfReaderInstance = this.currentPdfReaderInstance;
				this.pdf.Close();
				base.Close();
				if (currentPdfReaderInstance != null)
				{
					try
					{
						currentPdfReaderInstance.Reader.Close();
						currentPdfReaderInstance.ReaderFile.Close();
					}
					catch (IOException)
					{
					}
				}
			}
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0008B8FC File Offset: 0x0008A8FC
		public override void AddAnnotation(PdfAnnotation annot)
		{
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0008B8FE File Offset: 0x0008A8FE
		internal override PdfIndirectReference Add(PdfPage page, PdfContents contents)
		{
			return null;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0008B904 File Offset: 0x0008A904
		public override void FreeReader(PdfReader reader)
		{
			this.indirectMap.Remove(reader);
			if (this.currentPdfReaderInstance != null && this.currentPdfReaderInstance.Reader == reader)
			{
				try
				{
					this.currentPdfReaderInstance.Reader.Close();
					this.currentPdfReaderInstance.ReaderFile.Close();
				}
				catch (IOException)
				{
				}
				this.currentPdfReaderInstance = null;
			}
			base.FreeReader(reader);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0008B978 File Offset: 0x0008A978
		public PdfCopy.PageStamp CreatePageStamp(PdfImportedPage iPage)
		{
			int pageNumber = iPage.PageNumber;
			PdfReader pdfReader = iPage.PdfReaderInstance.Reader;
			PdfDictionary pageN = pdfReader.GetPageN(pageNumber);
			return new PdfCopy.PageStamp(pdfReader, pageN, this);
		}

		// Token: 0x0400103D RID: 4157
		protected Dictionary<PdfCopy.RefKey, PdfCopy.IndirectReferences> indirects;

		// Token: 0x0400103E RID: 4158
		protected Dictionary<PdfReader, Dictionary<PdfCopy.RefKey, PdfCopy.IndirectReferences>> indirectMap;

		// Token: 0x0400103F RID: 4159
		protected int currentObjectNum = 1;

		// Token: 0x04001040 RID: 4160
		protected PdfReader reader;

		// Token: 0x04001041 RID: 4161
		protected PdfIndirectReference acroForm;

		// Token: 0x04001042 RID: 4162
		protected int[] namePtr;

		// Token: 0x04001043 RID: 4163
		private bool rotateContents;

		// Token: 0x04001044 RID: 4164
		protected internal PdfArray fieldArray;

		// Token: 0x04001045 RID: 4165
		protected internal Dictionary<PdfTemplate, object> fieldTemplates;

		// Token: 0x02000279 RID: 633
		public class IndirectReferences
		{
			// Token: 0x0600181B RID: 6171 RVA: 0x0008B9A8 File Offset: 0x0008A9A8
			internal IndirectReferences(PdfIndirectReference refi)
			{
				this.theRef = refi;
				this.hasCopied = false;
			}

			// Token: 0x0600181C RID: 6172 RVA: 0x0008B9BE File Offset: 0x0008A9BE
			internal void SetCopied()
			{
				this.hasCopied = true;
			}

			// Token: 0x17000468 RID: 1128
			// (get) Token: 0x0600181D RID: 6173 RVA: 0x0008B9C7 File Offset: 0x0008A9C7
			internal bool Copied
			{
				get
				{
					return this.hasCopied;
				}
			}

			// Token: 0x17000469 RID: 1129
			// (get) Token: 0x0600181E RID: 6174 RVA: 0x0008B9CF File Offset: 0x0008A9CF
			internal PdfIndirectReference Ref
			{
				get
				{
					return this.theRef;
				}
			}

			// Token: 0x04001046 RID: 4166
			private PdfIndirectReference theRef;

			// Token: 0x04001047 RID: 4167
			private bool hasCopied;
		}

		// Token: 0x0200027A RID: 634
		public class RefKey
		{
			// Token: 0x0600181F RID: 6175 RVA: 0x0008B9D7 File Offset: 0x0008A9D7
			internal RefKey(int num, int gen)
			{
				this.num = num;
				this.gen = gen;
			}

			// Token: 0x06001820 RID: 6176 RVA: 0x0008B9ED File Offset: 0x0008A9ED
			internal RefKey(PdfIndirectReference refi)
			{
				this.num = refi.Number;
				this.gen = refi.Generation;
			}

			// Token: 0x06001821 RID: 6177 RVA: 0x0008BA0D File Offset: 0x0008AA0D
			internal RefKey(PRIndirectReference refi)
			{
				this.num = refi.Number;
				this.gen = refi.Generation;
			}

			// Token: 0x06001822 RID: 6178 RVA: 0x0008BA2D File Offset: 0x0008AA2D
			public override int GetHashCode()
			{
				return (this.gen << 16) + this.num;
			}

			// Token: 0x06001823 RID: 6179 RVA: 0x0008BA40 File Offset: 0x0008AA40
			public override bool Equals(object o)
			{
				if (!(o is PdfCopy.RefKey))
				{
					return false;
				}
				PdfCopy.RefKey refKey = (PdfCopy.RefKey)o;
				return this.gen == refKey.gen && this.num == refKey.num;
			}

			// Token: 0x06001824 RID: 6180 RVA: 0x0008BA7C File Offset: 0x0008AA7C
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					"",
					this.num,
					" ",
					this.gen
				});
			}

			// Token: 0x04001048 RID: 4168
			internal int num;

			// Token: 0x04001049 RID: 4169
			internal int gen;
		}

		// Token: 0x0200027B RID: 635
		public class PageStamp
		{
			// Token: 0x06001825 RID: 6181 RVA: 0x0008BAC2 File Offset: 0x0008AAC2
			internal PageStamp(PdfReader reader, PdfDictionary pageN, PdfCopy cstp)
			{
				this.pageN = pageN;
				this.reader = reader;
				this.cstp = cstp;
			}

			// Token: 0x06001826 RID: 6182 RVA: 0x0008BAE0 File Offset: 0x0008AAE0
			public PdfContentByte GetUnderContent()
			{
				if (this.under == null)
				{
					if (this.pageResources == null)
					{
						this.pageResources = new PageResources();
						PdfDictionary asDict = this.pageN.GetAsDict(PdfName.RESOURCES);
						this.pageResources.SetOriginalResources(asDict, this.cstp.namePtr);
					}
					this.under = new PdfCopy.StampContent(this.cstp, this.pageResources);
				}
				return this.under;
			}

			// Token: 0x06001827 RID: 6183 RVA: 0x0008BB50 File Offset: 0x0008AB50
			public PdfContentByte GetOverContent()
			{
				if (this.over == null)
				{
					if (this.pageResources == null)
					{
						this.pageResources = new PageResources();
						PdfDictionary asDict = this.pageN.GetAsDict(PdfName.RESOURCES);
						this.pageResources.SetOriginalResources(asDict, this.cstp.namePtr);
					}
					this.over = new PdfCopy.StampContent(this.cstp, this.pageResources);
				}
				return this.over;
			}

			// Token: 0x06001828 RID: 6184 RVA: 0x0008BBC0 File Offset: 0x0008ABC0
			public void AlterContents()
			{
				if (this.over == null && this.under == null)
				{
					return;
				}
				PdfObject pdfObject = PdfReader.GetPdfObject(this.pageN.Get(PdfName.CONTENTS), this.pageN);
				PdfArray pdfArray;
				if (pdfObject == null)
				{
					pdfArray = new PdfArray();
					this.pageN.Put(PdfName.CONTENTS, pdfArray);
				}
				else if (pdfObject.IsArray())
				{
					pdfArray = (PdfArray)pdfObject;
				}
				else if (pdfObject.IsStream())
				{
					pdfArray = new PdfArray();
					pdfArray.Add(this.pageN.Get(PdfName.CONTENTS));
					this.pageN.Put(PdfName.CONTENTS, pdfArray);
				}
				else
				{
					pdfArray = new PdfArray();
					this.pageN.Put(PdfName.CONTENTS, pdfArray);
				}
				ByteBuffer byteBuffer = new ByteBuffer();
				if (this.under != null)
				{
					byteBuffer.Append(PdfContents.SAVESTATE);
					this.ApplyRotation(this.pageN, byteBuffer);
					byteBuffer.Append(this.under.InternalBuffer);
					byteBuffer.Append(PdfContents.RESTORESTATE);
				}
				if (this.over != null)
				{
					byteBuffer.Append(PdfContents.SAVESTATE);
				}
				PdfStream pdfStream = new PdfStream(byteBuffer.ToByteArray());
				pdfStream.FlateCompress(this.cstp.CompressionLevel);
				PdfIndirectReference indirectReference = this.cstp.AddToBody(pdfStream).IndirectReference;
				pdfArray.AddFirst(indirectReference);
				byteBuffer.Reset();
				if (this.over != null)
				{
					byteBuffer.Append(' ');
					byteBuffer.Append(PdfContents.RESTORESTATE);
					byteBuffer.Append(PdfContents.SAVESTATE);
					this.ApplyRotation(this.pageN, byteBuffer);
					byteBuffer.Append(this.over.InternalBuffer);
					byteBuffer.Append(PdfContents.RESTORESTATE);
					pdfStream = new PdfStream(byteBuffer.ToByteArray());
					pdfStream.FlateCompress(this.cstp.CompressionLevel);
					pdfArray.Add(this.cstp.AddToBody(pdfStream).IndirectReference);
				}
				this.pageN.Put(PdfName.RESOURCES, this.pageResources.Resources);
			}

			// Token: 0x06001829 RID: 6185 RVA: 0x0008BDB8 File Offset: 0x0008ADB8
			private void ApplyRotation(PdfDictionary pageN, ByteBuffer out_p)
			{
				if (!this.cstp.rotateContents)
				{
					return;
				}
				Rectangle pageSizeWithRotation = this.reader.GetPageSizeWithRotation(pageN);
				int rotation = pageSizeWithRotation.Rotation;
				int num = rotation;
				if (num == 90)
				{
					out_p.Append(PdfContents.ROTATE90);
					out_p.Append(pageSizeWithRotation.Top);
					out_p.Append(' ').Append('0').Append(PdfContents.ROTATEFINAL);
					return;
				}
				if (num == 180)
				{
					out_p.Append(PdfContents.ROTATE180);
					out_p.Append(pageSizeWithRotation.Right);
					out_p.Append(' ');
					out_p.Append(pageSizeWithRotation.Top);
					out_p.Append(PdfContents.ROTATEFINAL);
					return;
				}
				if (num != 270)
				{
					return;
				}
				out_p.Append(PdfContents.ROTATE270);
				out_p.Append('0').Append(' ');
				out_p.Append(pageSizeWithRotation.Right);
				out_p.Append(PdfContents.ROTATEFINAL);
			}

			// Token: 0x0600182A RID: 6186 RVA: 0x0008BEA4 File Offset: 0x0008AEA4
			private void AddDocumentField(PdfIndirectReference refi)
			{
				if (this.cstp.fieldArray == null)
				{
					this.cstp.fieldArray = new PdfArray();
				}
				this.cstp.fieldArray.Add(refi);
			}

			// Token: 0x0600182B RID: 6187 RVA: 0x0008BED8 File Offset: 0x0008AED8
			private void ExpandFields(PdfFormField field, List<PdfAnnotation> allAnnots)
			{
				allAnnots.Add(field);
				List<PdfFormField> kids = field.Kids;
				if (kids != null)
				{
					foreach (PdfFormField field2 in kids)
					{
						this.ExpandFields(field2, allAnnots);
					}
				}
			}

			// Token: 0x0600182C RID: 6188 RVA: 0x0008BF38 File Offset: 0x0008AF38
			public void AddAnnotation(PdfAnnotation annot)
			{
				List<PdfAnnotation> list = new List<PdfAnnotation>();
				if (annot.IsForm())
				{
					PdfFormField pdfFormField = (PdfFormField)annot;
					if (pdfFormField.Parent != null)
					{
						return;
					}
					this.ExpandFields(pdfFormField, list);
					if (this.cstp.fieldTemplates == null)
					{
						this.cstp.fieldTemplates = new Dictionary<PdfTemplate, object>();
					}
				}
				else
				{
					list.Add(annot);
				}
				for (int i = 0; i < list.Count; i++)
				{
					annot = list[i];
					if (annot.IsForm())
					{
						if (!annot.IsUsed())
						{
							Dictionary<PdfTemplate, object> templates = annot.Templates;
							if (templates != null)
							{
								foreach (PdfTemplate key in templates.Keys)
								{
									this.cstp.fieldTemplates[key] = null;
								}
							}
						}
						PdfFormField pdfFormField2 = (PdfFormField)annot;
						if (pdfFormField2.Parent == null)
						{
							this.AddDocumentField(pdfFormField2.IndirectReference);
						}
					}
					if (annot.IsAnnotation())
					{
						PdfObject pdfObject = PdfReader.GetPdfObject(this.pageN.Get(PdfName.ANNOTS), this.pageN);
						PdfArray pdfArray;
						if (pdfObject == null || !pdfObject.IsArray())
						{
							pdfArray = new PdfArray();
							this.pageN.Put(PdfName.ANNOTS, pdfArray);
						}
						else
						{
							pdfArray = (PdfArray)pdfObject;
						}
						pdfArray.Add(annot.IndirectReference);
						if (!annot.IsUsed())
						{
							PdfRectangle pdfRectangle = (PdfRectangle)annot.Get(PdfName.RECT);
							if (pdfRectangle != null && (pdfRectangle.Left != 0f || pdfRectangle.Right != 0f || pdfRectangle.Top != 0f || pdfRectangle.Bottom != 0f))
							{
								int pageRotation = this.reader.GetPageRotation(this.pageN);
								Rectangle pageSizeWithRotation = this.reader.GetPageSizeWithRotation(this.pageN);
								int num = pageRotation;
								if (num != 90)
								{
									if (num != 180)
									{
										if (num == 270)
										{
											annot.Put(PdfName.RECT, new PdfRectangle(pdfRectangle.Bottom, pageSizeWithRotation.Right - pdfRectangle.Left, pdfRectangle.Top, pageSizeWithRotation.Right - pdfRectangle.Right));
										}
									}
									else
									{
										annot.Put(PdfName.RECT, new PdfRectangle(pageSizeWithRotation.Right - pdfRectangle.Left, pageSizeWithRotation.Top - pdfRectangle.Bottom, pageSizeWithRotation.Right - pdfRectangle.Right, pageSizeWithRotation.Top - pdfRectangle.Top));
									}
								}
								else
								{
									annot.Put(PdfName.RECT, new PdfRectangle(pageSizeWithRotation.Top - pdfRectangle.Bottom, pdfRectangle.Left, pageSizeWithRotation.Top - pdfRectangle.Top, pdfRectangle.Right));
								}
							}
						}
					}
					if (!annot.IsUsed())
					{
						annot.SetUsed();
						this.cstp.AddToBody(annot, annot.IndirectReference);
					}
				}
			}

			// Token: 0x0400104A RID: 4170
			private PdfDictionary pageN;

			// Token: 0x0400104B RID: 4171
			private PdfCopy.StampContent under;

			// Token: 0x0400104C RID: 4172
			private PdfCopy.StampContent over;

			// Token: 0x0400104D RID: 4173
			private PageResources pageResources;

			// Token: 0x0400104E RID: 4174
			private PdfReader reader;

			// Token: 0x0400104F RID: 4175
			private PdfCopy cstp;
		}

		// Token: 0x0200027C RID: 636
		public class StampContent : PdfContentByte
		{
			// Token: 0x0600182D RID: 6189 RVA: 0x0008C23C File Offset: 0x0008B23C
			internal StampContent(PdfWriter writer, PageResources pageResources) : base(writer)
			{
				this.pageResources = pageResources;
			}

			// Token: 0x1700046A RID: 1130
			// (get) Token: 0x0600182E RID: 6190 RVA: 0x0008C24C File Offset: 0x0008B24C
			public override PdfContentByte Duplicate
			{
				get
				{
					return new PdfCopy.StampContent(this.writer, this.pageResources);
				}
			}

			// Token: 0x1700046B RID: 1131
			// (get) Token: 0x0600182F RID: 6191 RVA: 0x0008C25F File Offset: 0x0008B25F
			internal override PageResources PageResources
			{
				get
				{
					return this.pageResources;
				}
			}

			// Token: 0x04001050 RID: 4176
			private PageResources pageResources;
		}
	}
}
