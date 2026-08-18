using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x020005E3 RID: 1507
	[ComVisible(true)]
	public interface IComponentChangeService
	{
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060037E1 RID: 14305
		// (remove) Token: 0x060037E2 RID: 14306
		event ComponentEventHandler ComponentAdded;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060037E3 RID: 14307
		// (remove) Token: 0x060037E4 RID: 14308
		event ComponentEventHandler ComponentAdding;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060037E5 RID: 14309
		// (remove) Token: 0x060037E6 RID: 14310
		event ComponentChangedEventHandler ComponentChanged;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060037E7 RID: 14311
		// (remove) Token: 0x060037E8 RID: 14312
		event ComponentChangingEventHandler ComponentChanging;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060037E9 RID: 14313
		// (remove) Token: 0x060037EA RID: 14314
		event ComponentEventHandler ComponentRemoved;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060037EB RID: 14315
		// (remove) Token: 0x060037EC RID: 14316
		event ComponentEventHandler ComponentRemoving;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060037ED RID: 14317
		// (remove) Token: 0x060037EE RID: 14318
		event ComponentRenameEventHandler ComponentRename;

		// Token: 0x060037EF RID: 14319
		void OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue);

		// Token: 0x060037F0 RID: 14320
		void OnComponentChanging(object component, MemberDescriptor member);
	}
}
