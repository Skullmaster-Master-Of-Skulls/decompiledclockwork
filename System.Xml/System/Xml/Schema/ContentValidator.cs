using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001A0 RID: 416
	internal class ContentValidator
	{
		// Token: 0x0600157E RID: 5502 RVA: 0x0005F320 File Offset: 0x0005E320
		public ContentValidator(XmlSchemaContentType contentType)
		{
			this.contentType = contentType;
			this.isEmptiable = true;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0005F336 File Offset: 0x0005E336
		protected ContentValidator(XmlSchemaContentType contentType, bool isOpen, bool isEmptiable)
		{
			this.contentType = contentType;
			this.isOpen = isOpen;
			this.isEmptiable = isEmptiable;
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0005F353 File Offset: 0x0005E353
		public XmlSchemaContentType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x0005F35B File Offset: 0x0005E35B
		public bool PreserveWhitespace
		{
			get
			{
				return this.contentType == XmlSchemaContentType.TextOnly || this.contentType == XmlSchemaContentType.Mixed;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x0005F370 File Offset: 0x0005E370
		public virtual bool IsEmptiable
		{
			get
			{
				return this.isEmptiable;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x0005F378 File Offset: 0x0005E378
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x0005F393 File Offset: 0x0005E393
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

		// Token: 0x06001585 RID: 5509 RVA: 0x0005F39C File Offset: 0x0005E39C
		public virtual void InitValidation(ValidationState context)
		{
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0005F39E File Offset: 0x0005E39E
		public virtual object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			if (this.contentType == XmlSchemaContentType.TextOnly || this.contentType == XmlSchemaContentType.Empty)
			{
				context.NeedValidateChildren = false;
			}
			errorCode = -1;
			return null;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0005F3BC File Offset: 0x0005E3BC
		public virtual bool CompleteValidation(ValidationState context)
		{
			return true;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0005F3BF File Offset: 0x0005E3BF
		public virtual ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			return null;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0005F3C2 File Offset: 0x0005E3C2
		public virtual ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly)
		{
			return null;
		}

		// Token: 0x04000CCD RID: 3277
		private XmlSchemaContentType contentType;

		// Token: 0x04000CCE RID: 3278
		private bool isOpen;

		// Token: 0x04000CCF RID: 3279
		private bool isEmptiable;

		// Token: 0x04000CD0 RID: 3280
		public static readonly ContentValidator Empty = new ContentValidator(XmlSchemaContentType.Empty);

		// Token: 0x04000CD1 RID: 3281
		public static readonly ContentValidator TextOnly = new ContentValidator(XmlSchemaContentType.TextOnly, false, false);

		// Token: 0x04000CD2 RID: 3282
		public static readonly ContentValidator Mixed = new ContentValidator(XmlSchemaContentType.Mixed);

		// Token: 0x04000CD3 RID: 3283
		public static readonly ContentValidator Any = new ContentValidator(XmlSchemaContentType.Mixed, true, true);
	}
}
