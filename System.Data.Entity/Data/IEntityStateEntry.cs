using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Objects;

namespace System.Data
{
	// Token: 0x0200001C RID: 28
	internal interface IEntityStateEntry
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001FF RID: 511
		IEntityStateManager StateManager { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000200 RID: 512
		EntityKey EntityKey { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000201 RID: 513
		EntitySetBase EntitySet { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000202 RID: 514
		bool IsRelationship { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000203 RID: 515
		bool IsKeyEntry { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000204 RID: 516
		EntityState State { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000205 RID: 517
		DbDataRecord OriginalValues { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000206 RID: 518
		CurrentValueRecord CurrentValues { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000207 RID: 519
		BitArray ModifiedProperties { get; }

		// Token: 0x06000208 RID: 520
		void AcceptChanges();

		// Token: 0x06000209 RID: 521
		void Delete();

		// Token: 0x0600020A RID: 522
		void SetModified();

		// Token: 0x0600020B RID: 523
		void SetModifiedProperty(string propertyName);

		// Token: 0x0600020C RID: 524
		IEnumerable<string> GetModifiedProperties();
	}
}
