using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Logging;
using Google.Apis.Media;
using Google.Apis.Services;
using Google.Apis.Util;

namespace Google.Apis.Download
{
	// Token: 0x0200000E RID: 14
	public class MediaDownloader : IMediaDownloader
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002C45 File Offset: 0x00000E45
		static MediaDownloader()
		{
			UriPatcher.PatchUriQuirks();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002C5B File Offset: 0x00000E5B
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002C63 File Offset: 0x00000E63
		public int ChunkSize
		{
			get
			{
				return this.chunkSize;
			}
			set
			{
				if (value > 10485760)
				{
					throw new ArgumentOutOfRangeException("ChunkSize");
				}
				this.chunkSize = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002C7F File Offset: 0x00000E7F
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002C87 File Offset: 0x00000E87
		public RangeHeaderValue Range { get; set; }

		// Token: 0x06000065 RID: 101 RVA: 0x00002C90 File Offset: 0x00000E90
		private void UpdateProgress(IDownloadProgress progress)
		{
			Action<IDownloadProgress> progressChanged = this.ProgressChanged;
			if (progressChanged == null)
			{
				return;
			}
			progressChanged(progress);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002CA3 File Offset: 0x00000EA3
		public MediaDownloader(IClientService service)
		{
			this.service = service;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002CBD File Offset: 0x00000EBD
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002CC5 File Offset: 0x00000EC5
		public Action<HttpRequestMessage> ModifyRequest { get; set; }

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000069 RID: 105 RVA: 0x00002CD0 File Offset: 0x00000ED0
		// (remove) Token: 0x0600006A RID: 106 RVA: 0x00002D08 File Offset: 0x00000F08
		public event Action<IDownloadProgress> ProgressChanged;

		// Token: 0x0600006B RID: 107 RVA: 0x00002D3D File Offset: 0x00000F3D
		public IDownloadProgress Download(string url, Stream stream)
		{
			return this.DownloadCoreAsync(url, stream, CancellationToken.None).Result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002D54 File Offset: 0x00000F54
		public async Task<IDownloadProgress> DownloadAsync(string url, Stream stream)
		{
			return await this.DownloadAsync(url, stream, CancellationToken.None).ConfigureAwait(false);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002DAC File Offset: 0x00000FAC
		public async Task<IDownloadProgress> DownloadAsync(string url, Stream stream, CancellationToken cancellationToken)
		{
			return await this.DownloadCoreAsync(url, stream, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E0C File Offset: 0x0000100C
		private async Task<IDownloadProgress> DownloadCoreAsync(string url, Stream stream, CancellationToken cancellationToken)
		{
			url.ThrowIfNull("url");
			stream.ThrowIfNull("stream");
			if (!stream.CanWrite)
			{
				throw new ArgumentException("stream doesn't support write operations");
			}
			UriBuilder uriBuilder = new UriBuilder(url);
			if (uriBuilder.Query == null || uriBuilder.Query.Length <= 1)
			{
				uriBuilder.Query = "alt=media";
			}
			else
			{
				uriBuilder.Query = uriBuilder.Query.Substring(1) + "&alt=media";
			}
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
			httpRequestMessage.Headers.Range = this.Range;
			Action<HttpRequestMessage> modifyRequest = this.ModifyRequest;
			if (modifyRequest != null)
			{
				modifyRequest(httpRequestMessage);
			}
			long bytesReturned = 0L;
			IDownloadProgress result;
			try
			{
				HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead;
				HttpResponseMessage httpResponseMessage = await this.service.HttpClient.SendAsync(httpRequestMessage, completionOption, cancellationToken).ConfigureAwait(false);
				using (HttpResponseMessage response = httpResponseMessage)
				{
					if (!response.IsSuccessStatusCode)
					{
						throw await MediaApiErrorHandling.ExceptionForResponseAsync(this.service, response).ConfigureAwait(false);
					}
					this.OnResponseReceived(response);
					using (Stream responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
					{
						MediaDownloader.CountedBuffer buffer = new MediaDownloader.CountedBuffer(this.ChunkSize + 1);
						for (;;)
						{
							await buffer.Fill(responseStream, cancellationToken).ConfigureAwait(false);
							int bytesToReturn = Math.Min(this.ChunkSize, buffer.Count);
							this.OnDataReceived(buffer.Data, bytesToReturn);
							await stream.WriteAsync(buffer.Data, 0, bytesToReturn, cancellationToken).ConfigureAwait(false);
							bytesReturned += (long)bytesToReturn;
							buffer.RemoveFromFront(this.ChunkSize);
							if (buffer.IsEmpty)
							{
								break;
							}
							this.UpdateProgress(new MediaDownloader.DownloadProgress(DownloadStatus.Downloading, bytesReturned));
						}
						buffer = null;
					}
					Stream responseStream = null;
					this.OnDownloadCompleted();
					MediaDownloader.DownloadProgress downloadProgress = new MediaDownloader.DownloadProgress(DownloadStatus.Completed, bytesReturned);
					this.UpdateProgress(downloadProgress);
					result = downloadProgress;
				}
			}
			catch (TaskCanceledException exception)
			{
				MediaDownloader.Logger.Error(exception, "Download media was canceled", new object[0]);
				this.UpdateProgress(new MediaDownloader.DownloadProgress(exception, bytesReturned));
				throw;
			}
			catch (Exception exception2)
			{
				MediaDownloader.Logger.Error(exception2, "Exception occurred while downloading media", new object[0]);
				MediaDownloader.DownloadProgress downloadProgress2 = new MediaDownloader.DownloadProgress(exception2, bytesReturned);
				this.UpdateProgress(downloadProgress2);
				result = downloadProgress2;
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002425 File Offset: 0x00000625
		protected virtual void OnResponseReceived(HttpResponseMessage response)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002425 File Offset: 0x00000625
		protected virtual void OnDataReceived(byte[] data, int length)
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002425 File Offset: 0x00000625
		protected virtual void OnDownloadCompleted()
		{
		}

		// Token: 0x04000035 RID: 53
		private static readonly ILogger Logger = ApplicationContext.Logger.ForType<MediaDownloader>();

		// Token: 0x04000036 RID: 54
		private readonly IClientService service;

		// Token: 0x04000037 RID: 55
		private const int MB = 1048576;

		// Token: 0x04000038 RID: 56
		public const int MaximumChunkSize = 10485760;

		// Token: 0x04000039 RID: 57
		private int chunkSize = 10485760;

		// Token: 0x02000025 RID: 37
		private class DownloadProgress : IDownloadProgress
		{
			// Token: 0x06000104 RID: 260 RVA: 0x0000516A File Offset: 0x0000336A
			public DownloadProgress(DownloadStatus status, long bytes)
			{
				this.Status = status;
				this.BytesDownloaded = bytes;
			}

			// Token: 0x06000105 RID: 261 RVA: 0x00005180 File Offset: 0x00003380
			public DownloadProgress(Exception exception, long bytes)
			{
				this.Status = DownloadStatus.Failed;
				this.BytesDownloaded = bytes;
				this.Exception = exception;
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06000106 RID: 262 RVA: 0x0000519D File Offset: 0x0000339D
			// (set) Token: 0x06000107 RID: 263 RVA: 0x000051A5 File Offset: 0x000033A5
			public DownloadStatus Status { get; private set; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x06000108 RID: 264 RVA: 0x000051AE File Offset: 0x000033AE
			// (set) Token: 0x06000109 RID: 265 RVA: 0x000051B6 File Offset: 0x000033B6
			public long BytesDownloaded { get; private set; }

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x0600010A RID: 266 RVA: 0x000051BF File Offset: 0x000033BF
			// (set) Token: 0x0600010B RID: 267 RVA: 0x000051C7 File Offset: 0x000033C7
			public Exception Exception { get; private set; }
		}

		// Token: 0x02000026 RID: 38
		private class CountedBuffer
		{
			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600010C RID: 268 RVA: 0x000051D0 File Offset: 0x000033D0
			// (set) Token: 0x0600010D RID: 269 RVA: 0x000051D8 File Offset: 0x000033D8
			public byte[] Data { get; set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x0600010E RID: 270 RVA: 0x000051E1 File Offset: 0x000033E1
			// (set) Token: 0x0600010F RID: 271 RVA: 0x000051E9 File Offset: 0x000033E9
			public int Count { get; private set; }

			// Token: 0x06000110 RID: 272 RVA: 0x000051F2 File Offset: 0x000033F2
			public CountedBuffer(int size)
			{
				this.Data = new byte[size];
				this.Count = 0;
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000111 RID: 273 RVA: 0x0000520D File Offset: 0x0000340D
			public bool IsEmpty
			{
				get
				{
					return this.Count == 0;
				}
			}

			// Token: 0x06000112 RID: 274 RVA: 0x00005218 File Offset: 0x00003418
			public async Task Fill(Stream stream, CancellationToken cancellationToken)
			{
				while (this.Count < this.Data.Length)
				{
					int num = await stream.ReadAsync(this.Data, this.Count, this.Data.Length - this.Count, cancellationToken).ConfigureAwait(false);
					if (num == 0)
					{
						break;
					}
					this.Count += num;
				}
			}

			// Token: 0x06000113 RID: 275 RVA: 0x0000526D File Offset: 0x0000346D
			public void RemoveFromFront(int n)
			{
				if (n >= this.Count)
				{
					this.Count = 0;
					return;
				}
				Array.Copy(this.Data, n, this.Data, 0, this.Count - n);
				this.Count -= n;
			}
		}
	}
}
