using System;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200051C RID: 1308
	[ComVisible(true)]
	[Serializable]
	public sealed class BStrWrapper
	{
		// Token: 0x060032D8 RID: 13016 RVA: 0x000ABB40 File Offset: 0x000AAB40
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public BStrWrapper(string value)
		{
			this.m_WrappedObject = value;
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x000ABB4F File Offset: 0x000AAB4F
		public string WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x040019F5 RID: 6645
		private string m_WrappedObject;
	}
}
