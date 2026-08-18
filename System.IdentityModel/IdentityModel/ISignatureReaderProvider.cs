using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200007F RID: 127
	internal interface ISignatureReaderProvider
	{
		// Token: 0x06000465 RID: 1125
		XmlDictionaryReader GetReader(object callbackContext);
	}
}
