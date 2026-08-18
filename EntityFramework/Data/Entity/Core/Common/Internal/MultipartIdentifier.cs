using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Data.Entity.Core.Common.Internal
{
	// Token: 0x020002F2 RID: 754
	internal static class MultipartIdentifier
	{
		// Token: 0x06001AAE RID: 6830 RVA: 0x00085306 File Offset: 0x00083506
		private static void IncrementStringCount(List<string> ary, ref int position)
		{
			position++;
			ary.Add(string.Empty);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00085319 File Offset: 0x00083519
		private static bool IsWhitespace(char ch)
		{
			return char.IsWhiteSpace(ch);
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00085324 File Offset: 0x00083524
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
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
								throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
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
							throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
						}
						if (-1 != leftQuote.IndexOf(c2))
						{
							throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
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
							throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
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
							throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
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
			throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
		}

		// Token: 0x04000934 RID: 2356
		private const int MaxParts = 4;

		// Token: 0x04000935 RID: 2357
		internal const int ServerIndex = 0;

		// Token: 0x04000936 RID: 2358
		internal const int CatalogIndex = 1;

		// Token: 0x04000937 RID: 2359
		internal const int SchemaIndex = 2;

		// Token: 0x04000938 RID: 2360
		internal const int TableIndex = 3;

		// Token: 0x020002F3 RID: 755
		private enum MPIState
		{
			// Token: 0x0400093A RID: 2362
			MPI_Value,
			// Token: 0x0400093B RID: 2363
			MPI_ParseNonQuote,
			// Token: 0x0400093C RID: 2364
			MPI_LookForSeparator,
			// Token: 0x0400093D RID: 2365
			MPI_LookForNextCharOrSeparator,
			// Token: 0x0400093E RID: 2366
			MPI_ParseQuote,
			// Token: 0x0400093F RID: 2367
			MPI_RightQuote
		}
	}
}
