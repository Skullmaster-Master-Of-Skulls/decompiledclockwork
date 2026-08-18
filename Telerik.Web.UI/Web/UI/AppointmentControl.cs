using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common.Helpers;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x02000821 RID: 2081
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public abstract class AppointmentControl : WebControl
	{
		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x06004CDC RID: 19676 RVA: 0x000F1699 File Offset: 0x000EF899
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x06004CDD RID: 19677 RVA: 0x000F169D File Offset: 0x000EF89D
		public Appointment Appointment
		{
			get
			{
				return this._appointment;
			}
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x06004CDE RID: 19678 RVA: 0x000F16A5 File Offset: 0x000EF8A5
		// (set) Token: 0x06004CDF RID: 19679 RVA: 0x000F16AD File Offset: 0x000EF8AD
		public SchedulerAppointmentContainer AppointmentContainer { get; protected set; }

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x000F16B6 File Offset: 0x000EF8B6
		// (set) Token: 0x06004CE1 RID: 19681 RVA: 0x000F16BE File Offset: 0x000EF8BE
		internal DayViewBlockColumn Column
		{
			get
			{
				return this._column;
			}
			set
			{
				this._column = value;
			}
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06004CE2 RID: 19682 RVA: 0x000F16C7 File Offset: 0x000EF8C7
		// (set) Token: 0x06004CE3 RID: 19683 RVA: 0x000F16CF File Offset: 0x000EF8CF
		protected internal DateTime BoxStart { get; protected set; }

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x06004CE4 RID: 19684 RVA: 0x000F16D8 File Offset: 0x000EF8D8
		// (set) Token: 0x06004CE5 RID: 19685 RVA: 0x000F16E0 File Offset: 0x000EF8E0
		protected internal DateTime BoxEnd { get; protected set; }

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x06004CE6 RID: 19686 RVA: 0x000F16E9 File Offset: 0x000EF8E9
		protected virtual int AppointmentColSpan
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x06004CE7 RID: 19687 RVA: 0x000F16EC File Offset: 0x000EF8EC
		protected Color EffectiveBackColor
		{
			get
			{
				if (this.Appointment.BackColor != Color.Empty)
				{
					return this.Appointment.BackColor;
				}
				RadScheduler owner = this.Appointment.Owner;
				foreach (object obj in this.Appointment.Resources)
				{
					Resource res = (Resource)obj;
					if (owner != null)
					{
						Color matchingBackColor = owner.ResourceStyles.GetMatchingBackColor(res);
						if (matchingBackColor != Color.Empty)
						{
							return matchingBackColor;
						}
					}
				}
				return Color.Empty;
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x000F17A4 File Offset: 0x000EF9A4
		protected Color EffectiveBorderColor
		{
			get
			{
				if (this.Appointment.BorderColor != Color.Empty)
				{
					return this.Appointment.BorderColor;
				}
				RadScheduler owner = this.Appointment.Owner;
				foreach (object obj in this.Appointment.Resources)
				{
					Resource res = (Resource)obj;
					if (owner != null)
					{
						Color matchingBorderColor = owner.ResourceStyles.GetMatchingBorderColor(res);
						if (matchingBorderColor != Color.Empty)
						{
							return matchingBorderColor;
						}
					}
				}
				return Color.Empty;
			}
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x06004CE9 RID: 19689 RVA: 0x000F185C File Offset: 0x000EFA5C
		private bool HasBorder
		{
			get
			{
				return this.EffectiveBorderColor != Color.Empty;
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x06004CEA RID: 19690 RVA: 0x000F186E File Offset: 0x000EFA6E
		private bool HasCustomColor
		{
			get
			{
				return this.EffectiveBackColor != Color.Empty || this.HasBorder;
			}
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x000F188C File Offset: 0x000EFA8C
		private AppointmentStyleMode StyleMode
		{
			get
			{
				AppointmentStyleMode result = AppointmentStyleMode.Auto;
				if (this.Appointment.Owner != null)
				{
					result = this.Appointment.Owner.AppointmentStyleMode;
				}
				return result;
			}
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x000F18BA File Offset: 0x000EFABA
		protected bool IsLightweight
		{
			get
			{
				return this.Appointment.Owner.ResolvedRenderMode == RenderMode.Lightweight;
			}
		}

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x06004CED RID: 19693 RVA: 0x000F18CF File Offset: 0x000EFACF
		protected bool IsMobile
		{
			get
			{
				return this.Appointment.Owner.ResolvedRenderMode == RenderMode.Mobile;
			}
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x000F18E4 File Offset: 0x000EFAE4
		protected AppointmentControl(Appointment appointment) : this(appointment, true)
		{
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x000F18F0 File Offset: 0x000EFAF0
		protected AppointmentControl(Appointment appointment, bool registerWithAppointment)
		{
			this._appointment = appointment;
			this.BoxStart = appointment.Start;
			this.BoxEnd = this.Appointment.End;
			if (registerWithAppointment)
			{
				this._appointment.AppointmentControls.Add(this);
				this._index = this._appointment.AppointmentControls.IndexOf(this);
			}
			this._roundedCornersHeight = (this.IsLightweight ? 2 : 4);
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x000F1964 File Offset: 0x000EFB64
		internal virtual void CalculateSize()
		{
			this.Width = this.GetWidth();
			this.Height = this.GetHeight();
			if (this.Height.Type == UnitType.Pixel)
			{
				int num = (int)this.Height.Value - this._roundedCornersHeight;
				this.Height = Unit.Pixel((num >= 0) ? num : 0);
				if (HttpContext.Current != null && HttpContext.Current.Request.Browser.IsBrowser("IE") && this.Appointment.Owner != null && this.Appointment.Owner.ResolvedRenderMode == RenderMode.Classic)
				{
					this._outerWrap.Height = this.Height;
				}
			}
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x000F1A18 File Offset: 0x000EFC18
		internal bool OverlapsWith(AppointmentControl control)
		{
			if (control.Appointment.Duration == TimeSpan.Zero && this.Appointment.Duration == TimeSpan.Zero)
			{
				return control.BoxStart == this.BoxStart;
			}
			if (this.Appointment.Duration == TimeSpan.Zero)
			{
				return this.BoxStart < control.BoxEnd && this.BoxEnd > control.BoxStart;
			}
			return this.BoxStart <= control.BoxEnd && this.BoxEnd > control.BoxStart;
		}

		// Token: 0x06004CF2 RID: 19698
		protected abstract Unit GetHeight();

		// Token: 0x06004CF3 RID: 19699
		protected abstract Unit GetWidth();

		// Token: 0x06004CF4 RID: 19700 RVA: 0x000F1ACC File Offset: 0x000EFCCC
		protected virtual void Initialize()
		{
			this.ID = string.Format("{0}_{1}", this._appointment.Owner.Appointments.IndexOf(this._appointment), this._index);
			if (this.IsLightweight)
			{
				this.AddContents(this);
				return;
			}
			this.AddWrap(this);
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x000F1B2B File Offset: 0x000EFD2B
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ApplyAppointmentStyles();
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x000F1B3C File Offset: 0x000EFD3C
		private void AddWrap(Control container)
		{
			this._outerWrap = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsAptOut"
			};
			this.AddMiddleWrap(this._outerWrap);
			container.Controls.Add(this._outerWrap);
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x000F1B80 File Offset: 0x000EFD80
		private void AddMiddleWrap(Control outWrap)
		{
			this._middleWrap = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsAptMid"
			};
			outWrap.Controls.Add(this._middleWrap);
			this.AddInnerWrap(this._middleWrap);
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x000F1BC4 File Offset: 0x000EFDC4
		private void AddInnerWrap(Control middleWrap)
		{
			this._innerWrap = new WebControl(HtmlTextWriterTag.Div);
			middleWrap.Controls.Add(this._innerWrap);
			this._innerWrap.MergeStyle(base.ControlStyle);
			this._innerWrap.Width = Unit.Empty;
			this._innerWrap.CssClass = this.GetAppointmentWrapCssStyle();
			this.AddContents(this._innerWrap);
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x000F1C30 File Offset: 0x000EFE30
		private void AddContents(Control container)
		{
			this._contentWrap = new WebControl(HtmlTextWriterTag.Div);
			container.Controls.Add(this._contentWrap);
			this._contentWrap.CssClass = "rsAptContent";
			this.AppointmentContainer = new SchedulerAppointmentContainer(this._appointment.Owner);
			this._contentWrap.Controls.Add(this.AppointmentContainer);
			this.AppointmentContainer.Appointment = this._appointment;
			if (!this._appointment.Owner.DesignMode)
			{
				this.AppointmentContainer.Template = this._appointment.Owner.AppointmentTemplate;
			}
			else
			{
				this.AppointmentContainer.Template = new AppointmentTemplate(this._appointment.Owner);
			}
			this.AppointmentContainer.Template.InstantiateIn(this.AppointmentContainer);
			if (!this.Appointment.Owner.ActiveModel.ReadOnly)
			{
				if (this.IsLightweight)
				{
					this.AddDeleteCommand(container);
				}
				else
				{
					this.AddDeleteCommand(this._contentWrap);
				}
				if (this.Appointment.AllowEdit)
				{
					if (this._renderStartResizeGrip)
					{
						if (this.IsLightweight)
						{
							container.Controls.Add(this.CreateResizeGrip(true));
						}
						else
						{
							container.Parent.Controls.Add(this.CreateResizeGrip(true));
						}
					}
					if (this._renderEndResizeGrip)
					{
						if (this.IsLightweight)
						{
							container.Controls.Add(this.CreateResizeGrip(false));
						}
						else
						{
							container.Parent.Controls.Add(this.CreateResizeGrip(false));
						}
					}
				}
			}
			this.AddArrows(container);
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x000F1DCC File Offset: 0x000EFFCC
		protected virtual void ApplyAppointmentStyles()
		{
			if (this.IsLightweight)
			{
				this._contentWrap.BackColor = this.EffectiveBackColor;
			}
			else if (this.StyleMode == AppointmentStyleMode.Default || (this.StyleMode == AppointmentStyleMode.Auto && !this.HasCustomColor))
			{
				if (this.Appointment.Owner.ResolvedRenderMode != RenderMode.Mobile)
				{
					this._middleWrap.BackColor = this.EffectiveBackColor;
					this._innerWrap.BackColor = this.EffectiveBackColor;
				}
				this._contentWrap.BackColor = this.EffectiveBackColor;
				if (this.EffectiveBorderColor != Color.Empty)
				{
					string value = AppointmentControl.FormatColor(this.EffectiveBorderColor);
					this._middleWrap.Style["border-color"] = value;
					this._innerWrap.Style["border-color"] = value;
					this._contentWrap.Style["border-color"] = value;
				}
			}
			else
			{
				this.BackColor = this.EffectiveBackColor;
			}
			this.ForeColor = this.Appointment.ForeColor;
			if (this.IsLightweight)
			{
				this._contentWrap.ForeColor = this.Appointment.ForeColor;
			}
			this.Font.CopyFrom(this.Appointment.Font);
			this.CssClass = this.GetClassName();
			this.ToolTip = this.Appointment.ToolTip;
			if (this.HasBorder)
			{
				if (this.IsLightweight)
				{
					this._contentWrap.BorderColor = this.EffectiveBorderColor;
					this._contentWrap.BorderStyle = this.Appointment.BorderStyle;
					this._contentWrap.BorderWidth = this.Appointment.BorderWidth;
					return;
				}
				if (this.StyleMode == AppointmentStyleMode.Simple || this.StyleMode == AppointmentStyleMode.Auto)
				{
					string value2 = (this.Appointment.BorderWidth == Unit.Empty) ? Unit.Pixel(1).ToString() : this.Appointment.BorderWidth.ToString();
					string value3 = (this.Appointment.BorderStyle == BorderStyle.NotSet) ? BorderStyle.Solid.ToString() : this.Appointment.BorderStyle.ToString();
					string value4 = AppointmentControl.FormatColor(this.EffectiveBorderColor);
					base.Style["border-top-width"] = (base.Style["border-bottom-width"] = value2);
					base.Style["border-top-style"] = (base.Style["border-bottom-style"] = value3);
					base.Style["border-top-color"] = (base.Style["border-bottom-color"] = value4);
					this._outerWrap.Style["border-left-width"] = (this._outerWrap.Style["border-right-width"] = value2);
					this._outerWrap.Style["border-left-style"] = (this._outerWrap.Style["border-right-style"] = value3);
					this._outerWrap.Style["border-left-color"] = (this._outerWrap.Style["border-right-color"] = value4);
				}
			}
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x000F2118 File Offset: 0x000F0318
		private static string FormatColor(Color c)
		{
			return string.Format("rgb({0}, {1}, {2})", c.R, c.G, c.B);
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x000F2148 File Offset: 0x000F0348
		protected string GetClassName()
		{
			List<string> list = new List<string>
			{
				"rsApt"
			};
			RadScheduler owner = this.Appointment.Owner;
			AppointmentControl.AddUniqueClassName(list, this.Appointment.CssClass);
			foreach (object obj in this.Appointment.Resources)
			{
				Resource resource = (Resource)obj;
				AppointmentControl.AddUniqueClassName(list, resource.CssClass);
				if (owner != null)
				{
					foreach (string value in owner.ResourceStyles.GetMatchingClasses(resource))
					{
						AppointmentControl.AddUniqueClassName(list, value);
					}
					Resource resource2 = owner.Resources.GetResource(resource.Type, resource.Key);
					if (resource2 != null)
					{
						AppointmentControl.AddUniqueClassName(list, resource2.CssClass);
					}
				}
			}
			bool flag = this.EffectiveBackColor != Color.Empty;
			bool flag2 = flag || this.HasBorder;
			if (!this.IsLightweight)
			{
				if (this.StyleMode == AppointmentStyleMode.Simple || (this.StyleMode == AppointmentStyleMode.Auto && flag2))
				{
					list.Add("rsAptSimple");
				}
				if (this.StyleMode == AppointmentStyleMode.Default && flag)
				{
					list.Add("rsAptColor");
				}
			}
			else
			{
				if (flag)
				{
					list.Add("rsAptColor");
				}
				if (this._renderLeftArrow)
				{
					list.Add("rsWArrowLeft");
				}
				if (this._renderRightArrow)
				{
					list.Add("rsWArrowRight");
				}
			}
			return string.Join(" ", list.ToArray());
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x000F2310 File Offset: 0x000F0510
		private static void AddUniqueClassName(ICollection<string> collection, string value)
		{
			if (string.IsNullOrEmpty(value) || collection.Contains(value))
			{
				return;
			}
			collection.Add(value);
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x000F232B File Offset: 0x000F052B
		private void AddArrows(Control container)
		{
			if (this._renderTopArrow)
			{
				this.AddTopArrow(container);
			}
			if (this._renderBottomArrow)
			{
				this.AddBottomArrow(container);
			}
			if (this._renderLeftArrow)
			{
				this.AddLeftArrow(container);
			}
			if (this._renderRightArrow)
			{
				this.AddRightArrow(container);
			}
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x000F236C File Offset: 0x000F056C
		private string GetAppointmentWrapCssStyle()
		{
			string text = "rsAptIn";
			if (this._renderTopArrow)
			{
				text += "  rsWArrowTop";
			}
			if (this._renderBottomArrow)
			{
				text += "  rsWArrowBottom";
			}
			if (this._renderLeftArrow)
			{
				text += "  rsWArrowLeft";
			}
			if (this._renderRightArrow)
			{
				text += "  rsWArrowRight";
			}
			return text;
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x000F23D0 File Offset: 0x000F05D0
		protected virtual WebControl CreateResizeGrip(bool resizeFromStart)
		{
			string text = "rsAptResize ";
			text += (resizeFromStart ? "rsAptResizeStart" : "rsAptResizeEnd");
			if (this.IsLightweight)
			{
				return new WebControl(HtmlTextWriterTag.Span)
				{
					CssClass = text
				};
			}
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = text
			};
			webControl.Style[HtmlTextWriterStyle.ZIndex] = "80";
			webControl.Controls.Add(new LiteralControl("<!-- -->"));
			return webControl;
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x000F244C File Offset: 0x000F064C
		protected void AddDeleteCommand(Control appointment)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("a");
			htmlGenericControl.Attributes["class"] = "rsAptDelete";
			htmlGenericControl.Attributes["href"] = "#";
			if (this.IsLightweight)
			{
				htmlGenericControl.Attributes["title"] = "delete";
				WebControl child = new WebControl(HtmlTextWriterTag.Span)
				{
					CssClass = string.Format("{0} {1}", "p-icon", "p-i-x")
				};
				htmlGenericControl.Controls.Add(child);
			}
			else
			{
				htmlGenericControl.InnerHtml = "delete";
			}
			appointment.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x000F24F4 File Offset: 0x000F06F4
		private void AddTopArrow(Control container)
		{
			Control child = this.CreateArrow("rsArrowTop", "arrow-60-up", "top");
			container.Controls.Add(child);
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x000F2524 File Offset: 0x000F0724
		private void AddBottomArrow(Control container)
		{
			Control child = this.CreateArrow("rsArrowBottom", "arrow-60-down", "bottom");
			container.Controls.Add(child);
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x000F2554 File Offset: 0x000F0754
		private void AddLeftArrow(Control container)
		{
			Control child = this.CreateArrow("rsArrowLeft", "arrow-60-left", "left");
			container.Controls.Add(child);
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x000F2584 File Offset: 0x000F0784
		private void AddRightArrow(Control container)
		{
			Control child = this.CreateArrow("rsArrowRight", "arrow-60-right", "right");
			container.Controls.Add(child);
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x000F25B4 File Offset: 0x000F07B4
		private Control CreateArrow(string cssClass, string iconCssClass, string text)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("a");
			htmlGenericControl.Attributes["class"] = cssClass;
			htmlGenericControl.Attributes["href"] = "#";
			if (this.IsLightweight || this.IsMobile)
			{
				htmlGenericControl.Attributes["title"] = text;
				WebControl child = IconHelper.CreateIcon(iconCssClass);
				htmlGenericControl.Controls.Add(child);
			}
			else
			{
				htmlGenericControl.InnerHtml = text;
				htmlGenericControl.Attributes["style"] = "z-index:80;";
			}
			return htmlGenericControl;
		}

		// Token: 0x04001340 RID: 4928
		private readonly int _index;

		// Token: 0x04001341 RID: 4929
		private readonly int _roundedCornersHeight;

		// Token: 0x04001342 RID: 4930
		private WebControl _outerWrap;

		// Token: 0x04001343 RID: 4931
		private WebControl _middleWrap;

		// Token: 0x04001344 RID: 4932
		private WebControl _innerWrap;

		// Token: 0x04001345 RID: 4933
		private WebControl _contentWrap;

		// Token: 0x04001346 RID: 4934
		internal Appointment _appointment;

		// Token: 0x04001347 RID: 4935
		internal DayViewBlockColumn _column;

		// Token: 0x04001348 RID: 4936
		internal bool _renderBottomArrow;

		// Token: 0x04001349 RID: 4937
		internal bool _renderLeftArrow;

		// Token: 0x0400134A RID: 4938
		internal bool _renderStartResizeGrip;

		// Token: 0x0400134B RID: 4939
		internal bool _renderEndResizeGrip;

		// Token: 0x0400134C RID: 4940
		internal bool _renderRightArrow;

		// Token: 0x0400134D RID: 4941
		internal bool _renderTime;

		// Token: 0x0400134E RID: 4942
		internal bool _renderTopArrow;
	}
}
