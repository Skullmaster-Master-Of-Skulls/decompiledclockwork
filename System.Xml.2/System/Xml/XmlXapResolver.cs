using System;
using System.ComponentModel;

namespace System.Xml
{
	// Token: 0x0200009A RID: 154
	[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class XmlXapResolver : XmlResolver
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x0001426B File Offset: 0x0001246B
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public XmlXapResolver()
		{
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00014273 File Offset: 0x00012473
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			throw new XmlException("Xml_XapResolverCannotOpenUri", absoluteUri.ToString(), null, null);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00014287 File Offset: 0x00012487
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterApplicationResourceStreamResolver(IApplicationResourceStreamResolver appStreamResolver)
		{
		}
	}
}
