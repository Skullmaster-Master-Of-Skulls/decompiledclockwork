using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020006A3 RID: 1699
	internal class RemotePropertyHolderAttribute : IContextAttribute
	{
		// Token: 0x06003D6B RID: 15723 RVA: 0x000D2261 File Offset: 0x000D1261
		internal RemotePropertyHolderAttribute(IList cp)
		{
			this._cp = cp;
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x000D2270 File Offset: 0x000D1270
		[ComVisible(true)]
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return false;
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x000D2274 File Offset: 0x000D1274
		[ComVisible(true)]
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			for (int i = 0; i < this._cp.Count; i++)
			{
				ctorMsg.ContextProperties.Add(this._cp[i]);
			}
		}

		// Token: 0x04001F6C RID: 8044
		private IList _cp;
	}
}
