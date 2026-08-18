using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003BF RID: 959
	internal sealed class FunctionImportStructuralTypeMappingKB
	{
		// Token: 0x06002302 RID: 8962 RVA: 0x000A3518 File Offset: 0x000A1718
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal FunctionImportStructuralTypeMappingKB(IEnumerable<FunctionImportStructuralTypeMapping> structuralTypeMappings, ItemCollection itemCollection)
		{
			this.m_itemCollection = itemCollection;
			if (structuralTypeMappings.Count<FunctionImportStructuralTypeMapping>() == 0)
			{
				this.ReturnTypeColumnsRenameMapping = new Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping>();
				this.NormalizedEntityTypeMappings = new ReadOnlyCollection<FunctionImportNormalizedEntityTypeMapping>(new List<FunctionImportNormalizedEntityTypeMapping>());
				this.DiscriminatorColumns = new ReadOnlyCollection<string>(new List<string>());
				this.MappedEntityTypes = new ReadOnlyCollection<EntityType>(new List<EntityType>());
				return;
			}
			IEnumerable<FunctionImportEntityTypeMapping> enumerable = structuralTypeMappings.OfType<FunctionImportEntityTypeMapping>();
			if (enumerable != null && enumerable.FirstOrDefault<FunctionImportEntityTypeMapping>() != null)
			{
				Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>> dictionary = new Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>>();
				Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>> dictionary2 = new Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>>();
				List<FunctionImportNormalizedEntityTypeMapping> list = new List<FunctionImportNormalizedEntityTypeMapping>();
				this.MappedEntityTypes = new ReadOnlyCollection<EntityType>(enumerable.SelectMany((FunctionImportEntityTypeMapping mapping) => mapping.GetMappedEntityTypes(this.m_itemCollection)).Distinct<EntityType>().ToList<EntityType>());
				this.DiscriminatorColumns = new ReadOnlyCollection<string>(enumerable.SelectMany((FunctionImportEntityTypeMapping mapping) => mapping.GetDiscriminatorColumns()).Distinct<string>().ToList<string>());
				this.m_entityTypeLineInfos = new KeyToListMap<EntityType, LineInfo>(EqualityComparer<EntityType>.Default);
				this.m_isTypeOfLineInfos = new KeyToListMap<EntityType, LineInfo>(EqualityComparer<EntityType>.Default);
				foreach (FunctionImportEntityTypeMapping functionImportEntityTypeMapping in enumerable)
				{
					foreach (EntityType key in functionImportEntityTypeMapping.EntityTypes)
					{
						this.m_entityTypeLineInfos.Add(key, functionImportEntityTypeMapping.LineInfo);
					}
					foreach (EntityType key2 in functionImportEntityTypeMapping.IsOfTypeEntityTypes)
					{
						this.m_isTypeOfLineInfos.Add(key2, functionImportEntityTypeMapping.LineInfo);
					}
					Dictionary<string, FunctionImportEntityTypeMappingCondition> dictionary3 = functionImportEntityTypeMapping.Conditions.ToDictionary((FunctionImportEntityTypeMappingCondition condition) => condition.ColumnName, (FunctionImportEntityTypeMappingCondition condition) => condition);
					List<FunctionImportEntityTypeMappingCondition> list2 = new List<FunctionImportEntityTypeMappingCondition>(this.DiscriminatorColumns.Count);
					for (int i = 0; i < this.DiscriminatorColumns.Count; i++)
					{
						string key3 = this.DiscriminatorColumns[i];
						FunctionImportEntityTypeMappingCondition item;
						if (dictionary3.TryGetValue(key3, out item))
						{
							list2.Add(item);
						}
						else
						{
							list2.Add(null);
						}
					}
					bool[] array = new bool[this.MappedEntityTypes.Count];
					Set<EntityType> set = new Set<EntityType>(functionImportEntityTypeMapping.GetMappedEntityTypes(this.m_itemCollection));
					for (int j = 0; j < this.MappedEntityTypes.Count; j++)
					{
						array[j] = set.Contains(this.MappedEntityTypes[j]);
					}
					list.Add(new FunctionImportNormalizedEntityTypeMapping(this, list2, new BitArray(array)));
					foreach (EntityType entityType in functionImportEntityTypeMapping.IsOfTypeEntityTypes)
					{
						if (!dictionary.Keys.Contains(entityType))
						{
							dictionary.Add(entityType, new Collection<FunctionImportReturnTypePropertyMapping>());
						}
						foreach (FunctionImportReturnTypePropertyMapping item2 in functionImportEntityTypeMapping.ColumnsRenameList)
						{
							dictionary[entityType].Add(item2);
						}
					}
					foreach (EntityType entityType2 in functionImportEntityTypeMapping.EntityTypes)
					{
						if (!dictionary2.Keys.Contains(entityType2))
						{
							dictionary2.Add(entityType2, new Collection<FunctionImportReturnTypePropertyMapping>());
						}
						foreach (FunctionImportReturnTypePropertyMapping item3 in functionImportEntityTypeMapping.ColumnsRenameList)
						{
							dictionary2[entityType2].Add(item3);
						}
					}
				}
				this.ReturnTypeColumnsRenameMapping = new FunctionImportReturnTypeEntityTypeColumnsRenameBuilder(dictionary, dictionary2).ColumnRenameMapping;
				this.NormalizedEntityTypeMappings = new ReadOnlyCollection<FunctionImportNormalizedEntityTypeMapping>(list);
				return;
			}
			IEnumerable<FunctionImportComplexTypeMapping> source = structuralTypeMappings.Cast<FunctionImportComplexTypeMapping>();
			this.ReturnTypeColumnsRenameMapping = new Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping>();
			foreach (FunctionImportReturnTypePropertyMapping functionImportReturnTypePropertyMapping in source.First<FunctionImportComplexTypeMapping>().ColumnsRenameList)
			{
				FunctionImportReturnTypeStructuralTypeColumnRenameMapping functionImportReturnTypeStructuralTypeColumnRenameMapping = new FunctionImportReturnTypeStructuralTypeColumnRenameMapping(functionImportReturnTypePropertyMapping.CMember);
				functionImportReturnTypeStructuralTypeColumnRenameMapping.AddRename(new FunctionImportReturnTypeStructuralTypeColumn(functionImportReturnTypePropertyMapping.SColumn, source.First<FunctionImportComplexTypeMapping>().ReturnType, false, functionImportReturnTypePropertyMapping.LineInfo));
				this.ReturnTypeColumnsRenameMapping.Add(functionImportReturnTypePropertyMapping.CMember, functionImportReturnTypeStructuralTypeColumnRenameMapping);
			}
			this.NormalizedEntityTypeMappings = new ReadOnlyCollection<FunctionImportNormalizedEntityTypeMapping>(new List<FunctionImportNormalizedEntityTypeMapping>());
			this.DiscriminatorColumns = new ReadOnlyCollection<string>(new List<string>());
			this.MappedEntityTypes = new ReadOnlyCollection<EntityType>(new List<EntityType>());
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000A3AC0 File Offset: 0x000A1CC0
		internal bool ValidateTypeConditions(bool validateAmbiguity, IList<EdmSchemaError> errors, string sourceLocation)
		{
			KeyToListMap<EntityType, LineInfo> keyToListMap;
			KeyToListMap<EntityType, LineInfo> keyToListMap2;
			this.GetUnreachableTypes(validateAmbiguity, out keyToListMap, out keyToListMap2);
			bool result = true;
			foreach (KeyValuePair<EntityType, List<LineInfo>> keyValuePair in keyToListMap.KeyValuePairs)
			{
				LineInfo lineInfo = keyValuePair.Value.First<LineInfo>();
				string p = StringUtil.ToCommaSeparatedString(from li in keyValuePair.Value
				select li.LineNumber);
				EdmSchemaError item = new EdmSchemaError(Strings.Mapping_FunctionImport_UnreachableType(keyValuePair.Key.FullName, p), 2076, EdmSchemaErrorSeverity.Error, sourceLocation, lineInfo.LineNumber, lineInfo.LinePosition);
				errors.Add(item);
				result = false;
			}
			foreach (KeyValuePair<EntityType, List<LineInfo>> keyValuePair2 in keyToListMap2.KeyValuePairs)
			{
				LineInfo lineInfo2 = keyValuePair2.Value.First<LineInfo>();
				string p2 = StringUtil.ToCommaSeparatedString(from li in keyValuePair2.Value
				select li.LineNumber);
				string p3 = "IsTypeOf(" + keyValuePair2.Key.FullName + ")";
				EdmSchemaError item2 = new EdmSchemaError(Strings.Mapping_FunctionImport_UnreachableIsTypeOf(p3, p2), 2076, EdmSchemaErrorSeverity.Error, sourceLocation, lineInfo2.LineNumber, lineInfo2.LinePosition);
				errors.Add(item2);
				result = false;
			}
			return result;
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x000A3C60 File Offset: 0x000A1E60
		private void GetUnreachableTypes(bool validateAmbiguity, out KeyToListMap<EntityType, LineInfo> unreachableEntityTypes, out KeyToListMap<EntityType, LineInfo> unreachableIsTypeOfs)
		{
			DomainVariable<string, ValueCondition>[] variables = this.ConstructDomainVariables();
			DomainConstraintConversionContext<string, ValueCondition> converter = new DomainConstraintConversionContext<string, ValueCondition>();
			Vertex[] mappingConditions = this.ConvertMappingConditionsToVertices(converter, variables);
			Set<EntityType> reachableTypes = validateAmbiguity ? this.FindUnambiguouslyReachableTypes(converter, mappingConditions) : this.FindReachableTypes(converter, mappingConditions);
			this.CollectUnreachableTypes(reachableTypes, out unreachableEntityTypes, out unreachableIsTypeOfs);
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x000A3CA4 File Offset: 0x000A1EA4
		private DomainVariable<string, ValueCondition>[] ConstructDomainVariables()
		{
			Set<ValueCondition>[] array = new Set<ValueCondition>[this.DiscriminatorColumns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Set<ValueCondition>();
				array[i].Add(ValueCondition.IsOther);
				array[i].Add(ValueCondition.IsNull);
			}
			foreach (FunctionImportNormalizedEntityTypeMapping functionImportNormalizedEntityTypeMapping in this.NormalizedEntityTypeMappings)
			{
				for (int j = 0; j < this.DiscriminatorColumns.Count; j++)
				{
					FunctionImportEntityTypeMappingCondition functionImportEntityTypeMappingCondition = functionImportNormalizedEntityTypeMapping.ColumnConditions[j];
					if (functionImportEntityTypeMappingCondition != null && !functionImportEntityTypeMappingCondition.ConditionValue.IsNotNullCondition)
					{
						array[j].Add(functionImportEntityTypeMappingCondition.ConditionValue);
					}
				}
			}
			DomainVariable<string, ValueCondition>[] array2 = new DomainVariable<string, ValueCondition>[array.Length];
			for (int k = 0; k < array2.Length; k++)
			{
				array2[k] = new DomainVariable<string, ValueCondition>(this.DiscriminatorColumns[k], array[k].MakeReadOnly());
			}
			return array2;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000A3DB4 File Offset: 0x000A1FB4
		private Vertex[] ConvertMappingConditionsToVertices(ConversionContext<DomainConstraint<string, ValueCondition>> converter, DomainVariable<string, ValueCondition>[] variables)
		{
			Vertex[] array = new Vertex[this.NormalizedEntityTypeMappings.Count];
			for (int i = 0; i < array.Length; i++)
			{
				FunctionImportNormalizedEntityTypeMapping functionImportNormalizedEntityTypeMapping = this.NormalizedEntityTypeMappings[i];
				Vertex vertex = Vertex.One;
				for (int j = 0; j < this.DiscriminatorColumns.Count; j++)
				{
					FunctionImportEntityTypeMappingCondition functionImportEntityTypeMappingCondition = functionImportNormalizedEntityTypeMapping.ColumnConditions[j];
					if (functionImportEntityTypeMappingCondition != null)
					{
						ValueCondition conditionValue = functionImportEntityTypeMappingCondition.ConditionValue;
						if (conditionValue.IsNotNullCondition)
						{
							TermExpr<DomainConstraint<string, ValueCondition>> term = new TermExpr<DomainConstraint<string, ValueCondition>>(new DomainConstraint<string, ValueCondition>(variables[j], ValueCondition.IsNull));
							Vertex vertex2 = converter.TranslateTermToVertex(term);
							vertex = converter.Solver.And(vertex, converter.Solver.Not(vertex2));
						}
						else
						{
							TermExpr<DomainConstraint<string, ValueCondition>> term2 = new TermExpr<DomainConstraint<string, ValueCondition>>(new DomainConstraint<string, ValueCondition>(variables[j], conditionValue));
							vertex = converter.Solver.And(vertex, converter.TranslateTermToVertex(term2));
						}
					}
				}
				array[i] = vertex;
			}
			return array;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000A3ED8 File Offset: 0x000A20D8
		private Set<EntityType> FindReachableTypes(DomainConstraintConversionContext<string, ValueCondition> converter, Vertex[] mappingConditions)
		{
			FunctionImportStructuralTypeMappingKB.<>c__DisplayClassd CS$<>8__locals1 = new FunctionImportStructuralTypeMappingKB.<>c__DisplayClassd();
			CS$<>8__locals1.converter = converter;
			Vertex[] array = new Vertex[this.MappedEntityTypes.Count];
			for (int k = 0; k < array.Length; k++)
			{
				Vertex vertex = Vertex.One;
				for (int j = 0; j < this.NormalizedEntityTypeMappings.Count; j++)
				{
					FunctionImportNormalizedEntityTypeMapping functionImportNormalizedEntityTypeMapping = this.NormalizedEntityTypeMappings[j];
					if (functionImportNormalizedEntityTypeMapping.ImpliedEntityTypes[k])
					{
						vertex = CS$<>8__locals1.converter.Solver.And(vertex, mappingConditions[j]);
					}
					else
					{
						vertex = CS$<>8__locals1.converter.Solver.And(vertex, CS$<>8__locals1.converter.Solver.Not(mappingConditions[j]));
					}
				}
				array[k] = vertex;
			}
			Set<EntityType> set = new Set<EntityType>();
			int i;
			for (i = 0; i < array.Length; i++)
			{
				Vertex vertex2 = CS$<>8__locals1.converter.Solver.And(array.Select(delegate(Vertex typeCondition, int ordinal)
				{
					if (ordinal != i)
					{
						return CS$<>8__locals1.converter.Solver.Not(typeCondition);
					}
					return typeCondition;
				}));
				if (!vertex2.IsZero())
				{
					set.Add(this.MappedEntityTypes[i]);
				}
			}
			return set;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x000A4028 File Offset: 0x000A2228
		private Set<EntityType> FindUnambiguouslyReachableTypes(DomainConstraintConversionContext<string, ValueCondition> converter, Vertex[] mappingConditions)
		{
			Vertex[] array = new Vertex[this.MappedEntityTypes.Count];
			for (int i = 0; i < array.Length; i++)
			{
				Vertex vertex = Vertex.One;
				for (int j = 0; j < this.NormalizedEntityTypeMappings.Count; j++)
				{
					FunctionImportNormalizedEntityTypeMapping functionImportNormalizedEntityTypeMapping = this.NormalizedEntityTypeMappings[j];
					if (functionImportNormalizedEntityTypeMapping.ImpliedEntityTypes[i])
					{
						vertex = converter.Solver.And(vertex, mappingConditions[j]);
					}
				}
				array[i] = vertex;
			}
			BitArray bitArray = new BitArray(array.Length, true);
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k].IsZero())
				{
					bitArray[k] = false;
				}
				else
				{
					for (int l = k + 1; l < array.Length; l++)
					{
						if (!converter.Solver.And(array[k], array[l]).IsZero())
						{
							bitArray[k] = false;
							bitArray[l] = false;
						}
					}
				}
			}
			Set<EntityType> set = new Set<EntityType>();
			for (int m = 0; m < array.Length; m++)
			{
				if (bitArray[m])
				{
					set.Add(this.MappedEntityTypes[m]);
				}
			}
			return set;
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000A4154 File Offset: 0x000A2354
		private void CollectUnreachableTypes(Set<EntityType> reachableTypes, out KeyToListMap<EntityType, LineInfo> entityTypes, out KeyToListMap<EntityType, LineInfo> isTypeOfEntityTypes)
		{
			entityTypes = new KeyToListMap<EntityType, LineInfo>(EqualityComparer<EntityType>.Default);
			isTypeOfEntityTypes = new KeyToListMap<EntityType, LineInfo>(EqualityComparer<EntityType>.Default);
			if (reachableTypes.Count == this.MappedEntityTypes.Count)
			{
				return;
			}
			foreach (EntityType entityType in this.m_isTypeOfLineInfos.Keys)
			{
				if (!MetadataHelper.GetTypeAndSubtypesOf(entityType, this.m_itemCollection, false).Cast<EntityType>().Intersect(reachableTypes).Any<EntityType>())
				{
					isTypeOfEntityTypes.AddRange(entityType, this.m_isTypeOfLineInfos.EnumerateValues(entityType));
				}
			}
			foreach (EntityType entityType2 in this.m_entityTypeLineInfos.Keys)
			{
				if (!reachableTypes.Contains(entityType2))
				{
					entityTypes.AddRange(entityType2, this.m_entityTypeLineInfos.EnumerateValues(entityType2));
				}
			}
		}

		// Token: 0x04000C4B RID: 3147
		private readonly ItemCollection m_itemCollection;

		// Token: 0x04000C4C RID: 3148
		private readonly KeyToListMap<EntityType, LineInfo> m_entityTypeLineInfos;

		// Token: 0x04000C4D RID: 3149
		private readonly KeyToListMap<EntityType, LineInfo> m_isTypeOfLineInfos;

		// Token: 0x04000C4E RID: 3150
		internal readonly ReadOnlyCollection<EntityType> MappedEntityTypes;

		// Token: 0x04000C4F RID: 3151
		internal readonly ReadOnlyCollection<string> DiscriminatorColumns;

		// Token: 0x04000C50 RID: 3152
		internal readonly ReadOnlyCollection<FunctionImportNormalizedEntityTypeMapping> NormalizedEntityTypeMappings;

		// Token: 0x04000C51 RID: 3153
		internal readonly Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> ReturnTypeColumnsRenameMapping;
	}
}
