using System;

namespace System.Web.Util
{
	// Token: 0x020001E9 RID: 489
	internal class CalliEventHandlerDelegateProxy
	{
		// Token: 0x06001806 RID: 6150 RVA: 0x0004B99E File Offset: 0x00049B9E
		internal CalliEventHandlerDelegateProxy(object target, IntPtr functionPointer, bool argless)
		{
			this._argless = argless;
			this._target = target;
			this._functionPointer = functionPointer;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0004B9BC File Offset: 0x00049BBC
		internal void Callback(object sender, EventArgs e)
		{
			if (this._argless)
			{
				CalliEventHandlerDelegateProxy.ParameterlessDelegate parameterlessDelegate = FastDelegateCreator<CalliEventHandlerDelegateProxy.ParameterlessDelegate>.BindTo(this._target, this._functionPointer);
				parameterlessDelegate();
				return;
			}
			CalliEventHandlerDelegateProxy.ParameterfulDelegate parameterfulDelegate = FastDelegateCreator<CalliEventHandlerDelegateProxy.ParameterfulDelegate>.BindTo(this._target, this._functionPointer);
			parameterfulDelegate(sender, e);
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0004BA04 File Offset: 0x00049C04
		internal EventHandler Handler
		{
			get
			{
				return new EventHandler(this.Callback);
			}
		}

		// Token: 0x0400176A RID: 5994
		private IntPtr _functionPointer;

		// Token: 0x0400176B RID: 5995
		private object _target;

		// Token: 0x0400176C RID: 5996
		private bool _argless;

		// Token: 0x02000945 RID: 2373
		// (Invoke) Token: 0x0600697F RID: 27007
		private delegate void ParameterlessDelegate();

		// Token: 0x02000946 RID: 2374
		// (Invoke) Token: 0x06006983 RID: 27011
		private delegate void ParameterfulDelegate(object sender, EventArgs e);
	}
}
