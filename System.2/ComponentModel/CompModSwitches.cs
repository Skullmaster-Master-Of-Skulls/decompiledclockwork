using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200052A RID: 1322
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class CompModSwitches
	{
		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000E0A11 File Offset: 0x000DEC11
		public static BooleanSwitch CommonDesignerServices
		{
			get
			{
				if (CompModSwitches.commonDesignerServices == null)
				{
					CompModSwitches.commonDesignerServices = new BooleanSwitch("CommonDesignerServices", "Assert if any common designer service is not found.");
				}
				return CompModSwitches.commonDesignerServices;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000E0A39 File Offset: 0x000DEC39
		public static TraceSwitch EventLog
		{
			get
			{
				if (CompModSwitches.eventLog == null)
				{
					CompModSwitches.eventLog = new TraceSwitch("EventLog", "Enable tracing for the EventLog component.");
				}
				return CompModSwitches.eventLog;
			}
		}

		// Token: 0x0400295F RID: 10591
		private static volatile BooleanSwitch commonDesignerServices;

		// Token: 0x04002960 RID: 10592
		private static volatile TraceSwitch eventLog;
	}
}
