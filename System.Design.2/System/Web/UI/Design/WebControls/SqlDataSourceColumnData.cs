using System;
using System.Collections.Specialized;
using System.ComponentModel.Design.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Globalization;
using System.Text;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200011E RID: 286
	internal sealed class SqlDataSourceColumnData
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x00042CB6 File Offset: 0x00040EB6
		public SqlDataSourceColumnData(DesignerDataConnection connection, DesignerDataColumn column) : this(connection, column, null)
		{
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00042CC1 File Offset: 0x00040EC1
		public SqlDataSourceColumnData(DesignerDataConnection connection, DesignerDataColumn column, StringCollection usedNames)
		{
			this._connection = connection;
			this._column = column;
			this._usedNames = usedNames;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00042CDE File Offset: 0x00040EDE
		public string AliasedName
		{
			get
			{
				if (this._cachedAliasedName == null)
				{
					this._cachedAliasedName = this.CreateAliasedName();
				}
				return this._cachedAliasedName;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00042CFA File Offset: 0x00040EFA
		public DesignerDataColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00042D04 File Offset: 0x00040F04
		public string SelectName
		{
			get
			{
				if (this._column == null)
				{
					return this.EscapedName;
				}
				string aliasedName = this.AliasedName;
				if (aliasedName != this._column.Name)
				{
					return this.EscapedName + " AS " + this.AliasedName;
				}
				return this.EscapedName;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00042D57 File Offset: 0x00040F57
		public string EscapedName
		{
			get
			{
				if (this._cachedEscapedName == null)
				{
					this._cachedEscapedName = this.CreateEscapedName();
				}
				return this._cachedEscapedName;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00042D73 File Offset: 0x00040F73
		public string ParameterPlaceholder
		{
			get
			{
				if (this._cachedParameterPlaceholder == null)
				{
					this._cachedParameterPlaceholder = this.CreateParameterPlaceholder(null);
				}
				return this._cachedParameterPlaceholder;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00042D90 File Offset: 0x00040F90
		public string WebParameterName
		{
			get
			{
				if (this._cachedWebParameterName == null)
				{
					this._cachedWebParameterName = this.CreateWebParameterName(null);
				}
				return this._cachedWebParameterName;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00042DB0 File Offset: 0x00040FB0
		internal static string EscapeObjectName(DesignerDataConnection connection, string objectName)
		{
			string text = "[";
			string text2 = "]";
			string result;
			try
			{
				DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(connection.ProviderName);
				DbCommandBuilder dbCommandBuilder = dbProviderFactory.CreateCommandBuilder();
				if (dbProviderFactory == OracleClientFactory.Instance)
				{
					text2 = (text = "\"");
				}
				dbCommandBuilder.QuotePrefix = text;
				dbCommandBuilder.QuoteSuffix = text2;
				result = dbCommandBuilder.QuoteIdentifier(objectName);
			}
			catch (Exception ex)
			{
				result = text + objectName + text2;
			}
			return result;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00042E28 File Offset: 0x00041028
		private string CreateAliasedName()
		{
			string name = this._column.Name;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			foreach (char c in name)
			{
				if (char.IsWhiteSpace(c) || c == '_')
				{
					if (!flag2)
					{
						stringBuilder.Append('_');
						flag2 = true;
					}
				}
				else
				{
					if (!char.IsLetterOrDigit(c))
					{
						flag = true;
						break;
					}
					stringBuilder.Append(c);
					flag2 = false;
				}
			}
			if (stringBuilder.Length == 0 || !char.IsLetter(stringBuilder[0]))
			{
				flag = true;
			}
			string text2;
			int num;
			string text3;
			if (flag)
			{
				text2 = "column";
				num = 1;
				text3 = text2 + "1";
			}
			else
			{
				num = 2;
				text2 = stringBuilder.ToString();
				text3 = text2;
			}
			if (this._usedNames != null)
			{
				if (this._usedNames.Contains(text3))
				{
					do
					{
						text3 = text2 + num.ToString(CultureInfo.InvariantCulture);
						num++;
					}
					while (this._usedNames.Contains(text3));
				}
				this._usedNames.Add(text3);
			}
			return text3;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00042F40 File Offset: 0x00041140
		private string CreateEscapedName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._column == null)
			{
				stringBuilder.Append("*");
			}
			else
			{
				stringBuilder.Append(SqlDataSourceColumnData.EscapeObjectName(this._connection, this._column.Name));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00042F8C File Offset: 0x0004118C
		private string CreateParameterPlaceholder(string oldValueFormatString)
		{
			DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(this._connection.ProviderName);
			string parameterPlaceholderPrefix = SqlDataSourceDesigner.GetParameterPlaceholderPrefix(dbProviderFactory);
			string text = parameterPlaceholderPrefix;
			if (SqlDataSourceDesigner.SupportsNamedParameters(dbProviderFactory))
			{
				if (oldValueFormatString == null)
				{
					text += this.AliasedName;
				}
				else
				{
					text += string.Format(CultureInfo.InvariantCulture, oldValueFormatString, new object[]
					{
						this.AliasedName
					});
				}
			}
			return text;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00042FEF File Offset: 0x000411EF
		private string CreateWebParameterName(string oldValueFormatString)
		{
			if (oldValueFormatString == null)
			{
				return this.AliasedName;
			}
			return string.Format(CultureInfo.InvariantCulture, oldValueFormatString, new object[]
			{
				this.AliasedName
			});
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00043015 File Offset: 0x00041215
		public string GetOldValueParameterPlaceHolder(string oldValueFormatString)
		{
			return this.CreateParameterPlaceholder(oldValueFormatString);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0004301E File Offset: 0x0004121E
		public string GetOldValueWebParameterName(string oldValueFormatString)
		{
			return this.CreateWebParameterName(oldValueFormatString);
		}

		// Token: 0x0400064A RID: 1610
		private DesignerDataConnection _connection;

		// Token: 0x0400064B RID: 1611
		private DesignerDataColumn _column;

		// Token: 0x0400064C RID: 1612
		private StringCollection _usedNames;

		// Token: 0x0400064D RID: 1613
		private string _cachedAliasedName;

		// Token: 0x0400064E RID: 1614
		private string _cachedEscapedName;

		// Token: 0x0400064F RID: 1615
		private string _cachedParameterPlaceholder;

		// Token: 0x04000650 RID: 1616
		private string _cachedWebParameterName;
	}
}
