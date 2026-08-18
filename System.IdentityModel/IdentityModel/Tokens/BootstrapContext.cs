using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	public class BootstrapContext : ISerializable
	{
		// Token: 0x0600078C RID: 1932 RVA: 0x0001FC3C File Offset: 0x0001DE3C
		protected BootstrapContext(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				return;
			}
			char @char = info.GetChar("K");
			if (@char != 'B')
			{
				if (@char == 'S')
				{
					this._tokenString = info.GetString("T");
					return;
				}
				if (@char == 'T')
				{
					SecurityTokenHandler securityTokenHandler = context.Context as SecurityTokenHandler;
					if (securityTokenHandler != null)
					{
						using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(Convert.FromBase64String(info.GetString("T")), XmlDictionaryReaderQuotas.Max))
						{
							xmlDictionaryReader.MoveToContent();
							if (securityTokenHandler.CanReadToken(xmlDictionaryReader))
							{
								string localName = xmlDictionaryReader.LocalName;
								string namespaceURI = xmlDictionaryReader.NamespaceURI;
								SecurityToken securityToken = securityTokenHandler.ReadToken(xmlDictionaryReader);
								if (securityToken == null)
								{
									this._tokenString = Encoding.UTF8.GetString(Convert.FromBase64String(info.GetString("T")));
								}
								else
								{
									this._token = securityToken;
								}
							}
							return;
						}
					}
					this._tokenString = Encoding.UTF8.GetString(Convert.FromBase64String(info.GetString("T")));
					return;
				}
			}
			else
			{
				this._tokenBytes = (byte[])info.GetValue("T", typeof(byte[]));
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001FD68 File Offset: 0x0001DF68
		public BootstrapContext(SecurityToken token, SecurityTokenHandler tokenHandler)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			if (tokenHandler == null)
			{
				throw new ArgumentNullException("tokenHandler");
			}
			this._token = token;
			this._tokenHandler = tokenHandler;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001FD9A File Offset: 0x0001DF9A
		public BootstrapContext(string token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			this._tokenString = token;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001FDB7 File Offset: 0x0001DFB7
		public BootstrapContext(byte[] token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			this._tokenBytes = token;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this._tokenBytes != null)
			{
				info.AddValue("K", 'B');
				info.AddValue("T", this._tokenBytes);
				return;
			}
			if (this._tokenString != null)
			{
				info.AddValue("K", 'S');
				info.AddValue("T", this._tokenString);
				return;
			}
			if (this._token != null && this._tokenHandler != null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					info.AddValue("K", 'T');
					using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8, false))
					{
						this._tokenHandler.WriteToken(xmlDictionaryWriter, this._token);
						xmlDictionaryWriter.Flush();
						info.AddValue("T", Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
					}
				}
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001FEC8 File Offset: 0x0001E0C8
		public byte[] TokenBytes
		{
			get
			{
				return this._tokenBytes;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001FED0 File Offset: 0x0001E0D0
		public string Token
		{
			get
			{
				return this._tokenString;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		public SecurityToken SecurityToken
		{
			get
			{
				return this._token;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
		public SecurityTokenHandler SecurityTokenHandler
		{
			get
			{
				return this._tokenHandler;
			}
		}

		// Token: 0x04000AC5 RID: 2757
		private SecurityToken _token;

		// Token: 0x04000AC6 RID: 2758
		private string _tokenString;

		// Token: 0x04000AC7 RID: 2759
		private byte[] _tokenBytes;

		// Token: 0x04000AC8 RID: 2760
		private SecurityTokenHandler _tokenHandler;

		// Token: 0x04000AC9 RID: 2761
		private const string _tokenTypeKey = "K";

		// Token: 0x04000ACA RID: 2762
		private const string _tokenKey = "T";

		// Token: 0x04000ACB RID: 2763
		private const char _securityTokenType = 'T';

		// Token: 0x04000ACC RID: 2764
		private const char _stringTokenType = 'S';

		// Token: 0x04000ACD RID: 2765
		private const char _byteTokenType = 'B';
	}
}
