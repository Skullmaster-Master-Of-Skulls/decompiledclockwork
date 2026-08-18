using System;
using System.Collections;
using System.Data;
using System.Xml;
using TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy
{
	// Token: 0x0200000C RID: 12
	public class CustomReport
	{
		// Token: 0x06000061 RID: 97 RVA: 0x000083E8 File Offset: 0x000065E8
		public static void FanshaweGetStudentData(XmlNode x, ref Report report, string student_no, DateTime excludeCoursesBeforeDate, string addressTypeCode_local, string addressTypeCode_permanent, string programStatusesToIgnore)
		{
			bool flag = x != null;
			if (flag)
			{
				CustomReport.FanshaweParseXml(x, ref report, student_no, excludeCoursesBeforeDate, addressTypeCode_local, addressTypeCode_permanent, programStatusesToIgnore);
			}
			else
			{
				DataView currentDataView = report.GetCurrentDataView();
				DataTable table = currentDataView.Table;
				bool flag2 = table.Rows.Count > 0;
				if (flag2)
				{
					Guid guid = new Guid(table.Rows[0][0].ToString());
					string text = "Guid(" + guid.ToString() + ")";
					string[] array = new string[]
					{
						student_no,
						text
					};
					DataView dv = currentDataView;
					string url = "https://lfacs2.fanshawec.ca/ClockWork/ClockWork.asmx";
					string serviceName = "ClockWorkData";
					string methodName = "GetStudentData";
					string extraInfo = "";
					object[] args = array;
					object obj = ReportFunction.ConsumeWebService0(dv, url, serviceName, methodName, extraInfo, args);
					bool flag3 = obj != null && obj is XmlNode;
					if (flag3)
					{
						x = (XmlNode)obj;
						CustomReport.FanshaweParseXml(x, ref report, student_no, excludeCoursesBeforeDate, addressTypeCode_local, addressTypeCode_permanent, programStatusesToIgnore);
					}
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000084E0 File Offset: 0x000066E0
		public static void FanshaweGetChangedStudentData(XmlNode x, ref Report report)
		{
			bool flag = x != null;
			if (flag)
			{
				CustomReport.FanshaweParseXmlChanged(x, ref report);
			}
			else
			{
				DataView currentDataView = report.GetCurrentDataView();
				DataTable table = currentDataView.Table;
				bool flag2 = table.Rows.Count > 0;
				if (flag2)
				{
					Guid guid = new Guid(table.Rows[0][0].ToString());
					string text = "Guid(" + guid.ToString() + ")";
					object[] args = new object[]
					{
						DateTime.Now.AddDays(-1.0),
						text
					};
					object obj = ReportFunction.ConsumeWebService0(currentDataView, "https://lfacs2.fanshawec.ca/ClockWork/ClockWork.asmx", "ClockWorkData", "GetChangedStudents", "", args);
					bool flag3 = obj != null && obj is XmlNode;
					if (flag3)
					{
						x = (XmlNode)obj;
						CustomReport.FanshaweParseXmlChanged(x, ref report);
					}
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000085DC File Offset: 0x000067DC
		public static void FanshaweParseXmlChanged(XmlNode x, ref Report report)
		{
			DataTable dataTable = new DataTable("t");
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

		// Token: 0x06000064 RID: 100 RVA: 0x00008710 File Offset: 0x00006910
		public static void FanshaweParseXml(XmlNode x, ref Report report, string student_no, DateTime excludeCoursesBeforeDate, string addressTypeCode_local, string addressTypeCode_permanent, string programStatusesToIgnore)
		{
			DataTable dataTable = new DataTable("t");
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
			bool flag = xmlNode != null;
			if (flag)
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
				bool flag2 = xmlNode2 != null;
				if (flag2)
				{
					XmlNodeList xmlNodeList = xmlNode2.SelectNodes("//Address");
					bool flag3 = xmlNodeList != null;
					if (flag3)
					{
						foreach (object obj in xmlNodeList)
						{
							XmlNode node = (XmlNode)obj;
							string text = CustomReport.GetInnerTextSafe(node, "AddressType").ToLower().Trim();
							bool flag4 = text.CompareTo(addressTypeCode_local) == 0;
							if (flag4)
							{
								dataRow["LocalStreetAddress"] = CustomReport.GetInnerTextSafe(node, "StreetAddress");
								dataRow["LocalCity"] = CustomReport.GetInnerTextSafe(node, "City");
								dataRow["LocalProvince"] = CustomReport.GetInnerTextSafe(node, "Province");
								dataRow["LocalCountry"] = CustomReport.GetInnerTextSafe(node, "Country");
								dataRow["LocalPostalCode"] = CustomReport.GetInnerTextSafe(node, "PostalCode");
								dataRow["LocalPhone"] = CustomReport.GetInnerTextSafe(node, "Phone");
							}
							else
							{
								bool flag5 = text.CompareTo(addressTypeCode_permanent) == 0;
								if (flag5)
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
				}
				XmlNode xmlNode3 = xmlNode.SelectSingleNode("//EmailAddresses");
				bool flag6 = xmlNode3 != null;
				if (flag6)
				{
					XmlNodeList xmlNodeList2 = xmlNode3.SelectNodes("//EmailAddress");
					bool flag7 = xmlNodeList2 != null;
					if (flag7)
					{
						foreach (object obj2 in xmlNodeList2)
						{
							XmlNode xmlNode4 = (XmlNode)obj2;
							string innerText = xmlNode4.InnerText;
							int num = innerText.IndexOf('@');
							bool flag8 = innerText.LastIndexOf("fanshawe") > num;
							if (flag8)
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
				DataTable dataTable2 = new DataTable("t");
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
				DataTable dataTable3 = new DataTable("t");
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
				bool flag9 = xmlNode5 != null;
				if (flag9)
				{
					XmlNodeList xmlNodeList3 = xmlNode5.SelectNodes("Term");
					bool flag10 = xmlNodeList3 != null;
					if (flag10)
					{
						foreach (object obj3 in xmlNodeList3)
						{
							XmlNode xmlNode6 = (XmlNode)obj3;
							DataRow dataRow2 = dataTable2.NewRow();
							dataRow2["StudentNumber"] = innerTextSafe;
							string innerText2 = xmlNode6["TermCode"].InnerText;
							dataRow2["TermCode"] = innerText2;
							XmlNode xmlNode7 = xmlNode6.SelectSingleNode("Program");
							bool flag11 = xmlNode7 != null;
							if (flag11)
							{
								string value = CustomReport.GetInnerTextSafe(xmlNode7, "ProgramStatus").ToLower().Trim();
								bool flag12 = programStatusesToIgnore.IndexOf(value) < 0;
								if (flag12)
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
									bool flag13 = text3.Length > 0;
									if (flag13)
									{
										bool flag14 = text2.Length > 0;
										if (flag14)
										{
											text2 += ",";
										}
										text2 = string.Concat(new string[]
										{
											text2,
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
							bool flag15 = xmlNode8 != null;
							if (flag15)
							{
								XmlNodeList xmlNodeList4 = xmlNode8.SelectNodes("Course");
								bool flag16 = xmlNodeList4 != null;
								if (flag16)
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
										bool flag17 = xmlNode10 != null && xmlNode10.ChildNodes.Count > 0;
										if (flag17)
										{
											string innerText3 = xmlNode10["FirstName"].InnerText;
											string innerText4 = xmlNode10["LastName"].InnerText;
											string value2 = innerText4 + ((innerText4.Trim().Length > 0) ? ", " : "") + innerText3;
											dataRow3["instructorid"] = CustomReport.GetInnerTextSafe(xmlNode10, "ID");
											dataRow3["instructorname"] = value2;
											dataRow3["instructoremail"] = CustomReport.GetInnerTextSafe(xmlNode10, "Email");
										}
										XmlNode nextSibling = xmlNode9.NextSibling;
										bool flag18 = nextSibling != null;
										if (flag18)
										{
											XmlNodeList xmlNodeList5 = nextSibling.SelectNodes("Days");
											foreach (object obj5 in xmlNodeList5)
											{
												XmlNode xmlNode11 = (XmlNode)obj5;
												string text4 = xmlNode11.InnerText.ToLower().Trim();
												string text5 = "";
												foreach (char c in text4)
												{
													bool flag19 = text5.Length >= 3;
													if (flag19)
													{
														break;
													}
													bool flag20 = char.IsLetter(c) || text5.Length > 0;
													if (flag20)
													{
														text5 += c.ToString();
													}
												}
												bool flag21 = text5.Length > 0;
												if (flag21)
												{
													string timeOfDay = nextSibling.ChildNodes[1].InnerText.ToLower();
													string timeOfDay2 = nextSibling.ChildNodes[2].InnerText.ToLower();
													int num2 = CustomReport.ParseTimeOfDay(timeOfDay);
													int num3 = CustomReport.ParseTimeOfDay(timeOfDay2);
													bool flag22 = num2 != 0 && num3 > num2;
													if (flag22)
													{
														string columnName = text5 + "startminutes";
														string columnName2 = text5 + "endminutes";
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
				bool flag23 = dataRow4 != null;
				if (flag23)
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
					string text7 = dataRow5["CourseStatus"].ToString().Trim().ToLower();
					bool flag24 = text7.CompareTo("a") != 0 && text7.CompareTo("n") != 0;
					if (flag24)
					{
						arrayList.Add(dataRow5);
					}
					else
					{
						string s = dataRow5["StartDate"].ToString();
						DateTime t = ReportFunction.ParseDateTime(s);
						bool flag25 = t < excludeCoursesBeforeDate;
						if (flag25)
						{
							arrayList.Add(dataRow5);
						}
						else
						{
							string text8 = dataRow5["Name"].ToString();
							int num4 = text8.IndexOf('-');
							bool flag26 = num4 > 0;
							if (flag26)
							{
								dataRow5["Subject"] = text8.Substring(0, num4);
								dataRow5["Course"] = text8.Substring(num4 + 1);
							}
							else
							{
								dataRow5["Subject"] = text8.Substring(0, 4);
								dataRow5["Course"] = text8.Substring(4);
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

		// Token: 0x06000065 RID: 101 RVA: 0x000098C4 File Offset: 0x00007AC4
		private static int ParseTimeOfDay(string timeOfDay)
		{
			int num = timeOfDay.IndexOf(':');
			bool flag = num > 0 && num < timeOfDay.Length - 1;
			int result;
			if (flag)
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

		// Token: 0x06000066 RID: 102 RVA: 0x00009928 File Offset: 0x00007B28
		private static string GetInnerTextSafe(XmlNode node, string name)
		{
			return (node[name] == null) ? "" : node[name].InnerText;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00009958 File Offset: 0x00007B58
		private static void SetInnerTextSafe(XmlNode node, DataRow dr, params string[] names)
		{
			foreach (string text in names)
			{
				dr[text] = CustomReport.GetInnerTextSafe(node, text);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000998C File Offset: 0x00007B8C
		private static DataRow FindNewestTerm(DataTable tAcademic)
		{
			DateTime t = DateTime.MinValue;
			DataRow result = null;
			foreach (object obj in tAcademic.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = dataRow["ProgramStartDate"] == DBNull.Value;
				DateTime minValue;
				if (flag)
				{
					minValue = DateTime.MinValue;
				}
				else
				{
					bool flag2 = DateTime.TryParse(dataRow["ProgramStartDate"].ToString().Trim(), out minValue);
					bool flag3 = !flag2;
					if (flag3)
					{
						minValue = DateTime.MinValue;
					}
				}
				bool flag4 = minValue < t;
				if (flag4)
				{
					t = minValue;
					result = dataRow;
				}
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00009A60 File Offset: 0x00007C60
		private static int ExtractNumber(string s)
		{
			string text = "";
			foreach (char c in s)
			{
				bool flag = char.IsDigit(c);
				if (flag)
				{
					text += c.ToString();
				}
			}
			bool flag2 = text.Length > 0;
			if (flag2)
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
