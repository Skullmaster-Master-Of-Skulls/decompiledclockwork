using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000C2 RID: 194
	public class QueryableDataSourceEditData
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00024435 File Offset: 0x00022635
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x0002443D File Offset: 0x0002263D
		public object NewDataObject
		{
			get
			{
				return this._newDataObject;
			}
			set
			{
				this._newDataObject = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00024446 File Offset: 0x00022646
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x0002444E File Offset: 0x0002264E
		public object OriginalDataObject
		{
			get
			{
				return this._originalDataObject;
			}
			set
			{
				this._originalDataObject = value;
			}
		}

		// Token: 0x04000314 RID: 788
		private object _newDataObject;

		// Token: 0x04000315 RID: 789
		private object _originalDataObject;
	}
}
