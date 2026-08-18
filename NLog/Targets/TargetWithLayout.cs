using System;
using System.ComponentModel;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000146 RID: 326
	public abstract class TargetWithLayout : Target
	{
		// Token: 0x06000B8F RID: 2959 RVA: 0x0001AC0B File Offset: 0x00018E0B
		protected TargetWithLayout()
		{
			this.Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}";
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0001AC23 File Offset: 0x00018E23
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0001AC2B File Offset: 0x00018E2B
		[DefaultValue("${longdate}|${level:uppercase=true}|${logger}|${message}")]
		[RequiredParameter]
		public virtual Layout Layout { get; set; }
	}
}
