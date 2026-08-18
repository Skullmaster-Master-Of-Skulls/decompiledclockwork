using System;
using System.CodeDom;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Globalization;

namespace System.Data.Design
{
	// Token: 0x02000259 RID: 601
	internal class QueryGenerator : QueryGeneratorBase
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x0007C1D2 File Offset: 0x0007A3D2
		internal QueryGenerator(TypedDataSourceCodeGenerator codeGenerator) : base(codeGenerator)
		{
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0007E55C File Offset: 0x0007C75C
		internal override CodeMemberMethod Generate()
		{
			if (this.methodSource == null)
			{
				throw new InternalException("MethodSource should not be null.");
			}
			if (StringUtil.Empty(base.ContainerParameterName))
			{
				throw new InternalException("ContainerParameterName should not be empty.");
			}
			if (this.methodSource.SelectCommand == null)
			{
				this.codeGenerator.ProblemList.Add(new DSGeneratorProblem(SR.GetString("CG_MainSelectCommandNotSet", new object[]
				{
					base.DesignTable.Name
				}), ProblemSeverity.NonFatalError, this.methodSource));
				return null;
			}
			this.activeCommand = this.methodSource.SelectCommand;
			this.methodAttributes = MemberAttributes.Overloaded;
			if (this.getMethod)
			{
				this.methodAttributes |= base.MethodSource.GetMethodModifier;
			}
			else
			{
				this.methodAttributes |= base.MethodSource.Modifier;
			}
			if (this.codeProvider == null)
			{
				this.codeProvider = this.codeGenerator.CodeProvider;
			}
			this.nameHandler = new GenericNameHandler(new string[]
			{
				base.MethodName,
				QueryGeneratorBase.returnVariableName
			}, this.codeProvider);
			return this.GenerateInternal();
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0007E67C File Offset: 0x0007C87C
		private CodeMemberMethod GenerateInternal()
		{
			this.returnType = typeof(int);
			CodeMemberMethod codeMemberMethod;
			if (this.getMethod)
			{
				codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.Type(base.ContainerParameterTypeName), base.MethodName, this.methodAttributes);
			}
			else
			{
				codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.Type(this.returnType), base.MethodName, this.methodAttributes);
			}
			codeMemberMethod.CustomAttributes.Add(CodeGenHelper.AttributeDecl(typeof(HelpKeywordAttribute).FullName, CodeGenHelper.Str("vs.data.TableAdapter")));
			this.AddParametersToMethod(codeMemberMethod);
			if (this.declarationOnly)
			{
				base.AddThrowsClauseIfNeeded(codeMemberMethod);
				return codeMemberMethod;
			}
			this.AddCustomAttributesToMethod(codeMemberMethod);
			if (this.AddStatementsToMethod(codeMemberMethod))
			{
				return codeMemberMethod;
			}
			return null;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0007E734 File Offset: 0x0007C934
		private void AddParametersToMethod(CodeMemberMethod dbMethod)
		{
			if (!this.getMethod)
			{
				string name = this.nameHandler.AddNameToList(base.ContainerParameterName);
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(CodeGenHelper.Type(base.ContainerParameterTypeName), name);
				dbMethod.Parameters.Add(codeParameterDeclarationExpression);
			}
			if (base.GeneratePagingMethod)
			{
				string name2 = this.nameHandler.AddNameToList(QueryGeneratorBase.startRecordParameterName);
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(int)), name2);
				dbMethod.Parameters.Add(codeParameterDeclarationExpression);
				string name3 = this.nameHandler.AddNameToList(QueryGeneratorBase.maxRecordsParameterName);
				codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(int)), name3);
				dbMethod.Parameters.Add(codeParameterDeclarationExpression);
			}
			if (this.activeCommand.Parameters == null)
			{
				return;
			}
			DesignConnection designConnection = (DesignConnection)this.methodSource.Connection;
			if (designConnection == null)
			{
				throw new InternalException(string.Format(CultureInfo.CurrentCulture, "Connection for query {0} is null.", new object[]
				{
					this.methodSource.Name
				}));
			}
			string parameterPrefix = designConnection.ParameterPrefix;
			foreach (object obj in this.activeCommand.Parameters)
			{
				DesignParameter designParameter = (DesignParameter)obj;
				if (designParameter.Direction != ParameterDirection.ReturnValue)
				{
					Type parameterUrtType = base.GetParameterUrtType(designParameter);
					string name4 = this.nameHandler.AddParameterNameToList(designParameter.ParameterName, parameterPrefix);
					CodeTypeReference type;
					if (designParameter.AllowDbNull && parameterUrtType.IsValueType)
					{
						type = CodeGenHelper.NullableType(parameterUrtType);
					}
					else
					{
						type = CodeGenHelper.Type(parameterUrtType);
					}
					CodeParameterDeclarationExpression codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(type, name4);
					codeParameterDeclarationExpression.Direction = CodeGenHelper.ParameterDirectionToFieldDirection(designParameter.Direction);
					dbMethod.Parameters.Add(codeParameterDeclarationExpression);
				}
			}
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0007E918 File Offset: 0x0007CB18
		private bool AddStatementsToMethod(CodeMemberMethod dbMethod)
		{
			if (!this.AddSetCommandStatements(dbMethod.Statements))
			{
				return false;
			}
			if (!this.AddSetParametersStatements(dbMethod.Statements))
			{
				return false;
			}
			if (!this.AddClearStatements(dbMethod.Statements))
			{
				return false;
			}
			bool flag;
			if (base.GeneratePagingMethod)
			{
				flag = this.AddExecuteCommandStatementsForPaging(dbMethod.Statements);
			}
			else
			{
				flag = this.AddExecuteCommandStatements(dbMethod.Statements);
			}
			return flag && this.AddSetReturnParamValuesStatements(dbMethod.Statements) && this.AddReturnStatements(dbMethod.Statements);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0007E9AC File Offset: 0x0007CBAC
		private bool AddSetCommandStatements(IList statements)
		{
			Type type = base.ProviderFactory.CreateCommand().GetType();
			statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "SelectCommand"), CodeGenHelper.ArrayIndexer(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionPropertyName), CodeGenHelper.Primitive(base.CommandIndex))));
			return true;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0007EA14 File Offset: 0x0007CC14
		private bool AddSetParametersStatements(IList statements)
		{
			int num = 0;
			if (this.activeCommand.Parameters != null)
			{
				num = this.activeCommand.Parameters.Count;
			}
			for (int i = 0; i < num; i++)
			{
				DesignParameter designParameter = this.activeCommand.Parameters[i];
				if (designParameter == null)
				{
					throw new DataSourceGeneratorException("Parameter type is not DesignParameter.");
				}
				if (designParameter.Direction == ParameterDirection.Input || designParameter.Direction == ParameterDirection.InputOutput)
				{
					string nameFromList = this.nameHandler.GetNameFromList(designParameter.ParameterName);
					CodeExpression cmdExpression = CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "SelectCommand");
					base.AddSetParameterStatements(designParameter, nameFromList, cmdExpression, i, statements);
				}
			}
			return true;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0007EABC File Offset: 0x0007CCBC
		private bool AddClearStatements(IList statements)
		{
			if (!this.getMethod)
			{
				CodeStatement trueStm;
				if (this.containerParamType == typeof(DataTable))
				{
					trueStm = CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Argument(base.ContainerParameterName), "Clear", new CodeExpression[0]));
				}
				else
				{
					if (!(this.containerParamType == typeof(DataSet)))
					{
						throw new InternalException("Unknown containerParameterType.");
					}
					trueStm = CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Argument(base.ContainerParameterName), base.DesignTable.GeneratorTablePropName), "Clear", new CodeExpression[0]));
				}
				statements.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.ClearBeforeFillPropertyName), CodeGenHelper.Primitive(true)), trueStm));
			}
			return true;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0007EB94 File Offset: 0x0007CD94
		private bool AddExecuteCommandStatements(IList statements)
		{
			if (this.getMethod)
			{
				CodeExpression[] parameters = new CodeExpression[0];
				bool flag = this.designTable != null && this.designTable.HasAnyExpressionColumn;
				if (flag)
				{
					parameters = new CodeExpression[]
					{
						CodeGenHelper.Primitive(true)
					};
				}
				statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.Type(base.ContainerParameterTypeName), base.ContainerParameterName, CodeGenHelper.New(CodeGenHelper.Type(base.ContainerParameterTypeName), parameters)));
			}
			CodeExpression[] parameters2 = new CodeExpression[]
			{
				CodeGenHelper.Variable(base.ContainerParameterName)
			};
			if (!this.getMethod)
			{
				statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(int)), QueryGeneratorBase.returnVariableName, CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "Fill", parameters2)));
			}
			else
			{
				statements.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "Fill", parameters2)));
			}
			return true;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0007EC90 File Offset: 0x0007CE90
		private bool AddExecuteCommandStatementsForPaging(IList statements)
		{
			if (this.containerParamType == typeof(DataTable))
			{
				statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.Type(this.codeGenerator.DataSourceName), this.nameHandler.AddNameToList("dataSet"), CodeGenHelper.New(CodeGenHelper.Type(this.codeGenerator.DataSourceName), new CodeExpression[0])));
			}
			CodeExpression[] array = new CodeExpression[4];
			if (this.containerParamType == typeof(DataTable))
			{
				array[0] = CodeGenHelper.Variable(this.nameHandler.GetNameFromList("dataSet"));
			}
			else
			{
				array[0] = CodeGenHelper.Argument(base.ContainerParameterName);
			}
			array[1] = CodeGenHelper.Argument(this.nameHandler.GetNameFromList(QueryGeneratorBase.startRecordParameterName));
			array[2] = CodeGenHelper.Argument(this.nameHandler.GetNameFromList(QueryGeneratorBase.maxRecordsParameterName));
			array[3] = CodeGenHelper.Str("Table");
			if (!this.getMethod)
			{
				statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(int)), QueryGeneratorBase.returnVariableName, CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "Fill", array)));
			}
			else
			{
				statements.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "Fill", array)));
			}
			if (this.containerParamType == typeof(DataTable) && !this.getMethod)
			{
				CodeStatement initStmt = CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(int)), "i", CodeGenHelper.Primitive(0));
				CodeExpression testExpression = CodeGenHelper.Less(CodeGenHelper.Variable("i"), CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Variable(this.nameHandler.GetNameFromList("dataSet")), base.DesignTable.GeneratorName), "Rows"), "Count"));
				CodeStatement incrementStmt = CodeGenHelper.Assign(CodeGenHelper.Variable("i"), CodeGenHelper.BinOperator(CodeGenHelper.Variable("i"), CodeBinaryOperatorType.Add, CodeGenHelper.Primitive(1)));
				CodeStatement codeStatement = CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Argument(base.ContainerParameterName), "ImportRow", new CodeExpression[]
				{
					CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Variable(this.nameHandler.GetNameFromList("dataSet")), base.DesignTable.GeneratorName), "Rows"), CodeGenHelper.Variable("i"))
				}));
				statements.Add(CodeGenHelper.ForLoop(initStmt, testExpression, incrementStmt, new CodeStatement[]
				{
					codeStatement
				}));
			}
			return true;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0007EF24 File Offset: 0x0007D124
		protected bool AddSetReturnParamValuesStatements(IList statements)
		{
			CodeExpression commandExpression = CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "SelectCommand");
			return base.AddSetReturnParamValuesStatements(statements, commandExpression);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0007EF54 File Offset: 0x0007D154
		private bool AddReturnStatements(IList statements)
		{
			if (this.getMethod)
			{
				if (base.GeneratePagingMethod)
				{
					statements.Add(CodeGenHelper.Return(CodeGenHelper.Property(CodeGenHelper.Variable(this.nameHandler.GetNameFromList("dataSet")), base.DesignTable.GeneratorName)));
				}
				else
				{
					statements.Add(CodeGenHelper.Return(CodeGenHelper.Variable(base.ContainerParameterName)));
				}
			}
			else
			{
				statements.Add(CodeGenHelper.Return(CodeGenHelper.Variable(QueryGeneratorBase.returnVariableName)));
			}
			return true;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0007EFD4 File Offset: 0x0007D1D4
		private void AddCustomAttributesToMethod(CodeMemberMethod dbMethod)
		{
			bool flag = false;
			if (this.methodSource.EnableWebMethods && this.getMethod)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Web.Services.WebMethod");
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Description", CodeGenHelper.Str(this.methodSource.WebMethodDescription)));
				dbMethod.CustomAttributes.Add(codeAttributeDeclaration);
			}
			if (base.GeneratePagingMethod)
			{
				return;
			}
			if (!this.getMethod && base.ContainerParameterType != typeof(DataTable))
			{
				return;
			}
			if (base.MethodSource == base.DesignTable.MainSource)
			{
				flag = true;
			}
			DataObjectMethodType dataObjectMethodType;
			if (this.getMethod)
			{
				dataObjectMethodType = DataObjectMethodType.Select;
			}
			else
			{
				dataObjectMethodType = DataObjectMethodType.Fill;
			}
			dbMethod.CustomAttributes.Add(new CodeAttributeDeclaration(CodeGenHelper.GlobalType(typeof(DataObjectMethodAttribute)), new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataObjectMethodType)), dataObjectMethodType.ToString())),
				new CodeAttributeArgument(CodeGenHelper.Primitive(flag))
			}));
		}
	}
}
