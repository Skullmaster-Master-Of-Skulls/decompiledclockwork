using System;
using System.Web.UI;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000487 RID: 1159
	internal class PersistedControl
	{
		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x00085722 File Offset: 0x00083922
		// (set) Token: 0x06002952 RID: 10578 RVA: 0x0008572A File Offset: 0x0008392A
		public Control Control { get; set; }

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x00085733 File Offset: 0x00083933
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x0008573B File Offset: 0x0008393B
		public string Prefix { get; set; }
	}
}
