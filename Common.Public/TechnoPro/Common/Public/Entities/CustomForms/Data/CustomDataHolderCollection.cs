using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data
{
	// Token: 0x02000424 RID: 1060
	public class CustomDataHolderCollection
	{
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x00024806 File Offset: 0x00022A06
		// (set) Token: 0x0600203B RID: 8251 RVA: 0x0002480E File Offset: 0x00022A0E
		public IList<CustomDataHolder> Datas { get; set; }

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x00024818 File Offset: 0x00022A18
		// (set) Token: 0x0600203D RID: 8253 RVA: 0x0002483C File Offset: 0x00022A3C
		public CustomDataHolder Data
		{
			get
			{
				IList<CustomDataHolder> datas = this.Datas;
				return (datas != null) ? datas.FirstOrDefault<CustomDataHolder>() : null;
			}
			set
			{
				this.Datas = new CustomDataHolder[]
				{
					value
				}.ToList<CustomDataHolder>();
			}
		}
	}
}
