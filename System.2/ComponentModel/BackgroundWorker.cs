using System;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	// Token: 0x02000517 RID: 1303
	[SRDescription("BackgroundWorker_Desc")]
	[DefaultEvent("DoWork")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class BackgroundWorker : Component
	{
		// Token: 0x0600315A RID: 12634 RVA: 0x000DF6B5 File Offset: 0x000DD8B5
		[__DynamicallyInvokable]
		public BackgroundWorker()
		{
			this.threadStart = new BackgroundWorker.WorkerThreadStartDelegate(this.WorkerThreadStart);
			this.operationCompleted = new SendOrPostCallback(this.AsyncOperationCompleted);
			this.progressReporter = new SendOrPostCallback(this.ProgressReporter);
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000DF6F3 File Offset: 0x000DD8F3
		private void AsyncOperationCompleted(object arg)
		{
			this.isRunning = false;
			this.cancellationPending = false;
			this.OnRunWorkerCompleted((RunWorkerCompletedEventArgs)arg);
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x000DF70F File Offset: 0x000DD90F
		[Browsable(false)]
		[SRDescription("BackgroundWorker_CancellationPending")]
		[__DynamicallyInvokable]
		public bool CancellationPending
		{
			[__DynamicallyInvokable]
			get
			{
				return this.cancellationPending;
			}
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000DF717 File Offset: 0x000DD917
		[__DynamicallyInvokable]
		public void CancelAsync()
		{
			if (!this.WorkerSupportsCancellation)
			{
				throw new InvalidOperationException(SR.GetString("BackgroundWorker_WorkerDoesntSupportCancellation"));
			}
			this.cancellationPending = true;
		}

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x0600315E RID: 12638 RVA: 0x000DF738 File Offset: 0x000DD938
		// (remove) Token: 0x0600315F RID: 12639 RVA: 0x000DF74B File Offset: 0x000DD94B
		[SRCategory("PropertyCategoryAsynchronous")]
		[SRDescription("BackgroundWorker_DoWork")]
		[__DynamicallyInvokable]
		public event DoWorkEventHandler DoWork
		{
			[__DynamicallyInvokable]
			add
			{
				base.Events.AddHandler(BackgroundWorker.doWorkKey, value);
			}
			[__DynamicallyInvokable]
			remove
			{
				base.Events.RemoveHandler(BackgroundWorker.doWorkKey, value);
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000DF75E File Offset: 0x000DD95E
		[Browsable(false)]
		[SRDescription("BackgroundWorker_IsBusy")]
		[__DynamicallyInvokable]
		public bool IsBusy
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isRunning;
			}
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000DF768 File Offset: 0x000DD968
		[__DynamicallyInvokable]
		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			DoWorkEventHandler doWorkEventHandler = (DoWorkEventHandler)base.Events[BackgroundWorker.doWorkKey];
			if (doWorkEventHandler != null)
			{
				doWorkEventHandler(this, e);
			}
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000DF798 File Offset: 0x000DD998
		[__DynamicallyInvokable]
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompletedEventHandler = (RunWorkerCompletedEventHandler)base.Events[BackgroundWorker.runWorkerCompletedKey];
			if (runWorkerCompletedEventHandler != null)
			{
				runWorkerCompletedEventHandler(this, e);
			}
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000DF7C8 File Offset: 0x000DD9C8
		[__DynamicallyInvokable]
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChangedEventHandler = (ProgressChangedEventHandler)base.Events[BackgroundWorker.progressChangedKey];
			if (progressChangedEventHandler != null)
			{
				progressChangedEventHandler(this, e);
			}
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06003164 RID: 12644 RVA: 0x000DF7F6 File Offset: 0x000DD9F6
		// (remove) Token: 0x06003165 RID: 12645 RVA: 0x000DF809 File Offset: 0x000DDA09
		[SRCategory("PropertyCategoryAsynchronous")]
		[SRDescription("BackgroundWorker_ProgressChanged")]
		[__DynamicallyInvokable]
		public event ProgressChangedEventHandler ProgressChanged
		{
			[__DynamicallyInvokable]
			add
			{
				base.Events.AddHandler(BackgroundWorker.progressChangedKey, value);
			}
			[__DynamicallyInvokable]
			remove
			{
				base.Events.RemoveHandler(BackgroundWorker.progressChangedKey, value);
			}
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000DF81C File Offset: 0x000DDA1C
		private void ProgressReporter(object arg)
		{
			this.OnProgressChanged((ProgressChangedEventArgs)arg);
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000DF82A File Offset: 0x000DDA2A
		[__DynamicallyInvokable]
		public void ReportProgress(int percentProgress)
		{
			this.ReportProgress(percentProgress, null);
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000DF834 File Offset: 0x000DDA34
		[__DynamicallyInvokable]
		public void ReportProgress(int percentProgress, object userState)
		{
			if (!this.WorkerReportsProgress)
			{
				throw new InvalidOperationException(SR.GetString("BackgroundWorker_WorkerDoesntReportProgress"));
			}
			ProgressChangedEventArgs progressChangedEventArgs = new ProgressChangedEventArgs(percentProgress, userState);
			if (this.asyncOperation != null)
			{
				this.asyncOperation.Post(this.progressReporter, progressChangedEventArgs);
				return;
			}
			this.progressReporter(progressChangedEventArgs);
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000DF888 File Offset: 0x000DDA88
		[__DynamicallyInvokable]
		public void RunWorkerAsync()
		{
			this.RunWorkerAsync(null);
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x000DF894 File Offset: 0x000DDA94
		[__DynamicallyInvokable]
		public void RunWorkerAsync(object argument)
		{
			if (this.isRunning)
			{
				throw new InvalidOperationException(SR.GetString("BackgroundWorker_WorkerAlreadyRunning"));
			}
			this.isRunning = true;
			this.cancellationPending = false;
			this.asyncOperation = AsyncOperationManager.CreateOperation(null);
			this.threadStart.BeginInvoke(argument, null, null);
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x0600316B RID: 12651 RVA: 0x000DF8E2 File Offset: 0x000DDAE2
		// (remove) Token: 0x0600316C RID: 12652 RVA: 0x000DF8F5 File Offset: 0x000DDAF5
		[SRCategory("PropertyCategoryAsynchronous")]
		[SRDescription("BackgroundWorker_RunWorkerCompleted")]
		[__DynamicallyInvokable]
		public event RunWorkerCompletedEventHandler RunWorkerCompleted
		{
			[__DynamicallyInvokable]
			add
			{
				base.Events.AddHandler(BackgroundWorker.runWorkerCompletedKey, value);
			}
			[__DynamicallyInvokable]
			remove
			{
				base.Events.RemoveHandler(BackgroundWorker.runWorkerCompletedKey, value);
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000DF908 File Offset: 0x000DDB08
		// (set) Token: 0x0600316E RID: 12654 RVA: 0x000DF910 File Offset: 0x000DDB10
		[SRCategory("PropertyCategoryAsynchronous")]
		[SRDescription("BackgroundWorker_WorkerReportsProgress")]
		[DefaultValue(false)]
		[__DynamicallyInvokable]
		public bool WorkerReportsProgress
		{
			[__DynamicallyInvokable]
			get
			{
				return this.workerReportsProgress;
			}
			[__DynamicallyInvokable]
			set
			{
				this.workerReportsProgress = value;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x000DF919 File Offset: 0x000DDB19
		// (set) Token: 0x06003170 RID: 12656 RVA: 0x000DF921 File Offset: 0x000DDB21
		[SRCategory("PropertyCategoryAsynchronous")]
		[SRDescription("BackgroundWorker_WorkerSupportsCancellation")]
		[DefaultValue(false)]
		[__DynamicallyInvokable]
		public bool WorkerSupportsCancellation
		{
			[__DynamicallyInvokable]
			get
			{
				return this.canCancelWorker;
			}
			[__DynamicallyInvokable]
			set
			{
				this.canCancelWorker = value;
			}
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000DF92C File Offset: 0x000DDB2C
		private void WorkerThreadStart(object argument)
		{
			object result = null;
			Exception error = null;
			bool cancelled = false;
			try
			{
				DoWorkEventArgs doWorkEventArgs = new DoWorkEventArgs(argument);
				this.OnDoWork(doWorkEventArgs);
				if (doWorkEventArgs.Cancel)
				{
					cancelled = true;
				}
				else
				{
					result = doWorkEventArgs.Result;
				}
			}
			catch (Exception ex)
			{
				error = ex;
			}
			RunWorkerCompletedEventArgs arg = new RunWorkerCompletedEventArgs(result, error, cancelled);
			this.asyncOperation.PostOperationCompleted(this.operationCompleted, arg);
		}

		// Token: 0x0400291C RID: 10524
		private static readonly object doWorkKey = new object();

		// Token: 0x0400291D RID: 10525
		private static readonly object runWorkerCompletedKey = new object();

		// Token: 0x0400291E RID: 10526
		private static readonly object progressChangedKey = new object();

		// Token: 0x0400291F RID: 10527
		private bool canCancelWorker;

		// Token: 0x04002920 RID: 10528
		private bool workerReportsProgress;

		// Token: 0x04002921 RID: 10529
		private bool cancellationPending;

		// Token: 0x04002922 RID: 10530
		private bool isRunning;

		// Token: 0x04002923 RID: 10531
		private AsyncOperation asyncOperation;

		// Token: 0x04002924 RID: 10532
		private readonly BackgroundWorker.WorkerThreadStartDelegate threadStart;

		// Token: 0x04002925 RID: 10533
		private readonly SendOrPostCallback operationCompleted;

		// Token: 0x04002926 RID: 10534
		private readonly SendOrPostCallback progressReporter;

		// Token: 0x02000890 RID: 2192
		// (Invoke) Token: 0x06004590 RID: 17808
		private delegate void WorkerThreadStartDelegate(object argument);
	}
}
