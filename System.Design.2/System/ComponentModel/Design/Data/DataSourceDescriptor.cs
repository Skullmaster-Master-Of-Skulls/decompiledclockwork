using System;
using System.Drawing;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x020001F6 RID: 502
	public abstract class DataSourceDescriptor
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001304 RID: 4868
		public abstract string Name { get; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001305 RID: 4869
		public abstract Bitmap Image { get; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001306 RID: 4870
		public abstract string TypeName { get; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001307 RID: 4871
		public abstract bool IsDesignable { get; }
	}
}
