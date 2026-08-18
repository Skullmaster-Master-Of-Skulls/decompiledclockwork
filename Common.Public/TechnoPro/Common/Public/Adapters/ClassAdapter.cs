using System;
using System.Reflection;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005E9 RID: 1513
	public static class ClassAdapter
	{
		// Token: 0x060030C6 RID: 12486 RVA: 0x000426B8 File Offset: 0x000408B8
		public static T GetCustomAttribute<T>(Type classType) where T : Attribute
		{
			return (T)((object)((classType != null) ? classType.GetCustomAttribute(typeof(T), true) : null));
		}
	}
}
