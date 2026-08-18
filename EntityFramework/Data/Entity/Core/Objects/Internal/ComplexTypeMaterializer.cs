using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000576 RID: 1398
	internal class ComplexTypeMaterializer
	{
		// Token: 0x060036A0 RID: 13984 RVA: 0x001036B2 File Offset: 0x001018B2
		internal ComplexTypeMaterializer(MetadataWorkspace workspace)
		{
			this._workspace = workspace;
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x001036C4 File Offset: 0x001018C4
		internal object CreateComplex(IExtendedDataRecord record, DataRecordInfo recordInfo, object result)
		{
			ComplexTypeMaterializer.Plan plan = this.GetPlan(recordInfo);
			if (result == null)
			{
				result = plan.ClrType();
			}
			this.SetProperties(record, result, plan.Properties);
			return result;
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x001036F8 File Offset: 0x001018F8
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

		// Token: 0x060036A3 RID: 13987 RVA: 0x001037B3 File Offset: 0x001019B3
		private static object ConvertDBNull(object value)
		{
			if (DBNull.Value == value)
			{
				return null;
			}
			return value;
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x001037C0 File Offset: 0x001019C0
		private object CreateComplexRecursive(object record, object existing)
		{
			if (DBNull.Value == record)
			{
				return existing;
			}
			return this.CreateComplexRecursive((IExtendedDataRecord)record, existing);
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x001037D9 File Offset: 0x001019D9
		private object CreateComplexRecursive(IExtendedDataRecord record, object existing)
		{
			return this.CreateComplex(record, record.DataRecordInfo, existing);
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x001037EC File Offset: 0x001019EC
		private ComplexTypeMaterializer.Plan GetPlan(DataRecordInfo recordInfo)
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
			ObjectTypeMapping objectMapping = Util.GetObjectMapping(recordInfo.RecordType.EdmType, this._workspace);
			this._lastPlanIndex = num;
			array2[num] = new ComplexTypeMaterializer.Plan(recordInfo.RecordType, objectMapping, recordInfo.FieldMetadata);
			return array2[num];
		}

		// Token: 0x040014DD RID: 5341
		private const int MaxPlanCount = 4;

		// Token: 0x040014DE RID: 5342
		private readonly MetadataWorkspace _workspace;

		// Token: 0x040014DF RID: 5343
		private ComplexTypeMaterializer.Plan[] _lastPlans;

		// Token: 0x040014E0 RID: 5344
		private int _lastPlanIndex;

		// Token: 0x02000577 RID: 1399
		private sealed class Plan
		{
			// Token: 0x060036A7 RID: 13991 RVA: 0x00103888 File Offset: 0x00101A88
			internal Plan(TypeUsage key, ObjectTypeMapping mapping, ReadOnlyCollection<FieldMetadata> fields)
			{
				this.Key = key;
				this.ClrType = DelegateFactory.GetConstructorDelegateForType((ClrComplexType)mapping.ClrType);
				this.Properties = new ComplexTypeMaterializer.PlanEdmProperty[fields.Count];
				for (int i = 0; i < this.Properties.Length; i++)
				{
					FieldMetadata fieldMetadata = fields[i];
					int ordinal = fieldMetadata.Ordinal;
					this.Properties[i] = new ComplexTypeMaterializer.PlanEdmProperty(ordinal, mapping.GetPropertyMap(fieldMetadata.FieldType.Name).ClrProperty);
				}
			}

			// Token: 0x040014E1 RID: 5345
			internal readonly TypeUsage Key;

			// Token: 0x040014E2 RID: 5346
			internal readonly Func<object> ClrType;

			// Token: 0x040014E3 RID: 5347
			internal readonly ComplexTypeMaterializer.PlanEdmProperty[] Properties;
		}

		// Token: 0x02000578 RID: 1400
		private struct PlanEdmProperty
		{
			// Token: 0x060036A8 RID: 13992 RVA: 0x0010391C File Offset: 0x00101B1C
			internal PlanEdmProperty(int ordinal, EdmProperty property)
			{
				this.Ordinal = ordinal;
				this.GetExistingComplex = (Helper.IsComplexType(property.TypeUsage.EdmType) ? DelegateFactory.GetGetterDelegateForProperty(property) : null);
				this.ClrProperty = DelegateFactory.GetSetterDelegateForProperty(property);
			}

			// Token: 0x040014E4 RID: 5348
			internal readonly int Ordinal;

			// Token: 0x040014E5 RID: 5349
			internal readonly Func<object, object> GetExistingComplex;

			// Token: 0x040014E6 RID: 5350
			internal readonly Action<object, object> ClrProperty;
		}
	}
}
