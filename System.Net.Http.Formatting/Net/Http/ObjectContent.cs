using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000020 RID: 32
	public class ObjectContent : HttpContent
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00004FB0 File Offset: 0x000031B0
		public ObjectContent(Type type, object value, MediaTypeFormatter formatter) : this(type, value, formatter, null)
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004FBC File Offset: 0x000031BC
		public ObjectContent(Type type, object value, MediaTypeFormatter formatter, string mediaType) : this(type, value, formatter, ObjectContent.BuildHeaderValue(mediaType))
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004FD0 File Offset: 0x000031D0
		public ObjectContent(Type type, object value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			if (!formatter.CanWriteType(type))
			{
				throw Error.InvalidOperation(Resources.ObjectContent_FormatterCannotWriteType, new object[]
				{
					formatter.GetType().FullName,
					type.Name
				});
			}
			this._formatter = formatter;
			this.ObjectType = type;
			this.VerifyAndSetObject(value);
			this._formatter.SetDefaultContentHeaders(type, base.Headers, mediaType);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005061 File Offset: 0x00003261
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00005069 File Offset: 0x00003269
		public Type ObjectType { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005072 File Offset: 0x00003272
		public MediaTypeFormatter Formatter
		{
			get
			{
				return this._formatter;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000507A File Offset: 0x0000327A
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00005082 File Offset: 0x00003282
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000508B File Offset: 0x0000328B
		internal static MediaTypeHeaderValue BuildHeaderValue(string mediaType)
		{
			if (mediaType == null)
			{
				return null;
			}
			return new MediaTypeHeaderValue(mediaType);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005098 File Offset: 0x00003298
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			return this._formatter.WriteToStreamAsync(this.ObjectType, this.Value, stream, this, context);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000050B4 File Offset: 0x000032B4
		protected override bool TryComputeLength(out long length)
		{
			length = -1L;
			return false;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000050BB File Offset: 0x000032BB
		private static bool IsTypeNullable(Type type)
		{
			return !type.IsValueType() || (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000050E8 File Offset: 0x000032E8
		private void VerifyAndSetObject(object value)
		{
			if (value == null)
			{
				if (!ObjectContent.IsTypeNullable(this.ObjectType))
				{
					throw Error.InvalidOperation(Resources.CannotUseNullValueType, new object[]
					{
						typeof(ObjectContent).Name,
						this.ObjectType.Name
					});
				}
			}
			else
			{
				Type type = value.GetType();
				if (!this.ObjectType.IsAssignableFrom(type))
				{
					throw Error.Argument("value", Resources.ObjectAndTypeDisagree, new object[]
					{
						type.Name,
						this.ObjectType.Name
					});
				}
			}
			this._value = value;
		}

		// Token: 0x04000048 RID: 72
		private object _value;

		// Token: 0x04000049 RID: 73
		private readonly MediaTypeFormatter _formatter;
	}
}
