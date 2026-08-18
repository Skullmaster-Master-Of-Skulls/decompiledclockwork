using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using Telerik.Pdf.Security;

namespace Telerik.Pdf
{
	// Token: 0x02001676 RID: 5750
	public class PdfWriter
	{
		// Token: 0x0600DE46 RID: 56902 RVA: 0x00309184 File Offset: 0x00307384
		public PdfWriter(Stream stream)
		{
			this.stream = stream;
		}

		// Token: 0x17004401 RID: 17409
		// (get) Token: 0x0600DE47 RID: 56903 RVA: 0x003091D9 File Offset: 0x003073D9
		// (set) Token: 0x0600DE48 RID: 56904 RVA: 0x003091E1 File Offset: 0x003073E1
		public SecurityManager SecurityManager
		{
			get
			{
				return this.securityManager;
			}
			set
			{
				this.securityManager = value;
			}
		}

		// Token: 0x17004402 RID: 17410
		// (get) Token: 0x0600DE49 RID: 56905 RVA: 0x003091EA File Offset: 0x003073EA
		internal PdfObject EnclosingIndirect
		{
			get
			{
				return (PdfObject)this.indirectObjects.Peek();
			}
		}

		// Token: 0x0600DE4A RID: 56906 RVA: 0x003091FC File Offset: 0x003073FC
		public void Close()
		{
			this.stream.Close();
		}

		// Token: 0x0600DE4B RID: 56907 RVA: 0x00309209 File Offset: 0x00307409
		public void WriteHeader(PdfVersion version)
		{
			this.WriteLine(version.Header);
		}

		// Token: 0x0600DE4C RID: 56908 RVA: 0x00309217 File Offset: 0x00307417
		public void WriteBinaryComment()
		{
			this.WriteLine(this.binaryComment);
		}

		// Token: 0x0600DE4D RID: 56909 RVA: 0x00309225 File Offset: 0x00307425
		public void Write(PdfObject obj)
		{
			if (obj.IsIndirect)
			{
				this.indirectObjects.Push(obj);
				obj.WriteIndirect(this);
				this.indirectObjects.Pop();
				return;
			}
			obj.Write(this);
		}

		// Token: 0x0600DE4E RID: 56910 RVA: 0x00309256 File Offset: 0x00307456
		public void WriteLine(PdfObject obj)
		{
			this.Write(obj);
			this.WriteLine();
		}

		// Token: 0x0600DE4F RID: 56911 RVA: 0x00309268 File Offset: 0x00307468
		public void Write(int val)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(val.ToString());
			this.Write(bytes);
		}

		// Token: 0x0600DE50 RID: 56912 RVA: 0x0030928E File Offset: 0x0030748E
		public void WriteLine(int val)
		{
			this.Write(val);
			this.WriteLine();
		}

		// Token: 0x0600DE51 RID: 56913 RVA: 0x003092A0 File Offset: 0x003074A0
		public void Write(decimal val)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(val.ToString(CultureInfo.InvariantCulture));
			this.Write(bytes);
		}

		// Token: 0x0600DE52 RID: 56914 RVA: 0x003092CB File Offset: 0x003074CB
		public void WriteLine(decimal val)
		{
			this.Write(val);
			this.WriteLine();
		}

		// Token: 0x0600DE53 RID: 56915 RVA: 0x003092DA File Offset: 0x003074DA
		public void WriteSpace()
		{
			this.stream.Write(this.space, 0, this.space.Length);
			this.position += (long)this.space.Length;
		}

		// Token: 0x0600DE54 RID: 56916 RVA: 0x0030930C File Offset: 0x0030750C
		public void WriteLine()
		{
			this.stream.Write(this.newLine, 0, this.newLine.Length);
			this.position += (long)this.newLine.Length;
		}

		// Token: 0x0600DE55 RID: 56917 RVA: 0x0030933E File Offset: 0x0030753E
		public void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
			this.position += 1L;
		}

		// Token: 0x0600DE56 RID: 56918 RVA: 0x0030935B File Offset: 0x0030755B
		public void Write(byte[] data)
		{
			this.stream.Write(data, 0, data.Length);
			this.position += (long)data.Length;
		}

		// Token: 0x0600DE57 RID: 56919 RVA: 0x0030937E File Offset: 0x0030757E
		public void WriteLine(byte[] data)
		{
			this.Write(data);
			this.WriteLine();
		}

		// Token: 0x0600DE58 RID: 56920 RVA: 0x0030938D File Offset: 0x0030758D
		public void WriteKeyword(Keyword keyword)
		{
			this.Write(KeywordEntries.GetKeyword(keyword));
		}

		// Token: 0x0600DE59 RID: 56921 RVA: 0x0030939B File Offset: 0x0030759B
		public void WriteKeywordLine(Keyword keyword)
		{
			this.WriteKeyword(keyword);
			this.WriteLine();
		}

		// Token: 0x17004403 RID: 17411
		// (get) Token: 0x0600DE5A RID: 56922 RVA: 0x003093AA File Offset: 0x003075AA
		public long Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17004404 RID: 17412
		// (get) Token: 0x0600DE5B RID: 56923 RVA: 0x003093B2 File Offset: 0x003075B2
		// (set) Token: 0x0600DE5C RID: 56924 RVA: 0x003093BA File Offset: 0x003075BA
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public byte[] NewLine
		{
			get
			{
				return this.newLine;
			}
			set
			{
				this.newLine = value;
			}
		}

		// Token: 0x17004405 RID: 17413
		// (get) Token: 0x0600DE5D RID: 56925 RVA: 0x003093C3 File Offset: 0x003075C3
		// (set) Token: 0x0600DE5E RID: 56926 RVA: 0x003093CB File Offset: 0x003075CB
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public byte[] BinaryComment
		{
			get
			{
				return this.binaryComment;
			}
			set
			{
				this.binaryComment = value;
			}
		}

		// Token: 0x04003FEA RID: 16362
		public static readonly ROByteCollection DefaultNewLine = new ROByteCollection(new byte[]
		{
			13,
			10
		});

		// Token: 0x04003FEB RID: 16363
		public static readonly ROByteCollection DefaultSpace = new ROByteCollection(new byte[]
		{
			32
		});

		// Token: 0x04003FEC RID: 16364
		public static readonly ROByteCollection DefaultBinaryComment = new ROByteCollection(new byte[]
		{
			37,
			226,
			227,
			207,
			211
		});

		// Token: 0x04003FED RID: 16365
		private Stream stream;

		// Token: 0x04003FEE RID: 16366
		private long position;

		// Token: 0x04003FEF RID: 16367
		private SecurityManager securityManager;

		// Token: 0x04003FF0 RID: 16368
		private Stack indirectObjects = new Stack();

		// Token: 0x04003FF1 RID: 16369
		private byte[] newLine = PdfWriter.DefaultNewLine.ToArray();

		// Token: 0x04003FF2 RID: 16370
		private byte[] space = PdfWriter.DefaultSpace.ToArray();

		// Token: 0x04003FF3 RID: 16371
		private byte[] binaryComment = PdfWriter.DefaultBinaryComment.ToArray();
	}
}
