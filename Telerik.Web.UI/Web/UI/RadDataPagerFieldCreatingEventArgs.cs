using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001963 RID: 6499
	public class RadDataPagerFieldCreatingEventArgs : EventArgs
	{
		// Token: 0x0600FB98 RID: 64408 RVA: 0x0038B506 File Offset: 0x00389706
		public RadDataPagerFieldCreatingEventArgs(RadDataPagerField field, string fieldType)
		{
			this._field = field;
			this._fieldType = fieldType;
		}

		// Token: 0x17004C05 RID: 19461
		// (get) Token: 0x0600FB99 RID: 64409 RVA: 0x0038B527 File Offset: 0x00389727
		// (set) Token: 0x0600FB9A RID: 64410 RVA: 0x0038B52F File Offset: 0x0038972F
		public RadDataPagerField Field
		{
			get
			{
				return this._field;
			}
			set
			{
				this._field = value;
			}
		}

		// Token: 0x17004C06 RID: 19462
		// (get) Token: 0x0600FB9B RID: 64411 RVA: 0x0038B538 File Offset: 0x00389738
		public string FieldType
		{
			get
			{
				return this._fieldType;
			}
		}

		// Token: 0x04004791 RID: 18321
		private string _fieldType = "";

		// Token: 0x04004792 RID: 18322
		private RadDataPagerField _field;
	}
}
