using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x020005E9 RID: 1513
	[ComVisible(true)]
	public interface IDesignerHost : IServiceContainer, IServiceProvider
	{
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06003808 RID: 14344
		bool Loading { get; }

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06003809 RID: 14345
		bool InTransaction { get; }

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x0600380A RID: 14346
		IContainer Container { get; }

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x0600380B RID: 14347
		IComponent RootComponent { get; }

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x0600380C RID: 14348
		string RootComponentClassName { get; }

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x0600380D RID: 14349
		string TransactionDescription { get; }

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x0600380E RID: 14350
		// (remove) Token: 0x0600380F RID: 14351
		event EventHandler Activated;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06003810 RID: 14352
		// (remove) Token: 0x06003811 RID: 14353
		event EventHandler Deactivated;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06003812 RID: 14354
		// (remove) Token: 0x06003813 RID: 14355
		event EventHandler LoadComplete;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06003814 RID: 14356
		// (remove) Token: 0x06003815 RID: 14357
		event DesignerTransactionCloseEventHandler TransactionClosed;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06003816 RID: 14358
		// (remove) Token: 0x06003817 RID: 14359
		event DesignerTransactionCloseEventHandler TransactionClosing;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06003818 RID: 14360
		// (remove) Token: 0x06003819 RID: 14361
		event EventHandler TransactionOpened;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x0600381A RID: 14362
		// (remove) Token: 0x0600381B RID: 14363
		event EventHandler TransactionOpening;

		// Token: 0x0600381C RID: 14364
		void Activate();

		// Token: 0x0600381D RID: 14365
		IComponent CreateComponent(Type componentClass);

		// Token: 0x0600381E RID: 14366
		IComponent CreateComponent(Type componentClass, string name);

		// Token: 0x0600381F RID: 14367
		DesignerTransaction CreateTransaction();

		// Token: 0x06003820 RID: 14368
		DesignerTransaction CreateTransaction(string description);

		// Token: 0x06003821 RID: 14369
		void DestroyComponent(IComponent component);

		// Token: 0x06003822 RID: 14370
		IDesigner GetDesigner(IComponent component);

		// Token: 0x06003823 RID: 14371
		Type GetType(string typeName);
	}
}
