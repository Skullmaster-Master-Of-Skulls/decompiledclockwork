using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000802 RID: 2050
	public class PropertyMaxLengthConvention : IConceptualModelConvention<EntityType>, IConceptualModelConvention<ComplexType>, IConceptualModelConvention<AssociationType>, IConvention
	{
		// Token: 0x06005C75 RID: 23669 RVA: 0x0018F356 File Offset: 0x0018D556
		public PropertyMaxLengthConvention() : this(128)
		{
		}

		// Token: 0x06005C76 RID: 23670 RVA: 0x0018F363 File Offset: 0x0018D563
		public PropertyMaxLengthConvention(int length)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", Strings.InvalidMaxLengthSize);
			}
			this._length = length;
		}

		// Token: 0x06005C77 RID: 23671 RVA: 0x0018F386 File Offset: 0x0018D586
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			this.SetLength(item.DeclaredProperties, item.KeyProperties);
		}

		// Token: 0x06005C78 RID: 23672 RVA: 0x0018F3B2 File Offset: 0x0018D5B2
		public virtual void Apply(ComplexType item, DbModel model)
		{
			Check.NotNull<ComplexType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			this.SetLength(item.Properties, new List<EdmProperty>());
		}

		// Token: 0x06005C79 RID: 23673 RVA: 0x0018F3E0 File Offset: 0x0018D5E0
		private void SetLength(IEnumerable<EdmProperty> properties, ICollection<EdmProperty> keyProperties)
		{
			foreach (EdmProperty edmProperty in properties)
			{
				if (edmProperty.IsPrimitiveType)
				{
					if (edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String))
					{
						this.SetStringDefaults(edmProperty, keyProperties.Contains(edmProperty));
					}
					if (edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Binary))
					{
						this.SetBinaryDefaults(edmProperty, keyProperties.Contains(edmProperty));
					}
				}
			}
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x0018F464 File Offset: 0x0018D664
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.Constraint == null)
			{
				return;
			}
			IEnumerable<EdmProperty> source = item.GetOtherEnd(item.Constraint.DependentEnd).GetEntityType().KeyProperties();
			if (source.Count<EdmProperty>() != item.Constraint.ToProperties.Count)
			{
				return;
			}
			for (int i = 0; i < item.Constraint.ToProperties.Count; i++)
			{
				EdmProperty edmProperty = item.Constraint.ToProperties[i];
				EdmProperty edmProperty2 = source.ElementAt(i);
				if (edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String) || edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Binary))
				{
					edmProperty.IsUnicode = edmProperty2.IsUnicode;
					edmProperty.IsFixedLength = edmProperty2.IsFixedLength;
					edmProperty.MaxLength = edmProperty2.MaxLength;
					edmProperty.IsMaxLength = edmProperty2.IsMaxLength;
				}
			}
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x0018F54C File Offset: 0x0018D74C
		private void SetStringDefaults(EdmProperty property, bool isKey)
		{
			if (property.IsUnicode == null)
			{
				property.IsUnicode = new bool?(true);
			}
			this.SetBinaryDefaults(property, isKey);
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x0018F580 File Offset: 0x0018D780
		private void SetBinaryDefaults(EdmProperty property, bool isKey)
		{
			if (property.IsFixedLength == null)
			{
				property.IsFixedLength = new bool?(false);
			}
			if (property.MaxLength == null && !property.IsMaxLength)
			{
				if (isKey || property.IsFixedLength == true)
				{
					property.MaxLength = new int?(this._length);
					return;
				}
				property.IsMaxLength = true;
			}
		}

		// Token: 0x040024AD RID: 9389
		private const int DefaultLength = 128;

		// Token: 0x040024AE RID: 9390
		private readonly int _length;
	}
}
