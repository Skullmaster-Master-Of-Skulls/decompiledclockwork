using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DynamicCompiler.CompilerArgs;

namespace TechnoPro.Common.DynamicCompiler
{
	// Token: 0x02000002 RID: 2
	public class CustomCompiler<P, R> where P : ICompilerParameters where R : ICompilerReturnValue
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ThreadSafeDictionary<CustomCodeCacheKey, CustomCodeCacheValue> CachedCompilers
		{
			get
			{
				ThreadSafeDictionary<CustomCodeCacheKey, CustomCodeCacheValue> result;
				if ((result = CustomCompiler<P, R>._cachedCompilers) == null)
				{
					result = (CustomCompiler<P, R>._cachedCompilers = new ThreadSafeDictionary<CustomCodeCacheKey, CustomCodeCacheValue>());
				}
				return result;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
		public CustomCompiler(CustomCSharpCode code, eCustomCompilerType compilerType, string compilerTypeSecondary = "")
		{
			this._customCompilerEnvironment = new CustomCompilerEnvironment(compilerType, compilerTypeSecondary);
			this._cSharpCode = code;
			bool flag = !string.IsNullOrEmpty(this._cSharpCode.Code);
			if (!flag)
			{
				IList<string> imports;
				string defaultCode = this.GetDefaultCode(out imports);
				this._cSharpCode.Code = defaultCode;
				this._cSharpCode.Imports = imports;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		public string GetDefaultCode(out IList<string> imports)
		{
			return this._customCompilerEnvironment.GetDefaultCode(typeof(R), typeof(P), out imports);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002114 File Offset: 0x00000314
		private static CustomCodeCacheValue GetAssemblyFromCache(CustomCodeCacheKey key)
		{
			ThreadSafeDictionary<CustomCodeCacheKey, CustomCodeCacheValue> cachedCompilers = CustomCompiler<P, R>.CachedCompilers;
			return (!cachedCompilers.ContainsKey(key)) ? null : cachedCompilers[key];
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002140 File Offset: 0x00000340
		private static void SetAssemblyToCache(CustomCodeCacheKey key, CustomCodeCacheValue val)
		{
			ThreadSafeDictionary<CustomCodeCacheKey, CustomCodeCacheValue> cachedCompilers = CustomCompiler<P, R>.CachedCompilers;
			bool flag = cachedCompilers.ContainsKey(key);
			if (flag)
			{
				cachedCompilers.Remove(key);
			}
			cachedCompilers.Add(key, val);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002170 File Offset: 0x00000370
		private static void ClearCurrentCodeCacheKey(CustomCodeCacheKey key)
		{
			ThreadSafeDictionary<CustomCodeCacheKey, CustomCodeCacheValue> cachedCompilers = CustomCompiler<P, R>.CachedCompilers;
			bool flag = cachedCompilers.ContainsKey(key);
			if (flag)
			{
				cachedCompilers.Remove(key);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002198 File Offset: 0x00000398
		public CustomCompileResult CompileCode(string binPath)
		{
			CustomCodeCacheKey currentCodeCacheKey = this.GetCurrentCodeCacheKey();
			CustomCodeCacheValue assemblyFromCache = CustomCompiler<P, R>.GetAssemblyFromCache(currentCodeCacheKey);
			bool flag = assemblyFromCache != null;
			CustomCompileResult result;
			if (flag)
			{
				this._assembly = assemblyFromCache.Assembly;
				result = new CustomCompileResult
				{
					Success = true
				};
			}
			else
			{
				CustomCompiler<P, R>.ClearCurrentCodeCacheKey(currentCodeCacheKey);
				CustomCompileResult customCompileResult = this.CompileCodeToAssembly(binPath);
				bool success = customCompileResult.Success;
				if (success)
				{
					this._assembly = customCompileResult.Assembly;
					CustomCompiler<P, R>.SetAssemblyToCache(currentCodeCacheKey, new CustomCodeCacheValue
					{
						Assembly = this._assembly
					});
				}
				else
				{
					this._assembly = null;
				}
				result = customCompileResult;
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000222C File Offset: 0x0000042C
		private CustomCodeCacheKey GetCurrentCodeCacheKey()
		{
			return new CustomCodeCacheKey
			{
				Code = this._cSharpCode,
				CompilerType = this._customCompilerEnvironment.CompilerTypeString,
				CompilerTypeSecondary = this._customCompilerEnvironment.CompilerTypeSecondary
			};
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002274 File Offset: 0x00000474
		public R ExecuteCode(string BinPath, P CodeParameters, out CustomCompileResult result)
		{
			result = this.CompileCode(BinPath);
			bool flag = !result.Success;
			R result2;
			if (flag)
			{
				result2 = default(R);
			}
			else
			{
				object obj = this._assembly.CreateInstance(string.Format("{0}.{1}", this._customCompilerEnvironment.CodeNamespace, this._customCompilerEnvironment.CodeClassName));
				Type type = obj.GetType();
				object obj2 = type.InvokeMember("CustomEntry", BindingFlags.InvokeMethod, null, obj, new object[]
				{
					CodeParameters
				});
				result2 = (R)((object)obj2);
			}
			return result2;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002308 File Offset: 0x00000508
		private CustomCompileResult CompileCodeToAssembly(string binPath)
		{
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v4.0"
				}
			};
			ICodeCompiler codeCompiler = new CSharpCodeProvider(providerOptions).CreateCompiler();
			CompilerParameters compilerParameters = new CompilerParameters
			{
				WarningLevel = 3,
				CompilerOptions = (string.IsNullOrEmpty(binPath) ? "/define:NET45" : ("/lib:\"" + binPath + "\" /define:NET45"))
			};
			foreach (string value in this._customCompilerEnvironment.DefaultImports)
			{
				compilerParameters.ReferencedAssemblies.Add(value);
			}
			IEnumerable<string> enumerable = from g in this._cSharpCode.Imports
			where !this._customCompilerEnvironment.DefaultImports.Contains(g)
			select g;
			foreach (string text in enumerable)
			{
				bool flag = text.Equals("oracle.dataaccess.dll", StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					compilerParameters.ReferencedAssemblies.Add("Oracle.ManagedDataAccess.dll");
				}
				else
				{
					compilerParameters.ReferencedAssemblies.Add(text);
				}
			}
			compilerParameters.GenerateExecutable = false;
			compilerParameters.GenerateInMemory = true;
			bool flag2 = !string.IsNullOrEmpty(binPath);
			if (flag2)
			{
				compilerParameters.CompilerOptions = "/lib:\"" + binPath + "\" /define:NET45";
			}
			string text2 = this._cSharpCode.Code;
			text2 = text2.Replace("Oracle.DataAccess", "Oracle.ManagedDataAccess");
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParameters, text2);
			List<CustomCompileMessage> warnings = new List<CustomCompileMessage>();
			List<CustomCompileMessage> list = new List<CustomCompileMessage>();
			bool hasWarnings = compilerResults.Errors.HasWarnings;
			if (hasWarnings)
			{
				list.AddRange(from CompilerError err in compilerResults.Errors
				where err.IsWarning
				select new CustomCompileMessage
				{
					LineNumber = err.Line,
					ColumnNumber = err.Column,
					Title = err.ErrorText,
					Filename = err.FileName,
					MessageType = eCustomCompileMessageType.Warning
				});
			}
			bool hasErrors = compilerResults.Errors.HasErrors;
			CustomCompileResult result;
			if (hasErrors)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Error Compiling Expression: ");
				foreach (object obj in compilerResults.Errors)
				{
					CompilerError compilerError = (CompilerError)obj;
					bool isWarning = compilerError.IsWarning;
					if (!isWarning)
					{
						int line = compilerError.Line;
						stringBuilder.AppendFormat("{0}: {1}\n", line.ToString(), compilerError.ErrorText);
						list.Add(new CustomCompileMessage
						{
							LineNumber = line,
							ColumnNumber = compilerError.Column,
							Title = compilerError.ErrorText,
							Filename = compilerError.FileName,
							MessageType = eCustomCompileMessageType.Error
						});
					}
				}
				result = new CustomCompileResult
				{
					Success = false,
					ErrorMessage = stringBuilder.ToString(),
					Errors = list,
					Warnings = warnings
				};
			}
			else
			{
				this._assembly = compilerResults.CompiledAssembly;
				result = new CustomCompileResult
				{
					Assembly = this._assembly,
					Success = (this._assembly != null),
					Warnings = warnings,
					Errors = list
				};
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private static ThreadSafeDictionary<CustomCodeCacheKey, CustomCodeCacheValue> _cachedCompilers;

		// Token: 0x04000002 RID: 2
		private readonly CustomCSharpCode _cSharpCode;

		// Token: 0x04000003 RID: 3
		private readonly CustomCompilerEnvironment _customCompilerEnvironment;

		// Token: 0x04000004 RID: 4
		private Assembly _assembly;
	}
}
