using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000061 RID: 97
	[TypeConverter(typeof(ConstraintConverter))]
	[DefaultProperty("ConstraintName")]
	public abstract class Constraint
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x001E7AF8 File Offset: 0x001E6EF8
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x001E7B18 File Offset: 0x001E6F18
		[DefaultValue("")]
		[ResDescription("ConstraintNameDescr")]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x001E7BE8 File Offset: 0x001E6FE8
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x001E7C18 File Offset: 0x001E7018
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x001E7C38 File Offset: 0x001E7038
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x001E7C58 File Offset: 0x001E7058
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

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000467 RID: 1127
		[ResDescription("ConstraintTableDescr")]
		public abstract DataTable Table { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x001E7C88 File Offset: 0x001E7088
		[ResDescription("ExtendedPropertiesDescr")]
		[Browsable(false)]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x06000469 RID: 1129
		internal abstract bool ContainsColumn(DataColumn column);

		// Token: 0x0600046A RID: 1130
		internal abstract bool CanEnableConstraint();

		// Token: 0x0600046B RID: 1131
		internal abstract Constraint Clone(DataSet destination);

		// Token: 0x0600046C RID: 1132
		internal abstract Constraint Clone(DataSet destination, bool ignoreNSforTableLookup);

		// Token: 0x0600046D RID: 1133 RVA: 0x001E7CB8 File Offset: 0x001E70B8
		internal void CheckConstraint()
		{
			if (!this.CanEnableConstraint())
			{
				throw ExceptionBuilder.ConstraintViolation(this.ConstraintName);
			}
		}

		// Token: 0x0600046E RID: 1134
		internal abstract void CheckCanAddToCollection(ConstraintCollection constraint);

		// Token: 0x0600046F RID: 1135
		internal abstract bool CanBeRemovedFromCollection(ConstraintCollection constraint, bool fThrowException);

		// Token: 0x06000470 RID: 1136
		internal abstract void CheckConstraint(DataRow row, DataRowAction action);

		// Token: 0x06000471 RID: 1137
		internal abstract void CheckState();

		// Token: 0x06000472 RID: 1138 RVA: 0x001E7CE8 File Offset: 0x001E70E8
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x001E7D38 File Offset: 0x001E7138
		[CLSCompliant(false)]
		protected virtual DataSet _DataSet
		{
			get
			{
				return this.dataSet;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x001E7D58 File Offset: 0x001E7158
		protected internal void SetDataSet(DataSet dataSet)
		{
			this.dataSet = dataSet;
		}

		// Token: 0x06000475 RID: 1141
		internal abstract bool IsConstraintViolated();

		// Token: 0x06000476 RID: 1142 RVA: 0x001E7D78 File Offset: 0x001E7178
		public override string ToString()
		{
			return this.ConstraintName;
		}

		// Token: 0x040006D3 RID: 1747
		internal string name = "";

		// Token: 0x040006D4 RID: 1748
		private string _schemaName = "";

		// Token: 0x040006D5 RID: 1749
		private bool inCollection;

		// Token: 0x040006D6 RID: 1750
		private DataSet dataSet;

		// Token: 0x040006D7 RID: 1751
		internal PropertyCollection extendedProperties;
	}
}
