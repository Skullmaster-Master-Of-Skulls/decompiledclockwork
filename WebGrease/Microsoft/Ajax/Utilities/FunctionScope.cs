using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000094 RID: 148
	public sealed class FunctionScope : ActivationObject
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x00029280 File Offset: 0x00027480
		internal FunctionScope(ActivationObject parent, bool isExpression, CodeSettings settings, FunctionObject funcObj) : base(parent, settings)
		{
			base.ScopeType = ScopeType.Function;
			this.m_refScopes = new HashSet<ActivationObject>();
			if (isExpression)
			{
				this.AddReference(base.Parent);
			}
			base.Owner = funcObj;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000292B3 File Offset: 0x000274B3
		public override void DeclareScope()
		{
			if (((FunctionObject)base.Owner).EnclosingScope == this)
			{
				this.DefineParameters();
				base.DefineLexicalDeclarations();
				this.DefineArgumentsObject();
				base.DefineVarDeclarations();
				return;
			}
			this.DefineFunctionExpressionName();
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000292E8 File Offset: 0x000274E8
		private void DefineFunctionExpressionName()
		{
			FunctionObject functionObject = (FunctionObject)base.Owner;
			JSVariableField jsvariableField = this.CreateField(functionObject.Binding.Name, functionObject, FieldAttributes.PrivateScope);
			jsvariableField.IsFunction = true;
			jsvariableField.OriginalContext = functionObject.Binding.Context;
			functionObject.Binding.VariableField = jsvariableField;
			base.AddField(jsvariableField);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00029344 File Offset: 0x00027544
		private void DefineParameters()
		{
			FunctionObject functionObject = (FunctionObject)base.Owner;
			if (functionObject.ParameterDeclarations != null)
			{
				foreach (AstNode astNode in functionObject.ParameterDeclarations)
				{
					ParameterDeclaration parameterDeclaration = (ParameterDeclaration)astNode;
					foreach (BindingIdentifier bindingIdentifier in BindingsVisitor.Bindings(parameterDeclaration.Binding))
					{
						JSVariableField jsvariableField = this[bindingIdentifier.Name];
						if (jsvariableField == null)
						{
							jsvariableField = new JSVariableField(FieldType.Argument, bindingIdentifier.Name, FieldAttributes.PrivateScope, null)
							{
								Position = parameterDeclaration.Position,
								OriginalContext = parameterDeclaration.Context,
								CanCrunch = !bindingIdentifier.RenameNotAllowed
							};
							base.AddField(jsvariableField);
						}
						bindingIdentifier.VariableField = jsvariableField;
						jsvariableField.Declarations.Add(bindingIdentifier);
					}
				}
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00029458 File Offset: 0x00027658
		private void DefineArgumentsObject()
		{
			if (this["arguments"] == null)
			{
				base.AddField(new JSVariableField(FieldType.Arguments, "arguments", FieldAttributes.PrivateScope, null));
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0002947B File Offset: 0x0002767B
		public override JSVariableField CreateField(string name, object value, FieldAttributes attributes)
		{
			return new JSVariableField(FieldType.Local, name, attributes, value);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00029486 File Offset: 0x00027686
		internal void AddReference(ActivationObject scope)
		{
			while (scope != null && scope is BlockScope)
			{
				scope = scope.Parent;
			}
			if (scope != null)
			{
				this.m_refScopes.Add(scope);
			}
		}

		// Token: 0x0400033D RID: 829
		private HashSet<ActivationObject> m_refScopes;
	}
}
