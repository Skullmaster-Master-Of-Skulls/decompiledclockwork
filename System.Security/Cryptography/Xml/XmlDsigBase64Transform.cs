using System;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000B1 RID: 177
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigBase64Transform : Transform
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x000148D0 File Offset: 0x000138D0
		public XmlDsigBase64Transform()
		{
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#base64";
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001493E File Offset: 0x0001393E
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00014946 File Offset: 0x00013946
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001494E File Offset: 0x0001394E
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (!Utils.GetAllowAdditionalSignatureNodes() && nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00014973 File Offset: 0x00013973
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00014978 File Offset: 0x00013978
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
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000149D0 File Offset: 0x000139D0
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

		// Token: 0x06000402 RID: 1026 RVA: 0x00014A80 File Offset: 0x00013A80
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

		// Token: 0x06000403 RID: 1027 RVA: 0x00014B84 File Offset: 0x00013B84
		public override object GetOutput()
		{
			return this._cs;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00014B8C File Offset: 0x00013B8C
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return this._cs;
		}

		// Token: 0x04000572 RID: 1394
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x04000573 RID: 1395
		private Type[] _outputTypes = new Type[]
		{
			typeof(Stream)
		};

		// Token: 0x04000574 RID: 1396
		private CryptoStream _cs;
	}
}
