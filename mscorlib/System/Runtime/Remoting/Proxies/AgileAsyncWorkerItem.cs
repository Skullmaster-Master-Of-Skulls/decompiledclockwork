using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000768 RID: 1896
	internal class AgileAsyncWorkerItem
	{
		// Token: 0x06004364 RID: 17252 RVA: 0x000E6435 File Offset: 0x000E5435
		public AgileAsyncWorkerItem(IMethodCallMessage message, AsyncResult ar, object target)
		{
			this._message = new MethodCall(message);
			this._ar = ar;
			this._target = target;
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x000E6457 File Offset: 0x000E5457
		public static void ThreadPoolCallBack(object o)
		{
			((AgileAsyncWorkerItem)o).DoAsyncCall();
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x000E6464 File Offset: 0x000E5464
		public void DoAsyncCall()
		{
			new StackBuilderSink(this._target).AsyncProcessMessage(this._message, this._ar);
		}

		// Token: 0x040021DE RID: 8670
		private IMethodCallMessage _message;

		// Token: 0x040021DF RID: 8671
		private AsyncResult _ar;

		// Token: 0x040021E0 RID: 8672
		private object _target;
	}
}
