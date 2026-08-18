using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000240 RID: 576
	public abstract class XmlSchemaContentModel : XmlSchemaAnnotated
	{
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001B78 RID: 7032
		// (set) Token: 0x06001B79 RID: 7033
		[XmlIgnore]
		public abstract XmlSchemaContent Content { get; set; }
	}
}
