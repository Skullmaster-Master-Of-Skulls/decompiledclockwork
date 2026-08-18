using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000010 RID: 16
	public class BsonMediaTypeFormatter : BaseJsonMediaTypeFormatter
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003BFA File Offset: 0x00001DFA
		public BsonMediaTypeFormatter()
		{
			base.SupportedMediaTypes.Add(MediaTypeConstants.ApplicationBsonMediaType);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003C12 File Offset: 0x00001E12
		protected BsonMediaTypeFormatter(BsonMediaTypeFormatter formatter) : base(formatter)
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003C1B File Offset: 0x00001E1B
		public static MediaTypeHeaderValue DefaultMediaType
		{
			get
			{
				return MediaTypeConstants.ApplicationBsonMediaType;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003C22 File Offset: 0x00001E22
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003C2A File Offset: 0x00001E2A
		public sealed override int MaxDepth
		{
			get
			{
				return base.MaxDepth;
			}
			set
			{
				base.MaxDepth = value;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003C34 File Offset: 0x00001E34
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
			if (type == typeof(DBNull) && content != null && content.Headers != null && content.Headers.ContentLength == 0L)
			{
				return Task.FromResult<object>(DBNull.Value);
			}
			return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003CBC File Offset: 0x00001EBC
		public override object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
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
			if (!BsonMediaTypeFormatter.IsSimpleType(type) && !(type == typeof(byte[])))
			{
				return base.ReadFromStream(type, readStream, effectiveEncoding, formatterLogger);
			}
			Type type2 = BsonMediaTypeFormatter.OpenDictionaryType.MakeGenericType(new Type[]
			{
				typeof(string),
				type
			});
			IDictionary dictionary = base.ReadFromStream(type2, readStream, effectiveEncoding, formatterLogger) as IDictionary;
			if (dictionary == null)
			{
				throw Error.InvalidOperation(Resources.MediaTypeFormatter_BsonParseError_MissingData, new object[]
				{
					type2.Name
				});
			}
			string text = string.Empty;
			using (IDictionaryEnumerator enumerator = dictionary.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
					if (dictionary.Count == 1 && dictionaryEntry.Key as string == "Value")
					{
						return dictionaryEntry.Value;
					}
					if (dictionaryEntry.Key != null)
					{
						text = dictionaryEntry.Key.ToString();
					}
				}
			}
			throw Error.InvalidOperation(Resources.MediaTypeFormatter_BsonParseError_UnexpectedData, new object[]
			{
				dictionary.Count,
				text
			});
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003E3C File Offset: 0x0000203C
		public override JsonReader CreateJsonReader(Type type, Stream readStream, Encoding effectiveEncoding)
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
			BsonReader bsonReader = new BsonReader(new BinaryReader(readStream, effectiveEncoding));
			try
			{
				bsonReader.ReadRootValueAsArray = (typeof(IEnumerable).IsAssignableFrom(type) && !typeof(IDictionary).IsAssignableFrom(type));
			}
			catch
			{
				((IDisposable)bsonReader).Dispose();
				throw;
			}
			return bsonReader;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003ED0 File Offset: 0x000020D0
		public override void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
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
			if (value == null)
			{
				return;
			}
			if (value == DBNull.Value)
			{
				return;
			}
			Type type2 = value.GetType();
			if (BsonMediaTypeFormatter.IsSimpleType(type2) || type2 == typeof(byte[]))
			{
				Dictionary<string, object> value2 = new Dictionary<string, object>
				{
					{
						"Value",
						value
					}
				};
				base.WriteToStream(typeof(Dictionary<string, object>), value2, writeStream, effectiveEncoding);
				return;
			}
			base.WriteToStream(type, value, writeStream, effectiveEncoding);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003F70 File Offset: 0x00002170
		public override JsonWriter CreateJsonWriter(Type type, Stream writeStream, Encoding effectiveEncoding)
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
			return new BsonWriter(new BinaryWriter(writeStream, effectiveEncoding));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003FB0 File Offset: 0x000021B0
		private static bool IsSimpleType(Type type)
		{
			return TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
		}

		// Token: 0x04000027 RID: 39
		private static readonly Type OpenDictionaryType = typeof(Dictionary<, >);
	}
}
