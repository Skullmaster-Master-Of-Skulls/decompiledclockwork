using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000CA RID: 202
	internal class SecurityAlgorithmDec2005Dictionary
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x00017260 File Offset: 0x00015460
		public SecurityAlgorithmDec2005Dictionary(IdentityModelDictionary dictionary)
		{
			this.Psha1KeyDerivationDec2005 = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1", 267);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001727E File Offset: 0x0001547E
		public SecurityAlgorithmDec2005Dictionary(IXmlDictionary dictionary)
		{
			this.Psha1KeyDerivationDec2005 = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1");
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00017298 File Offset: 0x00015498
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

		// Token: 0x0400057C RID: 1404
		public XmlDictionaryString Psha1KeyDerivationDec2005;
	}
}
