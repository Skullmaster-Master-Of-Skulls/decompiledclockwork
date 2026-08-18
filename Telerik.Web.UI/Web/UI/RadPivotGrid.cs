using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Functions;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using Telerik.Web.UI.PivotGrid.Core.DataProviders;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Layouts;
using Telerik.Web.UI.PivotGrid.Core.Olap;
using Telerik.Web.UI.PivotGrid.Core.ViewModels;
using Telerik.Web.UI.PivotGrid.DataProviders.Adomd;
using Telerik.Web.UI.PivotGrid.Queryable;
using Telerik.Web.UI.PivotGrid.Xmla;

namespace Telerik.Web.UI
{
	// Token: 0x02000E16 RID: 3606
	[ToolboxData("<{0}:RadPivotGrid runat=server></{0}:RadPivotGrid>")]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadTreeList))]
	[TelerikToolboxCategory("Data")]
	[Description("Telerik RadPivotGrid")]
	[Designer("Telerik.Web.Design.RadPivotGridDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadPivotGrid), "Telerik.Web.UI.PivotGrid.png")]
	[DefaultProperty("")]
	[DefaultEvent("NeedDataSource")]
	[EmbeddedSkin("PivotGrid", typeof(RadPivotGrid))]
	[EmbeddedSkin("PivotGrid", "Default", typeof(RadPivotGrid))]
	[ClientScriptResource("Telerik.Web.UI.RadPivotGrid", "Telerik.Web.UI.PivotGrid.RadPivotGridScripts.js")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadPivotGrid))]
	[RequiredScript(typeof(MaterialRipple))]
	public class RadPivotGrid : RadCompositeDataBoundControl, IPostBackEventHandler, INamingContainer, ILocalizableControl
	{
		// Token: 0x06008724 RID: 34596 RVA: 0x001EAA50 File Offset: 0x001E8C50
		static RadPivotGrid()
		{
			RadPivotGrid.EventNeedDataSource = new object();
			RadPivotGrid.EventExporting = new object();
			RadPivotGrid.EventBiffExporting = new object();
			RadPivotGrid.EventCellExporting = new object();
			RadPivotGrid.EventInfrastructureExporting = new object();
			RadPivotGrid.EventItemCreated = new object();
			RadPivotGrid.EventItemDataBound = new object();
			RadPivotGrid.EventCellCreated = new object();
			RadPivotGrid.EventCellDataBound = new object();
			RadPivotGrid.EventPageSizeChanged = new object();
			RadPivotGrid.EventPageIndexChanged = new object();
			RadPivotGrid.EventItemCommand = new object();
			RadPivotGrid.EventSorting = new object();
			RadPivotGrid.EventAddingFieldToZone = new object();
			RadPivotGrid.EventFieldCreated = new object();
			RadPivotGrid.EventFieldReorder = new object();
			RadPivotGrid.EventShowHideField = new object();
			RadPivotGrid.EventInitFilterDialogue = new object();
			RadPivotGrid.EventFilterCommand = new object();
			RadPivotGrid.EventItemNeedCalculation = new object();
			RadPivotGrid.EventDataProviderError = new object();
			RadPivotGrid.EventDataProviderStatusChanged = new object();
			RadPivotGrid.EventGetDescriptionsDataCompleted = new object();
		}

		// Token: 0x17002AEB RID: 10987
		// (get) Token: 0x06008725 RID: 34597 RVA: 0x001EABCB File Offset: 0x001E8DCB
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002AEC RID: 10988
		// (get) Token: 0x06008726 RID: 34598 RVA: 0x001EABCE File Offset: 0x001E8DCE
		// (set) Token: 0x06008727 RID: 34599 RVA: 0x001EABD6 File Offset: 0x001E8DD6
		internal PivotGridExportFormat CurrentExportFormat
		{
			get
			{
				return this.currentExportFormat;
			}
			set
			{
				this.currentExportFormat = value;
			}
		}

		// Token: 0x17002AED RID: 10989
		// (get) Token: 0x06008728 RID: 34600 RVA: 0x001EABDF File Offset: 0x001E8DDF
		// (set) Token: 0x06008729 RID: 34601 RVA: 0x001EABE7 File Offset: 0x001E8DE7
		internal bool IsExporting
		{
			get
			{
				return this._isExporting;
			}
			set
			{
				this._isExporting = value;
			}
		}

		// Token: 0x17002AEE RID: 10990
		// (get) Token: 0x0600872A RID: 34602 RVA: 0x001EABF0 File Offset: 0x001E8DF0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal PivotGridFilteringManager FilteringManager
		{
			get
			{
				if (this.filteringManager == null)
				{
					this.filteringManager = new PivotGridFilteringManager(this);
				}
				return this.filteringManager;
			}
		}

		// Token: 0x17002AEF RID: 10991
		// (get) Token: 0x0600872B RID: 34603 RVA: 0x001EAC0C File Offset: 0x001E8E0C
		// (set) Token: 0x0600872C RID: 34604 RVA: 0x001EAC35 File Offset: 0x001E8E35
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal int RowLayoutLevelsCount
		{
			get
			{
				object obj = this.ControlState["_!RowLayoutLevelsCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ControlState["_!RowLayoutLevelsCount"] = value;
			}
		}

		// Token: 0x17002AF0 RID: 10992
		// (get) Token: 0x0600872D RID: 34605 RVA: 0x001EAC4D File Offset: 0x001E8E4D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x17002AF1 RID: 10993
		// (get) Token: 0x0600872E RID: 34606 RVA: 0x001EAC55 File Offset: 0x001E8E55
		internal PivotModelSessionPersister ModelPersister
		{
			get
			{
				if (this.modelPersister == null)
				{
					this.modelPersister = new PivotModelSessionPersister();
				}
				return this.modelPersister;
			}
		}

		// Token: 0x17002AF2 RID: 10994
		// (get) Token: 0x0600872F RID: 34607 RVA: 0x001EAC70 File Offset: 0x001E8E70
		internal BaseLayout RowLayout
		{
			get
			{
				return this.rowLayout;
			}
		}

		// Token: 0x17002AF3 RID: 10995
		// (get) Token: 0x06008730 RID: 34608 RVA: 0x001EAC78 File Offset: 0x001E8E78
		internal BaseLayout ColumnLayout
		{
			get
			{
				return this.columnLayout;
			}
		}

		// Token: 0x17002AF4 RID: 10996
		// (get) Token: 0x06008731 RID: 34609 RVA: 0x001EAC80 File Offset: 0x001E8E80
		// (set) Token: 0x06008732 RID: 34610 RVA: 0x001EAC88 File Offset: 0x001E8E88
		internal bool HideHorizontalScroll { get; set; }

		// Token: 0x17002AF5 RID: 10997
		// (get) Token: 0x06008733 RID: 34611 RVA: 0x001EAC94 File Offset: 0x001E8E94
		// (set) Token: 0x06008734 RID: 34612 RVA: 0x001EACDA File Offset: 0x001E8EDA
		internal bool ShouldAdjustColumnsLayout
		{
			get
			{
				return this.ClientSettings.Scrolling.AllowVerticalScroll && ((this.ColumnHeaderTableLayout == PivotGridTableLayout.Auto && this.columnHeaderCellStyle.Width.IsEmpty) || this.shouldAdjustColumnsLayout);
			}
			set
			{
				this.shouldAdjustColumnsLayout = value;
			}
		}

		// Token: 0x17002AF6 RID: 10998
		// (get) Token: 0x06008735 RID: 34613 RVA: 0x001EACE3 File Offset: 0x001E8EE3
		internal PivotGridControlStateManager ControlState
		{
			get
			{
				if (this.controlStateManager == null)
				{
					this.controlStateManager = new PivotGridControlStateManager();
				}
				return this.controlStateManager;
			}
		}

		// Token: 0x17002AF7 RID: 10999
		// (get) Token: 0x06008736 RID: 34614 RVA: 0x001EACFE File Offset: 0x001E8EFE
		internal bool UsesControlState
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x17002AF8 RID: 11000
		// (get) Token: 0x06008737 RID: 34615 RVA: 0x001EAD0C File Offset: 0x001E8F0C
		// (set) Token: 0x06008738 RID: 34616 RVA: 0x001EAD35 File Offset: 0x001E8F35
		internal int ColumnGroupsCount
		{
			get
			{
				object obj = this.ControlState["ColumnGroupsCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ControlState["ColumnGroupsCount"] = value;
			}
		}

		// Token: 0x17002AF9 RID: 11001
		// (get) Token: 0x06008739 RID: 34617 RVA: 0x001EAD50 File Offset: 0x001E8F50
		// (set) Token: 0x0600873A RID: 34618 RVA: 0x001EAD79 File Offset: 0x001E8F79
		internal int TotalItemCount
		{
			get
			{
				object obj = this.ControlState["_!TotalItemCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ControlState["_!TotalItemCount"] = value;
			}
		}

		// Token: 0x17002AFA RID: 11002
		// (get) Token: 0x0600873B RID: 34619 RVA: 0x001EAD91 File Offset: 0x001E8F91
		// (set) Token: 0x0600873C RID: 34620 RVA: 0x001EADA8 File Offset: 0x001E8FA8
		internal int? CustomPageSize
		{
			get
			{
				return (int?)this.ControlState["_cps"];
			}
			set
			{
				this.ControlState["_cps"] = value;
			}
		}

		// Token: 0x17002AFB RID: 11003
		// (get) Token: 0x0600873D RID: 34621 RVA: 0x001EADC0 File Offset: 0x001E8FC0
		// (set) Token: 0x0600873E RID: 34622 RVA: 0x001EADC8 File Offset: 0x001E8FC8
		protected bool IsNeedDataSourceInProgress { get; set; }

		// Token: 0x17002AFC RID: 11004
		// (get) Token: 0x0600873F RID: 34623 RVA: 0x001EADD1 File Offset: 0x001E8FD1
		// (set) Token: 0x06008740 RID: 34624 RVA: 0x001EADD9 File Offset: 0x001E8FD9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsFilterCommandInProgress { get; set; }

		// Token: 0x17002AFD RID: 11005
		// (get) Token: 0x06008741 RID: 34625 RVA: 0x001EADE2 File Offset: 0x001E8FE2
		// (set) Token: 0x06008742 RID: 34626 RVA: 0x001EADEA File Offset: 0x001E8FEA
		internal bool IsDataBinding { get; set; }

		// Token: 0x17002AFE RID: 11006
		// (get) Token: 0x06008743 RID: 34627 RVA: 0x001EADF4 File Offset: 0x001E8FF4
		internal bool IsBoundToIQueryableCollection
		{
			get
			{
				if (this.IsDesignMode)
				{
					return false;
				}
				bool result = !base.IsBoundUsingDataSourceID && this.DataSource is IQueryable && this.UseQueryableDataProvider;
				if (!this.IsDataBinding)
				{
					result = (!base.IsBoundUsingDataSourceID && this.UseQueryableDataProvider);
				}
				else if (base.IsBoundUsingDataSourceID || !(this.DataSource is IQueryable))
				{
					this.UseQueryableDataProvider = false;
				}
				return result;
			}
		}

		// Token: 0x17002AFF RID: 11007
		// (get) Token: 0x06008744 RID: 34628 RVA: 0x001EAE63 File Offset: 0x001E9063
		internal bool IsBoundToAdomd
		{
			get
			{
				return this.OlapSettings.ProviderType == PivotGridOlapProviderType.Adomd;
			}
		}

		// Token: 0x17002B00 RID: 11008
		// (get) Token: 0x06008745 RID: 34629 RVA: 0x001EAE73 File Offset: 0x001E9073
		internal bool IsBoundToXmla
		{
			get
			{
				return this.OlapSettings.ProviderType == PivotGridOlapProviderType.Xmla;
			}
		}

		// Token: 0x17002B01 RID: 11009
		// (get) Token: 0x06008746 RID: 34630 RVA: 0x001EAE83 File Offset: 0x001E9083
		internal bool IsBoundToOlap
		{
			get
			{
				return this.OlapSettings.ProviderType != PivotGridOlapProviderType.None;
			}
		}

		// Token: 0x17002B02 RID: 11010
		// (get) Token: 0x06008747 RID: 34631 RVA: 0x001EAE96 File Offset: 0x001E9096
		// (set) Token: 0x06008748 RID: 34632 RVA: 0x001EAED4 File Offset: 0x001E90D4
		internal PivotGridRowHeadersModel RowHeaderModel
		{
			get
			{
				if (this.ControlState["RowHeaderModel"] == null)
				{
					this.ControlState["RowHeaderModel"] = new PivotGridRowHeadersModel();
				}
				return (PivotGridRowHeadersModel)this.ControlState["RowHeaderModel"];
			}
			set
			{
				this.ControlState["RowHeaderModel"] = value;
			}
		}

		// Token: 0x17002B03 RID: 11011
		// (get) Token: 0x06008749 RID: 34633 RVA: 0x001EAEE7 File Offset: 0x001E90E7
		// (set) Token: 0x0600874A RID: 34634 RVA: 0x001EAF25 File Offset: 0x001E9125
		internal PivotGridRowHeadersModel ColumnHeadersModel
		{
			get
			{
				if (this.ControlState["ColumnHeadersModel"] == null)
				{
					this.ControlState["ColumnHeadersModel"] = new PivotGridRowHeadersModel();
				}
				return (PivotGridRowHeadersModel)this.ControlState["ColumnHeadersModel"];
			}
			set
			{
				this.ControlState["ColumnHeadersModel"] = value;
			}
		}

		// Token: 0x17002B04 RID: 11012
		// (get) Token: 0x0600874B RID: 34635 RVA: 0x001EAF38 File Offset: 0x001E9138
		// (set) Token: 0x0600874C RID: 34636 RVA: 0x001EAF65 File Offset: 0x001E9165
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		internal virtual Dictionary<string, Unit> ResizedColumnsWidth
		{
			get
			{
				object obj = this.ViewState["ResizedColumnsWidth"];
				if (obj != null)
				{
					return (Dictionary<string, Unit>)obj;
				}
				return new Dictionary<string, Unit>();
			}
			set
			{
				this.ViewState["ResizedColumnsWidth"] = value;
			}
		}

		// Token: 0x17002B05 RID: 11013
		// (get) Token: 0x0600874D RID: 34637 RVA: 0x001EAF78 File Offset: 0x001E9178
		// (set) Token: 0x0600874E RID: 34638 RVA: 0x001EAFE0 File Offset: 0x001E91E0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		internal string FiltersPersistence
		{
			get
			{
				List<string> list = new List<string>();
				foreach (PivotGridFilter filter in this.Filters)
				{
					list.Add(PivotGridFilterPersistenceHelper.SerializePivotFilter(filter));
				}
				return string.Join(",", list.ToArray());
			}
			set
			{
				string[] array = value.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				this.Filters.Clear();
				foreach (string serializedFilter in array)
				{
					PivotGridFilter pivotGridFilter = PivotGridFilterPersistenceHelper.DeserializePivotFilter(serializedFilter);
					if (pivotGridFilter != null)
					{
						this.Filters.Add(pivotGridFilter);
					}
				}
				this.shouldAddNewSettings = true;
			}
		}

		// Token: 0x17002B06 RID: 11014
		// (get) Token: 0x0600874F RID: 34639 RVA: 0x001EB048 File Offset: 0x001E9248
		// (set) Token: 0x06008750 RID: 34640 RVA: 0x001EB076 File Offset: 0x001E9276
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public PivotGridFiltersCollection Filters
		{
			get
			{
				if (this.filters == null)
				{
					this.filters = new PivotGridFiltersCollection();
					if (base.IsTrackingViewState)
					{
						this.filters.TrackViewState();
					}
				}
				return this.filters;
			}
			internal set
			{
				this.filters = value;
				if (base.IsTrackingViewState)
				{
					this.filters.TrackViewState();
				}
			}
		}

		// Token: 0x17002B07 RID: 11015
		// (get) Token: 0x06008751 RID: 34641 RVA: 0x001EB092 File Offset: 0x001E9292
		// (set) Token: 0x06008752 RID: 34642 RVA: 0x001EB0D0 File Offset: 0x001E92D0
		internal PivotGridDataModel DataModel
		{
			get
			{
				if (this.ControlState["DataModel"] == null)
				{
					this.ControlState["DataModel"] = new PivotGridDataModel();
				}
				return (PivotGridDataModel)this.ControlState["DataModel"];
			}
			set
			{
				this.ControlState["DataModel"] = value;
			}
		}

		// Token: 0x17002B08 RID: 11016
		// (get) Token: 0x06008753 RID: 34643 RVA: 0x001EB0E3 File Offset: 0x001E92E3
		internal PivotGridOuterTable OuterTable
		{
			get
			{
				if (this.outerTable == null)
				{
					this.outerTable = new PivotGridOuterTable(this);
				}
				return this.outerTable;
			}
		}

		// Token: 0x17002B09 RID: 11017
		// (get) Token: 0x06008754 RID: 34644 RVA: 0x001EB0FF File Offset: 0x001E92FF
		internal PivotGridRowHeaderTable RowHeaderTable
		{
			get
			{
				if (this.rowHeaderTable == null)
				{
					this.rowHeaderTable = new PivotGridRowHeaderTable(this);
				}
				return this.rowHeaderTable;
			}
		}

		// Token: 0x17002B0A RID: 11018
		// (get) Token: 0x06008755 RID: 34645 RVA: 0x001EB11B File Offset: 0x001E931B
		internal PivotGridColumnHeaderTable ColumnHeaderTable
		{
			get
			{
				if (this.columnHeaderTable == null)
				{
					this.columnHeaderTable = new PivotGridColumnHeaderTable(this);
				}
				return this.columnHeaderTable;
			}
		}

		// Token: 0x17002B0B RID: 11019
		// (get) Token: 0x06008756 RID: 34646 RVA: 0x001EB137 File Offset: 0x001E9337
		internal PivotGridDataTable DataTable
		{
			get
			{
				if (this.dataTable == null)
				{
					this.dataTable = new PivotGridDataTable(this);
				}
				return this.dataTable;
			}
		}

		// Token: 0x17002B0C RID: 11020
		// (get) Token: 0x06008757 RID: 34647 RVA: 0x001EB153 File Offset: 0x001E9353
		internal bool ShouldBeBound
		{
			get
			{
				return this.ControlState["_!DSIC"] == null;
			}
		}

		// Token: 0x17002B0D RID: 11021
		// (get) Token: 0x06008758 RID: 34648 RVA: 0x001EB168 File Offset: 0x001E9368
		internal bool AlwaysAutoBindOnPostBack
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x17002B0E RID: 11022
		// (get) Token: 0x06008759 RID: 34649 RVA: 0x001EB173 File Offset: 0x001E9373
		internal PivotViewModel PivotModel
		{
			get
			{
				if (this.pivotModel == null)
				{
					if (this.EnableCachingInternal)
					{
						this.pivotModel = this.ModelPersister.GetPivotModel(this.UniqueID);
					}
					else
					{
						this.pivotModel = new PivotViewModel();
					}
				}
				return this.pivotModel;
			}
		}

		// Token: 0x17002B0F RID: 11023
		// (get) Token: 0x0600875A RID: 34650 RVA: 0x001EB1AF File Offset: 0x001E93AF
		internal HashSet<string> PromissedFieldsForCreation
		{
			get
			{
				if (this.promissedFieldsForCreation == null)
				{
					this.promissedFieldsForCreation = new HashSet<string>();
				}
				return this.promissedFieldsForCreation;
			}
		}

		// Token: 0x0600875B RID: 34651 RVA: 0x001EB1CC File Offset: 0x001E93CC
		public void Sort(string fieldUniqueName, PivotGridSortOrder sortOrder, bool suppressRebind = false)
		{
			if (!this.IsFieldCouldBeSorted(fieldUniqueName))
			{
				throw new PivotGridException("Only PivotGridColumnField and PivotGridRowField could be sorted");
			}
			this.Sort(new PivotGridSortExpression
			{
				FieldName = fieldUniqueName,
				SortOrder = sortOrder
			}, suppressRebind);
		}

		// Token: 0x0600875C RID: 34652 RVA: 0x001EB209 File Offset: 0x001E9409
		public void Sort(PivotGridField field, bool suppressRebind = false)
		{
			if (!this.IsFieldCouldBeSorted(field.UniqueName))
			{
				throw new PivotGridException("Only PivotGridColumnField and PivotGridRowField could be sorted");
			}
			this.Sort(field.UniqueName, suppressRebind);
		}

		// Token: 0x0600875D RID: 34653 RVA: 0x001EB231 File Offset: 0x001E9431
		public void Sort(PivotGridField field, PivotGridSortOrder sortOrder, bool suppressRebind = false)
		{
			if (!this.IsFieldCouldBeSorted(field.UniqueName))
			{
				throw new PivotGridException("Only PivotGridColumnField and PivotGridRowField could be sorted");
			}
			this.Sort(field.UniqueName, sortOrder, suppressRebind);
		}

		// Token: 0x0600875E RID: 34654 RVA: 0x001EB25A File Offset: 0x001E945A
		public void Sort(string expression, bool suppressRebind = false)
		{
			this.SortExpressions.ChangeSortOrder(expression, this.AllowNaturalSort);
			if (!suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x0600875F RID: 34655 RVA: 0x001EB277 File Offset: 0x001E9477
		public void Sort(PivotGridSortExpression expression, bool suppressRebind = false)
		{
			if (!this.IsFieldCouldBeSorted(expression.FieldName))
			{
				throw new PivotGridException("Only PivotGridColumnField and PivotGridRowField could be sorted");
			}
			this.Sort(expression.ToString(), suppressRebind);
		}

		// Token: 0x06008760 RID: 34656 RVA: 0x001EB2B4 File Offset: 0x001E94B4
		public void ExpandAllColumnGroups(bool suppressRebind = false)
		{
			if (this.ColumnGroupsDefaultExpanded)
			{
				this.expandCollapseColumnLevels.Clear();
			}
			else
			{
				int num = this.Fields.Count((PivotGridField f) => f is PivotGridColumnField && !f.IsHidden);
				for (int i = 0; i < num; i++)
				{
					this.expandCollapseColumnLevels.Add(i);
				}
			}
			this.CollapsedColumnIndexes.Clear();
			if (!suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x06008761 RID: 34657 RVA: 0x001EB348 File Offset: 0x001E9548
		public void ExpandAllColumnGroups(int level, bool suppressRebind = false)
		{
			if (this.ColumnGroupsDefaultExpanded)
			{
				this.expandCollapseColumnLevels.Remove(level);
			}
			else
			{
				this.expandCollapseColumnLevels.Add(level);
			}
			this.CollapsedColumnIndexes.RemoveWhere((Array indexes) => indexes.Length == level + 1);
			if (!suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x06008762 RID: 34658 RVA: 0x001EB3C8 File Offset: 0x001E95C8
		public void ExpandAllRowGroups(bool suppressRebind = false)
		{
			if (this.RowGroupsDefaultExpanded)
			{
				this.expandCollapseRowLevels.Clear();
			}
			else
			{
				int num = this.Fields.Count((PivotGridField f) => f is PivotGridRowField && !f.IsHidden);
				for (int i = 0; i < num; i++)
				{
					this.expandCollapseRowLevels.Add(i);
				}
			}
			this.CollapsedRowIndexes.Clear();
			if (!suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x06008763 RID: 34659 RVA: 0x001EB45C File Offset: 0x001E965C
		public void ExpandAllRowGroups(int level, bool suppressRebind = false)
		{
			if (this.RowGroupsDefaultExpanded)
			{
				this.expandCollapseRowLevels.Remove(level);
			}
			else
			{
				this.expandCollapseRowLevels.Add(level);
			}
			this.CollapsedRowIndexes.RemoveWhere((Array indexes) => indexes.Length == level + 1);
			if (!suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x06008764 RID: 34660 RVA: 0x001EB4C8 File Offset: 0x001E96C8
		public void ExpandAllFieldGroups(PivotGridRowField field, bool suppressRebind = false)
		{
			int fieldIndex = this.GetFieldIndex(field);
			this.ExpandAllRowGroups(fieldIndex, suppressRebind);
		}

		// Token: 0x06008765 RID: 34661 RVA: 0x001EB4E8 File Offset: 0x001E96E8
		public void ExpandAllFieldGroups(PivotGridColumnField field, bool suppressRebind = false)
		{
			int fieldIndex = this.GetFieldIndex(field);
			this.ExpandAllColumnGroups(fieldIndex, suppressRebind);
		}

		// Token: 0x06008766 RID: 34662 RVA: 0x001EB51C File Offset: 0x001E971C
		public void CollapseAllColumnGroups(bool suppressRebind = false)
		{
			if (this.ColumnGroupsDefaultExpanded)
			{
				int num = this.Fields.Count((PivotGridField f) => f is PivotGridColumnField && !f.IsHidden);
				for (int i = 0; i < num; i++)
				{
					this.expandCollapseColumnLevels.Add(i);
				}
			}
			else
			{
				this.expandCollapseColumnLevels.Clear();
			}
			this.CollapsedColumnIndexes.Clear();
			if (!suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x06008767 RID: 34663 RVA: 0x001EB5B0 File Offset: 0x001E97B0
		public void CollapseAllColumnGroups(int level, bool suppressRebind = false)
		{
			if (this.ColumnGroupsDefaultExpanded)
			{
				this.expandCollapseColumnLevels.Add(level);
			}
			else
			{
				this.expandCollapseColumnLevels.Remove(level);
			}
			this.CollapsedColumnIndexes.RemoveWhere((Array indexes) => indexes.Length == level + 1);
			if (!suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x06008768 RID: 34664 RVA: 0x001EB630 File Offset: 0x001E9830
		public void CollapseAllRowGroups(bool suppressRebind = false)
		{
			if (this.RowGroupsDefaultExpanded)
			{
				int num = this.Fields.Count((PivotGridField f) => f is PivotGridRowField && !f.IsHidden);
				for (int i = 0; i < num; i++)
				{
					this.expandCollapseRowLevels.Add(i);
				}
			}
			else
			{
				this.expandCollapseRowLevels.Clear();
			}
			this.CollapsedRowIndexes.Clear();
			if (!suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x06008769 RID: 34665 RVA: 0x001EB6C4 File Offset: 0x001E98C4
		public void CollapseAllRowGroups(int level, bool suppressRebind = false)
		{
			if (this.RowGroupsDefaultExpanded)
			{
				this.expandCollapseRowLevels.Add(level);
			}
			else
			{
				this.expandCollapseRowLevels.Remove(level);
			}
			this.CollapsedRowIndexes.RemoveWhere((Array indexes) => indexes.Length == level + 1);
			if (!suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x0600876A RID: 34666 RVA: 0x001EB730 File Offset: 0x001E9930
		public void CollapseAllFieldGroups(PivotGridRowField field, bool suppressRebind = false)
		{
			int fieldIndex = this.GetFieldIndex(field);
			this.CollapseAllRowGroups(fieldIndex, suppressRebind);
		}

		// Token: 0x0600876B RID: 34667 RVA: 0x001EB750 File Offset: 0x001E9950
		public void CollapseAllFieldGroups(PivotGridColumnField field, bool suppressRebind = false)
		{
			int fieldIndex = this.GetFieldIndex(field);
			this.CollapseAllColumnGroups(fieldIndex, suppressRebind);
		}

		// Token: 0x0600876C RID: 34668 RVA: 0x001EB76D File Offset: 0x001E996D
		public void ClearAllFilters()
		{
			this.Filters.Clear();
			this.IsFilterCommandInProgress = true;
			this.ResetPivotModel();
			this.ObtainDataSource(PivotGridRebindReason.ExplicitRebind);
			this.DataBind();
		}

		// Token: 0x0600876D RID: 34669 RVA: 0x001EB794 File Offset: 0x001E9994
		public void ClearFilter(string fieldUniqueName)
		{
			this.ClearFilter(this.Fields[fieldUniqueName]);
		}

		// Token: 0x0600876E RID: 34670 RVA: 0x001EB7C8 File Offset: 0x001E99C8
		public void ClearFilter(PivotGridField field)
		{
			if (field is PivotGridAggregateField)
			{
				throw new PivotGridException("PivotGridAggregateField could not be filtered");
			}
			this.Filters.RemoveAll((PivotGridFilter f) => f.FieldName == field.UniqueName);
			this.IsFilterCommandInProgress = true;
			this.ResetPivotModel();
			this.ObtainDataSource(PivotGridRebindReason.ExplicitRebind);
			this.DataBind();
		}

		// Token: 0x0600876F RID: 34671 RVA: 0x001EB82C File Offset: 0x001E9A2C
		public void FilterByLabel(PivotGridFilterFunction filterFunction, PivotGridField field, string filterValue, bool suppressRebind = false)
		{
			this.FilterByLabel(filterFunction, field, filterValue, "", suppressRebind, false);
		}

		// Token: 0x06008770 RID: 34672 RVA: 0x001EB860 File Offset: 0x001E9A60
		public void FilterByLabel(PivotGridFilterFunction filterFunction, PivotGridField field, string filterValue, string betweenFilterValue, bool suppressRebind = false, bool ignoreCase = false)
		{
			this.Filters.RemoveAll((PivotGridFilter f) => f.FieldName == field.UniqueName);
			PivotGridSingleGroupFilter pivotGridSingleGroupFilter;
			if (this.IsBoundToOlap)
			{
				pivotGridSingleGroupFilter = new PivotGridOlapLabelGroupFilter();
			}
			else
			{
				pivotGridSingleGroupFilter = new PivotGridLabelGroupFilter();
			}
			pivotGridSingleGroupFilter.FieldName = field.UniqueName;
			this.SetFilterCondition(pivotGridSingleGroupFilter, filterFunction, filterValue, betweenFilterValue, ignoreCase);
			this.Filters.Add(pivotGridSingleGroupFilter);
			if (!suppressRebind)
			{
				this.IsFilterCommandInProgress = true;
				this.Rebind();
			}
		}

		// Token: 0x06008771 RID: 34673 RVA: 0x001EB8E3 File Offset: 0x001E9AE3
		public void FilterByValue(PivotGridFilterFunction filterFunction, PivotGridField field, PivotGridAggregateField aggregateField, string filterValue, bool suppressRebind = false)
		{
			this.FilterByValue(filterFunction, field, aggregateField, filterValue, "", suppressRebind, false);
		}

		// Token: 0x06008772 RID: 34674 RVA: 0x001EB918 File Offset: 0x001E9B18
		public void FilterByValue(PivotGridFilterFunction filterFunction, PivotGridField field, PivotGridAggregateField aggregateField, string filterValue, string betweenFilterValue, bool suppressRebind = false, bool ignoreCase = false)
		{
			this.Filters.RemoveAll((PivotGridFilter f) => f.FieldName == field.UniqueName);
			PivotGridSingleGroupFilter pivotGridSingleGroupFilter;
			if (this.IsBoundToOlap)
			{
				pivotGridSingleGroupFilter = new PivotGridOlapValueGroupFilter
				{
					AggregateIndex = aggregateField.AggregateIndex
				};
			}
			else
			{
				pivotGridSingleGroupFilter = new PivotGridValueGroupFilter
				{
					AggregateIndex = aggregateField.AggregateIndex
				};
			}
			pivotGridSingleGroupFilter.FieldName = field.UniqueName;
			this.SetFilterCondition(pivotGridSingleGroupFilter, filterFunction, filterValue, betweenFilterValue, ignoreCase);
			this.Filters.Add(pivotGridSingleGroupFilter);
			if (!suppressRebind)
			{
				this.IsFilterCommandInProgress = true;
				this.Rebind();
			}
		}

		// Token: 0x06008773 RID: 34675 RVA: 0x001EB9B8 File Offset: 0x001E9BB8
		public void FilterTop(PivotGridField field, PivotGridAggregateField aggregateField, PivotGridAggregateType aggregateType, double value, bool suppressRebind = false)
		{
			this.FilterByTopOrBottom(PivotGridFilterFunction.Top, field, aggregateField, aggregateType, value, suppressRebind);
		}

		// Token: 0x06008774 RID: 34676 RVA: 0x001EB9C9 File Offset: 0x001E9BC9
		public void FilterBottom(PivotGridField field, PivotGridAggregateField aggregateField, PivotGridAggregateType aggregateType, double value, bool suppressRebind = false)
		{
			this.FilterByTopOrBottom(PivotGridFilterFunction.Bottom, field, aggregateField, aggregateType, value, suppressRebind);
		}

		// Token: 0x06008775 RID: 34677 RVA: 0x001EB9DA File Offset: 0x001E9BDA
		public void SetFilterIncludes(string fieldUniqueName, IEnumerable<object> values, bool suppressRebind = false)
		{
			this.SetFilterIncludesOrExcludes(fieldUniqueName, values, SetComparison.Includes, suppressRebind);
		}

		// Token: 0x06008776 RID: 34678 RVA: 0x001EB9E6 File Offset: 0x001E9BE6
		public void SetFilterExcludes(string fieldUniqueName, IEnumerable<object> values, bool suppressRebind = false)
		{
			this.SetFilterIncludesOrExcludes(fieldUniqueName, values, SetComparison.DoesNotInclude, suppressRebind);
		}

		// Token: 0x06008777 RID: 34679 RVA: 0x001EB9F4 File Offset: 0x001E9BF4
		public PivotGridItem[] GetItems(params PivotGridItemType[] types)
		{
			List<PivotGridItem> list = new List<PivotGridItem>();
			foreach (PivotGridItem pivotGridItem in this.Items)
			{
				foreach (PivotGridItemType pivotGridItemType in types)
				{
					if (pivotGridItem.ItemType == pivotGridItemType)
					{
						list.Add(pivotGridItem);
						break;
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06008778 RID: 34680 RVA: 0x001EBB30 File Offset: 0x001E9D30
		public bool TryReorderField(PivotGridField field, PivotGridFieldZoneType zoneType, int zoneIndex)
		{
			if (field.ZoneType == zoneType && field.ZoneIndex == zoneIndex && !field.IsHidden)
			{
				return false;
			}
			if (field.ZoneType == zoneType)
			{
				IEnumerable<PivotGridField> source = from f in this.Fields
				where f.ZoneType == zoneType
				orderby f.ZoneIndex
				select f;
				if (field.ZoneIndex > zoneIndex)
				{
					source = from f in source
					where f.ZoneIndex >= zoneIndex && field.ZoneIndex >= f.ZoneIndex
					select f;
				}
				else
				{
					source = (from f in source
					where f.ZoneIndex <= zoneIndex && field.ZoneIndex <= f.ZoneIndex
					select f).Reverse<PivotGridField>();
				}
				List<PivotGridField> list = source.ToList<PivotGridField>();
				for (int i = 0; i < list.Count; i++)
				{
					PivotGridField pivotGridField = list[i];
					if (i + 1 < list.Count)
					{
						pivotGridField.ZoneIndex = list[i + 1].ZoneIndex;
					}
				}
			}
			int j = 0;
			while (j < this.Fields.Count)
			{
				if (this.Fields[j].UniqueName == field.UniqueName)
				{
					if (field.ZoneType == zoneType)
					{
						field.ZoneIndex = zoneIndex;
						field.IsHidden = false;
						break;
					}
					this.Fields.RemoveAt(j);
					PivotGridField pivotGridField2 = this.Fields.FirstOrDefault((PivotGridField f) => f.ZoneType == zoneType && f.ZoneIndex == zoneIndex);
					if (pivotGridField2 != null)
					{
						IEnumerable<PivotGridField> enumerable = from f in this.Fields
						where f.ZoneType == zoneType && f.ZoneIndex >= zoneIndex
						select f;
						foreach (PivotGridField pivotGridField3 in enumerable)
						{
							pivotGridField3.ZoneIndex++;
						}
					}
					PivotGridField pivotGridField4 = this.CreateFieldByZoneType(zoneType);
					this.Fields.Insert(j, pivotGridField4);
					if (pivotGridField4 == null)
					{
						return false;
					}
					pivotGridField4.CopyBaseProperties(field);
					pivotGridField4.ZoneIndex = zoneIndex;
					pivotGridField4.IsHidden = false;
					break;
				}
				else
				{
					j++;
				}
			}
			this.CurrentPageIndex = 0;
			return true;
		}

		// Token: 0x06008779 RID: 34681 RVA: 0x001EBE08 File Offset: 0x001EA008
		public bool TryReorderField(string fieldUniqueName, PivotGridFieldZoneType zoneType, int zoneIndex)
		{
			string text = fieldUniqueName;
			fieldUniqueName = Regex.Replace(text, "\\s", string.Empty);
			PivotGridField pivotGridField = this.Fields.GetFieldByUniqueName(fieldUniqueName);
			if (pivotGridField == null)
			{
				pivotGridField = this.Fields.AddField(text, fieldUniqueName);
			}
			return this.TryReorderField(pivotGridField, zoneType, zoneIndex);
		}

		// Token: 0x0600877A RID: 34682 RVA: 0x001EBE50 File Offset: 0x001EA050
		public void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.IndexOf("FireCommand:") != -1)
			{
				this.HandleFireCommand(RadPivotGrid.parseFireCommandEventName(eventArgument), RadPivotGrid.parseFireCommandArgs(eventArgument));
			}
		}

		// Token: 0x0600877B RID: 34683 RVA: 0x001EBE94 File Offset: 0x001EA094
		private void HandleFireCommand(string commandName, string commandArgument)
		{
			PivotGridItem pivotGridItem = null;
			if (this.Items.Count > 0)
			{
				pivotGridItem = this.Items[0];
			}
			Dictionary<string, Unit> resizedColumnsWidth = new Dictionary<string, Unit>();
			switch (commandName)
			{
			case "RebindPivotGrid":
				this.Rebind();
				resizedColumnsWidth = this.ResizedColumnsWidth;
				goto IL_303;
			case "ShowHideField":
				if (pivotGridItem != null)
				{
					pivotGridItem.FireCommandEvent("ShowHideField", commandArgument);
					goto IL_303;
				}
				goto IL_303;
			case "Sort":
				if (pivotGridItem != null)
				{
					pivotGridItem.FireCommandEvent("Sort", commandArgument);
					resizedColumnsWidth = this.ResizedColumnsWidth;
					goto IL_303;
				}
				goto IL_303;
			case "FieldReorder":
				if (pivotGridItem != null)
				{
					pivotGridItem.FireCommandEvent("FieldReorder", commandArgument);
					goto IL_303;
				}
				goto IL_303;
			case "UpdateLayout":
				if (pivotGridItem != null)
				{
					pivotGridItem.FireCommandEvent("UpdateLayout", commandArgument);
					goto IL_303;
				}
				goto IL_303;
			case "AggregateChange":
				if (pivotGridItem != null)
				{
					pivotGridItem.FireCommandEvent("AggregateChange", commandArgument);
					goto IL_303;
				}
				goto IL_303;
			case "InitFilterDialogue":
				if (pivotGridItem != null)
				{
					pivotGridItem.FireCommandEvent("InitFilterDialogue", commandArgument);
					resizedColumnsWidth = this.ResizedColumnsWidth;
					goto IL_303;
				}
				goto IL_303;
			case "Filter":
				if (pivotGridItem != null)
				{
					int num2 = int.Parse(RadPivotGrid.parseFilterCommandArgs(commandArgument, 0));
					string y = RadPivotGrid.parseFilterCommandArgs(commandArgument, 2);
					Pair commandArgument2 = new Pair(num2, y);
					pivotGridItem.FireCommandEvent("Filter", commandArgument2);
					goto IL_303;
				}
				goto IL_303;
			case "AggregateFunctionChanged":
				if (pivotGridItem != null)
				{
					pivotGridItem.FireCommandEvent("AggregateFunctionChanged", commandArgument);
					goto IL_303;
				}
				goto IL_303;
			case "Page":
				if (pivotGridItem != null)
				{
					PivotGridPagerItem pivotGridPagerItem = this.Items.FirstOrDefault((PivotGridItem i) => i.ItemType == PivotGridItemType.PagerItem) as PivotGridPagerItem;
					if (pivotGridPagerItem == null)
					{
						pivotGridItem.FireCommandEvent("Page", commandArgument);
					}
					else
					{
						pivotGridPagerItem.FireCommandEvent("Page", commandArgument);
					}
					resizedColumnsWidth = this.ResizedColumnsWidth;
					goto IL_303;
				}
				goto IL_303;
			case "PageSizeChanged":
				if (pivotGridItem != null)
				{
					PivotGridPagerItem pivotGridPagerItem2 = this.Items.FirstOrDefault((PivotGridItem i) => i.ItemType == PivotGridItemType.PagerItem) as PivotGridPagerItem;
					if (pivotGridPagerItem2 == null)
					{
						pivotGridItem.FireCommandEvent("PageSizeChanged", commandArgument);
					}
					else
					{
						pivotGridPagerItem2.FireCommandEvent("PageSizeChanged", commandArgument);
					}
					resizedColumnsWidth = this.ResizedColumnsWidth;
					goto IL_303;
				}
				goto IL_303;
			case "ExpandCollapseLevel":
				if (pivotGridItem != null)
				{
					pivotGridItem.FireCommandEvent("ExpandCollapseLevel", commandArgument);
					goto IL_303;
				}
				goto IL_303;
			}
			if (pivotGridItem != null)
			{
				pivotGridItem.FireCommandEvent(commandName, commandArgument);
			}
			IL_303:
			this.ResizedColumnsWidth = resizedColumnsWidth;
		}

		// Token: 0x0600877C RID: 34684 RVA: 0x001EC1AB File Offset: 0x001EA3AB
		internal void ObtainDataSource(PivotGridRebindReason rebindReason)
		{
			this.ObtainDataSource(rebindReason, base.IsBoundUsingDataSourceID);
		}

		// Token: 0x0600877D RID: 34685 RVA: 0x001EC1BA File Offset: 0x001EA3BA
		internal void ObtainDataSource(PivotGridRebindReason rebindReason, bool isBoundUsingDataSourceId)
		{
			if (!this.DataSourceIsAssigned && !isBoundUsingDataSourceId)
			{
				this.OnNeedDataSource(new PivotGridNeedDataSourceEventArgs(rebindReason));
			}
		}

		// Token: 0x0600877E RID: 34686 RVA: 0x001EC1D3 File Offset: 0x001EA3D3
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.DescribeProperties(descriptor);
		}

		// Token: 0x0600877F RID: 34687 RVA: 0x001EC208 File Offset: 0x001EA408
		private void DescribeProperties(IScriptDescriptor descriptor)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new PivotGridJavaScriptConverter()
			});
			descriptor.AddProperty("ClientID", this.ClientID);
			descriptor.AddProperty("UniqueID", this.UniqueID);
			descriptor.AddProperty("Skin", base.RuntimeSkin);
			if (this.AllowPaging)
			{
				descriptor.AddProperty("_pageCount", this.PageCount);
				descriptor.AddProperty("_pageSize", this.PageSize);
				descriptor.AddProperty("_currentPageIndex", this.CurrentPageIndex);
			}
			if (!string.IsNullOrEmpty(this.EmptyValue))
			{
				descriptor.AddProperty("_emptyValue", this.EmptyValue);
			}
			if (!string.IsNullOrEmpty(this.ErrorValue))
			{
				descriptor.AddProperty("_errorValue", this.ErrorValue);
			}
			if (this.pivotModel != null)
			{
				descriptor.AddProperty("_columnGroupDescriptionsCount", (from f in this.Fields
				where f is PivotGridColumnField && !f.IsHidden
				select f).Count<PivotGridField>());
			}
			descriptor.AddScriptProperty("_rhtData", javaScriptSerializer.Serialize(this.InitializeRowHeaderTableData()));
			descriptor.AddScriptProperty("_chtData", javaScriptSerializer.Serialize(this.InitializeColumnHeaderTableData()));
			descriptor.AddScriptProperty("_dtData", javaScriptSerializer.Serialize(this.InitializeDataTableData()));
			if (this.HorizontalScrollDiv != null)
			{
				descriptor.AddProperty("_hzScrollClientID", this.HorizontalScrollDiv.ClientID);
			}
			if (this.VerticalScrollDiv != null)
			{
				descriptor.AddProperty("_verticalScrollClientID", this.VerticalScrollDiv.ClientID);
			}
			descriptor.AddScriptProperty("_clientSettings", javaScriptSerializer.Serialize(this.ClientSettings));
			if (this.ShouldAdjustColumnsLayout)
			{
				descriptor.AddProperty("_shouldAdjustColumnsLayout", this.ShouldAdjustColumnsLayout);
			}
			if (this.HideHorizontalScroll)
			{
				descriptor.AddProperty("_hideHorizontalScroll", this.HideHorizontalScroll);
			}
			if (this.EnableToolTips)
			{
				descriptor.AddProperty("_enableToolTips", true);
			}
			if (this.ClientSettings.EnableFieldsDragDrop)
			{
				descriptor.AddProperty("_enableFieldsDragDrop", this.ClientSettings.EnableFieldsDragDrop);
				Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
				if (!this.ShowColumnHeaderZone)
				{
					dictionary.Add("rpgColumnsZone", true);
				}
				if (!this.ShowDataHeaderZone)
				{
					dictionary.Add("rpgDataZone", true);
				}
				if (!this.ShowFilterHeaderZone)
				{
					dictionary.Add("rpgFilterZone", true);
				}
				if (!this.ShowRowHeaderZone)
				{
					dictionary.Add("rpgRowsZone", true);
				}
				descriptor.AddProperty("_hiddenZones", dictionary);
			}
			if (this.ClientSettings.Resizing.AllowColumnResize)
			{
				descriptor.AddProperty("_columnHeaderCells", (from c in this.resizeableHeaderCellsList
				select c.Value.ID).ToArray<string>());
			}
			if (this.EnableZoneContextMenu)
			{
				descriptor.AddProperty("_enableZoneContextMenu", this.EnableZoneContextMenu);
				descriptor.AddProperty("_showFieldsWindowText", this.Localization.ZoneContextMenuShowFieldsWindow);
				descriptor.AddProperty("_hideFieldsWindowText", this.Localization.ZoneContextMenuHideFieldsWindow);
			}
			if (this.ShouldIncludeContextMenu && !this.IsBoundToOlap)
			{
				descriptor.AddProperty("_enableFieldSettings", true);
			}
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.ClientSettings.ClientEvents);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!(propertyDescriptor.DisplayName == "ViewState"))
				{
					string text = propertyDescriptor.DisplayName.Replace("On", "");
					text = Regex.Replace(text, "^[A-Z]", new MatchEvaluator(RadPivotGrid.ToLower));
					string text2 = propertyDescriptor.GetValue(this.ClientSettings.ClientEvents).ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						descriptor.AddEvent(text, text2);
					}
				}
			}
			if (this.FilteringManager.IsInitFilterCommandInProgress)
			{
				descriptor.AddScriptProperty("_filterWindowClientData", javaScriptSerializer.Serialize(new Dictionary<string, object>
				{
					{
						"isVisible",
						true
					},
					{
						"fieldUniqueName",
						this.FilteringManager.FieldUniqueName
					},
					{
						"filterExpressions",
						this.Filters
					},
					{
						"zoneType",
						this.FilterWindow.ZoneType
					},
					{
						"isInAllFieldsZone",
						this.FilterWindow.IsInAllFieldsZone
					},
					{
						"isReportFilter",
						this.FilterWindow.IsReportFilter
					},
					{
						"valueFilterString",
						this.FilterWindow.GetFilterLocalizedValue("ValueFilter")
					},
					{
						"labelFilterString",
						this.FilterWindow.GetFilterLocalizedValue("LabelFilter")
					}
				}));
			}
			if (this.ShouldIncludeContextMenu && !this.IsBoundToOlap)
			{
				descriptor.AddScriptProperty("_fieldSettingsData", javaScriptSerializer.Serialize(new Dictionary<string, object>
				{
					{
						"ContainerID",
						this.FieldSettingsWindow.ContainerPanel.ClientID
					},
					{
						"Title",
						this.Localization.SummarizeBySettingsTitle
					}
				}));
			}
			if (this.Fields.Count > 0)
			{
				Dictionary<string, Dictionary<string, string>> dictionary2 = new Dictionary<string, Dictionary<string, string>>();
				List<IDictionary> list = new List<IDictionary>();
				foreach (PivotGridField pivotGridField in this.Fields)
				{
					if (pivotGridField.RenderingControl.Parent == null)
					{
						Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
						dictionary3.Add("uniqueName", pivotGridField.UniqueName);
						dictionary3.Add("zoneIndex", pivotGridField.ZoneIndex.ToString());
						dictionary3.Add("zoneType", ((int)pivotGridField.ZoneType).ToString());
						dictionary3.Add("isHidden", pivotGridField.IsHidden.ToString());
						if (pivotGridField is PivotGridAggregateField)
						{
							dictionary3.Add("aggregate", ((int)((PivotGridAggregateField)pivotGridField).Aggregate).ToString());
						}
						list.Add(dictionary3);
					}
					else
					{
						Dictionary<string, string> dictionary4 = new Dictionary<string, string>();
						dictionary4.Add("zoneIndex", pivotGridField.ZoneIndex.ToString());
						dictionary4.Add("zoneType", ((byte)pivotGridField.ZoneType).ToString());
						dictionary4.Add("uniqueName", pivotGridField.UniqueName);
						dictionary4.Add("isHidden", pivotGridField.IsHidden.ToString());
						if (pivotGridField is PivotGridAggregateField)
						{
							dictionary4.Add("aggregate", ((int)((PivotGridAggregateField)pivotGridField).Aggregate).ToString());
						}
						dictionary2.Add(pivotGridField.RenderingControl.ClientID, dictionary4);
					}
				}
				if (dictionary2.Count > 0)
				{
					descriptor.AddScriptProperty("_fieldsData", javaScriptSerializer.Serialize(dictionary2));
				}
				if (list.Count > 0)
				{
					descriptor.AddScriptProperty("hiddenFieldsData", javaScriptSerializer.Serialize(list));
				}
			}
			if (this.EnableConfigurationPanel)
			{
				Dictionary<string, string> dictionary5 = new Dictionary<string, string>();
				if (this.ConfigurationPanel.RenderingControls != null)
				{
					foreach (PivotGridFieldRenderingControl pivotGridFieldRenderingControl in this.ConfigurationPanel.RenderingControls)
					{
						dictionary5.Add(pivotGridFieldRenderingControl.ClientID, pivotGridFieldRenderingControl.OwnerField.UniqueName);
					}
				}
				descriptor.AddScriptProperty("_configurationPanelFieldsData", javaScriptSerializer.Serialize(dictionary5));
				descriptor.AddProperty("_aggregatesPosition", this.AggregatesPosition);
				descriptor.AddProperty("_aggregatesLevel", this.AggregatesLevel);
				if (!this.ConfigurationPanelSettings.EnableDragDrop)
				{
					descriptor.AddProperty("_configurationPanelEnableDragDrop", this.ConfigurationPanelSettings.EnableDragDrop);
				}
				if (!this.ConfigurationPanelSettings.EnableFieldsContextMenu)
				{
					descriptor.AddProperty("_configurationPanelEnableFieldsContextMenu", this.ConfigurationPanelSettings.EnableFieldsContextMenu);
				}
			}
		}

		// Token: 0x06008780 RID: 34688 RVA: 0x001ECAC0 File Offset: 0x001EACC0
		internal static string ToLower(Match m)
		{
			return m.ToString().ToLower();
		}

		// Token: 0x06008781 RID: 34689 RVA: 0x001ECAD0 File Offset: 0x001EACD0
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>(scriptReferences);
			if (this.EnableEmbeddedScripts)
			{
				this.AddFeatureSpecificScriptReferences(list);
			}
			return list;
		}

		// Token: 0x06008782 RID: 34690 RVA: 0x001ECB24 File Offset: 0x001EAD24
		private void AddFeatureSpecificScriptReferences(List<ScriptReference> baseReferences)
		{
			string resourceNameSuffix = "Script";
			string assemblyName = Assembly.GetExecutingAssembly().FullName;
			TFunc<string, ScriptReference> tfunc = (string resourceName) => new ScriptReference(string.Format("{0}{1}.js", resourceName, resourceNameSuffix), assemblyName);
			if (this.ClientSettings.Resizing.AllowColumnResize)
			{
				baseReferences.Add(tfunc("Telerik.Web.UI.PivotGrid.PivotGridColumnResizer"));
			}
			if (this.ClientSettings.EnableFieldsDragDrop || (this.EnableConfigurationPanel && this.ConfigurationPanelSettings.EnableDragDrop))
			{
				baseReferences.Add(tfunc("Telerik.Web.UI.PivotGrid.PivotGridDragDropBase"));
			}
			if (this.ClientSettings.EnableFieldsDragDrop)
			{
				baseReferences.Add(tfunc("Telerik.Web.UI.PivotGrid.PivotGridDragDrop"));
			}
			if (this.ShouldIncludeContextMenu)
			{
				baseReferences.Add(tfunc("Telerik.Web.UI.PivotGrid.PivotGridContextMenu"));
				if (!this.IsBoundToOlap)
				{
					baseReferences.Add(tfunc("Telerik.Web.UI.PivotGrid.Fields.PivotGridFieldSettings"));
				}
			}
			if (this.EnableConfigurationPanel)
			{
				baseReferences.Add(tfunc("Telerik.Web.UI.PivotGrid.ConfigurationPanel.PivotGridConfigurationPanel"));
				baseReferences.Add(tfunc("Telerik.Web.UI.PivotGrid.ConfigurationPanel.PivotGridAggregateLabel"));
				if (this.IsBoundToAdomd || this.IsBoundToXmla)
				{
					baseReferences.Add(tfunc("Telerik.Web.UI.PivotGrid.ConfigurationPanel.PivotGridOlapExtension"));
				}
			}
			if (this.IsMobile || (this.ResolvedRenderMode == RenderMode.Lightweight && base.RuntimeSkin == "Material"))
			{
				bool flag = true;
				RadScriptManager radScriptManager = ScriptManager.GetCurrent(this.Page) as RadScriptManager;
				if (radScriptManager != null)
				{
					flag = radScriptManager.EnableEmbeddedjQuery;
				}
				if (flag)
				{
					baseReferences.Add(new ScriptReference("Telerik.Web.UI.Common.jQuery.js", assemblyName));
				}
				baseReferences.Add(new ScriptReference("Telerik.Web.UI.Common.jQueryPlugins.js", assemblyName));
				baseReferences.Add(new ScriptReference("Telerik.Web.UI.Common.TouchScrollExtender.js", assemblyName));
			}
		}

		// Token: 0x17002B10 RID: 11024
		// (get) Token: 0x06008783 RID: 34691 RVA: 0x001ECCD5 File Offset: 0x001EAED5
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06008784 RID: 34692 RVA: 0x001ECCDC File Offset: 0x001EAEDC
		internal void SetFilterIncludesOrExcludes(string fieldUniqueName, IEnumerable<object> values, SetComparison setComparison, bool suppressRebind = false)
		{
			IPivotSetCondition pivotSetCondition;
			if (this.IsBoundToOlap)
			{
				pivotSetCondition = new PivotGridOlapSetCondition();
			}
			else
			{
				pivotSetCondition = new PivotGridSetCondition();
			}
			foreach (object value in values)
			{
				pivotSetCondition.Items.Add(value);
			}
			pivotSetCondition.Comparison = setComparison;
			PivotGridReportFilterField pivotGridReportFilterField = this.Fields[fieldUniqueName] as PivotGridReportFilterField;
			PivotGridFilter item;
			if (pivotGridReportFilterField != null)
			{
				item = new PivotGridReportFilter
				{
					Condition = (IFilterCondition)pivotSetCondition,
					FieldName = fieldUniqueName
				};
			}
			else if (!this.IsBoundToOlap)
			{
				item = new PivotGridLabelGroupFilter
				{
					Condition = (IFilterCondition)pivotSetCondition,
					FieldName = fieldUniqueName
				};
			}
			else
			{
				item = new PivotGridOlapLabelGroupFilter
				{
					Condition = (IFilterCondition)pivotSetCondition,
					FieldName = fieldUniqueName
				};
			}
			this.Filters.Add(item);
			if (!suppressRebind)
			{
				this.ResetPivotModel();
				this.IsFilterCommandInProgress = true;
				this.Rebind();
			}
		}

		// Token: 0x06008785 RID: 34693 RVA: 0x001ECDF0 File Offset: 0x001EAFF0
		internal void FilterByTopOrBottom(PivotGridFilterFunction filterFunction, PivotGridField field, PivotGridAggregateField aggregateField, PivotGridAggregateType aggregateType, double value, bool suppressRebind = false)
		{
			PivotGridSortedGroupsFilter pivotGridSortedGroupsFilter;
			if (aggregateType == PivotGridAggregateType.Sum)
			{
				pivotGridSortedGroupsFilter = new PivotGridGroupsSumFilter
				{
					Sum = value
				};
			}
			else if (aggregateType == PivotGridAggregateType.Items)
			{
				pivotGridSortedGroupsFilter = new PivotGridGroupsCountFilter
				{
					Count = (int)value
				};
			}
			else
			{
				pivotGridSortedGroupsFilter = new PivotGridGroupsPercentFilter
				{
					Percent = value / 100.0
				};
			}
			pivotGridSortedGroupsFilter.FieldName = field.UniqueName;
			pivotGridSortedGroupsFilter.Selection = ((filterFunction == PivotGridFilterFunction.Top) ? SortedListSelection.Top : SortedListSelection.Bottom);
			if (aggregateField != null)
			{
				pivotGridSortedGroupsFilter.AggregateIndex = aggregateField.AggregateIndex;
			}
			this.Filters.Add(pivotGridSortedGroupsFilter);
			if (!suppressRebind)
			{
				this.IsFilterCommandInProgress = true;
				this.Rebind();
			}
		}

		// Token: 0x06008786 RID: 34694 RVA: 0x001ECE8C File Offset: 0x001EB08C
		private void SetFilterCondition(PivotGridSingleGroupFilter groupFilter, PivotGridFilterFunction filterFunction, string filterValue1, string filterValue2, bool ignoreCase = false)
		{
			if (filterFunction > PivotGridFilterFunction.DoesNotEqual && filterFunction < PivotGridFilterFunction.IsGreaterThan)
			{
				IPivotTextCondition pivotTextCondition;
				if (this.IsBoundToOlap)
				{
					pivotTextCondition = new PivotGridOlapTextComparisonCondition();
				}
				else
				{
					pivotTextCondition = new PivotGridTextComparisonCondition();
				}
				pivotTextCondition.Pattern = filterValue1;
				pivotTextCondition.Comparison = (TextComparison)Enum.Parse(typeof(TextComparison), Enum.GetName(typeof(PivotGridFilterFunction), filterFunction));
				pivotTextCondition.IgnoreCase = ignoreCase;
				groupFilter.Condition = (IFilterCondition)pivotTextCondition;
				return;
			}
			if (filterFunction == PivotGridFilterFunction.IsBetween || filterFunction == PivotGridFilterFunction.IsNotBetween)
			{
				IPivotIntervalCondition pivotIntervalCondition;
				if (this.IsBoundToOlap)
				{
					pivotIntervalCondition = new PivotGridOlapIntervalCondition();
				}
				else
				{
					pivotIntervalCondition = new PivotGridIntervalCondition();
				}
				pivotIntervalCondition.Condition = ((filterFunction == PivotGridFilterFunction.IsBetween) ? IntervalComparison.IsBetween : IntervalComparison.IsNotBetween);
				pivotIntervalCondition.From = this.FilteringManager.GetUnboxedValue(filterValue1);
				pivotIntervalCondition.To = this.FilteringManager.GetUnboxedValue(filterValue2);
				pivotIntervalCondition.IgnoreCase = ignoreCase;
				groupFilter.Condition = (IFilterCondition)pivotIntervalCondition;
				return;
			}
			IPivotComparisonCondition pivotComparisonCondition;
			if (this.IsBoundToOlap)
			{
				pivotComparisonCondition = new PivotGridOlapComparisonCondition();
			}
			else
			{
				pivotComparisonCondition = new PivotGridComparisonCondition();
			}
			pivotComparisonCondition.Condition = (Comparison)Enum.Parse(typeof(Comparison), Enum.GetName(typeof(PivotGridFilterFunction), filterFunction));
			double num;
			if (groupFilter is PivotGridLabelGroupFilter)
			{
				pivotComparisonCondition.Than = this.FilteringManager.GetUnboxedValue(filterValue1);
			}
			else if (double.TryParse(filterValue1, out num))
			{
				pivotComparisonCondition.Than = num;
			}
			else
			{
				pivotComparisonCondition.Than = filterValue1;
			}
			pivotComparisonCondition.IgnoreCase = ignoreCase;
			groupFilter.Condition = (IFilterCondition)pivotComparisonCondition;
		}

		// Token: 0x06008787 RID: 34695 RVA: 0x001ED000 File Offset: 0x001EB200
		private bool IsFieldCouldBeSorted(string fieldUniqueName)
		{
			PivotGridField pivotGridField = this.Fields[fieldUniqueName];
			return pivotGridField is PivotGridRowField || pivotGridField is PivotGridColumnField;
		}

		// Token: 0x06008788 RID: 34696 RVA: 0x001ED064 File Offset: 0x001EB264
		private int GetFieldIndex(PivotGridField field)
		{
			PivotGridFieldZoneType zoneType = field.ZoneType;
			IEnumerable<PivotGridField> enumerable;
			if (zoneType != PivotGridFieldZoneType.Column)
			{
				if (zoneType != PivotGridFieldZoneType.Row)
				{
					throw new PivotGridException("The field should be of type PivotGridColumnField or PivotGridRowField.");
				}
				enumerable = from f in this.fields
				where f.ZoneType == PivotGridFieldZoneType.Row && !f.IsHidden
				select f;
			}
			else
			{
				enumerable = from f in this.Fields
				where f.ZoneType == PivotGridFieldZoneType.Column && !f.IsHidden
				select f;
			}
			enumerable = from f in enumerable
			orderby f.ZoneIndex
			select f;
			int num = 0;
			foreach (PivotGridField pivotGridField in enumerable)
			{
				if (pivotGridField == field)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06008789 RID: 34697 RVA: 0x001ED158 File Offset: 0x001EB358
		internal void ResetPivotModel()
		{
			this.pivotModel = null;
			if (this.EnableCachingInternal)
			{
				this.ModelPersister.Clear(this.UniqueID);
			}
		}

		// Token: 0x0600878A RID: 34698 RVA: 0x001ED17A File Offset: 0x001EB37A
		protected override void PerformSelect()
		{
			this._ignoreDataSourceViewChanged = true;
			this._currentDataSource = null;
			base.PerformSelect();
			this._ignoreDataSourceViewChanged = false;
		}

		// Token: 0x0600878B RID: 34699 RVA: 0x001ED198 File Offset: 0x001EB398
		protected override DataSourceView GetData()
		{
			if (this._currentDataSource == null)
			{
				if (this.IsBoundToIQueryableCollection)
				{
					this._currentDataSource = ((IDataSource)new RadPivotGrid.DummyDataSource((IEnumerable)this.DataSource)).GetView(this.DataMember);
				}
				else
				{
					this._currentDataSource = base.GetData();
				}
			}
			return this._currentDataSource;
		}

		// Token: 0x0600878C RID: 34700 RVA: 0x001ED1EA File Offset: 0x001EB3EA
		protected override void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			if (!this._ignoreDataSourceViewChanged)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x0600878D RID: 34701 RVA: 0x001ED1FC File Offset: 0x001EB3FC
		internal Dictionary<string, object> InitializeRowHeaderTableData()
		{
			return new Dictionary<string, object>
			{
				{
					"ClientID",
					this.RowHeaderTable.ClientID
				}
			};
		}

		// Token: 0x0600878E RID: 34702 RVA: 0x001ED228 File Offset: 0x001EB428
		internal Dictionary<string, object> InitializeColumnHeaderTableData()
		{
			return new Dictionary<string, object>
			{
				{
					"ClientID",
					this.ColumnHeaderTable.ClientID
				}
			};
		}

		// Token: 0x0600878F RID: 34703 RVA: 0x001ED254 File Offset: 0x001EB454
		internal Dictionary<string, object> InitializeDataTableData()
		{
			return new Dictionary<string, object>
			{
				{
					"ClientID",
					this.DataTable.ClientID
				}
			};
		}

		// Token: 0x06008790 RID: 34704 RVA: 0x001ED280 File Offset: 0x001EB480
		internal PivotGridField CreateFieldByType(string fieldtype)
		{
			PivotGridField result = null;
			if (fieldtype.IndexOf("PivotGridRowField", StringComparison.CurrentCulture) > -1)
			{
				result = new PivotGridRowField();
			}
			else if (fieldtype.IndexOf("PivotGridColumnField", StringComparison.CurrentCulture) > -1)
			{
				result = new PivotGridColumnField();
			}
			else if (fieldtype.IndexOf("PivotGridAggregateField", StringComparison.CurrentCulture) > -1)
			{
				result = new PivotGridAggregateField();
			}
			else if (fieldtype.IndexOf("PivotGridReportFilterField", StringComparison.CurrentCulture) > -1)
			{
				result = new PivotGridReportFilterField();
			}
			return result;
		}

		// Token: 0x06008791 RID: 34705 RVA: 0x001ED2EC File Offset: 0x001EB4EC
		internal PivotGridField CreateFieldByZoneType(PivotGridFieldZoneType zone)
		{
			PivotGridField result = null;
			switch (zone)
			{
			case PivotGridFieldZoneType.Filter:
				result = new PivotGridReportFilterField();
				break;
			case PivotGridFieldZoneType.Aggregate:
				result = new PivotGridAggregateField();
				break;
			case PivotGridFieldZoneType.Filter | PivotGridFieldZoneType.Aggregate:
				break;
			case PivotGridFieldZoneType.Column:
				result = new PivotGridColumnField();
				break;
			default:
				if (zone == PivotGridFieldZoneType.Row)
				{
					result = new PivotGridRowField();
				}
				break;
			}
			return result;
		}

		// Token: 0x06008792 RID: 34706 RVA: 0x001ED33C File Offset: 0x001EB53C
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(base.SaveViewState());
			arrayList.Add(((IStateManager)this.Fields).SaveViewState());
			arrayList.Add(((IStateManager)this.PagerStyle).SaveViewState());
			arrayList.Add(((IStateManager)this.RowHeaderCellStyle).SaveViewState());
			arrayList.Add(((IStateManager)this.ColumnHeaderCellStyle).SaveViewState());
			arrayList.Add(((IStateManager)this.RowTotalCellStyle).SaveViewState());
			arrayList.Add(((IStateManager)this.ColumnTotalCellStyle).SaveViewState());
			arrayList.Add(((IStateManager)this.DataCellStyle).SaveViewState());
			arrayList.Add(((IStateManager)this.RowGrandTotalCellStyle).SaveViewState());
			arrayList.Add(((IStateManager)this.ColumnGrandTotalCellStyle).SaveViewState());
			arrayList.Add(((IStateManager)this.ClientSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.AccessibilitySettings).SaveViewState());
			arrayList.Add(((IStateManager)this.TotalsSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.ConfigurationPanelSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.FieldsPopupSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.OlapSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.ExportSettings).SaveViewState());
			if (!this.UsesControlState)
			{
				this.SaveControlStateObject(arrayList);
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06008793 RID: 34707 RVA: 0x001ED49B File Offset: 0x001EB69B
		protected virtual void SaveControlStateObject(IList state)
		{
			state.Add(((IStateManager)this.ControlState).SaveViewState());
			state.Add(((IStateManager)this.SortExpressions).SaveViewState());
			state.Add(((IStateManager)this.Filters).SaveViewState());
		}

		// Token: 0x06008794 RID: 34708 RVA: 0x001ED4D4 File Offset: 0x001EB6D4
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int index = 0;
				base.LoadViewState(array[index++]);
				((IStateManager)this.Fields).LoadViewState(array[index++]);
				((IStateManager)this.PagerStyle).LoadViewState(array[index++]);
				((IStateManager)this.RowHeaderCellStyle).LoadViewState(array[index++]);
				((IStateManager)this.ColumnHeaderCellStyle).LoadViewState(array[index++]);
				((IStateManager)this.RowTotalCellStyle).LoadViewState(array[index++]);
				((IStateManager)this.ColumnTotalCellStyle).LoadViewState(array[index++]);
				((IStateManager)this.DataCellStyle).LoadViewState(array[index++]);
				((IStateManager)this.RowGrandTotalCellStyle).LoadViewState(array[index++]);
				((IStateManager)this.ColumnGrandTotalCellStyle).LoadViewState(array[index++]);
				((IStateManager)this.ClientSettings).LoadViewState(array[index++]);
				((IStateManager)this.AccessibilitySettings).LoadViewState(array[index++]);
				((IStateManager)this.TotalsSettings).LoadViewState(array[index++]);
				((IStateManager)this.ConfigurationPanelSettings).LoadViewState(array[index++]);
				((IStateManager)this.FieldsPopupSettings).LoadViewState(array[index++]);
				((IStateManager)this.OlapSettings).LoadViewState(array[index++]);
				((IStateManager)this.ExportSettings).LoadViewState(array[index++]);
				if (!this.UsesControlState)
				{
					this.LoadControlStateObject(array, index);
				}
			}
		}

		// Token: 0x06008795 RID: 34709 RVA: 0x001ED62D File Offset: 0x001EB82D
		protected virtual void LoadControlStateObject(object[] state, int index)
		{
			((IStateManager)this.ControlState).LoadViewState(state[index++]);
			((IStateManager)this.SortExpressions).LoadViewState(state[index++]);
			((IStateManager)this.Filters).LoadViewState(state[index++]);
		}

		// Token: 0x06008796 RID: 34710 RVA: 0x001ED668 File Offset: 0x001EB868
		protected override void TrackViewState()
		{
			if (base.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			((IStateManager)this.Fields).TrackViewState();
			((IStateManager)this.PagerStyle).TrackViewState();
			((IStateManager)this.RowHeaderCellStyle).TrackViewState();
			((IStateManager)this.ColumnHeaderCellStyle).TrackViewState();
			((IStateManager)this.RowTotalCellStyle).TrackViewState();
			((IStateManager)this.ColumnTotalCellStyle).TrackViewState();
			((IStateManager)this.DataCellStyle).TrackViewState();
			((IStateManager)this.RowGrandTotalCellStyle).TrackViewState();
			((IStateManager)this.ColumnGrandTotalCellStyle).TrackViewState();
			((IStateManager)this.ClientSettings).TrackViewState();
			((IStateManager)this.AccessibilitySettings).TrackViewState();
			((IStateManager)this.TotalsSettings).TrackViewState();
			((IStateManager)this.ConfigurationPanelSettings).TrackViewState();
			((IStateManager)this.FieldsPopupSettings).TrackViewState();
			((IStateManager)this.OlapSettings).TrackViewState();
			((IStateManager)this.ExportSettings).TrackViewState();
		}

		// Token: 0x06008797 RID: 34711 RVA: 0x001ED73C File Offset: 0x001EB93C
		protected override object SaveControlState()
		{
			object value = base.SaveControlState();
			ArrayList arrayList = new ArrayList();
			arrayList.Add(value);
			this.SaveControlStateObject(arrayList);
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06008798 RID: 34712 RVA: 0x001ED778 File Offset: 0x001EB978
		protected override void LoadControlState(object savedState)
		{
			object[] array = savedState as object[];
			if (array != null)
			{
				base.LoadControlState(array);
				this.LoadControlStateObject(array, 1);
				return;
			}
			base.LoadControlState(savedState);
		}

		// Token: 0x06008799 RID: 34713 RVA: 0x001ED7A8 File Offset: 0x001EB9A8
		private void SavePagingData(bool dataBinding, int dataSourceItemsCount)
		{
			if (dataBinding)
			{
				this.ControlState["_!DSIC"] = dataSourceItemsCount;
				if (this.pagingManager.IsPagingEnabled)
				{
					this.ControlState["_!PCount"] = this.pagingManager.PageCount;
					return;
				}
				this.ControlState["_!PCount"] = null;
			}
		}

		// Token: 0x0600879A RID: 34714 RVA: 0x001ED810 File Offset: 0x001EBA10
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			bool result = false;
			if (args is PivotGridCommandEventArgs)
			{
				PivotGridCommandEventArgs e = (PivotGridCommandEventArgs)args;
				this.OnItemCommand(e);
				result = true;
			}
			if (args is IPivotGridCommandEvent)
			{
				IPivotGridCommandEvent pivotGridCommandEvent = (IPivotGridCommandEvent)args;
				if (!pivotGridCommandEvent.Canceled)
				{
					pivotGridCommandEvent.ExecuteCommand(source);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600879B RID: 34715 RVA: 0x001ED858 File Offset: 0x001EBA58
		internal void SetRequiresDataBindingIfInitialized()
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x0600879C RID: 34716 RVA: 0x001ED869 File Offset: 0x001EBA69
		internal void CallAutoDataBind(PivotGridRebindReason rebindReason)
		{
			this.AutoDataBind(rebindReason);
		}

		// Token: 0x0600879D RID: 34717 RVA: 0x001ED874 File Offset: 0x001EBA74
		protected virtual void AutoDataBind(PivotGridRebindReason rebindReason)
		{
			if (!this.Visible && (rebindReason & PivotGridRebindReason.ExplicitRebind) != PivotGridRebindReason.ExplicitRebind)
			{
				return;
			}
			this.ObtainDataSource(rebindReason, base.IsBoundUsingDataSourceID);
			if (this.IsBoundToAdomd || this.IsBoundToXmla || (this.DataSource != null && !base.IsBoundUsingDataSourceID) || (base.IsBoundUsingDataSourceID && rebindReason == PivotGridRebindReason.ExplicitRebind) || (this.DataSource != null && rebindReason == PivotGridRebindReason.ExplicitRebind))
			{
				this.DataBind();
			}
		}

		// Token: 0x0600879E RID: 34718 RVA: 0x001ED8DC File Offset: 0x001EBADC
		protected override bool LoadClientState(Dictionary<string, object> clientState)
		{
			if (this.AlwaysAutoBindOnPostBack && this.Page.IsPostBack)
			{
				this.shouldCallDataBindOnLoad = false;
				base.RequiresDataBinding = true;
				this.AutoDataBind(PivotGridRebindReason.PostbackViewStateNotPersisted);
			}
			if (clientState.ContainsKey("scrolledPosition"))
			{
				string text = (string)clientState["scrolledPosition"];
				string[] array = text.Split(new char[]
				{
					','
				});
				this.ClientSettings.Scrolling.ScrollTop = array[0];
				this.ClientSettings.Scrolling.ScrollLeft = array[1];
			}
			if (clientState.ContainsKey("fieldsWindowClientData"))
			{
				Dictionary<string, object> dictionary = clientState["fieldsWindowClientData"] as Dictionary<string, object>;
				this.FieldsWindow.VisibleOnPageLoad = (bool)dictionary["isVisible"];
				if (dictionary.ContainsKey("x"))
				{
					this.FieldsWindow.Left = (int)dictionary["x"];
				}
				if (dictionary.ContainsKey("y"))
				{
					this.FieldsWindow.Top = (int)dictionary["y"];
				}
				if (dictionary.ContainsKey("width"))
				{
					this.FieldsWindow.Width = (int)dictionary["width"];
				}
				if (dictionary.ContainsKey("height"))
				{
					this.FieldsWindow.Height = (int)dictionary["height"];
				}
			}
			if (clientState.ContainsKey("configurationPanelLayoutType"))
			{
				this.ConfigurationPanelSettings.LayoutType = (PivotGridConfigurationPanelLayoutType)clientState["configurationPanelLayoutType"];
			}
			if (clientState.ContainsKey("resizedColumns"))
			{
				string text2 = (string)clientState["resizedColumns"];
				string[] array2 = text2.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text3 in array2)
				{
					string[] array4 = text3.Split(new char[]
					{
						','
					});
					string key = array4[0];
					Unit unit = Unit.Parse(array4[1]);
					PivotGridColumnHeaderCell pivotGridColumnHeaderCell = this.resizableHeaderCells[key];
					pivotGridColumnHeaderCell.Width = unit;
					Dictionary<string, Unit> resizedColumnsWidth = this.ResizedColumnsWidth;
					if (!resizedColumnsWidth.ContainsKey(key))
					{
						resizedColumnsWidth.Add(key, unit);
					}
					else
					{
						resizedColumnsWidth[key] = unit;
					}
					this.ResizedColumnsWidth = resizedColumnsWidth;
				}
			}
			if (clientState.ContainsKey("resizedTableWidth"))
			{
				string text4 = (string)clientState["resizedTableWidth"];
				if (!string.IsNullOrEmpty(text4))
				{
					if (this.ClientSettings.Scrolling.AllowVerticalScroll)
					{
						this.ColumnHeaderTable.Width = Unit.Parse(text4);
						this.DataTable.Width = Unit.Parse(text4);
					}
					else
					{
						this.DataTable.Width = Unit.Parse(text4);
					}
				}
			}
			return false;
		}

		// Token: 0x0600879F RID: 34719 RVA: 0x001EDBCC File Offset: 0x001EBDCC
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			this.IsDataBinding = dataBinding;
			if (dataBinding)
			{
				this.Fields.ClearRenderControls();
				this.Fields.ClearGroupDescriptors();
				this.Fields.ClearAggregateDescriptors();
				if (this.Page != null && !this.Page.IsPostBack)
				{
					this.ResetPivotModel();
				}
				if (!this.FilteringManager.IsInitFilterCommandInProgress)
				{
					this.ShouldCreateFilterDialog = false;
					this.ShouldCreateFilterWindow = false;
				}
			}
			this.ClearTables();
			this.Fields.EnsureUniqueNames();
			this.Fields.EnsureZoneIndexes();
			if (dataBinding)
			{
				this.Fields.RemoveGroupDescriptionsParent();
				this.Fields.EnsureGroupDescriptions();
			}
			this.ApplySortExpressions();
			PivotHelperModelManager pivotHelperModelManager = new PivotHelperModelManager(this);
			this.ApplyPredefinedReportFilters();
			if (dataBinding)
			{
				this.SetViewModel(dataSource, dataBinding);
				this.Fields.AddMissingHiddenFieldsFromDataSource();
				this.HandleExpandCollapse();
				this.shouldChangeCollapsedState = false;
				this.TotalItemCount = this.rowLayout.VisibleLineCount;
				pivotHelperModelManager.BuildRowPivotModel();
				pivotHelperModelManager.BuildColumnsPivotModel();
				pivotHelperModelManager.BuildDataPivotModel();
				this.ColumnGroupsCount = pivotHelperModelManager.GetColumnsGroupCount();
			}
			if (!this.IsExporting)
			{
				this.TryWrapOuterTable();
				this.Items.Clear();
				if (this.ShowFilterHeaderZone)
				{
					this.CreateFilterItem(dataBinding);
				}
				if (this.ShowDataHeaderZone || this.ShowColumnHeaderZone)
				{
					this.CreateAggregateItem(dataBinding);
				}
				this.CreateRowItem(dataBinding);
				this.CreateRowHeaderItems(dataBinding, dataSource);
				this.SetUpPagingManager();
				this.CreatePagerItem(true, dataBinding);
				this.SavePagingData(dataBinding, this.TotalItemCount - 1);
				this.CreateColumnHeaderItems(dataBinding);
				this.CreateDataItems(dataBinding);
				this.CreatePagerItem(false, dataBinding);
				this.CreateContextMenu();
				this.CreateFieldsWindow();
				this.CreateConfigurationPanel();
				this.FilteringManager.SetUpFilterWindowControls();
				this.CreateFieldSettingsWindow();
				if (this.EnableToolTips)
				{
					this.Controls.Add(this.ToolTipManager);
				}
			}
			else
			{
				this.Items.Clear();
				this.CreateRowItem(dataBinding);
				this.CreateRowHeaderItems(dataBinding, dataSource);
				this.CreateColumnHeaderItems(dataBinding);
				this.CreateDataItems(dataBinding);
			}
			return 0;
		}

		// Token: 0x060087A0 RID: 34720 RVA: 0x001EDDD4 File Offset: 0x001EBFD4
		private void ApplyPredefinedReportFilters()
		{
			IEnumerable<PivotGridField> enumerable = from f in this.Fields
			where f.FieldType == "PivotGridReportFilterField"
			select f;
			foreach (PivotGridField pivotGridField in enumerable)
			{
				PivotGridReportFilterField pivotGridReportFilterField = (PivotGridReportFilterField)pivotGridField;
				if (pivotGridReportFilterField.IsFiltered)
				{
					SetComparison setComparison = (SetComparison)Enum.Parse(typeof(SetComparison), pivotGridReportFilterField.FilterType.ToString(), true);
					IEnumerable<object> values = RadPivotGrid.ConvertArray(pivotGridReportFilterField.FilterValues, pivotGridReportFilterField.FilterValueType);
					this.SetFilterIncludesOrExcludes(pivotGridReportFilterField.UniqueName, values, setComparison, true);
				}
			}
		}

		// Token: 0x060087A1 RID: 34721 RVA: 0x001EDE98 File Offset: 0x001EC098
		internal static IEnumerable<object> ConvertArray(string[] values, Type type)
		{
			if (type == typeof(string))
			{
				return values;
			}
			List<object> list = new List<object>();
			foreach (string value in values)
			{
				list.Add(Convert.ChangeType(value, type));
			}
			return list;
		}

		// Token: 0x060087A2 RID: 34722 RVA: 0x001EDEE4 File Offset: 0x001EC0E4
		private void HandleExpandCollapse()
		{
			if (this.expandCollapseRowLevels.Count > 0)
			{
				this.CollapseAllGroupsInLayout(this.rowLayout, this.expandCollapseRowLevels, this.CollapsedRowIndexes);
			}
			if (this.expandCollapseColumnLevels.Count > 0)
			{
				this.CollapseAllGroupsInLayout(this.columnLayout, this.expandCollapseColumnLevels, this.CollapsedColumnIndexes);
			}
			this.CollapseRowGroups();
			this.CollapseColumnGroups();
			if (this.RowCollapsedCommandInProgress && this.shouldChangeCollapsedState)
			{
				this.AddRemoveCollapsedGroupIndexBySlot(this.rowGroupExpandCollapseSlot, this.RowLayout, this.CollapsedRowIndexes, this.RowGroupsDefaultExpanded);
			}
			if (this.ColumnCollapsedCommandInProgress && this.shouldChangeCollapsedState)
			{
				this.AddRemoveCollapsedGroupIndexBySlot(this.columnGroupExpandCollapseSlot, this.ColumnLayout, this.CollapsedColumnIndexes, this.ColumnGroupsDefaultExpanded);
			}
		}

		// Token: 0x060087A3 RID: 34723 RVA: 0x001EDFA8 File Offset: 0x001EC1A8
		private void TryWrapOuterTable()
		{
			if (this.EnableConfigurationPanel)
			{
				Panel panel = new Panel();
				if (this.ConfigurationPanelSettings.Position == PivotGridConfigurationPanelPosition.Left || this.ConfigurationPanelSettings.Position == PivotGridConfigurationPanelPosition.Right)
				{
					PivotGridTable pivotGridTable = new PivotGridTable(this);
					this.Controls.Add(pivotGridTable);
					pivotGridTable.CssClass = "rpgTableWrapper";
					TableHeaderRow tableHeaderRow = new TableHeaderRow();
					pivotGridTable.Rows.Add(tableHeaderRow);
					tableHeaderRow.Style["display"] = "none";
					tableHeaderRow.TableSection = TableRowSection.TableHeader;
					TableHeaderCell tableHeaderCell = new TableHeaderCell();
					tableHeaderRow.Cells.Add(tableHeaderCell);
					tableHeaderCell.Attributes["scope"] = "col";
					TableRow tableRow = new TableRow();
					pivotGridTable.Rows.Add(tableRow);
					TableCell tableCell = new TableCell();
					TableCell tableCell2 = new TableCell();
					if (this.ConfigurationPanelSettings.Position == PivotGridConfigurationPanelPosition.Left)
					{
						tableRow.Cells.Add(tableCell);
						tableRow.Cells.Add(tableCell2);
					}
					else if (this.ConfigurationPanelSettings.Position == PivotGridConfigurationPanelPosition.Right)
					{
						tableRow.Cells.Add(tableCell2);
						tableRow.Cells.Add(tableCell);
					}
					tableCell.ID = "ConfigurationPanelCell";
					tableCell2.Controls.Add(panel);
					if (this.Width.Type != UnitType.Percentage && this.Width.Value != 0.0)
					{
						panel.Width = this.Width;
					}
				}
				else
				{
					this.Controls.Add(panel);
				}
				if (this.ConfigurationPanelSettings.Position != PivotGridConfigurationPanelPosition.FieldsWindow)
				{
					panel.CssClass = "rpgOuterTableWrapper";
					panel.Controls.Add(this.OuterTable);
				}
				else
				{
					this.Controls.Add(this.OuterTable);
				}
			}
			else
			{
				this.Controls.Add(this.OuterTable);
			}
			AccessibilityHelper.AddAccessibilityRow(this.OuterTable, string.IsNullOrEmpty(this.OuterTable.Caption) ? "<span style='display: none'>empty</span>" : this.OuterTable.Caption);
		}

		// Token: 0x060087A4 RID: 34724 RVA: 0x001EE1B5 File Offset: 0x001EC3B5
		private void CreateFieldSettingsWindow()
		{
			if (this.ShouldIncludeContextMenu && !this.IsBoundToOlap)
			{
				this.Controls.Add(this.FieldSettingsWindow);
			}
		}

		// Token: 0x060087A5 RID: 34725 RVA: 0x001EE1D8 File Offset: 0x001EC3D8
		private void CreateFieldsWindow()
		{
			if (this.ShouldIncludeFieldsWindow)
			{
				PivotGridFieldsWindow pivotGridFieldsWindow;
				if (this.IsDataBinding)
				{
					pivotGridFieldsWindow = new PivotGridFieldsWindow(this);
				}
				else
				{
					pivotGridFieldsWindow = this.FieldsWindow;
				}
				this.Controls.Add(pivotGridFieldsWindow);
				pivotGridFieldsWindow.Initialize(this.fieldsWindow);
				this.fieldsWindow = pivotGridFieldsWindow;
			}
		}

		// Token: 0x060087A6 RID: 34726 RVA: 0x001EE226 File Offset: 0x001EC426
		private void CreateContextMenu()
		{
			if (this.ShouldIncludeContextMenu)
			{
				this.Controls.Add(this.ContextMenu);
				this.ContextMenu.ID = "ContextMenu";
				if (this.IsDataBinding)
				{
					this.ContextMenu.Initialize();
				}
			}
		}

		// Token: 0x060087A7 RID: 34727 RVA: 0x001EE264 File Offset: 0x001EC464
		private void CreateConfigurationPanel()
		{
			if (this.EnableConfigurationPanel)
			{
				PivotGridConfigurationPanelPosition position = this.ConfigurationPanelSettings.Position;
				if (position == PivotGridConfigurationPanelPosition.Left || position == PivotGridConfigurationPanelPosition.Right)
				{
					this.FindControl("ConfigurationPanelCell").Controls.Add(this.ConfigurationPanel);
				}
				else if (position == PivotGridConfigurationPanelPosition.Top)
				{
					this.Controls.AddAt(0, this.ConfigurationPanel);
				}
				else if (position == PivotGridConfigurationPanelPosition.Bottom)
				{
					this.Controls.Add(this.ConfigurationPanel);
				}
				else if (position == PivotGridConfigurationPanelPosition.FieldsWindow)
				{
					Control control = this.FieldsWindow.ContentContainer.FindControl("FieldsWindowWrapperPanel");
					if (control != null)
					{
						control.Controls.Add(this.ConfigurationPanel);
					}
				}
				this.ConfigurationPanel.ID = "ConfigurationPanel";
				if (this.IsDataBinding)
				{
					this.ConfigurationPanel.Initialize();
				}
			}
		}

		// Token: 0x060087A8 RID: 34728 RVA: 0x001EE32C File Offset: 0x001EC52C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if ((this.ConfigurationPanelSettings.Position == PivotGridConfigurationPanelPosition.Left || this.ConfigurationPanelSettings.Position == PivotGridConfigurationPanelPosition.Right) && this.Width.Type != UnitType.Percentage && this.Width.Value != 0.0)
			{
				this.Width = new Unit(this.Width.Value + 300.0, this.Width.Type);
				base.AddAttributesToRender(writer);
				this.Width = new Unit(this.Width.Value - 300.0, this.Width.Type);
				return;
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060087A9 RID: 34729 RVA: 0x001EE3F8 File Offset: 0x001EC5F8
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			base.RenderContents(writer);
		}

		// Token: 0x060087AA RID: 34730 RVA: 0x001EE444 File Offset: 0x001EC644
		private void ApplySortExpressions()
		{
			foreach (PivotGridField pivotGridField in this.Fields)
			{
				PivotGridSortExpression pivotGridSortExpression = new PivotGridSortExpression();
				pivotGridSortExpression.FieldName = pivotGridField.UniqueName;
				pivotGridSortExpression.SortOrder = pivotGridField.SortOrder;
				if (!this.SortExpressions.ContainsSortExpression(pivotGridSortExpression))
				{
					this.SortExpressions.Add(pivotGridSortExpression);
				}
			}
			using (IEnumerator enumerator2 = this.SortExpressions.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					PivotGridSortExpression sortExpression = (PivotGridSortExpression)enumerator2.Current;
					PivotGridField pivotGridField2 = (from field in this.Fields
					where field.UniqueName == sortExpression.FieldName && !field.IsHidden
					select field).FirstOrDefault<PivotGridField>();
					if (pivotGridField2 != null)
					{
						pivotGridField2.SortOrder = sortExpression.SortOrder;
					}
				}
			}
		}

		// Token: 0x060087AB RID: 34731 RVA: 0x001EE558 File Offset: 0x001EC758
		protected void SetUpPagingManager()
		{
			this.pagingManager = new PivotGridPagingManager(Math.Max(0, this.TotalItemCount - 1));
			this.pagingManager.AllowPaging = this.AllowPaging;
			this.pagingManager.PageSize = this.PageSize;
			if (this.CurrentPageIndex >= this.pagingManager.PageCount)
			{
				int num = this.pagingManager.PageCount - 1;
				if (num < 0)
				{
					num = 0;
				}
				this.CurrentPageIndex = num;
			}
			this.pagingManager.CurrentPageIndex = this.CurrentPageIndex;
		}

		// Token: 0x060087AC RID: 34732 RVA: 0x001EE618 File Offset: 0x001EC818
		protected void CreatePagerItem(bool isTopItem, bool dataBinding)
		{
			if (this.ShouldCratePagerItem(isTopItem))
			{
				if (!this.PagerStyle.AlwaysVisible && this.ControlState["_!TotalItemCount"] != null && (int)this.ControlState["_!TotalItemCount"] < this.PageSize)
				{
					return;
				}
				int num = 1 + (from f in this.Fields
				where f is PivotGridRowField && !f.IsHidden
				select f).Sum((PivotGridField f) => (f as PivotGridRowField).ColumnSpan);
				if (this.ClientSettings.Scrolling.AllowVerticalScroll)
				{
					num++;
				}
				if (this.AggregatesPosition == PivotGridAxis.Rows)
				{
					if ((from field in this.Fields
					where field is PivotGridAggregateField && !field.IsHidden
					select field).Count<PivotGridField>() > 1)
					{
						num++;
					}
				}
				num = Math.Max(2, num);
				PivotGridPagerItem pivotGridPagerItem = new PivotGridPagerItem(this, PivotGridItemType.PagerItem, dataBinding, num);
				if (isTopItem)
				{
					if (this.OuterTable.Rows.Count > 0 && this.OuterTable.Rows[0].TableSection == TableRowSection.TableHeader)
					{
						this.OuterTable.Rows.AddAt(1, pivotGridPagerItem);
					}
					else
					{
						this.OuterTable.Rows.AddAt(0, pivotGridPagerItem);
					}
				}
				else
				{
					this.OuterTable.Rows.Add(pivotGridPagerItem);
				}
				pivotGridPagerItem.IsTopItem = isTopItem;
				pivotGridPagerItem.Initialize();
			}
		}

		// Token: 0x060087AD RID: 34733 RVA: 0x001EE794 File Offset: 0x001EC994
		protected bool ShouldCratePagerItem(bool isTopItem)
		{
			return this.pagingManager.IsPagingEnabled && ((isTopItem && this.PagerStyle.Position > PivotGridPagerPosition.Bottom) || (!isTopItem && this.PagerStyle.Position != PivotGridPagerPosition.Top));
		}

		// Token: 0x17002B11 RID: 11025
		// (get) Token: 0x060087AE RID: 34734 RVA: 0x001EE7CC File Offset: 0x001EC9CC
		internal bool ShouldCreateFilterControls
		{
			get
			{
				return this.AllowFiltering;
			}
		}

		// Token: 0x17002B12 RID: 11026
		// (get) Token: 0x060087AF RID: 34735 RVA: 0x001EE7D4 File Offset: 0x001EC9D4
		// (set) Token: 0x060087B0 RID: 34736 RVA: 0x001EE80A File Offset: 0x001ECA0A
		internal bool ShouldCreateFilterWindow
		{
			get
			{
				return this.AllowFiltering && this.ControlState["ShouldCreateFilterWindow"] != null && (bool)this.ControlState["ShouldCreateFilterWindow"];
			}
			set
			{
				this.ControlState["ShouldCreateFilterWindow"] = value;
			}
		}

		// Token: 0x17002B13 RID: 11027
		// (get) Token: 0x060087B1 RID: 34737 RVA: 0x001EE822 File Offset: 0x001ECA22
		// (set) Token: 0x060087B2 RID: 34738 RVA: 0x001EE858 File Offset: 0x001ECA58
		internal bool ShouldCreateFilterDialog
		{
			get
			{
				return this.AllowFiltering && this.ControlState["ShouldCreateFilterDialog"] != null && (bool)this.ControlState["ShouldCreateFilterDialog"];
			}
			set
			{
				this.ControlState["ShouldCreateFilterDialog"] = value;
			}
		}

		// Token: 0x060087B3 RID: 34739 RVA: 0x001EE870 File Offset: 0x001ECA70
		internal void CreateFilterItem(bool dataBinding)
		{
			PivotGridFilterItem pivotGridFilterItem = new PivotGridFilterItem(this, PivotGridItemType.Filter, dataBinding);
			this.OuterTable.Rows.Add(pivotGridFilterItem);
			pivotGridFilterItem.Initialize();
		}

		// Token: 0x060087B4 RID: 34740 RVA: 0x001EE8A0 File Offset: 0x001ECAA0
		internal void CreateAggregateItem(bool dataBinding)
		{
			PivotGridAggregateItem pivotGridAggregateItem = new PivotGridAggregateItem(this, PivotGridItemType.Aggregate, dataBinding);
			this.OuterTable.Rows.Add(pivotGridAggregateItem);
			pivotGridAggregateItem.Initialize();
		}

		// Token: 0x060087B5 RID: 34741 RVA: 0x001EE8D0 File Offset: 0x001ECAD0
		internal void CreateRowItem(bool dataBinding)
		{
			PivotGridRowItem pivotGridRowItem = new PivotGridRowItem(this, PivotGridItemType.Row, dataBinding);
			this.OuterTable.Rows.Add(pivotGridRowItem);
			pivotGridRowItem.Initialize();
		}

		// Token: 0x060087B6 RID: 34742 RVA: 0x001EE900 File Offset: 0x001ECB00
		internal void CreateRowHeaderItems(bool dataBinding, IEnumerable dataSource)
		{
			this.AddRowHeaderItems(dataBinding);
			PivotGridZone zoneByType = this.GetZoneByType(PivotGridZoneType.Data);
			int num = this.RowHeaderModel.Rows.Count;
			if (zoneByType != null && !this.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				num++;
				zoneByType.RowSpan = num;
			}
			this.AddHorizontalScrollBar();
		}

		// Token: 0x060087B7 RID: 34743 RVA: 0x001EE98C File Offset: 0x001ECB8C
		private void AddHorizontalScrollBar()
		{
			PivotGridTableRow pivotGridTableRow = new PivotGridTableRow();
			this.OuterTable.Rows.Add(pivotGridTableRow);
			PivotGridTableCell pivotGridTableCell = new PivotGridTableCell();
			pivotGridTableRow.Cells.Add(pivotGridTableCell);
			pivotGridTableCell.CssClass = "rpgHorizontalScroll";
			int num = 1 + (from f in this.Fields
			where f is PivotGridRowField && !f.IsHidden
			select f).Sum((PivotGridField f) => (f as PivotGridRowField).ColumnSpan);
			if (this.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				num++;
			}
			if (this.AggregatesPosition == PivotGridAxis.Rows)
			{
				if ((from field in this.Fields
				where field is PivotGridAggregateField && !field.IsHidden
				select field).Count<PivotGridField>() > 1)
				{
					num++;
				}
			}
			pivotGridTableCell.ColumnSpan = Math.Max(2, num);
			Panel panel = new Panel();
			panel.ID = "HzSD";
			pivotGridTableCell.Controls.Add(panel);
			this.horizontalScrollDiv = panel;
			Panel child = new Panel();
			panel.Controls.Add(child);
		}

		// Token: 0x060087B8 RID: 34744 RVA: 0x001EEAE0 File Offset: 0x001ECCE0
		private void AddRowHeaderItems(bool dataBinding)
		{
			if (this.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				PivotGridTableRow pivotGridTableRow = new PivotGridTableRow();
				this.OuterTable.Rows.Add(pivotGridTableRow);
				PivotGridTableCell pivotGridTableCell = new PivotGridTableCell();
				pivotGridTableRow.Cells.Add(pivotGridTableCell);
				int num = (from f in this.Fields
				where f is PivotGridRowField && !f.IsHidden
				select f).Count<PivotGridField>();
				if (this.AggregatesPosition == PivotGridAxis.Rows)
				{
					IEnumerable<PivotGridField> source = from f in this.Fields
					where f is PivotGridAggregateField && !f.IsHidden
					select f;
					if (source.Count<PivotGridField>() > 1)
					{
						num++;
					}
				}
				if (num > 1)
				{
					pivotGridTableCell.ColumnSpan = num;
				}
				pivotGridTableCell.CssClass = "rpgRowHeaderZone";
				Panel panel = new Panel();
				pivotGridTableCell.Controls.Add(panel);
				panel.CssClass = "rpgRowHeaderZoneDiv";
				panel.ID = "RowHeaderZoneDiv";
				panel.Style.Add("height", this.clientSettings.Scrolling.ScrollHeight.ToString());
				panel.Controls.Add(this.RowHeaderTable);
				AccessibilityHelper.AddAccessibilityRow(this.RowHeaderTable, string.IsNullOrEmpty(this.RowHeaderTable.Caption) ? "<span style='display: none'>empty</span>" : this.RowHeaderTable.Caption);
				PivotGridDataZone pivotGridDataZone = new PivotGridDataZone(this);
				pivotGridTableRow.Cells.Add(pivotGridDataZone);
				pivotGridDataZone.CssClass = "rpgContentZone";
				Panel panel2 = new Panel();
				pivotGridDataZone.Controls.Add(panel2);
				panel2.CssClass = "rpgContentZoneDiv";
				panel2.ID = "ContentZoneDiv";
				panel2.Style.Add("height", this.clientSettings.Scrolling.ScrollHeight.ToString());
				panel2.Controls.Add(this.DataTable);
				AccessibilityHelper.AddAccessibilityRow(this.DataTable, string.IsNullOrEmpty(this.DataTable.Caption) ? "<span style='display: none'>empty</span>" : this.DataTable.Caption);
			}
			foreach (PivotGridModelRow item in this.RowHeaderModel.Rows)
			{
				PivotGridRowHeaderItem pivotGridRowHeaderItem = new PivotGridRowHeaderItem(this, PivotGridItemType.RowHeader, dataBinding);
				if (this.ClientSettings.Scrolling.AllowVerticalScroll)
				{
					this.RowHeaderTable.Rows.Add(pivotGridRowHeaderItem);
				}
				else
				{
					this.OuterTable.Rows.Add(pivotGridRowHeaderItem);
				}
				pivotGridRowHeaderItem.Initialize(item);
			}
		}

		// Token: 0x060087B9 RID: 34745 RVA: 0x001EEDA0 File Offset: 0x001ECFA0
		private void AddColumnHeadersItems(bool dataBinding)
		{
			if (dataBinding && this.ColumnHeadersModel.Rows.Count == 1)
			{
				for (int i = 0; i < this.ColumnHeadersModel.Rows[0].Cells.Count; i++)
				{
					PivotGridModelCell pivotGridModelCell = this.ColumnHeadersModel.Rows[0].Cells[i] as PivotGridModelCell;
					pivotGridModelCell.RowSpan = 1;
				}
			}
			this.columnHeaderItemsCreatedCount = 0;
			this.resizableHeaderCells.Clear();
			this.resizeableHeaderCellsList.Clear();
			foreach (PivotGridModelRow item in this.ColumnHeadersModel.Rows)
			{
				PivotGridColumnHeaderItem pivotGridColumnHeaderItem = new PivotGridColumnHeaderItem(this, PivotGridItemType.ColumnHeader, dataBinding);
				if (!this.ClientSettings.Scrolling.AllowVerticalScroll)
				{
					this.dataTable.Rows.Add(pivotGridColumnHeaderItem);
					pivotGridColumnHeaderItem.TableSection = TableRowSection.TableHeader;
				}
				else
				{
					this.columnHeaderTable.Rows.Add(pivotGridColumnHeaderItem);
				}
				pivotGridColumnHeaderItem.Initialize(item);
				this.columnHeaderItemsCreatedCount++;
			}
		}

		// Token: 0x060087BA RID: 34746 RVA: 0x001EEED4 File Offset: 0x001ED0D4
		private void CollapseRowGroups()
		{
			int count = this.CollapsedRowIndexes.Count;
			foreach (IGroup group in this.PivotModel.RowGroups)
			{
				this.CollapseRowGroup(group, count);
			}
		}

		// Token: 0x060087BB RID: 34747 RVA: 0x001EEF34 File Offset: 0x001ED134
		private void CollapseColumnGroups()
		{
			int count = this.CollapsedColumnIndexes.Count;
			foreach (IGroup group in this.PivotModel.ColumnGroups)
			{
				this.CollapseColumnGroup(group, count);
			}
		}

		// Token: 0x060087BC RID: 34748 RVA: 0x001EEF94 File Offset: 0x001ED194
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void CollapseRowGroup(IGroup group, int collapsedRowIndexesCount)
		{
			if (this.CollapsedRowIndexes.Contains(group.GetGroupIndex()))
			{
				if (this.RowGroupsDefaultExpanded)
				{
					this.rowLayout.Collapse(group);
				}
				else
				{
					this.rowLayout.Expand(group);
				}
				collapsedRowIndexesCount--;
			}
			else if (!this.RowGroupsDefaultExpanded)
			{
				this.rowLayout.Collapse(group);
			}
			foreach (IGroup group2 in group.Groups)
			{
				this.CollapseRowGroup(group2, collapsedRowIndexesCount);
			}
		}

		// Token: 0x060087BD RID: 34749 RVA: 0x001EF034 File Offset: 0x001ED234
		private void CollapseColumnGroup(IGroup group, int collapsedRowIndexesCount)
		{
			if (this.CollapsedColumnIndexes.Contains(group.GetGroupIndex()))
			{
				if (this.ColumnGroupsDefaultExpanded)
				{
					this.columnLayout.Collapse(group);
				}
				else
				{
					this.columnLayout.Expand(group);
				}
			}
			else if (!this.ColumnGroupsDefaultExpanded)
			{
				this.ColumnLayout.Collapse(group);
			}
			foreach (IGroup group2 in group.Groups)
			{
				this.CollapseColumnGroup(group2, collapsedRowIndexesCount);
			}
		}

		// Token: 0x060087BE RID: 34750 RVA: 0x001EF0F4 File Offset: 0x001ED2F4
		private void CollapseAllGroupsInLayout(BaseLayout layout, HashSet<int> levels, HashSet<Array> indexes)
		{
			HashSet<int> hashSet = new HashSet<int>();
			int num = 0;
			for (;;)
			{
				IEnumerable<ItemInfo> enumerable = layout.GetLines(num, true).FirstOrDefault<IList<ItemInfo>>();
				if (enumerable == null)
				{
					break;
				}
				enumerable = from g in enumerable
				where levels.Contains(g.LayoutInfo.Level)
				select g;
				foreach (ItemInfo itemInfo in enumerable)
				{
					IGroup group = itemInfo.Item as IGroup;
					indexes.Add(group.GetGroupIndex());
					hashSet.Add(group.Level);
				}
				num++;
			}
			foreach (int item in hashSet)
			{
				levels.Remove(item);
			}
		}

		// Token: 0x060087BF RID: 34751 RVA: 0x001EF220 File Offset: 0x001ED420
		private void AddRemoveCollapsedGroupIndexBySlot(PivotGridGroupSlot groupSlot, BaseLayout layout, HashSet<Array> indexes, bool defaultExpanded)
		{
			int slot = groupSlot.Slot;
			IList<ItemInfo> source = layout.GetLines(slot, true).First<IList<ItemInfo>>();
			IEnumerable<ItemInfo> source2 = from g in source
			where g.LayoutInfo.Level == groupSlot.Level
			select g;
			ItemInfo itemInfo;
			if (source2.Count<ItemInfo>() > 0)
			{
				itemInfo = source2.First<ItemInfo>();
			}
			else
			{
				itemInfo = source.First<ItemInfo>();
			}
			IGroup group = itemInfo.Item as IGroup;
			object[] groupIndex = group.GetGroupIndex();
			if (indexes.Contains(groupIndex))
			{
				indexes.Remove(groupIndex);
				if (defaultExpanded)
				{
					layout.Expand(group);
					return;
				}
				layout.Collapse(group);
				return;
			}
			else
			{
				indexes.Add(groupIndex);
				if (defaultExpanded)
				{
					layout.Collapse(group);
					return;
				}
				layout.Expand(group);
				return;
			}
		}

		// Token: 0x060087C0 RID: 34752 RVA: 0x001EF2E4 File Offset: 0x001ED4E4
		private void HandleFirstCollapse(PivotGridGroupSlot groupSlot, HashSet<PivotGridGroupSlot> alreadyExpandedSlots, HashSet<Array> indexes, BaseLayout layout)
		{
			if (!alreadyExpandedSlots.Contains(groupSlot))
			{
				alreadyExpandedSlots.Add(groupSlot);
				int num = groupSlot.Slot;
				for (;;)
				{
					num++;
					if (num >= layout.VisibleLineCount)
					{
						break;
					}
					IEnumerable<ItemInfo> enumerable = layout.GetLines(num, true).First<IList<ItemInfo>>();
					using (IEnumerator<ItemInfo> enumerator = enumerable.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ItemInfo itemInfo = enumerator.Current;
							IGroup group = itemInfo.Item as IGroup;
							if (itemInfo.Level > groupSlot.Level)
							{
								indexes.Add(group.GetGroupIndex());
								this.rowLayout.Collapse(group);
							}
						}
						continue;
					}
					return;
				}
				return;
			}
		}

		// Token: 0x060087C1 RID: 34753 RVA: 0x001EF398 File Offset: 0x001ED598
		internal void CreateColumnHeaderItems(bool dataBinding)
		{
			if (!this.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				PivotGridZone zoneByType = this.GetZoneByType(PivotGridZoneType.Data);
				zoneByType.CssClass = "rpgContentZone";
				Panel panel = new Panel();
				zoneByType.Controls.Add(panel);
				panel.CssClass = "rpgContentZoneDiv";
				panel.ID = "ContentZoneDiv";
				panel.Controls.Add(this.DataTable);
			}
			else
			{
				this.columnHeaderTable = new PivotGridColumnHeaderTable(this);
				Panel panel2 = new Panel();
				PivotGridZone zoneByType2 = this.GetZoneByType(PivotGridZoneType.ColumnHeader);
				zoneByType2.Controls.Add(panel2);
				panel2.Controls.Add(this.columnHeaderTable);
				AccessibilityHelper.AddAccessibilityRow(this.columnHeaderTable, string.IsNullOrEmpty(this.columnHeaderTable.Caption) ? "<span style='display: none'>empty</span>" : this.columnHeaderTable.Caption);
				panel2.CssClass = "rpgColumnHeaderDiv";
			}
			this.AddColumnHeadersItems(dataBinding);
		}

		// Token: 0x060087C2 RID: 34754 RVA: 0x001EF480 File Offset: 0x001ED680
		internal void CreateDataItems(bool dataBinding)
		{
			int num = 0;
			bool flag = false;
			if (this.DataModel.Rows.Count > 1)
			{
				flag = true;
			}
			else if (this.DataModel.Rows.Count == 1 && this.DataModel.Rows[0] != null)
			{
				for (int i = 0; i < this.DataModel.Rows[0].Cells.Count; i++)
				{
					if (this.DataModel.Rows[0].Cells[i].Name != null)
					{
						flag = true;
					}
				}
			}
			if (flag || !this.EnableNoRecordsTemplate)
			{
				using (List<PivotGridModelDataRow>.Enumerator enumerator = this.DataModel.Rows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PivotGridModelDataRow pivotGridModelDataRow = enumerator.Current;
						PivotGridDataItem pivotGridDataItem = new PivotGridDataItem(this, pivotGridModelDataRow.DisplayIndex, dataBinding);
						this.dataTable.Rows.Add(pivotGridDataItem);
						pivotGridDataItem.Initialize(pivotGridModelDataRow);
						num++;
					}
					return;
				}
			}
			PivotGridNoRecordsItem pivotGridNoRecordsItem = new PivotGridNoRecordsItem(this, PivotGridItemType.NoRecordsTemplateItem, false);
			this.dataTable.Rows.Add(pivotGridNoRecordsItem);
			pivotGridNoRecordsItem.Initialize();
		}

		// Token: 0x060087C3 RID: 34755 RVA: 0x001EF5BC File Offset: 0x001ED7BC
		private void ClearTables()
		{
			this.outerTable = null;
			this.rowHeaderTable = null;
			this.dataTable = null;
		}

		// Token: 0x060087C4 RID: 34756 RVA: 0x001EF5D4 File Offset: 0x001ED7D4
		internal void SetViewModel(IEnumerable dataSource, bool dataBinding)
		{
			if (!this.EnableCachingInternal || (this.EnableCachingInternal && (this.PivotModel == null || this.shouldAddNewSettings)))
			{
				this.pivotModel = new PivotViewModel();
				this.SetTotalsPosition();
				this.SyncLocalizationProviders(this.pivotModel);
				this.provider = null;
				if (this.IsBoundToXmla)
				{
					XmlaConnectionSettings xmlaConnectionSettings = new XmlaConnectionSettings();
					xmlaConnectionSettings.Cube = this.OlapSettings.XmlaConnectionSettings.Cube;
					xmlaConnectionSettings.Database = this.OlapSettings.XmlaConnectionSettings.DataBase;
					xmlaConnectionSettings.ServerAddress = this.OlapSettings.XmlaConnectionSettings.ServerAddress;
					xmlaConnectionSettings.Credentials = this.OlapSettings.XmlaConnectionSettings.Credentials.ToCoreXmlaNetworkCredentials();
					this.provider = new XmlaDataProvider
					{
						ConnectionSettings = xmlaConnectionSettings,
						SetConditionListCapacity = this.OlapSettings.SetConditionListCapacity
					};
					XmlaFieldDescriptionProvider xmlaFieldDescriptionProvider = new XmlaFieldDescriptionProvider(xmlaConnectionSettings);
					if (this.ConfigurationPanelSettings.FlattenOlapUncategoriezedFields || !string.IsNullOrEmpty(this.ConfigurationPanelSettings.OlapUncategorizedFolderName))
					{
						xmlaFieldDescriptionProvider.GetDescriptionsDataAsyncCompleted += this.fieldDescriptionProvider_GetDescriptionsDataAsyncCompleted;
					}
					else
					{
						xmlaFieldDescriptionProvider.GetDescriptionsDataAsyncCompleted += this.fieldDescriptionProvider_GetDescriptionsDataAsyncCompletedFireEventOnly;
					}
					(this.provider as DataProviderBase).FieldDescriptionsProvider = xmlaFieldDescriptionProvider;
				}
				else if (this.IsBoundToAdomd)
				{
					AdomdConnectionSettings connectionSettings = default(AdomdConnectionSettings);
					connectionSettings.ConnectionString = this.OlapSettings.AdomdConnectionSettings.ConnectionString;
					connectionSettings.Database = this.OlapSettings.AdomdConnectionSettings.DataBase;
					connectionSettings.Cube = this.OlapSettings.AdomdConnectionSettings.Cube;
					this.provider = new AdomdDataProvider
					{
						ConnectionSettings = connectionSettings,
						SetConditionListCapacity = this.OlapSettings.SetConditionListCapacity
					};
					AdomdFieldDescriptionProvider adomdFieldDescriptionProvider = new AdomdFieldDescriptionProvider(connectionSettings);
					if (this.ConfigurationPanelSettings.FlattenOlapUncategoriezedFields || !string.IsNullOrEmpty(this.ConfigurationPanelSettings.OlapUncategorizedFolderName))
					{
						adomdFieldDescriptionProvider.GetDescriptionsDataAsyncCompleted += this.fieldDescriptionProvider_GetDescriptionsDataAsyncCompleted;
					}
					else
					{
						adomdFieldDescriptionProvider.GetDescriptionsDataAsyncCompleted += this.fieldDescriptionProvider_GetDescriptionsDataAsyncCompletedFireEventOnly;
					}
					(this.provider as DataProviderBase).FieldDescriptionsProvider = adomdFieldDescriptionProvider;
				}
				else if (this.IsBoundToIQueryableCollection)
				{
					this.provider = new QueryableDataProvider
					{
						Source = (IQueryable)this.DataSource
					};
					(this.provider as DataProviderBase).FieldDescriptionsProvider = new QueryableFieldDescriptionsProvider();
				}
				else
				{
					this.provider = new LocalDataSourceProvider();
					(this.provider as DataProviderBase).FieldDescriptionsProvider = new LocalDataSourceFieldDescriptionsProvider();
					foreach (PivotGridField pivotGridField in this.Fields)
					{
						PivotGridAggregateField pivotGridAggregateField = pivotGridField as PivotGridAggregateField;
						if (pivotGridAggregateField != null && pivotGridAggregateField.CalculationDataFields.Length > 0)
						{
							RadPivotGrid.GeneralCalculatedField item = new RadPivotGrid.GeneralCalculatedField(this, pivotGridAggregateField.DataField, pivotGridAggregateField.CalculationExpression, pivotGridAggregateField.CalculationDataFields, pivotGridAggregateField.CalculationAggregates);
							(this.provider as LocalDataSourceProvider).CalculatedFields.Add(item);
						}
					}
				}
				this.provider.AggregatesLevel = this.AggregatesLevel;
				this.provider.AggregatesPosition = (PivotAxis)Enum.Parse(typeof(PivotAxis), this.AggregatesPosition.ToString());
				this.PivotModel.ErrorValue = this.ErrorValue;
				this.PivotModel.EmptyValue = this.EmptyValue;
				this.CreateAllFields();
				this.ApplyFilters();
				this.PivotModel.DataProvider = this.provider;
				this.provider.StatusChanged += this.provider_StatusChanged;
				DataProviderBase dataProviderBase = this.provider as DataProviderBase;
				if (dataProviderBase != null)
				{
					dataProviderBase.PropertyChanged += this.providerAsDataProviderBase_PropertyChanged;
				}
				if (dataBinding)
				{
					LocalDataSourceProvider localDataSourceProvider = this.provider as LocalDataSourceProvider;
					QueryableDataProvider queryableDataProvider = this.provider as QueryableDataProvider;
					AdomdDataProvider adomdDataProvider = this.provider as AdomdDataProvider;
					XmlaDataProvider xmlaDataProvider = this.provider as XmlaDataProvider;
					if (xmlaDataProvider != null)
					{
						xmlaDataProvider.Refresh();
					}
					else if (adomdDataProvider != null)
					{
						adomdDataProvider.Refresh();
					}
					else if (localDataSourceProvider != null)
					{
						localDataSourceProvider.ItemsSource = dataSource;
						localDataSourceProvider.BlockUntilRefreshCompletes();
					}
					else if (queryableDataProvider != null)
					{
						queryableDataProvider.BlockUntilRefreshCompletes();
					}
				}
				if (this.EnableCachingInternal)
				{
					this.ModelPersister.SavePivotModel(this.pivotModel, this.UniqueID);
				}
			}
			this.SetRowLayout();
			this.SetColumnLayout();
			this.SetLayoutsSource();
			this.RowLayoutLevelsCount = this.RowLayout.GroupLevels;
		}

		// Token: 0x060087C5 RID: 34757 RVA: 0x001EFA6C File Offset: 0x001EDC6C
		private void SyncLocalizationProviders(PivotViewModel pivotModel)
		{
			pivotModel.GrandTotalText = this.TotalsSettings.GrandTotalText;
			pivotModel.GrandTotalGroupNameFormat = this.TotalsSettings.TotalValueFormat;
			pivotModel.SubTotalGroupNameFormat = this.TotalsSettings.ValueTotalFormat;
			pivotModel.ErrorValue = this.Localization.ErrorValueText;
		}

		// Token: 0x060087C6 RID: 34758 RVA: 0x001EFABD File Offset: 0x001EDCBD
		private void fieldDescriptionProvider_GetDescriptionsDataAsyncCompletedFireEventOnly(object sender, GetDescriptionsDataCompletedEventArgs e)
		{
			this.CallGetDescriptionsDataCompleted(e);
		}

		// Token: 0x060087C7 RID: 34759 RVA: 0x001EFAD8 File Offset: 0x001EDCD8
		private void fieldDescriptionProvider_GetDescriptionsDataAsyncCompleted(object sender, GetDescriptionsDataCompletedEventArgs e)
		{
			this.CallGetDescriptionsDataCompleted(e);
			ContainerNode rootFieldInfo = e.DescriptionsData.RootFieldInfo;
			if (rootFieldInfo.HasChildren)
			{
				foreach (ContainerNode containerNode in rootFieldInfo.Children)
				{
					if (containerNode.HasChildren)
					{
						ContainerNode containerNode2 = containerNode.Children.FirstOrDefault((ContainerNode c) => c.Name == "More fields");
						if (containerNode2 != null)
						{
							if (this.ConfigurationPanelSettings.FlattenOlapUncategoriezedFields)
							{
								this.MoveChildren(containerNode, containerNode2);
							}
							else if (!string.IsNullOrEmpty(this.ConfigurationPanelSettings.OlapUncategorizedFolderName))
							{
								ContainerNode containerNode3 = new ContainerNode(this.ConfigurationPanelSettings.OlapUncategorizedFolderName, containerNode2.Role);
								this.MoveChildren(containerNode3, containerNode2);
								containerNode.Children.Add(containerNode3);
							}
							containerNode.Children.Remove(containerNode2);
						}
					}
				}
			}
		}

		// Token: 0x060087C8 RID: 34760 RVA: 0x001EFBE0 File Offset: 0x001EDDE0
		private void MoveChildren(ContainerNode topLevelItem, ContainerNode moreFields)
		{
			int count = moreFields.Children.Count;
			for (int i = 0; i < count; i++)
			{
				ContainerNode item = moreFields.Children[0];
				moreFields.Children.Remove(item);
				topLevelItem.Children.Add(item);
			}
		}

		// Token: 0x060087C9 RID: 34761 RVA: 0x001EFC2B File Offset: 0x001EDE2B
		internal void providerAsDataProviderBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "FieldDescriptionsProvider")
			{
				(sender as DataProviderBase).FieldDescriptionsProvider.GetDescriptionsDataAsyncCompleted += this.FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted;
			}
		}

		// Token: 0x060087CA RID: 34762 RVA: 0x001EFC5C File Offset: 0x001EDE5C
		internal void FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted(object sender, GetDescriptionsDataCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				PivotGridDataProviderErrorEventArgs e2 = new PivotGridDataProviderErrorEventArgs(e.Error);
				this.CallDataProviderError(e2);
			}
		}

		// Token: 0x060087CB RID: 34763 RVA: 0x001EFC84 File Offset: 0x001EDE84
		internal void provider_StatusChanged(object sender, DataProviderStatusChangedEventArgs e)
		{
			if (e.Error != null)
			{
				PivotGridDataProviderErrorEventArgs e2 = new PivotGridDataProviderErrorEventArgs(e.Error);
				this.CallDataProviderError(e2);
			}
			this.CallDataProviderStatusChanged(e);
		}

		// Token: 0x060087CC RID: 34764 RVA: 0x001EFCB4 File Offset: 0x001EDEB4
		private void CreateAllFields()
		{
			this.Fields.AddMissingHiddenFieldsFromDataSource();
			this.CreateFields<PivotGridReportFilterField>(this.Fields, this.provider.Settings.FilterDescriptions);
			this.CreateFields<PivotGridRowField>(this.Fields, this.provider.Settings.RowGroupDescriptions);
			this.CreateFields<PivotGridColumnField>(this.Fields, this.provider.Settings.ColumnGroupDescriptions);
			this.CreateFields<PivotGridAggregateField>(this.Fields, this.provider.Settings.AggregateDescriptions);
		}

		// Token: 0x060087CD RID: 34765 RVA: 0x001EFD5C File Offset: 0x001EDF5C
		private void SetTotalsPosition()
		{
			this.pivotModel.RowsSubTotalsPosition = (TotalsPosition)Enum.Parse(typeof(TotalsPosition), this.TotalsSettings.RowsSubTotalsPosition.ToString());
			this.pivotModel.RowGrandTotalsPosition = (TotalsPosition)Enum.Parse(typeof(TotalsPosition), this.TotalsSettings.RowGrandTotalsPosition.ToString());
			this.pivotModel.ColumnsSubTotalsPosition = (TotalsPosition)Enum.Parse(typeof(TotalsPosition), this.TotalsSettings.ColumnsSubTotalsPosition.ToString());
			this.pivotModel.ColumnGrandTotalsPosition = (TotalsPosition)Enum.Parse(typeof(TotalsPosition), this.TotalsSettings.ColumnGrandTotalsPosition.ToString());
			if (this.RowTableLayout == PivotGridLayout.Tabular && this.TotalsSettings.RowsSubTotalsPosition == TotalsPosition.First)
			{
				this.pivotModel.RowsSubTotalsPosition = TotalsPosition.Last;
			}
			if (this.TotalsSettings.ColumnsSubTotalsPosition == TotalsPosition.First)
			{
				this.pivotModel.ColumnsSubTotalsPosition = TotalsPosition.Last;
			}
			bool flag = this.RowTableLayout != PivotGridLayout.Tabular && this.TotalsSettings.RowsSubTotalsPosition == TotalsPosition.First;
			IOrderedEnumerable<PivotGridField> source = from f in this.Fields
			where f is PivotGridAggregateField && !f.IsHidden
			orderby f.ZoneIndex
			select f;
			if ((flag && source.Count<PivotGridField>() < 2) || (flag && this.AggregatesPosition == PivotGridAxis.Columns))
			{
				this.pivotModel.RowsSubTotalsPosition = TotalsPosition.Inline;
			}
		}

		// Token: 0x060087CE RID: 34766 RVA: 0x001EFF1C File Offset: 0x001EE11C
		private void CreateFields<T>(IEnumerable<PivotGridField> fields, IList descriptions)
		{
			fields = from field in fields
			where field is T && !field.IsHidden
			select field into f
			orderby f.ZoneIndex
			select f;
			bool hasParentWithNoDataEnabled = false;
			int num = 0;
			foreach (PivotGridField field2 in fields)
			{
				hasParentWithNoDataEnabled = this.SetFieldProperties(field2, num, hasParentWithNoDataEnabled);
				if (this.IsDataBinding)
				{
					PivotGridAddingFieldToZoneEventArgs e = new PivotGridAddingFieldToZoneEventArgs(field2);
					this.FireAddingFieldToZone(e);
				}
				this.CallInsertGroupDescription(field2, num, descriptions);
				num++;
			}
		}

		// Token: 0x060087CF RID: 34767 RVA: 0x001EFFB8 File Offset: 0x001EE1B8
		private bool SetFieldProperties(PivotGridField field, int index, bool hasParentWithNoDataEnabled)
		{
			PivotGridGroupField pivotGridGroupField = field as PivotGridGroupField;
			PivotGridReportFilterField pivotGridReportFilterField = field as PivotGridReportFilterField;
			if (pivotGridGroupField != null)
			{
				if (pivotGridGroupField.ShowGroupsWhenNoData)
				{
					hasParentWithNoDataEnabled = true;
				}
				else if (hasParentWithNoDataEnabled)
				{
					pivotGridGroupField.ShowGroupsWhenNoData = true;
				}
			}
			else if (pivotGridReportFilterField != null)
			{
				if (this.IsBoundToAdomd)
				{
					AdomdFilterDescription adomdFilterDescription = new AdomdFilterDescription();
					pivotGridReportFilterField.FilterDescription = adomdFilterDescription;
					adomdFilterDescription.MemberName = pivotGridReportFilterField.DataField;
				}
				else if (this.IsBoundToXmla)
				{
					XmlaFilterDescription xmlaFilterDescription = new XmlaFilterDescription();
					pivotGridReportFilterField.FilterDescription = xmlaFilterDescription;
					xmlaFilterDescription.MemberName = pivotGridReportFilterField.DataField;
				}
				else
				{
					PropertyFilterDescription propertyFilterDescription = new PropertyFilterDescription();
					propertyFilterDescription.PropertyName = field.DataField;
					pivotGridReportFilterField.FilterDescriptionIndex = index;
					pivotGridReportFilterField.FilterDescription = propertyFilterDescription;
				}
			}
			return hasParentWithNoDataEnabled;
		}

		// Token: 0x060087D0 RID: 34768 RVA: 0x001F005C File Offset: 0x001EE25C
		private void CallInsertGroupDescription(PivotGridField field, int index, IList descriptions)
		{
			PivotGridGroupField pivotGridGroupField = field as PivotGridGroupField;
			PivotGridAggregateField pivotGridAggregateField = field as PivotGridAggregateField;
			PivotGridReportFilterField pivotGridReportFilterField = field as PivotGridReportFilterField;
			object value = null;
			if (pivotGridGroupField != null)
			{
				foreach (object obj in pivotGridGroupField.CalculatedItems)
				{
					PivotGridCalculatedItem pivotGridCalculatedItem = (PivotGridCalculatedItem)obj;
					RadPivotGrid.GeneralCalculatedItem item = new RadPivotGrid.GeneralCalculatedItem(this, pivotGridCalculatedItem.GroupName, pivotGridCalculatedItem.SolveOrder);
					(pivotGridGroupField.GroupDescription as PropertyGroupDescription).CalculatedItems.Add(item);
				}
				value = pivotGridGroupField.GroupDescription;
			}
			else if (pivotGridAggregateField != null)
			{
				value = pivotGridAggregateField.GroupDescription;
			}
			else if (pivotGridReportFilterField != null)
			{
				value = pivotGridReportFilterField.FilterDescription;
			}
			descriptions.Insert(index, value);
			field.DescriptorIndex = index;
		}

		// Token: 0x060087D1 RID: 34769 RVA: 0x001F012C File Offset: 0x001EE32C
		private void SetRowLayout()
		{
			switch (this.RowTableLayout)
			{
			case PivotGridLayout.Tabular:
				this.rowLayout = new TabularLayout(new GroupHierarchyAdapter());
				return;
			case PivotGridLayout.Outline:
				this.rowLayout = new OutlineLayout(new GroupHierarchyAdapter());
				return;
			case PivotGridLayout.Compact:
				this.rowLayout = new CompactLayout(new GroupHierarchyAdapter());
				return;
			default:
				return;
			}
		}

		// Token: 0x060087D2 RID: 34770 RVA: 0x001F0185 File Offset: 0x001EE385
		private void SetColumnLayout()
		{
			this.columnLayout = new TabularLayout(new GroupHierarchyAdapter());
		}

		// Token: 0x060087D3 RID: 34771 RVA: 0x001F0198 File Offset: 0x001EE398
		private void SetLayoutsSource()
		{
			if (this.PivotModel.DataProvider.AggregatesPosition == PivotAxis.Rows)
			{
				this.rowLayout.SetSource(this.PivotModel.RowGroups, this.PivotModel.RowLevels, this.PivotModel.RowsSubTotalsPosition, this.PivotModel.DataProvider.AggregatesLevel, this.PivotModel.AggregateDescriptionCount, this.PivotModel.ShowSubTotalAggregatesInline);
				this.columnLayout.SetSource(this.PivotModel.ColumnGroups, this.PivotModel.ColumnLevels, this.PivotModel.ColumnsSubTotalsPosition, 0, 1, this.PivotModel.ShowSubTotalAggregatesInline);
				return;
			}
			this.rowLayout.SetSource(this.PivotModel.RowGroups, this.PivotModel.RowLevels, this.PivotModel.RowsSubTotalsPosition, 0, 1, this.PivotModel.ShowSubTotalAggregatesInline);
			this.columnLayout.SetSource(this.PivotModel.ColumnGroups, this.PivotModel.ColumnLevels, this.PivotModel.ColumnsSubTotalsPosition, this.PivotModel.DataProvider.AggregatesLevel, this.PivotModel.AggregateDescriptionCount, this.PivotModel.ShowSubTotalAggregatesInline);
		}

		// Token: 0x060087D4 RID: 34772 RVA: 0x001F02D4 File Offset: 0x001EE4D4
		private void ApplyFilters()
		{
			foreach (PivotGridFilter pivotGridFilter in this.Filters)
			{
				PivotGridField fieldByUniqueName = this.Fields.GetFieldByUniqueName(pivotGridFilter.FieldName);
				if (pivotGridFilter is PivotGridReportFilter)
				{
					PivotGridReportFilterField pivotGridReportFilterField = fieldByUniqueName as PivotGridReportFilterField;
					if (pivotGridReportFilterField != null)
					{
						IReportFilterDescription reportFilterDescription = pivotGridReportFilterField.FilterDescription as IReportFilterDescription;
						reportFilterDescription.Condition = (pivotGridFilter as PivotGridReportFilter).Condition.GetDataEngineFilterCondition();
					}
				}
				else
				{
					PivotGridGroupField pivotGridGroupField = fieldByUniqueName as PivotGridGroupField;
					if (pivotGridGroupField != null)
					{
						OlapGroupDescription olapGroupDescription = pivotGridGroupField.GroupDescription as OlapGroupDescription;
						if (olapGroupDescription != null && olapGroupDescription.Levels.Count > 0 && pivotGridGroupField.FlatChildOlapInfoNames.Count > 0)
						{
							olapGroupDescription.Levels[0].GroupFilter = pivotGridFilter.GetDataEngineFilter();
						}
						else
						{
							pivotGridGroupField.GroupDescription.GroupFilter = pivotGridFilter.GetDataEngineFilter();
						}
					}
				}
			}
		}

		// Token: 0x060087D5 RID: 34773 RVA: 0x001F03D8 File Offset: 0x001EE5D8
		internal void ParseOlapConnectionString(string ocs)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
			dbConnectionStringBuilder.ConnectionString = ocs;
			string a = dbConnectionStringBuilder.ContainsKey("olapprovider") ? dbConnectionStringBuilder["olapprovider"].ToString().ToLowerInvariant() : "adomd";
			if (a == "adomd")
			{
				this.ParseAdomdOlapConnectionString(dbConnectionStringBuilder, ocs);
				return;
			}
			if (a == "xmla")
			{
				this.ParseXmlaOlapConnectionString(dbConnectionStringBuilder, ocs);
				return;
			}
			throw new ArgumentException("OlapProvider", "Supported values for the OlapProvider parameters are 'ADOMD' and 'XMLA'!");
		}

		// Token: 0x060087D6 RID: 34774 RVA: 0x001F0460 File Offset: 0x001EE660
		private void ParseXmlaOlapConnectionString(DbConnectionStringBuilder connStrBuilder, string ocs)
		{
			string text = this.ParseFirstParameterByAliasList(connStrBuilder, new string[]
			{
				"data source",
				"datasource"
			});
			string text2 = this.ParseFirstParameterByAliasList(connStrBuilder, new string[]
			{
				"initial catalog",
				"catalog"
			});
			this.OlapSettings.ProviderType = PivotGridOlapProviderType.Xmla;
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("Data Source", "Data Source parameter must be set in the ADOMD connection string.");
			}
			this.OlapSettings.XmlaConnectionSettings.ServerAddress = text;
			if (connStrBuilder.ContainsKey("cube"))
			{
				this.OlapSettings.XmlaConnectionSettings.Cube = connStrBuilder["cube"].ToString();
			}
			if (!string.IsNullOrEmpty(text2))
			{
				this.OlapSettings.XmlaConnectionSettings.DataBase = text2;
			}
			if (connStrBuilder.ContainsKey("character encoding"))
			{
				string a = connStrBuilder["character encoding"].ToString().ToLowerInvariant();
				if (a == "utf-16")
				{
					this.OlapSettings.XmlaConnectionSettings.Encoding = Encoding.Unicode;
				}
				else if (a == "utf-8" || a == "default")
				{
					this.OlapSettings.XmlaConnectionSettings.Encoding = Encoding.UTF8;
				}
			}
			string text3 = this.ParseFirstParameterByAliasList(connStrBuilder, new string[]
			{
				"user id",
				"username",
				"uid"
			});
			if (!string.IsNullOrEmpty(text3))
			{
				string text4 = this.ParseFirstParameterByAliasList(connStrBuilder, new string[]
				{
					"password",
					"pwd"
				});
				this.OlapSettings.XmlaConnectionSettings.Credentials.UserName = text3;
				if (!string.IsNullOrEmpty(text3))
				{
					if (text3.Count((char str) => str == '\\') == 1)
					{
						string[] array = text3.Split(new char[]
						{
							'\\'
						}, StringSplitOptions.RemoveEmptyEntries);
						this.OlapSettings.XmlaConnectionSettings.Credentials.Domain = array[0];
						this.OlapSettings.XmlaConnectionSettings.Credentials.UserName = array[1];
					}
					else
					{
						this.OlapSettings.XmlaConnectionSettings.Credentials.UserName = text3;
					}
					if (!string.IsNullOrEmpty(text4))
					{
						this.OlapSettings.XmlaConnectionSettings.Credentials.PassWord = text4;
					}
				}
			}
		}

		// Token: 0x060087D7 RID: 34775 RVA: 0x001F06D0 File Offset: 0x001EE8D0
		private void ParseAdomdOlapConnectionString(DbConnectionStringBuilder connStrBuilder, string ocs)
		{
			string text = this.ParseFirstParameterByAliasList(connStrBuilder, new string[]
			{
				"data source",
				"datasource"
			});
			string text2 = this.ParseFirstParameterByAliasList(connStrBuilder, new string[]
			{
				"initial catalog",
				"catalog"
			});
			this.OlapSettings.ProviderType = PivotGridOlapProviderType.Adomd;
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("Data Source", "Data Source parameter must be set in the ADOMD connection string.");
			}
			this.OlapSettings.AdomdConnectionSettings.ConnectionString = string.Format("Data Source={0};", text);
			if (!string.IsNullOrEmpty(text2))
			{
				PivotGridAdomdConnectionSettings adomdConnectionSettings = this.OlapSettings.AdomdConnectionSettings;
				adomdConnectionSettings.ConnectionString += string.Format("Catalog={0}", text2);
				this.OlapSettings.AdomdConnectionSettings.DataBase = text2;
			}
			if (connStrBuilder.ContainsKey("cube"))
			{
				this.OlapSettings.AdomdConnectionSettings.Cube = connStrBuilder["cube"].ToString();
			}
		}

		// Token: 0x060087D8 RID: 34776 RVA: 0x001F07C8 File Offset: 0x001EE9C8
		private string ParseFirstParameterByAliasList(DbConnectionStringBuilder connStrBuilder, string[] aliases)
		{
			string result = string.Empty;
			foreach (string keyword in aliases)
			{
				if (connStrBuilder.ContainsKey(keyword))
				{
					result = connStrBuilder[keyword].ToString();
					break;
				}
			}
			return result;
		}

		// Token: 0x1400013A RID: 314
		// (add) Token: 0x060087D9 RID: 34777 RVA: 0x001F0808 File Offset: 0x001EEA08
		// (remove) Token: 0x060087DA RID: 34778 RVA: 0x001F081B File Offset: 0x001EEA1B
		public event EventHandler<PivotGridItemCreatedEventArgs> ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventItemCreated, value);
			}
		}

		// Token: 0x060087DB RID: 34779 RVA: 0x001F0830 File Offset: 0x001EEA30
		protected virtual void CallItemCreated(PivotGridItemCreatedEventArgs e)
		{
			EventHandler<PivotGridItemCreatedEventArgs> eventHandler = base.Events[RadPivotGrid.EventItemCreated] as EventHandler<PivotGridItemCreatedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060087DC RID: 34780 RVA: 0x001F0860 File Offset: 0x001EEA60
		internal virtual void CallPrepareDescriptionForField(PivotGridPrepareDescriptionForFieldEventArgs e)
		{
			EventHandler<PivotGridPrepareDescriptionForFieldEventArgs> eventHandler = base.Events[RadPivotGrid.EventPrepareDescriptionForField] as EventHandler<PivotGridPrepareDescriptionForFieldEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060087DD RID: 34781 RVA: 0x001F0890 File Offset: 0x001EEA90
		internal virtual void CallGetDescriptionsDataCompleted(GetDescriptionsDataCompletedEventArgs e)
		{
			EventHandler<GetDescriptionsDataCompletedEventArgs> eventHandler = base.Events[RadPivotGrid.EventGetDescriptionsDataCompleted] as EventHandler<GetDescriptionsDataCompletedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400013B RID: 315
		// (add) Token: 0x060087DE RID: 34782 RVA: 0x001F08BE File Offset: 0x001EEABE
		// (remove) Token: 0x060087DF RID: 34783 RVA: 0x001F08D1 File Offset: 0x001EEAD1
		public event EventHandler<PivotGridDataProviderErrorEventArgs> DataProviderError
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventDataProviderError, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventDataProviderError, value);
			}
		}

		// Token: 0x060087E0 RID: 34784 RVA: 0x001F08E4 File Offset: 0x001EEAE4
		protected virtual void CallDataProviderError(PivotGridDataProviderErrorEventArgs e)
		{
			EventHandler<PivotGridDataProviderErrorEventArgs> eventHandler = base.Events[RadPivotGrid.EventDataProviderError] as EventHandler<PivotGridDataProviderErrorEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400013C RID: 316
		// (add) Token: 0x060087E1 RID: 34785 RVA: 0x001F0912 File Offset: 0x001EEB12
		// (remove) Token: 0x060087E2 RID: 34786 RVA: 0x001F0925 File Offset: 0x001EEB25
		public event EventHandler<DataProviderStatusChangedEventArgs> DataProviderStatusChanged
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventDataProviderStatusChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventDataProviderStatusChanged, value);
			}
		}

		// Token: 0x060087E3 RID: 34787 RVA: 0x001F0938 File Offset: 0x001EEB38
		protected virtual void CallDataProviderStatusChanged(DataProviderStatusChangedEventArgs e)
		{
			EventHandler<DataProviderStatusChangedEventArgs> eventHandler = base.Events[RadPivotGrid.EventDataProviderStatusChanged] as EventHandler<DataProviderStatusChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400013D RID: 317
		// (add) Token: 0x060087E4 RID: 34788 RVA: 0x001F0966 File Offset: 0x001EEB66
		// (remove) Token: 0x060087E5 RID: 34789 RVA: 0x001F0979 File Offset: 0x001EEB79
		public event EventHandler<PivotGridAddingFieldToZoneEventArgs> AddingFieldToZone
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventAddingFieldToZone, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventAddingFieldToZone, value);
			}
		}

		// Token: 0x060087E6 RID: 34790 RVA: 0x001F098C File Offset: 0x001EEB8C
		protected virtual void CallAddingFieldToZone(PivotGridAddingFieldToZoneEventArgs e)
		{
			EventHandler<PivotGridAddingFieldToZoneEventArgs> eventHandler = base.Events[RadPivotGrid.EventAddingFieldToZone] as EventHandler<PivotGridAddingFieldToZoneEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400013E RID: 318
		// (add) Token: 0x060087E7 RID: 34791 RVA: 0x001F09BA File Offset: 0x001EEBBA
		// (remove) Token: 0x060087E8 RID: 34792 RVA: 0x001F09CD File Offset: 0x001EEBCD
		public event EventHandler<PivotGridFieldCreatedEventArgs> FieldCreated
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventFieldCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventFieldCreated, value);
			}
		}

		// Token: 0x060087E9 RID: 34793 RVA: 0x001F09E0 File Offset: 0x001EEBE0
		protected virtual void CallFieldCreated(PivotGridFieldCreatedEventArgs e)
		{
			EventHandler<PivotGridFieldCreatedEventArgs> eventHandler = base.Events[RadPivotGrid.EventFieldCreated] as EventHandler<PivotGridFieldCreatedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060087EA RID: 34794 RVA: 0x001F0A10 File Offset: 0x001EEC10
		protected virtual void CallItemDataBound(PivotGridItemDataBoundEventArgs e)
		{
			EventHandler<PivotGridItemDataBoundEventArgs> eventHandler = base.Events[RadPivotGrid.EventItemDataBound] as EventHandler<PivotGridItemDataBoundEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400013F RID: 319
		// (add) Token: 0x060087EB RID: 34795 RVA: 0x001F0A3E File Offset: 0x001EEC3E
		// (remove) Token: 0x060087EC RID: 34796 RVA: 0x001F0A51 File Offset: 0x001EEC51
		public event EventHandler<PivotGridCellCreatedEventArgs> CellCreated
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventCellCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventCellCreated, value);
			}
		}

		// Token: 0x060087ED RID: 34797 RVA: 0x001F0A64 File Offset: 0x001EEC64
		protected virtual void CallCreated(PivotGridCellCreatedEventArgs e)
		{
			EventHandler<PivotGridCellCreatedEventArgs> eventHandler = base.Events[RadPivotGrid.EventCellCreated] as EventHandler<PivotGridCellCreatedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000140 RID: 320
		// (add) Token: 0x060087EE RID: 34798 RVA: 0x001F0A92 File Offset: 0x001EEC92
		// (remove) Token: 0x060087EF RID: 34799 RVA: 0x001F0AA5 File Offset: 0x001EECA5
		public event EventHandler<PivotGridCellDataBoundEventArgs> CellDataBound
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventCellDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventCellDataBound, value);
			}
		}

		// Token: 0x060087F0 RID: 34800 RVA: 0x001F0AB8 File Offset: 0x001EECB8
		protected virtual void CallDataBound(PivotGridCellDataBoundEventArgs e)
		{
			EventHandler<PivotGridCellDataBoundEventArgs> eventHandler = base.Events[RadPivotGrid.EventCellDataBound] as EventHandler<PivotGridCellDataBoundEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060087F1 RID: 34801 RVA: 0x001F0AE8 File Offset: 0x001EECE8
		protected virtual void CallFieldReorder(PivotGridFieldReorderEventArgs e)
		{
			EventHandler<PivotGridFieldReorderEventArgs> eventHandler = base.Events[RadPivotGrid.EventFieldReorder] as EventHandler<PivotGridFieldReorderEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060087F2 RID: 34802 RVA: 0x001F0B18 File Offset: 0x001EED18
		protected virtual void CallShowHideField(PivotGridShowHideFieldEventArgs e)
		{
			EventHandler<PivotGridShowHideFieldEventArgs> eventHandler = base.Events[RadPivotGrid.EventShowHideField] as EventHandler<PivotGridShowHideFieldEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060087F3 RID: 34803 RVA: 0x001F0B48 File Offset: 0x001EED48
		protected virtual void CallUpdateLayout(PivotGridUpdateLayoutEventArgs e)
		{
			EventHandler<PivotGridUpdateLayoutEventArgs> eventHandler = base.Events["UpdateLayout"] as EventHandler<PivotGridUpdateLayoutEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060087F4 RID: 34804 RVA: 0x001F0B78 File Offset: 0x001EED78
		protected virtual void CallAggregateLabelChange(PivotGridAggregateLabelChangeEventArgs e)
		{
			EventHandler<PivotGridAggregateLabelChangeEventArgs> eventHandler = base.Events["AggregateChange"] as EventHandler<PivotGridAggregateLabelChangeEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060087F5 RID: 34805 RVA: 0x001F0BA8 File Offset: 0x001EEDA8
		protected virtual void CallExpandCollapseLevel(PivotGridExpandCollapseLevelEventArgs e)
		{
			EventHandler<PivotGridExpandCollapseLevelEventArgs> eventHandler = base.Events["ExpandCollapseLevel"] as EventHandler<PivotGridExpandCollapseLevelEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060087F6 RID: 34806 RVA: 0x001F0BD6 File Offset: 0x001EEDD6
		internal void FireAddingFieldToZone(PivotGridAddingFieldToZoneEventArgs e)
		{
			this.CallAddingFieldToZone(e);
		}

		// Token: 0x060087F7 RID: 34807 RVA: 0x001F0BDF File Offset: 0x001EEDDF
		internal void FireFieldCreated(PivotGridFieldCreatedEventArgs e)
		{
			this.CallFieldCreated(e);
		}

		// Token: 0x060087F8 RID: 34808 RVA: 0x001F0BE8 File Offset: 0x001EEDE8
		internal void FireItemCreated(PivotGridItemCreatedEventArgs e)
		{
			this.CallItemCreated(e);
		}

		// Token: 0x060087F9 RID: 34809 RVA: 0x001F0BF1 File Offset: 0x001EEDF1
		internal void FireItemDataBound(PivotGridItemDataBoundEventArgs e)
		{
			this.CallItemDataBound(e);
		}

		// Token: 0x060087FA RID: 34810 RVA: 0x001F0BFA File Offset: 0x001EEDFA
		internal void FireCellCreated(PivotGridCellCreatedEventArgs e)
		{
			this.CallCreated(e);
		}

		// Token: 0x060087FB RID: 34811 RVA: 0x001F0C03 File Offset: 0x001EEE03
		internal void FireCellDataBound(PivotGridCellDataBoundEventArgs e)
		{
			this.CallDataBound(e);
		}

		// Token: 0x060087FC RID: 34812 RVA: 0x001F0C0C File Offset: 0x001EEE0C
		internal void FireFieldReorder(PivotGridFieldReorderEventArgs e)
		{
			this.CallFieldReorder(e);
		}

		// Token: 0x060087FD RID: 34813 RVA: 0x001F0C15 File Offset: 0x001EEE15
		internal void FireShowHideField(PivotGridShowHideFieldEventArgs e)
		{
			this.CallShowHideField(e);
		}

		// Token: 0x060087FE RID: 34814 RVA: 0x001F0C1E File Offset: 0x001EEE1E
		internal void FireUpdateLayout(PivotGridUpdateLayoutEventArgs e)
		{
			this.CallUpdateLayout(e);
		}

		// Token: 0x060087FF RID: 34815 RVA: 0x001F0C27 File Offset: 0x001EEE27
		internal void FireAggregateLabelChange(PivotGridAggregateLabelChangeEventArgs e)
		{
			this.CallAggregateLabelChange(e);
		}

		// Token: 0x06008800 RID: 34816 RVA: 0x001F0C30 File Offset: 0x001EEE30
		internal void FireExpandCollapseLevel(PivotGridExpandCollapseLevelEventArgs e)
		{
			this.CallExpandCollapseLevel(e);
		}

		// Token: 0x14000141 RID: 321
		// (add) Token: 0x06008801 RID: 34817 RVA: 0x001F0C39 File Offset: 0x001EEE39
		// (remove) Token: 0x06008802 RID: 34818 RVA: 0x001F0C4C File Offset: 0x001EEE4C
		[Category("Action")]
		[Description("Fires when \"Page\" command bubbles")]
		public event EventHandler<PivotGridPageChangedEventArgs> PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventPageIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventPageIndexChanged, value);
			}
		}

		// Token: 0x06008803 RID: 34819 RVA: 0x001F0C60 File Offset: 0x001EEE60
		protected virtual void OnPageIndexChanged(PivotGridPageChangedEventArgs e)
		{
			EventHandler<PivotGridPageChangedEventArgs> eventHandler = base.Events[RadPivotGrid.EventPageIndexChanged] as EventHandler<PivotGridPageChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06008804 RID: 34820 RVA: 0x001F0C8E File Offset: 0x001EEE8E
		internal void FirePageIndexChanged(PivotGridPageChangedEventArgs e)
		{
			this.OnPageIndexChanged(e);
		}

		// Token: 0x14000142 RID: 322
		// (add) Token: 0x06008805 RID: 34821 RVA: 0x001F0C97 File Offset: 0x001EEE97
		// (remove) Token: 0x06008806 RID: 34822 RVA: 0x001F0CAA File Offset: 0x001EEEAA
		[Category("Action")]
		[Description("Fires when PageSize has been changed.")]
		public event EventHandler<PivotGridPageSizeChangedEventArgs> PageSizeChanged
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventPageSizeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventPageSizeChanged, value);
			}
		}

		// Token: 0x06008807 RID: 34823 RVA: 0x001F0CC0 File Offset: 0x001EEEC0
		protected virtual void OnPageSizeChanged(PivotGridPageSizeChangedEventArgs e)
		{
			EventHandler<PivotGridPageSizeChangedEventArgs> eventHandler = base.Events[RadPivotGrid.EventPageSizeChanged] as EventHandler<PivotGridPageSizeChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06008808 RID: 34824 RVA: 0x001F0CEE File Offset: 0x001EEEEE
		internal void FirePageSizeChanged(PivotGridPageSizeChangedEventArgs e)
		{
			this.OnPageSizeChanged(e);
		}

		// Token: 0x14000143 RID: 323
		// (add) Token: 0x06008809 RID: 34825 RVA: 0x001F0CF7 File Offset: 0x001EEEF7
		// (remove) Token: 0x0600880A RID: 34826 RVA: 0x001F0D0A File Offset: 0x001EEF0A
		[Description("Raised when a button in a RadPivotGrid control is clicked.")]
		[Category("Action")]
		public event EventHandler<PivotGridCommandEventArgs> ItemCommand
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventItemCommand, value);
			}
		}

		// Token: 0x0600880B RID: 34827 RVA: 0x001F0D20 File Offset: 0x001EEF20
		protected virtual void OnItemCommand(PivotGridCommandEventArgs e)
		{
			EventHandler<PivotGridCommandEventArgs> eventHandler = base.Events[RadPivotGrid.EventItemCommand] as EventHandler<PivotGridCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600880C RID: 34828 RVA: 0x001F0D50 File Offset: 0x001EEF50
		protected virtual void SetStyleClasses()
		{
			string text = string.Format("rpg{0}ConfigurationPanel", this.ConfigurationPanelSettings.Position) + " " + string.Format("rpg{0}ConfigurationPanel", this.ConfigurationPanelSettings.LayoutType);
			this.CssClass = string.Concat(new string[]
			{
				this.FormatCssClass("RadPivotGrid", this.CssClass),
				" rpg",
				this.RowTableLayout.ToString(),
				" ",
				text
			});
			this.CssClass = this.CssClass.Trim();
			this.PagerStyle.CssClass = this.FormatCssClass("rpgPager", this.PagerStyle.CssClass);
		}

		// Token: 0x0600880D RID: 34829 RVA: 0x001F0E1C File Offset: 0x001EF01C
		internal string FormatCssClass(string prefix, string userDefined)
		{
			string text = prefix;
			if (prefix == "RadPivotGrid")
			{
				text = string.Concat(new string[]
				{
					prefix,
					" ",
					prefix,
					"_",
					base.RuntimeSkin
				});
			}
			if (userDefined.IndexOf(text, StringComparison.CurrentCulture) >= 0)
			{
				return userDefined;
			}
			if (string.IsNullOrEmpty(userDefined))
			{
				return text;
			}
			return string.Format(CultureInfo.CurrentCulture, "{0} {1}", new object[]
			{
				text,
				userDefined
			});
		}

		// Token: 0x0600880E RID: 34830 RVA: 0x001F0E9D File Offset: 0x001EF09D
		internal string GetFilterMenuItemText(string gridKnownFunctionText)
		{
			return this.Localization.GetString(string.Format("{0}Text", gridKnownFunctionText));
		}

		// Token: 0x17002B14 RID: 11028
		// (get) Token: 0x0600880F RID: 34831 RVA: 0x001F0EB5 File Offset: 0x001EF0B5
		private bool RowCollapsedCommandInProgress
		{
			get
			{
				return this.rowGroupExpandCollapseSlot != null;
			}
		}

		// Token: 0x17002B15 RID: 11029
		// (get) Token: 0x06008810 RID: 34832 RVA: 0x001F0EC3 File Offset: 0x001EF0C3
		private bool ColumnCollapsedCommandInProgress
		{
			get
			{
				return this.columnGroupExpandCollapseSlot != null;
			}
		}

		// Token: 0x17002B16 RID: 11030
		// (get) Token: 0x06008811 RID: 34833 RVA: 0x001F0ED4 File Offset: 0x001EF0D4
		private bool IsMobile
		{
			get
			{
				return this.Page.Request != null && !string.IsNullOrEmpty(this.Page.Request.UserAgent) && (Regex.IsMatch(this.Page.Request.UserAgent, "like\\sMac\\sOS\\sX.*Mobile\\S+") || Regex.IsMatch(this.Page.Request.UserAgent, "Android.*Safari\\S+") || Regex.IsMatch(this.Page.Request.UserAgent, "BlackBerry.*Safari\\S+") || Regex.IsMatch(this.Page.Request.UserAgent, "Opera (?:Mobi|Tablet)"));
			}
		}

		// Token: 0x17002B17 RID: 11031
		// (get) Token: 0x06008812 RID: 34834 RVA: 0x001F0F7C File Offset: 0x001EF17C
		// (set) Token: 0x06008813 RID: 34835 RVA: 0x001F0FA5 File Offset: 0x001EF1A5
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool RowGroupsDefaultExpanded
		{
			get
			{
				object obj = this.ViewState["RowGroupsDefaultExpanded"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["RowGroupsDefaultExpanded"] = value;
			}
		}

		// Token: 0x17002B18 RID: 11032
		// (get) Token: 0x06008814 RID: 34836 RVA: 0x001F0FC0 File Offset: 0x001EF1C0
		// (set) Token: 0x06008815 RID: 34837 RVA: 0x001F0FE9 File Offset: 0x001EF1E9
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool ColumnGroupsDefaultExpanded
		{
			get
			{
				object obj = this.ViewState["ColumnGroupsDefaultExpanded"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ColumnGroupsDefaultExpanded"] = value;
			}
		}

		// Token: 0x17002B19 RID: 11033
		// (get) Token: 0x06008816 RID: 34838 RVA: 0x001F1004 File Offset: 0x001EF204
		// (set) Token: 0x06008817 RID: 34839 RVA: 0x001F103C File Offset: 0x001EF23C
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool RenderEmptyStringInDataCells
		{
			get
			{
				object obj = this.ViewState["ShowEmptyStringIntoDataCells"];
				return obj != null && (bool)this.ViewState["ShowEmptyStringIntoDataCells"];
			}
			set
			{
				this.ViewState["ShowEmptyStringIntoDataCells"] = value;
			}
		}

		// Token: 0x17002B1A RID: 11034
		// (get) Token: 0x06008818 RID: 34840 RVA: 0x001F1054 File Offset: 0x001EF254
		// (set) Token: 0x06008819 RID: 34841 RVA: 0x001F107D File Offset: 0x001EF27D
		[SimplePersistenceSetting]
		[NotifyParentProperty(true)]
		[DefaultValue(PivotGridAxis.Columns)]
		public PivotGridAxis AggregatesPosition
		{
			get
			{
				object obj = this.ViewState["AggregatesPosition"];
				if (obj != null)
				{
					return (PivotGridAxis)obj;
				}
				return PivotGridAxis.Columns;
			}
			set
			{
				this.ViewState["AggregatesPosition"] = value;
			}
		}

		// Token: 0x17002B1B RID: 11035
		// (get) Token: 0x0600881A RID: 34842 RVA: 0x001F10D8 File Offset: 0x001EF2D8
		// (set) Token: 0x0600881B RID: 34843 RVA: 0x001F1193 File Offset: 0x001EF393
		[SimplePersistenceSetting]
		[NotifyParentProperty(true)]
		[DefaultValue(-1)]
		public int AggregatesLevel
		{
			get
			{
				object obj = this.ViewState["AggregatesLevel"];
				if (obj != null)
				{
					return (int)obj;
				}
				int num = this.Fields.Count((PivotGridField f) => f.ZoneType == PivotGridFieldZoneType.Aggregate && !f.IsHidden);
				if (num < 2)
				{
					return -1;
				}
				if (this.AggregatesPosition == PivotGridAxis.Columns)
				{
					return this.Fields.Count((PivotGridField f) => f.ZoneType == PivotGridFieldZoneType.Column && !f.IsHidden);
				}
				if (this.AggregatesPosition == PivotGridAxis.Rows)
				{
					return this.Fields.Count((PivotGridField f) => f.ZoneType == PivotGridFieldZoneType.Row && !f.IsHidden);
				}
				return 0;
			}
			set
			{
				this.ViewState["AggregatesLevel"] = value;
			}
		}

		// Token: 0x17002B1C RID: 11036
		// (get) Token: 0x0600881C RID: 34844 RVA: 0x001F11AC File Offset: 0x001EF3AC
		// (set) Token: 0x0600881D RID: 34845 RVA: 0x001F11D5 File Offset: 0x001EF3D5
		[NotifyParentProperty(true)]
		[DefaultValue(PivotGridLayout.Tabular)]
		public PivotGridLayout RowTableLayout
		{
			get
			{
				object obj = this.ViewState["RowTableLayout"];
				if (obj != null)
				{
					return (PivotGridLayout)obj;
				}
				return PivotGridLayout.Tabular;
			}
			set
			{
				this.ViewState["RowTableLayout"] = value;
			}
		}

		// Token: 0x17002B1D RID: 11037
		// (get) Token: 0x0600881E RID: 34846 RVA: 0x001F11ED File Offset: 0x001EF3ED
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public PivotGridFieldsPopupSettings FieldsPopupSettings
		{
			get
			{
				if (this.popupSettings == null)
				{
					this.popupSettings = new PivotGridFieldsPopupSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.popupSettings).TrackViewState();
					}
				}
				return this.popupSettings;
			}
		}

		// Token: 0x17002B1E RID: 11038
		// (get) Token: 0x0600881F RID: 34847 RVA: 0x001F121C File Offset: 0x001EF41C
		// (set) Token: 0x06008820 RID: 34848 RVA: 0x001F1263 File Offset: 0x001EF463
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool EnableCaching
		{
			get
			{
				if (this.IsDesignMode)
				{
					object obj = this.ViewState["EnableCaching"];
					return obj != null && (bool)this.ViewState["EnableCaching"];
				}
				return this.EnableCachingInternal;
			}
			set
			{
				this.EnableCachingInternal = value;
			}
		}

		// Token: 0x17002B1F RID: 11039
		// (get) Token: 0x06008821 RID: 34849 RVA: 0x001F126C File Offset: 0x001EF46C
		// (set) Token: 0x06008822 RID: 34850 RVA: 0x001F12AE File Offset: 0x001EF4AE
		internal bool EnableCachingInternal
		{
			get
			{
				if (this.IsDesignMode)
				{
					return false;
				}
				object obj = this.ViewState["EnableCaching"];
				return obj != null && (bool)this.ViewState["EnableCaching"];
			}
			set
			{
				if (!this.IsDesignMode && value && HttpContext.Current.Session == null)
				{
					throw new Exception("Before using caching for RadPivotGrid you need to enable Session for your page.");
				}
				this.ViewState["EnableCaching"] = value;
			}
		}

		// Token: 0x17002B20 RID: 11040
		// (get) Token: 0x06008823 RID: 34851 RVA: 0x001F12E8 File Offset: 0x001EF4E8
		// (set) Token: 0x06008824 RID: 34852 RVA: 0x001F1311 File Offset: 0x001EF511
		[Category("Style")]
		[DefaultValue(PivotGridTableLayout.Auto)]
		[Description("")]
		[NotifyParentProperty(true)]
		public virtual PivotGridTableLayout RowHeaderTableLayout
		{
			get
			{
				object obj = this.ViewState["RowHeaderTableLayout"];
				if (obj != null)
				{
					return (PivotGridTableLayout)obj;
				}
				return PivotGridTableLayout.Auto;
			}
			set
			{
				this.ViewState["RowHeaderTableLayout"] = value;
			}
		}

		// Token: 0x17002B21 RID: 11041
		// (get) Token: 0x06008825 RID: 34853 RVA: 0x001F132C File Offset: 0x001EF52C
		// (set) Token: 0x06008826 RID: 34854 RVA: 0x001F1369 File Offset: 0x001EF569
		[Category("Style")]
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue(PivotGridTableLayout.Auto)]
		public virtual PivotGridTableLayout ColumnHeaderTableLayout
		{
			get
			{
				object obj = this.ViewState["ColumnTableLayout"];
				if (this.ClientSettings.Resizing.AllowColumnResize)
				{
					return PivotGridTableLayout.Fixed;
				}
				if (obj != null)
				{
					return (PivotGridTableLayout)obj;
				}
				return PivotGridTableLayout.Auto;
			}
			set
			{
				this.ViewState["ColumnTableLayout"] = value;
			}
		}

		// Token: 0x17002B22 RID: 11042
		// (get) Token: 0x06008827 RID: 34855 RVA: 0x001F1384 File Offset: 0x001EF584
		// (set) Token: 0x06008828 RID: 34856 RVA: 0x001F13B7 File Offset: 0x001EF5B7
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value that will be displayed in data cells where aggragate values could not be calculated.")]
		[Category("Appearance")]
		[DefaultValue("Error")]
		public virtual string ErrorValue
		{
			get
			{
				object obj = this.ViewState["ErrorValue"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return this.Localization.ErrorValueText;
			}
			set
			{
				this.ViewState["ErrorValue"] = value;
			}
		}

		// Token: 0x17002B23 RID: 11043
		// (get) Token: 0x06008829 RID: 34857 RVA: 0x001F13CC File Offset: 0x001EF5CC
		// (set) Token: 0x0600882A RID: 34858 RVA: 0x001F13F9 File Offset: 0x001EF5F9
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value that will be displayed in data cells where aggragate values were empty.")]
		[Category("Appearance")]
		[DefaultValue("")]
		public virtual string EmptyValue
		{
			get
			{
				object obj = this.ViewState["EmptyValue"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["EmptyValue"] = value;
			}
		}

		// Token: 0x17002B24 RID: 11044
		// (get) Token: 0x0600882B RID: 34859 RVA: 0x001F140C File Offset: 0x001EF60C
		// (set) Token: 0x0600882C RID: 34860 RVA: 0x001F1438 File Offset: 0x001EF638
		[Description("Specify the maximum number of items that would appear in a page,when paging is enabled by AllowPaging property.")]
		[Category("Paging")]
		[SimplePersistenceSetting]
		[DefaultValue(10)]
		[NotifyParentProperty(true)]
		public virtual int PageSize
		{
			get
			{
				object obj = this.ControlState["PageSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				object obj = this.PageSize;
				if ((int)obj != value && this.AllowPaging)
				{
					PivotGridPageSizeChangedEventArgs pivotGridPageSizeChangedEventArgs = new PivotGridPageSizeChangedEventArgs("ChangePageSize", value);
					this.FirePageSizeChanged(pivotGridPageSizeChangedEventArgs);
					if (pivotGridPageSizeChangedEventArgs.Canceled)
					{
						return;
					}
					this.CurrentPageIndex = 0;
					this.SetRequiresDataBindingIfInitialized();
				}
				this.ControlState["PageSize"] = value;
			}
		}

		// Token: 0x17002B25 RID: 11045
		// (get) Token: 0x0600882D RID: 34861 RVA: 0x001F14B5 File Offset: 0x001EF6B5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DataSourceIsAssigned
		{
			get
			{
				return this.DataSource != null || base.IsBoundUsingDataSourceID;
			}
		}

		// Token: 0x17002B26 RID: 11046
		// (get) Token: 0x0600882E RID: 34862 RVA: 0x001F14C8 File Offset: 0x001EF6C8
		// (set) Token: 0x0600882F RID: 34863 RVA: 0x001F14F1 File Offset: 0x001EF6F1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(true)]
		[Browsable(false)]
		[Description("Gets or sets a value indicating the index of the currently active page in case paging is enabled")]
		[SimplePersistenceSetting]
		public int CurrentPageIndex
		{
			get
			{
				object obj = this.ControlState["CurrentPageIndex"];
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
				this.ControlState["CurrentPageIndex"] = value;
			}
		}

		// Token: 0x17002B27 RID: 11047
		// (get) Token: 0x06008830 RID: 34864 RVA: 0x001F1518 File Offset: 0x001EF718
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NestedStateManager]
		[Category("Style")]
		[NotifyParentProperty(true)]
		public virtual PivotGridPagerStyle PagerStyle
		{
			get
			{
				if (this.pagerStyle == null)
				{
					this.pagerStyle = new PivotGridPagerStyle(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.pagerStyle).TrackViewState();
					}
				}
				return this.pagerStyle;
			}
		}

		// Token: 0x17002B28 RID: 11048
		// (get) Token: 0x06008831 RID: 34865 RVA: 0x001F1548 File Offset: 0x001EF748
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Description("Gets the number of pages required to display the records of the data source in a RadPivotGrid control.")]
		[Category("Paging")]
		public virtual int PageCount
		{
			get
			{
				if (this.pagingManager != null)
				{
					return this.pagingManager.PageCount;
				}
				object obj = this.ControlState["_!PCount"];
				if (obj == null)
				{
					return 1;
				}
				return (int)obj;
			}
		}

		// Token: 0x17002B29 RID: 11049
		// (get) Token: 0x06008832 RID: 34866 RVA: 0x001F1588 File Offset: 0x001EF788
		// (set) Token: 0x06008833 RID: 34867 RVA: 0x001F15B1 File Offset: 0x001EF7B1
		[Description("Gets or sets a value indicating whether paging feature is enabled.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Paging")]
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

		// Token: 0x17002B2A RID: 11050
		// (get) Token: 0x06008834 RID: 34868 RVA: 0x001F15CC File Offset: 0x001EF7CC
		// (set) Token: 0x06008835 RID: 34869 RVA: 0x001F15F5 File Offset: 0x001EF7F5
		[Category("General")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether the pivotgrid uses IQueryableDataProvider.")]
		public virtual bool UseQueryableDataProvider
		{
			get
			{
				object obj = this.ViewState["UseQueryableDataProvider"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["UseQueryableDataProvider"] = value;
			}
		}

		// Token: 0x17002B2B RID: 11051
		// (get) Token: 0x06008836 RID: 34870 RVA: 0x001F1610 File Offset: 0x001EF810
		// (set) Token: 0x06008837 RID: 34871 RVA: 0x001F1639 File Offset: 0x001EF839
		[DefaultValue(false)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether the sorting feature is enabled.")]
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

		// Token: 0x17002B2C RID: 11052
		// (get) Token: 0x06008838 RID: 34872 RVA: 0x001F1654 File Offset: 0x001EF854
		// (set) Token: 0x06008839 RID: 34873 RVA: 0x001F167D File Offset: 0x001EF87D
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether natural sorting is enabled.")]
		public virtual bool AllowNaturalSort
		{
			get
			{
				object obj = this.ViewState["AllowNaturalSort"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowNaturalSort"] = value;
			}
		}

		// Token: 0x17002B2D RID: 11053
		// (get) Token: 0x0600883A RID: 34874 RVA: 0x001F1698 File Offset: 0x001EF898
		// (set) Token: 0x0600883B RID: 34875 RVA: 0x001F16C1 File Offset: 0x001EF8C1
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the filtering feature is enabled.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool AllowFiltering
		{
			get
			{
				object obj = this.ViewState["AllowFiltering"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AllowFiltering"] = value;
			}
		}

		// Token: 0x17002B2E RID: 11054
		// (get) Token: 0x0600883C RID: 34876 RVA: 0x001F16D9 File Offset: 0x001EF8D9
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("DataBinding")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PivotGridOLAPSettings OlapSettings
		{
			get
			{
				if (this.olapSettings == null)
				{
					this.olapSettings = new PivotGridOLAPSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.olapSettings).TrackViewState();
					}
				}
				return this.olapSettings;
			}
		}

		// Token: 0x17002B2F RID: 11055
		// (get) Token: 0x0600883D RID: 34877 RVA: 0x001F1708 File Offset: 0x001EF908
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Gets a collection of RadPivotGridDataItem objects that represent the data items of the current page of data in a RadPivotGrid control.")]
		[Browsable(false)]
		public virtual PivotGridItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new PivotGridItemCollection();
					this.EnsureChildControls();
				}
				return this.items;
			}
		}

		// Token: 0x17002B30 RID: 11056
		// (get) Token: 0x0600883E RID: 34878 RVA: 0x001F1729 File Offset: 0x001EF929
		// (set) Token: 0x0600883F RID: 34879 RVA: 0x001F1731 File Offset: 0x001EF931
		[Category("Appearance")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17002B31 RID: 11057
		// (get) Token: 0x06008840 RID: 34880 RVA: 0x001F173A File Offset: 0x001EF93A
		[Category("Default")]
		[NotifyParentProperty(true)]
		[MergableProperty(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PivotGridFieldsCollection Fields
		{
			get
			{
				if (this.fields == null)
				{
					this.fields = new PivotGridFieldsCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.fields).TrackViewState();
					}
				}
				return this.fields;
			}
		}

		// Token: 0x17002B32 RID: 11058
		// (get) Token: 0x06008841 RID: 34881 RVA: 0x001F176C File Offset: 0x001EF96C
		// (set) Token: 0x06008842 RID: 34882 RVA: 0x001F1850 File Offset: 0x001EFA50
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		internal List<RadPivotGrid.PersistableFieldSetting> FieldSettings
		{
			get
			{
				List<RadPivotGrid.PersistableFieldSetting> list = new List<RadPivotGrid.PersistableFieldSetting>();
				RadPivotGrid.PersistableFieldSetting persistableFieldSetting = new RadPivotGrid.PersistableFieldSetting();
				foreach (PivotGridField pivotGridField in this.Fields)
				{
					persistableFieldSetting = new RadPivotGrid.PersistableFieldSetting
					{
						SortOrder = pivotGridField.SortOrder,
						ZoneIndex = pivotGridField.ZoneIndex,
						UniqueName = pivotGridField.UniqueName,
						ZoneType = pivotGridField.ZoneType,
						IsHidden = pivotGridField.IsHidden,
						FieldType = pivotGridField.FieldType,
						DataField = pivotGridField.DataField
					};
					if (pivotGridField is PivotGridAggregateField)
					{
						persistableFieldSetting.Aggregate = (pivotGridField as PivotGridAggregateField).Aggregate.ToString();
					}
					list.Add(persistableFieldSetting);
				}
				return list;
			}
			set
			{
				foreach (RadPivotGrid.PersistableFieldSetting persistableFieldSetting in value)
				{
					PivotGridField pivotGridField = this.fields.GetFieldByUniqueName(persistableFieldSetting.UniqueName);
					if (pivotGridField == null && !string.IsNullOrEmpty(persistableFieldSetting.FieldType))
					{
						string fieldType;
						if ((fieldType = persistableFieldSetting.FieldType) != null)
						{
							if (!(fieldType == "PivotGridAggregateField"))
							{
								if (!(fieldType == "PivotGridColumnField"))
								{
									if (!(fieldType == "PivotGridRowField"))
									{
										if (fieldType == "PivotGridReportFilterField")
										{
											pivotGridField = new PivotGridReportFilterField();
										}
									}
									else
									{
										pivotGridField = new PivotGridRowField();
									}
								}
								else
								{
									pivotGridField = new PivotGridColumnField();
								}
							}
							else
							{
								pivotGridField = new PivotGridAggregateField();
							}
						}
						if (pivotGridField != null)
						{
							pivotGridField.UniqueName = persistableFieldSetting.UniqueName;
							pivotGridField.DataField = persistableFieldSetting.DataField;
							this.Fields.Add(pivotGridField);
						}
					}
					if (pivotGridField != null)
					{
						if (pivotGridField.ZoneType != persistableFieldSetting.ZoneType)
						{
							this.TryReorderField(pivotGridField, persistableFieldSetting.ZoneType, persistableFieldSetting.ZoneIndex);
							pivotGridField = this.fields.GetFieldByUniqueName(persistableFieldSetting.UniqueName);
						}
						pivotGridField.IsHidden = persistableFieldSetting.IsHidden;
						pivotGridField.SortOrder = persistableFieldSetting.SortOrder;
						pivotGridField.ZoneIndex = persistableFieldSetting.ZoneIndex;
						if (persistableFieldSetting.DataField != null)
						{
							pivotGridField.DataField = persistableFieldSetting.DataField;
						}
						if (pivotGridField is PivotGridAggregateField && !string.IsNullOrEmpty(persistableFieldSetting.Aggregate))
						{
							(pivotGridField as PivotGridAggregateField).Aggregate = (PivotGridAggregate)Enum.Parse(typeof(PivotGridAggregate), persistableFieldSetting.Aggregate);
						}
					}
				}
			}
		}

		// Token: 0x17002B33 RID: 11059
		// (get) Token: 0x06008843 RID: 34883 RVA: 0x001F1A04 File Offset: 0x001EFC04
		// (set) Token: 0x06008844 RID: 34884 RVA: 0x001F1A52 File Offset: 0x001EFC52
		[SimplePersistenceSetting]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Default")]
		public HashSet<Array> CollapsedRowIndexes
		{
			get
			{
				if (this.ControlState["CollapsedRowIndexes"] == null)
				{
					this.ControlState["CollapsedRowIndexes"] = new HashSet<Array>(new RadPivotGrid.ArrayComparer());
				}
				return this.ControlState["CollapsedRowIndexes"] as HashSet<Array>;
			}
			internal set
			{
				this.ControlState["CollapsedRowIndexes"] = value;
			}
		}

		// Token: 0x17002B34 RID: 11060
		// (get) Token: 0x06008845 RID: 34885 RVA: 0x001F1A68 File Offset: 0x001EFC68
		// (set) Token: 0x06008846 RID: 34886 RVA: 0x001F1AB6 File Offset: 0x001EFCB6
		[NotifyParentProperty(true)]
		[Category("Default")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public HashSet<Array> CollapsedColumnIndexes
		{
			get
			{
				if (this.ControlState["CollapsedColumnIndexes"] == null)
				{
					this.ControlState["CollapsedColumnIndexes"] = new HashSet<Array>(new RadPivotGrid.ArrayComparer());
				}
				return this.ControlState["CollapsedColumnIndexes"] as HashSet<Array>;
			}
			internal set
			{
				this.ControlState["CollapsedColumnIndexes"] = value;
			}
		}

		// Token: 0x17002B35 RID: 11061
		// (get) Token: 0x06008847 RID: 34887 RVA: 0x001F1AC9 File Offset: 0x001EFCC9
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Default")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Browsable(false)]
		public HashSet<PivotGridGroupSlot> AlreadyExpandedRowSlots
		{
			get
			{
				if (this.ControlState["AlreadyExpandedRowSlots"] == null)
				{
					this.ControlState["AlreadyExpandedRowSlots"] = new HashSet<PivotGridGroupSlot>();
				}
				return this.ControlState["AlreadyExpandedRowSlots"] as HashSet<PivotGridGroupSlot>;
			}
		}

		// Token: 0x17002B36 RID: 11062
		// (get) Token: 0x06008848 RID: 34888 RVA: 0x001F1B07 File Offset: 0x001EFD07
		[NotifyParentProperty(true)]
		[MergableProperty(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Default")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public HashSet<PivotGridGroupSlot> AlreadyExpandedColumnSlots
		{
			get
			{
				if (this.ControlState["AlreadyExpandedColumnSlots"] == null)
				{
					this.ControlState["AlreadyExpandedColumnSlots"] = new HashSet<PivotGridGroupSlot>();
				}
				return this.ControlState["AlreadyExpandedColumnSlots"] as HashSet<PivotGridGroupSlot>;
			}
		}

		// Token: 0x17002B37 RID: 11063
		// (get) Token: 0x06008849 RID: 34889 RVA: 0x001F1B45 File Offset: 0x001EFD45
		// (set) Token: 0x0600884A RID: 34890 RVA: 0x001F1B73 File Offset: 0x001EFD73
		[SimplePersistenceSetting]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public virtual PivotGridSortExpressionCollection SortExpressions
		{
			get
			{
				if (this.sortExpressions == null)
				{
					this.sortExpressions = new PivotGridSortExpressionCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.sortExpressions).TrackViewState();
					}
				}
				return this.sortExpressions;
			}
			internal set
			{
				this.sortExpressions = value;
				if (base.IsTrackingViewState)
				{
					((IStateManager)this.sortExpressions).TrackViewState();
				}
			}
		}

		// Token: 0x17002B38 RID: 11064
		// (get) Token: 0x0600884B RID: 34891 RVA: 0x001F1B8F File Offset: 0x001EFD8F
		[NotifyParentProperty(true)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PivotGridClientSettings ClientSettings
		{
			get
			{
				if (this.clientSettings == null)
				{
					this.clientSettings = new PivotGridClientSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.clientSettings).TrackViewState();
					}
				}
				return this.clientSettings;
			}
		}

		// Token: 0x17002B39 RID: 11065
		// (get) Token: 0x0600884C RID: 34892 RVA: 0x001F1BBE File Offset: 0x001EFDBE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PivotGridAccessibilitySettings AccessibilitySettings
		{
			get
			{
				if (this.accessibilitySettings == null)
				{
					this.accessibilitySettings = new PivotGridAccessibilitySettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.accessibilitySettings).TrackViewState();
					}
				}
				return this.accessibilitySettings;
			}
		}

		// Token: 0x17002B3A RID: 11066
		// (get) Token: 0x0600884D RID: 34893 RVA: 0x001F1BED File Offset: 0x001EFDED
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PivotGridTotalsSettings TotalsSettings
		{
			get
			{
				if (this.totalsSettings == null)
				{
					this.totalsSettings = new PivotGridTotalsSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.totalsSettings).TrackViewState();
					}
				}
				return this.totalsSettings;
			}
		}

		// Token: 0x17002B3B RID: 11067
		// (get) Token: 0x0600884E RID: 34894 RVA: 0x001F1C1C File Offset: 0x001EFE1C
		// (set) Token: 0x0600884F RID: 34895 RVA: 0x001F1C45 File Offset: 0x001EFE45
		[Bindable(true)]
		[DefaultValue(true)]
		[Description("RadPivotGrid_ShowFilterHeaderZone")]
		[Category("Appearance")]
		public virtual bool ShowFilterHeaderZone
		{
			get
			{
				object obj = this.ViewState["ShowFilterHeaderZone"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowFilterHeaderZone"] = value;
			}
		}

		// Token: 0x17002B3C RID: 11068
		// (get) Token: 0x06008850 RID: 34896 RVA: 0x001F1C60 File Offset: 0x001EFE60
		// (set) Token: 0x06008851 RID: 34897 RVA: 0x001F1C89 File Offset: 0x001EFE89
		[DefaultValue(true)]
		[Description("RadPivotGrid_ShowDataHeaderZone")]
		[Bindable(true)]
		[Category("Appearance")]
		public virtual bool ShowDataHeaderZone
		{
			get
			{
				object obj = this.ViewState["ShowDataHeaderZone"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowDataHeaderZone"] = value;
			}
		}

		// Token: 0x17002B3D RID: 11069
		// (get) Token: 0x06008852 RID: 34898 RVA: 0x001F1CA4 File Offset: 0x001EFEA4
		// (set) Token: 0x06008853 RID: 34899 RVA: 0x001F1CCD File Offset: 0x001EFECD
		[DefaultValue(true)]
		[Bindable(true)]
		[Description("RadPivotGrid_ShowColumnHeaderZone")]
		[Category("Appearance")]
		public virtual bool ShowColumnHeaderZone
		{
			get
			{
				object obj = this.ViewState["ShowColumnHeaderZone"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowColumnHeaderZone"] = value;
			}
		}

		// Token: 0x17002B3E RID: 11070
		// (get) Token: 0x06008854 RID: 34900 RVA: 0x001F1CE8 File Offset: 0x001EFEE8
		// (set) Token: 0x06008855 RID: 34901 RVA: 0x001F1D11 File Offset: 0x001EFF11
		[Bindable(true)]
		[DefaultValue(true)]
		[Description("RadPivotGrid_ShowRowHeaderZone")]
		[Category("Appearance")]
		public virtual bool ShowRowHeaderZone
		{
			get
			{
				object obj = this.ViewState["ShowRowHeaderZone"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowRowHeaderZone"] = value;
			}
		}

		// Token: 0x17002B3F RID: 11071
		// (get) Token: 0x06008856 RID: 34902 RVA: 0x001F1D2C File Offset: 0x001EFF2C
		// (set) Token: 0x06008857 RID: 34903 RVA: 0x001F1D55 File Offset: 0x001EFF55
		[Category("Appearance")]
		[Description("RadPivotGrid_EnableConfigurationPanel")]
		[Bindable(true)]
		[DefaultValue(false)]
		public virtual bool EnableConfigurationPanel
		{
			get
			{
				object obj = this.ViewState["EnableConfigurationPanel"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableConfigurationPanel"] = value;
			}
		}

		// Token: 0x17002B40 RID: 11072
		// (get) Token: 0x06008858 RID: 34904 RVA: 0x001F1D6D File Offset: 0x001EFF6D
		[NotifyParentProperty(true)]
		[ComplexPersistenceSetting]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PivotGridConfigurationPanelSettings ConfigurationPanelSettings
		{
			get
			{
				if (this.configurationPanelSettings == null)
				{
					this.configurationPanelSettings = new PivotGridConfigurationPanelSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.configurationPanelSettings).TrackViewState();
					}
				}
				return this.configurationPanelSettings;
			}
		}

		// Token: 0x17002B41 RID: 11073
		// (get) Token: 0x06008859 RID: 34905 RVA: 0x001F1D9C File Offset: 0x001EFF9C
		// (set) Token: 0x0600885A RID: 34906 RVA: 0x001F1DC5 File Offset: 0x001EFFC5
		[Description("Set to True to enable zone context menu")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool EnableZoneContextMenu
		{
			get
			{
				object obj = this.ViewState["EnableZoneContextMenu"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableZoneContextMenu"] = value;
			}
		}

		// Token: 0x17002B42 RID: 11074
		// (get) Token: 0x0600885B RID: 34907 RVA: 0x001F1DDD File Offset: 0x001EFFDD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PivotGridContextMenu ContextMenu
		{
			get
			{
				if (!this.ShouldIncludeContextMenu)
				{
					return null;
				}
				if (this.contextMenu == null)
				{
					this.contextMenu = new PivotGridContextMenu(this);
				}
				return this.contextMenu;
			}
		}

		// Token: 0x17002B43 RID: 11075
		// (get) Token: 0x0600885C RID: 34908 RVA: 0x001F1E03 File Offset: 0x001F0003
		private bool ShouldIncludeContextMenu
		{
			get
			{
				return this.EnableZoneContextMenu || (this.EnableConfigurationPanel && this.ConfigurationPanelSettings.EnableFieldsContextMenu);
			}
		}

		// Token: 0x17002B44 RID: 11076
		// (get) Token: 0x0600885D RID: 34909 RVA: 0x001F1E25 File Offset: 0x001F0025
		public PivotGridFieldsWindow FieldsWindow
		{
			get
			{
				if (!this.ShouldIncludeFieldsWindow)
				{
					return null;
				}
				if (this.fieldsWindow == null)
				{
					this.fieldsWindow = new PivotGridFieldsWindow(this);
				}
				return this.fieldsWindow;
			}
		}

		// Token: 0x17002B45 RID: 11077
		// (get) Token: 0x0600885E RID: 34910 RVA: 0x001F1E4B File Offset: 0x001F004B
		private bool ShouldIncludeFieldsWindow
		{
			get
			{
				return this.EnableZoneContextMenu || (this.EnableConfigurationPanel && this.ConfigurationPanelSettings.Position == PivotGridConfigurationPanelPosition.FieldsWindow);
			}
		}

		// Token: 0x17002B46 RID: 11078
		// (get) Token: 0x0600885F RID: 34911 RVA: 0x001F1E6E File Offset: 0x001F006E
		[ComplexPersistenceSetting]
		public PivotGridConfigurationPanel ConfigurationPanel
		{
			get
			{
				if (!this.EnableConfigurationPanel)
				{
					return null;
				}
				if (this.configurationPanel == null)
				{
					this.configurationPanel = new PivotGridConfigurationPanel(this);
				}
				return this.configurationPanel;
			}
		}

		// Token: 0x17002B47 RID: 11079
		// (get) Token: 0x06008860 RID: 34912 RVA: 0x001F1E94 File Offset: 0x001F0094
		public PivotGridToolTipManager ToolTipManager
		{
			get
			{
				if (this.toolTipManager == null)
				{
					this.toolTipManager = new PivotGridToolTipManager(this);
					this.toolTipManager.ID = "PivotGridToolTipManager";
					this.toolTipManager.ShowDelay = 450;
					this.toolTipManager.ShowCallout = false;
					this.toolTipManager.Position = ToolTipPosition.BottomRight;
					this.toolTipManager.HideEvent = ToolTipHideEvent.FromCode;
				}
				return this.toolTipManager;
			}
		}

		// Token: 0x17002B48 RID: 11080
		// (get) Token: 0x06008861 RID: 34913 RVA: 0x001F1F04 File Offset: 0x001F0104
		public PivotGridFilterWindow FilterWindow
		{
			get
			{
				if (this.filterWindow == null)
				{
					this.filterWindow = new PivotGridFilterWindow(this);
					this.filterWindow.ID = "FilterWindow";
					this.filterWindow.OffsetElementID = this.ID;
					this.filterWindow.Width = 520;
					this.filterWindow.Height = 570;
					this.filterWindow.VisibleTitlebar = true;
					this.filterWindow.VisibleStatusbar = false;
					this.filterWindow.Skin = this.Skin;
					this.filterWindow.RenderMode = this.ResolvedRenderMode;
					this.filterWindow.Modal = true;
					this.filterWindow.Overlay = this.Overlay;
					this.filterWindow.Behaviors = (WindowBehaviors.Resize | WindowBehaviors.Close | WindowBehaviors.Move);
				}
				return this.filterWindow;
			}
		}

		// Token: 0x17002B49 RID: 11081
		// (get) Token: 0x06008862 RID: 34914 RVA: 0x001F1FE0 File Offset: 0x001F01E0
		public PivotGridFilterDialog FilterDialog
		{
			get
			{
				if (this.filterDialog == null)
				{
					this.filterDialog = new PivotGridFilterDialog(this);
					this.filterDialog.ID = "FilterDialog";
					this.filterDialog.OffsetElementID = this.ID;
					this.filterDialog.Width = 500;
					this.filterDialog.Height = 170;
					this.filterDialog.VisibleTitlebar = true;
					this.filterDialog.VisibleStatusbar = false;
					this.filterDialog.Skin = this.Skin;
					this.filterDialog.Modal = true;
					this.filterDialog.Behaviors = (WindowBehaviors.Resize | WindowBehaviors.Close | WindowBehaviors.Move);
				}
				return this.filterDialog;
			}
		}

		// Token: 0x17002B4A RID: 11082
		// (get) Token: 0x06008863 RID: 34915 RVA: 0x001F2098 File Offset: 0x001F0298
		public PivotGridFieldSettingsWindow FieldSettingsWindow
		{
			get
			{
				if (this.fieldSettingsWindow == null)
				{
					this.fieldSettingsWindow = new PivotGridFieldSettingsWindow(this);
					this.fieldSettingsWindow.ID = "FieldSettingsWindow";
					this.fieldSettingsWindow.OffsetElementID = this.ID;
					this.fieldSettingsWindow.Height = 450;
					this.fieldSettingsWindow.Width = 435;
					this.fieldSettingsWindow.VisibleTitlebar = true;
					this.fieldSettingsWindow.Skin = this.Skin;
					this.fieldSettingsWindow.Modal = true;
					this.fieldSettingsWindow.Behaviors = (WindowBehaviors.Resize | WindowBehaviors.Close | WindowBehaviors.Move);
				}
				return this.fieldSettingsWindow;
			}
		}

		// Token: 0x17002B4B RID: 11083
		// (get) Token: 0x06008864 RID: 34916 RVA: 0x001F2144 File Offset: 0x001F0344
		// (set) Token: 0x06008865 RID: 34917 RVA: 0x001F2177 File Offset: 0x001F0377
		[DefaultValue("Drop Row Fields Here")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the header zone text when there are no items added to the row header zone.")]
		[Category("Appearance")]
		public virtual string RowHeaderZoneText
		{
			get
			{
				object obj = this.ViewState["RowHeaderZoneText"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return this.Localization.RowHeaderZoneText;
			}
			set
			{
				this.ViewState["RowHeaderZoneText"] = value;
			}
		}

		// Token: 0x17002B4C RID: 11084
		// (get) Token: 0x06008866 RID: 34918 RVA: 0x001F218C File Offset: 0x001F038C
		// (set) Token: 0x06008867 RID: 34919 RVA: 0x001F21BF File Offset: 0x001F03BF
		[Description("Gets or sets the filter zone text when there are no items added to the filter header zone.")]
		[DefaultValue("Drop Filter Fields Here")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public virtual string FilterHeaderZoneText
		{
			get
			{
				object obj = this.ViewState["FilterHeaderZoneText"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return this.Localization.FilterHeaderZoneText;
			}
			set
			{
				this.ViewState["FilterHeaderZoneText"] = value;
			}
		}

		// Token: 0x17002B4D RID: 11085
		// (get) Token: 0x06008868 RID: 34920 RVA: 0x001F21D4 File Offset: 0x001F03D4
		// (set) Token: 0x06008869 RID: 34921 RVA: 0x001F2207 File Offset: 0x001F0407
		[NotifyParentProperty(true)]
		[Description("Gets or sets the column zone text when there are no items added to the column header zone.")]
		[Category("Appearance")]
		[DefaultValue("Drop Column Fields Here")]
		public virtual string ColumnHeaderZoneText
		{
			get
			{
				object obj = this.ViewState["ColumnHeaderZoneText"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return this.Localization.ColumnHeaderZoneText;
			}
			set
			{
				this.ViewState["ColumnHeaderZoneText"] = value;
			}
		}

		// Token: 0x17002B4E RID: 11086
		// (get) Token: 0x0600886A RID: 34922 RVA: 0x001F221C File Offset: 0x001F041C
		// (set) Token: 0x0600886B RID: 34923 RVA: 0x001F224F File Offset: 0x001F044F
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue("Drop Data Fields Here")]
		[Description("Gets or sets the data zone text when there are no items added to the data header zone.")]
		public virtual string DataHeaderZoneText
		{
			get
			{
				object obj = this.ViewState["DataHeaderZoneText"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return this.Localization.DataHeaderZoneText;
			}
			set
			{
				this.ViewState["DataHeaderZoneText"] = value;
			}
		}

		// Token: 0x17002B4F RID: 11087
		// (get) Token: 0x0600886C RID: 34924 RVA: 0x001F2262 File Offset: 0x001F0462
		[NotifyParentProperty(true)]
		[Description("Gets the horizontal scroll div.")]
		public Panel HorizontalScrollDiv
		{
			get
			{
				return this.horizontalScrollDiv;
			}
		}

		// Token: 0x17002B50 RID: 11088
		// (get) Token: 0x0600886D RID: 34925 RVA: 0x001F226A File Offset: 0x001F046A
		[NotifyParentProperty(true)]
		[Description("Gets the vertical scroll div.")]
		public Panel VerticalScrollDiv
		{
			get
			{
				return this.verticalScrollDiv;
			}
		}

		// Token: 0x17002B51 RID: 11089
		// (get) Token: 0x0600886E RID: 34926 RVA: 0x001F2272 File Offset: 0x001F0472
		// (set) Token: 0x0600886F RID: 34927 RVA: 0x001F2293 File Offset: 0x001F0493
		[Description("When set to true enables support for WAI-ARIA")]
		[DefaultValue(false)]
		[Category("Behavior")]
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

		// Token: 0x17002B52 RID: 11090
		// (get) Token: 0x06008870 RID: 34928 RVA: 0x001F22AB File Offset: 0x001F04AB
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Pivot grid row headers cells style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[NotifyParentProperty(true)]
		public virtual Style RowHeaderCellStyle
		{
			get
			{
				if (this.rowHeaderCellStyle == null)
				{
					this.rowHeaderCellStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.rowHeaderCellStyle).TrackViewState();
					}
				}
				return this.rowHeaderCellStyle;
			}
		}

		// Token: 0x17002B53 RID: 11091
		// (get) Token: 0x06008871 RID: 34929 RVA: 0x001F22D9 File Offset: 0x001F04D9
		[Description("Pivot grid column headers cells style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[NotifyParentProperty(true)]
		public virtual Style ColumnHeaderCellStyle
		{
			get
			{
				if (this.columnHeaderCellStyle == null)
				{
					this.columnHeaderCellStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.columnHeaderCellStyle).TrackViewState();
					}
				}
				return this.columnHeaderCellStyle;
			}
		}

		// Token: 0x17002B54 RID: 11092
		// (get) Token: 0x06008872 RID: 34930 RVA: 0x001F2307 File Offset: 0x001F0507
		[NotifyParentProperty(true)]
		[Description("Pivot grid row totals' cells style")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		public virtual Style RowTotalCellStyle
		{
			get
			{
				if (this.rowTotalCellStyle == null)
				{
					this.rowTotalCellStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.rowTotalCellStyle).TrackViewState();
					}
				}
				return this.rowTotalCellStyle;
			}
		}

		// Token: 0x17002B55 RID: 11093
		// (get) Token: 0x06008873 RID: 34931 RVA: 0x001F2335 File Offset: 0x001F0535
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Pivot grid column totals' cells style")]
		[Category("Style")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public virtual Style ColumnTotalCellStyle
		{
			get
			{
				if (this.columnTotalCellStyle == null)
				{
					this.columnTotalCellStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.columnTotalCellStyle).TrackViewState();
					}
				}
				return this.columnTotalCellStyle;
			}
		}

		// Token: 0x17002B56 RID: 11094
		// (get) Token: 0x06008874 RID: 34932 RVA: 0x001F2363 File Offset: 0x001F0563
		[DefaultValue(null)]
		[Description("Pivot grid data cells style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[NotifyParentProperty(true)]
		public virtual Style DataCellStyle
		{
			get
			{
				if (this.dataCellStyle == null)
				{
					this.dataCellStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.dataCellStyle).TrackViewState();
					}
				}
				return this.dataCellStyle;
			}
		}

		// Token: 0x17002B57 RID: 11095
		// (get) Token: 0x06008875 RID: 34933 RVA: 0x001F2391 File Offset: 0x001F0591
		[Category("Style")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Description("Pivot grid row totals' cells style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual Style RowGrandTotalCellStyle
		{
			get
			{
				if (this.rowGrandTotalCellStyle == null)
				{
					this.rowGrandTotalCellStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.rowGrandTotalCellStyle).TrackViewState();
					}
				}
				return this.rowGrandTotalCellStyle;
			}
		}

		// Token: 0x17002B58 RID: 11096
		// (get) Token: 0x06008876 RID: 34934 RVA: 0x001F23BF File Offset: 0x001F05BF
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Pivot grid column totals' cells style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public virtual Style ColumnGrandTotalCellStyle
		{
			get
			{
				if (this.columnGrandTotalCellStyle == null)
				{
					this.columnGrandTotalCellStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.columnGrandTotalCellStyle).TrackViewState();
					}
				}
				return this.columnGrandTotalCellStyle;
			}
		}

		// Token: 0x17002B59 RID: 11097
		// (get) Token: 0x06008877 RID: 34935 RVA: 0x001F23ED File Offset: 0x001F05ED
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal PivotGridStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new PivotGridStrings(new LocalizationProvider("RadPivotGrid.Main", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17002B5A RID: 11098
		// (get) Token: 0x06008878 RID: 34936 RVA: 0x001F242C File Offset: 0x001F062C
		// (set) Token: 0x06008879 RID: 34937 RVA: 0x001F244C File Offset: 0x001F064C
		[Category("Misc")]
		[Description("Gets or sets a value indicating where RadPivotGrid will look for its .resx localization files.")]
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

		// Token: 0x17002B5B RID: 11099
		// (get) Token: 0x0600887A RID: 34938 RVA: 0x001F249F File Offset: 0x001F069F
		// (set) Token: 0x0600887B RID: 34939 RVA: 0x001F24BF File Offset: 0x001F06BF
		[Category("Appearance")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				if (value != this.ViewState["Culture"])
				{
					this._localization = null;
				}
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17002B5C RID: 11100
		// (get) Token: 0x0600887C RID: 34940 RVA: 0x001F24EC File Offset: 0x001F06EC
		// (set) Token: 0x0600887D RID: 34941 RVA: 0x001F2515 File Offset: 0x001F0715
		[Category("Appearance")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether the ToolTips in the PivotGrid will be enabled feature.")]
		public virtual bool EnableToolTips
		{
			get
			{
				object obj = this.ViewState["EnableToolTips"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableToolTips"] = value;
			}
		}

		// Token: 0x17002B5D RID: 11101
		// (get) Token: 0x0600887E RID: 34942 RVA: 0x001F2530 File Offset: 0x001F0730
		// (set) Token: 0x0600887F RID: 34943 RVA: 0x001F256A File Offset: 0x001F076A
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether the Filter window of RadPivotGrid should have Overlay set to ensure popups are over a flash element or Java applet.")]
		[Browsable(true)]
		public bool Overlay
		{
			get
			{
				bool? flag = this.ViewState["Overlay"] as bool?;
				return flag != null && flag.Value;
			}
			set
			{
				this.ViewState["Overlay"] = value;
				if (this.filterWindow != null)
				{
					this.filterWindow.Overlay = value;
				}
			}
		}

		// Token: 0x17002B5E RID: 11102
		// (get) Token: 0x06008880 RID: 34944 RVA: 0x001F2596 File Offset: 0x001F0796
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Category("Export")]
		[Description("Export settings")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PivotGridExportSettings ExportSettings
		{
			get
			{
				if (this._exportSettings == null)
				{
					this._exportSettings = new PivotGridExportSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._exportSettings).TrackViewState();
					}
				}
				return this._exportSettings;
			}
		}

		// Token: 0x17002B5F RID: 11103
		// (get) Token: 0x06008881 RID: 34945 RVA: 0x001F25C4 File Offset: 0x001F07C4
		// (set) Token: 0x06008882 RID: 34946 RVA: 0x001F25CC File Offset: 0x001F07CC
		[DefaultValue(null)]
		[Bindable(false)]
		[Description("Template that will be displayed if there is no aggregated data to show")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(PivotGridNoRecordsItem))]
		[Browsable(false)]
		public ITemplate NoRecordsTemplate
		{
			get
			{
				return this._noRecordsTemplate;
			}
			set
			{
				this._noRecordsTemplate = value;
			}
		}

		// Token: 0x17002B60 RID: 11104
		// (get) Token: 0x06008883 RID: 34947 RVA: 0x001F25D8 File Offset: 0x001F07D8
		// (set) Token: 0x06008884 RID: 34948 RVA: 0x001F2601 File Offset: 0x001F0801
		[DefaultValue(true)]
		[Bindable(true)]
		[Description("Gets or sets a value indicating whether the NoRecordsTemplate will be visualized when no records are present.")]
		[Browsable(true)]
		[Category("Behavior")]
		public bool EnableNoRecordsTemplate
		{
			get
			{
				object obj = this.ViewState["EnableNoRecordsTemplate"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["EnableNoRecordsTemplate"] = value;
			}
		}

		// Token: 0x06008885 RID: 34949 RVA: 0x001F2619 File Offset: 0x001F0819
		internal ITemplate GetNoRecordsTemplate()
		{
			if (this.NoRecordsTemplate == null || !this.EnableNoRecordsTemplate)
			{
				return new RadPivotGrid.NoRecordsDefaultTempate(this);
			}
			return this.NoRecordsTemplate;
		}

		// Token: 0x17002B61 RID: 11105
		// (get) Token: 0x06008886 RID: 34950 RVA: 0x001F2638 File Offset: 0x001F0838
		// (set) Token: 0x06008887 RID: 34951 RVA: 0x001F266B File Offset: 0x001F086B
		[DefaultValue("No records to display.")]
		[Description("Gets or sets the text that will appear in the PivotGridNoRecordsItem when the default no records template is used.")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public string NoRecordsText
		{
			get
			{
				object obj = this.ViewState["NoRecordsText"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return this.Localization.NoRecordsText;
			}
			set
			{
				this.ViewState["NoRecordsText"] = value;
			}
		}

		// Token: 0x06008888 RID: 34952 RVA: 0x001F267E File Offset: 0x001F087E
		public override void DataBind()
		{
			if (this.IsNeedDataSourceInProgress)
			{
				throw new InvalidOperationException("You should not call DataBind in NeedDataSource event handler. DataBind would take place automatically right after NeedDataSource handler finishes execution.");
			}
			base.DataBind();
		}

		// Token: 0x06008889 RID: 34953 RVA: 0x001F2699 File Offset: 0x001F0899
		public virtual void Rebind()
		{
			this.AutoDataBind(PivotGridRebindReason.ExplicitRebind);
		}

		// Token: 0x0600888A RID: 34954 RVA: 0x001F26A4 File Offset: 0x001F08A4
		public List<PivotGridRowZone> GetRowZones()
		{
			foreach (object obj in this.OuterTable.Rows)
			{
				PivotGridRowItem pivotGridRowItem = obj as PivotGridRowItem;
				if (pivotGridRowItem != null)
				{
					return pivotGridRowItem.RowZones;
				}
			}
			return null;
		}

		// Token: 0x0600888B RID: 34955 RVA: 0x001F2710 File Offset: 0x001F0910
		public PivotGridZone GetZoneByType(PivotGridZoneType type)
		{
			PivotGridZone result = null;
			if (type <= PivotGridZoneType.Row)
			{
				switch (type)
				{
				case PivotGridZoneType.Filter:
					result = this.GetPivotGridFilterZone();
					break;
				case PivotGridZoneType.Aggregate:
					result = this.GetPivotGridAggregateZone();
					break;
				case PivotGridZoneType.Filter | PivotGridZoneType.Aggregate:
					break;
				case PivotGridZoneType.Column:
					result = this.GetPivotGridColumnZone();
					break;
				default:
					if (type == PivotGridZoneType.Row)
					{
						result = this.GetPivotGridRowZone();
					}
					break;
				}
			}
			else if (type != PivotGridZoneType.Data)
			{
				if (type == PivotGridZoneType.ColumnHeader)
				{
					result = this.GetPivotGridColumnHeaderZone();
				}
			}
			else
			{
				result = this.GetPivotGridDataZone();
			}
			return result;
		}

		// Token: 0x0600888C RID: 34956 RVA: 0x001F2784 File Offset: 0x001F0984
		private PivotGridAggregateZone GetPivotGridAggregateZone()
		{
			foreach (object obj in this.OuterTable.Rows)
			{
				PivotGridAggregateItem pivotGridAggregateItem = obj as PivotGridAggregateItem;
				if (pivotGridAggregateItem != null)
				{
					return pivotGridAggregateItem.AggregateZone;
				}
			}
			return null;
		}

		// Token: 0x0600888D RID: 34957 RVA: 0x001F27F0 File Offset: 0x001F09F0
		private PivotGridFilterZone GetPivotGridFilterZone()
		{
			foreach (object obj in this.OuterTable.Rows)
			{
				PivotGridFilterItem pivotGridFilterItem = obj as PivotGridFilterItem;
				if (pivotGridFilterItem != null)
				{
					return pivotGridFilterItem.FilterZone;
				}
			}
			return null;
		}

		// Token: 0x0600888E RID: 34958 RVA: 0x001F285C File Offset: 0x001F0A5C
		private PivotGridColumnZone GetPivotGridColumnZone()
		{
			foreach (object obj in this.OuterTable.Rows)
			{
				PivotGridAggregateItem pivotGridAggregateItem = obj as PivotGridAggregateItem;
				if (pivotGridAggregateItem != null)
				{
					return pivotGridAggregateItem.ColumnZone;
				}
			}
			return null;
		}

		// Token: 0x0600888F RID: 34959 RVA: 0x001F28C8 File Offset: 0x001F0AC8
		private PivotGridRowZone GetPivotGridRowZone()
		{
			foreach (object obj in this.OuterTable.Rows)
			{
				PivotGridRowItem pivotGridRowItem = obj as PivotGridRowItem;
				if (pivotGridRowItem != null)
				{
					return pivotGridRowItem.GetRowZone();
				}
			}
			return null;
		}

		// Token: 0x06008890 RID: 34960 RVA: 0x001F2934 File Offset: 0x001F0B34
		private PivotGridDataZone GetPivotGridDataZone()
		{
			if (this.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				TableRowCollection rows = this.OuterTable.Rows;
				int count = rows.Count;
				TableRow tableRow = rows[count - 1];
				if (tableRow.Cells.Count != 2)
				{
					tableRow = rows[count - 2];
				}
				return tableRow.Cells[1] as PivotGridDataZone;
			}
			foreach (object obj in this.OuterTable.Rows)
			{
				PivotGridRowItem pivotGridRowItem = obj as PivotGridRowItem;
				if (pivotGridRowItem != null)
				{
					return pivotGridRowItem.DataZone;
				}
			}
			return null;
		}

		// Token: 0x06008891 RID: 34961 RVA: 0x001F2A08 File Offset: 0x001F0C08
		private PivotGridColumnHeaderZone GetPivotGridColumnHeaderZone()
		{
			PivotGridTable pivotGridTable = this.OuterTable;
			if (!this.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				pivotGridTable = this.columnHeaderTable;
			}
			if (pivotGridTable == null)
			{
				return null;
			}
			foreach (object obj in pivotGridTable.Rows)
			{
				PivotGridRowItem pivotGridRowItem = obj as PivotGridRowItem;
				if (pivotGridRowItem != null)
				{
					return pivotGridRowItem.ColumnHeaderZone;
				}
			}
			return null;
		}

		// Token: 0x06008892 RID: 34962 RVA: 0x001F2A98 File Offset: 0x001F0C98
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!string.IsNullOrEmpty(this.OlapSettings.ConnectionString))
			{
				string text = ConfigurationManager.AppSettings[this.OlapSettings.ConnectionString];
				if (!string.IsNullOrEmpty(text) && this.OlapSettings.ProviderType == PivotGridOlapProviderType.None)
				{
					this.ParseOlapConnectionString(text);
				}
			}
			if (this.UsesControlState)
			{
				this.Page.RegisterRequiresControlState(this);
			}
		}

		// Token: 0x06008893 RID: 34963 RVA: 0x001F2B04 File Offset: 0x001F0D04
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			this._pagePreLoadFired = true;
			base.OnPagePreLoad(sender, e);
		}

		// Token: 0x06008894 RID: 34964 RVA: 0x001F2B18 File Offset: 0x001F0D18
		protected override void OnLoad(EventArgs e)
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				base.ConfirmInitState();
				if (this.Page != null && !this._pagePreLoadFired && this.ViewState["_!DataBound"] == null)
				{
					if (!this.Page.IsPostBack)
					{
						base.RequiresDataBinding = true;
					}
					else if (base.IsViewStateEnabled)
					{
						base.RequiresDataBinding = true;
					}
				}
			}
			else
			{
				base.OnLoad(e);
			}
			if (this.ShouldBeBound)
			{
				this.AutoDataBind(PivotGridRebindReason.InitialLoad);
				return;
			}
			if (this.AlwaysAutoBindOnPostBack && this.shouldCallDataBindOnLoad)
			{
				this.AutoDataBind(PivotGridRebindReason.PostbackViewStateNotPersisted);
			}
		}

		// Token: 0x06008895 RID: 34965 RVA: 0x001F2BAC File Offset: 0x001F0DAC
		protected virtual void OnNeedDataSource(PivotGridNeedDataSourceEventArgs e)
		{
			this.IsNeedDataSourceInProgress = true;
			try
			{
				EventHandler<PivotGridNeedDataSourceEventArgs> eventHandler = base.Events[RadPivotGrid.EventNeedDataSource] as EventHandler<PivotGridNeedDataSourceEventArgs>;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
			finally
			{
				this.IsNeedDataSourceInProgress = false;
			}
		}

		// Token: 0x14000144 RID: 324
		// (add) Token: 0x06008896 RID: 34966 RVA: 0x001F2BFC File Offset: 0x001F0DFC
		// (remove) Token: 0x06008897 RID: 34967 RVA: 0x001F2C0F File Offset: 0x001F0E0F
		[Description("Fires when a pivotgrid is exporting.")]
		public event EventHandler<PivotGridExportingArgs> PivotGridExporting
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventExporting, value);
			}
		}

		// Token: 0x14000145 RID: 325
		// (add) Token: 0x06008898 RID: 34968 RVA: 0x001F2C22 File Offset: 0x001F0E22
		// (remove) Token: 0x06008899 RID: 34969 RVA: 0x001F2C35 File Offset: 0x001F0E35
		[Description("Fires when RadPivotGrid is exporting to BIFF format")]
		public event EventHandler<PivotGridBiffExportingEventArgs> PivotGridBiffExporting
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventBiffExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventBiffExporting, value);
			}
		}

		// Token: 0x14000146 RID: 326
		// (add) Token: 0x0600889A RID: 34970 RVA: 0x001F2C48 File Offset: 0x001F0E48
		// (remove) Token: 0x0600889B RID: 34971 RVA: 0x001F2C5B File Offset: 0x001F0E5B
		[Description("Fires when a PivotGrid is exporting to XLS/XLSX/DOCX formats.")]
		public event EventHandler<PivotGridInfrastructureExportingEventArgs> PivotGridInfrastructureExporting
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventInfrastructureExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventInfrastructureExporting, value);
			}
		}

		// Token: 0x0600889C RID: 34972 RVA: 0x001F2C70 File Offset: 0x001F0E70
		protected virtual void OnPivotGridInfrastructureExporting(PivotGridInfrastructureExportingEventArgs e)
		{
			EventHandler<PivotGridInfrastructureExportingEventArgs> eventHandler = base.Events[RadPivotGrid.EventInfrastructureExporting] as EventHandler<PivotGridInfrastructureExportingEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600889D RID: 34973 RVA: 0x001F2C9E File Offset: 0x001F0E9E
		internal void FirePivotGridInfrastructureExporting(PivotGridInfrastructureExportingEventArgs e)
		{
			this.OnPivotGridInfrastructureExporting(e);
		}

		// Token: 0x0600889E RID: 34974 RVA: 0x001F2CA8 File Offset: 0x001F0EA8
		protected virtual void OnPivotGridExporting(PivotGridExportingArgs e)
		{
			EventHandler<PivotGridExportingArgs> eventHandler = base.Events[RadPivotGrid.EventExporting] as EventHandler<PivotGridExportingArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600889F RID: 34975 RVA: 0x001F2CD8 File Offset: 0x001F0ED8
		internal virtual void OnPivotGridItemNeedCalculation(PivotGridCalculationEventArgs e)
		{
			EventHandler<PivotGridCalculationEventArgs> eventHandler = base.Events[RadPivotGrid.EventItemNeedCalculation] as EventHandler<PivotGridCalculationEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060088A0 RID: 34976 RVA: 0x001F2D06 File Offset: 0x001F0F06
		internal void FirePivotGridExporting(PivotGridExportingArgs e)
		{
			this.OnPivotGridExporting(e);
		}

		// Token: 0x060088A1 RID: 34977 RVA: 0x001F2D0F File Offset: 0x001F0F0F
		internal void CallOnBiffExporting(PivotGridBiffExportingEventArgs e)
		{
			this.OnBiffExporting(e);
		}

		// Token: 0x060088A2 RID: 34978 RVA: 0x001F2D18 File Offset: 0x001F0F18
		protected virtual void OnBiffExporting(PivotGridBiffExportingEventArgs e)
		{
			EventHandler<PivotGridBiffExportingEventArgs> eventHandler = (EventHandler<PivotGridBiffExportingEventArgs>)base.Events[RadPivotGrid.EventBiffExporting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000147 RID: 327
		// (add) Token: 0x060088A3 RID: 34979 RVA: 0x001F2D46 File Offset: 0x001F0F46
		// (remove) Token: 0x060088A4 RID: 34980 RVA: 0x001F2D59 File Offset: 0x001F0F59
		[Description("Fires when a pivotgrid cell is exporting.")]
		public event EventHandler<PivotGridCellExportingArgs> PivotGridCellExporting
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventCellExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventCellExporting, value);
			}
		}

		// Token: 0x060088A5 RID: 34981 RVA: 0x001F2D6C File Offset: 0x001F0F6C
		protected virtual void OnPivotGridCellExporting(PivotGridCellExportingArgs e)
		{
			EventHandler<PivotGridCellExportingArgs> eventHandler = base.Events[RadPivotGrid.EventCellExporting] as EventHandler<PivotGridCellExportingArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060088A6 RID: 34982 RVA: 0x001F2D9A File Offset: 0x001F0F9A
		internal void FirePivotGridCellExporting(PivotGridCellExportingArgs e)
		{
			this.OnPivotGridCellExporting(e);
		}

		// Token: 0x14000148 RID: 328
		// (add) Token: 0x060088A7 RID: 34983 RVA: 0x001F2DA3 File Offset: 0x001F0FA3
		// (remove) Token: 0x060088A8 RID: 34984 RVA: 0x001F2DB6 File Offset: 0x001F0FB6
		[Description("Raised when the pivot grid is about to be bound and the data source must be assigned")]
		[Category("Action")]
		public event EventHandler<PivotGridNeedDataSourceEventArgs> NeedDataSource
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventNeedDataSource, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventNeedDataSource, value);
			}
		}

		// Token: 0x14000149 RID: 329
		// (add) Token: 0x060088A9 RID: 34985 RVA: 0x001F2DC9 File Offset: 0x001F0FC9
		// (remove) Token: 0x060088AA RID: 34986 RVA: 0x001F2DDC File Offset: 0x001F0FDC
		[Category("Data")]
		[Description("Fires when calculation for Item or Field is needed")]
		public event EventHandler<PivotGridCalculationEventArgs> ItemNeedCalculation
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventItemNeedCalculation, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventItemNeedCalculation, value);
			}
		}

		// Token: 0x1400014A RID: 330
		// (add) Token: 0x060088AB RID: 34987 RVA: 0x001F2DEF File Offset: 0x001F0FEF
		// (remove) Token: 0x060088AC RID: 34988 RVA: 0x001F2E02 File Offset: 0x001F1002
		[Category("Action")]
		[Description("Fires when Sort has been changed.")]
		public event EventHandler<PivotGridSortEventArgs> Sorting
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventSorting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventSorting, value);
			}
		}

		// Token: 0x1400014B RID: 331
		// (add) Token: 0x060088AD RID: 34989 RVA: 0x001F2E15 File Offset: 0x001F1015
		// (remove) Token: 0x060088AE RID: 34990 RVA: 0x001F2E28 File Offset: 0x001F1028
		[Category("Action")]
		[Description("Fires when default description of the field is created")]
		public event EventHandler<PivotGridPrepareDescriptionForFieldEventArgs> PrepareDescriptionForField
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventPrepareDescriptionForField, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventPrepareDescriptionForField, value);
			}
		}

		// Token: 0x1400014C RID: 332
		// (add) Token: 0x060088AF RID: 34991 RVA: 0x001F2E3B File Offset: 0x001F103B
		// (remove) Token: 0x060088B0 RID: 34992 RVA: 0x001F2E4E File Offset: 0x001F104E
		[Description("Fires when all descrition fields are got from the cube")]
		[Category("Action")]
		public event EventHandler<GetDescriptionsDataCompletedEventArgs> GetDescriptionsDataCompleted
		{
			add
			{
				base.Events.AddHandler(RadPivotGrid.EventGetDescriptionsDataCompleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPivotGrid.EventGetDescriptionsDataCompleted, value);
			}
		}

		// Token: 0x060088B1 RID: 34993 RVA: 0x001F2E64 File Offset: 0x001F1064
		protected virtual void OnSorting(PivotGridSortEventArgs e)
		{
			EventHandler<PivotGridSortEventArgs> eventHandler = base.Events[RadPivotGrid.EventSorting] as EventHandler<PivotGridSortEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060088B2 RID: 34994 RVA: 0x001F2E92 File Offset: 0x001F1092
		internal void FireSorting(PivotGridSortEventArgs e)
		{
			this.OnSorting(e);
		}

		// Token: 0x060088B3 RID: 34995 RVA: 0x001F2E9B File Offset: 0x001F109B
		internal void FireInitFilterDialogue(PivotGridInitFilterDialogueEventArgs e)
		{
			this.OnInitFilterDialogue(e);
		}

		// Token: 0x060088B4 RID: 34996 RVA: 0x001F2EA4 File Offset: 0x001F10A4
		protected virtual void OnInitFilterDialogue(PivotGridInitFilterDialogueEventArgs e)
		{
			EventHandler<PivotGridInitFilterDialogueEventArgs> eventHandler = base.Events[RadPivotGrid.EventInitFilterDialogue] as EventHandler<PivotGridInitFilterDialogueEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060088B5 RID: 34997 RVA: 0x001F2ED2 File Offset: 0x001F10D2
		internal void FireFilterCommand(PivotGridFilterCommandEventArgs e)
		{
			this.OnFilterCommand(e);
		}

		// Token: 0x060088B6 RID: 34998 RVA: 0x001F2EDC File Offset: 0x001F10DC
		protected virtual void OnFilterCommand(PivotGridFilterCommandEventArgs e)
		{
			EventHandler<PivotGridFilterCommandEventArgs> eventHandler = base.Events[RadPivotGrid.EventFilterCommand] as EventHandler<PivotGridFilterCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060088B7 RID: 34999 RVA: 0x001F2F0A File Offset: 0x001F110A
		protected override void OnPreRender(EventArgs e)
		{
			if (base.RequiresDataBinding)
			{
				this.Rebind();
			}
			base.OnPreRender(e);
		}

		// Token: 0x060088B8 RID: 35000 RVA: 0x001F2F24 File Offset: 0x001F1124
		protected override void Render(HtmlTextWriter writer)
		{
			this.SetStyleClasses();
			this.PrepareTableItemsStyle(this.OuterTable.Rows);
			this.PrepareTableItemsStyle(this.RowHeaderTable.Rows);
			this.PrepareTableItemsStyle(this.ColumnHeaderTable.Rows);
			this.PrepareTableItemsStyle(this.DataTable.Rows);
			if (this.EnableConfigurationPanel)
			{
				this.PrepareConfigurationPanelStyles();
			}
			base.Render(writer);
		}

		// Token: 0x060088B9 RID: 35001 RVA: 0x001F2F90 File Offset: 0x001F1190
		internal void PrepareTableItemsStyle(TableRowCollection rows)
		{
			for (int i = 0; i < rows.Count; i++)
			{
				PivotGridItem pivotGridItem = rows[i] as PivotGridItem;
				if (pivotGridItem != null)
				{
					pivotGridItem.PrepareItemStyle();
				}
			}
		}

		// Token: 0x060088BA RID: 35002 RVA: 0x001F2FC4 File Offset: 0x001F11C4
		private void PrepareConfigurationPanelStyles()
		{
			if (this.ConfigurationPanel.RenderingControls != null)
			{
				foreach (PivotGridFieldRenderingControl pivotGridFieldRenderingControl in this.ConfigurationPanel.RenderingControls)
				{
					pivotGridFieldRenderingControl.PrepareFieldRenderingControlStyle();
				}
			}
		}

		// Token: 0x060088BB RID: 35003 RVA: 0x001F3028 File Offset: 0x001F1228
		public void ExportToExcel()
		{
			this.CurrentExportFormat = ((this.ExportSettings.Excel.Format == PivotGridExcelFormat.Biff) ? PivotGridExportFormat.Biff : PivotGridExportFormat.Xlsx);
			Page page = this.Page;
			PivotGridExporter @object = new PivotGridExporter(this);
			page.SetRenderMethodDelegate(new RenderMethod(@object.ExportRenderPage));
		}

		// Token: 0x060088BC RID: 35004 RVA: 0x001F3074 File Offset: 0x001F1274
		public void ExportToWord()
		{
			this.CurrentExportFormat = PivotGridExportFormat.Docx;
			Page page = this.Page;
			PivotGridExporter @object = new PivotGridExporter(this);
			page.SetRenderMethodDelegate(new RenderMethod(@object.ExportRenderPage));
		}

		// Token: 0x04002565 RID: 9573
		internal const string RadPivotGridClassName = "RadPivotGrid";

		// Token: 0x04002566 RID: 9574
		internal const string PagerItemPart1ClassName = "rpgArrPart1";

		// Token: 0x04002567 RID: 9575
		internal const string PagerItemPart2ClassName = "rpgArrPart2";

		// Token: 0x04002568 RID: 9576
		internal const string PagerItemAdvPartClassName = "rpgAdvPart";

		// Token: 0x04002569 RID: 9577
		internal const string PagerItemClassName = "rpgPager";

		// Token: 0x0400256A RID: 9578
		internal const string TopPagerItemClassName = "rpgPagerTop";

		// Token: 0x0400256B RID: 9579
		internal const string BottonPagerItemClassName = "rpgPagerBottom";

		// Token: 0x0400256C RID: 9580
		internal const string PagerContentCellClassName = "rpgPagerCell";

		// Token: 0x0400256D RID: 9581
		internal const string AggregateZoneClassName = "rpgDataZone";

		// Token: 0x0400256E RID: 9582
		internal const string RowZoneClassName = "rpgRowsZone";

		// Token: 0x0400256F RID: 9583
		internal const string FilterZoneClassName = "rpgFilterZone";

		// Token: 0x04002570 RID: 9584
		internal const string ColumnZoneClassName = "rpgColumnsZone";

		// Token: 0x04002571 RID: 9585
		internal const string RenderingControlClassName = "rpgFieldItem";

		// Token: 0x04002572 RID: 9586
		internal const string RenderingControlSubFieldClassName = "rpgFieldItem rpgSubFieldItem";

		// Token: 0x04002573 RID: 9587
		internal const string PivotGridTableClassName = "rpgTable";

		// Token: 0x04002574 RID: 9588
		internal const string PivotGridFieldsPopupClassName = "rpgFieldsPopup";

		// Token: 0x04002575 RID: 9589
		internal const string PivotGridFieldsPopupWrapperClassName = "rpgFieldsPopupWrapper";

		// Token: 0x04002576 RID: 9590
		internal const string PivotGridGroupedFieldsTitle = "rpgGroupedFieldsTitle";

		// Token: 0x04002577 RID: 9591
		internal const string PivotGridSortAscClassName = "rpgSortAsc";

		// Token: 0x04002578 RID: 9592
		internal const string PivotGridSortDescClassName = "rpgSortDesc";

		// Token: 0x04002579 RID: 9593
		internal const string PivotGridHorizontalScrollClassName = "rpgHorizontalScroll";

		// Token: 0x0400257A RID: 9594
		internal const string PivotGridVerticalScrollClassName = "rpgVerticalScroll";

		// Token: 0x0400257B RID: 9595
		internal const string PivotGridVerticalScrollDivClassName = "rpgVerticalScrollDiv";

		// Token: 0x0400257C RID: 9596
		internal const string PivotGridRowHeaderZoneClassName = "rpgRowHeaderZone";

		// Token: 0x0400257D RID: 9597
		internal const string PivotGridContentZoneClassName = "rpgContentZone";

		// Token: 0x0400257E RID: 9598
		internal const string PivotGridRowHeaderZoneDivClassName = "rpgRowHeaderZoneDiv";

		// Token: 0x0400257F RID: 9599
		internal const string PivotGridContentZoneDivClassName = "rpgContentZoneDiv";

		// Token: 0x04002580 RID: 9600
		internal const string PivotGridExpandCellClassName = "rpgCollapse";

		// Token: 0x04002581 RID: 9601
		internal const string PivotGridCollapseCellClassName = "rpgExpand";

		// Token: 0x04002582 RID: 9602
		internal const string PivotGridRowHeaderClassName = "rpgRowHeader";

		// Token: 0x04002583 RID: 9603
		internal const string PivotGridRowHeadeFieldClassName = "rpgRowHeaderField";

		// Token: 0x04002584 RID: 9604
		internal const string PivotGridRowHeaderTotalClassName = "rpgRowHeaderTotal";

		// Token: 0x04002585 RID: 9605
		internal const string PivotGridRowHeaderGrandTotalClassName = "rpgRowHeaderGrandTotal";

		// Token: 0x04002586 RID: 9606
		internal const string PivotGridColumnHeaderClassName = "rpgColumnHeader";

		// Token: 0x04002587 RID: 9607
		internal const string PivotGridColumnHeaderZoneClassName = "rpgColumnHeaderZone";

		// Token: 0x04002588 RID: 9608
		internal const string PivotGridColumnHeaderDivClassName = "rpgColumnHeaderDiv";

		// Token: 0x04002589 RID: 9609
		internal const string PivotGridColumnHeaderTotalClassName = "rpgColumnHeaderTotal";

		// Token: 0x0400258A RID: 9610
		internal const string PivotGridColumnHeaderGrandTotalClassName = "rpgColumnHeaderGrandTotal";

		// Token: 0x0400258B RID: 9611
		internal const string PivotGridDataCellClassName = "rpgDataCell";

		// Token: 0x0400258C RID: 9612
		internal const string PivotGridRowTotalDataCellClassName = "rpgRowTotalDataCell";

		// Token: 0x0400258D RID: 9613
		internal const string PivotGridColumnTotalDataCellClassName = "rpgColumnTotalDataCell";

		// Token: 0x0400258E RID: 9614
		internal const string PivotGridRowGrandTotalDataCellClassName = "rpgRowGrandTotalDataCell";

		// Token: 0x0400258F RID: 9615
		internal const string PivotGridColumnGrandTotalDataCellClassName = "rpgColumnGrandTotalDataCell";

		// Token: 0x04002590 RID: 9616
		internal const string PivotGridDimensionRowClassName = "rpgDimensionRow";

		// Token: 0x04002591 RID: 9617
		internal const string PivotGridCompactLayoutAdditionalCellClassName = "rpgEC";

		// Token: 0x04002592 RID: 9618
		internal const string PivotGridOuterTableWrapperClassName = "rpgOuterTableWrapper";

		// Token: 0x04002593 RID: 9619
		internal const string PivotGridTableWrapperClassName = "rpgTableWrapper";

		// Token: 0x04002594 RID: 9620
		internal const string ConfigurationPanelClassNameFormatString = "rpg{0}ConfigurationPanel";

		// Token: 0x04002595 RID: 9621
		internal const string PivotGridNoRecordsClassName = "rpgNoRecords";

		// Token: 0x04002596 RID: 9622
		internal const int RowTableColgroupWidth = 23;

		// Token: 0x04002597 RID: 9623
		internal const string EmptyCellContent = "<span style='display: none'>empty</span>";

		// Token: 0x04002598 RID: 9624
		internal const string DataSourceItemCountControlStateKey = "_!DSIC";

		// Token: 0x04002599 RID: 9625
		internal const string PageCountViewStateKey = "_!PCount";

		// Token: 0x0400259A RID: 9626
		internal const string TotalItemCountControlStateKey = "_!TotalItemCount";

		// Token: 0x0400259B RID: 9627
		internal const string ColumnGroupsCountKey = "ColumnGroupsCount";

		// Token: 0x0400259C RID: 9628
		internal const string RowLayoutLevelsCountKey = "_!RowLayoutLevelsCount";

		// Token: 0x0400259D RID: 9629
		public const string RebindPivotGridCommandName = "RebindPivotGrid";

		// Token: 0x0400259E RID: 9630
		public const string PageCommandName = "Page";

		// Token: 0x0400259F RID: 9631
		public const string FirstPageCommandArgument = "First";

		// Token: 0x040025A0 RID: 9632
		public const string LastPageCommandArgument = "Last";

		// Token: 0x040025A1 RID: 9633
		public const string NextPageCommandArgument = "Next";

		// Token: 0x040025A2 RID: 9634
		public const string PrevPageCommandArgument = "Prev";

		// Token: 0x040025A3 RID: 9635
		public const string ExpandCollapseCommandName = "ExpandCollapse";

		// Token: 0x040025A4 RID: 9636
		public const string ExpandCollapseLevelCommandName = "ExpandCollapseLevel";

		// Token: 0x040025A5 RID: 9637
		public const string SelectCommandName = "Select";

		// Token: 0x040025A6 RID: 9638
		public const string DeselectCommandName = "Deselect";

		// Token: 0x040025A7 RID: 9639
		public const string SelectAllCommandName = "SelectAll";

		// Token: 0x040025A8 RID: 9640
		public const string DeselectAllCommandName = "DeselectAll";

		// Token: 0x040025A9 RID: 9641
		public const string SortCommandName = "Sort";

		// Token: 0x040025AA RID: 9642
		public const string ChangePageSizeCommandName = "ChangePageSize";

		// Token: 0x040025AB RID: 9643
		public const string FieldReorderCommandName = "FieldReorder";

		// Token: 0x040025AC RID: 9644
		public const string ShowHideFieldCommandName = "ShowHideField";

		// Token: 0x040025AD RID: 9645
		public const string UpdateLayoutCommandName = "UpdateLayout";

		// Token: 0x040025AE RID: 9646
		public const string AggregateLabelChangeCommandName = "AggregateChange";

		// Token: 0x040025AF RID: 9647
		public const string InitFilterDialogueCommandName = "InitFilterDialogue";

		// Token: 0x040025B0 RID: 9648
		public const string FilterCommandName = "Filter";

		// Token: 0x040025B1 RID: 9649
		public const string AggregateFunctionChangedCommandName = "AggregateFunctionChanged";

		// Token: 0x040025B2 RID: 9650
		public const string PageSizeChangedCommandName = "PageSizeChanged";

		// Token: 0x040025B3 RID: 9651
		internal PivotGridPagingManager pagingManager;

		// Token: 0x040025B4 RID: 9652
		internal PivotGridGroupSlot rowGroupExpandCollapseSlot;

		// Token: 0x040025B5 RID: 9653
		internal PivotGridGroupSlot columnGroupExpandCollapseSlot;

		// Token: 0x040025B6 RID: 9654
		private PivotViewModel pivotModel;

		// Token: 0x040025B7 RID: 9655
		internal IDataProvider provider;

		// Token: 0x040025B8 RID: 9656
		private PivotModelSessionPersister modelPersister;

		// Token: 0x040025B9 RID: 9657
		private PivotGridOuterTable outerTable;

		// Token: 0x040025BA RID: 9658
		private PivotGridRowHeaderTable rowHeaderTable;

		// Token: 0x040025BB RID: 9659
		private PivotGridColumnHeaderTable columnHeaderTable;

		// Token: 0x040025BC RID: 9660
		private PivotGridDataTable dataTable;

		// Token: 0x040025BD RID: 9661
		private PivotGridPagerStyle pagerStyle;

		// Token: 0x040025BE RID: 9662
		private bool shouldCallDataBindOnLoad = true;

		// Token: 0x040025BF RID: 9663
		private PivotGridSortExpressionCollection sortExpressions;

		// Token: 0x040025C0 RID: 9664
		private static readonly object EventItemCreated;

		// Token: 0x040025C1 RID: 9665
		private static readonly object EventItemDataBound;

		// Token: 0x040025C2 RID: 9666
		private static readonly object EventCellCreated;

		// Token: 0x040025C3 RID: 9667
		private static readonly object EventCellDataBound;

		// Token: 0x040025C4 RID: 9668
		private static readonly object EventNeedDataSource;

		// Token: 0x040025C5 RID: 9669
		private static readonly object EventExporting;

		// Token: 0x040025C6 RID: 9670
		private static readonly object EventBiffExporting;

		// Token: 0x040025C7 RID: 9671
		private static readonly object EventInfrastructureExporting;

		// Token: 0x040025C8 RID: 9672
		private static readonly object EventCellExporting;

		// Token: 0x040025C9 RID: 9673
		private static readonly object EventPageSizeChanged;

		// Token: 0x040025CA RID: 9674
		private static readonly object EventPageIndexChanged;

		// Token: 0x040025CB RID: 9675
		private static readonly object EventItemCommand;

		// Token: 0x040025CC RID: 9676
		private static readonly object EventItemNeedCalculation;

		// Token: 0x040025CD RID: 9677
		private static readonly object EventSorting;

		// Token: 0x040025CE RID: 9678
		private static readonly object EventAddingFieldToZone;

		// Token: 0x040025CF RID: 9679
		private static readonly object EventFieldCreated;

		// Token: 0x040025D0 RID: 9680
		private static readonly object EventFieldReorder;

		// Token: 0x040025D1 RID: 9681
		private static readonly object EventShowHideField;

		// Token: 0x040025D2 RID: 9682
		private static readonly object EventInitFilterDialogue;

		// Token: 0x040025D3 RID: 9683
		private static readonly object EventFilterCommand;

		// Token: 0x040025D4 RID: 9684
		private static readonly object EventDataProviderError;

		// Token: 0x040025D5 RID: 9685
		private static readonly object EventDataProviderStatusChanged;

		// Token: 0x040025D6 RID: 9686
		internal static readonly object EventPrepareDescriptionForField;

		// Token: 0x040025D7 RID: 9687
		private static readonly object EventGetDescriptionsDataCompleted;

		// Token: 0x040025D8 RID: 9688
		private BaseLayout rowLayout;

		// Token: 0x040025D9 RID: 9689
		private BaseLayout columnLayout;

		// Token: 0x040025DA RID: 9690
		private PivotGridItemCollection items;

		// Token: 0x040025DB RID: 9691
		private PivotGridControlStateManager controlStateManager;

		// Token: 0x040025DC RID: 9692
		private PivotGridFieldsCollection fields;

		// Token: 0x040025DD RID: 9693
		private PivotGridFieldsPopupSettings popupSettings;

		// Token: 0x040025DE RID: 9694
		private PivotGridClientSettings clientSettings;

		// Token: 0x040025DF RID: 9695
		private PivotGridAccessibilitySettings accessibilitySettings;

		// Token: 0x040025E0 RID: 9696
		private PivotGridOLAPSettings olapSettings;

		// Token: 0x040025E1 RID: 9697
		private PivotGridTotalsSettings totalsSettings;

		// Token: 0x040025E2 RID: 9698
		private PivotGridConfigurationPanelSettings configurationPanelSettings;

		// Token: 0x040025E3 RID: 9699
		private Panel horizontalScrollDiv;

		// Token: 0x040025E4 RID: 9700
		internal Panel verticalScrollDiv;

		// Token: 0x040025E5 RID: 9701
		private Style rowHeaderCellStyle;

		// Token: 0x040025E6 RID: 9702
		private Style columnHeaderCellStyle;

		// Token: 0x040025E7 RID: 9703
		private Style rowTotalCellStyle;

		// Token: 0x040025E8 RID: 9704
		private Style columnTotalCellStyle;

		// Token: 0x040025E9 RID: 9705
		private Style dataCellStyle;

		// Token: 0x040025EA RID: 9706
		private Style rowGrandTotalCellStyle;

		// Token: 0x040025EB RID: 9707
		private Style columnGrandTotalCellStyle;

		// Token: 0x040025EC RID: 9708
		private bool shouldAdjustColumnsLayout;

		// Token: 0x040025ED RID: 9709
		private bool shouldChangeCollapsedState = true;

		// Token: 0x040025EE RID: 9710
		private HashSet<int> expandCollapseRowLevels = new HashSet<int>();

		// Token: 0x040025EF RID: 9711
		private HashSet<int> expandCollapseColumnLevels = new HashSet<int>();

		// Token: 0x040025F0 RID: 9712
		internal int columnHeaderItemsCreatedCount;

		// Token: 0x040025F1 RID: 9713
		internal Dictionary<string, PivotGridColumnHeaderCell> resizableHeaderCells = new Dictionary<string, PivotGridColumnHeaderCell>();

		// Token: 0x040025F2 RID: 9714
		internal SortedList<int, PivotGridColumnHeaderCell> resizeableHeaderCellsList = new SortedList<int, PivotGridColumnHeaderCell>();

		// Token: 0x040025F3 RID: 9715
		private PivotGridExportFormat currentExportFormat;

		// Token: 0x040025F4 RID: 9716
		private bool _isExporting;

		// Token: 0x040025F5 RID: 9717
		private PivotGridFilteringManager filteringManager;

		// Token: 0x040025F6 RID: 9718
		private bool shouldAddNewSettings;

		// Token: 0x040025F7 RID: 9719
		private PivotGridFiltersCollection filters;

		// Token: 0x040025F8 RID: 9720
		private HashSet<string> promissedFieldsForCreation;

		// Token: 0x040025F9 RID: 9721
		private static TFunc<string, string> parseFireCommandArgs = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[2];
		};

		// Token: 0x040025FA RID: 9722
		private static TFunc<string, string> parseFireCommandEventName = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[0];
		};

		// Token: 0x040025FB RID: 9723
		private static TFunc<string, string> parseFireCommandSecondArgs = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[1];
		};

		// Token: 0x040025FC RID: 9724
		private static TFunc<string, int, string> parseFilterCommandArgs = (string input, int index) => new Regex("(\\|\\?)").Split(input)[index];

		// Token: 0x040025FD RID: 9725
		private bool _ignoreDataSourceViewChanged;

		// Token: 0x040025FE RID: 9726
		private DataSourceView _currentDataSource;

		// Token: 0x040025FF RID: 9727
		private PivotGridContextMenu contextMenu;

		// Token: 0x04002600 RID: 9728
		private PivotGridFieldsWindow fieldsWindow;

		// Token: 0x04002601 RID: 9729
		private PivotGridConfigurationPanel configurationPanel;

		// Token: 0x04002602 RID: 9730
		private PivotGridToolTipManager toolTipManager;

		// Token: 0x04002603 RID: 9731
		private PivotGridFilterWindow filterWindow;

		// Token: 0x04002604 RID: 9732
		private PivotGridFilterDialog filterDialog;

		// Token: 0x04002605 RID: 9733
		private PivotGridFieldSettingsWindow fieldSettingsWindow;

		// Token: 0x04002606 RID: 9734
		private PivotGridStrings _localization;

		// Token: 0x04002607 RID: 9735
		private PivotGridExportSettings _exportSettings;

		// Token: 0x04002608 RID: 9736
		private ITemplate _noRecordsTemplate;

		// Token: 0x04002609 RID: 9737
		private bool _pagePreLoadFired;

		// Token: 0x02000E17 RID: 3607
		private class DummyDataSource : DataSourceControl
		{
			// Token: 0x060088E0 RID: 35040 RVA: 0x001F30F5 File Offset: 0x001F12F5
			public DummyDataSource(IEnumerable source)
			{
				this._source = source;
			}

			// Token: 0x060088E1 RID: 35041 RVA: 0x001F3104 File Offset: 0x001F1304
			protected override DataSourceView GetView(string viewName)
			{
				return new RadPivotGrid.DummyDataSource.DummyDataView(this, viewName, this._source);
			}

			// Token: 0x0400262E RID: 9774
			private IEnumerable _source;

			// Token: 0x02000E18 RID: 3608
			private class DummyDataView : DataSourceView
			{
				// Token: 0x060088E2 RID: 35042 RVA: 0x001F3113 File Offset: 0x001F1313
				public DummyDataView(IDataSource owner, string viewName, IEnumerable source) : base(owner, viewName)
				{
					this._source = source;
				}

				// Token: 0x060088E3 RID: 35043 RVA: 0x001F3124 File Offset: 0x001F1324
				protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
				{
					return this._source;
				}

				// Token: 0x0400262F RID: 9775
				private IEnumerable _source;
			}
		}

		// Token: 0x02000E19 RID: 3609
		internal class GeneralCalculatedItem : CalculatedItem
		{
			// Token: 0x060088E4 RID: 35044 RVA: 0x001F312C File Offset: 0x001F132C
			public GeneralCalculatedItem(RadPivotGrid owner, string groupName, int solveOrder)
			{
				this.ownerPivotGrid = owner;
				base.GroupName = groupName;
				base.SolveOrder = solveOrder;
			}

			// Token: 0x060088E5 RID: 35045 RVA: 0x001F314C File Offset: 0x001F134C
			protected internal override AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues)
			{
				PivotGridCalculationEventArgs pivotGridCalculationEventArgs = new PivotGridCalculationEventArgs
				{
					AggregateSummaryValues = aggregateSummaryValues,
					GroupName = base.GroupName
				};
				this.ownerPivotGrid.OnPivotGridItemNeedCalculation(pivotGridCalculationEventArgs);
				return pivotGridCalculationEventArgs.CalculatedValue;
			}

			// Token: 0x04002630 RID: 9776
			private RadPivotGrid ownerPivotGrid;
		}

		// Token: 0x02000E1A RID: 3610
		internal class GeneralCalculatedField : CalculatedField
		{
			// Token: 0x060088E6 RID: 35046 RVA: 0x001F3188 File Offset: 0x001F1388
			public GeneralCalculatedField(RadPivotGrid owner, string name, string calculateExpression, string[] requiredFields, AggregateFunction[] aggregates)
			{
				this.ownerPivotGrid = owner;
				base.Name = name;
				this.CalculateExpression = string.Format(calculateExpression, requiredFields);
				int num = 0;
				foreach (string propertyName in requiredFields)
				{
					if (aggregates != null && num < aggregates.Length)
					{
						this.requiredFields.Add(RequiredField.ForProperty(propertyName, aggregates[num]));
					}
					else
					{
						this.requiredFields.Add(RequiredField.ForProperty(propertyName));
					}
					num++;
				}
			}

			// Token: 0x060088E7 RID: 35047 RVA: 0x001F321B File Offset: 0x001F141B
			protected internal override IEnumerable<RequiredField> RequiredFields()
			{
				return this.requiredFields;
			}

			// Token: 0x060088E8 RID: 35048 RVA: 0x001F3224 File Offset: 0x001F1424
			protected internal override AggregateValue CalculateValue(IAggregateValues aggregateValues)
			{
				PivotGridCalculationEventArgs pivotGridCalculationEventArgs = new PivotGridCalculationEventArgs();
				pivotGridCalculationEventArgs.AggregateValues = aggregateValues;
				if (!string.IsNullOrEmpty(this.CalculateExpression))
				{
					try
					{
						DataTable dataTable = new DataTable();
						for (int i = 0; i < this.requiredFields.Count; i++)
						{
							dataTable.Columns.Add(new DataColumn(this.requiredFields[i].Name, typeof(double)));
						}
						dataTable.Columns.Add(new DataColumn(base.Name, typeof(double))
						{
							Expression = this.CalculateExpression
						});
						DataRow dataRow = dataTable.NewRow();
						for (int j = 0; j < this.requiredFields.Count; j++)
						{
							double num;
							if (double.TryParse(aggregateValues.GetAggregateValue(this.requiredFields[j]).GetValue().ToString(), out num))
							{
								dataRow[j] = num;
							}
						}
						dataTable.Rows.Add(dataRow);
						DataView dataView = new DataView(dataTable);
						pivotGridCalculationEventArgs.CalculatedValue = new DoubleAggregateValue((double)dataView[0][this.requiredFields.Count]);
					}
					catch
					{
						pivotGridCalculationEventArgs.CalculatedValue = null;
					}
				}
				pivotGridCalculationEventArgs.DataField = base.Name;
				this.ownerPivotGrid.OnPivotGridItemNeedCalculation(pivotGridCalculationEventArgs);
				return pivotGridCalculationEventArgs.CalculatedValue;
			}

			// Token: 0x04002631 RID: 9777
			private RadPivotGrid ownerPivotGrid;

			// Token: 0x04002632 RID: 9778
			private List<RequiredField> requiredFields = new List<RequiredField>();

			// Token: 0x04002633 RID: 9779
			public string CalculateExpression = "";
		}

		// Token: 0x02000E1B RID: 3611
		// (Invoke) Token: 0x060088EA RID: 35050
		private delegate void InsertGroupDescription(int index, IDescriptionBase description);

		// Token: 0x02000E1C RID: 3612
		[Serializable]
		private class ArrayComparer : IEqualityComparer<Array>
		{
			// Token: 0x060088ED RID: 35053 RVA: 0x001F33A0 File Offset: 0x001F15A0
			public bool Equals(Array x, Array y)
			{
				if (x.Length != y.Length)
				{
					return false;
				}
				for (int i = 0; i < x.Length; i++)
				{
					if (!object.Equals(x.GetValue(i), y.GetValue(i)))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060088EE RID: 35054 RVA: 0x001F33E8 File Offset: 0x001F15E8
			public int GetHashCode(Array obj)
			{
				int num = obj.Length;
				for (int i = 0; i < obj.Length; i++)
				{
					num = num * 2903 + obj.GetValue(i).GetHashCode();
				}
				return num;
			}
		}

		// Token: 0x02000E1D RID: 3613
		[Serializable]
		public class PersistableFieldSetting
		{
			// Token: 0x17002B62 RID: 11106
			// (get) Token: 0x060088F0 RID: 35056 RVA: 0x001F342B File Offset: 0x001F162B
			// (set) Token: 0x060088F1 RID: 35057 RVA: 0x001F3433 File Offset: 0x001F1633
			public PivotGridSortOrder SortOrder { get; set; }

			// Token: 0x17002B63 RID: 11107
			// (get) Token: 0x060088F2 RID: 35058 RVA: 0x001F343C File Offset: 0x001F163C
			// (set) Token: 0x060088F3 RID: 35059 RVA: 0x001F3444 File Offset: 0x001F1644
			public int ZoneIndex { get; set; }

			// Token: 0x17002B64 RID: 11108
			// (get) Token: 0x060088F4 RID: 35060 RVA: 0x001F344D File Offset: 0x001F164D
			// (set) Token: 0x060088F5 RID: 35061 RVA: 0x001F3455 File Offset: 0x001F1655
			public string UniqueName { get; set; }

			// Token: 0x17002B65 RID: 11109
			// (get) Token: 0x060088F6 RID: 35062 RVA: 0x001F345E File Offset: 0x001F165E
			// (set) Token: 0x060088F7 RID: 35063 RVA: 0x001F3466 File Offset: 0x001F1666
			public PivotGridFieldZoneType ZoneType { get; set; }

			// Token: 0x17002B66 RID: 11110
			// (get) Token: 0x060088F8 RID: 35064 RVA: 0x001F346F File Offset: 0x001F166F
			// (set) Token: 0x060088F9 RID: 35065 RVA: 0x001F3477 File Offset: 0x001F1677
			public bool IsHidden { get; set; }

			// Token: 0x17002B67 RID: 11111
			// (get) Token: 0x060088FA RID: 35066 RVA: 0x001F3480 File Offset: 0x001F1680
			// (set) Token: 0x060088FB RID: 35067 RVA: 0x001F3488 File Offset: 0x001F1688
			public string FieldType { get; set; }

			// Token: 0x17002B68 RID: 11112
			// (get) Token: 0x060088FC RID: 35068 RVA: 0x001F3491 File Offset: 0x001F1691
			// (set) Token: 0x060088FD RID: 35069 RVA: 0x001F3499 File Offset: 0x001F1699
			public string Aggregate { get; set; }

			// Token: 0x17002B69 RID: 11113
			// (get) Token: 0x060088FE RID: 35070 RVA: 0x001F34A2 File Offset: 0x001F16A2
			// (set) Token: 0x060088FF RID: 35071 RVA: 0x001F34AA File Offset: 0x001F16AA
			public string DataField { get; set; }
		}

		// Token: 0x02000E1E RID: 3614
		private class NoRecordsDefaultTempate : ITemplate
		{
			// Token: 0x06008901 RID: 35073 RVA: 0x001F34BB File Offset: 0x001F16BB
			public NoRecordsDefaultTempate(RadPivotGrid pivot)
			{
				this.pivot = pivot;
			}

			// Token: 0x06008902 RID: 35074 RVA: 0x001F34CA File Offset: 0x001F16CA
			public void InstantiateIn(Control container)
			{
				container.Controls.Add(new LiteralControl(this.pivot.NoRecordsText));
			}

			// Token: 0x0400263C RID: 9788
			private RadPivotGrid pivot;
		}
	}
}
