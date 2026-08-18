using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020009AE RID: 2478
	public class AutoCompleteBoxTokensSettings : ObjectWithState
	{
		// Token: 0x06005F03 RID: 24323 RVA: 0x00122059 File Offset: 0x00120259
		public AutoCompleteBoxTokensSettings(StateBag ownerViewState) : base("TokensSettings", ownerViewState)
		{
		}

		// Token: 0x17001F5B RID: 8027
		// (get) Token: 0x06005F04 RID: 24324 RVA: 0x00122067 File Offset: 0x00120267
		// (set) Token: 0x06005F05 RID: 24325 RVA: 0x00122088 File Offset: 0x00120288
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Allow token editing at client-side upon double click.")]
		public bool AllowTokenEditing
		{
			get
			{
				return (bool)(base.ViewState["AllowTokenEditing"] ?? false);
			}
			set
			{
				base.ViewState["AllowTokenEditing"] = value;
			}
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x001220A0 File Offset: 0x001202A0
		internal JavaScriptConverter GetConverter()
		{
			return new AutoCompleteBoxTokensSettingsConverter();
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x001220A8 File Offset: 0x001202A8
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
