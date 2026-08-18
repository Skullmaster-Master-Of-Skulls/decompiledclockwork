using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200018C RID: 396
	internal enum XmlAttributeFlags
	{
		// Token: 0x04000BC9 RID: 3017
		Enum = 1,
		// Token: 0x04000BCA RID: 3018
		Array,
		// Token: 0x04000BCB RID: 3019
		Text = 4,
		// Token: 0x04000BCC RID: 3020
		ArrayItems = 8,
		// Token: 0x04000BCD RID: 3021
		Elements = 16,
		// Token: 0x04000BCE RID: 3022
		Attribute = 32,
		// Token: 0x04000BCF RID: 3023
		Root = 64,
		// Token: 0x04000BD0 RID: 3024
		Type = 128,
		// Token: 0x04000BD1 RID: 3025
		AnyElements = 256,
		// Token: 0x04000BD2 RID: 3026
		AnyAttribute = 512,
		// Token: 0x04000BD3 RID: 3027
		ChoiceIdentifier = 1024,
		// Token: 0x04000BD4 RID: 3028
		XmlnsDeclarations = 2048
	}
}
