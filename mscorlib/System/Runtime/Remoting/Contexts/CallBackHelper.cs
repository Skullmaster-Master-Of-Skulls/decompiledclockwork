using System;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020006C6 RID: 1734
	[Serializable]
	internal class CallBackHelper
	{
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x000D6F41 File Offset: 0x000D5F41
		// (set) Token: 0x06003EAA RID: 16042 RVA: 0x000D6F4E File Offset: 0x000D5F4E
		internal bool IsEERequested
		{
			get
			{
				return (this._flags & 1) == 1;
			}
			set
			{
				if (value)
				{
					this._flags |= 1;
				}
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (set) Token: 0x06003EAB RID: 16043 RVA: 0x000D6F61 File Offset: 0x000D5F61
		internal bool IsCrossDomain
		{
			set
			{
				if (value)
				{
					this._flags |= 256;
				}
			}
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x000D6F78 File Offset: 0x000D5F78
		internal CallBackHelper(IntPtr privateData, bool bFromEE, int targetDomainID)
		{
			this.IsEERequested = bFromEE;
			this.IsCrossDomain = (targetDomainID != 0);
			this._privateData = privateData;
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x000D6F9B File Offset: 0x000D5F9B
		internal void Func()
		{
			if (this.IsEERequested)
			{
				Context.ExecuteCallBackInEE(this._privateData);
			}
		}

		// Token: 0x04001FE3 RID: 8163
		internal const int RequestedFromEE = 1;

		// Token: 0x04001FE4 RID: 8164
		internal const int XDomainTransition = 256;

		// Token: 0x04001FE5 RID: 8165
		private int _flags;

		// Token: 0x04001FE6 RID: 8166
		private IntPtr _privateData;
	}
}
