using System;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.ButtonNS.JavaScriptSerialization
{
	// Token: 0x020000DA RID: 218
	public static class JavaScriptSerializeProvider
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x0001EE26 File Offset: 0x0001D026
		public static JavaScriptSerializer CreateSerializer()
		{
			return new JavaScriptSerializer();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001EE30 File Offset: 0x0001D030
		public static JavaScriptSerializer CreateSerializer(IJavaScriptConverterProvider convertersProvider)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(convertersProvider.GetJsConverters());
			return javaScriptSerializer;
		}
	}
}
