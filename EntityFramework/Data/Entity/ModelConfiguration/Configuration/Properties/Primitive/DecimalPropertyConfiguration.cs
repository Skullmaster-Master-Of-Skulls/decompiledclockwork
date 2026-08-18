using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x020007DB RID: 2011
	internal class DecimalPropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06005B7F RID: 23423 RVA: 0x00189831 File Offset: 0x00187A31
		// (set) Token: 0x06005B80 RID: 23424 RVA: 0x00189839 File Offset: 0x00187A39
		public byte? Precision { get; set; }

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06005B81 RID: 23425 RVA: 0x00189842 File Offset: 0x00187A42
		// (set) Token: 0x06005B82 RID: 23426 RVA: 0x0018984A File Offset: 0x00187A4A
		public byte? Scale { get; set; }

		// Token: 0x06005B83 RID: 23427 RVA: 0x00189853 File Offset: 0x00187A53
		public DecimalPropertyConfiguration()
		{
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x0018985B File Offset: 0x00187A5B
		private DecimalPropertyConfiguration(DecimalPropertyConfiguration source) : base(source)
		{
			this.Precision = source.Precision;
			this.Scale = source.Scale;
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x0018987C File Offset: 0x00187A7C
		internal override PrimitivePropertyConfiguration Clone()
		{
			return new DecimalPropertyConfiguration(this);
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x00189884 File Offset: 0x00187A84
		protected override void ConfigureProperty(EdmProperty property)
		{
			base.ConfigureProperty(property);
			byte? precision = this.Precision;
			int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
			if (num != null)
			{
				property.Precision = this.Precision;
			}
			byte? scale = this.Scale;
			int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
			if (num2 != null)
			{
				property.Scale = this.Scale;
			}
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x00189914 File Offset: 0x00187B14
		internal override void Configure(EdmProperty column, FacetDescription facetDescription)
		{
			base.Configure(column, facetDescription);
			string facetName;
			if ((facetName = facetDescription.FacetName) != null)
			{
				if (facetName == "Precision")
				{
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
					return;
				}
				if (!(facetName == "Scale"))
				{
					return;
				}
				byte? scale2;
				if (!facetDescription.IsConstant)
				{
					byte? scale = this.Scale;
					scale2 = ((scale != null) ? new byte?(scale.GetValueOrDefault()) : column.Scale);
				}
				else
				{
					scale2 = null;
				}
				column.Scale = scale2;
			}
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x001899D0 File Offset: 0x00187BD0
		internal override void CopyFrom(PrimitivePropertyConfiguration other)
		{
			base.CopyFrom(other);
			DecimalPropertyConfiguration decimalPropertyConfiguration = other as DecimalPropertyConfiguration;
			if (decimalPropertyConfiguration != null)
			{
				this.Precision = decimalPropertyConfiguration.Precision;
				this.Scale = decimalPropertyConfiguration.Scale;
			}
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x00189A08 File Offset: 0x00187C08
		internal override void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.FillFrom(other, inCSpace);
			DecimalPropertyConfiguration decimalPropertyConfiguration = other as DecimalPropertyConfiguration;
			if (decimalPropertyConfiguration != null)
			{
				byte? precision = this.Precision;
				int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
				if (num == null)
				{
					this.Precision = decimalPropertyConfiguration.Precision;
				}
				byte? scale = this.Scale;
				int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
				if (num2 == null)
				{
					this.Scale = decimalPropertyConfiguration.Scale;
				}
			}
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x00189AA4 File Offset: 0x00187CA4
		internal override void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.MakeCompatibleWith(other, inCSpace);
			DecimalPropertyConfiguration decimalPropertyConfiguration = other as DecimalPropertyConfiguration;
			if (decimalPropertyConfiguration == null)
			{
				return;
			}
			byte? precision = decimalPropertyConfiguration.Precision;
			int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
			if (num != null)
			{
				this.Precision = null;
			}
			byte? scale = decimalPropertyConfiguration.Scale;
			int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
			if (num2 != null)
			{
				this.Scale = null;
			}
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x00189B4C File Offset: 0x00187D4C
		internal override bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			DecimalPropertyConfiguration decimalPropertyConfiguration = other as DecimalPropertyConfiguration;
			bool flag = base.IsCompatible(other, inCSpace, out errorMessage);
			bool flag2 = decimalPropertyConfiguration == null || base.IsCompatible<byte, DecimalPropertyConfiguration>((DecimalPropertyConfiguration c) => c.Precision, decimalPropertyConfiguration, ref errorMessage);
			bool flag3 = decimalPropertyConfiguration == null || base.IsCompatible<byte, DecimalPropertyConfiguration>((DecimalPropertyConfiguration c) => c.Scale, decimalPropertyConfiguration, ref errorMessage);
			return flag && flag2 && flag3;
		}
	}
}
