using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000092 RID: 146
	[DefaultProperty("DataSource")]
	public sealed class OracleConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x00045E6C File Offset: 0x00044E6C
		static OracleConnectionStringBuilder()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
			OracleConnectionStringBuilder.m_boolMapping = new Hashtable(4);
			OracleConnectionStringBuilder.m_boolMapping["true"] = true;
			OracleConnectionStringBuilder.m_boolMapping["false"] = false;
			OracleConnectionStringBuilder.m_boolMapping["yes"] = true;
			OracleConnectionStringBuilder.m_boolMapping["no"] = false;
			OracleConnectionStringBuilder.m_defaultValues = new Hashtable();
			OracleConnectionStringBuilder.m_defaultValues.Add("USER ID", "");
			OracleConnectionStringBuilder.m_defaultValues.Add("PASSWORD", "");
			OracleConnectionStringBuilder.m_defaultValues.Add("PROXY USER ID", "");
			OracleConnectionStringBuilder.m_defaultValues.Add("PROXY PASSWORD", "");
			OracleConnectionStringBuilder.m_defaultValues.Add("DATA SOURCE", "");
			OracleConnectionStringBuilder.m_defaultValues.Add("DBA PRIVILEGE", "");
			OracleConnectionStringBuilder.m_defaultValues.Add("PROMOTABLE TRANSACTION", "promotable");
			OracleConnectionStringBuilder.m_defaultValues.Add("CONNECTION LIFETIME", 0);
			OracleConnectionStringBuilder.m_defaultValues.Add("INCR POOL SIZE", 5);
			OracleConnectionStringBuilder.m_defaultValues.Add("DECR POOL SIZE", 1);
			OracleConnectionStringBuilder.m_defaultValues.Add("MAX POOL SIZE", 100);
			OracleConnectionStringBuilder.m_defaultValues.Add("MIN POOL SIZE", 1);
			if (OraTrace.m_StmtCacheSize > 0)
			{
				OracleConnectionStringBuilder.m_defaultValues.Add("STATEMENT CACHE SIZE", OraTrace.m_StmtCacheSize);
			}
			else
			{
				OracleConnectionStringBuilder.m_defaultValues.Add("STATEMENT CACHE SIZE", 0);
			}
			OracleConnectionStringBuilder.m_defaultValues.Add("CONNECTION TIMEOUT", 15);
			OracleConnectionStringBuilder.m_defaultValues.Add("ENLIST", "true");
			OracleConnectionStringBuilder.m_defaultValues.Add("POOLING", true);
			OracleConnectionStringBuilder.m_defaultValues.Add("VALIDATE CONNECTION", false);
			OracleConnectionStringBuilder.m_defaultValues.Add("STATEMENT CACHE PURGE", false);
			OracleConnectionStringBuilder.m_defaultValues.Add("PERSIST SECURITY INFO", false);
			OracleConnectionStringBuilder.m_defaultValues.Add("HA EVENTS", false);
			OracleConnectionStringBuilder.m_defaultValues.Add("LOAD BALANCING", false);
			OracleConnectionStringBuilder.m_defaultValues.Add("CONTEXT CONNECTION", false);
			OracleConnectionStringBuilder.m_defaultValues.Add("METADATA POOLING", true);
			OracleConnectionStringBuilder.m_defaultValues.Add("SELF TUNING", OraTrace.m_selfTuning);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00046108 File Offset: 0x00045108
		private void SetProperty(string keyword, object value)
		{
			string text = keyword.ToUpperInvariant();
			string key;
			switch (key = text)
			{
			case "USER ID":
				this.UserID = value.ToString();
				return;
			case "PASSWORD":
				this.Password = value.ToString();
				return;
			case "DATA SOURCE":
				this.DataSource = value.ToString();
				return;
			case "DBA PRIVILEGE":
				this.DBAPrivilege = value.ToString();
				return;
			case "PROXY USER ID":
				this.ProxyUserId = value.ToString();
				return;
			case "PROXY PASSWORD":
				this.ProxyPassword = Convert.ToString(value);
				return;
			case "PROMOTABLE TRANSACTION":
				this.PromotableTransaction = Convert.ToString(value);
				return;
			case "ENLIST":
				this.Enlist = value.ToString();
				return;
			case "MIN POOL SIZE":
			{
				int num2 = 0;
				try
				{
					num2 = int.Parse(value.ToString(), NumberStyles.None);
				}
				catch
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Min Pool Size",
						value.ToString()
					}));
				}
				this.MinPoolSize = num2;
				return;
			}
			case "MAX POOL SIZE":
			{
				int num2;
				try
				{
					num2 = int.Parse(value.ToString(), NumberStyles.None);
				}
				catch
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Max Pool Size",
						value.ToString()
					}));
				}
				this.MaxPoolSize = num2;
				return;
			}
			case "CONNECTION LIFETIME":
			{
				int num2;
				try
				{
					num2 = int.Parse(value.ToString(), NumberStyles.None);
				}
				catch
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Connection Lifetime",
						value.ToString()
					}));
				}
				this.ConnectionLifeTime = num2;
				return;
			}
			case "CONNECTION TIMEOUT":
			case "CONNECT TIMEOUT":
			{
				int num2;
				try
				{
					num2 = int.Parse(value.ToString(), NumberStyles.None);
				}
				catch
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Connection Timeout",
						value.ToString()
					}));
				}
				this.ConnectionTimeout = num2;
				return;
			}
			case "INCR POOL SIZE":
			{
				int num2;
				try
				{
					num2 = int.Parse(value.ToString(), NumberStyles.None);
				}
				catch
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Incr Pool Size",
						value.ToString()
					}));
				}
				this.IncrPoolSize = num2;
				return;
			}
			case "DECR POOL SIZE":
			{
				int num2;
				try
				{
					num2 = int.Parse(value.ToString(), NumberStyles.None);
				}
				catch
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Decr Pool Size",
						value.ToString()
					}));
				}
				this.DecrPoolSize = num2;
				return;
			}
			case "STATEMENT CACHE SIZE":
			{
				int num2;
				try
				{
					num2 = int.Parse(value.ToString(), NumberStyles.None);
				}
				catch
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Statement Cache Size",
						value.ToString()
					}));
				}
				this.StatementCacheSize = num2;
				return;
			}
			case "PERSIST SECURITY INFO":
			{
				string text2 = value.ToString().ToLower();
				if (OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
				{
					this.PersistSecurityInfo = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
					return;
				}
				throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Persist Security Info",
					text2
				}));
			}
			case "POOLING":
			{
				string text2 = value.ToString().ToLower();
				if (OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
				{
					this.Pooling = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
					return;
				}
				throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Pooling",
					text2
				}));
			}
			case "VALIDATE CONNECTION":
			{
				string text2 = value.ToString().ToLower();
				if (OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
				{
					this.ValidateConnection = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
					return;
				}
				throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"validate connection",
					text2
				}));
			}
			case "STATEMENT CACHE PURGE":
			{
				string text2 = value.ToString().ToLower();
				if (OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
				{
					this.StatementCachePurge = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
					return;
				}
				throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Statement Cache Purge",
					text2
				}));
			}
			case "HA EVENTS":
			{
				string text2 = value.ToString().ToLower();
				if (OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
				{
					this.HAEvents = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
					return;
				}
				throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"HA events",
					text2
				}));
			}
			case "LOAD BALANCING":
			{
				string text2 = value.ToString().ToLower();
				if (OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
				{
					this.LoadBalancing = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
					return;
				}
				throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"load balancing",
					text2
				}));
			}
			case "CONTEXT CONNECTION":
			{
				string text2 = value.ToString().ToLower();
				if (OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
				{
					this.ContextConnection = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
					return;
				}
				throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Context Connection",
					text2
				}));
			}
			case "METADATA POOLING":
			{
				string text2 = value.ToString().ToLower();
				if (OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
				{
					this.MetadataPooling = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
					return;
				}
				throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"metadata pooling",
					text2
				}));
			}
			case "SELF TUNING":
			{
				string text2 = value.ToString().ToLower();
				if (OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
				{
					this.SelfTuning = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
					return;
				}
				throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
				{
					"Self Tuning",
					text2
				}));
			}
			}
			throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_ATTRIB, new string[]
			{
				text
			}));
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000469D8 File Offset: 0x000459D8
		private void ResetValues()
		{
			foreach (object obj in OracleConnectionStringBuilder.m_defaultValues.Keys)
			{
				string key = (string)obj;
				this.KeyValuePairList[key] = OracleConnectionStringBuilder.m_defaultValues[key];
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00046A48 File Offset: 0x00045A48
		private void Initialize()
		{
			this.KeyValuePairList = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
			IDictionaryEnumerator enumerator = OracleConnectionStringBuilder.m_defaultValues.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.KeyValuePairList.Add(enumerator.Key as string, enumerator.Value);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00046A96 File Offset: 0x00045A96
		private void SetValueToBaseAndList(string keyword, object value)
		{
			base[keyword] = value;
			this.KeyValuePairList[keyword] = value;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00046AB0 File Offset: 0x00045AB0
		public OracleConnectionStringBuilder()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnectionStringBuilder::OracleConnectionStringBuilder(1)\n"
				});
			}
			this.Initialize();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnectionStringBuilder::OracleConnectionStringBuilder(1)\n"
				});
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00046B04 File Offset: 0x00045B04
		public OracleConnectionStringBuilder(string connectionString)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnectionStringBuilder::OracleConnectionStringBuilder(2)\n"
				});
			}
			if (connectionString == null)
			{
				throw new ArgumentNullException();
			}
			this.Initialize();
			base.ConnectionString = connectionString;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnectionStringBuilder::OracleConnectionStringBuilder(2)\n"
				});
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00046B67 File Offset: 0x00045B67
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x00046B7E File Offset: 0x00045B7E
		[DisplayName("Proxy User")]
		public string ProxyUserId
		{
			get
			{
				return (string)this.KeyValuePairList["PROXY USER ID"];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.SetValueToBaseAndList("PROXY USER ID", value);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00046B95 File Offset: 0x00045B95
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x00046BAC File Offset: 0x00045BAC
		[DisplayName("Proxy Password")]
		public string ProxyPassword
		{
			get
			{
				return (string)this.KeyValuePairList["PROXY PASSWORD"];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.SetValueToBaseAndList("PROXY PASSWORD", value);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00046BC3 File Offset: 0x00045BC3
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00046BDC File Offset: 0x00045BDC
		[DisplayName("DBA Privilege")]
		public string DBAPrivilege
		{
			get
			{
				return (string)this.KeyValuePairList["DBA PRIVILEGE"];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.ToLower() != "sysdba" && value.ToLower() != "sysoper" && value != string.Empty)
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"DBA Privilege",
						value
					}));
				}
				this.SetValueToBaseAndList("DBA PRIVILEGE", value);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00046C62 File Offset: 0x00045C62
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00046C79 File Offset: 0x00045C79
		[DisplayName("User ID")]
		public string UserID
		{
			get
			{
				return (string)this.KeyValuePairList["USER ID"];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.SetValueToBaseAndList("USER ID", value);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00046C90 File Offset: 0x00045C90
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00046CA7 File Offset: 0x00045CA7
		[DisplayName("Data Source")]
		public string DataSource
		{
			get
			{
				return (string)this.KeyValuePairList["DATA SOURCE"];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.SetValueToBaseAndList("DATA SOURCE", value);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00046CBE File Offset: 0x00045CBE
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00046CD5 File Offset: 0x00045CD5
		[DisplayName("Password")]
		[PasswordPropertyText(true)]
		public string Password
		{
			get
			{
				return (string)this.KeyValuePairList["PASSWORD"];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.SetValueToBaseAndList("PASSWORD", value);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00046CEC File Offset: 0x00045CEC
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00046D04 File Offset: 0x00045D04
		[DisplayName("Max Pool Size")]
		public int MaxPoolSize
		{
			get
			{
				return (int)this.KeyValuePairList["MAX POOL SIZE"];
			}
			set
			{
				if (value < 1)
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Max Pool Size",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("MAX POOL SIZE", value);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00046D5F File Offset: 0x00045D5F
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x00046D78 File Offset: 0x00045D78
		[DisplayName("Min Pool Size")]
		public int MinPoolSize
		{
			get
			{
				return (int)this.KeyValuePairList["MIN POOL SIZE"];
			}
			set
			{
				if (value < 0)
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Min Pool Size",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("MIN POOL SIZE", value);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00046DD3 File Offset: 0x00045DD3
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x00046DEC File Offset: 0x00045DEC
		[DisplayName("Increment pool size")]
		public int IncrPoolSize
		{
			get
			{
				return (int)this.KeyValuePairList["INCR POOL SIZE"];
			}
			set
			{
				if (value < 1)
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Increment Pool Size",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("INCR POOL SIZE", value);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00046E47 File Offset: 0x00045E47
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00046E60 File Offset: 0x00045E60
		[DisplayName("Decrement pool size")]
		public int DecrPoolSize
		{
			get
			{
				return (int)this.KeyValuePairList["DECR POOL SIZE"];
			}
			set
			{
				if (value < 1)
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Decrement Pool Size",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("DECR POOL SIZE", value);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00046EBB File Offset: 0x00045EBB
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x00046ED4 File Offset: 0x00045ED4
		[DisplayName("Connection Life Time")]
		public int ConnectionLifeTime
		{
			get
			{
				return (int)this.KeyValuePairList["CONNECTION LIFETIME"];
			}
			set
			{
				if (value < 0)
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Connection Life Time",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("CONNECTION LIFETIME", value);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00046F2F File Offset: 0x00045F2F
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x00046F48 File Offset: 0x00045F48
		[DisplayName("Statement Cache Size")]
		public int StatementCacheSize
		{
			get
			{
				return (int)this.KeyValuePairList["STATEMENT CACHE SIZE"];
			}
			set
			{
				if (value < 0)
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Statement Cache Size",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("STATEMENT CACHE SIZE", value);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00046FA3 File Offset: 0x00045FA3
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x00046FBC File Offset: 0x00045FBC
		[DisplayName("Connection Timeout")]
		public int ConnectionTimeout
		{
			get
			{
				return (int)this.KeyValuePairList["CONNECTION TIMEOUT"];
			}
			set
			{
				if (value < 0)
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Connection Timeout",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("CONNECTION TIMEOUT", value);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00047017 File Offset: 0x00046017
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0004702E File Offset: 0x0004602E
		[DisplayName("Persist Security Info")]
		public bool PersistSecurityInfo
		{
			get
			{
				return (bool)this.KeyValuePairList["PERSIST SECURITY INFO"];
			}
			set
			{
				this.SetValueToBaseAndList("PERSIST SECURITY INFO", value);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00047041 File Offset: 0x00046041
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x00047058 File Offset: 0x00046058
		[DisplayName("Enlist")]
		public string Enlist
		{
			get
			{
				return (string)this.KeyValuePairList["ENLIST"];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.ToLowerInvariant() != "dynamic" && !OracleConnectionStringBuilder.m_boolMapping.ContainsKey(value.ToLower()))
				{
					throw new OracleException(ErrRes.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
					{
						"Enlist",
						value
					}));
				}
				this.SetValueToBaseAndList("ENLIST", value.ToLowerInvariant());
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x000470D6 File Offset: 0x000460D6
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x000470ED File Offset: 0x000460ED
		[DisplayName("metadata pooling")]
		public bool MetadataPooling
		{
			get
			{
				return (bool)this.KeyValuePairList["METADATA POOLING"];
			}
			set
			{
				this.SetValueToBaseAndList("METADATA POOLING", value);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00047100 File Offset: 0x00046100
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x00047117 File Offset: 0x00046117
		[DisplayName("Self Tuning")]
		public bool SelfTuning
		{
			get
			{
				return (bool)this.KeyValuePairList["SELF TUNING"];
			}
			set
			{
				this.SetValueToBaseAndList("SELF TUNING", value);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0004712A File Offset: 0x0004612A
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x00047141 File Offset: 0x00046141
		[DisplayName("Pooling")]
		public bool Pooling
		{
			get
			{
				return (bool)this.KeyValuePairList["POOLING"];
			}
			set
			{
				this.SetValueToBaseAndList("POOLING", value);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00047154 File Offset: 0x00046154
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0004716B File Offset: 0x0004616B
		[DisplayName("Validate Connection")]
		public bool ValidateConnection
		{
			get
			{
				return (bool)this.KeyValuePairList["VALIDATE CONNECTION"];
			}
			set
			{
				this.SetValueToBaseAndList("VALIDATE CONNECTION", value);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0004717E File Offset: 0x0004617E
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x00047195 File Offset: 0x00046195
		[DisplayName("Statement Cache Purge")]
		public bool StatementCachePurge
		{
			get
			{
				return (bool)this.KeyValuePairList["STATEMENT CACHE PURGE"];
			}
			set
			{
				this.SetValueToBaseAndList("STATEMENT CACHE PURGE", value);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x000471A8 File Offset: 0x000461A8
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x000471BF File Offset: 0x000461BF
		[DisplayName("HAEvents")]
		public bool HAEvents
		{
			get
			{
				return (bool)this.KeyValuePairList["HA EVENTS"];
			}
			set
			{
				this.SetValueToBaseAndList("HA EVENTS", value);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000471D2 File Offset: 0x000461D2
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x000471E9 File Offset: 0x000461E9
		[DisplayName("Load Balancing")]
		public bool LoadBalancing
		{
			get
			{
				return (bool)this.KeyValuePairList["LOAD BALANCING"];
			}
			set
			{
				this.SetValueToBaseAndList("LOAD BALANCING", value);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x000471FC File Offset: 0x000461FC
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x00047213 File Offset: 0x00046213
		[DisplayName("Context Connection")]
		public bool ContextConnection
		{
			get
			{
				return (bool)this.KeyValuePairList["CONTEXT CONNECTION"];
			}
			set
			{
				this.SetValueToBaseAndList("CONTEXT CONNECTION", value);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00047226 File Offset: 0x00046226
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x0004723D File Offset: 0x0004623D
		[DisplayName("PromotableTransaction")]
		public string PromotableTransaction
		{
			get
			{
				return (string)this.KeyValuePairList["PROMOTABLE TRANSACTION"];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.SetValueToBaseAndList("PROMOTABLE TRANSACTION", value);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00047254 File Offset: 0x00046254
		public override bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00047258 File Offset: 0x00046258
		public override ICollection Keys
		{
			get
			{
				ICollection<string> keys = this.KeyValuePairList.Keys;
				string[] array = new string[keys.Count];
				keys.CopyTo(array, 0);
				return new ReadOnlyCollection<string>(array);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0004728C File Offset: 0x0004628C
		public override ICollection Values
		{
			get
			{
				ICollection<string> collection = (ICollection<string>)this.Keys;
				IEnumerator<string> enumerator = collection.GetEnumerator();
				object[] array = new object[collection.Count];
				for (int i = 0; i < array.Length; i++)
				{
					enumerator.MoveNext();
					array[i] = this[enumerator.Current];
				}
				return new ReadOnlyCollection<object>(array);
			}
		}

		// Token: 0x1700011C RID: 284
		public override object this[string keyword]
		{
			get
			{
				if (keyword == null)
				{
					throw new ArgumentNullException();
				}
				return this.KeyValuePairList[keyword];
			}
			set
			{
				if (keyword == null)
				{
					throw new ArgumentNullException();
				}
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				this.SetProperty(keyword, value);
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00047318 File Offset: 0x00046318
		public override void Clear()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnectionStringBuilder::Clear()\n"
				});
			}
			base.Clear();
			this.ResetValues();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnectionStringBuilder::Clear()\n"
				});
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0004736C File Offset: 0x0004636C
		public override bool ContainsKey(string keyword)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnectionStringBuilder::ContainsKey()\n"
				});
			}
			if (keyword == null)
			{
				throw new ArgumentNullException();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnectionStringBuilder::ContainsKey()\n"
				});
			}
			return this.KeyValuePairList.ContainsKey(keyword);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000473C8 File Offset: 0x000463C8
		public override bool Remove(string keyword)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnectionStringBuilder::Remove()\n"
				});
			}
			if (keyword == null)
			{
				throw new ArgumentNullException();
			}
			string text = keyword.ToUpperInvariant();
			if (base.Remove(text))
			{
				this.KeyValuePairList[text] = OracleConnectionStringBuilder.m_defaultValues[text];
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleConnectionStringBuilder::Remove()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnectionStringBuilder::Remove()\n"
				});
			}
			return false;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00047460 File Offset: 0x00046460
		public override bool TryGetValue(string keyword, out object value)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleConnectionStringBuilder::TryGetValue()\n"
				});
			}
			if (keyword == null)
			{
				throw new ArgumentNullException();
			}
			if (this.ContainsKey(keyword))
			{
				value = this.KeyValuePairList[keyword];
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleConnectionStringBuilder::TryGetValue()\n"
					});
				}
				return true;
			}
			value = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleConnectionStringBuilder::TryGetValue()\n"
				});
			}
			return false;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000474EA File Offset: 0x000464EA
		protected override void GetProperties(Hashtable propertyDescriptors)
		{
			base.GetProperties(propertyDescriptors);
		}

		// Token: 0x04000419 RID: 1049
		private const int DEFAULT_STATEMENT_CACHE_SIZE = 0;

		// Token: 0x0400041A RID: 1050
		private Dictionary<string, object> KeyValuePairList;

		// Token: 0x0400041B RID: 1051
		private static Hashtable m_boolMapping;

		// Token: 0x0400041C RID: 1052
		private static Hashtable m_defaultValues;
	}
}
