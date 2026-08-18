using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000055 RID: 85
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class TransformChain
	{
		// Token: 0x0600031E RID: 798 RVA: 0x0000F3A2 File Offset: 0x0000D5A2
		public TransformChain()
		{
			this.m_transforms = new ArrayList();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000F3B5 File Offset: 0x0000D5B5
		public void Add(Transform transform)
		{
			if (transform != null)
			{
				this.m_transforms.Add(transform);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000F3C7 File Offset: 0x0000D5C7
		public IEnumerator GetEnumerator()
		{
			return this.m_transforms.GetEnumerator();
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		public int Count
		{
			get
			{
				return this.m_transforms.Count;
			}
		}

		// Token: 0x170000A7 RID: 167
		public Transform this[int index]
		{
			get
			{
				if (index >= this.m_transforms.Count)
				{
					throw new ArgumentException(SecurityResources.GetResourceString("ArgumentOutOfRange_Index"), "index");
				}
				return (Transform)this.m_transforms[index];
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F418 File Offset: 0x0000D618
		internal Stream TransformToOctetStream(object inputObject, Type inputType, XmlResolver resolver, string baseUri)
		{
			object obj = inputObject;
			foreach (object obj2 in this.m_transforms)
			{
				Transform transform = (Transform)obj2;
				if (obj == null || transform.AcceptsType(obj.GetType()))
				{
					transform.Resolver = resolver;
					transform.BaseURI = baseUri;
					transform.LoadInput(obj);
					obj = transform.GetOutput();
				}
				else if (obj is Stream)
				{
					if (!transform.AcceptsType(typeof(XmlDocument)))
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"));
					}
					Stream stream = obj as Stream;
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.PreserveWhitespace = true;
					XmlReader reader = Utils.PreProcessStreamInput(stream, resolver, baseUri);
					xmlDocument.Load(reader);
					transform.LoadInput(xmlDocument);
					stream.Close();
					obj = transform.GetOutput();
				}
				else if (obj is XmlNodeList)
				{
					if (!transform.AcceptsType(typeof(Stream)))
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"));
					}
					CanonicalXml canonicalXml = new CanonicalXml((XmlNodeList)obj, resolver, false);
					MemoryStream memoryStream = new MemoryStream(canonicalXml.GetBytes());
					transform.LoadInput(memoryStream);
					obj = transform.GetOutput();
					memoryStream.Close();
				}
				else
				{
					if (!(obj is XmlDocument))
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"));
					}
					if (!transform.AcceptsType(typeof(Stream)))
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"));
					}
					CanonicalXml canonicalXml2 = new CanonicalXml((XmlDocument)obj, resolver);
					MemoryStream memoryStream2 = new MemoryStream(canonicalXml2.GetBytes());
					transform.LoadInput(memoryStream2);
					obj = transform.GetOutput();
					memoryStream2.Close();
				}
			}
			if (obj is Stream)
			{
				return obj as Stream;
			}
			if (obj is XmlNodeList)
			{
				CanonicalXml canonicalXml3 = new CanonicalXml((XmlNodeList)obj, resolver, false);
				return new MemoryStream(canonicalXml3.GetBytes());
			}
			if (obj is XmlDocument)
			{
				CanonicalXml canonicalXml4 = new CanonicalXml((XmlDocument)obj, resolver);
				return new MemoryStream(canonicalXml4.GetBytes());
			}
			throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"));
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000F65C File Offset: 0x0000D85C
		internal Stream TransformToOctetStream(Stream input, XmlResolver resolver, string baseUri)
		{
			return this.TransformToOctetStream(input, typeof(Stream), resolver, baseUri);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000F671 File Offset: 0x0000D871
		internal Stream TransformToOctetStream(XmlDocument document, XmlResolver resolver, string baseUri)
		{
			return this.TransformToOctetStream(document, typeof(XmlDocument), resolver, baseUri);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000F688 File Offset: 0x0000D888
		internal XmlElement GetXml(XmlDocument document, string ns)
		{
			XmlElement xmlElement = document.CreateElement("Transforms", ns);
			foreach (object obj in this.m_transforms)
			{
				Transform transform = (Transform)obj;
				if (transform != null)
				{
					XmlElement xml = transform.GetXml(document);
					if (xml != null)
					{
						xmlElement.AppendChild(xml);
					}
				}
			}
			return xmlElement;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000F704 File Offset: 0x0000D904
		internal void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNodeList xmlNodeList = value.SelectNodes("ds:Transform", xmlNamespaceManager);
			if (xmlNodeList.Count == 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Transforms");
			}
			int maxTransformsPerChain = Utils.GetMaxTransformsPerChain();
			if (maxTransformsPerChain > 0 && xmlNodeList.Count > maxTransformsPerChain)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "MAX_TRANSFORMS_PER_CHAIN_EXCEEDED");
			}
			this.m_transforms.Clear();
			for (int i = 0; i < xmlNodeList.Count; i++)
			{
				XmlElement xmlElement = (XmlElement)xmlNodeList.Item(i);
				string attribute = Utils.GetAttribute(xmlElement, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
				Transform transform = Utils.CreateFromName<Transform>(attribute);
				if (transform == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
				}
				transform.LoadInnerXml(xmlElement.ChildNodes);
				this.m_transforms.Add(transform);
			}
		}

		// Token: 0x04000453 RID: 1107
		private ArrayList m_transforms;
	}
}
