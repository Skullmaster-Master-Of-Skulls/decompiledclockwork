using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Web.Hosting
{
	// Token: 0x020007B2 RID: 1970
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeIISMethods
	{
		// Token: 0x06005E55 RID: 24149
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetRequestBasics(IntPtr pRequestContext, out int pContentType, out int pContentTotalLength, out IntPtr pPathTranslated, out int pcchPathTranslated, out IntPtr pCacheUrl, out int pcchCacheUrl, out IntPtr pHttpMethod, out IntPtr pCookedUrl);

		// Token: 0x06005E56 RID: 24150
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetHeaderChanges(IntPtr pRequestContext, bool fResponse, out IntPtr knownHeaderSnapshot, out int unknownHeaderSnapshotCount, out IntPtr unknownHeaderSnapshotNames, out IntPtr unknownHeaderSnapshotValues, out IntPtr diffKnownIndicies, out int diffUnknownCount, out IntPtr diffUnknownIndicies);

		// Token: 0x06005E57 RID: 24151
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetServerVarChanges(IntPtr pRequestContext, out int count, out IntPtr names, out IntPtr values, out int diffCount, out IntPtr diffIndicies);

		// Token: 0x06005E58 RID: 24152
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetServerVariableW(IntPtr pHandler, string pszVarName, out IntPtr ppBuffer, out int pcchBufferSize);

		// Token: 0x06005E59 RID: 24153
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetServerVariableA(IntPtr pHandler, string pszVarName, out IntPtr ppBuffer, out int pcchBufferSize);

		// Token: 0x06005E5A RID: 24154
		[DllImport("webengine4.dll")]
		internal static extern IntPtr MgdGetStopListeningEventHandle();

		// Token: 0x06005E5B RID: 24155
		[DllImport("webengine4.dll")]
		internal static extern void MgdSetBadRequestStatus(IntPtr pHandler);

		// Token: 0x06005E5C RID: 24156
		[DllImport("webengine4.dll")]
		internal static extern void MgdSetManagedHttpContext(IntPtr pHandler, IntPtr pManagedHttpContext);

		// Token: 0x06005E5D RID: 24157
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdSetStatusW(IntPtr pRequestContext, int dwStatusCode, int dwSubStatusCode, string pszReason, string pszErrorDescription, bool fTrySkipCustomErrors);

		// Token: 0x06005E5E RID: 24158
		[DllImport("webengine4.dll")]
		internal static extern int MgdSetKnownHeader(IntPtr pRequestContext, bool fRequest, bool fReplace, ushort uHeaderIndex, byte[] value, ushort valueSize);

		// Token: 0x06005E5F RID: 24159
		[DllImport("webengine4.dll")]
		internal static extern int MgdSetUnknownHeader(IntPtr pRequestContext, bool fRequest, bool fReplace, byte[] header, byte[] value, ushort valueSize);

		// Token: 0x06005E60 RID: 24160
		[DllImport("webengine4.dll")]
		internal static extern int MgdFlushCore(IntPtr pRequestContext, bool keepConnected, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int[] fragmentsNative);

		// Token: 0x06005E61 RID: 24161
		[DllImport("webengine4.dll")]
		internal static extern int MgdSetKernelCachePolicy(IntPtr pHandler, int secondsToLive);

		// Token: 0x06005E62 RID: 24162
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdFlushKernelCache(string cacheKey);

		// Token: 0x06005E63 RID: 24163
		[DllImport("webengine4.dll")]
		internal static extern void MgdDisableKernelCache(IntPtr pHandler);

		// Token: 0x06005E64 RID: 24164
		[DllImport("webengine4.dll")]
		internal static extern void MgdDisableUserCache(IntPtr pHandler);

		// Token: 0x06005E65 RID: 24165
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdRegisterEventSubscription(IntPtr pAppContext, string pszModuleName, [MarshalAs(UnmanagedType.U4)] RequestNotification requestNotifications, [MarshalAs(UnmanagedType.U4)] RequestNotification postRequestNotifications, string pszModuleType, string pszModulePrecondition, IntPtr moduleSpecificData, bool useHighPriority);

		// Token: 0x06005E66 RID: 24166
		[DllImport("webengine4.dll")]
		internal static extern void MgdIndicateCompletion(IntPtr pHandler, [MarshalAs(UnmanagedType.U4)] ref RequestNotificationStatus notificationStatus);

		// Token: 0x06005E67 RID: 24167
		[DllImport("webengine4.dll")]
		internal static extern int MgdInsertEntityBody(IntPtr pHandler, byte[] buffer, int offset, int count);

		// Token: 0x06005E68 RID: 24168
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdPostCompletion(IntPtr pHandler, [MarshalAs(UnmanagedType.U4)] RequestNotificationStatus notificationStatus);

		// Token: 0x06005E69 RID: 24169
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdReadEntityBody(IntPtr pHandler, byte[] pBuffer, int dwOffset, int dwBytesToRead, bool fAsync, out int pBytesRead, out IntPtr ppAsyncReceiveBuffer);

		// Token: 0x06005E6A RID: 24170
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetCorrelationIdHeader(IntPtr pHandler, out IntPtr correlationId, out ushort correlationIdLength, out bool base64BinaryFormat);

		// Token: 0x06005E6B RID: 24171
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetUserToken(IntPtr pHandler, out IntPtr pToken);

		// Token: 0x06005E6C RID: 24172
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetVirtualToken(IntPtr pHandler, out IntPtr pToken);

		// Token: 0x06005E6D RID: 24173
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern bool MgdIsClientConnected(IntPtr pHandler);

		// Token: 0x06005E6E RID: 24174
		[DllImport("webengine4.dll")]
		internal static extern bool MgdIsHandlerExecutionDenied(IntPtr pHandler);

		// Token: 0x06005E6F RID: 24175
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern void MgdAbortConnection(IntPtr pHandler);

		// Token: 0x06005E70 RID: 24176
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern void MgdCloseConnection(IntPtr pHandler);

		// Token: 0x06005E71 RID: 24177
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetHandlerTypeString(IntPtr pHandler, out IntPtr ppszTypeString, out int pcchTypeString);

		// Token: 0x06005E72 RID: 24178
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetApplicationInfo(IntPtr pHandler, out IntPtr pVirtualPath, out int cchVirtualPath, out IntPtr pPhysPath, out int cchPhysPath);

		// Token: 0x06005E73 RID: 24179
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetUriPath(IntPtr pHandler, out IntPtr ppPath, out int pcchPath, bool fIncludePathInfo, bool fUseParentContext);

		// Token: 0x06005E74 RID: 24180
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetPreloadedContent(IntPtr pHandler, byte[] pBuffer, int lOffset, int cbLen, out int pcbReceived);

		// Token: 0x06005E75 RID: 24181
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetPreloadedSize(IntPtr pHandler, out int pcbAvailable);

		// Token: 0x06005E76 RID: 24182
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetPrincipal(IntPtr pHandler, int dwRequestingAppDomainId, out IntPtr pToken, out IntPtr ppAuthType, ref int pcchAuthType, out IntPtr ppUserName, ref int pcchUserName, out IntPtr pManagedPrincipal);

		// Token: 0x06005E77 RID: 24183
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdIsInRole(IntPtr pHandler, string pszRoleName, out bool pfIsInRole);

		// Token: 0x06005E78 RID: 24184
		[DllImport("webengine4.dll")]
		internal static extern IntPtr MgdAllocateRequestMemory(IntPtr pHandler, int cbSize);

		// Token: 0x06005E79 RID: 24185
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdAppDomainShutdown(IntPtr appContext);

		// Token: 0x06005E7A RID: 24186
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr MgdGetBufferPool(int cbBufferSize);

		// Token: 0x06005E7B RID: 24187
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr MgdGetBuffer(IntPtr pPool);

		// Token: 0x06005E7C RID: 24188
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr MgdReturnBuffer(IntPtr pBuffer);

		// Token: 0x06005E7D RID: 24189
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetLocalPort(IntPtr context);

		// Token: 0x06005E7E RID: 24190
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetRemotePort(IntPtr context);

		// Token: 0x06005E7F RID: 24191
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetUserAgent(IntPtr pRequestContext, out IntPtr pBuffer, out int cbBufferSize);

		// Token: 0x06005E80 RID: 24192
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetCookieHeader(IntPtr pRequestContext, out IntPtr pBuffer, out int cbBufferSize);

		// Token: 0x06005E81 RID: 24193
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdRewriteUrl(IntPtr pRequestContext, string pszUrl, bool fResetQueryString);

		// Token: 0x06005E82 RID: 24194
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetMaxConcurrentRequestsPerCPU();

		// Token: 0x06005E83 RID: 24195
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetMaxConcurrentThreadsPerCPU();

		// Token: 0x06005E84 RID: 24196
		[DllImport("webengine4.dll")]
		internal static extern int MgdSetMaxConcurrentRequestsPerCPU(int value);

		// Token: 0x06005E85 RID: 24197
		[DllImport("webengine4.dll")]
		internal static extern int MgdSetMaxConcurrentThreadsPerCPU(int value);

		// Token: 0x06005E86 RID: 24198
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetCurrentModuleName(IntPtr pHandler, out IntPtr pBuffer, out int cbBufferSize);

		// Token: 0x06005E87 RID: 24199
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetCurrentNotification(IntPtr pRequestContext);

		// Token: 0x06005E88 RID: 24200
		[DllImport("webengine4.dll")]
		internal static extern void MgdDisableNotifications(IntPtr pRequestContext, [MarshalAs(UnmanagedType.U4)] RequestNotification notifications, [MarshalAs(UnmanagedType.U4)] RequestNotification postNotifications);

		// Token: 0x06005E89 RID: 24201
		[DllImport("webengine4.dll")]
		internal static extern void MgdSuppressSendResponseNotifications(IntPtr pRequestContext);

		// Token: 0x06005E8A RID: 24202
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetNextNotification(IntPtr pRequestContext, [MarshalAs(UnmanagedType.U4)] RequestNotificationStatus dwStatus);

		// Token: 0x06005E8B RID: 24203
		[DllImport("webengine4.dll")]
		internal static extern int MgdClearResponse(IntPtr pRequestContext, bool fClearEntity, bool fClearHeaders);

		// Token: 0x06005E8C RID: 24204
		[DllImport("webengine4.dll")]
		internal static extern int MgdCreateNativeConfigSystem(out IntPtr ppConfigSystem);

		// Token: 0x06005E8D RID: 24205
		[DllImport("webengine4.dll")]
		internal static extern int MgdReleaseNativeConfigSystem(IntPtr pConfigSystem);

		// Token: 0x06005E8E RID: 24206
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetRequestTraceGuid(IntPtr pRequestContext, out Guid traceContextId);

		// Token: 0x06005E8F RID: 24207
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetStatusChanges(IntPtr pRequestContext, out ushort statusCode, out ushort subStatusCode, out IntPtr pBuffer, out ushort cbBufferSize);

		// Token: 0x06005E90 RID: 24208
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetResponseChunks(IntPtr pRequestContext, ref int fragmentCount, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int[] fragmentChunkType);

		// Token: 0x06005E91 RID: 24209
		[DllImport("webengine4.dll")]
		internal static extern int MgdEtwGetTraceConfig(IntPtr pRequestContext, out bool providerEnabled, out int flags, out int level);

		// Token: 0x06005E92 RID: 24210
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdEmitSimpleTrace(IntPtr pRequestContext, int type, string eventData);

		// Token: 0x06005E93 RID: 24211
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdEmitWebEventTrace(IntPtr pRequestContext, int webEventType, int fieldCount, string[] fieldNames, int[] fieldTypes, string[] fieldData);

		// Token: 0x06005E94 RID: 24212
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdSetRequestPrincipal(IntPtr pRequestContext, string userName, string authType, IntPtr token);

		// Token: 0x06005E95 RID: 24213
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern bool MgdCanDisposeManagedContext(IntPtr pRequestContext, [MarshalAs(UnmanagedType.U4)] RequestNotificationStatus dwStatus);

		// Token: 0x06005E96 RID: 24214
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern bool MgdIsLastNotification(IntPtr pRequestContext, [MarshalAs(UnmanagedType.U4)] RequestNotificationStatus dwStatus);

		// Token: 0x06005E97 RID: 24215
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern bool MgdIsWithinApp(IntPtr pConfigSystem, string siteName, string appPath, string virtualPath);

		// Token: 0x06005E98 RID: 24216
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetSiteNameFromId(IntPtr pConfigSystem, [MarshalAs(UnmanagedType.U4)] uint siteId, out IntPtr bstrSiteName, out int cchSiteName);

		// Token: 0x06005E99 RID: 24217
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetAppPathForPath(IntPtr pConfigSystem, [MarshalAs(UnmanagedType.U4)] uint siteId, string virtualPath, out IntPtr bstrPath, out int cchPath);

		// Token: 0x06005E9A RID: 24218
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetMemoryLimitKB(out long limit);

		// Token: 0x06005E9B RID: 24219
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetMimeMapCollection(IntPtr pConfigSystem, IntPtr appContext, out IntPtr pMimeMapCollection, out int count);

		// Token: 0x06005E9C RID: 24220
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetModuleCollection(IntPtr pConfigSystem, IntPtr appContext, out IntPtr pModuleCollection, out int count);

		// Token: 0x06005E9D RID: 24221
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetNextMimeMap(IntPtr pMimeMapCollection, uint dwIndex, out IntPtr bstrFileExtension, out int cchFileExtension, out IntPtr bstrMimeType, out int cchMimeType);

		// Token: 0x06005E9E RID: 24222
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetNextModule(IntPtr pModuleCollection, ref uint dwIndex, out IntPtr bstrModuleName, out int cchModuleName, out IntPtr bstrModuleType, out int cchModuleType, out IntPtr bstrModulePrecondition, out int cchModulePrecondition);

		// Token: 0x06005E9F RID: 24223
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetVrPathCreds(IntPtr pConfigSystem, string siteName, string virtualPath, out IntPtr bstrUserName, out int cchUserName, out IntPtr bstrPassword, out int cchPassword);

		// Token: 0x06005EA0 RID: 24224
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetAppCollection(IntPtr pConfigSystem, string siteName, string virtualPath, out IntPtr bstrPath, out int cchPath, out IntPtr pAppCollection, out int count);

		// Token: 0x06005EA1 RID: 24225
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetNextVPath(IntPtr pAppCollection, uint dwIndex, out IntPtr bstrPath, out int cchPath);

		// Token: 0x06005EA2 RID: 24226
		[DllImport("webengine4.dll")]
		internal static extern int MgdInitNativeConfig();

		// Token: 0x06005EA3 RID: 24227
		[DllImport("webengine4.dll")]
		internal static extern void MgdTerminateNativeConfig();

		// Token: 0x06005EA4 RID: 24228
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdMapPathDirect(IntPtr pConfigSystem, string siteName, string virtualPath, out IntPtr bstrPhysicalPath, out int cchPath);

		// Token: 0x06005EA5 RID: 24229
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdMapHandler(IntPtr pHandler, string method, string virtualPath, out IntPtr ppszTypeString, out int pcchTypeString, bool convertNativeStaticFileModule, bool ignoreWildcardMappings);

		// Token: 0x06005EA6 RID: 24230
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdReMapHandler(IntPtr pHandler, string pszVirtualPath, out IntPtr ppszTypeString, out int pcchTypeString, out bool pfHandlerExists);

		// Token: 0x06005EA7 RID: 24231
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdSetRemapHandler(IntPtr pHandler, string pszName, string ppszType);

		// Token: 0x06005EA8 RID: 24232
		[DllImport("webengine4.dll")]
		internal static extern int MgdSetScriptMapForRemapHandler(IntPtr pHandler);

		// Token: 0x06005EA9 RID: 24233
		[DllImport("webengine4.dll")]
		internal static extern int MgdSetNativeConfiguration(IntPtr nativeConfig);

		// Token: 0x06005EAA RID: 24234
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.U4)]
		internal static extern uint MgdResolveSiteName(IntPtr pConfigSystem, string siteName);

		// Token: 0x06005EAB RID: 24235
		[DllImport("webengine4.dll")]
		internal static extern void MgdSetResponseFilter(IntPtr context);

		// Token: 0x06005EAC RID: 24236
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetFileChunkInfo(IntPtr context, int chunkOffset, out long offset, out long length);

		// Token: 0x06005EAD RID: 24237
		[DllImport("webengine4.dll")]
		internal static extern int MgdReadChunkHandle(IntPtr context, IntPtr FileHandle, long startOffset, ref int length, IntPtr chunkEntity);

		// Token: 0x06005EAE RID: 24238
		[DllImport("webengine4.dll")]
		internal static extern int MgdExplicitFlush(IntPtr context, bool async, out bool completedSynchronously);

		// Token: 0x06005EAF RID: 24239
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdSetServerVariableW(IntPtr context, string variableName, string variableValue);

		// Token: 0x06005EB0 RID: 24240
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdExecuteUrl(IntPtr context, string url, bool resetQuerystring, bool preserveForm, byte[] entityBody, uint entityBodySize, string method, int numHeaders, string[] headersNames, string[] headersValues, bool preserveUser);

		// Token: 0x06005EB1 RID: 24241
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetClientCertificate(IntPtr pHandler, out IntPtr ppbClientCert, out int pcbClientCert, out IntPtr ppbClientCertIssuer, out int pcbClientCertIssuer, out IntPtr ppbClientCertPublicKey, out int pcbClientCertPublicKey, out uint pdwCertEncodingType, out long ftNotBefore, out long ftNotAfter);

		// Token: 0x06005EB2 RID: 24242
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetChannelBindingToken(IntPtr pHandler, out IntPtr ppbToken, out int pcbTokenSize);

		// Token: 0x06005EB3 RID: 24243
		[DllImport("webengine4.dll")]
		internal static extern void MgdGetCurrentNotificationInfo(IntPtr pHandler, out int currentModuleIndex, out bool isPostNotification, out int currentNotification);

		// Token: 0x06005EB4 RID: 24244
		[DllImport("webengine4.dll")]
		internal static extern int MgdAcceptWebSocket(IntPtr pHandler);

		// Token: 0x06005EB5 RID: 24245
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetWebSocketContext(IntPtr pHandler, out IntPtr ppWebSocketContext);

		// Token: 0x06005EB6 RID: 24246
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int MgdGetAnonymousUserToken(IntPtr pHandler, out IntPtr pToken);

		// Token: 0x06005EB7 RID: 24247
		[DllImport("webengine4.dll")]
		internal static extern void MgdGetIISVersionInformation(out uint pdwVersion, out bool pfIsIntegratedMode);

		// Token: 0x06005EB8 RID: 24248
		[DllImport("webengine4.dll")]
		internal static extern int MgdConfigureAsyncDisconnectNotification([In] IntPtr pHandler, [In] bool fEnable, out bool pfIsClientConnected);

		// Token: 0x06005EB9 RID: 24249
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetIsChildContext([In] IntPtr pHandler, out bool pfIsChildContext);

		// Token: 0x06005EBA RID: 24250
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetConfigProperty([MarshalAs(UnmanagedType.BStr)] [In] string appConfigMetabasePath, [MarshalAs(UnmanagedType.BStr)] [In] string sectionName, [MarshalAs(UnmanagedType.BStr)] [In] string propertyName, [MarshalAs(UnmanagedType.Struct)] out object value);

		// Token: 0x06005EBB RID: 24251
		[DllImport("webengine4.dll")]
		internal static extern int MgdPushPromise([In] IntPtr context, [MarshalAs(UnmanagedType.LPWStr)] [In] string path, [MarshalAs(UnmanagedType.LPWStr)] [In] string queryString, [MarshalAs(UnmanagedType.LPStr)] [In] string method, [In] int numHeaders, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] [In] string[] headersNames, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] [In] string[] headersValues);

		// Token: 0x06005EBC RID: 24252
		[DllImport("webengine4.dll")]
		internal static extern bool MgdIsAppPoolShuttingDown();

		// Token: 0x06005EBD RID: 24253
		[DllImport("webengine4.dll")]
		internal static extern int MgdGetTlsTokenBindingIdentifiers([In] IntPtr pHandler, [In] [Out] ref IntPtr tokenBindingHandle, out IntPtr providedToken, out uint providedTokenSize, out IntPtr referredToken, out uint referredTokenSize);

		// Token: 0x04003171 RID: 12657
		private const string _IIS_NATIVE_DLL = "webengine4.dll";

		// Token: 0x04003172 RID: 12658
		internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
	}
}
