using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.TabStrip.Rendering
{
	// Token: 0x020008ED RID: 2285
	public class TabRendererBase : IRenderer
	{
		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x06005658 RID: 22104 RVA: 0x00108824 File Offset: 0x00106A24
		// (set) Token: 0x06005659 RID: 22105 RVA: 0x0010882C File Offset: 0x00106A2C
		protected RadTab Tab
		{
			get
			{
				return this._tab;
			}
			set
			{
				this._tab = value;
			}
		}

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x0600565A RID: 22106 RVA: 0x00108835 File Offset: 0x00106A35
		protected IRadTabContainer Owner
		{
			get
			{
				return this.Tab.Owner;
			}
		}

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x0600565B RID: 22107 RVA: 0x00108844 File Offset: 0x00106A44
		protected virtual List<string> CurrentCssClass
		{
			get
			{
				return new List<string>
				{
					"rtsLink"
				};
			}
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x00108865 File Offset: 0x00106A65
		public TabRendererBase(RadTab tab)
		{
			this._tab = tab;
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x0600565D RID: 22109 RVA: 0x00108874 File Offset: 0x00106A74
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x0600565E RID: 22110 RVA: 0x00108878 File Offset: 0x00106A78
		public string CssClassFormatString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x0010887F File Offset: 0x00106A7F
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer, null);
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x0010888C File Offset: 0x00106A8C
		public virtual void AddAttributesToRender(HtmlTextWriter writer, Action<List<string>> action = null)
		{
			List<string> list = new List<string>
			{
				"rtsLI"
			};
			if (action != null)
			{
				action(list);
			}
			if (!this.Tab.Width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Tab.Width.ToString());
				this.Tab.Width = Unit.Empty;
			}
			if (!string.IsNullOrEmpty(this.Tab.OuterCssClass))
			{
				list.Add(this.Tab.OuterCssClass);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Join(" ", list.ToArray()));
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x00108938 File Offset: 0x00106B38
		public virtual void RenderContents(HtmlTextWriter writer)
		{
			string cssClass = this.Tab.CssClass;
			this.Tab.CssClass = string.Join(" ", this.CurrentCssClass.ToArray());
			this.Tab.AddAttributes(writer);
			this.Tab.CssClass = cssClass;
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x00108989 File Offset: 0x00106B89
		protected virtual void RenderSpan(HtmlTextWriter writer, Action action)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			action();
			writer.RenderEndTag();
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x001089A0 File Offset: 0x00106BA0
		protected virtual void RenderLink(HtmlTextWriter writer, Action action)
		{
			string value = string.IsNullOrEmpty(this.Tab.NavigateUrl) ? "#" : this.Tab.ResolveClientUrl(this.Tab.NavigateUrl);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
			if (!string.IsNullOrEmpty(this.Tab.Target))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, this.Tab.Target);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			action();
			writer.RenderEndTag();
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x00108A20 File Offset: 0x00106C20
		protected virtual void RenderDiv(HtmlTextWriter writer, Action action)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			action();
			writer.RenderEndTag();
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x00108A38 File Offset: 0x00106C38
		protected virtual void RenderImage(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(this.Tab.CurrentImageUrl))
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtsImg");
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.Tab.ToolTip);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, this.Tab.CurrentImageUrl);
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x00108A98 File Offset: 0x00106C98
		protected virtual void RenderText(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtsTxt");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.Tab.Text);
			writer.RenderEndTag();
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x00108AC8 File Offset: 0x00106CC8
		protected virtual void RenderTemplateContent(HtmlTextWriter writer)
		{
			if (this.Tab.Controls.IsReadOnly)
			{
				this.Tab.RenderChildControls(writer);
			}
			foreach (object obj in this.Tab.Controls)
			{
				Control control = (Control)obj;
				if (!(control is RadTab))
				{
					control.RenderControl(writer);
				}
			}
		}

		// Token: 0x04001523 RID: 5411
		private RadTab _tab;
	}
}
