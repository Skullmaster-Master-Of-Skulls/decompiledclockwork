using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.Design;
using Telerik.Web.UI.Map;

namespace Telerik.Web.UI
{
	// Token: 0x0200059E RID: 1438
	public class MapLayer : StateManager
	{
		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000AB793 File Offset: 0x000A9993
		// (set) Token: 0x06003384 RID: 13188 RVA: 0x000AB7B3 File Offset: 0x000A99B3
		[DefaultValue("")]
		public string Attribution
		{
			get
			{
				return (string)(base.ViewState["Attribution"] ?? "");
			}
			set
			{
				base.ViewState["Attribution"] = value;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000AB7C6 File Offset: 0x000A99C6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Extent ExtentSettings
		{
			get
			{
				if (this._extent == null)
				{
					this._extent = new Extent();
				}
				return this._extent;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000AB7E1 File Offset: 0x000A99E1
		// (set) Token: 0x06003387 RID: 13191 RVA: 0x000AB801 File Offset: 0x000A9A01
		[DefaultValue("")]
		public string Key
		{
			get
			{
				return (string)(base.ViewState["Key"] ?? "");
			}
			set
			{
				base.ViewState["Key"] = value;
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x000AB814 File Offset: 0x000A9A14
		// (set) Token: 0x06003389 RID: 13193 RVA: 0x000AB834 File Offset: 0x000A9A34
		[DefaultValue("road")]
		public string ImagerySet
		{
			get
			{
				return (string)(base.ViewState["ImagerySet"] ?? "road");
			}
			set
			{
				base.ViewState["ImagerySet"] = value;
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000AB847 File Offset: 0x000A9A47
		// (set) Token: 0x0600338B RID: 13195 RVA: 0x000AB867 File Offset: 0x000A9A67
		[DefaultValue("en-US")]
		public string Culture
		{
			get
			{
				return (string)(base.ViewState["Culture"] ?? "en-US");
			}
			set
			{
				base.ViewState["Culture"] = value;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000AB87A File Offset: 0x000A9A7A
		// (set) Token: 0x0600338D RID: 13197 RVA: 0x000AB89A File Offset: 0x000A9A9A
		[DefaultValue("location")]
		public string LocationField
		{
			get
			{
				return (string)(base.ViewState["LocationField"] ?? "location");
			}
			set
			{
				base.ViewState["LocationField"] = value;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x000AB8AD File Offset: 0x000A9AAD
		// (set) Token: 0x0600338F RID: 13199 RVA: 0x000AB8CD File Offset: 0x000A9ACD
		[DefaultValue("pinTarget")]
		[TypeConverter(typeof(MarkerShapeStringConverter))]
		public string Shape
		{
			get
			{
				return (string)(base.ViewState["Shape"] ?? "pinTarget");
			}
			set
			{
				base.ViewState["Shape"] = value;
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x000AB8E0 File Offset: 0x000A9AE0
		// (set) Token: 0x06003391 RID: 13201 RVA: 0x000AB909 File Offset: 0x000A9B09
		[DefaultValue(256.0)]
		public double TileSize
		{
			get
			{
				return (double)(base.ViewState["TileSize"] ?? 256.0);
			}
			set
			{
				base.ViewState["TileSize"] = value;
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000AB921 File Offset: 0x000A9B21
		// (set) Token: 0x06003393 RID: 13203 RVA: 0x000AB941 File Offset: 0x000A9B41
		[DefaultValue("title")]
		public string TitleField
		{
			get
			{
				return (string)(base.ViewState["TitleField"] ?? "title");
			}
			set
			{
				base.ViewState["TitleField"] = value;
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000AB954 File Offset: 0x000A9B54
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Tooltip TooltipSettings
		{
			get
			{
				if (this._tooltip == null)
				{
					this._tooltip = new Tooltip();
				}
				return this._tooltip;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06003395 RID: 13205 RVA: 0x000AB96F File Offset: 0x000A9B6F
		// (set) Token: 0x06003396 RID: 13206 RVA: 0x000AB998 File Offset: 0x000A9B98
		[DefaultValue(100.0)]
		public double MaxSize
		{
			get
			{
				return (double)(base.ViewState["MaxSize"] ?? 100.0);
			}
			set
			{
				base.ViewState["MaxSize"] = value;
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x000AB9B0 File Offset: 0x000A9BB0
		// (set) Token: 0x06003398 RID: 13208 RVA: 0x000AB9D9 File Offset: 0x000A9BD9
		[DefaultValue(0.0)]
		public double MinSize
		{
			get
			{
				return (double)(base.ViewState["MinSize"] ?? 0.0);
			}
			set
			{
				base.ViewState["MinSize"] = value;
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x000AB9F1 File Offset: 0x000A9BF1
		// (set) Token: 0x0600339A RID: 13210 RVA: 0x000ABA1A File Offset: 0x000A9C1A
		[DefaultValue(1.0)]
		public double Opacity
		{
			get
			{
				return (double)(base.ViewState["Opacity"] ?? 1.0);
			}
			set
			{
				base.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000ABA32 File Offset: 0x000A9C32
		// (set) Token: 0x0600339C RID: 13212 RVA: 0x000ABA4E File Offset: 0x000A9C4E
		[TypeConverter(typeof(ListConverter))]
		[DefaultValue(null)]
		public string[] Subdomains
		{
			get
			{
				return (string[])(base.ViewState["Subdomains"] ?? null);
			}
			set
			{
				base.ViewState["Subdomains"] = value;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000ABA61 File Offset: 0x000A9C61
		// (set) Token: 0x0600339E RID: 13214 RVA: 0x000ABA81 File Offset: 0x000A9C81
		[DefaultValue("circle")]
		public string Symbol
		{
			get
			{
				return (string)(base.ViewState["Symbol"] ?? "circle");
			}
			set
			{
				base.ViewState["Symbol"] = value;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000ABA94 File Offset: 0x000A9C94
		// (set) Token: 0x060033A0 RID: 13216 RVA: 0x000ABAB5 File Offset: 0x000A9CB5
		[DefaultValue(LayerType.Tile)]
		public LayerType Type
		{
			get
			{
				return (LayerType)(base.ViewState["Type"] ?? LayerType.Tile);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x060033A1 RID: 13217 RVA: 0x000ABACD File Offset: 0x000A9CCD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style StyleSettings
		{
			get
			{
				if (this._style == null)
				{
					this._style = new Style();
				}
				return this._style;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000ABAE8 File Offset: 0x000A9CE8
		// (set) Token: 0x060033A3 RID: 13219 RVA: 0x000ABB08 File Offset: 0x000A9D08
		[DefaultValue("")]
		public string UrlTemplate
		{
			get
			{
				return (string)(base.ViewState["UrlTemplate"] ?? "");
			}
			set
			{
				base.ViewState["UrlTemplate"] = value;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x000ABB1B File Offset: 0x000A9D1B
		// (set) Token: 0x060033A5 RID: 13221 RVA: 0x000ABB3B File Offset: 0x000A9D3B
		[DefaultValue("value")]
		public string ValueField
		{
			get
			{
				return (string)(base.ViewState["ValueField"] ?? "value");
			}
			set
			{
				base.ViewState["ValueField"] = value;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x000ABB4E File Offset: 0x000A9D4E
		// (set) Token: 0x060033A7 RID: 13223 RVA: 0x000ABB77 File Offset: 0x000A9D77
		[DefaultValue(0.0)]
		public double ZIndex
		{
			get
			{
				return (double)(base.ViewState["ZIndex"] ?? 0.0);
			}
			set
			{
				base.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x000ABB8F File Offset: 0x000A9D8F
		// (set) Token: 0x060033A9 RID: 13225 RVA: 0x000ABBB8 File Offset: 0x000A9DB8
		[DefaultValue(0.0)]
		public double MinZoom
		{
			get
			{
				return (double)(base.ViewState["MinZoom"] ?? 0.0);
			}
			set
			{
				base.ViewState["MinZoom"] = value;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x000ABBD0 File Offset: 0x000A9DD0
		// (set) Token: 0x060033AB RID: 13227 RVA: 0x000ABBF9 File Offset: 0x000A9DF9
		[DefaultValue(0.0)]
		public double MaxZoom
		{
			get
			{
				return (double)(base.ViewState["MaxZoom"] ?? 0.0);
			}
			set
			{
				base.ViewState["MaxZoom"] = value;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000ABC11 File Offset: 0x000A9E11
		// (set) Token: 0x060033AD RID: 13229 RVA: 0x000ABC31 File Offset: 0x000A9E31
		[DefaultValue("")]
		public string ClientDataSourceID
		{
			get
			{
				return (string)(base.ViewState["ClientDataSourceID"] ?? "");
			}
			set
			{
				base.ViewState["ClientDataSourceID"] = value;
			}
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000ABC44 File Offset: 0x000A9E44
		internal override void SetDirty()
		{
			base.SetDirty();
			this.ExtentSettings.SetDirty();
			this.StyleSettings.SetDirty();
			this.TooltipSettings.SetDirty();
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000ABC70 File Offset: 0x000A9E70
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ExtentSettings).LoadViewState(array[num++]);
			((IStateManager)this.StyleSettings).LoadViewState(array[num++]);
			((IStateManager)this.TooltipSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000ABCCC File Offset: 0x000A9ECC
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ExtentSettings).SaveViewState(),
				((IStateManager)this.StyleSettings).SaveViewState(),
				((IStateManager)this.TooltipSettings).SaveViewState()
			};
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000ABD16 File Offset: 0x000A9F16
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ExtentSettings).TrackViewState();
			((IStateManager)this.StyleSettings).TrackViewState();
			((IStateManager)this.TooltipSettings).TrackViewState();
		}

		// Token: 0x04000E1C RID: 3612
		private Extent _extent;

		// Token: 0x04000E1D RID: 3613
		private Tooltip _tooltip;

		// Token: 0x04000E1E RID: 3614
		private Style _style;
	}
}
