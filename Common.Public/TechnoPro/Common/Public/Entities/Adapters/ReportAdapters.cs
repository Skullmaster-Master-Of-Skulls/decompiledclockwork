using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.Serialization;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005DE RID: 1502
	public static class ReportAdapters
	{
		// Token: 0x0600304B RID: 12363 RVA: 0x0003E5DC File Offset: 0x0003C7DC
		private static void AddGroupsAndReportsToForest(List<ReportAdapters.Node> groupNodes, TreeNode<ReportOrGroup> currentParentNode, ref Forest<ReportOrGroup> forest, ReportCollection reportCollection)
		{
			using (List<ReportAdapters.Node>.Enumerator enumerator = groupNodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ReportAdapters.Node groupNode = enumerator.Current;
					ReportGroup group = reportCollection.ReportGroups.FirstOrDefault((ReportGroup g) => g.GroupId == groupNode.Id);
					TreeNode<ReportOrGroup> treeNode = forest.AppendNode(currentParentNode, new ReportOrGroup
					{
						Group = group
					});
					bool flag = groupNode.Children.Count > 0;
					if (flag)
					{
						ReportAdapters.AddGroupsAndReportsToForest(groupNode.Children, treeNode, ref forest, reportCollection);
					}
					List<Report> list = (from r in reportCollection.Reports
					where r.GroupId == groupNode.Id
					select r).ToList<Report>();
					foreach (Report report in list)
					{
						forest.AppendNode(treeNode, new ReportOrGroup
						{
							Report = report
						});
					}
				}
			}
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x0003E704 File Offset: 0x0003C904
		private static List<ReportAdapters.Node> MakeTreeFromFlatList(IList<ReportGroup> reportGroups)
		{
			List<ReportAdapters.Node> list = new List<ReportAdapters.Node>();
			foreach (ReportGroup reportGroup in reportGroups)
			{
				list.Add(new ReportAdapters.Node(reportGroup.GroupId, reportGroup.ParentGroupId));
			}
			return ReportAdapters.MakeTreeFromFlatList(list);
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x0003E770 File Offset: 0x0003C970
		private static List<ReportAdapters.Node> MakeTreeFromFlatList(IEnumerable<ReportAdapters.Node> flatList)
		{
			Dictionary<int, ReportAdapters.Node> dictionary = flatList.ToDictionary((ReportAdapters.Node n) => n.Id, (ReportAdapters.Node n) => n);
			List<ReportAdapters.Node> list = new List<ReportAdapters.Node>();
			foreach (ReportAdapters.Node node in flatList)
			{
				bool flag = node.ParentId != null && dictionary.ContainsKey(node.ParentId.Value);
				if (flag)
				{
					ReportAdapters.Node node2 = dictionary[node.ParentId.Value];
					node.Parent = node2;
					node2.Children.Add(node);
				}
				else
				{
					list.Add(node);
				}
			}
			return list;
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x0003E874 File Offset: 0x0003CA74
		private static int GetIntFromAttribute(XElement element, string attributeName, int defaultValue = 0)
		{
			bool flag = element == null;
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = xattribute == null || string.IsNullOrEmpty(xattribute.Value);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					int num;
					bool flag3 = !int.TryParse(xattribute.Value, out num);
					if (flag3)
					{
						result = defaultValue;
					}
					else
					{
						result = num;
					}
				}
			}
			return result;
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x0003E8D8 File Offset: 0x0003CAD8
		private static string GetStringFromAttribute(XElement element, string attributeName, string defaultValue = "")
		{
			bool flag = element == null;
			string result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = xattribute == null;
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					result = (xattribute.Value ?? defaultValue);
				}
			}
			return result;
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x0003E91C File Offset: 0x0003CB1C
		private static T GetEnumFromIntAttribute<T>(XElement element, string attributeName)
		{
			int intFromAttribute = ReportAdapters.GetIntFromAttribute(element, attributeName, 0);
			bool flag = intFromAttribute < 1;
			if (flag)
			{
				string stringFromAttribute = ReportAdapters.GetStringFromAttribute(element, attributeName, "");
				bool flag2 = !string.IsNullOrEmpty(stringFromAttribute) && Enum.IsDefined(typeof(T), stringFromAttribute);
				if (flag2)
				{
					return (T)((object)Enum.Parse(typeof(T), stringFromAttribute));
				}
			}
			bool flag3 = !Enum.IsDefined(typeof(T), intFromAttribute);
			T result;
			if (flag3)
			{
				result = default(T);
			}
			else
			{
				result = (T)((object)Enum.Parse(typeof(T), intFromAttribute.ToString()));
			}
			return result;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x0003E9D0 File Offset: 0x0003CBD0
		private static T GetEnumFromStringAttribute<T>(XElement element, string attributeName)
		{
			string stringFromAttribute = ReportAdapters.GetStringFromAttribute(element, attributeName, "");
			bool flag = !Enum.IsDefined(typeof(T), stringFromAttribute);
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				result = (T)((object)Enum.Parse(typeof(T), stringFromAttribute));
			}
			return result;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x0003EA28 File Offset: 0x0003CC28
		public static Forest<ReportOrGroup> BuildReportForest(this ReportCollection reportCollection)
		{
			List<Report> list = (from g in reportCollection.Reports
			where reportCollection.ReportGroups.All((ReportGroup h) => h.GroupId != g.GroupId)
			select g).ToList<Report>();
			List<ReportAdapters.Node> groupNodes = ReportAdapters.MakeTreeFromFlatList(reportCollection.ReportGroups);
			Forest<ReportOrGroup> forest = new Forest<ReportOrGroup>();
			ReportAdapters.AddGroupsAndReportsToForest(groupNodes, null, ref forest, reportCollection);
			foreach (Report report in list)
			{
				forest.AppendNode(null, new ReportOrGroup
				{
					Report = report
				});
			}
			return forest;
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x0003EAE8 File Offset: 0x0003CCE8
		private static byte[] ConvertBase64StringToBytes(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					return Convert.FromBase64String(s);
				}
				catch
				{
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x0003EB2C File Offset: 0x0003CD2C
		public static ReportCollection ParseReportsFromNewXml(this string reportsXml, bool LoadReportFunctions)
		{
			XDocument xdocument = XDocument.Parse(reportsXml);
			bool loadReportFunctions = LoadReportFunctions;
			IList<FormattedReport> formattedReports;
			if (loadReportFunctions)
			{
				formattedReports = (from g in xdocument.Root.Elements("formattedreports").Elements("formattedreport")
				select new FormattedReport
				{
					ReportFileId = ReportAdapters.GetIntFromAttribute(g, "reportfileid", 0),
					Description = ((g.Attribute("description") == null) ? "" : ((string)g.Attribute("description"))),
					Title = ((g.Attribute("title") == null) ? "" : ((string)g.Attribute("title"))),
					FormattedReportTemplate = ((g.Attribute("template") == null) ? null : Convert.FromBase64String((string)g.Attribute("template")))
				}).ToList<FormattedReport>();
			}
			else
			{
				formattedReports = new List<FormattedReport>();
			}
			List<ReportGroup> reportGroups = (from g in xdocument.Root.Elements("reportgroups").Elements("reportgroup")
			select new ReportGroup
			{
				GroupId = ReportAdapters.GetIntFromAttribute(g, "groupid", 0),
				Description = (string)g.Attribute("groupdescription"),
				IsTechnoProGroup = true,
				ParentGroupId = ((g.Attribute("parentgroupid") == null) ? 0 : ((int)g.Attribute("parentgroupid"))),
				Title = (string)g.Attribute("grouptitle"),
				OrderNum = ReportAdapters.GetIntFromAttribute(g, "ordernum", 0)
			}).ToList<ReportGroup>();
			IEnumerable<XElement> enumerable = xdocument.Root.Elements("reports");
			int functionOrderNumCtr = 0;
			bool flag = enumerable != null;
			IList<Report> list;
			if (flag)
			{
				list = (from r in enumerable.Elements("report")
				let builtByTproElement = r.Element("builtbytprosignedandencryptedreportxml")
				select new
				{
					<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
					builtByTpro = ((builtByTproElement == null) ? null : ReportAdapters.ConvertBase64StringToBytes(builtByTproElement.Value))
				}).Select(delegate(<>h__TransparentIdentifier1)
				{
					Report report2 = new Report();
					report2.ReportId = ReportAdapters.GetIntFromAttribute(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r, "reportid", 0);
					report2.IsTechnoProReport = true;
					report2.Title = ((<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("title") == null) ? "Unknown" : (((string)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("title")) ?? "Unknown"));
					report2.Description = ((<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("description") == null) ? "Unknown" : (((string)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("description")) ?? "Unknown"));
					report2.DateCreated = ((<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("datecreated") == null) ? DateTime.Now : ((DateTime)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("datecreated")));
					PersonBase whoCreated;
					if (<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("whocreatedpid") != null && (int)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("whocreatedpid") >= 1)
					{
						(whoCreated = new PersonBase()).PersonId = (int)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("whocreatedpid");
					}
					else
					{
						whoCreated = null;
					}
					report2.WhoCreated = whoCreated;
					report2.DateLastExecuted = ((<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("datelastexecuted") == null) ? DateTime.Now : ((DateTime)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("datelastexecuted")));
					PersonBase whoLastExecuted;
					if (ReportAdapters.GetIntFromAttribute(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r, "wholastexecutedpid", 0) >= 1)
					{
						(whoLastExecuted = new PersonBase()).PersonId = ReportAdapters.GetIntFromAttribute(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r, "wholastexecutedpid", 0);
					}
					else
					{
						whoLastExecuted = null;
					}
					report2.WhoLastExecuted = whoLastExecuted;
					report2.DateLastModified = ((<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("datelastmodified") == null) ? DateTime.Now : ((DateTime)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("datelastmodified")));
					PersonBase whoLastModified;
					if (ReportAdapters.GetIntFromAttribute(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r, "wholastmodifiedpid", 0) >= 1)
					{
						(whoLastModified = new PersonBase()).PersonId = ReportAdapters.GetIntFromAttribute(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r, "wholastmodifiedpid", 0);
					}
					else
					{
						whoLastModified = null;
					}
					report2.WhoLastModified = whoLastModified;
					report2.FunctionParametersAreEncrypted = (<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("functionparametersareencrypted") != null && (bool)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("functionparametersareencrypted"));
					report2.FormattedReports = (from q in <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Elements("formattedreportids").Elements("formattedreportid")
					select new FormattedReport
					{
						ReportFileId = ReportAdapters.GetIntFromAttribute(q, "reportfileid", 0)
					}).ToList<FormattedReport>();
					List<ReportFunction> functions;
					if (!LoadReportFunctions)
					{
						functions = new List<ReportFunction>();
					}
					else
					{
						functions = (from f in <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Elements("functions").Elements("function")
						select new
						{
							f = f,
							fRunOnClient = f.Attribute("ExecuteThisFunctionOnClientIfPossible")
						}).Select(delegate(<>h__TransparentIdentifier0)
						{
							ReportFunction reportFunction = new ReportFunction();
							reportFunction.FunctionCode = ReportAdapters.GetEnumFromIntAttribute<eFunctionType>(<>h__TransparentIdentifier0.f, "functioncode");
							reportFunction.Description = (string)<>h__TransparentIdentifier0.f.Attribute("description");
							reportFunction.ExampleUsage = (string)<>h__TransparentIdentifier0.f.Attribute("exampleusage");
							reportFunction.FunctionParameters = (from p in <>h__TransparentIdentifier0.f.Elements("functionparameters").Elements("functionparameter")
							select new ReportParameter
							{
								Name = (string)p.Attribute("name"),
								Value = ((p.Attribute("value") == null) ? "" : ((string)p.Attribute("value")))
							}).ToList<ReportParameter>();
							reportFunction.ReportFunctionId = ReportAdapters.GetIntFromAttribute(<>h__TransparentIdentifier0.f, "reportfunctionid", 0);
							int orderNum;
							if (ReportAdapters.GetIntFromAttribute(<>h__TransparentIdentifier0.f, "ordernum", 0) <= 0)
							{
								int functionOrderNumCtr = functionOrderNumCtr;
								functionOrderNumCtr++;
								orderNum = functionOrderNumCtr;
							}
							else
							{
								orderNum = ReportAdapters.GetIntFromAttribute(<>h__TransparentIdentifier0.f, "ordernum", 0);
							}
							reportFunction.OrderNum = orderNum;
							reportFunction.Title = (string)<>h__TransparentIdentifier0.f.Attribute("title");
							reportFunction.ExecuteThisFunctionOnClientIfPossible = (<>h__TransparentIdentifier0.fRunOnClient != null && !string.IsNullOrEmpty(<>h__TransparentIdentifier0.fRunOnClient.Value) && "1yestrue".IndexOf(<>h__TransparentIdentifier0.fRunOnClient.Value, StringComparison.OrdinalIgnoreCase) >= 0);
							return reportFunction;
						}).ToList<ReportFunction>();
					}
					report2.Functions = functions;
					report2.GroupId = ReportAdapters.GetIntFromAttribute(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r, "groupid", 0);
					report2.LegacyParameters = new ReportParametersLegacy
					{
						BuiltInDynamicForm = ReportAdapters.GetEnumFromIntAttribute<eReportBuiltInDynamicForm>(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r, "dynamiccontrolsscreennum")
					};
					report2.ParameterForm = new ReportParameterForm();
					report2.ReportOptions = ((<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("reportoptions") == null) ? null : (((string)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("reportoptions")) ?? "").ConvertFromStringStoredInXml().GetReportOptionsFromXml());
					report2.BuiltByTproSignedAndEncryptedReportXml = <>h__TransparentIdentifier1.builtByTpro;
					report2.CreatedByLocation = ((<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("createdbylocation") == null) ? "" : ((string)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("createdbylocation")));
					report2.ReportUniqueId = ReportAdapters.GetGuidFromString((<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("reportuniqueid") == null) ? "" : ((string)<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.r.Attribute("reportuniqueid")));
					report2.IsBuiltByTpro = (<>h__TransparentIdentifier1.builtByTpro != null && <>h__TransparentIdentifier1.builtByTpro.Length != 0);
					return report2;
				}).ToList<Report>();
			}
			else
			{
				list = new List<Report>();
			}
			Func<FormattedReport, FormattedReport> <>9__10;
			foreach (Report report in list)
			{
				bool flag2 = report.FormattedReports != null;
				if (flag2)
				{
					IEnumerable<FormattedReport> source = from fr in report.FormattedReports
					where fr.ReportFileId > 0
					select fr;
					Func<FormattedReport, FormattedReport> selector;
					if ((selector = <>9__10) == null)
					{
						selector = (<>9__10 = ((FormattedReport fr) => formattedReports.FirstOrDefault((FormattedReport f) => f.ReportFileId == fr.ReportFileId)));
					}
					List<FormattedReport> formattedReports2 = (from foundFr in source.Select(selector)
					where foundFr != null
					select foundFr).ToList<FormattedReport>();
					report.FormattedReports = formattedReports2;
				}
			}
			ReportAdapters.FixTproReportCollectionIds(list, reportGroups, formattedReports);
			return new ReportCollection
			{
				Reports = list,
				ReportGroups = reportGroups
			};
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x0003EDB8 File Offset: 0x0003CFB8
		public static Guid GetGuidFromString(string s)
		{
			try
			{
				bool flag = !string.IsNullOrEmpty(s);
				if (flag)
				{
					return new Guid(s);
				}
			}
			catch
			{
			}
			return default(Guid);
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x0003EE04 File Offset: 0x0003D004
		public static string ConvertToXml(this ReportOptions reportOptions)
		{
			bool flag = reportOptions == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = reportOptions.ColumnsToHide == null;
				if (flag2)
				{
					reportOptions.ColumnsToHide = new List<string>();
				}
				bool flag3 = reportOptions.ColumnFormattingRules == null;
				if (flag3)
				{
					reportOptions.ColumnFormattingRules = new List<ColumnFormattingRule>();
				}
				bool flag4 = reportOptions.TableSortingRule == null;
				if (flag4)
				{
					reportOptions.TableSortingRule = new List<ColumnSortingRule>();
				}
				XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
				object[] array = new object[1];
				int num = 0;
				XName name = "reportoptions";
				object[] array2 = new object[5];
				array2[0] = new XElement("basicoptions", new XElement("basicoption", new object[]
				{
					new XAttribute("dontshowreportrunresults", reportOptions.DontShowReportRunResults.ToString()),
					new XAttribute("notetouser", reportOptions.NoteToUser ?? "")
				}));
				array2[1] = new XElement("columnstohide", from r in reportOptions.ColumnsToHide
				select new XElement("columntohide", new XAttribute("columnname", r ?? "")));
				array2[2] = new XElement("formattingrules", from fr in reportOptions.ColumnFormattingRules
				select new XElement("formattingrule", new object[]
				{
					new XAttribute("columnname", fr.ColumnName ?? ""),
					new XAttribute("formattingstring", fr.FormattingString ?? "")
				}));
				array2[3] = new XElement("sortingrules", from sr in reportOptions.TableSortingRule
				select new XElement("sortingrule", new object[]
				{
					new XAttribute("columnname", sr.ColumnName ?? ""),
					new XAttribute("sortascending", (!sr.SortDescending).ToString())
				}));
				int num2 = 4;
				XName name2 = "rowformattings";
				object[] array3 = new object[2];
				array3[0] = from rf in reportOptions.RowFormattings
				select new XElement("rowformatting", new object[]
				{
					new XAttribute("columnname", rf.ColumnName ?? ""),
					new XAttribute("conditiontype", ReportAdapters.ConvertToString(rf.ConditionType)),
					new XAttribute("conditionvalue", rf.ConditionValue ?? ""),
					new XAttribute("backcolour", rf.BackColourArgB),
					new XAttribute("forecolour", rf.ForeColourArgB),
					new XAttribute("applytorow", rf.ApplyToRow)
				});
				array3[1] = new XElement("groupingcolumns", from g in reportOptions.GroupingColumns
				select new XElement("groupingcolumn", new XAttribute("columnname", g ?? "")));
				array2[num2] = new XElement(name2, array3);
				array[num] = new XElement(name, array2);
				XDocument xdocument = new XDocument(declaration, array);
				result = xdocument.ToString();
			}
			return result;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x0003F054 File Offset: 0x0003D254
		public static string ConvertToStringForStorageInXml(this string xml)
		{
			bool flag = xml == null || xml.Trim().Length < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				try
				{
					byte[] bytes = Encoding.UTF8.GetBytes(xml);
					return Convert.ToBase64String(bytes);
				}
				catch (Exception ex)
				{
				}
				result = "";
			}
			return result;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x0003F0B8 File Offset: 0x0003D2B8
		public static string ConvertFromStringStoredInXml(this string storedString)
		{
			bool flag = storedString == null || storedString.Trim().Length < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				try
				{
					byte[] bytes = Convert.FromBase64String(storedString);
					return Encoding.UTF8.GetString(bytes);
				}
				catch (Exception ex)
				{
				}
				result = "";
			}
			return result;
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x0003F11C File Offset: 0x0003D31C
		public static ReportOptions GetReportOptionsFromXml(this string xml)
		{
			bool flag = xml == null || xml.Trim().Length < 1;
			ReportOptions result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XDocument xdocument;
				try
				{
					xdocument = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + xml);
				}
				catch (Exception ex)
				{
					return null;
				}
				ReportOptions reportOptions = new ReportOptions();
				try
				{
					bool dontShowReportResults;
					List<ReportAdapters.ReportBasicOption> list = (from g in xdocument.Root.Elements("basicoptions").Elements("basicoption")
					let gDontShowReportResults = (g.Attribute("dontshowreportrunresults") == null) ? "" : (g.Attribute("dontshowreportrunresults").Value ?? "")
					select new ReportAdapters.ReportBasicOption
					{
						NoteToUser = ((g.Attribute("notetouser") == null) ? "" : ((string)g.Attribute("notetouser")).Trim()),
						DontShowReportResults = ((gDontShowReportResults.Length > 0 && bool.TryParse(gDontShowReportResults, out dontShowReportResults)) & dontShowReportResults)
					}).ToList<ReportAdapters.ReportBasicOption>();
					bool flag2 = list.Count > 0;
					if (flag2)
					{
						reportOptions.DontShowReportRunResults = list[0].DontShowReportResults;
						reportOptions.NoteToUser = list[0].NoteToUser;
					}
				}
				catch (Exception ex2)
				{
				}
				try
				{
					reportOptions.ColumnsToHide = (from g in xdocument.Root.Elements("columnstohide").Elements("columntohide")
					select new ReportAdapters.ReportOptionColumnName
					{
						ColumnName = ((g.Attribute("columnname") == null) ? "" : ((string)g.Attribute("columnname")).Trim())
					} into h
					where !string.IsNullOrEmpty(h.ColumnName)
					select h).ToList<ReportAdapters.ReportOptionColumnName>().ConvertAll<string>((ReportAdapters.ReportOptionColumnName g) => g.ColumnName);
				}
				catch (Exception ex3)
				{
				}
				try
				{
					reportOptions.ColumnFormattingRules = (from g in xdocument.Root.Elements("formattingrules").Elements("formattingrule")
					select new ColumnFormattingRule
					{
						ColumnName = ((g.Attribute("columnname") == null) ? "" : ((string)g.Attribute("columnname")).Trim()),
						FormattingString = ((g.Attribute("formattingstring") == null) ? "" : ((string)g.Attribute("formattingstring")).Trim())
					} into h
					where !string.IsNullOrEmpty(h.ColumnName) && !string.IsNullOrEmpty(h.FormattingString)
					select h).ToList<ColumnFormattingRule>();
				}
				catch (Exception ex4)
				{
				}
				try
				{
					bool gSortAscendingBool;
					reportOptions.TableSortingRule = (from g in xdocument.Root.Elements("sortingrules").Elements("sortingrule")
					let gSortAscending = (g.Attribute("sortascending") == null) ? "" : (g.Attribute("sortascending").Value ?? "")
					select new ColumnSortingRule
					{
						ColumnName = ((g.Attribute("columnname") == null) ? "" : ((string)g.Attribute("columnname")).Trim()),
						SortDescending = (gSortAscending.Length > 0 && bool.TryParse(gSortAscending, out gSortAscendingBool) && !gSortAscendingBool)
					} into h
					where !string.IsNullOrEmpty(h.ColumnName)
					select h).ToList<ColumnSortingRule>();
				}
				catch (Exception ex5)
				{
				}
				try
				{
					reportOptions.RowFormattings = (from h in xdocument.Root.Elements("rowformattings").Elements("rowformatting").Select(delegate(XElement g)
					{
						RowFormatting rowFormatting = new RowFormatting();
						rowFormatting.ColumnName = ((g.Attribute("columnname") == null) ? "" : ((string)g.Attribute("columnname")).Trim());
						rowFormatting.ConditionType = ReportAdapters.GetEnumFromStringAttribute<eRowFormattingConditionType>(g, "conditiontype");
						rowFormatting.ConditionValue = ((g.Attribute("conditionvalue") == null) ? "" : ((string)g.Attribute("conditionvalue")));
						rowFormatting.BackColourArgB = ReportAdapters.GetIntFromAttribute(g, "backcolour", 0);
						rowFormatting.ForeColourArgB = ReportAdapters.GetIntFromAttribute(g, "forecolour", 0);
						if (g.Attribute("applytorow") != null)
						{
							g.Attribute("applytorow");
						}
						rowFormatting.ApplyToRow = false;
						return rowFormatting;
					})
					where !string.IsNullOrEmpty(h.ColumnName)
					select h).ToList<RowFormatting>();
				}
				catch (Exception ex6)
				{
				}
				try
				{
					reportOptions.GroupingColumns = (from g in xdocument.Root.Elements("groupingcolumns").Elements("groupingcolumn")
					select new ReportAdapters.ReportOptionColumnName
					{
						ColumnName = ((g.Attribute("columnname") == null) ? "" : ((string)g.Attribute("columnname")).Trim())
					} into h
					where !string.IsNullOrEmpty(h.ColumnName)
					select h).ToList<ReportAdapters.ReportOptionColumnName>().ConvertAll<string>((ReportAdapters.ReportOptionColumnName g) => g.ColumnName);
				}
				catch (Exception ex7)
				{
				}
				result = reportOptions;
			}
			return result;
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x0003F568 File Offset: 0x0003D768
		private static void FixTproReportCollectionIds(IList<Report> reports, IList<ReportGroup> reportGroups, IList<FormattedReport> formattedReports)
		{
			bool flag = reports != null;
			if (flag)
			{
				foreach (Report report in reports)
				{
					bool flag2 = report.ReportId < 500000;
					if (flag2)
					{
						report.ReportId += 500000;
					}
					bool flag3 = report.FormattedReports != null;
					if (flag3)
					{
						using (IEnumerator<FormattedReport> enumerator2 = report.FormattedReports.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								FormattedReport fr = enumerator2.Current;
								bool flag4 = fr.ReportFileId < 2000000000;
								if (flag4)
								{
									int reportFileId = fr.ReportFileId + 1000000000;
									bool flag5 = formattedReports != null;
									if (flag5)
									{
										IEnumerable<FormattedReport> enumerable = from g in formattedReports
										where g.ReportFileId == fr.ReportFileId
										select g;
										foreach (FormattedReport formattedReport in enumerable)
										{
											formattedReport.ReportFileId = reportFileId;
										}
									}
									fr.ReportFileId = reportFileId;
								}
							}
						}
					}
				}
				bool flag6 = reportGroups != null;
				if (flag6)
				{
					IEnumerable<ReportGroup> enumerable2 = from g in reportGroups
					where g.GroupId > 0 && g.GroupId < 2000000000
					select g;
					using (IEnumerator<ReportGroup> enumerator4 = enumerable2.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							ReportGroup grp = enumerator4.Current;
							int num = grp.GroupId + 1000000000;
							IEnumerable<Report> enumerable3 = from r in reports
							where r.GroupId == grp.GroupId
							select r;
							foreach (Report report2 in enumerable3)
							{
								report2.GroupId = num;
							}
							IEnumerable<ReportGroup> enumerable4 = from rg in reportGroups
							where rg.ParentGroupId == grp.GroupId
							select rg;
							foreach (ReportGroup reportGroup in enumerable4)
							{
								reportGroup.ParentGroupId = num;
							}
							grp.GroupId = num;
						}
					}
				}
				bool flag7 = formattedReports != null;
				if (flag7)
				{
					using (IEnumerator<FormattedReport> enumerator7 = formattedReports.GetEnumerator())
					{
						while (enumerator7.MoveNext())
						{
							FormattedReport fr = enumerator7.Current;
							bool flag8 = fr.ReportFileId < 2000000000;
							if (flag8)
							{
								int reportFileId2 = fr.ReportFileId + 1000000000;
								Func<FormattedReport, bool> <>9__5;
								IEnumerable<Report> enumerable5 = reports.Where(delegate(Report r)
								{
									bool result;
									if (r.FormattedReports != null)
									{
										IEnumerable<FormattedReport> formattedReports2 = r.FormattedReports;
										Func<FormattedReport, bool> predicate;
										if ((predicate = <>9__5) == null)
										{
											predicate = (<>9__5 = ((FormattedReport h) => h.ReportFileId == fr.ReportFileId));
										}
										result = (formattedReports2.FirstOrDefault(predicate) != null);
									}
									else
									{
										result = false;
									}
									return result;
								});
								foreach (Report report3 in enumerable5)
								{
									foreach (FormattedReport formattedReport2 in report3.FormattedReports)
									{
										bool flag9 = formattedReport2.ReportFileId == fr.ReportFileId;
										if (flag9)
										{
											formattedReport2.ReportFileId = reportFileId2;
										}
									}
								}
								fr.ReportFileId = reportFileId2;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x0003F9FC File Offset: 0x0003DBFC
		public static bool AreBuiltByTprosEqual(this Report Report, string BuiltByTproXml)
		{
			bool flag = string.IsNullOrEmpty(BuiltByTproXml);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					ReportCollection reportCollection = BuiltByTproXml.ParseReportsFromNewXml(true);
					bool flag2 = reportCollection == null || reportCollection.Reports == null || reportCollection.Reports.Count < 1;
					if (flag2)
					{
						throw new Exception("No report returned from parse builtbytproxml");
					}
					result = ReportAdapters.AreBuiltByTprosEqual(Report, reportCollection.Reports[0]);
				}
				catch (Exception ex)
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x0003FA7C File Offset: 0x0003DC7C
		private static bool AreBuiltByTprosEqual(Report Report1, Report Report2)
		{
			bool flag = Report1 == null || Report2 == null;
			return !flag && (Report1.Title == Report2.Title && Report1.Description == Report2.Description && Report1.ReportUniqueId == Report2.ReportUniqueId && ReportAdapters.AreReportCreatedLocationsTheSame(Report1.CreatedByLocation, Report2.CreatedByLocation)) && ReportAdapters.AreBuiltByTproFunctionsEqual(Report1.Functions, Report2.Functions);
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x0003FB00 File Offset: 0x0003DD00
		private static bool AreReportCreatedLocationsTheSame(string createdByLocation1, string createdByLocation2)
		{
			string a = ReportAdapters.ExtractGuidFromCreatedByLocation(createdByLocation1);
			string b = ReportAdapters.ExtractGuidFromCreatedByLocation(createdByLocation2);
			return a == b;
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x0003FB28 File Offset: 0x0003DD28
		private static string ExtractGuidFromCreatedByLocation(string createdByLocation)
		{
			bool flag = string.IsNullOrEmpty(createdByLocation);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				int num = createdByLocation.LastIndexOf("_");
				result = ((num >= 0) ? createdByLocation.Substring(num + 1) : createdByLocation);
			}
			return result;
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x0003FB68 File Offset: 0x0003DD68
		private static bool AreBuiltByTproFunctionsEqual(IList<ReportFunction> functions1, IList<ReportFunction> functions2)
		{
			bool flag = ((functions1 == null) ? 0 : functions1.Count) != ((functions2 == null) ? 0 : functions2.Count);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = functions1 == null || functions2 == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					for (int i = 0; i < functions1.Count; i++)
					{
						ReportFunction reportFunction = functions1[i];
						ReportFunction reportFunction2 = functions2[i];
						bool flag3 = reportFunction.FunctionCode != reportFunction2.FunctionCode;
						if (flag3)
						{
							return false;
						}
						bool flag4 = ((reportFunction.FunctionParameters == null) ? 0 : reportFunction.FunctionParameters.Count) != ((reportFunction2.FunctionParameters == null) ? 0 : reportFunction2.FunctionParameters.Count);
						if (flag4)
						{
							return false;
						}
						bool flag5 = reportFunction.FunctionParameters != null;
						if (flag5)
						{
							for (int j = 0; j < reportFunction.FunctionParameters.Count; j++)
							{
								bool flag6 = reportFunction.FunctionParameters[j].Name != reportFunction2.FunctionParameters[j].Name || !ReportAdapters.CompareFunctionParametersValue(reportFunction.FunctionParameters[j].Value, reportFunction2.FunctionParameters[j].Value);
								if (flag6)
								{
									return false;
								}
							}
						}
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x0003FCEC File Offset: 0x0003DEEC
		private static bool CompareFunctionParametersValue(object o1, object o2)
		{
			bool flag = o1 == null && o2 == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = o1 == null || o2 == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = o1.GetType() != o2.GetType();
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = o1 is int;
						if (flag4)
						{
							result = ((int)o1 == (int)o2);
						}
						else
						{
							bool flag5 = o1 is DateTime;
							if (flag5)
							{
								result = ((DateTime)o1 == (DateTime)o2);
							}
							else
							{
								bool flag6 = o1 is bool;
								if (flag6)
								{
									result = ((bool)o1 == (bool)o2);
								}
								else
								{
									bool flag7 = o1 is double;
									if (flag7)
									{
										result = ((double)o1 == (double)o2);
									}
									else
									{
										result = (o1.ToString().Trim() == o2.ToString().Trim());
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x0003FDE8 File Offset: 0x0003DFE8
		public static string ConvertReportToNewXml(this ReportForExport ReportForExport)
		{
			Report item = (ReportForExport == null) ? null : ReportForExport.Report;
			ReportCollection reportCollection = new ReportCollection
			{
				Reports = new List<Report>
				{
					item
				},
				ReportGroups = new List<ReportGroup>()
			};
			ReportCollectionForExport reportCollectionForExport = new ReportCollectionForExport
			{
				ReportCollection = reportCollection
			};
			return reportCollectionForExport.ConvertReportsToNewXml();
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x0003FE44 File Offset: 0x0003E044
		public static string ConvertReportsToNewXml(this ReportCollectionForExport ReportCollectionForExport)
		{
			ReportCollection reportCollection = (ReportCollectionForExport == null) ? null : ReportCollectionForExport.ReportCollection;
			IList<ReportGroup> reportGroups = reportCollection.ReportGroups;
			IList<Report> reports = reportCollection.Reports;
			List<FormattedReport> list = new List<FormattedReport>();
			foreach (Report report in reports)
			{
				bool flag = report.FormattedReports == null;
				if (flag)
				{
					report.FormattedReports = new List<FormattedReport>();
				}
				using (IEnumerator<FormattedReport> enumerator2 = report.FormattedReports.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						FormattedReport fr = enumerator2.Current;
						bool flag2 = list.FirstOrDefault((FormattedReport g) => g.ReportFileId == fr.ReportFileId) == null;
						if (flag2)
						{
							list.Add(fr);
						}
					}
				}
			}
			ReportAdapters.FixTproReportCollectionIds(reports, reportGroups, list);
			XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
			object[] array = new object[1];
			int num = 0;
			XName name = "reportpackage";
			object[] array2 = new object[3];
			array2[0] = new XElement("reports", reports.Select(delegate(Report r)
			{
				XName name2 = "report";
				object[] array3 = new object[18];
				array3[0] = new XAttribute("reportid", r.ReportId);
				array3[1] = new XAttribute("title", r.Title ?? "");
				array3[2] = new XAttribute("description", r.Description ?? "");
				array3[3] = new XAttribute("datecreated", r.DateCreated);
				array3[4] = new XAttribute("whocreatedpid", (r.WhoCreated == null) ? 0 : r.WhoCreated.PersonId);
				array3[5] = new XAttribute("datelastexecuted", r.DateLastExecuted);
				array3[6] = new XAttribute("wholastexecutedpid", (r.WhoLastExecuted == null) ? 0 : r.WhoLastExecuted.PersonId);
				array3[7] = new XAttribute("datelastmodified", r.DateLastModified);
				array3[8] = new XAttribute("wholastmodifiedpid", (r.WhoLastModified == null) ? 0 : r.WhoLastModified.PersonId);
				array3[9] = new XAttribute("functionparametersareencrypted", r.FunctionParametersAreEncrypted);
				array3[10] = new XAttribute("groupid", r.GroupId);
				array3[11] = new XAttribute("dynamiccontrolsscreennum", (r.LegacyParameters == null) ? eReportBuiltInDynamicForm.None : r.LegacyParameters.BuiltInDynamicForm);
				array3[12] = new XAttribute("createdbylocation", r.CreatedByLocation ?? "");
				array3[13] = new XAttribute("reportuniqueid", r.ReportUniqueId.ToString());
				int num2 = 14;
				XName name3 = "formattedreportids";
				object[] array4 = new object[2];
				array4[0] = from q in r.FormattedReports
				select new XElement("formattedreportid", new XAttribute("reportfileid", q.ReportFileId));
				array4[1] = new XAttribute("reportoptions", (r.ReportOptions == null) ? "" : r.ReportOptions.ConvertToXml().ConvertToStringForStorageInXml());
				array3[num2] = new XElement(name3, array4);
				array3[15] = new XElement("functions", r.Functions.Select(delegate(ReportFunction f)
				{
					XName name4 = "function";
					object[] array5 = new object[7];
					array5[0] = new XAttribute("functioncode", (int)f.FunctionCode);
					array5[1] = new XAttribute("description", f.Description ?? "");
					array5[2] = new XAttribute("exampleusage", f.ExampleUsage ?? "");
					array5[3] = new XAttribute("ordernum", f.OrderNum);
					array5[4] = new XAttribute("title", f.Title ?? "");
					array5[5] = new XAttribute("ExecuteThisFunctionOnClientIfPossible", f.ExecuteThisFunctionOnClientIfPossible ? "1" : "");
					array5[6] = new XElement("functionparameters", from p in f.FunctionParameters
					select new XElement("functionparameter", new object[]
					{
						new XAttribute("name", p.Name),
						new XAttribute("value", p.Value.ToString())
					}));
					return new XElement(name4, array5);
				}));
				array3[16] = new XAttribute("reportoptions", (r.ReportOptions == null) ? "" : r.ReportOptions.ConvertToXml().ConvertToStringForStorageInXml());
				array3[17] = new XElement("builtbytprosignedandencryptedreportxml", (r.BuiltByTproSignedAndEncryptedReportXml != null && r.BuiltByTproSignedAndEncryptedReportXml.Length != 0) ? Convert.ToBase64String(r.BuiltByTproSignedAndEncryptedReportXml) : "");
				return new XElement(name2, array3);
			}));
			array2[1] = new XElement("reportgroups", from g in reportGroups
			select new XElement("reportgroup", new object[]
			{
				new XAttribute("groupid", g.GroupId),
				new XAttribute("groupdescription", g.Description ?? ""),
				new XAttribute("parentgroupid", g.ParentGroupId),
				new XAttribute("grouptitle", g.Title ?? ""),
				new XAttribute("ordernum", g.OrderNum)
			}));
			array2[2] = new XElement("formattedreports", from fr in list
			select new XElement("formattedreport", new object[]
			{
				new XAttribute("reportfileid", fr.ReportFileId),
				new XAttribute("title", fr.Title ?? ""),
				new XAttribute("description", fr.Description ?? ""),
				new XAttribute("template", (fr.FormattedReportTemplate == null) ? "" : Convert.ToBase64String(fr.FormattedReportTemplate))
			}));
			array[num] = new XElement(name, array2);
			XDocument xdocument = new XDocument(declaration, array);
			return xdocument.Declaration.ToString() + xdocument.ToString();
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x00040050 File Offset: 0x0003E250
		public static eRowFormattingConditionType ConvertFromString(this string conditionTypeString)
		{
			bool flag = !Enum.IsDefined(typeof(eRowFormattingConditionType), conditionTypeString);
			eRowFormattingConditionType result;
			if (flag)
			{
				result = eRowFormattingConditionType.None;
			}
			else
			{
				result = (eRowFormattingConditionType)Enum.Parse(typeof(eRowFormattingConditionType), conditionTypeString);
			}
			return result;
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x00040094 File Offset: 0x0003E294
		public static string ConvertToString(eRowFormattingConditionType conditionType)
		{
			return conditionType.ToString();
		}

		// Token: 0x02000658 RID: 1624
		internal class ReportBasicOption
		{
			// Token: 0x17001400 RID: 5120
			// (get) Token: 0x060032FE RID: 13054 RVA: 0x0004A41E File Offset: 0x0004861E
			// (set) Token: 0x060032FF RID: 13055 RVA: 0x0004A426 File Offset: 0x00048626
			public string NoteToUser { get; set; }

			// Token: 0x17001401 RID: 5121
			// (get) Token: 0x06003300 RID: 13056 RVA: 0x0004A42F File Offset: 0x0004862F
			// (set) Token: 0x06003301 RID: 13057 RVA: 0x0004A437 File Offset: 0x00048637
			public bool DontShowReportResults { get; set; }
		}

		// Token: 0x02000659 RID: 1625
		internal class ReportOptionColumnName
		{
			// Token: 0x17001402 RID: 5122
			// (get) Token: 0x06003303 RID: 13059 RVA: 0x0004A440 File Offset: 0x00048640
			// (set) Token: 0x06003304 RID: 13060 RVA: 0x0004A448 File Offset: 0x00048648
			public string ColumnName { get; set; }
		}

		// Token: 0x0200065A RID: 1626
		internal class Node
		{
			// Token: 0x06003306 RID: 13062 RVA: 0x0004A451 File Offset: 0x00048651
			public Node()
			{
				this.Children = new List<ReportAdapters.Node>();
			}

			// Token: 0x06003307 RID: 13063 RVA: 0x0004A468 File Offset: 0x00048668
			public Node(int id, int parentId)
			{
				this.Id = id;
				bool flag = parentId > 0;
				if (flag)
				{
					this.ParentId = new int?(parentId);
				}
				this.Children = new List<ReportAdapters.Node>();
			}

			// Token: 0x17001403 RID: 5123
			// (get) Token: 0x06003308 RID: 13064 RVA: 0x0004A4A8 File Offset: 0x000486A8
			// (set) Token: 0x06003309 RID: 13065 RVA: 0x0004A4B0 File Offset: 0x000486B0
			public int Id { get; set; }

			// Token: 0x17001404 RID: 5124
			// (get) Token: 0x0600330A RID: 13066 RVA: 0x0004A4B9 File Offset: 0x000486B9
			// (set) Token: 0x0600330B RID: 13067 RVA: 0x0004A4C1 File Offset: 0x000486C1
			public int? ParentId { get; set; }

			// Token: 0x17001405 RID: 5125
			// (get) Token: 0x0600330C RID: 13068 RVA: 0x0004A4CA File Offset: 0x000486CA
			// (set) Token: 0x0600330D RID: 13069 RVA: 0x0004A4D2 File Offset: 0x000486D2
			public List<ReportAdapters.Node> Children { get; set; }

			// Token: 0x17001406 RID: 5126
			// (get) Token: 0x0600330E RID: 13070 RVA: 0x0004A4DB File Offset: 0x000486DB
			// (set) Token: 0x0600330F RID: 13071 RVA: 0x0004A4E3 File Offset: 0x000486E3
			public ReportAdapters.Node Parent { get; set; }
		}
	}
}
