using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000249 RID: 585
	[Flags]
	public enum XmlSchemaDerivationMethod
	{
		// Token: 0x04001140 RID: 4416
		[XmlEnum("")]
		Empty = 0,
		// Token: 0x04001141 RID: 4417
		[XmlEnum("substitution")]
		Substitution = 1,
		// Token: 0x04001142 RID: 4418
		[XmlEnum("extension")]
		Extension = 2,
		// Token: 0x04001143 RID: 4419
		[XmlEnum("restriction")]
		Restriction = 4,
		// Token: 0x04001144 RID: 4420
		[XmlEnum("list")]
		List = 8,
		// Token: 0x04001145 RID: 4421
		[XmlEnum("union")]
		Union = 16,
		// Token: 0x04001146 RID: 4422
		[XmlEnum("#all")]
		All = 255,
		// Token: 0x04001147 RID: 4423
		[XmlIgnore]
		None = 256
	}
}
