using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000AC RID: 172
	internal abstract class ColumnMapVisitor<TArgType>
	{
		// Token: 0x06000A5D RID: 2653 RVA: 0x000368C0 File Offset: 0x00034AC0
		protected void VisitList<TListType>(TListType[] columnMaps, TArgType arg) where TListType : ColumnMap
		{
			foreach (TListType tlistType in columnMaps)
			{
				tlistType.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000368F4 File Offset: 0x00034AF4
		protected void VisitEntityIdentity(EntityIdentity entityIdentity, TArgType arg)
		{
			DiscriminatedEntityIdentity discriminatedEntityIdentity = entityIdentity as DiscriminatedEntityIdentity;
			if (discriminatedEntityIdentity != null)
			{
				this.VisitEntityIdentity(discriminatedEntityIdentity, arg);
				return;
			}
			this.VisitEntityIdentity((SimpleEntityIdentity)entityIdentity, arg);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00036924 File Offset: 0x00034B24
		protected virtual void VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, TArgType arg)
		{
			entityIdentity.EntitySetColumnMap.Accept<TArgType>(this, arg);
			foreach (SimpleColumnMap simpleColumnMap in entityIdentity.Keys)
			{
				simpleColumnMap.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00036960 File Offset: 0x00034B60
		protected virtual void VisitEntityIdentity(SimpleEntityIdentity entityIdentity, TArgType arg)
		{
			foreach (SimpleColumnMap simpleColumnMap in entityIdentity.Keys)
			{
				simpleColumnMap.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00036990 File Offset: 0x00034B90
		internal virtual void Visit(ComplexTypeColumnMap columnMap, TArgType arg)
		{
			ColumnMap nullSentinel = columnMap.NullSentinel;
			if (nullSentinel != null)
			{
				nullSentinel.Accept<TArgType>(this, arg);
			}
			foreach (ColumnMap columnMap2 in columnMap.Properties)
			{
				columnMap2.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000369D0 File Offset: 0x00034BD0
		internal virtual void Visit(DiscriminatedCollectionColumnMap columnMap, TArgType arg)
		{
			columnMap.Discriminator.Accept<TArgType>(this, arg);
			foreach (SimpleColumnMap simpleColumnMap in columnMap.ForeignKeys)
			{
				simpleColumnMap.Accept<TArgType>(this, arg);
			}
			foreach (SimpleColumnMap simpleColumnMap2 in columnMap.Keys)
			{
				simpleColumnMap2.Accept<TArgType>(this, arg);
			}
			columnMap.Element.Accept<TArgType>(this, arg);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00036A40 File Offset: 0x00034C40
		internal virtual void Visit(EntityColumnMap columnMap, TArgType arg)
		{
			this.VisitEntityIdentity(columnMap.EntityIdentity, arg);
			foreach (ColumnMap columnMap2 in columnMap.Properties)
			{
				columnMap2.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00036A7C File Offset: 0x00034C7C
		internal virtual void Visit(SimplePolymorphicColumnMap columnMap, TArgType arg)
		{
			columnMap.TypeDiscriminator.Accept<TArgType>(this, arg);
			foreach (ColumnMap columnMap2 in columnMap.TypeChoices.Values)
			{
				columnMap2.Accept<TArgType>(this, arg);
			}
			foreach (ColumnMap columnMap3 in columnMap.Properties)
			{
				columnMap3.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00036B08 File Offset: 0x00034D08
		internal virtual void Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, TArgType arg)
		{
			foreach (SimpleColumnMap simpleColumnMap in columnMap.TypeDiscriminators)
			{
				simpleColumnMap.Accept<TArgType>(this, arg);
			}
			foreach (TypedColumnMap typedColumnMap in columnMap.TypeChoices.Values)
			{
				typedColumnMap.Accept<TArgType>(this, arg);
			}
			foreach (ColumnMap columnMap2 in columnMap.Properties)
			{
				columnMap2.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00036BB0 File Offset: 0x00034DB0
		internal virtual void Visit(RecordColumnMap columnMap, TArgType arg)
		{
			ColumnMap nullSentinel = columnMap.NullSentinel;
			if (nullSentinel != null)
			{
				nullSentinel.Accept<TArgType>(this, arg);
			}
			foreach (ColumnMap columnMap2 in columnMap.Properties)
			{
				columnMap2.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00036BF0 File Offset: 0x00034DF0
		internal virtual void Visit(RefColumnMap columnMap, TArgType arg)
		{
			this.VisitEntityIdentity(columnMap.EntityIdentity, arg);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void Visit(ScalarColumnMap columnMap, TArgType arg)
		{
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00036C00 File Offset: 0x00034E00
		internal virtual void Visit(SimpleCollectionColumnMap columnMap, TArgType arg)
		{
			foreach (SimpleColumnMap simpleColumnMap in columnMap.ForeignKeys)
			{
				simpleColumnMap.Accept<TArgType>(this, arg);
			}
			foreach (SimpleColumnMap simpleColumnMap2 in columnMap.Keys)
			{
				simpleColumnMap2.Accept<TArgType>(this, arg);
			}
			columnMap.Element.Accept<TArgType>(this, arg);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void Visit(VarRefColumnMap columnMap, TArgType arg)
		{
		}
	}
}
