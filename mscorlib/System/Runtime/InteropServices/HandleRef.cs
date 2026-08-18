using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000510 RID: 1296
	[ComVisible(true)]
	public struct HandleRef
	{
		// Token: 0x060031DD RID: 12765 RVA: 0x000AA291 File Offset: 0x000A9291
		public HandleRef(object wrapper, IntPtr handle)
		{
			this.m_wrapper = wrapper;
			this.m_handle = handle;
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x000AA2A1 File Offset: 0x000A92A1
		public object Wrapper
		{
			get
			{
				return this.m_wrapper;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000AA2A9 File Offset: 0x000A92A9
		public IntPtr Handle
		{
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000AA2B1 File Offset: 0x000A92B1
		public static explicit operator IntPtr(HandleRef value)
		{
			return value.m_handle;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000AA2BA File Offset: 0x000A92BA
		public static IntPtr ToIntPtr(HandleRef value)
		{
			return value.m_handle;
		}

		// Token: 0x040019CC RID: 6604
		internal object m_wrapper;

		// Token: 0x040019CD RID: 6605
		internal IntPtr m_handle;
	}
}
