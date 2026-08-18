using System;
using System.Collections.Generic;

namespace ImportExportClassLibrary
{
	// Token: 0x02000004 RID: 4
	public class TemplateInDatabaseCollection : List<TemplateInDatabase>
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002AA5 File Offset: 0x00001AA5
		public TemplateInDatabaseCollection()
		{
			this.exception = null;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002AB4 File Offset: 0x00001AB4
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002ABC File Offset: 0x00001ABC
		public TemplateInDatabaseCollection(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002ADE File Offset: 0x00001ADE
		public new void Sort()
		{
			base.Sort((TemplateInDatabase t1, TemplateInDatabase t2) => t1.Name.CompareTo(t2.Name));
		}

		// Token: 0x0400000E RID: 14
		private Exception exception;
	}
}
