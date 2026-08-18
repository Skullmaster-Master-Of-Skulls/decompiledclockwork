using System;
using System.Reflection;
using System.Web.Compilation;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005B2 RID: 1458
	internal static class WebPartUtil
	{
		// Token: 0x060049BC RID: 18876 RVA: 0x000F4EC5 File Offset: 0x000F30C5
		internal static object CreateObjectFromType(Type type)
		{
			return HttpRuntime.FastCreatePublicInstance(type);
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x000F4ECD File Offset: 0x000F30CD
		internal static Type DeserializeType(string typeName, bool throwOnError)
		{
			return BuildManager.GetType(typeName, throwOnError);
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x000F4ED8 File Offset: 0x000F30D8
		internal static Type[] GetTypesForConstructor(ConstructorInfo constructor)
		{
			ParameterInfo[] parameters = constructor.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x000F4F10 File Offset: 0x000F3110
		internal static bool IsConnectionPointTypeValid(Type connectionPointType, bool isConsumer)
		{
			if (connectionPointType == null)
			{
				return true;
			}
			if (!connectionPointType.IsPublic && !connectionPointType.IsNestedPublic)
			{
				return false;
			}
			Type c = isConsumer ? typeof(ConsumerConnectionPoint) : typeof(ProviderConnectionPoint);
			if (!connectionPointType.IsSubclassOf(c))
			{
				return false;
			}
			Type[] types = isConsumer ? ConsumerConnectionPoint.ConstructorTypes : ProviderConnectionPoint.ConstructorTypes;
			ConstructorInfo constructor = connectionPointType.GetConstructor(types);
			return !(constructor == null);
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x000F4F83 File Offset: 0x000F3183
		internal static string SerializeType(Type type)
		{
			if (type.Assembly.GlobalAssemblyCache)
			{
				return type.AssemblyQualifiedName;
			}
			return type.FullName;
		}
	}
}
