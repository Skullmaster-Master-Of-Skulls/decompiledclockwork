using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002C0 RID: 704
	internal class UpdateCommandOrderer : Graph<UpdateCommand>
	{
		// Token: 0x060029C8 RID: 10696 RVA: 0x000A28EC File Offset: 0x000A0AEC
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

		// Token: 0x060029C9 RID: 10697 RVA: 0x000A29CC File Offset: 0x000A0BCC
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

		// Token: 0x060029CA RID: 10698 RVA: 0x000A2B94 File Offset: 0x000A0D94
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
						throw EntityUtil.Update(Strings.Update_AmbiguousServerGenIdentifier, innerException, updateCommand.GetStateEntries(this._translator));
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

		// Token: 0x060029CB RID: 10699 RVA: 0x000A2CD8 File Offset: 0x000A0ED8
		private void AddForeignKeyDependencies()
		{
			KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand> predecessors = this.DetermineForeignKeyPredecessors();
			this.AddForeignKeyEdges(predecessors);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000A2CF4 File Offset: 0x000A0EF4
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

		// Token: 0x060029CD RID: 10701 RVA: 0x000A2F4C File Offset: 0x000A114C
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

		// Token: 0x060029CE RID: 10702 RVA: 0x000A3108 File Offset: 0x000A1308
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

		// Token: 0x060029CF RID: 10703 RVA: 0x000A31A4 File Offset: 0x000A13A4
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
						if (updateCommand != updateCommand2 && (updateCommand.Kind == UpdateCommandKind.Function || updateCommand2.Kind == UpdateCommandKind.Function))
						{
							base.AddEdge(updateCommand, updateCommand2);
						}
					}
				}
			}
		}

		// Token: 0x0400129B RID: 4763
		private readonly UpdateCommandOrderer.ForeignKeyValueComparer _keyComparer;

		// Token: 0x0400129C RID: 4764
		private readonly KeyToListMap<EntitySetBase, ReferentialConstraint> _sourceMap;

		// Token: 0x0400129D RID: 4765
		private readonly KeyToListMap<EntitySetBase, ReferentialConstraint> _targetMap;

		// Token: 0x0400129E RID: 4766
		private readonly bool _hasFunctionCommands;

		// Token: 0x0400129F RID: 4767
		private readonly UpdateTranslator _translator;

		// Token: 0x02000612 RID: 1554
		private struct ForeignKeyValue
		{
			// Token: 0x06004297 RID: 17047 RVA: 0x000F1B3C File Offset: 0x000EFD3C
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

			// Token: 0x06004298 RID: 17048 RVA: 0x000F1BBC File Offset: 0x000EFDBC
			internal static bool TryCreateTargetKey(ReferentialConstraint metadata, PropagatorResult record, bool isInsert, out UpdateCommandOrderer.ForeignKeyValue key)
			{
				key = new UpdateCommandOrderer.ForeignKeyValue(metadata, record, true, isInsert);
				return key.Key != null;
			}

			// Token: 0x06004299 RID: 17049 RVA: 0x000F1BD8 File Offset: 0x000EFDD8
			internal static bool TryCreateSourceKey(ReferentialConstraint metadata, PropagatorResult record, bool isInsert, out UpdateCommandOrderer.ForeignKeyValue key)
			{
				key = new UpdateCommandOrderer.ForeignKeyValue(metadata, record, false, isInsert);
				return key.Key != null;
			}

			// Token: 0x04001E31 RID: 7729
			internal readonly ReferentialConstraint Metadata;

			// Token: 0x04001E32 RID: 7730
			internal readonly CompositeKey Key;

			// Token: 0x04001E33 RID: 7731
			internal readonly bool IsInsert;
		}

		// Token: 0x02000613 RID: 1555
		private class ForeignKeyValueComparer : IEqualityComparer<UpdateCommandOrderer.ForeignKeyValue>
		{
			// Token: 0x0600429A RID: 17050 RVA: 0x000F1BF4 File Offset: 0x000EFDF4
			internal ForeignKeyValueComparer(IEqualityComparer<CompositeKey> baseComparer)
			{
				this._baseComparer = EntityUtil.CheckArgumentNull<IEqualityComparer<CompositeKey>>(baseComparer, "baseComparer");
			}

			// Token: 0x0600429B RID: 17051 RVA: 0x000F1C0D File Offset: 0x000EFE0D
			public bool Equals(UpdateCommandOrderer.ForeignKeyValue x, UpdateCommandOrderer.ForeignKeyValue y)
			{
				return x.IsInsert == y.IsInsert && x.Metadata == y.Metadata && this._baseComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x0600429C RID: 17052 RVA: 0x000F1C44 File Offset: 0x000EFE44
			public int GetHashCode(UpdateCommandOrderer.ForeignKeyValue obj)
			{
				return this._baseComparer.GetHashCode(obj.Key);
			}

			// Token: 0x04001E34 RID: 7732
			private readonly IEqualityComparer<CompositeKey> _baseComparer;
		}
	}
}
