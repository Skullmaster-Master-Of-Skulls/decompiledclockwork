using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000442 RID: 1090
	internal static class AdministrationHelpers
	{
		// Token: 0x06002A7F RID: 10879 RVA: 0x000A4260 File Offset: 0x000A2460
		public static Type GetServiceModelBaseType(Type type)
		{
			Type type2 = type;
			while (null != type2 && (!type2.IsPublic || !(type2.Assembly == typeof(BindingElement).Assembly)))
			{
				type2 = type2.BaseType;
			}
			return type2;
		}
	}
}
