using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003F5 RID: 1013
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class FunctionUpdateCommand : UpdateCommand
	{
		// Token: 0x06002552 RID: 9554 RVA: 0x000B11A4 File Offset: 0x000AF3A4
		internal FunctionUpdateCommand(ModificationFunctionMapping functionMapping, UpdateTranslator translator, ReadOnlyCollection<IEntityStateEntry> stateEntries, ExtractedStateEntry stateEntry) : this(translator, stateEntries, stateEntry, translator.GenerateCommandDefinition(functionMapping).CreateCommand())
		{
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000B11BC File Offset: 0x000AF3BC
		protected FunctionUpdateCommand(UpdateTranslator translator, ReadOnlyCollection<IEntityStateEntry> stateEntries, ExtractedStateEntry stateEntry, DbCommand dbCommand) : base(translator, stateEntry.Original, stateEntry.Current)
		{
			this._stateEntries = stateEntries;
			this._dbCommand = new InterceptableDbCommand(dbCommand, translator.InterceptionContext, null);
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x000B11EE File Offset: 0x000AF3EE
		// (set) Token: 0x06002555 RID: 9557 RVA: 0x000B11F6 File Offset: 0x000AF3F6
		protected virtual List<KeyValuePair<string, PropagatorResult>> ResultColumns { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x000B13A4 File Offset: 0x000AF5A4
		internal override IEnumerable<int> InputIdentifiers
		{
			get
			{
				if (this._inputIdentifiers != null)
				{
					foreach (KeyValuePair<int, DbParameter> inputIdentifier in this._inputIdentifiers)
					{
						KeyValuePair<int, DbParameter> keyValuePair = inputIdentifier;
						yield return keyValuePair.Key;
					}
				}
				yield break;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000B13C1 File Offset: 0x000AF5C1
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

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000B13DC File Offset: 0x000AF5DC
		internal override UpdateCommandKind Kind
		{
			get
			{
				return UpdateCommandKind.Function;
			}
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000B13DF File Offset: 0x000AF5DF
		internal override IList<IEntityStateEntry> GetStateEntries(UpdateTranslator translator)
		{
			return this._stateEntries;
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000B13E8 File Offset: 0x000AF5E8
		internal void SetParameterValue(PropagatorResult result, ModificationFunctionParameterBinding parameterBinding, UpdateTranslator translator)
		{
			DbParameter dbParameter = this._dbCommand.Parameters[parameterBinding.Parameter.Name];
			TypeUsage typeUsage = parameterBinding.Parameter.TypeUsage;
			object principalValue = translator.KeyManager.GetPrincipalValue(result);
			translator.SetParameterValue(dbParameter, typeUsage, principalValue);
			int identifier = result.Identifier;
			if (-1 != identifier)
			{
				if (this._inputIdentifiers == null)
				{
					this._inputIdentifiers = new List<KeyValuePair<int, DbParameter>>(2);
				}
				foreach (int key in translator.KeyManager.GetPrincipals(identifier))
				{
					this._inputIdentifiers.Add(new KeyValuePair<int, DbParameter>(key, dbParameter));
				}
			}
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000B14AC File Offset: 0x000AF6AC
		internal void RegisterRowsAffectedParameter(FunctionParameter rowsAffectedParameter)
		{
			if (rowsAffectedParameter != null)
			{
				this._rowsAffectedParameter = this._dbCommand.Parameters[rowsAffectedParameter.Name];
			}
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000B14D0 File Offset: 0x000AF6D0
		internal void AddResultColumn(UpdateTranslator translator, string columnName, PropagatorResult result)
		{
			if (this.ResultColumns == null)
			{
				this.ResultColumns = new List<KeyValuePair<string, PropagatorResult>>(2);
			}
			this.ResultColumns.Add(new KeyValuePair<string, PropagatorResult>(columnName, result));
			int identifier = result.Identifier;
			if (-1 != identifier)
			{
				if (translator.KeyManager.HasPrincipals(identifier))
				{
					throw new InvalidOperationException(Strings.Update_GeneratedDependent(columnName));
				}
				this.AddOutputIdentifier(columnName, identifier);
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000B1530 File Offset: 0x000AF730
		private void AddOutputIdentifier(string columnName, int identifier)
		{
			if (this._outputIdentifiers == null)
			{
				this._outputIdentifiers = new Dictionary<int, string>(2);
			}
			this._outputIdentifiers[identifier] = columnName;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000B1554 File Offset: 0x000AF754
		internal virtual void SetInputIdentifiers(Dictionary<int, object> identifierValues)
		{
			if (this._inputIdentifiers != null)
			{
				foreach (KeyValuePair<int, DbParameter> keyValuePair in this._inputIdentifiers)
				{
					object value;
					if (identifierValues.TryGetValue(keyValuePair.Key, out value))
					{
						keyValuePair.Value.Value = value;
					}
				}
			}
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000B160C File Offset: 0x000AF80C
		internal override long Execute(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues)
		{
			EntityConnection connection = base.Translator.Connection;
			this._dbCommand.Transaction = ((connection.CurrentTransaction == null) ? null : connection.CurrentTransaction.StoreTransaction);
			this._dbCommand.Connection = connection.StoreConnection;
			if (base.Translator.CommandTimeout != null)
			{
				this._dbCommand.CommandTimeout = base.Translator.CommandTimeout.Value;
			}
			this.SetInputIdentifiers(identifierValues);
			long num;
			if (this.ResultColumns != null)
			{
				num = 0L;
				IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType);
				using (DbDataReader reader = this._dbCommand.ExecuteReader(CommandBehavior.SequentialAccess))
				{
					if (reader.Read())
					{
						num += 1L;
						foreach (KeyValuePair<int, PropagatorResult> keyValuePair in from r in this.ResultColumns
						select new KeyValuePair<int, PropagatorResult>(this.GetColumnOrdinal(this.Translator, reader, r.Key), r.Value) into r
						orderby r.Key
						select r)
						{
							int key = keyValuePair.Key;
							if (key == -1)
							{
								break;
							}
							TypeUsage typeUsage = allStructuralMembers[keyValuePair.Value.RecordOrdinal].TypeUsage;
							object value;
							if (Helper.IsSpatialType(typeUsage) && !reader.IsDBNull(key))
							{
								value = SpatialHelpers.GetSpatialValue(base.Translator.MetadataWorkspace, reader, typeUsage, key);
							}
							else
							{
								value = reader.GetValue(key);
							}
							PropagatorResult value2 = keyValuePair.Value;
							generatedValues.Add(new KeyValuePair<PropagatorResult, object>(value2, value));
							int identifier = value2.Identifier;
							if (-1 != identifier)
							{
								identifierValues.Add(identifier, value);
							}
						}
					}
					CommandHelper.ConsumeReader(reader);
					goto IL_218;
				}
			}
			num = (long)this._dbCommand.ExecuteNonQuery();
			IL_218:
			return this.GetRowsAffected(num, base.Translator);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000B2080 File Offset: 0x000B0280
		internal override async Task<long> ExecuteAsync(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			EntityConnection connection = base.Translator.Connection;
			this._dbCommand.Transaction = ((connection.CurrentTransaction == null) ? null : connection.CurrentTransaction.StoreTransaction);
			this._dbCommand.Connection = connection.StoreConnection;
			if (base.Translator.CommandTimeout != null)
			{
				this._dbCommand.CommandTimeout = base.Translator.CommandTimeout.Value;
			}
			this.SetInputIdentifiers(identifierValues);
			long rowsAffected;
			if (this.ResultColumns != null)
			{
				rowsAffected = 0L;
				IBaseList<EdmMember> members = TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType);
				using (DbDataReader reader = await this._dbCommand.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<DbDataReader>())
				{
					if (await reader.ReadAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						rowsAffected += 1L;
						foreach (KeyValuePair<int, PropagatorResult> resultColumn in from r in this.ResultColumns
						select new KeyValuePair<int, PropagatorResult>(this.GetColumnOrdinal(this.Translator, reader, r.Key), r.Value) into r
						orderby r.Key
						select r)
						{
							KeyValuePair<int, PropagatorResult> keyValuePair = resultColumn;
							int columnOrdinal = keyValuePair.Key;
							IBaseList<EdmMember> baseList = members;
							KeyValuePair<int, PropagatorResult> keyValuePair2 = resultColumn;
							TypeUsage columnType = baseList[keyValuePair2.Value.RecordOrdinal].TypeUsage;
							object value;
							if (Helper.IsSpatialType(columnType) && !(await reader.IsDBNullAsync(columnOrdinal, cancellationToken).WithCurrentCulture<bool>()))
							{
								value = await SpatialHelpers.GetSpatialValueAsync(base.Translator.MetadataWorkspace, reader, columnType, columnOrdinal, cancellationToken).WithCurrentCulture<object>();
							}
							else
							{
								value = await reader.GetFieldValueAsync<object>(columnOrdinal, cancellationToken).WithCurrentCulture<object>();
							}
							KeyValuePair<int, PropagatorResult> keyValuePair3 = resultColumn;
							PropagatorResult result = keyValuePair3.Value;
							generatedValues.Add(new KeyValuePair<PropagatorResult, object>(result, value));
							int identifier = result.Identifier;
							if (-1 != identifier)
							{
								identifierValues.Add(identifier, value);
							}
						}
					}
					await CommandHelper.ConsumeReaderAsync(reader, cancellationToken).WithCurrentCulture();
					goto IL_715;
				}
			}
			rowsAffected = await this._dbCommand.ExecuteNonQueryAsync(cancellationToken).WithCurrentCulture<int>();
			IL_715:
			return this.GetRowsAffected(rowsAffected, base.Translator);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000B20E0 File Offset: 0x000B02E0
		protected virtual long GetRowsAffected(long rowsAffected, UpdateTranslator translator)
		{
			if (this._rowsAffectedParameter != null)
			{
				if (DBNull.Value.Equals(this._rowsAffectedParameter.Value))
				{
					rowsAffected = 0L;
				}
				else
				{
					try
					{
						rowsAffected = Convert.ToInt64(this._rowsAffectedParameter.Value, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						if (ex.RequiresContext())
						{
							throw new UpdateException(Strings.Update_UnableToConvertRowsAffectedParameter(this._rowsAffectedParameter.ParameterName, typeof(long).FullName), ex, this.GetStateEntries(translator).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
						}
						throw;
					}
				}
			}
			return rowsAffected;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000B2180 File Offset: 0x000B0380
		private int GetColumnOrdinal(UpdateTranslator translator, DbDataReader reader, string columnName)
		{
			int ordinal;
			try
			{
				ordinal = reader.GetOrdinal(columnName);
			}
			catch (IndexOutOfRangeException)
			{
				throw new UpdateException(Strings.Update_MissingResultColumn(columnName), null, this.GetStateEntries(translator).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
			}
			return ordinal;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000B21C8 File Offset: 0x000B03C8
		private static ModificationOperator GetModificationOperator(EntityState state)
		{
			switch (state)
			{
			case EntityState.Unchanged:
				break;
			case EntityState.Detached | EntityState.Unchanged:
				return ModificationOperator.Update;
			case EntityState.Added:
				return ModificationOperator.Insert;
			default:
				if (state == EntityState.Deleted)
				{
					return ModificationOperator.Delete;
				}
				if (state != EntityState.Modified)
				{
					return ModificationOperator.Update;
				}
				break;
			}
			return ModificationOperator.Update;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000B21FC File Offset: 0x000B03FC
		internal override int CompareToType(UpdateCommand otherCommand)
		{
			FunctionUpdateCommand functionUpdateCommand = (FunctionUpdateCommand)otherCommand;
			IEntityStateEntry entityStateEntry = this._stateEntries[0];
			IEntityStateEntry entityStateEntry2 = functionUpdateCommand._stateEntries[0];
			int num = (int)(FunctionUpdateCommand.GetModificationOperator(entityStateEntry.State) - FunctionUpdateCommand.GetModificationOperator(entityStateEntry2.State));
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.Ordinal.Compare(entityStateEntry.EntitySet.Name, entityStateEntry2.EntitySet.Name);
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.Ordinal.Compare(entityStateEntry.EntitySet.EntityContainer.Name, entityStateEntry2.EntitySet.EntityContainer.Name);
			if (num != 0)
			{
				return num;
			}
			int num2 = (this._inputIdentifiers == null) ? 0 : this._inputIdentifiers.Count;
			int num3 = (functionUpdateCommand._inputIdentifiers == null) ? 0 : functionUpdateCommand._inputIdentifiers.Count;
			num = num2 - num3;
			if (num != 0)
			{
				return num;
			}
			for (int i = 0; i < num2; i++)
			{
				DbParameter value = this._inputIdentifiers[i].Value;
				DbParameter value2 = functionUpdateCommand._inputIdentifiers[i].Value;
				num = ByValueComparer.Default.Compare(value.Value, value2.Value);
				if (num != 0)
				{
					return num;
				}
			}
			for (int j = 0; j < num2; j++)
			{
				int key = this._inputIdentifiers[j].Key;
				int key2 = functionUpdateCommand._inputIdentifiers[j].Key;
				num = key - key2;
				if (num != 0)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x04000DE1 RID: 3553
		private readonly ReadOnlyCollection<IEntityStateEntry> _stateEntries;

		// Token: 0x04000DE2 RID: 3554
		private readonly DbCommand _dbCommand;

		// Token: 0x04000DE3 RID: 3555
		private List<KeyValuePair<int, DbParameter>> _inputIdentifiers;

		// Token: 0x04000DE4 RID: 3556
		private Dictionary<int, string> _outputIdentifiers;

		// Token: 0x04000DE5 RID: 3557
		private DbParameter _rowsAffectedParameter;
	}
}
