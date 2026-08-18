using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.Common
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	internal class OracleTimeZone : ISerializable
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0003E3EC File Offset: 0x0003C5EC
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ZoneIdMap", this.m_zoneIdMap);
			info.AddValue("zoneIdToOffsetMap", this.m_zoneIdToOffsetMap);
			info.AddValue("latestTZversion", this.m_latestTimeZoneVersion);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0003E424 File Offset: 0x0003C624
		internal OracleTimeZone()
		{
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0003E434 File Offset: 0x0003C634
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		public OracleTimeZone(SerializationInfo info, StreamingContext context)
		{
			this.m_zoneIdMap = (Dictionary<int, string>)info.GetValue("ZoneIdMap", typeof(Dictionary<int, string>));
			this.m_zoneIdToOffsetMap = (Hashtable)info.GetValue("zoneIdToOffsetMap", typeof(Hashtable));
			this.m_latestTimeZoneVersion = (int)info.GetValue("latestTZversion", typeof(int));
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0003E4B0 File Offset: 0x0003C6B0
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static OracleTimeZone GetInstance()
		{
			try
			{
				if (OracleTimeZone.tzCacheObj == null)
				{
					OracleTimeZone.tzCacheObj = (OracleTimeZone)OracleTimeZone.ReadObj("TimeZone.dst");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return OracleTimeZone.tzCacheObj;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0003E4F4 File Offset: 0x0003C6F4
		internal int GetZoneId(string zoneName)
		{
			IDictionaryEnumerator dictionaryEnumerator = this.m_zoneIdMap.GetEnumerator();
			while (dictionaryEnumerator.MoveNext())
			{
				if ((string)dictionaryEnumerator.Value == zoneName)
				{
					return (int)dictionaryEnumerator.Key;
				}
			}
			return -1;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0003E53C File Offset: 0x0003C73C
		internal string GetZoneName(int id)
		{
			return this.m_zoneIdMap[id];
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0003E54C File Offset: 0x0003C74C
		internal int GetlatestTZversion()
		{
			return this.m_latestTimeZoneVersion;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0003E554 File Offset: 0x0003C754
		internal void GetOffsetOfUTCTime(DateTime? utcTime, int zoneid, out TimeSpan offset, out bool isDST)
		{
			ZoneValue zoneValue = (ZoneValue)this.m_zoneIdToOffsetMap[zoneid];
			Transitions transitions = zoneValue.m_transitions[0];
			if (utcTime != null)
			{
				for (int i = 0; i < zoneValue.m_transitions.Length; i++)
				{
					Transitions transitions2 = zoneValue.m_transitions[i];
					int num = DateTime.Compare(utcTime.Value, transitions2.m_dateTimeInUtc);
					if (num < 0)
					{
						break;
					}
					transitions = transitions2;
					if (num == 0)
					{
						break;
					}
				}
			}
			offset = transitions.m_hourOffset;
			isDST = (transitions.m_dst > 0);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0003E5FC File Offset: 0x0003C7FC
		internal void GetOffsetOfLocalTime(DateTime? localTime, int zoneid, out TimeSpan offset, out bool isDST)
		{
			ZoneValue zoneValue = (ZoneValue)this.m_zoneIdToOffsetMap[zoneid];
			Transitions transitions = zoneValue.m_transitions[0];
			if (localTime != null)
			{
				for (int i = 0; i < zoneValue.m_transitions.Length; i++)
				{
					Transitions transitions2 = zoneValue.m_transitions[i];
					int num = DateTime.Compare(localTime.Value, transitions2.m_dateTimeInLocal);
					if (num < 0)
					{
						break;
					}
					transitions = transitions2;
					if (num == 0)
					{
						break;
					}
				}
			}
			offset = transitions.m_hourOffset;
			isDST = (transitions.m_dst > 0);
			if (isDST && (localTime == transitions.m_dateTimeInLocal || localTime < transitions.m_dateTimeInLocal.Add(transitions.m_dstDuration)))
			{
				throw new OracleException(ResourceStringConstants.ORA_FIELD_NOT_FOUND, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesgWithErrCode(ResourceStringConstants.ORA_FIELD_NOT_FOUND, new string[0]));
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0003E72C File Offset: 0x0003C92C
		internal bool IsValidZoneId(int id)
		{
			return this.m_zoneIdMap.ContainsKey(id);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0003E73C File Offset: 0x0003C93C
		internal bool IsValidZoneName(string zoneName)
		{
			return this.m_zoneIdMap.ContainsValue(zoneName);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0003E74C File Offset: 0x0003C94C
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static object ReadObj(string entryName)
		{
			object result = null;
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Oracle.ManagedDataAccess.src.Client.Resources." + entryName);
			using (GZipStream gzipStream = new GZipStream(manifestResourceStream, CompressionMode.Decompress, true))
			{
				result = new BinaryFormatter
				{
					Binder = new DeserializationBinder()
				}.Deserialize(gzipStream);
			}
			return result;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0003E7B8 File Offset: 0x0003C9B8
		internal void SetZoneIdMap(Dictionary<int, string> zoneIdMap)
		{
			this.m_zoneIdMap = zoneIdMap;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0003E7C4 File Offset: 0x0003C9C4
		internal void SetLatestTZVersion(int latestTZVersion)
		{
			this.m_latestTimeZoneVersion = latestTZVersion;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0003E7D0 File Offset: 0x0003C9D0
		internal void SetZoneIdtoOffsetMap(Hashtable offsetMap)
		{
			this.m_zoneIdToOffsetMap = offsetMap;
		}

		// Token: 0x0400090C RID: 2316
		private const string FILENAME = "TimeZone.dst";

		// Token: 0x0400090D RID: 2317
		internal const int INV_ZONEID = -1;

		// Token: 0x0400090E RID: 2318
		private static OracleTimeZone tzCacheObj;

		// Token: 0x0400090F RID: 2319
		internal int m_latestTimeZoneVersion = 28;

		// Token: 0x04000910 RID: 2320
		internal Dictionary<int, string> m_zoneIdMap;

		// Token: 0x04000911 RID: 2321
		internal Hashtable m_zoneIdToOffsetMap;
	}
}
