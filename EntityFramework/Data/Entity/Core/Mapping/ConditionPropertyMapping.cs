using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000010 RID: 16
	public class ConditionPropertyMapping : PropertyMapping
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00004AAC File Offset: 0x00002CAC
		internal ConditionPropertyMapping(EdmProperty propertyOrColumn, object value, bool? isNull)
		{
			DataSpace dataSpace = propertyOrColumn.TypeUsage.EdmType.DataSpace;
			switch (dataSpace)
			{
			case DataSpace.CSpace:
				base.Property = propertyOrColumn;
				break;
			case DataSpace.SSpace:
				this._column = propertyOrColumn;
				break;
			default:
				throw new ArgumentException(Strings.MetadataItem_InvalidDataSpace(dataSpace, typeof(EdmProperty).Name), "propertyOrColumn");
			}
			this._value = value;
			this._isNull = isNull;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004B29 File Offset: 0x00002D29
		internal ConditionPropertyMapping(EdmProperty property, EdmProperty column, object value, bool? isNull) : base(property)
		{
			this._column = column;
			this._value = value;
			this._isNull = isNull;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004B48 File Offset: 0x00002D48
		internal object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004B50 File Offset: 0x00002D50
		internal bool? IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004B58 File Offset: 0x00002D58
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00004B60 File Offset: 0x00002D60
		public override EdmProperty Property
		{
			get
			{
				return base.Property;
			}
			internal set
			{
				base.Property = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004B69 File Offset: 0x00002D69
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004B71 File Offset: 0x00002D71
		public EdmProperty Column
		{
			get
			{
				return this._column;
			}
			internal set
			{
				this._column = value;
			}
		}

		// Token: 0x0400001E RID: 30
		private EdmProperty _column;

		// Token: 0x0400001F RID: 31
		private readonly object _value;

		// Token: 0x04000020 RID: 32
		private readonly bool? _isNull;
	}
}
