using System;
using System.Collections.Specialized;
using System.ComponentModel.Design.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Globalization;
using System.Text;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004E4 RID: 1252
	internal sealed class SqlDataSourceColumnData
	{
		// Token: 0x06002CEC RID: 11500 RVA: 0x000FE22A File Offset: 0x000FD22A
		public SqlDataSourceColumnData(DesignerDataConnection connection, DesignerDataColumn column) : this(connection, column, null)
		{
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000FE235 File Offset: 0x000FD235
		public SqlDataSourceColumnData(DesignerDataConnection connection, DesignerDataColumn column, StringCollection usedNames)
		{
			this._connection = connection;
			this._column = column;
			this._usedNames = usedNames;
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x000FE252 File Offset: 0x000FD252
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

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x000FE26E File Offset: 0x000FD26E
		public DesignerDataColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000FE278 File Offset: 0x000FD278
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

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x000FE2CB File Offset: 0x000FD2CB
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

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000FE2E7 File Offset: 0x000FD2E7
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

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000FE304 File Offset: 0x000FD304
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

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000FE324 File Offset: 0x000FD324
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
			catch (Exception)
			{
				result = text + objectName + text2;
			}
			return result;
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000FE398 File Offset: 0x000FD398
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
				text3 = text2 + '1';
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

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000FE4B0 File Offset: 0x000FD4B0
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

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000FE4FC File Offset: 0x000FD4FC
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

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000FE564 File Offset: 0x000FD564
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

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000FE597 File Offset: 0x000FD597
		public string GetOldValueParameterPlaceHolder(string oldValueFormatString)
		{
			return this.CreateParameterPlaceholder(oldValueFormatString);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000FE5A0 File Offset: 0x000FD5A0
		public string GetOldValueWebParameterName(string oldValueFormatString)
		{
			return this.CreateWebParameterName(oldValueFormatString);
		}

		// Token: 0x04001EAC RID: 7852
		private DesignerDataConnection _connection;

		// Token: 0x04001EAD RID: 7853
		private DesignerDataColumn _column;

		// Token: 0x04001EAE RID: 7854
		private StringCollection _usedNames;

		// Token: 0x04001EAF RID: 7855
		private string _cachedAliasedName;

		// Token: 0x04001EB0 RID: 7856
		private string _cachedEscapedName;

		// Token: 0x04001EB1 RID: 7857
		private string _cachedParameterPlaceholder;

		// Token: 0x04001EB2 RID: 7858
		private string _cachedWebParameterName;
	}
}
