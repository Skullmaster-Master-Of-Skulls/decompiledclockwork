using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x020007DD RID: 2013
	internal class StringPropertyConfiguration : LengthPropertyConfiguration
	{
		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06005B8C RID: 23436 RVA: 0x00189C13 File Offset: 0x00187E13
		// (set) Token: 0x06005B8D RID: 23437 RVA: 0x00189C1B File Offset: 0x00187E1B
		public bool? IsUnicode { get; set; }

		// Token: 0x06005B8E RID: 23438 RVA: 0x00189C24 File Offset: 0x00187E24
		public StringPropertyConfiguration()
		{
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x00189C2C File Offset: 0x00187E2C
		private StringPropertyConfiguration(StringPropertyConfiguration source) : base(source)
		{
			this.IsUnicode = source.IsUnicode;
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x00189C41 File Offset: 0x00187E41
		internal override PrimitivePropertyConfiguration Clone()
		{
			return new StringPropertyConfiguration(this);
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x00189C4C File Offset: 0x00187E4C
		protected override void ConfigureProperty(EdmProperty property)
		{
			base.ConfigureProperty(property);
			if (this.IsUnicode != null)
			{
				property.IsUnicode = this.IsUnicode;
			}
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x00189C7C File Offset: 0x00187E7C
		internal override void Configure(EdmProperty column, FacetDescription facetDescription)
		{
			base.Configure(column, facetDescription);
			string facetName;
			if ((facetName = facetDescription.FacetName) != null)
			{
				if (!(facetName == "Unicode"))
				{
					return;
				}
				bool? isUnicode2;
				if (!facetDescription.IsConstant)
				{
					bool? isUnicode = this.IsUnicode;
					isUnicode2 = ((isUnicode != null) ? new bool?(isUnicode.GetValueOrDefault()) : column.IsUnicode);
				}
				else
				{
					isUnicode2 = null;
				}
				column.IsUnicode = isUnicode2;
			}
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x00189CE8 File Offset: 0x00187EE8
		internal override void CopyFrom(PrimitivePropertyConfiguration other)
		{
			base.CopyFrom(other);
			StringPropertyConfiguration stringPropertyConfiguration = other as StringPropertyConfiguration;
			if (stringPropertyConfiguration != null)
			{
				this.IsUnicode = stringPropertyConfiguration.IsUnicode;
			}
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x00189D14 File Offset: 0x00187F14
		internal override void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.FillFrom(other, inCSpace);
			StringPropertyConfiguration stringPropertyConfiguration = other as StringPropertyConfiguration;
			if (stringPropertyConfiguration != null && this.IsUnicode == null)
			{
				this.IsUnicode = stringPropertyConfiguration.IsUnicode;
			}
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x00189D50 File Offset: 0x00187F50
		internal override void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.MakeCompatibleWith(other, inCSpace);
			StringPropertyConfiguration stringPropertyConfiguration = other as StringPropertyConfiguration;
			if (stringPropertyConfiguration == null)
			{
				return;
			}
			if (stringPropertyConfiguration.IsUnicode != null)
			{
				this.IsUnicode = null;
			}
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x00189D90 File Offset: 0x00187F90
		internal override bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			StringPropertyConfiguration stringPropertyConfiguration = other as StringPropertyConfiguration;
			bool flag = base.IsCompatible(other, inCSpace, out errorMessage);
			bool flag2 = stringPropertyConfiguration == null || base.IsCompatible<bool, StringPropertyConfiguration>((StringPropertyConfiguration c) => c.IsUnicode, stringPropertyConfiguration, ref errorMessage);
			return flag && flag2;
		}
	}
}
