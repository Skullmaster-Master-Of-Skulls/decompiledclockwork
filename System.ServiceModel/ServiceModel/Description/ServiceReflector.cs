using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace System.ServiceModel.Description
{
	// Token: 0x02000423 RID: 1059
	internal static class ServiceReflector
	{
		// Token: 0x0600289F RID: 10399 RVA: 0x00098314 File Offset: 0x00096514
		internal static Type GetOperationContractProviderType(MethodInfo method)
		{
			if (ServiceReflector.GetSingleAttribute<OperationContractAttribute>(method) != null)
			{
				return ServiceReflector.OperationContractAttributeType;
			}
			IOperationContractAttributeProvider firstAttribute = ServiceReflector.GetFirstAttribute<IOperationContractAttributeProvider>(method);
			if (firstAttribute != null)
			{
				return firstAttribute.GetType();
			}
			return null;
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x00098344 File Offset: 0x00096544
		internal static List<Type> GetInterfaces(Type service)
		{
			List<Type> list = new List<Type>();
			bool flag = false;
			if (service.IsDefined(typeof(ServiceContractAttribute), false))
			{
				flag = true;
				list.Add(service);
			}
			if (!flag)
			{
				Type ancestorImplicitContractClass = ServiceReflector.GetAncestorImplicitContractClass(service);
				if (ancestorImplicitContractClass != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxContractInheritanceRequiresInterfaces2", new object[]
					{
						service,
						ancestorImplicitContractClass
					})));
				}
				foreach (MethodInfo methodInfo in ServiceReflector.GetMethodsInternal(service))
				{
					Type operationContractProviderType = ServiceReflector.GetOperationContractProviderType(methodInfo);
					if (operationContractProviderType == ServiceReflector.OperationContractAttributeType)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ServicesWithoutAServiceContractAttributeCan2", new object[]
						{
							operationContractProviderType.Name,
							methodInfo.Name,
							service.FullName
						})));
					}
				}
			}
			foreach (Type type in service.GetInterfaces())
			{
				if (type.IsDefined(typeof(ServiceContractAttribute), false))
				{
					if (flag)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxContractInheritanceRequiresInterfaces", new object[]
						{
							service,
							type
						})));
					}
					list.Add(type);
				}
			}
			return list;
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000984B4 File Offset: 0x000966B4
		private static Type GetAncestorImplicitContractClass(Type service)
		{
			service = service.BaseType;
			while (service != null)
			{
				if (ServiceReflector.GetSingleAttribute<ServiceContractAttribute>(service) != null)
				{
					return service;
				}
				service = service.BaseType;
			}
			return null;
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000984DC File Offset: 0x000966DC
		internal static List<Type> GetInheritedContractTypes(Type service)
		{
			List<Type> list = new List<Type>();
			foreach (Type type in service.GetInterfaces())
			{
				if (ServiceReflector.GetSingleAttribute<ServiceContractAttribute>(type) != null)
				{
					list.Add(type);
				}
			}
			service = service.BaseType;
			while (service != null)
			{
				if (ServiceReflector.GetSingleAttribute<ServiceContractAttribute>(service) != null)
				{
					list.Add(service);
				}
				service = service.BaseType;
			}
			return list;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x00098542 File Offset: 0x00096742
		internal static object[] GetCustomAttributes(ICustomAttributeProvider attrProvider, Type attrType)
		{
			return ServiceReflector.GetCustomAttributes(attrProvider, attrType, false);
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x0009854C File Offset: 0x0009674C
		internal static object[] GetCustomAttributes(ICustomAttributeProvider attrProvider, Type attrType, bool inherit)
		{
			object[] customAttributes;
			try
			{
				customAttributes = attrProvider.GetCustomAttributes(attrType, inherit);
			}
			catch (Exception innerException)
			{
				if (Fx.IsFatal(innerException))
				{
					throw;
				}
				if (innerException is CustomAttributeFormatException && innerException.InnerException != null)
				{
					innerException = innerException.InnerException;
					if (innerException is TargetInvocationException && innerException.InnerException != null)
					{
						innerException = innerException.InnerException;
					}
				}
				Type type = attrProvider as Type;
				MethodInfo methodInfo = attrProvider as MethodInfo;
				ParameterInfo parameterInfo = attrProvider as ParameterInfo;
				if (type != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxErrorReflectingOnType2", new object[]
					{
						attrType.Name,
						type.Name
					}), innerException));
				}
				if (methodInfo != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxErrorReflectingOnMethod3", new object[]
					{
						attrType.Name,
						methodInfo.Name,
						methodInfo.ReflectedType.Name
					}), innerException));
				}
				if (parameterInfo != null)
				{
					methodInfo = (parameterInfo.Member as MethodInfo);
					if (methodInfo != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxErrorReflectingOnParameter4", new object[]
						{
							attrType.Name,
							parameterInfo.Name,
							methodInfo.Name,
							methodInfo.ReflectedType.Name
						}), innerException));
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxErrorReflectionOnUnknown1", new object[]
				{
					attrType.Name
				}), innerException));
			}
			return customAttributes;
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000986E8 File Offset: 0x000968E8
		internal static T GetFirstAttribute<T>(ICustomAttributeProvider attrProvider) where T : class
		{
			Type typeFromHandle = typeof(T);
			object[] customAttributes = ServiceReflector.GetCustomAttributes(attrProvider, typeFromHandle);
			if (customAttributes.Length == 0)
			{
				return default(T);
			}
			return customAttributes[0] as T;
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x00098724 File Offset: 0x00096924
		internal static T GetSingleAttribute<T>(ICustomAttributeProvider attrProvider) where T : class
		{
			Type typeFromHandle = typeof(T);
			object[] customAttributes = ServiceReflector.GetCustomAttributes(attrProvider, typeFromHandle);
			if (customAttributes.Length == 0)
			{
				return default(T);
			}
			if (customAttributes.Length > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("tooManyAttributesOfTypeOn2", new object[]
				{
					typeFromHandle,
					attrProvider.ToString()
				})));
			}
			return customAttributes[0] as T;
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x00098794 File Offset: 0x00096994
		internal static T GetRequiredSingleAttribute<T>(ICustomAttributeProvider attrProvider) where T : class
		{
			T singleAttribute = ServiceReflector.GetSingleAttribute<T>(attrProvider);
			if (singleAttribute == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("couldnTFindRequiredAttributeOfTypeOn2", new object[]
				{
					typeof(T),
					attrProvider.ToString()
				})));
			}
			return singleAttribute;
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000987E8 File Offset: 0x000969E8
		internal static T GetSingleAttribute<T>(ICustomAttributeProvider attrProvider, Type[] attrTypeGroup) where T : class
		{
			T singleAttribute = ServiceReflector.GetSingleAttribute<T>(attrProvider);
			if (singleAttribute != null)
			{
				Type typeFromHandle = typeof(T);
				foreach (Type type in attrTypeGroup)
				{
					if (!(type == typeFromHandle))
					{
						object[] customAttributes = ServiceReflector.GetCustomAttributes(attrProvider, type);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDisallowedAttributeCombination", new object[]
							{
								attrProvider,
								typeFromHandle.FullName,
								type.FullName
							})));
						}
					}
				}
			}
			return singleAttribute;
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x0009887C File Offset: 0x00096A7C
		internal static T GetRequiredSingleAttribute<T>(ICustomAttributeProvider attrProvider, Type[] attrTypeGroup) where T : class
		{
			T singleAttribute = ServiceReflector.GetSingleAttribute<T>(attrProvider, attrTypeGroup);
			if (singleAttribute == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("couldnTFindRequiredAttributeOfTypeOn2", new object[]
				{
					typeof(T),
					attrProvider.ToString()
				})));
			}
			return singleAttribute;
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000988D0 File Offset: 0x00096AD0
		internal static Type GetContractType(Type interfaceType)
		{
			ServiceContractAttribute serviceContractAttribute;
			return ServiceReflector.GetContractTypeAndAttribute(interfaceType, out serviceContractAttribute);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000988E8 File Offset: 0x00096AE8
		internal static Type GetContractTypeAndAttribute(Type interfaceType, out ServiceContractAttribute contractAttribute)
		{
			contractAttribute = ServiceReflector.GetSingleAttribute<ServiceContractAttribute>(interfaceType);
			if (contractAttribute != null)
			{
				return interfaceType;
			}
			List<Type> list = new List<Type>(ServiceReflector.GetInheritedContractTypes(interfaceType));
			if (list.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AttemptedToGetContractTypeForButThatTypeIs1", new object[]
				{
					interfaceType.Name
				})));
			}
			foreach (Type type in list)
			{
				bool flag = true;
				foreach (Type type2 in list)
				{
					if (!type2.IsAssignableFrom(type))
					{
						flag = false;
					}
				}
				if (flag)
				{
					contractAttribute = ServiceReflector.GetSingleAttribute<ServiceContractAttribute>(type);
					return type;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoMostDerivedContract", new object[]
			{
				interfaceType.Name
			})));
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000989FC File Offset: 0x00096BFC
		private static List<MethodInfo> GetMethodsInternal(Type interfaceType)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (MethodInfo methodInfo in interfaceType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (ServiceReflector.GetSingleAttribute<OperationContractAttribute>(methodInfo) != null)
				{
					list.Add(methodInfo);
				}
				else if (ServiceReflector.GetFirstAttribute<IOperationContractAttributeProvider>(methodInfo) != null)
				{
					list.Add(methodInfo);
				}
			}
			return list;
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x00098A4C File Offset: 0x00096C4C
		internal static void ValidateParameterMetadata(MethodInfo methodInfo)
		{
			ParameterInfo[] parameters = methodInfo.GetParameters();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (!parameterInfo.ParameterType.IsByRef)
				{
					if (parameterInfo.IsOut)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBadByValueParameterMetadata", new object[]
						{
							methodInfo.Name,
							methodInfo.DeclaringType.Name
						})));
					}
				}
				else if (parameterInfo.IsIn && !parameterInfo.IsOut)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBadByReferenceParameterMetadata", new object[]
					{
						methodInfo.Name,
						methodInfo.DeclaringType.Name
					})));
				}
			}
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x00098B0D File Offset: 0x00096D0D
		internal static bool FlowsIn(ParameterInfo paramInfo)
		{
			return !paramInfo.IsOut || paramInfo.IsIn;
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x00098B1F File Offset: 0x00096D1F
		internal static bool FlowsOut(ParameterInfo paramInfo)
		{
			return paramInfo.ParameterType.IsByRef;
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x00098B2C File Offset: 0x00096D2C
		internal static ParameterInfo[] GetInputParameters(MethodInfo method, bool asyncPattern)
		{
			int num = 0;
			ParameterInfo[] parameters = method.GetParameters();
			int num2 = parameters.Length;
			if (asyncPattern)
			{
				num2 -= 2;
			}
			for (int i = 0; i < num2; i++)
			{
				if (ServiceReflector.FlowsIn(parameters[i]))
				{
					num++;
				}
			}
			ParameterInfo[] array = new ParameterInfo[num];
			int num3 = 0;
			for (int j = 0; j < num2; j++)
			{
				ParameterInfo parameterInfo = parameters[j];
				if (ServiceReflector.FlowsIn(parameterInfo))
				{
					array[num3++] = parameterInfo;
				}
			}
			return array;
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x00098BA4 File Offset: 0x00096DA4
		internal static ParameterInfo[] GetOutputParameters(MethodInfo method, bool asyncPattern)
		{
			int num = 0;
			ParameterInfo[] parameters = method.GetParameters();
			int num2 = parameters.Length;
			if (asyncPattern)
			{
				num2--;
			}
			for (int i = 0; i < num2; i++)
			{
				if (ServiceReflector.FlowsOut(parameters[i]))
				{
					num++;
				}
			}
			ParameterInfo[] array = new ParameterInfo[num];
			int num3 = 0;
			for (int j = 0; j < num2; j++)
			{
				ParameterInfo parameterInfo = parameters[j];
				if (ServiceReflector.FlowsOut(parameterInfo))
				{
					array[num3++] = parameterInfo;
				}
			}
			return array;
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x00098C1C File Offset: 0x00096E1C
		internal static bool HasOutputParameters(MethodInfo method, bool asyncPattern)
		{
			ParameterInfo[] parameters = method.GetParameters();
			int num = parameters.Length;
			if (asyncPattern)
			{
				num--;
			}
			for (int i = 0; i < num; i++)
			{
				if (ServiceReflector.FlowsOut(parameters[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x00098C54 File Offset: 0x00096E54
		private static MethodInfo GetEndMethodInternal(MethodInfo beginMethod)
		{
			string logicalName = ServiceReflector.GetLogicalName(beginMethod);
			string text = "End" + logicalName;
			MemberInfo[] member = beginMethod.DeclaringType.GetMember(text, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (member.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoEndMethodFoundForAsyncBeginMethod3", new object[]
				{
					beginMethod.Name,
					beginMethod.DeclaringType.FullName,
					text
				})));
			}
			if (member.Length > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MoreThanOneEndMethodFoundForAsyncBeginMethod3", new object[]
				{
					beginMethod.Name,
					beginMethod.DeclaringType.FullName,
					text
				})));
			}
			return (MethodInfo)member[0];
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x00098D0C File Offset: 0x00096F0C
		internal static MethodInfo GetEndMethod(MethodInfo beginMethod)
		{
			MethodInfo endMethodInternal = ServiceReflector.GetEndMethodInternal(beginMethod);
			if (!ServiceReflector.HasEndMethodShape(endMethodInternal))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidAsyncEndMethodSignatureForMethod2", new object[]
				{
					endMethodInternal.Name,
					endMethodInternal.DeclaringType.FullName
				})));
			}
			return endMethodInternal;
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x00098D60 File Offset: 0x00096F60
		internal static XmlName GetOperationName(MethodInfo method)
		{
			OperationContractAttribute operationContractAttribute = ServiceReflector.GetOperationContractAttribute(method);
			return NamingHelper.GetOperationName(ServiceReflector.GetLogicalName(method), operationContractAttribute.Name);
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x00098D88 File Offset: 0x00096F88
		internal static bool HasBeginMethodShape(MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			return method.Name.StartsWith("Begin", StringComparison.Ordinal) && parameters.Length >= 2 && !(parameters[parameters.Length - 2].ParameterType != ServiceReflector.asyncCallbackType) && !(parameters[parameters.Length - 1].ParameterType != ServiceReflector.objectType) && !(method.ReturnType != ServiceReflector.asyncResultType);
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x00098DFC File Offset: 0x00096FFC
		internal static bool IsBegin(OperationContractAttribute opSettings, MethodInfo method)
		{
			if (!opSettings.AsyncPattern)
			{
				return false;
			}
			if (!ServiceReflector.HasBeginMethodShape(method))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidAsyncBeginMethodSignatureForMethod2", new object[]
				{
					method.Name,
					method.DeclaringType.FullName
				})));
			}
			return true;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x00098E53 File Offset: 0x00097053
		internal static bool IsTask(MethodInfo method)
		{
			return method.ReturnType == ServiceReflector.taskType || (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == ServiceReflector.taskTResultType);
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x00098E90 File Offset: 0x00097090
		internal static bool IsTask(MethodInfo method, out Type taskTResult)
		{
			taskTResult = null;
			Type returnType = method.ReturnType;
			if (returnType == ServiceReflector.taskType)
			{
				taskTResult = ServiceReflector.VoidType;
				return true;
			}
			if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == ServiceReflector.taskTResultType)
			{
				taskTResult = returnType.GetGenericArguments()[0];
				return true;
			}
			return false;
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x00098EE4 File Offset: 0x000970E4
		internal static bool HasEndMethodShape(MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			return method.Name.StartsWith("End", StringComparison.Ordinal) && parameters.Length >= 1 && !(parameters[parameters.Length - 1].ParameterType != ServiceReflector.asyncResultType);
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x00098F2C File Offset: 0x0009712C
		internal static OperationContractAttribute GetOperationContractAttribute(MethodInfo method)
		{
			OperationContractAttribute singleAttribute = ServiceReflector.GetSingleAttribute<OperationContractAttribute>(method);
			if (singleAttribute != null)
			{
				return singleAttribute;
			}
			IOperationContractAttributeProvider firstAttribute = ServiceReflector.GetFirstAttribute<IOperationContractAttributeProvider>(method);
			if (firstAttribute != null)
			{
				return firstAttribute.GetOperationContractAttribute();
			}
			return null;
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x00098F58 File Offset: 0x00097158
		internal static bool IsBegin(MethodInfo method)
		{
			OperationContractAttribute operationContractAttribute = ServiceReflector.GetOperationContractAttribute(method);
			return operationContractAttribute != null && ServiceReflector.IsBegin(operationContractAttribute, method);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x00098F78 File Offset: 0x00097178
		internal static string GetLogicalName(MethodInfo method)
		{
			bool flag = ServiceReflector.IsBegin(method);
			bool isTask = !flag && ServiceReflector.IsTask(method);
			return ServiceReflector.GetLogicalName(method, flag, isTask);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x00098FA4 File Offset: 0x000971A4
		internal static string GetLogicalName(MethodInfo method, bool isAsync, bool isTask)
		{
			if (isAsync)
			{
				return method.Name.Substring("Begin".Length);
			}
			if (isTask && method.Name.EndsWith("Async", StringComparison.Ordinal))
			{
				return method.Name.Substring(0, method.Name.Length - "Async".Length);
			}
			return method.Name;
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x0009900C File Offset: 0x0009720C
		internal static bool HasNoDisposableParameters(MethodInfo methodInfo)
		{
			foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
			{
				if (ServiceReflector.IsParameterDisposable(parameterInfo.ParameterType))
				{
					return false;
				}
			}
			return methodInfo.ReturnParameter == null || !ServiceReflector.IsParameterDisposable(methodInfo.ReturnParameter.ParameterType);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x0009905E File Offset: 0x0009725E
		internal static bool IsParameterDisposable(Type type)
		{
			return !type.IsSealed || typeof(IDisposable).IsAssignableFrom(type);
		}

		// Token: 0x0400224C RID: 8780
		internal const BindingFlags ServiceModelBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x0400224D RID: 8781
		internal const string BeginMethodNamePrefix = "Begin";

		// Token: 0x0400224E RID: 8782
		internal const string EndMethodNamePrefix = "End";

		// Token: 0x0400224F RID: 8783
		internal static readonly Type VoidType = typeof(void);

		// Token: 0x04002250 RID: 8784
		internal const string AsyncMethodNameSuffix = "Async";

		// Token: 0x04002251 RID: 8785
		internal static readonly Type taskType = typeof(Task);

		// Token: 0x04002252 RID: 8786
		internal static readonly Type taskTResultType = typeof(Task<>);

		// Token: 0x04002253 RID: 8787
		internal static readonly Type CancellationTokenType = typeof(CancellationToken);

		// Token: 0x04002254 RID: 8788
		internal static readonly Type IProgressType = typeof(IProgress<>);

		// Token: 0x04002255 RID: 8789
		private static readonly Type asyncCallbackType = typeof(AsyncCallback);

		// Token: 0x04002256 RID: 8790
		private static readonly Type asyncResultType = typeof(IAsyncResult);

		// Token: 0x04002257 RID: 8791
		private static readonly Type objectType = typeof(object);

		// Token: 0x04002258 RID: 8792
		private static readonly Type OperationContractAttributeType = typeof(OperationContractAttribute);
	}
}
