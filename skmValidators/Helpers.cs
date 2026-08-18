using System;
using System.IO;
using System.Web;
using System.Xml;

namespace skmValidators
{
	// Token: 0x02000002 RID: 2
	internal class Helpers
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00001050
		public static bool EnableLegacyRendering()
		{
			bool result;
			try
			{
				string path = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config");
				XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(path));
				result = (xmlTextReader.ReadToFollowing("xhtmlConformance") && xmlTextReader.GetAttribute("mode") == "Legacy");
				xmlTextReader.Close();
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
