using System;
using System.Collections.Generic;

namespace System.Web.WebPages.Html
{
	// Token: 0x02000086 RID: 134
	public class ModelState
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000CF12 File Offset: 0x0000B112
		public IList<string> Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000CF1A File Offset: 0x0000B11A
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000CF22 File Offset: 0x0000B122
		public object Value { get; set; }

		// Token: 0x04000127 RID: 295
		private List<string> _errors = new List<string>();
	}
}
