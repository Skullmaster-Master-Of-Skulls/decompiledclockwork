using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000383 RID: 899
	public class BulletedListEventArgs : EventArgs
	{
		// Token: 0x060029F1 RID: 10737 RVA: 0x00087CC9 File Offset: 0x00085EC9
		public BulletedListEventArgs(int index)
		{
			this._index = index;
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x00087CD8 File Offset: 0x00085ED8
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04001E84 RID: 7812
		private int _index;
	}
}
