using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A2B RID: 2603
	internal class PeerHashToken : SecurityToken
	{
		// Token: 0x0600674A RID: 26442 RVA: 0x00181B80 File Offset: 0x0017FD80
		private PeerHashToken()
		{
			this.CheckValidity();
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x00181BD4 File Offset: 0x0017FDD4
		public PeerHashToken(byte[] authenticator)
		{
			this.authenticator = authenticator;
			this.CheckValidity();
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x00181C30 File Offset: 0x0017FE30
		public PeerHashToken(X509Certificate2 certificate, string password)
		{
			this.authenticator = PeerSecurityHelpers.ComputeHash(certificate, password);
			this.CheckValidity();
		}

		// Token: 0x0600674D RID: 26445 RVA: 0x00181C90 File Offset: 0x0017FE90
		public PeerHashToken(Claim claim, string password)
		{
			this.authenticator = PeerSecurityHelpers.ComputeHash(claim, password);
			this.CheckValidity();
		}

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x0600674E RID: 26446 RVA: 0x00181CF0 File Offset: 0x0017FEF0
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x0600674F RID: 26447 RVA: 0x00181CF8 File Offset: 0x0017FEF8
		public override DateTime ValidFrom
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x06006750 RID: 26448 RVA: 0x00181D00 File Offset: 0x0017FF00
		public override DateTime ValidTo
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x06006751 RID: 26449 RVA: 0x00181D08 File Offset: 0x0017FF08
		public static PeerHashToken Invalid
		{
			get
			{
				return PeerHashToken.invalid;
			}
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x06006752 RID: 26450 RVA: 0x00181D0F File Offset: 0x0017FF0F
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				if (this.keys == null)
				{
					this.keys = new ReadOnlyCollection<SecurityKey>(new List<SecurityKey>());
				}
				return this.keys;
			}
		}

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x06006753 RID: 26451 RVA: 0x00181D2F File Offset: 0x0017FF2F
		public Uri Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x06006754 RID: 26452 RVA: 0x00181D37 File Offset: 0x0017FF37
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x06006755 RID: 26453 RVA: 0x00181D40 File Offset: 0x0017FF40
		public bool Validate(Claim claim, string password)
		{
			if (this.authenticator == null)
			{
				throw Fx.AssertAndThrow("Incorrect initialization");
			}
			return PeerSecurityHelpers.Authenticate(claim, password, this.authenticator);
		}

		// Token: 0x06006756 RID: 26454 RVA: 0x00181D6F File Offset: 0x0017FF6F
		private void CheckValidity()
		{
			this.isValid = (this.authenticator != null);
			this.status = new Uri(this.isValid ? "http://schemas.xmlsoap.org/ws/2005/02/trust/status/valid" : "http://schemas.xmlsoap.org/ws/2005/02/trust/status/invalid");
		}

		// Token: 0x06006757 RID: 26455 RVA: 0x00181DA0 File Offset: 0x0017FFA0
		public void Write(XmlWriter writer)
		{
			writer.WriteStartElement("peer", "PeerHashToken", "http://schemas.microsoft.com/net/2006/05/peer");
			writer.WriteStartElement("peer", "Authenticator", "http://schemas.microsoft.com/net/2006/05/peer");
			writer.WriteString(Convert.ToBase64String(this.authenticator));
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x00181DF4 File Offset: 0x0017FFF4
		internal static PeerHashToken CreateFrom(XmlElement child)
		{
			byte[] array = null;
			foreach (object obj in child.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = (XmlElement)xmlNode;
				if (xmlElement != null && PeerRequestSecurityToken.CompareWithNS(xmlElement.LocalName, xmlElement.NamespaceURI, "PeerHashToken", "http://schemas.microsoft.com/net/2006/05/peer"))
				{
					if (xmlElement.ChildNodes.Count != 1)
					{
						break;
					}
					XmlElement xmlElement2 = xmlElement.ChildNodes[0] as XmlElement;
					if (xmlElement2 == null)
					{
						break;
					}
					if (!PeerRequestSecurityToken.CompareWithNS(xmlElement2.LocalName, xmlElement2.NamespaceURI, "Authenticator", "http://schemas.microsoft.com/net/2006/05/peer"))
					{
						break;
					}
					try
					{
						array = Convert.FromBase64String(XmlHelper.ReadTextElementAsTrimmedString(xmlElement2));
						break;
					}
					catch (ArgumentNullException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					catch (FormatException exception2)
					{
						DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
					}
				}
			}
			return new PeerHashToken(array);
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x00181F0C File Offset: 0x0018010C
		public override bool Equals(object token)
		{
			PeerHashToken peerHashToken = token as PeerHashToken;
			if (peerHashToken == null)
			{
				return false;
			}
			if (peerHashToken == this)
			{
				return true;
			}
			if (this.authenticator != null && peerHashToken.authenticator != null && this.authenticator.Length == peerHashToken.authenticator.Length)
			{
				for (int i = 0; i < this.authenticator.Length; i++)
				{
					if (this.authenticator[i] != peerHashToken.authenticator[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x00181F77 File Offset: 0x00180177
		public override int GetHashCode()
		{
			if (!this.isValid)
			{
				return 0;
			}
			return this.authenticator.GetHashCode();
		}

		// Token: 0x04003B49 RID: 15177
		private string id = SecurityUniqueId.Create().Value;

		// Token: 0x04003B4A RID: 15178
		private Uri status;

		// Token: 0x04003B4B RID: 15179
		private bool isValid;

		// Token: 0x04003B4C RID: 15180
		private ReadOnlyCollection<SecurityKey> keys;

		// Token: 0x04003B4D RID: 15181
		internal const string TokenTypeString = "http://schemas.microsoft.com/net/2006/05/peer/peerhashtoken";

		// Token: 0x04003B4E RID: 15182
		internal const string RequestTypeString = "http://schemas.xmlsoap.org/ws/2005/02/trust/Validate";

		// Token: 0x04003B4F RID: 15183
		internal const string Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate";

		// Token: 0x04003B50 RID: 15184
		public const string PeerNamespace = "http://schemas.microsoft.com/net/2006/05/peer";

		// Token: 0x04003B51 RID: 15185
		public const string PeerTokenElementName = "PeerHashToken";

		// Token: 0x04003B52 RID: 15186
		public const string PeerAuthenticatorElementName = "Authenticator";

		// Token: 0x04003B53 RID: 15187
		public const string PeerPrefix = "peer";

		// Token: 0x04003B54 RID: 15188
		private static PeerHashToken invalid = new PeerHashToken();

		// Token: 0x04003B55 RID: 15189
		private byte[] authenticator;

		// Token: 0x04003B56 RID: 15190
		private DateTime effectiveTime = DateTime.UtcNow;

		// Token: 0x04003B57 RID: 15191
		private DateTime expirationTime = DateTime.UtcNow.AddHours(10.0);
	}
}
