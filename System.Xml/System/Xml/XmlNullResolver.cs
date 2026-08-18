using System;

namespace System.Xml
{
	// Token: 0x02000047 RID: 71
	internal class XmlNullResolver : XmlUrlResolver
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00008D6B File Offset: 0x00007D6B
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			throw new XmlException("Xml_NullResolver", string.Empty);
		}

		// Token: 0x040004F3 RID: 1267
		public static readonly XmlNullResolver Singleton = new XmlNullResolver();
	}
}
