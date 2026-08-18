using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.Threading;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000240 RID: 576
	internal class ProxySupportWrapper
	{
		// Token: 0x06001113 RID: 4371 RVA: 0x0003EC09 File Offset: 0x0003CE09
		internal ProxySupportWrapper()
		{
			this.monikerSupportLibrary = null;
			this.getCODelegate = null;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003EC24 File Offset: 0x0003CE24
		~ProxySupportWrapper()
		{
			if (this.monikerSupportLibrary != null)
			{
				this.monikerSupportLibrary.Close();
				this.monikerSupportLibrary = null;
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0003EC6C File Offset: 0x0003CE6C
		internal IProxyProvider GetProxyProvider()
		{
			if (this.monikerSupportLibrary == null)
			{
				lock (this)
				{
					if (this.monikerSupportLibrary == null)
					{
						this.getCODelegate = null;
						using (RegistryHandle correctBitnessHKLMSubkey = RegistryHandle.GetCorrectBitnessHKLMSubkey(IntPtr.Size == 8, "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Client"))
						{
							string text = correctBitnessHKLMSubkey.GetStringValue("InstallPath").TrimEnd(new char[1]) + "\\ServiceMonikerSupport.dll";
							SafeLibraryHandle safeLibraryHandle = UnsafeNativeMethods.LoadLibrary(text);
							safeLibraryHandle.DoNotFreeLibraryOnRelease();
							this.monikerSupportLibrary = safeLibraryHandle;
							if (this.monikerSupportLibrary.IsInvalid)
							{
								this.monikerSupportLibrary.SetHandleAsInvalid();
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ServiceMonikerSupportLoadFailed(text));
							}
						}
					}
				}
			}
			if (this.getCODelegate == null)
			{
				lock (this)
				{
					if (this.getCODelegate == null)
					{
						try
						{
							IntPtr procAddress = UnsafeNativeMethods.GetProcAddress(this.monikerSupportLibrary, "DllGetClassObject");
							this.getCODelegate = (ProxySupportWrapper.DelegateDllGetClassObject)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(ProxySupportWrapper.DelegateDllGetClassObject));
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ComPlusProxyProviderException(SR.GetString("FailedProxyProviderCreation"), ex));
						}
					}
				}
			}
			IClassFactory classFactory = null;
			IProxyProvider result = null;
			try
			{
				this.getCODelegate(ProxySupportWrapper.ClsidProxyInstanceProvider, typeof(IClassFactory).GUID, ref classFactory);
				result = (classFactory.CreateInstance(null, typeof(IProxyProvider).GUID) as IProxyProvider);
				Thread.MemoryBarrier();
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ComPlusProxyProviderException(SR.GetString("FailedProxyProviderCreation"), ex2));
			}
			finally
			{
				if (classFactory != null)
				{
					Marshal.ReleaseComObject(classFactory);
					classFactory = null;
				}
			}
			return result;
		}

		// Token: 0x04001897 RID: 6295
		private const string fileName = "ServiceMonikerSupport.dll";

		// Token: 0x04001898 RID: 6296
		private const string functionName = "DllGetClassObject";

		// Token: 0x04001899 RID: 6297
		private static readonly Guid ClsidProxyInstanceProvider = new Guid("(BF0514FB-6912-4659-AD69-B727E5B7ADD4)");

		// Token: 0x0400189A RID: 6298
		private volatile SafeLibraryHandle monikerSupportLibrary;

		// Token: 0x0400189B RID: 6299
		private volatile ProxySupportWrapper.DelegateDllGetClassObject getCODelegate;

		// Token: 0x02000B13 RID: 2835
		// (Invoke) Token: 0x06006F78 RID: 28536
		internal delegate int DelegateDllGetClassObject([MarshalAs(UnmanagedType.LPStruct)] [In] Guid clsid, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid iid, ref IClassFactory ppv);
	}
}
