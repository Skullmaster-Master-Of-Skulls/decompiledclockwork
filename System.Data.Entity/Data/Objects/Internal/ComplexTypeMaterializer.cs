using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.Internal.Materialization;
using System.Data.Mapping;
using System.Data.Metadata.Edm;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200015E RID: 350
	internal class ComplexTypeMaterializer
	{
		// Token: 0x06001A41 RID: 6721 RVA: 0x00059C96 File Offset: 0x00057E96
		internal ComplexTypeMaterializer(MetadataWorkspace workspace)
		{
			this._workspace = workspace;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00059CA8 File Offset: 0x00057EA8
		internal object CreateComplex(IExtendedDataRecord record, DataRecordInfo recordInfo, object result)
		{
			ComplexTypeMaterializer.Plan plan = this.GetPlan(record, recordInfo);
			if (result == null)
			{
				result = ((Func<object>)plan.ClrType)();
			}
			this.SetProperties(record, result, plan.Properties);
			return result;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00059CE4 File Offset: 0x00057EE4
		private void SetProperties(IExtendedDataRecord record, object result, ComplexTypeMaterializer.PlanEdmProperty[] properties)
		{
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].GetExistingComplex != null)
				{
					object obj = properties[i].GetExistingComplex(result);
					object arg = this.CreateComplexRecursive(record.GetValue(properties[i].Ordinal), obj);
					if (obj == null)
					{
						properties[i].ClrProperty(result, arg);
					}
				}
				else
				{
					properties[i].ClrProperty(result, ComplexTypeMaterializer.ConvertDBNull(record.GetValue(properties[i].Ordinal)));
				}
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00059D81 File Offset: 0x00057F81
		private static object ConvertDBNull(object value)
		{
			if (DBNull.Value == value)
			{
				return null;
			}
			return value;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00059D8E File Offset: 0x00057F8E
		private object CreateComplexRecursive(object record, object existing)
		{
			if (DBNull.Value == record)
			{
				return existing;
			}
			return this.CreateComplexRecursive((IExtendedDataRecord)record, existing);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00059DA7 File Offset: 0x00057FA7
		private object CreateComplexRecursive(IExtendedDataRecord record, object existing)
		{
			return this.CreateComplex(record, record.DataRecordInfo, existing);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00059DB8 File Offset: 0x00057FB8
		private ComplexTypeMaterializer.Plan GetPlan(IExtendedDataRecord record, DataRecordInfo recordInfo)
		{
			ComplexTypeMaterializer.Plan[] array;
			if ((array = this._lastPlans) == null)
			{
				array = (this._lastPlans = new ComplexTypeMaterializer.Plan[4]);
			}
			ComplexTypeMaterializer.Plan[] array2 = array;
			int num = this._lastPlanIndex - 1;
			for (int i = 0; i < 4; i++)
			{
				num = (num + 1) % 4;
				if (array2[num] == null)
				{
					break;
				}
				if (array2[num].Key == recordInfo.RecordType)
				{
					this._lastPlanIndex = num;
					return array2[num];
				}
			}
			ObjectTypeMapping objectMapping = System.Data.Common.Internal.Materialization.Util.GetObjectMapping(recordInfo.RecordType.EdmType, this._workspace);
			this._lastPlanIndex = num;
			array2[num] = new ComplexTypeMaterializer.Plan(recordInfo.RecordType, objectMapping, recordInfo.FieldMetadata);
			return array2[num];
		}

		// Token: 0x04000AF6 RID: 2806
		private readonly MetadataWorkspace _workspace;

		// Token: 0x04000AF7 RID: 2807
		private const int MaxPlanCount = 4;

		// Token: 0x04000AF8 RID: 2808
		private ComplexTypeMaterializer.Plan[] _lastPlans;

		// Token: 0x04000AF9 RID: 2809
		private int _lastPlanIndex;

		// Token: 0x020004B1 RID: 1201
		private sealed class Plan
		{
			// Token: 0x06003C78 RID: 15480 RVA: 0x000E31C4 File Offset: 0x000E13C4
			internal Plan(TypeUsage key, ObjectTypeMapping mapping, ReadOnlyCollection<FieldMetadata> fields)
			{
				this.Key = key;
				this.ClrType = LightweightCodeGenerator.GetConstructorDelegateForType((ClrComplexType)mapping.ClrType);
				this.Properties = new ComplexTypeMaterializer.PlanEdmProperty[fields.Count];
				for (int i = 0; i < this.Properties.Length; i++)
				{
					FieldMetadata fieldMetadata = fields[i];
					int ordinal = fieldMetadata.Ordinal;
					this.Properties[i] = new ComplexTypeMaterializer.PlanEdmProperty(ordinal, mapping.GetPropertyMap(fieldMetadata.FieldType.Name).ClrProperty);
				}
			}

			// Token: 0x04001A62 RID: 6754
			internal readonly TypeUsage Key;

			// Token: 0x04001A63 RID: 6755
			internal readonly Delegate ClrType;

			// Token: 0x04001A64 RID: 6756
			internal readonly ComplexTypeMaterializer.PlanEdmProperty[] Properties;
		}

		// Token: 0x020004B2 RID: 1202
		private struct PlanEdmProperty
		{
			// Token: 0x06003C79 RID: 15481 RVA: 0x000E3253 File Offset: 0x000E1453
			internal PlanEdmProperty(int ordinal, EdmProperty property)
			{
				this.Ordinal = ordinal;
				this.GetExistingComplex = (Helper.IsComplexType(property.TypeUsage.EdmType) ? LightweightCodeGenerator.GetGetterDelegateForProperty(property) : null);
				this.ClrProperty = LightweightCodeGenerator.GetSetterDelegateForProperty(property);
			}

			// Token: 0x04001A65 RID: 6757
			internal readonly int Ordinal;

			// Token: 0x04001A66 RID: 6758
			internal readonly Func<object, object> GetExistingComplex;

			// Token: 0x04001A67 RID: 6759
			internal readonly Action<object, object> ClrProperty;
		}
	}
}
