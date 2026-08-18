using System;
using System.ComponentModel;

namespace NLog.Targets
{
	// Token: 0x0200016A RID: 362
	[Target("Null")]
	public sealed class NullTarget : TargetWithLayout
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0002103D File Offset: 0x0001F23D
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x00021045 File Offset: 0x0001F245
		[DefaultValue(false)]
		public bool FormatMessage { get; set; }

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002104E File Offset: 0x0001F24E
		public NullTarget()
		{
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00021056 File Offset: 0x0001F256
		public NullTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00021065 File Offset: 0x0001F265
		protected override void Write(LogEventInfo logEvent)
		{
			if (this.FormatMessage)
			{
				this.Layout.Render(logEvent);
			}
		}
	}
}
