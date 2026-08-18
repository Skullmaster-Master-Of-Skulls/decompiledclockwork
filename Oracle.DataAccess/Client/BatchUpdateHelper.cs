using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000116 RID: 278
	internal sealed class BatchUpdateHelper
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x0006F564 File Offset: 0x0006E564
		static BatchUpdateHelper()
		{
			string pattern = "[\\s]+|(?<string>'([^']|'')*')|(?<comment>(/\\*([^\\*]|\\*[^/])*\\*/)|(--.*))|(?<bindparammarker>:[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_#$]+)|(?<query>select)|(?<identifier>([\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_#$]+)|(\"([^\"]|\"\")*\"))|(?<other>.)";
			BatchUpdateHelper.m_parserRegEx = new Regex(pattern, RegexOptions.ExplicitCapture);
			BatchUpdateHelper.m_bindParameterMarkerGroup = BatchUpdateHelper.m_parserRegEx.GroupNumberFromName("bindparammarker");
			BatchUpdateHelper.m_strDeclareBlock = "DECLARE\n";
			if (OraTrace.m_RevertBUErrHandling == 1)
			{
				BatchUpdateHelper.m_strDeclareBlock += "cce EXCEPTION;\n";
				BatchUpdateHelper.m_strDeclareBlock += "PRAGMA EXCEPTION_INIT(cce, -08179);\n";
			}
			BatchUpdateHelper.m_strDeclareBlock += "TYPE tec IS TABLE OF NUMBER INDEX BY BINARY_INTEGER;\n";
			BatchUpdateHelper.m_strDeclareBlock += "TYPE trmd IS TABLE OF NUMBER INDEX BY BINARY_INTEGER;\n";
			BatchUpdateHelper.m_strDeclareBlock += "TYPE tem IS TABLE OF VARCHAR2(256) INDEX BY BINARY_INTEGER;\n";
			BatchUpdateHelper.m_strDeclareBlock += "rct NUMBER:=0;\n";
			BatchUpdateHelper.m_strDeclareBlock += "rmd NUMBER:=0;\n";
			BatchUpdateHelper.m_strDeclareBlock += "aecd tec;\n";
			BatchUpdateHelper.m_strDeclareBlock += "armd trmd;\n";
			BatchUpdateHelper.m_strDeclareBlock += "aem  tem;\n";
			if (OraTrace.m_RevertBUErrHandling == 1)
			{
				BatchUpdateHelper.m_strRowCountBlock = "IF (SQL%ROWCOUNT = 0) THEN\n";
				BatchUpdateHelper.m_strRowCountBlock += "RAISE cce;\n";
				BatchUpdateHelper.m_strRowCountBlock += "ELSE\n";
				BatchUpdateHelper.m_strRowCountBlock += "armd(rct):=SQL%ROWCOUNT;\n";
				BatchUpdateHelper.m_strRowCountBlock += "rmd:=rmd+SQL%ROWCOUNT;\n";
				BatchUpdateHelper.m_strRowCountBlock += "END IF;\n";
			}
			else
			{
				BatchUpdateHelper.m_strRowCountBlock = "armd(rct):=SQL%ROWCOUNT;\n";
				BatchUpdateHelper.m_strRowCountBlock += "rmd:=rmd+SQL%ROWCOUNT;\n";
				BatchUpdateHelper.m_strRowCountBlock += "aecd(rct):=0;\n";
			}
			BatchUpdateHelper.m_strExceptionBlock = "EXCEPTION\n";
			BatchUpdateHelper.m_strExceptionBlock += "WHEN OTHERS THEN\n";
			BatchUpdateHelper.m_strExceptionBlock += "armd(rct):=0;\n";
			BatchUpdateHelper.m_strExceptionBlock += "aecd(rct):=SQLCODE;\n";
			BatchUpdateHelper.m_strExceptionBlock += "aem(rct):=SQLERRM;\n";
			BatchUpdateHelper.m_strExceptionBlock += "end;\n";
			BatchUpdateHelper.m_outParamAssignmentBlock = ":rmd:=rmd;\n";
			BatchUpdateHelper.m_outParamAssignmentBlock += ":aecd:=aecd;\n";
			BatchUpdateHelper.m_outParamAssignmentBlock += ":aem:=aem;\n";
			BatchUpdateHelper.m_outParamAssignmentBlock += ":armd:=armd;\n";
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0006F804 File Offset: 0x0006E804
		internal BatchUpdateHelper()
		{
			this.m_batchCommand = new OracleCommand();
			this.m_paramArrayArray = new ArrayList();
			this.m_batchCmdTextBuilder = new StringBuilder();
			this.m_tempStringBuilder = new StringBuilder();
			this.m_usedParameterNames = new Hashtable();
			this.m_batchCmdTextBuilder.Append(BatchUpdateHelper.m_plsqlBlockPrefix);
			this.m_batchSizeCounter = 0;
			this.m_lastNumUsed = 0;
			this.m_prmRowsModified = new OracleParameter();
			this.m_prmRowsModified.ParameterName = "rmd";
			this.m_prmRowsModified.DbType = DbType.Int32;
			this.m_prmRowsModified.Direction = ParameterDirection.Output;
			this.m_prmErrCodesArray = new OracleParameter();
			this.m_prmErrCodesArray.ParameterName = "aecd";
			this.m_prmErrCodesArray.DbType = DbType.Int32;
			this.m_prmErrCodesArray.Direction = ParameterDirection.Output;
			this.m_prmErrCodesArray.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			this.m_prmErrMsgArray = new OracleParameter();
			this.m_prmErrMsgArray.ParameterName = "aem";
			this.m_prmErrMsgArray.DbType = DbType.String;
			this.m_prmErrMsgArray.Direction = ParameterDirection.Output;
			this.m_prmErrMsgArray.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			this.m_prmRowsMdArray = new OracleParameter();
			this.m_prmRowsMdArray.ParameterName = "armd";
			this.m_prmRowsMdArray.DbType = DbType.Int32;
			this.m_prmRowsMdArray.Direction = ParameterDirection.Output;
			this.m_prmRowsMdArray.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0006F961 File Offset: 0x0006E961
		internal OracleCommand BatchUpdateCommand
		{
			get
			{
				return this.m_batchCommand;
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0006F96C File Offset: 0x0006E96C
		internal void InitializeBUC()
		{
			this.m_batchCommand.CommandText = null;
			OracleParameterCollection parameters = this.m_batchCommand.Parameters;
			long num = (long)parameters.Count;
			int num2 = 0;
			while ((long)num2 < num - 4L)
			{
				parameters[num2].Dispose();
				num2++;
			}
			parameters.Clear();
			this.m_paramArrayArray.Clear();
			this.m_usedParameterNames.Clear();
			this.m_batchSizeCounter = 0;
			this.m_batchCmdTextBuilder.Remove(0, this.m_batchCmdTextBuilder.Length);
			this.m_batchCmdTextBuilder.Append(BatchUpdateHelper.m_strDeclareBlock);
			this.m_batchCmdTextBuilder.Append(BatchUpdateHelper.m_plsqlBlockPrefix);
			this.m_lastNumUsed = 0;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0006FA1C File Offset: 0x0006EA1C
		internal void FinalizeBUC()
		{
			this.m_batchCmdTextBuilder.Append(BatchUpdateHelper.m_outParamAssignmentBlock);
			this.m_batchCmdTextBuilder.Append(BatchUpdateHelper.m_plsqlBlockSuffix);
			this.m_batchCommand.Parameters.Add(this.m_prmRowsModified);
			this.m_prmErrCodesArray.Size = this.m_batchSizeCounter;
			this.m_batchCommand.Parameters.Add(this.m_prmErrCodesArray);
			this.m_prmErrMsgArray.Size = this.m_batchSizeCounter;
			int[] array = new int[this.m_batchSizeCounter];
			for (int i = 0; i < this.m_batchSizeCounter; i++)
			{
				array[i] = 256;
			}
			this.m_prmErrMsgArray.ArrayBindSize = array;
			this.m_batchCommand.Parameters.Add(this.m_prmErrMsgArray);
			this.m_prmRowsMdArray.Size = this.m_batchSizeCounter;
			this.m_batchCommand.Parameters.Add(this.m_prmRowsMdArray);
			this.m_batchCommand.CommandText = this.m_batchCmdTextBuilder.ToString();
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0006FB24 File Offset: 0x0006EB24
		internal int AddCommand(OracleCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException();
			}
			this.m_batchSizeCounter++;
			OracleParameterCollection parameters = command.Parameters;
			OracleParameter[] array = this.CloneParameters(parameters);
			this.m_paramArrayArray.Add(array);
			this.m_batchCmdTextBuilder.Append(BatchUpdateHelper.m_plsqlBlockPrefix);
			this.m_batchCmdTextBuilder.Append("rct:=rct+1;\n");
			if (CommandType.StoredProcedure == command.CommandType)
			{
				this.ParseStoredProcedure(command, array);
			}
			else
			{
				this.ParseCommandText(command, array);
			}
			this.m_batchCmdTextBuilder.Append(BatchUpdateHelper.m_strRowCountBlock);
			this.m_batchCmdTextBuilder.Append(BatchUpdateHelper.m_strExceptionBlock);
			this.m_batchCommand.Parameters.AddRange(array);
			return this.m_paramArrayArray.Count - 1;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0006FBE4 File Offset: 0x0006EBE4
		internal OracleParameter GetBatchedParameter(int cmdIdentifier, int paramIndex)
		{
			if (this.m_paramArrayArray.Count >= cmdIdentifier)
			{
				OracleParameter[] array = (OracleParameter[])this.m_paramArrayArray[cmdIdentifier];
				if (array.Length >= paramIndex)
				{
					return array[paramIndex];
				}
			}
			return null;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0006FC1C File Offset: 0x0006EC1C
		private OracleParameter[] CloneParameters(OracleParameterCollection parameters)
		{
			OracleParameter[] array = new OracleParameter[parameters.Count];
			for (int i = 0; i < parameters.Count; i++)
			{
				array[i] = (parameters[i].Clone() as OracleParameter);
			}
			return array;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0006FC5C File Offset: 0x0006EC5C
		private void ParseCommandText(OracleCommand command, OracleParameter[] parameters)
		{
			string commandText = command.CommandText;
			new ArrayList();
			Regex parserRegEx = BatchUpdateHelper.m_parserRegEx;
			this.m_usedParameterNames.Clear();
			this.m_tempStringBuilder.Remove(0, this.m_tempStringBuilder.Length);
			this.m_tempStringBuilder.Append(commandText);
			int num = 0;
			int num2 = 0;
			Match match = parserRegEx.Match(commandText);
			while (Match.Empty != match)
			{
				if (match.Groups[BatchUpdateHelper.m_bindParameterMarkerGroup].Success)
				{
					string text = match.Groups[BatchUpdateHelper.m_bindParameterMarkerGroup].Value.Substring(1);
					if (command.BindByName)
					{
						num2 = command.Parameters.IndexOf(text);
						if (0 > num2)
						{
							num2 = command.Parameters.IndexOf(BatchUpdateHelper.COLON + text);
							if (0 > num2)
							{
								throw new ArgumentOutOfRangeException();
							}
						}
					}
					string text2;
					if ((text2 = (this.m_usedParameterNames[text] as string)) == null)
					{
						text2 = BatchUpdateHelper.m_bindPrmPrefix + this.m_lastNumUsed;
						this.m_lastNumUsed++;
						this.m_usedParameterNames[text] = text2;
					}
					if (command.BindByName)
					{
						parameters[num2].ParameterName = text2;
					}
					this.m_tempStringBuilder.Remove(num + match.Index, match.Length);
					this.m_tempStringBuilder.Insert(num + match.Index, text2);
					num += text2.Length - match.Length;
				}
				match = match.NextMatch();
			}
			this.m_batchCmdTextBuilder.Append(this.m_tempStringBuilder.ToString());
			this.m_batchCmdTextBuilder.Append(BatchUpdateHelper.m_commandSuffix);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0006FE14 File Offset: 0x0006EE14
		private void ParseStoredProcedure(OracleCommand command, OracleParameter[] paramArray)
		{
			string commandText = command.CommandText;
			int num = paramArray.Length;
			this.m_tempStringBuilder.Remove(0, this.m_tempStringBuilder.Length);
			OracleParameter returnValueParam;
			if (paramArray == null || num == 0)
			{
				this.m_tempStringBuilder.Append("Begin " + commandText + "(); End;");
			}
			else if (!command.BindByName)
			{
				if ((returnValueParam = this.GetReturnValueParam(paramArray)) == null)
				{
					this.m_tempStringBuilder.Append(string.Concat(new object[]
					{
						"Begin ",
						commandText,
						"(",
						BatchUpdateHelper.m_bindPrmPrefix,
						this.m_lastNumUsed++
					}));
					for (int i = 1; i < num; i++)
					{
						this.m_tempStringBuilder.Append(", " + BatchUpdateHelper.m_bindPrmPrefix + this.m_lastNumUsed++);
					}
					this.m_tempStringBuilder.Append("); End;");
				}
				else
				{
					int i;
					if (paramArray[0] == returnValueParam)
					{
						if (num > 1)
						{
							this.m_tempStringBuilder.Append("Begin :ret" + this.m_lastNumUsed++);
							this.m_tempStringBuilder.Append(string.Concat(new object[]
							{
								" := ",
								commandText,
								"(",
								BatchUpdateHelper.m_bindPrmPrefix,
								this.m_lastNumUsed++
							}));
							i = 2;
						}
						else
						{
							this.m_tempStringBuilder.Append("Begin :ret" + this.m_lastNumUsed++);
							this.m_tempStringBuilder.Append(" := " + commandText + "(");
							i = 1;
						}
					}
					else
					{
						this.m_tempStringBuilder.Append("Begin :ret" + this.m_lastNumUsed++);
						this.m_tempStringBuilder.Append(string.Concat(new object[]
						{
							" := ",
							commandText,
							"(",
							BatchUpdateHelper.m_bindPrmPrefix,
							this.m_lastNumUsed++
						}));
						i = 1;
					}
					while (i < num)
					{
						if (paramArray[i] != returnValueParam)
						{
							this.m_tempStringBuilder.Append(", " + BatchUpdateHelper.m_bindPrmPrefix + this.m_lastNumUsed++);
						}
						i++;
					}
					this.m_tempStringBuilder.Append("); End;");
				}
			}
			else if ((returnValueParam = this.GetReturnValueParam(paramArray)) == null)
			{
				this.m_tempStringBuilder.Append(string.Concat(new object[]
				{
					"Begin ",
					commandText,
					"(",
					paramArray[0].ParameterName,
					"=>",
					BatchUpdateHelper.m_bindPrmPrefix,
					this.m_lastNumUsed
				}));
				paramArray[0].ParameterName = BatchUpdateHelper.m_bindPrmPrefix + this.m_lastNumUsed++;
				for (int i = 1; i < num; i++)
				{
					this.m_tempStringBuilder.Append(string.Concat(new object[]
					{
						", ",
						paramArray[i].ParameterName,
						"=>",
						BatchUpdateHelper.m_bindPrmPrefix,
						this.m_lastNumUsed
					}));
					paramArray[i].ParameterName = BatchUpdateHelper.m_bindPrmPrefix + this.m_lastNumUsed++;
				}
				this.m_tempStringBuilder.Append("); End;");
			}
			else
			{
				int i;
				if (paramArray[0] == returnValueParam)
				{
					if (num > 1)
					{
						this.m_tempStringBuilder.Append("Begin :ret" + this.m_lastNumUsed);
						returnValueParam.ParameterName = ":ret" + this.m_lastNumUsed++;
						this.m_tempStringBuilder.Append(string.Concat(new object[]
						{
							" := ",
							commandText,
							"(",
							paramArray[1].ParameterName,
							"=>",
							BatchUpdateHelper.m_bindPrmPrefix,
							this.m_lastNumUsed
						}));
						paramArray[1].ParameterName = BatchUpdateHelper.m_bindPrmPrefix + this.m_lastNumUsed++;
						i = 2;
					}
					else
					{
						this.m_tempStringBuilder.Append("Begin :ret" + this.m_lastNumUsed);
						returnValueParam.ParameterName = ":ret" + this.m_lastNumUsed++;
						this.m_tempStringBuilder.Append(" := " + commandText + "(");
						i = 1;
					}
				}
				else
				{
					this.m_tempStringBuilder.Append("Begin :ret" + this.m_lastNumUsed);
					returnValueParam.ParameterName = ":ret" + this.m_lastNumUsed++;
					this.m_tempStringBuilder.Append(string.Concat(new object[]
					{
						" := ",
						commandText,
						"(",
						paramArray[0].ParameterName,
						"=>",
						BatchUpdateHelper.m_bindPrmPrefix,
						this.m_lastNumUsed
					}));
					paramArray[0].ParameterName = BatchUpdateHelper.m_bindPrmPrefix + this.m_lastNumUsed++;
					i = 1;
				}
				while (i < num)
				{
					if (paramArray[i] != returnValueParam)
					{
						this.m_tempStringBuilder.Append(string.Concat(new object[]
						{
							", ",
							paramArray[i].ParameterName,
							"=>",
							BatchUpdateHelper.m_bindPrmPrefix,
							this.m_lastNumUsed
						}));
						paramArray[i].ParameterName = BatchUpdateHelper.m_bindPrmPrefix + this.m_lastNumUsed++;
					}
					i++;
				}
				this.m_tempStringBuilder.Append("); End;");
			}
			this.m_batchCmdTextBuilder.Append(this.m_tempStringBuilder.ToString());
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0007051C File Offset: 0x0006F51C
		private OracleParameter GetReturnValueParam(OracleParameter[] paramArray)
		{
			int num = paramArray.Length;
			for (int i = 0; i < num; i++)
			{
				if (paramArray[i].Direction == ParameterDirection.ReturnValue)
				{
					return paramArray[i];
				}
			}
			return null;
		}

		// Token: 0x040008F7 RID: 2295
		private OracleCommand m_batchCommand;

		// Token: 0x040008F8 RID: 2296
		private StringBuilder m_batchCmdTextBuilder;

		// Token: 0x040008F9 RID: 2297
		private StringBuilder m_tempStringBuilder;

		// Token: 0x040008FA RID: 2298
		private ArrayList m_paramArrayArray;

		// Token: 0x040008FB RID: 2299
		private Hashtable m_usedParameterNames;

		// Token: 0x040008FC RID: 2300
		private static Regex m_parserRegEx;

		// Token: 0x040008FD RID: 2301
		private static string m_strDeclareBlock;

		// Token: 0x040008FE RID: 2302
		private static string m_strExceptionBlock;

		// Token: 0x040008FF RID: 2303
		private static string m_strRowCountBlock;

		// Token: 0x04000900 RID: 2304
		private static string m_outParamAssignmentBlock;

		// Token: 0x04000901 RID: 2305
		private OracleParameter m_prmRowsModified;

		// Token: 0x04000902 RID: 2306
		private OracleParameter m_prmErrCodesArray;

		// Token: 0x04000903 RID: 2307
		private OracleParameter m_prmErrMsgArray;

		// Token: 0x04000904 RID: 2308
		private OracleParameter m_prmRowsMdArray;

		// Token: 0x04000905 RID: 2309
		private int m_lastNumUsed;

		// Token: 0x04000906 RID: 2310
		private int m_batchSizeCounter;

		// Token: 0x04000907 RID: 2311
		private static string m_plsqlBlockPrefix = "BEGIN\n";

		// Token: 0x04000908 RID: 2312
		private static string m_plsqlBlockSuffix = "END;";

		// Token: 0x04000909 RID: 2313
		private static string m_bindPrmPrefix = ":C";

		// Token: 0x0400090A RID: 2314
		private static string m_commandSuffix = ";\n";

		// Token: 0x0400090B RID: 2315
		private static string COLON = ":";

		// Token: 0x0400090C RID: 2316
		private static int m_bindParameterMarkerGroup;
	}
}
