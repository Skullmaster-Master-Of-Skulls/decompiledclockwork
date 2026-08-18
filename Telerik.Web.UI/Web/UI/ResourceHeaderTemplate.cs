using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001A40 RID: 6720
	internal class ResourceHeaderTemplate : ITemplate
	{
		// Token: 0x060104BE RID: 66750 RVA: 0x003A3E84 File Offset: 0x003A2084
		public void InstantiateIn(Control container)
		{
			Literal literal = new Literal();
			container.Controls.Add(literal);
			literal.DataBinding += ResourceHeaderTemplate.Text_OnDataBinding;
		}

		// Token: 0x060104BF RID: 66751 RVA: 0x003A3EB8 File Offset: 0x003A20B8
		private static void Text_OnDataBinding(object sender, EventArgs e)
		{
			Literal literal = (Literal)sender;
			IDataItemContainer dataItemContainer = (IDataItemContainer)literal.BindingContainer;
			literal.Text = HttpUtility.HtmlEncode((string)DataBinder.Eval(dataItemContainer.DataItem, "Text"));
		}
	}
}
