using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000630 RID: 1584
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class SecurityAttribute : Attribute
	{
		// Token: 0x06003929 RID: 14633 RVA: 0x000C133F File Offset: 0x000C033F
		protected SecurityAttribute(SecurityAction action)
		{
			this.m_action = action;
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600392A RID: 14634 RVA: 0x000C134E File Offset: 0x000C034E
		// (set) Token: 0x0600392B RID: 14635 RVA: 0x000C1356 File Offset: 0x000C0356
		public SecurityAction Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600392C RID: 14636 RVA: 0x000C135F File Offset: 0x000C035F
		// (set) Token: 0x0600392D RID: 14637 RVA: 0x000C1367 File Offset: 0x000C0367
		public bool Unrestricted
		{
			get
			{
				return this.m_unrestricted;
			}
			set
			{
				this.m_unrestricted = value;
			}
		}

		// Token: 0x0600392E RID: 14638
		public abstract IPermission CreatePermission();

		// Token: 0x0600392F RID: 14639 RVA: 0x000C1370 File Offset: 0x000C0370
		internal static IntPtr FindSecurityAttributeTypeHandle(string typeName)
		{
			PermissionSet.s_fullTrust.Assert();
			Type type = Type.GetType(typeName, false, false);
			if (type == null)
			{
				return IntPtr.Zero;
			}
			return type.TypeHandle.Value;
		}

		// Token: 0x04001DAE RID: 7598
		internal SecurityAction m_action;

		// Token: 0x04001DAF RID: 7599
		internal bool m_unrestricted;
	}
}
