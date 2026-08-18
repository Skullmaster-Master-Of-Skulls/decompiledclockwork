using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Data.Query.PlanCompiler;
using System.Data.Query.ResultAssembly;
using System.Linq;
using System.Text;

namespace System.Data.EntityClient
{
	// Token: 0x0200011E RID: 286
	internal sealed class EntityCommandDefinition : DbCommandDefinition
	{
		// Token: 0x06000F1F RID: 3871 RVA: 0x0003FB24 File Offset: 0x0003DD24
		internal EntityCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
		{
			EntityUtil.CheckArgumentNull<DbProviderFactory>(storeProviderFactory, "storeProviderFactory");
			EntityUtil.CheckArgumentNull<DbCommandTree>(commandTree, "commandTree");
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(storeProviderFactory);
			try
			{
				if (commandTree.CommandTreeKind == DbCommandTreeKind.Query)
				{
					List<ProviderCommandInfo> list = new List<ProviderCommandInfo>();
					ColumnMap columnMap;
					int fieldsRequired;
					PlanCompiler.Compile(commandTree, out list, out columnMap, out fieldsRequired, out this._entitySets);
					this._columnMapGenerators = new EntityCommandDefinition.IColumnMapGenerator[]
					{
						new EntityCommandDefinition.ConstantColumnMapGenerator(columnMap, fieldsRequired)
					};
					this._mappedCommandDefinitions = new List<DbCommandDefinition>(list.Count);
					using (List<ProviderCommandInfo>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ProviderCommandInfo providerCommandInfo = enumerator.Current;
							DbCommandDefinition dbCommandDefinition = providerServices.CreateCommandDefinition(providerCommandInfo.CommandTree);
							if (dbCommandDefinition == null)
							{
								throw EntityUtil.ProviderIncompatible(Strings.ProviderReturnedNullForCreateCommandDefinition);
							}
							this._mappedCommandDefinitions.Add(dbCommandDefinition);
						}
						goto IL_21C;
					}
				}
				DbFunctionCommandTree dbFunctionCommandTree = (DbFunctionCommandTree)commandTree;
				FunctionImportMappingNonComposable targetFunctionMapping = EntityCommandDefinition.GetTargetFunctionMapping(dbFunctionCommandTree);
				IList<FunctionParameter> returnParameters = dbFunctionCommandTree.EdmFunction.ReturnParameters;
				int num = (returnParameters.Count > 1) ? returnParameters.Count : 1;
				this._columnMapGenerators = new EntityCommandDefinition.IColumnMapGenerator[num];
				TypeUsage resultType = this.DetermineStoreResultType(dbFunctionCommandTree.MetadataWorkspace, targetFunctionMapping, 0, out this._columnMapGenerators[0]);
				for (int i = 1; i < num; i++)
				{
					this.DetermineStoreResultType(dbFunctionCommandTree.MetadataWorkspace, targetFunctionMapping, i, out this._columnMapGenerators[i]);
				}
				List<KeyValuePair<string, TypeUsage>> list2 = new List<KeyValuePair<string, TypeUsage>>();
				foreach (KeyValuePair<string, TypeUsage> item in dbFunctionCommandTree.Parameters)
				{
					list2.Add(item);
				}
				DbFunctionCommandTree commandTree2 = new DbFunctionCommandTree(dbFunctionCommandTree.MetadataWorkspace, DataSpace.SSpace, targetFunctionMapping.TargetFunction, resultType, list2);
				DbCommandDefinition item2 = providerServices.CreateCommandDefinition(commandTree2);
				this._mappedCommandDefinitions = new List<DbCommandDefinition>(1)
				{
					item2
				};
				EntitySet entitySet = targetFunctionMapping.FunctionImport.EntitySets.FirstOrDefault<EntitySet>();
				if (entitySet != null)
				{
					this._entitySets = new Set<EntitySet>();
					this._entitySets.Add(targetFunctionMapping.FunctionImport.EntitySets.FirstOrDefault<EntitySet>());
					this._entitySets.MakeReadOnly();
				}
				IL_21C:
				List<EntityParameter> list3 = new List<EntityParameter>();
				foreach (KeyValuePair<string, TypeUsage> queryParameter in commandTree.Parameters)
				{
					EntityParameter item3 = EntityCommandDefinition.CreateEntityParameterFromQueryParameter(queryParameter);
					list3.Add(item3);
				}
				this._parameters = new ReadOnlyCollection<EntityParameter>(list3);
			}
			catch (EntityCommandCompilationException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.CommandCompilation(Strings.EntityClient_CommandDefinitionPreparationFailed, ex);
				}
				throw;
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003FE3C File Offset: 0x0003E03C
		private TypeUsage DetermineStoreResultType(MetadataWorkspace workspace, FunctionImportMappingNonComposable mapping, int resultSetIndex, out EntityCommandDefinition.IColumnMapGenerator columnMapGenerator)
		{
			EdmFunction functionImport = mapping.FunctionImport;
			StructuralType structuralType;
			TypeUsage typeUsage;
			if (MetadataHelper.TryGetFunctionImportReturnType<StructuralType>(functionImport, resultSetIndex, out structuralType))
			{
				this.ValidateEdmResultType(structuralType, functionImport);
				EntitySet entitySet = (functionImport.EntitySets.Count > resultSetIndex) ? functionImport.EntitySets[resultSetIndex] : null;
				columnMapGenerator = new EntityCommandDefinition.FunctionColumnMapGenerator(mapping, resultSetIndex, entitySet, structuralType);
				typeUsage = mapping.GetExpectedTargetResultType(workspace, resultSetIndex);
			}
			else
			{
				FunctionParameter returnParameter = MetadataHelper.GetReturnParameter(functionImport, resultSetIndex);
				if (returnParameter != null && returnParameter.TypeUsage != null)
				{
					typeUsage = returnParameter.TypeUsage;
					TypeUsage typeUsage2 = ((CollectionType)typeUsage.EdmType).TypeUsage;
					ScalarColumnMap elementMap = new ScalarColumnMap(typeUsage2, string.Empty, 0, 0);
					SimpleCollectionColumnMap columnMap = new SimpleCollectionColumnMap(typeUsage, string.Empty, elementMap, null, null);
					columnMapGenerator = new EntityCommandDefinition.ConstantColumnMapGenerator(columnMap, 1);
				}
				else
				{
					typeUsage = null;
					columnMapGenerator = new EntityCommandDefinition.ConstantColumnMapGenerator(null, 0);
				}
			}
			return typeUsage;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0003FF04 File Offset: 0x0003E104
		private void ValidateEdmResultType(EdmType resultType, EdmFunction functionImport)
		{
			if (Helper.IsComplexType(resultType))
			{
				ComplexType complexType = resultType as ComplexType;
				foreach (EdmProperty edmProperty in complexType.Properties)
				{
					if (edmProperty.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType)
					{
						throw new NotSupportedException(Strings.ComplexTypeAsReturnTypeAndNestedComplexProperty(edmProperty.Name, complexType.Name, functionImport.FullName));
					}
				}
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0003FF90 File Offset: 0x0003E190
		private static FunctionImportMappingNonComposable GetTargetFunctionMapping(DbFunctionCommandTree functionCommandTree)
		{
			FunctionImportMapping functionImportMapping;
			if (!functionCommandTree.MetadataWorkspace.TryGetFunctionImportMapping(functionCommandTree.EdmFunction, out functionImportMapping))
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_UnmappedFunctionImport(functionCommandTree.EdmFunction.FullName));
			}
			return (FunctionImportMappingNonComposable)functionImportMapping;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0003FFCE File Offset: 0x0003E1CE
		public override DbCommand CreateCommand()
		{
			return new EntityCommand(this);
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0003FFD8 File Offset: 0x0003E1D8
		internal IEnumerable<string> MappedCommands
		{
			get
			{
				List<string> list = new List<string>();
				foreach (DbCommandDefinition dbCommandDefinition in this._mappedCommandDefinitions)
				{
					DbCommand dbCommand = dbCommandDefinition.CreateCommand();
					list.Add(dbCommand.CommandText);
				}
				return list;
			}
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00040040 File Offset: 0x0003E240
		internal ColumnMap CreateColumnMap(DbDataReader storeDataReader)
		{
			return this.CreateColumnMap(storeDataReader, 0);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0004004A File Offset: 0x0003E24A
		internal ColumnMap CreateColumnMap(DbDataReader storeDataReader, int resultSetIndex)
		{
			return this._columnMapGenerators[resultSetIndex].CreateColumnMap(storeDataReader);
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0004005A File Offset: 0x0003E25A
		internal IEnumerable<EntityParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00040062 File Offset: 0x0003E262
		internal Set<EntitySet> EntitySets
		{
			get
			{
				return this._entitySets;
			}
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0004006C File Offset: 0x0003E26C
		private static EntityParameter CreateEntityParameterFromQueryParameter(KeyValuePair<string, TypeUsage> queryParameter)
		{
			EntityParameter entityParameter = new EntityParameter();
			entityParameter.ParameterName = queryParameter.Key;
			EntityCommandDefinition.PopulateParameterFromTypeUsage(entityParameter, queryParameter.Value, false);
			return entityParameter;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0004009C File Offset: 0x0003E29C
		internal static void PopulateParameterFromTypeUsage(EntityParameter parameter, TypeUsage type, bool isOutParam)
		{
			if (type != null)
			{
				PrimitiveTypeKind primitiveTypeKind;
				if (Helper.IsEnumType(type.EdmType))
				{
					type = TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(type.EdmType));
				}
				else if (Helper.IsSpatialType(type, out primitiveTypeKind))
				{
					parameter.EdmType = EdmProviderManifest.Instance.GetPrimitiveType(primitiveTypeKind);
				}
			}
			DbCommandDefinition.PopulateParameterFromTypeUsage(parameter, type, isOutParam);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x000400F0 File Offset: 0x0003E2F0
		internal DbDataReader Execute(EntityCommand entityCommand, CommandBehavior behavior)
		{
			if (CommandBehavior.SequentialAccess != (behavior & CommandBehavior.SequentialAccess))
			{
				throw EntityUtil.MustUseSequentialAccess();
			}
			DbDataReader dbDataReader = this.ExecuteStoreCommands(entityCommand, behavior);
			DbDataReader result = null;
			if (dbDataReader != null)
			{
				try
				{
					ColumnMap columnMap = this.CreateColumnMap(dbDataReader, 0);
					if (columnMap == null)
					{
						CommandHelper.ConsumeReader(dbDataReader);
						result = dbDataReader;
					}
					else
					{
						result = BridgeDataReader.Create(dbDataReader, columnMap, entityCommand.Connection.GetMetadataWorkspace(), this.GetNextResultColumnMaps(dbDataReader));
					}
				}
				catch
				{
					dbDataReader.Dispose();
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00040168 File Offset: 0x0003E368
		private IEnumerable<ColumnMap> GetNextResultColumnMaps(DbDataReader storeDataReader)
		{
			int num;
			for (int i = 1; i < this._columnMapGenerators.Length; i = num)
			{
				yield return this.CreateColumnMap(storeDataReader, i);
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00040180 File Offset: 0x0003E380
		internal DbDataReader ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
		{
			if (1 != this._mappedCommandDefinitions.Count)
			{
				throw EntityUtil.NotSupported("MARS");
			}
			EntityTransaction entityTransaction = CommandHelper.GetEntityTransaction(entityCommand);
			DbCommandDefinition dbCommandDefinition = this._mappedCommandDefinitions[0];
			DbCommand dbCommand = dbCommandDefinition.CreateCommand();
			CommandHelper.SetStoreProviderCommandState(entityCommand, entityTransaction, dbCommand);
			bool flag = false;
			if (dbCommand.Parameters != null)
			{
				DbProviderServices providerServices = DbProviderServices.GetProviderServices(entityCommand.Connection.StoreProviderFactory);
				foreach (object obj in dbCommand.Parameters)
				{
					DbParameter dbParameter = (DbParameter)obj;
					int num = entityCommand.Parameters.IndexOf(dbParameter.ParameterName);
					if (-1 != num)
					{
						EntityParameter entityParameter = entityCommand.Parameters[num];
						EntityCommandDefinition.SyncParameterProperties(entityParameter, dbParameter, providerServices);
						if (dbParameter.Direction != ParameterDirection.Input)
						{
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				entityCommand.SetStoreProviderCommand(dbCommand);
			}
			DbDataReader result = null;
			try
			{
				result = dbCommand.ExecuteReader(behavior & ~CommandBehavior.SequentialAccess);
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.CommandExecution(Strings.EntityClient_CommandDefinitionExecutionFailed, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x000402B8 File Offset: 0x0003E4B8
		private static void SyncParameterProperties(EntityParameter entityParameter, DbParameter storeParameter, DbProviderServices storeProviderServices)
		{
			TypeUsage primitiveTypeUsageForScalar = TypeHelpers.GetPrimitiveTypeUsageForScalar(entityParameter.GetTypeUsage());
			storeProviderServices.SetParameterValue(storeParameter, primitiveTypeUsageForScalar, entityParameter.Value);
			if (entityParameter.IsDirectionSpecified)
			{
				storeParameter.Direction = entityParameter.Direction;
			}
			if (entityParameter.IsIsNullableSpecified)
			{
				storeParameter.IsNullable = entityParameter.IsNullable;
			}
			if (entityParameter.IsSizeSpecified)
			{
				storeParameter.Size = entityParameter.Size;
			}
			if (entityParameter.IsPrecisionSpecified)
			{
				((IDbDataParameter)storeParameter).Precision = entityParameter.Precision;
			}
			if (entityParameter.IsScaleSpecified)
			{
				((IDbDataParameter)storeParameter).Scale = entityParameter.Scale;
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00040348 File Offset: 0x0003E548
		internal string ToTraceString()
		{
			if (this._mappedCommandDefinitions == null)
			{
				return string.Empty;
			}
			if (this._mappedCommandDefinitions.Count == 1)
			{
				return this._mappedCommandDefinitions[0].CreateCommand().CommandText;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DbCommandDefinition dbCommandDefinition in this._mappedCommandDefinitions)
			{
				DbCommand dbCommand = dbCommandDefinition.CreateCommand();
				stringBuilder.Append(dbCommand.CommandText);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000A05 RID: 2565
		private readonly List<DbCommandDefinition> _mappedCommandDefinitions;

		// Token: 0x04000A06 RID: 2566
		private readonly EntityCommandDefinition.IColumnMapGenerator[] _columnMapGenerators;

		// Token: 0x04000A07 RID: 2567
		private readonly ReadOnlyCollection<EntityParameter> _parameters;

		// Token: 0x04000A08 RID: 2568
		private readonly Set<EntitySet> _entitySets;

		// Token: 0x0200049A RID: 1178
		private interface IColumnMapGenerator
		{
			// Token: 0x06003C14 RID: 15380
			ColumnMap CreateColumnMap(DbDataReader reader);
		}

		// Token: 0x0200049B RID: 1179
		private sealed class ConstantColumnMapGenerator : EntityCommandDefinition.IColumnMapGenerator
		{
			// Token: 0x06003C15 RID: 15381 RVA: 0x000E2624 File Offset: 0x000E0824
			internal ConstantColumnMapGenerator(ColumnMap columnMap, int fieldsRequired)
			{
				this._columnMap = columnMap;
				this._fieldsRequired = fieldsRequired;
			}

			// Token: 0x06003C16 RID: 15382 RVA: 0x000E263A File Offset: 0x000E083A
			ColumnMap EntityCommandDefinition.IColumnMapGenerator.CreateColumnMap(DbDataReader reader)
			{
				if (reader != null && reader.FieldCount < this._fieldsRequired)
				{
					throw EntityUtil.CommandExecution(Strings.EntityClient_TooFewColumns);
				}
				return this._columnMap;
			}

			// Token: 0x04001A18 RID: 6680
			private readonly ColumnMap _columnMap;

			// Token: 0x04001A19 RID: 6681
			private readonly int _fieldsRequired;
		}

		// Token: 0x0200049C RID: 1180
		private sealed class FunctionColumnMapGenerator : EntityCommandDefinition.IColumnMapGenerator
		{
			// Token: 0x06003C17 RID: 15383 RVA: 0x000E265E File Offset: 0x000E085E
			internal FunctionColumnMapGenerator(FunctionImportMappingNonComposable mapping, int resultSetIndex, EntitySet entitySet, StructuralType baseStructuralType)
			{
				this._mapping = mapping;
				this._entitySet = entitySet;
				this._baseStructuralType = baseStructuralType;
				this._resultSetIndex = resultSetIndex;
			}

			// Token: 0x06003C18 RID: 15384 RVA: 0x000E2683 File Offset: 0x000E0883
			ColumnMap EntityCommandDefinition.IColumnMapGenerator.CreateColumnMap(DbDataReader reader)
			{
				return ColumnMapFactory.CreateFunctionImportStructuralTypeColumnMap(reader, this._mapping, this._resultSetIndex, this._entitySet, this._baseStructuralType);
			}

			// Token: 0x04001A1A RID: 6682
			private readonly FunctionImportMappingNonComposable _mapping;

			// Token: 0x04001A1B RID: 6683
			private readonly EntitySet _entitySet;

			// Token: 0x04001A1C RID: 6684
			private readonly StructuralType _baseStructuralType;

			// Token: 0x04001A1D RID: 6685
			private readonly int _resultSetIndex;
		}
	}
}
