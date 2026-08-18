using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003EE RID: 1006
	internal class DynamicUpdateCommand : UpdateCommand
	{
		// Token: 0x06002529 RID: 9513 RVA: 0x000AF1E8 File Offset: 0x000AD3E8
		internal DynamicUpdateCommand(TableChangeProcessor processor, UpdateTranslator translator, ModificationOperator modificationOperator, PropagatorResult originalValues, PropagatorResult currentValues, DbModificationCommandTree tree, Dictionary<int, string> outputIdentifiers) : base(translator, originalValues, currentValues)
		{
			this._processor = processor;
			this._operator = modificationOperator;
			this._modificationCommandTree = tree;
			this._outputIdentifiers = outputIdentifiers;
			if (ModificationOperator.Insert == modificationOperator || modificationOperator == ModificationOperator.Update)
			{
				this._inputIdentifiers = new List<KeyValuePair<int, DbSetClause>>(2);
				foreach (KeyValuePair<EdmMember, PropagatorResult> keyValuePair in Helper.PairEnumerations<EdmMember, PropagatorResult>(TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType), base.CurrentValues.GetMemberValues()))
				{
					int identifier = keyValuePair.Value.Identifier;
					DbSetClause value;
					if (-1 != identifier && DynamicUpdateCommand.TryGetSetterExpression(tree, keyValuePair.Key, modificationOperator, out value))
					{
						foreach (int key in translator.KeyManager.GetPrincipals(identifier))
						{
							this._inputIdentifiers.Add(new KeyValuePair<int, DbSetClause>(key, value));
						}
					}
				}
			}
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000AF304 File Offset: 0x000AD504
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

		// Token: 0x0600252B RID: 9515 RVA: 0x000AF390 File Offset: 0x000AD590
		internal override long Execute(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues)
		{
			long result;
			using (DbCommand dbCommand = this.CreateCommand(identifierValues))
			{
				EntityConnection connection = base.Translator.Connection;
				dbCommand.Transaction = ((connection.CurrentTransaction == null) ? null : connection.CurrentTransaction.StoreTransaction);
				dbCommand.Connection = connection.StoreConnection;
				if (base.Translator.CommandTimeout != null)
				{
					dbCommand.CommandTimeout = base.Translator.CommandTimeout.Value;
				}
				int num;
				if (this._modificationCommandTree.HasReader)
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
									value = SpatialHelpers.GetSpatialValue(base.Translator.MetadataWorkspace, dbDataReader, edmMember.TypeUsage, i);
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
						goto IL_172;
					}
				}
				num = dbCommand.ExecuteNonQuery();
				IL_172:
				result = (long)num;
			}
			return result;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000AFC60 File Offset: 0x000ADE60
		internal override async Task<long> ExecuteAsync(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long result2;
			using (DbCommand command = this.CreateCommand(identifierValues))
			{
				EntityConnection connection = base.Translator.Connection;
				command.Transaction = ((connection.CurrentTransaction == null) ? null : connection.CurrentTransaction.StoreTransaction);
				command.Connection = connection.StoreConnection;
				if (base.Translator.CommandTimeout != null)
				{
					command.CommandTimeout = base.Translator.CommandTimeout.Value;
				}
				int rowsAffected;
				if (this._modificationCommandTree.HasReader)
				{
					rowsAffected = 0;
					using (DbDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<DbDataReader>())
					{
						if (await reader.ReadAsync(cancellationToken).WithCurrentCulture<bool>())
						{
							rowsAffected++;
							IBaseList<EdmMember> members = TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType);
							for (int ordinal = 0; ordinal < reader.FieldCount; ordinal++)
							{
								string columnName = reader.GetName(ordinal);
								EdmMember member = members[columnName];
								object value;
								if (Helper.IsSpatialType(member.TypeUsage) && !(await reader.IsDBNullAsync(ordinal, cancellationToken).WithCurrentCulture<bool>()))
								{
									value = await SpatialHelpers.GetSpatialValueAsync(base.Translator.MetadataWorkspace, reader, member.TypeUsage, ordinal, cancellationToken).WithCurrentCulture<object>();
								}
								else
								{
									value = await reader.GetFieldValueAsync<object>(ordinal, cancellationToken).WithCurrentCulture<object>();
								}
								int columnOrdinal = members.IndexOf(member);
								PropagatorResult result = base.CurrentValues.GetMemberValue(columnOrdinal);
								generatedValues.Add(new KeyValuePair<PropagatorResult, object>(result, value));
								int identifier = result.Identifier;
								if (-1 != identifier)
								{
									identifierValues.Add(identifier, value);
								}
							}
						}
						await CommandHelper.ConsumeReaderAsync(reader, cancellationToken).WithCurrentCulture();
						goto IL_651;
					}
				}
				rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken).WithCurrentCulture<int>();
				IL_651:
				result2 = (long)rowsAffected;
			}
			return result2;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000AFCC0 File Offset: 0x000ADEC0
		protected virtual DbCommand CreateCommand(Dictionary<int, object> identifierValues)
		{
			DbModificationCommandTree dbModificationCommandTree = this._modificationCommandTree;
			if (this._inputIdentifiers != null)
			{
				Dictionary<DbSetClause, DbSetClause> dictionary = new Dictionary<DbSetClause, DbSetClause>();
				for (int i = 0; i < this._inputIdentifiers.Count; i++)
				{
					KeyValuePair<int, DbSetClause> keyValuePair = this._inputIdentifiers[i];
					object value;
					if (identifierValues.TryGetValue(keyValuePair.Key, out value))
					{
						DbSetClause value2 = new DbSetClause(keyValuePair.Value.Property, DbExpressionBuilder.Constant(value));
						dictionary[keyValuePair.Value] = value2;
						this._inputIdentifiers[i] = new KeyValuePair<int, DbSetClause>(keyValuePair.Key, value2);
					}
				}
				dbModificationCommandTree = DynamicUpdateCommand.RebuildCommandTree(dbModificationCommandTree, dictionary);
			}
			return base.Translator.CreateCommand(dbModificationCommandTree);
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000AFD70 File Offset: 0x000ADF70
		private static DbModificationCommandTree RebuildCommandTree(DbModificationCommandTree originalTree, Dictionary<DbSetClause, DbSetClause> clauseMappings)
		{
			if (clauseMappings.Count == 0)
			{
				return originalTree;
			}
			DbModificationCommandTree result;
			if (originalTree.CommandTreeKind == DbCommandTreeKind.Insert)
			{
				DbInsertCommandTree dbInsertCommandTree = (DbInsertCommandTree)originalTree;
				result = new DbInsertCommandTree(dbInsertCommandTree.MetadataWorkspace, dbInsertCommandTree.DataSpace, dbInsertCommandTree.Target, new ReadOnlyCollection<DbModificationClause>(DynamicUpdateCommand.ReplaceClauses(dbInsertCommandTree.SetClauses, clauseMappings)), dbInsertCommandTree.Returning);
			}
			else
			{
				DbUpdateCommandTree dbUpdateCommandTree = (DbUpdateCommandTree)originalTree;
				result = new DbUpdateCommandTree(dbUpdateCommandTree.MetadataWorkspace, dbUpdateCommandTree.DataSpace, dbUpdateCommandTree.Target, dbUpdateCommandTree.Predicate, new ReadOnlyCollection<DbModificationClause>(DynamicUpdateCommand.ReplaceClauses(dbUpdateCommandTree.SetClauses, clauseMappings)), dbUpdateCommandTree.Returning);
			}
			return result;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000AFE08 File Offset: 0x000AE008
		private static List<DbModificationClause> ReplaceClauses(IList<DbModificationClause> originalClauses, Dictionary<DbSetClause, DbSetClause> mappings)
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

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06002530 RID: 9520 RVA: 0x000AFE5F File Offset: 0x000AE05F
		internal ModificationOperator Operator
		{
			get
			{
				return this._operator;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x000AFE67 File Offset: 0x000AE067
		internal override EntitySet Table
		{
			get
			{
				return this._processor.Table;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x000B0018 File Offset: 0x000AE218
		internal override IEnumerable<int> InputIdentifiers
		{
			get
			{
				if (this._inputIdentifiers != null)
				{
					foreach (KeyValuePair<int, DbSetClause> inputIdentifier in this._inputIdentifiers)
					{
						KeyValuePair<int, DbSetClause> keyValuePair = inputIdentifier;
						yield return keyValuePair.Key;
					}
				}
				yield break;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x000B0035 File Offset: 0x000AE235
		internal override IEnumerable<int> OutputIdentifiers
		{
			get
			{
				if (this._outputIdentifiers == null)
				{
					return Enumerable.Empty<int>();
				}
				return this._outputIdentifiers.Keys;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x000B0050 File Offset: 0x000AE250
		internal override UpdateCommandKind Kind
		{
			get
			{
				return UpdateCommandKind.Dynamic;
			}
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000B0054 File Offset: 0x000AE254
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

		// Token: 0x06002536 RID: 9526 RVA: 0x000B0114 File Offset: 0x000AE314
		internal override int CompareToType(UpdateCommand otherCommand)
		{
			DynamicUpdateCommand dynamicUpdateCommand = (DynamicUpdateCommand)otherCommand;
			int num = (int)(this.Operator - dynamicUpdateCommand.Operator);
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.Ordinal.Compare(this._processor.Table.Name, dynamicUpdateCommand._processor.Table.Name);
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.Ordinal.Compare(this._processor.Table.EntityContainer.Name, dynamicUpdateCommand._processor.Table.EntityContainer.Name);
			if (num != 0)
			{
				return num;
			}
			PropagatorResult propagatorResult = (this.Operator == ModificationOperator.Delete) ? base.OriginalValues : base.CurrentValues;
			PropagatorResult propagatorResult2 = (dynamicUpdateCommand.Operator == ModificationOperator.Delete) ? dynamicUpdateCommand.OriginalValues : dynamicUpdateCommand.CurrentValues;
			for (int i = 0; i < this._processor.KeyOrdinals.Length; i++)
			{
				int ordinal = this._processor.KeyOrdinals[i];
				object simpleValue = propagatorResult.GetMemberValue(ordinal).GetSimpleValue();
				object simpleValue2 = propagatorResult2.GetMemberValue(ordinal).GetSimpleValue();
				num = ByValueComparer.Default.Compare(simpleValue, simpleValue2);
				if (num != 0)
				{
					return num;
				}
			}
			for (int j = 0; j < this._processor.KeyOrdinals.Length; j++)
			{
				int ordinal2 = this._processor.KeyOrdinals[j];
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

		// Token: 0x04000DC7 RID: 3527
		private readonly ModificationOperator _operator;

		// Token: 0x04000DC8 RID: 3528
		private readonly TableChangeProcessor _processor;

		// Token: 0x04000DC9 RID: 3529
		private readonly List<KeyValuePair<int, DbSetClause>> _inputIdentifiers;

		// Token: 0x04000DCA RID: 3530
		private readonly Dictionary<int, string> _outputIdentifiers;

		// Token: 0x04000DCB RID: 3531
		private readonly DbModificationCommandTree _modificationCommandTree;
	}
}
