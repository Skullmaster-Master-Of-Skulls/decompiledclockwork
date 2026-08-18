using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000060 RID: 96
	internal class DotNetAtomicTransactionExternalDictionary
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public DotNetAtomicTransactionExternalDictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.microsoft.com/ws/2006/02/transactions", 65);
			this.Prefix = dictionary.CreateString("mstx", 409);
			this.Enlistment = dictionary.CreateString("Enlistment", 410);
			this.Protocol = dictionary.CreateString("protocol", 411);
			this.LocalTransactionId = dictionary.CreateString("LocalTransactionId", 412);
			this.IsolationLevel = dictionary.CreateString("IsolationLevel", 413);
			this.IsolationFlags = dictionary.CreateString("IsolationFlags", 414);
			this.Description = dictionary.CreateString("Description", 415);
			this.Loopback = dictionary.CreateString("Loopback", 416);
			this.RegisterInfo = dictionary.CreateString("RegisterInfo", 417);
			this.ContextId = dictionary.CreateString("ContextId", 418);
			this.TokenId = dictionary.CreateString("TokenId", 419);
			this.AccessDenied = dictionary.CreateString("AccessDenied", 420);
			this.InvalidPolicy = dictionary.CreateString("InvalidPolicy", 421);
			this.CoordinatorRegistrationFailed = dictionary.CreateString("CoordinatorRegistrationFailed", 422);
			this.TooManyEnlistments = dictionary.CreateString("TooManyEnlistments", 423);
			this.Disabled = dictionary.CreateString("Disabled", 424);
		}

		// Token: 0x04000531 RID: 1329
		public XmlDictionaryString Namespace;

		// Token: 0x04000532 RID: 1330
		public XmlDictionaryString Prefix;

		// Token: 0x04000533 RID: 1331
		public XmlDictionaryString Enlistment;

		// Token: 0x04000534 RID: 1332
		public XmlDictionaryString Protocol;

		// Token: 0x04000535 RID: 1333
		public XmlDictionaryString LocalTransactionId;

		// Token: 0x04000536 RID: 1334
		public XmlDictionaryString IsolationLevel;

		// Token: 0x04000537 RID: 1335
		public XmlDictionaryString IsolationFlags;

		// Token: 0x04000538 RID: 1336
		public XmlDictionaryString Description;

		// Token: 0x04000539 RID: 1337
		public XmlDictionaryString Loopback;

		// Token: 0x0400053A RID: 1338
		public XmlDictionaryString RegisterInfo;

		// Token: 0x0400053B RID: 1339
		public XmlDictionaryString ContextId;

		// Token: 0x0400053C RID: 1340
		public XmlDictionaryString TokenId;

		// Token: 0x0400053D RID: 1341
		public XmlDictionaryString AccessDenied;

		// Token: 0x0400053E RID: 1342
		public XmlDictionaryString InvalidPolicy;

		// Token: 0x0400053F RID: 1343
		public XmlDictionaryString CoordinatorRegistrationFailed;

		// Token: 0x04000540 RID: 1344
		public XmlDictionaryString TooManyEnlistments;

		// Token: 0x04000541 RID: 1345
		public XmlDictionaryString Disabled;
	}
}
