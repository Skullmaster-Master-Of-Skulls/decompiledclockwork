using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000774 RID: 1908
	internal interface IEntityStateEntry
	{
		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x0600566B RID: 22123
		object Entity { get; }

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x0600566C RID: 22124
		EntityState State { get; }

		// Token: 0x0600566D RID: 22125
		void ChangeState(EntityState state);

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x0600566E RID: 22126
		DbUpdatableDataRecord CurrentValues { get; }

		// Token: 0x0600566F RID: 22127
		DbUpdatableDataRecord GetUpdatableOriginalValues();

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06005670 RID: 22128
		EntitySetBase EntitySet { get; }

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06005671 RID: 22129
		EntityKey EntityKey { get; }

		// Token: 0x06005672 RID: 22130
		IEnumerable<string> GetModifiedProperties();

		// Token: 0x06005673 RID: 22131
		void SetModifiedProperty(string propertyName);

		// Token: 0x06005674 RID: 22132
		bool IsPropertyChanged(string propertyName);

		// Token: 0x06005675 RID: 22133
		void RejectPropertyChanges(string propertyName);
	}
}
