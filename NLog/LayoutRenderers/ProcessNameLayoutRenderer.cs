using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E8 RID: 232
	[ThreadAgnostic]
	[LayoutRenderer("processname")]
	[AppDomainFixedOutput]
	public class ProcessNameLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0000EE83 File Offset: 0x0000D083
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0000EE8B File Offset: 0x0000D08B
		[DefaultValue(false)]
		public bool FullName { get; set; }

		// Token: 0x060006AA RID: 1706 RVA: 0x0000EE94 File Offset: 0x0000D094
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this.FullName)
			{
				builder.Append(ThreadIDHelper.Instance.CurrentProcessName);
				return;
			}
			builder.Append(ThreadIDHelper.Instance.CurrentProcessBaseName);
		}
	}
}
