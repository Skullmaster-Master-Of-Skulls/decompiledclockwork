using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000F2 RID: 242
	internal class FetchArrayPooler
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x000582DC File Offset: 0x000572DC
		internal IntPtr GetFetchArray(IntPtr id, out int bRequiresDefine)
		{
			IntPtr intPtr = IntPtr.Zero;
			lock (this.m_htIdtoFA.SyncRoot)
			{
				if (this.m_htIdtoFA.ContainsKey(id))
				{
					intPtr = id;
					this.m_htIdtoFA.Remove(id);
					bRequiresDefine = 0;
					return intPtr;
				}
				bRequiresDefine = 1;
				IDictionaryEnumerator enumerator = this.m_htIdtoFA.GetEnumerator();
				if (enumerator.MoveNext())
				{
					intPtr = (IntPtr)enumerator.Value;
					this.m_htIdtoFA.Remove(intPtr);
					return intPtr;
				}
			}
			return intPtr;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00058390 File Offset: 0x00057390
		internal void PutFetchArray(IntPtr pFetchArray)
		{
			bool flag = false;
			if (pFetchArray != IntPtr.Zero)
			{
				if (this.m_htIdtoFA.Count < 3)
				{
					lock (this.m_htIdtoFA.SyncRoot)
					{
						if (this.m_htIdtoFA.Count < 3)
						{
							this.m_htIdtoFA.Add(pFetchArray, pFetchArray);
							flag = true;
						}
					}
				}
				if (!flag)
				{
					Marshal.FreeCoTaskMem(pFetchArray);
				}
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0005841C File Offset: 0x0005741C
		internal FetchArrayPooler()
		{
			this.m_htIdtoFA = new Hashtable();
			this.m_pFetchArrayGet = new FetchArrayGetCallbackFuncPtr(this.GetFetchArray);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00058444 File Offset: 0x00057444
		internal void ReSizeFetchArrayPooler(int capacity)
		{
			int num = 0;
			ArrayList arrayList = new ArrayList();
			lock (this.m_htIdtoFA.SyncRoot)
			{
				foreach (object obj in this.m_htIdtoFA.Keys)
				{
					IntPtr intPtr = (IntPtr)obj;
					if (++num > capacity)
					{
						arrayList.Add(intPtr);
					}
				}
				foreach (object obj2 in arrayList)
				{
					IntPtr intPtr2 = (IntPtr)obj2;
					this.m_htIdtoFA.Remove(intPtr2);
					Marshal.FreeCoTaskMem(intPtr2);
				}
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0005854C File Offset: 0x0005754C
		internal void Dispose()
		{
			this.ReSizeFetchArrayPooler(0);
		}

		// Token: 0x0400079E RID: 1950
		internal const int MinFetchArrayPoolerSize = 1;

		// Token: 0x0400079F RID: 1951
		internal const int MaxFetchArrayPoolerSize = 3;

		// Token: 0x040007A0 RID: 1952
		internal FetchArrayGetCallbackFuncPtr m_pFetchArrayGet;

		// Token: 0x040007A1 RID: 1953
		private Hashtable m_htIdtoFA;
	}
}
