using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200000D RID: 13
	public abstract class MediaTypeFormatter
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000030F4 File Offset: 0x000012F4
		protected MediaTypeFormatter()
		{
			this._supportedMediaTypes = new List<MediaTypeHeaderValue>();
			this.SupportedMediaTypes = new MediaTypeFormatter.MediaTypeHeaderValueCollection(this._supportedMediaTypes);
			this._supportedEncodings = new List<Encoding>();
			this.SupportedEncodings = new Collection<Encoding>(this._supportedEncodings);
			this._mediaTypeMappings = new List<MediaTypeMapping>();
			this.MediaTypeMappings = new Collection<MediaTypeMapping>(this._mediaTypeMappings);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000315C File Offset: 0x0000135C
		protected MediaTypeFormatter(MediaTypeFormatter formatter)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			this._supportedMediaTypes = formatter._supportedMediaTypes;
			this.SupportedMediaTypes = formatter.SupportedMediaTypes;
			this._supportedEncodings = formatter._supportedEncodings;
			this.SupportedEncodings = formatter.SupportedEncodings;
			this._mediaTypeMappings = formatter._mediaTypeMappings;
			this.MediaTypeMappings = formatter.MediaTypeMappings;
			this._requiredMemberSelector = formatter._requiredMemberSelector;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000031D1 File Offset: 0x000013D1
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000031EF File Offset: 0x000013EF
		public static int MaxHttpCollectionKeys
		{
			get
			{
				if (MediaTypeFormatter._maxHttpCollectionKeys < 0)
				{
					MediaTypeFormatter._maxHttpCollectionKeys = MediaTypeFormatter._defaultMaxHttpCollectionKeys.Value;
				}
				return MediaTypeFormatter._maxHttpCollectionKeys;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				MediaTypeFormatter._maxHttpCollectionKeys = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003212 File Offset: 0x00001412
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000321A File Offset: 0x0000141A
		public Collection<MediaTypeHeaderValue> SupportedMediaTypes { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003223 File Offset: 0x00001423
		internal List<MediaTypeHeaderValue> SupportedMediaTypesInternal
		{
			get
			{
				return this._supportedMediaTypes;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000322B File Offset: 0x0000142B
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003233 File Offset: 0x00001433
		public Collection<Encoding> SupportedEncodings { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000323C File Offset: 0x0000143C
		internal List<Encoding> SupportedEncodingsInternal
		{
			get
			{
				return this._supportedEncodings;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003244 File Offset: 0x00001444
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000324C File Offset: 0x0000144C
		public Collection<MediaTypeMapping> MediaTypeMappings { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003255 File Offset: 0x00001455
		internal List<MediaTypeMapping> MediaTypeMappingsInternal
		{
			get
			{
				return this._mediaTypeMappings;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000325D File Offset: 0x0000145D
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003265 File Offset: 0x00001465
		public virtual IRequiredMemberSelector RequiredMemberSelector
		{
			get
			{
				return this._requiredMemberSelector;
			}
			set
			{
				this._requiredMemberSelector = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000326E File Offset: 0x0000146E
		internal virtual bool CanWriteAnyTypes
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003274 File Offset: 0x00001474
		public virtual Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			throw Error.NotSupported(Resources.MediaTypeFormatterCannotRead, new object[]
			{
				base.GetType().Name
			});
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000032A1 File Offset: 0x000014A1
		public virtual Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelpers.Canceled<object>();
			}
			return this.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this.WriteToStreamAsync(type, value, writeStream, content, transportContext, CancellationToken.None);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000032D4 File Offset: 0x000014D4
		public virtual Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			throw Error.NotSupported(Resources.MediaTypeFormatterCannotWrite, new object[]
			{
				base.GetType().Name
			});
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003304 File Offset: 0x00001504
		private static bool TryGetDelegatingType(Type interfaceType, ref Type type)
		{
			if (type != null && type.IsInterface() && type.IsGenericType())
			{
				Type type2 = type.ExtractGenericInterface(interfaceType);
				if (type2 != null)
				{
					type = MediaTypeFormatter.GetOrAddDelegatingType(type, type2);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000334C File Offset: 0x0000154C
		private static int InitializeDefaultCollectionKeySize()
		{
			return int.MaxValue;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003353 File Offset: 0x00001553
		internal static bool TryGetDelegatingTypeForIEnumerableGenericOrSame(ref Type type)
		{
			return MediaTypeFormatter.TryGetDelegatingType(FormattingUtilities.EnumerableInterfaceGenericType, ref type);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003360 File Offset: 0x00001560
		internal static bool TryGetDelegatingTypeForIQueryableGenericOrSame(ref Type type)
		{
			return MediaTypeFormatter.TryGetDelegatingType(FormattingUtilities.QueryableInterfaceGenericType, ref type);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003370 File Offset: 0x00001570
		internal static ConstructorInfo GetTypeRemappingConstructor(Type type)
		{
			ConstructorInfo result;
			MediaTypeFormatter._delegatingEnumerableConstructorCache.TryGetValue(type, out result);
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000338C File Offset: 0x0000158C
		public Encoding SelectCharacterEncoding(HttpContentHeaders contentHeaders)
		{
			Encoding encoding = null;
			if (contentHeaders != null && contentHeaders.ContentType != null)
			{
				string charSet = contentHeaders.ContentType.CharSet;
				if (!string.IsNullOrWhiteSpace(charSet))
				{
					for (int i = 0; i < this._supportedEncodings.Count; i++)
					{
						Encoding encoding2 = this._supportedEncodings[i];
						if (charSet.Equals(encoding2.WebName, StringComparison.OrdinalIgnoreCase))
						{
							encoding = encoding2;
							break;
						}
					}
				}
			}
			if (encoding == null && this._supportedEncodings.Count > 0)
			{
				encoding = this._supportedEncodings[0];
			}
			if (encoding == null)
			{
				throw Error.InvalidOperation(Resources.MediaTypeFormatterNoEncoding, new object[]
				{
					base.GetType().Name
				});
			}
			return encoding;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003438 File Offset: 0x00001638
		public virtual void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			if (mediaType != null)
			{
				headers.ContentType = mediaType.Clone<MediaTypeHeaderValue>();
			}
			if (headers.ContentType == null)
			{
				MediaTypeHeaderValue mediaTypeHeaderValue = null;
				if (this._supportedMediaTypes.Count > 0)
				{
					mediaTypeHeaderValue = this._supportedMediaTypes[0];
				}
				if (mediaTypeHeaderValue != null)
				{
					headers.ContentType = mediaTypeHeaderValue.Clone<MediaTypeHeaderValue>();
				}
			}
			if (headers.ContentType != null && headers.ContentType.CharSet == null)
			{
				Encoding encoding = null;
				if (this._supportedEncodings.Count > 0)
				{
					encoding = this._supportedEncodings[0];
				}
				if (encoding != null)
				{
					headers.ContentType.CharSet = encoding.WebName;
				}
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000034F0 File Offset: 0x000016F0
		public virtual MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return this;
		}

		// Token: 0x06000066 RID: 102
		public abstract bool CanReadType(Type type);

		// Token: 0x06000067 RID: 103
		public abstract bool CanWriteType(Type type);

		// Token: 0x06000068 RID: 104 RVA: 0x00003590 File Offset: 0x00001790
		private static Type GetOrAddDelegatingType(Type type, Type genericType)
		{
			return MediaTypeFormatter._delegatingEnumerableCache.GetOrAdd(type, delegate(Type typeToRemap)
			{
				Type type2 = genericType.GetGenericArguments()[0];
				Type type3 = FormattingUtilities.DelegatingEnumerableGenericType.MakeGenericType(new Type[]
				{
					type2
				});
				ConstructorInfo constructor = type3.GetConstructor(new Type[]
				{
					FormattingUtilities.EnumerableInterfaceGenericType.MakeGenericType(new Type[]
					{
						type2
					})
				});
				MediaTypeFormatter._delegatingEnumerableConstructorCache.TryAdd(type3, constructor);
				return type3;
			});
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000035C1 File Offset: 0x000017C1
		public static object GetDefaultValueForType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (type.IsValueType())
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x04000015 RID: 21
		private const int DefaultMinHttpCollectionKeys = 1;

		// Token: 0x04000016 RID: 22
		private const int DefaultMaxHttpCollectionKeys = 1000;

		// Token: 0x04000017 RID: 23
		private const string IWellKnownComparerTypeName = "System.IWellKnownStringEqualityComparer, mscorlib, Version=4.0.0.0, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000018 RID: 24
		private static readonly ConcurrentDictionary<Type, Type> _delegatingEnumerableCache = new ConcurrentDictionary<Type, Type>();

		// Token: 0x04000019 RID: 25
		private static ConcurrentDictionary<Type, ConstructorInfo> _delegatingEnumerableConstructorCache = new ConcurrentDictionary<Type, ConstructorInfo>();

		// Token: 0x0400001A RID: 26
		private static Lazy<int> _defaultMaxHttpCollectionKeys = new Lazy<int>(new Func<int>(MediaTypeFormatter.InitializeDefaultCollectionKeySize), true);

		// Token: 0x0400001B RID: 27
		private static int _maxHttpCollectionKeys = -1;

		// Token: 0x0400001C RID: 28
		private readonly List<MediaTypeHeaderValue> _supportedMediaTypes;

		// Token: 0x0400001D RID: 29
		private readonly List<Encoding> _supportedEncodings;

		// Token: 0x0400001E RID: 30
		private readonly List<MediaTypeMapping> _mediaTypeMappings;

		// Token: 0x0400001F RID: 31
		private IRequiredMemberSelector _requiredMemberSelector;

		// Token: 0x0200000E RID: 14
		internal class MediaTypeHeaderValueCollection : Collection<MediaTypeHeaderValue>
		{
			// Token: 0x0600006B RID: 107 RVA: 0x0000361A File Offset: 0x0000181A
			internal MediaTypeHeaderValueCollection(IList<MediaTypeHeaderValue> list) : base(list)
			{
			}

			// Token: 0x0600006C RID: 108 RVA: 0x00003623 File Offset: 0x00001823
			protected override void InsertItem(int index, MediaTypeHeaderValue item)
			{
				MediaTypeFormatter.MediaTypeHeaderValueCollection.ValidateMediaType(item);
				base.InsertItem(index, item);
			}

			// Token: 0x0600006D RID: 109 RVA: 0x00003633 File Offset: 0x00001833
			protected override void SetItem(int index, MediaTypeHeaderValue item)
			{
				MediaTypeFormatter.MediaTypeHeaderValueCollection.ValidateMediaType(item);
				base.SetItem(index, item);
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00003644 File Offset: 0x00001844
			private static void ValidateMediaType(MediaTypeHeaderValue item)
			{
				if (item == null)
				{
					throw Error.ArgumentNull("item");
				}
				ParsedMediaTypeHeaderValue parsedMediaTypeHeaderValue = new ParsedMediaTypeHeaderValue(item);
				if (parsedMediaTypeHeaderValue.IsAllMediaRange || parsedMediaTypeHeaderValue.IsSubtypeMediaRange)
				{
					throw Error.Argument("item", Resources.CannotUseMediaRangeForSupportedMediaType, new object[]
					{
						MediaTypeFormatter.MediaTypeHeaderValueCollection._mediaTypeHeaderValueType.Name,
						item.MediaType
					});
				}
			}

			// Token: 0x04000023 RID: 35
			private static readonly Type _mediaTypeHeaderValueType = typeof(MediaTypeHeaderValue);
		}
	}
}
