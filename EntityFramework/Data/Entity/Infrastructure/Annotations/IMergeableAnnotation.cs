using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x0200013F RID: 319
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mergeable")]
	public interface IMergeableAnnotation
	{
		// Token: 0x06000A9E RID: 2718
		CompatibilityResult IsCompatibleWith(object other);

		// Token: 0x06000A9F RID: 2719
		object MergeWith(object other);
	}
}
