using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EB3 RID: 3763
	[ToolboxItem(false)]
	public class Zoom : ImageEditorDialog
	{
		// Token: 0x06008F53 RID: 36691 RVA: 0x00204B55 File Offset: 0x00202D55
		public Zoom(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002D53 RID: 11603
		// (get) Token: 0x06008F54 RID: 36692 RVA: 0x00204B5F File Offset: 0x00202D5F
		public override string DialogName
		{
			get
			{
				return "Zoom";
			}
		}

		// Token: 0x17002D54 RID: 11604
		// (get) Token: 0x06008F55 RID: 36693 RVA: 0x00204B66 File Offset: 0x00202D66
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002D55 RID: 11605
		// (get) Token: 0x06008F56 RID: 36694 RVA: 0x00204B6D File Offset: 0x00202D6D
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Zoom_Title;
			}
		}

		// Token: 0x06008F57 RID: 36695 RVA: 0x00204B84 File Offset: 0x00202D84
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			bool flag = base.IsTouchSkin();
			this._sliderZoom = (RadSlider)base.FindControlRecursive("sliderZoom");
			if (this._sliderZoom != null)
			{
				this._sliderZoom.DecreaseText = dialogs.Common_Decrease;
				this._sliderZoom.DragText = dialogs.Common_SliderDrag;
				this._sliderZoom.IncreaseText = dialogs.Common_Increase;
				this._sliderZoom.MinimumValue = base.ParentImageEditor.LowerZoomBound;
				this._sliderZoom.MaximumValue = base.ParentImageEditor.UpperZoomBound;
				if (flag)
				{
					this._sliderZoom.Width = Unit.Pixel(240);
				}
				base.SetChildControlRenderMode(this._sliderZoom);
			}
			this._txtZoom = (TextBox)base.FindControlRecursive("TxtZoom");
			if (this._txtZoom != null)
			{
				this._txtZoom.ToolTip = dialogs.Zoom_TextboxToolTip;
			}
			this._btnActualSize = (RadButton)base.FindControlRecursive("btnActualSize");
			if (this._btnActualSize != null)
			{
				this._btnActualSize.ToolTip = dialogs.Zoom_ActualSizeToolTip;
				if (base.ParentImageEditor.RenderMode == RenderMode.Classic)
				{
					this._btnActualSize.Width = Unit.Pixel(22);
					this._btnActualSize.Icon.PrimaryIconLeft = Unit.Pixel(4);
					this._btnActualSize.Icon.PrimaryIconTop = Unit.Pixel(4);
					if (flag)
					{
						this._btnActualSize.Width = Unit.Pixel(30);
						this._btnActualSize.Icon.PrimaryIconLeft = Unit.Pixel(10);
						this._btnActualSize.Icon.PrimaryIconTop = Unit.Pixel(9);
					}
				}
				base.SetChildControlRenderMode(this._btnActualSize);
			}
			this._lblActualSize = (HtmlGenericControl)base.FindControlRecursive("lblActualSize");
			if (this._lblActualSize != null)
			{
				this._lblActualSize.InnerText = dialogs.Common_ActualSize;
			}
			this._btnFitImage = (RadButton)base.FindControlRecursive("btnFitImage");
			if (this._btnFitImage != null)
			{
				this._btnFitImage.ToolTip = dialogs.Zoom_BestFitToolTip;
				if (base.ParentImageEditor.RenderMode == RenderMode.Classic)
				{
					this._btnFitImage.Width = Unit.Pixel(22);
					this._btnFitImage.Icon.PrimaryIconLeft = Unit.Pixel(4);
					this._btnFitImage.Icon.PrimaryIconTop = Unit.Pixel(4);
					if (flag)
					{
						this._btnFitImage.Width = Unit.Pixel(30);
						this._btnFitImage.Icon.PrimaryIconLeft = Unit.Pixel(10);
						this._btnFitImage.Icon.PrimaryIconTop = Unit.Pixel(9);
					}
				}
				base.SetChildControlRenderMode(this._btnFitImage);
			}
			this._lblBestFit = (HtmlGenericControl)base.FindControlRecursive("lblBestFit");
			if (this._lblBestFit != null)
			{
				this._lblBestFit.InnerText = dialogs.Common_BestFit;
			}
		}

		// Token: 0x06008F58 RID: 36696 RVA: 0x00204E8C File Offset: 0x0020308C
		protected override void Render(HtmlTextWriter writer)
		{
			if (this._lblActualSize != null && this._btnActualSize != null)
			{
				this._lblActualSize.Attributes.Add("for", this._btnActualSize.ClientID + "_input");
			}
			if (this._lblBestFit != null && this._btnFitImage != null)
			{
				this._lblBestFit.Attributes.Add("for", this._btnFitImage.ClientID + "_input");
			}
			base.Render(writer);
		}

		// Token: 0x040027E7 RID: 10215
		private RadSlider _sliderZoom;

		// Token: 0x040027E8 RID: 10216
		private TextBox _txtZoom;

		// Token: 0x040027E9 RID: 10217
		private RadButton _btnActualSize;

		// Token: 0x040027EA RID: 10218
		private RadButton _btnFitImage;

		// Token: 0x040027EB RID: 10219
		private HtmlGenericControl _lblActualSize;

		// Token: 0x040027EC RID: 10220
		private HtmlGenericControl _lblBestFit;
	}
}
