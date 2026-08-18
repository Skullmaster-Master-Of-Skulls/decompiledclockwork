using System;
using System.Globalization;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200000E RID: 14
	public sealed class ApplicationPoolFailure : ConfigurationElement
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00003FB1 File Offset: 0x00002FB1
		internal ApplicationPoolFailure()
		{
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003FB9 File Offset: 0x00002FB9
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003FCB File Offset: 0x00002FCB
		public string AutoShutdownExe
		{
			get
			{
				return (string)base.GetAttributeValue("autoShutdownExe");
			}
			set
			{
				base["autoShutdownExe"] = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003FD9 File Offset: 0x00002FD9
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003FEB File Offset: 0x00002FEB
		public string AutoShutdownParams
		{
			get
			{
				return (string)base.GetAttributeValue("autoShutdownParams");
			}
			set
			{
				base["autoShutdownParams"] = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003FF9 File Offset: 0x00002FF9
		// (set) Token: 0x060000BB RID: 187 RVA: 0x0000400B File Offset: 0x0000300B
		public LoadBalancerCapabilities LoadBalancerCapabilities
		{
			get
			{
				return (LoadBalancerCapabilities)base.GetAttributeValue("loadBalancerCapabilities");
			}
			set
			{
				base["loadBalancerCapabilities"] = (int)value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000401E File Offset: 0x0000301E
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004030 File Offset: 0x00003030
		public string OrphanActionExe
		{
			get
			{
				return (string)base.GetAttributeValue("orphanActionExe");
			}
			set
			{
				base["orphanActionExe"] = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000403E File Offset: 0x0000303E
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004050 File Offset: 0x00003050
		public string OrphanActionParams
		{
			get
			{
				return (string)base.GetAttributeValue("orphanActionParams");
			}
			set
			{
				base["orphanActionParams"] = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000405E File Offset: 0x0000305E
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00004070 File Offset: 0x00003070
		public bool OrphanWorkerProcess
		{
			get
			{
				return (bool)base.GetAttributeValue("orphanWorkerProcess");
			}
			set
			{
				base["orphanWorkerProcess"] = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004083 File Offset: 0x00003083
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00004095 File Offset: 0x00003095
		public bool RapidFailProtection
		{
			get
			{
				return (bool)base.GetAttributeValue("rapidFailProtection");
			}
			set
			{
				base["rapidFailProtection"] = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000040A8 File Offset: 0x000030A8
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000040BA File Offset: 0x000030BA
		public TimeSpan RapidFailProtectionInterval
		{
			get
			{
				return (TimeSpan)base.GetAttributeValue("rapidFailProtectionInterval");
			}
			set
			{
				base["rapidFailProtectionInterval"] = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000040CD File Offset: 0x000030CD
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000040E0 File Offset: 0x000030E0
		public long RapidFailProtectionMaxCrashes
		{
			get
			{
				return (long)base.GetAttributeValue("rapidFailProtectionMaxCrashes");
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"RapidFailProtectionMaxCrashes",
						0U,
						uint.MaxValue
					}));
				}
				base["rapidFailProtectionMaxCrashes"] = value;
			}
		}
	}
}
