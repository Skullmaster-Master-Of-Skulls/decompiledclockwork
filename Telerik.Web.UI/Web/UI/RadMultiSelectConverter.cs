using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000613 RID: 1555
	public class RadMultiSelectConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600387C RID: 14460 RVA: 0x000B9BE0 File Offset: 0x000B7DE0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			RadMultiSelect radMultiSelect = obj as RadMultiSelect;
			string text = "javascript:";
			ExplicitJavaScriptConverter.AddProperty(state, "theme", radMultiSelect.RuntimeSkin, "Default");
			ExplicitJavaScriptConverter.AddProperty(state, "animation", radMultiSelect.Animation, false);
			ExplicitJavaScriptConverter.AddProperty(state, "animation", radMultiSelect.AnimationSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "autoClose", radMultiSelect.AutoClose, true);
			ExplicitJavaScriptConverter.AddProperty(state, "autoBind", radMultiSelect.AutoBind, true);
			ExplicitJavaScriptConverter.AddProperty(state, "autoWidth", radMultiSelect.AutoWidth, false);
			ExplicitJavaScriptConverter.AddProperty(state, "clearButton", radMultiSelect.ClearButton, true);
			ExplicitJavaScriptConverter.AddProperty(state, "dataTextField", radMultiSelect.DataTextField, "");
			ExplicitJavaScriptConverter.AddProperty(state, "dataValueField", radMultiSelect.DataValueField, "");
			ExplicitJavaScriptConverter.AddProperty(state, "delay", radMultiSelect.Delay, 200.0);
			ExplicitJavaScriptConverter.AddProperty(state, "enable", radMultiSelect.GetEnabled, true);
			ExplicitJavaScriptConverter.AddProperty(state, "enforceMinLength", radMultiSelect.EnforceMinLength, false);
			ExplicitJavaScriptConverter.AddProperty(state, "filter", radMultiSelect.Filter, RadMultiSelectFilter.StartsWith);
			if (radMultiSelect.FixedGroupTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "fixedGroupTemplate", radMultiSelect.FixedGroupTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "fixedGroupTemplate", radMultiSelect.FixedGroupTemplate, "");
			}
			if (radMultiSelect.FooterTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "footerTemplate", radMultiSelect.FooterTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "footerTemplate", radMultiSelect.FooterTemplate, "");
			}
			if (radMultiSelect.GroupTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "groupTemplate", radMultiSelect.GroupTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "groupTemplate", radMultiSelect.GroupTemplate, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "height", radMultiSelect.DropDownHeight.Value, 200.0);
			ExplicitJavaScriptConverter.AddProperty(state, "highlightFirst", radMultiSelect.HighlightFirst, true);
			ExplicitJavaScriptConverter.AddProperty(state, "ignoreCase", radMultiSelect.IgnoreCase, true);
			ExplicitJavaScriptConverter.AddProperty(state, "messages", radMultiSelect.MessagesSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "minLength", radMultiSelect.MinLength, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "maxSelectedItems", radMultiSelect.MaxSelectedItems, 0.0);
			if (radMultiSelect.NoDataTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "noDataTemplate", radMultiSelect.NoDataTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "noDataTemplate", radMultiSelect.NoDataTemplate, true);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "placeholder", radMultiSelect.Placeholder, "");
			ExplicitJavaScriptConverter.AddProperty(state, "popup", radMultiSelect.PopupSettings, null);
			if (radMultiSelect.HeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "headerTemplate", radMultiSelect.HeaderTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "headerTemplate", radMultiSelect.HeaderTemplate, "");
			}
			if (radMultiSelect.ItemTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "itemTemplate", radMultiSelect.ItemTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "itemTemplate", radMultiSelect.ItemTemplate, "");
			}
			if (radMultiSelect.TagTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "tagTemplate", radMultiSelect.TagTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "tagTemplate", radMultiSelect.TagTemplate, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "tagMode", radMultiSelect.TagMode, RadMultiSelectTagMode.Multiple);
			ExplicitJavaScriptConverter.AddProperty(state, "value", radMultiSelect.Value, null);
			ExplicitJavaScriptConverter.AddProperty(state, "valuePrimitive", radMultiSelect.ValuePrimitive, false);
			ExplicitJavaScriptConverter.AddProperty(state, "virtual", radMultiSelect.Virtual, false);
			ExplicitJavaScriptConverter.AddProperty(state, "virtual", radMultiSelect.VirtualSettings, null);
			base.AddScript(state, "change", radMultiSelect.ClientEvents.OnChange);
			base.AddScript(state, "close", radMultiSelect.ClientEvents.OnClose);
			base.AddScript(state, "dataBound", radMultiSelect.ClientEvents.OnDataBound);
			base.AddScript(state, "filtering", radMultiSelect.ClientEvents.OnFiltering);
			base.AddScript(state, "open", radMultiSelect.ClientEvents.OnOpen);
			base.AddScript(state, "select", radMultiSelect.ClientEvents.OnSelect);
			base.AddScript(state, "deselect", radMultiSelect.ClientEvents.OnDeselect);
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x000BA17C File Offset: 0x000B837C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadMultiSelect)
				};
			}
		}
	}
}
