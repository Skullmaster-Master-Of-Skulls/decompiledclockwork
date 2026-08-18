using System;

namespace System.Runtime
{
	// Token: 0x02000034 RID: 52
	internal abstract class TypedAsyncResult<T> : AsyncResult
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00005CB5 File Offset: 0x00003EB5
		public TypedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00006E2D File Offset: 0x0000502D
		public T Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006E35 File Offset: 0x00005035
		protected void Complete(T data, bool completedSynchronously)
		{
			this.data = data;
			base.Complete(completedSynchronously);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006E48 File Offset: 0x00005048
		public static T End(IAsyncResult result)
		{
			TypedAsyncResult<T> typedAsyncResult = AsyncResult.End<TypedAsyncResult<T>>(result);
			return typedAsyncResult.Data;
		}

		// Token: 0x040000CB RID: 203
		private T data;
	}
}
