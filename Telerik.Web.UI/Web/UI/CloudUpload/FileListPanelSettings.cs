using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x02000131 RID: 305
	public class FileListPanelSettings : ObjectWithState
	{
		// Token: 0x06000C9F RID: 3231 RVA: 0x0002D7DC File Offset: 0x0002B9DC
		internal FileListPanelSettings(StateBag ownerViewState) : base("FileListPanelSettings", ownerViewState)
		{
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0002D7EA File Offset: 0x0002B9EA
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x0002D814 File Offset: 0x0002BA14
		[TypeConverter(typeof(UnitConverter))]
		[DefaultValue(typeof(Unit), "420px")]
		[Description("The width of the file list panel")]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Parse("420px"));
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002D82C File Offset: 0x0002BA2C
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0002D851 File Offset: 0x0002BA51
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		[Description("The height of the file list panel")]
		public Unit Height
		{
			get
			{
				return (Unit)(base.ViewState["Height"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0002D869 File Offset: 0x0002BA69
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0002D88E File Offset: 0x0002BA8E
		[TypeConverter(typeof(UnitConverter))]
		[Description("The maximum height the file list")]
		[DefaultValue(typeof(Unit), "")]
		public Unit MaxHeight
		{
			get
			{
				return (Unit)(base.ViewState["MaxHeight"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["MaxHeight"] = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0002D8A6 File Offset: 0x0002BAA6
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0002D8C7 File Offset: 0x0002BAC7
		[DefaultValue(false)]
		[Description("When set to true enables rendering of text on the buttons")]
		public bool RenderButtonText
		{
			get
			{
				return (bool)(base.ViewState["RenderButtonText"] ?? false);
			}
			set
			{
				base.ViewState["RenderButtonText"] = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0002D8DF File Offset: 0x0002BADF
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0002D8FF File Offset: 0x0002BAFF
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Behavior")]
		[Description("Gets or sets the zone, where the file list panel will be displayed.")]
		[Bindable(true)]
		public string PanelContainerSelector
		{
			get
			{
				return ((string)base.ViewState["PanelContainerSelector"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["PanelContainerSelector"] = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0002D914 File Offset: 0x0002BB14
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0002D949 File Offset: 0x0002BB49
		[Description("Gets or sets the whether the file list panel will be displayed when no files are uploaded.")]
		[Bindable(true)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.Attribute)]
		public bool ShowEmptyFileListPanel
		{
			get
			{
				return ((bool?)base.ViewState["ShowEmptyFileListPanel"]) ?? false;
			}
			set
			{
				base.ViewState["ShowEmptyFileListPanel"] = value;
			}
		}
	}
}
