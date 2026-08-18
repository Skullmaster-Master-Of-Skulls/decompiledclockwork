using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200006F RID: 111
	public class MultipartFileStreamProvider : MultipartStreamProvider
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x0000F90C File Offset: 0x0000DB0C
		public MultipartFileStreamProvider(string rootPath) : this(rootPath, 4096)
		{
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000F91C File Offset: 0x0000DB1C
		public MultipartFileStreamProvider(string rootPath, int bufferSize)
		{
			if (rootPath == null)
			{
				throw Error.ArgumentNull("rootPath");
			}
			if (bufferSize < 1)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("bufferSize", bufferSize, 1);
			}
			this._rootPath = Path.GetFullPath(rootPath);
			this._bufferSize = bufferSize;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000F981 File Offset: 0x0000DB81
		public Collection<MultipartFileData> FileData
		{
			get
			{
				return this._fileData;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000F989 File Offset: 0x0000DB89
		protected string RootPath
		{
			get
			{
				return this._rootPath;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000F991 File Offset: 0x0000DB91
		protected int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000F99C File Offset: 0x0000DB9C
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (parent == null)
			{
				throw Error.ArgumentNull("parent");
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			string text;
			try
			{
				string localFileName = this.GetLocalFileName(headers);
				text = Path.Combine(this._rootPath, Path.GetFileName(localFileName));
			}
			catch (Exception innerException)
			{
				throw Error.InvalidOperation(innerException, Resources.MultipartStreamProviderInvalidLocalFileName, new object[0]);
			}
			MultipartFileData item = new MultipartFileData(headers, text);
			this._fileData.Add(item);
			return File.Create(text, this._bufferSize, FileOptions.Asynchronous);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		public virtual string GetLocalFileName(HttpContentHeaders headers)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			return string.Format(CultureInfo.InvariantCulture, "BodyPart_{0}", new object[]
			{
				Guid.NewGuid()
			});
		}

		// Token: 0x04000183 RID: 387
		private const int MinBufferSize = 1;

		// Token: 0x04000184 RID: 388
		private const int DefaultBufferSize = 4096;

		// Token: 0x04000185 RID: 389
		private string _rootPath;

		// Token: 0x04000186 RID: 390
		private int _bufferSize = 4096;

		// Token: 0x04000187 RID: 391
		private Collection<MultipartFileData> _fileData = new Collection<MultipartFileData>();
	}
}
