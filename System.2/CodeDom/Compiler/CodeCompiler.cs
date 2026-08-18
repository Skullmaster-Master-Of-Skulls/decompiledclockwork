using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200066C RID: 1644
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeCompiler : CodeGenerator, ICodeCompiler
	{
		// Token: 0x06003B8B RID: 15243 RVA: 0x000F5E70 File Offset: 0x000F4070
		CompilerResults ICodeCompiler.CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromDom(options, e);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000F5EB4 File Offset: 0x000F40B4
		CompilerResults ICodeCompiler.CompileAssemblyFromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromFile(options, fileName);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000F5EF8 File Offset: 0x000F40F8
		CompilerResults ICodeCompiler.CompileAssemblyFromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromSource(options, source);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000F5F3C File Offset: 0x000F413C
		CompilerResults ICodeCompiler.CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromSourceBatch(options, sources);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000F5F80 File Offset: 0x000F4180
		CompilerResults ICodeCompiler.CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			CompilerResults result;
			try
			{
				foreach (string path in fileNames)
				{
					using (File.OpenRead(path))
					{
					}
				}
				result = this.FromFileBatch(options, fileNames);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000F6008 File Offset: 0x000F4208
		CompilerResults ICodeCompiler.CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromDomBatch(options, ea);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06003B91 RID: 15249
		protected abstract string FileExtension { get; }

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06003B92 RID: 15250
		protected abstract string CompilerName { get; }

		// Token: 0x06003B93 RID: 15251 RVA: 0x000F604C File Offset: 0x000F424C
		internal void Compile(CompilerParameters options, string compilerDirectory, string compilerExe, string arguments, ref string outputFile, ref int nativeReturnValue, string trueArgs)
		{
			string text = null;
			outputFile = options.TempFiles.AddExtension("out");
			string text2 = Path.Combine(compilerDirectory, compilerExe);
			if (File.Exists(text2))
			{
				string trueCmdLine = null;
				if (trueArgs != null)
				{
					trueCmdLine = "\"" + text2 + "\" " + trueArgs;
				}
				nativeReturnValue = Executor.ExecWaitWithCapture(options.SafeUserToken, "\"" + text2 + "\" " + arguments, Environment.CurrentDirectory, options.TempFiles, ref outputFile, ref text, trueCmdLine);
				return;
			}
			throw new InvalidOperationException(SR.GetString("CompilerNotFound", new object[]
			{
				text2
			}));
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000F60E4 File Offset: 0x000F42E4
		protected virtual CompilerResults FromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			return this.FromDomBatch(options, new CodeCompileUnit[]
			{
				e
			});
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x000F6120 File Offset: 0x000F4320
		protected virtual CompilerResults FromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			using (File.OpenRead(fileName))
			{
			}
			return this.FromFileBatch(options, new string[]
			{
				fileName
			});
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x000F618C File Offset: 0x000F438C
		protected virtual CompilerResults FromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			return this.FromSourceBatch(options, new string[]
			{
				source
			});
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x000F61C8 File Offset: 0x000F43C8
		protected virtual CompilerResults FromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (ea == null)
			{
				throw new ArgumentNullException("ea");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			string[] array = new string[ea.Length];
			CompilerResults result = null;
			try
			{
				WindowsImpersonationContext impersonation = Executor.RevertImpersonation();
				try
				{
					for (int i = 0; i < ea.Length; i++)
					{
						if (ea[i] != null)
						{
							this.ResolveReferencedAssemblies(options, ea[i]);
							array[i] = options.TempFiles.AddExtension(i.ToString() + this.FileExtension);
							Stream stream = new FileStream(array[i], FileMode.Create, FileAccess.Write, FileShare.Read);
							try
							{
								using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
								{
									((ICodeGenerator)this).GenerateCodeFromCompileUnit(ea[i], streamWriter, base.Options);
									streamWriter.Flush();
								}
							}
							finally
							{
								stream.Close();
							}
						}
					}
					result = this.FromFileBatch(options, array);
				}
				finally
				{
					Executor.ReImpersonate(impersonation);
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x000F62E4 File Offset: 0x000F44E4
		private void ResolveReferencedAssemblies(CompilerParameters options, CodeCompileUnit e)
		{
			if (e.ReferencedAssemblies.Count > 0)
			{
				foreach (string value in e.ReferencedAssemblies)
				{
					if (!options.ReferencedAssemblies.Contains(value))
					{
						options.ReferencedAssemblies.Add(value);
					}
				}
			}
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x000F635C File Offset: 0x000F455C
		protected virtual CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			string path = null;
			int num = 0;
			CompilerResults compilerResults = new CompilerResults(options.TempFiles);
			SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.ControlEvidence);
			securityPermission.Assert();
			try
			{
				compilerResults.Evidence = options.Evidence;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			bool flag = false;
			if (options.OutputAssembly == null || options.OutputAssembly.Length == 0)
			{
				string fileExtension = options.GenerateExecutable ? "exe" : "dll";
				options.OutputAssembly = compilerResults.TempFiles.AddExtension(fileExtension, !options.GenerateInMemory);
				new FileStream(options.OutputAssembly, FileMode.Create, FileAccess.ReadWrite).Close();
				flag = true;
			}
			compilerResults.TempFiles.AddExtension("pdb");
			string text = this.CmdArgsFromParameters(options) + " " + CodeCompiler.JoinStringArray(fileNames, " ");
			string responseFileCmdArgs = this.GetResponseFileCmdArgs(options, text);
			string trueArgs = null;
			if (responseFileCmdArgs != null)
			{
				trueArgs = text;
				text = responseFileCmdArgs;
			}
			this.Compile(options, Executor.GetRuntimeInstallDirectory(), this.CompilerName, text, ref path, ref num, trueArgs);
			compilerResults.NativeCompilerReturnValue = num;
			if (num != 0 || options.WarningLevel > 0)
			{
				FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				try
				{
					if (fileStream.Length > 0L)
					{
						StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8);
						string text2;
						do
						{
							text2 = streamReader.ReadLine();
							if (text2 != null)
							{
								compilerResults.Output.Add(text2);
								this.ProcessCompilerOutputLine(compilerResults, text2);
							}
						}
						while (text2 != null);
					}
				}
				finally
				{
					fileStream.Close();
				}
				if (num != 0 && flag)
				{
					File.Delete(options.OutputAssembly);
				}
			}
			if (!compilerResults.Errors.HasErrors && options.GenerateInMemory)
			{
				FileStream fileStream2 = new FileStream(options.OutputAssembly, FileMode.Open, FileAccess.Read, FileShare.Read);
				try
				{
					int num2 = (int)fileStream2.Length;
					byte[] array = new byte[num2];
					fileStream2.Read(array, 0, num2);
					SecurityPermission securityPermission2 = new SecurityPermission(SecurityPermissionFlag.ControlEvidence);
					securityPermission2.Assert();
					try
					{
						if (!FileIntegrity.IsEnabled)
						{
							compilerResults.CompiledAssembly = Assembly.Load(array, null, options.Evidence);
							return compilerResults;
						}
						if (!FileIntegrity.IsTrusted(fileStream2.SafeFileHandle))
						{
							throw new IOException(SR.GetString("FileIntegrityCheckFailed", new object[]
							{
								options.OutputAssembly
							}));
						}
						compilerResults.CompiledAssembly = CodeCompiler.LoadImageSkipIntegrityCheck(array, null, options.Evidence);
						return compilerResults;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				finally
				{
					fileStream2.Close();
				}
			}
			compilerResults.PathToAssembly = options.OutputAssembly;
			return compilerResults;
		}

		// Token: 0x06003B9A RID: 15258
		protected abstract void ProcessCompilerOutputLine(CompilerResults results, string line);

		// Token: 0x06003B9B RID: 15259
		protected abstract string CmdArgsFromParameters(CompilerParameters options);

		// Token: 0x06003B9C RID: 15260 RVA: 0x000F660C File Offset: 0x000F480C
		protected virtual string GetResponseFileCmdArgs(CompilerParameters options, string cmdArgs)
		{
			string text = options.TempFiles.AddExtension("cmdline");
			Stream stream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read);
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
				{
					streamWriter.Write(cmdArgs);
					streamWriter.Flush();
				}
			}
			finally
			{
				stream.Close();
			}
			return "@\"" + text + "\"";
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x000F668C File Offset: 0x000F488C
		protected virtual CompilerResults FromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			string[] array = new string[sources.Length];
			FileStream[] array2 = new FileStream[sources.Length];
			CompilerResults result = null;
			try
			{
				WindowsImpersonationContext impersonation = Executor.RevertImpersonation();
				try
				{
					try
					{
						bool isEnabled = FileIntegrity.IsEnabled;
						for (int i = 0; i < sources.Length; i++)
						{
							string text = options.TempFiles.AddExtension(i.ToString() + this.FileExtension);
							FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
							array2[i] = fileStream;
							using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
							{
								streamWriter.Write(sources[i]);
								streamWriter.Flush();
								if (isEnabled)
								{
									FileIntegrity.MarkAsTrusted(fileStream.SafeFileHandle);
								}
							}
							array[i] = text;
						}
						result = this.FromFileBatch(options, array);
					}
					finally
					{
						int num = 0;
						while (num < array2.Length && array2[num] != null)
						{
							array2[num].Close();
							num++;
						}
					}
				}
				finally
				{
					Executor.ReImpersonate(impersonation);
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x000F67DC File Offset: 0x000F49DC
		protected static string JoinStringArray(string[] sa, string separator)
		{
			if (sa == null || sa.Length == 0)
			{
				return string.Empty;
			}
			if (sa.Length == 1)
			{
				return "\"" + sa[0] + "\"";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < sa.Length - 1; i++)
			{
				stringBuilder.Append("\"");
				stringBuilder.Append(sa[i]);
				stringBuilder.Append("\"");
				stringBuilder.Append(separator);
			}
			stringBuilder.Append("\"");
			stringBuilder.Append(sa[sa.Length - 1]);
			stringBuilder.Append("\"");
			return stringBuilder.ToString();
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x000F687C File Offset: 0x000F4A7C
		internal static Assembly LoadImageSkipIntegrityCheck(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			MethodInfo method = typeof(Assembly).GetMethod("LoadImageSkipIntegrityCheck", BindingFlags.Static | BindingFlags.NonPublic);
			return (method != null) ? ((Assembly)method.Invoke(null, new object[]
			{
				rawAssembly,
				rawSymbolStore,
				securityEvidence
			})) : Assembly.Load(rawAssembly, rawSymbolStore, securityEvidence);
		}
	}
}
