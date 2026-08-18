using System;

namespace System.Web.Util
{
	// Token: 0x02000219 RID: 537
	public enum RequestValidationSource
	{
		// Token: 0x040017FC RID: 6140
		QueryString,
		// Token: 0x040017FD RID: 6141
		Form,
		// Token: 0x040017FE RID: 6142
		Cookies,
		// Token: 0x040017FF RID: 6143
		Files,
		// Token: 0x04001800 RID: 6144
		RawUrl,
		// Token: 0x04001801 RID: 6145
		Path,
		// Token: 0x04001802 RID: 6146
		PathInfo,
		// Token: 0x04001803 RID: 6147
		Headers
	}
}
