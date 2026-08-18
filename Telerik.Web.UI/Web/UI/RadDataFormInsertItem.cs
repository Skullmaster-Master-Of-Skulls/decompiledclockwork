using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001F6 RID: 502
	public class RadDataFormInsertItem : RadDataFormEditableItem, IRadDataFormInsertItem
	{
		// Token: 0x060011A7 RID: 4519 RVA: 0x000404BF File Offset: 0x0003E6BF
		public RadDataFormInsertItem(RadDataForm ownerDataForm, int displayIndex) : this(ownerDataForm, displayIndex, RadDataFormItemType.InsertItem)
		{
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000404CA File Offset: 0x0003E6CA
		internal RadDataFormInsertItem(RadDataForm ownerDataForm, int displayIndex, RadDataFormItemType itemType) : base(ownerDataForm, displayIndex, itemType)
		{
		}
	}
}
