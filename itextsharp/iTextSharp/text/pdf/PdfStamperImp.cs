using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.util;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.collection;
using iTextSharp.text.pdf.intern;
using iTextSharp.text.xml.xmp;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000276 RID: 630
	public class PdfStamperImp : PdfWriter
	{
		// Token: 0x060017C5 RID: 6085 RVA: 0x00087B74 File Offset: 0x00086B74
		internal PdfStamperImp(PdfReader reader, Stream os, char pdfVersion, bool append)
		{
			int[] array = new int[1];
			this.namePtr = array;
			this.partialFlattening = new Dictionary<string, object>();
			this.viewerPreferences = new PdfViewerPreferencesImp();
			this.fieldTemplates = new Dictionary<PdfTemplate, object>();
			base..ctor(new PdfDocument(), os);
			if (!reader.IsOpenedWithFullPermissions)
			{
				throw new BadPasswordException(MessageLocalization.GetComposedMessage("pdfreader.not.opened.with.owner.password"));
			}
			if (reader.Tampered)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("the.original.document.was.reused.read.it.again.from.file"));
			}
			reader.Tampered = true;
			this.reader = reader;
			this.file = reader.SafeFile;
			this.append = append;
			if (append)
			{
				if (reader.IsRebuilt())
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("append.mode.requires.a.document.without.errors.even.if.recovery.was.possible"));
				}
				if (reader.IsEncrypted())
				{
					this.crypto = new PdfEncryption(reader.Decrypt);
				}
				this.pdf_version.SetAppendmode(true);
				this.file.ReOpen();
				byte[] array2 = new byte[8192];
				int count;
				while ((count = this.file.Read(array2)) > 0)
				{
					this.os.Write(array2, 0, count);
				}
				this.file.Close();
				this.prevxref = reader.LastXref;
				reader.Appendable = true;
			}
			else if (pdfVersion == '\0')
			{
				base.PdfVersion = reader.PdfVersion;
			}
			else
			{
				base.PdfVersion = pdfVersion;
			}
			base.Open();
			this.pdf.AddWriter(this);
			if (append)
			{
				this.body.Refnum = reader.XrefSize;
				this.marked = new IntHashtable();
				if (reader.IsNewXrefType())
				{
					this.fullCompression = true;
				}
				if (reader.IsHybridXref())
				{
					this.fullCompression = false;
				}
			}
			this.initialXrefSize = reader.XrefSize;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00087D50 File Offset: 0x00086D50
		internal void Close(Dictionary<string, string> moreInfo)
		{
			if (this.closed)
			{
				return;
			}
			if (this.useVp)
			{
				this.reader.SetViewerPreferences(this.viewerPreferences);
				this.MarkUsed(this.reader.Trailer.Get(PdfName.ROOT));
			}
			if (this.flat)
			{
				this.FlatFields();
			}
			if (this.flatFreeText)
			{
				this.FlatFreeTextFields();
			}
			this.AddFieldResources();
			PdfDictionary catalog = this.reader.Catalog;
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(catalog.Get(PdfName.PAGES));
			pdfDictionary.Put(PdfName.ITXT, new PdfString(Document.Release));
			this.MarkUsed(pdfDictionary);
			PdfDictionary pdfDictionary2 = (PdfDictionary)PdfReader.GetPdfObject(catalog.Get(PdfName.ACROFORM), this.reader.Catalog);
			if (this.acroFields != null && this.acroFields.Xfa.Changed)
			{
				this.MarkUsed(pdfDictionary2);
				if (!this.flat)
				{
					this.acroFields.Xfa.SetXfa(this);
				}
			}
			if (this.sigFlags != 0 && pdfDictionary2 != null)
			{
				pdfDictionary2.Put(PdfName.SIGFLAGS, new PdfNumber(this.sigFlags));
				this.MarkUsed(pdfDictionary2);
				this.MarkUsed(catalog);
			}
			this.closed = true;
			base.AddSharedObjectsToBody();
			this.SetOutlines();
			this.SetJavaScript();
			this.AddFileAttachments();
			if (this.openAction != null)
			{
				catalog.Put(PdfName.OPENACTION, this.openAction);
			}
			if (this.pdf.pageLabels != null)
			{
				catalog.Put(PdfName.PAGELABELS, this.pdf.pageLabels.GetDictionary(this));
			}
			if (this.documentOCG.Count > 0)
			{
				base.FillOCProperties(false);
				PdfDictionary asDict = catalog.GetAsDict(PdfName.OCPROPERTIES);
				if (asDict == null)
				{
					this.reader.Catalog.Put(PdfName.OCPROPERTIES, base.OCProperties);
				}
				else
				{
					asDict.Put(PdfName.OCGS, base.OCProperties.Get(PdfName.OCGS));
					PdfDictionary pdfDictionary3 = asDict.GetAsDict(PdfName.D);
					if (pdfDictionary3 == null)
					{
						pdfDictionary3 = new PdfDictionary();
						asDict.Put(PdfName.D, pdfDictionary3);
					}
					pdfDictionary3.Put(PdfName.ORDER, base.OCProperties.GetAsDict(PdfName.D).Get(PdfName.ORDER));
					pdfDictionary3.Put(PdfName.RBGROUPS, base.OCProperties.GetAsDict(PdfName.D).Get(PdfName.RBGROUPS));
					pdfDictionary3.Put(PdfName.OFF, base.OCProperties.GetAsDict(PdfName.D).Get(PdfName.OFF));
					pdfDictionary3.Put(PdfName.AS, base.OCProperties.GetAsDict(PdfName.D).Get(PdfName.AS));
				}
			}
			int num = -1;
			PRIndirectReference prindirectReference = (PRIndirectReference)this.reader.Trailer.Get(PdfName.INFO);
			PdfDictionary pdfDictionary4 = (PdfDictionary)PdfReader.GetPdfObject(prindirectReference);
			string text = null;
			if (prindirectReference != null)
			{
				num = prindirectReference.Number;
			}
			if (pdfDictionary4 != null && pdfDictionary4.Get(PdfName.PRODUCER) != null)
			{
				text = pdfDictionary4.GetAsString(PdfName.PRODUCER).ToUnicodeString();
			}
			if (text == null)
			{
				text = Document.Version;
			}
			else if (text.IndexOf(Document.Product) == -1)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				stringBuilder.Append("; modified using ");
				stringBuilder.Append(Document.Version);
				text = stringBuilder.ToString();
			}
			byte[] array = null;
			PdfObject pdfObject = PdfReader.GetPdfObject(catalog.Get(PdfName.METADATA));
			if (pdfObject != null && pdfObject.IsStream())
			{
				array = PdfReader.GetStreamBytesRaw((PRStream)pdfObject);
				PdfReader.KillIndirect(catalog.Get(PdfName.METADATA));
			}
			if (this.xmpMetadata != null)
			{
				array = this.xmpMetadata;
			}
			PdfDate pdfDate = new PdfDate();
			if (array != null)
			{
				PdfStream pdfStream;
				try
				{
					XmpReader xmpReader = new XmpReader(array);
					if (!xmpReader.ReplaceNode("http://ns.adobe.com/pdf/1.3/", "Producer", text) && !xmpReader.ReplaceDescriptionAttribute("http://ns.adobe.com/pdf/1.3/", "Producer", text))
					{
						xmpReader.Add("rdf:Description", "http://ns.adobe.com/pdf/1.3/", "pdf:Producer", text);
					}
					if (!xmpReader.ReplaceNode("http://ns.adobe.com/xap/1.0/", "ModifyDate", pdfDate.GetW3CDate()) && !xmpReader.ReplaceDescriptionAttribute("http://ns.adobe.com/xap/1.0/", "ModifyDate", pdfDate.GetW3CDate()))
					{
						xmpReader.Add("rdf:Description", "http://ns.adobe.com/xap/1.0/", "xmp:ModifyDate", pdfDate.GetW3CDate());
					}
					if (!xmpReader.ReplaceNode("http://ns.adobe.com/xap/1.0/", "MetadataDate", pdfDate.GetW3CDate()))
					{
						xmpReader.ReplaceDescriptionAttribute("http://ns.adobe.com/xap/1.0/", "MetadataDate", pdfDate.GetW3CDate());
					}
					pdfStream = new PdfStream(xmpReader.SerializeDoc());
				}
				catch
				{
					pdfStream = new PdfStream(array);
				}
				pdfStream.Put(PdfName.TYPE, PdfName.METADATA);
				pdfStream.Put(PdfName.SUBTYPE, PdfName.XML);
				if (this.crypto != null && !this.crypto.IsMetadataEncrypted())
				{
					PdfArray pdfArray = new PdfArray();
					pdfArray.Add(PdfName.CRYPT);
					pdfStream.Put(PdfName.FILTER, pdfArray);
				}
				if (this.append && pdfObject != null)
				{
					this.body.Add(pdfStream, pdfObject.IndRef);
				}
				else
				{
					catalog.Put(PdfName.METADATA, this.body.Add(pdfStream).IndirectReference);
					this.MarkUsed(catalog);
				}
			}
			try
			{
				this.file.ReOpen();
				this.AlterContents();
				int number = ((PRIndirectReference)this.reader.trailer.Get(PdfName.ROOT)).Number;
				if (this.append)
				{
					foreach (int num2 in this.marked.GetKeys())
					{
						PdfObject pdfObjectRelease = this.reader.GetPdfObjectRelease(num2);
						if (pdfObjectRelease != null && num != num2 && num2 < this.initialXrefSize)
						{
							base.AddToBody(pdfObjectRelease, num2, num2 != number);
						}
					}
					for (int j = this.initialXrefSize; j < this.reader.XrefSize; j++)
					{
						PdfObject pdfObject2 = this.reader.GetPdfObject(j);
						if (pdfObject2 != null)
						{
							base.AddToBody(pdfObject2, this.GetNewObjectNumber(this.reader, j, 0));
						}
					}
				}
				else
				{
					for (int k = 1; k < this.reader.XrefSize; k++)
					{
						PdfObject pdfObjectRelease2 = this.reader.GetPdfObjectRelease(k);
						if (pdfObjectRelease2 != null && num != k)
						{
							base.AddToBody(pdfObjectRelease2, this.GetNewObjectNumber(this.reader, k, 0), k != number);
						}
					}
				}
			}
			finally
			{
				try
				{
					this.file.Close();
				}
				catch
				{
				}
			}
			PdfIndirectReference encryption = null;
			PdfObject fileID = null;
			if (this.crypto != null)
			{
				if (this.append)
				{
					encryption = this.reader.GetCryptoRef();
				}
				else
				{
					PdfIndirectObject pdfIndirectObject = base.AddToBody(this.crypto.GetEncryptionDictionary(), false);
					encryption = pdfIndirectObject.IndirectReference;
				}
				fileID = this.crypto.FileID;
			}
			else
			{
				fileID = PdfEncryption.CreateInfoId(PdfEncryption.CreateDocumentId());
			}
			PRIndirectReference prindirectReference2 = (PRIndirectReference)this.reader.trailer.Get(PdfName.ROOT);
			PdfIndirectReference root = new PdfIndirectReference(0, this.GetNewObjectNumber(this.reader, prindirectReference2.Number, 0));
			PdfDictionary pdfDictionary5 = new PdfDictionary();
			if (pdfDictionary4 != null)
			{
				foreach (PdfName key in pdfDictionary4.Keys)
				{
					PdfObject pdfObject3 = PdfReader.GetPdfObject(pdfDictionary4.Get(key));
					pdfDictionary5.Put(key, pdfObject3);
				}
			}
			if (moreInfo != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in moreInfo)
				{
					PdfName key2 = new PdfName(keyValuePair.Key);
					string value = keyValuePair.Value;
					if (value == null)
					{
						pdfDictionary5.Remove(key2);
					}
					else
					{
						pdfDictionary5.Put(key2, new PdfString(value, "UnicodeBig"));
					}
				}
			}
			pdfDictionary5.Put(PdfName.MODDATE, pdfDate);
			pdfDictionary5.Put(PdfName.PRODUCER, new PdfString(text));
			PdfIndirectReference indirectReference;
			if (this.append)
			{
				if (prindirectReference == null)
				{
					indirectReference = base.AddToBody(pdfDictionary5, false).IndirectReference;
				}
				else
				{
					indirectReference = base.AddToBody(pdfDictionary5, prindirectReference.Number, false).IndirectReference;
				}
			}
			else
			{
				indirectReference = base.AddToBody(pdfDictionary5, false).IndirectReference;
			}
			this.body.WriteCrossReferenceTable(this.os, root, indirectReference, encryption, fileID, this.prevxref);
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
				PdfWriter.PdfTrailer pdfTrailer = new PdfWriter.PdfTrailer(this.body.Size, this.body.Offset, root, indirectReference, encryption, fileID, this.prevxref);
				pdfTrailer.ToPdf(this, this.os);
			}
			this.os.Flush();
			if (this.CloseStream)
			{
				this.os.Close();
			}
			this.reader.Close();
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0008871C File Offset: 0x0008771C
		internal void ApplyRotation(PdfDictionary pageN, ByteBuffer out_p)
		{
			if (!this.rotateContents)
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

		// Token: 0x060017C8 RID: 6088 RVA: 0x00088804 File Offset: 0x00087804
		internal void AlterContents()
		{
			foreach (PdfStamperImp.PageStamp pageStamp in this.pagesToContent.Values)
			{
				PdfDictionary pageN = pageStamp.pageN;
				this.MarkUsed(pageN);
				PdfObject pdfObject = PdfReader.GetPdfObject(pageN.Get(PdfName.CONTENTS), pageN);
				PdfArray pdfArray;
				if (pdfObject == null)
				{
					pdfArray = new PdfArray();
					pageN.Put(PdfName.CONTENTS, pdfArray);
				}
				else if (pdfObject.IsArray())
				{
					pdfArray = (PdfArray)pdfObject;
					this.MarkUsed(pdfArray);
				}
				else if (pdfObject.IsStream())
				{
					pdfArray = new PdfArray();
					pdfArray.Add(pageN.Get(PdfName.CONTENTS));
					pageN.Put(PdfName.CONTENTS, pdfArray);
				}
				else
				{
					pdfArray = new PdfArray();
					pageN.Put(PdfName.CONTENTS, pdfArray);
				}
				ByteBuffer byteBuffer = new ByteBuffer();
				if (pageStamp.under != null)
				{
					byteBuffer.Append(PdfContents.SAVESTATE);
					this.ApplyRotation(pageN, byteBuffer);
					byteBuffer.Append(pageStamp.under.InternalBuffer);
					byteBuffer.Append(PdfContents.RESTORESTATE);
				}
				if (pageStamp.over != null)
				{
					byteBuffer.Append(PdfContents.SAVESTATE);
				}
				PdfStream pdfStream = new PdfStream(byteBuffer.ToByteArray());
				pdfStream.FlateCompress(this.compressionLevel);
				pdfArray.AddFirst(base.AddToBody(pdfStream).IndirectReference);
				byteBuffer.Reset();
				if (pageStamp.over != null)
				{
					byteBuffer.Append(' ');
					byteBuffer.Append(PdfContents.RESTORESTATE);
					ByteBuffer internalBuffer = pageStamp.over.InternalBuffer;
					byteBuffer.Append(internalBuffer.Buffer, 0, pageStamp.replacePoint);
					byteBuffer.Append(PdfContents.SAVESTATE);
					this.ApplyRotation(pageN, byteBuffer);
					byteBuffer.Append(internalBuffer.Buffer, pageStamp.replacePoint, internalBuffer.Size - pageStamp.replacePoint);
					byteBuffer.Append(PdfContents.RESTORESTATE);
					pdfStream = new PdfStream(byteBuffer.ToByteArray());
					pdfStream.FlateCompress(this.compressionLevel);
					pdfArray.Add(base.AddToBody(pdfStream).IndirectReference);
				}
				this.AlterResources(pageStamp);
			}
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00088A4C File Offset: 0x00087A4C
		internal void AlterResources(PdfStamperImp.PageStamp ps)
		{
			ps.pageN.Put(PdfName.RESOURCES, ps.pageResources.Resources);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00088A6C File Offset: 0x00087A6C
		protected internal override int GetNewObjectNumber(PdfReader reader, int number, int generation)
		{
			IntHashtable intHashtable;
			if (this.readers2intrefs.TryGetValue(reader, out intHashtable))
			{
				int num = intHashtable[number];
				if (num == 0)
				{
					num = base.IndirectReferenceNumber;
					intHashtable[number] = num;
				}
				return num;
			}
			if (this.currentPdfReaderInstance != null)
			{
				return this.currentPdfReaderInstance.GetNewObjectNumber(number, generation);
			}
			if (this.append && number < this.initialXrefSize)
			{
				return number;
			}
			int num2 = this.myXref[number];
			if (num2 == 0)
			{
				num2 = base.IndirectReferenceNumber;
				this.myXref[number] = num2;
			}
			return num2;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00088AF4 File Offset: 0x00087AF4
		internal override RandomAccessFileOrArray GetReaderFile(PdfReader reader)
		{
			if (this.readers2intrefs.ContainsKey(reader))
			{
				RandomAccessFileOrArray result;
				if (this.readers2file.TryGetValue(reader, out result))
				{
					return result;
				}
				return reader.SafeFile;
			}
			else
			{
				if (this.currentPdfReaderInstance == null)
				{
					return this.file;
				}
				return this.currentPdfReaderInstance.ReaderFile;
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00088B44 File Offset: 0x00087B44
		public void RegisterReader(PdfReader reader, bool openFile)
		{
			if (this.readers2intrefs.ContainsKey(reader))
			{
				return;
			}
			this.readers2intrefs[reader] = new IntHashtable();
			if (openFile)
			{
				RandomAccessFileOrArray safeFile = reader.SafeFile;
				this.readers2file[reader] = safeFile;
				safeFile.ReOpen();
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00088B90 File Offset: 0x00087B90
		public void UnRegisterReader(PdfReader reader)
		{
			if (!this.readers2intrefs.ContainsKey(reader))
			{
				return;
			}
			this.readers2intrefs.Remove(reader);
			RandomAccessFileOrArray randomAccessFileOrArray;
			if (!this.readers2file.TryGetValue(reader, out randomAccessFileOrArray))
			{
				return;
			}
			this.readers2file.Remove(reader);
			try
			{
				randomAccessFileOrArray.Close();
			}
			catch
			{
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00088BF4 File Offset: 0x00087BF4
		internal static void FindAllObjects(PdfReader reader, PdfObject obj, IntHashtable hits)
		{
			if (obj == null)
			{
				return;
			}
			switch (obj.Type)
			{
			case 5:
			{
				PdfArray pdfArray = (PdfArray)obj;
				for (int i = 0; i < pdfArray.Size; i++)
				{
					PdfStamperImp.FindAllObjects(reader, pdfArray[i], hits);
				}
				return;
			}
			case 6:
			case 7:
			{
				PdfDictionary pdfDictionary = (PdfDictionary)obj;
				foreach (PdfName key in pdfDictionary.Keys)
				{
					PdfStamperImp.FindAllObjects(reader, pdfDictionary.Get(key), hits);
				}
				break;
			}
			case 8:
			case 9:
				break;
			case 10:
			{
				PRIndirectReference prindirectReference = (PRIndirectReference)obj;
				if (reader != prindirectReference.Reader)
				{
					return;
				}
				if (hits.ContainsKey(prindirectReference.Number))
				{
					return;
				}
				hits[prindirectReference.Number] = 1;
				PdfStamperImp.FindAllObjects(reader, PdfReader.GetPdfObject(obj), hits);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00088CE8 File Offset: 0x00087CE8
		public void AddComments(FdfReader fdf)
		{
			if (this.readers2intrefs.ContainsKey(fdf))
			{
				return;
			}
			PdfDictionary pdfDictionary = fdf.Catalog;
			pdfDictionary = pdfDictionary.GetAsDict(PdfName.FDF);
			if (pdfDictionary == null)
			{
				return;
			}
			PdfArray asArray = pdfDictionary.GetAsArray(PdfName.ANNOTS);
			if (asArray == null || asArray.Size == 0)
			{
				return;
			}
			this.RegisterReader(fdf, false);
			IntHashtable intHashtable = new IntHashtable();
			Dictionary<string, PdfObject> dictionary = new Dictionary<string, PdfObject>();
			List<PdfObject> list = new List<PdfObject>();
			for (int i = 0; i < asArray.Size; i++)
			{
				PdfObject pdfObject = asArray[i];
				PdfDictionary pdfDictionary2 = (PdfDictionary)PdfReader.GetPdfObject(pdfObject);
				PdfNumber asNumber = pdfDictionary2.GetAsNumber(PdfName.PAGE);
				if (asNumber != null && asNumber.IntValue < this.reader.NumberOfPages)
				{
					PdfStamperImp.FindAllObjects(fdf, pdfObject, intHashtable);
					list.Add(pdfObject);
					if (pdfObject.Type == 10)
					{
						PdfObject pdfObject2 = PdfReader.GetPdfObject(pdfDictionary2.Get(PdfName.NM));
						if (pdfObject2 != null && pdfObject2.Type == 3)
						{
							dictionary[pdfObject2.ToString()] = pdfObject;
						}
					}
				}
			}
			foreach (int num in intHashtable.GetKeys())
			{
				PdfObject pdfObject3 = fdf.GetPdfObject(num);
				if (pdfObject3.Type == 6)
				{
					PdfObject pdfObject4 = PdfReader.GetPdfObject(((PdfDictionary)pdfObject3).Get(PdfName.IRT));
					if (pdfObject4 != null && pdfObject4.Type == 3)
					{
						PdfObject pdfObject5;
						dictionary.TryGetValue(pdfObject4.ToString(), out pdfObject5);
						if (pdfObject5 != null)
						{
							PdfDictionary pdfDictionary3 = new PdfDictionary();
							pdfDictionary3.Merge((PdfDictionary)pdfObject3);
							pdfDictionary3.Put(PdfName.IRT, pdfObject5);
							pdfObject3 = pdfDictionary3;
						}
					}
				}
				base.AddToBody(pdfObject3, this.GetNewObjectNumber(fdf, num, 0));
			}
			for (int k = 0; k < list.Count; k++)
			{
				PdfObject obj = list[k];
				PdfDictionary pdfDictionary4 = (PdfDictionary)PdfReader.GetPdfObject(obj);
				PdfNumber asNumber2 = pdfDictionary4.GetAsNumber(PdfName.PAGE);
				PdfDictionary pageN = this.reader.GetPageN(asNumber2.IntValue + 1);
				PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObject(pageN.Get(PdfName.ANNOTS), pageN);
				if (pdfArray == null)
				{
					pdfArray = new PdfArray();
					pageN.Put(PdfName.ANNOTS, pdfArray);
					this.MarkUsed(pageN);
				}
				this.MarkUsed(pdfArray);
				pdfArray.Add(obj);
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00088F4C File Offset: 0x00087F4C
		internal PdfStamperImp.PageStamp GetPageStamp(int pageNum)
		{
			PdfDictionary pageN = this.reader.GetPageN(pageNum);
			PdfStamperImp.PageStamp pageStamp;
			this.pagesToContent.TryGetValue(pageN, out pageStamp);
			if (pageStamp == null)
			{
				pageStamp = new PdfStamperImp.PageStamp(this, this.reader, pageN);
				this.pagesToContent[pageN] = pageStamp;
			}
			return pageStamp;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00088F94 File Offset: 0x00087F94
		internal PdfContentByte GetUnderContent(int pageNum)
		{
			if (pageNum < 1 || pageNum > this.reader.NumberOfPages)
			{
				return null;
			}
			PdfStamperImp.PageStamp pageStamp = this.GetPageStamp(pageNum);
			if (pageStamp.under == null)
			{
				pageStamp.under = new StampContent(this, pageStamp);
			}
			return pageStamp.under;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00088FD8 File Offset: 0x00087FD8
		internal PdfContentByte GetOverContent(int pageNum)
		{
			if (pageNum < 1 || pageNum > this.reader.NumberOfPages)
			{
				return null;
			}
			PdfStamperImp.PageStamp pageStamp = this.GetPageStamp(pageNum);
			if (pageStamp.over == null)
			{
				pageStamp.over = new StampContent(this, pageStamp);
			}
			return pageStamp.over;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0008901C File Offset: 0x0008801C
		internal void CorrectAcroFieldPages(int page)
		{
			if (this.acroFields == null)
			{
				return;
			}
			if (page > this.reader.NumberOfPages)
			{
				return;
			}
			Dictionary<string, AcroFields.Item> fields = this.acroFields.Fields;
			foreach (AcroFields.Item item in fields.Values)
			{
				for (int i = 0; i < item.Size; i++)
				{
					int page2 = item.GetPage(i);
					if (page2 >= page)
					{
						item.ForcePage(i, page2 + 1);
					}
				}
			}
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000890B4 File Offset: 0x000880B4
		private static void MoveRectangle(PdfDictionary dic2, PdfReader r, int pageImported, PdfName key, string name)
		{
			Rectangle boxSize = r.GetBoxSize(pageImported, name);
			if (boxSize == null)
			{
				dic2.Remove(key);
				return;
			}
			dic2.Put(key, new PdfRectangle(boxSize));
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000890E4 File Offset: 0x000880E4
		internal void ReplacePage(PdfReader r, int pageImported, int pageReplaced)
		{
			PdfDictionary pageN = this.reader.GetPageN(pageReplaced);
			if (this.pagesToContent.ContainsKey(pageN))
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("this.page.cannot.be.replaced.new.content.was.already.added"));
			}
			PdfImportedPage importedPage = this.GetImportedPage(r, pageImported);
			PdfDictionary pageNRelease = this.reader.GetPageNRelease(pageReplaced);
			pageNRelease.Remove(PdfName.RESOURCES);
			pageNRelease.Remove(PdfName.CONTENTS);
			PdfStamperImp.MoveRectangle(pageNRelease, r, pageImported, PdfName.MEDIABOX, "media");
			PdfStamperImp.MoveRectangle(pageNRelease, r, pageImported, PdfName.CROPBOX, "crop");
			PdfStamperImp.MoveRectangle(pageNRelease, r, pageImported, PdfName.TRIMBOX, "trim");
			PdfStamperImp.MoveRectangle(pageNRelease, r, pageImported, PdfName.ARTBOX, "art");
			PdfStamperImp.MoveRectangle(pageNRelease, r, pageImported, PdfName.BLEEDBOX, "bleed");
			pageNRelease.Put(PdfName.ROTATE, new PdfNumber(r.GetPageRotation(pageImported)));
			PdfContentByte overContent = this.GetOverContent(pageReplaced);
			overContent.AddTemplate(importedPage, 0f, 0f);
			PdfStamperImp.PageStamp pageStamp = this.pagesToContent[pageN];
			pageStamp.replacePoint = pageStamp.over.InternalBuffer.Size;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000891F8 File Offset: 0x000881F8
		internal void InsertPage(int pageNumber, Rectangle mediabox)
		{
			Rectangle rectangle = new Rectangle(mediabox);
			int num = rectangle.Rotation % 360;
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.PAGE);
			PdfDictionary pdfDictionary2 = new PdfDictionary();
			PdfArray pdfArray = new PdfArray();
			pdfArray.Add(PdfName.PDF);
			pdfArray.Add(PdfName.TEXT);
			pdfArray.Add(PdfName.IMAGEB);
			pdfArray.Add(PdfName.IMAGEC);
			pdfArray.Add(PdfName.IMAGEI);
			pdfDictionary2.Put(PdfName.PROCSET, pdfArray);
			pdfDictionary.Put(PdfName.RESOURCES, pdfDictionary2);
			pdfDictionary.Put(PdfName.ROTATE, new PdfNumber(num));
			pdfDictionary.Put(PdfName.MEDIABOX, new PdfRectangle(rectangle, num));
			PRIndirectReference prindirectReference = this.reader.AddPdfObject(pdfDictionary);
			PRIndirectReference prindirectReference2;
			PdfDictionary pdfDictionary3;
			if (pageNumber > this.reader.NumberOfPages)
			{
				PdfDictionary pageNRelease = this.reader.GetPageNRelease(this.reader.NumberOfPages);
				prindirectReference2 = (PRIndirectReference)pageNRelease.Get(PdfName.PARENT);
				prindirectReference2 = new PRIndirectReference(this.reader, prindirectReference2.Number);
				pdfDictionary3 = (PdfDictionary)PdfReader.GetPdfObject(prindirectReference2);
				PdfArray pdfArray2 = (PdfArray)PdfReader.GetPdfObject(pdfDictionary3.Get(PdfName.KIDS), pdfDictionary3);
				pdfArray2.Add(prindirectReference);
				this.MarkUsed(pdfArray2);
				this.reader.pageRefs.InsertPage(pageNumber, prindirectReference);
			}
			else
			{
				if (pageNumber < 1)
				{
					pageNumber = 1;
				}
				PdfDictionary pageN = this.reader.GetPageN(pageNumber);
				PRIndirectReference pageOrigRef = this.reader.GetPageOrigRef(pageNumber);
				this.reader.ReleasePage(pageNumber);
				prindirectReference2 = (PRIndirectReference)pageN.Get(PdfName.PARENT);
				prindirectReference2 = new PRIndirectReference(this.reader, prindirectReference2.Number);
				pdfDictionary3 = (PdfDictionary)PdfReader.GetPdfObject(prindirectReference2);
				PdfArray pdfArray3 = (PdfArray)PdfReader.GetPdfObject(pdfDictionary3.Get(PdfName.KIDS), pdfDictionary3);
				int size = pdfArray3.Size;
				int number = pageOrigRef.Number;
				for (int i = 0; i < size; i++)
				{
					PRIndirectReference prindirectReference3 = (PRIndirectReference)pdfArray3[i];
					if (number == prindirectReference3.Number)
					{
						pdfArray3.Add(i, prindirectReference);
						break;
					}
				}
				if (size == pdfArray3.Size)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("internal.inconsistence"));
				}
				this.MarkUsed(pdfArray3);
				this.reader.pageRefs.InsertPage(pageNumber, prindirectReference);
				this.CorrectAcroFieldPages(pageNumber);
			}
			pdfDictionary.Put(PdfName.PARENT, prindirectReference2);
			while (pdfDictionary3 != null)
			{
				this.MarkUsed(pdfDictionary3);
				PdfNumber pdfNumber = (PdfNumber)PdfReader.GetPdfObjectRelease(pdfDictionary3.Get(PdfName.COUNT));
				pdfDictionary3.Put(PdfName.COUNT, new PdfNumber(pdfNumber.IntValue + 1));
				pdfDictionary3 = pdfDictionary3.GetAsDict(PdfName.PARENT);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000894C8 File Offset: 0x000884C8
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x000894BF File Offset: 0x000884BF
		internal bool RotateContents
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

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x000894D0 File Offset: 0x000884D0
		internal bool ContentWritten
		{
			get
			{
				return this.body.Size > 1;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x000894E0 File Offset: 0x000884E0
		internal AcroFields AcroFields
		{
			get
			{
				if (this.acroFields == null)
				{
					this.acroFields = new AcroFields(this.reader, this);
				}
				return this.acroFields;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x00089502 File Offset: 0x00088502
		internal bool FormFlattening
		{
			set
			{
				this.flat = value;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (set) Token: 0x060017DC RID: 6108 RVA: 0x0008950B File Offset: 0x0008850B
		internal bool FreeTextFlattening
		{
			set
			{
				this.flatFreeText = value;
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00089514 File Offset: 0x00088514
		internal bool PartialFormFlattening(string name)
		{
			AcroFields acroFields = this.AcroFields;
			if (this.acroFields.Xfa.XfaPresent)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("partial.form.flattening.is.not.supported.with.xfa.forms"));
			}
			if (!this.acroFields.Fields.ContainsKey(name))
			{
				return false;
			}
			this.partialFlattening[name] = null;
			return true;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00089570 File Offset: 0x00088570
		internal void FlatFields()
		{
			if (this.append)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("field.flattening.is.not.supported.in.append.mode"));
			}
			AcroFields acroFields = this.AcroFields;
			Dictionary<string, AcroFields.Item> fields = this.acroFields.Fields;
			if (this.fieldsAdded && this.partialFlattening.Count == 0)
			{
				foreach (string key in fields.Keys)
				{
					this.partialFlattening[key] = null;
				}
			}
			PdfDictionary asDict = this.reader.Catalog.GetAsDict(PdfName.ACROFORM);
			PdfArray pdfArray = null;
			if (asDict != null)
			{
				pdfArray = (PdfArray)PdfReader.GetPdfObject(asDict.Get(PdfName.FIELDS), asDict);
			}
			foreach (KeyValuePair<string, AcroFields.Item> keyValuePair in fields)
			{
				string key2 = keyValuePair.Key;
				if (this.partialFlattening.Count == 0 || this.partialFlattening.ContainsKey(key2))
				{
					AcroFields.Item value = keyValuePair.Value;
					for (int i = 0; i < value.Size; i++)
					{
						PdfDictionary merged = value.GetMerged(i);
						PdfNumber asNumber = merged.GetAsNumber(PdfName.F);
						int num = 0;
						if (asNumber != null)
						{
							num = asNumber.IntValue;
						}
						int page = value.GetPage(i);
						PdfDictionary asDict2 = merged.GetAsDict(PdfName.AP);
						if (asDict2 != null && (num & 4) != 0 && (num & 2) == 0)
						{
							PdfObject pdfObject = asDict2.Get(PdfName.N);
							PdfAppearance pdfAppearance = null;
							if (pdfObject != null)
							{
								PdfObject pdfObject2 = PdfReader.GetPdfObject(pdfObject);
								if (pdfObject is PdfIndirectReference && !pdfObject.IsIndirect())
								{
									pdfAppearance = new PdfAppearance((PdfIndirectReference)pdfObject);
								}
								else if (pdfObject2 is PdfStream)
								{
									((PdfDictionary)pdfObject2).Put(PdfName.SUBTYPE, PdfName.FORM);
									pdfAppearance = new PdfAppearance((PdfIndirectReference)pdfObject);
								}
								else if (pdfObject2 != null && pdfObject2.IsDictionary())
								{
									PdfName asName = merged.GetAsName(PdfName.AS);
									if (asName != null)
									{
										PdfIndirectReference pdfIndirectReference = (PdfIndirectReference)((PdfDictionary)pdfObject2).Get(asName);
										if (pdfIndirectReference != null)
										{
											pdfAppearance = new PdfAppearance(pdfIndirectReference);
											if (pdfIndirectReference.IsIndirect())
											{
												pdfObject2 = PdfReader.GetPdfObject(pdfIndirectReference);
												((PdfDictionary)pdfObject2).Put(PdfName.SUBTYPE, PdfName.FORM);
											}
										}
									}
								}
							}
							if (pdfAppearance != null)
							{
								Rectangle normalizedRectangle = PdfReader.GetNormalizedRectangle(merged.GetAsArray(PdfName.RECT));
								PdfContentByte overContent = this.GetOverContent(page);
								overContent.SetLiteral("Q ");
								overContent.AddTemplate(pdfAppearance, normalizedRectangle.Left, normalizedRectangle.Bottom);
								overContent.SetLiteral("q ");
							}
						}
						if (this.partialFlattening.Count != 0)
						{
							PdfDictionary pageN = this.reader.GetPageN(page);
							PdfArray asArray = pageN.GetAsArray(PdfName.ANNOTS);
							if (asArray != null)
							{
								for (int j = 0; j < asArray.Size; j++)
								{
									PdfObject pdfObject3 = asArray[j];
									if (pdfObject3.IsIndirect())
									{
										PdfObject widgetRef = value.GetWidgetRef(i);
										if (widgetRef.IsIndirect() && ((PRIndirectReference)pdfObject3).Number == ((PRIndirectReference)widgetRef).Number)
										{
											asArray.Remove(j--);
											PRIndirectReference prindirectReference = (PRIndirectReference)widgetRef;
											for (;;)
											{
												PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(prindirectReference);
												PRIndirectReference prindirectReference2 = (PRIndirectReference)pdfDictionary.Get(PdfName.PARENT);
												PdfReader.KillIndirect(prindirectReference);
												if (prindirectReference2 == null)
												{
													break;
												}
												PdfDictionary pdfDictionary2 = (PdfDictionary)PdfReader.GetPdfObject(prindirectReference2);
												PdfArray asArray2 = pdfDictionary2.GetAsArray(PdfName.KIDS);
												for (int k = 0; k < asArray2.Size; k++)
												{
													PdfObject pdfObject4 = asArray2[k];
													if (pdfObject4.IsIndirect() && ((PRIndirectReference)pdfObject4).Number == prindirectReference.Number)
													{
														asArray2.Remove(k);
														k--;
													}
												}
												if (!asArray2.IsEmpty())
												{
													goto IL_41E;
												}
												prindirectReference = prindirectReference2;
											}
											for (int l = 0; l < pdfArray.Size; l++)
											{
												PdfObject pdfObject5 = pdfArray[l];
												if (pdfObject5.IsIndirect() && ((PRIndirectReference)pdfObject5).Number == prindirectReference.Number)
												{
													pdfArray.Remove(l);
													l--;
												}
											}
										}
									}
									IL_41E:;
								}
								if (asArray.IsEmpty())
								{
									PdfReader.KillIndirect(pageN.Get(PdfName.ANNOTS));
									pageN.Remove(PdfName.ANNOTS);
								}
							}
						}
					}
				}
			}
			if (!this.fieldsAdded && this.partialFlattening.Count == 0)
			{
				for (int m = 1; m <= this.reader.NumberOfPages; m++)
				{
					PdfDictionary pageN2 = this.reader.GetPageN(m);
					PdfArray asArray3 = pageN2.GetAsArray(PdfName.ANNOTS);
					if (asArray3 != null)
					{
						for (int n = 0; n < asArray3.Size; n++)
						{
							PdfObject directObject = asArray3.GetDirectObject(n);
							if ((!(directObject is PdfIndirectReference) || directObject.IsIndirect()) && (!directObject.IsDictionary() || PdfName.WIDGET.Equals(((PdfDictionary)directObject).Get(PdfName.SUBTYPE))))
							{
								asArray3.Remove(n);
								n--;
							}
						}
						if (asArray3.IsEmpty())
						{
							PdfReader.KillIndirect(pageN2.Get(PdfName.ANNOTS));
							pageN2.Remove(PdfName.ANNOTS);
						}
					}
				}
				this.EliminateAcroformObjects();
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00089B30 File Offset: 0x00088B30
		internal void EliminateAcroformObjects()
		{
			PdfObject pdfObject = this.reader.Catalog.Get(PdfName.ACROFORM);
			if (pdfObject == null)
			{
				return;
			}
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(pdfObject);
			this.reader.KillXref(pdfDictionary.Get(PdfName.XFA));
			pdfDictionary.Remove(PdfName.XFA);
			PdfObject pdfObject2 = pdfDictionary.Get(PdfName.FIELDS);
			if (pdfObject2 != null)
			{
				PdfDictionary pdfDictionary2 = new PdfDictionary();
				pdfDictionary2.Put(PdfName.KIDS, pdfObject2);
				this.SweepKids(pdfDictionary2);
				PdfReader.KillIndirect(pdfObject2);
				pdfDictionary.Put(PdfName.FIELDS, new PdfArray());
			}
			pdfDictionary.Remove(PdfName.SIGFLAGS);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00089BD0 File Offset: 0x00088BD0
		internal void SweepKids(PdfObject obj)
		{
			PdfObject pdfObject = PdfReader.KillIndirect(obj);
			if (pdfObject == null || !pdfObject.IsDictionary())
			{
				return;
			}
			PdfDictionary pdfDictionary = (PdfDictionary)pdfObject;
			PdfArray pdfArray = (PdfArray)PdfReader.KillIndirect(pdfDictionary.Get(PdfName.KIDS));
			if (pdfArray == null)
			{
				return;
			}
			for (int i = 0; i < pdfArray.Size; i++)
			{
				this.SweepKids(pdfArray[i]);
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00089C30 File Offset: 0x00088C30
		private void FlatFreeTextFields()
		{
			if (this.append)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("freetext.flattening.is.not.supported.in.append.mode"));
			}
			for (int i = 1; i <= this.reader.NumberOfPages; i++)
			{
				PdfDictionary pageN = this.reader.GetPageN(i);
				PdfArray asArray = pageN.GetAsArray(PdfName.ANNOTS);
				if (asArray != null)
				{
					for (int j = 0; j < asArray.Size; j++)
					{
						PdfObject directObject = asArray.GetDirectObject(j);
						if (!(directObject is PdfIndirectReference) || directObject.IsIndirect())
						{
							PdfDictionary pdfDictionary = (PdfDictionary)directObject;
							if (((PdfName)pdfDictionary.Get(PdfName.SUBTYPE)).Equals(PdfName.FREETEXT))
							{
								PdfNumber asNumber = pdfDictionary.GetAsNumber(PdfName.F);
								int num = (asNumber != null) ? asNumber.IntValue : 0;
								if ((num & 4) != 0 && (num & 2) == 0)
								{
									PdfObject pdfObject = pdfDictionary.Get(PdfName.AP);
									if (pdfObject != null)
									{
										PdfDictionary pdfDictionary2 = (pdfObject is PdfIndirectReference) ? ((PdfDictionary)PdfReader.GetPdfObject(pdfObject)) : ((PdfDictionary)pdfObject);
										PdfObject pdfObject2 = pdfDictionary2.Get(PdfName.N);
										PdfAppearance pdfAppearance = null;
										if (pdfObject2 != null)
										{
											PdfObject pdfObject3 = PdfReader.GetPdfObject(pdfObject2);
											if (pdfObject2 is PdfIndirectReference && !pdfObject2.IsIndirect())
											{
												pdfAppearance = new PdfAppearance((PdfIndirectReference)pdfObject2);
											}
											else if (pdfObject3 is PdfStream)
											{
												((PdfDictionary)pdfObject3).Put(PdfName.SUBTYPE, PdfName.FORM);
												pdfAppearance = new PdfAppearance((PdfIndirectReference)pdfObject2);
											}
											else if (pdfObject3.IsDictionary())
											{
												PdfName asName = pdfDictionary2.GetAsName(PdfName.AS);
												if (asName != null)
												{
													PdfIndirectReference pdfIndirectReference = (PdfIndirectReference)((PdfDictionary)pdfObject3).Get(asName);
													if (pdfIndirectReference != null)
													{
														pdfAppearance = new PdfAppearance(pdfIndirectReference);
														if (pdfIndirectReference.IsIndirect())
														{
															pdfObject3 = PdfReader.GetPdfObject(pdfIndirectReference);
															((PdfDictionary)pdfObject3).Put(PdfName.SUBTYPE, PdfName.FORM);
														}
													}
												}
											}
										}
										if (pdfAppearance != null)
										{
											Rectangle normalizedRectangle = PdfReader.GetNormalizedRectangle(pdfDictionary.GetAsArray(PdfName.RECT));
											PdfContentByte overContent = this.GetOverContent(i);
											overContent.SetLiteral("Q ");
											overContent.AddTemplate(pdfAppearance, normalizedRectangle.Left, normalizedRectangle.Bottom);
											overContent.SetLiteral("q ");
										}
									}
								}
							}
						}
					}
					for (int k = 0; k < asArray.Size; k++)
					{
						PdfDictionary asDict = asArray.GetAsDict(k);
						if (asDict != null && PdfName.FREETEXT.Equals(asDict.Get(PdfName.SUBTYPE)))
						{
							asArray.Remove(k);
							k--;
						}
					}
					if (asArray.IsEmpty())
					{
						PdfReader.KillIndirect(pageN.Get(PdfName.ANNOTS));
						pageN.Remove(PdfName.ANNOTS);
					}
				}
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00089EEC File Offset: 0x00088EEC
		public override PdfIndirectReference GetPageReference(int page)
		{
			PdfIndirectReference pageOrigRef = this.reader.GetPageOrigRef(page);
			if (pageOrigRef == null)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.page.number.1", page));
			}
			return pageOrigRef;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00089F20 File Offset: 0x00088F20
		public override void AddAnnotation(PdfAnnotation annot)
		{
			throw new Exception(MessageLocalization.GetComposedMessage("unsupported.in.this.context.use.pdfstamper.addannotation"));
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00089F34 File Offset: 0x00088F34
		internal void AddDocumentField(PdfIndirectReference ref_p)
		{
			PdfDictionary catalog = this.reader.Catalog;
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(catalog.Get(PdfName.ACROFORM), catalog);
			if (pdfDictionary == null)
			{
				pdfDictionary = new PdfDictionary();
				catalog.Put(PdfName.ACROFORM, pdfDictionary);
				this.MarkUsed(catalog);
			}
			PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObject(pdfDictionary.Get(PdfName.FIELDS), pdfDictionary);
			if (pdfArray == null)
			{
				pdfArray = new PdfArray();
				pdfDictionary.Put(PdfName.FIELDS, pdfArray);
				this.MarkUsed(pdfDictionary);
			}
			if (!pdfDictionary.Contains(PdfName.DA))
			{
				pdfDictionary.Put(PdfName.DA, new PdfString("/Helv 0 Tf 0 g "));
				this.MarkUsed(pdfDictionary);
			}
			pdfArray.Add(ref_p);
			this.MarkUsed(pdfArray);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00089FEC File Offset: 0x00088FEC
		internal void AddFieldResources()
		{
			if (this.fieldTemplates.Count == 0)
			{
				return;
			}
			PdfDictionary catalog = this.reader.Catalog;
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(catalog.Get(PdfName.ACROFORM), catalog);
			if (pdfDictionary == null)
			{
				pdfDictionary = new PdfDictionary();
				catalog.Put(PdfName.ACROFORM, pdfDictionary);
				this.MarkUsed(catalog);
			}
			PdfDictionary pdfDictionary2 = (PdfDictionary)PdfReader.GetPdfObject(pdfDictionary.Get(PdfName.DR), pdfDictionary);
			if (pdfDictionary2 == null)
			{
				pdfDictionary2 = new PdfDictionary();
				pdfDictionary.Put(PdfName.DR, pdfDictionary2);
				this.MarkUsed(pdfDictionary);
			}
			this.MarkUsed(pdfDictionary2);
			foreach (PdfTemplate pdfTemplate in this.fieldTemplates.Keys)
			{
				PdfFormField.MergeResources(pdfDictionary2, (PdfDictionary)pdfTemplate.Resources, this);
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
			if (pdfDictionary.Get(PdfName.DA) == null)
			{
				pdfDictionary.Put(PdfName.DA, new PdfString("/Helv 0 Tf 0 g "));
				this.MarkUsed(pdfDictionary);
			}
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0008A204 File Offset: 0x00089204
		internal void ExpandFields(PdfFormField field, List<PdfAnnotation> allAnnots)
		{
			allAnnots.Add(field);
			List<PdfFormField> kids = field.Kids;
			if (kids != null)
			{
				for (int i = 0; i < kids.Count; i++)
				{
					this.ExpandFields(kids[i], allAnnots);
				}
			}
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0008A244 File Offset: 0x00089244
		internal void AddAnnotation(PdfAnnotation annot, PdfDictionary pageN)
		{
			List<PdfAnnotation> list = new List<PdfAnnotation>();
			if (annot.IsForm())
			{
				this.fieldsAdded = true;
				AcroFields acroFields = this.AcroFields;
				PdfFormField pdfFormField = (PdfFormField)annot;
				if (pdfFormField.Parent != null)
				{
					return;
				}
				this.ExpandFields(pdfFormField, list);
			}
			else
			{
				list.Add(annot);
			}
			for (int i = 0; i < list.Count; i++)
			{
				annot = list[i];
				if (annot.PlaceInPage > 0)
				{
					pageN = this.reader.GetPageN(annot.PlaceInPage);
				}
				if (annot.IsForm())
				{
					if (!annot.IsUsed())
					{
						Dictionary<PdfTemplate, object> templates = annot.Templates;
						if (templates != null)
						{
							foreach (PdfTemplate key in templates.Keys)
							{
								this.fieldTemplates[key] = null;
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
					PdfObject pdfObject = PdfReader.GetPdfObject(pageN.Get(PdfName.ANNOTS), pageN);
					PdfArray pdfArray;
					if (pdfObject == null || !pdfObject.IsArray())
					{
						pdfArray = new PdfArray();
						pageN.Put(PdfName.ANNOTS, pdfArray);
						this.MarkUsed(pageN);
					}
					else
					{
						pdfArray = (PdfArray)pdfObject;
					}
					pdfArray.Add(annot.IndirectReference);
					this.MarkUsed(pdfArray);
					if (!annot.IsUsed())
					{
						PdfRectangle pdfRectangle = (PdfRectangle)annot.Get(PdfName.RECT);
						if (pdfRectangle != null && (pdfRectangle.Left != 0f || pdfRectangle.Right != 0f || pdfRectangle.Top != 0f || pdfRectangle.Bottom != 0f))
						{
							int pageRotation = this.reader.GetPageRotation(pageN);
							Rectangle pageSizeWithRotation = this.reader.GetPageSizeWithRotation(pageN);
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
								annot.Put(PdfName.RECT, new PdfRectangle(pageSizeWithRotation.Top - pdfRectangle.Top, pdfRectangle.Right, pageSizeWithRotation.Top - pdfRectangle.Bottom, pdfRectangle.Left));
							}
						}
					}
				}
				if (!annot.IsUsed())
				{
					annot.SetUsed();
					base.AddToBody(annot, annot.IndirectReference);
				}
			}
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0008A544 File Offset: 0x00089544
		internal override void AddAnnotation(PdfAnnotation annot, int page)
		{
			annot.Page = page;
			this.AddAnnotation(annot, this.reader.GetPageN(page));
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0008A560 File Offset: 0x00089560
		private void OutlineTravel(PRIndirectReference outline)
		{
			while (outline != null)
			{
				PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(outline);
				PRIndirectReference prindirectReference = (PRIndirectReference)pdfDictionary.Get(PdfName.FIRST);
				if (prindirectReference != null)
				{
					this.OutlineTravel(prindirectReference);
				}
				PdfReader.KillIndirect(pdfDictionary.Get(PdfName.DEST));
				PdfReader.KillIndirect(pdfDictionary.Get(PdfName.A));
				PdfReader.KillIndirect(outline);
				outline = (PRIndirectReference)pdfDictionary.Get(PdfName.NEXT);
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0008A5D4 File Offset: 0x000895D4
		internal void DeleteOutlines()
		{
			PdfDictionary catalog = this.reader.Catalog;
			PRIndirectReference prindirectReference = (PRIndirectReference)catalog.Get(PdfName.OUTLINES);
			if (prindirectReference == null)
			{
				return;
			}
			this.OutlineTravel(prindirectReference);
			PdfReader.KillIndirect(prindirectReference);
			catalog.Remove(PdfName.OUTLINES);
			this.MarkUsed(catalog);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0008A624 File Offset: 0x00089624
		internal void SetJavaScript()
		{
			Dictionary<string, PdfObject> documentLevelJS = this.pdf.GetDocumentLevelJS();
			if (documentLevelJS.Count == 0)
			{
				return;
			}
			PdfDictionary catalog = this.reader.Catalog;
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(catalog.Get(PdfName.NAMES), catalog);
			if (pdfDictionary == null)
			{
				pdfDictionary = new PdfDictionary();
				catalog.Put(PdfName.NAMES, pdfDictionary);
				this.MarkUsed(catalog);
			}
			this.MarkUsed(pdfDictionary);
			PdfDictionary objecta = PdfNameTree.WriteTree<PdfObject>(documentLevelJS, this);
			pdfDictionary.Put(PdfName.JAVASCRIPT, base.AddToBody(objecta).IndirectReference);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0008A6AC File Offset: 0x000896AC
		private void AddFileAttachments()
		{
			Dictionary<string, PdfObject> documentFileAttachment = this.pdf.GetDocumentFileAttachment();
			if (documentFileAttachment.Count == 0)
			{
				return;
			}
			PdfDictionary catalog = this.reader.Catalog;
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(catalog.Get(PdfName.NAMES), catalog);
			if (pdfDictionary == null)
			{
				pdfDictionary = new PdfDictionary();
				catalog.Put(PdfName.NAMES, pdfDictionary);
				this.MarkUsed(catalog);
			}
			this.MarkUsed(pdfDictionary);
			Dictionary<string, PdfObject> dictionary = PdfNameTree.ReadTree((PdfDictionary)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.EMBEDDEDFILES)));
			foreach (KeyValuePair<string, PdfObject> keyValuePair in documentFileAttachment)
			{
				string key = keyValuePair.Key;
				int num = 0;
				string text = key;
				while (dictionary.ContainsKey(text))
				{
					num++;
					text = text + " " + num;
				}
				dictionary[text] = keyValuePair.Value;
			}
			PdfDictionary objecta = PdfNameTree.WriteTree<PdfObject>(dictionary, this);
			PdfObject pdfObject = pdfDictionary.Get(PdfName.EMBEDDEDFILES);
			if (pdfObject != null)
			{
				PdfReader.KillIndirect(pdfObject);
			}
			pdfDictionary.Put(PdfName.EMBEDDEDFILES, base.AddToBody(objecta).IndirectReference);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0008A7EC File Offset: 0x000897EC
		internal void MakePackage(PdfCollection collection)
		{
			PdfDictionary catalog = this.reader.Catalog;
			catalog.Put(PdfName.COLLECTION, collection);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0008A814 File Offset: 0x00089814
		internal void SetOutlines()
		{
			if (this.newBookmarks == null)
			{
				return;
			}
			this.DeleteOutlines();
			if (this.newBookmarks.Count == 0)
			{
				return;
			}
			PdfDictionary catalog = this.reader.Catalog;
			bool namedAsNames = catalog.Get(PdfName.DESTS) != null;
			base.WriteOutlines(catalog, namedAsNames);
			this.MarkUsed(catalog);
		}

		// Token: 0x17000460 RID: 1120
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x0008A86B File Offset: 0x0008986B
		public override int ViewerPreferences
		{
			set
			{
				this.useVp = true;
				this.viewerPreferences.ViewerPreferences = value;
			}
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0008A880 File Offset: 0x00089880
		public override void AddViewerPreference(PdfName key, PdfObject value)
		{
			this.useVp = true;
			this.viewerPreferences.AddViewerPreference(key, value);
		}

		// Token: 0x17000461 RID: 1121
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x0008A896 File Offset: 0x00089896
		public override int SigFlags
		{
			set
			{
				this.sigFlags |= value;
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0008A8A6 File Offset: 0x000898A6
		public override void SetPageAction(PdfName actionType, PdfAction action)
		{
			throw new InvalidOperationException(MessageLocalization.GetComposedMessage("use.setpageaction.pdfname.actiontype.pdfaction.action.int.page"));
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0008A8B8 File Offset: 0x000898B8
		internal void SetPageAction(PdfName actionType, PdfAction action, int page)
		{
			if (!actionType.Equals(PdfWriter.PAGE_OPEN) && !actionType.Equals(PdfWriter.PAGE_CLOSE))
			{
				throw new PdfException(MessageLocalization.GetComposedMessage("invalid.page.additional.action.type.1", actionType.ToString()));
			}
			PdfDictionary pageN = this.reader.GetPageN(page);
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(pageN.Get(PdfName.AA), pageN);
			if (pdfDictionary == null)
			{
				pdfDictionary = new PdfDictionary();
				pageN.Put(PdfName.AA, pdfDictionary);
				this.MarkUsed(pageN);
			}
			pdfDictionary.Put(actionType, action);
			this.MarkUsed(pdfDictionary);
		}

		// Token: 0x17000462 RID: 1122
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x0008A944 File Offset: 0x00089944
		public override int Duration
		{
			set
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("use.the.methods.at.pdfstamper"));
			}
		}

		// Token: 0x17000463 RID: 1123
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x0008A955 File Offset: 0x00089955
		public override PdfTransition Transition
		{
			set
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("use.the.methods.at.pdfstamper"));
			}
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0008A968 File Offset: 0x00089968
		internal void SetDuration(int seconds, int page)
		{
			PdfDictionary pageN = this.reader.GetPageN(page);
			if (seconds < 0)
			{
				pageN.Remove(PdfName.DUR);
			}
			else
			{
				pageN.Put(PdfName.DUR, new PdfNumber(seconds));
			}
			this.MarkUsed(pageN);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0008A9AC File Offset: 0x000899AC
		internal void SetTransition(PdfTransition transition, int page)
		{
			PdfDictionary pageN = this.reader.GetPageN(page);
			if (transition == null)
			{
				pageN.Remove(PdfName.TRANS);
			}
			else
			{
				pageN.Put(PdfName.TRANS, transition.TransitionDictionary);
			}
			this.MarkUsed(pageN);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0008A9F0 File Offset: 0x000899F0
		protected internal void MarkUsed(PdfObject obj)
		{
			if (this.append && obj != null)
			{
				PRIndirectReference prindirectReference;
				if (obj.Type == 10)
				{
					prindirectReference = (PRIndirectReference)obj;
				}
				else
				{
					prindirectReference = obj.IndRef;
				}
				if (prindirectReference != null)
				{
					this.marked[prindirectReference.Number] = 1;
				}
			}
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0008AA39 File Offset: 0x00089A39
		protected internal void MarkUsed(int num)
		{
			if (this.append)
			{
				this.marked[num] = 1;
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0008AA50 File Offset: 0x00089A50
		internal bool IsAppend()
		{
			return this.append;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0008AA58 File Offset: 0x00089A58
		public override void SetAdditionalAction(PdfName actionType, PdfAction action)
		{
			if (!actionType.Equals(PdfWriter.DOCUMENT_CLOSE) && !actionType.Equals(PdfWriter.WILL_SAVE) && !actionType.Equals(PdfWriter.DID_SAVE) && !actionType.Equals(PdfWriter.WILL_PRINT) && !actionType.Equals(PdfWriter.DID_PRINT))
			{
				throw new PdfException(MessageLocalization.GetComposedMessage("invalid.additional.action.type.1", actionType.ToString()));
			}
			PdfDictionary pdfDictionary = this.reader.Catalog.GetAsDict(PdfName.AA);
			if (pdfDictionary == null)
			{
				if (action == null)
				{
					return;
				}
				pdfDictionary = new PdfDictionary();
				this.reader.Catalog.Put(PdfName.AA, pdfDictionary);
			}
			this.MarkUsed(pdfDictionary);
			if (action == null)
			{
				pdfDictionary.Remove(actionType);
				return;
			}
			pdfDictionary.Put(actionType, action);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0008AB0F File Offset: 0x00089B0F
		public override void SetOpenAction(PdfAction action)
		{
			this.openAction = action;
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0008AB18 File Offset: 0x00089B18
		public override void SetOpenAction(string name)
		{
			throw new InvalidOperationException(MessageLocalization.GetComposedMessage("open.actions.by.name.are.not.supported"));
		}

		// Token: 0x17000464 RID: 1124
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x0008AB29 File Offset: 0x00089B29
		public override Image Thumbnail
		{
			set
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("use.pdfstamper.thumbnail"));
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0008AB3C File Offset: 0x00089B3C
		internal void SetThumbnail(Image image, int page)
		{
			PdfIndirectReference imageReference = this.GetImageReference(base.AddDirectImageSimple(image));
			this.reader.ResetReleasePage();
			PdfDictionary pageN = this.reader.GetPageN(page);
			pageN.Put(PdfName.THUMB, imageReference);
			this.reader.ResetReleasePage();
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0008AB88 File Offset: 0x00089B88
		protected void ReadOCProperties()
		{
			if (this.documentOCG.Count != 0)
			{
				return;
			}
			PdfDictionary asDict = this.reader.Catalog.GetAsDict(PdfName.OCPROPERTIES);
			if (asDict == null)
			{
				return;
			}
			PdfArray asArray = asDict.GetAsArray(PdfName.OCGS);
			Dictionary<string, PdfLayer> dictionary = new Dictionary<string, PdfLayer>();
			ListIterator<PdfObject> listIterator = asArray.GetListIterator();
			while (listIterator.HasNext())
			{
				PdfIndirectReference pdfIndirectReference = (PdfIndirectReference)listIterator.Next();
				PdfLayer pdfLayer = new PdfLayer(null);
				pdfLayer.Ref = pdfIndirectReference;
				pdfLayer.OnPanel = false;
				pdfLayer.Merge((PdfDictionary)PdfReader.GetPdfObject(pdfIndirectReference));
				dictionary[pdfIndirectReference.ToString()] = pdfLayer;
			}
			PdfDictionary asDict2 = asDict.GetAsDict(PdfName.D);
			PdfArray asArray2 = asDict2.GetAsArray(PdfName.OFF);
			if (asArray2 != null)
			{
				ListIterator<PdfObject> listIterator2 = asArray2.GetListIterator();
				while (listIterator2.HasNext())
				{
					PdfIndirectReference pdfIndirectReference = (PdfIndirectReference)listIterator2.Next();
					PdfLayer pdfLayer = dictionary[pdfIndirectReference.ToString()];
					pdfLayer.On = false;
				}
			}
			PdfArray asArray3 = asDict2.GetAsArray(PdfName.ORDER);
			if (asArray3 != null)
			{
				this.AddOrder(null, asArray3, dictionary);
			}
			foreach (PdfLayer key in dictionary.Values)
			{
				this.documentOCG[key] = null;
			}
			this.OCGRadioGroup = asDict2.GetAsArray(PdfName.RBGROUPS);
			this.OCGLocked = asDict2.GetAsArray(PdfName.LOCKED);
			if (this.OCGLocked == null)
			{
				this.OCGLocked = new PdfArray();
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0008AD20 File Offset: 0x00089D20
		private void AddOrder(PdfLayer parent, PdfArray arr, Dictionary<string, PdfLayer> ocgmap)
		{
			for (int i = 0; i < arr.Size; i++)
			{
				PdfObject pdfObject = arr[i];
				if (pdfObject.IsIndirect())
				{
					PdfLayer pdfLayer = ocgmap[pdfObject.ToString()];
					pdfLayer.OnPanel = true;
					base.RegisterLayer(pdfLayer);
					if (parent != null)
					{
						parent.AddChild(pdfLayer);
					}
					if (arr.Size > i + 1 && arr[i + 1].IsArray())
					{
						i++;
						this.AddOrder(pdfLayer, (PdfArray)arr[i], ocgmap);
					}
				}
				else if (pdfObject.IsArray())
				{
					PdfArray pdfArray = (PdfArray)pdfObject;
					if (pdfArray.IsEmpty())
					{
						return;
					}
					pdfObject = pdfArray[0];
					if (pdfObject.IsString())
					{
						PdfLayer pdfLayer = new PdfLayer(pdfObject.ToString());
						pdfLayer.OnPanel = true;
						base.RegisterLayer(pdfLayer);
						if (parent != null)
						{
							parent.AddChild(pdfLayer);
						}
						PdfArray pdfArray2 = new PdfArray();
						ListIterator<PdfObject> listIterator = pdfArray.GetListIterator();
						while (listIterator.HasNext())
						{
							pdfArray2.Add(listIterator.Next());
						}
						this.AddOrder(pdfLayer, pdfArray2, ocgmap);
					}
					else
					{
						this.AddOrder(parent, (PdfArray)pdfObject, ocgmap);
					}
				}
			}
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0008AE4C File Offset: 0x00089E4C
		public Dictionary<string, PdfLayer> GetPdfLayers()
		{
			if (this.documentOCG.Count == 0)
			{
				this.ReadOCProperties();
			}
			Dictionary<string, PdfLayer> dictionary = new Dictionary<string, PdfLayer>();
			foreach (IPdfOCG pdfOCG in this.documentOCG.Keys)
			{
				PdfLayer pdfLayer = (PdfLayer)pdfOCG;
				string text;
				if (pdfLayer.Title == null)
				{
					text = pdfLayer.GetAsString(PdfName.NAME).ToString();
				}
				else
				{
					text = pdfLayer.Title;
				}
				if (dictionary.ContainsKey(text))
				{
					int num = 2;
					string text2 = string.Concat(new object[]
					{
						text,
						"(",
						num,
						")"
					});
					while (dictionary.ContainsKey(text2))
					{
						num++;
						text2 = string.Concat(new object[]
						{
							text,
							"(",
							num,
							")"
						});
					}
					text = text2;
				}
				dictionary[text] = pdfLayer;
			}
			return dictionary;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0008AF70 File Offset: 0x00089F70
		public override PdfContentByte DirectContent
		{
			get
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("use.pdfstamper.getundercontent.or.pdfstamper.getovercontent"));
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0008AF81 File Offset: 0x00089F81
		public override PdfContentByte DirectContentUnder
		{
			get
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("use.pdfstamper.getundercontent.or.pdfstamper.getovercontent"));
			}
		}

		// Token: 0x04001022 RID: 4130
		internal Dictionary<PdfReader, IntHashtable> readers2intrefs = new Dictionary<PdfReader, IntHashtable>();

		// Token: 0x04001023 RID: 4131
		internal Dictionary<PdfReader, RandomAccessFileOrArray> readers2file = new Dictionary<PdfReader, RandomAccessFileOrArray>();

		// Token: 0x04001024 RID: 4132
		internal RandomAccessFileOrArray file;

		// Token: 0x04001025 RID: 4133
		internal PdfReader reader;

		// Token: 0x04001026 RID: 4134
		internal IntHashtable myXref = new IntHashtable();

		// Token: 0x04001027 RID: 4135
		internal Dictionary<PdfDictionary, PdfStamperImp.PageStamp> pagesToContent = new Dictionary<PdfDictionary, PdfStamperImp.PageStamp>();

		// Token: 0x04001028 RID: 4136
		internal bool closed;

		// Token: 0x04001029 RID: 4137
		private bool rotateContents = true;

		// Token: 0x0400102A RID: 4138
		protected AcroFields acroFields;

		// Token: 0x0400102B RID: 4139
		protected bool flat;

		// Token: 0x0400102C RID: 4140
		protected bool flatFreeText;

		// Token: 0x0400102D RID: 4141
		protected int[] namePtr;

		// Token: 0x0400102E RID: 4142
		protected Dictionary<string, object> partialFlattening;

		// Token: 0x0400102F RID: 4143
		protected bool useVp;

		// Token: 0x04001030 RID: 4144
		protected PdfViewerPreferencesImp viewerPreferences;

		// Token: 0x04001031 RID: 4145
		protected Dictionary<PdfTemplate, object> fieldTemplates;

		// Token: 0x04001032 RID: 4146
		protected bool fieldsAdded;

		// Token: 0x04001033 RID: 4147
		protected int sigFlags;

		// Token: 0x04001034 RID: 4148
		protected internal bool append;

		// Token: 0x04001035 RID: 4149
		protected IntHashtable marked;

		// Token: 0x04001036 RID: 4150
		protected int initialXrefSize;

		// Token: 0x04001037 RID: 4151
		protected PdfAction openAction;

		// Token: 0x02000277 RID: 631
		internal class PageStamp
		{
			// Token: 0x06001805 RID: 6149 RVA: 0x0008AF94 File Offset: 0x00089F94
			internal PageStamp(PdfStamperImp stamper, PdfReader reader, PdfDictionary pageN)
			{
				this.pageN = pageN;
				this.pageResources = new PageResources();
				PdfDictionary asDict = pageN.GetAsDict(PdfName.RESOURCES);
				this.pageResources.SetOriginalResources(asDict, stamper.namePtr);
			}

			// Token: 0x04001038 RID: 4152
			internal PdfDictionary pageN;

			// Token: 0x04001039 RID: 4153
			internal StampContent under;

			// Token: 0x0400103A RID: 4154
			internal StampContent over;

			// Token: 0x0400103B RID: 4155
			internal PageResources pageResources;

			// Token: 0x0400103C RID: 4156
			internal int replacePoint;
		}
	}
}
