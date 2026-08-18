using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200027D RID: 637
	public abstract class XmlSchemaContentModel : XmlSchemaAnnotated
	{
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002644 RID: 9796
		// (set) Token: 0x06002645 RID: 9797
		[XmlIgnore]
		public abstract XmlSchemaContent Content { get; set; }
	}
}
