using System;
using System.Linq.Expressions.Compiler;

namespace System.Linq.Expressions
{
	// Token: 0x0200026B RID: 619
	internal sealed class SymbolDocumentWithGuids : SymbolDocumentInfo
	{
		// Token: 0x0600162C RID: 5676 RVA: 0x00049539 File Offset: 0x00047739
		internal SymbolDocumentWithGuids(string fileName, ref Guid language) : base(fileName)
		{
			this._language = language;
			this._documentType = SymbolGuids.DocumentType_Text;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00049559 File Offset: 0x00047759
		internal SymbolDocumentWithGuids(string fileName, ref Guid language, ref Guid vendor) : base(fileName)
		{
			this._language = language;
			this._vendor = vendor;
			this._documentType = SymbolGuids.DocumentType_Text;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00049585 File Offset: 0x00047785
		internal SymbolDocumentWithGuids(string fileName, ref Guid language, ref Guid vendor, ref Guid documentType) : base(fileName)
		{
			this._language = language;
			this._vendor = vendor;
			this._documentType = documentType;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x000495B3 File Offset: 0x000477B3
		public override Guid Language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x000495BB File Offset: 0x000477BB
		public override Guid LanguageVendor
		{
			get
			{
				return this._vendor;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x000495C3 File Offset: 0x000477C3
		public override Guid DocumentType
		{
			get
			{
				return this._documentType;
			}
		}

		// Token: 0x04000A56 RID: 2646
		private readonly Guid _language;

		// Token: 0x04000A57 RID: 2647
		private readonly Guid _vendor;

		// Token: 0x04000A58 RID: 2648
		private readonly Guid _documentType;
	}
}
