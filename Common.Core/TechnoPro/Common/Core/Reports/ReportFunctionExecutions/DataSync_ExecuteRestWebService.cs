using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Security.Hashing;
using TechnoPro.Common.Xml;
using TechnoPro.Common.Xml.Entity;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200006E RID: 110
	public class DataSync_ExecuteRestWebService : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x00018D6C File Offset: 0x00016F6C
		public DataSync_ExecuteRestWebService()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00018D87 File Offset: 0x00016F87
		public DataSync_ExecuteRestWebService(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00018DA5 File Offset: 0x00016FA5
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00018DAD File Offset: 0x00016FAD
		public OperationContext OpContext { get; set; }

		// Token: 0x06000462 RID: 1122 RVA: 0x00018DB8 File Offset: 0x00016FB8
		private string GetXml(string url, bool usingCredentials, string domain, string user, string pwd)
		{
			string result;
			using (WebClient webClient = new WebClient())
			{
				webClient.Headers.Add("accept", "application/xml");
				webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
				if (usingCredentials)
				{
					webClient.Credentials = new CredentialCache
					{
						{
							new Uri(url),
							"NTLM",
							new NetworkCredential(user, pwd, domain)
						}
					};
				}
				using (Stream stream = webClient.OpenRead(url))
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00018E94 File Offset: 0x00017094
		private bool ExtractUsernamePassword(DataSyncExecuteRestWebServiceParameters parameters, out string domain, out string username, out string password)
		{
			string text = (parameters == null || parameters.Username == null) ? "" : parameters.Username.Trim();
			bool flag = text.Length < 1;
			bool result;
			if (flag)
			{
				username = null;
				domain = null;
				password = null;
				result = false;
			}
			else
			{
				string text2 = (parameters.Password ?? "").Trim();
				bool flag2 = parameters.PasswordEncryptedSettingCode != null && text2.Length < 1;
				if (flag2)
				{
					ISettingManager settingManager = new SettingManager(this.OpContext);
					text2 = settingManager.GetSettingValue<string>(parameters.PasswordEncryptedSettingCode.Value);
				}
				username = text;
				password = text2;
				domain = ((parameters.Domain == null) ? null : parameters.Domain.Trim());
				result = true;
			}
			return result;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00018F64 File Offset: 0x00017164
		private IDictionary<string, object> ExtractParameters(DataTable t, IList<ReportParameter> reportParameters)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			DataRow dataRow = (t == null || t.Rows.Count < 1) ? null : t.Rows[0];
			bool flag = dataRow != null;
			if (flag)
			{
				foreach (object obj in t.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					string text = dataColumn.ColumnName.ToLower();
					bool flag2 = dictionary.ContainsKey(text);
					if (!flag2)
					{
						string value = this.ConvertObjectToString(dataRow[text]);
						dictionary.Add(text, value);
					}
				}
			}
			bool flag3 = reportParameters == null;
			IDictionary<string, object> result;
			if (flag3)
			{
				result = dictionary;
			}
			else
			{
				foreach (ReportParameter reportParameter in reportParameters)
				{
					string key = reportParameter.Name.ToLower();
					bool flag4 = dictionary.ContainsKey(key);
					if (!flag4)
					{
						string value2 = this.ConvertObjectToString(reportParameter.Value);
						dictionary.Add(key, value2);
					}
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000190B0 File Offset: 0x000172B0
		private static double ConvertToUnixTimestamp(DateTime date)
		{
			DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return Math.Floor((date.ToUniversalTime() - d).TotalSeconds);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000190F0 File Offset: 0x000172F0
		private string ConvertObjectToString(object o)
		{
			bool flag = o == null || o is DBNull;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				result = o.ToString();
			}
			return result;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00019124 File Offset: 0x00017324
		private string GetUrl(string urlWithFormatRules, IDictionary<string, object> parameters, string hashType, string hashSecret, string[] hashParameterNamesInOrder)
		{
			Regex regex = new Regex("\\{(.*?)\\}");
			MatchCollection matchCollection = regex.Matches(urlWithFormatRules ?? "");
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string text = match.Value.Substring(1, match.Value.Length - 2);
				string text2 = text.ToLower();
				string text3 = text2;
				string a = text3;
				if (!(a == "unixtimestamp"))
				{
					if (!(a == "hash"))
					{
						if (!(a == "hashsecret"))
						{
							List<string> list = (from g in text.Split(new char[]
							{
								','
							})
							select g.Trim() into h
							where h.Length > 0
							select h).ToList<string>();
							foreach (string key in list)
							{
								bool flag = parameters.ContainsKey(key);
								if (flag)
								{
									dictionary.Add(text, this.ConvertObjectToString(parameters[key]));
									break;
								}
							}
						}
						else
						{
							dictionary.Add(text, hashSecret ?? "");
						}
					}
					else
					{
						IHashingProvider hashingProvider = PasswordHashFactory.GetHashingProvider(hashType ?? "");
						dictionary.Add(text, hashingProvider.CreateHash(this.GetUrl(string.Join("", new string[]
						{
							"{" + (hashParameterNamesInOrder ?? new string[0]).ToArray<string>(),
							"}"
						}), parameters, null, null, null), null));
					}
				}
				else
				{
					dictionary.Add(text, DataSync_ExecuteRestWebService.ConvertToUnixTimestamp(DateTime.Now).ToString());
				}
			}
			return dictionary.Aggregate(urlWithFormatRules, (string current, KeyValuePair<string, string> kvp) => current.Replace(kvp.Key, kvp.Value ?? ""));
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000193B0 File Offset: 0x000175B0
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable dataTable = CurrentWholeReportResult.GetPrimaryDataTable() ?? new DataTable("t2");
			bool flag = string.IsNullOrEmpty(dataTable.TableName);
			if (flag)
			{
				dataTable.TableName = "t";
			}
			DataTable t = dataTable;
			try
			{
				string defaultFunctionParameter = function.GetDefaultFunctionParameter();
				DataSyncExecuteRestWebServiceParameters dataSyncExecuteRestWebServiceParameters = defaultFunctionParameter.ConvertXmlToDataSyncExecuteRestWebServiceParameters();
				string domain;
				string user;
				string pwd;
				bool usingCredentials = this.ExtractUsernamePassword(dataSyncExecuteRestWebServiceParameters, out domain, out user, out pwd);
				string text = dataSyncExecuteRestWebServiceParameters.HashSecret ?? "";
				bool flag2 = text.Trim().Length < 1 && dataSyncExecuteRestWebServiceParameters.HashSecretEncryptedSettingCode != null;
				if (flag2)
				{
					ISettingManager settingManager = new SettingManager(this.OpContext);
					text = settingManager.GetSettingValue<string>(dataSyncExecuteRestWebServiceParameters.HashSecretEncryptedSettingCode.Value);
				}
				IDictionary<string, object> parameters = this.ExtractParameters(t, CurrentWholeReportResult.CurrentReportParameters);
				string url = this.GetUrl(dataSyncExecuteRestWebServiceParameters.Url ?? "", parameters, dataSyncExecuteRestWebServiceParameters.HashType, text, dataSyncExecuteRestWebServiceParameters.HashParameterNamesInOrder);
				string xml = this.GetXml(url, usingCredentials, domain, user, pwd);
				bool returnXml = dataSyncExecuteRestWebServiceParameters.ReturnXml;
				if (returnXml)
				{
					DataTable dataTable2 = new DataTable("xml");
					dataTable2.Columns.Add("xml");
					dataTable2.Rows.Add(new object[]
					{
						xml ?? ""
					});
					result.Data.Table = dataTable2;
				}
				else
				{
					bool flag3 = string.IsNullOrEmpty(xml);
					if (flag3)
					{
						result.Data.Table = new DataTable("empty");
					}
					else
					{
						Forest<XmlEntry> forest = XmlDataTableConverter.ExtractForestFromXml(xml);
						bool flag4 = forest == null;
						if (flag4)
						{
							result.Data.Table = new DataTable("empty2");
						}
						else
						{
							result.Data.Table = XmlDataTableConverter.ConvertForestToDataTable(forest, dataSyncExecuteRestWebServiceParameters.RootNodeName);
						}
					}
				}
			}
			catch (Exception ex)
			{
				string text2 = string.Format("Common.Core.Reports.ReportFunctionExecutions.DataSync_ExecuteRestWebService:err={0}", ex.ToString());
				result.Result = new RunFunctionResult
				{
					Status = new RunStatus
					{
						ErrorMessage = text2,
						LastStatusStep = eRunStatusStep.Failed
					},
					Function = function
				};
				CWLogger.Logger.Error(text2);
			}
		}

		// Token: 0x040000CD RID: 205
		private ReportDAO dao;
	}
}
