using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000486 RID: 1158
	public class ObjectDataSourceDisposingEventArgs : CancelEventArgs
	{
		// Token: 0x0600399D RID: 14749 RVA: 0x000BAE00 File Offset: 0x000B9000
		public ObjectDataSourceDisposingEventArgs(object objectInstance)
		{
			this._objectInstance = objectInstance;
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000BAE0F File Offset: 0x000B900F
		public object ObjectInstance
		{
			get
			{
				return this._objectInstance;
			}
		}

		// Token: 0x040022BA RID: 8890
		private object _objectInstance;
	}
}
