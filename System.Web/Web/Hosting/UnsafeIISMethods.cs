using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Web.Hosting
{
	// Token: 0x02000292 RID: 658
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	internal sealed class UnsafeIISMethods
	{
		// Token: 0x0600224D RID: 8781 RVA: 0x00095D44 File Offset: 0x00094D44
		private UnsafeIISMethods()
		{
		}

		// Token: 0x0600224E RID: 8782
		[DllImport("webengine.dll")]
		internal static extern int MgdGetRequestBasics(IntPtr pRequestContext, out int pContentType, out int pContentTotalLength, out IntPtr pPathTranslated, out int pcchPathTranslated);

		// Token: 0x0600224F RID: 8783
		[DllImport("webengine.dll")]
		internal static extern int MgdGetHeaderChanges(IntPtr pRequestContext, bool fResponse, out IntPtr knownHeaderSnapshot, out int unknownHeaderSnapshotCount, out IntPtr unknownHeaderSnapshotNames, out IntPtr unknownHeaderSnapshotValues, out IntPtr diffKnownIndicies, out int diffUnknownCount, out IntPtr diffUnknownIndicies);

		// Token: 0x06002250 RID: 8784
		[DllImport("webengine.dll")]
		internal static extern int MgdGetServerVarChanges(IntPtr pRequestContext, out int count, out IntPtr names, out IntPtr values, out int diffCount, out IntPtr diffIndicies);

		// Token: 0x06002251 RID: 8785
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetServerVariableW(IntPtr pHandler, string pszVarName, out IntPtr ppBuffer, out int pcchBufferSize);

		// Token: 0x06002252 RID: 8786
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetServerVariableA(IntPtr pHandler, string pszVarName, out IntPtr ppBuffer, out int pcchBufferSize);

		// Token: 0x06002253 RID: 8787
		[DllImport("webengine.dll")]
		internal static extern bool MgdHasConfigChanged();

		// Token: 0x06002254 RID: 8788
		[DllImport("webengine.dll")]
		internal static extern void MgdSetBadRequestStatus(IntPtr pHandler);

		// Token: 0x06002255 RID: 8789
		[DllImport("webengine.dll")]
		internal static extern void MgdSetManagedHttpContext(IntPtr pHandler, IntPtr pManagedHttpContext);

		// Token: 0x06002256 RID: 8790
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdSetStatusW(IntPtr pRequestContext, int dwStatusCode, int dwSubStatusCode, string pszReason, string pszErrorDescription, bool fTrySkipCustomErrors);

		// Token: 0x06002257 RID: 8791
		[DllImport("webengine.dll")]
		internal static extern int MgdSetKnownHeader(IntPtr pRequestContext, bool fRequest, bool fReplace, ushort uHeaderIndex, byte[] value, ushort valueSize);

		// Token: 0x06002258 RID: 8792
		[DllImport("webengine.dll")]
		internal static extern int MgdSetUnknownHeader(IntPtr pRequestContext, bool fRequest, bool fReplace, byte[] header, byte[] value, ushort valueSize);

		// Token: 0x06002259 RID: 8793
		[DllImport("webengine.dll")]
		internal static extern int MgdFlushCore(IntPtr pRequestContext, bool keepConnected, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int[] fragmentsNative);

		// Token: 0x0600225A RID: 8794
		[DllImport("webengine.dll")]
		internal static extern int MgdSetKernelCachePolicy(IntPtr pHandler, int secondsToLive);

		// Token: 0x0600225B RID: 8795
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdFlushKernelCache(string cacheKey);

		// Token: 0x0600225C RID: 8796
		[DllImport("webengine.dll")]
		internal static extern void MgdDisableKernelCache(IntPtr pHandler);

		// Token: 0x0600225D RID: 8797
		[DllImport("webengine.dll")]
		internal static extern void MgdDisableUserCache(IntPtr pHandler);

		// Token: 0x0600225E RID: 8798
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdRegisterEventSubscription(IntPtr pAppContext, string pszModuleName, [MarshalAs(UnmanagedType.U4)] RequestNotification requestNotifications, [MarshalAs(UnmanagedType.U4)] RequestNotification postRequestNotifications, string pszModuleType, string pszModulePrecondition, IntPtr moduleSpecificData, bool useHighPriority);

		// Token: 0x0600225F RID: 8799
		[DllImport("webengine.dll")]
		internal static extern void MgdIndicateCompletion(IntPtr pHandler, [MarshalAs(UnmanagedType.U4)] ref RequestNotificationStatus notificationStatus);

		// Token: 0x06002260 RID: 8800
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdPostCompletion(IntPtr pHandler, [MarshalAs(UnmanagedType.U4)] RequestNotificationStatus notificationStatus);

		// Token: 0x06002261 RID: 8801
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdSyncReadRequest(IntPtr pHandler, byte[] pBuffer, int offset, int cbBuffer, out int pBytesRead, [MarshalAs(UnmanagedType.U4)] uint timeout);

		// Token: 0x06002262 RID: 8802
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetQueryString(IntPtr pHandler, out IntPtr pBuffer, out int cchBufferSize);

		// Token: 0x06002263 RID: 8803
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetUserToken(IntPtr pHandler, out IntPtr pToken);

		// Token: 0x06002264 RID: 8804
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetVirtualToken(IntPtr pHandler, out IntPtr pToken);

		// Token: 0x06002265 RID: 8805
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern bool MgdIsClientConnected(IntPtr pHandler);

		// Token: 0x06002266 RID: 8806
		[DllImport("webengine.dll")]
		internal static extern bool MgdIsHandlerExecutionDenied(IntPtr pHandler);

		// Token: 0x06002267 RID: 8807
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern void MgdCloseConnection(IntPtr pHandler);

		// Token: 0x06002268 RID: 8808
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern void MgdAbortConnection(IntPtr pHandler);

		// Token: 0x06002269 RID: 8809
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetHandlerTypeString(IntPtr pHandler, out IntPtr ppszTypeString, out int pcchTypeString);

		// Token: 0x0600226A RID: 8810
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetApplicationInfo(IntPtr pHandler, out IntPtr pVirtualPath, out int cchVirtualPath, out IntPtr pPhysPath, out int cchPhysPath);

		// Token: 0x0600226B RID: 8811
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetUriPath(IntPtr pHandler, out IntPtr ppPath, out int pcchPath, bool fIncludePathInfo, bool fUseParentContext);

		// Token: 0x0600226C RID: 8812
		[DllImport("webengine.dll")]
		internal static extern int MgdGetPreloadedContent(IntPtr pHandler, byte[] pBuffer, int lOffset, int cbLen, out int pcbReceived);

		// Token: 0x0600226D RID: 8813
		[DllImport("webengine.dll")]
		internal static extern int MgdGetPreloadedSize(IntPtr pHandler, out int pcbAvailable);

		// Token: 0x0600226E RID: 8814
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetPrincipal(IntPtr pHandler, out IntPtr pToken, out IntPtr ppAuthType, ref int pcchAuthType, out IntPtr ppUserName, ref int pcchUserName);

		// Token: 0x0600226F RID: 8815
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdIsInRole(IntPtr pHandler, string pszRoleName, out bool pfIsInRole);

		// Token: 0x06002270 RID: 8816
		[DllImport("webengine.dll")]
		internal static extern IntPtr MgdAllocateRequestMemory(IntPtr pHandler, int cbSize);

		// Token: 0x06002271 RID: 8817
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdAppDomainShutdown(IntPtr appContext);

		// Token: 0x06002272 RID: 8818
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr MgdGetBufferPool(int cbBufferSize);

		// Token: 0x06002273 RID: 8819
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr MgdGetBuffer(IntPtr pPool);

		// Token: 0x06002274 RID: 8820
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr MgdReturnBuffer(IntPtr pBuffer);

		// Token: 0x06002275 RID: 8821
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetLocalPort(IntPtr context);

		// Token: 0x06002276 RID: 8822
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetRemotePort(IntPtr context);

		// Token: 0x06002277 RID: 8823
		[DllImport("webengine.dll")]
		internal static extern int MgdGetUserAgent(IntPtr pRequestContext, out IntPtr pBuffer, out int cbBufferSize);

		// Token: 0x06002278 RID: 8824
		[DllImport("webengine.dll")]
		internal static extern int MgdGetCookieHeader(IntPtr pRequestContext, out IntPtr pBuffer, out int cbBufferSize);

		// Token: 0x06002279 RID: 8825
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdRewriteUrl(IntPtr pRequestContext, string pszUrl, bool fResetQueryString);

		// Token: 0x0600227A RID: 8826
		[DllImport("webengine.dll")]
		internal static extern int MgdGetMethod(IntPtr pRequestContext, out IntPtr pBuffer, out int cbBufferSize);

		// Token: 0x0600227B RID: 8827
		[DllImport("webengine.dll")]
		internal static extern int MgdGetCurrentModuleName(IntPtr pHandler, out IntPtr pBuffer, out int cbBufferSize);

		// Token: 0x0600227C RID: 8828
		[DllImport("webengine.dll")]
		internal static extern int MgdGetCurrentNotification(IntPtr pRequestContext);

		// Token: 0x0600227D RID: 8829
		[DllImport("webengine.dll")]
		internal static extern bool MgdIsCurrentNotificationPost(IntPtr pRequestContext);

		// Token: 0x0600227E RID: 8830
		[DllImport("webengine.dll")]
		internal static extern void MgdDisableNotifications(IntPtr pRequestContext, [MarshalAs(UnmanagedType.U4)] RequestNotification notifications, [MarshalAs(UnmanagedType.U4)] RequestNotification postNotifications);

		// Token: 0x0600227F RID: 8831
		[DllImport("webengine.dll")]
		internal static extern int MgdGetNextNotification(IntPtr pRequestContext, [MarshalAs(UnmanagedType.U4)] RequestNotificationStatus dwStatus);

		// Token: 0x06002280 RID: 8832
		[DllImport("webengine.dll")]
		internal static extern int MgdClearResponse(IntPtr pRequestContext, bool fClearEntity, bool fClearHeaders);

		// Token: 0x06002281 RID: 8833
		[DllImport("webengine.dll")]
		internal static extern int MgdGetRequestTraceGuid(IntPtr pRequestContext, out Guid traceContextId);

		// Token: 0x06002282 RID: 8834
		[DllImport("webengine.dll")]
		internal static extern int MgdGetStatusChanges(IntPtr pRequestContext, out ushort statusCode, out ushort subStatusCode, out IntPtr pBuffer, out ushort cbBufferSize);

		// Token: 0x06002283 RID: 8835
		[DllImport("webengine.dll")]
		internal static extern int MgdGetResponseChunks(IntPtr pRequestContext, ref int fragmentCount, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int[] fragmentChunkType);

		// Token: 0x06002284 RID: 8836
		[DllImport("webengine.dll")]
		internal static extern int MgdEtwGetTraceConfig(IntPtr pRequestContext, out bool providerEnabled, out int flags, out int level);

		// Token: 0x06002285 RID: 8837
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdEmitSimpleTrace(IntPtr pRequestContext, int type, string eventData);

		// Token: 0x06002286 RID: 8838
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdEmitWebEventTrace(IntPtr pRequestContext, int webEventType, int fieldCount, string[] fieldNames, int[] fieldTypes, string[] fieldData);

		// Token: 0x06002287 RID: 8839
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdSetRequestPrincipal(IntPtr pRequestContext, IntPtr pManagedPrincipal, string userName, string authType, IntPtr token);

		// Token: 0x06002288 RID: 8840
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern bool MgdCanDisposeManagedContext(IntPtr pRequestContext, [MarshalAs(UnmanagedType.U4)] RequestNotificationStatus dwStatus);

		// Token: 0x06002289 RID: 8841
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern bool MgdIsLastNotification(IntPtr pRequestContext, [MarshalAs(UnmanagedType.U4)] RequestNotificationStatus dwStatus);

		// Token: 0x0600228A RID: 8842
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern bool MgdIsWithinApp(string siteName, string appPath, string virtualPath);

		// Token: 0x0600228B RID: 8843
		[DllImport("webengine.dll")]
		internal static extern int MgdGetSiteNameFromId([MarshalAs(UnmanagedType.U4)] uint siteId, out IntPtr bstrSiteName, out int cchSiteName);

		// Token: 0x0600228C RID: 8844
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetAppPathForPath([MarshalAs(UnmanagedType.U4)] uint siteId, string virtualPath, out IntPtr bstrPath, out int cchPath);

		// Token: 0x0600228D RID: 8845
		[DllImport("webengine.dll")]
		internal static extern int MgdGetMemoryLimitKB(out long limit);

		// Token: 0x0600228E RID: 8846
		[DllImport("webengine.dll")]
		internal static extern int MgdGetModuleCollection(IntPtr appContext, out IntPtr pModuleCollection, out int count);

		// Token: 0x0600228F RID: 8847
		[DllImport("webengine.dll")]
		internal static extern int MgdGetNextModule(IntPtr pModuleCollection, ref uint dwIndex, out IntPtr bstrModuleName, out int cchModuleName, out IntPtr bstrModuleType, out int cchModuleType, out IntPtr bstrModulePrecondition, out int cchModulePrecondition);

		// Token: 0x06002290 RID: 8848
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetVrPathCreds(string siteName, string virtualPath, out IntPtr bstrUserName, out int cchUserName, out IntPtr bstrPassword, out int cchPassword);

		// Token: 0x06002291 RID: 8849
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetAppCollection(string siteName, string virtualPath, out IntPtr bstrPath, out int cchPath, out IntPtr pAppCollection, out int count);

		// Token: 0x06002292 RID: 8850
		[DllImport("webengine.dll")]
		internal static extern int MgdGetNextVPath(IntPtr pAppCollection, uint dwIndex, out IntPtr bstrPath, out int cchPath);

		// Token: 0x06002293 RID: 8851
		[DllImport("webengine.dll")]
		internal static extern int MgdInitNativeConfig();

		// Token: 0x06002294 RID: 8852
		[DllImport("webengine.dll")]
		internal static extern void MgdTerminateNativeConfig();

		// Token: 0x06002295 RID: 8853
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdMapPathDirect(string siteName, string virtualPath, out IntPtr bstrPhysicalPath, out int cchPath);

		// Token: 0x06002296 RID: 8854
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdMapHandler(IntPtr pHandler, string method, string virtualPath, out IntPtr ppszTypeString, out int pcchTypeString, bool convertNativeStaticFileModule, bool ignoreWildcardMappings);

		// Token: 0x06002297 RID: 8855
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdReMapHandler(IntPtr pHandler, string pszVirtualPath, out IntPtr ppszTypeString, out int pcchTypeString, out bool pfHandlerExists);

		// Token: 0x06002298 RID: 8856
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdSetRemapHandler(IntPtr pHandler, string pszName, string ppszType);

		// Token: 0x06002299 RID: 8857
		[DllImport("webengine.dll")]
		internal static extern int MgdSetNativeConfiguration(IntPtr nativeConfig);

		// Token: 0x0600229A RID: 8858
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.U4)]
		internal static extern uint MgdResolveSiteName(string siteName);

		// Token: 0x0600229B RID: 8859
		[DllImport("webengine.dll")]
		internal static extern void MgdSetResponseFilter(IntPtr context);

		// Token: 0x0600229C RID: 8860
		[DllImport("webengine.dll")]
		internal static extern int MgdGetFileChunkInfo(IntPtr context, int chunkOffset, out long offset, out long length);

		// Token: 0x0600229D RID: 8861
		[DllImport("webengine.dll")]
		internal static extern int MgdReadChunkHandle(IntPtr context, IntPtr FileHandle, long startOffset, ref int length, IntPtr chunkEntity);

		// Token: 0x0600229E RID: 8862
		[DllImport("webengine.dll")]
		internal static extern int MgdExplicitFlush(IntPtr context);

		// Token: 0x0600229F RID: 8863
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdSetServerVariableW(IntPtr context, string variableName, string variableValue);

		// Token: 0x060022A0 RID: 8864
		[DllImport("webengine.dll")]
		internal static extern int MgdGetCurrentModuleIndex(IntPtr pRequestContext);

		// Token: 0x060022A1 RID: 8865
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdExecuteUrl(IntPtr context, string url, bool resetQuerystring, bool preserveForm, byte[] entityBody, uint entityBodySize, string method, int numHeaders, string[] headersNames, string[] headersValues);

		// Token: 0x060022A2 RID: 8866
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetClientCertificate(IntPtr pHandler, out IntPtr ppbClientCert, out int pcbClientCert, out IntPtr ppbClientCertIssuer, out int pcbClientCertIssuer, out IntPtr ppbClientCertPublicKey, out int pcbClientCertPublicKey, out uint pdwCertEncodingType, out long ftNotBefore, out long ftNotAfter);

		// Token: 0x060022A3 RID: 8867
		[DllImport("webengine.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetAnonymousUserToken(IntPtr pHandler, out IntPtr pToken);

		// Token: 0x060022A4 RID: 8868
		[DllImport("webengine.dll")]
		internal static extern int MgdGetChannelBindingToken(IntPtr pHandler, out IntPtr ppbToken, out int pcbTokenSize);

		// Token: 0x04001B60 RID: 7008
		private const string _IIS_NATIVE_DLL = "webengine.dll";

		// Token: 0x04001B61 RID: 7009
		internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
	}
}
