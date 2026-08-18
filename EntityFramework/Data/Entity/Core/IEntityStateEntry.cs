using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Core
{
	// Token: 0x020003A1 RID: 929
	internal interface IEntityStateEntry
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06002196 RID: 8598
		IEntityStateManager StateManager { get; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06002197 RID: 8599
		EntityKey EntityKey { get; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06002198 RID: 8600
		EntitySetBase EntitySet { get; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06002199 RID: 8601
		bool IsRelationship { get; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600219A RID: 8602
		bool IsKeyEntry { get; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600219B RID: 8603
		EntityState State { get; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600219C RID: 8604
		DbDataRecord OriginalValues { get; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600219D RID: 8605
		CurrentValueRecord CurrentValues { get; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600219E RID: 8606
		BitArray ModifiedProperties { get; }

		// Token: 0x0600219F RID: 8607
		void AcceptChanges();

		// Token: 0x060021A0 RID: 8608
		void Delete();

		// Token: 0x060021A1 RID: 8609
		void SetModified();

		// Token: 0x060021A2 RID: 8610
		void SetModifiedProperty(string propertyName);

		// Token: 0x060021A3 RID: 8611
		IEnumerable<string> GetModifiedProperties();
	}
}
