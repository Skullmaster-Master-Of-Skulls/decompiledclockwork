using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http.Formatting.Internal;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000070 RID: 112
	public class MultipartFormDataStreamProvider : MultipartFileStreamProvider
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0000FA6B File Offset: 0x0000DC6B
		public MultipartFormDataStreamProvider(string rootPath) : base(rootPath)
		{
			this.FormData = HttpValueCollection.Create();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public MultipartFormDataStreamProvider(string rootPath, int bufferSize) : base(rootPath, bufferSize)
		{
			this.FormData = HttpValueCollection.Create();
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000FA94 File Offset: 0x0000DC94
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		public NameValueCollection FormData { get; private set; }

		// Token: 0x060003BC RID: 956 RVA: 0x0000FAA5 File Offset: 0x0000DCA5
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (MultipartFormDataStreamProviderHelper.IsFileContent(parent, headers))
			{
				return base.GetStream(parent, headers);
			}
			return new MemoryStream();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000FABE File Offset: 0x0000DCBE
		public override Task ExecutePostProcessingAsync()
		{
			return MultipartFormDataStreamProviderHelper.ReadFormDataAsync(base.Contents, this.FormData, this._cancellationToken);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000FAD7 File Offset: 0x0000DCD7
		public override Task ExecutePostProcessingAsync(CancellationToken cancellationToken)
		{
			this._cancellationToken = cancellationToken;
			return this.ExecutePostProcessingAsync();
		}

		// Token: 0x04000188 RID: 392
		private CancellationToken _cancellationToken;
	}
}
