using System;
using System.Runtime.Diagnostics;
using System.Security.Claims;
using System.Xml;

namespace System.IdentityModel.Diagnostics
{
	// Token: 0x020001E4 RID: 484
	internal class ClaimsPrincipalTraceRecord : TraceRecord
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x00046010 File Offset: 0x00044210
		public ClaimsPrincipalTraceRecord(ClaimsPrincipal claimsPrincipal)
		{
			this._claimsPrincipal = claimsPrincipal;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0004601F File Offset: 0x0004421F
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ClaimsPrincipalTraceRecord";
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00046028 File Offset: 0x00044228
		internal override void WriteTo(XmlWriter writer)
		{
			writer.WriteStartElement("ClaimsPrincipalTraceRecord");
			writer.WriteAttributeString("xmlns", this.EventId);
			writer.WriteStartElement("ClaimsPrincipal");
			writer.WriteAttributeString("Identity.Name", this._claimsPrincipal.Identity.Name);
			foreach (ClaimsIdentity ci in this._claimsPrincipal.Identities)
			{
				this.WriteClaimsIdentity(ci, writer);
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000460CC File Offset: 0x000442CC
		private void WriteClaimsIdentity(ClaimsIdentity ci, XmlWriter writer)
		{
			writer.WriteStartElement("ClaimsIdentity");
			writer.WriteAttributeString("Name", ci.Name);
			writer.WriteAttributeString("NameClaimType", ci.NameClaimType);
			writer.WriteAttributeString("RoleClaimType", ci.RoleClaimType);
			writer.WriteAttributeString("Label", ci.Label);
			if (ci.Actor != null)
			{
				writer.WriteStartElement("Actor");
				this.WriteClaimsIdentity(ci.Actor, writer);
				writer.WriteEndElement();
			}
			foreach (Claim claim in ci.Claims)
			{
				writer.WriteStartElement("Claim");
				writer.WriteAttributeString("Value", claim.Value);
				writer.WriteAttributeString("Type", claim.Type);
				writer.WriteAttributeString("ValueType", claim.ValueType);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000E28 RID: 3624
		internal const string ElementName = "ClaimsPrincipalTraceRecord";

		// Token: 0x04000E29 RID: 3625
		internal const string _eventId = "http://schemas.microsoft.com/2006/08/ServiceModel/ClaimsPrincipalTraceRecord";

		// Token: 0x04000E2A RID: 3626
		private ClaimsPrincipal _claimsPrincipal;
	}
}
