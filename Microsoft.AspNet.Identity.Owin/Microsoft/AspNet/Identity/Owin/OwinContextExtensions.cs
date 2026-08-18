using System;
using Microsoft.Owin;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x0200000E RID: 14
	public static class OwinContextExtensions
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00003A6C File Offset: 0x00001C6C
		private static string GetKey(Type t)
		{
			return OwinContextExtensions.IdentityKeyPrefix + t.AssemblyQualifiedName;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003A7E File Offset: 0x00001C7E
		public static IOwinContext Set<T>(this IOwinContext context, T value)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return context.Set<T>(OwinContextExtensions.GetKey(typeof(T)), value);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public static T Get<T>(this IOwinContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return context.Get<T>(OwinContextExtensions.GetKey(typeof(T)));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003AC9 File Offset: 0x00001CC9
		public static TManager GetUserManager<TManager>(this IOwinContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return context.Get<TManager>();
		}

		// Token: 0x0400000F RID: 15
		private static readonly string IdentityKeyPrefix = "AspNet.Identity.Owin:";
	}
}
