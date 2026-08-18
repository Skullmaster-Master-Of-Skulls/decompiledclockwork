using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Internal;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000CF RID: 207
	public class ReflectedHttpActionDescriptor : HttpActionDescriptor
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x0000FF20 File Offset: 0x0000E120
		public ReflectedHttpActionDescriptor()
		{
			this._parameters = new Lazy<Collection<HttpParameterDescriptor>>(() => this.InitializeParameterDescriptors());
			this._supportedHttpMethods = new Collection<HttpMethod>();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000FF64 File Offset: 0x0000E164
		public ReflectedHttpActionDescriptor(HttpControllerDescriptor controllerDescriptor, MethodInfo methodInfo) : base(controllerDescriptor)
		{
			if (methodInfo == null)
			{
				throw Error.ArgumentNull("methodInfo");
			}
			this.InitializeProperties(methodInfo);
			this._parameters = new Lazy<Collection<HttpParameterDescriptor>>(() => this.InitializeParameterDescriptors());
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000FFB1 File Offset: 0x0000E1B1
		public override string ActionName
		{
			get
			{
				return this._actionName;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000FFB9 File Offset: 0x0000E1B9
		public override Collection<HttpMethod> SupportedHttpMethods
		{
			get
			{
				return this._supportedHttpMethods;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000FFC1 File Offset: 0x0000E1C1
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0000FFC9 File Offset: 0x0000E1C9
		public MethodInfo MethodInfo
		{
			get
			{
				return this._methodInfo;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this.InitializeProperties(value);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0000FFE1 File Offset: 0x0000E1E1
		private ParameterInfo[] ParameterInfos
		{
			get
			{
				if (this._parameterInfos == null)
				{
					this._parameterInfos = this._methodInfo.GetParameters();
				}
				return this._parameterInfos;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00010002 File Offset: 0x0000E202
		public override Type ReturnType
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001000C File Offset: 0x0000E20C
		public override Collection<T> GetCustomAttributes<T>(bool inherit)
		{
			object[] objects = inherit ? this._attributeCache : this._declaredOnlyAttributeCache;
			return new Collection<T>(TypeHelper.OfType<T>(objects));
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00010038 File Offset: 0x0000E238
		public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			if (arguments == null)
			{
				throw Error.ArgumentNull("arguments");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelpers.Canceled<object>();
			}
			Task<object> result;
			try
			{
				object[] arguments2 = this.PrepareParameters(arguments, controllerContext);
				result = this._actionExecutor.Value.Execute(controllerContext.Controller, arguments2);
			}
			catch (Exception exception)
			{
				result = TaskHelpers.FromError<object>(exception);
			}
			return result;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000100B0 File Offset: 0x0000E2B0
		public override Collection<IFilter> GetFilters()
		{
			return new Collection<IFilter>(this.GetCustomAttributes<IFilter>().Concat(base.GetFilters()).ToList<IFilter>());
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000100CD File Offset: 0x0000E2CD
		public override Collection<HttpParameterDescriptor> GetParameters()
		{
			return this._parameters.Value;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000100E8 File Offset: 0x0000E2E8
		private void InitializeProperties(MethodInfo methodInfo)
		{
			this._methodInfo = methodInfo;
			this._parameterInfos = null;
			this._returnType = ReflectedHttpActionDescriptor.GetReturnType(methodInfo);
			this._actionExecutor = new Lazy<ReflectedHttpActionDescriptor.ActionExecutor>(() => ReflectedHttpActionDescriptor.InitializeActionExecutor(this._methodInfo));
			this._declaredOnlyAttributeCache = this._methodInfo.GetCustomAttributes(false);
			this._attributeCache = this._methodInfo.GetCustomAttributes(true);
			this._actionName = ReflectedHttpActionDescriptor.GetActionName(this._methodInfo, this._attributeCache);
			this._supportedHttpMethods = ReflectedHttpActionDescriptor.GetSupportedHttpMethods(this._methodInfo, this._attributeCache);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00010178 File Offset: 0x0000E378
		internal static Type GetReturnType(MethodInfo methodInfo)
		{
			Type type = methodInfo.ReturnType;
			if (typeof(Task).IsAssignableFrom(type))
			{
				type = TypeHelper.GetTaskInnerTypeOrNull(methodInfo.ReturnType);
			}
			if (type == typeof(void))
			{
				type = null;
			}
			return type;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000101C8 File Offset: 0x0000E3C8
		private Collection<HttpParameterDescriptor> InitializeParameterDescriptors()
		{
			List<HttpParameterDescriptor> list = (from item in this.ParameterInfos
			select new ReflectedHttpParameterDescriptor(this, item)).ToList<HttpParameterDescriptor>();
			return new Collection<HttpParameterDescriptor>(list);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000101F8 File Offset: 0x0000E3F8
		private object[] PrepareParameters(IDictionary<string, object> parameters, HttpControllerContext controllerContext)
		{
			if (this._parameters.Value.Count == 0)
			{
				return ReflectedHttpActionDescriptor._empty;
			}
			ParameterInfo[] parameterInfos = this.ParameterInfos;
			int num = parameterInfos.Length;
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.ExtractParameterFromDictionary(parameterInfos[i], parameters, controllerContext);
			}
			return array;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001024C File Offset: 0x0000E44C
		private object ExtractParameterFromDictionary(ParameterInfo parameterInfo, IDictionary<string, object> parameters, HttpControllerContext controllerContext)
		{
			object obj;
			if (!parameters.TryGetValue(parameterInfo.Name, out obj))
			{
				throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, SRResources.BadRequest, Error.Format(SRResources.ReflectedActionDescriptor_ParameterNotInDictionary, new object[]
				{
					parameterInfo.Name,
					parameterInfo.ParameterType,
					this.MethodInfo,
					this.MethodInfo.DeclaringType
				})));
			}
			if (obj == null && !TypeHelper.TypeAllowsNullValue(parameterInfo.ParameterType))
			{
				throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, SRResources.BadRequest, Error.Format(SRResources.ReflectedActionDescriptor_ParameterCannotBeNull, new object[]
				{
					parameterInfo.Name,
					parameterInfo.ParameterType,
					this.MethodInfo,
					this.MethodInfo.DeclaringType
				})));
			}
			if (obj != null && !parameterInfo.ParameterType.IsInstanceOfType(obj))
			{
				throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, SRResources.BadRequest, Error.Format(SRResources.ReflectedActionDescriptor_ParameterValueHasWrongType, new object[]
				{
					parameterInfo.Name,
					this.MethodInfo,
					this.MethodInfo.DeclaringType,
					obj.GetType(),
					parameterInfo.ParameterType
				})));
			}
			return obj;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00010398 File Offset: 0x0000E598
		private static string GetActionName(MethodInfo methodInfo, object[] actionAttributes)
		{
			ActionNameAttribute actionNameAttribute = TypeHelper.OfType<ActionNameAttribute>(actionAttributes).FirstOrDefault<ActionNameAttribute>();
			if (actionNameAttribute == null)
			{
				return methodInfo.Name;
			}
			return actionNameAttribute.Name;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000103C4 File Offset: 0x0000E5C4
		private static Collection<HttpMethod> GetSupportedHttpMethods(MethodInfo methodInfo, object[] actionAttributes)
		{
			Collection<HttpMethod> collection = new Collection<HttpMethod>();
			ICollection<IActionHttpMethodProvider> collection2 = TypeHelper.OfType<IActionHttpMethodProvider>(actionAttributes);
			if (collection2.Count > 0)
			{
				using (IEnumerator<IActionHttpMethodProvider> enumerator = collection2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IActionHttpMethodProvider actionHttpMethodProvider = enumerator.Current;
						foreach (HttpMethod item in actionHttpMethodProvider.HttpMethods)
						{
							collection.Add(item);
						}
					}
					goto IL_B5;
				}
			}
			for (int i = 0; i < ReflectedHttpActionDescriptor._supportedHttpMethodsByConvention.Length; i++)
			{
				if (methodInfo.Name.StartsWith(ReflectedHttpActionDescriptor._supportedHttpMethodsByConvention[i].Method, StringComparison.OrdinalIgnoreCase))
				{
					collection.Add(ReflectedHttpActionDescriptor._supportedHttpMethodsByConvention[i]);
					break;
				}
			}
			IL_B5:
			if (collection.Count == 0)
			{
				collection.Add(HttpMethod.Post);
			}
			return collection;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000104B8 File Offset: 0x0000E6B8
		public override int GetHashCode()
		{
			if (this._methodInfo != null)
			{
				return this._methodInfo.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000104DC File Offset: 0x0000E6DC
		public override bool Equals(object obj)
		{
			if (this._methodInfo != null)
			{
				ReflectedHttpActionDescriptor reflectedHttpActionDescriptor = obj as ReflectedHttpActionDescriptor;
				return reflectedHttpActionDescriptor != null && this._methodInfo.Equals(reflectedHttpActionDescriptor._methodInfo);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001051C File Offset: 0x0000E71C
		private static ReflectedHttpActionDescriptor.ActionExecutor InitializeActionExecutor(MethodInfo methodInfo)
		{
			if (methodInfo.ContainsGenericParameters)
			{
				throw Error.InvalidOperation(SRResources.ReflectedHttpActionDescriptor_CannotCallOpenGenericMethods, new object[]
				{
					methodInfo,
					methodInfo.ReflectedType.FullName
				});
			}
			return new ReflectedHttpActionDescriptor.ActionExecutor(methodInfo);
		}

		// Token: 0x04000175 RID: 373
		private static readonly object[] _empty = new object[0];

		// Token: 0x04000176 RID: 374
		private readonly Lazy<Collection<HttpParameterDescriptor>> _parameters;

		// Token: 0x04000177 RID: 375
		private ParameterInfo[] _parameterInfos;

		// Token: 0x04000178 RID: 376
		private Lazy<ReflectedHttpActionDescriptor.ActionExecutor> _actionExecutor;

		// Token: 0x04000179 RID: 377
		private MethodInfo _methodInfo;

		// Token: 0x0400017A RID: 378
		private Type _returnType;

		// Token: 0x0400017B RID: 379
		private string _actionName;

		// Token: 0x0400017C RID: 380
		private Collection<HttpMethod> _supportedHttpMethods;

		// Token: 0x0400017D RID: 381
		private object[] _attributeCache;

		// Token: 0x0400017E RID: 382
		private object[] _declaredOnlyAttributeCache;

		// Token: 0x0400017F RID: 383
		private static readonly HttpMethod[] _supportedHttpMethodsByConvention = new HttpMethod[]
		{
			HttpMethod.Get,
			HttpMethod.Post,
			HttpMethod.Put,
			HttpMethod.Delete,
			HttpMethod.Head,
			HttpMethod.Options,
			new HttpMethod("PATCH")
		};

		// Token: 0x020000D0 RID: 208
		private sealed class ActionExecutor
		{
			// Token: 0x06000511 RID: 1297 RVA: 0x000105BE File Offset: 0x0000E7BE
			public ActionExecutor(MethodInfo methodInfo)
			{
				this._executor = ReflectedHttpActionDescriptor.ActionExecutor.GetExecutor(methodInfo);
			}

			// Token: 0x06000512 RID: 1298 RVA: 0x000105D2 File Offset: 0x0000E7D2
			public Task<object> Execute(object instance, object[] arguments)
			{
				return this._executor(instance, arguments);
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x000105E4 File Offset: 0x0000E7E4
			private static Task<object> Convert<T>(object taskAsObject)
			{
				Task<T> task = (Task<T>)taskAsObject;
				return task.CastToObject<T>();
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x00010600 File Offset: 0x0000E800
			[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
			private static Func<object, Task<object>> CompileGenericTaskConversionDelegate(Type taskValueType)
			{
				return (Func<object, Task<object>>)Delegate.CreateDelegate(typeof(Func<object, Task<object>>), ReflectedHttpActionDescriptor.ActionExecutor._convertOfTMethod.MakeGenericMethod(new Type[]
				{
					taskValueType
				}));
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x00010744 File Offset: 0x0000E944
			private static Func<object, object[], Task<object>> GetExecutor(MethodInfo methodInfo)
			{
				ReflectedHttpActionDescriptor.ActionExecutor.<>c__DisplayClassa CS$<>8__locals1 = new ReflectedHttpActionDescriptor.ActionExecutor.<>c__DisplayClassa();
				CS$<>8__locals1.methodInfo = methodInfo;
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "instance");
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), "parameters");
				List<Expression> list = new List<Expression>();
				ParameterInfo[] parameters = CS$<>8__locals1.methodInfo.GetParameters();
				for (int i = 0; i < parameters.Length; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					BinaryExpression expression = Expression.ArrayIndex(parameterExpression2, Expression.Constant(i));
					UnaryExpression item = Expression.Convert(expression, parameterInfo.ParameterType);
					list.Add(item);
				}
				UnaryExpression instance2 = (!CS$<>8__locals1.methodInfo.IsStatic) ? Expression.Convert(parameterExpression, CS$<>8__locals1.methodInfo.ReflectedType) : null;
				MethodCallExpression methodCallExpression = Expression.Call(instance2, CS$<>8__locals1.methodInfo, list);
				if (methodCallExpression.Type == typeof(void))
				{
					Expression<Action<object, object[]>> expression2 = Expression.Lambda<Action<object, object[]>>(methodCallExpression, new ParameterExpression[]
					{
						parameterExpression,
						parameterExpression2
					});
					Action<object, object[]> voidExecutor = expression2.Compile();
					return delegate(object instance, object[] methodParameters)
					{
						voidExecutor(instance, methodParameters);
						return TaskHelpers.NullResult();
					};
				}
				UnaryExpression body = Expression.Convert(methodCallExpression, typeof(object));
				Expression<Func<object, object[], object>> expression3 = Expression.Lambda<Func<object, object[], object>>(body, new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression2
				});
				Func<object, object[], object> compiled = expression3.Compile();
				if (methodCallExpression.Type == typeof(Task))
				{
					return delegate(object instance, object[] methodParameters)
					{
						Task task = (Task)compiled(instance, methodParameters);
						ReflectedHttpActionDescriptor.ActionExecutor.ThrowIfWrappedTaskInstance(CS$<>8__locals1.methodInfo, task.GetType());
						return task.CastToObject();
					};
				}
				if (typeof(Task).IsAssignableFrom(methodCallExpression.Type))
				{
					Type taskInnerTypeOrNull = TypeHelper.GetTaskInnerTypeOrNull(methodCallExpression.Type);
					Func<object, Task<object>> compiledConversion = ReflectedHttpActionDescriptor.ActionExecutor.CompileGenericTaskConversionDelegate(taskInnerTypeOrNull);
					return delegate(object instance, object[] methodParameters)
					{
						object arg = compiled(instance, methodParameters);
						return compiledConversion(arg);
					};
				}
				return delegate(object instance, object[] methodParameters)
				{
					object obj = compiled(instance, methodParameters);
					Task task = obj as Task;
					if (task != null)
					{
						throw Error.InvalidOperation(SRResources.ActionExecutor_UnexpectedTaskInstance, new object[]
						{
							CS$<>8__locals1.methodInfo.Name,
							CS$<>8__locals1.methodInfo.DeclaringType.Name
						});
					}
					return Task.FromResult<object>(obj);
				};
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x00010970 File Offset: 0x0000EB70
			private static void ThrowIfWrappedTaskInstance(MethodInfo method, Type type)
			{
				if (type != typeof(Task))
				{
					Type taskInnerTypeOrNull = TypeHelper.GetTaskInnerTypeOrNull(type);
					if (taskInnerTypeOrNull != null && typeof(Task).IsAssignableFrom(taskInnerTypeOrNull))
					{
						throw Error.InvalidOperation(SRResources.ActionExecutor_WrappedTaskInstance, new object[]
						{
							method.Name,
							method.DeclaringType.Name,
							type.FullName
						});
					}
				}
			}

			// Token: 0x04000180 RID: 384
			private readonly Func<object, object[], Task<object>> _executor;

			// Token: 0x04000181 RID: 385
			private static MethodInfo _convertOfTMethod = typeof(ReflectedHttpActionDescriptor.ActionExecutor).GetMethod("Convert", BindingFlags.Static | BindingFlags.NonPublic);
		}
	}
}
