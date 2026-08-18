using System;

namespace System.Web
{
	// Token: 0x02000062 RID: 98
	internal class UseLastUnhandledErrorFormatter : UnhandledErrorFormatter
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x00009727 File Offset: 0x00007927
		internal UseLastUnhandledErrorFormatter(Exception e) : base(e)
		{
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0000A62D File Offset: 0x0000882D
		internal override void PrepareFormatter()
		{
			base.PrepareFormatter();
			this._initialException = this.Exception;
		}
	}
}
