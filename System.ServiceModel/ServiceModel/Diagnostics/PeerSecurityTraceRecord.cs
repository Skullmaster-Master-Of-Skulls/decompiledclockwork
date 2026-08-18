using System;
using System.IdentityModel.Claims;
using System.Runtime.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AA7 RID: 2727
	internal class PeerSecurityTraceRecord : TraceRecord
	{
		// Token: 0x06006C05 RID: 27653 RVA: 0x00193864 File Offset: 0x00191A64
		protected PeerSecurityTraceRecord(string meshId, string remoteAddress, ClaimSet claimSet, Exception exception)
		{
			this.meshId = meshId;
			this.remoteAddress = remoteAddress;
			this.claimSet = claimSet;
			this.exception = exception;
		}

		// Token: 0x06006C06 RID: 27654 RVA: 0x00193889 File Offset: 0x00191A89
		protected PeerSecurityTraceRecord(string meshId, string remoteAddress) : this(meshId, remoteAddress, null, null)
		{
		}

		// Token: 0x06006C07 RID: 27655 RVA: 0x00193898 File Offset: 0x00191A98
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteElementString("MeshId", this.meshId);
			writer.WriteElementString("RemoteAddress", this.remoteAddress);
			PeerSecurityTraceRecord.WriteClaimSet(writer, this.claimSet);
			if (this.exception != null)
			{
				writer.WriteElementString("Exception", this.exception.GetType().ToString() + ":" + this.exception.Message);
			}
		}

		// Token: 0x06006C08 RID: 27656 RVA: 0x00193914 File Offset: 0x00191B14
		internal static void WriteClaimSet(XmlWriter writer, ClaimSet claimSet)
		{
			writer.WriteStartElement("NeighborCredentials");
			if (claimSet != null)
			{
				foreach (Claim claim in claimSet)
				{
					if (claim.ClaimType == ClaimTypes.Name)
					{
						writer.WriteElementString("Name", claim.Resource.ToString());
					}
					else if (claim.ClaimType == ClaimTypes.X500DistinguishedName)
					{
						writer.WriteElementString("X500DistinguishedName", (claim.Resource as X500DistinguishedName).Name.ToString());
					}
					else if (claim.ClaimType == ClaimTypes.Thumbprint)
					{
						writer.WriteElementString("Thumbprint", Convert.ToBase64String(claim.Resource as byte[]));
					}
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x04003EA7 RID: 16039
		protected string meshId;

		// Token: 0x04003EA8 RID: 16040
		protected string remoteAddress;

		// Token: 0x04003EA9 RID: 16041
		protected ClaimSet claimSet;

		// Token: 0x04003EAA RID: 16042
		private Exception exception;
	}
}
