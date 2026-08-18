using System;
using System.Diagnostics;

namespace System.ComponentModel
{
	// Token: 0x020000F9 RID: 249
	internal static class CoreSwitches
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000C51F File Offset: 0x0000A71F
		public static BooleanSwitch PerfTrack
		{
			get
			{
				if (CoreSwitches.perfTrack == null)
				{
					CoreSwitches.perfTrack = new BooleanSwitch("PERFTRACK", "Debug performance critical sections.");
				}
				return CoreSwitches.perfTrack;
			}
		}

		// Token: 0x04000433 RID: 1075
		private static BooleanSwitch perfTrack;
	}
}
