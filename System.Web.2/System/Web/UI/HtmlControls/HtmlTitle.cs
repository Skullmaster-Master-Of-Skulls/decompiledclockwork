using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000361 RID: 865
	public class HtmlTitle : HtmlControl
	{
		// Token: 0x06002812 RID: 10258 RVA: 0x000818F4 File Offset: 0x0007FAF4
		public HtmlTitle() : base("title")
		{
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x00081901 File Offset: 0x0007FB01
		// (set) Token: 0x06002814 RID: 10260 RVA: 0x00081917 File Offset: 0x0007FB17
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual string Text
		{
			get
			{
				if (this._text == null)
				{
					return string.Empty;
				}
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x00081920 File Offset: 0x0007FB20
		protected override void AddParsedSubObject(object obj)
		{
			if (obj is LiteralControl)
			{
				this._text = ((LiteralControl)obj).Text;
				return;
			}
			base.AddParsedSubObject(obj);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x00059FD4 File Offset: 0x000581D4
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00081943 File Offset: 0x0007FB43
		protected internal override void Render(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Title);
			if (this.HasControls() || base.HasRenderDelegate())
			{
				this.RenderChildren(writer);
			}
			else if (this._text != null)
			{
				writer.Write(this._text);
			}
			writer.RenderEndTag();
		}

		// Token: 0x04001DE2 RID: 7650
		private string _text;
	}
}
