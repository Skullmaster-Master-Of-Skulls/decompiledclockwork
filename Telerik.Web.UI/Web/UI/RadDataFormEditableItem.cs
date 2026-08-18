using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001F4 RID: 500
	public class RadDataFormEditableItem : RadDataFormDataItem
	{
		// Token: 0x060011A4 RID: 4516 RVA: 0x00040498 File Offset: 0x0003E698
		public RadDataFormEditableItem(RadDataForm ownerDataForm, int displayIndex) : this(ownerDataForm, displayIndex, RadDataFormItemType.EditItem)
		{
			base.DisplayIndex = displayIndex;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000404AA File Offset: 0x0003E6AA
		internal RadDataFormEditableItem(RadDataForm ownerDataForm, int displayIndex, RadDataFormItemType itemType) : base(ownerDataForm, displayIndex, itemType)
		{
			base.DisplayIndex = displayIndex;
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x000404BC File Offset: 0x0003E6BC
		public override bool IsInEditMode
		{
			get
			{
				return true;
			}
		}
	}
}
