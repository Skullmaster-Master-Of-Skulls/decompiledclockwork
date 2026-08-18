using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Http;
using Google.Apis.Logging;
using Google.Apis.Media;
using Google.Apis.Requests;
using Google.Apis.Testing;
using Google.Apis.Util;

namespace Google.Apis.Upload
{
	// Token: 0x02000006 RID: 6
	public abstract class ResumableUpload
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		protected ResumableUpload(Stream contentStream, ResumableUploadOptions options)
		{
			contentStream.ThrowIfNull("contentStream");
			this.ContentStream = contentStream;
			this.StreamLength = (this.ContentStream.CanSeek ? this.ContentStream.Length : -1L);
			ConfigurableHttpClient configurableHttpClient;
			if ((configurableHttpClient = ((options != null) ? options.ConfigurableHttpClient : null)) == null)
			{
				configurableHttpClient = new HttpClientFactory().CreateHttpClient(new CreateHttpClientArgs
				{
					ApplicationName = "ResumableUpload",
					GZipEnabled = true
				});
			}
			this.HttpClient = configurableHttpClient;
			this.Options = options;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EC File Offset: 0x000002EC
		public static ResumableUpload CreateFromUploadUri(Uri uploadUri, Stream contentStream, ResumableUploadOptions options = null)
		{
			uploadUri.ThrowIfNull("uploadUri");
			return new ResumableUpload.InitiatedResumableUpload(uploadUri, contentStream, options);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002102 File Offset: 0x00000302
		protected ResumableUploadOptions Options { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		internal ConfigurableHttpClient HttpClient { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		public Stream ContentStream { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211A File Offset: 0x0000031A
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002122 File Offset: 0x00000322
		internal long StreamLength { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212B File Offset: 0x0000032B
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002133 File Offset: 0x00000333
		private byte[] LastMediaRequest { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000213C File Offset: 0x0000033C
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002144 File Offset: 0x00000344
		private int LastMediaLength { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000214D File Offset: 0x0000034D
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002155 File Offset: 0x00000355
		private Uri UploadUri { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000215E File Offset: 0x0000035E
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002166 File Offset: 0x00000366
		private long BytesServerReceived { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000216F File Offset: 0x0000036F
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002177 File Offset: 0x00000377
		private long BytesClientSent { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002180 File Offset: 0x00000380
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002188 File Offset: 0x00000388
		public int ChunkSize
		{
			get
			{
				return this.chunkSize;
			}
			set
			{
				if (value < 262144)
				{
					throw new ArgumentOutOfRangeException("ChunkSize");
				}
				this.chunkSize = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000018 RID: 24 RVA: 0x000021A4 File Offset: 0x000003A4
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x000021DC File Offset: 0x000003DC
		public event Action<IUploadProgress> ProgressChanged;

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002211 File Offset: 0x00000411
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002219 File Offset: 0x00000419
		private ResumableUpload.ResumableUploadProgress Progress { get; set; }

		// Token: 0x0600001C RID: 28 RVA: 0x00002222 File Offset: 0x00000422
		private void UpdateProgress(ResumableUpload.ResumableUploadProgress progress)
		{
			this.Progress = progress;
			Action<IUploadProgress> progressChanged = this.ProgressChanged;
			if (progressChanged == null)
			{
				return;
			}
			progressChanged(progress);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000223C File Offset: 0x0000043C
		public IUploadProgress GetProgress()
		{
			return this.Progress;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001E RID: 30 RVA: 0x00002244 File Offset: 0x00000444
		// (remove) Token: 0x0600001F RID: 31 RVA: 0x0000227C File Offset: 0x0000047C
		public event Action<IUploadSessionData> UploadSessionData;

		// Token: 0x06000020 RID: 32 RVA: 0x000022B1 File Offset: 0x000004B1
		private void SendUploadSessionData(ResumableUpload.ResumeableUploadSessionData sessionData)
		{
			Action<IUploadSessionData> uploadSessionData = this.UploadSessionData;
			if (uploadSessionData == null)
			{
				return;
			}
			uploadSessionData(sessionData);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022C4 File Offset: 0x000004C4
		public IUploadProgress Upload()
		{
			return this.UploadAsync(CancellationToken.None).Result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022D6 File Offset: 0x000004D6
		public Task<IUploadProgress> UploadAsync()
		{
			return this.UploadAsync(CancellationToken.None);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022E4 File Offset: 0x000004E4
		public async Task<IUploadProgress> UploadAsync(CancellationToken cancellationToken)
		{
			this.BytesServerReceived = 0L;
			this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(UploadStatus.Starting, 0L));
			try
			{
				Uri uploadUri = await this.InitiateSessionAsync(cancellationToken).ConfigureAwait(false);
				this.UploadUri = uploadUri;
				if (this.ContentStream.CanSeek)
				{
					this.SendUploadSessionData(new ResumableUpload.ResumeableUploadSessionData(this.UploadUri));
				}
				ResumableUpload.Logger.Debug("MediaUpload[{0}] - Start uploading...", new object[]
				{
					this.UploadUri
				});
			}
			catch (Exception exception)
			{
				ResumableUpload.Logger.Error(exception, "MediaUpload - Exception occurred while initializing the upload", new object[0]);
				this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(exception, this.BytesServerReceived));
				return this.Progress;
			}
			return await this.UploadCoreAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002331 File Offset: 0x00000531
		public IUploadProgress Resume()
		{
			return this.ResumeAsync(null, CancellationToken.None).Result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002344 File Offset: 0x00000544
		public IUploadProgress Resume(Uri uploadUri)
		{
			return this.ResumeAsync(uploadUri, CancellationToken.None).Result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002357 File Offset: 0x00000557
		public Task<IUploadProgress> ResumeAsync()
		{
			return this.ResumeAsync(null, CancellationToken.None);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002365 File Offset: 0x00000565
		public Task<IUploadProgress> ResumeAsync(CancellationToken cancellationToken)
		{
			return this.ResumeAsync(null, cancellationToken);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000236F File Offset: 0x0000056F
		public Task<IUploadProgress> ResumeAsync(Uri uploadUri)
		{
			return this.ResumeAsync(uploadUri, CancellationToken.None);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002380 File Offset: 0x00000580
		public async Task<IUploadProgress> ResumeAsync(Uri uploadUri, CancellationToken cancellationToken)
		{
			if (uploadUri != null)
			{
				if (!this.ContentStream.CanSeek)
				{
					throw new NotImplementedException("Resume after program restart not allowed when ContentStream.CanSeek is false");
				}
				ResumableUpload.Logger.Info("Resuming after program restart: UploadUri={0}", new object[]
				{
					uploadUri
				});
				this.UploadUri = uploadUri;
			}
			IUploadProgress result;
			if (this.UploadUri == null)
			{
				ResumableUpload.Logger.Info("There isn't any upload in progress, so starting to upload again", new object[0]);
				result = await this.UploadAsync(cancellationToken).ConfigureAwait(false);
			}
			else
			{
				string value = string.Format("bytes */{0}", (this.StreamLength < 0L) ? "*" : this.StreamLength.ToString());
				HttpRequestMessage httpRequestMessage = new RequestBuilder
				{
					BaseUri = this.UploadUri,
					Method = "PUT"
				}.CreateRequest();
				httpRequestMessage.SetEmptyContent().Headers.Add("Content-Range", value);
				try
				{
					HttpResponseMessage response;
					using (new ResumableUpload.ServerErrorCallback(this))
					{
						response = await this.HttpClient.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
					}
					ResumableUpload.ServerErrorCallback callback = null;
					if (await this.HandleResponse(response).ConfigureAwait(false))
					{
						this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(UploadStatus.Completed, this.BytesServerReceived));
						return this.Progress;
					}
				}
				catch (TaskCanceledException ex)
				{
					ResumableUpload.Logger.Error(ex, "MediaUpload[{0}] - Task was canceled", new object[]
					{
						this.UploadUri
					});
					this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(ex, this.BytesServerReceived));
					throw ex;
				}
				catch (Exception exception)
				{
					ResumableUpload.Logger.Error(exception, "MediaUpload[{0}] - Exception occurred while resuming uploading media", new object[]
					{
						this.UploadUri
					});
					this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(exception, this.BytesServerReceived));
					return this.Progress;
				}
				result = await this.UploadCoreAsync(cancellationToken).ConfigureAwait(false);
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023D8 File Offset: 0x000005D8
		private async Task<IUploadProgress> UploadCoreAsync(CancellationToken cancellationToken)
		{
			try
			{
				using (new ResumableUpload.ServerErrorCallback(this))
				{
					while (!(await this.SendNextChunkAsync(this.ContentStream, cancellationToken).ConfigureAwait(false)))
					{
						this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(UploadStatus.Uploading, this.BytesServerReceived));
					}
					this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(UploadStatus.Completed, this.BytesServerReceived));
				}
				ResumableUpload.ServerErrorCallback callback = null;
			}
			catch (TaskCanceledException ex)
			{
				ResumableUpload.Logger.Error(ex, "MediaUpload[{0}] - Task was canceled", new object[]
				{
					this.UploadUri
				});
				this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(ex, this.BytesServerReceived));
				throw ex;
			}
			catch (Exception exception)
			{
				ResumableUpload.Logger.Error(exception, "MediaUpload[{0}] - Exception occurred while uploading media", new object[]
				{
					this.UploadUri
				});
				this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(exception, this.BytesServerReceived));
			}
			return this.Progress;
		}

		// Token: 0x0600002B RID: 43
		public abstract Task<Uri> InitiateSessionAsync(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600002C RID: 44 RVA: 0x00002425 File Offset: 0x00000625
		protected virtual void ProcessResponse(HttpResponseMessage httpResponse)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002428 File Offset: 0x00000628
		protected async Task<bool> SendNextChunkAsync(Stream stream, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			HttpRequestMessage request = new RequestBuilder
			{
				BaseUri = this.UploadUri,
				Method = "PUT"
			}.CreateRequest();
			int num = this.ContentStream.CanSeek ? this.PrepareNextChunkKnownSize(request, stream, cancellationToken) : this.PrepareNextChunkUnknownSize(request, stream, cancellationToken);
			this.BytesClientSent = this.BytesServerReceived + (long)num;
			ResumableUpload.Logger.Debug("MediaUpload[{0}] - Sending bytes={1}-{2}", new object[]
			{
				this.UploadUri,
				this.BytesServerReceived,
				this.BytesClientSent - 1L
			});
			HttpResponseMessage response = await this.HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
			return await this.HandleResponse(response).ConfigureAwait(false);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002480 File Offset: 0x00000680
		private async Task<bool> HandleResponse(HttpResponseMessage response)
		{
			bool result;
			if (response.IsSuccessStatusCode)
			{
				this.MediaCompleted(response);
				result = true;
			}
			else
			{
				if (response.StatusCode != (HttpStatusCode)308)
				{
					throw await this.ExceptionForResponseAsync(response).ConfigureAwait(false);
				}
				IEnumerable<string> value = response.Headers.FirstOrDefault((KeyValuePair<string, IEnumerable<string>> x) => x.Key == "Range").Value;
				string range = (value != null) ? value.First<string>() : null;
				this.BytesServerReceived = this.GetNextByte(range);
				ResumableUpload.Logger.Debug("MediaUpload[{0}] - {1} Bytes were sent successfully", new object[]
				{
					this.UploadUri,
					this.BytesServerReceived
				});
				result = false;
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024CD File Offset: 0x000006CD
		protected Task<GoogleApiException> ExceptionForResponseAsync(HttpResponseMessage response)
		{
			ResumableUploadOptions options = this.Options;
			ISerializer serializer = (options != null) ? options.Serializer : null;
			ResumableUploadOptions options2 = this.Options;
			return MediaApiErrorHandling.ExceptionForResponseAsync(serializer, (options2 != null) ? options2.ServiceName : null, response);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024F9 File Offset: 0x000006F9
		private void MediaCompleted(HttpResponseMessage response)
		{
			ResumableUpload.Logger.Debug("MediaUpload[{0}] - media was uploaded successfully", new object[]
			{
				this.UploadUri
			});
			this.ProcessResponse(response);
			this.BytesServerReceived = this.StreamLength;
			this.LastMediaRequest = null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002534 File Offset: 0x00000734
		private int PrepareNextChunkUnknownSize(HttpRequestMessage request, Stream stream, CancellationToken cancellationToken)
		{
			if (this.LastMediaRequest == null)
			{
				this.LastMediaRequest = new byte[this.ChunkSize + 1];
				this.LastMediaLength = 0;
			}
			int num = (int)(this.BytesClientSent - this.BytesServerReceived) + Math.Max(0, this.LastMediaLength - this.ChunkSize);
			if (this.LastMediaLength != num)
			{
				Buffer.BlockCopy(this.LastMediaRequest, this.LastMediaLength - num, this.LastMediaRequest, 0, num);
				this.LastMediaLength = num;
			}
			while (this.LastMediaLength < this.ChunkSize + 1 && this.StreamLength == -1L)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int count = Math.Min(this.BufferSize, this.ChunkSize + 1 - this.LastMediaLength);
				int num2 = stream.Read(this.LastMediaRequest, this.LastMediaLength, count);
				this.LastMediaLength += num2;
				if (num2 == 0)
				{
					this.StreamLength = this.BytesServerReceived + (long)this.LastMediaLength;
				}
			}
			int num3 = Math.Min(this.ChunkSize, this.LastMediaLength);
			request.Content = new ByteArrayContent(this.LastMediaRequest, 0, num3)
			{
				Headers = 
				{
					{
						"Content-Range",
						this.GetContentRangeHeader(this.BytesServerReceived, (long)num3)
					}
				}
			};
			return num3;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002674 File Offset: 0x00000874
		private int PrepareNextChunkKnownSize(HttpRequestMessage request, Stream stream, CancellationToken cancellationToken)
		{
			int num = (int)Math.Min(this.StreamLength - this.BytesServerReceived, (long)this.ChunkSize);
			byte[] array = new byte[Math.Min(num, this.BufferSize)];
			if (stream.Position != this.BytesServerReceived)
			{
				stream.Position = this.BytesServerReceived;
			}
			MemoryStream memoryStream = new MemoryStream(num);
			int num2 = 0;
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int num3 = stream.Read(array, 0, Math.Min(array.Length, num - num2));
				if (num3 == 0)
				{
					break;
				}
				memoryStream.Write(array, 0, num3);
				num2 += num3;
			}
			memoryStream.Position = 0L;
			request.Content = new StreamContent(memoryStream);
			request.Content.Headers.Add("Content-Range", this.GetContentRangeHeader(this.BytesServerReceived, (long)num));
			return num;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000273A File Offset: 0x0000093A
		private long GetNextByte(string range)
		{
			if (range != null)
			{
				return long.Parse(range.Substring(range.IndexOf('-') + 1)) + 1L;
			}
			return 0L;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000275C File Offset: 0x0000095C
		private string GetContentRangeHeader(long chunkStart, long chunkSize)
		{
			string arg = (this.StreamLength < 0L) ? "*" : this.StreamLength.ToString();
			if (chunkStart == 0L && chunkSize == 0L && this.StreamLength == 0L)
			{
				return "bytes */0";
			}
			long num = chunkStart + chunkSize - 1L;
			return string.Format("bytes {0}-{1}/{2}", chunkStart, num, arg);
		}

		// Token: 0x0400000C RID: 12
		private static readonly ILogger Logger = ApplicationContext.Logger.ForType<ResumableUpload>();

		// Token: 0x0400000D RID: 13
		private const int KB = 1024;

		// Token: 0x0400000E RID: 14
		private const int MB = 1048576;

		// Token: 0x0400000F RID: 15
		public const int MinimumChunkSize = 262144;

		// Token: 0x04000010 RID: 16
		public const int DefaultChunkSize = 10485760;

		// Token: 0x04000011 RID: 17
		internal int BufferSize = 4096;

		// Token: 0x04000012 RID: 18
		private const int UnknownSize = -1;

		// Token: 0x04000013 RID: 19
		private const string ZeroByteContentRangeHeader = "bytes */0";

		// Token: 0x0400001D RID: 29
		[VisibleForTestOnly]
		protected int chunkSize = 10485760;

		// Token: 0x02000019 RID: 25
		private sealed class InitiatedResumableUpload : ResumableUpload
		{
			// Token: 0x060000DF RID: 223 RVA: 0x00003E46 File Offset: 0x00002046
			public InitiatedResumableUpload(Uri uploadUri, Stream contentStream, ResumableUploadOptions options) : base(contentStream, options)
			{
				this._initiatedUploadUri = uploadUri;
			}

			// Token: 0x060000E0 RID: 224 RVA: 0x00003E57 File Offset: 0x00002057
			public override Task<Uri> InitiateSessionAsync(CancellationToken cancellationToken = default(CancellationToken))
			{
				return Task.FromResult<Uri>(this._initiatedUploadUri);
			}

			// Token: 0x04000054 RID: 84
			private Uri _initiatedUploadUri;
		}

		// Token: 0x0200001A RID: 26
		private class ServerErrorCallback : IHttpUnsuccessfulResponseHandler, IHttpExceptionHandler, IDisposable
		{
			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003E64 File Offset: 0x00002064
			// (set) Token: 0x060000E2 RID: 226 RVA: 0x00003E6C File Offset: 0x0000206C
			private ResumableUpload Owner { get; set; }

			// Token: 0x060000E3 RID: 227 RVA: 0x00003E75 File Offset: 0x00002075
			public ServerErrorCallback(ResumableUpload resumable)
			{
				this.Owner = resumable;
				this.Owner.HttpClient.MessageHandler.AddUnsuccessfulResponseHandler(this);
				this.Owner.HttpClient.MessageHandler.AddExceptionHandler(this);
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x00003EB0 File Offset: 0x000020B0
			public Task<bool> HandleResponseAsync(HandleUnsuccessfulResponseArgs args)
			{
				bool result = false;
				int statusCode = (int)args.Response.StatusCode;
				if (args.SupportsRetry && args.Request.RequestUri.Equals(this.Owner.UploadUri) && statusCode / 100 == 5)
				{
					result = this.OnServerError(args.Request);
				}
				TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
				taskCompletionSource.SetResult(result);
				return taskCompletionSource.Task;
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x00003F18 File Offset: 0x00002118
			public Task<bool> HandleExceptionAsync(HandleExceptionArgs args)
			{
				bool result = args.SupportsRetry && !args.CancellationToken.IsCancellationRequested && args.Request.RequestUri.Equals(this.Owner.UploadUri) && this.OnServerError(args.Request);
				TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
				taskCompletionSource.SetResult(result);
				return taskCompletionSource.Task;
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x00003F7C File Offset: 0x0000217C
			private bool OnServerError(HttpRequestMessage request)
			{
				string value = string.Format("bytes */{0}", (this.Owner.StreamLength < 0L) ? "*" : this.Owner.StreamLength.ToString());
				request.Headers.Clear();
				request.Method = HttpMethod.Put;
				request.SetEmptyContent().Headers.Add("Content-Range", value);
				return true;
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x00003FEA File Offset: 0x000021EA
			public void Dispose()
			{
				this.Owner.HttpClient.MessageHandler.RemoveUnsuccessfulResponseHandler(this);
				this.Owner.HttpClient.MessageHandler.RemoveExceptionHandler(this);
			}
		}

		// Token: 0x0200001B RID: 27
		private class ResumableUploadProgress : IUploadProgress
		{
			// Token: 0x060000E8 RID: 232 RVA: 0x00004018 File Offset: 0x00002218
			public ResumableUploadProgress(UploadStatus status, long bytesSent)
			{
				this.Status = status;
				this.BytesSent = bytesSent;
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x0000402E File Offset: 0x0000222E
			public ResumableUploadProgress(Exception exception, long bytesSent)
			{
				this.Status = UploadStatus.Failed;
				this.BytesSent = bytesSent;
				this.Exception = exception;
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000EA RID: 234 RVA: 0x0000404B File Offset: 0x0000224B
			// (set) Token: 0x060000EB RID: 235 RVA: 0x00004053 File Offset: 0x00002253
			public UploadStatus Status { get; private set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000EC RID: 236 RVA: 0x0000405C File Offset: 0x0000225C
			// (set) Token: 0x060000ED RID: 237 RVA: 0x00004064 File Offset: 0x00002264
			public long BytesSent { get; private set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000EE RID: 238 RVA: 0x0000406D File Offset: 0x0000226D
			// (set) Token: 0x060000EF RID: 239 RVA: 0x00004075 File Offset: 0x00002275
			public Exception Exception { get; private set; }
		}

		// Token: 0x0200001C RID: 28
		private class ResumeableUploadSessionData : IUploadSessionData
		{
			// Token: 0x060000F0 RID: 240 RVA: 0x0000407E File Offset: 0x0000227E
			public ResumeableUploadSessionData(Uri uploadUri)
			{
				this.UploadUri = uploadUri;
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000408D File Offset: 0x0000228D
			// (set) Token: 0x060000F2 RID: 242 RVA: 0x00004095 File Offset: 0x00002295
			public Uri UploadUri { get; private set; }
		}
	}
}
