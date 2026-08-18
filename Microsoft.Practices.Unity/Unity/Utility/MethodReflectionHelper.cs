using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Practices.Unity.Utility
{
	// Token: 0x020000A9 RID: 169
	public class MethodReflectionHelper
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0000A959 File Offset: 0x00008B59
		public MethodReflectionHelper(MethodBase method)
		{
			this.method = method;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000A970 File Offset: 0x00008B70
		public bool MethodHasOpenGenericParameters
		{
			get
			{
				return this.GetParameterReflectors().Any((ParameterReflectionHelper r) => r.IsOpenGeneric);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000AB20 File Offset: 0x00008D20
		public IEnumerable<Type> ParameterTypes
		{
			get
			{
				foreach (ParameterInfo param in this.method.GetParameters())
				{
					yield return param.ParameterType;
				}
				yield break;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000AB3D File Offset: 0x00008D3D
		public Type[] GetClosedParameterTypes(Type[] genericTypeArguments)
		{
			return this.GetClosedParameterTypesSequence(genericTypeArguments).ToArray<Type>();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		private IEnumerable<ParameterReflectionHelper> GetParameterReflectors()
		{
			foreach (ParameterInfo pi in this.method.GetParameters())
			{
				yield return new ParameterReflectionHelper(pi);
			}
			yield break;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000AE90 File Offset: 0x00009090
		private IEnumerable<Type> GetClosedParameterTypesSequence(Type[] genericTypeArguments)
		{
			foreach (ParameterReflectionHelper r in this.GetParameterReflectors())
			{
				yield return r.GetClosedParameterType(genericTypeArguments);
			}
			yield break;
		}

		// Token: 0x040000BF RID: 191
		private readonly MethodBase method;
	}
}
