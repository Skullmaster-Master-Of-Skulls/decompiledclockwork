using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001328 RID: 4904
	internal class SchedulerCheckBoxList : CheckBoxList
	{
		// Token: 0x0600CCCF RID: 52431 RVA: 0x002DA834 File Offset: 0x002D8A34
		protected override void Render(HtmlTextWriter writer)
		{
			writer.WriteBeginTag("ul");
			writer.WriteAttribute("class", "rsCheckBoxList");
			writer.Write('>');
			for (int i = 0; i < this.RepeatedItemCount; i++)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				this.RenderItem(ListItemType.Item, i, null, writer);
				writer.RenderEndTag();
			}
			writer.WriteEndTag("ul");
		}
	}
}
