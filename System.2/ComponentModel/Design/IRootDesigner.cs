using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x020005F5 RID: 1525
	[ComVisible(true)]
	public interface IRootDesigner : IDesigner, IDisposable
	{
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x0600384D RID: 14413
		ViewTechnology[] SupportedTechnologies { get; }

		// Token: 0x0600384E RID: 14414
		object GetView(ViewTechnology technology);
	}
}
