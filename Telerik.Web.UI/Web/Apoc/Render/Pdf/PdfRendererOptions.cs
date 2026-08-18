using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using Telerik.Pdf.Filter;
using Telerik.Pdf.Gdi;

namespace Telerik.Web.Apoc.Render.Pdf
{
	// Token: 0x0200169F RID: 5791
	public sealed class PdfRendererOptions : IRendererOptions
	{
		// Token: 0x17004488 RID: 17544
		// (get) Token: 0x0600DF9A RID: 57242 RVA: 0x0031C3A4 File Offset: 0x0031A5A4
		// (set) Token: 0x0600DF9B RID: 57243 RVA: 0x0031C3AC File Offset: 0x0031A5AC
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x17004489 RID: 17545
		// (get) Token: 0x0600DF9C RID: 57244 RVA: 0x0031C3B5 File Offset: 0x0031A5B5
		// (set) Token: 0x0600DF9D RID: 57245 RVA: 0x0031C3BD File Offset: 0x0031A5BD
		public string DefaultFontFamily
		{
			get
			{
				return this.defaultFontFamily;
			}
			set
			{
				this.defaultFontFamily = value;
			}
		}

		// Token: 0x1700448A RID: 17546
		// (get) Token: 0x0600DF9E RID: 57246 RVA: 0x0031C3C6 File Offset: 0x0031A5C6
		// (set) Token: 0x0600DF9F RID: 57247 RVA: 0x0031C3CE File Offset: 0x0031A5CE
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700448B RID: 17547
		// (get) Token: 0x0600DFA0 RID: 57248 RVA: 0x0031C3D7 File Offset: 0x0031A5D7
		// (set) Token: 0x0600DFA1 RID: 57249 RVA: 0x0031C3DF File Offset: 0x0031A5DF
		public string Author
		{
			get
			{
				return this.author;
			}
			set
			{
				this.author = value;
			}
		}

		// Token: 0x1700448C RID: 17548
		// (get) Token: 0x0600DFA2 RID: 57250 RVA: 0x0031C3E8 File Offset: 0x0031A5E8
		// (set) Token: 0x0600DFA3 RID: 57251 RVA: 0x0031C403 File Offset: 0x0031A603
		public string Creator
		{
			get
			{
				if (string.IsNullOrEmpty(this._creator))
				{
					return "Telerik RadGrid - http://www.telerik.com/";
				}
				return this._creator;
			}
			set
			{
				this._creator = value;
			}
		}

		// Token: 0x1700448D RID: 17549
		// (get) Token: 0x0600DFA4 RID: 57252 RVA: 0x0031C40C File Offset: 0x0031A60C
		// (set) Token: 0x0600DFA5 RID: 57253 RVA: 0x0031C44E File Offset: 0x0031A64E
		public string Producer
		{
			get
			{
				if (string.IsNullOrEmpty(this._producer))
				{
					AssemblyName name = Assembly.GetExecutingAssembly().GetName();
					return name.FullName + ", " + name.Version;
				}
				return this._producer;
			}
			set
			{
				this._producer = value;
			}
		}

		// Token: 0x1700448E RID: 17550
		// (get) Token: 0x0600DFA6 RID: 57254 RVA: 0x0031C458 File Offset: 0x0031A658
		internal string Keywords
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.keywords != null)
				{
					int i = 0;
					int count = this.keywords.Count;
					while (i < count)
					{
						stringBuilder.Append(this.keywords[i]);
						if (i != count - 1)
						{
							stringBuilder.Append(", ");
						}
						i++;
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600DFA7 RID: 57255 RVA: 0x0031C4B6 File Offset: 0x0031A6B6
		public void AddKeyword(string keyword)
		{
			if (this.keywords == null)
			{
				this.keywords = new StringCollection();
			}
			this.keywords.Add(keyword);
		}

		// Token: 0x1700448F RID: 17551
		// (get) Token: 0x0600DFA8 RID: 57256 RVA: 0x0031C4D8 File Offset: 0x0031A6D8
		// (set) Token: 0x0600DFA9 RID: 57257 RVA: 0x0031C4E0 File Offset: 0x0031A6E0
		public string OwnerPassword
		{
			get
			{
				return this.ownerPassword;
			}
			set
			{
				this.ownerPassword = value;
			}
		}

		// Token: 0x17004490 RID: 17552
		// (get) Token: 0x0600DFAA RID: 57258 RVA: 0x0031C4E9 File Offset: 0x0031A6E9
		// (set) Token: 0x0600DFAB RID: 57259 RVA: 0x0031C4F1 File Offset: 0x0031A6F1
		public string UserPassword
		{
			get
			{
				return this.userPassword;
			}
			set
			{
				this.userPassword = value;
			}
		}

		// Token: 0x17004491 RID: 17553
		// (get) Token: 0x0600DFAC RID: 57260 RVA: 0x0031C4FA File Offset: 0x0031A6FA
		// (set) Token: 0x0600DFAD RID: 57261 RVA: 0x0031C502 File Offset: 0x0031A702
		public bool DisableSecurity
		{
			get
			{
				return this.disableSecurity;
			}
			set
			{
				this.disableSecurity = value;
			}
		}

		// Token: 0x17004492 RID: 17554
		// (get) Token: 0x0600DFAE RID: 57262 RVA: 0x0031C50B File Offset: 0x0031A70B
		internal bool HasPermissions
		{
			get
			{
				return !this.DisableSecurity && this.permissions.Data != -4;
			}
		}

		// Token: 0x17004493 RID: 17555
		// (get) Token: 0x0600DFAF RID: 57263 RVA: 0x0031C529 File Offset: 0x0031A729
		internal int Permissions
		{
			get
			{
				return this.permissions.Data;
			}
		}

		// Token: 0x17004494 RID: 17556
		// (get) Token: 0x0600DFB0 RID: 57264 RVA: 0x0031C536 File Offset: 0x0031A736
		// (set) Token: 0x0600DFB1 RID: 57265 RVA: 0x0031C544 File Offset: 0x0031A744
		public bool EnablePrinting
		{
			get
			{
				return this.permissions[4];
			}
			set
			{
				this.permissions[4] = value;
			}
		}

		// Token: 0x17004495 RID: 17557
		// (get) Token: 0x0600DFB2 RID: 57266 RVA: 0x0031C553 File Offset: 0x0031A753
		// (set) Token: 0x0600DFB3 RID: 57267 RVA: 0x0031C561 File Offset: 0x0031A761
		public bool EnableModify
		{
			get
			{
				return this.permissions[8];
			}
			set
			{
				this.permissions[8] = value;
			}
		}

		// Token: 0x17004496 RID: 17558
		// (get) Token: 0x0600DFB4 RID: 57268 RVA: 0x0031C570 File Offset: 0x0031A770
		// (set) Token: 0x0600DFB5 RID: 57269 RVA: 0x0031C57F File Offset: 0x0031A77F
		public bool EnableCopy
		{
			get
			{
				return this.permissions[16];
			}
			set
			{
				this.permissions[16] = value;
			}
		}

		// Token: 0x17004497 RID: 17559
		// (get) Token: 0x0600DFB6 RID: 57270 RVA: 0x0031C58F File Offset: 0x0031A78F
		// (set) Token: 0x0600DFB7 RID: 57271 RVA: 0x0031C59E File Offset: 0x0031A79E
		public bool EnableAdd
		{
			get
			{
				return this.permissions[32];
			}
			set
			{
				this.permissions[32] = value;
			}
		}

		// Token: 0x17004498 RID: 17560
		// (get) Token: 0x0600DFB8 RID: 57272 RVA: 0x0031C5AE File Offset: 0x0031A7AE
		// (set) Token: 0x0600DFB9 RID: 57273 RVA: 0x0031C5B6 File Offset: 0x0031A7B6
		public FontType FontType
		{
			get
			{
				return this.fontType;
			}
			set
			{
				this.fontType = value;
			}
		}

		// Token: 0x17004499 RID: 17561
		// (get) Token: 0x0600DFBA RID: 57274 RVA: 0x0031C5BF File Offset: 0x0031A7BF
		// (set) Token: 0x0600DFBB RID: 57275 RVA: 0x0031C5C7 File Offset: 0x0031A7C7
		public bool Kerning
		{
			get
			{
				return this.enableKerning;
			}
			set
			{
				this.enableKerning = value;
			}
		}

		// Token: 0x1700449A RID: 17562
		// (get) Token: 0x0600DFBC RID: 57276 RVA: 0x0031C5D0 File Offset: 0x0031A7D0
		// (set) Token: 0x0600DFBD RID: 57277 RVA: 0x0031C5D8 File Offset: 0x0031A7D8
		public bool ForceTextWrap { get; set; }

		// Token: 0x1700449B RID: 17563
		// (get) Token: 0x0600DFBE RID: 57278 RVA: 0x0031C5E1 File Offset: 0x0031A7E1
		// (set) Token: 0x0600DFBF RID: 57279 RVA: 0x0031C5E9 File Offset: 0x0031A7E9
		public PdfRendererOptions.PdfFilter Filter { get; set; }

		// Token: 0x0600DFC0 RID: 57280 RVA: 0x0031C5F2 File Offset: 0x0031A7F2
		public void AddPrivateFont(FileInfo fileInfo)
		{
			this.privateFonts.AddFontFile(fileInfo);
		}

		// Token: 0x0600DFC1 RID: 57281 RVA: 0x0031C600 File Offset: 0x0031A800
		internal IFilter GetActiveFilter()
		{
			switch (this.Filter)
			{
			case PdfRendererOptions.PdfFilter.NoFilter:
				return null;
			case PdfRendererOptions.PdfFilter.Ascii85:
				return new Ascii85Filter();
			case PdfRendererOptions.PdfFilter.AsciiHex:
				return new AsciiHexFilter();
			case PdfRendererOptions.PdfFilter.Flate:
				return new FlateFilter();
			default:
				return null;
			}
		}

		// Token: 0x04004096 RID: 16534
		private string author;

		// Token: 0x04004097 RID: 16535
		private string subject;

		// Token: 0x04004098 RID: 16536
		private string title;

		// Token: 0x04004099 RID: 16537
		private StringCollection keywords;

		// Token: 0x0400409A RID: 16538
		private string ownerPassword;

		// Token: 0x0400409B RID: 16539
		private string userPassword;

		// Token: 0x0400409C RID: 16540
		private string defaultFontFamily;

		// Token: 0x0400409D RID: 16541
		private bool enableKerning;

		// Token: 0x0400409E RID: 16542
		private FontType fontType;

		// Token: 0x0400409F RID: 16543
		private GdiPrivateFontCollection privateFonts = new GdiPrivateFontCollection();

		// Token: 0x040040A0 RID: 16544
		private BitVector32 permissions = new BitVector32(-4);

		// Token: 0x040040A1 RID: 16545
		private string _creator = "";

		// Token: 0x040040A2 RID: 16546
		private string _producer = "";

		// Token: 0x040040A3 RID: 16547
		private bool disableSecurity;

		// Token: 0x020016A0 RID: 5792
		public enum PdfFilter
		{
			// Token: 0x040040A7 RID: 16551
			NoFilter,
			// Token: 0x040040A8 RID: 16552
			Ascii85,
			// Token: 0x040040A9 RID: 16553
			AsciiHex,
			// Token: 0x040040AA RID: 16554
			Flate,
			// Token: 0x040040AB RID: 16555
			CcittFax,
			// Token: 0x040040AC RID: 16556
			Dct,
			// Token: 0x040040AD RID: 16557
			Jbig2,
			// Token: 0x040040AE RID: 16558
			Lzw,
			// Token: 0x040040AF RID: 16559
			Rle
		}
	}
}
