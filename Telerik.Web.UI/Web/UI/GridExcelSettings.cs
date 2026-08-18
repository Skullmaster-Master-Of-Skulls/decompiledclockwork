using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Export;

namespace Telerik.Web.UI
{
	// Token: 0x02001B32 RID: 6962
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridExcelSettings : ObjectWithState
	{
		// Token: 0x06010D8F RID: 69007 RVA: 0x003BC759 File Offset: 0x003BA959
		public GridExcelSettings(StateBag OwnerStateBag) : base("gxlss_", OwnerStateBag)
		{
		}

		// Token: 0x1700521F RID: 21023
		// (get) Token: 0x06010D90 RID: 69008 RVA: 0x003BC767 File Offset: 0x003BA967
		// (set) Token: 0x06010D91 RID: 69009 RVA: 0x003BC792 File Offset: 0x003BA992
		[DefaultValue(GridExcelExportFormat.Html)]
		[NotifyParentProperty(true)]
		[Description("")]
		public GridExcelExportFormat Format
		{
			get
			{
				if (base.ViewState["_fm"] == null)
				{
					return GridExcelExportFormat.Html;
				}
				return (GridExcelExportFormat)base.ViewState["_fm"];
			}
			set
			{
				base.ViewState["_fm"] = value;
			}
		}

		// Token: 0x17005220 RID: 21024
		// (get) Token: 0x06010D92 RID: 69010 RVA: 0x003BC7AA File Offset: 0x003BA9AA
		// (set) Token: 0x06010D93 RID: 69011 RVA: 0x003BC7D9 File Offset: 0x003BA9D9
		[NotifyParentProperty(true)]
		[DefaultValue("xls")]
		[Description("Gets or sets the file extension for RadGrid Excel export.")]
		public string FileExtension
		{
			get
			{
				if (base.ViewState["_fe"] == null)
				{
					return "xls";
				}
				return (string)base.ViewState["_fe"];
			}
			set
			{
				base.ViewState["_fe"] = value;
			}
		}

		// Token: 0x17005221 RID: 21025
		// (get) Token: 0x06010D94 RID: 69012 RVA: 0x003BC7EC File Offset: 0x003BA9EC
		// (set) Token: 0x06010D95 RID: 69013 RVA: 0x003BC817 File Offset: 0x003BAA17
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Determines whether RadGrid will fit the image within its cell boundaries or will leave it with the default dimensions (false).")]
		public bool AutoFitImages
		{
			get
			{
				return base.ViewState["AutoFitImages"] != null && (bool)base.ViewState["AutoFitImages"];
			}
			set
			{
				base.ViewState["AutoFitImages"] = value;
			}
		}

		// Token: 0x17005222 RID: 21026
		// (get) Token: 0x06010D96 RID: 69014 RVA: 0x003BC82F File Offset: 0x003BAA2F
		// (set) Token: 0x06010D97 RID: 69015 RVA: 0x003BC85A File Offset: 0x003BAA5A
		[NotifyParentProperty(true)]
		[DefaultValue(ExportAutoFitWidthMode.Disabled)]
		[Description("Determines whether RadGrid will auto-fit the columns' width based on the length of the content. XLSX format only.")]
		public ExportAutoFitWidthMode AutoFitColumnWidth
		{
			get
			{
				if (base.ViewState["AutoFitColumnWidth"] == null)
				{
					return ExportAutoFitWidthMode.Disabled;
				}
				return (ExportAutoFitWidthMode)base.ViewState["AutoFitColumnWidth"];
			}
			set
			{
				base.ViewState["AutoFitColumnWidth"] = value;
			}
		}

		// Token: 0x17005223 RID: 21027
		// (get) Token: 0x06010D98 RID: 69016 RVA: 0x003BC872 File Offset: 0x003BAA72
		// (set) Token: 0x06010D99 RID: 69017 RVA: 0x003BC89D File Offset: 0x003BAA9D
		[DefaultValue(HorizontalAlign.NotSet)]
		[NotifyParentProperty(true)]
		[Description("Determines the default header cell alignment when exporting to Excel.")]
		public HorizontalAlign DefaultCellAlignment
		{
			get
			{
				if (base.ViewState["DefaultCellAlignment"] == null)
				{
					return HorizontalAlign.NotSet;
				}
				return (HorizontalAlign)base.ViewState["DefaultCellAlignment"];
			}
			set
			{
				base.ViewState["DefaultCellAlignment"] = value;
			}
		}

		// Token: 0x17005224 RID: 21028
		// (get) Token: 0x06010D9A RID: 69018 RVA: 0x003BC8B5 File Offset: 0x003BAAB5
		// (set) Token: 0x06010D9B RID: 69019 RVA: 0x003BC8E4 File Offset: 0x003BAAE4
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("")]
		public string WorksheetName
		{
			get
			{
				if (base.ViewState["_wsn"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["_wsn"];
			}
			set
			{
				base.ViewState["_wsn"] = value;
			}
		}
	}
}
