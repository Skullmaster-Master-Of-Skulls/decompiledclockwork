using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000494 RID: 1172
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class ConsoleTraceListener : TextWriterTraceListener
	{
		// Token: 0x06002B68 RID: 11112 RVA: 0x000C52F8 File Offset: 0x000C34F8
		public ConsoleTraceListener() : base(Console.Out)
		{
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000C5305 File Offset: 0x000C3505
		public ConsoleTraceListener(bool useErrorStream) : base(useErrorStream ? Console.Error : Console.Out)
		{
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000C531C File Offset: 0x000C351C
		public override void Close()
		{
		}
	}
}
