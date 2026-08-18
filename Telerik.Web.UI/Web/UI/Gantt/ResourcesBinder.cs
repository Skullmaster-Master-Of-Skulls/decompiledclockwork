using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002EB RID: 747
	[ToolboxItem(false)]
	public class ResourcesBinder : DataBoundControl
	{
		// Token: 0x060019D1 RID: 6609 RVA: 0x00054C28 File Offset: 0x00052E28
		public new DataSourceView GetData()
		{
			return base.GetData();
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00054C30 File Offset: 0x00052E30
		public static IResource BindResource(object dataItem, IResourcesDataBindings bindings)
		{
			Resource resource = new Resource();
			resource.ID = DataBinder.Eval(dataItem, bindings.IdField);
			resource.Text = (string)DataBinder.Eval(dataItem, bindings.TextField);
			if (!string.IsNullOrEmpty(bindings.ColorField))
			{
				object obj = DataBinder.Eval(dataItem, bindings.ColorField);
				if (obj is Color)
				{
					resource.Color = (Color)obj;
				}
				else
				{
					resource.Color = ColorTranslator.FromHtml(DataBinder.Eval(dataItem, bindings.ColorField).ToString());
				}
			}
			if (!string.IsNullOrEmpty(bindings.FormatField))
			{
				resource.Format = (string)(DataBinder.Eval(dataItem, bindings.FormatField) ?? "%");
			}
			return resource;
		}
	}
}
