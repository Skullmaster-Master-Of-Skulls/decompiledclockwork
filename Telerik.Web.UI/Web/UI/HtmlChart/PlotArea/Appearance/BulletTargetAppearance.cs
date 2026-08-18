using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters.Bullet;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Appearance
{
	// Token: 0x020003C4 RID: 964
	public class BulletTargetAppearance : StateManager, IJsConvertable, IDefaultCheck
	{
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x00076236 File Offset: 0x00074436
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public DashedBorderAppearance Border
		{
			get
			{
				if (this._border == null)
				{
					this._border = new DashedBorderAppearance();
				}
				return this._border;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x00076251 File Offset: 0x00074451
		// (set) Token: 0x06002351 RID: 9041 RVA: 0x00076263 File Offset: 0x00074463
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(ColorConverter))]
		public Color Color
		{
			get
			{
				return base.GetViewStateValue<Color>("Color", Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x0007627B File Offset: 0x0007447B
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TargetLineAppearance Line
		{
			get
			{
				if (this._line == null)
				{
					this._line = new TargetLineAppearance();
				}
				return this._line;
			}
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x00076298 File Offset: 0x00074498
		public void RegisterJSConverters(JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new BulletTargetConverter[]
			{
				new BulletTargetConverter()
			});
			this.Border.RegisterJSConverters(serializer);
			this.Line.RegisterJSConverters(serializer);
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000762D4 File Offset: 0x000744D4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Border).SaveViewState(),
				((IStateManager)this.Line).SaveViewState()
			};
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x00076310 File Offset: 0x00074510
		protected override void LoadViewState(object state)
		{
			int num = 0;
			object[] array = (object[])state;
			base.LoadViewState(array[num++]);
			((IStateManager)this.Border).LoadViewState(array[num++]);
			((IStateManager)this.Line).LoadViewState(array[num++]);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x00076357 File Offset: 0x00074557
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Border).TrackViewState();
			((IStateManager)this.Line).TrackViewState();
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x00076375 File Offset: 0x00074575
		internal override void SetDirty()
		{
			base.SetDirty();
			this.Border.SetDirty();
			this.Line.SetDirty();
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x00076393 File Offset: 0x00074593
		public bool IsDefault
		{
			get
			{
				return this.Border.IsDefault && this.Color == Color.Empty && this.Line.IsDefault;
			}
		}

		// Token: 0x04000947 RID: 2375
		private DashedBorderAppearance _border;

		// Token: 0x04000948 RID: 2376
		private TargetLineAppearance _line;
	}
}
