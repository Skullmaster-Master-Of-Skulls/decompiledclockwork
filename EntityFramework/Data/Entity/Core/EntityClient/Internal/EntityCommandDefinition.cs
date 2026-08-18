using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Data.Entity.Core.Query.ResultAssembly;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.EntityClient.Internal
{
	// Token: 0x02000345 RID: 837
	internal class EntityCommandDefinition : DbCommandDefinition
	{
		// Token: 0x06001DDF RID: 7647 RVA: 0x0008F96F File Offset: 0x0008DB6F
		internal EntityCommandDefinition()
		{
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0008F978 File Offset: 0x0008DB78
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		internal EntityCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree, DbInterceptionContext interceptionContext, IDbDependencyResolver resolver = null, BridgeDataReaderFactory bridgeDataReaderFactory = null, ColumnMapFactory columnMapFactory = null)
		{
			this._bridgeDataReaderFactory = (bridgeDataReaderFactory ?? new BridgeDataReaderFactory(null));
			this._columnMapFactory = (columnMapFactory ?? new ColumnMapFactory());
			this._storeProviderServices = (((resolver != null) ? resolver.GetService(storeProviderFactory.GetProviderInvariantName()) : null) ?? storeProviderFactory.GetProviderServices());
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
							DbCommandDefinition dbCommandDefinition = this._storeProviderServices.CreateCommandDefinition(providerCommandInfo.CommandTree, interceptionContext);
							if (dbCommandDefinition == null)
							{
								throw new ProviderIncompatibleException(Strings.ProviderReturnedNullForCreateCommandDefinition);
							}
							this._mappedCommandDefinitions.Add(dbCommandDefinition);
						}
						goto IL_248;
					}
				}
				DbFunctionCommandTree dbFunctionCommandTree = (DbFunctionCommandTree)commandTree;
				FunctionImportMappingNonComposable targetFunctionMapping = EntityCommandDefinition.GetTargetFunctionMapping(dbFunctionCommandTree);
				IList<FunctionParameter> returnParameters = dbFunctionCommandTree.EdmFunction.ReturnParameters;
				int num = (returnParameters.Count > 1) ? returnParameters.Count : 1;
				this._columnMapGenerators = new EntityCommandDefinition.IColumnMapGenerator[num];
				TypeUsage resultType = this.DetermineStoreResultType(targetFunctionMapping, 0, out this._columnMapGenerators[0]);
				for (int i = 1; i < num; i++)
				{
					this.DetermineStoreResultType(targetFunctionMapping, i, out this._columnMapGenerators[i]);
				}
				List<KeyValuePair<string, TypeUsage>> list2 = new List<KeyValuePair<string, TypeUsage>>();
				foreach (KeyValuePair<string, TypeUsage> item in dbFunctionCommandTree.Parameters)
				{
					list2.Add(item);
				}
				DbFunctionCommandTree commandTree2 = new DbFunctionCommandTree(dbFunctionCommandTree.MetadataWorkspace, DataSpace.SSpace, targetFunctionMapping.TargetFunction, resultType, list2);
				DbCommandDefinition item2 = this._storeProviderServices.CreateCommandDefinition(commandTree2);
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
				IL_248:
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
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityCommandCompilationException(Strings.EntityClient_CommandDefinitionPreparationFailed, ex);
				}
				throw;
			}
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0008FCC0 File Offset: 0x0008DEC0
		protected EntityCommandDefinition(BridgeDataReaderFactory factory = null, ColumnMapFactory columnMapFactory = null, List<DbCommandDefinition> mappedCommandDefinitions = null)
		{
			this._bridgeDataReaderFactory = (factory ?? new BridgeDataReaderFactory(null));
			this._columnMapFactory = (columnMapFactory ?? new ColumnMapFactory());
			this._mappedCommandDefinitions = mappedCommandDefinitions;
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0008FCF0 File Offset: 0x0008DEF0
		private TypeUsage DetermineStoreResultType(FunctionImportMappingNonComposable mapping, int resultSetIndex, out EntityCommandDefinition.IColumnMapGenerator columnMapGenerator)
		{
			EdmFunction functionImport = mapping.FunctionImport;
			StructuralType structuralType;
			TypeUsage typeUsage;
			if (MetadataHelper.TryGetFunctionImportReturnType<StructuralType>(functionImport, resultSetIndex, out structuralType))
			{
				EntityCommandDefinition.ValidateEdmResultType(structuralType, functionImport);
				EntitySet entitySet = (functionImport.EntitySets.Count > resultSetIndex) ? functionImport.EntitySets[resultSetIndex] : null;
				columnMapGenerator = new EntityCommandDefinition.FunctionColumnMapGenerator(mapping, resultSetIndex, entitySet, structuralType, this._columnMapFactory);
				typeUsage = mapping.GetExpectedTargetResultType(resultSetIndex);
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

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0008FDB8 File Offset: 0x0008DFB8
		private static void ValidateEdmResultType(EdmType resultType, EdmFunction functionImport)
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

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0008FE44 File Offset: 0x0008E044
		private static FunctionImportMappingNonComposable GetTargetFunctionMapping(DbFunctionCommandTree functionCommandTree)
		{
			FunctionImportMapping functionImportMapping;
			if (!functionCommandTree.MetadataWorkspace.TryGetFunctionImportMapping(functionCommandTree.EdmFunction, out functionImportMapping))
			{
				throw new InvalidOperationException(Strings.EntityClient_UnmappedFunctionImport(functionCommandTree.EdmFunction.FullName));
			}
			return (FunctionImportMappingNonComposable)functionImportMapping;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0008FE82 File Offset: 0x0008E082
		internal virtual IEnumerable<EntityParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0008FE8A File Offset: 0x0008E08A
		internal virtual Set<EntitySet> EntitySets
		{
			get
			{
				return this._entitySets;
			}
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0008FE92 File Offset: 0x0008E092
		public override DbCommand CreateCommand()
		{
			return new EntityCommand(this, new DbInterceptionContext(), null);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0008FEA0 File Offset: 0x0008E0A0
		internal ColumnMap CreateColumnMap(DbDataReader storeDataReader)
		{
			return this.CreateColumnMap(storeDataReader, 0);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0008FEAA File Offset: 0x0008E0AA
		internal virtual ColumnMap CreateColumnMap(DbDataReader storeDataReader, int resultSetIndex)
		{
			return this._columnMapGenerators[resultSetIndex].CreateColumnMap(storeDataReader);
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0008FEBC File Offset: 0x0008E0BC
		private static EntityParameter CreateEntityParameterFromQueryParameter(KeyValuePair<string, TypeUsage> queryParameter)
		{
			EntityParameter entityParameter = new EntityParameter();
			entityParameter.ParameterName = queryParameter.Key;
			EntityCommandDefinition.PopulateParameterFromTypeUsage(entityParameter, queryParameter.Value, false);
			return entityParameter;
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0008FEEC File Offset: 0x0008E0EC
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

		// Token: 0x06001DEC RID: 7660 RVA: 0x0008FF40 File Offset: 0x0008E140
		internal virtual DbDataReader Execute(EntityCommand entityCommand, CommandBehavior behavior)
		{
			if (CommandBehavior.SequentialAccess != (behavior & CommandBehavior.SequentialAccess))
			{
				throw new InvalidOperationException(Strings.ADP_MustUseSequentialAccess);
			}
			DbDataReader dbDataReader = this.ExecuteStoreCommands(entityCommand, behavior & ~CommandBehavior.SequentialAccess);
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
						MetadataWorkspace metadataWorkspace = entityCommand.Connection.GetMetadataWorkspace();
						IEnumerable<ColumnMap> nextResultColumnMaps = this.GetNextResultColumnMaps(dbDataReader);
						result = this._bridgeDataReaderFactory.Create(dbDataReader, columnMap, metadataWorkspace, nextResultColumnMaps);
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

		// Token: 0x06001DED RID: 7661 RVA: 0x00090248 File Offset: 0x0008E448
		internal virtual async Task<DbDataReader> ExecuteAsync(EntityCommand entityCommand, CommandBehavior behavior, CancellationToken cancellationToken)
		{
			if (CommandBehavior.SequentialAccess != (behavior & CommandBehavior.SequentialAccess))
			{
				throw new InvalidOperationException(Strings.ADP_MustUseSequentialAccess);
			}
			cancellationToken.ThrowIfCancellationRequested();
			DbDataReader storeDataReader = await this.ExecuteStoreCommandsAsync(entityCommand, behavior & ~CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<DbDataReader>();
			DbDataReader result = null;
			if (storeDataReader != null)
			{
				try
				{
					ColumnMap columnMap = this.CreateColumnMap(storeDataReader, 0);
					if (columnMap == null)
					{
						await CommandHelper.ConsumeReaderAsync(storeDataReader, cancellationToken).WithCurrentCulture();
						result = storeDataReader;
					}
					else
					{
						MetadataWorkspace metadataWorkspace = entityCommand.Connection.GetMetadataWorkspace();
						IEnumerable<ColumnMap> nextResultColumnMaps = this.GetNextResultColumnMaps(storeDataReader);
						result = this._bridgeDataReaderFactory.Create(storeDataReader, columnMap, metadataWorkspace, nextResultColumnMaps);
					}
				}
				catch
				{
					storeDataReader.Dispose();
					throw;
				}
			}
			return result;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000903B8 File Offset: 0x0008E5B8
		private IEnumerable<ColumnMap> GetNextResultColumnMaps(DbDataReader storeDataReader)
		{
			for (int i = 1; i < this._columnMapGenerators.Length; i++)
			{
				yield return this.CreateColumnMap(storeDataReader, i);
			}
			yield break;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000903DC File Offset: 0x0008E5DC
		internal virtual DbDataReader ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
		{
			DbCommand dbCommand = this.PrepareEntityCommandBeforeExecution(entityCommand);
			DbDataReader result = null;
			try
			{
				result = dbCommand.ExecuteReader(behavior);
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_CommandDefinitionExecutionFailed, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00090580 File Offset: 0x0008E780
		internal virtual async Task<DbDataReader> ExecuteStoreCommandsAsync(EntityCommand entityCommand, CommandBehavior behavior, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			DbCommand storeProviderCommand = this.PrepareEntityCommandBeforeExecution(entityCommand);
			DbDataReader reader = null;
			try
			{
				reader = await storeProviderCommand.ExecuteReaderAsync(behavior, cancellationToken).WithCurrentCulture<DbDataReader>();
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_CommandDefinitionExecutionFailed, ex);
				}
				throw;
			}
			return reader;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000905E0 File Offset: 0x0008E7E0
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		private DbCommand PrepareEntityCommandBeforeExecution(EntityCommand entityCommand)
		{
			if (1 != this._mappedCommandDefinitions.Count)
			{
				throw new NotSupportedException("MARS");
			}
			EntityTransaction entityTransaction = entityCommand.ValidateAndGetEntityTransaction();
			DbCommandDefinition dbCommandDefinition = this._mappedCommandDefinitions[0];
			InterceptableDbCommand interceptableDbCommand = new InterceptableDbCommand(dbCommandDefinition.CreateCommand(), entityCommand.InterceptionContext, null);
			CommandHelper.SetStoreProviderCommandState(entityCommand, entityTransaction, interceptableDbCommand);
			bool flag = false;
			if (interceptableDbCommand.Parameters != null)
			{
				foreach (object obj in interceptableDbCommand.Parameters)
				{
					DbParameter dbParameter = (DbParameter)obj;
					int num = entityCommand.Parameters.IndexOf(dbParameter.ParameterName);
					if (-1 != num)
					{
						EntityParameter entityParameter = entityCommand.Parameters[num];
						EntityCommandDefinition.SyncParameterProperties(entityParameter, dbParameter, this._storeProviderServices);
						if (dbParameter.Direction != ParameterDirection.Input)
						{
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				entityCommand.SetStoreProviderCommand(interceptableDbCommand);
			}
			return interceptableDbCommand;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000906E0 File Offset: 0x0008E8E0
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

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00090770 File Offset: 0x0008E970
		internal virtual string ToTraceString()
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

		// Token: 0x04000A35 RID: 2613
		private readonly List<DbCommandDefinition> _mappedCommandDefinitions;

		// Token: 0x04000A36 RID: 2614
		private readonly EntityCommandDefinition.IColumnMapGenerator[] _columnMapGenerators;

		// Token: 0x04000A37 RID: 2615
		private readonly ReadOnlyCollection<EntityParameter> _parameters;

		// Token: 0x04000A38 RID: 2616
		private readonly Set<EntitySet> _entitySets;

		// Token: 0x04000A39 RID: 2617
		private readonly BridgeDataReaderFactory _bridgeDataReaderFactory;

		// Token: 0x04000A3A RID: 2618
		private readonly ColumnMapFactory _columnMapFactory;

		// Token: 0x04000A3B RID: 2619
		private readonly DbProviderServices _storeProviderServices;

		// Token: 0x02000346 RID: 838
		private interface IColumnMapGenerator
		{
			// Token: 0x06001DF4 RID: 7668
			ColumnMap CreateColumnMap(DbDataReader reader);
		}

		// Token: 0x02000347 RID: 839
		private sealed class ConstantColumnMapGenerator : EntityCommandDefinition.IColumnMapGenerator
		{
			// Token: 0x06001DF5 RID: 7669 RVA: 0x00090810 File Offset: 0x0008EA10
			internal ConstantColumnMapGenerator(ColumnMap columnMap, int fieldsRequired)
			{
				this._columnMap = columnMap;
				this._fieldsRequired = fieldsRequired;
			}

			// Token: 0x06001DF6 RID: 7670 RVA: 0x00090826 File Offset: 0x0008EA26
			ColumnMap EntityCommandDefinition.IColumnMapGenerator.CreateColumnMap(DbDataReader reader)
			{
				if (reader != null && reader.FieldCount < this._fieldsRequired)
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_TooFewColumns);
				}
				return this._columnMap;
			}

			// Token: 0x04000A3C RID: 2620
			private readonly ColumnMap _columnMap;

			// Token: 0x04000A3D RID: 2621
			private readonly int _fieldsRequired;
		}

		// Token: 0x02000348 RID: 840
		private sealed class FunctionColumnMapGenerator : EntityCommandDefinition.IColumnMapGenerator
		{
			// Token: 0x06001DF7 RID: 7671 RVA: 0x0009084A File Offset: 0x0008EA4A
			internal FunctionColumnMapGenerator(FunctionImportMappingNonComposable mapping, int resultSetIndex, EntitySet entitySet, StructuralType baseStructuralType, ColumnMapFactory columnMapFactory)
			{
				this._mapping = mapping;
				this._entitySet = entitySet;
				this._baseStructuralType = baseStructuralType;
				this._resultSetIndex = resultSetIndex;
				this._columnMapFactory = columnMapFactory;
			}

			// Token: 0x06001DF8 RID: 7672 RVA: 0x00090877 File Offset: 0x0008EA77
			ColumnMap EntityCommandDefinition.IColumnMapGenerator.CreateColumnMap(DbDataReader reader)
			{
				return this._columnMapFactory.CreateFunctionImportStructuralTypeColumnMap(reader, this._mapping, this._resultSetIndex, this._entitySet, this._baseStructuralType);
			}

			// Token: 0x04000A3E RID: 2622
			private readonly FunctionImportMappingNonComposable _mapping;

			// Token: 0x04000A3F RID: 2623
			private readonly EntitySet _entitySet;

			// Token: 0x04000A40 RID: 2624
			private readonly StructuralType _baseStructuralType;

			// Token: 0x04000A41 RID: 2625
			private readonly int _resultSetIndex;

			// Token: 0x04000A42 RID: 2626
			private readonly ColumnMapFactory _columnMapFactory;
		}
	}
}
