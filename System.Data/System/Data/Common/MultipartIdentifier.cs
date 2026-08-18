using System;
using System.Text;

namespace System.Data.Common
{
	// Token: 0x02000152 RID: 338
	internal class MultipartIdentifier
	{
		// Token: 0x06001578 RID: 5496 RVA: 0x00244A18 File Offset: 0x00243E18
		internal static string[] ParseMultipartIdentifier(string name, string leftQuote, string rightQuote, string property, bool ThrowOnEmptyMultipartName)
		{
			return MultipartIdentifier.ParseMultipartIdentifier(name, leftQuote, rightQuote, '.', 4, true, property, ThrowOnEmptyMultipartName);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00244A38 File Offset: 0x00243E38
		private static void IncrementStringCount(string name, string[] ary, ref int position, string property)
		{
			position++;
			int num = ary.Length;
			if (position >= num)
			{
				throw ADP.InvalidMultipartNameToManyParts(property, name, num);
			}
			ary[position] = string.Empty;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00244A68 File Offset: 0x00243E68
		private static bool IsWhitespace(char ch)
		{
			return char.IsWhiteSpace(ch);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00244A88 File Offset: 0x00243E88
		internal static string[] ParseMultipartIdentifier(string name, string leftQuote, string rightQuote, char separator, int limit, bool removequotes, string property, bool ThrowOnEmptyMultipartName)
		{
			if (limit <= 0)
			{
				throw ADP.InvalidMultipartNameToManyParts(property, name, limit);
			}
			if (-1 != leftQuote.IndexOf(separator) || -1 != rightQuote.IndexOf(separator) || leftQuote.Length != rightQuote.Length)
			{
				throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
			}
			string[] array = new string[limit];
			int num = 0;
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
						int index;
						if (c2 == separator)
						{
							array[num] = string.Empty;
							MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						}
						else if (-1 != (index = leftQuote.IndexOf(c2)))
						{
							c = rightQuote[index];
							stringBuilder.Length = 0;
							if (!removequotes)
							{
								stringBuilder.Append(c2);
							}
							mpistate = MultipartIdentifier.MPIState.MPI_ParseQuote;
						}
						else
						{
							if (-1 != rightQuote.IndexOf(c2))
							{
								throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
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
						array[num] = stringBuilder.ToString();
						MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					else
					{
						if (-1 != rightQuote.IndexOf(c2))
						{
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						if (-1 != leftQuote.IndexOf(c2))
						{
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						if (MultipartIdentifier.IsWhitespace(c2))
						{
							array[num] = stringBuilder.ToString();
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
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					break;
				case MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						if (c2 == separator)
						{
							MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
							mpistate = MultipartIdentifier.MPIState.MPI_Value;
						}
						else
						{
							stringBuilder.Append(stringBuilder2);
							stringBuilder.Append(c2);
							array[num] = stringBuilder.ToString();
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
						if (!removequotes)
						{
							stringBuilder.Append(c2);
						}
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
						array[num] = stringBuilder.ToString();
						MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					else
					{
						if (!MultipartIdentifier.IsWhitespace(c2))
						{
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						array[num] = stringBuilder.ToString();
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
				goto IL_2CA;
			case MultipartIdentifier.MPIState.MPI_ParseNonQuote:
			case MultipartIdentifier.MPIState.MPI_RightQuote:
				array[num] = stringBuilder.ToString();
				goto IL_2CA;
			}
			throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
			IL_2CA:
			if (array[0] == null)
			{
				if (ThrowOnEmptyMultipartName)
				{
					throw ADP.InvalidMultipartName(property, name);
				}
			}
			else
			{
				int num2 = limit - num - 1;
				if (num2 > 0)
				{
					for (int j = limit - 1; j >= num2; j--)
					{
						array[j] = array[j - num2];
						array[j - num2] = null;
					}
				}
			}
			return array;
		}

		// Token: 0x04000CA4 RID: 3236
		private const int MaxParts = 4;

		// Token: 0x04000CA5 RID: 3237
		internal const int ServerIndex = 0;

		// Token: 0x04000CA6 RID: 3238
		internal const int CatalogIndex = 1;

		// Token: 0x04000CA7 RID: 3239
		internal const int SchemaIndex = 2;

		// Token: 0x04000CA8 RID: 3240
		internal const int TableIndex = 3;

		// Token: 0x02000153 RID: 339
		private enum MPIState
		{
			// Token: 0x04000CAA RID: 3242
			MPI_Value,
			// Token: 0x04000CAB RID: 3243
			MPI_ParseNonQuote,
			// Token: 0x04000CAC RID: 3244
			MPI_LookForSeparator,
			// Token: 0x04000CAD RID: 3245
			MPI_LookForNextCharOrSeparator,
			// Token: 0x04000CAE RID: 3246
			MPI_ParseQuote,
			// Token: 0x04000CAF RID: 3247
			MPI_RightQuote
		}
	}
}
