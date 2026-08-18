using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002AA RID: 682
	internal class ErrorLog : InternalBase
	{
		// Token: 0x0600288B RID: 10379 RVA: 0x0009CEEB File Offset: 0x0009B0EB
		internal ErrorLog()
		{
			this.m_log = new List<ErrorLog.Record>();
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x0009CEFE File Offset: 0x0009B0FE
		internal int Count
		{
			get
			{
				return this.m_log.Count;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x0009CF0C File Offset: 0x0009B10C
		internal IEnumerable<EdmSchemaError> Errors
		{
			get
			{
				foreach (ErrorLog.Record record in this.m_log)
				{
					yield return record.Error;
				}
				List<ErrorLog.Record>.Enumerator enumerator = default(List<ErrorLog.Record>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x0009CF29 File Offset: 0x0009B129
		internal void AddEntry(ErrorLog.Record record)
		{
			EntityUtil.CheckArgumentNull<ErrorLog.Record>(record, "record");
			this.m_log.Add(record);
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x0009CF44 File Offset: 0x0009B144
		internal void Merge(ErrorLog log)
		{
			foreach (ErrorLog.Record item in log.m_log)
			{
				this.m_log.Add(item);
			}
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x0009CF9C File Offset: 0x0009B19C
		internal void PrintTrace()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			Helpers.StringTraceLine(stringBuilder.ToString());
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x0009CFC4 File Offset: 0x0009B1C4
		internal override void ToCompactString(StringBuilder builder)
		{
			foreach (ErrorLog.Record record in this.m_log)
			{
				record.ToCompactString(builder);
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x0009D018 File Offset: 0x0009B218
		internal string ToUserString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ErrorLog.Record record in this.m_log)
			{
				string value = record.ToUserString();
				stringBuilder.AppendLine(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400125C RID: 4700
		private List<ErrorLog.Record> m_log;

		// Token: 0x020005EC RID: 1516
		internal class Record : InternalBase
		{
			// Token: 0x060041DD RID: 16861 RVA: 0x000EFA34 File Offset: 0x000EDC34
			internal Record(bool isError, ViewGenErrorCode errorCode, string message, IEnumerable<LeftCellWrapper> wrappers, string debugMessage)
			{
				IEnumerable<Cell> inputCellsForWrappers = LeftCellWrapper.GetInputCellsForWrappers(wrappers);
				this.Init(isError, errorCode, message, inputCellsForWrappers, debugMessage);
			}

			// Token: 0x060041DE RID: 16862 RVA: 0x000EFA5B File Offset: 0x000EDC5B
			internal Record(bool isError, ViewGenErrorCode errorCode, string message, Cell sourceCell, string debugMessage)
			{
				this.Init(isError, errorCode, message, new Cell[]
				{
					sourceCell
				}, debugMessage);
			}

			// Token: 0x060041DF RID: 16863 RVA: 0x000EFA79 File Offset: 0x000EDC79
			internal Record(bool isError, ViewGenErrorCode errorCode, string message, IEnumerable<Cell> sourceCells, string debugMessage)
			{
				this.Init(isError, errorCode, message, sourceCells, debugMessage);
			}

			// Token: 0x060041E0 RID: 16864 RVA: 0x000EFA8E File Offset: 0x000EDC8E
			internal Record(EdmSchemaError error)
			{
				this.m_debugMessage = error.ToString();
				this.m_mappingError = error;
			}

			// Token: 0x060041E1 RID: 16865 RVA: 0x000EFAAC File Offset: 0x000EDCAC
			private void Init(bool isError, ViewGenErrorCode errorCode, string message, IEnumerable<Cell> sourceCells, string debugMessage)
			{
				this.m_sourceCells = new List<Cell>(sourceCells);
				CellLabel cellLabel = this.m_sourceCells[0].CellLabel;
				string sourceLocation = cellLabel.SourceLocation;
				int startLineNumber = cellLabel.StartLineNumber;
				int startLinePosition = cellLabel.StartLinePosition;
				string message2 = ErrorLog.Record.InternalToString(message, debugMessage, this.m_sourceCells, sourceLocation, errorCode, isError, false);
				this.m_debugMessage = ErrorLog.Record.InternalToString(message, debugMessage, this.m_sourceCells, sourceLocation, errorCode, isError, true);
				this.m_mappingError = new EdmSchemaError(message2, (int)errorCode, EdmSchemaErrorSeverity.Error, sourceLocation, startLineNumber, startLinePosition);
			}

			// Token: 0x17000B62 RID: 2914
			// (get) Token: 0x060041E2 RID: 16866 RVA: 0x000EFB2B File Offset: 0x000EDD2B
			internal EdmSchemaError Error
			{
				get
				{
					return this.m_mappingError;
				}
			}

			// Token: 0x060041E3 RID: 16867 RVA: 0x000EFB33 File Offset: 0x000EDD33
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append(this.m_debugMessage);
			}

			// Token: 0x060041E4 RID: 16868 RVA: 0x000EFB44 File Offset: 0x000EDD44
			private static void GetUserLinesFromCells(IEnumerable<Cell> sourceCells, StringBuilder lineBuilder, bool isInvariant)
			{
				IOrderedEnumerable<Cell> orderedEnumerable = sourceCells.OrderBy((Cell cell) => cell.CellLabel.StartLineNumber, Comparer<int>.Default);
				bool flag = true;
				foreach (Cell cell2 in orderedEnumerable)
				{
					if (!flag)
					{
						lineBuilder.Append(isInvariant ? EntityRes.GetString("ViewGen_CommaBlank") : ", ");
					}
					flag = false;
					lineBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}", new object[]
					{
						cell2.CellLabel.StartLineNumber
					});
				}
			}

			// Token: 0x060041E5 RID: 16869 RVA: 0x000EFBFC File Offset: 0x000EDDFC
			private static string InternalToString(string message, string debugMessage, List<Cell> sourceCells, string sourceLocation, ViewGenErrorCode errorCode, bool isError, bool isInvariant)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (isInvariant)
				{
					stringBuilder.AppendLine(debugMessage);
					stringBuilder.Append(isInvariant ? "ERROR" : Strings.ViewGen_Error);
					StringUtil.FormatStringBuilder(stringBuilder, " ({0}): ", new object[]
					{
						(int)errorCode
					});
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				ErrorLog.Record.GetUserLinesFromCells(sourceCells, stringBuilder2, isInvariant);
				if (isInvariant)
				{
					if (sourceCells.Count > 1)
					{
						StringUtil.FormatStringBuilder(stringBuilder, "Problem in Mapping Fragments starting at lines {0}: ", new object[]
						{
							stringBuilder2.ToString()
						});
					}
					else
					{
						StringUtil.FormatStringBuilder(stringBuilder, "Problem in Mapping Fragment starting at line {0}: ", new object[]
						{
							stringBuilder2.ToString()
						});
					}
				}
				else if (sourceCells.Count > 1)
				{
					stringBuilder.Append(Strings.ViewGen_ErrorLog2(stringBuilder2.ToString()));
				}
				else
				{
					stringBuilder.Append(Strings.ViewGen_ErrorLog(stringBuilder2.ToString()));
				}
				stringBuilder.AppendLine(message);
				return stringBuilder.ToString();
			}

			// Token: 0x060041E6 RID: 16870 RVA: 0x000EFCE1 File Offset: 0x000EDEE1
			internal string ToUserString()
			{
				return this.m_mappingError.ToString();
			}

			// Token: 0x04001DA5 RID: 7589
			private EdmSchemaError m_mappingError;

			// Token: 0x04001DA6 RID: 7590
			private List<Cell> m_sourceCells;

			// Token: 0x04001DA7 RID: 7591
			private string m_debugMessage;
		}
	}
}
