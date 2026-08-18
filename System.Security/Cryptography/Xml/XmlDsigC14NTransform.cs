using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000AD RID: 173
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigC14NTransform : Transform
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x000142EC File Offset: 0x000132EC
		public XmlDsigC14NTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001435C File Offset: 0x0001335C
		public XmlDsigC14NTransform(bool includeComments)
		{
			this._includeComments = includeComments;
			base.Algorithm = (includeComments ? "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments" : "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x000143DB File Offset: 0x000133DB
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x000143E3 File Offset: 0x000133E3
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000143EB File Offset: 0x000133EB
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (!Utils.GetAllowAdditionalSignatureNodes() && nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00014410 File Offset: 0x00013410
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00014414 File Offset: 0x00013414
		public override void LoadInput(object obj)
		{
			XmlResolver resolver = base.ResolverSet ? this.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI);
			if (obj is Stream)
			{
				this._cXml = new CanonicalXml((Stream)obj, this._includeComments, resolver, base.BaseURI);
				return;
			}
			if (obj is XmlDocument)
			{
				this._cXml = new CanonicalXml((XmlDocument)obj, resolver, this._includeComments);
				return;
			}
			if (obj is XmlNodeList)
			{
				this._cXml = new CanonicalXml((XmlNodeList)obj, resolver, this._includeComments);
				return;
			}
			throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "obj");
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000144BF File Offset: 0x000134BF
		public override object GetOutput()
		{
			return new MemoryStream(this._cXml.GetBytes());
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000144D4 File Offset: 0x000134D4
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return new MemoryStream(this._cXml.GetBytes());
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00014525 File Offset: 0x00013525
		[ComVisible(false)]
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return this._cXml.GetDigestedBytes(hash);
		}

		// Token: 0x04000569 RID: 1385
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		// Token: 0x0400056A RID: 1386
		private Type[] _outputTypes = new Type[]
		{
			typeof(Stream)
		};

		// Token: 0x0400056B RID: 1387
		private CanonicalXml _cXml;

		// Token: 0x0400056C RID: 1388
		private bool _includeComments;
	}
}
