using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001958 RID: 6488
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class RadDataPagerSliderField : RadDataPagerField
	{
		// Token: 0x0600FB41 RID: 64321 RVA: 0x00389B88 File Offset: 0x00387D88
		public override void InitializeFieldControls(RadDataPagerFieldItem inItem)
		{
			this.slider = new RadSlider();
			this.slider.RenderMode = base.Owner.RenderMode;
			this.slider.ID = "PageSlider";
			this.slider.MinimumValue = 1m;
			this.slider.MaximumValue = Math.Max(base.Owner.PageCount, 1);
			int value = base.Owner.CurrentPageIndex + 1;
			this.slider.Value = Math.Min(value, this.slider.MaximumValue);
			this.slider.ValueChanged += this.SliderValueChanged;
			this.slider.PreRender += delegate(object sender, EventArgs e)
			{
				((RadSlider)sender).Skin = base.Owner.RuntimeSkin;
			};
			this.slider.AutoPostBack = true;
			this.slider.DragText = this.SliderDragText;
			this.slider.DecreaseText = this.SliderDecreaseText;
			this.slider.IncreaseText = this.SliderIncreaseText;
			this.slider.Orientation = this.SliderOrientation;
			this.PrepareSkinnableControlProperties(this.slider);
			inItem.Controls.Add(this.slider);
			Label label = new Label();
			label.CssClass = "rdpSliderLabel";
			label.Text = string.Format(this.LabelTextFormat, base.Owner.CurrentPageIndex + 1, base.Owner.PageCount);
			inItem.Controls.Add(label);
		}

		// Token: 0x0600FB42 RID: 64322 RVA: 0x00389D14 File Offset: 0x00387F14
		protected virtual void SliderValueChanged(object sender, EventArgs e)
		{
			RadDataPagerCommandEventArgs commandArgs = new RadDataPagerCommandEventArgs(base.Owner, (RadDataPagerFieldItem)this.slider.NamingContainer, this.slider, new CommandEventArgs("Page", (--this.slider.Value).ToString()));
			base.Owner.FireCommand(commandArgs);
		}

		// Token: 0x17004BEC RID: 19436
		// (get) Token: 0x0600FB43 RID: 64323 RVA: 0x00389D71 File Offset: 0x00387F71
		// (set) Token: 0x0600FB44 RID: 64324 RVA: 0x00389DA7 File Offset: 0x00387FA7
		[NotifyParentProperty(true)]
		[DefaultValue("Drag")]
		public string SliderDragText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["SliderDragText"], "Drag") ?? base.Owner.Localization.SliderDragText;
			}
			set
			{
				base.ViewState["SliderDragText"] = value;
			}
		}

		// Token: 0x17004BED RID: 19437
		// (get) Token: 0x0600FB45 RID: 64325 RVA: 0x00389DBA File Offset: 0x00387FBA
		// (set) Token: 0x0600FB46 RID: 64326 RVA: 0x00389DF0 File Offset: 0x00387FF0
		[NotifyParentProperty(true)]
		[DefaultValue("Decrease")]
		public string SliderDecreaseText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["SliderDecreaseText"], "Decrease") ?? base.Owner.Localization.SliderDecreaseText;
			}
			set
			{
				base.ViewState["SliderDecreaseText"] = value;
			}
		}

		// Token: 0x17004BEE RID: 19438
		// (get) Token: 0x0600FB47 RID: 64327 RVA: 0x00389E03 File Offset: 0x00388003
		// (set) Token: 0x0600FB48 RID: 64328 RVA: 0x00389E39 File Offset: 0x00388039
		[DefaultValue("Increase")]
		[NotifyParentProperty(true)]
		public string SliderIncreaseText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["SliderIncreaseText"], "Increase") ?? base.Owner.Localization.SliderIncreaseText;
			}
			set
			{
				base.ViewState["SliderIncreaseText"] = value;
			}
		}

		// Token: 0x17004BEF RID: 19439
		// (get) Token: 0x0600FB49 RID: 64329 RVA: 0x00389E4C File Offset: 0x0038804C
		// (set) Token: 0x0600FB4A RID: 64330 RVA: 0x00389E75 File Offset: 0x00388075
		[DefaultValue(Orientation.Horizontal)]
		[NotifyParentProperty(true)]
		public Orientation SliderOrientation
		{
			get
			{
				object obj = base.ViewState["SliderOrientation"];
				if (obj == null)
				{
					return Orientation.Horizontal;
				}
				return (Orientation)obj;
			}
			set
			{
				base.ViewState["SliderOrientation"] = value;
			}
		}

		// Token: 0x17004BF0 RID: 19440
		// (get) Token: 0x0600FB4B RID: 64331 RVA: 0x00389E90 File Offset: 0x00388090
		// (set) Token: 0x0600FB4C RID: 64332 RVA: 0x00389F00 File Offset: 0x00388100
		[DefaultValue("Page <strong>{0}</strong> of <strong>{1}</strong>")]
		[NotifyParentProperty(true)]
		public string LabelTextFormat
		{
			get
			{
				object obj = base.ViewState["LabelTextFormat"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Format("{0} <strong>{2}</strong> {1} <strong>{3}</strong>", new object[]
				{
					base.Owner.Localization.CurrentPageText,
					base.Owner.Localization.TotalPageText,
					"{0}",
					"{1}"
				});
			}
			set
			{
				base.ViewState["LabelTextFormat"] = value;
			}
		}

		// Token: 0x0400476D RID: 18285
		protected const string SliderLabelClassName = "rdpSliderLabel";

		// Token: 0x0400476E RID: 18286
		private RadSlider slider;
	}
}
