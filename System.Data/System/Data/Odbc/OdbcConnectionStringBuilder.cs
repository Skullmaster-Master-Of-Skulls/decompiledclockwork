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
	// Token: 0x020001E1 RID: 481
	[TypeConverter(typeof(OdbcConnectionStringBuilder.OdbcConnectionStringBuilderConverter))]
	[DefaultProperty("Driver")]
	public sealed class OdbcConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x06001AD2 RID: 6866 RVA: 0x0025F598 File Offset: 0x0025E998
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

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0025F5F8 File Offset: 0x0025E9F8
		public OdbcConnectionStringBuilder() : this(null)
		{
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0025F618 File Offset: 0x0025EA18
		public OdbcConnectionStringBuilder(string connectionString) : base(true)
		{
			if (!ADP.IsEmpty(connectionString))
			{
				base.ConnectionString = connectionString;
			}
		}

		// Token: 0x1700038B RID: 907
		public override object this[string keyword]
		{
			get
			{
				Bid.Trace("<comm.OdbcConnectionStringBuilder.get_Item|API> keyword='%ls'\n", keyword);
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
				Bid.Trace("<comm.OdbcConnectionStringBuilder.set_Item|API> keyword='%ls'\n", keyword);
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
				switch (keywords)
				{
				case OdbcConnectionStringBuilder.Keywords.Dsn:
					this.Dsn = OdbcConnectionStringBuilder.ConvertToString(value);
					return;
				case OdbcConnectionStringBuilder.Keywords.Driver:
					this.Driver = OdbcConnectionStringBuilder.ConvertToString(value);
					return;
				default:
					throw ADP.KeywordNotSupported(keyword);
				}
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x0025F738 File Offset: 0x0025EB38
		// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x0025F758 File Offset: 0x0025EB58
		[DisplayName("Driver")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbConnectionString_Driver")]
		[ResCategory("DataCategory_Source")]
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

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x0025F778 File Offset: 0x0025EB78
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x0025F798 File Offset: 0x0025EB98
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_NamedConnectionString")]
		[DisplayName("Dsn")]
		[ResDescription("DbConnectionString_DSN")]
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0025F7B8 File Offset: 0x0025EBB8
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

		// Token: 0x06001ADC RID: 6876 RVA: 0x0025F928 File Offset: 0x0025ED28
		public override void Clear()
		{
			base.Clear();
			for (int i = 0; i < OdbcConnectionStringBuilder._validKeywords.Length; i++)
			{
				this.Reset((OdbcConnectionStringBuilder.Keywords)i);
			}
			this._knownKeywords = OdbcConnectionStringBuilder._validKeywords;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0025F968 File Offset: 0x0025ED68
		public override bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return OdbcConnectionStringBuilder._keywords.ContainsKey(keyword) || base.ContainsKey(keyword);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0025F998 File Offset: 0x0025ED98
		private static string ConvertToString(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToString(value);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0025F9B8 File Offset: 0x0025EDB8
		private object GetAt(OdbcConnectionStringBuilder.Keywords index)
		{
			switch (index)
			{
			case OdbcConnectionStringBuilder.Keywords.Dsn:
				return this.Dsn;
			case OdbcConnectionStringBuilder.Keywords.Driver:
				return this.Driver;
			default:
				throw ADP.KeywordNotSupported(OdbcConnectionStringBuilder._validKeywords[(int)index]);
			}
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0025F9F8 File Offset: 0x0025EDF8
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

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0025FA48 File Offset: 0x0025EE48
		private void Reset(OdbcConnectionStringBuilder.Keywords index)
		{
			switch (index)
			{
			case OdbcConnectionStringBuilder.Keywords.Dsn:
				this._dsn = "";
				return;
			case OdbcConnectionStringBuilder.Keywords.Driver:
				this._driver = "";
				return;
			default:
				throw ADP.KeywordNotSupported(OdbcConnectionStringBuilder._validKeywords[(int)index]);
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0025FA98 File Offset: 0x0025EE98
		private void SetValue(string keyword, string value)
		{
			ADP.CheckArgumentNull(value, keyword);
			base[keyword] = value;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0025FAB8 File Offset: 0x0025EEB8
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

		// Token: 0x04000FD8 RID: 4056
		private static readonly string[] _validKeywords;

		// Token: 0x04000FD9 RID: 4057
		private static readonly Dictionary<string, OdbcConnectionStringBuilder.Keywords> _keywords;

		// Token: 0x04000FDA RID: 4058
		private string[] _knownKeywords;

		// Token: 0x04000FDB RID: 4059
		private string _dsn = "";

		// Token: 0x04000FDC RID: 4060
		private string _driver = "";

		// Token: 0x020001E2 RID: 482
		private enum Keywords
		{
			// Token: 0x04000FDE RID: 4062
			Dsn,
			// Token: 0x04000FDF RID: 4063
			Driver
		}

		// Token: 0x020001E3 RID: 483
		internal sealed class OdbcConnectionStringBuilderConverter : ExpandableObjectConverter
		{
			// Token: 0x06001AE5 RID: 6885 RVA: 0x0025FB18 File Offset: 0x0025EF18
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06001AE6 RID: 6886 RVA: 0x0025FB48 File Offset: 0x0025EF48
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

			// Token: 0x06001AE7 RID: 6887 RVA: 0x0025FB98 File Offset: 0x0025EF98
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
