using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020000D1 RID: 209
	[ToolboxItem(false)]
	public class WaiAriaSettings : StateManager, IDefaultCheck
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x0001E25C File Offset: 0x0001C45C
		public WaiAriaSettings() : this(new JavaScriptConverter[]
		{
			new WaiAriaSettingsConverter()
		})
		{
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001E27F File Offset: 0x0001C47F
		public WaiAriaSettings(JavaScriptConverter[] converters)
		{
			this._serializer = new JavaScriptSerializer();
			this._serializer.RegisterConverters(converters);
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0001E29E File Offset: 0x0001C49E
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x0001E2BE File Offset: 0x0001C4BE
		[Description("Gets or sets the ID of the html element containing the description of the control.")]
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Appearance")]
		public string DescribedBy
		{
			get
			{
				return ((string)base.ViewState["DescribedBy"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["DescribedBy"] = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0001E2D1 File Offset: 0x0001C4D1
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x0001E2F1 File Offset: 0x0001C4F1
		[Description("Gets or sets the text added as aria-label attribute.")]
		[Localizable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		public string Label
		{
			get
			{
				return ((string)base.ViewState["Label"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Label"] = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0001E304 File Offset: 0x0001C504
		public virtual bool IsDefault
		{
			get
			{
				return string.IsNullOrEmpty(this.DescribedBy) && string.IsNullOrEmpty(this.Label);
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001E320 File Offset: 0x0001C520
		internal void Describe(IScriptDescriptor descriptor)
		{
			if (!this.IsDefault)
			{
				descriptor.AddProperty("_ariaSettings", this._serializer.Serialize(this));
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001E341 File Offset: 0x0001C541
		internal void Describe(IScriptDescriptor descriptor, string clientProperty)
		{
			if (!this.IsDefault)
			{
				descriptor.AddProperty(clientProperty, this._serializer.Serialize(this));
			}
		}

		// Token: 0x040001E6 RID: 486
		private JavaScriptSerializer _serializer;
	}
}
