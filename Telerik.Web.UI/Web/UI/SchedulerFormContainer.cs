using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x02001310 RID: 4880
	[ToolboxItem(false)]
	public sealed class SchedulerFormContainer : SchedulerAppointmentContainer
	{
		// Token: 0x0600CC3C RID: 52284 RVA: 0x002D8779 File Offset: 0x002D6979
		public SchedulerFormContainer(RadScheduler owner) : this(owner, 0, 0)
		{
		}

		// Token: 0x0600CC3D RID: 52285 RVA: 0x002D8784 File Offset: 0x002D6984
		public SchedulerFormContainer(RadScheduler owner, int width, int height) : base(owner)
		{
			this._width = width;
			this._height = height;
			this._isMobile = (owner.ResolvedRenderMode == RenderMode.Mobile);
			this._isLite = (owner.ResolvedRenderMode == RenderMode.Lightweight);
		}

		// Token: 0x170041BC RID: 16828
		// (get) Token: 0x0600CC3E RID: 52286 RVA: 0x002D87C0 File Offset: 0x002D69C0
		// (set) Token: 0x0600CC3F RID: 52287 RVA: 0x002D87C8 File Offset: 0x002D69C8
		[DefaultValue(ClientIDMode.AutoID)]
		[NotifyParentProperty(true)]
		[Description("This property is overridden in order to support controls which implement INamingContainer")]
		public override ClientIDMode ClientIDMode
		{
			get
			{
				return this.ClientIDModeValue;
			}
			set
			{
				if (this.ClientIDModeValue != value)
				{
					base.ClearEffectiveClientIDMode();
					base.ClearCachedClientID();
				}
				this.ClientIDModeValue = value;
			}
		}

		// Token: 0x170041BD RID: 16829
		// (get) Token: 0x0600CC40 RID: 52288 RVA: 0x002D87E6 File Offset: 0x002D69E6
		// (set) Token: 0x0600CC41 RID: 52289 RVA: 0x002D87F3 File Offset: 0x002D69F3
		public new IBindableTemplate Template
		{
			get
			{
				return (IBindableTemplate)base.Template;
			}
			set
			{
				base.Template = value;
			}
		}

		// Token: 0x170041BE RID: 16830
		// (get) Token: 0x0600CC42 RID: 52290 RVA: 0x002D87FC File Offset: 0x002D69FC
		// (set) Token: 0x0600CC43 RID: 52291 RVA: 0x002D8804 File Offset: 0x002D6A04
		public SchedulerFormMode Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		// Token: 0x170041BF RID: 16831
		// (get) Token: 0x0600CC44 RID: 52292 RVA: 0x002D880D File Offset: 0x002D6A0D
		// (set) Token: 0x0600CC45 RID: 52293 RVA: 0x002D8815 File Offset: 0x002D6A15
		internal int LeftOffset { get; set; }

		// Token: 0x170041C0 RID: 16832
		// (get) Token: 0x0600CC46 RID: 52294 RVA: 0x002D8820 File Offset: 0x002D6A20
		private bool IsCustomTemplate
		{
			get
			{
				return (base.Owner.ActiveFormMode != SchedulerFormMode.Insert || !(base.Owner.InlineInsertTemplate is InlineInsertTemplate)) && (base.Owner.ActiveFormMode != SchedulerFormMode.Edit || !(base.Owner.InlineEditTemplate is InlineEditTemplate));
			}
		}

		// Token: 0x0600CC47 RID: 52295 RVA: 0x002D8872 File Offset: 0x002D6A72
		protected override object GetDataItem()
		{
			return base.Appointment;
		}

		// Token: 0x0600CC48 RID: 52296 RVA: 0x002D887A File Offset: 0x002D6A7A
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.RegisterRequiresControlState(this);
		}

		// Token: 0x0600CC49 RID: 52297 RVA: 0x002D8890 File Offset: 0x002D6A90
		protected override void LoadControlState(object savedState)
		{
			object[] array = (object[])savedState;
			base.Appointment = (Appointment)array[0];
		}

		// Token: 0x0600CC4A RID: 52298 RVA: 0x002D88B4 File Offset: 0x002D6AB4
		protected override object SaveControlState()
		{
			return new object[]
			{
				base.Appointment
			};
		}

		// Token: 0x0600CC4B RID: 52299 RVA: 0x002D88D4 File Offset: 0x002D6AD4
		protected override void Render(HtmlTextWriter writer)
		{
			if (this._height > 0 && this.Template is InlineTemplate)
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					WebControl webControl = control as WebControl;
					if (webControl != null && webControl.CssClass == "rsAptEditTextareaWrapper")
					{
						webControl.Height = Unit.Pixel(this._height);
					}
				}
			}
			this.RenderPrologue(writer);
			base.Render(writer);
			this.RenderEpilogue(writer);
		}

		// Token: 0x0600CC4C RID: 52300 RVA: 0x002D8980 File Offset: 0x002D6B80
		private void RenderEpilogue(HtmlTextWriter writer)
		{
			if (this.Mode == SchedulerFormMode.AdvancedEdit || this.Mode == SchedulerFormMode.AdvancedInsert)
			{
				return;
			}
			if (this.IsCustomTemplate)
			{
				writer.RenderEndTag();
			}
			if (this._isMobile)
			{
				writer.RenderEndTag();
				return;
			}
			if (this._isLite)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {1}", "rsResize", "rsAptEditResizeHandle"));
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {1}", "p-icon", "p-i-arrow-45-down-right"));
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsAptEditResizeHandle");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600CC4D RID: 52301 RVA: 0x002D8A68 File Offset: 0x002D6C68
		private void RenderPrologue(HtmlTextWriter writer)
		{
			if (this.Mode == SchedulerFormMode.AdvancedEdit || this.Mode == SchedulerFormMode.AdvancedInsert)
			{
				return;
			}
			if (this._isMobile)
			{
				string text = "rsAptEditSizingWrapper";
				if (this.Mode == SchedulerFormMode.Insert)
				{
					text += " rsAptEditSizingWrapperInsert";
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			else
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "2000");
				if (this._width > 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this._width + "px");
				}
				if (this.LeftOffset != 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.MarginLeft, this.LeftOffset + "px");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Left, this.LeftOffset * 100 + "%");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsAptEditSizingWrapper");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				if (this._isLite)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {1}", "rsDialog rsInlineForm", "rsAptEditFormWrapper"));
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsAptEditFormWrapper");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsAptEditFormOuter");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsAptEditFormMiddle");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsAptEditFormMiddle2");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsAptEditFormInner");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
				}
			}
			if (this.IsCustomTemplate)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsTemplateWrapper");
				if (this._height > 0 && !this._isMobile)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this._height + "px");
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
		}

		// Token: 0x04003580 RID: 13696
		private SchedulerFormMode _mode;

		// Token: 0x04003581 RID: 13697
		private readonly int _width;

		// Token: 0x04003582 RID: 13698
		private readonly int _height;

		// Token: 0x04003583 RID: 13699
		private readonly bool _isMobile;

		// Token: 0x04003584 RID: 13700
		private readonly bool _isLite;

		// Token: 0x04003585 RID: 13701
		private ClientIDMode ClientIDModeValue = ClientIDMode.AutoID;
	}
}
