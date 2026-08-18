using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000D6 RID: 214
	public class JQueryMvcFormUrlEncodedFormatter : FormUrlEncodedMediaTypeFormatter
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x00010E3F File Offset: 0x0000F03F
		public JQueryMvcFormUrlEncodedFormatter()
		{
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00010E47 File Offset: 0x0000F047
		public JQueryMvcFormUrlEncodedFormatter(HttpConfiguration config)
		{
			this._configuration = config;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00010E56 File Offset: 0x0000F056
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return true;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00010E70 File Offset: 0x0000F070
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (readStream == null)
			{
				throw new ArgumentNullException("readStream");
			}
			if (base.CanReadType(type))
			{
				return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
			}
			return this.ReadFromStreamAsyncCore(type, readStream, content, formatterLogger);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00011050 File Offset: 0x0000F250
		private async Task<object> ReadFromStreamAsyncCore(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			object obj = await base.ReadFromStreamAsync(typeof(FormDataCollection), readStream, content, formatterLogger);
			FormDataCollection fd = (FormDataCollection)obj;
			object result;
			try
			{
				result = fd.ReadAs(type, string.Empty, this.RequiredMemberSelector, formatterLogger, this._configuration);
			}
			catch (Exception exception)
			{
				if (formatterLogger == null)
				{
					throw;
				}
				formatterLogger.LogError(string.Empty, exception);
				result = MediaTypeFormatter.GetDefaultValueForType(type);
			}
			return result;
		}

		// Token: 0x04000183 RID: 387
		private readonly HttpConfiguration _configuration;
	}
}
