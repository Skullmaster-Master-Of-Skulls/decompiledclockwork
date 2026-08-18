using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000057 RID: 87
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigC14NTransform : Transform
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000FB50 File Offset: 0x0000DD50
		public XmlDsigC14NTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		public XmlDsigC14NTransform(bool includeComments)
		{
			this._includeComments = includeComments;
			base.Algorithm = (includeComments ? "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments" : "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000FC37 File Offset: 0x0000DE37
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000FC3F File Offset: 0x0000DE3F
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000FC47 File Offset: 0x0000DE47
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (!Utils.GetAllowAdditionalSignatureNodes() && nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000FC70 File Offset: 0x0000DE70
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

		// Token: 0x0600034A RID: 842 RVA: 0x0000FD1B File Offset: 0x0000DF1B
		public override object GetOutput()
		{
			return new MemoryStream(this._cXml.GetBytes());
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000FD30 File Offset: 0x0000DF30
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return new MemoryStream(this._cXml.GetBytes());
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000FD86 File Offset: 0x0000DF86
		[ComVisible(false)]
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return this._cXml.GetDigestedBytes(hash);
		}

		// Token: 0x0400045C RID: 1116
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		// Token: 0x0400045D RID: 1117
		private Type[] _outputTypes = new Type[]
		{
			typeof(Stream)
		};

		// Token: 0x0400045E RID: 1118
		private CanonicalXml _cXml;

		// Token: 0x0400045F RID: 1119
		private bool _includeComments;
	}
}
