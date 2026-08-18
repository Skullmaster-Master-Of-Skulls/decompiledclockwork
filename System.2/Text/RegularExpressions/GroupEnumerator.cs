using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200069A RID: 1690
	internal class GroupEnumerator : IEnumerator
	{
		// Token: 0x06003EE0 RID: 16096 RVA: 0x00105C60 File Offset: 0x00103E60
		internal GroupEnumerator(GroupCollection rgc)
		{
			this._curindex = -1;
			this._rgc = rgc;
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x00105C78 File Offset: 0x00103E78
		public bool MoveNext()
		{
			int count = this._rgc.Count;
			if (this._curindex >= count)
			{
				return false;
			}
			this._curindex++;
			return this._curindex < count;
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x00105CB3 File Offset: 0x00103EB3
		public object Current
		{
			get
			{
				return this.Capture;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x00105CBB File Offset: 0x00103EBB
		public Capture Capture
		{
			get
			{
				if (this._curindex < 0 || this._curindex >= this._rgc.Count)
				{
					throw new InvalidOperationException(SR.GetString("EnumNotStarted"));
				}
				return this._rgc[this._curindex];
			}
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x00105CFA File Offset: 0x00103EFA
		public void Reset()
		{
			this._curindex = -1;
		}

		// Token: 0x04002DE6 RID: 11750
		internal GroupCollection _rgc;

		// Token: 0x04002DE7 RID: 11751
		internal int _curindex;
	}
}
