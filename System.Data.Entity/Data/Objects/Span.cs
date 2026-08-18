using System;
using System.Collections.Generic;
using System.Data.Common.Internal;
using System.Data.Entity;
using System.Text;

namespace System.Data.Objects
{
	// Token: 0x02000151 RID: 337
	internal sealed class Span
	{
		// Token: 0x0600189C RID: 6300 RVA: 0x000541D3 File Offset: 0x000523D3
		internal Span()
		{
			this._spanList = new List<Span.SpanPath>();
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x000541E6 File Offset: 0x000523E6
		internal List<Span.SpanPath> SpanList
		{
			get
			{
				return this._spanList;
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x000541EE File Offset: 0x000523EE
		internal static bool RequiresRelationshipSpan(MergeOption mergeOption)
		{
			return mergeOption != MergeOption.NoTracking;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x000541F7 File Offset: 0x000523F7
		internal static Span IncludeIn(Span spanToIncludeIn, string pathToInclude)
		{
			if (spanToIncludeIn == null)
			{
				spanToIncludeIn = new Span();
			}
			spanToIncludeIn.Include(pathToInclude);
			return spanToIncludeIn;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0005420C File Offset: 0x0005240C
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

		// Token: 0x060018A1 RID: 6305 RVA: 0x00054274 File Offset: 0x00052474
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

		// Token: 0x060018A2 RID: 6306 RVA: 0x0005437C File Offset: 0x0005257C
		public void Include(string path)
		{
			EntityUtil.CheckStringArgument(path, "path");
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException(Strings.ObjectQuery_Span_WhiteSpacePath, "path");
			}
			Span.SpanPath spanPath = new Span.SpanPath(Span.ParsePath(path));
			this.AddSpanPath(spanPath);
			this._cacheKey = null;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x000543CC File Offset: 0x000525CC
		internal Span Clone()
		{
			Span span = new Span();
			span.SpanList.AddRange(this._spanList);
			span._cacheKey = this._cacheKey;
			return span;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x000543FD File Offset: 0x000525FD
		internal void AddSpanPath(Span.SpanPath spanPath)
		{
			if (this.ValidateSpanPath(spanPath))
			{
				this.RemoveExistingSubPaths(spanPath);
				this._spanList.Add(spanPath);
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0005441C File Offset: 0x0005261C
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

		// Token: 0x060018A6 RID: 6310 RVA: 0x00054458 File Offset: 0x00052658
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

		// Token: 0x060018A7 RID: 6311 RVA: 0x000544F0 File Offset: 0x000526F0
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
					throw EntityUtil.SpanPathSyntaxError();
				}
			}
			return list;
		}

		// Token: 0x04000AD1 RID: 2769
		private List<Span.SpanPath> _spanList;

		// Token: 0x04000AD2 RID: 2770
		private string _cacheKey;

		// Token: 0x020004A7 RID: 1191
		internal class SpanPath
		{
			// Token: 0x06003C40 RID: 15424 RVA: 0x000E2AD7 File Offset: 0x000E0CD7
			public SpanPath(List<string> navigations)
			{
				this.Navigations = navigations;
			}

			// Token: 0x06003C41 RID: 15425 RVA: 0x000E2AE8 File Offset: 0x000E0CE8
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

			// Token: 0x04001A43 RID: 6723
			public readonly List<string> Navigations;
		}
	}
}
