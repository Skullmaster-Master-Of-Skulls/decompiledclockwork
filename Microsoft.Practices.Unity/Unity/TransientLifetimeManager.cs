using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200008E RID: 142
	public class TransientLifetimeManager : LifetimeManager
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000862B File Offset: 0x0000682B
		public override object GetValue()
		{
			return null;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000862E File Offset: 0x0000682E
		public override void SetValue(object newValue)
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00008630 File Offset: 0x00006830
		public override void RemoveValue()
		{
		}
	}
}
