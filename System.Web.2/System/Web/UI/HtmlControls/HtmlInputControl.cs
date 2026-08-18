using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200034E RID: 846
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public abstract class HtmlInputControl : HtmlControl
	{
		// Token: 0x060026E8 RID: 9960 RVA: 0x0007F332 File Offset: 0x0007D532
		protected HtmlInputControl(string type) : base("input")
		{
			this._type = type;
			base.Attributes["type"] = type;
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x0007F357 File Offset: 0x0007D557
		// (set) Token: 0x060026EA RID: 9962 RVA: 0x00006164 File Offset: 0x00004364
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Name
		{
			get
			{
				return this.UniqueID;
			}
			set
			{
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x0007F35F File Offset: 0x0007D55F
		internal virtual string RenderedNameAttribute
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x0007F368 File Offset: 0x0007D568
		// (set) Token: 0x060026ED RID: 9965 RVA: 0x0007F390 File Offset: 0x0007D590
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Value
		{
			get
			{
				string text = base.Attributes["value"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["value"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x0007F3A8 File Offset: 0x0007D5A8
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Type
		{
			get
			{
				string text = base.Attributes["type"];
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				if (this._type == null)
				{
					return string.Empty;
				}
				return this._type;
			}
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0007F3E4 File Offset: 0x0007D5E4
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			writer.WriteAttribute("name", this.RenderedNameAttribute);
			base.Attributes.Remove("name");
			bool flag = false;
			string type = this.Type;
			if (!string.IsNullOrEmpty(type))
			{
				writer.WriteAttribute("type", type);
				base.Attributes.Remove("type");
				flag = true;
			}
			base.RenderAttributes(writer);
			if (flag && base.DesignMode)
			{
				base.Attributes.Add("type", type);
			}
			writer.Write(" /");
		}

		// Token: 0x04001DC9 RID: 7625
		private string _type;
	}
}
