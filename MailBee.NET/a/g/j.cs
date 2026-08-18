using System;
using System.Collections;
using System.Reflection;

namespace a.g
{
	// Token: 0x02000404 RID: 1028
	[DefaultMember("Item")]
	internal class j : DictionaryBase
	{
		// Token: 0x06002426 RID: 9254 RVA: 0x00099EB7 File Offset: 0x00098EB7
		public f b(string A_0)
		{
			return (f)base.Dictionary[A_0];
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00099ECA File Offset: 0x00098ECA
		public void b(string A_0, f A_1)
		{
			base.Dictionary[A_0] = A_1;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00099ED9 File Offset: 0x00098ED9
		public ICollection b()
		{
			return base.Dictionary.Keys;
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00099EE6 File Offset: 0x00098EE6
		public ICollection c()
		{
			return base.Dictionary.Values;
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00099EF3 File Offset: 0x00098EF3
		public bool c(string A_0)
		{
			return base.Dictionary.Contains(A_0);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00099F01 File Offset: 0x00098F01
		public void a(string A_0, f A_1)
		{
			base.Dictionary.Add(A_0, A_1);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00099F10 File Offset: 0x00098F10
		public void a(string A_0)
		{
			base.Dictionary.Remove(A_0);
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00099F1E File Offset: 0x00098F1E
		public object a()
		{
			return base.InnerHashtable.SyncRoot;
		}
	}
}
