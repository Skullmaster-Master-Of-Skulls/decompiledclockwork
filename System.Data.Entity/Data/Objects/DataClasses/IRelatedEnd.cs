using System;
using System.Collections;
using System.Data.Metadata.Edm;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000193 RID: 403
	public interface IRelatedEnd
	{
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001CF5 RID: 7413
		bool IsLoaded { get; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001CF6 RID: 7414
		string RelationshipName { get; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001CF7 RID: 7415
		string SourceRoleName { get; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001CF8 RID: 7416
		string TargetRoleName { get; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001CF9 RID: 7417
		RelationshipSet RelationshipSet { get; }

		// Token: 0x06001CFA RID: 7418
		void Load();

		// Token: 0x06001CFB RID: 7419
		void Load(MergeOption mergeOption);

		// Token: 0x06001CFC RID: 7420
		void Add(IEntityWithRelationships entity);

		// Token: 0x06001CFD RID: 7421
		void Add(object entity);

		// Token: 0x06001CFE RID: 7422
		bool Remove(IEntityWithRelationships entity);

		// Token: 0x06001CFF RID: 7423
		bool Remove(object entity);

		// Token: 0x06001D00 RID: 7424
		void Attach(IEntityWithRelationships entity);

		// Token: 0x06001D01 RID: 7425
		void Attach(object entity);

		// Token: 0x06001D02 RID: 7426
		IEnumerable CreateSourceQuery();

		// Token: 0x06001D03 RID: 7427
		IEnumerator GetEnumerator();
	}
}
