using System;
using System.Diagnostics;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200079A RID: 1946
	internal class RetryLazy<TInput, TResult> where TResult : class
	{
		// Token: 0x06005823 RID: 22563 RVA: 0x0017B2A0 File Offset: 0x001794A0
		public RetryLazy(Func<TInput, TResult> valueFactory)
		{
			this._valueFactory = valueFactory;
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x0017B2BC File Offset: 0x001794BC
		[DebuggerStepThrough]
		public TResult GetValue(TInput input)
		{
			TResult value;
			lock (this._lock)
			{
				if (this._value == null)
				{
					Func<TInput, TResult> valueFactory = this._valueFactory;
					try
					{
						this._valueFactory = null;
						this._value = valueFactory(input);
					}
					catch (Exception)
					{
						this._valueFactory = valueFactory;
						throw;
					}
				}
				value = this._value;
			}
			return value;
		}

		// Token: 0x04002360 RID: 9056
		private readonly object _lock = new object();

		// Token: 0x04002361 RID: 9057
		private Func<TInput, TResult> _valueFactory;

		// Token: 0x04002362 RID: 9058
		private TResult _value;
	}
}
