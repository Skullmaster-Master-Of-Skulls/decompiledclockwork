using System;
using System.Collections;
using System.IO;
using System.Reflection;
using log4net.Config;
using log4net.Plugin;
using log4net.Repository;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02000059 RID: 89
	public class DefaultRepositorySelector : IRepositorySelector
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002F3 RID: 755 RVA: 0x0000A5B1 File Offset: 0x000087B1
		// (remove) Token: 0x060002F4 RID: 756 RVA: 0x0000A5BA File Offset: 0x000087BA
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

		// Token: 0x060002F5 RID: 757 RVA: 0x0000A5C4 File Offset: 0x000087C4
		public DefaultRepositorySelector(Type defaultRepositoryType)
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
			LogLog.Debug(DefaultRepositorySelector.declaringType, "defaultRepositoryType [" + this.m_defaultRepositoryType + "]");
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000A660 File Offset: 0x00008860
		public ILoggerRepository GetRepository(Assembly repositoryAssembly)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			return this.CreateRepository(repositoryAssembly, this.m_defaultRepositoryType);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000A684 File Offset: 0x00008884
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

		// Token: 0x060002F8 RID: 760 RVA: 0x0000A6F8 File Offset: 0x000088F8
		public ILoggerRepository CreateRepository(Assembly repositoryAssembly, Type repositoryType)
		{
			return this.CreateRepository(repositoryAssembly, repositoryType, "log4net-default-repository", true);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000A708 File Offset: 0x00008908
		public ILoggerRepository CreateRepository(Assembly repositoryAssembly, Type repositoryType, string repositoryName, bool readAssemblyAttributes)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			if (repositoryType == null)
			{
				repositoryType = this.m_defaultRepositoryType;
			}
			ILoggerRepository result;
			lock (this)
			{
				ILoggerRepository loggerRepository = this.m_assembly2repositoryMap[repositoryAssembly] as ILoggerRepository;
				if (loggerRepository == null)
				{
					LogLog.Debug(DefaultRepositorySelector.declaringType, "Creating repository for assembly [" + repositoryAssembly + "]");
					string text = repositoryName;
					Type type = repositoryType;
					if (readAssemblyAttributes)
					{
						this.GetInfoForAssembly(repositoryAssembly, ref text, ref type);
					}
					LogLog.Debug(DefaultRepositorySelector.declaringType, string.Concat(new object[]
					{
						"Assembly [",
						repositoryAssembly,
						"] using repository [",
						text,
						"] and repository type [",
						type,
						"]"
					}));
					loggerRepository = (this.m_name2repositoryMap[text] as ILoggerRepository);
					if (loggerRepository == null)
					{
						loggerRepository = this.CreateRepository(text, type);
						if (!readAssemblyAttributes)
						{
							goto IL_19A;
						}
						try
						{
							this.LoadAliases(repositoryAssembly, loggerRepository);
							this.LoadPlugins(repositoryAssembly, loggerRepository);
							this.ConfigureRepository(repositoryAssembly, loggerRepository);
							goto IL_19A;
						}
						catch (Exception exception)
						{
							LogLog.Error(DefaultRepositorySelector.declaringType, "Failed to configure repository [" + text + "] from assembly attributes.", exception);
							goto IL_19A;
						}
					}
					LogLog.Debug(DefaultRepositorySelector.declaringType, string.Concat(new string[]
					{
						"repository [",
						text,
						"] already exists, using repository type [",
						loggerRepository.GetType().FullName,
						"]"
					}));
					if (readAssemblyAttributes)
					{
						try
						{
							this.LoadPlugins(repositoryAssembly, loggerRepository);
						}
						catch (Exception exception2)
						{
							LogLog.Error(DefaultRepositorySelector.declaringType, "Failed to configure repository [" + text + "] from assembly attributes.", exception2);
						}
					}
					IL_19A:
					this.m_assembly2repositoryMap[repositoryAssembly] = loggerRepository;
				}
				result = loggerRepository;
			}
			return result;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000A91C File Offset: 0x00008B1C
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
				ILoggerRepository loggerRepository2 = this.m_alias2repositoryMap[repositoryName] as ILoggerRepository;
				if (loggerRepository2 != null)
				{
					if (loggerRepository2.GetType() == repositoryType)
					{
						LogLog.Debug(DefaultRepositorySelector.declaringType, string.Concat(new string[]
						{
							"Aliasing repository [",
							repositoryName,
							"] to existing repository [",
							loggerRepository2.Name,
							"]"
						}));
						loggerRepository = loggerRepository2;
						this.m_name2repositoryMap[repositoryName] = loggerRepository;
					}
					else
					{
						LogLog.Error(DefaultRepositorySelector.declaringType, string.Concat(new string[]
						{
							"Failed to alias repository [",
							repositoryName,
							"] to existing repository [",
							loggerRepository2.Name,
							"]. Requested repository type [",
							repositoryType.FullName,
							"] is not compatible with existing type [",
							loggerRepository2.GetType().FullName,
							"]"
						}));
					}
				}
				if (loggerRepository == null)
				{
					LogLog.Debug(DefaultRepositorySelector.declaringType, string.Concat(new object[]
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
				}
				result = loggerRepository;
			}
			return result;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000AB00 File Offset: 0x00008D00
		public bool ExistsRepository(string repositoryName)
		{
			bool result;
			lock (this)
			{
				result = this.m_name2repositoryMap.ContainsKey(repositoryName);
			}
			return result;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000AB44 File Offset: 0x00008D44
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

		// Token: 0x060002FD RID: 765 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public void AliasRepository(string repositoryAlias, ILoggerRepository repositoryTarget)
		{
			if (repositoryAlias == null)
			{
				throw new ArgumentNullException("repositoryAlias");
			}
			if (repositoryTarget == null)
			{
				throw new ArgumentNullException("repositoryTarget");
			}
			lock (this)
			{
				if (this.m_alias2repositoryMap.Contains(repositoryAlias))
				{
					if (repositoryTarget != (ILoggerRepository)this.m_alias2repositoryMap[repositoryAlias])
					{
						throw new InvalidOperationException(string.Concat(new string[]
						{
							"Repository [",
							repositoryAlias,
							"] is already aliased to repository [",
							((ILoggerRepository)this.m_alias2repositoryMap[repositoryAlias]).Name,
							"]. Aliases cannot be redefined."
						}));
					}
				}
				else if (this.m_name2repositoryMap.Contains(repositoryAlias))
				{
					if (repositoryTarget != (ILoggerRepository)this.m_name2repositoryMap[repositoryAlias])
					{
						throw new InvalidOperationException(string.Concat(new string[]
						{
							"Repository [",
							repositoryAlias,
							"] already exists and cannot be aliased to repository [",
							repositoryTarget.Name,
							"]."
						}));
					}
				}
				else
				{
					this.m_alias2repositoryMap[repositoryAlias] = repositoryTarget;
				}
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000ACC8 File Offset: 0x00008EC8
		protected virtual void OnLoggerRepositoryCreatedEvent(ILoggerRepository repository)
		{
			LoggerRepositoryCreationEventHandler loggerRepositoryCreatedEvent = this.m_loggerRepositoryCreatedEvent;
			if (loggerRepositoryCreatedEvent != null)
			{
				loggerRepositoryCreatedEvent(this, new LoggerRepositoryCreationEventArgs(repository));
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000ACEC File Offset: 0x00008EEC
		private void GetInfoForAssembly(Assembly assembly, ref string repositoryName, ref Type repositoryType)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			try
			{
				LogLog.Debug(DefaultRepositorySelector.declaringType, string.Concat(new string[]
				{
					"Assembly [",
					assembly.FullName,
					"] Loaded From [",
					SystemInfo.AssemblyLocationInfo(assembly),
					"]"
				}));
			}
			catch
			{
			}
			try
			{
				object[] customAttributes = Attribute.GetCustomAttributes(assembly, typeof(RepositoryAttribute), false);
				if (customAttributes == null || customAttributes.Length == 0)
				{
					LogLog.Debug(DefaultRepositorySelector.declaringType, "Assembly [" + assembly + "] does not have a RepositoryAttribute specified.");
				}
				else
				{
					if (customAttributes.Length > 1)
					{
						LogLog.Error(DefaultRepositorySelector.declaringType, "Assembly [" + assembly + "] has multiple log4net.Config.RepositoryAttribute assembly attributes. Only using first occurrence.");
					}
					RepositoryAttribute repositoryAttribute = customAttributes[0] as RepositoryAttribute;
					if (repositoryAttribute == null)
					{
						LogLog.Error(DefaultRepositorySelector.declaringType, "Assembly [" + assembly + "] has a RepositoryAttribute but it does not!.");
					}
					else
					{
						if (repositoryAttribute.Name != null)
						{
							repositoryName = repositoryAttribute.Name;
						}
						if (repositoryAttribute.RepositoryType != null)
						{
							if (typeof(ILoggerRepository).IsAssignableFrom(repositoryAttribute.RepositoryType))
							{
								repositoryType = repositoryAttribute.RepositoryType;
							}
							else
							{
								LogLog.Error(DefaultRepositorySelector.declaringType, "DefaultRepositorySelector: Repository Type [" + repositoryAttribute.RepositoryType + "] must implement the ILoggerRepository interface.");
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(DefaultRepositorySelector.declaringType, "Unhandled exception in GetInfoForAssembly", exception);
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000AE64 File Offset: 0x00009064
		private void ConfigureRepository(Assembly assembly, ILoggerRepository repository)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			object[] customAttributes = Attribute.GetCustomAttributes(assembly, typeof(ConfiguratorAttribute), false);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				Array.Sort<object>(customAttributes);
				foreach (ConfiguratorAttribute configuratorAttribute in customAttributes)
				{
					if (configuratorAttribute != null)
					{
						try
						{
							configuratorAttribute.Configure(assembly, repository);
						}
						catch (Exception exception)
						{
							LogLog.Error(DefaultRepositorySelector.declaringType, "Exception calling [" + configuratorAttribute.GetType().FullName + "] .Configure method.", exception);
						}
					}
				}
			}
			if (repository.Name == "log4net-default-repository")
			{
				string appSetting = SystemInfo.GetAppSetting("log4net.Config");
				if (appSetting != null && appSetting.Length > 0)
				{
					string text = null;
					try
					{
						text = SystemInfo.ApplicationBaseDirectory;
					}
					catch (Exception exception2)
					{
						LogLog.Warn(DefaultRepositorySelector.declaringType, "Exception getting ApplicationBaseDirectory. appSettings log4net.Config path [" + appSetting + "] will be treated as an absolute URI", exception2);
					}
					string text2 = appSetting;
					if (text != null)
					{
						text2 = Path.Combine(text, appSetting);
					}
					bool flag = false;
					bool.TryParse(SystemInfo.GetAppSetting("log4net.Config.Watch"), out flag);
					if (flag)
					{
						FileInfo configFile = null;
						try
						{
							configFile = new FileInfo(text2);
						}
						catch (Exception exception3)
						{
							LogLog.Error(DefaultRepositorySelector.declaringType, "DefaultRepositorySelector: Exception while parsing log4net.Config file physical path [" + text2 + "]", exception3);
						}
						try
						{
							LogLog.Debug(DefaultRepositorySelector.declaringType, "Loading and watching configuration for default repository from AppSettings specified Config path [" + text2 + "]");
							XmlConfigurator.ConfigureAndWatch(repository, configFile);
							return;
						}
						catch (Exception exception4)
						{
							LogLog.Error(DefaultRepositorySelector.declaringType, "DefaultRepositorySelector: Exception calling XmlConfigurator.ConfigureAndWatch method with ConfigFilePath [" + text2 + "]", exception4);
							return;
						}
					}
					Uri uri = null;
					try
					{
						uri = new Uri(text2);
					}
					catch (Exception exception5)
					{
						LogLog.Error(DefaultRepositorySelector.declaringType, "Exception while parsing log4net.Config file path [" + appSetting + "]", exception5);
					}
					if (uri != null)
					{
						LogLog.Debug(DefaultRepositorySelector.declaringType, "Loading configuration for default repository from AppSettings specified Config URI [" + uri.ToString() + "]");
						try
						{
							XmlConfigurator.Configure(repository, uri);
						}
						catch (Exception exception6)
						{
							LogLog.Error(DefaultRepositorySelector.declaringType, "Exception calling XmlConfigurator.Configure method with ConfigUri [" + uri + "]", exception6);
						}
					}
				}
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000B0E0 File Offset: 0x000092E0
		private void LoadPlugins(Assembly assembly, ILoggerRepository repository)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			object[] customAttributes = Attribute.GetCustomAttributes(assembly, typeof(PluginAttribute), false);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				foreach (IPluginFactory pluginFactory in customAttributes)
				{
					try
					{
						repository.PluginMap.Add(pluginFactory.CreatePlugin());
					}
					catch (Exception exception)
					{
						LogLog.Error(DefaultRepositorySelector.declaringType, "Failed to create plugin. Attribute [" + pluginFactory.ToString() + "]", exception);
					}
				}
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000B190 File Offset: 0x00009390
		private void LoadAliases(Assembly assembly, ILoggerRepository repository)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			object[] customAttributes = Attribute.GetCustomAttributes(assembly, typeof(AliasRepositoryAttribute), false);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				foreach (AliasRepositoryAttribute aliasRepositoryAttribute in customAttributes)
				{
					try
					{
						this.AliasRepository(aliasRepositoryAttribute.Name, repository);
					}
					catch (Exception exception)
					{
						LogLog.Error(DefaultRepositorySelector.declaringType, "Failed to alias repository [" + aliasRepositoryAttribute.Name + "]", exception);
					}
				}
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000303 RID: 771 RVA: 0x0000B23C File Offset: 0x0000943C
		// (remove) Token: 0x06000304 RID: 772 RVA: 0x0000B274 File Offset: 0x00009474
		private event LoggerRepositoryCreationEventHandler m_loggerRepositoryCreatedEvent;

		// Token: 0x0400015D RID: 349
		private const string DefaultRepositoryName = "log4net-default-repository";

		// Token: 0x0400015E RID: 350
		private static readonly Type declaringType = typeof(DefaultRepositorySelector);

		// Token: 0x0400015F RID: 351
		private readonly Hashtable m_name2repositoryMap = new Hashtable();

		// Token: 0x04000160 RID: 352
		private readonly Hashtable m_assembly2repositoryMap = new Hashtable();

		// Token: 0x04000161 RID: 353
		private readonly Hashtable m_alias2repositoryMap = new Hashtable();

		// Token: 0x04000162 RID: 354
		private readonly Type m_defaultRepositoryType;
	}
}
