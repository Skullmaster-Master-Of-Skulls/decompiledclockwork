using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping
{
	// Token: 0x02000222 RID: 546
	internal sealed class FunctionImportMappingNonComposable : FunctionImportMapping
	{
		// Token: 0x060023A0 RID: 9120 RVA: 0x000801BC File Offset: 0x0007E3BC
		internal FunctionImportMappingNonComposable(EdmFunction functionImport, EdmFunction targetFunction, List<List<FunctionImportStructuralTypeMapping>> structuralTypeMappingsList, ItemCollection itemCollection) : base(functionImport, targetFunction)
		{
			EntityUtil.CheckArgumentNull<List<List<FunctionImportStructuralTypeMapping>>>(structuralTypeMappingsList, "structuralTypeMappingsList");
			EntityUtil.CheckArgumentNull<ItemCollection>(itemCollection, "itemCollection");
			if (structuralTypeMappingsList.Count == 0)
			{
				this.ResultMappings = new ReadOnlyCollection<FunctionImportStructuralTypeMappingKB>(new FunctionImportStructuralTypeMappingKB[]
				{
					new FunctionImportStructuralTypeMappingKB(new List<FunctionImportStructuralTypeMapping>(), itemCollection)
				});
				this.noExplicitResultMappings = true;
				return;
			}
			this.ResultMappings = new ReadOnlyCollection<FunctionImportStructuralTypeMappingKB>((from structuralTypeMappings in EntityUtil.CheckArgumentNull<List<List<FunctionImportStructuralTypeMapping>>>(structuralTypeMappingsList, "structuralTypeMappingsList")
			select new FunctionImportStructuralTypeMappingKB(EntityUtil.CheckArgumentNull<List<FunctionImportStructuralTypeMapping>>(structuralTypeMappings, "structuralTypeMappings"), itemCollection)).ToArray<FunctionImportStructuralTypeMappingKB>());
			this.noExplicitResultMappings = false;
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x00080263 File Offset: 0x0007E463
		internal FunctionImportStructuralTypeMappingKB GetResultMapping(int resultSetIndex)
		{
			if (this.noExplicitResultMappings)
			{
				return this.ResultMappings[0];
			}
			if (this.ResultMappings.Count <= resultSetIndex)
			{
				EntityUtil.ThrowArgumentOutOfRangeException("resultSetIndex");
			}
			return this.ResultMappings[resultSetIndex];
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000802A0 File Offset: 0x0007E4A0
		internal IList<string> GetDiscriminatorColumns(int resultSetIndex)
		{
			FunctionImportStructuralTypeMappingKB resultMapping = this.GetResultMapping(resultSetIndex);
			return resultMapping.DiscriminatorColumns;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000802BC File Offset: 0x0007E4BC
		internal EntityType Discriminate(object[] discriminatorValues, int resultSetIndex)
		{
			FunctionImportStructuralTypeMappingKB resultMapping = this.GetResultMapping(resultSetIndex);
			BitArray bitArray = new BitArray(resultMapping.MappedEntityTypes.Count, true);
			foreach (FunctionImportNormalizedEntityTypeMapping functionImportNormalizedEntityTypeMapping in resultMapping.NormalizedEntityTypeMappings)
			{
				bool flag = true;
				ReadOnlyCollection<FunctionImportEntityTypeMappingCondition> columnConditions = functionImportNormalizedEntityTypeMapping.ColumnConditions;
				for (int i = 0; i < columnConditions.Count; i++)
				{
					if (columnConditions[i] != null && !columnConditions[i].ColumnValueMatchesCondition(discriminatorValues[i]))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					bitArray = bitArray.And(functionImportNormalizedEntityTypeMapping.ImpliedEntityTypes);
				}
				else
				{
					bitArray = bitArray.And(functionImportNormalizedEntityTypeMapping.ComplementImpliedEntityTypes);
				}
			}
			EntityType entityType = null;
			for (int j = 0; j < bitArray.Length; j++)
			{
				if (bitArray[j])
				{
					if (entityType != null)
					{
						throw EntityUtil.CommandExecution(Strings.ADP_InvalidDataReaderUnableToDetermineType);
					}
					entityType = resultMapping.MappedEntityTypes[j];
				}
			}
			if (entityType == null)
			{
				throw EntityUtil.CommandExecution(Strings.ADP_InvalidDataReaderUnableToDetermineType);
			}
			return entityType;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000803D4 File Offset: 0x0007E5D4
		internal TypeUsage GetExpectedTargetResultType(MetadataWorkspace workspace, int resultSetIndex)
		{
			FunctionImportStructuralTypeMappingKB resultMapping = this.GetResultMapping(resultSetIndex);
			Dictionary<string, TypeUsage> dictionary = new Dictionary<string, TypeUsage>();
			IEnumerable<StructuralType> enumerable;
			if (resultMapping.NormalizedEntityTypeMappings.Count == 0)
			{
				StructuralType structuralType;
				MetadataHelper.TryGetFunctionImportReturnType<StructuralType>(this.FunctionImport, resultSetIndex, out structuralType);
				enumerable = new StructuralType[]
				{
					structuralType
				};
			}
			else
			{
				enumerable = resultMapping.MappedEntityTypes.Cast<StructuralType>();
			}
			foreach (StructuralType edmType in enumerable)
			{
				foreach (object obj in TypeHelpers.GetAllStructuralMembers(edmType))
				{
					EdmProperty edmProperty = (EdmProperty)obj;
					dictionary[edmProperty.Name] = edmProperty.TypeUsage;
				}
			}
			foreach (string key in this.GetDiscriminatorColumns(resultSetIndex))
			{
				if (!dictionary.ContainsKey(key))
				{
					TypeUsage value = TypeUsage.CreateStringTypeUsage(workspace.GetModelPrimitiveType(PrimitiveTypeKind.String), true, false);
					dictionary.Add(key, value);
				}
			}
			RowType edmType2 = new RowType(from c in dictionary
			select new EdmProperty(c.Key, c.Value));
			return TypeUsage.Create(new CollectionType(TypeUsage.Create(edmType2)));
		}

		// Token: 0x04000FC4 RID: 4036
		private bool noExplicitResultMappings;

		// Token: 0x04000FC5 RID: 4037
		internal readonly ReadOnlyCollection<FunctionImportStructuralTypeMappingKB> ResultMappings;
	}
}
