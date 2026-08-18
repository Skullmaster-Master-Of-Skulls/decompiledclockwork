using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Gauge;

namespace Telerik.Web.UI
{
	// Token: 0x02000B61 RID: 2913
	[TelerikToolboxCategory("Visualization")]
	[EmbeddedSkin("LinearGauge", "Default")]
	[Description("Telerik LinearGauge control for data visualization.")]
	[ToolboxData("<{0}:RadLinearGauge runat=\"server\"></{0}:RadLinearGauge>")]
	[ToolboxBitmap(typeof(RadLinearGauge), "Telerik.Web.UI.Gauge.png")]
	[ClientScriptResource("Telerik.Web.UI.RadLinearGauge", "Telerik.Web.UI.Gauge.Scripts.RadGaugeControl.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("LinearGauge")]
	[Designer("Telerik.Web.Design.RadLinearGaugeDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RadLinearGauge : RadGaugeControl<LinearPointer, LinearScale>
	{
		// Token: 0x17002411 RID: 9233
		// (get) Token: 0x06006E05 RID: 28165 RVA: 0x00198810 File Offset: 0x00196A10
		[Browsable(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Defines the Pointer settings of the LinearGauge.")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public override LinearPointer Pointer
		{
			get
			{
				if (this._pointer == null)
				{
					this._pointer = new LinearPointer();
				}
				return this._pointer;
			}
		}

		// Token: 0x17002412 RID: 9234
		// (get) Token: 0x06006E06 RID: 28166 RVA: 0x0019882B File Offset: 0x00196A2B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public LinearPointersCollection Pointers
		{
			get
			{
				if (this._pointers == null)
				{
					this._pointers = new LinearPointersCollection();
				}
				return this._pointers;
			}
		}

		// Token: 0x17002413 RID: 9235
		// (get) Token: 0x06006E07 RID: 28167 RVA: 0x00198846 File Offset: 0x00196A46
		[Category("Behavior")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Defines the Scale settings of the LinearGauge.")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		public override LinearScale Scale
		{
			get
			{
				if (this._scale == null)
				{
					this._scale = new LinearScale();
				}
				return this._scale;
			}
		}

		// Token: 0x06006E08 RID: 28168 RVA: 0x00198864 File Offset: 0x00196A64
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			List<JavaScriptConverter> list = new List<JavaScriptConverter>();
			list.Add(new GaugeTypesConverter());
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(list);
			descriptor.AddScriptProperty("scaleData", javaScriptSerializer.Serialize(this.Scale));
			descriptor.AddScriptProperty("pointerData", javaScriptSerializer.Serialize(this.Pointer));
			if (this.Pointers.Count > 0)
			{
				descriptor.AddScriptProperty("pointers", javaScriptSerializer.Serialize(this.Pointers));
			}
			descriptor.AddScriptProperty("appearanceData", javaScriptSerializer.Serialize(base.Appearance));
		}

		// Token: 0x04001DBF RID: 7615
		private LinearPointer _pointer;

		// Token: 0x04001DC0 RID: 7616
		private LinearPointersCollection _pointers;

		// Token: 0x04001DC1 RID: 7617
		private LinearScale _scale;
	}
}
