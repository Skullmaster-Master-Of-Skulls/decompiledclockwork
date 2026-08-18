using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007CE RID: 1998
	[Serializable]
	internal enum SoapAttributeType
	{
		// Token: 0x040023AE RID: 9134
		None,
		// Token: 0x040023AF RID: 9135
		SchemaType,
		// Token: 0x040023B0 RID: 9136
		Embedded,
		// Token: 0x040023B1 RID: 9137
		XmlElement = 4,
		// Token: 0x040023B2 RID: 9138
		XmlAttribute = 8
	}
}
