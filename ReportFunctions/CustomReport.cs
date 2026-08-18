using System;
using System.Collections;
using System.Data;
using System.Xml;

namespace ReportFunctions
{
	// Token: 0x02000038 RID: 56
	public class CustomReport
	{
		// Token: 0x06000349 RID: 841 RVA: 0x000401D4 File Offset: 0x0003F1D4
		public static void FanshaweGetStudentData(XmlNode x, ref Report report, string student_no, DateTime excludeCoursesBeforeDate, string addressTypeCode_local, string addressTypeCode_permanent, string programStatusesToIgnore)
		{
			if (x != null)
			{
				CustomReport.FanshaweParseXml(x, ref report, student_no, excludeCoursesBeforeDate, addressTypeCode_local, addressTypeCode_permanent, programStatusesToIgnore);
			}
			else
			{
				DataView currentDataView = report.GetCurrentDataView();
				DataTable table = currentDataView.Table;
				if (table.Rows.Count > 0)
				{
					Guid guid = new Guid(table.Rows[0][0].ToString());
					string text = "Guid(" + guid.ToString() + ")";
					string[] args = new string[]
					{
						student_no,
						text
					};
					object obj = ReportFunction.ConsumeWebService0(currentDataView, "https://lfacs2.fanshawec.ca/ClockWork/ClockWork.asmx", "ClockWorkData", "GetStudentData", "", args);
					if (obj != null && obj is XmlNode)
					{
						x = (XmlNode)obj;
						CustomReport.FanshaweParseXml(x, ref report, student_no, excludeCoursesBeforeDate, addressTypeCode_local, addressTypeCode_permanent, programStatusesToIgnore);
					}
				}
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000402D8 File Offset: 0x0003F2D8
		public static void FanshaweGetChangedStudentData(XmlNode x, ref Report report)
		{
			if (x != null)
			{
				CustomReport.FanshaweParseXmlChanged(x, ref report);
			}
			else
			{
				DataView currentDataView = report.GetCurrentDataView();
				DataTable table = currentDataView.Table;
				if (table.Rows.Count > 0)
				{
					Guid guid = new Guid(table.Rows[0][0].ToString());
					string text = "Guid(" + guid.ToString() + ")";
					object[] args = new object[]
					{
						DateTime.Now.AddDays(-1.0),
						text
					};
					object obj = ReportFunction.ConsumeWebService0(currentDataView, "https://lfacs2.fanshawec.ca/ClockWork/ClockWork.asmx", "ClockWorkData", "GetChangedStudents", "", args);
					if (obj != null && obj is XmlNode)
					{
						x = (XmlNode)obj;
						CustomReport.FanshaweParseXmlChanged(x, ref report);
					}
				}
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000403E4 File Offset: 0x0003F3E4
		public static void FanshaweParseXmlChanged(XmlNode x, ref Report report)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("StudentNumber");
			dataTable.Columns.Add("firstname");
			dataTable.Columns.Add("lastname");
			dataTable.Columns.Add("middlename");
			XmlNodeList xmlNodeList = x.SelectNodes("//Person");
			foreach (object obj in xmlNodeList)
			{
				XmlNode node = (XmlNode)obj;
				string innerTextSafe = CustomReport.GetInnerTextSafe(node, "StudentNumber");
				string innerTextSafe2 = CustomReport.GetInnerTextSafe(node, "FirstName");
				string innerTextSafe3 = CustomReport.GetInnerTextSafe(node, "Surname");
				string innerTextSafe4 = CustomReport.GetInnerTextSafe(node, "MiddleName");
				dataTable.Rows.Add(new object[]
				{
					innerTextSafe,
					innerTextSafe2,
					innerTextSafe3,
					innerTextSafe4
				});
			}
			dataTable.TableName = "studentdata";
			report.AddResult(dataTable.DefaultView);
			report.NameCurrentTable("students");
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00040528 File Offset: 0x0003F528
		public static void FanshaweParseXml(XmlNode x, ref Report report, string student_no, DateTime excludeCoursesBeforeDate, string addressTypeCode_local, string addressTypeCode_permanent, string programStatusesToIgnore)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("StudentNumber");
			dataTable.Columns.Add("FirstName");
			dataTable.Columns.Add("MiddleName");
			dataTable.Columns.Add("Surname");
			dataTable.Columns.Add("Gender");
			dataTable.Columns.Add("Birthdate");
			dataTable.Columns.Add("SchoolEmail");
			dataTable.Columns.Add("AltEmail");
			dataTable.Columns.Add("LocalStreetAddress");
			dataTable.Columns.Add("LocalCity");
			dataTable.Columns.Add("LocalProvince");
			dataTable.Columns.Add("LocalPostalCode");
			dataTable.Columns.Add("LocalCountry");
			dataTable.Columns.Add("LocalPhone");
			dataTable.Columns.Add("PermStreetAddress");
			dataTable.Columns.Add("PermCity");
			dataTable.Columns.Add("PermProvince");
			dataTable.Columns.Add("PermPostalCode");
			dataTable.Columns.Add("PermCountry");
			dataTable.Columns.Add("PermPhone");
			dataTable.Columns.Add("TermCode");
			dataTable.Columns.Add("ProgramCode");
			dataTable.Columns.Add("ProgramTitle");
			dataTable.Columns.Add("ProgramStartDate");
			dataTable.Columns.Add("ProgramStatus");
			dataTable.Columns.Add("ProgramFullPartTime");
			dataTable.Columns.Add("ProgramStanding");
			dataTable.Columns.Add("Department");
			dataTable.Columns.Add("Division");
			dataTable.Columns.Add("Location");
			dataTable.Columns.Add("Gpa");
			dataTable.Columns.Add("TermGpa");
			XmlNode xmlNode = x.SelectSingleNode("//Person");
			if (xmlNode != null)
			{
				DataRow dataRow = dataTable.NewRow();
				string innerTextSafe = CustomReport.GetInnerTextSafe(xmlNode, "StudentNumber");
				dataRow["StudentNumber"] = innerTextSafe;
				dataRow["gpa"] = CustomReport.GetInnerTextSafe(xmlNode, "CumulativeGPA");
				CustomReport.SetInnerTextSafe(xmlNode, dataRow, new string[]
				{
					"FirstName",
					"MiddleName",
					"Surname",
					"Gender",
					"Birthdate"
				});
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("//Addresses");
				if (xmlNode2 != null)
				{
					XmlNodeList xmlNodeList = xmlNode2.SelectNodes("//Address");
					if (xmlNodeList != null)
					{
						foreach (object obj in xmlNodeList)
						{
							XmlNode node = (XmlNode)obj;
							string text = CustomReport.GetInnerTextSafe(node, "AddressType").ToLower().Trim();
							if (text.CompareTo(addressTypeCode_local) == 0)
							{
								dataRow["LocalStreetAddress"] = CustomReport.GetInnerTextSafe(node, "StreetAddress");
								dataRow["LocalCity"] = CustomReport.GetInnerTextSafe(node, "City");
								dataRow["LocalProvince"] = CustomReport.GetInnerTextSafe(node, "Province");
								dataRow["LocalCountry"] = CustomReport.GetInnerTextSafe(node, "Country");
								dataRow["LocalPostalCode"] = CustomReport.GetInnerTextSafe(node, "PostalCode");
								dataRow["LocalPhone"] = CustomReport.GetInnerTextSafe(node, "Phone");
							}
							else if (text.CompareTo(addressTypeCode_permanent) == 0)
							{
								dataRow["PermStreetAddress"] = CustomReport.GetInnerTextSafe(node, "StreetAddress");
								dataRow["PermCity"] = CustomReport.GetInnerTextSafe(node, "City");
								dataRow["PermProvince"] = CustomReport.GetInnerTextSafe(node, "Province");
								dataRow["PermCountry"] = CustomReport.GetInnerTextSafe(node, "Country");
								dataRow["PermPostalCode"] = CustomReport.GetInnerTextSafe(node, "PostalCode");
								dataRow["PermPhone"] = CustomReport.GetInnerTextSafe(node, "Phone");
							}
						}
					}
				}
				XmlNode xmlNode3 = xmlNode.SelectSingleNode("//EmailAddresses");
				if (xmlNode3 != null)
				{
					XmlNodeList xmlNodeList2 = xmlNode3.SelectNodes("//EmailAddress");
					if (xmlNodeList2 != null)
					{
						foreach (object obj2 in xmlNodeList2)
						{
							XmlNode xmlNode4 = (XmlNode)obj2;
							string innerText = xmlNode4.InnerText;
							int num = innerText.IndexOf('@');
							if (innerText.LastIndexOf("fanshawe") > num)
							{
								dataRow["SchoolEmail"] = innerText;
							}
							else
							{
								dataRow["AltEmail"] = xmlNode4.InnerText;
							}
						}
					}
				}
				dataTable.Columns["StudentNumber"].ColumnName = "student_no";
				dataTable.Columns["Surname"].ColumnName = "LastName";
				dataTable.Rows.Add(dataRow);
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("StudentNumber");
				dataTable2.Columns.Add("TermCode");
				dataTable2.Columns.Add("ProgramCode");
				dataTable2.Columns.Add("ProgramTitle");
				dataTable2.Columns.Add("ProgramStartDate");
				dataTable2.Columns.Add("ProgramStatus");
				dataTable2.Columns.Add("ProgramFullPartTime");
				dataTable2.Columns.Add("ProgramStanding");
				dataTable2.Columns.Add("Department");
				dataTable2.Columns.Add("Division");
				dataTable2.Columns.Add("Location");
				DataTable dataTable3 = new DataTable();
				dataTable3.Columns.Add("StudentNumber");
				dataTable3.Columns.Add("Term");
				dataTable3.Columns.Add("Subject");
				dataTable3.Columns.Add("Course");
				dataTable3.Columns.Add("Section");
				dataTable3.Columns.Add("StartDate");
				dataTable3.Columns.Add("EndDate");
				dataTable3.Columns.Add("Name");
				dataTable3.Columns.Add("Title");
				dataTable3.Columns.Add("CourseStatus");
				dataTable3.Columns.Add("instructorid");
				dataTable3.Columns.Add("instructorname");
				dataTable3.Columns.Add("instructoremail");
				dataTable3.Columns.Add("instructorphone");
				dataTable3.Columns.Add("grade");
				dataTable3.Columns.Add("sunstartminutes");
				dataTable3.Columns.Add("sunendminutes");
				dataTable3.Columns.Add("monstartminutes");
				dataTable3.Columns.Add("monendminutes");
				dataTable3.Columns.Add("tuestartminutes");
				dataTable3.Columns.Add("tueendminutes");
				dataTable3.Columns.Add("wedstartminutes");
				dataTable3.Columns.Add("wedendminutes");
				dataTable3.Columns.Add("thustartminutes");
				dataTable3.Columns.Add("thuendminutes");
				dataTable3.Columns.Add("fristartminutes");
				dataTable3.Columns.Add("friendminutes");
				dataTable3.Columns.Add("satstartminutes");
				dataTable3.Columns.Add("satendminutes");
				XmlNode xmlNode5 = x.SelectSingleNode("Academic");
				string text2 = "";
				if (xmlNode5 != null)
				{
					XmlNodeList xmlNodeList3 = xmlNode5.SelectNodes("Term");
					if (xmlNodeList3 != null)
					{
						foreach (object obj3 in xmlNodeList3)
						{
							XmlNode xmlNode6 = (XmlNode)obj3;
							DataRow dataRow2 = dataTable2.NewRow();
							dataRow2["StudentNumber"] = innerTextSafe;
							string innerText2 = xmlNode6["TermCode"].InnerText;
							dataRow2["TermCode"] = innerText2;
							XmlNode xmlNode7 = xmlNode6.SelectSingleNode("Program");
							if (xmlNode7 != null)
							{
								string value = CustomReport.GetInnerTextSafe(xmlNode7, "ProgramStatus").ToLower().Trim();
								if (programStatusesToIgnore.IndexOf(value) < 0)
								{
									CustomReport.SetInnerTextSafe(xmlNode7, dataRow2, new string[]
									{
										"ProgramCode",
										"ProgramTitle",
										"ProgramStartDate",
										"ProgramStatus",
										"ProgramFullPartTime",
										"ProgramStanding",
										"Department",
										"Division",
										"Location"
									});
									string text3 = CustomReport.GetInnerTextSafe(xmlNode7, "TermGPA").Trim();
									if (text3.Length > 0)
									{
										if (text2.Length > 0)
										{
											text2 += ",";
										}
										string text4 = text2;
										text2 = string.Concat(new string[]
										{
											text4,
											innerText2,
											" (",
											text3,
											")"
										});
									}
									dataTable2.Rows.Add(dataRow2);
								}
							}
							XmlNode xmlNode8 = xmlNode6.SelectSingleNode("Courses");
							if (xmlNode8 != null)
							{
								XmlNodeList xmlNodeList4 = xmlNode8.SelectNodes("Course");
								if (xmlNodeList4 != null)
								{
									foreach (object obj4 in xmlNodeList4)
									{
										XmlNode xmlNode9 = (XmlNode)obj4;
										DataRow dataRow3 = dataTable3.NewRow();
										dataRow3["StudentNumber"] = innerTextSafe;
										dataRow3["Term"] = innerText2;
										CustomReport.SetInnerTextSafe(xmlNode9, dataRow3, new string[]
										{
											"Name",
											"Title",
											"CourseStatus",
											"Section",
											"StartDate",
											"EndDate",
											"Grade"
										});
										XmlNode xmlNode10 = xmlNode9["Faculty"];
										if (xmlNode10 != null && xmlNode10.ChildNodes.Count > 0)
										{
											string innerText3 = xmlNode10["FirstName"].InnerText;
											string innerText4 = xmlNode10["LastName"].InnerText;
											string text5 = innerText4 + ((innerText4.Trim().Length > 0) ? ", " : "") + innerText3;
											dataRow3["instructorid"] = CustomReport.GetInnerTextSafe(xmlNode10, "ID");
											dataRow3["instructorname"] = text5;
											dataRow3["instructoremail"] = CustomReport.GetInnerTextSafe(xmlNode10, "Email");
										}
										XmlNode nextSibling = xmlNode9.NextSibling;
										if (nextSibling != null)
										{
											XmlNodeList xmlNodeList5 = nextSibling.SelectNodes("Days");
											foreach (object obj5 in xmlNodeList5)
											{
												XmlNode xmlNode11 = (XmlNode)obj5;
												string text6 = xmlNode11.InnerText.ToLower().Trim();
												string text7 = "";
												foreach (char c in text6)
												{
													if (text7.Length >= 3)
													{
														break;
													}
													if (char.IsLetter(c) || text7.Length > 0)
													{
														text7 += c;
													}
												}
												if (text7.Length > 0)
												{
													string timeOfDay = nextSibling.ChildNodes[1].InnerText.ToLower();
													string timeOfDay2 = nextSibling.ChildNodes[2].InnerText.ToLower();
													int num2 = CustomReport.ParseTimeOfDay(timeOfDay);
													int num3 = CustomReport.ParseTimeOfDay(timeOfDay2);
													if (num2 != 0 && num3 > num2)
													{
														string columnName = text7 + "startminutes";
														string columnName2 = text7 + "endminutes";
														dataRow3[columnName] = num2.ToString();
														dataRow3[columnName2] = num3.ToString();
													}
												}
											}
										}
										dataTable3.Rows.Add(dataRow3);
									}
								}
							}
						}
					}
				}
				DataRow dataRow4 = CustomReport.FindNewestTerm(dataTable2);
				if (dataRow4 != null)
				{
					dataRow["TermCode"] = dataRow4["TermCode"];
					dataRow["ProgramCode"] = dataRow4["ProgramCode"];
					dataRow["ProgramTitle"] = dataRow4["ProgramTitle"];
					dataRow["ProgramStartDate"] = dataRow4["ProgramStartDate"];
					dataRow["ProgramStatus"] = dataRow4["ProgramStatus"];
					dataRow["ProgramFullPartTime"] = dataRow4["ProgramFullPartTime"];
					dataRow["ProgramStanding"] = dataRow4["ProgramStanding"];
					dataRow["Department"] = dataRow4["Department"];
					dataRow["Division"] = dataRow4["Division"];
					dataRow["Location"] = dataRow4["Location"];
					dataRow["TermGpa"] = text2;
				}
				ArrayList arrayList = new ArrayList();
				foreach (object obj6 in dataTable3.Rows)
				{
					DataRow dataRow5 = (DataRow)obj6;
					string text9 = dataRow5["CourseStatus"].ToString().Trim().ToLower();
					if (text9.CompareTo("a") != 0 && text9.CompareTo("n") != 0)
					{
						arrayList.Add(dataRow5);
					}
					else
					{
						string s = dataRow5["StartDate"].ToString();
						DateTime t = ReportFunction.ParseDateTime(s);
						if (t < excludeCoursesBeforeDate)
						{
							arrayList.Add(dataRow5);
						}
						else
						{
							string text5 = dataRow5["Name"].ToString();
							int num4 = text5.IndexOf('-');
							if (num4 > 0)
							{
								dataRow5["Subject"] = text5.Substring(0, num4);
								dataRow5["Course"] = text5.Substring(num4 + 1);
							}
							else
							{
								dataRow5["Subject"] = text5.Substring(0, 4);
								dataRow5["Course"] = text5.Substring(4);
							}
						}
					}
				}
				foreach (object obj7 in arrayList)
				{
					DataRow row = (DataRow)obj7;
					dataTable3.Rows.Remove(row);
				}
				dataTable3.Columns["StudentNumber"].ColumnName = "student_no";
				dataTable.TableName = "studentdata";
				dataTable3.TableName = "courses";
				report.AddResult(dataTable3.DefaultView);
				report.NameCurrentTable("courses");
				report.AddResult(dataTable.DefaultView);
				report.NameCurrentTable("data");
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00041774 File Offset: 0x00040774
		private static int ParseTimeOfDay(string timeOfDay)
		{
			int num = timeOfDay.IndexOf(':');
			int result;
			if (num > 0 && num < timeOfDay.Length - 1)
			{
				string s = timeOfDay.Substring(0, num);
				string s2 = timeOfDay.Substring(num + 1);
				int num2 = int.Parse(s);
				int num3 = int.Parse(s2);
				result = num2 * 60 + num3;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000417DC File Offset: 0x000407DC
		private static string GetInnerTextSafe(XmlNode node, string name)
		{
			return (node[name] == null) ? "" : node[name].InnerText;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0004180C File Offset: 0x0004080C
		private static void SetInnerTextSafe(XmlNode node, DataRow dr, params string[] names)
		{
			foreach (string text in names)
			{
				dr[text] = CustomReport.GetInnerTextSafe(node, text);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00041844 File Offset: 0x00040844
		private static DataRow FindNewestTerm(DataTable tAcademic)
		{
			DateTime t = DateTime.MinValue;
			DataRow result = null;
			foreach (object obj in tAcademic.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DateTime minValue;
				if (dataRow["ProgramStartDate"] == DBNull.Value)
				{
					minValue = DateTime.MinValue;
				}
				else if (!DateTime.TryParse(dataRow["ProgramStartDate"].ToString().Trim(), out minValue))
				{
					minValue = DateTime.MinValue;
				}
				if (minValue < t)
				{
					t = minValue;
					result = dataRow;
				}
			}
			return result;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00041928 File Offset: 0x00040928
		private static int ExtractNumber(string s)
		{
			string text = "";
			foreach (char c in s)
			{
				if (char.IsDigit(c))
				{
					text += c;
				}
			}
			if (text.Length > 0)
			{
				try
				{
					return int.Parse(text);
				}
				catch
				{
					return 0;
				}
			}
			return 0;
		}
	}
}
