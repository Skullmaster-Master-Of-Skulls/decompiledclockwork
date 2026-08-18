using System;
using System.Collections;
using System.Threading;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x02000162 RID: 354
	internal class DataSources : INamingAdapter
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00093E74 File Offset: 0x00092074
		public string ID
		{
			get
			{
				return "DataSources";
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00093E7C File Offset: 0x0009207C
		public Hashtable Map
		{
			get
			{
				Hashtable result;
				try
				{
					DataSources.myLock.EnterReadLock();
					result = (Hashtable)DataSources._Map.Clone();
				}
				finally
				{
					DataSources.myLock.ExitReadLock();
				}
				return result;
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00093EDC File Offset: 0x000920DC
		public string Resolve(string TNSAlias, out ConnectionOption CO, string InstanceName = null)
		{
			string result;
			try
			{
				DataSources.myLock.EnterReadLock();
				object obj = DataSources._Map[TNSAlias];
				CO = null;
				if (obj != null)
				{
					result = obj.ToString();
				}
				else
				{
					result = null;
				}
			}
			finally
			{
				DataSources.myLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00093F30 File Offset: 0x00092130
		public void Refresh()
		{
			try
			{
				DataSources.myLock.EnterWriteLock();
				ProviderConfig.ReParseDataSourceSection();
			}
			finally
			{
				DataSources.myLock.ExitWriteLock();
			}
		}

		// Token: 0x04000F74 RID: 3956
		private static Hashtable _Map = ProviderConfig.ConfigDataSourcesMap;

		// Token: 0x04000F75 RID: 3957
		private static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
	}
}
