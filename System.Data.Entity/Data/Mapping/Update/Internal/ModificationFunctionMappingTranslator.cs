using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002C3 RID: 707
	internal abstract class ModificationFunctionMappingTranslator
	{
		// Token: 0x060029D5 RID: 10709
		internal abstract FunctionUpdateCommand Translate(UpdateTranslator translator, ExtractedStateEntry stateEntry);

		// Token: 0x060029D6 RID: 10710 RVA: 0x000A37B1 File Offset: 0x000A19B1
		internal static ModificationFunctionMappingTranslator CreateEntitySetTranslator(StorageEntitySetMapping setMapping)
		{
			return new ModificationFunctionMappingTranslator.EntitySetTranslator(setMapping);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000A37B9 File Offset: 0x000A19B9
		internal static ModificationFunctionMappingTranslator CreateAssociationSetTranslator(StorageAssociationSetMapping setMapping)
		{
			return new ModificationFunctionMappingTranslator.AssociationSetTranslator(setMapping);
		}

		// Token: 0x02000616 RID: 1558
		private sealed class EntitySetTranslator : ModificationFunctionMappingTranslator
		{
			// Token: 0x060042A3 RID: 17059 RVA: 0x000F1CEC File Offset: 0x000EFEEC
			internal EntitySetTranslator(StorageEntitySetMapping setMapping)
			{
				this.m_typeMappings = new Dictionary<EntityType, StorageEntityTypeModificationFunctionMapping>();
				foreach (StorageEntityTypeModificationFunctionMapping storageEntityTypeModificationFunctionMapping in setMapping.ModificationFunctionMappings)
				{
					this.m_typeMappings.Add(storageEntityTypeModificationFunctionMapping.EntityType, storageEntityTypeModificationFunctionMapping);
				}
			}

			// Token: 0x060042A4 RID: 17060 RVA: 0x000F1D58 File Offset: 0x000EFF58
			internal override FunctionUpdateCommand Translate(UpdateTranslator translator, ExtractedStateEntry stateEntry)
			{
				Tuple<StorageEntityTypeModificationFunctionMapping, StorageModificationFunctionMapping> functionMapping = this.GetFunctionMapping(stateEntry);
				StorageEntityTypeModificationFunctionMapping item = functionMapping.Item1;
				StorageModificationFunctionMapping item2 = functionMapping.Item2;
				EntityKey entityKey = stateEntry.Source.EntityKey;
				HashSet<IEntityStateEntry> hashSet = new HashSet<IEntityStateEntry>
				{
					stateEntry.Source
				};
				IEnumerable<Tuple<AssociationEndMember, IEntityStateEntry>> enumerable = from end in item2.CollocatedAssociationSetEnds
				join candidateEntry in translator.GetRelationships(entityKey) on end.CorrespondingAssociationEndMember.DeclaringType equals candidateEntry.EntitySet.ElementType
				select Tuple.Create<AssociationEndMember, IEntityStateEntry>(end.CorrespondingAssociationEndMember, candidateEntry);
				Dictionary<AssociationEndMember, IEntityStateEntry> dictionary = new Dictionary<AssociationEndMember, IEntityStateEntry>();
				Dictionary<AssociationEndMember, IEntityStateEntry> dictionary2 = new Dictionary<AssociationEndMember, IEntityStateEntry>();
				foreach (Tuple<AssociationEndMember, IEntityStateEntry> tuple in enumerable)
				{
					ModificationFunctionMappingTranslator.EntitySetTranslator.ProcessReferenceCandidate(entityKey, hashSet, dictionary, dictionary2, tuple.Item1, tuple.Item2);
				}
				FunctionUpdateCommand functionUpdateCommand;
				if (hashSet.All((IEntityStateEntry e) => e.State == EntityState.Unchanged))
				{
					functionUpdateCommand = null;
				}
				else
				{
					functionUpdateCommand = new FunctionUpdateCommand(item2, translator, hashSet.ToList<IEntityStateEntry>().AsReadOnly(), stateEntry);
					this.BindFunctionParameters(translator, stateEntry, item2, functionUpdateCommand, dictionary, dictionary2);
					if (item2.ResultBindings != null)
					{
						foreach (StorageModificationFunctionResultBinding storageModificationFunctionResultBinding in item2.ResultBindings)
						{
							PropagatorResult memberValue = stateEntry.Current.GetMemberValue(storageModificationFunctionResultBinding.Property);
							functionUpdateCommand.AddResultColumn(translator, storageModificationFunctionResultBinding.ColumnName, memberValue);
						}
					}
				}
				return functionUpdateCommand;
			}

			// Token: 0x060042A5 RID: 17061 RVA: 0x000F1F30 File Offset: 0x000F0130
			private static void ProcessReferenceCandidate(EntityKey source, HashSet<IEntityStateEntry> stateEntries, Dictionary<AssociationEndMember, IEntityStateEntry> currentReferenceEnd, Dictionary<AssociationEndMember, IEntityStateEntry> originalReferenceEnd, AssociationEndMember endMember, IEntityStateEntry candidateEntry)
			{
				Func<DbDataRecord, int, EntityKey> getEntityKey = (DbDataRecord record, int ordinal) => (EntityKey)record[ordinal];
				Action<DbDataRecord, Action<IEntityStateEntry>> action = delegate(DbDataRecord record, Action<IEntityStateEntry> registerTarget)
				{
					int ordinal = record.GetOrdinal(endMember.Name);
					int arg = (ordinal == 0) ? 1 : 0;
					if (getEntityKey(record, arg) == source)
					{
						stateEntries.Add(candidateEntry);
						registerTarget(candidateEntry);
					}
				};
				EntityState state = candidateEntry.State;
				if (state == EntityState.Unchanged)
				{
					action(candidateEntry.CurrentValues, delegate(IEntityStateEntry target)
					{
						currentReferenceEnd.Add(endMember, target);
						originalReferenceEnd.Add(endMember, target);
					});
					return;
				}
				if (state == EntityState.Added)
				{
					action(candidateEntry.CurrentValues, delegate(IEntityStateEntry target)
					{
						currentReferenceEnd.Add(endMember, target);
					});
					return;
				}
				if (state != EntityState.Deleted)
				{
					return;
				}
				action(candidateEntry.OriginalValues, delegate(IEntityStateEntry target)
				{
					originalReferenceEnd.Add(endMember, target);
				});
			}

			// Token: 0x060042A6 RID: 17062 RVA: 0x000F2014 File Offset: 0x000F0214
			private Tuple<StorageEntityTypeModificationFunctionMapping, StorageModificationFunctionMapping> GetFunctionMapping(ExtractedStateEntry stateEntry)
			{
				EntityType entityType;
				if (stateEntry.Current != null)
				{
					entityType = (EntityType)stateEntry.Current.StructuralType;
				}
				else
				{
					entityType = (EntityType)stateEntry.Original.StructuralType;
				}
				StorageEntityTypeModificationFunctionMapping storageEntityTypeModificationFunctionMapping = this.m_typeMappings[entityType];
				EntityState state = stateEntry.State;
				StorageModificationFunctionMapping storageModificationFunctionMapping;
				if (state <= EntityState.Added)
				{
					if (state != EntityState.Unchanged)
					{
						if (state != EntityState.Added)
						{
							goto IL_C8;
						}
						storageModificationFunctionMapping = storageEntityTypeModificationFunctionMapping.InsertFunctionMapping;
						EntityUtil.ValidateNecessaryModificationFunctionMapping(storageModificationFunctionMapping, "Insert", stateEntry.Source, "EntityType", entityType.Name);
						goto IL_CA;
					}
				}
				else
				{
					if (state == EntityState.Deleted)
					{
						storageModificationFunctionMapping = storageEntityTypeModificationFunctionMapping.DeleteFunctionMapping;
						EntityUtil.ValidateNecessaryModificationFunctionMapping(storageModificationFunctionMapping, "Delete", stateEntry.Source, "EntityType", entityType.Name);
						goto IL_CA;
					}
					if (state != EntityState.Modified)
					{
						goto IL_C8;
					}
				}
				storageModificationFunctionMapping = storageEntityTypeModificationFunctionMapping.UpdateFunctionMapping;
				EntityUtil.ValidateNecessaryModificationFunctionMapping(storageModificationFunctionMapping, "Update", stateEntry.Source, "EntityType", entityType.Name);
				goto IL_CA;
				IL_C8:
				storageModificationFunctionMapping = null;
				IL_CA:
				return Tuple.Create<StorageEntityTypeModificationFunctionMapping, StorageModificationFunctionMapping>(storageEntityTypeModificationFunctionMapping, storageModificationFunctionMapping);
			}

			// Token: 0x060042A7 RID: 17063 RVA: 0x000F20F4 File Offset: 0x000F02F4
			private void BindFunctionParameters(UpdateTranslator translator, ExtractedStateEntry stateEntry, StorageModificationFunctionMapping functionMapping, FunctionUpdateCommand command, Dictionary<AssociationEndMember, IEntityStateEntry> currentReferenceEnds, Dictionary<AssociationEndMember, IEntityStateEntry> originalReferenceEnds)
			{
				foreach (StorageModificationFunctionParameterBinding storageModificationFunctionParameterBinding in functionMapping.ParameterBindings)
				{
					PropagatorResult propagatorResult;
					if (storageModificationFunctionParameterBinding.MemberPath.AssociationSetEnd != null)
					{
						AssociationEndMember correspondingAssociationEndMember = storageModificationFunctionParameterBinding.MemberPath.AssociationSetEnd.CorrespondingAssociationEndMember;
						IEntityStateEntry stateEntry2;
						if (!(storageModificationFunctionParameterBinding.IsCurrent ? currentReferenceEnds.TryGetValue(correspondingAssociationEndMember, out stateEntry2) : originalReferenceEnds.TryGetValue(correspondingAssociationEndMember, out stateEntry2)))
						{
							if (correspondingAssociationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One)
							{
								string name = stateEntry.Source.EntitySet.Name;
								string name2 = storageModificationFunctionParameterBinding.MemberPath.AssociationSetEnd.ParentAssociationSet.Name;
								throw EntityUtil.Update(Strings.Update_MissingRequiredRelationshipValue(name, name2), null, command.GetStateEntries(translator));
							}
							propagatorResult = PropagatorResult.CreateSimpleValue(PropagatorFlags.NoFlags, null);
						}
						else
						{
							PropagatorResult propagatorResult2 = storageModificationFunctionParameterBinding.IsCurrent ? translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry2, ModifiedPropertiesBehavior.AllModified) : translator.RecordConverter.ConvertOriginalValuesToPropagatorResult(stateEntry2, ModifiedPropertiesBehavior.AllModified);
							PropagatorResult memberValue = propagatorResult2.GetMemberValue(correspondingAssociationEndMember);
							EdmProperty member = (EdmProperty)storageModificationFunctionParameterBinding.MemberPath.Members[0];
							propagatorResult = memberValue.GetMemberValue(member);
						}
					}
					else
					{
						propagatorResult = (storageModificationFunctionParameterBinding.IsCurrent ? stateEntry.Current : stateEntry.Original);
						int i = storageModificationFunctionParameterBinding.MemberPath.Members.Count;
						while (i > 0)
						{
							i--;
							EdmMember member2 = storageModificationFunctionParameterBinding.MemberPath.Members[i];
							propagatorResult = propagatorResult.GetMemberValue(member2);
						}
					}
					command.SetParameterValue(propagatorResult, storageModificationFunctionParameterBinding, translator);
				}
				command.RegisterRowsAffectedParameter(functionMapping.RowsAffectedParameter);
			}

			// Token: 0x04001E3D RID: 7741
			private readonly Dictionary<EntityType, StorageEntityTypeModificationFunctionMapping> m_typeMappings;
		}

		// Token: 0x02000617 RID: 1559
		private sealed class AssociationSetTranslator : ModificationFunctionMappingTranslator
		{
			// Token: 0x060042A8 RID: 17064 RVA: 0x000F22A8 File Offset: 0x000F04A8
			internal AssociationSetTranslator(StorageAssociationSetMapping setMapping)
			{
				if (setMapping != null)
				{
					this.m_mapping = setMapping.ModificationFunctionMapping;
				}
			}

			// Token: 0x060042A9 RID: 17065 RVA: 0x000F22C0 File Offset: 0x000F04C0
			internal override FunctionUpdateCommand Translate(UpdateTranslator translator, ExtractedStateEntry stateEntry)
			{
				if (this.m_mapping == null)
				{
					return null;
				}
				bool flag = EntityState.Added == stateEntry.State;
				EntityUtil.ValidateNecessaryModificationFunctionMapping(flag ? this.m_mapping.InsertFunctionMapping : this.m_mapping.DeleteFunctionMapping, flag ? "Insert" : "Delete", stateEntry.Source, "AssociationSet", this.m_mapping.AssociationSet.Name);
				StorageModificationFunctionMapping storageModificationFunctionMapping = flag ? this.m_mapping.InsertFunctionMapping : this.m_mapping.DeleteFunctionMapping;
				FunctionUpdateCommand functionUpdateCommand = new FunctionUpdateCommand(storageModificationFunctionMapping, translator, new IEntityStateEntry[]
				{
					stateEntry.Source
				}.ToList<IEntityStateEntry>().AsReadOnly(), stateEntry);
				PropagatorResult propagatorResult;
				if (flag)
				{
					propagatorResult = stateEntry.Current;
				}
				else
				{
					propagatorResult = stateEntry.Original;
				}
				foreach (StorageModificationFunctionParameterBinding storageModificationFunctionParameterBinding in storageModificationFunctionMapping.ParameterBindings)
				{
					EdmProperty member = (EdmProperty)storageModificationFunctionParameterBinding.MemberPath.Members[0];
					AssociationEndMember member2 = (AssociationEndMember)storageModificationFunctionParameterBinding.MemberPath.Members[1];
					PropagatorResult memberValue = propagatorResult.GetMemberValue(member2);
					PropagatorResult memberValue2 = memberValue.GetMemberValue(member);
					functionUpdateCommand.SetParameterValue(memberValue2, storageModificationFunctionParameterBinding, translator);
				}
				functionUpdateCommand.RegisterRowsAffectedParameter(storageModificationFunctionMapping.RowsAffectedParameter);
				return functionUpdateCommand;
			}

			// Token: 0x04001E3E RID: 7742
			private readonly StorageAssociationSetModificationFunctionMapping m_mapping;
		}
	}
}
