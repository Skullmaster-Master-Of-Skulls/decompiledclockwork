using System;
using System.Collections;
using System.Threading;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x02000160 RID: 352
	internal class TNSNames : INamingAdapter
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00093988 File Offset: 0x00091B88
		public string ID
		{
			get
			{
				return "TNSNames";
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00093990 File Offset: 0x00091B90
		public Hashtable Map
		{
			get
			{
				Hashtable result;
				try
				{
					TNSNames.myLock.EnterReadLock();
					result = (Hashtable)TNSNames.m_TNSNamesMap.Clone();
				}
				finally
				{
					TNSNames.myLock.ExitReadLock();
				}
				return result;
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000939D8 File Offset: 0x00091BD8
		static TNSNames()
		{
			ConfigBaseClass.m_TNSNamesoraloc = ProviderConfig.NewOraFileLoc(OraFiles.TnsNames);
			ProviderConfig.NewOraFileParams(OraFiles.TnsNames, ConfigBaseClass.m_TNSNamesoraloc, TNSNames.m_TNSNamesMap);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00093A10 File Offset: 0x00091C10
		public string Resolve(string TNSAlias, out ConnectionOption CO, string InstanceName = null)
		{
			string result;
			try
			{
				TNSNames.myLock.EnterReadLock();
				object obj = TNSNames.m_TNSNamesMap[TNSAlias];
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
				TNSNames.myLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00093A64 File Offset: 0x00091C64
		public void Refresh()
		{
			try
			{
				TNSNames.myLock.EnterWriteLock();
				ConfigBaseClass.m_ParseMode = ParseMode.ReParseTnsNames;
				ConfigBaseClass.m_TNSNamesoraloc = ProviderConfig.NewOraFileLoc(OraFiles.TnsNames);
				ProviderConfig.NewOraFileParams(OraFiles.TnsNames, ConfigBaseClass.m_TNSNamesoraloc, TNSNames.m_TNSNamesMap);
			}
			finally
			{
				TNSNames.myLock.ExitWriteLock();
				ConfigBaseClass.m_ParseMode = ParseMode.None;
			}
		}

		// Token: 0x04000F6F RID: 3951
		private static Hashtable m_TNSNamesMap = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000F70 RID: 3952
		private static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
	}
}
