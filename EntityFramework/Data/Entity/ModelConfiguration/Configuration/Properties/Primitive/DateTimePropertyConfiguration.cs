using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x020007DA RID: 2010
	internal class DateTimePropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06005B74 RID: 23412 RVA: 0x001895DD File Offset: 0x001877DD
		// (set) Token: 0x06005B75 RID: 23413 RVA: 0x001895E5 File Offset: 0x001877E5
		public byte? Precision { get; set; }

		// Token: 0x06005B76 RID: 23414 RVA: 0x001895EE File Offset: 0x001877EE
		public DateTimePropertyConfiguration()
		{
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x001895F6 File Offset: 0x001877F6
		private DateTimePropertyConfiguration(DateTimePropertyConfiguration source) : base(source)
		{
			this.Precision = source.Precision;
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x0018960B File Offset: 0x0018780B
		internal override PrimitivePropertyConfiguration Clone()
		{
			return new DateTimePropertyConfiguration(this);
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x00189614 File Offset: 0x00187814
		protected override void ConfigureProperty(EdmProperty property)
		{
			base.ConfigureProperty(property);
			byte? precision = this.Precision;
			int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
			if (num != null)
			{
				property.Precision = this.Precision;
			}
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x00189668 File Offset: 0x00187868
		internal override void Configure(EdmProperty column, FacetDescription facetDescription)
		{
			base.Configure(column, facetDescription);
			string facetName;
			if ((facetName = facetDescription.FacetName) != null)
			{
				if (!(facetName == "Precision"))
				{
					return;
				}
				byte? precision2;
				if (!facetDescription.IsConstant)
				{
					byte? precision = this.Precision;
					precision2 = ((precision != null) ? new byte?(precision.GetValueOrDefault()) : column.Precision);
				}
				else
				{
					precision2 = null;
				}
				column.Precision = precision2;
			}
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x001896D4 File Offset: 0x001878D4
		internal override void CopyFrom(PrimitivePropertyConfiguration other)
		{
			base.CopyFrom(other);
			DateTimePropertyConfiguration dateTimePropertyConfiguration = other as DateTimePropertyConfiguration;
			if (dateTimePropertyConfiguration != null)
			{
				this.Precision = dateTimePropertyConfiguration.Precision;
			}
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x00189700 File Offset: 0x00187900
		internal override void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.FillFrom(other, inCSpace);
			DateTimePropertyConfiguration dateTimePropertyConfiguration = other as DateTimePropertyConfiguration;
			if (dateTimePropertyConfiguration != null)
			{
				byte? precision = this.Precision;
				int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
				if (num == null)
				{
					this.Precision = dateTimePropertyConfiguration.Precision;
				}
			}
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x0018975C File Offset: 0x0018795C
		internal override void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.MakeCompatibleWith(other, inCSpace);
			DateTimePropertyConfiguration dateTimePropertyConfiguration = other as DateTimePropertyConfiguration;
			if (dateTimePropertyConfiguration == null)
			{
				return;
			}
			byte? precision = dateTimePropertyConfiguration.Precision;
			int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
			if (num != null)
			{
				this.Precision = null;
			}
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x001897C0 File Offset: 0x001879C0
		internal override bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			DateTimePropertyConfiguration dateTimePropertyConfiguration = other as DateTimePropertyConfiguration;
			bool flag = base.IsCompatible(other, inCSpace, out errorMessage);
			bool flag2 = dateTimePropertyConfiguration == null || base.IsCompatible<byte, DateTimePropertyConfiguration>((DateTimePropertyConfiguration c) => c.Precision, dateTimePropertyConfiguration, ref errorMessage);
			return flag && flag2;
		}
	}
}
