using System;
using System.Collections;

namespace log4net.Util
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	public sealed class EmptyCollection : ICollection, IEnumerable
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x000161E4 File Offset: 0x000143E4
		private EmptyCollection()
		{
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x000161EC File Offset: 0x000143EC
		public static EmptyCollection Instance
		{
			get
			{
				return EmptyCollection.s_instance;
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000161F3 File Offset: 0x000143F3
		public void CopyTo(Array array, int index)
		{
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x000161F5 File Offset: 0x000143F5
		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x000161F8 File Offset: 0x000143F8
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x000161FB File Offset: 0x000143FB
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000161FE File Offset: 0x000143FE
		public IEnumerator GetEnumerator()
		{
			return NullEnumerator.Instance;
		}

		// Token: 0x040002AB RID: 683
		private static readonly EmptyCollection s_instance = new EmptyCollection();
	}
}
