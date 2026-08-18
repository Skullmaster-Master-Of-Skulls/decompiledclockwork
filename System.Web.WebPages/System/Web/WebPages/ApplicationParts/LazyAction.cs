using System;

namespace System.Web.WebPages.ApplicationParts
{
	// Token: 0x0200000D RID: 13
	internal class LazyAction
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002D9C File Offset: 0x00000F9C
		public LazyAction(Action action)
		{
			this._lazyAction = new Lazy<object>(delegate()
			{
				action();
				return null;
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DDA File Offset: 0x00000FDA
		public object EnsurePerformed()
		{
			return this._lazyAction.Value;
		}

		// Token: 0x04000018 RID: 24
		private Lazy<object> _lazyAction;
	}
}
