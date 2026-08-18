using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004C0 RID: 1216
	internal enum QueryAxisType : byte
	{
		// Token: 0x0400251A RID: 9498
		None,
		// Token: 0x0400251B RID: 9499
		Ancestor,
		// Token: 0x0400251C RID: 9500
		AncestorOrSelf,
		// Token: 0x0400251D RID: 9501
		Attribute,
		// Token: 0x0400251E RID: 9502
		Child,
		// Token: 0x0400251F RID: 9503
		Descendant,
		// Token: 0x04002520 RID: 9504
		DescendantOrSelf,
		// Token: 0x04002521 RID: 9505
		Following,
		// Token: 0x04002522 RID: 9506
		FollowingSibling,
		// Token: 0x04002523 RID: 9507
		Namespace,
		// Token: 0x04002524 RID: 9508
		Parent,
		// Token: 0x04002525 RID: 9509
		Preceding,
		// Token: 0x04002526 RID: 9510
		PrecedingSibling,
		// Token: 0x04002527 RID: 9511
		Self
	}
}
