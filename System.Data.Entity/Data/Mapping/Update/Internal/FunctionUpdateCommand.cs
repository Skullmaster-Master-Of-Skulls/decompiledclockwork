using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Spatial;
using System.Globalization;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002C4 RID: 708
	internal sealed class FunctionUpdateCommand : UpdateCommand
	{
		// Token: 0x060029D9 RID: 10713 RVA: 0x000A37C4 File Offset: 0x000A19C4
		internal FunctionUpdateCommand(StorageModificationFunctionMapping functionMapping, UpdateTranslator translator, ReadOnlyCollection<IEntityStateEntry> stateEntries, ExtractedStateEntry stateEntry) : base(stateEntry.Original, stateEntry.Current)
		{
			EntityUtil.CheckArgumentNull<StorageModificationFunctionMapping>(functionMapping, "functionMapping");
			EntityUtil.CheckArgumentNull<UpdateTranslator>(translator, "translator");
			EntityUtil.CheckArgumentNull<ReadOnlyCollection<IEntityStateEntry>>(stateEntries, "stateEntries");
			this.m_stateEntries = stateEntries;
			DbCommandDefinition dbCommandDefinition = translator.GenerateCommandDefinition(functionMapping);
			this.m_dbCommand = dbCommandDefinition.CreateCommand();
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x000A3824 File Offset: 0x000A1A24
		internal override IEnumerable<int> InputIdentifiers
		{
			get
			{
				if (this.m_inputIdentifiers == null)
				{
					yield break;
				}
				foreach (KeyValuePair<int, DbParameter> keyValuePair in this.m_inputIdentifiers)
				{
					yield return keyValuePair.Key;
				}
				List<KeyValuePair<int, DbParameter>>.Enumerator enumerator = default(List<KeyValuePair<int, DbParameter>>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x000A3841 File Offset: 0x000A1A41
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

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x00017938 File Offset: 0x00015B38
		internal override UpdateCommandKind Kind
		{
			get
			{
				return UpdateCommandKind.Function;
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000A385C File Offset: 0x000A1A5C
		internal override IList<IEntityStateEntry> GetStateEntries(UpdateTranslator translator)
		{
			return this.m_stateEntries;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000A3864 File Offset: 0x000A1A64
		internal void SetParameterValue(PropagatorResult result, StorageModificationFunctionParameterBinding parameterBinding, UpdateTranslator translator)
		{
			DbParameter dbParameter = this.m_dbCommand.Parameters[parameterBinding.Parameter.Name];
			TypeUsage typeUsage = parameterBinding.Parameter.TypeUsage;
			object principalValue = translator.KeyManager.GetPrincipalValue(result);
			translator.SetParameterValue(dbParameter, typeUsage, principalValue);
			int identifier = result.Identifier;
			if (-1 != identifier)
			{
				if (this.m_inputIdentifiers == null)
				{
					this.m_inputIdentifiers = new List<KeyValuePair<int, DbParameter>>(2);
				}
				foreach (int key in translator.KeyManager.GetPrincipals(identifier))
				{
					this.m_inputIdentifiers.Add(new KeyValuePair<int, DbParameter>(key, dbParameter));
				}
			}
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000A3928 File Offset: 0x000A1B28
		internal void RegisterRowsAffectedParameter(FunctionParameter rowsAffectedParameter)
		{
			if (rowsAffectedParameter != null)
			{
				this.m_rowsAffectedParameter = this.m_dbCommand.Parameters[rowsAffectedParameter.Name];
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000A394C File Offset: 0x000A1B4C
		internal void AddResultColumn(UpdateTranslator translator, string columnName, PropagatorResult result)
		{
			if (this.m_resultColumns == null)
			{
				this.m_resultColumns = new List<KeyValuePair<string, PropagatorResult>>(2);
			}
			this.m_resultColumns.Add(new KeyValuePair<string, PropagatorResult>(columnName, result));
			int identifier = result.Identifier;
			if (-1 != identifier)
			{
				if (translator.KeyManager.HasPrincipals(identifier))
				{
					throw EntityUtil.InvalidOperation(Strings.Update_GeneratedDependent(columnName));
				}
				this.AddOutputIdentifier(columnName, identifier);
			}
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000A39AC File Offset: 0x000A1BAC
		private void AddOutputIdentifier(string columnName, int identifier)
		{
			if (this.m_outputIdentifiers == null)
			{
				this.m_outputIdentifiers = new Dictionary<int, string>(2);
			}
			this.m_outputIdentifiers[identifier] = columnName;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000A39D0 File Offset: 0x000A1BD0
		internal override long Execute(UpdateTranslator translator, EntityConnection connection, Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues)
		{
			this.m_dbCommand.Transaction = ((connection.CurrentTransaction != null) ? connection.CurrentTransaction.StoreTransaction : null);
			this.m_dbCommand.Connection = connection.StoreConnection;
			if (translator.CommandTimeout != null)
			{
				this.m_dbCommand.CommandTimeout = translator.CommandTimeout.Value;
			}
			if (this.m_inputIdentifiers != null)
			{
				foreach (KeyValuePair<int, DbParameter> keyValuePair in this.m_inputIdentifiers)
				{
					object value;
					if (identifierValues.TryGetValue(keyValuePair.Key, out value))
					{
						keyValuePair.Value.Value = value;
					}
				}
			}
			long num;
			if (this.m_resultColumns != null)
			{
				num = 0L;
				IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType);
				using (DbDataReader reader = this.m_dbCommand.ExecuteReader(CommandBehavior.SequentialAccess))
				{
					if (reader.Read())
					{
						num += 1L;
						IEnumerable<KeyValuePair<string, PropagatorResult>> resultColumns = this.m_resultColumns;
						Func<KeyValuePair<string, PropagatorResult>, KeyValuePair<int, PropagatorResult>> <>9__0;
						Func<KeyValuePair<string, PropagatorResult>, KeyValuePair<int, PropagatorResult>> selector;
						if ((selector = <>9__0) == null)
						{
							selector = (<>9__0 = ((KeyValuePair<string, PropagatorResult> r) => new KeyValuePair<int, PropagatorResult>(this.GetColumnOrdinal(translator, reader, r.Key), r.Value)));
						}
						foreach (KeyValuePair<int, PropagatorResult> keyValuePair2 in from r in resultColumns.Select(selector)
						orderby r.Key
						select r)
						{
							int key = keyValuePair2.Key;
							TypeUsage typeUsage = allStructuralMembers[keyValuePair2.Value.RecordOrdinal].TypeUsage;
							object value2;
							if (Helper.IsSpatialType(typeUsage) && !reader.IsDBNull(key))
							{
								value2 = SpatialHelpers.GetSpatialValue(translator.MetadataWorkspace, reader, typeUsage, key);
							}
							else
							{
								value2 = reader.GetValue(key);
							}
							PropagatorResult value3 = keyValuePair2.Value;
							generatedValues.Add(new KeyValuePair<PropagatorResult, object>(value3, value2));
							int identifier = value3.Identifier;
							if (-1 != identifier)
							{
								identifierValues.Add(identifier, value2);
							}
						}
					}
					CommandHelper.ConsumeReader(reader);
					goto IL_25E;
				}
			}
			num = (long)this.m_dbCommand.ExecuteNonQuery();
			IL_25E:
			if (this.m_rowsAffectedParameter != null)
			{
				if (DBNull.Value.Equals(this.m_rowsAffectedParameter.Value))
				{
					num = 0L;
				}
				else
				{
					try
					{
						num = Convert.ToInt64(this.m_rowsAffectedParameter.Value, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						if (UpdateTranslator.RequiresContext(ex))
						{
							throw EntityUtil.Update(Strings.Update_UnableToConvertRowsAffectedParameterToInt32(this.m_rowsAffectedParameter.ParameterName, typeof(int).FullName), ex, this.GetStateEntries(translator));
						}
						throw;
					}
				}
			}
			return num;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000A3D20 File Offset: 0x000A1F20
		private int GetColumnOrdinal(UpdateTranslator translator, DbDataReader reader, string columnName)
		{
			int ordinal;
			try
			{
				ordinal = reader.GetOrdinal(columnName);
			}
			catch (IndexOutOfRangeException)
			{
				throw EntityUtil.Update(Strings.Update_MissingResultColumn(columnName), null, this.GetStateEntries(translator));
			}
			return ordinal;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000A3D60 File Offset: 0x000A1F60
		private static ModificationOperator GetModificationOperator(EntityState state)
		{
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state != EntityState.Added)
					{
						return ModificationOperator.Update;
					}
					return ModificationOperator.Insert;
				}
			}
			else if (state == EntityState.Deleted)
			{
				return ModificationOperator.Delete;
			}
			return ModificationOperator.Update;
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000A3D80 File Offset: 0x000A1F80
		internal override int CompareToType(UpdateCommand otherCommand)
		{
			FunctionUpdateCommand functionUpdateCommand = (FunctionUpdateCommand)otherCommand;
			IEntityStateEntry entityStateEntry = this.m_stateEntries[0];
			IEntityStateEntry entityStateEntry2 = functionUpdateCommand.m_stateEntries[0];
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
			int num2 = (this.m_inputIdentifiers == null) ? 0 : this.m_inputIdentifiers.Count;
			int num3 = (functionUpdateCommand.m_inputIdentifiers == null) ? 0 : functionUpdateCommand.m_inputIdentifiers.Count;
			num = num2 - num3;
			if (num != 0)
			{
				return num;
			}
			for (int i = 0; i < num2; i++)
			{
				DbParameter value = this.m_inputIdentifiers[i].Value;
				DbParameter value2 = functionUpdateCommand.m_inputIdentifiers[i].Value;
				num = ByValueComparer.Default.Compare(value.Value, value2.Value);
				if (num != 0)
				{
					return num;
				}
			}
			for (int j = 0; j < num2; j++)
			{
				int key = this.m_inputIdentifiers[j].Key;
				int key2 = functionUpdateCommand.m_inputIdentifiers[j].Key;
				num = key - key2;
				if (num != 0)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x040012A7 RID: 4775
		private readonly ReadOnlyCollection<IEntityStateEntry> m_stateEntries;

		// Token: 0x040012A8 RID: 4776
		private readonly DbCommand m_dbCommand;

		// Token: 0x040012A9 RID: 4777
		private List<KeyValuePair<string, PropagatorResult>> m_resultColumns;

		// Token: 0x040012AA RID: 4778
		private List<KeyValuePair<int, DbParameter>> m_inputIdentifiers;

		// Token: 0x040012AB RID: 4779
		private Dictionary<int, string> m_outputIdentifiers;

		// Token: 0x040012AC RID: 4780
		private DbParameter m_rowsAffectedParameter;
	}
}
