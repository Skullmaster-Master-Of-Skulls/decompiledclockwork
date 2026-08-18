using System;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.Owin.Security.DataHandler
{
	// Token: 0x02000007 RID: 7
	public class SecureDataFormat<TData> : ISecureDataFormat<TData>
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021A9 File Offset: 0x000003A9
		public SecureDataFormat(IDataSerializer<TData> serializer, IDataProtector protector, ITextEncoder encoder)
		{
			this._serializer = serializer;
			this._protector = protector;
			this._encoder = encoder;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C8 File Offset: 0x000003C8
		public string Protect(TData data)
		{
			byte[] userData = this._serializer.Serialize(data);
			byte[] data2 = this._protector.Protect(userData);
			return this._encoder.Encode(data2);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002200 File Offset: 0x00000400
		public TData Unprotect(string protectedText)
		{
			TData result;
			try
			{
				if (protectedText == null)
				{
					result = default(TData);
				}
				else
				{
					byte[] array = this._encoder.Decode(protectedText);
					if (array == null)
					{
						result = default(TData);
					}
					else
					{
						byte[] array2 = this._protector.Unprotect(array);
						if (array2 == null)
						{
							result = default(TData);
						}
						else
						{
							TData tdata = this._serializer.Deserialize(array2);
							result = tdata;
						}
					}
				}
			}
			catch
			{
				result = default(TData);
			}
			return result;
		}

		// Token: 0x04000008 RID: 8
		private readonly IDataSerializer<TData> _serializer;

		// Token: 0x04000009 RID: 9
		private readonly IDataProtector _protector;

		// Token: 0x0400000A RID: 10
		private readonly ITextEncoder _encoder;
	}
}
