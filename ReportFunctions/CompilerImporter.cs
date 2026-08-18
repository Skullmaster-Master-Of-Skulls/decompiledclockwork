using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using EncryptionClassLibrary;
using Microsoft.CSharp;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000033 RID: 51
	public class CompilerImporter
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0003D890 File Offset: 0x0003C890
		public bool NeedsRecompile(string codeString)
		{
			return !this.codeString.Equals(codeString);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0003D8B1 File Offset: 0x0003C8B1
		public CompilerImporter(string codeString)
		{
			this.codeString = codeString;
			this._Compiled = null;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0003D8D8 File Offset: 0x0003C8D8
		public static Exception ExecuteCodeString(string codeString, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report)
		{
			if (CompilerImporter.compilerImporter == null)
			{
				CompilerImporter.compilerImporter = new CompilerImporter(codeString);
			}
			return CompilerImporter.compilerImporter.ExecuteCode(codeString, da, tripleDES, ref report);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0003D914 File Offset: 0x0003C914
		public static Exception CompileCodeString(string codeString)
		{
			Exception result;
			try
			{
				Assembly assembly = CompilerImporter.CompileCode(codeString);
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0003D948 File Offset: 0x0003C948
		public Exception ExecuteCode(string codeString, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report)
		{
			Exception result;
			try
			{
				if (this.NeedsRecompile(codeString) || this._Compiled == null)
				{
					this.codeString = codeString;
					this.assembly = CompilerImporter.CompileCode(codeString);
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

		// Token: 0x06000313 RID: 787 RVA: 0x0003DA8C File Offset: 0x0003CA8C
		public static Assembly CompileCode(string codeString)
		{
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v3.5"
				}
			};
			ICodeCompiler codeCompiler = new CSharpCodeProvider(providerOptions).CreateCompiler();
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.ReferencedAssemblies.Add("system.dll");
			compilerParameters.ReferencedAssemblies.Add("system.data.dll");
			compilerParameters.ReferencedAssemblies.Add("system.xml.dll");
			compilerParameters.ReferencedAssemblies.Add("ClockWorkAPI.dll");
			compilerParameters.ReferencedAssemblies.Add("AutoComboBox.dll");
			compilerParameters.ReferencedAssemblies.Add("DynamicScreens.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			compilerParameters.ReferencedAssemblies.Add("UnivOleDb.dll");
			compilerParameters.ReferencedAssemblies.Add("EncryptionClassLibrary.dll");
			compilerParameters.ReferencedAssemblies.Add("TechnoPro.ClockWork.ClockWorkMigration.dll");
			compilerParameters.ReferencedAssemblies.Add("Databases.dll");
			compilerParameters.ReferencedAssemblies.Add("ReportFunctions.dll");
			compilerParameters.ReferencedAssemblies.Add("ImportExportClassLibrary.dll");
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
			stringBuilder.Append("using TechnoPro.ClockWork.ClockWorkMigration; \n");
			stringBuilder.Append("using System.Data.Common; \n");
			stringBuilder.Append("using ReportFunctions; \n");
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
			stringBuilder.Append(codeString);
			stringBuilder.Append("    }\n");
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

		// Token: 0x0400017B RID: 379
		private Assembly assembly;

		// Token: 0x0400017C RID: 380
		private object _Compiled;

		// Token: 0x0400017D RID: 381
		private string codeString = "";

		// Token: 0x0400017E RID: 382
		private static CompilerImporter compilerImporter = null;
	}
}
