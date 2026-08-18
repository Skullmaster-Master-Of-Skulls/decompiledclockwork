using System;
using System.Collections;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000371 RID: 881
	public class BehaviorServiceAdornerCollectionEnumerator : IEnumerator
	{
		// Token: 0x0600240F RID: 9231 RVA: 0x000E0644 File Offset: 0x000DE844
		public BehaviorServiceAdornerCollectionEnumerator(BehaviorServiceAdornerCollection mappings)
		{
			this.temp = mappings;
			this.baseEnumerator = this.temp.GetEnumerator();
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x000E0664 File Offset: 0x000DE864
		public Adorner Current
		{
			get
			{
				return (Adorner)this.baseEnumerator.Current;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x000E0676 File Offset: 0x000DE876
		object IEnumerator.Current
		{
			get
			{
				return this.baseEnumerator.Current;
			}
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000E0683 File Offset: 0x000DE883
		public bool MoveNext()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000E0683 File Offset: 0x000DE883
		bool IEnumerator.MoveNext()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000E0690 File Offset: 0x000DE890
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000E0690 File Offset: 0x000DE890
		void IEnumerator.Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x04001A4C RID: 6732
		private IEnumerator baseEnumerator;

		// Token: 0x04001A4D RID: 6733
		private IEnumerable temp;
	}
}
