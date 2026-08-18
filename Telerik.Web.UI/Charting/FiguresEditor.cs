using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001754 RID: 5972
	internal class FiguresEditor : UITypeEditor, IDisposable
	{
		// Token: 0x0600E8E6 RID: 59622 RVA: 0x00344C8B File Offset: 0x00342E8B
		public FiguresEditor()
		{
			this.columnsListing = new ListBox();
			this.columnsListing.SelectedIndexChanged += this.columnsListing_SelectedIndexChanged;
		}

		// Token: 0x0600E8E7 RID: 59623 RVA: 0x00344CC0 File Offset: 0x00342EC0
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x0600E8E8 RID: 59624 RVA: 0x00344CC4 File Offset: 0x00342EC4
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			this.style = (Style)context.Instance;
			this.oldValue = value.ToString();
			if (provider != null)
			{
				this.editorService = (provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService);
			}
			if (this.editorService != null)
			{
				this.columnsListing.Items.Clear();
				if (this.style is StyleLabelTitle | this.style is StyleMarker | this.style is StyleLabel | this.style is StyleSeries | this.style is StyleSeriesItem | this.style is StyleMarkerSeriesPoint)
				{
					foreach (string item in this.style.Chart.Figures.Figures)
					{
						this.columnsListing.Items.Add(item);
					}
					foreach (CustomFigure customFigure in this.style.Chart.CustomFigures)
					{
						if (!this.columnsListing.Items.Contains(customFigure.Name))
						{
							this.columnsListing.Items.Add(customFigure.Name);
						}
					}
				}
				this.columnsListing.Size = this.columnsListing.PreferredSize;
				this.columnsListing.Height = this.columnsListing.PreferredHeight;
				this.editorService.DropDownControl(this.columnsListing);
				if (this.columnsListing.SelectedItem != null)
				{
					value = this.columnsListing.SelectedItem;
				}
			}
			return value;
		}

		// Token: 0x0600E8E9 RID: 59625 RVA: 0x00344EAC File Offset: 0x003430AC
		public void columnsListing_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.editorService != null)
			{
				this.editorService.CloseDropDown();
			}
		}

		// Token: 0x0600E8EA RID: 59626 RVA: 0x00344EC1 File Offset: 0x003430C1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600E8EB RID: 59627 RVA: 0x00344ED0 File Offset: 0x003430D0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.columnsListing.Dispose();
			}
		}

		// Token: 0x040042FC RID: 17148
		private IWindowsFormsEditorService editorService;

		// Token: 0x040042FD RID: 17149
		public ListBox columnsListing;

		// Token: 0x040042FE RID: 17150
		public string oldValue = string.Empty;

		// Token: 0x040042FF RID: 17151
		private Style style;
	}
}
