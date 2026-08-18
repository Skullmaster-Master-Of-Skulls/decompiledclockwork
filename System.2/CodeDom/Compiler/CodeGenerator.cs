using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000671 RID: 1649
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeGenerator : ICodeGenerator
	{
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06003BDD RID: 15325 RVA: 0x000F74A8 File Offset: 0x000F56A8
		protected CodeTypeDeclaration CurrentClass
		{
			get
			{
				return this.currentClass;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003BDE RID: 15326 RVA: 0x000F74B0 File Offset: 0x000F56B0
		protected string CurrentTypeName
		{
			get
			{
				if (this.currentClass != null)
				{
					return this.currentClass.Name;
				}
				return "<% unknown %>";
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x000F74CB File Offset: 0x000F56CB
		protected CodeTypeMember CurrentMember
		{
			get
			{
				return this.currentMember;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06003BE0 RID: 15328 RVA: 0x000F74D3 File Offset: 0x000F56D3
		protected string CurrentMemberName
		{
			get
			{
				if (this.currentMember != null)
				{
					return this.currentMember.Name;
				}
				return "<% unknown %>";
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x000F74EE File Offset: 0x000F56EE
		protected bool IsCurrentInterface
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsInterface;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x000F7512 File Offset: 0x000F5712
		protected bool IsCurrentClass
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsClass;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003BE3 RID: 15331 RVA: 0x000F7536 File Offset: 0x000F5736
		protected bool IsCurrentStruct
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsStruct;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x000F755A File Offset: 0x000F575A
		protected bool IsCurrentEnum
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsEnum;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x000F757E File Offset: 0x000F577E
		protected bool IsCurrentDelegate
		{
			get
			{
				return this.currentClass != null && this.currentClass is CodeTypeDelegate;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06003BE6 RID: 15334 RVA: 0x000F7598 File Offset: 0x000F5798
		// (set) Token: 0x06003BE7 RID: 15335 RVA: 0x000F75A5 File Offset: 0x000F57A5
		protected int Indent
		{
			get
			{
				return this.output.Indent;
			}
			set
			{
				this.output.Indent = value;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06003BE8 RID: 15336
		protected abstract string NullToken { get; }

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x000F75B3 File Offset: 0x000F57B3
		protected TextWriter Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x000F75BB File Offset: 0x000F57BB
		protected CodeGeneratorOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x000F75C4 File Offset: 0x000F57C4
		private void GenerateType(CodeTypeDeclaration e)
		{
			this.currentClass = e;
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			this.GenerateCommentStatements(e.Comments);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			this.GenerateTypeStart(e);
			if (this.Options.VerbatimOrder)
			{
				using (IEnumerator enumerator = e.Members.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CodeTypeMember member = (CodeTypeMember)obj;
						this.GenerateTypeMember(member, e);
					}
					goto IL_CA;
				}
			}
			this.GenerateFields(e);
			this.GenerateSnippetMembers(e);
			this.GenerateTypeConstructors(e);
			this.GenerateConstructors(e);
			this.GenerateProperties(e);
			this.GenerateEvents(e);
			this.GenerateMethods(e);
			this.GenerateNestedTypes(e);
			IL_CA:
			this.currentClass = e;
			this.GenerateTypeEnd(e);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x000F76E8 File Offset: 0x000F58E8
		protected virtual void GenerateDirectives(CodeDirectiveCollection directives)
		{
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x000F76EC File Offset: 0x000F58EC
		private void GenerateTypeMember(CodeTypeMember member, CodeTypeDeclaration declaredType)
		{
			if (this.options.BlankLinesBetweenMembers)
			{
				this.Output.WriteLine();
			}
			if (member is CodeTypeDeclaration)
			{
				((ICodeGenerator)this).GenerateCodeFromType((CodeTypeDeclaration)member, this.output.InnerWriter, this.options);
				this.currentClass = declaredType;
				return;
			}
			if (member.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(member.StartDirectives);
			}
			this.GenerateCommentStatements(member.Comments);
			if (member.LinePragma != null)
			{
				this.GenerateLinePragmaStart(member.LinePragma);
			}
			if (member is CodeMemberField)
			{
				this.GenerateField((CodeMemberField)member);
			}
			else if (member is CodeMemberProperty)
			{
				this.GenerateProperty((CodeMemberProperty)member, declaredType);
			}
			else if (member is CodeMemberMethod)
			{
				if (member is CodeConstructor)
				{
					this.GenerateConstructor((CodeConstructor)member, declaredType);
				}
				else if (member is CodeTypeConstructor)
				{
					this.GenerateTypeConstructor((CodeTypeConstructor)member);
				}
				else if (member is CodeEntryPointMethod)
				{
					this.GenerateEntryPointMethod((CodeEntryPointMethod)member, declaredType);
				}
				else
				{
					this.GenerateMethod((CodeMemberMethod)member, declaredType);
				}
			}
			else if (member is CodeMemberEvent)
			{
				this.GenerateEvent((CodeMemberEvent)member, declaredType);
			}
			else if (member is CodeSnippetTypeMember)
			{
				int indent = this.Indent;
				this.Indent = 0;
				this.GenerateSnippetMember((CodeSnippetTypeMember)member);
				this.Indent = indent;
				this.Output.WriteLine();
			}
			if (member.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(member.LinePragma);
			}
			if (member.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(member.EndDirectives);
			}
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x000F7884 File Offset: 0x000F5A84
		private void GenerateTypeConstructors(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeTypeConstructor)
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeTypeConstructor codeTypeConstructor = (CodeTypeConstructor)enumerator.Current;
					if (codeTypeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeTypeConstructor.LinePragma);
					}
					this.GenerateTypeConstructor(codeTypeConstructor);
					if (codeTypeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeTypeConstructor.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x000F797C File Offset: 0x000F5B7C
		protected void GenerateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace e2 = (CodeNamespace)obj;
				((ICodeGenerator)this).GenerateCodeFromNamespace(e2, this.output.InnerWriter, this.options);
			}
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x000F79E8 File Offset: 0x000F5BE8
		protected void GenerateTypes(CodeNamespace e)
		{
			foreach (object obj in e.Types)
			{
				CodeTypeDeclaration e2 = (CodeTypeDeclaration)obj;
				if (this.options.BlankLinesBetweenMembers)
				{
					this.Output.WriteLine();
				}
				((ICodeGenerator)this).GenerateCodeFromType(e2, this.output.InnerWriter, this.options);
			}
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x000F7A6C File Offset: 0x000F5C6C
		bool ICodeGenerator.Supports(GeneratorSupport support)
		{
			return this.Supports(support);
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x000F7A78 File Offset: 0x000F5C78
		void ICodeGenerator.GenerateCodeFromType(CodeTypeDeclaration e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this.output != null && w != this.output.InnerWriter)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenOutputWriter"));
			}
			if (this.output == null)
			{
				flag = true;
				this.options = ((o == null) ? new CodeGeneratorOptions() : o);
				this.output = new IndentedTextWriter(w, this.options.IndentString);
			}
			try
			{
				this.GenerateType(e);
			}
			finally
			{
				if (flag)
				{
					this.output = null;
					this.options = null;
				}
			}
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x000F7B0C File Offset: 0x000F5D0C
		void ICodeGenerator.GenerateCodeFromExpression(CodeExpression e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this.output != null && w != this.output.InnerWriter)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenOutputWriter"));
			}
			if (this.output == null)
			{
				flag = true;
				this.options = ((o == null) ? new CodeGeneratorOptions() : o);
				this.output = new IndentedTextWriter(w, this.options.IndentString);
			}
			try
			{
				this.GenerateExpression(e);
			}
			finally
			{
				if (flag)
				{
					this.output = null;
					this.options = null;
				}
			}
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x000F7BA0 File Offset: 0x000F5DA0
		void ICodeGenerator.GenerateCodeFromCompileUnit(CodeCompileUnit e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this.output != null && w != this.output.InnerWriter)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenOutputWriter"));
			}
			if (this.output == null)
			{
				flag = true;
				this.options = ((o == null) ? new CodeGeneratorOptions() : o);
				this.output = new IndentedTextWriter(w, this.options.IndentString);
			}
			try
			{
				if (e is CodeSnippetCompileUnit)
				{
					this.GenerateSnippetCompileUnit((CodeSnippetCompileUnit)e);
				}
				else
				{
					this.GenerateCompileUnit(e);
				}
			}
			finally
			{
				if (flag)
				{
					this.output = null;
					this.options = null;
				}
			}
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x000F7C4C File Offset: 0x000F5E4C
		void ICodeGenerator.GenerateCodeFromNamespace(CodeNamespace e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this.output != null && w != this.output.InnerWriter)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenOutputWriter"));
			}
			if (this.output == null)
			{
				flag = true;
				this.options = ((o == null) ? new CodeGeneratorOptions() : o);
				this.output = new IndentedTextWriter(w, this.options.IndentString);
			}
			try
			{
				this.GenerateNamespace(e);
			}
			finally
			{
				if (flag)
				{
					this.output = null;
					this.options = null;
				}
			}
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x000F7CE0 File Offset: 0x000F5EE0
		void ICodeGenerator.GenerateCodeFromStatement(CodeStatement e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this.output != null && w != this.output.InnerWriter)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenOutputWriter"));
			}
			if (this.output == null)
			{
				flag = true;
				this.options = ((o == null) ? new CodeGeneratorOptions() : o);
				this.output = new IndentedTextWriter(w, this.options.IndentString);
			}
			try
			{
				this.GenerateStatement(e);
			}
			finally
			{
				if (flag)
				{
					this.output = null;
					this.options = null;
				}
			}
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x000F7D74 File Offset: 0x000F5F74
		public virtual void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			if (this.output != null)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenReentrance"));
			}
			this.options = ((options == null) ? new CodeGeneratorOptions() : options);
			this.output = new IndentedTextWriter(writer, this.options.IndentString);
			try
			{
				CodeTypeDeclaration declaredType = new CodeTypeDeclaration();
				this.currentClass = declaredType;
				this.GenerateTypeMember(member, declaredType);
			}
			finally
			{
				this.currentClass = null;
				this.output = null;
				this.options = null;
			}
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x000F7E00 File Offset: 0x000F6000
		bool ICodeGenerator.IsValidIdentifier(string value)
		{
			return this.IsValidIdentifier(value);
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x000F7E09 File Offset: 0x000F6009
		void ICodeGenerator.ValidateIdentifier(string value)
		{
			this.ValidateIdentifier(value);
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x000F7E12 File Offset: 0x000F6012
		string ICodeGenerator.CreateEscapedIdentifier(string value)
		{
			return this.CreateEscapedIdentifier(value);
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x000F7E1B File Offset: 0x000F601B
		string ICodeGenerator.CreateValidIdentifier(string value)
		{
			return this.CreateValidIdentifier(value);
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x000F7E24 File Offset: 0x000F6024
		string ICodeGenerator.GetTypeOutput(CodeTypeReference type)
		{
			return this.GetTypeOutput(type);
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x000F7E30 File Offset: 0x000F6030
		private void GenerateConstructors(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeConstructor)
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeConstructor codeConstructor = (CodeConstructor)enumerator.Current;
					if (codeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeConstructor.LinePragma);
					}
					this.GenerateConstructor(codeConstructor, e);
					if (codeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeConstructor.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000F7F28 File Offset: 0x000F6128
		private void GenerateEvents(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeMemberEvent)
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeMemberEvent codeMemberEvent = (CodeMemberEvent)enumerator.Current;
					if (codeMemberEvent.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberEvent.LinePragma);
					}
					this.GenerateEvent(codeMemberEvent, e);
					if (codeMemberEvent.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberEvent.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x000F8020 File Offset: 0x000F6220
		protected void GenerateExpression(CodeExpression e)
		{
			if (e is CodeArrayCreateExpression)
			{
				this.GenerateArrayCreateExpression((CodeArrayCreateExpression)e);
				return;
			}
			if (e is CodeBaseReferenceExpression)
			{
				this.GenerateBaseReferenceExpression((CodeBaseReferenceExpression)e);
				return;
			}
			if (e is CodeBinaryOperatorExpression)
			{
				this.GenerateBinaryOperatorExpression((CodeBinaryOperatorExpression)e);
				return;
			}
			if (e is CodeCastExpression)
			{
				this.GenerateCastExpression((CodeCastExpression)e);
				return;
			}
			if (e is CodeDelegateCreateExpression)
			{
				this.GenerateDelegateCreateExpression((CodeDelegateCreateExpression)e);
				return;
			}
			if (e is CodeFieldReferenceExpression)
			{
				this.GenerateFieldReferenceExpression((CodeFieldReferenceExpression)e);
				return;
			}
			if (e is CodeArgumentReferenceExpression)
			{
				this.GenerateArgumentReferenceExpression((CodeArgumentReferenceExpression)e);
				return;
			}
			if (e is CodeVariableReferenceExpression)
			{
				this.GenerateVariableReferenceExpression((CodeVariableReferenceExpression)e);
				return;
			}
			if (e is CodeIndexerExpression)
			{
				this.GenerateIndexerExpression((CodeIndexerExpression)e);
				return;
			}
			if (e is CodeArrayIndexerExpression)
			{
				this.GenerateArrayIndexerExpression((CodeArrayIndexerExpression)e);
				return;
			}
			if (e is CodeSnippetExpression)
			{
				this.GenerateSnippetExpression((CodeSnippetExpression)e);
				return;
			}
			if (e is CodeMethodInvokeExpression)
			{
				this.GenerateMethodInvokeExpression((CodeMethodInvokeExpression)e);
				return;
			}
			if (e is CodeMethodReferenceExpression)
			{
				this.GenerateMethodReferenceExpression((CodeMethodReferenceExpression)e);
				return;
			}
			if (e is CodeEventReferenceExpression)
			{
				this.GenerateEventReferenceExpression((CodeEventReferenceExpression)e);
				return;
			}
			if (e is CodeDelegateInvokeExpression)
			{
				this.GenerateDelegateInvokeExpression((CodeDelegateInvokeExpression)e);
				return;
			}
			if (e is CodeObjectCreateExpression)
			{
				this.GenerateObjectCreateExpression((CodeObjectCreateExpression)e);
				return;
			}
			if (e is CodeParameterDeclarationExpression)
			{
				this.GenerateParameterDeclarationExpression((CodeParameterDeclarationExpression)e);
				return;
			}
			if (e is CodeDirectionExpression)
			{
				this.GenerateDirectionExpression((CodeDirectionExpression)e);
				return;
			}
			if (e is CodePrimitiveExpression)
			{
				this.GeneratePrimitiveExpression((CodePrimitiveExpression)e);
				return;
			}
			if (e is CodePropertyReferenceExpression)
			{
				this.GeneratePropertyReferenceExpression((CodePropertyReferenceExpression)e);
				return;
			}
			if (e is CodePropertySetValueReferenceExpression)
			{
				this.GeneratePropertySetValueReferenceExpression((CodePropertySetValueReferenceExpression)e);
				return;
			}
			if (e is CodeThisReferenceExpression)
			{
				this.GenerateThisReferenceExpression((CodeThisReferenceExpression)e);
				return;
			}
			if (e is CodeTypeReferenceExpression)
			{
				this.GenerateTypeReferenceExpression((CodeTypeReferenceExpression)e);
				return;
			}
			if (e is CodeTypeOfExpression)
			{
				this.GenerateTypeOfExpression((CodeTypeOfExpression)e);
				return;
			}
			if (e is CodeDefaultValueExpression)
			{
				this.GenerateDefaultValueExpression((CodeDefaultValueExpression)e);
				return;
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			throw new ArgumentException(SR.GetString("InvalidElementType", new object[]
			{
				e.GetType().FullName
			}), "e");
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x000F8270 File Offset: 0x000F6470
		private void GenerateFields(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeMemberField)
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeMemberField codeMemberField = (CodeMemberField)enumerator.Current;
					if (codeMemberField.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberField.LinePragma);
					}
					this.GenerateField(codeMemberField);
					if (codeMemberField.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberField.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x000F8368 File Offset: 0x000F6568
		private void GenerateSnippetMembers(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			bool flag = false;
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeSnippetTypeMember)
				{
					flag = true;
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeSnippetTypeMember codeSnippetTypeMember = (CodeSnippetTypeMember)enumerator.Current;
					if (codeSnippetTypeMember.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeSnippetTypeMember.LinePragma);
					}
					int indent = this.Indent;
					this.Indent = 0;
					this.GenerateSnippetMember(codeSnippetTypeMember);
					this.Indent = indent;
					if (codeSnippetTypeMember.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeSnippetTypeMember.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
			if (flag)
			{
				this.Output.WriteLine();
			}
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x000F8488 File Offset: 0x000F6688
		protected virtual void GenerateSnippetCompileUnit(CodeSnippetCompileUnit e)
		{
			this.GenerateDirectives(e.StartDirectives);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			this.Output.WriteLine(e.Value);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x000F84F4 File Offset: 0x000F66F4
		private void GenerateMethods(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeMemberMethod && !(enumerator.Current is CodeTypeConstructor) && !(enumerator.Current is CodeConstructor))
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeMemberMethod codeMemberMethod = (CodeMemberMethod)enumerator.Current;
					if (codeMemberMethod.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberMethod.LinePragma);
					}
					if (enumerator.Current is CodeEntryPointMethod)
					{
						this.GenerateEntryPointMethod((CodeEntryPointMethod)enumerator.Current, e);
					}
					else
					{
						this.GenerateMethod(codeMemberMethod, e);
					}
					if (codeMemberMethod.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberMethod.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x000F862C File Offset: 0x000F682C
		private void GenerateNestedTypes(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeTypeDeclaration)
				{
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					CodeTypeDeclaration e2 = (CodeTypeDeclaration)enumerator.Current;
					((ICodeGenerator)this).GenerateCodeFromType(e2, this.output.InnerWriter, this.options);
				}
			}
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x000F8698 File Offset: 0x000F6898
		protected virtual void GenerateCompileUnit(CodeCompileUnit e)
		{
			this.GenerateCompileUnitStart(e);
			this.GenerateNamespaces(e);
			this.GenerateCompileUnitEnd(e);
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x000F86AF File Offset: 0x000F68AF
		protected virtual void GenerateNamespace(CodeNamespace e)
		{
			this.GenerateCommentStatements(e.Comments);
			this.GenerateNamespaceStart(e);
			this.GenerateNamespaceImports(e);
			this.Output.WriteLine("");
			this.GenerateTypes(e);
			this.GenerateNamespaceEnd(e);
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x000F86EC File Offset: 0x000F68EC
		protected void GenerateNamespaceImports(CodeNamespace e)
		{
			foreach (object obj in e.Imports)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaStart(codeNamespaceImport.LinePragma);
				}
				this.GenerateNamespaceImport(codeNamespaceImport);
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaEnd(codeNamespaceImport.LinePragma);
				}
			}
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x000F874C File Offset: 0x000F694C
		private void GenerateProperties(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeMemberProperty)
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeMemberProperty codeMemberProperty = (CodeMemberProperty)enumerator.Current;
					if (codeMemberProperty.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberProperty.LinePragma);
					}
					this.GenerateProperty(codeMemberProperty, e);
					if (codeMemberProperty.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberProperty.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x000F8844 File Offset: 0x000F6A44
		protected void GenerateStatement(CodeStatement e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			if (e is CodeCommentStatement)
			{
				this.GenerateCommentStatement((CodeCommentStatement)e);
			}
			else if (e is CodeMethodReturnStatement)
			{
				this.GenerateMethodReturnStatement((CodeMethodReturnStatement)e);
			}
			else if (e is CodeConditionStatement)
			{
				this.GenerateConditionStatement((CodeConditionStatement)e);
			}
			else if (e is CodeTryCatchFinallyStatement)
			{
				this.GenerateTryCatchFinallyStatement((CodeTryCatchFinallyStatement)e);
			}
			else if (e is CodeAssignStatement)
			{
				this.GenerateAssignStatement((CodeAssignStatement)e);
			}
			else if (e is CodeExpressionStatement)
			{
				this.GenerateExpressionStatement((CodeExpressionStatement)e);
			}
			else if (e is CodeIterationStatement)
			{
				this.GenerateIterationStatement((CodeIterationStatement)e);
			}
			else if (e is CodeThrowExceptionStatement)
			{
				this.GenerateThrowExceptionStatement((CodeThrowExceptionStatement)e);
			}
			else if (e is CodeSnippetStatement)
			{
				int indent = this.Indent;
				this.Indent = 0;
				this.GenerateSnippetStatement((CodeSnippetStatement)e);
				this.Indent = indent;
			}
			else if (e is CodeVariableDeclarationStatement)
			{
				this.GenerateVariableDeclarationStatement((CodeVariableDeclarationStatement)e);
			}
			else if (e is CodeAttachEventStatement)
			{
				this.GenerateAttachEventStatement((CodeAttachEventStatement)e);
			}
			else if (e is CodeRemoveEventStatement)
			{
				this.GenerateRemoveEventStatement((CodeRemoveEventStatement)e);
			}
			else if (e is CodeGotoStatement)
			{
				this.GenerateGotoStatement((CodeGotoStatement)e);
			}
			else
			{
				if (!(e is CodeLabeledStatement))
				{
					throw new ArgumentException(SR.GetString("InvalidElementType", new object[]
					{
						e.GetType().FullName
					}), "e");
				}
				this.GenerateLabeledStatement((CodeLabeledStatement)e);
			}
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x000F8A40 File Offset: 0x000F6C40
		protected void GenerateStatements(CodeStatementCollection stms)
		{
			foreach (object obj in stms)
			{
				((ICodeGenerator)this).GenerateCodeFromStatement((CodeStatement)obj, this.output.InnerWriter, this.options);
			}
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x000F8A80 File Offset: 0x000F6C80
		protected virtual void OutputAttributeDeclarations(CodeAttributeDeclarationCollection attributes)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			this.GenerateAttributeDeclarationsStart(attributes);
			bool flag = true;
			IEnumerator enumerator = attributes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.ContinueOnNewLine(", ");
				}
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)enumerator.Current;
				this.Output.Write(codeAttributeDeclaration.Name);
				this.Output.Write("(");
				bool flag2 = true;
				foreach (object obj in codeAttributeDeclaration.Arguments)
				{
					CodeAttributeArgument arg = (CodeAttributeArgument)obj;
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						this.Output.Write(", ");
					}
					this.OutputAttributeArgument(arg);
				}
				this.Output.Write(")");
			}
			this.GenerateAttributeDeclarationsEnd(attributes);
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x000F8B7C File Offset: 0x000F6D7C
		protected virtual void OutputAttributeArgument(CodeAttributeArgument arg)
		{
			if (arg.Name != null && arg.Name.Length > 0)
			{
				this.OutputIdentifier(arg.Name);
				this.Output.Write("=");
			}
			((ICodeGenerator)this).GenerateCodeFromExpression(arg.Value, this.output.InnerWriter, this.options);
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x000F8BD8 File Offset: 0x000F6DD8
		protected virtual void OutputDirection(FieldDirection dir)
		{
			switch (dir)
			{
			case FieldDirection.In:
				break;
			case FieldDirection.Out:
				this.Output.Write("out ");
				return;
			case FieldDirection.Ref:
				this.Output.Write("ref ");
				break;
			default:
				return;
			}
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x000F8C10 File Offset: 0x000F6E10
		protected virtual void OutputFieldScopeModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.VTableMask;
			if (memberAttributes == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Final:
			case MemberAttributes.Override:
				break;
			case MemberAttributes.Static:
				this.Output.Write("static ");
				return;
			case MemberAttributes.Const:
				this.Output.Write("const ");
				break;
			default:
				return;
			}
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x000F8C7C File Offset: 0x000F6E7C
		protected virtual void OutputMemberAccessModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
			if (memberAttributes <= MemberAttributes.Family)
			{
				if (memberAttributes == MemberAttributes.Assembly)
				{
					this.Output.Write("internal ");
					return;
				}
				if (memberAttributes == MemberAttributes.FamilyAndAssembly)
				{
					this.Output.Write("internal ");
					return;
				}
				if (memberAttributes != MemberAttributes.Family)
				{
					return;
				}
				this.Output.Write("protected ");
				return;
			}
			else
			{
				if (memberAttributes == MemberAttributes.FamilyOrAssembly)
				{
					this.Output.Write("protected internal ");
					return;
				}
				if (memberAttributes == MemberAttributes.Private)
				{
					this.Output.Write("private ");
					return;
				}
				if (memberAttributes != MemberAttributes.Public)
				{
					return;
				}
				this.Output.Write("public ");
				return;
			}
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x000F8D30 File Offset: 0x000F6F30
		protected virtual void OutputMemberScopeModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.VTableMask;
			if (memberAttributes == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Abstract:
				this.Output.Write("abstract ");
				return;
			case MemberAttributes.Final:
				this.Output.Write("");
				return;
			case MemberAttributes.Static:
				this.Output.Write("static ");
				return;
			case MemberAttributes.Override:
				this.Output.Write("override ");
				return;
			default:
			{
				MemberAttributes memberAttributes2 = attributes & MemberAttributes.AccessMask;
				if (memberAttributes2 == MemberAttributes.Family || memberAttributes2 == MemberAttributes.Public)
				{
					this.Output.Write("virtual ");
				}
				return;
			}
			}
		}

		// Token: 0x06003C11 RID: 15377
		protected abstract void OutputType(CodeTypeReference typeRef);

		// Token: 0x06003C12 RID: 15378 RVA: 0x000F8DE8 File Offset: 0x000F6FE8
		protected virtual void OutputTypeAttributes(TypeAttributes attributes, bool isStruct, bool isEnum)
		{
			TypeAttributes typeAttributes = attributes & TypeAttributes.VisibilityMask;
			if (typeAttributes - TypeAttributes.Public > 1)
			{
				if (typeAttributes == TypeAttributes.NestedPrivate)
				{
					this.Output.Write("private ");
				}
			}
			else
			{
				this.Output.Write("public ");
			}
			if (isStruct)
			{
				this.Output.Write("struct ");
				return;
			}
			if (isEnum)
			{
				this.Output.Write("enum ");
				return;
			}
			TypeAttributes typeAttributes2 = attributes & TypeAttributes.ClassSemanticsMask;
			if (typeAttributes2 == TypeAttributes.NotPublic)
			{
				if ((attributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				{
					this.Output.Write("sealed ");
				}
				if ((attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract)
				{
					this.Output.Write("abstract ");
				}
				this.Output.Write("class ");
				return;
			}
			if (typeAttributes2 != TypeAttributes.ClassSemanticsMask)
			{
				return;
			}
			this.Output.Write("interface ");
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x000F8EBA File Offset: 0x000F70BA
		protected virtual void OutputTypeNamePair(CodeTypeReference typeRef, string name)
		{
			this.OutputType(typeRef);
			this.Output.Write(" ");
			this.OutputIdentifier(name);
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x000F8EDA File Offset: 0x000F70DA
		protected virtual void OutputIdentifier(string ident)
		{
			this.Output.Write(ident);
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x000F8EE8 File Offset: 0x000F70E8
		protected virtual void OutputExpressionList(CodeExpressionCollection expressions)
		{
			this.OutputExpressionList(expressions, false);
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x000F8EF4 File Offset: 0x000F70F4
		protected virtual void OutputExpressionList(CodeExpressionCollection expressions, bool newlineBetweenItems)
		{
			bool flag = true;
			IEnumerator enumerator = expressions.GetEnumerator();
			int indent = this.Indent;
			this.Indent = indent + 1;
			while (enumerator.MoveNext())
			{
				if (flag)
				{
					flag = false;
				}
				else if (newlineBetweenItems)
				{
					this.ContinueOnNewLine(",");
				}
				else
				{
					this.Output.Write(", ");
				}
				((ICodeGenerator)this).GenerateCodeFromExpression((CodeExpression)enumerator.Current, this.output.InnerWriter, this.options);
			}
			indent = this.Indent;
			this.Indent = indent - 1;
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x000F8F80 File Offset: 0x000F7180
		protected virtual void OutputOperator(CodeBinaryOperatorType op)
		{
			switch (op)
			{
			case CodeBinaryOperatorType.Add:
				this.Output.Write("+");
				return;
			case CodeBinaryOperatorType.Subtract:
				this.Output.Write("-");
				return;
			case CodeBinaryOperatorType.Multiply:
				this.Output.Write("*");
				return;
			case CodeBinaryOperatorType.Divide:
				this.Output.Write("/");
				return;
			case CodeBinaryOperatorType.Modulus:
				this.Output.Write("%");
				return;
			case CodeBinaryOperatorType.Assign:
				this.Output.Write("=");
				return;
			case CodeBinaryOperatorType.IdentityInequality:
				this.Output.Write("!=");
				return;
			case CodeBinaryOperatorType.IdentityEquality:
				this.Output.Write("==");
				return;
			case CodeBinaryOperatorType.ValueEquality:
				this.Output.Write("==");
				return;
			case CodeBinaryOperatorType.BitwiseOr:
				this.Output.Write("|");
				return;
			case CodeBinaryOperatorType.BitwiseAnd:
				this.Output.Write("&");
				return;
			case CodeBinaryOperatorType.BooleanOr:
				this.Output.Write("||");
				return;
			case CodeBinaryOperatorType.BooleanAnd:
				this.Output.Write("&&");
				return;
			case CodeBinaryOperatorType.LessThan:
				this.Output.Write("<");
				return;
			case CodeBinaryOperatorType.LessThanOrEqual:
				this.Output.Write("<=");
				return;
			case CodeBinaryOperatorType.GreaterThan:
				this.Output.Write(">");
				return;
			case CodeBinaryOperatorType.GreaterThanOrEqual:
				this.Output.Write(">=");
				return;
			default:
				return;
			}
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x000F90F8 File Offset: 0x000F72F8
		protected virtual void OutputParameters(CodeParameterDeclarationExpressionCollection parameters)
		{
			bool flag = true;
			bool flag2 = parameters.Count > 15;
			if (flag2)
			{
				this.Indent += 3;
			}
			foreach (object obj in parameters)
			{
				CodeParameterDeclarationExpression e = (CodeParameterDeclarationExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				if (flag2)
				{
					this.ContinueOnNewLine("");
				}
				this.GenerateExpression(e);
			}
			if (flag2)
			{
				this.Indent -= 3;
			}
		}

		// Token: 0x06003C19 RID: 15385
		protected abstract void GenerateArrayCreateExpression(CodeArrayCreateExpression e);

		// Token: 0x06003C1A RID: 15386
		protected abstract void GenerateBaseReferenceExpression(CodeBaseReferenceExpression e);

		// Token: 0x06003C1B RID: 15387 RVA: 0x000F9180 File Offset: 0x000F7380
		protected virtual void GenerateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
		{
			bool flag = false;
			this.Output.Write("(");
			this.GenerateExpression(e.Left);
			this.Output.Write(" ");
			if (e.Left is CodeBinaryOperatorExpression || e.Right is CodeBinaryOperatorExpression)
			{
				if (!this.inNestedBinary)
				{
					flag = true;
					this.inNestedBinary = true;
					this.Indent += 3;
				}
				this.ContinueOnNewLine("");
			}
			this.OutputOperator(e.Operator);
			this.Output.Write(" ");
			this.GenerateExpression(e.Right);
			this.Output.Write(")");
			if (flag)
			{
				this.Indent -= 3;
				this.inNestedBinary = false;
			}
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x000F924F File Offset: 0x000F744F
		protected virtual void ContinueOnNewLine(string st)
		{
			this.Output.WriteLine(st);
		}

		// Token: 0x06003C1D RID: 15389
		protected abstract void GenerateCastExpression(CodeCastExpression e);

		// Token: 0x06003C1E RID: 15390
		protected abstract void GenerateDelegateCreateExpression(CodeDelegateCreateExpression e);

		// Token: 0x06003C1F RID: 15391
		protected abstract void GenerateFieldReferenceExpression(CodeFieldReferenceExpression e);

		// Token: 0x06003C20 RID: 15392
		protected abstract void GenerateArgumentReferenceExpression(CodeArgumentReferenceExpression e);

		// Token: 0x06003C21 RID: 15393
		protected abstract void GenerateVariableReferenceExpression(CodeVariableReferenceExpression e);

		// Token: 0x06003C22 RID: 15394
		protected abstract void GenerateIndexerExpression(CodeIndexerExpression e);

		// Token: 0x06003C23 RID: 15395
		protected abstract void GenerateArrayIndexerExpression(CodeArrayIndexerExpression e);

		// Token: 0x06003C24 RID: 15396
		protected abstract void GenerateSnippetExpression(CodeSnippetExpression e);

		// Token: 0x06003C25 RID: 15397
		protected abstract void GenerateMethodInvokeExpression(CodeMethodInvokeExpression e);

		// Token: 0x06003C26 RID: 15398
		protected abstract void GenerateMethodReferenceExpression(CodeMethodReferenceExpression e);

		// Token: 0x06003C27 RID: 15399
		protected abstract void GenerateEventReferenceExpression(CodeEventReferenceExpression e);

		// Token: 0x06003C28 RID: 15400
		protected abstract void GenerateDelegateInvokeExpression(CodeDelegateInvokeExpression e);

		// Token: 0x06003C29 RID: 15401
		protected abstract void GenerateObjectCreateExpression(CodeObjectCreateExpression e);

		// Token: 0x06003C2A RID: 15402 RVA: 0x000F9260 File Offset: 0x000F7460
		protected virtual void GenerateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributeDeclarations(e.CustomAttributes);
				this.Output.Write(" ");
			}
			this.OutputDirection(e.Direction);
			this.OutputTypeNamePair(e.Type, e.Name);
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x000F92B5 File Offset: 0x000F74B5
		protected virtual void GenerateDirectionExpression(CodeDirectionExpression e)
		{
			this.OutputDirection(e.Direction);
			this.GenerateExpression(e.Expression);
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x000F92D0 File Offset: 0x000F74D0
		protected virtual void GeneratePrimitiveExpression(CodePrimitiveExpression e)
		{
			if (e.Value == null)
			{
				this.Output.Write(this.NullToken);
				return;
			}
			if (e.Value is string)
			{
				this.Output.Write(this.QuoteSnippetString((string)e.Value));
				return;
			}
			if (e.Value is char)
			{
				this.Output.Write("'" + e.Value.ToString() + "'");
				return;
			}
			if (e.Value is byte)
			{
				this.Output.Write(((byte)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is short)
			{
				this.Output.Write(((short)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is int)
			{
				this.Output.Write(((int)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is long)
			{
				this.Output.Write(((long)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is float)
			{
				this.GenerateSingleFloatValue((float)e.Value);
				return;
			}
			if (e.Value is double)
			{
				this.GenerateDoubleValue((double)e.Value);
				return;
			}
			if (e.Value is decimal)
			{
				this.GenerateDecimalValue((decimal)e.Value);
				return;
			}
			if (!(e.Value is bool))
			{
				throw new ArgumentException(SR.GetString("InvalidPrimitiveType", new object[]
				{
					e.Value.GetType().ToString()
				}));
			}
			if ((bool)e.Value)
			{
				this.Output.Write("true");
				return;
			}
			this.Output.Write("false");
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x000F94D9 File Offset: 0x000F76D9
		protected virtual void GenerateSingleFloatValue(float s)
		{
			this.Output.Write(s.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x000F94F7 File Offset: 0x000F76F7
		protected virtual void GenerateDoubleValue(double d)
		{
			this.Output.Write(d.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x000F9515 File Offset: 0x000F7715
		protected virtual void GenerateDecimalValue(decimal d)
		{
			this.Output.Write(d.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x000F952E File Offset: 0x000F772E
		protected virtual void GenerateDefaultValueExpression(CodeDefaultValueExpression e)
		{
		}

		// Token: 0x06003C31 RID: 15409
		protected abstract void GeneratePropertyReferenceExpression(CodePropertyReferenceExpression e);

		// Token: 0x06003C32 RID: 15410
		protected abstract void GeneratePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e);

		// Token: 0x06003C33 RID: 15411
		protected abstract void GenerateThisReferenceExpression(CodeThisReferenceExpression e);

		// Token: 0x06003C34 RID: 15412 RVA: 0x000F9530 File Offset: 0x000F7730
		protected virtual void GenerateTypeReferenceExpression(CodeTypeReferenceExpression e)
		{
			this.OutputType(e.Type);
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x000F953E File Offset: 0x000F773E
		protected virtual void GenerateTypeOfExpression(CodeTypeOfExpression e)
		{
			this.Output.Write("typeof(");
			this.OutputType(e.Type);
			this.Output.Write(")");
		}

		// Token: 0x06003C36 RID: 15414
		protected abstract void GenerateExpressionStatement(CodeExpressionStatement e);

		// Token: 0x06003C37 RID: 15415
		protected abstract void GenerateIterationStatement(CodeIterationStatement e);

		// Token: 0x06003C38 RID: 15416
		protected abstract void GenerateThrowExceptionStatement(CodeThrowExceptionStatement e);

		// Token: 0x06003C39 RID: 15417 RVA: 0x000F956C File Offset: 0x000F776C
		protected virtual void GenerateCommentStatement(CodeCommentStatement e)
		{
			if (e.Comment == null)
			{
				throw new ArgumentException(SR.GetString("Argument_NullComment", new object[]
				{
					"e"
				}), "e");
			}
			this.GenerateComment(e.Comment);
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x000F95A8 File Offset: 0x000F77A8
		protected virtual void GenerateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement e2 = (CodeCommentStatement)obj;
				this.GenerateCommentStatement(e2);
			}
		}

		// Token: 0x06003C3B RID: 15419
		protected abstract void GenerateComment(CodeComment e);

		// Token: 0x06003C3C RID: 15420
		protected abstract void GenerateMethodReturnStatement(CodeMethodReturnStatement e);

		// Token: 0x06003C3D RID: 15421
		protected abstract void GenerateConditionStatement(CodeConditionStatement e);

		// Token: 0x06003C3E RID: 15422
		protected abstract void GenerateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e);

		// Token: 0x06003C3F RID: 15423
		protected abstract void GenerateAssignStatement(CodeAssignStatement e);

		// Token: 0x06003C40 RID: 15424
		protected abstract void GenerateAttachEventStatement(CodeAttachEventStatement e);

		// Token: 0x06003C41 RID: 15425
		protected abstract void GenerateRemoveEventStatement(CodeRemoveEventStatement e);

		// Token: 0x06003C42 RID: 15426
		protected abstract void GenerateGotoStatement(CodeGotoStatement e);

		// Token: 0x06003C43 RID: 15427
		protected abstract void GenerateLabeledStatement(CodeLabeledStatement e);

		// Token: 0x06003C44 RID: 15428 RVA: 0x000F95FC File Offset: 0x000F77FC
		protected virtual void GenerateSnippetStatement(CodeSnippetStatement e)
		{
			this.Output.WriteLine(e.Value);
		}

		// Token: 0x06003C45 RID: 15429
		protected abstract void GenerateVariableDeclarationStatement(CodeVariableDeclarationStatement e);

		// Token: 0x06003C46 RID: 15430
		protected abstract void GenerateLinePragmaStart(CodeLinePragma e);

		// Token: 0x06003C47 RID: 15431
		protected abstract void GenerateLinePragmaEnd(CodeLinePragma e);

		// Token: 0x06003C48 RID: 15432
		protected abstract void GenerateEvent(CodeMemberEvent e, CodeTypeDeclaration c);

		// Token: 0x06003C49 RID: 15433
		protected abstract void GenerateField(CodeMemberField e);

		// Token: 0x06003C4A RID: 15434
		protected abstract void GenerateSnippetMember(CodeSnippetTypeMember e);

		// Token: 0x06003C4B RID: 15435
		protected abstract void GenerateEntryPointMethod(CodeEntryPointMethod e, CodeTypeDeclaration c);

		// Token: 0x06003C4C RID: 15436
		protected abstract void GenerateMethod(CodeMemberMethod e, CodeTypeDeclaration c);

		// Token: 0x06003C4D RID: 15437
		protected abstract void GenerateProperty(CodeMemberProperty e, CodeTypeDeclaration c);

		// Token: 0x06003C4E RID: 15438
		protected abstract void GenerateConstructor(CodeConstructor e, CodeTypeDeclaration c);

		// Token: 0x06003C4F RID: 15439
		protected abstract void GenerateTypeConstructor(CodeTypeConstructor e);

		// Token: 0x06003C50 RID: 15440
		protected abstract void GenerateTypeStart(CodeTypeDeclaration e);

		// Token: 0x06003C51 RID: 15441
		protected abstract void GenerateTypeEnd(CodeTypeDeclaration e);

		// Token: 0x06003C52 RID: 15442 RVA: 0x000F960F File Offset: 0x000F780F
		protected virtual void GenerateCompileUnitStart(CodeCompileUnit e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x000F962B File Offset: 0x000F782B
		protected virtual void GenerateCompileUnitEnd(CodeCompileUnit e)
		{
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x06003C54 RID: 15444
		protected abstract void GenerateNamespaceStart(CodeNamespace e);

		// Token: 0x06003C55 RID: 15445
		protected abstract void GenerateNamespaceEnd(CodeNamespace e);

		// Token: 0x06003C56 RID: 15446
		protected abstract void GenerateNamespaceImport(CodeNamespaceImport e);

		// Token: 0x06003C57 RID: 15447
		protected abstract void GenerateAttributeDeclarationsStart(CodeAttributeDeclarationCollection attributes);

		// Token: 0x06003C58 RID: 15448
		protected abstract void GenerateAttributeDeclarationsEnd(CodeAttributeDeclarationCollection attributes);

		// Token: 0x06003C59 RID: 15449
		protected abstract bool Supports(GeneratorSupport support);

		// Token: 0x06003C5A RID: 15450
		protected abstract bool IsValidIdentifier(string value);

		// Token: 0x06003C5B RID: 15451 RVA: 0x000F9647 File Offset: 0x000F7847
		protected virtual void ValidateIdentifier(string value)
		{
			if (!this.IsValidIdentifier(value))
			{
				throw new ArgumentException(SR.GetString("InvalidIdentifier", new object[]
				{
					value
				}));
			}
		}

		// Token: 0x06003C5C RID: 15452
		protected abstract string CreateEscapedIdentifier(string value);

		// Token: 0x06003C5D RID: 15453
		protected abstract string CreateValidIdentifier(string value);

		// Token: 0x06003C5E RID: 15454
		protected abstract string GetTypeOutput(CodeTypeReference value);

		// Token: 0x06003C5F RID: 15455
		protected abstract string QuoteSnippetString(string value);

		// Token: 0x06003C60 RID: 15456 RVA: 0x000F966C File Offset: 0x000F786C
		public static bool IsValidLanguageIndependentIdentifier(string value)
		{
			return CodeGenerator.IsValidTypeNameOrIdentifier(value, false);
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x000F9675 File Offset: 0x000F7875
		internal static bool IsValidLanguageIndependentTypeName(string value)
		{
			return CodeGenerator.IsValidTypeNameOrIdentifier(value, true);
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x000F9680 File Offset: 0x000F7880
		private static bool IsValidTypeNameOrIdentifier(string value, bool isTypeName)
		{
			bool flag = true;
			if (value.Length == 0)
			{
				return false;
			}
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				switch (char.GetUnicodeCategory(c))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.LetterNumber:
					flag = false;
					break;
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.ConnectorPunctuation:
					if (flag && c != '_')
					{
						return false;
					}
					flag = false;
					break;
				case UnicodeCategory.EnclosingMark:
				case UnicodeCategory.OtherNumber:
				case UnicodeCategory.SpaceSeparator:
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Control:
				case UnicodeCategory.Format:
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_88;
				default:
					goto IL_88;
				}
				IL_97:
				i++;
				continue;
				IL_88:
				if (!isTypeName || !CodeGenerator.IsSpecialTypeChar(c, ref flag))
				{
					return false;
				}
				goto IL_97;
			}
			return true;
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x000F9738 File Offset: 0x000F7938
		private static bool IsSpecialTypeChar(char ch, ref bool nextMustBeStartChar)
		{
			if (ch <= '>')
			{
				switch (ch)
				{
				case '$':
				case '&':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
					break;
				case '%':
				case '\'':
				case '(':
				case ')':
					return false;
				default:
					switch (ch)
					{
					case ':':
					case '<':
					case '>':
						break;
					case ';':
					case '=':
						return false;
					default:
						return false;
					}
					break;
				}
			}
			else if (ch != '[' && ch != ']')
			{
				if (ch != '`')
				{
					return false;
				}
				return true;
			}
			nextMustBeStartChar = true;
			return true;
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x000F97B8 File Offset: 0x000F79B8
		public static void ValidateIdentifiers(CodeObject e)
		{
			CodeValidator codeValidator = new CodeValidator();
			codeValidator.ValidateIdentifiers(e);
		}

		// Token: 0x04002C6F RID: 11375
		private const int ParameterMultilineThreshold = 15;

		// Token: 0x04002C70 RID: 11376
		private IndentedTextWriter output;

		// Token: 0x04002C71 RID: 11377
		private CodeGeneratorOptions options;

		// Token: 0x04002C72 RID: 11378
		private CodeTypeDeclaration currentClass;

		// Token: 0x04002C73 RID: 11379
		private CodeTypeMember currentMember;

		// Token: 0x04002C74 RID: 11380
		private bool inNestedBinary;
	}
}
