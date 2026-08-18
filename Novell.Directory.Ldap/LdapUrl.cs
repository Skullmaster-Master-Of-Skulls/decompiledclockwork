using System;
using System.Collections;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200004B RID: 75
	public class LdapUrl : ICloneable
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000EA64 File Offset: 0x0000DA64
		private void InitBlock()
		{
			this.scope = LdapUrl.DEFAULT_SCOPE;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000EA7C File Offset: 0x0000DA7C
		public virtual string[] AttributeArray
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000EA94 File Offset: 0x0000DA94
		public virtual IEnumerator Attributes
		{
			get
			{
				return new ArrayEnumeration(this.attrs);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000EAB0 File Offset: 0x0000DAB0
		public virtual string[] Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000EAC8 File Offset: 0x0000DAC8
		public virtual string Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000EAE0 File Offset: 0x0000DAE0
		public virtual string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000EAF8 File Offset: 0x0000DAF8
		public virtual int Port
		{
			get
			{
				int result;
				if (this.port == 0)
				{
					result = 389;
				}
				else
				{
					result = this.port;
				}
				return result;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000EB20 File Offset: 0x0000DB20
		public virtual int Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000EB38 File Offset: 0x0000DB38
		public virtual bool Secure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000EB50 File Offset: 0x0000DB50
		public LdapUrl(string url)
		{
			this.InitBlock();
			this.parseURL(url);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000EBAC File Offset: 0x0000DBAC
		public LdapUrl(string host, int port, string dn)
		{
			this.InitBlock();
			this.host = host;
			this.port = port;
			this.dn = dn;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000EC14 File Offset: 0x0000DC14
		public LdapUrl(string host, int port, string dn, string[] attrNames, int scope, string filter, string[] extensions)
		{
			this.InitBlock();
			this.host = host;
			this.port = port;
			this.dn = dn;
			this.attrs = new string[attrNames.Length];
			attrNames.CopyTo(this.attrs, 0);
			this.scope = scope;
			this.filter = filter;
			this.extensions = new string[extensions.Length];
			extensions.CopyTo(this.extensions, 0);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000ECC8 File Offset: 0x0000DCC8
		public LdapUrl(string host, int port, string dn, string[] attrNames, int scope, string filter, string[] extensions, bool secure)
		{
			this.InitBlock();
			this.host = host;
			this.port = port;
			this.dn = dn;
			this.attrs = attrNames;
			this.scope = scope;
			this.filter = filter;
			this.extensions = new string[extensions.Length];
			extensions.CopyTo(this.extensions, 0);
			this.secure = secure;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000ED70 File Offset: 0x0000DD70
		public object Clone()
		{
			object result;
			try
			{
				result = base.MemberwiseClone();
			}
			catch (Exception ex)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return result;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000EDB0 File Offset: 0x0000DDB0
		public static string decode(string URLEncoded)
		{
			int num = 0;
			int i = URLEncoded.IndexOf("%", num);
			if (i >= 0)
			{
				int num2 = 0;
				int length = URLEncoded.Length;
				StringBuilder stringBuilder = new StringBuilder(length);
				while (i <= length - 3)
				{
					if (i < 0)
					{
						i = length;
					}
					stringBuilder.Append(URLEncoded.Substring(num2, i - num2));
					i++;
					if (i < length)
					{
						num2 = i + 2;
						try
						{
							stringBuilder.Append((char)Convert.ToInt32(URLEncoded.Substring(i, num2 - i), 16));
						}
						catch (FormatException ex)
						{
							throw new UriFormatException("LdapUrl.decode: error converting hex characters to integer \"" + ex.Message + "\"");
						}
						num = num2;
						if (num != length)
						{
							i = URLEncoded.IndexOf("%", num);
							continue;
						}
					}
					return stringBuilder.ToString();
				}
				throw new UriFormatException("LdapUrl.decode: must be two hex characters following escape character '%'");
			}
			return URLEncoded;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000EEA4 File Offset: 0x0000DEA4
		public static string encode(string toEncode)
		{
			StringBuilder stringBuilder = new StringBuilder(toEncode.Length);
			foreach (char c in toEncode)
			{
				if (c <= '\u001f' || c == '\u007f' || (c >= '\u0080' && c <= 'ÿ') || c == '<' || c == '>' || c == '"' || c == '#' || c == '%' || c == '{' || c == '}' || c == '|' || c == '\\' || c == '^' || c == '~' || c == '[' || c == '\'' || c == ';' || c == '/' || c == '?' || c == ':' || c == '@' || c == '=' || c == '&')
				{
					string text = Convert.ToString((int)c, 16);
					if (text.Length == 1)
					{
						stringBuilder.Append("%0" + text);
					}
					else
					{
						stringBuilder.Append("%" + Convert.ToString((int)c, 16));
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000EFB0 File Offset: 0x0000DFB0
		public virtual string getDN()
		{
			return this.dn;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000EFC8 File Offset: 0x0000DFC8
		internal virtual void setDN(string dn)
		{
			this.dn = dn;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000EFE0 File Offset: 0x0000DFE0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (this.secure)
			{
				stringBuilder.Append("ldaps://");
			}
			else
			{
				stringBuilder.Append("ldap://");
			}
			if (this.ipV6)
			{
				stringBuilder.Append("[" + this.host + "]");
			}
			else
			{
				stringBuilder.Append(this.host);
			}
			if (this.port != 0)
			{
				stringBuilder.Append(":" + this.port);
			}
			string result;
			if (this.dn == null && this.attrs == null && this.scope == LdapUrl.DEFAULT_SCOPE && this.filter == null && this.extensions == null)
			{
				result = stringBuilder.ToString();
			}
			else
			{
				stringBuilder.Append("/");
				if (this.dn != null)
				{
					stringBuilder.Append(this.dn);
				}
				if (this.attrs == null && this.scope == LdapUrl.DEFAULT_SCOPE && this.filter == null && this.extensions == null)
				{
					result = stringBuilder.ToString();
				}
				else
				{
					stringBuilder.Append("?");
					if (this.attrs != null)
					{
						for (int i = 0; i < this.attrs.Length; i++)
						{
							stringBuilder.Append(this.attrs[i]);
							if (i < this.attrs.Length - 1)
							{
								stringBuilder.Append(",");
							}
						}
					}
					if (this.scope == LdapUrl.DEFAULT_SCOPE && this.filter == null && this.extensions == null)
					{
						result = stringBuilder.ToString();
					}
					else
					{
						stringBuilder.Append("?");
						if (this.scope != LdapUrl.DEFAULT_SCOPE)
						{
							if (this.scope == 1)
							{
								stringBuilder.Append("one");
							}
							else
							{
								stringBuilder.Append("sub");
							}
						}
						if (this.filter == null && this.extensions == null)
						{
							result = stringBuilder.ToString();
						}
						else
						{
							if (this.filter == null)
							{
								stringBuilder.Append("?");
							}
							else
							{
								stringBuilder.Append("?" + this.Filter);
							}
							if (this.extensions == null)
							{
								result = stringBuilder.ToString();
							}
							else
							{
								stringBuilder.Append("?");
								if (this.extensions != null)
								{
									for (int j = 0; j < this.extensions.Length; j++)
									{
										stringBuilder.Append(this.extensions[j]);
										if (j < this.extensions.Length - 1)
										{
											stringBuilder.Append(",");
										}
									}
								}
								result = stringBuilder.ToString();
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000F264 File Offset: 0x0000E264
		private string[] parseList(string listStr, char delimiter, int listStart, int listEnd)
		{
			string[] result;
			if (listEnd - listStart < 1)
			{
				result = null;
			}
			else
			{
				int i = listStart;
				int num = 0;
				while (i > 0)
				{
					num++;
					int num2 = listStr.IndexOf(delimiter, i);
					if (num2 <= 0 || num2 >= listEnd)
					{
						break;
					}
					i = num2 + 1;
				}
				i = listStart;
				string[] array = new string[num];
				num = 0;
				while (i > 0)
				{
					int num2 = listStr.IndexOf(delimiter, i);
					if (i > listEnd)
					{
						break;
					}
					if (num2 < 0)
					{
						num2 = listEnd;
					}
					if (num2 > listEnd)
					{
						num2 = listEnd;
					}
					array[num] = listStr.Substring(i, num2 - i);
					i = num2 + 1;
					num++;
				}
				result = array;
			}
			return result;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000F2F4 File Offset: 0x0000E2F4
		private void parseURL(string url)
		{
			int num = 0;
			int num2 = url.Length;
			if (url == null)
			{
				throw new UriFormatException("LdapUrl: URL cannot be null");
			}
			if (url[num] == '<')
			{
				if (url[num2 - 1] != '>')
				{
					throw new UriFormatException("LdapUrl: URL bad enclosure");
				}
				num++;
				num2--;
			}
			if (url.Substring(num, num + 4 - num).ToUpper().Equals("URL:".ToUpper()))
			{
				num += 4;
			}
			if (url.Substring(num, num + 7 - num).ToUpper().Equals("ldap://".ToUpper()))
			{
				num += 7;
				this.port = 389;
			}
			else
			{
				if (!url.Substring(num, num + 8 - num).ToUpper().Equals("ldaps://".ToUpper()))
				{
					throw new UriFormatException("LdapUrl: URL scheme is not ldap");
				}
				this.secure = true;
				num += 8;
				this.port = 636;
			}
			int num3 = url.IndexOf("/", num);
			int num4 = num2;
			bool flag = false;
			if (num3 < 0)
			{
				num3 = url.IndexOf("?", num);
				if (num3 > 0)
				{
					if (url[num3 + 1] == '?')
					{
						num4 = num3;
						num3++;
						flag = true;
					}
					else
					{
						num3 = -1;
					}
				}
			}
			else
			{
				num4 = num3;
			}
			if (url[num] == '[')
			{
				int num5 = url.IndexOf(']', num + 1);
				if (num5 >= num4 || num5 == -1)
				{
					throw new UriFormatException("LdapUrl: \"]\" is missing on IPV6 host name");
				}
				this.host = url.Substring(num + 1, num5 - (num + 1));
				int num6 = url.IndexOf(":", num5);
				if (num6 < num4 && num6 != -1)
				{
					this.port = int.Parse(url.Substring(num6 + 1, num4 - (num6 + 1)));
				}
			}
			else
			{
				int num6 = url.IndexOf(":", num);
				if (num6 < 0 || num6 > num4)
				{
					this.host = url.Substring(num, num4 - num);
				}
				else
				{
					this.host = url.Substring(num, num6 - num);
					this.port = int.Parse(url.Substring(num6 + 1, num4 - (num6 + 1)));
				}
			}
			num = num4 + 1;
			if (num < num2 && num3 >= 0)
			{
				num = num3 + 1;
				int num7 = url.IndexOf('?', num);
				if (num7 < 0)
				{
					this.dn = url.Substring(num, num2 - num);
				}
				else
				{
					this.dn = url.Substring(num, num7 - num);
				}
				num = num7 + 1;
				if (num < num2 && num7 >= 0 && !flag)
				{
					int num8 = url.IndexOf('?', num);
					if (num8 < 0)
					{
						num8 = num2 - 1;
					}
					this.attrs = this.parseList(url, ',', num7 + 1, num8);
					num = num8 + 1;
					if (num < num2)
					{
						int num9 = url.IndexOf('?', num);
						string text;
						if (num9 < 0)
						{
							text = url.Substring(num, num2 - num);
						}
						else
						{
							text = url.Substring(num, num9 - num);
						}
						if (text.ToUpper().Equals("".ToUpper()))
						{
							this.scope = 0;
						}
						else if (text.ToUpper().Equals("base".ToUpper()))
						{
							this.scope = 0;
						}
						else if (text.ToUpper().Equals("one".ToUpper()))
						{
							this.scope = 1;
						}
						else
						{
							if (!text.ToUpper().Equals("sub".ToUpper()))
							{
								throw new UriFormatException("LdapUrl: URL invalid scope");
							}
							this.scope = 2;
						}
						num = num9 + 1;
						if (num < num2 && num9 >= 0)
						{
							num = num9 + 1;
							int num10 = url.IndexOf('?', num);
							string text2;
							if (num10 < 0)
							{
								text2 = url.Substring(num, num2 - num);
							}
							else
							{
								text2 = url.Substring(num, num10 - num);
							}
							if (!text2.Equals(""))
							{
								this.filter = text2;
							}
							num = num10 + 1;
							if (num < num2 && num10 >= 0)
							{
								int num11 = url.IndexOf('?', num);
								if (num11 > 0)
								{
									throw new UriFormatException("LdapUrl: URL has too many ? fields");
								}
								this.extensions = this.parseList(url, ',', num, num2);
							}
						}
					}
				}
			}
		}

		// Token: 0x0400015F RID: 351
		private static readonly int DEFAULT_SCOPE = 0;

		// Token: 0x04000160 RID: 352
		private bool secure = false;

		// Token: 0x04000161 RID: 353
		private bool ipV6 = false;

		// Token: 0x04000162 RID: 354
		private string host = null;

		// Token: 0x04000163 RID: 355
		private int port = 0;

		// Token: 0x04000164 RID: 356
		private string dn = null;

		// Token: 0x04000165 RID: 357
		private string[] attrs = null;

		// Token: 0x04000166 RID: 358
		private string filter = null;

		// Token: 0x04000167 RID: 359
		private int scope;

		// Token: 0x04000168 RID: 360
		private string[] extensions = null;
	}
}
