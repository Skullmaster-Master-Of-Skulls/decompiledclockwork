using System;
using System.Collections;
using System.Xml;
using log4net.Appender;
using log4net.Core;
using log4net.Util;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x020000CD RID: 205
	public class Hierarchy : LoggerRepositorySkeleton, IBasicRepositoryConfigurator, IXmlRepositoryConfigurator
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600060D RID: 1549 RVA: 0x000124A5 File Offset: 0x000106A5
		// (remove) Token: 0x0600060E RID: 1550 RVA: 0x000124AE File Offset: 0x000106AE
		public event LoggerCreationEventHandler LoggerCreatedEvent
		{
			add
			{
				this.m_loggerCreatedEvent += value;
			}
			remove
			{
				this.m_loggerCreatedEvent -= value;
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000124B7 File Offset: 0x000106B7
		public Hierarchy() : this(new DefaultLoggerFactory())
		{
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000124C4 File Offset: 0x000106C4
		public Hierarchy(PropertiesDictionary properties) : this(properties, new DefaultLoggerFactory())
		{
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000124D2 File Offset: 0x000106D2
		public Hierarchy(ILoggerFactory loggerFactory) : this(new PropertiesDictionary(), loggerFactory)
		{
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000124E0 File Offset: 0x000106E0
		public Hierarchy(PropertiesDictionary properties, ILoggerFactory loggerFactory) : base(properties)
		{
			if (loggerFactory == null)
			{
				throw new ArgumentNullException("loggerFactory");
			}
			this.m_defaultFactory = loggerFactory;
			this.m_ht = Hashtable.Synchronized(new Hashtable());
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001250E File Offset: 0x0001070E
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00012516 File Offset: 0x00010716
		public bool EmittedNoAppenderWarning
		{
			get
			{
				return this.m_emittedNoAppenderWarning;
			}
			set
			{
				this.m_emittedNoAppenderWarning = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00012520 File Offset: 0x00010720
		public Logger Root
		{
			get
			{
				if (this.m_root == null)
				{
					lock (this)
					{
						if (this.m_root == null)
						{
							Logger logger = this.m_defaultFactory.CreateLogger(this, null);
							logger.Hierarchy = this;
							this.m_root = logger;
						}
					}
				}
				return this.m_root;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00012588 File Offset: 0x00010788
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00012590 File Offset: 0x00010790
		public ILoggerFactory LoggerFactory
		{
			get
			{
				return this.m_defaultFactory;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_defaultFactory = value;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000125A8 File Offset: 0x000107A8
		public override ILogger Exists(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			ILogger result;
			lock (this.m_ht)
			{
				result = (this.m_ht[new LoggerKey(name)] as Logger);
			}
			return result;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00012608 File Offset: 0x00010808
		public override ILogger[] GetCurrentLoggers()
		{
			ILogger[] result;
			lock (this.m_ht)
			{
				ArrayList arrayList = new ArrayList(this.m_ht.Count);
				foreach (object obj in this.m_ht.Values)
				{
					if (obj is Logger)
					{
						arrayList.Add(obj);
					}
				}
				result = (Logger[])arrayList.ToArray(typeof(Logger));
			}
			return result;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000126C4 File Offset: 0x000108C4
		public override ILogger GetLogger(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetLogger(name, this.m_defaultFactory);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000126E4 File Offset: 0x000108E4
		public override void Shutdown()
		{
			LogLog.Debug(Hierarchy.declaringType, "Shutdown called on Hierarchy [" + this.Name + "]");
			this.Root.CloseNestedAppenders();
			lock (this.m_ht)
			{
				ILogger[] currentLoggers = this.GetCurrentLoggers();
				foreach (Logger logger in currentLoggers)
				{
					logger.CloseNestedAppenders();
				}
				this.Root.RemoveAllAppenders();
				foreach (Logger logger2 in currentLoggers)
				{
					logger2.RemoveAllAppenders();
				}
			}
			base.Shutdown();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000127B0 File Offset: 0x000109B0
		public override void ResetConfiguration()
		{
			this.Root.Level = this.LevelMap.LookupWithDefault(Level.Debug);
			this.Threshold = this.LevelMap.LookupWithDefault(Level.All);
			lock (this.m_ht)
			{
				this.Shutdown();
				foreach (Logger logger in this.GetCurrentLoggers())
				{
					logger.Level = null;
					logger.Additivity = true;
				}
			}
			base.ResetConfiguration();
			this.OnConfigurationChanged(null);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00012860 File Offset: 0x00010A60
		public override void Log(LoggingEvent logEvent)
		{
			if (logEvent == null)
			{
				throw new ArgumentNullException("logEvent");
			}
			this.GetLogger(logEvent.LoggerName, this.m_defaultFactory).Log(logEvent);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00012888 File Offset: 0x00010A88
		public override IAppender[] GetAppenders()
		{
			ArrayList arrayList = new ArrayList();
			Hierarchy.CollectAppenders(arrayList, this.Root);
			foreach (Logger container in this.GetCurrentLoggers())
			{
				Hierarchy.CollectAppenders(arrayList, container);
			}
			return (IAppender[])arrayList.ToArray(typeof(IAppender));
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000128E4 File Offset: 0x00010AE4
		private static void CollectAppender(ArrayList appenderList, IAppender appender)
		{
			if (!appenderList.Contains(appender))
			{
				appenderList.Add(appender);
				IAppenderAttachable appenderAttachable = appender as IAppenderAttachable;
				if (appenderAttachable != null)
				{
					Hierarchy.CollectAppenders(appenderList, appenderAttachable);
				}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00012914 File Offset: 0x00010B14
		private static void CollectAppenders(ArrayList appenderList, IAppenderAttachable container)
		{
			foreach (IAppender appender in container.Appenders)
			{
				Hierarchy.CollectAppender(appenderList, appender);
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00012968 File Offset: 0x00010B68
		void IBasicRepositoryConfigurator.Configure(IAppender appender)
		{
			this.BasicRepositoryConfigure(new IAppender[]
			{
				appender
			});
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00012987 File Offset: 0x00010B87
		void IBasicRepositoryConfigurator.Configure(params IAppender[] appenders)
		{
			this.BasicRepositoryConfigure(appenders);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00012990 File Offset: 0x00010B90
		protected void BasicRepositoryConfigure(params IAppender[] appenders)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				foreach (IAppender newAppender in appenders)
				{
					this.Root.AddAppender(newAppender);
				}
			}
			this.Configured = true;
			this.ConfigurationMessages = arrayList;
			this.OnConfigurationChanged(new ConfigurationChangedEventArgs(arrayList));
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00012A08 File Offset: 0x00010C08
		void IXmlRepositoryConfigurator.Configure(XmlElement element)
		{
			this.XmlRepositoryConfigure(element);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00012A14 File Offset: 0x00010C14
		protected void XmlRepositoryConfigure(XmlElement element)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlHierarchyConfigurator xmlHierarchyConfigurator = new XmlHierarchyConfigurator(this);
				xmlHierarchyConfigurator.Configure(element);
			}
			this.Configured = true;
			this.ConfigurationMessages = arrayList;
			this.OnConfigurationChanged(new ConfigurationChangedEventArgs(arrayList));
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00012A74 File Offset: 0x00010C74
		public bool IsDisabled(Level level)
		{
			if (level == null)
			{
				throw new ArgumentNullException("level");
			}
			return !this.Configured || this.Threshold > level;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00012A9C File Offset: 0x00010C9C
		public void Clear()
		{
			lock (this.m_ht)
			{
				this.m_ht.Clear();
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00012AE4 File Offset: 0x00010CE4
		public Logger GetLogger(string name, ILoggerFactory factory)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			LoggerKey key = new LoggerKey(name);
			Logger result;
			lock (this.m_ht)
			{
				object obj = this.m_ht[key];
				if (obj == null)
				{
					Logger logger = factory.CreateLogger(this, name);
					logger.Hierarchy = this;
					this.m_ht[key] = logger;
					this.UpdateParents(logger);
					this.OnLoggerCreationEvent(logger);
					result = logger;
				}
				else
				{
					Logger logger2 = obj as Logger;
					if (logger2 != null)
					{
						result = logger2;
					}
					else
					{
						ProvisionNode provisionNode = obj as ProvisionNode;
						if (provisionNode != null)
						{
							Logger logger = factory.CreateLogger(this, name);
							logger.Hierarchy = this;
							this.m_ht[key] = logger;
							Hierarchy.UpdateChildren(provisionNode, logger);
							this.UpdateParents(logger);
							this.OnLoggerCreationEvent(logger);
							result = logger;
						}
						else
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00012BE0 File Offset: 0x00010DE0
		protected virtual void OnLoggerCreationEvent(Logger logger)
		{
			LoggerCreationEventHandler loggerCreatedEvent = this.m_loggerCreatedEvent;
			if (loggerCreatedEvent != null)
			{
				loggerCreatedEvent(this, new LoggerCreationEventArgs(logger));
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00012C04 File Offset: 0x00010E04
		private void UpdateParents(Logger log)
		{
			string name = log.Name;
			int length = name.Length;
			bool flag = false;
			for (int i = name.LastIndexOf('.', length - 1); i >= 0; i = name.LastIndexOf('.', i - 1))
			{
				string name2 = name.Substring(0, i);
				LoggerKey key = new LoggerKey(name2);
				object obj = this.m_ht[key];
				if (obj == null)
				{
					ProvisionNode value = new ProvisionNode(log);
					this.m_ht[key] = value;
				}
				else
				{
					Logger logger = obj as Logger;
					if (logger != null)
					{
						flag = true;
						log.Parent = logger;
						break;
					}
					ProvisionNode provisionNode = obj as ProvisionNode;
					if (provisionNode != null)
					{
						provisionNode.Add(log);
					}
					else
					{
						LogLog.Error(Hierarchy.declaringType, "Unexpected object type [" + obj.GetType() + "] in ht.", new LogException());
					}
				}
				if (i == 0)
				{
					break;
				}
			}
			if (!flag)
			{
				log.Parent = this.Root;
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00012CEC File Offset: 0x00010EEC
		private static void UpdateChildren(ProvisionNode pn, Logger log)
		{
			for (int i = 0; i < pn.Count; i++)
			{
				Logger logger = (Logger)pn[i];
				if (!logger.Parent.Name.StartsWith(log.Name))
				{
					log.Parent = logger.Parent;
					logger.Parent = log;
				}
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00012D44 File Offset: 0x00010F44
		internal void AddLevel(Hierarchy.LevelEntry levelEntry)
		{
			if (levelEntry == null)
			{
				throw new ArgumentNullException("levelEntry");
			}
			if (levelEntry.Name == null)
			{
				throw new ArgumentNullException("levelEntry.Name");
			}
			if (levelEntry.Value == -1)
			{
				Level level = this.LevelMap[levelEntry.Name];
				if (level == null)
				{
					throw new InvalidOperationException("Cannot redefine level [" + levelEntry.Name + "] because it is not defined in the LevelMap. To define the level supply the level value.");
				}
				levelEntry.Value = level.Value;
			}
			this.LevelMap.Add(levelEntry.Name, levelEntry.Value, levelEntry.DisplayName);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00012DDA File Offset: 0x00010FDA
		internal void AddProperty(PropertyEntry propertyEntry)
		{
			if (propertyEntry == null)
			{
				throw new ArgumentNullException("propertyEntry");
			}
			if (propertyEntry.Key == null)
			{
				throw new ArgumentNullException("propertyEntry.Key");
			}
			base.Properties[propertyEntry.Key] = propertyEntry.Value;
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600062E RID: 1582 RVA: 0x00012E14 File Offset: 0x00011014
		// (remove) Token: 0x0600062F RID: 1583 RVA: 0x00012E4C File Offset: 0x0001104C
		private event LoggerCreationEventHandler m_loggerCreatedEvent;

		// Token: 0x04000260 RID: 608
		private ILoggerFactory m_defaultFactory;

		// Token: 0x04000261 RID: 609
		private Hashtable m_ht;

		// Token: 0x04000262 RID: 610
		private Logger m_root;

		// Token: 0x04000263 RID: 611
		private bool m_emittedNoAppenderWarning;

		// Token: 0x04000265 RID: 613
		private static readonly Type declaringType = typeof(Hierarchy);

		// Token: 0x020000CE RID: 206
		internal class LevelEntry
		{
			// Token: 0x17000154 RID: 340
			// (get) Token: 0x06000631 RID: 1585 RVA: 0x00012E92 File Offset: 0x00011092
			// (set) Token: 0x06000632 RID: 1586 RVA: 0x00012E9A File Offset: 0x0001109A
			public int Value
			{
				get
				{
					return this.m_levelValue;
				}
				set
				{
					this.m_levelValue = value;
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000633 RID: 1587 RVA: 0x00012EA3 File Offset: 0x000110A3
			// (set) Token: 0x06000634 RID: 1588 RVA: 0x00012EAB File Offset: 0x000110AB
			public string Name
			{
				get
				{
					return this.m_levelName;
				}
				set
				{
					this.m_levelName = value;
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000635 RID: 1589 RVA: 0x00012EB4 File Offset: 0x000110B4
			// (set) Token: 0x06000636 RID: 1590 RVA: 0x00012EBC File Offset: 0x000110BC
			public string DisplayName
			{
				get
				{
					return this.m_levelDisplayName;
				}
				set
				{
					this.m_levelDisplayName = value;
				}
			}

			// Token: 0x06000637 RID: 1591 RVA: 0x00012EC8 File Offset: 0x000110C8
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					"LevelEntry(Value=",
					this.m_levelValue,
					", Name=",
					this.m_levelName,
					", DisplayName=",
					this.m_levelDisplayName,
					")"
				});
			}

			// Token: 0x04000266 RID: 614
			private int m_levelValue = -1;

			// Token: 0x04000267 RID: 615
			private string m_levelName;

			// Token: 0x04000268 RID: 616
			private string m_levelDisplayName;
		}
	}
}
