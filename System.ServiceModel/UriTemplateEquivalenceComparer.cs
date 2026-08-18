using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace System
{
	// Token: 0x0200000A RID: 10
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class UriTemplateEquivalenceComparer : IEqualityComparer<UriTemplate>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003D5C File Offset: 0x00001F5C
		internal static UriTemplateEquivalenceComparer Instance
		{
			get
			{
				if (UriTemplateEquivalenceComparer.instance == null)
				{
					UriTemplateEquivalenceComparer.instance = new UriTemplateEquivalenceComparer();
				}
				return UriTemplateEquivalenceComparer.instance;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003D74 File Offset: 0x00001F74
		public bool Equals(UriTemplate x, UriTemplate y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x.IsEquivalentTo(y);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003D88 File Offset: 0x00001F88
		public int GetHashCode(UriTemplate obj)
		{
			if (obj == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("obj");
			}
			for (int i = obj.segments.Count - 1; i >= 0; i--)
			{
				if (obj.segments[i].Nature == UriTemplatePartType.Literal)
				{
					return obj.segments[i].GetHashCode();
				}
			}
			return obj.segments.Count + obj.queries.Count;
		}

		// Token: 0x04000063 RID: 99
		private static UriTemplateEquivalenceComparer instance;
	}
}
