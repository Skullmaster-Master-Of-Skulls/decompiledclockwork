using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200000F RID: 15
	public abstract class BaseJsonMediaTypeFormatter : MediaTypeFormatter
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000036B8 File Offset: 0x000018B8
		protected BaseJsonMediaTypeFormatter()
		{
			this._defaultContractResolver = new JsonContractResolver(this);
			this._jsonSerializerSettings = this.CreateDefaultSerializerSettings();
			base.SupportedEncodings.Add(new UTF8Encoding(false, true));
			base.SupportedEncodings.Add(new UnicodeEncoding(false, true, true));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003713 File Offset: 0x00001913
		protected BaseJsonMediaTypeFormatter(BaseJsonMediaTypeFormatter formatter) : base(formatter)
		{
			this.SerializerSettings = formatter.SerializerSettings;
			this.MaxDepth = formatter._maxDepth;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000373F File Offset: 0x0000193F
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003747 File Offset: 0x00001947
		public JsonSerializerSettings SerializerSettings
		{
			get
			{
				return this._jsonSerializerSettings;
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._jsonSerializerSettings = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000375E File Offset: 0x0000195E
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003766 File Offset: 0x00001966
		public virtual int MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				this._maxDepth = value;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000378C File Offset: 0x0000198C
		public JsonSerializerSettings CreateDefaultSerializerSettings()
		{
			return new JsonSerializerSettings
			{
				ContractResolver = this._defaultContractResolver,
				MissingMemberHandling = MissingMemberHandling.Ignore,
				TypeNameHandling = TypeNameHandling.None
			};
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000037BA File Offset: 0x000019BA
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return true;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000037D1 File Offset: 0x000019D1
		public override bool CanWriteType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000037E8 File Offset: 0x000019E8
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			Task<object> result;
			try
			{
				result = Task.FromResult<object>(this.ReadFromStream(type, readStream, content, formatterLogger));
			}
			catch (Exception exception)
			{
				result = TaskHelpers.FromError<object>(exception);
			}
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003848 File Offset: 0x00001A48
		private object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			HttpContentHeaders httpContentHeaders = (content == null) ? null : content.Headers;
			if (httpContentHeaders != null && httpContentHeaders.ContentLength == 0L)
			{
				return MediaTypeFormatter.GetDefaultValueForType(type);
			}
			Encoding effectiveEncoding = base.SelectCharacterEncoding(httpContentHeaders);
			object result;
			try
			{
				result = this.ReadFromStream(type, readStream, effectiveEncoding, formatterLogger);
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

		// Token: 0x0600007B RID: 123 RVA: 0x00003914 File Offset: 0x00001B14
		public virtual object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			if (effectiveEncoding == null)
			{
				throw Error.ArgumentNull("effectiveEncoding");
			}
			object result;
			using (JsonReader jsonReader = this.CreateJsonReaderInternal(type, readStream, effectiveEncoding))
			{
				jsonReader.CloseInput = false;
				jsonReader.MaxDepth = new int?(this._maxDepth);
				JsonSerializer jsonSerializer = this.CreateJsonSerializerInternal();
				EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> eventHandler = null;
				if (formatterLogger != null)
				{
					eventHandler = delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
					{
						Exception error = e.ErrorContext.Error;
						formatterLogger.LogError(e.ErrorContext.Path, error);
						e.ErrorContext.Handled = true;
					};
					jsonSerializer.Error += eventHandler;
				}
				try
				{
					result = jsonSerializer.Deserialize(jsonReader, type);
				}
				finally
				{
					if (eventHandler != null)
					{
						jsonSerializer.Error -= eventHandler;
					}
				}
			}
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000039F0 File Offset: 0x00001BF0
		private JsonReader CreateJsonReaderInternal(Type type, Stream readStream, Encoding effectiveEncoding)
		{
			JsonReader jsonReader = this.CreateJsonReader(type, readStream, effectiveEncoding);
			if (jsonReader == null)
			{
				throw Error.InvalidOperation(Resources.MediaTypeFormatter_JsonReaderFactoryReturnedNull, new object[]
				{
					"CreateJsonReader"
				});
			}
			return jsonReader;
		}

		// Token: 0x0600007D RID: 125
		public abstract JsonReader CreateJsonReader(Type type, Stream readStream, Encoding effectiveEncoding);

		// Token: 0x0600007E RID: 126 RVA: 0x00003A28 File Offset: 0x00001C28
		private JsonSerializer CreateJsonSerializerInternal()
		{
			JsonSerializer jsonSerializer = null;
			try
			{
				jsonSerializer = this.CreateJsonSerializer();
			}
			catch (Exception innerException)
			{
				throw Error.InvalidOperation(innerException, Resources.JsonSerializerFactoryThrew, new object[]
				{
					"CreateJsonSerializer"
				});
			}
			if (jsonSerializer == null)
			{
				throw Error.InvalidOperation(Resources.JsonSerializerFactoryReturnedNull, new object[]
				{
					"CreateJsonSerializer"
				});
			}
			return jsonSerializer;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003A8C File Offset: 0x00001C8C
		public virtual JsonSerializer CreateJsonSerializer()
		{
			return JsonSerializer.Create(this.SerializerSettings);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (writeStream == null)
			{
				throw Error.ArgumentNull("writeStream");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelpers.Canceled();
			}
			Task result;
			try
			{
				this.WriteToStream(type, value, writeStream, content);
				result = TaskHelpers.Completed();
			}
			catch (Exception exception)
			{
				result = TaskHelpers.FromError(exception);
			}
			return result;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003B14 File Offset: 0x00001D14
		private void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			Encoding effectiveEncoding = base.SelectCharacterEncoding((content == null) ? null : content.Headers);
			this.WriteToStream(type, value, writeStream, effectiveEncoding);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003B40 File Offset: 0x00001D40
		public virtual void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (writeStream == null)
			{
				throw Error.ArgumentNull("writeStream");
			}
			if (effectiveEncoding == null)
			{
				throw Error.ArgumentNull("effectiveEncoding");
			}
			using (JsonWriter jsonWriter = this.CreateJsonWriterInternal(type, writeStream, effectiveEncoding))
			{
				jsonWriter.CloseOutput = false;
				JsonSerializer jsonSerializer = this.CreateJsonSerializerInternal();
				jsonSerializer.Serialize(jsonWriter, value);
				jsonWriter.Flush();
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003BC4 File Offset: 0x00001DC4
		private JsonWriter CreateJsonWriterInternal(Type type, Stream writeStream, Encoding effectiveEncoding)
		{
			JsonWriter jsonWriter = this.CreateJsonWriter(type, writeStream, effectiveEncoding);
			if (jsonWriter == null)
			{
				throw Error.InvalidOperation(Resources.MediaTypeFormatter_JsonWriterFactoryReturnedNull, new object[]
				{
					"CreateJsonWriter"
				});
			}
			return jsonWriter;
		}

		// Token: 0x06000084 RID: 132
		public abstract JsonWriter CreateJsonWriter(Type type, Stream writeStream, Encoding effectiveEncoding);

		// Token: 0x04000024 RID: 36
		private int _maxDepth = 256;

		// Token: 0x04000025 RID: 37
		private readonly IContractResolver _defaultContractResolver;

		// Token: 0x04000026 RID: 38
		private JsonSerializerSettings _jsonSerializerSettings;
	}
}
