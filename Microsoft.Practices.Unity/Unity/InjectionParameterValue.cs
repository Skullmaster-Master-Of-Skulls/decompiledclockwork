using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000015 RID: 21
	public abstract class InjectionParameterValue
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000044 RID: 68
		public abstract string ParameterTypeName { get; }

		// Token: 0x06000045 RID: 69
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter is meaningful enough in context")]
		public abstract bool MatchesType(Type t);

		// Token: 0x06000046 RID: 70
		public abstract IDependencyResolverPolicy GetResolverPolicy(Type typeToBuild);

		// Token: 0x06000047 RID: 71 RVA: 0x00002B98 File Offset: 0x00000D98
		public static IEnumerable<InjectionParameterValue> ToParameters(params object[] values)
		{
			foreach (object value in values)
			{
				yield return InjectionParameterValue.ToParameter(value);
			}
			yield break;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public static InjectionParameterValue ToParameter(object value)
		{
			InjectionParameterValue injectionParameterValue = value as InjectionParameterValue;
			if (injectionParameterValue != null)
			{
				return injectionParameterValue;
			}
			Type type = value as Type;
			if (type != null)
			{
				return new ResolvedParameter(type);
			}
			return new InjectionParameter(value);
		}
	}
}
