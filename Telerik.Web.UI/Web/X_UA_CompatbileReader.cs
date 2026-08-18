using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Xml;

namespace Telerik.Web
{
	// Token: 0x020000D0 RID: 208
	public class X_UA_CompatbileReader
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x0001E134 File Offset: 0x0001C334
		public virtual bool IsEdge(HttpContext context)
		{
			if (this.ReadResponseHeaders(context.Response))
			{
				return true;
			}
			XmlDocument xmlDocument = this.LoadWebServerConfig(context.Request.ApplicationPath);
			if (xmlDocument == null)
			{
				return false;
			}
			XmlNode xmlNode = xmlDocument.SelectSingleNode("//add[@name='X-UA-Compatible']");
			return xmlNode != null && xmlNode.Attributes["value"].Value.Equals("IE=edge", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001E19C File Offset: 0x0001C39C
		private bool ReadResponseHeaders(HttpResponse response)
		{
			bool result;
			try
			{
				if (response.Headers["X-UA-Compatible"] != null)
				{
					result = response.Headers["X-UA-Compatible"].Equals("IE=edge", StringComparison.OrdinalIgnoreCase);
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001E1F4 File Offset: 0x0001C3F4
		private XmlDocument LoadWebServerConfig(string applicationPath)
		{
			X_UA_CompatbileReader.Count++;
			XmlDocument result;
			try
			{
				Configuration configuration = WebConfigurationManager.OpenWebConfiguration(applicationPath);
				ConfigurationSection section = configuration.GetSection("system.webServer");
				string rawXml = section.SectionInformation.GetRawXml();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(rawXml);
				result = xmlDocument;
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040001E3 RID: 483
		private const string Edge = "IE=edge";

		// Token: 0x040001E4 RID: 484
		private const string X_UA_Compatible = "X-UA-Compatible";

		// Token: 0x040001E5 RID: 485
		public static int Count;
	}
}
