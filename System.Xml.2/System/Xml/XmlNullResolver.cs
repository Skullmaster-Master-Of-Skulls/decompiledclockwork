using System;
using System.Net;

namespace System.Xml
{
	// Token: 0x02000094 RID: 148
	internal class XmlNullResolver : XmlResolver
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x00013B70 File Offset: 0x00011D70
		private XmlNullResolver()
		{
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00013B78 File Offset: 0x00011D78
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			throw new XmlException("Xml_NullResolver", string.Empty);
		}

		// Token: 0x17000104 RID: 260
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x00013B89 File Offset: 0x00011D89
		public override ICredentials Credentials
		{
			set
			{
			}
		}

		// Token: 0x04000226 RID: 550
		public static readonly XmlNullResolver Singleton = new XmlNullResolver();
	}
}
