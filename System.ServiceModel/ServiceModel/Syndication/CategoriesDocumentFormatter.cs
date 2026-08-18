using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x020001AB RID: 427
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[DataContract]
	public abstract class CategoriesDocumentFormatter
	{
		// Token: 0x06000E09 RID: 3593 RVA: 0x000327BB File Offset: 0x000309BB
		protected CategoriesDocumentFormatter()
		{
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000327C3 File Offset: 0x000309C3
		protected CategoriesDocumentFormatter(CategoriesDocument documentToWrite)
		{
			if (documentToWrite == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("documentToWrite");
			}
			this.document = documentToWrite;
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x000327E5 File Offset: 0x000309E5
		public CategoriesDocument Document
		{
			get
			{
				return this.document;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000E0C RID: 3596
		public abstract string Version { get; }

		// Token: 0x06000E0D RID: 3597
		public abstract bool CanRead(XmlReader reader);

		// Token: 0x06000E0E RID: 3598
		public abstract void ReadFrom(XmlReader reader);

		// Token: 0x06000E0F RID: 3599
		public abstract void WriteTo(XmlWriter writer);

		// Token: 0x06000E10 RID: 3600 RVA: 0x000327ED File Offset: 0x000309ED
		protected virtual InlineCategoriesDocument CreateInlineCategoriesDocument()
		{
			return new InlineCategoriesDocument();
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000327F4 File Offset: 0x000309F4
		protected virtual ReferencedCategoriesDocument CreateReferencedCategoriesDocument()
		{
			return new ReferencedCategoriesDocument();
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000327FB File Offset: 0x000309FB
		protected virtual void SetDocument(CategoriesDocument document)
		{
			this.document = document;
		}

		// Token: 0x04001728 RID: 5928
		private CategoriesDocument document;
	}
}
