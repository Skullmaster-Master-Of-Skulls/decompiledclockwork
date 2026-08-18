using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000AFF RID: 2815
	[Obsolete]
	public class ODataEntitySet
	{
		// Token: 0x06006991 RID: 27025 RVA: 0x0018D0C5 File Offset: 0x0018B2C5
		public ODataEntitySet()
		{
		}

		// Token: 0x06006992 RID: 27026 RVA: 0x0018D0CD File Offset: 0x0018B2CD
		public ODataEntitySet(string name, string type)
		{
			this.Name = name;
			this.EntityType = type;
		}

		// Token: 0x17002293 RID: 8851
		// (get) Token: 0x06006993 RID: 27027 RVA: 0x0018D0E3 File Offset: 0x0018B2E3
		// (set) Token: 0x06006994 RID: 27028 RVA: 0x0018D0EB File Offset: 0x0018B2EB
		public string Name { get; set; }

		// Token: 0x17002294 RID: 8852
		// (get) Token: 0x06006995 RID: 27029 RVA: 0x0018D0F4 File Offset: 0x0018B2F4
		// (set) Token: 0x06006996 RID: 27030 RVA: 0x0018D0FC File Offset: 0x0018B2FC
		public string EntityType { get; set; }
	}
}
