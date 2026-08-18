using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000541 RID: 1345
	public interface IEntityChangeTracker
	{
		// Token: 0x060033D4 RID: 13268
		void EntityMemberChanging(string entityMemberName);

		// Token: 0x060033D5 RID: 13269
		void EntityMemberChanged(string entityMemberName);

		// Token: 0x060033D6 RID: 13270
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object")]
		void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x060033D7 RID: 13271
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object")]
		void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060033D8 RID: 13272
		EntityState EntityState { get; }
	}
}
