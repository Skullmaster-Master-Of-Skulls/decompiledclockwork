using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Discovery;
using Google.Apis.Http;
using Google.Apis.Json;
using Google.Apis.Logging;
using Google.Apis.Requests;
using Google.Apis.Testing;
using Google.Apis.Util;
using Newtonsoft.Json;

namespace Google.Apis.Services
{
	// Token: 0x02000010 RID: 16
	public abstract class BaseClientService : IClientService, IDisposable
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002EAC File Offset: 0x000010AC
		protected BaseClientService(BaseClientService.Initializer initializer)
		{
			this.GZipEnabled = initializer.GZipEnabled;
			this.Serializer = initializer.Serializer;
			this.ApiKey = initializer.ApiKey;
			this.ApplicationName = initializer.ApplicationName;
			if (this.ApplicationName == null)
			{
				BaseClientService.Logger.Warning("Application name is not set. Please set Initializer.ApplicationName property", new object[0]);
			}
			this.HttpClientInitializer = initializer.HttpClientInitializer;
			this.HttpClient = this.CreateHttpClient(initializer);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002F25 File Offset: 0x00001125
		private bool HasFeature(Features feature)
		{
			return this.Features.Contains(Utilities.GetEnumStringValue(feature));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002F40 File Offset: 0x00001140
		private ConfigurableHttpClient CreateHttpClient(BaseClientService.Initializer initializer)
		{
			IHttpClientFactory httpClientFactory = initializer.HttpClientFactory ?? new HttpClientFactory();
			CreateHttpClientArgs createHttpClientArgs = new CreateHttpClientArgs
			{
				GZipEnabled = this.GZipEnabled,
				ApplicationName = this.ApplicationName
			};
			if (this.HttpClientInitializer != null)
			{
				createHttpClientArgs.Initializers.Add(this.HttpClientInitializer);
			}
			if (initializer.DefaultExponentialBackOffPolicy != ExponentialBackOffPolicy.None)
			{
				createHttpClientArgs.Initializers.Add(new ExponentialBackOffInitializer(initializer.DefaultExponentialBackOffPolicy, new Func<BackOffHandler>(this.CreateBackOffHandler)));
			}
			ConfigurableHttpClient configurableHttpClient = httpClientFactory.CreateHttpClient(createHttpClientArgs);
			if (initializer.MaxUrlLength > 0U)
			{
				configurableHttpClient.MessageHandler.AddExecuteInterceptor(new MaxUrlLengthInterceptor(initializer.MaxUrlLength));
			}
			return configurableHttpClient;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002FE5 File Offset: 0x000011E5
		protected virtual BackOffHandler CreateBackOffHandler()
		{
			return new BackOffHandler(new ExponentialBackOff());
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002FF1 File Offset: 0x000011F1
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002FF9 File Offset: 0x000011F9
		public ConfigurableHttpClient HttpClient { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003002 File Offset: 0x00001202
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000300A File Offset: 0x0000120A
		public IConfigurableHttpClientInitializer HttpClientInitializer { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003013 File Offset: 0x00001213
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000301B File Offset: 0x0000121B
		public bool GZipEnabled { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003024 File Offset: 0x00001224
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000302C File Offset: 0x0000122C
		public string ApiKey { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003035 File Offset: 0x00001235
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000303D File Offset: 0x0000123D
		public string ApplicationName { get; private set; }

		// Token: 0x06000087 RID: 135 RVA: 0x00003046 File Offset: 0x00001246
		public void SetRequestSerailizedContent(HttpRequestMessage request, object body)
		{
			request.SetRequestSerailizedContent(this, body, this.GZipEnabled);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003056 File Offset: 0x00001256
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000305E File Offset: 0x0000125E
		public ISerializer Serializer { get; private set; }

		// Token: 0x0600008A RID: 138 RVA: 0x00003068 File Offset: 0x00001268
		public virtual string SerializeObject(object obj)
		{
			if (this.HasFeature(Google.Apis.Discovery.Features.LegacyDataResponse))
			{
				StandardResponse<object> obj2 = new StandardResponse<object>
				{
					Data = obj
				};
				return this.Serializer.Serialize(obj2);
			}
			return this.Serializer.Serialize(obj);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000030A4 File Offset: 0x000012A4
		public virtual async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
		{
			string text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			T result;
			if (object.Equals(typeof(T), typeof(string)))
			{
				result = (T)((object)text);
			}
			else if (this.HasFeature(Google.Apis.Discovery.Features.LegacyDataResponse))
			{
				StandardResponse<T> standardResponse = null;
				try
				{
					standardResponse = this.Serializer.Deserialize<StandardResponse<T>>(text);
				}
				catch (JsonReaderException inner)
				{
					throw new GoogleApiException(this.Name, "Failed to parse response from server as json [" + text + "]", inner);
				}
				if (standardResponse.Error != null)
				{
					throw new GoogleApiException(this.Name, "Server error - " + standardResponse.Error)
					{
						Error = standardResponse.Error
					};
				}
				if (standardResponse.Data == null)
				{
					throw new GoogleApiException(this.Name, "The response could not be deserialized.");
				}
				result = standardResponse.Data;
			}
			else
			{
				T t = default(T);
				try
				{
					t = this.Serializer.Deserialize<T>(text);
				}
				catch (JsonReaderException inner2)
				{
					throw new GoogleApiException(this.Name, "Failed to parse response from server as json [" + text + "]", inner2);
				}
				string text2 = (response.Headers.ETag != null) ? response.Headers.ETag.Tag : null;
				if (t is IDirectResponseSchema && text2 != null)
				{
					(t as IDirectResponseSchema).ETag = text2;
				}
				result = t;
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000030F4 File Offset: 0x000012F4
		public virtual async Task<RequestError> DeserializeError(HttpResponseMessage response)
		{
			StandardResponse<object> errorResponse = null;
			try
			{
				string input = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				errorResponse = this.Serializer.Deserialize<StandardResponse<object>>(input);
				if (errorResponse.Error == null)
				{
					throw new GoogleApiException(this.Name, "error response is null");
				}
			}
			catch (Exception inner)
			{
				throw new GoogleApiException(this.Name, "An Error occurred, but the error response could not be deserialized", inner);
			}
			return errorResponse.Error;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008D RID: 141
		public abstract string Name { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008E RID: 142
		public abstract string BaseUri { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008F RID: 143
		public abstract string BasePath { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003141 File Offset: 0x00001341
		public virtual string BatchUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003141 File Offset: 0x00001341
		public virtual string BatchPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000092 RID: 146
		public abstract IList<string> Features { get; }

		// Token: 0x06000093 RID: 147 RVA: 0x00003144 File Offset: 0x00001344
		public virtual void Dispose()
		{
			if (this.HttpClient != null)
			{
				this.HttpClient.Dispose();
			}
		}

		// Token: 0x0400003E RID: 62
		private static readonly ILogger Logger = ApplicationContext.Logger.ForType<BaseClientService>();

		// Token: 0x0400003F RID: 63
		[VisibleForTestOnly]
		public const uint DefaultMaxUrlLength = 2048U;

		// Token: 0x0200002A RID: 42
		public class Initializer
		{
			// Token: 0x17000052 RID: 82
			// (get) Token: 0x0600011A RID: 282 RVA: 0x00005ABA File Offset: 0x00003CBA
			// (set) Token: 0x0600011B RID: 283 RVA: 0x00005AC2 File Offset: 0x00003CC2
			public IHttpClientFactory HttpClientFactory { get; set; }

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x0600011C RID: 284 RVA: 0x00005ACB File Offset: 0x00003CCB
			// (set) Token: 0x0600011D RID: 285 RVA: 0x00005AD3 File Offset: 0x00003CD3
			public IConfigurableHttpClientInitializer HttpClientInitializer { get; set; }

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x0600011E RID: 286 RVA: 0x00005ADC File Offset: 0x00003CDC
			// (set) Token: 0x0600011F RID: 287 RVA: 0x00005AE4 File Offset: 0x00003CE4
			public ExponentialBackOffPolicy DefaultExponentialBackOffPolicy { get; set; }

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000120 RID: 288 RVA: 0x00005AED File Offset: 0x00003CED
			// (set) Token: 0x06000121 RID: 289 RVA: 0x00005AF5 File Offset: 0x00003CF5
			public bool GZipEnabled { get; set; }

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000122 RID: 290 RVA: 0x00005AFE File Offset: 0x00003CFE
			// (set) Token: 0x06000123 RID: 291 RVA: 0x00005B06 File Offset: 0x00003D06
			public ISerializer Serializer { get; set; }

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000124 RID: 292 RVA: 0x00005B0F File Offset: 0x00003D0F
			// (set) Token: 0x06000125 RID: 293 RVA: 0x00005B17 File Offset: 0x00003D17
			public string ApiKey { get; set; }

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000126 RID: 294 RVA: 0x00005B20 File Offset: 0x00003D20
			// (set) Token: 0x06000127 RID: 295 RVA: 0x00005B28 File Offset: 0x00003D28
			public string ApplicationName { get; set; }

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000128 RID: 296 RVA: 0x00005B31 File Offset: 0x00003D31
			// (set) Token: 0x06000129 RID: 297 RVA: 0x00005B39 File Offset: 0x00003D39
			public uint MaxUrlLength { get; set; }

			// Token: 0x0600012A RID: 298 RVA: 0x00005B42 File Offset: 0x00003D42
			public Initializer()
			{
				this.GZipEnabled = true;
				this.Serializer = new NewtonsoftJsonSerializer();
				this.DefaultExponentialBackOffPolicy = ExponentialBackOffPolicy.UnsuccessfulResponse503;
				this.MaxUrlLength = 2048U;
			}
		}
	}
}
