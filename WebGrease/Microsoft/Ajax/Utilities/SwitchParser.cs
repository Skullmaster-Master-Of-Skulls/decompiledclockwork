using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000DE RID: 222
	public class SwitchParser
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00042AB8 File Offset: 0x00040CB8
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x00042AC0 File Offset: 0x00040CC0
		public CodeSettings JSSettings { get; private set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00042AC9 File Offset: 0x00040CC9
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x00042AD1 File Offset: 0x00040CD1
		public CssSettings CssSettings { get; private set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00042ADA File Offset: 0x00040CDA
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x00042AE2 File Offset: 0x00040CE2
		public bool AnalyzeMode { get; private set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00042AEB File Offset: 0x00040CEB
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x00042AF3 File Offset: 0x00040CF3
		public string ReportFormat { get; private set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00042AFC File Offset: 0x00040CFC
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x00042B04 File Offset: 0x00040D04
		public string ReportPath { get; private set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x00042B0D File Offset: 0x00040D0D
		// (set) Token: 0x06000E80 RID: 3712 RVA: 0x00042B15 File Offset: 0x00040D15
		public bool PrettyPrint { get; private set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00042B1E File Offset: 0x00040D1E
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x00042B26 File Offset: 0x00040D26
		public int WarningLevel { get; set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x00042B2F File Offset: 0x00040D2F
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x00042B37 File Offset: 0x00040D37
		public ExistingFileTreatment Clobber { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00042B40 File Offset: 0x00040D40
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x00042B48 File Offset: 0x00040D48
		public string EncodingOutputName { get; private set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00042B51 File Offset: 0x00040D51
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x00042B59 File Offset: 0x00040D59
		public string EncodingInputName { get; private set; }

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000E89 RID: 3721 RVA: 0x00042B64 File Offset: 0x00040D64
		// (remove) Token: 0x06000E8A RID: 3722 RVA: 0x00042B9C File Offset: 0x00040D9C
		public event EventHandler<InvalidSwitchEventArgs> InvalidSwitch;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000E8B RID: 3723 RVA: 0x00042BD4 File Offset: 0x00040DD4
		// (remove) Token: 0x06000E8C RID: 3724 RVA: 0x00042C0C File Offset: 0x00040E0C
		public event EventHandler<UnknownParameterEventArgs> UnknownParameter;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000E8D RID: 3725 RVA: 0x00042C44 File Offset: 0x00040E44
		// (remove) Token: 0x06000E8E RID: 3726 RVA: 0x00042C7C File Offset: 0x00040E7C
		public event EventHandler JSOnlyParameter;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000E8F RID: 3727 RVA: 0x00042CB4 File Offset: 0x00040EB4
		// (remove) Token: 0x06000E90 RID: 3728 RVA: 0x00042CEC File Offset: 0x00040EEC
		public event EventHandler CssOnlyParameter;

		// Token: 0x06000E91 RID: 3729 RVA: 0x00042D21 File Offset: 0x00040F21
		public SwitchParser()
		{
			this.JSSettings = new CodeSettings();
			this.CssSettings = new CssSettings();
			this.m_isMono = (Type.GetType("Mono.Runtime") != null);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00042D55 File Offset: 0x00040F55
		public SwitchParser(CodeSettings scriptSettings, CssSettings cssSettings)
		{
			this.JSSettings = (scriptSettings ?? new CodeSettings());
			this.CssSettings = (cssSettings ?? new CssSettings());
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00042D80 File Offset: 0x00040F80
		public SwitchParser Clone()
		{
			return new SwitchParser(this.JSSettings.Clone(), this.CssSettings.Clone())
			{
				AnalyzeMode = this.AnalyzeMode,
				EncodingInputName = this.EncodingInputName,
				EncodingOutputName = this.EncodingOutputName,
				PrettyPrint = this.PrettyPrint,
				ReportFormat = this.ReportFormat,
				ReportPath = this.ReportPath,
				WarningLevel = this.WarningLevel
			};
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00042E00 File Offset: 0x00041000
		public static string[] ToArguments(string commandLine)
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(commandLine))
			{
				int length = commandLine.Length;
				for (int i = 0; i < length; i++)
				{
					while (i < length && char.IsWhiteSpace(commandLine[i]))
					{
						i++;
					}
					StringBuilder stringBuilder = null;
					if (i < length)
					{
						char c = commandLine[i];
						bool flag = c == '"';
						if (flag)
						{
							stringBuilder = new StringBuilder();
						}
						int num = flag ? (i + 1) : i;
						while (++i < length)
						{
							char c2 = commandLine[i];
							if (flag)
							{
								if (c2 == '"')
								{
									if (i + 1 < length && commandLine[i + 1] == '"')
									{
										if (i > num)
										{
											stringBuilder.Append(commandLine.Substring(num, i - num));
										}
										stringBuilder.Append('"');
										num = ++i + 1;
									}
									else
									{
										flag = false;
										if (i > num)
										{
											stringBuilder.Append(commandLine.Substring(num, i - num));
										}
										num = i + 1;
									}
								}
							}
							else
							{
								if (char.IsWhiteSpace(c2))
								{
									break;
								}
								if (c2 == '"')
								{
									flag = true;
									if (stringBuilder == null)
									{
										stringBuilder = new StringBuilder();
									}
									stringBuilder.Append(commandLine.Substring(num, i - num));
									num = i + 1;
								}
							}
						}
						if (stringBuilder != null)
						{
							if (i > num)
							{
								stringBuilder.Append(commandLine.Substring(num, i - num));
							}
							list.Add(stringBuilder.ToString());
						}
						else
						{
							list.Add(commandLine.Substring(num, i - num));
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00042F78 File Offset: 0x00041178
		public void Parse(string commandLine)
		{
			if (!string.IsNullOrEmpty(commandLine))
			{
				this.Parse(SwitchParser.ToArguments(commandLine));
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00042F90 File Offset: 0x00041190
		public void Parse(string[] args)
		{
			char[] separator = new char[]
			{
				',',
				';'
			};
			if (args != null)
			{
				bool flag = false;
				bool flag2 = false;
				bool killSpecified = false;
				bool flag3 = false;
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i];
					if (text.Length > 1 && (text.StartsWith("-", StringComparison.Ordinal) || text.StartsWith("–", StringComparison.Ordinal) || (!this.m_isMono && text.StartsWith("/", StringComparison.Ordinal))))
					{
						string[] array = text.Substring(1).Split(new char[]
						{
							':'
						});
						string text2 = array[0].ToUpperInvariant();
						string text3 = (array.Length == 1) ? null : array[1];
						string text4 = (text3 == null) ? null : text3.ToUpperInvariant();
						string key;
						switch (key = text2)
						{
						case "ANALYZE":
						case "A":
							this.AnalyzeMode = true;
							this.ReportFormat = null;
							if (text4 != null)
							{
								string[] array2 = text4.Split(separator, StringSplitOptions.RemoveEmptyEntries);
								foreach (string text5 in array2)
								{
									if (string.CompareOrdinal(text5, "OUT") == 0)
									{
										if (i >= args.Length - 1)
										{
											this.OnInvalidSwitch(text2, text3);
										}
										else
										{
											this.ReportPath = args[++i];
										}
									}
									else
									{
										this.ReportFormat = text5;
									}
								}
							}
							if (!flag)
							{
								this.WarningLevel = int.MaxValue;
								goto IL_1CE2;
							}
							goto IL_1CE2;
						case "ASPNET":
						{
							bool flag4;
							if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.JSSettings.AllowEmbeddedAspNetBlocks = (this.CssSettings.AllowEmbeddedAspNetBlocks = flag4);
								goto IL_1CE2;
							}
							this.OnInvalidSwitch(text2, text3);
							goto IL_1CE2;
						}
						case "BRACES":
							if (text4 == "NEW")
							{
								this.JSSettings.BlocksStartOnSameLine = (this.CssSettings.BlocksStartOnSameLine = BlockStart.NewLine);
								goto IL_1CE2;
							}
							if (text4 == "SAME")
							{
								this.JSSettings.BlocksStartOnSameLine = (this.CssSettings.BlocksStartOnSameLine = BlockStart.SameLine);
								goto IL_1CE2;
							}
							if (text4 == "SOURCE")
							{
								this.JSSettings.BlocksStartOnSameLine = (this.CssSettings.BlocksStartOnSameLine = BlockStart.UseSource);
								goto IL_1CE2;
							}
							this.OnInvalidSwitch(text2, text3);
							goto IL_1CE2;
						case "CC":
						{
							bool flag4;
							if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.JSSettings.IgnoreConditionalCompilation = !flag4;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						}
						case "CLOBBER":
						{
							if (text4 == null)
							{
								this.Clobber = ExistingFileTreatment.Overwrite;
								goto IL_1CE2;
							}
							bool flag4;
							if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.Clobber = (flag4 ? ExistingFileTreatment.Overwrite : ExistingFileTreatment.Auto);
								goto IL_1CE2;
							}
							this.OnInvalidSwitch(text2, text3);
							goto IL_1CE2;
						}
						case "COLORS":
							if (text4 == "HEX")
							{
								this.CssSettings.ColorNames = CssColor.Hex;
							}
							else if (text4 == "STRICT")
							{
								this.CssSettings.ColorNames = CssColor.Strict;
							}
							else if (text4 == "MAJOR")
							{
								this.CssSettings.ColorNames = CssColor.Major;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnCssOnlyParameter();
							goto IL_1CE2;
						case "COMMENTS":
							if (text4 == "NONE")
							{
								this.CssSettings.CommentMode = CssComment.None;
								this.JSSettings.PreserveImportantComments = false;
								goto IL_1CE2;
							}
							if (text4 == "ALL")
							{
								this.CssSettings.CommentMode = CssComment.All;
								this.OnCssOnlyParameter();
								goto IL_1CE2;
							}
							if (text4 == "IMPORTANT")
							{
								this.CssSettings.CommentMode = CssComment.Important;
								this.JSSettings.PreserveImportantComments = true;
								goto IL_1CE2;
							}
							if (text4 == "HACKS")
							{
								this.CssSettings.CommentMode = CssComment.Hacks;
								this.OnCssOnlyParameter();
								goto IL_1CE2;
							}
							this.OnInvalidSwitch(text2, text3);
							goto IL_1CE2;
						case "CONST":
							if (text4 == "MOZ")
							{
								this.JSSettings.ConstStatementsMozilla = true;
							}
							else if (text4 == "ES6")
							{
								this.JSSettings.ConstStatementsMozilla = false;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "CSS":
							this.OnCssOnlyParameter();
							if (text4 != null)
							{
								string a;
								if ((a = text4) != null)
								{
									if (a == "FULL")
									{
										this.CssSettings.CssType = CssType.FullStyleSheet;
										goto IL_1CE2;
									}
									if (a == "DECLS")
									{
										this.CssSettings.CssType = CssType.DeclarationList;
										goto IL_1CE2;
									}
								}
								this.OnInvalidSwitch(text2, text3);
								goto IL_1CE2;
							}
							goto IL_1CE2;
						case "DEBUG":
						{
							this.m_noPretty = true;
							if (this.PrettyPrint)
							{
								this.OnInvalidSwitch(text2, text3);
							}
							bool flag4;
							if (text4 != null && text4.IndexOf(',') >= 0)
							{
								string[] array4 = text3.Split(separator);
								if (SwitchParser.BooleanSwitch(array4[0].ToUpperInvariant(), true, out flag4))
								{
									this.JSSettings.StripDebugStatements = !flag4;
									SwitchParser.AlignDebugDefine(this.JSSettings.StripDebugStatements, this.JSSettings.PreprocessorValues);
								}
								else
								{
									this.OnInvalidSwitch(text2, text3);
								}
								this.JSSettings.DebugLookupList = null;
								for (int k = 1; k < array4.Length; k++)
								{
									string text6 = array4[k];
									if (!text6.IsNullOrWhiteSpace() && !this.JSSettings.AddDebugLookup(text6))
									{
										this.OnInvalidSwitch(text2, text6);
									}
								}
							}
							else if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.JSSettings.StripDebugStatements = !flag4;
								SwitchParser.AlignDebugDefine(this.JSSettings.StripDebugStatements, this.JSSettings.PreprocessorValues);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						}
						case "DEFINE":
							if (string.IsNullOrEmpty(text4))
							{
								this.OnInvalidSwitch(text2, text3);
								goto IL_1CE2;
							}
							foreach (string text7 in text3.Split(separator, StringSplitOptions.RemoveEmptyEntries))
							{
								int num2 = text7.IndexOf('=');
								string text8;
								string value;
								if (num2 < 0)
								{
									text8 = text7.Trim();
									value = string.Empty;
								}
								else
								{
									text8 = text7.Substring(0, num2).Trim();
									value = text7.Substring(num2 + 1);
								}
								if (!JSScanner.IsValidIdentifier(text8))
								{
									this.OnInvalidSwitch(text2, text7);
								}
								else
								{
									this.JSSettings.PreprocessorValues[text8] = value;
									this.CssSettings.PreprocessorValues[text8] = value;
								}
								if (string.Compare(text8, "DEBUG", StringComparison.OrdinalIgnoreCase) == 0)
								{
									this.JSSettings.StripDebugStatements = false;
								}
							}
							goto IL_1CE2;
						case "ENC":
						{
							if (i >= args.Length - 1)
							{
								this.OnInvalidSwitch(text2, text3);
								goto IL_1CE2;
							}
							string text9 = args[++i];
							if (text4 == "IN")
							{
								this.EncodingInputName = text9;
								goto IL_1CE2;
							}
							if (text4 == "OUT")
							{
								this.EncodingOutputName = text9;
								goto IL_1CE2;
							}
							this.OnInvalidSwitch(text2, text3);
							goto IL_1CE2;
						}
						case "ESC":
						{
							bool flag4;
							if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.JSSettings.AlwaysEscapeNonAscii = flag4;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						}
						case "EVALS":
							if (text4 == "IGNORE")
							{
								this.JSSettings.EvalTreatment = EvalTreatment.Ignore;
							}
							else if (text4 == "IMMEDIATE")
							{
								this.JSSettings.EvalTreatment = EvalTreatment.MakeImmediateSafe;
							}
							else if (text4 == "SAFEALL")
							{
								this.JSSettings.EvalTreatment = EvalTreatment.MakeAllSafe;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "EXPR":
							if (text4 == "MINIFY")
							{
								this.CssSettings.MinifyExpressions = true;
							}
							else if (text4 == "RAW")
							{
								this.CssSettings.MinifyExpressions = false;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnCssOnlyParameter();
							goto IL_1CE2;
						case "FNAMES":
							if (text4 == "LOCK")
							{
								this.JSSettings.RemoveFunctionExpressionNames = false;
								this.JSSettings.PreserveFunctionNames = true;
							}
							else if (text4 == "KEEP")
							{
								this.JSSettings.RemoveFunctionExpressionNames = false;
								this.JSSettings.PreserveFunctionNames = false;
							}
							else if (text4 == "ONLYREF")
							{
								this.JSSettings.RemoveFunctionExpressionNames = true;
								this.JSSettings.PreserveFunctionNames = false;
								this.m_noPretty = true;
								if (this.PrettyPrint)
								{
									this.OnInvalidSwitch(text2, text3);
								}
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "GLOBAL":
						case "G":
							if (string.IsNullOrEmpty(text4))
							{
								this.OnInvalidSwitch(text2, text3);
							}
							else
							{
								foreach (string text10 in text3.Split(separator, StringSplitOptions.RemoveEmptyEntries))
								{
									if (!this.JSSettings.AddKnownGlobal(text10))
									{
										this.OnInvalidSwitch(text2, text10);
									}
								}
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "IGNORE":
							if (string.IsNullOrEmpty(text4))
							{
								this.OnInvalidSwitch(text2, text3);
								goto IL_1CE2;
							}
							foreach (string text11 in text3.Split(separator, StringSplitOptions.RemoveEmptyEntries))
							{
								if (string.Compare(text11, "ALL", StringComparison.OrdinalIgnoreCase) == 0)
								{
									this.JSSettings.IgnoreAllErrors = (this.CssSettings.IgnoreAllErrors = true);
								}
								else
								{
									this.JSSettings.IgnoreErrorCollection.Add(text11);
									this.CssSettings.IgnoreErrorCollection.Add(text11);
								}
							}
							goto IL_1CE2;
						case "INLINE":
							if (string.IsNullOrEmpty(text3))
							{
								this.JSSettings.InlineSafeStrings = true;
							}
							else
							{
								foreach (string text12 in text4.Split(separator, StringSplitOptions.RemoveEmptyEntries))
								{
									bool flag4;
									if (string.CompareOrdinal(text12, "FORCE") == 0)
									{
										this.JSSettings.ErrorIfNotInlineSafe = true;
										this.JSSettings.InlineSafeStrings = true;
									}
									else if (string.CompareOrdinal(text12, "NOFORCE") == 0)
									{
										this.JSSettings.ErrorIfNotInlineSafe = false;
									}
									else if (SwitchParser.BooleanSwitch(text12, true, out flag4))
									{
										this.JSSettings.InlineSafeStrings = flag4;
									}
									else
									{
										this.OnInvalidSwitch(text2, text3);
									}
								}
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "JS":
							if (text3 == null)
							{
								this.JSSettings.SourceMode = JavaScriptSourceMode.Program;
								this.JSSettings.Format = JavaScriptFormat.Normal;
							}
							else
							{
								string[] array7 = text4.Split(new char[]
								{
									',',
									';'
								});
								string[] array3 = array7;
								int j = 0;
								while (j < array3.Length)
								{
									string text13 = array3[j];
									if ((key = text13) == null)
									{
										goto IL_FD7;
									}
									if (<PrivateImplementationDetails>{86487675-C393-48D4-AFEC-7657DB09B21F}.$$method0x6000d1e-2 == null)
									{
										<PrivateImplementationDetails>{86487675-C393-48D4-AFEC-7657DB09B21F}.$$method0x6000d1e-2 = new Dictionary<string, int>(11)
										{
											{
												"JSON",
												0
											},
											{
												"PROG",
												1
											},
											{
												"PROGRAM",
												2
											},
											{
												"MOD",
												3
											},
											{
												"MODULE",
												4
											},
											{
												"EXPR",
												5
											},
											{
												"EXPRESSION",
												6
											},
											{
												"EVT",
												7
											},
											{
												"EVENT",
												8
											},
											{
												"ES5",
												9
											},
											{
												"ES6",
												10
											}
										};
									}
									int num;
									if (!<PrivateImplementationDetails>{86487675-C393-48D4-AFEC-7657DB09B21F}.$$method0x6000d1e-2.TryGetValue(key, out num))
									{
										goto IL_FD7;
									}
									switch (num)
									{
									case 0:
										if (array7.Length > 1)
										{
											this.OnInvalidSwitch(text2, text3);
										}
										this.JSSettings.MinifyCode = false;
										this.JSSettings.SourceMode = JavaScriptSourceMode.Expression;
										this.JSSettings.Format = JavaScriptFormat.JSON;
										break;
									case 1:
									case 2:
										this.JSSettings.SourceMode = JavaScriptSourceMode.Program;
										break;
									case 3:
									case 4:
										this.JSSettings.SourceMode = JavaScriptSourceMode.Module;
										break;
									case 5:
									case 6:
										this.JSSettings.SourceMode = JavaScriptSourceMode.Expression;
										break;
									case 7:
									case 8:
										this.JSSettings.SourceMode = JavaScriptSourceMode.EventHandler;
										break;
									case 9:
										this.JSSettings.ScriptVersion = ScriptVersion.EcmaScript5;
										break;
									case 10:
										this.JSSettings.ScriptVersion = ScriptVersion.EcmaScript6;
										break;
									default:
										goto IL_FD7;
									}
									IL_FE1:
									j++;
									continue;
									IL_FD7:
									this.OnInvalidSwitch(text2, text3);
									goto IL_FE1;
								}
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "KILL":
							killSpecified = true;
							if (text4 == null)
							{
								this.OnInvalidSwitch(text2, text3);
								goto IL_1CE2;
							}
							if (text4.StartsWith("0X", StringComparison.OrdinalIgnoreCase))
							{
								long num3;
								if (!text4.Substring(2).TryParseLongInvariant(NumberStyles.AllowHexSpecifier, out num3))
								{
									this.OnInvalidSwitch(text2, text3);
									goto IL_1CE2;
								}
								this.JSSettings.KillSwitch = (this.CssSettings.KillSwitch = num3);
								if ((num3 & 1L) != 0L)
								{
									this.CssSettings.CommentMode = CssComment.None;
									goto IL_1CE2;
								}
								goto IL_1CE2;
							}
							else
							{
								long num3;
								if (!text4.TryParseLongInvariant(NumberStyles.AllowLeadingSign, out num3))
								{
									this.OnInvalidSwitch(text2, text3);
									goto IL_1CE2;
								}
								this.JSSettings.KillSwitch = (this.CssSettings.KillSwitch = num3);
								if ((num3 & 1L) != 0L)
								{
									this.CssSettings.CommentMode = CssComment.None;
									goto IL_1CE2;
								}
								goto IL_1CE2;
							}
							break;
						case "LINE":
						case "LINES":
						{
							if (string.IsNullOrEmpty(text4))
							{
								CommonSettings jssettings = this.JSSettings;
								int num = this.CssSettings.LineBreakThreshold = 2147482647;
								jssettings.LineBreakThreshold = num;
								this.JSSettings.OutputMode = (this.CssSettings.OutputMode = OutputMode.SingleLine);
								CommonSettings jssettings2 = this.JSSettings;
								num = (this.CssSettings.IndentSize = 4);
								jssettings2.IndentSize = num;
								goto IL_1CE2;
							}
							string[] array8 = text4.Split(separator, StringSplitOptions.RemoveEmptyEntries);
							int num4 = 1;
							if (array8.Length > 3)
							{
								this.OnInvalidSwitch(text2, text3);
								goto IL_1CE2;
							}
							if (!string.IsNullOrEmpty(array8[0]))
							{
								int lineBreakThreshold;
								if (array8[0].TryParseIntInvariant(NumberStyles.None, out lineBreakThreshold))
								{
									CommonSettings jssettings3 = this.JSSettings;
									int num = this.CssSettings.LineBreakThreshold = lineBreakThreshold;
									jssettings3.LineBreakThreshold = num;
								}
								else if (array8[0][0] == 'S')
								{
									this.JSSettings.OutputMode = (this.CssSettings.OutputMode = OutputMode.SingleLine);
									num4 = 0;
								}
								else if (array8[0][0] == 'M')
								{
									this.JSSettings.OutputMode = (this.CssSettings.OutputMode = OutputMode.MultipleLines);
									num4 = 0;
								}
								else
								{
									this.OnInvalidSwitch(text2, array8[0]);
								}
							}
							else
							{
								CommonSettings jssettings4 = this.JSSettings;
								int num = this.CssSettings.LineBreakThreshold = 2147482647;
								jssettings4.LineBreakThreshold = num;
							}
							if (array8.Length <= num4)
							{
								goto IL_1CE2;
							}
							if (num4 > 0)
							{
								if (string.IsNullOrEmpty(array8[num4]) || array8[num4][0] == 'S')
								{
									this.JSSettings.OutputMode = (this.CssSettings.OutputMode = OutputMode.SingleLine);
								}
								else if (array8[num4][0] == 'M')
								{
									this.JSSettings.OutputMode = (this.CssSettings.OutputMode = OutputMode.MultipleLines);
								}
								else
								{
									this.OnInvalidSwitch(text2, array8[num4]);
								}
							}
							num4++;
							if (array8.Length <= num4)
							{
								goto IL_1CE2;
							}
							if (string.IsNullOrEmpty(array8[num4]))
							{
								CommonSettings jssettings5 = this.JSSettings;
								int num = this.CssSettings.IndentSize = 4;
								jssettings5.IndentSize = num;
								goto IL_1CE2;
							}
							int indentSize;
							if (array8[num4].TryParseIntInvariant(NumberStyles.None, out indentSize))
							{
								CommonSettings jssettings6 = this.JSSettings;
								int num = this.CssSettings.IndentSize = indentSize;
								jssettings6.IndentSize = num;
								goto IL_1CE2;
							}
							this.OnInvalidSwitch(text2, array8[num4]);
							goto IL_1CE2;
						}
						case "LITERALS":
							if (!(text4 == "KEEP") && !(text4 == "COMBINE"))
							{
								if (text4 == "EVAL")
								{
									this.JSSettings.EvalLiteralExpressions = true;
									this.m_noPretty = true;
									if (this.PrettyPrint)
									{
										this.OnInvalidSwitch(text2, text3);
									}
								}
								else if (text4 == "NOEVAL")
								{
									this.JSSettings.EvalLiteralExpressions = false;
								}
								else
								{
									this.OnInvalidSwitch(text2, text3);
								}
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "MAC":
						{
							bool flag4;
							if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.JSSettings.MacSafariQuirks = flag4;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						}
						case "MINIFY":
						{
							flag3 = true;
							bool flag4;
							if (flag2 && this.JSSettings.LocalRenaming != LocalRenaming.KeepAll)
							{
								this.OnInvalidSwitch(text2, text3);
							}
							else if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.JSSettings.MinifyCode = flag4;
								if (flag4)
								{
									this.m_noPretty = true;
									if (this.PrettyPrint)
									{
										this.OnInvalidSwitch(text2, text3);
									}
								}
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						}
						case "NEW":
							if (text4 == "KEEP")
							{
								this.JSSettings.CollapseToLiteral = false;
							}
							else if (text4 == "COLLAPSE")
							{
								this.JSSettings.CollapseToLiteral = true;
								this.m_noPretty = true;
								if (this.PrettyPrint)
								{
									this.OnInvalidSwitch(text2, text3);
								}
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "NFE":
							if (text4 == "KEEPALL")
							{
								this.JSSettings.RemoveFunctionExpressionNames = false;
							}
							else if (text4 == "ONLYREF")
							{
								this.JSSettings.RemoveFunctionExpressionNames = true;
								this.m_noPretty = true;
								if (this.PrettyPrint)
								{
									this.OnInvalidSwitch(text2, text3);
								}
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "NOCLOBBER":
						{
							if (text4 == null)
							{
								this.Clobber = ExistingFileTreatment.Preserve;
								goto IL_1CE2;
							}
							bool flag4;
							if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.Clobber = (flag4 ? ExistingFileTreatment.Preserve : ExistingFileTreatment.Auto);
								goto IL_1CE2;
							}
							this.OnInvalidSwitch(text2, text3);
							goto IL_1CE2;
						}
						case "NORENAME":
							if (string.IsNullOrEmpty(text4))
							{
								this.OnInvalidSwitch(text2, text3);
							}
							else
							{
								foreach (string text14 in text3.Split(separator, StringSplitOptions.RemoveEmptyEntries))
								{
									if (!this.JSSettings.AddNoAutoRename(text14))
									{
										this.OnInvalidSwitch(text2, text14);
									}
								}
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "OBJ":
							this.m_noPretty = true;
							if (this.PrettyPrint)
							{
								this.OnInvalidSwitch(text2, text3);
							}
							if (text4 == "MIN")
							{
								this.JSSettings.QuoteObjectLiteralProperties = false;
							}
							else if (text4 == "QUOTE")
							{
								this.JSSettings.QuoteObjectLiteralProperties = true;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "PPONLY":
						{
							this.m_noPretty = true;
							if (this.PrettyPrint)
							{
								this.OnInvalidSwitch(text2, text3);
							}
							bool flag4;
							if (text3 == null)
							{
								this.JSSettings.PreprocessOnly = true;
							}
							else if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.JSSettings.PreprocessOnly = flag4;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						}
						case "PRETTY":
						case "P":
						{
							this.PrettyPrint = true;
							if (this.m_noPretty)
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.JSSettings.MinifyCode = false;
							this.JSSettings.OutputMode = (this.CssSettings.OutputMode = OutputMode.MultipleLines);
							this.CssSettings.KillSwitch = -2L;
							if (text4 == null)
							{
								goto IL_1CE2;
							}
							int indentSize2;
							if (text3.TryParseIntInvariant(NumberStyles.None, out indentSize2))
							{
								CommonSettings jssettings7 = this.JSSettings;
								int num = this.CssSettings.IndentSize = indentSize2;
								jssettings7.IndentSize = num;
								goto IL_1CE2;
							}
							this.OnInvalidSwitch(text2, text3);
							goto IL_1CE2;
						}
						case "RENAME":
							if (text4 == null)
							{
								i = this.OnUnknownParameter(args, i, text2, text3);
							}
							else if (text4.IndexOf('=') > 0)
							{
								this.m_noPretty = true;
								if (this.PrettyPrint)
								{
									this.OnInvalidSwitch(text2, text3);
								}
								string[] array9 = text3.Split(separator, StringSplitOptions.RemoveEmptyEntries);
								foreach (string text15 in array9)
								{
									string[] array10 = text15.Split(new char[]
									{
										'='
									});
									if (array10.Length == 2)
									{
										string text16 = array10[0];
										string text17 = array10[1];
										bool flag5 = JSScanner.IsValidIdentifier(text16);
										bool flag6 = JSScanner.IsValidIdentifier(text17);
										if (flag5 && flag6)
										{
											string newName = this.JSSettings.GetNewName(text16);
											if (newName == null)
											{
												this.JSSettings.AddRenamePair(text16, text17);
											}
											else if (string.CompareOrdinal(text17, newName) != 0)
											{
												this.OnInvalidSwitch(text2, text16);
											}
										}
										else
										{
											if (flag5)
											{
												this.OnInvalidSwitch(text2, text17);
											}
											if (flag6)
											{
												this.OnInvalidSwitch(text2, text16);
											}
										}
									}
									else
									{
										this.OnInvalidSwitch(text2, text3);
									}
								}
							}
							else if (text4 == "ALL")
							{
								this.JSSettings.LocalRenaming = LocalRenaming.CrunchAll;
								flag2 = true;
								this.m_noPretty = true;
								if (this.PrettyPrint)
								{
									this.OnInvalidSwitch(text2, text3);
								}
							}
							else if (text4 == "LOCALIZATION")
							{
								this.JSSettings.LocalRenaming = LocalRenaming.KeepLocalizationVars;
								flag2 = true;
								this.m_noPretty = true;
								if (this.PrettyPrint)
								{
									this.OnInvalidSwitch(text2, text3);
								}
							}
							else if (text4 == "NONE")
							{
								this.JSSettings.LocalRenaming = LocalRenaming.KeepAll;
								flag2 = true;
							}
							else if (text4 == "NOPROPS")
							{
								this.JSSettings.ManualRenamesProperties = false;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							if (this.JSSettings.LocalRenaming != LocalRenaming.KeepAll)
							{
								this.ResetRenamingKill(killSpecified);
								if (flag3 && !this.JSSettings.MinifyCode)
								{
									this.OnInvalidSwitch(text2, text3);
								}
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "REORDER":
						{
							bool flag4;
							if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.JSSettings.ReorderScopeDeclarations = flag4;
								if (flag4)
								{
									this.m_noPretty = true;
									if (this.PrettyPrint)
									{
										this.OnInvalidSwitch(text2, text3);
									}
								}
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						}
						case "STRICT":
						{
							bool flag4;
							if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.JSSettings.StrictMode = flag4;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						}
						case "TERM":
						{
							bool flag4;
							if (SwitchParser.BooleanSwitch(text4, true, out flag4))
							{
								this.JSSettings.TermSemicolons = (this.CssSettings.TermSemicolons = flag4);
								goto IL_1CE2;
							}
							this.OnInvalidSwitch(text2, text3);
							goto IL_1CE2;
						}
						case "UNUSED":
							if (text4 == "KEEP")
							{
								this.JSSettings.RemoveUnneededCode = false;
							}
							else if (text4 == "REMOVE")
							{
								this.JSSettings.RemoveUnneededCode = true;
								this.m_noPretty = true;
								if (this.PrettyPrint)
								{
									this.OnInvalidSwitch(text2, text3);
								}
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "VAR":
							this.m_noPretty = true;
							if (this.PrettyPrint || string.IsNullOrEmpty(text4))
							{
								this.OnInvalidSwitch(text2, text3);
							}
							else
							{
								string text18 = text3;
								string text19 = null;
								int num5 = text3.IndexOf(',');
								if (num5 == 0)
								{
									text18 = null;
									text19 = text3.Substring(num5 + 1);
								}
								else if (num5 > 0)
								{
									text18 = text3.Substring(0, num5);
									text19 = text3.Substring(num5 + 1);
								}
								if (!string.IsNullOrEmpty(text18))
								{
									CrunchEnumerator.FirstLetters = text18;
								}
								if (!string.IsNullOrEmpty(text19))
								{
									CrunchEnumerator.PartLetters = text19;
								}
								else if (!string.IsNullOrEmpty(text18))
								{
									CrunchEnumerator.PartLetters = text18;
								}
							}
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "WARN":
						case "W":
						{
							int warningLevel;
							if (string.IsNullOrEmpty(text4))
							{
								this.WarningLevel = int.MaxValue;
							}
							else if (text3.TryParseIntInvariant(NumberStyles.None, out warningLevel))
							{
								this.WarningLevel = warningLevel;
							}
							else
							{
								this.OnInvalidSwitch(text2, text3);
							}
							flag = true;
							goto IL_1CE2;
						}
						case "D":
							this.m_noPretty = true;
							if (this.PrettyPrint)
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.JSSettings.StripDebugStatements = true;
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "E":
						case "EO":
							if (array.Length < 2)
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.EncodingOutputName = text3;
							goto IL_1CE2;
						case "EI":
							if (array.Length < 2)
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.EncodingInputName = text3;
							goto IL_1CE2;
						case "H":
						case "HC":
							this.m_noPretty = true;
							if (this.PrettyPrint)
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.JSSettings.LocalRenaming = LocalRenaming.CrunchAll;
							this.JSSettings.RemoveUnneededCode = true;
							this.OnJSOnlyParameter();
							flag2 = true;
							this.ResetRenamingKill(killSpecified);
							if (flag3 && !this.JSSettings.MinifyCode)
							{
								this.OnInvalidSwitch(text2, text3);
								goto IL_1CE2;
							}
							goto IL_1CE2;
						case "HL":
						case "HLC":
						case "HCL":
							this.m_noPretty = true;
							if (this.PrettyPrint)
							{
								this.OnInvalidSwitch(text2, text3);
							}
							this.JSSettings.LocalRenaming = LocalRenaming.KeepLocalizationVars;
							this.JSSettings.RemoveUnneededCode = true;
							this.OnJSOnlyParameter();
							flag2 = true;
							this.ResetRenamingKill(killSpecified);
							if (flag3 && !this.JSSettings.MinifyCode)
							{
								this.OnInvalidSwitch(text2, text3);
								goto IL_1CE2;
							}
							goto IL_1CE2;
						case "J":
							this.JSSettings.EvalTreatment = EvalTreatment.Ignore;
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "K":
							this.JSSettings.InlineSafeStrings = true;
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "L":
							this.JSSettings.CollapseToLiteral = false;
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "M":
							this.JSSettings.MacSafariQuirks = true;
							this.OnJSOnlyParameter();
							goto IL_1CE2;
						case "Z":
							this.JSSettings.TermSemicolons = (this.CssSettings.TermSemicolons = true);
							goto IL_1CE2;
						}
						i = this.OnUnknownParameter(args, i, text2, text3);
					}
					else
					{
						i = this.OnUnknownParameter(args, i, null, null);
					}
					IL_1CE2:;
				}
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00044C90 File Offset: 0x00042E90
		protected virtual int OnUnknownParameter(IList<string> arguments, int index, string switchPart, string parameterPart)
		{
			if (this.UnknownParameter != null)
			{
				UnknownParameterEventArgs unknownParameterEventArgs = new UnknownParameterEventArgs(arguments)
				{
					Index = index,
					SwitchPart = switchPart,
					ParameterPart = parameterPart
				};
				this.UnknownParameter(this, unknownParameterEventArgs);
				if (unknownParameterEventArgs.Index > index)
				{
					index = unknownParameterEventArgs.Index;
				}
			}
			return index;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00044CE4 File Offset: 0x00042EE4
		protected virtual void OnInvalidSwitch(string switchPart, string parameterPart)
		{
			if (this.InvalidSwitch != null)
			{
				this.InvalidSwitch(this, new InvalidSwitchEventArgs
				{
					SwitchPart = switchPart,
					ParameterPart = parameterPart
				});
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00044D1A File Offset: 0x00042F1A
		protected virtual void OnJSOnlyParameter()
		{
			if (this.JSOnlyParameter != null)
			{
				this.JSOnlyParameter(this, new EventArgs());
			}
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00044D35 File Offset: 0x00042F35
		protected virtual void OnCssOnlyParameter()
		{
			if (this.CssOnlyParameter != null)
			{
				this.CssOnlyParameter(this, new EventArgs());
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00044D50 File Offset: 0x00042F50
		private static void AlignDebugDefine(bool stripDebugStatements, IDictionary<string, string> defines)
		{
			if (stripDebugStatements)
			{
				if (defines.ContainsKey("DEBUG"))
				{
					defines.Remove("DEBUG");
					return;
				}
			}
			else if (!defines.ContainsKey("DEBUG"))
			{
				defines.Add("debug", string.Empty);
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00044D8C File Offset: 0x00042F8C
		public static bool BooleanSwitch(string booleanText, bool defaultValue, out bool booleanValue)
		{
			bool result = true;
			switch (booleanText)
			{
			case "Y":
			case "YES":
			case "T":
			case "TRUE":
			case "ON":
			case "1":
				booleanValue = true;
				return result;
			case "N":
			case "NO":
			case "NONE":
			case "F":
			case "FALSE":
			case "OFF":
			case "0":
				booleanValue = false;
				return result;
			case "":
				goto IL_12C;
			case null:
				break;
			default:
				booleanValue = defaultValue;
				return false;
				break;
			}
			IL_12C:
			booleanValue = defaultValue;
			return result;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00044ED0 File Offset: 0x000430D0
		private void ResetRenamingKill(bool killSpecified)
		{
			if (!killSpecified && this.JSSettings.KillSwitch != 0L)
			{
				this.JSSettings.KillSwitch &= -16777217L;
			}
		}

		// Token: 0x0400059C RID: 1436
		private bool m_isMono;

		// Token: 0x0400059D RID: 1437
		private bool m_noPretty;
	}
}
