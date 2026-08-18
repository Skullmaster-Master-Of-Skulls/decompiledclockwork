using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000005 RID: 5
	public class AccordionDesigner : ControlDesigner
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002E7C File Offset: 0x0000107C
		public override void Initialize(IComponent component)
		{
			this._accordion = (component as Accordion);
			if (this._accordion == null)
			{
				throw new ArgumentException("Component must be an Accordion control", "component");
			}
			base.Initialize(component);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EAC File Offset: 0x000010AC
		public override string GetDesignTimeHtml()
		{
			if (this._accordion.Height == Unit.Empty)
			{
				this._accordion.Height = new Unit(175);
			}
			if (this._accordion.Width == Unit.Empty)
			{
				this._accordion.Width = new Unit(300);
			}
			ControlCollection controls = this._accordion.Controls;
			string text = base.GetDesignTimeHtml();
			int num = text.ToString().IndexOf("<div", 1);
			if (num > 0)
			{
				text = text.ToString().Substring(0, text.ToString().IndexOf("<div", 1));
			}
			else
			{
				text = text.Remove(text.Length - 6, 6);
			}
			text = text.Replace("\r", "").Replace("\n", "").Replace("\t", "");
			if (!text.Contains("overflow"))
			{
				text = text.Replace("style=\"", "style=\"overflow:scroll;");
			}
			StringBuilder stringBuilder = new StringBuilder(text);
			foreach (AccordionPane accordionPane in (AccordionPane[])this._accordion.Panes.ToArray<AccordionPane>().Clone())
			{
				stringBuilder.Append("<span>");
				string arg = (!string.IsNullOrEmpty(accordionPane.HeaderCssClass)) ? accordionPane.HeaderCssClass : this._accordion.HeaderCssClass;
				stringBuilder.AppendFormat("<div class=\"{0}\">", arg);
				TemplateBuilder templateBuilder = accordionPane.Header as TemplateBuilder;
				if (templateBuilder != null)
				{
					stringBuilder.Append(templateBuilder.Text);
				}
				else
				{
					stringBuilder.Append("AccordionPane Header ");
					stringBuilder.Append(accordionPane.ID);
				}
				stringBuilder.Append("</div>");
				string arg2 = (!string.IsNullOrEmpty(accordionPane.ContentCssClass)) ? accordionPane.ContentCssClass : this._accordion.ContentCssClass;
				stringBuilder.AppendFormat("<div class=\"{0}\">", arg2);
				templateBuilder = (accordionPane.Content as TemplateBuilder);
				if (templateBuilder != null)
				{
					stringBuilder.Append(templateBuilder.Text);
				}
				else
				{
					stringBuilder.Append("AccordionPane Content ");
					stringBuilder.Append(accordionPane.ID);
				}
				stringBuilder.Append("</div>");
				stringBuilder.Append("</span>");
			}
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}

		// Token: 0x04000019 RID: 25
		private Accordion _accordion;
	}
}
