using System;

namespace ClockWorkAPI.Collection
{
	// Token: 0x0200003A RID: 58
	public class NodeTreeDataEventArgs<T> : EventArgs
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x000113B8 File Offset: 0x000103B8
		public T Data
		{
			get
			{
				return this._Data;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000113D0 File Offset: 0x000103D0
		public NodeTreeDataEventArgs(T data)
		{
			this._Data = data;
		}

		// Token: 0x0400017C RID: 380
		private T _Data = default(T);
	}
}
