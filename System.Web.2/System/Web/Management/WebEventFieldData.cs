using System;

namespace System.Web.Management
{
	// Token: 0x02000187 RID: 391
	internal class WebEventFieldData
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00040BAD File Offset: 0x0003EDAD
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00040BB5 File Offset: 0x0003EDB5
		public string Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x00040BBD File Offset: 0x0003EDBD
		public WebEventFieldType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00040BC5 File Offset: 0x0003EDC5
		public WebEventFieldData(string name, string data, WebEventFieldType type)
		{
			this._name = name;
			this._data = data;
			this._type = type;
		}

		// Token: 0x04001626 RID: 5670
		private string _name;

		// Token: 0x04001627 RID: 5671
		private string _data;

		// Token: 0x04001628 RID: 5672
		private WebEventFieldType _type;
	}
}
