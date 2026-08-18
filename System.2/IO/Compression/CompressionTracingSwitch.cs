using System;
using System.Diagnostics;

namespace System.IO.Compression
{
	// Token: 0x0200041A RID: 1050
	internal class CompressionTracingSwitch : Switch
	{
		// Token: 0x0600276B RID: 10091 RVA: 0x000B5C6F File Offset: 0x000B3E6F
		internal CompressionTracingSwitch(string displayName, string description) : base(displayName, description)
		{
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x000B5C79 File Offset: 0x000B3E79
		public static bool Verbose
		{
			get
			{
				return CompressionTracingSwitch.tracingSwitch.SwitchSetting >= 2;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x000B5C8B File Offset: 0x000B3E8B
		public static bool Informational
		{
			get
			{
				return CompressionTracingSwitch.tracingSwitch.SwitchSetting >= 1;
			}
		}

		// Token: 0x0400216C RID: 8556
		internal static readonly CompressionTracingSwitch tracingSwitch = new CompressionTracingSwitch("CompressionSwitch", "Compression Library Tracing Switch");
	}
}
