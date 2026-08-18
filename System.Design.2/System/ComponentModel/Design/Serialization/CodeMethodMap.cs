using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001D9 RID: 473
	internal class CodeMethodMap
	{
		// Token: 0x060011DE RID: 4574 RVA: 0x0006561A File Offset: 0x0006381A
		internal CodeMethodMap(CodeMemberMethod method) : this(null, method)
		{
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00065624 File Offset: 0x00063824
		internal CodeMethodMap(CodeStatementCollection targetStatements, CodeMemberMethod method)
		{
			this._method = method;
			if (targetStatements != null)
			{
				this._targetStatements = targetStatements;
				return;
			}
			this._targetStatements = this._method.Statements;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0006564F File Offset: 0x0006384F
		internal CodeStatementCollection BeginStatements
		{
			get
			{
				if (this._begin == null)
				{
					this._begin = new CodeStatementCollection();
				}
				return this._begin;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x0006566A File Offset: 0x0006386A
		internal CodeStatementCollection EndStatements
		{
			get
			{
				if (this._end == null)
				{
					this._end = new CodeStatementCollection();
				}
				return this._end;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00065685 File Offset: 0x00063885
		internal CodeStatementCollection ContainerStatements
		{
			get
			{
				if (this._container == null)
				{
					this._container = new CodeStatementCollection();
				}
				return this._container;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x000656A0 File Offset: 0x000638A0
		internal CodeMemberMethod Method
		{
			get
			{
				return this._method;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x000656A8 File Offset: 0x000638A8
		internal CodeStatementCollection Statements
		{
			get
			{
				if (this._statements == null)
				{
					this._statements = new CodeStatementCollection();
				}
				return this._statements;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x000656C3 File Offset: 0x000638C3
		internal CodeStatementCollection LocalVariables
		{
			get
			{
				if (this._locals == null)
				{
					this._locals = new CodeStatementCollection();
				}
				return this._locals;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x000656DE File Offset: 0x000638DE
		internal CodeStatementCollection FieldAssignments
		{
			get
			{
				if (this._fields == null)
				{
					this._fields = new CodeStatementCollection();
				}
				return this._fields;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x000656F9 File Offset: 0x000638F9
		internal CodeStatementCollection VariableAssignments
		{
			get
			{
				if (this._variables == null)
				{
					this._variables = new CodeStatementCollection();
				}
				return this._variables;
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00065714 File Offset: 0x00063914
		internal void Add(CodeStatementCollection statements)
		{
			foreach (object obj in statements)
			{
				CodeStatement codeStatement = (CodeStatement)obj;
				string text = codeStatement.UserData["IContainer"] as string;
				if (text != null && text == "IContainer")
				{
					this.ContainerStatements.Add(codeStatement);
				}
				else if (codeStatement is CodeAssignStatement && ((CodeAssignStatement)codeStatement).Left is CodeFieldReferenceExpression)
				{
					this.FieldAssignments.Add(codeStatement);
				}
				else if (codeStatement is CodeAssignStatement && ((CodeAssignStatement)codeStatement).Left is CodeVariableReferenceExpression)
				{
					this.VariableAssignments.Add(codeStatement);
				}
				else if (codeStatement is CodeVariableDeclarationStatement)
				{
					this.LocalVariables.Add(codeStatement);
				}
				else
				{
					string text2 = codeStatement.UserData["statement-ordering"] as string;
					if (text2 != null)
					{
						if (!(text2 == "begin"))
						{
							if (!(text2 == "end"))
							{
								if (!(text2 == "default"))
								{
								}
								this.Statements.Add(codeStatement);
							}
							else
							{
								this.EndStatements.Add(codeStatement);
							}
						}
						else
						{
							this.BeginStatements.Add(codeStatement);
						}
					}
					else
					{
						this.Statements.Add(codeStatement);
					}
				}
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0006589C File Offset: 0x00063A9C
		internal void Combine()
		{
			if (this._container != null)
			{
				this._targetStatements.AddRange(this._container);
			}
			if (this._locals != null)
			{
				this._targetStatements.AddRange(this._locals);
			}
			if (this._fields != null)
			{
				this._targetStatements.AddRange(this._fields);
			}
			if (this._variables != null)
			{
				this._targetStatements.AddRange(this._variables);
			}
			if (this._begin != null)
			{
				this._targetStatements.AddRange(this._begin);
			}
			if (this._statements != null)
			{
				this._targetStatements.AddRange(this._statements);
			}
			if (this._end != null)
			{
				this._targetStatements.AddRange(this._end);
			}
		}

		// Token: 0x040009DB RID: 2523
		private CodeStatementCollection _container;

		// Token: 0x040009DC RID: 2524
		private CodeStatementCollection _begin;

		// Token: 0x040009DD RID: 2525
		private CodeStatementCollection _end;

		// Token: 0x040009DE RID: 2526
		private CodeStatementCollection _statements;

		// Token: 0x040009DF RID: 2527
		private CodeStatementCollection _locals;

		// Token: 0x040009E0 RID: 2528
		private CodeStatementCollection _fields;

		// Token: 0x040009E1 RID: 2529
		private CodeStatementCollection _variables;

		// Token: 0x040009E2 RID: 2530
		private CodeStatementCollection _targetStatements;

		// Token: 0x040009E3 RID: 2531
		private CodeMemberMethod _method;
	}
}
