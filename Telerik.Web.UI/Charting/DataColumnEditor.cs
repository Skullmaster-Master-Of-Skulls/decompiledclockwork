using System;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.Charting
{
	// Token: 0x02001752 RID: 5970
	internal class DataColumnEditor : UITypeEditor, IDisposable
	{
		// Token: 0x0600E8DE RID: 59614 RVA: 0x003449F9 File Offset: 0x00342BF9
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x0600E8DF RID: 59615 RVA: 0x003449FC File Offset: 0x00342BFC
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (value != null)
			{
				this.oldValue = value.ToString();
			}
			else
			{
				this.oldValue = string.Empty;
			}
			if (provider != null)
			{
				this.editorService = (provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService);
			}
			Chart chart;
			try
			{
				chart = ((ChartSeries)context.Instance).Chart;
			}
			catch
			{
				chart = null;
			}
			if (chart == null)
			{
				try
				{
					chart = ((ChartXAxis)context.Instance).Chart;
				}
				catch
				{
					chart = null;
				}
			}
			if (chart == null)
			{
				try
				{
					chart = ((IChartComponent)context.Instance).Chart;
				}
				catch
				{
					chart = null;
				}
			}
			if (this.editorService != null)
			{
				this.columnsListing = new ListBox();
				try
				{
					if (chart != null && chart.DataManager.DataSource != null)
					{
						this.FillListBox(chart.DataManager.DataSource, chart.DataManager.DataMember);
					}
				}
				catch
				{
					this.FillListBox(null, null);
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

		// Token: 0x0600E8E0 RID: 59616 RVA: 0x00344B6C File Offset: 0x00342D6C
		public virtual void FillListBox(object data, string dataMember)
		{
			this.columnsListing.Items.Clear();
			this.columnsListing.Items.Add("(None)");
			if (data != null)
			{
				DataTableDataHelper dataTableDataHelper = (DataTableDataHelper)DataHelper.CreateDataHelper(data, dataMember, true);
				if (dataTableDataHelper != null)
				{
					foreach (object obj in dataTableDataHelper.DataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						this.columnsListing.Items.Add(dataColumn.ColumnName);
					}
				}
			}
			this.columnsListing.SelectedItem = this.oldValue;
			this.columnsListing.SelectedIndexChanged += this.columnsListing_SelectedIndexChanged;
		}

		// Token: 0x0600E8E1 RID: 59617 RVA: 0x00344C3C File Offset: 0x00342E3C
		public void columnsListing_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.editorService != null)
			{
				this.editorService.CloseDropDown();
			}
		}

		// Token: 0x0600E8E2 RID: 59618 RVA: 0x00344C51 File Offset: 0x00342E51
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600E8E3 RID: 59619 RVA: 0x00344C60 File Offset: 0x00342E60
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.columnsListing.Dispose();
			}
		}

		// Token: 0x040042F9 RID: 17145
		private IWindowsFormsEditorService editorService;

		// Token: 0x040042FA RID: 17146
		public ListBox columnsListing;

		// Token: 0x040042FB RID: 17147
		public string oldValue = string.Empty;
	}
}
