using System;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200017D RID: 381
	internal class NullEntityWrapper : IEntityWrapper
	{
		// Token: 0x06001BA4 RID: 7076 RVA: 0x00002050 File Offset: 0x00000250
		private NullEntityWrapper()
		{
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0005F5CD File Offset: 0x0005D7CD
		internal static IEntityWrapper NullWrapper
		{
			get
			{
				return NullEntityWrapper.s_nullWrapper;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x00006174 File Offset: 0x00004374
		public RelationshipManager RelationshipManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x000173E2 File Offset: 0x000155E2
		public bool OwnsRelationshipManager
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x00006174 File Offset: 0x00004374
		public object Entity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00006174 File Offset: 0x00004374
		// (set) Token: 0x06001BAA RID: 7082 RVA: 0x000089D0 File Offset: 0x00006BD0
		public EntityEntry ObjectStateEntry
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x000173E2 File Offset: 0x000155E2
		public bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			return false;
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x00006174 File Offset: 0x00004374
		// (set) Token: 0x06001BAE RID: 7086 RVA: 0x000089D0 File Offset: 0x00006BD0
		public EntityKey EntityKey
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00006174 File Offset: 0x00004374
		public EntityKey GetEntityKeyFromEntity()
		{
			return null;
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x00006174 File Offset: 0x00004374
		// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x000089D0 File Offset: 0x00006BD0
		public ObjectContext Context
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x0003BF8C File Offset: 0x0003A18C
		public MergeOption MergeOption
		{
			get
			{
				return MergeOption.NoTracking;
			}
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void ResetContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void DetachContext()
		{
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void TakeSnapshot(EntityEntry entry)
		{
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void TakeSnapshotOfRelationships(EntityEntry entry)
		{
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x00006174 File Offset: 0x00004374
		public Type IdentityType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void EnsureCollectionNotNull(RelatedEnd relatedEnd)
		{
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00006174 File Offset: 0x00004374
		public object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			return null;
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x000173E2 File Offset: 0x000155E2
		// (set) Token: 0x06001BC0 RID: 7104 RVA: 0x000089D0 File Offset: 0x00006BD0
		public bool InitializingProxyRelatedEnds
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x000173E2 File Offset: 0x000155E2
		public bool RequiresRelationshipChangeTracking
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000B7C RID: 2940
		private static IEntityWrapper s_nullWrapper = new NullEntityWrapper();
	}
}
