using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200005E RID: 94
	[DefaultProperty("DataSource")]
	public sealed class OracleConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x000221B0 File Offset: 0x000203B0
		static OracleConnectionStringBuilder()
		{
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
			OracleConnectionStringBuilder.m_defaultValues.Add("STATEMENT CACHE SIZE", 0);
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
			OracleConnectionStringBuilder.m_defaultValues.Add("SELF TUNING", ConfigBaseClass.m_SelfTuning);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0002241C File Offset: 0x0002061C
		private void SetProperty(string keyword, object value)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				int num = 0;
				string text = keyword.ToUpperInvariant();
				string key;
				if ((key = text) != null)
				{
					if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x600041e-1 == null)
					{
						<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x600041e-1 = new Dictionary<string, int>(25)
						{
							{
								"USER ID",
								0
							},
							{
								"PASSWORD",
								1
							},
							{
								"DATA SOURCE",
								2
							},
							{
								"DBA PRIVILEGE",
								3
							},
							{
								"PROXY USER ID",
								4
							},
							{
								"PROXY PASSWORD",
								5
							},
							{
								"PROMOTABLE TRANSACTION",
								6
							},
							{
								"ENLIST",
								7
							},
							{
								"MIN POOL SIZE",
								8
							},
							{
								"MAX POOL SIZE",
								9
							},
							{
								"CONNECTION LIFETIME",
								10
							},
							{
								"CONNECTION TIMEOUT",
								11
							},
							{
								"CONNECT TIMEOUT",
								12
							},
							{
								"INCR POOL SIZE",
								13
							},
							{
								"DECR POOL SIZE",
								14
							},
							{
								"STATEMENT CACHE SIZE",
								15
							},
							{
								"PERSIST SECURITY INFO",
								16
							},
							{
								"POOLING",
								17
							},
							{
								"VALIDATE CONNECTION",
								18
							},
							{
								"STATEMENT CACHE PURGE",
								19
							},
							{
								"HA EVENTS",
								20
							},
							{
								"LOAD BALANCING",
								21
							},
							{
								"CONTEXT CONNECTION",
								22
							},
							{
								"METADATA POOLING",
								23
							},
							{
								"SELF TUNING",
								24
							}
						};
					}
					int num2;
					if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x600041e-1.TryGetValue(key, out num2))
					{
						switch (num2)
						{
						case 0:
							this.UserID = value.ToString();
							break;
						case 1:
							this.Password = value.ToString();
							break;
						case 2:
							this.DataSource = value.ToString();
							break;
						case 3:
							this.DBAPrivilege = value.ToString();
							break;
						case 4:
							this.ProxyUserId = value.ToString();
							break;
						case 5:
							this.ProxyPassword = Convert.ToString(value);
							break;
						case 6:
							this.PromotableTransaction = Convert.ToString(value);
							break;
						case 7:
							this.Enlist = value.ToString();
							break;
						case 8:
							try
							{
								num = int.Parse(value.ToString(), NumberStyles.None);
							}
							catch
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Min Pool Size",
									value.ToString()
								}));
							}
							this.MinPoolSize = num;
							break;
						case 9:
							try
							{
								num = int.Parse(value.ToString(), NumberStyles.None);
							}
							catch
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Max Pool Size",
									value.ToString()
								}));
							}
							this.MaxPoolSize = num;
							break;
						case 10:
							try
							{
								num = int.Parse(value.ToString(), NumberStyles.None);
							}
							catch
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Connection Lifetime",
									value.ToString()
								}));
							}
							this.ConnectionLifeTime = num;
							break;
						case 11:
						case 12:
							try
							{
								num = int.Parse(value.ToString(), NumberStyles.None);
							}
							catch
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Connection Timeout",
									value.ToString()
								}));
							}
							this.ConnectionTimeout = num;
							break;
						case 13:
							try
							{
								num = int.Parse(value.ToString(), NumberStyles.None);
							}
							catch
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Incr Pool Size",
									value.ToString()
								}));
							}
							this.IncrPoolSize = num;
							break;
						case 14:
							try
							{
								num = int.Parse(value.ToString(), NumberStyles.None);
							}
							catch
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Decr Pool Size",
									value.ToString()
								}));
							}
							this.DecrPoolSize = num;
							break;
						case 15:
							try
							{
								num = int.Parse(value.ToString(), NumberStyles.None);
							}
							catch
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Statement Cache Size",
									value.ToString()
								}));
							}
							this.StatementCacheSize = num;
							break;
						case 16:
						{
							string text2 = value.ToString().ToLowerInvariant();
							if (!OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Persist Security Info",
									text2
								}));
							}
							this.PersistSecurityInfo = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
							break;
						}
						case 17:
						{
							string text2 = value.ToString().ToLowerInvariant();
							if (!OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Pooling",
									text2
								}));
							}
							this.Pooling = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
							break;
						}
						case 18:
						{
							string text2 = value.ToString().ToLowerInvariant();
							if (!OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"validate connection",
									text2
								}));
							}
							this.ValidateConnection = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
							break;
						}
						case 19:
						{
							string text2 = value.ToString().ToLowerInvariant();
							if (!OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Statement Cache Purge",
									text2
								}));
							}
							this.StatementCachePurge = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
							break;
						}
						case 20:
						{
							string text2 = value.ToString().ToLowerInvariant();
							if (!OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"HA events",
									text2
								}));
							}
							this.HAEvents = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
							break;
						}
						case 21:
						{
							string text2 = value.ToString().ToLowerInvariant();
							if (!OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"load balancing",
									text2
								}));
							}
							this.LoadBalancing = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
							break;
						}
						case 22:
						{
							string text2 = value.ToString().ToLowerInvariant();
							if (!OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Context Connection",
									text2
								}));
							}
							this.ContextConnection = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
							break;
						}
						case 23:
						{
							string text2 = value.ToString().ToLowerInvariant();
							if (!OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"metadata pooling",
									text2
								}));
							}
							this.MetadataPooling = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
							break;
						}
						case 24:
						{
							string text2 = value.ToString().ToLowerInvariant();
							if (!OracleConnectionStringBuilder.m_boolMapping.ContainsKey(text2))
							{
								throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
								{
									"Self Tuning",
									value.ToString()
								}));
							}
							this.SelfTuning = (bool)OracleConnectionStringBuilder.m_boolMapping[text2];
							break;
						}
						default:
							goto IL_8B8;
						}
						return;
					}
				}
				IL_8B8:
				throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_ATTRIB, new string[]
				{
					text
				}));
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00022DF0 File Offset: 0x00020FF0
		private void ResetValues()
		{
			foreach (object obj in OracleConnectionStringBuilder.m_defaultValues.Keys)
			{
				string key = (string)obj;
				this.KeyValuePairList[key] = OracleConnectionStringBuilder.m_defaultValues[key];
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00022E60 File Offset: 0x00021060
		private void Initialize()
		{
			this.KeyValuePairList = new Dictionary<string, object>();
			IDictionaryEnumerator enumerator = OracleConnectionStringBuilder.m_defaultValues.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.KeyValuePairList.Add(enumerator.Key as string, enumerator.Value);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00022EAC File Offset: 0x000210AC
		private void SetValueToBaseAndList(string keyword, object value)
		{
			base[keyword] = value;
			this.KeyValuePairList[keyword] = value;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00022EC4 File Offset: 0x000210C4
		public OracleConnectionStringBuilder()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.Initialize();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00022F40 File Offset: 0x00021140
		public OracleConnectionStringBuilder(string connectionString)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (connectionString == null)
				{
					throw new ArgumentNullException();
				}
				this.Initialize();
				base.ConnectionString = connectionString;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00022FCC File Offset: 0x000211CC
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00022FE4 File Offset: 0x000211E4
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

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00022FFC File Offset: 0x000211FC
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00023014 File Offset: 0x00021214
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

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0002302C File Offset: 0x0002122C
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00023044 File Offset: 0x00021244
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
				if (value.ToLowerInvariant() != "sysdba" && value.ToLowerInvariant() != "sysoper" && value != string.Empty)
				{
					throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
					{
						"DBA Privilege",
						value
					}));
				}
				this.SetValueToBaseAndList("DBA PRIVILEGE", value);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000230CC File Offset: 0x000212CC
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x000230E4 File Offset: 0x000212E4
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

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000230FC File Offset: 0x000212FC
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00023114 File Offset: 0x00021314
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

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0002312C File Offset: 0x0002132C
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00023144 File Offset: 0x00021344
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0002315C File Offset: 0x0002135C
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x00023174 File Offset: 0x00021374
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
					throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
					{
						"Max Pool Size",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("MAX POOL SIZE", value);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000231D0 File Offset: 0x000213D0
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x000231E8 File Offset: 0x000213E8
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
					throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
					{
						"Min Pool Size",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("MIN POOL SIZE", value);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00023244 File Offset: 0x00021444
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0002325C File Offset: 0x0002145C
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
					throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
					{
						"Increment Pool Size",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("INCR POOL SIZE", value);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x000232B8 File Offset: 0x000214B8
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x000232D0 File Offset: 0x000214D0
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
					throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
					{
						"Decrement Pool Size",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("DECR POOL SIZE", value);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0002332C File Offset: 0x0002152C
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00023344 File Offset: 0x00021544
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
					throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
					{
						"Connection Life Time",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("CONNECTION LIFETIME", value);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x000233A0 File Offset: 0x000215A0
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x000233B8 File Offset: 0x000215B8
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
					throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
					{
						"Statement Cache Size",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("STATEMENT CACHE SIZE", value);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00023414 File Offset: 0x00021614
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0002342C File Offset: 0x0002162C
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00023440 File Offset: 0x00021640
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00023458 File Offset: 0x00021658
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
					throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
					{
						"Connection Timeout",
						value.ToString()
					}));
				}
				this.SetValueToBaseAndList("CONNECTION TIMEOUT", value);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000234B4 File Offset: 0x000216B4
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x000234CC File Offset: 0x000216CC
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

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000234E0 File Offset: 0x000216E0
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x000234F8 File Offset: 0x000216F8
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
				if (value.ToLowerInvariant() != "dynamic" && !OracleConnectionStringBuilder.m_boolMapping.ContainsKey(value.ToLowerInvariant()))
				{
					throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
					{
						"Enlist",
						value
					}));
				}
				this.SetValueToBaseAndList("ENLIST", value.ToLowerInvariant());
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00023578 File Offset: 0x00021778
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00023590 File Offset: 0x00021790
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

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x000235A4 File Offset: 0x000217A4
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x000235BC File Offset: 0x000217BC
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

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x000235D0 File Offset: 0x000217D0
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x000235E8 File Offset: 0x000217E8
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

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x000235FC File Offset: 0x000217FC
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00023614 File Offset: 0x00021814
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

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00023628 File Offset: 0x00021828
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00023640 File Offset: 0x00021840
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

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00023654 File Offset: 0x00021854
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0002366C File Offset: 0x0002186C
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

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00023680 File Offset: 0x00021880
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00023698 File Offset: 0x00021898
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

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x000236AC File Offset: 0x000218AC
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x000236C4 File Offset: 0x000218C4
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

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x000236DC File Offset: 0x000218DC
		public override bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000236E0 File Offset: 0x000218E0
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

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00023714 File Offset: 0x00021914
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

		// Token: 0x1700011E RID: 286
		public override object this[string keyword]
		{
			get
			{
				if (keyword == null)
				{
					throw new ArgumentNullException();
				}
				return this.KeyValuePairList[keyword.ToUpperInvariant()];
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

		// Token: 0x06000468 RID: 1128 RVA: 0x000237A8 File Offset: 0x000219A8
		public override void Clear()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				base.Clear();
				this.ResetValues();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00023824 File Offset: 0x00021A24
		public override bool ContainsKey(string keyword)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (keyword == null)
				{
					throw new ArgumentNullException();
				}
				result = this.KeyValuePairList.ContainsKey(keyword.ToUpperInvariant());
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000238AC File Offset: 0x00021AAC
		public override bool Remove(string keyword)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (keyword == null)
				{
					throw new ArgumentNullException();
				}
				string text = keyword.ToUpperInvariant();
				if (base.Remove(text))
				{
					this.KeyValuePairList[text] = OracleConnectionStringBuilder.m_defaultValues[text];
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00023950 File Offset: 0x00021B50
		public override bool TryGetValue(string keyword, out object value)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (keyword == null)
				{
					throw new ArgumentNullException();
				}
				string text = keyword.ToUpperInvariant();
				if (this.ContainsKey(text))
				{
					value = this.KeyValuePairList[text];
					result = true;
				}
				else
				{
					value = null;
					result = false;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000239F0 File Offset: 0x00021BF0
		protected override void GetProperties(Hashtable propertyDescriptors)
		{
			base.GetProperties(propertyDescriptors);
		}

		// Token: 0x040005BF RID: 1471
		private Dictionary<string, object> KeyValuePairList;

		// Token: 0x040005C0 RID: 1472
		private static Hashtable m_boolMapping = new Hashtable(4);

		// Token: 0x040005C1 RID: 1473
		private static Hashtable m_defaultValues;
	}
}
