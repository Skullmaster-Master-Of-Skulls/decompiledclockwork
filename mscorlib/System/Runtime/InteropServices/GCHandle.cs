using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200050E RID: 1294
	[ComVisible(true)]
	public struct GCHandle
	{
		// Token: 0x060031B8 RID: 12728 RVA: 0x000A9B0A File Offset: 0x000A8B0A
		static GCHandle()
		{
			GCHandle.s_probeIsActive = Mda.IsInvalidGCHandleCookieProbeEnabled();
			if (GCHandle.s_probeIsActive)
			{
				GCHandle.s_cookieTable = new GCHandleCookieTable();
			}
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000A9B3E File Offset: 0x000A8B3E
		internal GCHandle(object value, GCHandleType type)
		{
			this.m_handle = GCHandle.InternalAlloc(value, type);
			if (type == GCHandleType.Pinned)
			{
				this.SetIsPinned();
			}
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000A9B57 File Offset: 0x000A8B57
		internal GCHandle(IntPtr handle)
		{
			GCHandle.InternalCheckDomain(handle);
			this.m_handle = handle;
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000A9B66 File Offset: 0x000A8B66
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static GCHandle Alloc(object value)
		{
			return new GCHandle(value, GCHandleType.Normal);
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000A9B6F File Offset: 0x000A8B6F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static GCHandle Alloc(object value, GCHandleType type)
		{
			return new GCHandle(value, type);
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000A9B78 File Offset: 0x000A8B78
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void Free()
		{
			IntPtr handle = this.m_handle;
			if (!(handle != IntPtr.Zero) || !(Interlocked.CompareExchange(ref this.m_handle, IntPtr.Zero, handle) == handle))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
			}
			GCHandle.InternalFree((IntPtr)((long)handle & -2L));
			if (GCHandle.s_probeIsActive)
			{
				GCHandle.s_cookieTable.RemoveHandleIfPresent(handle);
				return;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x000A9BE9 File Offset: 0x000A8BE9
		// (set) Token: 0x060031BF RID: 12735 RVA: 0x000A9C18 File Offset: 0x000A8C18
		public object Target
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.m_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
				}
				return GCHandle.InternalGet(this.GetHandleValue());
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (this.m_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
				}
				GCHandle.InternalSet(this.GetHandleValue(), value, this.IsPinned());
			}
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000A9C50 File Offset: 0x000A8C50
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public IntPtr AddrOfPinnedObject()
		{
			if (this.IsPinned())
			{
				return GCHandle.InternalAddrOfPinnedObject(this.GetHandleValue());
			}
			if (this.m_handle == IntPtr.Zero)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotPinned"));
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060031C1 RID: 12737 RVA: 0x000A9CA2 File Offset: 0x000A8CA2
		public bool IsAllocated
		{
			get
			{
				return this.m_handle != IntPtr.Zero;
			}
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000A9CB4 File Offset: 0x000A8CB4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static explicit operator GCHandle(IntPtr value)
		{
			return GCHandle.FromIntPtr(value);
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000A9CBC File Offset: 0x000A8CBC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static GCHandle FromIntPtr(IntPtr value)
		{
			if (value == IntPtr.Zero)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
			}
			IntPtr intPtr = value;
			if (GCHandle.s_probeIsActive)
			{
				intPtr = GCHandle.s_cookieTable.GetHandle(value);
				if (IntPtr.Zero == intPtr)
				{
					Mda.FireInvalidGCHandleCookieProbe(value);
					return new GCHandle(IntPtr.Zero);
				}
			}
			return new GCHandle(intPtr);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000A9D1F File Offset: 0x000A8D1F
		public static explicit operator IntPtr(GCHandle value)
		{
			return GCHandle.ToIntPtr(value);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000A9D27 File Offset: 0x000A8D27
		public static IntPtr ToIntPtr(GCHandle value)
		{
			if (GCHandle.s_probeIsActive)
			{
				return GCHandle.s_cookieTable.FindOrAddHandle(value.m_handle);
			}
			return value.m_handle;
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000A9D49 File Offset: 0x000A8D49
		public override int GetHashCode()
		{
			return (int)this.m_handle;
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000A9D58 File Offset: 0x000A8D58
		public override bool Equals(object o)
		{
			if (o == null || !(o is GCHandle))
			{
				return false;
			}
			GCHandle gchandle = (GCHandle)o;
			return this.m_handle == gchandle.m_handle;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000A9D8B File Offset: 0x000A8D8B
		public static bool operator ==(GCHandle a, GCHandle b)
		{
			return a.m_handle == b.m_handle;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000A9DA0 File Offset: 0x000A8DA0
		public static bool operator !=(GCHandle a, GCHandle b)
		{
			return a.m_handle != b.m_handle;
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000A9DB5 File Offset: 0x000A8DB5
		internal IntPtr GetHandleValue()
		{
			return new IntPtr((long)this.m_handle & -2L);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000A9DCB File Offset: 0x000A8DCB
		internal bool IsPinned()
		{
			return ((long)this.m_handle & 1L) != 0L;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000A9DE2 File Offset: 0x000A8DE2
		internal void SetIsPinned()
		{
			this.m_handle = new IntPtr((long)this.m_handle | 1L);
		}

		// Token: 0x060031CD RID: 12749
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalAlloc(object value, GCHandleType type);

		// Token: 0x060031CE RID: 12750
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalFree(IntPtr handle);

		// Token: 0x060031CF RID: 12751
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InternalGet(IntPtr handle);

		// Token: 0x060031D0 RID: 12752
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalSet(IntPtr handle, object value, bool isPinned);

		// Token: 0x060031D1 RID: 12753
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InternalCompareExchange(IntPtr handle, object value, object oldValue, bool isPinned);

		// Token: 0x060031D2 RID: 12754
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalAddrOfPinnedObject(IntPtr handle);

		// Token: 0x060031D3 RID: 12755
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalCheckDomain(IntPtr handle);

		// Token: 0x040019C1 RID: 6593
		internal static readonly IntPtr InvalidCookie = new IntPtr(-1);

		// Token: 0x040019C2 RID: 6594
		private IntPtr m_handle;

		// Token: 0x040019C3 RID: 6595
		private static GCHandleCookieTable s_cookieTable = null;

		// Token: 0x040019C4 RID: 6596
		private static bool s_probeIsActive = false;
	}
}
