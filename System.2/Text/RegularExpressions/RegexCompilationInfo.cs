using System;
using System.Runtime.Serialization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000691 RID: 1681
	[Serializable]
	public class RegexCompilationInfo
	{
		// Token: 0x06003E35 RID: 15925 RVA: 0x00100C6D File Offset: 0x000FEE6D
		[OnDeserializing]
		private void InitMatchTimeoutDefaultForOldVersionDeserialization(StreamingContext unusedContext)
		{
			this.matchTimeout = Regex.DefaultMatchTimeout;
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x00100C7A File Offset: 0x000FEE7A
		public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic) : this(pattern, options, name, fullnamespace, ispublic, Regex.DefaultMatchTimeout)
		{
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x00100C8E File Offset: 0x000FEE8E
		public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic, TimeSpan matchTimeout)
		{
			this.Pattern = pattern;
			this.Name = name;
			this.Namespace = fullnamespace;
			this.options = options;
			this.isPublic = ispublic;
			this.MatchTimeout = matchTimeout;
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x00100CC3 File Offset: 0x000FEEC3
		// (set) Token: 0x06003E39 RID: 15929 RVA: 0x00100CCB File Offset: 0x000FEECB
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.pattern = value;
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x00100CE2 File Offset: 0x000FEEE2
		// (set) Token: 0x06003E3B RID: 15931 RVA: 0x00100CEA File Offset: 0x000FEEEA
		public RegexOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x00100CF3 File Offset: 0x000FEEF3
		// (set) Token: 0x06003E3D RID: 15933 RVA: 0x00100CFC File Offset: 0x000FEEFC
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[]
					{
						"value"
					}), "value");
				}
				this.name = value;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x00100D49 File Offset: 0x000FEF49
		// (set) Token: 0x06003E3F RID: 15935 RVA: 0x00100D51 File Offset: 0x000FEF51
		public string Namespace
		{
			get
			{
				return this.nspace;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.nspace = value;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003E40 RID: 15936 RVA: 0x00100D68 File Offset: 0x000FEF68
		// (set) Token: 0x06003E41 RID: 15937 RVA: 0x00100D70 File Offset: 0x000FEF70
		public bool IsPublic
		{
			get
			{
				return this.isPublic;
			}
			set
			{
				this.isPublic = value;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x00100D79 File Offset: 0x000FEF79
		// (set) Token: 0x06003E43 RID: 15939 RVA: 0x00100D81 File Offset: 0x000FEF81
		public TimeSpan MatchTimeout
		{
			get
			{
				return this.matchTimeout;
			}
			set
			{
				Regex.ValidateMatchTimeout(value);
				this.matchTimeout = value;
			}
		}

		// Token: 0x04002D73 RID: 11635
		private string pattern;

		// Token: 0x04002D74 RID: 11636
		private RegexOptions options;

		// Token: 0x04002D75 RID: 11637
		private string name;

		// Token: 0x04002D76 RID: 11638
		private string nspace;

		// Token: 0x04002D77 RID: 11639
		private bool isPublic;

		// Token: 0x04002D78 RID: 11640
		[OptionalField(VersionAdded = 2)]
		private TimeSpan matchTimeout;
	}
}
