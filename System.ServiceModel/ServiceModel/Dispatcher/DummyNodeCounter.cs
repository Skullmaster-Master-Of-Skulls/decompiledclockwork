using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004F1 RID: 1265
	internal class DummyNodeCounter : INodeCounter
	{
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000B781F File Offset: 0x000B5A1F
		// (set) Token: 0x06002FC6 RID: 12230 RVA: 0x000B7822 File Offset: 0x000B5A22
		public int CounterMarker
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (set) Token: 0x06002FC7 RID: 12231 RVA: 0x000B7824 File Offset: 0x000B5A24
		public int MaxCounter
		{
			set
			{
			}
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000B7826 File Offset: 0x000B5A26
		public int ElapsedCount(int marker)
		{
			return 0;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000B7829 File Offset: 0x000B5A29
		public void Increase()
		{
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000B782B File Offset: 0x000B5A2B
		public void IncreaseBy(int count)
		{
		}

		// Token: 0x040025E8 RID: 9704
		internal static DummyNodeCounter Dummy = new DummyNodeCounter();
	}
}
