using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.Versioning;
using System.Text;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006FF RID: 1791
	public sealed class HttpRuntimeSection : ConfigurationSection
	{
		// Token: 0x06005670 RID: 22128 RVA: 0x0012EA90 File Offset: 0x0012CC90
		static HttpRuntimeSection()
		{
			HttpRuntimeSection._properties = new ConfigurationPropertyCollection();
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propAsyncPreloadMode);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propExecutionTimeout);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propMaxRequestLength);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propRequestLengthDiskThreshold);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propUseFullyQualifiedRedirectUrl);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propMinFreeThreads);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propMinLocalRequestFreeThreads);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propAppRequestQueueLimit);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propEnableKernelOutputCache);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propEnableVersionHeader);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propRequireRootedSaveAsPath);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propEnable);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propDefaultRegexMatchTimeout);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propShutdownTimeout);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propDelayNotificationTimeout);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propWaitChangeNotification);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propMaxWaitChangeNotification);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propEnableHeaderChecking);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propSendCacheControlHeader);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propApartmentThreading);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propEncoderType);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propRequestValidationMode);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propRequestValidationType);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propRequestPathInvalidCharacters);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propMaxUrlLength);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propMaxQueryStringLength);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propRelaxedUrlToFileSystemMapping);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propAllowDynamicModuleRegistration);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propFcnMode);
			HttpRuntimeSection._properties.Add(HttpRuntimeSection._propTargetFramework);
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x0012F0E2 File Offset: 0x0012D2E2
		public HttpRuntimeSection()
		{
			this._MaxRequestLengthBytes = -1;
			this._RequestLengthDiskThresholdBytes = -1;
		}

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x06005672 RID: 22130 RVA: 0x0012F0FF File Offset: 0x0012D2FF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpRuntimeSection._properties;
			}
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06005673 RID: 22131 RVA: 0x0012F106 File Offset: 0x0012D306
		// (set) Token: 0x06005674 RID: 22132 RVA: 0x0012F133 File Offset: 0x0012D333
		[ConfigurationProperty("asyncPreloadMode", DefaultValue = AsyncPreloadModeFlags.None)]
		public AsyncPreloadModeFlags AsyncPreloadMode
		{
			get
			{
				if (!this.asyncPreloadModeCached)
				{
					this.asyncPreloadModeCache = (AsyncPreloadModeFlags)base[HttpRuntimeSection._propAsyncPreloadMode];
					this.asyncPreloadModeCached = true;
				}
				return this.asyncPreloadModeCache;
			}
			set
			{
				base[HttpRuntimeSection._propAsyncPreloadMode] = value;
				this.asyncPreloadModeCache = value;
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06005675 RID: 22133 RVA: 0x0012F14D File Offset: 0x0012D34D
		// (set) Token: 0x06005676 RID: 22134 RVA: 0x0012F17A File Offset: 0x0012D37A
		[ConfigurationProperty("fcnMode", DefaultValue = FcnMode.NotSet)]
		public FcnMode FcnMode
		{
			get
			{
				if (!this.fcnModeCached)
				{
					this.fcnModeCache = (FcnMode)base[HttpRuntimeSection._propFcnMode];
					this.fcnModeCached = true;
				}
				return this.fcnModeCache;
			}
			set
			{
				base[HttpRuntimeSection._propFcnMode] = value;
				this.fcnModeCache = value;
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x06005677 RID: 22135 RVA: 0x0012F194 File Offset: 0x0012D394
		// (set) Token: 0x06005678 RID: 22136 RVA: 0x0012F1C1 File Offset: 0x0012D3C1
		[ConfigurationProperty("executionTimeout", DefaultValue = "00:01:50")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan ExecutionTimeout
		{
			get
			{
				if (!this.executionTimeoutCached)
				{
					this.executionTimeoutCache = (TimeSpan)base[HttpRuntimeSection._propExecutionTimeout];
					this.executionTimeoutCached = true;
				}
				return this.executionTimeoutCache;
			}
			set
			{
				base[HttpRuntimeSection._propExecutionTimeout] = value;
				this.executionTimeoutCache = value;
			}
		}

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x06005679 RID: 22137 RVA: 0x0012F1DB File Offset: 0x0012D3DB
		// (set) Token: 0x0600567A RID: 22138 RVA: 0x0012F1F0 File Offset: 0x0012D3F0
		[ConfigurationProperty("maxRequestLength", DefaultValue = 4096)]
		[IntegerValidator(MinValue = 0)]
		public int MaxRequestLength
		{
			get
			{
				return (int)base[HttpRuntimeSection._propMaxRequestLength];
			}
			set
			{
				if (value < this.RequestLengthDiskThreshold)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_max_request_length_smaller_than_max_request_length_disk_threshold"), base.ElementInformation.Properties[HttpRuntimeSection._propMaxRequestLength.Name].Source, base.ElementInformation.Properties[HttpRuntimeSection._propMaxRequestLength.Name].LineNumber);
				}
				base[HttpRuntimeSection._propMaxRequestLength] = value;
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x0600567B RID: 22139 RVA: 0x0012F265 File Offset: 0x0012D465
		// (set) Token: 0x0600567C RID: 22140 RVA: 0x0012F278 File Offset: 0x0012D478
		[ConfigurationProperty("requestLengthDiskThreshold", DefaultValue = 80)]
		[IntegerValidator(MinValue = 1)]
		public int RequestLengthDiskThreshold
		{
			get
			{
				return (int)base[HttpRuntimeSection._propRequestLengthDiskThreshold];
			}
			set
			{
				if (value > this.MaxRequestLength)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_max_request_length_disk_threshold_exceeds_max_request_length"), base.ElementInformation.Properties[HttpRuntimeSection._propRequestLengthDiskThreshold.Name].Source, base.ElementInformation.Properties[HttpRuntimeSection._propRequestLengthDiskThreshold.Name].LineNumber);
				}
				base[HttpRuntimeSection._propRequestLengthDiskThreshold] = value;
			}
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x0600567D RID: 22141 RVA: 0x0012F2ED File Offset: 0x0012D4ED
		// (set) Token: 0x0600567E RID: 22142 RVA: 0x0012F2FF File Offset: 0x0012D4FF
		[ConfigurationProperty("useFullyQualifiedRedirectUrl", DefaultValue = false)]
		public bool UseFullyQualifiedRedirectUrl
		{
			get
			{
				return (bool)base[HttpRuntimeSection._propUseFullyQualifiedRedirectUrl];
			}
			set
			{
				base[HttpRuntimeSection._propUseFullyQualifiedRedirectUrl] = value;
			}
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x0600567F RID: 22143 RVA: 0x0012F312 File Offset: 0x0012D512
		// (set) Token: 0x06005680 RID: 22144 RVA: 0x0012F324 File Offset: 0x0012D524
		[ConfigurationProperty("minFreeThreads", DefaultValue = 8)]
		[IntegerValidator(MinValue = 0)]
		public int MinFreeThreads
		{
			get
			{
				return (int)base[HttpRuntimeSection._propMinFreeThreads];
			}
			set
			{
				base[HttpRuntimeSection._propMinFreeThreads] = value;
			}
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x06005681 RID: 22145 RVA: 0x0012F337 File Offset: 0x0012D537
		// (set) Token: 0x06005682 RID: 22146 RVA: 0x0012F349 File Offset: 0x0012D549
		[ConfigurationProperty("minLocalRequestFreeThreads", DefaultValue = 4)]
		[IntegerValidator(MinValue = 0)]
		public int MinLocalRequestFreeThreads
		{
			get
			{
				return (int)base[HttpRuntimeSection._propMinLocalRequestFreeThreads];
			}
			set
			{
				base[HttpRuntimeSection._propMinLocalRequestFreeThreads] = value;
			}
		}

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x06005683 RID: 22147 RVA: 0x0012F35C File Offset: 0x0012D55C
		// (set) Token: 0x06005684 RID: 22148 RVA: 0x0012F36E File Offset: 0x0012D56E
		[ConfigurationProperty("appRequestQueueLimit", DefaultValue = 5000)]
		[IntegerValidator(MinValue = 1)]
		public int AppRequestQueueLimit
		{
			get
			{
				return (int)base[HttpRuntimeSection._propAppRequestQueueLimit];
			}
			set
			{
				base[HttpRuntimeSection._propAppRequestQueueLimit] = value;
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x06005685 RID: 22149 RVA: 0x0012F381 File Offset: 0x0012D581
		// (set) Token: 0x06005686 RID: 22150 RVA: 0x0012F393 File Offset: 0x0012D593
		[ConfigurationProperty("enableKernelOutputCache", DefaultValue = true)]
		public bool EnableKernelOutputCache
		{
			get
			{
				return (bool)base[HttpRuntimeSection._propEnableKernelOutputCache];
			}
			set
			{
				base[HttpRuntimeSection._propEnableKernelOutputCache] = value;
			}
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x06005687 RID: 22151 RVA: 0x0012F3A6 File Offset: 0x0012D5A6
		// (set) Token: 0x06005688 RID: 22152 RVA: 0x0012F3D3 File Offset: 0x0012D5D3
		[ConfigurationProperty("enableVersionHeader", DefaultValue = true)]
		public bool EnableVersionHeader
		{
			get
			{
				if (!this.enableVersionHeaderCached)
				{
					this.enableVersionHeaderCache = (bool)base[HttpRuntimeSection._propEnableVersionHeader];
					this.enableVersionHeaderCached = true;
				}
				return this.enableVersionHeaderCache;
			}
			set
			{
				base[HttpRuntimeSection._propEnableVersionHeader] = value;
				this.enableVersionHeaderCache = value;
			}
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x06005689 RID: 22153 RVA: 0x0012F3ED File Offset: 0x0012D5ED
		// (set) Token: 0x0600568A RID: 22154 RVA: 0x0012F3FF File Offset: 0x0012D5FF
		[ConfigurationProperty("apartmentThreading", DefaultValue = false)]
		public bool ApartmentThreading
		{
			get
			{
				return (bool)base[HttpRuntimeSection._propApartmentThreading];
			}
			set
			{
				base[HttpRuntimeSection._propApartmentThreading] = value;
			}
		}

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x0600568B RID: 22155 RVA: 0x0012F412 File Offset: 0x0012D612
		// (set) Token: 0x0600568C RID: 22156 RVA: 0x0012F424 File Offset: 0x0012D624
		[ConfigurationProperty("requireRootedSaveAsPath", DefaultValue = true)]
		public bool RequireRootedSaveAsPath
		{
			get
			{
				return (bool)base[HttpRuntimeSection._propRequireRootedSaveAsPath];
			}
			set
			{
				base[HttpRuntimeSection._propRequireRootedSaveAsPath] = value;
			}
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x0600568D RID: 22157 RVA: 0x0012F437 File Offset: 0x0012D637
		// (set) Token: 0x0600568E RID: 22158 RVA: 0x0012F449 File Offset: 0x0012D649
		[ConfigurationProperty("enable", DefaultValue = true)]
		public bool Enable
		{
			get
			{
				return (bool)base[HttpRuntimeSection._propEnable];
			}
			set
			{
				base[HttpRuntimeSection._propEnable] = value;
			}
		}

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x0600568F RID: 22159 RVA: 0x0012F45C File Offset: 0x0012D65C
		// (set) Token: 0x06005690 RID: 22160 RVA: 0x0012F46E File Offset: 0x0012D66E
		[ConfigurationProperty("targetFramework", DefaultValue = null)]
		public string TargetFramework
		{
			get
			{
				return (string)base[HttpRuntimeSection._propTargetFramework];
			}
			set
			{
				base[HttpRuntimeSection._propTargetFramework] = value;
			}
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x0012F47C File Offset: 0x0012D67C
		internal FrameworkName GetTargetFrameworkName()
		{
			string targetFramework = this.TargetFramework;
			if (string.IsNullOrEmpty(targetFramework))
			{
				return null;
			}
			Version version;
			if (!Version.TryParse(targetFramework, out version))
			{
				PropertyInformation propertyInformation = base.ElementInformation.Properties["targetFramework"];
				throw new ConfigurationErrorsException(SR.GetString("HttpRuntimeSection_TargetFramework_Invalid"), propertyInformation.Source, propertyInformation.LineNumber);
			}
			return new FrameworkName(".NETFramework", version);
		}

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x06005692 RID: 22162 RVA: 0x0012F4E1 File Offset: 0x0012D6E1
		// (set) Token: 0x06005693 RID: 22163 RVA: 0x0012F50E File Offset: 0x0012D70E
		[ConfigurationProperty("sendCacheControlHeader", DefaultValue = true)]
		public bool SendCacheControlHeader
		{
			get
			{
				if (!this.sendCacheControlHeaderCached)
				{
					this.sendCacheControlHeaderCache = (bool)base[HttpRuntimeSection._propSendCacheControlHeader];
					this.sendCacheControlHeaderCached = true;
				}
				return this.sendCacheControlHeaderCache;
			}
			set
			{
				base[HttpRuntimeSection._propSendCacheControlHeader] = value;
				this.sendCacheControlHeaderCache = value;
			}
		}

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x06005694 RID: 22164 RVA: 0x0012F528 File Offset: 0x0012D728
		// (set) Token: 0x06005695 RID: 22165 RVA: 0x0012F53A File Offset: 0x0012D73A
		[ConfigurationProperty("defaultRegexMatchTimeout", DefaultValue = "00:00:00")]
		[RegexMatchTimeoutValidator]
		public TimeSpan DefaultRegexMatchTimeout
		{
			get
			{
				return (TimeSpan)base[HttpRuntimeSection._propDefaultRegexMatchTimeout];
			}
			set
			{
				base[HttpRuntimeSection._propDefaultRegexMatchTimeout] = value;
			}
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x06005696 RID: 22166 RVA: 0x0012F54D File Offset: 0x0012D74D
		// (set) Token: 0x06005697 RID: 22167 RVA: 0x0012F55F File Offset: 0x0012D75F
		[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:01:30")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		public TimeSpan ShutdownTimeout
		{
			get
			{
				return (TimeSpan)base[HttpRuntimeSection._propShutdownTimeout];
			}
			set
			{
				base[HttpRuntimeSection._propShutdownTimeout] = value;
			}
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x06005698 RID: 22168 RVA: 0x0012F572 File Offset: 0x0012D772
		// (set) Token: 0x06005699 RID: 22169 RVA: 0x0012F584 File Offset: 0x0012D784
		[ConfigurationProperty("delayNotificationTimeout", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		public TimeSpan DelayNotificationTimeout
		{
			get
			{
				return (TimeSpan)base[HttpRuntimeSection._propDelayNotificationTimeout];
			}
			set
			{
				base[HttpRuntimeSection._propDelayNotificationTimeout] = value;
			}
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x0600569A RID: 22170 RVA: 0x0012F597 File Offset: 0x0012D797
		// (set) Token: 0x0600569B RID: 22171 RVA: 0x0012F5A9 File Offset: 0x0012D7A9
		[ConfigurationProperty("waitChangeNotification", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int WaitChangeNotification
		{
			get
			{
				return (int)base[HttpRuntimeSection._propWaitChangeNotification];
			}
			set
			{
				base[HttpRuntimeSection._propWaitChangeNotification] = value;
			}
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x0600569C RID: 22172 RVA: 0x0012F5BC File Offset: 0x0012D7BC
		// (set) Token: 0x0600569D RID: 22173 RVA: 0x0012F5CE File Offset: 0x0012D7CE
		[ConfigurationProperty("maxWaitChangeNotification", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxWaitChangeNotification
		{
			get
			{
				return (int)base[HttpRuntimeSection._propMaxWaitChangeNotification];
			}
			set
			{
				base[HttpRuntimeSection._propMaxWaitChangeNotification] = value;
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x0600569E RID: 22174 RVA: 0x0012F5E1 File Offset: 0x0012D7E1
		// (set) Token: 0x0600569F RID: 22175 RVA: 0x0012F5F3 File Offset: 0x0012D7F3
		[ConfigurationProperty("enableHeaderChecking", DefaultValue = true)]
		public bool EnableHeaderChecking
		{
			get
			{
				return (bool)base[HttpRuntimeSection._propEnableHeaderChecking];
			}
			set
			{
				base[HttpRuntimeSection._propEnableHeaderChecking] = value;
			}
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x060056A0 RID: 22176 RVA: 0x0012F606 File Offset: 0x0012D806
		// (set) Token: 0x060056A1 RID: 22177 RVA: 0x0012F618 File Offset: 0x0012D818
		[ConfigurationProperty("encoderType", DefaultValue = "System.Web.Util.HttpEncoder")]
		[StringValidator(MinLength = 1)]
		public string EncoderType
		{
			get
			{
				return (string)base[HttpRuntimeSection._propEncoderType];
			}
			set
			{
				base[HttpRuntimeSection._propEncoderType] = value;
			}
		}

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x060056A2 RID: 22178 RVA: 0x0012F626 File Offset: 0x0012D826
		// (set) Token: 0x060056A3 RID: 22179 RVA: 0x0012F652 File Offset: 0x0012D852
		[ConfigurationProperty("requestValidationMode", DefaultValue = "4.0")]
		[TypeConverter(typeof(VersionConverter))]
		public Version RequestValidationMode
		{
			get
			{
				if (this._requestValidationMode == null)
				{
					this._requestValidationMode = (Version)base[HttpRuntimeSection._propRequestValidationMode];
				}
				return this._requestValidationMode;
			}
			set
			{
				this._requestValidationMode = value;
				base[HttpRuntimeSection._propRequestValidationMode] = value;
			}
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x060056A4 RID: 22180 RVA: 0x0012F667 File Offset: 0x0012D867
		// (set) Token: 0x060056A5 RID: 22181 RVA: 0x0012F679 File Offset: 0x0012D879
		[ConfigurationProperty("requestValidationType", DefaultValue = "System.Web.Util.RequestValidator")]
		[StringValidator(MinLength = 1)]
		public string RequestValidationType
		{
			get
			{
				return (string)base[HttpRuntimeSection._propRequestValidationType];
			}
			set
			{
				base[HttpRuntimeSection._propRequestValidationType] = value;
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x060056A6 RID: 22182 RVA: 0x0012F687 File Offset: 0x0012D887
		// (set) Token: 0x060056A7 RID: 22183 RVA: 0x0012F699 File Offset: 0x0012D899
		[ConfigurationProperty("requestPathInvalidCharacters", DefaultValue = "<,>,*,%,&,:,\\,?")]
		public string RequestPathInvalidCharacters
		{
			get
			{
				return (string)base[HttpRuntimeSection._propRequestPathInvalidCharacters];
			}
			set
			{
				base[HttpRuntimeSection._propRequestPathInvalidCharacters] = value;
				this._RequestPathInvalidCharactersArray = null;
			}
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x060056A8 RID: 22184 RVA: 0x0012F6AE File Offset: 0x0012D8AE
		// (set) Token: 0x060056A9 RID: 22185 RVA: 0x0012F6D4 File Offset: 0x0012D8D4
		[ConfigurationProperty("maxUrlLength", DefaultValue = 260)]
		[IntegerValidator(MinValue = 0)]
		public int MaxUrlLength
		{
			get
			{
				if (this._MaxUrlLength == 0)
				{
					this._MaxUrlLength = (int)base[HttpRuntimeSection._propMaxUrlLength];
				}
				return this._MaxUrlLength;
			}
			set
			{
				this._MaxUrlLength = value;
				base[HttpRuntimeSection._propMaxUrlLength] = value;
			}
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x060056AA RID: 22186 RVA: 0x0012F6EE File Offset: 0x0012D8EE
		// (set) Token: 0x060056AB RID: 22187 RVA: 0x0012F714 File Offset: 0x0012D914
		[ConfigurationProperty("maxQueryStringLength", DefaultValue = 2048)]
		[IntegerValidator(MinValue = 0)]
		public int MaxQueryStringLength
		{
			get
			{
				if (this._MaxQueryStringLength == 0)
				{
					this._MaxQueryStringLength = (int)base[HttpRuntimeSection._propMaxQueryStringLength];
				}
				return this._MaxQueryStringLength;
			}
			set
			{
				this._MaxQueryStringLength = value;
				base[HttpRuntimeSection._propMaxQueryStringLength] = value;
			}
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x060056AC RID: 22188 RVA: 0x0012F72E File Offset: 0x0012D92E
		// (set) Token: 0x060056AD RID: 22189 RVA: 0x0012F740 File Offset: 0x0012D940
		[ConfigurationProperty("relaxedUrlToFileSystemMapping", DefaultValue = false)]
		public bool RelaxedUrlToFileSystemMapping
		{
			get
			{
				return (bool)base[HttpRuntimeSection._propRelaxedUrlToFileSystemMapping];
			}
			set
			{
				base[HttpRuntimeSection._propRelaxedUrlToFileSystemMapping] = value;
			}
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x060056AE RID: 22190 RVA: 0x0012F753 File Offset: 0x0012D953
		// (set) Token: 0x060056AF RID: 22191 RVA: 0x0012F765 File Offset: 0x0012D965
		[ConfigurationProperty("allowDynamicModuleRegistration", DefaultValue = true)]
		public bool AllowDynamicModuleRegistration
		{
			get
			{
				return (bool)base[HttpRuntimeSection._propAllowDynamicModuleRegistration];
			}
			set
			{
				base[HttpRuntimeSection._propAllowDynamicModuleRegistration] = value;
			}
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x0012F778 File Offset: 0x0012D978
		private int BytesFromKilobytes(int kilobytes)
		{
			long num = (long)kilobytes * 1024L;
			if (num >= 2147483647L)
			{
				return int.MaxValue;
			}
			return (int)num;
		}

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x060056B1 RID: 22193 RVA: 0x0012F7A0 File Offset: 0x0012D9A0
		internal int MaxRequestLengthBytes
		{
			get
			{
				if (this._MaxRequestLengthBytes < 0)
				{
					this._MaxRequestLengthBytes = this.BytesFromKilobytes(this.MaxRequestLength);
				}
				return this._MaxRequestLengthBytes;
			}
		}

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x060056B2 RID: 22194 RVA: 0x0012F7C3 File Offset: 0x0012D9C3
		internal int RequestLengthDiskThresholdBytes
		{
			get
			{
				if (this._RequestLengthDiskThresholdBytes < 0)
				{
					this._RequestLengthDiskThresholdBytes = this.BytesFromKilobytes(this.RequestLengthDiskThreshold);
				}
				return this._RequestLengthDiskThresholdBytes;
			}
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x060056B3 RID: 22195 RVA: 0x0012F7E8 File Offset: 0x0012D9E8
		internal string VersionHeader
		{
			get
			{
				if (!this.EnableVersionHeader)
				{
					return null;
				}
				if (HttpRuntimeSection.s_versionHeader == null)
				{
					string text = null;
					try
					{
						string systemWebVersion = VersionInfo.SystemWebVersion;
						int num = systemWebVersion.LastIndexOf('.');
						if (num > 0)
						{
							text = systemWebVersion.Substring(0, num);
						}
					}
					catch
					{
					}
					if (text == null)
					{
						text = string.Empty;
					}
					HttpRuntimeSection.s_versionHeader = text;
				}
				return HttpRuntimeSection.s_versionHeader;
			}
		}

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x060056B4 RID: 22196 RVA: 0x0012F850 File Offset: 0x0012DA50
		internal char[] RequestPathInvalidCharactersArray
		{
			get
			{
				if (this._RequestPathInvalidCharactersArray != null)
				{
					return this._RequestPathInvalidCharactersArray;
				}
				this._RequestPathInvalidCharactersArray = HttpRuntimeSection.DecodeAndThenSplitString(this.RequestPathInvalidCharacters);
				if (this._RequestPathInvalidCharactersArray == null)
				{
					this._RequestPathInvalidCharactersArray = HttpRuntimeSection.SplitStringAndThenDecode(this.RequestPathInvalidCharacters);
				}
				if (this._RequestPathInvalidCharactersArray == null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_property_generic"), base.ElementInformation.Properties[HttpRuntimeSection._propRequestPathInvalidCharacters.Name].Source, base.ElementInformation.Properties[HttpRuntimeSection._propRequestPathInvalidCharacters.Name].LineNumber);
				}
				return this._RequestPathInvalidCharactersArray;
			}
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x0012F8F4 File Offset: 0x0012DAF4
		private static char[] DecodeAndThenSplitString(string invalidCharString)
		{
			if (string.IsNullOrEmpty(invalidCharString))
			{
				return new char[0];
			}
			string[] array = HttpUtility.UrlDecode(invalidCharString, Encoding.UTF8).Split(new char[]
			{
				','
			});
			char[] array2 = new char[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.Length != 1)
				{
					return null;
				}
				array2[i] = text[0];
			}
			return array2;
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x0012F964 File Offset: 0x0012DB64
		private static char[] SplitStringAndThenDecode(string invalidCharString)
		{
			if (string.IsNullOrEmpty(invalidCharString))
			{
				return new char[0];
			}
			string[] array = invalidCharString.Split(new char[]
			{
				','
			});
			char[] array2 = new char[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string text = HttpUtility.UrlDecode(array[i], Encoding.UTF8).Trim();
				if (text.Length != 1)
				{
					return null;
				}
				array2[i] = text[0];
			}
			return array2;
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x0012F9D4 File Offset: 0x0012DBD4
		protected override void SetReadOnly()
		{
			ConfigUtil.SetFX45DefaultValue(this, HttpRuntimeSection._propRequestValidationMode, VersionUtil.Framework45);
			base.SetReadOnly();
		}

		// Token: 0x04002DDE RID: 11742
		internal const int DefaultExecutionTimeout = 110;

		// Token: 0x04002DDF RID: 11743
		internal const int DefaultMaxRequestLength = 4194304;

		// Token: 0x04002DE0 RID: 11744
		internal const int DefaultRequestLengthDiskThreshold = 81920;

		// Token: 0x04002DE1 RID: 11745
		internal const int DefaultMinFreeThreads = 8;

		// Token: 0x04002DE2 RID: 11746
		internal const int DefaultMinLocalRequestFreeThreads = 4;

		// Token: 0x04002DE3 RID: 11747
		internal const int DefaultAppRequestQueueLimit = 100;

		// Token: 0x04002DE4 RID: 11748
		internal const int DefaultShutdownTimeout = 90;

		// Token: 0x04002DE5 RID: 11749
		internal const int DefaultDelayNotificationTimeout = 0;

		// Token: 0x04002DE6 RID: 11750
		internal const int DefaultWaitChangeNotification = 0;

		// Token: 0x04002DE7 RID: 11751
		internal const int DefaultMaxWaitChangeNotification = 0;

		// Token: 0x04002DE8 RID: 11752
		internal const bool DefaultAllowDynamicModuleRegistration = true;

		// Token: 0x04002DE9 RID: 11753
		internal const bool DefaultEnableKernelOutputCache = true;

		// Token: 0x04002DEA RID: 11754
		internal const bool DefaultRequireRootedSaveAsPath = true;

		// Token: 0x04002DEB RID: 11755
		internal const bool DefaultSendCacheControlHeader = true;

		// Token: 0x04002DEC RID: 11756
		internal const string DefaultEncoderType = "System.Web.Util.HttpEncoder";

		// Token: 0x04002DED RID: 11757
		internal static readonly Version DefaultRequestValidationMode = VersionUtil.FrameworkDefault;

		// Token: 0x04002DEE RID: 11758
		internal const string DefaultRequestValidationModeString = "4.0";

		// Token: 0x04002DEF RID: 11759
		internal const string DefaultRequestValidationType = "System.Web.Util.RequestValidator";

		// Token: 0x04002DF0 RID: 11760
		internal const string DefaultRequestPathInvalidCharacters = "<,>,*,%,&,:,\\,?";

		// Token: 0x04002DF1 RID: 11761
		internal const int DefaultMaxUrlLength = 260;

		// Token: 0x04002DF2 RID: 11762
		internal const int DefaultMaxQueryStringLength = 2048;

		// Token: 0x04002DF3 RID: 11763
		internal const bool DefaultRelaxedUrlToFileSystemMapping = false;

		// Token: 0x04002DF4 RID: 11764
		internal const string DefaultTargetFramework = null;

		// Token: 0x04002DF5 RID: 11765
		private AsyncPreloadModeFlags asyncPreloadModeCache;

		// Token: 0x04002DF6 RID: 11766
		private bool asyncPreloadModeCached;

		// Token: 0x04002DF7 RID: 11767
		private bool enableVersionHeaderCache = true;

		// Token: 0x04002DF8 RID: 11768
		private bool enableVersionHeaderCached;

		// Token: 0x04002DF9 RID: 11769
		private TimeSpan executionTimeoutCache;

		// Token: 0x04002DFA RID: 11770
		private bool executionTimeoutCached;

		// Token: 0x04002DFB RID: 11771
		private bool sendCacheControlHeaderCached;

		// Token: 0x04002DFC RID: 11772
		private bool sendCacheControlHeaderCache;

		// Token: 0x04002DFD RID: 11773
		private FcnMode fcnModeCache;

		// Token: 0x04002DFE RID: 11774
		private bool fcnModeCached;

		// Token: 0x04002DFF RID: 11775
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002E00 RID: 11776
		private static readonly ConfigurationProperty _propAsyncPreloadMode = new ConfigurationProperty("asyncPreloadMode", typeof(AsyncPreloadModeFlags), AsyncPreloadModeFlags.None, ConfigurationPropertyOptions.None);

		// Token: 0x04002E01 RID: 11777
		private static readonly ConfigurationProperty _propExecutionTimeout = new ConfigurationProperty("executionTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(110.0), StdValidatorsAndConverters.TimeSpanSecondsConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E02 RID: 11778
		private static readonly ConfigurationProperty _propMaxRequestLength = new ConfigurationProperty("maxRequestLength", typeof(int), 4096, null, StdValidatorsAndConverters.NonZeroPositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E03 RID: 11779
		private static readonly ConfigurationProperty _propRequestLengthDiskThreshold = new ConfigurationProperty("requestLengthDiskThreshold", typeof(int), 80, null, StdValidatorsAndConverters.NonZeroPositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E04 RID: 11780
		private static readonly ConfigurationProperty _propUseFullyQualifiedRedirectUrl = new ConfigurationProperty("useFullyQualifiedRedirectUrl", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002E05 RID: 11781
		private static readonly ConfigurationProperty _propMinFreeThreads = new ConfigurationProperty("minFreeThreads", typeof(int), 8, null, StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E06 RID: 11782
		private static readonly ConfigurationProperty _propMinLocalRequestFreeThreads = new ConfigurationProperty("minLocalRequestFreeThreads", typeof(int), 4, null, StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E07 RID: 11783
		private static readonly ConfigurationProperty _propAppRequestQueueLimit = new ConfigurationProperty("appRequestQueueLimit", typeof(int), 5000, null, StdValidatorsAndConverters.NonZeroPositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E08 RID: 11784
		private static readonly ConfigurationProperty _propEnableKernelOutputCache = new ConfigurationProperty("enableKernelOutputCache", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002E09 RID: 11785
		private static readonly ConfigurationProperty _propEnableVersionHeader = new ConfigurationProperty("enableVersionHeader", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002E0A RID: 11786
		private static readonly ConfigurationProperty _propRequireRootedSaveAsPath = new ConfigurationProperty("requireRootedSaveAsPath", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002E0B RID: 11787
		private static readonly ConfigurationProperty _propEnable = new ConfigurationProperty("enable", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002E0C RID: 11788
		private static readonly ConfigurationProperty _propDefaultRegexMatchTimeout = new ConfigurationProperty("defaultRegexMatchTimeout", typeof(TimeSpan), TimeSpan.Zero, null, StdValidatorsAndConverters.RegexMatchTimeoutValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E0D RID: 11789
		private static readonly ConfigurationProperty _propShutdownTimeout = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(90.0), StdValidatorsAndConverters.TimeSpanSecondsConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002E0E RID: 11790
		private static readonly ConfigurationProperty _propDelayNotificationTimeout = new ConfigurationProperty("delayNotificationTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), StdValidatorsAndConverters.TimeSpanSecondsConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002E0F RID: 11791
		private static readonly ConfigurationProperty _propWaitChangeNotification = new ConfigurationProperty("waitChangeNotification", typeof(int), 0, null, StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E10 RID: 11792
		private static readonly ConfigurationProperty _propMaxWaitChangeNotification = new ConfigurationProperty("maxWaitChangeNotification", typeof(int), 0, null, StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E11 RID: 11793
		private static readonly ConfigurationProperty _propEnableHeaderChecking = new ConfigurationProperty("enableHeaderChecking", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002E12 RID: 11794
		private static readonly ConfigurationProperty _propSendCacheControlHeader = new ConfigurationProperty("sendCacheControlHeader", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002E13 RID: 11795
		private static readonly ConfigurationProperty _propApartmentThreading = new ConfigurationProperty("apartmentThreading", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002E14 RID: 11796
		private static readonly ConfigurationProperty _propEncoderType = new ConfigurationProperty("encoderType", typeof(string), "System.Web.Util.HttpEncoder", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E15 RID: 11797
		private static readonly ConfigurationProperty _propRequestValidationMode = new ConfigurationProperty("requestValidationMode", typeof(Version), HttpRuntimeSection.DefaultRequestValidationMode, StdValidatorsAndConverters.VersionConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002E16 RID: 11798
		private static readonly ConfigurationProperty _propRequestValidationType = new ConfigurationProperty("requestValidationType", typeof(string), "System.Web.Util.RequestValidator", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E17 RID: 11799
		private static readonly ConfigurationProperty _propRequestPathInvalidCharacters = new ConfigurationProperty("requestPathInvalidCharacters", typeof(string), "<,>,*,%,&,:,\\,?", StdValidatorsAndConverters.WhiteSpaceTrimStringConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002E18 RID: 11800
		private static readonly ConfigurationProperty _propMaxUrlLength = new ConfigurationProperty("maxUrlLength", typeof(int), 260, null, new IntegerValidator(0, 2097151), ConfigurationPropertyOptions.None);

		// Token: 0x04002E19 RID: 11801
		private static readonly ConfigurationProperty _propMaxQueryStringLength = new ConfigurationProperty("maxQueryStringLength", typeof(int), 2048, null, new IntegerValidator(0, 2097151), ConfigurationPropertyOptions.None);

		// Token: 0x04002E1A RID: 11802
		private static readonly ConfigurationProperty _propRelaxedUrlToFileSystemMapping = new ConfigurationProperty("relaxedUrlToFileSystemMapping", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002E1B RID: 11803
		private static readonly ConfigurationProperty _propAllowDynamicModuleRegistration = new ConfigurationProperty("allowDynamicModuleRegistration", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002E1C RID: 11804
		private static readonly ConfigurationProperty _propFcnMode = new ConfigurationProperty("fcnMode", typeof(FcnMode), FcnMode.NotSet, ConfigurationPropertyOptions.None);

		// Token: 0x04002E1D RID: 11805
		private static readonly ConfigurationProperty _propTargetFramework = new ConfigurationProperty("targetFramework", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002E1E RID: 11806
		private int _MaxRequestLengthBytes;

		// Token: 0x04002E1F RID: 11807
		private int _RequestLengthDiskThresholdBytes;

		// Token: 0x04002E20 RID: 11808
		private static string s_versionHeader = null;

		// Token: 0x04002E21 RID: 11809
		private Version _requestValidationMode;

		// Token: 0x04002E22 RID: 11810
		private int _MaxUrlLength;

		// Token: 0x04002E23 RID: 11811
		private int _MaxQueryStringLength;

		// Token: 0x04002E24 RID: 11812
		private char[] _RequestPathInvalidCharactersArray;
	}
}
