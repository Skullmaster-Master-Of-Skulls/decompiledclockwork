using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005A0 RID: 1440
	internal class InstanceContextManager : LifetimeManager, IInstanceContextManager
	{
		// Token: 0x060037E2 RID: 14306 RVA: 0x000D6F7D File Offset: 0x000D517D
		public InstanceContextManager(object mutex) : base(mutex)
		{
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000D6F88 File Offset: 0x000D5188
		public void Add(InstanceContext instanceContext)
		{
			bool flag = false;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (base.State == LifetimeState.Opened)
				{
					if (instanceContext.InstanceContextManagerIndex != 0)
					{
						return;
					}
					if (this.firstFreeIndex == 0)
					{
						this.GrowItems();
					}
					this.AddItem(instanceContext);
					base.IncrementBusyCountWithoutLock();
					flag = true;
				}
			}
			if (!flag)
			{
				instanceContext.Abort();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
			}
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000D7018 File Offset: 0x000D5218
		private void AddItem(InstanceContext instanceContext)
		{
			int num = this.firstFreeIndex;
			this.firstFreeIndex = this.items[num].nextFreeIndex;
			this.items[num].instanceContext = instanceContext;
			instanceContext.InstanceContextManagerIndex = num;
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000D705C File Offset: 0x000D525C
		public IAsyncResult BeginCloseInput(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CloseInputAsyncResult(timeout, callback, state, this.ToArray());
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000D706C File Offset: 0x000D526C
		private void CloseInitiate(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			foreach (InstanceContext instanceContext in this.ToArray())
			{
				try
				{
					if (instanceContext.State == CommunicationState.Opened)
					{
						IAsyncResult asyncResult = instanceContext.BeginClose(timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(InstanceContextManager.CloseInstanceContextCallback)), instanceContext);
						if (asyncResult.CompletedSynchronously)
						{
							instanceContext.EndClose(asyncResult);
						}
					}
					else
					{
						instanceContext.Abort();
					}
				}
				catch (ObjectDisposedException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (InvalidOperationException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				}
				catch (CommunicationException exception3)
				{
					DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
				}
				catch (TimeoutException ex)
				{
					if (TD.CloseTimeoutIsEnabled())
					{
						TD.CloseTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
			}
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000D7160 File Offset: 0x000D5360
		public void CloseInput(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			InstanceContext[] array = this.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].CloseInput(timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000D719C File Offset: 0x000D539C
		private static void CloseInstanceContextCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			InstanceContext instanceContext = (InstanceContext)result.AsyncState;
			try
			{
				instanceContext.EndClose(result);
			}
			catch (ObjectDisposedException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (InvalidOperationException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
			catch (CommunicationException exception3)
			{
				DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
			}
			catch (TimeoutException ex)
			{
				if (TD.CloseTimeoutIsEnabled())
				{
					TD.CloseTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000D7238 File Offset: 0x000D5438
		public void EndCloseInput(IAsyncResult result)
		{
			CloseInputAsyncResult.End(result);
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000D7240 File Offset: 0x000D5440
		private void GrowItems()
		{
			InstanceContextManager.Item[] array = this.items;
			if (array != null)
			{
				this.InitItems(array.Length * 2);
				for (int i = 1; i < array.Length; i++)
				{
					this.AddItem(array[i].instanceContext);
				}
				return;
			}
			this.InitItems(4);
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000D728C File Offset: 0x000D548C
		private void InitItems(int count)
		{
			this.items = new InstanceContextManager.Item[count];
			for (int i = count - 2; i > 0; i--)
			{
				this.items[i].nextFreeIndex = i + 1;
			}
			this.firstFreeIndex = 1;
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000D72D0 File Offset: 0x000D54D0
		protected override void OnAbort()
		{
			InstanceContext[] array = this.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Abort();
			}
			base.OnAbort();
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000D7300 File Offset: 0x000D5500
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.CloseInitiate(timeoutHelper.RemainingTime());
			return base.OnBeginClose(timeoutHelper.RemainingTime(), callback, state);
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000D7334 File Offset: 0x000D5534
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.CloseInitiate(timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000D7363 File Offset: 0x000D5563
		protected override void OnEndClose(IAsyncResult result)
		{
			base.OnEndClose(result);
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000D736C File Offset: 0x000D556C
		public bool Remove(InstanceContext instanceContext)
		{
			if (instanceContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("instanceContext"));
			}
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				int instanceContextManagerIndex = instanceContext.InstanceContextManagerIndex;
				if (instanceContextManagerIndex == 0)
				{
					return false;
				}
				instanceContext.InstanceContextManagerIndex = 0;
				this.items[instanceContextManagerIndex].nextFreeIndex = this.firstFreeIndex;
				this.items[instanceContextManagerIndex].instanceContext = null;
				this.firstFreeIndex = instanceContextManagerIndex;
			}
			base.DecrementBusyCount();
			return true;
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000D740C File Offset: 0x000D560C
		public InstanceContext[] ToArray()
		{
			if (this.items == null)
			{
				return EmptyArray<InstanceContext>.Instance;
			}
			object thisLock = base.ThisLock;
			InstanceContext[] result;
			lock (thisLock)
			{
				int num = 0;
				for (int i = 1; i < this.items.Length; i++)
				{
					if (this.items[i].instanceContext != null)
					{
						num++;
					}
				}
				if (num == 0)
				{
					result = EmptyArray<InstanceContext>.Instance;
				}
				else
				{
					InstanceContext[] array = new InstanceContext[num];
					num = 0;
					for (int j = 1; j < this.items.Length; j++)
					{
						InstanceContext instanceContext = this.items[j].instanceContext;
						if (instanceContext != null)
						{
							array[num++] = instanceContext;
						}
					}
					result = array;
				}
			}
			return result;
		}

		// Token: 0x0400296A RID: 10602
		private int firstFreeIndex;

		// Token: 0x0400296B RID: 10603
		private InstanceContextManager.Item[] items;

		// Token: 0x02000CA5 RID: 3237
		private struct Item
		{
			// Token: 0x04004501 RID: 17665
			public int nextFreeIndex;

			// Token: 0x04004502 RID: 17666
			public InstanceContext instanceContext;
		}
	}
}
