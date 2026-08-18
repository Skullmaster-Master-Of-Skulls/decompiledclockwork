using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020003A4 RID: 932
	public class GridBindingData
	{
		// Token: 0x060022EB RID: 8939 RVA: 0x00075137 File Offset: 0x00073337
		public GridBindingData()
		{
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0007513F File Offset: 0x0007333F
		public GridBindingData(List<object> data, int count)
		{
			this._count = count;
			this._data = data;
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x00075155 File Offset: 0x00073355
		// (set) Token: 0x060022EE RID: 8942 RVA: 0x0007515D File Offset: 0x0007335D
		public int Count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x00075166 File Offset: 0x00073366
		// (set) Token: 0x060022F0 RID: 8944 RVA: 0x0007516E File Offset: 0x0007336E
		public List<object> Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x04000907 RID: 2311
		private int _count;

		// Token: 0x04000908 RID: 2312
		private List<object> _data;
	}
}
