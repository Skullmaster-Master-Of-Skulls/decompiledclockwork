using System;
using System.Reflection;
using System.Web.Compilation;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000745 RID: 1861
	internal static class WebPartUtil
	{
		// Token: 0x06005A46 RID: 23110 RVA: 0x0016C4D1 File Offset: 0x0016B4D1
		internal static object CreateObjectFromType(Type type)
		{
			return HttpRuntime.FastCreatePublicInstance(type);
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x0016C4D9 File Offset: 0x0016B4D9
		internal static Type DeserializeType(string typeName, bool throwOnError)
		{
			return BuildManager.GetType(typeName, throwOnError);
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x0016C4E4 File Offset: 0x0016B4E4
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

		// Token: 0x06005A49 RID: 23113 RVA: 0x0016C51C File Offset: 0x0016B51C
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
			return connectionPointType.GetConstructor(types) != null;
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x0016C583 File Offset: 0x0016B583
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
