using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using EncryptionClassLibrary;
using Microsoft.CSharp;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000031 RID: 49
	public class CompilerReports
	{
		// Token: 0x06000305 RID: 773 RVA: 0x0003D098 File Offset: 0x0003C098
		public bool NeedsRecompile(string codeString)
		{
			return !this.codeString.Equals(codeString);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0003D0B9 File Offset: 0x0003C0B9
		public CompilerReports(string codeString)
		{
			this.codeString = codeString;
			this._Compiled = null;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0003D0E0 File Offset: 0x0003C0E0
		public static Exception ExecuteCodeString(string codeString, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report)
		{
			if (CompilerReports.compilerReports == null)
			{
				CompilerReports.compilerReports = new CompilerReports(codeString);
			}
			return CompilerReports.compilerReports.ExecuteCode(codeString, da, tripleDES, ref report);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0003D11C File Offset: 0x0003C11C
		public static Exception CompileCodeString(string codeString)
		{
			Exception result;
			try
			{
				Assembly assembly = CompilerReports.CompileCode(codeString);
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0003D150 File Offset: 0x0003C150
		public Exception ExecuteCode(string codeString, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report)
		{
			Exception result;
			try
			{
				CompilerReports.CompilerEnvironment compilerEnvironment = CompilerReports.currentCompilerEnvironment;
				if (compilerEnvironment != CompilerReports.CompilerEnvironment.Web)
				{
					Directory.SetCurrentDirectory(Application.StartupPath);
				}
				if (this.NeedsRecompile(codeString) || this._Compiled == null)
				{
					this.codeString = codeString;
					this.assembly = CompilerReports.CompileCode(codeString);
					this._Compiled = this.assembly.CreateInstance("ClockWorkDynamicForms.ClockWorkRowScript");
				}
				Type type = this._Compiled.GetType();
				DataView currentDataView = report.GetCurrentDataView();
				DataTable[] tablesExceptCurrent = report.GetTablesExceptCurrent();
				DataTable dataTable = (currentDataView == null) ? null : currentDataView.Table;
				object obj = type.InvokeMember("TableAction", BindingFlags.InvokeMethod, null, this._Compiled, new object[]
				{
					dataTable,
					(currentDataView == null) ? "" : currentDataView.Sort,
					tablesExceptCurrent,
					da.Connection.ConnectionString,
					tripleDES,
					report.RememberedVariables2
				});
				if (obj != null && obj is DataTable)
				{
					dataTable = (DataTable)obj;
					report.AddResult(dataTable.DefaultView);
				}
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x1700006D RID: 109
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0003D2B8 File Offset: 0x0003C2B8
		public static string BinPath
		{
			set
			{
				CompilerReports.binPath = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (set) Token: 0x0600030B RID: 779 RVA: 0x0003D2C1 File Offset: 0x0003C2C1
		public static CompilerReports.CompilerEnvironment CurrentCompilerEnvironment
		{
			set
			{
				CompilerReports.currentCompilerEnvironment = value;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0003D2CC File Offset: 0x0003C2CC
		public static Assembly CompileCode(string CodeString)
		{
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v3.5"
				}
			};
			CompilerReports.CompilerEnvironment compilerEnvironment = CompilerReports.currentCompilerEnvironment;
			ICodeCompiler codeCompiler = new CSharpCodeProvider(providerOptions).CreateCompiler();
			CompilerParameters compilerParameters = new CompilerParameters();
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			string text;
			if (CodeString.StartsWith("import ") || CodeString.StartsWith("imports ") || CodeString.StartsWith("using "))
			{
				bool flag = false;
				using (StringReader stringReader = new StringReader(CodeString))
				{
					text = "";
					string text2;
					while ((text2 = stringReader.ReadLine()) != null)
					{
						if (!flag && text2.StartsWith("import "))
						{
							list.Add(text2.Substring(7));
						}
						else if (!flag && text2.StartsWith("imports "))
						{
							list.Add(text2.Substring(8));
						}
						else if (!flag && text2.StartsWith("using "))
						{
							list2.Add(text2);
						}
						else
						{
							text = text + text2 + Environment.NewLine;
							flag = true;
						}
					}
				}
			}
			else
			{
				text = CodeString;
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
			foreach (string value2 in list2)
			{
				stringBuilder.Append(value2);
				stringBuilder.Append("\r\n");
			}
			stringBuilder.Append("namespace ClockWorkDynamicForms { \n");
			stringBuilder.Append("  public class ClockWorkRowScript { \n");
			stringBuilder.Append("  public ClockWorkRowScript( ) { } \n");
			stringBuilder.Append("    public static DataTable TableAction( System.Data.DataTable t, string dvSortString, DataTable[] otherTables, string cs, TripleDESEncryptionClass tripleDES, List<Variable> variables ) {\n");
			stringBuilder.Append("DataView dv = new DataView( t ); dv.Sort = dvSortString;");
			stringBuilder.Append("UnivDataAdapter da; if ( ! String.IsNullOrEmpty( cs ) ) { UnivConnection conn = UnivOleDbFactory.CreateConnection(cs); da = conn.CreateDataAdapter(); } else { da = null; } \n");
			string[] array = stringBuilder.ToString().Split(new char[]
			{
				'\n'
			});
			int num = array.Length;
			stringBuilder.Append(text);
			stringBuilder.Append("    }\n");
			if (compilerEnvironment == CompilerReports.CompilerEnvironment.Web)
			{
				compilerParameters.CompilerOptions = string.Format("/lib:{0}", CompilerReports.binPath);
			}
			stringBuilder.Append(" } }");
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParameters, stringBuilder.ToString());
			if (compilerResults.Errors.HasErrors)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("Error Compiling Expression: ");
				foreach (object obj in compilerResults.Errors)
				{
					CompilerError compilerError = (CompilerError)obj;
					stringBuilder2.AppendFormat("{0}: {1}\n", (compilerError.Line - num).ToString(), compilerError.ErrorText);
				}
				throw new Exception("Error Compiling Expression: " + stringBuilder2.ToString());
			}
			return compilerResults.CompiledAssembly;
		}

		// Token: 0x04000172 RID: 370
		private Assembly assembly;

		// Token: 0x04000173 RID: 371
		private object _Compiled;

		// Token: 0x04000174 RID: 372
		private string codeString = "";

		// Token: 0x04000175 RID: 373
		private static CompilerReports compilerReports = null;

		// Token: 0x04000176 RID: 374
		private static string binPath = "";

		// Token: 0x04000177 RID: 375
		private static CompilerReports.CompilerEnvironment currentCompilerEnvironment = CompilerReports.CompilerEnvironment.Executable;

		// Token: 0x02000032 RID: 50
		public enum CompilerEnvironment
		{
			// Token: 0x04000179 RID: 377
			Executable,
			// Token: 0x0400017A RID: 378
			Web
		}
	}
}
