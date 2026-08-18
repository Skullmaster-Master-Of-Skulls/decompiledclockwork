using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Threading;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002D1 RID: 721
	internal abstract class UpdateCommand : IComparable<UpdateCommand>, IEquatable<UpdateCommand>
	{
		// Token: 0x06002A57 RID: 10839 RVA: 0x000A64DC File Offset: 0x000A46DC
		protected UpdateCommand(PropagatorResult originalValues, PropagatorResult currentValues)
		{
			this.m_originalValues = originalValues;
			this.m_currentValues = currentValues;
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002A58 RID: 10840
		internal abstract IEnumerable<int> OutputIdentifiers { get; }

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002A59 RID: 10841
		internal abstract IEnumerable<int> InputIdentifiers { get; }

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x00006174 File Offset: 0x00004374
		internal virtual EntitySet Table
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002A5B RID: 10843
		internal abstract UpdateCommandKind Kind { get; }

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x000A64F2 File Offset: 0x000A46F2
		internal PropagatorResult OriginalValues
		{
			get
			{
				return this.m_originalValues;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x000A64FA File Offset: 0x000A46FA
		internal PropagatorResult CurrentValues
		{
			get
			{
				return this.m_currentValues;
			}
		}

		// Token: 0x06002A5E RID: 10846
		internal abstract IList<IEntityStateEntry> GetStateEntries(UpdateTranslator translator);

		// Token: 0x06002A5F RID: 10847 RVA: 0x000A6504 File Offset: 0x000A4704
		internal void GetRequiredAndProducedEntities(UpdateTranslator translator, KeyToListMap<EntityKey, UpdateCommand> addedEntities, KeyToListMap<EntityKey, UpdateCommand> deletedEntities, KeyToListMap<EntityKey, UpdateCommand> addedRelationships, KeyToListMap<EntityKey, UpdateCommand> deletedRelationships)
		{
			IList<IEntityStateEntry> stateEntries = this.GetStateEntries(translator);
			foreach (IEntityStateEntry entityStateEntry in stateEntries)
			{
				if (!entityStateEntry.IsRelationship)
				{
					if (entityStateEntry.State == EntityState.Added)
					{
						addedEntities.Add(entityStateEntry.EntityKey, this);
					}
					else if (entityStateEntry.State == EntityState.Deleted)
					{
						deletedEntities.Add(entityStateEntry.EntityKey, this);
					}
				}
			}
			if (this.OriginalValues != null)
			{
				this.AddReferencedEntities(translator, this.OriginalValues, deletedRelationships);
			}
			if (this.CurrentValues != null)
			{
				this.AddReferencedEntities(translator, this.CurrentValues, addedRelationships);
			}
			foreach (IEntityStateEntry entityStateEntry2 in stateEntries)
			{
				if (entityStateEntry2.IsRelationship)
				{
					bool flag = entityStateEntry2.State == EntityState.Added;
					if (flag || entityStateEntry2.State == EntityState.Deleted)
					{
						DbDataRecord dbDataRecord = flag ? entityStateEntry2.CurrentValues : entityStateEntry2.OriginalValues;
						EntityKey key = (EntityKey)dbDataRecord[0];
						EntityKey key2 = (EntityKey)dbDataRecord[1];
						KeyToListMap<EntityKey, UpdateCommand> keyToListMap = flag ? addedRelationships : deletedRelationships;
						keyToListMap.Add(key, this);
						keyToListMap.Add(key2, this);
					}
				}
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000A665C File Offset: 0x000A485C
		private void AddReferencedEntities(UpdateTranslator translator, PropagatorResult result, KeyToListMap<EntityKey, UpdateCommand> referencedEntities)
		{
			foreach (PropagatorResult propagatorResult in result.GetMemberValues())
			{
				if (propagatorResult.IsSimple && propagatorResult.Identifier != -1 && PropagatorFlags.ForeignKey == (propagatorResult.PropagatorFlags & PropagatorFlags.ForeignKey))
				{
					foreach (int identifier in translator.KeyManager.GetDirectReferences(propagatorResult.Identifier))
					{
						PropagatorResult propagatorResult2;
						if (translator.KeyManager.TryGetIdentifierOwner(identifier, out propagatorResult2) && propagatorResult2.StateEntry != null)
						{
							referencedEntities.Add(propagatorResult2.StateEntry.EntityKey, this);
						}
					}
				}
			}
		}

		// Token: 0x06002A61 RID: 10849
		internal abstract long Execute(UpdateTranslator translator, EntityConnection connection, Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues);

		// Token: 0x06002A62 RID: 10850
		internal abstract int CompareToType(UpdateCommand other);

		// Token: 0x06002A63 RID: 10851 RVA: 0x000A6718 File Offset: 0x000A4918
		public int CompareTo(UpdateCommand other)
		{
			if (this.Equals(other))
			{
				return 0;
			}
			int num = this.Kind - other.Kind;
			if (num != 0)
			{
				return num;
			}
			num = this.CompareToType(other);
			if (num != 0)
			{
				return num;
			}
			if (this.m_orderingIdentifier == 0)
			{
				this.m_orderingIdentifier = Interlocked.Increment(ref UpdateCommand.s_orderingIdentifierCounter);
			}
			if (other.m_orderingIdentifier == 0)
			{
				other.m_orderingIdentifier = Interlocked.Increment(ref UpdateCommand.s_orderingIdentifierCounter);
			}
			return this.m_orderingIdentifier - other.m_orderingIdentifier;
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000A1177 File Offset: 0x0009F377
		public bool Equals(UpdateCommand other)
		{
			return base.Equals(other);
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000A1177 File Offset: 0x0009F377
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0009B148 File Offset: 0x00099348
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040012E1 RID: 4833
		private readonly PropagatorResult m_originalValues;

		// Token: 0x040012E2 RID: 4834
		private readonly PropagatorResult m_currentValues;

		// Token: 0x040012E3 RID: 4835
		private static int s_orderingIdentifierCounter;

		// Token: 0x040012E4 RID: 4836
		private int m_orderingIdentifier;
	}
}
