using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000023 RID: 35
	public class PerResolveLifetimeManager : LifetimeManager
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000313D File Offset: 0x0000133D
		public PerResolveLifetimeManager()
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003145 File Offset: 0x00001345
		internal PerResolveLifetimeManager(object value)
		{
			this.value = value;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003154 File Offset: 0x00001354
		public override object GetValue()
		{
			return this.value;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000315C File Offset: 0x0000135C
		public override void SetValue(object newValue)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000315E File Offset: 0x0000135E
		public override void RemoveValue()
		{
		}

		// Token: 0x0400001B RID: 27
		private readonly object value;
	}
}
