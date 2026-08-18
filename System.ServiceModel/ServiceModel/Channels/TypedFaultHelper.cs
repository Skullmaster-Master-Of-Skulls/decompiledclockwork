using System;
using System.Collections.Generic;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000938 RID: 2360
	internal abstract class TypedFaultHelper<TState> : FaultHelper
	{
		// Token: 0x06005AB5 RID: 23221 RVA: 0x0014D2A4 File Offset: 0x0014B4A4
		protected TypedFaultHelper(TimeSpan defaultSendTimeout, TimeSpan defaultCloseTimeout)
		{
			this.defaultSendTimeout = defaultSendTimeout;
			this.defaultCloseTimeout = defaultCloseTimeout;
		}

		// Token: 0x06005AB6 RID: 23222 RVA: 0x0014D2C8 File Offset: 0x0014B4C8
		public override void Abort()
		{
			object thisLock = base.ThisLock;
			Dictionary<IReliableChannelBinder, TState> dictionary;
			InterruptibleWaitObject interruptibleWaitObject;
			lock (thisLock)
			{
				dictionary = this.faultList;
				this.faultList = null;
				interruptibleWaitObject = this.closeHandle;
			}
			if (dictionary == null || dictionary.Count == 0)
			{
				if (interruptibleWaitObject != null)
				{
					interruptibleWaitObject.Set();
				}
				return;
			}
			foreach (KeyValuePair<IReliableChannelBinder, TState> keyValuePair in dictionary)
			{
				this.AbortState(keyValuePair.Value, true);
				keyValuePair.Key.Abort();
			}
			if (interruptibleWaitObject != null)
			{
				interruptibleWaitObject.Set();
			}
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x0014D388 File Offset: 0x0014B588
		private void AbortBinder(IReliableChannelBinder binder)
		{
			try
			{
				binder.Abort();
			}
			finally
			{
				this.RemoveBinder(binder);
			}
		}

		// Token: 0x06005AB8 RID: 23224 RVA: 0x0014D3B8 File Offset: 0x0014B5B8
		private void AsyncCloseBinder(IReliableChannelBinder binder)
		{
			if (this.onBinderCloseComplete == null)
			{
				this.onBinderCloseComplete = Fx.ThunkCallback(new AsyncCallback(this.OnBinderCloseComplete));
			}
			IAsyncResult asyncResult = binder.BeginClose(this.defaultCloseTimeout, this.onBinderCloseComplete, binder);
			if (asyncResult.CompletedSynchronously)
			{
				this.CompleteBinderClose(binder, asyncResult);
			}
		}

		// Token: 0x06005AB9 RID: 23225
		protected abstract void AbortState(TState state, bool isOnAbortThread);

		// Token: 0x06005ABA RID: 23226 RVA: 0x0014D408 File Offset: 0x0014B608
		private void AfterClose()
		{
			this.Abort();
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x0014D410 File Offset: 0x0014B610
		private bool BeforeClose()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.faultList == null || this.faultList.Count == 0)
				{
					return true;
				}
				this.closeHandle = new InterruptibleWaitObject(false, false);
			}
			return false;
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x0014D474 File Offset: 0x0014B674
		public override IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.BeforeClose())
			{
				return new TypedFaultHelper<TState>.AlreadyClosedAsyncResult(callback, state);
			}
			return this.closeHandle.BeginWait(timeout, callback, state);
		}

		// Token: 0x06005ABD RID: 23229
		protected abstract IAsyncResult BeginSendFault(IReliableChannelBinder binder, TState state, TimeSpan timeout, AsyncCallback callback, object asyncState);

		// Token: 0x06005ABE RID: 23230 RVA: 0x0014D494 File Offset: 0x0014B694
		public override void Close(TimeSpan timeout)
		{
			if (this.BeforeClose())
			{
				return;
			}
			this.closeHandle.Wait(timeout);
			this.AfterClose();
		}

		// Token: 0x06005ABF RID: 23231 RVA: 0x0014D4B4 File Offset: 0x0014B6B4
		private void CompleteBinderClose(IReliableChannelBinder binder, IAsyncResult result)
		{
			try
			{
				binder.EndClose(result);
			}
			finally
			{
				this.RemoveBinder(binder);
			}
		}

		// Token: 0x06005AC0 RID: 23232 RVA: 0x0014D4E4 File Offset: 0x0014B6E4
		private void CompleteSendFault(IReliableChannelBinder binder, TState state, IAsyncResult result)
		{
			bool flag = true;
			try
			{
				this.EndSendFault(binder, state, result);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.AbortState(state, false);
					this.AbortBinder(binder);
				}
			}
			this.AsyncCloseBinder(binder);
		}

		// Token: 0x06005AC1 RID: 23233 RVA: 0x0014D52C File Offset: 0x0014B72C
		public override void EndClose(IAsyncResult result)
		{
			if (result is TypedFaultHelper<TState>.AlreadyClosedAsyncResult)
			{
				CompletedAsyncResult.End(result);
			}
			else
			{
				this.closeHandle.EndWait(result);
			}
			this.AfterClose();
		}

		// Token: 0x06005AC2 RID: 23234
		protected abstract void EndSendFault(IReliableChannelBinder binder, TState state, IAsyncResult result);

		// Token: 0x06005AC3 RID: 23235
		protected abstract TState GetState(RequestContext requestContext, Message faultMessage);

		// Token: 0x06005AC4 RID: 23236 RVA: 0x0014D550 File Offset: 0x0014B750
		private void OnBinderCloseComplete(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			IReliableChannelBinder reliableChannelBinder = (IReliableChannelBinder)result.AsyncState;
			try
			{
				this.CompleteBinderClose(reliableChannelBinder, result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableChannelBinder.HandleException(ex);
			}
		}

		// Token: 0x06005AC5 RID: 23237 RVA: 0x0014D5A0 File Offset: 0x0014B7A0
		private void OnSendFaultComplete(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			object thisLock = base.ThisLock;
			IReliableChannelBinder reliableChannelBinder;
			TState state;
			lock (thisLock)
			{
				if (this.faultList == null)
				{
					return;
				}
				reliableChannelBinder = (IReliableChannelBinder)result.AsyncState;
				state = this.faultList[reliableChannelBinder];
			}
			try
			{
				this.CompleteSendFault(reliableChannelBinder, state, result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableChannelBinder.HandleException(ex);
			}
		}

		// Token: 0x06005AC6 RID: 23238 RVA: 0x0014D638 File Offset: 0x0014B838
		protected void RemoveBinder(IReliableChannelBinder binder)
		{
			object thisLock = base.ThisLock;
			InterruptibleWaitObject interruptibleWaitObject;
			lock (thisLock)
			{
				if (this.faultList == null)
				{
					return;
				}
				this.faultList.Remove(binder);
				if (this.closeHandle == null || this.faultList.Count > 0)
				{
					return;
				}
				this.faultList = null;
				interruptibleWaitObject = this.closeHandle;
			}
			interruptibleWaitObject.Set();
		}

		// Token: 0x06005AC7 RID: 23239 RVA: 0x0014D6B8 File Offset: 0x0014B8B8
		protected void SendFault(IReliableChannelBinder binder, TState state)
		{
			bool flag = true;
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.BeginSendFault(binder, state, this.defaultSendTimeout, this.onSendFaultComplete, binder);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.AbortState(state, false);
					this.AbortBinder(binder);
				}
			}
			if (asyncResult.CompletedSynchronously)
			{
				this.CompleteSendFault(binder, state, asyncResult);
			}
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x0014D718 File Offset: 0x0014B918
		public override void SendFaultAsync(IReliableChannelBinder binder, RequestContext requestContext, Message faultMessage)
		{
			try
			{
				bool flag = true;
				TState state = this.GetState(requestContext, faultMessage);
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.faultList != null)
					{
						flag = false;
						this.faultList.Add(binder, state);
						if (this.onSendFaultComplete == null)
						{
							this.onSendFaultComplete = Fx.ThunkCallback(new AsyncCallback(this.OnSendFaultComplete));
						}
					}
				}
				if (flag)
				{
					this.AbortState(state, false);
					binder.Abort();
				}
				else if (Thread.CurrentThread.IsThreadPoolThread)
				{
					this.SendFault(binder, state);
				}
				else
				{
					if (this.sendFaultCallback == null)
					{
						this.sendFaultCallback = new Action<object>(this.SendFaultCallback);
					}
					ActionItem.Schedule(this.sendFaultCallback, binder);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				binder.HandleException(ex);
			}
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x0014D808 File Offset: 0x0014BA08
		private void SendFaultCallback(object callbackState)
		{
			object thisLock = base.ThisLock;
			IReliableChannelBinder reliableChannelBinder;
			TState state;
			lock (thisLock)
			{
				if (this.faultList == null)
				{
					return;
				}
				reliableChannelBinder = (IReliableChannelBinder)callbackState;
				state = this.faultList[reliableChannelBinder];
			}
			try
			{
				this.SendFault(reliableChannelBinder, state);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableChannelBinder.HandleException(ex);
			}
		}

		// Token: 0x040036B3 RID: 14003
		private InterruptibleWaitObject closeHandle;

		// Token: 0x040036B4 RID: 14004
		private TimeSpan defaultCloseTimeout;

		// Token: 0x040036B5 RID: 14005
		private TimeSpan defaultSendTimeout;

		// Token: 0x040036B6 RID: 14006
		private Dictionary<IReliableChannelBinder, TState> faultList = new Dictionary<IReliableChannelBinder, TState>();

		// Token: 0x040036B7 RID: 14007
		private AsyncCallback onBinderCloseComplete;

		// Token: 0x040036B8 RID: 14008
		private AsyncCallback onSendFaultComplete;

		// Token: 0x040036B9 RID: 14009
		private Action<object> sendFaultCallback;

		// Token: 0x02000DC9 RID: 3529
		private class AlreadyClosedAsyncResult : CompletedAsyncResult
		{
			// Token: 0x06008003 RID: 32771 RVA: 0x001DC229 File Offset: 0x001DA429
			public AlreadyClosedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}
		}
	}
}
