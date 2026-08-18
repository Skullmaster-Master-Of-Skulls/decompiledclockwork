using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Properties;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x0200006A RID: 106
	internal class MimeMultipartParser
	{
		// Token: 0x0600039A RID: 922 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		public MimeMultipartParser(string boundary, long maxMessageSize)
		{
			if (maxMessageSize < 10L)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxMessageSize", maxMessageSize, 10);
			}
			if (string.IsNullOrWhiteSpace(boundary))
			{
				throw Error.ArgumentNull("boundary");
			}
			if (boundary.Length > 246)
			{
				throw Error.ArgumentMustBeLessThanOrEqualTo("boundary", boundary.Length, 246);
			}
			if (boundary.EndsWith(" ", StringComparison.Ordinal))
			{
				throw Error.Argument("boundary", Resources.MimeMultipartParserBadBoundary, new object[0]);
			}
			this._maxMessageSize = maxMessageSize;
			this._boundary = boundary;
			this._currentBoundary = new MimeMultipartParser.CurrentBodyPartStore(this._boundary);
			this._bodyPartState = MimeMultipartParser.BodyPartState.AfterFirstLineFeed;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000F09B File Offset: 0x0000D29B
		public bool IsWaitingForEndOfMessage
		{
			get
			{
				return this._bodyPartState == MimeMultipartParser.BodyPartState.AfterBoundary && this._currentBoundary != null && this._currentBoundary.IsFinal;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000F0BB File Offset: 0x0000D2BB
		public bool CanParseMore(int bytesRead, int bytesConsumed)
		{
			return bytesConsumed < bytesRead || (bytesRead == 0 && this.IsWaitingForEndOfMessage);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		public MimeMultipartParser.State ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed, out ArraySegment<byte> remainingBodyPart, out ArraySegment<byte> bodyPart, out bool isFinalBodyPart)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			MimeMultipartParser.State state = MimeMultipartParser.State.NeedMoreData;
			remainingBodyPart = MimeMultipartParser._emptyBodyPart;
			bodyPart = MimeMultipartParser._emptyBodyPart;
			isFinalBodyPart = false;
			try
			{
				state = MimeMultipartParser.ParseBodyPart(buffer, bytesReady, ref bytesConsumed, ref this._bodyPartState, this._maxMessageSize, ref this._totalBytesConsumed, this._currentBoundary);
			}
			catch (Exception)
			{
				state = MimeMultipartParser.State.Invalid;
			}
			remainingBodyPart = this._currentBoundary.GetDiscardedBoundary();
			bodyPart = this._currentBoundary.BodyPart;
			if (state == MimeMultipartParser.State.BodyPartCompleted)
			{
				isFinalBodyPart = this._currentBoundary.IsFinal;
				this._currentBoundary.ClearAll();
			}
			else
			{
				this._currentBoundary.ClearBodyPart();
			}
			return state;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000F194 File Offset: 0x0000D394
		private static MimeMultipartParser.State ParseBodyPart(byte[] buffer, int bytesReady, ref int bytesConsumed, ref MimeMultipartParser.BodyPartState bodyPartState, long maximumMessageLength, ref long totalBytesConsumed, MimeMultipartParser.CurrentBodyPartStore currentBodyPart)
		{
			int num = bytesConsumed;
			if (bytesReady == 0 && bodyPartState == MimeMultipartParser.BodyPartState.AfterBoundary && currentBodyPart.IsFinal)
			{
				return MimeMultipartParser.State.BodyPartCompleted;
			}
			MimeMultipartParser.State state = MimeMultipartParser.State.DataTooBig;
			long num2 = (maximumMessageLength <= 0L) ? long.MaxValue : (maximumMessageLength - totalBytesConsumed + (long)bytesConsumed);
			if (num2 == 0L)
			{
				return MimeMultipartParser.State.DataTooBig;
			}
			if ((long)bytesReady <= num2)
			{
				state = MimeMultipartParser.State.NeedMoreData;
				num2 = (long)bytesReady;
			}
			currentBodyPart.ResetBoundaryOffset();
			switch (bodyPartState)
			{
			case MimeMultipartParser.BodyPartState.BodyPart:
				break;
			case MimeMultipartParser.BodyPartState.AfterFirstCarriageReturn:
				goto IL_B4;
			case MimeMultipartParser.BodyPartState.AfterFirstLineFeed:
				goto IL_E6;
			case MimeMultipartParser.BodyPartState.AfterFirstDash:
				goto IL_14A;
			case MimeMultipartParser.BodyPartState.Boundary:
				goto IL_17F;
			case MimeMultipartParser.BodyPartState.AfterBoundary:
				goto IL_1F0;
			case MimeMultipartParser.BodyPartState.AfterSecondDash:
				goto IL_2BB;
			case MimeMultipartParser.BodyPartState.AfterSecondCarriageReturn:
				goto IL_303;
			default:
				goto IL_349;
			}
			IL_8E:
			while (buffer[bytesConsumed] != 13)
			{
				if ((long)(++bytesConsumed) == num2)
				{
					goto IL_349;
				}
			}
			currentBodyPart.AppendBoundary(13);
			bodyPartState = MimeMultipartParser.BodyPartState.AfterFirstCarriageReturn;
			if ((long)(++bytesConsumed) == num2)
			{
				goto IL_349;
			}
			IL_B4:
			if (buffer[bytesConsumed] != 10)
			{
				currentBodyPart.ResetBoundary();
				bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
				goto IL_8E;
			}
			currentBodyPart.AppendBoundary(10);
			bodyPartState = MimeMultipartParser.BodyPartState.AfterFirstLineFeed;
			if ((long)(++bytesConsumed) == num2)
			{
				goto IL_349;
			}
			IL_E6:
			if (buffer[bytesConsumed] == 13)
			{
				currentBodyPart.ResetBoundary();
				currentBodyPart.AppendBoundary(13);
				bodyPartState = MimeMultipartParser.BodyPartState.AfterFirstCarriageReturn;
				if ((long)(++bytesConsumed) == num2)
				{
					goto IL_349;
				}
				goto IL_B4;
			}
			else
			{
				if (buffer[bytesConsumed] != 45)
				{
					currentBodyPart.ResetBoundary();
					bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
					goto IL_8E;
				}
				currentBodyPart.AppendBoundary(45);
				bodyPartState = MimeMultipartParser.BodyPartState.AfterFirstDash;
				if ((long)(++bytesConsumed) == num2)
				{
					goto IL_349;
				}
			}
			IL_14A:
			if (buffer[bytesConsumed] != 45)
			{
				currentBodyPart.ResetBoundary();
				bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
				goto IL_8E;
			}
			currentBodyPart.AppendBoundary(45);
			bodyPartState = MimeMultipartParser.BodyPartState.Boundary;
			if ((long)(++bytesConsumed) == num2)
			{
				goto IL_349;
			}
			IL_17F:
			int num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 13)
			{
				if ((long)(++bytesConsumed) == num2)
				{
					if (!currentBodyPart.AppendBoundary(buffer, num3, bytesConsumed - num3))
					{
						currentBodyPart.ResetBoundary();
						bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
						goto IL_349;
					}
					if (currentBodyPart.IsBoundaryComplete())
					{
						bodyPartState = MimeMultipartParser.BodyPartState.AfterBoundary;
						goto IL_349;
					}
					goto IL_349;
				}
			}
			if (bytesConsumed > num3 && !currentBodyPart.AppendBoundary(buffer, num3, bytesConsumed - num3))
			{
				currentBodyPart.ResetBoundary();
				bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
				goto IL_8E;
			}
			IL_1F0:
			if (buffer[bytesConsumed] == 45 && !currentBodyPart.IsFinal)
			{
				currentBodyPart.AppendBoundary(45);
				if ((long)(++bytesConsumed) == num2)
				{
					bodyPartState = MimeMultipartParser.BodyPartState.AfterSecondDash;
					goto IL_349;
				}
			}
			else
			{
				num3 = bytesConsumed;
				while (buffer[bytesConsumed] != 13)
				{
					if ((long)(++bytesConsumed) == num2)
					{
						if (!currentBodyPart.AppendBoundary(buffer, num3, bytesConsumed - num3))
						{
							currentBodyPart.ResetBoundary();
							bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
							goto IL_349;
						}
						goto IL_349;
					}
				}
				if (bytesConsumed > num3 && !currentBodyPart.AppendBoundary(buffer, num3, bytesConsumed - num3))
				{
					currentBodyPart.ResetBoundary();
					bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
					goto IL_8E;
				}
				if (buffer[bytesConsumed] != 13)
				{
					currentBodyPart.ResetBoundary();
					bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
					goto IL_8E;
				}
				currentBodyPart.AppendBoundary(13);
				if ((long)(++bytesConsumed) == num2)
				{
					bodyPartState = MimeMultipartParser.BodyPartState.AfterSecondCarriageReturn;
					goto IL_349;
				}
				goto IL_303;
			}
			IL_2BB:
			if (buffer[bytesConsumed] != 45)
			{
				currentBodyPart.ResetBoundary();
				bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
				goto IL_8E;
			}
			currentBodyPart.AppendBoundary(45);
			bytesConsumed++;
			if (currentBodyPart.IsBoundaryComplete())
			{
				bodyPartState = MimeMultipartParser.BodyPartState.AfterBoundary;
				state = MimeMultipartParser.State.NeedMoreData;
				goto IL_349;
			}
			currentBodyPart.ResetBoundary();
			if ((long)bytesConsumed == num2)
			{
				goto IL_349;
			}
			goto IL_8E;
			IL_303:
			if (buffer[bytesConsumed] != 10)
			{
				currentBodyPart.ResetBoundary();
				bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
				goto IL_8E;
			}
			currentBodyPart.AppendBoundary(10);
			bytesConsumed++;
			bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
			if (currentBodyPart.IsBoundaryComplete())
			{
				state = MimeMultipartParser.State.BodyPartCompleted;
			}
			else
			{
				currentBodyPart.ResetBoundary();
				if ((long)bytesConsumed != num2)
				{
					goto IL_8E;
				}
			}
			IL_349:
			if (num < bytesConsumed)
			{
				int boundaryDelta = currentBodyPart.BoundaryDelta;
				if (boundaryDelta > 0 && state != MimeMultipartParser.State.BodyPartCompleted)
				{
					currentBodyPart.HasPotentialBoundaryLeftOver = true;
				}
				int count = bytesConsumed - num - boundaryDelta;
				currentBodyPart.BodyPart = new ArraySegment<byte>(buffer, num, count);
			}
			totalBytesConsumed += (long)(bytesConsumed - num);
			return state;
		}

		// Token: 0x04000157 RID: 343
		internal const int MinMessageSize = 10;

		// Token: 0x04000158 RID: 344
		private const int MaxBoundarySize = 256;

		// Token: 0x04000159 RID: 345
		private const byte HTAB = 9;

		// Token: 0x0400015A RID: 346
		private const byte SP = 32;

		// Token: 0x0400015B RID: 347
		private const byte CR = 13;

		// Token: 0x0400015C RID: 348
		private const byte LF = 10;

		// Token: 0x0400015D RID: 349
		private const byte Dash = 45;

		// Token: 0x0400015E RID: 350
		private static readonly ArraySegment<byte> _emptyBodyPart = new ArraySegment<byte>(new byte[0]);

		// Token: 0x0400015F RID: 351
		private long _totalBytesConsumed;

		// Token: 0x04000160 RID: 352
		private long _maxMessageSize;

		// Token: 0x04000161 RID: 353
		private MimeMultipartParser.BodyPartState _bodyPartState;

		// Token: 0x04000162 RID: 354
		private string _boundary;

		// Token: 0x04000163 RID: 355
		private MimeMultipartParser.CurrentBodyPartStore _currentBoundary;

		// Token: 0x0200006B RID: 107
		private enum BodyPartState
		{
			// Token: 0x04000165 RID: 357
			BodyPart,
			// Token: 0x04000166 RID: 358
			AfterFirstCarriageReturn,
			// Token: 0x04000167 RID: 359
			AfterFirstLineFeed,
			// Token: 0x04000168 RID: 360
			AfterFirstDash,
			// Token: 0x04000169 RID: 361
			Boundary,
			// Token: 0x0400016A RID: 362
			AfterBoundary,
			// Token: 0x0400016B RID: 363
			AfterSecondDash,
			// Token: 0x0400016C RID: 364
			AfterSecondCarriageReturn
		}

		// Token: 0x0200006C RID: 108
		private enum MessageState
		{
			// Token: 0x0400016E RID: 366
			Boundary,
			// Token: 0x0400016F RID: 367
			BodyPart,
			// Token: 0x04000170 RID: 368
			CloseDelimiter
		}

		// Token: 0x0200006D RID: 109
		public enum State
		{
			// Token: 0x04000172 RID: 370
			NeedMoreData,
			// Token: 0x04000173 RID: 371
			BodyPartCompleted,
			// Token: 0x04000174 RID: 372
			Invalid,
			// Token: 0x04000175 RID: 373
			DataTooBig
		}

		// Token: 0x0200006E RID: 110
		[DebuggerDisplay("{DebuggerToString()}")]
		private class CurrentBodyPartStore
		{
			// Token: 0x060003A0 RID: 928 RVA: 0x0000F540 File Offset: 0x0000D740
			public CurrentBodyPartStore(string referenceBoundary)
			{
				this._referenceBoundary[0] = 13;
				this._referenceBoundary[1] = 10;
				this._referenceBoundary[2] = 45;
				this._referenceBoundary[3] = 45;
				this._referenceBoundaryLength = 4 + Encoding.UTF8.GetBytes(referenceBoundary, 0, referenceBoundary.Length, this._referenceBoundary, 4);
				this._boundary[0] = 13;
				this._boundary[1] = 10;
				this._boundaryLength = 2;
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000F5F9 File Offset: 0x0000D7F9
			// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000F601 File Offset: 0x0000D801
			public bool HasPotentialBoundaryLeftOver { get; set; }

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000F60A File Offset: 0x0000D80A
			public int BoundaryDelta
			{
				get
				{
					if (this._boundaryLength - this._boundaryOffset <= 0)
					{
						return this._boundaryLength;
					}
					return this._boundaryLength - this._boundaryOffset;
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000F630 File Offset: 0x0000D830
			// (set) Token: 0x060003A5 RID: 933 RVA: 0x0000F638 File Offset: 0x0000D838
			public ArraySegment<byte> BodyPart
			{
				get
				{
					return this._bodyPart;
				}
				set
				{
					this._bodyPart = value;
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000F641 File Offset: 0x0000D841
			public bool IsFinal
			{
				get
				{
					return this._isFinal;
				}
			}

			// Token: 0x060003A7 RID: 935 RVA: 0x0000F649 File Offset: 0x0000D849
			public void ResetBoundaryOffset()
			{
				this._boundaryOffset = this._boundaryLength;
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x0000F658 File Offset: 0x0000D858
			public void ResetBoundary()
			{
				if (this.HasPotentialBoundaryLeftOver)
				{
					Buffer.BlockCopy(this._boundary, 0, this._boundaryStore, 0, this._boundaryOffset);
					this._boundaryStoreLength = this._boundaryOffset;
					this.HasPotentialBoundaryLeftOver = false;
					this._releaseDiscardedBoundary = true;
				}
				this._boundaryLength = 0;
				this._boundaryOffset = 0;
			}

			// Token: 0x060003A9 RID: 937 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
			public void AppendBoundary(byte data)
			{
				this._boundary[this._boundaryLength++] = data;
			}

			// Token: 0x060003AA RID: 938 RVA: 0x0000F6D8 File Offset: 0x0000D8D8
			public bool AppendBoundary(byte[] data, int offset, int count)
			{
				if (this._boundaryLength + count > this._referenceBoundaryLength + 6)
				{
					return false;
				}
				int i = this._boundaryLength;
				Buffer.BlockCopy(data, offset, this._boundary, this._boundaryLength, count);
				this._boundaryLength += count;
				int num = Math.Min(this._boundaryLength, this._referenceBoundaryLength);
				while (i < num)
				{
					if (this._boundary[i] != this._referenceBoundary[i])
					{
						return false;
					}
					i++;
				}
				return true;
			}

			// Token: 0x060003AB RID: 939 RVA: 0x0000F754 File Offset: 0x0000D954
			public ArraySegment<byte> GetDiscardedBoundary()
			{
				if (this._boundaryStoreLength > 0 && this._releaseDiscardedBoundary)
				{
					ArraySegment<byte> result = new ArraySegment<byte>(this._boundaryStore, 0, this._boundaryStoreLength);
					this._boundaryStoreLength = 0;
					return result;
				}
				return MimeMultipartParser._emptyBodyPart;
			}

			// Token: 0x060003AC RID: 940 RVA: 0x0000F794 File Offset: 0x0000D994
			public bool IsBoundaryValid()
			{
				int num = 0;
				if (this._isFirst)
				{
					num = 2;
				}
				int i;
				for (i = num; i < this._referenceBoundaryLength; i++)
				{
					if (this._boundary[i] != this._referenceBoundary[i])
					{
						return false;
					}
				}
				bool isFinal = false;
				if (this._boundary[i] == 45 && this._boundary[i + 1] == 45)
				{
					isFinal = true;
					i += 2;
				}
				while (i < this._boundaryLength - 2)
				{
					if (this._boundary[i] != 32 && this._boundary[i] != 9)
					{
						return false;
					}
					i++;
				}
				this._isFinal = isFinal;
				this._isFirst = false;
				return true;
			}

			// Token: 0x060003AD RID: 941 RVA: 0x0000F82E File Offset: 0x0000DA2E
			public bool IsBoundaryComplete()
			{
				return this.IsBoundaryValid() && this._boundaryLength >= this._referenceBoundaryLength && (this._boundaryLength != this._referenceBoundaryLength + 1 || this._boundary[this._referenceBoundaryLength] != 45);
			}

			// Token: 0x060003AE RID: 942 RVA: 0x0000F86E File Offset: 0x0000DA6E
			public void ClearBodyPart()
			{
				this.BodyPart = MimeMultipartParser._emptyBodyPart;
			}

			// Token: 0x060003AF RID: 943 RVA: 0x0000F87B File Offset: 0x0000DA7B
			public void ClearAll()
			{
				this._releaseDiscardedBoundary = false;
				this.HasPotentialBoundaryLeftOver = false;
				this._boundaryLength = 0;
				this._boundaryOffset = 0;
				this._boundaryStoreLength = 0;
				this._isFinal = false;
				this.ClearBodyPart();
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
			private string DebuggerToString()
			{
				string @string = Encoding.UTF8.GetString(this._referenceBoundary, 0, this._referenceBoundaryLength);
				string string2 = Encoding.UTF8.GetString(this._boundary, 0, this._boundaryLength);
				return string.Format(CultureInfo.InvariantCulture, "Expected: {0} *** Current: {1}", new object[]
				{
					@string,
					string2
				});
			}

			// Token: 0x04000176 RID: 374
			private const int InitialOffset = 2;

			// Token: 0x04000177 RID: 375
			private byte[] _boundaryStore = new byte[256];

			// Token: 0x04000178 RID: 376
			private int _boundaryStoreLength;

			// Token: 0x04000179 RID: 377
			private byte[] _referenceBoundary = new byte[256];

			// Token: 0x0400017A RID: 378
			private int _referenceBoundaryLength;

			// Token: 0x0400017B RID: 379
			private byte[] _boundary = new byte[256];

			// Token: 0x0400017C RID: 380
			private int _boundaryLength;

			// Token: 0x0400017D RID: 381
			private ArraySegment<byte> _bodyPart = MimeMultipartParser._emptyBodyPart;

			// Token: 0x0400017E RID: 382
			private bool _isFinal;

			// Token: 0x0400017F RID: 383
			private bool _isFirst = true;

			// Token: 0x04000180 RID: 384
			private bool _releaseDiscardedBoundary;

			// Token: 0x04000181 RID: 385
			private int _boundaryOffset;
		}
	}
}
