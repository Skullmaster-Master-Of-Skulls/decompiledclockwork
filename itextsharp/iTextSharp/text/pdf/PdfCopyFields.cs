using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.pdf.interfaces;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000216 RID: 534
	public class PdfCopyFields : IPdfViewerPreferences, IPdfEncryptionSettings
	{
		// Token: 0x060014BD RID: 5309 RVA: 0x00075617 File Offset: 0x00074617
		public PdfCopyFields(Stream os)
		{
			this.fc = new PdfCopyFieldsImp(os);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0007562B File Offset: 0x0007462B
		public PdfCopyFields(Stream os, char pdfVersion)
		{
			this.fc = new PdfCopyFieldsImp(os, pdfVersion);
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00075640 File Offset: 0x00074640
		public void AddDocument(PdfReader reader)
		{
			this.fc.AddDocument(reader);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0007564E File Offset: 0x0007464E
		public void AddDocument(PdfReader reader, IList<int> pagesToKeep)
		{
			this.fc.AddDocument(reader, pagesToKeep);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0007565D File Offset: 0x0007465D
		public void AddDocument(PdfReader reader, string ranges)
		{
			this.fc.AddDocument(reader, SequenceList.Expand(ranges, reader.NumberOfPages));
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00075677 File Offset: 0x00074677
		public void SetEncryption(byte[] userPassword, byte[] ownerPassword, int permissions, bool strength128Bits)
		{
			this.fc.SetEncryption(userPassword, ownerPassword, permissions, strength128Bits ? 1 : 0);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0007568F File Offset: 0x0007468F
		public void SetEncryption(bool strength, string userPassword, string ownerPassword, int permissions)
		{
			this.SetEncryption(DocWriter.GetISOBytes(userPassword), DocWriter.GetISOBytes(ownerPassword), permissions, strength);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x000756A6 File Offset: 0x000746A6
		public void Close()
		{
			this.fc.Close();
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x000756B3 File Offset: 0x000746B3
		public void Open()
		{
			this.fc.OpenDoc();
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x000756C0 File Offset: 0x000746C0
		public void AddJavaScript(string js)
		{
			this.fc.AddJavaScript(js, !PdfEncodings.IsPdfDocEncoding(js));
		}

		// Token: 0x170003DC RID: 988
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x000756D7 File Offset: 0x000746D7
		public IList<Dictionary<string, object>> Outlines
		{
			set
			{
				this.fc.Outlines = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x000756E5 File Offset: 0x000746E5
		public PdfWriter Writer
		{
			get
			{
				return this.fc;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x000756ED File Offset: 0x000746ED
		public bool FullCompression
		{
			get
			{
				return this.fc.FullCompression;
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x000756FA File Offset: 0x000746FA
		public void SetFullCompression()
		{
			this.fc.SetFullCompression();
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00075707 File Offset: 0x00074707
		public void SetEncryption(byte[] userPassword, byte[] ownerPassword, int permissions, int encryptionType)
		{
			this.fc.SetEncryption(userPassword, ownerPassword, permissions, encryptionType);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00075719 File Offset: 0x00074719
		public void AddViewerPreference(PdfName key, PdfObject value)
		{
			this.fc.AddViewerPreference(key, value);
		}

		// Token: 0x170003DF RID: 991
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00075728 File Offset: 0x00074728
		public int ViewerPreferences
		{
			set
			{
				this.fc.ViewerPreferences = value;
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00075736 File Offset: 0x00074736
		public void SetEncryption(X509Certificate[] certs, int[] permissions, int encryptionType)
		{
			this.fc.SetEncryption(certs, permissions, encryptionType);
		}

		// Token: 0x04000E21 RID: 3617
		private PdfCopyFieldsImp fc;
	}
}
