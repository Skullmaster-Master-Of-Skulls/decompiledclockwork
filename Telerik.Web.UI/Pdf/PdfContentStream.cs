using System;
using System.IO;
using System.Text;

namespace Telerik.Pdf
{
	// Token: 0x0200164D RID: 5709
	public class PdfContentStream : PdfStream, IDisposable
	{
		// Token: 0x170043C1 RID: 17345
		// (get) Token: 0x0600DD50 RID: 56656 RVA: 0x00305AE3 File Offset: 0x00303CE3
		// (set) Token: 0x0600DD51 RID: 56657 RVA: 0x00305AEB File Offset: 0x00303CEB
		protected MemoryStream stream { get; set; }

		// Token: 0x170043C2 RID: 17346
		// (get) Token: 0x0600DD52 RID: 56658 RVA: 0x00305AF4 File Offset: 0x00303CF4
		// (set) Token: 0x0600DD53 RID: 56659 RVA: 0x00305AFC File Offset: 0x00303CFC
		protected PdfWriter streamData { get; set; }

		// Token: 0x0600DD54 RID: 56660 RVA: 0x00305B05 File Offset: 0x00303D05
		public PdfContentStream(PdfObjectId objectId) : base(objectId)
		{
			this.stream = new MemoryStream();
			this.streamData = new PdfWriter(this.stream);
		}

		// Token: 0x0600DD55 RID: 56661 RVA: 0x00305B2A File Offset: 0x00303D2A
		public void Write(PdfObject obj)
		{
			if (obj.IsIndirect || obj is PdfObjectReference)
			{
				throw new ArgumentException("Cannot write indirect PdfObject", "obj");
			}
			this.streamData.Write(obj);
		}

		// Token: 0x0600DD56 RID: 56662 RVA: 0x00305B58 File Offset: 0x00303D58
		public void WriteLine(PdfObject obj)
		{
			if (obj.IsIndirect || obj is PdfObjectReference)
			{
				throw new ArgumentException("Cannot write indirect PdfObject", "obj");
			}
			this.streamData.WriteLine(obj);
		}

		// Token: 0x0600DD57 RID: 56663 RVA: 0x00305B86 File Offset: 0x00303D86
		public void Write(string s)
		{
			this.streamData.Write(Encoding.Default.GetBytes(s));
		}

		// Token: 0x0600DD58 RID: 56664 RVA: 0x00305B9E File Offset: 0x00303D9E
		public void WriteLine(string s)
		{
			this.streamData.WriteLine(Encoding.Default.GetBytes(s));
		}

		// Token: 0x0600DD59 RID: 56665 RVA: 0x00305BB6 File Offset: 0x00303DB6
		public void Write(int val)
		{
			this.streamData.Write(val);
		}

		// Token: 0x0600DD5A RID: 56666 RVA: 0x00305BC4 File Offset: 0x00303DC4
		public void WriteLine(int val)
		{
			this.streamData.WriteLine(val);
		}

		// Token: 0x0600DD5B RID: 56667 RVA: 0x00305BD2 File Offset: 0x00303DD2
		public void Write(decimal val)
		{
			this.streamData.Write(val);
		}

		// Token: 0x0600DD5C RID: 56668 RVA: 0x00305BE0 File Offset: 0x00303DE0
		public void WriteLine(decimal val)
		{
			this.streamData.WriteLine(val);
		}

		// Token: 0x0600DD5D RID: 56669 RVA: 0x00305BEE File Offset: 0x00303DEE
		public void WriteSpace()
		{
			this.streamData.WriteSpace();
		}

		// Token: 0x0600DD5E RID: 56670 RVA: 0x00305BFB File Offset: 0x00303DFB
		public void WriteLine()
		{
			this.streamData.WriteLine();
		}

		// Token: 0x0600DD5F RID: 56671 RVA: 0x00305C08 File Offset: 0x00303E08
		public void WriteByte(byte value)
		{
			this.streamData.WriteByte(value);
		}

		// Token: 0x0600DD60 RID: 56672 RVA: 0x00305C16 File Offset: 0x00303E16
		public void Write(byte[] data)
		{
			this.streamData.Write(data);
		}

		// Token: 0x0600DD61 RID: 56673 RVA: 0x00305C24 File Offset: 0x00303E24
		public void WriteKeyword(Keyword keyword)
		{
			this.streamData.WriteKeyword(keyword);
		}

		// Token: 0x0600DD62 RID: 56674 RVA: 0x00305C32 File Offset: 0x00303E32
		public void WriteLine(byte[] data)
		{
			this.streamData.WriteLine(data);
		}

		// Token: 0x0600DD63 RID: 56675 RVA: 0x00305C40 File Offset: 0x00303E40
		protected internal override void Write(PdfWriter writer)
		{
			base.data = this.stream.ToArray();
			base.Write(writer);
		}

		// Token: 0x0600DD64 RID: 56676 RVA: 0x00305C5A File Offset: 0x00303E5A
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.stream != null)
			{
				this.stream.Close();
			}
		}

		// Token: 0x0600DD65 RID: 56677 RVA: 0x00305C72 File Offset: 0x00303E72
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
