using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020016C6 RID: 5830
	internal class CaptchaBaseValidator : CustomValidator
	{
		// Token: 0x0600E102 RID: 57602 RVA: 0x0031FC35 File Offset: 0x0031DE35
		protected override bool EvaluateIsValid()
		{
			if (!this.isAlreadyValidated)
			{
				this.isAlreadyValidated = true;
				this.initialValue = this.ParentCaptcha.EvaluateIsValid();
			}
			return this.initialValue;
		}

		// Token: 0x170044F7 RID: 17655
		// (get) Token: 0x0600E103 RID: 57603 RVA: 0x0031FC5D File Offset: 0x0031DE5D
		// (set) Token: 0x0600E104 RID: 57604 RVA: 0x0031FC65 File Offset: 0x0031DE65
		public override string ValidationGroup
		{
			get
			{
				return this._validationGroup;
			}
			set
			{
				this._validationGroup = value;
			}
		}

		// Token: 0x170044F8 RID: 17656
		// (get) Token: 0x0600E105 RID: 57605 RVA: 0x0031FC6E File Offset: 0x0031DE6E
		// (set) Token: 0x0600E106 RID: 57606 RVA: 0x0031FC76 File Offset: 0x0031DE76
		internal RadCaptcha ParentCaptcha
		{
			get
			{
				return this._parentCaptcha;
			}
			set
			{
				this._parentCaptcha = value;
			}
		}

		// Token: 0x04004112 RID: 16658
		private bool isAlreadyValidated;

		// Token: 0x04004113 RID: 16659
		private bool initialValue;

		// Token: 0x04004114 RID: 16660
		private string _validationGroup;

		// Token: 0x04004115 RID: 16661
		private RadCaptcha _parentCaptcha;
	}
}
