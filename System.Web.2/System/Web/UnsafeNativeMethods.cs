using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000106 RID: 262
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06000F7B RID: 3963
		[DllImport("advapi32.dll")]
		internal static extern int SetThreadToken(IntPtr threadref, IntPtr token);

		// Token: 0x06000F7C RID: 3964
		[DllImport("advapi32.dll")]
		internal static extern int RevertToSelf();

		// Token: 0x06000F7D RID: 3965
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern int OpenThreadToken(IntPtr thread, int access, bool openAsSelf, ref IntPtr hToken);

		// Token: 0x06000F7E RID: 3966
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int GetFileSecurity(string filename, int requestedInformation, byte[] securityDescriptor, int length, ref int lengthNeeded);

		// Token: 0x06000F7F RID: 3967
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int LogonUser(string username, string domain, string password, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		// Token: 0x06000F80 RID: 3968
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int ConvertStringSidToSid(string stringSid, out IntPtr pSid);

		// Token: 0x06000F81 RID: 3969
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int LookupAccountSid(string systemName, IntPtr pSid, StringBuilder szName, ref int nameSize, StringBuilder szDomain, ref int domainSize, ref int eUse);

		// Token: 0x06000F82 RID: 3970
		[DllImport("aspnet_state.exe")]
		internal static extern void STWNDCloseConnection(IntPtr tracker);

		// Token: 0x06000F83 RID: 3971
		[DllImport("aspnet_state.exe")]
		internal static extern void STWNDDeleteStateItem(IntPtr stateItem);

		// Token: 0x06000F84 RID: 3972
		[DllImport("aspnet_state.exe")]
		internal static extern void STWNDEndOfRequest(IntPtr tracker);

		// Token: 0x06000F85 RID: 3973
		[DllImport("aspnet_state.exe", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern void STWNDGetLocalAddress(IntPtr tracker, StringBuilder buf);

		// Token: 0x06000F86 RID: 3974
		[DllImport("aspnet_state.exe")]
		internal static extern int STWNDGetLocalPort(IntPtr tracker);

		// Token: 0x06000F87 RID: 3975
		[DllImport("aspnet_state.exe", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern void STWNDGetRemoteAddress(IntPtr tracker, StringBuilder buf);

		// Token: 0x06000F88 RID: 3976
		[DllImport("aspnet_state.exe")]
		internal static extern int STWNDGetRemotePort(IntPtr tracker);

		// Token: 0x06000F89 RID: 3977
		[DllImport("aspnet_state.exe")]
		internal static extern bool STWNDIsClientConnected(IntPtr tracker);

		// Token: 0x06000F8A RID: 3978
		[DllImport("aspnet_state.exe", CharSet = CharSet.Unicode)]
		internal static extern void STWNDSendResponse(IntPtr tracker, StringBuilder status, int statusLength, StringBuilder headers, int headersLength, IntPtr unmanagedState);

		// Token: 0x06000F8B RID: 3979
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int lstrlenW(IntPtr ptr);

		// Token: 0x06000F8C RID: 3980
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		internal static extern int lstrlenA(IntPtr ptr);

		// Token: 0x06000F8D RID: 3981
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool MoveFileEx(string oldFilename, string newFilename, uint flags);

		// Token: 0x06000F8E RID: 3982
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000F8F RID: 3983
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FindClose(IntPtr hndFindFile);

		// Token: 0x06000F90 RID: 3984
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr FindFirstFile(string pFileName, out UnsafeNativeMethods.WIN32_FIND_DATA pFindFileData);

		// Token: 0x06000F91 RID: 3985
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool FindNextFile(IntPtr hndFindFile, out UnsafeNativeMethods.WIN32_FIND_DATA pFindFileData);

		// Token: 0x06000F92 RID: 3986
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool GetFileAttributesEx(string name, int fileInfoLevel, out UnsafeNativeMethods.WIN32_FILE_ATTRIBUTE_DATA data);

		// Token: 0x06000F93 RID: 3987
		[DllImport("kernel32.dll")]
		internal static extern int GetProcessAffinityMask(IntPtr handle, out IntPtr processAffinityMask, out IntPtr systemAffinityMask);

		// Token: 0x06000F94 RID: 3988
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetComputerName(StringBuilder nameBuffer, ref int bufferSize);

		// Token: 0x06000F95 RID: 3989
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetModuleFileName(IntPtr module, StringBuilder filename, int size);

		// Token: 0x06000F96 RID: 3990
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr GetModuleHandle(string moduleName);

		// Token: 0x06000F97 RID: 3991
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern void GetSystemInfo(out UnsafeNativeMethods.SYSTEM_INFO si);

		// Token: 0x06000F98 RID: 3992
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr LoadLibrary(string libFilename);

		// Token: 0x06000F99 RID: 3993
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x06000F9A RID: 3994
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr FindResource(IntPtr hModule, IntPtr lpName, IntPtr lpType);

		// Token: 0x06000F9B RID: 3995
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int SizeofResource(IntPtr hModule, IntPtr hResInfo);

		// Token: 0x06000F9C RID: 3996
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

		// Token: 0x06000F9D RID: 3997
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr LockResource(IntPtr hResData);

		// Token: 0x06000F9E RID: 3998
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr LocalFree(IntPtr pMem);

		// Token: 0x06000F9F RID: 3999
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GlobalMemoryStatusEx(ref UnsafeNativeMethods.MEMORYSTATUSEX memoryStatusEx);

		// Token: 0x06000FA0 RID: 4000
		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetCurrentThread();

		// Token: 0x06000FA1 RID: 4001
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetProcessHeap();

		// Token: 0x06000FA2 RID: 4002
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool HeapFree([In] IntPtr hHeap, [In] uint dwFlags, [In] IntPtr lpMem);

		// Token: 0x06000FA3 RID: 4003
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern void AppDomainRestart(string appId);

		// Token: 0x06000FA4 RID: 4004
		[DllImport("webengine4.dll")]
		internal static extern int AspCompatProcessRequest(AspCompatCallback callback, [MarshalAs(UnmanagedType.Interface)] object context, bool sharedActivity, int activityHash);

		// Token: 0x06000FA5 RID: 4005
		[DllImport("webengine4.dll")]
		internal static extern int AspCompatOnPageStart([MarshalAs(UnmanagedType.Interface)] object obj);

		// Token: 0x06000FA6 RID: 4006
		[DllImport("webengine4.dll")]
		internal static extern int AspCompatOnPageEnd();

		// Token: 0x06000FA7 RID: 4007
		[DllImport("webengine4.dll")]
		internal static extern int AspCompatIsApartmentComponent([MarshalAs(UnmanagedType.Interface)] object obj);

		// Token: 0x06000FA8 RID: 4008
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int AttachDebugger(string clsId, string sessId, IntPtr userToken);

		// Token: 0x06000FA9 RID: 4009
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int ChangeAccessToKeyContainer(string containerName, string accountName, string csp, int options);

		// Token: 0x06000FAA RID: 4010
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int CookieAuthParseTicket(byte[] pData, int iDataLen, StringBuilder szName, int iNameLen, StringBuilder szData, int iUserDataLen, StringBuilder szPath, int iPathLen, byte[] pBytes, long[] pDates);

		// Token: 0x06000FAB RID: 4011
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int CookieAuthConstructTicket(byte[] pData, int iDataLen, string szName, string szData, string szPath, byte[] pBytes, long[] pDates);

		// Token: 0x06000FAC RID: 4012
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr CreateUserToken(string name, string password, int fImpersonationToken, StringBuilder strError, int iErrorSize);

		// Token: 0x06000FAD RID: 4013
		[DllImport("webengine4.dll")]
		internal static extern void GetDirMonConfiguration(out int FCNMode);

		// Token: 0x06000FAE RID: 4014
		[DllImport("webengine4.dll")]
		internal static extern void DirMonClose(HandleRef dirMon, bool fNeedToDispose);

		// Token: 0x06000FAF RID: 4015
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int DirMonOpen(string dir, string appId, bool watchSubtree, uint notifyFilter, int fcnMode, NativeFileChangeNotification callback, out IntPtr pCompletion);

		// Token: 0x06000FB0 RID: 4016
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int GrowFileNotificationBuffer(string appId, bool fWatchSubtree);

		// Token: 0x06000FB1 RID: 4017
		[DllImport("webengine4.dll")]
		internal static extern void EcbFreeExecUrlEntityInfo(IntPtr pEntity);

		// Token: 0x06000FB2 RID: 4018
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetBasics(IntPtr pECB, byte[] buffer, int size, int[] contentInfo);

		// Token: 0x06000FB3 RID: 4019
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetBasicsContentInfo(IntPtr pECB, int[] contentInfo);

		// Token: 0x06000FB4 RID: 4020
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetTraceFlags(IntPtr pECB, int[] contentInfo);

		// Token: 0x06000FB5 RID: 4021
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int EcbEmitSimpleTrace(IntPtr pECB, int type, string eventData);

		// Token: 0x06000FB6 RID: 4022
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int EcbEmitWebEventTrace(IntPtr pECB, int webEventType, int fieldCount, string[] fieldNames, int[] fieldTypes, string[] fieldData);

		// Token: 0x06000FB7 RID: 4023
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetClientCertificate(IntPtr pECB, byte[] buffer, int size, int[] pInts, long[] pDates);

		// Token: 0x06000FB8 RID: 4024
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetExecUrlEntityInfo(int entityLength, byte[] entity, out IntPtr ppEntity);

		// Token: 0x06000FB9 RID: 4025
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetTraceContextId(IntPtr pECB, out Guid traceContextId);

		// Token: 0x06000FBA RID: 4026
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern int EcbGetServerVariable(IntPtr pECB, string name, byte[] buffer, int size);

		// Token: 0x06000FBB RID: 4027
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetServerVariableByIndex(IntPtr pECB, int nameIndex, byte[] buffer, int size);

		// Token: 0x06000FBC RID: 4028
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern int EcbGetQueryString(IntPtr pECB, int encode, StringBuilder buffer, int size);

		// Token: 0x06000FBD RID: 4029
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern int EcbGetUnicodeServerVariable(IntPtr pECB, string name, IntPtr buffer, int size);

		// Token: 0x06000FBE RID: 4030
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetUnicodeServerVariableByIndex(IntPtr pECB, int nameIndex, IntPtr buffer, int size);

		// Token: 0x06000FBF RID: 4031
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetUnicodeServerVariables(IntPtr pECB, IntPtr buffer, int bufferSizeInChars, int[] serverVarLengths, int serverVarCount, int startIndex, ref int requiredSize);

		// Token: 0x06000FC0 RID: 4032
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetVersion(IntPtr pECB);

		// Token: 0x06000FC1 RID: 4033
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetQueryStringRawBytes(IntPtr pECB, byte[] buffer, int size);

		// Token: 0x06000FC2 RID: 4034
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetPreloadedPostedContent(IntPtr pECB, byte[] bytes, int offset, int bufferSize);

		// Token: 0x06000FC3 RID: 4035
		[DllImport("webengine4.dll")]
		internal static extern int EcbGetAdditionalPostedContent(IntPtr pECB, byte[] bytes, int offset, int bufferSize);

		// Token: 0x06000FC4 RID: 4036
		[DllImport("webengine4.dll")]
		internal static extern int EcbReadClientAsync(IntPtr pECB, int dwBytesToRead, AsyncCompletionCallback pfnCallback);

		// Token: 0x06000FC5 RID: 4037
		[DllImport("webengine4.dll")]
		internal static extern int EcbFlushCore(IntPtr pECB, byte[] status, byte[] header, int keepConnected, int totalBodySize, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int doneWithSession, int finalStatus, int kernelCache, int async, ISAPIAsyncCompletionCallback asyncCompletionCallback);

		// Token: 0x06000FC6 RID: 4038
		[DllImport("webengine4.dll")]
		internal static extern int EcbIsClientConnected(IntPtr pECB);

		// Token: 0x06000FC7 RID: 4039
		[DllImport("webengine4.dll")]
		internal static extern int EcbCloseConnection(IntPtr pECB);

		// Token: 0x06000FC8 RID: 4040
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern int EcbMapUrlToPath(IntPtr pECB, string url, byte[] buffer, int size);

		// Token: 0x06000FC9 RID: 4041
		[DllImport("webengine4.dll")]
		internal static extern IntPtr EcbGetImpersonationToken(IntPtr pECB, IntPtr processHandle);

		// Token: 0x06000FCA RID: 4042
		[DllImport("webengine4.dll")]
		internal static extern IntPtr EcbGetVirtualPathToken(IntPtr pECB, IntPtr processHandle);

		// Token: 0x06000FCB RID: 4043
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern int EcbAppendLogParameter(IntPtr pECB, string logParam);

		// Token: 0x06000FCC RID: 4044
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int EcbExecuteUrlUnicode(IntPtr pECB, string url, string method, string childHeaders, bool sendHeaders, bool addUserIndo, IntPtr token, string name, string authType, IntPtr pEntity, ISAPIAsyncCompletionCallback asyncCompletionCallback);

		// Token: 0x06000FCD RID: 4045
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern void InvalidateKernelCache(string key);

		// Token: 0x06000FCE RID: 4046
		[DllImport("webengine4.dll")]
		internal static extern void FreeFileSecurityDescriptor(IntPtr securityDesciptor);

		// Token: 0x06000FCF RID: 4047
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr GetFileHandleForTransmitFile(string strFile);

		// Token: 0x06000FD0 RID: 4048
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr GetFileSecurityDescriptor(string strFile);

		// Token: 0x06000FD1 RID: 4049
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetGroupsForUser(IntPtr token, StringBuilder allGroups, int allGrpSize, StringBuilder error, int errorSize);

		// Token: 0x06000FD2 RID: 4050
		[DllImport("webengine4.dll")]
		internal static extern int GetHMACSHA1Hash(byte[] data1, int dataOffset1, int dataSize1, byte[] data2, int dataSize2, byte[] innerKey, int innerKeySize, byte[] outerKey, int outerKeySize, byte[] hash, int hashSize);

		// Token: 0x06000FD3 RID: 4051
		[DllImport("webengine4.dll")]
		internal static extern int GetPrivateBytesIIS6(out long privatePageCount, bool nocache);

		// Token: 0x06000FD4 RID: 4052
		[DllImport("webengine4.dll")]
		internal static extern int GetProcessMemoryInformation(uint pid, out uint privatePageCount, out uint peakPagefileUsage, bool nocache);

		// Token: 0x06000FD5 RID: 4053
		[DllImport("webengine4.dll")]
		internal static extern int GetSHA1Hash(byte[] data, int dataSize, byte[] hash, int hashSize);

		// Token: 0x06000FD6 RID: 4054
		[DllImport("webengine4.dll")]
		internal static extern int GetW3WPMemoryLimitInKB();

		// Token: 0x06000FD7 RID: 4055
		[DllImport("webengine4.dll")]
		internal static extern void SetClrThreadPoolLimits(int maxWorkerThreads, int maxIoThreads, bool autoConfig);

		// Token: 0x06000FD8 RID: 4056
		[DllImport("webengine4.dll")]
		internal static extern void SetMinRequestsExecutingToDetectDeadlock(int minRequestsExecutingToDetectDeadlock);

		// Token: 0x06000FD9 RID: 4057
		[DllImport("webengine4.dll")]
		internal static extern void InitializeLibrary(bool reduceMaxThreads);

		// Token: 0x06000FDA RID: 4058
		[DllImport("webengine4.dll")]
		internal static extern void PerfCounterInitialize();

		// Token: 0x06000FDB RID: 4059
		[DllImport("webengine4.dll")]
		internal static extern void InitializeHealthMonitor(int deadlockIntervalSeconds, int requestQueueLimit);

		// Token: 0x06000FDC RID: 4060
		[DllImport("webengine4.dll")]
		internal static extern int IsAccessToFileAllowed(IntPtr securityDesciptor, IntPtr iThreadToken, int iAccess);

		// Token: 0x06000FDD RID: 4061
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int IsUserInRole(IntPtr token, string rolename, StringBuilder error, int errorSize);

		// Token: 0x06000FDE RID: 4062
		[DllImport("webengine4.dll")]
		internal static extern void UpdateLastActivityTimeForHealthMonitor();

		// Token: 0x06000FDF RID: 4063
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int GetCredentialFromRegistry(string strRegKey, StringBuilder buffer, int size);

		// Token: 0x06000FE0 RID: 4064
		[DllImport("webengine4.dll", BestFitMapping = false)]
		internal static extern int EcbGetChannelBindingToken(IntPtr pECB, out IntPtr token, out int tokenSize);

		// Token: 0x06000FE1 RID: 4065
		[DllImport("webengine4.dll")]
		internal static extern int EcbCallISAPI(IntPtr pECB, UnsafeNativeMethods.CallISAPIFunc iFunction, byte[] bufferIn, int sizeIn, byte[] bufferOut, int sizeOut);

		// Token: 0x06000FE2 RID: 4066
		[DllImport("webengine4.dll")]
		internal static extern int PassportVersion();

		// Token: 0x06000FE3 RID: 4067
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportCreateHttpRaw(string szRequestLine, string szHeaders, int fSecure, StringBuilder szBufOut, int dwRetBufSize, ref IntPtr passportManager);

		// Token: 0x06000FE4 RID: 4068
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportTicket(IntPtr pManager, string szAttr, out object pReturn);

		// Token: 0x06000FE5 RID: 4069
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportGetCurrentConfig(IntPtr pManager, string szAttr, out object pReturn);

		// Token: 0x06000FE6 RID: 4070
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportLogoutURL(IntPtr pManager, string szReturnURL, string szCOBrandArgs, int iLangID, string strDomain, int iUseSecureAuth, StringBuilder szAuthVal, int iAuthValSize);

		// Token: 0x06000FE7 RID: 4071
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportGetOption(IntPtr pManager, string szOption, out object vOut);

		// Token: 0x06000FE8 RID: 4072
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportSetOption(IntPtr pManager, string szOption, object vOut);

		// Token: 0x06000FE9 RID: 4073
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportGetLoginChallenge(IntPtr pManager, string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, object vExtraParams, StringBuilder szOut, int iOutSize);

		// Token: 0x06000FEA RID: 4074
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportHexPUID(IntPtr pManager, StringBuilder szOut, int iOutSize);

		// Token: 0x06000FEB RID: 4075
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportCreate(string szQueryStrT, string szQueryStrP, string szAuthCookie, string szProfCookie, string szProfCCookie, StringBuilder szAuthCookieRet, StringBuilder szProfCookieRet, int iRetBufSize, ref IntPtr passportManager);

		// Token: 0x06000FEC RID: 4076
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportAuthURL(IntPtr iPassport, string szReturnURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, StringBuilder szAuthVal, int iAuthValSize);

		// Token: 0x06000FED RID: 4077
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportAuthURL2(IntPtr iPassport, string szReturnURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, StringBuilder szAuthVal, int iAuthValSize);

		// Token: 0x06000FEE RID: 4078
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportGetError(IntPtr iPassport);

		// Token: 0x06000FEF RID: 4079
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportDomainFromMemberName(IntPtr iPassport, string szDomain, StringBuilder szMember, int iMemberSize);

		// Token: 0x06000FF0 RID: 4080
		[DllImport("webengine4.dll")]
		internal static extern int PassportGetFromNetworkServer(IntPtr iPassport);

		// Token: 0x06000FF1 RID: 4081
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportGetDomainAttribute(IntPtr iPassport, string szAttributeName, int iLCID, string szDomain, StringBuilder szValue, int iValueSize);

		// Token: 0x06000FF2 RID: 4082
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportHasProfile(IntPtr iPassport, string szProfile);

		// Token: 0x06000FF3 RID: 4083
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportHasFlag(IntPtr iPassport, int iFlagMask);

		// Token: 0x06000FF4 RID: 4084
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportHasConsent(IntPtr iPassport, int iFullConsent, int iNeedBirthdate);

		// Token: 0x06000FF5 RID: 4085
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportGetHasSavedPassword(IntPtr iPassport);

		// Token: 0x06000FF6 RID: 4086
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportHasTicket(IntPtr iPassport);

		// Token: 0x06000FF7 RID: 4087
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportIsAuthenticated(IntPtr iPassport, int iTimeWindow, int fForceLogin, int iUseSecureAuth);

		// Token: 0x06000FF8 RID: 4088
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportLogoTag(IntPtr iPassport, string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, int fSecure, string strNameSpace, int iKPP, int iUseSecureAuth, StringBuilder szValue, int iValueSize);

		// Token: 0x06000FF9 RID: 4089
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportLogoTag2(IntPtr iPassport, string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, int fSecure, string strNameSpace, int iKPP, int iUseSecureAuth, StringBuilder szValue, int iValueSize);

		// Token: 0x06000FFA RID: 4090
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportGetProfile(IntPtr iPassport, string szProfile, out object rOut);

		// Token: 0x06000FFB RID: 4091
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportGetTicketAge(IntPtr iPassport);

		// Token: 0x06000FFC RID: 4092
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportGetTimeSinceSignIn(IntPtr iPassport);

		// Token: 0x06000FFD RID: 4093
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern void PassportDestroy(IntPtr iPassport);

		// Token: 0x06000FFE RID: 4094
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportCrypt(int iFunctionID, string szSrc, StringBuilder szDest, int iDestLength);

		// Token: 0x06000FFF RID: 4095
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int PassportCryptPut(int iFunctionID, string szSrc);

		// Token: 0x06001000 RID: 4096
		[DllImport("webengine4.dll")]
		internal static extern int PassportCryptIsValid();

		// Token: 0x06001001 RID: 4097
		[DllImport("webengine4.dll")]
		internal static extern int PostThreadPoolWorkItem(WorkItemCallback callback);

		// Token: 0x06001002 RID: 4098
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr InstrumentedMutexCreate(string name);

		// Token: 0x06001003 RID: 4099
		[DllImport("webengine4.dll")]
		internal static extern void InstrumentedMutexDelete(HandleRef mutex);

		// Token: 0x06001004 RID: 4100
		[DllImport("webengine4.dll")]
		internal static extern int InstrumentedMutexGetLock(HandleRef mutex, int timeout);

		// Token: 0x06001005 RID: 4101
		[DllImport("webengine4.dll")]
		internal static extern int InstrumentedMutexReleaseLock(HandleRef mutex);

		// Token: 0x06001006 RID: 4102
		[DllImport("webengine4.dll")]
		internal static extern void InstrumentedMutexSetState(HandleRef mutex, int state);

		// Token: 0x06001007 RID: 4103
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int IsapiAppHostMapPath(string appId, string virtualPath, StringBuilder buffer, int size);

		// Token: 0x06001008 RID: 4104
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int IsapiAppHostGetAppPath(string aboPath, StringBuilder buffer, int size);

		// Token: 0x06001009 RID: 4105
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int IsapiAppHostGetUncUser(string appId, StringBuilder usernameBuffer, int usernameSize, StringBuilder passwordBuffer, int passwordSize);

		// Token: 0x0600100A RID: 4106
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int IsapiAppHostGetSiteName(string appId, StringBuilder buffer, int size);

		// Token: 0x0600100B RID: 4107
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int IsapiAppHostGetSiteId(string site, StringBuilder buffer, int size);

		// Token: 0x0600100C RID: 4108
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int IsapiAppHostGetNextVirtualSubdir(string aboPath, bool inApp, ref int index, StringBuilder sb, int size);

		// Token: 0x0600100D RID: 4109
		[DllImport("webengine4.dll", BestFitMapping = false)]
		internal static extern IntPtr BufferPoolGetPool(int bufferSize, int maxFreeListCount);

		// Token: 0x0600100E RID: 4110
		[DllImport("webengine4.dll", BestFitMapping = false)]
		internal static extern IntPtr BufferPoolGetBuffer(IntPtr pool);

		// Token: 0x0600100F RID: 4111
		[DllImport("webengine4.dll", BestFitMapping = false)]
		internal static extern void BufferPoolReleaseBuffer(IntPtr buffer);

		// Token: 0x06001010 RID: 4112
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMGetTraceContextId(IntPtr pMsg, out Guid traceContextId);

		// Token: 0x06001011 RID: 4113
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMGetHistoryTable(int iRows, int[] dwPIDArr, int[] dwReqExecuted, int[] dwReqPending, int[] dwReqExecuting, int[] dwReasonForDeath, int[] dwPeakMemoryUsed, long[] tmCreateTime, long[] tmDeathTime);

		// Token: 0x06001012 RID: 4114
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMGetCurrentProcessInfo(ref int dwReqExecuted, ref int dwReqExecuting, ref int dwPeakMemoryUsed, ref long tmCreateTime, ref int pid);

		// Token: 0x06001013 RID: 4115
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMGetMemoryLimitInMB();

		// Token: 0x06001014 RID: 4116
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMGetBasics(IntPtr pMsg, byte[] buffer, int size, int[] contentInfo);

		// Token: 0x06001015 RID: 4117
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMGetClientCertificate(IntPtr pMsg, byte[] buffer, int size, int[] pInts, long[] pDates);

		// Token: 0x06001016 RID: 4118
		[DllImport("aspnet_wp.exe")]
		internal static extern long PMGetStartTimeStamp(IntPtr pMsg);

		// Token: 0x06001017 RID: 4119
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMGetAllServerVariables(IntPtr pMsg, byte[] buffer, int size);

		// Token: 0x06001018 RID: 4120
		[DllImport("aspnet_wp.exe", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern int PMGetQueryString(IntPtr pMsg, int encode, StringBuilder buffer, int size);

		// Token: 0x06001019 RID: 4121
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMGetQueryStringRawBytes(IntPtr pMsg, byte[] buffer, int size);

		// Token: 0x0600101A RID: 4122
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMGetPreloadedPostedContent(IntPtr pMsg, byte[] bytes, int offset, int bufferSize);

		// Token: 0x0600101B RID: 4123
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMGetAdditionalPostedContent(IntPtr pMsg, byte[] bytes, int offset, int bufferSize);

		// Token: 0x0600101C RID: 4124
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMEmptyResponse(IntPtr pMsg);

		// Token: 0x0600101D RID: 4125
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMIsClientConnected(IntPtr pMsg);

		// Token: 0x0600101E RID: 4126
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMCloseConnection(IntPtr pMsg);

		// Token: 0x0600101F RID: 4127
		[DllImport("aspnet_wp.exe", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern int PMMapUrlToPath(IntPtr pMsg, string url, byte[] buffer, int size);

		// Token: 0x06001020 RID: 4128
		[DllImport("aspnet_wp.exe")]
		internal static extern IntPtr PMGetImpersonationToken(IntPtr pMsg);

		// Token: 0x06001021 RID: 4129
		[DllImport("aspnet_wp.exe")]
		internal static extern IntPtr PMGetVirtualPathToken(IntPtr pMsg);

		// Token: 0x06001022 RID: 4130
		[DllImport("aspnet_wp.exe", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern int PMAppendLogParameter(IntPtr pMsg, string logParam);

		// Token: 0x06001023 RID: 4131
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMFlushCore(IntPtr pMsg, byte[] status, byte[] header, int keepConnected, int totalBodySize, int bodyFragmentsOffset, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int doneWithSession, int finalStatus);

		// Token: 0x06001024 RID: 4132
		[DllImport("aspnet_wp.exe")]
		internal static extern int PMCallISAPI(IntPtr pECB, UnsafeNativeMethods.CallISAPIFunc iFunction, byte[] bufferIn, int sizeIn, byte[] bufferOut, int sizeOut);

		// Token: 0x06001025 RID: 4133
		[DllImport("webengine4.dll")]
		internal static extern IntPtr PerfOpenGlobalCounters();

		// Token: 0x06001026 RID: 4134
		[DllImport("webengine4.dll")]
		internal static extern IntPtr PerfOpenStateCounters();

		// Token: 0x06001027 RID: 4135
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern PerfInstanceDataHandle PerfOpenAppCounters(string AppName);

		// Token: 0x06001028 RID: 4136
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("webengine4.dll")]
		internal static extern void PerfCloseAppCounters(IntPtr pCounters);

		// Token: 0x06001029 RID: 4137
		[DllImport("webengine4.dll")]
		internal static extern void PerfIncrementCounter(IntPtr pCounters, int number);

		// Token: 0x0600102A RID: 4138
		[DllImport("webengine4.dll")]
		internal static extern void PerfDecrementCounter(IntPtr pCounters, int number);

		// Token: 0x0600102B RID: 4139
		[DllImport("webengine4.dll")]
		internal static extern void PerfIncrementCounterEx(IntPtr pCounters, int number, int increment);

		// Token: 0x0600102C RID: 4140
		[DllImport("webengine4.dll")]
		internal static extern void PerfSetCounter(IntPtr pCounters, int number, int increment);

		// Token: 0x0600102D RID: 4141
		[DllImport("webengine4.dll")]
		internal static extern int PerfGetCounter(IntPtr pCounters, int number);

		// Token: 0x0600102E RID: 4142
		[DllImport("webengine4.dll")]
		internal static extern void GetEtwValues(out int level, out int flags);

		// Token: 0x0600102F RID: 4143
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern void TraceRaiseEventMgdHandler(int eventType, IntPtr pRequestContext, string data1, string data2, string data3, string data4);

		// Token: 0x06001030 RID: 4144
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern void TraceRaiseEventWithEcb(int eventType, IntPtr ecb, string data1, string data2, string data3, string data4);

		// Token: 0x06001031 RID: 4145
		[DllImport("aspnet_wp.exe", CharSet = CharSet.Unicode)]
		internal static extern void PMTraceRaiseEvent(int eventType, IntPtr pMsg, string data1, string data2, string data3, string data4);

		// Token: 0x06001032 RID: 4146
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int SessionNDConnectToService(string server);

		// Token: 0x06001033 RID: 4147
		[DllImport("webengine4.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
		internal static extern int SessionNDMakeRequest(HandleRef socket, string server, int port, bool forceIPv6, int networkTimeout, UnsafeNativeMethods.StateProtocolVerb verb, string uri, UnsafeNativeMethods.StateProtocolExclusive exclusive, int extraFlags, int timeout, int lockCookie, byte[] body, int cb, bool checkVersion, out UnsafeNativeMethods.SessionNDMakeRequestResults results);

		// Token: 0x06001034 RID: 4148
		[DllImport("webengine4.dll")]
		internal static extern void SessionNDFreeBody(HandleRef body);

		// Token: 0x06001035 RID: 4149
		[DllImport("webengine4.dll")]
		internal static extern void SessionNDCloseConnection(HandleRef socket);

		// Token: 0x06001036 RID: 4150
		[DllImport("webengine4.dll")]
		internal static extern int TransactManagedCallback(TransactedExecCallback callback, int mode);

		// Token: 0x06001037 RID: 4151
		[DllImport("webengine4.dll", SetLastError = true)]
		internal static extern bool IsValidResource(IntPtr hModule, IntPtr ip, int size);

		// Token: 0x06001038 RID: 4152
		[DllImport("clr.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetCachePath(int dwCacheFlags, StringBuilder pwzCachePath, ref int pcchPath);

		// Token: 0x06001039 RID: 4153
		[DllImport("clr.dll", CharSet = CharSet.Unicode)]
		internal static extern int DeleteShadowCache(string pwzCachePath, string pwzAppName);

		// Token: 0x0600103A RID: 4154
		[DllImport("webengine4.dll")]
		internal static extern int InitializeWmiManager();

		// Token: 0x0600103B RID: 4155
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int DoesKeyContainerExist(string containerName, string provider, int useMachineContainer);

		// Token: 0x0600103C RID: 4156
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int RaiseWmiEvent(ref UnsafeNativeMethods.WmiData pWmiData, bool IsInAspCompatMode);

		// Token: 0x0600103D RID: 4157
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern int RaiseEventlogEvent(int eventType, string[] dataFields, int size);

		// Token: 0x0600103E RID: 4158
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern void LogWebeventProviderFailure(string appUrl, string providerName, string exception);

		// Token: 0x0600103F RID: 4159
		[DllImport("webengine4.dll")]
		internal static extern IntPtr GetEcb(IntPtr pHttpCompletion);

		// Token: 0x06001040 RID: 4160
		[DllImport("webengine4.dll")]
		internal static extern void SetDoneWithSessionCalled(IntPtr pHttpCompletion);

		// Token: 0x06001041 RID: 4161
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern void ReportUnhandledException(string eventInfo);

		// Token: 0x06001042 RID: 4162
		[DllImport("webengine4.dll", CharSet = CharSet.Unicode)]
		internal static extern void RaiseFileMonitoringEventlogEvent(string eventInfo, string path, string appVirtualPath, int hr);

		// Token: 0x06001043 RID: 4163
		[DllImport("webengine4.dll")]
		internal static extern int StartPrefetchActivity(uint ulActivityId);

		// Token: 0x06001044 RID: 4164
		[DllImport("webengine4.dll")]
		internal static extern int EndPrefetchActivity(uint ulActivityId);

		// Token: 0x06001045 RID: 4165
		[DllImport("aspnet_filter.dll")]
		internal static extern IntPtr GetExtensionlessUrlAppendage();

		// Token: 0x06001046 RID: 4166
		[DllImport("ole32.dll", CharSet = CharSet.Unicode)]
		internal static extern int CoCreateInstanceEx(ref Guid clsid, IntPtr pUnkOuter, int dwClsContext, [In] [Out] COSERVERINFO srv, int num, [In] [Out] MULTI_QI[] amqi);

		// Token: 0x06001047 RID: 4167
		[DllImport("ole32.dll", CharSet = CharSet.Unicode)]
		internal static extern int CoCreateInstanceEx(ref Guid clsid, IntPtr pUnkOuter, int dwClsContext, [In] [Out] COSERVERINFO_X64 srv, int num, [In] [Out] MULTI_QI_X64[] amqi);

		// Token: 0x06001048 RID: 4168
		[DllImport("ole32.dll", CharSet = CharSet.Unicode)]
		internal static extern int CoSetProxyBlanket(IntPtr pProxy, RpcAuthent authent, RpcAuthor author, string serverprinc, RpcLevel level, RpcImpers impers, IntPtr ciptr, int dwCapabilities);

		// Token: 0x040005FA RID: 1530
		internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

		// Token: 0x040005FB RID: 1531
		public const int TOKEN_ALL_ACCESS = 983551;

		// Token: 0x040005FC RID: 1532
		public const int TOKEN_EXECUTE = 131072;

		// Token: 0x040005FD RID: 1533
		public const int TOKEN_READ = 131080;

		// Token: 0x040005FE RID: 1534
		public const int TOKEN_IMPERSONATE = 4;

		// Token: 0x040005FF RID: 1535
		public const int ERROR_NO_TOKEN = 1008;

		// Token: 0x04000600 RID: 1536
		public const int OWNER_SECURITY_INFORMATION = 1;

		// Token: 0x04000601 RID: 1537
		public const int GROUP_SECURITY_INFORMATION = 2;

		// Token: 0x04000602 RID: 1538
		public const int DACL_SECURITY_INFORMATION = 4;

		// Token: 0x04000603 RID: 1539
		public const int SACL_SECURITY_INFORMATION = 8;

		// Token: 0x04000604 RID: 1540
		internal const int FILE_ATTRIBUTE_READONLY = 1;

		// Token: 0x04000605 RID: 1541
		internal const int FILE_ATTRIBUTE_HIDDEN = 2;

		// Token: 0x04000606 RID: 1542
		internal const int FILE_ATTRIBUTE_SYSTEM = 4;

		// Token: 0x04000607 RID: 1543
		internal const int FILE_ATTRIBUTE_DIRECTORY = 16;

		// Token: 0x04000608 RID: 1544
		internal const int FILE_ATTRIBUTE_ARCHIVE = 32;

		// Token: 0x04000609 RID: 1545
		internal const int FILE_ATTRIBUTE_DEVICE = 64;

		// Token: 0x0400060A RID: 1546
		internal const int FILE_ATTRIBUTE_NORMAL = 128;

		// Token: 0x0400060B RID: 1547
		internal const int FILE_ATTRIBUTE_TEMPORARY = 256;

		// Token: 0x0400060C RID: 1548
		internal const int FILE_ATTRIBUTE_SPARSE_FILE = 512;

		// Token: 0x0400060D RID: 1549
		internal const int FILE_ATTRIBUTE_REPARSE_POINT = 1024;

		// Token: 0x0400060E RID: 1550
		internal const int FILE_ATTRIBUTE_COMPRESSED = 2048;

		// Token: 0x0400060F RID: 1551
		internal const int FILE_ATTRIBUTE_OFFLINE = 4096;

		// Token: 0x04000610 RID: 1552
		internal const int FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 8192;

		// Token: 0x04000611 RID: 1553
		internal const int FILE_ATTRIBUTE_ENCRYPTED = 16384;

		// Token: 0x04000612 RID: 1554
		internal const int DELETE = 65536;

		// Token: 0x04000613 RID: 1555
		internal const int READ_CONTROL = 131072;

		// Token: 0x04000614 RID: 1556
		internal const int WRITE_DAC = 262144;

		// Token: 0x04000615 RID: 1557
		internal const int WRITE_OWNER = 524288;

		// Token: 0x04000616 RID: 1558
		internal const int SYNCHRONIZE = 1048576;

		// Token: 0x04000617 RID: 1559
		internal const int STANDARD_RIGHTS_REQUIRED = 983040;

		// Token: 0x04000618 RID: 1560
		internal const int STANDARD_RIGHTS_READ = 131072;

		// Token: 0x04000619 RID: 1561
		internal const int STANDARD_RIGHTS_WRITE = 131072;

		// Token: 0x0400061A RID: 1562
		internal const int STANDARD_RIGHTS_EXECUTE = 131072;

		// Token: 0x0400061B RID: 1563
		internal const int GENERIC_READ = -2147483648;

		// Token: 0x0400061C RID: 1564
		internal const int STANDARD_RIGHTS_ALL = 2031616;

		// Token: 0x0400061D RID: 1565
		internal const int SPECIFIC_RIGHTS_ALL = 65535;

		// Token: 0x0400061E RID: 1566
		internal const int FILE_SHARE_READ = 1;

		// Token: 0x0400061F RID: 1567
		internal const int FILE_SHARE_WRITE = 2;

		// Token: 0x04000620 RID: 1568
		internal const int FILE_SHARE_DELETE = 4;

		// Token: 0x04000621 RID: 1569
		internal const int OPEN_EXISTING = 3;

		// Token: 0x04000622 RID: 1570
		internal const int OPEN_ALWAYS = 4;

		// Token: 0x04000623 RID: 1571
		internal const int FILE_FLAG_WRITE_THROUGH = -2147483648;

		// Token: 0x04000624 RID: 1572
		internal const int FILE_FLAG_OVERLAPPED = 1073741824;

		// Token: 0x04000625 RID: 1573
		internal const int FILE_FLAG_NO_BUFFERING = 536870912;

		// Token: 0x04000626 RID: 1574
		internal const int FILE_FLAG_RANDOM_ACCESS = 268435456;

		// Token: 0x04000627 RID: 1575
		internal const int FILE_FLAG_SEQUENTIAL_SCAN = 134217728;

		// Token: 0x04000628 RID: 1576
		internal const int FILE_FLAG_DELETE_ON_CLOSE = 67108864;

		// Token: 0x04000629 RID: 1577
		internal const int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

		// Token: 0x0400062A RID: 1578
		internal const int FILE_FLAG_POSIX_SEMANTICS = 16777216;

		// Token: 0x0400062B RID: 1579
		internal const int GetFileExInfoStandard = 0;

		// Token: 0x0400062C RID: 1580
		internal const uint FILE_NOTIFY_CHANGE_FILE_NAME = 1U;

		// Token: 0x0400062D RID: 1581
		internal const uint FILE_NOTIFY_CHANGE_DIR_NAME = 2U;

		// Token: 0x0400062E RID: 1582
		internal const uint FILE_NOTIFY_CHANGE_ATTRIBUTES = 4U;

		// Token: 0x0400062F RID: 1583
		internal const uint FILE_NOTIFY_CHANGE_SIZE = 8U;

		// Token: 0x04000630 RID: 1584
		internal const uint FILE_NOTIFY_CHANGE_LAST_WRITE = 16U;

		// Token: 0x04000631 RID: 1585
		internal const uint FILE_NOTIFY_CHANGE_LAST_ACCESS = 32U;

		// Token: 0x04000632 RID: 1586
		internal const uint FILE_NOTIFY_CHANGE_CREATION = 64U;

		// Token: 0x04000633 RID: 1587
		internal const uint FILE_NOTIFY_CHANGE_SECURITY = 256U;

		// Token: 0x04000634 RID: 1588
		internal const uint RDCW_FILTER_FILE_AND_DIR_CHANGES = 347U;

		// Token: 0x04000635 RID: 1589
		internal const uint RDCW_FILTER_FILE_CHANGES = 345U;

		// Token: 0x04000636 RID: 1590
		internal const uint RDCW_FILTER_DIR_RENAMES = 2U;

		// Token: 0x04000637 RID: 1591
		public const int RESTRICT_BIN = 1;

		// Token: 0x04000638 RID: 1592
		internal const int StateProtocolFlagUninitialized = 1;

		// Token: 0x020008EC RID: 2284
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WIN32_FIND_DATA
		{
			// Token: 0x0400365B RID: 13915
			internal uint dwFileAttributes;

			// Token: 0x0400365C RID: 13916
			internal uint ftCreationTime_dwLowDateTime;

			// Token: 0x0400365D RID: 13917
			internal uint ftCreationTime_dwHighDateTime;

			// Token: 0x0400365E RID: 13918
			internal uint ftLastAccessTime_dwLowDateTime;

			// Token: 0x0400365F RID: 13919
			internal uint ftLastAccessTime_dwHighDateTime;

			// Token: 0x04003660 RID: 13920
			internal uint ftLastWriteTime_dwLowDateTime;

			// Token: 0x04003661 RID: 13921
			internal uint ftLastWriteTime_dwHighDateTime;

			// Token: 0x04003662 RID: 13922
			internal uint nFileSizeHigh;

			// Token: 0x04003663 RID: 13923
			internal uint nFileSizeLow;

			// Token: 0x04003664 RID: 13924
			internal uint dwReserved0;

			// Token: 0x04003665 RID: 13925
			internal uint dwReserved1;

			// Token: 0x04003666 RID: 13926
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string cFileName;

			// Token: 0x04003667 RID: 13927
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			internal string cAlternateFileName;
		}

		// Token: 0x020008ED RID: 2285
		internal struct WIN32_FILE_ATTRIBUTE_DATA
		{
			// Token: 0x04003668 RID: 13928
			internal int fileAttributes;

			// Token: 0x04003669 RID: 13929
			internal uint ftCreationTimeLow;

			// Token: 0x0400366A RID: 13930
			internal uint ftCreationTimeHigh;

			// Token: 0x0400366B RID: 13931
			internal uint ftLastAccessTimeLow;

			// Token: 0x0400366C RID: 13932
			internal uint ftLastAccessTimeHigh;

			// Token: 0x0400366D RID: 13933
			internal uint ftLastWriteTimeLow;

			// Token: 0x0400366E RID: 13934
			internal uint ftLastWriteTimeHigh;

			// Token: 0x0400366F RID: 13935
			internal uint fileSizeHigh;

			// Token: 0x04003670 RID: 13936
			internal uint fileSizeLow;
		}

		// Token: 0x020008EE RID: 2286
		internal struct WIN32_BY_HANDLE_FILE_INFORMATION
		{
			// Token: 0x04003671 RID: 13937
			internal int fileAttributes;

			// Token: 0x04003672 RID: 13938
			internal uint ftCreationTimeLow;

			// Token: 0x04003673 RID: 13939
			internal uint ftCreationTimeHigh;

			// Token: 0x04003674 RID: 13940
			internal uint ftLastAccessTimeLow;

			// Token: 0x04003675 RID: 13941
			internal uint ftLastAccessTimeHigh;

			// Token: 0x04003676 RID: 13942
			internal uint ftLastWriteTimeLow;

			// Token: 0x04003677 RID: 13943
			internal uint ftLastWriteTimeHigh;

			// Token: 0x04003678 RID: 13944
			internal uint volumeSerialNumber;

			// Token: 0x04003679 RID: 13945
			internal uint fileSizeHigh;

			// Token: 0x0400367A RID: 13946
			internal uint fileSizeLow;

			// Token: 0x0400367B RID: 13947
			internal uint numberOfLinks;

			// Token: 0x0400367C RID: 13948
			internal uint fileIndexHigh;

			// Token: 0x0400367D RID: 13949
			internal uint fileIndexLow;
		}

		// Token: 0x020008EF RID: 2287
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct SYSTEM_INFO
		{
			// Token: 0x0400367E RID: 13950
			public ushort wProcessorArchitecture;

			// Token: 0x0400367F RID: 13951
			public ushort wReserved;

			// Token: 0x04003680 RID: 13952
			public uint dwPageSize;

			// Token: 0x04003681 RID: 13953
			public IntPtr lpMinimumApplicationAddress;

			// Token: 0x04003682 RID: 13954
			public IntPtr lpMaximumApplicationAddress;

			// Token: 0x04003683 RID: 13955
			public IntPtr dwActiveProcessorMask;

			// Token: 0x04003684 RID: 13956
			public uint dwNumberOfProcessors;

			// Token: 0x04003685 RID: 13957
			public uint dwProcessorType;

			// Token: 0x04003686 RID: 13958
			public uint dwAllocationGranularity;

			// Token: 0x04003687 RID: 13959
			public ushort wProcessorLevel;

			// Token: 0x04003688 RID: 13960
			public ushort wProcessorRevision;
		}

		// Token: 0x020008F0 RID: 2288
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct MEMORYSTATUSEX
		{
			// Token: 0x0600687B RID: 26747 RVA: 0x0017400A File Offset: 0x0017220A
			internal void Init()
			{
				this.dwLength = Marshal.SizeOf(typeof(UnsafeNativeMethods.MEMORYSTATUSEX));
			}

			// Token: 0x04003689 RID: 13961
			internal int dwLength;

			// Token: 0x0400368A RID: 13962
			internal int dwMemoryLoad;

			// Token: 0x0400368B RID: 13963
			internal long ullTotalPhys;

			// Token: 0x0400368C RID: 13964
			internal long ullAvailPhys;

			// Token: 0x0400368D RID: 13965
			internal long ullTotalPageFile;

			// Token: 0x0400368E RID: 13966
			internal long ullAvailPageFile;

			// Token: 0x0400368F RID: 13967
			internal long ullTotalVirtual;

			// Token: 0x04003690 RID: 13968
			internal long ullAvailVirtual;

			// Token: 0x04003691 RID: 13969
			internal long ullAvailExtendedVirtual;
		}

		// Token: 0x020008F1 RID: 2289
		internal enum CallISAPIFunc
		{
			// Token: 0x04003693 RID: 13971
			GetSiteServerComment = 1,
			// Token: 0x04003694 RID: 13972
			RestrictIISFolders,
			// Token: 0x04003695 RID: 13973
			CreateTempDir,
			// Token: 0x04003696 RID: 13974
			GetAutogenKeys,
			// Token: 0x04003697 RID: 13975
			GenerateToken
		}

		// Token: 0x020008F2 RID: 2290
		internal struct SessionNDMakeRequestResults
		{
			// Token: 0x04003698 RID: 13976
			internal IntPtr socket;

			// Token: 0x04003699 RID: 13977
			internal int httpStatus;

			// Token: 0x0400369A RID: 13978
			internal int timeout;

			// Token: 0x0400369B RID: 13979
			internal int contentLength;

			// Token: 0x0400369C RID: 13980
			internal IntPtr content;

			// Token: 0x0400369D RID: 13981
			internal int lockCookie;

			// Token: 0x0400369E RID: 13982
			internal long lockDate;

			// Token: 0x0400369F RID: 13983
			internal int lockAge;

			// Token: 0x040036A0 RID: 13984
			internal int stateServerMajVer;

			// Token: 0x040036A1 RID: 13985
			internal int actionFlags;

			// Token: 0x040036A2 RID: 13986
			internal int lastPhase;
		}

		// Token: 0x020008F3 RID: 2291
		internal enum SessionNDMakeRequestPhase
		{
			// Token: 0x040036A4 RID: 13988
			Initialization,
			// Token: 0x040036A5 RID: 13989
			Connecting,
			// Token: 0x040036A6 RID: 13990
			SendingRequest,
			// Token: 0x040036A7 RID: 13991
			ReadingResponse
		}

		// Token: 0x020008F4 RID: 2292
		internal enum StateProtocolVerb
		{
			// Token: 0x040036A9 RID: 13993
			GET = 1,
			// Token: 0x040036AA RID: 13994
			PUT,
			// Token: 0x040036AB RID: 13995
			DELETE,
			// Token: 0x040036AC RID: 13996
			HEAD
		}

		// Token: 0x020008F5 RID: 2293
		internal enum StateProtocolExclusive
		{
			// Token: 0x040036AE RID: 13998
			NONE,
			// Token: 0x040036AF RID: 13999
			ACQUIRE,
			// Token: 0x040036B0 RID: 14000
			RELEASE
		}

		// Token: 0x020008F6 RID: 2294
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WmiData
		{
			// Token: 0x040036B1 RID: 14001
			internal int eventType;

			// Token: 0x040036B2 RID: 14002
			internal int eventCode;

			// Token: 0x040036B3 RID: 14003
			internal int eventDetailCode;

			// Token: 0x040036B4 RID: 14004
			internal string eventTime;

			// Token: 0x040036B5 RID: 14005
			internal string eventMessage;

			// Token: 0x040036B6 RID: 14006
			internal string eventId;

			// Token: 0x040036B7 RID: 14007
			internal string sequenceNumber;

			// Token: 0x040036B8 RID: 14008
			internal string occurrence;

			// Token: 0x040036B9 RID: 14009
			internal int processId;

			// Token: 0x040036BA RID: 14010
			internal string processName;

			// Token: 0x040036BB RID: 14011
			internal string accountName;

			// Token: 0x040036BC RID: 14012
			internal string machineName;

			// Token: 0x040036BD RID: 14013
			internal string appDomain;

			// Token: 0x040036BE RID: 14014
			internal string trustLevel;

			// Token: 0x040036BF RID: 14015
			internal string appVirtualPath;

			// Token: 0x040036C0 RID: 14016
			internal string appPath;

			// Token: 0x040036C1 RID: 14017
			internal string details;

			// Token: 0x040036C2 RID: 14018
			internal string requestUrl;

			// Token: 0x040036C3 RID: 14019
			internal string requestPath;

			// Token: 0x040036C4 RID: 14020
			internal string userHostAddress;

			// Token: 0x040036C5 RID: 14021
			internal string userName;

			// Token: 0x040036C6 RID: 14022
			internal bool userAuthenticated;

			// Token: 0x040036C7 RID: 14023
			internal string userAuthenticationType;

			// Token: 0x040036C8 RID: 14024
			internal string requestThreadAccountName;

			// Token: 0x040036C9 RID: 14025
			internal string processStartTime;

			// Token: 0x040036CA RID: 14026
			internal int threadCount;

			// Token: 0x040036CB RID: 14027
			internal string workingSet;

			// Token: 0x040036CC RID: 14028
			internal string peakWorkingSet;

			// Token: 0x040036CD RID: 14029
			internal string managedHeapSize;

			// Token: 0x040036CE RID: 14030
			internal int appdomainCount;

			// Token: 0x040036CF RID: 14031
			internal int requestsExecuting;

			// Token: 0x040036D0 RID: 14032
			internal int requestsQueued;

			// Token: 0x040036D1 RID: 14033
			internal int requestsRejected;

			// Token: 0x040036D2 RID: 14034
			internal int threadId;

			// Token: 0x040036D3 RID: 14035
			internal string threadAccountName;

			// Token: 0x040036D4 RID: 14036
			internal string stackTrace;

			// Token: 0x040036D5 RID: 14037
			internal bool isImpersonating;

			// Token: 0x040036D6 RID: 14038
			internal string exceptionType;

			// Token: 0x040036D7 RID: 14039
			internal string exceptionMessage;

			// Token: 0x040036D8 RID: 14040
			internal string nameToAuthenticate;

			// Token: 0x040036D9 RID: 14041
			internal string remoteAddress;

			// Token: 0x040036DA RID: 14042
			internal string remotePort;

			// Token: 0x040036DB RID: 14043
			internal string userAgent;

			// Token: 0x040036DC RID: 14044
			internal string persistedState;

			// Token: 0x040036DD RID: 14045
			internal string referer;

			// Token: 0x040036DE RID: 14046
			internal string path;
		}
	}
}
