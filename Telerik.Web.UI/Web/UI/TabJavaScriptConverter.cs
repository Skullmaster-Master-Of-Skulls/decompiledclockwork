using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001AD0 RID: 6864
	internal class TabJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x060109F3 RID: 68083 RVA: 0x003B52F4 File Offset: 0x003B34F4
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060109F4 RID: 68084 RVA: 0x003B52FC File Offset: 0x003B34FC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadTab radTab = (RadTab)obj;
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			IList<ControlItem> visibleItems = radTab.Tabs.VisibleItems;
			if (radTab.TabStrip.ReorderTabsOnSelect)
			{
				dictionary.Add("index", radTab.ReorderedIndex);
			}
			if (radTab.Templated)
			{
				dictionary.Add("text", radTab.Text);
			}
			if (radTab.IsSeparator)
			{
				dictionary.Add("isSeparator", radTab.IsSeparator);
			}
			if (radTab.ScrollChildren)
			{
				dictionary.Add("scrollChildren", radTab.ScrollChildren);
			}
			if (radTab.PerTabScrolling)
			{
				dictionary.Add("perTabScrolling", radTab.PerTabScrolling);
			}
			if (radTab.ScrollButtonsPosition != TabStripScrollButtonsPosition.Right)
			{
				dictionary.Add("scrollButtonsPosition", radTab.ScrollButtonsPosition);
			}
			if (radTab.SelectedTab != null)
			{
				dictionary.Add("selectedIndex", radTab.Tabs.VisibleItems.IndexOf(radTab.SelectedTab));
			}
			if (!string.IsNullOrEmpty(radTab.Value))
			{
				dictionary.Add("value", radTab.Value);
			}
			if (!radTab.Enabled)
			{
				dictionary.Add("enabled", radTab.Enabled);
			}
			if (!string.IsNullOrEmpty(radTab.DisabledCssClass))
			{
				dictionary.Add("disabledCssClass", radTab.DisabledCssClass);
			}
			if (!string.IsNullOrEmpty(radTab.SelectedCssClass))
			{
				dictionary.Add("selectedCssClass", radTab.SelectedCssClass);
			}
			if (!string.IsNullOrEmpty(radTab.HoveredCssClass))
			{
				dictionary.Add("hoveredCssClass", radTab.HoveredCssClass);
			}
			if (!string.IsNullOrEmpty(radTab.OuterCssClass))
			{
				dictionary.Add("outerCssClass", radTab.OuterCssClass);
			}
			if (!string.IsNullOrEmpty(radTab.CssClass))
			{
				dictionary.Add("cssClass", radTab.CssClass);
			}
			if (!string.IsNullOrEmpty(radTab.ImageUrl))
			{
				dictionary.Add("imageUrl", radTab.ResolveClientUrl(radTab.ImageUrl));
			}
			if (radTab.IsBreak)
			{
				dictionary.Add("isBreak", radTab.IsBreak);
			}
			if (!string.IsNullOrEmpty(radTab.SelectedImageUrl))
			{
				dictionary.Add("selectedImageUrl", radTab.ResolveClientUrl(radTab.SelectedImageUrl));
			}
			if (!string.IsNullOrEmpty(radTab.HoveredImageUrl))
			{
				dictionary.Add("hoveredImageUrl", radTab.ResolveClientUrl(radTab.HoveredImageUrl));
			}
			if (!string.IsNullOrEmpty(radTab.DisabledImageUrl))
			{
				dictionary.Add("disabledImageUrl", radTab.ResolveClientUrl(radTab.DisabledImageUrl));
			}
			if (visibleItems.Count > 0)
			{
				dictionary.Add("items", visibleItems);
			}
			if (!radTab.PostBack)
			{
				dictionary.Add("postback", false);
			}
			if (radTab.PageView != null)
			{
				if (!string.IsNullOrEmpty(radTab.PageViewID))
				{
					dictionary.Add("pageViewID", radTab.PageView.ClientID);
				}
				else
				{
					dictionary.Add("_implPageViewID", radTab.PageView.ClientID);
				}
			}
			AttributeCollectionConverter attributeCollectionConverter = new AttributeCollectionConverter();
			IDictionary<string, object> dictionary2 = attributeCollectionConverter.Serialize(radTab.Attributes, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("attributes", dictionary2);
			}
			return dictionary;
		}

		// Token: 0x170050D0 RID: 20688
		// (get) Token: 0x060109F5 RID: 68085 RVA: 0x003B56F0 File Offset: 0x003B38F0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RadTab);
				yield break;
			}
		}
	}
}
