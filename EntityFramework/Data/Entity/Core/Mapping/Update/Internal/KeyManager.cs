using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003F7 RID: 1015
	internal class KeyManager
	{
		// Token: 0x0600256E RID: 9582 RVA: 0x000B2924 File Offset: 0x000B0B24
		internal int GetCliqueIdentifier(int identifier)
		{
			KeyManager.Partition partition = this._identifiers[identifier].Partition;
			if (partition != null)
			{
				return partition.PartitionId;
			}
			return identifier;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000B2950 File Offset: 0x000B0B50
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

		// Token: 0x06002570 RID: 9584 RVA: 0x000B29A7 File Offset: 0x000B0BA7
		internal void RegisterIdentifierOwner(PropagatorResult owner)
		{
			this._identifiers[owner.Identifier].Owner = owner;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000B29C0 File Offset: 0x000B0BC0
		internal bool TryGetIdentifierOwner(int identifier, out PropagatorResult owner)
		{
			owner = this._identifiers[identifier].Owner;
			return null != owner;
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000B29E0 File Offset: 0x000B0BE0
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

		// Token: 0x06002573 RID: 9587 RVA: 0x000B2A38 File Offset: 0x000B0C38
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

		// Token: 0x06002574 RID: 9588 RVA: 0x000B2A88 File Offset: 0x000B0C88
		internal IEnumerable<IEntityStateEntry> GetDependentStateEntries(int identifier)
		{
			return KeyManager.LinkedList<IEntityStateEntry>.Enumerate(this._identifiers[identifier].DependentStateEntries);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000B2AA0 File Offset: 0x000B0CA0
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
						throw new ConstraintException(Strings.Update_ReferentialConstraintIntegrityViolation);
					}
				}
			}
			if (flag)
			{
				obj = result.GetSimpleValue();
			}
			return obj;
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000B2B5C File Offset: 0x000B0D5C
		internal IEnumerable<int> GetPrincipals(int identifier)
		{
			return this.WalkGraph(identifier, (KeyManager.IdentifierInfo info) => info.References, true);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000B2D40 File Offset: 0x000B0F40
		internal IEnumerable<int> GetDirectReferences(int identifier)
		{
			KeyManager.LinkedList<int> references = this._identifiers[identifier].References;
			foreach (int i in KeyManager.LinkedList<int>.Enumerate(references))
			{
				yield return i;
			}
			yield break;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000B2D6C File Offset: 0x000B0F6C
		internal IEnumerable<int> GetDependents(int identifier)
		{
			return this.WalkGraph(identifier, (KeyManager.IdentifierInfo info) => info.ReferencedBy, false);
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000B2F80 File Offset: 0x000B1180
		private IEnumerable<int> WalkGraph(int identifier, Func<KeyManager.IdentifierInfo, KeyManager.LinkedList<int>> successorFunction, bool leavesOnly)
		{
			Stack<int> stack = new Stack<int>();
			stack.Push(identifier);
			while (stack.Count > 0)
			{
				int currentIdentifier = stack.Pop();
				KeyManager.LinkedList<int> successors = successorFunction(this._identifiers[currentIdentifier]);
				if (successors != null)
				{
					foreach (int item in KeyManager.LinkedList<int>.Enumerate(successors))
					{
						stack.Push(item);
					}
					if (!leavesOnly)
					{
						yield return currentIdentifier;
					}
				}
				else
				{
					yield return currentIdentifier;
				}
			}
			yield break;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000B2FB2 File Offset: 0x000B11B2
		internal bool HasPrincipals(int identifier)
		{
			return null != this._identifiers[identifier].References;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000B2FCC File Offset: 0x000B11CC
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

		// Token: 0x0600257C RID: 9596 RVA: 0x000B3010 File Offset: 0x000B1210
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

		// Token: 0x0600257D RID: 9597 RVA: 0x000B30EA File Offset: 0x000B12EA
		internal bool TryGetTempKey(EntityKey valueKey, out EntityKey tempKey)
		{
			return this._valueKeyToTempKey.TryGetValue(valueKey, out tempKey);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000B30FC File Offset: 0x000B12FC
		private void ValidateReferentialIntegrityGraphAcyclic(int node, byte[] color, KeyManager.LinkedList<int> parent)
		{
			color[node] = 2;
			KeyManager.LinkedList<int>.Add(ref parent, node);
			foreach (int num in KeyManager.LinkedList<int>.Enumerate(this._identifiers[node].References))
			{
				switch (color[num])
				{
				case 0:
					this.ValidateReferentialIntegrityGraphAcyclic(num, color, parent);
					break;
				case 2:
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
					throw new UpdateException(Strings.Update_CircularRelationships, null, list.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
				}
				}
			}
			color[node] = 1;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000B3210 File Offset: 0x000B1410
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

		// Token: 0x04000DED RID: 3565
		private const byte White = 0;

		// Token: 0x04000DEE RID: 3566
		private const byte Black = 1;

		// Token: 0x04000DEF RID: 3567
		private const byte Gray = 2;

		// Token: 0x04000DF0 RID: 3568
		private readonly Dictionary<Tuple<EntityKey, string, bool>, int> _foreignKeyIdentifiers = new Dictionary<Tuple<EntityKey, string, bool>, int>();

		// Token: 0x04000DF1 RID: 3569
		private readonly Dictionary<EntityKey, EntityKey> _valueKeyToTempKey = new Dictionary<EntityKey, EntityKey>();

		// Token: 0x04000DF2 RID: 3570
		private readonly Dictionary<EntityKey, int> _keyIdentifiers = new Dictionary<EntityKey, int>();

		// Token: 0x04000DF3 RID: 3571
		private readonly List<KeyManager.IdentifierInfo> _identifiers = new List<KeyManager.IdentifierInfo>
		{
			new KeyManager.IdentifierInfo()
		};

		// Token: 0x020003F8 RID: 1016
		private sealed class Partition
		{
			// Token: 0x06002583 RID: 9603 RVA: 0x000B32D0 File Offset: 0x000B14D0
			private Partition(int partitionId)
			{
				this._nodeIds = new List<int>(2);
				this.PartitionId = partitionId;
			}

			// Token: 0x06002584 RID: 9604 RVA: 0x000B32EC File Offset: 0x000B14EC
			internal static void CreatePartition(KeyManager manager, int firstId, int secondId)
			{
				KeyManager.Partition partition = new KeyManager.Partition(firstId);
				partition.AddNode(manager, firstId);
				partition.AddNode(manager, secondId);
			}

			// Token: 0x06002585 RID: 9605 RVA: 0x000B3310 File Offset: 0x000B1510
			internal void AddNode(KeyManager manager, int nodeId)
			{
				this._nodeIds.Add(nodeId);
				manager._identifiers[nodeId].Partition = this;
			}

			// Token: 0x06002586 RID: 9606 RVA: 0x000B3330 File Offset: 0x000B1530
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

			// Token: 0x04000DF6 RID: 3574
			internal readonly int PartitionId;

			// Token: 0x04000DF7 RID: 3575
			private readonly List<int> _nodeIds;
		}

		// Token: 0x020003F9 RID: 1017
		private sealed class LinkedList<T>
		{
			// Token: 0x06002587 RID: 9607 RVA: 0x000B3394 File Offset: 0x000B1594
			private LinkedList(T value, KeyManager.LinkedList<T> previous)
			{
				this._value = value;
				this._previous = previous;
			}

			// Token: 0x06002588 RID: 9608 RVA: 0x000B3498 File Offset: 0x000B1698
			internal static IEnumerable<T> Enumerate(KeyManager.LinkedList<T> current)
			{
				while (current != null)
				{
					yield return current._value;
					current = current._previous;
				}
				yield break;
			}

			// Token: 0x06002589 RID: 9609 RVA: 0x000B34B5 File Offset: 0x000B16B5
			internal static void Add(ref KeyManager.LinkedList<T> list, T value)
			{
				list = new KeyManager.LinkedList<T>(value, list);
			}

			// Token: 0x04000DF8 RID: 3576
			private readonly T _value;

			// Token: 0x04000DF9 RID: 3577
			private readonly KeyManager.LinkedList<T> _previous;
		}

		// Token: 0x020003FA RID: 1018
		private sealed class IdentifierInfo
		{
			// Token: 0x04000DFA RID: 3578
			internal KeyManager.Partition Partition;

			// Token: 0x04000DFB RID: 3579
			internal PropagatorResult Owner;

			// Token: 0x04000DFC RID: 3580
			internal KeyManager.LinkedList<IEntityStateEntry> DependentStateEntries;

			// Token: 0x04000DFD RID: 3581
			internal KeyManager.LinkedList<int> References;

			// Token: 0x04000DFE RID: 3582
			internal KeyManager.LinkedList<int> ReferencedBy;
		}
	}
}
