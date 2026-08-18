using System;
using System.Drawing;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x020001F8 RID: 504
	public abstract class DataSourceGroup
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001312 RID: 4882
		public abstract string Name { get; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001313 RID: 4883
		public abstract Bitmap Image { get; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001314 RID: 4884
		public abstract DataSourceDescriptorCollection DataSources { get; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001315 RID: 4885
		public abstract bool IsDefault { get; }
	}
}
