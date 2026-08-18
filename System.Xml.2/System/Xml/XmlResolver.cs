using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000097 RID: 151
	public abstract class XmlResolver
	{
		// Token: 0x06000555 RID: 1365
		public abstract object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn);

		// Token: 0x06000556 RID: 1366 RVA: 0x00013F70 File Offset: 0x00012170
		public virtual Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			if (baseUri == null || (!baseUri.IsAbsoluteUri && baseUri.OriginalString.Length == 0))
			{
				Uri uri = new Uri(relativeUri, UriKind.RelativeOrAbsolute);
				if (!uri.IsAbsoluteUri && uri.OriginalString.Length > 0)
				{
					uri = new Uri(Path.GetFullPath(relativeUri));
				}
				return uri;
			}
			if (relativeUri == null || relativeUri.Length == 0)
			{
				return baseUri;
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new NotSupportedException(Res.GetString("Xml_RelativeUriNotSupported"));
			}
			return new Uri(baseUri, relativeUri);
		}

		// Token: 0x17000108 RID: 264
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00013FF3 File Offset: 0x000121F3
		public virtual ICredentials Credentials
		{
			set
			{
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00013FF5 File Offset: 0x000121F5
		public virtual bool SupportsType(Uri absoluteUri, Type type)
		{
			if (absoluteUri == null)
			{
				throw new ArgumentNullException("absoluteUri");
			}
			return type == null || type == typeof(Stream);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00014029 File Offset: 0x00012229
		public virtual Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			throw new NotImplementedException();
		}
	}
}
