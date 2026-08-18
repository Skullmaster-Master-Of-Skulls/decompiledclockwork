using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200006A RID: 106
	internal class SecureConversationDictionary
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000DFF5 File Offset: 0x0000C1F5
		public SecureConversationDictionary()
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000DFFD File Offset: 0x0000C1FD
		public SecureConversationDictionary(ServiceModelDictionary dictionary)
		{
		}

		// Token: 0x040005A5 RID: 1445
		public XmlDictionaryString Namespace;

		// Token: 0x040005A6 RID: 1446
		public XmlDictionaryString DerivedKeyToken;

		// Token: 0x040005A7 RID: 1447
		public XmlDictionaryString Nonce;

		// Token: 0x040005A8 RID: 1448
		public XmlDictionaryString Length;

		// Token: 0x040005A9 RID: 1449
		public XmlDictionaryString SecurityContextToken;

		// Token: 0x040005AA RID: 1450
		public XmlDictionaryString AlgorithmAttribute;

		// Token: 0x040005AB RID: 1451
		public XmlDictionaryString Generation;

		// Token: 0x040005AC RID: 1452
		public XmlDictionaryString Label;

		// Token: 0x040005AD RID: 1453
		public XmlDictionaryString Offset;

		// Token: 0x040005AE RID: 1454
		public XmlDictionaryString Properties;

		// Token: 0x040005AF RID: 1455
		public XmlDictionaryString Identifier;

		// Token: 0x040005B0 RID: 1456
		public XmlDictionaryString Cookie;

		// Token: 0x040005B1 RID: 1457
		public XmlDictionaryString Prefix;

		// Token: 0x040005B2 RID: 1458
		public XmlDictionaryString DerivedKeyTokenType;

		// Token: 0x040005B3 RID: 1459
		public XmlDictionaryString SecurityContextTokenType;

		// Token: 0x040005B4 RID: 1460
		public XmlDictionaryString SecurityContextTokenReferenceValueType;

		// Token: 0x040005B5 RID: 1461
		public XmlDictionaryString RequestSecurityContextIssuance;

		// Token: 0x040005B6 RID: 1462
		public XmlDictionaryString RequestSecurityContextIssuanceResponse;

		// Token: 0x040005B7 RID: 1463
		public XmlDictionaryString RenewNeededFaultCode;

		// Token: 0x040005B8 RID: 1464
		public XmlDictionaryString BadContextTokenFaultCode;
	}
}
