using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x02000438 RID: 1080
	internal sealed class DbExpressionValidator : DbExpressionRebinder
	{
		// Token: 0x06003A0A RID: 14858 RVA: 0x000DD834 File Offset: 0x000DBA34
		internal DbExpressionValidator(MetadataWorkspace metadata, DataSpace expectedDataSpace) : base(metadata)
		{
			this.requiredSpace = expectedDataSpace;
			this.allowedFunctionSpaces = new DataSpace[]
			{
				DataSpace.CSpace,
				DataSpace.SSpace
			};
			if (expectedDataSpace == DataSpace.SSpace)
			{
				this.allowedMetadataSpaces = new DataSpace[]
				{
					DataSpace.SSpace,
					DataSpace.CSpace
				};
				return;
			}
			this.allowedMetadataSpaces = new DataSpace[]
			{
				DataSpace.CSpace
			};
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000DD8A2 File Offset: 0x000DBAA2
		internal Dictionary<string, DbParameterReferenceExpression> Parameters
		{
			get
			{
				return this.paramMappings;
			}
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000DD8AA File Offset: 0x000DBAAA
		internal void ValidateExpression(DbExpression expression, string argumentName)
		{
			this.expressionArgumentName = argumentName;
			this.VisitExpression(expression);
			this.expressionArgumentName = null;
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x000DD8C2 File Offset: 0x000DBAC2
		protected override EntitySetBase VisitEntitySet(EntitySetBase entitySet)
		{
			return this.ValidateMetadata<EntitySetBase>(entitySet, new Func<EntitySetBase, EntitySetBase>(base.VisitEntitySet), (EntitySetBase es) => es.EntityContainer.DataSpace, this.allowedMetadataSpaces);
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000DD8FC File Offset: 0x000DBAFC
		protected override EdmFunction VisitFunction(EdmFunction function)
		{
			return this.ValidateMetadata<EdmFunction>(function, new Func<EdmFunction, EdmFunction>(base.VisitFunction), (EdmFunction func) => func.DataSpace, this.allowedFunctionSpaces);
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000DD936 File Offset: 0x000DBB36
		protected override EdmType VisitType(EdmType type)
		{
			return this.ValidateMetadata<EdmType>(type, new Func<EdmType, EdmType>(base.VisitType), (EdmType et) => et.DataSpace, this.allowedMetadataSpaces);
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000DD970 File Offset: 0x000DBB70
		protected override TypeUsage VisitTypeUsage(TypeUsage type)
		{
			return this.ValidateMetadata<TypeUsage>(type, new Func<TypeUsage, TypeUsage>(base.VisitTypeUsage), (TypeUsage tu) => tu.EdmType.DataSpace, this.allowedMetadataSpaces);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000DD9AC File Offset: 0x000DBBAC
		protected override void OnEnterScope(IEnumerable<DbVariableReferenceExpression> scopeVariables)
		{
			Dictionary<string, TypeUsage> item = scopeVariables.ToDictionary((DbVariableReferenceExpression var) => var.VariableName, (DbVariableReferenceExpression var) => var.ResultType, StringComparer.Ordinal);
			this.variableScopes.Push(item);
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000DDA0F File Offset: 0x000DBC0F
		protected override void OnExitScope()
		{
			this.variableScopes.Pop();
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000DDA20 File Offset: 0x000DBC20
		public override DbExpression Visit(DbVariableReferenceExpression expression)
		{
			DbExpression dbExpression = base.Visit(expression);
			if (dbExpression.ExpressionKind == DbExpressionKind.VariableReference)
			{
				DbVariableReferenceExpression dbVariableReferenceExpression = (DbVariableReferenceExpression)dbExpression;
				TypeUsage typeUsage = null;
				foreach (Dictionary<string, TypeUsage> dictionary in this.variableScopes)
				{
					if (dictionary.TryGetValue(dbVariableReferenceExpression.VariableName, out typeUsage))
					{
						break;
					}
				}
				if (typeUsage == null)
				{
					this.ThrowInvalid(Strings.Cqt_Validator_VarRefInvalid(dbVariableReferenceExpression.VariableName));
				}
				if (!TypeSemantics.IsEqual(dbVariableReferenceExpression.ResultType, typeUsage))
				{
					this.ThrowInvalid(Strings.Cqt_Validator_VarRefTypeMismatch(dbVariableReferenceExpression.VariableName));
				}
			}
			return dbExpression;
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000DDAD0 File Offset: 0x000DBCD0
		public override DbExpression Visit(DbParameterReferenceExpression expression)
		{
			DbExpression dbExpression = base.Visit(expression);
			if (dbExpression.ExpressionKind == DbExpressionKind.ParameterReference)
			{
				DbParameterReferenceExpression dbParameterReferenceExpression = dbExpression as DbParameterReferenceExpression;
				DbParameterReferenceExpression dbParameterReferenceExpression2;
				if (this.paramMappings.TryGetValue(dbParameterReferenceExpression.ParameterName, out dbParameterReferenceExpression2))
				{
					if (!TypeSemantics.IsEqual(dbParameterReferenceExpression.ResultType, dbParameterReferenceExpression2.ResultType))
					{
						this.ThrowInvalid(Strings.Cqt_Validator_InvalidIncompatibleParameterReferences(dbParameterReferenceExpression.ParameterName));
					}
				}
				else
				{
					this.paramMappings.Add(dbParameterReferenceExpression.ParameterName, dbParameterReferenceExpression);
				}
			}
			return dbExpression;
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000DDB44 File Offset: 0x000DBD44
		private TMetadata ValidateMetadata<TMetadata>(TMetadata metadata, Func<TMetadata, TMetadata> map, Func<TMetadata, DataSpace> getDataSpace, DataSpace[] allowedSpaces)
		{
			TMetadata tmetadata = map(metadata);
			if (metadata != tmetadata)
			{
				this.ThrowInvalidMetadata<TMetadata>(metadata);
			}
			DataSpace resultSpace = getDataSpace(tmetadata);
			if (!allowedSpaces.Any((DataSpace ds) => ds == resultSpace))
			{
				this.ThrowInvalidSpace<TMetadata>(metadata);
			}
			return tmetadata;
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000DDB9E File Offset: 0x000DBD9E
		private void ThrowInvalidMetadata<TMetadata>(TMetadata invalid)
		{
			this.ThrowInvalid(Strings.Cqt_Validator_InvalidOtherWorkspaceMetadata(typeof(TMetadata).Name));
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000DDBBA File Offset: 0x000DBDBA
		private void ThrowInvalidSpace<TMetadata>(TMetadata invalid)
		{
			this.ThrowInvalid(Strings.Cqt_Validator_InvalidIncorrectDataSpaceMetadata(typeof(TMetadata).Name, Enum.GetName(typeof(DataSpace), this.requiredSpace)));
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000DDBF0 File Offset: 0x000DBDF0
		private void ThrowInvalid(string message)
		{
			throw EntityUtil.Argument(message, this.expressionArgumentName);
		}

		// Token: 0x0400186A RID: 6250
		private readonly DataSpace requiredSpace;

		// Token: 0x0400186B RID: 6251
		private readonly DataSpace[] allowedMetadataSpaces;

		// Token: 0x0400186C RID: 6252
		private readonly DataSpace[] allowedFunctionSpaces;

		// Token: 0x0400186D RID: 6253
		private readonly Dictionary<string, DbParameterReferenceExpression> paramMappings = new Dictionary<string, DbParameterReferenceExpression>();

		// Token: 0x0400186E RID: 6254
		private readonly Stack<Dictionary<string, TypeUsage>> variableScopes = new Stack<Dictionary<string, TypeUsage>>();

		// Token: 0x0400186F RID: 6255
		private string expressionArgumentName;
	}
}
