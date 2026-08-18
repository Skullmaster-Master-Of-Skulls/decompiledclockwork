using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000B1 RID: 177
	internal interface IValidationEventHandling
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000623 RID: 1571
		object EventHandler { get; }

		// Token: 0x06000624 RID: 1572
		void SendEvent(Exception exception, XmlSeverityType severity);
	}
}
