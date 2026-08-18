using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x0200000D RID: 13
	[__DynamicallyInvokable]
	public class ByteArrayContent : HttpContent
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00004505 File Offset: 0x00002705
		[__DynamicallyInvokable]
		public ByteArrayContent(byte[] content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this.content = content;
			this.offset = 0;
			this.count = content.Length;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004534 File Offset: 0x00002734
		[__DynamicallyInvokable]
		public ByteArrayContent(byte[] content, int offset, int count)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (offset < 0 || offset > content.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > content.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.content = content;
			this.offset = offset;
			this.count = count;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004596 File Offset: 0x00002796
		[__DynamicallyInvokable]
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			return Task.Factory.FromAsync<byte[], int, int>(new Func<byte[], int, int, AsyncCallback, object, IAsyncResult>(stream.BeginWrite), new Action<IAsyncResult>(stream.EndWrite), this.content, this.offset, this.count, null);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000045CF File Offset: 0x000027CF
		[__DynamicallyInvokable]
		protected internal override bool TryComputeLength(out long length)
		{
			length = (long)this.count;
			return true;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000045DB File Offset: 0x000027DB
		[__DynamicallyInvokable]
		protected override Task<Stream> CreateContentReadStreamAsync()
		{
			return Task.FromResult<Stream>(new MemoryStream(this.content, this.offset, this.count, false, false));
		}

		// Token: 0x04000085 RID: 133
		private byte[] content;

		// Token: 0x04000086 RID: 134
		private int offset;

		// Token: 0x04000087 RID: 135
		private int count;
	}
}
