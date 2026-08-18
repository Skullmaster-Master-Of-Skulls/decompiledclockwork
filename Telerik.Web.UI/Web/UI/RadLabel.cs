using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000400 RID: 1024
	[EmbeddedSkin("Label", typeof(RadLabel))]
	[ToolboxBitmap(typeof(RadLabel), "Telerik.Web.UI.Label.png")]
	[EmbeddedSkin("Label", "Default", typeof(RadLabel))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Description("Telerik RadLabel")]
	[Designer("Telerik.Web.Design.LabelControlDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[DefaultProperty("Text")]
	[ControlValueProperty("Text")]
	[ParseChildren(false)]
	[TelerikToolboxCategory("Miscellaneous")]
	public class RadLabel : RadWebControl, ITextControl
	{
		// Token: 0x06002574 RID: 9588 RVA: 0x0007C5A1 File Offset: 0x0007A7A1
		public override void RenderClientStateField(HtmlTextWriter writer)
		{
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06002575 RID: 9589 RVA: 0x0007C5A3 File Offset: 0x0007A7A3
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.AssociatedControlID.Length != 0)
				{
					return HtmlTextWriterTag.Label;
				}
				return base.TagKey;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x0007C5BB File Offset: 0x0007A7BB
		protected override string CssClassFormatString
		{
			get
			{
				return "RadLabel RadLabel_{0}";
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0007C5C4 File Offset: 0x0007A7C4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.AssociatedControlID))
			{
				Control control = this.FindControl(this.AssociatedControlID);
				if (control != null)
				{
					ILabelableControl labelableControl = control as ILabelableControl;
					if (labelableControl != null)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.For, labelableControl.ControlId);
					}
					else
					{
						writer.AddAttribute(HtmlTextWriterAttribute.For, control.ClientID);
					}
				}
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x0007C620 File Offset: 0x0007A820
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
			if (!string.IsNullOrEmpty(this.Text))
			{
				descriptor.AddProperty("_text", this.Text);
			}
			if (!string.IsNullOrEmpty(this.AssociatedControlID))
			{
				descriptor.AddProperty("_associatedControlID", this.AssociatedControlID);
			}
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x0007C670 File Offset: 0x0007A870
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				if (string.IsNullOrEmpty(this.Text))
				{
					writer.Write("[]");
				}
			}
			else
			{
				this.RenderTrialMessage(writer);
			}
			if (this.HtmlEncode)
			{
				writer.Write(HttpUtility.HtmlEncode(this.Text));
			}
			else
			{
				writer.Write(this.Text);
			}
			if (this.MarkDisplayMode == MarkDisplayMode.Optional)
			{
				this.Controls.Add(new Label
				{
					Text = this.OptionalMark,
					CssClass = "rlOptMark"
				});
			}
			if (this.MarkDisplayMode == MarkDisplayMode.Required)
			{
				this.Controls.Add(new Label
				{
					Text = this.RequiredMark,
					CssClass = "rlRequiredMark"
				});
			}
			base.RenderContents(writer);
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x0007C738 File Offset: 0x0007A938
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return new List<ScriptDescriptor>();
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x0007C73F File Offset: 0x0007A93F
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new List<ScriptReference>();
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x0007C748 File Offset: 0x0007A948
		// (set) Token: 0x0600257D RID: 9597 RVA: 0x0007C775 File Offset: 0x0007A975
		[TypeConverter(typeof(AssociatedControlConverter))]
		[DefaultValue("")]
		[IDReferenceProperty]
		public virtual string AssociatedControlID
		{
			get
			{
				string text = (string)this.ViewState["AssociatedControlID"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["AssociatedControlID"] = value;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x0007C788 File Offset: 0x0007A988
		// (set) Token: 0x0600257F RID: 9599 RVA: 0x0007C7B5 File Offset: 0x0007A9B5
		[DefaultValue("")]
		[Localizable(true)]
		[Bindable(true)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x0007C7DC File Offset: 0x0007A9DC
		// (set) Token: 0x06002581 RID: 9601 RVA: 0x0007C805 File Offset: 0x0007AA05
		[DefaultValue(false)]
		[Localizable(true)]
		[Description("Sets or gets whether Text content must be encoded.")]
		public virtual bool HtmlEncode
		{
			get
			{
				object obj = base.ViewState["HtmlEncode"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["HtmlEncode"] = value;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x0007C820 File Offset: 0x0007AA20
		// (set) Token: 0x06002583 RID: 9603 RVA: 0x0007C84D File Offset: 0x0007AA4D
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DefaultValue("*")]
		[Localizable(true)]
		[Bindable(true)]
		public virtual string RequiredMark
		{
			get
			{
				object obj = this.ViewState["RequiredMark"];
				if (obj == null)
				{
					return "*";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["RequiredMark"] = value;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x0007C860 File Offset: 0x0007AA60
		// (set) Token: 0x06002585 RID: 9605 RVA: 0x0007C88D File Offset: 0x0007AA8D
		[Bindable(true)]
		[DefaultValue("*")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Localizable(true)]
		public virtual string OptionalMark
		{
			get
			{
				object obj = this.ViewState["OptionalMark"];
				if (obj == null)
				{
					return "*";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["OptionalMark"] = value;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x0007C8A0 File Offset: 0x0007AAA0
		// (set) Token: 0x06002587 RID: 9607 RVA: 0x0007C8CB File Offset: 0x0007AACB
		[Description("Determinates if the Label should render RequiredMark or OptionalMark")]
		[NotifyParentProperty(true)]
		[DefaultValue(MarkDisplayMode.None)]
		public MarkDisplayMode MarkDisplayMode
		{
			get
			{
				if (this.ViewState["MarkDisplayMode"] == null)
				{
					return MarkDisplayMode.None;
				}
				return (MarkDisplayMode)this.ViewState["MarkDisplayMode"];
			}
			set
			{
				this.ViewState["MarkDisplayMode"] = value;
			}
		}
	}
}
