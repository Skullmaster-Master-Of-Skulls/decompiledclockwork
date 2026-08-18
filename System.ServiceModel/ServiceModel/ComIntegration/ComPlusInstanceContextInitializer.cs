using System;
using System.EnterpriseServices;
using System.IO;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Transactions;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000212 RID: 530
	internal class ComPlusInstanceContextInitializer : IInstanceContextInitializer
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x00039CE8 File Offset: 0x00037EE8
		static ComPlusInstanceContextInitializer()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.AssemblyResolve += ComPlusInstanceContextInitializer.ResolveAssembly;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00039D50 File Offset: 0x00037F50
		public ComPlusInstanceContextInitializer(ServiceInfo info)
		{
			this.info = info;
			if (this.info.HasUdts())
			{
				string text = string.Empty;
				object obj = ComPlusInstanceContextInitializer.manifestLock;
				lock (obj)
				{
					try
					{
						text = Path.GetTempPath();
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.CannotAccessDirectory(text));
					}
					string path = text + this.info.AppID.ToString();
					if (Directory.Exists(path))
					{
						Directory.Delete(path, true);
					}
				}
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00039E0C File Offset: 0x0003800C
		public void Initialize(InstanceContext instanceContext, Message message)
		{
			object obj = this.SetupServiceConfig(instanceContext, message);
			IServiceActivity activity = (IServiceActivity)SafeNativeMethods.CoCreateActivity(obj, ComPlusInstanceContextInitializer.IID_IServiceActivity);
			bool postSynchronous = this.info.ThreadingModel == ThreadingModel.MTA;
			ComPlusSynchronizationContext synchronizationContext = new ComPlusSynchronizationContext(activity, postSynchronous);
			instanceContext.SynchronizationContext = synchronizationContext;
			instanceContext.Closing += this.OnInstanceContextClosing;
			Marshal.ReleaseComObject(obj);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00039E6C File Offset: 0x0003806C
		public void OnInstanceContextClosing(object sender, EventArgs args)
		{
			InstanceContext instanceContext = (InstanceContext)sender;
			ComPlusSynchronizationContext comPlusSynchronizationContext = (ComPlusSynchronizationContext)instanceContext.SynchronizationContext;
			comPlusSynchronizationContext.Dispose();
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00039E94 File Offset: 0x00038094
		private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
		{
			int num = args.Name.IndexOf(",", StringComparison.Ordinal);
			if (num != -1)
			{
				Guid empty = Guid.Empty;
				string input = args.Name.Substring(0, num).Trim().ToLowerInvariant();
				if (Guid.TryParse(input, out empty))
				{
					return TypeCacheManager.Provider.ResolveAssembly(empty);
				}
			}
			return null;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00039EEC File Offset: 0x000380EC
		private object SetupServiceConfig(InstanceContext instanceContext, Message message)
		{
			object obj = new CServiceConfig();
			IServiceThreadPoolConfig serviceThreadPoolConfig = (IServiceThreadPoolConfig)obj;
			ThreadingModel threadingModel = this.info.ThreadingModel;
			if (threadingModel != ThreadingModel.MTA)
			{
				if (threadingModel != ThreadingModel.STA)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.UnexpectedThreadingModel());
				}
				serviceThreadPoolConfig.SelectThreadPool(ThreadPoolOption.STA);
			}
			else
			{
				serviceThreadPoolConfig.SelectThreadPool(ThreadPoolOption.MTA);
			}
			serviceThreadPoolConfig.SetBindingInfo(BindingOption.BindingToPoolThread);
			if (this.info.HasUdts())
			{
				IServiceSxsConfig serviceSxsConfig = obj as IServiceSxsConfig;
				if (serviceSxsConfig == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.QFENotPresent());
				}
				object obj2 = ComPlusInstanceContextInitializer.manifestLock;
				lock (obj2)
				{
					string text = string.Empty;
					try
					{
						text = Path.GetTempPath();
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.CannotAccessDirectory(text));
					}
					string text2 = text + this.info.AppID.ToString() + "\\";
					if (!Directory.Exists(text2))
					{
						try
						{
							Directory.CreateDirectory(text2);
						}
						catch (Exception exception2)
						{
							if (Fx.IsFatal(exception2))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.CannotAccessDirectory(text2));
						}
						Guid[] assemblies = this.info.Assemblies;
						ComIntegrationManifestGenerator.GenerateManifestCollectionFile(assemblies, text2 + ComPlusInstanceContextInitializer.manifestFileName + ".manifest", ComPlusInstanceContextInitializer.manifestFileName);
						foreach (Guid assemblyId in assemblies)
						{
							Type[] types = this.info.GetTypes(assemblyId);
							if (types.Length != 0)
							{
								string text3 = assemblyId.ToString();
								ComIntegrationManifestGenerator.GenerateWin32ManifestFile(types, text2 + text3 + ".manifest", text3);
							}
						}
					}
					serviceSxsConfig.SxsConfig(CSC_SxsConfig.CSC_NewSxs);
					serviceSxsConfig.SxsName(ComPlusInstanceContextInitializer.manifestFileName + ".manifest");
					serviceSxsConfig.SxsDirectory(text2);
				}
			}
			if (this.info.PartitionId != ComPlusInstanceContextInitializer.DefaultPartitionId)
			{
				IServicePartitionConfig servicePartitionConfig = (IServicePartitionConfig)obj;
				servicePartitionConfig.PartitionConfig(PartitionOption.New);
				servicePartitionConfig.PartitionID(this.info.PartitionId);
			}
			IServiceTransactionConfig serviceTransactionConfig = (IServiceTransactionConfig)obj;
			serviceTransactionConfig.ConfigureTransaction(TransactionConfig.NoTransaction);
			if (this.info.TransactionOption == TransactionOption.Required || this.info.TransactionOption == TransactionOption.Supported)
			{
				Transaction messageTransaction = MessageUtil.GetMessageTransaction(message);
				if (messageTransaction != null)
				{
					TransactionProxy transactionProxy = new TransactionProxy(this.info.AppID, this.info.Clsid);
					transactionProxy.SetTransaction(messageTransaction);
					instanceContext.Extensions.Add(transactionProxy);
					IServiceSysTxnConfig serviceSysTxnConfig = (IServiceSysTxnConfig)serviceTransactionConfig;
					IntPtr intPtr = TransactionProxyBuilder.CreateTransactionProxyTearOff(transactionProxy);
					serviceSysTxnConfig.ConfigureBYOTSysTxn(intPtr);
					Marshal.Release(intPtr);
				}
			}
			return obj;
		}

		// Token: 0x0400185D RID: 6237
		private ServiceInfo info;

		// Token: 0x0400185E RID: 6238
		private static readonly Guid IID_IServiceActivity = new Guid("67532E0C-9E2F-4450-A354-035633944E17");

		// Token: 0x0400185F RID: 6239
		private static readonly Guid DefaultPartitionId = new Guid("41E90F3E-56C1-4633-81C3-6E8BAC8BDD70");

		// Token: 0x04001860 RID: 6240
		private static object manifestLock = new object();

		// Token: 0x04001861 RID: 6241
		private static string manifestFileName = Guid.NewGuid().ToString();
	}
}
