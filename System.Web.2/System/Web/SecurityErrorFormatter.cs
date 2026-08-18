using System;

namespace System.Web
{
	// Token: 0x02000059 RID: 89
	internal class SecurityErrorFormatter : UnhandledErrorFormatter
	{
		// Token: 0x0600061E RID: 1566 RVA: 0x00009727 File Offset: 0x00007927
		internal SecurityErrorFormatter(Exception e) : base(e)
		{
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00009730 File Offset: 0x00007930
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Security_Err_Error");
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0000973C File Offset: 0x0000793C
		protected override string Description
		{
			get
			{
				return HttpUtility.FormatPlainTextAsHtml(SR.GetString("Security_Err_Desc"));
			}
		}
	}
}
