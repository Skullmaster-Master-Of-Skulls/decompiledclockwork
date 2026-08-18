using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EC8 RID: 3784
	[ToolboxItem(false)]
	public class Opacity : ImageEditorDialog
	{
		// Token: 0x06009042 RID: 36930 RVA: 0x002078C5 File Offset: 0x00205AC5
		public Opacity(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002DB0 RID: 11696
		// (get) Token: 0x06009043 RID: 36931 RVA: 0x002078CF File Offset: 0x00205ACF
		public override string DialogName
		{
			get
			{
				return "Opacity";
			}
		}

		// Token: 0x17002DB1 RID: 11697
		// (get) Token: 0x06009044 RID: 36932 RVA: 0x002078D6 File Offset: 0x00205AD6
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002DB2 RID: 11698
		// (get) Token: 0x06009045 RID: 36933 RVA: 0x002078DD File Offset: 0x00205ADD
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Opacity_Title;
			}
		}

		// Token: 0x06009046 RID: 36934 RVA: 0x002078F4 File Offset: 0x00205AF4
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			bool flag = base.ParentImageEditor.RuntimeSkin == "MetroTouch";
			this._sliderOpacity = (RadSlider)base.FindControlRecursive("SliderOpacity");
			if (this._sliderOpacity != null)
			{
				this._sliderOpacity.DecreaseText = dialogs.Common_Decrease;
				this._sliderOpacity.DragText = dialogs.Common_SliderDrag;
				this._sliderOpacity.IncreaseText = dialogs.Common_Increase;
				if (flag)
				{
					this._sliderOpacity.Width = Unit.Pixel(240);
				}
				base.SetChildControlRenderMode(this._sliderOpacity);
			}
			this._txtOpacity = (TextBox)base.FindControlRecursive("TxtOpacity");
			if (this._txtOpacity != null)
			{
				this._txtOpacity.ToolTip = dialogs.Opacity_TextboxToolTip;
			}
		}

		// Token: 0x04002839 RID: 10297
		private RadSlider _sliderOpacity;

		// Token: 0x0400283A RID: 10298
		private TextBox _txtOpacity;
	}
}
