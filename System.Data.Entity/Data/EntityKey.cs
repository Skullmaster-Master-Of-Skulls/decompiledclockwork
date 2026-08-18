using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.EntityModel.SchemaObjectModel;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace System.Data
{
	// Token: 0x02000018 RID: 24
	[DebuggerDisplay("{ConcatKeyValue()}")]
	[DataContract(IsReference = true)]
	[Serializable]
	public sealed class EntityKey : IEquatable<EntityKey>
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00004E6E File Offset: 0x0000306E
		public EntityKey()
		{
			this._isLocked = false;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00004E7D File Offset: 0x0000307D
		public EntityKey(string qualifiedEntitySetName, IEnumerable<KeyValuePair<string, object>> entityKeyValues)
		{
			EntityKey.GetEntitySetName(qualifiedEntitySetName, out this._entitySetName, out this._entityContainerName);
			EntityKey.CheckKeyValues(entityKeyValues, out this._keyNames, out this._singletonKeyValue, out this._compositeKeyValues);
			this._isLocked = true;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004EB8 File Offset: 0x000030B8
		public EntityKey(string qualifiedEntitySetName, IEnumerable<EntityKeyMember> entityKeyValues)
		{
			EntityKey.GetEntitySetName(qualifiedEntitySetName, out this._entitySetName, out this._entityContainerName);
			EntityUtil.CheckArgumentNull<IEnumerable<EntityKeyMember>>(entityKeyValues, "entityKeyValues");
			EntityKey.CheckKeyValues(new EntityKey.KeyValueReader(entityKeyValues), out this._keyNames, out this._singletonKeyValue, out this._compositeKeyValues);
			this._isLocked = true;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004F10 File Offset: 0x00003110
		public EntityKey(string qualifiedEntitySetName, string keyName, object keyValue)
		{
			EntityKey.GetEntitySetName(qualifiedEntitySetName, out this._entitySetName, out this._entityContainerName);
			EntityUtil.CheckStringArgument(keyName, "keyName");
			EntityUtil.CheckArgumentNull<object>(keyValue, "keyValue");
			this._keyNames = new string[1];
			EntityKey.ValidateName(keyName);
			this._keyNames[0] = keyName;
			this._singletonKeyValue = keyValue;
			this._isLocked = true;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00004F78 File Offset: 0x00003178
		internal EntityKey(EntitySet entitySet, IExtendedDataRecord record)
		{
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			EntityKey.GetKeyValues(entitySet, record, out this._keyNames, out this._singletonKeyValue, out this._compositeKeyValues);
			this._isLocked = true;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004FC8 File Offset: 0x000031C8
		internal EntityKey(string qualifiedEntitySetName)
		{
			EntityKey.GetEntitySetName(qualifiedEntitySetName, out this._entitySetName, out this._entityContainerName);
			this._isLocked = true;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00004FE9 File Offset: 0x000031E9
		internal EntityKey(EntitySetBase entitySet)
		{
			EntityUtil.CheckArgumentNull<EntitySetBase>(entitySet, "entitySet");
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this._isLocked = true;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005024 File Offset: 0x00003224
		internal EntityKey(EntitySetBase entitySet, object singletonKeyValue)
		{
			this._singletonKeyValue = singletonKeyValue;
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this._keyNames = entitySet.ElementType.KeyMemberNames;
			this._isLocked = true;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005074 File Offset: 0x00003274
		internal EntityKey(EntitySetBase entitySet, object[] compositeKeyValues)
		{
			this._compositeKeyValues = compositeKeyValues;
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this._keyNames = entitySet.ElementType.KeyMemberNames;
			this._isLocked = true;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000050C3 File Offset: 0x000032C3
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000050CC File Offset: 0x000032CC
		[DataMember]
		public string EntitySetName
		{
			get
			{
				return this._entitySetName;
			}
			set
			{
				this.ValidateWritable(this._entitySetName);
				Dictionary<string, string> nameLookup = EntityKey._nameLookup;
				lock (nameLookup)
				{
					this._entitySetName = EntityKey.LookupSingletonName(value);
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00005120 File Offset: 0x00003320
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00005128 File Offset: 0x00003328
		[DataMember]
		public string EntityContainerName
		{
			get
			{
				return this._entityContainerName;
			}
			set
			{
				this.ValidateWritable(this._entityContainerName);
				Dictionary<string, string> nameLookup = EntityKey._nameLookup;
				lock (nameLookup)
				{
					this._entityContainerName = EntityKey.LookupSingletonName(value);
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000517C File Offset: 0x0000337C
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000051F4 File Offset: 0x000033F4
		[DataMember]
		public EntityKeyMember[] EntityKeyValues
		{
			get
			{
				if (!this.IsTemporary)
				{
					EntityKeyMember[] array;
					if (this._singletonKeyValue != null)
					{
						array = new EntityKeyMember[]
						{
							new EntityKeyMember(this._keyNames[0], this._singletonKeyValue)
						};
					}
					else
					{
						array = new EntityKeyMember[this._compositeKeyValues.Length];
						for (int i = 0; i < this._compositeKeyValues.Length; i++)
						{
							array[i] = new EntityKeyMember(this._keyNames[i], this._compositeKeyValues[i]);
						}
					}
					return array;
				}
				return null;
			}
			set
			{
				this.ValidateWritable(this._keyNames);
				if (value != null && !EntityKey.CheckKeyValues(new EntityKey.KeyValueReader(value), true, true, out this._keyNames, out this._singletonKeyValue, out this._compositeKeyValues))
				{
					this._deserializedMembers = value;
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000522D File Offset: 0x0000342D
		public bool IsTemporary
		{
			get
			{
				return this.SingletonKeyValue == null && this.CompositeKeyValues == null;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00005242 File Offset: 0x00003442
		private object SingletonKeyValue
		{
			get
			{
				if (this.RequiresDeserialization)
				{
					this.DeserializeMembers();
				}
				return this._singletonKeyValue;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00005258 File Offset: 0x00003458
		private object[] CompositeKeyValues
		{
			get
			{
				if (this.RequiresDeserialization)
				{
					this.DeserializeMembers();
				}
				return this._compositeKeyValues;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005270 File Offset: 0x00003470
		public EntitySet GetEntitySet(MetadataWorkspace metadataWorkspace)
		{
			EntityUtil.CheckArgumentNull<MetadataWorkspace>(metadataWorkspace, "metadataWorkspace");
			if (string.IsNullOrEmpty(this._entityContainerName) || string.IsNullOrEmpty(this._entitySetName))
			{
				throw EntityUtil.MissingQualifiedEntitySetName();
			}
			return metadataWorkspace.GetEntityContainer(this._entityContainerName, DataSpace.CSpace).GetEntitySetByName(this._entitySetName, false);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000052C2 File Offset: 0x000034C2
		public override bool Equals(object obj)
		{
			return EntityKey.InternalEquals(this, obj as EntityKey, true);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000052D1 File Offset: 0x000034D1
		public bool Equals(EntityKey other)
		{
			return EntityKey.InternalEquals(this, other, true);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000052DC File Offset: 0x000034DC
		public override int GetHashCode()
		{
			int num = this._hashCode;
			if (num == 0)
			{
				this._containsByteArray = false;
				if (this.RequiresDeserialization)
				{
					this.DeserializeMembers();
				}
				if (this._entitySetName != null)
				{
					num = this._entitySetName.GetHashCode();
				}
				if (this._entityContainerName != null)
				{
					num ^= this._entityContainerName.GetHashCode();
				}
				if (this._singletonKeyValue != null)
				{
					num = this.AddHashValue(num, this._singletonKeyValue);
				}
				else if (this._compositeKeyValues != null)
				{
					int i = 0;
					int num2 = this._compositeKeyValues.Length;
					while (i < num2)
					{
						num = this.AddHashValue(num, this._compositeKeyValues[i]);
						i++;
					}
				}
				else
				{
					num = base.GetHashCode();
				}
				if (this._isLocked || (!string.IsNullOrEmpty(this._entitySetName) && !string.IsNullOrEmpty(this._entityContainerName) && (this._singletonKeyValue != null || this._compositeKeyValues != null)))
				{
					this._hashCode = num;
				}
			}
			return num;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000053C0 File Offset: 0x000035C0
		private int AddHashValue(int hashCode, object keyValue)
		{
			byte[] array = keyValue as byte[];
			if (array != null)
			{
				hashCode ^= ByValueEqualityComparer.ComputeBinaryHashCode(array);
				this._containsByteArray = true;
				return hashCode;
			}
			return hashCode ^ keyValue.GetHashCode();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000052D1 File Offset: 0x000034D1
		public static bool operator ==(EntityKey key1, EntityKey key2)
		{
			return EntityKey.InternalEquals(key1, key2, true);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000053F2 File Offset: 0x000035F2
		public static bool operator !=(EntityKey key1, EntityKey key2)
		{
			return !EntityKey.InternalEquals(key1, key2, true);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005400 File Offset: 0x00003600
		internal static bool InternalEquals(EntityKey key1, EntityKey key2, bool compareEntitySets)
		{
			if (key1 == key2)
			{
				return true;
			}
			if (key1 == null || key2 == null)
			{
				return false;
			}
			if ((key1.GetHashCode() != key2.GetHashCode() && compareEntitySets) || key1._containsByteArray != key2._containsByteArray)
			{
				return false;
			}
			if (key1._singletonKeyValue != null)
			{
				if (key1._containsByteArray)
				{
					if (key2._singletonKeyValue == null)
					{
						return false;
					}
					if (!ByValueEqualityComparer.CompareBinaryValues((byte[])key1._singletonKeyValue, (byte[])key2._singletonKeyValue))
					{
						return false;
					}
				}
				else if (!key1._singletonKeyValue.Equals(key2._singletonKeyValue))
				{
					return false;
				}
				if (!string.Equals(key1._keyNames[0], key2._keyNames[0]))
				{
					return false;
				}
			}
			else
			{
				if (key1._compositeKeyValues == null || key2._compositeKeyValues == null || key1._compositeKeyValues.Length != key2._compositeKeyValues.Length)
				{
					return false;
				}
				if (key1._containsByteArray)
				{
					if (!EntityKey.CompositeValuesWithBinaryEqual(key1, key2))
					{
						return false;
					}
				}
				else if (!EntityKey.CompositeValuesEqual(key1, key2))
				{
					return false;
				}
			}
			return !compareEntitySets || (string.Equals(key1._entitySetName, key2._entitySetName) && string.Equals(key1._entityContainerName, key2._entityContainerName));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005518 File Offset: 0x00003718
		internal static bool CompositeValuesWithBinaryEqual(EntityKey key1, EntityKey key2)
		{
			for (int i = 0; i < key1._compositeKeyValues.Length; i++)
			{
				if (key1._keyNames[i].Equals(key2._keyNames[i]))
				{
					if (!ByValueEqualityComparer.Default.Equals(key1._compositeKeyValues[i], key2._compositeKeyValues[i]))
					{
						return false;
					}
				}
				else if (!EntityKey.ValuesWithBinaryEqual(key1._keyNames[i], key1._compositeKeyValues[i], key2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005588 File Offset: 0x00003788
		private static bool ValuesWithBinaryEqual(string keyName, object keyValue, EntityKey key2)
		{
			for (int i = 0; i < key2._keyNames.Length; i++)
			{
				if (string.Equals(keyName, key2._keyNames[i]))
				{
					return ByValueEqualityComparer.Default.Equals(keyValue, key2._compositeKeyValues[i]);
				}
			}
			return false;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000055D0 File Offset: 0x000037D0
		private static bool CompositeValuesEqual(EntityKey key1, EntityKey key2)
		{
			for (int i = 0; i < key1._compositeKeyValues.Length; i++)
			{
				if (key1._keyNames[i].Equals(key2._keyNames[i]))
				{
					if (!object.Equals(key1._compositeKeyValues[i], key2._compositeKeyValues[i]))
					{
						return false;
					}
				}
				else if (!EntityKey.ValuesEqual(key1._keyNames[i], key1._compositeKeyValues[i], key2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000563C File Offset: 0x0000383C
		private static bool ValuesEqual(string keyName, object keyValue, EntityKey key2)
		{
			for (int i = 0; i < key2._keyNames.Length; i++)
			{
				if (string.Equals(keyName, key2._keyNames[i]))
				{
					return object.Equals(keyValue, key2._compositeKeyValues[i]);
				}
			}
			return false;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000567C File Offset: 0x0000387C
		internal KeyValuePair<string, DbExpression>[] GetKeyValueExpressions(EntitySet entitySet)
		{
			int num = 0;
			if (!this.IsTemporary)
			{
				if (this._singletonKeyValue != null)
				{
					num = 1;
				}
				else
				{
					num = this._compositeKeyValues.Length;
				}
			}
			if (entitySet.ElementType.KeyMembers.Count != num)
			{
				throw EntityUtil.EntitySetDoesNotMatch("metadataWorkspace", TypeHelpers.GetFullName(entitySet));
			}
			KeyValuePair<string, DbExpression>[] array;
			if (this._singletonKeyValue != null)
			{
				EdmMember edmMember = entitySet.ElementType.KeyMembers[0];
				array = new KeyValuePair<string, DbExpression>[]
				{
					Helper.GetModelTypeUsage(edmMember).Constant(this._singletonKeyValue).As(edmMember.Name)
				};
			}
			else
			{
				array = new KeyValuePair<string, DbExpression>[this._compositeKeyValues.Length];
				for (int i = 0; i < this._compositeKeyValues.Length; i++)
				{
					EdmMember edmMember2 = entitySet.ElementType.KeyMembers[i];
					array[i] = Helper.GetModelTypeUsage(edmMember2).Constant(this._compositeKeyValues[i]).As(edmMember2.Name);
				}
			}
			return array;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005770 File Offset: 0x00003970
		internal string ConcatKeyValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EntitySet=").Append(this._entitySetName);
			if (!this.IsTemporary)
			{
				foreach (EntityKeyMember entityKeyMember in this.EntityKeyValues)
				{
					stringBuilder.Append(';');
					stringBuilder.Append(entityKeyMember.Key).Append("=").Append(entityKeyMember.Value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000057EC File Offset: 0x000039EC
		internal object FindValueByName(string keyName)
		{
			if (this.SingletonKeyValue != null)
			{
				return this._singletonKeyValue;
			}
			object[] compositeKeyValues = this.CompositeKeyValues;
			for (int i = 0; i < compositeKeyValues.Length; i++)
			{
				if (keyName == this._keyNames[i])
				{
					return compositeKeyValues[i];
				}
			}
			throw EntityUtil.ArgumentOutOfRange("keyName");
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000583C File Offset: 0x00003A3C
		internal static void GetEntitySetName(string qualifiedEntitySetName, out string entitySet, out string container)
		{
			entitySet = null;
			container = null;
			EntityUtil.CheckStringArgument(qualifiedEntitySetName, "qualifiedEntitySetName");
			string[] array = qualifiedEntitySetName.Split(new char[]
			{
				'.'
			});
			if (array.Length != 2)
			{
				throw EntityUtil.InvalidQualifiedEntitySetName();
			}
			container = array[0];
			entitySet = array[1];
			if (container == null || container.Length == 0 || entitySet == null || entitySet.Length == 0)
			{
				throw EntityUtil.InvalidQualifiedEntitySetName();
			}
			EntityKey.ValidateName(container);
			EntityKey.ValidateName(entitySet);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000058B0 File Offset: 0x00003AB0
		internal static void ValidateName(string name)
		{
			if (!Utils.ValidUndottedName(name))
			{
				throw EntityUtil.EntityKeyInvalidName(name);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000058C1 File Offset: 0x00003AC1
		private static bool CheckKeyValues(IEnumerable<KeyValuePair<string, object>> entityKeyValues, out string[] keyNames, out object singletonKeyValue, out object[] compositeKeyValues)
		{
			return EntityKey.CheckKeyValues(entityKeyValues, false, false, out keyNames, out singletonKeyValue, out compositeKeyValues);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000058D0 File Offset: 0x00003AD0
		private static bool CheckKeyValues(IEnumerable<KeyValuePair<string, object>> entityKeyValues, bool allowNullKeys, bool tokenizeStrings, out string[] keyNames, out object singletonKeyValue, out object[] compositeKeyValues)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<KeyValuePair<string, object>>>(entityKeyValues, "entityKeyValues");
			int num = 0;
			keyNames = null;
			singletonKeyValue = null;
			compositeKeyValues = null;
			foreach (KeyValuePair<string, object> keyValuePair in entityKeyValues)
			{
				num++;
			}
			int num2 = num;
			if (num2 == 0)
			{
				if (!allowNullKeys)
				{
					throw EntityUtil.EntityKeyMustHaveValues("entityKeyValues");
				}
			}
			else
			{
				keyNames = new string[num2];
				if (num2 == 1)
				{
					Dictionary<string, string> nameLookup = EntityKey._nameLookup;
					lock (nameLookup)
					{
						using (IEnumerator<KeyValuePair<string, object>> enumerator2 = entityKeyValues.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<string, object> keyValuePair2 = enumerator2.Current;
								if (EntityUtil.IsNull(keyValuePair2.Value) || string.IsNullOrEmpty(keyValuePair2.Key))
								{
									throw EntityUtil.NoNullsAllowedInKeyValuePairs("entityKeyValues");
								}
								EntityKey.ValidateName(keyValuePair2.Key);
								keyNames[0] = (tokenizeStrings ? EntityKey.LookupSingletonName(keyValuePair2.Key) : keyValuePair2.Key);
								singletonKeyValue = keyValuePair2.Value;
							}
							goto IL_1C3;
						}
					}
				}
				compositeKeyValues = new object[num2];
				int num3 = 0;
				Dictionary<string, string> nameLookup2 = EntityKey._nameLookup;
				lock (nameLookup2)
				{
					foreach (KeyValuePair<string, object> keyValuePair3 in entityKeyValues)
					{
						if (EntityUtil.IsNull(keyValuePair3.Value) || string.IsNullOrEmpty(keyValuePair3.Key))
						{
							throw EntityUtil.NoNullsAllowedInKeyValuePairs("entityKeyValues");
						}
						EntityKey.ValidateName(keyValuePair3.Key);
						keyNames[num3] = (tokenizeStrings ? EntityKey.LookupSingletonName(keyValuePair3.Key) : keyValuePair3.Key);
						compositeKeyValues[num3] = keyValuePair3.Value;
						num3++;
					}
				}
			}
			IL_1C3:
			return num2 > 0;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005AE4 File Offset: 0x00003CE4
		private static void GetKeyValues(EntitySet entitySet, IExtendedDataRecord record, out string[] keyNames, out object singletonKeyValue, out object[] compositeKeyValues)
		{
			singletonKeyValue = null;
			compositeKeyValues = null;
			int count = entitySet.ElementType.KeyMembers.Count;
			keyNames = entitySet.ElementType.KeyMemberNames;
			EntityType entityType = record.DataRecordInfo.RecordType.EdmType as EntityType;
			if (count == 1)
			{
				EdmMember edmMember = entityType.KeyMembers[0];
				singletonKeyValue = record[edmMember.Name];
				if (EntityUtil.IsNull(singletonKeyValue))
				{
					throw EntityUtil.NoNullsAllowedInKeyValuePairs("record");
				}
			}
			else
			{
				compositeKeyValues = new object[count];
				for (int i = 0; i < count; i++)
				{
					EdmMember edmMember2 = entityType.KeyMembers[i];
					compositeKeyValues[i] = record[edmMember2.Name];
					if (EntityUtil.IsNull(compositeKeyValues[i]))
					{
						throw EntityUtil.NoNullsAllowedInKeyValuePairs("record");
					}
				}
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005BAB File Offset: 0x00003DAB
		internal void ValidateEntityKey(MetadataWorkspace workspace, EntitySet entitySet)
		{
			this.ValidateEntityKey(workspace, entitySet, false, null);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005BB8 File Offset: 0x00003DB8
		internal void ValidateEntityKey(MetadataWorkspace workspace, EntitySet entitySet, bool isArgumentException, string argumentName)
		{
			if (entitySet != null)
			{
				ReadOnlyMetadataCollection<EdmMember> keyMembers = entitySet.ElementType.KeyMembers;
				if (this._singletonKeyValue != null)
				{
					if (keyMembers.Count != 1)
					{
						if (isArgumentException)
						{
							throw EntityUtil.IncorrectNumberOfKeyValuePairs(argumentName, entitySet.ElementType.FullName, keyMembers.Count, 1);
						}
						throw EntityUtil.IncorrectNumberOfKeyValuePairsInvalidOperation(entitySet.ElementType.FullName, keyMembers.Count, 1);
					}
					else
					{
						EntityKey.ValidateTypeOfKeyValue(workspace, keyMembers[0], this._singletonKeyValue, isArgumentException, argumentName);
						if (this._keyNames[0] != keyMembers[0].Name)
						{
							if (isArgumentException)
							{
								throw EntityUtil.MissingKeyValue(argumentName, keyMembers[0].Name, entitySet.ElementType.FullName);
							}
							throw EntityUtil.MissingKeyValueInvalidOperation(keyMembers[0].Name, entitySet.ElementType.FullName);
						}
					}
				}
				else if (this._compositeKeyValues != null)
				{
					if (keyMembers.Count != this._compositeKeyValues.Length)
					{
						if (isArgumentException)
						{
							throw EntityUtil.IncorrectNumberOfKeyValuePairs(argumentName, entitySet.ElementType.FullName, keyMembers.Count, this._compositeKeyValues.Length);
						}
						throw EntityUtil.IncorrectNumberOfKeyValuePairsInvalidOperation(entitySet.ElementType.FullName, keyMembers.Count, this._compositeKeyValues.Length);
					}
					else
					{
						int i = 0;
						while (i < this._compositeKeyValues.Length)
						{
							EdmMember edmMember = entitySet.ElementType.KeyMembers[i];
							bool flag = false;
							for (int j = 0; j < this._compositeKeyValues.Length; j++)
							{
								if (edmMember.Name == this._keyNames[j])
								{
									EntityKey.ValidateTypeOfKeyValue(workspace, edmMember, this._compositeKeyValues[j], isArgumentException, argumentName);
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								if (isArgumentException)
								{
									throw EntityUtil.MissingKeyValue(argumentName, edmMember.Name, entitySet.ElementType.FullName);
								}
								throw EntityUtil.MissingKeyValueInvalidOperation(edmMember.Name, entitySet.ElementType.FullName);
							}
							else
							{
								i++;
							}
						}
					}
				}
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005D98 File Offset: 0x00003F98
		private static void ValidateTypeOfKeyValue(MetadataWorkspace workspace, EdmMember keyMember, object keyValue, bool isArgumentException, string argumentName)
		{
			EdmType edmType = keyMember.TypeUsage.EdmType;
			EnumType enumType;
			if (Helper.IsPrimitiveType(edmType))
			{
				Type clrEquivalentType = ((PrimitiveType)edmType).ClrEquivalentType;
				if (clrEquivalentType != keyValue.GetType())
				{
					if (isArgumentException)
					{
						throw EntityUtil.IncorrectValueType(argumentName, keyMember.Name, clrEquivalentType.FullName, keyValue.GetType().FullName);
					}
					throw EntityUtil.IncorrectValueTypeInvalidOperation(keyMember.Name, clrEquivalentType.FullName, keyValue.GetType().FullName);
				}
			}
			else if (workspace.TryGetObjectSpaceType((EnumType)edmType, out enumType))
			{
				Type clrType = ((ClrEnumType)enumType).ClrType;
				if (clrType != keyValue.GetType())
				{
					if (isArgumentException)
					{
						throw EntityUtil.IncorrectValueType(argumentName, keyMember.Name, clrType.FullName, keyValue.GetType().FullName);
					}
					throw EntityUtil.IncorrectValueTypeInvalidOperation(keyMember.Name, clrType.FullName, keyValue.GetType().FullName);
				}
			}
			else
			{
				if (isArgumentException)
				{
					throw EntityUtil.NoCorrespondingOSpaceTypeForEnumKeyField(argumentName, keyMember.Name, edmType.FullName);
				}
				throw EntityUtil.NoCorrespondingOSpaceTypeForEnumKeyFieldInvalidOperation(keyMember.Name, edmType.FullName);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005EA8 File Offset: 0x000040A8
		[Conditional("DEBUG")]
		private void AssertCorrectState(EntitySetBase entitySet, bool isTemporary)
		{
			if (this._singletonKeyValue != null)
			{
				return;
			}
			if (this._compositeKeyValues != null)
			{
				for (int i = 0; i < this._compositeKeyValues.Length; i++)
				{
				}
				return;
			}
			bool isTemporary2 = this.IsTemporary;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005EE5 File Offset: 0x000040E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[OnDeserializing]
		public void OnDeserializing(StreamingContext context)
		{
			if (this.RequiresDeserialization)
			{
				this.DeserializeMembers();
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005EF8 File Offset: 0x000040F8
		[OnDeserialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public void OnDeserialized(StreamingContext context)
		{
			Dictionary<string, string> nameLookup = EntityKey._nameLookup;
			lock (nameLookup)
			{
				this._entitySetName = EntityKey.LookupSingletonName(this._entitySetName);
				this._entityContainerName = EntityKey.LookupSingletonName(this._entityContainerName);
				if (this._keyNames != null)
				{
					for (int i = 0; i < this._keyNames.Length; i++)
					{
						this._keyNames[i] = EntityKey.LookupSingletonName(this._keyNames[i]);
					}
				}
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005F84 File Offset: 0x00004184
		private static string LookupSingletonName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (EntityKey._nameLookup.ContainsKey(name))
			{
				return EntityKey._nameLookup[name];
			}
			EntityKey._nameLookup.Add(name, name);
			return name;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005FB6 File Offset: 0x000041B6
		private void ValidateWritable(object instance)
		{
			if (this._isLocked || instance != null)
			{
				throw EntityUtil.CannotChangeEntityKey();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00005FC9 File Offset: 0x000041C9
		private bool RequiresDeserialization
		{
			get
			{
				return this._deserializedMembers != null;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00005FD4 File Offset: 0x000041D4
		private void DeserializeMembers()
		{
			if (EntityKey.CheckKeyValues(new EntityKey.KeyValueReader(this._deserializedMembers), true, true, out this._keyNames, out this._singletonKeyValue, out this._compositeKeyValues))
			{
				this._deserializedMembers = null;
			}
		}

		// Token: 0x04000094 RID: 148
		private string _entitySetName;

		// Token: 0x04000095 RID: 149
		private string _entityContainerName;

		// Token: 0x04000096 RID: 150
		private object _singletonKeyValue;

		// Token: 0x04000097 RID: 151
		private object[] _compositeKeyValues;

		// Token: 0x04000098 RID: 152
		private string[] _keyNames;

		// Token: 0x04000099 RID: 153
		private bool _isLocked;

		// Token: 0x0400009A RID: 154
		[NonSerialized]
		private bool _containsByteArray;

		// Token: 0x0400009B RID: 155
		[NonSerialized]
		private EntityKeyMember[] _deserializedMembers;

		// Token: 0x0400009C RID: 156
		[NonSerialized]
		private int _hashCode;

		// Token: 0x0400009D RID: 157
		private const string s_NoEntitySetKey = "NoEntitySetKey.NoEntitySetKey";

		// Token: 0x0400009E RID: 158
		private const string s_EntityNotValidKey = "EntityNotValidKey.EntityNotValidKey";

		// Token: 0x0400009F RID: 159
		public static readonly EntityKey NoEntitySetKey = new EntityKey("NoEntitySetKey.NoEntitySetKey");

		// Token: 0x040000A0 RID: 160
		public static readonly EntityKey EntityNotValidKey = new EntityKey("EntityNotValidKey.EntityNotValidKey");

		// Token: 0x040000A1 RID: 161
		private static Dictionary<string, string> _nameLookup = new Dictionary<string, string>();

		// Token: 0x02000442 RID: 1090
		private class KeyValueReader : IEnumerable<KeyValuePair<string, object>>, IEnumerable
		{
			// Token: 0x06003A35 RID: 14901 RVA: 0x000DE03D File Offset: 0x000DC23D
			public KeyValueReader(IEnumerable<EntityKeyMember> enumerator)
			{
				this._enumerator = enumerator;
			}

			// Token: 0x06003A36 RID: 14902 RVA: 0x000DE04C File Offset: 0x000DC24C
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				foreach (EntityKeyMember entityKeyMember in this._enumerator)
				{
					if (entityKeyMember != null)
					{
						yield return new KeyValuePair<string, object>(entityKeyMember.Key, entityKeyMember.Value);
					}
				}
				IEnumerator<EntityKeyMember> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06003A37 RID: 14903 RVA: 0x000DE05B File Offset: 0x000DC25B
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040018C2 RID: 6338
			private IEnumerable<EntityKeyMember> _enumerator;
		}
	}
}
