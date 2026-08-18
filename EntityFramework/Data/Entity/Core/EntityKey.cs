using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System.Data.Entity.Core
{
	// Token: 0x0200034C RID: 844
	[DataContract(IsReference = true)]
	[DebuggerDisplay("{ConcatKeyValue()}")]
	[Serializable]
	public sealed class EntityKey : IEquatable<EntityKey>
	{
		// Token: 0x06001E04 RID: 7684 RVA: 0x00090978 File Offset: 0x0008EB78
		public EntityKey()
		{
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00090980 File Offset: 0x0008EB80
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public EntityKey(string qualifiedEntitySetName, IEnumerable<KeyValuePair<string, object>> entityKeyValues)
		{
			Check.NotEmpty(qualifiedEntitySetName, "qualifiedEntitySetName");
			Check.NotNull<IEnumerable<KeyValuePair<string, object>>>(entityKeyValues, "entityKeyValues");
			this.InitializeEntitySetName(qualifiedEntitySetName);
			this.InitializeKeyValues(entityKeyValues, false, false);
			this._isLocked = true;
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x000909B8 File Offset: 0x0008EBB8
		public EntityKey(string qualifiedEntitySetName, IEnumerable<EntityKeyMember> entityKeyValues)
		{
			Check.NotEmpty(qualifiedEntitySetName, "qualifiedEntitySetName");
			Check.NotNull<IEnumerable<EntityKeyMember>>(entityKeyValues, "entityKeyValues");
			this.InitializeEntitySetName(qualifiedEntitySetName);
			this.InitializeKeyValues(new EntityKey.KeyValueReader(entityKeyValues), false, false);
			this._isLocked = true;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x000909F8 File Offset: 0x0008EBF8
		public EntityKey(string qualifiedEntitySetName, string keyName, object keyValue)
		{
			Check.NotEmpty(qualifiedEntitySetName, "qualifiedEntitySetName");
			Check.NotEmpty(keyName, "keyName");
			Check.NotNull<object>(keyValue, "keyValue");
			this.InitializeEntitySetName(qualifiedEntitySetName);
			EntityKey.ValidateName(keyName);
			this._keyNames = new string[]
			{
				keyName
			};
			this._singletonKeyValue = keyValue;
			this._isLocked = true;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00090A5C File Offset: 0x0008EC5C
		internal EntityKey(EntitySet entitySet, IExtendedDataRecord record)
		{
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this.InitializeKeyValues(entitySet, record);
			this._isLocked = true;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00090A90 File Offset: 0x0008EC90
		internal EntityKey(string qualifiedEntitySetName)
		{
			this.InitializeEntitySetName(qualifiedEntitySetName);
			this._isLocked = true;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00090AA6 File Offset: 0x0008ECA6
		internal EntityKey(EntitySetBase entitySet)
		{
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this._isLocked = true;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00090AD4 File Offset: 0x0008ECD4
		internal EntityKey(EntitySetBase entitySet, object singletonKeyValue)
		{
			this._singletonKeyValue = singletonKeyValue;
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this._keyNames = entitySet.ElementType.KeyMemberNames;
			this._isLocked = true;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00090B24 File Offset: 0x0008ED24
		internal EntityKey(EntitySetBase entitySet, object[] compositeKeyValues)
		{
			this._compositeKeyValues = compositeKeyValues;
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this._keyNames = entitySet.ElementType.KeyMemberNames;
			this._isLocked = true;
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x00090B73 File Offset: 0x0008ED73
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		public static EntityKey NoEntitySetKey
		{
			get
			{
				return EntityKey._noEntitySetKey;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x00090B7A File Offset: 0x0008ED7A
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		public static EntityKey EntityNotValidKey
		{
			get
			{
				return EntityKey._entityNotValidKey;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00090B81 File Offset: 0x0008ED81
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x00090B89 File Offset: 0x0008ED89
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
				this._entitySetName = EntityKey.LookupSingletonName(value);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00090BA3 File Offset: 0x0008EDA3
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x00090BAB File Offset: 0x0008EDAB
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
				this._entityContainerName = EntityKey.LookupSingletonName(value);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x00090BC8 File Offset: 0x0008EDC8
		// (set) Token: 0x06001E14 RID: 7700 RVA: 0x00090C42 File Offset: 0x0008EE42
		[DataMember]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Required for this feature")]
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
				if (value != null && !this.InitializeKeyValues(new EntityKey.KeyValueReader(value), true, true))
				{
					this._deserializedMembers = value;
				}
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x00090C6A File Offset: 0x0008EE6A
		public bool IsTemporary
		{
			get
			{
				return this.SingletonKeyValue == null && this.CompositeKeyValues == null;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x00090C7F File Offset: 0x0008EE7F
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

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x00090C95 File Offset: 0x0008EE95
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

		// Token: 0x06001E18 RID: 7704 RVA: 0x00090CAC File Offset: 0x0008EEAC
		public EntitySet GetEntitySet(MetadataWorkspace metadataWorkspace)
		{
			Check.NotNull<MetadataWorkspace>(metadataWorkspace, "metadataWorkspace");
			if (string.IsNullOrEmpty(this._entityContainerName) || string.IsNullOrEmpty(this._entitySetName))
			{
				throw new InvalidOperationException(Strings.EntityKey_MissingEntitySetName);
			}
			return metadataWorkspace.GetEntityContainer(this._entityContainerName, DataSpace.CSpace).GetEntitySetByName(this._entitySetName, false);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00090D03 File Offset: 0x0008EF03
		public override bool Equals(object obj)
		{
			return EntityKey.InternalEquals(this, obj as EntityKey, true);
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00090D12 File Offset: 0x0008EF12
		public bool Equals(EntityKey other)
		{
			return EntityKey.InternalEquals(this, other, true);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00090D1C File Offset: 0x0008EF1C
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

		// Token: 0x06001E1C RID: 7708 RVA: 0x00090E00 File Offset: 0x0008F000
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

		// Token: 0x06001E1D RID: 7709 RVA: 0x00090E32 File Offset: 0x0008F032
		public static bool operator ==(EntityKey key1, EntityKey key2)
		{
			return EntityKey.InternalEquals(key1, key2, true);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00090E3C File Offset: 0x0008F03C
		public static bool operator !=(EntityKey key1, EntityKey key2)
		{
			return !EntityKey.InternalEquals(key1, key2, true);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00090E4C File Offset: 0x0008F04C
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal static bool InternalEquals(EntityKey key1, EntityKey key2, bool compareEntitySets)
		{
			if (object.ReferenceEquals(key1, key2))
			{
				return true;
			}
			if (object.ReferenceEquals(key1, null) || object.ReferenceEquals(key2, null))
			{
				return false;
			}
			if (object.ReferenceEquals(EntityKey.NoEntitySetKey, key1) || object.ReferenceEquals(EntityKey.EntityNotValidKey, key1) || object.ReferenceEquals(EntityKey.NoEntitySetKey, key2) || object.ReferenceEquals(EntityKey.EntityNotValidKey, key2))
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

		// Token: 0x06001E20 RID: 7712 RVA: 0x00090FA4 File Offset: 0x0008F1A4
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

		// Token: 0x06001E21 RID: 7713 RVA: 0x00091014 File Offset: 0x0008F214
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

		// Token: 0x06001E22 RID: 7714 RVA: 0x0009105C File Offset: 0x0008F25C
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

		// Token: 0x06001E23 RID: 7715 RVA: 0x000910C8 File Offset: 0x0008F2C8
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

		// Token: 0x06001E24 RID: 7716 RVA: 0x00091108 File Offset: 0x0008F308
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
				throw new ArgumentException(Strings.EntityKey_EntitySetDoesNotMatch(TypeHelpers.GetFullName(entitySet.EntityContainer.Name, entitySet.Name)), "entitySet");
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

		// Token: 0x06001E25 RID: 7717 RVA: 0x00091220 File Offset: 0x0008F420
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

		// Token: 0x06001E26 RID: 7718 RVA: 0x0009129C File Offset: 0x0008F49C
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
			throw new ArgumentOutOfRangeException("keyName");
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000912EC File Offset: 0x0008F4EC
		internal void InitializeEntitySetName(string qualifiedEntitySetName)
		{
			string[] array = qualifiedEntitySetName.Split(new char[]
			{
				'.'
			});
			if (array.Length != 2 || string.IsNullOrWhiteSpace(array[0]) || string.IsNullOrWhiteSpace(array[1]))
			{
				throw new ArgumentException(Strings.EntityKey_InvalidQualifiedEntitySetName, "qualifiedEntitySetName");
			}
			this._entityContainerName = array[0];
			this._entitySetName = array[1];
			EntityKey.ValidateName(this._entityContainerName);
			EntityKey.ValidateName(this._entitySetName);
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0009135F File Offset: 0x0008F55F
		private static void ValidateName(string name)
		{
			if (!name.IsValidUndottedName())
			{
				throw new ArgumentException(Strings.EntityKey_InvalidName(name));
			}
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00091378 File Offset: 0x0008F578
		internal bool InitializeKeyValues(IEnumerable<KeyValuePair<string, object>> entityKeyValues, bool allowNullKeys = false, bool tokenizeStrings = false)
		{
			int num = entityKeyValues.Count<KeyValuePair<string, object>>();
			if (num == 1)
			{
				this._keyNames = new string[1];
				KeyValuePair<string, object> keyValuePair = entityKeyValues.Single<KeyValuePair<string, object>>();
				this.InitializeKeyValue(keyValuePair, 0, tokenizeStrings);
				this._singletonKeyValue = keyValuePair.Value;
			}
			else
			{
				if (num > 1)
				{
					this._keyNames = new string[num];
					this._compositeKeyValues = new object[num];
					int num2 = 0;
					using (IEnumerator<KeyValuePair<string, object>> enumerator = entityKeyValues.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, object> keyValuePair2 = enumerator.Current;
							this.InitializeKeyValue(keyValuePair2, num2, tokenizeStrings);
							this._compositeKeyValues[num2] = keyValuePair2.Value;
							num2++;
						}
						goto IL_AC;
					}
				}
				if (!allowNullKeys)
				{
					throw new ArgumentException(Strings.EntityKey_EntityKeyMustHaveValues, "entityKeyValues");
				}
			}
			IL_AC:
			return num > 0;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00091448 File Offset: 0x0008F648
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		private void InitializeKeyValue(KeyValuePair<string, object> keyValuePair, int i, bool tokenizeStrings)
		{
			if (EntityUtil.IsNull(keyValuePair.Value) || string.IsNullOrWhiteSpace(keyValuePair.Key))
			{
				throw new ArgumentException(Strings.EntityKey_NoNullsAllowedInKeyValuePairs, "entityKeyValues");
			}
			EntityKey.ValidateName(keyValuePair.Key);
			this._keyNames[i] = (tokenizeStrings ? EntityKey.LookupSingletonName(keyValuePair.Key) : keyValuePair.Key);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000914B0 File Offset: 0x0008F6B0
		private void InitializeKeyValues(EntitySet entitySet, IExtendedDataRecord record)
		{
			int count = entitySet.ElementType.KeyMembers.Count;
			this._keyNames = entitySet.ElementType.KeyMemberNames;
			EntityType entityType = (EntityType)record.DataRecordInfo.RecordType.EdmType;
			if (count == 1)
			{
				this._singletonKeyValue = record[entityType.KeyMembers[0].Name];
				if (EntityUtil.IsNull(this._singletonKeyValue))
				{
					throw new ArgumentException(Strings.EntityKey_NoNullsAllowedInKeyValuePairs, "record");
				}
			}
			else
			{
				this._compositeKeyValues = new object[count];
				for (int i = 0; i < count; i++)
				{
					this._compositeKeyValues[i] = record[entityType.KeyMembers[i].Name];
					if (EntityUtil.IsNull(this._compositeKeyValues[i]))
					{
						throw new ArgumentException(Strings.EntityKey_NoNullsAllowedInKeyValuePairs, "record");
					}
				}
			}
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00091589 File Offset: 0x0008F789
		internal void ValidateEntityKey(MetadataWorkspace workspace, EntitySet entitySet)
		{
			this.ValidateEntityKey(workspace, entitySet, false, null);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00091598 File Offset: 0x0008F798
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
							throw new ArgumentException(Strings.EntityKey_IncorrectNumberOfKeyValuePairs(entitySet.ElementType.FullName, keyMembers.Count, 1), argumentName);
						}
						throw new InvalidOperationException(Strings.EntityKey_IncorrectNumberOfKeyValuePairs(entitySet.ElementType.FullName, keyMembers.Count, 1));
					}
					else
					{
						EntityKey.ValidateTypeOfKeyValue(workspace, keyMembers[0], this._singletonKeyValue, isArgumentException, argumentName);
						if (this._keyNames[0] != keyMembers[0].Name)
						{
							if (isArgumentException)
							{
								throw new ArgumentException(Strings.EntityKey_MissingKeyValue(keyMembers[0].Name, entitySet.ElementType.FullName), argumentName);
							}
							throw new InvalidOperationException(Strings.EntityKey_MissingKeyValue(keyMembers[0].Name, entitySet.ElementType.FullName));
						}
					}
				}
				else if (this._compositeKeyValues != null)
				{
					if (keyMembers.Count != this._compositeKeyValues.Length)
					{
						if (isArgumentException)
						{
							throw new ArgumentException(Strings.EntityKey_IncorrectNumberOfKeyValuePairs(entitySet.ElementType.FullName, keyMembers.Count, this._compositeKeyValues.Length), argumentName);
						}
						throw new InvalidOperationException(Strings.EntityKey_IncorrectNumberOfKeyValuePairs(entitySet.ElementType.FullName, keyMembers.Count, this._compositeKeyValues.Length));
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
									throw new ArgumentException(Strings.EntityKey_MissingKeyValue(edmMember.Name, entitySet.ElementType.FullName), argumentName);
								}
								throw new InvalidOperationException(Strings.EntityKey_MissingKeyValue(edmMember.Name, entitySet.ElementType.FullName));
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

		// Token: 0x06001E2E RID: 7726 RVA: 0x000917C8 File Offset: 0x0008F9C8
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
						throw new ArgumentException(Strings.EntityKey_IncorrectValueType(keyMember.Name, clrEquivalentType.FullName, keyValue.GetType().FullName), argumentName);
					}
					throw new InvalidOperationException(Strings.EntityKey_IncorrectValueType(keyMember.Name, clrEquivalentType.FullName, keyValue.GetType().FullName));
				}
			}
			else if (workspace.TryGetObjectSpaceType((EnumType)edmType, out enumType))
			{
				Type clrType = enumType.ClrType;
				if (clrType != keyValue.GetType())
				{
					if (isArgumentException)
					{
						throw new ArgumentException(Strings.EntityKey_IncorrectValueType(keyMember.Name, clrType.FullName, keyValue.GetType().FullName), argumentName);
					}
					throw new InvalidOperationException(Strings.EntityKey_IncorrectValueType(keyMember.Name, clrType.FullName, keyValue.GetType().FullName));
				}
			}
			else
			{
				if (isArgumentException)
				{
					throw new ArgumentException(Strings.EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember(keyMember.Name, edmType.FullName), argumentName);
				}
				throw new InvalidOperationException(Strings.EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember(keyMember.Name, edmType.FullName));
			}
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000918F0 File Offset: 0x0008FAF0
		[Conditional("DEBUG")]
		private void AssertCorrectState(EntitySetBase entitySet, bool isTemporary)
		{
			if (this._singletonKeyValue != null)
			{
				if (entitySet != null)
				{
					return;
				}
			}
			else
			{
				if (this._compositeKeyValues != null)
				{
					for (int i = 0; i < this._compositeKeyValues.Length; i++)
					{
					}
					return;
				}
				bool isTemporary2 = this.IsTemporary;
			}
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0009192E File Offset: 0x0008FB2E
		[SuppressMessage("Microsoft.Usage", "CA2238:ImplementSerializationMethodsCorrectly")]
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

		// Token: 0x06001E31 RID: 7729 RVA: 0x00091940 File Offset: 0x0008FB40
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Usage", "CA2238:ImplementSerializationMethodsCorrectly")]
		[Browsable(false)]
		[OnDeserialized]
		public void OnDeserialized(StreamingContext context)
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

		// Token: 0x06001E32 RID: 7730 RVA: 0x000919A2 File Offset: 0x0008FBA2
		internal static string LookupSingletonName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return EntityKey.NameLookup.GetOrAdd(name, (string n) => n);
			}
			return null;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000919D6 File Offset: 0x0008FBD6
		private void ValidateWritable(object instance)
		{
			if (this._isLocked || instance != null)
			{
				throw new InvalidOperationException(Strings.EntityKey_CannotChangeKey);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x000919EE File Offset: 0x0008FBEE
		private bool RequiresDeserialization
		{
			get
			{
				return this._deserializedMembers != null;
			}
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000919FC File Offset: 0x0008FBFC
		private void DeserializeMembers()
		{
			if (this.InitializeKeyValues(new EntityKey.KeyValueReader(this._deserializedMembers), true, true))
			{
				this._deserializedMembers = null;
			}
		}

		// Token: 0x04000A46 RID: 2630
		private string _entitySetName;

		// Token: 0x04000A47 RID: 2631
		private string _entityContainerName;

		// Token: 0x04000A48 RID: 2632
		private object _singletonKeyValue;

		// Token: 0x04000A49 RID: 2633
		private object[] _compositeKeyValues;

		// Token: 0x04000A4A RID: 2634
		private string[] _keyNames;

		// Token: 0x04000A4B RID: 2635
		private readonly bool _isLocked;

		// Token: 0x04000A4C RID: 2636
		[NonSerialized]
		private bool _containsByteArray;

		// Token: 0x04000A4D RID: 2637
		[NonSerialized]
		private EntityKeyMember[] _deserializedMembers;

		// Token: 0x04000A4E RID: 2638
		[NonSerialized]
		private int _hashCode;

		// Token: 0x04000A4F RID: 2639
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		private static readonly EntityKey _noEntitySetKey = new EntityKey("NoEntitySetKey.NoEntitySetKey");

		// Token: 0x04000A50 RID: 2640
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		private static readonly EntityKey _entityNotValidKey = new EntityKey("EntityNotValidKey.EntityNotValidKey");

		// Token: 0x04000A51 RID: 2641
		private static readonly ConcurrentDictionary<string, string> NameLookup = new ConcurrentDictionary<string, string>();

		// Token: 0x0200034D RID: 845
		private class KeyValueReader : IEnumerable<KeyValuePair<string, object>>, IEnumerable
		{
			// Token: 0x06001E38 RID: 7736 RVA: 0x00091A44 File Offset: 0x0008FC44
			public KeyValueReader(IEnumerable<EntityKeyMember> enumerator)
			{
				this._enumerator = enumerator;
			}

			// Token: 0x06001E39 RID: 7737 RVA: 0x00091BA8 File Offset: 0x0008FDA8
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				foreach (EntityKeyMember pair in this._enumerator)
				{
					if (pair != null)
					{
						yield return new KeyValuePair<string, object>(pair.Key, pair.Value);
					}
				}
				yield break;
			}

			// Token: 0x06001E3A RID: 7738 RVA: 0x00091BC4 File Offset: 0x0008FDC4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000A53 RID: 2643
			private readonly IEnumerable<EntityKeyMember> _enumerator;
		}
	}
}
