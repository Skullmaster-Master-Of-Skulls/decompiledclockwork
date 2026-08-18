using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003B9 RID: 953
	public sealed class FunctionImportMappingNonComposable : FunctionImportMapping
	{
		// Token: 0x060022EA RID: 8938 RVA: 0x000A2DC4 File Offset: 0x000A0FC4
		public FunctionImportMappingNonComposable(EdmFunction functionImport, EdmFunction targetFunction, IEnumerable<FunctionImportResultMapping> resultMappings, EntityContainerMapping containerMapping) : base(Check.NotNull<EdmFunction>(functionImport, "functionImport"), Check.NotNull<EdmFunction>(targetFunction, "targetFunction"))
		{
			Check.NotNull<IEnumerable<FunctionImportResultMapping>>(resultMappings, "resultMappings");
			Check.NotNull<EntityContainerMapping>(containerMapping, "containerMapping");
			if (!resultMappings.Any<FunctionImportResultMapping>())
			{
				EdmItemCollection itemCollection = (containerMapping.StorageMappingItemCollection != null) ? containerMapping.StorageMappingItemCollection.EdmItemCollection : new EdmItemCollection(new EdmModel(DataSpace.CSpace, 3.0));
				this._internalResultMappings = new ReadOnlyCollection<FunctionImportStructuralTypeMappingKB>(new FunctionImportStructuralTypeMappingKB[]
				{
					new FunctionImportStructuralTypeMappingKB(new List<FunctionImportStructuralTypeMapping>(), itemCollection)
				});
				this.noExplicitResultMappings = true;
			}
			else
			{
				this._internalResultMappings = new ReadOnlyCollection<FunctionImportStructuralTypeMappingKB>((from resultMapping in resultMappings
				select new FunctionImportStructuralTypeMappingKB(resultMapping.TypeMappings, containerMapping.StorageMappingItemCollection.EdmItemCollection)).ToArray<FunctionImportStructuralTypeMappingKB>());
				this.noExplicitResultMappings = false;
			}
			this._resultMappings = new ReadOnlyCollection<FunctionImportResultMapping>(resultMappings.ToList<FunctionImportResultMapping>());
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000A2ED8 File Offset: 0x000A10D8
		internal FunctionImportMappingNonComposable(EdmFunction functionImport, EdmFunction targetFunction, List<List<FunctionImportStructuralTypeMapping>> structuralTypeMappingsList, ItemCollection itemCollection) : base(functionImport, targetFunction)
		{
			if (structuralTypeMappingsList.Count == 0)
			{
				this._internalResultMappings = new ReadOnlyCollection<FunctionImportStructuralTypeMappingKB>(new FunctionImportStructuralTypeMappingKB[]
				{
					new FunctionImportStructuralTypeMappingKB(new List<FunctionImportStructuralTypeMapping>(), itemCollection)
				});
				this.noExplicitResultMappings = true;
				return;
			}
			this._internalResultMappings = new ReadOnlyCollection<FunctionImportStructuralTypeMappingKB>((from structuralTypeMappings in structuralTypeMappingsList
			select new FunctionImportStructuralTypeMappingKB(structuralTypeMappings, itemCollection)).ToArray<FunctionImportStructuralTypeMappingKB>());
			this.noExplicitResultMappings = false;
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x000A2F61 File Offset: 0x000A1161
		internal ReadOnlyCollection<FunctionImportStructuralTypeMappingKB> InternalResultMappings
		{
			get
			{
				return this._internalResultMappings;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x000A2F69 File Offset: 0x000A1169
		public ReadOnlyCollection<FunctionImportResultMapping> ResultMappings
		{
			get
			{
				return this._resultMappings;
			}
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000A2F71 File Offset: 0x000A1171
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._resultMappings);
			base.SetReadOnly();
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000A2F84 File Offset: 0x000A1184
		internal FunctionImportStructuralTypeMappingKB GetResultMapping(int resultSetIndex)
		{
			if (this.noExplicitResultMappings)
			{
				return this.InternalResultMappings[0];
			}
			if (this.InternalResultMappings.Count <= resultSetIndex)
			{
				throw new ArgumentOutOfRangeException("resultSetIndex");
			}
			return this.InternalResultMappings[resultSetIndex];
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000A2FC0 File Offset: 0x000A11C0
		internal IList<string> GetDiscriminatorColumns(int resultSetIndex)
		{
			FunctionImportStructuralTypeMappingKB resultMapping = this.GetResultMapping(resultSetIndex);
			return resultMapping.DiscriminatorColumns;
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000A2FDC File Offset: 0x000A11DC
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
						throw new EntityCommandExecutionException(Strings.ADP_InvalidDataReaderUnableToDetermineType);
					}
					entityType = resultMapping.MappedEntityTypes[j];
				}
			}
			if (entityType == null)
			{
				throw new EntityCommandExecutionException(Strings.ADP_InvalidDataReaderUnableToDetermineType);
			}
			return entityType;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000A310C File Offset: 0x000A130C
		internal TypeUsage GetExpectedTargetResultType(int resultSetIndex)
		{
			FunctionImportStructuralTypeMappingKB resultMapping = this.GetResultMapping(resultSetIndex);
			Dictionary<string, TypeUsage> dictionary = new Dictionary<string, TypeUsage>();
			IEnumerable<StructuralType> enumerable;
			if (resultMapping.NormalizedEntityTypeMappings.Count == 0)
			{
				StructuralType structuralType;
				MetadataHelper.TryGetFunctionImportReturnType<StructuralType>(base.FunctionImport, resultSetIndex, out structuralType);
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
					TypeUsage value = TypeUsage.CreateStringTypeUsage(MetadataWorkspace.GetModelPrimitiveType(PrimitiveTypeKind.String), true, false);
					dictionary.Add(key, value);
				}
			}
			RowType edmType2 = new RowType(from c in dictionary
			select new EdmProperty(c.Key, c.Value));
			return TypeUsage.Create(new CollectionType(TypeUsage.Create(edmType2)));
		}

		// Token: 0x04000C3C RID: 3132
		private readonly ReadOnlyCollection<FunctionImportResultMapping> _resultMappings;

		// Token: 0x04000C3D RID: 3133
		private readonly bool noExplicitResultMappings;

		// Token: 0x04000C3E RID: 3134
		private readonly ReadOnlyCollection<FunctionImportStructuralTypeMappingKB> _internalResultMappings;
	}
}
