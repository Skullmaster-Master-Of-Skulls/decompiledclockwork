using System;
using System.Reflection;

namespace System.Web.Http.Internal
{
	// Token: 0x020000FA RID: 250
	internal static class MemberInfoExtensions
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x000143E8 File Offset: 0x000125E8
		public static TAttribute[] GetCustomAttributes<TAttribute>(this MemberInfo member, bool inherit) where TAttribute : class
		{
			if (member == null)
			{
				throw Error.ArgumentNull("member");
			}
			return (TAttribute[])member.GetCustomAttributes(typeof(TAttribute), inherit);
		}
	}
}
