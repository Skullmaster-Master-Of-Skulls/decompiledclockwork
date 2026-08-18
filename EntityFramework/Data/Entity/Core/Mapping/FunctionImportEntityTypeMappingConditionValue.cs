using System;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Xml.XPath;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003B3 RID: 947
	public sealed class FunctionImportEntityTypeMappingConditionValue : FunctionImportEntityTypeMappingCondition
	{
		// Token: 0x06002273 RID: 8819 RVA: 0x000A0D08 File Offset: 0x0009EF08
		public FunctionImportEntityTypeMappingConditionValue(string columnName, object value) : base(Check.NotNull<string>(columnName, "columnName"), LineInfo.Empty)
		{
			Check.NotNull<object>(value, "value");
			this._value = value;
			this._convertedValues = new Memoizer<Type, object>(new Func<Type, object>(this.GetConditionValue), null);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000A0D56 File Offset: 0x0009EF56
		internal FunctionImportEntityTypeMappingConditionValue(string columnName, XPathNavigator columnValue, LineInfo lineInfo) : base(columnName, lineInfo)
		{
			this._xPathValue = columnValue;
			this._convertedValues = new Memoizer<Type, object>(new Func<Type, object>(this.GetConditionValue), null);
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x000A0D7F File Offset: 0x0009EF7F
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x000A0D87 File Offset: 0x0009EF87
		internal override ValueCondition ConditionValue
		{
			get
			{
				return new ValueCondition((this._value != null) ? this._value.ToString() : this._xPathValue.Value);
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000A0DB0 File Offset: 0x0009EFB0
		internal override bool ColumnValueMatchesCondition(object columnValue)
		{
			if (columnValue == null || Convert.IsDBNull(columnValue))
			{
				return false;
			}
			Type type = columnValue.GetType();
			object y = this._convertedValues.Evaluate(type);
			return ByValueEqualityComparer.Default.Equals(columnValue, y);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000A0E3C File Offset: 0x0009F03C
		private object GetConditionValue(Type columnValueType)
		{
			return this.GetConditionValue(columnValueType, delegate()
			{
				throw new EntityCommandExecutionException(Strings.Mapping_FunctionImport_UnsupportedType(this.ColumnName, columnValueType.FullName));
			}, delegate()
			{
				throw new EntityCommandExecutionException(Strings.Mapping_FunctionImport_ConditionValueTypeMismatch("FunctionImportMapping", this.ColumnName, columnValueType.FullName));
			});
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000A0E84 File Offset: 0x0009F084
		internal object GetConditionValue(Type columnValueType, Action handleTypeNotComparable, Action handleInvalidConditionValue)
		{
			PrimitiveType primitiveType;
			if (!ClrProviderManifest.Instance.TryGetPrimitiveType(columnValueType, out primitiveType) || !MappingItemLoader.IsTypeSupportedForCondition(primitiveType.PrimitiveTypeKind))
			{
				handleTypeNotComparable();
				return null;
			}
			if (this._value == null)
			{
				object result;
				try
				{
					result = this._xPathValue.ValueAs(columnValueType);
				}
				catch (FormatException)
				{
					handleInvalidConditionValue();
					result = null;
				}
				return result;
			}
			if (this._value.GetType() == columnValueType)
			{
				return this._value;
			}
			handleInvalidConditionValue();
			return null;
		}

		// Token: 0x04000C26 RID: 3110
		private readonly object _value;

		// Token: 0x04000C27 RID: 3111
		private readonly XPathNavigator _xPathValue;

		// Token: 0x04000C28 RID: 3112
		private readonly Memoizer<Type, object> _convertedValues;
	}
}
