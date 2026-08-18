using System;

namespace System.Web.UI
{
	// Token: 0x02000301 RID: 769
	public sealed class StateItem
	{
		// Token: 0x0600238A RID: 9098 RVA: 0x00073C48 File Offset: 0x00071E48
		internal StateItem(object initialValue)
		{
			this.value = initialValue;
			this.isDirty = false;
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x00073C5E File Offset: 0x00071E5E
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x00073C66 File Offset: 0x00071E66
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
			set
			{
				this.isDirty = value;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x00073C6F File Offset: 0x00071E6F
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x00073C77 File Offset: 0x00071E77
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04001CC3 RID: 7363
		private object value;

		// Token: 0x04001CC4 RID: 7364
		private bool isDirty;
	}
}
