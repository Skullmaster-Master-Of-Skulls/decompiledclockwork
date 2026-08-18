using System;
using System.ComponentModel;

namespace AutoMapper.Internal
{
	// Token: 0x020000B7 RID: 183
	public abstract class ProxyBase
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x000056BE File Offset: 0x000038BE
		public ProxyBase()
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00014405 File Offset: 0x00012605
		protected void NotifyPropertyChanged(PropertyChangedEventHandler handler, string method)
		{
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(method));
			}
		}
	}
}
