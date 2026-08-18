using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x020001E7 RID: 487
	internal class ArglessEventHandlerProxy
	{
		// Token: 0x060017FF RID: 6143 RVA: 0x0004B960 File Offset: 0x00049B60
		internal ArglessEventHandlerProxy(object target, MethodInfo arglessMethod)
		{
			this._target = target;
			this._arglessMethod = arglessMethod;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0004B976 File Offset: 0x00049B76
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.RestrictedMemberAccess)]
		internal void Callback(object sender, EventArgs e)
		{
			this._arglessMethod.Invoke(this._target, new object[0]);
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0004B990 File Offset: 0x00049B90
		internal EventHandler Handler
		{
			get
			{
				return new EventHandler(this.Callback);
			}
		}

		// Token: 0x04001768 RID: 5992
		private object _target;

		// Token: 0x04001769 RID: 5993
		private MethodInfo _arglessMethod;
	}
}
