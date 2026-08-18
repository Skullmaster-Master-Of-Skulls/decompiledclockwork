using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x020017AF RID: 6063
	[ParseChildren(true)]
	[PersistChildren(false)]
	[DefaultProperty("Items")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class Palette : StateManagedObject, ICloneable
	{
		// Token: 0x17004773 RID: 18291
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public PaletteItem this[int index]
		{
			get
			{
				return this.paletteItems[index];
			}
			set
			{
				this.paletteItems[index] = value;
			}
		}

		// Token: 0x17004774 RID: 18292
		// (get) Token: 0x0600EC12 RID: 60434 RVA: 0x0035AC7B File Offset: 0x00358E7B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Description("Items collection.")]
		[Editor(typeof(PaletteItemsCollectionEditor), typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[SkinnableProperty]
		public PaletteItemsCollection Items
		{
			get
			{
				return this.paletteItems;
			}
		}

		// Token: 0x17004775 RID: 18293
		// (get) Token: 0x0600EC13 RID: 60435 RVA: 0x0035AC83 File Offset: 0x00358E83
		// (set) Token: 0x0600EC14 RID: 60436 RVA: 0x0035ACA3 File Offset: 0x00358EA3
		[DefaultValue("Palette")]
		[NotifyParentProperty(true)]
		[Description("Specifies the palette name.")]
		[SkinnableProperty]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? "Palette");
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x0600EC15 RID: 60437 RVA: 0x0035ACB6 File Offset: 0x00358EB6
		public Palette()
		{
			this.paletteItems = new PaletteItemsCollection();
		}

		// Token: 0x0600EC16 RID: 60438 RVA: 0x0035ACC9 File Offset: 0x00358EC9
		public Palette(string name) : this()
		{
			this.Name = name;
		}

		// Token: 0x0600EC17 RID: 60439 RVA: 0x0035ACD8 File Offset: 0x00358ED8
		public Palette(string name, Color[] mainColors, Color[] secondColors) : this(name)
		{
			this.FillItemsCollectionFromTwoArrays(mainColors, secondColors);
		}

		// Token: 0x0600EC18 RID: 60440 RVA: 0x0035ACEC File Offset: 0x00358EEC
		public Palette(string name, ColorBlend[] addtionalColors) : this(name)
		{
			for (int i = 0; i < addtionalColors.Length; i++)
			{
				this.paletteItems.Add(new PaletteItem(addtionalColors[i]));
			}
		}

		// Token: 0x0600EC19 RID: 60441 RVA: 0x0035AD24 File Offset: 0x00358F24
		public Palette(string name, Color[] colors, bool twoColors) : this(name)
		{
			if (twoColors)
			{
				this.FillItemsCollectionFromTwoArrays(colors, colors);
				return;
			}
			for (int i = 0; i < colors.Length; i++)
			{
				this.paletteItems.Add(new PaletteItem(colors[i], Color.Empty));
			}
		}

		// Token: 0x0600EC1A RID: 60442 RVA: 0x0035AD74 File Offset: 0x00358F74
		private void FillItemsCollectionFromTwoArrays(Color[] mainColors, Color[] secondColors)
		{
			int num = mainColors.Length;
			int num2 = secondColors.Length;
			if (num == num2)
			{
				for (int i = 0; i < num; i++)
				{
					this.paletteItems.Add(new PaletteItem(mainColors[i], secondColors[i]));
				}
				return;
			}
			if (num < num2)
			{
				for (int j = 0; j < num2; j++)
				{
					if (j < num)
					{
						this.paletteItems.Add(new PaletteItem(mainColors[j], secondColors[j]));
					}
					else
					{
						this.paletteItems.Add(new PaletteItem(Color.Empty, secondColors[j]));
					}
				}
				return;
			}
			for (int k = 0; k < num; k++)
			{
				if (k < num2)
				{
					this.paletteItems.Add(new PaletteItem(mainColors[k], secondColors[k]));
				}
				else
				{
					this.paletteItems.Add(new PaletteItem(mainColors[k], Color.Empty));
				}
			}
		}

		// Token: 0x0600EC1B RID: 60443 RVA: 0x0035AE85 File Offset: 0x00359085
		internal PaletteItem GetPaletteItem(int index)
		{
			if (this.paletteItems.Count > 0)
			{
				index %= this.paletteItems.Count;
				return this.paletteItems[index];
			}
			throw new ChartException("Selected palette have no items.");
		}

		// Token: 0x0600EC1C RID: 60444 RVA: 0x0035AEBD File Offset: 0x003590BD
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600EC1D RID: 60445 RVA: 0x0035AEC8 File Offset: 0x003590C8
		public object Clone()
		{
			Palette palette = (Palette)base.MemberwiseClone();
			palette.ViewState = base.CloneState();
			palette.paletteItems = new PaletteItemsCollection();
			foreach (PaletteItem paletteItem in this.paletteItems)
			{
				PaletteItem item = (PaletteItem)paletteItem.Clone();
				palette.paletteItems.Add(item);
			}
			return palette;
		}

		// Token: 0x0600EC1E RID: 60446 RVA: 0x0035AF4C File Offset: 0x0035914C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.paletteItems).TrackViewState();
		}

		// Token: 0x0600EC1F RID: 60447 RVA: 0x0035AF60 File Offset: 0x00359160
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.paletteItems).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600EC20 RID: 60448 RVA: 0x0035AF90 File Offset: 0x00359190
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.paletteItems).SaveViewState()
			}.ToArray();
		}

		// Token: 0x04004424 RID: 17444
		private PaletteItemsCollection paletteItems;
	}
}
