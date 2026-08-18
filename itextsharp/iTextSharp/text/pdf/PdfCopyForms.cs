using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.pdf.interfaces;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200045D RID: 1117
	public class PdfCopyForms : IPdfViewerPreferences, IPdfEncryptionSettings
	{
		// Token: 0x060025C9 RID: 9673 RVA: 0x000E443C File Offset: 0x000E343C
		public PdfCopyForms(Stream os)
		{
			this.fc = new PdfCopyFormsImp(os);
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000E4450 File Offset: 0x000E3450
		public void AddDocument(PdfReader reader)
		{
			this.fc.AddDocument(reader);
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000E445E File Offset: 0x000E345E
		public void AddDocument(PdfReader reader, ICollection<int> pagesToKeep)
		{
			this.fc.AddDocument(reader, pagesToKeep);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000E446D File Offset: 0x000E346D
		public void AddDocument(PdfReader reader, string ranges)
		{
			this.fc.AddDocument(reader, SequenceList.Expand(ranges, reader.NumberOfPages));
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000E4487 File Offset: 0x000E3487
		public void CopyDocumentFields(PdfReader reader)
		{
			this.fc.CopyDocumentFields(reader);
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000E4495 File Offset: 0x000E3495
		public void SetEncryption(byte[] userPassword, byte[] ownerPassword, int permissions, bool strength128Bits)
		{
			this.fc.SetEncryption(userPassword, ownerPassword, permissions, strength128Bits ? 1 : 0);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000E44AD File Offset: 0x000E34AD
		public void SetEncryption(bool strength, string userPassword, string ownerPassword, int permissions)
		{
			this.SetEncryption(DocWriter.GetISOBytes(userPassword), DocWriter.GetISOBytes(ownerPassword), permissions, strength);
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000E44C4 File Offset: 0x000E34C4
		public void Close()
		{
			this.fc.Close();
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000E44D1 File Offset: 0x000E34D1
		public void Open()
		{
			this.fc.OpenDoc();
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000E44DE File Offset: 0x000E34DE
		public void AddJavaScript(string js)
		{
			this.fc.AddJavaScript(js, !PdfEncodings.IsPdfDocEncoding(js));
		}

		// Token: 0x1700067E RID: 1662
		// (set) Token: 0x060025D3 RID: 9683 RVA: 0x000E44F5 File Offset: 0x000E34F5
		public IList<Dictionary<string, object>> Outlines
		{
			set
			{
				this.fc.Outlines = value;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x000E4503 File Offset: 0x000E3503
		public PdfWriter Writer
		{
			get
			{
				return this.fc;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x000E450B File Offset: 0x000E350B
		public bool FullCompression
		{
			get
			{
				return this.fc.FullCompression;
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000E4518 File Offset: 0x000E3518
		public void SetFullCompression()
		{
			this.fc.SetFullCompression();
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000E4525 File Offset: 0x000E3525
		public void SetEncryption(byte[] userPassword, byte[] ownerPassword, int permissions, int encryptionType)
		{
			this.fc.SetEncryption(userPassword, ownerPassword, permissions, encryptionType);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000E4537 File Offset: 0x000E3537
		public void AddViewerPreference(PdfName key, PdfObject value)
		{
			this.fc.AddViewerPreference(key, value);
		}

		// Token: 0x17000681 RID: 1665
		// (set) Token: 0x060025D9 RID: 9689 RVA: 0x000E4546 File Offset: 0x000E3546
		public int ViewerPreferences
		{
			set
			{
				this.fc.ViewerPreferences = value;
			}
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000E4554 File Offset: 0x000E3554
		public void SetEncryption(X509Certificate[] certs, int[] permissions, int encryptionType)
		{
			this.fc.SetEncryption(certs, permissions, encryptionType);
		}

		// Token: 0x04001A3F RID: 6719
		private PdfCopyFormsImp fc;
	}
}
