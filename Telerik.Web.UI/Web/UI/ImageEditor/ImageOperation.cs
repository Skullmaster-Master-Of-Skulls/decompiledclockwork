using System;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x0200051A RID: 1306
	public class ImageOperation
	{
		// Token: 0x06002EAD RID: 11949 RVA: 0x00098B40 File Offset: 0x00096D40
		public ImageOperation() : this(-1)
		{
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x00098B49 File Offset: 0x00096D49
		public ImageOperation(int index)
		{
			this._index = index;
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06002EAF RID: 11951 RVA: 0x00098B5F File Offset: 0x00096D5F
		// (set) Token: 0x06002EB0 RID: 11952 RVA: 0x00098B67 File Offset: 0x00096D67
		public virtual int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x04000C45 RID: 3141
		private int _index = -1;
	}
}
