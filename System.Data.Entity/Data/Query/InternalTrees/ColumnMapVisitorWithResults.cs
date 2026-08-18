using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000AD RID: 173
	internal abstract class ColumnMapVisitorWithResults<TResultType, TArgType>
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x00036C64 File Offset: 0x00034E64
		protected EntityIdentity VisitEntityIdentity(EntityIdentity entityIdentity, TArgType arg)
		{
			DiscriminatedEntityIdentity discriminatedEntityIdentity = entityIdentity as DiscriminatedEntityIdentity;
			if (discriminatedEntityIdentity != null)
			{
				return this.VisitEntityIdentity(discriminatedEntityIdentity, arg);
			}
			return this.VisitEntityIdentity((SimpleEntityIdentity)entityIdentity, arg);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00002391 File Offset: 0x00000591
		protected virtual EntityIdentity VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, TArgType arg)
		{
			return entityIdentity;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00002391 File Offset: 0x00000591
		protected virtual EntityIdentity VisitEntityIdentity(SimpleEntityIdentity entityIdentity, TArgType arg)
		{
			return entityIdentity;
		}

		// Token: 0x06000A6F RID: 2671
		internal abstract TResultType Visit(ComplexTypeColumnMap columnMap, TArgType arg);

		// Token: 0x06000A70 RID: 2672
		internal abstract TResultType Visit(DiscriminatedCollectionColumnMap columnMap, TArgType arg);

		// Token: 0x06000A71 RID: 2673
		internal abstract TResultType Visit(EntityColumnMap columnMap, TArgType arg);

		// Token: 0x06000A72 RID: 2674
		internal abstract TResultType Visit(SimplePolymorphicColumnMap columnMap, TArgType arg);

		// Token: 0x06000A73 RID: 2675
		internal abstract TResultType Visit(RecordColumnMap columnMap, TArgType arg);

		// Token: 0x06000A74 RID: 2676
		internal abstract TResultType Visit(RefColumnMap columnMap, TArgType arg);

		// Token: 0x06000A75 RID: 2677
		internal abstract TResultType Visit(ScalarColumnMap columnMap, TArgType arg);

		// Token: 0x06000A76 RID: 2678
		internal abstract TResultType Visit(SimpleCollectionColumnMap columnMap, TArgType arg);

		// Token: 0x06000A77 RID: 2679
		internal abstract TResultType Visit(VarRefColumnMap columnMap, TArgType arg);

		// Token: 0x06000A78 RID: 2680
		internal abstract TResultType Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, TArgType arg);
	}
}
