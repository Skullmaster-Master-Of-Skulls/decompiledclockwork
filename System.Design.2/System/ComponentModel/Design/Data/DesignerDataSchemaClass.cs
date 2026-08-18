using System;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x020001FF RID: 511
	public sealed class DesignerDataSchemaClass
	{
		// Token: 0x06001343 RID: 4931 RVA: 0x0000362F File Offset: 0x0000182F
		private DesignerDataSchemaClass()
		{
		}

		// Token: 0x04000A6B RID: 2667
		public static readonly DesignerDataSchemaClass StoredProcedures = new DesignerDataSchemaClass();

		// Token: 0x04000A6C RID: 2668
		public static readonly DesignerDataSchemaClass Tables = new DesignerDataSchemaClass();

		// Token: 0x04000A6D RID: 2669
		public static readonly DesignerDataSchemaClass Views = new DesignerDataSchemaClass();
	}
}
