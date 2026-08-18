using System;
using System.IO;
using System.Text;
using a;
using a.c;
using iTextSharp.text;

namespace MailBee.Pdf
{
	// Token: 0x02000014 RID: 20
	public class HtmlToPdf
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000066A3 File Offset: 0x000056A3
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000066AF File Offset: 0x000056AF
		[Obsolete("This property is obsolete. Use MailBee.Global.LicenseKey instead.")]
		public static string LicenseKey
		{
			get
			{
				return Resources.Instance.LicenseKeyIsWriteOnlyWarning;
			}
			set
			{
				Global.u = bn.a(value, typeof(HtmlToPdf));
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000066C6 File Offset: 0x000056C6
		internal static bm License
		{
			get
			{
				return Global.u;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000066CD File Offset: 0x000056CD
		internal static void a(string A_0)
		{
			Global.a(typeof(HtmlToPdf), A_0);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000066DF File Offset: 0x000056DF
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000066EB File Offset: 0x000056EB
		public HtmlToPdf() : this(null)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000066F4 File Offset: 0x000056F4
		public HtmlToPdf(string licenseKey)
		{
			HtmlToPdf.a(licenseKey);
			this.a = new global::a.c.s();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000670D File Offset: 0x0000570D
		public HtmlToPdf(bool autodetectSourceEncoding, Encoding sourceEncoding, string pdfCharset, string systemFontsFolder)
		{
			HtmlToPdf.a(null);
			this.a = new global::a.c.s(autodetectSourceEncoding, sourceEncoding, pdfCharset, systemFontsFolder);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00006739 File Offset: 0x00005739
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000672B File Offset: 0x0000572B
		[CLSCompliant(false)]
		public Rectangle PageRectangle
		{
			get
			{
				return this.a.m();
			}
			set
			{
				this.a.a(value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00006746 File Offset: 0x00005746
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00006753 File Offset: 0x00005753
		[CLSCompliant(false)]
		public Font DefaultFont
		{
			get
			{
				return this.a.o();
			}
			set
			{
				this.a.b(value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000676F File Offset: 0x0000576F
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00006761 File Offset: 0x00005761
		[CLSCompliant(false)]
		public float DefaultFontSize
		{
			get
			{
				return this.a.b();
			}
			set
			{
				this.a.a(value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000678A File Offset: 0x0000578A
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000677C File Offset: 0x0000577C
		[CLSCompliant(false)]
		public ConvertXmlNodeToPdfDelegate OnConvertXmlNodeToPdf
		{
			get
			{
				return this.a.h();
			}
			set
			{
				this.a.a(value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000067A5 File Offset: 0x000057A5
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00006797 File Offset: 0x00005797
		public ProcessImagePathDelegate OnProcessImagePath
		{
			get
			{
				return this.a.r();
			}
			set
			{
				this.a.a(value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000067C0 File Offset: 0x000057C0
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000067B2 File Offset: 0x000057B2
		public bool UseBlackAndWhiteStyle
		{
			get
			{
				return this.a.f();
			}
			set
			{
				this.a.a(value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000067DB File Offset: 0x000057DB
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000067CD File Offset: 0x000057CD
		public PdfSourceType SourceType
		{
			get
			{
				return this.a.q();
			}
			set
			{
				this.a.a(value);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000067E8 File Offset: 0x000057E8
		public void Convert(string source, Stream outputStream)
		{
			this.a.b(source, outputStream);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000067F7 File Offset: 0x000057F7
		public void Convert(string source, string pdfFile)
		{
			this.a.b(source, pdfFile);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006806 File Offset: 0x00005806
		public void Convert(Stream sourceStream, Stream outputStream)
		{
			this.a.a(sourceStream, outputStream);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006815 File Offset: 0x00005815
		public void Convert(Stream sourceStream, string pdfFile)
		{
			this.a.a(sourceStream, pdfFile);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006824 File Offset: 0x00005824
		public void Convert(Uri sourceUri, Stream outputStream)
		{
			this.a.a(sourceUri, outputStream);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006833 File Offset: 0x00005833
		public void Convert(Uri sourceUri, string pdfFile)
		{
			this.a.a(sourceUri, pdfFile);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006842 File Offset: 0x00005842
		public void ConvertFile(string sourceFile, Stream outputStream)
		{
			this.a.a(sourceFile, outputStream);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006851 File Offset: 0x00005851
		public void ConvertFile(string sourceFile, string pdfFile)
		{
			this.a.a(sourceFile, pdfFile);
		}

		// Token: 0x04000064 RID: 100
		private global::a.c.s a;
	}
}
