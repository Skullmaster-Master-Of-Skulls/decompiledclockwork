using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x0200000A RID: 10
	internal struct DisposableEnumeratorAdapter<T, TEnumerator> : IDisposable where TEnumerator : struct, IEnumerator<T>
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002C14 File Offset: 0x00000E14
		internal DisposableEnumeratorAdapter(TEnumerator enumerator)
		{
			this._enumeratorStruct = enumerator;
			this._enumeratorObject = null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C24 File Offset: 0x00000E24
		internal DisposableEnumeratorAdapter(IEnumerator<T> enumerator)
		{
			this._enumeratorStruct = default(TEnumerator);
			this._enumeratorObject = enumerator;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002C39 File Offset: 0x00000E39
		public T Current
		{
			get
			{
				if (this._enumeratorObject == null)
				{
					return this._enumeratorStruct.Current;
				}
				return this._enumeratorObject.Current;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C60 File Offset: 0x00000E60
		public bool MoveNext()
		{
			if (this._enumeratorObject == null)
			{
				return this._enumeratorStruct.MoveNext();
			}
			return this._enumeratorObject.MoveNext();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C87 File Offset: 0x00000E87
		public void Dispose()
		{
			if (this._enumeratorObject != null)
			{
				this._enumeratorObject.Dispose();
				return;
			}
			this._enumeratorStruct.Dispose();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CAE File Offset: 0x00000EAE
		public DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerator()
		{
			return this;
		}

		// Token: 0x04000006 RID: 6
		private readonly IEnumerator<T> _enumeratorObject;

		// Token: 0x04000007 RID: 7
		private TEnumerator _enumeratorStruct;
	}
}
