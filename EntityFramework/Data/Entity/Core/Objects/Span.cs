using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Text;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005BC RID: 1468
	internal sealed class Span
	{
		// Token: 0x06003ADD RID: 15069 RVA: 0x0011775C File Offset: 0x0011595C
		internal Span()
		{
			this._spanList = new List<Span.SpanPath>();
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x0011776F File Offset: 0x0011596F
		internal List<Span.SpanPath> SpanList
		{
			get
			{
				return this._spanList;
			}
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x00117777 File Offset: 0x00115977
		internal static bool RequiresRelationshipSpan(MergeOption mergeOption)
		{
			return mergeOption != MergeOption.NoTracking;
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x00117780 File Offset: 0x00115980
		internal static Span IncludeIn(Span spanToIncludeIn, string pathToInclude)
		{
			if (spanToIncludeIn == null)
			{
				spanToIncludeIn = new Span();
			}
			spanToIncludeIn.Include(pathToInclude);
			return spanToIncludeIn;
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x00117794 File Offset: 0x00115994
		internal static Span CopyUnion(Span span1, Span span2)
		{
			if (span1 == null)
			{
				return span2;
			}
			if (span2 == null)
			{
				return span1;
			}
			Span span3 = span1.Clone();
			foreach (Span.SpanPath spanPath in span2.SpanList)
			{
				span3.AddSpanPath(spanPath);
			}
			return span3;
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x001177FC File Offset: 0x001159FC
		internal string GetCacheKey()
		{
			if (this._cacheKey == null && this._spanList.Count > 0)
			{
				if (this._spanList.Count == 1 && this._spanList[0].Navigations.Count == 1)
				{
					this._cacheKey = this._spanList[0].Navigations[0];
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < this._spanList.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(";");
						}
						Span.SpanPath spanPath = this._spanList[i];
						stringBuilder.Append(spanPath.Navigations[0]);
						for (int j = 1; j < spanPath.Navigations.Count; j++)
						{
							stringBuilder.Append(".");
							stringBuilder.Append(spanPath.Navigations[j]);
						}
					}
					this._cacheKey = stringBuilder.ToString();
				}
			}
			return this._cacheKey;
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x00117904 File Offset: 0x00115B04
		public void Include(string path)
		{
			Check.NotEmpty(path, "path");
			Span.SpanPath spanPath = new Span.SpanPath(Span.ParsePath(path));
			this.AddSpanPath(spanPath);
			this._cacheKey = null;
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x00117938 File Offset: 0x00115B38
		internal Span Clone()
		{
			Span span = new Span();
			span.SpanList.AddRange(this._spanList);
			span._cacheKey = this._cacheKey;
			return span;
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x00117969 File Offset: 0x00115B69
		internal void AddSpanPath(Span.SpanPath spanPath)
		{
			if (this.ValidateSpanPath(spanPath))
			{
				this.RemoveExistingSubPaths(spanPath);
				this._spanList.Add(spanPath);
			}
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x00117988 File Offset: 0x00115B88
		private bool ValidateSpanPath(Span.SpanPath spanPath)
		{
			for (int i = 0; i < this._spanList.Count; i++)
			{
				if (spanPath.IsSubPath(this._spanList[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x001179C4 File Offset: 0x00115BC4
		private void RemoveExistingSubPaths(Span.SpanPath spanPath)
		{
			List<Span.SpanPath> list = new List<Span.SpanPath>();
			for (int i = 0; i < this._spanList.Count; i++)
			{
				if (this._spanList[i].IsSubPath(spanPath))
				{
					list.Add(this._spanList[i]);
				}
			}
			foreach (Span.SpanPath item in list)
			{
				this._spanList.Remove(item);
			}
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x00117A5C File Offset: 0x00115C5C
		private static List<string> ParsePath(string path)
		{
			List<string> list = MultipartIdentifier.ParseMultipartIdentifier(path, "[", "]", '.');
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i] == null)
				{
					list.RemoveAt(i);
				}
				else if (list[i].Length == 0)
				{
					throw new ArgumentException(Strings.ObjectQuery_Span_SpanPathSyntaxError);
				}
			}
			return list;
		}

		// Token: 0x04001642 RID: 5698
		private readonly List<Span.SpanPath> _spanList;

		// Token: 0x04001643 RID: 5699
		private string _cacheKey;

		// Token: 0x020005BD RID: 1469
		internal class SpanPath
		{
			// Token: 0x06003AE9 RID: 15081 RVA: 0x00117ABB File Offset: 0x00115CBB
			public SpanPath(List<string> navigations)
			{
				this.Navigations = navigations;
			}

			// Token: 0x06003AEA RID: 15082 RVA: 0x00117ACC File Offset: 0x00115CCC
			public bool IsSubPath(Span.SpanPath rhs)
			{
				if (this.Navigations.Count > rhs.Navigations.Count)
				{
					return false;
				}
				for (int i = 0; i < this.Navigations.Count; i++)
				{
					if (!this.Navigations[i].Equals(rhs.Navigations[i], StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04001644 RID: 5700
			public readonly List<string> Navigations;
		}
	}
}
