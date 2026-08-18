using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000E9 RID: 233
	public class ArrayEnumeration : IEnumerator
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x0001AC20 File Offset: 0x00019C20
		public virtual bool MoveNext()
		{
			bool flag = this.hasMoreElements();
			if (flag)
			{
				this.tempAuxObj = this.nextElement();
			}
			return flag;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001AC48 File Offset: 0x00019C48
		public virtual void Reset()
		{
			this.tempAuxObj = null;
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001AC5C File Offset: 0x00019C5C
		public virtual object Current
		{
			get
			{
				return this.tempAuxObj;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001AC74 File Offset: 0x00019C74
		public ArrayEnumeration(object[] eArray)
		{
			this.eArray = eArray;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001AC98 File Offset: 0x00019C98
		public bool hasMoreElements()
		{
			return this.eArray != null && this.index < this.eArray.Length;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001ACC8 File Offset: 0x00019CC8
		public object nextElement()
		{
			if (this.eArray == null || this.index >= this.eArray.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.eArray[this.index++];
		}

		// Token: 0x04000426 RID: 1062
		private object tempAuxObj;

		// Token: 0x04000427 RID: 1063
		private object[] eArray;

		// Token: 0x04000428 RID: 1064
		private int index = 0;
	}
}
