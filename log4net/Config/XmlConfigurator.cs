using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Xml;
using log4net.Repository;
using log4net.Util;

namespace log4net.Config
{
	// Token: 0x02000055 RID: 85
	public sealed class XmlConfigurator
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000966F File Offset: 0x0000786F
		private XmlConfigurator()
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00009678 File Offset: 0x00007878
		public static ICollection Configure(ILoggerRepository repository)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000096BC File Offset: 0x000078BC
		private static void InternalConfigure(ILoggerRepository repository)
		{
			LogLog.Debug(XmlConfigurator.declaringType, "configuring repository [" + repository.Name + "] using .config file section");
			try
			{
				LogLog.Debug(XmlConfigurator.declaringType, "Application config file is [" + SystemInfo.ConfigurationFileLocation + "]");
			}
			catch
			{
				LogLog.Debug(XmlConfigurator.declaringType, "Application config file location unknown");
			}
			try
			{
				XmlElement xmlElement = ConfigurationManager.GetSection("log4net") as XmlElement;
				if (xmlElement == null)
				{
					LogLog.Error(XmlConfigurator.declaringType, "Failed to find configuration section 'log4net' in the application's .config file. Check your .config file for the <log4net> and <configSections> elements. The configuration section should look like: <section name=\"log4net\" type=\"log4net.Config.Log4NetConfigurationSectionHandler,log4net\" />");
				}
				else
				{
					XmlConfigurator.InternalConfigureFromXml(repository, xmlElement);
				}
			}
			catch (ConfigurationException ex)
			{
				if (ex.BareMessage.IndexOf("Unrecognized element") >= 0)
				{
					LogLog.Error(XmlConfigurator.declaringType, "Failed to parse config file. Check your .config file is well formed XML.", ex);
				}
				else
				{
					string str = "<section name=\"log4net\" type=\"log4net.Config.Log4NetConfigurationSectionHandler," + Assembly.GetExecutingAssembly().FullName + "\" />";
					LogLog.Error(XmlConfigurator.declaringType, "Failed to parse config file. Is the <configSections> specified as: " + str, ex);
				}
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000097C0 File Offset: 0x000079C0
		public static ICollection Configure()
		{
			return XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000097D4 File Offset: 0x000079D4
		public static ICollection Configure(XmlElement element)
		{
			ArrayList arrayList = new ArrayList();
			ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigureFromXml(repository, element);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00009824 File Offset: 0x00007A24
		public static ICollection Configure(FileInfo configFile)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(LogManager.GetRepository(Assembly.GetCallingAssembly()), configFile);
			}
			return arrayList;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000986C File Offset: 0x00007A6C
		public static ICollection Configure(Uri configUri)
		{
			ArrayList arrayList = new ArrayList();
			ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository, configUri);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000098BC File Offset: 0x00007ABC
		public static ICollection Configure(Stream configStream)
		{
			ArrayList arrayList = new ArrayList();
			ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository, configStream);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000990C File Offset: 0x00007B0C
		public static ICollection Configure(ILoggerRepository repository, XmlElement element)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				LogLog.Debug(XmlConfigurator.declaringType, "configuring repository [" + repository.Name + "] using XML element");
				XmlConfigurator.InternalConfigureFromXml(repository, element);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00009970 File Offset: 0x00007B70
		public static ICollection Configure(ILoggerRepository repository, FileInfo configFile)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository, configFile);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000099B8 File Offset: 0x00007BB8
		private static void InternalConfigure(ILoggerRepository repository, FileInfo configFile)
		{
			LogLog.Debug(XmlConfigurator.declaringType, string.Concat(new object[]
			{
				"configuring repository [",
				repository.Name,
				"] using file [",
				configFile,
				"]"
			}));
			if (configFile == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Configure called with null 'configFile' parameter");
				return;
			}
			if (File.Exists(configFile.FullName))
			{
				FileStream fileStream = null;
				int num = 5;
				while (--num >= 0)
				{
					try
					{
						fileStream = configFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
						break;
					}
					catch (IOException exception)
					{
						if (num == 0)
						{
							LogLog.Error(XmlConfigurator.declaringType, "Failed to open XML config file [" + configFile.Name + "]", exception);
							fileStream = null;
						}
						Thread.Sleep(250);
					}
				}
				if (fileStream == null)
				{
					return;
				}
				try
				{
					XmlConfigurator.InternalConfigure(repository, fileStream);
					return;
				}
				finally
				{
					fileStream.Close();
				}
			}
			LogLog.Debug(XmlConfigurator.declaringType, "config file [" + configFile.FullName + "] not found. Configuration unchanged.");
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00009ABC File Offset: 0x00007CBC
		public static ICollection Configure(ILoggerRepository repository, Uri configUri)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository, configUri);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00009B04 File Offset: 0x00007D04
		private static void InternalConfigure(ILoggerRepository repository, Uri configUri)
		{
			LogLog.Debug(XmlConfigurator.declaringType, string.Concat(new object[]
			{
				"configuring repository [",
				repository.Name,
				"] using URI [",
				configUri,
				"]"
			}));
			if (configUri == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Configure called with null 'configUri' parameter");
				return;
			}
			if (configUri.IsFile)
			{
				XmlConfigurator.InternalConfigure(repository, new FileInfo(configUri.LocalPath));
				return;
			}
			WebRequest webRequest = null;
			try
			{
				webRequest = WebRequest.Create(configUri);
			}
			catch (Exception exception)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Failed to create WebRequest for URI [" + configUri + "]", exception);
			}
			if (webRequest != null)
			{
				try
				{
					webRequest.Credentials = CredentialCache.DefaultCredentials;
				}
				catch
				{
				}
				try
				{
					WebResponse response = webRequest.GetResponse();
					if (response != null)
					{
						try
						{
							using (Stream responseStream = response.GetResponseStream())
							{
								XmlConfigurator.InternalConfigure(repository, responseStream);
							}
						}
						finally
						{
							response.Close();
						}
					}
				}
				catch (Exception exception2)
				{
					LogLog.Error(XmlConfigurator.declaringType, "Failed to request config from URI [" + configUri + "]", exception2);
				}
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00009C54 File Offset: 0x00007E54
		public static ICollection Configure(ILoggerRepository repository, Stream configStream)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository, configStream);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00009C9C File Offset: 0x00007E9C
		private static void InternalConfigure(ILoggerRepository repository, Stream configStream)
		{
			LogLog.Debug(XmlConfigurator.declaringType, "configuring repository [" + repository.Name + "] using stream");
			if (configStream == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Configure called with null 'configStream' parameter");
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				XmlReader reader = XmlReader.Create(configStream, new XmlReaderSettings
				{
					DtdProcessing = DtdProcessing.Parse
				});
				xmlDocument.Load(reader);
			}
			catch (Exception exception)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Error while loading XML configuration", exception);
				xmlDocument = null;
			}
			if (xmlDocument != null)
			{
				LogLog.Debug(XmlConfigurator.declaringType, "loading XML configuration");
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("log4net");
				if (elementsByTagName.Count == 0)
				{
					LogLog.Debug(XmlConfigurator.declaringType, "XML configuration does not contain a <log4net> element. Configuration Aborted.");
					return;
				}
				if (elementsByTagName.Count > 1)
				{
					LogLog.Error(XmlConfigurator.declaringType, "XML configuration contains [" + elementsByTagName.Count + "] <log4net> elements. Only one is allowed. Configuration Aborted.");
					return;
				}
				XmlConfigurator.InternalConfigureFromXml(repository, elementsByTagName[0] as XmlElement);
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00009DA0 File Offset: 0x00007FA0
		public static ICollection ConfigureAndWatch(FileInfo configFile)
		{
			ArrayList arrayList = new ArrayList();
			ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigureAndWatch(repository, configFile);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00009DF0 File Offset: 0x00007FF0
		public static ICollection ConfigureAndWatch(ILoggerRepository repository, FileInfo configFile)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigureAndWatch(repository, configFile);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00009E38 File Offset: 0x00008038
		private static void InternalConfigureAndWatch(ILoggerRepository repository, FileInfo configFile)
		{
			LogLog.Debug(XmlConfigurator.declaringType, string.Concat(new object[]
			{
				"configuring repository [",
				repository.Name,
				"] using file [",
				configFile,
				"] watching for file updates"
			}));
			if (configFile == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "ConfigureAndWatch called with null 'configFile' parameter");
				return;
			}
			XmlConfigurator.InternalConfigure(repository, configFile);
			try
			{
				lock (XmlConfigurator.m_repositoryName2ConfigAndWatchHandler)
				{
					XmlConfigurator.ConfigureAndWatchHandler configureAndWatchHandler = (XmlConfigurator.ConfigureAndWatchHandler)XmlConfigurator.m_repositoryName2ConfigAndWatchHandler[configFile.FullName];
					if (configureAndWatchHandler != null)
					{
						XmlConfigurator.m_repositoryName2ConfigAndWatchHandler.Remove(configFile.FullName);
						configureAndWatchHandler.Dispose();
					}
					configureAndWatchHandler = new XmlConfigurator.ConfigureAndWatchHandler(repository, configFile);
					XmlConfigurator.m_repositoryName2ConfigAndWatchHandler[configFile.FullName] = configureAndWatchHandler;
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Failed to initialize configuration file watcher for file [" + configFile.FullName + "]", exception);
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00009F44 File Offset: 0x00008144
		private static void InternalConfigureFromXml(ILoggerRepository repository, XmlElement element)
		{
			if (element == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "ConfigureFromXml called with null 'element' parameter");
				return;
			}
			if (repository == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "ConfigureFromXml called with null 'repository' parameter");
				return;
			}
			LogLog.Debug(XmlConfigurator.declaringType, "Configuring Repository [" + repository.Name + "]");
			IXmlRepositoryConfigurator xmlRepositoryConfigurator = repository as IXmlRepositoryConfigurator;
			if (xmlRepositoryConfigurator == null)
			{
				LogLog.Warn(XmlConfigurator.declaringType, "Repository [" + repository + "] does not support the XmlConfigurator");
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement element2 = (XmlElement)xmlDocument.AppendChild(xmlDocument.ImportNode(element, true));
			xmlRepositoryConfigurator.Configure(element2);
		}

		// Token: 0x04000151 RID: 337
		private static readonly Hashtable m_repositoryName2ConfigAndWatchHandler = new Hashtable();

		// Token: 0x04000152 RID: 338
		private static readonly Type declaringType = typeof(XmlConfigurator);

		// Token: 0x02000056 RID: 86
		private sealed class ConfigureAndWatchHandler : IDisposable
		{
			// Token: 0x060002D9 RID: 729 RVA: 0x00009FF8 File Offset: 0x000081F8
			[SecuritySafeCritical]
			public ConfigureAndWatchHandler(ILoggerRepository repository, FileInfo configFile)
			{
				this.m_repository = repository;
				this.m_configFile = configFile;
				this.m_watcher = new FileSystemWatcher();
				this.m_watcher.Path = this.m_configFile.DirectoryName;
				this.m_watcher.Filter = this.m_configFile.Name;
				this.m_watcher.NotifyFilter = (NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime);
				this.m_watcher.Changed += this.ConfigureAndWatchHandler_OnChanged;
				this.m_watcher.Created += this.ConfigureAndWatchHandler_OnChanged;
				this.m_watcher.Deleted += this.ConfigureAndWatchHandler_OnChanged;
				this.m_watcher.Renamed += this.ConfigureAndWatchHandler_OnRenamed;
				this.m_watcher.EnableRaisingEvents = true;
				this.m_timer = new Timer(new TimerCallback(this.OnWatchedFileChange), null, -1, -1);
			}

			// Token: 0x060002DA RID: 730 RVA: 0x0000A0E0 File Offset: 0x000082E0
			private void ConfigureAndWatchHandler_OnChanged(object source, FileSystemEventArgs e)
			{
				LogLog.Debug(XmlConfigurator.declaringType, string.Concat(new object[]
				{
					"ConfigureAndWatchHandler: ",
					e.ChangeType,
					" [",
					this.m_configFile.FullName,
					"]"
				}));
				this.m_timer.Change(500, -1);
			}

			// Token: 0x060002DB RID: 731 RVA: 0x0000A14C File Offset: 0x0000834C
			private void ConfigureAndWatchHandler_OnRenamed(object source, RenamedEventArgs e)
			{
				LogLog.Debug(XmlConfigurator.declaringType, string.Concat(new object[]
				{
					"ConfigureAndWatchHandler: ",
					e.ChangeType,
					" [",
					this.m_configFile.FullName,
					"]"
				}));
				this.m_timer.Change(500, -1);
			}

			// Token: 0x060002DC RID: 732 RVA: 0x0000A1B6 File Offset: 0x000083B6
			private void OnWatchedFileChange(object state)
			{
				XmlConfigurator.InternalConfigure(this.m_repository, this.m_configFile);
			}

			// Token: 0x060002DD RID: 733 RVA: 0x0000A1C9 File Offset: 0x000083C9
			[SecuritySafeCritical]
			public void Dispose()
			{
				this.m_watcher.EnableRaisingEvents = false;
				this.m_watcher.Dispose();
				this.m_timer.Dispose();
			}

			// Token: 0x04000153 RID: 339
			private const int TimeoutMillis = 500;

			// Token: 0x04000154 RID: 340
			private FileInfo m_configFile;

			// Token: 0x04000155 RID: 341
			private ILoggerRepository m_repository;

			// Token: 0x04000156 RID: 342
			private Timer m_timer;

			// Token: 0x04000157 RID: 343
			private FileSystemWatcher m_watcher;
		}
	}
}
