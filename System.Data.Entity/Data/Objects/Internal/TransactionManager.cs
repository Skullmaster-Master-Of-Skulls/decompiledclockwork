using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200017E RID: 382
	internal class TransactionManager
	{
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0005F5E0 File Offset: 0x0005D7E0
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x0005F5E8 File Offset: 0x0005D7E8
		internal Dictionary<RelatedEnd, IList<IEntityWrapper>> PromotedRelationships { get; private set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0005F5F1 File Offset: 0x0005D7F1
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x0005F5F9 File Offset: 0x0005D7F9
		internal Dictionary<object, EntityEntry> PromotedKeyEntries { get; private set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x0005F602 File Offset: 0x0005D802
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x0005F60A File Offset: 0x0005D80A
		internal HashSet<EntityReference> PopulatedEntityReferences { get; private set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0005F613 File Offset: 0x0005D813
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x0005F61B File Offset: 0x0005D81B
		internal HashSet<EntityReference> AlignedEntityReferences { get; private set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0005F624 File Offset: 0x0005D824
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x0005F62C File Offset: 0x0005D82C
		internal MergeOption? OriginalMergeOption
		{
			get
			{
				return this._originalMergeOption;
			}
			set
			{
				this._originalMergeOption = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0005F635 File Offset: 0x0005D835
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x0005F63D File Offset: 0x0005D83D
		internal HashSet<IEntityWrapper> ProcessedEntities { get; private set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0005F646 File Offset: 0x0005D846
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x0005F64E File Offset: 0x0005D84E
		internal Dictionary<object, IEntityWrapper> WrappedEntities { get; private set; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0005F657 File Offset: 0x0005D857
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x0005F65F File Offset: 0x0005D85F
		internal bool TrackProcessedEntities { get; private set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x0005F668 File Offset: 0x0005D868
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x0005F670 File Offset: 0x0005D870
		internal bool IsAddTracking { get; private set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0005F679 File Offset: 0x0005D879
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x0005F681 File Offset: 0x0005D881
		internal bool IsAttachTracking { get; private set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0005F68A File Offset: 0x0005D88A
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x0005F692 File Offset: 0x0005D892
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>> AddedRelationshipsByGraph { get; private set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0005F69B File Offset: 0x0005D89B
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x0005F6A3 File Offset: 0x0005D8A3
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>> DeletedRelationshipsByGraph { get; private set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x0005F6AC File Offset: 0x0005D8AC
		// (set) Token: 0x06001BDD RID: 7133 RVA: 0x0005F6B4 File Offset: 0x0005D8B4
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> AddedRelationshipsByForeignKey { get; private set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x0005F6BD File Offset: 0x0005D8BD
		// (set) Token: 0x06001BDF RID: 7135 RVA: 0x0005F6C5 File Offset: 0x0005D8C5
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> AddedRelationshipsByPrincipalKey { get; private set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0005F6CE File Offset: 0x0005D8CE
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x0005F6D6 File Offset: 0x0005D8D6
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> DeletedRelationshipsByForeignKey { get; private set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0005F6DF File Offset: 0x0005D8DF
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x0005F6E7 File Offset: 0x0005D8E7
		internal Dictionary<IEntityWrapper, HashSet<RelatedEnd>> ChangedForeignKeys { get; private set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0005F6F0 File Offset: 0x0005D8F0
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x0005F6F8 File Offset: 0x0005D8F8
		internal bool IsDetectChanges { get; private set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0005F701 File Offset: 0x0005D901
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x0005F709 File Offset: 0x0005D909
		internal bool IsAlignChanges { get; private set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x0005F712 File Offset: 0x0005D912
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x0005F71A File Offset: 0x0005D91A
		internal bool IsLocalPublicAPI { get; private set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0005F723 File Offset: 0x0005D923
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x0005F72B File Offset: 0x0005D92B
		internal bool IsOriginalValuesGetter { get; private set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0005F734 File Offset: 0x0005D934
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x0005F73C File Offset: 0x0005D93C
		internal bool IsForeignKeyUpdate { get; private set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x0005F745 File Offset: 0x0005D945
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x0005F74D File Offset: 0x0005D94D
		internal bool IsRelatedEndAdd { get; private set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0005F756 File Offset: 0x0005D956
		internal bool IsGraphUpdate
		{
			get
			{
				return this._graphUpdateCount != 0;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x0005F761 File Offset: 0x0005D961
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x0005F769 File Offset: 0x0005D969
		internal object EntityBeingReparented { get; set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x0005F772 File Offset: 0x0005D972
		// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x0005F77A File Offset: 0x0005D97A
		internal bool IsDetaching { get; private set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x0005F783 File Offset: 0x0005D983
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x0005F78B File Offset: 0x0005D98B
		internal EntityReference RelationshipBeingUpdated { get; private set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0005F794 File Offset: 0x0005D994
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x0005F79C File Offset: 0x0005D99C
		internal bool IsFixupByReference { get; private set; }

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0005F7A8 File Offset: 0x0005D9A8
		internal void BeginAddTracking()
		{
			this.IsAddTracking = true;
			this.PopulatedEntityReferences = new HashSet<EntityReference>();
			this.AlignedEntityReferences = new HashSet<EntityReference>();
			this.PromotedRelationships = new Dictionary<RelatedEnd, IList<IEntityWrapper>>();
			if (!this.IsDetectChanges)
			{
				this.TrackProcessedEntities = true;
				this.ProcessedEntities = new HashSet<IEntityWrapper>();
				this.WrappedEntities = new Dictionary<object, IEntityWrapper>();
			}
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0005F802 File Offset: 0x0005DA02
		internal void EndAddTracking()
		{
			this.IsAddTracking = false;
			this.PopulatedEntityReferences = null;
			this.AlignedEntityReferences = null;
			this.PromotedRelationships = null;
			if (!this.IsDetectChanges)
			{
				this.TrackProcessedEntities = false;
				this.ProcessedEntities = null;
				this.WrappedEntities = null;
			}
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0005F840 File Offset: 0x0005DA40
		internal void BeginAttachTracking()
		{
			this.IsAttachTracking = true;
			this.PromotedRelationships = new Dictionary<RelatedEnd, IList<IEntityWrapper>>();
			this.PromotedKeyEntries = new Dictionary<object, EntityEntry>();
			this.PopulatedEntityReferences = new HashSet<EntityReference>();
			this.AlignedEntityReferences = new HashSet<EntityReference>();
			this.TrackProcessedEntities = true;
			this.ProcessedEntities = new HashSet<IEntityWrapper>();
			this.WrappedEntities = new Dictionary<object, IEntityWrapper>();
			this.OriginalMergeOption = null;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x0005F8AC File Offset: 0x0005DAAC
		internal void EndAttachTracking()
		{
			this.IsAttachTracking = false;
			this.PromotedRelationships = null;
			this.PromotedKeyEntries = null;
			this.PopulatedEntityReferences = null;
			this.AlignedEntityReferences = null;
			this.TrackProcessedEntities = false;
			this.ProcessedEntities = null;
			this.WrappedEntities = null;
			this.OriginalMergeOption = null;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0005F900 File Offset: 0x0005DB00
		internal bool BeginDetectChanges()
		{
			if (this.IsDetectChanges)
			{
				return false;
			}
			this.IsDetectChanges = true;
			this.TrackProcessedEntities = true;
			this.ProcessedEntities = new HashSet<IEntityWrapper>();
			this.WrappedEntities = new Dictionary<object, IEntityWrapper>();
			this.DeletedRelationshipsByGraph = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>>();
			this.AddedRelationshipsByGraph = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>>();
			this.DeletedRelationshipsByForeignKey = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>>();
			this.AddedRelationshipsByForeignKey = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>>();
			this.AddedRelationshipsByPrincipalKey = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>>();
			this.ChangedForeignKeys = new Dictionary<IEntityWrapper, HashSet<RelatedEnd>>();
			return true;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0005F980 File Offset: 0x0005DB80
		internal void EndDetectChanges()
		{
			this.IsDetectChanges = false;
			this.TrackProcessedEntities = false;
			this.ProcessedEntities = null;
			this.WrappedEntities = null;
			this.DeletedRelationshipsByGraph = null;
			this.AddedRelationshipsByGraph = null;
			this.DeletedRelationshipsByForeignKey = null;
			this.AddedRelationshipsByForeignKey = null;
			this.AddedRelationshipsByPrincipalKey = null;
			this.ChangedForeignKeys = null;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0005F9D3 File Offset: 0x0005DBD3
		internal void BeginAlignChanges()
		{
			this.IsAlignChanges = true;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x0005F9DC File Offset: 0x0005DBDC
		internal void EndAlignChanges()
		{
			this.IsAlignChanges = false;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0005F9E5 File Offset: 0x0005DBE5
		internal void ResetProcessedEntities()
		{
			this.ProcessedEntities.Clear();
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0005F9F2 File Offset: 0x0005DBF2
		internal void BeginLocalPublicAPI()
		{
			this.IsLocalPublicAPI = true;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0005F9FB File Offset: 0x0005DBFB
		internal void EndLocalPublicAPI()
		{
			this.IsLocalPublicAPI = false;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0005FA04 File Offset: 0x0005DC04
		internal void BeginOriginalValuesGetter()
		{
			this.IsOriginalValuesGetter = true;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x0005FA0D File Offset: 0x0005DC0D
		internal void EndOriginalValuesGetter()
		{
			this.IsOriginalValuesGetter = false;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x0005FA16 File Offset: 0x0005DC16
		internal void BeginForeignKeyUpdate(EntityReference relationship)
		{
			this.RelationshipBeingUpdated = relationship;
			this.IsForeignKeyUpdate = true;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0005FA26 File Offset: 0x0005DC26
		internal void EndForeignKeyUpdate()
		{
			this.RelationshipBeingUpdated = null;
			this.IsForeignKeyUpdate = false;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0005FA36 File Offset: 0x0005DC36
		internal void BeginRelatedEndAdd()
		{
			this.IsRelatedEndAdd = true;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x0005FA3F File Offset: 0x0005DC3F
		internal void EndRelatedEndAdd()
		{
			this.IsRelatedEndAdd = false;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0005FA48 File Offset: 0x0005DC48
		internal void BeginGraphUpdate()
		{
			this._graphUpdateCount++;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0005FA58 File Offset: 0x0005DC58
		internal void EndGraphUpdate()
		{
			this._graphUpdateCount--;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0005FA68 File Offset: 0x0005DC68
		internal void BeginDetaching()
		{
			this.IsDetaching = true;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0005FA71 File Offset: 0x0005DC71
		internal void EndDetaching()
		{
			this.IsDetaching = false;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0005FA7A File Offset: 0x0005DC7A
		internal void BeginFixupKeysByReference()
		{
			this.IsFixupByReference = true;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0005FA83 File Offset: 0x0005DC83
		internal void EndFixupKeysByReference()
		{
			this.IsFixupByReference = false;
		}

		// Token: 0x04000B81 RID: 2945
		private MergeOption? _originalMergeOption;

		// Token: 0x04000B93 RID: 2963
		private int _graphUpdateCount;
	}
}
