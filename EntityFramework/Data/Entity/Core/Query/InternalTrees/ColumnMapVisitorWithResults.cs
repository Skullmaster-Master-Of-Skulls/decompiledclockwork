using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020002EF RID: 751
	internal abstract class ColumnMapVisitorWithResults<TResultType, TArgType>
	{
		// Token: 0x06001A73 RID: 6771 RVA: 0x000836A0 File Offset: 0x000818A0
		protected EntityIdentity VisitEntityIdentity(EntityIdentity entityIdentity, TArgType arg)
		{
			DiscriminatedEntityIdentity discriminatedEntityIdentity = entityIdentity as DiscriminatedEntityIdentity;
			if (discriminatedEntityIdentity != null)
			{
				return this.VisitEntityIdentity(discriminatedEntityIdentity, arg);
			}
			return this.VisitEntityIdentity((SimpleEntityIdentity)entityIdentity, arg);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000836CD File Offset: 0x000818CD
		protected virtual EntityIdentity VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, TArgType arg)
		{
			return entityIdentity;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x000836D0 File Offset: 0x000818D0
		protected virtual EntityIdentity VisitEntityIdentity(SimpleEntityIdentity entityIdentity, TArgType arg)
		{
			return entityIdentity;
		}

		// Token: 0x06001A76 RID: 6774
		internal abstract TResultType Visit(ComplexTypeColumnMap columnMap, TArgType arg);

		// Token: 0x06001A77 RID: 6775
		internal abstract TResultType Visit(DiscriminatedCollectionColumnMap columnMap, TArgType arg);

		// Token: 0x06001A78 RID: 6776
		internal abstract TResultType Visit(EntityColumnMap columnMap, TArgType arg);

		// Token: 0x06001A79 RID: 6777
		internal abstract TResultType Visit(SimplePolymorphicColumnMap columnMap, TArgType arg);

		// Token: 0x06001A7A RID: 6778
		internal abstract TResultType Visit(RecordColumnMap columnMap, TArgType arg);

		// Token: 0x06001A7B RID: 6779
		internal abstract TResultType Visit(RefColumnMap columnMap, TArgType arg);

		// Token: 0x06001A7C RID: 6780
		internal abstract TResultType Visit(ScalarColumnMap columnMap, TArgType arg);

		// Token: 0x06001A7D RID: 6781
		internal abstract TResultType Visit(SimpleCollectionColumnMap columnMap, TArgType arg);

		// Token: 0x06001A7E RID: 6782
		internal abstract TResultType Visit(VarRefColumnMap columnMap, TArgType arg);

		// Token: 0x06001A7F RID: 6783
		internal abstract TResultType Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, TArgType arg);
	}
}
