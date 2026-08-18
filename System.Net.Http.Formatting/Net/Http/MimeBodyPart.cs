using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Formatting.Parsers;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000068 RID: 104
	internal class MimeBodyPart : IDisposable
	{
		// Token: 0x06000381 RID: 897 RVA: 0x0000E568 File Offset: 0x0000C768
		public MimeBodyPart(MultipartStreamProvider streamProvider, int maxBodyPartHeaderSize, HttpContent parentContent)
		{
			this._streamProvider = streamProvider;
			this._parentContent = parentContent;
			this.Segments = new List<ArraySegment<byte>>(2);
			this._headers = FormattingUtilities.CreateEmptyContentHeaders();
			this.HeaderParser = new InternetMessageFormatHeaderParser(this._headers, maxBodyPartHeaderSize, true);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		public InternetMessageFormatHeaderParser HeaderParser { get; private set; }

		// Token: 0x06000384 RID: 900 RVA: 0x0000E5B9 File Offset: 0x0000C7B9
		public HttpContent GetCompletedHttpContent()
		{
			if (this._content == null)
			{
				return null;
			}
			this._headers.CopyTo(this._content.Headers);
			return this._content;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000E5E1 File Offset: 0x0000C7E1
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000E5E9 File Offset: 0x0000C7E9
		public List<ArraySegment<byte>> Segments { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000E5F2 File Offset: 0x0000C7F2
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000E5FA File Offset: 0x0000C7FA
		public bool IsComplete { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000E603 File Offset: 0x0000C803
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000E60B File Offset: 0x0000C80B
		public bool IsFinal { get; set; }

		// Token: 0x0600038B RID: 907 RVA: 0x0000E71C File Offset: 0x0000C91C
		public async Task WriteSegment(ArraySegment<byte> segment, CancellationToken cancellationToken)
		{
			Stream stream = this.GetOutputStream();
			await stream.WriteAsync(segment.Array, segment.Offset, segment.Count, cancellationToken);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000E774 File Offset: 0x0000C974
		private Stream GetOutputStream()
		{
			if (this._outputStream == null)
			{
				try
				{
					this._outputStream = this._streamProvider.GetStream(this._parentContent, this._headers);
				}
				catch (Exception innerException)
				{
					throw Error.InvalidOperation(innerException, Resources.ReadAsMimeMultipartStreamProviderException, new object[]
					{
						this._streamProvider.GetType().Name
					});
				}
				if (this._outputStream == null)
				{
					throw Error.InvalidOperation(Resources.ReadAsMimeMultipartStreamProviderNull, new object[]
					{
						this._streamProvider.GetType().Name,
						MimeBodyPart._streamType.Name
					});
				}
				if (!this._outputStream.CanWrite)
				{
					throw Error.InvalidOperation(Resources.ReadAsMimeMultipartStreamProviderReadOnly, new object[]
					{
						this._streamProvider.GetType().Name,
						MimeBodyPart._streamType.Name
					});
				}
				this._content = new StreamContent(this._outputStream);
			}
			return this._outputStream;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000E878 File Offset: 0x0000CA78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000E887 File Offset: 0x0000CA87
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.CleanupOutputStream();
				this.CleanupHttpContent();
				this._parentContent = null;
				this.HeaderParser = null;
				this.Segments.Clear();
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000E8B1 File Offset: 0x0000CAB1
		private void CleanupHttpContent()
		{
			if (!this.IsComplete && this._content != null)
			{
				this._content.Dispose();
			}
			this._content = null;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		private void CleanupOutputStream()
		{
			if (this._outputStream != null)
			{
				MemoryStream memoryStream = this._outputStream as MemoryStream;
				if (memoryStream != null)
				{
					memoryStream.Position = 0L;
				}
				else
				{
					this._outputStream.Close();
				}
				this._outputStream = null;
			}
		}

		// Token: 0x04000142 RID: 322
		private static readonly Type _streamType = typeof(Stream);

		// Token: 0x04000143 RID: 323
		private Stream _outputStream;

		// Token: 0x04000144 RID: 324
		private MultipartStreamProvider _streamProvider;

		// Token: 0x04000145 RID: 325
		private HttpContent _parentContent;

		// Token: 0x04000146 RID: 326
		private HttpContent _content;

		// Token: 0x04000147 RID: 327
		private HttpContentHeaders _headers;
	}
}
