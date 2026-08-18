using System;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000787 RID: 1927
	internal class UriTraceRecord : TraceRecord
	{
		// Token: 0x06004991 RID: 18833 RVA: 0x0010E9AB File Offset: 0x0010CBAB
		public UriTraceRecord(Uri uri)
		{
			this.uri = uri;
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x0010E9BA File Offset: 0x0010CBBA
		internal override void WriteTo(XmlWriter xml)
		{
			xml.WriteElementString("Uri", this.uri.AbsoluteUri);
		}

		// Token: 0x04002E3D RID: 11837
		private Uri uri;
	}
}
