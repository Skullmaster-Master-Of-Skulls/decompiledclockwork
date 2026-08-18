using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x020017B0 RID: 6064
	[Description("A palette item.")]
	[DefaultProperty("Name")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PaletteItem : StateManagedObject, ICloneable
	{
		// Token: 0x17004776 RID: 18294
		// (get) Token: 0x0600EC21 RID: 60449 RVA: 0x0035AFC8 File Offset: 0x003591C8
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[Editor(typeof(ColorBlendEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ColorBlend AdditionalColors
		{
			get
			{
				return this.paletteItemAdditionalColors;
			}
		}

		// Token: 0x17004777 RID: 18295
		// (get) Token: 0x0600EC22 RID: 60450 RVA: 0x0035AFD0 File Offset: 0x003591D0
		// (set) Token: 0x0600EC23 RID: 60451 RVA: 0x0035AFF5 File Offset: 0x003591F5
		[TypeConverter(typeof(ColorConverter))]
		[DefaultValue(typeof(Color), "")]
		[SkinnableProperty]
		public Color MainColor
		{
			get
			{
				return (Color)(base.ViewState["MainColor"] ?? DefaultValues.DEFAULT_STYLE_COLOR);
			}
			set
			{
				base.ViewState["MainColor"] = value;
			}
		}

		// Token: 0x17004778 RID: 18296
		// (get) Token: 0x0600EC24 RID: 60452 RVA: 0x0035B00D File Offset: 0x0035920D
		// (set) Token: 0x0600EC25 RID: 60453 RVA: 0x0035B032 File Offset: 0x00359232
		[TypeConverter(typeof(ColorConverter))]
		[SkinnableProperty]
		[DefaultValue(typeof(Color), "")]
		public Color SecondColor
		{
			get
			{
				return (Color)(base.ViewState["SecondColor"] ?? DefaultValues.DEFAULT_STYLE_COLOR);
			}
			set
			{
				base.ViewState["SecondColor"] = value;
			}
		}

		// Token: 0x17004779 RID: 18297
		// (get) Token: 0x0600EC26 RID: 60454 RVA: 0x0035B04A File Offset: 0x0035924A
		// (set) Token: 0x0600EC27 RID: 60455 RVA: 0x0035B06A File Offset: 0x0035926A
		[DefaultValue("PaletteItem")]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? "PaletteItem");
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x0600EC28 RID: 60456 RVA: 0x0035B07D File Offset: 0x0035927D
		public PaletteItem()
		{
			this.paletteItemAdditionalColors = new ColorBlend();
		}

		// Token: 0x0600EC29 RID: 60457 RVA: 0x0035B090 File Offset: 0x00359290
		public PaletteItem(ColorBlend additionalColors) : this()
		{
			this.paletteItemAdditionalColors = additionalColors;
			this.MainColor = additionalColors.GetColors()[0];
			this.SecondColor = additionalColors.GetColors()[additionalColors.Count - 1];
		}

		// Token: 0x0600EC2A RID: 60458 RVA: 0x0035B0DF File Offset: 0x003592DF
		public PaletteItem(string name, ColorBlend additionalColors) : this(additionalColors)
		{
			this.Name = name;
		}

		// Token: 0x0600EC2B RID: 60459 RVA: 0x0035B0EF File Offset: 0x003592EF
		public PaletteItem(string name, Color mainColor, Color secondColor) : this(mainColor, secondColor)
		{
			this.Name = name;
		}

		// Token: 0x0600EC2C RID: 60460 RVA: 0x0035B100 File Offset: 0x00359300
		public PaletteItem(Color mainColor, Color secondColor) : this()
		{
			this.MainColor = mainColor;
			this.SecondColor = secondColor;
		}

		// Token: 0x0600EC2D RID: 60461 RVA: 0x0035B116 File Offset: 0x00359316
		internal void Reset()
		{
			this.MainColor = DefaultValues.DEFAULT_STYLE_COLOR;
			this.SecondColor = DefaultValues.DEFAULT_STYLE_COLOR;
			this.paletteItemAdditionalColors = new ColorBlend();
		}

		// Token: 0x0600EC2E RID: 60462 RVA: 0x0035B139 File Offset: 0x00359339
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600EC2F RID: 60463 RVA: 0x0035B141 File Offset: 0x00359341
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.paletteItemAdditionalColors).TrackViewState();
		}

		// Token: 0x0600EC30 RID: 60464 RVA: 0x0035B154 File Offset: 0x00359354
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.paletteItemAdditionalColors).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600EC31 RID: 60465 RVA: 0x0035B184 File Offset: 0x00359384
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.paletteItemAdditionalColors).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600EC32 RID: 60466 RVA: 0x0035B1BC File Offset: 0x003593BC
		public object Clone()
		{
			PaletteItem paletteItem = (PaletteItem)base.MemberwiseClone();
			paletteItem.ViewState = base.CloneState();
			paletteItem.paletteItemAdditionalColors = (ColorBlend)this.paletteItemAdditionalColors.Clone();
			return paletteItem;
		}

		// Token: 0x04004425 RID: 17445
		private ColorBlend paletteItemAdditionalColors;
	}
}
