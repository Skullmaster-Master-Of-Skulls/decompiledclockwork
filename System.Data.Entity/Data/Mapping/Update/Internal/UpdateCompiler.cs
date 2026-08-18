using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002D2 RID: 722
	internal sealed class UpdateCompiler
	{
		// Token: 0x06002A67 RID: 10855 RVA: 0x000A678D File Offset: 0x000A498D
		internal UpdateCompiler(UpdateTranslator translator)
		{
			this.m_translator = translator;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000A679C File Offset: 0x000A499C
		internal UpdateCommand BuildDeleteCommand(PropagatorResult oldRow, TableChangeProcessor processor)
		{
			bool flag = true;
			DbExpressionBinding target = UpdateCompiler.GetTarget(processor);
			DbExpression predicate = this.BuildPredicate(target, oldRow, null, processor, ref flag);
			DbDeleteCommandTree tree = new DbDeleteCommandTree(this.m_translator.MetadataWorkspace, DataSpace.SSpace, target, predicate);
			return new DynamicUpdateCommand(processor, this.m_translator, ModificationOperator.Delete, oldRow, null, tree, null);
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000A67E8 File Offset: 0x000A49E8
		internal UpdateCommand BuildUpdateCommand(PropagatorResult oldRow, PropagatorResult newRow, TableChangeProcessor processor)
		{
			bool flag = false;
			DbExpressionBinding target = UpdateCompiler.GetTarget(processor);
			List<DbModificationClause> list = new List<DbModificationClause>();
			Dictionary<int, string> outputIdentifiers;
			DbExpression returning;
			foreach (DbModificationClause item in this.BuildSetClauses(target, newRow, oldRow, processor, false, out outputIdentifiers, out returning, ref flag))
			{
				list.Add(item);
			}
			DbExpression predicate = this.BuildPredicate(target, oldRow, newRow, processor, ref flag);
			if (list.Count == 0)
			{
				if (flag)
				{
					List<IEntityStateEntry> list2 = new List<IEntityStateEntry>();
					list2.AddRange(SourceInterpreter.GetAllStateEntries(oldRow, this.m_translator, processor.Table));
					list2.AddRange(SourceInterpreter.GetAllStateEntries(newRow, this.m_translator, processor.Table));
					if (list2.All((IEntityStateEntry it) => it.State == EntityState.Unchanged))
					{
						flag = false;
					}
				}
				if (!flag)
				{
					return null;
				}
			}
			DbUpdateCommandTree tree = new DbUpdateCommandTree(this.m_translator.MetadataWorkspace, DataSpace.SSpace, target, predicate, list.AsReadOnly(), returning);
			return new DynamicUpdateCommand(processor, this.m_translator, ModificationOperator.Update, oldRow, newRow, tree, outputIdentifiers);
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000A6910 File Offset: 0x000A4B10
		internal UpdateCommand BuildInsertCommand(PropagatorResult newRow, TableChangeProcessor processor)
		{
			DbExpressionBinding target = UpdateCompiler.GetTarget(processor);
			bool flag = true;
			List<DbModificationClause> list = new List<DbModificationClause>();
			Dictionary<int, string> outputIdentifiers;
			DbExpression returning;
			foreach (DbModificationClause item in this.BuildSetClauses(target, newRow, null, processor, true, out outputIdentifiers, out returning, ref flag))
			{
				list.Add(item);
			}
			DbInsertCommandTree tree = new DbInsertCommandTree(this.m_translator.MetadataWorkspace, DataSpace.SSpace, target, list.AsReadOnly(), returning);
			return new DynamicUpdateCommand(processor, this.m_translator, ModificationOperator.Insert, null, newRow, tree, outputIdentifiers);
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000A69B4 File Offset: 0x000A4BB4
		private IEnumerable<DbModificationClause> BuildSetClauses(DbExpressionBinding target, PropagatorResult row, PropagatorResult originalRow, TableChangeProcessor processor, bool insertMode, out Dictionary<int, string> outputIdentifiers, out DbExpression returning, ref bool rowMustBeTouched)
		{
			Dictionary<EdmProperty, PropagatorResult> dictionary = new Dictionary<EdmProperty, PropagatorResult>();
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
			outputIdentifiers = new Dictionary<int, string>();
			PropagatorFlags propagatorFlags = insertMode ? PropagatorFlags.NoFlags : (PropagatorFlags.Preserve | PropagatorFlags.Unknown);
			for (int i = 0; i < processor.Table.ElementType.Properties.Count; i++)
			{
				EdmProperty edmProperty = processor.Table.ElementType.Properties[i];
				PropagatorResult propagatorResult = row.GetMemberValue(i);
				if (-1 != propagatorResult.Identifier)
				{
					propagatorResult = propagatorResult.ReplicateResultWithNewValue(this.m_translator.KeyManager.GetPrincipalValue(propagatorResult));
				}
				bool flag = false;
				bool flag2 = false;
				for (int j = 0; j < processor.KeyOrdinals.Length; j++)
				{
					if (processor.KeyOrdinals[j] == i)
					{
						flag2 = true;
						break;
					}
				}
				PropagatorFlags propagatorFlags2 = PropagatorFlags.NoFlags;
				if (!insertMode && flag2)
				{
					flag = true;
				}
				else
				{
					propagatorFlags2 |= propagatorResult.PropagatorFlags;
				}
				StoreGeneratedPattern storeGeneratedPattern = MetadataHelper.GetStoreGeneratedPattern(edmProperty);
				bool flag3 = storeGeneratedPattern == StoreGeneratedPattern.Computed || (insertMode && storeGeneratedPattern == StoreGeneratedPattern.Identity);
				if (flag3)
				{
					DbPropertyExpression value = target.Variable.Property(edmProperty);
					list.Add(new KeyValuePair<string, DbExpression>(edmProperty.Name, value));
					int identifier = propagatorResult.Identifier;
					if (-1 != identifier)
					{
						if (this.m_translator.KeyManager.HasPrincipals(identifier))
						{
							throw EntityUtil.InvalidOperation(Strings.Update_GeneratedDependent(edmProperty.Name));
						}
						outputIdentifiers.Add(identifier, edmProperty.Name);
						if (storeGeneratedPattern != StoreGeneratedPattern.Identity && processor.IsKeyProperty(i))
						{
							throw EntityUtil.NotSupported(Strings.Update_NotSupportedComputedKeyColumn("StoreGeneratedPattern", "Computed", "Identity", edmProperty.Name, edmProperty.DeclaringType.FullName));
						}
					}
				}
				if ((propagatorFlags2 & propagatorFlags) != PropagatorFlags.NoFlags)
				{
					flag = true;
				}
				else if (flag3)
				{
					flag = true;
					rowMustBeTouched = true;
				}
				if (!flag && !insertMode && storeGeneratedPattern == StoreGeneratedPattern.Identity)
				{
					PropagatorResult memberValue = originalRow.GetMemberValue(i);
					if (!ByValueEqualityComparer.Default.Equals(memberValue.GetSimpleValue(), propagatorResult.GetSimpleValue()))
					{
						throw EntityUtil.InvalidOperation(Strings.Update_ModifyingIdentityColumn("Identity", edmProperty.Name, edmProperty.DeclaringType.FullName));
					}
					flag = true;
				}
				if (!flag)
				{
					dictionary.Add(edmProperty, propagatorResult);
				}
			}
			if (0 < list.Count)
			{
				returning = DbExpressionBuilder.NewRow(list);
			}
			else
			{
				returning = null;
			}
			List<DbModificationClause> list2 = new List<DbModificationClause>(dictionary.Count);
			foreach (KeyValuePair<EdmProperty, PropagatorResult> keyValuePair in dictionary)
			{
				EdmProperty key = keyValuePair.Key;
				list2.Add(new DbSetClause(UpdateCompiler.GeneratePropertyExpression(target, keyValuePair.Key), this.GenerateValueExpression(keyValuePair.Key, keyValuePair.Value)));
			}
			return list2;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000A6C80 File Offset: 0x000A4E80
		private DbExpression BuildPredicate(DbExpressionBinding target, PropagatorResult referenceRow, PropagatorResult current, TableChangeProcessor processor, ref bool rowMustBeTouched)
		{
			Dictionary<EdmProperty, PropagatorResult> dictionary = new Dictionary<EdmProperty, PropagatorResult>();
			int num = 0;
			foreach (EdmProperty key in processor.Table.ElementType.Properties)
			{
				PropagatorResult memberValue = referenceRow.GetMemberValue(num);
				PropagatorResult input = (current == null) ? null : current.GetMemberValue(num);
				if (!rowMustBeTouched && (UpdateCompiler.HasFlag(memberValue, PropagatorFlags.ConcurrencyValue) || UpdateCompiler.HasFlag(input, PropagatorFlags.ConcurrencyValue)))
				{
					rowMustBeTouched = true;
				}
				if (!dictionary.ContainsKey(key) && (UpdateCompiler.HasFlag(memberValue, PropagatorFlags.ConcurrencyValue | PropagatorFlags.Key) || UpdateCompiler.HasFlag(input, PropagatorFlags.ConcurrencyValue | PropagatorFlags.Key)))
				{
					dictionary.Add(key, memberValue);
				}
				num++;
			}
			DbExpression dbExpression = null;
			foreach (KeyValuePair<EdmProperty, PropagatorResult> keyValuePair in dictionary)
			{
				DbExpression dbExpression2 = this.GenerateEqualityExpression(target, keyValuePair.Key, keyValuePair.Value);
				if (dbExpression == null)
				{
					dbExpression = dbExpression2;
				}
				else
				{
					dbExpression = dbExpression.And(dbExpression2);
				}
			}
			return dbExpression;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000A6DA8 File Offset: 0x000A4FA8
		private DbExpression GenerateEqualityExpression(DbExpressionBinding target, EdmProperty property, PropagatorResult value)
		{
			DbExpression dbExpression = UpdateCompiler.GeneratePropertyExpression(target, property);
			DbExpression dbExpression2 = this.GenerateValueExpression(property, value);
			if (dbExpression2.ExpressionKind == DbExpressionKind.Null)
			{
				return dbExpression.IsNull();
			}
			return dbExpression.Equal(dbExpression2);
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000A6DDE File Offset: 0x000A4FDE
		private static DbExpression GeneratePropertyExpression(DbExpressionBinding target, EdmProperty property)
		{
			return target.Variable.Property(property);
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000A6DEC File Offset: 0x000A4FEC
		private DbExpression GenerateValueExpression(EdmProperty property, PropagatorResult value)
		{
			if (value.IsNull)
			{
				return Helper.GetModelTypeUsage(property).Null();
			}
			object obj = this.m_translator.KeyManager.GetPrincipalValue(value);
			if (Convert.IsDBNull(obj))
			{
				return Helper.GetModelTypeUsage(property).Null();
			}
			TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(property);
			Type type = obj.GetType();
			if (type.IsEnum)
			{
				obj = Convert.ChangeType(obj, type.GetEnumUnderlyingType(), CultureInfo.InvariantCulture);
			}
			Type clrEquivalentType = ((PrimitiveType)modelTypeUsage.EdmType).ClrEquivalentType;
			if (type != clrEquivalentType)
			{
				obj = Convert.ChangeType(obj, clrEquivalentType, CultureInfo.InvariantCulture);
			}
			return modelTypeUsage.Constant(obj);
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000A6E89 File Offset: 0x000A5089
		private static bool HasFlag(PropagatorResult input, PropagatorFlags flags)
		{
			return input != null && (flags & input.PropagatorFlags) > PropagatorFlags.NoFlags;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000A6E9B File Offset: 0x000A509B
		private static DbExpressionBinding GetTarget(TableChangeProcessor processor)
		{
			return processor.Table.Scan().BindAs("target");
		}

		// Token: 0x040012E5 RID: 4837
		internal readonly UpdateTranslator m_translator;

		// Token: 0x040012E6 RID: 4838
		private const string s_targetVarName = "target";
	}
}
