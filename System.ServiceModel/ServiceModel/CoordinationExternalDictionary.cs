using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200005D RID: 93
	internal class CoordinationExternalDictionary
	{
		// Token: 0x0600025C RID: 604 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		public CoordinationExternalDictionary(ServiceModelDictionary dictionary)
		{
			this.Prefix = dictionary.CreateString("wscoor", 357);
			this.CreateCoordinationContext = dictionary.CreateString("CreateCoordinationContext", 358);
			this.CreateCoordinationContextResponse = dictionary.CreateString("CreateCoordinationContextResponse", 359);
			this.CoordinationContext = dictionary.CreateString("CoordinationContext", 360);
			this.CurrentContext = dictionary.CreateString("CurrentContext", 361);
			this.CoordinationType = dictionary.CreateString("CoordinationType", 362);
			this.RegistrationService = dictionary.CreateString("RegistrationService", 363);
			this.Register = dictionary.CreateString("Register", 364);
			this.RegisterResponse = dictionary.CreateString("RegisterResponse", 365);
			this.Protocol = dictionary.CreateString("ProtocolIdentifier", 366);
			this.CoordinatorProtocolService = dictionary.CreateString("CoordinatorProtocolService", 367);
			this.ParticipantProtocolService = dictionary.CreateString("ParticipantProtocolService", 368);
			this.Expires = dictionary.CreateString("Expires", 55);
			this.Identifier = dictionary.CreateString("Identifier", 15);
			this.ActivationCoordinatorPortType = dictionary.CreateString("ActivationCoordinatorPortType", 374);
			this.RegistrationCoordinatorPortType = dictionary.CreateString("RegistrationCoordinatorPortType", 375);
			this.InvalidState = dictionary.CreateString("InvalidState", 376);
			this.InvalidProtocol = dictionary.CreateString("InvalidProtocol", 377);
			this.InvalidParameters = dictionary.CreateString("InvalidParameters", 378);
			this.NoActivity = dictionary.CreateString("NoActivity", 379);
			this.ContextRefused = dictionary.CreateString("ContextRefused", 380);
			this.AlreadyRegistered = dictionary.CreateString("AlreadyRegistered", 381);
		}

		// Token: 0x04000512 RID: 1298
		public XmlDictionaryString Prefix;

		// Token: 0x04000513 RID: 1299
		public XmlDictionaryString CreateCoordinationContext;

		// Token: 0x04000514 RID: 1300
		public XmlDictionaryString CreateCoordinationContextResponse;

		// Token: 0x04000515 RID: 1301
		public XmlDictionaryString CoordinationContext;

		// Token: 0x04000516 RID: 1302
		public XmlDictionaryString CurrentContext;

		// Token: 0x04000517 RID: 1303
		public XmlDictionaryString CoordinationType;

		// Token: 0x04000518 RID: 1304
		public XmlDictionaryString RegistrationService;

		// Token: 0x04000519 RID: 1305
		public XmlDictionaryString Register;

		// Token: 0x0400051A RID: 1306
		public XmlDictionaryString RegisterResponse;

		// Token: 0x0400051B RID: 1307
		public XmlDictionaryString Protocol;

		// Token: 0x0400051C RID: 1308
		public XmlDictionaryString CoordinatorProtocolService;

		// Token: 0x0400051D RID: 1309
		public XmlDictionaryString ParticipantProtocolService;

		// Token: 0x0400051E RID: 1310
		public XmlDictionaryString Expires;

		// Token: 0x0400051F RID: 1311
		public XmlDictionaryString Identifier;

		// Token: 0x04000520 RID: 1312
		public XmlDictionaryString ActivationCoordinatorPortType;

		// Token: 0x04000521 RID: 1313
		public XmlDictionaryString RegistrationCoordinatorPortType;

		// Token: 0x04000522 RID: 1314
		public XmlDictionaryString InvalidState;

		// Token: 0x04000523 RID: 1315
		public XmlDictionaryString InvalidProtocol;

		// Token: 0x04000524 RID: 1316
		public XmlDictionaryString InvalidParameters;

		// Token: 0x04000525 RID: 1317
		public XmlDictionaryString NoActivity;

		// Token: 0x04000526 RID: 1318
		public XmlDictionaryString ContextRefused;

		// Token: 0x04000527 RID: 1319
		public XmlDictionaryString AlreadyRegistered;
	}
}
