using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000012 RID: 18
	public class custDGTemplate : ITemplate
	{
		// Token: 0x0600011C RID: 284 RVA: 0x0000E658 File Offset: 0x0000C858
		public custDGTemplate(string itemcolumname)
		{
			this.fieldname = itemcolumname;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000E66C File Offset: 0x0000C86C
		public void InstantiateIn(Control container)
		{
			TextBox textBox = new TextBox();
			textBox.MaxLength = 8000;
			textBox.DataBinding += this.OnDataBinding;
			container.Controls.Add(textBox);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		public void OnDataBinding(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.MaxLength = 8000;
			textBox.Width = new Unit(5.5, UnitType.Em);
			DataGridItem dataGridItem = (DataGridItem)textBox.NamingContainer;
			textBox.Text = ((DataRowView)dataGridItem.DataItem)[this.fieldname].ToString();
		}

		// Token: 0x04000077 RID: 119
		private string fieldname;
	}
}
