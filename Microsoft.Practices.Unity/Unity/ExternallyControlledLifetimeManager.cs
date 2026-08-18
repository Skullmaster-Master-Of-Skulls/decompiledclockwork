using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200008B RID: 139
	public class ExternallyControlledLifetimeManager : LifetimeManager
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000851C File Offset: 0x0000671C
		public override object GetValue()
		{
			return this.value.Target;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00008529 File Offset: 0x00006729
		public override void SetValue(object newValue)
		{
			this.value = new WeakReference(newValue);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00008537 File Offset: 0x00006737
		public override void RemoveValue()
		{
		}

		// Token: 0x0400008C RID: 140
		private WeakReference value = new WeakReference(null);
	}
}
