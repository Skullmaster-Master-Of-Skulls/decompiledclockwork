using System;

namespace System.Web.UI
{
	// Token: 0x020002E3 RID: 739
	[Serializable]
	public sealed class Pair
	{
		// Token: 0x0600225F RID: 8799 RVA: 0x000030B5 File Offset: 0x000012B5
		public Pair()
		{
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000704AB File Offset: 0x0006E6AB
		public Pair(object x, object y)
		{
			this.First = x;
			this.Second = y;
		}

		// Token: 0x04001C3A RID: 7226
		public object First;

		// Token: 0x04001C3B RID: 7227
		public object Second;
	}
}
