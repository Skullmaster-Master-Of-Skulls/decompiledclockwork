using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007BF RID: 1983
	public class StringColumnConfiguration : LengthColumnConfiguration
	{
		// Token: 0x060059B5 RID: 22965 RVA: 0x00183246 File Offset: 0x00181446
		internal StringColumnConfiguration(StringPropertyConfiguration configuration) : base(configuration)
		{
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x060059B6 RID: 22966 RVA: 0x0018324F File Offset: 0x0018144F
		internal new StringPropertyConfiguration Configuration
		{
			get
			{
				return (StringPropertyConfiguration)base.Configuration;
			}
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x0018325C File Offset: 0x0018145C
		public new StringColumnConfiguration IsMaxLength()
		{
			base.IsMaxLength();
			return this;
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x00183266 File Offset: 0x00181466
		public new StringColumnConfiguration HasMaxLength(int? value)
		{
			base.HasMaxLength(value);
			return this;
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x00183271 File Offset: 0x00181471
		public new StringColumnConfiguration IsFixedLength()
		{
			base.IsFixedLength();
			return this;
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x0018327B File Offset: 0x0018147B
		public new StringColumnConfiguration IsVariableLength()
		{
			base.IsVariableLength();
			return this;
		}

		// Token: 0x060059BB RID: 22971 RVA: 0x00183285 File Offset: 0x00181485
		public new StringColumnConfiguration IsOptional()
		{
			base.IsOptional();
			return this;
		}

		// Token: 0x060059BC RID: 22972 RVA: 0x0018328F File Offset: 0x0018148F
		public new StringColumnConfiguration IsRequired()
		{
			base.IsRequired();
			return this;
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x00183299 File Offset: 0x00181499
		public new StringColumnConfiguration HasColumnType(string columnType)
		{
			base.HasColumnType(columnType);
			return this;
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x001832A4 File Offset: 0x001814A4
		public new StringColumnConfiguration HasColumnOrder(int? columnOrder)
		{
			base.HasColumnOrder(columnOrder);
			return this;
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x001832AF File Offset: 0x001814AF
		public StringColumnConfiguration IsUnicode()
		{
			this.IsUnicode(new bool?(true));
			return this;
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x001832BF File Offset: 0x001814BF
		public StringColumnConfiguration IsUnicode(bool? unicode)
		{
			this.Configuration.IsUnicode = unicode;
			return this;
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x001832CE File Offset: 0x001814CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x001832D6 File Offset: 0x001814D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060059C3 RID: 22979 RVA: 0x001832DF File Offset: 0x001814DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060059C4 RID: 22980 RVA: 0x001832E7 File Offset: 0x001814E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
