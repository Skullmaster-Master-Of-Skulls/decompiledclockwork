using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Common.Internal
{
	// Token: 0x020003C4 RID: 964
	internal static class MultipartIdentifier
	{
		// Token: 0x0600342A RID: 13354 RVA: 0x000C9518 File Offset: 0x000C7718
		private static void IncrementStringCount(List<string> ary, ref int position)
		{
			position++;
			ary.Add(string.Empty);
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000C952B File Offset: 0x000C772B
		private static bool IsWhitespace(char ch)
		{
			return char.IsWhiteSpace(ch);
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000C9534 File Offset: 0x000C7734
		internal static List<string> ParseMultipartIdentifier(string name, string leftQuote, string rightQuote, char separator)
		{
			List<string> list = new List<string>();
			list.Add(null);
			int index = 0;
			MultipartIdentifier.MPIState mpistate = MultipartIdentifier.MPIState.MPI_Value;
			StringBuilder stringBuilder = new StringBuilder(name.Length);
			StringBuilder stringBuilder2 = null;
			char c = ' ';
			foreach (char c2 in name)
			{
				switch (mpistate)
				{
				case MultipartIdentifier.MPIState.MPI_Value:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						int index2;
						if (c2 == separator)
						{
							list[index] = string.Empty;
							MultipartIdentifier.IncrementStringCount(list, ref index);
						}
						else if (-1 != (index2 = leftQuote.IndexOf(c2)))
						{
							c = rightQuote[index2];
							stringBuilder.Length = 0;
							mpistate = MultipartIdentifier.MPIState.MPI_ParseQuote;
						}
						else
						{
							if (-1 != rightQuote.IndexOf(c2))
							{
								throw EntityUtil.ADP_InvalidMultipartNameDelimiterUsage();
							}
							stringBuilder.Length = 0;
							stringBuilder.Append(c2);
							mpistate = MultipartIdentifier.MPIState.MPI_ParseNonQuote;
						}
					}
					break;
				case MultipartIdentifier.MPIState.MPI_ParseNonQuote:
					if (c2 == separator)
					{
						list[index] = stringBuilder.ToString();
						MultipartIdentifier.IncrementStringCount(list, ref index);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					else
					{
						if (-1 != rightQuote.IndexOf(c2))
						{
							throw EntityUtil.ADP_InvalidMultipartNameDelimiterUsage();
						}
						if (-1 != leftQuote.IndexOf(c2))
						{
							throw EntityUtil.ADP_InvalidMultipartNameDelimiterUsage();
						}
						if (MultipartIdentifier.IsWhitespace(c2))
						{
							list[index] = stringBuilder.ToString();
							if (stringBuilder2 == null)
							{
								stringBuilder2 = new StringBuilder();
							}
							stringBuilder2.Length = 0;
							stringBuilder2.Append(c2);
							mpistate = MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator;
						}
						else
						{
							stringBuilder.Append(c2);
						}
					}
					break;
				case MultipartIdentifier.MPIState.MPI_LookForSeparator:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						if (c2 != separator)
						{
							throw EntityUtil.ADP_InvalidMultipartNameDelimiterUsage();
						}
						MultipartIdentifier.IncrementStringCount(list, ref index);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					break;
				case MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						if (c2 == separator)
						{
							MultipartIdentifier.IncrementStringCount(list, ref index);
							mpistate = MultipartIdentifier.MPIState.MPI_Value;
						}
						else
						{
							stringBuilder.Append(stringBuilder2);
							stringBuilder.Append(c2);
							list[index] = stringBuilder.ToString();
							mpistate = MultipartIdentifier.MPIState.MPI_ParseNonQuote;
						}
					}
					else
					{
						stringBuilder2.Append(c2);
					}
					break;
				case MultipartIdentifier.MPIState.MPI_ParseQuote:
					if (c2 == c)
					{
						mpistate = MultipartIdentifier.MPIState.MPI_RightQuote;
					}
					else
					{
						stringBuilder.Append(c2);
					}
					break;
				case MultipartIdentifier.MPIState.MPI_RightQuote:
					if (c2 == c)
					{
						stringBuilder.Append(c2);
						mpistate = MultipartIdentifier.MPIState.MPI_ParseQuote;
					}
					else if (c2 == separator)
					{
						list[index] = stringBuilder.ToString();
						MultipartIdentifier.IncrementStringCount(list, ref index);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					else
					{
						if (!MultipartIdentifier.IsWhitespace(c2))
						{
							throw EntityUtil.ADP_InvalidMultipartNameDelimiterUsage();
						}
						list[index] = stringBuilder.ToString();
						mpistate = MultipartIdentifier.MPIState.MPI_LookForSeparator;
					}
					break;
				}
			}
			switch (mpistate)
			{
			case MultipartIdentifier.MPIState.MPI_Value:
			case MultipartIdentifier.MPIState.MPI_LookForSeparator:
			case MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator:
				return list;
			case MultipartIdentifier.MPIState.MPI_ParseNonQuote:
			case MultipartIdentifier.MPIState.MPI_RightQuote:
				list[index] = stringBuilder.ToString();
				return list;
			}
			throw EntityUtil.ADP_InvalidMultipartNameDelimiterUsage();
		}

		// Token: 0x040016AF RID: 5807
		private const int MaxParts = 4;

		// Token: 0x040016B0 RID: 5808
		internal const int ServerIndex = 0;

		// Token: 0x040016B1 RID: 5809
		internal const int CatalogIndex = 1;

		// Token: 0x040016B2 RID: 5810
		internal const int SchemaIndex = 2;

		// Token: 0x040016B3 RID: 5811
		internal const int TableIndex = 3;

		// Token: 0x02000691 RID: 1681
		private enum MPIState
		{
			// Token: 0x04001FED RID: 8173
			MPI_Value,
			// Token: 0x04001FEE RID: 8174
			MPI_ParseNonQuote,
			// Token: 0x04001FEF RID: 8175
			MPI_LookForSeparator,
			// Token: 0x04001FF0 RID: 8176
			MPI_LookForNextCharOrSeparator,
			// Token: 0x04001FF1 RID: 8177
			MPI_ParseQuote,
			// Token: 0x04001FF2 RID: 8178
			MPI_RightQuote
		}
	}
}
