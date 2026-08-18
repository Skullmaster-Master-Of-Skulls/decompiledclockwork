using System;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000520 RID: 1312
	[ComVisible(true)]
	[Serializable]
	public sealed class ErrorWrapper
	{
		// Token: 0x060032DF RID: 13023 RVA: 0x000ABBD3 File Offset: 0x000AABD3
		public ErrorWrapper(int errorCode)
		{
			this.m_ErrorCode = errorCode;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000ABBE2 File Offset: 0x000AABE2
		public ErrorWrapper(object errorCode)
		{
			if (!(errorCode is int))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt32"), "errorCode");
			}
			this.m_ErrorCode = (int)errorCode;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000ABC13 File Offset: 0x000AAC13
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public ErrorWrapper(Exception e)
		{
			this.m_ErrorCode = Marshal.GetHRForException(e);
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000ABC27 File Offset: 0x000AAC27
		public int ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
		}

		// Token: 0x040019FC RID: 6652
		private int m_ErrorCode;
	}
}
