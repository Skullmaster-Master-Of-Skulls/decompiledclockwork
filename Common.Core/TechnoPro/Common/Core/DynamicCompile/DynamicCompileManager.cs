using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using ClockWorkLogger;
using Microsoft.CSharp;
using TechnoPro.Common.DAO.DynamicCompile;
using TechnoPro.Common.DAO.Impl.DynamicCompile;
using TechnoPro.Common.DAO.Reports;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.DynamicCompile;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicCompile;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.DynamicCompile
{
	// Token: 0x02000105 RID: 261
	public class DynamicCompileManager : IDynamicCompileManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00044874 File Offset: 0x00042A74
		public IReportDAO ReportDAO
		{
			get
			{
				bool flag = this.rdao == null;
				if (flag)
				{
					this.rdao = new ReportDAO(this.OpContext);
				}
				return this.rdao;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000448AA File Offset: 0x00042AAA
		public DynamicCompileManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new DynamicCompileDAO(opContext);
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x000448C8 File Offset: 0x00042AC8
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x000448D0 File Offset: 0x00042AD0
		public OperationContext OpContext { get; set; }

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000448DC File Offset: 0x00042ADC
		public void RunCustomReportCode(ref RunReportResult runReportResult, string code, RunReportResult CurrentReportResult)
		{
			Assembly assembly = this.CompileCodeToAssembly(code);
			bool flag = assembly == null;
			if (flag)
			{
				throw new Exception("Compiled assembly is null.");
			}
			object obj = assembly.CreateInstance("ClockWorkDynamicForms.ClockWorkRowScript");
			Type type = obj.GetType();
			object obj2 = type.InvokeMember("TableAction", BindingFlags.InvokeMethod, null, obj, this.ReportDAO.GetReportCodeCompileParameters(CurrentReportResult));
			bool flag2 = obj2 != null && obj2 is DataTable;
			if (flag2)
			{
				DataTable table = (DataTable)obj2;
				runReportResult.PrimaryData = new RunFunctionData
				{
					Table = table,
					AddToAdditionalData = false,
					IsPrimary = true,
					Name = "primary"
				};
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0004498D File Offset: 0x00042B8D
		public void CompileCode(string code)
		{
			this.CompileCodeToAssembly(code);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00044998 File Offset: 0x00042B98
		private Assembly CompileCodeToAssembly(string cCode)
		{
			IList<ReportCompileLineWarningOrError> list;
			Assembly assembly = this.CompileCodeToAssembly(cCode, out list, false);
			bool flag = assembly == null;
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Error Compiling Expression: ");
				foreach (ReportCompileLineWarningOrError reportCompileLineWarningOrError in list)
				{
					stringBuilder.AppendFormat("{0}: {1}\n", reportCompileLineWarningOrError.LineNumber.ToString(), reportCompileLineWarningOrError.Message ?? "");
				}
				throw new Exception("Error Compiling Expression: " + stringBuilder.ToString());
			}
			return assembly;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00044A58 File Offset: 0x00042C58
		private Assembly CompileCodeToAssembly(string cCode, out IList<ReportCompileLineWarningOrError> WarningsOrErrors, bool forceRecompile = false)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = "DynamicCompileCode_" + cCode.GetHashCode().ToString();
			bool flag = !forceRecompile;
			if (flag)
			{
				object obj = cacheStorageManager[key];
				bool flag2 = obj != null;
				if (flag2)
				{
					DynamicCompileAssembly dynamicCompileAssembly = (DynamicCompileAssembly)obj;
					bool flag3 = cCode.Equals(dynamicCompileAssembly.Code);
					if (flag3)
					{
						WarningsOrErrors = null;
						return dynamicCompileAssembly.Assembly;
					}
					cacheStorageManager.Remove(key);
				}
			}
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v4.0"
				}
			};
			ICodeCompiler codeCompiler = new CSharpCodeProvider(providerOptions).CreateCompiler();
			CompilerParameters compilerParameters = new CompilerParameters();
			List<string> list = new List<string>();
			bool flag4 = cCode.StartsWith("import ") || cCode.StartsWith("imports ");
			string text;
			if (flag4)
			{
				using (StringReader stringReader = new StringReader(cCode))
				{
					text = "";
					string text2;
					while ((text2 = stringReader.ReadLine()) != null)
					{
						bool flag5 = text2.StartsWith("import ");
						int num;
						if (flag5)
						{
							num = 7;
						}
						else
						{
							bool flag6 = text2.StartsWith("imports ");
							if (flag6)
							{
								num = 8;
							}
							else
							{
								num = 0;
							}
						}
						bool flag7 = num > 0;
						if (flag7)
						{
							string text3 = text2.Substring(num).Trim();
							bool flag8 = text3.EndsWith(";");
							if (flag8)
							{
								text3 = text3.Substring(0, text3.Length - 1);
							}
							list.Add(text3);
						}
						else
						{
							text = text + text2 + Environment.NewLine;
						}
					}
				}
			}
			else
			{
				text = cCode;
			}
			compilerParameters.ReferencedAssemblies.Add("system.dll");
			compilerParameters.ReferencedAssemblies.Add("system.data.dll");
			compilerParameters.ReferencedAssemblies.Add("system.xml.dll");
			compilerParameters.ReferencedAssemblies.Add("ClockWorkAPI.dll");
			compilerParameters.ReferencedAssemblies.Add("AutoComboBox.dll");
			compilerParameters.ReferencedAssemblies.Add("DynamicScreens.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			compilerParameters.ReferencedAssemblies.Add("UnivOleDb.dll");
			compilerParameters.ReferencedAssemblies.Add("EncryptionClassLibrary.dll");
			compilerParameters.ReferencedAssemblies.Add("Databases.dll");
			compilerParameters.ReferencedAssemblies.Add("ReportFunctions.dll");
			compilerParameters.ReferencedAssemblies.Add("ImportExportClassLibrary.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Data.OracleClient.dll");
			compilerParameters.ReferencedAssemblies.Add("System.DirectoryServices.dll");
			compilerParameters.ReferencedAssemblies.Add("System.DirectoryServices.Protocols.dll");
			compilerParameters.ReferencedAssemblies.Add("ClockWorkWebAPI.dll");
			compilerParameters.ReferencedAssemblies.Add("ClockWorkServer.Contracts.dll");
			foreach (string value in list)
			{
				compilerParameters.ReferencedAssemblies.Add(value);
			}
			compilerParameters.GenerateExecutable = false;
			compilerParameters.GenerateInMemory = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("using System; \n");
			stringBuilder.Append("using System.Data; \n");
			stringBuilder.Append("using System.Data.SqlClient; \n");
			stringBuilder.Append("using System.Data.OleDb; \n");
			stringBuilder.Append("using System.Xml; \n");
			stringBuilder.Append("using System.Windows.Forms; \n");
			stringBuilder.Append("using UnivOleDb; \n");
			stringBuilder.Append("using EncryptionClassLibrary; \n");
			stringBuilder.Append("using System.Collections; \n");
			stringBuilder.Append("using System.Collections.Generic; \n");
			stringBuilder.Append("using ReportFunctions; \n");
			stringBuilder.Append("using System.DirectoryServices; \n");
			stringBuilder.Append("using System.DirectoryServices.Protocols; \n");
			stringBuilder.Append("namespace ClockWorkDynamicForms { \n");
			stringBuilder.Append("  public class ClockWorkRowScript { \n");
			stringBuilder.Append("  public ClockWorkRowScript( ) { } \n");
			stringBuilder.Append("    public static DataTable TableAction( System.Data.DataTable t, string dvSortString, DataTable[] otherTables, string cs, IEncryption tripleDES, Dictionary<string,object> variables ) {\n");
			stringBuilder.Append("var tripleDes = tripleDES is TripleDES ? ((TripleDES) tripleDES).GetTripleDesEncryptionClass() : null;\n");
			stringBuilder.Append("DataView dv = new DataView( t ); dv.Sort = dvSortString;");
			stringBuilder.Append("UnivDataAdapter da; if ( ! String.IsNullOrEmpty( cs ) ) { UnivConnection conn = UnivOleDbFactory.CreateConnection(cs); da = conn.CreateDataAdapter(); } else { da = null; } \n");
			string[] array = stringBuilder.ToString().Split(new char[]
			{
				'\n'
			});
			int num2 = array.Length;
			stringBuilder.Append(text);
			stringBuilder.Append("    }\n");
			OperationContext opContext = this.OpContext;
			string text4 = (opContext != null) ? opContext.AppContext.ExecutingPath : null;
			bool flag9 = !string.IsNullOrEmpty(text4);
			if (flag9)
			{
				compilerParameters.CompilerOptions = string.Format("/lib:\"{0}\"", text4);
			}
			CWLogger.Logger.Trace("DynamicCompileManager:CompileCodeToAssembly:BinPath={0}", text4 ?? "NULL");
			stringBuilder.Append(" } }");
			string source = stringBuilder.ToString();
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParameters, source);
			bool hasErrors = compilerResults.Errors.HasErrors;
			Assembly result;
			if (hasErrors)
			{
				WarningsOrErrors = new List<ReportCompileLineWarningOrError>();
				foreach (object obj2 in compilerResults.Errors)
				{
					CompilerError compilerError = (CompilerError)obj2;
					int lineNumber = compilerError.Line - num2;
					WarningsOrErrors.Add(new ReportCompileLineWarningOrError
					{
						Message = compilerError.ErrorText,
						LineType = eReportCompileLineWarningOrErrorType.Error,
						LineNumber = lineNumber
					});
				}
				result = null;
			}
			else
			{
				WarningsOrErrors = null;
				Assembly compiledAssembly = compilerResults.CompiledAssembly;
				cacheStorageManager.Insert(key, new DynamicCompileAssembly
				{
					Assembly = compiledAssembly,
					Code = cCode
				}, new TimeSpan(0, 1, 0));
				result = compiledAssembly;
			}
			return result;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00045048 File Offset: 0x00043248
		public IList<ReportCompileLineWarningOrError> TryCompileCode(string code)
		{
			IList<ReportCompileLineWarningOrError> list;
			Assembly left = this.CompileCodeToAssembly(code, out list, true);
			bool flag = left == null && (list == null || list.Count < 1);
			if (flag)
			{
				list = new List<ReportCompileLineWarningOrError>
				{
					new ReportCompileLineWarningOrError
					{
						LineType = eReportCompileLineWarningOrErrorType.Error,
						Message = "Unknown error"
					}
				};
			}
			else
			{
				bool flag2 = left != null && list != null;
				if (flag2)
				{
					list = null;
				}
			}
			return list;
		}

		// Token: 0x040001CC RID: 460
		private IDynamicCompileDAO dao;

		// Token: 0x040001CD RID: 461
		private IReportDAO rdao;
	}
}
