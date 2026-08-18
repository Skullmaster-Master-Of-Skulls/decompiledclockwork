using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Credentials;
using TechnoPro.Common.Web.Security.Exceptions;

namespace TechnoPro.Common.Web.Security.Proxy
{
	// Token: 0x02000008 RID: 8
	public abstract class RestProxy<T> : IWebService where T : IWebService
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000022C9 File Offset: 0x000004C9
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000022D1 File Offset: 0x000004D1
		protected virtual string DefaultAuthenticationMethod { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000022DA File Offset: 0x000004DA
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000022E2 File Offset: 0x000004E2
		public string ServiceAddress { get; protected set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000022EB File Offset: 0x000004EB
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000022F3 File Offset: 0x000004F3
		protected string DefaultAddressSuffix { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000022FC File Offset: 0x000004FC
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002304 File Offset: 0x00000504
		public IHttpClientBuilder HttpClientBuilder { get; protected set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000230D File Offset: 0x0000050D
		public string AuthenticationType
		{
			get
			{
				return this.HttpClientBuilder.AuthenticationType;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000231A File Offset: 0x0000051A
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002322 File Offset: 0x00000522
		protected HttpClient ServiceProxy { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000232C File Offset: 0x0000052C
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002357 File Offset: 0x00000557
		public virtual IUserCredentials ClientCredentials
		{
			get
			{
				IUserCredentials result;
				if ((result = this._userSecCredentials) == null)
				{
					result = (this._userSecCredentials = ObjectFactory.Resolve<IUserCredentials>(this.DefaultAuthenticationMethod));
				}
				return result;
			}
			set
			{
				this._userSecCredentials = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002360 File Offset: 0x00000560
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002385 File Offset: 0x00000585
		public MediaTypeFormatter DefaultMediaTypeFormatter
		{
			get
			{
				MediaTypeFormatter result;
				if ((result = this._mediaTypeFormatter) == null)
				{
					result = (this._mediaTypeFormatter = new JsonMediaTypeFormatter());
				}
				return result;
			}
			set
			{
				this._mediaTypeFormatter = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000238E File Offset: 0x0000058E
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002396 File Offset: 0x00000596
		public OperationContext OpContext { get; set; }

		// Token: 0x0600003B RID: 59 RVA: 0x0000239F File Offset: 0x0000059F
		protected RestProxy(string serviceAddress, IHttpClientBuilder httpClientBuilder) : this(serviceAddress, string.Empty, httpClientBuilder)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000023AE File Offset: 0x000005AE
		protected RestProxy(string serviceAddress, string authenticationType) : this(serviceAddress, ObjectFactory.Resolve<IHttpClientBuilder>(authenticationType))
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000023BD File Offset: 0x000005BD
		protected RestProxy(string serviceAddress, string authenticationType, IUserCredentials credentials) : this(serviceAddress, string.Empty, ObjectFactory.Resolve<IHttpClientBuilder>(authenticationType), credentials)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000023D2 File Offset: 0x000005D2
		protected RestProxy(string serviceAddress) : this(serviceAddress, "Basic")
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000023E0 File Offset: 0x000005E0
		protected RestProxy(string serviceAddress, string serviceAddressSuffix, IHttpClientBuilder httpClientBuilder)
		{
			this.DefaultAuthenticationMethod = "Basic";
			base..ctor();
			this.ServiceAddress = this.ValidateAddress(serviceAddress);
			this.DefaultAddressSuffix = this.ValidateAddress(serviceAddressSuffix);
			this.HttpClientBuilder = (httpClientBuilder ?? ObjectFactory.Resolve<IHttpClientBuilder>("Basic"));
			this.HttpClientBuilder.DefaultMediaTypeFormatter = this.DefaultMediaTypeFormatter;
			this.HttpClientBuilder.ServiceAddress = this.ServiceAddress;
			this.HttpClientBuilder.DefaultAddressSuffix = this.DefaultAddressSuffix;
			this.ServiceProxy = this.CreateServiceProxy();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000246C File Offset: 0x0000066C
		protected RestProxy(string serviceAddress, string serviceAddressSuffix, IHttpClientBuilder httpClientBuilder, IUserCredentials credentials)
		{
			this.DefaultAuthenticationMethod = "Basic";
			base..ctor();
			this.ClientCredentials = credentials;
			this.ServiceAddress = this.ValidateAddress(serviceAddress);
			this.DefaultAddressSuffix = this.ValidateAddress(serviceAddressSuffix);
			this.HttpClientBuilder = (httpClientBuilder ?? ObjectFactory.Resolve<IHttpClientBuilder>("Basic"));
			this.HttpClientBuilder.DefaultMediaTypeFormatter = this.DefaultMediaTypeFormatter;
			this.HttpClientBuilder.ServiceAddress = this.ServiceAddress;
			this.HttpClientBuilder.DefaultAddressSuffix = this.DefaultAddressSuffix;
			this.ServiceProxy = this.CreateServiceProxy();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002500 File Offset: 0x00000700
		protected RestProxy(string serviceAddress, string serviceAddressSuffix, string authenticationType) : this(serviceAddress, serviceAddressSuffix, ObjectFactory.Resolve<IHttpClientBuilder>(authenticationType))
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002510 File Offset: 0x00000710
		protected RestProxy(string serviceAddress, string serviceAddressSuffix, string authenticationType, IUserCredentials credentials) : this(serviceAddress, serviceAddressSuffix, ObjectFactory.Resolve<IHttpClientBuilder>(authenticationType), credentials)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002522 File Offset: 0x00000722
		protected string ValidateAddress(string address)
		{
			if (!address.EndsWith("/"))
			{
				return address + "/";
			}
			return address;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002540 File Offset: 0x00000740
		protected HttpClient CreateServiceProxy()
		{
			HttpClient httpClient = this.HttpClientBuilder.CreateHttpClient(this.ClientCredentials);
			this.SetProxyProperties(httpClient);
			return httpClient;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002567 File Offset: 0x00000767
		protected virtual void SetProxyProperties(HttpClient httpClient)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000256C File Offset: 0x0000076C
		protected async Task<IList<TModel>> GetManyAsync<TModel>(bool ignoreExceptions = true)
		{
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.GetAsync(this.DefaultAddressSuffix);
			if (ignoreExceptions)
			{
				try
				{
					httpResponseMessage.EnsureSuccessStatusCode();
					goto IL_AA;
				}
				catch
				{
					return null;
				}
			}
			httpResponseMessage.EnsureSuccessStatusCode();
			IL_AA:
			return await httpResponseMessage.Content.ReadAsAsync<IList<TModel>>();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000025BC File Offset: 0x000007BC
		protected async Task<IList<TModel>> GetManyAsync<TModel>(string addressSuffix, bool ignoreExceptions = true)
		{
			string requestUri = this.ValidateAddress(addressSuffix);
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.GetAsync(requestUri);
			if (ignoreExceptions)
			{
				try
				{
					httpResponseMessage.EnsureSuccessStatusCode();
					goto IL_B5;
				}
				catch
				{
					return null;
				}
			}
			httpResponseMessage.EnsureSuccessStatusCode();
			IL_B5:
			return await httpResponseMessage.Content.ReadAsAsync<IList<TModel>>();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002614 File Offset: 0x00000814
		protected async Task<TModel> GetAsync<TModel>(string identifier, string addressSuffix, bool ignoreExceptions = true)
		{
			string str = this.ValidateAddress(addressSuffix);
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.GetAsync(str + identifier);
			HttpResponseMessage responseMessage = httpResponseMessage;
			if (ignoreExceptions)
			{
				int num = 0;
				try
				{
					responseMessage.EnsureSuccessStatusCode();
				}
				catch
				{
					num = 1;
				}
				if (num == 1)
				{
					return await Task.FromResult<TModel>(default(TModel));
				}
			}
			else
			{
				responseMessage.EnsureSuccessStatusCode();
			}
			return (!(typeof(TModel) == typeof(string))) ? (await responseMessage.Content.ReadAsAsync<TModel>()) : ((TModel)((object)(await responseMessage.Content.ReadAsStringAsync())));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002674 File Offset: 0x00000874
		protected async Task<TModel> GetAsync<TModel>(string addressSuffix, bool ignoreExceptions = true)
		{
			string requestUri = this.ValidateAddress(addressSuffix);
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.GetAsync(requestUri);
			HttpResponseMessage responseMessage = httpResponseMessage;
			if (ignoreExceptions)
			{
				int num = 0;
				try
				{
					responseMessage.EnsureSuccessStatusCode();
				}
				catch
				{
					num = 1;
				}
				if (num == 1)
				{
					return await Task.FromResult<TModel>(default(TModel));
				}
			}
			else
			{
				responseMessage.EnsureSuccessStatusCode();
			}
			return (!(typeof(TModel) == typeof(string))) ? (await responseMessage.Content.ReadAsAsync<TModel>()) : ((TModel)((object)(await responseMessage.Content.ReadAsStringAsync())));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000026CC File Offset: 0x000008CC
		protected async Task<TModel> GetAsync<TModel>(string[] identifiers, bool ignoreExceptions = true)
		{
			StringBuilder stringBuilder = new StringBuilder(this.DefaultAddressSuffix);
			foreach (string value in identifiers)
			{
				stringBuilder.Append(value);
				stringBuilder.Append("/");
			}
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.GetAsync(stringBuilder.ToString());
			HttpResponseMessage responseMessage = httpResponseMessage;
			if (ignoreExceptions)
			{
				int i = 0;
				try
				{
					responseMessage.EnsureSuccessStatusCode();
				}
				catch
				{
					i = 1;
				}
				if (i == 1)
				{
					return await Task.FromResult<TModel>(default(TModel));
				}
			}
			else
			{
				responseMessage.EnsureSuccessStatusCode();
			}
			return (!(typeof(TModel) == typeof(string))) ? (await responseMessage.Content.ReadAsAsync<TModel>()) : ((TModel)((object)(await responseMessage.Content.ReadAsStringAsync())));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002724 File Offset: 0x00000924
		protected async Task<TModel> GetAsync<TModel>(string[] identifiers, string addressSuffix, bool ignoreExceptions = true)
		{
			StringBuilder stringBuilder = new StringBuilder(this.ValidateAddress(addressSuffix));
			foreach (string value in identifiers)
			{
				stringBuilder.Append(value);
				stringBuilder.Append("/");
			}
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.GetAsync(stringBuilder.ToString());
			HttpResponseMessage responseMessage = httpResponseMessage;
			if (ignoreExceptions)
			{
				int i = 0;
				try
				{
					responseMessage.EnsureSuccessStatusCode();
				}
				catch
				{
					i = 1;
				}
				if (i == 1)
				{
					return await Task.FromResult<TModel>(default(TModel));
				}
			}
			else
			{
				responseMessage.EnsureSuccessStatusCode();
			}
			return (!(typeof(TModel) == typeof(string))) ? (await responseMessage.Content.ReadAsAsync<TModel>()) : ((TModel)((object)(await responseMessage.Content.ReadAsStringAsync())));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002784 File Offset: 0x00000984
		protected async Task<TModelResult> PostAsync<TModel, TModelResult>(TModel model)
		{
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.PostAsync(this.DefaultAddressSuffix, model, this.DefaultMediaTypeFormatter);
			HttpResponseMessage responseMessage = httpResponseMessage;
			if (responseMessage.IsSuccessStatusCode)
			{
				return (!(typeof(TModelResult) == typeof(string))) ? (await responseMessage.Content.ReadAsAsync<TModelResult>()) : ((TModelResult)((object)(await responseMessage.Content.ReadAsStringAsync())));
			}
			if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new InvalidCredentialsException("User couldn't be authenticated");
			}
			throw new WebException(string.Format("Asynchronous post operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, this.DefaultAddressSuffix));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000027D4 File Offset: 0x000009D4
		protected async Task<TModelResult> PostAsync<TModel, TModelResult>(TModel model, string addressSuffix)
		{
			string validatedAddressSuffix = this.ValidateAddress(addressSuffix);
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.PostAsync(validatedAddressSuffix, model, this.DefaultMediaTypeFormatter);
			HttpResponseMessage responseMessage = httpResponseMessage;
			if (responseMessage.IsSuccessStatusCode)
			{
				return (!(typeof(TModelResult) == typeof(string))) ? (await responseMessage.Content.ReadAsAsync<TModelResult>()) : ((TModelResult)((object)(await responseMessage.Content.ReadAsStringAsync())));
			}
			if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new InvalidCredentialsException("User couldn't be authenticated");
			}
			throw new WebException(string.Format("Asynchronous post operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, validatedAddressSuffix));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000282C File Offset: 0x00000A2C
		protected async Task<TModelResult> PostAsync<TModelResult>(string addressSuffix)
		{
			string validatedAddressSuffix = this.ValidateAddress(addressSuffix);
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.PostAsync(validatedAddressSuffix, null, this.DefaultMediaTypeFormatter);
			HttpResponseMessage responseMessage = httpResponseMessage;
			if (responseMessage.IsSuccessStatusCode)
			{
				return (!(typeof(TModelResult) == typeof(string))) ? (await responseMessage.Content.ReadAsAsync<TModelResult>()) : ((TModelResult)((object)(await responseMessage.Content.ReadAsStringAsync())));
			}
			if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new InvalidCredentialsException("User couldn't be authenticated");
			}
			throw new WebException(string.Format("Asynchronous post operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, validatedAddressSuffix));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000287C File Offset: 0x00000A7C
		protected async Task PostAsync<TModel>(TModel model, string addressSuffix)
		{
			string validatedAddressSuffix = this.ValidateAddress(addressSuffix);
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.PostAsync(validatedAddressSuffix, model, this.DefaultMediaTypeFormatter);
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				return;
			}
			if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new InvalidCredentialsException("User couldn't be authenticated");
			}
			throw new WebException(string.Format("Asynchronous post operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, validatedAddressSuffix));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000028D4 File Offset: 0x00000AD4
		protected async Task PostAsync(string addressSuffix)
		{
			string validatedAddressSuffix = this.ValidateAddress(addressSuffix);
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.PostAsync(validatedAddressSuffix, null, this.DefaultMediaTypeFormatter);
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				return;
			}
			if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new InvalidCredentialsException("User couldn't be authenticated");
			}
			throw new WebException(string.Format("Asynchronous post operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, validatedAddressSuffix));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002924 File Offset: 0x00000B24
		protected async Task PutAsync<TModel>(string identifier, TModel model, string addressSuffix)
		{
			string validatedAddressSuffix = this.ValidateAddress(addressSuffix);
			if (!(await this.ServiceProxy.PutAsync(validatedAddressSuffix + identifier, model, this.DefaultMediaTypeFormatter)).IsSuccessStatusCode)
			{
				throw new WebException(string.Format("Asynchronous put operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, validatedAddressSuffix + identifier));
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002984 File Offset: 0x00000B84
		protected async Task PutAsync<TModel>(TModel model, string addressSuffix)
		{
			string validatedAddressSuffix = this.ValidateAddress(addressSuffix);
			if (!(await this.ServiceProxy.PutAsync(validatedAddressSuffix, model, this.DefaultMediaTypeFormatter)).IsSuccessStatusCode)
			{
				throw new WebException(string.Format("Asynchronous put operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, validatedAddressSuffix));
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000029DC File Offset: 0x00000BDC
		protected async Task PutAsync(string addressSuffix)
		{
			string validatedAddressSuffix = this.ValidateAddress(addressSuffix);
			if (!(await this.ServiceProxy.PutAsync(validatedAddressSuffix, null, this.DefaultMediaTypeFormatter)).IsSuccessStatusCode)
			{
				throw new WebException(string.Format("Asynchronous put operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, validatedAddressSuffix));
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A2C File Offset: 0x00000C2C
		protected async Task<TModelResult> PutAsync<TModel, TModelResult>(TModel model, string addressSuffix)
		{
			string validatedAddressSuffix = this.ValidateAddress(addressSuffix);
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.PutAsync(validatedAddressSuffix, model, this.DefaultMediaTypeFormatter);
			HttpResponseMessage responseMessage = httpResponseMessage;
			if (!responseMessage.IsSuccessStatusCode)
			{
				throw new WebException(string.Format("Asynchronous put operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, validatedAddressSuffix));
			}
			return (!(typeof(TModelResult) == typeof(string))) ? (await responseMessage.Content.ReadAsAsync<TModelResult>()) : ((TModelResult)((object)(await responseMessage.Content.ReadAsStringAsync())));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A84 File Offset: 0x00000C84
		protected async Task DeleteAsync(string identifier, string addressSuffix)
		{
			string validatedAddressSuffix = this.ValidateAddress(addressSuffix);
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.DeleteAsync(validatedAddressSuffix + identifier);
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				return;
			}
			if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new InvalidCredentialsException("User couldn't be authenticated");
			}
			throw new WebException(string.Format("Asynchronous delete operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, validatedAddressSuffix + identifier));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002ADC File Offset: 0x00000CDC
		protected async Task DeleteAsync(string addressSuffix)
		{
			string validatedAddressSuffix = this.ValidateAddress(addressSuffix);
			HttpResponseMessage httpResponseMessage = await this.ServiceProxy.DeleteAsync(validatedAddressSuffix);
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				return;
			}
			if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new InvalidCredentialsException("User couldn't be authenticated");
			}
			throw new WebException(string.Format("Asynchronous delete operation was not successfully executed for service address: {0} and suffix address {1}", this.ServiceAddress, validatedAddressSuffix));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B2C File Offset: 0x00000D2C
		protected IList<TModel> GetMany<TModel>(bool ignoreExceptions = true)
		{
			IList<TModel> result;
			try
			{
				result = Task.Run<IList<TModel>>(() => this.GetManyAsync<TModel>(ignoreExceptions)).Result;
			}
			catch (HttpRequestException inner)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous query into the rest service. See inner exception for more information", inner);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is HttpRequestException && ex.InnerException.Message == "Response status code does not indicate success: 401 (Unauthorized).")
				{
					throw new InvalidCredentialsException("User credentials couldn't be authenticated");
				}
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002BD8 File Offset: 0x00000DD8
		protected IList<TModel> GetMany<TModel>(string addressSuffix, bool ignoreExceptions = true)
		{
			IList<TModel> result;
			try
			{
				result = Task.Run<IList<TModel>>(() => this.GetManyAsync<TModel>(addressSuffix, ignoreExceptions)).Result;
			}
			catch (HttpRequestException inner)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous query into the rest service. See inner exception for more information", inner);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is HttpRequestException && ex.InnerException.Message == "Response status code does not indicate success: 401 (Unauthorized).")
				{
					throw new InvalidCredentialsException("User credentials couldn't be authenticated");
				}
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C8C File Offset: 0x00000E8C
		protected TModel Get<TModel>(string identifier, bool ignoreExceptions = true)
		{
			TModel result;
			try
			{
				result = Task.Run<TModel>(() => this.GetAsync<TModel>(identifier, ignoreExceptions)).Result;
			}
			catch (HttpRequestException inner)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous query into the rest service. See inner exception for more information", inner);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is HttpRequestException && ex.InnerException.Message == "Response status code does not indicate success: 401 (Unauthorized).")
				{
					throw new InvalidCredentialsException("User credentials couldn't be authenticated");
				}
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D40 File Offset: 0x00000F40
		protected TModel Get<TModel>(string identifier, string addressSuffix, bool ignoreExceptions = true)
		{
			TModel result;
			try
			{
				result = Task.Run<TModel>(() => this.GetAsync<TModel>(identifier, addressSuffix, ignoreExceptions)).Result;
			}
			catch (HttpRequestException inner)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous query into the rest service. See inner exception for more information", inner);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is HttpRequestException && ex.InnerException.Message == "Response status code does not indicate success: 401 (Unauthorized).")
				{
					throw new InvalidCredentialsException("User credentials couldn't be authenticated");
				}
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DF8 File Offset: 0x00000FF8
		protected TModel Get<TModel>(string[] identifiers, bool ignoreExceptions = true)
		{
			TModel result;
			try
			{
				result = Task.Run<TModel>(() => this.GetAsync<TModel>(identifiers, ignoreExceptions)).Result;
			}
			catch (HttpRequestException inner)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous query into the rest service. See inner exception for more information", inner);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is HttpRequestException && ex.InnerException.Message == "Response status code does not indicate success: 401 (Unauthorized).")
				{
					throw new InvalidCredentialsException("User credentials couldn't be authenticated");
				}
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002EAC File Offset: 0x000010AC
		protected TModel Get<TModel>(string[] identifiers, string addressSuffix, bool ignoreExceptions = true)
		{
			TModel result;
			try
			{
				result = Task.Run<TModel>(() => this.GetAsync<TModel>(identifiers, addressSuffix, ignoreExceptions)).Result;
			}
			catch (HttpRequestException inner)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous query into the rest service. See inner exception for more information", inner);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is HttpRequestException && ex.InnerException.Message == "Response status code does not indicate success: 401 (Unauthorized).")
				{
					throw new InvalidCredentialsException("User credentials couldn't be authenticated");
				}
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F64 File Offset: 0x00001164
		protected TModelResult Post<TModel, TModelResult>(TModel model)
		{
			TModelResult result;
			try
			{
				result = Task.Run<TModelResult>(() => this.PostAsync<TModel, TModelResult>(model)).Result;
			}
			catch (TaskCanceledException inner)
			{
				throw new SecurityException("Post asynchronous operation was timeout. See inner exception for more information", inner);
			}
			catch (WebException inner2)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous Post operation to the rest service. See inner exception for more information", inner2);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002FFC File Offset: 0x000011FC
		protected TModelResult Post<TModel, TModelResult>(TModel model, string addressSuffix)
		{
			TModelResult result;
			try
			{
				result = Task.Run<TModelResult>(() => this.PostAsync<TModel, TModelResult>(model, addressSuffix)).Result;
			}
			catch (TaskCanceledException inner)
			{
				throw new SecurityException("Post asynchronous operation was timeout. See inner exception for more information", inner);
			}
			catch (WebException inner2)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous Post operation to the rest service. See inner exception for more information", inner2);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000309C File Offset: 0x0000129C
		protected TModelResult Post<TModelResult>(string addressSuffix)
		{
			TModelResult result;
			try
			{
				result = Task.Run<TModelResult>(() => this.PostAsync<TModelResult>(addressSuffix)).Result;
			}
			catch (TaskCanceledException inner)
			{
				throw new SecurityException("Post asynchronous operation was timeout. See inner exception for more information", inner);
			}
			catch (WebException inner2)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous Post operation to the rest service. See inner exception for more information", inner2);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003134 File Offset: 0x00001334
		protected void Post<TModel>(TModel model, string addressSuffix)
		{
			try
			{
				Task.Run(() => this.PostAsync<TModel>(model, addressSuffix));
			}
			catch (TaskCanceledException inner)
			{
				throw new SecurityException("Post asynchronous operation was timeout. See inner exception for more information", inner);
			}
			catch (WebException inner2)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous Post operation to the rest service. See inner exception for more information", inner2);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000031CC File Offset: 0x000013CC
		protected void Post(string addressSuffix)
		{
			try
			{
				Task.Run(() => this.PostAsync(addressSuffix));
			}
			catch (TaskCanceledException inner)
			{
				throw new SecurityException("Post asynchronous operation was timeout. See inner exception for more information", inner);
			}
			catch (WebException inner2)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous Post operation to the rest service. See inner exception for more information", inner2);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000325C File Offset: 0x0000145C
		protected void Put<TModel>(string identifier, TModel model, string addressSuffix)
		{
			try
			{
				Task.Run(() => this.PutAsync<TModel>(identifier, model, addressSuffix)).Wait();
			}
			catch (TaskCanceledException inner)
			{
				throw new SecurityException("Put asynchronous operation was timeout. See inner exception for more information", inner);
			}
			catch (WebException inner2)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous Put operation to the rest service. See inner exception for more information", inner2);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032FC File Offset: 0x000014FC
		protected void Put<TModel>(TModel model, string addressSuffix)
		{
			try
			{
				Task.Run(() => this.PutAsync<TModel>(model, addressSuffix)).Wait();
			}
			catch (TaskCanceledException inner)
			{
				throw new SecurityException("Put asynchronous operation was timeout. See inner exception for more information", inner);
			}
			catch (WebException inner2)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous Put operation to the rest service. See inner exception for more information", inner2);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003398 File Offset: 0x00001598
		protected void Delete(string identifier)
		{
			try
			{
				Task.Run(() => this.DeleteAsync(identifier)).Wait();
			}
			catch (TaskCanceledException inner)
			{
				throw new SecurityException("Delete asynchronous operation was timeout. See inner exception for more information", inner);
			}
			catch (WebException inner2)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous Delete operation to the rest service. See inner exception for more information", inner2);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000342C File Offset: 0x0000162C
		protected void Delete(string identifier, string addressSuffix)
		{
			try
			{
				Task.Run(() => this.DeleteAsync(identifier, addressSuffix)).Wait();
			}
			catch (TaskCanceledException inner)
			{
				throw new SecurityException("Delete asynchronous operation was timeout. See inner exception for more information", inner);
			}
			catch (WebException inner2)
			{
				throw new SecurityException("Unhandled exception occurred when executing an asynchronous Delete operation to the rest service. See inner exception for more information", inner2);
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is InvalidCredentialsException)
				{
					throw ex.InnerException;
				}
				throw;
			}
		}

		// Token: 0x04000013 RID: 19
		protected IUserCredentials _userSecCredentials;

		// Token: 0x04000014 RID: 20
		private MediaTypeFormatter _mediaTypeFormatter;
	}
}
