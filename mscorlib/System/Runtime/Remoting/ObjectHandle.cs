using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
	// Token: 0x0200072F RID: 1839
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class ObjectHandle : MarshalByRefObject, IObjectHandle
	{
		// Token: 0x060041F1 RID: 16881 RVA: 0x000E06D7 File Offset: 0x000DF6D7
		private ObjectHandle()
		{
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000E06DF File Offset: 0x000DF6DF
		public ObjectHandle(object o)
		{
			this.WrappedObject = o;
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000E06EE File Offset: 0x000DF6EE
		public object Unwrap()
		{
			return this.WrappedObject;
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000E06F8 File Offset: 0x000DF6F8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			MarshalByRefObject marshalByRefObject = this.WrappedObject as MarshalByRefObject;
			if (marshalByRefObject != null && marshalByRefObject.InitializeLifetimeService() == null)
			{
				return null;
			}
			return (ILease)base.InitializeLifetimeService();
		}

		// Token: 0x04002116 RID: 8470
		private object WrappedObject;
	}
}
