using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002F6 RID: 758
	internal enum TypeFlags
	{
		// Token: 0x04001507 RID: 5383
		None,
		// Token: 0x04001508 RID: 5384
		Abstract,
		// Token: 0x04001509 RID: 5385
		Reference,
		// Token: 0x0400150A RID: 5386
		Special = 4,
		// Token: 0x0400150B RID: 5387
		CanBeAttributeValue = 8,
		// Token: 0x0400150C RID: 5388
		CanBeTextValue = 16,
		// Token: 0x0400150D RID: 5389
		CanBeElementValue = 32,
		// Token: 0x0400150E RID: 5390
		HasCustomFormatter = 64,
		// Token: 0x0400150F RID: 5391
		AmbiguousDataType = 128,
		// Token: 0x04001510 RID: 5392
		IgnoreDefault = 512,
		// Token: 0x04001511 RID: 5393
		HasIsEmpty = 1024,
		// Token: 0x04001512 RID: 5394
		HasDefaultConstructor = 2048,
		// Token: 0x04001513 RID: 5395
		XmlEncodingNotRequired = 4096,
		// Token: 0x04001514 RID: 5396
		UseReflection = 16384,
		// Token: 0x04001515 RID: 5397
		CollapseWhitespace = 32768,
		// Token: 0x04001516 RID: 5398
		OptionalValue = 65536,
		// Token: 0x04001517 RID: 5399
		CtorInaccessible = 131072,
		// Token: 0x04001518 RID: 5400
		UsePrivateImplementation = 262144,
		// Token: 0x04001519 RID: 5401
		GenericInterface = 524288,
		// Token: 0x0400151A RID: 5402
		Unsupported = 1048576
	}
}
