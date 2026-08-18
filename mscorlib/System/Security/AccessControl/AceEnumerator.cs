using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x02000904 RID: 2308
	public sealed class AceEnumerator : IEnumerator
	{
		// Token: 0x06005378 RID: 21368 RVA: 0x0012E077 File Offset: 0x0012D077
		internal AceEnumerator(GenericAcl collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._acl = collection;
			this.Reset();
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06005379 RID: 21369 RVA: 0x0012E09A File Offset: 0x0012D09A
		object IEnumerator.Current
		{
			get
			{
				if (this._current == -1 || this._current >= this._acl.Count)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_InvalidOperationException"));
				}
				return this._acl[this._current];
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x0600537A RID: 21370 RVA: 0x0012E0D9 File Offset: 0x0012D0D9
		public GenericAce Current
		{
			get
			{
				return ((IEnumerator)this).Current as GenericAce;
			}
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x0012E0E6 File Offset: 0x0012D0E6
		public bool MoveNext()
		{
			this._current++;
			return this._current < this._acl.Count;
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x0012E109 File Offset: 0x0012D109
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x04002B50 RID: 11088
		private int _current;

		// Token: 0x04002B51 RID: 11089
		private readonly GenericAcl _acl;
	}
}
