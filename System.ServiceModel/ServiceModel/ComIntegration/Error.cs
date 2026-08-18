using System;
using System.Globalization;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200021F RID: 543
	internal static class Error
	{
		// Token: 0x06001071 RID: 4209 RVA: 0x0003CFEA File Offset: 0x0003B1EA
		public static Exception ActivationAccessDenied()
		{
			return Error.CreateFault("ComActivationAccessDenied", SR.GetString("ComActivationAccessDenied"));
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0003D000 File Offset: 0x0003B200
		public static Exception QFENotPresent()
		{
			return Error.CreateFault("ServiceHostStartingServiceErrorNoQFE", SR.GetString("ComPlusServiceHostStartingServiceErrorNoQFE"));
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0003D016 File Offset: 0x0003B216
		public static Exception DirectoryNotFound(string directory)
		{
			return Error.CreateFault("DirectoryNotFound", SR.GetString("TempDirectoryNotFound", new object[]
			{
				directory
			}));
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0003D036 File Offset: 0x0003B236
		public static Exception CannotAccessDirectory(string directory)
		{
			return Error.CreateFault("CannotAccessDirectory", SR.GetString("CannotAccessDirectory", new object[]
			{
				directory
			}));
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0003D056 File Offset: 0x0003B256
		public static Exception ManifestCreationFailed(string file, string error)
		{
			return Error.CreateFault("ManifestCreationFailed", SR.GetString("ComIntegrationManifestCreationFailed", new object[]
			{
				file,
				error
			}));
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003D07A File Offset: 0x0003B27A
		public static Exception ActivationFailure()
		{
			return Error.CreateFault("ComActivationFailure", SR.GetString("ComActivationFailure"));
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003D090 File Offset: 0x0003B290
		public static Exception UnexpectedThreadingModel()
		{
			return Error.CreateFault("UnexpectedThreadingModel", SR.GetString("UnexpectedThreadingModel"));
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003D0A6 File Offset: 0x0003B2A6
		public static Exception DllHostInitializerFoundNoServices()
		{
			return Error.CreateFault("DllHostInitializerFoundNoServices", SR.GetString("ComDllHostInitializerFoundNoServices"));
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0003D0BC File Offset: 0x0003B2BC
		public static Exception ServiceMonikerSupportLoadFailed(string dllname)
		{
			return Error.CreateFault("UnableToLoadServiceMonikerSupportDll", SR.GetString("UnableToLoadDll", new object[]
			{
				dllname
			}));
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0003D0DC File Offset: 0x0003B2DC
		public static Exception CallAccessDenied()
		{
			return Error.CreateFault("ComAccessDenied", SR.GetString("ComMessageAccessDenied"));
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0003D0F2 File Offset: 0x0003B2F2
		public static Exception RequiresWindowsSecurity()
		{
			return Error.CreateFault("ComWindowsIdentityRequired", SR.GetString("ComRequiresWindowsSecurity"));
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0003D108 File Offset: 0x0003B308
		public static Exception NoAsyncOperationsAllowed()
		{
			return Error.CreateFault("NoAsyncOperationsAllowed", SR.GetString("ComNoAsyncOperationsAllowed"));
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003D11E File Offset: 0x0003B31E
		public static Exception DuplicateOperation()
		{
			return Error.CreateFault("DuplicateOperation", SR.GetString("ComDuplicateOperation"));
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0003D134 File Offset: 0x0003B334
		public static Exception InconsistentSessionRequirements()
		{
			return Error.CreateFault("ComInconsistentSessionRequirements", SR.GetString("ComInconsistentSessionRequirements"));
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003D14A File Offset: 0x0003B34A
		public static Exception TransactionMismatch()
		{
			return Error.CreateFault("Transactions", SR.GetString("SFxTransactionsNotSupported"));
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0003D160 File Offset: 0x0003B360
		public static Exception ListenerInitFailed(string message)
		{
			return new ComPlusListenerInitializationException(message);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0003D168 File Offset: 0x0003B368
		public static Exception ListenerInitFailed(string message, Exception inner)
		{
			return new ComPlusListenerInitializationException(message, inner);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0003D174 File Offset: 0x0003B374
		private static Exception CreateFault(string code, string reason)
		{
			FaultCode code2 = FaultCode.CreateSenderFaultCode(code, "http://schemas.xmlsoap.org/Microsoft/WindowsCommunicationFoundation/2005/08/Faults/");
			FaultReason reason2 = new FaultReason(reason, CultureInfo.CurrentCulture);
			return new FaultException(reason2, code2);
		}

		// Token: 0x04001880 RID: 6272
		private const string FaultNamespace = "http://schemas.xmlsoap.org/Microsoft/WindowsCommunicationFoundation/2005/08/Faults/";
	}
}
