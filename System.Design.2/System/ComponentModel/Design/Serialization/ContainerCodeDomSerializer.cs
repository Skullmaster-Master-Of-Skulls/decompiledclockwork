using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001DE RID: 478
	internal class ContainerCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00067864 File Offset: 0x00065A64
		internal new static ContainerCodeDomSerializer Default
		{
			get
			{
				if (ContainerCodeDomSerializer._defaultSerializer == null)
				{
					ContainerCodeDomSerializer._defaultSerializer = new ContainerCodeDomSerializer();
				}
				return ContainerCodeDomSerializer._defaultSerializer;
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0006787C File Offset: 0x00065A7C
		protected override object DeserializeInstance(IDesignerSerializationManager manager, Type type, object[] parameters, string name, bool addToContainer)
		{
			if (typeof(IContainer).IsAssignableFrom(type))
			{
				object service = manager.GetService(typeof(IContainer));
				if (service != null)
				{
					manager.SetName(service, name);
					return service;
				}
			}
			return base.DeserializeInstance(manager, type, parameters, name, addToContainer);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000678C8 File Offset: 0x00065AC8
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			CodeTypeDeclaration codeTypeDeclaration = manager.Context[typeof(CodeTypeDeclaration)] as CodeTypeDeclaration;
			RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
			CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
			CodeExpression codeExpression;
			if (codeTypeDeclaration != null && rootContext != null)
			{
				CodeMemberField codeMemberField = new CodeMemberField(typeof(IContainer), "components");
				codeMemberField.Attributes = MemberAttributes.Private;
				codeTypeDeclaration.Members.Add(codeMemberField);
				codeExpression = new CodeFieldReferenceExpression(rootContext.Expression, "components");
			}
			else
			{
				CodeVariableDeclarationStatement value2 = new CodeVariableDeclarationStatement(typeof(IContainer), "components");
				codeStatementCollection.Add(value2);
				codeExpression = new CodeVariableReferenceExpression("components");
			}
			base.SetExpression(manager, value, codeExpression);
			CodeObjectCreateExpression right = new CodeObjectCreateExpression(typeof(Container), new CodeExpression[0]);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement(codeExpression, right);
			codeAssignStatement.UserData["IContainer"] = "IContainer";
			codeStatementCollection.Add(codeAssignStatement);
			return codeStatementCollection;
		}

		// Token: 0x040009EF RID: 2543
		private const string _containerName = "components";

		// Token: 0x040009F0 RID: 2544
		private static ContainerCodeDomSerializer _defaultSerializer;
	}
}
