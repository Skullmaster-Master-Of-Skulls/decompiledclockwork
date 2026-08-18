using System;

namespace OracleInternal.EntityFramework
{
	// Token: 0x02000083 RID: 131
	internal static class EFProviderSettings
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x0003A4DC File Offset: 0x000386DC
		internal static void InitializeProviderSettings<T>() where T : EFProviderSettings.IEFProviderSettings, new()
		{
			if (EFProviderSettings.Instance == null)
			{
				EFProviderSettings.Instance = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
			}
			EFProviderSettings.s_tracingEnabled = EFProviderSettings.Instance.TracingEnabled;
		}

		// Token: 0x04000791 RID: 1937
		internal const int ODP_NOT_SUPPORTED = -1703;

		// Token: 0x04000792 RID: 1938
		internal const int ODP_INVALID_VALUE = -1202;

		// Token: 0x04000793 RID: 1939
		internal const int EF_NILADIC_FUNCTION = -5000;

		// Token: 0x04000794 RID: 1940
		internal const int EF_READ_ONLY_ENTITY = -5001;

		// Token: 0x04000795 RID: 1941
		internal static EFProviderSettings.IEFProviderSettings Instance;

		// Token: 0x04000796 RID: 1942
		internal static bool s_tracingEnabled;

		// Token: 0x02000084 RID: 132
		internal interface IEFProviderSettings
		{
			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06000683 RID: 1667
			EFProviderSettings.EFOracleProviderType ThickOrThin { get; }

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000684 RID: 1668
			int InitialLONGFetchSize { get; }

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000685 RID: 1669
			int InitialLOBFetchSize { get; }

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000686 RID: 1670
			bool TracingEnabled { get; }

			// Token: 0x06000687 RID: 1671
			void Trace(EFProviderSettings.EFTraceLevel level, string message);

			// Token: 0x06000688 RID: 1672
			int GetMaxPrecision(string typeName);

			// Token: 0x06000689 RID: 1673
			string GetErrorMessage(int errorCode, params string[] args);
		}

		// Token: 0x02000085 RID: 133
		internal enum EFTraceLevel : byte
		{
			// Token: 0x04000798 RID: 1944
			None,
			// Token: 0x04000799 RID: 1945
			Entry,
			// Token: 0x0400079A RID: 1946
			Exit = 1
		}

		// Token: 0x02000086 RID: 134
		internal enum EFOracleProviderType : byte
		{
			// Token: 0x0400079C RID: 1948
			Thick,
			// Token: 0x0400079D RID: 1949
			Thin
		}
	}
}
