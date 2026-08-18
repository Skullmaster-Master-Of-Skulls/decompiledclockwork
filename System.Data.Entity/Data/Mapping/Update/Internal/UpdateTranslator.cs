using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Globalization;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002CC RID: 716
	internal class UpdateTranslator
	{
		// Token: 0x06002A1E RID: 10782 RVA: 0x000A48B0 File Offset: 0x000A2AB0
		private UpdateTranslator(IEntityStateManager stateManager, MetadataWorkspace metadataWorkspace, EntityConnection connection, int? commandTimeout)
		{
			EntityUtil.CheckArgumentNull<IEntityStateManager>(stateManager, "stateManager");
			EntityUtil.CheckArgumentNull<MetadataWorkspace>(metadataWorkspace, "metadataWorkspace");
			EntityUtil.CheckArgumentNull<EntityConnection>(connection, "connection");
			this.m_changes = new Dictionary<EntitySetBase, ChangeNode>();
			this.m_functionChanges = new Dictionary<EntitySetBase, List<ExtractedStateEntry>>();
			this.m_stateEntries = new List<IEntityStateEntry>();
			this.m_knownEntityKeys = new Set<EntityKey>();
			this.m_requiredEntities = new Dictionary<EntityKey, AssociationSet>();
			this.m_optionalEntities = new Set<EntityKey>();
			this.m_includedValueEntities = new Set<EntityKey>();
			this.m_metadataWorkspace = metadataWorkspace;
			this.m_viewLoader = metadataWorkspace.GetUpdateViewLoader();
			this.m_stateManager = stateManager;
			this.m_recordConverter = new RecordConverter(this);
			this.m_constraintValidator = new UpdateTranslator.RelationshipConstraintValidator(this);
			this.m_providerServices = DbProviderServices.GetProviderServices(connection.StoreProviderFactory);
			this.m_connection = connection;
			this.m_commandTimeout = commandTimeout;
			this.m_extractorMetadata = new Dictionary<Tuple<EntitySetBase, StructuralType>, ExtractorMetadata>();
			this.KeyManager = new KeyManager(this);
			this.KeyComparer = CompositeKey.CreateComparer(this.KeyManager);
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x000A49AE File Offset: 0x000A2BAE
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this.m_metadataWorkspace;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x000A49B6 File Offset: 0x000A2BB6
		internal ViewLoader ViewLoader
		{
			get
			{
				return this.m_viewLoader;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x000A49BE File Offset: 0x000A2BBE
		internal RecordConverter RecordConverter
		{
			get
			{
				return this.m_recordConverter;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x000A49C6 File Offset: 0x000A2BC6
		internal int? CommandTimeout
		{
			get
			{
				return this.m_commandTimeout;
			}
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000A49D0 File Offset: 0x000A2BD0
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

		// Token: 0x06002A24 RID: 10788 RVA: 0x000A4BA0 File Offset: 0x000A2DA0
		private void RegisterEntityReferentialConstraints(IEntityStateEntry stateEntry, bool currentValues)
		{
			IExtendedDataRecord extendedDataRecord;
			if (!currentValues)
			{
				extendedDataRecord = (IExtendedDataRecord)stateEntry.OriginalValues;
			}
			else
			{
				IExtendedDataRecord currentValues2 = stateEntry.CurrentValues;
				extendedDataRecord = currentValues2;
			}
			IExtendedDataRecord extendedDataRecord2 = extendedDataRecord;
			EntitySet entitySet = (EntitySet)stateEntry.EntitySet;
			EntityKey entityKey = stateEntry.EntityKey;
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in entitySet.ForeignKeyDependents)
			{
				AssociationSet item = tuple.Item1;
				ReferentialConstraint item2 = tuple.Item2;
				EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)item2.ToRole);
				if (entityTypeForEnd.IsAssignableFrom(extendedDataRecord2.DataRecordInfo.RecordType.EdmType))
				{
					EntityKey entityKey2 = null;
					if (!currentValues || !this.m_stateManager.TryGetReferenceKey(entityKey, (AssociationEndMember)item2.FromRole, out entityKey2))
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
							int ordinal = extendedDataRecord2.GetOrdinal(item2.ToProperties[index].Name);
							if (extendedDataRecord2.IsDBNull(ordinal))
							{
								flag = true;
								break;
							}
							array[i] = extendedDataRecord2.GetValue(ordinal);
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
						if (!this.m_stateManager.TryGetEntityStateEntry(entityKey2, out entityStateEntry) && currentValues && this.KeyManager.TryGetTempKey(entityKey2, out entityKey3))
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
						this.AddValidAncillaryKey(entityKey2, this.m_optionalEntities);
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

		// Token: 0x06002A25 RID: 10789 RVA: 0x000A4EFC File Offset: 0x000A30FC
		private static int GetKeyMemberOffset(RelationshipEndMember role, EdmProperty property, out int keyMemberCount)
		{
			RefType refType = (RefType)role.TypeUsage.EdmType;
			EntityType entityType = (EntityType)refType.ElementType;
			keyMemberCount = entityType.KeyMembers.Count;
			return entityType.KeyMembers.IndexOf(property);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000A4F3F File Offset: 0x000A313F
		internal IEnumerable<IEntityStateEntry> GetRelationships(EntityKey entityKey)
		{
			return this.m_stateManager.FindRelationshipsByKey(entityKey);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000A4F50 File Offset: 0x000A3150
		internal static int Update(IEntityStateManager stateManager, IEntityAdapter adapter)
		{
			EntityConnection entityConnection = (EntityConnection)adapter.Connection;
			MetadataWorkspace metadataWorkspace = entityConnection.GetMetadataWorkspace();
			int? commandTimeout = adapter.CommandTimeout;
			UpdateTranslator updateTranslator = new UpdateTranslator(stateManager, metadataWorkspace, entityConnection, commandTimeout);
			Dictionary<int, object> identifierValues = new Dictionary<int, object>();
			List<KeyValuePair<PropagatorResult, object>> generatedValues = new List<KeyValuePair<PropagatorResult, object>>();
			IEnumerable<UpdateCommand> enumerable = updateTranslator.ProduceCommands();
			UpdateCommand source = null;
			try
			{
				foreach (UpdateCommand updateCommand in enumerable)
				{
					source = updateCommand;
					long rowsAffected = updateCommand.Execute(updateTranslator, entityConnection, identifierValues, generatedValues);
					updateTranslator.ValidateRowsAffected(rowsAffected, source);
				}
			}
			catch (Exception ex)
			{
				if (UpdateTranslator.RequiresContext(ex))
				{
					throw EntityUtil.Update(Strings.Update_GeneralExecutionException, ex, updateTranslator.DetermineStateEntriesFromSource(source));
				}
				throw;
			}
			updateTranslator.BackPropagateServerGen(generatedValues);
			return updateTranslator.AcceptChanges(adapter);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000A5038 File Offset: 0x000A3238
		private IEnumerable<UpdateCommand> ProduceCommands()
		{
			this.PullModifiedEntriesFromStateManager();
			this.PullUnchangedEntriesFromStateManager();
			this.m_constraintValidator.ValidateConstraints();
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

		// Token: 0x06002A29 RID: 10793 RVA: 0x000A509C File Offset: 0x000A329C
		private void ValidateRowsAffected(long rowsAffected, UpdateCommand source)
		{
			if (rowsAffected == 0L)
			{
				IEnumerable<IEntityStateEntry> stateEntries = this.DetermineStateEntriesFromSource(source);
				throw EntityUtil.UpdateConcurrency(rowsAffected, null, stateEntries);
			}
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000A50BD File Offset: 0x000A32BD
		private IEnumerable<IEntityStateEntry> DetermineStateEntriesFromSource(UpdateCommand source)
		{
			if (source == null)
			{
				return Enumerable.Empty<IEntityStateEntry>();
			}
			return source.GetStateEntries(this);
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x000A50D0 File Offset: 0x000A32D0
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
					this.SetServerGenValue(key, value);
				}
				else
				{
					foreach (int identifier in this.KeyManager.GetDependents(key.Identifier))
					{
						if (this.KeyManager.TryGetIdentifierOwner(identifier, out key))
						{
							this.SetServerGenValue(key, value);
						}
					}
				}
			}
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x000A51CC File Offset: 0x000A33CC
		private void SetServerGenValue(PropagatorResult context, object value)
		{
			if (context.RecordOrdinal != -1)
			{
				CurrentValueRecord record = context.Record;
				IExtendedDataRecord extendedDataRecord = record;
				EdmMember fieldType = extendedDataRecord.DataRecordInfo.FieldMetadata[context.RecordOrdinal].FieldType;
				value = (value ?? DBNull.Value);
				value = this.AlignReturnValue(value, fieldType, context);
				record.SetValue(context.RecordOrdinal, value);
			}
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000A5230 File Offset: 0x000A3430
		private object AlignReturnValue(object value, EdmMember member, PropagatorResult context)
		{
			if (DBNull.Value.Equals(value))
			{
				if (BuiltInTypeKind.EdmProperty == member.BuiltInTypeKind && !((EdmProperty)member).Nullable)
				{
					throw EntityUtil.Update(Strings.Update_NullReturnValueForNonNullableMember(member.Name, member.DeclaringType.FullName), null, new IEntityStateEntry[0]);
				}
			}
			else if (!Helper.IsSpatialType(member.TypeUsage))
			{
				Type type = null;
				Type clrEquivalentType;
				if (Helper.IsEnumType(member.TypeUsage.EdmType))
				{
					PrimitiveType primitiveType = Helper.AsPrimitive(member.TypeUsage.EdmType);
					type = context.Record.GetFieldType(context.RecordOrdinal);
					clrEquivalentType = primitiveType.ClrEquivalentType;
				}
				else
				{
					PrimitiveType primitiveType2 = (PrimitiveType)member.TypeUsage.EdmType;
					clrEquivalentType = primitiveType2.ClrEquivalentType;
				}
				try
				{
					value = Convert.ChangeType(value, clrEquivalentType, CultureInfo.InvariantCulture);
					if (type != null)
					{
						value = Enum.ToObject(type, value);
					}
				}
				catch (Exception ex)
				{
					if (UpdateTranslator.RequiresContext(ex))
					{
						Type type2 = type ?? clrEquivalentType;
						throw EntityUtil.Update(Strings.Update_ReturnValueHasUnexpectedType(value.GetType().FullName, type2.FullName, member.Name, member.DeclaringType.FullName), ex, new IEntityStateEntry[0]);
					}
					throw;
				}
			}
			return value;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000A5374 File Offset: 0x000A3574
		private int AcceptChanges(IEntityAdapter adapter)
		{
			int num = 0;
			foreach (IEntityStateEntry entityStateEntry in this.m_stateEntries)
			{
				if (EntityState.Unchanged != entityStateEntry.State)
				{
					if (adapter.AcceptChangesDuringUpdate)
					{
						entityStateEntry.AcceptChanges();
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000A53E0 File Offset: 0x000A35E0
		private IEnumerable<EntitySetBase> GetDynamicModifiedExtents()
		{
			return this.m_changes.Keys;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000A53ED File Offset: 0x000A35ED
		private IEnumerable<EntitySetBase> GetFunctionModifiedExtents()
		{
			return this.m_functionChanges.Keys;
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x000A53FA File Offset: 0x000A35FA
		private IEnumerable<UpdateCommand> ProduceDynamicCommands()
		{
			UpdateCompiler updateCompiler = new UpdateCompiler(this);
			Set<EntitySet> set = new Set<EntitySet>();
			foreach (EntitySetBase entitySetBase in this.GetDynamicModifiedExtents())
			{
				Set<EntitySet> affectedTables = this.m_viewLoader.GetAffectedTables(entitySetBase, this.m_metadataWorkspace);
				if (affectedTables.Count == 0)
				{
					throw EntityUtil.Update(Strings.Update_MappingNotFound(entitySetBase.Name), null, new IEntityStateEntry[0]);
				}
				foreach (EntitySet element in affectedTables)
				{
					set.Add(element);
				}
			}
			foreach (EntitySet entitySet in set)
			{
				DbQueryCommandTree cqtView = this.m_connection.GetMetadataWorkspace().GetCqtView(entitySet);
				ChangeNode changeNode = Propagator.Propagate(this, entitySet, cqtView);
				TableChangeProcessor tableChangeProcessor = new TableChangeProcessor(entitySet);
				foreach (UpdateCommand updateCommand in tableChangeProcessor.CompileCommands(changeNode, updateCompiler))
				{
					yield return updateCommand;
				}
				List<UpdateCommand>.Enumerator enumerator4 = default(List<UpdateCommand>.Enumerator);
			}
			HashSet<EntitySet>.Enumerator enumerator3 = default(HashSet<EntitySet>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x000A540C File Offset: 0x000A360C
		internal DbCommandDefinition GenerateCommandDefinition(StorageModificationFunctionMapping functionMapping)
		{
			if (this.m_modificationFunctionCommandDefinitions == null)
			{
				this.m_modificationFunctionCommandDefinitions = new Dictionary<StorageModificationFunctionMapping, DbCommandDefinition>();
			}
			DbCommandDefinition result;
			if (!this.m_modificationFunctionCommandDefinitions.TryGetValue(functionMapping, out result))
			{
				TypeUsage resultType = null;
				if (functionMapping.ResultBindings != null && 0 < functionMapping.ResultBindings.Count)
				{
					List<EdmProperty> list = new List<EdmProperty>(functionMapping.ResultBindings.Count);
					foreach (StorageModificationFunctionResultBinding storageModificationFunctionResultBinding in functionMapping.ResultBindings)
					{
						list.Add(new EdmProperty(storageModificationFunctionResultBinding.ColumnName, storageModificationFunctionResultBinding.Property.TypeUsage));
					}
					RowType elementType = new RowType(list);
					CollectionType edmType = new CollectionType(elementType);
					resultType = TypeUsage.Create(edmType);
				}
				IEnumerable<KeyValuePair<string, TypeUsage>> parameters = from paramInfo in functionMapping.Function.Parameters
				select new KeyValuePair<string, TypeUsage>(paramInfo.Name, paramInfo.TypeUsage);
				DbFunctionCommandTree commandTree = new DbFunctionCommandTree(this.m_metadataWorkspace, DataSpace.SSpace, functionMapping.Function, resultType, parameters);
				result = this.m_providerServices.CreateCommandDefinition(commandTree);
			}
			return result;
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000A5538 File Offset: 0x000A3738
		private IEnumerable<UpdateCommand> ProduceFunctionCommands()
		{
			foreach (EntitySetBase extent in this.GetFunctionModifiedExtents())
			{
				ModificationFunctionMappingTranslator translator = this.m_viewLoader.GetFunctionMappingTranslator(extent, this.m_metadataWorkspace);
				if (translator != null)
				{
					foreach (ExtractedStateEntry stateEntry in this.GetExtentFunctionModifications(extent))
					{
						FunctionUpdateCommand functionUpdateCommand = translator.Translate(this, stateEntry);
						if (functionUpdateCommand != null)
						{
							yield return functionUpdateCommand;
						}
					}
					List<ExtractedStateEntry>.Enumerator enumerator2 = default(List<ExtractedStateEntry>.Enumerator);
				}
				translator = null;
			}
			IEnumerator<EntitySetBase> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000A5548 File Offset: 0x000A3748
		internal ExtractorMetadata GetExtractorMetadata(EntitySetBase entitySetBase, StructuralType type)
		{
			Tuple<EntitySetBase, StructuralType> key = Tuple.Create<EntitySetBase, StructuralType>(entitySetBase, type);
			ExtractorMetadata extractorMetadata;
			if (!this.m_extractorMetadata.TryGetValue(key, out extractorMetadata))
			{
				extractorMetadata = new ExtractorMetadata(entitySetBase, type, this);
				this.m_extractorMetadata.Add(key, extractorMetadata);
			}
			return extractorMetadata;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000A5584 File Offset: 0x000A3784
		private UpdateException DependencyOrderingError(IEnumerable<UpdateCommand> remainder)
		{
			HashSet<IEntityStateEntry> hashSet = new HashSet<IEntityStateEntry>();
			foreach (UpdateCommand updateCommand in remainder)
			{
				hashSet.UnionWith(updateCommand.GetStateEntries(this));
			}
			throw EntityUtil.Update(Strings.Update_ConstraintCycle, null, hashSet);
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000A55E4 File Offset: 0x000A37E4
		internal DbCommand CreateCommand(DbModificationCommandTree commandTree)
		{
			DbCommand result;
			try
			{
				result = this.m_providerServices.CreateCommand(commandTree);
			}
			catch (Exception ex)
			{
				if (UpdateTranslator.RequiresContext(ex))
				{
					throw EntityUtil.CommandCompilation(Strings.EntityClient_CommandDefinitionPreparationFailed, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000A5628 File Offset: 0x000A3828
		internal void SetParameterValue(DbParameter parameter, TypeUsage typeUsage, object value)
		{
			this.m_providerServices.SetParameterValue(parameter, typeUsage, value);
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000A5638 File Offset: 0x000A3838
		internal static bool RequiresContext(Exception e)
		{
			return EntityUtil.IsCatchableExceptionType(e) && !(e is UpdateException) && !(e is ProviderIncompatibleException);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000A565C File Offset: 0x000A385C
		private void PullModifiedEntriesFromStateManager()
		{
			foreach (IEntityStateEntry entityStateEntry in this.m_stateManager.GetEntityStateEntries(EntityState.Added))
			{
				if (!entityStateEntry.IsRelationship && !entityStateEntry.IsKeyEntry)
				{
					this.KeyManager.RegisterKeyValueForAddedEntity(entityStateEntry);
				}
			}
			foreach (IEntityStateEntry stateEntry in this.m_stateManager.GetEntityStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified))
			{
				this.RegisterReferentialConstraints(stateEntry);
			}
			foreach (IEntityStateEntry stateEntry2 in this.m_stateManager.GetEntityStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified))
			{
				this.LoadStateEntry(stateEntry2);
			}
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000A5754 File Offset: 0x000A3954
		private void PullUnchangedEntriesFromStateManager()
		{
			foreach (KeyValuePair<EntityKey, AssociationSet> keyValuePair in this.m_requiredEntities)
			{
				EntityKey key = keyValuePair.Key;
				if (!this.m_knownEntityKeys.Contains(key))
				{
					IEntityStateEntry entityStateEntry;
					if (!this.m_stateManager.TryGetEntityStateEntry(key, out entityStateEntry) || entityStateEntry.IsKeyEntry)
					{
						throw EntityUtil.UpdateMissingEntity(keyValuePair.Value.Name, TypeHelpers.GetFullName(key.EntityContainerName, key.EntitySetName));
					}
					this.LoadStateEntry(entityStateEntry);
				}
			}
			foreach (EntityKey entityKey in this.m_optionalEntities)
			{
				IEntityStateEntry entityStateEntry2;
				if (!this.m_knownEntityKeys.Contains(entityKey) && this.m_stateManager.TryGetEntityStateEntry(entityKey, out entityStateEntry2) && !entityStateEntry2.IsKeyEntry)
				{
					this.LoadStateEntry(entityStateEntry2);
				}
			}
			foreach (EntityKey entityKey2 in this.m_includedValueEntities)
			{
				IEntityStateEntry stateEntry;
				if (!this.m_knownEntityKeys.Contains(entityKey2) && this.m_stateManager.TryGetEntityStateEntry(entityKey2, out stateEntry))
				{
					PropagatorResult propagatorResult = this.m_recordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.NoneModified);
				}
			}
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000A58D8 File Offset: 0x000A3AD8
		private void ValidateAndRegisterStateEntry(IEntityStateEntry stateEntry)
		{
			EntityUtil.CheckArgumentNull<IEntityStateEntry>(stateEntry, "stateEntry");
			EntitySetBase entitySet = stateEntry.EntitySet;
			if (entitySet == null)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.InvalidStateEntry, 1);
			}
			EntityKey entityKey = stateEntry.EntityKey;
			IExtendedDataRecord extendedDataRecord = null;
			if (((EntityState.Unchanged | EntityState.Added | EntityState.Modified) & stateEntry.State) != (EntityState)0)
			{
				extendedDataRecord = stateEntry.CurrentValues;
				this.ValidateRecord(entitySet, extendedDataRecord, stateEntry);
			}
			if (((EntityState.Unchanged | EntityState.Deleted | EntityState.Modified) & stateEntry.State) != (EntityState)0)
			{
				extendedDataRecord = (IExtendedDataRecord)stateEntry.OriginalValues;
				this.ValidateRecord(entitySet, extendedDataRecord, stateEntry);
			}
			AssociationSet associationSet = entitySet as AssociationSet;
			if (associationSet != null)
			{
				AssociationSetMetadata associationSetMetadata = this.m_viewLoader.GetAssociationSetMetadata(associationSet, this.m_metadataWorkspace);
				if (associationSetMetadata.HasEnds)
				{
					foreach (FieldMetadata fieldMetadata in extendedDataRecord.DataRecordInfo.FieldMetadata)
					{
						EntityKey key = (EntityKey)extendedDataRecord.GetValue(fieldMetadata.Ordinal);
						AssociationEndMember element = (AssociationEndMember)fieldMetadata.FieldType;
						if (associationSetMetadata.RequiredEnds.Contains(element))
						{
							if (!this.m_requiredEntities.ContainsKey(key))
							{
								this.m_requiredEntities.Add(key, associationSet);
							}
						}
						else if (associationSetMetadata.OptionalEnds.Contains(element))
						{
							this.AddValidAncillaryKey(key, this.m_optionalEntities);
						}
						else if (associationSetMetadata.IncludedValueEnds.Contains(element))
						{
							this.AddValidAncillaryKey(key, this.m_includedValueEntities);
						}
					}
				}
				this.m_constraintValidator.RegisterAssociation(associationSet, extendedDataRecord, stateEntry);
			}
			else
			{
				this.m_constraintValidator.RegisterEntity(stateEntry);
			}
			this.m_stateEntries.Add(stateEntry);
			if (entityKey != null)
			{
				this.m_knownEntityKeys.Add(entityKey);
			}
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000A5A88 File Offset: 0x000A3C88
		private void AddValidAncillaryKey(EntityKey key, Set<EntityKey> keySet)
		{
			IEntityStateEntry entityStateEntry;
			if (this.m_stateManager.TryGetEntityStateEntry(key, out entityStateEntry) && !entityStateEntry.IsKeyEntry && entityStateEntry.State == EntityState.Unchanged)
			{
				keySet.Add(key);
			}
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000A5AC0 File Offset: 0x000A3CC0
		private void ValidateRecord(EntitySetBase extent, IExtendedDataRecord record, IEntityStateEntry entry)
		{
			DataRecordInfo dataRecordInfo;
			if (record == null || (dataRecordInfo = record.DataRecordInfo) == null || dataRecordInfo.RecordType == null)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.InvalidStateEntry, 2);
			}
			UpdateTranslator.VerifyExtent(this.MetadataWorkspace, extent);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000A5AFC File Offset: 0x000A3CFC
		private static void VerifyExtent(MetadataWorkspace workspace, EntitySetBase extent)
		{
			EntityContainer entityContainer = extent.EntityContainer;
			EntityContainer entityContainer2 = null;
			if (entityContainer != null)
			{
				workspace.TryGetEntityContainer(entityContainer.Name, entityContainer.DataSpace, out entityContainer2);
			}
			if (entityContainer == null || entityContainer2 == null || entityContainer != entityContainer2)
			{
				throw EntityUtil.Update(Strings.Update_WorkspaceMismatch, null, new IEntityStateEntry[0]);
			}
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000A5B48 File Offset: 0x000A3D48
		private void LoadStateEntry(IEntityStateEntry stateEntry)
		{
			this.ValidateAndRegisterStateEntry(stateEntry);
			ExtractedStateEntry extractedStateEntry = new ExtractedStateEntry(this, stateEntry);
			EntitySetBase entitySet = stateEntry.EntitySet;
			if (this.m_viewLoader.GetFunctionMappingTranslator(entitySet, this.m_metadataWorkspace) == null)
			{
				ChangeNode extentModifications = this.GetExtentModifications(entitySet);
				if (extractedStateEntry.Original != null)
				{
					extentModifications.Deleted.Add(extractedStateEntry.Original);
				}
				if (extractedStateEntry.Current != null)
				{
					extentModifications.Inserted.Add(extractedStateEntry.Current);
					return;
				}
			}
			else
			{
				List<ExtractedStateEntry> extentFunctionModifications = this.GetExtentFunctionModifications(entitySet);
				extentFunctionModifications.Add(extractedStateEntry);
			}
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000A5BCC File Offset: 0x000A3DCC
		internal ChangeNode GetExtentModifications(EntitySetBase extent)
		{
			EntityUtil.CheckArgumentNull<EntitySetBase>(extent, "extent");
			ChangeNode changeNode;
			if (!this.m_changes.TryGetValue(extent, out changeNode))
			{
				changeNode = new ChangeNode(TypeUsage.Create(extent.ElementType));
				this.m_changes.Add(extent, changeNode);
			}
			return changeNode;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000A5C14 File Offset: 0x000A3E14
		internal List<ExtractedStateEntry> GetExtentFunctionModifications(EntitySetBase extent)
		{
			EntityUtil.CheckArgumentNull<EntitySetBase>(extent, "extent");
			List<ExtractedStateEntry> list;
			if (!this.m_functionChanges.TryGetValue(extent, out list))
			{
				list = new List<ExtractedStateEntry>();
				this.m_functionChanges.Add(extent, list);
			}
			return list;
		}

		// Token: 0x040012C3 RID: 4803
		private readonly Dictionary<EntitySetBase, ChangeNode> m_changes;

		// Token: 0x040012C4 RID: 4804
		private readonly Dictionary<EntitySetBase, List<ExtractedStateEntry>> m_functionChanges;

		// Token: 0x040012C5 RID: 4805
		private readonly List<IEntityStateEntry> m_stateEntries;

		// Token: 0x040012C6 RID: 4806
		private readonly Set<EntityKey> m_knownEntityKeys;

		// Token: 0x040012C7 RID: 4807
		private readonly Dictionary<EntityKey, AssociationSet> m_requiredEntities;

		// Token: 0x040012C8 RID: 4808
		private readonly Set<EntityKey> m_optionalEntities;

		// Token: 0x040012C9 RID: 4809
		private readonly Set<EntityKey> m_includedValueEntities;

		// Token: 0x040012CA RID: 4810
		private readonly MetadataWorkspace m_metadataWorkspace;

		// Token: 0x040012CB RID: 4811
		private readonly ViewLoader m_viewLoader;

		// Token: 0x040012CC RID: 4812
		private readonly IEntityStateManager m_stateManager;

		// Token: 0x040012CD RID: 4813
		private readonly RecordConverter m_recordConverter;

		// Token: 0x040012CE RID: 4814
		private readonly UpdateTranslator.RelationshipConstraintValidator m_constraintValidator;

		// Token: 0x040012CF RID: 4815
		private readonly DbProviderServices m_providerServices;

		// Token: 0x040012D0 RID: 4816
		private readonly EntityConnection m_connection;

		// Token: 0x040012D1 RID: 4817
		private readonly int? m_commandTimeout;

		// Token: 0x040012D2 RID: 4818
		private Dictionary<StorageModificationFunctionMapping, DbCommandDefinition> m_modificationFunctionCommandDefinitions;

		// Token: 0x040012D3 RID: 4819
		private readonly Dictionary<Tuple<EntitySetBase, StructuralType>, ExtractorMetadata> m_extractorMetadata;

		// Token: 0x040012D4 RID: 4820
		private static readonly List<string> s_emptyMemberList = new List<string>();

		// Token: 0x040012D5 RID: 4821
		internal readonly KeyManager KeyManager;

		// Token: 0x040012D6 RID: 4822
		internal readonly IEqualityComparer<CompositeKey> KeyComparer;

		// Token: 0x02000626 RID: 1574
		private class RelationshipConstraintValidator
		{
			// Token: 0x0600431A RID: 17178 RVA: 0x000F4102 File Offset: 0x000F2302
			internal RelationshipConstraintValidator(UpdateTranslator updateTranslator)
			{
				this.m_existingRelationships = new Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>(EqualityComparer<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>.Default);
				this.m_impliedRelationships = new Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, IEntityStateEntry>(EqualityComparer<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>.Default);
				this.m_referencingRelationshipSets = new Dictionary<EntitySet, List<AssociationSet>>(EqualityComparer<EntitySet>.Default);
				this.m_updateTranslator = updateTranslator;
			}

			// Token: 0x0600431B RID: 17179 RVA: 0x000F4144 File Offset: 0x000F2344
			internal void RegisterEntity(IEntityStateEntry stateEntry)
			{
				EntityUtil.CheckArgumentNull<IEntityStateEntry>(stateEntry, "stateEntry");
				if (EntityState.Added == stateEntry.State || EntityState.Deleted == stateEntry.State)
				{
					EntityKey toEntityKey = EntityUtil.CheckArgumentNull<EntityKey>(stateEntry.EntityKey, "stateEntry.EntityKey");
					EntitySet entitySet = (EntitySet)stateEntry.EntitySet;
					EntityType otherType = (EntityState.Added == stateEntry.State) ? UpdateTranslator.RelationshipConstraintValidator.GetEntityType(stateEntry.CurrentValues) : UpdateTranslator.RelationshipConstraintValidator.GetEntityType(stateEntry.OriginalValues);
					foreach (AssociationSet associationSet in this.GetReferencingAssocationSets(entitySet))
					{
						ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = associationSet.AssociationSetEnds;
						foreach (AssociationSetEnd associationSetEnd in associationSetEnds)
						{
							foreach (AssociationSetEnd associationSetEnd2 in associationSetEnds)
							{
								if (associationSetEnd2.CorrespondingAssociationEndMember != associationSetEnd.CorrespondingAssociationEndMember && associationSetEnd2.EntitySet.EdmEquals(entitySet) && MetadataHelper.GetLowerBoundOfMultiplicity(associationSetEnd.CorrespondingAssociationEndMember.RelationshipMultiplicity) != 0 && MetadataHelper.GetEntityTypeForEnd(associationSetEnd2.CorrespondingAssociationEndMember).IsAssignableFrom(otherType))
								{
									UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship key = new UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship(toEntityKey, associationSetEnd.CorrespondingAssociationEndMember, associationSetEnd2.CorrespondingAssociationEndMember, associationSet, stateEntry);
									this.m_impliedRelationships.Add(key, stateEntry);
								}
							}
						}
					}
				}
			}

			// Token: 0x0600431C RID: 17180 RVA: 0x000F42E0 File Offset: 0x000F24E0
			private static EntityType GetEntityType(DbDataRecord dbDataRecord)
			{
				IExtendedDataRecord extendedDataRecord = dbDataRecord as IExtendedDataRecord;
				return (EntityType)extendedDataRecord.DataRecordInfo.RecordType.EdmType;
			}

			// Token: 0x0600431D RID: 17181 RVA: 0x000F430C File Offset: 0x000F250C
			internal void RegisterAssociation(AssociationSet associationSet, IExtendedDataRecord record, IEntityStateEntry stateEntry)
			{
				EntityUtil.CheckArgumentNull<AssociationSet>(associationSet, "relationshipSet");
				EntityUtil.CheckArgumentNull<IExtendedDataRecord>(record, "record");
				EntityUtil.CheckArgumentNull<IEntityStateEntry>(stateEntry, "stateEntry");
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
						if (associationSetEnd2.CorrespondingAssociationEndMember != associationSetEnd.CorrespondingAssociationEndMember)
						{
							EntityKey toEntityKey = dictionary[associationSetEnd2.CorrespondingAssociationEndMember.Name];
							UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship relationship = new UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship(toEntityKey, associationSetEnd.CorrespondingAssociationEndMember, associationSetEnd2.CorrespondingAssociationEndMember, associationSet, stateEntry);
							this.AddExistingRelationship(relationship);
						}
					}
				}
			}

			// Token: 0x0600431E RID: 17182 RVA: 0x000F4474 File Offset: 0x000F2674
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
							throw EntityUtil.Update(Strings.Update_RelationshipCardinalityViolation(upperBoundOfMultiplicity2.Value, entityState.Value, directionalRelationship.AssociationSet.ElementType.FullName, directionalRelationship.FromEnd.Name, directionalRelationship.ToEnd.Name, num6.Value), null, from reln in directionalRelationship.GetEquivalenceSet()
							select reln.StateEntry);
						}
					}
					if (1 == num5 && 1 == lowerBoundOfMultiplicity2)
					{
						int num7 = 1;
						int? num8 = upperBoundOfMultiplicity2;
						if (num7 == num8.GetValueOrDefault() & num8 != null)
						{
							bool flag = num3 > num4;
							IEntityStateEntry entityStateEntry;
							if (!this.m_impliedRelationships.TryGetValue(directionalRelationship, out entityStateEntry) || (flag && EntityState.Added != entityStateEntry.State) || (!flag && EntityState.Deleted != entityStateEntry.State))
							{
								throw EntityUtil.UpdateEntityMissingConstraintViolation(directionalRelationship.AssociationSet.Name, directionalRelationship.ToEnd.Name, directionalRelationship.StateEntry);
							}
						}
					}
				}
			}

			// Token: 0x0600431F RID: 17183 RVA: 0x000F4770 File Offset: 0x000F2970
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

			// Token: 0x06004320 RID: 17184 RVA: 0x000F479C File Offset: 0x000F299C
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

			// Token: 0x06004321 RID: 17185 RVA: 0x000F47D0 File Offset: 0x000F29D0
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

			// Token: 0x04001E6F RID: 7791
			private readonly Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship> m_existingRelationships;

			// Token: 0x04001E70 RID: 7792
			private readonly Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, IEntityStateEntry> m_impliedRelationships;

			// Token: 0x04001E71 RID: 7793
			private readonly Dictionary<EntitySet, List<AssociationSet>> m_referencingRelationshipSets;

			// Token: 0x04001E72 RID: 7794
			private readonly UpdateTranslator m_updateTranslator;

			// Token: 0x0200077F RID: 1919
			private class DirectionalRelationship : IEquatable<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>
			{
				// Token: 0x060048BD RID: 18621 RVA: 0x00105C3C File Offset: 0x00103E3C
				internal DirectionalRelationship(EntityKey toEntityKey, AssociationEndMember fromEnd, AssociationEndMember toEnd, AssociationSet associationSet, IEntityStateEntry stateEntry)
				{
					this.ToEntityKey = EntityUtil.CheckArgumentNull<EntityKey>(toEntityKey, "toEntityKey");
					this.FromEnd = EntityUtil.CheckArgumentNull<AssociationEndMember>(fromEnd, "fromEnd");
					this.ToEnd = EntityUtil.CheckArgumentNull<AssociationEndMember>(toEnd, "toEnd");
					this.AssociationSet = EntityUtil.CheckArgumentNull<AssociationSet>(associationSet, "associationSet");
					this.StateEntry = EntityUtil.CheckArgumentNull<IEntityStateEntry>(stateEntry, "stateEntry");
					this._equivalenceSetLinkedListNext = this;
					this._hashCode = (toEntityKey.GetHashCode() ^ fromEnd.GetHashCode() ^ toEnd.GetHashCode() ^ associationSet.GetHashCode());
				}

				// Token: 0x060048BE RID: 18622 RVA: 0x00105CD0 File Offset: 0x00103ED0
				internal void AddToEquivalenceSet(UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship other)
				{
					UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship equivalenceSetLinkedListNext = this._equivalenceSetLinkedListNext;
					this._equivalenceSetLinkedListNext = other;
					other._equivalenceSetLinkedListNext = equivalenceSetLinkedListNext;
				}

				// Token: 0x060048BF RID: 18623 RVA: 0x00105CF2 File Offset: 0x00103EF2
				internal IEnumerable<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship> GetEquivalenceSet()
				{
					UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship current = this;
					do
					{
						yield return current;
						current = current._equivalenceSetLinkedListNext;
					}
					while (current != this);
					yield break;
				}

				// Token: 0x060048C0 RID: 18624 RVA: 0x00105D04 File Offset: 0x00103F04
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
					while (directionalRelationship != this);
				}

				// Token: 0x060048C1 RID: 18625 RVA: 0x00105D4E File Offset: 0x00103F4E
				public override int GetHashCode()
				{
					return this._hashCode;
				}

				// Token: 0x060048C2 RID: 18626 RVA: 0x00105D58 File Offset: 0x00103F58
				public bool Equals(UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship other)
				{
					return this == other || (other != null && !(this.ToEntityKey != other.ToEntityKey) && this.AssociationSet == other.AssociationSet && this.ToEnd == other.ToEnd && this.FromEnd == other.FromEnd);
				}

				// Token: 0x060048C3 RID: 18627 RVA: 0x00105DB6 File Offset: 0x00103FB6
				public override bool Equals(object obj)
				{
					return this.Equals(obj as UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship);
				}

				// Token: 0x060048C4 RID: 18628 RVA: 0x00105DC4 File Offset: 0x00103FC4
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

				// Token: 0x040021A6 RID: 8614
				internal readonly EntityKey ToEntityKey;

				// Token: 0x040021A7 RID: 8615
				internal readonly AssociationEndMember FromEnd;

				// Token: 0x040021A8 RID: 8616
				internal readonly AssociationEndMember ToEnd;

				// Token: 0x040021A9 RID: 8617
				internal readonly IEntityStateEntry StateEntry;

				// Token: 0x040021AA RID: 8618
				internal readonly AssociationSet AssociationSet;

				// Token: 0x040021AB RID: 8619
				private UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship _equivalenceSetLinkedListNext;

				// Token: 0x040021AC RID: 8620
				private readonly int _hashCode;
			}
		}
	}
}
