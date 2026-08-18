using System;
using System.Collections;

namespace a.b
{
	// Token: 0x0200030F RID: 783
	internal class g6 : h2
	{
		// Token: 0x06001BF1 RID: 7153 RVA: 0x0007ADA4 File Offset: 0x00079DA4
		public g6()
		{
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0007ADB8 File Offset: 0x00079DB8
		public g6(h2 A_0)
		{
			foreach (object o in A_0)
			{
				this.Add(o);
			}
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0007AE18 File Offset: 0x00079E18
		public void Add(object o)
		{
			this.a[o] = null;
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0007AE27 File Offset: 0x00079E27
		public bool oj(object A_0)
		{
			return this.a.ContainsKey(A_0);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0007AE35 File Offset: 0x00079E35
		public void CopyTo(Array array, int index)
		{
			this.a.Keys.CopyTo(array, index);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0007AE49 File Offset: 0x00079E49
		public int get_Count()
		{
			return this.a.Count;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0007AE56 File Offset: 0x00079E56
		public IEnumerator GetEnumerator()
		{
			return this.a.Keys.GetEnumerator();
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0007AE68 File Offset: 0x00079E68
		public bool get_IsSynchronized()
		{
			return this.a.IsSynchronized;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0007AE75 File Offset: 0x00079E75
		public void ok(object A_0)
		{
			this.a.Remove(A_0);
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0007AE83 File Offset: 0x00079E83
		public object get_SyncRoot()
		{
			return this.a.SyncRoot;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0007AE90 File Offset: 0x00079E90
		public void ol()
		{
			this.a.Clear();
		}

		// Token: 0x04001347 RID: 4935
		private readonly Hashtable a = new Hashtable();
	}
}
