using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001DD RID: 477
	internal class ComponentTypeCodeDomSerializer : TypeCodeDomSerializer
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x000676D4 File Offset: 0x000658D4
		internal new static ComponentTypeCodeDomSerializer Default
		{
			get
			{
				if (ComponentTypeCodeDomSerializer._default == null)
				{
					ComponentTypeCodeDomSerializer._default = new ComponentTypeCodeDomSerializer();
				}
				return ComponentTypeCodeDomSerializer._default;
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000676EC File Offset: 0x000658EC
		protected override CodeMemberMethod GetInitializeMethod(IDesignerSerializationManager manager, CodeTypeDeclaration typeDecl, object value)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (typeDecl == null)
			{
				throw new ArgumentNullException("typeDecl");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			CodeMemberMethod codeMemberMethod = typeDecl.UserData[ComponentTypeCodeDomSerializer._initMethodKey] as CodeMemberMethod;
			if (codeMemberMethod == null)
			{
				codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "InitializeComponent";
				codeMemberMethod.Attributes = MemberAttributes.Private;
				typeDecl.UserData[ComponentTypeCodeDomSerializer._initMethodKey] = codeMemberMethod;
				CodeConstructor codeConstructor = new CodeConstructor();
				codeConstructor.Statements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "InitializeComponent", new CodeExpression[0]));
				typeDecl.Members.Add(codeConstructor);
			}
			return codeMemberMethod;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000677A0 File Offset: 0x000659A0
		protected override CodeMemberMethod[] GetInitializeMethods(IDesignerSerializationManager manager, CodeTypeDeclaration typeDecl)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (typeDecl == null)
			{
				throw new ArgumentNullException("typeDecl");
			}
			foreach (object obj in typeDecl.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				CodeMemberMethod codeMemberMethod = codeTypeMember as CodeMemberMethod;
				if (codeMemberMethod != null && codeMemberMethod.Name.Equals("InitializeComponent") && codeMemberMethod.Parameters.Count == 0)
				{
					return new CodeMemberMethod[]
					{
						codeMemberMethod
					};
				}
			}
			return new CodeMemberMethod[0];
		}

		// Token: 0x040009EC RID: 2540
		private static object _initMethodKey = new object();

		// Token: 0x040009ED RID: 2541
		private const string _initMethodName = "InitializeComponent";

		// Token: 0x040009EE RID: 2542
		private static ComponentTypeCodeDomSerializer _default;
	}
}
