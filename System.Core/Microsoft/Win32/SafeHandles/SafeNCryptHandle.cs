using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001C RID: 28
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class SafeNCryptHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00003662 File Offset: 0x00001862
		protected SafeNCryptHandle() : base(true)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000366C File Offset: 0x0000186C
		protected SafeNCryptHandle(IntPtr handle, SafeHandle parentHandle) : base(true)
		{
			if (parentHandle == null)
			{
				throw new ArgumentNullException("parentHandle");
			}
			if (parentHandle.IsClosed || parentHandle.IsInvalid)
			{
				throw new ArgumentException("Argument_Invalid_SafeHandleInvalidOrClosed", "parentHandle");
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag = false;
				parentHandle.DangerousAddRef(ref flag);
				if (flag)
				{
					this._parentHandle = parentHandle;
					base.SetHandle(handle);
					if (this.IsInvalid)
					{
						this._parentHandle.DangerousRelease();
						this._parentHandle = null;
					}
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000036FC File Offset: 0x000018FC
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00003704 File Offset: 0x00001904
		private SafeNCryptHandle Holder
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.m_holder;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this.m_holder = value;
				this.m_ownershipState = SafeNCryptHandle.OwnershipState.Duplicate;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003714 File Offset: 0x00001914
		internal T Duplicate<T>() where T : SafeNCryptHandle, new()
		{
			if (this.m_ownershipState == SafeNCryptHandle.OwnershipState.Owner)
			{
				return this.DuplicateOwnerHandle<T>();
			}
			return this.DuplicateDuplicatedHandle<T>();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000372C File Offset: 0x0000192C
		private T DuplicateDuplicatedHandle<T>() where T : SafeNCryptHandle, new()
		{
			bool flag = false;
			T t = Activator.CreateInstance<T>();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.Holder.DangerousAddRef(ref flag);
				t.SetHandle(this.Holder.DangerousGetHandle());
				t.Holder = this.Holder;
			}
			return t;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003790 File Offset: 0x00001990
		private T DuplicateOwnerHandle<T>() where T : SafeNCryptHandle, new()
		{
			bool flag = false;
			T t = Activator.CreateInstance<T>();
			T t2 = Activator.CreateInstance<T>();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				t.m_ownershipState = SafeNCryptHandle.OwnershipState.Holder;
				t.SetHandle(base.DangerousGetHandle());
				GC.SuppressFinalize(t);
				if (this._parentHandle != null)
				{
					t._parentHandle = this._parentHandle;
					this._parentHandle = null;
				}
				this.Holder = t;
				t.DangerousAddRef(ref flag);
				t2.SetHandle(t.DangerousGetHandle());
				t2.Holder = t;
			}
			return t2;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003850 File Offset: 0x00001A50
		protected override bool ReleaseHandle()
		{
			if (this.m_ownershipState == SafeNCryptHandle.OwnershipState.Duplicate)
			{
				this.Holder.DangerousRelease();
				return true;
			}
			if (this._parentHandle != null)
			{
				this._parentHandle.DangerousRelease();
				return true;
			}
			return this.ReleaseNativeHandle();
		}

		// Token: 0x060000EF RID: 239
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseNativeHandle();

		// Token: 0x040000D2 RID: 210
		private SafeNCryptHandle.OwnershipState m_ownershipState;

		// Token: 0x040000D3 RID: 211
		private SafeNCryptHandle m_holder;

		// Token: 0x040000D4 RID: 212
		private SafeHandle _parentHandle;

		// Token: 0x020002F7 RID: 759
		private enum OwnershipState
		{
			// Token: 0x04000DE8 RID: 3560
			Owner,
			// Token: 0x04000DE9 RID: 3561
			Duplicate,
			// Token: 0x04000DEA RID: 3562
			Holder
		}
	}
}
