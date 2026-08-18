using System;

namespace System.Web.Configuration
{
	// Token: 0x0200069E RID: 1694
	public enum AuthenticationMode
	{
		// Token: 0x04002B08 RID: 11016
		None,
		// Token: 0x04002B09 RID: 11017
		Windows,
		// Token: 0x04002B0A RID: 11018
		[Obsolete("This field is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
		Passport,
		// Token: 0x04002B0B RID: 11019
		Forms
	}
}
