using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005CA RID: 1482
	public static class ReportFunctionExecutionsAdapter
	{
		// Token: 0x06002FA2 RID: 12194 RVA: 0x00037490 File Offset: 0x00035690
		private static XDocument XDocumentParse(string xml)
		{
			string text = (xml ?? "").Trim();
			bool flag = text.Length < 1;
			XDocument result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					return XDocument.Parse(text);
				}
				catch (Exception ex)
				{
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000374E4 File Offset: 0x000356E4
		public static eCustomDataLoadType GetLoadType(this CustomDataParametersWithLoadParameters parameters)
		{
			bool flag = parameters == null;
			eCustomDataLoadType result;
			if (flag)
			{
				result = eCustomDataLoadType.Unknown;
			}
			else
			{
				bool flag2 = parameters.LoadType > eCustomDataLoadType.Unknown;
				if (flag2)
				{
					result = parameters.LoadType;
				}
				else
				{
					bool flag3 = string.IsNullOrEmpty(parameters.SourceFileName);
					if (flag3)
					{
						result = eCustomDataLoadType.Unknown;
					}
					else
					{
						string text = Path.GetExtension(parameters.SourceFileName).ToLower();
						string text2 = text;
						string a = text2;
						if (!(a == ".csv"))
						{
							if (!(a == ".xls") && !(a == ".xlsx"))
							{
								if (!(a == ".tab"))
								{
									if (!(a == ".txt"))
									{
										result = eCustomDataLoadType.Unknown;
									}
									else
									{
										bool flag4 = !File.Exists(parameters.SourceFileName);
										if (flag4)
										{
											result = eCustomDataLoadType.Unknown;
										}
										else
										{
											string text3 = ReportFunctionExecutionsAdapter.ReadFirstLine(parameters.SourceFileName);
											bool flag5 = text3.IndexOf(',') >= 0;
											if (flag5)
											{
												result = eCustomDataLoadType.Csv;
											}
											else
											{
												bool flag6 = text3.IndexOf('\t') >= 0;
												if (flag6)
												{
													result = eCustomDataLoadType.TabDelimited;
												}
												else
												{
													result = eCustomDataLoadType.Unknown;
												}
											}
										}
									}
								}
								else
								{
									result = eCustomDataLoadType.TabDelimited;
								}
							}
							else
							{
								result = eCustomDataLoadType.Excel;
							}
						}
						else
						{
							result = eCustomDataLoadType.Csv;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x00037600 File Offset: 0x00035800
		private static string ReadFirstLine(string fn)
		{
			StreamReader streamReader = null;
			try
			{
				StreamReader streamReader2;
				streamReader = (streamReader2 = new StreamReader(fn));
				try
				{
					return streamReader.ReadLine() ?? "";
				}
				finally
				{
					if (streamReader2 != null)
					{
						((IDisposable)streamReader2).Dispose();
					}
				}
			}
			catch
			{
			}
			finally
			{
				try
				{
					bool flag = streamReader != null;
					if (flag)
					{
						streamReader.Close();
					}
				}
				catch
				{
				}
			}
			return "";
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x00037698 File Offset: 0x00035898
		public static IList<int> GetIntListFromString(string s, bool returnNullForEmptyOrNullString)
		{
			bool flag = string.IsNullOrEmpty(s);
			IList<int> result;
			if (flag)
			{
				result = (returnNullForEmptyOrNullString ? null : new List<int>());
			}
			else
			{
				result = (from h in s.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries).Select(delegate(string g)
				{
					int result2;
					int.TryParse(g, out result2);
					return result2;
				})
				where h > 0
				select h).ToList<int>();
			}
			return result;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x00037724 File Offset: 0x00035924
		public static bool GetBoolFromString(this string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			bool flag2;
			return !flag && (s == "1" || (bool.TryParse(s, out flag2) && flag2));
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x00037760 File Offset: 0x00035960
		public static T GetEnumFromStringWithEnumIntValue<T>(this string s, T defaultValue = default(T))
		{
			bool flag = string.IsNullOrEmpty(s);
			T result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				int intFromString = s.GetIntFromString(0);
				bool flag2 = !Enum.IsDefined(typeof(T), intFromString);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					result = (T)((object)intFromString);
				}
			}
			return result;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000377B4 File Offset: 0x000359B4
		public static T? GetNullableEnumFromStringWithEnumIntValue<T>(this string s) where T : struct
		{
			bool flag = string.IsNullOrEmpty(s);
			T? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int intFromString = s.GetIntFromString(0);
				bool flag2 = !Enum.IsDefined(typeof(T), intFromString);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new T?((T)((object)intFromString));
				}
			}
			return result;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x00037820 File Offset: 0x00035A20
		public static int GetIntFromString(this string s, int defaultValue = 0)
		{
			bool flag = string.IsNullOrEmpty(s);
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				int num;
				bool flag2 = !int.TryParse(s, out num);
				if (flag2)
				{
					num = defaultValue;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x00037854 File Offset: 0x00035A54
		public static DateTime? GetDateTimeFromString(this string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DateTime value;
				result = ((!DateTime.TryParse(s, out value)) ? null : new DateTime?(value));
			}
			return result;
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x00037898 File Offset: 0x00035A98
		public static CustomDataParametersWithLoadParameters CustomDataParametersWithLoadParametersFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			CustomDataParametersWithLoadParameters result;
			if (flag)
			{
				result = new CustomDataParametersWithLoadParameters();
			}
			else
			{
				XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
				bool flag2 = xdocument == null;
				if (flag2)
				{
					result = new CustomDataParametersWithLoadParameters();
				}
				else
				{
					try
					{
						List<XAttribute> source = xdocument.Descendants("customdataparameterswithload").Attributes().ToList<XAttribute>();
						XAttribute xattribute = source.FirstOrDefault((XAttribute g) => g.Name == "fn");
						XAttribute xattribute2 = source.FirstOrDefault((XAttribute g) => g.Name == "snumcol");
						XAttribute xattribute3 = source.FirstOrDefault((XAttribute g) => g.Name == "tablename");
						XAttribute xattribute4 = source.FirstOrDefault((XAttribute g) => g.Name == "loadtype");
						XAttribute xattribute5 = source.FirstOrDefault((XAttribute g) => g.Name == "customdelimiter");
						XAttribute xattribute6 = source.FirstOrDefault((XAttribute g) => g.Name == "noheaders");
						int num;
						bool flag3 = xattribute4 == null || string.IsNullOrEmpty(xattribute4.Value) || !int.TryParse(xattribute4.Value, out num);
						if (flag3)
						{
							num = 0;
						}
						return new CustomDataParametersWithLoadParameters
						{
							SourceFileName = ((xattribute == null) ? "" : (xattribute.Value ?? "")),
							LoadType = (eCustomDataLoadType)(Enum.IsDefined(typeof(eCustomDataLoadType), num) ? num : 0),
							CustomDelimiter = ((xattribute5 == null) ? "" : (xattribute5.Value ?? "")),
							ExternalStudentNumberColumnName = ((xattribute2 == null) ? "" : (xattribute2.Value ?? "")),
							CustomTableNameWithoutCustomPrefix = ((xattribute3 == null) ? "" : (xattribute3.Value ?? "")),
							FirstRowDoesntHaveHeaders = (xattribute6 != null && (xattribute6.Value ?? "").Trim() == "1")
						};
					}
					catch
					{
					}
					result = new CustomDataParametersWithLoadParameters();
				}
			}
			return result;
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x00037B28 File Offset: 0x00035D28
		public static string CustomDataParametersWithLoadParametersToXml(this CustomDataParametersWithLoadParameters parameters)
		{
			bool flag = parameters == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = new XElement("customdataparameterswithload", new object[]
				{
					new XAttribute("fn", parameters.SourceFileName ?? ""),
					new XAttribute("loadtype", ((int)parameters.LoadType).ToString()),
					new XAttribute("customdelimiter", parameters.CustomDelimiter ?? ""),
					new XAttribute("snumcol", parameters.ExternalStudentNumberColumnName ?? ""),
					new XAttribute("tablename", parameters.CustomTableNameWithoutCustomPrefix ?? ""),
					new XAttribute("noheaders", parameters.FirstRowDoesntHaveHeaders ? "1" : "0")
				}).ToString();
			}
			return result;
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x00037C34 File Offset: 0x00035E34
		public static CustomDataParameters CustomDataParametersFromXml(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			CustomDataParameters result;
			if (flag)
			{
				result = new CustomDataParameters();
			}
			else
			{
				try
				{
					List<XAttribute> source = xdocument.Descendants("customdataparameters").Attributes().ToList<XAttribute>();
					XAttribute xattribute = source.FirstOrDefault((XAttribute g) => g.Name == "snumcol");
					XAttribute xattribute2 = source.FirstOrDefault((XAttribute g) => g.Name == "tablename");
					return new CustomDataParameters
					{
						ExternalStudentNumberColumnName = ((xattribute == null) ? "" : (xattribute.Value ?? "")),
						CustomTableNameWithoutCustomPrefix = ((xattribute2 == null) ? "" : (xattribute2.Value ?? ""))
					};
				}
				catch
				{
				}
				result = new CustomDataParameters();
			}
			return result;
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x00037D34 File Offset: 0x00035F34
		public static string CustomDataParametersToXml(this CustomDataParameters parameters)
		{
			bool flag = parameters == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = new XElement("customdataparameters", new object[]
				{
					new XAttribute("snumcol", parameters.ExternalStudentNumberColumnName ?? ""),
					new XAttribute("tablename", parameters.CustomTableNameWithoutCustomPrefix ?? "")
				}).ToString();
			}
			return result;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x00037DB4 File Offset: 0x00035FB4
		public static OracleQueryParameters OracleQueryParametersFromXml(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			OracleQueryParameters result;
			if (flag)
			{
				result = new OracleQueryParameters();
			}
			else
			{
				try
				{
					IEnumerable<XElement> source = xdocument.Descendants("oracleparameters");
					return (from op in source
					let attrConnectionstring = op.Attribute("cs")
					let opQuery = op.Element("query")
					let opQuerySql = (opQuery == null) ? null : opQuery.Attribute("sql")
					let opQueryType = (opQuery == null) ? null : opQuery.Attribute("querytype")
					let opQueryParameters = (opQuery == null) ? null : opQuery.Elements("parameters")
					select new
					{
						<>h__TransparentIdentifier4 = <>h__TransparentIdentifier4,
						opQueryParameters2 = ((opQueryParameters == null) ? null : opQueryParameters.Elements("parameter"))
					}).Select(delegate(<>h__TransparentIdentifier5)
					{
						OracleQueryParameters oracleQueryParameters = new OracleQueryParameters();
						oracleQueryParameters.ConnectionString = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.attrConnectionstring == null) ? "" : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.attrConnectionstring.Value ?? ""));
						OracleQueryRequest query;
						if (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.opQuery != null)
						{
							OracleQueryRequest oracleQueryRequest = new OracleQueryRequest();
							oracleQueryRequest.Sql = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.opQuerySql == null) ? "" : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.opQuerySql.Value ?? ""));
							oracleQueryRequest.QueryType = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.opQueryType == null) ? eOracleQueryType.Query : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.opQueryType.Value ?? "").GetEnumFromStringWithEnumIntValue(eOracleQueryType.Query));
							query = oracleQueryRequest;
							List<OracleParameter> parameters;
							if (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.opQueryParameters != null)
							{
								parameters = (from p in opQueryParameters2
								let pName = p.Attribute("name")
								let pOracleType = p.Attribute("oracletype")
								let pIsOut = p.Attribute("isout")
								select new OracleParameter
								{
									Name = ((pName == null) ? "" : (pName.Value ?? "")),
									OracleDbType = ((pOracleType == null) ? "" : (pOracleType.Value ?? "")),
									IsOutParameter = (pIsOut != null && pIsOut.Value != null && pIsOut.Value == "1")
								}).ToList<OracleParameter>();
							}
							else
							{
								parameters = new List<OracleParameter>();
							}
							oracleQueryRequest.Parameters = parameters;
						}
						else
						{
							query = new OracleQueryRequest();
						}
						oracleQueryParameters.Query = query;
						return oracleQueryParameters;
					}).FirstOrDefault<OracleQueryParameters>() ?? new OracleQueryParameters();
				}
				catch
				{
				}
				result = new OracleQueryParameters();
			}
			return result;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x00037F28 File Offset: 0x00036128
		public static string OracleQueryParametersToXml(this OracleQueryParameters parameters)
		{
			bool flag = parameters == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				XName name = "oracleparameters";
				object[] array = new object[2];
				array[0] = new XAttribute("cs", parameters.ConnectionString ?? "");
				int num = 1;
				XName name2 = "query";
				object[] array2 = new object[3];
				array2[0] = new XAttribute("sql", (parameters.Query == null) ? "" : (parameters.Query.Sql ?? ""));
				array2[1] = new XAttribute("querytype", (parameters.Query == null) ? "" : ((int)parameters.Query.QueryType).ToString());
				int num2 = 2;
				XName name3 = "parameters";
				object content;
				if (parameters.Query != null && parameters.Query.Parameters != null)
				{
					content = from OracleParameter p in parameters.Query.Parameters
					select new XElement("parameter", new object[]
					{
						new XAttribute("name", p.Name ?? ""),
						new XAttribute("oracletype", p.OracleDbType ?? ""),
						new XAttribute("isout", p.IsOutParameter ? "1" : "0")
					});
				}
				else
				{
					content = null;
				}
				array2[num2] = new XElement(name3, content);
				array[num] = new XElement(name2, array2);
				result = new XElement(name, array).ToString();
			}
			return result;
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x00038068 File Offset: 0x00036268
		public static SqlQueryExtendedParameters SqlQueryExtendedParametersFromXml(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			SqlQueryExtendedParameters result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					List<XAttribute> source = xdocument.Descendants("sqlqueryextendedparameters").Attributes().ToList<XAttribute>();
					XAttribute xattribute = source.FirstOrDefault((XAttribute g) => g.Name == "overridetimeout");
					XAttribute xattribute2 = source.FirstOrDefault((XAttribute g) => g.Name == "sql");
					string s = (xattribute == null) ? "" : (xattribute.Value ?? "");
					int overrideTimeout;
					int.TryParse(s, out overrideTimeout);
					return new SqlQueryExtendedParameters
					{
						OverrideTimeout = overrideTimeout,
						Sql = ((xattribute2 == null) ? "" : (xattribute2.Value ?? ""))
					};
				}
				catch
				{
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x0003816C File Offset: 0x0003636C
		public static string SqlQueryExtendedParametersToXml(this SqlQueryExtendedParameters parameters)
		{
			bool flag = parameters == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = new XElement("sqlqueryextendedparameters", new object[]
				{
					new XAttribute("overridetimeout", parameters.OverrideTimeout.ToString()),
					new XAttribute("sql", parameters.Sql ?? "")
				}).ToString();
			}
			return result;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000381EC File Offset: 0x000363EC
		public static string ConvertReportFunctionLoadAppointmentsParametersToXml(this ReportFunctionLoadAppointmentsParameters parameters)
		{
			bool flag = parameters == null;
			if (flag)
			{
				parameters = new ReportFunctionLoadAppointmentsParameters();
			}
			XName name = "loadappointmentsparameters";
			XName name2 = "loadappointments";
			object[] array = new object[7];
			array[0] = new XAttribute("startdate", (parameters.StartDate != null) ? parameters.StartDate.Value.ToString("yyyy-MM-dd") : "");
			array[1] = new XAttribute("enddate", (parameters.EndDate != null) ? parameters.EndDate.Value.ToString("yyyy-MM-dd") : "");
			array[2] = new XAttribute("includecancelled", parameters.IncludeCancelled.ToString());
			int num = 3;
			XName name3 = "pids";
			object value;
			if (parameters.PersonIds != null)
			{
				value = string.Join(",", (from g in parameters.PersonIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = "";
			}
			array[num] = new XAttribute(name3, value);
			int num2 = 4;
			XName name4 = "apptypeids";
			object value2;
			if (parameters.AppTypeIds != null)
			{
				value2 = string.Join(",", (from g in parameters.AppTypeIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value2 = "";
			}
			array[num2] = new XAttribute(name4, value2);
			array[5] = new XAttribute("loadappointmentsmethod", ((int)parameters.LoadAppointmentsdMethod).ToString());
			int num3 = 6;
			XName name5 = "gids";
			object value3;
			if (parameters.GroupIds != null)
			{
				value3 = string.Join(",", (from g in parameters.GroupIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value3 = "";
			}
			array[num3] = new XAttribute(name5, value3);
			XElement xelement = new XElement(name, new XElement(name2, array));
			return xelement.ToString();
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x00038414 File Offset: 0x00036614
		public static ReportFunctionLoadAppointmentsParameters GetReportFunctionLoadAppointmentsParametersFromXml(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			ReportFunctionLoadAppointmentsParameters result;
			if (flag)
			{
				result = new ReportFunctionLoadAppointmentsParameters();
			}
			else
			{
				try
				{
					List<XAttribute> source = xdocument.Descendants("loadappointments").Attributes().ToList<XAttribute>();
					XAttribute xattribute = source.FirstOrDefault((XAttribute g) => g.Name == "startdate");
					XAttribute xattribute2 = source.FirstOrDefault((XAttribute g) => g.Name == "enddate");
					XAttribute xattribute3 = source.FirstOrDefault((XAttribute g) => g.Name == "includecancelled");
					XAttribute xattribute4 = source.FirstOrDefault((XAttribute g) => g.Name == "pids");
					XAttribute xattribute5 = source.FirstOrDefault((XAttribute g) => g.Name == "gids");
					XAttribute xattribute6 = source.FirstOrDefault((XAttribute g) => g.Name == "apptypeids");
					XAttribute xattribute7 = source.FirstOrDefault((XAttribute g) => g.Name == "loadappointmentsmethod");
					string s = (xattribute7 == null) ? "" : (xattribute7.Value ?? "");
					int num;
					int.TryParse(s, out num);
					result = new ReportFunctionLoadAppointmentsParameters
					{
						LoadAppointmentsdMethod = (eLoadAppointmentsType)(Enum.IsDefined(typeof(eLoadAppointmentsType), num) ? num : 0),
						StartDate = ((xattribute == null) ? "" : xattribute.Value).GetDateTimeFromString(),
						EndDate = ((xattribute2 == null) ? "" : xattribute2.Value).GetDateTimeFromString(),
						IncludeCancelled = ((xattribute3 == null) ? "" : xattribute3.Value).GetBoolFromString(),
						PersonIds = ReportFunctionExecutionsAdapter.GetIntListFromString((xattribute4 == null) ? "" : xattribute4.Value, true),
						GroupIds = ReportFunctionExecutionsAdapter.GetIntListFromString((xattribute5 == null) ? "" : xattribute5.Value, true),
						AppTypeIds = ReportFunctionExecutionsAdapter.GetIntListFromString((xattribute6 == null) ? "" : xattribute6.Value, true)
					};
				}
				catch (Exception ex)
				{
					result = new ReportFunctionLoadAppointmentsParameters();
				}
			}
			return result;
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000386A8 File Offset: 0x000368A8
		public static string ConvertImportExcelParametersToXml(this ImportExcelParameters excelParameters)
		{
			bool flag = excelParameters == null;
			if (flag)
			{
				excelParameters = new ImportExcelParameters();
			}
			XElement xelement = new XElement("importexcelparameters", new XElement("importexcel", new object[]
			{
				new XAttribute("filename", excelParameters.ExcelFilenameWithPath ?? ""),
				new XAttribute("worksheet", excelParameters.WorksheetName ?? ""),
				new XAttribute("worksheetindex", excelParameters.WorksheetIndex.ToString())
			}));
			return xelement.ToString();
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x00038758 File Offset: 0x00036958
		public static ImportExcelParameters ConvertXmlToImportExcelParameters(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			ImportExcelParameters result;
			if (flag)
			{
				result = new ImportExcelParameters();
			}
			else
			{
				try
				{
					List<XAttribute> source = xdocument.Descendants("importexcel").Attributes().ToList<XAttribute>();
					XAttribute xattribute = source.FirstOrDefault((XAttribute g) => g.Name == "filename");
					XAttribute xattribute2 = source.FirstOrDefault((XAttribute g) => g.Name == "worksheet");
					XAttribute xattribute3 = source.FirstOrDefault((XAttribute g) => g.Name == "worksheetindex");
					string text = (xattribute3 == null) ? "" : (xattribute3.Value ?? "");
					int worksheetIndex;
					bool flag2 = string.IsNullOrEmpty(text) || !int.TryParse(text, out worksheetIndex);
					if (flag2)
					{
						worksheetIndex = 0;
					}
					return new ImportExcelParameters
					{
						ExcelFilenameWithPath = ((xattribute == null) ? "" : (xattribute.Value ?? "")),
						WorksheetName = ((xattribute == null) ? "" : (xattribute2.Value ?? "")),
						WorksheetIndex = worksheetIndex
					};
				}
				catch (Exception ex)
				{
				}
				result = new ImportExcelParameters();
			}
			return result;
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000388D0 File Offset: 0x00036AD0
		public static DataSyncFixTimetableParameters ConvertXmlToDataSyncFixTimetableParameters(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			DataSyncFixTimetableParameters result;
			if (flag)
			{
				result = new DataSyncFixTimetableParameters();
			}
			else
			{
				try
				{
					List<XAttribute> source = xdocument.Descendants("fixtimetable").Attributes().ToList<XAttribute>();
					XAttribute xattribute = source.FirstOrDefault((XAttribute g) => g.Name == "dowtype");
					XAttribute xattribute2 = source.FirstOrDefault((XAttribute g) => g.Name == "dowinseparatecolumns");
					XAttribute xattribute3 = source.FirstOrDefault((XAttribute g) => g.Name == "timetype");
					XAttribute xattribute4 = source.FirstOrDefault((XAttribute g) => g.Name == "dowcolname");
					XAttribute xattribute5 = source.FirstOrDefault((XAttribute g) => g.Name == "starttimecolname");
					XAttribute xattribute6 = source.FirstOrDefault((XAttribute g) => g.Name == "endtimecolname");
					result = new DataSyncFixTimetableParameters
					{
						DayOfWeekType = ((xattribute == null) ? "" : (xattribute.Value ?? "")).ParseEnumFromIntString<eDataSyncFixTimetableDayOfWeekType>(),
						IsDayOfWeekInSeparateColumns = ("1yestrue".IndexOf(((xattribute2 == null) ? "" : (xattribute2.Value ?? "")).Trim().ToLower()) >= 0),
						TimeType = ((xattribute3 == null) ? "" : (xattribute3.Value ?? "")).ParseEnumFromIntString<eDataSyncFixTimetableTimeType>(),
						DayOfWeekColName = ((xattribute4 == null) ? "" : (xattribute4.Value ?? "")),
						StartTimeColName = ((xattribute5 == null) ? "" : (xattribute5.Value ?? "")),
						EndTimeColName = ((xattribute6 == null) ? "" : (xattribute6.Value ?? ""))
					};
				}
				catch (Exception ex)
				{
					result = new DataSyncFixTimetableParameters();
				}
			}
			return result;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x00038B20 File Offset: 0x00036D20
		public static string ConvertDataSyncFixTimetableParametersToXml(this DataSyncFixTimetableParameters parameters)
		{
			bool flag = parameters == null;
			if (flag)
			{
				parameters = new DataSyncFixTimetableParameters();
			}
			XElement xelement = new XElement("fixtimetableparameters", new XElement("fixtimetable", new object[]
			{
				new XAttribute("dowtype", ((int)parameters.DayOfWeekType).ToString()),
				new XAttribute("timetype", ((int)parameters.TimeType).ToString()),
				new XAttribute("dowinseparatecolumns", parameters.IsDayOfWeekInSeparateColumns ? "1" : "0"),
				new XAttribute("dowcolname", parameters.DayOfWeekColName ?? ""),
				new XAttribute("starttimecolname", parameters.StartTimeColName ?? ""),
				new XAttribute("endtimecolname", parameters.EndTimeColName ?? "")
			}));
			return xelement.ToString();
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x00038C38 File Offset: 0x00036E38
		public static DataSyncMoveDataIntoClockWorkParameters ConvertXmlToDataSyncMoveDataIntoClockWorkParameters(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			DataSyncMoveDataIntoClockWorkParameters result;
			if (flag)
			{
				result = new DataSyncMoveDataIntoClockWorkParameters();
			}
			else
			{
				try
				{
					List<XAttribute> source = xdocument.Descendants("movedata").Attributes().ToList<XAttribute>();
					XAttribute xattribute = source.FirstOrDefault((XAttribute g) => g.Name == "sourcefiletype");
					XAttribute xattribute2 = source.FirstOrDefault((XAttribute g) => g.Name == "args");
					IEnumerable<XElement> source2 = xdocument.Descendants("movedataitem");
					List<DataSyncMoveDataIntoClockWorkItem> items = (from moveDataItem in source2
					let mfn = moveDataItem.Attribute("sourcefilename")
					let mt = moveDataItem.Attribute("customtable")
					let msnum = moveDataItem.Attribute("externalsnumcolname")
					let msourcetype = moveDataItem.Attribute("sourcefiletype")
					let margs = moveDataItem.Attribute("args")
					select new
					{
						<>h__TransparentIdentifier4 = <>h__TransparentIdentifier4,
						mhasoverride = (msourcetype != null && !string.IsNullOrEmpty(msourcetype.Value))
					}).Select(delegate(<>h__TransparentIdentifier5)
					{
						DataSyncMoveDataIntoClockWorkItem dataSyncMoveDataIntoClockWorkItem = new DataSyncMoveDataIntoClockWorkItem();
						dataSyncMoveDataIntoClockWorkItem.FullPathAndFilename = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.mfn == null) ? "" : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.mfn.Value ?? ""));
						dataSyncMoveDataIntoClockWorkItem.CustomTableNameWithoutCustomPrefix = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.mt == null) ? "" : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.mt.Value ?? ""));
						dataSyncMoveDataIntoClockWorkItem.StudentNumberExternalColumnName = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.msnum == null) ? "" : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.msnum.Value ?? ""));
						DataSyncMoveDataIntoClockWorkItem dataSyncMoveDataIntoClockWorkItem2 = dataSyncMoveDataIntoClockWorkItem;
						DataSyncMoveDataIntoClockWorkSourceFileInfo overrideSourceFileInfo;
						if (!<>h__TransparentIdentifier5.mhasoverride)
						{
							overrideSourceFileInfo = null;
						}
						else
						{
							DataSyncMoveDataIntoClockWorkSourceFileInfo dataSyncMoveDataIntoClockWorkSourceFileInfo2 = new DataSyncMoveDataIntoClockWorkSourceFileInfo();
							dataSyncMoveDataIntoClockWorkSourceFileInfo2.SourceFileType = ReportFunctionExecutionsAdapter.ConvertStringToMoveDataIntoClockWorkSourceFileType(<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.msourcetype.Value);
							dataSyncMoveDataIntoClockWorkSourceFileInfo2.Args = (from g in ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.margs == null) ? "" : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.margs.Value ?? "")).Split(new char[]
							{
								','
							})
							select g.Trim() into h
							where h.Length > 0
							select h).ToArray<string>();
							overrideSourceFileInfo = dataSyncMoveDataIntoClockWorkSourceFileInfo2;
						}
						dataSyncMoveDataIntoClockWorkItem2.OverrideSourceFileInfo = overrideSourceFileInfo;
						return dataSyncMoveDataIntoClockWorkItem;
					}).ToList<DataSyncMoveDataIntoClockWorkItem>();
					DataSyncMoveDataIntoClockWorkParameters dataSyncMoveDataIntoClockWorkParameters = new DataSyncMoveDataIntoClockWorkParameters();
					dataSyncMoveDataIntoClockWorkParameters.Items = items;
					DataSyncMoveDataIntoClockWorkParameters dataSyncMoveDataIntoClockWorkParameters2 = dataSyncMoveDataIntoClockWorkParameters;
					DataSyncMoveDataIntoClockWorkSourceFileInfo dataSyncMoveDataIntoClockWorkSourceFileInfo = new DataSyncMoveDataIntoClockWorkSourceFileInfo();
					dataSyncMoveDataIntoClockWorkSourceFileInfo.SourceFileType = ReportFunctionExecutionsAdapter.ConvertStringToMoveDataIntoClockWorkSourceFileType((xattribute == null) ? "" : (xattribute.Value ?? ""));
					dataSyncMoveDataIntoClockWorkSourceFileInfo.Args = (from g in ((xattribute2 == null) ? "" : (xattribute2.Value ?? "")).Split(new char[]
					{
						','
					})
					select g.Trim() into h
					where h.Length > 0
					select h).ToArray<string>();
					dataSyncMoveDataIntoClockWorkParameters2.SourceFileInfo = dataSyncMoveDataIntoClockWorkSourceFileInfo;
					result = dataSyncMoveDataIntoClockWorkParameters;
				}
				catch (Exception ex)
				{
					result = new DataSyncMoveDataIntoClockWorkParameters();
				}
			}
			return result;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x00038EDC File Offset: 0x000370DC
		private static eDataSyncMoveDataIntoClockWorkSourceFileType ConvertStringToMoveDataIntoClockWorkSourceFileType(string s)
		{
			int num;
			bool flag = string.IsNullOrEmpty(s) || !int.TryParse(s, out num) || num < 1 || !Enum.IsDefined(typeof(eDataSyncMoveDataIntoClockWorkSourceFileType), num);
			eDataSyncMoveDataIntoClockWorkSourceFileType result;
			if (flag)
			{
				result = eDataSyncMoveDataIntoClockWorkSourceFileType.Unknown;
			}
			else
			{
				result = (eDataSyncMoveDataIntoClockWorkSourceFileType)num;
			}
			return result;
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x00038F28 File Offset: 0x00037128
		public static string ConvertDataSyncMoveDataIntoClockWorkParametersToXml(this DataSyncMoveDataIntoClockWorkParameters parameters)
		{
			bool flag = parameters == null;
			if (flag)
			{
				parameters = new DataSyncMoveDataIntoClockWorkParameters();
			}
			bool flag2 = parameters.Items == null;
			if (flag2)
			{
				parameters.Items = new List<DataSyncMoveDataIntoClockWorkItem>();
			}
			XName name = "movedataparameters";
			XName name2 = "movedata";
			object[] array = new object[3];
			array[0] = new XAttribute("sourcefiletype", (parameters.SourceFileInfo == null) ? "" : ((int)parameters.SourceFileInfo.SourceFileType).ToString());
			array[1] = new XAttribute("externalsnumcolname", (parameters.SourceFileInfo == null || parameters.SourceFileInfo.Args == null) ? "" : string.Join(",", parameters.SourceFileInfo.Args));
			array[2] = (from item in parameters.Items
			select new XElement("movedataitem", new object[]
			{
				new XAttribute("sourcefilename", item.FullPathAndFilename ?? ""),
				new XAttribute("customtable", item.CustomTableNameWithoutCustomPrefix ?? ""),
				new XAttribute("externalsnumcolname", item.StudentNumberExternalColumnName ?? ""),
				new XAttribute("sourcefiletype", (item.OverrideSourceFileInfo == null) ? "" : ((int)item.OverrideSourceFileInfo.SourceFileType).ToString()),
				new XAttribute("args", (item.OverrideSourceFileInfo == null || item.OverrideSourceFileInfo.Args == null) ? "" : string.Join(",", item.OverrideSourceFileInfo.Args))
			})).ToArray<XElement>();
			XElement xelement = new XElement(name, new XElement(name2, array));
			return xelement.ToString();
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x00039038 File Offset: 0x00037238
		private static string GetStringFromElementValue(XElement element)
		{
			return (element == null || element.Value == null) ? string.Empty : element.Value;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x00039064 File Offset: 0x00037264
		public static DataSyncBatchParameters ConvertXmlToDataSyncBatchParameters(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			DataSyncBatchParameters result;
			if (flag)
			{
				result = new DataSyncBatchParameters();
			}
			else
			{
				bool flag2 = !xml.Trim().StartsWith("<") && xml.IndexOf(",") >= 0;
				if (flag2)
				{
					int num = xml.IndexOf(",");
					int overrideImportStudentDataReportId;
					int.TryParse(xml.Substring(0, num), out overrideImportStudentDataReportId);
					int overrideImportStudentCoursesReportId;
					int.TryParse(xml.Substring(num + 1), out overrideImportStudentCoursesReportId);
					result = new DataSyncBatchParameters
					{
						OverrideImportStudentDataReportId = overrideImportStudentDataReportId,
						OverrideImportStudentCoursesReportId = overrideImportStudentCoursesReportId
					};
				}
				else
				{
					try
					{
						XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
						bool flag3 = xdocument == null;
						if (flag3)
						{
							result = new DataSyncBatchParameters();
						}
						else
						{
							IEnumerable<XElement> source = xdocument.Descendants("datasyncbatch");
							List<DataSyncBatchParameters> list = (from l in source
							let lOverrideDataRid = l.Element("overridedatarid")
							let lOverrideCoursesRid = l.Element("overridecoursesrid")
							let lUseSingleThread = l.Element("usesinglethread")
							let lLastDataSyncCid = l.Element("lastdatasynccid")
							let lAllowedMinutesToRun = l.Element("allowedminutestorun")
							select new DataSyncBatchParameters
							{
								OverrideImportStudentDataReportId = ((lOverrideDataRid != null) ? lOverrideDataRid.Value.GetIntFromString(0) : 0),
								OverrideImportStudentCoursesReportId = ((lOverrideCoursesRid != null) ? lOverrideCoursesRid.Value.GetIntFromString(0) : 0),
								UseSingleThread = (lUseSingleThread != null && lUseSingleThread.Value.GetBoolFromString()),
								LastDataSyncControlId = ((lLastDataSyncCid != null) ? lLastDataSyncCid.Value.GetIntFromString(0) : 0),
								AllowedTimeToRun = TimeSpan.FromMinutes((double)Math.Max(0, (lAllowedMinutesToRun != null) ? lAllowedMinutesToRun.Value.GetIntFromString(0) : 0))
							}).ToList<DataSyncBatchParameters>();
							result = ((list.Count > 0) ? list[0] : new DataSyncBatchParameters());
						}
					}
					catch (Exception ex)
					{
						result = new DataSyncBatchParameters();
					}
				}
			}
			return result;
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x00039254 File Offset: 0x00037454
		public static string ConvertDataSyncBatchParametersToXml(this DataSyncBatchParameters parameters)
		{
			bool flag = parameters == null;
			if (flag)
			{
				parameters = new DataSyncBatchParameters();
			}
			XName name = "datasyncbatchparameters";
			XName name2 = "datasyncbatch";
			object[] array = new object[5];
			array[0] = new XElement("overridedatarid", parameters.OverrideImportStudentDataReportId.ToString());
			array[1] = new XElement("overridecoursesrid", parameters.OverrideImportStudentCoursesReportId.ToString());
			array[2] = new XElement("usesinglethread", parameters.UseSingleThread ? "1" : "0");
			array[3] = new XElement("lastdatasynccid", parameters.LastDataSyncControlId.ToString());
			int num = 4;
			XName name3 = "allowedminutestorun";
			TimeSpan allowedTimeToRun = parameters.AllowedTimeToRun;
			array[num] = new XElement(name3, Convert.ToInt32(parameters.AllowedTimeToRun.TotalMinutes).ToString());
			XElement xelement = new XElement(name, new XElement(name2, array));
			return xelement.ToString();
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x00039360 File Offset: 0x00037560
		public static DataSyncExecuteRestWebServiceParameters ConvertXmlToDataSyncExecuteRestWebServiceParameters(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			DataSyncExecuteRestWebServiceParameters result;
			if (flag)
			{
				result = new DataSyncExecuteRestWebServiceParameters();
			}
			else
			{
				try
				{
					IEnumerable<XElement> source = xdocument.Descendants("executerestwebservice");
					bool flag2 = !source.Any<XElement>();
					if (flag2)
					{
						result = new DataSyncExecuteRestWebServiceParameters();
					}
					else
					{
						List<DataSyncExecuteRestWebServiceParameters> list = (from l in source
						let lreturnxml = l.Element("returnxml")
						let lrootnodename = l.Element("rootnodename")
						let lusername = l.Element("username")
						let lpassword = l.Element("password")
						let lpasswordsetting = l.Element("passwordsetting")
						let ldomain = l.Element("domain")
						let lurl = l.Element("url")
						let lparameternames = l.Element("parameternames")
						let lhashtype = l.Element("hashtype")
						let lhashsecret = l.Element("hashsecret")
						let lhashsecretsetting = l.Element("hashsecretencryptedsettingcode")
						select new
						{
							<>h__TransparentIdentifier10 = <>h__TransparentIdentifier10,
							lhashpnames = l.Element("hashparameternamesinorder")
						}).Select(delegate(<>h__TransparentIdentifier11)
						{
							DataSyncExecuteRestWebServiceParameters dataSyncExecuteRestWebServiceParameters = new DataSyncExecuteRestWebServiceParameters();
							dataSyncExecuteRestWebServiceParameters.ReturnXml = (<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.lreturnxml != null && (<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.lreturnxml.Value ?? "").GetBoolFromString());
							dataSyncExecuteRestWebServiceParameters.RootNodeName = ReportFunctionExecutionsAdapter.GetStringFromElementValue(<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.lrootnodename);
							dataSyncExecuteRestWebServiceParameters.Username = ReportFunctionExecutionsAdapter.GetStringFromElementValue(<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.lusername);
							dataSyncExecuteRestWebServiceParameters.Password = ReportFunctionExecutionsAdapter.GetStringFromElementValue(<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.lpassword);
							dataSyncExecuteRestWebServiceParameters.Domain = ReportFunctionExecutionsAdapter.GetStringFromElementValue(<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.ldomain);
							dataSyncExecuteRestWebServiceParameters.PasswordEncryptedSettingCode = ReportFunctionExecutionsAdapter.GetStringFromElementValue(<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.lpasswordsetting).GetNullableEnumFromStringWithEnumIntValue<Setting>();
							dataSyncExecuteRestWebServiceParameters.Url = ReportFunctionExecutionsAdapter.GetStringFromElementValue(<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.lurl);
							dataSyncExecuteRestWebServiceParameters.HashType = ReportFunctionExecutionsAdapter.GetStringFromElementValue(<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.lhashtype);
							dataSyncExecuteRestWebServiceParameters.HashSecret = ReportFunctionExecutionsAdapter.GetStringFromElementValue(<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.lhashsecret);
							dataSyncExecuteRestWebServiceParameters.HashSecretEncryptedSettingCode = ReportFunctionExecutionsAdapter.GetStringFromElementValue(<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.lhashsecretsetting).GetNullableEnumFromStringWithEnumIntValue<Setting>();
							string[] hashParameterNamesInOrder;
							if (<>h__TransparentIdentifier11.lhashpnames != null)
							{
								hashParameterNamesInOrder = (from m in <>h__TransparentIdentifier11.lhashpnames.Elements()
								select ReportFunctionExecutionsAdapter.GetStringFromElementValue(m)).ToArray<string>();
							}
							else
							{
								hashParameterNamesInOrder = null;
							}
							dataSyncExecuteRestWebServiceParameters.HashParameterNamesInOrder = hashParameterNamesInOrder;
							return dataSyncExecuteRestWebServiceParameters;
						}).ToList<DataSyncExecuteRestWebServiceParameters>();
						result = ((list.Count > 0) ? list[0] : new DataSyncExecuteRestWebServiceParameters());
					}
				}
				catch (Exception ex)
				{
					result = new DataSyncExecuteRestWebServiceParameters();
				}
			}
			return result;
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000395D8 File Offset: 0x000377D8
		public static string ConvertDataSyncExecuteRestWebServiceParametersToXml(this DataSyncExecuteRestWebServiceParameters parameters)
		{
			bool flag = parameters == null;
			if (flag)
			{
				parameters = new DataSyncExecuteRestWebServiceParameters();
			}
			XName name = "executerestwebserviceparameters";
			XName name2 = "executerestwebservice";
			object[] array = new object[11];
			array[0] = new XElement("returnxml", parameters.ReturnXml ? "1" : "0");
			array[1] = new XElement("rootnodename", (parameters.RootNodeName ?? "").Trim());
			array[2] = new XElement("username", (parameters.Username ?? "").Trim());
			array[3] = new XElement("password", (parameters.Password ?? "").Trim());
			array[4] = new XElement("domain", (parameters.Domain ?? "").Trim());
			array[5] = new XElement("passwordsetting", (parameters.PasswordEncryptedSettingCode != null) ? ((int)parameters.PasswordEncryptedSettingCode.Value).ToString() : "");
			array[6] = new XElement("url", (parameters.Url ?? "").Trim());
			array[7] = new XElement("hashtype", (parameters.HashType ?? "").Trim());
			array[8] = new XElement("hashsecret", (parameters.HashSecret ?? "").Trim());
			array[9] = new XElement("hashsecretencryptedsettingcode", (parameters.HashSecretEncryptedSettingCode != null) ? ((int)parameters.HashSecretEncryptedSettingCode.Value).ToString() : "");
			array[10] = new XElement("hashparameternamesinorder", from g in parameters.HashParameterNamesInOrder ?? new string[0]
			select new XElement("hashpname", (g ?? "").Trim()));
			XElement xelement = new XElement(name, new XElement(name2, array));
			return xelement.ToString();
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x00039820 File Offset: 0x00037A20
		public static DataSyncLoadDataFromClockWorkParameters ConvertXmlToDataSyncLoadDataFromClockWorkParameters(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			DataSyncLoadDataFromClockWorkParameters result;
			if (flag)
			{
				result = new DataSyncLoadDataFromClockWorkParameters();
			}
			else
			{
				try
				{
					IEnumerable<XElement> source = xdocument.Descendants("loadcustomdata");
					bool flag2 = !source.Any<XElement>();
					if (flag2)
					{
						result = new DataSyncLoadDataFromClockWorkParameters();
					}
					else
					{
						List<DataSyncLoadDataFromClockWorkParameters> list = (from l in source
						let ltype = l.Element("type")
						let ltn = l.Element("customtablenamenoprefix")
						let lext = l.Element("externallookupcolname")
						let lsql = l.Element("overridesql")
						let lcols = l.Element("columnstoreturn")
						select new
						{
							<>h__TransparentIdentifier4 = <>h__TransparentIdentifier4,
							lpp = l.Element("lookupfieldparameters")
						}).Select(delegate(<>h__TransparentIdentifier5)
						{
							DataSyncLoadDataFromClockWorkParameters dataSyncLoadDataFromClockWorkParameters = new DataSyncLoadDataFromClockWorkParameters();
							dataSyncLoadDataFromClockWorkParameters.LoadDataType = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.ltype == null || <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.ltype.Value == null) ? eDataSyncLoadDataFromClockWorkParametersType.SingleTable : <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.ltype.Value.GetEnumFromStringWithEnumIntValue(eDataSyncLoadDataFromClockWorkParametersType.SingleTable));
							dataSyncLoadDataFromClockWorkParameters.CustomTableNameWithoutCustomPrefix = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.ltn == null || <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.ltn.Value == null) ? "" : <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.ltn.Value.Trim());
							dataSyncLoadDataFromClockWorkParameters.LookupExternalColumnName = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.lext == null || <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.lext.Value == null) ? "" : <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.lext.Value.Trim());
							dataSyncLoadDataFromClockWorkParameters.OverrideSql = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.lsql == null || <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.lsql.Value == null) ? "" : <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.lsql.Value.Trim());
							dataSyncLoadDataFromClockWorkParameters.LookupFieldParameterNames = (from p in lpp.Elements()
							let ppname = p.Attribute("pname")
							select (ppname == null || ppname.Value == null) ? "" : ppname.Value.Trim() into n
							where n.Length > 0
							select n).ToArray<string>();
							dataSyncLoadDataFromClockWorkParameters.ExternalColumnNamesToReturn = (from r in lcols.Elements()
							let rcol = r.Attribute("extcolname")
							select (rcol == null || rcol.Value == null) ? "" : rcol.Value.Trim() into m
							where m.Length > 0
							select m).ToArray<string>();
							return dataSyncLoadDataFromClockWorkParameters;
						}).ToList<DataSyncLoadDataFromClockWorkParameters>();
						result = ((list.Count > 0) ? list[0] : new DataSyncLoadDataFromClockWorkParameters());
					}
				}
				catch (Exception ex)
				{
					result = new DataSyncLoadDataFromClockWorkParameters();
				}
			}
			return result;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000399C0 File Offset: 0x00037BC0
		public static string ConvertDataSyncLoadDataFromClockWorkParametersToXml(this DataSyncLoadDataFromClockWorkParameters parameters)
		{
			bool flag = parameters == null;
			if (flag)
			{
				parameters = new DataSyncLoadDataFromClockWorkParameters();
			}
			XName name = "loadcustomdataparameters";
			XName name2 = "loadcustomdata";
			object[] array = new object[6];
			array[0] = new XElement("type", ((int)parameters.LoadDataType).ToString());
			array[1] = new XElement("customtablenamenoprefix", parameters.CustomTableNameWithoutCustomPrefix ?? "");
			array[2] = new XElement("externallookupcolname", parameters.LookupExternalColumnName ?? "");
			array[3] = new XElement("overridesql", parameters.OverrideSql ?? "");
			array[4] = new XElement("lookupfieldparameters", from g in parameters.LookupFieldParameterNames ?? new string[0]
			select new XElement("lookupfieldparameter", new XAttribute("pname", g.Trim())));
			array[5] = new XElement("columnstoreturn", from g in parameters.ExternalColumnNamesToReturn ?? new string[0]
			select new XElement("col", new XAttribute("extcolname", (g ?? "").Trim())));
			XElement xelement = new XElement(name, new XElement(name2, array));
			return xelement.ToString();
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x00039B1C File Offset: 0x00037D1C
		public static DataSyncFixAddressesForNotetakingParameters ConvertXmlToDataSyncFixAddressesForNotetakingParameters(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			DataSyncFixAddressesForNotetakingParameters result;
			if (flag)
			{
				result = new DataSyncFixAddressesForNotetakingParameters();
			}
			else
			{
				try
				{
					XElement xelement = xdocument.Descendants("localaddresslabel").FirstOrDefault<XElement>();
					XElement xelement2 = xdocument.Descendants("permaddresslabel").FirstOrDefault<XElement>();
					result = new DataSyncFixAddressesForNotetakingParameters
					{
						LocalAddressLabel = ((xelement == null) ? "" : (xelement.Value ?? "")).Replace("\r\n", "\n").Replace("\n", "\r\n"),
						PermAddressLabel = ((xelement2 == null) ? "" : (xelement2.Value ?? "")).Replace("\r\n", "\n").Replace("\n", "\r\n")
					};
				}
				catch (Exception ex)
				{
					result = new DataSyncFixAddressesForNotetakingParameters();
				}
			}
			return result;
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x00039C18 File Offset: 0x00037E18
		public static string ConvertDataSyncFixAddressesForNotetakingParametersToXml(this DataSyncFixAddressesForNotetakingParameters parameters)
		{
			bool flag = parameters == null;
			if (flag)
			{
				parameters = new DataSyncFixAddressesForNotetakingParameters();
			}
			XElement xelement = new XElement("fixaddressesparameters", new object[]
			{
				new XElement("localaddresslabel", parameters.LocalAddressLabel ?? ""),
				new XElement("permaddresslabel", parameters.PermAddressLabel ?? "")
			});
			return xelement.ToString();
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x00039C98 File Offset: 0x00037E98
		public static LoadDynamicDataOptions ConvertXmlToLoadDynamicDataOptions(this string xml)
		{
			XDocument xdocument = ReportFunctionExecutionsAdapter.XDocumentParse(xml);
			bool flag = xdocument == null;
			LoadDynamicDataOptions result;
			if (flag)
			{
				result = new LoadDynamicDataOptions();
			}
			else
			{
				try
				{
					List<XAttribute> source = xdocument.Descendants("dynamicdataoptions").Attributes().ToList<XAttribute>();
					XAttribute xattribute = source.FirstOrDefault((XAttribute g) => g.Name == "sql");
					XAttribute xattribute2 = source.FirstOrDefault((XAttribute g) => g.Name == "cids");
					XAttribute xattribute3 = source.FirstOrDefault((XAttribute g) => g.Name == "screennum");
					string text = (xattribute3 == null) ? "" : (xattribute3.Value ?? "");
					int screenNum;
					bool flag2 = string.IsNullOrEmpty(text) || !int.TryParse(text, out screenNum);
					if (flag2)
					{
						screenNum = 0;
					}
					string text2 = (xattribute2 == null) ? "" : (xattribute2.Value ?? "");
					List<int> controlIds = (from h in text2.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries).Select(delegate(string g)
					{
						string s = g.Trim();
						int num;
						return int.TryParse(s, out num) ? num : 0;
					})
					where h > 0
					select h).ToList<int>();
					result = new LoadDynamicDataOptions
					{
						SqlQuery = ((xattribute == null) ? "" : (xattribute.Value ?? "")),
						ControlIds = controlIds,
						ScreenNum = screenNum
					};
				}
				catch (Exception ex)
				{
					result = new LoadDynamicDataOptions();
				}
			}
			return result;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x00039E74 File Offset: 0x00038074
		public static string ConvertLoadDynamicDataOptionsToXml(this LoadDynamicDataOptions loadDataOptions)
		{
			bool flag = loadDataOptions == null;
			if (flag)
			{
				loadDataOptions = new LoadDynamicDataOptions();
			}
			XName name = "dynamicdataoptionsparameters";
			XName name2 = "dynamicdataoptions";
			object[] array = new object[3];
			array[0] = new XAttribute("sql", loadDataOptions.SqlQuery ?? "");
			int num = 1;
			XName name3 = "cids";
			object value;
			if (loadDataOptions.ControlIds != null)
			{
				value = string.Join(",", (from g in loadDataOptions.ControlIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = "";
			}
			array[num] = new XAttribute(name3, value);
			array[2] = new XAttribute("screennum", loadDataOptions.ScreenNum.ToString());
			XElement xelement = new XElement(name, new XElement(name2, array));
			return xelement.ToString();
		}
	}
}
