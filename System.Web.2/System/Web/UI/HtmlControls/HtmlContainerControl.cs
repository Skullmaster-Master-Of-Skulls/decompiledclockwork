using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000344 RID: 836
	public abstract class HtmlContainerControl : HtmlControl
	{
		// Token: 0x06002686 RID: 9862 RVA: 0x0007E6EA File Offset: 0x0007C8EA
		protected HtmlContainerControl() : this("span")
		{
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0007E6F7 File Offset: 0x0007C8F7
		public HtmlContainerControl(string tag) : base(tag)
		{
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x0007E700 File Offset: 0x0007C900
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x0007E79D File Offset: 0x0007C99D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[HtmlControlPersistable(false)]
		public virtual string InnerHtml
		{
			get
			{
				if (base.IsLiteralContent())
				{
					return ((LiteralControl)this.Controls[0]).Text;
				}
				if (this.HasControls() && this.Controls.Count == 1 && this.Controls[0] is DataBoundLiteralControl)
				{
					return ((DataBoundLiteralControl)this.Controls[0]).Text;
				}
				if (this.Controls.Count == 0)
				{
					return string.Empty;
				}
				throw new HttpException(SR.GetString("Inner_Content_not_literal", new object[]
				{
					this.ID
				}));
			}
			set
			{
				this.Controls.Clear();
				this.Controls.Add(new LiteralControl(value));
				this.ViewState["innerhtml"] = value;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x0007E7CC File Offset: 0x0007C9CC
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x0007E7D9 File Offset: 0x0007C9D9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[HtmlControlPersistable(false)]
		public virtual string InnerText
		{
			get
			{
				return HttpUtility.HtmlDecode(this.InnerHtml);
			}
			set
			{
				this.InnerHtml = HttpUtility.HtmlEncode(value);
			}
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x00059FD4 File Offset: 0x000581D4
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x0007E7E8 File Offset: 0x0007C9E8
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				base.LoadViewState(savedState);
				string text = (string)this.ViewState["innerhtml"];
				if (text != null)
				{
					this.Controls.Clear();
					this.Controls.Add(new LiteralControl(text));
				}
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0007E834 File Offset: 0x0007CA34
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderChildren(writer);
			this.RenderEndTag(writer);
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0007E84B File Offset: 0x0007CA4B
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			this.ViewState.Remove("innerhtml");
			base.RenderAttributes(writer);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0007E864 File Offset: 0x0007CA64
		protected virtual void RenderEndTag(HtmlTextWriter writer)
		{
			writer.WriteEndTag(this.TagName);
		}
	}
}
