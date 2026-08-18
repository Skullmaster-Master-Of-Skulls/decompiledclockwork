using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003D1 RID: 977
	public class Zoom : SerializableChartElement
	{
		// Token: 0x060023E2 RID: 9186 RVA: 0x00077868 File Offset: 0x00075A68
		public Zoom()
		{
			base.RegisterConverters(new List<JavaScriptConverter>
			{
				new ZoomConverter(),
				new MouseWheelZoomConverter(),
				new SelectionZoomConverter()
			});
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x000778A9 File Offset: 0x00075AA9
		// (set) Token: 0x060023E4 RID: 9188 RVA: 0x000778CA File Offset: 0x00075ACA
		[DefaultValue(false)]
		[Description("Specifies whether the zooming functionality is enabled.")]
		public bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? false);
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x000778E2 File Offset: 0x00075AE2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Specifies the zooming settings when the mouse wheel is used.")]
		[Category("Behavior")]
		public MouseWheelZoom MouseWheel
		{
			get
			{
				if (this._mouseWheelZoom == null)
				{
					this._mouseWheelZoom = new MouseWheelZoom();
				}
				return this._mouseWheelZoom;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x000778FD File Offset: 0x00075AFD
		[Description("Specifies the zooming settings when selection is used.")]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SelectionZoom Selection
		{
			get
			{
				if (this._selectionZoom == null)
				{
					this._selectionZoom = new SelectionZoom();
				}
				return this._selectionZoom;
			}
		}

		// Token: 0x04000958 RID: 2392
		private MouseWheelZoom _mouseWheelZoom;

		// Token: 0x04000959 RID: 2393
		private SelectionZoom _selectionZoom;
	}
}
