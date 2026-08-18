using System;

namespace System.Web.Mvc.Async
{
	// Token: 0x02000122 RID: 290
	internal sealed class Trigger
	{
		// Token: 0x060007AA RID: 1962 RVA: 0x00014C76 File Offset: 0x00012E76
		internal Trigger(Action fireAction)
		{
			this._fireAction = fireAction;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00014C85 File Offset: 0x00012E85
		public void Fire()
		{
			this._fireAction();
		}

		// Token: 0x04000222 RID: 546
		private readonly Action _fireAction;
	}
}
