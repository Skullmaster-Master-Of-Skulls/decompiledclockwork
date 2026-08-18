using System;
using System.IO;

namespace System.Web.Security
{
	// Token: 0x020005E2 RID: 1506
	internal static class FormsAuthenticationTicketSerializer
	{
		// Token: 0x06004C16 RID: 19478 RVA: 0x00103EB4 File Offset: 0x001020B4
		public static FormsAuthenticationTicket Deserialize(byte[] serializedTicket, int serializedTicketLength)
		{
			FormsAuthenticationTicket result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(serializedTicket))
				{
					using (FormsAuthenticationTicketSerializer.SerializingBinaryReader serializingBinaryReader = new FormsAuthenticationTicketSerializer.SerializingBinaryReader(memoryStream))
					{
						byte b = serializingBinaryReader.ReadByte();
						if (b != 1)
						{
							result = null;
						}
						else
						{
							int version = (int)serializingBinaryReader.ReadByte();
							long ticks = serializingBinaryReader.ReadInt64();
							DateTime issueDateUtc = new DateTime(ticks, DateTimeKind.Utc);
							DateTime dateTime = issueDateUtc.ToLocalTime();
							byte b2 = serializingBinaryReader.ReadByte();
							if (b2 != 254)
							{
								result = null;
							}
							else
							{
								long ticks2 = serializingBinaryReader.ReadInt64();
								DateTime expirationUtc = new DateTime(ticks2, DateTimeKind.Utc);
								DateTime dateTime2 = expirationUtc.ToLocalTime();
								byte b3 = serializingBinaryReader.ReadByte();
								bool isPersistent;
								if (b3 != 0)
								{
									if (b3 != 1)
									{
										return null;
									}
									isPersistent = true;
								}
								else
								{
									isPersistent = false;
								}
								string name = serializingBinaryReader.ReadBinaryString();
								string userData = serializingBinaryReader.ReadBinaryString();
								string cookiePath = serializingBinaryReader.ReadBinaryString();
								byte b4 = serializingBinaryReader.ReadByte();
								if (b4 != 255)
								{
									result = null;
								}
								else if (memoryStream.Position != (long)serializedTicketLength)
								{
									result = null;
								}
								else
								{
									result = FormsAuthenticationTicket.FromUtc(version, name, issueDateUtc, expirationUtc, isPersistent, userData, cookiePath);
								}
							}
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x00104018 File Offset: 0x00102218
		public static byte[] Serialize(FormsAuthenticationTicket ticket)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (FormsAuthenticationTicketSerializer.SerializingBinaryWriter serializingBinaryWriter = new FormsAuthenticationTicketSerializer.SerializingBinaryWriter(memoryStream))
				{
					serializingBinaryWriter.Write(1);
					serializingBinaryWriter.Write((byte)ticket.Version);
					serializingBinaryWriter.Write(ticket.IssueDateUtc.Ticks);
					serializingBinaryWriter.Write(254);
					serializingBinaryWriter.Write(ticket.ExpirationUtc.Ticks);
					serializingBinaryWriter.Write(ticket.IsPersistent);
					serializingBinaryWriter.WriteBinaryString(ticket.Name);
					serializingBinaryWriter.WriteBinaryString(ticket.UserData);
					serializingBinaryWriter.WriteBinaryString(ticket.CookiePath);
					serializingBinaryWriter.Write(byte.MaxValue);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x040028F5 RID: 10485
		private const byte CURRENT_TICKET_SERIALIZED_VERSION = 1;

		// Token: 0x02000A03 RID: 2563
		private sealed class SerializingBinaryReader : BinaryReader
		{
			// Token: 0x06006D67 RID: 28007 RVA: 0x00177AD2 File Offset: 0x00175CD2
			public SerializingBinaryReader(Stream input) : base(input)
			{
			}

			// Token: 0x06006D68 RID: 28008 RVA: 0x001873A0 File Offset: 0x001855A0
			public string ReadBinaryString()
			{
				int num = base.Read7BitEncodedInt();
				byte[] array = this.ReadBytes(num * 2);
				char[] array2 = new char[num];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (char)((int)array[2 * i] | (int)array[2 * i + 1] << 8);
				}
				return new string(array2);
			}

			// Token: 0x06006D69 RID: 28009 RVA: 0x00003ABB File Offset: 0x00001CBB
			public override string ReadString()
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x02000A04 RID: 2564
		private sealed class SerializingBinaryWriter : BinaryWriter
		{
			// Token: 0x06006D6A RID: 28010 RVA: 0x00177AE3 File Offset: 0x00175CE3
			public SerializingBinaryWriter(Stream output) : base(output)
			{
			}

			// Token: 0x06006D6B RID: 28011 RVA: 0x00003ABB File Offset: 0x00001CBB
			public override void Write(string value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006D6C RID: 28012 RVA: 0x001873EC File Offset: 0x001855EC
			public void WriteBinaryString(string value)
			{
				byte[] array = new byte[value.Length * 2];
				for (int i = 0; i < value.Length; i++)
				{
					char c = value[i];
					array[2 * i] = (byte)c;
					array[2 * i + 1] = (byte)(c >> 8);
				}
				base.Write7BitEncodedInt(value.Length);
				this.Write(array);
			}
		}
	}
}
