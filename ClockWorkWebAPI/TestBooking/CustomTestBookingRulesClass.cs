using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public class CustomTestBookingRulesClass
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000106AC File Offset: 0x0000E8AC
		// (set) Token: 0x06000278 RID: 632 RVA: 0x000106C4 File Offset: 0x0000E8C4
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

		// Token: 0x06000279 RID: 633 RVA: 0x000106D0 File Offset: 0x0000E8D0
		public bool NeedsRecompile(string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid)
		{
			return !this.code_FindPotentialBookingsStart.Equals(code_FindPotentialBookingsStart) || !this.code_FindPotentialBookingsEnd.Equals(code_FindPotentialBookingsEnd) || !this.code_FindPotentialBookingsMid.Equals(code_FindPotentialBookingsMid);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00010710 File Offset: 0x0000E910
		public CustomTestBookingRulesClass(string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid)
		{
			this.code_FindPotentialBookingsStart = code_FindPotentialBookingsStart;
			this.code_FindPotentialBookingsEnd = code_FindPotentialBookingsEnd;
			this.code_FindPotentialBookingsMid = code_FindPotentialBookingsMid;
			this._Compiled = null;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00010770 File Offset: 0x0000E970
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

		// Token: 0x0600027C RID: 636 RVA: 0x000107B0 File Offset: 0x0000E9B0
		public static CustomTestBookingRulesClass GetCustomRules()
		{
			return CustomTestBookingRulesClass.instance;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000107C8 File Offset: 0x0000E9C8
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

		// Token: 0x0600027E RID: 638 RVA: 0x000107FC File Offset: 0x0000E9FC
		public Assembly CompileCode(string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid)
		{
			return CustomTestBookingRulesClass.CompileCode(this.binPath, code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0001081C File Offset: 0x0000EA1C
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

		// Token: 0x06000280 RID: 640 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		public object FindPotentialBookingsStart(string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid, ref List<PotentialTest> potentialTests, ref List<Rule> rules, ref List<Asset> assets, ref List<PotentialRoom> rooms, FindPotentialBookingInfo pbookingInfo, out Exception ex, params object[] additionalParameters)
		{
			object result;
			try
			{
				bool flag = this.NeedsRecompile(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid) || this._Compiled == null;
				if (flag)
				{
					this.code_FindPotentialBookingsStart = code_FindPotentialBookingsStart;
					this.code_FindPotentialBookingsEnd = code_FindPotentialBookingsEnd;
					this.code_FindPotentialBookingsMid = code_FindPotentialBookingsMid;
					this.assembly = this.CompileCode(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid);
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

		// Token: 0x06000281 RID: 641 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		public object FindPotentialBookingsEnd(string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid, ref List<PotentialTest> potentialTests, ref List<Rule> rules, ref List<Asset> assets, ref List<PotentialRoom> rooms, FindPotentialBookingInfo pbookingInfo, out Exception ex, params object[] additionalParameters)
		{
			object result;
			try
			{
				bool flag = this.NeedsRecompile(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid) || this._Compiled == null;
				if (flag)
				{
					this.code_FindPotentialBookingsStart = code_FindPotentialBookingsStart;
					this.code_FindPotentialBookingsEnd = code_FindPotentialBookingsEnd;
					this.code_FindPotentialBookingsMid = code_FindPotentialBookingsMid;
					this.assembly = this.CompileCode(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid);
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

		// Token: 0x06000282 RID: 642 RVA: 0x00010DCC File Offset: 0x0000EFCC
		public object FindPotentialBookingsMid(string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid, char currentContext, ref List<DateRange> timesToInvestigate, Rule rule, ref List<PotentialTest> potentialTestsToAdd, ref List<PotentialTest> potentialTests, ref List<Rule> rules, ref List<Asset> assets, ref List<PotentialRoom> rooms, FindPotentialBookingInfo pbookingInfo, out Exception ex, params object[] additionalParameters)
		{
			object result;
			try
			{
				bool flag = this.NeedsRecompile(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid) || this._Compiled == null;
				if (flag)
				{
					this.code_FindPotentialBookingsStart = code_FindPotentialBookingsStart;
					this.code_FindPotentialBookingsEnd = code_FindPotentialBookingsEnd;
					this.code_FindPotentialBookingsMid = code_FindPotentialBookingsMid;
					this.assembly = this.CompileCode(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid);
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

		// Token: 0x0400015F RID: 351
		private Assembly assembly;

		// Token: 0x04000160 RID: 352
		private object _Compiled;

		// Token: 0x04000161 RID: 353
		private string code_FindPotentialBookingsStart = "";

		// Token: 0x04000162 RID: 354
		private string code_FindPotentialBookingsEnd = "";

		// Token: 0x04000163 RID: 355
		private string code_FindPotentialBookingsMid = "";

		// Token: 0x04000164 RID: 356
		private string binPath = "\\bin\\";

		// Token: 0x04000165 RID: 357
		private static CustomTestBookingRulesClass instance;
	}
}
