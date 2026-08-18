using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000632 RID: 1586
	internal class ColumnMapCopier : ColumnMapVisitorWithResults<ColumnMap, VarMap>
	{
		// Token: 0x06003DA9 RID: 15785 RVA: 0x0011B6DC File Offset: 0x001198DC
		private ColumnMapCopier()
		{
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x0011B6E4 File Offset: 0x001198E4
		internal static ColumnMap Copy(ColumnMap columnMap, VarMap replacementVarMap)
		{
			return columnMap.Accept<ColumnMap, VarMap>(ColumnMapCopier._instance, replacementVarMap);
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x0011B6F4 File Offset: 0x001198F4
		private static Var GetReplacementVar(Var originalVar, VarMap replacementVarMap)
		{
			Var var = originalVar;
			while (replacementVarMap.TryGetValue(var, out originalVar) && originalVar != var)
			{
				var = originalVar;
			}
			return var;
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x0011B718 File Offset: 0x00119918
		internal TListType[] VisitList<TListType>(TListType[] tList, VarMap replacementVarMap) where TListType : ColumnMap
		{
			TListType[] array = new TListType[tList.Length];
			for (int i = 0; i < tList.Length; i++)
			{
				array[i] = (TListType)((object)tList[i].Accept<ColumnMap, VarMap>(this, replacementVarMap));
			}
			return array;
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x0011B760 File Offset: 0x00119960
		protected override EntityIdentity VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, VarMap replacementVarMap)
		{
			SimpleColumnMap entitySetColumn = (SimpleColumnMap)entityIdentity.EntitySetColumnMap.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap[] keyColumns = this.VisitList<SimpleColumnMap>(entityIdentity.Keys, replacementVarMap);
			return new DiscriminatedEntityIdentity(entitySetColumn, entityIdentity.EntitySetMap, keyColumns);
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x0011B79C File Offset: 0x0011999C
		protected override EntityIdentity VisitEntityIdentity(SimpleEntityIdentity entityIdentity, VarMap replacementVarMap)
		{
			SimpleColumnMap[] keyColumns = this.VisitList<SimpleColumnMap>(entityIdentity.Keys, replacementVarMap);
			return new SimpleEntityIdentity(entityIdentity.EntitySet, keyColumns);
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x0011B7C4 File Offset: 0x001199C4
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

		// Token: 0x06003DB0 RID: 15792 RVA: 0x0011B80C File Offset: 0x00119A0C
		internal override ColumnMap Visit(DiscriminatedCollectionColumnMap columnMap, VarMap replacementVarMap)
		{
			ColumnMap elementMap = columnMap.Element.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap discriminator = (SimpleColumnMap)columnMap.Discriminator.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap[] keys = this.VisitList<SimpleColumnMap>(columnMap.Keys, replacementVarMap);
			SimpleColumnMap[] foreignKeys = this.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, replacementVarMap);
			return new DiscriminatedCollectionColumnMap(columnMap.Type, columnMap.Name, elementMap, keys, foreignKeys, discriminator, columnMap.DiscriminatorValue);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x0011B874 File Offset: 0x00119A74
		internal override ColumnMap Visit(EntityColumnMap columnMap, VarMap replacementVarMap)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, replacementVarMap);
			ColumnMap[] properties = this.VisitList<ColumnMap>(columnMap.Properties, replacementVarMap);
			return new EntityColumnMap(columnMap.Type, columnMap.Name, properties, entityIdentity);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x0011B8B0 File Offset: 0x00119AB0
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

		// Token: 0x06003DB3 RID: 15795 RVA: 0x0011B968 File Offset: 0x00119B68
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ColumnMapCopier")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MultipleDiscriminatorPolymorphicColumnMap")]
		internal override ColumnMap Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, VarMap replacementVarMap)
		{
			PlanCompiler.Assert(false, "unexpected MultipleDiscriminatorPolymorphicColumnMap in ColumnMapCopier");
			return null;
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x0011B978 File Offset: 0x00119B78
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

		// Token: 0x06003DB5 RID: 15797 RVA: 0x0011B9C0 File Offset: 0x00119BC0
		internal override ColumnMap Visit(RefColumnMap columnMap, VarMap replacementVarMap)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, replacementVarMap);
			return new RefColumnMap(columnMap.Type, columnMap.Name, entityIdentity);
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x0011B9ED File Offset: 0x00119BED
		internal override ColumnMap Visit(ScalarColumnMap columnMap, VarMap replacementVarMap)
		{
			return new ScalarColumnMap(columnMap.Type, columnMap.Name, columnMap.CommandId, columnMap.ColumnPos);
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x0011BA0C File Offset: 0x00119C0C
		internal override ColumnMap Visit(SimpleCollectionColumnMap columnMap, VarMap replacementVarMap)
		{
			ColumnMap elementMap = columnMap.Element.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap[] keys = this.VisitList<SimpleColumnMap>(columnMap.Keys, replacementVarMap);
			SimpleColumnMap[] foreignKeys = this.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, replacementVarMap);
			return new SimpleCollectionColumnMap(columnMap.Type, columnMap.Name, elementMap, keys, foreignKeys);
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x0011BA58 File Offset: 0x00119C58
		internal override ColumnMap Visit(VarRefColumnMap columnMap, VarMap replacementVarMap)
		{
			Var replacementVar = ColumnMapCopier.GetReplacementVar(columnMap.Var, replacementVarMap);
			return new VarRefColumnMap(columnMap.Type, columnMap.Name, replacementVar);
		}

		// Token: 0x0400174C RID: 5964
		private static readonly ColumnMapCopier _instance = new ColumnMapCopier();
	}
}
