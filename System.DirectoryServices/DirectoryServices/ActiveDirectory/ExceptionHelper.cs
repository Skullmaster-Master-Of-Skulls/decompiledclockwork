using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;

namespace System.DirectoryServices.ActiveDirectory
{
	// Token: 0x020000A5 RID: 165
	internal class ExceptionHelper
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x0001E966 File Offset: 0x0001D966
		internal static Exception GetExceptionFromCOMException(COMException e)
		{
			return ExceptionHelper.GetExceptionFromCOMException(null, e);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001E970 File Offset: 0x0001D970
		internal static Exception GetExceptionFromCOMException(DirectoryContext context, COMException e)
		{
			int errorCode = e.ErrorCode;
			string message = e.Message;
			Exception result;
			if (errorCode == -2147024891)
			{
				result = new UnauthorizedAccessException(message, e);
			}
			else if (errorCode == -2147023570)
			{
				result = new AuthenticationException(message, e);
			}
			else if (errorCode == -2147016657)
			{
				result = new InvalidOperationException(message, e);
			}
			else if (errorCode == -2147016651)
			{
				result = new InvalidOperationException(message, e);
			}
			else if (errorCode == -2147019886)
			{
				result = new ActiveDirectoryObjectExistsException(message, e);
			}
			else if (errorCode == -2147024888)
			{
				result = new OutOfMemoryException();
			}
			else if (errorCode == -2147016646 || errorCode == -2147016690 || errorCode == -2147016689)
			{
				if (context != null)
				{
					result = new ActiveDirectoryServerDownException(message, e, errorCode, context.GetServerName());
				}
				else
				{
					result = new ActiveDirectoryServerDownException(message, e, errorCode, null);
				}
			}
			else
			{
				result = new ActiveDirectoryOperationException(message, e, errorCode);
			}
			return result;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001EA3D File Offset: 0x0001DA3D
		internal static Exception GetExceptionFromErrorCode(int errorCode)
		{
			return ExceptionHelper.GetExceptionFromErrorCode(errorCode, null);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001EA48 File Offset: 0x0001DA48
		internal static Exception GetExceptionFromErrorCode(int errorCode, string targetName)
		{
			string errorMessage = ExceptionHelper.GetErrorMessage(errorCode, false);
			if (errorCode == ExceptionHelper.ERROR_ACCESS_DENIED || errorCode == ExceptionHelper.ERROR_DS_DRA_ACCESS_DENIED)
			{
				return new UnauthorizedAccessException(errorMessage);
			}
			if (errorCode == ExceptionHelper.ERROR_NOT_ENOUGH_MEMORY || errorCode == ExceptionHelper.ERROR_OUTOFMEMORY || errorCode == ExceptionHelper.ERROR_DS_DRA_OUT_OF_MEM || errorCode == ExceptionHelper.RPC_S_OUT_OF_RESOURCES)
			{
				return new OutOfMemoryException();
			}
			if (errorCode == ExceptionHelper.ERROR_NO_LOGON_SERVERS || errorCode == ExceptionHelper.ERROR_NO_SUCH_DOMAIN || errorCode == ExceptionHelper.RPC_S_SERVER_UNAVAILABLE || errorCode == ExceptionHelper.RPC_S_CALL_FAILED)
			{
				return new ActiveDirectoryServerDownException(errorMessage, errorCode, targetName);
			}
			return new ActiveDirectoryOperationException(errorMessage, errorCode);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001EACC File Offset: 0x0001DACC
		internal static string GetErrorMessage(int errorCode, bool hresult)
		{
			uint num = (uint)errorCode;
			if (!hresult)
			{
				num = ((num & 65535U) | 458752U | 2147483648U);
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			int num2 = UnsafeNativeMethods.FormatMessageW(12800, 0, (int)num, 0, stringBuilder, stringBuilder.Capacity + 1, 0);
			string result;
			if (num2 != 0)
			{
				result = stringBuilder.ToString(0, num2);
			}
			else
			{
				result = Res.GetString("DSUnknown", new object[]
				{
					Convert.ToString((long)((ulong)num), 16)
				});
			}
			return result;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001EB4C File Offset: 0x0001DB4C
		internal static SyncFromAllServersOperationException CreateSyncAllException(IntPtr errorInfo, bool singleError)
		{
			if (errorInfo == (IntPtr)0)
			{
				return new SyncFromAllServersOperationException();
			}
			if (singleError)
			{
				DS_REPSYNCALL_ERRINFO ds_REPSYNCALL_ERRINFO = new DS_REPSYNCALL_ERRINFO();
				Marshal.PtrToStructure(errorInfo, ds_REPSYNCALL_ERRINFO);
				string errorMessage = ExceptionHelper.GetErrorMessage(ds_REPSYNCALL_ERRINFO.dwWin32Err, false);
				string sourceServer = Marshal.PtrToStringUni(ds_REPSYNCALL_ERRINFO.pszSrcId);
				string targetServer = Marshal.PtrToStringUni(ds_REPSYNCALL_ERRINFO.pszSvrId);
				if (ds_REPSYNCALL_ERRINFO.dwWin32Err == ExceptionHelper.ERROR_CANCELLED)
				{
					return null;
				}
				SyncFromAllServersErrorInformation syncFromAllServersErrorInformation = new SyncFromAllServersErrorInformation(ds_REPSYNCALL_ERRINFO.error, ds_REPSYNCALL_ERRINFO.dwWin32Err, errorMessage, sourceServer, targetServer);
				return new SyncFromAllServersOperationException(Res.GetString("DSSyncAllFailure"), null, new SyncFromAllServersErrorInformation[]
				{
					syncFromAllServersErrorInformation
				});
			}
			else
			{
				IntPtr intPtr = Marshal.ReadIntPtr(errorInfo);
				ArrayList arrayList = new ArrayList();
				int num = 0;
				while (intPtr != (IntPtr)0)
				{
					DS_REPSYNCALL_ERRINFO ds_REPSYNCALL_ERRINFO2 = new DS_REPSYNCALL_ERRINFO();
					Marshal.PtrToStructure(intPtr, ds_REPSYNCALL_ERRINFO2);
					if (ds_REPSYNCALL_ERRINFO2.dwWin32Err != ExceptionHelper.ERROR_CANCELLED)
					{
						string errorMessage2 = ExceptionHelper.GetErrorMessage(ds_REPSYNCALL_ERRINFO2.dwWin32Err, false);
						string sourceServer2 = Marshal.PtrToStringUni(ds_REPSYNCALL_ERRINFO2.pszSrcId);
						string targetServer2 = Marshal.PtrToStringUni(ds_REPSYNCALL_ERRINFO2.pszSvrId);
						SyncFromAllServersErrorInformation value = new SyncFromAllServersErrorInformation(ds_REPSYNCALL_ERRINFO2.error, ds_REPSYNCALL_ERRINFO2.dwWin32Err, errorMessage2, sourceServer2, targetServer2);
						arrayList.Add(value);
					}
					num++;
					intPtr = Marshal.ReadIntPtr(errorInfo, num * Marshal.SizeOf(typeof(IntPtr)));
				}
				if (arrayList.Count == 0)
				{
					return null;
				}
				SyncFromAllServersErrorInformation[] array = new SyncFromAllServersErrorInformation[arrayList.Count];
				for (int i = 0; i < arrayList.Count; i++)
				{
					SyncFromAllServersErrorInformation syncFromAllServersErrorInformation2 = (SyncFromAllServersErrorInformation)arrayList[i];
					array[i] = new SyncFromAllServersErrorInformation(syncFromAllServersErrorInformation2.ErrorCategory, syncFromAllServersErrorInformation2.ErrorCode, syncFromAllServersErrorInformation2.ErrorMessage, syncFromAllServersErrorInformation2.SourceServer, syncFromAllServersErrorInformation2.TargetServer);
				}
				return new SyncFromAllServersOperationException(Res.GetString("DSSyncAllFailure"), null, array);
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001ED20 File Offset: 0x0001DD20
		internal static Exception CreateForestTrustCollisionException(IntPtr collisionInfo)
		{
			ForestTrustRelationshipCollisionCollection forestTrustRelationshipCollisionCollection = new ForestTrustRelationshipCollisionCollection();
			LSA_FOREST_TRUST_COLLISION_INFORMATION lsa_FOREST_TRUST_COLLISION_INFORMATION = new LSA_FOREST_TRUST_COLLISION_INFORMATION();
			Marshal.PtrToStructure(collisionInfo, lsa_FOREST_TRUST_COLLISION_INFORMATION);
			int recordCount = lsa_FOREST_TRUST_COLLISION_INFORMATION.RecordCount;
			IntPtr ptr = (IntPtr)0;
			for (int i = 0; i < recordCount; i++)
			{
				ptr = Marshal.ReadIntPtr(lsa_FOREST_TRUST_COLLISION_INFORMATION.Entries, i * Marshal.SizeOf(typeof(IntPtr)));
				LSA_FOREST_TRUST_COLLISION_RECORD lsa_FOREST_TRUST_COLLISION_RECORD = new LSA_FOREST_TRUST_COLLISION_RECORD();
				Marshal.PtrToStructure(ptr, lsa_FOREST_TRUST_COLLISION_RECORD);
				ForestTrustCollisionType type = lsa_FOREST_TRUST_COLLISION_RECORD.Type;
				string record = Marshal.PtrToStringUni(lsa_FOREST_TRUST_COLLISION_RECORD.Name.Buffer, (int)(lsa_FOREST_TRUST_COLLISION_RECORD.Name.Length / 2));
				TopLevelNameCollisionOptions tlnflag = TopLevelNameCollisionOptions.None;
				DomainCollisionOptions domainFlag = DomainCollisionOptions.None;
				if (type == ForestTrustCollisionType.TopLevelName)
				{
					tlnflag = (TopLevelNameCollisionOptions)lsa_FOREST_TRUST_COLLISION_RECORD.Flags;
				}
				else if (type == ForestTrustCollisionType.Domain)
				{
					domainFlag = (DomainCollisionOptions)lsa_FOREST_TRUST_COLLISION_RECORD.Flags;
				}
				ForestTrustRelationshipCollision collision = new ForestTrustRelationshipCollision(type, tlnflag, domainFlag, record);
				forestTrustRelationshipCollisionCollection.Add(collision);
			}
			return new ForestTrustCollisionException(Res.GetString("ForestTrustCollision"), null, forestTrustRelationshipCollisionCollection);
		}

		// Token: 0x0400043D RID: 1085
		private static int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x0400043E RID: 1086
		private static int ERROR_OUTOFMEMORY = 14;

		// Token: 0x0400043F RID: 1087
		private static int ERROR_DS_DRA_OUT_OF_MEM = 8446;

		// Token: 0x04000440 RID: 1088
		private static int ERROR_NO_SUCH_DOMAIN = 1355;

		// Token: 0x04000441 RID: 1089
		private static int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000442 RID: 1090
		private static int ERROR_NO_LOGON_SERVERS = 1311;

		// Token: 0x04000443 RID: 1091
		private static int ERROR_DS_DRA_ACCESS_DENIED = 8453;

		// Token: 0x04000444 RID: 1092
		private static int RPC_S_OUT_OF_RESOURCES = 1721;

		// Token: 0x04000445 RID: 1093
		internal static int RPC_S_SERVER_UNAVAILABLE = 1722;

		// Token: 0x04000446 RID: 1094
		internal static int RPC_S_CALL_FAILED = 1726;

		// Token: 0x04000447 RID: 1095
		private static int ERROR_CANCELLED = 1223;

		// Token: 0x04000448 RID: 1096
		internal static int ERROR_DS_DRA_BAD_DN = 8439;

		// Token: 0x04000449 RID: 1097
		internal static int ERROR_DS_NAME_UNPARSEABLE = 8350;

		// Token: 0x0400044A RID: 1098
		internal static int ERROR_DS_UNKNOWN_ERROR = 8431;
	}
}
