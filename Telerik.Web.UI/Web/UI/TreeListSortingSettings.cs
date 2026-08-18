using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001298 RID: 4760
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListSortingSettings : StateManager
	{
		// Token: 0x0600C654 RID: 50772 RVA: 0x002C3E44 File Offset: 0x002C2044
		public TreeListSortingSettings(RadTreeList ownerList)
		{
			this._ownerList = ownerList;
		}

		// Token: 0x1700400B RID: 16395
		// (get) Token: 0x0600C655 RID: 50773 RVA: 0x002C3E54 File Offset: 0x002C2054
		// (set) Token: 0x0600C656 RID: 50774 RVA: 0x002C3E8C File Offset: 0x002C208C
		[NotifyParentProperty(true)]
		[DefaultValue("Click here to sort")]
		[Localizable(true)]
		public string SortToolTip
		{
			get
			{
				object obj = base.ViewState["SortToolTip"] ?? this._ownerList.Localization.SortToolTip;
				return (string)obj;
			}
			set
			{
				base.ViewState["SortToolTip"] = value;
			}
		}

		// Token: 0x1700400C RID: 16396
		// (get) Token: 0x0600C657 RID: 50775 RVA: 0x002C3EA0 File Offset: 0x002C20A0
		// (set) Token: 0x0600C658 RID: 50776 RVA: 0x002C3ED8 File Offset: 0x002C20D8
		[DefaultValue("Sorted asc")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string SortedAscToolTip
		{
			get
			{
				object obj = base.ViewState["SortedAscToolTip"] ?? this._ownerList.Localization.SortedAscToolTip;
				return (string)obj;
			}
			set
			{
				base.ViewState["SortedAscToolTip"] = value;
			}
		}

		// Token: 0x1700400D RID: 16397
		// (get) Token: 0x0600C659 RID: 50777 RVA: 0x002C3EEC File Offset: 0x002C20EC
		// (set) Token: 0x0600C65A RID: 50778 RVA: 0x002C3F24 File Offset: 0x002C2124
		[NotifyParentProperty(true)]
		[DefaultValue("Sorted desc")]
		[Localizable(true)]
		public string SortedDescToolTip
		{
			get
			{
				object obj = base.ViewState["SortedDescToolTip"] ?? this._ownerList.Localization.SortedDescToolTip;
				return (string)obj;
			}
			set
			{
				base.ViewState["SortedDescToolTip"] = value;
			}
		}

		// Token: 0x0400346E RID: 13422
		private readonly RadTreeList _ownerList;
	}
}
