using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000472 RID: 1138
	internal class ErrorLog : InternalBase
	{
		// Token: 0x060029EB RID: 10731 RVA: 0x000CA50B File Offset: 0x000C870B
		internal ErrorLog()
		{
			this.m_log = new List<ErrorLog.Record>();
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x000CA51E File Offset: 0x000C871E
		internal int Count
		{
			get
			{
				return this.m_log.Count;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060029ED RID: 10733 RVA: 0x000CA6B8 File Offset: 0x000C88B8
		internal IEnumerable<EdmSchemaError> Errors
		{
			get
			{
				foreach (ErrorLog.Record record in this.m_log)
				{
					yield return record.Error;
				}
				yield break;
			}
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000CA6D5 File Offset: 0x000C88D5
		internal void AddEntry(ErrorLog.Record record)
		{
			this.m_log.Add(record);
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000CA6E4 File Offset: 0x000C88E4
		internal void Merge(ErrorLog log)
		{
			foreach (ErrorLog.Record item in log.m_log)
			{
				this.m_log.Add(item);
			}
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000CA73C File Offset: 0x000C893C
		internal void PrintTrace()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			Helpers.StringTraceLine(stringBuilder.ToString());
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000CA764 File Offset: 0x000C8964
		internal override void ToCompactString(StringBuilder builder)
		{
			foreach (ErrorLog.Record record in this.m_log)
			{
				record.ToCompactString(builder);
			}
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000CA7B8 File Offset: 0x000C89B8
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

		// Token: 0x04000F89 RID: 3977
		private readonly List<ErrorLog.Record> m_log;

		// Token: 0x02000473 RID: 1139
		internal class Record : InternalBase
		{
			// Token: 0x060029F3 RID: 10739 RVA: 0x000CA820 File Offset: 0x000C8A20
			internal Record(ViewGenErrorCode errorCode, string message, IEnumerable<LeftCellWrapper> wrappers, string debugMessage)
			{
				IEnumerable<Cell> inputCellsForWrappers = LeftCellWrapper.GetInputCellsForWrappers(wrappers);
				this.Init(errorCode, message, inputCellsForWrappers, debugMessage);
			}

			// Token: 0x060029F4 RID: 10740 RVA: 0x000CA848 File Offset: 0x000C8A48
			internal Record(ViewGenErrorCode errorCode, string message, Cell sourceCell, string debugMessage)
			{
				this.Init(errorCode, message, new Cell[]
				{
					sourceCell
				}, debugMessage);
			}

			// Token: 0x060029F5 RID: 10741 RVA: 0x000CA871 File Offset: 0x000C8A71
			internal Record(ViewGenErrorCode errorCode, string message, IEnumerable<Cell> sourceCells, string debugMessage)
			{
				this.Init(errorCode, message, sourceCells, debugMessage);
			}

			// Token: 0x060029F6 RID: 10742 RVA: 0x000CA884 File Offset: 0x000C8A84
			internal Record(EdmSchemaError error)
			{
				this.m_debugMessage = error.ToString();
				this.m_mappingError = error;
			}

			// Token: 0x060029F7 RID: 10743 RVA: 0x000CA8A0 File Offset: 0x000C8AA0
			private void Init(ViewGenErrorCode errorCode, string message, IEnumerable<Cell> sourceCells, string debugMessage)
			{
				this.m_sourceCells = new List<Cell>(sourceCells);
				CellLabel cellLabel = this.m_sourceCells[0].CellLabel;
				string sourceLocation = cellLabel.SourceLocation;
				int startLineNumber = cellLabel.StartLineNumber;
				int startLinePosition = cellLabel.StartLinePosition;
				string message2 = ErrorLog.Record.InternalToString(message, debugMessage, this.m_sourceCells, errorCode, false);
				this.m_debugMessage = ErrorLog.Record.InternalToString(message, debugMessage, this.m_sourceCells, errorCode, true);
				this.m_mappingError = new EdmSchemaError(message2, (int)errorCode, EdmSchemaErrorSeverity.Error, sourceLocation, startLineNumber, startLinePosition);
			}

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x060029F8 RID: 10744 RVA: 0x000CA91A File Offset: 0x000C8B1A
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			internal EdmSchemaError Error
			{
				get
				{
					return this.m_mappingError;
				}
			}

			// Token: 0x060029F9 RID: 10745 RVA: 0x000CA922 File Offset: 0x000C8B22
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append(this.m_debugMessage);
			}

			// Token: 0x060029FA RID: 10746 RVA: 0x000CA940 File Offset: 0x000C8B40
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

			// Token: 0x060029FB RID: 10747 RVA: 0x000CA9FC File Offset: 0x000C8BFC
			private static string InternalToString(string message, string debugMessage, List<Cell> sourceCells, ViewGenErrorCode errorCode, bool isInvariant)
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

			// Token: 0x060029FC RID: 10748 RVA: 0x000CAAE9 File Offset: 0x000C8CE9
			internal string ToUserString()
			{
				return this.m_mappingError.ToString();
			}

			// Token: 0x04000F8A RID: 3978
			private EdmSchemaError m_mappingError;

			// Token: 0x04000F8B RID: 3979
			private List<Cell> m_sourceCells;

			// Token: 0x04000F8C RID: 3980
			private string m_debugMessage;
		}
	}
}
