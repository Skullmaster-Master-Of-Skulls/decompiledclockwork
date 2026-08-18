using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation
{
	// Token: 0x020007C3 RID: 1987
	internal class ForeignKeyConstraintConfiguration : ConstraintConfiguration
	{
		// Token: 0x06005A34 RID: 23092 RVA: 0x001852A2 File Offset: 0x001834A2
		public ForeignKeyConstraintConfiguration()
		{
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x001852B5 File Offset: 0x001834B5
		internal ForeignKeyConstraintConfiguration(IEnumerable<PropertyInfo> dependentProperties)
		{
			this._dependentProperties.AddRange(dependentProperties);
			this._isFullySpecified = true;
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x001852DB File Offset: 0x001834DB
		private ForeignKeyConstraintConfiguration(ForeignKeyConstraintConfiguration source)
		{
			this._dependentProperties.AddRange(source._dependentProperties);
			this._isFullySpecified = source._isFullySpecified;
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x0018530B File Offset: 0x0018350B
		internal override ConstraintConfiguration Clone()
		{
			return new ForeignKeyConstraintConfiguration(this);
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06005A38 RID: 23096 RVA: 0x00185313 File Offset: 0x00183513
		public override bool IsFullySpecified
		{
			get
			{
				return this._isFullySpecified;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06005A39 RID: 23097 RVA: 0x0018531B File Offset: 0x0018351B
		internal IEnumerable<PropertyInfo> ToProperties
		{
			get
			{
				return this._dependentProperties;
			}
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x00185323 File Offset: 0x00183523
		public void AddColumn(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			if (!this._dependentProperties.ContainsSame(propertyInfo))
			{
				this._dependentProperties.Add(propertyInfo);
			}
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x00185560 File Offset: 0x00183760
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		internal override void Configure(AssociationType associationType, AssociationEndMember dependentEnd, EntityTypeConfiguration entityTypeConfiguration)
		{
			if (!this._dependentProperties.Any<PropertyInfo>())
			{
				return;
			}
			IEnumerable<PropertyInfo> enumerable = this._dependentProperties.AsEnumerable<PropertyInfo>();
			if (!this.IsFullySpecified)
			{
				if (dependentEnd.GetEntityType().GetClrType() != entityTypeConfiguration.ClrType)
				{
					return;
				}
				var source = from p in this._dependentProperties
				select new
				{
					PropertyInfo = p,
					ColumnOrder = entityTypeConfiguration.Property(new PropertyPath(p), null).ColumnOrder
				};
				if (this._dependentProperties.Count > 1)
				{
					if (source.Any(p => p.ColumnOrder == null))
					{
						ReadOnlyMetadataCollection<EdmProperty> dependentKeys = dependentEnd.GetEntityType().KeyProperties;
						if (dependentKeys.Count == this._dependentProperties.Count && source.All(fk => dependentKeys.Any((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(fk.PropertyInfo))))
						{
							enumerable = from p in dependentKeys
							select p.GetClrPropertyInfo();
							goto IL_17E;
						}
						throw Error.ForeignKeyAttributeConvention_OrderRequired(entityTypeConfiguration.ClrType);
					}
				}
				enumerable = from p in source
				orderby p.ColumnOrder
				select p.PropertyInfo;
			}
			IL_17E:
			List<EdmProperty> list = new List<EdmProperty>();
			foreach (PropertyInfo propertyInfo in enumerable)
			{
				EdmProperty declaredPrimitiveProperty = dependentEnd.GetEntityType().GetDeclaredPrimitiveProperty(propertyInfo);
				if (declaredPrimitiveProperty == null)
				{
					throw Error.ForeignKeyPropertyNotFound(propertyInfo.Name, dependentEnd.GetEntityType().Name);
				}
				list.Add(declaredPrimitiveProperty);
			}
			AssociationEndMember otherEnd = associationType.GetOtherEnd(dependentEnd);
			ReferentialConstraint referentialConstraint = new ReferentialConstraint(otherEnd, dependentEnd, otherEnd.GetEntityType().KeyProperties, list);
			if (otherEnd.IsRequired())
			{
				referentialConstraint.ToProperties.Each((EdmProperty p) => p.Nullable = false);
			}
			associationType.Constraint = referentialConstraint;
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x001857C4 File Offset: 0x001839C4
		public bool Equals(ForeignKeyConstraintConfiguration other)
		{
			if (object.ReferenceEquals(null, other))
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			return other.ToProperties.SequenceEqual(this.ToProperties, new DynamicEqualityComparer<PropertyInfo>((PropertyInfo p1, PropertyInfo p2) => p1.IsSameAs(p2)));
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x0018581A File Offset: 0x00183A1A
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != typeof(ForeignKeyConstraintConfiguration)) && this.Equals((ForeignKeyConstraintConfiguration)obj)));
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x00185861 File Offset: 0x00183A61
		public override int GetHashCode()
		{
			return this.ToProperties.Aggregate(0, (int t, PropertyInfo p) => t + p.GetHashCode());
		}

		// Token: 0x04002407 RID: 9223
		private readonly List<PropertyInfo> _dependentProperties = new List<PropertyInfo>();

		// Token: 0x04002408 RID: 9224
		private readonly bool _isFullySpecified;
	}
}
