using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http.Formatting.Internal;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000014 RID: 20
	public abstract class MultipartFormDataRemoteStreamProvider : MultipartStreamProvider
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000041E8 File Offset: 0x000023E8
		protected MultipartFormDataRemoteStreamProvider()
		{
			this.FormData = HttpValueCollection.Create();
			this.FileData = new Collection<MultipartRemoteFileData>();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004211 File Offset: 0x00002411
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00004219 File Offset: 0x00002419
		public Collection<MultipartRemoteFileData> FileData { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004222 File Offset: 0x00002422
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000422A File Offset: 0x0000242A
		public NameValueCollection FormData { get; private set; }

		// Token: 0x0600009F RID: 159
		public abstract RemoteStreamInfo GetRemoteStream(HttpContent parent, HttpContentHeaders headers);

		// Token: 0x060000A0 RID: 160 RVA: 0x00004234 File Offset: 0x00002434
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (!MultipartFormDataStreamProviderHelper.IsFileContent(parent, headers))
			{
				return new MemoryStream();
			}
			RemoteStreamInfo remoteStream = this.GetRemoteStream(parent, headers);
			if (remoteStream == null)
			{
				throw Error.InvalidOperation(Resources.RemoteStreamInfoCannotBeNull, new object[]
				{
					"GetRemoteStream",
					base.GetType().Name
				});
			}
			this.FileData.Add(new MultipartRemoteFileData(headers, remoteStream.Location, remoteStream.FileName));
			return remoteStream.RemoteStream;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000042A8 File Offset: 0x000024A8
		public override Task ExecutePostProcessingAsync()
		{
			return MultipartFormDataStreamProviderHelper.ReadFormDataAsync(base.Contents, this.FormData, this._cancellationToken);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000042C1 File Offset: 0x000024C1
		public override Task ExecutePostProcessingAsync(CancellationToken cancellationToken)
		{
			this._cancellationToken = cancellationToken;
			return this.ExecutePostProcessingAsync();
		}

		// Token: 0x0400002E RID: 46
		private CancellationToken _cancellationToken = CancellationToken.None;
	}
}
