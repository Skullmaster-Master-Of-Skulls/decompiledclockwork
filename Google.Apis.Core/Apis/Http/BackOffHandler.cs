using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Logging;
using Google.Apis.Util;

namespace Google.Apis.Http
{
	// Token: 0x02000024 RID: 36
	public class BackOffHandler : IHttpUnsuccessfulResponseHandler, IHttpExceptionHandler
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000038FC File Offset: 0x00001AFC
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003904 File Offset: 0x00001B04
		public IBackOff BackOff { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000390D File Offset: 0x00001B0D
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003915 File Offset: 0x00001B15
		public TimeSpan MaxTimeSpan { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000391E File Offset: 0x00001B1E
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003926 File Offset: 0x00001B26
		public Func<HttpResponseMessage, bool> HandleUnsuccessfulResponseFunc { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000392F File Offset: 0x00001B2F
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003937 File Offset: 0x00001B37
		public Func<Exception, bool> HandleExceptionFunc { get; private set; }

		// Token: 0x060000BF RID: 191 RVA: 0x00003940 File Offset: 0x00001B40
		public BackOffHandler(IBackOff backOff) : this(new BackOffHandler.Initializer(backOff))
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000394E File Offset: 0x00001B4E
		public BackOffHandler(BackOffHandler.Initializer initializer)
		{
			this.BackOff = initializer.BackOff;
			this.MaxTimeSpan = initializer.MaxTimeSpan;
			this.HandleExceptionFunc = initializer.HandleExceptionFunc;
			this.HandleUnsuccessfulResponseFunc = initializer.HandleUnsuccessfulResponseFunc;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003988 File Offset: 0x00001B88
		public virtual async Task<bool> HandleResponseAsync(HandleUnsuccessfulResponseArgs args)
		{
			bool result;
			if (this.HandleUnsuccessfulResponseFunc != null && this.HandleUnsuccessfulResponseFunc(args.Response))
			{
				result = await this.HandleAsync(args.SupportsRetry, args.CurrentFailedTry, args.CancellationToken).ConfigureAwait(false);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000039D8 File Offset: 0x00001BD8
		public virtual async Task<bool> HandleExceptionAsync(HandleExceptionArgs args)
		{
			bool result;
			if (this.HandleExceptionFunc != null && this.HandleExceptionFunc(args.Exception))
			{
				result = await this.HandleAsync(args.SupportsRetry, args.CurrentFailedTry, args.CancellationToken).ConfigureAwait(false);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003A28 File Offset: 0x00001C28
		private async Task<bool> HandleAsync(bool supportsRetry, int currentFailedTry, CancellationToken cancellationToken)
		{
			bool result;
			if (!supportsRetry || this.BackOff.MaxNumOfRetries < currentFailedTry)
			{
				result = false;
			}
			else
			{
				TimeSpan ts = this.BackOff.GetNextBackOff(currentFailedTry);
				if (ts > this.MaxTimeSpan || ts < TimeSpan.Zero)
				{
					result = false;
				}
				else
				{
					await this.Wait(ts, cancellationToken).ConfigureAwait(false);
					BackOffHandler.Logger.Debug("Back-Off handled the error. Waited {0}ms before next retry...", new object[]
					{
						ts.TotalMilliseconds
					});
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003A88 File Offset: 0x00001C88
		protected virtual async Task Wait(TimeSpan ts, CancellationToken cancellationToken)
		{
			await Task.Delay(ts, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0400003D RID: 61
		private static readonly ILogger Logger = ApplicationContext.Logger.ForType<BackOffHandler>();

		// Token: 0x02000043 RID: 67
		public class Initializer
		{
			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600014F RID: 335 RVA: 0x00004794 File Offset: 0x00002994
			// (set) Token: 0x06000150 RID: 336 RVA: 0x0000479C File Offset: 0x0000299C
			public IBackOff BackOff { get; private set; }

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000151 RID: 337 RVA: 0x000047A5 File Offset: 0x000029A5
			// (set) Token: 0x06000152 RID: 338 RVA: 0x000047AD File Offset: 0x000029AD
			public TimeSpan MaxTimeSpan { get; set; }

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000153 RID: 339 RVA: 0x000047B6 File Offset: 0x000029B6
			// (set) Token: 0x06000154 RID: 340 RVA: 0x000047BE File Offset: 0x000029BE
			public Func<HttpResponseMessage, bool> HandleUnsuccessfulResponseFunc { get; set; }

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000155 RID: 341 RVA: 0x000047C7 File Offset: 0x000029C7
			// (set) Token: 0x06000156 RID: 342 RVA: 0x000047CF File Offset: 0x000029CF
			public Func<Exception, bool> HandleExceptionFunc { get; set; }

			// Token: 0x06000157 RID: 343 RVA: 0x000047D8 File Offset: 0x000029D8
			public Initializer(IBackOff backOff)
			{
				this.BackOff = backOff;
				this.HandleExceptionFunc = BackOffHandler.Initializer.DefaultHandleExceptionFunc;
				this.HandleUnsuccessfulResponseFunc = BackOffHandler.Initializer.DefaultHandleUnsuccessfulResponseFunc;
				this.MaxTimeSpan = TimeSpan.FromSeconds(16.0);
			}

			// Token: 0x0400008E RID: 142
			public static readonly Func<HttpResponseMessage, bool> DefaultHandleUnsuccessfulResponseFunc = (HttpResponseMessage r) => r.StatusCode == HttpStatusCode.ServiceUnavailable;

			// Token: 0x0400008F RID: 143
			public static readonly Func<Exception, bool> DefaultHandleExceptionFunc = (Exception ex) => !(ex is TaskCanceledException) && !(ex is OperationCanceledException);
		}
	}
}
