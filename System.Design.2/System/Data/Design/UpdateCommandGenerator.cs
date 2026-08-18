using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;

namespace System.Data.Design
{
	// Token: 0x0200027F RID: 639
	internal class UpdateCommandGenerator : QueryGeneratorBase
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0008A404 File Offset: 0x00088604
		// (set) Token: 0x06001839 RID: 6201 RVA: 0x0008A40C File Offset: 0x0008860C
		internal bool GenerateOverloadWithoutCurrentPKParameters
		{
			get
			{
				return this.generateOverloadWithoutCurrentPKParameters;
			}
			set
			{
				this.generateOverloadWithoutCurrentPKParameters = value;
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0007C1D2 File Offset: 0x0007A3D2
		internal UpdateCommandGenerator(TypedDataSourceCodeGenerator codeGenerator) : base(codeGenerator)
		{
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0008A418 File Offset: 0x00088618
		internal override CodeMemberMethod Generate()
		{
			if (this.methodSource == null)
			{
				throw new InternalException("MethodSource should not be null.");
			}
			if (base.MethodType == MethodTypeEnum.ColumnParameters && this.activeCommand == null)
			{
				throw new InternalException("ActiveCommand should not be null.");
			}
			this.methodAttributes = (base.MethodSource.Modifier | MemberAttributes.Overloaded);
			this.returnType = typeof(int);
			CodeDomProvider codeProvider = (this.codeProvider != null) ? this.codeGenerator.CodeProvider : base.CodeProvider;
			this.nameHandler = new GenericNameHandler(new string[]
			{
				base.MethodName,
				QueryGeneratorBase.commandVariableName,
				QueryGeneratorBase.returnVariableName
			}, codeProvider);
			return this.GenerateInternal();
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0008A4C8 File Offset: 0x000886C8
		private CodeMemberMethod GenerateInternal()
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.Type(this.returnType), base.MethodName, this.methodAttributes);
			codeMemberMethod.CustomAttributes.Add(CodeGenHelper.AttributeDecl(typeof(HelpKeywordAttribute).FullName, CodeGenHelper.Str("vs.data.TableAdapter")));
			this.AddParametersToMethod(codeMemberMethod);
			if (this.declarationOnly)
			{
				return codeMemberMethod;
			}
			this.AddCustomAttributesToMethod(codeMemberMethod);
			if (this.AddStatementsToMethod(codeMemberMethod))
			{
				return codeMemberMethod;
			}
			return null;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0008A544 File Offset: 0x00088744
		private void AddParametersToMethod(CodeMemberMethod dbMethod)
		{
			DesignConnection designConnection = (DesignConnection)this.methodSource.Connection;
			if (designConnection == null)
			{
				throw new InternalException(string.Format(CultureInfo.CurrentCulture, "Connection for query {0} is null.", new object[]
				{
					this.methodSource.Name
				}));
			}
			string parameterPrefix = designConnection.ParameterPrefix;
			if (base.MethodType == MethodTypeEnum.ColumnParameters)
			{
				if (this.activeCommand.Parameters == null)
				{
					return;
				}
				using (IEnumerator enumerator = this.activeCommand.Parameters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DesignParameter designParameter = (DesignParameter)obj;
						if (designParameter.Direction != ParameterDirection.ReturnValue && !designParameter.SourceColumnNullMapping && (!this.GenerateOverloadWithoutCurrentPKParameters || designParameter.SourceVersion != DataRowVersion.Current || !this.IsPrimaryColumn(designParameter.SourceColumn) || this.GetOriginalVersionParameter(designParameter) == null))
						{
							Type parameterUrtType = base.GetParameterUrtType(designParameter);
							string name = this.nameHandler.AddParameterNameToList(designParameter.ParameterName, parameterPrefix);
							CodeTypeReference type;
							if (designParameter.AllowDbNull && parameterUrtType.IsValueType)
							{
								type = CodeGenHelper.NullableType(parameterUrtType);
							}
							else
							{
								type = CodeGenHelper.Type(parameterUrtType);
							}
							CodeParameterDeclarationExpression codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(type, name);
							codeParameterDeclarationExpression.Direction = CodeGenHelper.ParameterDirectionToFieldDirection(designParameter.Direction);
							dbMethod.Parameters.Add(codeParameterDeclarationExpression);
						}
					}
					return;
				}
			}
			string name2 = this.nameHandler.AddParameterNameToList(base.UpdateParameterName, parameterPrefix);
			CodeParameterDeclarationExpression value;
			if (base.UpdateParameterTypeName != null)
			{
				value = CodeGenHelper.ParameterDecl(CodeGenHelper.Type(base.UpdateParameterTypeName), name2);
			}
			else
			{
				value = CodeGenHelper.ParameterDecl(base.UpdateParameterTypeReference, name2);
			}
			dbMethod.Parameters.Add(value);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0008A710 File Offset: 0x00088910
		private bool AddStatementsToMethod(CodeMemberMethod dbMethod)
		{
			if (this.GenerateOverloadWithoutCurrentPKParameters)
			{
				return this.AddCallOverloadUpdateStm(dbMethod);
			}
			if (base.MethodType == MethodTypeEnum.ColumnParameters && !this.AddSetParametersStatements(dbMethod.Statements))
			{
				return false;
			}
			if (!this.AddExecuteCommandStatements(dbMethod.Statements))
			{
				return false;
			}
			if (base.MethodType == MethodTypeEnum.ColumnParameters)
			{
				if (!this.AddSetReturnParamValuesStatements(dbMethod.Statements))
				{
					return false;
				}
				if (!this.AddReturnStatements(dbMethod.Statements))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0008A788 File Offset: 0x00088988
		private bool AddCallOverloadUpdateStm(CodeMemberMethod dbMethod)
		{
			int num = 0;
			if (this.activeCommand.Parameters != null)
			{
				num = this.activeCommand.Parameters.Count;
			}
			if (num <= 0)
			{
				return false;
			}
			List<CodeExpression> list = new List<CodeExpression>();
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				DesignParameter designParameter = this.activeCommand.Parameters[i];
				if (designParameter == null)
				{
					throw new DataSourceGeneratorException("Parameter type is not DesignParameter.");
				}
				if ((designParameter.Direction == ParameterDirection.Input || designParameter.Direction == ParameterDirection.InputOutput) && !designParameter.SourceColumnNullMapping)
				{
					if (designParameter.SourceVersion == DataRowVersion.Current && this.IsPrimaryColumn(designParameter.SourceColumn))
					{
						DesignParameter originalVersionParameter = this.GetOriginalVersionParameter(designParameter);
						if (originalVersionParameter != null)
						{
							flag = true;
							designParameter = originalVersionParameter;
						}
					}
					if (designParameter != null)
					{
						string nameFromList = this.nameHandler.GetNameFromList(designParameter.ParameterName);
						list.Add(CodeGenHelper.Argument(nameFromList));
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			CodeStatement value = CodeGenHelper.Return(CodeGenHelper.MethodCall(CodeGenHelper.This(), "Update", list.ToArray()));
			dbMethod.Statements.Add(value);
			return true;
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0008A8A0 File Offset: 0x00088AA0
		private DesignParameter GetOriginalVersionParameter(DesignParameter currentVersionParameter)
		{
			if (currentVersionParameter == null || currentVersionParameter.SourceVersion != DataRowVersion.Current)
			{
				throw new InternalException("Invalid argutment currentVersionParameter");
			}
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
				if ((designParameter.Direction == ParameterDirection.Input || designParameter.Direction == ParameterDirection.InputOutput) && !designParameter.SourceColumnNullMapping && designParameter.SourceVersion == DataRowVersion.Original && StringUtil.EqualValue(designParameter.SourceColumn, currentVersionParameter.SourceColumn))
				{
					return designParameter;
				}
			}
			return null;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0008A954 File Offset: 0x00088B54
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
				if ((designParameter.Direction == ParameterDirection.Input || designParameter.Direction == ParameterDirection.InputOutput) && !designParameter.SourceColumnNullMapping)
				{
					string nameFromList = this.nameHandler.GetNameFromList(designParameter.ParameterName);
					CodeExpression cmdExpression = CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), base.UpdateCommandName);
					DesignParameter isNullParameter = null;
					int isNullParameterIndex = 0;
					if (designParameter.SourceVersion == DataRowVersion.Original)
					{
						isNullParameter = this.FindCorrespondingIsNullParameter(designParameter, out isNullParameterIndex);
					}
					base.AddSetParameterStatements(designParameter, nameFromList, isNullParameter, cmdExpression, i, isNullParameterIndex, statements);
				}
			}
			return true;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0008AA2C File Offset: 0x00088C2C
		private bool AddExecuteCommandStatements(IList statements)
		{
			if (base.MethodType == MethodTypeEnum.ColumnParameters)
			{
				CodeStatement[] array = new CodeStatement[1];
				CodeStatement[] array2 = new CodeStatement[1];
				statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(ConnectionState)), this.nameHandler.AddNameToList("previousConnectionState"), CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), base.UpdateCommandName), "Connection"), "State")));
				statements.Add(CodeGenHelper.If(CodeGenHelper.IdNotEQ(CodeGenHelper.BitwiseAnd(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), base.UpdateCommandName), "Connection"), "State"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ConnectionState)), "Open")), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ConnectionState)), "Open")), CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), base.UpdateCommandName), "Connection"), "Open"))));
				array[0] = CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(int)), QueryGeneratorBase.returnVariableName, CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), base.UpdateCommandName), "ExecuteNonQuery", new CodeExpression[0]));
				array2[0] = CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.Variable(this.nameHandler.GetNameFromList("previousConnectionState")), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ConnectionState)), "Closed")), CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), base.UpdateCommandName), "Connection"), "Close")));
				statements.Add(CodeGenHelper.Try(array, new CodeCatchClause[0], array2));
			}
			else if (StringUtil.EqualValue(base.UpdateParameterTypeReference.BaseType, typeof(DataRow).FullName) && base.UpdateParameterTypeReference.ArrayRank == 0)
			{
				statements.Add(CodeGenHelper.Return(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "Update", new CodeExpression[]
				{
					CodeGenHelper.NewArray(base.UpdateParameterTypeReference, new CodeExpression[]
					{
						CodeGenHelper.Argument(base.UpdateParameterName)
					})
				})));
			}
			else if (StringUtil.EqualValue(base.UpdateParameterTypeReference.BaseType, typeof(DataSet).FullName))
			{
				statements.Add(CodeGenHelper.Return(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "Update", new CodeExpression[]
				{
					CodeGenHelper.Argument(base.UpdateParameterName),
					CodeGenHelper.Str(base.DesignTable.Name)
				})));
			}
			else
			{
				statements.Add(CodeGenHelper.Return(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "Update", new CodeExpression[]
				{
					CodeGenHelper.Argument(base.UpdateParameterName)
				})));
			}
			return true;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0008AD4C File Offset: 0x00088F4C
		protected bool AddSetReturnParamValuesStatements(IList statements)
		{
			CodeExpression commandExpression = CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), base.UpdateCommandName);
			CodeTryCatchFinallyStatement codeTryCatchFinallyStatement = (CodeTryCatchFinallyStatement)statements[statements.Count - 1];
			return base.AddSetReturnParamValuesStatements(codeTryCatchFinallyStatement.TryStatements, commandExpression);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0008AD98 File Offset: 0x00088F98
		private bool AddReturnStatements(IList statements)
		{
			CodeTryCatchFinallyStatement codeTryCatchFinallyStatement = (CodeTryCatchFinallyStatement)statements[statements.Count - 1];
			codeTryCatchFinallyStatement.TryStatements.Add(CodeGenHelper.Return(CodeGenHelper.Variable(QueryGeneratorBase.returnVariableName)));
			return true;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0008ADD8 File Offset: 0x00088FD8
		private void AddCustomAttributesToMethod(CodeMemberMethod dbMethod)
		{
			DataObjectMethodType dataObjectMethodType = DataObjectMethodType.Update;
			if (this.methodSource.EnableWebMethods)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Web.Services.WebMethod");
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Description", CodeGenHelper.Str(this.methodSource.WebMethodDescription)));
				dbMethod.CustomAttributes.Add(codeAttributeDeclaration);
			}
			if (base.MethodType == MethodTypeEnum.GenericUpdate)
			{
				return;
			}
			if (this.activeCommand == this.methodSource.DeleteCommand)
			{
				dataObjectMethodType = DataObjectMethodType.Delete;
			}
			else if (this.activeCommand == this.methodSource.InsertCommand)
			{
				dataObjectMethodType = DataObjectMethodType.Insert;
			}
			else if (this.activeCommand == this.methodSource.UpdateCommand)
			{
				dataObjectMethodType = DataObjectMethodType.Update;
			}
			dbMethod.CustomAttributes.Add(new CodeAttributeDeclaration(CodeGenHelper.GlobalType(typeof(DataObjectMethodAttribute)), new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataObjectMethodType)), dataObjectMethodType.ToString())),
				new CodeAttributeArgument(CodeGenHelper.Primitive(true))
			}));
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0008AEE4 File Offset: 0x000890E4
		private DesignParameter FindCorrespondingIsNullParameter(DesignParameter originalParameter, out int isNullParameterIndex)
		{
			if (originalParameter == null || originalParameter.SourceVersion != DataRowVersion.Original || originalParameter.SourceColumnNullMapping)
			{
				throw new InternalException("'originalParameter' is not valid.");
			}
			isNullParameterIndex = 0;
			for (int i = 0; i < this.activeCommand.Parameters.Count; i++)
			{
				DesignParameter designParameter = this.activeCommand.Parameters[i];
				if (designParameter == null)
				{
					throw new DataSourceGeneratorException("Parameter type is not DesignParameter.");
				}
				if (((designParameter.Direction != ParameterDirection.Input && designParameter.Direction != ParameterDirection.InputOutput) || (designParameter.SourceColumnNullMapping && designParameter.SourceVersion == DataRowVersion.Original)) && StringUtil.EqualValue(originalParameter.SourceColumn, designParameter.SourceColumn))
				{
					isNullParameterIndex = i;
					return designParameter;
				}
			}
			return null;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0008AF94 File Offset: 0x00089194
		private bool IsPrimaryColumn(string columnName)
		{
			DataColumn[] primaryKeyColumns = base.DesignTable.PrimaryKeyColumns;
			if (primaryKeyColumns == null || primaryKeyColumns.Length == 0)
			{
				return false;
			}
			foreach (DataColumn dataColumn in primaryKeyColumns)
			{
				if (StringUtil.EqualValue(dataColumn.ColumnName, columnName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000CA6 RID: 3238
		private bool generateOverloadWithoutCurrentPKParameters;
	}
}
