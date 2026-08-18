using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000B19 RID: 2841
	[DataContract]
	public class DropDownListItemData : ControlItemData
	{
		// Token: 0x170022C9 RID: 8905
		// (get) Token: 0x06006A4D RID: 27213 RVA: 0x0018EAD5 File Offset: 0x0018CCD5
		// (set) Token: 0x06006A4E RID: 27214 RVA: 0x0018EADD File Offset: 0x0018CCDD
		[DataMember]
		public bool Selected
		{
			get
			{
				return this._selected;
			}
			set
			{
				this._selected = value;
			}
		}

		// Token: 0x06006A4F RID: 27215 RVA: 0x0018EAE6 File Offset: 0x0018CCE6
		public DropDownListItemData()
		{
			this._selected = false;
		}

		// Token: 0x04001CC4 RID: 7364
		private bool _selected;
	}
}
