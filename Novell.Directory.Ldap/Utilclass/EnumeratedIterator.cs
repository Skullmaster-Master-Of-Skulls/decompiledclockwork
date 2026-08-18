using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000EF RID: 239
	public class EnumeratedIterator : IEnumerator
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x0001C278 File Offset: 0x0001B278
		public virtual bool MoveNext()
		{
			bool flag = this.hasMoreElements();
			if (flag)
			{
				this.tempAuxObj = this.nextElement();
			}
			return flag;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001C2A0 File Offset: 0x0001B2A0
		public virtual void Reset()
		{
			this.tempAuxObj = null;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001C2B4 File Offset: 0x0001B2B4
		public virtual object Current
		{
			get
			{
				return this.tempAuxObj;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001C2CC File Offset: 0x0001B2CC
		public EnumeratedIterator(IEnumerator iterator)
		{
			this.i = iterator;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001C2E8 File Offset: 0x0001B2E8
		public bool hasMoreElements()
		{
			return this.i.MoveNext();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001C304 File Offset: 0x0001B304
		public object nextElement()
		{
			return this.i.Current;
		}

		// Token: 0x04000444 RID: 1092
		private object tempAuxObj;

		// Token: 0x04000445 RID: 1093
		private IEnumerator i;
	}
}
