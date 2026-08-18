using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Globalization;
using System.Reflection;

namespace System.Data.Odbc
{
	// Token: 0x02000296 RID: 662
	[TypeConverter(typeof(OdbcConnectionStringBuilder.OdbcConnectionStringBuilderConverter))]
	[DefaultProperty("Driver")]
	public sealed class OdbcConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x06002840 RID: 10304 RVA: 0x0010CF2C File Offset: 0x0010C32C
		static OdbcConnectionStringBuilder()
		{
			string[] array = new string[]
			{
				null,
				"Driver"
			};
			array[0] = "Dsn";
			OdbcConnectionStringBuilder._validKeywords = array;
			OdbcConnectionStringBuilder._keywords = new Dictionary<string, OdbcConnectionStringBuilder.Keywords>(2, StringComparer.OrdinalIgnoreCase)
			{
				{
					"Driver",
					OdbcConnectionStringBuilder.Keywords.Driver
				},
				{
					"Dsn",
					OdbcConnectionStringBuilder.Keywords.Dsn
				}
			};
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0010CF80 File Offset: 0x0010C380
		public OdbcConnectionStringBuilder() : this(null)
		{
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0010CF94 File Offset: 0x0010C394
		public OdbcConnectionStringBuilder(string connectionString) : base(true)
		{
			if (!ADP.IsEmpty(connectionString))
			{
				base.ConnectionString = connectionString;
			}
		}

		// Token: 0x1700068B RID: 1675
		public override object this[string keyword]
		{
			get
			{
				ADP.CheckArgumentNull(keyword, "keyword");
				OdbcConnectionStringBuilder.Keywords index;
				if (OdbcConnectionStringBuilder._keywords.TryGetValue(keyword, out index))
				{
					return this.GetAt(index);
				}
				return base[keyword];
			}
			set
			{
				ADP.CheckArgumentNull(keyword, "keyword");
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				OdbcConnectionStringBuilder.Keywords keywords;
				if (!OdbcConnectionStringBuilder._keywords.TryGetValue(keyword, out keywords))
				{
					base[keyword] = value;
					base.ClearPropertyDescriptors();
					this._knownKeywords = null;
					return;
				}
				if (keywords == OdbcConnectionStringBuilder.Keywords.Dsn)
				{
					this.Dsn = OdbcConnectionStringBuilder.ConvertToString(value);
					return;
				}
				if (keywords == OdbcConnectionStringBuilder.Keywords.Driver)
				{
					this.Driver = OdbcConnectionStringBuilder.ConvertToString(value);
					return;
				}
				throw ADP.KeywordNotSupported(keyword);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x0010D078 File Offset: 0x0010C478
		// (set) Token: 0x06002846 RID: 10310 RVA: 0x0010D08C File Offset: 0x0010C48C
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbConnectionString_Driver")]
		[ResCategory("DataCategory_Source")]
		[DisplayName("Driver")]
		public string Driver
		{
			get
			{
				return this._driver;
			}
			set
			{
				this.SetValue("Driver", value);
				this._driver = value;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x0010D0AC File Offset: 0x0010C4AC
		// (set) Token: 0x06002848 RID: 10312 RVA: 0x0010D0C0 File Offset: 0x0010C4C0
		[ResCategory("DataCategory_NamedConnectionString")]
		[ResDescription("DbConnectionString_DSN")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Dsn")]
		public string Dsn
		{
			get
			{
				return this._dsn;
			}
			set
			{
				this.SetValue("Dsn", value);
				this._dsn = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x0010D0E0 File Offset: 0x0010C4E0
		public override ICollection Keys
		{
			get
			{
				string[] array = this._knownKeywords;
				if (array == null)
				{
					array = OdbcConnectionStringBuilder._validKeywords;
					int num = 0;
					foreach (object obj in base.Keys)
					{
						string b = (string)obj;
						bool flag = true;
						foreach (string a in array)
						{
							if (a == b)
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							num++;
						}
					}
					if (0 < num)
					{
						string[] array3 = new string[array.Length + num];
						array.CopyTo(array3, 0);
						int num2 = array.Length;
						foreach (object obj2 in base.Keys)
						{
							string text = (string)obj2;
							bool flag2 = true;
							foreach (string a2 in array)
							{
								if (a2 == text)
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								array3[num2++] = text;
							}
						}
						array = array3;
					}
					this._knownKeywords = array;
				}
				return new ReadOnlyCollection<string>(array);
			}
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x0010D250 File Offset: 0x0010C650
		public override void Clear()
		{
			base.Clear();
			for (int i = 0; i < OdbcConnectionStringBuilder._validKeywords.Length; i++)
			{
				this.Reset((OdbcConnectionStringBuilder.Keywords)i);
			}
			this._knownKeywords = OdbcConnectionStringBuilder._validKeywords;
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x0010D288 File Offset: 0x0010C688
		public override bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return OdbcConnectionStringBuilder._keywords.ContainsKey(keyword) || base.ContainsKey(keyword);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0010D2B8 File Offset: 0x0010C6B8
		private static string ConvertToString(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToString(value);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0010D2CC File Offset: 0x0010C6CC
		private object GetAt(OdbcConnectionStringBuilder.Keywords index)
		{
			if (index == OdbcConnectionStringBuilder.Keywords.Dsn)
			{
				return this.Dsn;
			}
			if (index == OdbcConnectionStringBuilder.Keywords.Driver)
			{
				return this.Driver;
			}
			throw ADP.KeywordNotSupported(OdbcConnectionStringBuilder._validKeywords[(int)index]);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x0010D2FC File Offset: 0x0010C6FC
		public override bool Remove(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			if (base.Remove(keyword))
			{
				OdbcConnectionStringBuilder.Keywords index;
				if (OdbcConnectionStringBuilder._keywords.TryGetValue(keyword, out index))
				{
					this.Reset(index);
				}
				else
				{
					base.ClearPropertyDescriptors();
					this._knownKeywords = null;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x0010D348 File Offset: 0x0010C748
		private void Reset(OdbcConnectionStringBuilder.Keywords index)
		{
			if (index == OdbcConnectionStringBuilder.Keywords.Dsn)
			{
				this._dsn = "";
				return;
			}
			if (index == OdbcConnectionStringBuilder.Keywords.Driver)
			{
				this._driver = "";
				return;
			}
			throw ADP.KeywordNotSupported(OdbcConnectionStringBuilder._validKeywords[(int)index]);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x0010D380 File Offset: 0x0010C780
		private void SetValue(string keyword, string value)
		{
			ADP.CheckArgumentNull(value, keyword);
			base[keyword] = value;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x0010D39C File Offset: 0x0010C79C
		public override bool TryGetValue(string keyword, out object value)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			OdbcConnectionStringBuilder.Keywords index;
			if (OdbcConnectionStringBuilder._keywords.TryGetValue(keyword, out index))
			{
				value = this.GetAt(index);
				return true;
			}
			return base.TryGetValue(keyword, out value);
		}

		// Token: 0x04001A80 RID: 6784
		private static readonly string[] _validKeywords;

		// Token: 0x04001A81 RID: 6785
		private static readonly Dictionary<string, OdbcConnectionStringBuilder.Keywords> _keywords;

		// Token: 0x04001A82 RID: 6786
		private string[] _knownKeywords;

		// Token: 0x04001A83 RID: 6787
		private string _dsn = "";

		// Token: 0x04001A84 RID: 6788
		private string _driver = "";

		// Token: 0x0200041E RID: 1054
		private enum Keywords
		{
			// Token: 0x040022BD RID: 8893
			Dsn,
			// Token: 0x040022BE RID: 8894
			Driver
		}

		// Token: 0x0200041F RID: 1055
		internal sealed class OdbcConnectionStringBuilderConverter : ExpandableObjectConverter
		{
			// Token: 0x060035E7 RID: 13799 RVA: 0x00147AB8 File Offset: 0x00146EB8
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x060035E8 RID: 13800 RVA: 0x00147AE4 File Offset: 0x00146EE4
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType)
				{
					OdbcConnectionStringBuilder odbcConnectionStringBuilder = value as OdbcConnectionStringBuilder;
					if (odbcConnectionStringBuilder != null)
					{
						return this.ConvertToInstanceDescriptor(odbcConnectionStringBuilder);
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x060035E9 RID: 13801 RVA: 0x00147B38 File Offset: 0x00146F38
			private InstanceDescriptor ConvertToInstanceDescriptor(OdbcConnectionStringBuilder options)
			{
				Type[] types = new Type[]
				{
					typeof(string)
				};
				object[] arguments = new object[]
				{
					options.ConnectionString
				};
				ConstructorInfo constructor = typeof(OdbcConnectionStringBuilder).GetConstructor(types);
				return new InstanceDescriptor(constructor, arguments);
			}
		}
	}
}
