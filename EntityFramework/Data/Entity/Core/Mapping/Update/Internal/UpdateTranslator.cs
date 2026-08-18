using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x02000418 RID: 1048
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class UpdateTranslator
	{
		// Token: 0x06002672 RID: 9842 RVA: 0x000B76B8 File Offset: 0x000B58B8
		public UpdateTranslator(EntityAdapter adapter) : this()
		{
			this._stateManager = adapter.Context.ObjectStateManager;
			this._interceptionContext = adapter.Context.InterceptionContext;
			this._adapter = adapter;
			this._providerServices = adapter.Connection.StoreProviderFactory.GetProviderServices();
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000B770C File Offset: 0x000B590C
		protected UpdateTranslator()
		{
			this._changes = new Dictionary<EntitySetBase, ChangeNode>();
			this._functionChanges = new Dictionary<EntitySetBase, List<ExtractedStateEntry>>();
			this._stateEntries = new List<IEntityStateEntry>();
			this._knownEntityKeys = new Set<EntityKey>();
			this._requiredEntities = new Dictionary<EntityKey, AssociationSet>();
			this._optionalEntities = new Set<EntityKey>();
			this._includedValueEntities = new Set<EntityKey>();
			this._interceptionContext = new DbInterceptionContext();
			this._recordConverter = new RecordConverter(this);
			this._constraintValidator = new UpdateTranslator.RelationshipConstraintValidator();
			this._extractorMetadata = new Dictionary<Tuple<EntitySetBase, StructuralType>, ExtractorMetadata>();
			KeyManager keyManager = new KeyManager();
			this.KeyManager = keyManager;
			this.KeyComparer = CompositeKey.CreateComparer(keyManager);
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x000B77B2 File Offset: 0x000B59B2
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this.Connection.GetMetadataWorkspace();
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x000B77BF File Offset: 0x000B59BF
		// (set) Token: 0x06002676 RID: 9846 RVA: 0x000B77C7 File Offset: 0x000B59C7
		internal virtual KeyManager KeyManager { get; private set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000B77D0 File Offset: 0x000B59D0
		internal ViewLoader ViewLoader
		{
			get
			{
				return this.MetadataWorkspace.GetUpdateViewLoader();
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x000B77DD File Offset: 0x000B59DD
		internal RecordConverter RecordConverter
		{
			get
			{
				return this._recordConverter;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000B77E5 File Offset: 0x000B59E5
		internal virtual EntityConnection Connection
		{
			get
			{
				return this._adapter.Connection;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000B77F2 File Offset: 0x000B59F2
		internal virtual int? CommandTimeout
		{
			get
			{
				return this._adapter.CommandTimeout;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000B77FF File Offset: 0x000B59FF
		public virtual DbInterceptionContext InterceptionContext
		{
			get
			{
				return this._interceptionContext;
			}
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000B7808 File Offset: 0x000B5A08
		internal void RegisterReferentialConstraints(IEntityStateEntry stateEntry)
		{
			if (stateEntry.IsRelationship)
			{
				AssociationSet associationSet = (AssociationSet)stateEntry.EntitySet;
				if (0 >= associationSet.ElementType.ReferentialConstraints.Count)
				{
					return;
				}
				DbDataRecord dbDataRecord = (stateEntry.State == EntityState.Added) ? stateEntry.CurrentValues : stateEntry.OriginalValues;
				using (ReadOnlyMetadataCollection<ReferentialConstraint>.Enumerator enumerator = associationSet.ElementType.ReferentialConstraints.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ReferentialConstraint referentialConstraint = enumerator.Current;
						EntityKey entityKey = (EntityKey)dbDataRecord[referentialConstraint.FromRole.Name];
						EntityKey entityKey2 = (EntityKey)dbDataRecord[referentialConstraint.ToRole.Name];
						using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator2 = referentialConstraint.FromProperties.GetEnumerator())
						{
							using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator3 = referentialConstraint.ToProperties.GetEnumerator())
							{
								while (enumerator2.MoveNext() && enumerator3.MoveNext())
								{
									int keyMemberCount;
									int keyMemberOffset = UpdateTranslator.GetKeyMemberOffset(referentialConstraint.FromRole, enumerator2.Current, out keyMemberCount);
									int keyMemberCount2;
									int keyMemberOffset2 = UpdateTranslator.GetKeyMemberOffset(referentialConstraint.ToRole, enumerator3.Current, out keyMemberCount2);
									int keyIdentifierForMemberOffset = this.KeyManager.GetKeyIdentifierForMemberOffset(entityKey, keyMemberOffset, keyMemberCount);
									int keyIdentifierForMemberOffset2 = this.KeyManager.GetKeyIdentifierForMemberOffset(entityKey2, keyMemberOffset2, keyMemberCount2);
									this.KeyManager.AddReferentialConstraint(stateEntry, keyIdentifierForMemberOffset2, keyIdentifierForMemberOffset);
								}
							}
						}
					}
					return;
				}
			}
			if (!stateEntry.IsKeyEntry)
			{
				if (stateEntry.State == EntityState.Added || stateEntry.State == EntityState.Modified)
				{
					this.RegisterEntityReferentialConstraints(stateEntry, true);
				}
				if (stateEntry.State == EntityState.Deleted || stateEntry.State == EntityState.Modified)
				{
					this.RegisterEntityReferentialConstraints(stateEntry, false);
				}
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000B79DC File Offset: 0x000B5BDC
		private void RegisterEntityReferentialConstraints(IEntityStateEntry stateEntry, bool currentValues)
		{
			IExtendedDataRecord extendedDataRecord = currentValues ? stateEntry.CurrentValues : ((IExtendedDataRecord)stateEntry.OriginalValues);
			EntitySet entitySet = (EntitySet)stateEntry.EntitySet;
			EntityKey entityKey = stateEntry.EntityKey;
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in entitySet.ForeignKeyDependents)
			{
				AssociationSet item = tuple.Item1;
				ReferentialConstraint item2 = tuple.Item2;
				EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)item2.ToRole);
				if (entityTypeForEnd.IsAssignableFrom(extendedDataRecord.DataRecordInfo.RecordType.EdmType))
				{
					EntityKey entityKey2 = null;
					if (!currentValues || !this._stateManager.TryGetReferenceKey(entityKey, (AssociationEndMember)item2.FromRole, out entityKey2))
					{
						EntityType entityTypeForEnd2 = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)item2.FromRole);
						bool flag = false;
						object[] array = new object[entityTypeForEnd2.KeyMembers.Count];
						int i = 0;
						int num = array.Length;
						while (i < num)
						{
							EdmProperty value = (EdmProperty)entityTypeForEnd2.KeyMembers[i];
							int index = item2.FromProperties.IndexOf(value);
							int ordinal = extendedDataRecord.GetOrdinal(item2.ToProperties[index].Name);
							if (extendedDataRecord.IsDBNull(ordinal))
							{
								flag = true;
								break;
							}
							array[i] = extendedDataRecord.GetValue(ordinal);
							i++;
						}
						if (!flag)
						{
							EntitySet entitySet2 = item.AssociationSetEnds[item2.FromRole.Name].EntitySet;
							if (1 == array.Length)
							{
								entityKey2 = new EntityKey(entitySet2, array[0]);
							}
							else
							{
								entityKey2 = new EntityKey(entitySet2, array);
							}
						}
					}
					if (null != entityKey2)
					{
						IEntityStateEntry entityStateEntry;
						EntityKey entityKey3;
						if (!this._stateManager.TryGetEntityStateEntry(entityKey2, out entityStateEntry) && currentValues && this.KeyManager.TryGetTempKey(entityKey2, out entityKey3))
						{
							if (null == entityKey3)
							{
								throw EntityUtil.Update(Strings.Update_AmbiguousForeignKey(item2.ToRole.DeclaringType.FullName), null, new IEntityStateEntry[]
								{
									stateEntry
								});
							}
							entityKey2 = entityKey3;
						}
						this.AddValidAncillaryKey(entityKey2, this._optionalEntities);
						int j = 0;
						int count = item2.FromProperties.Count;
						while (j < count)
						{
							EdmProperty property = item2.FromProperties[j];
							EdmProperty edmProperty = item2.ToProperties[j];
							int keyMemberCount;
							int keyMemberOffset = UpdateTranslator.GetKeyMemberOffset(item2.FromRole, property, out keyMemberCount);
							int keyIdentifierForMemberOffset = this.KeyManager.GetKeyIdentifierForMemberOffset(entityKey2, keyMemberOffset, keyMemberCount);
							int dependentIdentifier;
							if (entitySet.ElementType.KeyMembers.Contains(edmProperty))
							{
								int keyMemberCount2;
								int keyMemberOffset2 = UpdateTranslator.GetKeyMemberOffset(item2.ToRole, edmProperty, out keyMemberCount2);
								dependentIdentifier = this.KeyManager.GetKeyIdentifierForMemberOffset(entityKey, keyMemberOffset2, keyMemberCount2);
							}
							else
							{
								dependentIdentifier = this.KeyManager.GetKeyIdentifierForMember(entityKey, edmProperty.Name, currentValues);
							}
							if (currentValues && entityStateEntry != null && entityStateEntry.State == EntityState.Deleted && (stateEntry.State == EntityState.Added || stateEntry.State == EntityState.Modified))
							{
								throw EntityUtil.Update(Strings.Update_InsertingOrUpdatingReferenceToDeletedEntity(item.ElementType.FullName), null, new IEntityStateEntry[]
								{
									stateEntry,
									entityStateEntry
								});
							}
							this.KeyManager.AddReferentialConstraint(stateEntry, dependentIdentifier, keyIdentifierForMemberOffset);
							j++;
						}
					}
				}
			}
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000B7D3C File Offset: 0x000B5F3C
		private static int GetKeyMemberOffset(RelationshipEndMember role, EdmProperty property, out int keyMemberCount)
		{
			RefType refType = (RefType)role.TypeUsage.EdmType;
			EntityType entityType = (EntityType)refType.ElementType;
			keyMemberCount = entityType.KeyMembers.Count;
			return entityType.KeyMembers.IndexOf(property);
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000B7D7F File Offset: 0x000B5F7F
		internal IEnumerable<IEntityStateEntry> GetRelationships(EntityKey entityKey)
		{
			return this._stateManager.FindRelationshipsByKey(entityKey);
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000B7D90 File Offset: 0x000B5F90
		internal virtual int Update()
		{
			Dictionary<int, object> identifierValues = new Dictionary<int, object>();
			List<KeyValuePair<PropagatorResult, object>> generatedValues = new List<KeyValuePair<PropagatorResult, object>>();
			IEnumerable<UpdateCommand> enumerable = this.ProduceCommands();
			UpdateCommand source = null;
			try
			{
				foreach (UpdateCommand updateCommand in enumerable)
				{
					source = updateCommand;
					long rowsAffected = updateCommand.Execute(identifierValues, generatedValues);
					this.ValidateRowsAffected(rowsAffected, source);
				}
			}
			catch (Exception ex)
			{
				if (ex.RequiresContext())
				{
					throw new UpdateException(Strings.Update_GeneralExecutionException, ex, this.DetermineStateEntriesFromSource(source).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
				}
				throw;
			}
			this.BackPropagateServerGen(generatedValues);
			return this.AcceptChanges();
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000B8090 File Offset: 0x000B6290
		internal virtual async Task<int> UpdateAsync(CancellationToken cancellationToken)
		{
			Dictionary<int, object> identifierValues = new Dictionary<int, object>();
			List<KeyValuePair<PropagatorResult, object>> generatedValues = new List<KeyValuePair<PropagatorResult, object>>();
			IEnumerable<UpdateCommand> orderedCommands = this.ProduceCommands();
			UpdateCommand source = null;
			try
			{
				foreach (UpdateCommand command in orderedCommands)
				{
					source = command;
					long rowsAffected = await command.ExecuteAsync(identifierValues, generatedValues, cancellationToken).WithCurrentCulture<long>();
					this.ValidateRowsAffected(rowsAffected, source);
				}
			}
			catch (Exception ex)
			{
				if (ex.RequiresContext())
				{
					throw new UpdateException(Strings.Update_GeneralExecutionException, ex, this.DetermineStateEntriesFromSource(source).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
				}
				throw;
			}
			this.BackPropagateServerGen(generatedValues);
			return this.AcceptChanges();
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000B80E0 File Offset: 0x000B62E0
		protected virtual IEnumerable<UpdateCommand> ProduceCommands()
		{
			this.PullModifiedEntriesFromStateManager();
			this.PullUnchangedEntriesFromStateManager();
			this._constraintValidator.ValidateConstraints();
			this.KeyManager.ValidateReferentialIntegrityGraphAcyclic();
			IEnumerable<UpdateCommand> first = this.ProduceDynamicCommands();
			IEnumerable<UpdateCommand> second = this.ProduceFunctionCommands();
			UpdateCommandOrderer updateCommandOrderer = new UpdateCommandOrderer(first.Concat(second), this);
			IEnumerable<UpdateCommand> result;
			IEnumerable<UpdateCommand> remainder;
			if (!updateCommandOrderer.TryTopologicalSort(out result, out remainder))
			{
				throw this.DependencyOrderingError(remainder);
			}
			return result;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000B8144 File Offset: 0x000B6344
		private void ValidateRowsAffected(long rowsAffected, UpdateCommand source)
		{
			if (0L == rowsAffected)
			{
				IEnumerable<IEntityStateEntry> source2 = this.DetermineStateEntriesFromSource(source);
				string message = Strings.Update_ConcurrencyError(rowsAffected);
				throw new OptimisticConcurrencyException(message, null, source2.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
			}
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000B817D File Offset: 0x000B637D
		private IEnumerable<IEntityStateEntry> DetermineStateEntriesFromSource(UpdateCommand source)
		{
			if (source == null)
			{
				return Enumerable.Empty<IEntityStateEntry>();
			}
			return source.GetStateEntries(this);
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000B8190 File Offset: 0x000B6390
		private void BackPropagateServerGen(List<KeyValuePair<PropagatorResult, object>> generatedValues)
		{
			foreach (KeyValuePair<PropagatorResult, object> keyValuePair in generatedValues)
			{
				PropagatorResult key;
				if (-1 == keyValuePair.Key.Identifier || !this.KeyManager.TryGetIdentifierOwner(keyValuePair.Key.Identifier, out key))
				{
					key = keyValuePair.Key;
				}
				object value = keyValuePair.Value;
				if (key.Identifier == -1)
				{
					key.SetServerGenValue(value);
				}
				else
				{
					foreach (int identifier in this.KeyManager.GetDependents(key.Identifier))
					{
						if (this.KeyManager.TryGetIdentifierOwner(identifier, out key))
						{
							key.SetServerGenValue(value);
						}
					}
				}
			}
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000B8288 File Offset: 0x000B6488
		private int AcceptChanges()
		{
			int num = 0;
			foreach (IEntityStateEntry entityStateEntry in this._stateEntries)
			{
				if (EntityState.Unchanged != entityStateEntry.State)
				{
					if (this._adapter.AcceptChangesDuringUpdate)
					{
						entityStateEntry.AcceptChanges();
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000B82F8 File Offset: 0x000B64F8
		private IEnumerable<EntitySetBase> GetDynamicModifiedExtents()
		{
			return this._changes.Keys;
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000B8305 File Offset: 0x000B6505
		private IEnumerable<EntitySetBase> GetFunctionModifiedExtents()
		{
			return this._functionChanges.Keys;
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000B8688 File Offset: 0x000B6888
		private IEnumerable<UpdateCommand> ProduceDynamicCommands()
		{
			UpdateCompiler updateCompiler = new UpdateCompiler(this);
			Set<EntitySet> tables = new Set<EntitySet>();
			foreach (EntitySetBase entitySetBase in this.GetDynamicModifiedExtents())
			{
				Set<EntitySet> affectedTables = this.ViewLoader.GetAffectedTables(entitySetBase, this.MetadataWorkspace);
				if (affectedTables.Count == 0)
				{
					throw EntityUtil.Update(Strings.Update_MappingNotFound(entitySetBase.Name), null, new IEntityStateEntry[0]);
				}
				foreach (EntitySet element in affectedTables)
				{
					tables.Add(element);
				}
			}
			foreach (EntitySet table in tables)
			{
				DbQueryCommandTree umView = this.Connection.GetMetadataWorkspace().GetCqtView(table);
				ChangeNode changeNode = Propagator.Propagate(this, table, umView);
				TableChangeProcessor change = new TableChangeProcessor(table);
				foreach (UpdateCommand command in change.CompileCommands(changeNode, updateCompiler))
				{
					yield return command;
				}
			}
			yield break;
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000B86B8 File Offset: 0x000B68B8
		internal DbCommandDefinition GenerateCommandDefinition(ModificationFunctionMapping functionMapping)
		{
			if (this._modificationFunctionCommandDefinitions == null)
			{
				this._modificationFunctionCommandDefinitions = new Dictionary<ModificationFunctionMapping, DbCommandDefinition>();
			}
			DbCommandDefinition result;
			if (!this._modificationFunctionCommandDefinitions.TryGetValue(functionMapping, out result))
			{
				TypeUsage resultType = null;
				if (functionMapping.ResultBindings != null && functionMapping.ResultBindings.Count > 0)
				{
					List<EdmProperty> list = new List<EdmProperty>(functionMapping.ResultBindings.Count);
					foreach (ModificationFunctionResultBinding modificationFunctionResultBinding in functionMapping.ResultBindings)
					{
						list.Add(new EdmProperty(modificationFunctionResultBinding.ColumnName, modificationFunctionResultBinding.Property.TypeUsage));
					}
					RowType elementType = new RowType(list);
					CollectionType edmType = new CollectionType(elementType);
					resultType = TypeUsage.Create(edmType);
				}
				IEnumerable<KeyValuePair<string, TypeUsage>> parameters = from paramInfo in functionMapping.Function.Parameters
				select new KeyValuePair<string, TypeUsage>(paramInfo.Name, paramInfo.TypeUsage);
				DbFunctionCommandTree commandTree = new DbFunctionCommandTree(this.MetadataWorkspace, DataSpace.SSpace, functionMapping.Function, resultType, parameters);
				result = this._providerServices.CreateCommandDefinition(commandTree, this._interceptionContext);
			}
			return result;
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000B8A68 File Offset: 0x000B6C68
		private IEnumerable<UpdateCommand> ProduceFunctionCommands()
		{
			foreach (EntitySetBase extent in this.GetFunctionModifiedExtents())
			{
				ModificationFunctionMappingTranslator translator = this.ViewLoader.GetFunctionMappingTranslator(extent, this.MetadataWorkspace);
				if (translator != null)
				{
					foreach (ExtractedStateEntry stateEntry in this.GetExtentFunctionModifications(extent))
					{
						FunctionUpdateCommand command = translator.Translate(this, stateEntry);
						if (command != null)
						{
							yield return command;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000B8A88 File Offset: 0x000B6C88
		internal ExtractorMetadata GetExtractorMetadata(EntitySetBase entitySetBase, StructuralType type)
		{
			Tuple<EntitySetBase, StructuralType> key = Tuple.Create<EntitySetBase, StructuralType>(entitySetBase, type);
			ExtractorMetadata extractorMetadata;
			if (!this._extractorMetadata.TryGetValue(key, out extractorMetadata))
			{
				extractorMetadata = new ExtractorMetadata(entitySetBase, type, this);
				this._extractorMetadata.Add(key, extractorMetadata);
			}
			return extractorMetadata;
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000B8AC4 File Offset: 0x000B6CC4
		private UpdateException DependencyOrderingError(IEnumerable<UpdateCommand> remainder)
		{
			HashSet<IEntityStateEntry> hashSet = new HashSet<IEntityStateEntry>();
			foreach (UpdateCommand updateCommand in remainder)
			{
				hashSet.UnionWith(updateCommand.GetStateEntries(this));
			}
			throw new UpdateException(Strings.Update_ConstraintCycle, null, hashSet.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000B8B30 File Offset: 0x000B6D30
		internal DbCommand CreateCommand(DbModificationCommandTree commandTree)
		{
			DbCommand result;
			try
			{
				result = new InterceptableDbCommand(this._providerServices.CreateCommand(commandTree, this._interceptionContext), this._interceptionContext, null);
			}
			catch (Exception ex)
			{
				if (ex.RequiresContext())
				{
					throw new EntityCommandCompilationException(Strings.EntityClient_CommandDefinitionPreparationFailed, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000B8B88 File Offset: 0x000B6D88
		internal void SetParameterValue(DbParameter parameter, TypeUsage typeUsage, object value)
		{
			this._providerServices.SetParameterValue(parameter, typeUsage, value);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000B8B98 File Offset: 0x000B6D98
		private void PullModifiedEntriesFromStateManager()
		{
			foreach (IEntityStateEntry entityStateEntry in this._stateManager.GetEntityStateEntries(EntityState.Added))
			{
				if (!entityStateEntry.IsRelationship && !entityStateEntry.IsKeyEntry)
				{
					this.KeyManager.RegisterKeyValueForAddedEntity(entityStateEntry);
				}
			}
			foreach (IEntityStateEntry stateEntry in this._stateManager.GetEntityStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified))
			{
				this.RegisterReferentialConstraints(stateEntry);
			}
			foreach (IEntityStateEntry stateEntry2 in this._stateManager.GetEntityStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified))
			{
				this.LoadStateEntry(stateEntry2);
			}
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000B8C90 File Offset: 0x000B6E90
		private void PullUnchangedEntriesFromStateManager()
		{
			foreach (KeyValuePair<EntityKey, AssociationSet> keyValuePair in this._requiredEntities)
			{
				EntityKey key = keyValuePair.Key;
				if (!this._knownEntityKeys.Contains(key))
				{
					IEntityStateEntry entityStateEntry;
					if (!this._stateManager.TryGetEntityStateEntry(key, out entityStateEntry) || entityStateEntry.IsKeyEntry)
					{
						throw EntityUtil.Update(Strings.Update_MissingEntity(keyValuePair.Value.Name, TypeHelpers.GetFullName(key.EntityContainerName, key.EntitySetName)), null, new IEntityStateEntry[0]);
					}
					this.LoadStateEntry(entityStateEntry);
				}
			}
			foreach (EntityKey entityKey in this._optionalEntities)
			{
				IEntityStateEntry entityStateEntry2;
				if (!this._knownEntityKeys.Contains(entityKey) && this._stateManager.TryGetEntityStateEntry(entityKey, out entityStateEntry2) && !entityStateEntry2.IsKeyEntry)
				{
					this.LoadStateEntry(entityStateEntry2);
				}
			}
			foreach (EntityKey entityKey2 in this._includedValueEntities)
			{
				IEntityStateEntry stateEntry;
				if (!this._knownEntityKeys.Contains(entityKey2) && this._stateManager.TryGetEntityStateEntry(entityKey2, out stateEntry))
				{
					this._recordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.NoneModified);
				}
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000B8E1C File Offset: 0x000B701C
		private void ValidateAndRegisterStateEntry(IEntityStateEntry stateEntry)
		{
			EntitySetBase entitySet = stateEntry.EntitySet;
			if (entitySet == null)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.InvalidStateEntry, 1, null);
			}
			EntityKey entityKey = stateEntry.EntityKey;
			IExtendedDataRecord extendedDataRecord = null;
			if (((EntityState.Unchanged | EntityState.Added | EntityState.Modified) & stateEntry.State) != (EntityState)0)
			{
				extendedDataRecord = stateEntry.CurrentValues;
				this.ValidateRecord(entitySet, extendedDataRecord);
			}
			if (((EntityState.Unchanged | EntityState.Deleted | EntityState.Modified) & stateEntry.State) != (EntityState)0)
			{
				extendedDataRecord = (IExtendedDataRecord)stateEntry.OriginalValues;
				this.ValidateRecord(entitySet, extendedDataRecord);
			}
			AssociationSet associationSet = entitySet as AssociationSet;
			if (associationSet != null)
			{
				AssociationSetMetadata associationSetMetadata = this.ViewLoader.GetAssociationSetMetadata(associationSet, this.MetadataWorkspace);
				if (associationSetMetadata.HasEnds)
				{
					foreach (FieldMetadata fieldMetadata in extendedDataRecord.DataRecordInfo.FieldMetadata)
					{
						EntityKey key = (EntityKey)extendedDataRecord.GetValue(fieldMetadata.Ordinal);
						AssociationEndMember element = (AssociationEndMember)fieldMetadata.FieldType;
						if (associationSetMetadata.RequiredEnds.Contains(element))
						{
							if (!this._requiredEntities.ContainsKey(key))
							{
								this._requiredEntities.Add(key, associationSet);
							}
						}
						else if (associationSetMetadata.OptionalEnds.Contains(element))
						{
							this.AddValidAncillaryKey(key, this._optionalEntities);
						}
						else if (associationSetMetadata.IncludedValueEnds.Contains(element))
						{
							this.AddValidAncillaryKey(key, this._includedValueEntities);
						}
					}
				}
				this._constraintValidator.RegisterAssociation(associationSet, extendedDataRecord, stateEntry);
			}
			else
			{
				this._constraintValidator.RegisterEntity(stateEntry);
			}
			this._stateEntries.Add(stateEntry);
			if (entityKey != null)
			{
				this._knownEntityKeys.Add(entityKey);
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000B8FC0 File Offset: 0x000B71C0
		private void AddValidAncillaryKey(EntityKey key, Set<EntityKey> keySet)
		{
			IEntityStateEntry entityStateEntry;
			if (this._stateManager.TryGetEntityStateEntry(key, out entityStateEntry) && !entityStateEntry.IsKeyEntry && entityStateEntry.State == EntityState.Unchanged)
			{
				keySet.Add(key);
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000B8FF8 File Offset: 0x000B71F8
		private void ValidateRecord(EntitySetBase extent, IExtendedDataRecord record)
		{
			DataRecordInfo dataRecordInfo;
			if (record == null || (dataRecordInfo = record.DataRecordInfo) == null || dataRecordInfo.RecordType == null)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.InvalidStateEntry, 2, null);
			}
			UpdateTranslator.VerifyExtent(this.MetadataWorkspace, extent);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000B9034 File Offset: 0x000B7234
		private static void VerifyExtent(MetadataWorkspace workspace, EntitySetBase extent)
		{
			EntityContainer entityContainer = extent.EntityContainer;
			EntityContainer entityContainer2 = null;
			if (entityContainer != null)
			{
				workspace.TryGetEntityContainer(entityContainer.Name, entityContainer.DataSpace, out entityContainer2);
			}
			if (entityContainer == null || entityContainer2 == null || !object.ReferenceEquals(entityContainer, entityContainer2))
			{
				throw EntityUtil.Update(Strings.Update_WorkspaceMismatch, null, new IEntityStateEntry[0]);
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000B9084 File Offset: 0x000B7284
		private void LoadStateEntry(IEntityStateEntry stateEntry)
		{
			this.ValidateAndRegisterStateEntry(stateEntry);
			ExtractedStateEntry item = new ExtractedStateEntry(this, stateEntry);
			EntitySetBase entitySet = stateEntry.EntitySet;
			if (this.ViewLoader.GetFunctionMappingTranslator(entitySet, this.MetadataWorkspace) == null)
			{
				ChangeNode extentModifications = this.GetExtentModifications(entitySet);
				if (item.Original != null)
				{
					extentModifications.Deleted.Add(item.Original);
				}
				if (item.Current != null)
				{
					extentModifications.Inserted.Add(item.Current);
					return;
				}
			}
			else
			{
				List<ExtractedStateEntry> extentFunctionModifications = this.GetExtentFunctionModifications(entitySet);
				extentFunctionModifications.Add(item);
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000B910C File Offset: 0x000B730C
		internal ChangeNode GetExtentModifications(EntitySetBase extent)
		{
			ChangeNode changeNode;
			if (!this._changes.TryGetValue(extent, out changeNode))
			{
				changeNode = new ChangeNode(TypeUsage.Create(extent.ElementType));
				this._changes.Add(extent, changeNode);
			}
			return changeNode;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000B9148 File Offset: 0x000B7348
		internal List<ExtractedStateEntry> GetExtentFunctionModifications(EntitySetBase extent)
		{
			List<ExtractedStateEntry> list;
			if (!this._functionChanges.TryGetValue(extent, out list))
			{
				list = new List<ExtractedStateEntry>();
				this._functionChanges.Add(extent, list);
			}
			return list;
		}

		// Token: 0x04000E67 RID: 3687
		private readonly EntityAdapter _adapter;

		// Token: 0x04000E68 RID: 3688
		private readonly Dictionary<EntitySetBase, ChangeNode> _changes;

		// Token: 0x04000E69 RID: 3689
		private readonly Dictionary<EntitySetBase, List<ExtractedStateEntry>> _functionChanges;

		// Token: 0x04000E6A RID: 3690
		private readonly List<IEntityStateEntry> _stateEntries;

		// Token: 0x04000E6B RID: 3691
		private readonly Set<EntityKey> _knownEntityKeys;

		// Token: 0x04000E6C RID: 3692
		private readonly Dictionary<EntityKey, AssociationSet> _requiredEntities;

		// Token: 0x04000E6D RID: 3693
		private readonly Set<EntityKey> _optionalEntities;

		// Token: 0x04000E6E RID: 3694
		private readonly Set<EntityKey> _includedValueEntities;

		// Token: 0x04000E6F RID: 3695
		private readonly IEntityStateManager _stateManager;

		// Token: 0x04000E70 RID: 3696
		private readonly DbInterceptionContext _interceptionContext;

		// Token: 0x04000E71 RID: 3697
		private readonly RecordConverter _recordConverter;

		// Token: 0x04000E72 RID: 3698
		private readonly UpdateTranslator.RelationshipConstraintValidator _constraintValidator;

		// Token: 0x04000E73 RID: 3699
		private readonly DbProviderServices _providerServices;

		// Token: 0x04000E74 RID: 3700
		private Dictionary<ModificationFunctionMapping, DbCommandDefinition> _modificationFunctionCommandDefinitions;

		// Token: 0x04000E75 RID: 3701
		private readonly Dictionary<Tuple<EntitySetBase, StructuralType>, ExtractorMetadata> _extractorMetadata;

		// Token: 0x04000E76 RID: 3702
		internal readonly IEqualityComparer<CompositeKey> KeyComparer;

		// Token: 0x02000419 RID: 1049
		private class RelationshipConstraintValidator
		{
			// Token: 0x0600269A RID: 9882 RVA: 0x000B9179 File Offset: 0x000B7379
			internal RelationshipConstraintValidator()
			{
				this.m_existingRelationships = new Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>(EqualityComparer<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>.Default);
				this.m_impliedRelationships = new Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, IEntityStateEntry>(EqualityComparer<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>.Default);
				this.m_referencingRelationshipSets = new Dictionary<EntitySet, List<AssociationSet>>(EqualityComparer<EntitySet>.Default);
			}

			// Token: 0x0600269B RID: 9883 RVA: 0x000B91B4 File Offset: 0x000B73B4
			internal void RegisterEntity(IEntityStateEntry stateEntry)
			{
				if (EntityState.Added == stateEntry.State || EntityState.Deleted == stateEntry.State)
				{
					EntityKey entityKey = stateEntry.EntityKey;
					EntitySet entitySet = (EntitySet)stateEntry.EntitySet;
					EntityType otherType = (EntityState.Added == stateEntry.State) ? UpdateTranslator.RelationshipConstraintValidator.GetEntityType(stateEntry.CurrentValues) : UpdateTranslator.RelationshipConstraintValidator.GetEntityType(stateEntry.OriginalValues);
					foreach (AssociationSet associationSet in this.GetReferencingAssocationSets(entitySet))
					{
						ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = associationSet.AssociationSetEnds;
						foreach (AssociationSetEnd associationSetEnd in associationSetEnds)
						{
							foreach (AssociationSetEnd associationSetEnd2 in associationSetEnds)
							{
								if (!object.ReferenceEquals(associationSetEnd2.CorrespondingAssociationEndMember, associationSetEnd.CorrespondingAssociationEndMember) && associationSetEnd2.EntitySet.EdmEquals(entitySet) && MetadataHelper.GetLowerBoundOfMultiplicity(associationSetEnd.CorrespondingAssociationEndMember.RelationshipMultiplicity) != 0 && MetadataHelper.GetEntityTypeForEnd(associationSetEnd2.CorrespondingAssociationEndMember).IsAssignableFrom(otherType))
								{
									UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship key = new UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship(entityKey, associationSetEnd.CorrespondingAssociationEndMember, associationSetEnd2.CorrespondingAssociationEndMember, associationSet, stateEntry);
									this.m_impliedRelationships.Add(key, stateEntry);
								}
							}
						}
					}
				}
			}

			// Token: 0x0600269C RID: 9884 RVA: 0x000B9344 File Offset: 0x000B7544
			private static EntityType GetEntityType(DbDataRecord dbDataRecord)
			{
				IExtendedDataRecord extendedDataRecord = dbDataRecord as IExtendedDataRecord;
				return (EntityType)extendedDataRecord.DataRecordInfo.RecordType.EdmType;
			}

			// Token: 0x0600269D RID: 9885 RVA: 0x000B9370 File Offset: 0x000B7570
			internal void RegisterAssociation(AssociationSet associationSet, IExtendedDataRecord record, IEntityStateEntry stateEntry)
			{
				Dictionary<string, EntityKey> dictionary = new Dictionary<string, EntityKey>(StringComparer.Ordinal);
				foreach (FieldMetadata fieldMetadata in record.DataRecordInfo.FieldMetadata)
				{
					string name = fieldMetadata.FieldType.Name;
					EntityKey value = (EntityKey)record.GetValue(fieldMetadata.Ordinal);
					dictionary.Add(name, value);
				}
				ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = associationSet.AssociationSetEnds;
				foreach (AssociationSetEnd associationSetEnd in associationSetEnds)
				{
					foreach (AssociationSetEnd associationSetEnd2 in associationSetEnds)
					{
						if (!object.ReferenceEquals(associationSetEnd2.CorrespondingAssociationEndMember, associationSetEnd.CorrespondingAssociationEndMember))
						{
							EntityKey toEntityKey = dictionary[associationSetEnd2.CorrespondingAssociationEndMember.Name];
							UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship relationship = new UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship(toEntityKey, associationSetEnd.CorrespondingAssociationEndMember, associationSetEnd2.CorrespondingAssociationEndMember, associationSet, stateEntry);
							this.AddExistingRelationship(relationship);
						}
					}
				}
			}

			// Token: 0x0600269E RID: 9886 RVA: 0x000B94C8 File Offset: 0x000B76C8
			internal void ValidateConstraints()
			{
				foreach (KeyValuePair<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, IEntityStateEntry> keyValuePair in this.m_impliedRelationships)
				{
					UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship key = keyValuePair.Key;
					IEntityStateEntry value = keyValuePair.Value;
					int num = this.GetDirectionalRelationshipCountDelta(key);
					if (EntityState.Deleted == value.State)
					{
						num = -num;
					}
					int lowerBoundOfMultiplicity = MetadataHelper.GetLowerBoundOfMultiplicity(key.FromEnd.RelationshipMultiplicity);
					int? upperBoundOfMultiplicity = MetadataHelper.GetUpperBoundOfMultiplicity(key.FromEnd.RelationshipMultiplicity);
					int num2 = (upperBoundOfMultiplicity != null) ? upperBoundOfMultiplicity.Value : num;
					if (num < lowerBoundOfMultiplicity || num > num2)
					{
						throw EntityUtil.UpdateRelationshipCardinalityConstraintViolation(key.AssociationSet.Name, lowerBoundOfMultiplicity, upperBoundOfMultiplicity, TypeHelpers.GetFullName(key.ToEntityKey.EntityContainerName, key.ToEntityKey.EntitySetName), num, key.FromEnd.Name, value);
					}
				}
				foreach (UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship directionalRelationship in this.m_existingRelationships.Keys)
				{
					int num3;
					int num4;
					directionalRelationship.GetCountsInEquivalenceSet(out num3, out num4);
					int num5 = Math.Abs(num3 - num4);
					int lowerBoundOfMultiplicity2 = MetadataHelper.GetLowerBoundOfMultiplicity(directionalRelationship.FromEnd.RelationshipMultiplicity);
					int? upperBoundOfMultiplicity2 = MetadataHelper.GetUpperBoundOfMultiplicity(directionalRelationship.FromEnd.RelationshipMultiplicity);
					if (upperBoundOfMultiplicity2 != null)
					{
						EntityState? entityState = null;
						int? num6 = null;
						if (num3 > upperBoundOfMultiplicity2.Value)
						{
							entityState = new EntityState?(EntityState.Added);
							num6 = new int?(num3);
						}
						else if (num4 > upperBoundOfMultiplicity2.Value)
						{
							entityState = new EntityState?(EntityState.Deleted);
							num6 = new int?(num4);
						}
						if (entityState != null)
						{
							throw new UpdateException(Strings.Update_RelationshipCardinalityViolation(upperBoundOfMultiplicity2.Value, entityState.Value, directionalRelationship.AssociationSet.ElementType.FullName, directionalRelationship.FromEnd.Name, directionalRelationship.ToEnd.Name, num6.Value), null, (from reln in directionalRelationship.GetEquivalenceSet()
							select reln.StateEntry).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
						}
					}
					if (1 == num5 && 1 == lowerBoundOfMultiplicity2 && 1 == upperBoundOfMultiplicity2)
					{
						bool flag = num3 > num4;
						IEntityStateEntry entityStateEntry;
						if (!this.m_impliedRelationships.TryGetValue(directionalRelationship, out entityStateEntry) || (flag && EntityState.Added != entityStateEntry.State) || (!flag && EntityState.Deleted != entityStateEntry.State))
						{
							string message = Strings.Update_MissingRequiredEntity(directionalRelationship.AssociationSet.Name, directionalRelationship.StateEntry.State, directionalRelationship.ToEnd.Name);
							throw EntityUtil.Update(message, null, new IEntityStateEntry[]
							{
								directionalRelationship.StateEntry
							});
						}
					}
				}
			}

			// Token: 0x0600269F RID: 9887 RVA: 0x000B97FC File Offset: 0x000B79FC
			private int GetDirectionalRelationshipCountDelta(UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship expectedRelationship)
			{
				UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship directionalRelationship;
				if (this.m_existingRelationships.TryGetValue(expectedRelationship, out directionalRelationship))
				{
					int num;
					int num2;
					directionalRelationship.GetCountsInEquivalenceSet(out num, out num2);
					return num - num2;
				}
				return 0;
			}

			// Token: 0x060026A0 RID: 9888 RVA: 0x000B9828 File Offset: 0x000B7A28
			private void AddExistingRelationship(UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship relationship)
			{
				UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship directionalRelationship;
				if (this.m_existingRelationships.TryGetValue(relationship, out directionalRelationship))
				{
					directionalRelationship.AddToEquivalenceSet(relationship);
					return;
				}
				this.m_existingRelationships.Add(relationship, relationship);
			}

			// Token: 0x060026A1 RID: 9889 RVA: 0x000B985C File Offset: 0x000B7A5C
			private IEnumerable<AssociationSet> GetReferencingAssocationSets(EntitySet entitySet)
			{
				List<AssociationSet> list;
				if (!this.m_referencingRelationshipSets.TryGetValue(entitySet, out list))
				{
					list = new List<AssociationSet>();
					EntityContainer entityContainer = entitySet.EntityContainer;
					foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
					{
						AssociationSet associationSet = entitySetBase as AssociationSet;
						if (associationSet != null && !associationSet.ElementType.IsForeignKey)
						{
							foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
							{
								if (associationSetEnd.EntitySet.Equals(entitySet))
								{
									list.Add(associationSet);
									break;
								}
							}
						}
					}
					this.m_referencingRelationshipSets.Add(entitySet, list);
				}
				return list;
			}

			// Token: 0x04000E79 RID: 3705
			private readonly Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship> m_existingRelationships;

			// Token: 0x04000E7A RID: 3706
			private readonly Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, IEntityStateEntry> m_impliedRelationships;

			// Token: 0x04000E7B RID: 3707
			private readonly Dictionary<EntitySet, List<AssociationSet>> m_referencingRelationshipSets;

			// Token: 0x0200041A RID: 1050
			private class DirectionalRelationship : IEquatable<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>
			{
				// Token: 0x060026A3 RID: 9891 RVA: 0x000B9944 File Offset: 0x000B7B44
				internal DirectionalRelationship(EntityKey toEntityKey, AssociationEndMember fromEnd, AssociationEndMember toEnd, AssociationSet associationSet, IEntityStateEntry stateEntry)
				{
					this.ToEntityKey = toEntityKey;
					this.FromEnd = fromEnd;
					this.ToEnd = toEnd;
					this.AssociationSet = associationSet;
					this.StateEntry = stateEntry;
					this._equivalenceSetLinkedListNext = this;
					this._hashCode = (toEntityKey.GetHashCode() ^ fromEnd.GetHashCode() ^ toEnd.GetHashCode() ^ associationSet.GetHashCode());
				}

				// Token: 0x060026A4 RID: 9892 RVA: 0x000B99A8 File Offset: 0x000B7BA8
				internal void AddToEquivalenceSet(UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship other)
				{
					UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship equivalenceSetLinkedListNext = this._equivalenceSetLinkedListNext;
					this._equivalenceSetLinkedListNext = other;
					other._equivalenceSetLinkedListNext = equivalenceSetLinkedListNext;
				}

				// Token: 0x060026A5 RID: 9893 RVA: 0x000B9AC4 File Offset: 0x000B7CC4
				internal IEnumerable<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship> GetEquivalenceSet()
				{
					UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship current = this;
					do
					{
						yield return current;
						current = current._equivalenceSetLinkedListNext;
					}
					while (!object.ReferenceEquals(current, this));
					yield break;
				}

				// Token: 0x060026A6 RID: 9894 RVA: 0x000B9AE4 File Offset: 0x000B7CE4
				internal void GetCountsInEquivalenceSet(out int addedCount, out int deletedCount)
				{
					addedCount = 0;
					deletedCount = 0;
					UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship directionalRelationship = this;
					do
					{
						if (directionalRelationship.StateEntry.State == EntityState.Added)
						{
							addedCount++;
						}
						else if (directionalRelationship.StateEntry.State == EntityState.Deleted)
						{
							deletedCount++;
						}
						directionalRelationship = directionalRelationship._equivalenceSetLinkedListNext;
					}
					while (!object.ReferenceEquals(directionalRelationship, this));
				}

				// Token: 0x060026A7 RID: 9895 RVA: 0x000B9B33 File Offset: 0x000B7D33
				public override int GetHashCode()
				{
					return this._hashCode;
				}

				// Token: 0x060026A8 RID: 9896 RVA: 0x000B9B3C File Offset: 0x000B7D3C
				public bool Equals(UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship other)
				{
					return object.ReferenceEquals(this, other) || (other != null && !(this.ToEntityKey != other.ToEntityKey) && this.AssociationSet == other.AssociationSet && this.ToEnd == other.ToEnd && this.FromEnd == other.FromEnd);
				}

				// Token: 0x060026A9 RID: 9897 RVA: 0x000B9B9F File Offset: 0x000B7D9F
				public override bool Equals(object obj)
				{
					return this.Equals(obj as UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship);
				}

				// Token: 0x060026AA RID: 9898 RVA: 0x000B9BB0 File Offset: 0x000B7DB0
				public override string ToString()
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}.{1}-->{2}: {3}", new object[]
					{
						this.AssociationSet.Name,
						this.FromEnd.Name,
						this.ToEnd.Name,
						StringUtil.BuildDelimitedList<EntityKeyMember>(this.ToEntityKey.EntityKeyValues, null, null)
					});
				}

				// Token: 0x04000E7D RID: 3709
				internal readonly EntityKey ToEntityKey;

				// Token: 0x04000E7E RID: 3710
				internal readonly AssociationEndMember FromEnd;

				// Token: 0x04000E7F RID: 3711
				internal readonly AssociationEndMember ToEnd;

				// Token: 0x04000E80 RID: 3712
				internal readonly IEntityStateEntry StateEntry;

				// Token: 0x04000E81 RID: 3713
				internal readonly AssociationSet AssociationSet;

				// Token: 0x04000E82 RID: 3714
				private UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship _equivalenceSetLinkedListNext;

				// Token: 0x04000E83 RID: 3715
				private readonly int _hashCode;
			}
		}
	}
}
