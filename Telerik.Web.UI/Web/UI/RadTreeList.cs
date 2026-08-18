using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace Telerik.Web.UI
{
	// Token: 0x02001260 RID: 4704
	[DefaultProperty("")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadTreeList", "Telerik.Web.UI.TreeList.RadTreeListScripts.js")]
	[Description("Telerik RadTreeList")]
	[Designer("Telerik.Web.Design.RadTreeListDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadTreeList), "Telerik.Web.UI.TreeList.png")]
	[TelerikToolboxCategory("Data")]
	[DefaultEvent("NeedDataSource")]
	[EmbeddedSkin("TreeList", typeof(RadTreeList))]
	[EmbeddedSkin("TreeList", "Default", typeof(RadTreeList))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadTreeList))]
	[ToolboxData("<{0}:RadTreeList runat=server></{0}:RadTreeList>")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadTreeList))]
	[LightweightRendering]
	[AdaptiveRendering]
	[RequiredScript(typeof(MaterialRipple))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadTreeList))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadTreeList))]
	public class RadTreeList : RadCompositeDataBoundControl, IPostBackEventHandler, INamingContainer, ILocalizableControl
	{
		// Token: 0x17003E8B RID: 16011
		// (get) Token: 0x0600C1F5 RID: 49653 RVA: 0x002B502F File Offset: 0x002B322F
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003E8C RID: 16012
		// (get) Token: 0x0600C1F6 RID: 49654 RVA: 0x002B5032 File Offset: 0x002B3232
		// (set) Token: 0x0600C1F7 RID: 49655 RVA: 0x002B503A File Offset: 0x002B323A
		protected bool IsDataBinding { get; set; }

		// Token: 0x17003E8D RID: 16013
		// (get) Token: 0x0600C1F8 RID: 49656 RVA: 0x002B5043 File Offset: 0x002B3243
		// (set) Token: 0x0600C1F9 RID: 49657 RVA: 0x002B504B File Offset: 0x002B324B
		protected IEnumerable CurrentDataSource { get; set; }

		// Token: 0x17003E8E RID: 16014
		// (get) Token: 0x0600C1FA RID: 49658 RVA: 0x002B5054 File Offset: 0x002B3254
		// (set) Token: 0x0600C1FB RID: 49659 RVA: 0x002B505C File Offset: 0x002B325C
		internal TreeListLoadOnDemandContext LoadOnDemandContext { get; set; }

		// Token: 0x17003E8F RID: 16015
		// (get) Token: 0x0600C1FC RID: 49660 RVA: 0x002B5065 File Offset: 0x002B3265
		// (set) Token: 0x0600C1FD RID: 49661 RVA: 0x002B506D File Offset: 0x002B326D
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

		// Token: 0x17003E90 RID: 16016
		// (get) Token: 0x0600C1FE RID: 49662 RVA: 0x002B5076 File Offset: 0x002B3276
		internal bool HasStaticHeaders
		{
			get
			{
				return this.ClientSettings.Scrolling.AllowScroll && this.ClientSettings.Scrolling.UseStaticHeaders;
			}
		}

		// Token: 0x17003E91 RID: 16017
		// (get) Token: 0x0600C1FF RID: 49663 RVA: 0x002B509C File Offset: 0x002B329C
		// (set) Token: 0x0600C200 RID: 49664 RVA: 0x002B50B7 File Offset: 0x002B32B7
		internal Dictionary<string, Dictionary<string, string>> CalculatedAggregates
		{
			get
			{
				if (this._calculatedAggregates == null)
				{
					this._calculatedAggregates = new Dictionary<string, Dictionary<string, string>>();
				}
				return this._calculatedAggregates;
			}
			set
			{
				this._calculatedAggregates = value;
			}
		}

		// Token: 0x17003E92 RID: 16018
		// (get) Token: 0x0600C201 RID: 49665 RVA: 0x002B50C0 File Offset: 0x002B32C0
		internal bool IsUsingModelBinding
		{
			get
			{
				return base.IsUsingModelBinders;
			}
		}

		// Token: 0x17003E93 RID: 16019
		// (get) Token: 0x0600C202 RID: 49666 RVA: 0x002B50C8 File Offset: 0x002B32C8
		internal TreeListLocalizationStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new TreeListLocalizationStrings(new LocalizationProvider("RadTreeList.Main", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17003E94 RID: 16020
		// (get) Token: 0x0600C203 RID: 49667 RVA: 0x002B5107 File Offset: 0x002B3307
		// (set) Token: 0x0600C204 RID: 49668 RVA: 0x002B511E File Offset: 0x002B331E
		internal int? CustomPageSize
		{
			get
			{
				return (int?)this.ControlState["CustomPageSize"];
			}
			set
			{
				this.ControlState["CustomPageSize"] = value;
			}
		}

		// Token: 0x17003E95 RID: 16021
		// (get) Token: 0x0600C205 RID: 49669 RVA: 0x002B5136 File Offset: 0x002B3336
		internal virtual TreeListDataSourceHelper DataSourceHelper
		{
			get
			{
				if (this._dataSourceHelper == null)
				{
					this._dataSourceHelper = new TreeListDataSourceHelper();
				}
				return this._dataSourceHelper;
			}
		}

		// Token: 0x17003E96 RID: 16022
		// (get) Token: 0x0600C206 RID: 49670 RVA: 0x002B5154 File Offset: 0x002B3354
		internal virtual TreeListEnumerableBase ResolvedDataSource
		{
			get
			{
				if (this._resolvedDataSource == null)
				{
					if (this.IsDataBinding)
					{
						this._resolvedDataSource = this.DataSourceHelper.GetResolvedDataSource(this, this.CurrentDataSource, this.DataMember);
					}
					else
					{
						this._resolvedDataSource = new TreeListEnumerableFromViewState(this.ControlState);
					}
					if (this._resolvedDataSource == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Cannot resolve data source. DataMember: '{0}'", new object[]
						{
							this.DataMember
						}));
					}
					this.PrepareDataSource();
				}
				return this._resolvedDataSource;
			}
		}

		// Token: 0x0600C207 RID: 49671 RVA: 0x002B51DC File Offset: 0x002B33DC
		protected virtual void PrepareDataSource()
		{
			if (this.ResolvedDataSource.SupportsSorting)
			{
				this.ResolvedDataSource.SetSortExpressions(this.SortExpressions);
			}
			if (this.ResolvedDataSource.SupportsPaging)
			{
				TreeListPagingManager pagingManager = this.ResolvedDataSource.PagingManager;
				pagingManager.CurrentPageIndex = this.CurrentPageIndex;
				pagingManager.PageSize = this.PageSize;
				pagingManager.AllowPaging = this.AllowPaging;
			}
		}

		// Token: 0x0600C208 RID: 49672 RVA: 0x002B5244 File Offset: 0x002B3444
		private void ClearResolvedDataSource()
		{
			this._resolvedDataSource = null;
		}

		// Token: 0x0600C209 RID: 49673 RVA: 0x002B524D File Offset: 0x002B344D
		internal void SetRequiresDataBindingIfInitialized()
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x17003E97 RID: 16023
		// (get) Token: 0x0600C20A RID: 49674 RVA: 0x002B525E File Offset: 0x002B345E
		internal bool IsBoundUsingDataSourceIDInternal
		{
			get
			{
				return base.IsBoundUsingDataSourceID;
			}
		}

		// Token: 0x0600C20B RID: 49675 RVA: 0x002B5268 File Offset: 0x002B3468
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			DataSourceSelectArguments result = new DataSourceSelectArguments();
			this.GetData();
			return result;
		}

		// Token: 0x0600C20C RID: 49676 RVA: 0x002B5283 File Offset: 0x002B3483
		protected override IDataSource GetDataSource()
		{
			return base.GetDataSource();
		}

		// Token: 0x0600C20D RID: 49677 RVA: 0x002B5334 File Offset: 0x002B3534
		static RadTreeList()
		{
			RadTreeList.EventNeedDataSource = new object();
			RadTreeList.EventChildItemsDataBind = new object();
			RadTreeList.EventItemCreated = new object();
			RadTreeList.EventItemDataBound = new object();
			RadTreeList.EventItemCommand = new object();
			RadTreeList.EventPageIndexChanged = new object();
			RadTreeList.EventPageSizeChanged = new object();
			RadTreeList.EventCreateCustomColumn = new object();
			RadTreeList.EventAutoGeneratedColumnCreated = new object();
			RadTreeList.EventSorting = new object();
			RadTreeList.EventEditCommand = new object();
			RadTreeList.EventInsertCommand = new object();
			RadTreeList.EventUpdateCommand = new object();
			RadTreeList.EventDeleteCommand = new object();
			RadTreeList.EventCancelCommand = new object();
			RadTreeList.EventCreateColumnEditor = new object();
			RadTreeList.EventItemUpdated = new object();
			RadTreeList.EventItemInserted = new object();
			RadTreeList.EventItemDeleted = new object();
			RadTreeList.EventItemDrop = new object();
			RadTreeList.EventColumnsOrderChanged = new object();
			RadTreeList.EventSelectedIndexChanged = new object();
			RadTreeList.EventExporting = new object();
			RadTreeList.EventPdfExporting = new object();
			RadTreeList.EventInfrastructureExporting = new object();
		}

		// Token: 0x0600C20E RID: 49678 RVA: 0x002B54A4 File Offset: 0x002B36A4
		public RadTreeList()
		{
			this._defaultInsertObjects = new HybridDictionary();
			this._customEditorInitializers = new Dictionary<TreeListEditableColumn, TreeListCreateCustomEditorDelegate>();
			this._popUpLocations = new Dictionary<string, Pair>();
			this._popUpIds = new List<string>();
			this.LoadOnDemandContext = new TreeListLoadOnDemandContext(this);
			this._treeListInitializedExpandCollapseIndexes = new TreeListExpandedIndexesCollection();
		}

		// Token: 0x0600C20F RID: 49679 RVA: 0x002B56A4 File Offset: 0x002B38A4
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (!this.IsDeleteInProgress)
			{
				if (this.ReorderContext != null)
				{
					this.CurrentDataSource = data;
					this.IsDataBinding = true;
					if (this.ResolvedDataSource == TreeListEnumerableBase.Null)
					{
						return;
					}
					if (this.ReorderContext.ReorderStage == TreeListReorderContext.DataReorderStage.InitialStage)
					{
						DataSourceView data2 = base.GetData();
						bool hasException = false;
						TreeListItemDragDropEventArgs dragArgs = this.ReorderContext.DragDropEventArgs;
						this.ReorderContext.ReorderStage = TreeListReorderContext.DataReorderStage.MappingStage;
						IEnumerable<TreeListSourceItem> itemsToReorder = this.ResolvedDataSource.GetItemsToReorder(this.ReorderContext);
						foreach (TreeListSourceItem treeListSourceItem in itemsToReorder)
						{
							int index = this.ReorderContext.ReorderedIndexes.IndexOf(treeListSourceItem.HierarchyIndex);
							Hashtable hashtable = new Hashtable();
							Hashtable hashtable2 = new Hashtable();
							foreach (string text in this.DataKeyNamesInternal)
							{
								hashtable.Add(text, this.ExtractDataKeyValue(treeListSourceItem.OriginalDataItem, text));
							}
							foreach (object obj in this.ReorderContext.OldValuesList[index].Keys)
							{
								hashtable2.Add(obj, this.ExtractDataKeyValue(treeListSourceItem.OriginalDataItem, obj.ToString()));
							}
							Hashtable hashtable3 = (Hashtable)hashtable2.Clone();
							foreach (object obj2 in dragArgs.UpdatedParentKeyValues)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
								if (hashtable3.ContainsKey(dictionaryEntry.Key))
								{
									hashtable3[dictionaryEntry.Key] = dictionaryEntry.Value;
								}
								else
								{
									hashtable3.Add(dictionaryEntry.Key, dictionaryEntry.Value);
								}
							}
							data2.Update(hashtable, hashtable3, hashtable2, delegate(int affectedRows, Exception exception)
							{
								TreeListUpdatedEventArgs treeListUpdatedEventArgs = new TreeListUpdatedEventArgs(affectedRows, exception, dragArgs.DraggedItems[index]);
								this.FireItemUpdated(treeListUpdatedEventArgs);
								hasException = (exception != null && !treeListUpdatedEventArgs.ExceptionHandled);
								return !hasException;
							});
							if (hasException)
							{
								break;
							}
						}
						if (!hasException)
						{
							this.ReorderContext.ReorderStage = TreeListReorderContext.DataReorderStage.IndexAdjustmentStage;
							this.ClearResolvedDataSource();
							return;
						}
					}
					else if (this.ReorderContext.ReorderStage == TreeListReorderContext.DataReorderStage.IndexAdjustmentStage)
					{
						this.ResolvedDataSource.AdjustReorderedIndexes(this.ReorderContext);
					}
				}
				base.PerformDataBinding(data);
				return;
			}
			this.CurrentDataSource = data;
			this.IsDataBinding = true;
			if (this.ResolvedDataSource == TreeListEnumerableBase.Null)
			{
				return;
			}
			IEnumerable<TreeListSourceItem> itemsToDelete = this.ResolvedDataSource.GetItemsToDelete(this.DeleteContext);
			DataSourceView data3 = base.GetData();
			if (this.AllowRecursiveDelete)
			{
				bool hasException = false;
				List<TreeListDataItem> childItemsRecursive = this.DeleteContext.Item.GetChildItemsRecursive();
				using (IEnumerator<TreeListSourceItem> enumerator4 = itemsToDelete.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						TreeListSourceItem treeListSourceItem2 = enumerator4.Current;
						Hashtable keys = new Hashtable();
						Hashtable hashtable4 = new Hashtable();
						foreach (string text2 in this.DataKeyNamesInternal)
						{
							keys.Add(text2, this.ExtractDataKeyValue(treeListSourceItem2.OriginalDataItem, text2));
						}
						foreach (object obj3 in this.DeleteContext.OldValues.Keys)
						{
							hashtable4.Add(obj3, this.ExtractDataKeyValue(treeListSourceItem2.OriginalDataItem, obj3.ToString()));
						}
						TreeListDataItem deletedItem = null;
						if (this.DeleteContext.Item.HasKeys(keys))
						{
							deletedItem = this.DeleteContext.Item;
						}
						else if (childItemsRecursive.Count > 0)
						{
							foreach (TreeListDataItem treeListDataItem in childItemsRecursive)
							{
								if (treeListDataItem.HasKeys(keys))
								{
									deletedItem = treeListDataItem;
									break;
								}
							}
						}
						data3.Delete(keys, hashtable4, delegate(int affectedRows, Exception exception)
						{
							TreeListDeletedEventArgs treeListDeletedEventArgs = new TreeListDeletedEventArgs(affectedRows, exception, keys, deletedItem);
							this.OnItemDeleted(treeListDeletedEventArgs);
							hasException = (exception != null);
							return !hasException || treeListDeletedEventArgs.ExceptionHandled;
						});
					}
					goto IL_235;
				}
			}
			data3.Delete(this.DeleteContext.Keys, this.DeleteContext.OldValues, delegate(int affectedRows, Exception exception)
			{
				if (exception == null)
				{
					List<TreeListIndexesCollection<TreeListHierarchyIndex>> list = new List<TreeListIndexesCollection<TreeListHierarchyIndex>>
					{
						this.ExpandedIndexes,
						this.EditIndexes,
						this.InsertIndexes,
						this.SelectedIndexes
					};
					for (int k = 0; k < list.Count; k++)
					{
						TreeListIndexesCollection<TreeListHierarchyIndex> treeListIndexesCollection = list[k];
						treeListIndexesCollection.Clear();
						treeListIndexesCollection.AddRange(this.DeleteContext.Indexes[k]);
					}
				}
				TreeListDeletedEventArgs treeListDeletedEventArgs = new TreeListDeletedEventArgs(affectedRows, exception, this.DeleteContext.Keys, this.DeleteContext.Item);
				this.OnItemDeleted(treeListDeletedEventArgs);
				if (exception == null && !this.DeleteContext.SuppressRebind)
				{
					this.Rebind();
				}
				return treeListDeletedEventArgs.ExceptionHandled;
			});
			IL_235:
			this.ClearResolvedDataSource();
		}

		// Token: 0x0600C210 RID: 49680 RVA: 0x002B5C0C File Offset: 0x002B3E0C
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			bool flag = this.CommandItemDisplay == TreeListCommandItemDisplay.Top || this.CommandItemDisplay == TreeListCommandItemDisplay.TopAndBottom;
			bool flag2 = this.CommandItemDisplay == TreeListCommandItemDisplay.Bottom || this.CommandItemDisplay == TreeListCommandItemDisplay.TopAndBottom;
			bool hasStaticHeaders = this.HasStaticHeaders;
			if (this.IsDesignMode && (this.DataKeyNames.Length == 0 || this.ParentDataKeyNames.Length == 0))
			{
				return 0;
			}
			if (dataBinding && (this.IsUsingModelBinding || !string.IsNullOrWhiteSpace(this.ItemType)))
			{
				this.ModelBindingModelType = dataSource.AsQueryable().ElementType;
			}
			if (this.ShowFooter)
			{
				TreeListAggregatesHelper.AggregatesSourceItemsCollection = new Dictionary<TreeListHierarchyIndex, List<TreeListSourceItem>>();
				TreeListAggregatesHelper.AggregatedSourceItems = new Dictionary<TreeListHierarchyIndex, TreeListSourceItem>();
			}
			this.CurrentDataSource = dataSource;
			this.IsDataBinding = dataBinding;
			if ((this.ResolvedDataSource == TreeListEnumerableBase.Null && dataSource != null) || (this.ResolvedDataSource is TreeListEnumerableFromViewState && this.IsDataBinding))
			{
				this.ClearResolvedDataSource();
			}
			if (this.ResolvedDataSource == TreeListEnumerableBase.Null && dataSource == null)
			{
				this.Controls.Clear();
				return 0;
			}
			if (this.IsDataBinding)
			{
				this.DataKeysArrayList.Clear();
				this.ParentDataKeysArrayList.Clear();
				this.ClientDataKeysArrayList.Clear();
				this.ItemState.Clear();
				this.ClearRenderColumns();
				this.ClearAutoGeneratedColumns();
				this.ClearDefaultInsertValues();
				this.ClearCustomEditorInitializers();
				this.FooterItems.Clear();
				DataSourceView data = this.GetData();
				bool flag3 = this.AllowPaging && data.CanPage;
				DataSourceSelectArguments selectArguments = base.SelectArguments;
				bool flag4 = false;
				if (flag3 && data.CanRetrieveTotalRowCount)
				{
					flag4 = true;
				}
				if (flag4)
				{
					this.PrepareDataSource();
				}
			}
			TreeListEnumerableHelper.showFooter = this.ShowFooter;
			IEnumerable<TreeListSourceItem> dataSource2 = this.ResolvedDataSource.RawEnumerable();
			if (this.AllowLoadOnDemand)
			{
				HashSet<TreeListHierarchyIndex> hashSet = new HashSet<TreeListHierarchyIndex>(this.ExpandedIndexes);
				foreach (TreeListSourceItem treeListSourceItem in this.LoadOnDemandContext.ExpandedItems)
				{
					if (treeListSourceItem != null)
					{
						hashSet.Add(treeListSourceItem.HierarchyIndex);
					}
				}
				this.ControlState["_expandedItems"] = new List<Hashtable>(this.LoadOnDemandContext.ExpandedItemsDataKeyValues);
			}
			if (this.ResolvedRenderMode == RenderMode.Mobile && flag)
			{
				this.CreateMobileCommandItem();
			}
			TreeListTable treeListTable = null;
			if (hasStaticHeaders)
			{
				treeListTable = new TreeListTable(this);
			}
			bool flag5 = this.CommandItemDisplay != TreeListCommandItemDisplay.None || (this.AllowPaging && this.PagerStyle.Position != TreeListPagerPosition.Bottom);
			if (hasStaticHeaders && flag5)
			{
				treeListTable.RenderStaticHeadersOnly = true;
				this.Controls.Add(treeListTable);
				if (this.ResolvedRenderMode == RenderMode.Lightweight && flag)
				{
					this.CreateCommandItem(treeListTable.Controls, true);
				}
				this.CreatePagerItem(treeListTable.Controls, true);
			}
			TreeListTable treeListTable2 = new TreeListTable(this);
			this.Controls.Add(treeListTable2);
			if (hasStaticHeaders)
			{
				TreeListTable treeListTable3 = new TreeListTable(this);
				treeListTable3.RenderBodyWithStaticHeaders = true;
				this.Controls.Add(treeListTable3);
				if (this.AllowPaging || this.CommandItemDisplay == TreeListCommandItemDisplay.TopAndBottom || this.CommandItemDisplay == TreeListCommandItemDisplay.Bottom)
				{
					TreeListTable treeListTable4 = new TreeListTable(this);
					treeListTable4.RenderTfootWithStaticHeaders = true;
					this.Controls.Add(treeListTable4);
				}
			}
			if (this.ResolvedRenderMode == RenderMode.Mobile && flag2)
			{
				this.CreateMobileCommandItem();
			}
			if (!hasStaticHeaders)
			{
				if (this.ResolvedRenderMode == RenderMode.Lightweight && flag)
				{
					this.CreateCommandItem(treeListTable2.Controls, true);
				}
				this.CreatePagerItem(treeListTable2.Controls, true);
			}
			this.CreateHeaderItem(treeListTable2.Controls);
			if (this.IsItemInserted)
			{
				this.CreateRootInsertItem(treeListTable2.Controls);
			}
			if (this.ShowFooter && this.IsDataBinding)
			{
				TreeListColumnsCollection treeListColumnsCollection = new TreeListColumnsCollection(this);
				foreach (TreeListColumn treeListColumn in this.RenderColumns)
				{
					if (treeListColumn is TreeListBoundColumn || treeListColumn is TreeListTemplateColumn || treeListColumn is TreeListCalculatedColumn)
					{
						treeListColumnsCollection.Add(treeListColumn);
					}
				}
				if (treeListColumnsCollection.Count > 0)
				{
					this.CalculateAggregates(treeListColumnsCollection);
				}
			}
			this.ExpandHash = new HashSet<TreeListHierarchyIndex>(this.ExpandedIndexes);
			this.CreateDataItems(dataSource2, treeListTable2.Controls);
			this.ExpandHash = null;
			this.HandlerNoRecords(treeListTable2);
			if (this.ResolvedRenderMode == RenderMode.Lightweight && flag2)
			{
				this.CreateCommandItem(treeListTable2.Controls, false);
			}
			this.CreatePagerItem(treeListTable2.Controls, false);
			this.CalculateMostNestedIndex();
			this.SavePagingData(this.IsDataBinding, this.ResolvedDataSource.Count, this.ResolvedDataSource.DataSourceCount);
			this.CurrentDataSource = null;
			if (!base.IsViewStateEnabled)
			{
				this.DataSource = null;
				base.RequiresDataBinding = false;
			}
			this.ClearResolvedDataSource();
			base.ChildControlsCreated = true;
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				this.CreateMobileViews();
			}
			if (this.ShowFooter)
			{
				TreeListAggregatesHelper.AggregatesSourceItemsCollection = null;
				TreeListAggregatesHelper.AggregatedSourceItems = null;
			}
			if (this.ControlState["_!ItemCount"] == null)
			{
				return 0;
			}
			return (int)this.ControlState["_!ItemCount"];
		}

		// Token: 0x0600C211 RID: 49681 RVA: 0x002B6100 File Offset: 0x002B4300
		private void CalculateMostNestedIndex()
		{
			if (this.ItemState.Count == 0)
			{
				return;
			}
			TreeListIndexesCollection<KeyValuePair<TreeListHierarchyIndex, TreeListItemState>> treeListIndexesCollection = new TreeListIndexesCollection<KeyValuePair<TreeListHierarchyIndex, TreeListItemState>>(this.ItemState);
			treeListIndexesCollection.Sort(new RadTreeList.MostNestedIndexComparission());
			this.MostNestedIndex = treeListIndexesCollection[0].Key.NestedLevel;
		}

		// Token: 0x0600C212 RID: 49682 RVA: 0x002B614C File Offset: 0x002B434C
		protected bool ShouldCreatePagerItem(bool isTopItem)
		{
			return this.ResolvedDataSource.PagingManager.IsPagingEnabled && ((isTopItem && this.PagerStyle.Position > TreeListPagerPosition.Bottom) || (!isTopItem && this.PagerStyle.Position != TreeListPagerPosition.Top));
		}

		// Token: 0x0600C213 RID: 49683 RVA: 0x002B618C File Offset: 0x002B438C
		private void HandlerNoRecords(TreeListTable mainTable)
		{
			if (this.EnableNoRecordsTemplate && this.ControlState["_!TotalItemCount"] != null && (int)this.ControlState["_!TotalItemCount"] == 0)
			{
				TreeListNoRecordsItem treeListNoRecordsItem = new TreeListNoRecordsItem(this, TreeListItemType.NoRecordsTemplateItem, false);
				TreeListTable treeListTable = this.GetTreeListTable();
				treeListTable.Rows.Add(treeListNoRecordsItem);
				treeListNoRecordsItem.Initialize(this.RenderColumnsInternal);
			}
		}

		// Token: 0x0600C214 RID: 49684 RVA: 0x002B61F4 File Offset: 0x002B43F4
		protected void CreatePagerItem(ControlCollection tableRows, bool isTopItem)
		{
			if (this.ShouldCreatePagerItem(isTopItem))
			{
				if (!this.PagerStyle.AlwaysVisible && this.ControlState["_!TotalItemCount"] != null && (int)this.ControlState["_!TotalItemCount"] == 0)
				{
					return;
				}
				TreeListPagerItem treeListPagerItem = new TreeListPagerItem(this, TreeListItemType.PagerItem, this.IsDataBinding);
				treeListPagerItem.IsTopItem = isTopItem;
				tableRows.Add(treeListPagerItem);
				treeListPagerItem.Initialize(this.RenderColumnsInternal);
			}
		}

		// Token: 0x0600C215 RID: 49685 RVA: 0x002B626C File Offset: 0x002B446C
		protected void CreateCommandItem(ControlCollection tableRows, bool isTopItem)
		{
			if (this.ResolvedRenderMode == RenderMode.Mobile && (this.ClientSettings.AllowColumnHide || this.ClientSettings.Reordering.AllowColumnsReorder || this.CommandItemDisplay != TreeListCommandItemDisplay.None))
			{
				this.CreateMobileCommandItem();
			}
			TreeListCommandItem treeListCommandItem = new TreeListCommandItem(this, TreeListItemType.CommandItem, this.IsDataBinding);
			treeListCommandItem.IsTopItem = isTopItem;
			tableRows.Add(treeListCommandItem);
			treeListCommandItem.Initialize(this.RenderColumnsInternal);
		}

		// Token: 0x0600C216 RID: 49686 RVA: 0x002B62DC File Offset: 0x002B44DC
		private void CreateMobileCommandItem()
		{
			Panel panel = new Panel();
			panel.CssClass = "rtlCommand";
			if (this.ClientSettings.AllowColumnHide || this.ClientSettings.Reordering.AllowColumnsReorder)
			{
				HtmlGenericControl child = RadTreeList.CreateButton("Menu", "Menu", true);
				panel.Controls.Add(child);
			}
			if (this.CommandItemDisplay != TreeListCommandItemDisplay.None)
			{
				HtmlGenericControl child2 = RadTreeList.CreateButton("Export", "Export", true);
				panel.Controls.Add(child2);
			}
			this.Controls.Add(panel);
		}

		// Token: 0x0600C217 RID: 49687 RVA: 0x002B6368 File Offset: 0x002B4568
		protected void CreateHeaderItem(ControlCollection tableRows)
		{
			TreeListHeaderItem treeListHeaderItem = new TreeListHeaderItem(this, TreeListItemType.HeaderItem, this.IsDataBinding);
			tableRows.Add(treeListHeaderItem);
			treeListHeaderItem.Initialize(this.RenderColumnsInternal);
		}

		// Token: 0x0600C218 RID: 49688 RVA: 0x002B6396 File Offset: 0x002B4596
		private int CalculateItemDataIndex(int currentDisplayIndex)
		{
			return currentDisplayIndex + this.CurrentPageIndex * this.PageSize;
		}

		// Token: 0x0600C219 RID: 49689 RVA: 0x002B63A8 File Offset: 0x002B45A8
		protected internal virtual TreeListDataItem CreateDataItem(int displayIndex, TreeListHierarchyIndex hierarchyIndex)
		{
			if (this.EditIndexes.Contains(hierarchyIndex))
			{
				return new TreeListDataItem(this, TreeListItemType.EditItem, displayIndex, this.IsDataBinding);
			}
			if (this.SelectedIndexes.Contains(hierarchyIndex))
			{
				return new TreeListDataItem(this, TreeListItemType.SelectedItem, displayIndex, this.IsDataBinding);
			}
			if (displayIndex % 2 != 0)
			{
				return new TreeListDataItem(this, TreeListItemType.AlternatingItem, displayIndex, this.IsDataBinding);
			}
			return new TreeListDataItem(this, displayIndex, this.IsDataBinding);
		}

		// Token: 0x0600C21A RID: 49690 RVA: 0x002B6414 File Offset: 0x002B4614
		protected internal virtual TreeListEditFormItem CreateEditFormItem(TreeListDataItem parentItem)
		{
			return new TreeListEditFormItem(this, parentItem, this.IsDataBinding);
		}

		// Token: 0x0600C21B RID: 49691 RVA: 0x002B6424 File Offset: 0x002B4624
		protected internal virtual TreeListEditableItem CreateInsertItem(TreeListDataItem parentItem)
		{
			object key = this;
			if (parentItem != null)
			{
				key = parentItem.HierarchyIndex;
			}
			if (this.EditMode == TreeListEditMode.InPlace)
			{
				return new TreeListDataInsertItem(this, parentItem, this.IsDataBinding)
				{
					DataItem = (this._defaultInsertObjects[key] ?? this.CreateInsertionObject())
				};
			}
			return new TreeListEditFormInsertItem(this, parentItem, this.IsDataBinding)
			{
				DataItem = (this._defaultInsertObjects[key] ?? this.CreateInsertionObject())
			};
		}

		// Token: 0x0600C21C RID: 49692 RVA: 0x002B649C File Offset: 0x002B469C
		protected virtual void CreateRootInsertItem(ControlCollection tableRows)
		{
			TreeListEditableItem treeListEditableItem = this.CreateInsertItem(null);
			tableRows.Add(treeListEditableItem);
			treeListEditableItem.Initialize(this.RenderColumnsInternal);
		}

		// Token: 0x0600C21D RID: 49693 RVA: 0x002B64C4 File Offset: 0x002B46C4
		protected internal virtual TreeListDetailTemplateItem CreateDetailTemplateItem(TreeListDataItem parentItem)
		{
			return new TreeListDetailTemplateItem(this, TreeListItemType.DetailTemplateItem, this.IsDataBinding, parentItem);
		}

		// Token: 0x0600C21E RID: 49694 RVA: 0x002B64D8 File Offset: 0x002B46D8
		protected virtual int CreateDataItems(IEnumerable<TreeListSourceItem> dataSource, ControlCollection tableRows)
		{
			int num = 0;
			this.Items.Clear();
			if (dataSource == null)
			{
				return num;
			}
			if (this.AllowLoadOnDemand)
			{
				this.SelectedIndexes.Clear();
			}
			foreach (TreeListSourceItem treeListSourceItem in dataSource)
			{
				if (this.IsDataBinding)
				{
					KeyValuePair<TreeListHierarchyIndex, TreeListItemState> item = new KeyValuePair<TreeListHierarchyIndex, TreeListItemState>(treeListSourceItem.HierarchyIndex, treeListSourceItem.ItemState);
					this.ItemState.UnCheckedAdd(item);
					this.PopulateDataKeys(treeListSourceItem.OriginalDataItem);
				}
				TreeListDataItem treeListDataItem = this.CreateDataItem(num, treeListSourceItem.HierarchyIndex);
				if (this.AllowLoadOnDemand && !this.IsDesignMode)
				{
					Hashtable hashtable = new Hashtable();
					foreach (string text in this.DataKeyNames)
					{
						hashtable.Add(text, treeListDataItem.GetDataKeyValue(text));
					}
					if (this.LoadOnDemandContext.ItemNeedsToBeSelected(hashtable))
					{
						this.SelectedIndexes.Add(treeListSourceItem.HierarchyIndex);
						treeListDataItem = this.CreateDataItem(num, treeListSourceItem.HierarchyIndex);
					}
					else if (this.AllowRecursiveSelection)
					{
						TreeListDataItem treeListDataItem2 = null;
						foreach (TreeListDataItem treeListDataItem3 in this.Items)
						{
							if (treeListDataItem3.HierarchyIndex == treeListSourceItem.ItemState.ParentHierarchyIndex)
							{
								treeListDataItem2 = treeListDataItem3;
								break;
							}
						}
						if (treeListDataItem2 != null && treeListDataItem2.Selected)
						{
							this.SelectedIndexes.Add(treeListSourceItem.HierarchyIndex);
							treeListDataItem.Selected = true;
							treeListDataItem = this.CreateDataItem(num, treeListSourceItem.HierarchyIndex);
						}
					}
				}
				treeListDataItem.DataItemIndex = this.CalculateItemDataIndex(num);
				treeListDataItem.DataItem = treeListSourceItem.OriginalDataItem;
				treeListDataItem.ItemState = treeListSourceItem.ItemState;
				treeListDataItem.HierarchyIndex = treeListSourceItem.HierarchyIndex;
				treeListDataItem.SourceItem = treeListSourceItem;
				tableRows.Add(treeListDataItem);
				this.Items.Add(treeListDataItem);
				treeListDataItem.Initialize(this.RenderColumnsInternal);
				if (this.DetailTemplate != null)
				{
					TreeListDetailTemplateItem treeListDetailTemplateItem = this.CreateDetailTemplateItem(treeListDataItem);
					treeListDetailTemplateItem.DataItem = treeListSourceItem.OriginalDataItem;
					tableRows.Add(treeListDetailTemplateItem);
					treeListDetailTemplateItem.Initialize(this.RenderColumnsInternal);
					treeListDataItem.DetailItem = treeListDetailTemplateItem;
				}
				if (treeListDataItem.IsInEditMode)
				{
					TreeListEditableItem treeListEditableItem = treeListDataItem;
					if (this.EditMode == TreeListEditMode.EditForms || this.EditMode == TreeListEditMode.PopUp)
					{
						TreeListEditFormItem treeListEditFormItem = this.CreateEditFormItem(treeListDataItem);
						treeListEditFormItem.DataItem = treeListDataItem.DataItem;
						tableRows.Add(treeListEditFormItem);
						treeListEditFormItem.Initialize(this.RenderColumnsInternal);
						treeListEditableItem = treeListEditFormItem;
					}
					if (this.IsDataBinding && treeListEditableItem.CanExtractValues)
					{
						treeListEditableItem.ExtractValues(treeListEditableItem.SavedOldValues);
					}
				}
				if (treeListDataItem.IsChildInserted)
				{
					TreeListEditableItem treeListEditableItem2 = this.CreateInsertItem(treeListDataItem);
					tableRows.Add(treeListEditableItem2);
					treeListEditableItem2.Initialize(this.RenderColumnsInternal);
				}
				if (this.ShowFooter && this.FooterItems.ContainsKey(treeListDataItem.HierarchyIndex))
				{
					List<TreeListHierarchyIndex> list = this.FooterItems[treeListDataItem.HierarchyIndex];
					foreach (TreeListHierarchyIndex hierarchyIndex in list)
					{
						TreeListFooterItem treeListFooterItem = new TreeListFooterItem(this, hierarchyIndex, treeListDataItem, this.IsDataBinding);
						tableRows.Add(treeListFooterItem);
						treeListFooterItem.Initialize(this.RenderColumnsInternal);
					}
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600C21F RID: 49695 RVA: 0x002B6884 File Offset: 0x002B4A84
		internal object CreateInsertionObject()
		{
			IDictionary defaultInsertValues = this.GetDefaultInsertValues();
			if (defaultInsertValues == null || defaultInsertValues.Count <= 0)
			{
				return null;
			}
			if (this.ModelBindingModelType != null && (this.IsUsingModelBinding || !string.IsNullOrWhiteSpace(this.ItemType)))
			{
				object obj = Activator.CreateInstance(this.ModelBindingModelType);
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj.GetType());
				for (int i = 0; i < properties.Count; i++)
				{
					object obj2 = defaultInsertValues[properties[i].DisplayName];
					if (obj2 != null)
					{
						properties[i].SetValue(obj, obj2);
					}
				}
				return obj;
			}
			return new TreeListInsertionObject(defaultInsertValues);
		}

		// Token: 0x0600C220 RID: 49696 RVA: 0x002B6924 File Offset: 0x002B4B24
		internal IDictionary GetDefaultInsertValues()
		{
			if (this._defaultInsertValues == null)
			{
				ListDictionary listDictionary = new ListDictionary();
				foreach (TreeListColumn treeListColumn in this.RenderColumns)
				{
					TreeListEditableColumn treeListEditableColumn = treeListColumn as TreeListEditableColumn;
					if (treeListEditableColumn != null && !string.IsNullOrEmpty(treeListEditableColumn.DataField) && !listDictionary.Contains(treeListEditableColumn.DataField))
					{
						try
						{
							object value;
							if (!string.IsNullOrEmpty(treeListEditableColumn.DefaultInsertValue))
							{
								if (treeListEditableColumn.DataType == typeof(Guid))
								{
									value = new Guid(treeListEditableColumn.DefaultInsertValue);
								}
								else
								{
									value = Convert.ChangeType(treeListEditableColumn.DefaultInsertValue, treeListEditableColumn.DataType, CultureInfo.CurrentCulture);
								}
							}
							else
							{
								value = null;
							}
							listDictionary.Add(treeListEditableColumn.DataField, value);
						}
						catch (FormatException innerException)
						{
							throw new FormatException(string.Format(CultureInfo.CurrentCulture, "The default insert value for column {0} cannot be converted to type {1}", new object[]
							{
								treeListEditableColumn.UniqueName,
								treeListEditableColumn.DataType.ToString()
							}), innerException);
						}
						catch (InvalidCastException innerException2)
						{
							throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "The default insert value for column {0} cannot be converted to type {1}", new object[]
							{
								treeListEditableColumn.UniqueName,
								treeListEditableColumn.DataType.ToString()
							}), innerException2);
						}
					}
				}
				if (listDictionary.Count > 0)
				{
					this._defaultInsertValues = listDictionary;
				}
			}
			return this._defaultInsertValues;
		}

		// Token: 0x0600C221 RID: 49697 RVA: 0x002B6AA4 File Offset: 0x002B4CA4
		internal void ClearDefaultInsertValues()
		{
			this._defaultInsertValues = null;
		}

		// Token: 0x0600C222 RID: 49698 RVA: 0x002B6AB0 File Offset: 0x002B4CB0
		private void SavePagingData(bool isDataBinding, int itemsCreatedCount, int dataSourceItemsCount)
		{
			if (isDataBinding)
			{
				this.ControlState["_!DSIC"] = dataSourceItemsCount;
				this.ControlState["_!ItemCount"] = itemsCreatedCount;
				if (this.ResolvedDataSource.PagingManager.IsPagingEnabled)
				{
					this.ControlState["_!PCount"] = this.ResolvedDataSource.PagingManager.PageCount;
					return;
				}
				this.ControlState["_!PCount"] = null;
			}
		}

		// Token: 0x0600C223 RID: 49699 RVA: 0x002B6B38 File Offset: 0x002B4D38
		private void EnsureColumns()
		{
			if (this._renderColumns != null)
			{
				return;
			}
			this._renderColumns = new List<TreeListColumn>();
			this.UpdateColumnsDataType();
			this._renderColumns.AddRange(this.Columns);
			this._renderColumns.AddRange(this.AutoGeneratedColumns);
			this.BuildOrderIndexes(this._renderColumns);
		}

		// Token: 0x0600C224 RID: 49700 RVA: 0x002B6B90 File Offset: 0x002B4D90
		private void BuildOrderIndexes(List<TreeListColumn> renderColumns)
		{
			bool flag = false;
			for (int i = 0; i < renderColumns.Count; i++)
			{
				TreeListColumn treeListColumn = renderColumns[i];
				if (treeListColumn.OrderIndex == -1)
				{
					treeListColumn.OrderIndex = i;
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				renderColumns.Sort();
			}
			this._renderColumns = renderColumns;
		}

		// Token: 0x17003E98 RID: 16024
		// (get) Token: 0x0600C225 RID: 49701 RVA: 0x002B6BDC File Offset: 0x002B4DDC
		private bool IsMobile
		{
			get
			{
				return this.Page.Request != null && !string.IsNullOrEmpty(this.Page.Request.UserAgent) && (Regex.IsMatch(this.Page.Request.UserAgent, "like\\sMac\\sOS\\sX.*Mobile\\S+") || Regex.IsMatch(this.Page.Request.UserAgent, "Android.*Safari\\S+") || Regex.IsMatch(this.Page.Request.UserAgent, "BlackBerry.*Safari\\S+"));
			}
		}

		// Token: 0x17003E99 RID: 16025
		// (get) Token: 0x0600C226 RID: 49702 RVA: 0x002B6C63 File Offset: 0x002B4E63
		internal List<TreeListColumn> RenderColumnsInternal
		{
			get
			{
				this.EnsureColumns();
				return this._renderColumns;
			}
		}

		// Token: 0x17003E9A RID: 16026
		// (get) Token: 0x0600C227 RID: 49703 RVA: 0x002B6C71 File Offset: 0x002B4E71
		[Browsable(false)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeListColumn[] RenderColumns
		{
			get
			{
				return this.RenderColumnsInternal.ToArray();
			}
		}

		// Token: 0x0600C228 RID: 49704 RVA: 0x002B6C7E File Offset: 0x002B4E7E
		protected void ClearRenderColumns()
		{
			this._renderColumns = null;
		}

		// Token: 0x0600C229 RID: 49705 RVA: 0x002B6C88 File Offset: 0x002B4E88
		internal void EnsureAutoGeneratedColumnsInternal()
		{
			if (this._autoGeneratedColumns == null)
			{
				this._autoGeneratedColumns = new List<TreeListColumn>();
				if (this.IsDataBinding && this.AutoGenerateColumns)
				{
					if (this.ResolvedDataSource.Columns == null)
					{
						return;
					}
					foreach (KeyValuePair<string, PropertyDescriptor> keyValuePair in this.ResolvedDataSource.Columns)
					{
						TreeListColumn treeListColumn = this.CreateColumnByDataType(keyValuePair.Value.PropertyType);
						TreeListDataColumn treeListDataColumn = treeListColumn as TreeListDataColumn;
						if (treeListDataColumn != null)
						{
							treeListDataColumn.DataField = keyValuePair.Key;
							treeListDataColumn.DataType = keyValuePair.Value.PropertyType;
							if (this._previousAutoGeneratedColumns != null && this.PersistAutoGenerateColumnsStateOnRebind)
							{
								foreach (TreeListDataColumn treeListDataColumn2 in this._previousAutoGeneratedColumns)
								{
									if (treeListDataColumn2.DataField == treeListDataColumn.DataField && treeListDataColumn2.DataType == treeListDataColumn.DataType)
									{
										((IStateManager)treeListDataColumn).LoadViewState(((IStateManager)treeListDataColumn2).SaveViewState());
										break;
									}
								}
							}
						}
						treeListColumn.UniqueName = keyValuePair.Key;
						treeListColumn.HeaderText = keyValuePair.Key;
						treeListColumn.SetDirty();
						this._autoGeneratedColumns.Add(treeListColumn);
						this.CallOnAutoGeneratedColumnCreated(treeListColumn);
					}
				}
				return;
			}
		}

		// Token: 0x17003E9B RID: 16027
		// (get) Token: 0x0600C22A RID: 49706 RVA: 0x002B6E24 File Offset: 0x002B5024
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Browsable(false)]
		public virtual TreeListColumn[] AutoGeneratedColumns
		{
			get
			{
				this.EnsureAutoGeneratedColumnsInternal();
				return this._autoGeneratedColumns.ToArray();
			}
		}

		// Token: 0x0600C22B RID: 49707 RVA: 0x002B6E38 File Offset: 0x002B5038
		protected void ClearAutoGeneratedColumns()
		{
			if (this._autoGeneratedColumns != null && this.PersistAutoGenerateColumnsStateOnRebind)
			{
				this._previousAutoGeneratedColumns = new List<TreeListDataColumn>();
				foreach (TreeListColumn treeListColumn in this._autoGeneratedColumns)
				{
					TreeListDataColumn treeListDataColumn = treeListColumn as TreeListDataColumn;
					if (treeListDataColumn != null)
					{
						this._previousAutoGeneratedColumns.Add(treeListDataColumn);
					}
				}
			}
			this._autoGeneratedColumns = null;
		}

		// Token: 0x0600C22C RID: 49708 RVA: 0x002B6EBC File Offset: 0x002B50BC
		internal void UpdateColumnsDataType()
		{
			if (this.ResolvedDataSource.Columns == null)
			{
				return;
			}
			foreach (TreeListColumn treeListColumn in this.Columns)
			{
				TreeListDataColumn treeListDataColumn = treeListColumn as TreeListDataColumn;
				if (treeListDataColumn != null && !string.IsNullOrEmpty(treeListDataColumn.DataField) && !treeListDataColumn.DataTypeIsSet && this.ResolvedDataSource.Columns.ContainsKey(treeListDataColumn.DataField))
				{
					PropertyDescriptor propertyDescriptor = this.ResolvedDataSource.Columns[treeListDataColumn.DataField];
					treeListDataColumn.DataType = propertyDescriptor.PropertyType;
				}
			}
		}

		// Token: 0x17003E9C RID: 16028
		// (get) Token: 0x0600C22D RID: 49709 RVA: 0x002B6F6C File Offset: 0x002B516C
		// (set) Token: 0x0600C22E RID: 49710 RVA: 0x002B6F74 File Offset: 0x002B5174
		[TemplateContainer(typeof(TreeListDetailTemplateItem), BindingDirection.TwoWay)]
		[Browsable(false)]
		[DefaultValue(null)]
		[Description("RadTreeList NestedViewTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate DetailTemplate
		{
			get
			{
				return this.detailTemplate;
			}
			set
			{
				this.detailTemplate = value;
			}
		}

		// Token: 0x0600C22F RID: 49711 RVA: 0x002B6F80 File Offset: 0x002B5180
		internal TreeListDataItem GetParentDataItem(TreeListEditableItem item)
		{
			TreeListEditFormItem treeListEditFormItem = item as TreeListEditFormItem;
			ITreeListInsertItem treeListInsertItem = item as ITreeListInsertItem;
			TreeListDataItem result;
			if (treeListEditFormItem != null)
			{
				result = treeListEditFormItem.ParentItem;
			}
			else if (treeListInsertItem != null)
			{
				result = treeListInsertItem.ParentItem;
			}
			else
			{
				result = (TreeListDataItem)item;
			}
			return result;
		}

		// Token: 0x0600C230 RID: 49712 RVA: 0x002B6FC0 File Offset: 0x002B51C0
		internal ITreeListInsertItem GetRootInsertItem()
		{
			if (this.IsItemInserted)
			{
				TreeListItem[] items = this.GetItems(new TreeListItemType[]
				{
					TreeListItemType.EditItem,
					TreeListItemType.EditFormItem
				});
				foreach (TreeListItem treeListItem in items)
				{
					ITreeListInsertItem treeListInsertItem = treeListItem as ITreeListInsertItem;
					if (treeListInsertItem != null && treeListInsertItem.IsRoot)
					{
						return treeListInsertItem;
					}
				}
			}
			return null;
		}

		// Token: 0x0600C231 RID: 49713 RVA: 0x002B7030 File Offset: 0x002B5230
		private void FillDataKeys(IDictionary keys, TreeListEditableItem item)
		{
			TreeListDataItem parentDataItem = this.GetParentDataItem(item);
			if (parentDataItem == null)
			{
				return;
			}
			foreach (string key in this.DataKeyNames)
			{
				keys[key] = this.DataKeyValues[parentDataItem.DisplayIndex][key];
			}
		}

		// Token: 0x0600C232 RID: 49714 RVA: 0x002B7080 File Offset: 0x002B5280
		internal static void ExtractParentDataKeyValues(IDictionary values, ITreeListInsertItem insertItem)
		{
			if (insertItem != null && insertItem.ParentItem != null)
			{
				int num = 0;
				RadTreeList ownerTreeList = insertItem.ParentItem.OwnerTreeList;
				foreach (string key in ownerTreeList.ParentDataKeyNames)
				{
					object value = ownerTreeList.DataKeyValues[insertItem.ParentItem.DisplayIndex][ownerTreeList.DataKeyNames[num++]];
					if (values.Contains(key))
					{
						values[key] = value;
					}
					else
					{
						values.Add(key, value);
					}
				}
			}
		}

		// Token: 0x0600C233 RID: 49715 RVA: 0x002B710C File Offset: 0x002B530C
		protected internal void PopulateDataKeys(object dataItem)
		{
			if (this.IsDesignMode || (this.DataKeyNamesInternal.Length == 0 && this.ParentDataKeyNamesInternal.Length == 0))
			{
				return;
			}
			DataKey dataKey = new DataKey(base.IsTrackingViewState);
			this.DataKeysArrayList.Add(dataKey);
			DataKey dataKey2 = new DataKey(base.IsTrackingViewState);
			this.ParentDataKeysArrayList.Add(dataKey2);
			DataKey dataKey3 = new DataKey(base.IsTrackingViewState);
			this.ClientDataKeysArrayList.Add(dataKey3);
			try
			{
				foreach (string text in this.DataKeyNamesInternal)
				{
					dataKey[text] = this.ExtractDataKeyValue(dataItem, text);
				}
				foreach (string text2 in this.ParentDataKeyNamesInternal)
				{
					dataKey2[text2] = this.ExtractDataKeyValue(dataItem, text2);
				}
				foreach (string text3 in this.ClientDataKeyNamesInternal)
				{
					dataKey3[text3] = this.ExtractDataKeyValue(dataItem, text3);
				}
			}
			catch (ArgumentNullException innerException)
			{
				throw new ArgumentException("There was a problem extracting DataKeyValues from the DataSource. Please ensure that DataKeyNames are specified correctly and all fields specified exist in the DataSource.", innerException);
			}
			catch (HttpException innerException2)
			{
				throw new ArgumentException("There was a problem extracting DataKeyValues from the DataSource. Please ensure that DataKeyNames are specified correctly and all fields specified exist in the DataSource.", innerException2);
			}
		}

		// Token: 0x0600C234 RID: 49716 RVA: 0x002B7250 File Offset: 0x002B5450
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal object ExtractDataKeyValue(object dataItem, string name)
		{
			object obj;
			if (dataItem is DataRowView)
			{
				obj = ((DataRowView)dataItem)[name];
			}
			else if (dataItem is DataRow)
			{
				obj = ((DataRow)dataItem)[name];
			}
			else
			{
				if (name.Contains("."))
				{
					try
					{
						obj = DataBinder.GetPropertyValue(dataItem, name);
						goto IL_58;
					}
					catch
					{
						obj = DataBinder.Eval(dataItem, name);
						goto IL_58;
					}
				}
				obj = DataBinder.GetPropertyValue(dataItem, name);
			}
			IL_58:
			if (obj == DBNull.Value)
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x17003E9D RID: 16029
		// (get) Token: 0x0600C235 RID: 49717 RVA: 0x002B72D0 File Offset: 0x002B54D0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x17003E9E RID: 16030
		// (get) Token: 0x0600C236 RID: 49718 RVA: 0x002B72D8 File Offset: 0x002B54D8
		// (set) Token: 0x0600C237 RID: 49719 RVA: 0x002B72E0 File Offset: 0x002B54E0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal ExportFormat? CurrentExportFormat { get; set; }

		// Token: 0x17003E9F RID: 16031
		// (get) Token: 0x0600C238 RID: 49720 RVA: 0x002B72EC File Offset: 0x002B54EC
		private string[] ClientDataKeyNamesInternal
		{
			get
			{
				object obj = this.ViewState["ClientDataKeyNames"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
		}

		// Token: 0x17003EA0 RID: 16032
		// (get) Token: 0x0600C239 RID: 49721 RVA: 0x002B731A File Offset: 0x002B551A
		private List<DataKey> ClientDataKeysArrayList
		{
			get
			{
				if (this._clientDataKeysArrayList == null)
				{
					this._clientDataKeysArrayList = new List<DataKey>();
				}
				return this._clientDataKeysArrayList;
			}
		}

		// Token: 0x17003EA1 RID: 16033
		// (get) Token: 0x0600C23A RID: 49722 RVA: 0x002B7338 File Offset: 0x002B5538
		private string[] DataKeyNamesInternal
		{
			get
			{
				object obj = this.ViewState["DataKeyNames"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
		}

		// Token: 0x17003EA2 RID: 16034
		// (get) Token: 0x0600C23B RID: 49723 RVA: 0x002B7366 File Offset: 0x002B5566
		private List<DataKey> DataKeysArrayList
		{
			get
			{
				if (this._dataKeysArrayList == null)
				{
					this._dataKeysArrayList = new List<DataKey>();
				}
				return this._dataKeysArrayList;
			}
		}

		// Token: 0x17003EA3 RID: 16035
		// (get) Token: 0x0600C23C RID: 49724 RVA: 0x002B7384 File Offset: 0x002B5584
		private string[] ParentDataKeyNamesInternal
		{
			get
			{
				object obj = this.ViewState["ParentDataKeyNames"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
		}

		// Token: 0x17003EA4 RID: 16036
		// (get) Token: 0x0600C23D RID: 49725 RVA: 0x002B73B2 File Offset: 0x002B55B2
		private List<DataKey> ParentDataKeysArrayList
		{
			get
			{
				if (this._parentDataKeysArrayList == null)
				{
					this._parentDataKeysArrayList = new List<DataKey>();
				}
				return this._parentDataKeysArrayList;
			}
		}

		// Token: 0x17003EA5 RID: 16037
		// (get) Token: 0x0600C23E RID: 49726 RVA: 0x002B73CD File Offset: 0x002B55CD
		internal bool UsesControlState
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x0600C23F RID: 49727 RVA: 0x002B73D8 File Offset: 0x002B55D8
		protected virtual Pair SaveAutoGeneratedColumnsState()
		{
			Pair pair = new Pair();
			ArrayList arrayList = new ArrayList();
			pair.Second = arrayList;
			if (this._autoGeneratedColumns == null)
			{
				pair.First = 0;
				return pair;
			}
			pair.First = this._autoGeneratedColumns.Count;
			foreach (TreeListColumn treeListColumn in this._autoGeneratedColumns)
			{
				arrayList.Add(new Pair
				{
					First = treeListColumn.ColumnType,
					Second = ((IStateManager)treeListColumn).SaveViewState()
				});
			}
			return pair;
		}

		// Token: 0x0600C240 RID: 49728 RVA: 0x002B748C File Offset: 0x002B568C
		protected virtual void LoadAutoGeneratedColumnsState(object columnsState)
		{
			Pair pair = columnsState as Pair;
			int capacity = (int)pair.First;
			this._autoGeneratedColumns = new List<TreeListColumn>(capacity);
			ArrayList arrayList = pair.Second as ArrayList;
			foreach (object obj in arrayList)
			{
				Pair pair2 = obj as Pair;
				TreeListColumn treeListColumn = this.CreateColumnByType(pair2.First.ToString());
				if (treeListColumn != null)
				{
					this._autoGeneratedColumns.Add(treeListColumn);
					((IStateManager)treeListColumn).LoadViewState(pair2.Second);
					this.CallOnAutoGeneratedColumnCreated(treeListColumn);
					treeListColumn.SetDirty();
				}
			}
		}

		// Token: 0x0600C241 RID: 49729 RVA: 0x002B7550 File Offset: 0x002B5750
		internal TreeListColumn CreateColumnByType(string columnType)
		{
			TreeListColumn treeListColumn = null;
			if (columnType.IndexOf("TreeListBoundColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListBoundColumn();
			}
			else if (columnType.IndexOf("TreeListCheckBoxColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListCheckBoxColumn();
			}
			else if (columnType.IndexOf("TreeListSelectColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListSelectColumn();
			}
			else if (columnType.IndexOf("TreeListTemplateColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListTemplateColumn();
			}
			else if (columnType.IndexOf("TreeListButtonColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListButtonColumn();
			}
			else if (columnType.IndexOf("TreeListNumericColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListNumericColumn();
			}
			else if (columnType.IndexOf("TreeListDateTimeColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListDateTimeColumn();
			}
			else if (columnType.IndexOf("TreeListHyperLinkColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListHyperLinkColumn();
			}
			else if (columnType.IndexOf("TreeListImageColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListImageColumn();
			}
			else if (columnType.IndexOf("TreeListEditCommandColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListEditCommandColumn();
			}
			else if (columnType.IndexOf("TreeListButtonColumn", StringComparison.CurrentCulture) > -1)
			{
				treeListColumn = new TreeListButtonColumn();
			}
			else if (columnType.IndexOf("TreeListCalculatedColumn") > -1)
			{
				treeListColumn = new TreeListCalculatedColumn();
			}
			else
			{
				TreeListCreateCustomColumnEventArgs treeListCreateCustomColumnEventArgs = new TreeListCreateCustomColumnEventArgs(treeListColumn, columnType);
				this.CallOnCreateCustomColumn(treeListCreateCustomColumnEventArgs);
				treeListColumn = treeListCreateCustomColumnEventArgs.Column;
			}
			if (treeListColumn == null)
			{
				throw new Exception(string.Format(CultureInfo.CurrentCulture, "Invalid column type: \"{0}\".", new object[]
				{
					columnType
				}));
			}
			treeListColumn.SetOwner(this);
			return treeListColumn;
		}

		// Token: 0x0600C242 RID: 49730 RVA: 0x002B76CC File Offset: 0x002B58CC
		internal TreeListColumn CreateColumnByDataType(Type dataType)
		{
			if (dataType == typeof(bool))
			{
				return this.CreateColumnByType(typeof(TreeListCheckBoxColumn).Name);
			}
			if (TreeListTypeHelper.GetNumericTypeKind(dataType) == 1)
			{
				return this.CreateColumnByType(typeof(TreeListNumericColumn).Name);
			}
			if (TreeListTypeHelper.IsDateType(dataType))
			{
				return this.CreateColumnByType(typeof(TreeListDateTimeColumn).Name);
			}
			return this.CreateColumnByType(typeof(TreeListBoundColumn).Name);
		}

		// Token: 0x0600C243 RID: 49731 RVA: 0x002B7753 File Offset: 0x002B5953
		internal void SaveEditIndexState(TreeListHierarchyIndex index)
		{
			if (!this.AllowMultiItemEdit)
			{
				this.EditIndexes.Clear();
				this.InsertIndexes.Clear();
			}
			this.EditIndexes.Add(index);
		}

		// Token: 0x0600C244 RID: 49732 RVA: 0x002B777F File Offset: 0x002B597F
		internal void RemoveEditIndexState(TreeListHierarchyIndex index)
		{
			this.EditIndexes.Remove(index);
		}

		// Token: 0x0600C245 RID: 49733 RVA: 0x002B778E File Offset: 0x002B598E
		internal void SaveInsertIndexState(TreeListHierarchyIndex index)
		{
			if (!this.AllowMultiItemEdit)
			{
				this.EditIndexes.Clear();
				this.InsertIndexes.Clear();
			}
			this.InsertIndexes.Add(index);
		}

		// Token: 0x0600C246 RID: 49734 RVA: 0x002B77BA File Offset: 0x002B59BA
		internal void RemoveInsertIndexState(TreeListHierarchyIndex index)
		{
			this.InsertIndexes.Remove(index);
		}

		// Token: 0x0600C247 RID: 49735 RVA: 0x002B77CC File Offset: 0x002B59CC
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(base.SaveViewState());
			arrayList.Add(this.SaveAutoGeneratedColumnsState());
			arrayList.Add(((IStateManager)this.Columns).SaveViewState());
			arrayList.Add(((IStateManager)this.ClientSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.PagerStyle).SaveViewState());
			if (this._headerStyle != null)
			{
				arrayList.Add(((IStateManager)this._headerStyle).SaveViewState());
			}
			else
			{
				arrayList.Add(null);
			}
			if (this._itemStyle != null)
			{
				arrayList.Add(((IStateManager)this._itemStyle).SaveViewState());
			}
			else
			{
				arrayList.Add(null);
			}
			if (this._footerItemStyle != null)
			{
				arrayList.Add(((IStateManager)this._footerItemStyle).SaveViewState());
			}
			else
			{
				arrayList.Add(null);
			}
			if (this._alternatingItemStyle != null)
			{
				arrayList.Add(((IStateManager)this._alternatingItemStyle).SaveViewState());
			}
			else
			{
				arrayList.Add(null);
			}
			if (this._selectedItemStyle != null)
			{
				arrayList.Add(((IStateManager)this._selectedItemStyle).SaveViewState());
			}
			else
			{
				arrayList.Add(null);
			}
			if (this._editItemStyle != null)
			{
				arrayList.Add(((IStateManager)this._editItemStyle).SaveViewState());
			}
			else
			{
				arrayList.Add(null);
			}
			arrayList.Add(((IStateManager)this.SortingSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.EditFormSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.ValidationSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.ExportSettings).SaveViewState());
			if (!this.UsesControlState)
			{
				this.SaveControlStateObject(arrayList);
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600C248 RID: 49736 RVA: 0x002B7970 File Offset: 0x002B5B70
		protected virtual void SaveControlStateObject(IList state)
		{
			state.Add(((IStateManager)this.ControlState).SaveViewState());
			state.Add(((IStateManager)this.DataKeyValues).SaveViewState());
			state.Add(((IStateManager)this.ParentDataKeyValues).SaveViewState());
			state.Add(((IStateManager)this.ClientDataKeyValues).SaveViewState());
			state.Add(((IStateManager)this.SortExpressions).SaveViewState());
		}

		// Token: 0x0600C249 RID: 49737 RVA: 0x002B79D8 File Offset: 0x002B5BD8
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int index = 0;
				base.LoadViewState(array[index++]);
				this.LoadAutoGeneratedColumnsState(array[index++]);
				((IStateManager)this.Columns).LoadViewState(array[index++]);
				((IStateManager)this.ClientSettings).LoadViewState(array[index++]);
				((IStateManager)this.PagerStyle).LoadViewState(array[index++]);
				((IStateManager)this.HeaderStyle).LoadViewState(array[index++]);
				((IStateManager)this.ItemStyle).LoadViewState(array[index++]);
				((IStateManager)this.AlternatingItemStyle).LoadViewState(array[index++]);
				((IStateManager)this.FooterItemStyle).LoadViewState(array[index++]);
				((IStateManager)this.SelectedItemStyle).LoadViewState(array[index++]);
				((IStateManager)this.EditItemStyle).LoadViewState(array[index++]);
				((IStateManager)this.SortingSettings).LoadViewState(array[index++]);
				((IStateManager)this.EditFormSettings).LoadViewState(array[index++]);
				((IStateManager)this.ValidationSettings).LoadViewState(array[index++]);
				((IStateManager)this.ExportSettings).LoadViewState(array[index++]);
				if (!this.UsesControlState)
				{
					this.LoadControlStateObject(array, index);
				}
			}
		}

		// Token: 0x0600C24A RID: 49738 RVA: 0x002B7B08 File Offset: 0x002B5D08
		protected virtual void LoadControlStateObject(object[] state, int index)
		{
			((IStateManager)this.ControlState).LoadViewState(state[index++]);
			((IStateManager)this.DataKeyValues).LoadViewState(state[index++]);
			((IStateManager)this.ParentDataKeyValues).LoadViewState(state[index++]);
			((IStateManager)this.ClientDataKeyValues).LoadViewState(state[index++]);
			((IStateManager)this.SortExpressions).LoadViewState(state[index++]);
		}

		// Token: 0x0600C24B RID: 49739 RVA: 0x002B7B74 File Offset: 0x002B5D74
		protected override void TrackViewState()
		{
			if (base.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			((IStateManager)this.DataKeyValues).TrackViewState();
			((IStateManager)this.ParentDataKeyValues).TrackViewState();
			((IStateManager)this.ClientDataKeyValues).TrackViewState();
			((IStateManager)this.Columns).TrackViewState();
			((IStateManager)this.ClientSettings).TrackViewState();
			((IStateManager)this.PagerStyle).TrackViewState();
			if (this._headerStyle != null)
			{
				((IStateManager)this._headerStyle).TrackViewState();
			}
			if (this._footerItemStyle != null)
			{
				((IStateManager)this._footerItemStyle).TrackViewState();
			}
			if (this._itemStyle != null)
			{
				((IStateManager)this._itemStyle).TrackViewState();
			}
			if (this._alternatingItemStyle != null)
			{
				((IStateManager)this._alternatingItemStyle).TrackViewState();
			}
			if (this._selectedItemStyle != null)
			{
				((IStateManager)this._selectedItemStyle).TrackViewState();
			}
			if (this._editItemStyle != null)
			{
				((IStateManager)this._editItemStyle).TrackViewState();
			}
			if (this._sortingSettings != null)
			{
				((IStateManager)this.SortingSettings).TrackViewState();
			}
			if (this._editFormSettings != null)
			{
				((IStateManager)this.EditFormSettings).TrackViewState();
			}
			if (this._validationSettings != null)
			{
				((IStateManager)this.ValidationSettings).TrackViewState();
			}
			((IStateManager)this.ExportSettings).TrackViewState();
		}

		// Token: 0x0600C24C RID: 49740 RVA: 0x002B7C90 File Offset: 0x002B5E90
		protected override object SaveControlState()
		{
			object value = base.SaveControlState();
			ArrayList arrayList = new ArrayList();
			arrayList.Add(value);
			this.SaveControlStateObject(arrayList);
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600C24D RID: 49741 RVA: 0x002B7CCC File Offset: 0x002B5ECC
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

		// Token: 0x17003EA6 RID: 16038
		// (get) Token: 0x0600C24E RID: 49742 RVA: 0x002B7CFA File Offset: 0x002B5EFA
		internal TreeListControlStateManager ControlState
		{
			get
			{
				if (this._controlStateManager == null)
				{
					this._controlStateManager = new TreeListControlStateManager();
				}
				return this._controlStateManager;
			}
		}

		// Token: 0x17003EA7 RID: 16039
		// (get) Token: 0x0600C24F RID: 49743 RVA: 0x002B7D15 File Offset: 0x002B5F15
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600C250 RID: 49744 RVA: 0x002B7D19 File Offset: 0x002B5F19
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderMode == RenderMode.Mobile)
			{
				writer.AddStyleAttribute("overflow", "auto");
			}
		}

		// Token: 0x0600C251 RID: 49745 RVA: 0x002B7D3C File Offset: 0x002B5F3C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			BaseClass.RenderVersionStamp(writer);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			if (this.ClientSettings.Scrolling.AllowScroll && !this.ClientSettings.Scrolling.UseStaticHeaders)
			{
				if (base.RenderMode == RenderMode.Mobile)
				{
					writer.WriteLine("\t<div id='{0}_rtlData' class='rtlDataDiv' style='float: left; height: {1};'>\r\n", this.ClientID, this.Height.IsEmpty ? this.ClientSettings.Scrolling.ScrollHeight : this.Height);
				}
				else
				{
					writer.WriteLine("\t<div id='{0}_rtlData' class='rtlDataDiv' style='overflow: auto; height: {1};'>\r\n", this.ClientID, this.Height.IsEmpty ? this.ClientSettings.Scrolling.ScrollHeight : this.Height);
				}
			}
			base.RenderContents(writer);
			if (this.ClientSettings.Scrolling.AllowScroll && !this.ClientSettings.Scrolling.UseStaticHeaders)
			{
				writer.WriteLine("\r\n\t</div>");
			}
		}

		// Token: 0x0600C252 RID: 49746 RVA: 0x002B7E4C File Offset: 0x002B604C
		private void PrepareStaticRows()
		{
			TreeListTable staticTreeListTable = this.GetStaticTreeListTable();
			if (staticTreeListTable != null)
			{
				foreach (object obj in staticTreeListTable.Rows)
				{
					TreeListItem treeListItem = (TreeListItem)obj;
					treeListItem.PrepareItemStyle();
				}
			}
			TreeListItemDecorator.PrepareDataItemsServiceCells(this);
		}

		// Token: 0x0600C253 RID: 49747 RVA: 0x002B7EB4 File Offset: 0x002B60B4
		private void PrepareRows()
		{
			TreeListTable treeListTable = this.GetTreeListTable();
			if (treeListTable != null)
			{
				foreach (object obj in treeListTable.Rows)
				{
					TreeListItem treeListItem = (TreeListItem)obj;
					treeListItem.PrepareItemStyle();
				}
			}
			TreeListItemDecorator.PrepareDataItemsServiceCells(this);
		}

		// Token: 0x0600C254 RID: 49748 RVA: 0x002B7F1C File Offset: 0x002B611C
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			if (this.ClientSettings.AllowKeyboardNavigation)
			{
				this.SetControlToFocus();
			}
			if (this.Rebound && this.ExpandCollapseMode == TreeListExpandCollapseMode.Client)
			{
				this.ExpandAllItems();
			}
		}

		// Token: 0x0600C255 RID: 49749 RVA: 0x002B7F50 File Offset: 0x002B6150
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.IsDesignMode && (this.DataKeyNames.Length == 0 || this.ParentDataKeyNames.Length == 0))
			{
				writer.Write("<div><p>You need to set the <b>DataKeyNames</b> and the <b>ParentDataKeyNames</b> properties</p></div>");
			}
			this.SetStyleClasses();
			this.PrepareRows();
			this.PrepareStaticRows();
			base.Render(writer);
		}

		// Token: 0x0600C256 RID: 49750 RVA: 0x002B7FA0 File Offset: 0x002B61A0
		private void SetControlToFocus()
		{
			new TreeListEditItemCollection();
			switch (this.focusItemType)
			{
			case TreeListFocusItemType.EditItem:
				if (this.EditItems.Count > 0)
				{
					TreeListEditableItem item = null;
					if (!this.AllowMultiItemEdit)
					{
						item = this.EditItems[this.EditItems.Count - 1];
					}
					else
					{
						foreach (TreeListEditableItem treeListEditableItem in this.EditItems)
						{
							TreeListEditFormItem treeListEditFormItem = treeListEditableItem as TreeListEditFormItem;
							if (treeListEditFormItem != null && treeListEditFormItem.ParentItem != null && treeListEditFormItem.ParentItem.DisplayIndex == this.focusItemIndex)
							{
								item = treeListEditFormItem;
								break;
							}
							TreeListDataItem treeListDataItem = treeListEditableItem as TreeListDataItem;
							if (treeListDataItem != null && treeListDataItem.DisplayIndex == this.focusItemIndex)
							{
								item = treeListDataItem;
								break;
							}
						}
					}
					this.FocusControlInItem(item);
					return;
				}
				break;
			case TreeListFocusItemType.InsertItem:
				if (this.InsertItems.Count > 0)
				{
					TreeListEditableItem item2 = null;
					if (!this.AllowMultiItemEdit)
					{
						item2 = this.InsertItems[this.InsertItems.Count - 1];
					}
					else
					{
						foreach (TreeListEditableItem treeListEditableItem2 in this.InsertItems)
						{
							TreeListEditFormInsertItem treeListEditFormInsertItem = treeListEditableItem2 as TreeListEditFormInsertItem;
							if (treeListEditFormInsertItem != null && treeListEditFormInsertItem.ParentItem != null && treeListEditFormInsertItem.ParentItem.DisplayIndex == this.focusItemIndex)
							{
								item2 = treeListEditFormInsertItem;
								break;
							}
							TreeListDataInsertItem treeListDataInsertItem = treeListEditableItem2 as TreeListDataInsertItem;
							if (treeListDataInsertItem != null && treeListDataInsertItem.ParentItem != null && treeListDataInsertItem.ParentItem.DisplayIndex == this.focusItemIndex)
							{
								item2 = treeListDataInsertItem;
								break;
							}
						}
					}
					this.FocusControlInItem(item2);
					return;
				}
				break;
			case TreeListFocusItemType.RootInsertItem:
				if (this.IsItemInserted)
				{
					TreeListEditableItem item3 = (TreeListEditableItem)this.GetRootInsertItem();
					this.FocusControlInItem(item3);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600C257 RID: 49751 RVA: 0x002B8194 File Offset: 0x002B6394
		private void FocusControlInItem(TreeListEditableItem item)
		{
			if (item == null)
			{
				return;
			}
			foreach (TreeListColumn treeListColumn in item.OwnerTreeList.RenderColumns)
			{
				TreeListEditableColumn treeListEditableColumn = treeListColumn as TreeListEditableColumn;
				if (treeListEditableColumn != null && !treeListEditableColumn.ReadOnly)
				{
					TableCell tableCell = null;
					TreeListEditFormItem treeListEditFormItem = item as TreeListEditFormItem;
					if (treeListEditFormItem != null)
					{
						if (treeListEditFormItem.EditFormCell != null)
						{
							tableCell = treeListEditFormItem[treeListColumn.UniqueName];
						}
					}
					else
					{
						if (!treeListColumn.Visible)
						{
							goto IL_11E;
						}
						TreeListDataItem treeListDataItem = item as TreeListDataItem;
						if (treeListDataItem != null)
						{
							tableCell = treeListDataItem[treeListColumn.UniqueName];
						}
						TreeListDataInsertItem treeListDataInsertItem = item as TreeListDataInsertItem;
						if (treeListDataInsertItem != null)
						{
							tableCell = treeListDataInsertItem[treeListColumn.UniqueName];
						}
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
				IL_11E:;
			}
		}

		// Token: 0x0600C258 RID: 49752 RVA: 0x002B82E0 File Offset: 0x002B64E0
		internal string FormatCssClass(string prefix, string userDefined)
		{
			string text = prefix;
			if (prefix == "RadTreeList")
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
			if (prefix == "RadTreeListRTL")
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

		// Token: 0x0600C259 RID: 49753 RVA: 0x002B83A0 File Offset: 0x002B65A0
		protected virtual void SetStyleClasses()
		{
			if (this.Dir == TreeListTextDirection.RTL)
			{
				this.CssClass = this.FormatCssClass("RadTreeListRTL", this.CssClass);
				if (!this.ShowOuterBorders)
				{
					this.CssClass = this.FormatCssClass("RadTreeListNoBorderRTL", this.CssClass);
				}
			}
			if (!this.ShowOuterBorders)
			{
				this.CssClass = this.FormatCssClass("RadTreeListNoBorder", this.CssClass);
			}
			this.CssClass = this.FormatCssClass("RadTreeList", this.CssClass);
			this.PagerStyle.CssClass = this.FormatCssClass("rtlPager", this.PagerStyle.CssClass);
			this.CommandItemStyle.CssClass = this.FormatCssClass("rtlCommand", this.CommandItemStyle.CssClass);
			this.ItemStyle.CssClass = this.FormatCssClass("rtlR", this.ItemStyle.CssClass);
			this.FooterItemStyle.CssClass = this.FormatCssClass("rtlRFooter", this.FooterItemStyle.CssClass);
			this.AlternatingItemStyle.CssClass = this.FormatCssClass("rtlA", this.AlternatingItemStyle.CssClass);
			this.SelectedItemStyle.CssClass = this.FormatCssClass("rtlRSel", this.SelectedItemStyle.CssClass);
			this.EditItemStyle.CssClass = this.FormatCssClass("rtlREdit", this.EditItemStyle.CssClass);
		}

		// Token: 0x17003EA8 RID: 16040
		// (get) Token: 0x0600C25A RID: 49754 RVA: 0x002B8509 File Offset: 0x002B6709
		internal TreeListMobileExportView ExportView
		{
			get
			{
				if (this.exportView == null)
				{
					this.exportView = new TreeListMobileExportView(this);
					this.exportView.ID = "ExportView" + this.ID;
				}
				return this.exportView;
			}
		}

		// Token: 0x17003EA9 RID: 16041
		// (get) Token: 0x0600C25B RID: 49755 RVA: 0x002B8540 File Offset: 0x002B6740
		internal TreeListMobileColumnsView ColumnsView
		{
			get
			{
				if (this.columnsView == null)
				{
					this.columnsView = new TreeListMobileColumnsView(this);
					this.columnsView.ID = "ColumnsView" + this.ID;
				}
				return this.columnsView;
			}
		}

		// Token: 0x0600C25C RID: 49756 RVA: 0x002B8578 File Offset: 0x002B6778
		protected void CreateMobileViews()
		{
			if (this.FindControl(this.ColumnsView.ID) == null)
			{
				this.Controls.Add(this.ColumnsView);
			}
			if (this.FindControl(this.ExportView.ID) == null)
			{
				this.Controls.Add(this.ExportView);
			}
		}

		// Token: 0x0600C25D RID: 49757 RVA: 0x002B85D0 File Offset: 0x002B67D0
		internal TreeListTable GetStaticTreeListTable()
		{
			foreach (object obj in this.Controls)
			{
				TreeListTable treeListTable = obj as TreeListTable;
				if (treeListTable != null && treeListTable.RenderStaticHeadersOnly)
				{
					return treeListTable;
				}
			}
			return null;
		}

		// Token: 0x0600C25E RID: 49758 RVA: 0x002B863C File Offset: 0x002B683C
		internal TreeListTable GetTreeListTable()
		{
			foreach (object obj in this.Controls)
			{
				TreeListTable treeListTable = obj as TreeListTable;
				if (treeListTable != null && !treeListTable.RenderStaticHeadersOnly)
				{
					return treeListTable;
				}
			}
			return null;
		}

		// Token: 0x0600C25F RID: 49759 RVA: 0x002B86A8 File Offset: 0x002B68A8
		protected virtual void AutoDataBind(TreeListRebindReason rebindReason)
		{
			if (!this.Visible && (rebindReason & TreeListRebindReason.ExplicitRebind) != TreeListRebindReason.ExplicitRebind)
			{
				return;
			}
			this.ObtainDataSource(rebindReason, base.IsBoundUsingDataSourceID);
			if ((this.DataSource != null && !base.IsBoundUsingDataSourceID) || (this.IsUsingModelBinding && rebindReason == TreeListRebindReason.ExplicitRebind) || (base.IsBoundUsingDataSourceID && rebindReason == TreeListRebindReason.ExplicitRebind) || (this.DataSource != null && rebindReason == TreeListRebindReason.ExplicitRebind))
			{
				this.Rebound = true;
				this.DataBind();
			}
		}

		// Token: 0x0600C260 RID: 49760 RVA: 0x002B8712 File Offset: 0x002B6912
		internal void ObtainDataSource(TreeListRebindReason rebindReason, bool isBoundUsingDataSourceId)
		{
			if (!this.DataSourceIsAssigned && !isBoundUsingDataSourceId)
			{
				this.OnNeedDataSource(new TreeListNeedDataSourceEventArgs(rebindReason));
			}
		}

		// Token: 0x0600C261 RID: 49761 RVA: 0x002B872B File Offset: 0x002B692B
		internal void ObtainDataSource(TreeListRebindReason rebindReason)
		{
			this.ObtainDataSource(rebindReason, base.IsBoundUsingDataSourceID);
		}

		// Token: 0x17003EAA RID: 16042
		// (get) Token: 0x0600C262 RID: 49762 RVA: 0x002B873A File Offset: 0x002B693A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool DataSourceIsAssigned
		{
			get
			{
				return this.DataSource != null || base.IsBoundUsingDataSourceID;
			}
		}

		// Token: 0x17003EAB RID: 16043
		// (get) Token: 0x0600C263 RID: 49763 RVA: 0x002B874C File Offset: 0x002B694C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal TreeListItemStateCollection ItemState
		{
			get
			{
				if (this._itemState == null)
				{
					this._itemState = (TreeListItemStateCollection)this.ControlState["_!ItemState"];
					if (this._itemState == null)
					{
						this._itemState = new TreeListItemStateCollection();
						this.ControlState["_!ItemState"] = this._itemState;
					}
				}
				return this._itemState;
			}
		}

		// Token: 0x0600C264 RID: 49764 RVA: 0x002B87AC File Offset: 0x002B69AC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			bool result = false;
			if (args is TreeListCommandEventArgs)
			{
				TreeListCommandEventArgs e = (TreeListCommandEventArgs)args;
				this.OnItemCommand(e);
				result = true;
			}
			if (args is ITreeListCommandEvent)
			{
				ITreeListCommandEvent treeListCommandEvent = (ITreeListCommandEvent)args;
				if (!treeListCommandEvent.Canceled)
				{
					treeListCommandEvent.ExecuteCommand(source);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600C265 RID: 49765 RVA: 0x002B87F4 File Offset: 0x002B69F4
		public void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.Contains("FireCommand:"))
			{
				this.HandleClientFireCommand(RadTreeList.parseFireCommandEventName(eventArgument), RadTreeList.parseFireCommandArgs(eventArgument), RadTreeList.parseFireCommandSecondArgs(eventArgument));
			}
		}

		// Token: 0x0600C266 RID: 49766 RVA: 0x002B882C File Offset: 0x002B6A2C
		protected virtual void HandleClientFireCommand(string eventName, string eventArgs, string secondEventArgs)
		{
			int num = -1;
			bool flag = false;
			if (int.TryParse(eventArgs, out num) && num >= 0 && num < this.Items.Count)
			{
				flag = true;
			}
			switch (eventName)
			{
			case "Edit":
				if (flag)
				{
					this.ClientSettings.ActiveRowIndex = num.ToString();
					this.shouldFocusOnPage = true;
					this.focusItemType = TreeListFocusItemType.EditItem;
					this.focusItemIndex = num;
					this.Items[num].FireCommandEvent("Edit", string.Empty);
					return;
				}
				return;
			case "Delete":
				if (flag)
				{
					this.ClearActiveRowIndex(true);
					this.Items[num].FireCommandEvent("Delete", string.Empty);
					return;
				}
				return;
			case "Update":
				if (!flag)
				{
					return;
				}
				this.ClientSettings.ActiveRowIndex = num.ToString();
				this.shouldFocusOnPage = true;
				if (this.EditMode == TreeListEditMode.InPlace)
				{
					this.Items[num].FireCommandEvent("Update", string.Empty);
					return;
				}
				this.FireCommandEventForEditItem(num, "Update");
				return;
			case "Cancel":
				if (flag)
				{
					this.ClientSettings.ActiveRowIndex = num.ToString();
					this.shouldFocusOnPage = true;
					this.Items[num].FireCommandEvent("Cancel", string.Empty);
					return;
				}
				return;
			case "InitInsert":
			{
				if (flag)
				{
					this.ClientSettings.ActiveRowIndex = num.ToString();
					this.shouldFocusOnPage = true;
					this.focusItemType = TreeListFocusItemType.InsertItem;
					this.focusItemIndex = num;
					this.Items[num].FireCommandEvent("InitInsert", string.Empty);
					return;
				}
				if (!string.IsNullOrEmpty(eventArgs))
				{
					return;
				}
				this.shouldFocusOnPage = true;
				this.focusItemType = TreeListFocusItemType.RootInsertItem;
				TreeListItem[] items = this.GetItems(new TreeListItemType[]
				{
					TreeListItemType.HeaderItem
				});
				if (items.Length > 0)
				{
					(items[0] as TreeListHeaderItem).FireCommandEvent("InitInsert", string.Empty);
					return;
				}
				return;
			}
			case "PerformInsert":
			{
				this.ClearActiveRowIndex(true);
				if (flag)
				{
					this.FireCommandEventForInsertItem(num, "PerformInsert");
					return;
				}
				if (!string.IsNullOrEmpty(eventArgs))
				{
					return;
				}
				ITreeListInsertItem rootInsertItem = this.GetRootInsertItem();
				if (rootInsertItem != null)
				{
					this.shouldFocusOnPage = true;
					((TreeListEditableItem)rootInsertItem).FireCommandEvent("PerformInsert", string.Empty);
					return;
				}
				return;
			}
			case "CancelInsert":
			{
				if (flag)
				{
					this.ClientSettings.ActiveRowIndex = num.ToString();
					this.shouldFocusOnPage = true;
					this.FireCommandEventForInsertItem(num, "Cancel");
					return;
				}
				if (!string.IsNullOrEmpty(eventArgs))
				{
					return;
				}
				ITreeListInsertItem rootInsertItem2 = this.GetRootInsertItem();
				if (rootInsertItem2 != null)
				{
					this.shouldFocusOnPage = true;
					((TreeListEditableItem)rootInsertItem2).FireCommandEvent("Cancel", string.Empty);
					return;
				}
				return;
			}
			case "ExpandCollapse":
				if (flag)
				{
					this.ClientSettings.ActiveRowIndex = num.ToString();
					this.shouldFocusOnPage = true;
					this.Items[num].FireCommandEvent("ExpandCollapse", string.Empty);
					return;
				}
				return;
			case "Select":
				if (flag)
				{
					this.ClientSettings.ActiveRowIndex = num.ToString();
					this.shouldFocusOnPage = true;
					this.Items[num].FireCommandEvent("Select", string.Empty);
					return;
				}
				return;
			case "Deselect":
				if (flag)
				{
					this.Items[num].FireCommandEvent("Deselect", string.Empty);
					return;
				}
				return;
			case "SelectAll":
				this.GetItems(new TreeListItemType[]
				{
					TreeListItemType.HeaderItem
				})[0].FireCommandEvent("SelectAll", string.Empty);
				return;
			case "DeselectAll":
				this.GetItems(new TreeListItemType[]
				{
					TreeListItemType.HeaderItem
				})[0].FireCommandEvent("DeselectAll", string.Empty);
				return;
			case "Page":
			{
				TreeListItem[] items2 = this.GetItems(new TreeListItemType[]
				{
					TreeListItemType.PagerItem
				});
				if (items2.Length > 0)
				{
					this.ClearActiveRowIndex(true);
					TreeListPageChangedEventArgs.HandlePaging(items2[0], this, eventArgs);
					return;
				}
				return;
			}
			case "ChangePageSize":
			{
				int pageSize;
				if (int.TryParse(eventArgs, out pageSize))
				{
					this.ClearActiveRowIndex(true);
					this.PageSize = pageSize;
					this.Rebind();
					return;
				}
				return;
			}
			case "RebindTreeList":
				this.Rebind();
				return;
			case "ItemClick":
				if (flag)
				{
					this.Items[num].FireCommandEvent("ItemClick", string.Empty);
					return;
				}
				return;
			case "ItemDrop":
			{
				string[] array = eventArgs.Split(new char[]
				{
					';'
				});
				if (array.Length >= 2)
				{
					TreeListDataItem treeListDataItem = null;
					TreeListHeaderItem destinationHeaderItem = null;
					int num3 = -1;
					if (int.TryParse(array[0], out num3))
					{
						if (num3 > -1)
						{
							treeListDataItem = this.Items[num3];
						}
						else if (num3 == -1)
						{
							destinationHeaderItem = (TreeListHeaderItem)this.GetItems(new TreeListItemType[]
							{
								TreeListItemType.HeaderItem
							})[0];
						}
					}
					string htmlElement = array[1];
					TreeListDataItemCollection treeListDataItemCollection = new TreeListDataItemCollection();
					if (this._draggedIndexes != null)
					{
						foreach (int index in this._draggedIndexes)
						{
							treeListDataItemCollection.Add(this.Items[index]);
						}
					}
					Hashtable hashtable = new Hashtable();
					this.ExtractParentKeyValuesForChild(hashtable, treeListDataItem);
					TreeListItemDragDropEventArgs e = new TreeListItemDragDropEventArgs(treeListDataItemCollection, treeListDataItem, destinationHeaderItem, htmlElement, hashtable);
					this.OnItemDrop(e);
					return;
				}
				return;
			}
			case "Swap":
				this.SwapColumns(eventArgs, secondEventArgs);
				this.OnColumnsOrderChanged();
				return;
			case "Reorder":
				this.ReorderColumns(eventArgs, secondEventArgs);
				this.OnColumnsOrderChanged();
				return;
			}
			if (flag)
			{
				this.Items[num].FireCommandEvent(eventName, secondEventArgs);
				return;
			}
			this.GetItems(new TreeListItemType[]
			{
				TreeListItemType.HeaderItem
			})[0].FireCommandEvent(eventName, eventArgs);
		}

		// Token: 0x0600C267 RID: 49767 RVA: 0x002B8EBF File Offset: 0x002B70BF
		protected override void RaisePostDataChangedEvent()
		{
			base.RaisePostDataChangedEvent();
			if (this._shouldCallOnSelectedIndexChanged)
			{
				this._shouldCallOnSelectedIndexChanged = false;
				this.CallOnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600C268 RID: 49768 RVA: 0x002B8EE1 File Offset: 0x002B70E1
		private void ClearActiveRowIndex(bool shouldFocusOnPage)
		{
			if (this.ClientSettings.AllowKeyboardNavigation)
			{
				this.ClientSettings.ActiveRowIndex = null;
				this.shouldFocusOnPage = shouldFocusOnPage;
			}
		}

		// Token: 0x0600C269 RID: 49769 RVA: 0x002B8F04 File Offset: 0x002B7104
		private void FireCommandEventForEditItem(int itemIndex, string commandName)
		{
			TreeListItem[] items = this.GetItems(new TreeListItemType[]
			{
				TreeListItemType.EditItem,
				TreeListItemType.EditFormItem
			});
			foreach (TreeListItem treeListItem in items)
			{
				TreeListEditFormItem treeListEditFormItem = treeListItem as TreeListEditFormItem;
				if (treeListEditFormItem != null && treeListEditFormItem.ParentItem.DisplayIndex == itemIndex)
				{
					treeListEditFormItem.FireCommandEvent(commandName, string.Empty);
					break;
				}
			}
		}

		// Token: 0x0600C26A RID: 49770 RVA: 0x002B8F74 File Offset: 0x002B7174
		private void FireCommandEventForInsertItem(int itemIndex, string commandName)
		{
			TreeListItem[] items = this.GetItems(new TreeListItemType[]
			{
				TreeListItemType.EditItem,
				TreeListItemType.EditFormItem
			});
			foreach (TreeListItem treeListItem in items)
			{
				TreeListEditFormInsertItem treeListEditFormInsertItem = treeListItem as TreeListEditFormInsertItem;
				if (treeListEditFormInsertItem != null && treeListEditFormInsertItem.ParentItem != null && treeListEditFormInsertItem.ParentItem.DisplayIndex == itemIndex)
				{
					treeListEditFormInsertItem.FireCommandEvent(commandName, string.Empty);
					break;
				}
				TreeListDataInsertItem treeListDataInsertItem = treeListItem as TreeListDataInsertItem;
				if (treeListDataInsertItem != null && treeListDataInsertItem.ParentItem != null && treeListDataInsertItem.ParentItem.DisplayIndex == itemIndex)
				{
					treeListDataInsertItem.FireCommandEvent(commandName, string.Empty);
					break;
				}
			}
		}

		// Token: 0x17003EAC RID: 16044
		// (get) Token: 0x0600C26B RID: 49771 RVA: 0x002B901C File Offset: 0x002B721C
		// (set) Token: 0x0600C26C RID: 49772 RVA: 0x002B9024 File Offset: 0x002B7224
		internal TreeListEnumerableHelper.TreeListDataItemEvaluator ItemEvaluator { get; set; }

		// Token: 0x17003EAD RID: 16045
		// (get) Token: 0x0600C26D RID: 49773 RVA: 0x002B902D File Offset: 0x002B722D
		// (set) Token: 0x0600C26E RID: 49774 RVA: 0x002B9035 File Offset: 0x002B7235
		internal object FirstItemInstance { get; set; }

		// Token: 0x0600C26F RID: 49775 RVA: 0x002B905C File Offset: 0x002B725C
		internal void TrackPaging(int pageIndex)
		{
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "Paging").OfPriority(FeaturePriority.High).OfClass(FeatureClass.Other).OfValue(() => pageIndex.ToString()));
		}

		// Token: 0x0600C270 RID: 49776 RVA: 0x002B90DC File Offset: 0x002B72DC
		internal void TrackSorting(string sortExpression)
		{
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "Sorting").OfPriority(FeaturePriority.High).OfClass(FeatureClass.DataOperation).OfValue(() => sortExpression));
		}

		// Token: 0x0600C271 RID: 49777 RVA: 0x002B915C File Offset: 0x002B735C
		internal void TrackSelection(string dataKeyValue, bool selected)
		{
			string selectString = string.Format("[{0}]:[{1}]", dataKeyValue, selected);
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "Selection").OfPriority(FeaturePriority.High).OfClass(FeatureClass.Selection).OfValue(() => selectString));
		}

		// Token: 0x0600C272 RID: 49778 RVA: 0x002B91EC File Offset: 0x002B73EC
		internal void TrackExport(string exportType)
		{
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "Export").OfPriority(FeaturePriority.High).OfClass(FeatureClass.Other).OfValue(() => exportType));
		}

		// Token: 0x0600C273 RID: 49779 RVA: 0x002B9258 File Offset: 0x002B7458
		internal void TrackItemSelection(TreeListItem item, string commandName)
		{
			TreeListDataItem treeListDataItem = item as TreeListDataItem;
			string dataKeyValue = string.Empty;
			if (treeListDataItem != null)
			{
				bool selected;
				if (commandName == "Select" || commandName == "Deselect")
				{
					selected = !treeListDataItem.Selected;
				}
				else
				{
					selected = treeListDataItem.Selected;
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (this.DataKeyNames.Count<string>() > 0)
				{
					foreach (string text in this.DataKeyNames)
					{
						stringBuilder.AppendFormat("{0}:{1}&&", text, treeListDataItem.GetDataKeyValue(text));
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Remove(stringBuilder.Length - 2, 2);
				}
				else
				{
					stringBuilder.AppendFormat("dataItemIndex:{0}", treeListDataItem.DataItemIndex);
				}
				dataKeyValue = stringBuilder.ToString();
				this.TrackSelection(dataKeyValue, selected);
			}
		}

		// Token: 0x0600C274 RID: 49780 RVA: 0x002B9358 File Offset: 0x002B7558
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal void CalculateAggregates(TreeListColumnsCollection Columns)
		{
			List<TreeListSourceItem> list = (from a in TreeListAggregatesHelper.AggregatedSourceItems
			where a.Key.NestedLevel == 0
			select a.Value).ToList<TreeListSourceItem>();
			TreeListSourceItem treeListSourceItem = new TreeListSourceItem
			{
				HierarchyIndex = new TreeListHierarchyIndex
				{
					LevelIndex = -1,
					NestedLevel = -1
				}
			};
			foreach (TreeListSourceItem treeListSourceItem2 in list)
			{
				treeListSourceItem2.ParentItem = treeListSourceItem;
				treeListSourceItem.ChildItems = list;
			}
			if (TreeListAggregatesHelper.AggregatesSourceItemsCollection.Count == 0)
			{
				this.PopulateAggregatedDictionaryReqursive(treeListSourceItem);
			}
			foreach (KeyValuePair<TreeListHierarchyIndex, List<TreeListSourceItem>> kv in TreeListAggregatesHelper.AggregatesSourceItemsCollection)
			{
				Type type = typeof(ListViewLinqEnumerableHelper.ListViewGenericEnumerable<>).MakeGenericType(new Type[]
				{
					TreeListEnumerableHelper.resolvedItemType
				});
				IEnumerable<object> enumerable = from a in kv.Value.Distinct<TreeListSourceItem>().Skip(1)
				select a.OriginalDataItem;
				IEnumerable enumerable2 = (IEnumerable)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[]
				{
					enumerable
				}, null);
				IQueryable queryable = enumerable2.AsQueryable();
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (TreeListColumn treeListColumn in Columns)
				{
					if (treeListColumn is TreeListBoundColumn)
					{
						TreeListBoundColumn boundColumn = treeListColumn as TreeListBoundColumn;
						this.PopulateBoundColumnAggregates(kv, enumerable2, queryable, dictionary, boundColumn);
					}
					if (treeListColumn is TreeListTemplateColumn)
					{
						TreeListTemplateColumn templateColumn = treeListColumn as TreeListTemplateColumn;
						this.PopulateTemplateColumnAggregates(kv, enumerable2, queryable, dictionary, templateColumn);
					}
					if (treeListColumn is TreeListCalculatedColumn)
					{
						TreeListCalculatedColumn calculatedColumn = treeListColumn as TreeListCalculatedColumn;
						this.PopulateCalculatedColumnAggregates(kv, dictionary, calculatedColumn);
					}
				}
				string key = kv.Key.LevelIndex.ToString() + kv.Key.NestedLevel.ToString();
				if (!this.CalculatedAggregates.ContainsKey(key))
				{
					this.CalculatedAggregates.Add(key, dictionary);
				}
			}
		}

		// Token: 0x0600C275 RID: 49781 RVA: 0x002B962C File Offset: 0x002B782C
		private void PopulateCalculatedColumnAggregates(KeyValuePair<TreeListHierarchyIndex, List<TreeListSourceItem>> kv, Dictionary<string, string> aggregatesPerColumn, TreeListCalculatedColumn calculatedColumn)
		{
			if (calculatedColumn.Aggregate != TreeListAggregateFunction.None)
			{
				string calcColumnUniqueName = calculatedColumn.UniqueName + "Result";
				IEnumerable<object> enumerable = from item in kv.Value.Skip(1)
				select item.CalculatedColumns[calcColumnUniqueName];
				IEnumerable enumerable2 = enumerable;
				IQueryable queryable = enumerable2.AsQueryable();
				Type type = enumerable.FirstOrDefault<object>().GetType();
				object aggregate = TreeListAggregatesHelper.GetAggregate(enumerable2, queryable, null, type, calculatedColumn.Aggregate);
				if (!string.IsNullOrEmpty(calculatedColumn.FooterAggregateFormatString))
				{
					string value = string.Format(calculatedColumn.FooterAggregateFormatString, aggregate);
					aggregatesPerColumn.Add(calcColumnUniqueName, value);
					return;
				}
				aggregatesPerColumn.Add(calcColumnUniqueName, aggregate.ToString());
			}
		}

		// Token: 0x0600C276 RID: 49782 RVA: 0x002B96E8 File Offset: 0x002B78E8
		private void PopulateTemplateColumnAggregates(KeyValuePair<TreeListHierarchyIndex, List<TreeListSourceItem>> kv, IEnumerable enumerable, IQueryable queryable, Dictionary<string, string> aggregatesPerColumn, TreeListTemplateColumn templateColumn)
		{
			if (templateColumn.Aggregate != TreeListAggregateFunction.None)
			{
				Type propertyType = TreeListAggregatesHelper.GetPropertyType(TreeListEnumerableHelper.resolvedItemType, templateColumn.DataField, kv.Value.FirstOrDefault<TreeListSourceItem>(), this.FirstItemInstance, this.ItemEvaluator);
				object aggregate = TreeListAggregatesHelper.GetAggregate(enumerable, queryable, templateColumn.DataField, propertyType, templateColumn.Aggregate);
				if (!string.IsNullOrEmpty(templateColumn.FooterAggregateFormatString))
				{
					string value = string.Format(templateColumn.FooterAggregateFormatString, aggregate);
					aggregatesPerColumn.Add(templateColumn.UniqueName, value);
					return;
				}
				aggregatesPerColumn.Add(templateColumn.UniqueName, aggregate.ToString());
			}
		}

		// Token: 0x0600C277 RID: 49783 RVA: 0x002B9784 File Offset: 0x002B7984
		private void PopulateBoundColumnAggregates(KeyValuePair<TreeListHierarchyIndex, List<TreeListSourceItem>> kv, IEnumerable enumerable, IQueryable queryable, Dictionary<string, string> aggregatesPerColumn, TreeListBoundColumn boundColumn)
		{
			if (boundColumn.Aggregate != TreeListAggregateFunction.None)
			{
				Type propertyType = TreeListAggregatesHelper.GetPropertyType(TreeListEnumerableHelper.resolvedItemType, boundColumn.DataField, kv.Value.FirstOrDefault<TreeListSourceItem>(), this.FirstItemInstance, this.ItemEvaluator);
				object aggregate = TreeListAggregatesHelper.GetAggregate(enumerable, queryable, boundColumn.DataField, propertyType, boundColumn.Aggregate);
				if (!string.IsNullOrEmpty(boundColumn.FooterAggregateFormatString))
				{
					string value = string.Format(boundColumn.FooterAggregateFormatString, aggregate);
					aggregatesPerColumn.Add(boundColumn.UniqueName, value);
					return;
				}
				aggregatesPerColumn.Add(boundColumn.UniqueName, aggregate.ToString());
			}
		}

		// Token: 0x0600C278 RID: 49784 RVA: 0x002B9820 File Offset: 0x002B7A20
		private List<TreeListSourceItem> PopulateAggregatedDictionaryReqursive(TreeListSourceItem node)
		{
			List<TreeListSourceItem> list = new List<TreeListSourceItem>();
			list.Add(node);
			if (node.ChildItemsCount > 0 && !TreeListAggregatesHelper.AggregatesSourceItemsCollection.ContainsKey(node.HierarchyIndex))
			{
				TreeListAggregatesHelper.AggregatesSourceItemsCollection.Add(node.HierarchyIndex, list);
			}
			foreach (TreeListSourceItem node2 in node.ChildItems)
			{
				list.AddRange(this.PopulateAggregatedDictionaryReqursive(node2));
			}
			return list.Distinct<TreeListSourceItem>().ToList<TreeListSourceItem>();
		}

		// Token: 0x17003EAE RID: 16046
		// (get) Token: 0x0600C279 RID: 49785 RVA: 0x002B98B8 File Offset: 0x002B7AB8
		// (set) Token: 0x0600C27A RID: 49786 RVA: 0x002B98C0 File Offset: 0x002B7AC0
		public bool IsExporting { get; set; }

		// Token: 0x0600C27B RID: 49787 RVA: 0x002B98C9 File Offset: 0x002B7AC9
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.UsesControlState)
			{
				this.Page.RegisterRequiresControlState(this);
			}
		}

		// Token: 0x0600C27C RID: 49788 RVA: 0x002B98E6 File Offset: 0x002B7AE6
		protected override void OnLoad(EventArgs e)
		{
			if (this.ShouldBeBound)
			{
				this.AutoDataBind(TreeListRebindReason.InitialLoad);
			}
			else if (this.AlwaysAutoBindOnPostBack && this._shouldCallDataBindOnLoad)
			{
				this.AutoDataBind(TreeListRebindReason.PostbackViewStateNotPersisted);
			}
			base.OnLoad(e);
		}

		// Token: 0x0600C27D RID: 49789 RVA: 0x002B9917 File Offset: 0x002B7B17
		protected override void OnPreRender(EventArgs e)
		{
			if (base.RequiresDataBinding)
			{
				this.Rebind();
			}
			base.OnPreRender(e);
		}

		// Token: 0x17003EAF RID: 16047
		// (get) Token: 0x0600C27E RID: 49790 RVA: 0x002B992E File Offset: 0x002B7B2E
		// (set) Token: 0x0600C27F RID: 49791 RVA: 0x002B9936 File Offset: 0x002B7B36
		protected bool IsNeedDataSourceInProgress { get; set; }

		// Token: 0x14000183 RID: 387
		// (add) Token: 0x0600C280 RID: 49792 RVA: 0x002B993F File Offset: 0x002B7B3F
		// (remove) Token: 0x0600C281 RID: 49793 RVA: 0x002B9952 File Offset: 0x002B7B52
		[Category("Action")]
		[Description("Raised when the treelist is about to be bound and the data source must be assigned")]
		public event EventHandler<TreeListNeedDataSourceEventArgs> NeedDataSource
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventNeedDataSource, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventNeedDataSource, value);
			}
		}

		// Token: 0x0600C282 RID: 49794 RVA: 0x002B9968 File Offset: 0x002B7B68
		protected virtual void OnNeedDataSource(TreeListNeedDataSourceEventArgs e)
		{
			this.IsNeedDataSourceInProgress = true;
			try
			{
				EventHandler<TreeListNeedDataSourceEventArgs> eventHandler = base.Events[RadTreeList.EventNeedDataSource] as EventHandler<TreeListNeedDataSourceEventArgs>;
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

		// Token: 0x14000184 RID: 388
		// (add) Token: 0x0600C283 RID: 49795 RVA: 0x002B99B8 File Offset: 0x002B7BB8
		// (remove) Token: 0x0600C284 RID: 49796 RVA: 0x002B99CB File Offset: 0x002B7BCB
		[Category("Action")]
		[Description("Raised when the TreeList item's child items will be bound.")]
		public event EventHandler<TreeListChildItemsDataBindEventArgs> ChildItemsDataBind
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventChildItemsDataBind, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventChildItemsDataBind, value);
			}
		}

		// Token: 0x0600C285 RID: 49797 RVA: 0x002B99E0 File Offset: 0x002B7BE0
		protected virtual void OnChildItemsDataBind(TreeListChildItemsDataBindEventArgs e)
		{
			EventHandler<TreeListChildItemsDataBindEventArgs> eventHandler = base.Events[RadTreeList.EventChildItemsDataBind] as EventHandler<TreeListChildItemsDataBindEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C286 RID: 49798 RVA: 0x002B9A0E File Offset: 0x002B7C0E
		internal void CallOnChildItemsDataBind(TreeListChildItemsDataBindEventArgs e)
		{
			this.OnChildItemsDataBind(e);
		}

		// Token: 0x0600C287 RID: 49799 RVA: 0x002B9A17 File Offset: 0x002B7C17
		public override void DataBind()
		{
			if (this.IsNeedDataSourceInProgress)
			{
				throw new InvalidOperationException("You should not call DataBind in NeedDataSource event handler. DataBind would take place automatically right after NeedDataSource handler finishes execution.");
			}
			base.DataBind();
		}

		// Token: 0x0600C288 RID: 49800 RVA: 0x002B9A32 File Offset: 0x002B7C32
		public virtual void Rebind()
		{
			this.AutoDataBind(TreeListRebindReason.ExplicitRebind);
		}

		// Token: 0x17003EB0 RID: 16048
		// (get) Token: 0x0600C289 RID: 49801 RVA: 0x002B9A3B File Offset: 0x002B7C3B
		internal bool AlwaysAutoBindOnPostBack
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x17003EB1 RID: 16049
		// (get) Token: 0x0600C28A RID: 49802 RVA: 0x002B9A46 File Offset: 0x002B7C46
		internal bool ShouldBeBound
		{
			get
			{
				return this.ControlState["_!DSIC"] == null;
			}
		}

		// Token: 0x0600C28B RID: 49803 RVA: 0x002B9A5B File Offset: 0x002B7C5B
		public void PerformUpdate(TreeListEditableItem editedItem)
		{
			this.PerformUpdate(editedItem, false);
		}

		// Token: 0x0600C28C RID: 49804 RVA: 0x002B9B38 File Offset: 0x002B7D38
		public virtual void PerformUpdate(TreeListEditableItem editedItem, bool suppressRebind)
		{
			if (editedItem == null)
			{
				throw new ArgumentNullException("editedItem");
			}
			DataSourceView data = base.GetData();
			ModelDataSourceView modelDataSourceView = data as ModelDataSourceView;
			if (modelDataSourceView != null)
			{
				if (string.IsNullOrWhiteSpace(this.UpdateMethod))
				{
					throw new Exception("Updating is not supported unless the UpdateMethod is specified.");
				}
				this.ModelBindingUpdateProperties(modelDataSourceView);
			}
			if (data.CanUpdate)
			{
				Hashtable keys = new Hashtable();
				this.FillDataKeys(keys, editedItem);
				Hashtable hashtable = new Hashtable();
				editedItem.ExtractValues(hashtable);
				data.Update(keys, hashtable, editedItem.SavedOldValues, delegate(int affectedRows, Exception exception)
				{
					TreeListUpdatedEventArgs treeListUpdatedEventArgs = new TreeListUpdatedEventArgs(affectedRows, exception, editedItem)
					{
						KeepInEditMode = (exception != null)
					};
					if (this.IsUsingModelBinding)
					{
						if (this.Page.ModelState.IsValid)
						{
							this.FireItemUpdatedEvent(editedItem, suppressRebind, exception, treeListUpdatedEventArgs);
						}
						else
						{
							this.RequiresDataBinding = false;
							if (this.EditMode == TreeListEditMode.InPlace)
							{
								editedItem.Edit = true;
							}
							else
							{
								(editedItem as TreeListEditFormItem).ParentItem.Edit = true;
							}
						}
					}
					else
					{
						this.FireItemUpdatedEvent(editedItem, suppressRebind, exception, treeListUpdatedEventArgs);
					}
					return treeListUpdatedEventArgs.ExceptionHandled;
				});
			}
		}

		// Token: 0x0600C28D RID: 49805 RVA: 0x002B9C03 File Offset: 0x002B7E03
		private void FireItemUpdatedEvent(TreeListEditableItem editedItem, bool suppressRebind, Exception exception, TreeListUpdatedEventArgs args)
		{
			this.FireItemUpdated(args);
			if (!args.KeepInEditMode)
			{
				editedItem.Edit = false;
			}
			if (exception == null && !suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x0600C28E RID: 49806 RVA: 0x002B9C2C File Offset: 0x002B7E2C
		private void ModelBindingUpdateProperties(ModelDataSourceView modelView)
		{
			string dataKeyName = string.Empty;
			if (this.DataKeyNames.Length > 0)
			{
				dataKeyName = this.DataKeyNames[0];
			}
			modelView.UpdateProperties(this.ItemType, this.SelectMethod, this.UpdateMethod, base.InsertMethod, this.DeleteMethod, dataKeyName);
		}

		// Token: 0x0600C28F RID: 49807 RVA: 0x002B9C78 File Offset: 0x002B7E78
		public virtual void PerformInsert(TreeListEditableItem insertItem)
		{
			if (insertItem == null)
			{
				throw new InvalidOperationException("Insert item is available only when RadTreeList is in insert mode.");
			}
			this.PerformInsert(insertItem, false);
		}

		// Token: 0x0600C290 RID: 49808 RVA: 0x002B9D3C File Offset: 0x002B7F3C
		public virtual void PerformInsert(TreeListEditableItem insertItem, bool suppressRebind)
		{
			if (insertItem == null)
			{
				throw new ArgumentNullException("insertItem");
			}
			if (!(insertItem is ITreeListInsertItem))
			{
				throw new ArgumentException("insertItem");
			}
			DataSourceView data = base.GetData();
			ModelDataSourceView modelDataSourceView = data as ModelDataSourceView;
			if (modelDataSourceView != null)
			{
				if (string.IsNullOrWhiteSpace(this.InsertMethod))
				{
					throw new Exception("Inserting is not supported unless the InsertMethod is specified.");
				}
				this.ModelBindingUpdateProperties(modelDataSourceView);
				suppressRebind = true;
			}
			if (data.CanInsert)
			{
				Hashtable hashtable = new Hashtable();
				insertItem.ExtractValues(hashtable);
				data.Insert(hashtable, delegate(int affectedRows, Exception exception)
				{
					TreeListInsertedEventArgs treeListInsertedEventArgs = new TreeListInsertedEventArgs(affectedRows, exception, insertItem)
					{
						KeepInInsertMode = (exception != null)
					};
					if (this.IsUsingModelBinding)
					{
						if (this.Page.ModelState.IsValid)
						{
							this.FireItemInsertedEvent(insertItem, suppressRebind, exception, treeListInsertedEventArgs);
						}
						else
						{
							this.RequiresDataBinding = false;
							insertItem.Edit = true;
						}
					}
					else
					{
						this.FireItemInsertedEvent(insertItem, suppressRebind, exception, treeListInsertedEventArgs);
					}
					return treeListInsertedEventArgs.ExceptionHandled;
				});
			}
		}

		// Token: 0x0600C291 RID: 49809 RVA: 0x002B9E03 File Offset: 0x002B8003
		private void FireItemInsertedEvent(TreeListEditableItem insertItem, bool suppressRebind, Exception exception, TreeListInsertedEventArgs args)
		{
			this.OnItemInserted(args);
			if (!args.KeepInInsertMode)
			{
				insertItem.Edit = false;
			}
			if (exception == null && !suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x0600C292 RID: 49810 RVA: 0x002B9E29 File Offset: 0x002B8029
		public virtual void PerformDelete(TreeListDataItem editedItem)
		{
			this.PerformDelete(editedItem, false);
		}

		// Token: 0x0600C293 RID: 49811 RVA: 0x002B9E34 File Offset: 0x002B8034
		internal virtual void PerformDelete(TreeListDataItem editedItem, bool suppressRebind)
		{
			DataSourceView data = base.GetData();
			ModelDataSourceView modelDataSourceView = data as ModelDataSourceView;
			if (modelDataSourceView != null)
			{
				if (string.IsNullOrWhiteSpace(this.DeleteMethod))
				{
					throw new Exception("Deleting is not supported unless the DeleteMethod is specified.");
				}
				this.ModelBindingUpdateProperties(modelDataSourceView);
			}
			if (data.CanDelete)
			{
				this.BeginDelete();
				try
				{
					Hashtable keys = new Hashtable();
					this.FillDataKeys(keys, editedItem);
					Hashtable hashtable = new Hashtable();
					this.ExtractValuesFromItem(hashtable, editedItem, false);
					this.DeleteContext = new TreeListDeleteContext(keys, hashtable, editedItem, suppressRebind);
					this.Rebind();
				}
				finally
				{
					this.EndDelete();
				}
			}
		}

		// Token: 0x17003EB2 RID: 16050
		// (get) Token: 0x0600C294 RID: 49812 RVA: 0x002B9ECC File Offset: 0x002B80CC
		// (set) Token: 0x0600C295 RID: 49813 RVA: 0x002B9EF5 File Offset: 0x002B80F5
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool AllowRecursiveDelete
		{
			get
			{
				object obj = this.ViewState["AllowRecursiveDelete"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowRecursiveDelete"] = value;
			}
		}

		// Token: 0x17003EB3 RID: 16051
		// (get) Token: 0x0600C296 RID: 49814 RVA: 0x002B9F0D File Offset: 0x002B810D
		// (set) Token: 0x0600C297 RID: 49815 RVA: 0x002B9F15 File Offset: 0x002B8115
		internal TreeListDeleteContext DeleteContext { get; private set; }

		// Token: 0x17003EB4 RID: 16052
		// (get) Token: 0x0600C298 RID: 49816 RVA: 0x002B9F1E File Offset: 0x002B811E
		// (set) Token: 0x0600C299 RID: 49817 RVA: 0x002B9F26 File Offset: 0x002B8126
		internal bool IsDeleteInProgress { get; private set; }

		// Token: 0x0600C29A RID: 49818 RVA: 0x002B9F2F File Offset: 0x002B812F
		private void BeginDelete()
		{
			this.IsDeleteInProgress = true;
		}

		// Token: 0x0600C29B RID: 49819 RVA: 0x002B9F38 File Offset: 0x002B8138
		private void EndDelete()
		{
			this.IsDeleteInProgress = false;
			this.DeleteContext = null;
		}

		// Token: 0x17003EB5 RID: 16053
		// (get) Token: 0x0600C29C RID: 49820 RVA: 0x002B9F48 File Offset: 0x002B8148
		// (set) Token: 0x0600C29D RID: 49821 RVA: 0x002B9F50 File Offset: 0x002B8150
		internal TreeListReorderContext ReorderContext { get; private set; }

		// Token: 0x0600C29E RID: 49822 RVA: 0x002B9F59 File Offset: 0x002B8159
		public static HtmlGenericControl CreateButton(string name, bool display = true)
		{
			return RadTreeList.CreateButton(name, name, display);
		}

		// Token: 0x0600C29F RID: 49823 RVA: 0x002B9F64 File Offset: 0x002B8164
		public static HtmlGenericControl CreateButton(string name, string text, bool display = true)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("button");
			htmlGenericControl.Attributes.Add("title", text);
			htmlGenericControl.Attributes.Add("class", string.Format("t-button rtlActionButton rtl{0}", name));
			if (!display)
			{
				htmlGenericControl.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
			htmlGenericControl2.Attributes.Add("class", "t-font-icon rtlIcon rtl" + name + "Icon");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			return htmlGenericControl;
		}

		// Token: 0x0600C2A0 RID: 49824 RVA: 0x002B9FF8 File Offset: 0x002B81F8
		public virtual void ExtractValuesFromItem(IDictionary newValues, TreeListEditableItem dataItem, bool includePrimaryKey)
		{
			if (newValues == null)
			{
				throw new ArgumentNullException("newValues");
			}
			if (dataItem == null)
			{
				throw new ArgumentNullException("dataItem");
			}
			dataItem.ExtractValues(newValues);
			if (includePrimaryKey)
			{
				Hashtable hashtable = new Hashtable();
				this.FillDataKeys(hashtable, dataItem);
				foreach (object obj in hashtable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (!newValues.Contains(dictionaryEntry.Key))
					{
						newValues.Add(dictionaryEntry.Key, dictionaryEntry.Value);
					}
				}
			}
		}

		// Token: 0x14000185 RID: 389
		// (add) Token: 0x0600C2A1 RID: 49825 RVA: 0x002BA09C File Offset: 0x002B829C
		// (remove) Token: 0x0600C2A2 RID: 49826 RVA: 0x002BA0AF File Offset: 0x002B82AF
		[Category("Action")]
		[Description("Occurs when a dwhen a new item has been selected in the TreeList control or the currently selected item has changed.")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventSelectedIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventSelectedIndexChanged, value);
			}
		}

		// Token: 0x0600C2A3 RID: 49827 RVA: 0x002BA0C4 File Offset: 0x002B82C4
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadTreeList.EventSelectedIndexChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C2A4 RID: 49828 RVA: 0x002BA0F2 File Offset: 0x002B82F2
		internal void CallOnSelectedIndexChanged(EventArgs e)
		{
			this.shouldTrackSelection = true;
			this.OnSelectedIndexChanged(e);
		}

		// Token: 0x14000186 RID: 390
		// (add) Token: 0x0600C2A5 RID: 49829 RVA: 0x002BA102 File Offset: 0x002B8302
		// (remove) Token: 0x0600C2A6 RID: 49830 RVA: 0x002BA115 File Offset: 0x002B8315
		[Category("Action")]
		[Description("Occurs when a delete operation is requested, after the RadTreeList control deletes the item.")]
		public event EventHandler<TreeListDeletedEventArgs> ItemDeleted
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventItemDeleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventItemDeleted, value);
			}
		}

		// Token: 0x0600C2A7 RID: 49831 RVA: 0x002BA128 File Offset: 0x002B8328
		protected virtual void OnItemDeleted(TreeListDeletedEventArgs e)
		{
			EventHandler<TreeListDeletedEventArgs> eventHandler = base.Events[RadTreeList.EventItemDeleted] as EventHandler<TreeListDeletedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000187 RID: 391
		// (add) Token: 0x0600C2A8 RID: 49832 RVA: 0x002BA156 File Offset: 0x002B8356
		// (remove) Token: 0x0600C2A9 RID: 49833 RVA: 0x002BA169 File Offset: 0x002B8369
		[Description("Occurs when an insert operation is requested, after the RadTreeList control has inserted the item in the data source.")]
		[Category("Action")]
		public event EventHandler<TreeListInsertedEventArgs> ItemInserted
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventItemInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventItemInserted, value);
			}
		}

		// Token: 0x0600C2AA RID: 49834 RVA: 0x002BA17C File Offset: 0x002B837C
		protected virtual void OnItemInserted(TreeListInsertedEventArgs e)
		{
			EventHandler<TreeListInsertedEventArgs> eventHandler = base.Events[RadTreeList.EventItemInserted] as EventHandler<TreeListInsertedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000188 RID: 392
		// (add) Token: 0x0600C2AB RID: 49835 RVA: 0x002BA1AA File Offset: 0x002B83AA
		// (remove) Token: 0x0600C2AC RID: 49836 RVA: 0x002BA1BD File Offset: 0x002B83BD
		[Description("Occurs when an update operation is requested, after the RadTreeList control updates the item.")]
		[Category("Action")]
		public event EventHandler<TreeListUpdatedEventArgs> ItemUpdated
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventItemUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventItemUpdated, value);
			}
		}

		// Token: 0x0600C2AD RID: 49837 RVA: 0x002BA1D0 File Offset: 0x002B83D0
		protected virtual void OnItemUpdated(TreeListUpdatedEventArgs e)
		{
			EventHandler<TreeListUpdatedEventArgs> eventHandler = base.Events[RadTreeList.EventItemUpdated] as EventHandler<TreeListUpdatedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C2AE RID: 49838 RVA: 0x002BA1FE File Offset: 0x002B83FE
		internal void FireItemUpdated(TreeListUpdatedEventArgs e)
		{
			this.OnItemUpdated(e);
		}

		// Token: 0x14000189 RID: 393
		// (add) Token: 0x0600C2AF RID: 49839 RVA: 0x002BA207 File Offset: 0x002B8407
		// (remove) Token: 0x0600C2B0 RID: 49840 RVA: 0x002BA21A File Offset: 0x002B841A
		[Description("Provides access to the Export Infrastructure before sending the file to the browser.")]
		[Category("Action")]
		public event EventHandler<TreeListInfrastructureExportingEventArgs> InfrastructureExporting
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventInfrastructureExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventInfrastructureExporting, value);
			}
		}

		// Token: 0x0600C2B1 RID: 49841 RVA: 0x002BA230 File Offset: 0x002B8430
		protected virtual void OnInfrastructureExporting(TreeListInfrastructureExportingEventArgs e)
		{
			EventHandler<TreeListInfrastructureExportingEventArgs> eventHandler = base.Events[RadTreeList.EventInfrastructureExporting] as EventHandler<TreeListInfrastructureExportingEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C2B2 RID: 49842 RVA: 0x002BA25E File Offset: 0x002B845E
		internal void CallOnInfrastructureExporting(TreeListInfrastructureExportingEventArgs args)
		{
			this.TrackExport(args.ExportFormat.ToString());
			this.OnInfrastructureExporting(args);
		}

		// Token: 0x1400018A RID: 394
		// (add) Token: 0x0600C2B3 RID: 49843 RVA: 0x002BA27D File Offset: 0x002B847D
		// (remove) Token: 0x0600C2B4 RID: 49844 RVA: 0x002BA290 File Offset: 0x002B8490
		[Category("Action")]
		[Description("Triggered when the export output is about to be sent to the file.")]
		public event EventHandler<TreeListExportingEventArgs> Exporting
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventExporting, value);
			}
		}

		// Token: 0x0600C2B5 RID: 49845 RVA: 0x002BA2A4 File Offset: 0x002B84A4
		protected virtual void OnExporting(TreeListExportingEventArgs e)
		{
			EventHandler<TreeListExportingEventArgs> eventHandler = base.Events[RadTreeList.EventExporting] as EventHandler<TreeListExportingEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C2B6 RID: 49846 RVA: 0x002BA2D2 File Offset: 0x002B84D2
		internal void CallOnExporting(TreeListExportingEventArgs args)
		{
			this.TrackExport(args.ExportType.ToString());
			this.OnExporting(args);
		}

		// Token: 0x1400018B RID: 395
		// (add) Token: 0x0600C2B7 RID: 49847 RVA: 0x002BA2F1 File Offset: 0x002B84F1
		// (remove) Token: 0x0600C2B8 RID: 49848 RVA: 0x002BA304 File Offset: 0x002B8504
		[Category("Action")]
		[Description("Raised before the HTML code is parsed to PDF binary.")]
		public event EventHandler<TreeListPdfExportingEventArgs> PdfExporting
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventPdfExporting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventPdfExporting, value);
			}
		}

		// Token: 0x0600C2B9 RID: 49849 RVA: 0x002BA318 File Offset: 0x002B8518
		protected virtual void OnPdfExporting(TreeListPdfExportingEventArgs e)
		{
			EventHandler<TreeListPdfExportingEventArgs> eventHandler = base.Events[RadTreeList.EventPdfExporting] as EventHandler<TreeListPdfExportingEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C2BA RID: 49850 RVA: 0x002BA346 File Offset: 0x002B8546
		internal void CallOnPdfExporting(TreeListPdfExportingEventArgs args)
		{
			this.OnPdfExporting(args);
		}

		// Token: 0x1400018C RID: 396
		// (add) Token: 0x0600C2BB RID: 49851 RVA: 0x002BA34F File Offset: 0x002B854F
		// (remove) Token: 0x0600C2BC RID: 49852 RVA: 0x002BA362 File Offset: 0x002B8562
		[Category("Action")]
		[Description("Occurs when a TreeList item is dragged and dropped on an HTML element")]
		public event EventHandler<TreeListItemDragDropEventArgs> ItemDrop
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventItemDrop, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventItemDrop, value);
			}
		}

		// Token: 0x0600C2BD RID: 49853 RVA: 0x002BA378 File Offset: 0x002B8578
		protected virtual void OnItemDrop(TreeListItemDragDropEventArgs e)
		{
			EventHandler<TreeListItemDragDropEventArgs> eventHandler = base.Events[RadTreeList.EventItemDrop] as EventHandler<TreeListItemDragDropEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.IsBoundUsingDataSourceIDInternal && !e.Canceled)
			{
				this.PerformReorder(e);
			}
		}

		// Token: 0x1400018D RID: 397
		// (add) Token: 0x0600C2BE RID: 49854 RVA: 0x002BA3BD File Offset: 0x002B85BD
		// (remove) Token: 0x0600C2BF RID: 49855 RVA: 0x002BA3D0 File Offset: 0x002B85D0
		[Category("Action")]
		[Description("Occurs when column's order in the columns collection is changed")]
		public event EventHandler<TreeListColumnsOrderChangedEventArgs> ColumnsOrderChanged
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventColumnsOrderChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventColumnsOrderChanged, value);
			}
		}

		// Token: 0x0600C2C0 RID: 49856 RVA: 0x002BA3E3 File Offset: 0x002B85E3
		public int GetMaximumNestedLevel()
		{
			this.CalculateMostNestedIndex();
			return this.MostNestedIndex;
		}

		// Token: 0x0600C2C1 RID: 49857 RVA: 0x002BA3F4 File Offset: 0x002B85F4
		protected virtual void OnColumnsOrderChanged()
		{
			TreeListColumnsOrderChangedEventArgs e = new TreeListColumnsOrderChangedEventArgs(this.reorderedColumns);
			EventHandler<TreeListColumnsOrderChangedEventArgs> eventHandler = base.Events[RadTreeList.EventColumnsOrderChanged] as EventHandler<TreeListColumnsOrderChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C2C2 RID: 49858 RVA: 0x002BA430 File Offset: 0x002B8630
		protected virtual void PerformReorder(TreeListItemDragDropEventArgs e)
		{
			if (e.Canceled || e.DraggedItems.Count == 0 || (e.DestinationDataItem == null && e.DestinationHeaderItem == null))
			{
				return;
			}
			DataSourceView data = base.GetData();
			if (data.CanUpdate)
			{
				this.ReorderContext = new TreeListReorderContext(this, e);
				try
				{
					foreach (TreeListDataItem treeListDataItem in e.DraggedItems)
					{
						Hashtable hashtable = new Hashtable();
						this.FillDataKeys(hashtable, treeListDataItem);
						Hashtable hashtable2 = new Hashtable();
						this.ExtractValuesFromItem(hashtable2, treeListDataItem, false);
						this.ReorderContext.AddReorderedItemData(treeListDataItem.HierarchyIndex, hashtable, hashtable2);
					}
					if (e.DestinationDataItem != null && e.ExpandTargetItem && !e.DestinationDataItem.Expanded)
					{
						this.ExpandedIndexes.Add(e.DestinationDataItem.HierarchyIndex);
					}
					this.Rebind();
				}
				finally
				{
					this.Rebind();
					this.ReorderContext = null;
				}
			}
		}

		// Token: 0x0600C2C3 RID: 49859 RVA: 0x002BA548 File Offset: 0x002B8748
		protected virtual void ExtractParentKeyValuesForChild(IDictionary keys, TreeListDataItem parentDataItem)
		{
			if (parentDataItem != null)
			{
				int num = 0;
				foreach (string key in this.ParentDataKeyNames)
				{
					if (keys.Contains(key))
					{
						keys[key] = this.DataKeyValues[parentDataItem.DisplayIndex][this.DataKeyNames[num++]];
					}
					else
					{
						keys.Add(key, this.DataKeyValues[parentDataItem.DisplayIndex][this.DataKeyNames[num++]]);
					}
				}
				return;
			}
			foreach (string key2 in this.ParentDataKeyNames)
			{
				if (keys.Contains(key2))
				{
					keys[key2] = null;
				}
				else
				{
					keys.Add(key2, null);
				}
			}
		}

		// Token: 0x17003EB6 RID: 16054
		// (get) Token: 0x0600C2C4 RID: 49860 RVA: 0x002BA612 File Offset: 0x002B8812
		// (set) Token: 0x0600C2C5 RID: 49861 RVA: 0x002BA61A File Offset: 0x002B881A
		[Description("Gets or sets the name of the method to call in order to update data")]
		[DefaultValue("")]
		[Category("Data")]
		[NotifyParentProperty(true)]
		public new string UpdateMethod
		{
			get
			{
				return base.UpdateMethod;
			}
			set
			{
				base.UpdateMethod = value;
			}
		}

		// Token: 0x17003EB7 RID: 16055
		// (get) Token: 0x0600C2C6 RID: 49862 RVA: 0x002BA623 File Offset: 0x002B8823
		// (set) Token: 0x0600C2C7 RID: 49863 RVA: 0x002BA62B File Offset: 0x002B882B
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Data")]
		[Description("Gets or sets the name of the method to call in order to insert data")]
		public new string InsertMethod
		{
			get
			{
				return base.InsertMethod;
			}
			set
			{
				base.InsertMethod = value;
			}
		}

		// Token: 0x17003EB8 RID: 16056
		// (get) Token: 0x0600C2C8 RID: 49864 RVA: 0x002BA634 File Offset: 0x002B8834
		// (set) Token: 0x0600C2C9 RID: 49865 RVA: 0x002BA63C File Offset: 0x002B883C
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("Gets or sets the name of the method to call in order to delete data")]
		public new string DeleteMethod
		{
			get
			{
				return base.DeleteMethod;
			}
			set
			{
				base.DeleteMethod = value;
			}
		}

		// Token: 0x17003EB9 RID: 16057
		// (get) Token: 0x0600C2CA RID: 49866 RVA: 0x002BA648 File Offset: 0x002B8848
		// (set) Token: 0x0600C2CB RID: 49867 RVA: 0x002BA6AD File Offset: 0x002B88AD
		public TreeListCommandItemDisplay CommandItemDisplay
		{
			get
			{
				object obj = this.ViewState["CommandItemDisplay"];
				if (obj != null && (this.ResolvedRenderMode == RenderMode.Lightweight || this.ResolvedRenderMode == RenderMode.Mobile))
				{
					return (TreeListCommandItemDisplay)obj;
				}
				if (this.ResolvedRenderMode == RenderMode.Mobile && (this.ClientSettings.AllowColumnHide || this.ClientSettings.Reordering.AllowColumnsReorder))
				{
					return TreeListCommandItemDisplay.Top;
				}
				return TreeListCommandItemDisplay.None;
			}
			set
			{
				this.ViewState["CommandItemDisplay"] = value;
			}
		}

		// Token: 0x17003EBA RID: 16058
		// (get) Token: 0x0600C2CC RID: 49868 RVA: 0x002BA6C8 File Offset: 0x002B88C8
		// (set) Token: 0x0600C2CD RID: 49869 RVA: 0x002BA700 File Offset: 0x002B8900
		[TypeConverter(typeof(TreeListStringArrayConverter))]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Category("Client")]
		[Description("Comma delimited list of data-field Names")]
		public virtual string[] ClientDataKeyNames
		{
			get
			{
				object obj = this.ViewState["ClientDataKeyNames"] ?? new string[0];
				return (string[])((string[])obj).Clone();
			}
			set
			{
				if (!TreeListArrayComparerHelper.CompareStringArrays(value, this.ClientDataKeyNamesInternal))
				{
					this.ViewState["ClientDataKeyNames"] = ((value != null) ? value.Clone() : null);
					this.ClientDataKeysArrayList.Clear();
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x17003EBB RID: 16059
		// (get) Token: 0x0600C2CE RID: 49870 RVA: 0x002BA73D File Offset: 0x002B893D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual TreeListDataKeyArray ClientDataKeyValues
		{
			get
			{
				if (this._clientDataKeyValues == null)
				{
					this._clientDataKeyValues = new TreeListDataKeyArray(this.ClientDataKeysArrayList);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._clientDataKeyValues).TrackViewState();
					}
				}
				return this._clientDataKeyValues;
			}
		}

		// Token: 0x17003EBC RID: 16060
		// (get) Token: 0x0600C2CF RID: 49871 RVA: 0x002BA774 File Offset: 0x002B8974
		// (set) Token: 0x0600C2D0 RID: 49872 RVA: 0x002BA7AC File Offset: 0x002B89AC
		[Category("Data")]
		[Description("Comma delimited list of data-field Names")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(TreeListStringArrayConverter))]
		public virtual string[] DataKeyNames
		{
			get
			{
				object obj = this.ViewState["DataKeyNames"] ?? new string[0];
				return (string[])((string[])obj).Clone();
			}
			set
			{
				if (!TreeListArrayComparerHelper.CompareStringArrays(value, this.DataKeyNamesInternal))
				{
					this.ViewState["DataKeyNames"] = ((value != null) ? value.Clone() : null);
					this.DataKeysArrayList.Clear();
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x17003EBD RID: 16061
		// (get) Token: 0x0600C2D1 RID: 49873 RVA: 0x002BA7E9 File Offset: 0x002B89E9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual TreeListDataKeyArray DataKeyValues
		{
			get
			{
				if (this._dataKeyValues == null)
				{
					this._dataKeyValues = new TreeListDataKeyArray(this.DataKeysArrayList);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._dataKeyValues).TrackViewState();
					}
				}
				return this._dataKeyValues;
			}
		}

		// Token: 0x17003EBE RID: 16062
		// (get) Token: 0x0600C2D2 RID: 49874 RVA: 0x002BA820 File Offset: 0x002B8A20
		// (set) Token: 0x0600C2D3 RID: 49875 RVA: 0x002BA858 File Offset: 0x002B8A58
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(TreeListStringArrayConverter))]
		[DefaultValue(null)]
		[Category("Data")]
		[NotifyParentProperty(true)]
		[Description("Comma delimited list of data-field Names")]
		public virtual string[] ParentDataKeyNames
		{
			get
			{
				object obj = this.ViewState["ParentDataKeyNames"] ?? new string[0];
				return (string[])((string[])obj).Clone();
			}
			set
			{
				if (!TreeListArrayComparerHelper.CompareStringArrays(value, this.ParentDataKeyNamesInternal))
				{
					this.ViewState["ParentDataKeyNames"] = ((value != null) ? value.Clone() : null);
					this.ParentDataKeysArrayList.Clear();
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x0600C2D4 RID: 49876 RVA: 0x002BA898 File Offset: 0x002B8A98
		public TreeListDataItem FindItemByKeyValue(string keyName, object keyValue)
		{
			TreeListDataItem result = null;
			foreach (TreeListDataItem treeListDataItem in this.Items)
			{
				object dataKeyValue = treeListDataItem.GetDataKeyValue(keyName);
				object parentDataKeyValue = treeListDataItem.GetParentDataKeyValue(keyName);
				if ((dataKeyValue != null && dataKeyValue.Equals(keyValue)) || (parentDataKeyValue != null && parentDataKeyValue.Equals(keyValue)))
				{
					result = treeListDataItem;
					break;
				}
			}
			return result;
		}

		// Token: 0x17003EBF RID: 16063
		// (get) Token: 0x0600C2D5 RID: 49877 RVA: 0x002BA918 File Offset: 0x002B8B18
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual TreeListDataKeyArray ParentDataKeyValues
		{
			get
			{
				if (this._parentDataKeyValues == null)
				{
					this._parentDataKeyValues = new TreeListDataKeyArray(this.ParentDataKeysArrayList);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._parentDataKeyValues).TrackViewState();
					}
				}
				return this._parentDataKeyValues;
			}
		}

		// Token: 0x17003EC0 RID: 16064
		// (get) Token: 0x0600C2D6 RID: 49878 RVA: 0x002BA94C File Offset: 0x002B8B4C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TreeListClientSettings ClientSettings
		{
			get
			{
				if (this._clientSettings == null)
				{
					this._clientSettings = new TreeListClientSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._clientSettings).TrackViewState();
					}
				}
				return this._clientSettings;
			}
		}

		// Token: 0x17003EC1 RID: 16065
		// (get) Token: 0x0600C2D7 RID: 49879 RVA: 0x002BA97B File Offset: 0x002B8B7B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public TreeListCommandItemSettings CommandItemSettings
		{
			get
			{
				if (this._commandItemSettings == null)
				{
					this._commandItemSettings = new TreeListCommandItemSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._commandItemSettings).TrackViewState();
					}
				}
				return this._commandItemSettings;
			}
		}

		// Token: 0x17003EC2 RID: 16066
		// (get) Token: 0x0600C2D8 RID: 49880 RVA: 0x002BA9AA File Offset: 0x002B8BAA
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Export")]
		public TreeListExportSettings ExportSettings
		{
			get
			{
				if (this._exportSettings == null)
				{
					this._exportSettings = new TreeListExportSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._exportSettings).TrackViewState();
					}
				}
				return this._exportSettings;
			}
		}

		// Token: 0x17003EC3 RID: 16067
		// (get) Token: 0x0600C2D9 RID: 49881 RVA: 0x002BA9D8 File Offset: 0x002B8BD8
		// (set) Token: 0x0600C2DA RID: 49882 RVA: 0x002BAA01 File Offset: 0x002B8C01
		[Bindable(true)]
		[SimplePersistenceSetting]
		[Browsable(false)]
		[Description("Gets or sets a value indicating the index of the currently active page in case paging is enabled")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17003EC4 RID: 16068
		// (get) Token: 0x0600C2DB RID: 49883 RVA: 0x002BAA28 File Offset: 0x002B8C28
		// (set) Token: 0x0600C2DC RID: 49884 RVA: 0x002BAA54 File Offset: 0x002B8C54
		[NotifyParentProperty(true)]
		[Category("Paging")]
		[DefaultValue(10)]
		[SimplePersistenceSetting]
		[Description("Specify the maximum number of items that would appear in a page,when paging is enabled by AllowPaging property.")]
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
					TreeListPageSizeChangedEventArgs treeListPageSizeChangedEventArgs = new TreeListPageSizeChangedEventArgs(null, null, value);
					this.FirePageSizeChanged(treeListPageSizeChangedEventArgs);
					if (treeListPageSizeChangedEventArgs.Canceled)
					{
						return;
					}
					this.CurrentPageIndex = 0;
					this.SetRequiresDataBindingIfInitialized();
				}
				this.ControlState["PageSize"] = value;
			}
		}

		// Token: 0x17003EC5 RID: 16069
		// (get) Token: 0x0600C2DD RID: 49885 RVA: 0x002BAAD0 File Offset: 0x002B8CD0
		// (set) Token: 0x0600C2DE RID: 49886 RVA: 0x002BAB2F File Offset: 0x002B8D2F
		[Category("Layout")]
		[Description("Gets or sets a value indicating the width of the RadTreeList's ExpandCollapse column.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		public Unit ExpandCollapseColumnWidth
		{
			get
			{
				object obj = this.ViewState["ExpandCollapseColumnWidth"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				if (this.ResolvedRenderMode == RenderMode.Lightweight)
				{
					return new Unit(36.0, UnitType.Pixel);
				}
				if (this.ResolvedRenderMode == RenderMode.Mobile)
				{
					return new Unit(3.0, UnitType.Em);
				}
				return Unit.Empty;
			}
			set
			{
				this.ViewState["ExpandCollapseColumnWidth"] = value;
			}
		}

		// Token: 0x17003EC6 RID: 16070
		// (get) Token: 0x0600C2DF RID: 49887 RVA: 0x002BAB48 File Offset: 0x002B8D48
		// (set) Token: 0x0600C2E0 RID: 49888 RVA: 0x002BAB71 File Offset: 0x002B8D71
		[NotifyParentProperty(true)]
		[Category("General")]
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether the TreeListItem's child items loaded on demand is enabled.")]
		public virtual bool AllowLoadOnDemand
		{
			get
			{
				object obj = this.ViewState["AllowLoadOnDemand"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowLoadOnDemand"] = value;
			}
		}

		// Token: 0x17003EC7 RID: 16071
		// (get) Token: 0x0600C2E1 RID: 49889 RVA: 0x002BAB8C File Offset: 0x002B8D8C
		// (set) Token: 0x0600C2E2 RID: 49890 RVA: 0x002BABB5 File Offset: 0x002B8DB5
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Category("General")]
		[Description("Gets or sets a value indicating whether the expand/collapse image of the treelist item should be visible when it does not have child items.")]
		public virtual bool HideExpandCollapseButtonIfNoChildren
		{
			get
			{
				object obj = this.ViewState["HideExpandCollapseButtonIfNoChildren"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["HideExpandCollapseButtonIfNoChildren"] = value;
			}
		}

		// Token: 0x17003EC8 RID: 16072
		// (get) Token: 0x0600C2E3 RID: 49891 RVA: 0x002BABD0 File Offset: 0x002B8DD0
		// (set) Token: 0x0600C2E4 RID: 49892 RVA: 0x002BABF9 File Offset: 0x002B8DF9
		[Category("Paging")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether the automatic paging feature is enabled.")]
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

		// Token: 0x17003EC9 RID: 16073
		// (get) Token: 0x0600C2E5 RID: 49893 RVA: 0x002BAC14 File Offset: 0x002B8E14
		[Description("Gets the number of pages required to display the records of the data source in a RadTreeList control.")]
		[Category("Paging")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int PageCount
		{
			get
			{
				if (this._resolvedDataSource != null)
				{
					return this._resolvedDataSource.PagingManager.PageCount;
				}
				object obj = this.ControlState["_!PCount"];
				if (obj == null)
				{
					return 1;
				}
				return (int)obj;
			}
		}

		// Token: 0x17003ECA RID: 16074
		// (get) Token: 0x0600C2E6 RID: 49894 RVA: 0x002BAC56 File Offset: 0x002B8E56
		// (set) Token: 0x0600C2E7 RID: 49895 RVA: 0x002BAC71 File Offset: 0x002B8E71
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		[Browsable(false)]
		public TreeListExpandedIndexesCollection ClientExpandedIndexes
		{
			get
			{
				if (this._clientExpandedIndexes == null)
				{
					this._clientExpandedIndexes = new TreeListExpandedIndexesCollection();
				}
				return this._clientExpandedIndexes;
			}
			internal set
			{
				this._clientExpandedIndexes = value;
			}
		}

		// Token: 0x17003ECB RID: 16075
		// (get) Token: 0x0600C2E8 RID: 49896 RVA: 0x002BAC7C File Offset: 0x002B8E7C
		// (set) Token: 0x0600C2E9 RID: 49897 RVA: 0x002BACDB File Offset: 0x002B8EDB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		[Browsable(false)]
		public TreeListExpandedIndexesCollection ExpandedIndexes
		{
			get
			{
				if (this._expandedIndexes == null)
				{
					this._expandedIndexes = (TreeListExpandedIndexesCollection)this.ControlState["_!ExpandedItem"];
					if (this._expandedIndexes == null)
					{
						this._expandedIndexes = new TreeListExpandedIndexesCollection();
						this.ControlState["_!ExpandedItem"] = this._expandedIndexes;
					}
				}
				return this._expandedIndexes;
			}
			internal set
			{
				this._expandedIndexes = value;
			}
		}

		// Token: 0x17003ECC RID: 16076
		// (get) Token: 0x0600C2EA RID: 49898 RVA: 0x002BACE4 File Offset: 0x002B8EE4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal Dictionary<TreeListHierarchyIndex, List<TreeListHierarchyIndex>> FooterItems
		{
			get
			{
				if (this._footerItems == null)
				{
					this._footerItems = (Dictionary<TreeListHierarchyIndex, List<TreeListHierarchyIndex>>)this.ControlState["_!FooterItemState"];
					if (this._footerItems == null)
					{
						this._footerItems = new Dictionary<TreeListHierarchyIndex, List<TreeListHierarchyIndex>>();
						this.ControlState["_!FooterItemState"] = this._footerItems;
					}
				}
				return this._footerItems;
			}
		}

		// Token: 0x17003ECD RID: 16077
		// (get) Token: 0x0600C2EB RID: 49899 RVA: 0x002BAD44 File Offset: 0x002B8F44
		// (set) Token: 0x0600C2EC RID: 49900 RVA: 0x002BADA3 File Offset: 0x002B8FA3
		[SimplePersistenceSetting]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeListSelectedIndexesCollection SelectedIndexes
		{
			get
			{
				if (this._selectedIndexes == null)
				{
					this._selectedIndexes = (TreeListSelectedIndexesCollection)this.ControlState["_!SelectedItems"];
					if (this._selectedIndexes == null)
					{
						this._selectedIndexes = new TreeListSelectedIndexesCollection();
						this.ControlState["_!SelectedItems"] = this._selectedIndexes;
					}
				}
				return this._selectedIndexes;
			}
			internal set
			{
				this._selectedIndexes = value;
			}
		}

		// Token: 0x17003ECE RID: 16078
		// (get) Token: 0x0600C2ED RID: 49901 RVA: 0x002BADAC File Offset: 0x002B8FAC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TreeListDataItemCollection SelectedItems
		{
			get
			{
				TreeListDataItemCollection treeListDataItemCollection = new TreeListDataItemCollection();
				foreach (TreeListDataItem treeListDataItem in this.Items)
				{
					if (treeListDataItem.Selected)
					{
						treeListDataItemCollection.Add(treeListDataItem);
					}
				}
				return treeListDataItemCollection;
			}
		}

		// Token: 0x17003ECF RID: 16079
		// (get) Token: 0x0600C2EE RID: 49902 RVA: 0x002BAE10 File Offset: 0x002B9010
		// (set) Token: 0x0600C2EF RID: 49903 RVA: 0x002BAE6F File Offset: 0x002B906F
		[SimplePersistenceSetting]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeListEditIndexesCollection EditIndexes
		{
			get
			{
				if (this._editIndexes == null)
				{
					this._editIndexes = (TreeListEditIndexesCollection)this.ControlState["_!EditItems"];
					if (this._editIndexes == null)
					{
						this._editIndexes = new TreeListEditIndexesCollection();
						this.ControlState["_!EditItems"] = this._editIndexes;
					}
				}
				return this._editIndexes;
			}
			internal set
			{
				this._editIndexes = value;
			}
		}

		// Token: 0x17003ED0 RID: 16080
		// (get) Token: 0x0600C2F0 RID: 49904 RVA: 0x002BAE78 File Offset: 0x002B9078
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeListEditItemCollection EditItems
		{
			get
			{
				TreeListEditItemCollection treeListEditItemCollection = new TreeListEditItemCollection();
				foreach (TreeListDataItem treeListDataItem in this.Items)
				{
					if (treeListDataItem.Edit)
					{
						if (treeListDataItem.EditFormItem != null)
						{
							treeListEditItemCollection.Add(treeListDataItem.EditFormItem);
						}
						else
						{
							treeListEditItemCollection.Add(treeListDataItem);
						}
					}
				}
				return treeListEditItemCollection;
			}
		}

		// Token: 0x17003ED1 RID: 16081
		// (get) Token: 0x0600C2F1 RID: 49905 RVA: 0x002BAEF0 File Offset: 0x002B90F0
		// (set) Token: 0x0600C2F2 RID: 49906 RVA: 0x002BAF4F File Offset: 0x002B914F
		[SimplePersistenceSetting]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeListEditIndexesCollection InsertIndexes
		{
			get
			{
				if (this._insertIndexes == null)
				{
					this._insertIndexes = (TreeListEditIndexesCollection)this.ControlState["_!InsertItems"];
					if (this._insertIndexes == null)
					{
						this._insertIndexes = new TreeListEditIndexesCollection();
						this.ControlState["_!InsertItems"] = this._insertIndexes;
					}
				}
				return this._insertIndexes;
			}
			internal set
			{
				this._insertIndexes = value;
			}
		}

		// Token: 0x17003ED2 RID: 16082
		// (get) Token: 0x0600C2F3 RID: 49907 RVA: 0x002BAF58 File Offset: 0x002B9158
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeListEditItemCollection InsertItems
		{
			get
			{
				TreeListEditItemCollection treeListEditItemCollection = new TreeListEditItemCollection();
				foreach (TreeListDataItem treeListDataItem in this.Items)
				{
					if (treeListDataItem.IsChildInserted)
					{
						if (treeListDataItem.InsertItem != null)
						{
							treeListEditItemCollection.Add(treeListDataItem.InsertItem);
						}
						else
						{
							treeListEditItemCollection.Add(treeListDataItem);
						}
					}
				}
				return treeListEditItemCollection;
			}
		}

		// Token: 0x17003ED3 RID: 16083
		// (get) Token: 0x0600C2F4 RID: 49908 RVA: 0x002BAFD0 File Offset: 0x002B91D0
		// (set) Token: 0x0600C2F5 RID: 49909 RVA: 0x002BAFF9 File Offset: 0x002B91F9
		[SimplePersistenceSetting]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool IsItemInserted
		{
			get
			{
				object obj = this.ViewState["IsItemInserted"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["IsItemInserted"] = value;
			}
		}

		// Token: 0x0600C2F6 RID: 49910 RVA: 0x002BB011 File Offset: 0x002B9211
		public void InsertItem()
		{
			this.InsertItem(null);
		}

		// Token: 0x0600C2F7 RID: 49911 RVA: 0x002BB01A File Offset: 0x002B921A
		public virtual void InsertItem(object newDataItem)
		{
			this._defaultInsertObjects[this] = newDataItem;
			this.IsItemInserted = true;
			this.Rebind();
		}

		// Token: 0x0600C2F8 RID: 49912 RVA: 0x002BB036 File Offset: 0x002B9236
		public void InsertChildItem(TreeListDataItem parentItem)
		{
			this.InsertChildItem(parentItem, null);
		}

		// Token: 0x0600C2F9 RID: 49913 RVA: 0x002BB040 File Offset: 0x002B9240
		public virtual void InsertChildItem(TreeListDataItem parentItem, object newDataItem)
		{
			if (parentItem != null)
			{
				IDictionary dictionary = newDataItem as IDictionary;
				if (dictionary != null)
				{
					this._defaultInsertValues = dictionary;
				}
				else
				{
					this._defaultInsertObjects[parentItem.HierarchyIndex] = newDataItem;
				}
				parentItem.IsChildInserted = true;
				this.Rebind();
			}
		}

		// Token: 0x17003ED4 RID: 16084
		// (get) Token: 0x0600C2FA RID: 49914 RVA: 0x002BB084 File Offset: 0x002B9284
		// (set) Token: 0x0600C2FB RID: 49915 RVA: 0x002BB0B7 File Offset: 0x002B92B7
		[Category("Behavior")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool AllowMultiItemSelection
		{
			get
			{
				if (this.AllowRecursiveSelection)
				{
					return true;
				}
				object obj = this.ControlState["_!AllowMultiItemSelection"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ControlState["_!AllowMultiItemSelection"] = value;
			}
		}

		// Token: 0x17003ED5 RID: 16085
		// (get) Token: 0x0600C2FC RID: 49916 RVA: 0x002BB0D0 File Offset: 0x002B92D0
		// (set) Token: 0x0600C2FD RID: 49917 RVA: 0x002BB0F9 File Offset: 0x002B92F9
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool AllowMultiItemEdit
		{
			get
			{
				object obj = this.ControlState["_!AllowMultiItemEdit"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ControlState["_!AllowMultiItemEdit"] = value;
			}
		}

		// Token: 0x17003ED6 RID: 16086
		// (get) Token: 0x0600C2FE RID: 49918 RVA: 0x002BB111 File Offset: 0x002B9311
		// (set) Token: 0x0600C2FF RID: 49919 RVA: 0x002BB13F File Offset: 0x002B933F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SimplePersistenceSetting]
		public virtual TreeListSortExpressionCollection SortExpressions
		{
			get
			{
				if (this._sortExpressions == null)
				{
					this._sortExpressions = new TreeListSortExpressionCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._sortExpressions).TrackViewState();
					}
				}
				return this._sortExpressions;
			}
			internal set
			{
				this._sortExpressions = value;
			}
		}

		// Token: 0x17003ED7 RID: 16087
		// (get) Token: 0x0600C300 RID: 49920 RVA: 0x002BB148 File Offset: 0x002B9348
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual TreeListSortingSettings SortingSettings
		{
			get
			{
				if (this._sortingSettings == null)
				{
					this._sortingSettings = new TreeListSortingSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._sortingSettings).TrackViewState();
					}
				}
				return this._sortingSettings;
			}
		}

		// Token: 0x17003ED8 RID: 16088
		// (get) Token: 0x0600C301 RID: 49921 RVA: 0x002BB177 File Offset: 0x002B9377
		// (set) Token: 0x0600C302 RID: 49922 RVA: 0x002BB184 File Offset: 0x002B9384
		[Description("Gets or sets a value indicating whether the multi column sorting feature is enabled.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool AllowMultiColumnSorting
		{
			get
			{
				return this.SortExpressions.AllowMultiColumnSorting;
			}
			set
			{
				this.SortExpressions.AllowMultiColumnSorting = value;
			}
		}

		// Token: 0x17003ED9 RID: 16089
		// (get) Token: 0x0600C303 RID: 49923 RVA: 0x002BB192 File Offset: 0x002B9392
		// (set) Token: 0x0600C304 RID: 49924 RVA: 0x002BB19F File Offset: 0x002B939F
		[Category("Behavior")]
		[Description("Allow the no-sort state when changing sort order.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool AllowNaturalSort
		{
			get
			{
				return this.SortExpressions.AllowNaturalSort;
			}
			set
			{
				this.SortExpressions.AllowNaturalSort = value;
			}
		}

		// Token: 0x17003EDA RID: 16090
		// (get) Token: 0x0600C305 RID: 49925 RVA: 0x002BB1B0 File Offset: 0x002B93B0
		// (set) Token: 0x0600C306 RID: 49926 RVA: 0x002BB1D9 File Offset: 0x002B93D9
		[Category("Behavior")]
		[Description("Allow RadTreeList equal items not to be reordered when sorting.Enables sorting result consistancy between 3.5, 4.0, 4.5 Framework")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool AllowStableSort
		{
			get
			{
				object obj = this.ViewState["AllowStableSort"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowStableSort"] = value;
			}
		}

		// Token: 0x17003EDB RID: 16091
		// (get) Token: 0x0600C307 RID: 49927 RVA: 0x002BB1F4 File Offset: 0x002B93F4
		// (set) Token: 0x0600C308 RID: 49928 RVA: 0x002BB21D File Offset: 0x002B941D
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
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

		// Token: 0x1400018E RID: 398
		// (add) Token: 0x0600C309 RID: 49929 RVA: 0x002BB235 File Offset: 0x002B9435
		// (remove) Token: 0x0600C30A RID: 49930 RVA: 0x002BB248 File Offset: 0x002B9448
		[Category("Action")]
		[Description("Fires when Sort has been changed.")]
		public event EventHandler<TreeListSortEventArgs> Sorting
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventSorting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventSorting, value);
			}
		}

		// Token: 0x0600C30B RID: 49931 RVA: 0x002BB25C File Offset: 0x002B945C
		protected virtual void OnSorting(TreeListSortEventArgs e)
		{
			EventHandler<TreeListSortEventArgs> eventHandler = base.Events[RadTreeList.EventSorting] as EventHandler<TreeListSortEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C30C RID: 49932 RVA: 0x002BB28C File Offset: 0x002B948C
		internal void FireSorting(TreeListSortEventArgs e)
		{
			string text = e.SortExpression;
			if (!text.ToUpper().Contains(" ASC") && !text.ToUpper().Contains(" DESC"))
			{
				text = e.SortExpression + " " + e.NewSortOrder;
			}
			this.TrackSorting(text);
			this.OnSorting(e);
		}

		// Token: 0x1400018F RID: 399
		// (add) Token: 0x0600C30D RID: 49933 RVA: 0x002BB2EE File Offset: 0x002B94EE
		// (remove) Token: 0x0600C30E RID: 49934 RVA: 0x002BB301 File Offset: 0x002B9501
		[Description("Fires when an item is edited.")]
		[Category("Action")]
		public event EventHandler<TreeListCommandEventArgs> EditCommand
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventEditCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventEditCommand, value);
			}
		}

		// Token: 0x0600C30F RID: 49935 RVA: 0x002BB314 File Offset: 0x002B9514
		protected virtual void OnEditCommand(TreeListCommandEventArgs e)
		{
			EventHandler<TreeListCommandEventArgs> eventHandler = base.Events[RadTreeList.EventEditCommand] as EventHandler<TreeListCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C310 RID: 49936 RVA: 0x002BB342 File Offset: 0x002B9542
		internal void CallOnEditCommand(TreeListCommandEventArgs e)
		{
			this.OnEditCommand(e);
		}

		// Token: 0x14000190 RID: 400
		// (add) Token: 0x0600C311 RID: 49937 RVA: 0x002BB34B File Offset: 0x002B954B
		// (remove) Token: 0x0600C312 RID: 49938 RVA: 0x002BB35E File Offset: 0x002B955E
		[Description("Fires when an item is inserted.")]
		[Category("Action")]
		public event EventHandler<TreeListCommandEventArgs> InsertCommand
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventInsertCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventInsertCommand, value);
			}
		}

		// Token: 0x0600C313 RID: 49939 RVA: 0x002BB374 File Offset: 0x002B9574
		protected virtual void OnInsertCommand(TreeListCommandEventArgs e)
		{
			EventHandler<TreeListCommandEventArgs> eventHandler = base.Events[RadTreeList.EventInsertCommand] as EventHandler<TreeListCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C314 RID: 49940 RVA: 0x002BB3A2 File Offset: 0x002B95A2
		internal void CallOnInsertCommand(TreeListCommandEventArgs e)
		{
			this.OnInsertCommand(e);
		}

		// Token: 0x14000191 RID: 401
		// (add) Token: 0x0600C315 RID: 49941 RVA: 0x002BB3AB File Offset: 0x002B95AB
		// (remove) Token: 0x0600C316 RID: 49942 RVA: 0x002BB3BE File Offset: 0x002B95BE
		[Description("Fires when an item is updated.")]
		[Category("Action")]
		public event EventHandler<TreeListCommandEventArgs> UpdateCommand
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventUpdateCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventUpdateCommand, value);
			}
		}

		// Token: 0x0600C317 RID: 49943 RVA: 0x002BB3D4 File Offset: 0x002B95D4
		protected virtual void OnUpdateCommand(TreeListCommandEventArgs e)
		{
			EventHandler<TreeListCommandEventArgs> eventHandler = base.Events[RadTreeList.EventUpdateCommand] as EventHandler<TreeListCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C318 RID: 49944 RVA: 0x002BB402 File Offset: 0x002B9602
		internal void CallOnUpdateCommand(TreeListCommandEventArgs e)
		{
			this.OnUpdateCommand(e);
		}

		// Token: 0x14000192 RID: 402
		// (add) Token: 0x0600C319 RID: 49945 RVA: 0x002BB40B File Offset: 0x002B960B
		// (remove) Token: 0x0600C31A RID: 49946 RVA: 0x002BB41E File Offset: 0x002B961E
		[Description("Fires when an item is deleted.")]
		[Category("Action")]
		public event EventHandler<TreeListCommandEventArgs> DeleteCommand
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventDeleteCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventDeleteCommand, value);
			}
		}

		// Token: 0x0600C31B RID: 49947 RVA: 0x002BB434 File Offset: 0x002B9634
		protected virtual void OnDeleteCommand(TreeListCommandEventArgs e)
		{
			EventHandler<TreeListCommandEventArgs> eventHandler = base.Events[RadTreeList.EventDeleteCommand] as EventHandler<TreeListCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C31C RID: 49948 RVA: 0x002BB462 File Offset: 0x002B9662
		internal void CallOnDeleteCommand(TreeListCommandEventArgs e)
		{
			this.OnDeleteCommand(e);
		}

		// Token: 0x14000193 RID: 403
		// (add) Token: 0x0600C31D RID: 49949 RVA: 0x002BB46B File Offset: 0x002B966B
		// (remove) Token: 0x0600C31E RID: 49950 RVA: 0x002BB47E File Offset: 0x002B967E
		[Category("Action")]
		[Description("Fires when a data insert or update is canceled.")]
		public event EventHandler<TreeListCommandEventArgs> CancelCommand
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventCancelCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventCancelCommand, value);
			}
		}

		// Token: 0x0600C31F RID: 49951 RVA: 0x002BB494 File Offset: 0x002B9694
		protected virtual void OnCancelCommand(TreeListCommandEventArgs e)
		{
			EventHandler<TreeListCommandEventArgs> eventHandler = base.Events[RadTreeList.EventCancelCommand] as EventHandler<TreeListCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C320 RID: 49952 RVA: 0x002BB4C2 File Offset: 0x002B96C2
		internal void CallOnCancelCommand(TreeListCommandEventArgs e)
		{
			this.OnCancelCommand(e);
		}

		// Token: 0x14000194 RID: 404
		// (add) Token: 0x0600C321 RID: 49953 RVA: 0x002BB4CB File Offset: 0x002B96CB
		// (remove) Token: 0x0600C322 RID: 49954 RVA: 0x002BB4DE File Offset: 0x002B96DE
		[Category("Action")]
		[Description("Fires when a column editor is initialized in an editable item.")]
		public event EventHandler<TreeListCreateColumnEditorEventArgs> CreateColumnEditor
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventCreateColumnEditor, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventCreateColumnEditor, value);
			}
		}

		// Token: 0x0600C323 RID: 49955 RVA: 0x002BB4F4 File Offset: 0x002B96F4
		protected virtual void OnCreateColumnEditor(TreeListCreateColumnEditorEventArgs e)
		{
			EventHandler<TreeListCreateColumnEditorEventArgs> eventHandler = base.Events[RadTreeList.EventCreateColumnEditor] as EventHandler<TreeListCreateColumnEditorEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C324 RID: 49956 RVA: 0x002BB522 File Offset: 0x002B9722
		internal void CallCreateColumnEditor(TreeListCreateColumnEditorEventArgs e)
		{
			this.OnCreateColumnEditor(e);
		}

		// Token: 0x0600C325 RID: 49957 RVA: 0x002BB52B File Offset: 0x002B972B
		internal bool IsCreateColumnEditorHandled()
		{
			return base.Events[RadTreeList.EventCreateColumnEditor] is EventHandler<TreeListCreateColumnEditorEventArgs>;
		}

		// Token: 0x0600C326 RID: 49958 RVA: 0x002BB548 File Offset: 0x002B9748
		internal TreeListCreateCustomEditorDelegate GetCustomEditorInitializer(TreeListEditableColumn editableColumn, ITreeListColumnEditor defaultEditor)
		{
			if (this.IsCreateColumnEditorHandled())
			{
				if (this._customEditorInitializers.ContainsKey(editableColumn))
				{
					return this._customEditorInitializers[editableColumn];
				}
				TreeListCreateColumnEditorEventArgs treeListCreateColumnEditorEventArgs = new TreeListCreateColumnEditorEventArgs(editableColumn, defaultEditor);
				this.CallCreateColumnEditor(treeListCreateColumnEditorEventArgs);
				if (treeListCreateColumnEditorEventArgs.CustomEditorInitializer != null)
				{
					this._customEditorInitializers[editableColumn] = treeListCreateColumnEditorEventArgs.CustomEditorInitializer;
					return treeListCreateColumnEditorEventArgs.CustomEditorInitializer;
				}
				this._customEditorInitializers[editableColumn] = TreeListCreateColumnEditorEventArgs.EmptyInitializer;
			}
			return TreeListCreateColumnEditorEventArgs.EmptyInitializer;
		}

		// Token: 0x0600C327 RID: 49959 RVA: 0x002BB5BE File Offset: 0x002B97BE
		internal void ClearCustomEditorInitializers()
		{
			this._customEditorInitializers.Clear();
		}

		// Token: 0x17003EDC RID: 16092
		// (get) Token: 0x0600C328 RID: 49960 RVA: 0x002BB5CC File Offset: 0x002B97CC
		// (set) Token: 0x0600C329 RID: 49961 RVA: 0x002BB5F5 File Offset: 0x002B97F5
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool AllowRecursiveSelection
		{
			get
			{
				object obj = this.ControlState["_!AllowRecursiveSelection"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ControlState["_!AllowRecursiveSelection"] = value;
				if (value)
				{
					this.AllowMultiItemSelection = value;
				}
			}
		}

		// Token: 0x0600C32A RID: 49962 RVA: 0x002BB617 File Offset: 0x002B9817
		public virtual void ApplyRecursiveSelection(TreeListDataItem item, bool selected)
		{
			this.ApplyRecursiveSelection(item.HierarchyIndex, selected);
		}

		// Token: 0x0600C32B RID: 49963 RVA: 0x002BB644 File Offset: 0x002B9844
		public virtual void ApplyRecursiveSelection(TreeListHierarchyIndex hierarchyIndex, bool selected)
		{
			if (!this.AllowRecursiveSelection)
			{
				return;
			}
			if (!this.IsDataBinding)
			{
				this.Rebind();
			}
			TreeListDataItem treeListDataItem = this.Items.Find((TreeListDataItem item) => item.HierarchyIndex == hierarchyIndex);
			if (treeListDataItem != null)
			{
				this.AllowRecursiveSelection = false;
				bool allowMultiItemSelection = this.AllowMultiItemSelection;
				this.AllowMultiItemSelection = true;
				if (treeListDataItem.Selected != selected)
				{
					treeListDataItem.Selected = selected;
				}
				this.ApplyChildSelectionRecursive(treeListDataItem.SourceItem, selected);
				this.ApplyParentSelectionRecursive(treeListDataItem.SourceItem.ParentItem, selected);
				this.AllowRecursiveSelection = true;
				this.AllowMultiItemSelection = allowMultiItemSelection;
			}
		}

		// Token: 0x0600C32C RID: 49964 RVA: 0x002BB6E4 File Offset: 0x002B98E4
		private void ApplyChildSelectionRecursive(TreeListSourceItem sourceItem, bool selected)
		{
			foreach (TreeListSourceItem treeListSourceItem in sourceItem.ChildItems)
			{
				this.SetItemSelectedIfExists(treeListSourceItem.HierarchyIndex, selected);
				this.ApplyChildSelectionRecursive(treeListSourceItem, selected);
			}
		}

		// Token: 0x0600C32D RID: 49965 RVA: 0x002BB740 File Offset: 0x002B9940
		private void ApplyParentSelectionRecursive(TreeListSourceItem sourceItem, bool selected)
		{
			if (sourceItem == null)
			{
				return;
			}
			if (selected)
			{
				bool flag = true;
				foreach (TreeListSourceItem treeListSourceItem in sourceItem.ChildItems)
				{
					if (!this.SelectedIndexes.Contains(treeListSourceItem.HierarchyIndex))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					this.SetItemSelectedIfExists(sourceItem.HierarchyIndex, true);
				}
			}
			else
			{
				this.SetItemSelectedIfExists(sourceItem.HierarchyIndex, false);
			}
			if (sourceItem.ParentItem != null)
			{
				this.ApplyParentSelectionRecursive(sourceItem.ParentItem, selected);
			}
		}

		// Token: 0x0600C32E RID: 49966 RVA: 0x002BB7F8 File Offset: 0x002B99F8
		private void SetItemSelectedIfExists(TreeListHierarchyIndex index, bool selected)
		{
			TreeListDataItem treeListDataItem = this.Items.Find((TreeListDataItem item) => item.HierarchyIndex == index);
			if (treeListDataItem != null)
			{
				treeListDataItem.Selected = selected;
				return;
			}
			if (selected)
			{
				this.SelectedIndexes.Add(index);
				return;
			}
			this.SelectedIndexes.Remove(index);
		}

		// Token: 0x0600C32F RID: 49967 RVA: 0x002BB85C File Offset: 0x002B9A5C
		internal void SelectAllItems()
		{
			this.ApplySelectionToAllItems(true);
		}

		// Token: 0x0600C330 RID: 49968 RVA: 0x002BB865 File Offset: 0x002B9A65
		internal void DeselectAllItems()
		{
			this.ApplySelectionToAllItems(false);
		}

		// Token: 0x0600C331 RID: 49969 RVA: 0x002BB870 File Offset: 0x002B9A70
		private void ApplySelectionToAllItems(bool selected)
		{
			if (this.AllowRecursiveSelection)
			{
				if (!this.IsDataBinding)
				{
					this.Rebind();
				}
				if (this.RootItems == null)
				{
					return;
				}
				using (IEnumerator<TreeListSourceItem> enumerator = this.RootItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TreeListSourceItem treeListSourceItem = enumerator.Current;
						this.SetItemSelectedIfExists(treeListSourceItem.HierarchyIndex, selected);
						this.ApplyChildSelectionRecursive(treeListSourceItem, selected);
					}
					return;
				}
			}
			foreach (TreeListDataItem treeListDataItem in this.Items)
			{
				treeListDataItem.Selected = selected;
			}
		}

		// Token: 0x17003EDD RID: 16093
		// (get) Token: 0x0600C332 RID: 49970 RVA: 0x002BB92C File Offset: 0x002B9B2C
		// (set) Token: 0x0600C333 RID: 49971 RVA: 0x002BB934 File Offset: 0x002B9B34
		internal IList<TreeListSourceItem> RootItems { get; set; }

		// Token: 0x0600C334 RID: 49972 RVA: 0x002BB940 File Offset: 0x002B9B40
		internal bool GetAllItemsSelected()
		{
			if (this.AllowRecursiveSelection)
			{
				return this.SelectedIndexes.Count > 0 && this.SelectedIndexes.Count == this.TotalItemCount;
			}
			return this.SelectedItems.Count > 0 && this.SelectedItems.Count == this.Items.Count;
		}

		// Token: 0x0600C335 RID: 49973 RVA: 0x002BB9A1 File Offset: 0x002B9BA1
		public void ExpandAllItems()
		{
			this.SetExpandedStateToAllItems(true);
		}

		// Token: 0x0600C336 RID: 49974 RVA: 0x002BB9AA File Offset: 0x002B9BAA
		public void CollapseAllItems()
		{
			this.SetExpandedStateToAllItems(false);
		}

		// Token: 0x0600C337 RID: 49975 RVA: 0x002BB9B4 File Offset: 0x002B9BB4
		private void SetExpandedStateToAllItems(bool expanded)
		{
			if (!this.IsDataBinding)
			{
				this.Rebind();
			}
			if (this.RootItems != null)
			{
				if (this.AllowLoadOnDemand)
				{
					this.LoadOnDemandContext.ExpandedItems.Clear();
					this.LoadOnDemandContext.ExpandedItemsDataKeyValues.Clear();
					this.LoadOnDemandContext.ExpandedOnDemandIndexes.Clear();
				}
				foreach (TreeListSourceItem sourceItem in this.RootItems)
				{
					this.ToggleItemExpandedStateToLevelRecursive(sourceItem, expanded, 0, int.MaxValue);
				}
				this.CurrentPageIndex = 0;
				this.DataBind();
			}
		}

		// Token: 0x0600C338 RID: 49976 RVA: 0x002BBA64 File Offset: 0x002B9C64
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void ToggleItemExpandedStateToLevelRecursive(TreeListSourceItem sourceItem, bool expanded, int currentLevel, int maximumDepth)
		{
			bool flag = false;
			if (expanded)
			{
				if (this.ExpandHash == null)
				{
					this.ExpandHash = new HashSet<TreeListHierarchyIndex>();
					flag = true;
				}
				if (sourceItem.ChildItemsCount > 0 || this.AllowLoadOnDemand)
				{
					if (this.AllowLoadOnDemand)
					{
						this.LoadOnDemandContext.ExpandedOnDemandIndexes.Add(sourceItem.HierarchyIndex);
						this.LoadOnDemandContext.ExpansionDepth = maximumDepth;
					}
					else
					{
						this.ExpandHash.Add(sourceItem.HierarchyIndex);
					}
				}
			}
			else
			{
				this.ExpandedIndexes.Remove(sourceItem.HierarchyIndex);
			}
			if (currentLevel + 1 < maximumDepth)
			{
				foreach (TreeListSourceItem treeListSourceItem in sourceItem.ChildItems)
				{
					if (treeListSourceItem.ParentItem == sourceItem)
					{
						this.ToggleItemExpandedStateToLevelRecursive(treeListSourceItem, expanded, currentLevel + 1, maximumDepth);
					}
				}
			}
			if (flag)
			{
				this.ExpandedIndexes.AddHash(this.ExpandHash);
				this.ExpandHash = null;
			}
		}

		// Token: 0x0600C339 RID: 49977 RVA: 0x002BBB64 File Offset: 0x002B9D64
		public void ExportToPdf()
		{
			TreeListPdfExporter treeListPdfExporter = new TreeListPdfExporter(this);
			this.IsExporting = true;
			treeListPdfExporter.ExportToPdf();
		}

		// Token: 0x0600C33A RID: 49978 RVA: 0x002BBB88 File Offset: 0x002B9D88
		public void ExportToExcel()
		{
			TreeListExportInfrastructureExporter treeListExportInfrastructureExporter = new TreeListExportInfrastructureExporter(this);
			this.IsExporting = true;
			treeListExportInfrastructureExporter.ExportToExcel();
		}

		// Token: 0x0600C33B RID: 49979 RVA: 0x002BBBAC File Offset: 0x002B9DAC
		public void ExportToWord()
		{
			TreeListExportInfrastructureExporter treeListExportInfrastructureExporter = new TreeListExportInfrastructureExporter(this);
			this.IsExporting = true;
			treeListExportInfrastructureExporter.ExportToWord();
		}

		// Token: 0x0600C33C RID: 49980 RVA: 0x002BBBD0 File Offset: 0x002B9DD0
		public void ExpandToLevel(int level)
		{
			if (level <= 0)
			{
				return;
			}
			if (!this.IsDataBinding)
			{
				this.Rebind();
			}
			if (this.RootItems != null)
			{
				foreach (TreeListSourceItem sourceItem in this.RootItems)
				{
					this.ToggleItemExpandedStateToLevelRecursive(sourceItem, true, 0, level);
				}
			}
			this.CurrentPageIndex = 0;
			this.DataBind();
		}

		// Token: 0x0600C33D RID: 49981 RVA: 0x002BBC68 File Offset: 0x002B9E68
		public void ExpandItemToLevel(TreeListDataItem item, int level)
		{
			if (level <= 0)
			{
				return;
			}
			if (!this.IsDataBinding)
			{
				this.Rebind();
			}
			TreeListDataItem treeListDataItem = this.Items.Find((TreeListDataItem treeListItem) => treeListItem.HierarchyIndex == item.HierarchyIndex);
			if (treeListDataItem != null)
			{
				this.ToggleItemExpandedStateToLevelRecursive(treeListDataItem.SourceItem, true, 0, level);
			}
			this.DataBind();
		}

		// Token: 0x17003EDE RID: 16094
		// (get) Token: 0x0600C33E RID: 49982 RVA: 0x002BBCC8 File Offset: 0x002B9EC8
		// (set) Token: 0x0600C33F RID: 49983 RVA: 0x002BBCF1 File Offset: 0x002B9EF1
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

		// Token: 0x0600C340 RID: 49984 RVA: 0x002BBD0C File Offset: 0x002B9F0C
		private TreeListColumn FindColumnByOrderIndex(int OrderIndex)
		{
			TreeListColumn result = null;
			foreach (TreeListColumn treeListColumn in this.RenderColumns)
			{
				if (treeListColumn.OrderIndex == OrderIndex)
				{
					result = treeListColumn;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600C341 RID: 49985 RVA: 0x002BBD44 File Offset: 0x002B9F44
		public void SwapColumns(string columnName1, string columnName2)
		{
			TreeListColumn columnSafe = this.GetColumnSafe(columnName1);
			TreeListColumn columnSafe2 = this.GetColumnSafe(columnName2);
			if (columnSafe != null && columnSafe2 != null)
			{
				this.SwapColumns(columnSafe, columnSafe2);
			}
		}

		// Token: 0x0600C342 RID: 49986 RVA: 0x002BBD70 File Offset: 0x002B9F70
		public void SwapColumns(TreeListColumn column1, TreeListColumn column2)
		{
			if (!column1.Reorderable || !column2.Reorderable || !this.ClientSettings.Reordering.AllowColumnsReorder)
			{
				return;
			}
			int orderIndex = column1.OrderIndex;
			int orderIndex2 = column2.OrderIndex;
			column1.OrderIndex = orderIndex2;
			column2.OrderIndex = orderIndex;
			this.reorderedColumns = new TreeListReorderedColumn[]
			{
				new TreeListReorderedColumn(column1, orderIndex),
				new TreeListReorderedColumn(column2, orderIndex2)
			};
			if (!this.IsDataBinding)
			{
				this.Rebind();
			}
		}

		// Token: 0x0600C343 RID: 49987 RVA: 0x002BBDEC File Offset: 0x002B9FEC
		public void SwapColumns(int orderIndex1, int orderIndex2)
		{
			this.SwapColumns(this.FindColumnByOrderIndex(orderIndex1), this.FindColumnByOrderIndex(orderIndex2));
		}

		// Token: 0x0600C344 RID: 49988 RVA: 0x002BBE04 File Offset: 0x002BA004
		public void ReorderColumns(string columnName1, string columnName2)
		{
			TreeListColumn columnSafe = this.GetColumnSafe(columnName1);
			TreeListColumn columnSafe2 = this.GetColumnSafe(columnName2);
			if (columnSafe != null && columnSafe2 != null)
			{
				this.ReorderColumns(columnSafe, columnSafe2);
			}
		}

		// Token: 0x0600C345 RID: 49989 RVA: 0x002BBE30 File Offset: 0x002BA030
		public void ReorderColumns(TreeListColumn column1, TreeListColumn column2)
		{
			if (!column1.Reorderable || !column2.Reorderable || !this.ClientSettings.Reordering.AllowColumnsReorder)
			{
				return;
			}
			int num = -1;
			bool flag = false;
			bool flag2 = false;
			List<TreeListReorderedColumn> list = new List<TreeListReorderedColumn>();
			for (int i = 0; i < this.RenderColumns.Length; i++)
			{
				int orderIndex = this.RenderColumns[i].OrderIndex;
				if (flag2)
				{
					int orderIndex2 = num;
					num = this.RenderColumns[i].OrderIndex;
					this.RenderColumns[i].OrderIndex = orderIndex2;
					if (this.RenderColumns[i].OrderIndex != orderIndex)
					{
						list.Add(new TreeListReorderedColumn(this.RenderColumns[i], orderIndex));
					}
					if (this.RenderColumns[i].UniqueName == column2.UniqueName)
					{
						break;
					}
				}
				else if (flag)
				{
					if (this.RenderColumns[i].UniqueName == column1.UniqueName)
					{
						column1.OrderIndex = num;
						if (this.RenderColumns[i].OrderIndex != orderIndex)
						{
							list.Add(new TreeListReorderedColumn(this.RenderColumns[i], orderIndex));
							break;
						}
						break;
					}
					else
					{
						this.RenderColumns[i].OrderIndex = this.RenderColumns[i + 1].OrderIndex;
						if (this.RenderColumns[i].OrderIndex != orderIndex)
						{
							list.Add(new TreeListReorderedColumn(this.RenderColumns[i], orderIndex));
						}
					}
				}
				else
				{
					if (this.RenderColumns[i].UniqueName == column1.UniqueName)
					{
						num = column1.OrderIndex;
						column1.OrderIndex = column2.OrderIndex;
						flag2 = true;
					}
					if (this.RenderColumns[i].UniqueName == column2.UniqueName)
					{
						num = column2.OrderIndex;
						column2.OrderIndex = this.RenderColumns[i + 1].OrderIndex;
						flag = true;
					}
					if (this.RenderColumns[i].OrderIndex != orderIndex)
					{
						list.Add(new TreeListReorderedColumn(this.RenderColumns[i], orderIndex));
					}
				}
			}
			this.reorderedColumns = list.ToArray();
			if (!this.IsDataBinding)
			{
				this.Rebind();
			}
		}

		// Token: 0x0600C346 RID: 49990 RVA: 0x002BC05A File Offset: 0x002BA25A
		public void ReorderColumns(int orderIndex1, int orderIndex2)
		{
			this.ReorderColumns(this.FindColumnByOrderIndex(orderIndex1), this.FindColumnByOrderIndex(orderIndex2));
		}

		// Token: 0x17003EDF RID: 16095
		// (get) Token: 0x0600C347 RID: 49991 RVA: 0x002BC070 File Offset: 0x002BA270
		// (set) Token: 0x0600C348 RID: 49992 RVA: 0x002BC099 File Offset: 0x002BA299
		[NotifyParentProperty(true)]
		[DefaultValue(TreeListEditMode.EditForms)]
		[Category("Behavior")]
		public TreeListEditMode EditMode
		{
			get
			{
				object obj = this.ViewState["EditMode"];
				if (obj != null)
				{
					return (TreeListEditMode)obj;
				}
				return TreeListEditMode.EditForms;
			}
			set
			{
				this.ViewState["EditMode"] = value;
			}
		}

		// Token: 0x17003EE0 RID: 16096
		// (get) Token: 0x0600C349 RID: 49993 RVA: 0x002BC0B4 File Offset: 0x002BA2B4
		// (set) Token: 0x0600C34A RID: 49994 RVA: 0x002BC0DD File Offset: 0x002BA2DD
		[DefaultValue(TreeListExpandCollapseMode.Server)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public TreeListExpandCollapseMode ExpandCollapseMode
		{
			get
			{
				object obj = this.ViewState["ExpandCollapseMode"];
				if (obj != null)
				{
					return (TreeListExpandCollapseMode)obj;
				}
				return TreeListExpandCollapseMode.Server;
			}
			set
			{
				this.ViewState["ExpandCollapseMode"] = value;
			}
		}

		// Token: 0x17003EE1 RID: 16097
		// (get) Token: 0x0600C34B RID: 49995 RVA: 0x002BC0F5 File Offset: 0x002BA2F5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TreeListEditFormSettings EditFormSettings
		{
			get
			{
				if (this._editFormSettings == null)
				{
					this._editFormSettings = new TreeListEditFormSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._editFormSettings).TrackViewState();
					}
				}
				return this._editFormSettings;
			}
		}

		// Token: 0x17003EE2 RID: 16098
		// (get) Token: 0x0600C34C RID: 49996 RVA: 0x002BC124 File Offset: 0x002BA324
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public TreeListValidationSettings ValidationSettings
		{
			get
			{
				if (this._validationSettings == null)
				{
					this._validationSettings = new TreeListValidationSettings(this);
				}
				return this._validationSettings;
			}
		}

		// Token: 0x17003EE3 RID: 16099
		// (get) Token: 0x0600C34D RID: 49997 RVA: 0x002BC140 File Offset: 0x002BA340
		// (set) Token: 0x0600C34E RID: 49998 RVA: 0x002BC169 File Offset: 0x002BA369
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("Enable/Disable auto genrated columns")]
		[Category("Behavior")]
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

		// Token: 0x17003EE4 RID: 16100
		// (get) Token: 0x0600C34F RID: 49999 RVA: 0x002BC184 File Offset: 0x002BA384
		// (set) Token: 0x0600C350 RID: 50000 RVA: 0x002BC1AD File Offset: 0x002BA3AD
		[Description("Enable/Disable persisting of the columns settings for the automatically generated columns during rebind")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual bool PersistAutoGenerateColumnsStateOnRebind
		{
			get
			{
				object obj = this.ViewState["_pagcsor"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["_pagcsor"] = value;
			}
		}

		// Token: 0x17003EE5 RID: 16101
		// (get) Token: 0x0600C351 RID: 50001 RVA: 0x002BC1C5 File Offset: 0x002BA3C5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Editor("Telerik.Web.Design.RadTreeListColumnsEditorForm, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Category("Default")]
		public TreeListColumnsCollection Columns
		{
			get
			{
				if (this._declarativeColumns == null)
				{
					this._declarativeColumns = new TreeListColumnsCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._declarativeColumns).TrackViewState();
					}
				}
				return this._declarativeColumns;
			}
		}

		// Token: 0x0600C352 RID: 50002 RVA: 0x002BC1F4 File Offset: 0x002BA3F4
		public TreeListColumn GetColumn(string columnUniqueName)
		{
			TreeListColumn columnSafe = this.GetColumnSafe(columnUniqueName);
			if (columnSafe != null)
			{
				return columnSafe;
			}
			throw new ArgumentException("Cannot find column with UniqueName '" + columnUniqueName + "'");
		}

		// Token: 0x0600C353 RID: 50003 RVA: 0x002BC224 File Offset: 0x002BA424
		public TreeListColumn GetColumnSafe(string columnUniqueName)
		{
			TreeListColumn[] renderColumns = this.RenderColumns;
			foreach (TreeListColumn treeListColumn in renderColumns)
			{
				if (string.Compare(treeListColumn.UniqueName, columnUniqueName, true) == 0)
				{
					return treeListColumn;
				}
			}
			return null;
		}

		// Token: 0x17003EE6 RID: 16102
		// (get) Token: 0x0600C354 RID: 50004 RVA: 0x002BC267 File Offset: 0x002BA467
		[Description("Gets a collection of RadTreListDataItem objects that represent the data items of the current page of data in a RadTreeList control.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual TreeListDataItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new TreeListDataItemCollection();
					this.EnsureChildControls();
				}
				return this._items;
			}
		}

		// Token: 0x0600C355 RID: 50005 RVA: 0x002BC288 File Offset: 0x002BA488
		public TreeListItem[] GetItems(params TreeListItemType[] includeItemTypes)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList(includeItemTypes);
			TreeListTable treeListTable = this.GetTreeListTable();
			if (treeListTable != null)
			{
				foreach (object obj in treeListTable.Rows)
				{
					TreeListItem treeListItem = (TreeListItem)obj;
					if (arrayList2.Contains(treeListItem.ItemType))
					{
						arrayList.Add(treeListItem);
					}
				}
			}
			TreeListItem[] array = new TreeListItem[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x0600C356 RID: 50006 RVA: 0x002BC32C File Offset: 0x002BA52C
		public void ClearSelectedItems()
		{
			this.ApplySelectionToAllItems(false);
			if (this.SelectedIndexes.Count > 0)
			{
				this.SelectedIndexes.Clear();
			}
		}

		// Token: 0x17003EE7 RID: 16103
		// (get) Token: 0x0600C357 RID: 50007 RVA: 0x002BC34E File Offset: 0x002BA54E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Style")]
		[NotifyParentProperty(true)]
		[NestedStateManager]
		public virtual TreeListPagerStyle PagerStyle
		{
			get
			{
				if (this._pagerStyle == null)
				{
					this._pagerStyle = new TreeListPagerStyle(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._pagerStyle).TrackViewState();
					}
				}
				return this._pagerStyle;
			}
		}

		// Token: 0x17003EE8 RID: 16104
		// (get) Token: 0x0600C358 RID: 50008 RVA: 0x002BC37D File Offset: 0x002BA57D
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		[NestedStateManager]
		public virtual TreeListCommandItemStyle CommandItemStyle
		{
			get
			{
				if (this._commandItemStyle == null)
				{
					this._commandItemStyle = new TreeListCommandItemStyle(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._commandItemStyle).TrackViewState();
					}
				}
				return this._commandItemStyle;
			}
		}

		// Token: 0x17003EE9 RID: 16105
		// (get) Token: 0x0600C359 RID: 50009 RVA: 0x002BC3AC File Offset: 0x002BA5AC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeListTableItemStyle HeaderStyle
		{
			get
			{
				if (this._headerStyle == null)
				{
					this._headerStyle = new TreeListTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._headerStyle).TrackViewState();
					}
				}
				return this._headerStyle;
			}
		}

		// Token: 0x17003EEA RID: 16106
		// (get) Token: 0x0600C35A RID: 50010 RVA: 0x002BC3DA File Offset: 0x002BA5DA
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeListTableItemStyle ItemStyle
		{
			get
			{
				if (this._itemStyle == null)
				{
					this._itemStyle = new TreeListTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._itemStyle).TrackViewState();
					}
				}
				return this._itemStyle;
			}
		}

		// Token: 0x17003EEB RID: 16107
		// (get) Token: 0x0600C35B RID: 50011 RVA: 0x002BC408 File Offset: 0x002BA608
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		public TreeListTableItemStyle AlternatingItemStyle
		{
			get
			{
				if (this._alternatingItemStyle == null)
				{
					this._alternatingItemStyle = new TreeListTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._alternatingItemStyle).TrackViewState();
					}
				}
				return this._alternatingItemStyle;
			}
		}

		// Token: 0x17003EEC RID: 16108
		// (get) Token: 0x0600C35C RID: 50012 RVA: 0x002BC436 File Offset: 0x002BA636
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeListTableItemStyle FooterItemStyle
		{
			get
			{
				if (this._footerItemStyle == null)
				{
					this._footerItemStyle = new TreeListTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._footerItemStyle).TrackViewState();
					}
				}
				return this._footerItemStyle;
			}
		}

		// Token: 0x17003EED RID: 16109
		// (get) Token: 0x0600C35D RID: 50013 RVA: 0x002BC464 File Offset: 0x002BA664
		[Category("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeListTableItemStyle SelectedItemStyle
		{
			get
			{
				if (this._selectedItemStyle == null)
				{
					this._selectedItemStyle = new TreeListTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._selectedItemStyle).TrackViewState();
					}
				}
				return this._selectedItemStyle;
			}
		}

		// Token: 0x17003EEE RID: 16110
		// (get) Token: 0x0600C35E RID: 50014 RVA: 0x002BC492 File Offset: 0x002BA692
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TreeListTableItemStyle EditItemStyle
		{
			get
			{
				if (this._editItemStyle == null)
				{
					this._editItemStyle = new TreeListTableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._editItemStyle).TrackViewState();
					}
				}
				return this._editItemStyle;
			}
		}

		// Token: 0x17003EEF RID: 16111
		// (get) Token: 0x0600C35F RID: 50015 RVA: 0x002BC4C0 File Offset: 0x002BA6C0
		// (set) Token: 0x0600C360 RID: 50016 RVA: 0x002BC4E6 File Offset: 0x002BA6E6
		[Localizable(true)]
		[Description("Gets or sets the caption for the RadTreeList table. Part of RadTreeList accessibility features.")]
		[DefaultValue("")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		public virtual string Caption
		{
			get
			{
				return (this.ViewState["Caption"] as string) ?? this.Localization.Caption;
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		// Token: 0x17003EF0 RID: 16112
		// (get) Token: 0x0600C361 RID: 50017 RVA: 0x002BC4F9 File Offset: 0x002BA6F9
		// (set) Token: 0x0600C362 RID: 50018 RVA: 0x002BC51F File Offset: 0x002BA71F
		[DefaultValue("")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Gets or sets the 'summary' attribute for the RadTreeList. Part of RadTreeList accessibility features.")]
		public virtual string Summary
		{
			get
			{
				return (this.ViewState["Summary"] as string) ?? this.Localization.Summary;
			}
			set
			{
				this.ViewState["Summary"] = value;
			}
		}

		// Token: 0x17003EF1 RID: 16113
		// (get) Token: 0x0600C363 RID: 50019 RVA: 0x002BC532 File Offset: 0x002BA732
		// (set) Token: 0x0600C364 RID: 50020 RVA: 0x002BC554 File Offset: 0x002BA754
		[Description("Gets or sets a value indicating where RadTreeList will look for its .resx localization files.")]
		[Category("Misc")]
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

		// Token: 0x17003EF2 RID: 16114
		// (get) Token: 0x0600C365 RID: 50021 RVA: 0x002BC5A7 File Offset: 0x002BA7A7
		// (set) Token: 0x0600C366 RID: 50022 RVA: 0x002BC5C7 File Offset: 0x002BA7C7
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[Category("Appearance")]
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

		// Token: 0x17003EF3 RID: 16115
		// (get) Token: 0x0600C367 RID: 50023 RVA: 0x002BC5F4 File Offset: 0x002BA7F4
		// (set) Token: 0x0600C368 RID: 50024 RVA: 0x002BC61D File Offset: 0x002BA81D
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue(TreeListTextDirection.LTR)]
		public virtual TreeListTextDirection Dir
		{
			get
			{
				object obj = this.ViewState["Dir"];
				if (obj != null)
				{
					return (TreeListTextDirection)obj;
				}
				return TreeListTextDirection.LTR;
			}
			set
			{
				this.ViewState["Dir"] = value;
			}
		}

		// Token: 0x17003EF4 RID: 16116
		// (get) Token: 0x0600C369 RID: 50025 RVA: 0x002BC638 File Offset: 0x002BA838
		// (set) Token: 0x0600C36A RID: 50026 RVA: 0x002BC661 File Offset: 0x002BA861
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("")]
		[Category("Appearance")]
		public virtual bool ShowOuterBorders
		{
			get
			{
				object obj = this.ViewState["ShowOuterBorders"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowOuterBorders"] = value;
			}
		}

		// Token: 0x17003EF5 RID: 16117
		// (get) Token: 0x0600C36B RID: 50027 RVA: 0x002BC67C File Offset: 0x002BA87C
		// (set) Token: 0x0600C36C RID: 50028 RVA: 0x002BC6A5 File Offset: 0x002BA8A5
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("")]
		public virtual bool ShowTreeLines
		{
			get
			{
				object obj = this.ViewState["ShowTreeLines"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowTreeLines"] = value;
			}
		}

		// Token: 0x17003EF6 RID: 16118
		// (get) Token: 0x0600C36D RID: 50029 RVA: 0x002BC6C0 File Offset: 0x002BA8C0
		// (set) Token: 0x0600C36E RID: 50030 RVA: 0x002BC6E9 File Offset: 0x002BA8E9
		[DefaultValue(TreeListGridLines.Both)]
		[Description("")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public virtual TreeListGridLines GridLines
		{
			get
			{
				object obj = this.ViewState["GridLines"];
				if (obj != null)
				{
					return (TreeListGridLines)obj;
				}
				return TreeListGridLines.Both;
			}
			set
			{
				this.ViewState["GridLines"] = value;
			}
		}

		// Token: 0x17003EF7 RID: 16119
		// (get) Token: 0x0600C36F RID: 50031 RVA: 0x002BC704 File Offset: 0x002BA904
		// (set) Token: 0x0600C370 RID: 50032 RVA: 0x002BC72D File Offset: 0x002BA92D
		[Bindable(true)]
		[Description("TreeList_ShowFooter")]
		[Category("Appearance")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003EF8 RID: 16120
		// (get) Token: 0x0600C371 RID: 50033 RVA: 0x002BC745 File Offset: 0x002BA945
		// (set) Token: 0x0600C372 RID: 50034 RVA: 0x002BC74D File Offset: 0x002BA94D
		[DefaultValue(null)]
		[Bindable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[TemplateContainer(typeof(TreeListPagerItem))]
		public ITemplate PagerTemplate
		{
			get
			{
				return this._pagerTemplate;
			}
			set
			{
				this._pagerTemplate = value;
			}
		}

		// Token: 0x17003EF9 RID: 16121
		// (get) Token: 0x0600C373 RID: 50035 RVA: 0x002BC756 File Offset: 0x002BA956
		// (set) Token: 0x0600C374 RID: 50036 RVA: 0x002BC75E File Offset: 0x002BA95E
		[TemplateContainer(typeof(TreeListNoRecordsItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		[Bindable(false)]
		[Description("Template that will be displayed if there are no records in the DataSource assigned")]
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

		// Token: 0x17003EFA RID: 16122
		// (get) Token: 0x0600C375 RID: 50037 RVA: 0x002BC768 File Offset: 0x002BA968
		// (set) Token: 0x0600C376 RID: 50038 RVA: 0x002BC796 File Offset: 0x002BA996
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool EnableNoRecordsTemplate
		{
			get
			{
				object obj = this.ViewState["EnableNoRecordsTemplate"];
				if (obj == null)
				{
					obj = true;
				}
				return (bool)obj;
			}
			set
			{
				this.ViewState["EnableNoRecordsTemplate"] = value;
			}
		}

		// Token: 0x17003EFB RID: 16123
		// (get) Token: 0x0600C377 RID: 50039 RVA: 0x002BC7B0 File Offset: 0x002BA9B0
		// (set) Token: 0x0600C378 RID: 50040 RVA: 0x002BC7E3 File Offset: 0x002BA9E3
		[Localizable(true)]
		[DefaultValue("No records to display.")]
		[NotifyParentProperty(true)]
		public string NoRecordsText
		{
			get
			{
				object obj = this.ViewState["NoRecordsText"] ?? this.Localization.NoRecordsText;
				return (string)obj;
			}
			set
			{
				this.ViewState["NoRecordsText"] = value;
			}
		}

		// Token: 0x17003EFC RID: 16124
		// (get) Token: 0x0600C379 RID: 50041 RVA: 0x002BC7F6 File Offset: 0x002BA9F6
		// (set) Token: 0x0600C37A RID: 50042 RVA: 0x002BC817 File Offset: 0x002BAA17
		[DefaultValue(false)]
		[Description("When set to true enables support for WAI-ARIA")]
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

		// Token: 0x14000195 RID: 405
		// (add) Token: 0x0600C37B RID: 50043 RVA: 0x002BC82F File Offset: 0x002BAA2F
		// (remove) Token: 0x0600C37C RID: 50044 RVA: 0x002BC842 File Offset: 0x002BAA42
		public event EventHandler<TreeListItemCreatedEventArgs> ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventItemCreated, value);
			}
		}

		// Token: 0x0600C37D RID: 50045 RVA: 0x002BC858 File Offset: 0x002BAA58
		protected virtual void CallItemCreated(TreeListItemCreatedEventArgs e)
		{
			EventHandler<TreeListItemCreatedEventArgs> eventHandler = base.Events[RadTreeList.EventItemCreated] as EventHandler<TreeListItemCreatedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C37E RID: 50046 RVA: 0x002BC886 File Offset: 0x002BAA86
		internal void FireItemCreated(TreeListItemCreatedEventArgs e)
		{
			this.CallItemCreated(e);
		}

		// Token: 0x0600C37F RID: 50047 RVA: 0x002BC88F File Offset: 0x002BAA8F
		internal void FireItemDataBound(TreeListItemDataBoundEventArgs e)
		{
			this.CallItemDataBound(e);
		}

		// Token: 0x14000196 RID: 406
		// (add) Token: 0x0600C380 RID: 50048 RVA: 0x002BC898 File Offset: 0x002BAA98
		// (remove) Token: 0x0600C381 RID: 50049 RVA: 0x002BC8AB File Offset: 0x002BAAAB
		public event EventHandler<TreeListItemDataBoundEventArgs> ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventItemDataBound, value);
			}
		}

		// Token: 0x0600C382 RID: 50050 RVA: 0x002BC8C0 File Offset: 0x002BAAC0
		protected virtual void CallItemDataBound(TreeListItemDataBoundEventArgs e)
		{
			EventHandler<TreeListItemDataBoundEventArgs> eventHandler = base.Events[RadTreeList.EventItemDataBound] as EventHandler<TreeListItemDataBoundEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000197 RID: 407
		// (add) Token: 0x0600C383 RID: 50051 RVA: 0x002BC8EE File Offset: 0x002BAAEE
		// (remove) Token: 0x0600C384 RID: 50052 RVA: 0x002BC901 File Offset: 0x002BAB01
		[Category("Action")]
		[Description("Raised when a button in a RadTreeList control is clicked.")]
		public event EventHandler<TreeListCommandEventArgs> ItemCommand
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventItemCommand, value);
			}
		}

		// Token: 0x0600C385 RID: 50053 RVA: 0x002BC914 File Offset: 0x002BAB14
		protected virtual void OnItemCommand(TreeListCommandEventArgs e)
		{
			if (this.shouldTrackSelection || e.CommandName == "Select" || e.CommandName == "Deselect")
			{
				this.shouldTrackSelection = false;
				this.TrackItemSelection(e.Item, e.CommandName);
			}
			EventHandler<TreeListCommandEventArgs> eventHandler = base.Events[RadTreeList.EventItemCommand] as EventHandler<TreeListCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000198 RID: 408
		// (add) Token: 0x0600C386 RID: 50054 RVA: 0x002BC987 File Offset: 0x002BAB87
		// (remove) Token: 0x0600C387 RID: 50055 RVA: 0x002BC99A File Offset: 0x002BAB9A
		[Category("Action")]
		[Description("Fires when \"Page\" command bubbles")]
		public event EventHandler<TreeListPageChangedEventArgs> PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventPageIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventPageIndexChanged, value);
			}
		}

		// Token: 0x0600C388 RID: 50056 RVA: 0x002BC9B0 File Offset: 0x002BABB0
		protected virtual void OnPageIndexChanged(TreeListPageChangedEventArgs e)
		{
			EventHandler<TreeListPageChangedEventArgs> eventHandler = base.Events[RadTreeList.EventPageIndexChanged] as EventHandler<TreeListPageChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C389 RID: 50057 RVA: 0x002BC9DE File Offset: 0x002BABDE
		internal void FirePageIndexChanged(TreeListPageChangedEventArgs e)
		{
			this.TrackPaging(e.NewPageIndex);
			this.OnPageIndexChanged(e);
		}

		// Token: 0x14000199 RID: 409
		// (add) Token: 0x0600C38A RID: 50058 RVA: 0x002BC9F3 File Offset: 0x002BABF3
		// (remove) Token: 0x0600C38B RID: 50059 RVA: 0x002BCA06 File Offset: 0x002BAC06
		[Description("Fires when PageSize has been changed.")]
		[Category("Action")]
		public event EventHandler<TreeListPageSizeChangedEventArgs> PageSizeChanged
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventPageSizeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventPageSizeChanged, value);
			}
		}

		// Token: 0x0600C38C RID: 50060 RVA: 0x002BCA1C File Offset: 0x002BAC1C
		protected virtual void OnPageSizeChanged(TreeListPageSizeChangedEventArgs e)
		{
			EventHandler<TreeListPageSizeChangedEventArgs> eventHandler = base.Events[RadTreeList.EventPageSizeChanged] as EventHandler<TreeListPageSizeChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C38D RID: 50061 RVA: 0x002BCA4A File Offset: 0x002BAC4A
		internal void FirePageSizeChanged(TreeListPageSizeChangedEventArgs e)
		{
			this.OnPageSizeChanged(e);
		}

		// Token: 0x1400019A RID: 410
		// (add) Token: 0x0600C38E RID: 50062 RVA: 0x002BCA53 File Offset: 0x002BAC53
		// (remove) Token: 0x0600C38F RID: 50063 RVA: 0x002BCA66 File Offset: 0x002BAC66
		[Description("Raised when a auto generated column is created.")]
		[Category("Action")]
		public event EventHandler<TreeListAutoGeneratedColumnCreatedEventArgs> AutoGeneratedColumnCreated
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventAutoGeneratedColumnCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventAutoGeneratedColumnCreated, value);
			}
		}

		// Token: 0x0600C390 RID: 50064 RVA: 0x002BCA7C File Offset: 0x002BAC7C
		protected virtual void OnAutoGeneratedColumnCreated(TreeListAutoGeneratedColumnCreatedEventArgs e)
		{
			EventHandler<TreeListAutoGeneratedColumnCreatedEventArgs> eventHandler = base.Events[RadTreeList.EventAutoGeneratedColumnCreated] as EventHandler<TreeListAutoGeneratedColumnCreatedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C391 RID: 50065 RVA: 0x002BCAAA File Offset: 0x002BACAA
		internal void CallOnAutoGeneratedColumnCreated(TreeListColumn column)
		{
			this.OnAutoGeneratedColumnCreated(new TreeListAutoGeneratedColumnCreatedEventArgs(column));
		}

		// Token: 0x1400019B RID: 411
		// (add) Token: 0x0600C392 RID: 50066 RVA: 0x002BCAB8 File Offset: 0x002BACB8
		// (remove) Token: 0x0600C393 RID: 50067 RVA: 0x002BCACB File Offset: 0x002BACCB
		[Description("Raised when a custom column is recreated on postback.")]
		[Category("Action")]
		public event EventHandler<TreeListCreateCustomColumnEventArgs> CreateCustomColumn
		{
			add
			{
				base.Events.AddHandler(RadTreeList.EventCreateCustomColumn, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeList.EventCreateCustomColumn, value);
			}
		}

		// Token: 0x0600C394 RID: 50068 RVA: 0x002BCAE0 File Offset: 0x002BACE0
		protected virtual void OnCreateCustomColumn(TreeListCreateCustomColumnEventArgs e)
		{
			EventHandler<TreeListCreateCustomColumnEventArgs> eventHandler = base.Events[RadTreeList.EventCreateCustomColumn] as EventHandler<TreeListCreateCustomColumnEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600C395 RID: 50069 RVA: 0x002BCB0E File Offset: 0x002BAD0E
		internal void CallOnCreateCustomColumn(TreeListCreateCustomColumnEventArgs args)
		{
			this.OnCreateCustomColumn(args);
		}

		// Token: 0x0600C396 RID: 50070 RVA: 0x002BCB18 File Offset: 0x002BAD18
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>();
			foreach (ScriptReference scriptReference in scriptReferences)
			{
				if (scriptReference.Name != "Telerik.Web.UI.TreeList.RadTreeList.Desktop.js")
				{
					list.Add(scriptReference);
				}
			}
			if (this.EnableEmbeddedScripts)
			{
				this.AddFeatureSpecificScriptReferences(list);
			}
			return list;
		}

		// Token: 0x0600C397 RID: 50071 RVA: 0x002BCBDC File Offset: 0x002BADDC
		private void AddFeatureSpecificScriptReferences(List<ScriptReference> baseReferences)
		{
			string resourceNameSuffix = "Script";
			string assemblyName = Assembly.GetExecutingAssembly().FullName;
			TFunc<string, ScriptReference> tfunc = (string resourceName) => new ScriptReference(string.Format("{0}{1}.js", resourceName, resourceNameSuffix), assemblyName);
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				baseReferences.Add(new ScriptReference("Telerik.Web.UI.TreeList.RadTreeListMobileScripts.js", Assembly.GetExecutingAssembly().FullName));
				baseReferences.Remove(baseReferences.Find((ScriptReference r) => r.Name == "Telerik.Web.UI.TreeList.RadTreeListScripts.js"));
			}
			else
			{
				baseReferences.Add(new ScriptReference("Telerik.Web.UI.TreeList.RadTreeListScripts.js", Assembly.GetExecutingAssembly().FullName));
			}
			if (this.ClientSettings.AllowItemsDragDrop)
			{
				baseReferences.Add(tfunc("Telerik.Web.UI.TreeList.TreeListItemDrag"));
			}
			if (this.ClientSettings.AllowKeyboardNavigation)
			{
				baseReferences.Add(tfunc("Telerik.Web.UI.TreeList.TreeListKeyboardNavigation"));
			}
			if (this.ClientSettings.Resizing.AllowColumnResize || this.ClientSettings.Reordering.AllowColumnsReorder || (this.ClientSettings.Scrolling.AllowScroll && this.IsMobile) || this.ResolvedRenderMode == RenderMode.Mobile || (this.ResolvedRenderMode == RenderMode.Lightweight && base.RuntimeSkin == "Material"))
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
			}
			if ((this.ClientSettings.Scrolling.AllowScroll && this.IsMobile) || this.ResolvedRenderMode == RenderMode.Mobile)
			{
				baseReferences.Add(new ScriptReference("Telerik.Web.UI.Common.jQueryPlugins.js", assemblyName));
				baseReferences.Add(new ScriptReference("Telerik.Web.UI.Common.TouchScrollExtender.js", assemblyName));
			}
			if (this.ClientSettings.Resizing.AllowColumnResize)
			{
				if (this.ResolvedRenderMode == RenderMode.Mobile)
				{
					baseReferences.Add(new ScriptReference("Telerik.Web.UI.TreeList.TreeListColumnResizerMobileScript.js", Assembly.GetExecutingAssembly().FullName));
					baseReferences.Remove(baseReferences.Find((ScriptReference r) => r.Name == "Telerik.Web.UI.TreeList.TreeListColumnResizerScript.js"));
				}
				else
				{
					baseReferences.Add(new ScriptReference("Telerik.Web.UI.TreeList.TreeListColumnResizerScript.js", Assembly.GetExecutingAssembly().FullName));
				}
			}
			if (this.ClientSettings.Reordering.AllowColumnsReorder)
			{
				baseReferences.Add(tfunc("Telerik.Web.UI.TreeList.TreeListColumnReordering"));
			}
		}

		// Token: 0x0600C398 RID: 50072 RVA: 0x002BCE44 File Offset: 0x002BB044
		private void AddScriptReference(List<ScriptReference> scriptReferences, string path)
		{
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				scriptReferences.Add(new ScriptReference(path.Replace(".js", ".Mobile.js"), Assembly.GetExecutingAssembly().FullName));
				return;
			}
			scriptReferences.Add(new ScriptReference(path.Replace(".js", ".Desktop.js"), Assembly.GetExecutingAssembly().FullName));
		}

		// Token: 0x0600C399 RID: 50073 RVA: 0x002BCEBC File Offset: 0x002BB0BC
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.RegisterClientSideEvents(delegate(string eventName, string eventValue)
			{
				RadCompositeDataBoundControl.DescribeEvent(descriptor, eventName, eventValue);
			});
			this.DescribeProperties(descriptor);
		}

		// Token: 0x0600C39A RID: 50074 RVA: 0x002BCF14 File Offset: 0x002BB114
		private void RegisterClientSideEvents(TAction<string, string> eventData)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.ClientSettings.ClientEvents);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!(propertyDescriptor.DisplayName == "ViewState"))
				{
					string text = propertyDescriptor.DisplayName.Replace("On", string.Empty);
					text = Regex.Replace(text, "^[A-Z]", (Match match) => match.ToString().ToLower(CultureInfo.InvariantCulture));
					string text2 = propertyDescriptor.GetValue(this.ClientSettings.ClientEvents).ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						eventData(text, text2);
					}
				}
			}
		}

		// Token: 0x0600C39B RID: 50075 RVA: 0x002BD01C File Offset: 0x002BB21C
		private void DescribeProperties(IScriptDescriptor descriptor)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new TreeListJavaScriptConverter()
			});
			descriptor.AddProperty("UniqueID", this.UniqueID);
			descriptor.AddProperty("Skin", base.RuntimeSkin);
			descriptor.AddProperty("_embeddedSkin", this.EnableEmbeddedSkins);
			if (this.ClientSettings.AllowKeyboardNavigation)
			{
				descriptor.AddProperty("_activeRowIndex", this.ClientSettings.ActiveRowIndex);
				descriptor.AddProperty("_shouldFocusOnPage", this.shouldFocusOnPage);
				descriptor.AddProperty("_controlToFocus", this._controlToFocus);
				bool flag = this.EditMode == TreeListEditMode.InPlace && this.focusItemType == TreeListFocusItemType.EditItem;
				descriptor.AddProperty("_isInPlaceEditMode", flag);
				descriptor.AddProperty("_validationSettings", new Dictionary<string, object>
				{
					{
						"_enableValidation",
						this.ValidationSettings.EnableValidation
					},
					{
						"_validationGroup",
						this.ValidationSettings.ValidationGroup
					},
					{
						"_commandsToValidate",
						this.ValidationSettings.CommandsToValidate
					}
				});
			}
			if (this.IsItemInserted)
			{
				descriptor.AddProperty("_isItemInserted", this.IsItemInserted);
			}
			if (this.AllowMultiItemEdit)
			{
				descriptor.AddProperty("_allowMultiItemEdit", this.AllowMultiItemEdit);
			}
			if (this.EditIndexes.Count > 0)
			{
				descriptor.AddProperty("_editIndexes", javaScriptSerializer.Serialize(this.EditIndexes));
			}
			if (this.InsertIndexes.Count > 0)
			{
				descriptor.AddProperty("_insertIndexes", javaScriptSerializer.Serialize(this.InsertIndexes));
			}
			if ((this.ExpandCollapseMode == TreeListExpandCollapseMode.Client || this.ExpandCollapseMode == TreeListExpandCollapseMode.Combined) && this.ClientExpandedIndexes.Count > 0)
			{
				descriptor.AddProperty("_expandedIndexes", javaScriptSerializer.Serialize(this.ClientExpandedIndexes));
			}
			descriptor.AddScriptProperty("_clientSettings", javaScriptSerializer.Serialize(this.ClientSettings));
			if (this.SelectedItems.Count > 0)
			{
				ArrayList selectedIndexes = new ArrayList();
				this.SelectedItems.ForEach(delegate(TreeListDataItem item)
				{
					selectedIndexes.Add(item.DisplayIndex);
				});
				descriptor.AddProperty("selectedIndexes", selectedIndexes);
			}
			descriptor.AddProperty("_data", this.DescribeClientData());
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
		}

		// Token: 0x0600C39C RID: 50076 RVA: 0x002BD40C File Offset: 0x002BB60C
		private Dictionary<string, object> DescribeClientData()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (this.AllowMultiItemSelection)
			{
				dictionary.Add("_allowMultiItemSelection", this.AllowMultiItemSelection);
			}
			if (this.AllowRecursiveSelection)
			{
				dictionary.Add("_allowRecursiveSelection", this.AllowRecursiveSelection);
			}
			if (this.ExpandCollapseMode != TreeListExpandCollapseMode.Server)
			{
				dictionary.Add("_expandCollapseMode", this.ExpandCollapseMode);
			}
			this.SelectedItemStyle.CssClass = HttpUtility.HtmlEncode(this.FormatCssClass("rtlRSel", this.SelectedItemStyle.CssClass));
			dictionary.Add("_selectedItemStyle", this.SerializeStyle(this.SelectedItemStyle));
			dictionary.Add("_selectedItemStyleClass", this.SelectedItemStyle.CssClass);
			this.ExpandHash = new HashSet<TreeListHierarchyIndex>(this.ExpandedIndexes);
			ArrayList arrayList = new ArrayList();
			foreach (TreeListDataItem item in this.Items)
			{
				arrayList.Add(this.DescribeItemData(item));
			}
			if (arrayList.Count > 0)
			{
				dictionary.Add("_itemData", arrayList.ToArray());
			}
			dictionary.Add("_currentPageIndex", this.CurrentPageIndex);
			dictionary.Add("_pageCount", this.PageCount);
			dictionary.Add("_pagerMode", this.PagerStyle.Mode.ToString());
			dictionary.Add("_clientDataKeyNames", this.ClientDataKeyNames);
			if (this.EditMode == TreeListEditMode.PopUp)
			{
				dictionary.Add("_popUpSettings", new
				{
					_popUpIds = this._popUpIds.ToArray(),
					_modal = this.EditFormSettings.PopUpSettings.Modal,
					_zIndex = this.EditFormSettings.PopUpSettings.ZIndex
				});
			}
			ArrayList arrayList2 = new ArrayList();
			foreach (TreeListColumn treeListColumn in this.RenderColumns)
			{
				if (treeListColumn.Visible)
				{
					if (treeListColumn is TreeListDragDropColumn)
					{
						dictionary.Add("_useDragDropColumn", true);
					}
					arrayList2.Add(this.DescribeColumnData(treeListColumn));
				}
			}
			dictionary.Add("_columnsData", arrayList2);
			return dictionary;
		}

		// Token: 0x0600C39D RID: 50077 RVA: 0x002BD654 File Offset: 0x002BB854
		private Dictionary<string, object> DescribeColumnData(TreeListColumn column)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("UniqueName", column.UniqueName);
			dictionary.Add("ColumnType", column.ColumnType);
			dictionary.Add("Reorderable", column.Reorderable);
			dictionary.Add("Resizable", column.Resizable);
			dictionary.Add("OrderIndex", column.OrderIndex);
			if (column.MinWidth != Unit.Empty)
			{
				dictionary.Add("_minWidth", column.MinWidth.ToString());
			}
			if (column.MaxWidth != Unit.Empty)
			{
				dictionary.Add("_maxWidth", column.MaxWidth.ToString());
			}
			dictionary.Add("_display", column.Display);
			return dictionary;
		}

		// Token: 0x0600C39E RID: 50078 RVA: 0x002BD744 File Offset: 0x002BB944
		private string SerializeStyle(Style value)
		{
			string result = "";
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
					result = text.Substring(num, num2 - num);
				}
			}
			return result;
		}

		// Token: 0x0600C39F RID: 50079 RVA: 0x002BD7C4 File Offset: 0x002BB9C4
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private Dictionary<string, object> DescribeItemData(TreeListDataItem item)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("_hIndex", item.HierarchyIndex);
			if (this.ExpandHash.Contains(item.HierarchyIndex))
			{
				dictionary.Add("_expanded", true);
			}
			if (this.ExpandCollapseMode == TreeListExpandCollapseMode.Combined && item.TreeListInitializedExpandCollapse)
			{
				dictionary.Add("_treeListInitializedExpandCollapse", true);
			}
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (string text in this.ClientDataKeyNames)
			{
				object dataKeyValue = item.GetDataKeyValue(text);
				dictionary2.Add(text, (dataKeyValue == null) ? null : dataKeyValue.ToString());
			}
			dictionary.Add("_clientDataKeyValues", dictionary2);
			return dictionary;
		}

		// Token: 0x0600C3A0 RID: 50080 RVA: 0x002BD880 File Offset: 0x002BBA80
		protected internal int GetIntegerPixelFromClientState(string clientStateData)
		{
			clientStateData = clientStateData.Replace("px", "");
			int result;
			try
			{
				NumberFormatInfo provider = new NumberFormatInfo
				{
					NumberDecimalSeparator = ".",
					NumberGroupSeparator = ","
				};
				result = (int)Math.Round(double.Parse(clientStateData, provider));
			}
			catch (Exception)
			{
				if (clientStateData.IndexOf('.') > -1)
				{
					try
					{
						return int.Parse(clientStateData.Substring(0, clientStateData.IndexOf('.')));
					}
					catch (Exception)
					{
						return 0;
					}
				}
				result = 0;
			}
			return result;
		}

		// Token: 0x0600C3A1 RID: 50081 RVA: 0x002BD918 File Offset: 0x002BBB18
		protected override bool LoadClientState(Dictionary<string, object> clientState)
		{
			if (this.AlwaysAutoBindOnPostBack && this.Page.IsPostBack)
			{
				this._shouldCallDataBindOnLoad = false;
				base.RequiresDataBinding = true;
				this.AutoDataBind(TreeListRebindReason.PostbackViewStateNotPersisted);
			}
			if (clientState.ContainsKey("shouldFocusOnPage"))
			{
				bool.TryParse(clientState["shouldFocusOnPage"].ToString(), out this.shouldFocusOnPage);
			}
			if (clientState.ContainsKey("popUpLocations"))
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)clientState["popUpLocations"];
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					if (!string.IsNullOrEmpty(keyValuePair.Key))
					{
						Pair pair = new Pair();
						string[] array = keyValuePair.Value.ToString().Split(new char[]
						{
							','
						});
						pair.First = Unit.Pixel(this.GetIntegerPixelFromClientState(array[0]));
						pair.Second = Unit.Pixel(this.GetIntegerPixelFromClientState(array[1]));
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
			if (clientState.ContainsKey("scrolledPosition"))
			{
				string text = (string)clientState["scrolledPosition"];
				string[] array2 = text.Split(new char[]
				{
					','
				});
				this.ClientSettings.Scrolling.ScrollTop = array2[0];
				this.ClientSettings.Scrolling.ScrollLeft = array2[1];
			}
			if (clientState.ContainsKey("draggedIndexes"))
			{
				this._draggedIndexes = (object[])clientState["draggedIndexes"];
			}
			if (clientState.ContainsKey("activeRowIndex"))
			{
				this.ClientSettings.ActiveRowIndex = (string)clientState["activeRowIndex"];
			}
			if (clientState.ContainsKey("resizedColumns"))
			{
				string text2 = (string)clientState["resizedColumns"];
				text2 = text2.Remove(text2.Length - 1, 1);
				string[] array3 = text2.Split(new char[]
				{
					';'
				});
				int num = int.MaxValue;
				Dictionary<TreeListColumn, Unit> dictionary2 = new Dictionary<TreeListColumn, Unit>(array3.Length);
				foreach (string text3 in array3)
				{
					string[] array5 = text3.Split(new char[]
					{
						','
					});
					string columnUniqueName = array5[0];
					Unit value = Unit.Parse(array5[1]);
					TreeListColumn columnSafe = this.GetColumnSafe(columnUniqueName);
					if (columnSafe != null)
					{
						if (columnSafe.OrderIndex < num)
						{
							num = columnSafe.OrderIndex;
						}
						dictionary2.Add(columnSafe, value);
					}
				}
				if (this.ClientSettings.Resizing.ResizeMode == TreeListResizeMode.NoScroll || (this.ClientSettings.Resizing.ResizeMode == TreeListResizeMode.AllowScroll && !this.ClientSettings.Scrolling.AllowScroll))
				{
					foreach (TreeListColumn treeListColumn in this.Columns)
					{
						if (treeListColumn.OrderIndex > num)
						{
							treeListColumn.HeaderStyle.Width = Unit.Empty;
						}
					}
				}
				foreach (KeyValuePair<TreeListColumn, Unit> keyValuePair2 in dictionary2)
				{
					keyValuePair2.Key.HeaderStyle.Width = keyValuePair2.Value;
				}
			}
			if (clientState.ContainsKey("resizedControlWidth"))
			{
				string s = (string)clientState["resizedControlWidth"];
				Unit width = Unit.Parse(s);
				if (this.ClientSettings.Resizing.ResizeMode == TreeListResizeMode.ResizeTreeList)
				{
					this.Width = width;
				}
				else if (this.ClientSettings.Resizing.ResizeMode == TreeListResizeMode.AllowScroll)
				{
					this.GetTreeListTable().Width = width;
					if (this.ClientSettings.Scrolling.UseStaticHeaders)
					{
						foreach (object obj in this.Controls)
						{
							TreeListTable treeListTable = obj as TreeListTable;
							if (treeListTable != null)
							{
								treeListTable.Width = width;
							}
						}
					}
				}
			}
			if (clientState.ContainsKey("displayColumns"))
			{
				Dictionary<string, object> dictionary3 = (Dictionary<string, object>)clientState["displayColumns"];
				foreach (KeyValuePair<string, object> keyValuePair3 in dictionary3)
				{
					TreeListColumn columnSafe2 = this.GetColumnSafe(keyValuePair3.Key);
					if (columnSafe2 != null)
					{
						columnSafe2.Display = (bool)keyValuePair3.Value;
					}
				}
			}
			if (clientState.ContainsKey("clientExpandedIndexes"))
			{
				object[] array6 = clientState["clientExpandedIndexes"] as object[];
				if (array6 != null && array6.Length > 0)
				{
					if (this.ExpandCollapseMode == TreeListExpandCollapseMode.Combined)
					{
						this.ClientExpandedIndexes.Clear();
						foreach (TreeListDataItem treeListDataItem in this.Items)
						{
							if (!treeListDataItem.IsCombineExpanded)
							{
								treeListDataItem.Expanded = false;
							}
						}
					}
					ArrayList arrayList = new ArrayList(array6);
					using (IEnumerator enumerator7 = arrayList.GetEnumerator())
					{
						while (enumerator7.MoveNext())
						{
							object obj2 = enumerator7.Current;
							int index = (int)obj2;
							TreeListDataItem treeListDataItem2 = this.Items[index];
							treeListDataItem2.Expanded = true;
							this.ClientExpandedIndexes.Add(treeListDataItem2.HierarchyIndex);
						}
						goto IL_61A;
					}
				}
				if (this.ExpandCollapseMode == TreeListExpandCollapseMode.Combined)
				{
					this.ClientExpandedIndexes.Clear();
					foreach (TreeListDataItem treeListDataItem3 in this.Items)
					{
						if (!treeListDataItem3.IsCombineExpanded)
						{
							treeListDataItem3.Expanded = false;
						}
					}
				}
			}
			IL_61A:
			if (clientState.ContainsKey("reorderedColumns"))
			{
				string text4 = clientState["reorderedColumns"].ToString();
				string[] array7 = text4.Split(new string[]
				{
					"||"
				}, StringSplitOptions.RemoveEmptyEntries);
				List<TreeListReorderedColumn> list = new List<TreeListReorderedColumn>();
				foreach (string text5 in array7)
				{
					string[] array9 = text5.Split(new string[]
					{
						";|"
					}, StringSplitOptions.RemoveEmptyEntries);
					string columnUniqueName2 = array9[0];
					int num2 = int.Parse(array9[1]);
					TreeListColumn columnSafe3 = this.GetColumnSafe(columnUniqueName2);
					if (columnSafe3 != null && columnSafe3.OrderIndex != num2)
					{
						int orderIndex = columnSafe3.OrderIndex;
						columnSafe3.OrderIndex = num2;
						list.Add(new TreeListReorderedColumn(columnSafe3, orderIndex));
					}
				}
				if (list.Count > 0)
				{
					this.reorderedColumns = list.ToArray();
					base.RequiresDataBinding = true;
					this.OnColumnsOrderChanged();
				}
			}
			return this.LoadSelectedState(clientState);
		}

		// Token: 0x0600C3A2 RID: 50082 RVA: 0x002BE09C File Offset: 0x002BC29C
		protected virtual bool LoadSelectedState(Dictionary<string, object> clientState)
		{
			bool flag = false;
			object[] array = clientState["selectedIndexes"] as object[];
			if (array != null)
			{
				ArrayList arrayList = new ArrayList(array);
				foreach (TreeListDataItem treeListDataItem in this.SelectedItems)
				{
					if (arrayList.Contains(treeListDataItem.DisplayIndex))
					{
						arrayList.Remove(treeListDataItem.DisplayIndex);
					}
					else
					{
						flag = true;
						treeListDataItem.Selected = false;
					}
				}
				if (arrayList.Count > 0)
				{
					flag = true;
					foreach (object obj in arrayList)
					{
						int index = (int)obj;
						this.Items[index].Selected = true;
					}
				}
			}
			if (flag)
			{
				this._shouldCallOnSelectedIndexChanged = true;
			}
			return flag;
		}

		// Token: 0x04003316 RID: 13078
		internal const string DataSourceItemCountControlStateKey = "_!DSIC";

		// Token: 0x04003317 RID: 13079
		internal const string ItemCountControlStateKey = "_!ItemCount";

		// Token: 0x04003318 RID: 13080
		internal const string TotalItemCountControlStateKey = "_!TotalItemCount";

		// Token: 0x04003319 RID: 13081
		internal const string ItemStateControlStateKey = "_!ItemState";

		// Token: 0x0400331A RID: 13082
		internal const string SelectedIndexesControlStateKey = "_!SelectedItems";

		// Token: 0x0400331B RID: 13083
		internal const string ExpandedControlStateKey = "_!ExpandedItem";

		// Token: 0x0400331C RID: 13084
		internal const string ClientExpandedControlStateKey = "_!ClientExpandedItem";

		// Token: 0x0400331D RID: 13085
		internal const string EditIndexesControlStateKey = "_!EditItems";

		// Token: 0x0400331E RID: 13086
		internal const string InsertIndexesControlStateKey = "_!InsertItems";

		// Token: 0x0400331F RID: 13087
		internal const string PageCountViewStateKey = "_!PCount";

		// Token: 0x04003320 RID: 13088
		internal const string AllowMultiItemSelectionStateKey = "_!AllowMultiItemSelection";

		// Token: 0x04003321 RID: 13089
		internal const string AllowRecursiveSelectionStateKey = "_!AllowRecursiveSelection";

		// Token: 0x04003322 RID: 13090
		internal const string AllowMultiItemEditStateKey = "_!AllowMultiItemEdit";

		// Token: 0x04003323 RID: 13091
		internal const string FooterItemStateControlStateKey = "_!FooterItemState";

		// Token: 0x04003324 RID: 13092
		internal const string GridLinesHorizontalClassName = "rtlHBorders";

		// Token: 0x04003325 RID: 13093
		internal const string GridLinesVerticalClassName = "rtlVBorders";

		// Token: 0x04003326 RID: 13094
		internal const string GridLinesBothClassName = "rtlHVBorders";

		// Token: 0x04003327 RID: 13095
		internal const string TreeLinesClassName = "rtlLines";

		// Token: 0x04003328 RID: 13096
		internal const string TreeListTableClassName = "rtlTable";

		// Token: 0x04003329 RID: 13097
		internal const string CollapseButtonClassName = "rtlCollapse";

		// Token: 0x0400332A RID: 13098
		internal const string ExpandButtonClassName = "rtlExpand";

		// Token: 0x0400332B RID: 13099
		internal const string ServiceCellBaseClassName = "rtlL";

		// Token: 0x0400332C RID: 13100
		internal const string ServiceCellL0ClassName = "rtlL0";

		// Token: 0x0400332D RID: 13101
		internal const string ServiceCellL1ClassName = "rtlL1";

		// Token: 0x0400332E RID: 13102
		internal const string ServiceCellL2ClassName = "rtlL2";

		// Token: 0x0400332F RID: 13103
		internal const string ServiceCellL3ClassName = "rtlL3";

		// Token: 0x04003330 RID: 13104
		internal const string LastItemInLevelClassName = "rtlRL";

		// Token: 0x04003331 RID: 13105
		internal const string FirstItemInHigherLevelClassName = "rtlROut";

		// Token: 0x04003332 RID: 13106
		internal const string LastItemClassName = "rtlRBtm";

		// Token: 0x04003333 RID: 13107
		internal const string FirstDataCellClassName = "rtlCF";

		// Token: 0x04003334 RID: 13108
		internal const string LastDataCellClassName = "rtlCL";

		// Token: 0x04003335 RID: 13109
		internal const string HeaderItemClassName = "rtlHeader";

		// Token: 0x04003336 RID: 13110
		internal const string CommandItemClassName = "rtlCommand";

		// Token: 0x04003337 RID: 13111
		internal const string PagerItemPart1ClassName = "rtlArrPart1";

		// Token: 0x04003338 RID: 13112
		internal const string PagerItemPart2ClassName = "rtlArrPart2";

		// Token: 0x04003339 RID: 13113
		internal const string PagerItemAdvPartClassName = "rtlAdvPart";

		// Token: 0x0400333A RID: 13114
		internal const string RadTreeListClassName = "RadTreeList";

		// Token: 0x0400333B RID: 13115
		internal const string RadTreeListRTLClassName = "RadTreeListRTL";

		// Token: 0x0400333C RID: 13116
		internal const string RadTreeListNoBorderRTLClassName = "RadTreeListNoBorderRTL";

		// Token: 0x0400333D RID: 13117
		internal const string RadTreeListNoBorderClassName = "RadTreeListNoBorder";

		// Token: 0x0400333E RID: 13118
		internal const string PagerItemClassName = "rtlPager";

		// Token: 0x0400333F RID: 13119
		internal const string PagerItemTopClassName = "rtlPagerTop";

		// Token: 0x04003340 RID: 13120
		internal const string PagerContentCellClassName = "rtlPagerCell";

		// Token: 0x04003341 RID: 13121
		internal const string CommandItemContentCellClassName = "rtlCommandCell";

		// Token: 0x04003342 RID: 13122
		internal const string DataItemClassName = "rtlR";

		// Token: 0x04003343 RID: 13123
		internal const string FooterItemClassName = "rtlRFooter";

		// Token: 0x04003344 RID: 13124
		internal const string AlternatingDataItemClassName = "rtlA";

		// Token: 0x04003345 RID: 13125
		internal const string SelectedItemClassName = "rtlRSel";

		// Token: 0x04003346 RID: 13126
		internal const string EditItemClassName = "rtlREdit";

		// Token: 0x04003347 RID: 13127
		internal const string EditFormCellClassName = "rtlCEdit";

		// Token: 0x04003348 RID: 13128
		internal const string EditFormClassName = "rtlEditForm";

		// Token: 0x04003349 RID: 13129
		internal const string EditButtonClassName = "rtlEdit";

		// Token: 0x0400334A RID: 13130
		internal const string InsertButtonClassName = "rtlAdd";

		// Token: 0x0400334B RID: 13131
		internal const string UpdateButtonClassName = "rtlUpdate";

		// Token: 0x0400334C RID: 13132
		internal const string DeleteButtonClassName = "rtlDel";

		// Token: 0x0400334D RID: 13133
		internal const string CancelButtonClassName = "rtlCancel";

		// Token: 0x0400334E RID: 13134
		internal const string CaptionClassName = "rtlCaption";

		// Token: 0x0400334F RID: 13135
		internal const string ActionButtonClassName = "t-button rtlActionButton";

		// Token: 0x04003350 RID: 13136
		internal const string ButtonIconClassName = "t-font-icon rtlIcon";

		// Token: 0x04003351 RID: 13137
		public const string RebindTreeListCommandName = "RebindTreeList";

		// Token: 0x04003352 RID: 13138
		public const string PageCommandName = "Page";

		// Token: 0x04003353 RID: 13139
		public const string FirstPageCommandArgument = "First";

		// Token: 0x04003354 RID: 13140
		public const string LastPageCommandArgument = "Last";

		// Token: 0x04003355 RID: 13141
		public const string NextPageCommandArgument = "Next";

		// Token: 0x04003356 RID: 13142
		public const string PrevPageCommandArgument = "Prev";

		// Token: 0x04003357 RID: 13143
		public const string ChangePageSizeCommandName = "ChangePageSize";

		// Token: 0x04003358 RID: 13144
		public const string ExpandCollapseCommandName = "ExpandCollapse";

		// Token: 0x04003359 RID: 13145
		public const string SelectCommandName = "Select";

		// Token: 0x0400335A RID: 13146
		public const string DeselectCommandName = "Deselect";

		// Token: 0x0400335B RID: 13147
		public const string SelectAllCommandName = "SelectAll";

		// Token: 0x0400335C RID: 13148
		public const string DeselectAllCommandName = "DeselectAll";

		// Token: 0x0400335D RID: 13149
		public const string SortCommandName = "Sort";

		// Token: 0x0400335E RID: 13150
		public const string EditCommandName = "Edit";

		// Token: 0x0400335F RID: 13151
		public const string InitInsertCommandName = "InitInsert";

		// Token: 0x04003360 RID: 13152
		public const string PerformInsertCommandName = "PerformInsert";

		// Token: 0x04003361 RID: 13153
		public const string UpdateCommandName = "Update";

		// Token: 0x04003362 RID: 13154
		public const string DeleteCommandName = "Delete";

		// Token: 0x04003363 RID: 13155
		public const string CancelCommandName = "Cancel";

		// Token: 0x04003364 RID: 13156
		public const string SwapCommandName = "Swap";

		// Token: 0x04003365 RID: 13157
		public const string ReorderCommandName = "Reorder";

		// Token: 0x04003366 RID: 13158
		public const string ExportToExcelCommandName = "ExportToExcel";

		// Token: 0x04003367 RID: 13159
		public const string ExportToWordCommandName = "ExportToWord";

		// Token: 0x04003368 RID: 13160
		public const string ExportToPdfCommandName = "ExportToPdf";

		// Token: 0x04003369 RID: 13161
		internal const string ClientPostbackFunctionFormat = "FireCommand:{0}|;{1}|;{2}|;";

		// Token: 0x0400336A RID: 13162
		private static readonly object EventNeedDataSource;

		// Token: 0x0400336B RID: 13163
		private static readonly object EventChildItemsDataBind;

		// Token: 0x0400336C RID: 13164
		private static readonly object EventItemCreated;

		// Token: 0x0400336D RID: 13165
		private static readonly object EventItemDataBound;

		// Token: 0x0400336E RID: 13166
		private static readonly object EventItemCommand;

		// Token: 0x0400336F RID: 13167
		private static readonly object EventPageIndexChanged;

		// Token: 0x04003370 RID: 13168
		private static readonly object EventPageSizeChanged;

		// Token: 0x04003371 RID: 13169
		private static readonly object EventCreateCustomColumn;

		// Token: 0x04003372 RID: 13170
		private static readonly object EventAutoGeneratedColumnCreated;

		// Token: 0x04003373 RID: 13171
		private static readonly object EventSorting;

		// Token: 0x04003374 RID: 13172
		private static readonly object EventEditCommand;

		// Token: 0x04003375 RID: 13173
		private static readonly object EventInsertCommand;

		// Token: 0x04003376 RID: 13174
		private static readonly object EventUpdateCommand;

		// Token: 0x04003377 RID: 13175
		private static readonly object EventDeleteCommand;

		// Token: 0x04003378 RID: 13176
		private static readonly object EventCancelCommand;

		// Token: 0x04003379 RID: 13177
		private static readonly object EventCreateColumnEditor;

		// Token: 0x0400337A RID: 13178
		private static readonly object EventItemUpdated;

		// Token: 0x0400337B RID: 13179
		private static readonly object EventItemInserted;

		// Token: 0x0400337C RID: 13180
		private static readonly object EventItemDeleted;

		// Token: 0x0400337D RID: 13181
		private static readonly object EventItemDrop;

		// Token: 0x0400337E RID: 13182
		private static readonly object EventColumnsOrderChanged;

		// Token: 0x0400337F RID: 13183
		private static readonly object EventSelectedIndexChanged;

		// Token: 0x04003380 RID: 13184
		private static readonly object EventExporting;

		// Token: 0x04003381 RID: 13185
		private static readonly object EventPdfExporting;

		// Token: 0x04003382 RID: 13186
		private static readonly object EventInfrastructureExporting;

		// Token: 0x04003383 RID: 13187
		private TreeListExportSettings _exportSettings;

		// Token: 0x04003384 RID: 13188
		private TreeListEnumerableBase _resolvedDataSource;

		// Token: 0x04003385 RID: 13189
		private TreeListDataSourceHelper _dataSourceHelper;

		// Token: 0x04003386 RID: 13190
		private List<DataKey> _dataKeysArrayList;

		// Token: 0x04003387 RID: 13191
		private List<DataKey> _clientDataKeysArrayList;

		// Token: 0x04003388 RID: 13192
		private TreeListDataKeyArray _dataKeyValues;

		// Token: 0x04003389 RID: 13193
		private TreeListDataKeyArray _clientDataKeyValues;

		// Token: 0x0400338A RID: 13194
		private List<DataKey> _parentDataKeysArrayList;

		// Token: 0x0400338B RID: 13195
		private TreeListDataKeyArray _parentDataKeyValues;

		// Token: 0x0400338C RID: 13196
		private TreeListControlStateManager _controlStateManager;

		// Token: 0x0400338D RID: 13197
		private bool _shouldCallDataBindOnLoad = true;

		// Token: 0x0400338E RID: 13198
		private bool _shouldCallOnSelectedIndexChanged;

		// Token: 0x0400338F RID: 13199
		private TreeListClientSettings _clientSettings;

		// Token: 0x04003390 RID: 13200
		private TreeListCommandItemSettings _commandItemSettings;

		// Token: 0x04003391 RID: 13201
		private TreeListSelectedIndexesCollection _selectedIndexes;

		// Token: 0x04003392 RID: 13202
		private TreeListExpandedIndexesCollection _expandedIndexes;

		// Token: 0x04003393 RID: 13203
		private TreeListExpandedIndexesCollection _clientExpandedIndexes;

		// Token: 0x04003394 RID: 13204
		internal TreeListExpandedIndexesCollection _treeListInitializedExpandCollapseIndexes;

		// Token: 0x04003395 RID: 13205
		private bool Rebound;

		// Token: 0x04003396 RID: 13206
		private TreeListEditIndexesCollection _editIndexes;

		// Token: 0x04003397 RID: 13207
		private TreeListEditIndexesCollection _insertIndexes;

		// Token: 0x04003398 RID: 13208
		private TreeListItemStateCollection _itemState;

		// Token: 0x04003399 RID: 13209
		private Dictionary<TreeListHierarchyIndex, List<TreeListHierarchyIndex>> _footerItems;

		// Token: 0x0400339A RID: 13210
		private TreeListColumnsCollection _declarativeColumns;

		// Token: 0x0400339B RID: 13211
		private TreeListDataItemCollection _items;

		// Token: 0x0400339C RID: 13212
		private TreeListPagerStyle _pagerStyle;

		// Token: 0x0400339D RID: 13213
		private TreeListCommandItemStyle _commandItemStyle;

		// Token: 0x0400339E RID: 13214
		private TreeListTableItemStyle _headerStyle;

		// Token: 0x0400339F RID: 13215
		private TreeListTableItemStyle _itemStyle;

		// Token: 0x040033A0 RID: 13216
		private TreeListTableItemStyle _alternatingItemStyle;

		// Token: 0x040033A1 RID: 13217
		private TreeListTableItemStyle _footerItemStyle;

		// Token: 0x040033A2 RID: 13218
		private TreeListTableItemStyle _selectedItemStyle;

		// Token: 0x040033A3 RID: 13219
		private TreeListTableItemStyle _editItemStyle;

		// Token: 0x040033A4 RID: 13220
		private TreeListSortExpressionCollection _sortExpressions;

		// Token: 0x040033A5 RID: 13221
		private TreeListSortingSettings _sortingSettings;

		// Token: 0x040033A6 RID: 13222
		private ITemplate _pagerTemplate;

		// Token: 0x040033A7 RID: 13223
		private ITemplate _noRecordsTemplate;

		// Token: 0x040033A8 RID: 13224
		private TreeListValidationSettings _validationSettings;

		// Token: 0x040033A9 RID: 13225
		private TreeListEditFormSettings _editFormSettings;

		// Token: 0x040033AA RID: 13226
		private IDictionary _defaultInsertValues;

		// Token: 0x040033AB RID: 13227
		private HybridDictionary _defaultInsertObjects;

		// Token: 0x040033AC RID: 13228
		private Dictionary<TreeListEditableColumn, TreeListCreateCustomEditorDelegate> _customEditorInitializers;

		// Token: 0x040033AD RID: 13229
		internal Dictionary<string, Pair> _popUpLocations;

		// Token: 0x040033AE RID: 13230
		internal List<string> _popUpIds;

		// Token: 0x040033AF RID: 13231
		private object[] _draggedIndexes;

		// Token: 0x040033B0 RID: 13232
		private bool shouldFocusOnPage;

		// Token: 0x040033B1 RID: 13233
		private string _controlToFocus;

		// Token: 0x040033B2 RID: 13234
		private TreeListFocusItemType focusItemType;

		// Token: 0x040033B3 RID: 13235
		private int focusItemIndex;

		// Token: 0x040033B4 RID: 13236
		private TreeListReorderedColumn[] reorderedColumns;

		// Token: 0x040033B5 RID: 13237
		internal bool shouldTrackSelection;

		// Token: 0x040033B6 RID: 13238
		internal Type ModelBindingModelType;

		// Token: 0x040033B7 RID: 13239
		private static TFunc<string, string> parseFireCommandArgs = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[2];
		};

		// Token: 0x040033B8 RID: 13240
		private static TFunc<string, string> parseFireCommandEventName = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[0];
		};

		// Token: 0x040033B9 RID: 13241
		private static TFunc<string, string> parseFireCommandSecondArgs = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[4];
		};

		// Token: 0x040033BA RID: 13242
		private Dictionary<string, Dictionary<string, string>> _calculatedAggregates;

		// Token: 0x040033BB RID: 13243
		private TreeListLocalizationStrings _localization;

		// Token: 0x040033BC RID: 13244
		internal int MostNestedIndex;

		// Token: 0x040033BD RID: 13245
		private List<TreeListColumn> _renderColumns;

		// Token: 0x040033BE RID: 13246
		private List<TreeListColumn> _autoGeneratedColumns;

		// Token: 0x040033BF RID: 13247
		private List<TreeListDataColumn> _previousAutoGeneratedColumns;

		// Token: 0x040033C0 RID: 13248
		internal ITemplate detailTemplate;

		// Token: 0x040033C1 RID: 13249
		private TreeListMobileExportView exportView;

		// Token: 0x040033C2 RID: 13250
		private TreeListMobileColumnsView columnsView;

		// Token: 0x040033C3 RID: 13251
		internal HashSet<TreeListHierarchyIndex> ExpandHash;

		// Token: 0x02001261 RID: 4705
		internal class MostNestedIndexComparission : IComparer<KeyValuePair<TreeListHierarchyIndex, TreeListItemState>>
		{
			// Token: 0x0600C3B1 RID: 50097 RVA: 0x002BE1A8 File Offset: 0x002BC3A8
			public int Compare(KeyValuePair<TreeListHierarchyIndex, TreeListItemState> x, KeyValuePair<TreeListHierarchyIndex, TreeListItemState> y)
			{
				return Comparer<int>.Default.Compare(x.Key.NestedLevel, y.Key.NestedLevel) * -1;
			}
		}
	}
}
