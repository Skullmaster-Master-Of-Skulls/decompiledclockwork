using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000247 RID: 583
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBPROPSET
	{
		// Token: 0x0600205B RID: 8283 RVA: 0x0027FFB8 File Offset: 0x0027F3B8
		internal tagDBPROPSET()
		{
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0027FFD8 File Offset: 0x0027F3D8
		internal tagDBPROPSET(int propertyCount, Guid propertySet)
		{
			this.cProperties = propertyCount;
			this.guidPropertySet = propertySet;
		}

		// Token: 0x040014DA RID: 5338
		internal IntPtr rgProperties;

		// Token: 0x040014DB RID: 5339
		internal int cProperties;

		// Token: 0x040014DC RID: 5340
		internal Guid guidPropertySet;
	}
}
