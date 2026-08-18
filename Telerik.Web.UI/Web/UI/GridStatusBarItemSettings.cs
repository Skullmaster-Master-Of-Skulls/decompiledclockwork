using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02001177 RID: 4471
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridStatusBarItemSettings : ObjectWithState
	{
		// Token: 0x0600B651 RID: 46673 RVA: 0x00281CBA File Offset: 0x0027FEBA
		public GridStatusBarItemSettings(StateBag ownerStateBag, RadGrid grid) : base("gsbis_", ownerStateBag)
		{
			this.grid = grid;
		}

		// Token: 0x0600B652 RID: 46674 RVA: 0x00281CCF File Offset: 0x0027FECF
		private string GetLocalizationString(TFunc<GridStrings, string> extractor, string defaultValue)
		{
			if (this.grid != null)
			{
				return extractor(this.grid.Localization);
			}
			return defaultValue;
		}

		// Token: 0x17003AF0 RID: 15088
		// (get) Token: 0x0600B653 RID: 46675 RVA: 0x00281CEC File Offset: 0x0027FEEC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string StatusLabelID
		{
			get
			{
				string result = "";
				if (this.grid.ShowStatusBar)
				{
					GridItem[] items = this.grid.MasterTableView.GetItems(new GridItemType[]
					{
						GridItemType.Pager
					});
					if (items.Length > 0)
					{
						GridPagerItem gridPagerItem = (GridPagerItem)items[1];
						if (gridPagerItem != null)
						{
							Panel panel = gridPagerItem.FindControl("StatusPanel") as Panel;
							if (panel != null)
							{
								result = panel.ClientID;
							}
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17003AF1 RID: 15089
		// (get) Token: 0x0600B654 RID: 46676 RVA: 0x00281D64 File Offset: 0x0027FF64
		// (set) Token: 0x0600B655 RID: 46677 RVA: 0x00281DB4 File Offset: 0x0027FFB4
		[Description("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Ready")]
		public virtual string ReadyText
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_rdt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.StatusReadyText, "Ready");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_rdt"] = value;
			}
		}

		// Token: 0x17003AF2 RID: 15090
		// (get) Token: 0x0600B656 RID: 46678 RVA: 0x00281DD0 File Offset: 0x0027FFD0
		// (set) Token: 0x0600B657 RID: 46679 RVA: 0x00281E20 File Offset: 0x00280020
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("")]
		[DefaultValue("Loading...")]
		public virtual string LoadingText
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_lt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.LoadingText, "Loading...");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_lt"] = value;
			}
		}

		// Token: 0x04003007 RID: 12295
		internal RadGrid grid;
	}
}
