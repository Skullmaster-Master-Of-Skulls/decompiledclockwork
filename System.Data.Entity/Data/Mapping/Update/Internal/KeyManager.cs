using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002BF RID: 703
	internal class KeyManager
	{
		// Token: 0x060029B5 RID: 10677 RVA: 0x000A2320 File Offset: 0x000A0520
		internal KeyManager(UpdateTranslator translator)
		{
			this._translator = EntityUtil.CheckArgumentNull<UpdateTranslator>(translator, "translator");
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000A237C File Offset: 0x000A057C
		internal int GetCliqueIdentifier(int identifier)
		{
			KeyManager.Partition partition = this._identifiers[identifier].Partition;
			if (partition != null)
			{
				return partition.PartitionId;
			}
			return identifier;
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000A23A8 File Offset: 0x000A05A8
		internal void AddReferentialConstraint(IEntityStateEntry dependentStateEntry, int dependentIdentifier, int principalIdentifier)
		{
			KeyManager.IdentifierInfo identifierInfo = this._identifiers[dependentIdentifier];
			if (dependentIdentifier != principalIdentifier)
			{
				this.AssociateNodes(dependentIdentifier, principalIdentifier);
				KeyManager.LinkedList<int>.Add(ref identifierInfo.References, principalIdentifier);
				KeyManager.IdentifierInfo identifierInfo2 = this._identifiers[principalIdentifier];
				KeyManager.LinkedList<int>.Add(ref identifierInfo2.ReferencedBy, dependentIdentifier);
			}
			KeyManager.LinkedList<IEntityStateEntry>.Add(ref identifierInfo.DependentStateEntries, dependentStateEntry);
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000A23FF File Offset: 0x000A05FF
		internal void RegisterIdentifierOwner(PropagatorResult owner)
		{
			this._identifiers[owner.Identifier].Owner = owner;
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000A2418 File Offset: 0x000A0618
		internal bool TryGetIdentifierOwner(int identifier, out PropagatorResult owner)
		{
			owner = this._identifiers[identifier].Owner;
			return owner != null;
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000A2434 File Offset: 0x000A0634
		internal int GetKeyIdentifierForMemberOffset(EntityKey entityKey, int memberOffset, int keyMemberCount)
		{
			int num;
			if (!this._keyIdentifiers.TryGetValue(entityKey, out num))
			{
				num = this._identifiers.Count;
				for (int i = 0; i < keyMemberCount; i++)
				{
					this._identifiers.Add(new KeyManager.IdentifierInfo());
				}
				this._keyIdentifiers.Add(entityKey, num);
			}
			num += memberOffset;
			return num;
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000A248C File Offset: 0x000A068C
		internal int GetKeyIdentifierForMember(EntityKey entityKey, string member, bool currentValues)
		{
			Tuple<EntityKey, string, bool> key = Tuple.Create<EntityKey, string, bool>(entityKey, member, currentValues);
			int count;
			if (!this._foreignKeyIdentifiers.TryGetValue(key, out count))
			{
				count = this._identifiers.Count;
				this._identifiers.Add(new KeyManager.IdentifierInfo());
				this._foreignKeyIdentifiers.Add(key, count);
			}
			return count;
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000A24DC File Offset: 0x000A06DC
		internal IEnumerable<IEntityStateEntry> GetDependentStateEntries(int identifier)
		{
			return KeyManager.LinkedList<IEntityStateEntry>.Enumerate(this._identifiers[identifier].DependentStateEntries);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000A24F4 File Offset: 0x000A06F4
		internal object GetPrincipalValue(PropagatorResult result)
		{
			int identifier = result.Identifier;
			if (-1 == identifier)
			{
				return result.GetSimpleValue();
			}
			bool flag = true;
			object obj = null;
			foreach (int index in this.GetPrincipals(identifier))
			{
				PropagatorResult owner = this._identifiers[index].Owner;
				if (owner != null)
				{
					if (flag)
					{
						obj = owner.GetSimpleValue();
						flag = false;
					}
					else if (!ByValueEqualityComparer.Default.Equals(obj, owner.GetSimpleValue()))
					{
						throw EntityUtil.Constraint(Strings.Update_ReferentialConstraintIntegrityViolation);
					}
				}
			}
			if (flag)
			{
				obj = result.GetSimpleValue();
			}
			return obj;
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000A25A4 File Offset: 0x000A07A4
		internal IEnumerable<int> GetPrincipals(int identifier)
		{
			return this.WalkGraph(identifier, (KeyManager.IdentifierInfo info) => info.References, true);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000A25CD File Offset: 0x000A07CD
		internal IEnumerable<int> GetDirectReferences(int identifier)
		{
			KeyManager.LinkedList<int> references = this._identifiers[identifier].References;
			foreach (int num in KeyManager.LinkedList<int>.Enumerate(references))
			{
				yield return num;
			}
			IEnumerator<int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000A25E4 File Offset: 0x000A07E4
		internal IEnumerable<int> GetDependents(int identifier)
		{
			return this.WalkGraph(identifier, (KeyManager.IdentifierInfo info) => info.ReferencedBy, false);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000A260D File Offset: 0x000A080D
		private IEnumerable<int> WalkGraph(int identifier, Func<KeyManager.IdentifierInfo, KeyManager.LinkedList<int>> successorFunction, bool leavesOnly)
		{
			Stack<int> stack = new Stack<int>();
			stack.Push(identifier);
			while (stack.Count > 0)
			{
				int num = stack.Pop();
				KeyManager.LinkedList<int> linkedList = successorFunction(this._identifiers[num]);
				if (linkedList != null)
				{
					foreach (int item in KeyManager.LinkedList<int>.Enumerate(linkedList))
					{
						stack.Push(item);
					}
					if (!leavesOnly)
					{
						yield return num;
					}
				}
				else
				{
					yield return num;
				}
			}
			yield break;
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000A2632 File Offset: 0x000A0832
		internal bool HasPrincipals(int identifier)
		{
			return this._identifiers[identifier].References != null;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000A2648 File Offset: 0x000A0848
		internal void ValidateReferentialIntegrityGraphAcyclic()
		{
			byte[] array = new byte[this._identifiers.Count];
			int i = 0;
			int count = this._identifiers.Count;
			while (i < count)
			{
				if (array[i] == 0)
				{
					this.ValidateReferentialIntegrityGraphAcyclic(i, array, null);
				}
				i++;
			}
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000A268C File Offset: 0x000A088C
		internal void RegisterKeyValueForAddedEntity(IEntityStateEntry addedEntry)
		{
			EntityKey entityKey = addedEntry.EntityKey;
			ReadOnlyMetadataCollection<EdmMember> keyMembers = addedEntry.EntitySet.ElementType.KeyMembers;
			CurrentValueRecord currentValues = addedEntry.CurrentValues;
			object[] array = new object[keyMembers.Count];
			bool flag = false;
			int i = 0;
			int count = keyMembers.Count;
			while (i < count)
			{
				int ordinal = currentValues.GetOrdinal(keyMembers[i].Name);
				if (currentValues.IsDBNull(ordinal))
				{
					flag = true;
					break;
				}
				array[i] = currentValues.GetValue(ordinal);
				i++;
			}
			if (flag)
			{
				return;
			}
			EntityKey key = (array.Length == 1) ? new EntityKey(addedEntry.EntitySet, array[0]) : new EntityKey(addedEntry.EntitySet, array);
			if (this._valueKeyToTempKey.ContainsKey(key))
			{
				this._valueKeyToTempKey[key] = null;
				return;
			}
			this._valueKeyToTempKey.Add(key, entityKey);
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000A2766 File Offset: 0x000A0966
		internal bool TryGetTempKey(EntityKey valueKey, out EntityKey tempKey)
		{
			return this._valueKeyToTempKey.TryGetValue(valueKey, out tempKey);
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000A2778 File Offset: 0x000A0978
		private void ValidateReferentialIntegrityGraphAcyclic(int node, byte[] color, KeyManager.LinkedList<int> parent)
		{
			color[node] = 2;
			KeyManager.LinkedList<int>.Add(ref parent, node);
			foreach (int num in KeyManager.LinkedList<int>.Enumerate(this._identifiers[node].References))
			{
				byte b = color[num];
				if (b != 0)
				{
					if (b == 2)
					{
						List<IEntityStateEntry> list = new List<IEntityStateEntry>();
						foreach (int num2 in KeyManager.LinkedList<int>.Enumerate(parent))
						{
							PropagatorResult owner = this._identifiers[num2].Owner;
							if (owner != null)
							{
								list.Add(owner.StateEntry);
							}
							if (num2 == num)
							{
								break;
							}
						}
						throw EntityUtil.Update(Strings.Update_CircularRelationships, null, list);
					}
				}
				else
				{
					this.ValidateReferentialIntegrityGraphAcyclic(num, color, parent);
				}
			}
			color[node] = 1;
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000A2878 File Offset: 0x000A0A78
		internal void AssociateNodes(int firstId, int secondId)
		{
			if (firstId == secondId)
			{
				return;
			}
			KeyManager.Partition partition = this._identifiers[firstId].Partition;
			if (partition != null)
			{
				KeyManager.Partition partition2 = this._identifiers[secondId].Partition;
				if (partition2 != null)
				{
					partition.Merge(this, partition2);
					return;
				}
				partition.AddNode(this, secondId);
				return;
			}
			else
			{
				KeyManager.Partition partition3 = this._identifiers[secondId].Partition;
				if (partition3 != null)
				{
					partition3.AddNode(this, firstId);
					return;
				}
				KeyManager.Partition.CreatePartition(this, firstId, secondId);
				return;
			}
		}

		// Token: 0x04001293 RID: 4755
		private readonly Dictionary<Tuple<EntityKey, string, bool>, int> _foreignKeyIdentifiers = new Dictionary<Tuple<EntityKey, string, bool>, int>();

		// Token: 0x04001294 RID: 4756
		private readonly Dictionary<EntityKey, EntityKey> _valueKeyToTempKey = new Dictionary<EntityKey, EntityKey>();

		// Token: 0x04001295 RID: 4757
		private readonly Dictionary<EntityKey, int> _keyIdentifiers = new Dictionary<EntityKey, int>();

		// Token: 0x04001296 RID: 4758
		private readonly List<KeyManager.IdentifierInfo> _identifiers = new List<KeyManager.IdentifierInfo>
		{
			new KeyManager.IdentifierInfo()
		};

		// Token: 0x04001297 RID: 4759
		private readonly UpdateTranslator _translator;

		// Token: 0x04001298 RID: 4760
		private const byte White = 0;

		// Token: 0x04001299 RID: 4761
		private const byte Black = 1;

		// Token: 0x0400129A RID: 4762
		private const byte Gray = 2;

		// Token: 0x0200060C RID: 1548
		private sealed class Partition
		{
			// Token: 0x0600427A RID: 17018 RVA: 0x000F16D3 File Offset: 0x000EF8D3
			private Partition(int partitionId)
			{
				this._nodeIds = new List<int>(2);
				this.PartitionId = partitionId;
			}

			// Token: 0x0600427B RID: 17019 RVA: 0x000F16F0 File Offset: 0x000EF8F0
			internal static void CreatePartition(KeyManager manager, int firstId, int secondId)
			{
				KeyManager.Partition partition = new KeyManager.Partition(firstId);
				partition.AddNode(manager, firstId);
				partition.AddNode(manager, secondId);
			}

			// Token: 0x0600427C RID: 17020 RVA: 0x000F1714 File Offset: 0x000EF914
			internal void AddNode(KeyManager manager, int nodeId)
			{
				this._nodeIds.Add(nodeId);
				manager._identifiers[nodeId].Partition = this;
			}

			// Token: 0x0600427D RID: 17021 RVA: 0x000F1734 File Offset: 0x000EF934
			internal void Merge(KeyManager manager, KeyManager.Partition other)
			{
				if (other.PartitionId == this.PartitionId)
				{
					return;
				}
				foreach (int nodeId in other._nodeIds)
				{
					this.AddNode(manager, nodeId);
				}
			}

			// Token: 0x04001E13 RID: 7699
			internal readonly int PartitionId;

			// Token: 0x04001E14 RID: 7700
			private readonly List<int> _nodeIds;
		}

		// Token: 0x0200060D RID: 1549
		private sealed class LinkedList<T>
		{
			// Token: 0x0600427E RID: 17022 RVA: 0x000F1798 File Offset: 0x000EF998
			private LinkedList(T value, KeyManager.LinkedList<T> previous)
			{
				this._value = value;
				this._previous = previous;
			}

			// Token: 0x0600427F RID: 17023 RVA: 0x000F17AE File Offset: 0x000EF9AE
			internal static IEnumerable<T> Enumerate(KeyManager.LinkedList<T> current)
			{
				while (current != null)
				{
					yield return current._value;
					current = current._previous;
				}
				yield break;
			}

			// Token: 0x06004280 RID: 17024 RVA: 0x000F17BE File Offset: 0x000EF9BE
			internal static void Add(ref KeyManager.LinkedList<T> list, T value)
			{
				list = new KeyManager.LinkedList<T>(value, list);
			}

			// Token: 0x04001E15 RID: 7701
			private readonly T _value;

			// Token: 0x04001E16 RID: 7702
			private readonly KeyManager.LinkedList<T> _previous;
		}

		// Token: 0x0200060E RID: 1550
		private sealed class IdentifierInfo
		{
			// Token: 0x04001E17 RID: 7703
			internal KeyManager.Partition Partition;

			// Token: 0x04001E18 RID: 7704
			internal PropagatorResult Owner;

			// Token: 0x04001E19 RID: 7705
			internal KeyManager.LinkedList<IEntityStateEntry> DependentStateEntries;

			// Token: 0x04001E1A RID: 7706
			internal KeyManager.LinkedList<int> References;

			// Token: 0x04001E1B RID: 7707
			internal KeyManager.LinkedList<int> ReferencedBy;
		}
	}
}
