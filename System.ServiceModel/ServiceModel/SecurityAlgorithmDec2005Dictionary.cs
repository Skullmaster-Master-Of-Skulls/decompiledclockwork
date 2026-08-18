using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000041 RID: 65
	internal class SecurityAlgorithmDec2005Dictionary
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00009CF2 File Offset: 0x00007EF2
		public SecurityAlgorithmDec2005Dictionary(XmlDictionary dictionary)
		{
			this.Psha1KeyDerivationDec2005 = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1");
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009D16 File Offset: 0x00007F16
		public void PopulateSecurityAlgorithmDictionaryString()
		{
			this.SecurityAlgorithmDictionaryStrings.Add(DXD.SecurityAlgorithmDec2005Dictionary.Psha1KeyDerivationDec2005);
		}

		// Token: 0x040001DD RID: 477
		public XmlDictionaryString Psha1KeyDerivationDec2005;

		// Token: 0x040001DE RID: 478
		public List<XmlDictionaryString> SecurityAlgorithmDictionaryStrings = new List<XmlDictionaryString>();
	}
}
