using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Principal;
using System.Threading;
using log4net.Repository;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02000070 RID: 112
	[Serializable]
	public class LoggingEvent : ISerializable
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		public LoggingEvent(Type callerStackBoundaryDeclaringType, ILoggerRepository repository, string loggerName, Level level, object message, Exception exception)
		{
			this.m_callerStackBoundaryDeclaringType = callerStackBoundaryDeclaringType;
			this.m_message = message;
			this.m_repository = repository;
			this.m_thrownException = exception;
			this.m_data.LoggerName = loggerName;
			this.m_data.Level = level;
			this.m_data.TimeStampUtc = DateTime.UtcNow;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000CABD File Offset: 0x0000ACBD
		public LoggingEvent(Type callerStackBoundaryDeclaringType, ILoggerRepository repository, LoggingEventData data, FixFlags fixedData)
		{
			this.m_callerStackBoundaryDeclaringType = callerStackBoundaryDeclaringType;
			this.m_repository = repository;
			this.m_data = data;
			this.m_fixFlags = fixedData;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000CAE9 File Offset: 0x0000ACE9
		public LoggingEvent(Type callerStackBoundaryDeclaringType, ILoggerRepository repository, LoggingEventData data) : this(callerStackBoundaryDeclaringType, repository, data, FixFlags.All)
		{
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000CAF9 File Offset: 0x0000ACF9
		public LoggingEvent(LoggingEventData data) : this(null, null, data)
		{
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000CB04 File Offset: 0x0000AD04
		protected LoggingEvent(SerializationInfo info, StreamingContext context)
		{
			this.m_data.LoggerName = info.GetString("LoggerName");
			this.m_data.Level = (Level)info.GetValue("Level", typeof(Level));
			this.m_data.Message = info.GetString("Message");
			this.m_data.ThreadName = info.GetString("ThreadName");
			this.m_data.TimeStampUtc = info.GetDateTime("TimeStamp").ToUniversalTime();
			this.m_data.LocationInfo = (LocationInfo)info.GetValue("LocationInfo", typeof(LocationInfo));
			this.m_data.UserName = info.GetString("UserName");
			this.m_data.ExceptionString = info.GetString("ExceptionString");
			this.m_data.Properties = (PropertiesDictionary)info.GetValue("Properties", typeof(PropertiesDictionary));
			this.m_data.Domain = info.GetString("Domain");
			this.m_data.Identity = info.GetString("Identity");
			this.m_fixFlags = FixFlags.All;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000CC50 File Offset: 0x0000AE50
		public static DateTime StartTime
		{
			get
			{
				return SystemInfo.ProcessStartTimeUtc.ToLocalTime();
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000CC6A File Offset: 0x0000AE6A
		public static DateTime StartTimeUtc
		{
			get
			{
				return SystemInfo.ProcessStartTimeUtc;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000CC71 File Offset: 0x0000AE71
		public Level Level
		{
			get
			{
				return this.m_data.Level;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public DateTime TimeStamp
		{
			get
			{
				return this.m_data.TimeStampUtc.ToLocalTime();
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		public DateTime TimeStampUtc
		{
			get
			{
				return this.m_data.TimeStampUtc;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000CCAD File Offset: 0x0000AEAD
		public string LoggerName
		{
			get
			{
				return this.m_data.LoggerName;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000CCBA File Offset: 0x0000AEBA
		public LocationInfo LocationInformation
		{
			get
			{
				if (this.m_data.LocationInfo == null && this.m_cacheUpdatable)
				{
					this.m_data.LocationInfo = new LocationInfo(this.m_callerStackBoundaryDeclaringType);
				}
				return this.m_data.LocationInfo;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000CCF2 File Offset: 0x0000AEF2
		public object MessageObject
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000CCFA File Offset: 0x0000AEFA
		public Exception ExceptionObject
		{
			get
			{
				return this.m_thrownException;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000CD02 File Offset: 0x0000AF02
		public ILoggerRepository Repository
		{
			get
			{
				return this.m_repository;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000CD0A File Offset: 0x0000AF0A
		internal void EnsureRepository(ILoggerRepository repository)
		{
			if (repository != null)
			{
				this.m_repository = repository;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000CD18 File Offset: 0x0000AF18
		public string RenderedMessage
		{
			get
			{
				if (this.m_data.Message == null && this.m_cacheUpdatable)
				{
					if (this.m_message == null)
					{
						this.m_data.Message = "";
					}
					else if (this.m_message is string)
					{
						this.m_data.Message = (this.m_message as string);
					}
					else if (this.m_repository != null)
					{
						this.m_data.Message = this.m_repository.RendererMap.FindAndRender(this.m_message);
					}
					else
					{
						this.m_data.Message = this.m_message.ToString();
					}
				}
				return this.m_data.Message;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		public void WriteRenderedMessage(TextWriter writer)
		{
			if (this.m_data.Message != null)
			{
				writer.Write(this.m_data.Message);
				return;
			}
			if (this.m_message != null)
			{
				if (this.m_message is string)
				{
					writer.Write(this.m_message as string);
					return;
				}
				if (this.m_repository != null)
				{
					this.m_repository.RendererMap.FindAndRender(this.m_message, writer);
					return;
				}
				writer.Write(this.m_message.ToString());
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000CE50 File Offset: 0x0000B050
		public string ThreadName
		{
			get
			{
				if (this.m_data.ThreadName == null && this.m_cacheUpdatable)
				{
					this.m_data.ThreadName = Thread.CurrentThread.Name;
					if (this.m_data.ThreadName != null)
					{
						if (this.m_data.ThreadName.Length != 0)
						{
							goto IL_A2;
						}
					}
					try
					{
						this.m_data.ThreadName = SystemInfo.CurrentThreadId.ToString(NumberFormatInfo.InvariantInfo);
					}
					catch (SecurityException)
					{
						LogLog.Debug(LoggingEvent.declaringType, "Security exception while trying to get current thread ID. Error Ignored. Empty thread name.");
						this.m_data.ThreadName = Thread.CurrentThread.GetHashCode().ToString(CultureInfo.InvariantCulture);
					}
				}
				IL_A2:
				return this.m_data.ThreadName;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000CF1C File Offset: 0x0000B11C
		public string UserName
		{
			get
			{
				if (this.m_data.UserName == null && this.m_cacheUpdatable)
				{
					try
					{
						WindowsIdentity current = WindowsIdentity.GetCurrent();
						if (current != null && current.Name != null)
						{
							this.m_data.UserName = current.Name;
						}
						else
						{
							this.m_data.UserName = "";
						}
					}
					catch (SecurityException)
					{
						LogLog.Debug(LoggingEvent.declaringType, "Security exception while trying to get current windows identity. Error Ignored. Empty user name.");
						this.m_data.UserName = "";
					}
				}
				return this.m_data.UserName;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000CFB4 File Offset: 0x0000B1B4
		public string Identity
		{
			get
			{
				if (this.m_data.Identity == null && this.m_cacheUpdatable)
				{
					try
					{
						if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity != null && Thread.CurrentPrincipal.Identity.Name != null)
						{
							this.m_data.Identity = Thread.CurrentPrincipal.Identity.Name;
						}
						else
						{
							this.m_data.Identity = "";
						}
					}
					catch (ObjectDisposedException)
					{
						LogLog.Debug(LoggingEvent.declaringType, "Object disposed exception while trying to get current thread principal. Error Ignored. Empty identity name.");
						this.m_data.Identity = "";
					}
					catch (SecurityException)
					{
						LogLog.Debug(LoggingEvent.declaringType, "Security exception while trying to get current thread principal. Error Ignored. Empty identity name.");
						this.m_data.Identity = "";
					}
				}
				return this.m_data.Identity;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000D09C File Offset: 0x0000B29C
		public string Domain
		{
			get
			{
				if (this.m_data.Domain == null && this.m_cacheUpdatable)
				{
					this.m_data.Domain = SystemInfo.ApplicationFriendlyName;
				}
				return this.m_data.Domain;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000D0CE File Offset: 0x0000B2CE
		public PropertiesDictionary Properties
		{
			get
			{
				if (this.m_data.Properties != null)
				{
					return this.m_data.Properties;
				}
				if (this.m_eventProperties == null)
				{
					this.m_eventProperties = new PropertiesDictionary();
				}
				return this.m_eventProperties;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000D102 File Offset: 0x0000B302
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0000D10A File Offset: 0x0000B30A
		public FixFlags Fix
		{
			get
			{
				return this.m_fixFlags;
			}
			set
			{
				this.FixVolatileData(value);
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000D114 File Offset: 0x0000B314
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("LoggerName", this.m_data.LoggerName);
			info.AddValue("Level", this.m_data.Level);
			info.AddValue("Message", this.m_data.Message);
			info.AddValue("ThreadName", this.m_data.ThreadName);
			info.AddValue("TimeStamp", this.m_data.TimeStamp);
			info.AddValue("LocationInfo", this.m_data.LocationInfo);
			info.AddValue("UserName", this.m_data.UserName);
			info.AddValue("ExceptionString", this.m_data.ExceptionString);
			info.AddValue("Properties", this.m_data.Properties);
			info.AddValue("Domain", this.m_data.Domain);
			info.AddValue("Identity", this.m_data.Identity);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000D213 File Offset: 0x0000B413
		public LoggingEventData GetLoggingEventData()
		{
			return this.GetLoggingEventData(FixFlags.Partial);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000D220 File Offset: 0x0000B420
		public LoggingEventData GetLoggingEventData(FixFlags fixFlags)
		{
			this.Fix = fixFlags;
			return this.m_data;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000D22F File Offset: 0x0000B42F
		[Obsolete("Use GetExceptionString instead")]
		public string GetExceptionStrRep()
		{
			return this.GetExceptionString();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000D238 File Offset: 0x0000B438
		public string GetExceptionString()
		{
			if (this.m_data.ExceptionString == null && this.m_cacheUpdatable)
			{
				if (this.m_thrownException != null)
				{
					if (this.m_repository != null)
					{
						this.m_data.ExceptionString = this.m_repository.RendererMap.FindAndRender(this.m_thrownException);
					}
					else
					{
						this.m_data.ExceptionString = this.m_thrownException.ToString();
					}
				}
				else
				{
					this.m_data.ExceptionString = "";
				}
			}
			return this.m_data.ExceptionString;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		[Obsolete("Use Fix property")]
		public void FixVolatileData()
		{
			this.Fix = FixFlags.All;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000D2CD File Offset: 0x0000B4CD
		[Obsolete("Use Fix property")]
		public void FixVolatileData(bool fastButLoose)
		{
			if (fastButLoose)
			{
				this.Fix = FixFlags.Partial;
				return;
			}
			this.Fix = FixFlags.All;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		protected void FixVolatileData(FixFlags flags)
		{
			this.m_cacheUpdatable = true;
			FixFlags fixFlags = (flags ^ this.m_fixFlags) & flags;
			if (fixFlags > FixFlags.None)
			{
				if ((fixFlags & FixFlags.Message) != FixFlags.None)
				{
					object obj = this.RenderedMessage;
					this.m_fixFlags |= FixFlags.Message;
				}
				if ((fixFlags & FixFlags.ThreadName) != FixFlags.None)
				{
					object obj = this.ThreadName;
					this.m_fixFlags |= FixFlags.ThreadName;
				}
				if ((fixFlags & FixFlags.LocationInfo) != FixFlags.None)
				{
					object obj = this.LocationInformation;
					this.m_fixFlags |= FixFlags.LocationInfo;
				}
				if ((fixFlags & FixFlags.UserName) != FixFlags.None)
				{
					object obj = this.UserName;
					this.m_fixFlags |= FixFlags.UserName;
				}
				if ((fixFlags & FixFlags.Domain) != FixFlags.None)
				{
					object obj = this.Domain;
					this.m_fixFlags |= FixFlags.Domain;
				}
				if ((fixFlags & FixFlags.Identity) != FixFlags.None)
				{
					object obj = this.Identity;
					this.m_fixFlags |= FixFlags.Identity;
				}
				if ((fixFlags & FixFlags.Exception) != FixFlags.None)
				{
					object obj = this.GetExceptionString();
					this.m_fixFlags |= FixFlags.Exception;
				}
				if ((fixFlags & FixFlags.Properties) != FixFlags.None)
				{
					this.CacheProperties();
					this.m_fixFlags |= FixFlags.Properties;
				}
			}
			this.m_cacheUpdatable = false;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000D40C File Offset: 0x0000B60C
		private void CreateCompositeProperties()
		{
			CompositeProperties compositeProperties = new CompositeProperties();
			if (this.m_eventProperties != null)
			{
				compositeProperties.Add(this.m_eventProperties);
			}
			PropertiesDictionary properties = LogicalThreadContext.Properties.GetProperties(false);
			if (properties != null)
			{
				compositeProperties.Add(properties);
			}
			PropertiesDictionary properties2 = ThreadContext.Properties.GetProperties(false);
			if (properties2 != null)
			{
				compositeProperties.Add(properties2);
			}
			PropertiesDictionary propertiesDictionary = new PropertiesDictionary();
			propertiesDictionary["log4net:UserName"] = this.UserName;
			propertiesDictionary["log4net:Identity"] = this.Identity;
			compositeProperties.Add(propertiesDictionary);
			compositeProperties.Add(GlobalContext.Properties.GetReadOnlyProperties());
			this.m_compositeProperties = compositeProperties;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		private void CacheProperties()
		{
			if (this.m_data.Properties == null && this.m_cacheUpdatable)
			{
				if (this.m_compositeProperties == null)
				{
					this.CreateCompositeProperties();
				}
				PropertiesDictionary propertiesDictionary = this.m_compositeProperties.Flatten();
				PropertiesDictionary propertiesDictionary2 = new PropertiesDictionary();
				foreach (object obj in ((IEnumerable)propertiesDictionary))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = dictionaryEntry.Key as string;
					if (text != null)
					{
						object obj2 = dictionaryEntry.Value;
						IFixingRequired fixingRequired = obj2 as IFixingRequired;
						if (fixingRequired != null)
						{
							obj2 = fixingRequired.GetFixedObject();
						}
						if (obj2 != null)
						{
							propertiesDictionary2[text] = obj2;
						}
					}
				}
				this.m_data.Properties = propertiesDictionary2;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000D580 File Offset: 0x0000B780
		public object LookupProperty(string key)
		{
			if (this.m_data.Properties != null)
			{
				return this.m_data.Properties[key];
			}
			if (this.m_compositeProperties == null)
			{
				this.CreateCompositeProperties();
			}
			return this.m_compositeProperties[key];
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000D5BB File Offset: 0x0000B7BB
		public PropertiesDictionary GetProperties()
		{
			if (this.m_data.Properties != null)
			{
				return this.m_data.Properties;
			}
			if (this.m_compositeProperties == null)
			{
				this.CreateCompositeProperties();
			}
			return this.m_compositeProperties.Flatten();
		}

		// Token: 0x040001B5 RID: 437
		public const string HostNameProperty = "log4net:HostName";

		// Token: 0x040001B6 RID: 438
		public const string IdentityProperty = "log4net:Identity";

		// Token: 0x040001B7 RID: 439
		public const string UserNameProperty = "log4net:UserName";

		// Token: 0x040001B8 RID: 440
		private static readonly Type declaringType = typeof(LoggingEvent);

		// Token: 0x040001B9 RID: 441
		private LoggingEventData m_data;

		// Token: 0x040001BA RID: 442
		private CompositeProperties m_compositeProperties;

		// Token: 0x040001BB RID: 443
		private PropertiesDictionary m_eventProperties;

		// Token: 0x040001BC RID: 444
		private readonly Type m_callerStackBoundaryDeclaringType;

		// Token: 0x040001BD RID: 445
		private readonly object m_message;

		// Token: 0x040001BE RID: 446
		private readonly Exception m_thrownException;

		// Token: 0x040001BF RID: 447
		private ILoggerRepository m_repository;

		// Token: 0x040001C0 RID: 448
		private FixFlags m_fixFlags;

		// Token: 0x040001C1 RID: 449
		private bool m_cacheUpdatable = true;
	}
}
