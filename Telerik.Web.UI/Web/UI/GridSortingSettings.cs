using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02001175 RID: 4469
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridSortingSettings : ObjectWithState
	{
		// Token: 0x0600B633 RID: 46643 RVA: 0x0028180E File Offset: 0x0027FA0E
		public GridSortingSettings(RadGrid ownerGrid, StateBag ownerStateBag) : base("gss_", ownerStateBag)
		{
			this._ownerGrid = ownerGrid;
		}

		// Token: 0x0600B634 RID: 46644 RVA: 0x00281823 File Offset: 0x0027FA23
		public GridSortingSettings(StateBag ownerStateBag) : this(null, ownerStateBag)
		{
		}

		// Token: 0x0600B635 RID: 46645 RVA: 0x0028182D File Offset: 0x0027FA2D
		private string GetLocalizationString(TFunc<GridStrings, string> extractor, string defaultValue)
		{
			if (this._ownerGrid != null)
			{
				return extractor(this._ownerGrid.Localization);
			}
			return defaultValue;
		}

		// Token: 0x17003AE9 RID: 15081
		// (get) Token: 0x0600B636 RID: 46646 RVA: 0x00281854 File Offset: 0x0027FA54
		// (set) Token: 0x0600B637 RID: 46647 RVA: 0x002818A4 File Offset: 0x0027FAA4
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Click here to sort")]
		public string SortToolTip
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_stt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.SortToolTip, "Click here to sort");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_stt"] = value;
			}
		}

		// Token: 0x17003AEA RID: 15082
		// (get) Token: 0x0600B638 RID: 46648 RVA: 0x002818C0 File Offset: 0x0027FAC0
		// (set) Token: 0x0600B639 RID: 46649 RVA: 0x00281910 File Offset: 0x0027FB10
		[NotifyParentProperty(true)]
		[DefaultValue("Sorted asc")]
		[Localizable(true)]
		public string SortedAscToolTip
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_satt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.SortedAscToolTip, "Sorted asc");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_satt"] = value;
			}
		}

		// Token: 0x17003AEB RID: 15083
		// (get) Token: 0x0600B63A RID: 46650 RVA: 0x0028192C File Offset: 0x0027FB2C
		// (set) Token: 0x0600B63B RID: 46651 RVA: 0x0028197C File Offset: 0x0027FB7C
		[NotifyParentProperty(true)]
		[DefaultValue("Sorted desc")]
		[Localizable(true)]
		public string SortedDescToolTip
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_sdtt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.SortedDescToolTip, "Sorted desc");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_sdtt"] = value;
			}
		}

		// Token: 0x17003AEC RID: 15084
		// (get) Token: 0x0600B63C RID: 46652 RVA: 0x00281990 File Offset: 0x0027FB90
		// (set) Token: 0x0600B63D RID: 46653 RVA: 0x002819C2 File Offset: 0x0027FBC2
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Color), "")]
		public virtual Color SortedBackColor
		{
			get
			{
				object obj = base.ViewState["_sbc"] ?? Color.Empty;
				return (Color)obj;
			}
			set
			{
				base.ViewState["_sbc"] = value;
			}
		}

		// Token: 0x17003AED RID: 15085
		// (get) Token: 0x0600B63E RID: 46654 RVA: 0x002819DC File Offset: 0x0027FBDC
		// (set) Token: 0x0600B63F RID: 46655 RVA: 0x00281A0A File Offset: 0x0027FC0A
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool EnableSkinSortStyles
		{
			get
			{
				object obj = base.ViewState["_esss"] ?? true;
				return (bool)obj;
			}
			set
			{
				base.ViewState["_esss"] = value;
			}
		}

		// Token: 0x04002FFF RID: 12287
		private const string _sortToolTip = "Click here to sort";

		// Token: 0x04003000 RID: 12288
		private readonly RadGrid _ownerGrid;
	}
}
