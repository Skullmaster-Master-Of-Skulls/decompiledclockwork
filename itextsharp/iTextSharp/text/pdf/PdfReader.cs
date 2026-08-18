using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.util;
using System.util.zlib;
using iTextSharp.text.error_messages;
using iTextSharp.text.exceptions;
using iTextSharp.text.pdf.codec;
using iTextSharp.text.pdf.interfaces;
using iTextSharp.text.pdf.intern;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x020001C7 RID: 455
	public class PdfReader : IPdfViewerPreferences
	{
		// Token: 0x06001136 RID: 4406 RVA: 0x000607CD File Offset: 0x0005F7CD
		protected internal PdfReader()
		{
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000607F9 File Offset: 0x0005F7F9
		public PdfReader(string filename) : this(filename, null)
		{
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00060804 File Offset: 0x0005F804
		public PdfReader(string filename, byte[] ownerPassword)
		{
			this.password = ownerPassword;
			this.tokens = new PRTokeniser(filename);
			this.ReadPdf();
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00060854 File Offset: 0x0005F854
		public PdfReader(byte[] pdfIn) : this(pdfIn, null)
		{
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00060860 File Offset: 0x0005F860
		public PdfReader(byte[] pdfIn, byte[] ownerPassword)
		{
			this.password = ownerPassword;
			this.tokens = new PRTokeniser(pdfIn);
			this.ReadPdf();
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000608B0 File Offset: 0x0005F8B0
		public PdfReader(string filename, X509Certificate certificate, ICipherParameters certificateKey)
		{
			this.certificate = certificate;
			this.certificateKey = certificateKey;
			this.tokens = new PRTokeniser(filename);
			this.ReadPdf();
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00060907 File Offset: 0x0005F907
		public PdfReader(Uri url) : this(url, null)
		{
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00060914 File Offset: 0x0005F914
		public PdfReader(Uri url, byte[] ownerPassword)
		{
			this.password = ownerPassword;
			this.tokens = new PRTokeniser(new RandomAccessFileOrArray(url));
			this.ReadPdf();
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0006096C File Offset: 0x0005F96C
		public PdfReader(Stream isp, byte[] ownerPassword)
		{
			this.password = ownerPassword;
			this.tokens = new PRTokeniser(new RandomAccessFileOrArray(isp));
			this.ReadPdf();
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000609C1 File Offset: 0x0005F9C1
		public PdfReader(Stream isp) : this(isp, null)
		{
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000609CC File Offset: 0x0005F9CC
		public PdfReader(RandomAccessFileOrArray raf, byte[] ownerPassword)
		{
			this.password = ownerPassword;
			this.partial = true;
			this.tokens = new PRTokeniser(raf);
			this.ReadPdfPartial();
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00060A24 File Offset: 0x0005FA24
		public PdfReader(PdfReader reader)
		{
			this.appendable = reader.appendable;
			this.consolidateNamedDestinations = reader.consolidateNamedDestinations;
			this.encrypted = reader.encrypted;
			this.rebuilt = reader.rebuilt;
			this.sharedStreams = reader.sharedStreams;
			this.tampered = reader.tampered;
			this.password = reader.password;
			this.pdfVersion = reader.pdfVersion;
			this.eofPos = reader.eofPos;
			this.freeXref = reader.freeXref;
			this.lastXref = reader.lastXref;
			this.tokens = new PRTokeniser(reader.tokens.SafeFile);
			if (reader.decrypt != null)
			{
				this.decrypt = new PdfEncryption(reader.decrypt);
			}
			this.pValue = reader.pValue;
			this.rValue = reader.rValue;
			this.xrefObj = new List<PdfObject>(reader.xrefObj);
			for (int i = 0; i < reader.xrefObj.Count; i++)
			{
				this.xrefObj[i] = PdfReader.DuplicatePdfObject(reader.xrefObj[i], this);
			}
			this.pageRefs = new PdfReader.PageRefs(reader.pageRefs, this);
			this.trailer = (PdfDictionary)PdfReader.DuplicatePdfObject(reader.trailer, this);
			this.catalog = this.trailer.GetAsDict(PdfName.ROOT);
			this.rootPages = this.catalog.GetAsDict(PdfName.PAGES);
			this.fileLength = reader.fileLength;
			this.partial = reader.partial;
			this.hybridXref = reader.hybridXref;
			this.objStmToOffset = reader.objStmToOffset;
			this.xref = reader.xref;
			this.cryptoRef = (PRIndirectReference)PdfReader.DuplicatePdfObject(reader.cryptoRef, this);
			this.ownerPasswordUsed = reader.ownerPasswordUsed;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00060C1F File Offset: 0x0005FC1F
		public RandomAccessFileOrArray SafeFile
		{
			get
			{
				return this.tokens.SafeFile;
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00060C2C File Offset: 0x0005FC2C
		protected internal PdfReaderInstance GetPdfReaderInstance(PdfWriter writer)
		{
			return new PdfReaderInstance(this, writer);
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00060C35 File Offset: 0x0005FC35
		public int NumberOfPages
		{
			get
			{
				return this.pageRefs.Size;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00060C42 File Offset: 0x0005FC42
		public PdfDictionary Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00060C4C File Offset: 0x0005FC4C
		public PRAcroForm AcroForm
		{
			get
			{
				if (!this.acroFormParsed)
				{
					this.acroFormParsed = true;
					PdfObject pdfObject = this.catalog.Get(PdfName.ACROFORM);
					if (pdfObject != null)
					{
						try
						{
							this.acroForm = new PRAcroForm(this);
							this.acroForm.ReadAcroForm((PdfDictionary)PdfReader.GetPdfObject(pdfObject));
						}
						catch
						{
							this.acroForm = null;
						}
					}
				}
				return this.acroForm;
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00060CC0 File Offset: 0x0005FCC0
		public int GetPageRotation(int index)
		{
			return this.GetPageRotation(this.pageRefs.GetPageNRelease(index));
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00060CD4 File Offset: 0x0005FCD4
		internal int GetPageRotation(PdfDictionary page)
		{
			PdfNumber asNumber = page.GetAsNumber(PdfName.ROTATE);
			if (asNumber == null)
			{
				return 0;
			}
			int num = asNumber.IntValue;
			num %= 360;
			if (num >= 0)
			{
				return num;
			}
			return num + 360;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00060D0E File Offset: 0x0005FD0E
		public Rectangle GetPageSizeWithRotation(int index)
		{
			return this.GetPageSizeWithRotation(this.pageRefs.GetPageNRelease(index));
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00060D24 File Offset: 0x0005FD24
		public Rectangle GetPageSizeWithRotation(PdfDictionary page)
		{
			Rectangle rectangle = this.GetPageSize(page);
			for (int i = this.GetPageRotation(page); i > 0; i -= 90)
			{
				rectangle = rectangle.Rotate();
			}
			return rectangle;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00060D54 File Offset: 0x0005FD54
		public Rectangle GetPageSize(int index)
		{
			return this.GetPageSize(this.pageRefs.GetPageNRelease(index));
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00060D68 File Offset: 0x0005FD68
		public Rectangle GetPageSize(PdfDictionary page)
		{
			PdfArray asArray = page.GetAsArray(PdfName.MEDIABOX);
			return PdfReader.GetNormalizedRectangle(asArray);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00060D88 File Offset: 0x0005FD88
		public Rectangle GetCropBox(int index)
		{
			PdfDictionary pageNRelease = this.pageRefs.GetPageNRelease(index);
			PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(pageNRelease.Get(PdfName.CROPBOX));
			if (pdfArray == null)
			{
				return this.GetPageSize(pageNRelease);
			}
			return PdfReader.GetNormalizedRectangle(pdfArray);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00060DCC File Offset: 0x0005FDCC
		public Rectangle GetBoxSize(int index, string boxName)
		{
			PdfDictionary pageNRelease = this.pageRefs.GetPageNRelease(index);
			PdfArray pdfArray = null;
			if (boxName.Equals("trim"))
			{
				pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(pageNRelease.Get(PdfName.TRIMBOX));
			}
			else if (boxName.Equals("art"))
			{
				pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(pageNRelease.Get(PdfName.ARTBOX));
			}
			else if (boxName.Equals("bleed"))
			{
				pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(pageNRelease.Get(PdfName.BLEEDBOX));
			}
			else if (boxName.Equals("crop"))
			{
				pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(pageNRelease.Get(PdfName.CROPBOX));
			}
			else if (boxName.Equals("media"))
			{
				pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(pageNRelease.Get(PdfName.MEDIABOX));
			}
			if (pdfArray == null)
			{
				return null;
			}
			return PdfReader.GetNormalizedRectangle(pdfArray);
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00060EB0 File Offset: 0x0005FEB0
		public Dictionary<string, string> Info
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				PdfDictionary asDict = this.trailer.GetAsDict(PdfName.INFO);
				if (asDict == null)
				{
					return dictionary;
				}
				foreach (PdfName pdfName in asDict.Keys)
				{
					PdfObject pdfObject = PdfReader.GetPdfObject(asDict.Get(pdfName));
					if (pdfObject != null)
					{
						string text = pdfObject.ToString();
						switch (pdfObject.Type)
						{
						case 3:
							text = ((PdfString)pdfObject).ToUnicodeString();
							break;
						case 4:
							text = PdfName.DecodeName(text);
							break;
						}
						dictionary[PdfName.DecodeName(pdfName.ToString())] = text;
					}
				}
				return dictionary;
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00060F78 File Offset: 0x0005FF78
		public static Rectangle GetNormalizedRectangle(PdfArray box)
		{
			float floatValue = ((PdfNumber)PdfReader.GetPdfObjectRelease(box[0])).FloatValue;
			float floatValue2 = ((PdfNumber)PdfReader.GetPdfObjectRelease(box[1])).FloatValue;
			float floatValue3 = ((PdfNumber)PdfReader.GetPdfObjectRelease(box[2])).FloatValue;
			float floatValue4 = ((PdfNumber)PdfReader.GetPdfObjectRelease(box[3])).FloatValue;
			return new Rectangle(Math.Min(floatValue, floatValue3), Math.Min(floatValue2, floatValue4), Math.Max(floatValue, floatValue3), Math.Max(floatValue2, floatValue4));
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00061004 File Offset: 0x00060004
		protected internal virtual void ReadPdf()
		{
			try
			{
				this.fileLength = this.tokens.File.Length;
				this.pdfVersion = this.tokens.CheckPdfHeader();
				try
				{
					this.ReadXref();
				}
				catch (Exception ex)
				{
					try
					{
						this.rebuilt = true;
						this.RebuildXref();
						this.lastXref = -1;
					}
					catch (Exception ex2)
					{
						throw new InvalidPdfException(MessageLocalization.GetComposedMessage("rebuild.failed.1.original.message.2", ex2.Message, ex.Message));
					}
				}
				try
				{
					this.ReadDocObj();
				}
				catch (Exception ex3)
				{
					if (ex3 is BadPasswordException)
					{
						throw new BadPasswordException(ex3.Message);
					}
					if (this.rebuilt || this.encryptionError)
					{
						throw new InvalidPdfException(ex3.Message);
					}
					this.rebuilt = true;
					this.encrypted = false;
					this.RebuildXref();
					this.lastXref = -1;
					this.ReadDocObj();
				}
				this.strings.Clear();
				this.ReadPages();
				this.EliminateSharedStreams();
				this.RemoveUnusedObjects();
			}
			finally
			{
				try
				{
					this.tokens.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00061144 File Offset: 0x00060144
		protected internal void ReadPdfPartial()
		{
			try
			{
				this.fileLength = this.tokens.File.Length;
				this.pdfVersion = this.tokens.CheckPdfHeader();
				try
				{
					this.ReadXref();
				}
				catch (Exception ex)
				{
					try
					{
						this.rebuilt = true;
						this.RebuildXref();
						this.lastXref = -1;
					}
					catch (Exception ex2)
					{
						throw new InvalidPdfException(MessageLocalization.GetComposedMessage("rebuild.failed.1.original.message.2", ex2.Message, ex.Message));
					}
				}
				this.ReadDocObjPartial();
				this.ReadPages();
			}
			catch (IOException ex3)
			{
				try
				{
					this.tokens.Close();
				}
				catch
				{
				}
				throw ex3;
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0006120C File Offset: 0x0006020C
		private bool EqualsArray(byte[] ar1, byte[] ar2, int size)
		{
			for (int i = 0; i < size; i++)
			{
				if (ar1[i] != ar2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00061230 File Offset: 0x00060230
		private void ReadDecryptedDocObj()
		{
			if (this.encrypted)
			{
				return;
			}
			PdfObject pdfObject = this.trailer.Get(PdfName.ENCRYPT);
			if (pdfObject == null || pdfObject.ToString().Equals("null"))
			{
				return;
			}
			this.encryptionError = true;
			byte[] array = null;
			this.encrypted = true;
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(pdfObject);
			PdfArray asArray = this.trailer.GetAsArray(PdfName.ID);
			byte[] array2 = null;
			if (asArray != null)
			{
				PdfObject pdfObject2 = asArray[0];
				this.strings.Remove((PdfString)pdfObject2);
				string text = pdfObject2.ToString();
				array2 = DocWriter.GetISOBytes(text);
				if (asArray.Size > 1)
				{
					this.strings.Remove((PdfString)asArray[1]);
				}
			}
			if (array2 == null)
			{
				array2 = new byte[0];
			}
			byte[] array3 = null;
			byte[] ownerKey = null;
			int num = 0;
			int num2 = 0;
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.FILTER));
			if (pdfObjectRelease.Equals(PdfName.STANDARD))
			{
				string text = pdfDictionary.Get(PdfName.U).ToString();
				this.strings.Remove((PdfString)pdfDictionary.Get(PdfName.U));
				array3 = DocWriter.GetISOBytes(text);
				text = pdfDictionary.Get(PdfName.O).ToString();
				this.strings.Remove((PdfString)pdfDictionary.Get(PdfName.O));
				ownerKey = DocWriter.GetISOBytes(text);
				PdfObject pdfObject2 = pdfDictionary.Get(PdfName.P);
				if (!pdfObject2.IsNumber())
				{
					throw new InvalidPdfException(MessageLocalization.GetComposedMessage("illegal.p.value"));
				}
				this.pValue = ((PdfNumber)pdfObject2).IntValue;
				pdfObject2 = pdfDictionary.Get(PdfName.R);
				if (!pdfObject2.IsNumber())
				{
					throw new InvalidPdfException(MessageLocalization.GetComposedMessage("illegal.r.value"));
				}
				this.rValue = ((PdfNumber)pdfObject2).IntValue;
				switch (this.rValue)
				{
				case 2:
					num = 0;
					break;
				case 3:
					pdfObject2 = pdfDictionary.Get(PdfName.LENGTH);
					if (!pdfObject2.IsNumber())
					{
						throw new InvalidPdfException(MessageLocalization.GetComposedMessage("illegal.length.value"));
					}
					num2 = ((PdfNumber)pdfObject2).IntValue;
					if (num2 > 128 || num2 < 40 || num2 % 8 != 0)
					{
						throw new InvalidPdfException(MessageLocalization.GetComposedMessage("illegal.length.value"));
					}
					num = 1;
					break;
				case 4:
				{
					PdfDictionary pdfDictionary2 = (PdfDictionary)pdfDictionary.Get(PdfName.CF);
					if (pdfDictionary2 == null)
					{
						throw new InvalidPdfException(MessageLocalization.GetComposedMessage("cf.not.found.encryption"));
					}
					pdfDictionary2 = (PdfDictionary)pdfDictionary2.Get(PdfName.STDCF);
					if (pdfDictionary2 == null)
					{
						throw new InvalidPdfException(MessageLocalization.GetComposedMessage("stdcf.not.found.encryption"));
					}
					if (PdfName.V2.Equals(pdfDictionary2.Get(PdfName.CFM)))
					{
						num = 1;
					}
					else
					{
						if (!PdfName.AESV2.Equals(pdfDictionary2.Get(PdfName.CFM)))
						{
							throw new UnsupportedPdfException(MessageLocalization.GetComposedMessage("no.compatible.encryption.found"));
						}
						num = 2;
					}
					PdfObject pdfObject3 = pdfDictionary.Get(PdfName.ENCRYPTMETADATA);
					if (pdfObject3 != null && pdfObject3.ToString().Equals("false"))
					{
						num |= 8;
					}
					break;
				}
				default:
					throw new UnsupportedPdfException(MessageLocalization.GetComposedMessage("unknown.encryption.type.r.eq.1", this.rValue));
				}
			}
			else if (pdfObjectRelease.Equals(PdfName.PUBSEC))
			{
				bool flag = false;
				byte[] array4 = null;
				PdfArray pdfArray = null;
				PdfObject pdfObject2 = pdfDictionary.Get(PdfName.V);
				if (!pdfObject2.IsNumber())
				{
					throw new InvalidPdfException(MessageLocalization.GetComposedMessage("illegal.v.value"));
				}
				switch (((PdfNumber)pdfObject2).IntValue)
				{
				case 1:
					num = 0;
					num2 = 40;
					pdfArray = (PdfArray)pdfDictionary.Get(PdfName.RECIPIENTS);
					goto IL_53D;
				case 2:
					pdfObject2 = pdfDictionary.Get(PdfName.LENGTH);
					if (!pdfObject2.IsNumber())
					{
						throw new InvalidPdfException(MessageLocalization.GetComposedMessage("illegal.length.value"));
					}
					num2 = ((PdfNumber)pdfObject2).IntValue;
					if (num2 > 128 || num2 < 40 || num2 % 8 != 0)
					{
						throw new InvalidPdfException(MessageLocalization.GetComposedMessage("illegal.length.value"));
					}
					num = 1;
					pdfArray = (PdfArray)pdfDictionary.Get(PdfName.RECIPIENTS);
					goto IL_53D;
				case 4:
				{
					PdfDictionary pdfDictionary3 = (PdfDictionary)pdfDictionary.Get(PdfName.CF);
					if (pdfDictionary3 == null)
					{
						throw new InvalidPdfException(MessageLocalization.GetComposedMessage("cf.not.found.encryption"));
					}
					pdfDictionary3 = (PdfDictionary)pdfDictionary3.Get(PdfName.DEFAULTCRYPTFILTER);
					if (pdfDictionary3 == null)
					{
						throw new InvalidPdfException(MessageLocalization.GetComposedMessage("defaultcryptfilter.not.found.encryption"));
					}
					if (PdfName.V2.Equals(pdfDictionary3.Get(PdfName.CFM)))
					{
						num = 1;
						num2 = 128;
					}
					else
					{
						if (!PdfName.AESV2.Equals(pdfDictionary3.Get(PdfName.CFM)))
						{
							throw new UnsupportedPdfException(MessageLocalization.GetComposedMessage("no.compatible.encryption.found"));
						}
						num = 2;
						num2 = 128;
					}
					PdfObject pdfObject4 = pdfDictionary3.Get(PdfName.ENCRYPTMETADATA);
					if (pdfObject4 != null && pdfObject4.ToString().Equals("false"))
					{
						num |= 8;
					}
					pdfArray = (PdfArray)pdfDictionary3.Get(PdfName.RECIPIENTS);
					goto IL_53D;
				}
				}
				throw new UnsupportedPdfException(MessageLocalization.GetComposedMessage("unknown.encryption.type.v.eq.1", this.rValue));
				IL_53D:
				for (int i = 0; i < pdfArray.Size; i++)
				{
					PdfObject pdfObject5 = pdfArray[i];
					if (pdfObject5 is PdfString)
					{
						this.strings.Remove((PdfString)pdfObject5);
					}
					CmsEnvelopedData cmsEnvelopedData = new CmsEnvelopedData(pdfObject5.GetBytes());
					foreach (object obj in cmsEnvelopedData.GetRecipientInfos().GetRecipients())
					{
						RecipientInformation recipientInformation = (RecipientInformation)obj;
						if (recipientInformation.RecipientID.Match(this.certificate) && !flag)
						{
							array4 = recipientInformation.GetContent(this.certificateKey);
							flag = true;
						}
					}
				}
				if (!flag || array4 == null)
				{
					throw new UnsupportedPdfException(MessageLocalization.GetComposedMessage("bad.certificate.and.key"));
				}
				IDigest digest = DigestUtilities.GetDigest("SHA1");
				digest.BlockUpdate(array4, 0, 20);
				for (int j = 0; j < pdfArray.Size; j++)
				{
					byte[] bytes = pdfArray[j].GetBytes();
					digest.BlockUpdate(bytes, 0, bytes.Length);
				}
				if ((num & 8) != 0)
				{
					digest.BlockUpdate(PdfEncryption.metadataPad, 0, PdfEncryption.metadataPad.Length);
				}
				array = new byte[digest.GetDigestSize()];
				digest.DoFinal(array, 0);
			}
			this.decrypt = new PdfEncryption();
			this.decrypt.SetCryptoMode(num, num2);
			if (pdfObjectRelease.Equals(PdfName.STANDARD))
			{
				this.decrypt.SetupByOwnerPassword(array2, this.password, array3, ownerKey, this.pValue);
				if (!this.EqualsArray(array3, this.decrypt.userKey, (this.rValue == 3 || this.rValue == 4) ? 16 : 32))
				{
					this.decrypt.SetupByUserPassword(array2, this.password, ownerKey, this.pValue);
					if (!this.EqualsArray(array3, this.decrypt.userKey, (this.rValue == 3 || this.rValue == 4) ? 16 : 32))
					{
						throw new BadPasswordException(MessageLocalization.GetComposedMessage("bad.user.password"));
					}
				}
				else
				{
					this.ownerPasswordUsed = true;
				}
			}
			else if (pdfObjectRelease.Equals(PdfName.PUBSEC))
			{
				this.decrypt.SetupByEncryptionKey(array, num2);
				this.ownerPasswordUsed = true;
			}
			for (int k = 0; k < this.strings.Count; k++)
			{
				PdfString pdfString = this.strings[k];
				pdfString.Decrypt(this);
			}
			if (pdfObject.IsIndirect())
			{
				this.cryptoRef = (PRIndirectReference)pdfObject;
				this.xrefObj[this.cryptoRef.Number] = null;
			}
			this.encryptionError = false;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00061A3C File Offset: 0x00060A3C
		public static PdfObject GetPdfObjectRelease(PdfObject obj)
		{
			PdfObject pdfObject = PdfReader.GetPdfObject(obj);
			PdfReader.ReleaseLastXrefPartial(obj);
			return pdfObject;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00061A58 File Offset: 0x00060A58
		public static PdfObject GetPdfObject(PdfObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (!obj.IsIndirect())
			{
				return obj;
			}
			PRIndirectReference prindirectReference = (PRIndirectReference)obj;
			int number = prindirectReference.Number;
			bool flag = prindirectReference.Reader.appendable;
			obj = prindirectReference.Reader.GetPdfObject(number);
			if (obj == null)
			{
				return null;
			}
			if (flag)
			{
				int type = obj.Type;
				if (type != 1)
				{
					if (type != 4)
					{
						if (type == 8)
						{
							obj = new PdfNull();
						}
					}
					else
					{
						obj = new PdfName(obj.GetBytes());
					}
				}
				else
				{
					obj = new PdfBoolean(((PdfBoolean)obj).BooleanValue);
				}
				obj.IndRef = prindirectReference;
			}
			return obj;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00061AEC File Offset: 0x00060AEC
		public static PdfObject GetPdfObjectRelease(PdfObject obj, PdfObject parent)
		{
			PdfObject pdfObject = PdfReader.GetPdfObject(obj, parent);
			PdfReader.ReleaseLastXrefPartial(obj);
			return pdfObject;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00061B08 File Offset: 0x00060B08
		public static PdfObject GetPdfObject(PdfObject obj, PdfObject parent)
		{
			if (obj == null)
			{
				return null;
			}
			if (!obj.IsIndirect())
			{
				PRIndirectReference indRef;
				if (parent != null && (indRef = parent.IndRef) != null && indRef.Reader.Appendable)
				{
					int type = obj.Type;
					if (type != 1)
					{
						if (type != 4)
						{
							if (type == 8)
							{
								obj = new PdfNull();
							}
						}
						else
						{
							obj = new PdfName(obj.GetBytes());
						}
					}
					else
					{
						obj = new PdfBoolean(((PdfBoolean)obj).BooleanValue);
					}
					obj.IndRef = indRef;
				}
				return obj;
			}
			return PdfReader.GetPdfObject(obj);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00061B8C File Offset: 0x00060B8C
		public PdfObject GetPdfObjectRelease(int idx)
		{
			PdfObject pdfObject = this.GetPdfObject(idx);
			this.ReleaseLastXrefPartial();
			return pdfObject;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00061BA8 File Offset: 0x00060BA8
		public PdfObject GetPdfObject(int idx)
		{
			this.lastXrefPartial = -1;
			if (idx < 0 || idx >= this.xrefObj.Count)
			{
				return null;
			}
			PdfObject pdfObject = this.xrefObj[idx];
			if (!this.partial || pdfObject != null)
			{
				return pdfObject;
			}
			if (idx * 2 >= this.xref.Length)
			{
				return null;
			}
			pdfObject = this.ReadSingleObject(idx);
			this.lastXrefPartial = -1;
			if (pdfObject != null)
			{
				this.lastXrefPartial = idx;
			}
			return pdfObject;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00061C13 File Offset: 0x00060C13
		public void ResetLastXrefPartial()
		{
			this.lastXrefPartial = -1;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00061C1C File Offset: 0x00060C1C
		public void ReleaseLastXrefPartial()
		{
			if (this.partial && this.lastXrefPartial != -1)
			{
				this.xrefObj[this.lastXrefPartial] = null;
				this.lastXrefPartial = -1;
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00061C48 File Offset: 0x00060C48
		public static void ReleaseLastXrefPartial(PdfObject obj)
		{
			if (obj == null)
			{
				return;
			}
			if (!obj.IsIndirect())
			{
				return;
			}
			if (!(obj is PRIndirectReference))
			{
				return;
			}
			PRIndirectReference prindirectReference = (PRIndirectReference)obj;
			PdfReader reader = prindirectReference.Reader;
			if (reader.partial && reader.lastXrefPartial != -1 && reader.lastXrefPartial == prindirectReference.Number)
			{
				reader.xrefObj[reader.lastXrefPartial] = null;
			}
			reader.lastXrefPartial = -1;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00061CB1 File Offset: 0x00060CB1
		private void SetXrefPartialObject(int idx, PdfObject obj)
		{
			if (!this.partial || idx < 0)
			{
				return;
			}
			this.xrefObj[idx] = obj;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00061CCD File Offset: 0x00060CCD
		public PRIndirectReference AddPdfObject(PdfObject obj)
		{
			this.xrefObj.Add(obj);
			return new PRIndirectReference(this, this.xrefObj.Count - 1);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00061CEE File Offset: 0x00060CEE
		protected internal void ReadPages()
		{
			this.catalog = this.trailer.GetAsDict(PdfName.ROOT);
			this.rootPages = this.catalog.GetAsDict(PdfName.PAGES);
			this.pageRefs = new PdfReader.PageRefs(this);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00061D28 File Offset: 0x00060D28
		protected internal void ReadDocObjPartial()
		{
			this.xrefObj = new List<PdfObject>(this.xref.Length / 2);
			for (int i = 0; i < this.xref.Length / 2; i++)
			{
				this.xrefObj.Add(null);
			}
			this.ReadDecryptedDocObj();
			if (this.objStmToOffset != null)
			{
				foreach (int num in this.objStmToOffset.GetKeys())
				{
					this.objStmToOffset[num] = this.xref[num * 2];
					this.xref[num * 2] = -1;
				}
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00061DB8 File Offset: 0x00060DB8
		protected internal PdfObject ReadSingleObject(int k)
		{
			this.strings.Clear();
			int num = k * 2;
			int num2 = this.xref[num];
			if (num2 < 0)
			{
				return null;
			}
			if (this.xref[num + 1] > 0)
			{
				num2 = this.objStmToOffset[this.xref[num + 1]];
			}
			if (num2 == 0)
			{
				return null;
			}
			this.tokens.Seek(num2);
			this.tokens.NextValidToken();
			if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
			{
				this.tokens.ThrowError(MessageLocalization.GetComposedMessage("invalid.object.number"));
			}
			this.objNum = this.tokens.IntValue;
			this.tokens.NextValidToken();
			if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
			{
				this.tokens.ThrowError(MessageLocalization.GetComposedMessage("invalid.generation.number"));
			}
			this.objGen = this.tokens.IntValue;
			this.tokens.NextValidToken();
			if (!this.tokens.StringValue.Equals("obj"))
			{
				this.tokens.ThrowError(MessageLocalization.GetComposedMessage("token.obj.expected"));
			}
			PdfObject pdfObject;
			try
			{
				pdfObject = this.ReadPRObject();
				for (int i = 0; i < this.strings.Count; i++)
				{
					PdfString pdfString = this.strings[i];
					pdfString.Decrypt(this);
				}
				if (pdfObject.IsStream())
				{
					this.CheckPRStreamLength((PRStream)pdfObject);
				}
			}
			catch
			{
				pdfObject = null;
			}
			if (this.xref[num + 1] > 0)
			{
				pdfObject = this.ReadOneObjStm((PRStream)pdfObject, this.xref[num]);
			}
			this.xrefObj[k] = pdfObject;
			return pdfObject;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00061F5C File Offset: 0x00060F5C
		protected internal PdfObject ReadOneObjStm(PRStream stream, int idx)
		{
			int intValue = stream.GetAsNumber(PdfName.FIRST).IntValue;
			byte[] streamBytes = PdfReader.GetStreamBytes(stream, this.tokens.File);
			PRTokeniser prtokeniser = this.tokens;
			this.tokens = new PRTokeniser(streamBytes);
			PdfObject result;
			try
			{
				int pos = 0;
				bool flag = true;
				idx++;
				for (int i = 0; i < idx; i++)
				{
					flag = this.tokens.NextToken();
					if (!flag)
					{
						break;
					}
					if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
					{
						flag = false;
						break;
					}
					flag = this.tokens.NextToken();
					if (!flag)
					{
						break;
					}
					if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
					{
						flag = false;
						break;
					}
					pos = this.tokens.IntValue + intValue;
				}
				if (!flag)
				{
					throw new InvalidPdfException(MessageLocalization.GetComposedMessage("error.reading.objstm"));
				}
				this.tokens.Seek(pos);
				result = this.ReadPRObject();
			}
			finally
			{
				this.tokens = prtokeniser;
			}
			return result;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00062054 File Offset: 0x00061054
		public double DumpPerc()
		{
			int num = 0;
			for (int i = 0; i < this.xrefObj.Count; i++)
			{
				if (this.xrefObj[i] != null)
				{
					num++;
				}
			}
			return (double)num * 100.0 / (double)this.xrefObj.Count;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x000620A4 File Offset: 0x000610A4
		protected internal void ReadDocObj()
		{
			List<PRStream> list = new List<PRStream>();
			this.xrefObj = new List<PdfObject>(this.xref.Length / 2);
			for (int i = 0; i < this.xref.Length / 2; i++)
			{
				this.xrefObj.Add(null);
			}
			for (int j = 2; j < this.xref.Length; j += 2)
			{
				int num = this.xref[j];
				if (num > 0 && this.xref[j + 1] <= 0)
				{
					this.tokens.Seek(num);
					this.tokens.NextValidToken();
					if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
					{
						this.tokens.ThrowError(MessageLocalization.GetComposedMessage("invalid.object.number"));
					}
					this.objNum = this.tokens.IntValue;
					this.tokens.NextValidToken();
					if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
					{
						this.tokens.ThrowError(MessageLocalization.GetComposedMessage("invalid.generation.number"));
					}
					this.objGen = this.tokens.IntValue;
					this.tokens.NextValidToken();
					if (!this.tokens.StringValue.Equals("obj"))
					{
						this.tokens.ThrowError(MessageLocalization.GetComposedMessage("token.obj.expected"));
					}
					PdfObject pdfObject;
					try
					{
						pdfObject = this.ReadPRObject();
						if (pdfObject.IsStream())
						{
							list.Add((PRStream)pdfObject);
						}
					}
					catch
					{
						pdfObject = null;
					}
					this.xrefObj[j / 2] = pdfObject;
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				this.CheckPRStreamLength(list[k]);
			}
			this.ReadDecryptedDocObj();
			if (this.objStmMark != null)
			{
				foreach (KeyValuePair<int, IntHashtable> keyValuePair in this.objStmMark)
				{
					int key = keyValuePair.Key;
					IntHashtable value = keyValuePair.Value;
					this.ReadObjStm((PRStream)this.xrefObj[key], value);
					this.xrefObj[key] = null;
				}
				this.objStmMark = null;
			}
			this.xref = null;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x000622E4 File Offset: 0x000612E4
		private void CheckPRStreamLength(PRStream stream)
		{
			int length = this.tokens.Length;
			int offset = stream.Offset;
			bool flag = false;
			int num = 0;
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(stream.Get(PdfName.LENGTH));
			if (pdfObjectRelease != null && pdfObjectRelease.Type == 2)
			{
				num = ((PdfNumber)pdfObjectRelease).IntValue;
				if (num + offset > length - 20)
				{
					flag = true;
				}
				else
				{
					this.tokens.Seek(offset + num);
					string text = this.tokens.ReadString(20);
					if (!text.StartsWith("\nendstream") && !text.StartsWith("\r\nendstream") && !text.StartsWith("\rendstream") && !text.StartsWith("endstream"))
					{
						flag = true;
					}
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				byte[] array = new byte[16];
				this.tokens.Seek(offset);
				int num2;
				for (;;)
				{
					num2 = this.tokens.FilePointer;
					if (!this.tokens.ReadLineSegment(array))
					{
						goto IL_147;
					}
					if (PdfReader.Equalsn(array, PdfReader.endstream))
					{
						break;
					}
					if (PdfReader.Equalsn(array, PdfReader.endobj))
					{
						goto Block_11;
					}
				}
				num = num2 - offset;
				goto IL_147;
				Block_11:
				this.tokens.Seek(num2 - 16);
				string text2 = this.tokens.ReadString(16);
				int num3 = text2.IndexOf("endstream");
				if (num3 >= 0)
				{
					num2 = num2 - 16 + num3;
				}
				num = num2 - offset;
			}
			IL_147:
			stream.Length = num;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00062440 File Offset: 0x00061440
		protected internal void ReadObjStm(PRStream stream, IntHashtable map)
		{
			int intValue = stream.GetAsNumber(PdfName.FIRST).IntValue;
			int intValue2 = stream.GetAsNumber(PdfName.N).IntValue;
			byte[] streamBytes = PdfReader.GetStreamBytes(stream, this.tokens.File);
			PRTokeniser prtokeniser = this.tokens;
			this.tokens = new PRTokeniser(streamBytes);
			try
			{
				int[] array = new int[intValue2];
				int[] array2 = new int[intValue2];
				bool flag = true;
				for (int i = 0; i < intValue2; i++)
				{
					flag = this.tokens.NextToken();
					if (!flag)
					{
						break;
					}
					if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
					{
						flag = false;
						break;
					}
					array2[i] = this.tokens.IntValue;
					flag = this.tokens.NextToken();
					if (!flag)
					{
						break;
					}
					if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
					{
						flag = false;
						break;
					}
					array[i] = this.tokens.IntValue + intValue;
				}
				if (!flag)
				{
					throw new InvalidPdfException(MessageLocalization.GetComposedMessage("error.reading.objstm"));
				}
				for (int j = 0; j < intValue2; j++)
				{
					if (map.ContainsKey(j))
					{
						this.tokens.Seek(array[j]);
						this.tokens.NextToken();
						PdfObject value;
						if (this.tokens.TokenType == PRTokeniser.TokType.NUMBER)
						{
							value = new PdfNumber(this.tokens.StringValue);
						}
						else
						{
							this.tokens.Seek(array[j]);
							value = this.ReadPRObject();
						}
						this.xrefObj[array2[j]] = value;
					}
				}
			}
			finally
			{
				this.tokens = prtokeniser;
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000625E0 File Offset: 0x000615E0
		public static PdfObject KillIndirect(PdfObject obj)
		{
			if (obj == null || obj.IsNull())
			{
				return null;
			}
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(obj);
			if (obj.IsIndirect())
			{
				PRIndirectReference prindirectReference = (PRIndirectReference)obj;
				PdfReader reader = prindirectReference.Reader;
				int number = prindirectReference.Number;
				reader.xrefObj[number] = null;
				if (reader.partial)
				{
					reader.xref[number * 2] = -1;
				}
			}
			return pdfObjectRelease;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00062640 File Offset: 0x00061640
		private void EnsureXrefSize(int size)
		{
			if (size == 0)
			{
				return;
			}
			if (this.xref == null)
			{
				this.xref = new int[size];
				return;
			}
			if (this.xref.Length < size)
			{
				int[] destinationArray = new int[size];
				Array.Copy(this.xref, 0, destinationArray, 0, this.xref.Length);
				this.xref = destinationArray;
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00062698 File Offset: 0x00061698
		protected internal void ReadXref()
		{
			this.hybridXref = false;
			this.newXrefType = false;
			this.tokens.Seek(this.tokens.Startxref);
			this.tokens.NextToken();
			if (!this.tokens.StringValue.Equals("startxref"))
			{
				throw new InvalidPdfException(MessageLocalization.GetComposedMessage("startxref.not.found"));
			}
			this.tokens.NextToken();
			if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
			{
				throw new InvalidPdfException(MessageLocalization.GetComposedMessage("startxref.is.not.followed.by.a.number"));
			}
			int intValue = this.tokens.IntValue;
			this.lastXref = intValue;
			this.eofPos = this.tokens.FilePointer;
			try
			{
				if (this.ReadXRefStream(intValue))
				{
					this.newXrefType = true;
					return;
				}
			}
			catch
			{
			}
			this.xref = null;
			this.tokens.Seek(intValue);
			this.trailer = this.ReadXrefSection();
			PdfDictionary pdfDictionary = this.trailer;
			for (;;)
			{
				PdfNumber pdfNumber = (PdfNumber)pdfDictionary.Get(PdfName.PREV);
				if (pdfNumber == null)
				{
					break;
				}
				this.tokens.Seek(pdfNumber.IntValue);
				pdfDictionary = this.ReadXrefSection();
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000627C8 File Offset: 0x000617C8
		protected internal PdfDictionary ReadXrefSection()
		{
			this.tokens.NextValidToken();
			if (!this.tokens.StringValue.Equals("xref"))
			{
				this.tokens.ThrowError(MessageLocalization.GetComposedMessage("xref.subsection.not.found"));
			}
			for (;;)
			{
				this.tokens.NextValidToken();
				if (this.tokens.StringValue.Equals("trailer"))
				{
					break;
				}
				if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
				{
					this.tokens.ThrowError(MessageLocalization.GetComposedMessage("object.number.of.the.first.object.in.this.xref.subsection.not.found"));
				}
				int num = this.tokens.IntValue;
				this.tokens.NextValidToken();
				if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
				{
					this.tokens.ThrowError(MessageLocalization.GetComposedMessage("number.of.entries.in.this.xref.subsection.not.found"));
				}
				int num2 = this.tokens.IntValue + num;
				if (num == 1)
				{
					int filePointer = this.tokens.FilePointer;
					this.tokens.NextValidToken();
					int intValue = this.tokens.IntValue;
					this.tokens.NextValidToken();
					int intValue2 = this.tokens.IntValue;
					if (intValue == 0 && intValue2 == 65535)
					{
						num--;
						num2--;
					}
					this.tokens.Seek(filePointer);
				}
				this.EnsureXrefSize(num2 * 2);
				for (int i = num; i < num2; i++)
				{
					this.tokens.NextValidToken();
					int intValue = this.tokens.IntValue;
					this.tokens.NextValidToken();
					int intValue2 = this.tokens.IntValue;
					this.tokens.NextValidToken();
					int num3 = i * 2;
					if (this.tokens.StringValue.Equals("n"))
					{
						if (this.xref[num3] == 0 && this.xref[num3 + 1] == 0)
						{
							this.xref[num3] = intValue;
						}
					}
					else if (this.tokens.StringValue.Equals("f"))
					{
						if (this.xref[num3] == 0 && this.xref[num3 + 1] == 0)
						{
							this.xref[num3] = -1;
						}
					}
					else
					{
						this.tokens.ThrowError(MessageLocalization.GetComposedMessage("invalid.cross.reference.entry.in.this.xref.subsection"));
					}
				}
			}
			PdfDictionary pdfDictionary = (PdfDictionary)this.ReadPRObject();
			PdfNumber pdfNumber = (PdfNumber)pdfDictionary.Get(PdfName.SIZE);
			this.EnsureXrefSize(pdfNumber.IntValue * 2);
			PdfObject pdfObject = pdfDictionary.Get(PdfName.XREFSTM);
			if (pdfObject != null && pdfObject.IsNumber())
			{
				int intValue3 = ((PdfNumber)pdfObject).IntValue;
				try
				{
					this.ReadXRefStream(intValue3);
					this.newXrefType = true;
					this.hybridXref = true;
				}
				catch (IOException ex)
				{
					this.xref = null;
					throw ex;
				}
			}
			return pdfDictionary;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00062A80 File Offset: 0x00061A80
		protected internal bool ReadXRefStream(int ptr)
		{
			this.tokens.Seek(ptr);
			if (!this.tokens.NextToken())
			{
				return false;
			}
			if (this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
			{
				return false;
			}
			int num = this.tokens.IntValue;
			if (!this.tokens.NextToken() || this.tokens.TokenType != PRTokeniser.TokType.NUMBER)
			{
				return false;
			}
			if (!this.tokens.NextToken() || !this.tokens.StringValue.Equals("obj"))
			{
				return false;
			}
			PdfObject pdfObject = this.ReadPRObject();
			if (!pdfObject.IsStream())
			{
				return false;
			}
			PRStream prstream = (PRStream)pdfObject;
			if (!PdfName.XREF.Equals(prstream.Get(PdfName.TYPE)))
			{
				return false;
			}
			if (this.trailer == null)
			{
				this.trailer = new PdfDictionary();
				this.trailer.Merge(prstream);
			}
			prstream.Length = ((PdfNumber)prstream.Get(PdfName.LENGTH)).IntValue;
			int intValue = ((PdfNumber)prstream.Get(PdfName.SIZE)).IntValue;
			PdfObject pdfObject2 = prstream.Get(PdfName.INDEX);
			PdfArray pdfArray;
			if (pdfObject2 == null)
			{
				pdfArray = new PdfArray();
				pdfArray.Add(new int[]
				{
					0,
					intValue
				});
			}
			else
			{
				pdfArray = (PdfArray)pdfObject2;
			}
			PdfArray pdfArray2 = (PdfArray)prstream.Get(PdfName.W);
			int num2 = -1;
			pdfObject2 = prstream.Get(PdfName.PREV);
			if (pdfObject2 != null)
			{
				num2 = ((PdfNumber)pdfObject2).IntValue;
			}
			this.EnsureXrefSize(intValue * 2);
			if (this.objStmMark == null && !this.partial)
			{
				this.objStmMark = new Dictionary<int, IntHashtable>();
			}
			if (this.objStmToOffset == null && this.partial)
			{
				this.objStmToOffset = new IntHashtable();
			}
			byte[] streamBytes = PdfReader.GetStreamBytes(prstream, this.tokens.File);
			int num3 = 0;
			int[] array = new int[3];
			for (int i = 0; i < 3; i++)
			{
				array[i] = pdfArray2.GetAsNumber(i).IntValue;
			}
			for (int j = 0; j < pdfArray.Size; j += 2)
			{
				int num4 = pdfArray.GetAsNumber(j).IntValue;
				int intValue2 = pdfArray.GetAsNumber(j + 1).IntValue;
				this.EnsureXrefSize((num4 + intValue2) * 2);
				while (intValue2-- > 0)
				{
					int num5 = 1;
					if (array[0] > 0)
					{
						num5 = 0;
						for (int k = 0; k < array[0]; k++)
						{
							num5 = (num5 << 8) + (int)(streamBytes[num3++] & byte.MaxValue);
						}
					}
					int num6 = 0;
					for (int l = 0; l < array[1]; l++)
					{
						num6 = (num6 << 8) + (int)(streamBytes[num3++] & byte.MaxValue);
					}
					int num7 = 0;
					for (int m = 0; m < array[2]; m++)
					{
						num7 = (num7 << 8) + (int)(streamBytes[num3++] & byte.MaxValue);
					}
					int num8 = num4 * 2;
					if (this.xref[num8] == 0 && this.xref[num8 + 1] == 0)
					{
						switch (num5)
						{
						case 0:
							this.xref[num8] = -1;
							break;
						case 1:
							this.xref[num8] = num6;
							break;
						case 2:
						{
							this.xref[num8] = num7;
							this.xref[num8 + 1] = num6;
							IntHashtable intHashtable;
							if (this.partial)
							{
								this.objStmToOffset[num6] = 0;
							}
							else if (!this.objStmMark.TryGetValue(num6, out intHashtable))
							{
								intHashtable = new IntHashtable();
								intHashtable[num7] = 1;
								this.objStmMark[num6] = intHashtable;
							}
							else
							{
								intHashtable[num7] = 1;
							}
							break;
						}
						}
					}
					num4++;
				}
			}
			num *= 2;
			if (num < this.xref.Length)
			{
				this.xref[num] = -1;
			}
			return num2 == -1 || this.ReadXRefStream(num2);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00062E5C File Offset: 0x00061E5C
		protected internal void RebuildXref()
		{
			this.hybridXref = false;
			this.newXrefType = false;
			this.tokens.Seek(0);
			int[][] array = new int[1024][];
			int num = 0;
			this.trailer = null;
			byte[] array2 = new byte[64];
			for (;;)
			{
				int filePointer = this.tokens.FilePointer;
				if (!this.tokens.ReadLineSegment(array2))
				{
					break;
				}
				if (array2[0] == 116)
				{
					if (!PdfEncodings.ConvertToString(array2, null).StartsWith("trailer"))
					{
						continue;
					}
					this.tokens.Seek(filePointer);
					this.tokens.NextToken();
					filePointer = this.tokens.FilePointer;
					try
					{
						PdfDictionary pdfDictionary = (PdfDictionary)this.ReadPRObject();
						if (pdfDictionary.Get(PdfName.ROOT) != null)
						{
							this.trailer = pdfDictionary;
						}
						else
						{
							this.tokens.Seek(filePointer);
						}
						continue;
					}
					catch
					{
						this.tokens.Seek(filePointer);
						continue;
					}
				}
				if (array2[0] >= 48 && array2[0] <= 57)
				{
					int[] array3 = PRTokeniser.CheckObjectStart(array2);
					if (array3 != null)
					{
						int num2 = array3[0];
						int num3 = array3[1];
						if (num2 >= array.Length)
						{
							int num4 = num2 * 2;
							int[][] array4 = new int[num4][];
							Array.Copy(array, 0, array4, 0, num);
							array = array4;
						}
						if (num2 >= num)
						{
							num = num2 + 1;
						}
						if (array[num2] == null || num3 >= array[num2][1])
						{
							array3[0] = filePointer;
							array[num2] = array3;
						}
					}
				}
			}
			if (this.trailer == null)
			{
				throw new InvalidPdfException(MessageLocalization.GetComposedMessage("trailer.not.found"));
			}
			this.xref = new int[num * 2];
			for (int i = 0; i < num; i++)
			{
				int[] array5 = array[i];
				if (array5 != null)
				{
					this.xref[i * 2] = array5[0];
				}
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00063024 File Offset: 0x00062024
		protected internal PdfDictionary ReadDictionary()
		{
			PdfDictionary pdfDictionary = new PdfDictionary();
			for (;;)
			{
				this.tokens.NextValidToken();
				if (this.tokens.TokenType == PRTokeniser.TokType.END_DIC)
				{
					break;
				}
				if (this.tokens.TokenType != PRTokeniser.TokType.NAME)
				{
					this.tokens.ThrowError(MessageLocalization.GetComposedMessage("dictionary.key.is.not.a.name"));
				}
				PdfName key = new PdfName(this.tokens.StringValue, false);
				PdfObject pdfObject = this.ReadPRObject();
				int type = pdfObject.Type;
				if (-type == 8)
				{
					this.tokens.ThrowError(MessageLocalization.GetComposedMessage("unexpected.gt.gt"));
				}
				if (-type == 6)
				{
					this.tokens.ThrowError(MessageLocalization.GetComposedMessage("unexpected.close.bracket"));
				}
				pdfDictionary.Put(key, pdfObject);
			}
			return pdfDictionary;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000630D8 File Offset: 0x000620D8
		protected internal PdfArray ReadArray()
		{
			PdfArray pdfArray = new PdfArray();
			for (;;)
			{
				PdfObject pdfObject = this.ReadPRObject();
				int type = pdfObject.Type;
				if (-type == 6)
				{
					break;
				}
				if (-type == 8)
				{
					this.tokens.ThrowError(MessageLocalization.GetComposedMessage("unexpected.gt.gt"));
				}
				pdfArray.Add(pdfObject);
			}
			return pdfArray;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00063124 File Offset: 0x00062124
		protected internal PdfObject ReadPRObject()
		{
			this.tokens.NextValidToken();
			PRTokeniser.TokType tokenType = this.tokens.TokenType;
			switch (tokenType)
			{
			case PRTokeniser.TokType.NUMBER:
				return new PdfNumber(this.tokens.StringValue);
			case PRTokeniser.TokType.STRING:
			{
				PdfString pdfString = new PdfString(this.tokens.StringValue, null).SetHexWriting(this.tokens.IsHexString());
				pdfString.SetObjNum(this.objNum, this.objGen);
				if (this.strings != null)
				{
					this.strings.Add(pdfString);
				}
				return pdfString;
			}
			case PRTokeniser.TokType.NAME:
			{
				PdfName pdfName;
				PdfName.staticNames.TryGetValue(this.tokens.StringValue, out pdfName);
				if (this.readDepth > 0 && pdfName != null)
				{
					return pdfName;
				}
				return new PdfName(this.tokens.StringValue, false);
			}
			case PRTokeniser.TokType.START_ARRAY:
			{
				this.readDepth++;
				PdfArray result = this.ReadArray();
				this.readDepth--;
				return result;
			}
			case PRTokeniser.TokType.START_DIC:
			{
				this.readDepth++;
				PdfDictionary pdfDictionary = this.ReadDictionary();
				this.readDepth--;
				int filePointer = this.tokens.FilePointer;
				bool flag;
				do
				{
					flag = this.tokens.NextToken();
				}
				while (flag && this.tokens.TokenType == PRTokeniser.TokType.COMMENT);
				if (flag && this.tokens.StringValue.Equals("stream"))
				{
					int num;
					do
					{
						num = this.tokens.Read();
					}
					while (num == 32 || num == 9 || num == 0 || num == 12);
					if (num != 10)
					{
						num = this.tokens.Read();
					}
					if (num != 10)
					{
						this.tokens.BackOnePosition(num);
					}
					PRStream prstream = new PRStream(this, this.tokens.FilePointer);
					prstream.Merge(pdfDictionary);
					prstream.ObjNum = this.objNum;
					prstream.ObjGen = this.objGen;
					return prstream;
				}
				this.tokens.Seek(filePointer);
				return pdfDictionary;
			}
			case PRTokeniser.TokType.REF:
			{
				int reference = this.tokens.Reference;
				return new PRIndirectReference(this, reference, this.tokens.Generation);
			}
			case PRTokeniser.TokType.ENDOFFILE:
				throw new IOException(MessageLocalization.GetComposedMessage("unexpected.end.of.file"));
			}
			string stringValue = this.tokens.StringValue;
			if ("null".Equals(stringValue))
			{
				if (this.readDepth == 0)
				{
					return new PdfNull();
				}
				return PdfNull.PDFNULL;
			}
			else if ("true".Equals(stringValue))
			{
				if (this.readDepth == 0)
				{
					return new PdfBoolean(true);
				}
				return PdfBoolean.PDFTRUE;
			}
			else
			{
				if (!"false".Equals(stringValue))
				{
					return new PdfLiteral((int)(-(int)tokenType), this.tokens.StringValue);
				}
				if (this.readDepth == 0)
				{
					return new PdfBoolean(false);
				}
				return PdfBoolean.PDFFALSE;
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000633FC File Offset: 0x000623FC
		public static byte[] FlateDecode(byte[] inp)
		{
			byte[] array = PdfReader.FlateDecode(inp, true);
			if (array == null)
			{
				return PdfReader.FlateDecode(inp, false);
			}
			return array;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00063420 File Offset: 0x00062420
		public static byte[] DecodePredictor(byte[] inp, PdfObject dicPar)
		{
			if (dicPar == null || !dicPar.IsDictionary())
			{
				return inp;
			}
			PdfDictionary pdfDictionary = (PdfDictionary)dicPar;
			PdfObject pdfObject = PdfReader.GetPdfObject(pdfDictionary.Get(PdfName.PREDICTOR));
			if (pdfObject == null || !pdfObject.IsNumber())
			{
				return inp;
			}
			int intValue = ((PdfNumber)pdfObject).IntValue;
			if (intValue < 10)
			{
				return inp;
			}
			int num = 1;
			pdfObject = PdfReader.GetPdfObject(pdfDictionary.Get(PdfName.COLUMNS));
			if (pdfObject != null && pdfObject.IsNumber())
			{
				num = ((PdfNumber)pdfObject).IntValue;
			}
			int num2 = 1;
			pdfObject = PdfReader.GetPdfObject(pdfDictionary.Get(PdfName.COLORS));
			if (pdfObject != null && pdfObject.IsNumber())
			{
				num2 = ((PdfNumber)pdfObject).IntValue;
			}
			int num3 = 8;
			pdfObject = PdfReader.GetPdfObject(pdfDictionary.Get(PdfName.BITSPERCOMPONENT));
			if (pdfObject != null && pdfObject.IsNumber())
			{
				num3 = ((PdfNumber)pdfObject).IntValue;
			}
			MemoryStream memoryStream = new MemoryStream(inp);
			MemoryStream memoryStream2 = new MemoryStream(inp.Length);
			int num4 = num2 * num3 / 8;
			int num5 = (num2 * num * num3 + 7) / 8;
			byte[] array = new byte[num5];
			byte[] array2 = new byte[num5];
			for (;;)
			{
				int num6 = 0;
				try
				{
					num6 = memoryStream.ReadByte();
					if (num6 < 0)
					{
						return memoryStream2.ToArray();
					}
					int num7;
					for (int i = 0; i < num5; i += num7)
					{
						num7 = memoryStream.Read(array, i, num5 - i);
						if (num7 <= 0)
						{
							return memoryStream2.ToArray();
						}
					}
				}
				catch
				{
					return memoryStream2.ToArray();
				}
				switch (num6)
				{
				case 0:
					goto IL_357;
				case 1:
					for (int j = num4; j < num5; j++)
					{
						byte[] array3 = array;
						int num8 = j;
						array3[num8] += array[j - num4];
					}
					goto IL_357;
				case 2:
					for (int k = 0; k < num5; k++)
					{
						byte[] array4 = array;
						int num9 = k;
						array4[num9] += array2[k];
					}
					goto IL_357;
				case 3:
					for (int l = 0; l < num4; l++)
					{
						byte[] array5 = array;
						int num10 = l;
						array5[num10] += array2[l] / 2;
					}
					for (int m = num4; m < num5; m++)
					{
						byte[] array6 = array;
						int num11 = m;
						array6[num11] += ((array[m - num4] & byte.MaxValue) + (array2[m] & byte.MaxValue)) / 2;
					}
					goto IL_357;
				case 4:
					for (int n = 0; n < num4; n++)
					{
						byte[] array7 = array;
						int num12 = n;
						array7[num12] += array2[n];
					}
					for (int num13 = num4; num13 < num5; num13++)
					{
						int num14 = (int)(array[num13 - num4] & byte.MaxValue);
						int num15 = (int)(array2[num13] & byte.MaxValue);
						int num16 = (int)(array2[num13 - num4] & byte.MaxValue);
						int num17 = num14 + num15 - num16;
						int num18 = Math.Abs(num17 - num14);
						int num19 = Math.Abs(num17 - num15);
						int num20 = Math.Abs(num17 - num16);
						int num21;
						if (num18 <= num19 && num18 <= num20)
						{
							num21 = num14;
						}
						else if (num19 <= num20)
						{
							num21 = num15;
						}
						else
						{
							num21 = num16;
						}
						byte[] array8 = array;
						int num22 = num13;
						array8[num22] += (byte)num21;
					}
					goto IL_357;
				}
				break;
				IL_357:
				memoryStream2.Write(array, 0, array.Length);
				byte[] array9 = array2;
				array2 = array;
				array = array9;
			}
			throw new Exception(MessageLocalization.GetComposedMessage("png.filter.unknown"));
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000637B8 File Offset: 0x000627B8
		public static byte[] FlateDecode(byte[] inp, bool strict)
		{
			MemoryStream inp2 = new MemoryStream(inp);
			ZInflaterInputStream zinflaterInputStream = new ZInflaterInputStream(inp2);
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[strict ? 4092 : 1];
			byte[] result;
			try
			{
				int count;
				while ((count = zinflaterInputStream.Read(array, 0, array.Length)) > 0)
				{
					memoryStream.Write(array, 0, count);
				}
				zinflaterInputStream.Close();
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			catch
			{
				if (strict)
				{
					result = null;
				}
				else
				{
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00063844 File Offset: 0x00062844
		public static byte[] ASCIIHexDecode(byte[] inp)
		{
			MemoryStream memoryStream = new MemoryStream();
			bool flag = true;
			int num = 0;
			for (int i = 0; i < inp.Length; i++)
			{
				int num2 = (int)(inp[i] & byte.MaxValue);
				if (num2 == 62)
				{
					break;
				}
				if (!PRTokeniser.IsWhitespace(num2))
				{
					int hex = PRTokeniser.GetHex(num2);
					if (hex == -1)
					{
						throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.character.in.asciihexdecode"));
					}
					if (flag)
					{
						num = hex;
					}
					else
					{
						memoryStream.WriteByte((byte)((num << 4) + hex));
					}
					flag = !flag;
				}
			}
			if (!flag)
			{
				memoryStream.WriteByte((byte)(num << 4));
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000638D0 File Offset: 0x000628D0
		public static byte[] ASCII85Decode(byte[] inp)
		{
			MemoryStream memoryStream = new MemoryStream();
			int num = 0;
			int[] array = new int[5];
			for (int i = 0; i < inp.Length; i++)
			{
				int num2 = (int)(inp[i] & byte.MaxValue);
				if (num2 == 126)
				{
					break;
				}
				if (!PRTokeniser.IsWhitespace(num2))
				{
					if (num2 == 122 && num == 0)
					{
						memoryStream.WriteByte(0);
						memoryStream.WriteByte(0);
						memoryStream.WriteByte(0);
						memoryStream.WriteByte(0);
					}
					else
					{
						if (num2 < 33 || num2 > 117)
						{
							throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.character.in.ascii85decode"));
						}
						array[num] = num2 - 33;
						num++;
						if (num == 5)
						{
							num = 0;
							int num3 = 0;
							for (int j = 0; j < 5; j++)
							{
								num3 = num3 * 85 + array[j];
							}
							memoryStream.WriteByte((byte)(num3 >> 24));
							memoryStream.WriteByte((byte)(num3 >> 16));
							memoryStream.WriteByte((byte)(num3 >> 8));
							memoryStream.WriteByte((byte)num3);
						}
					}
				}
			}
			if (num == 2)
			{
				int num4 = array[0] * 85 * 85 * 85 * 85 + array[1] * 85 * 85 * 85 + 614125 + 7225 + 85;
				memoryStream.WriteByte((byte)(num4 >> 24));
			}
			else if (num == 3)
			{
				int num4 = array[0] * 85 * 85 * 85 * 85 + array[1] * 85 * 85 * 85 + array[2] * 85 * 85 + 7225 + 85;
				memoryStream.WriteByte((byte)(num4 >> 24));
				memoryStream.WriteByte((byte)(num4 >> 16));
			}
			else if (num == 4)
			{
				int num4 = array[0] * 85 * 85 * 85 * 85 + array[1] * 85 * 85 * 85 + array[2] * 85 * 85 + array[3] * 85 + 85;
				memoryStream.WriteByte((byte)(num4 >> 24));
				memoryStream.WriteByte((byte)(num4 >> 16));
				memoryStream.WriteByte((byte)(num4 >> 8));
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00063AB4 File Offset: 0x00062AB4
		public static byte[] LZWDecode(byte[] inp)
		{
			MemoryStream memoryStream = new MemoryStream();
			LZWDecoder lzwdecoder = new LZWDecoder();
			lzwdecoder.Decode(inp, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00063ADB File Offset: 0x00062ADB
		public bool IsRebuilt()
		{
			return this.rebuilt;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00063AE4 File Offset: 0x00062AE4
		public PdfDictionary GetPageN(int pageNum)
		{
			PdfDictionary pageN = this.pageRefs.GetPageN(pageNum);
			if (pageN == null)
			{
				return null;
			}
			if (this.appendable)
			{
				pageN.IndRef = this.pageRefs.GetPageOrigRef(pageNum);
			}
			return pageN;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00063B20 File Offset: 0x00062B20
		public PdfDictionary GetPageNRelease(int pageNum)
		{
			PdfDictionary pageN = this.GetPageN(pageNum);
			this.pageRefs.ReleasePage(pageNum);
			return pageN;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00063B42 File Offset: 0x00062B42
		public void ReleasePage(int pageNum)
		{
			this.pageRefs.ReleasePage(pageNum);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00063B50 File Offset: 0x00062B50
		public void ResetReleasePage()
		{
			this.pageRefs.ResetReleasePage();
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00063B5D File Offset: 0x00062B5D
		public PRIndirectReference GetPageOrigRef(int pageNum)
		{
			return this.pageRefs.GetPageOrigRef(pageNum);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00063B6C File Offset: 0x00062B6C
		public byte[] GetPageContent(int pageNum, RandomAccessFileOrArray file)
		{
			PdfDictionary pageNRelease = this.GetPageNRelease(pageNum);
			if (pageNRelease == null)
			{
				return null;
			}
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(pageNRelease.Get(PdfName.CONTENTS));
			if (pdfObjectRelease == null)
			{
				return new byte[0];
			}
			if (pdfObjectRelease.IsStream())
			{
				return PdfReader.GetStreamBytes((PRStream)pdfObjectRelease, file);
			}
			if (pdfObjectRelease.IsArray())
			{
				PdfArray pdfArray = (PdfArray)pdfObjectRelease;
				MemoryStream memoryStream = new MemoryStream();
				for (int i = 0; i < pdfArray.Size; i++)
				{
					PdfObject pdfObjectRelease2 = PdfReader.GetPdfObjectRelease(pdfArray[i]);
					if (pdfObjectRelease2 != null && pdfObjectRelease2.IsStream())
					{
						byte[] streamBytes = PdfReader.GetStreamBytes((PRStream)pdfObjectRelease2, file);
						memoryStream.Write(streamBytes, 0, streamBytes.Length);
						if (i != pdfArray.Size - 1)
						{
							memoryStream.WriteByte(10);
						}
					}
				}
				return memoryStream.ToArray();
			}
			return new byte[0];
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00063C3C File Offset: 0x00062C3C
		public byte[] GetPageContent(int pageNum)
		{
			RandomAccessFileOrArray safeFile = this.SafeFile;
			byte[] pageContent;
			try
			{
				safeFile.ReOpen();
				pageContent = this.GetPageContent(pageNum, safeFile);
			}
			finally
			{
				try
				{
					safeFile.Close();
				}
				catch
				{
				}
			}
			return pageContent;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00063C8C File Offset: 0x00062C8C
		protected internal void KillXref(PdfObject obj)
		{
			if (obj == null)
			{
				return;
			}
			if (obj is PdfIndirectReference && !obj.IsIndirect())
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
					this.KillXref(pdfArray[i]);
				}
				return;
			}
			case 6:
			case 7:
			{
				PdfDictionary pdfDictionary = (PdfDictionary)obj;
				foreach (PdfName key in pdfDictionary.Keys)
				{
					this.KillXref(pdfDictionary.Get(key));
				}
				break;
			}
			case 8:
			case 9:
				break;
			case 10:
			{
				int number = ((PRIndirectReference)obj).Number;
				obj = this.xrefObj[number];
				this.xrefObj[number] = null;
				this.freeXref = number;
				this.KillXref(obj);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00063D8C File Offset: 0x00062D8C
		public void SetPageContent(int pageNum, byte[] content)
		{
			this.SetPageContent(pageNum, content, -1);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00063D98 File Offset: 0x00062D98
		public void SetPageContent(int pageNum, byte[] content, int compressionLevel)
		{
			PdfDictionary pageN = this.GetPageN(pageNum);
			if (pageN == null)
			{
				return;
			}
			PdfObject obj = pageN.Get(PdfName.CONTENTS);
			this.freeXref = -1;
			this.KillXref(obj);
			if (this.freeXref == -1)
			{
				this.xrefObj.Add(null);
				this.freeXref = this.xrefObj.Count - 1;
			}
			pageN.Put(PdfName.CONTENTS, new PRIndirectReference(this, this.freeXref));
			this.xrefObj[this.freeXref] = new PRStream(this, content, compressionLevel);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00063E24 File Offset: 0x00062E24
		public static byte[] GetStreamBytes(PRStream stream, RandomAccessFileOrArray file)
		{
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(stream.Get(PdfName.FILTER));
			byte[] array = PdfReader.GetStreamBytesRaw(stream, file);
			List<PdfObject> list = new List<PdfObject>();
			if (pdfObjectRelease != null)
			{
				if (pdfObjectRelease.IsName())
				{
					list.Add(pdfObjectRelease);
				}
				else if (pdfObjectRelease.IsArray())
				{
					list = ((PdfArray)pdfObjectRelease).ArrayList;
				}
			}
			List<PdfObject> list2 = new List<PdfObject>();
			PdfObject pdfObjectRelease2 = PdfReader.GetPdfObjectRelease(stream.Get(PdfName.DECODEPARMS));
			if (pdfObjectRelease2 == null || (!pdfObjectRelease2.IsDictionary() && !pdfObjectRelease2.IsArray()))
			{
				pdfObjectRelease2 = PdfReader.GetPdfObjectRelease(stream.Get(PdfName.DP));
			}
			if (pdfObjectRelease2 != null)
			{
				if (pdfObjectRelease2.IsDictionary())
				{
					list2.Add(pdfObjectRelease2);
				}
				else if (pdfObjectRelease2.IsArray())
				{
					list2 = ((PdfArray)pdfObjectRelease2).ArrayList;
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				PdfName pdfName = (PdfName)PdfReader.GetPdfObjectRelease(list[i]);
				if (PdfName.FLATEDECODE.Equals(pdfName) || PdfName.FL.Equals(pdfName))
				{
					array = PdfReader.FlateDecode(array);
					if (i < list2.Count)
					{
						PdfObject dicPar = list2[i];
						array = PdfReader.DecodePredictor(array, dicPar);
					}
				}
				else if (PdfName.ASCIIHEXDECODE.Equals(pdfName) || PdfName.AHX.Equals(pdfName))
				{
					array = PdfReader.ASCIIHexDecode(array);
				}
				else if (PdfName.ASCII85DECODE.Equals(pdfName) || PdfName.A85.Equals(pdfName))
				{
					array = PdfReader.ASCII85Decode(array);
				}
				else if (PdfName.LZWDECODE.Equals(pdfName))
				{
					array = PdfReader.LZWDecode(array);
					if (i < list2.Count)
					{
						PdfObject dicPar2 = list2[i];
						array = PdfReader.DecodePredictor(array, dicPar2);
					}
				}
				else if (PdfName.CCITTFAXDECODE.Equals(pdfName))
				{
					PdfNumber pdfNumber = (PdfNumber)PdfReader.GetPdfObjectRelease(stream.Get(PdfName.WIDTH));
					PdfNumber pdfNumber2 = (PdfNumber)PdfReader.GetPdfObjectRelease(stream.Get(PdfName.HEIGHT));
					if (pdfNumber == null || pdfNumber2 == null)
					{
						throw new UnsupportedPdfException(MessageLocalization.GetComposedMessage("filter.ccittfaxdecode.is.only.supported.for.images"));
					}
					int intValue = pdfNumber.IntValue;
					int intValue2 = pdfNumber2.IntValue;
					PdfDictionary pdfDictionary = null;
					if (i < list2.Count)
					{
						PdfObject pdfObjectRelease3 = PdfReader.GetPdfObjectRelease(list2[i]);
						if (pdfObjectRelease3 != null && pdfObjectRelease3 is PdfDictionary)
						{
							pdfDictionary = (PdfDictionary)pdfObjectRelease3;
						}
					}
					int num = 0;
					bool flag = false;
					bool flag2 = false;
					if (pdfDictionary != null)
					{
						PdfNumber asNumber = pdfDictionary.GetAsNumber(PdfName.K);
						if (asNumber != null)
						{
							num = asNumber.IntValue;
						}
						PdfBoolean asBoolean = pdfDictionary.GetAsBoolean(PdfName.BLACKIS1);
						if (asBoolean != null)
						{
							flag = asBoolean.BooleanValue;
						}
						asBoolean = pdfDictionary.GetAsBoolean(PdfName.ENCODEDBYTEALIGN);
						if (asBoolean != null)
						{
							flag2 = asBoolean.BooleanValue;
						}
					}
					byte[] array2 = new byte[(intValue + 7) / 8 * intValue2];
					TIFFFaxDecoder tifffaxDecoder = new TIFFFaxDecoder(1, intValue, intValue2);
					if (num == 0 || num > 0)
					{
						int num2 = (num > 0) ? 1 : 0;
						num2 |= (flag2 ? 4 : 0);
						try
						{
							tifffaxDecoder.Decode2D(array2, array, 0, intValue2, (long)num2);
							goto IL_31F;
						}
						catch (Exception ex)
						{
							num2 ^= 4;
							try
							{
								tifffaxDecoder.Decode2D(array2, array, 0, intValue2, (long)num2);
							}
							catch
							{
								throw ex;
							}
							goto IL_31F;
						}
						goto IL_310;
					}
					goto IL_310;
					IL_31F:
					if (!flag)
					{
						int num3 = array2.Length;
						for (int j = 0; j < num3; j++)
						{
							byte[] array3 = array2;
							int num4 = j;
							array3[num4] ^= byte.MaxValue;
						}
					}
					array = array2;
					goto IL_37A;
					IL_310:
					tifffaxDecoder.DecodeT6(array2, array, 0, intValue2, 0L);
					goto IL_31F;
				}
				else if (!PdfName.CRYPT.Equals(pdfName))
				{
					throw new UnsupportedPdfException(MessageLocalization.GetComposedMessage("the.filter.1.is.not.supported", pdfName));
				}
				IL_37A:;
			}
			return array;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000641DC File Offset: 0x000631DC
		public static byte[] GetStreamBytes(PRStream stream)
		{
			RandomAccessFileOrArray safeFile = stream.Reader.SafeFile;
			byte[] streamBytes;
			try
			{
				safeFile.ReOpen();
				streamBytes = PdfReader.GetStreamBytes(stream, safeFile);
			}
			finally
			{
				try
				{
					safeFile.Close();
				}
				catch
				{
				}
			}
			return streamBytes;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00064230 File Offset: 0x00063230
		public static byte[] GetStreamBytesRaw(PRStream stream, RandomAccessFileOrArray file)
		{
			PdfReader reader = stream.Reader;
			byte[] array;
			if (stream.Offset < 0)
			{
				array = stream.GetBytes();
			}
			else
			{
				array = new byte[stream.Length];
				file.Seek(stream.Offset);
				file.ReadFully(array);
				PdfEncryption pdfEncryption = reader.Decrypt;
				if (pdfEncryption != null)
				{
					PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(stream.Get(PdfName.FILTER));
					List<PdfObject> list = new List<PdfObject>();
					if (pdfObjectRelease != null)
					{
						if (pdfObjectRelease.IsName())
						{
							list.Add(pdfObjectRelease);
						}
						else if (pdfObjectRelease.IsArray())
						{
							list = ((PdfArray)pdfObjectRelease).ArrayList;
						}
					}
					bool flag = false;
					for (int i = 0; i < list.Count; i++)
					{
						PdfObject pdfObjectRelease2 = PdfReader.GetPdfObjectRelease(list[i]);
						if (pdfObjectRelease2 != null && pdfObjectRelease2.ToString().Equals("/Crypt"))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						pdfEncryption.SetHashKey(stream.ObjNum, stream.ObjGen);
						array = pdfEncryption.DecryptByteArray(array);
					}
				}
			}
			return array;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0006432C File Offset: 0x0006332C
		public static byte[] GetStreamBytesRaw(PRStream stream)
		{
			RandomAccessFileOrArray safeFile = stream.Reader.SafeFile;
			byte[] streamBytesRaw;
			try
			{
				safeFile.ReOpen();
				streamBytesRaw = PdfReader.GetStreamBytesRaw(stream, safeFile);
			}
			finally
			{
				try
				{
					safeFile.Close();
				}
				catch
				{
				}
			}
			return streamBytesRaw;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00064380 File Offset: 0x00063380
		public void EliminateSharedStreams()
		{
			if (!this.sharedStreams)
			{
				return;
			}
			this.sharedStreams = false;
			if (this.pageRefs.Size == 1)
			{
				return;
			}
			List<PRIndirectReference> list = new List<PRIndirectReference>();
			List<PRStream> list2 = new List<PRStream>();
			IntHashtable intHashtable = new IntHashtable();
			for (int i = 1; i <= this.pageRefs.Size; i++)
			{
				PdfDictionary pageN = this.pageRefs.GetPageN(i);
				if (pageN != null)
				{
					PdfObject pdfObject = PdfReader.GetPdfObject(pageN.Get(PdfName.CONTENTS));
					if (pdfObject != null)
					{
						if (pdfObject.IsStream())
						{
							PRIndirectReference prindirectReference = (PRIndirectReference)pageN.Get(PdfName.CONTENTS);
							if (intHashtable.ContainsKey(prindirectReference.Number))
							{
								list.Add(prindirectReference);
								list2.Add(new PRStream((PRStream)pdfObject, null));
							}
							else
							{
								intHashtable[prindirectReference.Number] = 1;
							}
						}
						else if (pdfObject.IsArray())
						{
							PdfArray pdfArray = (PdfArray)pdfObject;
							for (int j = 0; j < pdfArray.Size; j++)
							{
								PRIndirectReference prindirectReference2 = (PRIndirectReference)pdfArray[j];
								if (intHashtable.ContainsKey(prindirectReference2.Number))
								{
									list.Add(prindirectReference2);
									list2.Add(new PRStream((PRStream)PdfReader.GetPdfObject(prindirectReference2), null));
								}
								else
								{
									intHashtable[prindirectReference2.Number] = 1;
								}
							}
						}
					}
				}
			}
			if (list2.Count == 0)
			{
				return;
			}
			for (int k = 0; k < list2.Count; k++)
			{
				this.xrefObj.Add(list2[k]);
				PRIndirectReference prindirectReference3 = list[k];
				prindirectReference3.SetNumber(this.xrefObj.Count - 1, 0);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0006452B File Offset: 0x0006352B
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x00064533 File Offset: 0x00063533
		public bool Tampered
		{
			get
			{
				return this.tampered;
			}
			set
			{
				this.tampered = value;
				this.pageRefs.KeepPages();
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00064548 File Offset: 0x00063548
		public byte[] Metadata
		{
			get
			{
				PdfObject pdfObject = PdfReader.GetPdfObject(this.catalog.Get(PdfName.METADATA));
				if (!(pdfObject is PRStream))
				{
					return null;
				}
				RandomAccessFileOrArray safeFile = this.SafeFile;
				byte[] result = null;
				try
				{
					safeFile.ReOpen();
					result = PdfReader.GetStreamBytes((PRStream)pdfObject, safeFile);
				}
				finally
				{
					try
					{
						safeFile.Close();
					}
					catch
					{
					}
				}
				return result;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x000645BC File Offset: 0x000635BC
		public int LastXref
		{
			get
			{
				return this.lastXref;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x000645C4 File Offset: 0x000635C4
		public int XrefSize
		{
			get
			{
				return this.xrefObj.Count;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x000645D1 File Offset: 0x000635D1
		public int EofPos
		{
			get
			{
				return this.eofPos;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x000645D9 File Offset: 0x000635D9
		public char PdfVersion
		{
			get
			{
				return this.pdfVersion;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000645E1 File Offset: 0x000635E1
		public bool IsEncrypted()
		{
			return this.encrypted;
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x000645E9 File Offset: 0x000635E9
		public int Permissions
		{
			get
			{
				return this.pValue;
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000645F1 File Offset: 0x000635F1
		public bool Is128Key()
		{
			return this.rValue == 3;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x000645FC File Offset: 0x000635FC
		public PdfDictionary Trailer
		{
			get
			{
				return this.trailer;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00064604 File Offset: 0x00063604
		internal PdfEncryption Decrypt
		{
			get
			{
				return this.decrypt;
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0006460C File Offset: 0x0006360C
		internal static bool Equalsn(byte[] a1, byte[] a2)
		{
			int num = a2.Length;
			for (int i = 0; i < num; i++)
			{
				if (a1[i] != a2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00064634 File Offset: 0x00063634
		internal static bool ExistsName(PdfDictionary dic, PdfName key, PdfName value)
		{
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(dic.Get(key));
			if (pdfObjectRelease == null || !pdfObjectRelease.IsName())
			{
				return false;
			}
			PdfName pdfName = (PdfName)pdfObjectRelease;
			return pdfName.Equals(value);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0006466C File Offset: 0x0006366C
		internal static string GetFontName(PdfDictionary dic)
		{
			if (dic == null)
			{
				return null;
			}
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(dic.Get(PdfName.BASEFONT));
			if (pdfObjectRelease == null || !pdfObjectRelease.IsName())
			{
				return null;
			}
			return PdfName.DecodeName(pdfObjectRelease.ToString());
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000646A8 File Offset: 0x000636A8
		internal static string GetSubsetPrefix(PdfDictionary dic)
		{
			if (dic == null)
			{
				return null;
			}
			string fontName = PdfReader.GetFontName(dic);
			if (fontName == null)
			{
				return null;
			}
			if (fontName.Length < 8 || fontName[6] != '+')
			{
				return null;
			}
			for (int i = 0; i < 6; i++)
			{
				char c = fontName[i];
				if (c < 'A' || c > 'Z')
				{
					return null;
				}
			}
			return fontName;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00064700 File Offset: 0x00063700
		public int ShuffleSubsetNames()
		{
			int num = 0;
			for (int i = 1; i < this.xrefObj.Count; i++)
			{
				PdfObject pdfObjectRelease = this.GetPdfObjectRelease(i);
				if (pdfObjectRelease != null && pdfObjectRelease.IsDictionary())
				{
					PdfDictionary pdfDictionary = (PdfDictionary)pdfObjectRelease;
					if (PdfReader.ExistsName(pdfDictionary, PdfName.TYPE, PdfName.FONT))
					{
						if (PdfReader.ExistsName(pdfDictionary, PdfName.SUBTYPE, PdfName.TYPE1) || PdfReader.ExistsName(pdfDictionary, PdfName.SUBTYPE, PdfName.MMTYPE1) || PdfReader.ExistsName(pdfDictionary, PdfName.SUBTYPE, PdfName.TRUETYPE))
						{
							string subsetPrefix = PdfReader.GetSubsetPrefix(pdfDictionary);
							if (subsetPrefix != null)
							{
								string name = BaseFont.CreateSubsetPrefix() + subsetPrefix.Substring(7);
								PdfName value = new PdfName(name);
								pdfDictionary.Put(PdfName.BASEFONT, value);
								this.SetXrefPartialObject(i, pdfDictionary);
								num++;
								PdfDictionary asDict = pdfDictionary.GetAsDict(PdfName.FONTDESCRIPTOR);
								if (asDict != null)
								{
									asDict.Put(PdfName.FONTNAME, value);
								}
							}
						}
						else if (PdfReader.ExistsName(pdfDictionary, PdfName.SUBTYPE, PdfName.TYPE0))
						{
							string subsetPrefix2 = PdfReader.GetSubsetPrefix(pdfDictionary);
							PdfArray asArray = pdfDictionary.GetAsArray(PdfName.DESCENDANTFONTS);
							if (asArray != null && !asArray.IsEmpty())
							{
								PdfDictionary asDict2 = asArray.GetAsDict(0);
								string subsetPrefix3 = PdfReader.GetSubsetPrefix(asDict2);
								if (subsetPrefix3 != null)
								{
									string str = BaseFont.CreateSubsetPrefix();
									if (subsetPrefix2 != null)
									{
										pdfDictionary.Put(PdfName.BASEFONT, new PdfName(str + subsetPrefix2.Substring(7)));
									}
									this.SetXrefPartialObject(i, pdfDictionary);
									PdfName value2 = new PdfName(str + subsetPrefix3.Substring(7));
									asDict2.Put(PdfName.BASEFONT, value2);
									num++;
									PdfDictionary asDict3 = asDict2.GetAsDict(PdfName.FONTDESCRIPTOR);
									if (asDict3 != null)
									{
										asDict3.Put(PdfName.FONTNAME, value2);
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000648D4 File Offset: 0x000638D4
		public int CreateFakeFontSubsets()
		{
			int num = 0;
			for (int i = 1; i < this.xrefObj.Count; i++)
			{
				PdfObject pdfObjectRelease = this.GetPdfObjectRelease(i);
				if (pdfObjectRelease != null && pdfObjectRelease.IsDictionary())
				{
					PdfDictionary pdfDictionary = (PdfDictionary)pdfObjectRelease;
					if (PdfReader.ExistsName(pdfDictionary, PdfName.TYPE, PdfName.FONT) && (PdfReader.ExistsName(pdfDictionary, PdfName.SUBTYPE, PdfName.TYPE1) || PdfReader.ExistsName(pdfDictionary, PdfName.SUBTYPE, PdfName.MMTYPE1) || PdfReader.ExistsName(pdfDictionary, PdfName.SUBTYPE, PdfName.TRUETYPE)) && PdfReader.GetSubsetPrefix(pdfDictionary) == null)
					{
						string fontName = PdfReader.GetFontName(pdfDictionary);
						if (fontName != null)
						{
							string name = BaseFont.CreateSubsetPrefix() + fontName;
							PdfDictionary pdfDictionary2 = (PdfDictionary)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.FONTDESCRIPTOR));
							if (pdfDictionary2 != null && (pdfDictionary2.Get(PdfName.FONTFILE) != null || pdfDictionary2.Get(PdfName.FONTFILE2) != null || pdfDictionary2.Get(PdfName.FONTFILE3) != null))
							{
								pdfDictionary2 = pdfDictionary.GetAsDict(PdfName.FONTDESCRIPTOR);
								PdfName value = new PdfName(name);
								pdfDictionary.Put(PdfName.BASEFONT, value);
								pdfDictionary2.Put(PdfName.FONTNAME, value);
								this.SetXrefPartialObject(i, pdfDictionary);
								num++;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00064A1C File Offset: 0x00063A1C
		private static PdfArray GetNameArray(PdfObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			obj = PdfReader.GetPdfObjectRelease(obj);
			if (obj == null)
			{
				return null;
			}
			if (obj.IsArray())
			{
				return (PdfArray)obj;
			}
			if (obj.IsDictionary())
			{
				PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(((PdfDictionary)obj).Get(PdfName.D));
				if (pdfObjectRelease != null && pdfObjectRelease.IsArray())
				{
					return (PdfArray)pdfObjectRelease;
				}
			}
			return null;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00064A7B File Offset: 0x00063A7B
		public Dictionary<object, PdfObject> GetNamedDestination()
		{
			return this.GetNamedDestination(false);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00064A84 File Offset: 0x00063A84
		public Dictionary<object, PdfObject> GetNamedDestination(bool keepNames)
		{
			Dictionary<object, PdfObject> namedDestinationFromNames = this.GetNamedDestinationFromNames(keepNames);
			Dictionary<string, PdfObject> namedDestinationFromStrings = this.GetNamedDestinationFromStrings();
			foreach (KeyValuePair<string, PdfObject> keyValuePair in namedDestinationFromStrings)
			{
				namedDestinationFromNames[keyValuePair.Key] = keyValuePair.Value;
			}
			return namedDestinationFromNames;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00064AF0 File Offset: 0x00063AF0
		public Dictionary<string, PdfObject> GetNamedDestinationFromNames()
		{
			Dictionary<string, PdfObject> dictionary = new Dictionary<string, PdfObject>();
			foreach (KeyValuePair<object, PdfObject> keyValuePair in this.GetNamedDestinationFromNames(false))
			{
				dictionary[(string)keyValuePair.Key] = keyValuePair.Value;
			}
			return dictionary;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00064B60 File Offset: 0x00063B60
		public Dictionary<object, PdfObject> GetNamedDestinationFromNames(bool keepNames)
		{
			Dictionary<object, PdfObject> dictionary = new Dictionary<object, PdfObject>();
			if (this.catalog.Get(PdfName.DESTS) != null)
			{
				PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(this.catalog.Get(PdfName.DESTS));
				if (pdfDictionary == null)
				{
					return dictionary;
				}
				foreach (PdfName pdfName in pdfDictionary.Keys)
				{
					PdfArray nameArray = PdfReader.GetNameArray(pdfDictionary.Get(pdfName));
					if (nameArray != null)
					{
						if (keepNames)
						{
							dictionary[pdfName] = nameArray;
						}
						else
						{
							string key = PdfName.DecodeName(pdfName.ToString());
							dictionary[key] = nameArray;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00064C1C File Offset: 0x00063C1C
		public Dictionary<string, PdfObject> GetNamedDestinationFromStrings()
		{
			if (this.catalog.Get(PdfName.NAMES) != null)
			{
				PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(this.catalog.Get(PdfName.NAMES));
				if (pdfDictionary != null)
				{
					pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.DESTS));
					if (pdfDictionary != null)
					{
						Dictionary<string, PdfObject> dictionary = PdfNameTree.ReadTree(pdfDictionary);
						string[] array = new string[dictionary.Count];
						dictionary.Keys.CopyTo(array, 0);
						foreach (string key in array)
						{
							PdfArray nameArray = PdfReader.GetNameArray(dictionary[key]);
							if (nameArray != null)
							{
								dictionary[key] = nameArray;
							}
							else
							{
								dictionary.Remove(key);
							}
						}
						return dictionary;
					}
				}
			}
			return new Dictionary<string, PdfObject>();
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00064CE0 File Offset: 0x00063CE0
		public void RemoveFields()
		{
			this.pageRefs.ResetReleasePage();
			for (int i = 1; i <= this.pageRefs.Size; i++)
			{
				PdfDictionary pageN = this.pageRefs.GetPageN(i);
				PdfArray asArray = pageN.GetAsArray(PdfName.ANNOTS);
				if (asArray == null)
				{
					this.pageRefs.ReleasePage(i);
				}
				else
				{
					for (int j = 0; j < asArray.Size; j++)
					{
						PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(asArray[j]);
						if (pdfObjectRelease != null && pdfObjectRelease.IsDictionary())
						{
							PdfDictionary pdfDictionary = (PdfDictionary)pdfObjectRelease;
							if (PdfName.WIDGET.Equals(pdfDictionary.Get(PdfName.SUBTYPE)))
							{
								asArray.Remove(j--);
							}
						}
					}
					if (asArray.IsEmpty())
					{
						pageN.Remove(PdfName.ANNOTS);
					}
					else
					{
						this.pageRefs.ReleasePage(i);
					}
				}
			}
			this.catalog.Remove(PdfName.ACROFORM);
			this.pageRefs.ResetReleasePage();
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00064DD4 File Offset: 0x00063DD4
		public void RemoveAnnotations()
		{
			this.pageRefs.ResetReleasePage();
			for (int i = 1; i <= this.pageRefs.Size; i++)
			{
				PdfDictionary pageN = this.pageRefs.GetPageN(i);
				if (pageN.Get(PdfName.ANNOTS) == null)
				{
					this.pageRefs.ReleasePage(i);
				}
				else
				{
					pageN.Remove(PdfName.ANNOTS);
				}
			}
			this.catalog.Remove(PdfName.ACROFORM);
			this.pageRefs.ResetReleasePage();
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00064E50 File Offset: 0x00063E50
		public List<PdfAnnotation.PdfImportedLink> GetLinks(int page)
		{
			this.pageRefs.ResetReleasePage();
			List<PdfAnnotation.PdfImportedLink> list = new List<PdfAnnotation.PdfImportedLink>();
			PdfDictionary pageN = this.pageRefs.GetPageN(page);
			if (pageN.Get(PdfName.ANNOTS) != null)
			{
				PdfArray asArray = pageN.GetAsArray(PdfName.ANNOTS);
				for (int i = 0; i < asArray.Size; i++)
				{
					PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(asArray[i]);
					if (PdfName.LINK.Equals(pdfDictionary.Get(PdfName.SUBTYPE)))
					{
						list.Add(new PdfAnnotation.PdfImportedLink(pdfDictionary));
					}
				}
			}
			this.pageRefs.ReleasePage(page);
			this.pageRefs.ResetReleasePage();
			return list;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00064EF8 File Offset: 0x00063EF8
		private void IterateBookmarks(PdfObject outlineRef, Dictionary<object, PdfObject> names)
		{
			while (outlineRef != null)
			{
				this.ReplaceNamedDestination(outlineRef, names);
				PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(outlineRef);
				PdfObject pdfObject = pdfDictionary.Get(PdfName.FIRST);
				if (pdfObject != null)
				{
					this.IterateBookmarks(pdfObject, names);
				}
				outlineRef = pdfDictionary.Get(PdfName.NEXT);
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00064F44 File Offset: 0x00063F44
		public void MakeRemoteNamedDestinationsLocal()
		{
			if (this.remoteToLocalNamedDestinations)
			{
				return;
			}
			this.remoteToLocalNamedDestinations = true;
			Dictionary<object, PdfObject> namedDestination = this.GetNamedDestination(true);
			if (namedDestination.Count == 0)
			{
				return;
			}
			for (int i = 1; i <= this.pageRefs.Size; i++)
			{
				PdfDictionary pageN = this.pageRefs.GetPageN(i);
				PdfObject pdfObject;
				PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObject(pdfObject = pageN.Get(PdfName.ANNOTS));
				int idx = this.lastXrefPartial;
				this.ReleaseLastXrefPartial();
				if (pdfArray == null)
				{
					this.pageRefs.ReleasePage(i);
				}
				else
				{
					bool flag = false;
					for (int j = 0; j < pdfArray.Size; j++)
					{
						PdfObject pdfObject2 = pdfArray[j];
						if (this.ConvertNamedDestination(pdfObject2, namedDestination) && !pdfObject2.IsIndirect())
						{
							flag = true;
						}
					}
					if (flag)
					{
						this.SetXrefPartialObject(idx, pdfArray);
					}
					if (!flag || pdfObject.IsIndirect())
					{
						this.pageRefs.ReleasePage(i);
					}
				}
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00065038 File Offset: 0x00064038
		private bool ConvertNamedDestination(PdfObject obj, Dictionary<object, PdfObject> names)
		{
			obj = PdfReader.GetPdfObject(obj);
			int idx = this.lastXrefPartial;
			this.ReleaseLastXrefPartial();
			if (obj != null && obj.IsDictionary())
			{
				PdfObject pdfObject = PdfReader.GetPdfObject(((PdfDictionary)obj).Get(PdfName.A));
				if (pdfObject != null)
				{
					int idx2 = this.lastXrefPartial;
					this.ReleaseLastXrefPartial();
					PdfDictionary pdfDictionary = (PdfDictionary)pdfObject;
					PdfName obj2 = (PdfName)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.S));
					if (PdfName.GOTOR.Equals(obj2))
					{
						PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.D));
						object obj3 = null;
						if (pdfObjectRelease != null)
						{
							if (pdfObjectRelease.IsName())
							{
								obj3 = pdfObjectRelease;
							}
							else if (pdfObjectRelease.IsString())
							{
								obj3 = pdfObjectRelease.ToString();
							}
							PdfArray pdfArray = null;
							if (obj3 != null && names.ContainsKey(obj3))
							{
								pdfArray = (PdfArray)names[obj3];
							}
							if (pdfArray != null)
							{
								pdfDictionary.Remove(PdfName.F);
								pdfDictionary.Remove(PdfName.NEWWINDOW);
								pdfDictionary.Put(PdfName.S, PdfName.GOTO);
								this.SetXrefPartialObject(idx2, pdfObject);
								this.SetXrefPartialObject(idx, obj);
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0006515C File Offset: 0x0006415C
		public void ConsolidateNamedDestinations()
		{
			if (this.consolidateNamedDestinations)
			{
				return;
			}
			this.consolidateNamedDestinations = true;
			Dictionary<object, PdfObject> namedDestination = this.GetNamedDestination(true);
			if (namedDestination.Count == 0)
			{
				return;
			}
			for (int i = 1; i <= this.pageRefs.Size; i++)
			{
				PdfDictionary pageN = this.pageRefs.GetPageN(i);
				PdfObject pdfObject;
				PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObject(pdfObject = pageN.Get(PdfName.ANNOTS));
				int idx = this.lastXrefPartial;
				this.ReleaseLastXrefPartial();
				if (pdfArray == null)
				{
					this.pageRefs.ReleasePage(i);
				}
				else
				{
					bool flag = false;
					for (int j = 0; j < pdfArray.Size; j++)
					{
						PdfObject pdfObject2 = pdfArray[j];
						if (this.ReplaceNamedDestination(pdfObject2, namedDestination) && !pdfObject2.IsIndirect())
						{
							flag = true;
						}
					}
					if (flag)
					{
						this.SetXrefPartialObject(idx, pdfArray);
					}
					if (!flag || pdfObject.IsIndirect())
					{
						this.pageRefs.ReleasePage(i);
					}
				}
			}
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(this.catalog.Get(PdfName.OUTLINES));
			if (pdfDictionary == null)
			{
				return;
			}
			this.IterateBookmarks(pdfDictionary.Get(PdfName.FIRST), namedDestination);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00065284 File Offset: 0x00064284
		private bool ReplaceNamedDestination(PdfObject obj, Dictionary<object, PdfObject> names)
		{
			obj = PdfReader.GetPdfObject(obj);
			int idx = this.lastXrefPartial;
			this.ReleaseLastXrefPartial();
			if (obj != null && obj.IsDictionary())
			{
				PdfObject pdfObject = PdfReader.GetPdfObjectRelease(((PdfDictionary)obj).Get(PdfName.DEST));
				object obj2 = null;
				if (pdfObject != null)
				{
					if (pdfObject.IsName())
					{
						obj2 = pdfObject;
					}
					else if (pdfObject.IsString())
					{
						obj2 = pdfObject.ToString();
					}
					if (obj2 != null)
					{
						PdfArray pdfArray = (PdfArray)names[obj2];
						if (pdfArray != null)
						{
							((PdfDictionary)obj).Put(PdfName.DEST, pdfArray);
							this.SetXrefPartialObject(idx, obj);
							return true;
						}
					}
				}
				else if ((pdfObject = PdfReader.GetPdfObject(((PdfDictionary)obj).Get(PdfName.A))) != null)
				{
					int idx2 = this.lastXrefPartial;
					this.ReleaseLastXrefPartial();
					PdfDictionary pdfDictionary = (PdfDictionary)pdfObject;
					PdfName obj3 = (PdfName)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.S));
					if (PdfName.GOTO.Equals(obj3))
					{
						PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.D));
						if (pdfObjectRelease != null)
						{
							if (pdfObjectRelease.IsName())
							{
								obj2 = pdfObjectRelease;
							}
							else if (pdfObjectRelease.IsString())
							{
								obj2 = pdfObjectRelease.ToString();
							}
						}
						if (obj2 != null)
						{
							PdfArray pdfArray2 = (PdfArray)names[obj2];
							if (pdfArray2 != null)
							{
								pdfDictionary.Put(PdfName.D, pdfArray2);
								this.SetXrefPartialObject(idx2, pdfObject);
								this.SetXrefPartialObject(idx, obj);
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000653E8 File Offset: 0x000643E8
		protected internal static PdfDictionary DuplicatePdfDictionary(PdfDictionary original, PdfDictionary copy, PdfReader newReader)
		{
			if (copy == null)
			{
				copy = new PdfDictionary();
			}
			foreach (PdfName key in original.Keys)
			{
				copy.Put(key, PdfReader.DuplicatePdfObject(original.Get(key), newReader));
			}
			return copy;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00065454 File Offset: 0x00064454
		protected internal static PdfObject DuplicatePdfObject(PdfObject original, PdfReader newReader)
		{
			if (original == null)
			{
				return null;
			}
			switch (original.Type)
			{
			case 5:
			{
				PdfArray pdfArray = new PdfArray();
				ListIterator<PdfObject> listIterator = ((PdfArray)original).GetListIterator();
				while (listIterator.HasNext())
				{
					pdfArray.Add(PdfReader.DuplicatePdfObject(listIterator.Next(), newReader));
				}
				return pdfArray;
			}
			case 6:
				return PdfReader.DuplicatePdfDictionary((PdfDictionary)original, null, newReader);
			case 7:
			{
				PRStream prstream = (PRStream)original;
				PRStream prstream2 = new PRStream(prstream, null, newReader);
				PdfReader.DuplicatePdfDictionary(prstream, prstream2, newReader);
				return prstream2;
			}
			case 10:
			{
				PRIndirectReference prindirectReference = (PRIndirectReference)original;
				return new PRIndirectReference(newReader, prindirectReference.Number, prindirectReference.Generation);
			}
			}
			return original;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00065509 File Offset: 0x00064509
		public void Close()
		{
			if (!this.partial)
			{
				return;
			}
			this.tokens.Close();
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00065520 File Offset: 0x00064520
		protected internal void RemoveUnusedNode(PdfObject obj, bool[] hits)
		{
			Stack<object> stack = new Stack<object>();
			stack.Push(obj);
			while (stack.Count != 0)
			{
				object obj2 = stack.Pop();
				if (obj2 != null)
				{
					List<PdfObject> list = null;
					PdfDictionary pdfDictionary = null;
					PdfName[] array = null;
					object[] array2 = null;
					int num = 0;
					if (obj2 is PdfObject)
					{
						obj = (PdfObject)obj2;
						switch (obj.Type)
						{
						case 5:
							list = ((PdfArray)obj).ArrayList;
							break;
						case 6:
						case 7:
							pdfDictionary = (PdfDictionary)obj;
							array = new PdfName[pdfDictionary.Size];
							pdfDictionary.Keys.CopyTo(array, 0);
							break;
						case 8:
						case 9:
							continue;
						case 10:
						{
							PRIndirectReference prindirectReference = (PRIndirectReference)obj;
							int number = prindirectReference.Number;
							if (!hits[number])
							{
								hits[number] = true;
								stack.Push(PdfReader.GetPdfObjectRelease(prindirectReference));
								continue;
							}
							continue;
						}
						default:
							continue;
						}
					}
					else
					{
						array2 = (object[])obj2;
						if (array2[0] is List<PdfObject>)
						{
							list = (List<PdfObject>)array2[0];
							num = (int)array2[1];
						}
						else
						{
							array = (PdfName[])array2[0];
							pdfDictionary = (PdfDictionary)array2[1];
							num = (int)array2[2];
						}
					}
					if (list != null)
					{
						int i = num;
						while (i < list.Count)
						{
							PdfObject pdfObject = list[i];
							if (pdfObject.IsIndirect())
							{
								int number2 = ((PRIndirectReference)pdfObject).Number;
								if (number2 >= this.xrefObj.Count || (!this.partial && this.xrefObj[number2] == null))
								{
									list[i] = PdfNull.PDFNULL;
									i++;
									continue;
								}
							}
							if (array2 == null)
							{
								stack.Push(new object[]
								{
									list,
									i + 1
								});
							}
							else
							{
								array2[1] = i + 1;
								stack.Push(array2);
							}
							stack.Push(pdfObject);
							break;
						}
					}
					else
					{
						int j = num;
						while (j < array.Length)
						{
							PdfName key = array[j];
							PdfObject pdfObject2 = pdfDictionary.Get(key);
							if (pdfObject2.IsIndirect())
							{
								int number3 = ((PRIndirectReference)pdfObject2).Number;
								if (number3 >= this.xrefObj.Count || (!this.partial && this.xrefObj[number3] == null))
								{
									pdfDictionary.Put(key, PdfNull.PDFNULL);
									j++;
									continue;
								}
							}
							if (array2 == null)
							{
								stack.Push(new object[]
								{
									array,
									pdfDictionary,
									j + 1
								});
							}
							else
							{
								array2[2] = j + 1;
								stack.Push(array2);
							}
							stack.Push(pdfObject2);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000657E0 File Offset: 0x000647E0
		public int RemoveUnusedObjects()
		{
			bool[] array = new bool[this.xrefObj.Count];
			this.RemoveUnusedNode(this.trailer, array);
			int num = 0;
			if (this.partial)
			{
				for (int i = 1; i < array.Length; i++)
				{
					if (!array[i])
					{
						this.xref[i * 2] = -1;
						this.xref[i * 2 + 1] = 0;
						this.xrefObj[i] = null;
						num++;
					}
				}
			}
			else
			{
				for (int j = 1; j < array.Length; j++)
				{
					if (!array[j])
					{
						this.xrefObj[j] = null;
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00065878 File Offset: 0x00064878
		public AcroFields AcroFields
		{
			get
			{
				return new AcroFields(this, null);
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00065884 File Offset: 0x00064884
		public string GetJavaScript(RandomAccessFileOrArray file)
		{
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(this.catalog.Get(PdfName.NAMES));
			if (pdfDictionary == null)
			{
				return null;
			}
			PdfDictionary pdfDictionary2 = (PdfDictionary)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.JAVASCRIPT));
			if (pdfDictionary2 == null)
			{
				return null;
			}
			Dictionary<string, PdfObject> dictionary = PdfNameTree.ReadTree(pdfDictionary2);
			string[] array = new string[dictionary.Count];
			dictionary.Keys.CopyTo(array, 0);
			Array.Sort<string>(array);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				PdfDictionary pdfDictionary3 = (PdfDictionary)PdfReader.GetPdfObjectRelease(dictionary[array[i]]);
				if (pdfDictionary3 != null)
				{
					PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(pdfDictionary3.Get(PdfName.JS));
					if (pdfObjectRelease != null)
					{
						if (pdfObjectRelease.IsString())
						{
							stringBuilder.Append(((PdfString)pdfObjectRelease).ToUnicodeString()).Append('\n');
						}
						else if (pdfObjectRelease.IsStream())
						{
							byte[] streamBytes = PdfReader.GetStreamBytes((PRStream)pdfObjectRelease, file);
							if (streamBytes.Length >= 2 && streamBytes[0] == 254 && streamBytes[1] == 255)
							{
								stringBuilder.Append(PdfEncodings.ConvertToString(streamBytes, "UnicodeBig"));
							}
							else
							{
								stringBuilder.Append(PdfEncodings.ConvertToString(streamBytes, "PDF"));
							}
							stringBuilder.Append('\n');
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x000659E0 File Offset: 0x000649E0
		public string JavaScript
		{
			get
			{
				RandomAccessFileOrArray safeFile = this.SafeFile;
				string javaScript;
				try
				{
					safeFile.ReOpen();
					javaScript = this.GetJavaScript(safeFile);
				}
				finally
				{
					try
					{
						safeFile.Close();
					}
					catch
					{
					}
				}
				return javaScript;
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00065A30 File Offset: 0x00064A30
		public void SelectPages(string ranges)
		{
			this.SelectPages(SequenceList.Expand(ranges, this.NumberOfPages));
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00065A44 File Offset: 0x00064A44
		public void SelectPages(ICollection<int> pagesToKeep)
		{
			this.pageRefs.SelectPages(pagesToKeep);
			this.RemoveUnusedObjects();
		}

		// Token: 0x1700035A RID: 858
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x00065A59 File Offset: 0x00064A59
		public virtual int ViewerPreferences
		{
			set
			{
				this.viewerPreferences.ViewerPreferences = value;
				this.SetViewerPreferences(this.viewerPreferences);
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00065A73 File Offset: 0x00064A73
		public virtual void AddViewerPreference(PdfName key, PdfObject value)
		{
			this.viewerPreferences.AddViewerPreference(key, value);
			this.SetViewerPreferences(this.viewerPreferences);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00065A8E File Offset: 0x00064A8E
		internal virtual void SetViewerPreferences(PdfViewerPreferencesImp vp)
		{
			vp.AddToCatalog(this.catalog);
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x00065A9C File Offset: 0x00064A9C
		public virtual int SimpleViewerPreferences
		{
			get
			{
				return PdfViewerPreferencesImp.GetViewerPreferences(this.catalog).PageLayoutAndMode;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00065AD5 File Offset: 0x00064AD5
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x00065AAE File Offset: 0x00064AAE
		public bool Appendable
		{
			get
			{
				return this.appendable;
			}
			set
			{
				this.appendable = value;
				if (this.appendable)
				{
					PdfReader.GetPdfObject(this.trailer.Get(PdfName.ROOT));
				}
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00065ADD File Offset: 0x00064ADD
		public bool IsNewXrefType()
		{
			return this.newXrefType;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00065AE5 File Offset: 0x00064AE5
		public int FileLength
		{
			get
			{
				return this.fileLength;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00065AED File Offset: 0x00064AED
		public bool IsHybridXref()
		{
			return this.hybridXref;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00065AF5 File Offset: 0x00064AF5
		internal PdfIndirectReference GetCryptoRef()
		{
			if (this.cryptoRef == null)
			{
				return null;
			}
			return new PdfIndirectReference(0, this.cryptoRef.Number, this.cryptoRef.Generation);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00065B20 File Offset: 0x00064B20
		public void RemoveUsageRights()
		{
			PdfDictionary asDict = this.catalog.GetAsDict(PdfName.PERMS);
			if (asDict == null)
			{
				return;
			}
			asDict.Remove(PdfName.UR);
			asDict.Remove(PdfName.UR3);
			if (asDict.Size == 0)
			{
				this.catalog.Remove(PdfName.PERMS);
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00065B70 File Offset: 0x00064B70
		public int GetCertificationLevel()
		{
			PdfDictionary asDict = this.catalog.GetAsDict(PdfName.PERMS);
			if (asDict == null)
			{
				return 0;
			}
			asDict = asDict.GetAsDict(PdfName.DOCMDP);
			if (asDict == null)
			{
				return 0;
			}
			PdfArray asArray = asDict.GetAsArray(PdfName.REFERENCE);
			if (asArray == null || asArray.Size == 0)
			{
				return 0;
			}
			asDict = asArray.GetAsDict(0);
			if (asDict == null)
			{
				return 0;
			}
			asDict = asDict.GetAsDict(PdfName.TRANSFORMPARAMS);
			if (asDict == null)
			{
				return 0;
			}
			PdfNumber asNumber = asDict.GetAsNumber(PdfName.P);
			if (asNumber == null)
			{
				return 0;
			}
			return asNumber.IntValue;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00065BF2 File Offset: 0x00064BF2
		public bool IsOpenedWithFullPermissions
		{
			get
			{
				return !this.encrypted || this.ownerPasswordUsed || PdfReader.unethicalreading;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00065C0B File Offset: 0x00064C0B
		public int GetCryptoMode()
		{
			if (this.decrypt == null)
			{
				return -1;
			}
			return this.decrypt.GetCryptoMode();
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00065C22 File Offset: 0x00064C22
		public bool IsMetadataEncrypted()
		{
			return this.decrypt != null && this.decrypt.IsMetadataEncrypted();
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00065C39 File Offset: 0x00064C39
		public byte[] ComputeUserPassword()
		{
			if (!this.encrypted || !this.ownerPasswordUsed)
			{
				return null;
			}
			return this.decrypt.ComputeUserPassword(this.password);
		}

		// Token: 0x04000C59 RID: 3161
		public static bool unethicalreading = false;

		// Token: 0x04000C5A RID: 3162
		private static PdfName[] pageInhCandidates = new PdfName[]
		{
			PdfName.MEDIABOX,
			PdfName.ROTATE,
			PdfName.RESOURCES,
			PdfName.CROPBOX
		};

		// Token: 0x04000C5B RID: 3163
		private static byte[] endstream = PdfEncodings.ConvertToBytes("endstream", null);

		// Token: 0x04000C5C RID: 3164
		private static byte[] endobj = PdfEncodings.ConvertToBytes("endobj", null);

		// Token: 0x04000C5D RID: 3165
		protected internal PRTokeniser tokens;

		// Token: 0x04000C5E RID: 3166
		protected internal int[] xref;

		// Token: 0x04000C5F RID: 3167
		protected internal Dictionary<int, IntHashtable> objStmMark;

		// Token: 0x04000C60 RID: 3168
		protected internal IntHashtable objStmToOffset;

		// Token: 0x04000C61 RID: 3169
		protected internal bool newXrefType;

		// Token: 0x04000C62 RID: 3170
		private List<PdfObject> xrefObj;

		// Token: 0x04000C63 RID: 3171
		private PdfDictionary rootPages;

		// Token: 0x04000C64 RID: 3172
		protected internal PdfDictionary trailer;

		// Token: 0x04000C65 RID: 3173
		protected internal PdfDictionary catalog;

		// Token: 0x04000C66 RID: 3174
		protected internal PdfReader.PageRefs pageRefs;

		// Token: 0x04000C67 RID: 3175
		protected internal PRAcroForm acroForm;

		// Token: 0x04000C68 RID: 3176
		protected internal bool acroFormParsed;

		// Token: 0x04000C69 RID: 3177
		protected internal bool encrypted;

		// Token: 0x04000C6A RID: 3178
		protected internal bool rebuilt;

		// Token: 0x04000C6B RID: 3179
		protected internal int freeXref;

		// Token: 0x04000C6C RID: 3180
		protected internal bool tampered;

		// Token: 0x04000C6D RID: 3181
		protected internal int lastXref;

		// Token: 0x04000C6E RID: 3182
		protected internal int eofPos;

		// Token: 0x04000C6F RID: 3183
		protected internal char pdfVersion;

		// Token: 0x04000C70 RID: 3184
		protected internal PdfEncryption decrypt;

		// Token: 0x04000C71 RID: 3185
		protected internal byte[] password;

		// Token: 0x04000C72 RID: 3186
		protected ICipherParameters certificateKey;

		// Token: 0x04000C73 RID: 3187
		protected X509Certificate certificate;

		// Token: 0x04000C74 RID: 3188
		private bool ownerPasswordUsed;

		// Token: 0x04000C75 RID: 3189
		protected internal List<PdfString> strings = new List<PdfString>();

		// Token: 0x04000C76 RID: 3190
		protected internal bool sharedStreams = true;

		// Token: 0x04000C77 RID: 3191
		protected internal bool consolidateNamedDestinations;

		// Token: 0x04000C78 RID: 3192
		protected bool remoteToLocalNamedDestinations;

		// Token: 0x04000C79 RID: 3193
		protected internal int rValue;

		// Token: 0x04000C7A RID: 3194
		protected internal int pValue;

		// Token: 0x04000C7B RID: 3195
		private int objNum;

		// Token: 0x04000C7C RID: 3196
		private int objGen;

		// Token: 0x04000C7D RID: 3197
		private int fileLength;

		// Token: 0x04000C7E RID: 3198
		private bool hybridXref;

		// Token: 0x04000C7F RID: 3199
		private int lastXrefPartial = -1;

		// Token: 0x04000C80 RID: 3200
		private bool partial;

		// Token: 0x04000C81 RID: 3201
		private PRIndirectReference cryptoRef;

		// Token: 0x04000C82 RID: 3202
		private PdfViewerPreferencesImp viewerPreferences = new PdfViewerPreferencesImp();

		// Token: 0x04000C83 RID: 3203
		private bool encryptionError;

		// Token: 0x04000C84 RID: 3204
		private bool appendable;

		// Token: 0x04000C85 RID: 3205
		private int readDepth;

		// Token: 0x020001C8 RID: 456
		public class PageRefs
		{
			// Token: 0x060011C2 RID: 4546 RVA: 0x00065CC0 File Offset: 0x00064CC0
			internal PageRefs(PdfReader reader)
			{
				this.reader = reader;
				if (reader.partial)
				{
					this.refsp = new IntHashtable();
					PdfNumber pdfNumber = (PdfNumber)PdfReader.GetPdfObjectRelease(reader.rootPages.Get(PdfName.COUNT));
					this.sizep = pdfNumber.IntValue;
					return;
				}
				this.ReadPages();
			}

			// Token: 0x060011C3 RID: 4547 RVA: 0x00065D24 File Offset: 0x00064D24
			internal PageRefs(PdfReader.PageRefs other, PdfReader reader)
			{
				this.reader = reader;
				this.sizep = other.sizep;
				if (other.refsn != null)
				{
					this.refsn = new List<PRIndirectReference>(other.refsn);
					for (int i = 0; i < this.refsn.Count; i++)
					{
						this.refsn[i] = (PRIndirectReference)PdfReader.DuplicatePdfObject(this.refsn[i], reader);
					}
					return;
				}
				this.refsp = other.refsp.Clone();
			}

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00065DB5 File Offset: 0x00064DB5
			internal int Size
			{
				get
				{
					if (this.refsn != null)
					{
						return this.refsn.Count;
					}
					return this.sizep;
				}
			}

			// Token: 0x060011C5 RID: 4549 RVA: 0x00065DD4 File Offset: 0x00064DD4
			internal void ReadPages()
			{
				if (this.refsn != null)
				{
					return;
				}
				this.refsp = null;
				this.refsn = new List<PRIndirectReference>();
				this.pageInh = new List<PdfDictionary>();
				this.IteratePages((PRIndirectReference)this.reader.catalog.Get(PdfName.PAGES));
				this.pageInh = null;
				this.reader.rootPages.Put(PdfName.COUNT, new PdfNumber(this.refsn.Count));
			}

			// Token: 0x060011C6 RID: 4550 RVA: 0x00065E53 File Offset: 0x00064E53
			internal void ReReadPages()
			{
				this.refsn = null;
				this.ReadPages();
			}

			// Token: 0x060011C7 RID: 4551 RVA: 0x00065E64 File Offset: 0x00064E64
			public PdfDictionary GetPageN(int pageNum)
			{
				PRIndirectReference pageOrigRef = this.GetPageOrigRef(pageNum);
				return (PdfDictionary)PdfReader.GetPdfObject(pageOrigRef);
			}

			// Token: 0x060011C8 RID: 4552 RVA: 0x00065E84 File Offset: 0x00064E84
			public PdfDictionary GetPageNRelease(int pageNum)
			{
				PdfDictionary pageN = this.GetPageN(pageNum);
				this.ReleasePage(pageNum);
				return pageN;
			}

			// Token: 0x060011C9 RID: 4553 RVA: 0x00065EA4 File Offset: 0x00064EA4
			public PRIndirectReference GetPageOrigRefRelease(int pageNum)
			{
				PRIndirectReference pageOrigRef = this.GetPageOrigRef(pageNum);
				this.ReleasePage(pageNum);
				return pageOrigRef;
			}

			// Token: 0x060011CA RID: 4554 RVA: 0x00065EC4 File Offset: 0x00064EC4
			public PRIndirectReference GetPageOrigRef(int pageNum)
			{
				pageNum--;
				if (pageNum < 0 || pageNum >= this.Size)
				{
					return null;
				}
				if (this.refsn != null)
				{
					return this.refsn[pageNum];
				}
				int num = this.refsp[pageNum];
				if (num == 0)
				{
					PRIndirectReference singlePage = this.GetSinglePage(pageNum);
					if (this.reader.lastXrefPartial == -1)
					{
						this.lastPageRead = -1;
					}
					else
					{
						this.lastPageRead = pageNum;
					}
					this.reader.lastXrefPartial = -1;
					this.refsp[pageNum] = singlePage.Number;
					if (this.keepPages)
					{
						this.lastPageRead = -1;
					}
					return singlePage;
				}
				if (this.lastPageRead != pageNum)
				{
					this.lastPageRead = -1;
				}
				if (this.keepPages)
				{
					this.lastPageRead = -1;
				}
				return new PRIndirectReference(this.reader, num);
			}

			// Token: 0x060011CB RID: 4555 RVA: 0x00065F8A File Offset: 0x00064F8A
			internal void KeepPages()
			{
				if (this.refsp == null || this.keepPages)
				{
					return;
				}
				this.keepPages = true;
				this.refsp.Clear();
			}

			// Token: 0x060011CC RID: 4556 RVA: 0x00065FB0 File Offset: 0x00064FB0
			public void ReleasePage(int pageNum)
			{
				if (this.refsp == null)
				{
					return;
				}
				pageNum--;
				if (pageNum < 0 || pageNum >= this.Size)
				{
					return;
				}
				if (pageNum != this.lastPageRead)
				{
					return;
				}
				this.lastPageRead = -1;
				this.reader.lastXrefPartial = this.refsp[pageNum];
				this.reader.ReleaseLastXrefPartial();
				this.refsp.Remove(pageNum);
			}

			// Token: 0x060011CD RID: 4557 RVA: 0x00066019 File Offset: 0x00065019
			public void ResetReleasePage()
			{
				if (this.refsp == null)
				{
					return;
				}
				this.lastPageRead = -1;
			}

			// Token: 0x060011CE RID: 4558 RVA: 0x0006602C File Offset: 0x0006502C
			internal void InsertPage(int pageNum, PRIndirectReference refi)
			{
				pageNum--;
				if (this.refsn != null)
				{
					if (pageNum >= this.refsn.Count)
					{
						this.refsn.Add(refi);
						return;
					}
					this.refsn.Insert(pageNum, refi);
					return;
				}
				else
				{
					this.sizep++;
					this.lastPageRead = -1;
					if (pageNum >= this.Size)
					{
						this.refsp[this.Size] = refi.Number;
						return;
					}
					IntHashtable intHashtable = new IntHashtable((this.refsp.Size + 1) * 2);
					IntHashtable.IntHashtableIterator entryIterator = this.refsp.GetEntryIterator();
					while (entryIterator.HasNext())
					{
						IntHashtable.IntHashtableEntry intHashtableEntry = entryIterator.Next();
						int key = intHashtableEntry.Key;
						intHashtable[(key >= pageNum) ? (key + 1) : key] = intHashtableEntry.Value;
					}
					intHashtable[pageNum] = refi.Number;
					this.refsp = intHashtable;
					return;
				}
			}

			// Token: 0x060011CF RID: 4559 RVA: 0x00066108 File Offset: 0x00065108
			private void PushPageAttributes(PdfDictionary nodePages)
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				if (this.pageInh.Count != 0)
				{
					pdfDictionary.Merge(this.pageInh[this.pageInh.Count - 1]);
				}
				for (int i = 0; i < PdfReader.pageInhCandidates.Length; i++)
				{
					PdfObject pdfObject = nodePages.Get(PdfReader.pageInhCandidates[i]);
					if (pdfObject != null)
					{
						pdfDictionary.Put(PdfReader.pageInhCandidates[i], pdfObject);
					}
				}
				this.pageInh.Add(pdfDictionary);
			}

			// Token: 0x060011D0 RID: 4560 RVA: 0x00066183 File Offset: 0x00065183
			private void PopPageAttributes()
			{
				this.pageInh.RemoveAt(this.pageInh.Count - 1);
			}

			// Token: 0x060011D1 RID: 4561 RVA: 0x000661A0 File Offset: 0x000651A0
			private void IteratePages(PRIndirectReference rpage)
			{
				PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(rpage);
				PdfArray asArray = pdfDictionary.GetAsArray(PdfName.KIDS);
				if (asArray == null)
				{
					pdfDictionary.Put(PdfName.TYPE, PdfName.PAGE);
					PdfDictionary pdfDictionary2 = this.pageInh[this.pageInh.Count - 1];
					foreach (PdfName key in pdfDictionary2.Keys)
					{
						if (pdfDictionary.Get(key) == null)
						{
							pdfDictionary.Put(key, pdfDictionary2.Get(key));
						}
					}
					if (pdfDictionary.Get(PdfName.MEDIABOX) == null)
					{
						PdfArray value = new PdfArray(new float[]
						{
							0f,
							0f,
							PageSize.LETTER.Right,
							PageSize.LETTER.Top
						});
						pdfDictionary.Put(PdfName.MEDIABOX, value);
					}
					this.refsn.Add(rpage);
					return;
				}
				pdfDictionary.Put(PdfName.TYPE, PdfName.PAGES);
				this.PushPageAttributes(pdfDictionary);
				for (int i = 0; i < asArray.Size; i++)
				{
					PdfObject pdfObject = asArray[i];
					if (!pdfObject.IsIndirect())
					{
						while (i < asArray.Size)
						{
							asArray.Remove(i);
						}
						break;
					}
					this.IteratePages((PRIndirectReference)pdfObject);
				}
				this.PopPageAttributes();
			}

			// Token: 0x060011D2 RID: 4562 RVA: 0x00066308 File Offset: 0x00065308
			protected internal PRIndirectReference GetSinglePage(int n)
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				PdfDictionary pdfDictionary2 = this.reader.rootPages;
				int num = 0;
				PRIndirectReference prindirectReference;
				PdfDictionary pdfDictionary3;
				for (;;)
				{
					for (int i = 0; i < PdfReader.pageInhCandidates.Length; i++)
					{
						PdfObject pdfObject = pdfDictionary2.Get(PdfReader.pageInhCandidates[i]);
						if (pdfObject != null)
						{
							pdfDictionary.Put(PdfReader.pageInhCandidates[i], pdfObject);
						}
					}
					PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(pdfDictionary2.Get(PdfName.KIDS));
					ListIterator<PdfObject> listIterator = new ListIterator<PdfObject>(pdfArray.ArrayList);
					while (listIterator.HasNext())
					{
						prindirectReference = (PRIndirectReference)listIterator.Next();
						pdfDictionary3 = (PdfDictionary)PdfReader.GetPdfObject(prindirectReference);
						int lastXrefPartial = this.reader.lastXrefPartial;
						PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(pdfDictionary3.Get(PdfName.COUNT));
						this.reader.lastXrefPartial = lastXrefPartial;
						int num2 = 1;
						if (pdfObjectRelease != null && pdfObjectRelease.Type == 2)
						{
							num2 = ((PdfNumber)pdfObjectRelease).IntValue;
						}
						if (n < num + num2)
						{
							if (pdfObjectRelease == null)
							{
								goto Block_6;
							}
							this.reader.ReleaseLastXrefPartial();
							pdfDictionary2 = pdfDictionary3;
							break;
						}
						else
						{
							this.reader.ReleaseLastXrefPartial();
							num += num2;
						}
					}
				}
				Block_6:
				pdfDictionary3.MergeDifferent(pdfDictionary);
				return prindirectReference;
			}

			// Token: 0x060011D3 RID: 4563 RVA: 0x00066438 File Offset: 0x00065438
			internal void SelectPages(ICollection<int> pagesToKeep)
			{
				IntHashtable intHashtable = new IntHashtable();
				List<int> list = new List<int>();
				int size = this.Size;
				foreach (int num in pagesToKeep)
				{
					if (num >= 1 && num <= size && !intHashtable.ContainsKey(num))
					{
						intHashtable[num] = 1;
						list.Add(num);
					}
				}
				if (this.reader.partial)
				{
					for (int i = 1; i <= size; i++)
					{
						this.GetPageOrigRef(i);
						this.ResetReleasePage();
					}
				}
				PRIndirectReference prindirectReference = (PRIndirectReference)this.reader.catalog.Get(PdfName.PAGES);
				PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(prindirectReference);
				List<PRIndirectReference> list2 = new List<PRIndirectReference>(list.Count);
				PdfArray pdfArray = new PdfArray();
				foreach (int pageNum in list)
				{
					PRIndirectReference pageOrigRef = this.GetPageOrigRef(pageNum);
					this.ResetReleasePage();
					pdfArray.Add(pageOrigRef);
					list2.Add(pageOrigRef);
					this.GetPageN(pageNum).Put(PdfName.PARENT, prindirectReference);
				}
				AcroFields acroFields = this.reader.AcroFields;
				bool flag = acroFields.Fields.Count > 0;
				for (int j = 1; j <= size; j++)
				{
					if (!intHashtable.ContainsKey(j))
					{
						if (flag)
						{
							acroFields.RemoveFieldsFromPage(j);
						}
						PRIndirectReference pageOrigRef2 = this.GetPageOrigRef(j);
						int number = pageOrigRef2.Number;
						this.reader.xrefObj[number] = null;
						if (this.reader.partial)
						{
							this.reader.xref[number * 2] = -1;
							this.reader.xref[number * 2 + 1] = 0;
						}
					}
				}
				pdfDictionary.Put(PdfName.COUNT, new PdfNumber(list.Count));
				pdfDictionary.Put(PdfName.KIDS, pdfArray);
				this.refsp = null;
				this.refsn = list2;
			}

			// Token: 0x04000C86 RID: 3206
			private PdfReader reader;

			// Token: 0x04000C87 RID: 3207
			private IntHashtable refsp;

			// Token: 0x04000C88 RID: 3208
			private List<PRIndirectReference> refsn;

			// Token: 0x04000C89 RID: 3209
			private List<PdfDictionary> pageInh;

			// Token: 0x04000C8A RID: 3210
			private int lastPageRead = -1;

			// Token: 0x04000C8B RID: 3211
			private int sizep;

			// Token: 0x04000C8C RID: 3212
			private bool keepPages;
		}
	}
}
