using System;
using System.Collections.Generic;
using System.Data.Query.PlanCompiler;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000AB RID: 171
	internal class ColumnMapCopier : ColumnMapVisitorWithResults<ColumnMap, VarMap>
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x0003650F File Offset: 0x0003470F
		private ColumnMapCopier()
		{
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00036517 File Offset: 0x00034717
		internal static ColumnMap Copy(ColumnMap columnMap, VarMap replacementVarMap)
		{
			return columnMap.Accept<ColumnMap, VarMap>(ColumnMapCopier.Instance, replacementVarMap);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00036528 File Offset: 0x00034728
		private static Var GetReplacementVar(Var originalVar, VarMap replacementVarMap)
		{
			Var var = originalVar;
			while (replacementVarMap.TryGetValue(var, out originalVar) && originalVar != var)
			{
				var = originalVar;
			}
			return var;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0003654C File Offset: 0x0003474C
		internal TListType[] VisitList<TListType>(TListType[] tList, VarMap replacementVarMap) where TListType : ColumnMap
		{
			TListType[] array = new TListType[tList.Length];
			for (int i = 0; i < tList.Length; i++)
			{
				array[i] = (TListType)((object)tList[i].Accept<ColumnMap, VarMap>(this, replacementVarMap));
			}
			return array;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00036590 File Offset: 0x00034790
		protected override EntityIdentity VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, VarMap replacementVarMap)
		{
			SimpleColumnMap entitySetColumn = (SimpleColumnMap)entityIdentity.EntitySetColumnMap.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap[] keyColumns = this.VisitList<SimpleColumnMap>(entityIdentity.Keys, replacementVarMap);
			return new DiscriminatedEntityIdentity(entitySetColumn, entityIdentity.EntitySetMap, keyColumns);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000365CC File Offset: 0x000347CC
		protected override EntityIdentity VisitEntityIdentity(SimpleEntityIdentity entityIdentity, VarMap replacementVarMap)
		{
			SimpleColumnMap[] keyColumns = this.VisitList<SimpleColumnMap>(entityIdentity.Keys, replacementVarMap);
			return new SimpleEntityIdentity(entityIdentity.EntitySet, keyColumns);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000365F4 File Offset: 0x000347F4
		internal override ColumnMap Visit(ComplexTypeColumnMap columnMap, VarMap replacementVarMap)
		{
			SimpleColumnMap simpleColumnMap = columnMap.NullSentinel;
			if (simpleColumnMap != null)
			{
				simpleColumnMap = (SimpleColumnMap)simpleColumnMap.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			}
			ColumnMap[] properties = this.VisitList<ColumnMap>(columnMap.Properties, replacementVarMap);
			return new ComplexTypeColumnMap(columnMap.Type, columnMap.Name, properties, simpleColumnMap);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0003663C File Offset: 0x0003483C
		internal override ColumnMap Visit(DiscriminatedCollectionColumnMap columnMap, VarMap replacementVarMap)
		{
			ColumnMap elementMap = columnMap.Element.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap discriminator = (SimpleColumnMap)columnMap.Discriminator.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap[] keys = this.VisitList<SimpleColumnMap>(columnMap.Keys, replacementVarMap);
			SimpleColumnMap[] foreignKeys = this.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, replacementVarMap);
			return new DiscriminatedCollectionColumnMap(columnMap.Type, columnMap.Name, elementMap, keys, foreignKeys, discriminator, columnMap.DiscriminatorValue);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000366A4 File Offset: 0x000348A4
		internal override ColumnMap Visit(EntityColumnMap columnMap, VarMap replacementVarMap)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, replacementVarMap);
			ColumnMap[] properties = this.VisitList<ColumnMap>(columnMap.Properties, replacementVarMap);
			return new EntityColumnMap(columnMap.Type, columnMap.Name, properties, entityIdentity);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000366E0 File Offset: 0x000348E0
		internal override ColumnMap Visit(SimplePolymorphicColumnMap columnMap, VarMap replacementVarMap)
		{
			SimpleColumnMap typeDiscriminator = (SimpleColumnMap)columnMap.TypeDiscriminator.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			Dictionary<object, TypedColumnMap> dictionary = new Dictionary<object, TypedColumnMap>(columnMap.TypeChoices.Comparer);
			foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
			{
				TypedColumnMap value = (TypedColumnMap)keyValuePair.Value.Accept<ColumnMap, VarMap>(this, replacementVarMap);
				dictionary[keyValuePair.Key] = value;
			}
			ColumnMap[] baseTypeColumns = this.VisitList<ColumnMap>(columnMap.Properties, replacementVarMap);
			return new SimplePolymorphicColumnMap(columnMap.Type, columnMap.Name, baseTypeColumns, typeDiscriminator, dictionary);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00036798 File Offset: 0x00034998
		internal override ColumnMap Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, VarMap replacementVarMap)
		{
			PlanCompiler.Assert(false, "unexpected MultipleDiscriminatorPolymorphicColumnMap in ColumnMapCopier");
			return null;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000367A8 File Offset: 0x000349A8
		internal override ColumnMap Visit(RecordColumnMap columnMap, VarMap replacementVarMap)
		{
			SimpleColumnMap simpleColumnMap = columnMap.NullSentinel;
			if (simpleColumnMap != null)
			{
				simpleColumnMap = (SimpleColumnMap)simpleColumnMap.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			}
			ColumnMap[] properties = this.VisitList<ColumnMap>(columnMap.Properties, replacementVarMap);
			return new RecordColumnMap(columnMap.Type, columnMap.Name, properties, simpleColumnMap);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000367F0 File Offset: 0x000349F0
		internal override ColumnMap Visit(RefColumnMap columnMap, VarMap replacementVarMap)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, replacementVarMap);
			return new RefColumnMap(columnMap.Type, columnMap.Name, entityIdentity);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0003681D File Offset: 0x00034A1D
		internal override ColumnMap Visit(ScalarColumnMap columnMap, VarMap replacementVarMap)
		{
			return new ScalarColumnMap(columnMap.Type, columnMap.Name, columnMap.CommandId, columnMap.ColumnPos);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0003683C File Offset: 0x00034A3C
		internal override ColumnMap Visit(SimpleCollectionColumnMap columnMap, VarMap replacementVarMap)
		{
			ColumnMap elementMap = columnMap.Element.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap[] keys = this.VisitList<SimpleColumnMap>(columnMap.Keys, replacementVarMap);
			SimpleColumnMap[] foreignKeys = this.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, replacementVarMap);
			return new SimpleCollectionColumnMap(columnMap.Type, columnMap.Name, elementMap, keys, foreignKeys);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00036888 File Offset: 0x00034A88
		internal override ColumnMap Visit(VarRefColumnMap columnMap, VarMap replacementVarMap)
		{
			Var replacementVar = ColumnMapCopier.GetReplacementVar(columnMap.Var, replacementVarMap);
			return new VarRefColumnMap(columnMap.Type, columnMap.Name, replacementVar);
		}

		// Token: 0x040008C9 RID: 2249
		private static ColumnMapCopier Instance = new ColumnMapCopier();
	}
}
