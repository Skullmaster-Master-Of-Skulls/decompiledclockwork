using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000488 RID: 1160
	public class ObjectDataSourceEventArgs : EventArgs
	{
		// Token: 0x060039A3 RID: 14755 RVA: 0x000BAE17 File Offset: 0x000B9017
		public ObjectDataSourceEventArgs(object objectInstance)
		{
			this._objectInstance = objectInstance;
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000BAE26 File Offset: 0x000B9026
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x000BAE2E File Offset: 0x000B902E
		public object ObjectInstance
		{
			get
			{
				return this._objectInstance;
			}
			set
			{
				this._objectInstance = value;
			}
		}

		// Token: 0x040022BB RID: 8891
		private object _objectInstance;
	}
}
