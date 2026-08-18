using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200024D RID: 589
	internal class MimeWriter : BaseWriter
	{
		// Token: 0x06001662 RID: 5730 RVA: 0x00073F92 File Offset: 0x00072192
		internal MimeWriter(Stream stream, string boundary) : base(stream, false)
		{
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			this.boundaryBytes = Encoding.ASCII.GetBytes(boundary);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00073FC4 File Offset: 0x000721C4
		internal override void WriteHeaders(NameValueCollection headers, bool allowUnicode)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			foreach (object obj in headers)
			{
				string name = (string)obj;
				base.WriteHeader(name, headers[name], allowUnicode);
			}
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00074030 File Offset: 0x00072230
		internal IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			this.Close(multiAsyncResult);
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00074054 File Offset: 0x00072254
		internal void EndClose(IAsyncResult result)
		{
			MultiAsyncResult.End(result);
			this.stream.Close();
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00074068 File Offset: 0x00072268
		internal override void Close()
		{
			this.Close(null);
			this.stream.Close();
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0007407C File Offset: 0x0007227C
		private void Close(MultiAsyncResult multiResult)
		{
			this.bufferBuilder.Append(BaseWriter.CRLF);
			this.bufferBuilder.Append(MimeWriter.DASHDASH);
			this.bufferBuilder.Append(this.boundaryBytes);
			this.bufferBuilder.Append(MimeWriter.DASHDASH);
			this.bufferBuilder.Append(BaseWriter.CRLF);
			base.Flush(multiResult);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x000740E1 File Offset: 0x000722E1
		protected override void OnClose(object sender, EventArgs args)
		{
			if (this.contentStream != sender)
			{
				return;
			}
			this.contentStream.Flush();
			this.contentStream = null;
			this.writeBoundary = true;
			this.isInContent = false;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00074110 File Offset: 0x00072310
		protected override void CheckBoundary()
		{
			if (this.writeBoundary)
			{
				this.bufferBuilder.Append(BaseWriter.CRLF);
				this.bufferBuilder.Append(MimeWriter.DASHDASH);
				this.bufferBuilder.Append(this.boundaryBytes);
				this.bufferBuilder.Append(BaseWriter.CRLF);
				this.writeBoundary = false;
			}
		}

		// Token: 0x0400173F RID: 5951
		private static byte[] DASHDASH = new byte[]
		{
			45,
			45
		};

		// Token: 0x04001740 RID: 5952
		private byte[] boundaryBytes;

		// Token: 0x04001741 RID: 5953
		private bool writeBoundary = true;
	}
}
