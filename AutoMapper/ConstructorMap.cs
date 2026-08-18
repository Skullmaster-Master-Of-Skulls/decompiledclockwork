using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Internal;
using AutoMapper.QueryableExtensions;
using AutoMapper.QueryableExtensions.Impl;

namespace AutoMapper
{
	// Token: 0x0200000A RID: 10
	public class ConstructorMap
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002CFD File Offset: 0x00000EFD
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002D05 File Offset: 0x00000F05
		public ConstructorInfo Ctor { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002D0E File Offset: 0x00000F0E
		public IEnumerable<ConstructorParameterMap> CtorParams { get; }

		// Token: 0x0600003E RID: 62 RVA: 0x00002D18 File Offset: 0x00000F18
		public ConstructorMap(ConstructorInfo ctor, IEnumerable<ConstructorParameterMap> ctorParams)
		{
			ConstructorMap <>4__this = this;
			this.Ctor = ctor;
			this.CtorParams = ctorParams;
			this._runtimeCtor = new Lazy<LateBoundParamsCtor>(() => ConstructorMap.DelegateFactory.CreateCtor(ctor, <>4__this.CtorParams));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D6C File Offset: 0x00000F6C
		public Expression NewExpression(Expression instanceParameter)
		{
			IEnumerable<ExpressionResolutionResult> source = this.CtorParams.Select(delegate(ConstructorParameterMap map)
			{
				ExpressionResolutionResult result = new ExpressionResolutionResult(instanceParameter, this.Ctor.DeclaringType);
				IValueResolver[] sourceResolvers = map.SourceResolvers;
				for (int i = 0; i < sourceResolvers.Length; i++)
				{
					IValueResolver resolver = sourceResolvers[i];
					IExpressionResultConverter expressionResultConverter = ConstructorMap.ExpressionResultConverters.FirstOrDefault((IExpressionResultConverter c) => c.CanGetExpressionResolutionResult(result, resolver));
					if (expressionResultConverter == null)
					{
						throw new Exception("Can't resolve this to Queryable Expression");
					}
					result = expressionResultConverter.GetExpressionResolutionResult(result, map, resolver);
				}
				return result;
			});
			return Expression.New(this.Ctor, from p in source
			select p.ResolutionExpression);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public object ResolveValue(ResolutionContext context)
		{
			List<object> list = new List<object>();
			foreach (ConstructorParameterMap constructorParameterMap in this.CtorParams)
			{
				ResolutionResult resolutionResult = constructorParameterMap.ResolveValue(context);
				Type type = resolutionResult.Type;
				Type parameterType = constructorParameterMap.Parameter.ParameterType;
				TypeMap typeMap = context.ConfigurationProvider.ResolveTypeMap(resolutionResult, parameterType);
				Type sourceType = (typeMap != null) ? typeMap.SourceType : type;
				ResolutionContext context2 = context.CreateTypeContext(typeMap, resolutionResult.Value, null, sourceType, parameterType);
				if (typeMap == null && constructorParameterMap.Parameter.IsOptional)
				{
					object defaultValue = constructorParameterMap.Parameter.DefaultValue;
					list.Add(defaultValue);
				}
				else
				{
					object item = context.Engine.Map(context2);
					list.Add(item);
				}
			}
			return this._runtimeCtor.Value(list.ToArray());
		}

		// Token: 0x04000012 RID: 18
		private static readonly DelegateFactory DelegateFactory = new DelegateFactory();

		// Token: 0x04000013 RID: 19
		private readonly Lazy<LateBoundParamsCtor> _runtimeCtor;

		// Token: 0x04000016 RID: 22
		private static readonly IExpressionResultConverter[] ExpressionResultConverters = new IExpressionResultConverter[]
		{
			new MemberGetterExpressionResultConverter(),
			new MemberResolverExpressionResultConverter(),
			new NullSubstitutionExpressionResultConverter()
		};
	}
}
