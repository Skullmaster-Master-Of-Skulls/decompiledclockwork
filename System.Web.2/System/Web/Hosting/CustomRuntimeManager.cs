using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Web.Hosting
{
	// Token: 0x02000793 RID: 1939
	internal sealed class CustomRuntimeManager : ICustomRuntimeManager
	{
		// Token: 0x06005C90 RID: 23696 RVA: 0x0014058C File Offset: 0x0013E78C
		private List<IProcessSuspendListener> GetAllSuspendListeners()
		{
			List<IProcessSuspendListener> list = new List<IProcessSuspendListener>();
			foreach (CustomRuntimeManager.CustomRuntimeRegistration customRuntimeRegistration in this._activeRegistrations.Keys)
			{
				IProcessSuspendListener processSuspendListener = customRuntimeRegistration.CustomRuntime as IProcessSuspendListener;
				if (processSuspendListener != null)
				{
					list.Add(processSuspendListener);
				}
			}
			return list;
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x001405F4 File Offset: 0x0013E7F4
		public ICustomRuntimeRegistrationToken Register(ICustomRuntime customRuntime)
		{
			CustomRuntimeManager.CustomRuntimeRegistration customRuntimeRegistration = new CustomRuntimeManager.CustomRuntimeRegistration(this, customRuntime);
			this._activeRegistrations[customRuntimeRegistration] = null;
			return customRuntimeRegistration;
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x00140618 File Offset: 0x0013E818
		public Action SuspendAllCustomRuntimes()
		{
			List<IProcessSuspendListener> allSuspendListeners = this.GetAllSuspendListeners();
			if (allSuspendListeners == null || allSuspendListeners.Count == 0)
			{
				return null;
			}
			List<IProcessResumeCallback> callbacks = new List<IProcessResumeCallback>(allSuspendListeners.Count);
			foreach (IProcessSuspendListener processSuspendListener in allSuspendListeners)
			{
				IProcessResumeCallback processResumeCallback = null;
				try
				{
					processResumeCallback = processSuspendListener.Suspend();
				}
				catch (AppDomainUnloadedException)
				{
				}
				if (processResumeCallback != null)
				{
					callbacks.Add(processResumeCallback);
				}
			}
			return delegate()
			{
				foreach (IProcessResumeCallback processResumeCallback2 in callbacks)
				{
					try
					{
						processResumeCallback2.Resume();
					}
					catch (AppDomainUnloadedException)
					{
					}
				}
			};
		}

		// Token: 0x040030C7 RID: 12487
		private readonly ConcurrentDictionary<CustomRuntimeManager.CustomRuntimeRegistration, object> _activeRegistrations = new ConcurrentDictionary<CustomRuntimeManager.CustomRuntimeRegistration, object>();

		// Token: 0x02000A50 RID: 2640
		private sealed class CustomRuntimeRegistration : ICustomRuntimeRegistrationToken
		{
			// Token: 0x06006EC0 RID: 28352 RVA: 0x0018ADC2 File Offset: 0x00188FC2
			public CustomRuntimeRegistration(CustomRuntimeManager customRuntimeManager, ICustomRuntime customRuntime)
			{
				this._customRuntimeManager = customRuntimeManager;
				this.CustomRuntime = customRuntime;
			}

			// Token: 0x17001E40 RID: 7744
			// (get) Token: 0x06006EC1 RID: 28353 RVA: 0x0018ADD8 File Offset: 0x00188FD8
			// (set) Token: 0x06006EC2 RID: 28354 RVA: 0x0018ADE0 File Offset: 0x00188FE0
			public ICustomRuntime CustomRuntime { get; private set; }

			// Token: 0x06006EC3 RID: 28355 RVA: 0x0018ADEC File Offset: 0x00188FEC
			public void Unregister()
			{
				object obj;
				bool flag = this._customRuntimeManager._activeRegistrations.TryRemove(this, out obj);
			}

			// Token: 0x04003B65 RID: 15205
			private readonly CustomRuntimeManager _customRuntimeManager;
		}
	}
}
