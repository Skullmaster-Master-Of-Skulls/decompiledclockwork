using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class DataModelErrorEventArgs : EventArgs
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004D67 File Offset: 0x00002F67
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004D6F File Offset: 0x00002F6F
		public string PropertyName { get; internal set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004D78 File Offset: 0x00002F78
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004D80 File Offset: 0x00002F80
		public string ErrorMessage { get; internal set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004D89 File Offset: 0x00002F89
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004D91 File Offset: 0x00002F91
		public MetadataItem Item
		{
			get
			{
				return this._item;
			}
			set
			{
				this._item = value;
			}
		}

		// Token: 0x04000022 RID: 34
		[NonSerialized]
		private MetadataItem _item;
	}
}
