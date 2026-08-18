using System;
using System.Dynamic.Utils;
using System.Linq.Expressions.Compiler;

namespace System.Linq.Expressions
{
	// Token: 0x0200026A RID: 618
	[__DynamicallyInvokable]
	public class SymbolDocumentInfo
	{
		// Token: 0x06001627 RID: 5671 RVA: 0x00049502 File Offset: 0x00047702
		internal SymbolDocumentInfo(string fileName)
		{
			ContractUtils.RequiresNotNull(fileName, "fileName");
			this._fileName = fileName;
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x0004951C File Offset: 0x0004771C
		[__DynamicallyInvokable]
		public string FileName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x00049524 File Offset: 0x00047724
		[__DynamicallyInvokable]
		public virtual Guid Language
		{
			[__DynamicallyInvokable]
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x0004952B File Offset: 0x0004772B
		[__DynamicallyInvokable]
		public virtual Guid LanguageVendor
		{
			[__DynamicallyInvokable]
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x00049532 File Offset: 0x00047732
		[__DynamicallyInvokable]
		public virtual Guid DocumentType
		{
			[__DynamicallyInvokable]
			get
			{
				return SymbolGuids.DocumentType_Text;
			}
		}

		// Token: 0x04000A55 RID: 2645
		private readonly string _fileName;
	}
}
