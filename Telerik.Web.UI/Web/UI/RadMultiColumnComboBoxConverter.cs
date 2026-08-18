using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020005F5 RID: 1525
	public class RadMultiColumnComboBoxConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003729 RID: 14121 RVA: 0x000B6624 File Offset: 0x000B4824
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			RadMultiColumnComboBox radMultiColumnComboBox = obj as RadMultiColumnComboBox;
			string text = "javascript:";
			ExplicitJavaScriptConverter.AddProperty(state, "_skin", radMultiColumnComboBox.RuntimeSkin, "Default");
			ExplicitJavaScriptConverter.AddProperty(state, "animation", radMultiColumnComboBox.AnimationSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "cascadeFrom", radMultiColumnComboBox.CascadeFrom, "");
			ExplicitJavaScriptConverter.AddProperty(state, "cascadeFromField", radMultiColumnComboBox.CascadeFromField, "");
			ExplicitJavaScriptConverter.AddProperty(state, "cascadeFromParentField", radMultiColumnComboBox.CascadeFromParentField, "");
			if (radMultiColumnComboBox.ColumnsCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "columns", radMultiColumnComboBox.ColumnsCollection.ItemsList, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "clearButton", radMultiColumnComboBox.ClearButton, true);
			ExplicitJavaScriptConverter.AddProperty(state, "dataTextField", radMultiColumnComboBox.DataTextField, "");
			ExplicitJavaScriptConverter.AddProperty(state, "dataValueField", radMultiColumnComboBox.DataValueField, "");
			ExplicitJavaScriptConverter.AddProperty(state, "delay", radMultiColumnComboBox.Delay, 200.0);
			ExplicitJavaScriptConverter.AddProperty(state, "dropDownWidth", radMultiColumnComboBox.DropDownWidth, "");
			ExplicitJavaScriptConverter.AddProperty(state, "enable", radMultiColumnComboBox.Enable, true);
			ExplicitJavaScriptConverter.AddProperty(state, "enforceMinLength", radMultiColumnComboBox.EnforceMinLength, false);
			ExplicitJavaScriptConverter.AddProperty(state, "filter", radMultiColumnComboBox.Filter.ToLower(), "none");
			ExplicitJavaScriptConverter.AddProperty(state, "filterFields", radMultiColumnComboBox.FilterFields, null);
			if (radMultiColumnComboBox.FixedGroupTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "fixedGroupTemplate", radMultiColumnComboBox.FixedGroupTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "fixedGroupTemplate", radMultiColumnComboBox.FixedGroupTemplate, "");
			}
			if (radMultiColumnComboBox.FooterTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "footerTemplate", radMultiColumnComboBox.FooterTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "footerTemplate", radMultiColumnComboBox.FooterTemplate, "");
			}
			if (radMultiColumnComboBox.GroupTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "groupTemplate", radMultiColumnComboBox.GroupTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "groupTemplate", radMultiColumnComboBox.GroupTemplate, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "height", radMultiColumnComboBox.Height.Value, 200.0);
			ExplicitJavaScriptConverter.AddProperty(state, "highlightFirst", radMultiColumnComboBox.HighlightFirst, true);
			ExplicitJavaScriptConverter.AddProperty(state, "ignoreCase", radMultiColumnComboBox.IgnoreCase, true);
			ExplicitJavaScriptConverter.AddProperty(state, "index", radMultiColumnComboBox.Index, -1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "minLength", radMultiColumnComboBox.MinLength, 1.0);
			if (radMultiColumnComboBox.NoDataTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "noDataTemplate", radMultiColumnComboBox.NoDataTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "noDataTemplate", radMultiColumnComboBox.NoDataTemplate, "NO DATA FOUND.");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "placeholder", radMultiColumnComboBox.Placeholder, "");
			ExplicitJavaScriptConverter.AddProperty(state, "messages", radMultiColumnComboBox.MessagesSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "popup", radMultiColumnComboBox.PopupSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "suggest", radMultiColumnComboBox.Suggest, false);
			ExplicitJavaScriptConverter.AddProperty(state, "syncValueAndText", radMultiColumnComboBox.SyncValueAndText, true);
			if (radMultiColumnComboBox.HeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "headerTemplate", radMultiColumnComboBox.HeaderTemplate.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "headerTemplate", radMultiColumnComboBox.HeaderTemplate, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "text", radMultiColumnComboBox.Text, "");
			ExplicitJavaScriptConverter.AddProperty(state, "value", radMultiColumnComboBox.Value, "");
			ExplicitJavaScriptConverter.AddProperty(state, "valuePrimitive", radMultiColumnComboBox.ValuePrimitive, false);
			ExplicitJavaScriptConverter.AddProperty(state, "virtual", radMultiColumnComboBox.Virtual, false);
			ExplicitJavaScriptConverter.AddProperty(state, "virtual", radMultiColumnComboBox.VirtualSettings, null);
			base.AddScript(state, "change", radMultiColumnComboBox.ClientEvents.OnChange);
			base.AddScript(state, "close", radMultiColumnComboBox.ClientEvents.OnClose);
			base.AddScript(state, "dataBound", radMultiColumnComboBox.ClientEvents.OnDataBound);
			base.AddScript(state, "filtering", radMultiColumnComboBox.ClientEvents.OnFiltering);
			base.AddScript(state, "open", radMultiColumnComboBox.ClientEvents.OnOpen);
			base.AddScript(state, "select", radMultiColumnComboBox.ClientEvents.OnSelect);
			base.AddScript(state, "cascade", radMultiColumnComboBox.ClientEvents.OnCascade);
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x000B6B70 File Offset: 0x000B4D70
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadMultiColumnComboBox)
				};
			}
		}
	}
}
