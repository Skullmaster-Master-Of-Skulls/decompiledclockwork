using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003ED RID: 1005
	internal abstract class UpdateCommand : IComparable<UpdateCommand>, IEquatable<UpdateCommand>
	{
		// Token: 0x06002514 RID: 9492 RVA: 0x000AEEE8 File Offset: 0x000AD0E8
		protected UpdateCommand(UpdateTranslator translator, PropagatorResult originalValues, PropagatorResult currentValues)
		{
			this.OriginalValues = originalValues;
			this.CurrentValues = currentValues;
			this.Translator = translator;
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002515 RID: 9493
		internal abstract IEnumerable<int> OutputIdentifiers { get; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002516 RID: 9494
		internal abstract IEnumerable<int> InputIdentifiers { get; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000AEF05 File Offset: 0x000AD105
		internal virtual EntitySet Table
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06002518 RID: 9496
		internal abstract UpdateCommandKind Kind { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000AEF08 File Offset: 0x000AD108
		// (set) Token: 0x0600251A RID: 9498 RVA: 0x000AEF10 File Offset: 0x000AD110
		internal PropagatorResult OriginalValues { get; private set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x000AEF19 File Offset: 0x000AD119
		// (set) Token: 0x0600251C RID: 9500 RVA: 0x000AEF21 File Offset: 0x000AD121
		internal PropagatorResult CurrentValues { get; private set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000AEF2A File Offset: 0x000AD12A
		// (set) Token: 0x0600251E RID: 9502 RVA: 0x000AEF32 File Offset: 0x000AD132
		private protected UpdateTranslator Translator { protected get; private set; }

		// Token: 0x0600251F RID: 9503
		internal abstract IList<IEntityStateEntry> GetStateEntries(UpdateTranslator translator);

		// Token: 0x06002520 RID: 9504 RVA: 0x000AEF3C File Offset: 0x000AD13C
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

		// Token: 0x06002521 RID: 9505 RVA: 0x000AF094 File Offset: 0x000AD294
		private void AddReferencedEntities(UpdateTranslator translator, PropagatorResult result, KeyToListMap<EntityKey, UpdateCommand> referencedEntities)
		{
			foreach (PropagatorResult propagatorResult in result.GetMemberValues())
			{
				if (propagatorResult.IsSimple && propagatorResult.Identifier != -1 && 32 == (byte)(propagatorResult.PropagatorFlags & PropagatorFlags.ForeignKey))
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

		// Token: 0x06002522 RID: 9506
		internal abstract long Execute(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues);

		// Token: 0x06002523 RID: 9507
		internal abstract Task<long> ExecuteAsync(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues, CancellationToken cancellationToken);

		// Token: 0x06002524 RID: 9508
		internal abstract int CompareToType(UpdateCommand other);

		// Token: 0x06002525 RID: 9509 RVA: 0x000AF158 File Offset: 0x000AD358
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
			if (this._orderingIdentifier == 0)
			{
				this._orderingIdentifier = Interlocked.Increment(ref UpdateCommand.OrderingIdentifierCounter);
			}
			if (other._orderingIdentifier == 0)
			{
				other._orderingIdentifier = Interlocked.Increment(ref UpdateCommand.OrderingIdentifierCounter);
			}
			return this._orderingIdentifier - other._orderingIdentifier;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000AF1CD File Offset: 0x000AD3CD
		public bool Equals(UpdateCommand other)
		{
			return base.Equals(other);
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000AF1D6 File Offset: 0x000AD3D6
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000AF1DF File Offset: 0x000AD3DF
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000DC2 RID: 3522
		private static int OrderingIdentifierCounter;

		// Token: 0x04000DC3 RID: 3523
		private int _orderingIdentifier;
	}
}
