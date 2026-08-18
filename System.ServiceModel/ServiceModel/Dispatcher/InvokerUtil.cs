using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Description;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000577 RID: 1399
	internal sealed class InvokerUtil
	{
		// Token: 0x06003642 RID: 13890 RVA: 0x000D1AC5 File Offset: 0x000CFCC5
		[SecuritySafeCritical]
		public InvokerUtil()
		{
			this.helper = new InvokerUtil.CriticalHelper();
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000D1AD8 File Offset: 0x000CFCD8
		[SecuritySafeCritical]
		internal CreateInstanceDelegate GenerateCreateInstanceDelegate(Type type, ConstructorInfo constructor)
		{
			return this.helper.GenerateCreateInstanceDelegate(type, constructor);
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000D1AE7 File Offset: 0x000CFCE7
		[SecuritySafeCritical]
		internal InvokeDelegate GenerateInvokeDelegate(MethodInfo method, out int inputParameterCount, out int outputParameterCount)
		{
			return this.helper.GenerateInvokeDelegate(method, out inputParameterCount, out outputParameterCount);
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000D1AF7 File Offset: 0x000CFCF7
		[SecuritySafeCritical]
		internal InvokeBeginDelegate GenerateInvokeBeginDelegate(MethodInfo method, out int inputParameterCount)
		{
			return this.helper.GenerateInvokeBeginDelegate(method, out inputParameterCount);
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000D1B06 File Offset: 0x000CFD06
		[SecuritySafeCritical]
		internal InvokeEndDelegate GenerateInvokeEndDelegate(MethodInfo method, out int outputParameterCount)
		{
			return this.helper.GenerateInvokeEndDelegate(method, out outputParameterCount);
		}

		// Token: 0x040028AC RID: 10412
		[SecurityCritical]
		private InvokerUtil.CriticalHelper helper;

		// Token: 0x02000C8C RID: 3212
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class CriticalHelper
		{
			// Token: 0x060078A8 RID: 30888 RVA: 0x001C26E0 File Offset: 0x001C08E0
			internal CreateInstanceDelegate GenerateCreateInstanceDelegate(Type type, ConstructorInfo constructor)
			{
				bool flag = !InvokerUtil.CriticalHelper.IsTypeVisible(type) || InvokerUtil.CriticalHelper.ConstructorRequiresMemberAccess(constructor);
				this.ilg = new CodeGenerator();
				try
				{
					this.ilg.BeginMethod("Create" + type.FullName, typeof(CreateInstanceDelegate), flag);
				}
				catch (SecurityException ex)
				{
					if (flag && ex.PermissionType.Equals(typeof(ReflectionPermission)))
					{
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Warning);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityException(SR.GetString("PartialTrustServiceCtorNotVisible", new object[]
						{
							type.FullName
						})));
					}
					throw;
				}
				if (type.IsValueType)
				{
					LocalBuilder localBuilder = this.ilg.DeclareLocal(type, type.Name + "Instance");
					this.ilg.LoadZeroValueIntoLocal(type, localBuilder);
					this.ilg.Load(localBuilder);
				}
				else
				{
					this.ilg.New(constructor);
				}
				this.ilg.ConvertValue(type, this.ilg.CurrentMethod.ReturnType);
				return (CreateInstanceDelegate)this.ilg.EndMethod();
			}

			// Token: 0x060078A9 RID: 30889 RVA: 0x001C2808 File Offset: 0x001C0A08
			internal InvokeDelegate GenerateInvokeDelegate(MethodInfo method, out int inputParameterCount, out int outputParameterCount)
			{
				bool flag = InvokerUtil.CriticalHelper.MethodRequiresMemberAccess(method);
				this.ilg = new CodeGenerator();
				try
				{
					this.ilg.BeginMethod("SyncInvoke" + method.Name, typeof(InvokeDelegate), flag);
				}
				catch (SecurityException ex)
				{
					if (flag && ex.PermissionType.Equals(typeof(ReflectionPermission)))
					{
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Warning);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityException(SR.GetString("PartialTrustServiceMethodNotVisible", new object[]
						{
							method.DeclaringType.FullName,
							method.Name
						})));
					}
					throw;
				}
				ArgBuilder arg = this.ilg.GetArg(0);
				ArgBuilder arg2 = this.ilg.GetArg(1);
				ArgBuilder arg3 = this.ilg.GetArg(2);
				ParameterInfo[] parameters = method.GetParameters();
				LocalBuilder localBuilder = this.ilg.DeclareLocal(this.ilg.CurrentMethod.ReturnType, "returnParam");
				LocalBuilder[] parameterLocals = new LocalBuilder[parameters.Length];
				this.DeclareParameterLocals(parameters, parameterLocals);
				this.LoadInputParametersIntoLocals(parameters, parameterLocals, arg2, out inputParameterCount);
				this.LoadTarget(arg, method.ReflectedType);
				this.LoadParameters(parameters, parameterLocals);
				this.InvokeMethod(method, localBuilder);
				this.LoadOutputParametersIntoArray(parameters, parameterLocals, arg3, out outputParameterCount);
				this.ilg.Load(localBuilder);
				return (InvokeDelegate)this.ilg.EndMethod();
			}

			// Token: 0x060078AA RID: 30890 RVA: 0x001C2978 File Offset: 0x001C0B78
			internal InvokeBeginDelegate GenerateInvokeBeginDelegate(MethodInfo method, out int inputParameterCount)
			{
				bool flag = InvokerUtil.CriticalHelper.MethodRequiresMemberAccess(method);
				this.ilg = new CodeGenerator();
				try
				{
					this.ilg.BeginMethod("AsyncInvokeBegin" + method.Name, typeof(InvokeBeginDelegate), flag);
				}
				catch (SecurityException ex)
				{
					if (flag && ex.PermissionType.Equals(typeof(ReflectionPermission)))
					{
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Warning);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityException(SR.GetString("PartialTrustServiceMethodNotVisible", new object[]
						{
							method.DeclaringType.FullName,
							method.Name
						})));
					}
					throw;
				}
				ArgBuilder arg = this.ilg.GetArg(0);
				ArgBuilder arg2 = this.ilg.GetArg(1);
				ArgBuilder arg3 = this.ilg.GetArg(2);
				ArgBuilder arg4 = this.ilg.GetArg(3);
				ParameterInfo[] parameters = method.GetParameters();
				LocalBuilder localBuilder = this.ilg.DeclareLocal(this.ilg.CurrentMethod.ReturnType, "returnParam");
				LocalBuilder[] parameterLocals = new LocalBuilder[parameters.Length - 2];
				this.DeclareParameterLocals(parameters, parameterLocals);
				this.LoadInputParametersIntoLocals(parameters, parameterLocals, arg2, out inputParameterCount);
				this.LoadTarget(arg, method.ReflectedType);
				this.LoadParameters(parameters, parameterLocals);
				this.ilg.Load(arg3);
				this.ilg.Load(arg4);
				this.InvokeMethod(method, localBuilder);
				this.ilg.Load(localBuilder);
				return (InvokeBeginDelegate)this.ilg.EndMethod();
			}

			// Token: 0x060078AB RID: 30891 RVA: 0x001C2B08 File Offset: 0x001C0D08
			internal InvokeEndDelegate GenerateInvokeEndDelegate(MethodInfo method, out int outputParameterCount)
			{
				bool flag = InvokerUtil.CriticalHelper.MethodRequiresMemberAccess(method);
				this.ilg = new CodeGenerator();
				try
				{
					this.ilg.BeginMethod("AsyncInvokeEnd" + method.Name, typeof(InvokeEndDelegate), flag);
				}
				catch (SecurityException ex)
				{
					if (flag && ex.PermissionType.Equals(typeof(ReflectionPermission)))
					{
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Warning);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityException(SR.GetString("PartialTrustServiceMethodNotVisible", new object[]
						{
							method.DeclaringType.FullName,
							method.Name
						})));
					}
					throw;
				}
				ArgBuilder arg = this.ilg.GetArg(0);
				ArgBuilder arg2 = this.ilg.GetArg(1);
				ArgBuilder arg3 = this.ilg.GetArg(2);
				ParameterInfo[] parameters = method.GetParameters();
				LocalBuilder localBuilder = this.ilg.DeclareLocal(this.ilg.CurrentMethod.ReturnType, "returnParam");
				LocalBuilder[] parameterLocals = new LocalBuilder[parameters.Length - 1];
				this.DeclareParameterLocals(parameters, parameterLocals);
				this.LoadZeroValueInputParametersIntoLocals(parameters, parameterLocals);
				this.LoadTarget(arg, method.ReflectedType);
				this.LoadParameters(parameters, parameterLocals);
				this.ilg.Load(arg3);
				this.InvokeMethod(method, localBuilder);
				this.LoadOutputParametersIntoArray(parameters, parameterLocals, arg2, out outputParameterCount);
				this.ilg.Load(localBuilder);
				return (InvokeEndDelegate)this.ilg.EndMethod();
			}

			// Token: 0x060078AC RID: 30892 RVA: 0x001C2C84 File Offset: 0x001C0E84
			private void DeclareParameterLocals(ParameterInfo[] parameters, LocalBuilder[] parameterLocals)
			{
				for (int i = 0; i < parameterLocals.Length; i++)
				{
					parameterLocals[i] = this.ilg.DeclareLocal(TypeLoader.GetParameterType(parameters[i]), "param" + i.ToString(CultureInfo.InvariantCulture));
				}
			}

			// Token: 0x060078AD RID: 30893 RVA: 0x001C2CCC File Offset: 0x001C0ECC
			private void LoadInputParametersIntoLocals(ParameterInfo[] parameters, LocalBuilder[] parameterLocals, ArgBuilder inputParametersArg, out int inputParameterCount)
			{
				inputParameterCount = 0;
				for (int i = 0; i < parameterLocals.Length; i++)
				{
					if (ServiceReflector.FlowsIn(parameters[i]))
					{
						Type localType = parameterLocals[i].LocalType;
						this.ilg.LoadArrayElement(inputParametersArg, inputParameterCount);
						if (!localType.IsValueType)
						{
							this.ilg.ConvertValue(InvokerUtil.CriticalHelper.TypeOfObject, localType);
							this.ilg.Store(parameterLocals[i]);
						}
						else
						{
							this.ilg.Dup();
							this.ilg.If();
							this.ilg.ConvertValue(InvokerUtil.CriticalHelper.TypeOfObject, localType);
							this.ilg.Store(parameterLocals[i]);
							this.ilg.Else();
							this.ilg.Pop();
							this.ilg.LoadZeroValueIntoLocal(localType, parameterLocals[i]);
							this.ilg.EndIf();
						}
						inputParameterCount++;
					}
				}
			}

			// Token: 0x060078AE RID: 30894 RVA: 0x001C2DB4 File Offset: 0x001C0FB4
			private void LoadZeroValueInputParametersIntoLocals(ParameterInfo[] parameters, LocalBuilder[] parameterLocals)
			{
				for (int i = 0; i < parameterLocals.Length; i++)
				{
					if (ServiceReflector.FlowsIn(parameters[i]))
					{
						this.ilg.LoadZeroValueIntoLocal(parameterLocals[i].LocalType, parameterLocals[i]);
					}
				}
			}

			// Token: 0x060078AF RID: 30895 RVA: 0x001C2DF0 File Offset: 0x001C0FF0
			private void LoadTarget(ArgBuilder targetArg, Type targetType)
			{
				this.ilg.Load(targetArg);
				this.ilg.ConvertValue(targetArg.ArgType, targetType);
				if (targetType.IsValueType)
				{
					LocalBuilder localBuilder = this.ilg.DeclareLocal(targetType, "target");
					this.ilg.Store(localBuilder);
					this.ilg.LoadAddress(localBuilder);
				}
			}

			// Token: 0x060078B0 RID: 30896 RVA: 0x001C2E50 File Offset: 0x001C1050
			private void LoadParameters(ParameterInfo[] parameters, LocalBuilder[] parameterLocals)
			{
				for (int i = 0; i < parameterLocals.Length; i++)
				{
					if (parameters[i].ParameterType.IsByRef)
					{
						this.ilg.Ldloca(parameterLocals[i]);
					}
					else
					{
						this.ilg.Ldloc(parameterLocals[i]);
					}
				}
			}

			// Token: 0x060078B1 RID: 30897 RVA: 0x001C2E98 File Offset: 0x001C1098
			private void InvokeMethod(MethodInfo method, LocalBuilder returnLocal)
			{
				this.ilg.Call(method);
				if (method.ReturnType == typeof(void))
				{
					this.ilg.Load(null);
				}
				else
				{
					this.ilg.ConvertValue(method.ReturnType, this.ilg.CurrentMethod.ReturnType);
				}
				this.ilg.Store(returnLocal);
			}

			// Token: 0x060078B2 RID: 30898 RVA: 0x001C2F04 File Offset: 0x001C1104
			private void LoadOutputParametersIntoArray(ParameterInfo[] parameters, LocalBuilder[] parameterLocals, ArgBuilder outputParametersArg, out int outputParameterCount)
			{
				outputParameterCount = 0;
				for (int i = 0; i < parameterLocals.Length; i++)
				{
					if (ServiceReflector.FlowsOut(parameters[i]))
					{
						this.ilg.Load(outputParametersArg);
						this.ilg.Load(outputParameterCount);
						this.ilg.Load(parameterLocals[i]);
						this.ilg.ConvertValue(parameterLocals[i].LocalType, InvokerUtil.CriticalHelper.TypeOfObject);
						this.ilg.Stelem(InvokerUtil.CriticalHelper.TypeOfObject);
						outputParameterCount++;
					}
				}
			}

			// Token: 0x060078B3 RID: 30899 RVA: 0x001C2F8C File Offset: 0x001C118C
			private static bool IsTypeVisible(Type t)
			{
				if (t.Module == typeof(InvokerUtil).Module)
				{
					return true;
				}
				if (!t.IsVisible)
				{
					return false;
				}
				foreach (Type type in t.GetGenericArguments())
				{
					if (!type.IsGenericParameter && !InvokerUtil.CriticalHelper.IsTypeVisible(type))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060078B4 RID: 30900 RVA: 0x001C2FED File Offset: 0x001C11ED
			private static bool ConstructorRequiresMemberAccess(ConstructorInfo ctor)
			{
				return ctor != null && (!ctor.IsPublic || !InvokerUtil.CriticalHelper.IsTypeVisible(ctor.DeclaringType)) && ctor.Module != typeof(InvokerUtil).Module;
			}

			// Token: 0x060078B5 RID: 30901 RVA: 0x001C3029 File Offset: 0x001C1229
			private static bool MethodRequiresMemberAccess(MethodInfo method)
			{
				return method != null && (!method.IsPublic || !InvokerUtil.CriticalHelper.IsTypeVisible(method.DeclaringType)) && method.Module != typeof(InvokerUtil).Module;
			}

			// Token: 0x040044C4 RID: 17604
			private static Type TypeOfObject = typeof(object);

			// Token: 0x040044C5 RID: 17605
			private CodeGenerator ilg;
		}
	}
}
