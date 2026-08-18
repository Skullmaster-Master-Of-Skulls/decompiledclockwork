using System;
using System.Configuration.Provider;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200007C RID: 124
	public abstract class ProtectedConfigurationProvider : ProviderBase
	{
		// Token: 0x060004B8 RID: 1208
		public abstract XmlNode Encrypt(XmlNode node);

		// Token: 0x060004B9 RID: 1209
		public abstract XmlNode Decrypt(XmlNode encryptedNode);

		// Token: 0x060004BA RID: 1210 RVA: 0x000194F4 File Offset: 0x000176F4
		internal static void LoadXml(XmlDocument xmlDoc, string xmlText)
		{
			using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(xmlText)))
			{
				xmlTextReader.DtdProcessing = DtdProcessing.Ignore;
				xmlDoc.Load(xmlTextReader);
			}
		}
	}
}
