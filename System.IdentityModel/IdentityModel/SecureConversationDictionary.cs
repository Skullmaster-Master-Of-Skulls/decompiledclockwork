using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000C6 RID: 198
	internal class SecureConversationDictionary
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00004469 File Offset: 0x00002669
		public SecureConversationDictionary()
		{
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00004469 File Offset: 0x00002669
		public SecureConversationDictionary(IdentityModelDictionary dictionary)
		{
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00004469 File Offset: 0x00002669
		public SecureConversationDictionary(IXmlDictionary dictionary)
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000165C8 File Offset: 0x000147C8
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

		// Token: 0x0400054A RID: 1354
		public XmlDictionaryString Namespace;

		// Token: 0x0400054B RID: 1355
		public XmlDictionaryString DerivedKeyToken;

		// Token: 0x0400054C RID: 1356
		public XmlDictionaryString Nonce;

		// Token: 0x0400054D RID: 1357
		public XmlDictionaryString Length;

		// Token: 0x0400054E RID: 1358
		public XmlDictionaryString SecurityContextToken;

		// Token: 0x0400054F RID: 1359
		public XmlDictionaryString AlgorithmAttribute;

		// Token: 0x04000550 RID: 1360
		public XmlDictionaryString Generation;

		// Token: 0x04000551 RID: 1361
		public XmlDictionaryString Label;

		// Token: 0x04000552 RID: 1362
		public XmlDictionaryString Offset;

		// Token: 0x04000553 RID: 1363
		public XmlDictionaryString Properties;

		// Token: 0x04000554 RID: 1364
		public XmlDictionaryString Identifier;

		// Token: 0x04000555 RID: 1365
		public XmlDictionaryString Cookie;

		// Token: 0x04000556 RID: 1366
		public XmlDictionaryString RenewNeededFaultCode;

		// Token: 0x04000557 RID: 1367
		public XmlDictionaryString BadContextTokenFaultCode;

		// Token: 0x04000558 RID: 1368
		public XmlDictionaryString Prefix;

		// Token: 0x04000559 RID: 1369
		public XmlDictionaryString DerivedKeyTokenType;

		// Token: 0x0400055A RID: 1370
		public XmlDictionaryString SecurityContextTokenType;

		// Token: 0x0400055B RID: 1371
		public XmlDictionaryString SecurityContextTokenReferenceValueType;

		// Token: 0x0400055C RID: 1372
		public XmlDictionaryString RequestSecurityContextIssuance;

		// Token: 0x0400055D RID: 1373
		public XmlDictionaryString RequestSecurityContextIssuanceResponse;

		// Token: 0x0400055E RID: 1374
		public XmlDictionaryString RequestSecurityContextRenew;

		// Token: 0x0400055F RID: 1375
		public XmlDictionaryString RequestSecurityContextRenewResponse;

		// Token: 0x04000560 RID: 1376
		public XmlDictionaryString RequestSecurityContextClose;

		// Token: 0x04000561 RID: 1377
		public XmlDictionaryString RequestSecurityContextCloseResponse;

		// Token: 0x04000562 RID: 1378
		public XmlDictionaryString Instance;
	}
}
