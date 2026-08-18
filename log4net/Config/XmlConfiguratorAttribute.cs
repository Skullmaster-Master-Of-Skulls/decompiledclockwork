using System;
using System.Collections;
using System.IO;
using System.Reflection;
using log4net.Repository;
using log4net.Util;

namespace log4net.Config
{
	// Token: 0x0200004F RID: 79
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public class XmlConfiguratorAttribute : ConfiguratorAttribute
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000090A8 File Offset: 0x000072A8
		public XmlConfiguratorAttribute() : base(0)
		{
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000090B1 File Offset: 0x000072B1
		// (set) Token: 0x060002AA RID: 682 RVA: 0x000090B9 File Offset: 0x000072B9
		public string ConfigFile
		{
			get
			{
				return this.m_configFile;
			}
			set
			{
				this.m_configFile = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000090C2 File Offset: 0x000072C2
		// (set) Token: 0x060002AC RID: 684 RVA: 0x000090CA File Offset: 0x000072CA
		public string ConfigFileExtension
		{
			get
			{
				return this.m_configFileExtension;
			}
			set
			{
				this.m_configFileExtension = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000090D3 File Offset: 0x000072D3
		// (set) Token: 0x060002AE RID: 686 RVA: 0x000090DB File Offset: 0x000072DB
		public bool Watch
		{
			get
			{
				return this.m_configureAndWatch;
			}
			set
			{
				this.m_configureAndWatch = value;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000090E4 File Offset: 0x000072E4
		public override void Configure(Assembly sourceAssembly, ILoggerRepository targetRepository)
		{
			IList list = new ArrayList();
			using (new LogLog.LogReceivedAdapter(list))
			{
				string text = null;
				try
				{
					text = SystemInfo.ApplicationBaseDirectory;
				}
				catch
				{
				}
				if (text == null || new Uri(text).IsFile)
				{
					this.ConfigureFromFile(sourceAssembly, targetRepository);
				}
				else
				{
					this.ConfigureFromUri(sourceAssembly, targetRepository);
				}
			}
			targetRepository.ConfigurationMessages = list;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000915C File Offset: 0x0000735C
		private void ConfigureFromFile(Assembly sourceAssembly, ILoggerRepository targetRepository)
		{
			string text = null;
			if (this.m_configFile == null || this.m_configFile.Length == 0)
			{
				if (this.m_configFileExtension != null)
				{
					if (this.m_configFileExtension.Length != 0)
					{
						goto IL_50;
					}
				}
				try
				{
					text = SystemInfo.ConfigurationFileLocation;
					goto IL_FC;
				}
				catch (Exception exception)
				{
					LogLog.Error(XmlConfiguratorAttribute.declaringType, "XmlConfiguratorAttribute: Exception getting ConfigurationFileLocation. Must be able to resolve ConfigurationFileLocation when ConfigFile and ConfigFileExtension properties are not set.", exception);
					goto IL_FC;
				}
				IL_50:
				if (this.m_configFileExtension[0] != '.')
				{
					this.m_configFileExtension = "." + this.m_configFileExtension;
				}
				string text2 = null;
				try
				{
					text2 = SystemInfo.ApplicationBaseDirectory;
				}
				catch (Exception exception2)
				{
					LogLog.Error(XmlConfiguratorAttribute.declaringType, "Exception getting ApplicationBaseDirectory. Must be able to resolve ApplicationBaseDirectory and AssemblyFileName when ConfigFileExtension property is set.", exception2);
				}
				if (text2 != null)
				{
					text = Path.Combine(text2, SystemInfo.AssemblyFileName(sourceAssembly) + this.m_configFileExtension);
				}
			}
			else
			{
				string text3 = null;
				try
				{
					text3 = SystemInfo.ApplicationBaseDirectory;
				}
				catch (Exception exception3)
				{
					LogLog.Warn(XmlConfiguratorAttribute.declaringType, "Exception getting ApplicationBaseDirectory. ConfigFile property path [" + this.m_configFile + "] will be treated as an absolute path.", exception3);
				}
				if (text3 != null)
				{
					text = Path.Combine(text3, this.m_configFile);
				}
				else
				{
					text = this.m_configFile;
				}
			}
			IL_FC:
			if (text != null)
			{
				this.ConfigureFromFile(targetRepository, new FileInfo(text));
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000092A0 File Offset: 0x000074A0
		private void ConfigureFromFile(ILoggerRepository targetRepository, FileInfo configFile)
		{
			if (this.m_configureAndWatch)
			{
				XmlConfigurator.ConfigureAndWatch(targetRepository, configFile);
				return;
			}
			XmlConfigurator.Configure(targetRepository, configFile);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000092BC File Offset: 0x000074BC
		private void ConfigureFromUri(Assembly sourceAssembly, ILoggerRepository targetRepository)
		{
			Uri uri = null;
			if (this.m_configFile == null || this.m_configFile.Length == 0)
			{
				if (this.m_configFileExtension == null || this.m_configFileExtension.Length == 0)
				{
					string text = null;
					try
					{
						text = SystemInfo.ConfigurationFileLocation;
					}
					catch (Exception exception)
					{
						LogLog.Error(XmlConfiguratorAttribute.declaringType, "XmlConfiguratorAttribute: Exception getting ConfigurationFileLocation. Must be able to resolve ConfigurationFileLocation when ConfigFile and ConfigFileExtension properties are not set.", exception);
					}
					if (text != null)
					{
						Uri uri2 = new Uri(text);
						uri = uri2;
					}
				}
				else
				{
					if (this.m_configFileExtension[0] != '.')
					{
						this.m_configFileExtension = "." + this.m_configFileExtension;
					}
					string text2 = null;
					try
					{
						text2 = SystemInfo.ConfigurationFileLocation;
					}
					catch (Exception exception2)
					{
						LogLog.Error(XmlConfiguratorAttribute.declaringType, "XmlConfiguratorAttribute: Exception getting ConfigurationFileLocation. Must be able to resolve ConfigurationFileLocation when the ConfigFile property are not set.", exception2);
					}
					if (text2 != null)
					{
						UriBuilder uriBuilder = new UriBuilder(new Uri(text2));
						string text3 = uriBuilder.Path;
						int num = text3.LastIndexOf(".");
						if (num >= 0)
						{
							text3 = text3.Substring(0, num);
						}
						text3 += this.m_configFileExtension;
						uriBuilder.Path = text3;
						uri = uriBuilder.Uri;
					}
				}
			}
			else
			{
				string text4 = null;
				try
				{
					text4 = SystemInfo.ApplicationBaseDirectory;
				}
				catch (Exception exception3)
				{
					LogLog.Warn(XmlConfiguratorAttribute.declaringType, "Exception getting ApplicationBaseDirectory. ConfigFile property path [" + this.m_configFile + "] will be treated as an absolute URI.", exception3);
				}
				if (text4 != null)
				{
					uri = new Uri(new Uri(text4), this.m_configFile);
				}
				else
				{
					uri = new Uri(this.m_configFile);
				}
			}
			if (uri != null)
			{
				if (uri.IsFile)
				{
					this.ConfigureFromFile(targetRepository, new FileInfo(uri.LocalPath));
					return;
				}
				if (this.m_configureAndWatch)
				{
					LogLog.Warn(XmlConfiguratorAttribute.declaringType, "XmlConfiguratorAttribute: Unable to watch config file loaded from a URI");
				}
				XmlConfigurator.Configure(targetRepository, uri);
			}
		}

		// Token: 0x04000149 RID: 329
		private string m_configFile;

		// Token: 0x0400014A RID: 330
		private string m_configFileExtension;

		// Token: 0x0400014B RID: 331
		private bool m_configureAndWatch;

		// Token: 0x0400014C RID: 332
		private static readonly Type declaringType = typeof(XmlConfiguratorAttribute);
	}
}
