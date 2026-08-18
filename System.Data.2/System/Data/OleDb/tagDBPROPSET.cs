using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200026C RID: 620
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBPROPSET
	{
		// Token: 0x0600266C RID: 9836 RVA: 0x001048A4 File Offset: 0x00103CA4
		internal tagDBPROPSET()
		{
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x001048B8 File Offset: 0x00103CB8
		internal tagDBPROPSET(int propertyCount, Guid propertySet)
		{
			this.cProperties = propertyCount;
			this.guidPropertySet = propertySet;
		}

		// Token: 0x040017EC RID: 6124
		internal IntPtr rgProperties;

		// Token: 0x040017ED RID: 6125
		internal int cProperties;

		// Token: 0x040017EE RID: 6126
		internal Guid guidPropertySet;
	}
}
