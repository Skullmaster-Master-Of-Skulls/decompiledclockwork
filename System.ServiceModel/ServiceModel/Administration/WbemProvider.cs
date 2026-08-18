using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceModel.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000457 RID: 1111
	internal class WbemProvider : WbemNative.IWbemProviderInit, WbemNative.IWbemServices
	{
		// Token: 0x06002AFE RID: 11006 RVA: 0x000A88E3 File Offset: 0x000A6AE3
		internal WbemProvider(string nameSpace, string appName)
		{
			this.nameSpace = nameSpace;
			this.appName = appName;
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000A8914 File Offset: 0x000A6B14
		internal void Initialize()
		{
			try
			{
				AppDomain.CurrentDomain.DomainUnload += this.ExitOrUnloadEventHandler;
				AppDomain.CurrentDomain.ProcessExit += this.ExitOrUnloadEventHandler;
				AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.ExitOrUnloadEventHandler);
				WbemProvider.MTAExecute(new WaitCallback(this.RegisterWbemProvider), null);
				this.initialized = true;
			}
			catch (SecurityException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PartialTrustWMINotEnabled")));
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000A89AC File Offset: 0x000A6BAC
		private void RegisterWbemProvider(object state)
		{
			this.wbemRegistrar = (WbemNative.IWbemDecoupledRegistrar)new WbemNative.WbemDecoupledRegistrar();
			int num = this.wbemRegistrar.Register(0, null, null, null, this.nameSpace, this.appName, this);
			if (num != 0)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356562U, new string[]
				{
					TraceUtility.CreateSourceString(this),
					num.ToString("x", CultureInfo.InvariantCulture)
				});
				this.wbemRegistrar = null;
			}
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000A8A28 File Offset: 0x000A6C28
		private void UnRegisterWbemProvider(object state)
		{
			if (this.wbemRegistrar != null)
			{
				int num = this.wbemRegistrar.UnRegister();
				if (num != 0)
				{
					DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356563U, new string[]
					{
						TraceUtility.CreateSourceString(this),
						num.ToString("x", CultureInfo.InvariantCulture)
					});
				}
				this.wbemRegistrar = null;
			}
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000A8A88 File Offset: 0x000A6C88
		private void ExitOrUnloadEventHandler(object sender, EventArgs e)
		{
			if (this.wbemRegistrar != null)
			{
				WbemProvider.MTAExecute(new WaitCallback(this.UnRegisterWbemProvider), null);
			}
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000A8AA4 File Offset: 0x000A6CA4
		public void Register(string className, IWmiProvider wmiProvider)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (!this.initialized)
				{
					this.Initialize();
				}
				this.wmiProviders.Add(className, wmiProvider);
			}
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000A8AFC File Offset: 0x000A6CFC
		private IWmiProvider GetProvider(string className)
		{
			Dictionary<string, IWmiProvider> obj = this.wmiProviders;
			IWmiProvider @default;
			lock (obj)
			{
				if (!this.wmiProviders.TryGetValue(className, out @default))
				{
					@default = WbemProvider.NoInstanceWMIProvider.Default;
				}
			}
			return @default;
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000A8B50 File Offset: 0x000A6D50
		int WbemNative.IWbemProviderInit.Initialize(string wszUser, int lFlags, string wszNamespace, string wszLocale, WbemNative.IWbemServices wbemServices, WbemNative.IWbemContext wbemContext, WbemNative.IWbemProviderInitSink wbemSink)
		{
			if (wbemServices == null || wbemContext == null || wbemSink == null)
			{
				return -2147217400;
			}
			try
			{
				WbemProvider.MTAExecute(new WaitCallback(this.RelocateWbemServicesRCWToMTA), wbemServices);
				wbemSink.SetStatus(0, 0);
			}
			catch (WbemException ex)
			{
				wbemSink.SetStatus(ex.ErrorCode, 0);
				return ex.ErrorCode;
			}
			catch (Exception)
			{
				wbemSink.SetStatus(-2147217407, 0);
				return -2147217407;
			}
			finally
			{
				Marshal.ReleaseComObject(wbemSink);
			}
			return 0;
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000A8BF4 File Offset: 0x000A6DF4
		private void RelocateWbemServicesRCWToMTA(object comObject)
		{
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(comObject);
			Marshal.ReleaseComObject(comObject);
			this.wbemServices = (WbemNative.IWbemServices)Marshal.GetObjectForIUnknown(iunknownForObject);
			Marshal.Release(iunknownForObject);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000A8C27 File Offset: 0x000A6E27
		int WbemNative.IWbemServices.OpenNamespace(string nameSpace, int flags, WbemNative.IWbemContext wbemContext, ref WbemNative.IWbemServices wbemServices, IntPtr wbemCallResult)
		{
			return -2147217396;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000A8C2E File Offset: 0x000A6E2E
		int WbemNative.IWbemServices.CancelAsyncCall(WbemNative.IWbemObjectSink wbemSink)
		{
			return -2147217396;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000A8C35 File Offset: 0x000A6E35
		int WbemNative.IWbemServices.QueryObjectSink(int flags, out WbemNative.IWbemObjectSink wbemSink)
		{
			wbemSink = null;
			return -2147217396;
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000A8C3F File Offset: 0x000A6E3F
		int WbemNative.IWbemServices.GetObject(string objectPath, int flags, WbemNative.IWbemContext wbemContext, ref WbemNative.IWbemClassObject wbemObject, IntPtr wbemResult)
		{
			return -2147217396;
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000A8C48 File Offset: 0x000A6E48
		int WbemNative.IWbemServices.GetObjectAsync(string objectPath, int flags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemObjectSink wbemSink)
		{
			if (wbemContext == null || wbemSink == null || this.wbemServices == null)
			{
				return -2147217400;
			}
			using (DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateActivity(true, SR.GetString("WmiGetObject", new object[]
			{
				string.IsNullOrEmpty(objectPath) ? string.Empty : objectPath
			}), ActivityType.WmiGetObject) : null)
			{
				try
				{
					WbemProvider.ObjectPathRegex objectPathRegex = new WbemProvider.ObjectPathRegex(objectPath);
					WbemProvider.ParameterContext parameterContext = new WbemProvider.ParameterContext(objectPathRegex.ClassName, this.wbemServices, wbemContext, wbemSink);
					WbemProvider.WbemInstance wbemInstance = new WbemProvider.WbemInstance(parameterContext, objectPathRegex);
					IWmiProvider provider = this.GetProvider(parameterContext.ClassName);
					if (provider.GetInstance(new WbemProvider.InstanceContext(wbemInstance)))
					{
						wbemInstance.Indicate();
					}
					WbemException.ThrowIfFail(wbemSink.SetStatus(0, 0, null, null));
				}
				catch (WbemException ex)
				{
					DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356556U, new string[]
					{
						TraceUtility.CreateSourceString(this),
						ex.ToString()
					});
					wbemSink.SetStatus(0, ex.ErrorCode, null, null);
					return ex.ErrorCode;
				}
				catch (Exception ex2)
				{
					DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356556U, new string[]
					{
						TraceUtility.CreateSourceString(this),
						ex2.ToString()
					});
					wbemSink.SetStatus(0, -2147217407, null, null);
					return -2147217407;
				}
				finally
				{
					Marshal.ReleaseComObject(wbemSink);
				}
			}
			return 0;
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000A8DD8 File Offset: 0x000A6FD8
		int WbemNative.IWbemServices.PutClass(WbemNative.IWbemClassObject wbemObject, int flags, WbemNative.IWbemContext wbemContext, IntPtr wbemCallResult)
		{
			return -2147217396;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000A8DDF File Offset: 0x000A6FDF
		int WbemNative.IWbemServices.PutClassAsync(WbemNative.IWbemClassObject wbemObject, int flags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemObjectSink wbemSink)
		{
			return -2147217396;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000A8DE6 File Offset: 0x000A6FE6
		int WbemNative.IWbemServices.DeleteClass(string className, int flags, WbemNative.IWbemContext wbemContext, IntPtr wbemCallResult)
		{
			return -2147217396;
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000A8DED File Offset: 0x000A6FED
		int WbemNative.IWbemServices.DeleteClassAsync(string className, int lFlags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemObjectSink wbemSink)
		{
			return -2147217396;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000A8DF4 File Offset: 0x000A6FF4
		int WbemNative.IWbemServices.CreateClassEnum(string superClassName, int flags, WbemNative.IWbemContext wbemContext, out WbemNative.IEnumWbemClassObject wbemEnum)
		{
			wbemEnum = null;
			return -2147217396;
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000A8DFF File Offset: 0x000A6FFF
		int WbemNative.IWbemServices.CreateClassEnumAsync(string superClassName, int flags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemObjectSink wbemSink)
		{
			return -2147217396;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000A8E06 File Offset: 0x000A7006
		int WbemNative.IWbemServices.PutInstance(WbemNative.IWbemClassObject pInst, int lFlags, WbemNative.IWbemContext wbemContext, IntPtr wbemCallResult)
		{
			return -2147217396;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000A8E10 File Offset: 0x000A7010
		int WbemNative.IWbemServices.PutInstanceAsync(WbemNative.IWbemClassObject wbemObject, int lFlags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemObjectSink wbemSink)
		{
			if (wbemObject == null || wbemContext == null || wbemSink == null || this.wbemServices == null)
			{
				return -2147217400;
			}
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				try
				{
					object obj = null;
					int num = 0;
					int num2 = 0;
					WbemException.ThrowIfFail(wbemObject.Get("__CLASS", 0, ref obj, ref num, ref num2));
					string text = (string)obj;
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("WmiPutInstance", new object[]
					{
						string.IsNullOrEmpty(text) ? string.Empty : text
					}), ActivityType.WmiPutInstance);
					WbemProvider.ParameterContext parameterContext = new WbemProvider.ParameterContext(text, this.wbemServices, wbemContext, wbemSink);
					WbemProvider.WbemInstance wbemInstance = new WbemProvider.WbemInstance(parameterContext, wbemObject);
					IWmiProvider provider = this.GetProvider(parameterContext.ClassName);
					if (provider.PutInstance(new WbemProvider.InstanceContext(wbemInstance)))
					{
						wbemInstance.Indicate();
					}
					WbemException.ThrowIfFail(wbemSink.SetStatus(0, 0, null, null));
				}
				catch (WbemException ex)
				{
					DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356557U, new string[]
					{
						TraceUtility.CreateSourceString(this),
						ex.ToString()
					});
					wbemSink.SetStatus(0, ex.ErrorCode, null, null);
					return ex.ErrorCode;
				}
				catch (Exception ex2)
				{
					DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356557U, new string[]
					{
						TraceUtility.CreateSourceString(this),
						ex2.ToString()
					});
					wbemSink.SetStatus(0, -2147217407, null, null);
					return -2147217407;
				}
				finally
				{
					Marshal.ReleaseComObject(wbemSink);
				}
			}
			return 0;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000A8FFC File Offset: 0x000A71FC
		int WbemNative.IWbemServices.DeleteInstance(string objectPath, int flags, WbemNative.IWbemContext wbemContext, IntPtr wbemCallResult)
		{
			return -2147217396;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000A9004 File Offset: 0x000A7204
		int WbemNative.IWbemServices.DeleteInstanceAsync(string objectPath, int lFlags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemObjectSink wbemSink)
		{
			if (wbemContext == null || wbemSink == null || this.wbemServices == null)
			{
				return -2147217400;
			}
			try
			{
				WbemProvider.ObjectPathRegex objectPathRegex = new WbemProvider.ObjectPathRegex(objectPath);
				WbemProvider.ParameterContext parameterContext = new WbemProvider.ParameterContext(objectPathRegex.ClassName, this.wbemServices, wbemContext, wbemSink);
				WbemProvider.WbemInstance wbemInstance = new WbemProvider.WbemInstance(parameterContext, objectPathRegex);
				IWmiProvider provider = this.GetProvider(parameterContext.ClassName);
				if (provider.DeleteInstance(new WbemProvider.InstanceContext(wbemInstance)))
				{
					wbemInstance.Indicate();
				}
				WbemException.ThrowIfFail(wbemSink.SetStatus(0, 0, null, null));
			}
			catch (WbemException ex)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356558U, new string[]
				{
					ex.ToString()
				});
				wbemSink.SetStatus(0, ex.ErrorCode, null, null);
				return ex.ErrorCode;
			}
			catch (Exception ex2)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356558U, new string[]
				{
					ex2.ToString()
				});
				wbemSink.SetStatus(0, -2147217407, null, null);
				return -2147217407;
			}
			finally
			{
				Marshal.ReleaseComObject(wbemSink);
			}
			return 0;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000A9134 File Offset: 0x000A7334
		int WbemNative.IWbemServices.CreateInstanceEnum(string filter, int flags, WbemNative.IWbemContext wbemContext, out WbemNative.IEnumWbemClassObject wbemEnum)
		{
			wbemEnum = null;
			return -2147217396;
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000A9140 File Offset: 0x000A7340
		int WbemNative.IWbemServices.CreateInstanceEnumAsync(string className, int flags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemObjectSink wbemSink)
		{
			if (wbemContext == null || wbemSink == null || this.wbemServices == null)
			{
				return -2147217400;
			}
			try
			{
				WbemProvider.ParameterContext parameterContext = new WbemProvider.ParameterContext(className, this.wbemServices, wbemContext, wbemSink);
				IWmiProvider provider = this.GetProvider(parameterContext.ClassName);
				provider.EnumInstances(new WbemProvider.InstancesContext(parameterContext));
				WbemException.ThrowIfFail(wbemSink.SetStatus(0, 0, null, null));
			}
			catch (WbemException ex)
			{
				wbemSink.SetStatus(0, ex.ErrorCode, null, null);
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356559U, new string[]
				{
					className,
					ex.ToString()
				});
				return ex.ErrorCode;
			}
			catch (Exception ex2)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356559U, new string[]
				{
					className,
					ex2.ToString()
				});
				wbemSink.SetStatus(0, -2147217407, null, null);
				return -2147217407;
			}
			finally
			{
				Marshal.ReleaseComObject(wbemSink);
			}
			return 0;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000A9258 File Offset: 0x000A7458
		int WbemNative.IWbemServices.ExecQuery(string queryLanguage, string query, int flags, WbemNative.IWbemContext wbemContext, out WbemNative.IEnumWbemClassObject wbemEnum)
		{
			wbemEnum = null;
			return -2147217396;
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000A9264 File Offset: 0x000A7464
		int WbemNative.IWbemServices.ExecQueryAsync(string queryLanguage, string query, int flags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemObjectSink wbemSink)
		{
			if (wbemContext == null || wbemSink == null || this.wbemServices == null)
			{
				return -2147217400;
			}
			try
			{
				WbemProvider.QueryRegex queryRegex = new WbemProvider.QueryRegex(query);
				WbemProvider.ParameterContext parameterContext = new WbemProvider.ParameterContext(queryRegex.ClassName, this.wbemServices, wbemContext, wbemSink);
				IWmiProvider provider = this.GetProvider(parameterContext.ClassName);
				provider.EnumInstances(new WbemProvider.InstancesContext(parameterContext));
				WbemException.ThrowIfFail(wbemSink.SetStatus(0, 0, null, null));
			}
			catch (WbemException ex)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356560U, new string[]
				{
					ex.ToString()
				});
				wbemSink.SetStatus(0, ex.ErrorCode, null, null);
				return ex.ErrorCode;
			}
			catch (Exception ex2)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356560U, new string[]
				{
					ex2.ToString()
				});
				wbemSink.SetStatus(0, -2147217407, null, null);
				return -2147217407;
			}
			finally
			{
				Marshal.ReleaseComObject(wbemSink);
			}
			return 0;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000A9384 File Offset: 0x000A7584
		int WbemNative.IWbemServices.ExecNotificationQuery(string queryLanguage, string query, int flags, WbemNative.IWbemContext wbemContext, out WbemNative.IEnumWbemClassObject wbemEnum)
		{
			wbemEnum = null;
			return -2147217396;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000A938F File Offset: 0x000A758F
		int WbemNative.IWbemServices.ExecNotificationQueryAsync(string queryLanguage, string query, int flags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemObjectSink wbemSink)
		{
			return -2147217396;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000A9396 File Offset: 0x000A7596
		int WbemNative.IWbemServices.ExecMethod(string objectPath, string methodName, int flags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemClassObject wbemInParams, ref WbemNative.IWbemClassObject wbemOutParams, IntPtr wbemCallResult)
		{
			return -2147217396;
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000A93A0 File Offset: 0x000A75A0
		int WbemNative.IWbemServices.ExecMethodAsync(string objectPath, string methodName, int flags, WbemNative.IWbemContext wbemContext, WbemNative.IWbemClassObject wbemInParams, WbemNative.IWbemObjectSink wbemSink)
		{
			if (wbemContext == null || wbemInParams == null || wbemSink == null || this.wbemServices == null)
			{
				return -2147217400;
			}
			int num = 0;
			try
			{
				WbemProvider.ObjectPathRegex objectPathRegex = new WbemProvider.ObjectPathRegex(objectPath);
				WbemProvider.ParameterContext parameterContext = new WbemProvider.ParameterContext(objectPathRegex.ClassName, this.wbemServices, wbemContext, wbemSink);
				WbemProvider.WbemInstance wbemInstance = new WbemProvider.WbemInstance(parameterContext, objectPathRegex);
				WbemProvider.MethodContext method = new WbemProvider.MethodContext(parameterContext, methodName, wbemInParams, wbemInstance);
				IWmiProvider provider = this.GetProvider(parameterContext.ClassName);
				if (!provider.InvokeMethod(method))
				{
					num = -2147217406;
				}
				WbemException.ThrowIfFail(wbemSink.SetStatus(0, num, null, null));
			}
			catch (WbemException ex)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356561U, new string[]
				{
					ex.ToString()
				});
				num = ex.ErrorCode;
				wbemSink.SetStatus(0, num, null, null);
			}
			catch (Exception ex2)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, 3221356561U, new string[]
				{
					ex2.ToString()
				});
				num = -2147217407;
				wbemSink.SetStatus(0, num, null, null);
			}
			finally
			{
				Marshal.ReleaseComObject(wbemSink);
			}
			return num;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000A94D0 File Offset: 0x000A76D0
		internal static void MTAExecute(WaitCallback callback, object state)
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.MTA)
			{
				using (WbemProvider.ThreadJob threadJob = new WbemProvider.ThreadJob(callback, state))
				{
					Thread thread = new Thread(new ThreadStart(threadJob.Run));
					thread.SetApartmentState(ApartmentState.MTA);
					thread.IsBackground = true;
					thread.Start();
					Exception ex = threadJob.Wait();
					if (ex != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ApplicationException(SR.GetString("AdminMTAWorkerThreadException"), ex));
					}
					return;
				}
			}
			callback(state);
		}

		// Token: 0x04002406 RID: 9222
		private object syncRoot = new object();

		// Token: 0x04002407 RID: 9223
		private WbemNative.IWbemDecoupledRegistrar wbemRegistrar;

		// Token: 0x04002408 RID: 9224
		private WbemNative.IWbemServices wbemServices;

		// Token: 0x04002409 RID: 9225
		private Dictionary<string, IWmiProvider> wmiProviders = new Dictionary<string, IWmiProvider>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400240A RID: 9226
		private string nameSpace;

		// Token: 0x0400240B RID: 9227
		private string appName;

		// Token: 0x0400240C RID: 9228
		private bool initialized;

		// Token: 0x02000C29 RID: 3113
		private class InstancesContext : IWmiInstances
		{
			// Token: 0x0600770D RID: 30477 RVA: 0x001BD2CC File Offset: 0x001BB4CC
			internal InstancesContext(WbemProvider.ParameterContext parms)
			{
				this.parms = parms;
			}

			// Token: 0x0600770E RID: 30478 RVA: 0x001BD2DB File Offset: 0x001BB4DB
			IWmiInstance IWmiInstances.NewInstance(string className)
			{
				return new WbemProvider.InstanceContext(new WbemProvider.WbemInstance(this.parms, className));
			}

			// Token: 0x0600770F RID: 30479 RVA: 0x001BD2EE File Offset: 0x001BB4EE
			void IWmiInstances.AddInstance(IWmiInstance inst)
			{
				WbemException.ThrowIfFail(this.parms.WbemSink.Indicate(1, new WbemNative.IWbemClassObject[]
				{
					((WbemProvider.InstanceContext)inst).WbemObject
				}));
			}

			// Token: 0x04004418 RID: 17432
			private WbemProvider.ParameterContext parms;
		}

		// Token: 0x02000C2A RID: 3114
		private class InstanceContext : IWmiInstance
		{
			// Token: 0x06007710 RID: 30480 RVA: 0x001BD31A File Offset: 0x001BB51A
			internal InstanceContext(WbemProvider.WbemInstance wbemInstance)
			{
				this.wbemInstance = wbemInstance;
			}

			// Token: 0x17001B3C RID: 6972
			// (get) Token: 0x06007711 RID: 30481 RVA: 0x001BD329 File Offset: 0x001BB529
			internal WbemNative.IWbemClassObject WbemObject
			{
				get
				{
					return this.wbemInstance.WbemObject;
				}
			}

			// Token: 0x06007712 RID: 30482 RVA: 0x001BD336 File Offset: 0x001BB536
			IWmiInstance IWmiInstance.NewInstance(string className)
			{
				return new WbemProvider.InstanceContext(new WbemProvider.WbemInstance(this.wbemInstance, className));
			}

			// Token: 0x06007713 RID: 30483 RVA: 0x001BD349 File Offset: 0x001BB549
			object IWmiInstance.GetProperty(string name)
			{
				return this.wbemInstance.GetProperty(name);
			}

			// Token: 0x06007714 RID: 30484 RVA: 0x001BD357 File Offset: 0x001BB557
			void IWmiInstance.SetProperty(string name, object val)
			{
				this.wbemInstance.SetProperty(name, val);
			}

			// Token: 0x04004419 RID: 17433
			private WbemProvider.WbemInstance wbemInstance;
		}

		// Token: 0x02000C2B RID: 3115
		private class MethodContext : IWmiMethodContext
		{
			// Token: 0x06007715 RID: 30485 RVA: 0x001BD368 File Offset: 0x001BB568
			internal MethodContext(WbemProvider.ParameterContext parms, string methodName, WbemNative.IWbemClassObject wbemInParms, WbemProvider.WbemInstance wbemInstance)
			{
				this.parms = parms;
				this.methodName = methodName;
				this.wbemInParms = wbemInParms;
				this.instance = new WbemProvider.InstanceContext(wbemInstance);
				WbemNative.IWbemClassObject wbemClassObject = null;
				WbemException.ThrowIfFail(parms.WbemServices.GetObject(parms.ClassName, 0, parms.WbemContext, ref wbemClassObject, IntPtr.Zero));
				WbemNative.IWbemClassObject wbemClassObject2 = null;
				WbemException.ThrowIfFail(wbemClassObject.GetMethod(methodName, 0, IntPtr.Zero, out wbemClassObject2));
				WbemException.ThrowIfFail(wbemClassObject2.SpawnInstance(0, out this.wbemOutParms));
			}

			// Token: 0x17001B3D RID: 6973
			// (get) Token: 0x06007716 RID: 30486 RVA: 0x001BD3EB File Offset: 0x001BB5EB
			string IWmiMethodContext.MethodName
			{
				get
				{
					return this.methodName;
				}
			}

			// Token: 0x17001B3E RID: 6974
			// (get) Token: 0x06007717 RID: 30487 RVA: 0x001BD3F3 File Offset: 0x001BB5F3
			IWmiInstance IWmiMethodContext.Instance
			{
				get
				{
					return this.instance;
				}
			}

			// Token: 0x17001B3F RID: 6975
			// (set) Token: 0x06007718 RID: 30488 RVA: 0x001BD3FC File Offset: 0x001BB5FC
			object IWmiMethodContext.ReturnParameter
			{
				set
				{
					object obj = value;
					WbemException.ThrowIfFail(this.wbemOutParms.Put("ReturnValue", 0, ref obj, 0));
					WbemException.ThrowIfFail(this.parms.WbemSink.Indicate(1, new WbemNative.IWbemClassObject[]
					{
						this.wbemOutParms
					}));
				}
			}

			// Token: 0x06007719 RID: 30489 RVA: 0x001BD44C File Offset: 0x001BB64C
			object IWmiMethodContext.GetParameter(string name)
			{
				object result = null;
				int num = 0;
				int num2 = 0;
				WbemException.ThrowIfFail(this.wbemInParms.Get(name, 0, ref result, ref num, ref num2));
				return result;
			}

			// Token: 0x0600771A RID: 30490 RVA: 0x001BD478 File Offset: 0x001BB678
			void IWmiMethodContext.SetParameter(string name, object value)
			{
				WbemException.ThrowIfFail(this.wbemOutParms.Put(name, 0, ref value, 0));
			}

			// Token: 0x0400441A RID: 17434
			private WbemProvider.ParameterContext parms;

			// Token: 0x0400441B RID: 17435
			private string methodName;

			// Token: 0x0400441C RID: 17436
			private WbemNative.IWbemClassObject wbemInParms;

			// Token: 0x0400441D RID: 17437
			private WbemNative.IWbemClassObject wbemOutParms;

			// Token: 0x0400441E RID: 17438
			private IWmiInstance instance;
		}

		// Token: 0x02000C2C RID: 3116
		private class ObjectPathRegex
		{
			// Token: 0x0600771B RID: 30491 RVA: 0x001BD490 File Offset: 0x001BB690
			public ObjectPathRegex(string objectPath)
			{
				objectPath = objectPath.Replace("\\\\", "\\");
				Match match = WbemProvider.ObjectPathRegex.nsRegEx.Match(objectPath);
				if (match.Success)
				{
					objectPath = match.Groups["path"].Value;
				}
				match = WbemProvider.ObjectPathRegex.classRegEx.Match(objectPath);
				this.className = match.Groups["className"].Value;
				string value = match.Groups["keys"].Value;
				match = WbemProvider.ObjectPathRegex.keysRegEx.Match(value);
				if (!match.Success)
				{
					WbemException.Throw(WbemNative.WbemStatus.WBEM_E_INVALID_OBJECT_PATH);
				}
				while (match.Success)
				{
					if (!string.IsNullOrEmpty(match.Groups["ival"].Value))
					{
						this.keys.Add(match.Groups["key"].Value, int.Parse(match.Groups["ival"].Value, CultureInfo.CurrentCulture));
					}
					else
					{
						this.keys.Add(match.Groups["key"].Value, match.Groups["sval"].Value);
					}
					match = match.NextMatch();
				}
			}

			// Token: 0x17001B40 RID: 6976
			// (get) Token: 0x0600771C RID: 30492 RVA: 0x001BD5F7 File Offset: 0x001BB7F7
			internal string ClassName
			{
				get
				{
					return this.className;
				}
			}

			// Token: 0x17001B41 RID: 6977
			// (get) Token: 0x0600771D RID: 30493 RVA: 0x001BD5FF File Offset: 0x001BB7FF
			internal Dictionary<string, object> Keys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x0400441F RID: 17439
			private static Regex nsRegEx = new Regex("^(?<namespace>[^\"]*?:)(?<path>.*)");

			// Token: 0x04004420 RID: 17440
			private static Regex classRegEx = new Regex("^(?<className>.*?)\\.(?<keys>.*)");

			// Token: 0x04004421 RID: 17441
			private static Regex keysRegEx = new Regex("(?<key>.*?)=((?<ival>[\\d]+)|\"(?<sval>.*?)\"),?");

			// Token: 0x04004422 RID: 17442
			private string className;

			// Token: 0x04004423 RID: 17443
			private Dictionary<string, object> keys = new Dictionary<string, object>();
		}

		// Token: 0x02000C2D RID: 3117
		private class QueryRegex
		{
			// Token: 0x0600771F RID: 30495 RVA: 0x001BD638 File Offset: 0x001BB838
			internal QueryRegex(string query)
			{
				Match match = WbemProvider.QueryRegex.regEx.Match(query);
				if (!match.Success)
				{
					WbemException.Throw(WbemNative.WbemStatus.WBEM_E_INVALID_QUERY);
				}
				this.className = match.Groups["className"].Value;
			}

			// Token: 0x17001B42 RID: 6978
			// (get) Token: 0x06007720 RID: 30496 RVA: 0x001BD684 File Offset: 0x001BB884
			internal string ClassName
			{
				get
				{
					return this.className;
				}
			}

			// Token: 0x04004424 RID: 17444
			private static Regex regEx = new Regex("\\bfrom\\b\\s+(?<className>\\w+)", RegexOptions.IgnoreCase);

			// Token: 0x04004425 RID: 17445
			private string className;
		}

		// Token: 0x02000C2E RID: 3118
		private class ParameterContext
		{
			// Token: 0x06007722 RID: 30498 RVA: 0x001BD69E File Offset: 0x001BB89E
			internal ParameterContext(string className, WbemNative.IWbemServices wbemServices, WbemNative.IWbemContext wbemContext, WbemNative.IWbemObjectSink wbemSink)
			{
				this.className = className;
				this.wbemServices = wbemServices;
				this.wbemContext = wbemContext;
				this.wbemSink = wbemSink;
			}

			// Token: 0x17001B43 RID: 6979
			// (get) Token: 0x06007723 RID: 30499 RVA: 0x001BD6C3 File Offset: 0x001BB8C3
			internal string ClassName
			{
				get
				{
					return this.className;
				}
			}

			// Token: 0x17001B44 RID: 6980
			// (get) Token: 0x06007724 RID: 30500 RVA: 0x001BD6CB File Offset: 0x001BB8CB
			internal WbemNative.IWbemServices WbemServices
			{
				get
				{
					return this.wbemServices;
				}
			}

			// Token: 0x17001B45 RID: 6981
			// (get) Token: 0x06007725 RID: 30501 RVA: 0x001BD6D3 File Offset: 0x001BB8D3
			internal WbemNative.IWbemContext WbemContext
			{
				get
				{
					return this.wbemContext;
				}
			}

			// Token: 0x17001B46 RID: 6982
			// (get) Token: 0x06007726 RID: 30502 RVA: 0x001BD6DB File Offset: 0x001BB8DB
			internal WbemNative.IWbemObjectSink WbemSink
			{
				get
				{
					return this.wbemSink;
				}
			}

			// Token: 0x04004426 RID: 17446
			private string className;

			// Token: 0x04004427 RID: 17447
			private WbemNative.IWbemServices wbemServices;

			// Token: 0x04004428 RID: 17448
			private WbemNative.IWbemContext wbemContext;

			// Token: 0x04004429 RID: 17449
			private WbemNative.IWbemObjectSink wbemSink;
		}

		// Token: 0x02000C2F RID: 3119
		private class WbemInstance
		{
			// Token: 0x06007727 RID: 30503 RVA: 0x001BD6E4 File Offset: 0x001BB8E4
			internal WbemInstance(WbemProvider.ParameterContext parms, WbemProvider.ObjectPathRegex objPathRegex) : this(parms, objPathRegex.ClassName)
			{
				foreach (KeyValuePair<string, object> keyValuePair in objPathRegex.Keys)
				{
					this.SetProperty(keyValuePair.Key, keyValuePair.Value);
				}
			}

			// Token: 0x06007728 RID: 30504 RVA: 0x001BD754 File Offset: 0x001BB954
			internal WbemInstance(WbemProvider.WbemInstance wbemInstance, string className) : this(wbemInstance.parms, className)
			{
			}

			// Token: 0x06007729 RID: 30505 RVA: 0x001BD764 File Offset: 0x001BB964
			internal WbemInstance(WbemProvider.ParameterContext parms, string className)
			{
				this.parms = parms;
				if (string.IsNullOrEmpty(className))
				{
					className = parms.ClassName;
				}
				this.className = className;
				WbemNative.IWbemClassObject wbemClassObject = null;
				WbemException.ThrowIfFail(parms.WbemServices.GetObject(className, 0, parms.WbemContext, ref wbemClassObject, IntPtr.Zero));
				if (wbemClassObject != null)
				{
					WbemException.ThrowIfFail(wbemClassObject.SpawnInstance(0, out this.wbemObject));
				}
			}

			// Token: 0x0600772A RID: 30506 RVA: 0x001BD7CB File Offset: 0x001BB9CB
			internal WbemInstance(WbemProvider.ParameterContext parms, WbemNative.IWbemClassObject wbemObject)
			{
				this.parms = parms;
				this.wbemObject = wbemObject;
			}

			// Token: 0x17001B47 RID: 6983
			// (get) Token: 0x0600772B RID: 30507 RVA: 0x001BD7E1 File Offset: 0x001BB9E1
			internal WbemNative.IWbemClassObject WbemObject
			{
				get
				{
					return this.wbemObject;
				}
			}

			// Token: 0x0600772C RID: 30508 RVA: 0x001BD7EC File Offset: 0x001BB9EC
			internal void SetProperty(string name, object val)
			{
				if (val != null)
				{
					WbemNative.CIMTYPE type = WbemNative.CIMTYPE.CIM_EMPTY;
					if (val is DateTime)
					{
						val = ((DateTime)val).ToString("yyyyMMddhhmmss.ffffff", CultureInfo.InvariantCulture) + "+000";
					}
					else if (val is TimeSpan)
					{
						TimeSpan timeSpan = (TimeSpan)val;
						long num = timeSpan.Ticks % 1000L / 10L;
						val = string.Format(CultureInfo.InvariantCulture, "{0:00000000}{1:00}{2:00}{3:00}.{4:000}{5:000}:000", new object[]
						{
							timeSpan.Days,
							timeSpan.Hours,
							timeSpan.Minutes,
							timeSpan.Seconds,
							timeSpan.Milliseconds,
							num
						});
					}
					else if (val is WbemProvider.InstanceContext)
					{
						WbemProvider.InstanceContext instanceContext = (WbemProvider.InstanceContext)val;
						val = instanceContext.WbemObject;
					}
					else if (val is Array)
					{
						Array array = (Array)val;
						if (array.GetLength(0) > 0 && array.GetValue(0) is WbemProvider.InstanceContext)
						{
							WbemNative.IWbemClassObject[] array2 = new WbemNative.IWbemClassObject[array.GetLength(0)];
							for (int i = 0; i < array2.Length; i++)
							{
								array2[i] = ((WbemProvider.InstanceContext)array.GetValue(i)).WbemObject;
							}
							val = array2;
						}
					}
					else if (val is long)
					{
						val = ((long)val).ToString(CultureInfo.InvariantCulture);
						type = WbemNative.CIMTYPE.CIM_SINT64;
					}
					int num2 = this.wbemObject.Put(name, 0, ref val, (int)type);
					if (-2147217403 == num2 || -2147217406 == num2)
					{
						EventLogEventId eventId;
						if (-2147217403 == num2)
						{
							eventId = (EventLogEventId)3221356564U;
						}
						else
						{
							eventId = (EventLogEventId)3221356565U;
						}
						DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 9, (uint)eventId, new string[]
						{
							this.className,
							name,
							val.GetType().ToString()
						});
						return;
					}
					WbemException.ThrowIfFail(num2);
				}
			}

			// Token: 0x0600772D RID: 30509 RVA: 0x001BD9E4 File Offset: 0x001BBBE4
			internal object GetProperty(string name)
			{
				object result = null;
				int num = 0;
				int num2 = 0;
				WbemException.ThrowIfFail(this.wbemObject.Get(name, 0, ref result, ref num, ref num2));
				return result;
			}

			// Token: 0x0600772E RID: 30510 RVA: 0x001BDA10 File Offset: 0x001BBC10
			internal void Indicate()
			{
				WbemException.ThrowIfFail(this.parms.WbemSink.Indicate(1, new WbemNative.IWbemClassObject[]
				{
					this.wbemObject
				}));
			}

			// Token: 0x0400442A RID: 17450
			private string className;

			// Token: 0x0400442B RID: 17451
			private WbemProvider.ParameterContext parms;

			// Token: 0x0400442C RID: 17452
			private WbemNative.IWbemClassObject wbemObject;
		}

		// Token: 0x02000C30 RID: 3120
		private class ThreadJob : IDisposable
		{
			// Token: 0x0600772F RID: 30511 RVA: 0x001BDA37 File Offset: 0x001BBC37
			public ThreadJob(WaitCallback callback, object state)
			{
				this.callback = callback;
				this.state = state;
			}

			// Token: 0x06007730 RID: 30512 RVA: 0x001BDA5C File Offset: 0x001BBC5C
			public void Run()
			{
				try
				{
					this.callback(this.state);
				}
				catch (Exception ex)
				{
					this.exception = ex;
				}
				finally
				{
					this.evtDone.Set();
				}
			}

			// Token: 0x06007731 RID: 30513 RVA: 0x001BDAB0 File Offset: 0x001BBCB0
			public Exception Wait()
			{
				this.evtDone.WaitOne();
				return this.exception;
			}

			// Token: 0x06007732 RID: 30514 RVA: 0x001BDAC4 File Offset: 0x001BBCC4
			public void Dispose()
			{
				if (this.evtDone != null)
				{
					this.evtDone.Close();
					this.evtDone = null;
				}
			}

			// Token: 0x0400442D RID: 17453
			private WaitCallback callback;

			// Token: 0x0400442E RID: 17454
			private object state;

			// Token: 0x0400442F RID: 17455
			private ManualResetEvent evtDone = new ManualResetEvent(false);

			// Token: 0x04004430 RID: 17456
			private Exception exception;
		}

		// Token: 0x02000C31 RID: 3121
		private class NoInstanceWMIProvider : IWmiProvider
		{
			// Token: 0x17001B48 RID: 6984
			// (get) Token: 0x06007733 RID: 30515 RVA: 0x001BDAE0 File Offset: 0x001BBCE0
			internal static WbemProvider.NoInstanceWMIProvider Default
			{
				get
				{
					if (WbemProvider.NoInstanceWMIProvider.singleton == null)
					{
						WbemProvider.NoInstanceWMIProvider.singleton = new WbemProvider.NoInstanceWMIProvider();
					}
					return WbemProvider.NoInstanceWMIProvider.singleton;
				}
			}

			// Token: 0x06007734 RID: 30516 RVA: 0x001BDAF8 File Offset: 0x001BBCF8
			void IWmiProvider.EnumInstances(IWmiInstances instances)
			{
			}

			// Token: 0x06007735 RID: 30517 RVA: 0x001BDAFA File Offset: 0x001BBCFA
			bool IWmiProvider.GetInstance(IWmiInstance instance)
			{
				return false;
			}

			// Token: 0x06007736 RID: 30518 RVA: 0x001BDAFD File Offset: 0x001BBCFD
			bool IWmiProvider.PutInstance(IWmiInstance instance)
			{
				return false;
			}

			// Token: 0x06007737 RID: 30519 RVA: 0x001BDB00 File Offset: 0x001BBD00
			bool IWmiProvider.DeleteInstance(IWmiInstance instance)
			{
				return false;
			}

			// Token: 0x06007738 RID: 30520 RVA: 0x001BDB03 File Offset: 0x001BBD03
			bool IWmiProvider.InvokeMethod(IWmiMethodContext method)
			{
				return false;
			}

			// Token: 0x04004431 RID: 17457
			private static WbemProvider.NoInstanceWMIProvider singleton;
		}
	}
}
