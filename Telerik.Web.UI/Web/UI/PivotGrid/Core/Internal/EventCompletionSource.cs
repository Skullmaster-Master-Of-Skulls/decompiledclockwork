using System;
using System.Reflection;
using System.Threading;

namespace Telerik.Web.UI.PivotGrid.Core.Internal
{
	// Token: 0x02000CDF RID: 3295
	internal sealed class EventCompletionSource<TArgs> : IDisposable where TArgs : EventArgs
	{
		// Token: 0x06007B24 RID: 31524 RVA: 0x001C44D8 File Offset: 0x001C26D8
		public EventCompletionSource(object eventSource, string eventName)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			if (eventName == null)
			{
				throw new ArgumentNullException("eventName");
			}
			this.eventSource = eventSource;
			this.eventName = eventName;
			this.eventSync = new ManualResetEvent(false);
			this.eventDelegate = null;
			this.Subscribe();
		}

		// Token: 0x06007B25 RID: 31525 RVA: 0x001C452E File Offset: 0x001C272E
		public void AwaitEvent()
		{
			if (this.eventSync == null)
			{
				return;
			}
			this.eventSync.WaitOne();
		}

		// Token: 0x06007B26 RID: 31526 RVA: 0x001C4545 File Offset: 0x001C2745
		private void Handler(object sender, TArgs args)
		{
			this.Unsubscribe();
			if (this.eventSync != null)
			{
				this.eventSync.Set();
			}
		}

		// Token: 0x06007B27 RID: 31527 RVA: 0x001C4561 File Offset: 0x001C2761
		private void Unsubscribe()
		{
			if (this.eventInfo == null)
			{
				return;
			}
			if (this.eventDelegate == null)
			{
				return;
			}
			this.eventInfo.RemoveEventHandler(this.eventSource, this.eventDelegate);
			this.eventInfo = null;
			this.eventDelegate = null;
		}

		// Token: 0x06007B28 RID: 31528 RVA: 0x001C45A0 File Offset: 0x001C27A0
		private void Subscribe()
		{
			this.eventInfo = this.eventSource.GetType().GetEvent(this.eventName);
			if (this.eventInfo == null)
			{
				throw new InvalidOperationException("Event not found");
			}
			Type eventHandlerType = this.eventInfo.EventHandlerType;
			this.eventDelegate = Delegate.CreateDelegate(eventHandlerType, this, "Handler");
			this.eventInfo.AddEventHandler(this.eventSource, this.eventDelegate);
		}

		// Token: 0x06007B29 RID: 31529 RVA: 0x001C4617 File Offset: 0x001C2817
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06007B2A RID: 31530 RVA: 0x001C4628 File Offset: 0x001C2828
		private void Dispose(bool disposing)
		{
			if (this.eventSync == null)
			{
				throw new InvalidOperationException("Already disposed.");
			}
			if (!disposing)
			{
				return;
			}
			IDisposable disposable = this.eventSync;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			this.eventSync = null;
		}

		// Token: 0x040021B1 RID: 8625
		private object eventSource;

		// Token: 0x040021B2 RID: 8626
		private string eventName;

		// Token: 0x040021B3 RID: 8627
		private ManualResetEvent eventSync;

		// Token: 0x040021B4 RID: 8628
		private EventInfo eventInfo;

		// Token: 0x040021B5 RID: 8629
		private Delegate eventDelegate;
	}
}
