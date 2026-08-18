using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using ClockWorkLogger;

namespace TechnoPro.Common.WCF
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Class)]
	public class ErrorHandlerBehaviorAttribute : Attribute, IServiceBehavior, IErrorHandler
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002A58 File Offset: 0x00000C58
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002A60 File Offset: 0x00000C60
		protected Type ServiceType { get; set; }

		// Token: 0x06000032 RID: 50 RVA: 0x00002A69 File Offset: 0x00000C69
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A6C File Offset: 0x00000C6C
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			this.ServiceType = serviceDescription.ServiceType;
			foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = (ChannelDispatcher)channelDispatcherBase;
				channelDispatcher.ErrorHandlers.Add(this);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public bool HandleError(Exception error)
		{
			bool isErrorEnabled = CWLogger.Logger.IsErrorEnabled;
			if (isErrorEnabled)
			{
				CWLogger.Logger.ErrorException(error.Message, error);
			}
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B0C File Offset: 0x00000D0C
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			bool isErrorEnabled = CWLogger.Logger.IsErrorEnabled;
			if (isErrorEnabled)
			{
				CWLogger.Logger.Error("ErrorHandlerBehavior: Error type={0}", error.GetType().ToString());
				CWLogger.Logger.Error("ErrorHandlerBehavior: Error message={0}", error.Message);
				CWLogger.Logger.Error("ErrorHandlerBehavior: {0}", error.ToString());
			}
			bool flag = !(error is FaultException);
			if (flag)
			{
				bool isErrorEnabled2 = CWLogger.Logger.IsErrorEnabled;
				if (isErrorEnabled2)
				{
					CWLogger.Logger.Error("ErrorHandlerBehavior: Not a FaultException");
				}
				object obj;
				bool flag2 = !this.IsExceptionDefinedInContract(error, out obj);
				FaultException ex;
				if (flag2)
				{
					bool isErrorEnabled3 = CWLogger.Logger.IsErrorEnabled;
					if (isErrorEnabled3)
					{
						CWLogger.Logger.Error("ErrorHandlerBehavior: Exception '{0}' not define in contract", error.GetType().ToString());
					}
					ex = new FaultException<UnexpectedFault>(new UnexpectedFault(error), error.Message);
				}
				else
				{
					ex = (FaultException)Activator.CreateInstance(typeof(FaultException<>).MakeGenericType(new Type[]
					{
						obj.GetType()
					}), new object[]
					{
						obj,
						error.Message
					});
					bool isErrorEnabled4 = CWLogger.Logger.IsErrorEnabled;
					if (isErrorEnabled4)
					{
						CWLogger.Logger.Error("ErrorHandlerBehavior: Fault exception {0} define in contract", obj.GetType());
					}
				}
				MessageFault fault2 = ex.CreateMessageFault();
				fault = Message.CreateMessage(version, fault2, ex.Action);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A69 File Offset: 0x00000C69
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C78 File Offset: 0x00000E78
		private string GetMethodName(Exception ex)
		{
			string result;
			try
			{
				int num = ex.StackTrace.IndexOf(this.ServiceType.Name);
				int num2 = num + this.ServiceType.Name.Length + 1;
				string text = ex.StackTrace.Substring(num2, ex.StackTrace.IndexOf("(", num) - num2);
				bool isErrorEnabled = CWLogger.Logger.IsErrorEnabled;
				if (isErrorEnabled)
				{
					CWLogger.Logger.Error("ErrorHandlerBehavior: Method Name = {0}", text);
				}
				result = text;
			}
			catch (Exception ex2)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D18 File Offset: 0x00000F18
		private bool IsBasedOnException(Type type, Type exceptionType)
		{
			Type baseType = type.BaseType;
			bool flag = baseType == null || !baseType.IsGenericType || !baseType.GetGenericTypeDefinition().Equals(typeof(ExceptionFault<>));
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Type type2 = baseType.GetGenericArguments()[0];
				result = type2.Equals(exceptionType);
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D78 File Offset: 0x00000F78
		private bool IsExceptionDefinedInContract(Exception ex, out object contractFault)
		{
			contractFault = ex;
			string methodName = this.GetMethodName(ex);
			bool flag = string.IsNullOrEmpty(methodName);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Type[] interfaces = this.ServiceType.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					MethodInfo method = interfaces[i].GetMethod(methodName);
					bool flag2 = method != null;
					if (flag2)
					{
						Type type = ex.GetType();
						object[] customAttributes = method.GetCustomAttributes(typeof(FaultContractAttribute), true);
						foreach (FaultContractAttribute faultContractAttribute in customAttributes)
						{
							bool flag3 = faultContractAttribute.DetailType.Equals(type);
							if (flag3)
							{
								return true;
							}
							bool flag4 = this.IsBasedOnException(faultContractAttribute.DetailType, type);
							if (flag4)
							{
								contractFault = faultContractAttribute.DetailType.GetConstructor(Type.EmptyTypes).Invoke(null);
								faultContractAttribute.DetailType.InvokeMember("ConvertFrom", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, contractFault, new object[]
								{
									ex
								});
								return true;
							}
						}
						return false;
					}
				}
				result = false;
			}
			return result;
		}
	}
}
