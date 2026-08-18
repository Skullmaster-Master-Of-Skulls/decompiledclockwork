using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000281 RID: 641
	[Flags]
	public enum XmlSchemaDerivationMethod
	{
		// Token: 0x040010BC RID: 4284
		[XmlEnum("")]
		Empty = 0,
		// Token: 0x040010BD RID: 4285
		[XmlEnum("substitution")]
		Substitution = 1,
		// Token: 0x040010BE RID: 4286
		[XmlEnum("extension")]
		Extension = 2,
		// Token: 0x040010BF RID: 4287
		[XmlEnum("restriction")]
		Restriction = 4,
		// Token: 0x040010C0 RID: 4288
		[XmlEnum("list")]
		List = 8,
		// Token: 0x040010C1 RID: 4289
		[XmlEnum("union")]
		Union = 16,
		// Token: 0x040010C2 RID: 4290
		[XmlEnum("#all")]
		All = 255,
		// Token: 0x040010C3 RID: 4291
		[XmlIgnore]
		None = 256
	}
}
