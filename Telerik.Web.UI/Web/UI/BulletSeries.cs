using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters.Bullet;
using Telerik.Web.UI.HtmlChart.PlotArea;
using Telerik.Web.UI.HtmlChart.PlotArea.Appearance;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;
using Telerik.Web.UI.HtmlChart.SeriesItemCollections;

namespace Telerik.Web.UI
{
	// Token: 0x020003CA RID: 970
	public class BulletSeries : SeriesBase, IJsConvertable
	{
		// Token: 0x060023A0 RID: 9120 RVA: 0x00076BFE File Offset: 0x00074DFE
		public BulletSeries()
		{
			this.sType = SeriesType.Bullet;
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x00076C10 File Offset: 0x00074E10
		internal override bool IsDataBound
		{
			get
			{
				string empty = string.Empty;
				return this.SeriesItems.Count == 0 && (this.DataCurrentField != empty || this.DataTargetField != empty || base.ColorField != empty || base.Data != empty);
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x00076C6A File Offset: 0x00074E6A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public BulletSeriesItemCollection SeriesItems
		{
			get
			{
				if (this._seriesItems == null)
				{
					this._seriesItems = new BulletSeriesItemCollection();
				}
				return this._seriesItems;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x00076C85 File Offset: 0x00074E85
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public BulletTargetAppearance Target
		{
			get
			{
				if (this._target == null)
				{
					this._target = new BulletTargetAppearance();
				}
				return this._target;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x00076CA0 File Offset: 0x00074EA0
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x00076CB2 File Offset: 0x00074EB2
		[DefaultValue("")]
		public string DataCurrentField
		{
			get
			{
				return base.GetViewStateValue<string>("DataCurrentField", string.Empty);
			}
			set
			{
				base.ViewState["DataCurrentField"] = value;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x00076CC5 File Offset: 0x00074EC5
		// (set) Token: 0x060023A7 RID: 9127 RVA: 0x00076CD7 File Offset: 0x00074ED7
		[DefaultValue("")]
		public string DataTargetField
		{
			get
			{
				return base.GetViewStateValue<string>("DataTargetField", string.Empty);
			}
			set
			{
				base.ViewState["DataTargetField"] = value;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x00076CEA File Offset: 0x00074EEA
		// (set) Token: 0x060023A9 RID: 9129 RVA: 0x00076CF2 File Offset: 0x00074EF2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new string DataFieldY
		{
			get
			{
				return base.DataFieldY;
			}
			set
			{
				base.DataFieldY = value;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x00076CFB File Offset: 0x00074EFB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new SeriesItemCollection Items
		{
			get
			{
				throw new NotSupportedException("This series type does not support the obsolete Items collection. Please, use the SeriesItems collection intead.");
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x00076D08 File Offset: 0x00074F08
		internal override string Serialize()
		{
			AdvancedJavaScriptSerializer advancedJavaScriptSerializer = new AdvancedJavaScriptSerializer();
			this.RegisterJSConverters(advancedJavaScriptSerializer);
			return advancedJavaScriptSerializer.Serialize(this);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x00076D2C File Offset: 0x00074F2C
		public void RegisterJSConverters(JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new BulletSeriesConverter(),
				new BulletSeriesItemConverter()
			});
			base.Appearance.RegisterJSConverters(serializer);
			this.Target.RegisterJSConverters(serializer);
			base.TooltipsAppearance.RegisterJSConverters(serializer);
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x00076D7C File Offset: 0x00074F7C
		protected override void LoadViewState(object state)
		{
			int num = 0;
			object[] array = (object[])state;
			base.LoadViewState(array[num++]);
			((IStateManager)this.SeriesItems).LoadViewState(array[num++]);
			((IStateManager)this.Target).LoadViewState(array[num++]);
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x00076DC4 File Offset: 0x00074FC4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState(),
				((IStateManager)this.Target).SaveViewState()
			};
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00076E00 File Offset: 0x00075000
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
			((IStateManager)this.Target).TrackViewState();
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x00076E1E File Offset: 0x0007501E
		internal override void SetDirty()
		{
			base.SetDirty();
			this.SeriesItems.SetDirty();
			this.Target.SetDirty();
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00076E3C File Offset: 0x0007503C
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			BulletSeriesItem bulletSeriesItem = (BulletSeriesItem)seriesItem;
			if (bulletSeriesItem != null)
			{
				this.SeriesItems.Add(bulletSeriesItem);
			}
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00076E5F File Offset: 0x0007505F
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00076E6C File Offset: 0x0007506C
		internal override SeriesItemBase GetSeriesItem()
		{
			return new BulletSeriesItem();
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x00077014 File Offset: 0x00075214
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				BulletSeriesItem item = (BulletSeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x0400094E RID: 2382
		private BulletSeriesItemCollection _seriesItems;

		// Token: 0x0400094F RID: 2383
		private BulletTargetAppearance _target;
	}
}
