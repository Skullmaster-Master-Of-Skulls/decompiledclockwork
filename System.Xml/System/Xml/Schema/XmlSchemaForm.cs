using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200025D RID: 605
	public enum XmlSchemaForm
	{
		// Token: 0x04001180 RID: 4480
		[XmlIgnore]
		None,
		// Token: 0x04001181 RID: 4481
		[XmlEnum("qualified")]
		Qualified,
		// Token: 0x04001182 RID: 4482
		[XmlEnum("unqualified")]
		Unqualified
	}
}
