using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001F8 RID: 504
	internal class ContentValidator
	{
		// Token: 0x060020CE RID: 8398 RVA: 0x000B3177 File Offset: 0x000B1377
		public ContentValidator(XmlSchemaContentType contentType)
		{
			this.contentType = contentType;
			this.isEmptiable = true;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000B318D File Offset: 0x000B138D
		protected ContentValidator(XmlSchemaContentType contentType, bool isOpen, bool isEmptiable)
		{
			this.contentType = contentType;
			this.isOpen = isOpen;
			this.isEmptiable = isEmptiable;
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x000B31AA File Offset: 0x000B13AA
		public XmlSchemaContentType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x000B31B2 File Offset: 0x000B13B2
		public bool PreserveWhitespace
		{
			get
			{
				return this.contentType == XmlSchemaContentType.TextOnly || this.contentType == XmlSchemaContentType.Mixed;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x000B31C7 File Offset: 0x000B13C7
		public virtual bool IsEmptiable
		{
			get
			{
				return this.isEmptiable;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x000B31CF File Offset: 0x000B13CF
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x000B31EA File Offset: 0x000B13EA
		public bool IsOpen
		{
			get
			{
				return this.contentType != XmlSchemaContentType.TextOnly && this.contentType != XmlSchemaContentType.Empty && this.isOpen;
			}
			set
			{
				this.isOpen = value;
			}
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000B31F3 File Offset: 0x000B13F3
		public virtual void InitValidation(ValidationState context)
		{
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000B31F5 File Offset: 0x000B13F5
		public virtual object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			if (this.contentType == XmlSchemaContentType.TextOnly || this.contentType == XmlSchemaContentType.Empty)
			{
				context.NeedValidateChildren = false;
			}
			errorCode = -1;
			return null;
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000B3213 File Offset: 0x000B1413
		public virtual bool CompleteValidation(ValidationState context)
		{
			return true;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000B3216 File Offset: 0x000B1416
		public virtual ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			return null;
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000B3219 File Offset: 0x000B1419
		public virtual ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			return null;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000B321C File Offset: 0x000B141C
		public static void AddParticleToExpected(XmlSchemaParticle p, XmlSchemaSet schemaSet, ArrayList particles)
		{
			ContentValidator.AddParticleToExpected(p, schemaSet, particles, false);
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000B3228 File Offset: 0x000B1428
		public static void AddParticleToExpected(XmlSchemaParticle p, XmlSchemaSet schemaSet, ArrayList particles, bool global)
		{
			if (!particles.Contains(p))
			{
				particles.Add(p);
			}
			XmlSchemaElement xmlSchemaElement = p as XmlSchemaElement;
			if (xmlSchemaElement != null && (global || !xmlSchemaElement.RefName.IsEmpty))
			{
				XmlSchemaObjectTable substitutionGroups = schemaSet.SubstitutionGroups;
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)substitutionGroups[xmlSchemaElement.QualifiedName];
				if (xmlSchemaSubstitutionGroup != null)
				{
					for (int i = 0; i < xmlSchemaSubstitutionGroup.Members.Count; i++)
					{
						XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)xmlSchemaSubstitutionGroup.Members[i];
						if (!xmlSchemaElement.QualifiedName.Equals(xmlSchemaElement2.QualifiedName) && !particles.Contains(xmlSchemaElement2))
						{
							particles.Add(xmlSchemaElement2);
						}
					}
				}
			}
		}

		// Token: 0x04000DC2 RID: 3522
		private XmlSchemaContentType contentType;

		// Token: 0x04000DC3 RID: 3523
		private bool isOpen;

		// Token: 0x04000DC4 RID: 3524
		private bool isEmptiable;

		// Token: 0x04000DC5 RID: 3525
		public static readonly ContentValidator Empty = new ContentValidator(XmlSchemaContentType.Empty);

		// Token: 0x04000DC6 RID: 3526
		public static readonly ContentValidator TextOnly = new ContentValidator(XmlSchemaContentType.TextOnly, false, false);

		// Token: 0x04000DC7 RID: 3527
		public static readonly ContentValidator Mixed = new ContentValidator(XmlSchemaContentType.Mixed);

		// Token: 0x04000DC8 RID: 3528
		public static readonly ContentValidator Any = new ContentValidator(XmlSchemaContentType.Mixed, true, true);
	}
}
