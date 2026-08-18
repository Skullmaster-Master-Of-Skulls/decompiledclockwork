using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000DFF RID: 3583
	[Serializable]
	internal class PivotGridModelBase<T> where T : PivotGridModelRowBase
	{
		// Token: 0x17002A0F RID: 10767
		// (get) Token: 0x06008501 RID: 34049 RVA: 0x001E6284 File Offset: 0x001E4484
		// (set) Token: 0x06008502 RID: 34050 RVA: 0x001E628C File Offset: 0x001E448C
		public List<T> Rows { get; set; }

		// Token: 0x06008503 RID: 34051 RVA: 0x001E6295 File Offset: 0x001E4495
		public PivotGridModelBase()
		{
			this.Rows = new List<T>();
		}

		// Token: 0x06008504 RID: 34052 RVA: 0x001E62A8 File Offset: 0x001E44A8
		public void Clear()
		{
			for (int i = 0; i < this.Rows.Count; i++)
			{
				T t = this.Rows[i];
				t.Cells.Clear();
			}
			this.Rows.Clear();
		}
	}
}
