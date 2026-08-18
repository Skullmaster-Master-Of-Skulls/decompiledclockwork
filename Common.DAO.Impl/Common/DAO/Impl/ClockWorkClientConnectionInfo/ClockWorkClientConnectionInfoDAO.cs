using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.ClockWorkClientConnectionInfo;

namespace TechnoPro.Common.DAO.Impl.ClockWorkClientConnectionInfo
{
	// Token: 0x02000115 RID: 277
	public class ClockWorkClientConnectionInfoDAO : IClockWorkClientConnectionInfoDAO
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x00051E59 File Offset: 0x00050059
		public ClockWorkClientConnectionInfoDAO()
		{
			this.StaticEncryption = EncryptionFactory.GetEncryption(EncryptionType.TripleDES_192bit);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00051E70 File Offset: 0x00050070
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

		// Token: 0x060007EE RID: 2030 RVA: 0x00051F78 File Offset: 0x00050178
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

		// Token: 0x060007EF RID: 2031 RVA: 0x0005200C File Offset: 0x0005020C
		public string StoreXmlInStorageString(string Xml, string ConnectionString, string DbPassword)
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

		// Token: 0x060007F0 RID: 2032 RVA: 0x000520F8 File Offset: 0x000502F8
		public string GetXmlFromStorageString(string StorageString)
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

		// Token: 0x040004A2 RID: 1186
		private IEncryption StaticEncryption;
	}
}
