using System;
using System.Collections;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004CC RID: 1228
	internal class NodeSequenceEnumerator : IEnumerator
	{
		// Token: 0x06002EA9 RID: 11945 RVA: 0x000B50E7 File Offset: 0x000B32E7
		internal NodeSequenceEnumerator(NodeSequenceIterator iter)
		{
			this.iter = new NodeSequenceIterator(iter);
			this.Reset();
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002EAA RID: 11946 RVA: 0x000B5104 File Offset: 0x000B3304
		public object Current
		{
			get
			{
				if (this.iter.CurrentPosition == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("QueryBeforeNodes")));
				}
				if (this.iter.CurrentPosition > this.iter.Count)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("QueryAfterNodes")));
				}
				return this.iter.Current;
			}
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000B5175 File Offset: 0x000B3375
		public bool MoveNext()
		{
			return this.iter.MoveNext();
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000B5182 File Offset: 0x000B3382
		public void Reset()
		{
			this.iter.Reset();
		}

		// Token: 0x0400254F RID: 9551
		private NodeSequenceIterator iter;
	}
}
