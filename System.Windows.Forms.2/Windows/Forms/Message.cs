using System;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020002F9 RID: 761
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public struct Message
	{
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x000D92E7 File Offset: 0x000D74E7
		// (set) Token: 0x06003041 RID: 12353 RVA: 0x000D92EF File Offset: 0x000D74EF
		public IntPtr HWnd
		{
			get
			{
				return this.hWnd;
			}
			set
			{
				this.hWnd = value;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x000D92F8 File Offset: 0x000D74F8
		// (set) Token: 0x06003043 RID: 12355 RVA: 0x000D9300 File Offset: 0x000D7500
		public int Msg
		{
			get
			{
				return this.msg;
			}
			set
			{
				this.msg = value;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000D9309 File Offset: 0x000D7509
		// (set) Token: 0x06003045 RID: 12357 RVA: 0x000D9311 File Offset: 0x000D7511
		public IntPtr WParam
		{
			get
			{
				return this.wparam;
			}
			set
			{
				this.wparam = value;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x000D931A File Offset: 0x000D751A
		// (set) Token: 0x06003047 RID: 12359 RVA: 0x000D9322 File Offset: 0x000D7522
		public IntPtr LParam
		{
			get
			{
				return this.lparam;
			}
			set
			{
				this.lparam = value;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x000D932B File Offset: 0x000D752B
		// (set) Token: 0x06003049 RID: 12361 RVA: 0x000D9333 File Offset: 0x000D7533
		public IntPtr Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000D933C File Offset: 0x000D753C
		public object GetLParam(Type cls)
		{
			return UnsafeNativeMethods.PtrToStructure(this.lparam, cls);
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000D934C File Offset: 0x000D754C
		public static Message Create(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			return new Message
			{
				hWnd = hWnd,
				msg = msg,
				wparam = wparam,
				lparam = lparam,
				result = IntPtr.Zero
			};
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000D9390 File Offset: 0x000D7590
		public override bool Equals(object o)
		{
			if (!(o is Message))
			{
				return false;
			}
			Message message = (Message)o;
			return this.hWnd == message.hWnd && this.msg == message.msg && this.wparam == message.wparam && this.lparam == message.lparam && this.result == message.result;
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000D9408 File Offset: 0x000D7608
		public static bool operator !=(Message a, Message b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000D9420 File Offset: 0x000D7620
		public static bool operator ==(Message a, Message b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000D9435 File Offset: 0x000D7635
		public override int GetHashCode()
		{
			return (int)this.hWnd << 4 | this.msg;
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000D944C File Offset: 0x000D764C
		public override string ToString()
		{
			bool flag = false;
			try
			{
				IntSecurity.UnmanagedCode.Demand();
				flag = true;
			}
			catch (SecurityException)
			{
			}
			if (flag)
			{
				return MessageDecoder.ToString(this);
			}
			return base.ToString();
		}

		// Token: 0x040013EA RID: 5098
		private IntPtr hWnd;

		// Token: 0x040013EB RID: 5099
		private int msg;

		// Token: 0x040013EC RID: 5100
		private IntPtr wparam;

		// Token: 0x040013ED RID: 5101
		private IntPtr lparam;

		// Token: 0x040013EE RID: 5102
		private IntPtr result;
	}
}
