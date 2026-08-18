using System;
using System.Globalization;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200000C RID: 12
	public sealed class ApplicationPoolCpu : ConfigurationElement
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003B86 File Offset: 0x00002B86
		internal ApplicationPoolCpu()
		{
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003B8E File Offset: 0x00002B8E
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003BA0 File Offset: 0x00002BA0
		public ProcessorAction Action
		{
			get
			{
				return (ProcessorAction)base.GetAttributeValue("action");
			}
			set
			{
				base["action"] = (int)value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003BB3 File Offset: 0x00002BB3
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003BC8 File Offset: 0x00002BC8
		public long Limit
		{
			get
			{
				return (long)base.GetAttributeValue("limit");
			}
			set
			{
				if (value < 0L || value > 100000L)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"Limit",
						0,
						100000
					}));
				}
				base["limit"] = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003C34 File Offset: 0x00002C34
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003C46 File Offset: 0x00002C46
		public TimeSpan ResetInterval
		{
			get
			{
				return (TimeSpan)base.GetAttributeValue("resetInterval");
			}
			set
			{
				base["resetInterval"] = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003C59 File Offset: 0x00002C59
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003C6B File Offset: 0x00002C6B
		public bool SmpAffinitized
		{
			get
			{
				return (bool)base.GetAttributeValue("smpAffinitized");
			}
			set
			{
				base["smpAffinitized"] = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003C7E File Offset: 0x00002C7E
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003C90 File Offset: 0x00002C90
		public long SmpProcessorAffinityMask
		{
			get
			{
				return (long)base.GetAttributeValue("smpProcessorAffinityMask");
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"SmpProcessorAffinityMask",
						0U,
						uint.MaxValue
					}));
				}
				base["smpProcessorAffinityMask"] = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003CF4 File Offset: 0x00002CF4
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003D08 File Offset: 0x00002D08
		public long SmpProcessorAffinityMask2
		{
			get
			{
				return (long)base.GetAttributeValue("smpProcessorAffinityMask2");
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"SmpProcessorAffinityMask2",
						0U,
						uint.MaxValue
					}));
				}
				base["smpProcessorAffinityMask2"] = value;
			}
		}
	}
}
