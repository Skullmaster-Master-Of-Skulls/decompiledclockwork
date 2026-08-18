using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200009A RID: 154
	[Serializable]
	public sealed class HttpCompileException : HttpException
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x00016C58 File Offset: 0x00014E58
		public HttpCompileException()
		{
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00016C60 File Offset: 0x00014E60
		public HttpCompileException(string message) : base(message)
		{
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00016CA3 File Offset: 0x00014EA3
		public HttpCompileException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00016CAD File Offset: 0x00014EAD
		public HttpCompileException(CompilerResults results, string sourceCode)
		{
			this._results = results;
			this._sourceCode = sourceCode;
			base.SetFormatter(new DynamicCompileErrorFormatter(this));
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00016CCF File Offset: 0x00014ECF
		private HttpCompileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._results = (CompilerResults)info.GetValue("_results", typeof(CompilerResults));
			this._sourceCode = info.GetString("_sourceCode");
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00016D0A File Offset: 0x00014F0A
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x00016D12 File Offset: 0x00014F12
		internal bool DontCache
		{
			get
			{
				return this._dontCache;
			}
			set
			{
				this._dontCache = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00016D1B File Offset: 0x00014F1B
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x00016D23 File Offset: 0x00014F23
		internal ICollection VirtualPathDependencies
		{
			get
			{
				return this._virtualPathDependencies;
			}
			set
			{
				this._virtualPathDependencies = value;
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00016D2C File Offset: 0x00014F2C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_results", this._results);
			info.AddValue("_sourceCode", this._sourceCode);
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00016D58 File Offset: 0x00014F58
		public override string Message
		{
			get
			{
				CompilerError firstCompileError = this.FirstCompileError;
				if (firstCompileError == null)
				{
					return base.Message;
				}
				return string.Format(CultureInfo.CurrentCulture, "{0}({1}): error {2}: {3}", new object[]
				{
					firstCompileError.FileName,
					firstCompileError.Line,
					firstCompileError.ErrorNumber,
					firstCompileError.ErrorText
				});
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00016DB6 File Offset: 0x00014FB6
		public CompilerResults Results
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
			get
			{
				return this._results;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00016DB6 File Offset: 0x00014FB6
		internal CompilerResults ResultsWithoutDemand
		{
			get
			{
				return this._results;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00016DBE File Offset: 0x00014FBE
		public string SourceCode
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
			get
			{
				return this._sourceCode;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00016DBE File Offset: 0x00014FBE
		internal string SourceCodeWithoutDemand
		{
			get
			{
				return this._sourceCode;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00016DC8 File Offset: 0x00014FC8
		internal CompilerError FirstCompileError
		{
			get
			{
				if (this._results == null || !this._results.Errors.HasErrors)
				{
					return null;
				}
				CompilerError compilerError = null;
				foreach (object obj in this._results.Errors)
				{
					CompilerError compilerError2 = (CompilerError)obj;
					if (!compilerError2.IsWarning)
					{
						if (HttpRuntime.CodegenDirInternal != null && compilerError2.FileName != null && !StringUtil.StringStartsWith(compilerError2.FileName, HttpRuntime.CodegenDirInternal))
						{
							compilerError = compilerError2;
							break;
						}
						if (compilerError == null)
						{
							compilerError = compilerError2;
						}
					}
				}
				return compilerError;
			}
		}

		// Token: 0x040003A7 RID: 935
		private CompilerResults _results;

		// Token: 0x040003A8 RID: 936
		private string _sourceCode;

		// Token: 0x040003A9 RID: 937
		private bool _dontCache;

		// Token: 0x040003AA RID: 938
		private ICollection _virtualPathDependencies;

		// Token: 0x040003AB RID: 939
		private const string compileErrorFormat = "{0}({1}): error {2}: {3}";
	}
}
