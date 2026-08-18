using System;
using System.Runtime.Diagnostics;
using System.Security.Claims;
using System.Xml;

namespace System.IdentityModel.Diagnostics
{
	// Token: 0x020001E3 RID: 483
	internal class AuthorizeTraceRecord : TraceRecord
	{
		// Token: 0x06001044 RID: 4164 RVA: 0x00045E79 File Offset: 0x00044079
		public AuthorizeTraceRecord(ClaimsPrincipal claimsPrincipal, string url, string action)
		{
			this._claimsPrincipal = claimsPrincipal;
			this._url = url;
			this._action = action;
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x00045E96 File Offset: 0x00044096
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/AuthorizeTraceRecord";
			}
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00045EA0 File Offset: 0x000440A0
		internal override void WriteTo(XmlWriter writer)
		{
			writer.WriteStartElement("AuthorizeTraceRecord");
			writer.WriteAttributeString("xmlns", this.EventId);
			writer.WriteStartElement("Authorize");
			writer.WriteElementString("Url", this._url);
			writer.WriteElementString("Action", this._action);
			writer.WriteStartElement("ClaimsPrincipal");
			writer.WriteAttributeString("Identity.Name", this._claimsPrincipal.Identity.Name);
			foreach (ClaimsIdentity claimsIdentity in this._claimsPrincipal.Identities)
			{
				writer.WriteStartElement("ClaimsIdentity");
				writer.WriteAttributeString("name", claimsIdentity.Name);
				foreach (Claim claim in claimsIdentity.Claims)
				{
					writer.WriteStartElement("Claim");
					writer.WriteAttributeString("Value", claim.Value);
					writer.WriteAttributeString("Type", claim.Type);
					writer.WriteAttributeString("ValueType", claim.ValueType);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x04000E23 RID: 3619
		private const string _elementName = "AuthorizeTraceRecord";

		// Token: 0x04000E24 RID: 3620
		private const string _eventId = "http://schemas.microsoft.com/2006/08/ServiceModel/AuthorizeTraceRecord";

		// Token: 0x04000E25 RID: 3621
		private ClaimsPrincipal _claimsPrincipal;

		// Token: 0x04000E26 RID: 3622
		private string _url;

		// Token: 0x04000E27 RID: 3623
		private string _action;
	}
}
