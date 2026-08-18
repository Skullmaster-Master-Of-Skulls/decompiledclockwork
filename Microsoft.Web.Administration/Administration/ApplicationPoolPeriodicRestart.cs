using System;
using System.Globalization;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200000F RID: 15
	public sealed class ApplicationPoolPeriodicRestart : ConfigurationElement
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004144 File Offset: 0x00003144
		internal ApplicationPoolPeriodicRestart()
		{
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000414C File Offset: 0x0000314C
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004160 File Offset: 0x00003160
		public long Memory
		{
			get
			{
				return (long)base.GetAttributeValue("memory");
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"Memory",
						0U,
						uint.MaxValue
					}));
				}
				base["memory"] = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000041C4 File Offset: 0x000031C4
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000041D8 File Offset: 0x000031D8
		public long PrivateMemory
		{
			get
			{
				return (long)base.GetAttributeValue("privateMemory");
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"PrivateMemory",
						0U,
						uint.MaxValue
					}));
				}
				base["privateMemory"] = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000423C File Offset: 0x0000323C
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004250 File Offset: 0x00003250
		public long Requests
		{
			get
			{
				return (long)base.GetAttributeValue("requests");
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"Requests",
						0U,
						uint.MaxValue
					}));
				}
				base["requests"] = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000042B4 File Offset: 0x000032B4
		public ScheduleCollection Schedule
		{
			get
			{
				if (this._scheduleCollection == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("schedule");
					this._scheduleCollection = new ScheduleCollection();
					this._scheduleCollection.Initialize(base.Configuration, elementByName);
				}
				return this._scheduleCollection;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000042FD File Offset: 0x000032FD
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000430F File Offset: 0x0000330F
		public TimeSpan Time
		{
			get
			{
				return (TimeSpan)base.GetAttributeValue("time");
			}
			set
			{
				base["time"] = value;
			}
		}

		// Token: 0x04000028 RID: 40
		private ScheduleCollection _scheduleCollection;
	}
}
