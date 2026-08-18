using System;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml.XPath;

namespace System.Data.Mapping
{
	// Token: 0x02000227 RID: 551
	internal sealed class FunctionImportEntityTypeMappingConditionValue : FunctionImportEntityTypeMappingCondition
	{
		// Token: 0x060023B5 RID: 9141 RVA: 0x00081371 File Offset: 0x0007F571
		internal FunctionImportEntityTypeMappingConditionValue(string columnName, XPathNavigator columnValue, LineInfo lineInfo) : base(columnName, lineInfo)
		{
			this._xPathValue = EntityUtil.CheckArgumentNull<XPathNavigator>(columnValue, "columnValue");
			this._convertedValues = new Memoizer<Type, object>(new Func<Type, object>(this.GetConditionValue), null);
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x000813A4 File Offset: 0x0007F5A4
		internal override ValueCondition ConditionValue
		{
			get
			{
				return new ValueCondition(this._xPathValue.Value);
			}
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000813B8 File Offset: 0x0007F5B8
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

		// Token: 0x060023B8 RID: 9144 RVA: 0x000813F4 File Offset: 0x0007F5F4
		private object GetConditionValue(Type columnValueType)
		{
			return this.GetConditionValue(columnValueType, delegate()
			{
				throw EntityUtil.CommandExecution(Strings.Mapping_FunctionImport_UnsupportedType(this.ColumnName, columnValueType.FullName));
			}, delegate()
			{
				throw EntityUtil.CommandExecution(Strings.Mapping_FunctionImport_ConditionValueTypeMismatch("FunctionImportMapping", this.ColumnName, columnValueType.FullName));
			});
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x0008143C File Offset: 0x0007F63C
		internal object GetConditionValue(Type columnValueType, Action handleTypeNotComparable, Action handleInvalidConditionValue)
		{
			PrimitiveType primitiveType;
			if (!ClrProviderManifest.Instance.TryGetPrimitiveType(columnValueType, out primitiveType) || !StorageMappingItemLoader.IsTypeSupportedForCondition(primitiveType.PrimitiveTypeKind))
			{
				handleTypeNotComparable();
				return null;
			}
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

		// Token: 0x04000FD4 RID: 4052
		private readonly XPathNavigator _xPathValue;

		// Token: 0x04000FD5 RID: 4053
		private readonly Memoizer<Type, object> _convertedValues;
	}
}
