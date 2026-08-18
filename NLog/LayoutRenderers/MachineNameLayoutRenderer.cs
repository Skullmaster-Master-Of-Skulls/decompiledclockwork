using System;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DD RID: 221
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[LayoutRenderer("machinename")]
	public class MachineNameLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0000E91B File Offset: 0x0000CB1B
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0000E923 File Offset: 0x0000CB23
		internal string MachineName { get; private set; }

		// Token: 0x06000670 RID: 1648 RVA: 0x0000E92C File Offset: 0x0000CB2C
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			try
			{
				this.MachineName = Environment.MachineName;
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error getting machine name.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
				this.MachineName = string.Empty;
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0000E980 File Offset: 0x0000CB80
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.MachineName);
		}
	}
}
