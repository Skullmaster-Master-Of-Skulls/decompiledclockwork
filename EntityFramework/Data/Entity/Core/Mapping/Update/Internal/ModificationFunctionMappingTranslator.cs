using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003F2 RID: 1010
	internal abstract class ModificationFunctionMappingTranslator
	{
		// Token: 0x06002542 RID: 9538
		internal abstract FunctionUpdateCommand Translate(UpdateTranslator translator, ExtractedStateEntry stateEntry);

		// Token: 0x06002543 RID: 9539 RVA: 0x000B0911 File Offset: 0x000AEB11
		internal static ModificationFunctionMappingTranslator CreateEntitySetTranslator(EntitySetMapping setMapping)
		{
			return new ModificationFunctionMappingTranslator.EntitySetTranslator(setMapping);
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000B0919 File Offset: 0x000AEB19
		internal static ModificationFunctionMappingTranslator CreateAssociationSetTranslator(AssociationSetMapping setMapping)
		{
			return new ModificationFunctionMappingTranslator.AssociationSetTranslator(setMapping);
		}

		// Token: 0x020003F3 RID: 1011
		private sealed class EntitySetTranslator : ModificationFunctionMappingTranslator
		{
			// Token: 0x06002546 RID: 9542 RVA: 0x000B092C File Offset: 0x000AEB2C
			internal EntitySetTranslator(EntitySetMapping setMapping)
			{
				this.m_typeMappings = new Dictionary<EntityType, EntityTypeModificationFunctionMapping>();
				foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in setMapping.ModificationFunctionMappings)
				{
					this.m_typeMappings.Add(entityTypeModificationFunctionMapping.EntityType, entityTypeModificationFunctionMapping);
				}
			}

			// Token: 0x06002547 RID: 9543 RVA: 0x000B09CC File Offset: 0x000AEBCC
			internal override FunctionUpdateCommand Translate(UpdateTranslator translator, ExtractedStateEntry stateEntry)
			{
				Tuple<EntityTypeModificationFunctionMapping, ModificationFunctionMapping> functionMapping = this.GetFunctionMapping(stateEntry);
				ModificationFunctionMapping item = functionMapping.Item2;
				EntityKey entityKey = stateEntry.Source.EntityKey;
				HashSet<IEntityStateEntry> hashSet = new HashSet<IEntityStateEntry>
				{
					stateEntry.Source
				};
				IEnumerable<Tuple<AssociationEndMember, IEntityStateEntry>> enumerable = from end in item.CollocatedAssociationSetEnds
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
					functionUpdateCommand = new FunctionUpdateCommand(item, translator, new ReadOnlyCollection<IEntityStateEntry>(hashSet.ToList<IEntityStateEntry>()), stateEntry);
					ModificationFunctionMappingTranslator.EntitySetTranslator.BindFunctionParameters(translator, stateEntry, item, functionUpdateCommand, dictionary, dictionary2);
					if (item.ResultBindings != null)
					{
						foreach (ModificationFunctionResultBinding modificationFunctionResultBinding in item.ResultBindings)
						{
							PropagatorResult memberValue = stateEntry.Current.GetMemberValue(modificationFunctionResultBinding.Property);
							functionUpdateCommand.AddResultColumn(translator, modificationFunctionResultBinding.ColumnName, memberValue);
						}
					}
				}
				return functionUpdateCommand;
			}

			// Token: 0x06002548 RID: 9544 RVA: 0x000B0C60 File Offset: 0x000AEE60
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
				switch (state)
				{
				case EntityState.Unchanged:
					action(candidateEntry.CurrentValues, delegate(IEntityStateEntry target)
					{
						currentReferenceEnd.Add(endMember, target);
						originalReferenceEnd.Add(endMember, target);
					});
					return;
				case EntityState.Detached | EntityState.Unchanged:
					break;
				case EntityState.Added:
					action(candidateEntry.CurrentValues, delegate(IEntityStateEntry target)
					{
						currentReferenceEnd.Add(endMember, target);
					});
					return;
				default:
					if (state != EntityState.Deleted)
					{
						return;
					}
					action(candidateEntry.OriginalValues, delegate(IEntityStateEntry target)
					{
						originalReferenceEnd.Add(endMember, target);
					});
					break;
				}
			}

			// Token: 0x06002549 RID: 9545 RVA: 0x000B0D78 File Offset: 0x000AEF78
			private Tuple<EntityTypeModificationFunctionMapping, ModificationFunctionMapping> GetFunctionMapping(ExtractedStateEntry stateEntry)
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
				EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping = this.m_typeMappings[entityType];
				EntityState state = stateEntry.State;
				ModificationFunctionMapping modificationFunctionMapping;
				switch (state)
				{
				case EntityState.Unchanged:
					break;
				case EntityState.Detached | EntityState.Unchanged:
					goto IL_D5;
				case EntityState.Added:
					modificationFunctionMapping = entityTypeModificationFunctionMapping.InsertFunctionMapping;
					EntityUtil.ValidateNecessaryModificationFunctionMapping(modificationFunctionMapping, "Insert", stateEntry.Source, "EntityType", entityType.Name);
					goto IL_D7;
				default:
					if (state == EntityState.Deleted)
					{
						modificationFunctionMapping = entityTypeModificationFunctionMapping.DeleteFunctionMapping;
						EntityUtil.ValidateNecessaryModificationFunctionMapping(modificationFunctionMapping, "Delete", stateEntry.Source, "EntityType", entityType.Name);
						goto IL_D7;
					}
					if (state != EntityState.Modified)
					{
						goto IL_D5;
					}
					break;
				}
				modificationFunctionMapping = entityTypeModificationFunctionMapping.UpdateFunctionMapping;
				EntityUtil.ValidateNecessaryModificationFunctionMapping(modificationFunctionMapping, "Update", stateEntry.Source, "EntityType", entityType.Name);
				goto IL_D7;
				IL_D5:
				modificationFunctionMapping = null;
				IL_D7:
				return Tuple.Create<EntityTypeModificationFunctionMapping, ModificationFunctionMapping>(entityTypeModificationFunctionMapping, modificationFunctionMapping);
			}

			// Token: 0x0600254A RID: 9546 RVA: 0x000B0E64 File Offset: 0x000AF064
			private static void BindFunctionParameters(UpdateTranslator translator, ExtractedStateEntry stateEntry, ModificationFunctionMapping functionMapping, FunctionUpdateCommand command, Dictionary<AssociationEndMember, IEntityStateEntry> currentReferenceEnds, Dictionary<AssociationEndMember, IEntityStateEntry> originalReferenceEnds)
			{
				foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in functionMapping.ParameterBindings)
				{
					PropagatorResult propagatorResult;
					if (modificationFunctionParameterBinding.MemberPath.AssociationSetEnd != null)
					{
						AssociationEndMember correspondingAssociationEndMember = modificationFunctionParameterBinding.MemberPath.AssociationSetEnd.CorrespondingAssociationEndMember;
						IEntityStateEntry stateEntry2;
						if (!(modificationFunctionParameterBinding.IsCurrent ? currentReferenceEnds.TryGetValue(correspondingAssociationEndMember, out stateEntry2) : originalReferenceEnds.TryGetValue(correspondingAssociationEndMember, out stateEntry2)))
						{
							if (correspondingAssociationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One)
							{
								string name = stateEntry.Source.EntitySet.Name;
								string name2 = modificationFunctionParameterBinding.MemberPath.AssociationSetEnd.ParentAssociationSet.Name;
								throw new UpdateException(Strings.Update_MissingRequiredRelationshipValue(name, name2), null, command.GetStateEntries(translator).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
							}
							propagatorResult = PropagatorResult.CreateSimpleValue(PropagatorFlags.NoFlags, null);
						}
						else
						{
							PropagatorResult propagatorResult2 = modificationFunctionParameterBinding.IsCurrent ? translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry2, ModifiedPropertiesBehavior.AllModified) : translator.RecordConverter.ConvertOriginalValuesToPropagatorResult(stateEntry2, ModifiedPropertiesBehavior.AllModified);
							PropagatorResult memberValue = propagatorResult2.GetMemberValue(correspondingAssociationEndMember);
							EdmProperty member = (EdmProperty)modificationFunctionParameterBinding.MemberPath.Members[0];
							propagatorResult = memberValue.GetMemberValue(member);
						}
					}
					else
					{
						propagatorResult = (modificationFunctionParameterBinding.IsCurrent ? stateEntry.Current : stateEntry.Original);
						int i = modificationFunctionParameterBinding.MemberPath.Members.Count;
						while (i > 0)
						{
							i--;
							EdmMember member2 = modificationFunctionParameterBinding.MemberPath.Members[i];
							propagatorResult = propagatorResult.GetMemberValue(member2);
						}
					}
					command.SetParameterValue(propagatorResult, modificationFunctionParameterBinding, translator);
				}
				command.RegisterRowsAffectedParameter(functionMapping.RowsAffectedParameter);
			}

			// Token: 0x04000DDA RID: 3546
			private readonly Dictionary<EntityType, EntityTypeModificationFunctionMapping> m_typeMappings;
		}

		// Token: 0x020003F4 RID: 1012
		private sealed class AssociationSetTranslator : ModificationFunctionMappingTranslator
		{
			// Token: 0x06002550 RID: 9552 RVA: 0x000B1028 File Offset: 0x000AF228
			internal AssociationSetTranslator(AssociationSetMapping setMapping)
			{
				if (setMapping != null)
				{
					this.m_mapping = setMapping.ModificationFunctionMapping;
				}
			}

			// Token: 0x06002551 RID: 9553 RVA: 0x000B1040 File Offset: 0x000AF240
			internal override FunctionUpdateCommand Translate(UpdateTranslator translator, ExtractedStateEntry stateEntry)
			{
				if (this.m_mapping == null)
				{
					return null;
				}
				bool flag = EntityState.Added == stateEntry.State;
				EntityUtil.ValidateNecessaryModificationFunctionMapping(flag ? this.m_mapping.InsertFunctionMapping : this.m_mapping.DeleteFunctionMapping, flag ? "Insert" : "Delete", stateEntry.Source, "AssociationSet", this.m_mapping.AssociationSet.Name);
				ModificationFunctionMapping modificationFunctionMapping = flag ? this.m_mapping.InsertFunctionMapping : this.m_mapping.DeleteFunctionMapping;
				FunctionUpdateCommand functionUpdateCommand = new FunctionUpdateCommand(modificationFunctionMapping, translator, new ReadOnlyCollection<IEntityStateEntry>(new IEntityStateEntry[]
				{
					stateEntry.Source
				}.ToList<IEntityStateEntry>()), stateEntry);
				PropagatorResult propagatorResult;
				if (flag)
				{
					propagatorResult = stateEntry.Current;
				}
				else
				{
					propagatorResult = stateEntry.Original;
				}
				foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in modificationFunctionMapping.ParameterBindings)
				{
					EdmProperty member = (EdmProperty)modificationFunctionParameterBinding.MemberPath.Members[0];
					AssociationEndMember member2 = (AssociationEndMember)modificationFunctionParameterBinding.MemberPath.Members[1];
					PropagatorResult memberValue = propagatorResult.GetMemberValue(member2);
					PropagatorResult memberValue2 = memberValue.GetMemberValue(member);
					functionUpdateCommand.SetParameterValue(memberValue2, modificationFunctionParameterBinding, translator);
				}
				functionUpdateCommand.RegisterRowsAffectedParameter(modificationFunctionMapping.RowsAffectedParameter);
				return functionUpdateCommand;
			}

			// Token: 0x04000DE0 RID: 3552
			private readonly AssociationSetModificationFunctionMapping m_mapping;
		}
	}
}
