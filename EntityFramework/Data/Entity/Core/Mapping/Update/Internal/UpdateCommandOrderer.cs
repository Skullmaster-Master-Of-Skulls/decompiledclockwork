using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x02000414 RID: 1044
	internal class UpdateCommandOrderer : Graph<UpdateCommand>
	{
		// Token: 0x06002658 RID: 9816 RVA: 0x000B64B0 File Offset: 0x000B46B0
		internal UpdateCommandOrderer(IEnumerable<UpdateCommand> commands, UpdateTranslator translator) : base(EqualityComparer<UpdateCommand>.Default)
		{
			this._translator = translator;
			this._keyComparer = new UpdateCommandOrderer.ForeignKeyValueComparer(this._translator.KeyComparer);
			HashSet<EntitySet> hashSet = new HashSet<EntitySet>();
			HashSet<EntityContainer> hashSet2 = new HashSet<EntityContainer>();
			foreach (UpdateCommand updateCommand in commands)
			{
				if (updateCommand.Table != null)
				{
					hashSet.Add(updateCommand.Table);
					hashSet2.Add(updateCommand.Table.EntityContainer);
				}
				base.AddVertex(updateCommand);
				if (updateCommand.Kind == UpdateCommandKind.Function)
				{
					this._hasFunctionCommands = true;
				}
			}
			UpdateCommandOrderer.InitializeForeignKeyMaps(hashSet2, hashSet, out this._sourceMap, out this._targetMap);
			this.AddServerGenDependencies();
			this.AddForeignKeyDependencies();
			if (this._hasFunctionCommands)
			{
				this.AddModelDependencies();
			}
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000B6590 File Offset: 0x000B4790
		private static void InitializeForeignKeyMaps(HashSet<EntityContainer> containers, HashSet<EntitySet> tables, out KeyToListMap<EntitySetBase, ReferentialConstraint> sourceMap, out KeyToListMap<EntitySetBase, ReferentialConstraint> targetMap)
		{
			sourceMap = new KeyToListMap<EntitySetBase, ReferentialConstraint>(EqualityComparer<EntitySetBase>.Default);
			targetMap = new KeyToListMap<EntitySetBase, ReferentialConstraint>(EqualityComparer<EntitySetBase>.Default);
			foreach (EntityContainer entityContainer in containers)
			{
				foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
				{
					AssociationSet associationSet = entitySetBase as AssociationSet;
					if (associationSet != null)
					{
						AssociationSetEnd associationSetEnd = null;
						AssociationSetEnd associationSetEnd2 = null;
						ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = associationSet.AssociationSetEnds;
						if (2 == associationSetEnds.Count)
						{
							AssociationType elementType = associationSet.ElementType;
							bool flag = false;
							ReferentialConstraint value = null;
							foreach (ReferentialConstraint referentialConstraint in elementType.ReferentialConstraints)
							{
								if (!flag)
								{
									flag = true;
								}
								associationSetEnd = associationSet.AssociationSetEnds[referentialConstraint.ToRole.Name];
								associationSetEnd2 = associationSet.AssociationSetEnds[referentialConstraint.FromRole.Name];
								value = referentialConstraint;
							}
							if (associationSetEnd2 != null && associationSetEnd != null && tables.Contains(associationSetEnd2.EntitySet) && tables.Contains(associationSetEnd.EntitySet))
							{
								sourceMap.Add(associationSetEnd.EntitySet, value);
								targetMap.Add(associationSetEnd2.EntitySet, value);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000B6750 File Offset: 0x000B4950
		private void AddServerGenDependencies()
		{
			Dictionary<int, UpdateCommand> dictionary = new Dictionary<int, UpdateCommand>();
			foreach (UpdateCommand updateCommand in base.Vertices)
			{
				foreach (int key in updateCommand.OutputIdentifiers)
				{
					try
					{
						dictionary.Add(key, updateCommand);
					}
					catch (ArgumentException innerException)
					{
						throw new UpdateException(Strings.Update_AmbiguousServerGenIdentifier, innerException, updateCommand.GetStateEntries(this._translator).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
					}
				}
			}
			foreach (UpdateCommand updateCommand2 in base.Vertices)
			{
				foreach (int key2 in updateCommand2.InputIdentifiers)
				{
					UpdateCommand from;
					if (dictionary.TryGetValue(key2, out from))
					{
						base.AddEdge(from, updateCommand2);
					}
				}
			}
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000B68A4 File Offset: 0x000B4AA4
		private void AddForeignKeyDependencies()
		{
			KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand> predecessors = this.DetermineForeignKeyPredecessors();
			this.AddForeignKeyEdges(predecessors);
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000B68C0 File Offset: 0x000B4AC0
		private void AddForeignKeyEdges(KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand> predecessors)
		{
			foreach (DynamicUpdateCommand dynamicUpdateCommand in base.Vertices.OfType<DynamicUpdateCommand>())
			{
				if (dynamicUpdateCommand.Operator == ModificationOperator.Update || ModificationOperator.Insert == dynamicUpdateCommand.Operator)
				{
					foreach (ReferentialConstraint metadata in this._sourceMap.EnumerateValues(dynamicUpdateCommand.Table))
					{
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue;
						UpdateCommandOrderer.ForeignKeyValue x;
						if (UpdateCommandOrderer.ForeignKeyValue.TryCreateSourceKey(metadata, dynamicUpdateCommand.CurrentValues, true, out foreignKeyValue) && (dynamicUpdateCommand.Operator != ModificationOperator.Update || !UpdateCommandOrderer.ForeignKeyValue.TryCreateSourceKey(metadata, dynamicUpdateCommand.OriginalValues, true, out x) || !this._keyComparer.Equals(x, foreignKeyValue)))
						{
							foreach (UpdateCommand updateCommand in predecessors.EnumerateValues(foreignKeyValue))
							{
								if (updateCommand != dynamicUpdateCommand)
								{
									base.AddEdge(updateCommand, dynamicUpdateCommand);
								}
							}
						}
					}
				}
				if (dynamicUpdateCommand.Operator == ModificationOperator.Update || ModificationOperator.Delete == dynamicUpdateCommand.Operator)
				{
					foreach (ReferentialConstraint metadata2 in this._targetMap.EnumerateValues(dynamicUpdateCommand.Table))
					{
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue2;
						UpdateCommandOrderer.ForeignKeyValue x2;
						if (UpdateCommandOrderer.ForeignKeyValue.TryCreateTargetKey(metadata2, dynamicUpdateCommand.OriginalValues, false, out foreignKeyValue2) && (dynamicUpdateCommand.Operator != ModificationOperator.Update || !UpdateCommandOrderer.ForeignKeyValue.TryCreateTargetKey(metadata2, dynamicUpdateCommand.CurrentValues, false, out x2) || !this._keyComparer.Equals(x2, foreignKeyValue2)))
						{
							foreach (UpdateCommand updateCommand2 in predecessors.EnumerateValues(foreignKeyValue2))
							{
								if (updateCommand2 != dynamicUpdateCommand)
								{
									base.AddEdge(updateCommand2, dynamicUpdateCommand);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000B6B1C File Offset: 0x000B4D1C
		private KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand> DetermineForeignKeyPredecessors()
		{
			KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand> keyToListMap = new KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand>(this._keyComparer);
			foreach (DynamicUpdateCommand dynamicUpdateCommand in base.Vertices.OfType<DynamicUpdateCommand>())
			{
				if (dynamicUpdateCommand.Operator == ModificationOperator.Update || ModificationOperator.Insert == dynamicUpdateCommand.Operator)
				{
					foreach (ReferentialConstraint metadata in this._targetMap.EnumerateValues(dynamicUpdateCommand.Table))
					{
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue;
						UpdateCommandOrderer.ForeignKeyValue x;
						if (UpdateCommandOrderer.ForeignKeyValue.TryCreateTargetKey(metadata, dynamicUpdateCommand.CurrentValues, true, out foreignKeyValue) && (dynamicUpdateCommand.Operator != ModificationOperator.Update || !UpdateCommandOrderer.ForeignKeyValue.TryCreateTargetKey(metadata, dynamicUpdateCommand.OriginalValues, true, out x) || !this._keyComparer.Equals(x, foreignKeyValue)))
						{
							keyToListMap.Add(foreignKeyValue, dynamicUpdateCommand);
						}
					}
				}
				if (dynamicUpdateCommand.Operator == ModificationOperator.Update || ModificationOperator.Delete == dynamicUpdateCommand.Operator)
				{
					foreach (ReferentialConstraint metadata2 in this._sourceMap.EnumerateValues(dynamicUpdateCommand.Table))
					{
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue2;
						UpdateCommandOrderer.ForeignKeyValue x2;
						if (UpdateCommandOrderer.ForeignKeyValue.TryCreateSourceKey(metadata2, dynamicUpdateCommand.OriginalValues, false, out foreignKeyValue2) && (dynamicUpdateCommand.Operator != ModificationOperator.Update || !UpdateCommandOrderer.ForeignKeyValue.TryCreateSourceKey(metadata2, dynamicUpdateCommand.CurrentValues, false, out x2) || !this._keyComparer.Equals(x2, foreignKeyValue2)))
						{
							keyToListMap.Add(foreignKeyValue2, dynamicUpdateCommand);
						}
					}
				}
			}
			return keyToListMap;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000B6CDC File Offset: 0x000B4EDC
		private void AddModelDependencies()
		{
			KeyToListMap<EntityKey, UpdateCommand> keyToListMap = new KeyToListMap<EntityKey, UpdateCommand>(EqualityComparer<EntityKey>.Default);
			KeyToListMap<EntityKey, UpdateCommand> keyToListMap2 = new KeyToListMap<EntityKey, UpdateCommand>(EqualityComparer<EntityKey>.Default);
			KeyToListMap<EntityKey, UpdateCommand> keyToListMap3 = new KeyToListMap<EntityKey, UpdateCommand>(EqualityComparer<EntityKey>.Default);
			KeyToListMap<EntityKey, UpdateCommand> keyToListMap4 = new KeyToListMap<EntityKey, UpdateCommand>(EqualityComparer<EntityKey>.Default);
			foreach (UpdateCommand updateCommand in base.Vertices)
			{
				updateCommand.GetRequiredAndProducedEntities(this._translator, keyToListMap, keyToListMap2, keyToListMap3, keyToListMap4);
			}
			this.AddModelDependencies(keyToListMap, keyToListMap3);
			this.AddModelDependencies(keyToListMap4, keyToListMap2);
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000B6D78 File Offset: 0x000B4F78
		private void AddModelDependencies(KeyToListMap<EntityKey, UpdateCommand> producedMap, KeyToListMap<EntityKey, UpdateCommand> requiredMap)
		{
			foreach (KeyValuePair<EntityKey, List<UpdateCommand>> keyValuePair in requiredMap.KeyValuePairs)
			{
				EntityKey key = keyValuePair.Key;
				List<UpdateCommand> value = keyValuePair.Value;
				foreach (UpdateCommand updateCommand in producedMap.EnumerateValues(key))
				{
					foreach (UpdateCommand updateCommand2 in value)
					{
						if (!object.ReferenceEquals(updateCommand, updateCommand2) && (updateCommand.Kind == UpdateCommandKind.Function || updateCommand2.Kind == UpdateCommandKind.Function))
						{
							base.AddEdge(updateCommand, updateCommand2);
						}
					}
				}
			}
		}

		// Token: 0x04000E5B RID: 3675
		private readonly UpdateCommandOrderer.ForeignKeyValueComparer _keyComparer;

		// Token: 0x04000E5C RID: 3676
		private readonly KeyToListMap<EntitySetBase, ReferentialConstraint> _sourceMap;

		// Token: 0x04000E5D RID: 3677
		private readonly KeyToListMap<EntitySetBase, ReferentialConstraint> _targetMap;

		// Token: 0x04000E5E RID: 3678
		private readonly bool _hasFunctionCommands;

		// Token: 0x04000E5F RID: 3679
		private readonly UpdateTranslator _translator;

		// Token: 0x02000415 RID: 1045
		private struct ForeignKeyValue
		{
			// Token: 0x06002660 RID: 9824 RVA: 0x000B6E74 File Offset: 0x000B5074
			private ForeignKeyValue(ReferentialConstraint metadata, PropagatorResult record, bool isTarget, bool isInsert)
			{
				this.Metadata = metadata;
				IList<EdmProperty> list = isTarget ? metadata.FromProperties : metadata.ToProperties;
				PropagatorResult[] array = new PropagatorResult[list.Count];
				bool flag = false;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = record.GetMemberValue(list[i]);
					if (array[i].IsNull)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					this.Key = null;
				}
				else
				{
					this.Key = new CompositeKey(array);
				}
				this.IsInsert = isInsert;
			}

			// Token: 0x06002661 RID: 9825 RVA: 0x000B6EF4 File Offset: 0x000B50F4
			internal static bool TryCreateTargetKey(ReferentialConstraint metadata, PropagatorResult record, bool isInsert, out UpdateCommandOrderer.ForeignKeyValue key)
			{
				key = new UpdateCommandOrderer.ForeignKeyValue(metadata, record, true, isInsert);
				return key.Key != null;
			}

			// Token: 0x06002662 RID: 9826 RVA: 0x000B6F10 File Offset: 0x000B5110
			internal static bool TryCreateSourceKey(ReferentialConstraint metadata, PropagatorResult record, bool isInsert, out UpdateCommandOrderer.ForeignKeyValue key)
			{
				key = new UpdateCommandOrderer.ForeignKeyValue(metadata, record, false, isInsert);
				return key.Key != null;
			}

			// Token: 0x04000E60 RID: 3680
			internal readonly ReferentialConstraint Metadata;

			// Token: 0x04000E61 RID: 3681
			internal readonly CompositeKey Key;

			// Token: 0x04000E62 RID: 3682
			internal readonly bool IsInsert;
		}

		// Token: 0x02000416 RID: 1046
		private class ForeignKeyValueComparer : IEqualityComparer<UpdateCommandOrderer.ForeignKeyValue>
		{
			// Token: 0x06002663 RID: 9827 RVA: 0x000B6F2C File Offset: 0x000B512C
			internal ForeignKeyValueComparer(IEqualityComparer<CompositeKey> baseComparer)
			{
				this._baseComparer = baseComparer;
			}

			// Token: 0x06002664 RID: 9828 RVA: 0x000B6F3B File Offset: 0x000B513B
			public bool Equals(UpdateCommandOrderer.ForeignKeyValue x, UpdateCommandOrderer.ForeignKeyValue y)
			{
				return x.IsInsert == y.IsInsert && x.Metadata == y.Metadata && this._baseComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x06002665 RID: 9829 RVA: 0x000B6F78 File Offset: 0x000B5178
			public int GetHashCode(UpdateCommandOrderer.ForeignKeyValue obj)
			{
				return this._baseComparer.GetHashCode(obj.Key);
			}

			// Token: 0x04000E63 RID: 3683
			private readonly IEqualityComparer<CompositeKey> _baseComparer;
		}
	}
}
