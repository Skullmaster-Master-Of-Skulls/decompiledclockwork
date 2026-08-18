using System;
using System.Web.Configuration;

namespace System.Web.UI
{
	// Token: 0x0200004C RID: 76
	internal sealed class CustomErrorsSectionWrapper : ICustomErrorsSection
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x00011D8A File Offset: 0x0000FF8A
		public CustomErrorsSectionWrapper(CustomErrorsSection customErrorsSection)
		{
			this._customErrorsSection = customErrorsSection;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00011D99 File Offset: 0x0000FF99
		string ICustomErrorsSection.DefaultRedirect
		{
			get
			{
				return this._customErrorsSection.DefaultRedirect;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00011DA6 File Offset: 0x0000FFA6
		CustomErrorCollection ICustomErrorsSection.Errors
		{
			get
			{
				return this._customErrorsSection.Errors;
			}
		}

		// Token: 0x04000113 RID: 275
		private readonly CustomErrorsSection _customErrorsSection;
	}
}
