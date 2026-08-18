using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000D0 RID: 208
	internal class UtilityDictionary
	{
		// Token: 0x0600061E RID: 1566 RVA: 0x00018CE4 File Offset: 0x00016EE4
		public UtilityDictionary(IdentityModelDictionary dictionary)
		{
			this.IdAttribute = dictionary.CreateString("Id", 3);
			this.Namespace = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", 16);
			this.Timestamp = dictionary.CreateString("Timestamp", 17);
			this.CreatedElement = dictionary.CreateString("Created", 18);
			this.ExpiresElement = dictionary.CreateString("Expires", 19);
			this.Prefix = dictionary.CreateString("u", 81);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00018D68 File Offset: 0x00016F68
		public UtilityDictionary(IXmlDictionary dictionary)
		{
			this.IdAttribute = this.LookupDictionaryString(dictionary, "Id");
			this.Namespace = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
			this.Timestamp = this.LookupDictionaryString(dictionary, "Timestamp");
			this.CreatedElement = this.LookupDictionaryString(dictionary, "Created");
			this.ExpiresElement = this.LookupDictionaryString(dictionary, "Expires");
			this.Prefix = this.LookupDictionaryString(dictionary, "u");
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00018DE8 File Offset: 0x00016FE8
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

		// Token: 0x040005E7 RID: 1511
		public XmlDictionaryString IdAttribute;

		// Token: 0x040005E8 RID: 1512
		public XmlDictionaryString Namespace;

		// Token: 0x040005E9 RID: 1513
		public XmlDictionaryString Timestamp;

		// Token: 0x040005EA RID: 1514
		public XmlDictionaryString CreatedElement;

		// Token: 0x040005EB RID: 1515
		public XmlDictionaryString ExpiresElement;

		// Token: 0x040005EC RID: 1516
		public XmlDictionaryString Prefix;
	}
}
