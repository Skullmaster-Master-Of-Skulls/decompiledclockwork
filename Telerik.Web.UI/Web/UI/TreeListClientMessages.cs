using System;
using System.ComponentModel;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x0200122D RID: 4653
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListClientMessages : StateManager
	{
		// Token: 0x0600BFF2 RID: 49138 RVA: 0x002A9721 File Offset: 0x002A7921
		public TreeListClientMessages(RadTreeList owner)
		{
			this._ownerTreeList = owner;
		}

		// Token: 0x0600BFF3 RID: 49139 RVA: 0x002A9730 File Offset: 0x002A7930
		private string GetLocalizationString(TFunc<TreeListLocalizationStrings, string> extractor, string defaultValue)
		{
			if (this._ownerTreeList != null)
			{
				return extractor(this._ownerTreeList.Localization);
			}
			return defaultValue;
		}

		// Token: 0x17003DEC RID: 15852
		// (get) Token: 0x0600BFF4 RID: 49140 RVA: 0x002A9758 File Offset: 0x002A7958
		// (set) Token: 0x0600BFF5 RID: 49141 RVA: 0x002A97A8 File Offset: 0x002A79A8
		[Category("Client")]
		[DefaultValue("Drop here to reorder")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Drop here to reorder tooltip")]
		public virtual string DropHereToReorder
		{
			get
			{
				object obj = base.ViewState["_dhtr"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.DropHereToReorder, "Drop here to reorder");
			}
			set
			{
				base.ViewState["_dhtr"] = value;
			}
		}

		// Token: 0x17003DED RID: 15853
		// (get) Token: 0x0600BFF6 RID: 49142 RVA: 0x002A97C4 File Offset: 0x002A79C4
		// (set) Token: 0x0600BFF7 RID: 49143 RVA: 0x002A9814 File Offset: 0x002A7A14
		[Localizable(true)]
		[DefaultValue("Drag to reorder")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Drag to reorder tooltip")]
		public virtual string DragToReorder
		{
			get
			{
				object obj = base.ViewState["_dtgr"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.DragToReorder, "Drag to reorder");
			}
			set
			{
				base.ViewState["_dtgr"] = value;
			}
		}

		// Token: 0x17003DEE RID: 15854
		// (get) Token: 0x0600BFF8 RID: 49144 RVA: 0x002A9830 File Offset: 0x002A7A30
		// (set) Token: 0x0600BFF9 RID: 49145 RVA: 0x002A9880 File Offset: 0x002A7A80
		[NotifyParentProperty(true)]
		[DefaultValue("Drag to resize")]
		[Description("Drag to resize tooltip")]
		[Category("Client")]
		[Localizable(true)]
		public virtual string DragToResize
		{
			get
			{
				object obj = base.ViewState["_dtr"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.DragToResize, "Drag to resize");
			}
			set
			{
				base.ViewState["_dtr"] = value;
			}
		}

		// Token: 0x17003DEF RID: 15855
		// (get) Token: 0x0600BFFA RID: 49146 RVA: 0x002A989C File Offset: 0x002A7A9C
		// (set) Token: 0x0600BFFB RID: 49147 RVA: 0x002A98EC File Offset: 0x002A7AEC
		[DefaultValue("Width: <strong>{0}</strong> <em>pixels</em>")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("the format string used for the tooltip when resizing a column")]
		[Category("Client")]
		public string ColumnResizeTooltipFormatString
		{
			get
			{
				object obj = base.ViewState["ColumnResizeTooltipFormatString"];
				if (obj == null)
				{
					return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.ColumnResizeTooltipFormatString, "Width: <strong>{0}</strong> <em>pixels</em>");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ColumnResizeTooltipFormatString"] = value;
			}
		}

		// Token: 0x17003DF0 RID: 15856
		// (get) Token: 0x0600BFFC RID: 49148 RVA: 0x002A9908 File Offset: 0x002A7B08
		// (set) Token: 0x0600BFFD RID: 49149 RVA: 0x002A9958 File Offset: 0x002A7B58
		[Description("The title attribute that will be to the expand image")]
		[Category("Client")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ExpandToolTip
		{
			get
			{
				object obj = base.ViewState["ExpandToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.ExpandToolTip, string.Empty);
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ExpandToolTip"] = value;
			}
		}

		// Token: 0x17003DF1 RID: 15857
		// (get) Token: 0x0600BFFE RID: 49150 RVA: 0x002A9974 File Offset: 0x002A7B74
		// (set) Token: 0x0600BFFF RID: 49151 RVA: 0x002A99C4 File Offset: 0x002A7BC4
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Client")]
		[Localizable(true)]
		[Description("The title attribute that will be to the collapse image")]
		public string CollapseToolTip
		{
			get
			{
				object obj = base.ViewState["CollapseToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.CollapseToolTip, string.Empty);
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["CollapseToolTip"] = value;
			}
		}

		// Token: 0x0400325D RID: 12893
		private readonly RadTreeList _ownerTreeList;
	}
}
