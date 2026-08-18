using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000020 RID: 32
	public class FileSelector
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000030FB File Offset: 0x000012FB
		public FileSelector(string selectionCriteria) : this(selectionCriteria, true)
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003105 File Offset: 0x00001305
		public FileSelector(string selectionCriteria, bool traverseDirectoryReparsePoints)
		{
			if (!string.IsNullOrEmpty(selectionCriteria))
			{
				this._Criterion = FileSelector._ParseCriterion(selectionCriteria);
			}
			this.TraverseReparsePoints = traverseDirectoryReparsePoints;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003128 File Offset: 0x00001328
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000313F File Offset: 0x0000133F
		public string SelectionCriteria
		{
			get
			{
				if (this._Criterion == null)
				{
					return null;
				}
				return this._Criterion.ToString();
			}
			set
			{
				if (value == null)
				{
					this._Criterion = null;
					return;
				}
				if (value.Trim() == "")
				{
					this._Criterion = null;
					return;
				}
				this._Criterion = FileSelector._ParseCriterion(value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003172 File Offset: 0x00001372
		// (set) Token: 0x0600008F RID: 143 RVA: 0x0000317A File Offset: 0x0000137A
		public bool TraverseReparsePoints { get; set; }

		// Token: 0x06000090 RID: 144 RVA: 0x00003184 File Offset: 0x00001384
		private static string NormalizeCriteriaExpression(string source)
		{
			string[][] array = new string[][]
			{
				new string[]
				{
					"([^']*)\\(\\(([^']+)",
					"$1( ($2"
				},
				new string[]
				{
					"(.)\\)\\)",
					"$1) )"
				},
				new string[]
				{
					"\\((\\S)",
					"( $1"
				},
				new string[]
				{
					"(\\S)\\)",
					"$1 )"
				},
				new string[]
				{
					"^\\)",
					" )"
				},
				new string[]
				{
					"(\\S)\\(",
					"$1 ("
				},
				new string[]
				{
					"\\)(\\S)",
					") $1"
				},
				new string[]
				{
					"(=)('[^']*')",
					"$1 $2"
				},
				new string[]
				{
					"([^ !><])(>|<|!=|=)",
					"$1 $2"
				},
				new string[]
				{
					"(>|<|!=|=)([^ =])",
					"$1 $2"
				},
				new string[]
				{
					"/",
					"\\"
				}
			};
			string input = source;
			for (int i = 0; i < array.Length; i++)
			{
				string pattern = FileSelector.RegexAssertions.PrecededByEvenNumberOfSingleQuotes + array[i][0] + FileSelector.RegexAssertions.FollowedByEvenNumberOfSingleQuotesAndLineEnd;
				input = Regex.Replace(input, pattern, array[i][1]);
			}
			string pattern2 = "/" + FileSelector.RegexAssertions.FollowedByOddNumberOfSingleQuotesAndLineEnd;
			input = Regex.Replace(input, pattern2, "\\");
			pattern2 = " " + FileSelector.RegexAssertions.FollowedByOddNumberOfSingleQuotesAndLineEnd;
			return Regex.Replace(input, pattern2, "\u0006");
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003370 File Offset: 0x00001570
		private static SelectionCriterion _ParseCriterion(string s)
		{
			if (s == null)
			{
				return null;
			}
			s = FileSelector.NormalizeCriteriaExpression(s);
			if (s.IndexOf(" ") == -1)
			{
				s = "name = " + s;
			}
			string[] array = s.Trim().Split(new char[]
			{
				' ',
				'\t'
			});
			if (array.Length < 3)
			{
				throw new ArgumentException(s);
			}
			SelectionCriterion selectionCriterion = null;
			Stack<FileSelector.ParseState> stack = new Stack<FileSelector.ParseState>();
			Stack<SelectionCriterion> stack2 = new Stack<SelectionCriterion>();
			stack.Push(FileSelector.ParseState.Start);
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i].ToLower();
				string key;
				if ((key = text) != null)
				{
					if (<PrivateImplementationDetails>{510A9A0A-2EB8-4C1C-AA23-D4ACD845FEA7}.$$method0x6000091-1 == null)
					{
						<PrivateImplementationDetails>{510A9A0A-2EB8-4C1C-AA23-D4ACD845FEA7}.$$method0x6000091-1 = new Dictionary<string, int>(16)
						{
							{
								"and",
								0
							},
							{
								"xor",
								1
							},
							{
								"or",
								2
							},
							{
								"(",
								3
							},
							{
								")",
								4
							},
							{
								"atime",
								5
							},
							{
								"ctime",
								6
							},
							{
								"mtime",
								7
							},
							{
								"length",
								8
							},
							{
								"size",
								9
							},
							{
								"filename",
								10
							},
							{
								"name",
								11
							},
							{
								"attrs",
								12
							},
							{
								"attributes",
								13
							},
							{
								"type",
								14
							},
							{
								"",
								15
							}
						};
					}
					int num;
					if (<PrivateImplementationDetails>{510A9A0A-2EB8-4C1C-AA23-D4ACD845FEA7}.$$method0x6000091-1.TryGetValue(key, out num))
					{
						FileSelector.ParseState parseState;
						switch (num)
						{
						case 0:
						case 1:
						case 2:
						{
							parseState = stack.Peek();
							if (parseState != FileSelector.ParseState.CriterionDone)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							if (array.Length <= i + 3)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							LogicalConjunction conjunction = (LogicalConjunction)Enum.Parse(typeof(LogicalConjunction), array[i].ToUpper(), true);
							selectionCriterion = new CompoundCriterion
							{
								Left = selectionCriterion,
								Right = null,
								Conjunction = conjunction
							};
							stack.Push(parseState);
							stack.Push(FileSelector.ParseState.ConjunctionPending);
							stack2.Push(selectionCriterion);
							break;
						}
						case 3:
							parseState = stack.Peek();
							if (parseState != FileSelector.ParseState.Start && parseState != FileSelector.ParseState.ConjunctionPending && parseState != FileSelector.ParseState.OpenParen)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							if (array.Length <= i + 4)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							stack.Push(FileSelector.ParseState.OpenParen);
							break;
						case 4:
							parseState = stack.Pop();
							if (stack.Peek() != FileSelector.ParseState.OpenParen)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							stack.Pop();
							stack.Push(FileSelector.ParseState.CriterionDone);
							break;
						case 5:
						case 6:
						case 7:
						{
							if (array.Length <= i + 2)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							DateTime dateTime;
							try
							{
								dateTime = DateTime.ParseExact(array[i + 2], "yyyy-MM-dd-HH:mm:ss", null);
							}
							catch (FormatException)
							{
								try
								{
									dateTime = DateTime.ParseExact(array[i + 2], "yyyy/MM/dd-HH:mm:ss", null);
								}
								catch (FormatException)
								{
									try
									{
										dateTime = DateTime.ParseExact(array[i + 2], "yyyy/MM/dd", null);
									}
									catch (FormatException)
									{
										try
										{
											dateTime = DateTime.ParseExact(array[i + 2], "MM/dd/yyyy", null);
										}
										catch (FormatException)
										{
											dateTime = DateTime.ParseExact(array[i + 2], "yyyy-MM-dd", null);
										}
									}
								}
							}
							dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();
							selectionCriterion = new TimeCriterion
							{
								Which = (WhichTime)Enum.Parse(typeof(WhichTime), array[i], true),
								Operator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]),
								Time = dateTime
							};
							i += 2;
							stack.Push(FileSelector.ParseState.CriterionDone);
							break;
						}
						case 8:
						case 9:
						{
							if (array.Length <= i + 2)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							string text2 = array[i + 2];
							long size;
							if (text2.ToUpper().EndsWith("K"))
							{
								size = long.Parse(text2.Substring(0, text2.Length - 1)) * 1024L;
							}
							else if (text2.ToUpper().EndsWith("KB"))
							{
								size = long.Parse(text2.Substring(0, text2.Length - 2)) * 1024L;
							}
							else if (text2.ToUpper().EndsWith("M"))
							{
								size = long.Parse(text2.Substring(0, text2.Length - 1)) * 1024L * 1024L;
							}
							else if (text2.ToUpper().EndsWith("MB"))
							{
								size = long.Parse(text2.Substring(0, text2.Length - 2)) * 1024L * 1024L;
							}
							else if (text2.ToUpper().EndsWith("G"))
							{
								size = long.Parse(text2.Substring(0, text2.Length - 1)) * 1024L * 1024L * 1024L;
							}
							else if (text2.ToUpper().EndsWith("GB"))
							{
								size = long.Parse(text2.Substring(0, text2.Length - 2)) * 1024L * 1024L * 1024L;
							}
							else
							{
								size = long.Parse(array[i + 2]);
							}
							selectionCriterion = new SizeCriterion
							{
								Size = size,
								Operator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1])
							};
							i += 2;
							stack.Push(FileSelector.ParseState.CriterionDone);
							break;
						}
						case 10:
						case 11:
						{
							if (array.Length <= i + 2)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							ComparisonOperator comparisonOperator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]);
							if (comparisonOperator != ComparisonOperator.NotEqualTo && comparisonOperator != ComparisonOperator.EqualTo)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							string text3 = array[i + 2];
							if (text3.StartsWith("'") && text3.EndsWith("'"))
							{
								text3 = text3.Substring(1, text3.Length - 2).Replace("\u0006", " ");
							}
							selectionCriterion = new NameCriterion
							{
								MatchingFileSpec = text3,
								Operator = comparisonOperator
							};
							i += 2;
							stack.Push(FileSelector.ParseState.CriterionDone);
							break;
						}
						case 12:
						case 13:
						case 14:
						{
							if (array.Length <= i + 2)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							ComparisonOperator comparisonOperator2 = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]);
							if (comparisonOperator2 != ComparisonOperator.NotEqualTo && comparisonOperator2 != ComparisonOperator.EqualTo)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							selectionCriterion = ((text == "type") ? new TypeCriterion
							{
								AttributeString = array[i + 2],
								Operator = comparisonOperator2
							} : new AttributesCriterion
							{
								AttributeString = array[i + 2],
								Operator = comparisonOperator2
							});
							i += 2;
							stack.Push(FileSelector.ParseState.CriterionDone);
							break;
						}
						case 15:
							stack.Push(FileSelector.ParseState.Whitespace);
							break;
						default:
							goto IL_7AF;
						}
						parseState = stack.Peek();
						if (parseState == FileSelector.ParseState.CriterionDone)
						{
							stack.Pop();
							if (stack.Peek() == FileSelector.ParseState.ConjunctionPending)
							{
								while (stack.Peek() == FileSelector.ParseState.ConjunctionPending)
								{
									CompoundCriterion compoundCriterion = stack2.Pop() as CompoundCriterion;
									compoundCriterion.Right = selectionCriterion;
									selectionCriterion = compoundCriterion;
									stack.Pop();
									parseState = stack.Pop();
									if (parseState != FileSelector.ParseState.CriterionDone)
									{
										throw new ArgumentException("??");
									}
								}
							}
							else
							{
								stack.Push(FileSelector.ParseState.CriterionDone);
							}
						}
						if (parseState == FileSelector.ParseState.Whitespace)
						{
							stack.Pop();
						}
						i++;
						continue;
					}
				}
				IL_7AF:
				throw new ArgumentException("'" + array[i] + "'");
			}
			return selectionCriterion;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003C04 File Offset: 0x00001E04
		public override string ToString()
		{
			return "FileSelector(" + this._Criterion.ToString() + ")";
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003C20 File Offset: 0x00001E20
		private bool Evaluate(string filename)
		{
			return this._Criterion.Evaluate(filename);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003C3B File Offset: 0x00001E3B
		[Conditional("SelectorTrace")]
		private void SelectorTrace(string format, params object[] args)
		{
			if (this._Criterion != null && this._Criterion.Verbose)
			{
				Console.WriteLine(format, args);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003C59 File Offset: 0x00001E59
		public ICollection<string> SelectFiles(string directory)
		{
			return this.SelectFiles(directory, false);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003C64 File Offset: 0x00001E64
		public ReadOnlyCollection<string> SelectFiles(string directory, bool recurseDirectories)
		{
			if (this._Criterion == null)
			{
				throw new ArgumentException("SelectionCriteria has not been set");
			}
			List<string> list = new List<string>();
			try
			{
				if (Directory.Exists(directory))
				{
					string[] files = Directory.GetFiles(directory);
					foreach (string text in files)
					{
						if (this.Evaluate(text))
						{
							list.Add(text);
						}
					}
					if (recurseDirectories)
					{
						string[] directories = Directory.GetDirectories(directory);
						foreach (string text2 in directories)
						{
							if (this.TraverseReparsePoints || (File.GetAttributes(text2) & FileAttributes.ReparsePoint) == (FileAttributes)0)
							{
								if (this.Evaluate(text2))
								{
									list.Add(text2);
								}
								list.AddRange(this.SelectFiles(text2, recurseDirectories));
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (IOException)
			{
			}
			return list.AsReadOnly();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003D54 File Offset: 0x00001F54
		private bool Evaluate(ZipEntry entry)
		{
			return this._Criterion.Evaluate(entry);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003D70 File Offset: 0x00001F70
		public ICollection<ZipEntry> SelectEntries(ZipFile zip)
		{
			if (zip == null)
			{
				throw new ArgumentNullException("zip");
			}
			List<ZipEntry> list = new List<ZipEntry>();
			foreach (ZipEntry zipEntry in zip)
			{
				if (this.Evaluate(zipEntry))
				{
					list.Add(zipEntry);
				}
			}
			return list;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public ICollection<ZipEntry> SelectEntries(ZipFile zip, string directoryPathInArchive)
		{
			if (zip == null)
			{
				throw new ArgumentNullException("zip");
			}
			List<ZipEntry> list = new List<ZipEntry>();
			string text = (directoryPathInArchive == null) ? null : directoryPathInArchive.Replace("/", "\\");
			if (text != null)
			{
				while (text.EndsWith("\\"))
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			foreach (ZipEntry zipEntry in zip)
			{
				if ((directoryPathInArchive == null || Path.GetDirectoryName(zipEntry.FileName) == directoryPathInArchive || Path.GetDirectoryName(zipEntry.FileName) == text) && this.Evaluate(zipEntry))
				{
					list.Add(zipEntry);
				}
			}
			return list;
		}

		// Token: 0x04000051 RID: 81
		internal SelectionCriterion _Criterion;

		// Token: 0x02000021 RID: 33
		private enum ParseState
		{
			// Token: 0x04000054 RID: 84
			Start,
			// Token: 0x04000055 RID: 85
			OpenParen,
			// Token: 0x04000056 RID: 86
			CriterionDone,
			// Token: 0x04000057 RID: 87
			ConjunctionPending,
			// Token: 0x04000058 RID: 88
			Whitespace
		}

		// Token: 0x02000022 RID: 34
		private static class RegexAssertions
		{
			// Token: 0x04000059 RID: 89
			public static readonly string PrecededByOddNumberOfSingleQuotes = "(?<=(?:[^']*'[^']*')*'[^']*)";

			// Token: 0x0400005A RID: 90
			public static readonly string FollowedByOddNumberOfSingleQuotesAndLineEnd = "(?=[^']*'(?:[^']*'[^']*')*[^']*$)";

			// Token: 0x0400005B RID: 91
			public static readonly string PrecededByEvenNumberOfSingleQuotes = "(?<=(?:[^']*'[^']*')*[^']*)";

			// Token: 0x0400005C RID: 92
			public static readonly string FollowedByEvenNumberOfSingleQuotesAndLineEnd = "(?=(?:[^']*'[^']*')*[^']*$)";
		}
	}
}
