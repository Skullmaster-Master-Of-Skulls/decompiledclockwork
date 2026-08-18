using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000081 RID: 129
	internal class StandardSignedInfo : SignedInfo
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x00010D22 File Offset: 0x0000EF22
		public StandardSignedInfo(DictionaryManager dictionaryManager) : base(dictionaryManager)
		{
			this.references = new List<Reference>();
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00010D41 File Offset: 0x0000EF41
		public override int ReferenceCount
		{
			get
			{
				return this.references.Count;
			}
		}

		// Token: 0x1700010B RID: 267
		public Reference this[int index]
		{
			get
			{
				return this.references[index];
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00010D5C File Offset: 0x0000EF5C
		public void AddReference(Reference reference)
		{
			if (!LocalAppContextSwitches.AllowUnlimitedXmlReferences)
			{
				long maxXmlReferencesPerSignedInfo = SecurityUtils.GetMaxXmlReferencesPerSignedInfo();
				if ((long)this.ReferenceCount > maxXmlReferencesPerSignedInfo)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException());
				}
			}
			reference.ResourcePool = base.ResourcePool;
			this.references.Add(reference);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		public override void EnsureAllReferencesVerified()
		{
			for (int i = 0; i < this.references.Count; i++)
			{
				if (!this.references[i].Verified)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnableToResolveReferenceUriForSignature", new object[]
					{
						this.references[i].Uri
					})));
				}
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00010E14 File Offset: 0x0000F014
		public override bool EnsureDigestValidityIfIdMatches(string id, object resolvedXmlSource)
		{
			for (int i = 0; i < this.references.Count; i++)
			{
				if (this.references[i].EnsureDigestValidityIfIdMatches(id, resolvedXmlSource))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00010E50 File Offset: 0x0000F050
		public override bool HasUnverifiedReference(string id)
		{
			for (int i = 0; i < this.references.Count; i++)
			{
				if (!this.references[i].Verified && this.references[i].ExtractReferredId() == id)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		public override void ComputeReferenceDigests()
		{
			if (this.references.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("AtLeastOneReferenceRequired")));
			}
			for (int i = 0; i < this.references.Count; i++)
			{
				this.references[i].ComputeAndSetDigest();
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00010F00 File Offset: 0x0000F100
		public override void ReadFrom(XmlDictionaryReader reader, TransformFactory transformFactory, DictionaryManager dictionaryManager)
		{
			base.SendSide = false;
			if (reader.CanCanonicalize)
			{
				base.CanonicalStream = new MemoryStream();
				reader.StartCanonicalization(base.CanonicalStream, false, null);
			}
			reader.MoveToStartElement(dictionaryManager.XmlSignatureDictionary.SignedInfo, dictionaryManager.XmlSignatureDictionary.Namespace);
			this.prefix = reader.Prefix;
			base.Id = reader.GetAttribute(dictionaryManager.UtilityDictionary.IdAttribute, null);
			reader.Read();
			base.ReadCanonicalizationMethod(reader, dictionaryManager);
			base.ReadSignatureMethod(reader, dictionaryManager);
			while (reader.IsStartElement(dictionaryManager.XmlSignatureDictionary.Reference, dictionaryManager.XmlSignatureDictionary.Namespace))
			{
				Reference reference = new Reference(dictionaryManager);
				reference.ReadFrom(reader, transformFactory, dictionaryManager);
				this.AddReference(reference);
			}
			reader.ReadEndElement();
			if (reader.CanCanonicalize)
			{
				reader.EndCanonicalization();
			}
			string[] inclusivePrefixes = base.GetInclusivePrefixes();
			if (inclusivePrefixes != null)
			{
				base.CanonicalStream = null;
				this.context = new Dictionary<string, string>(inclusivePrefixes.Length);
				for (int i = 0; i < inclusivePrefixes.Length; i++)
				{
					this.context.Add(inclusivePrefixes[i], reader.LookupNamespace(inclusivePrefixes[i]));
				}
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001101C File Offset: 0x0000F21C
		public override void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			writer.WriteStartElement(this.prefix, dictionaryManager.XmlSignatureDictionary.SignedInfo, dictionaryManager.XmlSignatureDictionary.Namespace);
			if (base.Id != null)
			{
				writer.WriteAttributeString(dictionaryManager.UtilityDictionary.IdAttribute, null, base.Id);
			}
			base.WriteCanonicalizationMethod(writer, dictionaryManager);
			base.WriteSignatureMethod(writer, dictionaryManager);
			for (int i = 0; i < this.references.Count; i++)
			{
				this.references[i].WriteTo(writer, dictionaryManager);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000110AA File Offset: 0x0000F2AA
		protected override string GetNamespaceForInclusivePrefix(string prefix)
		{
			if (this.context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException());
			}
			if (prefix == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("prefix");
			}
			return this.context[prefix];
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000110E3 File Offset: 0x0000F2E3
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x000110EB File Offset: 0x0000F2EB
		protected string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x000110F4 File Offset: 0x0000F2F4
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x000110FC File Offset: 0x0000F2FC
		protected Dictionary<string, string> Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x040003B1 RID: 945
		private string prefix = "";

		// Token: 0x040003B2 RID: 946
		private List<Reference> references;

		// Token: 0x040003B3 RID: 947
		private Dictionary<string, string> context;
	}
}
