using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EncryptionClassLibrary;
using TechnoPro.Common.ClientManager.Core.Adapters;
using TechnoPro.Common.ClientManager.ICore.ClockWorkServerConnection;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Public.Entities.Database;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.ClientManager.Core.ClockWorkServerConnection
{
	// Token: 0x02000075 RID: 117
	public class ClockWorkClientConnectionInfoClientManager : IClockWorkClientConnectionInfoClientManager, IWebService
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0001322C File Offset: 0x0001142C
		private int ParsePort(string xmlValue)
		{
			int num;
			bool flag = int.TryParse(xmlValue, out num);
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				result = 808;
			}
			return result;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00013254 File Offset: 0x00011454
		private int ParseExternalPort(string xmlValue)
		{
			int num;
			bool flag = int.TryParse(xmlValue, out num);
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				result = 80;
			}
			return result;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00013278 File Offset: 0x00011478
		private InternetInformationServicesVersion ParseIISVersion(string xmlValue)
		{
			bool flag = Enum.IsDefined(typeof(InternetInformationServicesVersion), xmlValue);
			InternetInformationServicesVersion result;
			if (flag)
			{
				result = (InternetInformationServicesVersion)Enum.Parse(typeof(InternetInformationServicesVersion), xmlValue);
			}
			else
			{
				result = InternetInformationServicesVersion.IIS7;
			}
			return result;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000132B8 File Offset: 0x000114B8
		private eBindingType ParseBinding(string xmlValue)
		{
			bool flag = Enum.IsDefined(typeof(eBindingType), xmlValue);
			eBindingType result;
			if (flag)
			{
				result = (eBindingType)Enum.Parse(typeof(eBindingType), xmlValue);
			}
			else
			{
				result = eBindingType.Unspecified;
			}
			return result;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000132F8 File Offset: 0x000114F8
		private CertificateInfo ParseCertInfo(XElement xmlElement)
		{
			bool flag = xmlElement == null;
			CertificateInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XElement xelement = xmlElement.Element("CertSubjectName");
				XElement xelement2 = xmlElement.Element("CertPublicKey");
				string subjectNameXmlValue = (xelement == null) ? "" : (xelement.Value ?? "");
				string publicKeyXmlValue = (xelement2 == null) ? "" : (xelement2.Value ?? "");
				result = this.ParseCertInfo(publicKeyXmlValue, subjectNameXmlValue);
			}
			return result;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001337C File Offset: 0x0001157C
		private CertificateInfo ParseCertInfo(string publicKeyXmlValue, string subjectNameXmlValue)
		{
			bool flag = string.IsNullOrEmpty(publicKeyXmlValue) && string.IsNullOrEmpty(subjectNameXmlValue);
			CertificateInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new CertificateInfo
				{
					SubjectName = (subjectNameXmlValue ?? ""),
					CertificatePublicKey = (publicKeyXmlValue ?? "")
				};
			}
			return result;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000133D0 File Offset: 0x000115D0
		private string ParseOldStorageString(string StorageString)
		{
			string content = "";
			string content2 = "";
			using (StringReader stringReader = new StringReader(StorageString))
			{
				int num = 0;
				string text;
				while ((text = stringReader.ReadLine()) != null)
				{
					string text2 = (text.Trim().Length < 1) ? "" : this.StaticEncryption.Decrypt(text);
					bool flag = text2.Length > 0;
					if (flag)
					{
						int num2 = num;
						int num3 = num2;
						if (num3 != 0)
						{
							if (num3 == 1)
							{
								content2 = text2;
							}
						}
						else
						{
							content = text2;
						}
						num++;
					}
				}
			}
			XElement xelement = new XElement("DatabaseConnection", new object[]
			{
				new XElement("ConnectionString", content),
				new XElement("DatabasePassword", content2)
			});
			return string.Format("<?xml version=\"1.0\"?>{0}", xelement.ToString());
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000134D8 File Offset: 0x000116D8
		private string ParseNewStorageString(string StorageString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringReader stringReader = new StringReader(StorageString))
			{
				string text;
				while ((text = stringReader.ReadLine()) != null)
				{
					string text2 = (text.Trim().Length < 1) ? "" : this.StaticEncryption.Decrypt(text);
					bool flag = text2.StartsWith("<?xml");
					if (flag)
					{
						return text2;
					}
				}
			}
			return "";
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001356C File Offset: 0x0001176C
		private string StoreXmlInStorageString(string Xml, string ConnectionString, string DbPassword)
		{
			bool flag = !string.IsNullOrEmpty(Xml) && !Xml.StartsWith("<?xml");
			if (flag)
			{
				Xml = string.Format("<?xml version=\"1.0\"?>{0}", Xml);
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag2 = !string.IsNullOrEmpty(ConnectionString) && ConnectionString.IndexOf("Provider=", StringComparison.OrdinalIgnoreCase) < 0;
			if (flag2)
			{
				ConnectionString = "Provider=SQLOLEDB;" + ConnectionString;
			}
			stringBuilder.AppendLine(string.IsNullOrEmpty(ConnectionString) ? "" : Convert.ToBase64String(this.StaticEncryption.Encrypt(ConnectionString)));
			stringBuilder.AppendLine(string.IsNullOrEmpty(DbPassword) ? "" : Convert.ToBase64String(this.StaticEncryption.Encrypt(DbPassword)));
			stringBuilder.AppendLine(string.IsNullOrEmpty(Xml) ? "" : Convert.ToBase64String(this.StaticEncryption.Encrypt(Xml)));
			return stringBuilder.ToString();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00013658 File Offset: 0x00011858
		private string GetXmlFromStorageString(string StorageString)
		{
			bool flag = false;
			using (StringReader stringReader = new StringReader(StorageString))
			{
				string text;
				while ((text = stringReader.ReadLine()) != null)
				{
					string text2 = (text.Trim().Length < 1) ? "" : this.StaticEncryption.Decrypt(text);
					bool flag2 = text2.StartsWith("<?xml");
					if (flag2)
					{
						flag = true;
						break;
					}
				}
			}
			bool flag3 = flag;
			string result;
			if (flag3)
			{
				result = this.ParseNewStorageString(StorageString);
			}
			else
			{
				result = this.ParseOldStorageString(StorageString);
			}
			return result;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000136FC File Offset: 0x000118FC
		public ClockWorkClientConnectionInfo GetConnectionInfoFromStorageString(string StorageString)
		{
			string xmlFromStorageString = this.GetXmlFromStorageString(StorageString);
			bool flag = string.IsNullOrEmpty(xmlFromStorageString);
			ClockWorkClientConnectionInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XDocument xdocument = XDocument.Parse(xmlFromStorageString);
				IEnumerable<DbConnectionInfo> source = from item in xdocument.Descendants("DatabaseConnection")
				select new DbConnectionInfo
				{
					ConnectionString = item.Element("ConnectionString").Value.ParseConnectionString(),
					DbEncryptionPassword = item.Element("DatabasePassword").Value,
					NoDirectDbAccess = (item.Element("NoDirectDbAccess") != null && Convert.ToBoolean(item.Element("NoDirectDbAccess").Value ?? "false"))
				};
				IEnumerable<ClockWorkServerPreferredConnectionInfo> source2 = from item in xdocument.Descendants("ServerConnection")
				select new ClockWorkServerPreferredConnectionInfo
				{
					Hostname = (item.Element("HostName").Value ?? ""),
					Port = this.ParsePort(item.Element("Port").Value),
					VirtualDirectory = (item.Element("VirtualDirectory").Value ?? ""),
					IdentityDNS = (item.Element("IdentityDNS").Value ?? ""),
					IISVersion = this.ParseIISVersion(item.Element("IISVersion").Value),
					BindingType = this.ParseBinding(item.Element("BindingType").Value),
					Certificate = this.ParseCertInfo(item.Element("Certificate")),
					ExternalHostname = ((item.Element("ExternalHostName") != null && item.Element("ExternalHostName").Value != null) ? item.Element("ExternalHostName").Value : string.Empty),
					ExternalPort = this.ParseExternalPort((item.Element("ExternalPort") != null && item.Element("ExternalPort").Value != null) ? item.Element("ExternalPort").Value : string.Empty)
				};
				XElement xelement = xdocument.Element("ConnectionInfo");
				bool flag2 = xelement == null;
				if (flag2)
				{
					List<DbConnectionInfo> list = source.ToList<DbConnectionInfo>();
					bool flag3 = list.Count > 0;
					if (flag3)
					{
						result = new ClockWorkClientConnectionInfo
						{
							Version = "",
							DatabaseConnection = list[0],
							ServerPreferredConnection = new ClockWorkServerPreferredConnectionInfo()
						};
					}
					else
					{
						result = new ClockWorkClientConnectionInfo
						{
							Version = "",
							DatabaseConnection = new DbConnectionInfo(),
							ServerPreferredConnection = new ClockWorkServerPreferredConnectionInfo()
						};
					}
				}
				else
				{
					XAttribute xattribute = xdocument.Element("ConnectionInfo").Attributes("Version").FirstOrDefault<XAttribute>();
					List<ClockWorkServerPreferredConnectionInfo> list2 = source2.ToList<ClockWorkServerPreferredConnectionInfo>();
					List<DbConnectionInfo> list3 = source.ToList<DbConnectionInfo>();
					result = new ClockWorkClientConnectionInfo
					{
						Version = ((xattribute != null) ? xattribute.Value : string.Empty),
						DatabaseConnection = ((list3.Count > 0) ? list3[0] : null),
						ServerPreferredConnection = ((list2.Count > 0) ? list2[0] : null)
					};
				}
			}
			return result;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000138B0 File Offset: 0x00011AB0
		public string GetStorageStringFromConnectionInfo(ClockWorkClientConnectionInfo ConnectionInfo)
		{
			string text = (ConnectionInfo.DatabaseConnection == null) ? "" : (ConnectionInfo.DatabaseConnection.ConnectionString ?? "");
			string text2 = (ConnectionInfo.DatabaseConnection == null) ? "" : (ConnectionInfo.DatabaseConnection.DbEncryptionPassword ?? "");
			bool flag = ConnectionInfo.DatabaseConnection != null && ConnectionInfo.DatabaseConnection.NoDirectDbAccess;
			string text3 = (ConnectionInfo.ServerPreferredConnection == null) ? "" : (ConnectionInfo.ServerPreferredConnection.Hostname ?? "");
			string text4 = (ConnectionInfo.ServerPreferredConnection == null) ? "" : ConnectionInfo.ServerPreferredConnection.Port.ToString();
			string text5 = (ConnectionInfo.ServerPreferredConnection == null) ? "" : (ConnectionInfo.ServerPreferredConnection.ExternalHostname ?? "");
			string text6 = (ConnectionInfo.ServerPreferredConnection == null) ? "" : ConnectionInfo.ServerPreferredConnection.ExternalPort.ToString();
			string text7 = (ConnectionInfo.ServerPreferredConnection == null) ? "" : (ConnectionInfo.ServerPreferredConnection.VirtualDirectory ?? "");
			string text8 = (ConnectionInfo.ServerPreferredConnection == null) ? "" : (ConnectionInfo.ServerPreferredConnection.IdentityDNS ?? "");
			string text9 = (ConnectionInfo.ServerPreferredConnection == null) ? "" : ConnectionInfo.ServerPreferredConnection.IISVersion.ToString();
			string text10 = (ConnectionInfo.ServerPreferredConnection == null || ConnectionInfo.ServerPreferredConnection.Certificate == null) ? "" : (ConnectionInfo.ServerPreferredConnection.Certificate.CertificatePublicKey ?? "");
			string text11 = (ConnectionInfo.ServerPreferredConnection == null || ConnectionInfo.ServerPreferredConnection.Certificate == null) ? "" : (ConnectionInfo.ServerPreferredConnection.Certificate.SubjectName ?? "");
			string text12 = (ConnectionInfo.ServerPreferredConnection == null) ? "" : ConnectionInfo.ServerPreferredConnection.BindingType.ToString();
			XElement xelement = new XElement("ConnectionInfo", new object[]
			{
				new XAttribute("Version", ConnectionInfo.Version ?? ""),
				new XElement("DatabaseConnection", new object[]
				{
					new XElement("ConnectionString", text ?? ""),
					new XElement("DatabasePassword", text2 ?? ""),
					new XElement("NoDirectDbAccess", flag.ToString())
				}),
				new XElement("ServerConnection", new object[]
				{
					new XElement("HostName", text3 ?? ""),
					new XElement("Port", text4 ?? ""),
					new XElement("ExternalHostName", text5 ?? ""),
					new XElement("ExternalPort", text6 ?? ""),
					new XElement("VirtualDirectory", text7 ?? ""),
					new XElement("IdentityDNS", text8 ?? ""),
					new XElement("IISVersion", text9 ?? ""),
					new XElement("BindingType", text12 ?? ""),
					new XElement("Certificate", new object[]
					{
						new XElement("CertPublicKey", text10 ?? ""),
						new XElement("CertSubjectName", text11 ?? "")
					})
				})
			});
			return this.StoreXmlInStorageString(string.Format("<?xml version=\"1.0\"?>{0}", xelement.ToString()), text ?? "", text2 ?? "");
		}

		// Token: 0x04000014 RID: 20
		private readonly IEncryption StaticEncryption = EncryptionFactory.GetEncryption(EncryptionType.TripleDES_192bit);
	}
}
