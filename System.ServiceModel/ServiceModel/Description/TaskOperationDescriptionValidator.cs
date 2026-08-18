using System;
using System.Reflection;

namespace System.ServiceModel.Description
{
	// Token: 0x0200042E RID: 1070
	internal static class TaskOperationDescriptionValidator
	{
		// Token: 0x060029B4 RID: 10676 RVA: 0x000A0EE4 File Offset: 0x0009F0E4
		internal static void Validate(OperationDescription operationDescription, bool isForService)
		{
			MethodInfo taskMethod = operationDescription.TaskMethod;
			if (taskMethod != null)
			{
				if (isForService)
				{
					TaskOperationDescriptionValidator.EnsureNoSyncMethod(operationDescription);
					TaskOperationDescriptionValidator.EnsureNoBeginEndMethod(operationDescription);
				}
				else
				{
					TaskOperationDescriptionValidator.EnsureNoOutputParameters(taskMethod);
				}
				TaskOperationDescriptionValidator.EnsureParametersAreSupported(taskMethod);
			}
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000A0F20 File Offset: 0x0009F120
		private static void EnsureNoSyncMethod(OperationDescription operation)
		{
			if (operation.SyncMethod != null)
			{
				string name = operation.TaskMethod.Name;
				string name2 = operation.SyncMethod.Name;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotHaveTwoOperationsWithTheSameName3", new object[]
				{
					name,
					name2,
					operation.DeclaringContract.ContractType
				})));
			}
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000A0F8C File Offset: 0x0009F18C
		private static void EnsureNoBeginEndMethod(OperationDescription operation)
		{
			if (operation.BeginMethod != null)
			{
				string name = operation.TaskMethod.Name;
				string name2 = operation.BeginMethod.Name;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotHaveTwoOperationsWithTheSameName3", new object[]
				{
					name,
					name2,
					operation.DeclaringContract.ContractType
				})));
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000A0FF8 File Offset: 0x0009F1F8
		private static void EnsureParametersAreSupported(MethodInfo method)
		{
			foreach (ParameterInfo parameterInfo in method.GetParameters())
			{
				Type parameterType = parameterInfo.ParameterType;
				if (parameterType == ServiceReflector.CancellationTokenType || (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == ServiceReflector.IProgressType))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("TaskMethodParameterNotSupported", new object[]
					{
						parameterType
					})));
				}
			}
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000A1070 File Offset: 0x0009F270
		private static void EnsureNoOutputParameters(MethodInfo method)
		{
			if (ServiceReflector.HasOutputParameters(method, false))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TaskMethodMustNotHaveOutParameter")));
			}
		}
	}
}
