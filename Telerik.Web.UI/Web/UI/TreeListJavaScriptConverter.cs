using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001272 RID: 4722
	internal class TreeListJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x0600C47B RID: 50299 RVA: 0x002BF5D7 File Offset: 0x002BD7D7
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C47C RID: 50300 RVA: 0x002BF5E0 File Offset: 0x002BD7E0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			TreeListClientSettings treeListClientSettings = obj as TreeListClientSettings;
			if (treeListClientSettings != null)
			{
				if (treeListClientSettings.ShouldSerializeAllowPostBackOnItemClick)
				{
					dictionary.Add("_allowPostBackOnItemClick", treeListClientSettings.AllowPostBackOnItemClick);
				}
				if (treeListClientSettings.ShouldSerializePostBackFunction)
				{
					dictionary.Add("_postBackFunction", treeListClientSettings.PostBackFunction);
				}
				if (treeListClientSettings.ShouldSerializeAllowItemsDragDrop)
				{
					dictionary.Add("_allowItemsDragDrop", treeListClientSettings.AllowItemsDragDrop);
				}
				if (treeListClientSettings.ShouldSerializeAllowColumnHide)
				{
					dictionary.Add("_allowColumnHide", treeListClientSettings.AllowColumnHide);
				}
				if (treeListClientSettings.AllowKeyboardNavigation)
				{
					dictionary.Add("_allowKeyboardNavigation", treeListClientSettings.AllowKeyboardNavigation);
				}
				dictionary.Add("_selecting", treeListClientSettings.Selecting);
				dictionary.Add("_scrolling", treeListClientSettings.Scrolling);
				dictionary.Add("_keyboardNavigationSettings", treeListClientSettings.KeyboardNavigationSettings);
				dictionary.Add("_resizing", treeListClientSettings.Resizing);
				dictionary.Add("_clientMessages", treeListClientSettings.ClientMessages);
				dictionary.Add("_reordering", treeListClientSettings.Reordering);
			}
			TreeListSelecting treeListSelecting = obj as TreeListSelecting;
			if (treeListSelecting != null)
			{
				if (treeListSelecting.ShouldSerializeAllowItemSelection)
				{
					dictionary.Add("_allowItemSelection", treeListSelecting.AllowItemSelection);
				}
				if (treeListSelecting.ShouldSerializeAllowToggleSelection)
				{
					dictionary.Add("_allowToggleSelection", treeListSelecting.AllowToggleSelection);
				}
				if (treeListSelecting.ShouldSerializeUseSelectColumnOnly)
				{
					dictionary.Add("_useSelectColumnOnly", treeListSelecting.UseSelectColumnOnly);
				}
			}
			TreeListScrolling treeListScrolling = obj as TreeListScrolling;
			if (treeListScrolling != null)
			{
				if (treeListScrolling.ShouldSerializeAllowScroll)
				{
					dictionary.Add("_allowScroll", treeListScrolling.AllowScroll);
				}
				if (treeListScrolling.ShouldSerializeScrollTop)
				{
					dictionary.Add("_scrollTop", treeListScrolling.ScrollTop);
				}
				if (treeListScrolling.ShouldSerializeScrollLeft)
				{
					dictionary.Add("_scrollLeft", treeListScrolling.ScrollLeft);
				}
				if (treeListScrolling.ShouldSerializeSaveScrollPosition)
				{
					dictionary.Add("_saveScrollPosition", treeListScrolling.SaveScrollPosition);
				}
				if (treeListScrolling.ShouldSerializeUseStaticHeaders)
				{
					dictionary.Add("_useStaticHeaders", treeListScrolling.UseStaticHeaders);
				}
			}
			TreeListKeyboardNavigationSettings treeListKeyboardNavigationSettings = obj as TreeListKeyboardNavigationSettings;
			if (treeListKeyboardNavigationSettings != null)
			{
				if (treeListKeyboardNavigationSettings.ShouldSerializeAllowActiveRowCycle)
				{
					dictionary.Add("_allowActiveRowCycle", treeListKeyboardNavigationSettings.AllowActiveRowCycle);
				}
				if (treeListKeyboardNavigationSettings.ShouldSerializeAllowSubmitOnEnter)
				{
					dictionary.Add("_allowSubmitOnEnter", treeListKeyboardNavigationSettings.AllowSubmitOnEnter);
				}
				if (treeListKeyboardNavigationSettings.ShouldSerializeCollapseChildItemsKey)
				{
					dictionary.Add("_collapseChildItemsKey", treeListKeyboardNavigationSettings.CollapseChildItemsKey);
				}
				if (treeListKeyboardNavigationSettings.ShouldSerializeDeleteActiveRowKey)
				{
					dictionary.Add("_deleteActiveRowKey", treeListKeyboardNavigationSettings.DeleteActiveRowKey);
				}
				if (treeListKeyboardNavigationSettings.ShouldSerializeExitEditInsertModeKey)
				{
					dictionary.Add("_exitEditInsertModeKey", treeListKeyboardNavigationSettings.ExitEditInsertModeKey);
				}
				if (treeListKeyboardNavigationSettings.ShouldSerializeExpandChildItemsKey)
				{
					dictionary.Add("_expandChildItemsKey", treeListKeyboardNavigationSettings.ExpandChildItemsKey);
				}
				if (treeListKeyboardNavigationSettings.ShouldSerializeFocusKey)
				{
					dictionary.Add("_focusKey", treeListKeyboardNavigationSettings.FocusKey);
				}
				if (treeListKeyboardNavigationSettings.ShouldSerializeInitInsertKey)
				{
					dictionary.Add("_initInsertKey", treeListKeyboardNavigationSettings.InitInsertKey);
				}
				if (treeListKeyboardNavigationSettings.ShouldSerializeUpdateInsertItemKey)
				{
					dictionary.Add("_updateInsertItemKey", treeListKeyboardNavigationSettings.UpdateInsertItemKey);
				}
			}
			TreeListResizing treeListResizing = obj as TreeListResizing;
			if (treeListResizing != null)
			{
				if (treeListResizing.AllowColumnResize)
				{
					dictionary.Add("_allowColumnResize", treeListResizing.AllowColumnResize);
				}
				if (treeListResizing.EnableRealTimeResize)
				{
					dictionary.Add("_enableRealTimeResize", treeListResizing.EnableRealTimeResize);
				}
				if (treeListResizing.ResizeMode != TreeListResizeMode.NoScroll)
				{
					dictionary.Add("_resizeMode", treeListResizing.ResizeMode);
				}
			}
			TreeListClientMessages treeListClientMessages = obj as TreeListClientMessages;
			if (treeListClientMessages != null)
			{
				TreeListClientMessages treeListClientMessages2 = treeListClientMessages;
				if (treeListClientMessages2.DragToReorder != "Drag to reorder")
				{
					dictionary.Add("_dragToReorder", treeListClientMessages2.DragToReorder);
				}
				if (treeListClientMessages2.DragToResize != "Drag to resize")
				{
					dictionary.Add("_dragToResize", treeListClientMessages2.DragToResize);
				}
				if (treeListClientMessages2.DropHereToReorder != "Drop here to reorder")
				{
					dictionary.Add("_dropHereToReorder", treeListClientMessages2.DropHereToReorder);
				}
				if (treeListClientMessages2.ColumnResizeTooltipFormatString != "Width: <strong>{0}</strong> <em>pixels</em>")
				{
					dictionary.Add("_columnResizeTooltipFormatString", treeListClientMessages2.ColumnResizeTooltipFormatString);
				}
			}
			TreeListReordering treeListReordering = obj as TreeListReordering;
			if (treeListReordering != null)
			{
				TreeListReordering treeListReordering2 = treeListReordering;
				if (treeListReordering2.AllowColumnsReorder)
				{
					dictionary.Add("_allowColumnsReorder", treeListReordering2.AllowColumnsReorder);
				}
				if (treeListReordering2.ColumnsReorderMethod != TreeListColumnsReorderMethod.Swap)
				{
					dictionary.Add("_columnsReorderMethod", treeListReordering2.ColumnsReorderMethod);
				}
				if (treeListReordering2.ReorderColumnsOnClient)
				{
					dictionary.Add("_reorderColumnsOnClient", treeListReordering2.ReorderColumnsOnClient);
				}
			}
			return dictionary;
		}

		// Token: 0x17003F4B RID: 16203
		// (get) Token: 0x0600C47D RID: 50301 RVA: 0x002BFC74 File Offset: 0x002BDE74
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(TreeListClientSettings);
				yield return typeof(TreeListSelecting);
				yield return typeof(TreeListClientEvents);
				yield return typeof(TreeListScrolling);
				yield return typeof(TreeListKeyboardNavigationSettings);
				yield return typeof(TreeListResizing);
				yield return typeof(TreeListClientMessages);
				yield return typeof(TreeListReordering);
				yield break;
			}
		}
	}
}
