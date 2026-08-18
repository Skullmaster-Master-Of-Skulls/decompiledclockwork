using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000C4 RID: 196
	internal class ExclusiveC14NDictionary
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00015C34 File Offset: 0x00013E34
		public ExclusiveC14NDictionary(IdentityModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://www.w3.org/2001/10/xml-exc-c14n#", 20);
			this.PrefixList = dictionary.CreateString("PrefixList", 21);
			this.InclusiveNamespaces = dictionary.CreateString("InclusiveNamespaces", 22);
			this.Prefix = dictionary.CreateString("ec", 23);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00015C94 File Offset: 0x00013E94
		public ExclusiveC14NDictionary(IXmlDictionary dictionary)
		{
			this.Namespace = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/10/xml-exc-c14n#");
			this.PrefixList = this.LookupDictionaryString(dictionary, "PrefixList");
			this.InclusiveNamespaces = this.LookupDictionaryString(dictionary, "InclusiveNamespaces");
			this.Prefix = this.LookupDictionaryString(dictionary, "ec");
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00015CF0 File Offset: 0x00013EF0
		private XmlDictionaryString LookupDictionaryString(IXmlDictionary dictionary, string value)
		{
			XmlDictionaryString result;
			if (!dictionary.TryLookup(value, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("XDCannotFindValueInDictionaryString", new object[]
				{
					value
				}));
			}
			return result;
		}

		// Token: 0x0400050D RID: 1293
		public XmlDictionaryString Namespace;

		// Token: 0x0400050E RID: 1294
		public XmlDictionaryString PrefixList;

		// Token: 0x0400050F RID: 1295
		public XmlDictionaryString InclusiveNamespaces;

		// Token: 0x04000510 RID: 1296
		public XmlDictionaryString Prefix;
	}
}
