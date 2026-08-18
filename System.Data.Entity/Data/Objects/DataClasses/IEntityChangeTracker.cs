using System;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000191 RID: 401
	public interface IEntityChangeTracker
	{
		// Token: 0x06001CEF RID: 7407
		void EntityMemberChanging(string entityMemberName);

		// Token: 0x06001CF0 RID: 7408
		void EntityMemberChanged(string entityMemberName);

		// Token: 0x06001CF1 RID: 7409
		void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x06001CF2 RID: 7410
		void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001CF3 RID: 7411
		EntityState EntityState { get; }
	}
}
