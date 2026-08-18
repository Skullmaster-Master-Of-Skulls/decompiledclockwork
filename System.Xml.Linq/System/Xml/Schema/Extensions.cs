using System;
using System.Xml.Linq;

namespace System.Xml.Schema
{
	// Token: 0x02000007 RID: 7
	public static class Extensions
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000297E File Offset: 0x00000B7E
		public static IXmlSchemaInfo GetSchemaInfo(this XElement source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Annotation<IXmlSchemaInfo>();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002994 File Offset: 0x00000B94
		public static IXmlSchemaInfo GetSchemaInfo(this XAttribute source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Annotation<IXmlSchemaInfo>();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000029AA File Offset: 0x00000BAA
		public static void Validate(this XDocument source, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			source.Validate(schemas, validationEventHandler, false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000029B5 File Offset: 0x00000BB5
		public static void Validate(this XDocument source, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler, bool addSchemaInfo)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			new XNodeValidator(schemas, validationEventHandler).Validate(source, null, addSchemaInfo);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000029E2 File Offset: 0x00000BE2
		public static void Validate(this XElement source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			source.Validate(partialValidationType, schemas, validationEventHandler, false);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000029EE File Offset: 0x00000BEE
		public static void Validate(this XElement source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler, bool addSchemaInfo)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (partialValidationType == null)
			{
				throw new ArgumentNullException("partialValidationType");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			new XNodeValidator(schemas, validationEventHandler).Validate(source, partialValidationType, addSchemaInfo);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A2A File Offset: 0x00000C2A
		public static void Validate(this XAttribute source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			source.Validate(partialValidationType, schemas, validationEventHandler, false);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A36 File Offset: 0x00000C36
		public static void Validate(this XAttribute source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler, bool addSchemaInfo)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (partialValidationType == null)
			{
				throw new ArgumentNullException("partialValidationType");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			new XNodeValidator(schemas, validationEventHandler).Validate(source, partialValidationType, addSchemaInfo);
		}
	}
}
