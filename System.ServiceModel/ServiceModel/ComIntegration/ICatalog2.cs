using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001D8 RID: 472
	[Guid("790C6E0B-9194-4cc9-9426-A48A63185696")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[ComImport]
	internal interface ICatalog2
	{
		// Token: 0x06000F1C RID: 3868
		[DispId(1)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object GetCollection([MarshalAs(UnmanagedType.BStr)] [In] string bstrCollName);

		// Token: 0x06000F1D RID: 3869
		[DispId(2)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object Connect([MarshalAs(UnmanagedType.BStr)] [In] string connectStr);

		// Token: 0x06000F1E RID: 3870
		[DispId(3)]
		int MajorVersion();

		// Token: 0x06000F1F RID: 3871
		[DispId(4)]
		int MinorVersion();

		// Token: 0x06000F20 RID: 3872
		[DispId(5)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object GetCollectionByQuery([MarshalAs(UnmanagedType.BStr)] [In] string collName, [MarshalAs(UnmanagedType.SafeArray)] [In] ref object[] aQuery);

		// Token: 0x06000F21 RID: 3873
		[DispId(6)]
		void ImportComponent([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplIdOrName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrCLSIDOrProgId);

		// Token: 0x06000F22 RID: 3874
		[DispId(7)]
		void InstallComponent([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplIdOrName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrDLL, [MarshalAs(UnmanagedType.BStr)] [In] string bstrTLB, [MarshalAs(UnmanagedType.BStr)] [In] string bstrPSDLL);

		// Token: 0x06000F23 RID: 3875
		[DispId(8)]
		void ShutdownApplication([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplIdOrName);

		// Token: 0x06000F24 RID: 3876
		[DispId(9)]
		void ExportApplication([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplIdOrName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationFile, [In] int lOptions);

		// Token: 0x06000F25 RID: 3877
		[DispId(10)]
		void InstallApplication([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationFile, [MarshalAs(UnmanagedType.BStr)] [In] string bstrDestinationDirectory, [In] int lOptions, [MarshalAs(UnmanagedType.BStr)] [In] string bstrUserId, [MarshalAs(UnmanagedType.BStr)] [In] string bstrPassword, [MarshalAs(UnmanagedType.BStr)] [In] string bstrRSN);

		// Token: 0x06000F26 RID: 3878
		[DispId(11)]
		void StopRouter();

		// Token: 0x06000F27 RID: 3879
		[DispId(12)]
		void RefreshRouter();

		// Token: 0x06000F28 RID: 3880
		[DispId(13)]
		void StartRouter();

		// Token: 0x06000F29 RID: 3881
		[DispId(14)]
		void Reserved1();

		// Token: 0x06000F2A RID: 3882
		[DispId(15)]
		void Reserved2();

		// Token: 0x06000F2B RID: 3883
		[DispId(16)]
		void InstallMultipleComponents([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplIdOrName, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] [In] ref object[] fileNames, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] [In] ref object[] CLSIDS);

		// Token: 0x06000F2C RID: 3884
		[DispId(17)]
		void GetMultipleComponentsInfo([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplIdOrName, [In] object varFileNames, [MarshalAs(UnmanagedType.SafeArray)] out object[] varCLSIDS, [MarshalAs(UnmanagedType.SafeArray)] out object[] varClassNames, [MarshalAs(UnmanagedType.SafeArray)] out object[] varFileFlags, [MarshalAs(UnmanagedType.SafeArray)] out object[] varComponentFlags);

		// Token: 0x06000F2D RID: 3885
		[DispId(18)]
		void RefreshComponents();

		// Token: 0x06000F2E RID: 3886
		[DispId(19)]
		void BackupREGDB([MarshalAs(UnmanagedType.BStr)] [In] string bstrBackupFilePath);

		// Token: 0x06000F2F RID: 3887
		[DispId(20)]
		void RestoreREGDB([MarshalAs(UnmanagedType.BStr)] [In] string bstrBackupFilePath);

		// Token: 0x06000F30 RID: 3888
		[DispId(21)]
		void QueryApplicationFile([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationFile, [MarshalAs(UnmanagedType.BStr)] out string bstrApplicationName, [MarshalAs(UnmanagedType.BStr)] out string bstrApplicationDescription, [MarshalAs(UnmanagedType.VariantBool)] out bool bHasUsers, [MarshalAs(UnmanagedType.VariantBool)] out bool bIsProxy, [MarshalAs(UnmanagedType.SafeArray)] out object[] varFileNames);

		// Token: 0x06000F31 RID: 3889
		[DispId(22)]
		void StartApplication([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplIdOrName);

		// Token: 0x06000F32 RID: 3890
		[DispId(23)]
		int ServiceCheck([In] int lService);

		// Token: 0x06000F33 RID: 3891
		[DispId(24)]
		void InstallMultipleEventClasses([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplIdOrName, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] [In] ref object[] fileNames, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] [In] ref object[] CLSIDS);

		// Token: 0x06000F34 RID: 3892
		[DispId(25)]
		void InstallEventClass([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplIdOrName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrDLL, [MarshalAs(UnmanagedType.BStr)] [In] string bstrTLB, [MarshalAs(UnmanagedType.BStr)] [In] string bstrPSDLL);

		// Token: 0x06000F35 RID: 3893
		[DispId(26)]
		void GetEventClassesForIID([In] string bstrIID, [MarshalAs(UnmanagedType.SafeArray)] [In] [Out] ref object[] varCLSIDS, [MarshalAs(UnmanagedType.SafeArray)] [In] [Out] ref object[] varProgIDs, [MarshalAs(UnmanagedType.SafeArray)] [In] [Out] ref object[] varDescriptions);

		// Token: 0x06000F36 RID: 3894
		[DispId(27)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object GetCollectionByQuery2([MarshalAs(UnmanagedType.BStr)] [In] string bstrCollectionName, [MarshalAs(UnmanagedType.LPStruct)] [In] object pVarQueryStrings);

		// Token: 0x06000F37 RID: 3895
		[DispId(28)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetApplicationInstanceIDFromProcessID([MarshalAs(UnmanagedType.I4)] [In] int lProcessID);

		// Token: 0x06000F38 RID: 3896
		[DispId(29)]
		void ShutdownApplicationInstances([MarshalAs(UnmanagedType.LPStruct)] [In] object pVarApplicationInstanceID);

		// Token: 0x06000F39 RID: 3897
		[DispId(30)]
		void PauseApplicationInstances([MarshalAs(UnmanagedType.LPStruct)] [In] object pVarApplicationInstanceID);

		// Token: 0x06000F3A RID: 3898
		[DispId(31)]
		void ResumeApplicationInstances([MarshalAs(UnmanagedType.LPStruct)] [In] object pVarApplicationInstanceID);

		// Token: 0x06000F3B RID: 3899
		[DispId(32)]
		void RecycleApplicationInstances([MarshalAs(UnmanagedType.LPStruct)] [In] object pVarApplicationInstanceID, [MarshalAs(UnmanagedType.I4)] [In] int lReasonCode);

		// Token: 0x06000F3C RID: 3900
		[DispId(33)]
		[return: MarshalAs(UnmanagedType.VariantBool)]
		bool AreApplicationInstancesPaused([MarshalAs(UnmanagedType.LPStruct)] [In] object pVarApplicationInstanceID);

		// Token: 0x06000F3D RID: 3901
		[DispId(34)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string DumpApplicationInstance([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationInstanceID, [MarshalAs(UnmanagedType.BStr)] [In] string bstrDirectory, [MarshalAs(UnmanagedType.I4)] [In] int lMaxImages);

		// Token: 0x06000F3E RID: 3902
		[DispId(35)]
		[return: MarshalAs(UnmanagedType.VariantBool)]
		bool IsApplicationInstanceDumpSupported();

		// Token: 0x06000F3F RID: 3903
		[DispId(36)]
		void CreateServiceForApplication([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationIDOrName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrServiceName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrStartType, [MarshalAs(UnmanagedType.BStr)] [In] string bstrErrorControl, [MarshalAs(UnmanagedType.BStr)] [In] string bstrDependencies, [MarshalAs(UnmanagedType.BStr)] [In] string bstrRunAs, [MarshalAs(UnmanagedType.BStr)] [In] string bstrPassword, [MarshalAs(UnmanagedType.VariantBool)] [In] bool bDesktopOk);

		// Token: 0x06000F40 RID: 3904
		[DispId(37)]
		void DeleteServiceForApplication([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationIDOrName);

		// Token: 0x06000F41 RID: 3905
		[DispId(38)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetPartitionID([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationIDOrName);

		// Token: 0x06000F42 RID: 3906
		[DispId(39)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetPartitionName([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationIDOrName);

		// Token: 0x06000F43 RID: 3907
		[DispId(40)]
		void CurrentPartition([MarshalAs(UnmanagedType.BStr)] [In] string bstrPartitionIDOrName);

		// Token: 0x06000F44 RID: 3908
		[DispId(41)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string CurrentPartitionID();

		// Token: 0x06000F45 RID: 3909
		[DispId(42)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string CurrentPartitionName();

		// Token: 0x06000F46 RID: 3910
		[DispId(43)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GlobalPartitionID();

		// Token: 0x06000F47 RID: 3911
		[DispId(44)]
		void FlushPartitionCache();

		// Token: 0x06000F48 RID: 3912
		[DispId(45)]
		void CopyApplications([MarshalAs(UnmanagedType.BStr)] [In] string bstrSourcePartitionIDOrName, [MarshalAs(UnmanagedType.LPStruct)] [In] object pVarApplicationID, [MarshalAs(UnmanagedType.BStr)] [In] string bstrDestinationPartitionIDOrName);

		// Token: 0x06000F49 RID: 3913
		[DispId(46)]
		void CopyComponents([MarshalAs(UnmanagedType.BStr)] [In] string bstrSourceApplicationIDOrName, [MarshalAs(UnmanagedType.LPStruct)] [In] object pVarCLSIDOrProgID, [MarshalAs(UnmanagedType.BStr)] [In] string bstrDestinationApplicationIDOrName);

		// Token: 0x06000F4A RID: 3914
		[DispId(47)]
		void MoveComponents([MarshalAs(UnmanagedType.BStr)] [In] string bstrSourceApplicationIDOrName, [MarshalAs(UnmanagedType.LPStruct)] [In] object pVarCLSIDOrProgID, [MarshalAs(UnmanagedType.BStr)] [In] string bstrDestinationApplicationIDOrName);

		// Token: 0x06000F4B RID: 3915
		[DispId(48)]
		void AliasComponent([MarshalAs(UnmanagedType.BStr)] [In] string bstrSrcApplicationIDOrName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrCLSIDOrProgID, [MarshalAs(UnmanagedType.BStr)] [In] string bstrDestApplicationIDOrName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrNewProgId, [MarshalAs(UnmanagedType.BStr)] [In] string bstrNewClsid);

		// Token: 0x06000F4C RID: 3916
		[DispId(49)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object IsSafeToDelete([MarshalAs(UnmanagedType.BStr)] [In] string bstrDllName);

		// Token: 0x06000F4D RID: 3917
		[DispId(50)]
		void ImportUnconfiguredComponents([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationIDOrName, [MarshalAs(UnmanagedType.LPStruct)] [In] object pVarCLSIDOrProgID, [MarshalAs(UnmanagedType.LPStruct)] [In] object pVarComponentType);

		// Token: 0x06000F4E RID: 3918
		[DispId(51)]
		void PromoteUnconfiguredComponents([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationIDOrName, [MarshalAs(UnmanagedType.LPStruct)] [In] object pVarCLSIDOrProgID, [MarshalAs(UnmanagedType.LPStruct)] [In] object pVarComponentType);

		// Token: 0x06000F4F RID: 3919
		[DispId(52)]
		void ImportComponents([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationIDOrName, [MarshalAs(UnmanagedType.LPStruct)] [In] object pVarCLSIDOrProgID, [MarshalAs(UnmanagedType.LPStruct)] [In] object pVarComponentType);

		// Token: 0x06000F50 RID: 3920
		[DispId(53)]
		[return: MarshalAs(UnmanagedType.VariantBool)]
		bool Is64BitCatalogServer();

		// Token: 0x06000F51 RID: 3921
		[DispId(54)]
		void ExportPartition([MarshalAs(UnmanagedType.BStr)] [In] string bstrPartitionIDOrName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrPartitionFileName, [MarshalAs(UnmanagedType.I4)] [In] int lOptions);

		// Token: 0x06000F52 RID: 3922
		[DispId(55)]
		void InstallPartition([MarshalAs(UnmanagedType.BStr)] [In] string bstrFileName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrDestDirectory, [MarshalAs(UnmanagedType.I4)] [In] int lOptions, [MarshalAs(UnmanagedType.BStr)] [In] string bstrUserID, [MarshalAs(UnmanagedType.BStr)] [In] string bstrPassword, [MarshalAs(UnmanagedType.BStr)] [In] string bstrRSN);

		// Token: 0x06000F53 RID: 3923
		[DispId(56)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object QueryApplicationFile2([MarshalAs(UnmanagedType.BStr)] [In] string bstrApplicationFile);

		// Token: 0x06000F54 RID: 3924
		[DispId(57)]
		[return: MarshalAs(UnmanagedType.I4)]
		int GetComponentVersionCount([MarshalAs(UnmanagedType.BStr)] [In] string bstrCLSIDOrProgID);
	}
}
