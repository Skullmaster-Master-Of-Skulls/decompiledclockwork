using System;
using System.Collections;
using log4net.Appender;
using log4net.Core;
using log4net.ObjectRenderer;
using log4net.Plugin;
using log4net.Util;

namespace log4net.Repository
{
	// Token: 0x020000CA RID: 202
	public abstract class LoggerRepositorySkeleton : ILoggerRepository, IFlushable
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060005E0 RID: 1504 RVA: 0x00011EAC File Offset: 0x000100AC
		// (remove) Token: 0x060005E1 RID: 1505 RVA: 0x00011EE4 File Offset: 0x000100E4
		private event LoggerRepositoryShutdownEventHandler m_shutdownEvent;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060005E2 RID: 1506 RVA: 0x00011F1C File Offset: 0x0001011C
		// (remove) Token: 0x060005E3 RID: 1507 RVA: 0x00011F54 File Offset: 0x00010154
		private event LoggerRepositoryConfigurationResetEventHandler m_configurationResetEvent;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060005E4 RID: 1508 RVA: 0x00011F8C File Offset: 0x0001018C
		// (remove) Token: 0x060005E5 RID: 1509 RVA: 0x00011FC4 File Offset: 0x000101C4
		private event LoggerRepositoryConfigurationChangedEventHandler m_configurationChangedEvent;

		// Token: 0x060005E6 RID: 1510 RVA: 0x00011FF9 File Offset: 0x000101F9
		protected LoggerRepositorySkeleton() : this(new PropertiesDictionary())
		{
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00012008 File Offset: 0x00010208
		protected LoggerRepositorySkeleton(PropertiesDictionary properties)
		{
			this.m_properties = properties;
			this.m_rendererMap = new RendererMap();
			this.m_pluginMap = new PluginMap(this);
			this.m_levelMap = new LevelMap();
			this.m_configurationMessages = EmptyCollection.Instance;
			this.m_configured = false;
			this.AddBuiltinLevels();
			this.m_threshold = Level.All;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00012067 File Offset: 0x00010267
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0001206F File Offset: 0x0001026F
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00012078 File Offset: 0x00010278
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00012080 File Offset: 0x00010280
		public virtual Level Threshold
		{
			get
			{
				return this.m_threshold;
			}
			set
			{
				if (value != null)
				{
					this.m_threshold = value;
					return;
				}
				LogLog.Warn(LoggerRepositorySkeleton.declaringType, "LoggerRepositorySkeleton: Threshold cannot be set to null. Setting to ALL");
				this.m_threshold = Level.All;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x000120AD File Offset: 0x000102AD
		public virtual RendererMap RendererMap
		{
			get
			{
				return this.m_rendererMap;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x000120B5 File Offset: 0x000102B5
		public virtual PluginMap PluginMap
		{
			get
			{
				return this.m_pluginMap;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x000120BD File Offset: 0x000102BD
		public virtual LevelMap LevelMap
		{
			get
			{
				return this.m_levelMap;
			}
		}

		// Token: 0x060005EF RID: 1519
		public abstract ILogger Exists(string name);

		// Token: 0x060005F0 RID: 1520
		public abstract ILogger[] GetCurrentLoggers();

		// Token: 0x060005F1 RID: 1521
		public abstract ILogger GetLogger(string name);

		// Token: 0x060005F2 RID: 1522 RVA: 0x000120C8 File Offset: 0x000102C8
		public virtual void Shutdown()
		{
			foreach (IPlugin plugin in this.PluginMap.AllPlugins)
			{
				plugin.Shutdown();
			}
			this.OnShutdown(null);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00012128 File Offset: 0x00010328
		public virtual void ResetConfiguration()
		{
			this.m_rendererMap.Clear();
			this.m_levelMap.Clear();
			this.m_configurationMessages = EmptyCollection.Instance;
			this.AddBuiltinLevels();
			this.Configured = false;
			this.OnConfigurationReset(null);
		}

		// Token: 0x060005F4 RID: 1524
		public abstract void Log(LoggingEvent logEvent);

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001215F File Offset: 0x0001035F
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00012167 File Offset: 0x00010367
		public virtual bool Configured
		{
			get
			{
				return this.m_configured;
			}
			set
			{
				this.m_configured = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00012170 File Offset: 0x00010370
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00012178 File Offset: 0x00010378
		public virtual ICollection ConfigurationMessages
		{
			get
			{
				return this.m_configurationMessages;
			}
			set
			{
				this.m_configurationMessages = value;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060005F9 RID: 1529 RVA: 0x00012181 File Offset: 0x00010381
		// (remove) Token: 0x060005FA RID: 1530 RVA: 0x0001218A File Offset: 0x0001038A
		public event LoggerRepositoryShutdownEventHandler ShutdownEvent
		{
			add
			{
				this.m_shutdownEvent += value;
			}
			remove
			{
				this.m_shutdownEvent -= value;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060005FB RID: 1531 RVA: 0x00012193 File Offset: 0x00010393
		// (remove) Token: 0x060005FC RID: 1532 RVA: 0x0001219C File Offset: 0x0001039C
		public event LoggerRepositoryConfigurationResetEventHandler ConfigurationReset
		{
			add
			{
				this.m_configurationResetEvent += value;
			}
			remove
			{
				this.m_configurationResetEvent -= value;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060005FD RID: 1533 RVA: 0x000121A5 File Offset: 0x000103A5
		// (remove) Token: 0x060005FE RID: 1534 RVA: 0x000121AE File Offset: 0x000103AE
		public event LoggerRepositoryConfigurationChangedEventHandler ConfigurationChanged
		{
			add
			{
				this.m_configurationChangedEvent += value;
			}
			remove
			{
				this.m_configurationChangedEvent -= value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000121B7 File Offset: 0x000103B7
		public PropertiesDictionary Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x06000600 RID: 1536
		public abstract IAppender[] GetAppenders();

		// Token: 0x06000601 RID: 1537 RVA: 0x000121C0 File Offset: 0x000103C0
		private void AddBuiltinLevels()
		{
			this.m_levelMap.Add(Level.Off);
			this.m_levelMap.Add(Level.Emergency);
			this.m_levelMap.Add(Level.Fatal);
			this.m_levelMap.Add(Level.Alert);
			this.m_levelMap.Add(Level.Critical);
			this.m_levelMap.Add(Level.Severe);
			this.m_levelMap.Add(Level.Error);
			this.m_levelMap.Add(Level.Warn);
			this.m_levelMap.Add(Level.Notice);
			this.m_levelMap.Add(Level.Info);
			this.m_levelMap.Add(Level.Debug);
			this.m_levelMap.Add(Level.Fine);
			this.m_levelMap.Add(Level.Trace);
			this.m_levelMap.Add(Level.Finer);
			this.m_levelMap.Add(Level.Verbose);
			this.m_levelMap.Add(Level.Finest);
			this.m_levelMap.Add(Level.All);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000122DD File Offset: 0x000104DD
		public virtual void AddRenderer(Type typeToRender, IObjectRenderer rendererInstance)
		{
			if (typeToRender == null)
			{
				throw new ArgumentNullException("typeToRender");
			}
			if (rendererInstance == null)
			{
				throw new ArgumentNullException("rendererInstance");
			}
			this.m_rendererMap.Put(typeToRender, rendererInstance);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00012310 File Offset: 0x00010510
		protected virtual void OnShutdown(EventArgs e)
		{
			if (e == null)
			{
				e = EventArgs.Empty;
			}
			LoggerRepositoryShutdownEventHandler shutdownEvent = this.m_shutdownEvent;
			if (shutdownEvent != null)
			{
				shutdownEvent(this, e);
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001233C File Offset: 0x0001053C
		protected virtual void OnConfigurationReset(EventArgs e)
		{
			if (e == null)
			{
				e = EventArgs.Empty;
			}
			LoggerRepositoryConfigurationResetEventHandler configurationResetEvent = this.m_configurationResetEvent;
			if (configurationResetEvent != null)
			{
				configurationResetEvent(this, e);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00012368 File Offset: 0x00010568
		protected virtual void OnConfigurationChanged(EventArgs e)
		{
			if (e == null)
			{
				e = EventArgs.Empty;
			}
			LoggerRepositoryConfigurationChangedEventHandler configurationChangedEvent = this.m_configurationChangedEvent;
			if (configurationChangedEvent != null)
			{
				configurationChangedEvent(this, e);
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00012391 File Offset: 0x00010591
		public void RaiseConfigurationChanged(EventArgs e)
		{
			this.OnConfigurationChanged(e);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001239C File Offset: 0x0001059C
		private static int GetWaitTime(DateTime startTimeUtc, int millisecondsTimeout)
		{
			if (millisecondsTimeout == -1)
			{
				return -1;
			}
			if (millisecondsTimeout == 0)
			{
				return 0;
			}
			int num = (int)(DateTime.UtcNow - startTimeUtc).TotalMilliseconds;
			int num2 = millisecondsTimeout - num;
			if (num2 < 0)
			{
				num2 = 0;
			}
			return num2;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000123D4 File Offset: 0x000105D4
		public bool Flush(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", "Timeout must be -1 (Timeout.Infinite) or non-negative");
			}
			bool result = true;
			DateTime utcNow = DateTime.UtcNow;
			foreach (IAppender appender in this.GetAppenders())
			{
				IFlushable flushable = appender as IFlushable;
				if (flushable != null && appender is BufferingAppenderSkeleton)
				{
					int waitTime = LoggerRepositorySkeleton.GetWaitTime(utcNow, millisecondsTimeout);
					if (!flushable.Flush(waitTime))
					{
						result = false;
					}
				}
			}
			foreach (IAppender appender2 in this.GetAppenders())
			{
				IFlushable flushable2 = appender2 as IFlushable;
				if (flushable2 != null && !(appender2 is BufferingAppenderSkeleton))
				{
					int waitTime2 = LoggerRepositorySkeleton.GetWaitTime(utcNow, millisecondsTimeout);
					if (!flushable2.Flush(waitTime2))
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x04000254 RID: 596
		private string m_name;

		// Token: 0x04000255 RID: 597
		private RendererMap m_rendererMap;

		// Token: 0x04000256 RID: 598
		private PluginMap m_pluginMap;

		// Token: 0x04000257 RID: 599
		private LevelMap m_levelMap;

		// Token: 0x04000258 RID: 600
		private Level m_threshold;

		// Token: 0x04000259 RID: 601
		private bool m_configured;

		// Token: 0x0400025A RID: 602
		private ICollection m_configurationMessages;

		// Token: 0x0400025E RID: 606
		private PropertiesDictionary m_properties;

		// Token: 0x0400025F RID: 607
		private static readonly Type declaringType = typeof(LoggerRepositorySkeleton);
	}
}
