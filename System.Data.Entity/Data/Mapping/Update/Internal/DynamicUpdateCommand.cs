using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Spatial;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002BD RID: 701
	internal sealed class DynamicUpdateCommand : UpdateCommand
	{
		// Token: 0x060029A7 RID: 10663 RVA: 0x000A1AC8 File Offset: 0x0009FCC8
		internal DynamicUpdateCommand(TableChangeProcessor processor, UpdateTranslator translator, ModificationOperator op, PropagatorResult originalValues, PropagatorResult currentValues, DbModificationCommandTree tree, Dictionary<int, string> outputIdentifiers) : base(originalValues, currentValues)
		{
			this.m_processor = EntityUtil.CheckArgumentNull<TableChangeProcessor>(processor, "processor");
			this.m_operator = op;
			this.m_modificationCommandTree = EntityUtil.CheckArgumentNull<DbModificationCommandTree>(tree, "commandTree");
			this.m_outputIdentifiers = outputIdentifiers;
			if (ModificationOperator.Insert == op || op == ModificationOperator.Update)
			{
				this.m_inputIdentifiers = new List<KeyValuePair<int, DbSetClause>>(2);
				foreach (KeyValuePair<EdmMember, PropagatorResult> keyValuePair in Helper.PairEnumerations<EdmMember, PropagatorResult>(TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType), base.CurrentValues.GetMemberValues()))
				{
					int identifier = keyValuePair.Value.Identifier;
					DbSetClause value;
					if (-1 != identifier && DynamicUpdateCommand.TryGetSetterExpression(tree, keyValuePair.Key, op, out value))
					{
						foreach (int key in translator.KeyManager.GetPrincipals(identifier))
						{
							this.m_inputIdentifiers.Add(new KeyValuePair<int, DbSetClause>(key, value));
						}
					}
				}
			}
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000A1BF4 File Offset: 0x0009FDF4
		private static bool TryGetSetterExpression(DbModificationCommandTree tree, EdmMember member, ModificationOperator op, out DbSetClause setter)
		{
			IEnumerable<DbModificationClause> setClauses;
			if (ModificationOperator.Insert == op)
			{
				setClauses = ((DbInsertCommandTree)tree).SetClauses;
			}
			else
			{
				setClauses = ((DbUpdateCommandTree)tree).SetClauses;
			}
			foreach (DbModificationClause dbModificationClause in setClauses)
			{
				DbSetClause dbSetClause = (DbSetClause)dbModificationClause;
				if (((DbPropertyExpression)dbSetClause.Property).Property.EdmEquals(member))
				{
					setter = dbSetClause;
					return true;
				}
			}
			setter = null;
			return false;
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000A1C80 File Offset: 0x0009FE80
		internal override long Execute(UpdateTranslator translator, EntityConnection connection, Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues)
		{
			long result;
			using (DbCommand dbCommand = this.CreateCommand(translator, identifierValues))
			{
				dbCommand.Transaction = ((connection.CurrentTransaction != null) ? connection.CurrentTransaction.StoreTransaction : null);
				dbCommand.Connection = connection.StoreConnection;
				if (translator.CommandTimeout != null)
				{
					dbCommand.CommandTimeout = translator.CommandTimeout.Value;
				}
				int num;
				if (this.m_modificationCommandTree.HasReader)
				{
					num = 0;
					using (DbDataReader dbDataReader = dbCommand.ExecuteReader(CommandBehavior.SequentialAccess))
					{
						if (dbDataReader.Read())
						{
							num++;
							IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType);
							for (int i = 0; i < dbDataReader.FieldCount; i++)
							{
								string name = dbDataReader.GetName(i);
								EdmMember edmMember = allStructuralMembers[name];
								object value;
								if (Helper.IsSpatialType(edmMember.TypeUsage) && !dbDataReader.IsDBNull(i))
								{
									value = SpatialHelpers.GetSpatialValue(translator.MetadataWorkspace, dbDataReader, edmMember.TypeUsage, i);
								}
								else
								{
									value = dbDataReader.GetValue(i);
								}
								int ordinal = allStructuralMembers.IndexOf(edmMember);
								PropagatorResult memberValue = base.CurrentValues.GetMemberValue(ordinal);
								generatedValues.Add(new KeyValuePair<PropagatorResult, object>(memberValue, value));
								int identifier = memberValue.Identifier;
								if (-1 != identifier)
								{
									identifierValues.Add(identifier, value);
								}
							}
						}
						CommandHelper.ConsumeReader(dbDataReader);
						goto IL_157;
					}
				}
				num = dbCommand.ExecuteNonQuery();
				IL_157:
				result = (long)num;
			}
			return result;
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000A1E2C File Offset: 0x000A002C
		private DbCommand CreateCommand(UpdateTranslator translator, Dictionary<int, object> identifierValues)
		{
			DbModificationCommandTree dbModificationCommandTree = this.m_modificationCommandTree;
			if (this.m_inputIdentifiers != null)
			{
				Dictionary<DbSetClause, DbSetClause> dictionary = new Dictionary<DbSetClause, DbSetClause>();
				for (int i = 0; i < this.m_inputIdentifiers.Count; i++)
				{
					KeyValuePair<int, DbSetClause> keyValuePair = this.m_inputIdentifiers[i];
					object value;
					if (identifierValues.TryGetValue(keyValuePair.Key, out value))
					{
						DbSetClause value2 = new DbSetClause(keyValuePair.Value.Property, DbExpressionBuilder.Constant(value));
						dictionary[keyValuePair.Value] = value2;
						this.m_inputIdentifiers[i] = new KeyValuePair<int, DbSetClause>(keyValuePair.Key, value2);
					}
				}
				dbModificationCommandTree = this.RebuildCommandTree(dbModificationCommandTree, dictionary);
			}
			return translator.CreateCommand(dbModificationCommandTree);
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000A1ED8 File Offset: 0x000A00D8
		private DbModificationCommandTree RebuildCommandTree(DbModificationCommandTree originalTree, Dictionary<DbSetClause, DbSetClause> clauseMappings)
		{
			if (clauseMappings.Count == 0)
			{
				return originalTree;
			}
			DbModificationCommandTree result;
			if (originalTree.CommandTreeKind == DbCommandTreeKind.Insert)
			{
				DbInsertCommandTree dbInsertCommandTree = (DbInsertCommandTree)originalTree;
				result = new DbInsertCommandTree(dbInsertCommandTree.MetadataWorkspace, dbInsertCommandTree.DataSpace, dbInsertCommandTree.Target, this.ReplaceClauses(dbInsertCommandTree.SetClauses, clauseMappings).AsReadOnly(), dbInsertCommandTree.Returning);
			}
			else
			{
				DbUpdateCommandTree dbUpdateCommandTree = (DbUpdateCommandTree)originalTree;
				result = new DbUpdateCommandTree(dbUpdateCommandTree.MetadataWorkspace, dbUpdateCommandTree.DataSpace, dbUpdateCommandTree.Target, dbUpdateCommandTree.Predicate, this.ReplaceClauses(dbUpdateCommandTree.SetClauses, clauseMappings).AsReadOnly(), dbUpdateCommandTree.Returning);
			}
			return result;
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000A1F70 File Offset: 0x000A0170
		private List<DbModificationClause> ReplaceClauses(IList<DbModificationClause> originalClauses, Dictionary<DbSetClause, DbSetClause> mappings)
		{
			List<DbModificationClause> list = new List<DbModificationClause>(originalClauses.Count);
			for (int i = 0; i < originalClauses.Count; i++)
			{
				DbSetClause item;
				if (mappings.TryGetValue((DbSetClause)originalClauses[i], out item))
				{
					list.Add(item);
				}
				else
				{
					list.Add(originalClauses[i]);
				}
			}
			return list;
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x000A1FC7 File Offset: 0x000A01C7
		internal ModificationOperator Operator
		{
			get
			{
				return this.m_operator;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060029AE RID: 10670 RVA: 0x000A1FCF File Offset: 0x000A01CF
		internal override EntitySet Table
		{
			get
			{
				return this.m_processor.Table;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x000A1FDC File Offset: 0x000A01DC
		internal override IEnumerable<int> InputIdentifiers
		{
			get
			{
				if (this.m_inputIdentifiers == null)
				{
					yield break;
				}
				foreach (KeyValuePair<int, DbSetClause> keyValuePair in this.m_inputIdentifiers)
				{
					yield return keyValuePair.Key;
				}
				List<KeyValuePair<int, DbSetClause>>.Enumerator enumerator = default(List<KeyValuePair<int, DbSetClause>>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x000A1FF9 File Offset: 0x000A01F9
		internal override IEnumerable<int> OutputIdentifiers
		{
			get
			{
				if (this.m_outputIdentifiers == null)
				{
					return Enumerable.Empty<int>();
				}
				return this.m_outputIdentifiers.Keys;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override UpdateCommandKind Kind
		{
			get
			{
				return UpdateCommandKind.Dynamic;
			}
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000A2014 File Offset: 0x000A0214
		internal override IList<IEntityStateEntry> GetStateEntries(UpdateTranslator translator)
		{
			List<IEntityStateEntry> list = new List<IEntityStateEntry>(2);
			if (base.OriginalValues != null)
			{
				foreach (IEntityStateEntry item in SourceInterpreter.GetAllStateEntries(base.OriginalValues, translator, this.Table))
				{
					list.Add(item);
				}
			}
			if (base.CurrentValues != null)
			{
				foreach (IEntityStateEntry item2 in SourceInterpreter.GetAllStateEntries(base.CurrentValues, translator, this.Table))
				{
					list.Add(item2);
				}
			}
			return list;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000A20D0 File Offset: 0x000A02D0
		internal override int CompareToType(UpdateCommand otherCommand)
		{
			DynamicUpdateCommand dynamicUpdateCommand = (DynamicUpdateCommand)otherCommand;
			int num = (int)(this.Operator - dynamicUpdateCommand.Operator);
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.Ordinal.Compare(this.m_processor.Table.Name, dynamicUpdateCommand.m_processor.Table.Name);
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.Ordinal.Compare(this.m_processor.Table.EntityContainer.Name, dynamicUpdateCommand.m_processor.Table.EntityContainer.Name);
			if (num != 0)
			{
				return num;
			}
			PropagatorResult propagatorResult = (this.Operator == ModificationOperator.Delete) ? base.OriginalValues : base.CurrentValues;
			PropagatorResult propagatorResult2 = (dynamicUpdateCommand.Operator == ModificationOperator.Delete) ? dynamicUpdateCommand.OriginalValues : dynamicUpdateCommand.CurrentValues;
			for (int i = 0; i < this.m_processor.KeyOrdinals.Length; i++)
			{
				int ordinal = this.m_processor.KeyOrdinals[i];
				object simpleValue = propagatorResult.GetMemberValue(ordinal).GetSimpleValue();
				object simpleValue2 = propagatorResult2.GetMemberValue(ordinal).GetSimpleValue();
				num = ByValueComparer.Default.Compare(simpleValue, simpleValue2);
				if (num != 0)
				{
					return num;
				}
			}
			for (int j = 0; j < this.m_processor.KeyOrdinals.Length; j++)
			{
				int ordinal2 = this.m_processor.KeyOrdinals[j];
				int identifier = propagatorResult.GetMemberValue(ordinal2).Identifier;
				int identifier2 = propagatorResult2.GetMemberValue(ordinal2).Identifier;
				num = identifier - identifier2;
				if (num != 0)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x0400128A RID: 4746
		private readonly ModificationOperator m_operator;

		// Token: 0x0400128B RID: 4747
		private readonly TableChangeProcessor m_processor;

		// Token: 0x0400128C RID: 4748
		private readonly List<KeyValuePair<int, DbSetClause>> m_inputIdentifiers;

		// Token: 0x0400128D RID: 4749
		private readonly Dictionary<int, string> m_outputIdentifiers;

		// Token: 0x0400128E RID: 4750
		private readonly DbModificationCommandTree m_modificationCommandTree;
	}
}
