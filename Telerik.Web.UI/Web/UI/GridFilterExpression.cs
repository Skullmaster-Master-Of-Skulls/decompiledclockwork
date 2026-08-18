using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001198 RID: 4504
	[Serializable]
	public class GridFilterExpression
	{
		// Token: 0x17003BCB RID: 15307
		// (get) Token: 0x0600B8F6 RID: 47350 RVA: 0x0028F0FB File Offset: 0x0028D2FB
		// (set) Token: 0x0600B8F7 RID: 47351 RVA: 0x0028F103 File Offset: 0x0028D303
		public string ColumnUniqueName
		{
			get
			{
				return this._columnUniqueName;
			}
			set
			{
				this._columnUniqueName = value;
			}
		}

		// Token: 0x17003BCC RID: 15308
		// (get) Token: 0x0600B8F8 RID: 47352 RVA: 0x0028F10C File Offset: 0x0028D30C
		// (set) Token: 0x0600B8F9 RID: 47353 RVA: 0x0028F114 File Offset: 0x0028D314
		public string DataTypeName
		{
			get
			{
				return this._dataTypeName;
			}
			set
			{
				this._dataTypeName = value;
			}
		}

		// Token: 0x17003BCD RID: 15309
		// (get) Token: 0x0600B8FA RID: 47354 RVA: 0x0028F11D File Offset: 0x0028D31D
		// (set) Token: 0x0600B8FB RID: 47355 RVA: 0x0028F125 File Offset: 0x0028D325
		public string FieldName
		{
			get
			{
				return this._fieldName;
			}
			set
			{
				this._fieldName = value;
			}
		}

		// Token: 0x17003BCE RID: 15310
		// (get) Token: 0x0600B8FC RID: 47356 RVA: 0x0028F12E File Offset: 0x0028D32E
		// (set) Token: 0x0600B8FD RID: 47357 RVA: 0x0028F136 File Offset: 0x0028D336
		public string FieldValue
		{
			get
			{
				return this._fieldValue;
			}
			set
			{
				this._fieldValue = value;
			}
		}

		// Token: 0x17003BCF RID: 15311
		// (get) Token: 0x0600B8FE RID: 47358 RVA: 0x0028F13F File Offset: 0x0028D33F
		// (set) Token: 0x0600B8FF RID: 47359 RVA: 0x0028F147 File Offset: 0x0028D347
		public string FilterFunction
		{
			get
			{
				return this._filterFunction;
			}
			set
			{
				this._filterFunction = value;
			}
		}

		// Token: 0x040030EB RID: 12523
		private string _columnUniqueName;

		// Token: 0x040030EC RID: 12524
		private string _dataTypeName;

		// Token: 0x040030ED RID: 12525
		private string _fieldName;

		// Token: 0x040030EE RID: 12526
		private string _fieldValue;

		// Token: 0x040030EF RID: 12527
		private string _filterFunction;
	}
}
