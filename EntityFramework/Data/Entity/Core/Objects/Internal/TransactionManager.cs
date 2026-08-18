using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200059A RID: 1434
	internal class TransactionManager
	{
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x00109EAF File Offset: 0x001080AF
		// (set) Token: 0x06003800 RID: 14336 RVA: 0x00109EB7 File Offset: 0x001080B7
		internal Dictionary<RelatedEnd, IList<IEntityWrapper>> PromotedRelationships { get; private set; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x00109EC0 File Offset: 0x001080C0
		// (set) Token: 0x06003802 RID: 14338 RVA: 0x00109EC8 File Offset: 0x001080C8
		internal Dictionary<object, EntityEntry> PromotedKeyEntries { get; private set; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x00109ED1 File Offset: 0x001080D1
		// (set) Token: 0x06003804 RID: 14340 RVA: 0x00109ED9 File Offset: 0x001080D9
		internal HashSet<EntityReference> PopulatedEntityReferences { get; private set; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06003805 RID: 14341 RVA: 0x00109EE2 File Offset: 0x001080E2
		// (set) Token: 0x06003806 RID: 14342 RVA: 0x00109EEA File Offset: 0x001080EA
		internal HashSet<EntityReference> AlignedEntityReferences { get; private set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x00109EF3 File Offset: 0x001080F3
		// (set) Token: 0x06003808 RID: 14344 RVA: 0x00109EFB File Offset: 0x001080FB
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

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x00109F04 File Offset: 0x00108104
		// (set) Token: 0x0600380A RID: 14346 RVA: 0x00109F0C File Offset: 0x0010810C
		internal HashSet<IEntityWrapper> ProcessedEntities { get; private set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x0600380B RID: 14347 RVA: 0x00109F15 File Offset: 0x00108115
		// (set) Token: 0x0600380C RID: 14348 RVA: 0x00109F1D File Offset: 0x0010811D
		internal Dictionary<object, IEntityWrapper> WrappedEntities { get; private set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x00109F26 File Offset: 0x00108126
		// (set) Token: 0x0600380E RID: 14350 RVA: 0x00109F2E File Offset: 0x0010812E
		internal bool TrackProcessedEntities { get; private set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x0600380F RID: 14351 RVA: 0x00109F37 File Offset: 0x00108137
		// (set) Token: 0x06003810 RID: 14352 RVA: 0x00109F3F File Offset: 0x0010813F
		internal bool IsAddTracking { get; private set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06003811 RID: 14353 RVA: 0x00109F48 File Offset: 0x00108148
		// (set) Token: 0x06003812 RID: 14354 RVA: 0x00109F50 File Offset: 0x00108150
		internal bool IsAttachTracking { get; private set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06003813 RID: 14355 RVA: 0x00109F59 File Offset: 0x00108159
		// (set) Token: 0x06003814 RID: 14356 RVA: 0x00109F61 File Offset: 0x00108161
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>> AddedRelationshipsByGraph { get; private set; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06003815 RID: 14357 RVA: 0x00109F6A File Offset: 0x0010816A
		// (set) Token: 0x06003816 RID: 14358 RVA: 0x00109F72 File Offset: 0x00108172
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>> DeletedRelationshipsByGraph { get; private set; }

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06003817 RID: 14359 RVA: 0x00109F7B File Offset: 0x0010817B
		// (set) Token: 0x06003818 RID: 14360 RVA: 0x00109F83 File Offset: 0x00108183
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> AddedRelationshipsByForeignKey { get; private set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x00109F8C File Offset: 0x0010818C
		// (set) Token: 0x0600381A RID: 14362 RVA: 0x00109F94 File Offset: 0x00108194
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> AddedRelationshipsByPrincipalKey { get; private set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x00109F9D File Offset: 0x0010819D
		// (set) Token: 0x0600381C RID: 14364 RVA: 0x00109FA5 File Offset: 0x001081A5
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> DeletedRelationshipsByForeignKey { get; private set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x00109FAE File Offset: 0x001081AE
		// (set) Token: 0x0600381E RID: 14366 RVA: 0x00109FB6 File Offset: 0x001081B6
		internal Dictionary<IEntityWrapper, HashSet<RelatedEnd>> ChangedForeignKeys { get; private set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x00109FBF File Offset: 0x001081BF
		// (set) Token: 0x06003820 RID: 14368 RVA: 0x00109FC7 File Offset: 0x001081C7
		internal bool IsDetectChanges { get; private set; }

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x00109FD0 File Offset: 0x001081D0
		// (set) Token: 0x06003822 RID: 14370 RVA: 0x00109FD8 File Offset: 0x001081D8
		internal bool IsAlignChanges { get; private set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x00109FE1 File Offset: 0x001081E1
		// (set) Token: 0x06003824 RID: 14372 RVA: 0x00109FE9 File Offset: 0x001081E9
		internal bool IsLocalPublicAPI { get; private set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06003825 RID: 14373 RVA: 0x00109FF2 File Offset: 0x001081F2
		// (set) Token: 0x06003826 RID: 14374 RVA: 0x00109FFA File Offset: 0x001081FA
		internal bool IsOriginalValuesGetter { get; private set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06003827 RID: 14375 RVA: 0x0010A003 File Offset: 0x00108203
		// (set) Token: 0x06003828 RID: 14376 RVA: 0x0010A00B File Offset: 0x0010820B
		internal bool IsForeignKeyUpdate { get; private set; }

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x0010A014 File Offset: 0x00108214
		// (set) Token: 0x0600382A RID: 14378 RVA: 0x0010A01C File Offset: 0x0010821C
		internal bool IsRelatedEndAdd { get; private set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600382B RID: 14379 RVA: 0x0010A025 File Offset: 0x00108225
		internal bool IsGraphUpdate
		{
			get
			{
				return this._graphUpdateCount != 0;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x0600382C RID: 14380 RVA: 0x0010A033 File Offset: 0x00108233
		// (set) Token: 0x0600382D RID: 14381 RVA: 0x0010A03B File Offset: 0x0010823B
		internal object EntityBeingReparented { get; set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x0010A044 File Offset: 0x00108244
		// (set) Token: 0x0600382F RID: 14383 RVA: 0x0010A04C File Offset: 0x0010824C
		internal bool IsDetaching { get; private set; }

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06003830 RID: 14384 RVA: 0x0010A055 File Offset: 0x00108255
		// (set) Token: 0x06003831 RID: 14385 RVA: 0x0010A05D File Offset: 0x0010825D
		internal EntityReference RelationshipBeingUpdated { get; private set; }

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06003832 RID: 14386 RVA: 0x0010A066 File Offset: 0x00108266
		// (set) Token: 0x06003833 RID: 14387 RVA: 0x0010A06E File Offset: 0x0010826E
		internal bool IsFixupByReference { get; private set; }

		// Token: 0x06003834 RID: 14388 RVA: 0x0010A078 File Offset: 0x00108278
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
				this.WrappedEntities = new Dictionary<object, IEntityWrapper>(ObjectReferenceEqualityComparer.Default);
			}
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x0010A0D7 File Offset: 0x001082D7
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

		// Token: 0x06003836 RID: 14390 RVA: 0x0010A114 File Offset: 0x00108314
		internal void BeginAttachTracking()
		{
			this.IsAttachTracking = true;
			this.PromotedRelationships = new Dictionary<RelatedEnd, IList<IEntityWrapper>>();
			this.PromotedKeyEntries = new Dictionary<object, EntityEntry>(ObjectReferenceEqualityComparer.Default);
			this.PopulatedEntityReferences = new HashSet<EntityReference>();
			this.AlignedEntityReferences = new HashSet<EntityReference>();
			this.TrackProcessedEntities = true;
			this.ProcessedEntities = new HashSet<IEntityWrapper>();
			this.WrappedEntities = new Dictionary<object, IEntityWrapper>(ObjectReferenceEqualityComparer.Default);
			this.OriginalMergeOption = null;
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x0010A18C File Offset: 0x0010838C
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

		// Token: 0x06003838 RID: 14392 RVA: 0x0010A1E0 File Offset: 0x001083E0
		internal bool BeginDetectChanges()
		{
			if (this.IsDetectChanges)
			{
				return false;
			}
			this.IsDetectChanges = true;
			this.TrackProcessedEntities = true;
			this.ProcessedEntities = new HashSet<IEntityWrapper>();
			this.WrappedEntities = new Dictionary<object, IEntityWrapper>(ObjectReferenceEqualityComparer.Default);
			this.DeletedRelationshipsByGraph = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>>();
			this.AddedRelationshipsByGraph = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>>();
			this.DeletedRelationshipsByForeignKey = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>>();
			this.AddedRelationshipsByForeignKey = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>>();
			this.AddedRelationshipsByPrincipalKey = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>>();
			this.ChangedForeignKeys = new Dictionary<IEntityWrapper, HashSet<RelatedEnd>>();
			return true;
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x0010A264 File Offset: 0x00108464
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

		// Token: 0x0600383A RID: 14394 RVA: 0x0010A2B7 File Offset: 0x001084B7
		internal void BeginAlignChanges()
		{
			this.IsAlignChanges = true;
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x0010A2C0 File Offset: 0x001084C0
		internal void EndAlignChanges()
		{
			this.IsAlignChanges = false;
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x0010A2C9 File Offset: 0x001084C9
		internal void ResetProcessedEntities()
		{
			this.ProcessedEntities.Clear();
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x0010A2D6 File Offset: 0x001084D6
		internal void BeginLocalPublicAPI()
		{
			this.IsLocalPublicAPI = true;
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x0010A2DF File Offset: 0x001084DF
		internal void EndLocalPublicAPI()
		{
			this.IsLocalPublicAPI = false;
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x0010A2E8 File Offset: 0x001084E8
		internal void BeginOriginalValuesGetter()
		{
			this.IsOriginalValuesGetter = true;
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x0010A2F1 File Offset: 0x001084F1
		internal void EndOriginalValuesGetter()
		{
			this.IsOriginalValuesGetter = false;
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x0010A2FA File Offset: 0x001084FA
		internal void BeginForeignKeyUpdate(EntityReference relationship)
		{
			this.RelationshipBeingUpdated = relationship;
			this.IsForeignKeyUpdate = true;
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x0010A30A File Offset: 0x0010850A
		internal void EndForeignKeyUpdate()
		{
			this.RelationshipBeingUpdated = null;
			this.IsForeignKeyUpdate = false;
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x0010A31A File Offset: 0x0010851A
		internal void BeginRelatedEndAdd()
		{
			this.IsRelatedEndAdd = true;
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x0010A323 File Offset: 0x00108523
		internal void EndRelatedEndAdd()
		{
			this.IsRelatedEndAdd = false;
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x0010A32C File Offset: 0x0010852C
		internal void BeginGraphUpdate()
		{
			this._graphUpdateCount++;
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x0010A33C File Offset: 0x0010853C
		internal void EndGraphUpdate()
		{
			this._graphUpdateCount--;
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x0010A34C File Offset: 0x0010854C
		internal void BeginDetaching()
		{
			this.IsDetaching = true;
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x0010A355 File Offset: 0x00108555
		internal void EndDetaching()
		{
			this.IsDetaching = false;
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x0010A35E File Offset: 0x0010855E
		internal void BeginFixupKeysByReference()
		{
			this.IsFixupByReference = true;
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x0010A367 File Offset: 0x00108567
		internal void EndFixupKeysByReference()
		{
			this.IsFixupByReference = false;
		}

		// Token: 0x04001589 RID: 5513
		private MergeOption? _originalMergeOption;

		// Token: 0x0400158A RID: 5514
		private int _graphUpdateCount;
	}
}
