using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml.Resolvers
{
	// Token: 0x020002F2 RID: 754
	public class XmlPreloadedResolver : XmlResolver
	{
		// Token: 0x06002D4B RID: 11595 RVA: 0x000EC12E File Offset: 0x000EA32E
		public XmlPreloadedResolver() : this(null)
		{
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000EC137 File Offset: 0x000EA337
		public XmlPreloadedResolver(XmlKnownDtds preloadedDtds) : this(null, preloadedDtds, null)
		{
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000EC142 File Offset: 0x000EA342
		public XmlPreloadedResolver(XmlResolver fallbackResolver) : this(fallbackResolver, XmlKnownDtds.All, null)
		{
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000EC151 File Offset: 0x000EA351
		public XmlPreloadedResolver(XmlResolver fallbackResolver, XmlKnownDtds preloadedDtds) : this(fallbackResolver, preloadedDtds, null)
		{
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000EC15C File Offset: 0x000EA35C
		public XmlPreloadedResolver(XmlResolver fallbackResolver, XmlKnownDtds preloadedDtds, IEqualityComparer<Uri> uriComparer)
		{
			this.fallbackResolver = fallbackResolver;
			this.mappings = new Dictionary<Uri, XmlPreloadedResolver.PreloadedData>(16, uriComparer);
			this.preloadedDtds = preloadedDtds;
			if (preloadedDtds != XmlKnownDtds.None)
			{
				if ((preloadedDtds & XmlKnownDtds.Xhtml10) != XmlKnownDtds.None)
				{
					this.AddKnownDtd(XmlPreloadedResolver.Xhtml10_Dtd);
				}
				if ((preloadedDtds & XmlKnownDtds.Rss091) != XmlKnownDtds.None)
				{
					this.AddKnownDtd(XmlPreloadedResolver.Rss091_Dtd);
				}
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000EC1B0 File Offset: 0x000EA3B0
		public override Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			if (relativeUri != null && relativeUri.StartsWith("-//", StringComparison.CurrentCulture))
			{
				if ((this.preloadedDtds & XmlKnownDtds.Xhtml10) != XmlKnownDtds.None && relativeUri.StartsWith("-//W3C//", StringComparison.CurrentCulture))
				{
					for (int i = 0; i < XmlPreloadedResolver.Xhtml10_Dtd.Length; i++)
					{
						if (relativeUri == XmlPreloadedResolver.Xhtml10_Dtd[i].publicId)
						{
							return new Uri(relativeUri, UriKind.Relative);
						}
					}
				}
				if ((this.preloadedDtds & XmlKnownDtds.Rss091) != XmlKnownDtds.None && relativeUri == XmlPreloadedResolver.Rss091_Dtd[0].publicId)
				{
					return new Uri(relativeUri, UriKind.Relative);
				}
			}
			return base.ResolveUri(baseUri, relativeUri);
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000EC244 File Offset: 0x000EA444
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (absoluteUri == null)
			{
				throw new ArgumentNullException("absoluteUri");
			}
			XmlPreloadedResolver.PreloadedData preloadedData;
			if (!this.mappings.TryGetValue(absoluteUri, out preloadedData))
			{
				if (this.fallbackResolver != null)
				{
					return this.fallbackResolver.GetEntity(absoluteUri, role, ofObjectToReturn);
				}
				throw new XmlException(Res.GetString("Xml_CannotResolveUrl", new object[]
				{
					absoluteUri.ToString()
				}));
			}
			else
			{
				if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
				{
					return preloadedData.AsStream();
				}
				if (ofObjectToReturn == typeof(TextReader))
				{
					return preloadedData.AsTextReader();
				}
				throw new XmlException(Res.GetString("Xml_UnsupportedClass"));
			}
		}

		// Token: 0x170009FD RID: 2557
		// (set) Token: 0x06002D52 RID: 11602 RVA: 0x000EC307 File Offset: 0x000EA507
		public override ICredentials Credentials
		{
			set
			{
				if (this.fallbackResolver != null)
				{
					this.fallbackResolver.Credentials = value;
				}
			}
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000EC320 File Offset: 0x000EA520
		public override bool SupportsType(Uri absoluteUri, Type type)
		{
			if (absoluteUri == null)
			{
				throw new ArgumentNullException("absoluteUri");
			}
			XmlPreloadedResolver.PreloadedData preloadedData;
			if (this.mappings.TryGetValue(absoluteUri, out preloadedData))
			{
				return preloadedData.SupportsType(type);
			}
			if (this.fallbackResolver != null)
			{
				return this.fallbackResolver.SupportsType(absoluteUri, type);
			}
			return base.SupportsType(absoluteUri, type);
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000EC377 File Offset: 0x000EA577
		public void Add(Uri uri, byte[] value)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(value, 0, value.Length));
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000EC3AC File Offset: 0x000EA5AC
		public void Add(Uri uri, byte[] value, int offset, int count)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (value.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(value, offset, count));
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000EC420 File Offset: 0x000EA620
		public void Add(Uri uri, Stream value)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			checked
			{
				if (value.CanSeek)
				{
					int num = (int)value.Length;
					byte[] array = new byte[num];
					value.Read(array, 0, num);
					this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(array));
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				byte[] array2 = new byte[4096];
				int count;
				while ((count = value.Read(array2, 0, array2.Length)) > 0)
				{
					memoryStream.Write(array2, 0, count);
				}
				int num2 = (int)memoryStream.Position;
				byte[] array3 = new byte[num2];
				Array.Copy(memoryStream.GetBuffer(), array3, num2);
				this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(array3));
			}
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000EC4DB File Offset: 0x000EA6DB
		public void Add(Uri uri, string value)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Add(uri, new XmlPreloadedResolver.StringData(value));
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x000EC50C File Offset: 0x000EA70C
		public IEnumerable<Uri> PreloadedUris
		{
			get
			{
				return this.mappings.Keys;
			}
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000EC519 File Offset: 0x000EA719
		public void Remove(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.mappings.Remove(uri);
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000EC53C File Offset: 0x000EA73C
		private void Add(Uri uri, XmlPreloadedResolver.PreloadedData data)
		{
			if (this.mappings.ContainsKey(uri))
			{
				this.mappings[uri] = data;
				return;
			}
			this.mappings.Add(uri, data);
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000EC568 File Offset: 0x000EA768
		private void AddKnownDtd(XmlPreloadedResolver.XmlKnownDtdData[] dtdSet)
		{
			foreach (XmlPreloadedResolver.XmlKnownDtdData xmlKnownDtdData in dtdSet)
			{
				this.mappings.Add(new Uri(xmlKnownDtdData.publicId, UriKind.RelativeOrAbsolute), xmlKnownDtdData);
				this.mappings.Add(new Uri(xmlKnownDtdData.systemId, UriKind.RelativeOrAbsolute), xmlKnownDtdData);
			}
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000EC5B8 File Offset: 0x000EA7B8
		public override Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (absoluteUri == null)
			{
				throw new ArgumentNullException("absoluteUri");
			}
			XmlPreloadedResolver.PreloadedData preloadedData;
			if (!this.mappings.TryGetValue(absoluteUri, out preloadedData))
			{
				if (this.fallbackResolver != null)
				{
					return this.fallbackResolver.GetEntityAsync(absoluteUri, role, ofObjectToReturn);
				}
				throw new XmlException(Res.GetString("Xml_CannotResolveUrl", new object[]
				{
					absoluteUri.ToString()
				}));
			}
			else
			{
				if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
				{
					return Task.FromResult<object>(preloadedData.AsStream());
				}
				if (ofObjectToReturn == typeof(TextReader))
				{
					return Task.FromResult<object>(preloadedData.AsTextReader());
				}
				throw new XmlException(Res.GetString("Xml_UnsupportedClass"));
			}
		}

		// Token: 0x0400138B RID: 5003
		private XmlResolver fallbackResolver;

		// Token: 0x0400138C RID: 5004
		private Dictionary<Uri, XmlPreloadedResolver.PreloadedData> mappings;

		// Token: 0x0400138D RID: 5005
		private XmlKnownDtds preloadedDtds;

		// Token: 0x0400138E RID: 5006
		private static XmlPreloadedResolver.XmlKnownDtdData[] Xhtml10_Dtd = new XmlPreloadedResolver.XmlKnownDtdData[]
		{
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//DTD XHTML 1.0 Strict//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", "xhtml1-strict.dtd"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//DTD XHTML 1.0 Transitional//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd", "xhtml1-transitional.dtd"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//DTD XHTML 1.0 Frameset//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd", "xhtml1-frameset.dtd"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//ENTITIES Latin 1 for XHTML//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml-lat1.ent", "xhtml-lat1.ent"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//ENTITIES Symbols for XHTML//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml-symbol.ent", "xhtml-symbol.ent"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//ENTITIES Special for XHTML//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml-special.ent", "xhtml-special.ent")
		};

		// Token: 0x0400138F RID: 5007
		private static XmlPreloadedResolver.XmlKnownDtdData[] Rss091_Dtd = new XmlPreloadedResolver.XmlKnownDtdData[]
		{
			new XmlPreloadedResolver.XmlKnownDtdData("-//Netscape Communications//DTD RSS 0.91//EN", "http://my.netscape.com/publish/formats/rss-0.91.dtd", "rss-0.91.dtd")
		};

		// Token: 0x020004C1 RID: 1217
		private abstract class PreloadedData
		{
			// Token: 0x060031B0 RID: 12720
			internal abstract Stream AsStream();

			// Token: 0x060031B1 RID: 12721 RVA: 0x00121031 File Offset: 0x0011F231
			internal virtual TextReader AsTextReader()
			{
				throw new XmlException(Res.GetString("Xml_UnsupportedClass"));
			}

			// Token: 0x060031B2 RID: 12722 RVA: 0x00121042 File Offset: 0x0011F242
			internal virtual bool SupportsType(Type type)
			{
				return type == null || type == typeof(Stream);
			}
		}

		// Token: 0x020004C2 RID: 1218
		private class XmlKnownDtdData : XmlPreloadedResolver.PreloadedData
		{
			// Token: 0x060031B4 RID: 12724 RVA: 0x0012106A File Offset: 0x0011F26A
			internal XmlKnownDtdData(string publicId, string systemId, string resourceName)
			{
				this.publicId = publicId;
				this.systemId = systemId;
				this.resourceName = resourceName;
			}

			// Token: 0x060031B5 RID: 12725 RVA: 0x00121088 File Offset: 0x0011F288
			internal override Stream AsStream()
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				return executingAssembly.GetManifestResourceStream(this.resourceName);
			}

			// Token: 0x04001F9D RID: 8093
			internal string publicId;

			// Token: 0x04001F9E RID: 8094
			internal string systemId;

			// Token: 0x04001F9F RID: 8095
			private string resourceName;
		}

		// Token: 0x020004C3 RID: 1219
		private class ByteArrayChunk : XmlPreloadedResolver.PreloadedData
		{
			// Token: 0x060031B6 RID: 12726 RVA: 0x001210A7 File Offset: 0x0011F2A7
			internal ByteArrayChunk(byte[] array) : this(array, 0, array.Length)
			{
			}

			// Token: 0x060031B7 RID: 12727 RVA: 0x001210B4 File Offset: 0x0011F2B4
			internal ByteArrayChunk(byte[] array, int offset, int length)
			{
				this.array = array;
				this.offset = offset;
				this.length = length;
			}

			// Token: 0x060031B8 RID: 12728 RVA: 0x001210D1 File Offset: 0x0011F2D1
			internal override Stream AsStream()
			{
				return new MemoryStream(this.array, this.offset, this.length);
			}

			// Token: 0x04001FA0 RID: 8096
			private byte[] array;

			// Token: 0x04001FA1 RID: 8097
			private int offset;

			// Token: 0x04001FA2 RID: 8098
			private int length;
		}

		// Token: 0x020004C4 RID: 1220
		private class StringData : XmlPreloadedResolver.PreloadedData
		{
			// Token: 0x060031B9 RID: 12729 RVA: 0x001210EA File Offset: 0x0011F2EA
			internal StringData(string str)
			{
				this.str = str;
			}

			// Token: 0x060031BA RID: 12730 RVA: 0x001210F9 File Offset: 0x0011F2F9
			internal override Stream AsStream()
			{
				return new MemoryStream(Encoding.Unicode.GetBytes(this.str));
			}

			// Token: 0x060031BB RID: 12731 RVA: 0x00121110 File Offset: 0x0011F310
			internal override TextReader AsTextReader()
			{
				return new StringReader(this.str);
			}

			// Token: 0x060031BC RID: 12732 RVA: 0x0012111D File Offset: 0x0011F31D
			internal override bool SupportsType(Type type)
			{
				return type == typeof(TextReader) || base.SupportsType(type);
			}

			// Token: 0x04001FA3 RID: 8099
			private string str;
		}
	}
}
