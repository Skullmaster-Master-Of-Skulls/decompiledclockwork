using System;
using System.Globalization;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.IdentityModel.Diagnostics
{
	// Token: 0x020001E5 RID: 485
	internal class DeflateCookieTraceRecord : TraceRecord
	{
		// Token: 0x0600104B RID: 4171 RVA: 0x000461D4 File Offset: 0x000443D4
		public DeflateCookieTraceRecord(int originalSize, int deflatedSize)
		{
			this._originalSize = originalSize;
			this._deflatedSize = deflatedSize;
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x000461EA File Offset: 0x000443EA
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/DeflateCookieTraceRecord";
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000461F4 File Offset: 0x000443F4
		internal override void WriteTo(XmlWriter writer)
		{
			writer.WriteStartElement("DeflateCookieTraceRecord");
			writer.WriteAttributeString("xmlns", this.EventId);
			writer.WriteElementString("OriginalSize", this._originalSize.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("AfterDeflating", this._deflatedSize.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
		}

		// Token: 0x04000E2B RID: 3627
		private const string ElementName = "DeflateCookieTraceRecord";

		// Token: 0x04000E2C RID: 3628
		private const string _eventId = "http://schemas.microsoft.com/2006/08/ServiceModel/DeflateCookieTraceRecord";

		// Token: 0x04000E2D RID: 3629
		private int _originalSize;

		// Token: 0x04000E2E RID: 3630
		private int _deflatedSize;
	}
}
