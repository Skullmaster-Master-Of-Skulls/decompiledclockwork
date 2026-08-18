using System;
using System.Data.ProviderBase;
using System.Threading;

namespace System.Data.Common
{
	// Token: 0x02000125 RID: 293
	internal sealed class DbAsyncResult : IAsyncResult
	{
		// Token: 0x060012D0 RID: 4816 RVA: 0x00238898 File Offset: 0x00237C98
		internal DbAsyncResult(object owner, string endMethodName, AsyncCallback callback, object stateObject, ExecutionContext execContext)
		{
			this._owner = owner;
			this._endMethodName = endMethodName;
			this._callback = callback;
			this._stateObject = stateObject;
			this._manualResetEvent = new ManualResetEvent(false);
			this._execContext = execContext;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x002388E8 File Offset: 0x00237CE8
		object IAsyncResult.AsyncState
		{
			get
			{
				return this._stateObject;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00238908 File Offset: 0x00237D08
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				return this._manualResetEvent;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00238928 File Offset: 0x00237D28
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return this._fCompletedSynchronously;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00238948 File Offset: 0x00237D48
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x00238968 File Offset: 0x00237D68
		internal DbConnectionInternal ConnectionInternal
		{
			get
			{
				return this._connectionInternal;
			}
			set
			{
				this._connectionInternal = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00238988 File Offset: 0x00237D88
		bool IAsyncResult.IsCompleted
		{
			get
			{
				return this._fCompleted;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x002389A8 File Offset: 0x00237DA8
		internal string EndMethodName
		{
			get
			{
				return this._endMethodName;
			}
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x002389C8 File Offset: 0x00237DC8
		internal void CompareExchangeOwner(object owner, string method)
		{
			object obj = Interlocked.CompareExchange(ref this._owner, null, owner);
			if (obj == owner)
			{
				return;
			}
			if (obj != null)
			{
				throw ADP.IncorrectAsyncResult();
			}
			throw ADP.MethodCalledTwice(method);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x002389F8 File Offset: 0x00237DF8
		internal void Reset()
		{
			this._fCompleted = false;
			this._fCompletedSynchronously = false;
			this._manualResetEvent.Reset();
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00238A28 File Offset: 0x00237E28
		internal void SetCompleted()
		{
			this._fCompleted = true;
			this._manualResetEvent.Set();
			if (this._callback != null)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ExecuteCallback), this);
			}
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00238A68 File Offset: 0x00237E68
		internal void SetCompletedSynchronously()
		{
			this._fCompletedSynchronously = true;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00238A88 File Offset: 0x00237E88
		private static void AsyncCallback_Context(object state)
		{
			DbAsyncResult dbAsyncResult = (DbAsyncResult)state;
			if (dbAsyncResult._callback != null)
			{
				dbAsyncResult._callback(dbAsyncResult);
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00238AB8 File Offset: 0x00237EB8
		private void ExecuteCallback(object asyncResult)
		{
			DbAsyncResult dbAsyncResult = (DbAsyncResult)asyncResult;
			if (dbAsyncResult._callback != null)
			{
				if (dbAsyncResult._execContext != null)
				{
					ExecutionContext.Run(dbAsyncResult._execContext, DbAsyncResult._contextCallback, dbAsyncResult);
					return;
				}
				dbAsyncResult._callback(this);
			}
		}

		// Token: 0x04000BC6 RID: 3014
		private readonly AsyncCallback _callback;

		// Token: 0x04000BC7 RID: 3015
		private bool _fCompleted;

		// Token: 0x04000BC8 RID: 3016
		private bool _fCompletedSynchronously;

		// Token: 0x04000BC9 RID: 3017
		private readonly ManualResetEvent _manualResetEvent;

		// Token: 0x04000BCA RID: 3018
		private object _owner;

		// Token: 0x04000BCB RID: 3019
		private readonly object _stateObject;

		// Token: 0x04000BCC RID: 3020
		private readonly string _endMethodName;

		// Token: 0x04000BCD RID: 3021
		private ExecutionContext _execContext;

		// Token: 0x04000BCE RID: 3022
		private static ContextCallback _contextCallback = new ContextCallback(DbAsyncResult.AsyncCallback_Context);

		// Token: 0x04000BCF RID: 3023
		private DbConnectionInternal _connectionInternal;
	}
}
