using System;
using System.Runtime.Remoting;
using System.Security;
using log4net.Appender;
using log4net.Core;
using log4net.Repository;
using log4net.Util;

namespace log4net.Plugin
{
	// Token: 0x020000C1 RID: 193
	public class RemoteLoggingServerPlugin : PluginSkeleton
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x00011750 File Offset: 0x0000F950
		public RemoteLoggingServerPlugin() : base("RemoteLoggingServerPlugin:Unset URI")
		{
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001175D File Offset: 0x0000F95D
		public RemoteLoggingServerPlugin(string sinkUri) : base("RemoteLoggingServerPlugin:" + sinkUri)
		{
			this.m_sinkUri = sinkUri;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00011777 File Offset: 0x0000F977
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0001177F File Offset: 0x0000F97F
		public virtual string SinkUri
		{
			get
			{
				return this.m_sinkUri;
			}
			set
			{
				this.m_sinkUri = value;
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00011788 File Offset: 0x0000F988
		[SecuritySafeCritical]
		public override void Attach(ILoggerRepository repository)
		{
			base.Attach(repository);
			this.m_sink = new RemoteLoggingServerPlugin.RemoteLoggingSinkImpl(repository);
			try
			{
				RemotingServices.Marshal(this.m_sink, this.m_sinkUri, typeof(RemotingAppender.IRemoteLoggingSink));
			}
			catch (Exception exception)
			{
				LogLog.Error(RemoteLoggingServerPlugin.declaringType, "Failed to Marshal remoting sink", exception);
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000117EC File Offset: 0x0000F9EC
		[SecuritySafeCritical]
		public override void Shutdown()
		{
			RemotingServices.Disconnect(this.m_sink);
			this.m_sink = null;
			base.Shutdown();
		}

		// Token: 0x04000247 RID: 583
		private RemoteLoggingServerPlugin.RemoteLoggingSinkImpl m_sink;

		// Token: 0x04000248 RID: 584
		private string m_sinkUri;

		// Token: 0x04000249 RID: 585
		private static readonly Type declaringType = typeof(RemoteLoggingServerPlugin);

		// Token: 0x020000C2 RID: 194
		private class RemoteLoggingSinkImpl : MarshalByRefObject, RemotingAppender.IRemoteLoggingSink
		{
			// Token: 0x0600059F RID: 1439 RVA: 0x00011818 File Offset: 0x0000FA18
			public RemoteLoggingSinkImpl(ILoggerRepository repository)
			{
				this.m_repository = repository;
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x00011828 File Offset: 0x0000FA28
			public void LogEvents(LoggingEvent[] events)
			{
				if (events != null)
				{
					foreach (LoggingEvent loggingEvent in events)
					{
						if (loggingEvent != null)
						{
							this.m_repository.Log(loggingEvent);
						}
					}
				}
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x0001185B File Offset: 0x0000FA5B
			[SecurityCritical]
			public override object InitializeLifetimeService()
			{
				return null;
			}

			// Token: 0x0400024A RID: 586
			private readonly ILoggerRepository m_repository;
		}
	}
}
