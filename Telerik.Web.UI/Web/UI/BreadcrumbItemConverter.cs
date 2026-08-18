using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200000D RID: 13
	public class BreadcrumbItemConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00003688 File Offset: 0x00001888
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			BreadcrumbItem breadcrumbItem = obj as BreadcrumbItem;
			ExplicitJavaScriptConverter.AddProperty(state, "type", breadcrumbItem.Type, BreadcrumbItemType.Item);
			ExplicitJavaScriptConverter.AddProperty(state, "href", breadcrumbItem.Href, "");
			ExplicitJavaScriptConverter.AddProperty(state, "text", breadcrumbItem.Text, "");
			ExplicitJavaScriptConverter.AddProperty(state, "title", breadcrumbItem.ToolTip, "");
			ExplicitJavaScriptConverter.AddProperty(state, "icon", breadcrumbItem.Icon, "");
			ExplicitJavaScriptConverter.AddProperty(state, "itemClass", breadcrumbItem.ItemClass, "");
			ExplicitJavaScriptConverter.AddProperty(state, "linkClass", breadcrumbItem.LinkClass, "");
			ExplicitJavaScriptConverter.AddProperty(state, "iconClass", breadcrumbItem.IconClass, "");
			ExplicitJavaScriptConverter.AddProperty(state, "disabled", breadcrumbItem.Disabled, false);
			ExplicitJavaScriptConverter.AddProperty(state, "showIcon", breadcrumbItem.ShowIcon, breadcrumbItem.Type == BreadcrumbItemType.RootItem);
			ExplicitJavaScriptConverter.AddProperty(state, "showText", breadcrumbItem.ShowText, breadcrumbItem.Type == BreadcrumbItemType.Item);
			if (breadcrumbItem.Attributes.Count > 0)
			{
				state.Add("attributes", breadcrumbItem.Attributes);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000037DC File Offset: 0x000019DC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(BreadcrumbItem)
				};
			}
		}
	}
}
