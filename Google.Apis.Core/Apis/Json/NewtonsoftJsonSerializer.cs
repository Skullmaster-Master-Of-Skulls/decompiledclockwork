using System;
using System.IO;
using Newtonsoft.Json;

namespace Google.Apis.Json
{
	// Token: 0x02000023 RID: 35
	public class NewtonsoftJsonSerializer : IJsonSerializer, ISerializer
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003761 File Offset: 0x00001961
		public static NewtonsoftJsonSerializer Instance
		{
			get
			{
				return NewtonsoftJsonSerializer.instance = (NewtonsoftJsonSerializer.instance ?? new NewtonsoftJsonSerializer());
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000037CC File Offset: 0x000019CC
		public string Format
		{
			get
			{
				return "json";
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000037D4 File Offset: 0x000019D4
		public void Serialize(object obj, Stream target)
		{
			using (StreamWriter streamWriter = new StreamWriter(target))
			{
				if (obj == null)
				{
					obj = string.Empty;
				}
				NewtonsoftJsonSerializer.newtonsoftSerializer.Serialize(streamWriter, obj);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000381C File Offset: 0x00001A1C
		public string Serialize(object obj)
		{
			string result;
			using (TextWriter textWriter = new StringWriter())
			{
				if (obj == null)
				{
					obj = string.Empty;
				}
				NewtonsoftJsonSerializer.newtonsoftSerializer.Serialize(textWriter, obj);
				result = textWriter.ToString();
			}
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000386C File Offset: 0x00001A6C
		public T Deserialize<T>(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return default(T);
			}
			return JsonConvert.DeserializeObject<T>(input, NewtonsoftJsonSerializer.newtonsoftSettings);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003896 File Offset: 0x00001A96
		public object Deserialize(string input, Type type)
		{
			if (string.IsNullOrEmpty(input))
			{
				return null;
			}
			return JsonConvert.DeserializeObject(input, type, NewtonsoftJsonSerializer.newtonsoftSettings);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000038B0 File Offset: 0x00001AB0
		public T Deserialize<T>(Stream input)
		{
			T result;
			using (StreamReader streamReader = new StreamReader(input))
			{
				result = (T)((object)NewtonsoftJsonSerializer.newtonsoftSerializer.Deserialize(streamReader, typeof(T)));
			}
			return result;
		}

		// Token: 0x0400003A RID: 58
		private static readonly JsonSerializerSettings newtonsoftSettings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore,
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			Converters = 
			{
				new RFC3339DateTimeConverter(),
				new ExplicitNullConverter()
			}
		};

		// Token: 0x0400003B RID: 59
		private static readonly JsonSerializer newtonsoftSerializer = JsonSerializer.Create(NewtonsoftJsonSerializer.newtonsoftSettings);

		// Token: 0x0400003C RID: 60
		private static NewtonsoftJsonSerializer instance;
	}
}
