using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Microsoft.Practices.Unity.Utility
{
	// Token: 0x020000AD RID: 173
	public class ReflectionHelper
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000AF87 File Offset: 0x00009187
		public ReflectionHelper(Type typeToReflect)
		{
			this.t = typeToReflect;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000AF96 File Offset: 0x00009196
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This is the type part of the key.")]
		public Type Type
		{
			get
			{
				return this.t;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000AF9E File Offset: 0x0000919E
		public bool IsGenericType
		{
			get
			{
				return this.t.GetTypeInfo().IsGenericType;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000AFB0 File Offset: 0x000091B0
		public bool IsOpenGeneric
		{
			get
			{
				return this.t.GetTypeInfo().IsGenericType && this.t.GetTypeInfo().ContainsGenericParameters;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000AFD6 File Offset: 0x000091D6
		public bool IsArray
		{
			get
			{
				return this.t.IsArray;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000AFE3 File Offset: 0x000091E3
		public bool IsGenericArray
		{
			get
			{
				return this.IsArray && this.ArrayElementType.GetTypeInfo().IsGenericParameter;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000AFFF File Offset: 0x000091FF
		public Type ArrayElementType
		{
			get
			{
				return this.t.GetElementType();
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000B00C File Offset: 0x0000920C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done via Guard class")]
		public static bool MethodHasOpenGenericParameters(MethodBase method)
		{
			Guard.ArgumentNotNull(method, "method");
			foreach (ParameterInfo parameterInfo in method.GetParameters())
			{
				ReflectionHelper reflectionHelper = new ReflectionHelper(parameterInfo.ParameterType);
				if (reflectionHelper.IsOpenGeneric)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000B060 File Offset: 0x00009260
		public Type GetClosedParameterType(Type[] genericArguments)
		{
			Guard.ArgumentNotNull(genericArguments, "genericArguments");
			if (this.IsOpenGeneric)
			{
				TypeInfo typeInfo = this.Type.GetTypeInfo();
				Type[] array = typeInfo.IsGenericTypeDefinition ? typeInfo.GenericTypeParameters : typeInfo.GenericTypeArguments;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = genericArguments[array[i].GenericParameterPosition];
				}
				return this.Type.GetGenericTypeDefinition().MakeGenericType(array);
			}
			if (this.Type.GetTypeInfo().IsGenericParameter)
			{
				return genericArguments[this.Type.GenericParameterPosition];
			}
			if (!this.IsArray || !this.ArrayElementType.GetTypeInfo().IsGenericParameter)
			{
				return this.Type;
			}
			int arrayRank;
			if ((arrayRank = this.Type.GetArrayRank()) == 1)
			{
				return genericArguments[this.Type.GetElementType().GenericParameterPosition].MakeArrayType();
			}
			return genericArguments[this.Type.GetElementType().GenericParameterPosition].MakeArrayType(arrayRank);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000B154 File Offset: 0x00009354
		public Type GetNamedGenericParameter(string parameterName)
		{
			TypeInfo typeInfo = this.Type.GetGenericTypeDefinition().GetTypeInfo();
			Type result = null;
			int num = -1;
			foreach (Type type in typeInfo.GenericTypeParameters)
			{
				if (type.GetTypeInfo().Name == parameterName)
				{
					num = type.GenericParameterPosition;
					break;
				}
			}
			if (num != -1)
			{
				result = this.Type.GenericTypeArguments[num];
			}
			return result;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public IEnumerable<ConstructorInfo> InstanceConstructors
		{
			get
			{
				return from c in this.Type.GetTypeInfo().DeclaredConstructors
				where !c.IsStatic && c.IsPublic
				select c;
			}
		}

		// Token: 0x040000C5 RID: 197
		private readonly Type t;
	}
}
