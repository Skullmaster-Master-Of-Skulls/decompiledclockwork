using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020009B0 RID: 2480
	public class AutoCompleteBoxTextSettings : ObjectWithState
	{
		// Token: 0x06005F0B RID: 24331 RVA: 0x00122162 File Offset: 0x00120362
		public AutoCompleteBoxTextSettings(StateBag ownerViewState) : base("TextSettings", ownerViewState)
		{
		}

		// Token: 0x17001F5D RID: 8029
		// (get) Token: 0x06005F0C RID: 24332 RVA: 0x00122170 File Offset: 0x00120370
		// (set) Token: 0x06005F0D RID: 24333 RVA: 0x00122191 File Offset: 0x00120391
		[Category("Behavior")]
		[Description("The selection mode of the RadAutoCompleteBox.")]
		[DefaultValue(RadAutoCompleteSelectionMode.Multiple)]
		[NotifyParentProperty(true)]
		public RadAutoCompleteSelectionMode SelectionMode
		{
			get
			{
				return (RadAutoCompleteSelectionMode)(base.ViewState["SelectionMode"] ?? RadAutoCompleteSelectionMode.Multiple);
			}
			set
			{
				base.ViewState["SelectionMode"] = value;
			}
		}

		// Token: 0x06005F0E RID: 24334 RVA: 0x001221A9 File Offset: 0x001203A9
		internal JavaScriptConverter GetConverter()
		{
			return new AutoCompleteBoxTextSettingsConverter();
		}

		// Token: 0x06005F0F RID: 24335 RVA: 0x001221B0 File Offset: 0x001203B0
		internal void Describe(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			JavaScriptConverter converter = this.GetConverter();
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				converter
			});
			IDictionary<string, object> dictionary = converter.Serialize(this, serializer);
			if (dictionary.Count > 0)
			{
				descriptor.AddProperty(propertyName, serializer.Serialize(this));
			}
		}
	}
}
