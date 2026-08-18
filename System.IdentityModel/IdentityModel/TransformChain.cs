using System;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000084 RID: 132
	internal sealed class TransformChain
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00011A8D File Offset: 0x0000FC8D
		public int TransformCount
		{
			get
			{
				return this.transforms.Count;
			}
		}

		// Token: 0x17000118 RID: 280
		public Transform this[int index]
		{
			get
			{
				return this.transforms[index];
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		public bool NeedsInclusiveContext
		{
			get
			{
				for (int i = 0; i < this.TransformCount; i++)
				{
					if (this[i].NeedsInclusiveContext)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		public void Add(Transform transform)
		{
			if (!LocalAppContextSwitches.AllowUnlimitedXmlTransforms)
			{
				long maxXmlTransformsPerReference = SecurityUtils.GetMaxXmlTransformsPerReference();
				if ((long)this.TransformCount > maxXmlTransformsPerReference)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException());
				}
			}
			this.transforms.Add(transform);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00011B18 File Offset: 0x0000FD18
		public void ReadFrom(XmlDictionaryReader reader, TransformFactory transformFactory, DictionaryManager dictionaryManager, bool preserveComments)
		{
			reader.MoveToStartElement(dictionaryManager.XmlSignatureDictionary.Transforms, dictionaryManager.XmlSignatureDictionary.Namespace);
			this.prefix = reader.Prefix;
			reader.Read();
			while (reader.IsStartElement(dictionaryManager.XmlSignatureDictionary.Transform, dictionaryManager.XmlSignatureDictionary.Namespace))
			{
				string attribute = reader.GetAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
				Transform transform = transformFactory.CreateTransform(attribute);
				transform.ReadFrom(reader, dictionaryManager, preserveComments);
				this.Add(transform);
			}
			reader.MoveToContent();
			reader.ReadEndElement();
			if (this.TransformCount == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("AtLeastOneTransformRequired")));
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00011BD0 File Offset: 0x0000FDD0
		public byte[] TransformToDigest(object data, SignatureResourcePool resourcePool, string digestMethod, DictionaryManager dictionaryManager)
		{
			for (int i = 0; i < this.TransformCount - 1; i++)
			{
				data = this[i].Process(data, resourcePool, dictionaryManager);
			}
			return this[this.TransformCount - 1].ProcessAndDigest(data, resourcePool, digestMethod, dictionaryManager);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00011C1C File Offset: 0x0000FE1C
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			writer.WriteStartElement(this.prefix, dictionaryManager.XmlSignatureDictionary.Transforms, dictionaryManager.XmlSignatureDictionary.Namespace);
			for (int i = 0; i < this.TransformCount; i++)
			{
				this[i].WriteTo(writer, dictionaryManager);
			}
			writer.WriteEndElement();
		}

		// Token: 0x040003C3 RID: 963
		private string prefix = "";

		// Token: 0x040003C4 RID: 964
		private MostlySingletonList<Transform> transforms;
	}
}
