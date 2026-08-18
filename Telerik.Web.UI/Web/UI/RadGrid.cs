using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Analytics;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.Functions;
using Telerik.Web.UI.GridExcelBuilder;

namespace Telerik.Web.UI
{
	// Token: 0x02000397 RID: 919
	[RequiredScript(typeof(MaterialRipple))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadGrid))]
	[AdaptiveRendering]
	[Designer("Telerik.Web.Design.RadGridDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[Description("Telerik RadGrid")]
	[TelerikToolboxCategory("Data")]
	[ToolboxBitmap(typeof(RadGrid), "Telerik.Web.UI.Grid.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ControlValueProperty("SelectedValue")]
	[ClientScriptResource("Telerik.Web.UI.RadGrid", "Telerik.Web.UI.Grid.RadGridScripts.js")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadGrid))]
	[EmbeddedSkin("Grid")]
	[EmbeddedSkin("Grid", "Default")]
	[DefaultProperty("")]
	[DefaultEvent("NeedDataSource")]
	[ToolboxData("<{0}:RadGrid runat=server></{0}:RadGrid>")]
	[LightweightRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadGrid))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadGrid))]
	public class RadGrid : GridBaseDataList, INamingContainer, IPostBackDataHandler, IPostBackEventHandler, IRadFilterableContainer, ILocalizableControl, ICallbackEventHandler
	{
		// Token: 0x06001FA3 RID: 8099 RVA: 0x00064159 File Offset: 0x00062359
		public static HtmlGenericControl CreateButton(string name, bool display = true)
		{
			return RadGrid.CreateButton(name, name, display);
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00064164 File Offset: 0x00062364
		public static HtmlGenericControl CreateButton(string name, string text, bool display = true)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("button");
			htmlGenericControl.Attributes.Add("title", text);
			htmlGenericControl.Attributes.Add("onclick", "return false;");
			htmlGenericControl.Attributes.Add("class", string.Format("t-button rgActionButton rg{0}", name));
			if (!display)
			{
				htmlGenericControl.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
			htmlGenericControl2.Attributes.Add("class", "t-font-icon rgIcon rg" + name + "Icon");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("span");
			htmlGenericControl3.Attributes.Add("class", "t-text rgButtonText");
			htmlGenericControl3.InnerText = text;
			htmlGenericControl.Controls.Add(htmlGenericControl3);
			return htmlGenericControl;
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06001FA5 RID: 8101 RVA: 0x0006423D File Offset: 0x0006243D
		// (remove) Token: 0x06001FA6 RID: 8102 RVA: 0x00064250 File Offset: 0x00062450
		[Category("Action")]
		[Description("Fires when batch edit operation is made.")]
		public event GridBatchEditEventHandler BatchEditCommand
		{
			add
			{
				base.Events.AddHandler("BatchEdit", value);
			}
			remove
			{
				base.Events.RemoveHandler("BatchEdit", value);
			}
		}

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06001FA7 RID: 8103 RVA: 0x00064263 File Offset: 0x00062463
		// (remove) Token: 0x06001FA8 RID: 8104 RVA: 0x00064276 File Offset: 0x00062476
		[Category("Action")]
		[Description("Fires when \"Cancel\" command bubbles")]
		public event GridCommandEventHandler CancelCommand
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventCancelCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventCancelCommand, value);
			}
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06001FA9 RID: 8105 RVA: 0x00064289 File Offset: 0x00062489
		// (remove) Token: 0x06001FAA RID: 8106 RVA: 0x0006429C File Offset: 0x0006249C
		[Category("Action")]
		[Description("")]
		public event GridDragDropEventHandler RowDrop
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventRowDrop, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventRowDrop, value);
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06001FAB RID: 8107 RVA: 0x000642AF File Offset: 0x000624AF
		// (remove) Token: 0x06001FAC RID: 8108 RVA: 0x000642C2 File Offset: 0x000624C2
		[Description("Fires when each editable column creates its column editor")]
		[Category("Action")]
		public event GridCreateColumnEditorEventHandler CreateColumnEditor
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventCreateColumnEditor, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventCreateColumnEditor, value);
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06001FAD RID: 8109 RVA: 0x000642D5 File Offset: 0x000624D5
		// (remove) Token: 0x06001FAE RID: 8110 RVA: 0x000642E8 File Offset: 0x000624E8
		[Description("Fires when the grid is about to be bound and the data source must be assigned")]
		[Category("Action")]
		public event GridNeedDataSourceEventHandler NeedDataSource
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventNeedDataSource, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventNeedDataSource, value);
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06001FAF RID: 8111 RVA: 0x000642FB File Offset: 0x000624FB
		// (remove) Token: 0x06001FB0 RID: 8112 RVA: 0x0006430E File Offset: 0x0006250E
		[Description("Fires when the ListBox inside the filter item require DataSource. In this event you can add items to the ListBox using its Items collection. Or you can assign DataSource and call DataBind() for the ListBox.")]
		[Category("Action")]
		public event GridFilterCheckListItemsRequestedEventHandler FilterCheckListItemsRequested
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventFilterCheckListItemsRequested, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventFilterCheckListItemsRequested, value);
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06001FB1 RID: 8113 RVA: 0x00064321 File Offset: 0x00062521
		// (remove) Token: 0x06001FB2 RID: 8114 RVA: 0x00064334 File Offset: 0x00062534
		[Description("Fires when various item events occur - for example, before Pager item is initialized")]
		[Category("Action")]
		public event GridItemEventHandler ItemEvent
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventItemEvent, value);
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06001FB3 RID: 8115 RVA: 0x00064347 File Offset: 0x00062547
		// (remove) Token: 0x06001FB4 RID: 8116 RVA: 0x0006435A File Offset: 0x0006255A
		[Description("Fires when a detail-table in the hierarchy needs data to bound")]
		[Category("Action")]
		public event GridDetailTableDataBindEventHandler DetailTableDataBind
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventDetailTableDataBind, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventDetailTableDataBind, value);
			}
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06001FB5 RID: 8117 RVA: 0x0006436D File Offset: 0x0006256D
		// (remove) Token: 0x06001FB6 RID: 8118 RVA: 0x00064380 File Offset: 0x00062580
		[Description("Fires when \"Delete\" command bubbles")]
		[Category("Action")]
		public event GridCommandEventHandler DeleteCommand
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventDeleteCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventDeleteCommand, value);
			}
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06001FB7 RID: 8119 RVA: 0x00064393 File Offset: 0x00062593
		// (remove) Token: 0x06001FB8 RID: 8120 RVA: 0x000643A6 File Offset: 0x000625A6
		[Category("Action")]
		[Description("Fires when \"Edit\" command bubbles ")]
		public event GridCommandEventHandler EditCommand
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventEditCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventEditCommand, value);
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06001FB9 RID: 8121 RVA: 0x000643B9 File Offset: 0x000625B9
		// (remove) Token: 0x06001FBA RID: 8122 RVA: 0x000643CC File Offset: 0x000625CC
		[Description("Fires when any command bubbles from within a grid item")]
		[Category("Action")]
		public event GridCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventItemCommand, value);
			}
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06001FBB RID: 8123 RVA: 0x000643DF File Offset: 0x000625DF
		// (remove) Token: 0x06001FBC RID: 8124 RVA: 0x000643F2 File Offset: 0x000625F2
		[Category("Behavior")]
		[Description("Fires when an item is created, just before that item has been initialized. ")]
		public event GridItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventItemCreated, value);
			}
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06001FBD RID: 8125 RVA: 0x00064405 File Offset: 0x00062605
		// (remove) Token: 0x06001FBE RID: 8126 RVA: 0x00064418 File Offset: 0x00062618
		[Category("Behavior")]
		[Description("Fires when Aggregate property of GridBoundColumn is set to Custom. ")]
		public event GridCustomAggregateEventHandler CustomAggregate
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventCustomAggregate, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventCustomAggregate, value);
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06001FBF RID: 8127 RVA: 0x0006442B File Offset: 0x0006262B
		// (remove) Token: 0x06001FC0 RID: 8128 RVA: 0x0006443E File Offset: 0x0006263E
		[Category("Behavior")]
		[Description("Fires when an column is about to be created. ")]
		public event GridColumnCreatingEventHandler ColumnCreating
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventColumnCreating, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventColumnCreating, value);
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06001FC1 RID: 8129 RVA: 0x00064451 File Offset: 0x00062651
		// (remove) Token: 0x06001FC2 RID: 8130 RVA: 0x00064464 File Offset: 0x00062664
		[Description("Fires when an column is created. ")]
		[Category("Behavior")]
		public event GridColumnCreatedEventHandler ColumnCreated
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventColumnCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventColumnCreated, value);
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06001FC3 RID: 8131 RVA: 0x00064477 File Offset: 0x00062677
		// (remove) Token: 0x06001FC4 RID: 8132 RVA: 0x0006448A File Offset: 0x0006268A
		[Category("Behavior")]
		[Description("Fires when an item is bound to data")]
		public event GridItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventItemDataBound, value);
			}
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06001FC5 RID: 8133 RVA: 0x0006449D File Offset: 0x0006269D
		// (remove) Token: 0x06001FC6 RID: 8134 RVA: 0x000644B0 File Offset: 0x000626B0
		[Description("Fires when \"Page\" command bubbles")]
		[Category("Action")]
		public event GridPageChangedEventHandler PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventPageIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventPageIndexChanged, value);
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06001FC7 RID: 8135 RVA: 0x000644C3 File Offset: 0x000626C3
		// (remove) Token: 0x06001FC8 RID: 8136 RVA: 0x000644D6 File Offset: 0x000626D6
		[Category("Action")]
		[Description("Fires when PageSize has been changed.")]
		public event GridPageSizeChangedEventHandler PageSizeChanged
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventPageSizeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventPageSizeChanged, value);
			}
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06001FC9 RID: 8137 RVA: 0x000644E9 File Offset: 0x000626E9
		// (remove) Token: 0x06001FCA RID: 8138 RVA: 0x000644FC File Offset: 0x000626FC
		[Category("Action")]
		[Description("Fires when \"Sort\" command bubbles")]
		public event GridSortCommandEventHandler SortCommand
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventSortCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventSortCommand, value);
			}
		}

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06001FCB RID: 8139 RVA: 0x0006450F File Offset: 0x0006270F
		// (remove) Token: 0x06001FCC RID: 8140 RVA: 0x00064522 File Offset: 0x00062722
		[Category("Action")]
		[Description("Fires when \"Update\" command bubbles")]
		public event GridCommandEventHandler UpdateCommand
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventUpdateCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventUpdateCommand, value);
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06001FCD RID: 8141 RVA: 0x00064535 File Offset: 0x00062735
		// (remove) Token: 0x06001FCE RID: 8142 RVA: 0x00064548 File Offset: 0x00062748
		[Category("Action")]
		[Description("fires when \"Insert\" command bubbles")]
		public event GridCommandEventHandler InsertCommand
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventInsertCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventInsertCommand, value);
			}
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06001FCF RID: 8143 RVA: 0x0006455B File Offset: 0x0006275B
		// (remove) Token: 0x06001FD0 RID: 8144 RVA: 0x0006456E File Offset: 0x0006276E
		[Category("Action")]
		[Description("Fires when a column header was dragged onto/removed from the group panel")]
		public event GridGroupsChangingEventHandler GroupsChanging
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventGroupsChanging, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventGroupsChanging, value);
			}
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06001FD1 RID: 8145 RVA: 0x00064581 File Offset: 0x00062781
		// (remove) Token: 0x06001FD2 RID: 8146 RVA: 0x00064594 File Offset: 0x00062794
		[Description("Fires after an automatic update operation.")]
		[Category("Data editing")]
		public event GridUpdatedEventHandler ItemUpdated
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventItemUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventItemUpdated, value);
			}
		}

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06001FD3 RID: 8147 RVA: 0x000645A7 File Offset: 0x000627A7
		// (remove) Token: 0x06001FD4 RID: 8148 RVA: 0x000645BA File Offset: 0x000627BA
		[Category("Data editing")]
		[Description("Fires after an automatic insert operation.")]
		public event GridInsertedEventHandler ItemInserted
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventItemInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventItemInserted, value);
			}
		}

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06001FD5 RID: 8149 RVA: 0x000645CD File Offset: 0x000627CD
		// (remove) Token: 0x06001FD6 RID: 8150 RVA: 0x000645E0 File Offset: 0x000627E0
		[Description("Fires after an automatic delete operation.")]
		[Category("Data editing")]
		public event GridDeletedEventHandler ItemDeleted
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventItemDeleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventItemDeleted, value);
			}
		}

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06001FD7 RID: 8151 RVA: 0x000645F3 File Offset: 0x000627F3
		// (remove) Token: 0x06001FD8 RID: 8152 RVA: 0x00064606 File Offset: 0x00062806
		[Description("Fires when a grid is exported to ExcelML and styles collections is created.")]
		public event GridExcelMLExportStylesCreatedEventHandler ExcelMLExportStylesCreated
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventExcelMLExportStylesCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventExcelMLExportStylesCreated, value);
			}
		}

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06001FD9 RID: 8153 RVA: 0x00064619 File Offset: 0x00062819
		// (remove) Token: 0x06001FDA RID: 8154 RVA: 0x0006462C File Offset: 0x0006282C
		[Description("Fires when a grid is exported to ExcelML and row is created.")]
		public event GridExcelMLExportRowCreatedEventHandler ExcelMLExportRowCreated
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventExcelMLExportRowCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventExcelMLExportRowCreated, value);
			}
		}

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06001FDB RID: 8155 RVA: 0x0006463F File Offset: 0x0006283F
		// (remove) Token: 0x06001FDC RID: 8156 RVA: 0x00064652 File Offset: 0x00062852
		[Description("Fires when a grid is exported to ExcelML and WorkBook is created.")]
		public event EventHandler<GridExcelMLWorkBookCreatedEventArgs> ExcelMLWorkBookCreated
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventExcelMLWorkBookCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventExcelMLWorkBookCreated, value);
			}
		}

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06001FDD RID: 8157 RVA: 0x00064665 File Offset: 0x00062865
		// (remove) Token: 0x06001FDE RID: 8158 RVA: 0x00064678 File Offset: 0x00062878
		[Description("Fires when a grid is exporting.")]
		public event OnGridExportingEventHandler GridExporting
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventExporting, value);
			}
		}

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06001FDF RID: 8159 RVA: 0x0006468B File Offset: 0x0006288B
		// (remove) Token: 0x06001FE0 RID: 8160 RVA: 0x0006469E File Offset: 0x0006289E
		[Description("Fires when RadGrid is exporting to XLS BIFF, DOCX, or XLSX formats")]
		public event EventHandler<GridInfrastructureExportingEventArgs> InfrastructureExporting
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventInfrastructureExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventInfrastructureExporting, value);
			}
		}

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06001FE1 RID: 8161 RVA: 0x000646B1 File Offset: 0x000628B1
		// (remove) Token: 0x06001FE2 RID: 8162 RVA: 0x000646C4 File Offset: 0x000628C4
		[Description("Fires when RadGrid is exporting to BIFF format")]
		public event EventHandler<GridBiffExportingEventArgs> BiffExporting
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventBiffExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventBiffExporting, value);
			}
		}

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06001FE3 RID: 8163 RVA: 0x000646D7 File Offset: 0x000628D7
		// (remove) Token: 0x06001FE4 RID: 8164 RVA: 0x000646EA File Offset: 0x000628EA
		[Description("Fires before RadGrid's HTML is transformed to PDF.")]
		public event OnGridPdfExportingEventHandler PdfExporting
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventPdfExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventPdfExporting, value);
			}
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06001FE5 RID: 8165 RVA: 0x000646FD File Offset: 0x000628FD
		// (remove) Token: 0x06001FE6 RID: 8166 RVA: 0x00064710 File Offset: 0x00062910
		[Description("Fires when a grid is exporting to Word or HTML Excel.")]
		public event EventHandler<GridHTMLExportingEventArgs> HTMLExporting
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventHTMLExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventHTMLExporting, value);
			}
		}

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06001FE7 RID: 8167 RVA: 0x00064723 File Offset: 0x00062923
		// (remove) Token: 0x06001FE8 RID: 8168 RVA: 0x00064736 File Offset: 0x00062936
		[Description("Fires when a grid is exporting.")]
		[Obsolete("Please use ExportCellFormatting instead.")]
		public event OnExcelExportCellFormattingEventHandler ExcelExportCellFormatting
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventExcelExportCellFormatting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventExcelExportCellFormatting, value);
			}
		}

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06001FE9 RID: 8169 RVA: 0x00064749 File Offset: 0x00062949
		// (remove) Token: 0x06001FEA RID: 8170 RVA: 0x0006475C File Offset: 0x0006295C
		[Description("Fires when a grid is exporting.")]
		public event EventHandler<ExportCellFormattingEventArgs> ExportCellFormatting
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventExportCellFormatting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventExportCellFormatting, value);
			}
		}

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06001FEB RID: 8171 RVA: 0x0006476F File Offset: 0x0006296F
		// (remove) Token: 0x06001FEC RID: 8172 RVA: 0x00064782 File Offset: 0x00062982
		[Description("Fires when a columns reorder action has been performed")]
		[Category("Action")]
		public event GridColumnsReorderEventHandler ColumnsReorder
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventColumnsReorder, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventColumnsReorder, value);
			}
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x00064798 File Offset: 0x00062998
		static RadGrid()
		{
			RadGrid.EventCustomAggregate = new object();
			RadGrid.EventColumnCreated = new object();
			RadGrid.EventColumnCreating = new object();
			RadGrid.EventItemDataBound = new object();
			RadGrid.EventPageIndexChanged = new object();
			RadGrid.EventPageSizeChanged = new object();
			RadGrid.EventSortCommand = new object();
			RadGrid.EventUpdateCommand = new object();
			RadGrid.EventInsertCommand = new object();
			RadGrid.EventItemEvent = new object();
			RadGrid.EventDataBound = new object();
			RadGrid.EventGroupsChanging = new object();
			RadGrid.EventItemUpdated = new object();
			RadGrid.EventItemInserted = new object();
			RadGrid.EventItemDeleted = new object();
			RadGrid.EventExcelMLExportRowCreated = new object();
			RadGrid.EventExcelMLWorkBookCreated = new object();
			RadGrid.EventExcelMLExportStylesCreated = new object();
			RadGrid.EventRowDrop = new object();
			RadGrid.EventExporting = new object();
			RadGrid.EventPdfExporting = new object();
			RadGrid.EventExcelExportCellFormatting = new object();
			RadGrid.EventExportCellFormatting = new object();
			RadGrid.EventHTMLExporting = new object();
			RadGrid.EventBatchEditCommand = new object();
			RadGrid.EventBiffExporting = new object();
			RadGrid.EventInfrastructureExporting = new object();
			RadGrid.EventFieldDescriptorsReady = new object();
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x00064997 File Offset: 0x00062B97
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x0006499A File Offset: 0x00062B9A
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x000649A7 File Offset: 0x00062BA7
		[Description("The datasource that is used to populate the items in the list.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue(null)]
		[Category("Data")]
		[Bindable(true)]
		public override object DataSource
		{
			get
			{
				return this.MasterTableView.DataSource;
			}
			set
			{
				base.DataSource = value;
				this.MasterTableView.DataSource = value;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x000649BC File Offset: 0x00062BBC
		// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x000649C9 File Offset: 0x00062BC9
		public override string DataMember
		{
			get
			{
				return this.MasterTableView.DataMember;
			}
			set
			{
				this.MasterTableView.DataMember = value;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x000649D7 File Offset: 0x00062BD7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Description("Misc. grouping settings")]
		[Category("Grouping")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridGroupingSettings GroupingSettings
		{
			get
			{
				if (this._groupingSettings == null)
				{
					this._groupingSettings = new GridGroupingSettings(this, this.ViewState);
				}
				return this._groupingSettings;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x000649F9 File Offset: 0x00062BF9
		// (set) Token: 0x06001FF6 RID: 8182 RVA: 0x00064A01 File Offset: 0x00062C01
		[SimplePersistenceSetting]
		[NotifyParentProperty(true)]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x00064A0A File Offset: 0x00062C0A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Misc. sorting settings")]
		[Category("Sorting")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridSortingSettings SortingSettings
		{
			get
			{
				if (this._sortingSettings == null)
				{
					this._sortingSettings = new GridSortingSettings(this, this.ViewState);
				}
				return this._sortingSettings;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x00064A2C File Offset: 0x00062C2C
		[NotifyParentProperty(true)]
		[Description("Misc. hierarchy settings")]
		[Category("Hierarchy")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridHierarchySettings HierarchySettings
		{
			get
			{
				if (this._hierarchySettings == null)
				{
					this._hierarchySettings = new GridHierarchySettings(this, this.ViewState);
				}
				return this._hierarchySettings;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x00064A4E File Offset: 0x00062C4E
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(GridExportSettings))]
		[Description("Export settings")]
		[Category("Export")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridExportSettings ExportSettings
		{
			get
			{
				if (this._exportSettings == null)
				{
					this._exportSettings = new GridExportSettings(this.ViewState);
				}
				return this._exportSettings;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x00064A6F File Offset: 0x00062C6F
		[Description("Validation settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public GridValidationSettings ValidationSettings
		{
			get
			{
				if (this._validationSettings == null)
				{
					this._validationSettings = new GridValidationSettings(this.ViewState, this);
				}
				return this._validationSettings;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x00064A91 File Offset: 0x00062C91
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x00064A99 File Offset: 0x00062C99
		public override bool EnableViewState
		{
			get
			{
				return base.EnableViewState;
			}
			set
			{
				base.EnableViewState = value;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x00064AA2 File Offset: 0x00062CA2
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x00064AAF File Offset: 0x00062CAF
		[Category("Data")]
		[Description("Gets or sets the name of the method to call in order to update data")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public new string UpdateMethod
		{
			get
			{
				return this.MasterTableView.UpdateMethod;
			}
			set
			{
				this.MasterTableView.UpdateMethod = value;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x00064ABD File Offset: 0x00062CBD
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x00064ACA File Offset: 0x00062CCA
		[Description("Gets or sets the name of the method to call in order to insert data")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Data")]
		public new string InsertMethod
		{
			get
			{
				return this.MasterTableView.InsertMethod;
			}
			set
			{
				this.MasterTableView.InsertMethod = value;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x00064AD8 File Offset: 0x00062CD8
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x00064AE5 File Offset: 0x00062CE5
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("Gets or sets the name of the method to call in order to delete data")]
		[DefaultValue("")]
		public new string DeleteMethod
		{
			get
			{
				return this.MasterTableView.DeleteMethod;
			}
			set
			{
				this.MasterTableView.DeleteMethod = value;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x00064AF3 File Offset: 0x00062CF3
		// (set) Token: 0x06002004 RID: 8196 RVA: 0x00064B00 File Offset: 0x00062D00
		[Category("Data")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Gets or sets the name of the method to call in order to select data")]
		public override string SelectMethod
		{
			get
			{
				return this.MasterTableView.SelectMethod;
			}
			set
			{
				this.MasterTableView.SelectMethod = value;
			}
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00064B10 File Offset: 0x00062D10
		public override void DataBind()
		{
			if (this.IsNeedDataSourceInProgress)
			{
				throw new GridBindingException("You should not call DataBind in NeedDataSource event handler. DataBind would take place automatically right after NeedDataSource handler finishes execution.");
			}
			base.DataBind();
			this.currIndexHierarchical = 0;
			if (this.dataSourceControlAutomaticDataBindTriggered)
			{
				this.MasterTableView.CallEnsureDataBound();
				return;
			}
			this.MasterTableView.DataBind();
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00064B5C File Offset: 0x00062D5C
		internal void CallEnsureDataBound()
		{
			this.EnsureDataBound();
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00064B64 File Offset: 0x00062D64
		protected override DataSourceView GetData()
		{
			if (base.IsBoundUsingDataSourceID)
			{
				return new RadGrid.RadGridEmptyDataView(new RadGrid.RadGridEmptyDataSource(), this.DataMember);
			}
			return base.GetData();
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00064B85 File Offset: 0x00062D85
		protected override void OnDataSourceViewChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00064B87 File Offset: 0x00062D87
		protected override void OnDataBound(EventArgs e)
		{
			if (!this.suppressOnDataBoundEvent)
			{
				base.OnDataBound(e);
			}
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x00064B98 File Offset: 0x00062D98
		internal void CallOnDataBound(EventArgs e)
		{
			this.suppressOnDataBoundEvent = false;
			this.OnDataBound(e);
			this.suppressOnDataBoundEvent = true;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x00064BB0 File Offset: 0x00062DB0
		private void SetHierarchyIndexes(GridTableView tableView)
		{
			int num = 0;
			foreach (GridTableView gridTableView in tableView.DetailTables)
			{
				string hierarchyIndex = string.Empty;
				if (!string.IsNullOrEmpty(tableView.HierarchyIndex))
				{
					hierarchyIndex = tableView.HierarchyIndex + "_" + num;
				}
				else
				{
					hierarchyIndex = num.ToString();
				}
				gridTableView.SetHierarchyIndex(hierarchyIndex);
				num++;
				this.SetHierarchyIndexes(gridTableView);
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x00064C48 File Offset: 0x00062E48
		// (set) Token: 0x0600200D RID: 8205 RVA: 0x00064C50 File Offset: 0x00062E50
		internal bool ShouldBindInvisibleColumns { get; set; }

		// Token: 0x0600200E RID: 8206 RVA: 0x00064C5C File Offset: 0x00062E5C
		internal bool AddFilterMenus(GridTableView tableView)
		{
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				return false;
			}
			if (tableView.AllowFilteringByColumn)
			{
				return true;
			}
			foreach (GridTableView tableView2 in tableView.DetailTables)
			{
				if (this.AddFilterMenus(tableView2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00064CD0 File Offset: 0x00062ED0
		internal bool AddHeaderContextMenus(GridTableView tableView)
		{
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				return false;
			}
			if (tableView.EnableHeaderContextMenu)
			{
				return true;
			}
			foreach (GridTableView tableView2 in tableView.DetailTables)
			{
				if (this.AddHeaderContextMenus(tableView2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00064D44 File Offset: 0x00062F44
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.MasterTableView.SelfHierarchySettings.IsSet() && !base.DesignMode)
			{
				this.CreateSelfRefence(this.MasterTableView, this.MasterTableView.SelfHierarchySettings, 0);
				this.DetailTableDataBind += this.RadGrid_DetailTableDataBind;
			}
			foreach (GridTableView gridTableView in this.MasterTableView.DetailTables)
			{
				if (gridTableView.SelfHierarchySettings.IsSet() && !base.DesignMode)
				{
					this.CreateSelfRefence(gridTableView, gridTableView.SelfHierarchySettings, 0);
				}
			}
			if (!base.DesignMode && this.BrowserIsCrawler())
			{
				this.PagerStyle.EnableSEOPaging = true;
			}
			if (this.AlwaysAutoBindOnPostBack && this.Page.IsPostBack)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00064E3C File Offset: 0x0006303C
		protected internal virtual bool BrowserIsCrawler()
		{
			return this.Page != null && this.Page.Request != null && this.Page.Request.Browser.Crawler;
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00064E6A File Offset: 0x0006306A
		private void RadGrid_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.DetailTableView.DataSourceID))
			{
				e.DetailTableView.DataSourceID = "";
				e.DetailTableView.DataSource = this.MasterTableView._currentDataSource;
			}
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00064EA4 File Offset: 0x000630A4
		protected internal override string GetSkinSuffix()
		{
			return base.GetSkinSuffix();
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00064EAC File Offset: 0x000630AC
		private void CreateSelfRefence(GridTableView parentTableView, GridSelfHierarchySettings settings, int currentLevel)
		{
			if (currentLevel > settings.MaximumDepth)
			{
				return;
			}
			GridTableView gridTableView = this.CreateTableView();
			gridTableView.CopyBaseAttributes(parentTableView);
			gridTableView.CopyProperties(parentTableView);
			foreach (object obj in parentTableView.Columns)
			{
				GridColumn from = (GridColumn)obj;
				GridColumn column = GridColumn.InheritanceSafeClone(from);
				gridTableView.Columns.Add(column);
			}
			gridTableView.DataKeyNames = parentTableView.DataKeyNames;
			GridRelationFields gridRelationFields = new GridRelationFields();
			gridRelationFields.MasterKeyField = settings.KeyName;
			gridRelationFields.DetailKeyField = settings.ParentKeyName;
			gridTableView.ParentTableRelation.Add(gridRelationFields);
			parentTableView.DetailTables.Add(gridTableView);
			this.CreateSelfRefence(gridTableView, settings, currentLevel + 1);
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00064F88 File Offset: 0x00063188
		private GridItem GetItemForCommandFire(GridTableView tableView, params GridItemType[] itemTypes)
		{
			foreach (GridItemType gridItemType in itemTypes)
			{
				GridItem[] items = tableView.GetItems(new GridItemType[]
				{
					gridItemType
				});
				if (items.Length > 0)
				{
					return items[0];
				}
			}
			return null;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00064FD4 File Offset: 0x000631D4
		private void HandleGlobalBatchUpdate(string eventArgument)
		{
			string[] array = eventArgument.Split(new string[]
			{
				":::"
			}, StringSplitOptions.RemoveEmptyEntries);
			List<GridTableView> list = new List<GridTableView>();
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					';'
				}, 2);
				GridTableView gridTableView = (GridTableView)this.Page.FindControl(array3[0]);
				GridItem itemForCommandFire = this.GetItemForCommandFire(gridTableView, new GridItemType[]
				{
					GridItemType.CommandItem,
					GridItemType.Header,
					GridItemType.Item
				});
				GridBatchEditingEventArgs gridBatchEditingEventArgs = new GridBatchEditingEventArgs(itemForCommandFire, array3[1], true);
				gridBatchEditingEventArgs.ExecuteCommand(itemForCommandFire);
				if (!gridTableView.SuppressRebindOnUpdate)
				{
					bool flag = true;
					foreach (GridTableView parent in list)
					{
						if (this.IsTableAChild(parent, gridTableView))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						for (int j = list.Count - 1; j >= 0; j--)
						{
							if (this.IsTableAChild(gridTableView, list[j]))
							{
								list.RemoveAt(j);
							}
						}
						list.Add(gridTableView);
					}
				}
			}
			foreach (GridTableView gridTableView2 in list)
			{
				gridTableView2.SaveExpandCollapseState(true);
				gridTableView2.Rebind();
				gridTableView2.LoadExpandCollapseState();
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x00065170 File Offset: 0x00063370
		private bool IsTableAChild(Control parent, Control child)
		{
			while (child != null)
			{
				if (child == parent)
				{
					return true;
				}
				child = child.Parent;
			}
			return false;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00065188 File Offset: 0x00063388
		public virtual void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.IndexOf("RowClick;") != -1 && this.Items.Count > 0)
			{
				string hierarchicalIndex = eventArgument.Split(new char[]
				{
					';'
				})[1];
				GridItem gridItem = this.Items.FindByHierarchyIndex(hierarchicalIndex);
				if (gridItem != null)
				{
					if (!this.RowClickOnly)
					{
						gridItem.OwnerTableView.TrackSelection(gridItem, gridItem.Selected);
					}
					gridItem.FireCommandEvent(eventArgument.Split(new char[]
					{
						';'
					})[0], string.Empty);
				}
			}
			if (eventArgument.IndexOf("UnGroupByExpression") != -1 && eventArgument.IndexOf("FireCommand") == -1)
			{
				this.GroupPanel.Ungroup(eventArgument.Split(new char[]
				{
					','
				})[1]);
			}
			if (eventArgument.IndexOf("ReorderGroupByExpression") != -1)
			{
				if (this.ResolvedRenderMode == RenderMode.Mobile)
				{
					string[] array = eventArgument.Split(new char[]
					{
						';'
					})[2].Split(new char[]
					{
						','
					});
					this.GroupPanel.Swap(array[0], array[1], this);
				}
				else
				{
					this.GroupPanel.Swap(eventArgument.Split(new char[]
					{
						','
					})[1], eventArgument.Split(new char[]
					{
						','
					})[2], this);
				}
			}
			if (eventArgument.IndexOf("ReorderColumns") != -1)
			{
				GridTableView gridTableView = (GridTableView)this.Page.FindControl(eventArgument.Split(new char[]
				{
					','
				})[1]);
				if (gridTableView != null)
				{
					GridColumn column = gridTableView.GetColumn(eventArgument.Split(new char[]
					{
						','
					})[3]);
					GridColumn column2 = gridTableView.GetColumn(eventArgument.Split(new char[]
					{
						','
					})[2]);
					GridColumnsReorderEventArgs gridColumnsReorderEventArgs = new GridColumnsReorderEventArgs(column, column2);
					GridColumnsReorderEventHandler gridColumnsReorderEventHandler = (GridColumnsReorderEventHandler)base.Events[RadGrid.EventColumnsReorder];
					if (gridColumnsReorderEventHandler != null)
					{
						gridColumnsReorderEventHandler(this, gridColumnsReorderEventArgs);
					}
					if (gridColumnsReorderEventArgs.Canceled)
					{
						return;
					}
					if (this.ClientSettings.ColumnsReorderMethod == GridClientSettings.GridColumnsReorderMethod.Reorder)
					{
						List<GridColumn> list = new List<GridColumn>();
						foreach (GridColumn item in gridTableView.RenderColumns)
						{
							list.Add(item);
						}
						int j = list.IndexOf(column);
						int k = list.IndexOf(column2);
						if (k > j)
						{
							while (j < k)
							{
								GridColumn column3 = list[j + 1];
								GridColumn column4 = list[j];
								gridTableView.SwapColumns(column3, column4);
								j++;
							}
						}
						else
						{
							while (k < j)
							{
								GridColumn column5 = list[j - 1];
								GridColumn column6 = list[j];
								gridTableView.SwapColumns(column5, column6);
								j--;
							}
						}
					}
					else
					{
						gridTableView.SwapColumns(column, column2);
					}
				}
			}
			if (eventArgument.IndexOf("GlobalBatchEdit:") == 0)
			{
				this.HandleGlobalBatchUpdate(eventArgument.Split(new char[]
				{
					':'
				}, 2)[1]);
			}
			if (eventArgument.IndexOf("FireCommand") != -1)
			{
				string[] array2 = eventArgument.Split(new char[]
				{
					';'
				});
				string id = array2[0].Split(new char[]
				{
					':'
				})[1];
				GridTableView gridTableView2 = (GridTableView)this.Page.FindControl(id);
				string text = array2[1];
				string text2 = string.Join(";", array2, 2, array2.Length - 2);
				this.ClearActiveRowIndex(true);
				string key;
				switch (key = text)
				{
				case "BatchEdit":
				{
					GridItem itemForCommandFire = this.GetItemForCommandFire(gridTableView2, new GridItemType[]
					{
						GridItemType.CommandItem,
						GridItemType.Header,
						GridItemType.Item
					});
					GridBatchEditingEventArgs gridBatchEditingEventArgs = new GridBatchEditingEventArgs(itemForCommandFire, text2);
					gridBatchEditingEventArgs.ExecuteCommand(itemForCommandFire);
					return;
				}
				case "InitInsert":
				{
					GridItem[] items = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.CommandItem
					});
					if (items.Length > 0)
					{
						items[0].FireCommandEvent("InitInsert", string.Empty);
						if (this.ClientSettings.AllowKeyboardNavigation)
						{
							this.shouldFocusOnPage = true;
							return;
						}
						return;
					}
					else
					{
						GridItem[] items2 = gridTableView2.GetItems(new GridItemType[]
						{
							GridItemType.Header
						});
						if (items2.Length > 0)
						{
							items2[0].FireCommandEvent("InitInsert", string.Empty);
							return;
						}
						gridTableView2.IsItemInserted = true;
						gridTableView2.Rebind();
						return;
					}
					break;
				}
				case "CancelInsert":
				{
					GridItem insertItem = gridTableView2.GetInsertItem();
					if (insertItem != null)
					{
						insertItem.FireCommandEvent("Cancel", string.Empty);
					}
					else
					{
						gridTableView2.IsItemInserted = false;
						gridTableView2.Rebind();
					}
					if (this.ClientSettings.AllowKeyboardNavigation)
					{
						this.shouldFocusOnPage = true;
						return;
					}
					return;
				}
				case "Select":
					this.Items[text2].FireCommandEvent("Select", string.Empty);
					return;
				case "Edit":
					if (this.ClientSettings.AllowKeyboardNavigation && this.Items[text2].OwnerTableView != null)
					{
						string activeRowIndex = this.Items[text2].OwnerTableView.ClientID + "__" + this.Items[text2].ItemIndexHierarchical;
						this.ClientSettings.ActiveRowIndex = activeRowIndex;
						this.shouldFocusOnPage = true;
					}
					this.Items[text2].FireCommandEvent("Edit", "");
					return;
				case "Update":
					if (this.ClientSettings.AllowKeyboardNavigation && this.Items[text2].OwnerTableView != null)
					{
						string activeRowIndex2 = this.Items[text2].OwnerTableView.ClientID + "__" + this.Items[text2].ItemIndexHierarchical;
						this.ClientSettings.ActiveRowIndex = activeRowIndex2;
						this.shouldFocusOnPage = true;
					}
					if (this.Items[text2].OwnerTableView.EditMode == GridEditMode.InPlace)
					{
						this.Items[text2].FireCommandEvent("Update", string.Empty);
						return;
					}
					this.Items[text2].EditFormItem.FireCommandEvent("Update", string.Empty);
					return;
				case "Delete":
					this.Items[text2].FireCommandEvent("Delete", "");
					return;
				case "PerformInsert":
					gridTableView2.GetInsertItem().FireCommandEvent("PerformInsert", "");
					return;
				case "RebindGrid":
				{
					GridItem[] items3 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					});
					if (items3.Length > 0)
					{
						((GridHeaderItem)items3[0]).FireCommandEvent("RebindGrid", text2);
						return;
					}
					gridTableView2.Rebind();
					return;
				}
				case "Sort":
				{
					GridItem[] items4 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					});
					if (items4.Length > 0)
					{
						((GridHeaderItem)items4[0]).FireCommandEvent("Sort", text2);
						return;
					}
					return;
				}
				case "HeaderSort":
				{
					GridHeaderItem gridHeaderItem = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					})[0] as GridHeaderItem;
					gridHeaderItem.FireCommandEvent("HeaderSort", text2);
					return;
				}
				case "ClearSort":
				{
					GridItem[] items5 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					});
					if (items5.Length > 0)
					{
						items5[0].FireCommandEvent("ClearSort", text2);
						return;
					}
					return;
				}
				case "Page":
				{
					GridItem[] items6 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Pager
					});
					if (items6.Length > 0)
					{
						((GridPagerItem)items6[0]).FireCommandEvent("Page", text2);
						return;
					}
					return;
				}
				case "ExportToExcel":
				{
					GridItem[] items7 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.CommandItem
					});
					if (items7.Length > 0)
					{
						((GridCommandItem)items7[0]).FireCommandEvent("ExportToExcel", string.Empty);
						return;
					}
					GridItem[] items8 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					});
					if (items8.Length > 0)
					{
						items8[0].FireCommandEvent("ExportToExcel", string.Empty);
						return;
					}
					gridTableView2.ExportToExcel();
					return;
				}
				case "ExportToWord":
				{
					GridItem[] items9 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.CommandItem
					});
					if (items9.Length > 0)
					{
						((GridCommandItem)items9[0]).FireCommandEvent("ExportToWord", string.Empty);
						return;
					}
					GridItem[] items10 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					});
					if (items10.Length > 0)
					{
						items10[0].FireCommandEvent("ExportToWord", string.Empty);
						return;
					}
					gridTableView2.ExportToWord();
					return;
				}
				case "ExportToCsv":
				{
					GridItem[] items11 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.CommandItem
					});
					if (items11.Length > 0)
					{
						((GridCommandItem)items11[0]).FireCommandEvent("ExportToCsv", string.Empty);
						return;
					}
					GridItem[] items12 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					});
					if (items12.Length > 0)
					{
						items12[0].FireCommandEvent("ExportToCsv", string.Empty);
						return;
					}
					gridTableView2.ExportToCSV();
					return;
				}
				case "ExportToPdf":
				{
					GridItem[] items13 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.CommandItem
					});
					if (items13.Length > 0)
					{
						((GridCommandItem)items13[0]).FireCommandEvent("ExportToPdf", string.Empty);
						return;
					}
					GridItem[] items14 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					});
					if (items14.Length > 0)
					{
						items14[0].FireCommandEvent("ExportToPdf", string.Empty);
						return;
					}
					gridTableView2.ExportToPdf();
					return;
				}
				case "EditSelected":
				{
					GridItem[] items15 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.CommandItem
					});
					if (items15.Length > 0)
					{
						((GridCommandItem)items15[0]).FireCommandEvent("EditSelected", string.Empty);
						return;
					}
					if (this.Items.Count > 0)
					{
						this.Items[0].FireCommandEvent("EditSelected", string.Empty);
						return;
					}
					return;
				}
				case "DeleteSelected":
				{
					GridItem[] items16 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.CommandItem
					});
					if (items16.Length > 0)
					{
						((GridCommandItem)items16[0]).FireCommandEvent("DeleteSelected", string.Empty);
						return;
					}
					if (this.Items.Count > 0)
					{
						this.Items[0].FireCommandEvent("DeleteSelected", string.Empty);
						return;
					}
					return;
				}
				case "UpdateEdited":
				{
					GridItem[] items17 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.CommandItem
					});
					if (items17.Length > 0)
					{
						((GridCommandItem)items17[0]).FireCommandEvent("UpdateEdited", string.Empty);
						return;
					}
					if (this.Items.Count > 0)
					{
						this.Items[0].FireCommandEvent("UpdateEdited", string.Empty);
						return;
					}
					return;
				}
				case "EditAll":
				{
					GridItem[] items18 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.CommandItem
					});
					if (items18.Length > 0)
					{
						((GridCommandItem)items18[0]).FireCommandEvent("EditAll", string.Empty);
						return;
					}
					if (this.Items.Count > 0)
					{
						this.Items[0].FireCommandEvent("EditAll", string.Empty);
						return;
					}
					return;
				}
				case "CancelAll":
				{
					GridItem[] items19 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.CommandItem
					});
					if (items19.Length > 0)
					{
						((GridCommandItem)items19[0]).FireCommandEvent("CancelAll", string.Empty);
						return;
					}
					if (this.Items.Count > 0)
					{
						this.Items[0].FireCommandEvent("CancelAll", string.Empty);
						return;
					}
					return;
				}
				case "CancelUpdate":
					if (this.ClientSettings.AllowKeyboardNavigation && this.Items[text2].OwnerTableView != null)
					{
						string activeRowIndex3 = this.Items[text2].OwnerTableView.ClientID + "__" + this.Items[text2].ItemIndexHierarchical;
						this.ClientSettings.ActiveRowIndex = activeRowIndex3;
						this.shouldFocusOnPage = true;
					}
					this.Items[text2].FireCommandEvent("Cancel", "");
					return;
				case "GroupByColumn":
				{
					GridColumn gridColumn = gridTableView2.GetColumnSafe(text2);
					if (!gridColumn.Groupable)
					{
						gridColumn = gridTableView2.GetGroupableColumnSafe(text2);
					}
					GridGroupByExpression gridGroupByExpression = new GridGroupByExpression(gridColumn);
					GridGroupsChangingEventArgs gridGroupsChangingEventArgs = new GridGroupsChangingEventArgs(gridTableView2, gridGroupByExpression, GridGroupsChangingAction.Group);
					this.CallOnGroupsChanging(gridGroupsChangingEventArgs);
					if (gridGroupsChangingEventArgs.Canceled)
					{
						return;
					}
					gridGroupByExpression = gridGroupsChangingEventArgs.Expression;
					GridRebindReason gridRebindReason = GridRebindReason.PostBackEvent;
					if (gridTableView2.IsClone)
					{
						gridRebindReason |= GridRebindReason.DetailTableBinding;
					}
					gridTableView2.ObtainDataSource(gridRebindReason);
					gridTableView2.GroupByExpressions.Add(gridGroupByExpression);
					gridTableView2.ResetRenderColumns();
					gridTableView2.BindAllInHierarchyLevel();
					return;
				}
				case "UnGroupByColumn":
				{
					GridColumn gridColumn2 = gridTableView2.GetColumnSafe(text2);
					if (!gridColumn2.Groupable)
					{
						gridColumn2 = gridTableView2.GetGroupableColumnSafe(text2);
					}
					GridGroupByExpression gridGroupByExpression2 = new GridGroupByExpression(gridColumn2);
					GridGroupByExpression gridGroupByExpression3 = null;
					foreach (GridGroupByExpression gridGroupByExpression4 in gridTableView2.GroupByExpressions)
					{
						if (gridGroupByExpression4.Expression.Contains(gridGroupByExpression2.Expression))
						{
							gridGroupByExpression3 = gridGroupByExpression4;
							break;
						}
						foreach (object obj in gridGroupByExpression4.GroupByFields)
						{
							GridGroupByField gridGroupByField = (GridGroupByField)obj;
							if (gridGroupByExpression2.GroupByFields[0].FieldName == gridGroupByField.FieldName)
							{
								gridGroupByExpression3 = gridGroupByExpression4;
								break;
							}
						}
					}
					if (gridGroupByExpression3 == null)
					{
						return;
					}
					GridGroupsChangingEventArgs gridGroupsChangingEventArgs2 = new GridGroupsChangingEventArgs(gridTableView2, gridGroupByExpression2, GridGroupsChangingAction.Ungroup);
					this.CallOnGroupsChanging(gridGroupsChangingEventArgs2);
					if (gridGroupsChangingEventArgs2.Canceled)
					{
						return;
					}
					gridGroupByExpression2 = gridGroupsChangingEventArgs2.Expression;
					GridRebindReason gridRebindReason2 = GridRebindReason.PostBackEvent;
					if (gridTableView2.IsClone)
					{
						gridRebindReason2 |= GridRebindReason.DetailTableBinding;
					}
					gridTableView2.ObtainDataSource(gridRebindReason2);
					gridTableView2.GroupByExpressions.Remove(gridGroupByExpression3);
					gridTableView2.ResetRenderColumns();
					gridTableView2.BindAllInHierarchyLevel();
					return;
				}
				case "UnGroupByExpression":
					this.GroupPanel.Ungroup(text2);
					return;
				case "Filter":
				{
					string[] array3 = text2.Split(new string[]
					{
						"|?"
					}, StringSplitOptions.None);
					string text3 = array3[0];
					string text4 = array3[1];
					string text5 = array3[2];
					if (text4.Contains("^#"))
					{
						text4 = text4.Replace("^#", ";");
					}
					GridColumn columnSafe = gridTableView2.GetColumnSafe(text3);
					if (columnSafe == null)
					{
						return;
					}
					columnSafe.SetCurrentFilterValueFromFilterCommand(text4);
					if (string.Compare(text5, "Between", true) == 0 || string.Compare(text5, "NotBetween", true) == 0)
					{
						this.SetIsBetweenFilter(columnSafe);
					}
					GridItem[] items20 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.FilteringItem
					});
					if (items20.Length > 0)
					{
						items20[0].OwnerTableView.CurrentPageIndex = 0;
						columnSafe.SetCurrentFilterValueToControlInternal(((GridFilteringItem)items20[0])[text3]);
						((GridFilteringItem)items20[0]).FireCommandEvent("Filter", new Pair(text5, text3));
						return;
					}
					return;
				}
				case "ClearFilter":
				{
					GridItem[] items21 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.FilteringItem
					});
					if (items21.Length < 1)
					{
						items21 = gridTableView2.GetItems(new GridItemType[]
						{
							GridItemType.Header
						});
					}
					if (items21.Length <= 0)
					{
						return;
					}
					if (string.IsNullOrEmpty(text2))
					{
						items21[0].FireCommandEvent("ClearFilter", text2);
						return;
					}
					GridColumn columnSafe2 = gridTableView2.GetColumnSafe(text2);
					if (columnSafe2 != null)
					{
						items21[0].FireCommandEvent("Filter", new Pair("NoFilter", text2));
						return;
					}
					return;
				}
				case "HeaderContextMenuFilter":
				{
					string str = string.Join("|", Enum.GetNames(typeof(GridKnownFunction)));
					string[] array4 = new Regex("\\|\\?(" + str + ")").Split(text2);
					string text6 = array4[0];
					string x = array4[1];
					string y = array4[2].Substring(1);
					string x2 = array4[3];
					string y2 = array4[4].Substring(1);
					Triplet commandArgument = new Triplet(text6, new Pair(x, y), new Pair(x2, y2));
					GridColumn columnSafe3 = gridTableView2.GetColumnSafe(text6);
					if (columnSafe3 == null)
					{
						return;
					}
					GridItem[] items22 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					});
					if (gridTableView2.AllowFilteringByColumn)
					{
						GridItem[] items23 = gridTableView2.GetItems(new GridItemType[]
						{
							GridItemType.FilteringItem
						});
						if (items23.Length > 0)
						{
							columnSafe3.SetCurrentFilterValueToControlInternal(((GridFilteringItem)items23[0])[text6]);
						}
					}
					if (items22.Length > 0)
					{
						items22[0].OwnerTableView.CurrentPageIndex = 0;
						columnSafe3.TriggeredFilterCommand = true;
						((GridHeaderItem)items22[0]).FireCommandEvent("HeaderContextMenuFilter", commandArgument);
						return;
					}
					return;
				}
				case "SetColumnAggregate":
					this.HandleSetColumnAggregateClientCommand(text2);
					return;
				case "PageSize":
					gridTableView2.CurrentPageIndex = 0;
					gridTableView2.PageSize = int.Parse(text2);
					if (gridTableView2 == gridTableView2.OwnerGrid.MasterTableView && gridTableView2.DetailTables.Count == 0)
					{
						gridTableView2.OwnerGrid.PageSize = int.Parse(text2);
					}
					if (((gridTableView2.AllowCustomPaging && !gridTableView2.EnableViewState) || (gridTableView2.OwnerGrid.AllowCustomPaging && !gridTableView2.OwnerGrid.EnableViewState)) && !gridTableView2.BoundUsingDataSourceID)
					{
						gridTableView2.DataSource = null;
					}
					gridTableView2.ClearEditItemsAfterPageSizeChanged(gridTableView2.PageSize);
					gridTableView2.Rebind();
					return;
				case "RowDropped":
				{
					string text7 = text2.Split(new char[]
					{
						','
					})[0];
					string text8 = text2.Split(new char[]
					{
						','
					})[1];
					string text9 = text2.Split(new char[]
					{
						','
					})[2].Trim();
					string text10 = (text2.Split(new char[]
					{
						','
					}).Length >= 3) ? text2.Split(new char[]
					{
						','
					})[3].Trim() : string.Empty;
					GridItemDropPosition dropPosition = text9.ToUpper().Equals("BELOW") ? GridItemDropPosition.Below : GridItemDropPosition.Above;
					GridDataItem destinationItem = null;
					RadGrid radGrid = null;
					GridTableView destinationItemTableView = null;
					if (!string.IsNullOrEmpty(text8))
					{
						radGrid = (this.Page.FindControl(text8) as RadGrid);
						if (radGrid != null && !string.IsNullOrEmpty(text7))
						{
							destinationItem = radGrid.Items[text7];
						}
						if (!string.IsNullOrEmpty(text10))
						{
							destinationItemTableView = (this.Page.FindControl(text10) as GridTableView);
						}
					}
					List<GridDataItem> list2 = new List<GridDataItem>();
					foreach (string hierarchicalIndex2 in this.draggedItemsIndexes)
					{
						list2.Add(this.Items[hierarchicalIndex2]);
					}
					this.OnRowDropEvent(list2, destinationItem, radGrid, dropPosition, destinationItemTableView);
					return;
				}
				case "RowDroppedHtml":
				{
					string htmlElementId = text2.Split(new char[]
					{
						','
					})[0];
					List<GridDataItem> list3 = new List<GridDataItem>();
					foreach (string hierarchicalIndex3 in this.draggedItemsIndexes)
					{
						list3.Add(this.Items[hierarchicalIndex3]);
					}
					this.OnRowDropEvent(list3, htmlElementId);
					return;
				}
				case "ExpandCollapse":
					if (text2.Contains("##"))
					{
						string text11 = text2.Replace("##", "").Split(new string[]
						{
							"__"
						}, StringSplitOptions.RemoveEmptyEntries)[1];
						this.ClientSettings.ActiveRowIndex = text11;
						this.shouldFocusOnPage = true;
						this.Items[text11].FireCommandEvent("ExpandCollapse", text11);
						return;
					}
					this.Items[text2].FireCommandEvent("ExpandCollapse", text2);
					return;
				case "ExpandCollapseAll":
				{
					GridItem gridItem2 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					})[0];
					if (gridItem2 != null)
					{
						gridItem2.FireCommandEvent("ExpandCollapseAll", text2);
						return;
					}
					return;
				}
				case "GroupsCustomExpandCollapse":
				{
					GridItem gridItem3 = gridTableView2.GetItems(new GridItemType[]
					{
						GridItemType.Header
					})[0];
					if (gridItem3 != null)
					{
						gridItem3.FireCommandEvent("GroupsCustomExpandCollapse", text2);
						return;
					}
					return;
				}
				}
				if (gridTableView2.Items.Count > 0)
				{
					gridTableView2.Items[0].FireCommandEvent(text, text2);
				}
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x000667DC File Offset: 0x000649DC
		private void SetIsBetweenFilter(GridColumn column)
		{
			string columnType;
			if ((columnType = column.ColumnType) != null)
			{
				if (columnType == "GridDateTimeColumn")
				{
					(column as GridDateTimeColumn).IsBetweenFilter = true;
					return;
				}
				if (columnType == "GridNumericColumn")
				{
					(column as GridNumericColumn).IsBetweenFilter = true;
					return;
				}
				if (!(columnType == "GridRatingColumn"))
				{
					return;
				}
				(column as GridRatingColumn).IsBetweenFilter = true;
			}
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00066844 File Offset: 0x00064A44
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void HandleSetColumnAggregateClientCommand(string commandArgument)
		{
			string[] array = commandArgument.Split(new char[]
			{
				'|'
			});
			if (array.Length == 3)
			{
				string value = array[0];
				string id = array[1];
				string columnUniqueName = array[2];
				GridAggregateFunction aggregate = (GridAggregateFunction)Enum.Parse(typeof(GridAggregateFunction), value);
				GridTableView gridTableView = this.Page.FindControl(id) as GridTableView;
				if (gridTableView != null)
				{
					GridColumn column = gridTableView.GetColumn(columnUniqueName);
					if (column is GridBoundColumn)
					{
						((GridBoundColumn)column).Aggregate = aggregate;
						gridTableView.Rebind();
						return;
					}
					if (column is GridTemplateColumn)
					{
						((GridTemplateColumn)column).Aggregate = aggregate;
						gridTableView.Rebind();
						return;
					}
					if (column is GridCalculatedColumn)
					{
						((GridCalculatedColumn)column).Aggregate = aggregate;
						gridTableView.Rebind();
					}
				}
			}
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00066914 File Offset: 0x00064B14
		protected virtual void OnRowDropEvent(IList<GridDataItem> draggedItems, GridDataItem destinationItem, RadGrid parentGrid, GridItemDropPosition dropPosition, GridTableView destinationItemTableView)
		{
			GridDragDropEventHandler gridDragDropEventHandler = (GridDragDropEventHandler)base.Events[RadGrid.EventRowDrop];
			if (gridDragDropEventHandler != null)
			{
				gridDragDropEventHandler(this, new GridDragDropEventArgs(draggedItems, destinationItem, parentGrid, dropPosition, destinationItemTableView));
			}
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00066950 File Offset: 0x00064B50
		protected virtual void OnRowDropEvent(IList<GridDataItem> draggedItems, string htmlElementId)
		{
			GridDragDropEventHandler gridDragDropEventHandler = (GridDragDropEventHandler)base.Events[RadGrid.EventRowDrop];
			if (gridDragDropEventHandler != null)
			{
				gridDragDropEventHandler(this, new GridDragDropEventArgs(draggedItems, htmlElementId));
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x00066984 File Offset: 0x00064B84
		// (set) Token: 0x0600201E RID: 8222 RVA: 0x000669AD File Offset: 0x00064BAD
		[DefaultValue(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual bool ShowDesignTimeSmartTagMessage
		{
			get
			{
				object obj = this.ViewState["_sdtstm"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["_sdtstm"] = value;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x000669C8 File Offset: 0x00064BC8
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x000669F1 File Offset: 0x00064BF1
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual bool AutoGenerateEditColumn
		{
			get
			{
				object obj = this.ViewState["_agec"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["_agec"] = value;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x00066A0C File Offset: 0x00064C0C
		// (set) Token: 0x06002022 RID: 8226 RVA: 0x00066A35 File Offset: 0x00064C35
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool AutoGenerateDeleteColumn
		{
			get
			{
				object obj = this.ViewState["_agdc"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["_agdc"] = value;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x00066A4D File Offset: 0x00064C4D
		internal bool AlwaysAutoBindOnPostBack
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x00066A58 File Offset: 0x00064C58
		private bool IsMobile
		{
			get
			{
				return this.Page.Request != null && !string.IsNullOrEmpty(this.Page.Request.UserAgent) && (Regex.IsMatch(this.Page.Request.UserAgent, "like\\sMac\\sOS\\sX.*Mobile\\S+") || Regex.IsMatch(this.Page.Request.UserAgent, "Android.*Safari\\S+") || Regex.IsMatch(this.Page.Request.UserAgent, "BlackBerry.*Safari\\S+"));
			}
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x00066AF4 File Offset: 0x00064CF4
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>();
			foreach (ScriptReference scriptReference in scriptReferences)
			{
				if (scriptReference.Name != "Telerik.Web.UI.Grid.RadGrid.Desktop.js")
				{
					list.Add(scriptReference);
				}
			}
			RadScriptManager radScriptManager = ScriptManager.GetCurrent(this.Page) as RadScriptManager;
			if (!this.EnableEmbeddedScripts)
			{
				if (radScriptManager != null && radScriptManager.CdnSettings.CombinedResourceResloved == CombinedResourceMode.Enabled && this.ResolvedRenderMode == RenderMode.Mobile)
				{
					list.Add(new ScriptReference("Telerik.Web.UI.Grid.RadGridMobileScripts.js", Assembly.GetExecutingAssembly().FullName));
				}
				return list;
			}
			bool flag = true;
			if (radScriptManager != null)
			{
				flag = radScriptManager.EnableEmbeddedjQuery;
			}
			string fullName = Assembly.GetExecutingAssembly().FullName;
			if (this.ResolvedRenderMode == RenderMode.Lightweight && flag)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.jQuery.js", fullName));
			}
			if ((this.ResolvedRenderMode == RenderMode.Mobile || this.ClientSettings.DataBinding.IsSet || this.ClientSettings.AllowKeyboardNavigation || (this.ResolvedRenderMode == RenderMode.Lightweight && base.RuntimeSkin == "Material") || this.ClientSettings.Animation.IsSet || this.ClientSettings.Scrolling.AllowScroll || this.ClientSettings.Selecting.CellSelectionMode != GridCellSelectionMode.None || this.ClientSettings.Scrolling.EnableNextPrevFrozenColumns || this.ClientSettings.EnableClientPrint || GridBatchEditingHelper.IsBatchEditingEnabled(this.MasterTableView) || RadGrid.IsScreenBoundaryDetectionEnabled(this.MasterTableView) || (this.IsMobile && this.ClientSettings.ClientEvents.OnRowDblClick != string.Empty)) && flag)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.jQuery.js", Assembly.GetExecutingAssembly().FullName));
				list.Add(new ScriptReference("Telerik.Web.UI.Common.jQueryPlugins.js", Assembly.GetExecutingAssembly().FullName));
			}
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.GestureFramework.GestureFramework.js", Assembly.GetExecutingAssembly().FullName));
			}
			if (this.ClientSettings.Scrolling.AllowScroll)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.TouchScrollExtender.js", Assembly.GetExecutingAssembly().FullName));
			}
			if (this.ClientSettings.Selecting.CellSelectionMode != GridCellSelectionMode.None)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.Extensions.js", Assembly.GetExecutingAssembly().FullName));
				this.AddScriptReference(list, "Telerik.Web.UI.Grid.GridCellSelection.js");
			}
			if (this.ClientSettings.EnableClientPrint)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.core.js", fullName));
				list.Add(new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.color.js", fullName));
				list.Add(new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.drawing.js", fullName));
				list.Add(new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.popup.js", fullName));
				list.Add(new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.pdf.js", fullName));
			}
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Grid.RadGridMobileScripts.js", Assembly.GetExecutingAssembly().FullName));
				list.Remove(list.Find((ScriptReference r) => r.Name == "Telerik.Web.UI.Grid.RadGridScripts.js"));
			}
			else
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Grid.RadGridScripts.js", Assembly.GetExecutingAssembly().FullName));
			}
			if (this.ClientSettings.Virtualization.EnableVirtualization)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Grid.GridVirtualizationScripts.js", Assembly.GetExecutingAssembly().FullName));
			}
			if (GridBatchEditingHelper.IsBatchEditingEnabled(this.MasterTableView))
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Grid.GridBatchEditingScripts.js", Assembly.GetExecutingAssembly().FullName));
			}
			return list;
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x00066EB0 File Offset: 0x000650B0
		private void AddScriptReference(List<ScriptReference> scriptReferences, string path)
		{
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				scriptReferences.Add(new ScriptReference(path.Replace(".js", ".Mobile.js"), Assembly.GetExecutingAssembly().FullName));
				return;
			}
			scriptReferences.Add(new ScriptReference(path.Replace(".js", ".Desktop.js"), Assembly.GetExecutingAssembly().FullName));
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00066F11 File Offset: 0x00065111
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.DescribeProperties(descriptor);
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00066F21 File Offset: 0x00065121
		internal static string ToLower(Match m)
		{
			return m.ToString().ToLower();
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00066F30 File Offset: 0x00065130
		internal static void ToggleColumnFilteredClass(Button filterImage, GridColumn column)
		{
			bool flag = column.ListOfFilterValues != null && column.ListOfFilterValues.Length > 0;
			bool flag2 = column.CurrentFilterFunction != GridKnownFunction.NoFilter && (!string.IsNullOrEmpty(column.CurrentFilterValue) || flag);
			bool flag3 = column.CurrentFilterFunction == GridKnownFunction.IsNull || column.CurrentFilterFunction == GridKnownFunction.IsEmpty || column.CurrentFilterFunction == GridKnownFunction.NotIsNull || column.CurrentFilterFunction == GridKnownFunction.NotIsEmpty;
			if (filterImage.CssClass.Contains(RadGrid.FilteredClassName))
			{
				if (!flag2)
				{
					filterImage.CssClass = filterImage.CssClass.Replace(RadGrid.FilteredClassName, string.Empty).Trim();
					return;
				}
			}
			else if (flag2 || flag3)
			{
				filterImage.CssClass = filterImage.CssClass + " " + RadGrid.FilteredClassName;
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00066FF4 File Offset: 0x000651F4
		internal static bool IsScreenBoundaryDetectionEnabled(GridTableView tableView)
		{
			if (tableView.EditFormSettings.PopUpSettings.KeepInScreenBounds)
			{
				return true;
			}
			foreach (GridTableView tableView2 in tableView.DetailTables)
			{
				if (RadGrid.IsScreenBoundaryDetectionEnabled(tableView2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00067064 File Offset: 0x00065264
		private void DescribeProperties(IScriptDescriptor descriptor)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new GridJavaScriptConverter()
			});
			if (this.ClientSettings.Virtualization.EnableVirtualization)
			{
				this.Page.ClientScript.GetCallbackEventReference(this, "arg", "Telerik.Web.UI.Grid.DummyCallbackReference", "");
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("_clientDataSourceID", control.ClientID);
					goto IL_BD;
				}
				catch (GridException)
				{
					descriptor.AddProperty("_clientDataSourceID", this.ClientDataSourceID);
					goto IL_BD;
				}
			}
			if (this.IsBoundUsingOData)
			{
				descriptor.AddScriptProperty("odataClientSettings", javaScriptSerializer.Serialize(ODataClientSettings.FromRadGridControl(this)));
			}
			IL_BD:
			descriptor.AddProperty("Skin", base.RuntimeSkin);
			string text = base.ResolveUrl(this.ImagesPath);
			if (text.LastIndexOf("/") != text.Length - 1)
			{
				text += "/";
			}
			if (!string.IsNullOrEmpty(text))
			{
				descriptor.AddProperty("_imagesPath", text);
			}
			descriptor.AddProperty("_embeddedSkin", this.EnableEmbeddedSkins);
			descriptor.AddProperty("ClientID", this.ClientID);
			descriptor.AddProperty("UniqueID", this.UniqueID);
			descriptor.AddProperty("_masterClientID", this.MasterTableView.ClientID);
			descriptor.AddProperty("allowMultiRowSelection", this.AllowMultiRowSelection);
			descriptor.AddProperty("_activeRowIndex", this.ClientSettings.ActiveRowIndex);
			descriptor.AddProperty("_currentPageIndex", this.CurrentPageIndex);
			descriptor.AddProperty("_shouldFocusOnPage", this.shouldFocusOnPage);
			descriptor.AddProperty("_defaultDateTimeFormat", CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern);
			descriptor.AddProperty("_freezeText", this.Localization.HeaderContextMenuFreeze);
			descriptor.AddProperty("_unfreezeText", this.Localization.HeaderContextMenuUnfreeze);
			if (this.FilterCheckList != null)
			{
				descriptor.AddProperty("_filterCheckListClientID", this.FilterCheckList.ClientID);
			}
			if (this.ShowStatusBar)
			{
				descriptor.AddProperty("_statusLabelID", this.StatusBarSettings.StatusLabelID);
				descriptor.AddProperty("_loadingText", this.StatusBarSettings.LoadingText);
				descriptor.AddProperty("_readyText", this.StatusBarSettings.ReadyText);
			}
			if (this.IsPopUpEnabled(this.MasterTableView))
			{
				descriptor.AddProperty("_popUpIds", javaScriptSerializer.Serialize(this._popUpIds));
				descriptor.AddScriptProperty("_popUpSettings", javaScriptSerializer.Serialize(this.MasterTableView.EditFormSettings.PopUpSettings));
			}
			descriptor.AddProperty("_editIndexes", javaScriptSerializer.Serialize(this.EditIndexes));
			descriptor.AddProperty("_controlToFocus", this._controlToFocus);
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.ClientSettings.ClientEvents);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!(propertyDescriptor.DisplayName == "ViewState"))
				{
					string text2 = propertyDescriptor.DisplayName.Replace("On", "");
					text2 = Regex.Replace(text2, "^[A-Z]", new MatchEvaluator(RadGrid.ToLower));
					string text3 = propertyDescriptor.GetValue(this.ClientSettings.ClientEvents).ToString();
					if (!string.IsNullOrEmpty(text3))
					{
						descriptor.AddEvent(text2, text3);
					}
				}
			}
			if (this.ShowGroupPanel && this.GroupingEnabled)
			{
				descriptor.AddProperty("ShowGroupPanel", this.ShowGroupPanel);
				descriptor.AddProperty("_groupPanelClientID", (this.ResolvedRenderMode == RenderMode.Lightweight) ? this.GroupPanel.ClientID : this.GroupPanel.WrappingTable.ClientID);
				descriptor.AddProperty("_groupPanelItems", this.GroupPanel.SerializeItemsToJavaScript());
				descriptor.AddProperty("_groupPanelText", this.GroupPanel.Text);
			}
			this.InitializeGridTableViewsRecursive(this.MasterTableView);
			descriptor.AddProperty("_gridTableViewsData", javaScriptSerializer.Serialize(this._gridTableViewsData));
			if (this.batchEditingOpenForEditEvents != null)
			{
				descriptor.AddScriptProperty("_batchEditingOpenForEditEvents", javaScriptSerializer.Serialize(this.batchEditingOpenForEditEvents));
			}
			descriptor.AddScriptProperty("ClientSettings", javaScriptSerializer.Serialize(this.ClientSettings));
			if (this.HierarchyColsExpandedState.Count > 0)
			{
				descriptor.AddProperty("_hierarchyColsExpandedState", this.HierarchyColsExpandedState);
			}
			if (this.ClientSettings.AllowKeyboardNavigation)
			{
				descriptor.AddProperty("ValidationSettings", new Dictionary<string, object>
				{
					{
						"EnableValidation",
						this.ValidationSettings.EnableValidation
					},
					{
						"ValidationGroup",
						this.ValidationSettings.ValidationGroup
					},
					{
						"CommandsToValidate",
						this.ValidationSettings.CommandsToValidate
					}
				});
			}
			if (this.IsSortingEnabled(this.MasterTableView))
			{
				descriptor.AddScriptProperty("SortingSettings", javaScriptSerializer.Serialize(this.SortingSettings));
			}
			if (this.SelectedIndexes.Count > 0)
			{
				List<IDictionary> list = new List<IDictionary>();
				foreach (object obj2 in this.SelectedIndexes)
				{
					string text4 = (string)obj2;
					GridItem gridItem = this.Items.FindByHierarchyIndex(text4);
					if (gridItem != null)
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						dictionary["itemIndex"] = text4;
						dictionary["id"] = string.Format("{0}__{1}", gridItem.OwnerTableView.ClientID, gridItem.ItemIndexHierarchical);
						list.Add(dictionary);
					}
				}
				descriptor.AddScriptProperty("selectedItemsInternal", javaScriptSerializer.Serialize(list));
			}
			if (this.lastSelectedItemIndex != -1)
			{
				descriptor.AddProperty("_lastSelectedItemIndex", this.lastSelectedItemIndex);
			}
			if (this.SelectedIndexes.Count > 0)
			{
				List<IDictionary> list2 = new List<IDictionary>();
				foreach (object obj3 in this.SelectedIndexes)
				{
					string text5 = (string)obj3;
					GridItem gridItem2 = this.Items.FindByHierarchyIndex(text5);
					if (gridItem2 != null)
					{
						Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
						dictionary2["itemIndex"] = text5;
						dictionary2["id"] = string.Format("{0}__{1}", gridItem2.OwnerTableView.ClientID, gridItem2.ItemIndexHierarchical);
						list2.Add(dictionary2);
					}
				}
				descriptor.AddScriptProperty("selectedItemsInternal", javaScriptSerializer.Serialize(list2));
			}
			if (this.SelectedCellIndexes.Count > 0)
			{
				List<IDictionary> list3 = new List<IDictionary>();
				foreach (object obj4 in this.SelectedCellIndexes)
				{
					string text6 = (string)obj4;
					GridItem gridItem3 = this.Items.FindByHierarchyIndex(text6.Substring(0, text6.IndexOf("&")));
					if (gridItem3 != null)
					{
						Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
						dictionary3["cellIndex"] = text6;
						dictionary3["id"] = string.Format("{0}__{1}", gridItem3.OwnerTableView.ClientID, text6);
						list3.Add(dictionary3);
					}
				}
				descriptor.AddScriptProperty("selectedCellsInternal", javaScriptSerializer.Serialize(list3));
			}
			if (this.Items.Count > 0)
			{
				Dictionary<string, bool> dictionary4 = new Dictionary<string, bool>();
				foreach (object obj5 in this.Items)
				{
					GridItem gridItem4 = (GridItem)obj5;
					if (gridItem4.Expanded)
					{
						dictionary4.Add(gridItem4.ItemIndexHierarchical, true);
					}
				}
				descriptor.AddScriptProperty("expandItems", javaScriptSerializer.Serialize(dictionary4));
			}
			if (this.ClientUnselectableIndexes.Count > 0)
			{
				List<IDictionary> list4 = new List<IDictionary>();
				foreach (object obj6 in this.ClientUnselectableIndexes)
				{
					string text7 = (string)obj6;
					GridItem gridItem5 = this.Items.FindByHierarchyIndex(text7);
					if (gridItem5 != null)
					{
						Dictionary<string, string> dictionary5 = new Dictionary<string, string>();
						dictionary5["itemIndex"] = text7;
						dictionary5["id"] = string.Format("{0}__{1}", gridItem5.OwnerTableView.ClientID, gridItem5.ItemIndexHierarchical);
						list4.Add(dictionary5);
					}
				}
				descriptor.AddScriptProperty("unselectableItemsInternal", javaScriptSerializer.Serialize(list4));
			}
			if (this.hiddenColumns != null)
			{
				List<string> list5 = new List<string>();
				foreach (string text8 in this.hiddenColumns)
				{
					string[] array2 = text8.Split(new char[]
					{
						','
					});
					string text9 = array2[0];
					string text10 = array2[1];
					GridTableView gridTableView = (GridTableView)this.Page.FindControl(text9);
					if (gridTableView != null && gridTableView != this.MasterTableView && gridTableView.Visible)
					{
						GridColumn column = gridTableView.GetColumn(text10);
						if (!column.Display && column.Visible)
						{
							list5.Add(string.Format("{0},{1}", text9, text10));
						}
					}
				}
				if (list5.Count > 0)
				{
					descriptor.AddScriptProperty("hidedColumns", javaScriptSerializer.Serialize(list5));
				}
			}
			if (this.showedColumns != null)
			{
				List<string> list6 = new List<string>();
				foreach (string text11 in this.showedColumns)
				{
					string[] array3 = text11.Split(new char[]
					{
						','
					});
					string text12 = array3[0];
					string text13 = array3[1];
					GridTableView gridTableView2 = (GridTableView)this.Page.FindControl(text12);
					if (gridTableView2 != null && gridTableView2 != this.MasterTableView && gridTableView2.Visible)
					{
						GridColumn column2 = gridTableView2.GetColumn(text13);
						if (column2.Display && column2.Visible)
						{
							list6.Add(string.Format("{0},{1}", text12, text13));
						}
					}
				}
				if (list6.Count > 0)
				{
					descriptor.AddScriptProperty("showedColumns", javaScriptSerializer.Serialize(list6));
				}
			}
			Dictionary<string, Dictionary<string, string>> dictionary6 = new Dictionary<string, Dictionary<string, string>>();
			foreach (object obj7 in this.Items)
			{
				GridDataItem gridDataItem = (GridDataItem)obj7;
				if (gridDataItem.Visible && gridDataItem.OwnerTableView.ClientDataKeyNames.Length != 0)
				{
					Dictionary<string, string> dictionary7 = new Dictionary<string, string>();
					for (int j = 0; j < gridDataItem.OwnerTableView.ClientDataKeyNames.Length; j++)
					{
						string text14 = gridDataItem.OwnerTableView.ClientDataKeyNames[j];
						string value = (gridDataItem.GetDataKeyValue(text14) != null) ? gridDataItem.GetDataKeyValue(text14).ToString() : null;
						dictionary7[text14] = value;
					}
					dictionary6[gridDataItem.ItemIndexHierarchical] = dictionary7;
				}
			}
			if (dictionary6.Count > 0)
			{
				descriptor.AddScriptProperty("_clientKeyValues", javaScriptSerializer.Serialize(dictionary6));
			}
			if (this.IsClientHierarchyEnabled(this.MasterTableView))
			{
				descriptor.AddScriptProperty("_hierarchySettings", javaScriptSerializer.Serialize(this.HierarchySettings));
			}
			if (this.IsClientGroupingEnabled(this.MasterTableView))
			{
				descriptor.AddScriptProperty("_groupingSettings", javaScriptSerializer.Serialize(this.GroupingSettings));
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				if (this.GroupingSettings.ShowUnGroupButton)
				{
					descriptor.AddProperty("_showUnGroupButton", true);
				}
				if (this.GroupingSettings.UnGroupTooltipSet)
				{
					descriptor.AddProperty("_unGroupTooltip", this.GroupingSettings.UnGroupTooltip);
				}
				if (this.GroupingSettings.UnGroupButtonTooltipSet)
				{
					descriptor.AddProperty("_unGroupButtonTooltip", this.GroupingSettings.UnGroupButtonTooltip);
				}
			}
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
			if (GridBatchEditingHelper.IsBatchEditingEnabled(this.MasterTableView))
			{
				descriptor.AddProperty("_isBatchEditingEnabled", true);
				bool flag = GridBatchEditingHelper.IsHighlightingForDeletedRowsEnabled(this.MasterTableView);
				if (flag)
				{
					descriptor.AddProperty("_rowHighlightingForDeletedRows", true);
				}
			}
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00067D74 File Offset: 0x00065F74
		internal bool IsClientHierarchyEnabled(GridTableView parentView)
		{
			bool result = parentView.HierarchyLoadMode == GridChildLoadMode.Client;
			if (parentView.HasDetailTables)
			{
				foreach (GridTableView parentView2 in parentView.DetailTables)
				{
					if (this.IsClientHierarchyEnabled(parentView2))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x00067DE4 File Offset: 0x00065FE4
		internal bool IsSortingEnabled(GridTableView parentView)
		{
			bool result = parentView.AllowSorting;
			if (parentView.HasDetailTables)
			{
				foreach (GridTableView parentView2 in parentView.DetailTables)
				{
					if (this.IsSortingEnabled(parentView2))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00067E50 File Offset: 0x00066050
		internal bool IsPopUpEnabled(GridTableView parentView)
		{
			bool result = parentView.EditMode == GridEditMode.PopUp;
			if (parentView.HasDetailTables)
			{
				foreach (GridTableView parentView2 in parentView.DetailTables)
				{
					if (this.IsPopUpEnabled(parentView2))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00067EC0 File Offset: 0x000660C0
		internal bool IsClientGroupingEnabled(GridTableView parentView)
		{
			bool result = parentView.GroupLoadMode == GridGroupLoadMode.Client || !string.IsNullOrEmpty(parentView.OwnerGrid.ClientDataSourceID);
			if (parentView.HasDetailTables)
			{
				foreach (GridTableView parentView2 in parentView.DetailTables)
				{
					if (this.IsClientGroupingEnabled(parentView2))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00067F44 File Offset: 0x00066144
		internal void InitializeGridTableViewsRecursive(GridTableView tableView)
		{
			this.InitializeGridTableViewData(tableView);
			GridItem[] items = tableView.GetItems(new GridItemType[]
			{
				GridItemType.NestedView
			});
			foreach (GridNestedViewItem gridNestedViewItem in items)
			{
				foreach (GridTableView gridTableView in gridNestedViewItem.NestedTableViews)
				{
					this.InitializeGridTableViewData(gridTableView);
					if (gridTableView.HasDetailTables)
					{
						this.InitializeGridTableViewsRecursive(gridTableView);
					}
				}
			}
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00067FC4 File Offset: 0x000661C4
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal void InitializeGridTableViewData(GridTableView tableView)
		{
			if (!tableView.Visible)
			{
				return;
			}
			Dictionary<string, object> data = new Dictionary<string, object>();
			this.InitializeDesktopGridTableViewData(tableView, data);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00067FE8 File Offset: 0x000661E8
		private void InitializeDesktopGridTableViewData(GridTableView tableView, Dictionary<string, object> data)
		{
			data.Add("ClientID", tableView.ClientID);
			data.Add("UniqueID", tableView.UniqueID);
			data.Add("PageSize", tableView.PageSize);
			data.Add("PageCount", tableView.PageCount);
			data.Add("EditMode", tableView.EditMode.ToString());
			data.Add("AllowPaging", tableView.AllowPaging);
			data.Add("CurrentPageIndex", tableView.CurrentPageIndex);
			data.Add("VirtualItemCount", tableView.VirtualItemCount);
			data.Add("AllowMultiColumnSorting", tableView.AllowMultiColumnSorting);
			data.Add("AllowNaturalSort", tableView.AllowNaturalSort);
			data.Add("AllowFilteringByColumn", tableView.AllowFilteringByColumn);
			int num = (tableView.PagerStyle.PageButtonCount != 10 && tableView.PagerStyle.PageButtonCount != tableView.OwnerGrid.PagerStyle.PageButtonCount) ? tableView.PagerStyle.PageButtonCount : tableView.OwnerGrid.PagerStyle.PageButtonCount;
			data.Add("PageButtonCount", num);
			data.Add("HasDetailTables", tableView.HasDetailTables);
			data.Add("HasMultiHeaders", tableView.HasMultiHeaders);
			data.Add("CheckListWebServicePath", tableView.CheckListWebServicePath);
			if (tableView.HasMultiHeaders)
			{
				data.Add("hiddenColumnHeaderSpans", tableView.hiddenColumnHeaderSpans);
			}
			if (this.ClientSettings.Virtualization.EnableVirtualization)
			{
				this.ClientSettings.Virtualization.ValidateTableViewLimitations(tableView);
				this.ClientSettings.Virtualization.ValidateProperties();
				data.Add("VirtualizationDataAsJSON", tableView.GetJsonData(0, this.ClientSettings.Virtualization.InitiallyCachedItemsCount));
				if (string.IsNullOrEmpty(this.ClientDataSourceID))
				{
					if (tableView.PagingManager != null)
					{
						data.Add("TotalItemsCount", tableView.PagingManager.DataSourceCount);
					}
					else
					{
						data.Add("TotalItemsCount", tableView.VirtualItemCount);
					}
				}
				if (!string.IsNullOrEmpty(this.ClientSettings.Virtualization.LoadingPanelID))
				{
					Control control = ChildControlHelper.FindControlRecursive(this, this.ClientSettings.Virtualization.LoadingPanelID, null);
					if (control == null)
					{
						throw new GridException(string.Format("A RadAjaxLoadingPanel with ID of {0} was not found. Please ensure the control exists.", this.ClientSettings.Virtualization.LoadingPanelID));
					}
					if (!(control is RadAjaxLoadingPanel))
					{
						throw new GridException("The LoadingPanelID property should point to the ID of RadAjaxLoadingPanel control. It currently points to a control of type " + control.GetType().ToString());
					}
					data.Add("LoadingPanelID", control.ClientID);
				}
			}
			if (tableView.GroupByExpressions.Count > 0 || !string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				data.Add("GroupLevelsCount", tableView.GroupByExpressions.Count.ToString());
				data.Add("EnableGroupsExpandAll", tableView.EnableGroupsExpandAll);
				data.Add("GroupHeadersCount", tableView.GetGroupHeadersCountForAllLevels());
				if (!string.IsNullOrEmpty(this.ClientDataSourceID))
				{
					if (tableView.ShowGroupFooter)
					{
						data.Add("ShowGroupFooter", tableView.ShowGroupFooter);
					}
					List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
					foreach (GridColumn gridColumn in tableView.RenderColumns)
					{
						GridBoundColumn gridBoundColumn = gridColumn as GridBoundColumn;
						if (gridBoundColumn != null && gridBoundColumn.Aggregate != GridAggregateFunction.None)
						{
							list.Add(new Dictionary<string, string>
							{
								{
									"field",
									gridBoundColumn.UniqueName
								},
								{
									"aggregate",
									gridBoundColumn.Aggregate.ToString().ToLower()
								}
							});
						}
					}
					if (list.Count > 0)
					{
						data.Add("Aggregates", list);
					}
					List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
					foreach (GridGroupByExpression gridGroupByExpression in tableView.GroupByExpressions)
					{
						foreach (object obj in gridGroupByExpression.SelectFields)
						{
							GridGroupByField gridGroupByField = (GridGroupByField)obj;
							Dictionary<string, object> dictionary = new Dictionary<string, object>();
							dictionary.Add("field", gridGroupByField.FieldName);
							dictionary.Add("alias", gridGroupByField.FieldAlias);
							dictionary.Add("dir", (gridGroupByField.SortOrder == GridSortOrder.Ascending) ? "asc" : "desc");
							if (list.Count > 0)
							{
								dictionary.Add("aggregates", list);
							}
							list2.Add(dictionary);
						}
					}
					data.Add("GroupByExpressions", list2);
				}
				else if (this.ResolvedRenderMode == RenderMode.Mobile)
				{
					List<Dictionary<string, object>> list3 = new List<Dictionary<string, object>>();
					foreach (GridGroupByExpression gridGroupByExpression2 in tableView.GroupByExpressions)
					{
						foreach (object obj2 in gridGroupByExpression2.SelectFields)
						{
							GridGroupByField gridGroupByField2 = (GridGroupByField)obj2;
							list3.Add(new Dictionary<string, object>
							{
								{
									"field",
									gridGroupByField2.FieldName
								}
							});
						}
					}
					data.Add("GroupByExpressions", list3);
				}
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
				foreach (object obj3 in tableView.Columns)
				{
					GridColumn gridColumn2 = (GridColumn)obj3;
					GridBoundColumn gridBoundColumn2 = gridColumn2 as GridBoundColumn;
					if (gridBoundColumn2 != null && !dictionary2.ContainsKey(gridBoundColumn2.DataField))
					{
						dictionary2.Add(gridBoundColumn2.DataField, gridBoundColumn2.HeaderText);
					}
				}
				data.Add("DataFieldHeaderText", dictionary2);
			}
			if (tableView.HierarchyLoadMode != GridChildLoadMode.ServerOnDemand)
			{
				data.Add("HierarchyLoadMode", tableView.HierarchyLoadMode.ToString());
			}
			if (tableView.GroupLoadMode == GridGroupLoadMode.Server)
			{
				data.Add("GroupLoadMode", tableView.GroupLoadMode.ToString());
			}
			if (tableView.HierarchyLoadMode == GridChildLoadMode.Client || tableView.HierarchyLoadMode == GridChildLoadMode.Conditional)
			{
				data.Add("EnableHierarchyExpandAll", tableView.EnableHierarchyExpandAll);
			}
			data.Add("PagerAlwaysVisible", tableView.PagerStyle.AlwaysVisible);
			if (tableView.PagerStyle.Mode == GridPagerMode.Slider || tableView.OwnerGrid.PagerStyle.Mode == GridPagerMode.Slider)
			{
				data.Add("sliderClientID", tableView.sliderClientID);
				data.Add("sliderLabelClientID", tableView.sliderLabelClientID);
				if (this.IsClientCommandAssigned)
				{
					data.Add("sliderTopClientID", tableView.sliderTopClientID);
					data.Add("sliderTopLabelClientID", tableView.sliderTopLabelClientID);
				}
			}
			if (tableView.PagerStyle.Mode == GridPagerMode.Advanced || tableView.OwnerGrid.PagerStyle.Mode == GridPagerMode.Advanced || tableView.PagerStyle.Mode == GridPagerMode.NextPrevNumericAndAdvanced || tableView.OwnerGrid.PagerStyle.Mode == GridPagerMode.NextPrevNumericAndAdvanced)
			{
				data.Add("goToPageTextBoxClientID", tableView.goToPageTextBoxClientID);
				data.Add("changePageSizeTextBoxClientID", tableView.changePageSizeTextBoxClientID);
				if (this.IsClientCommandAssigned)
				{
					data.Add("goToPageTextBoxTopClientID", tableView.goToPageTextBoxTopClientID);
					data.Add("changePageSizeTextBoxTopClientID", tableView.changePageSizeTextBoxTopClientID);
					data.Add("pageOfLabelTopClientID", tableView.pageOfLabelTopClientID);
					data.Add("pageOfLabelClientID", tableView.pageOfLabelClientID);
				}
			}
			if (tableView.PagerStyle.Mode == GridPagerMode.NextPrevAndNumeric && this.IsClientCommandAssigned)
			{
				data.Add("changePageSizeComboBoxTopClientID", tableView.changePageSizeComboBoxTopClientID);
				data.Add("changePageSizeComboBoxClientID", tableView.changePageSizeComboBoxClientID);
			}
			if (!string.IsNullOrEmpty(tableView.Name))
			{
				data.Add("Name", tableView.Name);
			}
			data.Add("IsItemInserted", tableView.IsItemInserted);
			data.Add("clientDataKeyNames", tableView.ClientDataKeyNames);
			data.Add("hasDetailItemTemplate", tableView.DetailItemTemplate != null);
			if (tableView.EnableHeaderContextMenu)
			{
				data.Add("enableHeaderContextMenu", tableView.EnableHeaderContextMenu);
				if (tableView.EnableHeaderContextFilterMenu)
				{
					data.Add("enableHeaderContextFilterMenu", tableView.EnableHeaderContextFilterMenu);
				}
				if (tableView.EnableHeaderContextAggregatesMenu)
				{
					data.Add("enableHeaderContextAggregatesMenu", tableView.EnableHeaderContextAggregatesMenu);
				}
			}
			data.Add("_dataBindTemplates", tableView.EditFormSettings.EditFormType != GridEditFormType.AutoGenerated || tableView.ItemTemplate != null || tableView.EditItemTemplate != null);
			if (!base.EmptySkin())
			{
				this.ActiveItemStyle.CssClass = HttpUtility.HtmlEncode(this.FormatCssClass("rgActiveRow", this.ActiveItemStyle.CssClass));
				this.SelectedItemStyle.CssClass = HttpUtility.HtmlEncode(this.FormatCssClass("rgSelectedRow", this.SelectedItemStyle.CssClass));
				this.MasterTableView.SelectedItemStyle.CssClass = HttpUtility.HtmlEncode(this.FormatCssClass("rgSelectedRow", this.MasterTableView.SelectedItemStyle.CssClass));
			}
			this.SerializeStyle("_selectedItemStyle", tableView.SelectedItemStyle, data);
			data.Add("_selectedItemStyleClass", tableView.SelectedItemStyle.CssClass);
			if (tableView.OwnerGrid.ClientSettings.AllowKeyboardNavigation)
			{
				this.SerializeStyle("_renderActiveItemStyle", tableView.RenderActiveItemStyle, data);
				data.Add("_renderActiveItemStyleClass", tableView.RenderActiveItemStyle.CssClass);
			}
			if (this.ClientSettings.IsFilteringEnabled(tableView))
			{
				GridItem[] items = this.MasterTableView.GetItems(new GridItemType[]
				{
					GridItemType.FilteringItem
				});
				if (items.Length > 0)
				{
					data.Add("isFilterItemExpanded", ((GridFilteringItem)items[0]).Expanded);
				}
			}
			if (tableView.EditMode == GridEditMode.Batch)
			{
				if (this.batchEditingOpenForEditEvents == null)
				{
					this.batchEditingOpenForEditEvents = new HashSet<string>();
				}
				this.batchEditingOpenForEditEvents.Add(tableView.BatchEditingSettings.OpenEditingEvent.ToString().ToLower());
				Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
				if (tableView.BatchEditingSettings.SaveAllHierarchyLevels)
				{
					dictionary3.Add("saveAll", tableView.BatchEditingSettings.SaveAllHierarchyLevels);
				}
				if (tableView.BatchEditingSettings.HighlightDeletedRows)
				{
					dictionary3.Add("highlightDeletedRows", tableView.BatchEditingSettings.HighlightDeletedRows);
				}
				dictionary3.Add("editType", tableView.BatchEditingSettings.EditType.ToString());
				dictionary3.Add("eventType", tableView.BatchEditingSettings.OpenEditingEvent.ToString().ToLower());
				dictionary3.Add("insertItemDisplay", tableView.InsertItemDisplay.ToString());
				data.Add("_batchEditingSettings", dictionary3);
			}
			List<IDictionary> list4 = new List<IDictionary>();
			foreach (GridColumn gridColumn3 in tableView.RenderColumns)
			{
				if (gridColumn3.Visible)
				{
					Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
					GridEditableColumn gridEditableColumn = gridColumn3 as GridEditableColumn;
					dictionary4.Add("UniqueName", gridColumn3.UniqueName);
					dictionary4.Add("Resizable", gridColumn3.Resizable);
					dictionary4.Add("Reorderable", gridColumn3.Reorderable);
					dictionary4.Add("Selectable", gridColumn3.Selectable);
					dictionary4.Add("Groupable", gridColumn3.Groupable);
					dictionary4.Add("ColumnType", gridColumn3.ColumnType);
					if (!gridColumn3.EnableHeaderContextMenu)
					{
						dictionary4.Add("EnableHeaderContextMenu", gridColumn3.EnableHeaderContextMenu);
					}
					dictionary4.Add("ColumnGroupName", gridColumn3.ColumnGroupName);
					dictionary4.Add("Editable", GridBatchEditingHelper.IsColumnEditable(gridColumn3));
					if (this.ResolvedRenderMode == RenderMode.Mobile && !string.IsNullOrEmpty(gridColumn3.HeaderText))
					{
						dictionary4.Add("HeaderText", gridColumn3.HeaderText);
					}
					if (gridEditableColumn != null && tableView.EditMode == GridEditMode.Batch)
					{
						dictionary4.Add("InsertVisiblityMode", gridEditableColumn.InsertVisiblityMode.ToString());
						if (!string.IsNullOrEmpty(gridEditableColumn.DefaultInsertValue))
						{
							dictionary4.Add("DefaultInsertValue", gridEditableColumn.DefaultInsertValue);
						}
					}
					if (this.ClientSettings.Selecting.CellSelectionMode != GridCellSelectionMode.None)
					{
						dictionary4.Add("SelectedCellsCount", gridColumn3.SelectedCellsCount);
					}
					string sortExpression = gridColumn3.GetSortExpression();
					if (this.AllowSorting && !string.IsNullOrEmpty(sortExpression))
					{
						dictionary4.Add("SortExpression", sortExpression);
					}
					dictionary4.Add("DataTypeName", gridColumn3.DataTypeName);
					if (this.ClientSettings.IsFilteringEnabled(this.MasterTableView) && gridColumn3.Owner.AllowFilteringByColumn)
					{
						dictionary4.Add("FilterListOptions", gridColumn3.FilterListOptions);
						dictionary4.Add("CurrentFilterFunction", gridColumn3.CurrentFilterFunction);
						dictionary4.Add("CurrentFilterValue", gridColumn3.CurrentFilterValue);
						dictionary4.Add("AndCurrnetFilterFunction", gridColumn3.AndCurrentFilterFunction);
						dictionary4.Add("AndCurrentFilterValue", gridColumn3.AndCurrentFilterValue);
						if (gridColumn3.AutoPostBackFilterFunction != GridKnownFunction.NoFilter)
						{
							dictionary4.Add("Acff", Enum.GetName(gridColumn3.AutoPostBackFilterFunction.GetType(), gridColumn3.AutoPostBackFilterFunction));
						}
						if (!string.IsNullOrEmpty(gridColumn3.FilterCheckListWebServiceMethod))
						{
							dictionary4.Add("FilterCheckListWebServiceMethod", gridColumn3.FilterCheckListWebServiceMethod);
						}
						if (gridColumn3.FilterCheckListEnableLoadOnDemand)
						{
							dictionary4.Add("FilterCheckListEnableLoadOnDemand", gridColumn3.FilterCheckListEnableLoadOnDemand);
						}
						if (gridColumn3.ListOfFilterValues != null && gridColumn3.ListOfFilterValues.Length > 0)
						{
							dictionary4.Add("ListOfFilterValues", gridColumn3.ListOfFilterValues);
						}
						if (gridColumn3.FilterDelay != null)
						{
							dictionary4.Add("FilterDelay", gridColumn3.FilterDelay);
						}
						dictionary4.Add("CurrentFilterFunctionName", Enum.GetName(gridColumn3.CurrentFilterFunction.GetType(), gridColumn3.CurrentFilterFunction));
						dictionary4.Add("AndCurrentFilterFunctionName", Enum.GetName(gridColumn3.AndCurrentFilterFunction.GetType(), gridColumn3.AndCurrentFilterFunction));
					}
					this.SerializeColumnDataForContextFilterMenu(gridColumn3, tableView, dictionary4);
					if (gridColumn3 is GridBoundColumn)
					{
						GridBoundColumn gridBoundColumn3 = (GridBoundColumn)gridColumn3;
						dictionary4.Add("DataField", gridBoundColumn3.DataField);
						if (gridBoundColumn3.Aggregate != GridAggregateFunction.None)
						{
							dictionary4.Add("Aggregate", gridBoundColumn3.Aggregate);
							string @string = this.Localization.GetString("AggregateFunction" + gridBoundColumn3.Aggregate.ToString());
							string footerText = string.IsNullOrEmpty(gridBoundColumn3.FooterText) ? string.Format("{0} : ", @string) : gridBoundColumn3.FooterText;
							if (!string.IsNullOrEmpty(gridBoundColumn3.FooterAggregateFormatString) || !string.IsNullOrEmpty(gridBoundColumn3.DataFormatString))
							{
								footerText = "";
							}
							dictionary4.Add("AggregateClientFormatString", gridBoundColumn3.FormatCellText(footerText, "{0}"));
						}
						if (!string.IsNullOrEmpty(gridBoundColumn3.DataFormatString))
						{
							dictionary4.Add("DataFormatString", gridBoundColumn3.DataFormatString);
						}
						if (gridBoundColumn3.ReadOnly)
						{
							dictionary4.Add("ReadOnly", gridBoundColumn3.ReadOnly);
						}
					}
					else if (gridColumn3 is GridButtonColumn)
					{
						GridButtonColumn gridButtonColumn = gridColumn3 as GridButtonColumn;
						dictionary4.Add("ButtonType", gridButtonColumn.ButtonType.ToString());
						dictionary4.Add("CommandName", gridButtonColumn.CommandName);
						dictionary4.Add("CommandArgument", gridButtonColumn.CommandArgument);
						dictionary4.Add("Text", gridButtonColumn.Text);
						dictionary4.Add("DataTextField", gridButtonColumn.DataTextField);
						dictionary4.Add("DataTextFormatString", gridButtonColumn.DataTextFormatString);
						string text = "";
						if (HttpContext.Current != null && HttpContext.Current.Response != null)
						{
							if (string.IsNullOrEmpty(gridButtonColumn.ImageUrl))
							{
								if (gridButtonColumn.CommandName == "Delete")
								{
									text = this.ResolveGridImageUrl("Delete.gif", false);
								}
								else if (gridButtonColumn.CommandName == "Edit")
								{
									text = this.ResolveGridImageUrl("Edit.gif", false);
								}
							}
							else
							{
								text = gridButtonColumn.ImageUrl;
							}
							text = HttpContext.Current.Response.ApplyAppPathModifier(text);
						}
						dictionary4.Add("ImageUrl", text);
					}
					else if (gridColumn3 is GridEditCommandColumn)
					{
						GridEditCommandColumn gridEditCommandColumn = gridColumn3 as GridEditCommandColumn;
						dictionary4.Add("CommandName", "Edit");
						dictionary4.Add("Text", gridEditCommandColumn.EditText);
						dictionary4.Add("ButtonType", gridEditCommandColumn.ButtonType.ToString());
						string text2 = "";
						if (HttpContext.Current != null && HttpContext.Current.Response != null)
						{
							if (string.IsNullOrEmpty(gridEditCommandColumn.EditImageUrl))
							{
								text2 = this.ResolveGridImageUrl("Edit.gif", false);
							}
							else
							{
								text2 = gridEditCommandColumn.EditImageUrl;
							}
							text2 = HttpContext.Current.Response.ApplyAppPathModifier(text2);
						}
						dictionary4.Add("ImageUrl", text2);
					}
					else if (gridColumn3 is GridTemplateColumn)
					{
						dictionary4.Add("DataField", ((GridTemplateColumn)gridColumn3).DataField);
						if (((GridTemplateColumn)gridColumn3).ReadOnly)
						{
							dictionary4.Add("ReadOnly", ((GridTemplateColumn)gridColumn3).ReadOnly);
						}
						if (!string.IsNullOrEmpty(((GridTemplateColumn)gridColumn3).ClientItemTemplate))
						{
							dictionary4.Add("ClientItemTemplate", ((GridTemplateColumn)gridColumn3).ClientItemTemplate);
						}
					}
					else if (gridColumn3 is GridCheckBoxColumn)
					{
						dictionary4.Add("DataField", ((GridCheckBoxColumn)gridColumn3).DataField);
					}
					else if (gridColumn3 is GridDropDownColumn)
					{
						GridDropDownColumn gridDropDownColumn = (GridDropDownColumn)gridColumn3;
						dictionary4.Add("DataField", gridDropDownColumn.DataField);
						if (gridDropDownColumn.EnableEmptyListItem)
						{
							dictionary4.Add("enableEmptyListItem", gridDropDownColumn.EnableEmptyListItem);
						}
					}
					else if (gridColumn3 is GridAutoCompleteColumn)
					{
						dictionary4.Add("DataField", ((GridAutoCompleteColumn)gridColumn3).DataField);
					}
					else if (gridColumn3 is GridCalculatedColumn)
					{
						dictionary4.Add("DataFields", ((GridCalculatedColumn)gridColumn3).DataFields);
						dictionary4.Add("Expression", ((GridCalculatedColumn)gridColumn3).Expression);
						dictionary4.Add("DataFormatString", ((GridCalculatedColumn)gridColumn3).DataFormatString);
					}
					else if (gridColumn3 is GridHyperLinkColumn)
					{
						GridHyperLinkColumn gridHyperLinkColumn = (GridHyperLinkColumn)gridColumn3;
						dictionary4.Add("DataTextField", gridHyperLinkColumn.DataTextField);
						dictionary4.Add("DataTextFormatString", gridHyperLinkColumn.DataTextFormatString);
						dictionary4.Add("DataNavigateUrlFields", gridHyperLinkColumn.DataNavigateUrlFields);
						if (!string.IsNullOrEmpty(gridHyperLinkColumn.NavigateUrl) && HttpContext.Current != null && HttpContext.Current.Response != null)
						{
							dictionary4.Add("NavigateUrl", HttpContext.Current.Response.ApplyAppPathModifier(gridHyperLinkColumn.NavigateUrl));
						}
						if (!string.IsNullOrEmpty(gridHyperLinkColumn.Target))
						{
							dictionary4.Add("Target", gridHyperLinkColumn.Target);
						}
						if (!string.IsNullOrEmpty(gridHyperLinkColumn.Text))
						{
							dictionary4.Add("Text", gridHyperLinkColumn.Text);
						}
						string value = "";
						if (HttpContext.Current != null && HttpContext.Current.Response != null)
						{
							value = HttpContext.Current.Response.ApplyAppPathModifier(gridHyperLinkColumn.DataNavigateUrlFormatString);
						}
						dictionary4.Add("DataNavigateUrlFormatString", value);
					}
					else if (gridColumn3 is GridImageColumn)
					{
						GridImageColumn gridImageColumn = (GridImageColumn)gridColumn3;
						dictionary4.Add("DataAlternateTextField", gridImageColumn.DataAlternateTextField);
						dictionary4.Add("DataAlternateTextFormatString", gridImageColumn.DataAlternateTextFormatString);
						dictionary4.Add("DataImageUrlFields", gridImageColumn.DataImageUrlFields);
						if (!gridImageColumn.ImageHeight.IsEmpty)
						{
							dictionary4.Add("ImageHeight", gridImageColumn.ImageHeight.ToString());
						}
						if (!gridImageColumn.ImageWidth.IsEmpty)
						{
							dictionary4.Add("ImageWidth", gridImageColumn.ImageWidth.ToString());
						}
						if (!string.IsNullOrEmpty(gridImageColumn.AlternateText))
						{
							dictionary4.Add("AlternateText", gridImageColumn.AlternateText);
						}
						if (HttpContext.Current != null && HttpContext.Current.Response != null && !string.IsNullOrEmpty(gridImageColumn.ImageUrl))
						{
							dictionary4.Add("ImageUrl", HttpContext.Current.Response.ApplyAppPathModifier(gridImageColumn.ImageUrl));
						}
						string value2 = "";
						if (HttpContext.Current != null && HttpContext.Current.Response != null)
						{
							value2 = HttpContext.Current.Response.ApplyAppPathModifier(gridImageColumn.DataImageUrlFormatString);
						}
						dictionary4.Add("DataImageUrlFormatString", value2);
					}
					else if (gridColumn3 is GridBinaryImageColumn)
					{
						dictionary4.Add("DataAlternateTextField", ((GridBinaryImageColumn)gridColumn3).DataAlternateTextField);
						dictionary4.Add("DataAlternateTextFormatString", ((GridBinaryImageColumn)gridColumn3).DataAlternateTextFormatString);
					}
					else if (gridColumn3 is GridAttachmentColumn)
					{
						GridAttachmentColumn gridAttachmentColumn = (GridAttachmentColumn)gridColumn3;
						dictionary4.Add("DataTextField", gridAttachmentColumn.DataTextField);
						dictionary4.Add("DataTextFormatString", gridAttachmentColumn.FileNameTextFormatString);
						dictionary4.Add("FileNameTextField", gridAttachmentColumn.FileNameTextField);
						dictionary4.Add("FileNameTextFormatString", gridAttachmentColumn.FileNameTextFormatString);
					}
					else if (gridColumn3 is GridRatingColumn)
					{
						dictionary4.Add("DataField", ((GridRatingColumn)gridColumn3).DataField);
					}
					if (gridColumn3 is GridDateTimeColumn)
					{
						GridDateTimeColumn gridDateTimeColumn = gridColumn3 as GridDateTimeColumn;
						if (this.ResolvedRenderMode == RenderMode.Mobile && !dictionary4.ContainsKey("PickerType"))
						{
							dictionary4.Add("PickerType", gridDateTimeColumn.PickerType.ToString());
						}
						dictionary4.Add("EnableRangeFiltering", gridDateTimeColumn.EnableRangeFiltering);
						if (!string.IsNullOrEmpty(gridDateTimeColumn.DataFormatString))
						{
							string text3 = Regex.Replace(gridDateTimeColumn.DataFormatString, "[^a-zA-Z]+", "");
							if (text3 != InputUtil.MapDateFormatShortCuts(text3, this.Culture.DateTimeFormat))
							{
								dictionary4["DataFormatString"] = string.Format("{{0:{0}}}", InputUtil.MapDateFormatShortCuts(text3, this.Culture.DateTimeFormat));
							}
						}
					}
					if ((gridColumn3.Owner.HierarchyLoadMode == GridChildLoadMode.Client || gridColumn3.Owner.HierarchyLoadMode == GridChildLoadMode.Conditional) && gridColumn3 is GridExpandColumn)
					{
						dictionary4.Add("ExpandImageUrl", ((GridExpandColumn)gridColumn3).ExpandImageUrl);
						dictionary4.Add("CollapseImageUrl", ((GridExpandColumn)gridColumn3).CollapseImageUrl);
					}
					if (gridColumn3.Owner.GroupLoadMode == GridGroupLoadMode.Client && gridColumn3 is GridGroupSplitterColumn)
					{
						GridGroupSplitterColumn gridGroupSplitterColumn = gridColumn3 as GridGroupSplitterColumn;
						dictionary4.Add("ExpandImageUrl", gridGroupSplitterColumn.ExpandImageUrl);
						dictionary4.Add("CollapseImageUrl", gridGroupSplitterColumn.CollapseImageUrl);
					}
					dictionary4.Add("Display", gridColumn3.Display);
					list4.Add(dictionary4);
					if (gridColumn3 is GridDragDropColumn)
					{
						data["_useDragColumn"] = true;
					}
				}
			}
			data.Add("_columnsData", list4);
			this._gridTableViewsData.Add(data);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000698A0 File Offset: 0x00067AA0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void SerializeColumnDataForContextFilterMenu(GridColumn column, GridTableView tableView, Dictionary<string, object> _columnData)
		{
			if (tableView.EnableHeaderContextFilterMenu)
			{
				_columnData.Add("AllowFiltering", column.SupportsFiltering());
				if (!column.FilterControlWidth.IsEmpty)
				{
					_columnData.Add("FilterControlWidth", column.FilterControlWidth);
				}
				if (column is GridDateTimeColumn)
				{
					CultureInfo provider = new CultureInfo("en-US");
					_columnData.Add("PickerType", Enum.GetName(typeof(GridDateTimeColumnPickerType), ((GridDateTimeColumn)column).PickerType));
					_columnData.Add("MinDate", ((GridDateTimeColumn)column).MinDate.ToString(provider));
					_columnData.Add("MaxDate", ((GridDateTimeColumn)column).MaxDate.ToString(provider));
					return;
				}
				if (column is GridNumericColumn)
				{
					RadNumericTextBox filterNumericBoxFirstCondition = tableView.OwnerGrid.HeaderContextMenu.FilterNumericBoxFirstCondition;
					if (filterNumericBoxFirstCondition != null)
					{
						string value = string.Empty;
						string value2 = string.Empty;
						string numericPlaceHolder = filterNumericBoxFirstCondition.NumberFormat.NumericPlaceHolder;
						switch (((GridNumericColumn)column).NumericType)
						{
						case NumericType.Currency:
							value2 = InputUtil.ToStringCurrencyPositivePattern(filterNumericBoxFirstCondition.Culture, numericPlaceHolder);
							break;
						case NumericType.Percent:
							value2 = InputUtil.ToStringPercentPositivePattern(filterNumericBoxFirstCondition.Culture, numericPlaceHolder);
							break;
						default:
							value2 = InputUtil.ToStringNumberPositivePattern(numericPlaceHolder);
							break;
						}
						switch (((GridNumericColumn)column).NumericType)
						{
						case NumericType.Currency:
							value = InputUtil.ToStringCurrencyNegativePattern(filterNumericBoxFirstCondition.Culture, numericPlaceHolder);
							break;
						case NumericType.Percent:
							value = InputUtil.ToStringPercentNegativePattern(filterNumericBoxFirstCondition.Culture, numericPlaceHolder);
							break;
						default:
							value = InputUtil.ToStringNumberNegativePattern(filterNumericBoxFirstCondition.Culture, numericPlaceHolder);
							break;
						}
						if (!string.IsNullOrEmpty(value2))
						{
							_columnData.Add("PositivePattern", value2);
						}
						if (!string.IsNullOrEmpty(value))
						{
							_columnData.Add("NegativePattern", value);
						}
						_columnData.Add("AllowRounding", ((GridNumericColumn)column).AllowRounding);
						_columnData.Add("KeepNotRoundedValue", ((GridNumericColumn)column).KeepNotRoundedValue);
						return;
					}
				}
				else if (column is GridMaskedColumn)
				{
					MaskParser maskParser = new MaskParser();
					MaskPartCollection maskPartCollection = maskParser.Parse(((GridMaskedColumn)column).Mask);
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < maskPartCollection.Count; i++)
					{
						stringBuilder.Append(maskPartCollection[i].InitScript);
						if (i < maskPartCollection.Count - 1)
						{
							stringBuilder.Append(",");
						}
					}
					if (!string.IsNullOrEmpty(stringBuilder.ToString()))
					{
						_columnData.Add("Mask", stringBuilder.ToString());
					}
				}
			}
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00069B36 File Offset: 0x00067D36
		public void ExportToExcel()
		{
			this.MasterTableView.ExportToExcel();
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00069B43 File Offset: 0x00067D43
		public void ExportToExcel(GridExcelExportFormat format)
		{
			this.ExportSettings.Excel.Format = format;
			this.ExportToExcel();
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00069B5C File Offset: 0x00067D5C
		public void ExportToWord()
		{
			this.MasterTableView.ExportToWord();
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00069B69 File Offset: 0x00067D69
		public void ExportToWord(GridWordExportFormat format)
		{
			this.ExportSettings.Word.Format = format;
			this.ExportToWord();
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00069B82 File Offset: 0x00067D82
		public void ExportToPdf()
		{
			this.MasterTableView.ExportToPdf();
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00069B8F File Offset: 0x00067D8F
		public void ExportToCsv()
		{
			this.MasterTableView.ExportToCSV();
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00069B9C File Offset: 0x00067D9C
		public void SerializeStyle(string name, Style value, Dictionary<string, object> data)
		{
			string value2 = "";
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			value.AddAttributesToRender(htmlTextWriter);
			htmlTextWriter.RenderBeginTag("");
			htmlTextWriter.RenderEndTag();
			string text = stringWriter.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.IndexOf("style=\"");
				if (num >= 0)
				{
					num += 7;
					int num2 = text.IndexOf("\"", num);
					value2 = text.Substring(num, num2 - num);
				}
				num = text.IndexOf("class=\"");
				if (num >= 0)
				{
					num += 7;
					int num3 = text.IndexOf("\"", num);
					text.Substring(num, num3 - num);
				}
			}
			data.Add(name, value2);
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00069C55 File Offset: 0x00067E55
		protected override void RenderContents(HtmlTextWriter writer)
		{
			BaseClass.RenderVersionStamp(writer);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			base.RenderContents(writer);
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00069C78 File Offset: 0x00067E78
		protected override void Render(HtmlTextWriter writer)
		{
			RadGrid.RadListBoxShared radListBoxShared = this.FilterCheckList as RadGrid.RadListBoxShared;
			if (radListBoxShared != null)
			{
				radListBoxShared.Visible = false;
			}
			if (this.MasterTableView != null)
			{
				this.SetStyleClasses();
				this.PrepareRows(this.MasterTableView);
				this.PrepareRowsRecursive(this.MasterTableView);
			}
			if (this.Visible)
			{
				writer.WriteLine(this.RenderBeginTagWithAttributes(this));
				base.Render(writer);
				writer.WriteLine("\t</div>");
			}
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00069CE8 File Offset: 0x00067EE8
		private string RenderBeginTagWithAttributes(WebControl control)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.ID = control.ClientID;
			webControl.CopyBaseAttributes(control);
			webControl.Enabled = true;
			RadGrid radGrid = control as RadGrid;
			if (!string.IsNullOrEmpty(radGrid.BackImageUrl))
			{
				webControl.Style["background-image"] = string.Format("url({0});", radGrid.BackImageUrl);
			}
			webControl.ApplyStyle(control.ControlStyle);
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(new StringWriter());
			webControl.RenderBeginTag(htmlTextWriter);
			return htmlTextWriter.InnerWriter.ToString();
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00069D74 File Offset: 0x00067F74
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			bool result = false;
			if (e is GridCommandEventArgs)
			{
				GridCommandEventArgs e2 = (GridCommandEventArgs)e;
				this.OnItemCommand(e2);
				result = true;
			}
			if (e is IGridCommandEvent)
			{
				IGridCommandEvent gridCommandEvent = (IGridCommandEvent)e;
				if (!gridCommandEvent.Canceled)
				{
					gridCommandEvent.ExecuteCommand(source);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x00069DBC File Offset: 0x00067FBC
		internal void CallOnBatchEditCommand(GridBatchEditingEventArgs e)
		{
			this.OnBatchEditCommand(e);
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x00069DC8 File Offset: 0x00067FC8
		protected virtual void OnBatchEditCommand(GridBatchEditingEventArgs e)
		{
			GridBatchEditEventHandler gridBatchEditEventHandler = (GridBatchEditEventHandler)base.Events["BatchEdit"];
			if (gridBatchEditEventHandler != null)
			{
				gridBatchEditEventHandler(this, e);
			}
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x00069DF6 File Offset: 0x00067FF6
		internal void CallOnCancelCommand(GridCommandEventArgs e)
		{
			this.OnCancelCommand(e);
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00069E00 File Offset: 0x00068000
		protected virtual void OnCancelCommand(GridCommandEventArgs e)
		{
			GridCommandEventHandler gridCommandEventHandler = (GridCommandEventHandler)base.Events[RadGrid.EventCancelCommand];
			if (gridCommandEventHandler != null)
			{
				gridCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x00069E2E File Offset: 0x0006802E
		internal void CallOnCreateColumnEditor(GridCreateColumnEditorEventArgs e)
		{
			this.OnCreateColumnEditor(e);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00069E38 File Offset: 0x00068038
		protected virtual void OnCreateColumnEditor(GridCreateColumnEditorEventArgs e)
		{
			GridCreateColumnEditorEventHandler gridCreateColumnEditorEventHandler = (GridCreateColumnEditorEventHandler)base.Events[RadGrid.EventCreateColumnEditor];
			if (gridCreateColumnEditorEventHandler != null)
			{
				gridCreateColumnEditorEventHandler(this, e);
			}
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x00069E66 File Offset: 0x00068066
		internal void CallOnDetailTableDataBind(GridDetailTableDataBindEventArgs e)
		{
			this.OnDetailTableDataBind(e);
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00069E70 File Offset: 0x00068070
		protected virtual void OnDetailTableDataBind(GridDetailTableDataBindEventArgs e)
		{
			GridDetailTableDataBindEventHandler gridDetailTableDataBindEventHandler = (GridDetailTableDataBindEventHandler)base.Events[RadGrid.EventDetailTableDataBind];
			if (gridDetailTableDataBindEventHandler != null)
			{
				gridDetailTableDataBindEventHandler(this, e);
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00069E9E File Offset: 0x0006809E
		internal void OnColumnsChanged()
		{
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00069EA0 File Offset: 0x000680A0
		internal void CallOnDeleteCommand(GridCommandEventArgs e)
		{
			this.OnDeleteCommand(e);
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00069EAC File Offset: 0x000680AC
		protected virtual void OnDeleteCommand(GridCommandEventArgs e)
		{
			GridCommandEventHandler gridCommandEventHandler = (GridCommandEventHandler)base.Events[RadGrid.EventDeleteCommand];
			if (gridCommandEventHandler != null)
			{
				gridCommandEventHandler(this, e);
			}
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00069EDA File Offset: 0x000680DA
		internal void CallOnEditCommand(GridCommandEventArgs e)
		{
			this.OnEditCommand(e);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00069EE4 File Offset: 0x000680E4
		protected virtual void OnEditCommand(GridCommandEventArgs e)
		{
			GridCommandEventHandler gridCommandEventHandler = (GridCommandEventHandler)base.Events[RadGrid.EventEditCommand];
			if (gridCommandEventHandler != null)
			{
				gridCommandEventHandler(this, e);
			}
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00069F12 File Offset: 0x00068112
		internal void CallOnItemCommand(GridCommandEventArgs e)
		{
			this.OnItemCommand(e);
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00069F1C File Offset: 0x0006811C
		protected virtual void OnItemCommand(GridCommandEventArgs e)
		{
			GridCommandEventHandler gridCommandEventHandler = (GridCommandEventHandler)base.Events[RadGrid.EventItemCommand];
			if (gridCommandEventHandler != null)
			{
				gridCommandEventHandler(this, e);
			}
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00069F4A File Offset: 0x0006814A
		internal void CallOnItemCreated(GridItemEventArgs e)
		{
			this.OnItemCreated(e);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00069F54 File Offset: 0x00068154
		protected virtual void OnItemCreated(GridItemEventArgs e)
		{
			GridItemEventHandler gridItemEventHandler = (GridItemEventHandler)base.Events[RadGrid.EventItemCreated];
			if (gridItemEventHandler != null)
			{
				gridItemEventHandler(this, e);
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00069F82 File Offset: 0x00068182
		internal void CallOnCustomAggregate(GridCustomAggregateEventArgs e)
		{
			this.OnCustomAggregate(e);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00069F8C File Offset: 0x0006818C
		protected virtual void OnCustomAggregate(GridCustomAggregateEventArgs e)
		{
			GridCustomAggregateEventHandler gridCustomAggregateEventHandler = (GridCustomAggregateEventHandler)base.Events[RadGrid.EventCustomAggregate];
			if (gridCustomAggregateEventHandler != null)
			{
				gridCustomAggregateEventHandler(this, e);
			}
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00069FBA File Offset: 0x000681BA
		internal void CallOnColumnCreating(GridColumnCreatingEventArgs e)
		{
			this.OnColumnCreating(e);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00069FC4 File Offset: 0x000681C4
		protected virtual void OnColumnCreating(GridColumnCreatingEventArgs e)
		{
			GridColumnCreatingEventHandler gridColumnCreatingEventHandler = (GridColumnCreatingEventHandler)base.Events[RadGrid.EventColumnCreating];
			if (gridColumnCreatingEventHandler != null)
			{
				gridColumnCreatingEventHandler(this, e);
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00069FF2 File Offset: 0x000681F2
		internal void CallOnColumnCreated(GridColumnCreatedEventArgs e)
		{
			this.OnColumnCreated(e);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x00069FFC File Offset: 0x000681FC
		protected virtual void OnColumnCreated(GridColumnCreatedEventArgs e)
		{
			GridColumnCreatedEventHandler gridColumnCreatedEventHandler = (GridColumnCreatedEventHandler)base.Events[RadGrid.EventColumnCreated];
			if (gridColumnCreatedEventHandler != null)
			{
				gridColumnCreatedEventHandler(this, e);
			}
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x0006A02A File Offset: 0x0006822A
		internal void CallOnItemDataBound(GridItemEventArgs e)
		{
			this.OnItemDataBound(e);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x0006A034 File Offset: 0x00068234
		protected virtual void OnItemDataBound(GridItemEventArgs e)
		{
			GridItemEventHandler gridItemEventHandler = (GridItemEventHandler)base.Events[RadGrid.EventItemDataBound];
			if (gridItemEventHandler != null)
			{
				gridItemEventHandler(this, e);
			}
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0006A062 File Offset: 0x00068262
		internal void CallOnPageIndexChanged(GridPageChangedEventArgs e)
		{
			this.TrackPaging(e.NewPageIndex);
			this.OnPageIndexChanged(e);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x0006A078 File Offset: 0x00068278
		protected virtual void OnPageIndexChanged(GridPageChangedEventArgs e)
		{
			GridPageChangedEventHandler gridPageChangedEventHandler = (GridPageChangedEventHandler)base.Events[RadGrid.EventPageIndexChanged];
			if (gridPageChangedEventHandler != null)
			{
				gridPageChangedEventHandler(this, e);
			}
			this.ClearActiveRowIndex(false);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x0006A0AD File Offset: 0x000682AD
		internal void CallOnPageSizeChanged(GridPageSizeChangedEventArgs e)
		{
			this.OnPageSizeChanged(e);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x0006A0B8 File Offset: 0x000682B8
		protected virtual void OnPageSizeChanged(GridPageSizeChangedEventArgs e)
		{
			GridPageSizeChangedEventHandler gridPageSizeChangedEventHandler = (GridPageSizeChangedEventHandler)base.Events[RadGrid.EventPageSizeChanged];
			if (gridPageSizeChangedEventHandler != null)
			{
				gridPageSizeChangedEventHandler(this, e);
			}
			this.ClearActiveRowIndex(false);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0006A0ED File Offset: 0x000682ED
		internal void CallOnItemEvent(GridItemEventArgs args)
		{
			this.OnItemEvent(args);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x0006A0F8 File Offset: 0x000682F8
		protected virtual void OnItemEvent(GridItemEventArgs e)
		{
			GridItemEventHandler gridItemEventHandler = (GridItemEventHandler)base.Events[RadGrid.EventItemEvent];
			if (gridItemEventHandler != null)
			{
				gridItemEventHandler(this, e);
			}
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x0006A126 File Offset: 0x00068326
		internal void CallOnSelectedCellChanged(EventArgs e)
		{
			this.OnSelectedCellChanged(e);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x0006A12F File Offset: 0x0006832F
		internal void CallOnSelectedIndexChanged(EventArgs e)
		{
			this.OnSelectedIndexChanged(e);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x0006A138 File Offset: 0x00068338
		internal void CallOnSortCommand(GridSortCommandEventArgs e)
		{
			this.OnSortCommand(e);
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x0006A144 File Offset: 0x00068344
		protected virtual void OnSortCommand(GridSortCommandEventArgs e)
		{
			GridSortCommandEventHandler gridSortCommandEventHandler = (GridSortCommandEventHandler)base.Events[RadGrid.EventSortCommand];
			if (gridSortCommandEventHandler != null)
			{
				gridSortCommandEventHandler(this, e);
			}
			this.ClearActiveRowIndex(false);
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x0006A179 File Offset: 0x00068379
		private void ClearActiveRowIndex(bool shouldFocusOnPage)
		{
			if (this.ClientSettings.AllowKeyboardNavigation)
			{
				this.ClientSettings.ActiveRowIndex = null;
				this.shouldFocusOnPage = shouldFocusOnPage;
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0006A19B File Offset: 0x0006839B
		internal void CallOnUpdateCommand(GridCommandEventArgs e)
		{
			this.OnUpdateCommand(e);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x0006A1A4 File Offset: 0x000683A4
		protected virtual void OnUpdateCommand(GridCommandEventArgs e)
		{
			GridCommandEventHandler gridCommandEventHandler = (GridCommandEventHandler)base.Events[RadGrid.EventUpdateCommand];
			if (gridCommandEventHandler != null)
			{
				gridCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0006A1D2 File Offset: 0x000683D2
		internal void CallOnInsertCommand(GridCommandEventArgs gridCommandEventArgs)
		{
			this.OnInsertCommand(gridCommandEventArgs);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x0006A1DC File Offset: 0x000683DC
		protected virtual void OnInsertCommand(GridCommandEventArgs e)
		{
			GridCommandEventHandler gridCommandEventHandler = (GridCommandEventHandler)base.Events[RadGrid.EventInsertCommand];
			if (gridCommandEventHandler != null)
			{
				gridCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x0006A20A File Offset: 0x0006840A
		internal bool CallOnNeedDataSource(GridNeedDataSourceEventArgs e)
		{
			return this.OnNeedDataSource(e);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x0006A214 File Offset: 0x00068414
		protected virtual bool OnNeedDataSource(GridNeedDataSourceEventArgs e)
		{
			this.IsNeedDataSourceInProgress = true;
			try
			{
				GridNeedDataSourceEventHandler gridNeedDataSourceEventHandler = (GridNeedDataSourceEventHandler)base.Events[RadGrid.EventNeedDataSource];
				if (gridNeedDataSourceEventHandler != null)
				{
					gridNeedDataSourceEventHandler(this, e);
					this.isBoundUsingNeedDataSource = true;
					return true;
				}
			}
			finally
			{
				this.IsNeedDataSourceInProgress = false;
			}
			return false;
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0006A274 File Offset: 0x00068474
		protected virtual bool OnFilterCheckListItemsRequested(GridFilterCheckListItemsRequestedEventArgs e)
		{
			GridFilterCheckListItemsRequestedEventHandler gridFilterCheckListItemsRequestedEventHandler = (GridFilterCheckListItemsRequestedEventHandler)base.Events[RadGrid.EventFilterCheckListItemsRequested];
			if (gridFilterCheckListItemsRequestedEventHandler != null)
			{
				gridFilterCheckListItemsRequestedEventHandler(this, e);
				return true;
			}
			return false;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0006A2A5 File Offset: 0x000684A5
		internal void CallOnGroupsChanging(GridGroupsChangingEventArgs e)
		{
			this.OnGroupsChanging(e);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0006A2B0 File Offset: 0x000684B0
		protected virtual void OnGroupsChanging(GridGroupsChangingEventArgs e)
		{
			GridGroupsChangingEventHandler gridGroupsChangingEventHandler = (GridGroupsChangingEventHandler)base.Events[RadGrid.EventGroupsChanging];
			if (gridGroupsChangingEventHandler != null)
			{
				gridGroupsChangingEventHandler(this, e);
			}
			this.ClearActiveRowIndex(false);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0006A2E5 File Offset: 0x000684E5
		internal void CallOnItemUpdated(GridUpdatedEventArgs e)
		{
			this.OnItemUpdated(e);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0006A2F0 File Offset: 0x000684F0
		protected virtual void OnItemUpdated(GridUpdatedEventArgs e)
		{
			GridUpdatedEventHandler gridUpdatedEventHandler = (GridUpdatedEventHandler)base.Events[RadGrid.EventItemUpdated];
			if (gridUpdatedEventHandler != null)
			{
				gridUpdatedEventHandler(this, e);
			}
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0006A31E File Offset: 0x0006851E
		internal void CallOnItemInserted(GridInsertedEventArgs e)
		{
			this.OnItemInserted(e);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x0006A328 File Offset: 0x00068528
		protected virtual void OnItemInserted(GridInsertedEventArgs e)
		{
			GridInsertedEventHandler gridInsertedEventHandler = (GridInsertedEventHandler)base.Events[RadGrid.EventItemInserted];
			if (gridInsertedEventHandler != null)
			{
				gridInsertedEventHandler(this, e);
			}
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x0006A356 File Offset: 0x00068556
		internal void CallOnItemDeleted(GridDeletedEventArgs e)
		{
			this.OnItemDeleted(e);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0006A360 File Offset: 0x00068560
		protected virtual void OnItemDeleted(GridDeletedEventArgs e)
		{
			GridDeletedEventHandler gridDeletedEventHandler = (GridDeletedEventHandler)base.Events[RadGrid.EventItemDeleted];
			if (gridDeletedEventHandler != null)
			{
				gridDeletedEventHandler(this, e);
			}
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0006A38E File Offset: 0x0006858E
		internal void CallOnExcelExportCellFormatting(ExcelExportCellFormattingEventArgs e)
		{
			this.OnExcelExportCellFormatting(e);
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x0006A398 File Offset: 0x00068598
		protected virtual void OnExcelExportCellFormatting(ExcelExportCellFormattingEventArgs e)
		{
			OnExcelExportCellFormattingEventHandler onExcelExportCellFormattingEventHandler = (OnExcelExportCellFormattingEventHandler)base.Events[RadGrid.EventExcelExportCellFormatting];
			if (onExcelExportCellFormattingEventHandler != null)
			{
				onExcelExportCellFormattingEventHandler(this, e);
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x0006A3C6 File Offset: 0x000685C6
		internal void CallOnExportCellFormatting(ExportCellFormattingEventArgs e)
		{
			this.OnExportCellFormatting(e);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x0006A3D0 File Offset: 0x000685D0
		protected virtual void OnExportCellFormatting(ExportCellFormattingEventArgs e)
		{
			EventHandler<ExportCellFormattingEventArgs> eventHandler = (EventHandler<ExportCellFormattingEventArgs>)base.Events[RadGrid.EventExportCellFormatting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0006A3FE File Offset: 0x000685FE
		internal void CallOnPdfExporting(GridPdfExportingArgs e)
		{
			this.OnPdfExporting(e);
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0006A408 File Offset: 0x00068608
		protected virtual void OnPdfExporting(GridPdfExportingArgs e)
		{
			OnGridPdfExportingEventHandler onGridPdfExportingEventHandler = (OnGridPdfExportingEventHandler)base.Events[RadGrid.EventPdfExporting];
			if (onGridPdfExportingEventHandler != null)
			{
				onGridPdfExportingEventHandler(this, e);
			}
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x0006A436 File Offset: 0x00068636
		internal void CallOnInfrastructureExporting(GridInfrastructureExportingEventArgs e)
		{
			this.OnInfrastructureExporting(e);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0006A440 File Offset: 0x00068640
		protected virtual void OnInfrastructureExporting(GridInfrastructureExportingEventArgs e)
		{
			EventHandler<GridInfrastructureExportingEventArgs> eventHandler = (EventHandler<GridInfrastructureExportingEventArgs>)base.Events[RadGrid.EventInfrastructureExporting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x0006A46E File Offset: 0x0006866E
		internal void CallOnBiffExporting(GridBiffExportingEventArgs e)
		{
			this.OnBiffExporting(e);
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x0006A478 File Offset: 0x00068678
		protected virtual void OnBiffExporting(GridBiffExportingEventArgs e)
		{
			EventHandler<GridBiffExportingEventArgs> eventHandler = (EventHandler<GridBiffExportingEventArgs>)base.Events[RadGrid.EventBiffExporting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0006A4A6 File Offset: 0x000686A6
		internal void CallOnHTMLExporting(GridHTMLExportingEventArgs e)
		{
			this.OnHTMLExporting(e);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0006A4B0 File Offset: 0x000686B0
		protected virtual void OnHTMLExporting(GridHTMLExportingEventArgs e)
		{
			EventHandler<GridHTMLExportingEventArgs> eventHandler = (EventHandler<GridHTMLExportingEventArgs>)base.Events[RadGrid.EventHTMLExporting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0006A4DE File Offset: 0x000686DE
		internal void CallOnGridExporting(GridExportingArgs e)
		{
			this.TrackExport(e.ExportType.ToString());
			this.OnGridExporting(e);
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0006A500 File Offset: 0x00068700
		protected virtual void OnGridExporting(GridExportingArgs e)
		{
			OnGridExportingEventHandler onGridExportingEventHandler = (OnGridExportingEventHandler)base.Events[RadGrid.EventExporting];
			if (onGridExportingEventHandler != null)
			{
				onGridExportingEventHandler(this, e);
			}
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0006A530 File Offset: 0x00068730
		protected virtual void OnExcelMLExportRowCreated(GridExportExcelMLRowCreatedArgs e)
		{
			GridExcelMLExportRowCreatedEventHandler gridExcelMLExportRowCreatedEventHandler = (GridExcelMLExportRowCreatedEventHandler)base.Events[RadGrid.EventExcelMLExportRowCreated];
			if (gridExcelMLExportRowCreatedEventHandler != null)
			{
				gridExcelMLExportRowCreatedEventHandler(this, e);
			}
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0006A55E File Offset: 0x0006875E
		internal void CallOnExcelMLExportRowCreated(GridExportExcelMLRowCreatedArgs e)
		{
			this.OnExcelMLExportRowCreated(e);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0006A567 File Offset: 0x00068767
		internal void CallOnExcelMLWorkBookCreated(GridExcelMLWorkBookCreatedEventArgs e)
		{
			this.OnExcelMLWorkBookCreated(e);
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0006A570 File Offset: 0x00068770
		protected virtual void OnExcelMLWorkBookCreated(GridExcelMLWorkBookCreatedEventArgs e)
		{
			EventHandler<GridExcelMLWorkBookCreatedEventArgs> eventHandler = (EventHandler<GridExcelMLWorkBookCreatedEventArgs>)base.Events[RadGrid.EventExcelMLWorkBookCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0006A5A0 File Offset: 0x000687A0
		protected virtual void OnExcelMLExportStylesCreated(GridExportExcelMLStyleCreatedArgs e)
		{
			GridExcelMLExportStylesCreatedEventHandler gridExcelMLExportStylesCreatedEventHandler = (GridExcelMLExportStylesCreatedEventHandler)base.Events[RadGrid.EventExcelMLExportStylesCreated];
			if (gridExcelMLExportStylesCreatedEventHandler != null)
			{
				gridExcelMLExportStylesCreatedEventHandler(this, e);
			}
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x0006A5CE File Offset: 0x000687CE
		internal void CallOnExcelMLExportStylesCreated(GridExportExcelMLStyleCreatedArgs e)
		{
			this.OnExcelMLExportStylesCreated(e);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x0006A5D7 File Offset: 0x000687D7
		protected override void PrepareControlHierarchy()
		{
			this.GroupPanel.Visible = (this.ShowGroupPanel && this.GroupingEnabled);
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x0006A5F8 File Offset: 0x000687F8
		private void SaveTableViewStructure(ArrayList stateList, GridTableView tableView)
		{
			stateList.Add(tableView.GetStructureState());
			stateList.Add(tableView.DetailTables.Count);
			foreach (GridTableView tableView2 in tableView.DetailTables)
			{
				this.SaveTableViewStructure(stateList, tableView2);
			}
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x0006A674 File Offset: 0x00068874
		private void LoadTableViewStructure(IEnumerator stateEnumerator, GridTableView tableView)
		{
			if (stateEnumerator.MoveNext())
			{
				tableView.LoadStructureState(stateEnumerator.Current);
				if (!stateEnumerator.MoveNext())
				{
					return;
				}
				int num = (int)stateEnumerator.Current;
				bool flag = false;
				if (tableView.DetailTables.Count != num)
				{
					tableView.DetailTables.Clear();
					flag = true;
				}
				for (int i = 0; i < num; i++)
				{
					GridTableView gridTableView;
					if (flag)
					{
						gridTableView = this.CreateTableView();
						tableView.DetailTables.Add(gridTableView);
					}
					else
					{
						gridTableView = tableView.DetailTables[i];
					}
					this.LoadTableViewStructure(stateEnumerator, gridTableView);
				}
			}
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0006A704 File Offset: 0x00068904
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			ArrayList arrayList = new ArrayList();
			this.SaveTableViewStructure(arrayList, this.MasterTableView);
			object[] array = NestedState.SaveViewState(this);
			object[] array2 = new object[array.Length + 2];
			array2[0] = obj;
			array2[1] = arrayList;
			array.CopyTo(array2, 2);
			return array2;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0006A750 File Offset: 0x00068950
		public void PrepareRowsRecursive(GridTableView tableView)
		{
			GridItem[] items = tableView.GetItems(new GridItemType[]
			{
				GridItemType.NestedView
			});
			foreach (GridNestedViewItem gridNestedViewItem in items)
			{
				foreach (GridTableView gridTableView in gridNestedViewItem.NestedTableViews)
				{
					gridTableView.CssClass = this.FormatCssClass("rgDetailTable", gridTableView.CssClass);
					this.PrepareRows(gridTableView);
					if (gridTableView.HasDetailTables)
					{
						this.PrepareRowsRecursive(gridTableView);
					}
				}
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x0006A7E0 File Offset: 0x000689E0
		private void PrepareRows(GridTableView view)
		{
			view.RenderItemStyle.CssClass = this.FormatCssClass("rgRow", view.RenderItemStyle.CssClass);
			if (this.ClientSettings.EnableAlternatingItems)
			{
				this.AlternatingItemStyle.CssClass = this.FormatCssClass("rgAltRow", this.AlternatingItemStyle.CssClass);
				view.RenderAlternatingItemStyle.CssClass = this.FormatCssClass("rgAltRow", view.RenderAlternatingItemStyle.CssClass);
			}
			else
			{
				this.AlternatingItemStyle.CssClass = this.FormatCssClass("rgRow", this.AlternatingItemStyle.CssClass);
				view.RenderAlternatingItemStyle.CssClass = this.FormatCssClass("rgRow", view.RenderAlternatingItemStyle.CssClass);
			}
			GridTable gridTable = view.GetGridTable();
			if (gridTable != null)
			{
				int num = 0;
				foreach (object obj in gridTable.Rows)
				{
					GridItem gridItem = (GridItem)obj;
					gridItem.PrepareItemStyle();
					if (!(gridItem is GridTHead) && !(gridItem is GridTFoot) && gridItem.Visible)
					{
						gridItem.SetClientRowIndex(num);
						num++;
					}
				}
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0006A920 File Offset: 0x00068B20
		public void PrepareRowsVisibilityRecursive(GridTableView tableView)
		{
			GridItem[] items = tableView.GetItems(new GridItemType[]
			{
				GridItemType.NestedView
			});
			foreach (GridNestedViewItem gridNestedViewItem in items)
			{
				foreach (GridTableView gridTableView in gridNestedViewItem.NestedTableViews)
				{
					if (gridTableView.Visible)
					{
						this.PrepareRowsVisibility(gridTableView);
						if (gridTableView.HasDetailTables)
						{
							this.PrepareRowsVisibilityRecursive(gridTableView);
						}
					}
				}
			}
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x0006A9A4 File Offset: 0x00068BA4
		private void PrepareRowsVisibility(GridTableView view)
		{
			GridTable gridTable = view.GetGridTable();
			if (gridTable != null)
			{
				foreach (object obj in gridTable.Rows)
				{
					GridItem gridItem = (GridItem)obj;
					gridItem.PrepareItemVisibility();
				}
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0006AA08 File Offset: 0x00068C08
		protected override void LoadViewState(object savedStateObject)
		{
			if (savedStateObject != null)
			{
				object[] array = (object[])savedStateObject;
				if (array[0] != null)
				{
					base.LoadViewState(array[0]);
				}
				if (array[1] != null)
				{
					ArrayList arrayList = (ArrayList)array[1];
					if (arrayList.Count > 0)
					{
						this.LoadTableViewStructure(arrayList.GetEnumerator(), this.MasterTableView);
					}
				}
				object[] array2 = new object[array.Length - 2];
				Array.Copy(array, 2, array2, 0, array2.Length);
				NestedState.LoadViewState(this, array2);
			}
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x0006AA78 File Offset: 0x00068C78
		protected override void TrackViewState()
		{
			if (base.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			this.MasterTableView.CallTrackViewState();
			if (this.ShowGroupPanel && this.GroupPanel != null)
			{
				this.GroupPanel.CallTrackViewState();
			}
			NestedState.TrackViewState(this);
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x0006AAC8 File Offset: 0x00068CC8
		// (set) Token: 0x06002091 RID: 8337 RVA: 0x0006AAF1 File Offset: 0x00068CF1
		[Category("Paging")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_AllowCustomPaging")]
		[DefaultValue(false)]
		public virtual bool AllowCustomPaging
		{
			get
			{
				object obj = this.ViewState["AllowCustomPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowCustomPaging"] = value;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x0006AB0C File Offset: 0x00068D0C
		// (set) Token: 0x06002093 RID: 8339 RVA: 0x0006AB35 File Offset: 0x00068D35
		[NotifyParentProperty(true)]
		[Description("RadGrid_AllowPaging")]
		[Category("Paging")]
		[DefaultValue(false)]
		public virtual bool AllowPaging
		{
			get
			{
				object obj = this.ViewState["AllowPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowPaging"] = value;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002094 RID: 8340 RVA: 0x0006AB50 File Offset: 0x00068D50
		// (set) Token: 0x06002095 RID: 8341 RVA: 0x0006AB79 File Offset: 0x00068D79
		[Description("RadGrid_AllowSorting")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual bool AllowSorting
		{
			get
			{
				object obj = this.ViewState["AllowSorting"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowSorting"] = value;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x0006AB94 File Offset: 0x00068D94
		// (set) Token: 0x06002097 RID: 8343 RVA: 0x0006ABBD File Offset: 0x00068DBD
		[Category("Behavior")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether native LINQ expressions will be enabled.")]
		public virtual bool EnableLinqExpressions
		{
			get
			{
				object obj = this.ViewState["EnableLinqExpressions"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["EnableLinqExpressions"] = value;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x0006ABD5 File Offset: 0x00068DD5
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		public GridClientSettings ClientSettings
		{
			get
			{
				if (this._clientSettins == null)
				{
					this._clientSettins = new GridClientSettings(this.ViewState, this);
				}
				return this._clientSettins;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x0006ABF7 File Offset: 0x00068DF7
		[NotifyParentProperty(true)]
		[NestedStateManager]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("RadGrid_AlternatingItemStyle")]
		public virtual GridTableItemStyle AlternatingItemStyle
		{
			get
			{
				if (this._alternatingItemStyle == null)
				{
					this._alternatingItemStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._alternatingItemStyle).TrackViewState();
					}
				}
				return this._alternatingItemStyle;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x0006AC25 File Offset: 0x00068E25
		[NestedStateManager]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Description("RadGrid_AlternatingItemStyle")]
		public virtual GridTableItemStyle GroupHeaderItemStyle
		{
			get
			{
				if (this._groupHeaderItemStyle == null)
				{
					this._groupHeaderItemStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._groupHeaderItemStyle).TrackViewState();
					}
				}
				return this._groupHeaderItemStyle;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x0006AC54 File Offset: 0x00068E54
		// (set) Token: 0x0600209C RID: 8348 RVA: 0x0006AC7D File Offset: 0x00068E7D
		[DefaultValue(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_AutoGenerateColumns")]
		public virtual bool AutoGenerateColumns
		{
			get
			{
				object obj = this.ViewState["AutoGenerateColumns"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AutoGenerateColumns"] = value;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x0006AC98 File Offset: 0x00068E98
		// (set) Token: 0x0600209E RID: 8350 RVA: 0x0006ACC1 File Offset: 0x00068EC1
		[Description("RadGrid_AutoGenerateHierarchy")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool AutoGenerateHierarchy
		{
			get
			{
				object obj = this.ViewState["AutoGenerateHierarchy"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoGenerateHierarchy"] = value;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x0006ACDC File Offset: 0x00068EDC
		// (set) Token: 0x060020A0 RID: 8352 RVA: 0x0006AD05 File Offset: 0x00068F05
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool RetainExpandStateOnRebind
		{
			get
			{
				object obj = this.ViewState["RetainExpandStateOnRebind"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["RetainExpandStateOnRebind"] = value;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x0006AD1D File Offset: 0x00068F1D
		// (set) Token: 0x060020A2 RID: 8354 RVA: 0x0006AD3D File Offset: 0x00068F3D
		[DefaultValue("")]
		[Description("RadGrid_BackImageUrl")]
		[UrlProperty]
		[Category("Appearance")]
		[Bindable(true)]
		[Localizable(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return string.Empty;
				}
				return ((TableStyle)base.ControlStyle).BackImageUrl;
			}
			set
			{
				((TableStyle)base.ControlStyle).BackImageUrl = value;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0006AD50 File Offset: 0x00068F50
		[Description("RadGrid_GroupPanel")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Grouping")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridGroupPanel GroupPanel
		{
			get
			{
				if (this._groupPanel == null)
				{
					this._groupPanel = new GridGroupPanel();
				}
				return this._groupPanel;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x0006AD6C File Offset: 0x00068F6C
		// (set) Token: 0x060020A5 RID: 8357 RVA: 0x0006AD95 File Offset: 0x00068F95
		[Bindable(true)]
		[Description("RadGrid_ShowGroupPanel")]
		[Category("Grouping")]
		[DefaultValue(false)]
		public virtual bool ShowGroupPanel
		{
			get
			{
				object obj = this.ViewState["ShowGroupPanel"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ShowGroupPanel"] = value;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x0006ADB0 File Offset: 0x00068FB0
		// (set) Token: 0x060020A7 RID: 8359 RVA: 0x0006ADEC File Offset: 0x00068FEC
		[Description("Specify the position of the of GroupPanel")]
		[Category("Grouping")]
		[Bindable(true)]
		[DefaultValue(GridGroupPanelPosition.Top)]
		public virtual GridGroupPanelPosition GroupPanelPosition
		{
			get
			{
				object obj = this.ViewState["GroupPanelPosition"];
				if (obj != null)
				{
					return (GridGroupPanelPosition)obj;
				}
				if (this.ResolvedRenderMode != RenderMode.Mobile || this.IsDesignMode)
				{
					return GridGroupPanelPosition.Top;
				}
				return GridGroupPanelPosition.BeforeHeader;
			}
			set
			{
				this.ViewState["GroupPanelPosition"] = value;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x0006AE04 File Offset: 0x00069004
		// (set) Token: 0x060020A9 RID: 8361 RVA: 0x0006AE2D File Offset: 0x0006902D
		[Category("Grouping")]
		[Description("RadGrid_GroupingEnabled")]
		[Bindable(true)]
		[DefaultValue(true)]
		public virtual bool GroupingEnabled
		{
			get
			{
				object obj = this.ViewState["GroupingEnabled"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["GroupingEnabled"] = value;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x0006AE48 File Offset: 0x00069048
		// (set) Token: 0x060020AB RID: 8363 RVA: 0x0006AE76 File Offset: 0x00069076
		[DefaultValue(false)]
		[Category("Data editing")]
		[NotifyParentProperty(true)]
		public bool AllowAutomaticUpdates
		{
			get
			{
				object obj = this.ViewState["_aau"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				this.ViewState["_aau"] = value;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x0006AE90 File Offset: 0x00069090
		// (set) Token: 0x060020AD RID: 8365 RVA: 0x0006AEBE File Offset: 0x000690BE
		[Category("Data editing")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool AllowAutomaticInserts
		{
			get
			{
				object obj = this.ViewState["_aai"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				this.ViewState["_aai"] = value;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x0006AED8 File Offset: 0x000690D8
		// (set) Token: 0x060020AF RID: 8367 RVA: 0x0006AF06 File Offset: 0x00069106
		[Category("Data editing")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool AllowAutomaticDeletes
		{
			get
			{
				object obj = this.ViewState["_aad"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				this.ViewState["_aad"] = value;
			}
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0006AF1E File Offset: 0x0006911E
		public virtual GridTableView CreateTableView()
		{
			return new GridTableView(this);
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x0006AF26 File Offset: 0x00069126
		[Description("The instance of type GridTableView that represents main grid table-view in RadGrid.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		[ComplexPersistenceSetting]
		[Category("Layout")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Bindable(false)]
		public virtual GridTableView MasterTableView
		{
			get
			{
				if (this._masterTableView == null)
				{
					this._masterTableView = this.CreateTableView();
					this._masterTableView.ID = string.Empty;
				}
				return this._masterTableView;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0006AF52 File Offset: 0x00069152
		// (set) Token: 0x060020B3 RID: 8371 RVA: 0x0006AF5F File Offset: 0x0006915F
		[Description("RadGrid_CurrentPageIndex")]
		[SimplePersistenceSetting]
		[Bindable(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int CurrentPageIndex
		{
			get
			{
				return this.MasterTableView.CurrentPageIndex;
			}
			set
			{
				this.MasterTableView.CurrentPageIndex = value;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x0006AF6D File Offset: 0x0006916D
		[Description("RadGrid_EditItemStyle")]
		[NotifyParentProperty(true)]
		[NestedStateManager]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual GridTableItemStyle EditItemStyle
		{
			get
			{
				if (this._editItemStyle == null)
				{
					this._editItemStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._editItemStyle).TrackViewState();
					}
				}
				return this._editItemStyle;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x0006AF9B File Offset: 0x0006919B
		[Description("RadGrid_FooterStyle")]
		[NestedStateManager]
		[Category("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public virtual GridTableItemStyle FooterStyle
		{
			get
			{
				if (this._footerStyle == null)
				{
					this._footerStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._footerStyle).TrackViewState();
					}
				}
				return this._footerStyle;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x0006AFC9 File Offset: 0x000691C9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NestedStateManager]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Description("RadGrid_HeaderStyle")]
		[NotifyParentProperty(true)]
		[Category("Style")]
		public virtual GridTableItemStyle HeaderStyle
		{
			get
			{
				if (this._headerStyle == null)
				{
					this._headerStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._headerStyle).TrackViewState();
					}
				}
				return this._headerStyle;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x0006AFF7 File Offset: 0x000691F7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Style")]
		[NestedStateManager]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("RadGrid_FilterItemStyle")]
		[NotifyParentProperty(true)]
		public virtual GridTableItemStyle FilterItemStyle
		{
			get
			{
				if (this._filterItemStyle == null)
				{
					this._filterItemStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._filterItemStyle).TrackViewState();
					}
				}
				return this._filterItemStyle;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x0006B025 File Offset: 0x00069225
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NestedStateManager]
		[DefaultValue(null)]
		[Description("RadGrid_FilterItemStyle")]
		[Category("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public virtual GridTableItemStyle CommandItemStyle
		{
			get
			{
				if (this._commandItemStyle == null)
				{
					this._commandItemStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._commandItemStyle).TrackViewState();
					}
				}
				return this._commandItemStyle;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x0006B053 File Offset: 0x00069253
		[DefaultValue(null)]
		[NestedStateManager]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("RadGrid ActiveItemStyle")]
		[NotifyParentProperty(true)]
		[Category("Style")]
		public virtual GridTableItemStyle ActiveItemStyle
		{
			get
			{
				if (this._activeItemStyle == null)
				{
					this._activeItemStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._activeItemStyle).TrackViewState();
					}
				}
				return this._activeItemStyle;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x0006B081 File Offset: 0x00069281
		[Category("Style")]
		[NestedStateManager]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("RadGrid MultiHeaderItemStyle")]
		[NotifyParentProperty(true)]
		public virtual GridTableItemStyle MultiHeaderItemStyle
		{
			get
			{
				if (this._multiHeaderItemStyle == null)
				{
					this._multiHeaderItemStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._multiHeaderItemStyle).TrackViewState();
					}
				}
				return this._multiHeaderItemStyle;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x0006B0AF File Offset: 0x000692AF
		[Browsable(false)]
		[Description("RadGrid_Items")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual GridDataItemCollection Items
		{
			get
			{
				return this.MasterTableView.ItemsHierarchy;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x0006B0BC File Offset: 0x000692BC
		[Category("Style")]
		[NestedStateManager]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("RadGrid_ItemStyle")]
		[NotifyParentProperty(true)]
		public virtual GridTableItemStyle ItemStyle
		{
			get
			{
				if (this._itemStyle == null)
				{
					this._itemStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._itemStyle).TrackViewState();
					}
				}
				return this._itemStyle;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x0006B0EA File Offset: 0x000692EA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("RadGrid_PageCount")]
		public int PageCount
		{
			get
			{
				return this.MasterTableView.PageCount;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x0006B0F7 File Offset: 0x000692F7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NestedStateManager]
		[Description("RadGrid_PagerStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public virtual GridPagerStyle PagerStyle
		{
			get
			{
				if (this._pagerStyle == null)
				{
					this._pagerStyle = new GridPagerStyle(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._pagerStyle).TrackViewState();
					}
				}
				return this._pagerStyle;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x0006B128 File Offset: 0x00069328
		// (set) Token: 0x060020C0 RID: 8384 RVA: 0x0006B158 File Offset: 0x00069358
		[DefaultValue(10)]
		[NotifyParentProperty(true)]
		[SimplePersistenceSetting]
		[Category("Paging")]
		[Description("RadGrid_PageSize")]
		public virtual int PageSize
		{
			get
			{
				object obj = this.ControlState["PageSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return this._defaultPageSize;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.Page != null)
				{
					object obj = this.PageSize;
					if ((int)obj != value && this.AllowPaging)
					{
						GridPageSizeChangedEventArgs gridPageSizeChangedEventArgs = new GridPageSizeChangedEventArgs(null, null, new CommandEventArgs("ChangePageSize", string.Empty), value);
						this.CallOnPageSizeChanged(gridPageSizeChangedEventArgs);
						if (gridPageSizeChangedEventArgs.Canceled)
						{
							return;
						}
					}
				}
				this.ControlState["PageSize"] = value;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0006B1D8 File Offset: 0x000693D8
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x0006B201 File Offset: 0x00069401
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool AllowMultiRowSelection
		{
			get
			{
				object obj = this.ViewState["_amrs"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["_amrs"] = value;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x0006B21C File Offset: 0x0006941C
		// (set) Token: 0x060020C4 RID: 8388 RVA: 0x0006B245 File Offset: 0x00069445
		[Category("Behavior")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool AllowMultiRowEdit
		{
			get
			{
				object obj = this.ViewState["_amre"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["_amre"] = value;
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x0006B260 File Offset: 0x00069460
		internal int GetCurrentIndexHierarchical()
		{
			return this.currIndexHierarchical++;
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x0006B280 File Offset: 0x00069480
		// (set) Token: 0x060020C7 RID: 8391 RVA: 0x0006B2D7 File Offset: 0x000694D7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		[Browsable(false)]
		public GridIndexCollection SelectedIndexes
		{
			get
			{
				if (this._selectedIndexes == null)
				{
					ArrayList arrayList = (ArrayList)this.ControlState["SelectedIndexes"];
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						this.ControlState["SelectedIndexes"] = arrayList;
					}
					this._selectedIndexes = new GridIndexCollection(arrayList);
				}
				return this._selectedIndexes;
			}
			internal set
			{
				this._selectedIndexes = value;
				this.ControlState["SelectedIndexes"] = value.GetArrayList();
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x0006B2F8 File Offset: 0x000694F8
		// (set) Token: 0x060020C9 RID: 8393 RVA: 0x0006B34F File Offset: 0x0006954F
		[SimplePersistenceSetting]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GridIndexCollection SelectedCellIndexes
		{
			get
			{
				if (this._selectedCellIndexes == null)
				{
					ArrayList arrayList = (ArrayList)this.ControlState["SelectedCellIndexes"];
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						this.ControlState["SelectedCellIndexes"] = arrayList;
					}
					this._selectedCellIndexes = new GridIndexCollection(arrayList);
				}
				return this._selectedCellIndexes;
			}
			internal set
			{
				this._selectedCellIndexes = value;
				this.ControlState["SelectedCellIndexes"] = value.GetArrayList();
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x0006B370 File Offset: 0x00069570
		internal GridIndexCollection ClientUnselectableIndexes
		{
			get
			{
				if (this._clientUnselectableIndexes == null)
				{
					ArrayList arrayList = (ArrayList)this.ControlState["ClientUnselectableIndexes"];
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						this.ControlState["ClientUnselectableIndexes"] = arrayList;
					}
					this._clientUnselectableIndexes = new GridIndexCollection(arrayList);
				}
				return this._clientUnselectableIndexes;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0006B3C8 File Offset: 0x000695C8
		// (set) Token: 0x060020CC RID: 8396 RVA: 0x0006B41F File Offset: 0x0006961F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		[Browsable(false)]
		public GridIndexCollection EditIndexes
		{
			get
			{
				if (this._editIndexes == null)
				{
					ArrayList arrayList = (ArrayList)this.ControlState["EditIndexes"];
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						this.ControlState["EditIndexes"] = arrayList;
					}
					this._editIndexes = new GridIndexCollection(arrayList);
				}
				return this._editIndexes;
			}
			internal set
			{
				this.ControlState["EditIndexes"] = value.GetArrayList();
				this._editIndexes = value;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x0006B440 File Offset: 0x00069640
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public GridItemCollection SelectedItems
		{
			get
			{
				GridItemCollection gridItemCollection = new GridItemCollection();
				foreach (object obj in this.SelectedIndexes)
				{
					string hierarchicalIndex = (string)obj;
					gridItemCollection.Add(this.Items[hierarchicalIndex]);
				}
				return gridItemCollection;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x0006B4AC File Offset: 0x000696AC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public GridTableCellCollection SelectedCells
		{
			get
			{
				GridTableCellCollection gridTableCellCollection = new GridTableCellCollection();
				foreach (object obj in this.SelectedCellIndexes)
				{
					string text = (string)obj;
					string[] array = text.Split(new char[]
					{
						'&'
					});
					string hierarchicalIndex = array[0];
					string columnUniqueName = array[1];
					GridDataItem gridDataItem = (GridDataItem)this.Items.FindByHierarchyIndex(hierarchicalIndex);
					if (gridDataItem != null)
					{
						GridTableCell cell = (GridTableCell)gridDataItem[columnUniqueName];
						gridTableCellCollection.Add(cell);
					}
				}
				return gridTableCellCollection;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x0006B55C File Offset: 0x0006975C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedValue
		{
			get
			{
				object result = null;
				if (this.Items.Count == 0)
				{
					return result;
				}
				if (this.SelectedItems.Count > 0)
				{
					GridItem gridItem = this.SelectedItems[this.SelectedItems.Count - 1];
					if (gridItem != null && gridItem.OwnerTableView.DataKeyNames.Length > 0)
					{
						result = gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex][gridItem.OwnerTableView.DataKeyNames[0].ToString()];
					}
				}
				return result;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x0006B5E4 File Offset: 0x000697E4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataKey SelectedValues
		{
			get
			{
				DataKey result = null;
				if (this.Items.Count == 0)
				{
					return result;
				}
				if (this.SelectedItems.Count > 0)
				{
					GridItem gridItem = this.SelectedItems[this.SelectedItems.Count - 1];
					if (gridItem != null && gridItem.OwnerTableView.DataKeyNames.Length > 0)
					{
						result = gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex];
					}
				}
				return result;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x0006B654 File Offset: 0x00069854
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public GridItemCollection EditItems
		{
			get
			{
				GridItemCollection gridItemCollection = new GridItemCollection();
				foreach (object obj in this.EditIndexes)
				{
					string hierarchicalIndex = (string)obj;
					if (this.Items.FindByHierarchyIndex(hierarchicalIndex) != null)
					{
						gridItemCollection.Add(this.Items[hierarchicalIndex]);
					}
				}
				return gridItemCollection;
			}
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x0006B6D0 File Offset: 0x000698D0
		internal void SaveSelectedIndexState(string newValue)
		{
			if (!this.AllowMultiRowSelection)
			{
				try
				{
					foreach (object obj in this.SelectedItems)
					{
						GridItem gridItem = (GridItem)obj;
						gridItem.Selected = false;
					}
				}
				catch
				{
					throw new GridException("Please set RadGrid AllowMultiRowSelection to \"True\" to start selecting multiple items.");
				}
			}
			this.SelectedIndexes.Add(newValue);
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x0006B758 File Offset: 0x00069958
		internal void RemoveSelectedIndexState(string index)
		{
			this.SelectedIndexes.Remove(index);
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x0006B768 File Offset: 0x00069968
		internal void SaveEditIndexState(string newValue)
		{
			bool flag = !this.AllowMultiRowEdit && this.MasterTableView.DetailTables.Count > 0;
			if (!this.AllowMultiRowEdit)
			{
				foreach (object obj in this.EditItems)
				{
					GridItem gridItem = (GridItem)obj;
					if (gridItem.OwnerTableView.EditMode != GridEditMode.InPlace || (gridItem.OwnerTableView.EditMode == GridEditMode.InPlace && !flag))
					{
						gridItem.Edit = false;
					}
				}
			}
			this.EditIndexes.Add(newValue);
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x0006B814 File Offset: 0x00069A14
		internal void RemoveEditIndexState(string index)
		{
			this.EditIndexes.Remove(index);
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x0006B822 File Offset: 0x00069A22
		[Category("Style")]
		[NestedStateManager]
		[Description("RadGrid_SelectedItemStyle")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual GridTableItemStyle SelectedItemStyle
		{
			get
			{
				if (this._selectedItemStyle == null)
				{
					this._selectedItemStyle = new GridTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._selectedItemStyle).TrackViewState();
					}
				}
				return this._selectedItemStyle;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x0006B850 File Offset: 0x00069A50
		// (set) Token: 0x060020D8 RID: 8408 RVA: 0x0006B879 File Offset: 0x00069A79
		[Bindable(true)]
		[Description("RadGrid_ShowFooter")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue(false)]
		public virtual bool ShowFooter
		{
			get
			{
				object obj = this.ViewState["ShowFooter"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ShowFooter"] = value;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0006B894 File Offset: 0x00069A94
		// (set) Token: 0x060020DA RID: 8410 RVA: 0x0006B8BD File Offset: 0x00069ABD
		[Description("Gets or set a value indicating whether the statusbar item of the grid will be")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(false)]
		public virtual bool ShowStatusBar
		{
			get
			{
				object obj = this.ViewState["ShowStatusBar"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ShowStatusBar"] = value;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0006B8D5 File Offset: 0x00069AD5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Description("Misc. StatusBar settings")]
		[Category("Misc")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public GridStatusBarItemSettings StatusBarSettings
		{
			get
			{
				if (this._statusBarItemSettings == null)
				{
					this._statusBarItemSettings = new GridStatusBarItemSettings(this.ViewState, this);
				}
				return this._statusBarItemSettings;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x0006B8F8 File Offset: 0x00069AF8
		// (set) Token: 0x060020DD RID: 8413 RVA: 0x0006B921 File Offset: 0x00069B21
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue(true)]
		[Description("RadGrid_ShowHeader")]
		public virtual bool ShowHeader
		{
			get
			{
				object obj = this.ViewState["ShowHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowHeader"] = value;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x0006B93C File Offset: 0x00069B3C
		// (set) Token: 0x060020DF RID: 8415 RVA: 0x0006B965 File Offset: 0x00069B65
		[Bindable(true)]
		[Category("Paging")]
		[Description("RadGrid_VisibleItemCount")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		public virtual int VirtualItemCount
		{
			get
			{
				object obj = this.ViewState["VirtualItemCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["VirtualItemCount"] = value;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x0006B9C0 File Offset: 0x00069BC0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(typeof(GridFilterMenu))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridFilterMenu FilterMenu
		{
			get
			{
				if (this._filterMenu == null)
				{
					this._filterMenu = new GridFilterMenu(this)
					{
						ID = string.Format("rfltMenu", new object[0]),
						ClickToOpen = true,
						RenderMode = base.RenderMode
					};
					if (this.ViewState["EnableEmbeddedScripts"] != null)
					{
						this._filterMenu.EnableEmbeddedScripts = (bool)this.ViewState["EnableEmbeddedScripts"];
					}
					if (this.ViewState["EnableEmbeddedSkins"] != null)
					{
						this._filterMenu.EnableEmbeddedSkins = (bool)this.ViewState["EnableEmbeddedSkins"];
					}
					if (this.ViewState["EnableEmbeddedBaseStylesheet"] != null)
					{
						this._filterMenu.EnableEmbeddedBaseStylesheet = (bool)this.ViewState["EnableEmbeddedBaseStylesheet"];
					}
					this._filterMenu.PreRender += delegate(object sender, EventArgs e)
					{
						if (!base.DesignMode)
						{
							this._filterMenu.Visible = this.HasFilterIcon(this.MasterTableView);
						}
						((GridFilterMenu)sender).Skin = base.RuntimeSkin;
					};
					if (!base.DesignMode)
					{
						this._filterMenu.EnableTheming = this.EnableTheming;
					}
					this.CreateFilterMenuItems(this._filterMenu);
					this._filterMenu.Visible = !base.DesignMode;
					if (base.DesignMode)
					{
						this.Controls.Add(this._filterMenu);
					}
				}
				return this._filterMenu;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0006BB1C File Offset: 0x00069D1C
		private void CreateFilterMenuItems(GridFilterMenu gridFilterMenu)
		{
			gridFilterMenu.Items.Clear();
			if (this.FilterType == GridFilterType.Classic || this.FilterType == GridFilterType.Combined)
			{
				RadMenuItem radMenuItem = null;
				if (this.FilterType == GridFilterType.Combined)
				{
					radMenuItem = new RadMenuItem("Filter Options");
					radMenuItem.CssClass = "RadFilterMenu_Combined";
					gridFilterMenu.ClickToOpen = false;
					gridFilterMenu.Items.Add(radMenuItem);
				}
				int num = 0;
				foreach (string text in Enum.GetNames(typeof(GridKnownFunction)))
				{
					RadMenuItem radMenuItem2 = new RadMenuItem(this.GetFilterMenuItemText(text));
					radMenuItem2.Value = text;
					radMenuItem2.Attributes.Add("columnUniqueName", "");
					radMenuItem2.Attributes.Add("tableID", "");
					radMenuItem2.Value = text;
					radMenuItem2.ID = string.Format("Item{0}", num);
					radMenuItem2.PostBack = false;
					if (radMenuItem != null)
					{
						radMenuItem.Items.Add(radMenuItem2);
					}
					else
					{
						gridFilterMenu.Items.Add(radMenuItem2);
					}
				}
			}
			if (this.FilterType == GridFilterType.CheckList || this.FilterType == GridFilterType.Combined)
			{
				gridFilterMenu.CssClass = "RadFilterMenu_CheckList";
				RadMenuItem radMenuItem3 = new RadMenuItem();
				radMenuItem3.Template = new RadGrid.ListBoxMenuTemplate(this, false);
				gridFilterMenu.Items.Add(radMenuItem3);
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x0006BC6C File Offset: 0x00069E6C
		internal string GetFilterMenuItemText(string gridKnownFunctionText)
		{
			return this.Localization.GetStringFromViewState(string.Format("{0}Text", gridKnownFunctionText));
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x0006BC84 File Offset: 0x00069E84
		private bool HasFilterIcon(GridTableView view)
		{
			foreach (GridColumn gridColumn in view.RenderColumns)
			{
				if (gridColumn.ShowFilterIcon)
				{
					return true;
				}
			}
			if (view.HasDetailTables)
			{
				using (GridTableViewCollection.GridDataTableEnumerator enumerator = view.DetailTables.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						GridTableView view2 = enumerator.Current;
						return this.HasFilterIcon(view2);
					}
				}
			}
			return false;
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0006BD18 File Offset: 0x00069F18
		private void _filterMenu_ItemClick(object sender, RadMenuEventArgs e)
		{
			string value = e.Item.Value;
			string y = e.Item.Attributes["columnUniqueName"];
			string text = e.Item.Attributes["tableID"];
			GridTableView gridTableView = null;
			if (text == this.UniqueID)
			{
				gridTableView = this.MasterTableView;
			}
			else if (!string.IsNullOrEmpty(text))
			{
				gridTableView = (this.Page.FindControl(text) as GridTableView);
			}
			if (gridTableView == null)
			{
				return;
			}
			this.EditIndexes.Clear();
			GridFilteringItem gridFilteringItem = (GridFilteringItem)gridTableView.GetItems(new GridItemType[]
			{
				GridItemType.FilteringItem
			})[0];
			gridFilteringItem.FireCommandEvent("Filter", new Pair(value, y));
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x0006BE1C File Offset: 0x0006A01C
		[DefaultValue(typeof(GridHeaderContextMenu))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridHeaderContextMenu HeaderContextMenu
		{
			get
			{
				if (this._headerContextMenu == null)
				{
					this._headerContextMenu = new GridHeaderContextMenu(this);
					if (this.ViewState["RenderMode"] == null)
					{
						this.InitializeRenderMode();
					}
					this._headerContextMenu.RenderMode = base.RenderMode;
					this._headerContextMenu.ID = string.Format("rghcMenu", new object[0]);
					this._headerContextMenu.EnableAutoScroll = true;
					this._headerContextMenu.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
					this._headerContextMenu.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
					this._headerContextMenu.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
					this._headerContextMenu.PreRender += delegate(object sender, EventArgs e)
					{
						GridHeaderContextMenu gridHeaderContextMenu = sender as GridHeaderContextMenu;
						gridHeaderContextMenu.Skin = base.RuntimeSkin;
						gridHeaderContextMenu.CssClass = string.Format(CultureInfo.InvariantCulture, "GridContextMenu GridContextMenu_{0}", new object[]
						{
							base.RuntimeSkin
						});
					};
					this._headerContextMenu.ItemClick += this.headerContextMenu_ItemClick;
					if (!base.DesignMode)
					{
						this._headerContextMenu.EnableTheming = this.EnableTheming;
						this._headerContextMenu.GenerateMenuItems();
					}
					else
					{
						this.Controls.Add(this._headerContextMenu);
						this._headerContextMenu.Visible = false;
					}
				}
				return this._headerContextMenu;
			}
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x0006BF44 File Offset: 0x0006A144
		protected void headerContextMenu_ItemClick(object sender, RadMenuEventArgs e)
		{
			string text = e.Item.Attributes["ColumnName"];
			string text2 = e.Item.Attributes["TableID"];
			GridTableView gridTableView = null;
			if (text2 == this.UniqueID)
			{
				gridTableView = this.MasterTableView;
			}
			else if (text2 != null)
			{
				gridTableView = (this.Page.FindControl(text2) as GridTableView);
			}
			if (gridTableView == null)
			{
				return;
			}
			this.EditIndexes.Clear();
			GridSortExpression gridSortExpression = null;
			bool flag = gridTableView.SortExpressions.TryGetExpression(text, out gridSortExpression);
			GridSortOrder oldSortOrder = GridSortOrder.None;
			GridSortOrder newSortOrder = GridSortOrder.None;
			if (flag)
			{
				oldSortOrder = gridSortExpression.SortOrder;
			}
			if (e.Item.Value.IndexOf("None") > -1)
			{
				if (flag)
				{
					gridTableView.SortExpressions.RemoveSortExpression(gridSortExpression);
				}
				else
				{
					gridSortExpression = new GridSortExpression();
					gridSortExpression.FieldName = text;
				}
			}
			else
			{
				gridSortExpression = new GridSortExpression();
				gridSortExpression.FieldName = text;
				gridSortExpression.SortOrder = ((e.Item.Value.IndexOf("Asc") > -1) ? GridSortOrder.Ascending : GridSortOrder.Descending);
				gridTableView.SortExpressions.AddSortExpression(gridSortExpression);
				newSortOrder = gridSortExpression.SortOrder;
			}
			GridItem item = null;
			if (this.MasterTableView.Items.Count > 0)
			{
				item = this.MasterTableView.Items[0];
			}
			else
			{
				GridItem[] items = this.MasterTableView.GetItems(new GridItemType[]
				{
					GridItemType.Header
				});
				if (items.Length > 0)
				{
					item = items[0];
				}
			}
			GridSortCommandEventArgs e2 = new GridSortCommandEventArgs(item, this, gridSortExpression.ToString(), oldSortOrder, newSortOrder);
			this.CallOnSortCommand(e2);
			gridTableView.Rebind();
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x0006C0D0 File Offset: 0x0006A2D0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal string ViewStateString
		{
			get
			{
				return this._savedViewStateAsync;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x0006C0D8 File Offset: 0x0006A2D8
		// (set) Token: 0x060020E9 RID: 8425 RVA: 0x0006C0E0 File Offset: 0x0006A2E0
		[DesignOnly(true)]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(false)]
		internal bool DesignTimePopulateColumns
		{
			get
			{
				return this._designTimePopulatColumns;
			}
			set
			{
				this._designTimePopulatColumns = value;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x0006C0E9 File Offset: 0x0006A2E9
		internal Hashtable GroupColsState
		{
			get
			{
				if (this.groupColState == null)
				{
					this.groupColState = new Hashtable();
				}
				return this.groupColState;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x0006C104 File Offset: 0x0006A304
		internal Hashtable HierarchyColsExpandedState
		{
			get
			{
				if (this.hierarchyColsExpandedState == null)
				{
					this.hierarchyColsExpandedState = new Hashtable();
				}
				return this.hierarchyColsExpandedState;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x0006C11F File Offset: 0x0006A31F
		[Browsable(false)]
		[Description("GridDataTable_Columns")]
		[MergableProperty(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Default")]
		public virtual GridColumnCollection Columns
		{
			get
			{
				return this.MasterTableView.Columns;
			}
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0006C12C File Offset: 0x0006A32C
		internal void PrepareItems()
		{
			foreach (object obj in this.SelectedIndexes)
			{
				string hierarchicalIndex = (string)obj;
				GridItem gridItem = this.Items.FindByHierarchyIndex(hierarchicalIndex);
				if (gridItem != null)
				{
					gridItem.SetSelected(true);
				}
			}
			if (this.MasterTableView != null)
			{
				this.PrepareRowsVisibility(this.MasterTableView);
				this.PrepareRowsVisibilityRecursive(this.MasterTableView);
			}
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0006C1B8 File Offset: 0x0006A3B8
		internal void FinishedDetailDatabind()
		{
			this.GroupPanel.InitializeIn(this, false);
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x0006C1C8 File Offset: 0x0006A3C8
		// (set) Token: 0x060020F0 RID: 8432 RVA: 0x0006C1F1 File Offset: 0x0006A3F1
		private int lastSelectedItemIndex
		{
			get
			{
				object obj = this.ViewState["lastSelectedItemIndex"];
				if (obj == null)
				{
					return -1;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["lastSelectedItemIndex"] = value;
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0006C21C File Offset: 0x0006A41C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override bool LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			if (clientState.ContainsKey("shouldFocusOnPage"))
			{
				bool.TryParse(clientState["shouldFocusOnPage"].ToString(), out this.shouldFocusOnPage);
			}
			if (clientState["popUpLocations"] != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)clientState["popUpLocations"]))
				{
					if (!string.IsNullOrEmpty(keyValuePair.Key))
					{
						Pair pair = new Pair();
						pair.First = Unit.Parse(keyValuePair.Value.ToString().Split(new char[]
						{
							','
						})[0].ToString(), CultureInfo.InvariantCulture);
						pair.Second = Unit.Parse(keyValuePair.Value.ToString().Split(new char[]
						{
							','
						})[1].ToString(), CultureInfo.InvariantCulture);
						if (!this._popUpLocations.ContainsKey(keyValuePair.Key))
						{
							this._popUpLocations.Add(keyValuePair.Key, pair);
						}
						else
						{
							this._popUpLocations[keyValuePair.Key] = pair;
						}
					}
				}
			}
			if (clientState["selectedIndexes"] != null)
			{
				bool flag = false;
				foreach (string text in (object[])clientState["selectedIndexes"])
				{
					if (!string.IsNullOrEmpty(text) && !this.SelectedIndexes.Contains(text))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					List<object> list = new List<object>((object[])clientState["selectedIndexes"]);
					foreach (object obj in this.SelectedIndexes)
					{
						string text2 = (string)obj;
						if (!string.IsNullOrEmpty(text2) && !list.Contains(text2))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					this.SelectedIndexes.Clear();
					this.RowClickOnly = false;
					int num = 0;
					foreach (string text3 in (object[])clientState["selectedIndexes"])
					{
						if (!string.IsNullOrEmpty(text3))
						{
							this.SelectedIndexes.Add(text3);
							num++;
						}
					}
					if (num > 0)
					{
						this.ShouldCallOnSelectedIndexChanged = true;
					}
				}
			}
			if (clientState.ContainsKey("lastSelectedItemIndex"))
			{
				this.lastSelectedItemIndex = (int)clientState["lastSelectedItemIndex"];
			}
			if (clientState["selectedCellsIndexes"] != null)
			{
				bool flag2 = false;
				foreach (string text4 in (object[])clientState["selectedCellsIndexes"])
				{
					if (!string.IsNullOrEmpty(text4) && !this.SelectedCellIndexes.Contains(text4))
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					List<object> list2 = new List<object>((object[])clientState["selectedCellsIndexes"]);
					foreach (object obj2 in this.SelectedCellIndexes)
					{
						string text5 = (string)obj2;
						if (!string.IsNullOrEmpty(text5) && !list2.Contains(text5))
						{
							flag2 = true;
							break;
						}
					}
				}
				if (flag2)
				{
					this.SelectedCellIndexes.Clear();
					int num2 = 0;
					foreach (string text6 in (object[])clientState["selectedCellsIndexes"])
					{
						if (!string.IsNullOrEmpty(text6))
						{
							this.SelectedCellIndexes.Add(text6);
							num2++;
						}
					}
					if (num2 > 0)
					{
						this.ShouldCallOnSelectedCellChanged = true;
					}
				}
			}
			if (clientState["unselectableItemsIndexes"] != null)
			{
				bool flag3 = false;
				foreach (string text7 in (object[])clientState["unselectableItemsIndexes"])
				{
					if (!string.IsNullOrEmpty(text7) && !this.ClientUnselectableIndexes.Contains(text7))
					{
						flag3 = true;
						break;
					}
				}
				if (!flag3)
				{
					List<object> list3 = new List<object>((object[])clientState["unselectableItemsIndexes"]);
					foreach (object obj3 in this.ClientUnselectableIndexes)
					{
						string text8 = (string)obj3;
						if (!string.IsNullOrEmpty(text8) && !list3.Contains(text8))
						{
							flag3 = true;
							break;
						}
					}
				}
				if (flag3)
				{
					this.ClientUnselectableIndexes.Clear();
					this.RowClickOnly = false;
					int num3 = 0;
					foreach (string text9 in (object[])clientState["unselectableItemsIndexes"])
					{
						if (!string.IsNullOrEmpty(text9))
						{
							this.ClientUnselectableIndexes.Add(text9);
							num3++;
						}
					}
					if (num3 > 0)
					{
						this.ShouldCallOnSelectedIndexChanged = true;
					}
				}
			}
			if (clientState["deletedItems"] != null)
			{
				this.deletedItems = (object[])clientState["deletedItems"];
				this.ShouldCallClientDeleteCommand = true;
			}
			if (clientState["draggedItemsIndexes"] != null)
			{
				this.draggedItemsIndexes = (object[])clientState["draggedItemsIndexes"];
			}
			if (clientState["expandedGroupItems"] != null)
			{
				foreach (string text10 in (object[])clientState["expandedGroupItems"])
				{
					string id = text10.Split(new char[]
					{
						'!'
					})[0];
					string s = text10.Split(new char[]
					{
						'!'
					})[1];
					GridTableView gridTableView = (GridTableView)this.Page.FindControl(id);
					if (gridTableView != null)
					{
						GridTable gridTable = gridTableView.GetGridTable();
						TableRow tableRow = gridTable.Rows[int.Parse(s)];
						if (tableRow is GridGroupHeaderItem)
						{
							GridGroupHeaderItem gridGroupHeaderItem = tableRow as GridGroupHeaderItem;
							gridGroupHeaderItem.Expanded = !gridGroupHeaderItem.Expanded;
						}
					}
				}
			}
			if (clientState["expandedFilterItems"] != null)
			{
				foreach (string text11 in (object[])clientState["expandedFilterItems"])
				{
					string id2 = text11.Split(new char[]
					{
						'!'
					})[0];
					GridTableView gridTableView2 = (GridTableView)this.Page.FindControl(id2);
					if (gridTableView2 != null)
					{
						gridTableView2.IsFilterItemExpanded = !gridTableView2.IsFilterItemExpanded;
						GridItem[] items = gridTableView2.GetItems(new GridItemType[]
						{
							GridItemType.FilteringItem
						});
						if (items.Length > 0)
						{
							items[0].Expanded = !items[0].Expanded;
						}
					}
				}
			}
			if (clientState["expandedItems"] != null)
			{
				object[] array2 = (object[])clientState["expandedItems"];
				Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
				foreach (string text12 in array2)
				{
					if (!string.IsNullOrEmpty(text12))
					{
						if (dictionary.ContainsKey(text12))
						{
							bool flag4 = dictionary[text12];
							dictionary[text12] = !flag4;
						}
						else
						{
							dictionary.Add(text12, true);
						}
					}
				}
				foreach (KeyValuePair<string, bool> keyValuePair2 in dictionary)
				{
					if (keyValuePair2.Value && this.Items.Count > 0)
					{
						GridDataItem gridDataItem = this.Items[keyValuePair2.Key];
						if (gridDataItem != null)
						{
							gridDataItem.Expanded = !gridDataItem.Expanded;
						}
					}
				}
			}
			if (clientState.ContainsKey("hierarchyState"))
			{
				Dictionary<string, object> dictionary2 = clientState["hierarchyState"] as Dictionary<string, object>;
				if (dictionary2 != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair3 in dictionary2)
					{
						bool flag5 = (bool)keyValuePair3.Value;
						GridTableView gridTableView3 = this.Page.FindControl(keyValuePair3.Key) as GridTableView;
						GridHeaderItem gridHeaderItem = gridTableView3.GetItems(new GridItemType[]
						{
							GridItemType.Header
						})[0] as GridHeaderItem;
						IEnumerable<GridColumn> enumerable = from col in gridTableView3.RenderColumns
						where col is GridExpandColumn
						select col;
						WebControl webControl = null;
						foreach (GridColumn gridColumn in enumerable)
						{
							if (gridHeaderItem.MultiHeaderCells != null)
							{
								for (int j = 0; j < gridHeaderItem.MultiHeaderCells.Count; j++)
								{
									GridColumn gridColumn2 = gridHeaderItem.MultiHeaderCells[j] as GridColumn;
									if (gridColumn2 != null && gridColumn2.UniqueName == gridColumn.UniqueName)
									{
										webControl = (gridHeaderItem.Cells[j].Controls[0] as WebControl);
										break;
									}
								}
							}
							if (webControl == null)
							{
								webControl = (gridHeaderItem[gridColumn.UniqueName].Controls[0] as WebControl);
							}
							webControl.CssClass = ((webControl is ElasticButton) ? "t-button rgActionButton " : "") + (flag5 ? "rgCollapse" : "rgExpand");
						}
					}
				}
			}
			if (clientState.ContainsKey("groupColsState"))
			{
				Dictionary<string, object> dictionary3 = clientState["groupColsState"] as Dictionary<string, object>;
				if (dictionary3 != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair4 in dictionary3)
					{
						object[] array3 = keyValuePair4.Value as object[];
						GridTableView gridTableView4 = this.Page.FindControl(keyValuePair4.Key) as GridTableView;
						GridHeaderItem gridHeaderItem2 = gridTableView4.GetItems(new GridItemType[]
						{
							GridItemType.Header
						})[0] as GridHeaderItem;
						WebControl webControl2 = null;
						for (int k = 0; k < array3.Count<object>(); k++)
						{
							if (gridHeaderItem2.Cells[k].Controls.Count > 0)
							{
								WebControl webControl3 = gridHeaderItem2.Cells[k].Controls[0] as WebControl;
								if (webControl2 != null)
								{
									if (webControl2.CssClass == "rgCollapse" && webControl2.Visible && webControl2.Style["display"] != "none")
									{
										webControl3.Style.Add("display", "");
									}
									else
									{
										webControl3.Style.Add("display", "none");
									}
								}
								if (array3[k] != null)
								{
									webControl3.CssClass = (((bool)array3[k]) ? "rgCollapse" : "rgExpand");
								}
								webControl2 = webControl3;
							}
						}
					}
				}
			}
			if (clientState["reorderedColumns"] != null)
			{
				this.reorderedColumns = (object[])clientState["reorderedColumns"];
				this._shouldCallColumnsReorder = true;
			}
			if (clientState.ContainsKey("activeRowIndex"))
			{
				this.ClientSettings.ActiveRowIndex = (string)clientState["activeRowIndex"];
			}
			if (clientState.ContainsKey("hidedItems"))
			{
				string text13 = (string)clientState["hidedItems"];
				text13 = text13.Remove(text13.Length - 1, 1);
				string[] array4 = text13.Split(new char[]
				{
					';'
				});
				foreach (string text14 in array4)
				{
					string[] array6 = text14.Split(new char[]
					{
						','
					});
					string text15 = array6[0];
					string hierarchicalIndex = array6[1];
					if (this.Items.Count > 0)
					{
						this.Items[hierarchicalIndex].Display = false;
					}
				}
			}
			if (clientState.ContainsKey("showedItems"))
			{
				string text16 = (string)clientState["showedItems"];
				text16 = text16.Remove(text16.Length - 1, 1);
				string[] array7 = text16.Split(new char[]
				{
					';'
				});
				foreach (string text17 in array7)
				{
					string[] array8 = text17.Split(new char[]
					{
						','
					});
					string text18 = array8[0];
					string hierarchicalIndex2 = array8[1];
					if (this.Items.Count > 0)
					{
						this.Items[hierarchicalIndex2].Display = true;
					}
				}
			}
			if (clientState.ContainsKey("hidedColumns"))
			{
				object[] array9 = (object[])clientState["hidedColumns"];
				this.hiddenColumns = new string[array9.Length];
				int num4 = 0;
				foreach (string text19 in array9)
				{
					this.hiddenColumns[num4++] = text19;
					string[] array10 = text19.Split(new char[]
					{
						','
					});
					string id3 = array10[0];
					string columnUniqueName = array10[1];
					GridTableView gridTableView5 = (GridTableView)this.Page.FindControl(id3);
					if (gridTableView5 != null)
					{
						gridTableView5.GetColumn(columnUniqueName).Display = false;
					}
				}
			}
			if (clientState.ContainsKey("showedColumns"))
			{
				object[] array11 = (object[])clientState["showedColumns"];
				this.showedColumns = new string[array11.Length];
				int num5 = 0;
				foreach (string text20 in array11)
				{
					this.showedColumns[num5++] = text20;
					string[] array12 = text20.Split(new char[]
					{
						','
					});
					string id4 = array12[0];
					string columnUniqueName2 = array12[1];
					GridTableView gridTableView6 = (GridTableView)this.Page.FindControl(id4);
					if (gridTableView6 != null)
					{
						gridTableView6.GetColumn(columnUniqueName2).Display = true;
					}
				}
			}
			if (clientState.ContainsKey("checkListFilterKeys") && clientState.ContainsKey("checkListFilterValues"))
			{
				object[] array13 = (object[])clientState["checkListFilterKeys"];
				int num6 = 0;
				foreach (string text21 in array13)
				{
					string[] array14 = text21.Split(new char[]
					{
						','
					});
					string id5 = array14[0];
					string columnUniqueName3 = array14[1];
					GridTableView gridTableView7 = (GridTableView)this.Page.FindControl(id5);
					if (gridTableView7 != null)
					{
						GridColumn column = gridTableView7.GetColumn(columnUniqueName3);
						object[] array15 = (object[])((object[])clientState["checkListFilterValues"])[num6];
						if (array15 != null)
						{
							column.ListOfFilterValues = Array.ConvertAll<object, string>(array15, (object item) => item.ToString());
						}
					}
					num6++;
				}
			}
			if (clientState.ContainsKey("groupSplitterColumnsState"))
			{
				object obj4 = clientState["groupSplitterColumnsState"];
			}
			if (clientState.ContainsKey("resizedItems"))
			{
				string text22 = (string)clientState["resizedItems"];
				text22 = text22.Remove(text22.Length - 1, 1);
				string[] array16 = text22.Split(new char[]
				{
					';'
				});
				foreach (string text23 in array16)
				{
					string[] array17 = text23.Split(new char[]
					{
						','
					});
					string text24 = array17[0];
					string hierarchicalIndex3 = array17[1];
					Unit height = Unit.Parse(array17[2]);
					if (this.Items.Count > 0)
					{
						this.Items[hierarchicalIndex3].Height = height;
					}
				}
			}
			if (clientState.ContainsKey("resizedColumns"))
			{
				string text25 = (string)clientState["resizedColumns"];
				text25 = text25.Remove(text25.Length - 1, 1);
				string[] array18 = text25.Split(new char[]
				{
					';'
				});
				foreach (string text26 in array18)
				{
					string[] array19 = text26.Split(new char[]
					{
						','
					});
					string id6 = array19[0];
					string columnUniqueName4 = array19[1];
					Unit width = Unit.Parse(array19[2]);
					GridTableView gridTableView8 = (GridTableView)this.Page.FindControl(id6);
					if (gridTableView8 != null)
					{
						gridTableView8.GetColumn(columnUniqueName4).HeaderStyle.Width = width;
					}
				}
			}
			if (clientState.ContainsKey("scrolledPosition"))
			{
				string text27 = (string)clientState["scrolledPosition"];
				string[] array20 = text27.Split(new char[]
				{
					','
				});
				this.ClientSettings.Scrolling.ScrollTop = array20[0];
				this.ClientSettings.Scrolling.ScrollLeft = array20[1];
			}
			if (clientState.ContainsKey("currentPageIndex"))
			{
				this.ClientSettings.Virtualization.CurrentPageIndex = (int)clientState["currentPageIndex"];
			}
			if (clientState.ContainsKey("itemAtTop"))
			{
				this.ClientSettings.Virtualization.ItemAtTop = decimal.Parse(clientState["itemAtTop"].ToString());
				this.ClientSettings.Virtualization.StartIndex = (int)clientState["startIndex"];
			}
			if (clientState.ContainsKey("resizedControl"))
			{
				string text28 = (string)clientState["resizedControl"];
				string[] array21 = text28.Split(new char[]
				{
					';'
				});
				int num7 = 0;
				foreach (string text29 in array21)
				{
					string[] array22 = text29.Split(new char[]
					{
						','
					});
					if (array22[0] == this.MasterTableView.UniqueID)
					{
						int.TryParse(array22[1].Replace("px", ""), out num7);
					}
					else
					{
						GridTableView gridTableView9 = this.Page.FindControl(array22[0]) as GridTableView;
						if (gridTableView9 != null && !string.IsNullOrEmpty(array22[1]))
						{
							gridTableView9.Width = Unit.Parse(array22[1]);
						}
					}
				}
				if (num7 != 0)
				{
					if (this.ClientSettings.Scrolling.AllowScroll)
					{
						this.MasterTableView.Width = Unit.Pixel(num7);
					}
					else
					{
						this.Width = Unit.Pixel(num7);
					}
				}
			}
			return this.ShouldCallOnSelectedIndexChanged || this.ShouldCallClientDeleteCommand || this.ShouldCallOnSelectedCellChanged;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0006D610 File Offset: 0x0006B810
		protected override void RaisePostDataChangedEvent()
		{
			base.RaisePostDataChangedEvent();
			if (this._shouldCallColumnsReorder)
			{
				this._shouldCallColumnsReorder = false;
				foreach (string text in this.reorderedColumns)
				{
					GridTableView gridTableView = (GridTableView)this.Page.FindControl(text.Split(new char[]
					{
						','
					})[0]);
					if (gridTableView != null)
					{
						GridColumn column = gridTableView.GetColumn(text.Split(new char[]
						{
							','
						})[1]);
						GridColumn column2 = gridTableView.GetColumn(text.Split(new char[]
						{
							','
						})[2]);
						GridColumnsReorderEventArgs gridColumnsReorderEventArgs = new GridColumnsReorderEventArgs(column, column2);
						GridColumnsReorderEventHandler gridColumnsReorderEventHandler = (GridColumnsReorderEventHandler)base.Events[RadGrid.EventColumnsReorder];
						if (gridColumnsReorderEventHandler != null)
						{
							gridColumnsReorderEventHandler(this, gridColumnsReorderEventArgs);
						}
						if (!gridColumnsReorderEventArgs.Canceled)
						{
							gridTableView.SwapColumns(text.Split(new char[]
							{
								','
							})[1], text.Split(new char[]
							{
								','
							})[2]);
						}
					}
				}
			}
			if (this.ShouldCallOnSelectedCellChanged)
			{
				this.ShouldCallOnSelectedCellChanged = false;
				this.CallOnSelectedCellChanged(EventArgs.Empty);
			}
			if (this.ShouldCallOnSelectedIndexChanged)
			{
				this.ShouldCallOnSelectedIndexChanged = false;
				this.CallOnSelectedIndexChanged(EventArgs.Empty);
			}
			if (this.ShouldCallClientDeleteCommand)
			{
				ArrayList arrayList = new ArrayList();
				foreach (string hierarchicalIndex in this.deletedItems)
				{
					GridDataItem gridDataItem = this.Items[hierarchicalIndex];
					if (gridDataItem != null)
					{
						GridTableView ownerTableView = gridDataItem.OwnerTableView;
						if (ownerTableView != null)
						{
							GridCommandEventArgs gridCommandEventArgs = new GridCommandEventArgs(gridDataItem, null, new CommandEventArgs("Delete", string.Empty));
							this.OnItemCommand(gridCommandEventArgs);
							if (!gridCommandEventArgs.Canceled)
							{
								this.CallOnDeleteCommand(gridCommandEventArgs);
								if (!gridCommandEventArgs.Canceled)
								{
									if (gridDataItem.OwnerTableView.AllowAutomaticDeletes)
									{
										gridDataItem.OwnerTableView.PerformDelete(gridDataItem, true);
									}
									if (!arrayList.Contains(ownerTableView))
									{
										arrayList.Add(ownerTableView);
									}
								}
							}
						}
					}
				}
				foreach (object obj in arrayList)
				{
					GridTableView gridTableView2 = (GridTableView)obj;
					gridTableView2.ClearEditItems();
					gridTableView2.ClearSelectedItems();
					foreach (object obj2 in gridTableView2.Items)
					{
						GridItem gridItem = (GridItem)obj2;
						gridItem.Expanded = false;
					}
					gridTableView2.Rebind();
				}
			}
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0006D8EC File Offset: 0x0006BAEC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.currIndexHierarchical = 0;
			if (base.IsBoundUsingDataSourceID)
			{
				if (!this.MasterTableView.ShouldBeBound && this.AlwaysAutoBindOnPostBack)
				{
					this.EnsureDataBound();
				}
			}
			else if (this.MasterTableView.ShouldBeBound)
			{
				this.AutoDataBind(GridRebindReason.InitialLoad);
			}
			else if (this.AlwaysAutoBindOnPostBack)
			{
				this.AutoDataBind(GridRebindReason.PostbackViewStateNotPersisted);
			}
			if (this._filterMenu != null && this._filterMenu.RenderMode != this.ResolvedRenderMode)
			{
				this.FilterMenu.RenderMode = this.ResolvedRenderMode;
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0006D980 File Offset: 0x0006BB80
		internal int GetSEOPageSizeFromUrl()
		{
			if (this.PagerStyle.UseRouting && !string.IsNullOrEmpty(this.PagerStyle.SEOPageIndexRouteParameterName))
			{
				object obj = this.Page.RouteData.Values[this.PagerStyle.SEOPageIndexRouteParameterName];
				if (obj != null)
				{
					return this.TryGetPageSizeFromQueryValue(obj.ToString());
				}
				if (this.Page.Request.QueryString[this.PagerStyle.SEOPageIndexRouteParameterName] != null)
				{
					return this.TryGetPageSizeFromQueryValue(this.Page.Request.QueryString[this.PagerStyle.SEOPageIndexRouteParameterName]);
				}
			}
			if (string.IsNullOrEmpty(this.SEOPagingQueryStringKey()))
			{
				if (this.Page.Request.QueryString[string.Format("{0}ChangePage", this.ClientID)] != null)
				{
					return this.TryGetPageSizeFromQueryValue(this.Page.Request.QueryString[string.Format("{0}ChangePage", this.ClientID)]);
				}
			}
			else if (this.Page.Request.QueryString[this.SEOPagingQueryStringKey()] != null)
			{
				return this.TryGetPageSizeFromQueryValue(this.Page.Request.QueryString[this.SEOPagingQueryStringKey()]);
			}
			return -1;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x0006DAC9 File Offset: 0x0006BCC9
		private string SEOPagingQueryStringKey()
		{
			if (string.IsNullOrEmpty(this.MasterTableView.PagerStyle.SEOPagingQueryStringKey))
			{
				return this.PagerStyle.SEOPagingQueryStringKey;
			}
			return this.MasterTableView.PagerStyle.SEOPagingQueryStringKey;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0006DB00 File Offset: 0x0006BD00
		private int TryGetPageSizeFromQueryValue(string value)
		{
			int result = -1;
			if (value.IndexOf(",") > -1)
			{
				value = value.Split(new string[]
				{
					","
				}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			if (!value.Contains("_"))
			{
				return this._defaultPageSize;
			}
			string[] array = value.Split(new char[]
			{
				'_'
			});
			if (array.Length == 2 && int.TryParse(array[1], out result))
			{
				return result;
			}
			return result;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0006DB78 File Offset: 0x0006BD78
		private int GetSEOPageIndexFromUrl()
		{
			if (this.PagerStyle.UseRouting && !string.IsNullOrEmpty(this.PagerStyle.SEOPageIndexRouteParameterName))
			{
				object obj = this.Page.RouteData.Values[this.PagerStyle.SEOPageIndexRouteParameterName];
				if (obj != null)
				{
					return this.TryGetPageIndexFromQueryValue(obj.ToString());
				}
				if (this.Page.Request.QueryString[this.PagerStyle.SEOPageIndexRouteParameterName] != null)
				{
					return this.TryGetPageIndexFromQueryValue(this.Page.Request.QueryString[this.PagerStyle.SEOPageIndexRouteParameterName]);
				}
			}
			if (string.IsNullOrEmpty(this.SEOPagingQueryStringKey()))
			{
				if (this.Page.Request.QueryString[string.Format("{0}ChangePage", this.ClientID)] != null)
				{
					return this.TryGetPageIndexFromQueryValue(this.Page.Request.QueryString[string.Format("{0}ChangePage", this.ClientID)]);
				}
			}
			else if (this.Page.Request.QueryString[this.SEOPagingQueryStringKey()] != null)
			{
				return this.TryGetPageIndexFromQueryValue(this.Page.Request.QueryString[this.SEOPagingQueryStringKey()]);
			}
			return -1;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0006DCC4 File Offset: 0x0006BEC4
		private int TryGetPageIndexFromQueryValue(string value)
		{
			int num = -1;
			if (value.IndexOf(",") > -1)
			{
				value = value.Split(new string[]
				{
					","
				}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			if (value.Contains("_"))
			{
				string[] array = value.Split(new char[]
				{
					'_'
				});
				if (array.Length == 2 && int.TryParse(array[0], out num))
				{
					return num - 1;
				}
			}
			else if (int.TryParse(value, out num))
			{
				return num - 1;
			}
			return num;
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0006DD50 File Offset: 0x0006BF50
		internal void ShowHideExpandCollapseColumns()
		{
			if (this._filterMenu != null)
			{
				IEnumerable<GridColumn> enumerable = from c in this.MasterTableView.RenderColumns
				where c is GridExpandColumn
				select c;
				foreach (GridColumn gridColumn in enumerable)
				{
					GridExpandColumn gridExpandColumn = (GridExpandColumn)gridColumn;
					if ((this.MasterTableView.OwnerGrid != null && this.MasterTableView.OwnerGrid.IsExporting && this.MasterTableView.OwnerGrid.ExportSettings.HideStructureColumns) || (!this.MasterTableView.HasDetailTables && this.MasterTableView.NestedViewTemplate == null))
					{
						gridExpandColumn.Visible = false;
					}
				}
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0006DE28 File Offset: 0x0006C028
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override void ControlPreRender()
		{
			this.dataSourceControlAutomaticDataBindTriggered = true;
			base.ControlPreRender();
			if (!this.AllowMultiRowEdit && this.MasterTableView.DetailTables.Count > 0)
			{
				this.CloseEditItems();
			}
			this.dataSourceControlAutomaticDataBindTriggered = false;
			this.ShowHideExpandCollapseColumns();
			if (this.MasterTableView.ColumnGroups != null && this.MasterTableView.ColumnGroups.Count > 0)
			{
				GridColumnGroup gridColumnGroup = new GridColumnGroup();
				int count = this.MasterTableView.ColumnGroups.Count;
				Dictionary<string, GridColumnGroup> groups = new Dictionary<string, GridColumnGroup>(count);
				foreach (GridColumnGroup gridColumnGroup2 in this.MasterTableView.ColumnGroups)
				{
					gridColumnGroup2.Columns.Clear();
					gridColumnGroup2.ChildGroups.Clear();
					gridColumnGroup2.VisibleColSpan = 0;
				}
				this.MasterTableView.InitMultiHeaderStructure(gridColumnGroup, groups, this.MasterTableView.RenderColumns);
				Dictionary<int, KeyValuePair<List<GridColumn>, List<GridColumnGroup>>> dictionary = new Dictionary<int, KeyValuePair<List<GridColumn>, List<GridColumnGroup>>>();
				this.MasterTableView.BuildMultiHeaderStructure(gridColumnGroup, 1, dictionary);
				List<int> list = new List<int>(dictionary.Keys);
				list.Sort();
				list.Reverse();
				this.MasterTableView.hiddenColumnHeaderSpans = string.Empty;
				int i = list.Count - 1;
				int num = 0;
				while (i >= 0)
				{
					int num2 = list[i];
					GridItem[] items = this.MasterTableView.GetItems(new GridItemType[]
					{
						GridItemType.Header
					});
					if (items.Length > 0)
					{
						GridHeaderItem gridHeaderItem = items[num] as GridHeaderItem;
						gridHeaderItem.NumberOfHeaders = dictionary.Keys.Count;
						List<GridColumn> key = dictionary[num2].Key;
						List<GridColumnGroup> value = dictionary[num2].Value;
						ArrayList arrayList = new ArrayList(key.Count + value.Count);
						arrayList.AddRange(key);
						arrayList.AddRange(value);
						arrayList.Sort();
						gridHeaderItem.MultiHeaderCells = arrayList;
						gridHeaderItem.Level = num2 - 1;
						gridHeaderItem.AdjustColSpan();
					}
					i--;
					num++;
				}
			}
			this.PrepareItems();
			if (this._masterTableView.PagerStyle.EnableSEOPaging || this.PagerStyle.EnableSEOPaging)
			{
				int seopageIndexFromUrl = this.GetSEOPageIndexFromUrl();
				int seopageSizeFromUrl = this.GetSEOPageSizeFromUrl();
				if (!base.DesignMode && (seopageIndexFromUrl > -1 || seopageSizeFromUrl == 10))
				{
					this.CurrentPageIndex = ((seopageIndexFromUrl < 0) ? 0 : seopageIndexFromUrl);
					this.PageSize = seopageSizeFromUrl;
					this.Rebind();
				}
			}
			if (this.AddHeaderContextMenus(this.MasterTableView))
			{
				this.HeaderContextMenu.GenerateMenuItems();
			}
			if (this.EditItems.Count > 0 && this.ClientSettings.AllowKeyboardNavigation)
			{
				GridDataItem gridDataItem = (GridDataItem)this.EditItems[this.EditItems.Count - 1];
				foreach (GridColumn gridColumn in gridDataItem.OwnerTableView.RenderColumns)
				{
					if (gridColumn is GridEditableColumn)
					{
						TableCell tableCell = null;
						if (gridDataItem != null)
						{
							if (gridDataItem.EditFormItem != null)
							{
								tableCell = gridDataItem.EditFormItem[gridColumn];
							}
						}
						else
						{
							tableCell = gridDataItem[gridColumn];
						}
						if (tableCell != null)
						{
							foreach (object obj in tableCell.Controls)
							{
								Control control = (Control)obj;
								if (!(control is LiteralControl))
								{
									WebControl webControl = control as WebControl;
									if (control.Visible && webControl != null && webControl.Enabled)
									{
										this._controlToFocus = control.ClientID;
										break;
									}
								}
							}
						}
						if (!string.IsNullOrEmpty(this._controlToFocus))
						{
							return;
						}
					}
				}
			}
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0006E214 File Offset: 0x0006C414
		private void CloseEditItems()
		{
			List<GridTableView> list = new List<GridTableView>();
			if (this.EditIndexes != null && this.EditIndexes.Count > 1)
			{
				while (this.EditIndexes.Count > 1)
				{
					GridItem gridItem = this.EditItems[this.EditIndexes[0]];
					gridItem.Edit = false;
					if (!list.Contains(gridItem.OwnerTableView))
					{
						list.Add(gridItem.OwnerTableView);
					}
				}
				if (list.Contains(this.MasterTableView))
				{
					this.MasterTableView.Rebind();
					return;
				}
				if (list.Count > 0)
				{
					foreach (GridTableView gridTableView in list)
					{
						if (gridTableView.EditMode == GridEditMode.InPlace)
						{
							gridTableView.Rebind();
						}
					}
				}
			}
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x0006E2F8 File Offset: 0x0006C4F8
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			this.SetHierarchyIndexes(this.MasterTableView);
			if (this.GroupPanelPosition == GridGroupPanelPosition.Top)
			{
				this.CreateGroupPanel();
			}
			this.Controls.Add(this.MasterTableView);
			this.CreateSharedCalendar();
			if (this.FilterType == GridFilterType.HeaderContext)
			{
				this.CreateSharedFilterListBox();
			}
			if (this.GroupPanelPosition == GridGroupPanelPosition.Bottom)
			{
				this.CreateGroupPanel();
			}
			if (this.AddFilterMenus(this.MasterTableView))
			{
				this.Controls.Add(this.FilterMenu);
			}
			if (this.AddHeaderContextMenus(this.MasterTableView))
			{
				this.Controls.Add(this.HeaderContextMenu);
			}
			base.ChildControlsCreated = true;
			return 1;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0006E39C File Offset: 0x0006C59C
		private void CreateSharedCalendar()
		{
			Panel panel = new Panel();
			panel.ID = "SharedCalendarContainer";
			this.Controls.Add(panel);
			RadCalendar radCalendar = new RadCalendar();
			radCalendar.ID = GridDateTimeColumn._sharedCalendarName;
			radCalendar.RenderMode = base.RenderMode;
			radCalendar.UseColumnHeadersAsSelectors = false;
			radCalendar.UseRowHeadersAsSelectors = false;
			radCalendar.Visible = false;
			panel.Controls.Add(radCalendar);
			radCalendar.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
			radCalendar.EnableAriaSupport = this.EnableAriaSupport;
			radCalendar.PreRender += this.sharedCalendar_PreRender;
			panel.Style["display"] = "none";
			radCalendar.Visible = false;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x0006E44A File Offset: 0x0006C64A
		private void sharedCalendar_PreRender(object sender, EventArgs e)
		{
			((RadCalendar)sender).Skin = base.RuntimeSkin;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x0006E460 File Offset: 0x0006C660
		private void CreateSharedFilterListBox()
		{
			RadGrid.RadListBoxShared radListBoxShared = new RadGrid.RadListBoxShared();
			radListBoxShared.ID = "filterCheckList";
			this.Controls.Add(radListBoxShared);
			radListBoxShared.Height = Unit.Pixel(300);
			radListBoxShared.CheckBoxes = true;
			radListBoxShared.ShowCheckAll = true;
			this.FilterCheckList = radListBoxShared;
			radListBoxShared.ItemsRequested += this.listBox_ItemsRequested;
			radListBoxShared.RenderMode = this.ResolvedRenderMode;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0006E4D0 File Offset: 0x0006C6D0
		internal void listBox_ItemsRequested(object sender, RadListBoxItemsRequestedEventArgs e)
		{
			if (e.Context.Keys.Contains("columnUniqueName") && e.Context.Keys.Contains("tableViewUniqueId"))
			{
				string b = e.Context["columnUniqueName"].ToString();
				string id = e.Context["tableViewUniqueId"].ToString();
				GridTableView gridTableView = this.Page.FindControl(id) as GridTableView;
				if (gridTableView == null)
				{
					gridTableView = this.MasterTableView;
				}
				RadListBox listBox = sender as RadListBox;
				foreach (object obj in gridTableView.Columns)
				{
					GridColumn gridColumn = (GridColumn)obj;
					if (gridColumn.UniqueName == b && gridColumn is IGridDataColumn)
					{
						GridFilterCheckListItemsRequestedEventArgs e2 = new GridFilterCheckListItemsRequestedEventArgs(listBox, gridColumn);
						this.OnFilterCheckListItemsRequested(e2);
					}
				}
				foreach (GridColumn gridColumn2 in gridTableView.AutoGeneratedColumns)
				{
					if (gridColumn2.UniqueName == b && gridColumn2 is IGridDataColumn)
					{
						GridFilterCheckListItemsRequestedEventArgs e3 = new GridFilterCheckListItemsRequestedEventArgs(listBox, gridColumn2);
						this.OnFilterCheckListItemsRequested(e3);
					}
				}
			}
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0006E628 File Offset: 0x0006C828
		internal Control GetProxyRenderControl()
		{
			return new RadGrid.ProxyRenderControl(this);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0006E630 File Offset: 0x0006C830
		internal void RenderListBoxShared(HtmlTextWriter writer)
		{
			(this.FilterCheckList as RadGrid.RadListBoxShared).Render(writer);
			this.FilterCheckList.Visible = false;
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0006E64F File Offset: 0x0006C84F
		private void CreateGroupPanel()
		{
			this.CreateGroupPanel(this);
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0006E658 File Offset: 0x0006C858
		internal void CreateGroupPanel(Control container)
		{
			this.GroupPanel.ID = "GroupPanel";
			if (this.ResolvedRenderMode != RenderMode.Lightweight)
			{
				this.GroupPanel.Style["width"] = "100%";
			}
			container.Controls.Add(this.GroupPanel);
			this.GroupPanel.InitializeIn(this, false);
			this.GroupPanel.Visible = (this.ShowGroupPanel && this.GroupingEnabled);
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0006E6D4 File Offset: 0x0006C8D4
		internal void CreateMobileGroupPanel(Control container)
		{
			if (this.ShowGroupPanel && this.GroupingEnabled)
			{
				if (this.MasterTableView.GroupByExpressions.Count > 0)
				{
					HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
					htmlGenericControl.InnerText = this.MasterTableView.GroupView.ViewGroupsText;
					htmlGenericControl.Attributes["class"] = "rgGroupPanelCollapse";
					container.Controls.Add(htmlGenericControl);
				}
				else if (this.ClientSettings.AllowDragToGroup)
				{
					HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
					htmlGenericControl2.InnerText = this.GroupPanel.Text;
					container.Controls.Add(htmlGenericControl2);
				}
				else
				{
					(container as TableCell).Style["display"] = "none";
				}
				if (this.FindControl(this.MasterTableView.GroupView.ID) == null)
				{
					container.Controls.Add(this.MasterTableView.GroupView);
				}
			}
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0006E7D0 File Offset: 0x0006C9D0
		internal void ObtainDataSource(GridRebindReason rebindReason, bool IsBoundUsingDataSourceId)
		{
			if (!this.DataSourceIsAssigned && !IsBoundUsingDataSourceId)
			{
				if (this.AllowCustomPaging && this.ClientSettings.Virtualization.EnableVirtualization)
				{
					this.OnNeedDataSource(new GridNeedDataSourceEventArgs(rebindReason, 0, this.ClientSettings.Virtualization.RetrievedItemsPerRequest));
					return;
				}
				if (this.AllowCustomPaging)
				{
					this.OnNeedDataSource(new GridNeedDataSourceEventArgs(rebindReason, this.CurrentPageIndex * this.PageSize, this.PageSize));
					return;
				}
				this.OnNeedDataSource(new GridNeedDataSourceEventArgs(rebindReason));
			}
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x0006E858 File Offset: 0x0006CA58
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void AutoDataBind(GridRebindReason rebindReason)
		{
			if (!this.Visible && (rebindReason & GridRebindReason.ExplicitRebind) != GridRebindReason.ExplicitRebind)
			{
				return;
			}
			this.ObtainDataSource(rebindReason, base.IsBoundUsingDataSourceID);
			if ((this.IsClientCommandAssigned && !base.IsBoundUsingDataSourceID && this.DataSource == null && string.IsNullOrEmpty(this.MasterTableView.SelectMethod)) || this.IsBoundUsingOData)
			{
				if (this.MasterTableView.AutoGenerateColumns && this.MasterTableView.Columns.Count == 0)
				{
					throw new NotSupportedException("Client-side data-binding with auto-generated columns is not supported! Please declare at least one column for the grid.");
				}
				DataTable dataTable = new DataTable();
				this.internalTable = dataTable;
				foreach (object obj in this.MasterTableView.Columns)
				{
					GridColumn gridColumn = (GridColumn)obj;
					if (gridColumn is GridBoundColumn)
					{
						string dataField = ((GridBoundColumn)gridColumn).DataField;
						if (!string.IsNullOrEmpty(dataField) && !dataTable.Columns.Contains(dataField))
						{
							dataTable.Columns.Add(new DataColumn(dataField, gridColumn.DataType));
						}
					}
					if (gridColumn is GridTemplateColumn)
					{
						string dataField2 = ((GridTemplateColumn)gridColumn).DataField;
						if (!string.IsNullOrEmpty(dataField2) && !dataTable.Columns.Contains(dataField2))
						{
							dataTable.Columns.Add(new DataColumn(dataField2, gridColumn.DataType));
						}
					}
					if (gridColumn is GridCheckBoxColumn)
					{
						string dataField3 = ((GridCheckBoxColumn)gridColumn).DataField;
						if (!string.IsNullOrEmpty(dataField3) && !dataTable.Columns.Contains(dataField3))
						{
							dataTable.Columns.Add(new DataColumn(dataField3, gridColumn.DataType));
						}
					}
					if (gridColumn is GridDropDownColumn)
					{
						string dataField4 = ((GridDropDownColumn)gridColumn).DataField;
						if (!string.IsNullOrEmpty(dataField4) && !dataTable.Columns.Contains(dataField4))
						{
							dataTable.Columns.Add(new DataColumn(dataField4, gridColumn.DataType));
						}
					}
					if (gridColumn is GridButtonColumn)
					{
						string dataTextField = ((GridButtonColumn)gridColumn).DataTextField;
						if (!string.IsNullOrEmpty(dataTextField) && !dataTable.Columns.Contains(dataTextField))
						{
							dataTable.Columns.Add(new DataColumn(dataTextField, gridColumn.DataType));
						}
					}
					if (gridColumn is GridHyperLinkColumn)
					{
						string dataTextField2 = ((GridHyperLinkColumn)gridColumn).DataTextField;
						if (!string.IsNullOrEmpty(dataTextField2) && !dataTable.Columns.Contains(dataTextField2))
						{
							dataTable.Columns.Add(new DataColumn(dataTextField2, gridColumn.DataType));
						}
						foreach (string text in ((GridHyperLinkColumn)gridColumn).DataNavigateUrlFields)
						{
							if (!string.IsNullOrEmpty(text) && !dataTable.Columns.Contains(text))
							{
								dataTable.Columns.Add(new DataColumn(text, gridColumn.DataType));
							}
						}
					}
					if (gridColumn is GridCalculatedColumn)
					{
						foreach (string text2 in ((GridCalculatedColumn)gridColumn).DataFields)
						{
							if (!string.IsNullOrEmpty(text2) && !dataTable.Columns.Contains(text2))
							{
								dataTable.Columns.Add(new DataColumn(text2, gridColumn.DataType));
							}
						}
					}
				}
				foreach (string text3 in this.MasterTableView.ClientDataKeyNames)
				{
					if (!string.IsNullOrEmpty(text3) && !dataTable.Columns.Contains(text3))
					{
						dataTable.Columns.Add(new DataColumn(text3));
					}
				}
				int num = this.PageSize + 1;
				if (this.ClientSettings.Virtualization.EnableVirtualization)
				{
					num = this.ClientSettings.Virtualization.ItemsPerView;
				}
				for (int l = 0; l < num; l++)
				{
					dataTable.Rows.Add(dataTable.NewRow());
				}
				if (dataTable.Columns.Count > 0)
				{
					this.AutoGenerateColumns = false;
				}
				this.DataSource = dataTable;
				this._isClientBindingDummyDataGenerated = true;
			}
			else if (!base.IsBoundUsingDataSourceID && this.DataSource == null && string.IsNullOrEmpty(this.MasterTableView.SelectMethod))
			{
				this.Controls.Clear();
				if (this.MasterTableView.itemsArray != null)
				{
					this.MasterTableView.itemsArray.Clear();
				}
			}
			if (!string.IsNullOrEmpty(this.MasterTableView.SelectMethod) || (this.DataSource != null && !base.IsBoundUsingDataSourceID) || (this.DataSource != null && rebindReason == GridRebindReason.ExplicitRebind) || (base.IsBoundUsingDataSourceID && rebindReason == GridRebindReason.ExplicitRebind))
			{
				if (rebindReason == GridRebindReason.ExplicitRebind)
				{
					this.dataSourceControlAutomaticDataBindTriggered = false;
				}
				this.DataBind();
			}
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x0006ECFC File Offset: 0x0006CEFC
		public virtual void Rebind()
		{
			this.MasterTableView.SaveExpandCollapseState();
			this.AutoDataBind(GridRebindReason.ExplicitRebind);
			this.MasterTableView.LoadExpandCollapseState();
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0006ED1B File Offset: 0x0006CF1B
		internal override void RegisterForControlState()
		{
			if (!this.IsExporting)
			{
				this.Page.RegisterRequiresControlState(this);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x0006ED31 File Offset: 0x0006CF31
		// (set) Token: 0x0600210B RID: 8459 RVA: 0x0006ED39 File Offset: 0x0006CF39
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsExporting
		{
			get
			{
				return this._isExporting;
			}
			internal set
			{
				this._isExporting = value;
			}
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0006ED42 File Offset: 0x0006CF42
		internal virtual void RebindForExport()
		{
			this.Page.UnregisterRequiresControlState(this);
			this.Page.UnregisterRequiresControlState(this.MasterTableView);
			this.IsExporting = true;
			this.AutoDataBind(GridRebindReason.ExplicitRebind);
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x0006ED6F File Offset: 0x0006CF6F
		internal void EnterDetailDataBinding()
		{
			if (this.IsNeedDataSourceInProgress)
			{
				throw new GridBindingException("You should not call DataBind in NeedDataSource event handler. DataBind would take place automatically right after NeedDataSource handler finishes execution.");
			}
			this.detailDataBindingInProgress++;
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0006ED92 File Offset: 0x0006CF92
		internal void ExitDetailDataBinding()
		{
			this.detailDataBindingInProgress--;
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x0006EDA2 File Offset: 0x0006CFA2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsDetailDataBindingInProgress
		{
			get
			{
				return this.detailDataBindingInProgress > 0;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x0006EDAD File Offset: 0x0006CFAD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool DataSourceIsAssigned
		{
			get
			{
				return this.DataSource != null || base.IsBoundUsingDataSourceID;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x0006EDC0 File Offset: 0x0006CFC0
		internal bool IsClientCommandAssigned
		{
			get
			{
				return !string.IsNullOrEmpty(this.ClientSettings.ClientEvents.OnCommand) || !string.IsNullOrEmpty(this.ClientSettings.ClientEvents.OnUserAction) || this.ClientSettings.DataBinding.IsSet || this.virtualizationTemporaryCommandAssign || this.ClientSettings.Virtualization.EnableVirtualization || !string.IsNullOrEmpty(this.ClientDataSourceID);
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x0006EE37 File Offset: 0x0006D037
		// (set) Token: 0x06002113 RID: 8467 RVA: 0x0006EE3F File Offset: 0x0006D03F
		[DefaultValue("")]
		[Themeable(false)]
		[Description("DataSourceID property")]
		[Category("Data")]
		public override string DataSourceID
		{
			get
			{
				return base.DataSourceID;
			}
			set
			{
				base.DataSourceID = value;
				this.MasterTableView.DataSourceID = value;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x0006EE54 File Offset: 0x0006D054
		// (set) Token: 0x06002115 RID: 8469 RVA: 0x0006EE74 File Offset: 0x0006D074
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("Gets or sets ID of RadClientDataSource control that to be used for client side binding")]
		[DefaultValue("")]
		public virtual string ClientDataSourceID
		{
			get
			{
				return ((string)this.ViewState["ClientDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientDataSourceID"] = value;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x0006EE87 File Offset: 0x0006D087
		protected virtual bool IsBoundUsingOData
		{
			get
			{
				return !string.IsNullOrEmpty(this.ODataDataSourceID);
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x0006EE97 File Offset: 0x0006D097
		// (set) Token: 0x06002118 RID: 8472 RVA: 0x0006EEB7 File Offset: 0x0006D0B7
		[Category("Data")]
		[Description("Gets or sets the ODataDataSource used for data binding.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string ODataDataSourceID
		{
			get
			{
				return ((string)this.ViewState["ODataDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ODataDataSourceID"] = value;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x0006EECA File Offset: 0x0006D0CA
		// (set) Token: 0x0600211A RID: 8474 RVA: 0x0006EEEA File Offset: 0x0006D0EA
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataModelID
		{
			get
			{
				return (string)(this.ViewState["DataModelID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataModelID"] = value;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x0006EF00 File Offset: 0x0006D100
		// (set) Token: 0x0600211C RID: 8476 RVA: 0x0006EF5D File Offset: 0x0006D15D
		internal string applicationPath
		{
			get
			{
				if (string.IsNullOrEmpty(this._applicationPath))
				{
					this._applicationPath = HttpContext.Current.Request.ApplicationPath;
					if (!this._applicationPath.EndsWith("/"))
					{
						this._applicationPath += "/";
					}
				}
				return this._applicationPath;
			}
			set
			{
				this._applicationPath = value;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x0006EF68 File Offset: 0x0006D168
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ViewStateSize
		{
			get
			{
				if (this._viewStateSize == 0)
				{
					try
					{
						this._viewStateSize = this.GetViewStateSize();
					}
					catch
					{
						this._viewStateSize = -1;
					}
				}
				return this._viewStateSize;
			}
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0006EFAC File Offset: 0x0006D1AC
		private int GetViewStateSize()
		{
			Type typeFromHandle = typeof(Control);
			MethodInfo method = typeFromHandle.GetMethod("SaveViewStateRecursive", BindingFlags.Instance | BindingFlags.NonPublic);
			object value = method.Invoke(this, new object[]
			{
				ViewStateMode.Enabled
			});
			StringWriter stringWriter = new StringWriter();
			LosFormatter losFormatter = new LosFormatter();
			losFormatter.Serialize(stringWriter, value);
			return stringWriter.ToString().Length;
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x0006F00F File Offset: 0x0006D20F
		// (set) Token: 0x06002120 RID: 8480 RVA: 0x0006F017 File Offset: 0x0006D217
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				base.Skin = value;
			}
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0006F020 File Offset: 0x0006D220
		public virtual void SetStyleClasses()
		{
			if (!base.EmptySkin())
			{
				this.CssClass = this.FormatCssClass("RadGrid", this.CssClass);
				this.MasterTableView.CssClass = this.FormatCssClass("rgMasterTable", this.MasterTableView.CssClass);
				this.MasterTableView.RowIndicatorColumn.HeaderStyle.CssClass = this.FormatCssClass("rgResizeCol", this.MasterTableView.RowIndicatorColumn.HeaderStyle.CssClass);
				this.MasterTableView.RowIndicatorColumn.ItemStyle.CssClass = this.FormatCssClass("rgResizeCol", this.MasterTableView.RowIndicatorColumn.ItemStyle.CssClass);
				this.ItemStyle.CssClass = this.FormatCssClass("rgRow", this.ItemStyle.CssClass);
				this.HeaderStyle.CssClass = this.FormatCssClass("rgHeader", this.HeaderStyle.CssClass);
				this.MasterTableView.RenderHeaderStyle.CssClass = this.FormatCssClass("rgHeader", this.MasterTableView.RenderHeaderStyle.CssClass);
				this.PagerStyle.CssClass = this.FormatCssClass("rgPager", this.PagerStyle.CssClass);
				this.MasterTableView.RenderPagerStyle.CssClass = this.FormatCssClass("rgPager", this.MasterTableView.RenderPagerStyle.CssClass);
				this.FooterStyle.CssClass = this.FormatCssClass("rgFooter", this.FooterStyle.CssClass);
				this.MasterTableView.RenderFooterStyle.CssClass = this.FormatCssClass("rgFooter", this.MasterTableView.RenderFooterStyle.CssClass);
				this.SelectedItemStyle.CssClass = this.FormatCssClass("rgSelectedRow", this.SelectedItemStyle.CssClass);
				this.MasterTableView.SelectedItemStyle.CssClass = this.FormatCssClass("rgSelectedRow", this.MasterTableView.SelectedItemStyle.CssClass);
				this.ActiveItemStyle.CssClass = this.FormatCssClass("rgActiveRow", this.ActiveItemStyle.CssClass);
				this.MasterTableView.RenderActiveItemStyle.CssClass = this.FormatCssClass("rgActiveRow", this.MasterTableView.RenderActiveItemStyle.CssClass);
				this.GroupHeaderItemStyle.CssClass = this.FormatCssClass("rgGroupHeader", this.GroupHeaderItemStyle.CssClass);
				this.MasterTableView.RenderGroupHeaderItemStyle.CssClass = this.FormatCssClass("rgGroupHeader", this.MasterTableView.RenderGroupHeaderItemStyle.CssClass);
				this.EditItemStyle.CssClass = this.FormatCssClass("rgEditRow", this.EditItemStyle.CssClass);
				this.MasterTableView.RenderEditItemStyle.CssClass = this.FormatCssClass("rgEditRow", this.MasterTableView.RenderEditItemStyle.CssClass);
				this.CommandItemStyle.CssClass = this.FormatCssClass("rgCommandRow", this.CommandItemStyle.CssClass);
				this.MasterTableView.RenderCommandItemStyle.CssClass = this.FormatCssClass("rgCommandRow", this.MasterTableView.RenderCommandItemStyle.CssClass);
				this.FilterItemStyle.CssClass = this.FormatCssClass("rgFilterRow", this.FilterItemStyle.CssClass);
				this.MasterTableView.RenderFilterItemStyle.CssClass = this.FormatCssClass("rgFilterRow", this.MasterTableView.RenderFilterItemStyle.CssClass);
				this.MultiHeaderItemStyle.CssClass = this.FormatCssClass("rgMultiHeaderRow", this.MultiHeaderItemStyle.CssClass);
				this.MasterTableView.RenderMultiHeaderItemStyle.CssClass = this.FormatCssClass("rgMultiHeaderRow", this.MasterTableView.RenderMultiHeaderItemStyle.CssClass);
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0006F3F4 File Offset: 0x0006D5F4
		internal string FormatCssClass(string prefix, string userDefined)
		{
			string text = prefix;
			if (prefix == "RadGrid")
			{
				text = string.Concat(new string[]
				{
					prefix,
					" ",
					prefix,
					"_",
					base.RuntimeSkin
				});
				if (this.MasterTableView.HasMultiHeaders)
				{
					text += " rgMultiHeader";
				}
			}
			if (userDefined.IndexOf(text) >= 0)
			{
				return userDefined;
			}
			if (string.IsNullOrEmpty(userDefined))
			{
				return text;
			}
			return string.Format("{0} {1}", text, userDefined);
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x0006F47C File Offset: 0x0006D67C
		// (set) Token: 0x06002124 RID: 8484 RVA: 0x0006F4A5 File Offset: 0x0006D6A5
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Allow filtering")]
		public virtual bool AllowFilteringByColumn
		{
			get
			{
				object obj = this.ViewState["_afbc"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["_afbc"] = value;
				if (this.MasterTableView != null && !value)
				{
					this.MasterTableView.FilterExpression = "";
				}
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x0006F4D8 File Offset: 0x0006D6D8
		// (set) Token: 0x06002126 RID: 8486 RVA: 0x0006F512 File Offset: 0x0006D712
		[DefaultValue(GridFilterType.Classic)]
		[Description("Change the filter type displayed in the filter dropdown")]
		[NotifyParentProperty(true)]
		public virtual GridFilterType FilterType
		{
			get
			{
				GridFilterType? gridFilterType = this.ViewState["_ft"] as GridFilterType?;
				if (gridFilterType != null)
				{
					return gridFilterType.Value;
				}
				return GridFilterType.Classic;
			}
			set
			{
				this.ViewState["_ft"] = value;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x0006F52C File Offset: 0x0006D72C
		// (set) Token: 0x06002128 RID: 8488 RVA: 0x0006F555 File Offset: 0x0006D755
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Set to True to enable header context menu")]
		public virtual bool EnableHeaderContextMenu
		{
			get
			{
				object obj = this.ViewState["_enhcm"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["_enhcm"] = value;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x0006F570 File Offset: 0x0006D770
		// (set) Token: 0x0600212A RID: 8490 RVA: 0x0006F5A3 File Offset: 0x0006D7A3
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Set to True to enable columns aggregates option in header context menu")]
		public virtual bool EnableHeaderContextAggregatesMenu
		{
			get
			{
				if (!this.EnableHeaderContextMenu)
				{
					return false;
				}
				object obj = this.ViewState["EnableHeaderContextAggregatesMenu"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableHeaderContextAggregatesMenu"] = value;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x0006F5BC File Offset: 0x0006D7BC
		// (set) Token: 0x0600212C RID: 8492 RVA: 0x0006F5ED File Offset: 0x0006D7ED
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Set to True to enable header context filter menu")]
		[DefaultValue(false)]
		public virtual bool EnableHeaderContextFilterMenu
		{
			get
			{
				if (this.EnableHeaderContextMenu)
				{
					object obj = this.ViewState["_enhcfm"];
					if (obj != null)
					{
						return (bool)obj;
					}
				}
				return false;
			}
			set
			{
				this.ViewState["_enhcfm"] = value;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x0006F608 File Offset: 0x0006D808
		// (set) Token: 0x0600212E RID: 8494 RVA: 0x0006F631 File Offset: 0x0006D831
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_SortHeaderContextMenuColumns")]
		[DefaultValue(false)]
		public virtual bool SortHeaderContextMenuColumns
		{
			get
			{
				object obj = this.ViewState["SortHeaderContextMenuColumns"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["SortHeaderContextMenuColumns"] = value;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x0006F649 File Offset: 0x0006D849
		// (set) Token: 0x06002130 RID: 8496 RVA: 0x0006F678 File Offset: 0x0006D878
		[DefaultValue("")]
		[Description("Specifies the default path for all grid images, used as buttons or indicators.")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public virtual string ImagesPath
		{
			get
			{
				if (this.ViewState["ImagesPath"] == null)
				{
					return "";
				}
				return (string)this.ViewState["ImagesPath"];
			}
			set
			{
				this.ViewState["ImagesPath"] = value;
			}
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0006F68B File Offset: 0x0006D88B
		internal string ResolveGridImageUrl(string imageName)
		{
			return this.ResolveGridImageUrl(imageName, true);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0006F698 File Offset: 0x0006D898
		internal string ResolveGridImageUrl(string imageName, bool canBeSpriteButton)
		{
			if (string.IsNullOrEmpty(this.ImagesPath.Trim()) && !base.EmptySkin() && canBeSpriteButton)
			{
				return "";
			}
			if (!string.IsNullOrEmpty(this.ImagesPath.Trim()) || !this.EnableEmbeddedSkins)
			{
				return base.ResolveUrl(Path.Combine(this.ImagesPath.Trim(), imageName));
			}
			if (base.EmptySkin())
			{
				return SkinRegistrar.GetWebResourceUrl(this, string.Format("Telerik.Web.UI.Skins.Default.Grid.{0}", imageName));
			}
			return SkinRegistrar.GetWebResourceUrl(this, string.Format("Telerik.Web.UI.Skins.{0}.Grid.{1}", base.RuntimeSkin, imageName));
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0006F72B File Offset: 0x0006D92B
		internal bool ShouldSerializeImageUrl(string imageUrl)
		{
			return !string.IsNullOrEmpty(imageUrl) && !imageUrl.StartsWith("mvwres:");
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0006F745 File Offset: 0x0006D945
		internal bool ShouldRenderImg(string imageUrl)
		{
			return (!string.IsNullOrEmpty(imageUrl.Trim()) && !imageUrl.Contains("WebResource.axd?")) || base.EmptySkin();
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x0006F76C File Offset: 0x0006D96C
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0006F774 File Offset: 0x0006D974
		public static GridBindingData GetBindingData(string contextTypeName, string tableName, int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			GridLinqDataSource gridLinqDataSource = new GridLinqDataSource(contextTypeName, tableName, "", startRowIndex, maximumRows, sortExpression, filterExpression);
			Pair data = gridLinqDataSource.GetData();
			return new GridBindingData(((IEnumerable)data.First).OfType<object>().ToList<object>(), (int)data.Second);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0006F7C0 File Offset: 0x0006D9C0
		public static GridBindingData GetBindingData(string contextTypeName, string tableName, string select, int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			GridLinqDataSource gridLinqDataSource = new GridLinqDataSource(contextTypeName, tableName, select, startRowIndex, maximumRows, sortExpression, filterExpression);
			Pair data = gridLinqDataSource.GetData();
			return new GridBindingData(((IEnumerable)data.First).OfType<object>().ToList<object>(), (int)data.Second);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0006F80C File Offset: 0x0006DA0C
		public static GridLinqBindingData<T> GetBindingData<T>(IQueryable<T> source, int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			GridLinqBindingData bindingData = RadGrid.GetBindingData(source, startRowIndex, maximumRows, sortExpression, filterExpression);
			return new GridLinqBindingData<T>((IQueryable<T>)bindingData.Data, bindingData.Count);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0006F83C File Offset: 0x0006DA3C
		public static GridLinqBindingData GetBindingData(IQueryable source, int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			if (!string.IsNullOrEmpty(filterExpression))
			{
				source = source.Where(filterExpression, new object[0]);
			}
			int count = source.Count();
			if (!string.IsNullOrEmpty(sortExpression))
			{
				source = source.OrderBy(sortExpression, new object[0]);
			}
			source = source.Skip(startRowIndex).Take(maximumRows);
			return new GridLinqBindingData(source, count);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0006F898 File Offset: 0x0006DA98
		protected virtual void OnFieldDescriptorsReady(RadFilterFildDesciptorsEventArgs e)
		{
			EventHandler<RadFilterFildDesciptorsEventArgs> eventHandler = base.Events[RadGrid.EventFieldDescriptorsReady] as EventHandler<RadFilterFildDesciptorsEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x0600213B RID: 8507 RVA: 0x0006F8C6 File Offset: 0x0006DAC6
		// (remove) Token: 0x0600213C RID: 8508 RVA: 0x0006F8D9 File Offset: 0x0006DAD9
		event EventHandler<RadFilterFildDesciptorsEventArgs> IRadFilterableContainer.FieldDescriptorsReady
		{
			add
			{
				base.Events.AddHandler(RadGrid.EventFieldDescriptorsReady, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGrid.EventFieldDescriptorsReady, value);
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0006F8EC File Offset: 0x0006DAEC
		private RadFilterGridContext CreateFilterableContext()
		{
			RadFilterGridContext radFilterGridContext = new RadFilterGridContext();
			if (this.MasterTableView.IsOpenAccessDataSourceView())
			{
				radFilterGridContext.ExpressionType = GridFilterExpressionType.Oql;
				return radFilterGridContext;
			}
			if (this.MasterTableView.IsBoundToForwardOnly || (!this.MasterTableView.OwnerGrid.EnableLinqExpressions && !this.MasterTableView.IsDataSourceViewWithFiltering()))
			{
				radFilterGridContext.ExpressionType = GridFilterExpressionType.Sql;
				return radFilterGridContext;
			}
			Type queryableElementType = this.MasterTableView.QueryableElementType;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = this.MasterTableView.HasCalculatedColumns();
			if ((queryableElementType == typeof(DataRowView) || queryableElementType == typeof(DataRow) || queryableElementType.GetInterface("IDataRecord") != null) && !flag3)
			{
				flag = true;
				if (this.MasterTableView.IsEntityDataSourceView() && this.MasterTableView.OwnerGrid.EnableLinqExpressions)
				{
					flag = false;
				}
			}
			if (GridBaseDataList.IsBindableType(queryableElementType) && !flag3)
			{
				flag2 = true;
			}
			if (flag)
			{
				radFilterGridContext.ExpressionType = GridFilterExpressionType.RowLinq;
			}
			else if (flag2)
			{
				radFilterGridContext.ExpressionType = GridFilterExpressionType.BindableType;
			}
			else if (flag3)
			{
				radFilterGridContext.ExpressionType = GridFilterExpressionType.CalculatedColumns;
			}
			else if (this.MasterTableView.IsEntityDataSourceView())
			{
				radFilterGridContext.ExpressionType = GridFilterExpressionType.EntitySql;
			}
			else
			{
				radFilterGridContext.ExpressionType = GridFilterExpressionType.DLinq;
			}
			return radFilterGridContext;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0006FEB4 File Offset: 0x0006E0B4
		private IEnumerable<RadFilterFieldDescriptor> CreateColumnDescriptors()
		{
			Dictionary<string, Pair> dict = new Dictionary<string, Pair>();
			TAction<string, Type, string> insertItem = delegate(string name, Type type, string displayName)
			{
				if (!dict.ContainsKey(name))
				{
					Pair value2 = new Pair(type, displayName);
					dict.Add(name, value2);
				}
			};
			GridColumn[] columns = this.MasterTableView.RenderColumns;
			foreach (GridColumn gridColumn in columns)
			{
				if (gridColumn is GridBoundColumn)
				{
					if (((GridBoundColumn)gridColumn).AllowFiltering)
					{
						insertItem(((GridBoundColumn)gridColumn).DataField, gridColumn.DataType, gridColumn.HeaderText);
					}
				}
				else if (gridColumn is GridTemplateColumn)
				{
					if (((GridTemplateColumn)gridColumn).AllowFiltering)
					{
						insertItem(((GridTemplateColumn)gridColumn).DataField, gridColumn.DataType, gridColumn.HeaderText);
					}
				}
				else if (gridColumn is GridBinaryImageColumn)
				{
					if (((GridBinaryImageColumn)gridColumn).AllowFiltering)
					{
						insertItem(((GridBinaryImageColumn)gridColumn).DataAlternateTextField, gridColumn.DataType, gridColumn.HeaderText);
					}
				}
				else if (gridColumn is GridCheckBoxColumn)
				{
					if (((GridCheckBoxColumn)gridColumn).AllowFiltering)
					{
						insertItem(((GridCheckBoxColumn)gridColumn).DataField, gridColumn.DataType, gridColumn.HeaderText);
					}
				}
				else if (gridColumn is GridRatingColumn)
				{
					if (((GridRatingColumn)gridColumn).AllowFiltering)
					{
						insertItem(((GridRatingColumn)gridColumn).DataField, gridColumn.DataType, gridColumn.HeaderText);
					}
				}
				else if (gridColumn is GridDropDownColumn)
				{
					if (((GridDropDownColumn)gridColumn).AllowFiltering)
					{
						insertItem(((GridDropDownColumn)gridColumn).DataField, gridColumn.DataType, gridColumn.HeaderText);
					}
				}
				else if (gridColumn is GridHyperLinkColumn)
				{
					if (((GridHyperLinkColumn)gridColumn).AllowFiltering)
					{
						insertItem(((GridHyperLinkColumn)gridColumn).DataTextField, gridColumn.DataType, gridColumn.HeaderText);
					}
				}
				else if (gridColumn is GridImageColumn)
				{
					if (((GridImageColumn)gridColumn).AllowFiltering)
					{
						insertItem(((GridImageColumn)gridColumn).DataAlternateTextField, gridColumn.DataType, gridColumn.HeaderText);
					}
				}
				else if (gridColumn is GridCalculatedColumn)
				{
					GridCalculatedColumn gridCalculatedColumn = (GridCalculatedColumn)gridColumn;
					if (gridCalculatedColumn.AllowFiltering)
					{
						insertItem(gridCalculatedColumn.GetResultFieldName(), gridCalculatedColumn.DataType, gridColumn.HeaderText);
					}
				}
			}
			foreach (KeyValuePair<string, Pair> item in dict)
			{
				KeyValuePair<string, Pair> keyValuePair = item;
				Pair value = keyValuePair.Value;
				KeyValuePair<string, Pair> keyValuePair2 = item;
				yield return new RadFilterFieldDescriptor(keyValuePair2.Key, (Type)value.First, value.Second.ToString());
			}
			yield break;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x0006FED4 File Offset: 0x0006E0D4
		internal void UpdateFilterControl()
		{
			if (this.MasterTableView._resolvedDataSource != null)
			{
				RadFilterFilterableView radFilterFilterableView = new RadFilterFilterableView();
				((List<RadFilterFieldDescriptor>)radFilterFilterableView.DataFields).AddRange(this.CreateColumnDescriptors());
				if (!this.IsExporting)
				{
					this.OnFieldDescriptorsReady(new RadFilterFildDesciptorsEventArgs(radFilterFilterableView));
				}
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x0006FF20 File Offset: 0x0006E120
		void IRadFilterableContainer.ApplyFilterExpressions(RadFilterGroupExpression expressionRoot, bool shouldBind)
		{
			if (shouldBind)
			{
				this.MasterTableView.FilterExpression = string.Empty;
				if (!expressionRoot.IsEmpty)
				{
					RadFilterGridContext context = this.CreateFilterableContext();
					RadFilterGridQueryProvider radFilterGridQueryProvider = new RadFilterGridQueryProvider(context);
					radFilterGridQueryProvider.IsCaseSensitive = this.GroupingSettings.CaseSensitive;
					radFilterGridQueryProvider.ProcessGroup(expressionRoot);
					string result = radFilterGridQueryProvider.Result;
					if (result.Length > 0)
					{
						this.MasterTableView.FilterExpression = result;
					}
				}
				this.SelectedIndexes.Clear();
				this.EditIndexes.Clear();
				this.CurrentPageIndex = 0;
				this.MasterTableView.Rebind();
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x0006FFB5 File Offset: 0x0006E1B5
		// (set) Token: 0x06002142 RID: 8514 RVA: 0x0006FFD8 File Offset: 0x0006E1D8
		[Category("Appearance")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				bool flag = false;
				if (value != this.ViewState["Culture"])
				{
					flag = true;
					this._localization = null;
				}
				this.ViewState["Culture"] = value;
				if (flag && this.AllowFilteringByColumn)
				{
					this.CreateFilterMenuItems(this.FilterMenu);
				}
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0007002C File Offset: 0x0006E22C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal GridStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new GridStrings(new LocalizationProvider("RadGrid.Main", this, base.DesignMode ? "" : this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x00070085 File Offset: 0x0006E285
		// (set) Token: 0x06002145 RID: 8517 RVA: 0x000700A8 File Offset: 0x0006E2A8
		[Category("Misc")]
		[Description("Gets or sets a value indicating where RadGrid will look for its .resx localization files.")]
		[DefaultValue("")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x000700FB File Offset: 0x0006E2FB
		// (set) Token: 0x06002147 RID: 8519 RVA: 0x0007011C File Offset: 0x0006E31C
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true enables support for WAI-ARIA")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x00070134 File Offset: 0x0006E334
		// (set) Token: 0x06002149 RID: 8521 RVA: 0x0007015D File Offset: 0x0006E35D
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the hierarchy expand/collapse all header buttons should be switched on.")]
		public virtual bool EnableHierarchyExpandAll
		{
			get
			{
				object obj = this.ViewState["EnableHierarchyExpandAll"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableHierarchyExpandAll"] = value;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x00070178 File Offset: 0x0006E378
		// (set) Token: 0x0600214B RID: 8523 RVA: 0x000701A1 File Offset: 0x0006E3A1
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the groups expand/collapse all header buttons should be switched on.")]
		public virtual bool EnableGroupsExpandAll
		{
			get
			{
				object obj = this.ViewState["EnableGroupsExpandAll"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableGroupsExpandAll"] = value;
			}
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000701B9 File Offset: 0x0006E3B9
		protected internal virtual List<DataColumn> ParseSPViewFieldsIntoDataColumns<T>(T firstObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000701C0 File Offset: 0x0006E3C0
		protected internal virtual object GetSPViewFieldValue<T>(T resultItem, string fieldName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000701C8 File Offset: 0x0006E3C8
		public void RaiseCallbackEvent(string eventArgument)
		{
			this.callbackArguments = eventArgument.Split(new string[]
			{
				"$$"
			}, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000701F4 File Offset: 0x0006E3F4
		public string GetCallbackResult()
		{
			string a;
			if ((a = this.callbackArguments[0]) != null && a == "getData")
			{
				return this.MasterTableView.GetJsonData(int.Parse(this.callbackArguments[2]), int.Parse(this.callbackArguments[3]));
			}
			return string.Empty;
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00070264 File Offset: 0x0006E464
		internal void TrackPaging(int pageIndex)
		{
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "Paging").OfPriority(FeaturePriority.High).OfClass(FeatureClass.Other).OfValue(() => pageIndex.ToString()));
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000702E4 File Offset: 0x0006E4E4
		internal void TrackSorting(string sortExpression)
		{
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "Sorting").OfPriority(FeaturePriority.High).OfClass(FeatureClass.DataOperation).OfValue(() => sortExpression));
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00070364 File Offset: 0x0006E564
		internal void TrackFiltering(string columnName, string filterFunction, object value)
		{
			string filterExpression = string.Format("[{0}]:[{1}]:[{2}]", columnName, filterFunction, value);
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "Filtering").OfPriority(FeaturePriority.High).OfClass(FeatureClass.DataOperation).OfValue(() => filterExpression));
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000703F0 File Offset: 0x0006E5F0
		internal void TrackSelection(string dataKeyValue, bool selected)
		{
			string selectString = string.Format("[{0}]:[{1}]", dataKeyValue, selected);
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "Selection").OfPriority(FeaturePriority.High).OfClass(FeatureClass.Selection).OfValue(() => selectString));
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00070480 File Offset: 0x0006E680
		internal void TrackExport(string exportType)
		{
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "Export").OfPriority(FeaturePriority.High).OfClass(FeatureClass.Other).OfValue(() => exportType));
		}

		// Token: 0x04000818 RID: 2072
		public const string CancelCommandName = "Cancel";

		// Token: 0x04000819 RID: 2073
		public const string DeleteCommandName = "Delete";

		// Token: 0x0400081A RID: 2074
		public const string EditCommandName = "Edit";

		// Token: 0x0400081B RID: 2075
		public const string InitInsertCommandName = "InitInsert";

		// Token: 0x0400081C RID: 2076
		public const string PerformInsertCommandName = "PerformInsert";

		// Token: 0x0400081D RID: 2077
		public const string RebindGridCommandName = "RebindGrid";

		// Token: 0x0400081E RID: 2078
		public const string FirstPageCommandArgument = "First";

		// Token: 0x0400081F RID: 2079
		public const string LastPageCommandArgument = "Last";

		// Token: 0x04000820 RID: 2080
		public const string NextPageCommandArgument = "Next";

		// Token: 0x04000821 RID: 2081
		public const string PageCommandName = "Page";

		// Token: 0x04000822 RID: 2082
		public const string PrevPageCommandArgument = "Prev";

		// Token: 0x04000823 RID: 2083
		public const string SelectCommandName = "Select";

		// Token: 0x04000824 RID: 2084
		public const string SortCommandName = "Sort";

		// Token: 0x04000825 RID: 2085
		public const string HeaderSortCommandName = "HeaderSort";

		// Token: 0x04000826 RID: 2086
		public const string ClearSortCommandName = "ClearSort";

		// Token: 0x04000827 RID: 2087
		public const string ExportToExcelCommandName = "ExportToExcel";

		// Token: 0x04000828 RID: 2088
		public const string ExportToWordCommandName = "ExportToWord";

		// Token: 0x04000829 RID: 2089
		public const string ExportToPdfCommandName = "ExportToPdf";

		// Token: 0x0400082A RID: 2090
		public const string ExportToCsvCommandName = "ExportToCsv";

		// Token: 0x0400082B RID: 2091
		public const string UpdateCommandName = "Update";

		// Token: 0x0400082C RID: 2092
		public const string ExpandCollapseCommandName = "ExpandCollapse";

		// Token: 0x0400082D RID: 2093
		public const string ExpandCollapseAllCommandName = "ExpandCollapseAll";

		// Token: 0x0400082E RID: 2094
		public const string GroupsExpandAllCommandName = "GroupsExpandAll";

		// Token: 0x0400082F RID: 2095
		public const string GroupsCustomExpandCollapseCommandName = "GroupsCustomExpandCollapse";

		// Token: 0x04000830 RID: 2096
		public const string DeselectCommandName = "Deselect";

		// Token: 0x04000831 RID: 2097
		public const string FilterCommandName = "Filter";

		// Token: 0x04000832 RID: 2098
		public const string ClearFilterCommandName = "ClearFilter";

		// Token: 0x04000833 RID: 2099
		public const string EditSelectedCommandName = "EditSelected";

		// Token: 0x04000834 RID: 2100
		public const string EditAllCommandName = "EditAll";

		// Token: 0x04000835 RID: 2101
		public const string UpdateEditedCommandName = "UpdateEdited";

		// Token: 0x04000836 RID: 2102
		public const string CancelAllCommandName = "CancelAll";

		// Token: 0x04000837 RID: 2103
		public const string DeleteSelectedCommandName = "DeleteSelected";

		// Token: 0x04000838 RID: 2104
		public const string DownloadAttachmentCommandName = "DownloadAttachment";

		// Token: 0x04000839 RID: 2105
		public const string HeaderContextMenuFilterCommandName = "HeaderContextMenuFilter";

		// Token: 0x0400083A RID: 2106
		public const string BatchEditCommandName = "BatchEdit";

		// Token: 0x0400083B RID: 2107
		private GridGroupingSettings _groupingSettings;

		// Token: 0x0400083C RID: 2108
		private GridSortingSettings _sortingSettings;

		// Token: 0x0400083D RID: 2109
		private GridHierarchySettings _hierarchySettings;

		// Token: 0x0400083E RID: 2110
		private GridExportSettings _exportSettings;

		// Token: 0x0400083F RID: 2111
		private GridValidationSettings _validationSettings;

		// Token: 0x04000840 RID: 2112
		private bool suppressOnDataBoundEvent = true;

		// Token: 0x04000841 RID: 2113
		internal bool ShouldCallOnSelectedCellChanged;

		// Token: 0x04000842 RID: 2114
		internal bool ShouldCallOnSelectedIndexChanged;

		// Token: 0x04000843 RID: 2115
		internal bool RowClickOnly = true;

		// Token: 0x04000844 RID: 2116
		internal bool ShouldCallClientDeleteCommand;

		// Token: 0x04000845 RID: 2117
		internal bool _shouldCallColumnsReorder;

		// Token: 0x04000846 RID: 2118
		internal string deletedRows = "";

		// Token: 0x04000847 RID: 2119
		internal static readonly string FilteredClassName = "rgFiltered";

		// Token: 0x04000848 RID: 2120
		private string _controlToFocus = string.Empty;

		// Token: 0x04000849 RID: 2121
		internal List<string> _popUpIds = new List<string>();

		// Token: 0x0400084A RID: 2122
		internal List<IDictionary> _gridTableViewsData = new List<IDictionary>();

		// Token: 0x0400084B RID: 2123
		private HashSet<string> batchEditingOpenForEditEvents;

		// Token: 0x0400084C RID: 2124
		private bool IsNeedDataSourceInProgress;

		// Token: 0x0400084D RID: 2125
		internal bool isBoundUsingNeedDataSource;

		// Token: 0x0400084E RID: 2126
		private GridClientSettings _clientSettins;

		// Token: 0x0400084F RID: 2127
		private GridTableView _masterTableView;

		// Token: 0x04000850 RID: 2128
		internal int _defaultPageSize = 10;

		// Token: 0x04000851 RID: 2129
		private int currIndexHierarchical;

		// Token: 0x04000852 RID: 2130
		private GridIndexCollection _selectedCellIndexes;

		// Token: 0x04000853 RID: 2131
		private GridIndexCollection _clientUnselectableIndexes;

		// Token: 0x04000854 RID: 2132
		private GridStatusBarItemSettings _statusBarItemSettings;

		// Token: 0x04000855 RID: 2133
		private GridFilterMenu _filterMenu;

		// Token: 0x04000856 RID: 2134
		internal RadListBox FilterCheckList;

		// Token: 0x04000857 RID: 2135
		private GridHeaderContextMenu _headerContextMenu;

		// Token: 0x04000858 RID: 2136
		internal string _savedViewStateAsync;

		// Token: 0x04000859 RID: 2137
		private bool _designTimePopulatColumns;

		// Token: 0x0400085A RID: 2138
		private Hashtable groupColState;

		// Token: 0x0400085B RID: 2139
		private Hashtable hierarchyColsExpandedState;

		// Token: 0x0400085C RID: 2140
		private object[] deletedItems;

		// Token: 0x0400085D RID: 2141
		private object[] reorderedColumns;

		// Token: 0x0400085E RID: 2142
		private string[] hiddenColumns;

		// Token: 0x0400085F RID: 2143
		private string[] showedColumns;

		// Token: 0x04000860 RID: 2144
		private object[] draggedItemsIndexes;

		// Token: 0x04000861 RID: 2145
		internal Dictionary<string, Pair> _popUpLocations = new Dictionary<string, Pair>();

		// Token: 0x04000862 RID: 2146
		private bool shouldFocusOnPage;

		// Token: 0x04000863 RID: 2147
		internal bool dataSourceControlAutomaticDataBindTriggered;

		// Token: 0x04000864 RID: 2148
		internal bool _isClientBindingDummyDataGenerated;

		// Token: 0x04000865 RID: 2149
		internal DataTable internalTable;

		// Token: 0x04000866 RID: 2150
		private bool _isExporting;

		// Token: 0x04000867 RID: 2151
		private int detailDataBindingInProgress;

		// Token: 0x04000868 RID: 2152
		internal bool virtualizationTemporaryCommandAssign;

		// Token: 0x04000869 RID: 2153
		private GridIndexCollection _editIndexes;

		// Token: 0x0400086A RID: 2154
		private GridIndexCollection _selectedIndexes;

		// Token: 0x0400086B RID: 2155
		private GridGroupPanel _groupPanel;

		// Token: 0x0400086C RID: 2156
		private static readonly object EventNeedDataSource = new object();

		// Token: 0x0400086D RID: 2157
		private static readonly object EventFilterCheckListItemsRequested = new object();

		// Token: 0x0400086E RID: 2158
		private static readonly object EventDetailTableDataBind = new object();

		// Token: 0x0400086F RID: 2159
		private static readonly object EventColumnsReorder = new object();

		// Token: 0x04000870 RID: 2160
		private static readonly object EventCancelCommand = new object();

		// Token: 0x04000871 RID: 2161
		private static readonly object EventCreateColumnEditor = new object();

		// Token: 0x04000872 RID: 2162
		private static readonly object EventDeleteCommand = new object();

		// Token: 0x04000873 RID: 2163
		private static readonly object EventEditCommand = new object();

		// Token: 0x04000874 RID: 2164
		private static readonly object EventItemCommand = new object();

		// Token: 0x04000875 RID: 2165
		private static readonly object EventCustomAggregate;

		// Token: 0x04000876 RID: 2166
		private static readonly object EventItemCreated = new object();

		// Token: 0x04000877 RID: 2167
		private static readonly object EventColumnCreated;

		// Token: 0x04000878 RID: 2168
		private static readonly object EventColumnCreating;

		// Token: 0x04000879 RID: 2169
		private static readonly object EventItemDataBound;

		// Token: 0x0400087A RID: 2170
		private static readonly object EventPageIndexChanged;

		// Token: 0x0400087B RID: 2171
		private static readonly object EventPageSizeChanged;

		// Token: 0x0400087C RID: 2172
		private static readonly object EventSortCommand;

		// Token: 0x0400087D RID: 2173
		private static readonly object EventUpdateCommand;

		// Token: 0x0400087E RID: 2174
		private static readonly object EventInsertCommand;

		// Token: 0x0400087F RID: 2175
		private static readonly object EventItemEvent;

		// Token: 0x04000880 RID: 2176
		private static readonly object EventDataBound;

		// Token: 0x04000881 RID: 2177
		private static readonly object EventGroupsChanging;

		// Token: 0x04000882 RID: 2178
		private static readonly object EventItemUpdated;

		// Token: 0x04000883 RID: 2179
		private static readonly object EventItemInserted;

		// Token: 0x04000884 RID: 2180
		private static readonly object EventItemDeleted;

		// Token: 0x04000885 RID: 2181
		private static readonly object EventExcelMLExportStylesCreated;

		// Token: 0x04000886 RID: 2182
		private static readonly object EventExcelMLExportRowCreated;

		// Token: 0x04000887 RID: 2183
		private static readonly object EventExcelMLWorkBookCreated;

		// Token: 0x04000888 RID: 2184
		private static readonly object EventRowDrop;

		// Token: 0x04000889 RID: 2185
		private static readonly object EventExporting;

		// Token: 0x0400088A RID: 2186
		private static readonly object EventPdfExporting;

		// Token: 0x0400088B RID: 2187
		private static readonly object EventExcelExportCellFormatting;

		// Token: 0x0400088C RID: 2188
		private static readonly object EventExportCellFormatting;

		// Token: 0x0400088D RID: 2189
		private static readonly object EventHTMLExporting;

		// Token: 0x0400088E RID: 2190
		private static readonly object EventBatchEditCommand;

		// Token: 0x0400088F RID: 2191
		private static readonly object EventBiffExporting;

		// Token: 0x04000890 RID: 2192
		private static readonly object EventInfrastructureExporting;

		// Token: 0x04000891 RID: 2193
		private GridTableItemStyle _groupHeaderItemStyle;

		// Token: 0x04000892 RID: 2194
		private GridTableItemStyle _alternatingItemStyle;

		// Token: 0x04000893 RID: 2195
		private GridTableItemStyle _editItemStyle;

		// Token: 0x04000894 RID: 2196
		private GridTableItemStyle _footerStyle;

		// Token: 0x04000895 RID: 2197
		private GridTableItemStyle _headerStyle;

		// Token: 0x04000896 RID: 2198
		private GridTableItemStyle _itemStyle;

		// Token: 0x04000897 RID: 2199
		private GridTableItemStyle _filterItemStyle;

		// Token: 0x04000898 RID: 2200
		private GridTableItemStyle _commandItemStyle;

		// Token: 0x04000899 RID: 2201
		private GridTableItemStyle _activeItemStyle;

		// Token: 0x0400089A RID: 2202
		private GridTableItemStyle _multiHeaderItemStyle;

		// Token: 0x0400089B RID: 2203
		private GridPagerStyle _pagerStyle;

		// Token: 0x0400089C RID: 2204
		private GridTableItemStyle _selectedItemStyle;

		// Token: 0x0400089D RID: 2205
		private string _applicationPath = string.Empty;

		// Token: 0x0400089E RID: 2206
		private int _viewStateSize;

		// Token: 0x0400089F RID: 2207
		private static readonly object EventFieldDescriptorsReady;

		// Token: 0x040008A0 RID: 2208
		private GridStrings _localization;

		// Token: 0x040008A1 RID: 2209
		private string[] callbackArguments;

		// Token: 0x02000398 RID: 920
		internal class RadGridEmptyDataView : DataSourceView
		{
			// Token: 0x06002160 RID: 8544 RVA: 0x000704E9 File Offset: 0x0006E6E9
			internal RadGridEmptyDataView(IDataSource owner, string viewName) : base(owner, viewName)
			{
			}

			// Token: 0x06002161 RID: 8545 RVA: 0x000704F4 File Offset: 0x0006E6F4
			protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
			{
				return new object[]
				{
					string.Empty
				};
			}
		}

		// Token: 0x02000399 RID: 921
		internal class RadGridEmptyDataSource : IDataSource
		{
			// Token: 0x14000069 RID: 105
			// (add) Token: 0x06002162 RID: 8546 RVA: 0x00070511 File Offset: 0x0006E711
			// (remove) Token: 0x06002163 RID: 8547 RVA: 0x00070513 File Offset: 0x0006E713
			public event EventHandler DataSourceChanged
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x06002164 RID: 8548 RVA: 0x00070515 File Offset: 0x0006E715
			public DataSourceView GetView(string viewName)
			{
				return new RadGrid.RadGridEmptyDataView(this, viewName);
			}

			// Token: 0x06002165 RID: 8549 RVA: 0x00070520 File Offset: 0x0006E720
			public ICollection GetViewNames()
			{
				return new string[]
				{
					"default"
				};
			}
		}

		// Token: 0x0200039A RID: 922
		internal class ListBoxMenuTemplate : ITemplate
		{
			// Token: 0x06002167 RID: 8551 RVA: 0x00070545 File Offset: 0x0006E745
			private void SkinableControl_PreRender(object sender, EventArgs e)
			{
				((ISkinnableControl)sender).Skin = this.OwnerGrid.RuntimeSkin;
			}

			// Token: 0x06002168 RID: 8552 RVA: 0x0007055D File Offset: 0x0006E75D
			public ListBoxMenuTemplate(RadGrid grid, bool headerContext)
			{
				this.OwnerGrid = grid;
				this.HeaderContext = headerContext;
			}

			// Token: 0x06002169 RID: 8553 RVA: 0x00070574 File Offset: 0x0006E774
			public void InstantiateIn(Control container)
			{
				if (this.HeaderContext)
				{
					Label label = new Label();
					string cssClass = (this.OwnerGrid.RenderMode == RenderMode.Classic) ? "rmLeftImage rgHCMFilterIcon" : "rmLeftImage rmIcon rgHCMFilterIcon";
					label.CssClass = cssClass;
					label.Text = "";
					label.ToolTip = "";
					container.Controls.Add(label);
					RadTextBox radTextBox = new RadTextBox();
					radTextBox.RenderMode = this.OwnerGrid.RenderMode;
					radTextBox.ID = "filterCheckListSearch";
					radTextBox.EmptyMessage = "Search";
					radTextBox.EnableEmbeddedScripts = this.OwnerGrid.EnableEmbeddedScripts;
					radTextBox.EnableEmbeddedSkins = this.OwnerGrid.EnableEmbeddedSkins;
					radTextBox.EnableEmbeddedBaseStylesheet = this.OwnerGrid.EnableEmbeddedBaseStylesheet;
					radTextBox.Attributes.Add("onkeyup", string.Format("Telerik.Web.UI.Grid.FilterSearch($find('{0}'), this);", this.OwnerGrid.ClientID));
					radTextBox.PreRender += this.SkinableControl_PreRender;
					container.Controls.Add(radTextBox);
					container.Controls.Add(this.OwnerGrid.GetProxyRenderControl());
					return;
				}
				RadListBox radListBox = new RadListBox();
				radListBox.Height = Unit.Pixel(300);
				container.Controls.Add(radListBox);
				radListBox.ID = "filterCheckList";
				radListBox.CheckBoxes = true;
				radListBox.ShowCheckAll = true;
				Button child;
				Button child2;
				if (this.OwnerGrid.ResolvedRenderMode == RenderMode.Classic)
				{
					child = new Button
					{
						Text = "Apply",
						CssClass = "rgFilterApply",
						OnClientClick = "return false;"
					};
					child2 = new Button
					{
						Text = "Cancel",
						CssClass = "rgFilterCancel",
						CausesValidation = false,
						OnClientClick = "return false;"
					};
				}
				else
				{
					child = new ElasticButton
					{
						FirstSpanClass = string.Empty,
						SecondSpanClass = "t-text",
						Text = "Apply",
						CssClass = "t-button rgFilterApply",
						OnClientClick = "return false;"
					};
					child2 = new ElasticButton
					{
						FirstSpanClass = string.Empty,
						SecondSpanClass = "t-text",
						Text = "Cancel",
						CssClass = "t-button rgFilterCancel",
						CausesValidation = false,
						OnClientClick = "return false;"
					};
				}
				container.Controls.Add(child);
				container.Controls.Add(child2);
				this.OwnerGrid.FilterCheckList = radListBox;
				radListBox.ItemsRequested += this.OwnerGrid.listBox_ItemsRequested;
			}

			// Token: 0x040008AC RID: 2220
			private RadGrid OwnerGrid;

			// Token: 0x040008AD RID: 2221
			private bool HeaderContext;
		}

		// Token: 0x020003A2 RID: 930
		private class RadListBoxShared : RadListBox
		{
			// Token: 0x060022E8 RID: 8936 RVA: 0x00075111 File Offset: 0x00073311
			public new void Render(HtmlTextWriter writer)
			{
				base.Render(writer);
			}
		}

		// Token: 0x020003A3 RID: 931
		private class ProxyRenderControl : Control
		{
			// Token: 0x060022E9 RID: 8937 RVA: 0x0007511A File Offset: 0x0007331A
			public ProxyRenderControl(RadGrid owner)
			{
				this.ownerGrid = owner;
			}

			// Token: 0x060022EA RID: 8938 RVA: 0x00075129 File Offset: 0x00073329
			protected override void Render(HtmlTextWriter writer)
			{
				this.ownerGrid.RenderListBoxShared(writer);
			}

			// Token: 0x04000906 RID: 2310
			private RadGrid ownerGrid;
		}
	}
}
