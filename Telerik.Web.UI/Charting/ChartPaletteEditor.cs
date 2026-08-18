using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200174B RID: 5963
	internal class ChartPaletteEditor : UITypeEditor, IDisposable
	{
		// Token: 0x0600E8C2 RID: 59586 RVA: 0x003442EA File Offset: 0x003424EA
		public ChartPaletteEditor()
		{
			this.columnsListing = new ListBox();
			this.columnsListing.SelectedIndexChanged += this.columnsListing_SelectedIndexChanged;
		}

		// Token: 0x170046C1 RID: 18113
		// (get) Token: 0x0600E8C3 RID: 59587 RVA: 0x00344314 File Offset: 0x00342514
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600E8C4 RID: 59588 RVA: 0x00344317 File Offset: 0x00342517
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x0600E8C5 RID: 59589 RVA: 0x0034431C File Offset: 0x0034251C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			object obj = null;
			Chart chart = null;
			if (provider != null)
			{
				this.editorService = (provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService);
			}
			if (this.editorService != null)
			{
				this.columnsListing.Items.Clear();
				this.columnsListing.Items.Add("(None)");
				for (int i = 0; i < PalettesCollection.Palettes.Count; i++)
				{
					this.columnsListing.Items.Add(PalettesCollection.Palettes[i]);
				}
				this.columnsListing.Size = this.columnsListing.PreferredSize;
				this.columnsListing.Height = this.columnsListing.PreferredHeight * 2 / 3;
				if (context.Instance is IChartComponent)
				{
					chart = ((IChartComponent)context.Instance).Chart;
				}
				else if (context.Instance is StylePlotArea)
				{
					chart = ((StylePlotArea)context.Instance).PlotArea.Chart;
				}
				if (chart != null)
				{
					for (int j = 0; j < chart.CustomPalettes.Count; j++)
					{
						if (!this.columnsListing.Items.Contains(chart.CustomPalettes[j].Name))
						{
							this.columnsListing.Items.Add(chart.CustomPalettes[j].Name);
						}
					}
					if (context.Instance is IChartComponent)
					{
						this.columnsListing.SelectedItem = chart.SeriesPaletteWrapper;
					}
					else if (context.Instance is StylePlotArea)
					{
						this.columnsListing.SelectedItem = ((StylePlotArea)context.Instance).PlotArea.Appearance.SeriesPalette;
					}
				}
				this.editorService.DropDownControl(this.columnsListing);
				if (this.columnsListing.SelectedItem != null)
				{
					obj = this.columnsListing.SelectedItem.ToString();
				}
			}
			if (obj != value)
			{
				value = obj;
			}
			return value;
		}

		// Token: 0x0600E8C6 RID: 59590 RVA: 0x0034450A File Offset: 0x0034270A
		public void columnsListing_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.editorService != null)
			{
				this.editorService.CloseDropDown();
			}
		}

		// Token: 0x0600E8C7 RID: 59591 RVA: 0x0034451F File Offset: 0x0034271F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600E8C8 RID: 59592 RVA: 0x0034452E File Offset: 0x0034272E
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.columnsListing.Dispose();
			}
		}

		// Token: 0x040042F1 RID: 17137
		private IWindowsFormsEditorService editorService;

		// Token: 0x040042F2 RID: 17138
		public ListBox columnsListing;
	}
}
