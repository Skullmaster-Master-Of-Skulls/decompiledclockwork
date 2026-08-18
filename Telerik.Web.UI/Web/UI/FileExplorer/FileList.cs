using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Editor.DialogControls;

namespace Telerik.Web.UI.FileExplorer
{
	// Token: 0x02000B5A RID: 2906
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class FileList
	{
		// Token: 0x170023F2 RID: 9202
		// (get) Token: 0x06006DA6 RID: 28070 RVA: 0x00197068 File Offset: 0x00195268
		// (set) Token: 0x06006DA7 RID: 28071 RVA: 0x00197070 File Offset: 0x00195270
		public string PrefixID { get; set; }

		// Token: 0x170023F3 RID: 9203
		// (get) Token: 0x06006DA8 RID: 28072 RVA: 0x00197079 File Offset: 0x00195279
		// (set) Token: 0x06006DA9 RID: 28073 RVA: 0x00197081 File Offset: 0x00195281
		public RadSlider Slider { get; set; }

		// Token: 0x170023F4 RID: 9204
		// (get) Token: 0x06006DAA RID: 28074 RVA: 0x0019708A File Offset: 0x0019528A
		// (set) Token: 0x06006DAB RID: 28075 RVA: 0x00197092 File Offset: 0x00195292
		public bool Visible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				this._isVisible = value;
				this.ProcessControlsVisibility();
			}
		}

		// Token: 0x170023F5 RID: 9205
		// (get) Token: 0x06006DAC RID: 28076 RVA: 0x001970A1 File Offset: 0x001952A1
		// (set) Token: 0x06006DAD RID: 28077 RVA: 0x001970A9 File Offset: 0x001952A9
		public bool Enabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				this._isEnabled = value;
				this.EnableDisableControls();
			}
		}

		// Token: 0x170023F6 RID: 9206
		// (get) Token: 0x06006DAE RID: 28078 RVA: 0x001970B8 File Offset: 0x001952B8
		// (set) Token: 0x06006DAF RID: 28079 RVA: 0x001970C0 File Offset: 0x001952C0
		public bool AllowPaging
		{
			get
			{
				return this._allowPaging;
			}
			set
			{
				RadListView listView = this._listView;
				this._allowPaging = value;
				listView.AllowPaging = value;
				this.ProcessGridPaging(this._allowPaging);
				this.ProcessListViewPaging();
			}
		}

		// Token: 0x170023F7 RID: 9207
		// (get) Token: 0x06006DB0 RID: 28080 RVA: 0x001970F4 File Offset: 0x001952F4
		// (set) Token: 0x06006DB1 RID: 28081 RVA: 0x001970FC File Offset: 0x001952FC
		public bool EnableFilter { get; set; }

		// Token: 0x170023F8 RID: 9208
		// (get) Token: 0x06006DB2 RID: 28082 RVA: 0x00197105 File Offset: 0x00195305
		// (set) Token: 0x06006DB3 RID: 28083 RVA: 0x00197110 File Offset: 0x00195310
		public int PageSize
		{
			get
			{
				return this._pageSize;
			}
			set
			{
				RadListView listView = this._listView;
				RadGrid grid = this._grid;
				this._pageSize = value;
				grid.PageSize = value;
				listView.PageSize = value;
			}
		}

		// Token: 0x170023F9 RID: 9209
		// (get) Token: 0x06006DB4 RID: 28084 RVA: 0x00197140 File Offset: 0x00195340
		// (set) Token: 0x06006DB5 RID: 28085 RVA: 0x00197148 File Offset: 0x00195348
		public FileExplorerMode ViewMode
		{
			get
			{
				return this._viewMode;
			}
			set
			{
				this._viewMode = value;
				this.ProcessControlsVisibility();
			}
		}

		// Token: 0x170023FA RID: 9210
		// (get) Token: 0x06006DB6 RID: 28086 RVA: 0x00197157 File Offset: 0x00195357
		// (set) Token: 0x06006DB7 RID: 28087 RVA: 0x0019715F File Offset: 0x0019535F
		public FileListControls AvailableFileListControls
		{
			get
			{
				return this._availableFileListControls;
			}
			set
			{
				this._availableFileListControls = value;
				this.ProcessControlsVisibility();
			}
		}

		// Token: 0x170023FB RID: 9211
		// (get) Token: 0x06006DB8 RID: 28088 RVA: 0x0019716E File Offset: 0x0019536E
		// (set) Token: 0x06006DB9 RID: 28089 RVA: 0x00197176 File Offset: 0x00195376
		public bool AllowMultipleItemSelect
		{
			get
			{
				return this._allowMultipleItemSelection;
			}
			set
			{
				this._allowMultipleItemSelection = value;
				this.ProcessMultipleItemSelection();
			}
		}

		// Token: 0x170023FC RID: 9212
		// (get) Token: 0x06006DBA RID: 28090 RVA: 0x00197185 File Offset: 0x00195385
		// (set) Token: 0x06006DBB RID: 28091 RVA: 0x0019718D File Offset: 0x0019538D
		public string FilterTextBoxLabel { get; set; }

		// Token: 0x170023FD RID: 9213
		// (get) Token: 0x06006DBC RID: 28092 RVA: 0x00197196 File Offset: 0x00195396
		// (set) Token: 0x06006DBD RID: 28093 RVA: 0x0019719E File Offset: 0x0019539E
		public FileExplorerConfiguration Configuration { get; set; }

		// Token: 0x170023FE RID: 9214
		// (get) Token: 0x06006DBE RID: 28094 RVA: 0x001971A7 File Offset: 0x001953A7
		// (set) Token: 0x06006DBF RID: 28095 RVA: 0x001971AF File Offset: 0x001953AF
		public DialogLocalizationStrings Localization { get; set; }

		// Token: 0x170023FF RID: 9215
		// (get) Token: 0x06006DC0 RID: 28096 RVA: 0x001971B8 File Offset: 0x001953B8
		// (set) Token: 0x06006DC1 RID: 28097 RVA: 0x001971C0 File Offset: 0x001953C0
		public bool ControlsAreCreated { get; private set; }

		// Token: 0x17002400 RID: 9216
		// (get) Token: 0x06006DC2 RID: 28098 RVA: 0x001971C9 File Offset: 0x001953C9
		// (set) Token: 0x06006DC3 RID: 28099 RVA: 0x001971D4 File Offset: 0x001953D4
		public Unit Height
		{
			get
			{
				return this._height;
			}
			set
			{
				this._height = value;
				this.Grid.Height = (this.ListViewContainer.Height = this._height);
			}
		}

		// Token: 0x17002401 RID: 9217
		// (get) Token: 0x06006DC4 RID: 28100 RVA: 0x00197207 File Offset: 0x00195407
		public RadGrid Grid
		{
			get
			{
				return this._grid;
			}
		}

		// Token: 0x17002402 RID: 9218
		// (get) Token: 0x06006DC5 RID: 28101 RVA: 0x0019720F File Offset: 0x0019540F
		public RadListView ListView
		{
			get
			{
				return this._listView;
			}
		}

		// Token: 0x17002403 RID: 9219
		// (get) Token: 0x06006DC6 RID: 28102 RVA: 0x00197217 File Offset: 0x00195417
		public Panel ListViewContainer
		{
			get
			{
				return this._listViewContainer;
			}
		}

		// Token: 0x06006DC7 RID: 28103 RVA: 0x00197220 File Offset: 0x00195420
		public WebControl GetControl()
		{
			switch (this.ViewMode)
			{
			case FileExplorerMode.Thumbnails:
				return this._listViewContainer;
			}
			return this._grid;
		}

		// Token: 0x06006DC8 RID: 28104 RVA: 0x00197257 File Offset: 0x00195457
		public void Rebind()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006DC9 RID: 28105 RVA: 0x0019725E File Offset: 0x0019545E
		internal void CreateControls()
		{
			this.CreateGridControl();
			this.CreateListView();
			this.ControlsAreCreated = true;
		}

		// Token: 0x06006DCA RID: 28106 RVA: 0x00197274 File Offset: 0x00195474
		internal void ControlsPreRender()
		{
			if (this.EnableFilter)
			{
				this._grid.MasterTableView.CommandItemTemplate = new FileList.FilterTemplate(this.FilterTextBoxLabel);
				this._grid.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
			}
			else
			{
				this._grid.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
			}
			this._filterContainer.Visible = this.EnableFilter;
			this.UpdateLocalization();
		}

		// Token: 0x06006DCB RID: 28107 RVA: 0x001972E0 File Offset: 0x001954E0
		private void CreateGridControl()
		{
			this._grid = new RadGrid();
			this._grid.ID = "grid";
			this._grid.AutoGenerateColumns = false;
			this._grid.GridLines = GridLines.None;
			this._grid.AllowSorting = true;
			GridTableView masterTableView = this._grid.MasterTableView;
			masterTableView.ClientDataKeyNames = new string[]
			{
				"Name"
			};
			masterTableView.Caption = this.Localization.GetString("FileListSummary");
			GridTemplateColumn gridTemplateColumn = new GridTemplateColumn();
			gridTemplateColumn.HeaderText = "SortByFilename";
			gridTemplateColumn.SortExpression = "Name";
			gridTemplateColumn.UniqueName = "Name";
			gridTemplateColumn.DataField = "Name";
			masterTableView.Columns.Add(gridTemplateColumn);
			GridTemplateColumn gridTemplateColumn2 = new GridTemplateColumn();
			gridTemplateColumn2.HeaderText = "SortBySize";
			gridTemplateColumn2.SortExpression = "Size";
			gridTemplateColumn2.UniqueName = "Size";
			gridTemplateColumn2.DataField = "Size";
			gridTemplateColumn2.HeaderStyle.Width = Unit.Pixel(70);
			masterTableView.Columns.Add(gridTemplateColumn2);
			GridClientSettings clientSettings = this._grid.ClientSettings;
			clientSettings.Resizing.AllowColumnResize = true;
			clientSettings.AllowKeyboardNavigation = true;
			clientSettings.KeyboardNavigationSettings.CollapseDetailTableKey = GridFocusKeys.D0;
			clientSettings.KeyboardNavigationSettings.ExpandDetailTableKey = GridFocusKeys.D0;
			clientSettings.ClientEvents.OnCommand = "function(){}";
			clientSettings.EnableAlternatingItems = false;
			clientSettings.Selecting.AllowRowSelect = true;
			clientSettings.Selecting.EnableDragToSelectRows = false;
			this._grid.AllowMultiRowSelection = true;
			clientSettings.Scrolling.UseStaticHeaders = true;
			clientSettings.Scrolling.AllowScroll = true;
			clientSettings.EnableRowHoverStyle = true;
			clientSettings.AllowRowsDragDrop = true;
			this._grid.PagerStyle.Mode = GridPagerMode.Slider;
			this._grid.EnableViewState = false;
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x001974AC File Offset: 0x001956AC
		private void CreateListView()
		{
			this._listViewContainer = new Panel
			{
				ID = "FileListThumbnailsContainer",
				CssClass = "rfeThumbnailsContainer"
			};
			Panel panel = new Panel
			{
				ID = "ListViewContainer",
				CssClass = "rfeListViewContainer"
			};
			this._listView = new RadListView
			{
				ID = "FileListThumbnailView",
				ClientDataKeyNames = new string[]
				{
					"Name"
				},
				AllowPaging = this.AllowPaging,
				AllowMultiItemSelection = true
			};
			this._listView.ClientSettings.AllowItemsDragDrop = true;
			this._listView.ClientSettings.DataBinding.ItemPlaceHolderID = this.PrefixID + "_rfeThumbnailView";
			this._listView.ClientSettings.DataBinding.LayoutTemplate = string.Format("<ul id='{0}_rfeThumbnailView' class='rfeThumbnailView'></ul>", this.PrefixID);
			this._listView.ClientSettings.DataBinding.ItemTemplate = "<li class=\"rfeThumbList rlvI\">\r\n\t\t\t\t\t<a href=\"javascript: void 0;\" class=\"rfeLink rlvDrag#= isSelected ? ' rfeSelectedLink' : ''#\" data-index=\"#= index #\" title=\"#= Name #\">\r\n\t\t\t\t\t\t<span class=\"rfeFile#= Telerik.Web.UI.FileExplorerHelper.isWebImage(item.Extension) ? ' rfeImageFile' : '' #\">\r\n\t\t\t\t\t\t# if(Telerik.Web.UI.FileExplorerHelper.isWebImage(item.Extension)) {#\r\n\t\t\t\t\t\t\t<img src=\"#= item.Url || Path #\" alt=\"#= Name #\" width=\"32\" height=\"32\" />\r\n\t\t\t\t\t\t# } else { #\r\n\t\t\t\t\t\t\t<span class=\"rfeFileIcon #= Telerik.Web.UI.FileExplorerHelper.getThumbnailCSSExtension(item) #\"></span>\r\n\t\t\t\t\t\t# } #\r\n\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t<span class=\"rfeThumbTitle\">#= Name #</span>\r\n\t\t\t\t\t</a>\r\n\t\t\t\t</li>";
			this.CreateListViewFilterControl();
			this._listViewContainer.Controls.Add(this._filterContainer);
			panel.Controls.Add(this._listView);
			this._listViewContainer.Controls.Add(panel);
			this.CreateListViewPageControl();
			this._listViewContainer.Controls.Add(this._fileListPageControlContainer);
		}

		// Token: 0x06006DCD RID: 28109 RVA: 0x00197610 File Offset: 0x00195810
		private void CreateListViewFilterControl()
		{
			string text = HttpUtility.HtmlEncode(this.FilterTextBoxLabel);
			this._filterContainer = new Panel
			{
				ID = "FileListFilterContainer",
				CssClass = "rfeFilterContainer"
			};
			TextBox textBox = new TextBox
			{
				ID = "FileListFilterTextBox",
				CssClass = "rfeFilterTxt radPreventDecorate",
				ToolTip = text,
				EnableViewState = false
			};
			Label child = new Label
			{
				ID = "FileListFilterLabel",
				CssClass = "rfeFilterLbl",
				AssociatedControlID = textBox.ID,
				Text = text,
				EnableViewState = false
			};
			this._filterContainer.Controls.Add(child);
			this._filterContainer.Controls.Add(textBox);
		}

		// Token: 0x06006DCE RID: 28110 RVA: 0x001976E0 File Offset: 0x001958E0
		private void CreateListViewPageControl()
		{
			this._fileListPageControlContainer = new Panel
			{
				ID = "FileListPageControlContainer",
				CssClass = "rfePageControlContainer"
			};
			this.Slider = new RadSlider
			{
				ID = "FileListPageControlSlider",
				CssClass = "rfePageControl"
			};
			this._fileListPageControlContainer.Controls.Add(this.Slider);
		}

		// Token: 0x06006DCF RID: 28111 RVA: 0x0019774C File Offset: 0x0019594C
		private void UpdateLocalization()
		{
			GridTableView masterTableView = this._grid.MasterTableView;
			foreach (object obj in masterTableView.Columns)
			{
				GridColumn gridColumn = (GridColumn)obj;
				string @string = this.Localization.GetString(gridColumn.HeaderText);
				if (!string.IsNullOrEmpty(@string))
				{
					gridColumn.HeaderText = @string;
				}
			}
			this._grid.PagerStyle.PagerTextFormat = this.Localization.GetString("GridPagerText");
			this._grid.SortingSettings.SortToolTip = this.Localization.GetString("GridSortToolTip");
			this._grid.SortingSettings.SortedAscToolTip = this.Localization.GetString("GridSortedAscToolTip");
			this._grid.SortingSettings.SortedDescToolTip = this.Localization.GetString("GridSortedDescToolTip");
			this._grid.Rebind();
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x0019785C File Offset: 0x00195A5C
		private void EnableDisableControls()
		{
			bool enabled = this.Enabled;
			this._grid.AllowSorting = enabled;
			GridClientSettings clientSettings = this._grid.ClientSettings;
			clientSettings.Selecting.AllowRowSelect = enabled;
			clientSettings.AllowRowsDragDrop = enabled;
			clientSettings.EnableRowHoverStyle = enabled;
			clientSettings.Resizing.AllowColumnResize = enabled;
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x001978B0 File Offset: 0x00195AB0
		private void ProcessGridPaging(bool allowPaging)
		{
			if (this._grid != null)
			{
				this._grid.AllowPaging = allowPaging;
				if (this._grid.PageSize != 10)
				{
					this.PageSize = this._grid.PageSize;
				}
				this._grid.PagerStyle.AlwaysVisible = true;
			}
		}

		// Token: 0x06006DD2 RID: 28114 RVA: 0x00197902 File Offset: 0x00195B02
		private void ProcessListViewPaging()
		{
			this._fileListPageControlContainer.Visible = this.AllowPaging;
		}

		// Token: 0x06006DD3 RID: 28115 RVA: 0x00197918 File Offset: 0x00195B18
		private void ProcessControlsVisibility()
		{
			if (this._viewMode == FileExplorerMode.FileTree)
			{
				this._isVisible = false;
			}
			if (this._grid != null)
			{
				this._grid.Visible = (this._isVisible && (this._availableFileListControls & FileListControls.Grid) != (FileListControls)0);
			}
			if (this._listView != null)
			{
				this._listViewContainer.Visible = (this._isVisible && (this._availableFileListControls & FileListControls.Thumbnails) != (FileListControls)0);
			}
		}

		// Token: 0x06006DD4 RID: 28116 RVA: 0x00197990 File Offset: 0x00195B90
		private void ProcessMultipleItemSelection()
		{
			this.ListView.AllowMultiItemSelection = (this.Grid.AllowMultiRowSelection = this._allowMultipleItemSelection);
		}

		// Token: 0x04001DA3 RID: 7587
		private bool _isEnabled;

		// Token: 0x04001DA4 RID: 7588
		private bool _isVisible;

		// Token: 0x04001DA5 RID: 7589
		private bool _allowPaging;

		// Token: 0x04001DA6 RID: 7590
		private bool _allowMultipleItemSelection;

		// Token: 0x04001DA7 RID: 7591
		private int _pageSize;

		// Token: 0x04001DA8 RID: 7592
		private Unit _height;

		// Token: 0x04001DA9 RID: 7593
		private FileExplorerMode _viewMode;

		// Token: 0x04001DAA RID: 7594
		private FileListControls _availableFileListControls;

		// Token: 0x04001DAB RID: 7595
		private RadGrid _grid;

		// Token: 0x04001DAC RID: 7596
		private RadListView _listView;

		// Token: 0x04001DAD RID: 7597
		private Panel _filterContainer;

		// Token: 0x04001DAE RID: 7598
		private Panel _listViewContainer;

		// Token: 0x04001DAF RID: 7599
		private Panel _fileListPageControlContainer;

		// Token: 0x02000B5B RID: 2907
		[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
		internal class FilterTemplate : ITemplate
		{
			// Token: 0x06006DD6 RID: 28118 RVA: 0x001979C4 File Offset: 0x00195BC4
			public FilterTemplate(string text)
			{
				string text2 = HttpUtility.HtmlEncode(text);
				this._txt = new TextBox();
				this._txt.ID = "FilterTextBox";
				this._txt.EnableViewState = false;
				this._txt.ToolTip = text2;
				this._txt.CssClass = "rfeFilterTxt radPreventDecorate";
				this._lbl = new Label();
				this._lbl.EnableViewState = false;
				this._lbl.ID = "FilterLabel";
				this._lbl.AssociatedControlID = "FilterTextBox";
				this._lbl.Text = text2;
				this._lbl.CssClass = "rfeFilterLbl";
				this._pnl = new Panel();
				this._pnl.EnableViewState = false;
				this._pnl.ID = "FilterDiv";
				this._pnl.CssClass = "rfeFilterWrapper";
			}

			// Token: 0x06006DD7 RID: 28119 RVA: 0x00197AAB File Offset: 0x00195CAB
			public void InstantiateIn(Control container)
			{
				container.Controls.Add(this._pnl);
				this._pnl.Controls.Add(this._lbl);
				this._pnl.Controls.Add(this._txt);
			}

			// Token: 0x04001DB7 RID: 7607
			private TextBox _txt;

			// Token: 0x04001DB8 RID: 7608
			private Label _lbl;

			// Token: 0x04001DB9 RID: 7609
			private Panel _pnl;
		}
	}
}
