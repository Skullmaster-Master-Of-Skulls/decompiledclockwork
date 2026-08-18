using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200003F RID: 63
	internal class CoordinationExternal11Dictionary
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00009874 File Offset: 0x00007A74
		public CoordinationExternal11Dictionary(XmlDictionary dictionary)
		{
			this.Namespace = dictionary.Add("http://docs.oasis-open.org/ws-tx/wscoor/2006/06");
			this.CreateCoordinationContextAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wscoor/2006/06/CreateCoordinationContext");
			this.CreateCoordinationContextResponseAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wscoor/2006/06/CreateCoordinationContextResponse");
			this.RegisterAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wscoor/2006/06/Register");
			this.RegisterResponseAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wscoor/2006/06/RegisterResponse");
			this.FaultAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wscoor/2006/06/fault");
			this.CannotCreateContext = dictionary.Add("CannotCreateContext");
			this.CannotRegisterParticipant = dictionary.Add("CannotRegisterParticipant");
		}

		// Token: 0x040001CF RID: 463
		public XmlDictionaryString Namespace;

		// Token: 0x040001D0 RID: 464
		public XmlDictionaryString CreateCoordinationContextAction;

		// Token: 0x040001D1 RID: 465
		public XmlDictionaryString CreateCoordinationContextResponseAction;

		// Token: 0x040001D2 RID: 466
		public XmlDictionaryString RegisterAction;

		// Token: 0x040001D3 RID: 467
		public XmlDictionaryString RegisterResponseAction;

		// Token: 0x040001D4 RID: 468
		public XmlDictionaryString FaultAction;

		// Token: 0x040001D5 RID: 469
		public XmlDictionaryString CannotCreateContext;

		// Token: 0x040001D6 RID: 470
		public XmlDictionaryString CannotRegisterParticipant;
	}
}
