using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200068E RID: 1678
	[Serializable]
	internal class CaptureEnumerator : IEnumerator
	{
		// Token: 0x06003E04 RID: 15876 RVA: 0x000FE18B File Offset: 0x000FC38B
		internal CaptureEnumerator(CaptureCollection rcc)
		{
			this._curindex = -1;
			this._rcc = rcc;
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000FE1A4 File Offset: 0x000FC3A4
		public bool MoveNext()
		{
			int count = this._rcc.Count;
			if (this._curindex >= count)
			{
				return false;
			}
			this._curindex++;
			return this._curindex < count;
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x000FE1DF File Offset: 0x000FC3DF
		public object Current
		{
			get
			{
				return this.Capture;
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003E07 RID: 15879 RVA: 0x000FE1E7 File Offset: 0x000FC3E7
		public Capture Capture
		{
			get
			{
				if (this._curindex < 0 || this._curindex >= this._rcc.Count)
				{
					throw new InvalidOperationException(SR.GetString("EnumNotStarted"));
				}
				return this._rcc[this._curindex];
			}
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000FE226 File Offset: 0x000FC426
		public void Reset()
		{
			this._curindex = -1;
		}

		// Token: 0x04002D08 RID: 11528
		internal CaptureCollection _rcc;

		// Token: 0x04002D09 RID: 11529
		internal int _curindex;
	}
}
