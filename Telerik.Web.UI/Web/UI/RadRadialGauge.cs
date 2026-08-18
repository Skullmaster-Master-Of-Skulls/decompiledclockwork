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
	// Token: 0x02000B68 RID: 2920
	[ToolboxData("<{0}:RadRadialGauge runat=\"server\"></{0}:RadRadialGauge>")]
	[ToolboxBitmap(typeof(RadRadialGauge), "Telerik.Web.UI.Gauge.png")]
	[Description("Telerik RadialGauge control for data visualization.")]
	[Designer("Telerik.Web.Design.RadRadialGaugeDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Visualization")]
	[ParseChildren(ChildrenAsProperties = true)]
	[EmbeddedSkin("RadialGauge")]
	[ClientScriptResource("Telerik.Web.UI.RadRadialGauge", "Telerik.Web.UI.Gauge.Scripts.RadGaugeControl.js")]
	[EmbeddedSkin("RadialGauge", "Default")]
	public class RadRadialGauge : RadGaugeControl<RadialPointer, RadialScale>
	{
		// Token: 0x17002422 RID: 9250
		// (get) Token: 0x06006E2F RID: 28207 RVA: 0x00198CFD File Offset: 0x00196EFD
		[Description("Defines the Pointer settings of the RadialGauge.")]
		[Browsable(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public override RadialPointer Pointer
		{
			get
			{
				if (this._pointer == null)
				{
					this._pointer = new RadialPointer();
				}
				return this._pointer;
			}
		}

		// Token: 0x17002423 RID: 9251
		// (get) Token: 0x06006E30 RID: 28208 RVA: 0x00198D18 File Offset: 0x00196F18
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadialPointersCollection Pointers
		{
			get
			{
				if (this._pointers == null)
				{
					this._pointers = new RadialPointersCollection();
				}
				return this._pointers;
			}
		}

		// Token: 0x17002424 RID: 9252
		// (get) Token: 0x06006E31 RID: 28209 RVA: 0x00198D33 File Offset: 0x00196F33
		[Category("Behavior")]
		[Description("Defines the Scale settings of the RadialGauge.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public override RadialScale Scale
		{
			get
			{
				if (this._scale == null)
				{
					this._scale = new RadialScale();
				}
				return this._scale;
			}
		}

		// Token: 0x06006E32 RID: 28210 RVA: 0x00198D50 File Offset: 0x00196F50
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

		// Token: 0x04001DC7 RID: 7623
		private RadialPointer _pointer;

		// Token: 0x04001DC8 RID: 7624
		private RadialPointersCollection _pointers;

		// Token: 0x04001DC9 RID: 7625
		private RadialScale _scale;
	}
}
