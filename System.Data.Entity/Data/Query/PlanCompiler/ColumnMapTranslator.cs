using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000048 RID: 72
	internal class ColumnMapTranslator : ColumnMapVisitorWithResults<ColumnMap, ColumnMapTranslatorTranslationDelegate>
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x00019833 File Offset: 0x00017A33
		private ColumnMapTranslator()
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001983C File Offset: 0x00017A3C
		private static Var GetReplacementVar(Var originalVar, Dictionary<Var, Var> replacementVarMap)
		{
			Var var = originalVar;
			while (replacementVarMap.TryGetValue(var, out originalVar) && originalVar != var)
			{
				var = originalVar;
			}
			return var;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001985F File Offset: 0x00017A5F
		internal static ColumnMap Translate(ColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			return columnMap.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(ColumnMapTranslator.Instance, translationDelegate);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00019870 File Offset: 0x00017A70
		internal static ColumnMap Translate(ColumnMap columnMapToTranslate, Dictionary<Var, ColumnMap> varToColumnMap)
		{
			return ColumnMapTranslator.Translate(columnMapToTranslate, delegate(ColumnMap columnMap)
			{
				VarRefColumnMap varRefColumnMap = columnMap as VarRefColumnMap;
				if (varRefColumnMap != null)
				{
					if (varToColumnMap.TryGetValue(varRefColumnMap.Var, out columnMap))
					{
						if (!columnMap.IsNamed && varRefColumnMap.IsNamed)
						{
							columnMap.Name = varRefColumnMap.Name;
						}
					}
					else
					{
						columnMap = varRefColumnMap;
					}
				}
				return columnMap;
			});
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000198A0 File Offset: 0x00017AA0
		internal static ColumnMap Translate(ColumnMap columnMapToTranslate, Dictionary<Var, Var> varToVarMap)
		{
			return ColumnMapTranslator.Translate(columnMapToTranslate, delegate(ColumnMap columnMap)
			{
				VarRefColumnMap varRefColumnMap = columnMap as VarRefColumnMap;
				if (varRefColumnMap != null)
				{
					Var replacementVar = ColumnMapTranslator.GetReplacementVar(varRefColumnMap.Var, varToVarMap);
					if (varRefColumnMap.Var != replacementVar)
					{
						columnMap = new VarRefColumnMap(varRefColumnMap.Type, varRefColumnMap.Name, replacementVar);
					}
				}
				return columnMap;
			});
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000198D0 File Offset: 0x00017AD0
		internal static ColumnMap Translate(ColumnMap columnMapToTranslate, Dictionary<Var, KeyValuePair<int, int>> varToCommandColumnMap)
		{
			return ColumnMapTranslator.Translate(columnMapToTranslate, delegate(ColumnMap columnMap)
			{
				VarRefColumnMap varRefColumnMap = columnMap as VarRefColumnMap;
				if (varRefColumnMap != null)
				{
					KeyValuePair<int, int> keyValuePair;
					if (!varToCommandColumnMap.TryGetValue(varRefColumnMap.Var, out keyValuePair))
					{
						throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UnknownVar, 1, varRefColumnMap.Var.Id);
					}
					columnMap = new ScalarColumnMap(varRefColumnMap.Type, varRefColumnMap.Name, keyValuePair.Key, keyValuePair.Value);
				}
				if (!columnMap.IsNamed)
				{
					columnMap.Name = "Value";
				}
				return columnMap;
			});
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00019900 File Offset: 0x00017B00
		private void VisitList<TResultType>(TResultType[] tList, ColumnMapTranslatorTranslationDelegate translationDelegate) where TResultType : ColumnMap
		{
			for (int i = 0; i < tList.Length; i++)
			{
				tList[i] = (TResultType)((object)tList[i].Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate));
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001993C File Offset: 0x00017B3C
		protected override EntityIdentity VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			ColumnMap columnMap = entityIdentity.EntitySetColumnMap.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
			this.VisitList<SimpleColumnMap>(entityIdentity.Keys, translationDelegate);
			if (columnMap != entityIdentity.EntitySetColumnMap)
			{
				entityIdentity = new DiscriminatedEntityIdentity((SimpleColumnMap)columnMap, entityIdentity.EntitySetMap, entityIdentity.Keys);
			}
			return entityIdentity;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00019987 File Offset: 0x00017B87
		protected override EntityIdentity VisitEntityIdentity(SimpleEntityIdentity entityIdentity, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			this.VisitList<SimpleColumnMap>(entityIdentity.Keys, translationDelegate);
			return entityIdentity;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00019998 File Offset: 0x00017B98
		internal override ColumnMap Visit(ComplexTypeColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			SimpleColumnMap simpleColumnMap = columnMap.NullSentinel;
			if (simpleColumnMap != null)
			{
				simpleColumnMap = (SimpleColumnMap)translationDelegate(simpleColumnMap);
			}
			this.VisitList<ColumnMap>(columnMap.Properties, translationDelegate);
			if (columnMap.NullSentinel != simpleColumnMap)
			{
				columnMap = new ComplexTypeColumnMap(columnMap.Type, columnMap.Name, columnMap.Properties, simpleColumnMap);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000199F4 File Offset: 0x00017BF4
		internal override ColumnMap Visit(DiscriminatedCollectionColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			ColumnMap columnMap2 = columnMap.Discriminator.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
			this.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, translationDelegate);
			this.VisitList<SimpleColumnMap>(columnMap.Keys, translationDelegate);
			ColumnMap columnMap3 = columnMap.Element.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
			if (columnMap2 != columnMap.Discriminator || columnMap3 != columnMap.Element)
			{
				columnMap = new DiscriminatedCollectionColumnMap(columnMap.Type, columnMap.Name, columnMap3, columnMap.Keys, columnMap.ForeignKeys, (SimpleColumnMap)columnMap2, columnMap.DiscriminatorValue);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00019A7C File Offset: 0x00017C7C
		internal override ColumnMap Visit(EntityColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, translationDelegate);
			this.VisitList<ColumnMap>(columnMap.Properties, translationDelegate);
			if (entityIdentity != columnMap.EntityIdentity)
			{
				columnMap = new EntityColumnMap(columnMap.Type, columnMap.Name, columnMap.Properties, entityIdentity);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00019AD0 File Offset: 0x00017CD0
		internal override ColumnMap Visit(SimplePolymorphicColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			ColumnMap columnMap2 = columnMap.TypeDiscriminator.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
			Dictionary<object, TypedColumnMap> dictionary = columnMap.TypeChoices;
			foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
			{
				TypedColumnMap typedColumnMap = (TypedColumnMap)keyValuePair.Value.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
				if (typedColumnMap != keyValuePair.Value)
				{
					if (dictionary == columnMap.TypeChoices)
					{
						dictionary = new Dictionary<object, TypedColumnMap>(columnMap.TypeChoices);
					}
					dictionary[keyValuePair.Key] = typedColumnMap;
				}
			}
			this.VisitList<ColumnMap>(columnMap.Properties, translationDelegate);
			if (columnMap2 != columnMap.TypeDiscriminator || dictionary != columnMap.TypeChoices)
			{
				columnMap = new SimplePolymorphicColumnMap(columnMap.Type, columnMap.Name, columnMap.Properties, (SimpleColumnMap)columnMap2, dictionary);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00019BBC File Offset: 0x00017DBC
		internal override ColumnMap Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			PlanCompiler.Assert(false, "unexpected MultipleDiscriminatorPolymorphicColumnMap in ColumnMapTranslator");
			return null;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00019BCC File Offset: 0x00017DCC
		internal override ColumnMap Visit(RecordColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			SimpleColumnMap simpleColumnMap = columnMap.NullSentinel;
			if (simpleColumnMap != null)
			{
				simpleColumnMap = (SimpleColumnMap)translationDelegate(simpleColumnMap);
			}
			this.VisitList<ColumnMap>(columnMap.Properties, translationDelegate);
			if (columnMap.NullSentinel != simpleColumnMap)
			{
				columnMap = new RecordColumnMap(columnMap.Type, columnMap.Name, columnMap.Properties, simpleColumnMap);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00019C28 File Offset: 0x00017E28
		internal override ColumnMap Visit(RefColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, translationDelegate);
			if (entityIdentity != columnMap.EntityIdentity)
			{
				columnMap = new RefColumnMap(columnMap.Type, columnMap.Name, entityIdentity);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00019C67 File Offset: 0x00017E67
		internal override ColumnMap Visit(ScalarColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			return translationDelegate(columnMap);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00019C70 File Offset: 0x00017E70
		internal override ColumnMap Visit(SimpleCollectionColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			this.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, translationDelegate);
			this.VisitList<SimpleColumnMap>(columnMap.Keys, translationDelegate);
			ColumnMap columnMap2 = columnMap.Element.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
			if (columnMap2 != columnMap.Element)
			{
				columnMap = new SimpleCollectionColumnMap(columnMap.Type, columnMap.Name, columnMap2, columnMap.Keys, columnMap.ForeignKeys);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00019C67 File Offset: 0x00017E67
		internal override ColumnMap Visit(VarRefColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			return translationDelegate(columnMap);
		}

		// Token: 0x04000765 RID: 1893
		private static ColumnMapTranslator Instance = new ColumnMapTranslator();
	}
}
