using System;
using System.IO;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001827 RID: 6183
	internal static class ClientKeyConverter
	{
		// Token: 0x0600F070 RID: 61552 RVA: 0x0036A660 File Offset: 0x00368860
		public static string SerializeKey(object key)
		{
			LosFormatter losFormatter = new LosFormatter();
			StringWriter stringWriter = new StringWriter();
			losFormatter.Serialize(stringWriter, key);
			return stringWriter.ToString();
		}

		// Token: 0x0600F071 RID: 61553 RVA: 0x0036A688 File Offset: 0x00368888
		public static object DeserializeKey(string key)
		{
			LosFormatter losFormatter = new LosFormatter();
			return losFormatter.Deserialize(key);
		}
	}
}
