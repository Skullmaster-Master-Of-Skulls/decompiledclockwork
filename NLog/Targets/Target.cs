using System;
using System.Collections.Generic;
using System.Threading;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000145 RID: 325
	[NLogConfigurationItem]
	public abstract class Target : ISupportsInitialize, IDisposable
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0001A3D6 File Offset: 0x000185D6
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x0001A3DE File Offset: 0x000185DE
		public string Name { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0001A3E7 File Offset: 0x000185E7
		protected object SyncRoot
		{
			get
			{
				return this.lockObject;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0001A3EF File Offset: 0x000185EF
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0001A3F7 File Offset: 0x000185F7
		private protected LoggingConfiguration LoggingConfiguration { protected get; private set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0001A400 File Offset: 0x00018600
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0001A408 File Offset: 0x00018608
		private protected bool IsInitialized { protected get; private set; }

		// Token: 0x06000B78 RID: 2936 RVA: 0x0001A411 File Offset: 0x00018611
		internal List<Layout> GetAllLayouts()
		{
			if (!this.scannedForLayouts)
			{
				this.FindAllLayouts();
			}
			return this.allLayouts;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0001A427 File Offset: 0x00018627
		void ISupportsInitialize.Initialize(LoggingConfiguration configuration)
		{
			this.Initialize(configuration);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001A430 File Offset: 0x00018630
		void ISupportsInitialize.Close()
		{
			this.Close();
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001A438 File Offset: 0x00018638
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0001A448 File Offset: 0x00018648
		public void Flush(AsyncContinuation asyncContinuation)
		{
			if (asyncContinuation == null)
			{
				throw new ArgumentNullException("asyncContinuation");
			}
			lock (this.SyncRoot)
			{
				if (!this.IsInitialized)
				{
					asyncContinuation(null);
				}
				else
				{
					asyncContinuation = AsyncHelpers.PreventMultipleCalls(asyncContinuation);
					try
					{
						this.FlushAsync(asyncContinuation);
					}
					catch (Exception exception)
					{
						if (exception.MustBeRethrown())
						{
							throw;
						}
						asyncContinuation(exception);
					}
				}
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001A4D4 File Offset: 0x000186D4
		public void PrecalculateVolatileLayouts(LogEventInfo logEvent)
		{
			lock (this.SyncRoot)
			{
				if (this.IsInitialized && this.allLayouts != null)
				{
					foreach (Layout layout in this.allLayouts)
					{
						layout.Precalculate(logEvent);
					}
				}
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0001A560 File Offset: 0x00018760
		public override string ToString()
		{
			TargetAttribute targetAttribute = (TargetAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(TargetAttribute));
			if (targetAttribute != null)
			{
				return targetAttribute.Name + " Target[" + (this.Name ?? "(unnamed)") + "]";
			}
			return base.GetType().Name;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0001A5BC File Offset: 0x000187BC
		public void WriteAsyncLogEvent(AsyncLogEventInfo logEvent)
		{
			lock (this.SyncRoot)
			{
				if (!this.IsInitialized)
				{
					logEvent.Continuation(null);
				}
				else if (this.initializeException != null)
				{
					logEvent.Continuation(this.CreateInitException());
				}
				else
				{
					AsyncContinuation asyncContinuation = AsyncHelpers.PreventMultipleCalls(logEvent.Continuation);
					try
					{
						this.Write(logEvent.LogEvent.WithContinuation(asyncContinuation));
					}
					catch (Exception exception)
					{
						if (exception.MustBeRethrown())
						{
							throw;
						}
						asyncContinuation(exception);
					}
				}
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0001A66C File Offset: 0x0001886C
		public void WriteAsyncLogEvents(params AsyncLogEventInfo[] logEvents)
		{
			if (logEvents == null || logEvents.Length == 0)
			{
				return;
			}
			lock (this.SyncRoot)
			{
				if (!this.IsInitialized)
				{
					foreach (AsyncLogEventInfo asyncLogEventInfo in logEvents)
					{
						asyncLogEventInfo.Continuation(null);
					}
				}
				else if (this.initializeException != null)
				{
					foreach (AsyncLogEventInfo asyncLogEventInfo2 in logEvents)
					{
						asyncLogEventInfo2.Continuation(this.CreateInitException());
					}
				}
				else
				{
					AsyncLogEventInfo[] array = new AsyncLogEventInfo[logEvents.Length];
					for (int k = 0; k < logEvents.Length; k++)
					{
						array[k] = logEvents[k].LogEvent.WithContinuation(AsyncHelpers.PreventMultipleCalls(logEvents[k].Continuation));
					}
					try
					{
						this.Write(array);
					}
					catch (Exception exception)
					{
						if (exception.MustBeRethrown())
						{
							throw;
						}
						foreach (AsyncLogEventInfo asyncLogEventInfo3 in array)
						{
							asyncLogEventInfo3.Continuation(exception);
						}
					}
				}
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0001A7EC File Offset: 0x000189EC
		internal void Initialize(LoggingConfiguration configuration)
		{
			lock (this.SyncRoot)
			{
				this.LoggingConfiguration = configuration;
				if (!this.IsInitialized)
				{
					PropertyHelper.CheckRequiredParameters(this);
					this.IsInitialized = true;
					try
					{
						this.InitializeTarget();
						this.initializeException = null;
						if (!this.scannedForLayouts)
						{
							InternalLogger.Debug("InitializeTarget is done but not scanned For Layouts");
							this.FindAllLayouts();
						}
					}
					catch (Exception ex)
					{
						InternalLogger.Error(ex, "Error initializing target '{0}'.", new object[]
						{
							this
						});
						this.initializeException = ex;
						if (ex.MustBeRethrown())
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0001A8A0 File Offset: 0x00018AA0
		internal void Close()
		{
			lock (this.SyncRoot)
			{
				this.LoggingConfiguration = null;
				if (this.IsInitialized)
				{
					this.IsInitialized = false;
					try
					{
						if (this.initializeException == null)
						{
							this.CloseTarget();
						}
					}
					catch (Exception ex)
					{
						InternalLogger.Error(ex, "Error closing target '{0}'.", new object[]
						{
							this
						});
						if (ex.MustBeRethrown())
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0001A97C File Offset: 0x00018B7C
		internal void WriteAsyncLogEvents(AsyncLogEventInfo[] logEventInfos, AsyncContinuation continuation)
		{
			if (logEventInfos.Length == 0)
			{
				continuation(null);
				return;
			}
			AsyncLogEventInfo[] array = new AsyncLogEventInfo[logEventInfos.Length];
			int remaining = logEventInfos.Length;
			for (int i = 0; i < logEventInfos.Length; i++)
			{
				AsyncContinuation originalContinuation = logEventInfos[i].Continuation;
				AsyncContinuation asyncContinuation = delegate(Exception ex)
				{
					originalContinuation(ex);
					if (Interlocked.Decrement(ref remaining) == 0)
					{
						continuation(null);
					}
				};
				array[i] = logEventInfos[i].LogEvent.WithContinuation(asyncContinuation);
			}
			this.WriteAsyncLogEvents(array);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0001AA2D File Offset: 0x00018C2D
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.CloseTarget();
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001AA38 File Offset: 0x00018C38
		protected virtual void InitializeTarget()
		{
			this.FindAllLayouts();
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0001AA40 File Offset: 0x00018C40
		private void FindAllLayouts()
		{
			this.allLayouts = new List<Layout>(ObjectGraphScanner.FindReachableObjects<Layout>(new object[]
			{
				this
			}));
			InternalLogger.Trace("{0} has {1} layouts", new object[]
			{
				this,
				this.allLayouts.Count
			});
			this.scannedForLayouts = true;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0001AA99 File Offset: 0x00018C99
		protected virtual void CloseTarget()
		{
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0001AA9B File Offset: 0x00018C9B
		protected virtual void FlushAsync(AsyncContinuation asyncContinuation)
		{
			asyncContinuation(null);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0001AAA4 File Offset: 0x00018CA4
		protected virtual void Write(LogEventInfo logEvent)
		{
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		protected virtual void Write(AsyncLogEventInfo logEvent)
		{
			try
			{
				this.MergeEventProperties(logEvent.LogEvent);
				this.Write(logEvent.LogEvent);
				logEvent.Continuation(null);
			}
			catch (Exception exception)
			{
				if (exception.MustBeRethrown())
				{
					throw;
				}
				logEvent.Continuation(exception);
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0001AB08 File Offset: 0x00018D08
		protected virtual void Write(AsyncLogEventInfo[] logEvents)
		{
			for (int i = 0; i < logEvents.Length; i++)
			{
				this.Write(logEvents[i]);
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0001AB35 File Offset: 0x00018D35
		private Exception CreateInitException()
		{
			return new NLogRuntimeException("Target " + this + " failed to initialize.", this.initializeException);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0001AB54 File Offset: 0x00018D54
		protected void MergeEventProperties(LogEventInfo logEvent)
		{
			if (logEvent.Parameters == null)
			{
				return;
			}
			foreach (object obj in logEvent.Parameters)
			{
				LogEventInfo logEventInfo = obj as LogEventInfo;
				if (logEventInfo != null)
				{
					foreach (object key in logEventInfo.Properties.Keys)
					{
						logEvent.Properties.Add(key, logEventInfo.Properties[key]);
					}
					logEventInfo.Properties.Clear();
				}
			}
		}

		// Token: 0x040002C8 RID: 712
		private object lockObject = new object();

		// Token: 0x040002C9 RID: 713
		private List<Layout> allLayouts;

		// Token: 0x040002CA RID: 714
		private bool scannedForLayouts;

		// Token: 0x040002CB RID: 715
		private Exception initializeException;
	}
}
