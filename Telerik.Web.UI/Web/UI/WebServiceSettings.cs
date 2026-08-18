using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200034D RID: 845
	[ClientScriptResource("Telerik.Web.UI.WebServiceSettings", "Telerik.Web.UI.Common.Core.js")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class WebServiceSettings : ObjectWithState
	{
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x0005C256 File Offset: 0x0005A456
		// (set) Token: 0x06001CFB RID: 7419 RVA: 0x0005C276 File Offset: 0x0005A476
		[Description("Specifies the url of the web service to be used to populate items with ExpandMode set to WebService.")]
		[UrlProperty]
		[DefaultValue("")]
		[Category("Behavior")]
		public string Path
		{
			get
			{
				return (string)(base.ViewState["Path"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Path"] = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0005C289 File Offset: 0x0005A489
		// (set) Token: 0x06001CFD RID: 7421 RVA: 0x0005C2A9 File Offset: 0x0005A4A9
		[Description("Specifies the web service method name to be used to populate items with ExpandMode set to WebService.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public virtual string Method
		{
			get
			{
				return (string)(base.ViewState["Method"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Method"] = value;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x0005C2BC File Offset: 0x0005A4BC
		// (set) Token: 0x06001CFF RID: 7423 RVA: 0x0005C2DD File Offset: 0x0005A4DD
		[Category("Behavior")]
		[Description("")]
		[DefaultValue(false)]
		public virtual bool UseHttpGet
		{
			get
			{
				return (bool)(base.ViewState["UseHttpGet"] ?? false);
			}
			set
			{
				base.ViewState["UseHttpGet"] = value;
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0005C2F5 File Offset: 0x0005A4F5
		public WebServiceSettings(string prefix, StateBag viewState) : base(prefix, viewState)
		{
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0005C2FF File Offset: 0x0005A4FF
		public WebServiceSettings(StateBag viewState) : this("WebServiceSettings", viewState)
		{
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0005C30D File Offset: 0x0005A50D
		public WebServiceSettings() : this("WebServiceSettings", null)
		{
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0005C31B File Offset: 0x0005A51B
		internal virtual void Describe(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			this.DescribeWebServiceSettings(propertyName, serializer, descriptor);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0005C328 File Offset: 0x0005A528
		internal virtual void DescribeWebServiceSettings(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new WebServiceSettingsConverter()
			});
			WebServiceSettingsConverter webServiceSettingsConverter = new WebServiceSettingsConverter();
			IDictionary<string, object> dictionary = webServiceSettingsConverter.Serialize(this, serializer);
			if (dictionary.Count > 0)
			{
				descriptor.AddProperty(propertyName, serializer.Serialize(this));
			}
		}
	}
}
