using System;
using iTextSharp.text.pdf.interfaces;

namespace iTextSharp.text.pdf.intern
{
	// Token: 0x020004E8 RID: 1256
	public class PdfVersionImp : IPdfVersion
	{
		// Token: 0x17000776 RID: 1910
		// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x00104E04 File Offset: 0x00103E04
		public char PdfVersion
		{
			set
			{
				if (this.headerWasWritten || this.appendmode)
				{
					this.SetPdfVersion(this.GetVersionAsName(value));
					return;
				}
				this.header_version = value;
			}
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x00104E2B File Offset: 0x00103E2B
		public void SetAtLeastPdfVersion(char version)
		{
			if (version > this.header_version)
			{
				this.PdfVersion = version;
			}
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x00104E3D File Offset: 0x00103E3D
		public void SetPdfVersion(PdfName version)
		{
			if (this.catalog_version == null || this.catalog_version.CompareTo(version) < 0)
			{
				this.catalog_version = version;
			}
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x00104E5D File Offset: 0x00103E5D
		public void SetAppendmode(bool appendmode)
		{
			this.appendmode = appendmode;
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x00104E68 File Offset: 0x00103E68
		public void WriteHeader(OutputStreamCounter os)
		{
			if (this.appendmode)
			{
				os.Write(PdfVersionImp.HEADER[0], 0, PdfVersionImp.HEADER[0].Length);
				return;
			}
			os.Write(PdfVersionImp.HEADER[1], 0, PdfVersionImp.HEADER[1].Length);
			os.Write(this.GetVersionAsByteArray(this.header_version), 0, this.GetVersionAsByteArray(this.header_version).Length);
			os.Write(PdfVersionImp.HEADER[2], 0, PdfVersionImp.HEADER[2].Length);
			this.headerWasWritten = true;
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x00104EEC File Offset: 0x00103EEC
		public PdfName GetVersionAsName(char version)
		{
			switch (version)
			{
			case '2':
				return PdfWriter.PDF_VERSION_1_2;
			case '3':
				return PdfWriter.PDF_VERSION_1_3;
			case '4':
				return PdfWriter.PDF_VERSION_1_4;
			case '5':
				return PdfWriter.PDF_VERSION_1_5;
			case '6':
				return PdfWriter.PDF_VERSION_1_6;
			case '7':
				return PdfWriter.PDF_VERSION_1_7;
			default:
				return PdfWriter.PDF_VERSION_1_4;
			}
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x00104F47 File Offset: 0x00103F47
		public byte[] GetVersionAsByteArray(char version)
		{
			return DocWriter.GetISOBytes(this.GetVersionAsName(version).ToString().Substring(1));
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x00104F60 File Offset: 0x00103F60
		public void AddToCatalog(PdfDictionary catalog)
		{
			if (this.catalog_version != null)
			{
				catalog.Put(PdfName.VERSION, this.catalog_version);
			}
			if (this.extensions != null)
			{
				catalog.Put(PdfName.EXTENSIONS, this.extensions);
			}
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x00104F94 File Offset: 0x00103F94
		public void AddDeveloperExtension(PdfDeveloperExtension de)
		{
			if (this.extensions == null)
			{
				this.extensions = new PdfDictionary();
			}
			else
			{
				PdfDictionary asDict = this.extensions.GetAsDict(de.Prefix);
				if (asDict != null)
				{
					int num = de.Baseversion.CompareTo(asDict.GetAsName(PdfName.BASEVERSION));
					if (num < 0)
					{
						return;
					}
					num = de.ExtensionLevel - asDict.GetAsNumber(PdfName.EXTENSIONLEVEL).IntValue;
					if (num <= 0)
					{
						return;
					}
				}
			}
			this.extensions.Put(de.Prefix, de.GetDeveloperExtensions());
		}

		// Token: 0x04001DB9 RID: 7609
		public static readonly byte[][] HEADER = new byte[][]
		{
			DocWriter.GetISOBytes("\n"),
			DocWriter.GetISOBytes("%PDF-"),
			DocWriter.GetISOBytes("\n%âãÏÓ\n")
		};

		// Token: 0x04001DBA RID: 7610
		protected bool headerWasWritten;

		// Token: 0x04001DBB RID: 7611
		protected bool appendmode;

		// Token: 0x04001DBC RID: 7612
		protected char header_version = '4';

		// Token: 0x04001DBD RID: 7613
		protected PdfName catalog_version;

		// Token: 0x04001DBE RID: 7614
		protected PdfDictionary extensions;
	}
}
