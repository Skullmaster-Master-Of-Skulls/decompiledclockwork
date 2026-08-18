using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Internal;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200003E RID: 62
	public abstract class BufferedMediaTypeFormatter : MediaTypeFormatter
	{
		// Token: 0x06000238 RID: 568 RVA: 0x000088F7 File Offset: 0x00006AF7
		protected BufferedMediaTypeFormatter()
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000890A File Offset: 0x00006B0A
		protected BufferedMediaTypeFormatter(BufferedMediaTypeFormatter formatter) : base(formatter)
		{
			this.BufferSize = formatter.BufferSize;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000892A File Offset: 0x00006B2A
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00008932 File Offset: 0x00006B32
		public int BufferSize
		{
			get
			{
				return this._bufferSizeInBytes;
			}
			set
			{
				if (value < 0)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 0);
				}
				this._bufferSizeInBytes = value;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008956 File Offset: 0x00006B56
		public virtual void WriteToStream(Type type, object value, Stream writeStream, HttpContent content, CancellationToken cancellationToken)
		{
			this.WriteToStream(type, value, writeStream, content);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008964 File Offset: 0x00006B64
		public virtual void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			throw Error.NotSupported(Resources.MediaTypeFormatterCannotWriteSync, new object[]
			{
				base.GetType().Name
			});
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008991 File Offset: 0x00006B91
		public virtual object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this.ReadFromStream(type, readStream, content, formatterLogger);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000089A0 File Offset: 0x00006BA0
		public virtual object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			throw Error.NotSupported(Resources.MediaTypeFormatterCannotReadSync, new object[]
			{
				base.GetType().Name
			});
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000089CD File Offset: 0x00006BCD
		public sealed override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this.WriteToStreamAsync(type, value, writeStream, content, transportContext, CancellationToken.None);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000089E4 File Offset: 0x00006BE4
		public sealed override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (writeStream == null)
			{
				throw Error.ArgumentNull("writeStream");
			}
			Task result;
			try
			{
				this.WriteToStreamSync(type, value, writeStream, content, cancellationToken);
				result = TaskHelpers.Completed();
			}
			catch (Exception exception)
			{
				result = TaskHelpers.FromError(exception);
			}
			return result;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008A44 File Offset: 0x00006C44
		private void WriteToStreamSync(Type type, object value, Stream writeStream, HttpContent content, CancellationToken cancellationToken)
		{
			using (Stream bufferStream = BufferedMediaTypeFormatter.GetBufferStream(writeStream, this._bufferSizeInBytes))
			{
				this.WriteToStream(type, value, bufferStream, content, cancellationToken);
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008A88 File Offset: 0x00006C88
		public sealed override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this.ReadFromStreamAsync(type, readStream, content, formatterLogger, CancellationToken.None);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008A9C File Offset: 0x00006C9C
		public sealed override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			Task<object> result;
			try
			{
				result = Task.FromResult<object>(this.ReadFromStreamSync(type, readStream, content, formatterLogger, cancellationToken));
			}
			catch (Exception exception)
			{
				result = TaskHelpers.FromError<object>(exception);
			}
			return result;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00008AFC File Offset: 0x00006CFC
		private object ReadFromStreamSync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			HttpContentHeaders httpContentHeaders = (content == null) ? null : content.Headers;
			object result;
			if (httpContentHeaders != null && httpContentHeaders.ContentLength == 0L)
			{
				result = MediaTypeFormatter.GetDefaultValueForType(type);
			}
			else
			{
				using (Stream bufferStream = BufferedMediaTypeFormatter.GetBufferStream(readStream, this._bufferSizeInBytes))
				{
					result = this.ReadFromStream(type, bufferStream, content, formatterLogger, cancellationToken);
				}
			}
			return result;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00008B78 File Offset: 0x00006D78
		private static Stream GetBufferStream(Stream innerStream, int bufferSize)
		{
			Stream stream = new NonClosingDelegatingStream(innerStream);
			return new BufferedStream(stream, bufferSize);
		}

		// Token: 0x04000098 RID: 152
		private const int MinBufferSize = 0;

		// Token: 0x04000099 RID: 153
		private const int DefaultBufferSize = 16384;

		// Token: 0x0400009A RID: 154
		private int _bufferSizeInBytes = 16384;
	}
}
