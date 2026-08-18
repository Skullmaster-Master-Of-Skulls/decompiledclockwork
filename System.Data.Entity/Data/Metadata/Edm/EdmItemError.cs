using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001AC RID: 428
	internal class EdmItemError : EdmError
	{
		// Token: 0x06001EB2 RID: 7858 RVA: 0x0006C734 File Offset: 0x0006A934
		public EdmItemError(string message, MetadataItem item) : base(message)
		{
			this._item = item;
		}

		// Token: 0x04000CDF RID: 3295
		private MetadataItem _item;
	}
}
