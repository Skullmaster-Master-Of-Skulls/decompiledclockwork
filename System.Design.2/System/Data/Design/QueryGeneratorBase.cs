using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Design;
using System.Reflection;
using Microsoft.Win32;

namespace System.Data.Design
{
	// Token: 0x0200025B RID: 603
	internal abstract class QueryGeneratorBase
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x0007F0E8 File Offset: 0x0007D2E8
		internal QueryGeneratorBase(TypedDataSourceCodeGenerator codeGenerator)
		{
			this.codeGenerator = codeGenerator;
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0007F142 File Offset: 0x0007D342
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x0007F14A File Offset: 0x0007D34A
		internal DbProviderFactory ProviderFactory
		{
			get
			{
				return this.providerFactory;
			}
			set
			{
				this.providerFactory = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0007F153 File Offset: 0x0007D353
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x0007F15B File Offset: 0x0007D35B
		internal DbSource MethodSource
		{
			get
			{
				return this.methodSource;
			}
			set
			{
				this.methodSource = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0007F164 File Offset: 0x0007D364
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x0007F16C File Offset: 0x0007D36C
		internal DbSourceCommand ActiveCommand
		{
			get
			{
				return this.activeCommand;
			}
			set
			{
				this.activeCommand = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0007F175 File Offset: 0x0007D375
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x0007F17D File Offset: 0x0007D37D
		internal string MethodName
		{
			get
			{
				return this.methodName;
			}
			set
			{
				this.methodName = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0007F186 File Offset: 0x0007D386
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x0007F18E File Offset: 0x0007D38E
		internal ParameterGenerationOption ParameterOption
		{
			get
			{
				return this.parameterOption;
			}
			set
			{
				this.parameterOption = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0007F197 File Offset: 0x0007D397
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x0007F19F File Offset: 0x0007D39F
		internal Type ContainerParameterType
		{
			get
			{
				return this.containerParamType;
			}
			set
			{
				this.containerParamType = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0007F1A8 File Offset: 0x0007D3A8
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x0007F1B0 File Offset: 0x0007D3B0
		internal string ContainerParameterTypeName
		{
			get
			{
				return this.containerParamTypeName;
			}
			set
			{
				this.containerParamTypeName = value;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0007F1B9 File Offset: 0x0007D3B9
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x0007F1C1 File Offset: 0x0007D3C1
		internal string ContainerParameterName
		{
			get
			{
				return this.containerParamName;
			}
			set
			{
				this.containerParamName = value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0007F1CA File Offset: 0x0007D3CA
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x0007F1D2 File Offset: 0x0007D3D2
		internal int CommandIndex
		{
			get
			{
				return this.commandIndex;
			}
			set
			{
				this.commandIndex = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0007F1DB File Offset: 0x0007D3DB
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x0007F1E3 File Offset: 0x0007D3E3
		internal DesignTable DesignTable
		{
			get
			{
				return this.designTable;
			}
			set
			{
				this.designTable = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0007F1EC File Offset: 0x0007D3EC
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x0007F1F4 File Offset: 0x0007D3F4
		internal bool GenerateGetMethod
		{
			get
			{
				return this.getMethod;
			}
			set
			{
				this.getMethod = value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0007F1FD File Offset: 0x0007D3FD
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x0007F205 File Offset: 0x0007D405
		internal bool GeneratePagingMethod
		{
			get
			{
				return this.pagingMethod;
			}
			set
			{
				this.pagingMethod = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0007F20E File Offset: 0x0007D40E
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x0007F216 File Offset: 0x0007D416
		internal bool DeclarationOnly
		{
			get
			{
				return this.declarationOnly;
			}
			set
			{
				this.declarationOnly = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0007F21F File Offset: 0x0007D41F
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x0007F227 File Offset: 0x0007D427
		internal MethodTypeEnum MethodType
		{
			get
			{
				return this.methodType;
			}
			set
			{
				this.methodType = value;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x0007F230 File Offset: 0x0007D430
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x0007F238 File Offset: 0x0007D438
		internal string UpdateParameterName
		{
			get
			{
				return this.updateParameterName;
			}
			set
			{
				this.updateParameterName = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0007F241 File Offset: 0x0007D441
		// (set) Token: 0x0600172E RID: 5934 RVA: 0x0007F249 File Offset: 0x0007D449
		internal string UpdateParameterTypeName
		{
			get
			{
				return this.updateParameterTypeName;
			}
			set
			{
				this.updateParameterTypeName = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0007F252 File Offset: 0x0007D452
		// (set) Token: 0x06001730 RID: 5936 RVA: 0x0007F25A File Offset: 0x0007D45A
		internal CodeTypeReference UpdateParameterTypeReference
		{
			get
			{
				return this.updateParameterTypeReference;
			}
			set
			{
				this.updateParameterTypeReference = value;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x0007F263 File Offset: 0x0007D463
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x0007F26B File Offset: 0x0007D46B
		internal CodeDomProvider CodeProvider
		{
			get
			{
				return this.codeProvider;
			}
			set
			{
				this.codeProvider = value;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0007F274 File Offset: 0x0007D474
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x0007F27C File Offset: 0x0007D47C
		internal string UpdateCommandName
		{
			get
			{
				return this.updateCommandName;
			}
			set
			{
				this.updateCommandName = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x0007F285 File Offset: 0x0007D485
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x0007F28D File Offset: 0x0007D48D
		internal bool IsFunctionsDataComponent
		{
			get
			{
				return this.isFunctionsDataComponent;
			}
			set
			{
				this.isFunctionsDataComponent = value;
			}
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0007F296 File Offset: 0x0007D496
		internal static bool IsSqlCeParameterType(Type type)
		{
			return type != null && "System.Data.SqlServerCe.SqlCeParameter".Equals(type.FullName, StringComparison.Ordinal);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0007F2B4 File Offset: 0x0007D4B4
		internal static CodeStatement SetCommandTextStatement(CodeExpression commandExpression, string commandText)
		{
			return CodeGenHelper.Assign(CodeGenHelper.Property(commandExpression, "CommandText"), CodeGenHelper.Str(commandText));
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0007F2DC File Offset: 0x0007D4DC
		internal static CodeStatement SetCommandTypeStatement(CodeExpression commandExpression, CommandType commandType)
		{
			CodeExpression left = CodeGenHelper.Property(commandExpression, "CommandType");
			CodeExpression exp = CodeGenHelper.GlobalTypeExpr(typeof(CommandType));
			CommandType commandType2 = commandType;
			return CodeGenHelper.Assign(left, CodeGenHelper.Field(exp, commandType2.ToString()));
		}

		// Token: 0x0600173A RID: 5946
		internal abstract CodeMemberMethod Generate();

		// Token: 0x0600173B RID: 5947 RVA: 0x0007F320 File Offset: 0x0007D520
		protected DesignParameter GetReturnParameter(DbSourceCommand command)
		{
			foreach (object obj in command.Parameters)
			{
				DesignParameter designParameter = (DesignParameter)obj;
				if (designParameter.Direction == ParameterDirection.ReturnValue)
				{
					return designParameter;
				}
			}
			return null;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0007F384 File Offset: 0x0007D584
		protected int GetReturnParameterPosition(DbSourceCommand command)
		{
			if (command == null || command.Parameters == null)
			{
				return -1;
			}
			for (int i = 0; i < command.Parameters.Count; i++)
			{
				if (command.Parameters[i].Direction == ParameterDirection.ReturnValue)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0007F3CC File Offset: 0x0007D5CC
		internal static CodeExpression AddNewParameterStatements(DesignParameter parameter, Type parameterType, DbProviderFactory factory, IList statements, CodeExpression parameterVariable)
		{
			if (parameterType == typeof(SqlParameter))
			{
				return QueryGeneratorBase.BuildNewSqlParameterStatement(parameter);
			}
			if (parameterType == typeof(OleDbParameter))
			{
				return QueryGeneratorBase.BuildNewOleDbParameterStatement(parameter);
			}
			if (parameterType == typeof(OdbcParameter))
			{
				return QueryGeneratorBase.BuildNewOdbcParameterStatement(parameter);
			}
			if (parameterType == typeof(OracleParameter))
			{
				return QueryGeneratorBase.BuildNewOracleParameterStatement(parameter);
			}
			if (QueryGeneratorBase.IsSqlCeParameterType(parameterType) && StringUtil.NotEmptyAfterTrim(parameter.ProviderType))
			{
				return QueryGeneratorBase.BuildNewSqlCeParameterStatement(parameter, factory);
			}
			return QueryGeneratorBase.BuildNewUnknownParameterStatements(parameter, parameterType, factory, statements, parameterVariable);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0007F468 File Offset: 0x0007D668
		private static CodeExpression BuildNewSqlParameterStatement(DesignParameter parameter)
		{
			SqlParameter sqlParameter = new SqlParameter();
			SqlDbType sqlDbType = SqlDbType.Char;
			bool flag = false;
			if (parameter.ProviderType != null && parameter.ProviderType.Length > 0)
			{
				try
				{
					sqlDbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), parameter.ProviderType);
					flag = true;
				}
				catch
				{
				}
			}
			if (!flag)
			{
				sqlParameter.DbType = parameter.DbType;
				sqlDbType = sqlParameter.SqlDbType;
			}
			return QueryGeneratorBase.NewParameter(parameter, typeof(SqlParameter), typeof(SqlDbType), sqlDbType.ToString());
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0007F504 File Offset: 0x0007D704
		private static CodeExpression BuildNewSqlCeParameterStatement(DesignParameter parameter, DbProviderFactory factory)
		{
			SqlDbType sqlDbType = SqlDbType.Char;
			bool flag = false;
			if (parameter.ProviderType != null && parameter.ProviderType.Length > 0)
			{
				try
				{
					sqlDbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), parameter.ProviderType);
					flag = true;
				}
				catch
				{
				}
			}
			Type type = null;
			object obj = factory.CreateParameter();
			if (obj != null)
			{
				type = obj.GetType();
			}
			if (!flag && obj != null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
				if (properties != null)
				{
					PropertyDescriptor propertyDescriptor = properties["DbType"];
					if (propertyDescriptor != null)
					{
						propertyDescriptor.SetValue(obj, parameter.DbType);
						sqlDbType = (SqlDbType)propertyDescriptor.GetValue(obj);
					}
				}
			}
			return QueryGeneratorBase.NewParameter(parameter, type, typeof(SqlDbType), sqlDbType.ToString());
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0007F5D4 File Offset: 0x0007D7D4
		private static CodeExpression BuildNewOleDbParameterStatement(DesignParameter parameter)
		{
			OleDbParameter oleDbParameter = new OleDbParameter();
			OleDbType oleDbType = OleDbType.Char;
			bool flag = false;
			if (parameter.ProviderType != null && parameter.ProviderType.Length > 0)
			{
				try
				{
					oleDbType = (OleDbType)Enum.Parse(typeof(OleDbType), parameter.ProviderType);
					flag = true;
				}
				catch
				{
				}
			}
			if (!flag)
			{
				oleDbParameter.DbType = parameter.DbType;
				oleDbType = oleDbParameter.OleDbType;
			}
			return QueryGeneratorBase.NewParameter(parameter, typeof(OleDbParameter), typeof(OleDbType), oleDbType.ToString());
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0007F674 File Offset: 0x0007D874
		private static CodeExpression BuildNewOdbcParameterStatement(DesignParameter parameter)
		{
			OdbcParameter odbcParameter = new OdbcParameter();
			OdbcType odbcType = OdbcType.Char;
			bool flag = false;
			if (parameter.ProviderType != null && parameter.ProviderType.Length > 0)
			{
				try
				{
					odbcType = (OdbcType)Enum.Parse(typeof(OdbcType), parameter.ProviderType);
					flag = true;
				}
				catch
				{
				}
			}
			if (!flag)
			{
				odbcParameter.DbType = parameter.DbType;
				odbcType = odbcParameter.OdbcType;
			}
			return QueryGeneratorBase.NewParameter(parameter, typeof(OdbcParameter), typeof(OdbcType), odbcType.ToString());
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0007F710 File Offset: 0x0007D910
		private static CodeExpression BuildNewOracleParameterStatement(DesignParameter parameter)
		{
			OracleParameter oracleParameter = new OracleParameter();
			OracleType oracleType = OracleType.Char;
			bool flag = false;
			if (parameter.ProviderType != null && parameter.ProviderType.Length > 0)
			{
				try
				{
					oracleType = (OracleType)Enum.Parse(typeof(OracleType), parameter.ProviderType);
					flag = true;
				}
				catch
				{
				}
			}
			if (!flag)
			{
				oracleParameter.DbType = parameter.DbType;
				oracleType = oracleParameter.OracleType;
			}
			return QueryGeneratorBase.NewParameter(parameter, typeof(OracleParameter), typeof(OracleType), oracleType.ToString());
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0007F7AC File Offset: 0x0007D9AC
		private static CodeExpression NewParameter(DesignParameter parameter, Type parameterType, Type typeEnumType, string typeEnumValue)
		{
			CodeExpression result;
			if (parameterType == typeof(SqlParameter))
			{
				result = CodeGenHelper.New(CodeGenHelper.GlobalType(parameterType), new CodeExpression[]
				{
					CodeGenHelper.Str(parameter.ParameterName),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeEnumType), typeEnumValue),
					CodeGenHelper.Primitive(parameter.Size),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ParameterDirection)), parameter.Direction.ToString()),
					CodeGenHelper.Primitive(parameter.Precision),
					CodeGenHelper.Primitive(parameter.Scale),
					CodeGenHelper.Str(parameter.SourceColumn),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataRowVersion)), parameter.SourceVersion.ToString()),
					CodeGenHelper.Primitive(parameter.SourceColumnNullMapping),
					CodeGenHelper.Primitive(null),
					CodeGenHelper.Str(string.Empty),
					CodeGenHelper.Str(string.Empty),
					CodeGenHelper.Str(string.Empty)
				});
			}
			else if (QueryGeneratorBase.IsSqlCeParameterType(parameterType))
			{
				result = CodeGenHelper.New(CodeGenHelper.GlobalType(parameterType), new CodeExpression[]
				{
					CodeGenHelper.Str(parameter.ParameterName),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeEnumType), typeEnumValue),
					CodeGenHelper.Primitive(parameter.Size),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ParameterDirection)), parameter.Direction.ToString()),
					CodeGenHelper.Primitive(parameter.IsNullable),
					CodeGenHelper.Primitive(parameter.Precision),
					CodeGenHelper.Primitive(parameter.Scale),
					CodeGenHelper.Str(parameter.SourceColumn),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataRowVersion)), parameter.SourceVersion.ToString()),
					CodeGenHelper.Primitive(null)
				});
			}
			else if (parameterType == typeof(OracleParameter))
			{
				result = CodeGenHelper.New(CodeGenHelper.GlobalType(parameterType), new CodeExpression[]
				{
					CodeGenHelper.Str(parameter.ParameterName),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeEnumType), typeEnumValue),
					CodeGenHelper.Primitive(parameter.Size),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ParameterDirection)), parameter.Direction.ToString()),
					CodeGenHelper.Str(parameter.SourceColumn),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataRowVersion)), parameter.SourceVersion.ToString()),
					CodeGenHelper.Primitive(parameter.SourceColumnNullMapping),
					CodeGenHelper.Primitive(null)
				});
			}
			else
			{
				result = CodeGenHelper.New(CodeGenHelper.GlobalType(parameterType), new CodeExpression[]
				{
					CodeGenHelper.Str(parameter.ParameterName),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeEnumType), typeEnumValue),
					CodeGenHelper.Primitive(parameter.Size),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ParameterDirection)), parameter.Direction.ToString()),
					CodeGenHelper.Cast(CodeGenHelper.GlobalType(typeof(byte)), CodeGenHelper.Primitive(parameter.Precision)),
					CodeGenHelper.Cast(CodeGenHelper.GlobalType(typeof(byte)), CodeGenHelper.Primitive(parameter.Scale)),
					CodeGenHelper.Str(parameter.SourceColumn),
					CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataRowVersion)), parameter.SourceVersion.ToString()),
					CodeGenHelper.Primitive(parameter.SourceColumnNullMapping),
					CodeGenHelper.Primitive(null)
				});
			}
			return result;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0007FBD4 File Offset: 0x0007DDD4
		private static bool ParamVariableDeclared(IList statements)
		{
			foreach (object obj in statements)
			{
				if (obj is CodeVariableDeclarationStatement)
				{
					CodeVariableDeclarationStatement codeVariableDeclarationStatement = obj as CodeVariableDeclarationStatement;
					if (codeVariableDeclarationStatement.Name == "param")
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0007FC48 File Offset: 0x0007DE48
		private static CodeExpression BuildNewUnknownParameterStatements(DesignParameter parameter, Type parameterType, DbProviderFactory factory, IList statements, CodeExpression parameterVariable)
		{
			if (!QueryGeneratorBase.ParamVariableDeclared(statements))
			{
				statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(parameterType), "param", CodeGenHelper.New(CodeGenHelper.GlobalType(parameterType), new CodeExpression[0])));
				parameterVariable = CodeGenHelper.Variable("param");
			}
			else
			{
				if (parameterVariable == null || !(parameterVariable is CodeVariableReferenceExpression))
				{
					parameterVariable = CodeGenHelper.Variable("param");
				}
				statements.Add(CodeGenHelper.Assign(parameterVariable, CodeGenHelper.New(CodeGenHelper.GlobalType(parameterType), new CodeExpression[0])));
			}
			IDbDataParameter dbDataParameter = (IDbDataParameter)Activator.CreateInstance(parameterType);
			statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, "ParameterName"), CodeGenHelper.Str(parameter.ParameterName)));
			if (parameter.DbType != dbDataParameter.DbType)
			{
				statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, "DbType"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DbType)), parameter.DbType.ToString())));
			}
			PropertyInfo providerTypeProperty = ProviderManager.GetProviderTypeProperty(factory);
			if (providerTypeProperty != null && parameter.ProviderType != null && parameter.ProviderType.Length > 0)
			{
				object obj = null;
				try
				{
					obj = Enum.Parse(providerTypeProperty.PropertyType, parameter.ProviderType);
				}
				catch
				{
				}
				if (obj != null)
				{
					statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, providerTypeProperty.Name), CodeGenHelper.Field(CodeGenHelper.TypeExpr(CodeGenHelper.GlobalType(providerTypeProperty.PropertyType)), obj.ToString())));
				}
			}
			if (parameter.Size != dbDataParameter.Size)
			{
				statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, "Size"), CodeGenHelper.Primitive(parameter.Size)));
			}
			if (parameter.Direction != dbDataParameter.Direction)
			{
				statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, "Direction"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ParameterDirection)), parameter.Direction.ToString())));
			}
			if (parameter.IsNullable != dbDataParameter.IsNullable)
			{
				statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, "IsNullable"), CodeGenHelper.Primitive(parameter.IsNullable)));
			}
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(QueryGeneratorBase.persistScaleAndPrecisionRegistryKey))
			{
				if (registryKey != null)
				{
					if (parameter.Precision != dbDataParameter.Precision)
					{
						statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, "Precision"), CodeGenHelper.Primitive(parameter.Precision)));
					}
					if (parameter.Scale != dbDataParameter.Scale)
					{
						statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, "Scale"), CodeGenHelper.Primitive(parameter.Scale)));
					}
				}
			}
			if (parameter.SourceColumn != dbDataParameter.SourceColumn)
			{
				statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, "SourceColumn"), CodeGenHelper.Str(parameter.SourceColumn)));
			}
			if (parameter.SourceVersion != dbDataParameter.SourceVersion)
			{
				statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, "SourceVersion"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataRowVersion)), parameter.SourceVersion.ToString())));
			}
			if (dbDataParameter is DbParameter && parameter.SourceColumnNullMapping != ((DbParameter)dbDataParameter).SourceColumnNullMapping)
			{
				statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(parameterVariable, "SourceColumnNullMapping"), CodeGenHelper.Primitive(parameter.SourceColumnNullMapping)));
			}
			return parameterVariable;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0007FFF0 File Offset: 0x0007E1F0
		protected Type GetParameterUrtType(DesignParameter parameter)
		{
			if (this.ParameterOption == ParameterGenerationOption.SqlTypes)
			{
				return this.GetParameterSqlType(parameter);
			}
			if (this.ParameterOption == ParameterGenerationOption.Objects)
			{
				return typeof(object);
			}
			if (this.ParameterOption == ParameterGenerationOption.ClrTypes)
			{
				Type type;
				if (parameter.DbType == DbType.Time && this.methodSource != null && this.methodSource.Connection != null && StringUtil.EqualValue(this.methodSource.Connection.Provider, ManagedProviderNames.SqlClient, true))
				{
					type = typeof(TimeSpan);
				}
				else
				{
					type = TypeConvertions.DbTypeToUrtType(parameter.DbType);
				}
				if (type == null)
				{
					if (this.codeGenerator != null)
					{
						this.codeGenerator.ProblemList.Add(new DSGeneratorProblem(SR.GetString("CG_UnableToConvertDbTypeToUrtType", new object[]
						{
							this.MethodName,
							parameter.Name
						}), ProblemSeverity.NonFatalError, this.methodSource));
					}
					type = typeof(object);
				}
				return type;
			}
			throw new InternalException("Unknown parameter generation option.");
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000800EC File Offset: 0x0007E2EC
		private Type GetParameterSqlType(DesignParameter parameter)
		{
			if (this.methodSource != null && this.methodSource.Connection != null && StringUtil.EqualValue(this.methodSource.Connection.Provider, ManagedProviderNames.SqlClient, true))
			{
				SqlDbType sqlDbType = SqlDbType.Char;
				bool flag = false;
				if (parameter.ProviderType != null && parameter.ProviderType.Length > 0)
				{
					try
					{
						sqlDbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), parameter.ProviderType);
						flag = true;
					}
					catch
					{
					}
				}
				if (!flag)
				{
					sqlDbType = new SqlParameter
					{
						DbType = parameter.DbType
					}.SqlDbType;
				}
				Type type = TypeConvertions.SqlDbTypeToSqlType(sqlDbType);
				if (type == null)
				{
					if (this.codeGenerator != null)
					{
						this.codeGenerator.ProblemList.Add(new DSGeneratorProblem(SR.GetString("CG_UnableToConvertSqlDbTypeToSqlType", new object[]
						{
							this.MethodName,
							parameter.Name
						}), ProblemSeverity.NonFatalError, this.methodSource));
					}
					type = typeof(object);
				}
				return type;
			}
			throw new InternalException("We should never attempt to generate SqlType-parameters for non-Sql providers.");
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0008020C File Offset: 0x0007E40C
		protected void AddThrowsClauseIfNeeded(CodeMemberMethod dbMethod)
		{
			CodeTypeReference[] array = new CodeTypeReference[1];
			int num = 0;
			bool flag = false;
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
				if (designParameter.Direction == ParameterDirection.Output || designParameter.Direction == ParameterDirection.InputOutput)
				{
					Type parameterUrtType = this.GetParameterUrtType(designParameter);
					if (CodeGenHelper.GenerateNullExpression(parameterUrtType) == null)
					{
						array[0] = CodeGenHelper.GlobalType(typeof(StrongTypingException));
						flag = true;
					}
				}
			}
			if (!flag)
			{
				int returnParameterPosition = this.GetReturnParameterPosition(this.activeCommand);
				if (returnParameterPosition >= 0 && !this.getMethod && this.methodSource.QueryType != QueryType.Scalar)
				{
					Type parameterUrtType2 = this.GetParameterUrtType(this.activeCommand.Parameters[returnParameterPosition]);
					if (CodeGenHelper.GenerateNullExpression(parameterUrtType2) == null)
					{
						array[0] = CodeGenHelper.GlobalType(typeof(StrongTypingException));
						flag = true;
					}
				}
			}
			if (flag)
			{
				dbMethod.UserData.Add("throwsCollection", new CodeTypeReferenceCollection(array));
			}
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0008032F File Offset: 0x0007E52F
		protected void AddSetParameterStatements(DesignParameter parameter, string parameterName, CodeExpression cmdExpression, int parameterIndex, IList statements)
		{
			this.AddSetParameterStatements(parameter, parameterName, null, cmdExpression, parameterIndex, 0, statements);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00080340 File Offset: 0x0007E540
		protected void AddSetParameterStatements(DesignParameter parameter, string parameterName, DesignParameter isNullParameter, CodeExpression cmdExpression, int parameterIndex, int isNullParameterIndex, IList statements)
		{
			Type parameterUrtType = this.GetParameterUrtType(parameter);
			CodeCastExpression codeCastExpression = new CodeCastExpression(parameterUrtType, CodeGenHelper.Argument(parameterName));
			codeCastExpression.UserData.Add("CastIsBoxing", true);
			CodeCastExpression codeCastExpression2;
			CodeCastExpression codeCastExpression3;
			if (this.codeGenerator != null && CodeGenHelper.IsGeneratingJSharpCode(this.codeGenerator.CodeProvider))
			{
				codeCastExpression2 = new CodeCastExpression(typeof(int), CodeGenHelper.Primitive(0));
				codeCastExpression2.UserData.Add("CastIsBoxing", true);
				codeCastExpression3 = new CodeCastExpression(typeof(int), CodeGenHelper.Primitive(1));
				codeCastExpression3.UserData.Add("CastIsBoxing", true);
			}
			else
			{
				codeCastExpression2 = new CodeCastExpression(typeof(object), CodeGenHelper.Primitive(0));
				codeCastExpression3 = new CodeCastExpression(typeof(object), CodeGenHelper.Primitive(1));
			}
			CodeExpression left = CodeGenHelper.Property(CodeGenHelper.Indexer(CodeGenHelper.Property(cmdExpression, "Parameters"), CodeGenHelper.Primitive(parameterIndex)), "Value");
			CodeExpression left2 = null;
			if (isNullParameter != null)
			{
				left2 = CodeGenHelper.Property(CodeGenHelper.Indexer(CodeGenHelper.Property(cmdExpression, "Parameters"), CodeGenHelper.Primitive(isNullParameterIndex)), "Value");
			}
			int num = (isNullParameter == null) ? 1 : 2;
			CodeStatement[] array = new CodeStatement[num];
			CodeStatement[] array2 = new CodeStatement[num];
			if (parameter.AllowDbNull && parameterUrtType.IsValueType)
			{
				array[0] = CodeGenHelper.Assign(left, new CodeCastExpression(parameterUrtType, CodeGenHelper.Property(CodeGenHelper.Argument(parameterName), "Value"))
				{
					UserData = 
					{
						{
							"CastIsBoxing",
							true
						}
					}
				});
				array2[0] = CodeGenHelper.Assign(left, CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DBNull)), "Value"));
				if (isNullParameter != null)
				{
					array[1] = array[0];
					array2[1] = array2[0];
					array[0] = CodeGenHelper.Assign(left2, codeCastExpression2);
					array2[0] = CodeGenHelper.Assign(left2, codeCastExpression3);
				}
				statements.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.Property(CodeGenHelper.Argument(parameterName), "HasValue"), CodeGenHelper.Primitive(true)), array, array2));
				return;
			}
			if (parameter.AllowDbNull && !parameterUrtType.IsValueType)
			{
				array[0] = CodeGenHelper.Assign(left, CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DBNull)), "Value"));
				array2[0] = CodeGenHelper.Assign(left, codeCastExpression);
				if (isNullParameter != null)
				{
					array[1] = array[0];
					array2[1] = array2[0];
					array[0] = CodeGenHelper.Assign(left2, codeCastExpression3);
					array2[0] = CodeGenHelper.Assign(left2, codeCastExpression2);
				}
				statements.Add(CodeGenHelper.If(CodeGenHelper.IdEQ(CodeGenHelper.Argument(parameterName), CodeGenHelper.Primitive(null)), array, array2));
				return;
			}
			if (!parameter.AllowDbNull && !parameterUrtType.IsValueType)
			{
				CodeStatement[] trueStms = new CodeStatement[]
				{
					CodeGenHelper.Throw(CodeGenHelper.GlobalType(typeof(ArgumentNullException)), parameterName)
				};
				array2[0] = CodeGenHelper.Assign(left, codeCastExpression);
				if (isNullParameter != null)
				{
					array2[1] = array2[0];
					array2[0] = CodeGenHelper.Assign(left2, codeCastExpression2);
				}
				statements.Add(CodeGenHelper.If(CodeGenHelper.IdEQ(CodeGenHelper.Argument(parameterName), CodeGenHelper.Primitive(null)), trueStms, array2));
				return;
			}
			if (!parameter.AllowDbNull && parameterUrtType.IsValueType)
			{
				if (isNullParameter != null)
				{
					statements.Add(CodeGenHelper.Assign(left2, codeCastExpression2));
				}
				statements.Add(CodeGenHelper.Assign(left, codeCastExpression));
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000806B0 File Offset: 0x0007E8B0
		protected bool AddSetReturnParamValuesStatements(IList statements, CodeExpression commandExpression)
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
				if (designParameter.Direction == ParameterDirection.Output || designParameter.Direction == ParameterDirection.InputOutput)
				{
					Type parameterUrtType = this.GetParameterUrtType(designParameter);
					string nameFromList = this.nameHandler.GetNameFromList(designParameter.ParameterName);
					CodeExpression codeExpression = CodeGenHelper.Property(CodeGenHelper.Indexer(CodeGenHelper.Property(commandExpression, "Parameters"), CodeGenHelper.Primitive(i)), "Value");
					CodeExpression cond = CodeGenHelper.GenerateDbNullCheck(codeExpression);
					CodeExpression codeExpression2 = CodeGenHelper.GenerateNullExpression(parameterUrtType);
					CodeStatement trueStm;
					if (codeExpression2 == null)
					{
						if (designParameter.AllowDbNull && parameterUrtType.IsValueType)
						{
							trueStm = CodeGenHelper.Assign(CodeGenHelper.Argument(nameFromList), CodeGenHelper.New(CodeGenHelper.NullableType(parameterUrtType), new CodeExpression[0]));
						}
						else if (designParameter.AllowDbNull && !parameterUrtType.IsValueType)
						{
							trueStm = CodeGenHelper.Assign(CodeGenHelper.Argument(nameFromList), CodeGenHelper.Primitive(null));
						}
						else
						{
							trueStm = CodeGenHelper.Throw(CodeGenHelper.GlobalType(typeof(StrongTypingException)), SR.GetString("CG_ParameterIsDBNull", new object[]
							{
								nameFromList
							}), CodeGenHelper.Primitive(null));
						}
					}
					else
					{
						trueStm = CodeGenHelper.Assign(CodeGenHelper.Argument(this.nameHandler.GetNameFromList(designParameter.ParameterName)), codeExpression2);
					}
					CodeStatement falseStm;
					if (designParameter.AllowDbNull && parameterUrtType.IsValueType)
					{
						falseStm = CodeGenHelper.Assign(CodeGenHelper.Argument(nameFromList), CodeGenHelper.New(CodeGenHelper.NullableType(parameterUrtType), new CodeExpression[]
						{
							CodeGenHelper.Cast(CodeGenHelper.GlobalType(parameterUrtType), codeExpression)
						}));
					}
					else
					{
						falseStm = CodeGenHelper.Assign(CodeGenHelper.Argument(nameFromList), CodeGenHelper.Cast(CodeGenHelper.GlobalType(parameterUrtType), codeExpression));
					}
					statements.Add(CodeGenHelper.If(cond, trueStm, falseStm));
				}
			}
			return true;
		}

		// Token: 0x04000BBD RID: 3005
		protected TypedDataSourceCodeGenerator codeGenerator;

		// Token: 0x04000BBE RID: 3006
		protected GenericNameHandler nameHandler;

		// Token: 0x04000BBF RID: 3007
		protected static string returnVariableName = "returnValue";

		// Token: 0x04000BC0 RID: 3008
		protected static string commandVariableName = "command";

		// Token: 0x04000BC1 RID: 3009
		protected static string startRecordParameterName = "startRecord";

		// Token: 0x04000BC2 RID: 3010
		protected static string maxRecordsParameterName = "maxRecords";

		// Token: 0x04000BC3 RID: 3011
		protected DbProviderFactory providerFactory;

		// Token: 0x04000BC4 RID: 3012
		protected DbSource methodSource;

		// Token: 0x04000BC5 RID: 3013
		protected DbSourceCommand activeCommand;

		// Token: 0x04000BC6 RID: 3014
		protected string methodName;

		// Token: 0x04000BC7 RID: 3015
		protected MemberAttributes methodAttributes;

		// Token: 0x04000BC8 RID: 3016
		protected Type containerParamType = typeof(DataSet);

		// Token: 0x04000BC9 RID: 3017
		protected string containerParamTypeName;

		// Token: 0x04000BCA RID: 3018
		protected string containerParamName = "dataSet";

		// Token: 0x04000BCB RID: 3019
		protected ParameterGenerationOption parameterOption;

		// Token: 0x04000BCC RID: 3020
		protected Type returnType = typeof(void);

		// Token: 0x04000BCD RID: 3021
		protected int commandIndex;

		// Token: 0x04000BCE RID: 3022
		protected DesignTable designTable;

		// Token: 0x04000BCF RID: 3023
		protected bool getMethod;

		// Token: 0x04000BD0 RID: 3024
		protected bool pagingMethod;

		// Token: 0x04000BD1 RID: 3025
		protected bool declarationOnly;

		// Token: 0x04000BD2 RID: 3026
		protected MethodTypeEnum methodType;

		// Token: 0x04000BD3 RID: 3027
		protected string updateParameterName;

		// Token: 0x04000BD4 RID: 3028
		protected CodeTypeReference updateParameterTypeReference = CodeGenHelper.GlobalType(typeof(DataSet));

		// Token: 0x04000BD5 RID: 3029
		protected string updateParameterTypeName;

		// Token: 0x04000BD6 RID: 3030
		protected CodeDomProvider codeProvider;

		// Token: 0x04000BD7 RID: 3031
		protected string updateCommandName;

		// Token: 0x04000BD8 RID: 3032
		protected bool isFunctionsDataComponent;

		// Token: 0x04000BD9 RID: 3033
		private const string SqlCeParameterTypeName = "System.Data.SqlServerCe.SqlCeParameter";

		// Token: 0x04000BDA RID: 3034
		private static string persistScaleAndPrecisionRegistryKey = "SOFTWARE\\Microsoft\\MSDataSetGenerator\\PersistScaleAndPrecision";
	}
}
