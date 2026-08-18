using System;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200005B RID: 91
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigBase64Transform : Transform
	{
		// Token: 0x0600035E RID: 862 RVA: 0x00010134 File Offset: 0x0000E334
		public XmlDsigBase64Transform()
		{
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#base64";
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0001019E File Offset: 0x0000E39E
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000101A6 File Offset: 0x0000E3A6
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000FC47 File Offset: 0x0000DE47
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (!Utils.GetAllowAdditionalSignatureNodes() && nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000101B0 File Offset: 0x0000E3B0
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				this.LoadStreamInput((Stream)obj);
				return;
			}
			if (obj is XmlNodeList)
			{
				this.LoadXmlNodeListInput((XmlNodeList)obj);
				return;
			}
			if (obj is XmlDocument)
			{
				this.LoadXmlNodeListInput(((XmlDocument)obj).SelectNodes("//."));
				return;
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00010208 File Offset: 0x0000E408
		private void LoadStreamInput(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentException("obj");
			}
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[1024];
			int num;
			do
			{
				num = inputStream.Read(array, 0, 1024);
				if (num > 0)
				{
					int i = 0;
					while (i < num && !char.IsWhiteSpace((char)array[i]))
					{
						i++;
					}
					int num2 = i;
					for (i++; i < num; i++)
					{
						if (!char.IsWhiteSpace((char)array[i]))
						{
							array[num2] = array[i];
							num2++;
						}
					}
					memoryStream.Write(array, 0, num2);
				}
			}
			while (num > 0);
			memoryStream.Position = 0L;
			this._cs = new CryptoStream(memoryStream, new FromBase64Transform(), CryptoStreamMode.Read);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000102B8 File Offset: 0x0000E4B8
		private void LoadXmlNodeListInput(XmlNodeList nodeList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("self::text()");
				if (xmlNode2 != null)
				{
					stringBuilder.Append(xmlNode2.OuterXml);
				}
			}
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			byte[] bytes = utf8Encoding.GetBytes(stringBuilder.ToString());
			int i = 0;
			while (i < bytes.Length && !char.IsWhiteSpace((char)bytes[i]))
			{
				i++;
			}
			int num = i;
			for (i++; i < bytes.Length; i++)
			{
				if (!char.IsWhiteSpace((char)bytes[i]))
				{
					bytes[num] = bytes[i];
					num++;
				}
			}
			MemoryStream stream = new MemoryStream(bytes, 0, num);
			this._cs = new CryptoStream(stream, new FromBase64Transform(), CryptoStreamMode.Read);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public override object GetOutput()
		{
			return this._cs;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000103BC File Offset: 0x0000E5BC
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return this._cs;
		}

		// Token: 0x04000465 RID: 1125
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x04000466 RID: 1126
		private Type[] _outputTypes = new Type[]
		{
			typeof(Stream)
		};

		// Token: 0x04000467 RID: 1127
		private CryptoStream _cs;
	}
}
