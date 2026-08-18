using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x0200053B RID: 1339
	[Serializable]
	public class CustomTestBookingRulesClass
	{
		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x0002D118 File Offset: 0x0002B318
		// (set) Token: 0x06002AA4 RID: 10916 RVA: 0x0002D130 File Offset: 0x0002B330
		public string Code_FindPotentialBookingsStart
		{
			get
			{
				return this.code_FindPotentialBookingsStart;
			}
			set
			{
				this.code_FindPotentialBookingsStart = value;
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x0002D13C File Offset: 0x0002B33C
		// (set) Token: 0x06002AA6 RID: 10918 RVA: 0x0002D154 File Offset: 0x0002B354
		public string Code_FindPotentialBookingsEnd
		{
			get
			{
				return this.code_FindPotentialBookingsEnd;
			}
			set
			{
				this.code_FindPotentialBookingsEnd = value;
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x0002D160 File Offset: 0x0002B360
		// (set) Token: 0x06002AA8 RID: 10920 RVA: 0x0002D178 File Offset: 0x0002B378
		public string Code_FindPotentialBookingsMid
		{
			get
			{
				return this.code_FindPotentialBookingsMid;
			}
			set
			{
				this.code_FindPotentialBookingsMid = value;
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x0002D184 File Offset: 0x0002B384
		// (set) Token: 0x06002AAA RID: 10922 RVA: 0x0002D19C File Offset: 0x0002B39C
		public string BinPath
		{
			get
			{
				return this.binPath;
			}
			set
			{
				this.binPath = value;
			}
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0002D1A8 File Offset: 0x0002B3A8
		public bool NeedsRecompile(string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid)
		{
			return !this.code_FindPotentialBookingsStart.Equals(code_FindPotentialBookingsStart) || !this.code_FindPotentialBookingsEnd.Equals(code_FindPotentialBookingsEnd) || !this.code_FindPotentialBookingsMid.Equals(code_FindPotentialBookingsMid);
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0002D1E8 File Offset: 0x0002B3E8
		public CustomTestBookingRulesClass()
		{
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x0002D220 File Offset: 0x0002B420
		public CustomTestBookingRulesClass(string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid)
		{
			this.code_FindPotentialBookingsStart = code_FindPotentialBookingsStart;
			this.code_FindPotentialBookingsEnd = code_FindPotentialBookingsEnd;
			this.code_FindPotentialBookingsMid = code_FindPotentialBookingsMid;
			this._Compiled = null;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x0002D280 File Offset: 0x0002B480
		public static CustomTestBookingRulesClass GetCustomRules(string binPath, string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid)
		{
			bool flag = CustomTestBookingRulesClass.instance == null;
			if (flag)
			{
				CustomTestBookingRulesClass.instance = new CustomTestBookingRulesClass(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid);
				CustomTestBookingRulesClass.instance.BinPath = binPath;
			}
			return CustomTestBookingRulesClass.instance;
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x0002D2C0 File Offset: 0x0002B4C0
		public static CustomTestBookingRulesClass GetCustomRules()
		{
			return CustomTestBookingRulesClass.instance;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0002D2D8 File Offset: 0x0002B4D8
		public static Exception CompileCodeString(string binPath, string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid)
		{
			Exception result;
			try
			{
				Assembly assembly = CustomTestBookingRulesClass.CompileCode(binPath, code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid);
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x0002D30C File Offset: 0x0002B50C
		public Assembly CompileCode(string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid)
		{
			return CustomTestBookingRulesClass.CompileCode(this.binPath, code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x0002D32C File Offset: 0x0002B52C
		public static Assembly CompileCode(string binPath, string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid)
		{
			bool flag = string.IsNullOrEmpty(code_FindPotentialBookingsStart) && string.IsNullOrEmpty(code_FindPotentialBookingsEnd) && string.IsNullOrEmpty(code_FindPotentialBookingsMid);
			Assembly result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ICodeCompiler codeCompiler = new CSharpCodeProvider().CreateCompiler();
				CompilerParameters compilerParameters = new CompilerParameters();
				compilerParameters.ReferencedAssemblies.Add("system.dll");
				compilerParameters.ReferencedAssemblies.Add("system.data.dll");
				compilerParameters.ReferencedAssemblies.Add("system.xml.dll");
				compilerParameters.ReferencedAssemblies.Add(Path.Combine(binPath, "ClockWorkAPI.dll"));
				compilerParameters.ReferencedAssemblies.Add(Path.Combine(binPath, "AutoComboBox.dll"));
				compilerParameters.ReferencedAssemblies.Add(Path.Combine(binPath, "UnivOleDb.dll"));
				compilerParameters.ReferencedAssemblies.Add(Path.Combine(binPath, "EncryptionClassLibrary.dll"));
				compilerParameters.ReferencedAssemblies.Add(Path.Combine(binPath, "ReportFunctions.dll"));
				compilerParameters.ReferencedAssemblies.Add(Path.Combine(binPath, "ImportExportClassLibrary.dll"));
				compilerParameters.ReferencedAssemblies.Add(Path.Combine(binPath, "ClockWorkWebAPI.dll"));
				compilerParameters.GenerateExecutable = false;
				compilerParameters.GenerateInMemory = true;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("using System; \n");
				stringBuilder.Append("using System.Data; \n");
				stringBuilder.Append("using System.Data.SqlClient; \n");
				stringBuilder.Append("using System.Data.OleDb; \n");
				stringBuilder.Append("using System.Xml; \n");
				stringBuilder.Append("using UnivOleDb; \n");
				stringBuilder.Append("using EncryptionClassLibrary; \n");
				stringBuilder.Append("using System.Collections; \n");
				stringBuilder.Append("using System.Collections.Generic; \n");
				stringBuilder.Append("using ClockWorkWebAPI.TestBooking; \n");
				stringBuilder.Append("namespace ClockWorkTestBooking { \n");
				stringBuilder.Append("  public class CustomRulesTestBooking { \n");
				stringBuilder.Append("  public CustomRulesTestBooking( ) { } \n");
				string[] array = stringBuilder.ToString().Split(new char[]
				{
					'\n'
				});
				int num = array.Length;
				stringBuilder.Append("    public object FindPotentialBookingsStart( List<PotentialTest> potentialTests, List<ClockWorkWebAPI.TestBooking.Rule> rules, List<ClockWorkWebAPI.TestBooking.Asset> assets, List<PotentialRoom> rooms, FindPotentialBookingInfo pbookingInfo, out Exception ex, params object[] additionalParameters ) {\n");
				stringBuilder.Append("     try {\n");
				stringBuilder.Append(code_FindPotentialBookingsStart);
				stringBuilder.Append("     ex = null; return null; } catch ( Exception myException ) { ex = myException; return null; } \n");
				stringBuilder.Append("    }\n");
				stringBuilder.Append("    public object FindPotentialBookingsEnd( List<PotentialTest> potentialTests, List<ClockWorkWebAPI.TestBooking.Rule> rules, List<ClockWorkWebAPI.TestBooking.Asset> assets, List<PotentialRoom> rooms, FindPotentialBookingInfo pbookingInfo, out Exception ex, params object[] additionalParameters ) {\n");
				stringBuilder.Append("     try {\n");
				stringBuilder.Append(code_FindPotentialBookingsEnd);
				stringBuilder.Append("     ex = null; return null; } catch ( Exception myException ) { ex = myException; return null; } \n");
				stringBuilder.Append("    }\n");
				stringBuilder.Append("    public object FindPotentialBookingsMid( char currentContext, ref List<DateRange> timesToInvestigate, ClockWorkWebAPI.TestBooking.Rule rule,  List<PotentialTest> potentialTestsToAdd,ref List<PotentialTest> potentialTests, ref List<ClockWorkWebAPI.TestBooking.Rule> rules, List<ClockWorkWebAPI.TestBooking.Asset> assets, List<PotentialRoom> rooms, FindPotentialBookingInfo pbookingInfo, out Exception ex, params object[] additionalParameters  ) {\n");
				stringBuilder.Append("     try {\n");
				stringBuilder.Append(code_FindPotentialBookingsMid);
				stringBuilder.Append("     ex = null; return null; } catch ( Exception myException ) { ex = myException; return null; } \n");
				stringBuilder.Append("    }\n");
				stringBuilder.Append(" } }");
				CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParameters, stringBuilder.ToString());
				bool hasErrors = compilerResults.Errors.HasErrors;
				if (hasErrors)
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
				result = compilerResults.CompiledAssembly;
			}
			return result;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x0002D6C4 File Offset: 0x0002B8C4
		public object FindPotentialBookingsStart(ref IList<PotentialTest> potentialTests, ref IList<TestRule> rules, ref IList<Asset> assets, ref IList<PotentialRoom> rooms, FindPotentialBookingInfo pbookingInfo, out Exception ex, params object[] additionalParameters)
		{
			object result;
			try
			{
				bool flag = this._Compiled == null;
				if (flag)
				{
					this.assembly = this.CompileCode(this.code_FindPotentialBookingsStart, this.code_FindPotentialBookingsEnd, this.code_FindPotentialBookingsMid);
					this._Compiled = this.assembly.CreateInstance("ClockWorkTestBooking.CustomRulesTestBooking");
				}
				bool flag2 = this._Compiled != null && this.assembly != null;
				if (flag2)
				{
					Type type = this._Compiled.GetType();
					ex = null;
					object[] args = new object[]
					{
						potentialTests,
						rules,
						assets,
						rooms,
						pbookingInfo,
						ex,
						additionalParameters
					};
					object obj = type.InvokeMember("FindPotentialBookingsStart", BindingFlags.InvokeMethod, null, this._Compiled, args);
					result = obj;
				}
				else
				{
					ex = null;
					result = null;
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = null;
			}
			return result;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
		public object FindPotentialBookingsEnd(ref IList<PotentialTest> potentialTests, ref IList<TestRule> rules, ref IList<Asset> assets, ref IList<PotentialRoom> rooms, FindPotentialBookingInfo pbookingInfo, out Exception ex, params object[] additionalParameters)
		{
			object result;
			try
			{
				bool flag = this._Compiled == null;
				if (flag)
				{
					this.assembly = this.CompileCode(this.code_FindPotentialBookingsStart, this.code_FindPotentialBookingsEnd, this.code_FindPotentialBookingsMid);
					this._Compiled = this.assembly.CreateInstance("ClockWorkTestBooking.CustomRulesTestBooking");
				}
				bool flag2 = this._Compiled != null && this.assembly != null;
				if (flag2)
				{
					Type type = this._Compiled.GetType();
					ex = null;
					object[] args = new object[]
					{
						potentialTests,
						rules,
						assets,
						rooms,
						pbookingInfo,
						ex,
						additionalParameters
					};
					object obj = type.InvokeMember("FindPotentialBookingsEnd", BindingFlags.InvokeMethod, null, this._Compiled, args);
					result = obj;
				}
				else
				{
					ex = null;
					result = null;
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = null;
			}
			return result;
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x0002D8AC File Offset: 0x0002BAAC
		public object FindPotentialBookingsMid(char currentContext, ref IList<DateRange> timesToInvestigate, TestRule rule, ref IList<PotentialTest> potentialTestsToAdd, ref IList<PotentialTest> potentialTests, ref IList<TestRule> rules, ref IList<Asset> assets, ref IList<PotentialRoom> rooms, FindPotentialBookingInfo pbookingInfo, out Exception ex, params object[] additionalParameters)
		{
			object result;
			try
			{
				bool flag = this._Compiled == null;
				if (flag)
				{
					this.assembly = this.CompileCode(this.code_FindPotentialBookingsStart, this.code_FindPotentialBookingsEnd, this.code_FindPotentialBookingsMid);
					this._Compiled = this.assembly.CreateInstance("ClockWorkTestBooking.CustomRulesTestBooking");
				}
				bool flag2 = this._Compiled != null && this.assembly != null;
				if (flag2)
				{
					Type type = this._Compiled.GetType();
					ex = null;
					object[] args = new object[]
					{
						currentContext,
						timesToInvestigate,
						rule,
						potentialTestsToAdd,
						potentialTests,
						rules,
						assets,
						rooms,
						pbookingInfo,
						ex,
						additionalParameters
					};
					object obj = type.InvokeMember("FindPotentialBookingsMid", BindingFlags.InvokeMethod, null, this._Compiled, args);
					result = obj;
				}
				else
				{
					ex = null;
					result = null;
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = null;
			}
			return result;
		}

		// Token: 0x04001E64 RID: 7780
		private Assembly assembly;

		// Token: 0x04001E65 RID: 7781
		private object _Compiled;

		// Token: 0x04001E66 RID: 7782
		private string code_FindPotentialBookingsStart = "";

		// Token: 0x04001E67 RID: 7783
		private string code_FindPotentialBookingsEnd = "";

		// Token: 0x04001E68 RID: 7784
		private string code_FindPotentialBookingsMid = "";

		// Token: 0x04001E69 RID: 7785
		private string binPath = "\\bin\\";

		// Token: 0x04001E6A RID: 7786
		private static CustomTestBookingRulesClass instance;
	}
}
