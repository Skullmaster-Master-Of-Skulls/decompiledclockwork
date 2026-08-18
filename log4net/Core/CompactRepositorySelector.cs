using System;
using System.Collections;
using System.Reflection;
using log4net.Repository;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02000058 RID: 88
	public class CompactRepositorySelector : IRepositorySelector
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002E6 RID: 742 RVA: 0x0000A1F0 File Offset: 0x000083F0
		// (remove) Token: 0x060002E7 RID: 743 RVA: 0x0000A228 File Offset: 0x00008428
		private event LoggerRepositoryCreationEventHandler m_loggerRepositoryCreatedEvent;

		// Token: 0x060002E8 RID: 744 RVA: 0x0000A260 File Offset: 0x00008460
		public CompactRepositorySelector(Type defaultRepositoryType)
		{
			if (defaultRepositoryType == null)
			{
				throw new ArgumentNullException("defaultRepositoryType");
			}
			if (!typeof(ILoggerRepository).IsAssignableFrom(defaultRepositoryType))
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("defaultRepositoryType", defaultRepositoryType, "Parameter: defaultRepositoryType, Value: [" + defaultRepositoryType + "] out of range. Argument must implement the ILoggerRepository interface");
			}
			this.m_defaultRepositoryType = defaultRepositoryType;
			LogLog.Debug(CompactRepositorySelector.declaringType, "defaultRepositoryType [" + this.m_defaultRepositoryType + "]");
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000A2E6 File Offset: 0x000084E6
		public ILoggerRepository GetRepository(Assembly assembly)
		{
			return this.CreateRepository(assembly, this.m_defaultRepositoryType);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public ILoggerRepository GetRepository(string repositoryName)
		{
			if (repositoryName == null)
			{
				throw new ArgumentNullException("repositoryName");
			}
			ILoggerRepository result;
			lock (this)
			{
				ILoggerRepository loggerRepository = this.m_name2repositoryMap[repositoryName] as ILoggerRepository;
				if (loggerRepository == null)
				{
					throw new LogException("Repository [" + repositoryName + "] is NOT defined.");
				}
				result = loggerRepository;
			}
			return result;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000A36C File Offset: 0x0000856C
		public ILoggerRepository CreateRepository(Assembly assembly, Type repositoryType)
		{
			if (repositoryType == null)
			{
				repositoryType = this.m_defaultRepositoryType;
			}
			ILoggerRepository result;
			lock (this)
			{
				ILoggerRepository loggerRepository = this.m_name2repositoryMap["log4net-default-repository"] as ILoggerRepository;
				if (loggerRepository == null)
				{
					loggerRepository = this.CreateRepository("log4net-default-repository", repositoryType);
				}
				result = loggerRepository;
			}
			return result;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000A3DC File Offset: 0x000085DC
		public ILoggerRepository CreateRepository(string repositoryName, Type repositoryType)
		{
			if (repositoryName == null)
			{
				throw new ArgumentNullException("repositoryName");
			}
			if (repositoryType == null)
			{
				repositoryType = this.m_defaultRepositoryType;
			}
			ILoggerRepository result;
			lock (this)
			{
				ILoggerRepository loggerRepository = this.m_name2repositoryMap[repositoryName] as ILoggerRepository;
				if (loggerRepository != null)
				{
					throw new LogException("Repository [" + repositoryName + "] is already defined. Repositories cannot be redefined.");
				}
				LogLog.Debug(CompactRepositorySelector.declaringType, string.Concat(new object[]
				{
					"Creating repository [",
					repositoryName,
					"] using type [",
					repositoryType,
					"]"
				}));
				loggerRepository = (ILoggerRepository)Activator.CreateInstance(repositoryType);
				loggerRepository.Name = repositoryName;
				this.m_name2repositoryMap[repositoryName] = loggerRepository;
				this.OnLoggerRepositoryCreatedEvent(loggerRepository);
				result = loggerRepository;
			}
			return result;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000A4C8 File Offset: 0x000086C8
		public bool ExistsRepository(string repositoryName)
		{
			bool result;
			lock (this)
			{
				result = this.m_name2repositoryMap.ContainsKey(repositoryName);
			}
			return result;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000A50C File Offset: 0x0000870C
		public ILoggerRepository[] GetAllRepositories()
		{
			ILoggerRepository[] result;
			lock (this)
			{
				ICollection values = this.m_name2repositoryMap.Values;
				ILoggerRepository[] array = new ILoggerRepository[values.Count];
				values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002EF RID: 751 RVA: 0x0000A568 File Offset: 0x00008768
		// (remove) Token: 0x060002F0 RID: 752 RVA: 0x0000A571 File Offset: 0x00008771
		public event LoggerRepositoryCreationEventHandler LoggerRepositoryCreatedEvent
		{
			add
			{
				this.m_loggerRepositoryCreatedEvent += value;
			}
			remove
			{
				this.m_loggerRepositoryCreatedEvent -= value;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000A57C File Offset: 0x0000877C
		protected virtual void OnLoggerRepositoryCreatedEvent(ILoggerRepository repository)
		{
			LoggerRepositoryCreationEventHandler loggerRepositoryCreatedEvent = this.m_loggerRepositoryCreatedEvent;
			if (loggerRepositoryCreatedEvent != null)
			{
				loggerRepositoryCreatedEvent(this, new LoggerRepositoryCreationEventArgs(repository));
			}
		}

		// Token: 0x04000158 RID: 344
		private const string DefaultRepositoryName = "log4net-default-repository";

		// Token: 0x04000159 RID: 345
		private readonly Hashtable m_name2repositoryMap = new Hashtable();

		// Token: 0x0400015A RID: 346
		private readonly Type m_defaultRepositoryType;

		// Token: 0x0400015C RID: 348
		private static readonly Type declaringType = typeof(CompactRepositorySelector);
	}
}
