using System;

namespace System.Data.Common
{
	// Token: 0x020002DF RID: 735
	public abstract class DbColumn
	{
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x00125BC8 File Offset: 0x00124FC8
		// (set) Token: 0x06002E10 RID: 11792 RVA: 0x00125BDC File Offset: 0x00124FDC
		public bool? AllowDBNull { get; protected set; }

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x00125BF0 File Offset: 0x00124FF0
		// (set) Token: 0x06002E12 RID: 11794 RVA: 0x00125C04 File Offset: 0x00125004
		public string BaseCatalogName { get; protected set; }

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x00125C18 File Offset: 0x00125018
		// (set) Token: 0x06002E14 RID: 11796 RVA: 0x00125C2C File Offset: 0x0012502C
		public string BaseColumnName { get; protected set; }

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x00125C40 File Offset: 0x00125040
		// (set) Token: 0x06002E16 RID: 11798 RVA: 0x00125C54 File Offset: 0x00125054
		public string BaseSchemaName { get; protected set; }

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x00125C68 File Offset: 0x00125068
		// (set) Token: 0x06002E18 RID: 11800 RVA: 0x00125C7C File Offset: 0x0012507C
		public string BaseServerName { get; protected set; }

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x00125C90 File Offset: 0x00125090
		// (set) Token: 0x06002E1A RID: 11802 RVA: 0x00125CA4 File Offset: 0x001250A4
		public string BaseTableName { get; protected set; }

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x00125CB8 File Offset: 0x001250B8
		// (set) Token: 0x06002E1C RID: 11804 RVA: 0x00125CCC File Offset: 0x001250CC
		public string ColumnName { get; protected set; }

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x00125CE0 File Offset: 0x001250E0
		// (set) Token: 0x06002E1E RID: 11806 RVA: 0x00125CF4 File Offset: 0x001250F4
		public int? ColumnOrdinal { get; protected set; }

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x00125D08 File Offset: 0x00125108
		// (set) Token: 0x06002E20 RID: 11808 RVA: 0x00125D1C File Offset: 0x0012511C
		public int? ColumnSize { get; protected set; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x00125D30 File Offset: 0x00125130
		// (set) Token: 0x06002E22 RID: 11810 RVA: 0x00125D44 File Offset: 0x00125144
		public bool? IsAliased { get; protected set; }

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002E23 RID: 11811 RVA: 0x00125D58 File Offset: 0x00125158
		// (set) Token: 0x06002E24 RID: 11812 RVA: 0x00125D6C File Offset: 0x0012516C
		public bool? IsAutoIncrement { get; protected set; }

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x00125D80 File Offset: 0x00125180
		// (set) Token: 0x06002E26 RID: 11814 RVA: 0x00125D94 File Offset: 0x00125194
		public bool? IsExpression { get; protected set; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x00125DA8 File Offset: 0x001251A8
		// (set) Token: 0x06002E28 RID: 11816 RVA: 0x00125DBC File Offset: 0x001251BC
		public bool? IsHidden { get; protected set; }

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x00125DD0 File Offset: 0x001251D0
		// (set) Token: 0x06002E2A RID: 11818 RVA: 0x00125DE4 File Offset: 0x001251E4
		public bool? IsIdentity { get; protected set; }

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x00125DF8 File Offset: 0x001251F8
		// (set) Token: 0x06002E2C RID: 11820 RVA: 0x00125E0C File Offset: 0x0012520C
		public bool? IsKey { get; protected set; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x00125E20 File Offset: 0x00125220
		// (set) Token: 0x06002E2E RID: 11822 RVA: 0x00125E34 File Offset: 0x00125234
		public bool? IsLong { get; protected set; }

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x00125E48 File Offset: 0x00125248
		// (set) Token: 0x06002E30 RID: 11824 RVA: 0x00125E5C File Offset: 0x0012525C
		public bool? IsReadOnly { get; protected set; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x00125E70 File Offset: 0x00125270
		// (set) Token: 0x06002E32 RID: 11826 RVA: 0x00125E84 File Offset: 0x00125284
		public bool? IsUnique { get; protected set; }

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x00125E98 File Offset: 0x00125298
		// (set) Token: 0x06002E34 RID: 11828 RVA: 0x00125EAC File Offset: 0x001252AC
		public int? NumericPrecision { get; protected set; }

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x00125EC0 File Offset: 0x001252C0
		// (set) Token: 0x06002E36 RID: 11830 RVA: 0x00125ED4 File Offset: 0x001252D4
		public int? NumericScale { get; protected set; }

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002E37 RID: 11831 RVA: 0x00125EE8 File Offset: 0x001252E8
		// (set) Token: 0x06002E38 RID: 11832 RVA: 0x00125EFC File Offset: 0x001252FC
		public string UdtAssemblyQualifiedName { get; protected set; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x00125F10 File Offset: 0x00125310
		// (set) Token: 0x06002E3A RID: 11834 RVA: 0x00125F24 File Offset: 0x00125324
		public Type DataType { get; protected set; }

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x00125F38 File Offset: 0x00125338
		// (set) Token: 0x06002E3C RID: 11836 RVA: 0x00125F4C File Offset: 0x0012534C
		public string DataTypeName { get; protected set; }

		// Token: 0x17000777 RID: 1911
		public virtual object this[string property]
		{
			get
			{
				uint num = <PrivateImplementationDetails><System_Data_netmodule>.ComputeStringHash(property);
				if (num <= 2477638934U)
				{
					if (num <= 1067318116U)
					{
						if (num <= 687909556U)
						{
							if (num != 405521230U)
							{
								if (num == 687909556U)
								{
									if (property == "ColumnOrdinal")
									{
										return this.ColumnOrdinal;
									}
								}
							}
							else if (property == "DataTypeName")
							{
								return this.DataTypeName;
							}
						}
						else if (num != 720006947U)
						{
							if (num != 1005639113U)
							{
								if (num == 1067318116U)
								{
									if (property == "ColumnName")
									{
										return this.ColumnName;
									}
								}
							}
							else if (property == "IsHidden")
							{
								return this.IsHidden;
							}
						}
						else if (property == "IsLong")
						{
							return this.IsLong;
						}
					}
					else if (num <= 2215472237U)
					{
						if (num != 1154057342U)
						{
							if (num != 1309233724U)
							{
								if (num == 2215472237U)
								{
									if (property == "DataType")
									{
										return this.DataType;
									}
								}
							}
							else if (property == "IsKey")
							{
								return this.IsKey;
							}
						}
						else if (property == "ColumnSize")
						{
							return this.ColumnSize;
						}
					}
					else if (num != 2239129947U)
					{
						if (num != 2380251540U)
						{
							if (num == 2477638934U)
							{
								if (property == "IsUnique")
								{
									return this.IsUnique;
								}
							}
						}
						else if (property == "NumericPrecision")
						{
							return this.NumericPrecision;
						}
					}
					else if (property == "IsExpression")
					{
						return this.IsExpression;
					}
				}
				else if (num <= 3042527364U)
				{
					if (num <= 2711511624U)
					{
						if (num != 2504653387U)
						{
							if (num != 2586490225U)
							{
								if (num == 2711511624U)
								{
									if (property == "BaseServerName")
									{
										return this.BaseServerName;
									}
								}
							}
							else if (property == "UdtAssemblyQualifiedName")
							{
								return this.UdtAssemblyQualifiedName;
							}
						}
						else if (property == "IsIdentity")
						{
							return this.IsIdentity;
						}
					}
					else if (num != 2741140585U)
					{
						if (num != 2757192823U)
						{
							if (num == 3042527364U)
							{
								if (property == "BaseCatalogName")
								{
									return this.BaseCatalogName;
								}
							}
						}
						else if (property == "BaseTableName")
						{
							return this.BaseTableName;
						}
					}
					else if (property == "BaseColumnName")
					{
						return this.BaseColumnName;
					}
				}
				else if (num <= 3656290791U)
				{
					if (num != 3115085976U)
					{
						if (num != 3173893005U)
						{
							if (num == 3656290791U)
							{
								if (property == "IsReadOnly")
								{
									return this.IsReadOnly;
								}
							}
						}
						else if (property == "AllowDBNull")
						{
							return this.AllowDBNull;
						}
					}
					else if (property == "BaseSchemaName")
					{
						return this.BaseSchemaName;
					}
				}
				else if (num != 3912158903U)
				{
					if (num != 3938522122U)
					{
						if (num == 4233439846U)
						{
							if (property == "IsAliased")
							{
								return this.IsAliased;
							}
						}
					}
					else if (property == "NumericScale")
					{
						return this.NumericScale;
					}
				}
				else if (property == "IsAutoIncrement")
				{
					return this.IsAutoIncrement;
				}
				return null;
			}
		}
	}
}
