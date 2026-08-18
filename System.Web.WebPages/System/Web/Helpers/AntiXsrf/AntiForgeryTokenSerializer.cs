using System;
using System.IO;
using System.Web.Mvc;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x02000032 RID: 50
	internal sealed class AntiForgeryTokenSerializer : IAntiForgeryTokenSerializer
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000509A File Offset: 0x0000329A
		internal AntiForgeryTokenSerializer(ICryptoSystem cryptoSystem)
		{
			this._cryptoSystem = cryptoSystem;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000050AC File Offset: 0x000032AC
		public AntiForgeryToken Deserialize(string serializedToken)
		{
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(this._cryptoSystem.Unprotect(serializedToken)))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						AntiForgeryToken antiForgeryToken = AntiForgeryTokenSerializer.DeserializeImpl(binaryReader);
						if (antiForgeryToken != null)
						{
							return antiForgeryToken;
						}
					}
				}
			}
			catch
			{
			}
			throw HttpAntiForgeryException.CreateDeserializationFailedException();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000512C File Offset: 0x0000332C
		private static AntiForgeryToken DeserializeImpl(BinaryReader reader)
		{
			byte b = reader.ReadByte();
			if (b != 1)
			{
				return null;
			}
			AntiForgeryToken antiForgeryToken = new AntiForgeryToken();
			byte[] data = reader.ReadBytes(16);
			antiForgeryToken.SecurityToken = new BinaryBlob(128, data);
			antiForgeryToken.IsSessionToken = reader.ReadBoolean();
			if (!antiForgeryToken.IsSessionToken)
			{
				bool flag = reader.ReadBoolean();
				if (flag)
				{
					byte[] data2 = reader.ReadBytes(32);
					antiForgeryToken.ClaimUid = new BinaryBlob(256, data2);
				}
				else
				{
					antiForgeryToken.Username = reader.ReadString();
				}
				antiForgeryToken.AdditionalData = reader.ReadString();
			}
			if (reader.BaseStream.ReadByte() != -1)
			{
				return null;
			}
			return antiForgeryToken;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000051CC File Offset: 0x000033CC
		public string Serialize(AntiForgeryToken token)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(1);
					binaryWriter.Write(token.SecurityToken.GetData());
					binaryWriter.Write(token.IsSessionToken);
					if (!token.IsSessionToken)
					{
						if (token.ClaimUid != null)
						{
							binaryWriter.Write(true);
							binaryWriter.Write(token.ClaimUid.GetData());
						}
						else
						{
							binaryWriter.Write(false);
							binaryWriter.Write(token.Username);
						}
						binaryWriter.Write(token.AdditionalData);
					}
					binaryWriter.Flush();
					result = this._cryptoSystem.Protect(memoryStream.ToArray());
				}
			}
			return result;
		}

		// Token: 0x0400006F RID: 111
		private const byte TokenVersion = 1;

		// Token: 0x04000070 RID: 112
		private readonly ICryptoSystem _cryptoSystem;
	}
}
