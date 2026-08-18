using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020002D8 RID: 728
	internal abstract class ColumnMapVisitor<TArgType>
	{
		// Token: 0x06001971 RID: 6513 RVA: 0x0007F0BC File Offset: 0x0007D2BC
		protected void VisitList<TListType>(TListType[] columnMaps, TArgType arg) where TListType : ColumnMap
		{
			foreach (TListType tlistType in columnMaps)
			{
				tlistType.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0007F0F0 File Offset: 0x0007D2F0
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

		// Token: 0x06001973 RID: 6515 RVA: 0x0007F120 File Offset: 0x0007D320
		protected virtual void VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, TArgType arg)
		{
			entityIdentity.EntitySetColumnMap.Accept<TArgType>(this, arg);
			foreach (SimpleColumnMap simpleColumnMap in entityIdentity.Keys)
			{
				simpleColumnMap.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0007F15C File Offset: 0x0007D35C
		protected virtual void VisitEntityIdentity(SimpleEntityIdentity entityIdentity, TArgType arg)
		{
			foreach (SimpleColumnMap simpleColumnMap in entityIdentity.Keys)
			{
				simpleColumnMap.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0007F18C File Offset: 0x0007D38C
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

		// Token: 0x06001976 RID: 6518 RVA: 0x0007F1CC File Offset: 0x0007D3CC
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

		// Token: 0x06001977 RID: 6519 RVA: 0x0007F240 File Offset: 0x0007D440
		internal virtual void Visit(EntityColumnMap columnMap, TArgType arg)
		{
			this.VisitEntityIdentity(columnMap.EntityIdentity, arg);
			foreach (ColumnMap columnMap2 in columnMap.Properties)
			{
				columnMap2.Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0007F27C File Offset: 0x0007D47C
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

		// Token: 0x06001979 RID: 6521 RVA: 0x0007F308 File Offset: 0x0007D508
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

		// Token: 0x0600197A RID: 6522 RVA: 0x0007F3B4 File Offset: 0x0007D5B4
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

		// Token: 0x0600197B RID: 6523 RVA: 0x0007F3F4 File Offset: 0x0007D5F4
		internal virtual void Visit(RefColumnMap columnMap, TArgType arg)
		{
			this.VisitEntityIdentity(columnMap.EntityIdentity, arg);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0007F403 File Offset: 0x0007D603
		internal virtual void Visit(ScalarColumnMap columnMap, TArgType arg)
		{
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0007F408 File Offset: 0x0007D608
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

		// Token: 0x0600197E RID: 6526 RVA: 0x0007F46C File Offset: 0x0007D66C
		internal virtual void Visit(VarRefColumnMap columnMap, TArgType arg)
		{
		}
	}
}
