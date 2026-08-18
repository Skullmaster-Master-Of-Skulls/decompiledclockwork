using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000166 RID: 358
	internal class ComponentManagerProxy : MarshalByRefObject, UnsafeNativeMethods.IMsoComponentManager, UnsafeNativeMethods.IMsoComponent
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002C7BB File Offset: 0x0002A9BB
		internal ComponentManagerProxy(ComponentManagerBroker broker, UnsafeNativeMethods.IMsoComponentManager original)
		{
			this._broker = broker;
			this._original = original;
			this._creationThread = SafeNativeMethods.GetCurrentThreadId();
			this._refCount = 0;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002C7E4 File Offset: 0x0002A9E4
		private void Dispose()
		{
			if (this._original != null)
			{
				Marshal.ReleaseComObject(this._original);
				this._original = null;
				this._components = null;
				this._componentId = (IntPtr)0;
				this._refCount = 0;
				this._broker.ClearComponentManager();
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00015ECC File Offset: 0x000140CC
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0002C831 File Offset: 0x0002AA31
		private bool RevokeComponent()
		{
			return this._original.FRevokeComponent(this._componentId);
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x0002C844 File Offset: 0x0002AA44
		private UnsafeNativeMethods.IMsoComponent Component
		{
			get
			{
				if (this._trackingComponent != null)
				{
					return this._trackingComponent;
				}
				if (this._activeComponent != null)
				{
					return this._activeComponent;
				}
				return null;
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0002C868 File Offset: 0x0002AA68
		bool UnsafeNativeMethods.IMsoComponent.FDebugMessage(IntPtr hInst, int msg, IntPtr wparam, IntPtr lparam)
		{
			UnsafeNativeMethods.IMsoComponent component = this.Component;
			return component != null && component.FDebugMessage(hInst, msg, wparam, lparam);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0002C88C File Offset: 0x0002AA8C
		bool UnsafeNativeMethods.IMsoComponent.FPreTranslateMessage(ref NativeMethods.MSG msg)
		{
			UnsafeNativeMethods.IMsoComponent component = this.Component;
			return component != null && component.FPreTranslateMessage(ref msg);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0002C8AC File Offset: 0x0002AAAC
		void UnsafeNativeMethods.IMsoComponent.OnEnterState(int uStateID, bool fEnter)
		{
			if (this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					msoComponent.OnEnterState(uStateID, fEnter);
				}
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002C910 File Offset: 0x0002AB10
		void UnsafeNativeMethods.IMsoComponent.OnAppActivate(bool fActive, int dwOtherThreadID)
		{
			if (this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					msoComponent.OnAppActivate(fActive, dwOtherThreadID);
				}
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002C974 File Offset: 0x0002AB74
		void UnsafeNativeMethods.IMsoComponent.OnLoseActivation()
		{
			if (this._activeComponent != null)
			{
				this._activeComponent.OnLoseActivation();
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002C98C File Offset: 0x0002AB8C
		void UnsafeNativeMethods.IMsoComponent.OnActivationChange(UnsafeNativeMethods.IMsoComponent component, bool fSameComponent, int pcrinfo, bool fHostIsActivating, int pchostinfo, int dwReserved)
		{
			if (this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					msoComponent.OnActivationChange(component, fSameComponent, pcrinfo, fHostIsActivating, pchostinfo, dwReserved);
				}
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0002C9F4 File Offset: 0x0002ABF4
		bool UnsafeNativeMethods.IMsoComponent.FDoIdle(int grfidlef)
		{
			bool flag = false;
			if (this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					flag |= msoComponent.FDoIdle(grfidlef);
				}
			}
			return flag;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0002CA5C File Offset: 0x0002AC5C
		bool UnsafeNativeMethods.IMsoComponent.FContinueMessageLoop(int reason, int pvLoopData, NativeMethods.MSG[] msgPeeked)
		{
			bool flag = false;
			if (this._refCount == 0 && this._componentId != (IntPtr)0 && this.RevokeComponent())
			{
				this._components.Clear();
				this._componentId = (IntPtr)0;
			}
			if (this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					flag |= msoComponent.FContinueMessageLoop(reason, pvLoopData, msgPeeked);
				}
			}
			return flag;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00013062 File Offset: 0x00011262
		bool UnsafeNativeMethods.IMsoComponent.FQueryTerminate(bool fPromptUser)
		{
			return true;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0002CB00 File Offset: 0x0002AD00
		void UnsafeNativeMethods.IMsoComponent.Terminate()
		{
			if (this._components != null && this._components.Values.Count > 0)
			{
				UnsafeNativeMethods.IMsoComponent[] array = new UnsafeNativeMethods.IMsoComponent[this._components.Values.Count];
				this._components.Values.CopyTo(array, 0);
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in array)
				{
					msoComponent.Terminate();
				}
			}
			if (this._original != null)
			{
				this.RevokeComponent();
			}
			this.Dispose();
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0002CB80 File Offset: 0x0002AD80
		IntPtr UnsafeNativeMethods.IMsoComponent.HwndGetWindow(int dwWhich, int dwReserved)
		{
			UnsafeNativeMethods.IMsoComponent component = this.Component;
			if (component != null)
			{
				return component.HwndGetWindow(dwWhich, dwReserved);
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002CBA5 File Offset: 0x0002ADA5
		int UnsafeNativeMethods.IMsoComponentManager.QueryService(ref Guid guidService, ref Guid iid, out object ppvObj)
		{
			return this._original.QueryService(ref guidService, ref iid, out ppvObj);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002CBB5 File Offset: 0x0002ADB5
		bool UnsafeNativeMethods.IMsoComponentManager.FDebugMessage(IntPtr hInst, int msg, IntPtr wparam, IntPtr lparam)
		{
			return this._original.FDebugMessage(hInst, msg, wparam, lparam);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002CBC8 File Offset: 0x0002ADC8
		bool UnsafeNativeMethods.IMsoComponentManager.FRegisterComponent(UnsafeNativeMethods.IMsoComponent component, NativeMethods.MSOCRINFOSTRUCT pcrinfo, out IntPtr dwComponentID)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			dwComponentID = (IntPtr)0;
			if (this._refCount == 0 && !this._original.FRegisterComponent(this, pcrinfo, out this._componentId))
			{
				return false;
			}
			this._refCount++;
			if (this._components == null)
			{
				this._components = new Dictionary<int, UnsafeNativeMethods.IMsoComponent>();
			}
			this._nextComponentId++;
			if (this._nextComponentId == 2147483647)
			{
				this._nextComponentId = 1;
			}
			bool flag = false;
			while (this._components.ContainsKey(this._nextComponentId))
			{
				this._nextComponentId++;
				if (this._nextComponentId == 2147483647)
				{
					if (flag)
					{
						throw new InvalidOperationException(SR.GetString("ComponentManagerProxyOutOfMemory"));
					}
					flag = true;
					this._nextComponentId = 1;
				}
			}
			this._components.Add(this._nextComponentId, component);
			dwComponentID = (IntPtr)this._nextComponentId;
			return true;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002CCBC File Offset: 0x0002AEBC
		bool UnsafeNativeMethods.IMsoComponentManager.FRevokeComponent(IntPtr dwComponentID)
		{
			int num = (int)((long)dwComponentID);
			if (this._original == null)
			{
				return false;
			}
			if (this._components == null || num <= 0 || !this._components.ContainsKey(num))
			{
				return false;
			}
			if (this._refCount == 1 && SafeNativeMethods.GetCurrentThreadId() == this._creationThread && !this.RevokeComponent())
			{
				return false;
			}
			this._refCount--;
			this._components.Remove(num);
			if (this._refCount <= 0)
			{
				this.Dispose();
			}
			if (num == this._activeComponentId)
			{
				this._activeComponent = null;
				this._activeComponentId = 0;
			}
			if (num == this._trackingComponentId)
			{
				this._trackingComponent = null;
				this._trackingComponentId = 0;
			}
			return true;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0002CD70 File Offset: 0x0002AF70
		bool UnsafeNativeMethods.IMsoComponentManager.FUpdateComponentRegistration(IntPtr dwComponentID, NativeMethods.MSOCRINFOSTRUCT info)
		{
			return this._original != null && this._original.FUpdateComponentRegistration(this._componentId, info);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002CD90 File Offset: 0x0002AF90
		bool UnsafeNativeMethods.IMsoComponentManager.FOnComponentActivate(IntPtr dwComponentID)
		{
			int num = (int)((long)dwComponentID);
			if (this._original == null)
			{
				return false;
			}
			if (this._components == null || num <= 0 || !this._components.ContainsKey(num))
			{
				return false;
			}
			if (!this._original.FOnComponentActivate(this._componentId))
			{
				return false;
			}
			this._activeComponent = this._components[num];
			this._activeComponentId = num;
			return true;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0002CDFC File Offset: 0x0002AFFC
		bool UnsafeNativeMethods.IMsoComponentManager.FSetTrackingComponent(IntPtr dwComponentID, bool fTrack)
		{
			int num = (int)((long)dwComponentID);
			if (this._original == null)
			{
				return false;
			}
			if (this._components == null || num <= 0 || !this._components.ContainsKey(num))
			{
				return false;
			}
			if (!this._original.FSetTrackingComponent(this._componentId, fTrack))
			{
				return false;
			}
			if (fTrack)
			{
				this._trackingComponent = this._components[num];
				this._trackingComponentId = num;
			}
			else
			{
				this._trackingComponent = null;
				this._trackingComponentId = 0;
			}
			return true;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0002CE7C File Offset: 0x0002B07C
		void UnsafeNativeMethods.IMsoComponentManager.OnComponentEnterState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude, int dwReserved)
		{
			if (this._original == null)
			{
				return;
			}
			if ((uContext == 0 || uContext == 1) && this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					msoComponent.OnEnterState(uStateID, true);
				}
			}
			this._original.OnComponentEnterState(this._componentId, uStateID, uContext, cpicmExclude, rgpicmExclude, dwReserved);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002CF08 File Offset: 0x0002B108
		bool UnsafeNativeMethods.IMsoComponentManager.FOnComponentExitState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude)
		{
			if (this._original == null)
			{
				return false;
			}
			if ((uContext == 0 || uContext == 1) && this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					msoComponent.OnEnterState(uStateID, false);
				}
			}
			return this._original.FOnComponentExitState(this._componentId, uStateID, uContext, cpicmExclude, rgpicmExclude);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002CF94 File Offset: 0x0002B194
		bool UnsafeNativeMethods.IMsoComponentManager.FInState(int uStateID, IntPtr pvoid)
		{
			return this._original != null && this._original.FInState(uStateID, pvoid);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002CFAD File Offset: 0x0002B1AD
		bool UnsafeNativeMethods.IMsoComponentManager.FContinueIdle()
		{
			return this._original != null && this._original.FContinueIdle();
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0002CFC4 File Offset: 0x0002B1C4
		bool UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(IntPtr dwComponentID, int reason, int pvLoopData)
		{
			return this._original != null && this._original.FPushMessageLoop(this._componentId, reason, pvLoopData);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0002CFE3 File Offset: 0x0002B1E3
		bool UnsafeNativeMethods.IMsoComponentManager.FCreateSubComponentManager(object punkOuter, object punkServProv, ref Guid riid, out IntPtr ppvObj)
		{
			if (this._original == null)
			{
				ppvObj = IntPtr.Zero;
				return false;
			}
			return this._original.FCreateSubComponentManager(punkOuter, punkServProv, ref riid, out ppvObj);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0002D007 File Offset: 0x0002B207
		bool UnsafeNativeMethods.IMsoComponentManager.FGetParentComponentManager(out UnsafeNativeMethods.IMsoComponentManager ppicm)
		{
			if (this._original == null)
			{
				ppicm = null;
				return false;
			}
			return this._original.FGetParentComponentManager(out ppicm);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002D024 File Offset: 0x0002B224
		bool UnsafeNativeMethods.IMsoComponentManager.FGetActiveComponent(int dwgac, UnsafeNativeMethods.IMsoComponent[] ppic, NativeMethods.MSOCRINFOSTRUCT info, int dwReserved)
		{
			if (this._original == null)
			{
				return false;
			}
			if (this._original.FGetActiveComponent(dwgac, ppic, info, dwReserved))
			{
				if (ppic[0] == this)
				{
					if (dwgac == 0)
					{
						ppic[0] = this._activeComponent;
					}
					else if (dwgac == 1)
					{
						ppic[0] = this._trackingComponent;
					}
					else if (dwgac == 2 && this._trackingComponent != null)
					{
						ppic[0] = this._trackingComponent;
					}
				}
				return ppic[0] != null;
			}
			return false;
		}

		// Token: 0x04000805 RID: 2053
		private ComponentManagerBroker _broker;

		// Token: 0x04000806 RID: 2054
		private UnsafeNativeMethods.IMsoComponentManager _original;

		// Token: 0x04000807 RID: 2055
		private int _refCount;

		// Token: 0x04000808 RID: 2056
		private int _creationThread;

		// Token: 0x04000809 RID: 2057
		private IntPtr _componentId;

		// Token: 0x0400080A RID: 2058
		private int _nextComponentId;

		// Token: 0x0400080B RID: 2059
		private Dictionary<int, UnsafeNativeMethods.IMsoComponent> _components;

		// Token: 0x0400080C RID: 2060
		private UnsafeNativeMethods.IMsoComponent _activeComponent;

		// Token: 0x0400080D RID: 2061
		private int _activeComponentId;

		// Token: 0x0400080E RID: 2062
		private UnsafeNativeMethods.IMsoComponent _trackingComponent;

		// Token: 0x0400080F RID: 2063
		private int _trackingComponentId;
	}
}
