using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000E1 RID: 225
	internal class RLB
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x0005E1F4 File Offset: 0x0005C3F4
		static RLB()
		{
			RLB.s_dfi.ShortDatePattern = "yyyy-MM-dd";
			RLB.s_dfi.ShortTimePattern = "HH:mm:ss";
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0005E228 File Offset: 0x0005C428
		internal RLB(string message)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				this.m_random = new Random((int)DateTime.Now.Ticks);
				this.m_syncObject = new object();
				this.m_removedInstances = new List<string>();
				if (this.Parse(message))
				{
					this.m_id = (this.m_database + "|" + this.m_service).ToLowerInvariant();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0005E2F8 File Offset: 0x0005C4F8
		internal string GetInstanceName(out int currentIndex)
		{
			int num = this.m_random.Next(10000);
			for (currentIndex = 0; currentIndex < this.m_percentages.Length; currentIndex++)
			{
				if (num <= this.m_percentages[currentIndex])
				{
					return this.m_instances[currentIndex];
				}
			}
			return this.m_instances[this.m_percentages.Length - 1];
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0005E354 File Offset: 0x0005C554
		internal bool Parse(string message)
		{
			this.m_bStatus = true;
			if (message != null && message.Length != 0)
			{
				Regex regex = new Regex(RLB.s_pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
				Match match = regex.Match(message);
				this.m_service = match.Groups["svc"].Value.ToLowerInvariant();
				this.m_database = match.Groups["db"].Value.ToLowerInvariant();
				CaptureCollection captures = match.Groups["inst"].Captures;
				CaptureCollection captures2 = match.Groups["sg"].Captures;
				CaptureCollection captures3 = match.Groups["perc"].Captures;
				CaptureCollection captures4 = match.Groups["flag"].Captures;
				this.m_timestamp = match.Groups["ts"].Value;
				this.m_timeZone = match.Groups["tz"].Value;
				float.TryParse(match.Groups["ver"].Value, out this.m_version);
				if (captures.Count == 0 || captures3.Count == 0 || captures4.Count == 0 || match.Groups["svc"].Captures.Count == 0)
				{
					this.m_bStatus = false;
				}
				else if (captures.Count != captures3.Count || captures3.Count != captures4.Count)
				{
					this.m_bStatus = false;
				}
				else if (this.m_service != null && this.m_service.Length == 0)
				{
					this.m_bStatus = false;
				}
				else if (this.m_timestamp == null || this.m_timestamp == string.Empty)
				{
					this.m_bStatus = false;
				}
				else if ((double)this.m_version <= 0.0)
				{
					this.m_bStatus = false;
				}
				else
				{
					DateTime dateTime = DateTime.Parse(this.m_timestamp, RLB.s_dfi);
					if (this.m_timeZone != string.Empty)
					{
						TimeSpan offset = TimeSpan.Parse(this.m_timeZone);
						DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, offset);
						this.m_dateTime = dateTimeOffset.UtcDateTime;
					}
					else
					{
						this.m_dateTime = dateTime;
					}
				}
				if (this.m_bStatus)
				{
					lock (this.m_syncObject)
					{
						this.m_lastUpdateTime = DateTime.Now;
						this.m_percentages = new int[captures.Count];
						this.m_instances = new string[captures.Count];
						this.m_dispenseCounter = new int[captures.Count];
						this.m_rlbPercentages = new int[captures.Count];
						this.m_removedInstances.Clear();
					}
					for (int i = 0; i < captures.Count; i++)
					{
						this.m_instances[i] = captures[i].Value.ToLowerInvariant();
						this.m_rlbPercentages[i] = Convert.ToInt32(captures3[i].Value);
						double num = Convert.ToDouble(captures3[i].Value);
						if (i == 0)
						{
							this.m_percentages[i] = (int)(num * 100.0);
						}
						if (i > 0)
						{
							this.m_percentages[i] = this.m_percentages[i - 1] + (int)(num * 100.0);
						}
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.RLB, new string[]
							{
								string.Concat(new object[]
								{
									"Database=",
									this.m_database,
									";Service=",
									this.m_service,
									";Instance=",
									this.m_instances[i],
									";Percentage=",
									(int)num,
									";Flag=",
									captures4[i].Value,
									";Timestamp=",
									this.m_dateTime,
									"\n"
								})
							});
						}
					}
				}
			}
			return this.m_bStatus;
		}

		// Token: 0x04000BB8 RID: 3000
		internal static string s_pattern = "\\s*VERSION\\s*=\\s*(?<ver>.*?)\\s*database\\s*=\\s*(?<db>.*?)\\s*service\\s*=\\s*(?<svc>.*?)\\s*{\\s*(\\s*{\\s*instance\\s*=\\s*(?<inst>.*?)\\s+percent\\s*=\\s*(?<perc>.*?)\\s+(percentf\\s*=\\s*(?<percf>.*?)\\s+)*\\s*flag\\s*=\\s*(?<flag>.*?)\\s*}\\s*)*\\s*}\\s*timestamp\\s*=\\s*(?<ts>.*?)\\s*(timezone\\s*=\\s*(?<tz>.*?))*\\s*\\Z";

		// Token: 0x04000BB9 RID: 3001
		public bool m_bStatus;

		// Token: 0x04000BBA RID: 3002
		public float m_version;

		// Token: 0x04000BBB RID: 3003
		public string m_database;

		// Token: 0x04000BBC RID: 3004
		public string m_service;

		// Token: 0x04000BBD RID: 3005
		internal string[] m_instances;

		// Token: 0x04000BBE RID: 3006
		public int[] m_percentages;

		// Token: 0x04000BBF RID: 3007
		public int[] m_rlbPercentages;

		// Token: 0x04000BC0 RID: 3008
		private string m_timestamp;

		// Token: 0x04000BC1 RID: 3009
		private string m_timeZone;

		// Token: 0x04000BC2 RID: 3010
		internal DateTime m_dateTime;

		// Token: 0x04000BC3 RID: 3011
		public int[] m_dispenseCounter;

		// Token: 0x04000BC4 RID: 3012
		internal List<string> m_removedInstances;

		// Token: 0x04000BC5 RID: 3013
		internal DateTime m_lastUpdateTime;

		// Token: 0x04000BC6 RID: 3014
		public string m_id;

		// Token: 0x04000BC7 RID: 3015
		private Random m_random;

		// Token: 0x04000BC8 RID: 3016
		private static DateTimeFormatInfo s_dfi = new DateTimeFormatInfo();

		// Token: 0x04000BC9 RID: 3017
		internal object m_syncObject;
	}
}
