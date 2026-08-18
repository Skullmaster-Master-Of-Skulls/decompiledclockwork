using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000262 RID: 610
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("70b51430-b6ca-11d0-b9b9-00a0c922e750")]
	[ComImport]
	internal interface IMSAdminBase
	{
		// Token: 0x06001705 RID: 5893
		[PreserveSig]
		int AddKey(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path);

		// Token: 0x06001706 RID: 5894
		[PreserveSig]
		int DeleteKey(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path);

		// Token: 0x06001707 RID: 5895
		void DeleteChildKeys(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path);

		// Token: 0x06001708 RID: 5896
		[PreserveSig]
		int EnumKeys(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, StringBuilder Buffer, int EnumKeyIndex);

		// Token: 0x06001709 RID: 5897
		void CopyKey(IntPtr source, [MarshalAs(UnmanagedType.LPWStr)] string SourcePath, IntPtr dest, [MarshalAs(UnmanagedType.LPWStr)] string DestPath, bool OverwriteFlag, bool CopyFlag);

		// Token: 0x0600170A RID: 5898
		void RenameKey(IntPtr key, [MarshalAs(UnmanagedType.LPWStr)] string path, [MarshalAs(UnmanagedType.LPWStr)] string newName);

		// Token: 0x0600170B RID: 5899
		[PreserveSig]
		int SetData(IntPtr key, [MarshalAs(UnmanagedType.LPWStr)] string path, ref MetadataRecord data);

		// Token: 0x0600170C RID: 5900
		[PreserveSig]
		int GetData(IntPtr key, [MarshalAs(UnmanagedType.LPWStr)] string path, ref MetadataRecord data, [In] [Out] ref uint RequiredDataLen);

		// Token: 0x0600170D RID: 5901
		[PreserveSig]
		int DeleteData(IntPtr key, [MarshalAs(UnmanagedType.LPWStr)] string path, uint Identifier, uint DataType);

		// Token: 0x0600170E RID: 5902
		[PreserveSig]
		int EnumData(IntPtr key, [MarshalAs(UnmanagedType.LPWStr)] string path, ref MetadataRecord data, int EnumDataIndex, [In] [Out] ref uint RequiredDataLen);

		// Token: 0x0600170F RID: 5903
		[PreserveSig]
		int GetAllData(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, uint Attributes, uint UserType, uint DataType, [In] [Out] ref uint NumDataEntries, [In] [Out] ref uint DataSetNumber, uint BufferSize, IntPtr buffer, [In] [Out] ref uint RequiredBufferSize);

		// Token: 0x06001710 RID: 5904
		void DeleteAllData(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, uint UserType, uint DataType);

		// Token: 0x06001711 RID: 5905
		[PreserveSig]
		int CopyData(IntPtr sourcehandle, [MarshalAs(UnmanagedType.LPWStr)] string SourcePath, IntPtr desthandle, [MarshalAs(UnmanagedType.LPWStr)] string DestPath, int Attributes, int UserType, int DataType, [MarshalAs(UnmanagedType.Bool)] bool CopyFlag);

		// Token: 0x06001712 RID: 5906
		[PreserveSig]
		void GetDataPaths(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, int Identifier, int DataType, int BufferSize, [MarshalAs(UnmanagedType.LPWStr)] out char[] Buffer, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int RequiredBufferSize);

		// Token: 0x06001713 RID: 5907
		[PreserveSig]
		int OpenKey(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, [MarshalAs(UnmanagedType.U4)] MBKeyAccess AccessRequested, int TimeOut, [In] [Out] ref IntPtr NewHandle);

		// Token: 0x06001714 RID: 5908
		[PreserveSig]
		int CloseKey(IntPtr handle);

		// Token: 0x06001715 RID: 5909
		void ChangePermissions(IntPtr handle, int TimeOut, [MarshalAs(UnmanagedType.U4)] MBKeyAccess AccessRequested);

		// Token: 0x06001716 RID: 5910
		void SaveData();

		// Token: 0x06001717 RID: 5911
		[PreserveSig]
		void GetHandleInfo(IntPtr handle, [In] [Out] ref _METADATA_HANDLE_INFO Info);

		// Token: 0x06001718 RID: 5912
		[PreserveSig]
		void GetSystemChangeNumber([MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint SystemChangeNumber);

		// Token: 0x06001719 RID: 5913
		[PreserveSig]
		void GetDataSetNumber(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, [In] [Out] ref uint DataSetNumber);

		// Token: 0x0600171A RID: 5914
		[PreserveSig]
		void SetLastChangeTime(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, out System.Runtime.InteropServices.ComTypes.FILETIME LastChangeTime, bool LocalTime);

		// Token: 0x0600171B RID: 5915
		[PreserveSig]
		int GetLastChangeTime(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, [In] [Out] ref System.Runtime.InteropServices.ComTypes.FILETIME LastChangeTime, bool LocalTime);

		// Token: 0x0600171C RID: 5916
		[PreserveSig]
		int KeyExchangePhase1();

		// Token: 0x0600171D RID: 5917
		[PreserveSig]
		int KeyExchangePhase2();

		// Token: 0x0600171E RID: 5918
		[PreserveSig]
		int Backup([MarshalAs(UnmanagedType.LPWStr)] string Location, int Version, int Flags);

		// Token: 0x0600171F RID: 5919
		[PreserveSig]
		int Restore([MarshalAs(UnmanagedType.LPWStr)] string Location, int Version, int Flags);

		// Token: 0x06001720 RID: 5920
		[PreserveSig]
		void EnumBackups([MarshalAs(UnmanagedType.LPWStr)] out string Location, [MarshalAs(UnmanagedType.U4)] out uint Version, out System.Runtime.InteropServices.ComTypes.FILETIME BackupTime, uint EnumIndex);

		// Token: 0x06001721 RID: 5921
		[PreserveSig]
		void DeleteBackup([MarshalAs(UnmanagedType.LPWStr)] string Location, int Version);

		// Token: 0x06001722 RID: 5922
		[PreserveSig]
		int UnmarshalInterface([MarshalAs(UnmanagedType.Interface)] out IMSAdminBase interf);

		// Token: 0x06001723 RID: 5923
		[PreserveSig]
		int GetServerGuid();
	}
}
