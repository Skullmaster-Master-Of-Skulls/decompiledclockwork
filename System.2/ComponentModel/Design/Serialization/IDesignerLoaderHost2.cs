using System;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000609 RID: 1545
	public interface IDesignerLoaderHost2 : IDesignerLoaderHost, IDesignerHost, IServiceContainer, IServiceProvider
	{
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060038B9 RID: 14521
		// (set) Token: 0x060038BA RID: 14522
		bool IgnoreErrorsDuringReload { get; set; }

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060038BB RID: 14523
		// (set) Token: 0x060038BC RID: 14524
		bool CanReloadWithErrors { get; set; }
	}
}
