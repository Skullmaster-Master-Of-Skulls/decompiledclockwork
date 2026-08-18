using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000333 RID: 819
	public sealed class HttpListenerTimeoutsElement : ConfigurationElement
	{
		// Token: 0x06001D56 RID: 7510 RVA: 0x0008BA90 File Offset: 0x00089C90
		static HttpListenerTimeoutsElement()
		{
			HttpListenerTimeoutsElement.properties = new ConfigurationPropertyCollection();
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.entityBody);
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.drainEntityBody);
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.requestQueue);
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.idleConnection);
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.headerWait);
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.minSendBytesPerSecond);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0008BB73 File Offset: 0x00089D73
		private static ConfigurationProperty CreateTimeSpanProperty(string name)
		{
			return new ConfigurationProperty(name, typeof(TimeSpan), TimeSpan.Zero, null, new HttpListenerTimeoutsElement.TimeSpanValidator(), ConfigurationPropertyOptions.None);
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0008BB96 File Offset: 0x00089D96
		[ConfigurationProperty("entityBody", DefaultValue = 0, IsRequired = false)]
		public TimeSpan EntityBody
		{
			get
			{
				return (TimeSpan)base[HttpListenerTimeoutsElement.entityBody];
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x0008BBA8 File Offset: 0x00089DA8
		[ConfigurationProperty("drainEntityBody", DefaultValue = 0, IsRequired = false)]
		public TimeSpan DrainEntityBody
		{
			get
			{
				return (TimeSpan)base[HttpListenerTimeoutsElement.drainEntityBody];
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0008BBBA File Offset: 0x00089DBA
		[ConfigurationProperty("requestQueue", DefaultValue = 0, IsRequired = false)]
		public TimeSpan RequestQueue
		{
			get
			{
				return (TimeSpan)base[HttpListenerTimeoutsElement.requestQueue];
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0008BBCC File Offset: 0x00089DCC
		[ConfigurationProperty("idleConnection", DefaultValue = 0, IsRequired = false)]
		public TimeSpan IdleConnection
		{
			get
			{
				return (TimeSpan)base[HttpListenerTimeoutsElement.idleConnection];
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0008BBDE File Offset: 0x00089DDE
		[ConfigurationProperty("headerWait", DefaultValue = 0, IsRequired = false)]
		public TimeSpan HeaderWait
		{
			get
			{
				return (TimeSpan)base[HttpListenerTimeoutsElement.headerWait];
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x0008BBF0 File Offset: 0x00089DF0
		[ConfigurationProperty("minSendBytesPerSecond", DefaultValue = 0L, IsRequired = false)]
		public long MinSendBytesPerSecond
		{
			get
			{
				return (long)base[HttpListenerTimeoutsElement.minSendBytesPerSecond];
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0008BC02 File Offset: 0x00089E02
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpListenerTimeoutsElement.properties;
			}
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0008BC0C File Offset: 0x00089E0C
		internal long[] GetTimeouts()
		{
			return new long[]
			{
				Convert.ToInt64(this.EntityBody.TotalSeconds),
				Convert.ToInt64(this.DrainEntityBody.TotalSeconds),
				Convert.ToInt64(this.RequestQueue.TotalSeconds),
				Convert.ToInt64(this.IdleConnection.TotalSeconds),
				Convert.ToInt64(this.HeaderWait.TotalSeconds),
				this.MinSendBytesPerSecond
			};
		}

		// Token: 0x04001C3F RID: 7231
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04001C40 RID: 7232
		private static readonly ConfigurationProperty entityBody = HttpListenerTimeoutsElement.CreateTimeSpanProperty("entityBody");

		// Token: 0x04001C41 RID: 7233
		private static readonly ConfigurationProperty drainEntityBody = HttpListenerTimeoutsElement.CreateTimeSpanProperty("drainEntityBody");

		// Token: 0x04001C42 RID: 7234
		private static readonly ConfigurationProperty requestQueue = HttpListenerTimeoutsElement.CreateTimeSpanProperty("requestQueue");

		// Token: 0x04001C43 RID: 7235
		private static readonly ConfigurationProperty idleConnection = HttpListenerTimeoutsElement.CreateTimeSpanProperty("idleConnection");

		// Token: 0x04001C44 RID: 7236
		private static readonly ConfigurationProperty headerWait = HttpListenerTimeoutsElement.CreateTimeSpanProperty("headerWait");

		// Token: 0x04001C45 RID: 7237
		private static readonly ConfigurationProperty minSendBytesPerSecond = new ConfigurationProperty("minSendBytesPerSecond", typeof(long), 0L, null, new HttpListenerTimeoutsElement.LongValidator(), ConfigurationPropertyOptions.None);

		// Token: 0x020007C0 RID: 1984
		private class TimeSpanValidator : ConfigurationValidatorBase
		{
			// Token: 0x0600438A RID: 17290 RVA: 0x0011CFA0 File Offset: 0x0011B1A0
			public override bool CanValidate(Type type)
			{
				return type == typeof(TimeSpan);
			}

			// Token: 0x0600438B RID: 17291 RVA: 0x0011CFB4 File Offset: 0x0011B1B4
			public override void Validate(object value)
			{
				TimeSpan timeSpan = (TimeSpan)value;
				long num = Convert.ToInt64(timeSpan.TotalSeconds);
				if (num < 0L || num > 65535L)
				{
					throw new ArgumentOutOfRangeException("value", timeSpan, SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[]
					{
						"0:0:0",
						"18:12:15"
					}));
				}
			}
		}

		// Token: 0x020007C1 RID: 1985
		private class LongValidator : ConfigurationValidatorBase
		{
			// Token: 0x0600438D RID: 17293 RVA: 0x0011D01C File Offset: 0x0011B21C
			public override bool CanValidate(Type type)
			{
				return type == typeof(long);
			}

			// Token: 0x0600438E RID: 17294 RVA: 0x0011D030 File Offset: 0x0011B230
			public override void Validate(object value)
			{
				long num = (long)value;
				if (num < 0L || num > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value", num, SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[]
					{
						0,
						uint.MaxValue
					}));
				}
			}
		}
	}
}
