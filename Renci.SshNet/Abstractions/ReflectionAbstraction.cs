using System;
using System.Collections.Generic;
using System.Linq;

namespace Renci.SshNet.Abstractions
{
	// Token: 0x02000118 RID: 280
	internal static class ReflectionAbstraction
	{
		// Token: 0x06000C12 RID: 3090 RVA: 0x000271D8 File Offset: 0x000253D8
		public static IEnumerable<T> GetCustomAttributes<T>(this Type type, bool inherit) where T : Attribute
		{
			return new List<T>(type.GetCustomAttributes(typeof(T), inherit).Cast<T>());
		}
	}
}
