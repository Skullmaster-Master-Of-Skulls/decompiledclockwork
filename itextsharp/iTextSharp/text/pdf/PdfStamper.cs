using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.collection;
using iTextSharp.text.pdf.interfaces;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x020001C6 RID: 454
	public class PdfStamper : IPdfViewerPreferences, IPdfEncryptionSettings
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x00060206 File Offset: 0x0005F206
		public PdfStamper(PdfReader reader, Stream os)
		{
			this.stamper = new PdfStamperImp(reader, os, '\0', false);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0006021D File Offset: 0x0005F21D
		public PdfStamper(PdfReader reader, Stream os, char pdfVersion)
		{
			this.stamper = new PdfStamperImp(reader, os, pdfVersion, false);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00060234 File Offset: 0x0005F234
		public PdfStamper(PdfReader reader, Stream os, char pdfVersion, bool append)
		{
			this.stamper = new PdfStamperImp(reader, os, pdfVersion, append);
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x00060255 File Offset: 0x0005F255
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x0006024C File Offset: 0x0005F24C
		public Dictionary<string, string> MoreInfo
		{
			get
			{
				return this.moreInfo;
			}
			set
			{
				this.moreInfo = value;
			}
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0006025D File Offset: 0x0005F25D
		public void ReplacePage(PdfReader r, int pageImported, int pageReplaced)
		{
			this.stamper.ReplacePage(r, pageImported, pageReplaced);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0006026D File Offset: 0x0005F26D
		public void InsertPage(int pageNumber, Rectangle mediabox)
		{
			this.stamper.InsertPage(pageNumber, mediabox);
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x0006027C File Offset: 0x0005F27C
		public PdfSignatureAppearance SignatureAppearance
		{
			get
			{
				return this.sigApp;
			}
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00060284 File Offset: 0x0005F284
		public void Close()
		{
			if (!this.hasSignature)
			{
				this.stamper.Close(this.moreInfo);
				return;
			}
			this.sigApp.PreClose();
			PdfSigGenericPKCS sigStandard = this.sigApp.SigStandard;
			PdfLiteral pdfLiteral = (PdfLiteral)sigStandard.Get(PdfName.CONTENTS);
			int num = (pdfLiteral.PosLength - 2) / 2;
			byte[] array = new byte[8192];
			Stream rangeStream = this.sigApp.RangeStream;
			int len;
			while ((len = rangeStream.Read(array, 0, array.Length)) > 0)
			{
				sigStandard.Signer.Update(array, 0, len);
			}
			array = new byte[num];
			byte[] signerContents = sigStandard.SignerContents;
			Array.Copy(signerContents, 0, array, 0, signerContents.Length);
			PdfString pdfString = new PdfString(array);
			pdfString.SetHexWriting(true);
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.CONTENTS, pdfString);
			this.sigApp.Close(pdfDictionary);
			this.stamper.reader.Close();
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00060379 File Offset: 0x0005F379
		public PdfContentByte GetUnderContent(int pageNum)
		{
			return this.stamper.GetUnderContent(pageNum);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00060387 File Offset: 0x0005F387
		public PdfContentByte GetOverContent(int pageNum)
		{
			return this.stamper.GetOverContent(pageNum);
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x000603A3 File Offset: 0x0005F3A3
		// (set) Token: 0x06001112 RID: 4370 RVA: 0x00060395 File Offset: 0x0005F395
		public bool RotateContents
		{
			get
			{
				return this.stamper.RotateContents;
			}
			set
			{
				this.stamper.RotateContents = value;
			}
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000603B0 File Offset: 0x0005F3B0
		public void SetEncryption(byte[] userPassword, byte[] ownerPassword, int permissions, bool strength128Bits)
		{
			if (this.stamper.append)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("append.mode.does.not.support.changing.the.encryption.status"));
			}
			if (this.stamper.ContentWritten)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("content.was.already.written.to.the.output"));
			}
			this.stamper.SetEncryption(userPassword, ownerPassword, permissions, strength128Bits ? 1 : 0);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00060410 File Offset: 0x0005F410
		public void SetEncryption(byte[] userPassword, byte[] ownerPassword, int permissions, int encryptionType)
		{
			if (this.stamper.IsAppend())
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("append.mode.does.not.support.changing.the.encryption.status"));
			}
			if (this.stamper.ContentWritten)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("content.was.already.written.to.the.output"));
			}
			this.stamper.SetEncryption(userPassword, ownerPassword, permissions, encryptionType);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00060467 File Offset: 0x0005F467
		public void SetEncryption(bool strength, string userPassword, string ownerPassword, int permissions)
		{
			this.SetEncryption(DocWriter.GetISOBytes(userPassword), DocWriter.GetISOBytes(ownerPassword), permissions, strength);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0006047E File Offset: 0x0005F47E
		public void SetEncryption(int encryptionType, string userPassword, string ownerPassword, int permissions)
		{
			this.SetEncryption(DocWriter.GetISOBytes(userPassword), DocWriter.GetISOBytes(ownerPassword), permissions, encryptionType);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00060498 File Offset: 0x0005F498
		public void SetEncryption(X509Certificate[] certs, int[] permissions, int encryptionType)
		{
			if (this.stamper.IsAppend())
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("append.mode.does.not.support.changing.the.encryption.status"));
			}
			if (this.stamper.ContentWritten)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("content.was.already.written.to.the.output"));
			}
			this.stamper.SetEncryption(certs, permissions, encryptionType);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000604ED File Offset: 0x0005F4ED
		public PdfImportedPage GetImportedPage(PdfReader reader, int pageNumber)
		{
			return this.stamper.GetImportedPage(reader, pageNumber);
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x000604FC File Offset: 0x0005F4FC
		public PdfWriter Writer
		{
			get
			{
				return this.stamper;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00060504 File Offset: 0x0005F504
		public PdfReader Reader
		{
			get
			{
				return this.stamper.reader;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00060511 File Offset: 0x0005F511
		public AcroFields AcroFields
		{
			get
			{
				return this.stamper.AcroFields;
			}
		}

		// Token: 0x17000343 RID: 835
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x0006051E File Offset: 0x0005F51E
		public bool FormFlattening
		{
			set
			{
				this.stamper.FormFlattening = value;
			}
		}

		// Token: 0x17000344 RID: 836
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x0006052C File Offset: 0x0005F52C
		public bool FreeTextFlattening
		{
			set
			{
				this.stamper.FreeTextFlattening = value;
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0006053A File Offset: 0x0005F53A
		public void AddAnnotation(PdfAnnotation annot, int page)
		{
			this.stamper.AddAnnotation(annot, page);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0006054C File Offset: 0x0005F54C
		public PdfFormField AddSignature(string name, int page, float llx, float lly, float urx, float ury)
		{
			PdfAcroForm acroForm = this.stamper.AcroForm;
			PdfFormField pdfFormField = PdfFormField.CreateSignature(this.stamper);
			acroForm.SetSignatureParams(pdfFormField, name, llx, lly, urx, ury);
			acroForm.DrawSignatureAppearences(pdfFormField, llx, lly, urx, ury);
			this.AddAnnotation(pdfFormField, page);
			return pdfFormField;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00060597 File Offset: 0x0005F597
		public void AddComments(FdfReader fdf)
		{
			this.stamper.AddComments(fdf);
		}

		// Token: 0x17000345 RID: 837
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x000605A5 File Offset: 0x0005F5A5
		public IList<Dictionary<string, object>> Outlines
		{
			set
			{
				this.stamper.Outlines = value;
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000605B3 File Offset: 0x0005F5B3
		public void SetThumbnail(Image image, int page)
		{
			this.stamper.SetThumbnail(image, page);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000605C2 File Offset: 0x0005F5C2
		public bool PartialFormFlattening(string name)
		{
			return this.stamper.PartialFormFlattening(name);
		}

		// Token: 0x17000346 RID: 838
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x000605D0 File Offset: 0x0005F5D0
		public string JavaScript
		{
			set
			{
				this.stamper.AddJavaScript(value, !PdfEncodings.IsPdfDocEncoding(value));
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000605E7 File Offset: 0x0005F5E7
		public void AddFileAttachment(string description, byte[] fileStore, string file, string fileDisplay)
		{
			this.AddFileAttachment(description, PdfFileSpecification.FileEmbedded(this.stamper, file, fileDisplay, fileStore));
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x000605FF File Offset: 0x0005F5FF
		public void AddFileAttachment(string description, PdfFileSpecification fs)
		{
			this.stamper.AddFileAttachment(description, fs);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00060610 File Offset: 0x0005F610
		public void MakePackage(PdfName initialView)
		{
			PdfCollection pdfCollection = new PdfCollection(0);
			pdfCollection.Put(PdfName.VIEW, initialView);
			this.stamper.MakePackage(pdfCollection);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0006063C File Offset: 0x0005F63C
		public void MakePackage(PdfCollection collection)
		{
			this.stamper.MakePackage(collection);
		}

		// Token: 0x17000347 RID: 839
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x0006064A File Offset: 0x0005F64A
		public virtual int ViewerPreferences
		{
			set
			{
				this.stamper.ViewerPreferences = value;
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00060658 File Offset: 0x0005F658
		public virtual void AddViewerPreference(PdfName key, PdfObject value)
		{
			this.stamper.AddViewerPreference(key, value);
		}

		// Token: 0x17000348 RID: 840
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x00060667 File Offset: 0x0005F667
		public byte[] XmpMetadata
		{
			set
			{
				this.stamper.XmpMetadata = value;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x00060675 File Offset: 0x0005F675
		public bool FullCompression
		{
			get
			{
				return this.stamper.FullCompression;
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00060682 File Offset: 0x0005F682
		public void SetFullCompression()
		{
			if (this.stamper.append)
			{
				return;
			}
			this.stamper.SetFullCompression();
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0006069D File Offset: 0x0005F69D
		public void SetPageAction(PdfName actionType, PdfAction action, int page)
		{
			this.stamper.SetPageAction(actionType, action, page);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000606AD File Offset: 0x0005F6AD
		public void SetDuration(int seconds, int page)
		{
			this.stamper.SetDuration(seconds, page);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000606BC File Offset: 0x0005F6BC
		public void SetTransition(PdfTransition transition, int page)
		{
			this.stamper.SetTransition(transition, page);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000606CC File Offset: 0x0005F6CC
		public static PdfStamper CreateSignature(PdfReader reader, Stream os, char pdfVersion, string tempFile, bool append)
		{
			PdfStamper pdfStamper;
			if (tempFile == null)
			{
				ByteBuffer byteBuffer = new ByteBuffer();
				pdfStamper = new PdfStamper(reader, byteBuffer, pdfVersion, append);
				pdfStamper.sigApp = new PdfSignatureAppearance(pdfStamper.stamper);
				pdfStamper.sigApp.Sigout = byteBuffer;
			}
			else
			{
				if (Directory.Exists(tempFile))
				{
					tempFile = Path.GetTempFileName();
				}
				FileStream os2 = new FileStream(tempFile, FileMode.Create, FileAccess.Write);
				pdfStamper = new PdfStamper(reader, os2, pdfVersion, append);
				pdfStamper.sigApp = new PdfSignatureAppearance(pdfStamper.stamper);
				pdfStamper.sigApp.SetTempFile(tempFile);
			}
			pdfStamper.sigApp.Originalout = os;
			pdfStamper.sigApp.SetStamper(pdfStamper);
			pdfStamper.hasSignature = true;
			PdfDictionary catalog = reader.Catalog;
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(catalog.Get(PdfName.ACROFORM), catalog);
			if (pdfDictionary != null)
			{
				pdfDictionary.Remove(PdfName.NEEDAPPEARANCES);
				pdfStamper.stamper.MarkUsed(pdfDictionary);
			}
			return pdfStamper;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000607A8 File Offset: 0x0005F7A8
		public static PdfStamper CreateSignature(PdfReader reader, Stream os, char pdfVersion)
		{
			return PdfStamper.CreateSignature(reader, os, pdfVersion, null, false);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000607B4 File Offset: 0x0005F7B4
		public static PdfStamper CreateSignature(PdfReader reader, Stream os, char pdfVersion, string tempFile)
		{
			return PdfStamper.CreateSignature(reader, os, pdfVersion, tempFile, false);
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000607C0 File Offset: 0x0005F7C0
		public Dictionary<string, PdfLayer> GetPdfLayers()
		{
			return this.stamper.GetPdfLayers();
		}

		// Token: 0x04000C55 RID: 3157
		protected PdfStamperImp stamper;

		// Token: 0x04000C56 RID: 3158
		private Dictionary<string, string> moreInfo;

		// Token: 0x04000C57 RID: 3159
		private bool hasSignature;

		// Token: 0x04000C58 RID: 3160
		private PdfSignatureAppearance sigApp;
	}
}
