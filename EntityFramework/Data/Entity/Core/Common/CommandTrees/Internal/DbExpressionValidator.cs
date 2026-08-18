using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x02000131 RID: 305
	internal sealed class DbExpressionValidator : DbExpressionRebinder
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x00034610 File Offset: 0x00032810
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00034684 File Offset: 0x00032884
		internal Dictionary<string, DbParameterReferenceExpression> Parameters
		{
			get
			{
				return this.paramMappings;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0003468C File Offset: 0x0003288C
		internal void ValidateExpression(DbExpression expression, string argumentName)
		{
			this.expressionArgumentName = argumentName;
			this.VisitExpression(expression);
			this.expressionArgumentName = null;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000346B1 File Offset: 0x000328B1
		protected override EntitySetBase VisitEntitySet(EntitySetBase entitySet)
		{
			return this.ValidateMetadata<EntitySetBase>(entitySet, new Func<EntitySetBase, EntitySetBase>(base.VisitEntitySet), (EntitySetBase es) => es.EntityContainer.DataSpace, this.allowedMetadataSpaces);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000346F1 File Offset: 0x000328F1
		protected override EdmFunction VisitFunction(EdmFunction function)
		{
			return this.ValidateMetadata<EdmFunction>(function, new Func<EdmFunction, EdmFunction>(base.VisitFunction), (EdmFunction func) => func.DataSpace, this.allowedFunctionSpaces);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00034731 File Offset: 0x00032931
		protected override EdmType VisitType(EdmType type)
		{
			return this.ValidateMetadata<EdmType>(type, new Func<EdmType, EdmType>(base.VisitType), (EdmType et) => et.DataSpace, this.allowedMetadataSpaces);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00034776 File Offset: 0x00032976
		protected override TypeUsage VisitTypeUsage(TypeUsage type)
		{
			return this.ValidateMetadata<TypeUsage>(type, new Func<TypeUsage, TypeUsage>(base.VisitTypeUsage), (TypeUsage tu) => tu.EdmType.DataSpace, this.allowedMetadataSpaces);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000347C0 File Offset: 0x000329C0
		protected override void OnEnterScope(IEnumerable<DbVariableReferenceExpression> scopeVariables)
		{
			Dictionary<string, TypeUsage> item = scopeVariables.ToDictionary((DbVariableReferenceExpression var) => var.VariableName, (DbVariableReferenceExpression var) => var.ResultType, StringComparer.Ordinal);
			this.variableScopes.Push(item);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0003481F File Offset: 0x00032A1F
		protected override void OnExitScope()
		{
			this.variableScopes.Pop();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00034830 File Offset: 0x00032A30
		public override DbExpression Visit(DbVariableReferenceExpression expression)
		{
			Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
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

		// Token: 0x06000A4C RID: 2636 RVA: 0x000348E8 File Offset: 0x00032AE8
		public override DbExpression Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
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

		// Token: 0x06000A4D RID: 2637 RVA: 0x0003497C File Offset: 0x00032B7C
		private TMetadata ValidateMetadata<TMetadata>(TMetadata metadata, Func<TMetadata, TMetadata> map, Func<TMetadata, DataSpace> getDataSpace, DataSpace[] allowedSpaces)
		{
			TMetadata tmetadata = map(metadata);
			if (!object.ReferenceEquals(metadata, tmetadata))
			{
				this.ThrowInvalidMetadata<TMetadata>();
			}
			DataSpace resultSpace = getDataSpace(tmetadata);
			if (!allowedSpaces.Any((DataSpace ds) => ds == resultSpace))
			{
				this.ThrowInvalidSpace<TMetadata>();
			}
			return tmetadata;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000349D9 File Offset: 0x00032BD9
		private void ThrowInvalidMetadata<TMetadata>()
		{
			this.ThrowInvalid(Strings.Cqt_Validator_InvalidOtherWorkspaceMetadata(typeof(TMetadata).Name));
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000349F5 File Offset: 0x00032BF5
		private void ThrowInvalidSpace<TMetadata>()
		{
			this.ThrowInvalid(Strings.Cqt_Validator_InvalidIncorrectDataSpaceMetadata(typeof(TMetadata).Name, Enum.GetName(typeof(DataSpace), this.requiredSpace)));
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00034A2B File Offset: 0x00032C2B
		private void ThrowInvalid(string message)
		{
			throw new ArgumentException(message, this.expressionArgumentName);
		}

		// Token: 0x040002B0 RID: 688
		private readonly DataSpace requiredSpace;

		// Token: 0x040002B1 RID: 689
		private readonly DataSpace[] allowedMetadataSpaces;

		// Token: 0x040002B2 RID: 690
		private readonly DataSpace[] allowedFunctionSpaces;

		// Token: 0x040002B3 RID: 691
		private readonly Dictionary<string, DbParameterReferenceExpression> paramMappings = new Dictionary<string, DbParameterReferenceExpression>();

		// Token: 0x040002B4 RID: 692
		private readonly Stack<Dictionary<string, TypeUsage>> variableScopes = new Stack<Dictionary<string, TypeUsage>>();

		// Token: 0x040002B5 RID: 693
		private string expressionArgumentName;
	}
}
