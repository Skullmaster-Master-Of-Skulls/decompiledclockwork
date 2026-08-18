using System;

namespace System.IdentityModel
{
	// Token: 0x020000B2 RID: 178
	public class TypedAsyncResult<T> : AsyncResult
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x0001474F File Offset: 0x0001294F
		public TypedAsyncResult(object state) : base(state)
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00014758 File Offset: 0x00012958
		public TypedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00014762 File Offset: 0x00012962
		public void Complete(T result, bool completedSynchronously)
		{
			this._result = result;
			base.Complete(completedSynchronously);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00014772 File Offset: 0x00012972
		public void Complete(T result, bool completedSynchronously, Exception exception)
		{
			this._result = result;
			base.Complete(completedSynchronously, exception);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00014784 File Offset: 0x00012984
		public new static T End(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			TypedAsyncResult<T> typedAsyncResult = result as TypedAsyncResult<T>;
			if (typedAsyncResult == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("result", SR.GetString("ID2004", new object[]
				{
					typeof(TypedAsyncResult<T>),
					result.GetType()
				}));
			}
			AsyncResult.End(typedAsyncResult);
			return typedAsyncResult.Result;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x000147F0 File Offset: 0x000129F0
		public T Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x040004C9 RID: 1225
		private T _result;
	}
}
