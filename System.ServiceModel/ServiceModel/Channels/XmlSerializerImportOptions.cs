using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services.Description;
using System.Xml.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009A2 RID: 2466
	public class XmlSerializerImportOptions
	{
		// Token: 0x060060CD RID: 24781 RVA: 0x00169E6C File Offset: 0x0016806C
		public XmlSerializerImportOptions() : this(new CodeCompileUnit())
		{
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x00169E79 File Offset: 0x00168079
		public XmlSerializerImportOptions(CodeCompileUnit codeCompileUnit)
		{
			this.codeCompileUnit = codeCompileUnit;
		}

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x060060CF RID: 24783 RVA: 0x00169E88 File Offset: 0x00168088
		public CodeCompileUnit CodeCompileUnit
		{
			get
			{
				if (this.codeCompileUnit == null)
				{
					this.codeCompileUnit = new CodeCompileUnit();
				}
				return this.codeCompileUnit;
			}
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x060060D0 RID: 24784 RVA: 0x00169EA3 File Offset: 0x001680A3
		// (set) Token: 0x060060D1 RID: 24785 RVA: 0x00169EC3 File Offset: 0x001680C3
		public CodeDomProvider CodeProvider
		{
			get
			{
				if (this.codeProvider == null)
				{
					this.codeProvider = CodeDomProvider.CreateProvider("C#");
				}
				return this.codeProvider;
			}
			set
			{
				this.codeProvider = value;
			}
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x060060D2 RID: 24786 RVA: 0x00169ECC File Offset: 0x001680CC
		// (set) Token: 0x060060D3 RID: 24787 RVA: 0x00169ED4 File Offset: 0x001680D4
		public string ClrNamespace
		{
			get
			{
				return this.clrNamespace;
			}
			set
			{
				this.clrNamespace = value;
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x060060D4 RID: 24788 RVA: 0x00169EDD File Offset: 0x001680DD
		// (set) Token: 0x060060D5 RID: 24789 RVA: 0x00169F08 File Offset: 0x00168108
		public WebReferenceOptions WebReferenceOptions
		{
			get
			{
				if (this.webReferenceOptions == null)
				{
					this.webReferenceOptions = new WebReferenceOptions();
					this.webReferenceOptions.CodeGenerationOptions = XmlSerializerImportOptions.defaultCodeGenerationOptions;
				}
				return this.webReferenceOptions;
			}
			set
			{
				this.webReferenceOptions = value;
			}
		}

		// Token: 0x040038A1 RID: 14497
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x040038A2 RID: 14498
		private CodeDomProvider codeProvider;

		// Token: 0x040038A3 RID: 14499
		private string clrNamespace;

		// Token: 0x040038A4 RID: 14500
		private WebReferenceOptions webReferenceOptions;

		// Token: 0x040038A5 RID: 14501
		private static CodeGenerationOptions defaultCodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateOrder;
	}
}
