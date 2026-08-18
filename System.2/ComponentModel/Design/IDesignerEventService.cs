using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020005E7 RID: 1511
	public interface IDesignerEventService
	{
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060037F8 RID: 14328
		IDesignerHost ActiveDesigner { get; }

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060037F9 RID: 14329
		DesignerCollection Designers { get; }

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060037FA RID: 14330
		// (remove) Token: 0x060037FB RID: 14331
		event ActiveDesignerEventHandler ActiveDesignerChanged;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060037FC RID: 14332
		// (remove) Token: 0x060037FD RID: 14333
		event DesignerEventHandler DesignerCreated;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060037FE RID: 14334
		// (remove) Token: 0x060037FF RID: 14335
		event DesignerEventHandler DesignerDisposed;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06003800 RID: 14336
		// (remove) Token: 0x06003801 RID: 14337
		event EventHandler SelectionChanged;
	}
}
