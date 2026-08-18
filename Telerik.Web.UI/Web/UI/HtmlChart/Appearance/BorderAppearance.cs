using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart.JavaScriptSerializers;

namespace Telerik.Web.UI.HtmlChart.Appearance
{
	// Token: 0x0200004C RID: 76
	public class BorderAppearance : StateManager
	{
		// Token: 0x0600025F RID: 607 RVA: 0x00006801 File Offset: 0x00004A01
		public BorderAppearance()
		{
			this.InitSerializer();
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000680F File Offset: 0x00004A0F
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00006834 File Offset: 0x00004A34
		[TypeConverter(typeof(ColorConverter))]
		[DefaultValue(typeof(Color), "")]
		public Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000684C File Offset: 0x00004A4C
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00006871 File Offset: 0x00004A71
		[TypeConverter(typeof(UnitConverter))]
		[DefaultValue(typeof(Unit), "")]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00006889 File Offset: 0x00004A89
		internal virtual string Serialize()
		{
			return string.Format("{0}", this._serializer.Serialize(this));
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000068A4 File Offset: 0x00004AA4
		private void InitSerializer()
		{
			this._serializer = new JavaScriptSerializer();
			this._serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new BorderAppearanceConverter()
			});
		}

		// Token: 0x04000052 RID: 82
		private JavaScriptSerializer _serializer;
	}
}
