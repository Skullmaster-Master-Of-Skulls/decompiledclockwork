using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x0200009D RID: 157
	[DefaultProperty("ConstraintName")]
	[TypeConverter(typeof(ConstraintConverter))]
	public abstract class Constraint
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00056A40 File Offset: 0x00055E40
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00056A54 File Offset: 0x00055E54
		[ResDescription("ConstraintNameDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		public virtual string ConstraintName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (ADP.IsEmpty(value) && this.Table != null && this.InCollection)
				{
					throw ExceptionBuilder.NoConstraintName();
				}
				CultureInfo culture = (this.Table != null) ? this.Table.Locale : CultureInfo.CurrentCulture;
				if (string.Compare(this.name, value, true, culture) != 0)
				{
					if (this.Table != null && this.InCollection)
					{
						this.Table.Constraints.RegisterName(value);
						if (this.name.Length != 0)
						{
							this.Table.Constraints.UnregisterName(this.name);
						}
					}
					this.name = value;
					return;
				}
				if (string.Compare(this.name, value, false, culture) != 0)
				{
					this.name = value;
				}
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00056B18 File Offset: 0x00055F18
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00056B40 File Offset: 0x00055F40
		internal string SchemaName
		{
			get
			{
				if (ADP.IsEmpty(this._schemaName))
				{
					return this.ConstraintName;
				}
				return this._schemaName;
			}
			set
			{
				if (!ADP.IsEmpty(value))
				{
					this._schemaName = value;
				}
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00056B5C File Offset: 0x00055F5C
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00056B70 File Offset: 0x00055F70
		internal virtual bool InCollection
		{
			get
			{
				return this.inCollection;
			}
			set
			{
				this.inCollection = value;
				if (value)
				{
					this.dataSet = this.Table.DataSet;
					return;
				}
				this.dataSet = null;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060007E5 RID: 2021
		[ResDescription("ConstraintTableDescr")]
		public abstract DataTable Table { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00056BA0 File Offset: 0x00055FA0
		[Browsable(false)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("ExtendedPropertiesDescr")]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				if (this.extendedProperties == null)
				{
					this.extendedProperties = new PropertyCollection();
				}
				return this.extendedProperties;
			}
		}

		// Token: 0x060007E7 RID: 2023
		internal abstract bool ContainsColumn(DataColumn column);

		// Token: 0x060007E8 RID: 2024
		internal abstract bool CanEnableConstraint();

		// Token: 0x060007E9 RID: 2025
		internal abstract Constraint Clone(DataSet destination);

		// Token: 0x060007EA RID: 2026
		internal abstract Constraint Clone(DataSet destination, bool ignoreNSforTableLookup);

		// Token: 0x060007EB RID: 2027 RVA: 0x00056BC8 File Offset: 0x00055FC8
		internal void CheckConstraint()
		{
			if (!this.CanEnableConstraint())
			{
				throw ExceptionBuilder.ConstraintViolation(this.ConstraintName);
			}
		}

		// Token: 0x060007EC RID: 2028
		internal abstract void CheckCanAddToCollection(ConstraintCollection constraint);

		// Token: 0x060007ED RID: 2029
		internal abstract bool CanBeRemovedFromCollection(ConstraintCollection constraint, bool fThrowException);

		// Token: 0x060007EE RID: 2030
		internal abstract void CheckConstraint(DataRow row, DataRowAction action);

		// Token: 0x060007EF RID: 2031
		internal abstract void CheckState();

		// Token: 0x060007F0 RID: 2032 RVA: 0x00056BEC File Offset: 0x00055FEC
		protected void CheckStateForProperty()
		{
			try
			{
				this.CheckState();
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ExceptionBuilder.BadObjectPropertyAccess(ex.Message);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00056C38 File Offset: 0x00056038
		[CLSCompliant(false)]
		protected virtual DataSet _DataSet
		{
			get
			{
				return this.dataSet;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00056C4C File Offset: 0x0005604C
		protected internal void SetDataSet(DataSet dataSet)
		{
			this.dataSet = dataSet;
		}

		// Token: 0x060007F3 RID: 2035
		internal abstract bool IsConstraintViolated();

		// Token: 0x060007F4 RID: 2036 RVA: 0x00056C60 File Offset: 0x00056060
		public override string ToString()
		{
			return this.ConstraintName;
		}

		// Token: 0x040002DD RID: 733
		internal string name = "";

		// Token: 0x040002DE RID: 734
		private string _schemaName = "";

		// Token: 0x040002DF RID: 735
		private bool inCollection;

		// Token: 0x040002E0 RID: 736
		private DataSet dataSet;

		// Token: 0x040002E1 RID: 737
		internal PropertyCollection extendedProperties;
	}
}
