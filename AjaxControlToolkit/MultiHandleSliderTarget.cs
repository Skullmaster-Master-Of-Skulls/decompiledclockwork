using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x02000148 RID: 328
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class MultiHandleSliderTarget
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x000171C4 File Offset: 0x000153C4
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x000171CC File Offset: 0x000153CC
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(WebControl))]
		[NotifyParentProperty(true)]
		[Description("Sets the ID of the control that is bound to the location of this handle.")]
		public string ControlID
		{
			get
			{
				return this._controlID;
			}
			set
			{
				this._controlID = value;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x000171D5 File Offset: 0x000153D5
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x000171DD File Offset: 0x000153DD
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[ExtenderControlProperty]
		[Description("Sets the style of the handle associated with the MultiHandleSliderTarget, if custom styles are used.")]
		public string HandleCssClass
		{
			get
			{
				return this._handleCssClass;
			}
			set
			{
				this._handleCssClass = value;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x000171E6 File Offset: 0x000153E6
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x000171EE File Offset: 0x000153EE
		[Description("Sets the number of decimal places to store with the value.")]
		[ExtenderControlProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		public int Decimals
		{
			get
			{
				return this._decimals;
			}
			set
			{
				this._decimals = value;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000171F7 File Offset: 0x000153F7
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x000171FF File Offset: 0x000153FF
		[ExtenderControlProperty]
		[NotifyParentProperty(true)]
		[Description("Sets the number of pixels to offset the width of the handle, for handles with transparent space.")]
		[DefaultValue(0)]
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		// Token: 0x04000368 RID: 872
		private string _controlID;

		// Token: 0x04000369 RID: 873
		private string _handleCssClass;

		// Token: 0x0400036A RID: 874
		private int _decimals;

		// Token: 0x0400036B RID: 875
		private int _offset;
	}
}
