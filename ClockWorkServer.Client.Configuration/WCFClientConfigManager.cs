using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using TechnoPro.Common.ClientManager.Core.ClockWorkServerConnection;
using TechnoPro.Common.ClientManager.ICore.ClockWorkServerConnection;
using TechnoPro.Common.Core.ClockWorkServerConnection;
using TechnoPro.Common.ICore.ClockWorkServerConnection;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Win32;

namespace TechnoPro.ClockWorkServer.Client.Configuration
{
	// Token: 0x02000004 RID: 4
	public class WCFClientConfigManager
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002350 File Offset: 0x00000550
		public string App_DataPath
		{
			get
			{
				string name = Assembly.GetEntryAssembly().GetName().Name;
				string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "TechnoPro");
				return Path.Combine(path, name);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000238B File Offset: 0x0000058B
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002393 File Offset: 0x00000593
		public WCFClientConfig ClientConfig { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000239C File Offset: 0x0000059C
		public static WCFClientConfigManager CurrentInstance
		{
			get
			{
				bool flag = WCFClientConfigManager._instance == null;
				if (flag)
				{
					WCFClientConfigManager._instance = new WCFClientConfigManager();
				}
				return WCFClientConfigManager._instance;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023C9 File Offset: 0x000005C9
		protected WCFClientConfigManager()
		{
			this.ClientConfig = new WCFClientConfig();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023E0 File Offset: 0x000005E0
		public void OpenConfig(string appPath, eClockWorkServerInstanceName serverInstanceName, string serverVirtualDirectory, string sVersion)
		{
			IClockWorkServerConnectionInfoManager clockWorkServerConnectionInfoManager = new ClockWorkServerConnectionInfoManager(new ClockWorkServerOperationContext
			{
				WhoAmI = 0,
				ClockWorkServerInstanceName = serverInstanceName,
				ClockWorkServerVirtualDirectory = serverVirtualDirectory
			});
			ClockWorkServerConnectionInfo clockWorkServerConnectionInfo = clockWorkServerConnectionInfoManager.GetClockWorkServerConnectionInfo();
			bool flag = clockWorkServerConnectionInfo != null && !string.IsNullOrEmpty(clockWorkServerConnectionInfo.TcpHostname) && !string.IsNullOrEmpty(clockWorkServerConnectionInfo.HttpHostname);
			if (flag)
			{
				this.ClientConfig.Settings["internalhostname"] = clockWorkServerConnectionInfo.TcpHostname;
				this.ClientConfig.Settings["externalhostname"] = clockWorkServerConnectionInfo.HttpHostname;
				this.ClientConfig.Settings["internalhostnameport"] = ((clockWorkServerConnectionInfo.TcpPort > 0) ? clockWorkServerConnectionInfo.TcpPort.ToString() : "808");
				this.ClientConfig.Settings["externalhostnameport"] = ((clockWorkServerConnectionInfo.HttpPort > 0) ? clockWorkServerConnectionInfo.HttpPort.ToString() : "80");
				this.ClientConfig.Settings["virtualdirectoryname"] = serverVirtualDirectory;
				this.ClientConfig.Settings["identitydns"] = clockWorkServerConnectionInfo.IdentityDNS;
				this.ClientConfig.Settings["behaviorconfiguration"] = "Certificate.Behavior";
				this.ClientConfig.Settings["bindingconfiguration"] = "Certificate.Binding";
				bool flag2 = !this.ClientConfig.Settings.ContainsKey("certificate");
				if (flag2)
				{
					this.ClientConfig.Settings["certificate"] = clockWorkServerConnectionInfo.Certificate.CertificatePublicKey;
				}
				bool flag3 = !this.ClientConfig.Settings.ContainsKey("certificatesubjectname");
				if (flag3)
				{
					this.ClientConfig.Settings["certificatesubjectname"] = clockWorkServerConnectionInfo.Certificate.SubjectName;
				}
				bool flag4 = !this.ClientConfig.Settings.ContainsKey("certificatethumbprint");
				if (flag4)
				{
					this.ClientConfig.Settings["certificatesubjectname"] = (clockWorkServerConnectionInfo.Certificate.Thumbprint ?? string.Empty);
				}
				this.ClientConfig.HostType = clockWorkServerConnectionInfo.IISVersion;
				this.ClientConfig.Version = sVersion;
			}
			else
			{
				string text = Path.Combine(appPath, "ClockWork2.ini");
				bool flag5 = File.Exists(text);
				if (flag5)
				{
					string storageString = File.ReadAllText(text);
					IClockWorkClientConnectionInfoClientManager clockWorkClientConnectionInfoClientManager = new ClockWorkClientConnectionInfoClientManager();
					ClockWorkClientConnectionInfo connectionInfoFromStorageString = clockWorkClientConnectionInfoClientManager.GetConnectionInfoFromStorageString(storageString);
					this.ClientConfig.Settings["internalhostname"] = connectionInfoFromStorageString.ServerPreferredConnection.Hostname;
					this.ClientConfig.Settings["externalhostname"] = connectionInfoFromStorageString.ServerPreferredConnection.ExternalHostname;
					this.ClientConfig.Settings["internalhostnameport"] = ((connectionInfoFromStorageString.ServerPreferredConnection.Port > 0) ? connectionInfoFromStorageString.ServerPreferredConnection.Port.ToString() : "808");
					this.ClientConfig.Settings["externalhostnameport"] = ((connectionInfoFromStorageString.ServerPreferredConnection.ExternalPort > 0) ? connectionInfoFromStorageString.ServerPreferredConnection.ExternalPort.ToString() : "80");
					this.ClientConfig.Settings["virtualdirectoryname"] = connectionInfoFromStorageString.ServerPreferredConnection.VirtualDirectory;
					this.ClientConfig.Settings["identitydns"] = connectionInfoFromStorageString.ServerPreferredConnection.IdentityDNS;
					this.ClientConfig.Settings["behaviorconfiguration"] = "Certificate.Behavior";
					this.ClientConfig.Settings["bindingconfiguration"] = "Certificate.Binding";
					bool flag6 = !this.ClientConfig.Settings.ContainsKey("certificate");
					if (flag6)
					{
						this.ClientConfig.Settings["certificate"] = connectionInfoFromStorageString.ServerPreferredConnection.Certificate.CertificatePublicKey;
					}
					bool flag7 = !this.ClientConfig.Settings.ContainsKey("certificatesubjectname");
					if (flag7)
					{
						this.ClientConfig.Settings["certificatesubjectname"] = connectionInfoFromStorageString.ServerPreferredConnection.Certificate.SubjectName;
					}
					this.ClientConfig.HostType = connectionInfoFromStorageString.ServerPreferredConnection.IISVersion;
					this.ClientConfig.Version = connectionInfoFromStorageString.Version;
					FileInfo fileInfo = new FileInfo(text);
					this.ClientConfig.ClientConfigModifiedDatetime = fileInfo.LastWriteTime;
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002884 File Offset: 0x00000A84
		public XElement GetWCFConfigElement()
		{
			XName name = "WCF";
			object[] array = new object[2];
			array[0] = new XAttribute("hosttype", this.ClientConfig.HostType);
			array[1] = new XElement("appSettings", this.ClientConfig.Settings.Select(delegate(KeyValuePair<string, string> s)
			{
				XName name2 = "add";
				object[] array2 = new object[2];
				int num = 0;
				XName name3 = "key";
				KeyValuePair<string, string> keyValuePair = s;
				array2[num] = new XAttribute(name3, keyValuePair.Key);
				int num2 = 1;
				XName name4 = "value";
				keyValuePair = s;
				array2[num2] = new XAttribute(name4, keyValuePair.Value);
				return new XElement(name2, array2);
			}));
			return new XElement(name, array);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002910 File Offset: 0x00000B10
		private IDictionary<string, string> readConfigFile(XElement configElement, out InternetInformationServicesVersion hostType, out string version)
		{
			XAttribute xattribute = configElement.Attribute("version");
			version = xattribute.Value;
			XElement xelement = configElement.Element("WCF");
			hostType = (Enum.IsDefined(typeof(InternetInformationServicesVersion), xelement.Attribute("hosttype").Value.ToUpper()) ? ((InternetInformationServicesVersion)Enum.Parse(typeof(InternetInformationServicesVersion), xelement.Attribute("hosttype").Value.ToUpper())) : InternetInformationServicesVersion.IIS7);
			return (from se in xelement.Element("appSettings").Elements("add")
			select new
			{
				Key = se.Attribute("key").Value,
				Value = se.Attribute("value").Value
			}).ToDictionary(e => e.Key, e => e.Value);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A30 File Offset: 0x00000C30
		private void installClientCertificate()
		{
			bool flag = this.ClientConfig.Settings.ContainsKey("certificate");
			if (flag)
			{
				X509Certificate2 certificate = new X509Certificate2(Convert.FromBase64String(this.ClientConfig.Settings["certificate"]));
				certificate.Install(StoreName.TrustedPeople, StoreLocation.LocalMachine);
				certificate.Install(StoreName.TrustedPeople, StoreLocation.CurrentUser);
			}
		}

		// Token: 0x04000008 RID: 8
		protected static WCFClientConfigManager _instance;
	}
}
