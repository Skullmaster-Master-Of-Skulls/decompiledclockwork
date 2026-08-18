using System;
using System.IdentityModel.Tokens;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Claims;

namespace Microsoft.Owin.Security.DataHandler.Serializer
{
	// Token: 0x02000013 RID: 19
	public class TicketSerializer : IDataSerializer<AuthenticationTicket>
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000026C4 File Offset: 0x000008C4
		public virtual byte[] Serialize(AuthenticationTicket model)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(gzipStream))
					{
						TicketSerializer.Write(binaryWriter, model);
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002744 File Offset: 0x00000944
		public virtual AuthenticationTicket Deserialize(byte[] data)
		{
			AuthenticationTicket result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					using (BinaryReader binaryReader = new BinaryReader(gzipStream))
					{
						result = TicketSerializer.Read(binaryReader);
					}
				}
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027B8 File Offset: 0x000009B8
		public static void Write(BinaryWriter writer, AuthenticationTicket model)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			writer.Write(3);
			ClaimsIdentity identity = model.Identity;
			writer.Write(identity.AuthenticationType);
			TicketSerializer.WriteWithDefault(writer, identity.NameClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
			TicketSerializer.WriteWithDefault(writer, identity.RoleClaimType, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
			writer.Write(identity.Claims.Count<Claim>());
			foreach (Claim claim in identity.Claims)
			{
				TicketSerializer.WriteWithDefault(writer, claim.Type, identity.NameClaimType);
				writer.Write(claim.Value);
				TicketSerializer.WriteWithDefault(writer, claim.ValueType, "http://www.w3.org/2001/XMLSchema#string");
				TicketSerializer.WriteWithDefault(writer, claim.Issuer, "LOCAL AUTHORITY");
				TicketSerializer.WriteWithDefault(writer, claim.OriginalIssuer, claim.Issuer);
			}
			BootstrapContext bootstrapContext = identity.BootstrapContext as BootstrapContext;
			if (bootstrapContext == null || string.IsNullOrWhiteSpace(bootstrapContext.Token))
			{
				writer.Write(0);
			}
			else
			{
				writer.Write(bootstrapContext.Token.Length);
				writer.Write(bootstrapContext.Token);
			}
			PropertiesSerializer.Write(writer, model.Properties);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002908 File Offset: 0x00000B08
		public static AuthenticationTicket Read(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ReadInt32() != 3)
			{
				return null;
			}
			string authenticationType = reader.ReadString();
			string text = TicketSerializer.ReadWithDefault(reader, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
			string roleType = TicketSerializer.ReadWithDefault(reader, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
			int num = reader.ReadInt32();
			Claim[] array = new Claim[num];
			for (int num2 = 0; num2 != num; num2++)
			{
				string type = TicketSerializer.ReadWithDefault(reader, text);
				string value = reader.ReadString();
				string valueType = TicketSerializer.ReadWithDefault(reader, "http://www.w3.org/2001/XMLSchema#string");
				string text2 = TicketSerializer.ReadWithDefault(reader, "LOCAL AUTHORITY");
				string originalIssuer = TicketSerializer.ReadWithDefault(reader, text2);
				array[num2] = new Claim(type, value, valueType, text2, originalIssuer);
			}
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(array, authenticationType, text, roleType);
			int num3 = reader.ReadInt32();
			if (num3 > 0)
			{
				claimsIdentity.BootstrapContext = new BootstrapContext(reader.ReadString());
			}
			AuthenticationProperties properties = PropertiesSerializer.Read(reader);
			return new AuthenticationTicket(claimsIdentity, properties);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029F1 File Offset: 0x00000BF1
		private static void WriteWithDefault(BinaryWriter writer, string value, string defaultValue)
		{
			if (string.Equals(value, defaultValue, StringComparison.Ordinal))
			{
				writer.Write("\0");
				return;
			}
			writer.Write(value);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A10 File Offset: 0x00000C10
		private static string ReadWithDefault(BinaryReader reader, string defaultValue)
		{
			string text = reader.ReadString();
			if (string.Equals(text, "\0", StringComparison.Ordinal))
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x04000012 RID: 18
		private const int FormatVersion = 3;

		// Token: 0x02000014 RID: 20
		private static class DefaultValues
		{
			// Token: 0x04000013 RID: 19
			public const string DefaultStringPlaceholder = "\0";

			// Token: 0x04000014 RID: 20
			public const string NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

			// Token: 0x04000015 RID: 21
			public const string RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

			// Token: 0x04000016 RID: 22
			public const string LocalAuthority = "LOCAL AUTHORITY";

			// Token: 0x04000017 RID: 23
			public const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";
		}
	}
}
