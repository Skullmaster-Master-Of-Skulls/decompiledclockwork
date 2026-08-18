using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200001F RID: 31
	internal class IOThreadCancellationTokenSource : IDisposable
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00005092 File Offset: 0x00003292
		public IOThreadCancellationTokenSource(TimeSpan timeout)
		{
			TimeoutHelper.ThrowIfNegativeArgument(timeout);
			this.timeout = timeout;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000050A7 File Offset: 0x000032A7
		public IOThreadCancellationTokenSource(int timeout) : this(TimeSpan.FromMilliseconds((double)timeout))
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000050B8 File Offset: 0x000032B8
		public CancellationToken Token
		{
			get
			{
				if (this.token == null)
				{
					if (this.timeout >= TimeoutHelper.MaxWait)
					{
						this.token = new CancellationToken?(CancellationToken.None);
					}
					else
					{
						this.timer = new IOThreadTimer(IOThreadCancellationTokenSource.onCancel, this, true);
						this.source = new CancellationTokenSource();
						this.timer.Set(this.timeout);
						this.token = new CancellationToken?(this.source.Token);
					}
				}
				return this.token.Value;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005145 File Offset: 0x00003345
		public void Dispose()
		{
			if (this.source != null && this.timer.Cancel())
			{
				this.source.Dispose();
				this.source = null;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005170 File Offset: 0x00003370
		private static void OnCancel(object obj)
		{
			IOThreadCancellationTokenSource iothreadCancellationTokenSource = (IOThreadCancellationTokenSource)obj;
			iothreadCancellationTokenSource.Cancel();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000518A File Offset: 0x0000338A
		private void Cancel()
		{
			this.source.Cancel();
			this.source.Dispose();
			this.source = null;
		}

		// Token: 0x04000076 RID: 118
		private static readonly Action<object> onCancel = Fx.ThunkCallback<object>(new Action<object>(IOThreadCancellationTokenSource.OnCancel));

		// Token: 0x04000077 RID: 119
		private readonly TimeSpan timeout;

		// Token: 0x04000078 RID: 120
		private CancellationTokenSource source;

		// Token: 0x04000079 RID: 121
		private CancellationToken? token;

		// Token: 0x0400007A RID: 122
		private IOThreadTimer timer;
	}
}
