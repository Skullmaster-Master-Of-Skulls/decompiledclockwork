using System;
using System.IO;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200004C RID: 76
	public abstract class PdfObject
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x0000A63C File Offset: 0x0000963C
		protected PdfObject(int type)
		{
			this.type = type;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000A64B File Offset: 0x0000964B
		protected PdfObject(int type, string content)
		{
			this.type = type;
			this.bytes = PdfEncodings.ConvertToBytes(content, null);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A667 File Offset: 0x00009667
		protected PdfObject(int type, byte[] bytes)
		{
			this.bytes = bytes;
			this.type = type;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A67D File Offset: 0x0000967D
		public virtual void ToPdf(PdfWriter writer, Stream os)
		{
			if (this.bytes != null)
			{
				os.Write(this.bytes, 0, this.bytes.Length);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A69C File Offset: 0x0000969C
		public virtual byte[] GetBytes()
		{
			return this.bytes;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A6A4 File Offset: 0x000096A4
		public bool CanBeInObjStm()
		{
			switch (this.type)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 8:
				return true;
			}
			return false;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000A6ED File Offset: 0x000096ED
		public override string ToString()
		{
			if (this.bytes == null)
			{
				return "";
			}
			return PdfEncodings.ConvertToString(this.bytes, null);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000A709 File Offset: 0x00009709
		public int Length
		{
			get
			{
				return this.ToString().Length;
			}
		}

		// Token: 0x1700004A RID: 74
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000A716 File Offset: 0x00009716
		protected string Content
		{
			set
			{
				this.bytes = PdfEncodings.ConvertToBytes(value, null);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000A725 File Offset: 0x00009725
		public int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000A72D File Offset: 0x0000972D
		public bool IsNull()
		{
			return this.type == 8;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A738 File Offset: 0x00009738
		public bool IsBoolean()
		{
			return this.type == 1;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A743 File Offset: 0x00009743
		public bool IsNumber()
		{
			return this.type == 2;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A74E File Offset: 0x0000974E
		public bool IsString()
		{
			return this.type == 3;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A759 File Offset: 0x00009759
		public bool IsName()
		{
			return this.type == 4;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A764 File Offset: 0x00009764
		public bool IsArray()
		{
			return this.type == 5;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A76F File Offset: 0x0000976F
		public bool IsDictionary()
		{
			return this.type == 6;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A77A File Offset: 0x0000977A
		public bool IsStream()
		{
			return this.type == 7;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A785 File Offset: 0x00009785
		public bool IsIndirect()
		{
			return this.type == 10;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000A791 File Offset: 0x00009791
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000A799 File Offset: 0x00009799
		public PRIndirectReference IndRef
		{
			get
			{
				return this.indRef;
			}
			set
			{
				this.indRef = value;
			}
		}

		// Token: 0x040000DC RID: 220
		public const int BOOLEAN = 1;

		// Token: 0x040000DD RID: 221
		public const int NUMBER = 2;

		// Token: 0x040000DE RID: 222
		public const int STRING = 3;

		// Token: 0x040000DF RID: 223
		public const int NAME = 4;

		// Token: 0x040000E0 RID: 224
		public const int ARRAY = 5;

		// Token: 0x040000E1 RID: 225
		public const int DICTIONARY = 6;

		// Token: 0x040000E2 RID: 226
		public const int STREAM = 7;

		// Token: 0x040000E3 RID: 227
		public const int NULL = 8;

		// Token: 0x040000E4 RID: 228
		public const int INDIRECT = 10;

		// Token: 0x040000E5 RID: 229
		public const string NOTHING = "";

		// Token: 0x040000E6 RID: 230
		public const string TEXT_PDFDOCENCODING = "PDF";

		// Token: 0x040000E7 RID: 231
		public const string TEXT_UNICODE = "UnicodeBig";

		// Token: 0x040000E8 RID: 232
		protected byte[] bytes;

		// Token: 0x040000E9 RID: 233
		protected int type;

		// Token: 0x040000EA RID: 234
		protected PRIndirectReference indRef;
	}
}
