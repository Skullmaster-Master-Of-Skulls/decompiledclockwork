using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000750 RID: 1872
	[TypeConverter(typeof(StringToObjectConverter))]
	public class ObjectWrapper
	{
		// Token: 0x0600424D RID: 16973 RVA: 0x000D0302 File Offset: 0x000CE502
		public ObjectWrapper()
		{
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x000D030A File Offset: 0x000CE50A
		public ObjectWrapper(string value)
		{
			this.Value = value;
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x000D0319 File Offset: 0x000CE519
		// (set) Token: 0x06004250 RID: 16976 RVA: 0x000D0321 File Offset: 0x000CE521
		public object Value { get; set; }
	}
}
