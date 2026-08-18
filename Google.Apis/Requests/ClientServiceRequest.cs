using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Discovery;
using Google.Apis.Logging;
using Google.Apis.Requests.Parameters;
using Google.Apis.Services;
using Google.Apis.Testing;
using Google.Apis.Util;

namespace Google.Apis.Requests
{
	// Token: 0x02000013 RID: 19
	public abstract class ClientServiceRequest<TResponse> : IClientServiceRequest<TResponse>, IClientServiceRequest
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003575 File Offset: 0x00001775
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000357D File Offset: 0x0000177D
		public ETagAction ETagAction { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003586 File Offset: 0x00001786
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000358E File Offset: 0x0000178E
		public Action<HttpRequestMessage> ModifyRequest { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B2 RID: 178
		public abstract string MethodName { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B3 RID: 179
		public abstract string RestPath { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B4 RID: 180
		public abstract string HttpMethod { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003597 File Offset: 0x00001797
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000359F File Offset: 0x0000179F
		public IDictionary<string, IParameter> RequestParameters { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000035A8 File Offset: 0x000017A8
		public IClientService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000035B0 File Offset: 0x000017B0
		protected ClientServiceRequest(IClientService service)
		{
			this.service = service;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000035BF File Offset: 0x000017BF
		protected virtual void InitParameters()
		{
			this.RequestParameters = new Dictionary<string, IParameter>();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000035CC File Offset: 0x000017CC
		public TResponse Execute()
		{
			TResponse result2;
			try
			{
				using (HttpResponseMessage result = this.ExecuteUnparsedAsync(CancellationToken.None).Result)
				{
					result2 = this.ParseResponse(result).Result;
				}
			}
			catch (AggregateException ex)
			{
				throw ex.InnerException;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
			return result2;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003634 File Offset: 0x00001834
		public Stream ExecuteAsStream()
		{
			Stream result;
			try
			{
				result = this.ExecuteUnparsedAsync(CancellationToken.None).Result.Content.ReadAsStreamAsync().Result;
			}
			catch (AggregateException ex)
			{
				throw ex.InnerException;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003688 File Offset: 0x00001888
		public async Task<TResponse> ExecuteAsync()
		{
			return await this.ExecuteAsync(CancellationToken.None).ConfigureAwait(false);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000036D0 File Offset: 0x000018D0
		public async Task<TResponse> ExecuteAsync(CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponseMessage = await this.ExecuteUnparsedAsync(cancellationToken).ConfigureAwait(false);
			TResponse result;
			using (HttpResponseMessage response = httpResponseMessage)
			{
				cancellationToken.ThrowIfCancellationRequested();
				result = await this.ParseResponse(response).ConfigureAwait(false);
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003720 File Offset: 0x00001920
		public async Task<Stream> ExecuteAsStreamAsync()
		{
			return await this.ExecuteAsStreamAsync(CancellationToken.None).ConfigureAwait(false);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003768 File Offset: 0x00001968
		public async Task<Stream> ExecuteAsStreamAsync(CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponseMessage = await this.ExecuteUnparsedAsync(cancellationToken).ConfigureAwait(false);
			cancellationToken.ThrowIfCancellationRequested();
			return await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000037B8 File Offset: 0x000019B8
		private async Task<HttpResponseMessage> ExecuteUnparsedAsync(CancellationToken cancellationToken)
		{
			HttpResponseMessage result;
			using (HttpRequestMessage request = this.CreateRequest(null))
			{
				result = await this.service.HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
			}
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003808 File Offset: 0x00001A08
		private async Task<TResponse> ParseResponse(HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode)
			{
				return await this.service.DeserializeResponse<TResponse>(response).ConfigureAwait(false);
			}
			RequestError requestError = await this.service.DeserializeError(response).ConfigureAwait(false);
			throw new GoogleApiException(this.service.Name, requestError.ToString())
			{
				Error = requestError,
				HttpStatusCode = response.StatusCode
			};
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003858 File Offset: 0x00001A58
		public HttpRequestMessage CreateRequest(bool? overrideGZipEnabled = null)
		{
			HttpRequestMessage httpRequestMessage = this.CreateBuilder().CreateRequest();
			object body = this.GetBody();
			httpRequestMessage.SetRequestSerailizedContent(this.service, body, (overrideGZipEnabled != null) ? overrideGZipEnabled.Value : this.service.GZipEnabled);
			this.AddETag(httpRequestMessage);
			Action<HttpRequestMessage> modifyRequest = this.ModifyRequest;
			if (modifyRequest != null)
			{
				modifyRequest(httpRequestMessage);
			}
			return httpRequestMessage;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000038BC File Offset: 0x00001ABC
		private RequestBuilder CreateBuilder()
		{
			RequestBuilder requestBuilder = new RequestBuilder
			{
				BaseUri = new Uri(this.Service.BaseUri),
				Path = this.RestPath,
				Method = this.HttpMethod
			};
			if (this.service.ApiKey != null)
			{
				requestBuilder.AddParameter(RequestParameterType.Query, "key", this.service.ApiKey);
			}
			IDictionary<string, object> dictionary = ParameterUtils.CreateParameterDictionary(this);
			this.AddParameters(requestBuilder, ParameterCollection.FromDictionary(dictionary));
			return requestBuilder;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003936 File Offset: 0x00001B36
		protected string GenerateRequestUri()
		{
			return this.CreateBuilder().BuildUri().ToString();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003141 File Offset: 0x00001341
		protected virtual object GetBody()
		{
			return null;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003948 File Offset: 0x00001B48
		private void AddETag(HttpRequestMessage request)
		{
			IDirectResponseSchema directResponseSchema = this.GetBody() as IDirectResponseSchema;
			if (directResponseSchema != null && !string.IsNullOrEmpty(directResponseSchema.ETag))
			{
				string etag = directResponseSchema.ETag;
				ETagAction etagAction = (this.ETagAction == ETagAction.Default) ? ClientServiceRequest<TResponse>.GetDefaultETagAction(this.HttpMethod) : this.ETagAction;
				try
				{
					if (etagAction != ETagAction.IfMatch)
					{
						if (etagAction == ETagAction.IfNoneMatch)
						{
							request.Headers.IfNoneMatch.Add(new EntityTagHeaderValue(etag));
						}
					}
					else
					{
						request.Headers.IfMatch.Add(new EntityTagHeaderValue(etag));
					}
				}
				catch (FormatException exception)
				{
					ClientServiceRequest<TResponse>.Logger.Error(exception, "Can't set {0}. Etag is: {1}.", new object[]
					{
						etagAction,
						etag
					});
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003A0C File Offset: 0x00001C0C
		[VisibleForTestOnly]
		public static ETagAction GetDefaultETagAction(string httpMethod)
		{
			if (httpMethod == "GET")
			{
				return ETagAction.IfNoneMatch;
			}
			if (!(httpMethod == "PUT") && !(httpMethod == "POST") && !(httpMethod == "PATCH") && !(httpMethod == "DELETE"))
			{
				return ETagAction.Ignore;
			}
			return ETagAction.IfMatch;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003A64 File Offset: 0x00001C64
		private void AddParameters(RequestBuilder requestBuilder, ParameterCollection inputParameters)
		{
			foreach (KeyValuePair<string, string> keyValuePair in inputParameters)
			{
				IParameter parameter;
				if (!this.RequestParameters.TryGetValue(keyValuePair.Key, out parameter))
				{
					throw new GoogleApiException(this.Service.Name, string.Format("Invalid parameter \"{0}\" was specified", keyValuePair.Key));
				}
				string text = keyValuePair.Value;
				if (!ParameterValidator.ValidateParameter(parameter, text))
				{
					throw new GoogleApiException(this.Service.Name, string.Format("Parameter validation failed for \"{0}\"", parameter.Name));
				}
				if (text == null)
				{
					text = parameter.DefaultValue;
				}
				string parameterType = parameter.ParameterType;
				if (!(parameterType == "path"))
				{
					if (!(parameterType == "query"))
					{
						throw new GoogleApiException(this.service.Name, string.Format("Unsupported parameter type \"{0}\" for \"{1}\"", parameter.ParameterType, parameter.Name));
					}
					if (!object.Equals(text, parameter.DefaultValue) || parameter.IsRequired)
					{
						requestBuilder.AddParameter(RequestParameterType.Query, keyValuePair.Key, text);
					}
				}
				else
				{
					requestBuilder.AddParameter(RequestParameterType.Path, keyValuePair.Key, text);
				}
			}
			foreach (IParameter parameter2 in this.RequestParameters.Values)
			{
				if (parameter2.IsRequired && !inputParameters.ContainsKey(parameter2.Name))
				{
					throw new GoogleApiException(this.service.Name, string.Format("Parameter \"{0}\" is missing", parameter2.Name));
				}
			}
		}

		// Token: 0x0400004B RID: 75
		private static readonly ILogger Logger = ApplicationContext.Logger.ForType<ClientServiceRequest<TResponse>>();

		// Token: 0x0400004C RID: 76
		private readonly IClientService service;
	}
}
