using System;

namespace System.Web.UI
{
	// Token: 0x02000079 RID: 121
	public class ScriptReferenceEventArgs : EventArgs
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x00018024 File Offset: 0x00016224
		public ScriptReferenceEventArgs(ScriptReference script)
		{
			if (script == null)
			{
				throw new ArgumentNullException("script");
			}
			this._script = script;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00018041 File Offset: 0x00016241
		public ScriptReference Script
		{
			get
			{
				return this._script;
			}
		}

		// Token: 0x040001D9 RID: 473
		private readonly ScriptReference _script;
	}
}
