using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001B33 RID: 6963
	internal class GridJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x06010D9C RID: 69020 RVA: 0x003BC8F7 File Offset: 0x003BAAF7
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06010D9D RID: 69021 RVA: 0x003BC900 File Offset: 0x003BAB00
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (obj is GridFilterMenu)
			{
				GridFilterMenu gridFilterMenu = (GridFilterMenu)obj;
			}
			if (obj is GridClientSettings)
			{
				GridClientSettings gridClientSettings = (GridClientSettings)obj;
				if (gridClientSettings.AllowRowsDragDrop)
				{
					dictionary.Add("AllowRowsDragDrop", gridClientSettings.AllowRowsDragDrop);
				}
				if (gridClientSettings.AllowAutoScrollOnDragDrop)
				{
					dictionary.Add("AllowAutoScrollOnDragDrop", gridClientSettings.AllowAutoScrollOnDragDrop);
				}
				if (gridClientSettings.EnableRowHoverStyle)
				{
					dictionary.Add("EnableRowHoverStyle", gridClientSettings.EnableRowHoverStyle);
				}
				if (!gridClientSettings.EnableAlternatingItems)
				{
					dictionary.Add("EnableAlternatingItems", gridClientSettings.EnableAlternatingItems);
				}
				if (gridClientSettings.AllowColumnHide)
				{
					dictionary.Add("AllowColumnHide", gridClientSettings.AllowColumnHide);
				}
				if (gridClientSettings.AllowColumnsReorder)
				{
					dictionary.Add("AllowColumnsReorder", gridClientSettings.AllowColumnsReorder);
				}
				if (gridClientSettings.AllowDragToGroup)
				{
					dictionary.Add("AllowDragToGroup", gridClientSettings.AllowDragToGroup);
				}
				if (!gridClientSettings.AllowExpandCollapse)
				{
					dictionary.Add("AllowExpandCollapse", gridClientSettings.AllowExpandCollapse);
				}
				if (!gridClientSettings.AllowGroupExpandCollapse)
				{
					dictionary.Add("AllowGroupExpandCollapse", gridClientSettings.AllowGroupExpandCollapse);
				}
				if (gridClientSettings.AllowKeyboardNavigation)
				{
					dictionary.Add("AllowKeyboardNavigation", gridClientSettings.AllowKeyboardNavigation);
				}
				if (gridClientSettings.AllowRowHide)
				{
					dictionary.Add("AllowRowHide", gridClientSettings.AllowRowHide);
				}
				if (gridClientSettings.ColumnsReorderMethod != GridClientSettings.GridColumnsReorderMethod.Swap)
				{
					dictionary.Add("ColumnsReorderMethod", gridClientSettings.ColumnsReorderMethod);
				}
				if (gridClientSettings.EnablePostBackOnRowClick)
				{
					dictionary.Add("EnablePostBackOnRowClick", gridClientSettings.EnablePostBackOnRowClick);
				}
				if (gridClientSettings.ShouldCreateRows)
				{
					dictionary.Add("ShouldCreateRows", gridClientSettings.ShouldCreateRows);
				}
				if (gridClientSettings.ReorderColumnsOnClient)
				{
					dictionary.Add("ReorderColumnsOnClient", gridClientSettings.ReorderColumnsOnClient);
				}
				if (gridClientSettings.EnableClientPrint)
				{
					dictionary.Add("EnableClientPrint", gridClientSettings.EnableClientPrint);
				}
				if (!string.IsNullOrEmpty(gridClientSettings.PostBackFunction) && gridClientSettings.PostBackFunction != "__doPostBack('{0}','{1}')")
				{
					dictionary.Add("PostBackFunction", gridClientSettings.PostBackFunction);
				}
				dictionary.Add("DataBinding", ((GridClientSettings)obj).DataBinding);
				dictionary.Add("Selecting", ((GridClientSettings)obj).Selecting);
				dictionary.Add("Scrolling", ((GridClientSettings)obj).Scrolling);
				dictionary.Add("Resizing", ((GridClientSettings)obj).Resizing);
				dictionary.Add("ClientMessages", ((GridClientSettings)obj).ClientMessages);
				dictionary.Add("KeyboardNavigationSettings", ((GridClientSettings)obj).KeyboardNavigationSettings);
				dictionary.Add("Animation", ((GridClientSettings)obj).Animation);
				dictionary.Add("Virtualization", ((GridClientSettings)obj).Virtualization);
			}
			if (obj is GridClientDataBinding)
			{
				GridClientDataBinding gridClientDataBinding = (GridClientDataBinding)obj;
				if (!string.IsNullOrEmpty(gridClientDataBinding.Location))
				{
					dictionary.Add("Location", HttpContext.Current.Response.ApplyAppPathModifier(gridClientDataBinding.Location));
				}
				if (!string.IsNullOrEmpty(gridClientDataBinding.SelectMethod))
				{
					dictionary.Add("SelectMethod", gridClientDataBinding.SelectMethod);
				}
				if (!string.IsNullOrEmpty(gridClientDataBinding.SelectCountMethod))
				{
					dictionary.Add("SelectCountMethod", gridClientDataBinding.SelectCountMethod);
				}
				if (gridClientDataBinding.MaximumRowsParameterName != "maximumRows")
				{
					dictionary.Add("MaximumRowsParameterName", gridClientDataBinding.MaximumRowsParameterName);
				}
				if (gridClientDataBinding.StartRowIndexParameterName != "startRowIndex")
				{
					dictionary.Add("StartRowIndexParameterName", gridClientDataBinding.StartRowIndexParameterName);
				}
				if (gridClientDataBinding.SortParameterName != "sortExpression")
				{
					dictionary.Add("SortParameterName", gridClientDataBinding.SortParameterName);
				}
				if (gridClientDataBinding.FilterParameterName != "filterExpression")
				{
					dictionary.Add("FilterParameterName", gridClientDataBinding.FilterParameterName);
				}
				if (gridClientDataBinding.EnableCaching)
				{
					dictionary.Add("EnableCaching", gridClientDataBinding.EnableCaching);
				}
				if (gridClientDataBinding.SortParameterType != GridClientDataBindingParameterType.List)
				{
					dictionary.Add("SortParameterType", gridClientDataBinding.SortParameterType);
				}
				if (gridClientDataBinding.FilterParameterType != GridClientDataBindingParameterType.List)
				{
					dictionary.Add("FilterParameterType", gridClientDataBinding.FilterParameterType);
				}
				if (gridClientDataBinding.DataPropertyName != "Data")
				{
					dictionary.Add("DataPropertyName", gridClientDataBinding.DataPropertyName);
				}
				if (gridClientDataBinding.CountPropertyName != "Count")
				{
					dictionary.Add("CountPropertyName", gridClientDataBinding.CountPropertyName);
				}
				if (gridClientDataBinding.ResponseType != GridClientDataResponseType.JSON)
				{
					dictionary.Add("ResponseType", gridClientDataBinding.ResponseType);
				}
				if (!string.IsNullOrEmpty(gridClientDataBinding.DataService.TableName))
				{
					dictionary.Add("DataService", gridClientDataBinding.DataService);
				}
			}
			if (obj is GridClientDataService)
			{
				GridClientDataService gridClientDataService = (GridClientDataService)obj;
				if (!string.IsNullOrEmpty(gridClientDataService.TableName))
				{
					dictionary.Add("TableName", gridClientDataService.TableName);
				}
				if (gridClientDataService.Type != GridClientDataServiceType.ADONet)
				{
					dictionary.Add("Type", gridClientDataService.Type);
				}
				if (!string.IsNullOrEmpty(gridClientDataService.FilterQueryOption))
				{
					dictionary.Add("FilterQueryOption", gridClientDataService.FilterQueryOption);
				}
				if (!string.IsNullOrEmpty(gridClientDataService.SortQueryOption))
				{
					dictionary.Add("SortQueryOption", gridClientDataService.SortQueryOption);
				}
			}
			if (obj is GridKeyboardNavigationSettings)
			{
				GridKeyboardNavigationSettings gridKeyboardNavigationSettings = (GridKeyboardNavigationSettings)obj;
				dictionary.Add("AllowActiveRowCycle", gridKeyboardNavigationSettings.AllowActiveRowCycle);
				dictionary.Add("EnableKeyboardShortcuts", gridKeyboardNavigationSettings.EnableKeyboardShortcuts);
				dictionary.Add("FocusKey", (int)gridKeyboardNavigationSettings.FocusKey);
				if (gridKeyboardNavigationSettings.EnableKeyboardShortcuts)
				{
					dictionary.Add("InitInsertKey", (int)gridKeyboardNavigationSettings.InitInsertKey);
					dictionary.Add("RebindKey", (int)gridKeyboardNavigationSettings.RebindKey);
					dictionary.Add("ExitEditInsertModeKey", gridKeyboardNavigationSettings.ExitEditInsertModeKey);
					dictionary.Add("UpdateInsertItemKey", gridKeyboardNavigationSettings.UpdateInsertItemKey);
					dictionary.Add("DeleteActiveRow", gridKeyboardNavigationSettings.DeleteActiveRow);
					dictionary.Add("ExpandDetailTableKey", gridKeyboardNavigationSettings.ExpandDetailTableKey);
					dictionary.Add("CollapseDetailTableKey", gridKeyboardNavigationSettings.CollapseDetailTableKey);
					dictionary.Add("MoveDownKey", gridKeyboardNavigationSettings.MoveDownKey);
					dictionary.Add("MoveUpKey", gridKeyboardNavigationSettings.MoveUpKey);
					if (gridKeyboardNavigationSettings.AllowSubmitOnEnter)
					{
						dictionary.Add("AllowSubmitOnEnter", gridKeyboardNavigationSettings.AllowSubmitOnEnter);
					}
					if (!string.IsNullOrEmpty(gridKeyboardNavigationSettings.ValidationGroup))
					{
						dictionary.Add("ValidationGroup", gridKeyboardNavigationSettings.ValidationGroup);
					}
					dictionary.Add("SaveChangesKey", gridKeyboardNavigationSettings.SaveChangesKey);
					dictionary.Add("CancelChangesKey", gridKeyboardNavigationSettings.CancelChangesKey);
				}
			}
			if (obj is GridSelecting)
			{
				GridSelecting gridSelecting = (GridSelecting)obj;
				dictionary.Add("CellSelectionMode", gridSelecting.CellSelectionMode);
				if (!gridSelecting.AllowRowSelect)
				{
					return dictionary;
				}
				dictionary.Add("AllowRowSelect", gridSelecting.AllowRowSelect);
				if (gridSelecting.EnableDragToSelectRows)
				{
					dictionary.Add("EnableDragToSelectRows", gridSelecting.EnableDragToSelectRows);
				}
				if (gridSelecting.UseClientSelectColumnOnly)
				{
					dictionary.Add("UseClientSelectColumnOnly", gridSelecting.UseClientSelectColumnOnly);
				}
			}
			GridClientEvents gridClientEvents = obj as GridClientEvents;
			if (obj is GridScrolling)
			{
				GridScrolling gridScrolling = (GridScrolling)obj;
				if (!gridScrolling.AllowScroll)
				{
					return dictionary;
				}
				dictionary.Add("AllowScroll", gridScrolling.AllowScroll);
				if (gridScrolling.EnableVirtualScrollPaging)
				{
					dictionary.Add("EnableVirtualScrollPaging", gridScrolling.EnableVirtualScrollPaging);
				}
				if (!string.IsNullOrEmpty(gridScrolling.AJAXScrollTop))
				{
					dictionary.Add("AJAXScrollTop", gridScrolling.AJAXScrollTop);
				}
				if (gridScrolling.FrozenColumnsCount > 0 || gridScrolling.EnableColumnClientFreeze)
				{
					dictionary.Add("FrozenColumnsCount", gridScrolling.FrozenColumnsCount);
				}
				if (gridScrolling.FrozenColumnsCount > 0 && !gridScrolling.CountGroupSplitterColumnAsFrozen)
				{
					dictionary.Add("CountGroupSplitterColumnAsFrozen", false);
				}
				if (gridScrolling.SaveScrollPosition)
				{
					dictionary.Add("SaveScrollPosition", gridScrolling.SaveScrollPosition);
				}
				if (gridScrolling.ScrollBarWidth != Unit.Empty)
				{
					dictionary.Add("ScrollBarWidth", gridScrolling.ScrollBarWidth.ToString());
				}
				if (gridScrolling.ScrollHeight != Unit.Empty)
				{
					dictionary.Add("ScrollHeight", gridScrolling.ScrollHeight.ToString());
				}
				if (!string.IsNullOrEmpty(gridScrolling.ScrollLeft))
				{
					dictionary.Add("ScrollLeft", gridScrolling.ScrollLeft.ToString());
				}
				if (!string.IsNullOrEmpty(gridScrolling.ScrollTop))
				{
					dictionary.Add("ScrollTop", gridScrolling.ScrollTop.ToString());
				}
				if (gridScrolling.UseStaticHeaders)
				{
					dictionary.Add("UseStaticHeaders", gridScrolling.UseStaticHeaders);
				}
			}
			if (obj is GridVirtualization)
			{
				GridVirtualization gridVirtualization = (GridVirtualization)obj;
				if (gridVirtualization.EnableVirtualization)
				{
					dictionary.Add("EnableVirtualization", gridVirtualization.EnableVirtualization);
					if (gridVirtualization.EnableCurrentPageScrollOnly)
					{
						dictionary.Add("EnableCurrentPageScrollOnly", gridVirtualization.EnableCurrentPageScrollOnly);
					}
					if (gridVirtualization.InitiallyCachedItemsCount != 5000)
					{
						dictionary.Add("InitiallyCachedItemsCount", gridVirtualization.InitiallyCachedItemsCount);
					}
					if (gridVirtualization.RetrievedItemsPerRequest != 1000)
					{
						dictionary.Add("RetrievedItemsPerRequest", gridVirtualization.RetrievedItemsPerRequest);
					}
					if (gridVirtualization.ItemsPerView != 100)
					{
						dictionary.Add("ItemsPerView", gridVirtualization.ItemsPerView);
					}
					if (gridVirtualization.MaxCacheSize != 2147483647)
					{
						dictionary.Add("MaxCacheSize", gridVirtualization.MaxCacheSize);
					}
					if (gridVirtualization.CurrentPageIndex > 0)
					{
						dictionary.Add("CurrentPageIndex", gridVirtualization.CurrentPageIndex);
					}
					if (gridVirtualization.ItemAtTop > 0m)
					{
						dictionary.Add("ItemAtTop", gridVirtualization.ItemAtTop);
						dictionary.Add("StartIndex", gridVirtualization.StartIndex);
					}
				}
			}
			if (obj is GridClientMessages)
			{
				GridClientMessages gridClientMessages = (GridClientMessages)obj;
				if (gridClientMessages.DragToGroupOrReorder != "Drag to group or reorder")
				{
					dictionary.Add("DragToGroupOrReorder", gridClientMessages.DragToGroupOrReorder);
				}
				if (gridClientMessages.DragToResize != "Drag to resize")
				{
					dictionary.Add("DragToResize", gridClientMessages.DragToResize);
				}
				if (gridClientMessages.DropHereToReorder != "Drop here to reorder")
				{
					dictionary.Add("DropHereToReorder", gridClientMessages.DropHereToReorder);
				}
				if (gridClientMessages.PagerTooltipFormatString != "Page <strong>{0}</strong> of <strong>{1}</strong>")
				{
					dictionary.Add("PagerTooltipFormatString", gridClientMessages.PagerTooltipFormatString);
				}
				if (gridClientMessages.ColumnResizeTooltipFormatString != "Width: <strong>{0}</strong> <em>pixels</em>")
				{
					dictionary.Add("ColumnResizeTooltipFormatString", gridClientMessages.ColumnResizeTooltipFormatString);
				}
			}
			if (obj is GridResizing)
			{
				GridResizing gridResizing = (GridResizing)obj;
				if (gridResizing.AllowColumnResize)
				{
					dictionary.Add("AllowColumnResize", gridResizing.AllowColumnResize);
				}
				if (gridResizing.AllowRowResize)
				{
					dictionary.Add("AllowRowResize", gridResizing.AllowRowResize);
				}
				if (gridResizing.ClipCellContentOnResize && (gridResizing.AllowColumnResize || gridResizing.AllowRowResize))
				{
					dictionary.Add("ClipCellContentOnResize", gridResizing.ClipCellContentOnResize);
				}
				if (gridResizing.EnableRealTimeResize)
				{
					dictionary.Add("EnableRealTimeResize", gridResizing.EnableRealTimeResize);
				}
				if (gridResizing.ResizeGridOnColumnResize)
				{
					dictionary.Add("ResizeGridOnColumnResize", gridResizing.ResizeGridOnColumnResize);
				}
				if (gridResizing.AllowResizeToFit)
				{
					dictionary.Add("AllowResizeToFit", gridResizing.AllowResizeToFit);
				}
				if (gridResizing.EnableNextColumnResize)
				{
					dictionary.Add("EnableNextColumnResize", gridResizing.EnableNextColumnResize);
				}
			}
			if (obj is GridAnimationSettings)
			{
				GridAnimationSettings gridAnimationSettings = (GridAnimationSettings)obj;
				if (gridAnimationSettings.AllowColumnReorderAnimation)
				{
					dictionary.Add("AllowColumnReorderAnimation", gridAnimationSettings.AllowColumnReorderAnimation);
					dictionary.Add("ColumnReorderAnimationDuration", gridAnimationSettings.ColumnReorderAnimationDuration);
				}
				if (gridAnimationSettings.AllowColumnRevertAnimation)
				{
					dictionary.Add("AllowColumnRevertAnimation", gridAnimationSettings.AllowColumnRevertAnimation);
					dictionary.Add("ColumnRevertAnimationDuration", gridAnimationSettings.ColumnRevertAnimationDuration);
				}
			}
			return dictionary;
		}

		// Token: 0x17005225 RID: 21029
		// (get) Token: 0x06010D9E RID: 69022 RVA: 0x003BD5EC File Offset: 0x003BB7EC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(GridVirtualization),
					typeof(GridClientSettings),
					typeof(GridClientDataBinding),
					typeof(GridClientDataService),
					typeof(GridSelecting),
					typeof(GridScrolling),
					typeof(GridResizing),
					typeof(GridClientEvents),
					typeof(GridClientMessages),
					typeof(GridKeyboardNavigationSettings),
					typeof(GridValidationSettings),
					typeof(GridAnimationSettings)
				};
			}
		}
	}
}
