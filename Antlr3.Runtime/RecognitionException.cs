using System;
using System.Runtime.Serialization;
using Antlr.Runtime.Tree;

namespace Antlr.Runtime
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class RecognitionException : Exception
	{
		// Token: 0x0600014D RID: 333 RVA: 0x000048FB File Offset: 0x00002AFB
		public RecognitionException() : this("A recognition error occurred.", null, null)
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000490A File Offset: 0x00002B0A
		public RecognitionException(IIntStream input) : this("A recognition error occurred.", input, 1, null)
		{
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000491A File Offset: 0x00002B1A
		public RecognitionException(IIntStream input, int k) : this("A recognition error occurred.", input, k, null)
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000492A File Offset: 0x00002B2A
		public RecognitionException(string message) : this(message, null, null)
		{
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004935 File Offset: 0x00002B35
		public RecognitionException(string message, IIntStream input) : this(message, input, 1, null)
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004941 File Offset: 0x00002B41
		public RecognitionException(string message, IIntStream input, int k) : this(message, input, k, null)
		{
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000494D File Offset: 0x00002B4D
		public RecognitionException(string message, Exception innerException) : this(message, null, innerException)
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004958 File Offset: 0x00002B58
		public RecognitionException(string message, IIntStream input, Exception innerException) : this(message, input, 1, innerException)
		{
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004964 File Offset: 0x00002B64
		public RecognitionException(string message, IIntStream input, int k, Exception innerException) : base(message, innerException)
		{
			this._input = input;
			this._k = k;
			if (input != null)
			{
				this._index = input.Index + k - 1;
				if (input is ITokenStream)
				{
					this._token = ((ITokenStream)input).LT(k);
					this._line = this._token.Line;
					this._charPositionInLine = this._token.CharPositionInLine;
				}
				ITreeNodeStream treeNodeStream = input as ITreeNodeStream;
				if (treeNodeStream != null)
				{
					this.ExtractInformationFromTreeNodeStream(treeNodeStream, k);
					return;
				}
				ICharStream charStream = input as ICharStream;
				if (charStream != null)
				{
					int marker = input.Mark();
					try
					{
						for (int i = 0; i < k - 1; i++)
						{
							input.Consume();
						}
						this._c = input.LA(1);
						this._line = ((ICharStream)input).Line;
						this._charPositionInLine = ((ICharStream)input).CharPositionInLine;
						return;
					}
					finally
					{
						input.Rewind(marker);
					}
				}
				this._c = input.LA(k);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004A68 File Offset: 0x00002C68
		protected RecognitionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._index = info.GetInt32("Index");
			this._c = info.GetInt32("C");
			this._line = info.GetInt32("Line");
			this._charPositionInLine = info.GetInt32("CharPositionInLine");
			this._approximateLineInfo = info.GetBoolean("ApproximateLineInfo");
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public virtual int UnexpectedType
		{
			get
			{
				if (this._input is ITokenStream)
				{
					return this._token.Type;
				}
				ITreeNodeStream treeNodeStream = this._input as ITreeNodeStream;
				if (treeNodeStream != null)
				{
					ITreeAdaptor treeAdaptor = treeNodeStream.TreeAdaptor;
					return treeAdaptor.GetType(this._node);
				}
				return this._c;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00004B2F File Offset: 0x00002D2F
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00004B37 File Offset: 0x00002D37
		public bool ApproximateLineInfo
		{
			get
			{
				return this._approximateLineInfo;
			}
			protected set
			{
				this._approximateLineInfo = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004B40 File Offset: 0x00002D40
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00004B48 File Offset: 0x00002D48
		public IIntStream Input
		{
			get
			{
				return this._input;
			}
			protected set
			{
				this._input = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004B51 File Offset: 0x00002D51
		public int Lookahead
		{
			get
			{
				return this._k;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004B59 File Offset: 0x00002D59
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00004B61 File Offset: 0x00002D61
		public IToken Token
		{
			get
			{
				return this._token;
			}
			set
			{
				this._token = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004B6A File Offset: 0x00002D6A
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00004B72 File Offset: 0x00002D72
		public object Node
		{
			get
			{
				return this._node;
			}
			protected set
			{
				this._node = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004B7B File Offset: 0x00002D7B
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00004B83 File Offset: 0x00002D83
		public int Character
		{
			get
			{
				return this._c;
			}
			protected set
			{
				this._c = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004B8C File Offset: 0x00002D8C
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00004B94 File Offset: 0x00002D94
		public int Index
		{
			get
			{
				return this._index;
			}
			protected set
			{
				this._index = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004B9D File Offset: 0x00002D9D
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00004BA5 File Offset: 0x00002DA5
		public int Line
		{
			get
			{
				return this._line;
			}
			set
			{
				this._line = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00004BAE File Offset: 0x00002DAE
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00004BB6 File Offset: 0x00002DB6
		public int CharPositionInLine
		{
			get
			{
				return this._charPositionInLine;
			}
			set
			{
				this._charPositionInLine = value;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004BC0 File Offset: 0x00002DC0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Index", this._index);
			info.AddValue("C", this._c);
			info.AddValue("Line", this._line);
			info.AddValue("CharPositionInLine", this._charPositionInLine);
			info.AddValue("ApproximateLineInfo", this._approximateLineInfo);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004C38 File Offset: 0x00002E38
		protected virtual void ExtractInformationFromTreeNodeStream(ITreeNodeStream input)
		{
			this._node = input.LT(1);
			object obj = null;
			IPositionTrackingStream positionTrackingStream = input as IPositionTrackingStream;
			if (positionTrackingStream != null)
			{
				obj = positionTrackingStream.GetKnownPositionElement(false);
				if (obj == null)
				{
					obj = positionTrackingStream.GetKnownPositionElement(true);
					this._approximateLineInfo = (obj != null);
				}
			}
			ITokenStreamInformation tokenStreamInformation = input as ITokenStreamInformation;
			if (tokenStreamInformation != null)
			{
				IToken lastToken = tokenStreamInformation.LastToken;
				IToken lastRealToken = tokenStreamInformation.LastRealToken;
				if (lastRealToken != null)
				{
					this._token = lastRealToken;
					this._line = lastRealToken.Line;
					this._charPositionInLine = lastRealToken.CharPositionInLine;
					this._approximateLineInfo = lastRealToken.Equals(lastToken);
					return;
				}
			}
			else
			{
				ITreeAdaptor treeAdaptor = input.TreeAdaptor;
				IToken token = treeAdaptor.GetToken(obj ?? this._node);
				if (token != null)
				{
					this._token = token;
					if (token.Line <= 0)
					{
						int num = -1;
						object t = input.LT(num);
						while (t != null)
						{
							IToken token2 = treeAdaptor.GetToken(t);
							if (token2 != null && token2.Line > 0)
							{
								this._line = token2.Line;
								this._charPositionInLine = token2.CharPositionInLine;
								this._approximateLineInfo = true;
								return;
							}
							num--;
							try
							{
								t = input.LT(num);
							}
							catch (NotSupportedException)
							{
								t = null;
							}
						}
						return;
					}
					this._line = token.Line;
					this._charPositionInLine = token.CharPositionInLine;
					return;
				}
				else if (this._node is ITree)
				{
					this._line = ((ITree)this._node).Line;
					this._charPositionInLine = ((ITree)this._node).CharPositionInLine;
					if (this._node is CommonTree)
					{
						this._token = ((CommonTree)this._node).Token;
						return;
					}
				}
				else
				{
					int type = treeAdaptor.GetType(this._node);
					string text = treeAdaptor.GetText(this._node);
					this._token = new CommonToken(type, text);
				}
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004E20 File Offset: 0x00003020
		protected virtual void ExtractInformationFromTreeNodeStream(ITreeNodeStream input, int k)
		{
			int marker = input.Mark();
			try
			{
				for (int i = 0; i < k - 1; i++)
				{
					input.Consume();
				}
				this.ExtractInformationFromTreeNodeStream(input);
			}
			finally
			{
				input.Rewind(marker);
			}
		}

		// Token: 0x04000040 RID: 64
		private IIntStream _input;

		// Token: 0x04000041 RID: 65
		private int _k;

		// Token: 0x04000042 RID: 66
		private int _index;

		// Token: 0x04000043 RID: 67
		private IToken _token;

		// Token: 0x04000044 RID: 68
		private object _node;

		// Token: 0x04000045 RID: 69
		private int _c;

		// Token: 0x04000046 RID: 70
		private int _line;

		// Token: 0x04000047 RID: 71
		private int _charPositionInLine;

		// Token: 0x04000048 RID: 72
		private bool _approximateLineInfo;
	}
}
