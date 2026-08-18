using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200065D RID: 1629
	internal class ColumnMapTranslator : ColumnMapVisitorWithResults<ColumnMap, ColumnMapTranslatorTranslationDelegate>
	{
		// Token: 0x06003F9E RID: 16286 RVA: 0x00123496 File Offset: 0x00121696
		private ColumnMapTranslator()
		{
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x001234A0 File Offset: 0x001216A0
		private static Var GetReplacementVar(Var originalVar, Dictionary<Var, Var> replacementVarMap)
		{
			Var var = originalVar;
			while (replacementVarMap.TryGetValue(var, out originalVar) && originalVar != var)
			{
				var = originalVar;
			}
			return var;
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x001234C3 File Offset: 0x001216C3
		internal static ColumnMap Translate(ColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			return columnMap.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(ColumnMapTranslator._instance, translationDelegate);
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x00123560 File Offset: 0x00121760
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
						if (Helper.IsEnumType(varRefColumnMap.Type.EdmType) && varRefColumnMap.Type.EdmType != columnMap.Type.EdmType)
						{
							columnMap.Type = varRefColumnMap.Type;
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

		// Token: 0x06003FA2 RID: 16290 RVA: 0x001235E0 File Offset: 0x001217E0
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

		// Token: 0x06003FA3 RID: 16291 RVA: 0x00123698 File Offset: 0x00121898
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

		// Token: 0x06003FA4 RID: 16292 RVA: 0x001236C8 File Offset: 0x001218C8
		private void VisitList<TResultType>(TResultType[] tList, ColumnMapTranslatorTranslationDelegate translationDelegate) where TResultType : ColumnMap
		{
			for (int i = 0; i < tList.Length; i++)
			{
				tList[i] = (TResultType)((object)tList[i].Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate));
			}
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x00123708 File Offset: 0x00121908
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

		// Token: 0x06003FA6 RID: 16294 RVA: 0x00123753 File Offset: 0x00121953
		protected override EntityIdentity VisitEntityIdentity(SimpleEntityIdentity entityIdentity, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			this.VisitList<SimpleColumnMap>(entityIdentity.Keys, translationDelegate);
			return entityIdentity;
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x00123764 File Offset: 0x00121964
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

		// Token: 0x06003FA8 RID: 16296 RVA: 0x001237C0 File Offset: 0x001219C0
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

		// Token: 0x06003FA9 RID: 16297 RVA: 0x00123848 File Offset: 0x00121A48
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

		// Token: 0x06003FAA RID: 16298 RVA: 0x0012389C File Offset: 0x00121A9C
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

		// Token: 0x06003FAB RID: 16299 RVA: 0x00123988 File Offset: 0x00121B88
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MultipleDiscriminatorPolymorphicColumnMap")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ColumnMapTranslator")]
		internal override ColumnMap Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			PlanCompiler.Assert(false, "unexpected MultipleDiscriminatorPolymorphicColumnMap in ColumnMapTranslator");
			return null;
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x00123998 File Offset: 0x00121B98
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

		// Token: 0x06003FAD RID: 16301 RVA: 0x001239F4 File Offset: 0x00121BF4
		internal override ColumnMap Visit(RefColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, translationDelegate);
			if (entityIdentity != columnMap.EntityIdentity)
			{
				columnMap = new RefColumnMap(columnMap.Type, columnMap.Name, entityIdentity);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x00123A33 File Offset: 0x00121C33
		internal override ColumnMap Visit(ScalarColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			return translationDelegate(columnMap);
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x00123A3C File Offset: 0x00121C3C
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

		// Token: 0x06003FB0 RID: 16304 RVA: 0x00123AA1 File Offset: 0x00121CA1
		internal override ColumnMap Visit(VarRefColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			return translationDelegate(columnMap);
		}

		// Token: 0x040017BC RID: 6076
		private static readonly ColumnMapTranslator _instance = new ColumnMapTranslator();
	}
}
