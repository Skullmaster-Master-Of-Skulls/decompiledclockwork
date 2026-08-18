using System;
using System.CodeDom;

namespace System.ServiceModel.Description
{
	// Token: 0x02000427 RID: 1063
	internal class UniqueCodeNamespaceScope : UniqueCodeIdentifierScope
	{
		// Token: 0x06002915 RID: 10517 RVA: 0x0009C3FF File Offset: 0x0009A5FF
		public UniqueCodeNamespaceScope(CodeNamespace codeNamespace)
		{
			this.codeNamespace = codeNamespace;
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x0009C40E File Offset: 0x0009A60E
		public CodeNamespace CodeNamespace
		{
			get
			{
				return this.codeNamespace;
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x0009C416 File Offset: 0x0009A616
		protected override void AddIdentifier(string identifier)
		{
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x0009C418 File Offset: 0x0009A618
		public CodeTypeReference AddUnique(CodeTypeDeclaration codeType, string name, string defaultName)
		{
			codeType.Name = base.AddUnique(name, defaultName);
			this.codeNamespace.Types.Add(codeType);
			return ServiceContractGenerator.NamespaceHelper.GetCodeTypeReference(this.codeNamespace, codeType);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x0009C446 File Offset: 0x0009A646
		public override bool IsUnique(string identifier)
		{
			return !this.NamespaceContainsType(identifier);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x0009C454 File Offset: 0x0009A654
		private bool NamespaceContainsType(string typeName)
		{
			foreach (object obj in this.codeNamespace.Types)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
				if (string.Compare(codeTypeDeclaration.Name, typeName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400226A RID: 8810
		private CodeNamespace codeNamespace;
	}
}
