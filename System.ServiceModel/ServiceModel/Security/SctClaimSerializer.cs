using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002F9 RID: 761
	internal static class SctClaimSerializer
	{
		// Token: 0x060019C7 RID: 6599 RVA: 0x0005FD80 File Offset: 0x0005DF80
		private static void SerializeSid(SecurityIdentifier sid, SctClaimDictionary dictionary, XmlDictionaryWriter writer)
		{
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			writer.WriteBase64(array, 0, array.Length);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0005FDAC File Offset: 0x0005DFAC
		private static void WriteRightAttribute(Claim claim, SctClaimDictionary dictionary, XmlDictionaryWriter writer)
		{
			if (Rights.PossessProperty.Equals(claim.Right))
			{
				return;
			}
			writer.WriteAttributeString(dictionary.Right, dictionary.EmptyString, claim.Right);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0005FDDC File Offset: 0x0005DFDC
		private static string ReadRightAttribute(XmlDictionaryReader reader, SctClaimDictionary dictionary)
		{
			string attribute = reader.GetAttribute(dictionary.Right, dictionary.EmptyString);
			if (!string.IsNullOrEmpty(attribute))
			{
				return attribute;
			}
			return Rights.PossessProperty;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0005FE0C File Offset: 0x0005E00C
		private static void WriteSidAttribute(SecurityIdentifier sid, SctClaimDictionary dictionary, XmlDictionaryWriter writer)
		{
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			writer.WriteAttributeString(dictionary.Sid, dictionary.EmptyString, Convert.ToBase64String(array));
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0005FE48 File Offset: 0x0005E048
		private static SecurityIdentifier ReadSidAttribute(XmlDictionaryReader reader, SctClaimDictionary dictionary)
		{
			byte[] binaryForm = Convert.FromBase64String(reader.GetAttribute(dictionary.Sid, dictionary.EmptyString));
			return new SecurityIdentifier(binaryForm, 0);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0005FE74 File Offset: 0x0005E074
		public static void SerializeClaim(Claim claim, SctClaimDictionary dictionary, XmlDictionaryWriter writer, XmlObjectSerializer serializer)
		{
			if (claim == null)
			{
				writer.WriteElementString(dictionary.NullValue, dictionary.EmptyString, string.Empty);
				return;
			}
			if (ClaimTypes.Sid.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.WindowsSidClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				SctClaimSerializer.SerializeSid((SecurityIdentifier)claim.Resource, dictionary, writer);
				writer.WriteEndElement();
				return;
			}
			if (ClaimTypes.DenyOnlySid.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.DenyOnlySidClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				SctClaimSerializer.SerializeSid((SecurityIdentifier)claim.Resource, dictionary, writer);
				writer.WriteEndElement();
				return;
			}
			if (ClaimTypes.X500DistinguishedName.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.X500DistinguishedNameClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				byte[] rawData = ((X500DistinguishedName)claim.Resource).RawData;
				writer.WriteBase64(rawData, 0, rawData.Length);
				writer.WriteEndElement();
				return;
			}
			if (ClaimTypes.Thumbprint.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.X509ThumbprintClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				byte[] array = (byte[])claim.Resource;
				writer.WriteBase64(array, 0, array.Length);
				writer.WriteEndElement();
				return;
			}
			if (ClaimTypes.Name.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.NameClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				writer.WriteString((string)claim.Resource);
				writer.WriteEndElement();
				return;
			}
			if (ClaimTypes.Dns.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.DnsClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				writer.WriteString((string)claim.Resource);
				writer.WriteEndElement();
				return;
			}
			if (ClaimTypes.Rsa.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.RsaClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				writer.WriteString(((RSA)claim.Resource).ToXmlString(false));
				writer.WriteEndElement();
				return;
			}
			if (ClaimTypes.Email.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.MailAddressClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				writer.WriteString(((MailAddress)claim.Resource).Address);
				writer.WriteEndElement();
				return;
			}
			if (claim == Claim.System)
			{
				writer.WriteElementString(dictionary.SystemClaim, dictionary.EmptyString, string.Empty);
				return;
			}
			if (ClaimTypes.Hash.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.HashClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				byte[] array2 = (byte[])claim.Resource;
				writer.WriteBase64(array2, 0, array2.Length);
				writer.WriteEndElement();
				return;
			}
			if (ClaimTypes.Spn.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.SpnClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				writer.WriteString((string)claim.Resource);
				writer.WriteEndElement();
				return;
			}
			if (ClaimTypes.Upn.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.UpnClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				writer.WriteString((string)claim.Resource);
				writer.WriteEndElement();
				return;
			}
			if (ClaimTypes.Uri.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(dictionary.UrlClaim, dictionary.EmptyString);
				SctClaimSerializer.WriteRightAttribute(claim, dictionary, writer);
				writer.WriteString(((Uri)claim.Resource).AbsoluteUri);
				writer.WriteEndElement();
				return;
			}
			serializer.WriteObject(writer, claim);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00060220 File Offset: 0x0005E420
		public static void SerializeClaimSet(ClaimSet claimSet, SctClaimDictionary dictionary, XmlDictionaryWriter writer, XmlObjectSerializer serializer, XmlObjectSerializer claimSerializer)
		{
			if (claimSet is X509CertificateClaimSet)
			{
				X509CertificateClaimSet x509CertificateClaimSet = (X509CertificateClaimSet)claimSet;
				writer.WriteStartElement(dictionary.X509CertificateClaimSet, dictionary.EmptyString);
				byte[] rawData = x509CertificateClaimSet.X509Certificate.RawData;
				writer.WriteBase64(rawData, 0, rawData.Length);
				writer.WriteEndElement();
				return;
			}
			if (claimSet == ClaimSet.System)
			{
				writer.WriteElementString(dictionary.SystemClaimSet, dictionary.EmptyString, string.Empty);
				return;
			}
			if (claimSet == ClaimSet.Windows)
			{
				writer.WriteElementString(dictionary.WindowsClaimSet, dictionary.EmptyString, string.Empty);
				return;
			}
			if (claimSet == ClaimSet.Anonymous)
			{
				writer.WriteElementString(dictionary.AnonymousClaimSet, dictionary.EmptyString, string.Empty);
				return;
			}
			if (claimSet is WindowsClaimSet || claimSet is DefaultClaimSet)
			{
				writer.WriteStartElement(dictionary.ClaimSet, dictionary.EmptyString);
				writer.WriteStartElement(dictionary.PrimaryIssuer, dictionary.EmptyString);
				if (claimSet.Issuer == claimSet)
				{
					writer.WriteElementString(dictionary.NullValue, dictionary.EmptyString, string.Empty);
				}
				else
				{
					SctClaimSerializer.SerializeClaimSet(claimSet.Issuer, dictionary, writer, serializer, claimSerializer);
				}
				writer.WriteEndElement();
				foreach (Claim claim in claimSet)
				{
					writer.WriteStartElement(dictionary.Claim, dictionary.EmptyString);
					SctClaimSerializer.SerializeClaim(claim, dictionary, writer, claimSerializer);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
				return;
			}
			serializer.WriteObject(writer, claimSet);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000603A0 File Offset: 0x0005E5A0
		public static Claim DeserializeClaim(XmlDictionaryReader reader, SctClaimDictionary dictionary, XmlObjectSerializer serializer)
		{
			if (reader.IsStartElement(dictionary.NullValue, dictionary.EmptyString))
			{
				reader.ReadElementString();
				return null;
			}
			if (reader.IsStartElement(dictionary.WindowsSidClaim, dictionary.EmptyString))
			{
				string right = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				byte[] binaryForm = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.Sid, new SecurityIdentifier(binaryForm, 0), right);
			}
			if (reader.IsStartElement(dictionary.DenyOnlySidClaim, dictionary.EmptyString))
			{
				string right2 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				byte[] binaryForm2 = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.DenyOnlySid, new SecurityIdentifier(binaryForm2, 0), right2);
			}
			if (reader.IsStartElement(dictionary.X500DistinguishedNameClaim, dictionary.EmptyString))
			{
				string right3 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				byte[] encodedDistinguishedName = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.X500DistinguishedName, new X500DistinguishedName(encodedDistinguishedName), right3);
			}
			if (reader.IsStartElement(dictionary.X509ThumbprintClaim, dictionary.EmptyString))
			{
				string right4 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				byte[] resource = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.Thumbprint, resource, right4);
			}
			if (reader.IsStartElement(dictionary.NameClaim, dictionary.EmptyString))
			{
				string right5 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				string resource2 = reader.ReadString();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.Name, resource2, right5);
			}
			if (reader.IsStartElement(dictionary.DnsClaim, dictionary.EmptyString))
			{
				string right6 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				string resource3 = reader.ReadString();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.Dns, resource3, right6);
			}
			if (reader.IsStartElement(dictionary.RsaClaim, dictionary.EmptyString))
			{
				string right7 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				string xmlString = reader.ReadString();
				reader.ReadEndElement();
				RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
				rsacryptoServiceProvider.FromXmlString(xmlString);
				return new Claim(ClaimTypes.Rsa, rsacryptoServiceProvider, right7);
			}
			if (reader.IsStartElement(dictionary.MailAddressClaim, dictionary.EmptyString))
			{
				string right8 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				string address = reader.ReadString();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.Email, new MailAddress(address), right8);
			}
			if (reader.IsStartElement(dictionary.SystemClaim, dictionary.EmptyString))
			{
				reader.ReadElementString();
				return Claim.System;
			}
			if (reader.IsStartElement(dictionary.HashClaim, dictionary.EmptyString))
			{
				string right9 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				byte[] resource4 = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.Hash, resource4, right9);
			}
			if (reader.IsStartElement(dictionary.SpnClaim, dictionary.EmptyString))
			{
				string right10 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				string resource5 = reader.ReadString();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.Spn, resource5, right10);
			}
			if (reader.IsStartElement(dictionary.UpnClaim, dictionary.EmptyString))
			{
				string right11 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				string resource6 = reader.ReadString();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.Upn, resource6, right11);
			}
			if (reader.IsStartElement(dictionary.UrlClaim, dictionary.EmptyString))
			{
				string right12 = SctClaimSerializer.ReadRightAttribute(reader, dictionary);
				reader.ReadStartElement();
				string uriString = reader.ReadString();
				reader.ReadEndElement();
				return new Claim(ClaimTypes.Uri, new Uri(uriString), right12);
			}
			return (Claim)serializer.ReadObject(reader);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0006071C File Offset: 0x0005E91C
		public static ClaimSet DeserializeClaimSet(XmlDictionaryReader reader, SctClaimDictionary dictionary, XmlObjectSerializer serializer, XmlObjectSerializer claimSerializer)
		{
			if (reader.IsStartElement(dictionary.NullValue, dictionary.EmptyString))
			{
				reader.ReadElementString();
				return null;
			}
			if (reader.IsStartElement(dictionary.X509CertificateClaimSet, dictionary.EmptyString))
			{
				reader.ReadStartElement();
				byte[] rawData = reader.ReadContentAsBase64();
				X509Helper.VerifyNotPfx(rawData);
				reader.ReadEndElement();
				return new X509CertificateClaimSet(new X509Certificate2(rawData), false);
			}
			if (reader.IsStartElement(dictionary.SystemClaimSet, dictionary.EmptyString))
			{
				reader.ReadElementString();
				return ClaimSet.System;
			}
			if (reader.IsStartElement(dictionary.WindowsClaimSet, dictionary.EmptyString))
			{
				reader.ReadElementString();
				return ClaimSet.Windows;
			}
			if (reader.IsStartElement(dictionary.AnonymousClaimSet, dictionary.EmptyString))
			{
				reader.ReadElementString();
				return ClaimSet.Anonymous;
			}
			if (!reader.IsStartElement(dictionary.ClaimSet, dictionary.EmptyString))
			{
				return (ClaimSet)serializer.ReadObject(reader);
			}
			ClaimSet claimSet = null;
			List<Claim> list = new List<Claim>();
			reader.ReadStartElement();
			if (reader.IsStartElement(dictionary.PrimaryIssuer, dictionary.EmptyString))
			{
				reader.ReadStartElement();
				claimSet = SctClaimSerializer.DeserializeClaimSet(reader, dictionary, serializer, claimSerializer);
				reader.ReadEndElement();
			}
			while (reader.IsStartElement())
			{
				reader.ReadStartElement();
				list.Add(SctClaimSerializer.DeserializeClaim(reader, dictionary, claimSerializer));
				reader.ReadEndElement();
			}
			reader.ReadEndElement();
			if (claimSet == null)
			{
				return new DefaultClaimSet(list);
			}
			return new DefaultClaimSet(claimSet, list);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00060878 File Offset: 0x0005EA78
		public static void SerializeIdentities(AuthorizationContext authContext, SctClaimDictionary dictionary, XmlDictionaryWriter writer, XmlObjectSerializer serializer)
		{
			object obj;
			if (authContext.Properties.TryGetValue("Identities", out obj))
			{
				IList<IIdentity> list = obj as IList<IIdentity>;
				if (list != null && list.Count > 0)
				{
					writer.WriteStartElement(dictionary.Identities, dictionary.EmptyString);
					for (int i = 0; i < list.Count; i++)
					{
						SctClaimSerializer.SerializePrimaryIdentity(list[i], dictionary, writer, serializer);
					}
					writer.WriteEndElement();
				}
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000608E4 File Offset: 0x0005EAE4
		private static void SerializePrimaryIdentity(IIdentity identity, SctClaimDictionary dictionary, XmlDictionaryWriter writer, XmlObjectSerializer serializer)
		{
			if (identity != null && identity != SecurityUtils.AnonymousIdentity)
			{
				writer.WriteStartElement(dictionary.PrimaryIdentity, dictionary.EmptyString);
				if (identity is WindowsIdentity)
				{
					WindowsIdentity windowsIdentity = (WindowsIdentity)identity;
					writer.WriteStartElement(dictionary.WindowsSidIdentity, dictionary.EmptyString);
					SctClaimSerializer.WriteSidAttribute(windowsIdentity.User, dictionary, writer);
					string value = null;
					using (WindowsIdentity current = WindowsIdentity.GetCurrent())
					{
						if (current.User == windowsIdentity.Owner || (windowsIdentity.Owner != null && current.Groups.Contains(windowsIdentity.Owner)) || (windowsIdentity.Owner != SecurityUtils.AdministratorsSid && current.Groups.Contains(SecurityUtils.AdministratorsSid)))
						{
							value = windowsIdentity.AuthenticationType;
						}
					}
					if (!string.IsNullOrEmpty(value))
					{
						writer.WriteAttributeString(dictionary.AuthenticationType, dictionary.EmptyString, value);
					}
					writer.WriteString(windowsIdentity.Name);
					writer.WriteEndElement();
				}
				else if (identity is WindowsSidIdentity)
				{
					WindowsSidIdentity windowsSidIdentity = (WindowsSidIdentity)identity;
					writer.WriteStartElement(dictionary.WindowsSidIdentity, dictionary.EmptyString);
					SctClaimSerializer.WriteSidAttribute(windowsSidIdentity.SecurityIdentifier, dictionary, writer);
					if (!string.IsNullOrEmpty(windowsSidIdentity.AuthenticationType))
					{
						writer.WriteAttributeString(dictionary.AuthenticationType, dictionary.EmptyString, windowsSidIdentity.AuthenticationType);
					}
					writer.WriteString(windowsSidIdentity.Name);
					writer.WriteEndElement();
				}
				else if (identity is GenericIdentity)
				{
					GenericIdentity genericIdentity = (GenericIdentity)identity;
					writer.WriteStartElement(dictionary.GenericIdentity, dictionary.EmptyString);
					if (!string.IsNullOrEmpty(genericIdentity.AuthenticationType))
					{
						writer.WriteAttributeString(dictionary.AuthenticationType, dictionary.EmptyString, genericIdentity.AuthenticationType);
					}
					writer.WriteString(genericIdentity.Name);
					writer.WriteEndElement();
				}
				else
				{
					serializer.WriteObject(writer, identity);
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00060AD0 File Offset: 0x0005ECD0
		public static IList<IIdentity> DeserializeIdentities(XmlDictionaryReader reader, SctClaimDictionary dictionary, XmlObjectSerializer serializer)
		{
			List<IIdentity> list = null;
			if (reader.IsStartElement(dictionary.Identities, dictionary.EmptyString))
			{
				list = new List<IIdentity>();
				reader.ReadStartElement();
				while (reader.IsStartElement(dictionary.PrimaryIdentity, dictionary.EmptyString))
				{
					IIdentity identity = SctClaimSerializer.DeserializePrimaryIdentity(reader, dictionary, serializer);
					if (identity != null && identity != SecurityUtils.AnonymousIdentity)
					{
						list.Add(identity);
					}
				}
				reader.ReadEndElement();
			}
			return list;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00060B38 File Offset: 0x0005ED38
		private static IIdentity DeserializePrimaryIdentity(XmlDictionaryReader reader, SctClaimDictionary dictionary, XmlObjectSerializer serializer)
		{
			IIdentity result = null;
			if (reader.IsStartElement(dictionary.PrimaryIdentity, dictionary.EmptyString))
			{
				reader.ReadStartElement();
				if (reader.IsStartElement(dictionary.WindowsSidIdentity, dictionary.EmptyString))
				{
					SecurityIdentifier sid = SctClaimSerializer.ReadSidAttribute(reader, dictionary);
					string attribute = reader.GetAttribute(dictionary.AuthenticationType, dictionary.EmptyString);
					reader.ReadStartElement();
					string name = reader.ReadContentAsString();
					result = new WindowsSidIdentity(sid, name, attribute ?? string.Empty);
					reader.ReadEndElement();
				}
				else if (reader.IsStartElement(dictionary.GenericIdentity, dictionary.EmptyString))
				{
					string attribute2 = reader.GetAttribute(dictionary.AuthenticationType, dictionary.EmptyString);
					reader.ReadStartElement();
					string name2 = reader.ReadContentAsString();
					result = SecurityUtils.CreateIdentity(name2, attribute2 ?? string.Empty);
					reader.ReadEndElement();
				}
				else
				{
					result = (IIdentity)serializer.ReadObject(reader);
				}
				reader.ReadEndElement();
			}
			return result;
		}
	}
}
