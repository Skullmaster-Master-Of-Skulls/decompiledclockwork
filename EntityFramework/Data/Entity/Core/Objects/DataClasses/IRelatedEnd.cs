using System;
using System.Collections;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200053A RID: 1338
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public interface IRelatedEnd
	{
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060032F6 RID: 13046
		// (set) Token: 0x060032F7 RID: 13047
		bool IsLoaded { get; set; }

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060032F8 RID: 13048
		string RelationshipName { get; }

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060032F9 RID: 13049
		string SourceRoleName { get; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060032FA RID: 13050
		string TargetRoleName { get; }

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060032FB RID: 13051
		RelationshipSet RelationshipSet { get; }

		// Token: 0x060032FC RID: 13052
		void Load();

		// Token: 0x060032FD RID: 13053
		Task LoadAsync(CancellationToken cancellationToken);

		// Token: 0x060032FE RID: 13054
		void Load(MergeOption mergeOption);

		// Token: 0x060032FF RID: 13055
		Task LoadAsync(MergeOption mergeOption, CancellationToken cancellationToken);

		// Token: 0x06003300 RID: 13056
		void Add(IEntityWithRelationships entity);

		// Token: 0x06003301 RID: 13057
		void Add(object entity);

		// Token: 0x06003302 RID: 13058
		bool Remove(IEntityWithRelationships entity);

		// Token: 0x06003303 RID: 13059
		bool Remove(object entity);

		// Token: 0x06003304 RID: 13060
		void Attach(IEntityWithRelationships entity);

		// Token: 0x06003305 RID: 13061
		void Attach(object entity);

		// Token: 0x06003306 RID: 13062
		IEnumerable CreateSourceQuery();

		// Token: 0x06003307 RID: 13063
		IEnumerator GetEnumerator();
	}
}
