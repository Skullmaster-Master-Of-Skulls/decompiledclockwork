using System;
using System.CodeDom;
using System.Collections;
using System.Data.Common;

namespace System.Data.Design
{
	// Token: 0x0200021D RID: 541
	internal sealed class DataComponentMethodGenerator
	{
		// Token: 0x06001402 RID: 5122 RVA: 0x00070F26 File Offset: 0x0006F126
		internal DataComponentMethodGenerator(TypedDataSourceCodeGenerator codeGenerator, DesignTable designTable, bool generateHierarchicalUpdate)
		{
			this.generateHierarchicalUpdate = generateHierarchicalUpdate;
			this.codeGenerator = codeGenerator;
			this.designTable = designTable;
			if (designTable.Connection != null)
			{
				this.providerFactory = ProviderManager.GetFactory(designTable.Connection.Provider);
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00070F64 File Offset: 0x0006F164
		internal void AddMethods(CodeTypeDeclaration dataComponentClass, bool isFunctionsDataComponent)
		{
			if (dataComponentClass == null)
			{
				throw new InternalException("dataComponent CodeTypeDeclaration should not be null.");
			}
			if (isFunctionsDataComponent)
			{
				this.AddCommandCollectionMembers(dataComponentClass, true);
				this.AddInitCommandCollection(dataComponentClass, true);
				return;
			}
			if (this.designTable.Connection == null || this.providerFactory == null)
			{
				return;
			}
			this.AddConstructor(dataComponentClass);
			this.AddAdapterMembers(dataComponentClass);
			this.AddInitAdapter(dataComponentClass);
			this.AddConnectionMembers(dataComponentClass);
			this.AddInitConnection(dataComponentClass);
			if (this.generateHierarchicalUpdate)
			{
				this.AddTransactionMembers(dataComponentClass);
			}
			this.AddCommandCollectionMembers(dataComponentClass, false);
			this.AddInitCommandCollection(dataComponentClass, false);
			this.AddClearBeforeFillMembers(dataComponentClass);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00070FF4 File Offset: 0x0006F1F4
		private void AddConstructor(CodeTypeDeclaration dataComponentClass)
		{
			CodeConstructor codeConstructor = CodeGenHelper.Constructor(MemberAttributes.Public);
			codeConstructor.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.ClearBeforeFillPropertyName), CodeGenHelper.Primitive(true)));
			dataComponentClass.Members.Add(codeConstructor);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00071044 File Offset: 0x0006F244
		private void AddAdapterMembers(CodeTypeDeclaration dataComponentClass)
		{
			Type type = this.providerFactory.CreateDataAdapter().GetType();
			CodeMemberField codeMemberField = CodeGenHelper.FieldDecl(CodeGenHelper.GlobalType(type), DataComponentNameHandler.AdapterVariableName);
			codeMemberField.UserData.Add("WithEvents", true);
			dataComponentClass.Members.Add(codeMemberField);
			CodeMemberProperty codeMemberProperty;
			if (this.generateHierarchicalUpdate)
			{
				codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(type), DataComponentNameHandler.AdapterPropertyName, (MemberAttributes)16386);
			}
			else
			{
				codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(type), DataComponentNameHandler.AdapterPropertyName, (MemberAttributes)20482);
			}
			codeMemberProperty.GetStatements.Add(CodeGenHelper.If(CodeGenHelper.IdEQ(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.AdapterVariableName), CodeGenHelper.Primitive(null)), new CodeStatement[]
			{
				CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), DataComponentNameHandler.InitAdapter, new CodeExpression[0]))
			}));
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.AdapterVariableName)));
			dataComponentClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0007114C File Offset: 0x0006F34C
		private void AddInitAdapter(CodeTypeDeclaration dataComponentClass)
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), DataComponentNameHandler.InitAdapter, (MemberAttributes)20482);
			codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.AdapterVariableName), CodeGenHelper.New(CodeGenHelper.GlobalType(this.providerFactory.CreateDataAdapter().GetType()), new CodeExpression[0])));
			if (this.designTable.Mappings != null && this.designTable.Mappings.Count > 0)
			{
				codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataTableMapping)), "tableMapping", CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(DataTableMapping)), new CodeExpression[0])));
				codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Variable("tableMapping"), "SourceTable"), CodeGenHelper.Str("Table")));
				codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Variable("tableMapping"), "DataSetTable"), CodeGenHelper.Str(this.designTable.Name)));
				foreach (object obj in this.designTable.Mappings)
				{
					DataColumnMapping dataColumnMapping = (DataColumnMapping)obj;
					codeMemberMethod.Statements.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Variable("tableMapping"), "ColumnMappings"), "Add", new CodeExpression[]
					{
						CodeGenHelper.Str(dataColumnMapping.SourceColumn),
						CodeGenHelper.Str(dataColumnMapping.DataSetColumn)
					})));
				}
				codeMemberMethod.Statements.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.AdapterVariableName), "TableMappings"), "Add", new CodeExpression[]
				{
					CodeGenHelper.Variable("tableMapping")
				})));
			}
			this.AddInitAdapterCommands(codeMemberMethod);
			dataComponentClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00071378 File Offset: 0x0006F578
		private void AddCommandCollectionMembers(CodeTypeDeclaration dataComponentClass, bool isFunctionsDataComponent)
		{
			Type type;
			if (isFunctionsDataComponent)
			{
				type = typeof(IDbCommand);
			}
			else
			{
				type = this.providerFactory.CreateCommand().GetType();
			}
			dataComponentClass.Members.Add(CodeGenHelper.FieldDecl(CodeGenHelper.GlobalType(type, 1), DataComponentNameHandler.SelectCmdCollectionVariableName));
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(type, 1), DataComponentNameHandler.SelectCmdCollectionPropertyName, (MemberAttributes)12290);
			codeMemberProperty.GetStatements.Add(CodeGenHelper.If(CodeGenHelper.IdEQ(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionVariableName), CodeGenHelper.Primitive(null)), new CodeStatement[]
			{
				CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), DataComponentNameHandler.InitCmdCollection, new CodeExpression[0]))
			}));
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionVariableName)));
			dataComponentClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00071458 File Offset: 0x0006F658
		private void AddInitCommandCollection(CodeTypeDeclaration dataComponentClass, bool isFunctionsDataComponent)
		{
			int num = this.designTable.Sources.Count;
			if (!isFunctionsDataComponent)
			{
				num++;
			}
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), DataComponentNameHandler.InitCmdCollection, (MemberAttributes)20482);
			Type type;
			if (isFunctionsDataComponent)
			{
				type = typeof(IDbCommand);
			}
			else
			{
				type = this.providerFactory.CreateCommand().GetType();
			}
			codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionVariableName), CodeGenHelper.NewArray(CodeGenHelper.GlobalType(type), num)));
			if (!isFunctionsDataComponent && this.designTable.MainSource != null && this.designTable.MainSource is DbSource)
			{
				DbSource dbSource = (DbSource)this.designTable.MainSource;
				DbSourceCommand activeCommand = dbSource.GetActiveCommand();
				if (activeCommand != null)
				{
					CodeExpression commandExpression = CodeGenHelper.ArrayIndexer(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionVariableName), CodeGenHelper.Primitive(0));
					this.AddCommandInitStatements(codeMemberMethod.Statements, commandExpression, activeCommand, this.providerFactory, isFunctionsDataComponent);
				}
			}
			if (this.designTable.Sources != null)
			{
				int num2 = 0;
				if (isFunctionsDataComponent)
				{
					num2--;
				}
				foreach (object obj in this.designTable.Sources)
				{
					Source source = (Source)obj;
					DbSource dbSource2 = source as DbSource;
					num2++;
					if (dbSource2 != null)
					{
						DbProviderFactory factory = this.providerFactory;
						if (dbSource2.Connection != null)
						{
							factory = ProviderManager.GetFactory(dbSource2.Connection.Provider);
						}
						if (factory != null)
						{
							DbSourceCommand activeCommand2 = dbSource2.GetActiveCommand();
							if (activeCommand2 != null)
							{
								CodeExpression commandExpression2 = CodeGenHelper.ArrayIndexer(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionVariableName), CodeGenHelper.Primitive(num2));
								this.AddCommandInitStatements(codeMemberMethod.Statements, commandExpression2, activeCommand2, factory, isFunctionsDataComponent);
							}
						}
					}
				}
			}
			dataComponentClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00071660 File Offset: 0x0006F860
		private void AddConnectionMembers(CodeTypeDeclaration dataComponentClass)
		{
			Type type = this.providerFactory.CreateConnection().GetType();
			MemberAttributes modifier = ((DesignConnection)this.designTable.Connection).Modifier;
			dataComponentClass.Members.Add(CodeGenHelper.FieldDecl(CodeGenHelper.GlobalType(type), DataComponentNameHandler.DefaultConnectionVariableName));
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(type), DataComponentNameHandler.DefaultConnectionPropertyName, modifier | MemberAttributes.Final);
			codeMemberProperty.GetStatements.Add(CodeGenHelper.If(CodeGenHelper.IdEQ(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.DefaultConnectionVariableName), CodeGenHelper.Primitive(null)), new CodeStatement[]
			{
				CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), DataComponentNameHandler.InitConnection, new CodeExpression[0]))
			}));
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.DefaultConnectionVariableName)));
			codeMemberProperty.SetStatements.Add(CodeGenHelper.Assign(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.DefaultConnectionVariableName), CodeGenHelper.Argument("value")));
			codeMemberProperty.SetStatements.Add(CodeGenHelper.If(CodeGenHelper.IdNotEQ(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "InsertCommand"), CodeGenHelper.Primitive(null)), CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "InsertCommand"), "Connection"), CodeGenHelper.Argument("value"))));
			codeMemberProperty.SetStatements.Add(CodeGenHelper.If(CodeGenHelper.IdNotEQ(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "DeleteCommand"), CodeGenHelper.Primitive(null)), CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "DeleteCommand"), "Connection"), CodeGenHelper.Argument("value"))));
			codeMemberProperty.SetStatements.Add(CodeGenHelper.If(CodeGenHelper.IdNotEQ(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "UpdateCommand"), CodeGenHelper.Primitive(null)), CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName), "UpdateCommand"), "Connection"), CodeGenHelper.Argument("value"))));
			int num = this.designTable.Sources.Count + 1;
			CodeStatement initStmt = CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(int)), "i", CodeGenHelper.Primitive(0));
			CodeExpression testExpression = CodeGenHelper.Less(CodeGenHelper.Variable("i"), CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionPropertyName), "Length"));
			CodeStatement incrementStmt = CodeGenHelper.Assign(CodeGenHelper.Variable("i"), CodeGenHelper.BinOperator(CodeGenHelper.Variable("i"), CodeBinaryOperatorType.Add, CodeGenHelper.Primitive(1)));
			CodeExpression left = CodeGenHelper.Property(CodeGenHelper.Cast(CodeGenHelper.GlobalType(this.providerFactory.CreateCommand().GetType()), CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionPropertyName), CodeGenHelper.Variable("i"))), "Connection");
			CodeExpression left2 = CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionPropertyName), CodeGenHelper.Variable("i"));
			CodeStatement codeStatement = CodeGenHelper.If(CodeGenHelper.IdNotEQ(left2, CodeGenHelper.Primitive(null)), CodeGenHelper.Assign(left, CodeGenHelper.Argument("value")));
			codeMemberProperty.SetStatements.Add(CodeGenHelper.ForLoop(initStmt, testExpression, incrementStmt, new CodeStatement[]
			{
				codeStatement
			}));
			dataComponentClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x000719D8 File Offset: 0x0006FBD8
		private void AddInitConnection(CodeTypeDeclaration dataComponentClass)
		{
			IDesignConnection connection = this.designTable.Connection;
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), DataComponentNameHandler.InitConnection, (MemberAttributes)20482);
			codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.DefaultConnectionVariableName), CodeGenHelper.New(CodeGenHelper.GlobalType(this.providerFactory.CreateConnection().GetType()), new CodeExpression[0])));
			CodeExpression right;
			if (connection.PropertyReference == null)
			{
				right = CodeGenHelper.Str(connection.ConnectionStringObject.ToFullString());
			}
			else
			{
				right = connection.PropertyReference;
			}
			codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.DefaultConnectionVariableName), "ConnectionString"), right));
			dataComponentClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00071AAC File Offset: 0x0006FCAC
		private void AddTransactionMembers(CodeTypeDeclaration dataComponentClass)
		{
			Type transactionType = this.designTable.PropertyCache.TransactionType;
			if (transactionType == null)
			{
				return;
			}
			CodeTypeReference type = CodeGenHelper.GlobalType(transactionType);
			dataComponentClass.Members.Add(CodeGenHelper.FieldDecl(type, DataComponentNameHandler.TransactionVariableName));
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(type, DataComponentNameHandler.TransactionPropertyName, (MemberAttributes)4098);
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.TransactionVariableName)));
			codeMemberProperty.SetStatements.Add(CodeGenHelper.Assign(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.TransactionVariableName), CodeGenHelper.Argument("value")));
			CodeStatement initStmt = CodeGenHelper.VariableDecl(CodeGenHelper.Type(typeof(int)), "i", CodeGenHelper.Primitive(0));
			CodeExpression testExpression = CodeGenHelper.Less(CodeGenHelper.Variable("i"), CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionPropertyName), "Length"));
			CodeStatement incrementStmt = CodeGenHelper.Assign(CodeGenHelper.Variable("i"), CodeGenHelper.BinOperator(CodeGenHelper.Variable("i"), CodeBinaryOperatorType.Add, CodeGenHelper.Primitive(1)));
			CodeExpression transaction = CodeGenHelper.Property(CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.SelectCmdCollectionPropertyName), CodeGenHelper.Variable("i")), "Transaction");
			CodeExpression oldTransaction = CodeGenHelper.Variable("oldTransaction");
			CodeExpression newTransaction = CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.TransactionVariableName);
			CodeStatement codeStatement = this.GenerateSetTransactionStmt(transaction, oldTransaction, newTransaction);
			codeMemberProperty.SetStatements.Add(CodeGenHelper.ForLoop(initStmt, testExpression, incrementStmt, new CodeStatement[]
			{
				codeStatement
			}));
			CodeExpression codeExpression = CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.AdapterPropertyName);
			CodeExpression codeExpression2 = CodeGenHelper.Property(codeExpression, "DeleteCommand");
			transaction = CodeGenHelper.Property(codeExpression2, "Transaction");
			codeMemberProperty.SetStatements.Add(CodeGenHelper.If(CodeGenHelper.And(CodeGenHelper.IdNotEQ(codeExpression, CodeGenHelper.Primitive(null)), CodeGenHelper.IdNotEQ(codeExpression2, CodeGenHelper.Primitive(null))), this.GenerateSetTransactionStmt(transaction, oldTransaction, newTransaction)));
			codeExpression2 = CodeGenHelper.Property(codeExpression, "InsertCommand");
			transaction = CodeGenHelper.Property(codeExpression2, "Transaction");
			codeMemberProperty.SetStatements.Add(CodeGenHelper.If(CodeGenHelper.And(CodeGenHelper.IdNotEQ(codeExpression, CodeGenHelper.Primitive(null)), CodeGenHelper.IdNotEQ(codeExpression2, CodeGenHelper.Primitive(null))), this.GenerateSetTransactionStmt(transaction, oldTransaction, newTransaction)));
			codeExpression2 = CodeGenHelper.Property(codeExpression, "UpdateCommand");
			transaction = CodeGenHelper.Property(codeExpression2, "Transaction");
			codeMemberProperty.SetStatements.Add(CodeGenHelper.If(CodeGenHelper.And(CodeGenHelper.IdNotEQ(codeExpression, CodeGenHelper.Primitive(null)), CodeGenHelper.IdNotEQ(codeExpression2, CodeGenHelper.Primitive(null))), this.GenerateSetTransactionStmt(transaction, oldTransaction, newTransaction)));
			dataComponentClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00071D64 File Offset: 0x0006FF64
		private CodeStatement GenerateSetTransactionStmt(CodeExpression transaction, CodeExpression oldTransaction, CodeExpression newTransaction)
		{
			return CodeGenHelper.Assign(transaction, newTransaction);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00071D70 File Offset: 0x0006FF70
		private void AddClearBeforeFillMembers(CodeTypeDeclaration dataComponentClass)
		{
			dataComponentClass.Members.Add(CodeGenHelper.FieldDecl(CodeGenHelper.GlobalType(typeof(bool)), DataComponentNameHandler.ClearBeforeFillVariableName));
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(typeof(bool)), DataComponentNameHandler.ClearBeforeFillPropertyName, (MemberAttributes)24578);
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.ClearBeforeFillVariableName)));
			codeMemberProperty.SetStatements.Add(CodeGenHelper.Assign(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.ClearBeforeFillVariableName), CodeGenHelper.Argument("value")));
			dataComponentClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00071E18 File Offset: 0x00070018
		private void AddInitAdapterCommands(CodeMemberMethod method)
		{
			if (this.designTable.DeleteCommand != null)
			{
				CodeExpression commandExpression = CodeGenHelper.Property(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.AdapterVariableName), "DeleteCommand");
				this.AddCommandInitStatements(method.Statements, commandExpression, this.designTable.DeleteCommand, this.providerFactory, false);
			}
			if (this.designTable.InsertCommand != null)
			{
				CodeExpression commandExpression2 = CodeGenHelper.Property(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.AdapterVariableName), "InsertCommand");
				this.AddCommandInitStatements(method.Statements, commandExpression2, this.designTable.InsertCommand, this.providerFactory, false);
			}
			if (this.designTable.UpdateCommand != null)
			{
				CodeExpression commandExpression3 = CodeGenHelper.Property(CodeGenHelper.Field(CodeGenHelper.This(), DataComponentNameHandler.AdapterVariableName), "UpdateCommand");
				this.AddCommandInitStatements(method.Statements, commandExpression3, this.designTable.UpdateCommand, this.providerFactory, false);
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00071EF8 File Offset: 0x000700F8
		private void AddCommandInitStatements(IList statements, CodeExpression commandExpression, DbSourceCommand command, DbProviderFactory currentFactory, bool isFunctionsDataComponent)
		{
			if (statements == null || commandExpression == null || command == null)
			{
				throw new InternalException("Argument should not be null.");
			}
			Type type = currentFactory.CreateParameter().GetType();
			Type type2 = currentFactory.CreateCommand().GetType();
			CodeExpression codeExpression = null;
			statements.Add(CodeGenHelper.Assign(commandExpression, CodeGenHelper.New(CodeGenHelper.GlobalType(type2), new CodeExpression[0])));
			if (isFunctionsDataComponent)
			{
				commandExpression = CodeGenHelper.Cast(CodeGenHelper.GlobalType(type2), commandExpression);
			}
			if (((DbSource)command.Parent).Connection == null || (this.designTable.Connection != null && this.designTable.Connection == ((DbSource)command.Parent).Connection))
			{
				statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(commandExpression, "Connection"), CodeGenHelper.Property(CodeGenHelper.This(), DataComponentNameHandler.DefaultConnectionPropertyName)));
			}
			else
			{
				Type type3 = currentFactory.CreateConnection().GetType();
				IDesignConnection connection = ((DbSource)command.Parent).Connection;
				CodeExpression codeExpression2;
				if (connection.PropertyReference == null)
				{
					codeExpression2 = CodeGenHelper.Str(connection.ConnectionStringObject.ToFullString());
				}
				else
				{
					codeExpression2 = connection.PropertyReference;
				}
				statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(commandExpression, "Connection"), CodeGenHelper.New(CodeGenHelper.GlobalType(type3), new CodeExpression[]
				{
					codeExpression2
				})));
			}
			statements.Add(QueryGeneratorBase.SetCommandTextStatement(commandExpression, command.CommandText));
			statements.Add(QueryGeneratorBase.SetCommandTypeStatement(commandExpression, command.CommandType));
			if (command.Parameters != null)
			{
				foreach (object obj in command.Parameters)
				{
					DesignParameter parameter = (DesignParameter)obj;
					codeExpression = QueryGeneratorBase.AddNewParameterStatements(parameter, type, currentFactory, statements, codeExpression);
					statements.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(commandExpression, "Parameters"), "Add", new CodeExpression[]
					{
						codeExpression
					})));
				}
			}
		}

		// Token: 0x04000AB4 RID: 2740
		private TypedDataSourceCodeGenerator codeGenerator;

		// Token: 0x04000AB5 RID: 2741
		private DesignTable designTable;

		// Token: 0x04000AB6 RID: 2742
		private DbProviderFactory providerFactory;

		// Token: 0x04000AB7 RID: 2743
		private bool generateHierarchicalUpdate;
	}
}
