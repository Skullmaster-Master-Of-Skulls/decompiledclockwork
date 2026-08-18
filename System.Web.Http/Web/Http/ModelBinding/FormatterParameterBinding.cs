using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;
using System.Web.Http.Validation;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000C6 RID: 198
	public class FormatterParameterBinding : HttpParameterBinding
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x0000E71C File Offset: 0x0000C91C
		public FormatterParameterBinding(HttpParameterDescriptor descriptor, IEnumerable<MediaTypeFormatter> formatters, IBodyModelValidator bodyModelValidator) : base(descriptor)
		{
			if (descriptor.IsOptional)
			{
				this._errorMessage = Error.Format(SRResources.OptionalBodyParameterNotSupported, new object[]
				{
					descriptor.Prefix ?? descriptor.ParameterName,
					base.GetType().Name
				});
			}
			this.Formatters = formatters;
			this.BodyModelValidator = bodyModelValidator;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000E77F File Offset: 0x0000C97F
		public override bool WillReadBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000E782 File Offset: 0x0000C982
		public override string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000E78A File Offset: 0x0000C98A
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0000E792 File Offset: 0x0000C992
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._formatters;
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("formatters");
				}
				this._formatters = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000E7A9 File Offset: 0x0000C9A9
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000E7B1 File Offset: 0x0000C9B1
		public IBodyModelValidator BodyModelValidator { get; set; }

		// Token: 0x06000493 RID: 1171 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		public virtual Task<object> ReadContentAsync(HttpRequestMessage request, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
		{
			object obj;
			if (!request.Properties.TryGetValue("MS_FormatterParameterBinding_CancellationToken", out obj))
			{
				obj = CancellationToken.None;
			}
			return this.ReadContentAsync(request, type, formatters, formatterLogger, (CancellationToken)obj);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000E7FC File Offset: 0x0000C9FC
		public virtual Task<object> ReadContentAsync(HttpRequestMessage request, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			HttpContent content = request.Content;
			if (content != null)
			{
				Task<object> result;
				try
				{
					result = content.ReadAsAsync(type, formatters, formatterLogger, cancellationToken);
				}
				catch (UnsupportedMediaTypeException ex)
				{
					string format = (content.Headers.ContentType == null) ? SRResources.UnsupportedMediaTypeNoContentType : SRResources.UnsupportedMediaType;
					throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, Error.Format(format, new object[]
					{
						ex.MediaType.MediaType
					}), ex));
				}
				return result;
			}
			object defaultValueForType = MediaTypeFormatter.GetDefaultValueForType(type);
			if (defaultValueForType == null)
			{
				return TaskHelpers.NullResult();
			}
			return Task.FromResult<object>(defaultValueForType);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000E898 File Offset: 0x0000CA98
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			HttpParameterDescriptor descriptor = base.Descriptor;
			Type parameterType = descriptor.ParameterType;
			HttpRequestMessage request = actionContext.ControllerContext.Request;
			IFormatterLogger formatterLogger = new ModelStateFormatterLogger(actionContext.ModelState, descriptor.ParameterName);
			return this.ExecuteBindingAsyncCore(metadataProvider, actionContext, descriptor, parameterType, request, formatterLogger, cancellationToken);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		private async Task ExecuteBindingAsyncCore(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, HttpParameterDescriptor paramFromBody, Type type, HttpRequestMessage request, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			request.Properties["MS_FormatterParameterBinding_CancellationToken"] = cancellationToken;
			object model = await this.ReadContentAsync(request, type, this._formatters, formatterLogger);
			base.SetValue(actionContext, model);
			if (this.BodyModelValidator != null)
			{
				this.BodyModelValidator.Validate(model, type, metadataProvider, actionContext, paramFromBody.ParameterName);
			}
		}

		// Token: 0x04000159 RID: 345
		private const string CancellationTokenKey = "MS_FormatterParameterBinding_CancellationToken";

		// Token: 0x0400015A RID: 346
		private IEnumerable<MediaTypeFormatter> _formatters;

		// Token: 0x0400015B RID: 347
		private string _errorMessage;
	}
}
