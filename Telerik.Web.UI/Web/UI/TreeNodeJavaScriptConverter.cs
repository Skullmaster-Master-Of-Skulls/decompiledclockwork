using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200129F RID: 4767
	internal class TreeNodeJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x0600C7D4 RID: 51156 RVA: 0x002C7B4C File Offset: 0x002C5D4C
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C7D5 RID: 51157 RVA: 0x002C7B54 File Offset: 0x002C5D54
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadTreeNode radTreeNode = (RadTreeNode)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (radTreeNode.Value != string.Empty)
			{
				dictionary.Add("value", radTreeNode.Value);
			}
			if (radTreeNode.Templated)
			{
				dictionary.Add("text", radTreeNode.Text);
			}
			if (radTreeNode.Expanded && radTreeNode.Nodes.Count > 0)
			{
				dictionary.Add("expanded", true);
			}
			if (radTreeNode.Checked)
			{
				dictionary.Add("checked", true);
			}
			IList<ControlItem> visibleItems = radTreeNode.Nodes.VisibleItems;
			if (visibleItems.Count > 0 && radTreeNode.ShouldRenderChildren)
			{
				dictionary.Add("items", visibleItems);
			}
			if (radTreeNode.TreeView.ShouldRenderPostBackReference && !radTreeNode.PostBack)
			{
				dictionary.Add("postBack", false);
			}
			if (radTreeNode.Selected)
			{
				dictionary.Add("selected", true);
			}
			if (!radTreeNode.Enabled)
			{
				dictionary.Add("enabled", false);
			}
			if (radTreeNode.ExpandMode != TreeNodeExpandMode.ClientSide)
			{
				dictionary.Add("expandMode", (int)radTreeNode.ExpandMode);
			}
			if (!string.IsNullOrEmpty(radTreeNode.CssClass))
			{
				dictionary.Add("cssClass", radTreeNode.CssClass);
			}
			if (!string.IsNullOrEmpty(radTreeNode.SelectedCssClass))
			{
				dictionary.Add("selectedCssClass", radTreeNode.SelectedCssClass);
			}
			if (!string.IsNullOrEmpty(radTreeNode.ContentCssClass))
			{
				dictionary.Add("contentCssClass", radTreeNode.ContentCssClass);
			}
			if (!string.IsNullOrEmpty(radTreeNode.HoveredCssClass))
			{
				dictionary.Add("hoveredCssClass", radTreeNode.HoveredCssClass);
			}
			if (!string.IsNullOrEmpty(radTreeNode.DisabledCssClass))
			{
				dictionary.Add("disabledCssClass", radTreeNode.DisabledCssClass);
			}
			if (!string.IsNullOrEmpty(radTreeNode.ContextMenuID))
			{
				dictionary.Add("contextMenuID", radTreeNode.ContextMenuID);
			}
			if (radTreeNode.TreeView.ContextMenus.Count > 0 && !radTreeNode.EnableContextMenu)
			{
				dictionary.Add("enableContextMenu", false);
			}
			if (!radTreeNode.AllowEdit)
			{
				dictionary.Add("allowEdit", false);
			}
			if (!radTreeNode.AllowDrag)
			{
				dictionary.Add("allowDrag", false);
			}
			if (!radTreeNode.AllowDrop)
			{
				dictionary.Add("allowDrop", false);
			}
			if (!radTreeNode.Checkable)
			{
				dictionary.Add("checkable", false);
			}
			if (radTreeNode.CurrentImageUrl != radTreeNode.ImageUrl && !string.IsNullOrEmpty(radTreeNode.ImageUrl))
			{
				dictionary.Add("imageUrl", radTreeNode.ResolveUrl(radTreeNode.ImageUrl));
			}
			if (!string.IsNullOrEmpty(radTreeNode.ExpandedImageUrl))
			{
				dictionary.Add("expandedImageUrl", radTreeNode.ResolveUrl(radTreeNode.ExpandedImageUrl));
			}
			if (!string.IsNullOrEmpty(radTreeNode.DisabledImageUrl))
			{
				dictionary.Add("disabledImageUrl", radTreeNode.ResolveUrl(radTreeNode.DisabledImageUrl));
			}
			if (!string.IsNullOrEmpty(radTreeNode.SelectedImageUrl))
			{
				dictionary.Add("selectedImageUrl", radTreeNode.ResolveUrl(radTreeNode.SelectedImageUrl));
			}
			if (!string.IsNullOrEmpty(radTreeNode.HoveredImageUrl))
			{
				dictionary.Add("hoveredImageUrl", radTreeNode.ResolveUrl(radTreeNode.HoveredImageUrl));
			}
			if (!string.IsNullOrEmpty(radTreeNode.NavigateUrl) && radTreeNode.Templated)
			{
				dictionary.Add("navigateUrl", radTreeNode.ResolveUrl(radTreeNode.NavigateUrl));
			}
			if (!string.IsNullOrEmpty(radTreeNode.Category))
			{
				dictionary.Add("category", radTreeNode.Category);
			}
			if (!string.IsNullOrEmpty(radTreeNode.ToolTip))
			{
				dictionary.Add("toolTip", radTreeNode.ToolTip);
			}
			if (radTreeNode.SkipLogging)
			{
				dictionary.Add("skip", true);
			}
			AttributeCollectionConverter attributeCollectionConverter = new AttributeCollectionConverter();
			IDictionary<string, object> dictionary2 = attributeCollectionConverter.Serialize(radTreeNode.Attributes, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("attributes", dictionary2);
			}
			return dictionary;
		}

		// Token: 0x17004090 RID: 16528
		// (get) Token: 0x0600C7D6 RID: 51158 RVA: 0x002C8010 File Offset: 0x002C6210
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RadTreeNode);
				yield break;
			}
		}
	}
}
